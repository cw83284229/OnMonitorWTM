using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.ProjectChangeCameraVMs
{
    public partial class ProjectChangeCameraBatchVM : BaseBatchVM<ProjectChangeCamera, ProjectChangeCamera_BatchEdit>
    {
        public ProjectChangeCameraBatchVM()
        {
            ListVM = new ProjectChangeCameraListVM();
            LinkedVM = new ProjectChangeCamera_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProjectChangeCamera_BatchEdit : BaseVM
    {
        [Display(Name = "改造状态")]
        public TransformationStatus? TransformationStatus { get; set; }
        [Display(Name = "拆除标记")]
        public Boolean? IsDismantle { get; set; }

        protected override void InitVM()
        {
        }

    }

}
