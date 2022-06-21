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


namespace OnMonitor.ViewModel.Repair.DVRInfoCheckVMs
{
    public partial class DVRInfoCheckListVM : BasePagedListVM<DVRInfoCheck_View, DVRInfoCheckSearcher>
    {

        protected override IEnumerable<IGridColumn<DVRInfoCheck_View>> InitGridHeader()
        {
            return new List<GridColumn<DVRInfoCheck_View>>{
                 this.MakeGridHeader(x => x.Monitor_Room),
                this.MakeGridHeader(x => x.DVR_ID_view),
                this.MakeGridHeader(x => x.DVR_SN),
                this.MakeGridHeader(x => x.DVR_Channel),
                this.MakeGridHeader(x => x.DiskTotal),
                this.MakeGridHeader(x => x.DVR_Online),
                this.MakeGridHeader(x => x.TimeInfoChenk),
                this.MakeGridHeader(x => x.DiskChenk),
                this.MakeGridHeader(x => x.SNChenk),
                this.MakeGridHeader(x => x.VideoCheck90Day),
                this.MakeGridHeader(x => x.VideoStarageTime),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DVRInfoCheck_View> GetSearchQuery()
        {
            var query = DC.Set<DVRInfoCheck>().Include(i => i.DVR).Include(x => x.DVR.monitorRoom)
                // .CheckEqual(Searcher.Monitor_Room, x => x.DVR.monitorRoom.ID)
                //.CheckEqual(Searcher.DVRId, x => x.DVRId)
                //.CheckEqual(Searcher.DVR_Online, x => x.DVR_Online)
                .CheckEqual(Searcher.TimeInfoChenk, x => x.TimeInfoChenk)
                .CheckEqual(Searcher.DiskChenk, x => x.DiskChenk)
                .CheckEqual(Searcher.SNChenk, x => x.SNChenk)
                .CheckEqual(Searcher.VideoCheck90Day, x => x.VideoCheck90Day)
                .DPWhere(Wtm,x=>x.DVR.monitorRoomId)
                .Select(x => new DVRInfoCheck_View
                {

                    ID = x.ID,
                    Monitor_Room=x.DVR.monitorRoom.RoomLocation, 
                    DVR_ID_view = x.DVR.DVR_ID,
                    DVR_SN = x.DVR_SN,
                    DVR_Channel = x.DVR_Channel,
                    DiskTotal = x.DiskTotal,
                    DVR_Online = x.DVR_Online,
                    TimeInfoChenk = x.TimeInfoChenk,
                    DiskChenk = x.DiskChenk,
                    SNChenk = x.SNChenk,
                    VideoCheck90Day = x.VideoCheck90Day,
                    VideoStarageTime=x.VideoStarageTime,
                    UpdateTime = x.UpdateTime,
                    Remark = x.Remark,
                    Monitor_RoomId = x.DVR.monitorRoomId
                })
                .OrderBy(x => x.ID);

            Searcher.UpdateTime = new DateRange(DateTime.Now.AddDays(-1), DateTime.Now);

            query = (IOrderedQueryable<DVRInfoCheck_View>)query.CheckBetween(Searcher.UpdateTime.GetStartTime(), Searcher.UpdateTime.GetEndTime(), u => u.UpdateTime);


            //监控室搜索
            if (Searcher.Monitor_Room != null && Searcher.Monitor_Room.Length > 0)
            {
                var query1 = query.CheckEqual(Searcher.Monitor_Room[0], u => u.Monitor_RoomId);

                for (int i = 1; i < Searcher.Monitor_Room.Length; i++)
                {

                    query1 = (IOrderedQueryable<DVRInfoCheck_View>)query1.Union(query.CheckEqual(Searcher.Monitor_Room[i], u => u.Monitor_RoomId));

                }

                query = (IOrderedQueryable<DVRInfoCheck_View>)query1;


            }
            return query;
        }

    }

    public class DVRInfoCheck_View : DVRInfoCheck
    {
        [Display(Name = "监控室")]
        public String Monitor_Room { get; set; }

        [Display(Name = "主机号")]
        public String DVR_ID_view { get; set; }

        public Guid Monitor_RoomId { get; set; }

    }
}
