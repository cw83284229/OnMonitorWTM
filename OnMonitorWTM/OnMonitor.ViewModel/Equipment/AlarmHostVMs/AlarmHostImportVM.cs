using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmHostVMs
{
    public partial class AlarmHostTemplateVM : BaseTemplateVM
    {
        public ExcelPropety MonitorRoom_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.MonitorRoomId);
        [Display(Name = "报警主机号")]
        public ExcelPropety AlarmHost_ID_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.AlarmHost_ID);
        [Display(Name = "账号")]
        public ExcelPropety User_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.User);
        [Display(Name = "密码")]
        public ExcelPropety Password_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.Password);
        [Display(Name = "报警主机类型")]
        public ExcelPropety AlarmHostType_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.AlarmHostType);
        [Display(Name = "IP地址")]
        public ExcelPropety AlarmHostIP_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.AlarmHostIP);
        [Display(Name = "通道数量")]
        public ExcelPropety AlarmChannelCount_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.AlarmChannelCount);
        [Display(Name = "安装时间")]
        public ExcelPropety install_time_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.install_time);
        [Display(Name = "安装厂商")]
        public ExcelPropety category_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.category);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<AlarmHost>(x => x.Remark);

	    protected override void InitVM()
        {
            MonitorRoom_Excel.DataType = ColumnDataType.ComboBox;
            MonitorRoom_Excel.ListItems = DC.Set<MonitorRoom>().GetSelectListItems(Wtm, y => y.RoomLocation);
        }

    }

    public class AlarmHostImportVM : BaseImportVM<AlarmHostTemplateVM, AlarmHost>
    {

    }

}
