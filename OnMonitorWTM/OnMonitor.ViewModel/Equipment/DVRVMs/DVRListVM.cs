using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.DVRVMs
{
    public partial class DVRListVM : BasePagedListVM<DVR_View, DVRSearcher>
    {

        protected override IEnumerable<IGridColumn<DVR_View>> InitGridHeader()
        {
            return new List<GridColumn<DVR_View>>{
                this.MakeGridHeader(x => x.RoomLocation_view),
                this.MakeGridHeader(x => x.DVR_ID),
                this.MakeGridHeader(x => x.Home_server),
                this.MakeGridHeader(x => x.Hard_drive),
                this.MakeGridHeader(x => x.DVR_IP),
                this.MakeGridHeader(x => x.DVR_port),
                this.MakeGridHeader(x => x.DVR_usre),
                this.MakeGridHeader(x => x.DVR_possword),
                this.MakeGridHeader(x => x.install_time),
                this.MakeGridHeader(x => x.Manufacturer),
                this.MakeGridHeader(x => x.DVR_type),
                this.MakeGridHeader(x => x.DVR_SN),
                this.MakeGridHeader(x => x.DVR_Channel),
                this.MakeGridHeader(x => x.department),
                this.MakeGridHeader(x => x.Cost_code),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DVR_View> GetSearchQuery()
        {
            var query = DC.Set<DVR>()
                .CheckEqual(Searcher.monitorRoomId, x=>x.monitorRoomId)
                .CheckContain(Searcher.DVR_ID, x=>x.DVR_ID)
                .CheckContain(Searcher.DVR_IP, x=>x.DVR_IP)
                .CheckContain(Searcher.department, x=>x.department)
                .Select(x => new DVR_View
                {
				    ID = x.ID,
                    RoomLocation_view = x.monitorRoom.RoomLocation,
                    DVR_ID = x.DVR_ID,
                    Home_server = x.Home_server,
                    Hard_drive = x.Hard_drive,
                    DVR_IP = x.DVR_IP,
                    DVR_port = x.DVR_port,
                    DVR_usre = x.DVR_usre,
                    DVR_possword = x.DVR_possword,
                    install_time = x.install_time,
                    Manufacturer = x.Manufacturer,
                    DVR_type = x.DVR_type,
                    DVR_SN = x.DVR_SN,
                    DVR_Channel = x.DVR_Channel,
                    department = x.department,
                    Cost_code = x.Cost_code,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DVR_View : DVR{
        [Display(Name = "监控室")]
        public String RoomLocation_view { get; set; }

    }
}
