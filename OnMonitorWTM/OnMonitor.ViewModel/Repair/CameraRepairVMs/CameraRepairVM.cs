using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.CameraRepairVMs
{
    public partial class CameraRepairVM : BaseCRUDVM<CameraRepair>
    {


        [Display(Name = "镜头号")]
        //[Required(ErrorMessage = "此项不能为空")]
        public string Camera_ID { get; set; }
        public CameraRepairVM()
        {
            SetInclude(x => x.Camera);
        }

        public override DuplicatedInfo<CameraRepair> SetDuplicatedCheck()
        {
           
            
            var rv = CreateFieldsInfo();
            rv.AddGroup(SimpleField(x => x.CameraId), SimpleField(x => x.RepairState));
            
            return rv;

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
