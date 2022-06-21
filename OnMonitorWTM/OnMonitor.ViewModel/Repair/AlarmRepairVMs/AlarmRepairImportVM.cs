using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.AlarmRepairVMs
{
    public partial class AlarmRepairTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Alarm_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.AlarmId);
        [Display(Name = "测试时间")]
        public ExcelPropety TestTime_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.TestTime);
        [Display(Name = "异常时间")]
        public ExcelPropety AnomalyTime_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.AnomalyTime);
        [Display(Name = "异常状态")]
        public ExcelPropety AlarmAnomalyState_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.AlarmAnomalyState);
        [Display(Name = "测试人员")]
        public ExcelPropety Registrar_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.Registrar);
        [Display(Name = "修复状态")]
        public ExcelPropety RepairState_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.RepairState);
        [Display(Name = "修复时间")]
        public ExcelPropety RepairedTime_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.RepairedTime);
        [Display(Name = "维修人员")]
        public ExcelPropety Accendant_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.Accendant);
        [Display(Name = "维修内容")]
        public ExcelPropety RepairDetails_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.RepairDetails);
        [Display(Name = "维修厂商")]
        public ExcelPropety RepairFirm_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.RepairFirm);
        [Display(Name = "确认人")]
        public ExcelPropety Supervisor_Excel = ExcelPropety.CreateProperty<AlarmRepair>(x => x.Supervisor);

	    protected override void InitVM()
        {
            Alarm_Excel.DataType = ColumnDataType.ComboBox;
            Alarm_Excel.ListItems = DC.Set<Alarm>().GetSelectListItems(Wtm, y => y.Alarm_ID);
        }

    }

    public class AlarmRepairImportVM : BaseImportVM<AlarmRepairTemplateVM, AlarmRepair>
    {

    }

}
