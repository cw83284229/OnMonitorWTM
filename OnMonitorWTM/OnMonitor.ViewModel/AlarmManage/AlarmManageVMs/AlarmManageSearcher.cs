using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.AlarmManages.AlarmManageVMs
{
    public partial class AlarmManageSearcher : BaseSearcher
    {

        [Display(Name = "监控室")]
        public Guid? MonitorRoomId { get; set; }
        [Display(Name = "报警号")]
        public Guid? AlarmId { get; set; }
        [Display(Name = "报警时间")]
        public DateRange AlarmTime { get; set; }
        [Display(Name = "撤防类型")]
        public WithdrawType? WithdrawType { get; set; }
        [Display(Name = "布防时间")]
        public DateRange DefenceTime { get; set; }
       
        [Display(Name = "报警类型")]
        public AlarmMessageTypeEnum? AlarmType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
