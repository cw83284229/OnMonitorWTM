using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
   public class DVR : BasePoco
    {

        [Display(Name = "监控室")]
        [Required()]
        public Guid monitorRoomId { get; set; }
        public MonitorRoom monitorRoom { get; set; }

        [Display(Name = "主机号")]
        [Required()]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_ID { get; set; }
        [Display(Name = "归属服务器")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Home_server { get; set; }
        [Display(Name = "硬盘容量")]
        public int? Hard_drive { get; set; }
        [Display(Name = "IP")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_IP { get; set; }
        [Display(Name = "端口")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_port { get; set; }
        [Display(Name = "账号")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_usre { get; set; }
       
        [Display(Name = "密码")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_possword { get; set; }
        
        [Display(Name = "安装时间")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string install_time { get; set; }
       
        [Display(Name = "安装厂商")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Manufacturer { get; set; }
       
        [Display(Name = "DVR类型")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_type { get; set; }
        
        [Display(Name = "SN号")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_SN { get; set; }
        
        [Display(Name = "监控数量")]
        public int? DVR_Channel { get; set; }
        
        [Display(Name = "部门")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string department { get; set; }
       
        [Display(Name = "费用代码")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Cost_code { get; set; }
        
        [Display(Name = "备注")]
        [StringLength(150, ErrorMessage = "输入超出限定长度")]
        public string Remark { get; set; }



        public virtual List<Camera> Cameras { get; set; }

    }
}
