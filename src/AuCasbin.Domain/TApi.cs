using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace AuCasbin.Domain {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_api", DisableSyncStructure = true)]
	public partial class TApi {

		/// <summary>
		/// 主键编码
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long FId { get; set; }

		/// <summary>
		/// 请求类型
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string FAction { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty, Column(InsertValueSql = "CURRENT_TIMESTAMP(3)")]
		public DateTime? FCreatedTime { get; set; }

		/// <summary>
		/// 创建者Id
		/// </summary>
		[JsonProperty]
		public long? FCreatedUserId { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FCreatedUserName { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty, Column(InsertValueSql = "CURRENT_TIMESTAMP(3)")]
		public DateTime? FModifiedTime { get; set; }

		/// <summary>
		/// 修改者Id
		/// </summary>
		[JsonProperty]
		public long? FModifiedUserId { get; set; }

		/// <summary>
		/// 修改者
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FModifiedUserName { get; set; }

		/// <summary>
		/// 父级节点主键编码
		/// </summary>
		[JsonProperty]
		public long? FParentId { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string FPath { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string FTitle { get; set; }

		[Navigate(nameof(FParentId))]
		public List<TApi> Childs { get; set; }

		[Navigate(ManyToMany = typeof(TMenu))]
		public ICollection<TMenu> Menus { get; set; }

	}

}
