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
	/// 组织架构
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_organization", DisableSyncStructure = true)]
	public partial class TOrganization {

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
		/// 描述
		/// </summary>
		[JsonProperty, Column(StringLength = 500)]
		public string FDescription { get; set; }

		/// <summary>
		/// 员工人数
		/// </summary>
		[JsonProperty]
		public int FEmployeeCount { get; set; }

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
		/// 父级
		/// </summary>
		[JsonProperty]
		public long FParentId { get; set; }

		/// <summary>
		/// 主管Id
		/// </summary>
		[JsonProperty]
		public long? FPrimaryEmployeeId { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty]
		public int FSort { get; set; }

		/// <summary>
		/// 值
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FValue { get; set; }

		[Navigate(nameof(FParentId))]
		public List<TOrganization> Childs { get; set; }

	}

}
