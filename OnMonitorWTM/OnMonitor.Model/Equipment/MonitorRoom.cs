using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
    public  class MonitorRoom :TopBasePoco
    {
        /// <summary>
        /// 厂区
        /// </summary>
       
        [Display(Name = "厂区")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Factory { get; set; }
        /// <summary>
        /// 监控室
        /// </summary>
        [Display(Name = "监控室")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string RoomLocation { get; set; }
        /// <summary>
        /// 监控室类别
        /// </summary>
        [Display(Name = "监控室类别")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string RoomType { get; set; }
      
    }
}
