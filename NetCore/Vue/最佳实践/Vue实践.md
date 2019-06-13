





---



1.第三方库要和你的业务js分隔开
2.千万不要在首页加载大的第三方库

[发布于 2017-04-29](https://www.zhihu.com/question/58773096/answer/162304823)





---

# [element-ui 与 iView的对比](https://zhuanlan.zhihu.com/p/27479767)



1.Element 有级联选择。

2.Element 的 Popover 即 iView 的 Poptip 组件。

3.Tooltip iView 和 Element 都有。

一些小众组件上各有所长 整体iview 更丰富（时间轴，加载进度条，气泡卡片 ，BackTop,图钉）



一些小众组件上各有所长 整体iview 更丰富（时间轴，加载进度条，气泡卡片 ，BackTop,图钉）



**项目优化角度**

首屏优化，第三方组件库依赖过大 会给首屏加载带来很大的压力，一般解决方式是 按需求引入组件

element-ui  根据官方说明 现需要引入 [babel-plugin-component](https://link.zhihu.com/?target=https%3A//github.com/QingWei-Li/babel-plugin-component) 插件 做相关配置 然后直接在组件目录 注册全局组件

**element-ui**

如果只替换颜色 ，可以使用 [在线主题生成工具](https://link.zhihu.com/?target=https%3A//elementui.github.io/theme-preview) 在线编辑颜色， 生成element-ui 主题 直接下载 再引入

深度定制主题

官方提供了 主题生成工具  element-them 

执行命令 初始化得到一个配置文件 ，修改相关配置 经过编译得到 得到相关主题文件  再通过babel 插件引入**对设计人员**

**element 提供了 Sketch 和 Axure 工具 对设计人员友好**

**iview 没有提供**

以上 ...

[编辑于 2017-06-21](https://www.zhihu.com/question/53842719/answer/185855867)

---

React 的生态圈带给了我们 Redux（immutable 的思想，带来 hot-reloading 的用法）、react-motion（真正的 spring 动画）、react-router（经历过 Angular 1 的我：路由还可以这样写？路由还可以到处扔？）、CSS-in-JS 等等漂亮的想法，以及 Hooks、Suspense、Portal、SSR、React Native、Async Rendering，甚至 [Yew](https://link.zhihu.com/?target=https%3A//github.com/DenisKolodin/yew) 这种 “JSX in Rust”。

我这里并非在说，这些是 React 生态圈的原创。但它们都是在 React 的应用中得到认可、逐渐主流的。

所以回到根源，我并不选择去关注某一堆代码，而是它背后一群有创造力的人（和他们的天才想法）。

Dan Abramov、Evan You、Cheng Lou、Andrew Clark、Sebastian Markbåge、Ryan Florence、Christopher Chedeau、Jordan Walke……每一个人都值得我 bet on。

[编辑于 2019-05-23]()

作者：Parabola

链接：https://www.zhihu.com/question/294210442/answer/569044233

来源：知乎

著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。

---





使用vue不需要转变思想，从传统JS开发转到vue非常容易。但需要记指令，各种指令，那么免不了经常查文档。

React是大道至简的，学React只需要记住一句话：“props不变、states可变，所有的变化都来自setStates。”就学完了，JSX基本不用学，和写HTML一样。即不用记任何指令，API也只需掌握屈指可数的几个就能实现99%的功能。

vue像一个武林高手，vue的实现精致优雅，吸收了传统武学精髓，对世面上所见过的所有招式都能见招拆招，深受各路武侠喜爱。

而React则是一把手枪，你只需要记住：“无论遇到什么样的武功高强之人，只用瞄准，扣动扳机。”

最后我说一个vue目前来说比react难办好几个数量级的事情:我目前搞定了react 16在ie8甚至更低的浏览器上兼容。我认为这个问题，如果被有上古浏览器兼容需求的项目遇到了，vue将极其难搞，而react则轻松许多。

[编辑于 2018-09-28]()

作者：甘明

链接：https://www.zhihu.com/question/294210442/answer/492106025

来源：知乎

著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。

