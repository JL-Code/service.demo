using mecode.toolkit.Entites;
using System;
using System.Collections.Generic;

namespace mecode.toolkit
{
    /// <summary>
    /// 自动升级管理
    /// </summary>
    public class AutoUpgradeManager
    {

        public void Run(UpgradeInfo info, Action<string> notice)
        {
            string message = "开始解压升级包...";
            notice(message);
            //1.解压升级包文件
            //UnPack(info.ZipFilePath);
            notice("升级包解压完毕,正准备执行sql升级脚本...");
            //2.找出并执行sql脚本
            //ExecSqlUtil.ExcuteSqlFile($"{info.DecompressionPath}\\DbScript\\{info.UpgradeVersion}.sql", info.DbConnStr);
            notice("sql升级脚本执行完毕,正复制升级包文件到目标站点目录中...");
            //var copyitem = $"Copy-Item -Path \"{info.CopyPath}\" -Destination \"{info.MainSite.PhysicalPath}\" -Recurse -Force";
            //PowerShellUtil.RunScript(new List<string> { copyitem });
            notice("文件复制完毕,正重启目标网站中...");
            var startupSite = $"Start-Website \"{info.MainSite.Name}\";Get-Website -name \"{info.MainSite.Name}\"";
            PowerShellUtil.RunScript(new List<string> { startupSite });
            notice("正在打开站点首页...");
            var openurl = $"Start-Process -FilePath {info.MainSite.DefaultPage}";
            PowerShellUtil.RunScript(new List<string> { openurl });
        }


        public void TryRun(UpgradeInfo info, Action<string> notice)
        {
            notice("正在进行执行环境检测...");
            var num = 1;
            var result = TryUpgrade(info);
            var packages = result.UpgradePackages;
            var main = result.MainSite;
            notice("正在进行执行环境检测完毕。");
            notice("正在停止主站...");
            IISManager.StopIISSite(main.Name);
            notice("停止主站成功");
            packages.ForEach((package) =>
            {
                notice($"正在解压第{num}个升级包...");
                package.UpgradeSqlPath = $"{package.UnzipPath}\\DBScript\\{package.Version}.sql";
                Unzip(package.ZipPath);
                num++;
            });

            notice("升级包解压完毕,正准备执行sql升级脚本...");
            num = 1;//重置计数
            packages.ForEach((package) =>
            {
                notice($"正在执行第{num}个升级脚本...");
                ExecSqlUtil.ExcuteSqlFile(package.UpgradeSqlPath, result.DbConnStr);
                num++;
            });
            notice("sql升级脚本执行完毕,正复制升级包文件到目标站点目录中...");
            num = 1;//重置计数
            packages.ForEach((package) =>
            {
                notice($"正在复制第{num}升级包...");
                IISManager.CopyItem(package.CopyPath, main.PhysicalPath);
                num++;
            });
            notice("文件复制完毕,系统马上准备完毕...");

            notice("正在重启主站...");
            IISManager.StartIISSite(main.Name);
            if (!string.IsNullOrEmpty(main?.DefaultPage))
            {
                notice("正在打开站点首页...");
                IISManager.OpenDefaultBrower(main.DefaultPage);
            }
            using (var service = new UpgradeService(result.DbConnStr))
            {
                packages.ForEach((package) =>
                {
                    service.UpdateUpgradeStatus(new UpgradeLog { IsUpgraded = true, UpgradeDate = DateTime.Now, UpgradeLogGUID = package.PackageID });
                });
            }
            notice("移除中转站");
            IISManager.RemoveSite("UpgradeTranferSite");
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="sourcePath"></param>
        public void Unzip(string source, string destination = null)
        {
            CompressUtil.DecompressFile(source, destination);
        }

        /// <summary>
        /// 尝试升级
        /// </summary>
        /// <param name="info">升级信息</param>
        /// <returns></returns>
        public UpgradeInfo TryUpgrade(UpgradeInfo info)
        {
            if (info.MainSite == null)
                throw new Exception("升级配置信息错误,不存在的主站。");
            if (info.UpgradePackages == null || info.UpgradePackages?.Count == 0)
                throw new Exception("升级包不存在，升级已取消。");
            using (var upgradeService = new UpgradeService(info.DbConnStr))
            {
                var dels = new List<Guid>();
                info.UpgradePackages?.ForEach((package) =>
                {
                    if (upgradeService.DeduceIsUpgraded(package.PackageID))
                        dels.Add(package.PackageID);
                });
                info.UpgradePackages.RemoveAll(m => dels.Contains(m.PackageID));
            }
            if (info.UpgradePackages.Count == 0)
            {
                throw new Exception("当前已是最新版本，不用再升级了。");
            }
            return info;
        }

        #region 设置升级信息
        /// <summary>
        /// 获取升级信息
        /// </summary>
        /// <param name="path">升级信息文件地址</param>
        /// <returns></returns>
        public static UpgradeInfo GetUpgradeInfo(string path)
        {
            var str = FileUtil.GetFileContent(path);
            var config = JsonHelper.JSONToObject<UpgradeConfig>(str);
            if (config == null)
                throw new Exception("升级信息获取失败");
            var info = DESUtil.DecryptDes(config.Encryptstr, "cqhz2017");
            var upgradeInfo = JsonHelper.JSONToObject<UpgradeInfo>(info);
            return upgradeInfo;
        }
        #endregion
    }
}
