using mecode.toolkit.Entites;
using System.Collections.Generic;
using System.Diagnostics;

namespace mecode.toolkit
{
    /// <summary>
    /// 自动升级管理
    /// </summary>
    public class AutoUpgradeManager
    {
        /// <summary>
        /// 运行站点升级
        /// </summary>
        /// <param name="info">站点升级信息</param>
        public void Run(UpgradeInfo info)
        {
            //1.解压升级包
            UnPack(info.ZipFilePath);
            //2.找出并执行sql脚本
            ExecSqlUtil.ExcuteSqlFile($"{info.DecompressionPath}\\DbScript\\{info.UpgradeVersion}.sql", info.DbConnStr);
            //3.将升级包复制替换到网站目录
            StopIISSite(info.MainSite.Name);
            //Debug.WriteLine("开始复制文件...");
            //CopyItem(info.CopyPath, info.MainSite.PhysicalPath);
            //Debug.WriteLine("新建IIS站点...");
            //NewIISSite(info.MainSite);
            //StartIISSite(info.MainSite.Name);
            //OpenDefaultBrower(info.MainSite.DomainName);
        }

        /// <summary>
        /// 创建一个IIS站点
        /// </summary>
        /// <param name="source">站点源文件路径</param>
        /// <param name="site">站点信息</param>
        public void CreateIISSite(string source, Site site, bool open = false)
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
        public void CreateIISSite(Site site, bool open = false)
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
        public void CopyItem(string source, string destination)
        {
            var script = $"Copy-Item -Path \"{source}\" -Destination \"{destination}\" -Recurse -Force";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"站点文件复制: {script} 返回信息: {result}");
        }

        public void TryCopyItem(string source, string destination)
        {
            var script = $"$result = Copy-Item -Path \"{source}\" -Destination \"{destination}\" -Recurse -Force -WhatIf;$result";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"站点文件复制: {script} 返回信息: {result}");
        }

        /// <summary>
        /// 创建一个新站点
        /// </summary>
        /// <param name="siteInfo">站点信息</param>
        private void NewIISSite(Site siteInfo)
        {
            var script = $"New-Website -Name {siteInfo.Name} -Force -HostHeader {siteInfo.HostName} -IPAddress {siteInfo.IPAddress} -PhysicalPath {siteInfo.PhysicalPath} -Port {siteInfo.Port}";
            Logger.Info($"创建站点中: {script}");
            PowerShellUtil.RunScript(script);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sourcePath"></param>
        private void UnPack(string source, string destination = null)
        {
            CompressUtil.DecompressFile(source, destination);
        }

        /// <summary>
        /// 停止站点
        /// </summary>
        /// <param name="name">站点名</param>
        private void StopIISSite(string name)
        {
            //var script = $"$e = get-website {name}; if ($e.Count -eq 1) {{ Stop-Website {name}; }}";
            var script = @"Get-IISSite " + name + "| Where-Object { if ($_.Name -ne ''){Stop-IISSite $_.Name -Confirm:$false; Get-IISSite $_.Name;}};";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"停止站点: {result}");
        }
        /// <summary>
        /// 启动站点
        /// </summary>
        /// <param name="name">站点名</param>
        private void StartIISSite(string name)
        {
            var script = $"Start-Website \"{name}\";Get-Website -name \"{name}\"";
            var result = PowerShellUtil.RunScript(script);
            Logger.Info($"启动站点: {result}");
        }

        /// <summary>
        /// 打开默认浏览器 
        /// </summary>
        /// <param name="url">访问地址</param>
        private void OpenDefaultBrower(string url)
        {
            var script = $"Start-Process -FilePath {url}";
            PowerShellUtil.RunScript(script);
            Logger.Info($"正在通过默认浏览器访问: {url}");
        }
    }
}
