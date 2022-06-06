using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace AuCasbin.Domain {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_role_menu", DisableSyncStructure = true)]
	public partial class TRoleMenu {

		[JsonProperty, Column(IsPrimary = true)]
		public long FMenuId { get; set; }

		[JsonProperty, Column(IsPrimary = true)]
		public long FRoleId { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty, Column(InsertValueSql = "CURRENT_TIMESTAMP(3)")]
		public DateTime? CreatedTime { get; set; }

		/// <summary>
		/// 创建者Id
		/// </summary>
		[JsonProperty]
		public long? CreatedUserId { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string CreatedUserName { get; set; }

		/// <summary>
		/// 角色
		/// </summary>
		public TRole Role { get; set; }

		/// <summary>
		/// 权限
		/// </summary>
		public TMenu Menus { get; set; }

	}

}
