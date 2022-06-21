using OnMonitor.Model.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Project
{
   public class CameraLayout : BasePoco
    {
        [Display(Name = "监控室")]
        public Guid monitorRoomId { get; set; }
        public MonitorRoom monitorRoom { get; set; }
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
        public string Floor { get; set; }

        /// <summary>
        /// 数据资料
        /// </summary>
        [Display(Name = "数据资料")]
        public Guid ExcelDataId { get; set; }
        public FileAttachment ExcelData { get; set; }
        /// <summary>
        /// 楼层图纸信息
        /// </summary>
        [Display(Name = "楼层图纸")]
        public Guid LayoutInfoId { get; set; }
        public FileAttachment LayoutInfo { get; set; }

        /// <summary>
        /// PDF图纸
        /// </summary>
        [Display(Name = "PDF图纸")]
        public Guid LayoutInfoPDFId { get; set; }
        public FileAttachment LayoutInfoPDF { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
