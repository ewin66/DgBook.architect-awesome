



#  			[ASP.NET MVC WEB API必知必会知识点总结](https://www.cnblogs.com/zuowj/p/4769315.html) 		



 一、理解WEB API:提供基于RESTful架构的WEB服务，通过HTTP请求方法（GET， PUT， POST， DELETE）映射到服务器端相应的ACTION方法（CRUD）。

RESTful架构：

（1）每一个URI代表一种资源；
（2）客户端和服务器之间，传递这种资源的某种表现层；
（3）客户端通过四个HTTP动词，对服务器端资源进行操作，实现"表现层状态转化"。


HTTP 的四个主要方法 （GET， PUT， POST， DELETE） 按照下列方式映射为 CURD 操作：

GET 用于获取 URI 资源的进行展示， GET 操作不应对服务端有任何影响； 

PUT 用于更新 URI 上的一个资源， 如果服务端允许， PUT 也可以用于新建一个资源； 
POST 用于新建 资源， 服务端在指定的 URI 上创建一个新的对象， 将新资源的地址作为响应消息的一部分返回； 
DELETE 用于删除指定的 URI 资源。 

二、WEB API特点：

1.CONTROL类继承自ApiController抽象类；

2.注册路由时一般无需指定ACTION节点，ACTION方法名称一般都包含HTTP请求方法名名称，路由系统通过HTTP请求方法自动寻找与之相应的ACTION方法并执行；

3.ACTION方法返回值一般为：JSON、XML或一般值对象

三、实现发送GET， PUT， POST， DELETE HTTP请求方法

1.通过JQUERY.AJAX方法指定TYPE类型来实现GET， PUT， POST， DELETE HTTP请求方法； 

2.直接访问URL或将表单的METHOD方法设为GET，则可实现GET  HTTP请求方法；

3.将表单的METHOD方法设为POST，则可实现POST  HTTP请求方法；

4.PUT、DELETE除第一种方法外，只能通过先在服务端重写HTTP请求方法（自定义HttpMessageHandler来实现），然后再在客户端请求报文头指定“X-HTTP-Method-Override

”值为PUT或DELETE来实现；具体实现方法详见：[如果调用ASP.NET Web API不能发送PUT/DELETE请求怎么办？ ](http://www.cnblogs.com/artech/p/x-http-method-override.html)

5.在注册WEB API路由规则时指定ACTION节点；

 四、WEB API请求与服务端处理实现方法：

1.GET ALL方法:

客户端：

```
`$(``"#Button1"``).click(``function` `() {``               ``$.getJSON(``"@Url.Content("``~/api/values``")"``, ``function` `(data) {``                   ``var` `rs = ``""``;``                   ``$.each(data, ``function` `() {``                       ``rs += ``this` `+ ``","``;``                   ``})``                   ``alert(data);``                   ``showResult(rs);``               ``})``   ``});`
```

服务器端：

```
`// GET api/values``public IEnumerable<string> Get()``{``    ``return` `new` `string[] { ``"value1"``, ``"value2"` `};``}`
```

2.GET ONE方法：

客户端：

```
`$(``"#Button2"``).click(``function` `() {``    ``$.getJSON(``"@Url.Content("``~/api/values/5``")"``, ``function` `(data) {``        ``alert(data);``        ``showResult(data);``    ``})``});`
```

服务器端：

```
`// GET api/values/5``public string Get(int id)``{``    ``return` `"value is "` `+ id.ToString();``}`
```

3.POST CREATE方法：（注意以下客户端中的第几种方法就对应服务器端的第几种方法）

客户端：

```
`//第一种：``$(``"#Button1"``).click(``function` `() {``                ``$.post(``"@Url.Content("``~/api/values``")"``, {name:``'zwj'``,age:29},``function` `(data) {``                    ``alert(data);``                    ``showResult(data);``                ``})``            ``});` `//第二种：`` ``$(``"#Button3"``).click(``function` `() {``                ``$.ajax(``"@Url.Content("``~/api/values/1``")"``, {``                    ``type:``'post'``,``                    ``data:JSON.stringify({ name: ``'zwj'``, age: 29 }),``                    ``contentType: ``'application/json'``,``                    ``//dataType: 'json',``                    ``success: ``function` `(result, status, xhr) {``                        ``alert(result);``                        ``showResult(result);``                    ``}``                ``})``            ``});`
```

服务器端：

```
`//第一种方法：        ``public` `string` `Post()``        ``{``            ``string` `s = ``""``;``            ``HttpContextBase context = (HttpContextBase)Request.Properties[``"MS_HttpContext"``];``//获取传统context     ``            ``HttpRequestBase request = context.Request;``//定义传统request对象` `            ``for` `(``int` `i = 0; i < request.Form.Keys.Count; i++)``            ``{``                ``s += ``string``.Format(``"{0}={1}<br/>"``, request.Form.Keys[i], request.Form[i]);``            ``}``                ``return` `"Post values:"` `+ s;``        ``}` `//第二种方法：``        ``public` `string` `Post([FromBody]Person p)``        ``{``            ``return` `string``.Format(``"Put values:name:{0}，age:{1}"` `+ p.Name,p.Age);``        ``}`
```

4.PUT UPDATE方法：

　客户端方法与POST方法相同，只是TYPE指定为：PUT；

　服务器端与POST方法相同；

5.DELETE 方法：

　客户端方法与GET方法相同，只是TYPE指定为：DELETE；

　服务器端与GET方法相同；

也参见这篇文章：[ASP.NET MVC学习系列(二)-WebAPI请求](http://www.cnblogs.com/babycool/p/3922738.html) 





标签: [MVC](https://www.cnblogs.com/zuowj/tag/MVC/), [WebApi](https://www.cnblogs.com/zuowj/tag/WebApi/)