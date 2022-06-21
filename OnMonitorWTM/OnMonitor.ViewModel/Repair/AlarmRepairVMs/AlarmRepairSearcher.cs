using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.AlarmRepairVMs
{
    public partial class AlarmRepairSearcher : BaseSearcher
    {
        [Display(Name = "报警编号")]
        public Guid? AlarmId { get; set; }
        [Display(Name = "测试时间")]
        public DateRange TestTime { get; set; }
        [Display(Name = "异常时间")]
        public DateRange AnomalyTime { get; set; }
        [Display(Name = "维修状态")]
        public RepairState? RepairState { get; set; }
        [Display(Name = "异常状态")]
        public AlarmAnomalyState? AlarmAnomalyState { get; set; }
        [Display(Name = "测试人员")]
        public String Registrar { get; set; }
        [Display(Name = "修复时间")]
        public DateRange RepairedTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
