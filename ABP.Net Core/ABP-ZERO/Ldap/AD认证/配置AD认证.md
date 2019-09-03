# 配置AD认证

------

如果您的组织使用Microsoft Active Directory作为统一用户管理系统，则Rancher可以集成Active  Directory服务以进行统一身份验证。Rancher根据Active  Directory管理的用户和组来控制对集群和项目的访问，同时允许最终用户在登录Rancher UI时使用其AD凭据进行身份验证。

Rancher使用LDAP与Active Directory服务通信。因此，Active Directory的身份验证流程与[OpenLDAP](https://www.rancher.cn/docs/rancher/v2.x/cn/configuration/admin-settings/authentication/openldap)身份验证集成方法相同。

> **注意** 在开始之前，请熟悉[外部身份验证配置和主要用户](https://www.rancher.cn/docs/rancher/v2.x/cn/configuration/admin-settings/authentication/#外部身份验证配置和主要用户)的概念。

## 一、先决条件

您需要通过AD管理员创建或获取新的AD用户，以用作Rancher的`服务帐户`。此用户必须具有足够的权限才能执行LDAP搜索并读取AD域下的用户和组的属性。

通常应该使用域用户帐户(非管理员)来实现此目的，因为默认情况下此用户对域中的大多数对象具有只读权限。

但请注意，在某些锁定的Active Directory配置中，此默认行为可能不适用。在这种情况下，您需要确保服务帐户用户至少具有在基本OU(封闭用户和组)上授予的`读取和列出内容`权限，或者全局授予域。

> **使用TLS？** 如果AD服务器使用的证书是自签名的，或者不是来自公认的证书颁发机构，请确保手头有PEM格式的CA证书(与所有中间证书连接)。您必须在配置期间设置此证书，以便Rancher能够验证证书。

## 二、选择Active Direct