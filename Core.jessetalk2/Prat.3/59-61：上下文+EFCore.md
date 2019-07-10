

微服务拆分和上下文的确定

项目



身份与访问



项目推荐

通讯录

a 

消息	元数据



![1562685568757](assets/1562685568757.png)



查询的时候	跨服务查询，难以管理

或者权衡不频繁的变更【几乎不修改】	来做冗余

​				用户信息变更的时候进行同步

衍生





##	User





- Project
- VisibleRule
- Viewer
- ProjectContributor

![1562716155879](assets/1562716155879.png)



![1562716257392](assets/1562716257392.png)







业务规模拆开	适合业务则为最好的



![1562733740734](assets/1562733740734.png)

## Project



- Project
- VisibleRule
- Viewer
- ProjectContributor
  - 项目类型，投资类型

![1562733792966](assets/1562733792966.png)



![1562716101620](assets/1562716101620.png)



Key-Value