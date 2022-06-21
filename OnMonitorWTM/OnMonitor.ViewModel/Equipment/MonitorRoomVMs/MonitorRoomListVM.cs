using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.MonitorRoomVMs
{
    public partial class MonitorRoomListVM : BasePagedListVM<MonitorRoom_View, MonitorRoomSearcher>
    {

        protected override IEnumerable<IGridColumn<MonitorRoom_View>> InitGridHeader()
        {
            return new List<GridColumn<MonitorRoom_View>>{
                this.MakeGridHeader(x => x.Factory),
                this.MakeGridHeader(x => x.RoomLocation),
                this.MakeGridHeader(x => x.RoomType),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<MonitorRoom_View> GetSearchQuery()
        {
            var query = DC.Set<MonitorRoom>()
                .CheckContain(Searcher.Factory, x=>x.Factory)
                .CheckContain(Searcher.RoomLocation, x=>x.RoomLocation)
                .CheckContain(Searcher.RoomType, x=>x.RoomType)
                .Select(x => new MonitorRoom_View
                {
				    ID = x.ID,
                    Factory = x.Factory,
                    RoomLocation = x.RoomLocation,
                    RoomType = x.RoomType,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class MonitorRoom_View : MonitorRoom{

    }
}
