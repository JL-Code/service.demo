using mecode.toolkit.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mecode.toolkit
{
    public class IISManager
    {
        #region 站点操作
        /// <summary>
        /// 创建一个IIS站点
        /// </summary>
        /// <param name="source">站点源文件路径</param>
        /// <param name="site">站点信息</param>
        public static void CreateIISSite(string source, Site site, bool open = false)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Path", source },
                { "Destination",site.PhysicalPath},
                { "Recurse",true},
                { "Force",true}
            };
            PowerShellUtil.Run("Copy-Item", parameters);
            NewIISSite(site);
            StartIISSite(site.Name);
            if (!string.IsNullOrEmpty(site.DefaultPage) && open)
                OpenDefaultBrower(site.DefaultPage);
        }

        /// <summary>
        /// 创建一个IIS站点
        /// </summary>
        /// <param name="site">站点信息</param>
        public static void CreateSite(Site site, bool open = false)
        {
            NewIISSite(site);
            StartIISSite(site.Name);
            if (!string.IsNullOrEmpty(site.DefaultPage) && open)
                OpenDefaultBrower(site.DefaultPage);
        }

        /// <summary>
        /// 复制文件或目录
        /// </summary>
        /// <param name="source">源文件路径</param>
        /// <param name="destination">目标文件路径</param>
        public static void CopyItem(string source, string destination)
        {
            var script = $"Copy-Item -Path \"{source}\" -Destination \"{destination}\" -Recurse -Force";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"站点文件复制: {script} 返回信息: {result}");
        }

        public static void TryCopyItem(string source, string destination)
        {
            var script = $"$result = Copy-Item -Path \"{source}\" -Destination \"{destination}\" -Recurse -Force -WhatIf;$result";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"站点文件复制: {script} 返回信息: {result}");
        }

        /// <summary>
        /// 创建一个新站点
        /// </summary>
        /// <param name="siteInfo">站点信息</param>
        public static void NewIISSite(Site siteInfo)
        {
            RemoveSite(siteInfo.Name);
            var script = $"New-Website -Name \"{siteInfo.Name}\" -Force -HostHeader {siteInfo.HostName} -IPAddress {siteInfo.IPAddress} -PhysicalPath \"{siteInfo.PhysicalPath}\" -Port {siteInfo.Port}";
            Logger.Info($"创建站点中: {script}");
            PowerShellUtil.RunScript(script);
        }

        /// <summary>
        /// 删除站点
        /// </summary>
        /// <param name="siteName">站点名称</param>
        public static void RemoveSite(string siteName)
        {
            var script = $"Remove-Website -Name \"{siteName}\" -Confirm:$false";
            Logger.Info($"移除站点中: {script}");
            PowerShellUtil.RunScript(script);
        }

        /// <summary>
        /// 停止站点
        /// </summary>
        /// <param name="name">站点名</param>
        public static void StopIISSite(string name)
        {
            var script = @"Get-WebSite " + name + " | Where-Object { if ($_.Name -ne \"\"){Stop-WebSite $_.Name; Get-WebSite $_.Name;}};";
            //var script = @"Get-IISSite " + name + "| Where-Object { if ($_.Name -ne ''){Stop-IISSite $_.Name -Confirm:$false; Get-IISSite $_.Name;}};";
            Logger.Info($"停止站点: {script}");
            var result = PowerShellUtil.RunScript(script);
        }
        /// <summary>
        /// 启动站点
        /// </summary>
        /// <param name="name">站点名</param>
        public static void StartIISSite(string name)
        {
            var script = $"Start-Website \"{name}\";Get-Website -name \"{name}\"";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"启动站点: {result}");
        }

        /// <summary>
        /// 打开默认浏览器 
        /// </summary>
        /// <param name="url">访问地址</param>
        public static void OpenDefaultBrower(string url)
        {
            var script = $"Start-Process -FilePath {url}";
            PowerShellUtil.RunScript(script);
            Logger.Info($"正在通过默认浏览器访问: {url}");
        }
        #endregion
    }
}
