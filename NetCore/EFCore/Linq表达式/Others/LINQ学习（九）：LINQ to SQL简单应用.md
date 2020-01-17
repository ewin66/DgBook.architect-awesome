前面的章节简单的介绍了LINQ的基础知识，那么我们应该如何使用LINQ去连接数据库并对数据进行操作呢？下面举个例子：

 

**1.新建一个空的网站。**

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072909573010.jpg)

 

**2.创建数据库LinqData.mdf，添加表Product，再向表里面添加数据。**

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072909580294.jpg)

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072909595220.jpg)

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072910000257.jpg)

 

**3.创建LINQProduct.dbml，将表Product拖到里面。**

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072910033893.jpg)

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072910034816.jpg)

 

**4.创建Web窗体，在页面上加入一个GridView控件，然后编写绑定代码。**

Default.aspx：

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/copycode.gif)](javascript:void(0);)

```
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="ProductGridView" runat="server"></asp:GridView>
    </form>
</body>
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

Default.aspx.cs：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProductList();
        }
    }

    protected void ProductList()
    {
        LINQProductDataContext lp = new LINQProductDataContext();
        var query = from p in lp.Product
                    select p;
        ProductGridView.DataSource = query;
        ProductGridView.DataBind();
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**5.运行显示结果。**

![img](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B9%9D%EF%BC%89%EF%BC%9ALINQ%20to%20SQL%E7%AE%80%E5%8D%95%E5%BA%94%E7%94%A8.assets/2012072910144697.jpg)

 

**6.简单说明。**

（1）LINQProductDataContext类继承于System.Data.Linq.DataContext，DataContext类表示 LINQ to SQL 框架的主入口点，提供了一系列数据库操作方法。

（2）将数据库中Poruduct表转换为密封类Poruduct，表中的字段转为类对应的字段，就可以通过对象方式进行操作。

（3）按照我的理解LINQ to SQL 框架会将对应的查询方法编译成SQL语句再从数据库中获得数据。