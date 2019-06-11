# token那些事儿

2018年10月09日 09:31:15           [Mr丶D](https://me.csdn.net/cn_1937)           阅读数：303                                                                  

​                   

 版权声明：本文为博主原创文章，如需转载请注明出处。

​     https://blog.csdn.net/cn_1937/article/details/82977485        

## 一、token的介绍

#### 接口特点汇总：

- 因为是非开放性的，所以所有的接口都是封闭的，只对公司内部的产品有效；
- 因为是非开放性的，所以OAuth那套协议是行不通的，因为没有中间用户的授权过程；
- 有点接口需要用户登录才能访问；
- 有点接口不需要用户登录就可访问；

针对以上特点，移动端与服务端的通信就需要2把钥匙，即2个token。
 第一个token是针对接口的（api_token）；
 第二个token是针对用户的（user_token）；

##### 先说第一个token（api_token）

它的职责是保持接口访问的隐蔽性和有效性，保证接口只能给自家人用，怎么做到？

###### 参考思路如下：

现在的接口基本是mvc模式，URL基本是restful风格，URL大体格式如下：
 <http://blog.snsgou.com/模块名/控制器名/方法名?参数名1=参数值1&参数名2=参数值2&参数名3=参数值3>

###### 接口token生成规则参考如下：

```
api_token = md5 ('模块名' + '控制器名' + '方法名' + '2017-07-18' + '加密密钥') = 770fed4ca2aabd20ae9a5dd774711de2
1
```

###### 其中 

- ‘2013-12-18’ 为当天时间，
- ‘加密密钥’ 为私有的加密密钥，手机端需要在服务端注册一个“接口使用者”账号后，系统会分配一个账号及密码，数据表设计参考如下：<

| 字段名        | 字段类型    | 注释             |
| ------------- | ----------- | ---------------- |
| client_id     | varchar(20) | 客户端ID         |
| client_secret | varchar(20) | 客户端(加密)密钥 |

（注：只列出了核心字段，其它的再扩展吧！！！）

##### 服务端接口校验，PHP实现流程如下：

1. 获取GET参数值

   $module = $_GET[‘mod’];
    $controller = $_GET[‘ctl’];
    $action = $_GET[‘act’];>
    $client_id = $_GET[‘client_id’];
    $api_token = $_GET[‘api_token’];

2. 根据客户端传过来的client_id，查询数据库，获取对应的client_secret。

   ```
   $client_secret = getclientSecretById($client_id);
   1
   ```

3. 服务器重新生成一份api_token
    `$api_token_server = md5($module.$controller.$action.date('Y-m-d', time()).$client_secret)`;

4. 客户端传过来的api_token与服务器生成的api_token进行校对，如果不相等，则表示验证失败。

   if($api_token != $api_token_server)
    exit(‘access deny’);

5. 验证通过，返回数据到客户端。

#### 再说第二个token（user_token）

它的职责是保护用户的用户名及密码多次提交，以防密码泄露。
 如果接口需要用户登录，其访问流程如下：
 1、用户提交“用户名”和“密码”，实现登录（条件允许，这一步最好走https）；
 2、登录成功后，服务端返回一个 user_token，生成规则参考如下：

##### 服务端用数据表维护user_token的状态，表设计如下：

| 字段名      | 字段类型    | 注释                   |
| ----------- | ----------- | ---------------------- |
| user_id     | int         | 用户ID                 |
| user_token  | varchar(36) | 用户token              |
| expire_time | int         | 过期时间（Unix时间戳） |

（注：只列出了核心字段，其它的再扩展吧！！！）

 服务端生成 user_token 后，返回给客户端（自己存储），客户端每次接口请求时，如果接口需要用户登录才能访问，则需要把 user_id 与 user_token 传回给服务端，服务端接受到这2个参数后，需要做以下几步： 

- 检测 api_token的有效性；<
- 删除过期的 user_token 表记录；
- 根据 user_id，user_token 获取表记录，如果表记录不存在，直接返回错误，如果记录存在，则进行下一步；
- 更新 user_token 的过期时间（延期，保证其有效期内连续操作不掉线）；

###### 使用token防止表单重复提交

如何防止表单重复提交？ 在前台页面中放置一个隐藏域用于存放session中的token，当第一次提交时验证token相同后，会将session中的token信息更新，页面重复提交时，因为表单中的token值没有更新，所以提交失败。<
 此外，要避免“加token但不进行校验”的情况，在session中增加了token，但服务端没有对token进行验证，根本起不到防范的作用。<

###### 如何防止token劫持？

token肯定会存在泄露的问题。比如我拿到你的手机，把你的token拷出来，在过期之前就都可以以你的身份在别的地方登录。

###### 解决这个问题的一个简单办法

- 在存储的时候把token进行[对称加密](https://www.baidu.com/s?wd=对称加密&tn=44039180_cpr&fenlei=mv6quAkxTZn0IZRqIHckPjm4nH00T1YzPHbLmWI-n1wbm1u-rA7b0ZwV5Hcvrjm3rH6sPfKWUMw85HfYnjn4nH6sgvPsT6KdThsqpZwYTjCEQLGCpyw9Uz4Bmy-bIi4WUvYETgN-TLwGUv3EnHbYrHRknjmYPjTdnWcLPjDYr0)存储，用时解开。
- 将请求URL、时间戳、token三者进行合并加盐签名，服务端校验有效性。

这两种办法的出发点都是：窃取你存储的数据较为容易，而反汇编你的程序hack你的加密解密和签名算法是比较难的。然而其实说难也不难，所以终究是防君子不防小人的做法。

## 二、python中token的具体生成与使用

**原理:** 
 通过hmac sha1 算法产生用户给定的key和token的最大过期时间戳的一个消息摘要，将这个消息摘要和最大过期时间戳通过”:”拼接起来，再进行base64编码，生成最终的token 
 **实现:**

```
import time
import base64
import hmac

def generate_token(key, expire=3600):
    r'''
        @Args:
            key: str (用户给定的key，需要用户保存以便之后验证token,每次产生token时的key 都可以是同一个key)
            expire: int(最大有效时间，单位为s)
        @Return:
            state: str
    '''
    ts_str = str(time.time() + expire)
    ts_byte = ts_str.encode("utf-8")
    sha1_tshexstr  = hmac.new(key.encode("utf-8"),ts_byte,'sha1').hexdigest() 
    token = ts_str+':'+sha1_tshexstr
    b64_token = base64.urlsafe_b64encode(token.encode("utf-8"))
    return b64_token.decode("utf-8")

12345678910111213141516171819
```

## 3.验证token

**原理:** 
 将token进行base64解码,通过token得到token最大过期时间戳和消息摘要。判断token是否过期。 
 如没过期才将 从token中的取得最大过期时间戳进行hmac sha1 算法运算(**注意这里的key要与产生token的key要相同**)，最后将产生的摘要与通过token取得消息摘要进行对比， 如果两个摘要相等，则token有效，否则token无效 。 
 **实现：**

```
import time
import base64
import hmac

def certify_token(key, token):
    r'''
        @Args:
            key: str
            token: str
        @Returns:
            boolean
    '''
    token_str = base64.urlsafe_b64decode(token).decode('utf-8')
    token_list = token_str.split(':')
    if len(token_list) != 2:
        return False
    ts_str = token_list[0]
    if float(ts_str) < time.time():
        # token expired
        return False
    known_sha1_tsstr = token_list[1]
    sha1 = hmac.new(key.encode("utf-8"),ts_str.encode('utf-8'),'sha1')
    calc_sha1_tsstr = sha1.hexdigest()
    if calc_sha1_tsstr != known_sha1_tsstr:
        # token certification failed
        return False 
    # token certification success
    return True 
12345678910111213141516171819202122232425262728
```

#### 4.用法

```
key = “JD98Dskw=23njQndW9D” 
# 一小时后过期 
token = generate_token(key, 3600)

certify_token(key, token)
12345
```

## 5.Note!!!

本代码只能在python3.x 中运行，