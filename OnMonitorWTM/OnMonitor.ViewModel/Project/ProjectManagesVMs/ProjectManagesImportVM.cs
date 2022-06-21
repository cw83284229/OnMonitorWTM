using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.ProjectManagesVMs
{
    public partial class ProjectManagesTemplateVM : BaseTemplateVM
    {
        [Display(Name = "工程类型")]
        public ExcelPropety ProjectManageType_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.ProjectManageType);
        [Display(Name = "工程名称")]
        public ExcelPropety ProjectName_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.ProjectName);
        [Display(Name = "工程单号")]
        public ExcelPropety ProjectOrder_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.ProjectOrder);
        [Display(Name = "开始时间")]
        public ExcelPropety StartWorkDate_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.StartWorkDate);
        [Display(Name = "完工时间")]
        public ExcelPropety CompleteDate_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.CompleteDate);
        [Display(Name = "验收时间")]
        public ExcelPropety AcceptanceData_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.AcceptanceData);
        [Display(Name = "施工厂商")]
        public ExcelPropety ManufacturerName_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.ManufacturerName);
        [Display(Name = "工程说明")]
        public ExcelPropety ProjectSpecifications_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.ProjectSpecifications);
        [Display(Name = "楼栋")]
        public ExcelPropety Build_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.Build);
        [Display(Name = "楼层")]
        public ExcelPropety Floor_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.Floor);
        [Display(Name = "配电信息")]
        public ExcelPropety PowerInfo_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.PowerInfo);
        [Display(Name = "报警设备")]
        public ExcelPropety AlarmInfo_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.AlarmInfo);
        [Display(Name = "验收说明")]
        public ExcelPropety AcceptanceResult_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.AcceptanceResult);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<ProjectManages>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class ProjectManagesImportVM : BaseImportVM<ProjectManagesTemplateVM, ProjectManages>
    {

    }

}
