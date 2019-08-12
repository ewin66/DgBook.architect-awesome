##   [Linq/EF/lambda Group by/Order by 多个字段详细用法](https://www.cnblogs.com/jory/p/6136427.html)



1）单个字段Group by：

```
//a.Key类型与a.Province字段类型一样 
.GroupBy(a => a.Province).Select(a => a.Key).ToList();　
```

2）多个字段Group by：

```
//此时返回的数据列表需要自己转换
.GroupBy(a => ``new` `{ a.Date, a.Week }).Select(a => a.Key).ToList();
```

3）单个字段Order by：

```
`.OrderBy(a => a.Date).ToList()`
```

4）多个字段Order by：

```
`.OrderBy(a => a.Date).ThenBy(a => a.EndTime).ThenBy(a => a.StartTime).ToList()`
```

5）多个字段Order by倒序：

```
`.OrderByDescending(a => a.Date).ThenByDescending(a => a.EndTime).ThenByDescending(a => a.StartTime).ToList()`
```

版权所有：jory—经得起折磨，耐得住寂寞                                                                                                                                                                                                                                                                                            



https://www.cnblogs.com/jory/p/6136427.html

---



## EF   照指定条件置顶排序

```csharp

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Top([Bind(Include = "Plan_ID,StartID,EndID")] SelectProgram selectProgram)
        {
            var all = from m in db.SelectProgram
                      orderby m.Plan_ID == selectProgram.Plan_ID ? 0 : 1
                      select m;     
            if (all == null)
            {
                return HttpNotFound();
            }
            return View(all);
        }

```





​                                                                



---





## [IGrouping转换为IQueryable](https://www.cnblogs.com/opts/p/7827101.html)







```csharp

之前一直被一个Linq问题困扰，数据分组后就会变成IGrouping形式，但是返回的需要的是 IQueryable，现在终于知道怎么转了。

query =  from c in query 
　　　　group c by c.Id
　　　　into cGroup
　　　　orderby cGroup.Key
　　　　select cGroup.FirstOrDefault();

```



https://www.cnblogs.com/opts/p/7827101.html

---



