using mecode.toolkit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace service.tests
{
    [TestClass]
    public class AdoNetTests
    {
        private static readonly string connstr = "server=.;uid=sa;pwd=123456;database=dev";

        [TestCategory("AdoNet测试")]
        [TestMethod]
        public void QueryUser_UserPasswrod_True()
        {
            var sqlText = "select * from sys_user where userid=@userid";
            var dt = AdoNetUtil.Query(sqlText, connstr, new SqlParameter
            {
                ParameterName = "@userid",
                SqlValue = "9461F8A8-66AA-4EDD-93B7-064F11AB19C3"
            });
            Console.WriteLine(JsonHelper.ObjectToJSON(dt));
            Assert.AreEqual(true, dt.Rows.Count > 0);
        }
    }
}
