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
    public partial class AlarmHostSearcher : BaseSearcher
    {
        [Display(Name = "监控室")]
        public Guid? MonitorRoomId { get; set; }
        [Display(Name = "报警主机号")]
        public String AlarmHost_ID { get; set; }
        [Display(Name = "IP地址")]
        public String AlarmHostIP { get; set; }

        protected override void InitVM()
        {
        }

    }
}
