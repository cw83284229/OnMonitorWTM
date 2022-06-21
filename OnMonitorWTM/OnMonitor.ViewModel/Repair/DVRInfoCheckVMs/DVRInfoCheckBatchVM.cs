using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;


namespace OnMonitor.ViewModel.Repair.DVRInfoCheckVMs
{
    public partial class DVRInfoCheckBatchVM : BaseBatchVM<DVRInfoCheck, DVRInfoCheck_BatchEdit>
    {
        public DVRInfoCheckBatchVM()
        {
            ListVM = new DVRInfoCheckListVM();
            LinkedVM = new DVRInfoCheck_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DVRInfoCheck_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
