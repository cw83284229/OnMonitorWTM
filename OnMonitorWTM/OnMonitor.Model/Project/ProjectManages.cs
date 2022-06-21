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
   public class ProjectManages:BasePoco
    {
        /// <summary>
        /// 工程变更类型
        /// </summary>
        [StringLength(55)]
        [Display(Name = "工程类型")]
        public string ProjectManageType { get; set; }
        /// <summary>
        /// 工程名称
        /// </summary>
        [StringLength(55)]
        [Display(Name = "工程名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 工程单号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "工程单号")]
        public string ProjectOrder { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [StringLength(55)]
        [Display(Name = "开始时间")]
        public string StartWorkDate { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        [StringLength(55)]
        [Display(Name = "完工时间")]
        public string CompleteDate { get; set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        [StringLength(55)]
        [Display(Name = "验收时间")]
        public string AcceptanceData { get; set; }

        /// <summary>
        /// 施工厂商
        /// </summary>
        [StringLength(55)]
        [Display(Name = "施工厂商")]
        public string ManufacturerName { get; set; }
        /// <summary>
        /// 工程说明
        /// </summary>
        [Display(Name = "工程说明")]
        public string ProjectSpecifications { get; set; }
        /// <summary>
        /// 改造楼栋
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼栋")]
        public string Build { get; set; }
        /// <summary>
        /// 改造楼层
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼层")]
        public string Floor { get; set; }
        /// <summary>
        /// 镜头编号JOSN类型
        /// </summary>
        [Display(Name = "镜头设备")]
        public virtual List<ProjectChangeCamera> CameraInfo { get; set; }

        /// <summary>
        /// 数据资料
        /// </summary>
        [Display(Name = "数据资料")]
        public Guid ExcelDataId { get; set; }
        public  FileAttachment ExcelData { get; set; }
        /// <summary>
        /// 楼层图纸信息
        /// </summary>
        [Display(Name = "楼层图纸")]
        public Guid LayoutInfoId { get; set; }
        public   FileAttachment LayoutInfo { get; set; }

        /// <summary>
        /// 配电信息JOSN类型
        /// </summary>
        [Display(Name = "配电信息")]
        public string PowerInfo { get; set; }

        /// <summary>
        /// 报警设备JOSN类型
        /// </summary>
        [Display(Name = "报警设备")]
        public string AlarmInfo { get; set; }

        /// <summary>
        /// 验收结果说明
        /// </summary>
        [Display(Name = "验收说明")]
        public string AcceptanceResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

    }
}
