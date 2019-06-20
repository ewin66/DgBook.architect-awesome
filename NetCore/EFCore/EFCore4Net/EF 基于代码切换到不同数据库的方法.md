# EF 基于代码切换到不同数据库的方法

EF 基于代码切换到不同数据库的方法

 2018年08月28日 17:55:58           [走错路的程序员](https://me.csdn.net/phker)           阅读数 992          

​     https://blog.csdn.net/phker/article/details/82152125                

最近写项目一直在用EF ,  跨库能力极强.  很喜欢. 

但是不能动态切换数据库, 最近搞了一个配置中心的功能, 把连接字符串放到配置中心中单独配置管理. 
 但是EF默认是放到Web.config中的.     就算自己拼装出了连接字符串也会因为没有 providerName=”System.Data.SqlClient”  而失败.  
 经过再三研究, 今天研究了一下午终于搞定了.

关键是要重载 DbContext  的一个构造函数

```
  public partial class DatacenterContext: DbContext
    {
        static  DatacenterContext()
        {
            //DbConfiguration.SetConfiguration()
            Database.SetInitializer< DatacenterContext>(null);
        }
        public DatacenterContext(DbConnection conn)
            : base(conn, true)
        {
           //这个方法是用来实现切换不同库的主方法.本身EF就提供的. 
        }
    }12345678910111213
```

数据库连接工厂, 的作用是在EF初始化之前先实例化一个新的数据库连接.  然后用它实例化 EF 的 DbContext

```
 public static class DBContextFactory
    {
            static string reportDBType = MK.Base.CenterConfig.GetConfig("数据库类型");
            static string dbip = MK.Base.CenterConfig.GetConfig("数据库IP地址");
            static string dbport = MK.Base.CenterConfig.GetConfig("数据库端口");
            static string dbuser = MK.Base.CenterConfig.GetConfig("数据库用户名");
            static string dbpassword = MK.Base.CenterConfig.GetConfig("数据库密码");
            static string dbname = MK.Base.CenterConfig.GetConfig("数据库名或(Orcale)服务名"); 
        /// <summary>
        /// 根据配置取得数据库连接上下文
        /// </summary>
        /// <returns></returns>
        public static DbContext GetReportDataDbContext()
        { 
            if (reportDBType == "DataCenter_MySql")
            {

                MySqlConnectionStringBuilder sqlbulider = new MySqlConnectionStringBuilder();
                sqlbulider.Server = dbip;
                sqlbulider.UserID = dbuser;
                sqlbulider.Password = dbpassword;
                sqlbulider.Database = dbname;
                sqlbulider.Port = uint.Parse(dbport);
                sqlbulider.AllowZeroDateTime = true;
                sqlbulider.ConvertZeroDateTime = true;
                sqlbulider.IntegratedSecurity = true;

                MySqlConnection conn = new MySqlConnection(sqlbulider.ToString());
                return new DatacenterContext( conn );
            }
            else if (reportDBType == "DataCenter_SqlServer")
            {
                connectstring = string.Format("Data Source={0},{4};Initial Catalog={3};Persist Security Info=True;User ID={1};Password={2};", dbip, dbuser, dbpassword, dbname, dbport);
                SqlConnectionStringBuilder sqlbulider = new SqlConnectionStringBuilder();
                sqlbulider.DataSource = dbip+","+ uint.Parse(dbport);
                sqlbulider.UserID = dbuser;
                sqlbulider.Password = dbpassword;
                sqlbulider.UserID = dbname;
                sqlbulider.InitialCatalog = dbname;
                sqlbulider.PersistSecurityInfo = true; 
                sqlbulider.IntegratedSecurity = true;

                SqlConnection conn = new SqlConnection(sqlbulider.ToString()); 
                return new DatacenterContext(conn);
            }  
        }


    }
```