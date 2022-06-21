using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.AlarmHostVMs
{
    public partial class AlarmHostListVM : BasePagedListVM<AlarmHost_View, AlarmHostSearcher>
    {

        protected override IEnumerable<IGridColumn<AlarmHost_View>> InitGridHeader()
        {
            return new List<GridColumn<AlarmHost_View>>{
                this.MakeGridHeader(x => x.RoomLocation_view),
                this.MakeGridHeader(x => x.AlarmHost_ID),
                this.MakeGridHeader(x => x.User),
                this.MakeGridHeader(x => x.Password),
                this.MakeGridHeader(x => x.AlarmHostType),
                this.MakeGridHeader(x => x.AlarmHostIP),
                this.MakeGridHeader(x => x.AlarmChannelCount),
                this.MakeGridHeader(x => x.install_time),
                this.MakeGridHeader(x => x.category),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<AlarmHost_View> GetSearchQuery()
        {
            var query = DC.Set<AlarmHost>()
                .CheckEqual(Searcher.MonitorRoomId, x=>x.MonitorRoomId)
                .CheckContain(Searcher.AlarmHost_ID, x=>x.AlarmHost_ID)
                .CheckContain(Searcher.AlarmHostIP, x=>x.AlarmHostIP)
                .Select(x => new AlarmHost_View
                {
				    ID = x.ID,
                    RoomLocation_view = x.MonitorRoom.RoomLocation,
                    AlarmHost_ID = x.AlarmHost_ID,
                    User = x.User,
                    Password = x.Password,
                    AlarmHostType = x.AlarmHostType,
                    AlarmHostIP = x.AlarmHostIP,
                    AlarmChannelCount = x.AlarmChannelCount,
                    install_time = x.install_time,
                    category = x.category,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class AlarmHost_View : AlarmHost{
        [Display(Name = "监控室")]
        public String RoomLocation_view { get; set; }

    }
}
