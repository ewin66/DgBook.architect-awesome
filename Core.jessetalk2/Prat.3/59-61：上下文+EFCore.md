

微服务拆分和上下文的确定

项目



身份与访问



项目推荐

通讯录

a 

消息	元数据

##	原界限上下文



![1562685568757](assets/1562685568757.png)



查询的时候	跨服务查询，难以管理

或者权衡不频繁的变更【几乎不修改】	来做冗余

​				用户信息变更的时候进行同步

衍生

##	丰富界限上下文



![1562765619985](assets/1562765619985.png)







##	User



###	User-Info

- Project
- VisibleRule
- Viewer
- ProjectContributor

![1562716155879](assets/1562716155879.png)

Key-Value

Company

Gender

省市县

![1562766032643](assets/1562766032643.png)

###	**UserProperty**	

userId

key

Text

Value

![1562766221783](assets/1562766221783.png)





###	**User	TagsData**



![1562766298197](assets/1562766298197.png)



###	BPFile

Id

FileName

![1562765522092](assets/1562765522092.png)



###	UserContext` modelBuilder

![1562766832475](assets/1562766832475.png)



###	Docker -mysql

![1562766985188](assets/1562766985188.png)





![1562767058462](assets/1562767058462.png)



![1562767084241](assets/1562767084241.png)

##	User	Contact

![1562716257392](assets/1562716257392.png)







业务规模拆开	适合业务则为最好的



##	Contact



![1562733740734](assets/1562733740734.png)

## Project



- Project
- VisibleRule
- Viewer
- ProjectContributor
  - 项目类型，投资类型

![1562733792966](assets/1562733792966.png)



![1562716101620](assets/1562716101620.png)









##	Users接口大纲



![1562769227984](assets/1562769227984.png)







![1562769319247](assets/1562769319247.png)













![1562769553964](assets/1562769553964.png)



###	Get	Patch



![1562769781602](assets/1562769781602.png)



![1562769967747](assets/1562769967747.png)



###	AsNoTracking	无需追踪

UserIdentity	用户

【63	用户信息-Dto	Model 】

![1562770113688](assets/1562770113688.png)



