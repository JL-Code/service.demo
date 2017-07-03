using mecode.toolkit;
using mecode.toolkit.Entites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace service.tests
{
    [TestClass]
    public class Upgrade_BusinessLogic_Tests
    {
        private static readonly string connstr = "server=zapdev.ticp.net\\dev,2989;uid=sa;pwd=Hz2017;database=Zapsoft_kx_test";

        [TestCategory("升级业务测试")]
        [TestMethod]
        public void TryUpgradeTest()
        {
            var info = new UpgradeInfo
            {
                DbConnStr = connstr,
                UpgradePackages = new List<UpgradePackage>
                {
                    new UpgradePackage{
                        PackageID = Guid.Parse("5A1B0FDA-F10E-4844-9092-F7FFBDE9D6C8"),
                        PackageName = "upgrade_2.zip"
                    },
                    new UpgradePackage{
                        PackageID = Guid.Parse("6583BC9D-E1FF-4C0B-9625-78B27BEDCD68"),
                        PackageName = "upgrade_1.zip"
                    }
                }
            };
            try
            {
                var result = new AutoUpgradeManager().TryUpgrade(info);
                Console.WriteLine(JsonHelper.ObjectToJSON(result));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestCategory("升级业务测试")]
        [TestMethod]
        public void UpgradeSiteTest()
        {
            var info = new UpgradeInfo
            {
                DbConnStr = "server=zapdev.ticp.net\\dev,2989;uid=sa;pwd=Hz2017;database=Zapsoft_kx_test",
                UpgradePackages = new List<UpgradePackage>
                {
                    new UpgradePackage{
                        PackageID = Guid.Parse("5A1B0FDA-F10E-4844-9092-F7FFBDE9D6C8"),
                        PackageName = "upgrade_2.zip",
                        UnzipPath ="",
                        ZipPath ="",
                        CopyPath="",
                        Version="",
                        UpgradeSqlPath=""
                    },
                    new UpgradePackage{
                        PackageID = Guid.Parse("6583BC9D-E1FF-4C0B-9625-78B27BEDCD68"),
                        PackageName = "upgrade_1.zip"
                    }
                }
            };
            try
            {
                var manager = new AutoUpgradeManager();
                var result = manager.TryUpgrade(info);
                manager.TryRun(result, (message) =>
                {
                    Console.WriteLine(message);
                });
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
