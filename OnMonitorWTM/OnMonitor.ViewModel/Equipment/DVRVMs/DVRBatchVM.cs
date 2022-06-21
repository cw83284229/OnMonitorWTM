using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.DVRVMs
{
    public partial class DVRBatchVM : BaseBatchVM<DVR, DVR_BatchEdit>
    {
        public DVRBatchVM()
        {
            ListVM = new DVRListVM();
            LinkedVM = new DVR_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DVR_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
