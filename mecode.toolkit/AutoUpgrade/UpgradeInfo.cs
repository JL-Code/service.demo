using System.Collections.Generic;

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
        public string UpzipPath { get; set; }

        /// <summary>
        /// 主站
        /// </summary>
        public Site MainSite { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnStr { get; set; }
        /// <summary>
        /// 升级服务地址 包含端口号
        /// </summary>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// 升级包
        /// </summary>
        public List<UpgradePackage> UpgradePackages { get; set; }

    }
}
