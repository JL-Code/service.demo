using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace service.tests
{
    [TestClass]
    public class FileTest
    {
        [TestCategory("文件操作")]
        [TestMethod]
        public void CreateFile_FileMode_OpenOrCreate()
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\test\\1.txt";
            var content = "你好 openOrCreate";
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            };
        }

        [TestCategory("文件操作")]
        [TestMethod]
        public void CreateFile_FileMode_Create()
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\test\\1.txt";
            var content = "你好 Create";
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            };
        }
    }
}
