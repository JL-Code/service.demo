using System.ServiceProcess;

namespace service.demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new UpgradeService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
