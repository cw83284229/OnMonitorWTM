using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.CameraVMs
{
    public partial class CameraListVM : BasePagedListVM<Camera_View, CameraSearcher>
    {

        protected override IEnumerable<IGridColumn<Camera_View>> InitGridHeader()
        {
            return new List<GridColumn<Camera_View>>{
                 this.MakeGridHeader(x => x.MonitorRoom),
                this.MakeGridHeader(x => x.DVR_ID_view),
                this.MakeGridHeader(x => x.channel_ID),
                this.MakeGridHeader(x => x.Camera_ID),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.floor),
                this.MakeGridHeader(x => x.Direction),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.MonitorClassification),
                this.MakeGridHeader(x => x.CameraTpye),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.install_time),
                this.MakeGridHeader(x => x.manufacturer),
                this.MakeGridHeader(x => x.category),
                this.MakeGridHeader(x => x.Camera_IP),
                this.MakeGridHeader(x => x.Camera_port),
                this.MakeGridHeader(x => x.Camera_usre),
                this.MakeGridHeader(x => x.DVR_possword),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Camera_View> GetSearchQuery()
        {
            var query = DC.Set<Camera>()
                .CheckEqual(Searcher.monitorRoomId,x=>x.DVR.monitorRoom.ID)
                .CheckEqual(Searcher.DVRId, x=>x.DVRId)
                .CheckContain(Searcher.Camera_ID, x=>x.Camera_ID)
                .CheckContain(Searcher.Build, x=>x.Build)
                .CheckContain(Searcher.floor, x=>x.floor)
                .CheckContain(Searcher.Direction, x=>x.Direction)
                .CheckContain(Searcher.Location, x=>x.Location)
                .CheckEqual(Searcher.DepartmentId, x=>x.DepartmentId)
                .CheckContain(Searcher.manufacturer, x=>x.manufacturer)
                .Select(x => new Camera_View
                {
                    MonitorRoom=x.DVR.monitorRoom.RoomLocation,
				    ID = x.ID,
                    DVR_ID_view = x.DVR.DVR_ID,
                    channel_ID = x.channel_ID,
                    Camera_ID = x.Camera_ID,
                    Build = x.Build,
                    floor = x.floor,
                    Direction = x.Direction,
                    Location = x.Location,
                    MonitorClassification = x.MonitorClassification,
                    CameraTpye = x.CameraTpye,
                    Name_view = x.Department.Name,
                    install_time = x.install_time,
                    manufacturer = x.manufacturer,
                    category = x.category,
                    Camera_IP = x.Camera_IP,
                    Camera_port = x.Camera_port,
                    Camera_usre = x.Camera_usre,
                    DVR_possword = x.DVR_possword,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Camera_View : Camera{
        [Display(Name = "监控室")]
        public String MonitorRoom { get; set; }
        [Display(Name = "主机号")]
        public String DVR_ID_view { get; set; }
        [Display(Name = "部门名称")]
        public String Name_view { get; set; }

    }
}
