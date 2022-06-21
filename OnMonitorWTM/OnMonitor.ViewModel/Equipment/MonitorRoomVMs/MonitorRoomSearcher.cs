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
    public partial class MonitorRoomSearcher : BaseSearcher
    {
        [Display(Name = "厂区")]
        public String Factory { get; set; }
        [Display(Name = "监控室")]
        public String RoomLocation { get; set; }
        [Display(Name = "监控室类别")]
        public String RoomType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
