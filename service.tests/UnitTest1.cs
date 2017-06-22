using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net;
using System.Reflection;
using mecode.toolkit;
using System.IO;
using System.Collections.Generic;
using mecode.toolkit.Entites;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace service.tests
{
    [TestClass]
    public class UnitTest1
    {
        private AutoUpgradeManager _manager;

        [TestInitialize]
        public void Initialize()
        {
            _manager = new AutoUpgradeManager();
        }

        [TestCategory("日志记录测试")]
        [TestMethod]
        public void LogTest()
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error("error", new Exception("发生了一个异常"));
            log.Fatal("fatal", new Exception("发生了一个致命错误"));
            log.Info("info");
            log.Debug("debug");
            log.Warn("warn");
            Console.WriteLine("日志记录完毕。");
        }

        [TestCategory("自动升级_文件解压")]
        [TestMethod]
        public void CompressFolder()
        {
            CompressUtil.CompressFolder(@"E:\\PublishOutput", @"E:\\PublishOutput.zip");
            var path = "E:\\PublishOutput.zip";
            var flag = File.Exists(path);
            Assert.AreEqual(flag, true);
        }

        [TestCategory("自动升级_文件解压")]
        [TestMethod]
        public void UnZipFolder()
        {
            var path = "E:\\1.0.0";
            CompressUtil.DecompressFile(@"E:\\1.0.0.zip");
            var flag = Directory.Exists(path);
            Assert.AreEqual(flag, true);
        }

        [TestCategory("自动升级_启动升级测试")]
        [TestMethod]
        public void UpgradeSite_UpgradeInfo_Success()
        {
            var version = "1.0.1";
            try
            {
                var info = new UpgradeInfo
                {
                    ZipFilePath = $"E:\\{version}.zip",
                    DecompressionPath = $"E:\\{version}",
                    CopyPath = $"E:\\{version}\\*",
                    UpgradeVersion = version,
                    DbConnStr = "server=.;uid=sa;pwd=123456;database=dev",
                    MainSite = new Site
                    {
                        Name = "UpgradeSiteTest",
                        PhysicalPath = @"E:\03_ReleaseWebSite\UpgradeSiteTest",
                        DomainName = "http://localhost:806/home/windowsSignalrTest",
                        Port = 806
                    }
                };
                Logger.Info("升级信息: " + JsonHelper.ObjectToJSON(info));
                _manager.Run(info);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Assert.Fail(ex.Message);
            }
        }

        [TestCategory("自动升级_文件解压")]
        [TestMethod]
        public void CreateIISSite_SiteInfo_Success()
        {
            _manager.CreateIISSite("E:\\PublishOutput\\*", new Site
            {
                Name = "PowerShellSite",
                Port = 805,
                PhysicalPath = "E:\\03_ReleaseWebSite\\PowerShellSite",
                DefaultPage = "http://localhost:805/home/windowsSignalrTest"
            }, true);

        }

        [TestCategory("自动升级_文件解压")]
        [TestMethod]
        public void CopyItem_SiteInfo_Success()
        {
            var dict = new Dictionary<string, object>
            {
                { "Path", @"E:\PublishOutput\*" },
                { "Destination",@"E:\03_ReleaseWebSite\PowerShellSite"},
                { "Recurse",true},
                { "Force",true}
            };
            PowerShellUtil.Run("Copy-Item", dict);
        }
    }
}
