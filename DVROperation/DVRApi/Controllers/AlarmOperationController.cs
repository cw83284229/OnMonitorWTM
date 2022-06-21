using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmOperationController : ControllerBase
    {
      
    //        CCTV_Client.DaHua.DaHuaSDKHelper dahuasdk = new DaHuaSDKHelper();
    //        NET_DEVICEINFO_Ex m_DeviceInfo = new NET_DEVICEINFO_Ex();


    //        /// <summary>
    //        /// 获取主机布防信息
    //        /// </summary>
    //        /// <param name="DVR_IP"></param>
    //        /// <param name="DVR_Name"></param>
    //        /// <param name="DVR_PassWord"></param>
    //        /// <returns></returns>
    //        [HttpGet]
    //        public Dictionary<int, String> GetDefenceArmMode(String DVR_IP, String DVR_Name, String DVR_PassWord)
    //        {

    //            dahuasdk.DeviceInititalize();
    //            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
    //            var requst = dahuasdk.GetDefenceArmMode(m_LoginID);


    //            return requst;


    //        }


    //        /// <summary>
    //        /// 设定指定通道布防
    //        /// </summary>
    //        /// <param name="DVR_IP"></param>
    //        /// <param name="DVR_Name"></param>
    //        /// <param name="DVR_PassWord"></param>
    //        /// <returns></returns>
    //        [HttpGet]
    //        public bool SetDefenceArmMode(String DVR_IP, String DVR_Name, String DVR_PassWord, int AlarmChannel, int DefenceState)
    //        {

    //            dahuasdk.DeviceInititalize();
    //            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
    //            var requst = dahuasdk.SetDefenceArmMode(m_LoginID, AlarmChannel, DVR_PassWord, DefenceState);


    //            return requst;


    //        }

    //        /// <summary>
    //        /// 获取主机在线状态
    //        /// </summary>
    //        /// <param name="DVR_IP"></param>
    //        /// <param name="DVR_Name"></param>
    //        /// <param name="DVR_PassWord"></param>
    //        /// <returns></returns>
    //        [HttpGet]
    //        public Dictionary<int, String> GetConnectionStatus(String DVR_IP, String DVR_Name, String DVR_PassWord)
    //        {

    //            dahuasdk.DeviceInititalize();
    //            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
    //            var requst = dahuasdk.GetConnectionStatus(m_LoginID);


    //            return requst;


    //        }
    //        /// <summary>
    //        /// 报警激活状态
    //        /// </summary>
    //        /// <param name="DVR_IP"></param>
    //        /// <param name="DVR_Name"></param>
    //        /// <param name="DVR_PassWord"></param>
    //        /// <returns></returns>
    //        [HttpGet]
    //        public Dictionary<int, String> GetAlarmStatus(String DVR_IP, String DVR_Name, String DVR_PassWord)
    //        {

    //            dahuasdk.DeviceInititalize();
    //            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);
    //            var requst = dahuasdk.GetAlarmStatus(m_LoginID);


    //            return requst;


    //        }

    //        /// <summary>
    //        /// 报警信息状态
    //        /// </summary>
    //        /// <param name="DVR_IP"></param>
    //        /// <param name="DVR_Name"></param>
    //        /// <param name="DVR_PassWord"></param>
    //        /// <returns></returns>
    //        [HttpGet]
    //        public List<AlarmStatus> GetAlarmInfo(String DVR_IP, String DVR_Name, String DVR_PassWord)
    //        {
    //            List<AlarmStatus> listAlarmInfo = new List<AlarmStatus>();
    //            dahuasdk.DeviceInititalize();
    //            IntPtr m_LoginID = dahuasdk.LoginClick(DVR_IP, "37777", DVR_Name, DVR_PassWord, ref m_DeviceInfo);

    //            var requst = dahuasdk.GetAlarmStatus(m_LoginID);
    //            var requst2 = dahuasdk.GetConnectionStatus(m_LoginID);
    //            var requst3 = dahuasdk.GetDefenceArmMode(m_LoginID);

    //            //构建list
    //            for (int i = 0; i < 128; i++)
    //            {
    //                AlarmStatus alarmInfo = new AlarmStatus();
    //                alarmInfo.Alarm_ID = i.ToString();
    //                alarmInfo.Channel_ID = i;
    //                listAlarmInfo.Add(alarmInfo);
    //            }
    //            //循环加载数据
    //            foreach (var item in listAlarmInfo)
    //            {
    //                var DER = requst3.Where(u => u.Key == item.Channel_ID).FirstOrDefault().Value;

    //                if (DER == "ARMING")
    //                {
    //                    item.IsDefence = 1;
    //                }
    //                else if (DER == "DISARMING")
    //                {
    //                    item.IsDefence = 0;
    //                }
    //                else
    //                {
    //                    item.IsDefence = -1;
    //                }

    //                var DER2 = requst2.Where(u => u.Key == item.Channel_ID).FirstOrDefault().Value;

    //                item.IsAnomaly = Convert.ToInt16(DER2);

    //                if (requst.Where(u => u.Key == item.Channel_ID).FirstOrDefault().Key > 0)
    //                {
    //                    item.IsAlarm = 1;
    //                    item.LastModificationTime = requst.Where(u => u.Key == item.Channel_ID).FirstOrDefault().Value;
    //                }
    //                item.AlarmHostIP = DVR_IP;
    //            }


    //            return listAlarmInfo;

    //        }
    //    }
    //}

  }
}
