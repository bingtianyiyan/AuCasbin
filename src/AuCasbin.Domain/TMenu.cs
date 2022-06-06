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
	/// 权限
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_menu", DisableSyncStructure = true)]
	public partial class TMenu {

		/// <summary>
		/// 主键Id
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long FId { get; set; }

		/// <summary>
		/// 请求类型(POST,GET)
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string FAction { get; set; }

		/// <summary>
		/// 可关闭
		/// </summary>
		[JsonProperty]
		public bool? FClosable { get; set; }

		/// <summary>
		/// 菜单访问的实际页面地址(/admin/sys-role/index)
		/// </summary>
		[JsonProperty]
		public string FComponent { get; set; }

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
		[JsonProperty, Column(StringLength = 100)]
		public string FDescription { get; set; }

		/// <summary>
		/// 启用
		/// </summary>
		[JsonProperty]
		public bool FEnabled { get; set; }

		/// <summary>
		/// 链接外显
		/// </summary>
		[JsonProperty]
		public bool? FExternal { get; set; }

		/// <summary>
		/// 隐藏
		/// </summary>
		[JsonProperty]
		public bool FHidden { get; set; }

		/// <summary>
		/// 图标
		/// </summary>
		[JsonProperty, Column(StringLength = 100)]
		public string FIcon { get; set; }

		/// <summary>
		/// 父子级主键ID拼接(/0/2/52
		/// </summary>
		[JsonProperty, Column(StringLength = 500)]
		public string FIdPath { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		[JsonProperty]
		public bool FIsDeleted { get; set; }

		/// <summary>
		/// 权限名称
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string FLabel { get; set; }

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
		/// 打开新窗口
		/// </summary>
		[JsonProperty]
		public bool? FNewWindow { get; set; }

		/// <summary>
		/// 打开组
		/// </summary>
		[JsonProperty]
		public bool? FOpened { get; set; }

		/// <summary>
		/// 父级节点
		/// </summary>
		[JsonProperty]
		public long FParentId { get; set; }

		/// <summary>
		/// 菜单主地址(/admin/sys-role)
		/// </summary>
		[JsonProperty, Column(StringLength = 500)]
		public string FPath { get; set; }

		/// <summary>
		/// 权限编码(比如admin:sysRole:list
		/// </summary>
		[JsonProperty, Column(StringLength = 550)]
		public string FPermisson { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		[JsonProperty]
		public int? FSort { get; set; }

		/// <summary>
		/// 权限类型(M,C,F)
		/// </summary>
		[JsonProperty]
		public int FType { get; set; }

		[Navigate(nameof(FParentId))]
		public List<TMenu> Childs { get; set; }

		[Navigate(ManyToMany = typeof(TMenuApiRule))]
		public ICollection<TApi> Apis { get; set; }

	}

}
