using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.AlarmManages.AlarmManageVMs
{
    public partial class AlarmManageVM : BaseCRUDVM<AlarmManage>
    {

        public AlarmManageVM()
        {
            SetInclude(x => x.Alarm);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
