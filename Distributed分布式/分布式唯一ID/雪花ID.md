#    [ 关于生成订单号规则的一些思考](https://segmentfault.com/a/1190000015497232)                                         



- ​                                       读完需要 16 分钟                                                                              

​                         

​                                                                                                                                                  

关于我为什么写这篇文章是因为今天在做订单模块的时候,看到之前的PRD上描述的年月日＋用户id2位+企业id位
＋四位自增长数。然后竟被我反驳的突然改成了精确时间＋4位自增长数，于是我更失望了。

我们考虑一下，据我所常见的订单基本都14-20位。(年月日时分秒和随机数)基本上就有14位了。虽然一般项目做不到淘宝双11这种
支付峰值达到每秒10万笔订单.但是我觉得至少事先可以考虑到，想必当初淘宝或许也没意识到以后发展
得这么好。

## 背景

对于其定订单的生成。我觉得要至少要符合以下这三种,全局唯一 ,

在复杂的分布式系统中，很多场景需要的都是全局唯一ID的场景，一般为了防止冲突可以考虑的有36
位的UUID,twitter的snowflake等。

但是可以思考这些问题？

1. 是不是应该有一些其他意义的思考，比如说订单系统有买家的id(取固定几位)
2. 是否有商品的标识,方便熟悉业务的排查问题或者查询也通过不去系统查找可以有个初步的认识，但是业务量大的话感觉就可以排除这个人为的去辨识了。
3. 个人的看法是主要是唯一，其他关于业务方面的不是太太重要。

**查阅了相关资料，主要有以下这几种**

1.UUID
组成：当前日期+时间+时钟序列+机器识别号（Mac地址或其他）没有mac网卡的话会有别的东西识别。
在分布式系统中，所有元素（WEB服务器）都不需要通过中央控制端来判断数据唯一性。几十年之内可以达到全球唯一性。
snowflake的结构如下(每部分用-分开):

2.Mysql通过AUTO_INCREMENT实现、Oracle通过Sequence序列实现。
在数据库集群环境下，不同数据库节点可设置不同起步值、相同步长来实现集群下生产全局唯一、递增ID

3.Snowflake算法 雪花算法　
41位时间戳+10位机器ID+12位序列号（自增） 转化长度为18位的长整型。
Twitter为满足美秒上万条消息的创建，且ID需要趋势递增，方便客户端排序。
Snowflake虽然有同步锁，但是比uuid效率高。

4.Redis自增ID
实现了incr(key)用于将key的值递增1，并返回结果。如果key不存在，创建默认并赋值为0。 具有原子性，保证在并发的时候。

------

但是我在这主要想说的是雪花算法生成id

**关于序列**
0 - 0000000000 0000000000 0000000000 0000000000 0 - 00000 - 00000 - 000000000000

第一位为未使用，接下来的41位为毫秒级时间(41位的长度可以使用69年)，然后是5位datacenterId和5位workerId(10位的长度最多支持部署1024个节点）  ，最后12位是毫秒内的计数（12位的计数顺序号支持每个节点每毫秒产生4096个ID序号）

一共加起来刚好64位，为一个Long型。(转换成字符串长度为18)

snowflake生成的ID整体上按照时间自增排序，并且整个分布式系统内不会产生ID碰撞（由datacenter和workerId作区分），并且效率较高。据说：snowflake每秒能够产生26万个ID。

以下是代码
部分借鉴与网络
100万个ID 耗时２秒

```java
/**
 * Created by youze on 18-7-5
 */
public class IdWorker {

    /**
     * 起始的时间戳
     */
    private final static long START_STMP = 1530795377086L;

    /**
     * 每一部分占用的位数
     */

    /**
     * 序列号占用的位数
     */
    private final static long SEQUENCE_BIT = 12;

    /**
     * 机器标识占用的位数
     */
    private final static long MACHINE_BIT = 5;

    /**
     * 数据中心占用的位数
     */
    private final static long DATACENTER_BIT = 5;

    /**
     * 每一部分的最大值
     */
    private final static long MAX_DATACENTER_NUM = -1L ^ (-1L << DATACENTER_BIT);
    private final static long MAX_MACHINE_NUM = -1L ^ (-1L << MACHINE_BIT);
    private final static long MAX_SEQUENCE = -1L ^ (-1L << SEQUENCE_BIT);

    /**
     * 每一部分向左的位移
     */
    private final static long MACHINE_LEFT = SEQUENCE_BIT;
    private final static long DATACENTER_LEFT = SEQUENCE_BIT + MACHINE_BIT;
    private final static long TIMESTMP_LEFT = DATACENTER_LEFT + DATACENTER_BIT;

    /**
     * 数据中心
     */
    private long datacenterId;

    /**
     * 机器标识
     */
    private long machineId;
    /**
     * 序列号
     */
    private long sequence = 0L;

    /**
     * 上一次时间戳
     */
    private long lastStmp = -1L;

    public IdWorker(long datacenterId, long machineId) {
        if (datacenterId > MAX_DATACENTER_NUM || datacenterId < 0) {
            throw new IllegalArgumentException("datacenterId can't be greater than MAX_DATACENTER_NUM or less than 0");
        }
        if (machineId > MAX_MACHINE_NUM || machineId < 0) {
            throw new IllegalArgumentException("machineId can't be greater than MAX_MACHINE_NUM or less than 0");
        }
        this.datacenterId = datacenterId;
        this.machineId = machineId;
    }

    /**
     * 产生下一个ID
     * @return
     */
    public synchronized long nextId() {
        long currStmp = getNewstmp();
        if (currStmp < lastStmp) {
            throw new RuntimeException("Clock moved backwards.  Refusing to generate id");
        }

        if (currStmp == lastStmp) {
            //相同毫秒内，序列号自增
            sequence = (sequence + 1) & MAX_SEQUENCE;
            //同一毫秒的序列数已经达到最大
            if (sequence == 0L) {
                currStmp = getNextMill();
            }
        } else {
            //不同毫秒内，序列号置为0
            sequence = 0L;
        }

        lastStmp = currStmp;

        return (
                //时间戳部分
                currStmp - START_STMP) << TIMESTMP_LEFT
                //数据中心部分
                | datacenterId << DATACENTER_LEFT
                //机器标识部分
                | machineId << MACHINE_LEFT
                //序列号部分
                | sequence;
    }

    private long getNextMill() {
        long mill = getNewstmp();
        while (mill <= lastStmp) {
            mill = getNewstmp();
        }
        return mill;
    }

    private long getNewstmp() {
        return System.currentTimeMillis();
    }

    public static void main(String[] args) {
        IdWorker snowFlake = new IdWorker(2, 3);
        long start = System.currentTimeMillis();
        for (int i = 0; i < 1000000; i++) {
            System.out.println(snowFlake.nextId());
        }
        System.out.println(System.currentTimeMillis() - start);
    }
}
```