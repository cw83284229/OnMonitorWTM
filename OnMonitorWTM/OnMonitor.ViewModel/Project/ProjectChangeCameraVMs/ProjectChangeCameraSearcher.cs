using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Project.ProjectChangeCameraVMs
{
    public partial class ProjectChangeCameraSearcher : BaseSearcher
    {
        [Display(Name = "工程名称")]
        public Guid? ProjectManagesId { get; set; }
        [Display(Name = "镜头编号")]
        public Guid? CameraId { get; set; }
        [Display(Name = "改造状态")]
        public TransformationStatus? TransformationStatus { get; set; }
        [Display(Name = "拆除标记")]
        public Boolean? IsDismantle { get; set; }

        protected override void InitVM()
        {
        }

    }
}
