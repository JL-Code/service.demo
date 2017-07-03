using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mecode.toolkit
{
    /// <summary>
    /// 升级包
    /// </summary>
    public class UpgradePackage
    {
        public Guid PackageID { get; set; }
        public string PackageName { get; set; }
        /// <summary>
        /// 压缩文件路径
        /// </summary>
        public string ZipPath { get; set; }
        /// <summary>
        /// 复制路径
        /// </summary>
        public string CopyPath { get; set; }

        /// <summary>
        /// 解压路径
        /// </summary>
        public string UnzipPath { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 升级sql路径
        /// </summary>
        public string UpgradeSqlPath { get; set; }
    }
}
