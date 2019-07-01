# webapi  自宿主 post  多个参数

​    2019年01月03日 17:21:59           [思则出](https://me.csdn.net/yinyt370983)           阅读数 435                   

​                   https://blog.csdn.net/yinyt370983/article/details/85705687 

​      项目需要  java发起请求 调用c#客户端的一个程序，而且要直接通过前台ajax请求  ，不能通过java后台访问c#的接口服务。这样就设计到跨域问题，如果java后台访问就不涉及到跨域。查阅了很多资料，发现IE对跨域基本上限制要求比较小。在脚本中设置一下跨域cors支持就可以。webapi通过后台设置跨域支持。还有一个比较头疼的问题就是传参的问题。单一的字符串，对象，或者多个参数  多个字符成员，多个对象也没问题。关键是我这里面对象嵌套对象。在解析的时候遇到很大的问题。最终还是解决了。如果单纯的百度  肯定是永远找不到解决的方式。最终还是通过了解了代码结构找到了解决的方法。直接上方法吧。 

```
             $.ajax({
                    type: "POST",
                    url: "服务地址",
                    data: { 对象1: json对象1, 对象2: json对象2 },
                    success: function (data, status) {
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    },
                    complete: function () {
                    }
                });
--------------------- 
作者：思则出 
来源：CSDN 
原文：https://blog.csdn.net/yinyt370983/article/details/85705687 
版权声明：本文为博主原创文章，转载请附上博文链接！
```

​        这种方式支持post 一次传递多个参数，如果用JsonStringfy 只能传递一个数据，这样的弊端是，参数不明确。而且为了后台转换方便，你必须单独New一个对象，这样无疑既麻烦 又不明确。

​        这种方式后台需要方法接收参数类型是 JObject类型。然后需要先序列化，再反序列化。注意：如果上面的data 你传递的是json对象，那么需要你再一次序列化，再一次反序列化，直接拿到你想要的对象数据。

```cs
  var jsonobject = Newtonsoft.Json.JsonConvert.SerializeObject(参数1);



  var jsonpara = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonobject);
 
  var jsonborrow = Newtonsoft.Json.JsonConvert.SerializeObject(jsonpara.属性1); 

  对象 bw = Newtonsoft.Json.JsonConvert.DeserializeObject<对象>(jsonborrow); 
  varjsonsignAuthority=Newtonsoft.Json.JsonConvert.SerializeObject(jsonpara.属性2); 
  对象2 signAuthority = Newtonsoft.Json.JsonConvert.DeserializeObject<对象2>(jsonsignAuthority);
```

​      webapi自宿主的方式也挺简单

  

```
 var config = new HttpSelfHostConfiguration("http://localhost:" + System.Configuration.ConfigurationSettings.AppSettings["apiport"].ToString());



            config.Routes.MapHttpRoute("default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional }); 

            var server = new HttpSelfHostServer(config);
 
            server.OpenAsync().Wait();
```