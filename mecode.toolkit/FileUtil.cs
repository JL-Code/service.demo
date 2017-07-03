using System;
using System.IO;
using System.Text;

namespace mecode.toolkit
{
    public class FileUtil
    {
        /// <summary>
        /// 将文本文件转换为string字符串
        /// </summary>
        /// <param name="path">文件路径</param>
        public static string GetFileContent(string path)
        {
            string scriptStr;
            if (!File.Exists(path))
                throw new Exception("当前文件不存在");
            using (var fs = new FileStream(path, FileMode.Open))
            {
                if (!fs.CanRead)
                    throw new Exception("当前文件不不可读");
                var reader = new StreamReader(fs);
                scriptStr = reader.ReadToEnd();
                reader.Close();
            }
            return scriptStr;
        }

        /// <summary>
        /// 将字符串写到硬盘中
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <param name="content">要写的内容</param>
        public static void Write(string path, string content)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            };
        }
    }
}
