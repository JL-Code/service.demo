namespace server.console.Entities
{
    /// <summary>
    /// 服务模型
    /// </summary>
    public class Service
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务状态
        /// </summary>
        public int Status { get; set; }
    }
}
