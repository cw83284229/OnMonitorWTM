using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnMonitor.Model.Equipment;
using OnMonitor.Model.Repair;
using OnMonitor.Monitor;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace OnMonitor.Areas.ReportForms
{
    [Area("ReportForms")]
    [AuthorizeJwt]
    [ActionDescription("数据报表")]
    [ApiController]
    [Route("api/ReportForms")]
    [AllRights]
    public class ReportFormController : BaseApiController
    {
        List<MonitorRoom> MonitorRoomlist;
        //  List<Department> Departmentlist;
        List<DVR> DVRlist;
        List<Camera> Cameralist;
        List<CameraRepair> CameraRepairlist;

        private void GetReportFormsData()
        {
            MonitorRoomlist = DC.Set<MonitorRoom>().ToList();
            DVRlist = DC.Set<DVR>().Include(i => i.monitorRoom).ToList();
            Cameralist = DC.Set<Camera>().Include(x => x.Department).Include(u => u.DVR).ThenInclude(i => i.monitorRoom).ToList();
            CameraRepairlist = DC.Set<CameraRepair>().Include(b => b.Camera).ThenInclude(u => u.DVR).ThenInclude(i => i.monitorRoom).ToList();


        }


        #region 按监控室获取报表信息

        /// <summary>
        /// 按监控室分类数据
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取数据")]
        [HttpGet("GetReportFormsByMonitorRoom")]
        public List<ReportFormsDto> GetReportFormsByMonitorRoom()
        {
            GetReportFormsData();
            List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
            foreach (var item in MonitorRoomlist)
            {
                ReportFormsDto formsDto = new ReportFormsDto();
                formsDto.DVRRoom = item.RoomLocation;

                //镜头总数
                formsDto.CameraTotal = Cameralist.Where(u => u.DVR.monitorRoom.RoomLocation == item.RoomLocation).Distinct().Count();
                formsDto.DVRTotal = DVRlist.Where(u => u.monitorRoom.RoomLocation == item.RoomLocation).Distinct().Count();

                //加载异常数量
                formsDto.CameraAnomaly = CameraRepairlist.Where(u => u.Camera.DVR.monitorRoom.RoomLocation == item.RoomLocation).Where(i => i.RepairState == RepairState.Untreated).Count();
                //加载维修数据
                formsDto.RepairTotal = CameraRepairlist.Where(u => u.Camera.DVR.monitorRoom.RoomLocation == item.RoomLocation).Where(i => i.RepairState == RepairState.Treated).Count();
                //异常+维修总数
                formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
                //异常比例
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal; ;
                }


                listreportForms.Add(formsDto);
            }


            return listreportForms;
        }



        ///// <summary>
        ///// 按监控室分类数据,指定时间及异常等级/类别
        ///// </summary>
        ///// <returns></returns>
        //public List<ReportFormsDto> GetReportFormsByMonitorRoomorAnomalyCondition(string StartTime, string EndTime, string AnomalyGrade, string AnomalyType)

        //{
        //    dVRCameraRepairlist();
        //    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
        //    var DVRRooms = _monitorRommrepository.ToList();
        //    listDVRCameraRepair = listDVRCameraRepair.Where(u => u.AnomalyTime != null);
        //    if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
        //    {
        //        listDVRCameraRepair = listDVRCameraRepair.Where(r => r.AnomalyTime != null).Where(u => string.Compare(u.AnomalyTime, StartTime) >= 0 && string.Compare(u.AnomalyTime, EndTime) <= 0);
        //    }
        //    if (!string.IsNullOrEmpty(AnomalyGrade))
        //    {
        //        listDVRCameraRepair = listDVRCameraRepair.Where(r => r.AnomalyGrade != null).Where(u => u.AnomalyGrade.Contains(AnomalyGrade));
        //    }
        //    if (!string.IsNullOrEmpty(AnomalyType))
        //    {
        //        listDVRCameraRepair = listDVRCameraRepair.Where(r => r != null).Where(u => u.AnomalyType == AnomalyGrade);
        //    }

        //    foreach (var item in DVRRooms)
        //    {
        //        ReportFormsDto formsDto = new ReportFormsDto();
        //        formsDto.DVRRoom = item.RoomLocation;
        //        //加载主机总数
        //        var data = _dvrrepository.Where(u => u.Monitoring_room == item.RoomLocation).Distinct();
        //        formsDto.DVRTotal = data.Count();

        //        //镜头总数
        //        formsDto.CameraTotal = listdVRCamera.Where(u => u.Monitoring_room == item.RoomLocation).Distinct().Count();


        //        //加载异常数量
        //        formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i => i.RepairState == false).Count();
        //        //加载维修数据
        //        formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.DVR_Room == item.RoomLocation).Where(i => i.RepairState == true).Count();
        //        //异常+维修总数
        //        formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
        //        //异常比例
        //        if (formsDto.CameraTotal != 0)
        //        {
        //            formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal; ;
        //        }


        //        listreportForms.Add(formsDto);
        //    }


        //    return listreportForms;
        //}

        #endregion

        //    ////#region 按楼栋获取报表信息

        //    /////// <summary>
        //    /////// 按楼栋分类数据
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public List<ReportFormsDto> GetReportFormsByBuild()

        //    ////{
        //    ////    dVRCameraRepairlist();
        //    ////    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
        //    ////    var builds = listdVRCamera.Select(i => new { build = i.Build }).Distinct();

        //    ////    foreach (var item in builds)
        //    ////    {
        //    ////        if (!String.IsNullOrEmpty(item.build))
        //    ////        {
        //    ////            ReportFormsDto formsDto = new ReportFormsDto();
        //    ////            formsDto.Camera_build = item.build;

        //    ////            //镜头总数
        //    ////            formsDto.CameraTotal = listdVRCamera.Where(u => u.Build == item.build).Select(t => new { Camera_ID = t.CameraID }).Distinct().Count();
        //    ////            //加载异常数量
        //    ////            formsDto.CameraAnomaly = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.Build)).Where(u => u.Build == item.build).Where(i => i.RepairState == false).Count();
        //    ////            //加载维修数据
        //    ////            formsDto.RepairTotal = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.Build)).Where(u => u.Build == item.build).Where(i => i.RepairState == true).Count();
        //    ////            //异常+维修总数
        //    ////            formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
        //    ////            //异常比例
        //    ////            if (formsDto.CameraTotal != 0)
        //    ////            {
        //    ////                formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
        //    ////            }

        //
        //    ////            listreportForms.Add(formsDto);
        //    ////        }

        //    ////    }


        //    ////    return listreportForms;
        //    ////}
        //    ////#endregion

        //    ////#region 按部门获取报表信息

        //    /////// <summary>
        //    /////// 按部门分类数据
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public List<ReportFormsDto> GetReportFormsBydepartment()

        //    ////{
        //    ////    dVRCameraRepairlist();
        //    ////    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
        //    ////    var departments = listdVRCamera.Select(i => new { department = i.Department }).Distinct();

        //    ////    foreach (var item in departments)
        //    ////    {
        //    ////        ReportFormsDto formsDto = new ReportFormsDto();
        //    ////        formsDto.department = item.department;

        //    ////        //镜头总数
        //    ////        formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.department == item.department).Distinct().Count();
        //    ////        //加载异常数量
        //    ////        formsDto.CameraAnomaly = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.department)).Where(u => u.department == item.department).Where(i => i.RepairState == false).Count();
        //    ////        //加载维修数据
        //    ////        formsDto.RepairTotal = listDVRCameraRepair.Where(r => !string.IsNullOrEmpty(r.department)).Where(u => u.department == item.department).Where(i => i.RepairState == true).Count();
        //    ////        //异常+维修总数
        //    ////        formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
        //    ////        //异常比例
        //    ////        if (formsDto.CameraTotal != 0)
        //    ////        {
        //    ////            formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
        //    ////        }
        //    ////        listreportForms.Add(formsDto);
        //    ////    }

        //    ////    return listreportForms;
        //    ////}
        //    ////#endregion

        //    ////#region 按年份获取报表信息

        //    /////// <summary>
        //    /////// 按年份分类数据
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public List<ReportFormsDto> GetReportFormsByYear()

        //    ////{
        //    ////    dVRCameraRepairlist();
        //    ////    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();
        //    ////    var yeartimes = _camerarepository.Select(i => new { YearTime = i.install_time }).Distinct().DefaultIfEmpty().ToList();
        //    ////    List<string> yearlist = new List<string>();
        //    ////    Dictionary<int, string> dic = new Dictionary<int, string>();
        //    ////    foreach (var item in yeartimes)
        //    ////    {

        //    ////        if (item.YearTime != null && item.YearTime != "")
        //    ////        {
        //    ////            yearlist.Add(item.YearTime.Substring(0, 4));
        //    ////        }
        //    ////    }
        //    ////    yearlist = yearlist.Distinct().ToList();

        //    ////    foreach (var item in yearlist)

        //    ////    {
        //    ////        ReportFormsDto formsDto = new ReportFormsDto();
        //    ////        formsDto.install_time = item;

        //    ////        //镜头总数
        //    ////        formsDto.CameraTotal = listDVRCameraRepair.Where(u => u.install_time.Contains(item)).Distinct().ToList().Count();
        //    ////        //加载异常数量

        //    ////        formsDto.CameraAnomaly = listDVRCameraRepair.Where(i => i.RepairState == false).Where(u => u.install_time != null).Where(r => r.install_time.Contains(item)).Count();
        //    ////        //加载维修数据
        //    ////        formsDto.RepairTotal = listDVRCameraRepair.Where(i => i.RepairState == true).Where(u => u.install_time != null).Where(r => r.install_time.Contains(item)).Count();
        //    ////        //异常+维修总数
        //    ////        formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;
        //    ////        //异常比例
        //    ////        if (formsDto.CameraTotal != 0)
        //    ////        {
        //    ////            formsDto.AnomalyProportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
        //    ////        }


        //    ////        listreportForms.Add(formsDto);
        //    ////    }

        //    ////    return listreportForms;
        //    ////}
        //    ////#endregion

        //    ////#region 获取在线DVR数量
        //    /////// <summary>
        //    /////// 获取在线DVR数量
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public List<ReportFormsDto> GetDVROnlineTotal()
        //    ////{

        //    ////    var dvrlist = _dvrrepository.Join(_dvrchenkrepository, b => b.DVR_ID, p => p.DVR_ID, (b, p) => new RequstDVRCheckInfoDto
        //    ////    {
        //    ////        Monitoring_room = b.Monitoring_room,
        //    ////        DVR_ID = b.DVR_ID,
        //    ////        DVR_Online = p.DVR_Online,
        //    ////        SNChenk = p.SNChenk,
        //    ////        LastModificationTime = p.LastModificationTime,
        //    ////        DiskChenk = p.DiskChenk,
        //    ////        Id = p.Id



        //    ////    });

        //    ////    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();

        //    ////    var Chanklist = dvrlist.Where(u => u.LastModificationTime > DateTime.Now.AddDays(-1)).Where(i => i.LastModificationTime < DateTime.Now.AddDays(+1)).Distinct().ToList();



        //    ////    foreach (var item in _monitorRommrepository)
        //    ////    {

        //    ////        ReportFormsDto formsDto = new ReportFormsDto();
        //    ////        formsDto.DVRRoom = item.RoomLocation;
        //    ////        //加载主机总数
        //    ////        formsDto.DVRTotal = Chanklist.Where(u => u.Monitoring_room == item.RoomLocation).Distinct().Count();

        //    ////        formsDto.DVRAnomaly = Chanklist.Where(u => u.Monitoring_room == item.RoomLocation).Where(r => r.DiskChenk != null).Where(u => u.DiskChenk == false).Count();

        //    ////        formsDto.DVROnLine = Chanklist.Where(u => u.Monitoring_room == item.RoomLocation).Where(r => r.DVR_Online != null).Where(u => u.DVR_Online == true).Count();

        //    ////        listreportForms.Add(formsDto);
        //    ////    }

        //    ////    return listreportForms;


        //    ////}
        //    ////#endregion

        //    ////#region 按年份获取报表信息

        //    /////// <summary>
        //    /////// 按时间分类数据
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public List<ReportFormsDto> GetReportFormsByTime(string StartTime, string EndTime, string AnomalyGrade, string AnomalyType, string MonitorRoom)

        //    ////{
        //    ////    dVRCameraRepairlist();
        //    ////    List<ReportFormsDto> listreportForms = new List<ReportFormsDto>();

        //    ////    List<DateTime> timelist = new List<DateTime>();

        //    ////    DateTime dateTimeStart = DateTime.Parse(StartTime);
        //    ////    DateTime dateTimeEnd = DateTime.Parse(EndTime);
        //    ////    for (int i = 0; dateTimeStart.Day + i < dateTimeEnd.Day; i++)
        //    ////    {

        //    ////        timelist.Add(dateTimeStart.AddDays(i));
        //    ////    }

        //    ////    if (!string.IsNullOrEmpty(AnomalyGrade))
        //    ////    {
        //    ////        listDVRCameraRepair = listDVRCameraRepair.Where(u => u.AnomalyGrade.Contains(AnomalyGrade));
        //    ////    }
        //    ////    if (!string.IsNullOrEmpty(AnomalyType))
        //    ////    {
        //    ////        listDVRCameraRepair = listDVRCameraRepair.Where(u => u.AnomalyType.Contains(AnomalyType));
        //    ////    }
        //    ////    if (!string.IsNullOrEmpty(MonitorRoom))
        //    ////    {
        //    ////        listDVRCameraRepair = listDVRCameraRepair.Where(r => r.DVR_Room != null).Where(u => u.DVR_Room.Contains(MonitorRoom));
        //    ////    }



        //    ////    foreach (var item in timelist)

        //    ////    {
        //    ////        ReportFormsDto formsDto = new ReportFormsDto();
        //    ////        formsDto.install_time = item.ToString("yyyy-MM-dd");

        //    ////        //加载异常数量
        //    ////        formsDto.CameraAnomaly = listDVRCameraRepair.Where(u => u.CollectTime.Contains(item.ToString("yyyy-MM-dd"))).Count();
        //    ////        //加载维修数据
        //    ////        formsDto.RepairTotal = listDVRCameraRepair.Where(u => u.RepairedTime.Contains(item.ToString("yyyy-MM-dd"))).Count();
        //    ////        //异常+维修总数
        //    ////        formsDto.CameraAnomalyRepair = formsDto.CameraAnomaly + formsDto.RepairTotal;

        //    ////        formsDto.CameraTotal = _camerarepository.Count();


        //    ////        listreportForms.Add(formsDto);
        //    ////    }

        //    ////    return listreportForms;
        //    ////}
        //    ////#endregion

        //    ////#region 获取全部安防设备总数
        //    /////// <summary>
        //    /////// 获取在线DVR数量
        //    /////// </summary>
        //    /////// <returns></returns>
        //    ////public Dictionary<string, int> GetDVRCameraAlalrmTotal()
        //    ////{
        //    ////    dVRCameraRepairlist();
        //    ////    Dictionary<string, int> dic = new Dictionary<string, int>();
        //    ////    var dvrCount = listdVRCamera.Select(u => new
        //    ////    {
        //    ////        DVR_ID
        //    ////          = u.DVR_ID
        //    ////    }).Distinct().Count();
        //    ////    var CameraCount = listdVRCamera.Select(u => new
        //    ////    {
        //    ////        CameraID
        //    ////          = u.CameraID
        //    ////    }).Distinct().Count();
        //    ////    var alarmCount = _alarmrepository.GetListAsync().Result.Count();

        //    ////    dic.Add("dvrCount", dvrCount);
        //    ////    dic.Add("CameraCount", CameraCount);
        //    ////    dic.Add("alarmCount", alarmCount);

        //    ////    return dic;
        //    ////}
        //    ////#endregion




    }
}
