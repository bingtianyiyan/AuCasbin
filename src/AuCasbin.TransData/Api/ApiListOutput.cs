namespace AuCasbin.TransData.Api
{
    public class ApiListOutput
    {
        /// <summary>
        /// 接口Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 接口父级
        /// </summary>
		public long? ParentId { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 接口提交方法
        /// </summary>
        public string HttpMethods { get; set; }
    }
}