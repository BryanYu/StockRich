using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StockRich.Infrastructure.Models
{
    /// <summary>
    /// 公司基本資料
    /// </summary>
    [Table("company_info")]
    public partial class CompanyInfo
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        /// <summary>
        /// 股號
        /// </summary>
        [Column("stock_code", TypeName = "character varying")]
        public string StockCode { get; set; } = null!;
        /// <summary>
        /// 公司全名
        /// </summary>
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 公司簡稱
        /// </summary>
        [Column("alias", TypeName = "character varying")]
        public string Alias { get; set; } = null!;
        /// <summary>
        /// 產業別
        /// </summary>
        [Column("industry_type", TypeName = "character varying")]
        public string IndustryType { get; set; } = null!;
        /// <summary>
        /// 董事長
        /// </summary>
        [Column("chair_man", TypeName = "character varying")]
        public string ChairMan { get; set; } = null!;
        /// <summary>
        /// 總經理
        /// </summary>
        [Column("general_manager", TypeName = "character varying")]
        public string GeneralManager { get; set; } = null!;
        /// <summary>
        /// 發言人
        /// </summary>
        [Column("spokes_man", TypeName = "character varying")]
        public string SpokesMan { get; set; } = null!;
        /// <summary>
        /// 連絡電話
        /// </summary>
        [Column("contact_telephone", TypeName = "character varying")]
        public string ContactTelephone { get; set; } = null!;
        /// <summary>
        /// 成立日期
        /// </summary>
        [Column("begin_date")]
        public DateOnly BeginDate { get; set; }
        /// <summary>
        /// 上市日期
        /// </summary>
        [Column("public_date")]
        public DateOnly PublicDate { get; set; }
        /// <summary>
        /// 實收資本額
        /// </summary>
        [Column("capital")]
        public decimal Capital { get; set; }
        /// <summary>
        /// 電子郵件信箱
        /// </summary>
        [Column("contact_email", TypeName = "character varying")]
        public string ContactEmail { get; set; } = null!;
        /// <summary>
        /// 網址
        /// </summary>
        [Column("website", TypeName = "character varying")]
        public string Website { get; set; } = null!;
        [Column("create_datetime")]
        public DateTime? CreateDatetime { get; set; }
        [Column("update_datetime")]
        public DateTime? UpdateDatetime { get; set; }
    }
}
