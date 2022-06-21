using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.DVRVMs
{
    public partial class DVRTemplateVM : BaseTemplateVM
    {
        public ExcelPropety monitorRoom_Excel = ExcelPropety.CreateProperty<DVR>(x => x.monitorRoomId);
        [Display(Name = "主机号")]
        public ExcelPropety DVR_ID_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_ID);
        [Display(Name = "归属服务器")]
        public ExcelPropety Home_server_Excel = ExcelPropety.CreateProperty<DVR>(x => x.Home_server);
        [Display(Name = "硬盘容量")]
        public ExcelPropety Hard_drive_Excel = ExcelPropety.CreateProperty<DVR>(x => x.Hard_drive);
        [Display(Name = "IP")]
        public ExcelPropety DVR_IP_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_IP);
        [Display(Name = "端口")]
        public ExcelPropety DVR_port_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_port);
        [Display(Name = "账号")]
        public ExcelPropety DVR_usre_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_usre);
        [Display(Name = "密码")]
        public ExcelPropety DVR_possword_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_possword);
        [Display(Name = "安装时间")]
        public ExcelPropety install_time_Excel = ExcelPropety.CreateProperty<DVR>(x => x.install_time);
        [Display(Name = "安装厂商")]
        public ExcelPropety Manufacturer_Excel = ExcelPropety.CreateProperty<DVR>(x => x.Manufacturer);
        [Display(Name = "DVR类型")]
        public ExcelPropety DVR_type_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_type);
        [Display(Name = "SN号")]
        public ExcelPropety DVR_SN_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_SN);
        [Display(Name = "监控数量")]
        public ExcelPropety DVR_Channel_Excel = ExcelPropety.CreateProperty<DVR>(x => x.DVR_Channel);
        [Display(Name = "部门")]
        public ExcelPropety department_Excel = ExcelPropety.CreateProperty<DVR>(x => x.department);
        [Display(Name = "费用代码")]
        public ExcelPropety Cost_code_Excel = ExcelPropety.CreateProperty<DVR>(x => x.Cost_code);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<DVR>(x => x.Remark);

	    protected override void InitVM()
        {
            monitorRoom_Excel.DataType = ColumnDataType.ComboBox;
            monitorRoom_Excel.ListItems = DC.Set<MonitorRoom>().GetSelectListItems(Wtm, y => y.RoomLocation);
        }

    }

    public class DVRImportVM : BaseImportVM<DVRTemplateVM, DVR>
    {

    }

}
