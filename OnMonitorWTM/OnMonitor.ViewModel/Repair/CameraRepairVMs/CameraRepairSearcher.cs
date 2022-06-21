using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;
using OnMonitor.Model.Repair;

namespace OnMonitor.ViewModel.Repair.CameraRepairVMs
{
    public partial class CameraRepairSearcher : BaseSearcher
    {
        public Guid? CameraId { get; set; }
        [Display(Name = "监控室")]
        public Guid[] Monitor_Room { get; set; }

        [Display(Name = "位置")]
        public String Location { get; set; }
        [Display(Name = "异常时间")]
        public DateRange AnomalyTime { get; set; }
        [Display(Name = "统计时间")]
        public DateRange CollectTime { get; set; }
        [Display(Name = "异常类别")]
        public IEnumerable<AnomalyType> AnomalyType { get; set; }
        [Display(Name = "异常等级")]
        public IEnumerable<AnomalyGrade> AnomalyGrade { get; set; }
        [Display(Name = "修复状态")]
        public RepairState? RepairState { get; set; } = Model.Repair.RepairState.Untreated;
        [Display(Name = "修复时间")]
        public DateRange RepairedTime { get; set; }
        [Display(Name = "维修确认")]
        public AnomalyIdentification? AnomalyIdentification { get; set; }

        protected override void InitVM()
        {

           // CollectTime = new DateRange(DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2000-01-01 00:00:00"));
           // RepairedTime= new DateRange(DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2000-01-01 00:00:00"));

        }

    }
}
