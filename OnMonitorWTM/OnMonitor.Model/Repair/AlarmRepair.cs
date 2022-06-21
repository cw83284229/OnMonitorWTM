using OnMonitor.Model.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Repair
{
   public class AlarmRepair : BasePoco
    {

        [Display(Name = "报警号")]
        public Guid AlarmId { get; set; }
        public Alarm Alarm { get; set; }

        [Display(Name ="测试时间")]
        public DateTime? TestTime { get; set; }
        [Display(Name = "异常时间")]
        public DateTime? AnomalyTime { get; set; }

        [Display(Name = "异常状态")]
        public AlarmAnomalyState AlarmAnomalyState { get; set; }
        [Display(Name = "测试人员")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]

        public string Registrar { get; set; }

        [Display(Name = "修复状态")]
        public RepairState RepairState { get; set; }

        [Display(Name = "修复时间")]
        public DateTime? RepairedTime { get; set; }

        [Display(Name = "维修人员")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Accendant { get; set; }

        [Display(Name = "维修内容")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string RepairDetails { get; set; }

        [Display(Name = "维修厂商")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string RepairFirm { get; set; }

        [Display(Name = "确认人")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Supervisor { get; set; }

       

    }

    public enum AlarmAnomalyState
    {
     
        [Display(Name = "报警主机异常")]
        HostAnomaly,
     
        [Display(Name = "前端不报警")]
        ProAnomaly,
  
        [Display(Name = "后端不报警")]
        LaterAnomaly,

        [Display(Name = "两端无信号")]
        AllAnomaly,

        [Display(Name = "系统异常")]
        SystemAnomaly

    }
}
