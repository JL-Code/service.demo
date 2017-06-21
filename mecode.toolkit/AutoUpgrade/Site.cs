namespace mecode.toolkit.Entites
{
    /// <summary>
    /// 站点信息
    /// </summary>
    public class Site
    {
        public string Name { get; set; }

        public string PhysicalPath { get; set; }

        public string DomainName { get; set; } = "";
        /// <summary>
        /// 站点绑定信息 [IP]:Port:[hostname] eg:"*:8080:"
        /// </summary>
        public string BindingInformation { get => $"{IPAddress}:{Port}:{HostName}"; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        public int Port { get; set; } = 80;

        public string IPAddress { get; set; } = "*";

        public string DefaultPage { get; set; }
    }
}
