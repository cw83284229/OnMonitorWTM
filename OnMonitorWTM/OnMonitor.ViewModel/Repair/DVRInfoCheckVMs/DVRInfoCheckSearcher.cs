using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.DVRInfoCheckVMs
{
    public partial class DVRInfoCheckSearcher : BaseSearcher
    {
        [Display(Name = "监控室")]
        public Guid[] Monitor_Room { get; set; }
        //[Display(Name = "主机号")]
        //public Guid? DVRId { get; set; }
        //[Display(Name = "在线状态")]
        //public CheckState? DVR_Online { get; set; }
        [Display(Name = "时间检查")]
        public CheckState? TimeInfoChenk { get; set; }
        [Display(Name = "硬盘检查")]
        public CheckState? DiskChenk { get; set; }
        [Display(Name = "SN检查")]
        public CheckState? SNChenk { get; set; }
        [Display(Name = "90天检查")]
        public CheckState? VideoCheck90Day { get; set; }
        [Display(Name ="存储时间")]
        public int[] VideoStarageTime { get; set; }

        [Display(Name = "更新时间")]
        public DateRange UpdateTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
