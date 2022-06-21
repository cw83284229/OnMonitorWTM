using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitorSDK.Models;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace DVRApi.Controllers
{
    [Route("api/ChannelOperation")]
    [ApiController]
    public class DVRChannelOperationController : ControllerBase
    {


        private MonitorSDK.DaHuaSDKcs dahuasdk;
        private NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();

        public DVRChannelOperationController()
        {

            if (dahuasdk==null)
            {
                dahuasdk = new MonitorSDK.DaHuaSDKcs();
            }


        }

        #region 获取单路通道信息
        /// <summary>
        /// 获取单路通道信息
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Port"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        [Route("GetChannelInfo")]
        [HttpGet]
        public IActionResult GetChannelInfo(String DVR_IP, String DVR_Name, String DVR_PassWord, int ChannelID)
        {
            DVRChannelInfo videoEncodeinfo = new DVRChannelInfo();
            try
            {
                dahuasdk.DeviceInititalize();
                IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
            //    String channelname = dahuasdk.GetChannelname(m_LoginID, ChannelID - 1);

                var data = dahuasdk.GetEncodeConfig(m_LoginID, ChannelID - 1);


                return Ok(data);
            }
            catch (Exception)
            {

                return BadRequest("获取失败");

            }
        }
        #endregion

        #region 获取单路通道名称
        /// <summary>
        /// 获取单路通道名称
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Port"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        [Route("GetChannelName")]
        [HttpGet]
        public IActionResult GetChannelName(String DVR_IP, String DVR_Name, String DVR_PassWord, int ChannelID)
        {
            try
            {
                dahuasdk.DeviceInititalize();
                IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
                String Channelname = dahuasdk.GetChannelname(m_LoginID, ChannelID - 1);
                return Ok(Channelname);
            }
            catch (Exception)
            {

                return BadRequest("获取失败");

            }
        }
        #endregion

        #region 修改单路通道名称
        /// <summary>
        /// 设定通道名称
        /// </summary>
        /// <param name="DVR_IP">ip</param>
        /// <param name="DVR_Name">账户</param>
        /// <param name="DVR_PassWord">密码</param>
        /// <param name="ChannelID">通道号</param>
        /// <param name="ChannelName">设定名称</param>
        /// <returns></returns>
        [Route("SetChannelName")]
        [HttpGet]
        public IActionResult SetChannelName(String DVR_IP, String DVR_Name, String DVR_PassWord, int ChannelID, String ChannelName)
        {

            dahuasdk.DeviceInititalize();
            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
            bool res = dahuasdk.SetChannelname(m_LoginID, ChannelID - 1, ChannelName);


            if (res == false)
            {
                return  BadRequest("修改失败");
            }
            return Ok("ok");

        }
        #endregion

        #region 读取单通道画面图片
        /// <summary>
        /// 读取单通道画面图片
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChannelPicture")]
        public IActionResult GetChannelPicture(String IP, String Name, String PassWord, int ChannelID)
        {

            dahuasdk.DeviceInititalize();
            IntPtr m_LoginID = dahuasdk.LoginClick(IP, "37777", Name, PassWord, ref m_DeviceInfo);
            string res = dahuasdk.GetChannelPictureEx(m_LoginID, ChannelID - 1);
            if (!string.IsNullOrEmpty(res))
            {
                return File(System.IO.File.ReadAllBytes(res), "image/jpg");
            }
            else
            {
                return BadRequest("获取失败");
            }

        }

        /// <summary>
        /// 读取单通道画面图片本地截图
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChannelPictureLocal")]
        public IActionResult GetChannelPictureLocal(String IP, String Name, String PassWord, int ChannelID)
        {

            dahuasdk.DeviceInititalize();
            IntPtr m_LoginID = dahuasdk.LoginClick(IP, "37777", Name,PassWord, ref m_DeviceInfo);
            
            string res = dahuasdk.GetChannelPictureLocal(m_LoginID, ChannelID - 1);
            if (!string.IsNullOrEmpty(res))
            {
                return File(System.IO.File.ReadAllBytes(res), "image/jpg");
            }
            else
            {
                return BadRequest("获取失败");
            }

        }








        #endregion

        #region 按时间下载录像文件
        /// <summary>
        /// 按通道及时间备份视频录像
        /// </summary>
        /// <param name="DVR_IP"></param>
        /// <param name="DVR_Name"></param>
        /// <param name="DVR_PassWord"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        public Dictionary<String, String> GetVideoData(String DVR_IP, String DVR_Name, String DVR_PassWord, int ChannelID, string startTime, string endTime)
        {

            dahuasdk.DeviceInititalize();
            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);//登录
            DateTime startTime1 = Convert.ToDateTime(startTime);
            DateTime endTime1 = Convert.ToDateTime(endTime);

            string path = AppDomain.CurrentDomain.BaseDirectory + "savedata";
            //string path = @"E:\DAHUA\32v\Demos\PlayBackAndDownloadDemo\PlayBackAndDownloadDemo\bin\x86Debug\savedata";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var channelname = dahuasdk.GetChannelname(m_LoginID, ChannelID - 1);
            string filename2 = channelname + startTime1.ToString("yyyy-MM-ddHHmmss") + ".dav";
            string fileName = path + "\\" + filename2;

            var res = dahuasdk.DownloadVideoFileByTime(m_LoginID, ChannelID - 1, startTime1, endTime1, fileName);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (res != IntPtr.Zero)
            {
                dic.Add("FileName", filename2);
                dic.Add("m_DownloadID", res.ToString());

            }
            else
            {
                dic.Add("Error", "视频下载失败");
            }
            return dic;
        }
        /// <summary>
        /// 获取全部文件名称
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, long> GetVideoFiles()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "savedata";
            var files = Directory.GetFiles(path, "*.dav");

            Dictionary<string, long> dic = new Dictionary<string, long>();

            // string[] fileNames = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileinfo = new FileInfo(files[i]);
                dic.Add(Path.GetFileName(files[i]), fileinfo.Length);
            }
            return dic;
        }
        /// <summary>
        /// 下载指定文件，传入文件名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage DownloadVideoFile(string fileName)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "savedata" + "\\" + fileName;

            FileStream stream = new FileStream(path, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            { FileName = fileName };
            return response;

        }
        /// <summary>
        /// 删除指定文件/传入文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteVideoFile(string fileName)
        {


            string path = AppDomain.CurrentDomain.BaseDirectory + "savedata" + "\\" + fileName;
            try
            {
               System.IO.File.Delete(path);
                
                return "删除成功";
            }
            catch (Exception)
            {

                return "删除失败";
                throw;
            }




        }

        /// <summary>
        /// 获取视频下载进度
        /// </summary>
        /// <param name="DownloadID"></param>
        /// <returns></returns>

        [HttpGet]
        public Dictionary<string, int> DownloadVideoFilePlan(string DownloadID)
        {

            Dictionary<string, int> dictionaryint = new Dictionary<string, int>();



            try
            {
                IntPtr m_DownloadID = (IntPtr)(int.Parse(DownloadID));
                dictionaryint = dahuasdk.DownloadVideoFilePlan(m_DownloadID);
            }
            catch (Exception)
            {

                dictionaryint.Add("无下载内容", 0);
            }



            return dictionaryint;
        }
        [HttpGet]
        public string StopDownloadVideo(string DownloadID)
        {
            IntPtr m_DownloadID = Marshal.StringToHGlobalAnsi(DownloadID);
            var requst = dahuasdk.StopDownloadVideo(m_DownloadID);

            if (requst)
            {
                return "下载已停止";
            }
            else
            {
                return "下载停止失败";
            }
        }



        #endregion



        ///// <summary>
        ///// 获取通道编码信息
        ///// </summary>
        ///// <param name="fileName"></param>
        //[HttpGet]
        //public async void GetFile(string fileName)






        /// <summary>
        /// 获得文件
        /// </summary>
        /// <param name="fileName"></param>
        [HttpGet]
        public async void GetFile(string fileName)
        {
            string url = $"http://localhost:57583/api/savedata?fileName={fileName}";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "savedata";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                long fileLength = response.Content.Headers.ContentLength.Value;
                long blockSize = (long)fileLength / 4;
                List<long> sizeList = new List<long>();
                var tasks = new List<Task>();
                //强行分了4块下载
                for (int i = 0; i < 3; i++)
                {
                    var begin = i * blockSize;
                    var end = begin + blockSize - 1;
                    tasks.Add(DownLoad(url, begin, end, i));
                }
                tasks.Add(DownLoad(url, 3 * blockSize, fileLength, 3));
                await Task.WhenAll(tasks);

                for (int i = 0; i < 4; i++)
                {
                    using (FileStream fs = new FileStream(filePath + DateTime.Now.ToString("yyyyMMddhhmmss") + fileName, FileMode.Append, FileAccess.Write))
                    {
                        byte[] bytes =System.IO.File.ReadAllBytes(filePath + i);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    System.IO.File.Delete(filePath + i);
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private async Task DownLoad(string url, long start, long end, int index)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "savedata";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Range = new RangeHeaderValue(start, end);
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();
                var bufferSize = 1024 * 100;
                int wirtesize;
                byte[] bytes = new byte[bufferSize];
                while ((wirtesize = stream.Read(bytes, 0, bufferSize)) > 0)
                {
                    using (FileStream fs = new FileStream(filePath + index, FileMode.Append, FileAccess.Write))
                    {
                        fs.Write(bytes, 0, (int)wirtesize);
                    }
                }
            }
        }
    }






}

