#  			[基于ASP.NET WEB API实现分布式数据访问中间层（提供对数据库的CRUD）](https://www.cnblogs.com/zuowj/p/8039891.html) 		



一些小的C/S项目（winform、WPF等），因需要访问操作数据库，但又不能把DB连接配置在客户端上，原因有很多，可能是DB连接无法直接访问，或客户端不想安装各种DB访问组件，或DB连接不想暴露在客户端（即使加密连接字符串仍有可能被破解的情况），总之都是出于安全考虑，同时因项目小，也无需采用分布式架构来将业务操作封装到服务端，但又想保证客户端业务的正常处理，这时我们就可以利用ASP.NET  WEB API框架开发一个简单的提供对数据库的直接操作（CRUD）框架，简称为：分布式数据访问中间层。

实现方案很简单，就是利用ASP.NET WEB  API框架编写于一个DataController，然后在DataController分别实现CRUD相关的公开ACTION方法即可，具体实现代码如下：（因为逻辑简单，一看就懂，故下面不再详细说明逻辑，文末会有一些总结）

**ASP.NET WEB API服务端相关核心代码：**

1.DataController代码：

[![复制代码](assets/copycode-1558869092742.gif)](javascript:void(0);)

```
    [SqlInjectionFilter]
    [Authorize]
    public class DataController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        public ApiResultInfo Login([FromBody]string[] loginInfo)
        {
            ApiResultInfo loginResult = null;
            try
            {
                if (loginInfo == null || loginInfo.Length != 4)
                {
                    throw new Exception("登录信息不全。");
                }

                using (var da = BaseUtil.CreateDataAccess())
                {
                    if (用户名及密码判断逻辑)
                    {
                        throw new Exception("登录名或密码错误。");
                    }
                    else
                    {
                        string token = Guid.NewGuid().ToString("N");
                        HttpRuntime.Cache.Insert(Constants.CacheKey_SessionTokenPrefix + token, loginInfo[0], null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
                    
                        //登录成功后需要处理的逻辑


                        loginResult = ApiResultInfo.BuildOKResult(token);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUitl.Error(ex, "Api.Data.Login", BaseUtil.SerializeToJson(loginInfo));
                loginResult = ApiResultInfo.BuildErrResult("LoginErr", ex.Message);
            }

            return loginResult;
        }

        [HttpPost]
        public ApiResultInfo LogOut([FromBody] string token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    if (HttpRuntime.Cache[Constants.CacheKey_SessionTokenPrefix + token] != null)
                    {
                        HttpRuntime.Cache.Remove(token);
                    }

                    using (var da = BaseUtil.CreateDataAccess())
                    {
                        //登出后需要处理的逻辑
                    }
                }
            }
            catch
            { }

            return ApiResultInfo.BuildOKResult();
        }


        [HttpPost]
        public ApiResultInfo GetValue([FromBody]SqlCmdInfo sqlCmd)
        {
            using (var da = BaseUtil.CreateDataAccess())
            {
                var result = da.ExecuteScalar<string>(sqlCmd.SqlCmdText, sqlCmd.GetCommandType(), sqlCmd.Parameters.TryToArray());
                return ApiResultInfo.BuildOKResult(result);
            }

        }


        [Compression]
        [HttpPost]
        public ApiResultInfo GetDataSet([FromBody]SqlCmdInfo sqlCmd)
        {
            using (var da = BaseUtil.CreateDataAccess())
            {
                var ds = da.ExecuteDataSet(sqlCmd.SqlCmdText, sqlCmd.GetCommandType(), sqlCmd.Parameters.TryToArray());
                return ApiResultInfo.BuildOKResult(ds);
            }
        }

        [Compression]
        [HttpPost]
        public ApiResultInfo GetDataTable([FromBody]SqlCmdInfo sqlCmd)
        {
            using (var da = BaseUtil.CreateDataAccess())
            {
                var table = da.ExecuteDataTable(sqlCmd.SqlCmdText, sqlCmd.GetCommandType(), sqlCmd.Parameters.TryToArray());
                return ApiResultInfo.BuildOKResult(table);
            }
        }


        [HttpPost]
        public ApiResultInfo ExecuteCommand([FromBody]SqlCmdInfo sqlCmd)
        {
            using (var da = BaseUtil.CreateDataAccess())
            {
                int result = da.ExecuteCommand(sqlCmd.SqlCmdText, sqlCmd.GetCommandType(), sqlCmd.Parameters.TryToArray());
                return ApiResultInfo.BuildOKResult(result);
            }
        }

        [HttpPost]
        public ApiResultInfo BatchExecuteCommand([FromBody] IEnumerable<SqlCmdInfo> sqlCmds)
        {
            using (var da = BaseUtil.CreateDataAccess())
            {
                int execCount = 0;
                da.UseTransaction();
                foreach (var sqlCmd in sqlCmds)
                {
                    execCount += da.ExecuteCommand(sqlCmd.SqlCmdText, sqlCmd.GetCommandType(), sqlCmd.Parameters.TryToArray());
                }
                da.Commit();
                return new ApiResultInfo(execCount > 0);
            }
        }


        [HttpPost]
        public async Task<ApiResultInfo> ExecuteCommandAsync([FromBody]SqlCmdInfo sqlCmd)
        {
            return await Task.Factory.StartNew((arg) =>
             {
                 var sqlCmdObj = arg as SqlCmdInfo;
                 string connName = BaseUtil.GetDbConnectionName(sqlCmdObj.DbType);
                 using (var da = BaseUtil.CreateDataAccess(connName))
                 {
                     try
                     {
                         int result = da.ExecuteCommand(sqlCmdObj.SqlCmdText, sqlCmdObj.GetCommandType(), sqlCmdObj.Parameters.TryToArray());
                         return ApiResultInfo.BuildOKResult(result);
                     }
                     catch (Exception ex)
                     {
                         LogUitl.Error(ex, "Api.Data.ExecuteCommandAsync", BaseUtil.SerializeToJson(sqlCmdObj));

                         return ApiResultInfo.BuildErrResult("ExecuteCommandAsyncErr", ex.Message,
                                new Dictionary<string, object> { { "StackTrace", ex.StackTrace } });
                     }
                 }
             }, sqlCmd);
        }

        [HttpPost]
        public IHttpActionResult SaveLog([FromBody]string[] logInfo)
        {
            if (logInfo == null || logInfo.Length < 3)
            {
                return Ok();
            }

            string[] saveLogInfo = new string[7];
            for (int i = 1; i < logInfo.Length; i++)
            {
                if (saveLogInfo.Length > i + 1)
                {
                    saveLogInfo[i] = logInfo[i];
                }
            }


            switch (saveLogInfo[0].ToUpperInvariant())
            {
                case "ERR":
                    {
                        LogUitl.Error(saveLogInfo[1], saveLogInfo[2], saveLogInfo[3], saveLogInfo[4], saveLogInfo[5], saveLogInfo[6]);
                        break;
                    }
                case "WARN":
                    {
                        LogUitl.Warn(saveLogInfo[1], saveLogInfo[2], saveLogInfo[3], saveLogInfo[4], saveLogInfo[5], saveLogInfo[6]);
                        break;
                    }
                case "INFO":
                    {
                        LogUitl.Info(saveLogInfo[1], saveLogInfo[2], saveLogInfo[3], saveLogInfo[4], saveLogInfo[5], saveLogInfo[6]);
                        break;
                    }
            }

            return Ok();

        }



    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 2.SqlInjectionFilterAttribute (防止SQL注入、危险关键字攻击过滤器)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SqlInjectionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsKey("sqlCmd"))
            {
                var sqlCmd = actionContext.ActionArguments["sqlCmd"] as SqlCmdInfo;
                if (BaseUtil.IsIncludeDangerSql(sqlCmd.SqlCmdText))
                {
                    throw new Exception("存在SQL注入风险，禁止操作！");
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

IsIncludeDangerSql：判断是否包含危险关键字

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
        /// <summary>
        /// 判断是否包含危险的SQL关键词
        /// </summary>
        /// <param name="sqlCmdText"></param>
        /// <returns>包含返回true，否则false</returns>
        public static bool IsIncludeDangerSql(string sqlCmdText)
        {
            if (string.IsNullOrWhiteSpace(sqlCmdText)) return false;

            sqlCmdText = sqlCmdText.Replace("[", " ").Replace("]", " ");

            //string dangerSqlObjs = @"sys\.columns|sys\.tables|sys\.views|sys\.objects|sys\.procedures|sys\.indexes|INFORMATION_SCHEMA\.TABLES|INFORMATION_SCHEMA\.VIEWS|INFORMATION_SCHEMA\.COLUMNS|GRANT|DENY|SP_HELP|SP_HELPTEXT";
            //dangerSqlObjs += @"|object_id|syscolumns|sysobjects|sysindexes|drop\s+\w+|alter\s+\w+|create\s+\w+";

            string dangerSqlObjs = @"sys\.\w+|INFORMATION_SCHEMA\.\w+|GRANT|DENY|SP_HELP|SP_HELPTEXT|sp_executesql";
            dangerSqlObjs += @"|object_id|syscolumns|sysobjects|sysindexes|exec\s+\(.+\)|(create|drop|alter)\s+(database|table|index|procedure|view|trigger)\s+\w+(?!#)";

            string patternStr = string.Format(@"(^|\s|,|\.)({0})(\s|,|\(|;|$)", dangerSqlObjs);
            bool mathed = Regex.IsMatch(sqlCmdText, patternStr, RegexOptions.IgnoreCase);
            if (mathed)
            {
                //TODO:记录到危险请求表中，以便后续追查
                LogUitl.Warn("检测到包含危险的SQL关键词语句：" + sqlCmdText, "IsIncludeDangerSql");
            }

            return mathed;
        }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

3.SqlCmdInfo （ACTION参数对象，SQL命令信息类）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    [Serializable]
    public class SqlCmdInfo
    {
        public string SqlCmdText { get; set; }

        public ArrayList Parameters { get; set; }

        public bool IsSPCmdType { get; set; }

        public int DbType { get; set; }

        public CommandType GetCommandType()
        {
            return IsSPCmdType ? CommandType.StoredProcedure : CommandType.Text;
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

4.CompressionAttribute（压缩返回内容过滤器，当返回的是大量数据时，可以标记该过滤器，以便提高响应速度）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    /// <summary>
    /// 压缩返回信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CompressionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var content = actionExecutedContext.Response.Content;

            #region 根据请求是否压缩，暂时不用
            ////var acceptEncoding = actionExecutedContext.Request.Headers.AcceptEncoding.
            ////    Where(x => x.Value == "gzip" || x.Value == "deflate").ToList();
            ////if (acceptEncoding.HasItem() && content != null && actionExecutedContext.Request.Method != HttpMethod.Options)
            ////{
            ////    var first = acceptEncoding.FirstOrDefault();
            ////    if (first != null)
            ////    {
            ////        var bytes = content.ReadAsByteArrayAsync().Result;
            ////        switch (first.Value)
            ////        {
            ////            case "gzip":
            ////                actionExecutedContext.Response.Content = new ByteArrayContent(CompressionHelper.GZipBytes(bytes));
            ////                actionExecutedContext.Response.Content.Headers.Add("Content-Encoding", "gzip");
            ////                break;
            ////            case "deflate":
            ////                actionExecutedContext.Response.Content = new ByteArrayContent(CompressionHelper.DeflateBytes(bytes));
            ////                actionExecutedContext.Response.Content.Headers.Add("Content-encoding", "deflate");
            ////                break;
            ////        }
            ////    }
            ////}

            #endregion

            //只要使用了CompressionAttribute，则默认使用GZIP压缩
            var bytes = content.ReadAsByteArrayAsync().Result;
            actionExecutedContext.Response.Content = new ByteArrayContent(CompressionHelper.GZipBytes(bytes));
            actionExecutedContext.Response.Content.Headers.Add("Content-Encoding", "gzip");

            base.OnActionExecuted(actionExecutedContext);
        }
    }
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    internal static class CompressionHelper
    {
        public static byte[] DeflateBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var output = new MemoryStream())
            {
                using (var compressor = new DeflateStream(output, CompressionMode.Compress, false))
                {
                    compressor.Write(bytes, 0, bytes.Length);
                }
                return output.ToArray();
            }
        }

        public static byte[] GZipBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var output = new MemoryStream())
            {
                using (var compressor = new GZipStream(output, CompressionMode.Compress, false))
                {
                    compressor.Write(bytes, 0, bytes.Length);
                }
                return output.ToArray();
            }
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 5.RequestAuthenticationHandler （验证请求合法性处理管道（包含请求内容解密），即：未正确登录则不能调API操作数据库）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    public class RequestAuthenticationHandler : DelegatingHandler
    {
        private const string rsaPrivateKey = "私钥字符串";
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                //验证TOKEN
                HttpRequestHeaders headers = request.Headers;
                IEnumerable<string> tokenHeaders = null;
                if (headers.TryGetValues("AccessToken", out tokenHeaders) && tokenHeaders.Any())
                {

                    string loginID = TokenVerification(tokenHeaders.ElementAt(0));

                    if (!string.IsNullOrEmpty(loginID))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(loginID, "token"), null);
                        Thread.CurrentPrincipal = principal;
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                    }
                }

                IEnumerable<string> encryptHeaders=null;
                if (headers.TryGetValues("Encryption", out encryptHeaders) && encryptHeaders.Any())
                {
                    if (encryptHeaders.ElementAt(0) == "1")
                    {
                        //私钥解密请求体内容
                        var originContent = request.Content;
                        string requestData = await request.Content.ReadAsStringAsync();

                        string deContentStr = EncryptUtility.RSADecrypt(rsaPrivateKey, requestData);
                        request.Content = new StringContent(deContentStr);

                        request.Content.Headers.Clear();
                        foreach (var header in originContent.Headers)
                        {
                            request.Content.Headers.Add(header.Key, header.Value);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUitl.Error(ex, "Api.RequestAuthenticationHandler");
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        private string TokenVerification(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            string loginID = null;
            if (HttpRuntime.Cache[Constants.CacheKey_SessionTokenPrefix + token] == null) //如果过期，则尝试从DB中恢复授权状态
            {
                using (var da = BaseUtil.CreateDataAccess())
                {
                    //loginID = 根据Token获取登录用户ID逻辑
                    if (!string.IsNullOrEmpty(loginID))
                    {
                        HttpRuntime.Cache.Insert(Constants.CacheKey_SessionTokenPrefix + token, loginID, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
                    }
                }
            }
            else
            {
                loginID = HttpRuntime.Cache[Constants.CacheKey_SessionTokenPrefix + token].ToNotNullString();
            }

            return loginID;

        }

    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 6.HandleExceptionFilterAttribute（全局异常处理过滤器，只要某个ACTION发生异常就会报被该过滤器捕获并处理）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    /// <summary>
    /// 统一全局异常过滤处理
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string ctrllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            string sqlCmd = null;
            if (actionExecutedContext.ActionContext.ActionArguments.ContainsKey("sqlCmd"))
            {
                sqlCmd = BaseUtil.SerializeToJson(actionExecutedContext.ActionContext.ActionArguments["sqlCmd"] as SqlCmdInfo);
            }

            //记录到日志表中
            LogUitl.Error(actionExecutedContext.Exception.Message, "Api.HandleExceptionFilterAttribute",
                            string.Format("SqlCmdInfo:{0};StackTrace:{1}", sqlCmd, actionExecutedContext.Exception.StackTrace));

            var errResult = new ApiResultInfo(false, sqlCmd, actionName + "Err", actionExecutedContext.Exception.Message);
            errResult.ExtendedData["StackTrace"] = actionExecutedContext.Exception.StackTrace;

            actionExecutedContext.Response = actionExecutedContext.ActionContext.Request.CreateResponse(HttpStatusCode.OK, errResult, "application/json");

        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

7.ApiResultInfo（API返回结果实体类）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    [Serializable]
    public class ApiResultInfo
    {
        public bool Stauts { get; set; }

        public object Data { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public Dictionary<string, object> ExtendedData { get; set; }


        public ApiResultInfo()
        {
            this.ExtendedData = new Dictionary<string, object>();
        }


        public ApiResultInfo(bool status, object data = null, string errCode = null, string errMsg = null, Dictionary<string, object> extData = null)
        {
            this.Stauts = status;
            this.Data = data;
            this.ErrCode = errCode;
            this.ErrMsg = errMsg;
            this.ExtendedData = extData;
            if (this.ExtendedData == null)
            {
                this.ExtendedData = new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// 构建成功结果对象
        /// </summary>
        /// <param name="data"></param>
        /// <param name="extData"></param>
        /// <returns></returns>
        public static ApiResultInfo BuildOKResult(object data = null, Dictionary<string, object> extData = null)
        {
            return new ApiResultInfo(true, data, extData: extData);
        }

        /// <summary>
        /// 构建错误结果对象
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        /// <param name="extData"></param>
        /// <returns></returns>
        public static ApiResultInfo BuildErrResult(string errCode = null, string errMsg = null, Dictionary<string, object> extData = null)
        {
            return new ApiResultInfo(false, errCode: errCode, errMsg: errMsg, extData: extData);
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 8.非对称加解密算法（允许客户端请求时进行公钥加密请求内容，然后服务端API中通过RequestAuthenticationHandler自定义验证管道解密请求内容） 

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
        /// <summary>
        /// 生成公钥及私钥对
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="privatekey"></param>
        public static void GeneratePublicAndPrivateKey(out string publickey, out string privatekey)
        {
            RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
            publickey = crypt.ToXmlString(false);//公钥
            privatekey = crypt.ToXmlString(true);//私钥
        }


        /// <summary>
        /// 分段使用公钥加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="rawInput"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publicKey, string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(publicKey);//载入公钥
                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    { //分段加密
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                }
            }
        }


        /// <summary>
        /// 分段使用私钥解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="encryptedInput"></param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, string encryptedInput)
        {
            if (string.IsNullOrEmpty(encryptedInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException("Invalid Private Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Convert.FromBase64String(encryptedInput);
                rsaProvider.FromXmlString(privateKey);
                int bufferSize = rsaProvider.KeySize / 8;
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    {
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var rawBytes = rsaProvider.Decrypt(temp, false);
                        outputStream.Write(rawBytes, 0, rawBytes.Length);
                    }
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
        }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

9.LogUitl（基于NLOG.MONGO组件简单封装实现MONGODB日志功能）--后期有机会再单独讲MONGODB的相关知识 

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    public static class LogUitl
    {
        private static NLog.Logger _Logger = null;
        private const string cacheKey_NLogConfigFlag = "NLogConfigFlag";
        private static Logger GetLogger()
        {
            if (_Logger == null || HttpRuntime.Cache[cacheKey_NLogConfigFlag] == null)
            {
                LoggingConfiguration config = new LoggingConfiguration();

                string connSetStr = ConfigUtility.GetAppSettingValue("MongoDbConnectionSet");

                MongoTarget mongoTarget = new MongoTarget();
                mongoTarget.ConnectionString = EncryptUtility.Decrypt(connSetStr);
                mongoTarget.DatabaseName = "KYELog";
                mongoTarget.CollectionName = "KYCallCenterLog";
                mongoTarget.IncludeDefaults = false;
                AppendLogMongoFields(mongoTarget.Fields);

                LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, mongoTarget);
                config.LoggingRules.Add(rule1);
                LogManager.Configuration = config;

                _Logger = LogManager.GetCurrentClassLogger();

                HttpRuntime.Cache.Insert(cacheKey_NLogConfigFlag, "Nlog", new System.Web.Caching.CacheDependency(HttpContext.Current.Server.MapPath("~/Web.config")));
            }

            return _Logger;

        }

        private static void AppendLogMongoFields(IList<MongoField> mongoFields)
        {
            mongoFields.Clear();
            Type logPropertiesType = typeof(SysLogInfo.LogProperties);
            foreach (var pro in typeof(SysLogInfo).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pro.PropertyType == logPropertiesType) continue;

                string layoutStr = string.Empty; //"${event-context:item=" + pro.Name + "}";
                if (pro.Name.Equals("ThreadID") || pro.Name.Equals("Level") || pro.Name.Equals("MachineName"))
                {
                    layoutStr = "${" + pro.Name.ToLower() + "}";
                }
                else if (pro.Name.Equals("LogDT"))
                {
                    layoutStr = "${date:format=yyyy-MM-dd HH\\:mm\\:ss}";
                }
                else if (pro.Name.Equals("Msg"))
                {
                    layoutStr = "${message}";
                }

                if (!string.IsNullOrEmpty(layoutStr))
                {
                    mongoFields.Add(new MongoField(pro.Name, layoutStr, pro.PropertyType.Name));
                }
            }
        }


        private static LogEventInfo BuildLogEventInfo(LogLevel level, string msg, string source, string detailTrace = null, string other1 = null, string other2 = null, string other3 = null)
        {
            var eventInfo = new LogEventInfo();
            eventInfo.Level = level;
            eventInfo.Message = msg;
            eventInfo.Properties["DetailTrace"] = detailTrace ?? string.Empty;
            eventInfo.Properties["Source"] = source ?? string.Empty;
            eventInfo.Properties["Other1"] = other1 ?? string.Empty;
            eventInfo.Properties["Other2"] = other2 ?? string.Empty;
            eventInfo.Properties["Other3"] = other3 ?? string.Empty;

            string uid = string.Empty;
            if (HttpContext.Current.User != null)
            {
                uid = HttpContext.Current.User.Identity.Name;
            }
            eventInfo.Properties["UserID"] = uid;

            return eventInfo;
        }

        public static void Info(string msg, string source, string detailTrace = null, string other1 = null, string other2 = null, string other3 = null)
        {
            try
            {
                var eventInfo = BuildLogEventInfo(LogLevel.Info, msg, source, detailTrace, other1, other2, other3);
                var logger = GetLogger();
                logger.Log(eventInfo);
            }
            catch
            { }
        }

        public static void Warn(string msg, string source, string detailTrace = null, string other1 = null, string other2 = null, string other3 = null)
        {
            try
            {
                var eventInfo = BuildLogEventInfo(LogLevel.Warn, msg, source, detailTrace, other1, other2, other3);

                var logger = GetLogger();
                logger.Log(eventInfo);
            }
            catch
            { }
        }


        public static void Error(string msg, string source, string detailTrace = null, string other1 = null, string other2 = null, string other3 = null)
        {
            try
            {
                var eventInfo = BuildLogEventInfo(LogLevel.Error, msg, source, detailTrace, other1, other2, other3);

                var logger = GetLogger();
                logger.Log(eventInfo);
            }
            catch
            { }
        }

        public static void Error(Exception ex, string source, string other1 = null, string other2 = null, string other3 = null)
        {
            try
            {
                var eventInfo = BuildLogEventInfo(LogLevel.Error, ex.Message, source, ex.StackTrace, other1, other2, other3);

                var logger = GetLogger();
                logger.Log(eventInfo);
            }
            catch
            { }
        }


    }



    public class SysLogInfo
    {
        public DateTime LogDT { get; set; }

        public int ThreadID { get; set; }

        public string Level { get; set; }

        public string Msg { get; set; }

        public string MachineName { get; set; }

        public LogProperties Properties { get; set; }

        public class LogProperties
        {
            public string Source { get; set; }

            public string DetailTrace { get; set; }

            public string UserID { get; set; }

            public string Other1 { get; set; }

            public string Other2 { get; set; }

            public string Other3 { get; set; }
        }



    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

10.其它一些用到的公共实用方法

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
//BaseUtil:
        public static DataAccess CreateDataAccess(string connName = "DefaultConnectionString")
        {
            return new DataAccess(connName, EncryptUtility.Decrypt);
        }

        public static string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static JObject DeserializeObject(string json)
        {
            return JObject.Parse(json);
        }

        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

//=====================================

    /// <summary>
    /// 类型扩展方法集合
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 转换为不为空的字符串（即：若为空，则返回为空字符串，而不是Null）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToNotNullString(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            return obj.ToString();
        }



        /// <summary>
        /// 判断列表中是否存在项
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasItem(this IEnumerable<object> list)
        {
            if (list != null && list.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 从字答串左边起取出指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Substring(0, length);
        }


        /// <summary>
        /// 从字答串右边起取出指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string str, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Substring(str.Length - length);
        }

        /// <summary>
        /// 判断DataSet指定表是否包含记录
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static bool HasRows(this DataSet ds, int tableIndex = 0)
        {
            if (ds != null && ds.Tables[tableIndex].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通用类型转换方法，EG:"".As<String>()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this object obj)
        {
            T result;
            try
            {
                Type type = typeof(T);
                if (type.IsNullableType())
                {
                    if (obj == null || obj.ToString().Length == 0)
                    {
                        result = default(T);
                    }
                    else
                    {
                        type = type.GetGenericArguments()[0];
                        result = (T)Convert.ChangeType(obj, type);
                    }
                }
                else
                {
                    if (obj == null)
                    {
                        if (type == typeof(string))
                        {
                            result = (T)Convert.ChangeType(string.Empty, type);
                        }
                        else
                        {
                            result = default(T);
                        }
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(obj, type);
                    }
                }
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return (type.IsGenericType &&
              type.GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        /// <summary>
        /// 尝试将ArrayList转换为Array，如果为空则转换为null
        /// </summary>
        /// <param name="arrList"></param>
        /// <returns></returns>
        public static object[] TryToArray(this ArrayList arrList)
        {
            return arrList != null ? arrList.ToArray() : null;
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

WebApiConfig增加上述定义的一些过滤器、处理管道,以便实现拦截处理：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new HandleExceptionFilterAttribute());//添加统一处理异常过滤器

            config.MessageHandlers.Add(new RequestAuthenticationHandler());//添加统一TOKEN身份证码

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver { IgnoreSerializableAttribute = true };
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**客户端调用上述API相关核心代码：**

1.DataService（客户端访问API通用类，通过该类公共静态方法可以进行数据库的CRUD）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    public class DataService : BaseService
    {

        #region 私有方法
        private static SqlCmdInfo BuildSqlCmdInfo(string sqlCmdText, bool isSPCmdType = false, int dbType = 0, params object[] sqlParams)
        {
            var sqlCmdInfo = new SqlCmdInfo()
            {
                SqlCmdText = sqlCmdText,
                DbType = dbType,
                IsSPCmdType = isSPCmdType
            };

            if (sqlParams != null && sqlParams.Length > 0)
            {
                sqlCmdInfo.Parameters = new ArrayList(sqlParams);
            }

            return sqlCmdInfo;
        }

        private static string GetRrequestApiUrl(string action)
        {
            string requestApiUrl = string.Format("http://{0}/api/Data/{1}", ApiHost, action);
            return requestApiUrl;
        }

        #endregion


        public static ApiResultInfo<string> Login(string uid, string pwd, string mac, string pcName)
        {
            var result = WebApiUtil.GetResultFromWebApi<string>(null, new[] { uid, pwd, mac, pcName }, GetRrequestApiUrl("Login"));
            if (result.Stauts)
            {
                SessionToken = result.Data;
            }

            return result;
        }

        public static void LogOut()
        {
            WebApiUtil.GetResultFromWebApi<string>(AddHeadersWithToken(), string.Format("{\"\":\"{0}\"}", SessionToken), GetRrequestApiUrl("LogOut"));
        }


        public static T GetValue<T>(string sqlCmdText, object[] sqlParams = null, bool isSPCmdType = false, int dbType = 0)
        {
            var sqlCmdInfo = BuildSqlCmdInfo(sqlCmdText, isSPCmdType, dbType, sqlParams);
            var result = WebApiUtil.GetResultFromWebApi<T>(AddHeadersWithToken(), sqlCmdInfo, GetRrequestApiUrl("GetValue"));
            if (result.Stauts)
            {
                return result.Data;
            }
            throw new Exception(result.ErrCode + ":" + result.ErrMsg);
        }

        public static DataSet GetDataSet(string sqlCmdText, object[] sqlParams = null, bool isSPCmdType = false, int dbType = 0)
        {
            var sqlCmdInfo = BuildSqlCmdInfo(sqlCmdText, isSPCmdType, dbType, sqlParams);
            var result = WebApiUtil.GetResultFromWebApi<DataSet>(AddHeadersWithToken(), sqlCmdInfo, GetRrequestApiUrl("GetDataSet"));
            if (result.Stauts)
            {
                return result.Data;
            }
            throw new Exception(result.ErrCode + ":" + result.ErrMsg);
        }

        public static DataTable GetDataTable(string sqlCmdText, object[] sqlParams = null, bool isSPCmdType = false, int dbType = 0)
        {
            var sqlCmdInfo = BuildSqlCmdInfo(sqlCmdText, isSPCmdType, dbType, sqlParams);
            var result = WebApiUtil.GetResultFromWebApi<DataTable>(AddHeadersWithToken(), sqlCmdInfo, GetRrequestApiUrl("GetDataTable"));
            if (result.Stauts)
            {
                return result.Data;
            }
            throw new Exception(result.ErrCode + ":" + result.ErrMsg);
        }

        public static int ExecuteCommand(string sqlCmdText, object[] sqlParams = null, bool isSPCmdType = false, int dbType = 0)
        {
            var sqlCmdInfo = BuildSqlCmdInfo(sqlCmdText, isSPCmdType, dbType, sqlParams);
            var result = WebApiUtil.GetResultFromWebApi<int>(AddHeadersWithToken(), sqlCmdInfo, GetRrequestApiUrl("ExecuteCommand"));
            if (result.Stauts)
            {
                return result.Data;
            }
            throw new Exception(result.ErrCode + ":" + result.ErrMsg);
        }


        public static bool BatchExecuteCommand(IEnumerable<SqlCmdInfo> sqlCmdInfos)
        {
            var result = WebApiUtil.GetResultFromWebApi<bool>(AddHeadersWithToken(), sqlCmdInfos, GetRrequestApiUrl("BatchExecuteCommand"));
            if (result.Stauts)
            {
                return result.Data;
            }
            throw new Exception(result.ErrCode + ":" + result.ErrMsg);
        }


        public static void ExecuteCommandAsync(string sqlCmdText, object[] sqlParams = null, bool isSPCmdType = false, int dbType = 0, Action<ApiResultInfo<object>> callBackAction = null)
        {
            var sqlCmdInfo = BuildSqlCmdInfo(sqlCmdText, isSPCmdType, dbType, sqlParams);

            Func<SqlCmdInfo, ApiResultInfo<object>> execCmdFunc = new Func<SqlCmdInfo, ApiResultInfo<object>>((sqlCmdObj) =>
            {
                var result = WebApiUtil.GetResultFromWebApi<object>(AddHeadersWithToken(), sqlCmdObj, GetRrequestApiUrl("ExecuteCommandAsync"));
                return result;
            });

            execCmdFunc.BeginInvoke(sqlCmdInfo, new AsyncCallback((ar) =>
            {
                ApiResultInfo<object> apiResult = null;
                try
                {
                    var func = ar.AsyncState as Func<SqlCmdInfo, ApiResultInfo<object>>;
                    apiResult = func.EndInvoke(ar);
                }
                catch (Exception ex)
                {
                    apiResult = new ApiResultInfo<object>(false, ex, "ExecuteCommandAsyncErr", ex.Message);
                }
                if (callBackAction != null)
                {
                    callBackAction(apiResult);
                }
            }), execCmdFunc);
        }


        public static void SaveLogAsync(string logType, string msg, string source, string detailTrace = null, string other1 = null, string other2 = null, string other3 = null)
        {
            string[] logInfo = new[] { logType, msg, source, detailTrace, other1, other2, other3 };

            Task.Factory.StartNew((o) =>
            {
                try
                {
                    string[] logInfoObj = o as string[];
                    var result = WebApiUtil.HttpRequestToString(AddHeadersWithToken(), logInfoObj, GetRrequestApiUrl("SaveLog"));
                }
                catch
                { }
            }, logInfo);
        }


    }


    public abstract class BaseService
    {
        public static string SessionToken = null;
        public static string ApiHost = null;

        protected static Dictionary<string, string> AddHeadersWithToken()
        {
            return new Dictionary<string, string> { 
                {"AccessToken",SessionToken}
            };
        }


    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

2.WebApiUtil（WEB API请求工具类）

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    /// <summary>
    /// WebApi实用工具类
    /// Author:Zuowenjun
    /// Date:2017/11/3
    /// </summary>
    public static class WebApiUtil
    {
        private const string rsaPublicKey = "公钥字符串";

        static WebApiUtil()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
        }


        /// <summary>
        /// 获取API结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestHeaders"></param>
        /// <param name="requestMsg"></param>
        /// <param name="apiUrl"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static ApiResultInfo<T> GetResultFromWebApi<T>(Dictionary<string, string> requestHeaders, object requestMsg, string apiUrl, string requestMethod = "POST")
        {
            string retString = HttpRequestToString(requestHeaders, requestMsg, apiUrl, requestMethod);
            return JsonConvert.DeserializeObject<ApiResultInfo<T>>(retString);
        }


        /// <summary>
        /// 发送Http请求，模拟访问指定的Url，返回响应内容转换成JSON对象
        /// </summary>
        /// <param name="requestHeaders"></param>
        /// <param name="requestMsg"></param>
        /// <param name="apiUrl"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static JObject HttpRequestToJson(Dictionary<string, string> requestHeaders, object requestMsg, string apiUrl, string requestMethod = "POST")
        {
            string retString = HttpRequestToString(requestHeaders, requestMsg, apiUrl, requestMethod);
            return JObject.Parse(retString);
        }



        /// <summary>
        /// 发送Http请求，模拟访问指定的Url，返回响应内容文本
        /// </summary>
        /// <param name="requestHeaders">请求头</param>
        /// <param name="requestData">请求体（若为GetMethod时，则该值应为空）</param>
        /// <param name="apiUrl">要访问的Url</param>
        /// <param name="requestMethod">请求方式</param>
        /// <param name="isEncryptBody">是否对请求体内容进行公钥加密</param>
        /// <returns>响应内容(响应头、响应体)</returns>
        public static string HttpRequestToString(Dictionary<string, string> requestHeaders, object requestMsg, string apiUrl, string requestMethod = "POST", bool isEncryptBody = true)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = requestMethod;
            request.KeepAlive = false;
            request.Proxy = null;
            request.ServicePoint.UseNagleAlgorithm = false;
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "application/json";

            if (requestHeaders != null)
            {
                foreach (var item in requestHeaders)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            request.Headers.Set("Pragma", "no-cache");

            if (requestMsg != null)
            {
                string dataStr = JsonConvert.SerializeObject(requestMsg);

                if (isEncryptBody)
                {
                    request.Headers.Add("Encryption", "1");
                    dataStr = RSAEncrypt(rsaPublicKey, dataStr);//加密请求内容
                }

                byte[] data = Encoding.UTF8.GetBytes(dataStr);
                request.ContentLength = data.Length;

                using (Stream myRequestStream = request.GetRequestStream())
                {
                    myRequestStream.Write(data, 0, data.Length);
                    myRequestStream.Close();
                }
            }

            string retString = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                retString = GetResponseBody(response);
            }
            request = null;

            return retString;
        }



        private static string GetResponseBody(HttpWebResponse response)
        {
            string responseBody = string.Empty;
            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                using (DeflateStream stream = new DeflateStream(
                    response.GetResponseStream(), CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
            }
            return responseBody;
        }


        public static string RSAEncrypt(string publicKey, string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(publicKey);//载入公钥
                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    { //分段加密
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                }
            }
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

3.ApiResultInfo（API返回结果类）、SqlCmdInfo（SQL命令信息类） 与服务端的同名类基本相同，只是少了一些方法，因为这些方法在客户端用不到所有无需再定义了：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    [Serializable]
    public class SqlCmdInfo
    {
        public string SqlCmdText { get; set; }

        public ArrayList Parameters { get; set; }

        public bool IsSPCmdType { get; set; }

        public int DbType { get; set; }
    }


    [Serializable]
    public class ApiResultInfo<T>
    {
        public bool Stauts { get; set; }

        public T Data { get; set; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public Dictionary<string, object> ExtendedData { get; set; }


        public ApiResultInfo()
        {
            this.ExtendedData = new Dictionary<string, object>();
        }


        public ApiResultInfo(bool status, T data, string errCode = null, string errMsg = null, Dictionary<string, object> extData = null)
        {
            this.Stauts = status;
            this.Data = data;
            this.ErrCode = errCode;
            this.ErrMsg = errMsg;
            this.ExtendedData = extData;
            if (this.ExtendedData == null)
            {
                this.ExtendedData = new Dictionary<string, object>();
            }
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

客户端使用方法如下：

```
//直接使用DataService的相关公共静态方法即可，就像本地直接使用ADO.NET操作数据库一样；例如：

DataTable dt = DataService.GetDataTable("SQL语句", new object[] { 参数});

DataService.ExecuteCommand("SQL语句", new Object[] { 参数 });
```

 以上就是基于ASP.NET WEB API实现分布式数据访问中间层，这个分布式数据访问中间层虽简单，但我包含了如下几个核心内容：

**1.身份验证：未经登录授权是无法访问API，当然上述的验证非常简单，这个可以根据实际情况进行扩展，比如：OA2.0验证，摘要验证等**

**2.请求内容加密：一些重要的请求内容必需要加密，而且防止被破解，这里使用公钥加密，私钥解密，从客户端是无法截获加密的请求内容**

**3.压缩响应报文：如果返回的内容过大，则需要进行压缩，以提高响应速度**

**4.防SQL注入：通过自定义过滤器，对传入的SQL语句利用正则进行分析，若包含非法关键字则直接不予执行且报错，执行SQL语句时也是全面采用ADO.NET的参数化，确保整个执行安全可靠。** 

**5.全局异常捕获：对于每个可能发生的异常一个都不放过，全部记录到日志中**

**6.MONGODB日志记录：利用NLOG.MONGO组件实现日志记录，不影响正常的DB**

好了本文总结就到这里了，之前由于工作忙，项目较多，没有时间写文章，今天刚好利用晚上加班的稍空闲时期对近期我独立写的项目进行总结与分享，希望能帮助大家，谢谢！



分类: [ASP.NET](https://www.cnblogs.com/zuowj/category/548289.html)

标签: [WebApi](https://www.cnblogs.com/zuowj/tag/WebApi/), [ASP.NET](https://www.cnblogs.com/zuowj/tag/ASP.NET/)