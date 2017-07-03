using mecode.toolkit;
using mecode.toolkit.Entites;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace server.console
{
    /// <summary>
    /// 服务监控中心
    /// </summary>
    [HubName("serviceMonitorHub")]
    public class ServiceMonitorHub : Hub
    {
        private static readonly string tranferSite = "UpgradeTranferSite";

        #region Hub事件
        /// <summary>
        /// 已连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Logger.Info("signalr正在连接...");
            return base.OnConnected();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            Logger.Info("signalr正在断开连接...");
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            Logger.Info("signalr正在重新连接...");
            return base.OnReconnected();
        }
        #endregion

        #region 站点升级      
        public void UpgradeSite()
        {
            try
            {
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\upgrade.config.json";
                var upgradeInfo = AutoUpgradeManager.GetUpgradeInfo(path);
                var manager = new AutoUpgradeManager();
                manager.TryRun(upgradeInfo, (msg) =>
                {
                    Clients.All.notice(new { success = true, msg = msg, level = 1 });
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Clients.All.notice(new { success = false, msg = ex.Message, level = 4 });
            }
        }

        /// <summary>
        /// 尝试创建升级中转站
        /// </summary>
        public void TryCreateUpgradeTransferSite(Site siteInfo)
        {
            try
            {
                if (siteInfo == null)
                    throw new ArgumentNullException("siteinfo is null");
                var rootPath = AppDomain.CurrentDomain.BaseDirectory;
                siteInfo.PhysicalPath = $"{rootPath}" + siteInfo.PhysicalPath;
                IISManager.CreateSite(siteInfo);
                Clients.All.tranfersitecallback(new { success = true, msg = "站点创建成功", url = siteInfo.DefaultPage });

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Clients.All.tranfersitecallback(new { success = false, msg = ex.Message });
            }

        }

        /// <summary>
        /// 设置升级信息
        /// </summary>
        /// <param name="config"></param>
        public void SetUpgradeInfo(UpgradeConfig config)
        {
            try
            {
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\upgrade.config.json";
                var str = JsonHelper.ObjectToJSON(config);
                FileUtil.Write(path, str);
                Clients.Caller.setupgradecallback(new
                {
                    success = true,
                    site = new Site
                    {
                        Name = tranferSite,
                        PhysicalPath = "UpgradeSite",
                        Port = 3000,
                        DefaultPage = "http://127.0.0.1:3000"
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Clients.Caller.setupgradecallback(new { success = false, msg = ex.Message });
            }

        }
        #endregion

        #region 服务端调用客户端的方法

        /// <summary>
        /// 发送消息到客户端
        /// </summary>
        /// <param name="message">消息</param>
        public void Send(string message)
        {
            //Clients.All.print(message);
            Logger.Info(message);
        }
        #endregion
    }
}
