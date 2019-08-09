##                                                 Linq使用Group By

​                                                                                              

天才小龙



浏览1676

- ​                             [DB](https://yq.aliyun.com/tags/type_blog-tagid_1245/)                         
- ​                             [BY](https://yq.aliyun.com/tags/type_blog-tagid_1284/)                         
- ​                             [表达式](https://yq.aliyun.com/tags/type_blog-tagid_1440/)                         
- ​                             [序列](https://yq.aliyun.com/tags/type_blog-tagid_1484/)                         
- ​                             [Group](https://yq.aliyun.com/tags/type_blog-tagid_2291/)                         
- ​                             [linq](https://yq.aliyun.com/tags/type_blog-tagid_2635/)                         



### **1.简单形式：**

1. var q =  
2. from p in db.Products 
3. group p by p.CategoryID into g
4. select g;

语句描述：Linq使用Group By按CategoryID划分产品。

说明：from p in db.Products 表示从表中将产品对象取出来。group p by p.CategoryID into  g表示对p按CategoryID字段归类。其结果命名为g，一旦重新命名，p的作用域就结束了，所以，最后select时，只能select g。

### **2.最大值**

1. var q =  
2. from p in db.Products 
3. group p by p.CategoryID into g
4. select new {
5. g.Key,
6. MaxPrice = g.Max(p =**>** p.UnitPrice) 
7. };

语句描述：Linq使用Group By和Max查找每个CategoryID的最高单价。

说明：先按CategoryID归类，判断各个分类产品中单价最大的Products。取出CategoryID值，并把UnitPrice值赋给MaxPrice。

### **3.最小值**

1. var q =  
2. from p in db.Products 
3. group p by p.CategoryID into g
4. select new {
5. g.Key,
6. MinPrice = g.Min(p =**>** p.UnitPrice) 
7. };

语句描述：Linq使用Group By和Min查找每个CategoryID的最低单价。

说明：先按CategoryID归类，判断各个分类产品中单价最小的Products。取出CategoryID值，并把UnitPrice值赋给MinPrice。

### **4.平均值**

1. var q =  
2. from p in db.Products 
3. group p by p.CategoryID into g
4. select new {
5. g.Key,
6. AveragePrice = g.Average(p =**>** p.UnitPrice) 
7. };

语句描述：Linq使用Group By和Average得到每个CategoryID的平均单价。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品中单价的平均值。

### **5.求和**

1. var q =  
2. from p in db.Products 
3. group p by p.CategoryID into g
4. select new {
5. g.Key,
6. TotalPrice = g.Sum(p => p.UnitPrice) 
7. };





## Linq使用Group By问题的解决方法

学习Linq时，经常会遇到Linq使用Group By问题，这里将介绍Linq使用Group By问题的解决方法。

### **1.计数**

1. var q =   
2. from p in db.Products  
3. group p by p.CategoryID into g  
4. select new {  
5. g.Key,  
6. NumProducts = g.Count()  
7. }; 

语句描述：Linq使用Group By和Count得到每个CategoryID中产品的数量。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品的数量。

### **2.带条件计数**

1. var q =   
2. from p in db.Products  
3. group p by p.CategoryID into g  
4. select new {  
5. g.Key,  
6. NumProducts = g.Count(p =**>** p.Discontinued)  
7. }; 

语句描述：Linq使用Group By和Count得到每个CategoryID中断货产品的数量。

说明：先按CategoryID归类，取出CategoryID值和各个分类产品的断货数量。 Count函数里，使用了Lambda表达式，Lambda表达式中的p，代表这个组里的一个元素或对象，即某一个产品。

### **3.Where限制**

1. var q =   
2. from p in db.Products  
3. group p by p.CategoryID into g  
4. where g.Count() **>**= 10   
5. select new {  
6. g.Key,  
7. ProductCount = g.Count()  
8. }; 

语句描述：根据产品的―ID分组，查询产品数量大于10的ID和产品数量。这个示例在Group By子句后使用Where子句查找所有至少有10种产品的类别。

说明：在翻译成SQL语句时，在最外层嵌套了Where条件。

### **4.多列(Multiple Columns)**

1. var categories =   
2. from p in db.Products  
3. group p by new  
4. {  
5. p.CategoryID,  
6. p.SupplierID  
7. }  
8. into g  
9. select new  
10. {  
11. g.Key,  
12. g  
13. }; 

语句描述：Linq使用Group By按CategoryID和SupplierID将产品分组。

说明：既按产品的分类，又按供应商分类。在by后面，new出来一个匿名类。这里，Key其实质是一个类的对象，Key包含两个Property：CategoryID、SupplierID。用g.Key.CategoryID可以遍历CategoryID的值。

**5.表达式(Expression)**

1. var categories =   
2. from p in db.Products  
3. group p by new { Criterion = p.UnitPrice **>** 10 } into g   
4. select g; 

语句描述：Linq使用Group By返回两个产品序列。第一个序列包含单价大于10的产品。第二个序列包含单价小于或等于10的产品。

说明：按产品单价是否大于10分类。其结果分为两类，大于的是一类，小于及等于为另一类。

 

描述：根据顾客的国家分组，查询顾客数大于5的国家名和顾客数
查询句法：
var 一般分组 = from c in ctx.Customers
group c by c.Country into g
where g.Count() > 5
orderby g.Count() descending
select new
{
国家 = g.Key,
顾客数 = g.Count()
};
对应SQL：
SELECT [t1].[Country], [t1].[value3] AS [顾客数]
FROM (
SELECT COUNT(*) AS [value], COUNT(*) AS [value2], COUNT(*) AS [value3], [t0].[Country]
FROM [dbo].[Customers] AS [t0]
GROUP BY [t0].[Country]
) AS [t1]
WHERE [t1].[value] > @p0
ORDER BY [t1].[value2] DESC
-- @p0: Input Int32 (Size = 0; Prec = 0; Scale = 0) [5]

描述：根据国家和城市分组，查询顾客覆盖的国家和城市
查询句法：
var 匿名类型分组 = from c in ctx.Customers
group c by new { c.City, c.Country } into g
orderby g.Key.Country, g.Key.City
select new
{
国家 = g.Key.Country,
城市 = g.Key.City
};
描述：按照是否超重条件分组，分别查询订单数量
查询句法：
var 按照条件分组 = from o in ctx.Orders
group o by new { 条件 = o.Freight > 100 } into g
select new
{
数量 = g.Count(),
是否超重 = g.Key.条件 ? "是" : "否"
};