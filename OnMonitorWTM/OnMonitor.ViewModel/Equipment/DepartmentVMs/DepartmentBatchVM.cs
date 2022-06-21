using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.DepartmentVMs
{
    public partial class DepartmentBatchVM : BaseBatchVM<Department, Department_BatchEdit>
    {
        public DepartmentBatchVM()
        {
            ListVM = new DepartmentListVM();
            LinkedVM = new Department_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Department_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
