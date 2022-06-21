using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.CameraRepairVMs
{
    public partial class CameraRepairTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Camera_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.CameraId);
        [Display(Name = "异常时间")]
        public ExcelPropety AnomalyTime_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.AnomalyTime);
        [Display(Name = "统计时间")]
        public ExcelPropety CollectTime_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.CollectTime);
        [Display(Name = "异常类别")]
        public ExcelPropety AnomalyType_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.AnomalyType);
        [Display(Name = "异常等级")]
        public ExcelPropety AnomalyGrade_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.AnomalyGrade);
        [Display(Name = "统计人员")]
        public ExcelPropety Registrar_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.Registrar);
        [Display(Name = "修复状态")]
        public ExcelPropety RepairState_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.RepairState);
        [Display(Name = "修复时间")]
        public ExcelPropety RepairedTime_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.RepairedTime);
        [Display(Name = "维修人员")]
        public ExcelPropety Accendant_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.Accendant);
        [Display(Name = "维修内容")]
        public ExcelPropety RepairDetails_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.RepairDetails);
        [Display(Name = "维修厂商")]
        public ExcelPropety RepairFirm_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.RepairFirm);
        [Display(Name = "确认人")]
        public ExcelPropety Supervisor_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.Supervisor);
        [Display(Name = "更换设备")]
        public ExcelPropety ReplacePart_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.ReplacePart);
        [Display(Name = "工程异常")]
        public ExcelPropety ProjectAnomaly_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.ProjectAnomaly);
        [Display(Name = "异常确认")]
        public ExcelPropety NoSignal_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.NoSignal);
        [Display(Name = "维修确认")]
        public ExcelPropety AnomalyIdentification_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.AnomalyIdentification);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<CameraRepair>(x => x.Remark);

	    protected override void InitVM()
        {
            Camera_Excel.DataType = ColumnDataType.ComboBox;
            Camera_Excel.ListItems = DC.Set<Camera>().GetSelectListItems(Wtm, y => y.Camera_ID);
        }

    }

    public class CameraRepairImportVM : BaseImportVM<CameraRepairTemplateVM, CameraRepair>
    {

    }

}
