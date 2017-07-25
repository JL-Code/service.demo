using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mecode.toolkit;

namespace server.console.tests
{
    [TestClass]
    public class SqlExecuteTest
    {
        private static readonly string connstr = "server=zapdev.ticp.net\\dev,2989;uid=sa;pwd=Hz2017;database=Zapsoft_kx_test";

        [TestCategory("自动升级_SQL执行测试")]
        [TestMethod]
        public void ExcuteSqlFile_SQLFile_Success()
        {
            var path = "E:\\1.0.3.sql";
            ExecSqlUtil.ExcuteSqlFile(path, connstr);
        }
    }
}
