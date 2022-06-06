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
	/// 用户
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_user", DisableSyncStructure = true)]
	public partial class TUser {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long FId { get; set; }

		/// <summary>
		/// 头像
		/// </summary>
		[JsonProperty, Column(StringLength = 100)]
		public string FAvatar { get; set; }

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
		/// 是否删除
		/// </summary>
		[JsonProperty]
		public bool FIsDeleted { get; set; }

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
		/// 昵称
		/// </summary>
		[JsonProperty, Column(StringLength = 60)]
		public string FNickName { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		[JsonProperty, Column(StringLength = 60, IsNullable = false)]
		public string FPassword { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = 500)]
		public string FRemark { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[JsonProperty]
		public int FStatus { get; set; }

		/// <summary>
		/// 账号
		/// </summary>
		[JsonProperty, Column(StringLength = 60, IsNullable = false)]
		public string FUserName { get; set; }

		/// <summary>
		/// 主属部门Id
		/// </summary>
		[JsonProperty]
		public long OrganizationId { get; set; }

		[Navigate(ManyToMany = typeof(TUserRole))]
		public ICollection<TRole> Roles { get; set; }

	}

}
