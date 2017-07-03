using mecode.toolkit;
using mecode.toolkit.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace service.tests
{
    [TestClass]
    public class DESTests
    {
        private static readonly string key = "cqhz2017";
        [TestCategory("DES加解密测试")]
        [TestMethod]
        public void EncryptTest()
        {
            var info = new UpgradeInfo
            {
                MainSite = new Site
                {
                    Name = "AutoUpgradeTest",
                    DefaultPage = "http://localhost:801/index.html",
                    PhysicalPath = "E:\\03_ReleaseWebSite\\AutoUpgradeTest",
                    Port = 801
                },
                DbConnStr = "server=zapdev.ticp.net\\dev,2989;uid=sa;pwd=Hz2017;database=Zapsoft_kx_test",
                UpgradePackages = new List<UpgradePackage>
                {
                    new UpgradePackage{
                        PackageID = Guid.Parse("6583bc9d-e1ff-4c0b-9625-78b27bedcd68"),
                        PackageName = "1.0.1.zip",
                        UnzipPath ="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.1\\",
                        ZipPath ="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.1.zip",
                        CopyPath="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.1\\*",
                        Version="1.0.1"
                    },
                    new UpgradePackage{
                        PackageID = Guid.Parse("5A1B0FDA-F10E-4844-9092-F7FFBDE9D6C8"),
                        PackageName = "1.0.2.zip",
                        UnzipPath ="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.2\\",
                        ZipPath ="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.2.zip",
                        CopyPath="E:\\03_ReleaseWebSite\\AutoUpgradeTest\\UpgradePackage\\1.0.2\\*",
                        Version="1.0.2"
                    }
                }
            };
            var encryptstr = JsonHelper.ObjectToJSON(info);
            var result = DESUtil.EncryptDES(encryptstr, key);
            FileUtil.Write("E:\\03_ReleaseWebSite\\AutoUpgradeTest\\upgrade.config.json", JsonHelper.ObjectToJSON(new UpgradeConfig { Encryptstr = result }));
            Console.WriteLine(result);
        }

        [TestCategory("DES加解密测试")]
        [TestMethod]
        public void DecryptDesTest()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\upgrade.config.json";
            var str = FileUtil.GetFileContent(path);
            var config = JsonHelper.JSONToObject<UpgradeConfig>(str);
            var result = DESUtil.DecryptDes(config.Encryptstr, key);
            var info = JsonHelper.JSONToObject<UpgradeInfo>(result);
            Console.WriteLine(result);
        }
    }
}
