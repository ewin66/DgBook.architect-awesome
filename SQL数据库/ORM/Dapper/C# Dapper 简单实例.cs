/// <summary>

 
	/// 分页信息
	/// </summary>
	public class PageInfo<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public PageInfo()
        {
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalCount
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Data
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="data"></param>
        public PageInfo(long total, IEnumerable<T> data)
        {
            this.TotalCount = total;
            this.Data = data;
        }
    }

************* 

using DapperExtensions.Mapper;
using Statistics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowStatistics
{
    public static class Mappings
    {
        public static void Initialize()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);

            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                typeof(Mappings).Assembly
            });
        }

        public class FlowCellMapper : ClassMapper<FlowCell>
        {
            public FlowCellMapper()
            {
                Table("jxc_flow_cell");
                //Map(fcel => fcel.id).Column("id");
                //Map(fcel => fcel.parent_id).Column("parent_id");
                //Map(fcel => fcel.create_time).Column("create_time");
                //Map(fcel => fcel.type_id).Column("type_id");
                Map(fcel => fcel.comId).Column("bloc_code");
                //Map(fcel => fcel.bloc_name).Column("bloc_name");
                //Map(fcel => fcel.cell_number).Column("cell_number");
                //Map(fcel => fcel.name).Column("name");
                //Map(fcel => fcel.flows).Column("flows");
                //Map(fcel => fcel.status).Column("status");
                //Map(fcel => fcel.del).Column("del");
                AutoMap();
            }
        }
    }
}

 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System.Data;
using Z.Dapper.Plus;

namespace FlowStatistics
{
    /// <summary>
    /// 数据客户端
    /// 参考：https://github.com/StackExchange/dapper-dot-net
    /// Predicates参考：https://github.com/tmsmith/Dapper-Extensions/wiki/Predicates
    /// https://github.com/zzzprojects/Dapper-Plus
    /// </summary>
    public class DbClient : IDisposable, IDbClient
    {
        string connStr = @"Data Source=.\sqlexpress;Initial Catalog=tempdb;Integrated Security=True;uid=sa;pwd=123456";      
        int commandTimeout = 30;
        /// <summary>
        /// 数据客户端
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="commandTimeout">操作超时,单位：秒</param>
        /// <param name="autoEditEntityTime">是否自动更实体对象的创建时间、更新时间</param>
        public DbClient(string connStr, int commandTimeout = 30)
        {
            if (string.IsNullOrWhiteSpace(connStr)) throw new NoNullAllowedException("数据库连接字符串不允许为空");
            this.connStr = connStr;
            this.commandTimeout = commandTimeout;
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            Mappings.Initialize();
            //DapperExtensions.DapperExtensions.DefaultMapper = typeof(CustomPluralizedMapper<>);
        }
        /// <summary>
        /// 获取打开的连接
        /// </summary>
        /// <param name="mars">MSSql数据库下有效：如果为 true，则应用程序可以保留多活动结果集 (MARS)。 如果为 false，则应用程序必须处理或取消一个批处理中的所有结果集，然后才能对该连接执行任何其他批处理。</param>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            IDbConnection connection = null;
            string cs = connStr;
            connection = new MySqlConnection(cs);
            connection.Open();
            return connection;
        }

        #region Add

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体对象集</param>
        public void Add<T>(IEnumerable<T> entities) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        cnn.Insert(entities, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
            }

            //using (IDbConnection cnn = GetOpenConnection())
            //{
            //    var trans = cnn.BeginTransaction();
            //    cnn.Execute(@"insert Member(Username, IsActive) values(@Username, @IsActive)", entities, transaction: trans);
            //    trans.Commit();
            //}
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>实体对象</returns>
        public T Add<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                T res = null;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        int id = cnn.Insert(entity, trans, commandTimeout);
                        if (id > 0)
                        {
                            res = entity;
                        }
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }
       
        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Update<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Update(entity, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        public bool Update<T>(IEnumerable<T> entities) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        trans.BulkUpdate(entities);
                        res = true;
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Delete<T>(T entity) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Delete(entity, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">实体对象</param>
        /// <returns>是否成功</returns>
        public bool Delete<T>(object predicate) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                bool res = false;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        res = cnn.Delete(predicate, trans, commandTimeout);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                return res;
            }
        }

        #endregion

        #region Query/Get

        /// <summary>
        /// 查询单个结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">实体的Id属性值</param>
        /// <returns>查询结果</returns>
        public T Get<T>(object id) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                T res = null;
                try
                {
                    res = cnn.Get<T>(id, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return res;
            }
        }

        /// <summary>
        /// 查询结果集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">分页查询条件</param>
        /// <param name="sort">是否排序</param>
        /// <returns>查询结果</returns>
        public IEnumerable<T> Get<T>(object predicate = null, IList<ISort> sort = null) where T : class, new()
        {
            using (IDbConnection cnn = GetOpenConnection())
            {
                IEnumerable<T> res = null;
                try
                {
                    res = cnn.GetList<T>(predicate, sort, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return res;
            }
        }

        /// <summary>
        /// 查询结果分页
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">分页查询条件</param>
        /// <param name="sort">是否排序</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>查询结果</returns>
        public PageInfo<T> Get<T>(object predicate, IList<ISort> sort, int pageIndex, int pageSize) where T : class, new()
        {
            if (sort == null) throw new ArgumentNullException("sort 不允许为null");
            if (pageIndex < 0) pageIndex = 0;
            using (IDbConnection cnn = GetOpenConnection())
            {
                PageInfo<T> pInfo = null;
                try
                {
                    int count = cnn.Count<T>(predicate, null, commandTimeout);
                    pInfo = new PageInfo<T>();
                    pInfo.TotalCount = count;
                    pInfo.Data = cnn.GetPage<T>(predicate, sort, pageIndex, pageSize, null, commandTimeout);
                }
                catch (DataException ex)
                {
                    throw ex;
                }
                return pInfo;
            }
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DbClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}

public class FlowCell
    {
        public int Id { get; set; }
        public int type_id { get; set; }
        public string comId { get; set; }
        public string bloc_name { get; set; }
        public string cell_number { get; set; }
        public string name { get; set; }

        public int flows { get; set; }     

        public int status { get; set; }      

        public int del { get; set; }
    }

// 使用：

public void Statistics()
        {
            try
            {
                DbClient dbClient = new DbClient(mysqlConstr);
                var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<FlowCell>(f => f.status, Operator.Eq, 1));
                pg.Predicates.Add(Predicates.Field<FlowCell>(f => f.del, Operator.Eq, 0));

                var flowCell = dbClient.Get<FlowCell>(4);

                IList<ISort> sorts = new List<ISort>();
                ISort sort = new Sort();
                sort.Ascending = false;
                sort.PropertyName = "name"; //如果有Map，则此次要填写Map对象的字段名称，而不是数据库表字段名称
                sorts.Add(sort);
                var flowCell2 = dbClient.Get<FlowCell>(pg, sorts);

                var flowCell3 = dbClient.Get<FlowCell>(pg, sorts, 0, 2);
            }
            catch (Exception ex)
            {

            }
        }