using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace AuCasbin.Domain {

	/// <summary>
	/// 用户角色
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_user_role", DisableSyncStructure = true)]
	public partial class TUserRole {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long FId { get; set; }

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
		/// 角色Id
		/// </summary>
		[JsonProperty]
		public long FRoleId { get; set; }

		/// <summary>
		/// 用户Id
		/// </summary>
		[JsonProperty]
		public long FUserId { get; set; }

		public TUser User { get; set; }

		public TRole Role { get; set; }

	}

}
