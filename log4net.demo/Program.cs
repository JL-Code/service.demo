using System;

namespace log4net.demo
{
    class Program
    {

        static void Main(string[] args)
        {
            LogHelper.Info("Entering application.");
            LogHelper.Debug("测试信息",new Exception("测试信息"));
            LogHelper.Warn("Warn测试信息", new Exception("测试信息"));
            LogHelper.Fatal("Fatal测试信息", new Exception("测试信息"));
            LogHelper.Error("错误", new NotImplementedException("未实现的错误"));
            LogHelper.Info("Exiting application.");
            Console.WriteLine("记录完毕");
            Console.ReadLine();
        }
    }

}
