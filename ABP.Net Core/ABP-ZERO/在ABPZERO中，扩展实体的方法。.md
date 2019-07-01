# 在ABPZERO中，扩展实体的方法。







# 内容

1. ## 介绍

2. ## 扩展的抽象实体

   1. ### 将新属性添加给用户

   2. ### 添加迁移

   3. ### 在界面上显示地址

   4. ### 在用户编辑/添加功能中添加地址

3. ## 扩展的非抽象类实体

   1. ### 获得版本的派生实体

   2. ### 添加迁移

   3. ### 在界面上添加价格

   4. ### 在创建/编辑版本功能中加入价格

4. ## 源代码

------

## 介绍

本教程是一步一步指南以了解如何添加新的属性，对现有的实体，从数据库层和 UI 层。

在 AspNet ZERO中的Tenant、User和Role的实体都算 抽象的，另一些则不。有一些差异。所以，我们分离它分成两个部分。

## 扩展抽象实体

我们用User实体作为例子。我们想要将address 属性添加到实体中。

### 将新属性添加到用户

打开 Authorization\Users\User.cs （在.CORE类库中） 并添加新的属性 ︰

```
public class User : AbpUser<Tenant, User>
{
    //...existing code

    public virtual string Address { get; set; }
}
```

在这里，我们隐藏了其他代码仅仅为了显示简单的用户类。
 然后您可以添加地址属性的属性。

### 添加迁移

由于我们添加新的属性，我们数据库架构已更改。不论我们改变我们的实体，我们应添加新的数据库迁移。打开控制台软件包管理器并编写新的迁移代码 ︰

```
Add-Migration "Added_Address_To_User"
```

得到一个迁移类：

```
public partial class Added_Address_To_User : DbMigration
{
    public override void Up()
    {
        AddColumn("dbo.AbpUsers", "Address", c => c.String());
    }
        
    public override void Down()
    {
        DropColumn("dbo.AbpUsers", "Address");
    }
}
```

然后更新数据库：

> Update-Database

然后打开数据库中的“AbpUsers”表，可以看到一个新的“Address”字段：



image

便于测试，我们添加了一些用户数据。

### 在UI界面上显示address字段

从Authorization\Users\UserAppService.cs (in .Application 类库中)
 获取用户列表。
 他的返回dto为“UserListDto ”。所以我们要修改他。

```
[AutoMapFrom(typeof(User))]
public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
{
    //...existing code

    public string Address { get; set; }
}
```

UserListDto 是通过automapper自动映射的，所以不需要修改UserAppService.GetUsers方法.
 然后我们去ui层，路径Web\App\common\views\users\index.js 添加name属性为“address” 的值。

然后运行项目，然后打开用户列表：



44

上面的例子是通过SPA来演示的。如果要使用MPA，操作也是类似的，只要打开WEB项目中的Web\Areas\Mpa\Views\Users\index.js，添加字段就可以。

### 在添加和编辑页面上添加address

客户端使用 UserAppService.GetUserForEdit方法来编辑窗体上显示用户信息。它返回GetUserForEditOutput对象，其中包含一个UserEditDto对象，包括用户属性。所以，我们应该将地址添加到 UserEditDto，以允许客户端上创建更新地址属性更改 ︰

```
public class UserEditDto : IValidate, IPassivable
{
    //...existing code

    public string Address { get; set; }
}
```

然后打开路径"Web\App\common\views\users\createOrEditModal.cshtml"
 添加以下代码

```
<div class="form-group form-md-line-input form-md-floating-label no-hint">
    <input type="text" name="Address" class="form-control" ng-class="{'edited':vm.user.address}" ng-model="vm.user.address">
    <label>@L("Address")</label>
</div>
```

然后运行项目：



image

此处我们没有使用本地化文本，如果要启用的话，Core类库中\Localization\ExtendEntitiesDemo中的XML文件打开。

## 拓展非抽象实体

我们拿Edition实体作为示例

### 获得Edition的派生实体

由于 Edition不是抽象对象，我们无法给他添加新属性。但是我们可以使用OOP模式中的
 继承和组合（inheritance or composition）。

我们使用简单的继承，创建一个新类MyEdition继承Edition。

```
public class MyEdition : Edition
{
    public virtual int Price { get; set; }
}
```

### 添加迁移

添加迁移

由于我们添加一个新的实体类，我们数据库架构已更改。不论我们改变我们的实体，我们应添加新的数据库迁移。打开控制台软件包管理器并编写新的迁移代码 ︰

> Add-Migration "Added_MyEdition_Entity"

这将创建一个新的实体框架迁移类，如下所示 ︰

```
public partial class Added_MyEdition_Entity : DbMigration
{
    public override void Up()
    {
        AlterTableAnnotations(
            "dbo.AbpEditions",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false, maxLength: 32),
                DisplayName = c.String(nullable: false, maxLength: 64),
                IsDeleted = c.Boolean(nullable: false),
                DeleterUserId = c.Long(),
                DeletionTime = c.DateTime(),
                LastModificationTime = c.DateTime(),
                LastModifierUserId = c.Long(),
                CreationTime = c.DateTime(nullable: false),
                CreatorUserId = c.Long(),
                Price = c.Int(),
                Discriminator = c.String(nullable: false, maxLength: 128),
            },
            annotations: new Dictionary<string, AnnotationValues>
            {
                {
                    "DynamicFilter_MyEdition_SoftDelete",
                    new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                },
            });

        AddColumn("dbo.AbpEditions", "Price", c => c.Int(nullable: false, defaultValue: 0));
        AddColumn("dbo.AbpEditions", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "MyEdition"));
    }

    public override void Down()
    {
        //...other code
    }
}
```

实际上，AbpEditions 迁移中，添加两个新字段：

- Price: 这个是我们添加到MyEdition中的价格字段
- Discriminator： EF实体框架来区别Edition和MyEdition的区别（自动创建的继承）

在更新数据库之前，我们需要改下默认的迁移代码：

```
AddColumn("dbo.AbpEditions", "Price", c => c.Int());
AddColumn("dbo.AbpEditions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
```

修改为：

```
AddColumn("dbo.AbpEditions", "Price", c => c.Int(nullable: false, defaultValue: 0));
AddColumn("dbo.AbpEditions", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "MyEdition"));
```

这样做的目的是为了让MyEdition替换为现有的Edition实体。

> Update-Database

打开表“AbpEditions”看到的新字段：



image

然后我们可以看到现有的标准版的价格被MyEdition修改为0。

有关迁移的最后一件事情就是 Seed Code 中。我们需要进行修改EntityFramework\Migrations\Seed\DefaultEditionCreator.cs：

```
defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
```

修改为

```
defaultEdition = new MyEdition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
```

因此，我们创建新数据库的时候，会创建MyEdition实体。

### 在UI界面上显示价格

从application类库中打开Editions\EditionAppService.cs ，调用getlist方法。
 返回值是：EditionListDto （ABPZERO只会用DTO进行客户端之间的通信）。

所以我们的需要把价格属性添加到“EditionListDto”中：

```
[AutoMapFrom(typeof(Edition), typeof(MyEdition))]
public class EditionListDto : EntityDto, IHasCreationTime
{
    //...existing code

    public int Price { get; set; }
}
```

然后就是automapper自动映射，不用进行处理。

然后打开WEB类库中的“Web\App\host\views\editions\index.js”，添加name属性为“Price”:

```
{
    name: app.localize('EditionName'),
    field: 'displayName'
},
{
    name: app.localize('Price'),
    field: 'price'
},
{
    name: app.localize('CreationTime'),
    field: 'creationTime',
    cellFilter: 'momentFormat: \'L\''
}
```

然后运行项目：

作者：角落的白板笔

链接：https://www.jianshu.com/p/96361b3bdb90

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。