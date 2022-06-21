using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.MonitorRoomVMs
{
    public partial class MonitorRoomTemplateVM : BaseTemplateVM
    {
        [Display(Name = "厂区")]
        public ExcelPropety Factory_Excel = ExcelPropety.CreateProperty<MonitorRoom>(x => x.Factory);
        [Display(Name = "监控室")]
        public ExcelPropety RoomLocation_Excel = ExcelPropety.CreateProperty<MonitorRoom>(x => x.RoomLocation);
        [Display(Name = "监控室类别")]
        public ExcelPropety RoomType_Excel = ExcelPropety.CreateProperty<MonitorRoom>(x => x.RoomType);

	    protected override void InitVM()
        {
        }

    }

    public class MonitorRoomImportVM : BaseImportVM<MonitorRoomTemplateVM, MonitorRoom>
    {

    }

}
