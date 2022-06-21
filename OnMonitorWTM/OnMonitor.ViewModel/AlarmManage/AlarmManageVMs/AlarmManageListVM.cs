using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.AlarmManages.AlarmManageVMs
{
    public partial class AlarmManageListVM : BasePagedListVM<AlarmManage_View, AlarmManageSearcher>
    {

        protected override IEnumerable<IGridColumn<AlarmManage_View>> InitGridHeader()
        {
            return new List<GridColumn<AlarmManage_View>>{
                this.MakeGridHeader(x => x.MonitorRoom),
                this.MakeGridHeader(x => x.Alarm_ID),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.floor),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.AlarmTime),
                this.MakeGridHeader(x => x.WithdrawTime),
                this.MakeGridHeader(x => x.WithdrawMan),
                this.MakeGridHeader(x => x.WithdrawRemark),
                this.MakeGridHeader(x => x.WithdrawType),
                this.MakeGridHeader(x => x.DefenceTime),
                this.MakeGridHeader(x => x.TreatmentTime),
                this.MakeGridHeader(x => x.TreatmentTimeState),
                this.MakeGridHeader(x => x.TreatmentMan),
                this.MakeGridHeader(x => x.TreatmentReply),
                this.MakeGridHeader(x => x.AlarmType),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AlarmManage_View> GetSearchQuery()
        {
            var query = DC.Set<AlarmManage>()
                 .CheckEqual(Searcher.MonitorRoomId, x => x.Alarm.AlarmHost.MonitorRoomId)
                .CheckEqual(Searcher.AlarmId, x=>x.AlarmId)
                .CheckBetween(Searcher.AlarmTime?.GetStartTime(), Searcher.AlarmTime?.GetEndTime(), x => x.AlarmTime, includeMax: false)
                .CheckEqual(Searcher.WithdrawType, x => x.WithdrawType)
                .CheckBetween(Searcher.DefenceTime?.GetStartTime(), Searcher.DefenceTime?.GetEndTime(), x => x.DefenceTime, includeMax: false)
                .CheckEqual(Searcher.AlarmType, x=>x.AlarmType)
                .DPWhere(Wtm, x => x.Alarm.AlarmHost.MonitorRoomId)
                .Select(x => new AlarmManage_View
                {
				    ID = x.ID,
                    MonitorRoom=x.Alarm.AlarmHost.MonitorRoom.RoomLocation,
                    AlarmHost_ID=x.Alarm.AlarmHost.AlarmHost_ID,
                    Alarm_ID = x.Alarm.Alarm_ID,
                    Build=x.Alarm.Build,
                    floor=x.Alarm.floor,
                    Location=x.Alarm.Location,
                    AlarmTime = x.AlarmTime,
                    WithdrawTime = x.WithdrawTime,
                    WithdrawMan = x.WithdrawMan,
                    WithdrawRemark = x.WithdrawRemark,
                    WithdrawType = x.WithdrawType,
                    DefenceTime = x.DefenceTime,
                    TreatmentTime = x.TreatmentTime,
                    TreatmentTimeState = x.TreatmentTimeState,
                    TreatmentMan = x.TreatmentMan,
                    TreatmentReply = x.TreatmentReply,
                    AlarmType = x.AlarmType,
                    Remark = x.Remark,
                    UpdateTime=x.UpdateTime,    
                })
                 .OrderByDescending(x => x.UpdateTime);


            return query;

        }

    }

    public class AlarmManage_View : AlarmManage{
        [Display(Name = "监控室")]
        public String MonitorRoom { get; set; }
        [Display(Name = "主机号")]
        public String AlarmHost_ID { get; set; }

        [Display(Name = "报警编号")]
        public String Alarm_ID { get; set; }
        [Display(Name = "楼栋")]
        public string Build { get; set; }

        [Display(Name = "楼层")]
        public string floor { get; set; }

        [Display(Name = "位置")]
        public string Location { get; set; }

    }
}
