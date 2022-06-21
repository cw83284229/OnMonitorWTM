using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmVMs
{
    public partial class AlarmTemplateVM : BaseTemplateVM
    {
        public ExcelPropety AlarmHost_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.AlarmHostID);
        [Display(Name = "报警编号")]
        public ExcelPropety Alarm_ID_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Alarm_ID);
        [Display(Name = "通道号")]
        public ExcelPropety Channel_ID_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Channel_ID);
        [Display(Name = "楼栋")]
        public ExcelPropety Build_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Build);
        [Display(Name = "楼层")]
        public ExcelPropety floor_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.floor);
        [Display(Name = "位置")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Location);
        [Display(Name = "岗位类型")]
        public ExcelPropety GeteType_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.GeteType);
        [Display(Name = "传感器类型")]
        public ExcelPropety SensorType_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.SensorType);
        [Display(Name = "部门")]
        public ExcelPropety department_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.department);
        [Display(Name = "费用代码")]
        public ExcelPropety Cost_code_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Cost_code);
        [Display(Name = "安装时间")]
        public ExcelPropety install_time_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.install_time);
        [Display(Name = "安装厂商")]
        public ExcelPropety category_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.category);
        [Display(Name = "内侧镜头号")]
        public ExcelPropety InsideCamera_ID_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.InsideCamera_ID);
        [Display(Name = "外侧镜头号")]
        public ExcelPropety OutsideCamera_ID_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.OutsideCamera_ID);
        [Display(Name = "有无报警器")]
        public ExcelPropety IsAlertor_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.IsAlertor);
        [Display(Name = "开岗状态")]
        public ExcelPropety IsOpenOrClosed_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.IsOpenOrClosed);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Alarm>(x => x.Remark);

	    protected override void InitVM()
        {
            AlarmHost_Excel.DataType = ColumnDataType.ComboBox;
            AlarmHost_Excel.ListItems = DC.Set<AlarmHost>().GetSelectListItems(Wtm, y => y.AlarmHost_ID);
        }

    }

    public class AlarmImportVM : BaseImportVM<AlarmTemplateVM, Alarm>
    {
        public override DuplicatedInfo<Alarm> SetDuplicatedCheck()
        {
            //筛选重复数据

            var rv = CreateFieldsInfo();
            rv.AddGroup(SimpleField(x => x.Alarm_ID), SimpleField(x => x.Channel_ID));

            return rv;

        }
    }

}
