
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using Research.PropertyInfoes;
using Research.PropertyInfoes.Dtos;
using Research.PropertyInfoes.DomainService;
using System.Text;

namespace Research.PropertyInfoes
{
    /// <summary>
    /// PropertyInfo应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class PropertyInfoAppService : ResearchAppServiceBase, IPropertyInfoAppService
    {
        private readonly IRepository<PropertyInfo, int> _entityRepository;

        private readonly IPropertyInfoManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public PropertyInfoAppService(
        IRepository<PropertyInfo, int> entityRepository
        ,IPropertyInfoManager entityManager
        )
        {
            _entityRepository = entityRepository; 
             _entityManager=entityManager;
        }


        /// <summary>
        /// 初始化属性名
        /// 以添加mysql的EF列名映射 
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<PropertyInfoListDto>> InitColumnFullName( )
        {

            var query = _entityRepository.GetAll();
            var entityList = await query 
              .ToListAsync();
            // TODO:根据传入的参数添加过滤条件
            foreach (var item in entityList)
            {
                var _entity = _entityRepository.FirstOrDefault(t => t.Id == item.Id); 
                if (!string.IsNullOrWhiteSpace(_entity.PropertyFullName))
                {
                    _entity.PropertyName = NamingConventions.GetPropertyName(_entity.PropertyFullName);
                    _entity.ColumnName = NamingConventions.GetSqlColumnRename(_entity.PropertyName);
                    _entity.PropertyNameWithColumn = NamingConventions.GetPropertyNameWithColumn(_entity.PropertyFullName,
                             _entity.ColumnName, _entity.IsZeroModule ?? 0); 
                }
                //CurrentUnitOfWork.SaveChanges();  放于for循环外    
                //推荐的做法是，每1000条数据，重新生成一次EFCONTEXT，保存一次，这样即可以避免本地EFCONTEXT占用过多内存，
                // 又可减少收发数据包的失误率，同时，SQL SERVER的解析时间短也能增加响应【推荐SaveChangesAsync异步替代方法】
                //https://www.cnblogs.com/izhaofu/p/4748563.html 
                _entityRepository.Update(_entity); 
            } 
            var count = await query.CountAsync();
            var entityListDtos = ObjectMapper.Map<List<PropertyInfoListDto>>(entityList);
            //当所做的更改被写入数据库时，异步保存可避免阻塞线程。 这有助于避免胖客户端应用程序的 UI 被冻结。 
            //异步操作还可以增加 Web 应用程序的吞吐量，可以释放线程以在数据库操作完成前去处理其他请求。 
            //有关详细信息，请参阅使用 C# 异步编程。https://docs.microsoft.com/zh-cn/ef/core/saving/async 
            await CurrentUnitOfWork.SaveChangesAsync();
            return new PagedResultDto<PropertyInfoListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 迁移名批量修改
        /// form    C#大驼峰属性名    to  mysql下划线列名
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<PropertyInfoListDto>> MigratorReName()
        {

            var query = _entityRepository.GetAll();
            var entityList = await query
              .ToListAsync();
            // TODO:根据传入的参数添加过滤条件
            foreach (var item in entityList)
            {
                var _entity = _entityRepository.FirstOrDefault(t => t.Id == item.Id); 
                if (!string.IsNullOrWhiteSpace(_entity.ColumnFullName))
                {
                    _entity.ColumnName = NamingConventions.GetColumnFirstName(_entity.ColumnFullName);
                    //暂用DomainName存放新迁移FluentAPI
<<<<<<< HEAD
                    _entity.DomainName = NamingConventions.ReplaceMigrator2(_entity.ColumnFullName, _entity.ColumnName);
=======
                    _entity.DomainName = NamingConventions.ReplaceMigrator(_entity.ColumnFullName, _entity.ColumnName); 
>>>>>>> 48297e6d6665bb68e0ff67dd1dc96755e06bfd14
                } 
                _entityRepository.Update(_entity);
            }
            var count = await query.CountAsync();
            var entityListDtos = ObjectMapper.Map<List<PropertyInfoListDto>>(entityList);
            await CurrentUnitOfWork.SaveChangesAsync();
            return new PagedResultDto<PropertyInfoListDto>(count, entityListDtos);
        }




        /// <summary>
        /// 获取PropertyInfo的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<PropertyInfoListDto>> GetPaged(GetPropertyInfosInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<PropertyInfoListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<PropertyInfoListDto>>();

			return new PagedResultDto<PropertyInfoListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取PropertyInfoListDto信息
		/// </summary>
		 
		public async Task<PropertyInfoListDto> GetById(EntityDto<int> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<PropertyInfoListDto>();
		}

		/// <summary>
		/// 获取编辑 PropertyInfo
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetPropertyInfoForEditOutput> GetForEdit(NullableIdDto<int> input)
		{
			var output = new GetPropertyInfoForEditOutput();
PropertyInfoEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<PropertyInfoEditDto>();

				//propertyInfoEditDto = ObjectMapper.Map<List<propertyInfoEditDto>>(entity);
			}
			else
			{
				editDto = new PropertyInfoEditDto();
			}

			output.PropertyInfo = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改PropertyInfo的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdatePropertyInfoInput input)
		{

			if (input.PropertyInfo.Id.HasValue)
			{
				await Update(input.PropertyInfo);
			}
			else
			{
				await Create(input.PropertyInfo);
			}
		}


		/// <summary>
		/// 新增PropertyInfo
		/// </summary>
		
		protected virtual async Task<PropertyInfoEditDto> Create(PropertyInfoEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <PropertyInfo>(input);
            var entity=input.MapTo<PropertyInfo>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<PropertyInfoEditDto>();
		}

		/// <summary>
		/// 编辑PropertyInfo
		/// </summary>
		
		protected virtual async Task Update(PropertyInfoEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			input.MapTo(entity);

			// ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除PropertyInfo信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<int> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除PropertyInfo的方法
		/// </summary>
		
		public async Task BatchDelete(List<int> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id??-1));
		}


		/// <summary>
		/// 导出PropertyInfo为excel表,等待开发。
		/// </summary>
		/// <returns></returns>
		//public async Task<FileDto> GetToExcel()
		//{
		//	var users = await UserManager.Users.ToListAsync();
		//	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
		//	await FillRoleNames(userListDtos);
		//	return _userListExcelExporter.ExportToFile(userListDtos);
		//}

    }
}


