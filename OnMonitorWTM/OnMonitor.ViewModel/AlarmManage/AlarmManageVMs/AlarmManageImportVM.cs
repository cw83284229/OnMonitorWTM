using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.AlarmManages.AlarmManageVMs
{
    public partial class AlarmManageTemplateVM : BaseTemplateVM
    {
        [Display(Name = "报警号")]
        public ExcelPropety Alarm_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.AlarmId);
        [Display(Name = "报警时间")]
        public ExcelPropety AlarmTime_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.AlarmTime);
        [Display(Name = "撤防时间")]
        public ExcelPropety WithdrawTime_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.WithdrawTime);
        [Display(Name = "操作员")]
        public ExcelPropety WithdrawMan_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.WithdrawMan);
        [Display(Name = "撤防备注")]
        public ExcelPropety WithdrawRemark_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.WithdrawRemark);
        [Display(Name = "撤防类型")]
        public ExcelPropety WithdrawType_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.WithdrawType);
        [Display(Name = "布防时间")]
        public ExcelPropety DefenceTime_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.DefenceTime);
        [Display(Name = "机动岗处理时间")]
        public ExcelPropety TreatmentTime_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.TreatmentTime);
        [Display(Name = "当前处理状态")]
        public ExcelPropety TreatmentTimeState_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.TreatmentTimeState);
        [Display(Name = "现场机动岗")]
        public ExcelPropety TreatmentMan_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.TreatmentMan);
        [Display(Name = "现场处理回复")]
        public ExcelPropety TreatmentReply_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.TreatmentReply);
        [Display(Name = "报警类型")]
        public ExcelPropety AlarmMessage_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.AlarmType);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<AlarmManage>(x => x.Remark);

	    protected override void InitVM()
        {
            Alarm_Excel.DataType = ColumnDataType.ComboBox;
            Alarm_Excel.ListItems = DC.Set<Alarm>().GetSelectListItems(Wtm, y => y.Alarm_ID);
        }

    }

    public class AlarmManageImportVM : BaseImportVM<AlarmManageTemplateVM, AlarmManage>
    {

    }

}
