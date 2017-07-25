using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zap.Framework;

namespace service.tests
{
    /// <summary>
    /// 文件下载测试
    /// </summary>
    [TestClass]
    public class FileDownloadTest
    {
        [TestCategory("自动升级_文件下载")]
        [TestMethod]
        public void DownloadTest()
        {
            var path = "E:\\";
            var uri = "http://reg.highzap.com/api/upgrade/file?version=4.6.0.70708";
            using (var downloader = new Downloader(uri, path, "4.6.0.70708.zip"))
            {
                while (!downloader.IsCompleted)
                {
                    downloader.Download();
                    Console.WriteLine("进度：" + downloader.CurrentProgress);
                }
                Console.WriteLine("下载完成");
            }
        }
    }
}
