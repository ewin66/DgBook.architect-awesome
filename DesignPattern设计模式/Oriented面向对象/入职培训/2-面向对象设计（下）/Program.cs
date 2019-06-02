using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Dynamic;
using System.Xml.Linq;

namespace ObjectOriented
{
    class Program
    {
        static void Main(string[] args)
        {
            //==================================================
            // 不好的命名：
            var s = "Frank";

            // 用户名   <= 不需要的备注
            var s1 = "Frank";

            // 拼音命名
            var mingzi = "Frank";
            var nianling = 30;

            // 错误的匈牙利命名
            var iAge = 30;

            //==================================================

            // 比较好的命名，也是正确的匈牙利命名：准确清晰的描述变量的用途
            var user_name = "Frank";
            // 或者
            var userName = "Frank";

            //==================================================

            Console.ReadLine();
        }

        private static bool IsAuthorized()
        {
            return true;
        }

        private static int GetLevel()
        {
            return 5;
        }

        private static int GetSalary()
        {
            return 1000;
        }

        //重构1：抽取变量
        private void Demo1()
        {
            //==================================================
            // 抽取变量
            int salary = GetSalary();
            int level = GetLevel();
            bool authorized = IsAuthorized();
            // 工资大于5000的人，或者会员等级高于10的人，或者是工资不高不低、等级不上不下但是经过特别授权的人
            // 可以进入
            if (salary > 5000 || level > 10 || (salary > 4000 && level > 8 && authorized))
            {
                //...
            }

            // 重构后的代码
            bool isHightSalary = salary > 5000;
            bool isHightLevel = level > 10;
            bool isMiddleButAllowed = salary > 4000 && level > 8 && authorized;
            if (isHightSalary || isHightLevel || isMiddleButAllowed)
            {
                //...
            }
        }

        // 重构2：抽取函数
        private void OnLoginClicked(object sender, EventArgs e)
        {
            // 启动一些登录效果
            // 10行代码
            //...
            // 检查必须录入的数据是否合法
            // 10行代码
            //...
            // 检查电子邮箱是否存在
            // 5行代码
            //...
            // 检查账号和密码
            // 100行代码
            //...
            // 处理异常
            // 20行代码
            //...
            // 处理检查账号和密码的结果，弹出相应的错误或警告
            // 30行代码
            //...
            // 更新登录记录和软件的其他一些登录相关数据
            // 40行代码
            //...
        }

        // 重构后的代码
        private void OnLoginClicked(object sender, EventArgs e)
        {
            UpdateUI();
            ValidateInputs();
            CheckUser();
            HandleExceptions();
            HandleCheckResult();
            UpdateLoginRecords();
        }

        private void UpdateUI()
        {
            // 启动一些登录效果
            // 10行代码
            //...
        }
        private void ValidateInputs()
        {
            // 检查必须录入的数据是否为空
            // 10行代码
            //...
        }
        private void CheckUser()
        {
            // 检查电子邮箱是否存在
            // 5行代码
            //...
            // 检查账号和密码
            // 40行代码
            //...
        }
        private void HandleExceptions()
        {
            // 处理异常
            // 10行代码
            //...
        }
        private void HandleCheckResult()
        {
            // 处理检查账号和密码的结果，弹出相应的错误或警告
            // 20行代码
            //...
        }
        private void UpdateLoginRecords()
        {
            // 更新登录记录
            // 10行代码
            //...
        }


    }

    // 重构3：抽取对象
    class Helper
    {
        #region Query Names
        public List<string> QueryAllNames()
        {
            var names = QueryNames(true);
            return names;
        }

        private List<string> QueryNames(bool queryValidUsers)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Query Ages
        public List<int> QueryAllAges()
        {
            var ages = QueryAges(false);
            return ages;
        }

        private List<int> QueryAges(bool queryValidUsers)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update Name
        public void UpdateName(int id, string name)
        {
            //检查一些条件，然后更新
            UpdateDBName(id, name);
        }

        private void UpdateDBName(int id, string name)
        {
            //...
        }
        #endregion

        #region Update Age
        public void UpdateAge(int id, int age)
        {
            //检查一些条件，然后更新
            UpdateDBAge(id, age);
        }

        private void UpdateDBAge(int id, int age)
        {
            //...
        }
        #endregion
    }

    // 重构后的代码
    class NameQueryHelper
    {
        public List<string> QueryAllNames()
        {
            var names = QueryNames(true);
            return names;
        }

        private List<string> QueryNames(bool queryValidUsers)
        {
            throw new NotImplementedException();
        }
    }

    class NameUpdateHelper
    {
        public void UpdateName(int id, string name)
        {
            //检查一些条件，然后更新
            UpdateDBName(id, name);
        }

        private void UpdateDBName(int id, string name)
        {
            //...
        }
    }

    class AgeQueryHelper
    {
        public List<int> QueryAllAges()
        {
            var ages = QueryAges(false);
            return ages;
        }

        private List<int> QueryAges(bool queryValidUsers)
        {
            throw new NotImplementedException();
        }
    }

    class AgeUpdateHelper
    {
        public void UpdateAge(int id, int age)
        {
            //检查一些条件，然后更新
            UpdateDBAge(id, age);
        }

        private void UpdateDBAge(int id, int age)
        {
            //...
        }
    }

    // 重构4：抽象基类
    // HouseDemand三个类的重构：继承与多态的应用
}
