using mecode.toolkit;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Configuration;
using Topshelf;

namespace server.console.Services
{
    /// <summary>
    ///监控服务
    /// </summary>
    public class ServiceMonitorService : ServiceControl
    {
        private IDisposable app;
        private static string domain = "http://*:8000";

        /// <summary>
        /// 静态构造函数 初始数据
        /// </summary>
        static ServiceMonitorService()
        {
            domain = ConfigurationManager.AppSettings["Domain"] ?? domain;
            Logger.Info($"获取配置:{domain}");
        }

        /// <summary>
        /// 服务启动
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            Logger.Info($"消息服务运行在:{domain}");
            Console.WriteLine($"消息服务运行在:{domain}");
            //启动一个webapp
            app = WebApp.Start(domain, builder =>
             {
                 //设置signalr跨域
                 builder.UseCors(CorsOptions.AllowAll);
                 builder.MapSignalR(new HubConfiguration
                 {
                     EnableJSONP = true,
                     EnableDetailedErrors = true,
                     EnableJavaScriptProxies = true
                 });
            });
            return true;
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Stop(HostControl hostControl)
        {
            Logger.Info($"消息服务已停止");
            Console.WriteLine($"消息服务已停止");
            app?.Dispose();
            return true;
        }
    }
}
