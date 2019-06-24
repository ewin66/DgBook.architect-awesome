





# 【Y】防止浏览器窗口太小，设置最小宽度

https://blog.csdn.net/qq_32442967/article/details/82734678

页面设置宽高百分比的时候，防止浏览器窗口改变太小，页面内容发生改变
给页面设置最小宽度和最小高度

```css
.contain{
		width : 100%;
	    min-width: 810px;
	    height: 100%;
	    min-height: 600px;
}
```

------

## 个人整理资源下载：  <https://me.csdn.net/download/qq_32442967>







----







# 控制浏览器窗口的可以缩放的最小高度和宽度



$(window).resize(function(){
if(document.body.clientHeight<700){
alert(“禁止缩放”);
window.resizeTo(document.body.clientHeight,300);
}
});
宽度同理
--------------------- 
作者：陈振阳 
来源：CSDN 
原文：https://blog.csdn.net/xichenguan/article/details/52108107 
版权声明：本文为博主原创文章，转载请附上博文链接！





---



### CSS如何把DIV永远置于页面的底部？

https://bbs.csdn.net/topics/380201694?list=1557841

#footer{
width:100%; 
height:30px;
**position:absolute;**

  clear: both;	//清除浮动

bottom:0; 
overflow:hidden;
}







body相对定位，给这个div 绝对定位到底部

```html
<body>
<style>
body{position: relative;height: 1000px;}
.footer{position: absolute;bottom: 0;left: 0;height: 20px;width: 100%;background-color: yellow;}
</style>
<div class="header"></div>
<div class="main"></div>
<div class="footer"></div>

</body> 
```

作者：java_12138 
来源：CSDN 
原文：https://blog.csdn.net/java_12138/article/details/78912980 
版权声明：本文为博主原创文章，转载请附上博文链接！

# 

# css让footer永远保持在页面底部

https://blog.csdn.net/u011263845/article/details/47727463

---

---

# 【y】HTML+CSS底部footer两种固定方式



网页常见的底部栏（footer）目前有两种：

一、永久固定，不管页面的内容有多高，footer一直位于浏览器最底部，适合做移动端底部菜单，这个比较好实现;(向立凯)

二、相对固定，当页面内容高度不沾满浏览器高度，footer显示在浏览器底部，且不会出现滚动条，如果页面内容高度超出浏览器高度，footer则相对与内容的最底部，并且自动出现滚动条；(向立凯)



废话不多说，可以直接复制代码查看效果

一、永久固定(向立凯)



```html
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" />
    <style>
        body {
            padding-bottom: 50px;
        }
 
        .footer {
            position: fixed;
            left: 0px;
            bottom: 0px;
            width: 100%;
            height: 50px;
            background-color: #eee;
            z-index: 9999;
        }
    </style>
</head>
<body>
    内容，可以大量复制看效果<br />
 
    <div class="footer">固定在底部</div>
</body>
</html>
```

二、相对固定(向立凯)



```html
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" />
    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }
 
        html, body {
            height: 100%;
        }
 
        .footer {
            margin-top: -50px;
            height: 50px;
            background-color: #eee;
            z-index: 9999;
        }
 
        .wrap {
            min-height: 100%;
        }
 
        .main {
            padding-bottom: 50px;
        }
    </style>
</head>
<body>
    <div class="wrap">
        <div class="main">
            内容，可以大量复制看效果<br />
 
        </div>
    </div>
    <div class="footer">相对在底部</div>
</body>
</html>
```





---

---



# CSS div水平垂直居中和div置于底部

2016年10月06日 20:12:40

Yuyuquan

阅读数 9129

更多     个人分类：                    版权声明：本文为博主归纳总结                    https://blog.csdn.net/qq_27918787/article/details/52745383                

```html
一、水平居中
    .hor_center {  
            margin: 0 auto;  
    }  
 
 
二、水平垂直居中
 
<div class="content"></div>
 
    .content {  
          width: 360px;  
          height: 240px;  
    }  
    .ver_hor_center {  
          position: absolute;  
          top: 50%;  
          left: 50%;  
          margin-left: -180px; /*要居中的div的宽度的一半*/  
          margin-top: -120px; /*要居中的div的高度的一半*/  
    }  
 
 
三、div置于底部(footer)
 
    .bottom_footer {  
           position: fixed; /*or前面的是absolute就可以用*/  
           bottom: 0px;  
    }  
```



---

# HTML中footer一直沉底的最常用解决办法

想让footer沉底，肯定要让body中的内容填满整个屏幕，但是屏幕的高低又不能给一个具体的像素值，这样的话就想到了让body元素的高度100%，然后让footer相对于body来绝对定位，bottom:0px;这样就可以了，不过为了有些浏览器的兼容，最好用两个方向来定位，比如加个left值。但是别忘了让footer的width:100%;最好给个固定的高度。下面直接展示代码。

    footer {
      bottom: 0px;
      left: 0px;
      color: #666;
      width: 100%;
      clear: both;//清除浮动
      line-height: 90px;
      background-color: #ebebed;
    }
    
    body {
      font-family: "Microsoft YaHei" ! important;//字体
      min-height: 100%;//最底高度为页面的100%高度
    }
---------------------
作者：ljw_Josie 
来源：CSDN 
原文：https://blog.csdn.net/ljw_josie/article/details/53351171 
版权声明：本文为博主原创文章，转载请附上博文链接！

---



# 用Position:fixed实现底部浮动条的一种兼容方案

http://bbs.websjy.com/thread-8921-1-1.html

原理：在标准浏览器下用position: fixed的方式就可以了。IE6下面用overlay.className = overlay.className迫使浏览器重新布局，以达到position: fixed的效果。



----

