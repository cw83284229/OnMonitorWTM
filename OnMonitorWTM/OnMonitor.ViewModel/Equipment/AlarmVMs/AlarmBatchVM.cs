using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmVMs
{
    public partial class AlarmBatchVM : BaseBatchVM<Alarm, Alarm_BatchEdit>
    {
        public AlarmBatchVM()
        {
            ListVM = new AlarmListVM();
            LinkedVM = new Alarm_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Alarm_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
