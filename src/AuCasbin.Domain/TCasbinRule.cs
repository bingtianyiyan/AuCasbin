using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace AuCasbin.Domain {

	[JsonObject(MemberSerialization.OptIn), Table(Name = "t_casbin_rule", DisableSyncStructure = true)]
	public partial class TCasbinRule {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int FId { get; set; }

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

		[JsonProperty, Column(StringLength = 100)]
		public string FPtype { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV0 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV1 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV2 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV3 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV4 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string FV5 { get; set; }

	}

}
