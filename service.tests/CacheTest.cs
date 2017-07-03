using mecode.toolkit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace service.tests
{
    [TestClass]
    public class CacheTest
    {

        [TestCategory("缓存测试")]
        [TestMethod]
        public void InsertCache()
        {
            for (int i = 0; i < 50; i++)
            {
                CacheUtil.Insert(i.ToString(), "值：" + i);
            }
            for (int i = 0; i < 50; i++)
            {
                var obj = CacheUtil.Get(i.ToString());
                Console.WriteLine(obj);
            }
        }
    }
}
