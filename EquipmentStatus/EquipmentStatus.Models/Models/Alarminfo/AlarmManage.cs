namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AlarmManage
    {
      
        public Guid ID { get; set; }

        public Alarm Alarm { get; set; }
        public Guid AlarmID { get; set; }

       
        /// <summary>
        /// 报警时间
        /// </summary>

        public DateTime? AlarmTime { get; set; }
        /// <summary>
        /// 撤防时间
        /// </summary>
        
        public DateTime? WithdrawTime { get; set; }
        /// <summary>
        /// 撤防人员
        /// </summary>
        [StringLength(55)]
        public string WithdrawMan { get; set; }
        /// <summary>
        /// 撤防备注
        /// </summary>
        [StringLength(55)]
        public string WithdrawRemark { get; set; }
        /// <summary>
        /// 撤防类型
        /// </summary>
        public WithdrawType? WithdrawType { get; set; }
        /// <summary>
        /// 布防时间
        /// </summary>
       
        public DateTime? DefenceTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
      
        public DateTime? TreatmentTime { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [StringLength(55)]
        public string TreatmentTimeState { get; set; }
        /// <summary>
        /// 现场机动岗
        /// </summary>
        [StringLength(55)]
        public string TreatmentMan { get; set; }
        /// <summary>
        /// 现场处理回复
        /// </summary>
        [StringLength(55)]
        public string TreatmentReply { get; set; }
        /// <summary>
        /// 异常类型
        /// </summary>
       
        public AlarmMessageTypeEnum? AlarmType { get; set; }

        [StringLength(55)]
        public string Remark { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

     
    }

    public enum AlarmMessageTypeEnum
    {
        [Display(Name = "无异常")]
        NAD,
        [Display(Name = "现场使用")]
        FieldUse,
        [Display(Name = "设备异常")]
        EquipmentFault,
        [Display(Name = "异常报警")]
        AnomalyAlarm
    }
    public enum WithdrawType
    {
        [Display(Name = "现场使用")]
        FieldUse,
        [Display(Name = "开岗")]
        OpenDoor,
        [Display(Name = "报警撤防")]
        AlarmWithdraw,
        [Display(Name = "设备异常")]
        EquipmentFault,
    }
}
