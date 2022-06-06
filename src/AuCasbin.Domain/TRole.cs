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
	/// 角色
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_role", DisableSyncStructure = true)]
	public partial class TRole {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long FId { get; set; }

		/// <summary>
		/// 编码
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FCode { get; set; }

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
		/// 数据范围
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string FDataScope { get; set; }

		/// <summary>
		/// 说明
		/// </summary>
		[JsonProperty, Column(StringLength = 200)]
		public string FDescription { get; set; }

		/// <summary>
		/// 启用
		/// </summary>
		[JsonProperty]
		public bool FEnabled { get; set; }

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
		/// 名称
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FName { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty]
		public int FSort { get; set; }

		[Navigate(ManyToMany = typeof(TUserRole))]
		public ICollection<TUser> Users { get; set; }

		[Navigate(ManyToMany = typeof(TRoleMenu))]
		public ICollection<TMenu> Menus { get; set; }

	}

}
