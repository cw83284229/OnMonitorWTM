using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmVMs
{
    public partial class AlarmListVM : BasePagedListVM<Alarm_View, AlarmSearcher>
    {

        protected override IEnumerable<IGridColumn<Alarm_View>> InitGridHeader()
        {
            return new List<GridColumn<Alarm_View>>{
                this.MakeGridHeader(x => x.AlarmHost_ID_view),
                this.MakeGridHeader(x => x.Alarm_ID),
                this.MakeGridHeader(x => x.Channel_ID),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.floor),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.GeteType),
                this.MakeGridHeader(x => x.SensorType),
                this.MakeGridHeader(x => x.department),
                this.MakeGridHeader(x => x.Cost_code),
                this.MakeGridHeader(x => x.install_time),
                this.MakeGridHeader(x => x.category),
                this.MakeGridHeader(x => x.InsideCamera_ID),
                this.MakeGridHeader(x => x.OutsideCamera_ID),
                this.MakeGridHeader(x => x.IsAlertor),
                this.MakeGridHeader(x => x.IsOpenOrClosed),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Alarm_View> GetSearchQuery()
        {
            var query = DC.Set<Alarm>()
                .CheckEqual(Searcher.AlarmHostID, x=>x.AlarmHostID)
                .CheckContain(Searcher.Alarm_ID, x=>x.Alarm_ID)
                .CheckContain(Searcher.Build, x=>x.Build)
                .CheckContain(Searcher.floor, x=>x.floor)
                .CheckContain(Searcher.Location, x=>x.Location)
                .Select(x => new Alarm_View
                {
				    ID = x.ID,
                    AlarmHost_ID_view = x.AlarmHost.AlarmHost_ID,
                    Alarm_ID = x.Alarm_ID,
                    Channel_ID = x.Channel_ID,
                    Build = x.Build,
                    floor = x.floor,
                    Location = x.Location,
                    GeteType = x.GeteType,
                    SensorType = x.SensorType,
                    department = x.department,
                    Cost_code = x.Cost_code,
                    install_time = x.install_time,
                    category = x.category,
                    InsideCamera_ID = x.InsideCamera_ID,
                    OutsideCamera_ID = x.OutsideCamera_ID,
                    IsAlertor = x.IsAlertor,
                    IsOpenOrClosed = x.IsOpenOrClosed,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Alarm_View : Alarm{
        [Display(Name = "报警主机号")]
        public String AlarmHost_ID_view { get; set; }

    }
}
