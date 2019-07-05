# vue自动保存表单功能的实现

​             ![96](assets/f8e90262-cbb8-4263-b031-9faa7d490a23.png) 

​             [爱余星痕](https://www.jianshu.com/u/8db2a00af5c8)                          

​                                               2018.10.03 14:13               字数 99             阅读 406评论 0喜欢 4

最近想实现一个表单内容的自动保存,原来是想通过监听表单的change事件来解决.但后面想想,现在都是数据驱动了,监听数据变化就行了.
 页面表单如下:



https://www.jianshu.com/p/8b661c32fa6e

https://blog.csdn.net/weixin_33709609/article/details/87053736

```
   <el-form label-width="100px" :model="modalFormData" ref="modalFormData" :rules="formAddRules">
                        <el-form-item prop="name" label="控件ID：" v-if="typeof modalFormData.name != 'undefined'">
                            <el-input v-model="modalFormData.name" required placeholder=""></el-input>
                        </el-form-item>
                        ....
                        
 </el-form>
```

按上所述,我只要监听modalFormData的变化即可
 监听方法如下:

```
watch: {
       
            modalFormData:{
                //注意：当观察的数据为对象或数组时，curVal和oldVal是相等的，因为这两个形参指向的是同一个数据对象
                handler(curVal,oldVal){
                   // 自动保存方法
                  this.autoSave();
                },
                deep:true
            }
        }
```

至此,自动保存表单的功能已完成!