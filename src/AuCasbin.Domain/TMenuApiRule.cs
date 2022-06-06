using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace AuCasbin.Domain {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_menu_api_rule", DisableSyncStructure = true)]
	public partial class TMenuApiRule {

		/// <summary>
		/// 主键编码
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long FApiId { get; set; }

		[JsonProperty, Column(IsPrimary = true)]
		public long FMenuId { get; set; }

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
		/// 权限
		/// </summary>
		public TMenu Menus { get; set; }

		/// <summary>
		/// 接口
		/// </summary>
		public TApi Api { get; set; }

	}

}
