using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Project.ProjectChangeCameraVMs
{
    public partial class ProjectChangeCameraTemplateVM : BaseTemplateVM
    {
       
        public ExcelPropety ProjectManages_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.ProjectManagesId);
        public ExcelPropety Camera_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.CameraId);
        [Display(Name = "改造后位置")]
        public ExcelPropety ChangeLocation_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.ChangeLocation);
        [Display(Name = "改造状态")]
        public ExcelPropety TransformationStatus_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.TransformationStatus);
        [Display(Name = "拆除标记")]
        public ExcelPropety IsDismantle_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.IsDismantle);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<ProjectChangeCamera>(x => x.Remark);

	    protected override void InitVM()
        {
            ProjectManages_Excel.DataType = ColumnDataType.ComboBox;
            ProjectManages_Excel.ListItems = DC.Set<ProjectManages>().GetSelectListItems(Wtm, y => y.ProjectName);
            Camera_Excel.DataType = ColumnDataType.ComboBox;
            Camera_Excel.ListItems = DC.Set<Camera>().GetSelectListItems(Wtm, y => y.Camera_ID);
        }

    }

    public class ProjectChangeCameraImportVM : BaseImportVM<ProjectChangeCameraTemplateVM, ProjectChangeCamera>
    {

    }

}
