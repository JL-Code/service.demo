using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mecode.toolkit;

namespace server.console.tests
{
    [TestClass]
    public class SqlExecuteTest
    {
        private static readonly string connstr = "server=.;uid=sa;pwd=123456;database=dev";

        [TestCategory("自动升级_SQL执行测试")]
        [TestMethod]
        public void ExcuteSqlFile_SQLFile_Success()
        {
            var path = "E:\\dev_test.sql";
            ExecSqlUtil.ExcuteSqlFile(path, connstr);
        }
    }
}
