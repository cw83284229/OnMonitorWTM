using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
   public  class Alarm:BasePoco
    {


        /// <summary>
        /// 报警主机号
        /// </summary>
        [Display(Name = "报警主机号")]
        public Guid AlarmHostID { get; set; }
        public AlarmHost AlarmHost { get; set; }

        /// <summary>
        /// 报警编号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "报警编号")]
        public string Alarm_ID { get; set; }
        /// <summary>
        /// 通道编号
        /// </summary>
       
        [Display(Name = "通道号")]
        public int? Channel_ID { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼栋")]
        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼层")]
        public string floor { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(55)]
        [Display(Name = "位置")]
        public string Location { get; set; }
        /// <summary>
        /// 门岗类型
        /// </summary>
        [StringLength(55)]
        [Display(Name = "岗位类型")]
        public string GeteType { get; set; }
        /// <summary>
        /// 传感器类型
        /// </summary>
        [StringLength(55)]
        [Display(Name = "传感器类型")]
        public string SensorType { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name = "部门")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string department { get; set; }
        /// <summary>
        /// 费用代码
        /// </summary>
        [Display(Name = "费用代码")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Cost_code { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        [StringLength(55)]
        [Display(Name = "安装时间")]
        public string install_time { get; set; }
        /// <summary>
        /// 安装厂商
        /// </summary>
        [StringLength(55)]
        [Display(Name = "安装厂商")]
        public string category { get; set; }

        /// <summary>
        /// 内侧镜头号
        /// </summary>
       [StringLength(55)]
       [Display(Name = "内侧镜头号")]
        public string InsideCamera_ID { get; set; }

        /// <summary>
        /// 外侧镜头号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "外侧镜头号")]
        public string  OutsideCamera_ID { get; set; }

        /// <summary>
        /// 有无报警器
        /// </summary>
        [Display(Name = "有无报警器")]
        public bool IsAlertor { get; set; }

        /// <summary>
        /// 开岗状态
        /// </summary>
        [Display(Name = "开岗状态")]
        public bool IsOpenOrClosed { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
