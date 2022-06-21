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
    public partial class DVRSearcher : BaseSearcher
    {
        [Display(Name = "监控室")]
        public Guid? monitorRoomId { get; set; }
        [Display(Name = "主机号")]
        public String DVR_ID { get; set; }
        [Display(Name = "IP")]
        public String DVR_IP { get; set; }
        [Display(Name = "部门")]
        public String department { get; set; }

        protected override void InitVM()
        {
        }

    }
}
