using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Project.CameraLayoutVMs
{
    public partial class CameraLayoutTemplateVM : BaseTemplateVM
    {
        public ExcelPropety monitorRoom_Excel = ExcelPropety.CreateProperty<CameraLayout>(x => x.monitorRoomId);
        [Display(Name = "楼栋")]
        public ExcelPropety Build_Excel = ExcelPropety.CreateProperty<CameraLayout>(x => x.Build);
        [Display(Name = "楼层")]
        public ExcelPropety Floor_Excel = ExcelPropety.CreateProperty<CameraLayout>(x => x.Floor);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<CameraLayout>(x => x.Remark);

	    protected override void InitVM()
        {
            monitorRoom_Excel.DataType = ColumnDataType.ComboBox;
            monitorRoom_Excel.ListItems = DC.Set<MonitorRoom>().GetSelectListItems(Wtm, y => y.RoomLocation);
        }

    }

    public class CameraLayoutImportVM : BaseImportVM<CameraLayoutTemplateVM, CameraLayout>
    {

    }

}
