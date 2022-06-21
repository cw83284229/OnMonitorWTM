using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;


namespace OnMonitor.ViewModel.Repair.CameraRepairVMs
{
    public partial class CameraRepairBatchVM : BaseBatchVM<CameraRepair, CameraRepair_BatchEdit>
    {
        public CameraRepairBatchVM()
        {
            ListVM = new CameraRepairListVM();
            LinkedVM = new CameraRepair_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CameraRepair_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
