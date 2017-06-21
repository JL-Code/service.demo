using server.console.Services;
using Topshelf;

namespace server.console
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(s =>
            {
                s.Service<ServiceMonitorService>();
                //设置服务显示名称
                s.SetDisplayName("Windows实时消息服务测试");
                //设置服务自动运行
                s.StartAutomatically();
            });
        }
    }
}
