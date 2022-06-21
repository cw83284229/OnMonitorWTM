using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.DVRInfoCheckVMs
{
    public partial class DVRInfoCheckVM : BaseCRUDVM<DVRInfoCheck>
    {

        public DVRInfoCheckVM()
        {
            SetInclude(x => x.DVR);
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
