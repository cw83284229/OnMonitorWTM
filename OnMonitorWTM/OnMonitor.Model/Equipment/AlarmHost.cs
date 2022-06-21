using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
   public class AlarmHost : BasePoco
    {
        /// <summary>
        /// 监控室
        /// </summary>
        [Display(Name = "监控室")]
        public Guid MonitorRoomId { get; set; }
        public MonitorRoom MonitorRoom { get; set; }
        /// <summary>
        /// 报警主机号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "报警主机号")]
        public string AlarmHost_ID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "账号")]
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(55)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 报警主机类型
        /// </summary>
        [StringLength(55)]
        [Display(Name = "报警主机类型")]
        public string AlarmHostType { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        [StringLength(55)]
        [Display(Name = "IP地址")]
        public string AlarmHostIP { get; set; }
        /// <summary>
        /// 通道数量
        /// </summary>
        [Display(Name = "通道数量")]
        public int? AlarmChannelCount { get; set; }
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
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }




        public virtual List<Alarm> Alarms { get; set; }
    }
}
