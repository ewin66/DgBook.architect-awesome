import random
import math
import json
import logging

import redis
import requests
from requests.exceptions import ConnectionError, ReadTimeout
from scrapy.conf import settings
from houses_dictionary_spider.utils.thread_pool import ThreadPool

logger = logging.getLogger(__name__)


class ProxyManager:
    test_url = ['https://bj.58.com/', 'https://guangzhou.anjuke.com//','https://sh.ke.com/']
    TIMEOUT = 5
    def __init__(self):
        self.redis_connector = redis.Redis(host=settings['REDIS_HOST'], port=settings['REDIS_PORT'],
                                           db=settings['REDIS_PROXY_DB'])

    def test_proxy(self, ip, port, *thread_pool_arguments):
        if thread_pool_arguments and isinstance(thread_pool_arguments[0], ThreadPool):
            thread_pool_arguments[0].add_thread()
        proxies = {'https': '{}:{}'.format(ip, port)}
        logger.info(f'正在测试代理ip{ip},端口为{port}')
        connect_flag = False
        try:
            requests.get("https://icanhazip.com/", proxies=proxies, timeout=TIMEOUT)

            requests.get(random.choice(test_url), proxies=proxies, timout=TIMEOUT)
     
        except (ConnectionError, ReadTimeout) as error:
            logger.info(error)
            self.decline_priority(ip=ip, port=port)
        else:
            if ip == response.text.strip():
                logger.info(f'测试代理ip{ip},端口为{port},状态为{response.status_code}')
                connect_flag = True
                self.redis_connector.hincrby('proxy', json.dumps({"ip": ip, 'port': port}), 1)
            else:
                logger.info(f'当前IP代理不可用{ip}:{response.text().strip()}')
        finally:
            return connect_flag

    def get_proxy(self):
        proxy_pool = self.redis_connector.hgetall('proxy')
        proxy_pool_length = len(proxy_pool)
        logger.info(f'现有代理ip个数为:{proxy_pool_length}')
        available_proxy = None
        if proxy_pool_length:
            available_proxy = json.loads(
                random.choice(sorted(proxy_pool.items(), key=lambda x: int(x[1].decode()), reverse=True)[
                              :math.floor(proxy_pool_length * 0.2)])[0])
            if self.test_proxy(ip=available_proxy["ip"], port=available_proxy["port"]):
                logger.info(f"可用代理{available_proxy}")
            else:
                self.get_proxy()
        return available_proxy

    def decline_priority(self, ip, port):
        logger.info(f'ip:{ip},port:{port}无法使用代理')
        proxy = json.dumps({"ip": ip, 'port': port})
        if int(self.redis_connector.hget('proxy', proxy).decode()) > 0:
            logger.info(f'降低此代理优先级')
            self.redis_connector.hincrby('proxy', proxy, -1)
        else:
            logger.info(f'此代理已经无法正常使用,正在删除此代理')
            self.redis_connector.hdel('proxy', proxy)
