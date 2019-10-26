[Automapper结合EF实现insert,update方法](https://www.cnblogs.com/Finding2013/p/3144670.html)

​            **AutoMapper.Mapper.Map(input, entity);**

在执行更新操作时，需注意使用方法

```csharp
public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
```

对objectContext中的对象属性进行更新，而不能用这个方法

```csharp
public static TDestination Map<TSource, TDestination>(TSource source);
```

因为这个方法会创建一个新的TSource对象

 

先在global.asax或其他初始化地方初始化要map的几种类的关系，本例中完全通过名字进行匹配，也即对应类中的属性名完全相同：





```csharp

   AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InvestorModel, WMS_Cust>();
                cfg.CreateMap<WMS_Cust,InvestorModel>();
                cfg.CreateMap<InvestorModel,WMS_Cust_Original>();
                cfg.CreateMap<ContractModel, WMS_Contract>();
                cfg.CreateMap<WMS_Contract, ContractModel>();
            });   
```



具体示例代码如下：





```csharp
if (ModelState.IsValid)
            { 
                Contract.ContractStatus contractStatus = Contract.ContractStatus.CSR2;
                using (WMSEntities dbContext = new WMSEntities())
                {
                    //创建一个新的WMS_Cust对象，然后将invest.Investor的属性复制到该新对象中的对应属性值中
                    WMS_Cust wms_cust = Mapper.Map<InvestorModel, WMS_Cust>(invest.Investor);
                    WMS_Cust oldWms_cust = dbContext.WMS_Cust.Where(o => o.CityIDNo == invest.Investor.CityIDNo).FirstOrDefault();
                    WMS_Contract wms_contract = Mapper.Map<ContractModel, WMS_Contract>(invest.Contract); // new WMS_Contract() { CustID = 0, ProductID = invest.Contract.ProductID, Amount = 100.1m, CollectBankAccount = "aaa", CollectBankID = 1, OperateDate = DateTime.Now, Operator = 1, PayBankAccount = "aaa", PayBankID = 1, ContinueMode = false, ContinueProductID = 1 };
                    //需为本类增加attribute [InitializeSimpleMembership],WebSecurity方可使用
                    wms_contract.Operator = WebSecurity.CurrentUserId;
                    wms_contract.OperateDate = DateTime.Now;
                    wms_contract.State = Contract.ContractStatus.CSR2.ToString();
                    if (oldWms_cust != null)
                    {
                        //将invest.Investor中的属性复制到oldWms_cust中的对应属性值中，oldWms_cust原对象不变
                        Mapper.Map<InvestorModel, WMS_Cust>(invest.Investor, oldWms_cust);

                        WMS_Contract oldwms_contract = oldWms_cust.WMS_Contract.Where(o => o.ID == wms_contract.ID).FirstOrDefault();
                        if (oldwms_contract != null)
                        {
                            Mapper.Map<ContractModel, WMS_Contract>(invest.Contract, oldwms_contract);
                            contractStatus=Contract.ContractStatus.ToBeMatch;
                            oldwms_contract.State = contractStatus.ToString();
                        }
                        else
                        {
                            oldWms_cust.WMS_Contract.Add(wms_contract);
                        }
                    }
                    else
                    {
                        dbContext.WMS_Cust.AddObject(wms_cust);
                        WMS_Cust_Original wms_cust_origin = Mapper.Map<InvestorModel, WMS_Cust_Original>(invest.Investor);
                        dbContext.WMS_Cust_Original.AddObject(wms_cust_origin);
                        wms_contract.WMS_Cust = wms_cust;
                        dbContext.WMS_Contract.AddObject(wms_contract);
                    }
                    dbContext.SaveChanges();

                }
                if (contractStatus == Contract.ContractStatus.CSR2)
                {
                    return View("Result", new ResultModel() { Title = "录入成功", Message = "录入成功！已提交至复核队列!" });
                }
                else
                {
                    return View("Result", new ResultModel() { Title = "复核成功", Message = "复核成功！已提交至待匹配队列!" });
                }
            }
```

