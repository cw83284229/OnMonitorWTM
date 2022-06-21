using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.CameraLayoutVMs
{
    public partial class CameraLayoutBatchVM : BaseBatchVM<CameraLayout, CameraLayout_BatchEdit>
    {
        public CameraLayoutBatchVM()
        {
            ListVM = new CameraLayoutListVM();
            LinkedVM = new CameraLayout_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CameraLayout_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
