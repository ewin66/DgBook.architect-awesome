







token的作用：和PC登陆的session一样，作为用户进入的唯一票据

例如：app与服务器端的接口、java与php之间不同程序的接口，这些接口一般通过json格式传输数据

所以为了保证移动端和服务端数据传输相对安全，需要对接口进行加密传输

1、token的设计目的： 
因为APP端没有和PC端一样的session机制，所以无法判断用户是否登陆，以及无法保持用户状态，所以就需要一种机制来实现session，这就是token的作用，token是用户登陆的唯一票据，只要APP传来的token和服务器端一致，就能证明你已经登陆,有权限（就和你去看电影一样，需要买票，拿着票就能进了）


第一种方式,  使用普通ajax方式

1、用户用密码登录成功后，服务器返回token给客户端；

2、客户端将token保存在本地，发起后续的相关请求时，将token发回给服务器；

3、服务器检查token的有效性，有效则返回数据，若无效，分两种情况：

token错误，这时需要用户重新登录，获取正确的token

然而，此种验证方式存在一个安全性问题：

当登录接口被劫持时，黑客就获取到了用户token，后续则可以对该用户做任何事情了

当然 你可以使用 HTTPS或者限制token的有限时间 来降低这种 被劫持的机会， 但是还是会被劫持利用


第二种方式,  使用websocket方式来进行api接口

原理:

一般websocket服务器 都会保存, 客户端的id序号(唯一的)

我们只要唯一id 把这个也作为判断的条件,  这样即使客户端token被劫持了也没有关系

工具:  workerman(websocket) + php 为例子 主要代码为下____workerman库可以去 官网下载

    <?php   
    use Workerman\Worker;  
    //引入socket库
    include __DIR__ . '/vendor/autoload.php';
    $ws_worker = new Worker("websocket://0.0.0.0:8000");
    // 只启动1个进程，这样方便测试客户端之间传输数据
    $ws_worker->count = 1;
    $ws_worker->connection_uids = array();
     
    $ws_worker->onWorkerStart=function($worker){
    	echo '开启worket start';
    };
     
    //websocket开始连接
    $ws_worker->onWebSocketConnect = function($connection , $http_header)
    {
    	echo 'open connect';
    };
     
    //客户端发送过来的消息
    $ws_worker->onMessage = function($connection, $data){
        global $ws_worker;
    	$jsonData = array();
    	$jsonData['status'] = 0;
    	$jsonData['msg'] = '未知错误';
    	//获取前端发来的数据并解析
        $clientJson = json_decode($data);
    	if(!isset($clientJson->type)){ //错误的提交
    		$jsonData['msg'] = 'error submit data';
    		$result = json_encode($jsonData);
    		$connection->send($result); 
    		return;	
    	}
    	$askType = $clientJson->type; //客户端请求类型
    	$jsonData['type'] = $askType; //保存客户端请求类型
    	$clientId = $connection->id; //当前客户端唯一的id号(这是重点)
    	//判断请求类型
        switch($askType){
            case 'login': //登录
    			//进行登录操作
    			$username = $clientJson->username; //用户名
    			$pwd = $clientJson->pwd;  //密码
    			$loginArr = qlogin($username, $pwd, $clientId); //登录验证生成客户端token和websocket token
    			$loginStatus = $loginArr['status'];
                if($loginStatus){
    				$jsonData['status'] = 1;
    				$jsonData['token'] = $loginArr['token'];
    				$jsonData['wstoken'] = $loginArr['wstoken'];
    				$jsonData['msg'] = $loginArr['msg'];				
    			}else{
    				$jsonData['status'] = 0;
    				$jsonData['msg'] = $loginArr['msg'];				
    			}
                break;
    		case 'getuserinfo': //获取用户信息
    			$bwsAuth = wsAuthVerify($clientId); //验证当前的websocket id是否和数据库一样
    			if(!$bwsAuth){ //非法提交
    				$jsonData['msg'] = 'Illegal Sources';
    				$result = json_encode($jsonData);
    				$connection->send($result); 
    				return;
    			}
    			$token = $clientJson->token;
    			//判断token是否正确
    			$userInfo = getMemberInfo($token);
    			$userStatus = $userInfo['status'];
    			if($userStatus){
    				$jsonData['status'] = 1;
    				$jsonData['token'] = $token;
    				$jsonData['type'] = $askType;
    				$userid = $userInfo['userid'];
    				$jsonData['msg'] = 'userinfo success';						
    			}else{
    				$jsonData['msg'] = $userInfo['msg'];	
    			}
    			break;
            default:
                break;
        }
    	$result = json_encode($jsonData);
    	$connection->send($result);
    	return;	
    };
    //websocket关闭
    $ws_worker->onClose = function($connection){
        global $ws_worker;
    	echo 'close';
    };


登录判断函数

判断websocket客户端是否正常

---------------------
作者：墨苍天 
来源：CSDN 
原文：https://blog.csdn.net/qq635968970/article/details/84649196 
版权声明：本文为博主原创文章，转载请附上博文链接！