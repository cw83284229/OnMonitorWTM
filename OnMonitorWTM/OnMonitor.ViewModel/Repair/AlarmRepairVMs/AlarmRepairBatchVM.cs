using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;


namespace OnMonitor.ViewModel.Repair.AlarmRepairVMs
{
    public partial class AlarmRepairBatchVM : BaseBatchVM<AlarmRepair, AlarmRepair_BatchEdit>
    {
        public AlarmRepairBatchVM()
        {
            ListVM = new AlarmRepairListVM();
            LinkedVM = new AlarmRepair_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class AlarmRepair_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
