using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.ProjectManagesVMs
{
    public partial class ProjectManagesBatchVM : BaseBatchVM<ProjectManages, ProjectManages_BatchEdit>
    {
        public ProjectManagesBatchVM()
        {
            ListVM = new ProjectManagesListVM();
            LinkedVM = new ProjectManages_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProjectManages_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
