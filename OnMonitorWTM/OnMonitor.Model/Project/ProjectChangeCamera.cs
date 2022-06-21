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
   public class ProjectChangeCamera : BasePoco
    {
        /// <summary>
        /// 工程名称
        /// </summary>
        [Display(Name = "工程名称")]
        public Guid ProjectManagesId { get; set; }
        public ProjectManages ProjectManages { get; set; }

        /// <summary>
        /// 改造镜头
        /// </summary>
        [Display(Name = "镜头信息")]
        public Guid CameraId { get; set; }
        public Camera Camera{ get; set; }
        /// <summary>
        /// 改造后位置
        /// </summary>
        [StringLength(55)]
        [Display(Name = "改造后位置")]
        public string ChangeLocation { get; set; }
        /// <summary>
        /// 改造状态
        /// </summary>
        [Display(Name = "改造状态")]
        public TransformationStatus TransformationStatus { get; set; }



        /// <summary>
        /// 拆除标记
        /// </summary>
        [Display(Name = "拆除标记")]
        public bool IsDismantle { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

    }


    public enum TransformationStatus
    {
        /// <summary>
        /// 未改造
        /// </summary>
        [Display(Name = "未改造")]
        Unrefined,

        /// <summary>
        /// 已完成
        /// </summary>
        [Display(Name = "已完成")]
        Completed,
        /// <summary>
        /// 改造中
        /// </summary>
        [Display(Name = "改造中")]

        OnGoing
    
    
    }

}
