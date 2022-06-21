using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using OnMonitor.Model;
using OnMonitor.Model.Equipment;

namespace OnMonitor.Model.AlarmManages
{
    /// <summary>
    /// 报警记录
    /// </summary>
	[Table("AlarmManages")]
    [Display(Name = "报警记录")]
    public class AlarmManage : BasePoco
    {
        [Display(Name = "报警号")]
        public Alarm Alarm { get; set; }
        [Display(Name = "报警ID")]
        public Guid? AlarmId { get; set; }
        [Display(Name = "报警时间")]
        public DateTime? AlarmTime { get; set; }
        [Display(Name = "撤防时间")]
        public DateTime? WithdrawTime { get; set; }
        [Display(Name = "操作员")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string WithdrawMan { get; set; }
        [Display(Name = "撤防备注")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string WithdrawRemark { get; set; }
        [Display(Name = "操作类型")]
        public WithdrawType? WithdrawType { get; set; }
        [Display(Name = "布防时间")]
        public DateTime? DefenceTime { get; set; }
        [Display(Name = "机动岗处理时间")]
        public DateTime? TreatmentTime { get; set; }
        [Display(Name = "当前处理状态")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string TreatmentTimeState { get; set; }
        [Display(Name = "现场机动岗")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string TreatmentMan { get; set; }
        [Display(Name = "现场处理回复")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string TreatmentReply { get; set; }
        [Display(Name = "报警类型")]
        public AlarmMessageTypeEnum? AlarmType { get; set; }
        [Display(Name = "备注")]
        [StringLength(55, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Remark { get; set; }
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
        AnomalyAlarm,
        [Display(Name = "门磁测试")]
        TestAlarm
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
        [Display(Name = "封岗")]
        ClosedDoor,
    }

}
