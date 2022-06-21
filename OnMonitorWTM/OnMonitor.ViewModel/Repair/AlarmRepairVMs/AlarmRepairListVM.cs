using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.AlarmRepairVMs
{
    public partial class AlarmRepairListVM : BasePagedListVM<AlarmRepair_View, AlarmRepairSearcher>
    {

        protected override IEnumerable<IGridColumn<AlarmRepair_View>> InitGridHeader()
        {
            return new List<GridColumn<AlarmRepair_View>>{
                this.MakeGridHeader(x => x.AlarmHost_ID_view),
                this.MakeGridHeader(x => x.Alarm_ID_view),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.floor),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.TestTime),
                this.MakeGridHeader(x => x.AnomalyTime),
                this.MakeGridHeader(x => x.AlarmAnomalyState),
                this.MakeGridHeader(x => x.Registrar),
                this.MakeGridHeader(x => x.RepairState),
                this.MakeGridHeader(x => x.RepairedTime),
                this.MakeGridHeader(x => x.Accendant),
                this.MakeGridHeader(x => x.RepairDetails),
                this.MakeGridHeader(x => x.RepairFirm),
                this.MakeGridHeader(x => x.Supervisor),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AlarmRepair_View> GetSearchQuery()
        {
            if (Searcher.RepairState==null)
            {
                Searcher.RepairState = RepairState.Untreated;
            }
            var query = DC.Set<AlarmRepair>()
                .CheckEqual(Searcher.AlarmId, x=>x.AlarmId)
                .CheckBetween(Searcher.TestTime?.GetStartTime(), Searcher.TestTime?.GetEndTime(), x => x.TestTime, includeMax: false)
                .CheckBetween(Searcher.AnomalyTime?.GetStartTime(), Searcher.AnomalyTime?.GetEndTime(), x => x.AnomalyTime, includeMax: false)
                .CheckEqual(Searcher.AlarmAnomalyState, x=>x.AlarmAnomalyState)
                .CheckEqual(Searcher.RepairState,x=>x.RepairState)     
                .CheckContain(Searcher.Registrar, x=>x.Registrar)
                .CheckBetween(Searcher.RepairedTime?.GetStartTime(), Searcher.RepairedTime?.GetEndTime(), x => x.RepairedTime, includeMax: false)
                .DPWhere(Wtm,x=>x.Alarm.AlarmHost.MonitorRoomId)
                .Select(x => new AlarmRepair_View
                {
                    ID = x.ID,
                    AlarmHost_ID_view = x.Alarm.AlarmHost.AlarmHost_ID,
                    Alarm_ID_view = x.Alarm.Alarm_ID,
                    Build = x.Alarm.Build,
                    floor = x.Alarm.floor,
                    Location = x.Alarm.Location,
                    TestTime = x.TestTime,
                    AnomalyTime = x.AnomalyTime,
                    AlarmAnomalyState = x.AlarmAnomalyState,
                    Registrar = x.Registrar,
                    RepairState = x.RepairState,
                    RepairedTime = x.RepairedTime,
                    Accendant = x.Accendant,
                    RepairDetails = x.RepairDetails,
                    RepairFirm = x.RepairFirm,
                    Supervisor = x.Supervisor,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class AlarmRepair_View : AlarmRepair{
        [Display(Name = "报警主机号")]
        public String AlarmHost_ID_view { get; set; }
        [Display(Name = "报警编号")]
        public String Alarm_ID_view { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼栋")]
        public string Build { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        [StringLength(55)]
        [Display(Name = "楼层")]
        public string floor { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(55)]
        [Display(Name = "位置")]
        public string Location { get; set; }

    }
}
