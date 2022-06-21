using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmVMs
{
    public partial class AlarmSearcher : BaseSearcher
    {
        [Display(Name = "报警主机")]
        public Guid? AlarmHostID { get; set; }
        [Display(Name = "报警编号")]
        public String Alarm_ID { get; set; }
        [Display(Name = "楼栋")]
        public String Build { get; set; }
        [Display(Name = "楼层")]
        public String floor { get; set; }
        [Display(Name = "位置")]
        public String Location { get; set; }

        protected override void InitVM()
        {
        }

    }
}
