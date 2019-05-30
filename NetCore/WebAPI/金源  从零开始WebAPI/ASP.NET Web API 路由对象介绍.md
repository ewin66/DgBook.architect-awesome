ASP.NET Web API 路由对象介绍
前言

- **ASP.NET Web API 开篇介绍示例**

- **ASP.NET Web API 路由对象介绍**

- **ASP.NET Web API 管道模型**

  **ASP.NET Web API [selfhos](https://blog.csdn.net/JinYuan0829/article/details/38395451)t宿主环境中管道、路由**

  **ASP.NET Web API [[webhost](https://blog.csdn.net/JinYuan0829/article/details/38412517)宿主环境中管道、路由**]()

 





在ASP.NET、ASP.NET MVC和ASP.NET Web API中这些框架中都会发现有路由的身影，它们的原理都差不多，只不过在不同的环境下作了一些微小的修改，这也是根据每个框架的特性来制定的，今天我们就来看一看路由的结构，虽然我在MVC系列里写过路由的篇幅不过在这里是Web API 路由对象介绍。


路由结构

图1

路由系统中最重要的部分也就是路由对象了，那我们首先就来看一下【路由对象】的定义，不管是在ASP.NET、ASP.NET MVC、还是ASP.NET Web API的路由系统中路由都要有个名称，其实这个名称并不是路由对象中的而是在注册路由信息的时候，添加到路由对象集合的时候需要的名称，这里也只是当作路由的一部分，这个大家知道就好了。

在生成路由对象的时候我们要给路由赋值URL模板，这也是共同的，也是必须的，至于约束URL模板的条件是可以根据自己情况来定义的。在生成的同时框架会给路由对象赋值上【路由请求处理程序】用以作为衔接路由系统和框架的主体功能部分。


注册路由到系统框架中

图2

在路由定义好之后，我们便会把它注册到系统框架中。


路由对象的URL匹配

图3

在路由对象注册到系统框架中之后，这个时候如果有外部的请求的到达，这个时候路由系统会让路由对象集合中每个路由对象对这个请求进行匹配，就如图4一样。

图4

这个时候就是路由对象所要能做出的行为就是URL的匹配，根据什么来匹配？是根据在路由对象实例化的时候定义好的URL模板和条件，拿请求信息的URL和自身定义的URL模板进行匹配，假使没有匹配成功则会返回Null，这个时候框架则会让下一个路由对象来进行匹配直到有匹配的成功为止，如果这个时候匹配成功了路由则会生成一个【路由数据对象】。

 

路由数据对象也很重要，因为后续的框架功能部分都是使用它的，它也是整个路由系统的结晶，我们看下图5

图5



#####	路由数据对象*

**路由数据对象**会保持一个生成它的路由对象的引用，然后是Values的是保存着路由对象在经过URL匹配后的值，分别表示着URL片段的名字和对应的URL真实值，而DataTokens则是在路由对象定义生成的时候直接带过来的值，当然了路由请求处理程序也是由执行生成的路由对象带来的。

 

在ASP.NET、ASP.NET MVC、ASP.NET Web API这些框架中路由系统都是遵循着上面的所述的这样一个过程，只不过在不同的框架环境下使用的类型不同，做的处理也不太一样，但是整体的流程是一致的，下面附上图6说明了之间的类型的差异性，还有更多的细节就不一一展示了。

图6

还有在Web API(WebHost)环境下路由显示的是这样实质的本质其实又是ASP.NET的路由系统在支持的，这个会在后面的Web API系列篇幅中讲解。

下面简单的演示一下在各种框架环境下的路由对象注册，

ASP.NET:

RouteTable.Routes.MapPageRoute(
                "ASP.NETRoute",
                "ProductInfo/{action}/{id}",
                "~/ProductInfo.aspx",
                true,
                new RouteValueDictionary { { "id", RouteParameter.Optional }, { "action", "show" } }
                );

 

ASP.NET MVC：

            RouteTable.Routes.MapRoute(
                "ASP.NETMVCRoute",
                "ProductInfo/{action}/{id}",
                new { controller="Product",action="show",id=RouteParameter.Optional}
                );

ASP.NET Web API（WEBHOST）：

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
               "WebAPIRoute",
               "api/{controller}/{id}", new { id = RouteParameter.Optional }
               );

ASP.NET Web API（SELFHOST）：

HttpSelfHostConfiguration configuration = 
                new HttpSelfHostConfiguration("http://loacalhost/selfhost");
            using (HttpSelfHostServer selfHostServer = new HttpSelfHostServer(configuration))
            {
                selfHostServer.Configuration.Routes.MapHttpRoute(
                    "DefaultApi", "api/{controller}/{id}", new { id=RouteParameter.Optional});
               
                selfHostServer.OpenAsync();
                Console.Read();
            }

 




ASP.NET Web API 路由系列对象

从上图的图表中就可以看出，ASP.NET Web API框架在不同的宿主环境下路由系统中所对应的对象类型是不同的，这里就先给大家介绍在SelfHost环境下的路由系统中的路由对象吧。


SelfHost宿主环境

 

Web API路由对象(System.Web.Http.Routing)

HttpRoute

    // 摘要:
    //     表示自承载（即在 ASP.NET 之外承载）的路由类。
    public class HttpRoute : IHttpRoute
    {
        public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler);
    
        public IDictionary<string, object> Constraints { get; }
        public IDictionary<string, object> DataTokens { get; }
        public IDictionary<string, object> Defaults { get; }
        public HttpMessageHandler Handler { get; }
        public string RouteTemplate { get; }
        public virtual IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request);
        public virtual IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values);
        protected virtual bool ProcessConstraint(HttpRequestMessage request, object constraint, string parameterName, HttpRouteValueDictionary values, HttpRouteDirection routeDirection);
    }

可以从上面的定义中看到HttpRoute对象就是代表着在Web API框架中的路由对象了，在HttpRoute类型定义的构造函数中的参数分别表示着路由模板、路由模板对应的默认值、路由匹配条件、注册的路由附带的值以及最后的Http请求处理程序，这几个参数值也分别对应着HttpRoute类型中的几个属性，这个自行看一下就明白了。

 

Web API路由对象集合(System.Web.Http)

HttpRouteCollection

HttpRouteCollectionExtensions

 

我们先来看一下HttpRouteCollection类型的扩展类型HttpRouteCollectionExtensions吧

    public static class HttpRouteCollectionExtensions
    {
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate);
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults);
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints);
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler);
    }

这里大家可以对比上面的路由注册时的代码，就可以知道我们在路由集合 添加/注册 路由的时候是由HttpRouteCollectionExtensions类型的扩展方法来进行操作的，这个时候我们再看一下方法参数最多的那个MapHttpRoute()方法的实现：

public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
        {
            if (routes == null)
            {
                throw System.Web.Http.Error.ArgumentNull("routes");
            }
            HttpRouteValueDictionary dictionary = new HttpRouteValueDictionary(defaults);
            HttpRouteValueDictionary dictionary2 = new HttpRouteValueDictionary(constraints);
            IDictionary<string, object> dataTokens = null;
            HttpMessageHandler handler2 = handler;
            IHttpRoute route = routes.CreateRoute(routeTemplate, dictionary, dictionary2, dataTokens, handler2);
            routes.Add(name, route);
            return route;
        }

这里大家就可以看到了，HttpRoute对象的创建操作和添加操作是在这扩展方法里执行的，现在我们就可以去看一下HttpRouteCollection类型的定义了，看一下如何创建的IHttpRoute对象：

    public class HttpRouteCollection : ICollection<IHttpRoute>, IEnumerable<IHttpRoute>, IEnumerable, IDisposable
    {
        public HttpRouteCollection();
        public HttpRouteCollection(string virtualPathRoot);
    
        public virtual int Count { get; }
        public virtual bool IsReadOnly { get; }
        public virtual string VirtualPathRoot { get; }
    
        public virtual void Add(string name, IHttpRoute route);
        public IHttpRoute CreateRoute(string routeTemplate, object defaults, object constraints);
        public IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens);
        public virtual IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler);
        public virtual IHttpRouteData GetRouteData(HttpRequestMessage request);
    }

这里只是其中的一部分，下面我们就来看一下具体的实现，其实就是实例化一个HttpRoute路由对象根据用户配置的参数信息：

public virtual IHttpRoute CreateRoute(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
        {
            HttpRouteValueDictionary dictionary = new HttpRouteValueDictionary(defaults);
            HttpRouteValueDictionary dictionary2 = new HttpRouteValueDictionary(constraints);
            return new HttpRoute(routeTemplate, dictionary, dictionary2, new HttpRouteValueDictionary(dataTokens), handler);
        }

这是路由对象集合类型的第一个作用就是添加/注册 路由信息，那么第二个呢？就是根据请求信息来匹配路由对象，上面也说过了，其实真正根据请求来匹配的并不是路由对象集合类型（HttpRouteCollection），而是在其中的每个路由，我们看一下HttpRouteCollection的障眼法：

public virtual IHttpRouteData GetRouteData(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw System.Web.Http.Error.ArgumentNull("request");
            }
            foreach (IHttpRoute route in this._collection)
            {
                IHttpRouteData routeData = route.GetRouteData(this._virtualPathRoot, request);
                if (routeData != null)
                {
                    return routeData;
                }
            }
            return null;
        }

从这里可以看出在路由匹配完成后会返回一个实现IHttpRouteDatarouteData接口的对象，也就是上面所说的路由数据对象。

 

Web API路由数据对象(System.Web.Http.Routing)

HttpRouteData

    public class HttpRouteData : IHttpRouteData
    {
        public HttpRouteData(IHttpRoute route);
        public HttpRouteData(IHttpRoute route, HttpRouteValueDictionary values);
    
        public IHttpRoute Route { get; }
        public IDictionary<string, object> Values { get; }
    }

 



其实这里都不用讲了，上面都讲过了，HttpRouteData对象包含着生成它的路由对象(HttpRoute)的引用，并且Values值就是经过匹配过后的路由模板值，key键对应着Url模板的片段值，value对应着的是片段对应的真实值。

 

SelfHost环境下的路由就说到这里,大家看一下如下的示意图，简单的表示了在SelfHost环境下路由的一个处理过程，具体的细节会在后面的篇幅讲解。

图7


WebHost宿主环境

 

Web API路由对象(System.Web.Http.WebHost.Routing)

HostedHttpRoute

    internal class HostedHttpRoute : IHttpRoute
    {
        // Methods
        public HostedHttpRoute(string uriTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler);
        public IHttpRouteData GetRouteData(string rootVirtualPath, HttpRequestMessage request);
        public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values);
    
        // Properties
        public IDictionary<string, object> Constraints { get; }
        public IDictionary<string, object> DataTokens { get; }
        public IDictionary<string, object> Defaults { get; }
        public HttpMessageHandler Handler { get; private set; }
        internal Route OriginalRoute { get; private set; }
        public string RouteTemplate { get; }
}

从上面的代码定义中可以看到HostedHttpRoute是程序集内部类型，并且是直接继承自IHttpRoute接口，跟SelfHost环境中的HttpRoute对象是一点关系都没有。

从它定义的内部结构来看它跟HttpRoute对象的结构相似，还是那些属性那些个对象，唯一不同的就是多了个OriginalRoute的只读属性（对于外部来说），这个属性也就是封装的HttpWebRoute对象，看下封装时的实现

public HostedHttpRoute(string uriTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler)
    {
        RouteValueDictionary dictionary = (defaults != null) ? new RouteValueDictionary(defaults) : null;
        RouteValueDictionary dictionary2 = (constraints != null) ? new RouteValueDictionary(constraints) : null;
        RouteValueDictionary dictionary3 = (dataTokens != null) ? new RouteValueDictionary(dataTokens) : null;
        this.OriginalRoute = new HttpWebRoute(uriTemplate, dictionary, dictionary2, dictionary3, HttpControllerRouteHandler.Instance, this);
        this.Handler = handler;
    }

在HostedHttpRoute对象构造函数中可以清楚的看到OriginalRoute属性是赋值的HttpWebRoute对象的实例，我们现在就来看一下HttpWebRoute对象的定义：

    internal class HttpWebRoute : Route
    {
        // Fields
        internal const string HttpRouteKey = "httproute";
    
        // Methods
        public HttpWebRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler, IHttpRoute httpRoute);
        private static HttpRouteDirection ConvertRouteDirection(RouteDirection routeDirection);
        private static RouteValueDictionary GetRouteDictionaryWithoutHttpRouteKey(IDictionary<string, object> routeValues);
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values);
        protected override bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);
    
        // Properties
        public IHttpRoute HttpRoute { get; private set; }
}

 

从这里可以看到HttpWebRoute对象继承自ASP.NET中的Route对象，现在就可以理解为HostedHttpRoute对象持有对ASP.NET中Route对象的引用，而在HostedHttpRoute的构造函数实现中，对OriginalRoute属性是赋值实例化的时候，在最后传入了一个HttpControllerRouteHandler类型的路由处理程序，实则是给ASP.NET中的Route对象的路由处理程序（Routehandler属性）进行的赋值。这里路由的具体的操作后续篇幅中会有讲到一个全面的过程。

 

Web API路由对象集合(System.Web.Http.WebHost.Routing

HostedHttpRouteCollection

 

internal class HostedHttpRouteCollection : HttpRouteCollection
        {
            // Fields
            private readonly RouteCollection _routeCollection;

            // Methods
            public HostedHttpRouteCollection(RouteCollection routeCollection);
            public override void Add(string name, IHttpRoute route);
            public override void Clear();
            public override bool Contains(IHttpRoute item);
            public override bool ContainsKey(string name);
            public override void CopyTo(IHttpRoute[] array, int arrayIndex);
            public override void CopyTo(KeyValuePair<string, IHttpRoute>[] array, int arrayIndex);
            public override IHttpRoute CreateRoute(string uriTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IDictionary<string, object> dataTokens, HttpMessageHandler handler);
            public override IEnumerator<IHttpRoute> GetEnumerator();
            public override IHttpRouteData GetRouteData(HttpRequestMessage request);
            public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, string name, IDictionary<string, object> values);
            public override void Insert(int index, string name, IHttpRoute value);
            private static NotSupportedException NotSupportedByHostedRouteCollection();
            private static NotSupportedException NotSupportedByRouteCollection();
            public override bool Remove(string name);
            public override bool TryGetValue(string name, out IHttpRoute route);
    
            // Properties
            public override int Count { get; }
            public override IHttpRoute this[string name] { get; }
            public override IHttpRoute this[int index] { get; }
            public override string VirtualPathRoot { get; }
        }

看到这里的代码定义，HostedHttpRouteCollection对象同样也是程序集内部类型，继承自ASP.NET中的RouteCollection对象，这里要说是CreateRoute()方法和GetRouteData()方法返回的分别是HostedHttpRoute对象和HostedHttpRouteData对象，其实在GetRouteData()方法中起初生成的就是Routedata对象，只不过在返回的时候经过HostedHttpRouteData对象封装了一下。

 

Web API路由数据对象(System.Web.Http.WebHost.Routing)

HostedHttpRouteData

这里我们看一下HostedHttpRouteData类型的定义：

        internal class HostedHttpRouteData : IHttpRouteData
        {
            // Methods
            public HostedHttpRouteData(RouteData routeData);
    
            // Properties
            internal RouteData OriginalRouteData { get; private set; }
            public IHttpRoute Route { get; private set; }
            public IDictionary<string, object> Values { get; }
        }

从构造函数的定义就可以看出来是HostedHttpRouteData是封装的RouteData对象，这些路由流程细节后面篇幅中会有讲解。

 

最后我们看一下在WebHost宿主环境下的路由示意图。

图8



作者：金源

出处：http://blog.csdn.net/jinyuan0829

本文版权归作者和CSDN共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面
--------------------- 
作者：JinYuan0829 
来源：CSDN 
原文：https://blog.csdn.net/JinYuan0829/article/details/38364761 
版权声明：本文为博主原创文章，转载请附上博文链接！