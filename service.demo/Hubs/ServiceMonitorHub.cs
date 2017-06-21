﻿using mecode.toolkit;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace server.demo.Hubs
{
    /// <summary>
    /// 服务监控中心
    /// </summary>
    [HubName("serviceMonitorHub")]
    public class ServiceMonitorHub : Hub
    {
        /// <summary>
        /// 静态构造函数 获取所有的windwos服务信息
        /// </summary>
        static ServiceMonitorHub()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    //获取所有服务名称以Hyper开头的服务
                    var services = ServiceController
                    .GetServices()
                    .Where(t => t.ServiceName.StartsWith("Hyper"))
                    .Select(t => new Entities.Service
                    {
                        DisplayName = t.DisplayName,
                        ServiceName = t.ServiceName,
                        Status = (int)t.Status
                    });
                    GlobalHost.ConnectionManager.GetHubContext<ServiceMonitorHub>().Clients.All.refresh(services);
                    //休眠3秒，实现每3秒推送服务运行状态
                    Thread.Sleep(3000);
                }
            }).Start();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Info(string message)
        {
            Logger.Info(message);
        }
        /// <summary>
        /// 停止站点
        /// </summary>
        /// <param name="site">站点名称</param>
        public void StopIISSite(string site)
        {
            try
            {
                var script = $"Stop-IISSite -Name {site} -Confirm:$false;Get-IISSite";
                var result = PowerShellUtil.RunScript(script);
                Logger.Info(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }
        /// <summary>
        /// 启动站点
        /// </summary>
        /// <param name="site">站点名称</param>
        public void StartIISSite(string site)
        {
            try
            {
                var result = PowerShellUtil.RunScript($"Start IISSite {site};Get-IISSite");
                Logger.Info(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }
    }
}
