using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmHostVMs
{
    public partial class AlarmHostBatchVM : BaseBatchVM<AlarmHost, AlarmHost_BatchEdit>
    {
        public AlarmHostBatchVM()
        {
            ListVM = new AlarmHostListVM();
            LinkedVM = new AlarmHost_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class AlarmHost_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
