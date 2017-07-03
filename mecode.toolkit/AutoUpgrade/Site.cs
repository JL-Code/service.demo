namespace mecode.toolkit.Entites
{
    /// <summary>
    /// 站点信息
    /// </summary>
    public class Site
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 站点物理地址
        /// </summary>
        public string PhysicalPath { get; set; }
        /// <summary>
        /// 站点绑定信息 [IP]:Port:[hostname] eg:"*:8080:"
        /// </summary>
        public string BindingInformation { get => $"{IPAddress}:{Port}:{HostName}"; }
        /// <summary>
        /// 主机名(默认为空)
        /// </summary>
        public string HostName { get; set; } = "\"\"";
        /// <summary>
        /// 端口号（默认80）
        /// </summary>
        public int Port { get; set; } = 80;
        /// <summary>
        /// IP地址（默认 *）
        /// </summary>
        public string IPAddress { get; set; } = "*";
        /// <summary>
        /// 默认访问页面
        /// </summary>
        public string DefaultPage { get; set; }
    }
}
