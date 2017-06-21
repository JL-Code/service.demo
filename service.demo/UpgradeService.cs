using mecode.toolkit;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.ServiceProcess;

namespace service.demo
{
    public partial class UpgradeService : ServiceBase
    {
        private IDisposable app;
        private static string domain = "http://127.0.0.1:8000";

        /// <summary>
        /// 服务启动
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Logger.Info($"服务正在启动中...");
            try
            {
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
                Logger.Info($"消息服务运行在:{domain}");
            }
            catch (Exception ex)
            {
                Logger.Error("服务器启动事发生了错误: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        protected override void OnStop()
        {
            Logger.Info($"服务已停止");
            app?.Dispose();
        }

    }
}
