



https://blog.csdn.net/weixin_33749242/article/details/88168222



一、事件

1.编写一个通用的事件监听函数

     1 function bindEvent(elem, type, selector, fn) {
     2     if(fn == null) {
     3         fn = selector
     4         selector = null
     5     }
     6     elem.addEventListener(type,function(e) {
     7         var target
     8         if(selector) {
     9             target = e.target
    10             if(target.matches(selector)) {
    11                 fn.call(target, e)
    12             }
    13         } else {
    14             fn(e)
    15         }
    16     })
    17 }

2.事件冒泡流程

1）DOM树形结构

2）事件冒泡

3）阻止冒泡

4）冒泡的应用

3.对一个无限下拉加载图片的页面，如何给每一个图片绑定事件？

使用代理

二、ajax

1.手动编写一个ajax，不依赖第三方库

（无论什么复杂的ajax的封装，都是依照这个原理形式做的）

    1 var xhr = new XMLHttpRequest()
    2 xhr.open("GET","/api",false)  
    3 xhr.onreadystatechange = function(){
    4     if(xhr.readyState == 4 && xhr.status == 200) {
    5         alert(xhr.responseText)
    6     }
    7 }
    8 xhr.send(null)

<pre>readyState == 0　　（未初始化），还没调用send()方法
　　　　　　　　　1　　（载入），已调用send()方法，正在发送
　　　　　　　　　2　　（载入完成），send()方法执行完成，已经接受到全部形影内容
　　　　　　　　　3　　（交互），正在解析相应内容
　　　　　　　　　4　　（完成），响应内容解析完成，可以在客户端调用</pre>

status == 2xx - 表示成功处理请求，如200

3xx - 需要重定向，浏览器直接跳转

4xx - 客户端请求错误，如404

5xx - 服务器端错误，如504（服务端连接数据库超时）

2.跨域

有三个标签允许跨域加载资源：<img src="xxx" > <link href="xxx" /> <script src="xxx"></script>

解决跨域方法：

1）JSONP实现原理

服务器可以根据请求动态生成一个不存在的文件返回

window.callback = function(data) {

//这是我们跨域得到信息

console.log(data)

}

2）服务器端设置http header

另外一个解决跨域的解决方法需要服务器来完成

    1 //不同的后盾语言的写法可能不一样
    2 
    3 //第二个参数填写允许跨域的域名城，不建议直接写*
    4 response.setHeader("Access-Control-Allow-Origin", "http://a.com,http://b.com");
    5 response.setHeader("Access-Control-Allow-Headers", "X-Requested-With");
    6 response.setHeader("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS");
    7 
    8 //接受跨域的cookie
    9 response.setHeader("Access-Control-Allow-Credentials", "true");

三、存储

1.cookie,sessionStorage和localStorage的区别

1）容量：cookie只有4kb，而sessionStorage和localStorage最大容量5M

2）是否会携带到ajax中：cookie每次都会带，而sessionStorage和localStorage不会

3）API易用性：cookie要自己封装且较麻烦，而sessionStorage和localStorage，getItem,setItem基本上就能搞定
--------------------- 
作者：weixin_33749242 
来源：CSDN 
原文：https://blog.csdn.net/weixin_33749242/article/details/88168222 
版权声明：本文为博主原创文章，转载请附上博文链接！