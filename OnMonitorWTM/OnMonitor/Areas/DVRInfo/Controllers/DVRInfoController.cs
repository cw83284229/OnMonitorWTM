
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using OnMonitor.Model.Equipment;
using WalkingTec.Mvvm.Core;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using OnMonitor.Models;
using OnMonitor.Monitor;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using Microsoft.EntityFrameworkCore;

namespace OnMonitor.Areas.Equipment.Controllers
{

    [Area("DVRInfo")]
    [AuthorizeJwt]
    [ActionDescription("DVR操作")]
    [ApiController]
    [Route("api/DVRInfo")]
    [AllRights]
    public partial class DVRInfoController : BaseApiController
    {
        static public HttpClient _httpClient;
        public IConfiguration _configuration;
        public IWebHostEnvironment _hostingEnvironment;
        string dvrurl;
        public DVRInfoController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {

            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
        }



        #region DVR通道操作
        /// <summary>
        /// 获取通道截图/传入镜头编号
        /// </summary>
        /// <param name="Camera_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChannelPicture")]
        public async System.Threading.Tasks.Task<IActionResult> GetChannelPictureAsync(string Camera_ID)
        {
            var cameradata = DC.Set<Camera>().Where(u => u.Camera_ID == Camera_ID).FirstOrDefault();
            if (cameradata == null)
            {
                return Content("NO");
            }
            var dvrdata = DC.Set<DVR>().CheckEqual(cameradata.DVRId, u => u.ID).FirstOrDefault();
            if (dvrdata == null)
            {
                return Content("NO");
            }
            cameradata.DVR.DVR_IP = dvrdata.DVR_IP;
            cameradata.DVR.DVR_usre = dvrdata.DVR_usre;
            cameradata.DVR.DVR_port = dvrdata.DVR_port;
            cameradata.DVR.DVR_possword = dvrdata.DVR_possword;


            string url = $"{dvrurl}/api/DVRClannel/GetChannelPictureLocal?DVR_IP={cameradata.DVR.DVR_IP}&DVR_Name={cameradata.DVR.DVR_usre}&DVR_PassWord={cameradata.DVR.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = await response.Content.ReadAsStreamAsync();
            var type = response.Content.Headers.ContentType.ToString();

            var DD = Directory.GetCurrentDirectory();
            var basePath = _hostingEnvironment.WebRootPath + "\\images\\Picture";

            string name = $"{Guid.NewGuid()}.jpg";

            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            if (System.IO.File.Exists(basePath + "\\" + name))
            {
                System.IO.File.Delete(basePath + "\\" + name);
            }



            System.Drawing.Image image = System.Drawing.Image.FromStream(dt);

            image.Save(basePath + "\\" + name);
            image.Dispose();

            return Content(name);

        }

        ///// <summary>
        ///// 获取通道截图/传入报警编号
        ///// </summary>
        ///// <param name="Alarm_ID"></param>
        ///// <returns></returns>

        //[HttpGet]
        //[Route("GetChannelPictureByAlarmID")]
        //public IActionResult GetChannelPictureAlarmID(string Alarm_ID)
        //{


        //    var Camera_ID = _alarmAppService.GetAlarmDto(Alarm_ID).Camera_ID;
        //    var cameradata = _cameraAppService.GetListByCameraID(Camera_ID).Result.ToList().FirstOrDefault();

        //    if (cameradata == null)
        //    {
        //        return Json("无此镜头");
        //    }

        //    var dvrdata = _dVRAppService.GetListByCondition(null, null, null, cameradata.DVR_ID).Result.Items.FirstOrDefault();


        //    string url = $"{dvrurl}/api/DVRClannel/GetChannelPictureLocal?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

        //    var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
        //    var response = _httpClient.GetAsync(url).Result;
        //    var dt = response.Content.ReadAsByteArrayAsync().Result;
        //    var type = response.Content.Headers.ContentType.ToString();

        //    return File(dt, type);

        //}

        /// <summary>
        /// 按文件名称下载录像
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("DownloadVideoByFileName")]
        public IActionResult DownloadVideoByFileName(string fileName)
        {
            //临时IP转换地址
            dvrurl = $"http://10.172.131.76:8002";
            string url = $"{dvrurl}/api/DVRClannel/DownloadVideoFile?fileName={fileName}";

            //var response = _httpClient.GetAsync(url).Result;
            //var dt = response.Content.ReadAsByteArrayAsync().Result;
            //var type = response.Content.Headers.ContentType.ToString();

            //return File(dt, type, fileName, true);

            return Ok(url);

        }
        /// <summary>
        /// 获取全部备份录像文件
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetVideoFileName")]
        public string GetVideoFileName()
        {
            string url = $"{dvrurl}/api/DVRClannel/GetVideoFiles";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }



        /// <summary>
        /// 按时间/镜头编号备份视频文件
        /// </summary>
        /// <returns></returns>
        //  [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("BackupsVideoByTime")]
        public string BackupsVideoByTime(string Camera_ID, string startTime, string endTime, string username, string password)
        {
            var cameradata = DC.Set<Camera>().Include(x => x.DVR).Where(u => u.Camera_ID==Camera_ID).FirstOrDefault();


            if (cameradata == null)
            {
                return "无此镜头";
            }


            string url = $"{dvrurl}/api/DVRClannel/GetVideoData?DVR_IP={cameradata.DVR.DVR_IP}&DVR_Name={cameradata.DVR.DVR_usre}&DVR_PassWord={cameradata.DVR.DVR_possword}&ChannelID={cameradata.channel_ID}&startTime={startTime}&endTime={endTime}";

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
            {
                url = $"{dvrurl}/api/DVRClannel/GetVideoData?DVR_IP={cameradata.DVR.DVR_IP}&DVR_Name={username}&DVR_PassWord={password}&ChannelID={cameradata.channel_ID}&startTime={startTime}&endTime={endTime}";
            }
            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }


        /// <summary>
        /// 下载进度状态查询
        /// </summary>
        /// <param name="DownloadID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadVideoFilePlan")]
        public string DownloadVideoFilePlan(string DownloadID)
        {



            string url = $"{dvrurl}/api/DVRClannel/DownloadVideoFilePlan?DownloadID={DownloadID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }
        /// <summary>
        /// 停止失败备份
        /// </summary>
        /// <param name="DownloadID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("StopDownloadVideo")]
        public string StopDownloadVideo(string DownloadID)
        {



            string url = $"{dvrurl}/api/DVRClannel/StopDownloadVideo?DownloadID={DownloadID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        /// <summary>
        /// 按名称删除录像文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteVideoFile")]
        public string DeleteVideoFile(string fileName)
        {



            string url = $"{dvrurl}/api/DVRClannel/DeleteVideoFile?fileName={fileName}";

            var handler = new HttpClientHandler();
            Dictionary<string, string> dic = new Dictionary<string, string>() { { "fileName", fileName } };
            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        /// <summary>
        /// 获取通道名称
        /// </summary>
        /// <param name="CameraID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChannelName")]
        public string GetChannelName(string Camera_ID)
        {

            var cameradata = DC.Set<Camera>().Where(u => u.Camera_ID == Camera_ID).FirstOrDefault();
            if (cameradata == null)
            {
                return "无此镜头";
            }

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;


            string url = $"{dvrurl}/api/DVRClannel/Get?DVR_IP={cameradata.DVR.DVR_IP}&DVR_Name={cameradata.DVR.DVR_usre}&DVR_PassWord={cameradata.DVR.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;


        }
        /// <summary>
        /// 自动设定通道名称与数据库同步
        /// </summary>
        /// <param name="CameraID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetChannelName")]
        public string SetChannelName(string Camera_ID)
        {
            var cameradata = DC.Set<Camera>().Where(u => u.Camera_ID == Camera_ID).FirstOrDefault();
            if (cameradata == null)
            {
                return "NO";
            }
            var dvrdata = DC.Set<DVR>().CheckEqual(cameradata.DVRId, u => u.ID).FirstOrDefault();
            if (dvrdata == null)
            {
                return "NO";
            }
            if (cameradata.Direction != null)
            {   //繁体转简体
                cameradata.Direction = ChineseConverter.Convert(cameradata.Direction, ChineseConversionDirection.TraditionalToSimplified);
            }

            if (cameradata.Location != null)
            {
                cameradata.Location = ChineseConverter.Convert(cameradata.Location, ChineseConversionDirection.TraditionalToSimplified);
            }


            string chammelname = $"{cameradata.Camera_ID} {cameradata.Build}-{cameradata.floor} {cameradata.Direction}{cameradata.Location}";
            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
            string url = $"{dvrurl}/api/DVRClannel/Post?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}&ChannelName={chammelname}";
            Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"DVR_IP",dvrdata.DVR_IP },
                    {"DVR_Name",dvrdata.DVR_usre},
                    {"DVR_PassWord",dvrdata.DVR_possword},
                    {"ChannelID",dvrdata.DVR_Channel.ToString() },
                    {"ChannelName",chammelname }
                };

            var content = new FormUrlEncodedContent(dic);
            var response = _httpClient.PostAsync(url, content).Result;

            return response.Content.ReadAsStringAsync().Result;
        }


        /// <summary>
        /// 获取镜头编码信息
        /// </summary>
        /// <returns></returns>
        // [Authorize(Roles = "videoCheck")]
        [HttpGet]
        [Route("GetChannelInfo")]
        public string GetChannelInfo(string Camera_ID)
        {
            var cameradata = DC.Set<Camera>().Where(u => u.Camera_ID == Camera_ID).FirstOrDefault();
            if (cameradata == null)
            {
                return "NO";
            }
            var dvrdata = DC.Set<DVR>().CheckEqual(cameradata.DVRId, u => u.ID).FirstOrDefault();
            if (dvrdata == null)
            {
                return "NO";
            }

            string url = $"{dvrurl}/api/DVRClannel/GetChannelInfo?DVR_IP={dvrdata.DVR_IP}&DVR_Name={dvrdata.DVR_usre}&DVR_PassWord={dvrdata.DVR_possword}&ChannelID={cameradata.channel_ID}";

            var handler = new HttpClientHandler();
            var response = _httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;

        }

        #endregion

        #region 主机信息操作
        /// <summary>
        /// 获取主机信息
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDVRInfo")]
        public IActionResult GetDVRInfo(string DVR_ID)
        {


            var dvrdata = DC.Set<DVR>().Where(u => u.DVR_ID == DVR_ID).FirstOrDefault();


            string url = $"{dvrurl}/api/DVRInfo/Get?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            var type = response.Content.Headers.ContentType.ToString();
            JsonResult json = new JsonResult(data);
            return json;

        }
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDVRTime")]
        public IActionResult GetDVRTime(string DVR_ID)
        {

            var dvrdata = DC.Set<DVR>().Where(u => u.DVR_ID == DVR_ID).FirstOrDefault();



            string url = $"{dvrurl}/api/DVRInfo/GetTime?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.GetAsync(url).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            return new JsonResult(data);

        }
        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostDVRTime")]
        public IActionResult PostDVRTime(string DVR_ID)
        {


            var dvrdata = DC.Set<DVR>().Where(u => u.DVR_ID == DVR_ID).FirstOrDefault();


            string url = $"{dvrurl}/api/DVRInfo/SetDVRTime?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";

            var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
            var response = _httpClient.PostAsync(url, null).Result;
            var dt = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(dt);
            JsonResult json = new JsonResult(data);
            return json;

        }
        #endregion


        /// <summary>
        /// 依据主机号获取比对数据
        /// </summary>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        [HttpGet("GetDVRInfoCheckByDVR_ID")]
        [ActionDescription("获取比对数据")]
        //[Route("GetDVRInfoCheckByDVR_ID")]
        public DVRcheckinfoModel GetDVRCheckInfoByDVR_ID(string DVR_ID)
        {

            var dvrurl = _configuration.GetSection("DVRInfourl:url").Value;
            var dvrdata = DC.Set<DVR>().Where(u => u.DVR_ID == DVR_ID).FirstOrDefault();

            if (dvrdata != null)
            {


                string url = $"{dvrurl}/api/DVRInfo/Get?IP={dvrdata.DVR_IP}&name={dvrdata.DVR_usre}&password={dvrdata.DVR_possword}";
                var handler = new HttpClientHandler();//{ AutomaticDecompression = DecompressionMethods.GZip };
                var response = _httpClient.GetAsync(url).Result;
                var dt = response.Content.ReadAsStringAsync().Result;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DVRInfoDto>(dt);

                DVRcheckinfoModel dVRCheckInfo = new DVRcheckinfoModel();
                //时间检查验证
                var servertime = DateTime.Now;
                dVRCheckInfo.SystemTime = servertime.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dvrtime = Convert.ToDateTime(data.DVR_DateTine);
                dVRCheckInfo.DVRTime = data.DVR_DateTine;
                if (DateTime.Compare(servertime.AddSeconds(-10), dvrtime) < 0 && DateTime.Compare(servertime.AddSeconds(10), dvrtime) > 0)
                {
                    dVRCheckInfo.TimeInfoChenk = true;
                }
                else
                {
                    dVRCheckInfo.TimeInfoChenk = false;
                }
                //硬盘检查装配
                int dvrhard = (int)(dvrdata.Hard_drive * 0.91);
                List<DVRDisk> listdvrdisk = new List<DVRDisk>();

                //装配硬盘
                foreach (var item in data.DVRDisk)
                {
                    DVRDisk dVRDisk = new DVRDisk();
                    dVRDisk.Number = item.Number;
                    dVRDisk.Disk = item.Disk / 1000;//四舍五入取值法
                    listdvrdisk.Add(dVRDisk);
                }


                dVRCheckInfo.DVRDISK = listdvrdisk;
                dVRCheckInfo.DataDiskTotal = dvrhard;

                if (dvrhard == data.HardDrive)
                {
                    dVRCheckInfo.DiskTotal = data.HardDrive;
                    dVRCheckInfo.DiskChenk = true;
                }
                else
                {
                    dVRCheckInfo.DiskTotal = data.HardDrive;
                    dVRCheckInfo.DiskChenk = false;
                }
                //在线及sn检查
                dVRCheckInfo.DataDVR_SN = dvrdata.DVR_SN;
                dVRCheckInfo.DVR_ID = dvrdata.DVR_ID;

                if (data.DVR_SN != null)
                {
                    dVRCheckInfo.DVR_SN = data.DVR_SN;
                    dVRCheckInfo.DVR_ChannelTotal = data.ChannelTotal;
                    dVRCheckInfo.DVR_Online = true;
                    if (dvrdata.DVR_SN == data.DVR_SN)
                    {
                        dVRCheckInfo.SNChenk = true;
                    }
                    else
                    {
                        dVRCheckInfo.SNChenk = false;
                    }
                }
                else
                {
                    dVRCheckInfo.DVR_Online = false;
                }


                //检查通道信息
                var cameraData = DC.Set<Camera>().Where(u => u.DVRId == dvrdata.ID);
                dVRCheckInfo.DataChannelTotal = cameraData.Count();
                List<DVRChannelInfoModel> listdVRChannelInfo = new List<DVRChannelInfoModel>();
                foreach (var tem in data.Channelname)
                {
                    DVRChannelInfoModel dVRChannelInfo = new DVRChannelInfoModel();
                    var channldata = cameraData.Where(u => u.channel_ID == tem.Number).FirstOrDefault();
                    dVRChannelInfo.DVRChannelName = tem.Name;
                    dVRChannelInfo.Channelid = tem.Number;
                    if (channldata != null)
                    {
                        string dataName = $"{channldata.Camera_ID} {channldata.Build}-{channldata.floor} {channldata.Direction}{channldata.Location}";
                        dataName = ChineseConverter.Convert(dataName, ChineseConversionDirection.TraditionalToSimplified);
                        dVRChannelInfo.DataChannelName = dataName;
                        string DVRname = tem.Name.Replace(" ", "");
                        if (dataName.Replace(" ", "") == DVRname)
                        {
                            dVRChannelInfo.ChannelNameCheck = true;
                        }
                        else
                        {
                            dVRChannelInfo.ChannelNameCheck = false;
                        }
                    }
                    else
                    {
                        dVRChannelInfo.DataChannelName = "无";
                        dVRChannelInfo.ChannelNameCheck = true;
                    }

                    listdVRChannelInfo.Add(dVRChannelInfo);

                }


                dVRCheckInfo.DVRChannelInfo = listdVRChannelInfo;

                return dVRCheckInfo;
            }
            else
            {
                return null;
            }


        }

    }
}