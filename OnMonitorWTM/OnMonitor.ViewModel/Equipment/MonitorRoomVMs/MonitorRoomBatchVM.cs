using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.MonitorRoomVMs
{
    public partial class MonitorRoomBatchVM : BaseBatchVM<MonitorRoom, MonitorRoom_BatchEdit>
    {
        public MonitorRoomBatchVM()
        {
            ListVM = new MonitorRoomListVM();
            LinkedVM = new MonitorRoom_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class MonitorRoom_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
