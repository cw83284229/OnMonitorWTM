namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DVRInfoChecks
    {
        public Guid ID { get; set; }

        public Guid DVRId { get; set; }

        [StringLength(55)]
        public string DVR_SN { get; set; }

        public int? DVR_Channel { get; set; }

        public int? DiskTotal { get; set; }

        public string DVRDISK { get; set; }

        public string DVRChannelInfo { get; set; }

        public CheckState DVR_Online { get; set; }

        public CheckState TimeInfoChenk { get; set; }

        public CheckState DiskChenk { get; set; }

        public CheckState SNChenk { get; set; }

        public CheckState VideoCheck90Day { get; set; }
        public int VideoStarageTime { get; set; }
        [StringLength(55)]
        public string Remark { get; set; }

        public virtual DVRs DVRs { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }
    }



    public enum CheckState
    {
        /// <summary>
        /// 未检查
        /// </summary>
        [Display(Name = "未检查")]
        Inactive,
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal,
        /// <summary>
        /// 异常
        /// </summary>
        [Display(Name = "异常")]
        Anomaly,


    }
}
