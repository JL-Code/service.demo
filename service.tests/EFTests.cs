using mecode.toolkit;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace service.tests
{
    [TestClass]
    public class EFTests
    {
        private static readonly string connstr = "server=zapdev.ticp.net\\dev,2989;uid=sa;pwd=Hz2017;database=Zapsoft_kx_test";

        [TestCategory("EF操作数据库测试")]
        [TestMethod]
        public void Query()
        {
            using (var db = new EFDbContext(connstr))
            {
                var list = db.UpgradeLogs.AsEnumerable().ToList();
                Assert.AreNotEqual(0, list.Count);
            }
        }
    }
}
