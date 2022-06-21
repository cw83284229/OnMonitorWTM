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


namespace OnMonitor.ViewModel.Repair.CameraRepairVMs
{
    public partial class CameraRepairListVM : BasePagedListVM<CameraRepair_View, CameraRepairSearcher>
    {

        protected override IEnumerable<IGridColumn<CameraRepair_View>> InitGridHeader()
        {
            return new List<GridColumn<CameraRepair_View>>{
                this.MakeGridHeader(x => x.Monitor_Room),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.Floor),
                this.MakeGridHeader(x => x.Location),
                this.MakeGridHeader(x => x.DVR_ID),
                this.MakeGridHeader(x => x.Camera_ID_view),
                this.MakeGridHeader(x => x.DepartmentName),
                this.MakeGridHeader(x => x.install_time),
                this.MakeGridHeader(x => x.AnomalyTime),
                this.MakeGridHeader(x => x.CollectTime),
                this.MakeGridHeader(x => x.AnomalyType),
                this.MakeGridHeader(x => x.AnomalyGrade),
                this.MakeGridHeader(x => x.Registrar),
                this.MakeGridHeader(x => x.RepairState),
                this.MakeGridHeader(x => x.RepairedTime),
                this.MakeGridHeader(x => x.Accendant),
                this.MakeGridHeader(x => x.RepairDetails),
                this.MakeGridHeader(x => x.RepairFirm),
                this.MakeGridHeader(x => x.Supervisor),
                this.MakeGridHeader(x => x.ReplacePart),
                this.MakeGridHeader(x => x.ProjectAnomaly),
                this.MakeGridHeader(x => x.AnomalyIdentification),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<CameraRepair_View> GetSearchQuery()
        {
            var DD = LoginUserInfo.DataPrivileges;

            var query = DC.Set<CameraRepair>().Include(x=>x.Camera.DVR.monitorRoom)
                 // .CheckEqual(Searcher.Monitor_Room, x=>x.Camera.DVR.monitorRoom.ID.ToString())

                 .CheckBetween(Searcher.AnomalyTime?.GetStartTime(), Searcher.AnomalyTime?.GetEndTime(), x => x.AnomalyTime, includeMax: false)
                 .CheckBetween(Searcher.CollectTime?.GetStartTime(), Searcher.CollectTime?.GetEndTime(), x => x.CollectTime, includeMax: false)
                 .CheckBetween(Searcher.RepairedTime?.GetStartTime(), Searcher.RepairedTime?.GetEndTime(), x => x.RepairedTime, includeMax: false)
                .CheckEqual(Searcher.RepairState, x=>x.RepairState)
               // .CheckBetween(Searcher.RepairedTime.GetStartTime(),Searcher.RepairedTime.GetStartTime(), x=>Convert.ToDateTime(x.RepairedTime))
                .CheckEqual(Searcher.AnomalyIdentification, x=>x.AnomalyIdentification)
                .DPWhere(Wtm,x=>x.Camera.DVR.monitorRoomId)
                .Select(x => new CameraRepair_View
                {
				    ID = x.ID,
                    Monitor_Room=x.Camera.DVR.monitorRoom.RoomLocation,
                    DVR_ID=x.Camera.DVR.DVR_ID,
                    Build=x.Camera.Build,
                    Floor=x.Camera.floor,
                    Location=x.Camera.Direction+x.Camera.Location,
                    DepartmentName=x.Camera.Department.Name,
                    Camera_ID_view = x.Camera.Camera_ID,
                    AnomalyTime = x.AnomalyTime,
                    CollectTime = x.CollectTime,
                    AnomalyType = x.AnomalyType,
                    AnomalyGrade = x.AnomalyGrade,
                    Registrar = x.Registrar,
                    RepairState = x.RepairState,
                    RepairedTime = x.RepairedTime,
                    Accendant = x.Accendant,
                    RepairDetails = x.RepairDetails,
                    RepairFirm = x.RepairFirm,
                    Supervisor = x.Supervisor,
                    ReplacePart = x.ReplacePart,
                    ProjectAnomaly = x.ProjectAnomaly,
                    AnomalyIdentification = x.AnomalyIdentification,
                    Monitor_RoomId=x.Camera.DVR.monitorRoomId,
                    install_time=x.Camera.install_time,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);

            DateRange dateRange = new DateRange(DateTime.Parse("2000-01-01 00:00:00"), DateTime.Parse("2000-01-01 00:00:00"));
           //// 异常时间筛选
           // if (Searcher.CollectTime != null)
           // {
           //     string StartTime = ((DateTime)Searcher.CollectTime.GetStartTime()).ToString("yyyy-MM-dd HH:mm:ss");
           //     string EndTime = ((DateTime)Searcher.CollectTime.GetEndTime()).ToString("yyyy-MM-dd HH:mm:ss");
           //     query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => string.Compare(u.CollectTime,StartTime) >= 0 && string.Compare(u.CollectTime, EndTime) <= 0);
           // }


           // //维修时间筛选
           // if (Searcher.RepairedTime != null)
           // {
           //     string StartTime = ((DateTime)Searcher.RepairedTime.GetStartTime()).ToString("yyyy-MM-dd HH:mm:ss");
           //     string EndTime = ((DateTime)Searcher.RepairedTime.GetEndTime()).ToString("yyyy-MM-dd HH:mm:ss");
           //     query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => string.Compare(u.RepairedTime, StartTime) >= 0 && string.Compare(u.RepairedTime, EndTime) <= 0);
           // }
           // //模糊搜索
            if (!string.IsNullOrEmpty(Searcher.Location))
            {

                if (Searcher.Location.Length > 6)
                {
                    if (Searcher.Location[5].ToString().Equals("F") || Searcher.Location[7].ToString().Equals("F"))
                    {

                        if (query.Where(u => u.Build.Contains(Searcher.Location.Substring(0, 3))).ToList().Count != 0)
                        {
                            query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.Build.Contains(Searcher.Location.Substring(0, 3)));

                            string str1 = Searcher.Location.Split('F')[0];

                            if (query.Where(u => u.Floor.Contains(str1.Substring(4))).Count() != 0)
                            {
                                query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.Floor.Contains(str1.Substring(4)));
                                String str2 = Searcher.Location.Split('F')[1];
                                if (str2 != null)
                                {
                                    if (query.Where(u => u.Location.Contains(str2)).Count() != 0)
                                    {
                                        query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.Location.Contains(str2));
                                    }
                                    else
                                    {
                                        return null;
                                    }

                                }
                            }

                        }
                    }

                }
                else
                {
                    //条件筛选
                    if (query.Where(u => u.DVR_ID.Contains(Searcher.Location)).Count() != 0)
                    {
                        query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.DVR_ID.Contains(Searcher.Location));
                    }
                    if (query.Where(u => u.Camera_ID_view.Contains(Searcher.Location)).Count() != 0)
                    {
                        query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.Camera_ID_view.Contains(Searcher.Location));
                    }
                    else
                    {
                        //返回空值
                        query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.ID==Guid.Empty);
                        return query;

                    }

                    if (query.Where(u => u.Location.Contains(Searcher.Location)).Count() != 0)
                    {
                        query = (IOrderedQueryable<CameraRepair_View>)query.Where(u => u.Location.Contains(Searcher.Location));
                    }
                }

            }
            else
            {
                //异常等级搜索
                if (Searcher.AnomalyGrade != null && Searcher.AnomalyGrade.Count() > 0)
                {
                    var AnomalyGrades = Searcher.AnomalyGrade.ToArray();

                    var query1 = query.CheckEqual(AnomalyGrades[0], x => x.AnomalyGrade);

                    for (int i = 1; i < AnomalyGrades.Length; i++)
                    {

                        query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(AnomalyGrades[i], u => u.AnomalyGrade));

                    }

                    query = (IOrderedQueryable<CameraRepair_View>)query1;
                }
                else
                {
                    //  var AnomalyGrades = Searcher.AnomalyGrade.ToArray();

                    var query1 = query.CheckEqual(AnomalyGrade.One, x => x.AnomalyGrade);
                    query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(AnomalyGrade.Two, u => u.AnomalyGrade));
                    query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(AnomalyGrade.Three, u => u.AnomalyGrade));


                    query = (IOrderedQueryable<CameraRepair_View>)query1;
                }
                var inte2 = query.Count();

            }


            //监控室搜索
            if (Searcher.Monitor_Room!=null&&Searcher.Monitor_Room.Length>0)
            {
              var query1= query.CheckEqual(Searcher.Monitor_Room[0], u => u.Monitor_RoomId);
              
                for (int i = 1; i < Searcher.Monitor_Room.Length; i++)
                {
                   
                    query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(Searcher.Monitor_Room[i], u => u.Monitor_RoomId));
                   
                }
              
                    query = (IOrderedQueryable<CameraRepair_View>)query1;
             
               
            }

            //异常等级搜索
            if (Searcher.AnomalyGrade!=null&&Searcher.AnomalyGrade.Count()>0)
            {
              var AnomalyGrades=   Searcher.AnomalyGrade.ToArray();

                var query1 = query.CheckEqual(AnomalyGrades[0], x => x.AnomalyGrade);

                for (int i = 1; i < AnomalyGrades.Length; i++)
                {

                    query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(AnomalyGrades[i], u => u.AnomalyGrade));

                }
               
                    query = (IOrderedQueryable<CameraRepair_View>)query1;
            }
         


            //异常类别搜索
            if (Searcher.AnomalyType!=null&&Searcher.AnomalyType.Count()>0)
            {
                var AnomalyTypes = Searcher.AnomalyType.ToArray();
                var query1 = query.CheckEqual(AnomalyTypes[0], u =>u.AnomalyType);

                for (int i = 1; i < AnomalyTypes.Length; i++)
                {

                    query1 = (IOrderedQueryable<CameraRepair_View>)query1.Union(query.CheckEqual(AnomalyTypes[i], u => u.AnomalyType));

                }
              
                    query = (IOrderedQueryable<CameraRepair_View>)query1;

            }
            var inte = query.Count();
          



            return query;
        }


        

    }

    public class CameraRepair_View : CameraRepair{
        [Display(Name = "监控室")]
        public String Monitor_Room { get; set; }
        [Display(Name = "楼栋")]
        public String Build { get; set; }
        [Display(Name = "楼层")]
        public String Floor { get; set; }
        [Display(Name = "位置")]
        public String Location { get; set; }
        [Display(Name = "主机号")]
        public String DVR_ID { get; set; }

        [Display(Name = "镜头号")]
        public String Camera_ID_view { get; set; }
        [Display(Name = "部門")]
        public String DepartmentName{ get; set; }
        public Guid Monitor_RoomId { get; set; }

        [Display(Name = "安装时间")]
        public string install_time { get; set; }

    }
}
