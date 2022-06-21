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
    public partial class DepartmentTemplateVM : BaseTemplateVM
    {
        [Display(Name = "部门名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Department>(x => x.Name);
        [Display(Name = "费用代码")]
        public ExcelPropety Cost_code_Excel = ExcelPropety.CreateProperty<Department>(x => x.Cost_code);

	    protected override void InitVM()
        {
        }

    }

    public class DepartmentImportVM : BaseImportVM<DepartmentTemplateVM, Department>
    {

    }

}
