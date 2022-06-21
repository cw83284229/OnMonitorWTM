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
    public class CameraRepair : BasePoco
    {

        [Display(Name = "镜头号")]
        [Required(ErrorMessage = "此项不能为空")]
        public Guid CameraId { get; set; }
        public Camera Camera { get; set; }

        [Display(Name = "异常时间")]

        public DateTime? AnomalyTime { get; set; }

        [Display(Name = "统计时间")]

        public DateTime? CollectTime { get; set; }
        [Display(Name = "异常类别")]
        public AnomalyType AnomalyType { get; set; }

        [Display(Name = "异常等级")]
        public AnomalyGrade AnomalyGrade { get; set; }

        [Display(Name = "统计人员")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Registrar { get; set; }

        [Display(Name = "修复状态")]
        public  RepairState RepairState { get; set; }

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

        [Display(Name = "更换设备")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string ReplacePart { get; set; }

        [Display(Name = "工程异常")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string ProjectAnomaly { get; set; }

        [Display(Name = "灰屏确认")]
        public bool NoSignal { get; set; } = false;

        [Display(Name = "异常确认")]
        public AnomalyIdentification AnomalyIdentification { get; set; }

        [Display(Name = "备注")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Remark { get; set; }

    }

    public enum AnomalyType
    {    
        /// <summary>
        /// 灰屏
        /// </summary>
        [Display(Name = "灰屏")]
        NoSignal,
        /// <summary>
        /// 模糊
        /// </summary>
        [Display(Name = "模糊")]
        Dim,
        /// <summary>
        /// 干扰
        /// </summary>
        [Display(Name = "干擾")]
        Disturb,
        /// <summary>
        /// 错位
        /// </summary>
        [Display(Name = "錯位")]
        Malposition

    }
    public enum AnomalyGrade
    {
        [Display(Name = "1级")]
        One,
        [Display(Name = "2级")]
        Two,
        [Display(Name = "3级")]
        Three,
        [Display(Name = "4级")]
        four,
        [Display(Name = "5级")]
        Five
    }

    public enum AnomalyIdentification
    {     /// <summary>
         /// 未知
         /// </summary>
        [Display(Name = "未知")]
        Unknown,
        /// <summary>
        /// 已确认
        /// </summary>
        [Display(Name = "已确认")]
        Confirmed,
        /// <summary>
        /// 未确认
        /// </summary>
        [Display(Name = "未确认")]
        Unconfirmed,
    }
    public enum RepairState
    {  
        /// <summary>
        /// 未处理
        /// </summary>
        [Display(Name = "未处理")]
        Untreated,
        /// <summary>
        /// 已处理
        /// </summary>
        [Display(Name = "已处理")]
        Treated,
        /// <summary>
        /// 无需处理
        /// </summary>
        [Display(Name = "无需处理")]
        Undeal

    }

}
