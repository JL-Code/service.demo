namespace mecode.toolkit.Entites
{
    /// <summary>
    /// 升级信息
    /// </summary>
    public class UpgradeInfo
    {
        /// <summary>
        /// 解压路径
        /// </summary>
        public string DecompressionPath { get; set; }
        /// <summary>
        /// 压缩文件路径
        /// </summary>
        public string ZipFilePath { get; set; }
        /// <summary>
        /// 复制地址
        /// </summary>
        public string CopyPath { get; set; }
        /// <summary>
        /// 主站
        /// </summary>
        public Site MainSite { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnStr { get; set; }
        /// <summary>
        /// 升级版本
        /// </summary>
        public string UpgradeVersion { get; set; }

    }
}
