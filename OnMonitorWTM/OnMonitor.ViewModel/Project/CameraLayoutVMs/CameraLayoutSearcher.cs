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
    public partial class CameraLayoutSearcher : BaseSearcher
    {
        [Display(Name = "监控室")]
        public Guid? monitorRoomId { get; set; }
        [Display(Name = "楼栋")]
        public String Build { get; set; }
        [Display(Name = "楼层")]
        public String Floor { get; set; }

        protected override void InitVM()
        {
        }

    }
}
