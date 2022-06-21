using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnMonitor.Model.Equipment;
using OnMonitor.Model.Repair;
using OnMonitor.Models;
using OnMonitor.Monitor;
using OnMonitor.ViewModel.Repair.CameraRepairVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace OnMonitor.Areas.ReportForms
{
    [Area("ReportForms")]
    [AllowAnonymous]
    [ActionDescription("数据报表")]
    [ApiController]
    [Route("api/ReportForms")]
    [AllRights]
    public class ReportFormController : BaseApiController
    {
        List<MonitorRoom> MonitorRoomlist;
        List<Department> Departmentlist;
        List<DVR> DVRlist;
        List<Camera> Cameralist;
        List<CameraRepair> CameraRepairlist;
        List<AnalyzeModel> listreportForms = new List<AnalyzeModel>();
        AnalyzeModel reportForm = new AnalyzeModel();
        List<CategoryDetails> categoryDetails = new List<CategoryDetails>();
        private void GetReportFormsData()
        {
            MonitorRoomlist = DC.Set<MonitorRoom>().ToList();
            DVRlist = DC.Set<DVR>().Include(i => i.monitorRoom).ToList();
            Cameralist = DC.Set<Camera>().Include(x => x.Department).Include(u => u.DVR).ThenInclude(i => i.monitorRoom).ToList();
            CameraRepairlist = DC.Set<CameraRepair>().Include(b => b.Camera).ThenInclude(u => u.DVR).ThenInclude(i => i.monitorRoom).ToList();
            Departmentlist = DC.Set<Department>().ToList();

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



        /// <summary>
        /// 每日新增异常分析
        /// </summary>
        /// <returns></returns>
        [ActionDescription("获取7天内异常分析")]
        [HttpGet("GetReportFormsByDate7")]
        public AnalyzeModel GetReportFormsByDate7()

        {
         

            GetReportFormsData();
          //  List<AnalyzeModel> listreportForms = new List<AnalyzeModel>();
            for (int i = 0; i < 7; i++)
            {
                CategoryDetails formsDto = new CategoryDetails();
                DateTime StartTime=DateTime.Now.AddDays(-i-1);
                DateTime EndTime = DateTime.Now.AddDays(-i);
                var data=  CameraRepairlist.Where(u => u.CollectTime>= StartTime && u.CollectTime < EndTime);
                var data2 = CameraRepairlist.Where(u => u.RepairedTime >= StartTime && u.RepairedTime < EndTime);
                AnalyzeModel reportForms =new AnalyzeModel();
                formsDto.FormsRefer= DateTime.Now.AddDays(-i).ToShortDateString();
                formsDto.CameraAnomaly = data.Count();
                formsDto.RepeatRepair = data2.Count();    
                var cameralistcount= Cameralist.Where(u => u.Location.Equals("無")).Count();
                formsDto.Proportion=(float)data.Count()/(Cameralist.Count- cameralistcount);
                categoryDetails.Add(formsDto); 
            }
            reportForm.CategoryDetailsList = categoryDetails;
            return reportForm;
        }

        /// <summary>
        /// 按区域分析异常
        /// </summary>
        /// <returns></returns>
        [ActionDescription("按区域分析")]
        [HttpGet("GetReportFormsByFactory")]
        public AnalyzeModel GetReportFormsByFactory()

        {
           
            GetReportFormsData();
           
            var list1 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.One);
            var list2 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.Two);
            var list3 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.Three);
            var list = list1.Union(list2).Union(list3); 
                var data1 = list.Where(u=>u.Camera.DVR.monitorRoom.Factory=="LH");
                var data2 = list.Where(u => u.Camera.DVR.monitorRoom.Factory == "GL").Where(u => u.Camera.DVR.monitorRoom.RoomType== "MP");
                var data3 = list.Where(u => u.Camera.DVR.monitorRoom.Factory == "GL").Where(u => u.Camera.DVR.monitorRoom.RoomType== "NPI");
            CategoryDetails reportForms1 = new CategoryDetails();
            CategoryDetails reportForms2 = new CategoryDetails();
            CategoryDetails reportForms3 = new CategoryDetails();
                var cameralistcount = Cameralist.Where(u => u.DVR.monitorRoom.Factory == "LH").Where(u => u.Location!="無").Count();

                reportForms1.CameraAnomaly = data1.Count();
                reportForms1.FormsRefer = "LH";
                reportForms1.Proportion = (float)data1.Count() / Cameralist.Where(u => u.DVR.monitorRoom.Factory == "LH").Where(u => u.Location != "無").Count();
                categoryDetails.Add(reportForms1);
                reportForms2.CameraAnomaly = data2.Count();
                reportForms2.FormsRefer = "GL";
                reportForms2.Proportion = (float)data2.Count() / Cameralist.Where(u => u.DVR.monitorRoom.Factory == "GL").Where(u => u.DVR.monitorRoom.RoomType == "MP").Where(u => u.Location != "無").Count();
                categoryDetails.Add(reportForms2);
                reportForms3.CameraAnomaly = data3.Count();
                reportForms3.FormsRefer = "NPI";
                reportForms3.Proportion = (float)data3.Count() / Cameralist.Where(u => u.DVR.monitorRoom.Factory == "GL").Where(u => u.DVR.monitorRoom.RoomType == "NPI").Where(u => u.Location != "無").Count();
                categoryDetails.Add(reportForms3);
             
             reportForm.CategoryDetailsList = categoryDetails;  
              
            return reportForm;
        }


        /// <summary>
        /// 按部门分类数据
        /// </summary>
        /// <returns></returns>
        [ActionDescription("按部门获取报表")]
        [HttpGet("GetReportFormsByDepartment")]
        public AnalyzeModel GetReportFormsByDepartment()
        {
           
            GetReportFormsData();
           
            foreach (var item in Departmentlist)
            {
                CategoryDetails formsDto = new CategoryDetails();
                formsDto.FormsRefer = item.Name;

                //镜头总数
                formsDto.CameraTotal = Cameralist.Where(u => u.Department.Name == item.Name).Distinct().Count();
                var list1 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.One);
                var list2 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.Two);
                var list3 = CameraRepairlist.Where(u => u.RepairState == RepairState.Untreated).Where(i => i.AnomalyGrade == AnomalyGrade.Three);
                var list = list1.Union(list2).Union(list3);

                //加载异常数量
                formsDto.CameraAnomaly = list.Where(u => u.Camera.Department.Name == item.Name).Where(i => i.RepairState== RepairState.Untreated).Distinct().Count();
              
                if (formsDto.CameraTotal != 0)
                {
                    formsDto.Proportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal ;
                }
                categoryDetails.Add(formsDto);  

                
            }
            reportForm.CategoryDetailsList= categoryDetails;
            reportForm.FormsType = "Department";

            return reportForm;
        }


        /// <summary>
        /// 按异常等级分类数据
        /// </summary>
        /// <returns></returns>
        [ActionDescription("按异常等级获取报表")]
        [HttpGet("GetReportFormsByAnomalyGrade")]
        public AnalyzeModel  GetReportFormsByAnomalyGrade()
        {
            GetReportFormsData();
            foreach (AnomalyGrade item in Enum.GetValues(typeof(AnomalyGrade)))
            {
                CategoryDetails formsDto = new CategoryDetails();
                formsDto.FormsRefer = item.GetEnumDisplayName();

                //镜头总数
                formsDto.CameraTotal = Cameralist.Distinct().Count();

                //加载异常数量
                formsDto.CameraAnomaly= CameraRepairlist.Where(u => u.AnomalyGrade == item).Where(i => i.RepairState == RepairState.Untreated).Distinct().Count();

                if (formsDto.CameraTotal!= 0)
                {
                    formsDto.Proportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal ;
                }


               categoryDetails.Add(formsDto);
            }
            reportForm.CategoryDetailsList= categoryDetails;
            reportForm.FormsType = "Grade";
            return reportForm;
        }

        /// <summary>
        /// 按异常类别分类数据
        /// </summary>
        /// <returns></returns>
        [ActionDescription("按异常类别获取报表")]
        [HttpGet("GetReportFormsByAnomalyType")]
        public AnalyzeModel GetReportFormsByAnomalyType()
        {
            GetReportFormsData();
           

            foreach (AnomalyType item in Enum.GetValues(typeof(AnomalyType)))
            {
                CategoryDetails formsDto = new CategoryDetails();
                formsDto.FormsRefer = item.GetEnumDisplayName();
              
                //镜头总数
                formsDto.CameraTotal = Cameralist.Distinct().Count();
             
                //加载异常数量
                formsDto.CameraAnomaly = CameraRepairlist.Where(u => u.AnomalyType == item).Where(i => i.RepairState == RepairState.Untreated).Distinct().Count();

                if (formsDto.CameraTotal != 0)
                {
                    formsDto.Proportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal ;
                }
                categoryDetails.Add(formsDto);    

               
            }
            reportForm.CategoryDetailsList=categoryDetails;
            reportForm.FormsType = "AnomalyType";

            return reportForm;
        }


        /// <summary>
        /// 按安装时间分类数据
        /// </summary>
        /// <returns></returns>
        [ActionDescription("按异常类别获取报表")]
        [HttpGet("GetReportFormsByInstallTime")]
        public AnalyzeModel GetReportFormsByInstallTime()
        {
            GetReportFormsData();
            AnalyzeModel reportForm = new AnalyzeModel();
            List< CategoryDetails> categoryDetails = new List<CategoryDetails>();   
            for (int i = DateTime.Now.Year; i > 2014; i--)
            {
                CategoryDetails formsDto = new CategoryDetails();
                formsDto.FormsRefer = i.ToString();

                //镜头总数
                formsDto.CameraTotal = Cameralist.Where(u=>u.install_time.Contains(i.ToString())).Distinct().Count();

                //加载异常数量
                formsDto.CameraAnomaly = CameraRepairlist.Where(u => u.Camera.install_time.Contains(i.ToString())).Where(i => i.RepairState == RepairState.Untreated).Distinct().Count();

                if (formsDto.CameraTotal!= 0)
                {
                    formsDto.Proportion = (float)formsDto.CameraAnomaly / (float)formsDto.CameraTotal;
                }


                categoryDetails.Add(formsDto);
            }
            reportForm.FormsType = "install_time";
            reportForm.CategoryDetailsList = categoryDetails;   

            return reportForm;
        }








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
