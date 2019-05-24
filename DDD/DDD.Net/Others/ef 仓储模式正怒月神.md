# ef 仓储模式

​                                                   2017年03月17日 15:10:21           [正怒月神](https://me.csdn.net/hanjun0612)           阅读数：3889                                                                  

​                   

​                                                                         版权声明：本文为博主原创文章，未经博主允许不得转载。          https://blog.csdn.net/hanjun0612/article/details/62887466        

构建一个仓储模式。

### Model

大家自己创建就行了，上个图，就不多说了（我是code first）

![img](assets/20170317151220192)



###  IDAL



```csharp
namespace IDAL



{



    public interface IBaseRepository<T>



    {



        /// <summary>



        /// 添加



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>添加后的数据实体</returns>



        T Add(T entity);



 



        /// <summary>



        /// 添加



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>添加后的数据实体</returns>



        bool AddOK(T entity);



 



        /// <summary>



        /// 查询记录数



        /// </summary>



        /// <param name="predicate">条件表达式</param>



        /// <returns>记录数</returns>



        int Count(Expression<Func<T, bool>> predicate);



 



        /// <summary>



        /// 更新



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        T Update(T entity);



 



        /// <summary>



        /// 更新



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        bool UpdateOK(T entity);



 



        /// <summary>



        /// 删除



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        bool Delete(T entity);



 



        /// <summary>



        /// 是否存在



        /// </summary>



        /// <param name="anyLambda">查询表达式</param>



        /// <returns>布尔值</returns>



        bool Exist(Expression<Func<T, bool>> anyLambda);



 



        /// <summary>



        /// 查询数据



        /// </summary>



        /// <param name="whereLambda">查询表达式</param>



        /// <returns>实体</returns>



        T Find(Expression<Func<T, bool>> whereLambda);



 



        /// <summary>



        /// 查找数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="whereLamdba">查询表达式</param>



        /// <returns></returns>



        IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba);



 



        /// <summary>



        /// 查找数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="whereLamdba">查询表达式</param>



        /// <param name="isAsc">是否升序</param>



        /// <param name="orderLamdba">排序表达式</param>



        /// <returns></returns>



        IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);



 



        /// <summary>



        /// 查找分页数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="pageIndex">当前页</param>



        /// <param name="pageSize">每页记录数</param>



        /// <param name="totalRecord">总记录数</param>



        /// <param name="whereLamdba">查询表达式</param>



        /// <param name="isAsc">是否升序</param>



        /// <param name="orderLamdba">排序表达式</param>



        /// <returns></returns>



        IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);



 



        List<T> FindListBySQL<T>(string sql, params object[] parameters);



 



        int ExecuteBySQL(string sql, params object[] parameters);



    }



}
```



### IBLL

这里我就偷懒了（直接拷贝了idal），业务层实际可以根据自己需求封装





```csharp
namespace IBLL



{



    public interface IBaseRepositoryBLL<T>  where T:class



    {



        /// <summary>



        /// 添加



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>添加后的数据实体</returns>



        T Add(T entity);



 



        /// <summary>



        /// 添加



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>添加后的数据实体</returns>



        bool AddOK(T entity);



 



        /// <summary>



        /// 查询记录数



        /// </summary>



        /// <param name="predicate">条件表达式</param>



        /// <returns>记录数</returns>



        int Count(Expression<Func<T, bool>> predicate);



 



        /// <summary>



        /// 更新



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        T Update(T entity);



 



        /// <summary>



        /// 更新



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        bool UpdateOK(T entity);



 



        /// <summary>



        /// 删除



        /// </summary>



        /// <param name="entity">数据实体</param>



        /// <returns>是否成功</returns>



        bool Delete(T entity);



 



        /// <summary>



        /// 是否存在



        /// </summary>



        /// <param name="anyLambda">查询表达式</param>



        /// <returns>布尔值</returns>



        bool Exist(Expression<Func<T, bool>> anyLambda);



 



        /// <summary>



        /// 查询数据



        /// </summary>



        /// <param name="whereLambda">查询表达式</param>



        /// <returns>实体</returns>



        T Find(Expression<Func<T, bool>> whereLambda);



 



        /// <summary>



        /// 查找数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="whereLamdba">查询表达式</param>



        /// <returns></returns>



        IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba);



 



        /// <summary>



        /// 查找数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="whereLamdba">查询表达式</param>



        /// <param name="isAsc">是否升序</param>



        /// <param name="orderLamdba">排序表达式</param>



        /// <returns></returns>



        IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);



 



        /// <summary>



        /// 查找分页数据列表



        /// </summary>



        /// <typeparam name="S">排序</typeparam>



        /// <param name="pageIndex">当前页</param>



        /// <param name="pageSize">每页记录数</param>



        /// <param name="totalRecord">总记录数</param>



        /// <param name="whereLamdba">查询表达式</param>



        /// <param name="isAsc">是否升序</param>



        /// <param name="orderLamdba">排序表达式</param>



        /// <returns></returns>



        IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);



 



        List<T> FindListBySQL<T>(string sql, params object[] parameters);



        int ExecuteBySQL(string sql, params object[] parameters);



    }
```



以上工作做完后，我们开始准备  DAL和 BLL了。

分别创建DAL和 BLL类库。（你要是文件夹，也无所谓）

### DAL

先来一个图形象一点
 （截图中，大家可能看到了BaseRespositoryRedis.cs 顾名思义，不解释了。下一章我贴代码吧。其实也没什么好说的）

![img](assets/20170317150015233)



ELDBEntity.cs（其实就是我们的 DBContext，不多说了吧）



```csharp
namespace DAL.DBContext



{



    public class ELDBEntity : DbContext



    {



        public ELDBEntity()



            : base("name=Entities")



        {



        }



    



        protected override void OnModelCreating(DbModelBuilder modelBuilder)



        {



            



            



        }



       



        public DbSet<SYS_User> SYS_User { get; set; }



        public DbSet<SYS_UserAccess> SYS_UserAccess { get; set; }



        



    }



}
```

DbContextFactory



这个文件，稍微介绍一下，

主要是为了让每一个用户，从始至终只使用自己的dbcontext。

简单点说，每个用户都会有属于自己的dbcontext。但是只会有一个，不会有多个。

代码：



```csharp
namespace DAL



{



    public class DbContextFactory



    {



        public static ELDBEntity GetCurrentContext()



        {



            ELDBEntity _nContext = CallContext.GetData("ELDBEntity") as ELDBEntity;



            if (_nContext == null)



            {



                _nContext = new ELDBEntity();



                CallContext.SetData("ELDBEntity", _nContext);



            }



            



            return _nContext;



        }



    }



}
```

BaseRepository.cs

：



由于我封装了 分页方法，所有添加了Webdiyer.WebControls.Mvc引用，

大家可以通过nuget 查找MvcPager 添加这个dll

不喜欢的童鞋，自行删除。

另外，我封装了通过sql语句来增删改查。



```csharp
using Webdiyer.WebControls.Mvc;



namespace DAL.Base



{



    public class BaseRepository<T> :BaseClass, IBaseRepository<T> where T: class



    {



        public ELDBEntity dbEF = DbContextFactory.GetCurrentContext();



        //public ELDBEntity dbEF = ELDBEntity();



        



        public virtual T Add(T entity)



        {



            dbEF.Entry<T>(entity).State = EntityState.Added;



            dbEF.SaveChanges();



            return entity;



        }



 



        public virtual bool AddOK(T entity)



        {



            dbEF.Entry<T>(entity).State = EntityState.Added;



            return dbEF.SaveChanges() > 0;            



        }



 



        public virtual int Count(Expression<Func<T, bool>> predicate)



        {



            return dbEF.Set<T>().AsNoTracking().Count(predicate);



        }



 



        public virtual T Update(T entity)



        {



            dbEF.Set<T>().Attach(entity);



            dbEF.Entry<T>(entity).State = EntityState.Modified;



            dbEF.SaveChanges();



            return entity;



        }



 



        public virtual bool UpdateOK(T entity)



        {



            dbEF.Set<T>().Attach(entity);



            dbEF.Entry<T>(entity).State = EntityState.Modified;



            return dbEF.SaveChanges() > 0;



        }



 



        public virtual bool Delete(T entity)



        {



            dbEF.Set<T>().Attach(entity);



            dbEF.Entry<T>(entity).State = EntityState.Deleted;



           



            return dbEF.SaveChanges() > 0;



        }



 



        public virtual bool Exist(Expression<Func<T, bool>> anyLambda)



        {



            return dbEF.Set<T>().AsNoTracking().Any(anyLambda);



        }



 



        public virtual T Find(Expression<Func<T, bool>> whereLambda)



        {



            T _entity = dbEF.Set<T>().AsNoTracking().FirstOrDefault<T>(whereLambda);



            return _entity;



        }



 



        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba)



        {



            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);



            return _list;



        }



 



        public virtual IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)



        {



            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);



            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);



            else _list = _list.OrderByDescending<T, S>(orderLamdba);



            return _list;



        }



 



        public virtual IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)



        {



            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);



            totalRecord = _list.Count();



            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);



            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);



            return _list;



        }



        public virtual PagedList<T> FindPageList1<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)



        {



            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);



            totalRecord = _list.Count();



            PagedList<T> resultList = null;



            if (isAsc) resultList = _list.OrderBy<T, S>(orderLamdba).ToPagedList(pageIndex,pageSize);



            else resultList = _list.OrderByDescending<T, S>(orderLamdba).ToPagedList(pageIndex, pageSize);



            return resultList;



        }



 



 



        public virtual List<T> FindListBySQL<T>(string sql, params object[] parameters)



        {



            var list = dbEF.Database.SqlQuery<T>(sql, parameters).ToList();



            return list;



        }



 



        public virtual int ExecuteBySQL(string sql, params object[] parameters)



        {



            var q = dbEF.Database.ExecuteSqlCommand(sql, parameters);



            return q;



        }



    }



}
```



### BLL

BaseRepositoryBLL.cs



```csharp
namespace BLL.Base



{



    public class BaseRepositoryBLL<T> :BaseClass, IBaseRepositoryBLL<T> where T:class



    {



        BaseRepository<T> obj = new BaseRepository<T>();



        public virtual T Add(T entity)



        {



            return obj.Add(entity);



        }



 



        public virtual bool AddOK(T entity)



        {



            return obj.AddOK(entity);



        }



 



        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)



        {



            return obj.Count(predicate);



        }



 



        public virtual T Update(T entity)



        {



            return obj.Update(entity);



        }



 



        public virtual bool UpdateOK(T entity)



        {



            return obj.UpdateOK(entity);



        }



 



        public virtual bool Delete(T entity)



        {



            return obj.Delete(entity);



        }



 



        public virtual bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> anyLambda)



        {



            return obj.Exist(anyLambda);



        }



 



        public virtual T Find(Expression<Func<T, bool>> whereLambda)



        {



            return obj.Find(whereLambda);



        }



 



        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba)



        {



            var _list = obj.FindList(whereLamdba);



            return _list;



        }



 



        public virtual IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, System.Linq.Expressions.Expression<Func<T, S>> orderLamdba)



        {



            return obj.FindList<S>(whereLamdba, isAsc, orderLamdba);



        }



 



        public virtual IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)



        {



            return obj.FindPageList<S>(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);



        }



 



        public virtual PagedList<T> FindPageList1<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)



        {



            return obj.FindPageList1<S>(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);



        }



 



        public virtual List<T> FindListBySQL<T>(string sql, params object[] parameters)



        {



            return obj.FindListBySQL<T>(sql, parameters);



        }



 



        public virtual int ExecuteBySQL(string sql, params object[] parameters)



        {



            return obj.ExecuteBySQL(sql, parameters);



        }



    }



}
```

### 调用方式

DAL层：先创建属于某个实体对象的DAL

```csharp
namespace DAL



{



    public class SYS_UserDAL : BaseRepository<SYS_User>



    {



        /// <summary>



        /// 登录



        /// </summary>



        /// <param name="e"></param>



        /// <returns></returns>



        //public SYS_User Login(SYS_User u)



        //{



        //    //u.Password = EncryptionClass.MD5_16(e.pwd);



        //    var q = dbEF.SYS_User.FirstOrDefault(x => x.Password == u.Password && x.Account == u.Account);



        //    return q;



 



        //}



    }



}
```

 BLL层：然后创建对应的BLL

```csharp
namespace BLL



{



    public class SYS_UserBLL : BaseRepositoryBLL<SYS_User>



    {



        SYS_UserDAL dal = new SYS_UserDAL();



        /// <summary>



        /// 添加账号



        /// </summary>



        /// <param name="entity"></param>



        /// <returns></returns>



        public bool AddUser(SYS_User entity)



        {



            return dal.AddOK(entity);



        }



 



    }



}
```