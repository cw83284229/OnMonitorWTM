using CCTV_Client.DaHua;
using EFmodel;
using EquipmentStatus;
using EquipmentStatus.Models;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentStatus
{
    public  class DVRInfoCheck
    {
        private static NET_DEVICEINFO_Ex device = new NET_DEVICEINFO_Ex();
        private static IntPtr m_LoginID = IntPtr.Zero;
        public static object locker = new object();

        /// <summary>
        /// 定时任务，自动对比主机数据，每天2:00启动一次
        /// </summary>
        public static void GetDVRInfoCheck()
        {
            Console.WriteLine($"DVR定时服务啓動+{DateTime.Now}");
            loghelper.WriteLog($"DVR定时服务啓動+{DateTime.Now}");

            System.Timers.Timer timer = null;
            if (timer == null)
            {
                timer = new System.Timers.Timer(86400000);
            }//获取主机报警信息
            lock (locker)
            {
              
                timer.AutoReset = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(GetDVRInfoCheckStart);
                timer.Enabled = true;
            }

        }

        public static void GetDVRInfoCheckStart(object sender, System.Timers.ElapsedEventArgs e)
        {

            Console.WriteLine($"DVR检测服務啓動+{DateTime.Now}");
            loghelper.WriteLog($"DVR检测服務啓動+{DateTime.Now}");
            EFDBHelp<DVRs> dvrDB = new EFDBHelp<DVRs>();
        
            var dvrdata = dvrDB.FindList(u=>u.DVR_ID!=null);

            List<DVRInfoChecks> ListInfoCheck =new List<DVRInfoChecks>();

            foreach (var item in dvrdata)
            {
                DVRInfoCheckService(item);
               
            }
            Console.WriteLine($"DVR检测服務關閉+{DateTime.Now}");
            loghelper.WriteLog($"DVR检测服務關閉+{DateTime.Now}");
        }

        public static void  DVRInfoCheckService(DVRs appDVRs )
        {
            EFDBHelp<DVRInfoChecks> dVRInfoCheckDB = new EFDBHelp<DVRInfoChecks>();
            DaHuaSDKHelper daHuaSDK = new DaHuaSDKHelper();
            DVRInfoChecks dVRInfoCheck = new DVRInfoChecks();
                daHuaSDK.DeviceInititalize();
                m_LoginID = daHuaSDK.LoginClick(appDVRs.DVR_IP,appDVRs.DVR_port,appDVRs.DVR_usre,appDVRs.DVR_possword, ref device);

                if (m_LoginID == IntPtr.Zero)//在线检查
                {
                 dVRInfoCheck.DVR_Online = CheckState.Anomaly;
                }
                else
                {
                     dVRInfoCheck.DVR_Online = CheckState.Normal;
                    dVRInfoCheck.DVR_SN = device.sSerialNumber;
                    if (appDVRs.DVR_SN == device.sSerialNumber)
                    {
                        dVRInfoCheck.SNChenk = CheckState.Normal;
                    }
                    else
                    {
                        dVRInfoCheck.SNChenk = CheckState.Anomaly;
                    }

                }

                DateTime dvrtime = daHuaSDK.GetDVRTime(m_LoginID);
                var diskinfo = daHuaSDK.GetDiskInfo(m_LoginID);


                dVRInfoCheck.DVRId = appDVRs.ID;
                dVRInfoCheck.CreateTime = DateTime.Now;
                dVRInfoCheck.UpdateTime = DateTime.Now;

                //时间检查验证
                var servertime = DateTime.Now;

                if (DateTime.Compare(servertime.AddSeconds(-5), dvrtime) < 0 && DateTime.Compare(servertime.AddSeconds(5), dvrtime) > 0)
                {
                    dVRInfoCheck.TimeInfoChenk = CheckState.Normal;
                }
                else
                {
                    dVRInfoCheck.TimeInfoChenk = CheckState.Anomaly;
                }

            //硬盘检查

            if (appDVRs.Hard_drive==null)
            {
                appDVRs.Hard_drive = 0;
            }
            int dvrhard = (int)(appDVRs.Hard_drive * 0.91 );
            var disksum = diskinfo.Sum(u => int.Parse(u.TotalSpace)) / 1024 / 1024;
               
                if (dvrhard <= disksum)
                {
                    dVRInfoCheck.DiskTotal = disksum;
                    dVRInfoCheck.DiskChenk = CheckState.Normal;
                }
                else
                {
                    dVRInfoCheck.DiskTotal = disksum;
                    dVRInfoCheck.DiskChenk = CheckState.Anomaly;
                }


          //  90天存储检查

            DateTime startTime = DateTime.Now.AddDays(-90);
            DateTime endTime = DateTime.Now.AddDays(-89) ;
      
          var data2=  daHuaSDK.QueryRecordFile(m_LoginID, 1, startTime, endTime);

            if (data2 > 0)
            {
                dVRInfoCheck.VideoCheck90Day = CheckState.Normal;
            }
            if (data2 == 0)
            {
                dVRInfoCheck.VideoCheck90Day = CheckState.Anomaly;
            }

            // DateTime startTime2 = DateTime.Now.AddDays(-90);
            int iday = 0;
            if (m_LoginID!=IntPtr.Zero)
            {
                for (int i = 1; i < 100; i++)
                {
                    DateTime startTime2 = DateTime.Now.AddDays(-i);

                    DateTime endTime2 = startTime2.AddDays(1).AddHours(-21);
                    var a = daHuaSDK.QueryRecordFile(m_LoginID, 1, startTime2, endTime2);

                    if (a > 0)
                    {
                        iday++;
                    };

                }
                dVRInfoCheck.VideoStarageTime = iday;
            }    
         

                dVRInfoCheck.ID = Guid.NewGuid();
                var res = dVRInfoCheckDB.Add(dVRInfoCheck);
                daHuaSDK.LogOut(m_LoginID);
         
                if (res > 0)
                {
                    Console.WriteLine($"{appDVRs.DVR_ID}+{DateTime.Now}+写入成功");
                   loghelper.WriteLog($"{appDVRs.DVR_ID}+{DateTime.Now}+写入成功");
            }
                else
                {
                    Console.WriteLine($"{appDVRs.DVR_ID}+{DateTime.Now}+写入失败");
                   loghelper.WriteLog($"{appDVRs.DVR_ID}+{DateTime.Now}+写入失败");
            }

        }
    }
}
