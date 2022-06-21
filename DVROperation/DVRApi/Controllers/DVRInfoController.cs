using DVRApi.Models;
using Microsoft.AspNetCore.Mvc;
using MonitorSDK.Models;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVRApi.Controllers
{
    [ApiController]
    [Route("Api/DVRInfo")]
    public class DVRInfoController : ControllerBase
    {
       private IntPtr m_LoginID;
       private MonitorSDK.DaHuaSDKcs dahuasdk;
        public DVRInfoController()
        {

            if (dahuasdk==null)
            {
                dahuasdk = new MonitorSDK.DaHuaSDKcs();
            }
        
        
        
        }

        #region 读取硬盘录像机参数
        /// <summary>
        /// 获取DVR设备信息
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("Info")]
        [HttpGet]
        public  IActionResult  GetDVRInfo(string IP, string name, string password)
        {
            try
            {
                DVRInfoDto DVRInfo1 = new DVRInfoDto();
             
                NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();
                dahuasdk.DeviceInititalize();
                m_LoginID = dahuasdk.LoginClick(IP, "37777", name, password, ref m_DeviceInfo);
                if (m_LoginID==IntPtr.Zero)
                {
                    return BadRequest("登录失败");
                   
                }
                var disklist = dahuasdk.GetDiskInfo(m_LoginID);
                var TotalDisk = 0;
                foreach (var item in disklist)
                {
                    int OneDisk = int.Parse(item.TotalSpace);
                    TotalDisk = OneDisk + TotalDisk;
                }
             
                DVRInfo1.HardTotal = (TotalDisk / 1024 / 1024);
                DVRInfo1.ChannelTotal = m_DeviceInfo.nChanNum;
                DVRInfo1.HardCount = m_DeviceInfo.nDiskNum;
                DVRInfo1.DVR_SN= m_DeviceInfo.sSerialNumber;
                DVRInfo1.DVR_IP = IP;
                DVRInfo1.DiskInfos = disklist;

                List<DVRChannelInfo> listChannelInfo = new List<DVRChannelInfo>();

                for (int i = 0; i < m_DeviceInfo.nChanNum; i++)
                {
                   // DVRChannelInfo channelInfo = new DVRChannelInfo();

                    var channelInfo = dahuasdk.GetEncodeConfig(m_LoginID, i);
                    channelInfo.Number = channelInfo.Number + 1;
                    listChannelInfo.Add(channelInfo);
                }
                dahuasdk.LogOut(m_LoginID);
                DVRInfo1.ChannelInfos = listChannelInfo;
                var dvrtime = dahuasdk.GetDVRTime(m_LoginID);
                DVRInfo1.DVR_DateTine = dvrtime.ToString("yyyy-MM-dd HH:mm:ss");
                return Ok(DVRInfo1);

            }
            catch (Exception)
            {

                return BadRequest("读取失败");
            }

        }
        #endregion

        #region 获取硬盘录像机时间
        /// <summary>
        /// 获取硬盘录像机时间信息
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("GetDVRTime")]
        [HttpGet]
        public IActionResult GetDVRTime(string IP, string name, string password)
        {
            try
            {
               
            
                NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();
                dahuasdk.DeviceInititalize();
                m_LoginID = dahuasdk.LoginClick(IP, "37777", name, password, ref m_DeviceInfo);

                DVRDateTimeDto timedto = new DVRDateTimeDto();

                var servertime = DateTime.Now;
                var dvrtime = dahuasdk.GetDVRTime(m_LoginID);
                timedto.DVRTime = dvrtime.ToString("yyyy-MM-dd HH:mm:ss");
                timedto.ServerTime = servertime.ToString("yyyy-MM-dd HH:mm:ss");


                if (servertime.Second + 5 >= dvrtime.Second && dvrtime.Second >= servertime.Second - 5)
                {
                    timedto.IsOk = true;
                }
                else
                {
                    timedto.IsOk = false;
                }
                return  Ok(timedto);
            }
            catch (Exception)
            {

                return BadRequest("读取失败");
            }

        }
        #endregion

        #region 设定硬盘录像机时间
        /// <summary>
        /// 设定时间同步
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("SetDVRTime")]
        [HttpGet]
        public IActionResult SetDVRTime(string IP, string name, string password)

        {
           
            NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();
            dahuasdk.DeviceInititalize();
            m_LoginID = dahuasdk.LoginClick(IP, "37777", name, password, ref m_DeviceInfo);

            var res = dahuasdk.SetDVRTime(m_LoginID, DateTime.Now);

            if (res)
            {
                return Ok("OK");
            }
            else
            {
                return BadRequest("修改失败");
            }



        }

        #endregion

        #region 查询指定时间监控文件
        /// <summary>
        /// 查询指定时间监控文件，返回文件数量
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [Route("QueryVideoFileByTime")]
        [HttpGet]
        public int QueryVideoFileByTime(string IP, string name, string password, string startTimestr, string endTimestr)
        {


            DateTime startTime = Convert.ToDateTime(startTimestr);
            DateTime endTime = Convert.ToDateTime(endTimestr);
       
            NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();
            dahuasdk.DeviceInititalize();
            m_LoginID = dahuasdk.LoginClick(IP, "37777", name, password, ref m_DeviceInfo);

            int requst = dahuasdk.QueryRecordFile(m_LoginID, 1, startTime, endTime);

            dahuasdk.LogOut(m_LoginID);
            return requst;

        }
        #endregion
    }
}
