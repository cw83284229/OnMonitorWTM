using AlarmServer.Models;
using CCTV_Client.DaHua;
using EFmodel;
using EquipmentStatus.Models;
using NetSDKCS;
using OnMonitor.Monitor.Alarm;
using Ruanmou.Redis.Exchange.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentStatus
{
    public class AlarmStatusDetection
    {
     
        private static IntPtr lLoginID = IntPtr.Zero;
        private static IntPtr lAttachHandle = IntPtr.Zero;
        private static NET_DEVICEINFO_Ex device = new NET_DEVICEINFO_Ex();
        public static object locker = new object();
        private static fDisConnectCallBack disConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
        private static fHaveReConnectCallBack haveReConnectCallBack = new fHaveReConnectCallBack(HaveReConnectCallBack);
        private static fMessCallBackEx messCallBackEx = new fMessCallBackEx(AlarmCallBackEx);
        private static RedisHashService  service = new RedisHashService();
        public  static List<AppAlarmStatus> listAlarmAtatus = new List<AppAlarmStatus>();
        private static EFDBHelp<Alarm> DBHelpAlarm = new EFDBHelp<Alarm>();
        private static EFDBHelp<AlarmHost> DBHelpAlarmHost = new EFDBHelp<AlarmHost>();
        private static EFDBHelp<AlarmManage> DBHelpstate = new EFDBHelp<AlarmManage>();
        public  static List<AlarmHost> listAlarmHost = new List<AlarmHost>();
        public  static List<Alarm> listAlarm = new List<Alarm>();
        public  static List<AlarmManage> listAlarmManage = new List<AlarmManage>();
        public static void TaskLoginStartListen()
        {
            EFDBHelp<AlarmHost> DBHelpAlarmHost = new EFDBHelp<AlarmHost>();
            Console.WriteLine("开启报警主机自动监听");
            QueryAlarmAll();
            var listAlarmHost =DBHelpAlarmHost.FindList(u=>u.ID!=null).ToList();
            service.HashDelete("AlarmStatusData", "data");
            foreach (var item in listAlarmHost)
            {

                 Task task = Task.Run(() => LoginStartListenAsync(item.ID,item.AlarmHostIP, "37777", item.User, item.Password));
                 task.Wait();
            }

        }

        //启动监听
        public static async Task LoginStartListenAsync(Guid id, string ip, string prot, string user, string pwd)
        {
            Console.WriteLine($"使用线程ID：{Thread.CurrentThread.ManagedThreadId}");
            loghelper.loginfo.Info($"使用线程ID：{Thread.CurrentThread.ManagedThreadId}");
            NETClient.Init(disConnectCallBack, IntPtr.Zero, null);//初始化设置断线回掉

            NETClient.SetAutoReconnect(haveReConnectCallBack, IntPtr.Zero);//设定自动重连

            lLoginID = NETClient.Login(ip, Convert.ToUInt16(prot), user, pwd, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref device);
           

                DaHuaSDKHelper daHuaSDK = new DaHuaSDKHelper();
                EFDBHelp<Alarm> DBHelpAlarm = new EFDBHelp<Alarm>();
             
         
                var alarmdata = DBHelpAlarm.FindList(u=>u.AlarmHostID==id);
                var alarminfo = daHuaSDK.GetAlarmStatus(lLoginID);
                var onlineinfo = daHuaSDK.GetConnectionStatus(lLoginID);
                var defenceinfo = daHuaSDK.GetDefenceArmMode(lLoginID);
          
            
            NETClient.SetDVRMessCallBack(messCallBackEx, IntPtr.Zero);//设置报警回掉
            if (IntPtr.Zero != lLoginID)
            {

                lock (locker)
                {
                    foreach (var item in alarmdata)
                    {
                        AppAlarmStatus appAlarmStatus = new AppAlarmStatus();

                        var der = alarminfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value;
                        if (!string.IsNullOrEmpty(der))//报警数据导入
                        {
                            appAlarmStatus.IsAlarm = 1;
                        }
                        else
                        {
                            appAlarmStatus.IsAlarm = 0;
                        }
                        //
                        if (onlineinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Key > 0)//异常数据
                        {
                            appAlarmStatus.IsAnomaly = int.Parse(onlineinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value);
                        }

                        var defn = defenceinfo.Where(u => u.Key == item.Channel_ID - 1).FirstOrDefault().Value;

                        if (!string.IsNullOrEmpty(defn))
                        {
                            if (defn == EM_DEFENCEMODE.ARMING.ToString())
                            {
                                appAlarmStatus.IsDefence = 1;//布防
                            }
                            else if (defn == EM_DEFENCEMODE.DISARMING.ToString())
                            {
                                appAlarmStatus.IsDefence = 2;//撤防
                            }
                            else
                            {
                                appAlarmStatus.IsDefence = -1;//未知
                            }

                        }//布防数据
                        lock (locker)//访问http加锁
                        {
                            // var defg = httpHelper.GetAlarmManageStates(item.Alarm_ID).FirstOrDefault();

                            //if (defg != null)
                            //{
                            //    if (defg.TreatmentTimeState != null)//处理状态
                            //    {
                            //        appAlarmStatus.TreatmentState = 0;
                            //    }
                            //    else
                            //    {
                            appAlarmStatus.TreatmentState = 1;
                            //   }
                            // }
                            appAlarmStatus.Channel_ID = item.Channel_ID;
                            appAlarmStatus.IsOpenDoor = item.IsOpenOrClosed;//开岗数据
                            appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                            appAlarmStatus.Alarm_ID = item.Alarm_ID;
                            appAlarmStatus.AlarmHostIP = ip;
                        }


                        listAlarmAtatus.Add(appAlarmStatus);


                        Console.WriteLine($"ip:{ip}，通道：{item.Channel_ID},初始化成功！，时间：{appAlarmStatus.LastModificationTime}");
                        loghelper.WriteLog($"ip:{ip}，通道：{item.Channel_ID},初始化成功");
                    }


                    service.HashSet<List<AppAlarmStatus>>("AlarmStatusData", "data", listAlarmAtatus);//把数据存到Redis
                }//初始化数据
                bool result = NETClient.StartListen(lLoginID);
                if (result)
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"IP：{ip}开启监听模式");
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"IP：{ip}登陆失败");
            }
        }
      
        
        private static void Logout()
        {
            NETClient.Logout(lLoginID);
            NETClient.Cleanup();
        }

        //掉线回掉
        private static void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            Console.WriteLine($"主机{pchDVRIP}掉线,时间:{DateTime.Now.ToString()}");
            loghelper.WriteLog($"主机{pchDVRIP}掉线,时间:{DateTime.Now.ToString()}");
        }
        //重连回掉
        private static void HaveReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            Console.WriteLine($"主机{pchDVRIP}掉线:重新链接Reconnected,时间:{DateTime.Now.ToString()}");
            loghelper.WriteLog($"主机{pchDVRIP}掉线:重新链接Reconnected,时间:{DateTime.Now.ToString()}");

        }

        //报警事件回掉
        private static bool AlarmCallBackEx(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, IntPtr pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
        {
            EM_ALARM_TYPE type = (EM_ALARM_TYPE)lCommand;
            string hostIp = Marshal.PtrToStringAnsi(pchDVRIP);
            EFDBHelp<AlarmManage> DBHelpstate = new EFDBHelp<AlarmManage>();
            var alarmdata =listAlarm.Where(u=>u.AlarmHost.AlarmHostIP==hostIp);
            switch (type)
            {
                case EM_ALARM_TYPE.ALARM_ARMMODE_CHANGE_EVENT:
                    {
                        NET_ALARM_ARMMODE_CHANGE_INFO info = (NET_ALARM_ARMMODE_CHANGE_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_ARMMODE_CHANGE_INFO));
                        Console.WriteLine("布撤防状态变化事件信息：变化后状态:{0},场景模式:{1},触发方式:{2},ID:{3}",
                            GetStatus(info.bArm), GetScene(info.emSceneMode), GetTrigger(info.emTriggerMode), info.dwID);
                    }
                    break;
                case EM_ALARM_TYPE.ALARM_INPUT_SOURCE_SIGNAL://报警状态变化写入
                    {
                        NET_ALARM_INPUT_SOURCE_SIGNAL_INFO info = (NET_ALARM_INPUT_SOURCE_SIGNAL_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_INPUT_SOURCE_SIGNAL_INFO));
                        var alarm = alarmdata.Where(u => u.Channel_ID-1 == info.nChannelID).FirstOrDefault();
                        if (alarm==null)
                        {
                            break;
                        }
                    
                        var listStatus = service.HashGet<List<AppAlarmStatus>>("AlarmStatusData", "data");
                        var appAlarmStatus = listStatus.Find(u=>u.Alarm_ID==alarm.Alarm_ID);
                        DaHuaSDKHelper daHuaSDK = new DaHuaSDKHelper();
                        var alarmstate = daHuaSDK.GetDefenceArmMode(lLoginID).Where(u=>u.Key==info.nChannelID);

                        if (info.nAction == 0)  // 0:开始 1:停止(设备内0表示报警)
                        {
                            if (alarmstate != null)
                            {
                                appAlarmStatus.IsOpenDoor = alarm.IsOpenOrClosed;
                                appAlarmStatus.IsAlarm = 1;
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                                appAlarmStatus.TreatmentState = 1;
                                appAlarmStatus.AlarmHostIP = hostIp;
                               
                                if (alarmstate.FirstOrDefault().Value == "ARMING")//报警
                                {
                                    AlarmManage AppAlarmManage = new AlarmManage();
                                    AppAlarmManage.AlarmID = listAlarm.Where(u => u.AlarmHost.AlarmHostIP == hostIp).Where(x => x.Channel_ID == info.nChannelID + 1).FirstOrDefault().ID;
                                    AppAlarmManage.ID = Guid.NewGuid();
                                    AppAlarmManage.CreateTime = DateTime.Now;
                                    AppAlarmManage.UpdateTime = DateTime.Now;
                                    AppAlarmManage.AlarmTime = DateTime.Now;
                                    DBHelpstate.Add(AppAlarmManage);

                                    listStatus.Remove(listStatus.Where(u => u.Alarm_ID == alarm.Alarm_ID).FirstOrDefault());
                                    listStatus.Add(appAlarmStatus);

                                    service.HashSet<List<AppAlarmStatus>>("AlarmStatusData", "data", listStatus);

                                    Console.WriteLine("设备处于布防状态");
                                }
                            }
                            else
                            {

                                AlarmManage AppAlarmManage = new AlarmManage();
                                //AppAlarmManageStates2.AlarmHost_IP = hostIp;
                                //AppAlarmManageStates2.AlarmTime =info.stuTime.ToString();
  
                                //AppAlarmManageStates2.Channel_ID= info.nChannelID;
                                //httpHelper.AddAlarmManageState(AppAlarmManageStates2);
                               
                                Console.WriteLine("此模块未添加进资料库");
                            }
 
                        }
                        else//消除报警状态
                        {
      
                            if (appAlarmStatus!=null)
                            {
                               

                                listStatus.Remove(listStatus.Where(u => u.Alarm_ID == appAlarmStatus.Alarm_ID).FirstOrDefault());
                                appAlarmStatus.IsAlarm = 0;
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();
                                listStatus.Add(appAlarmStatus);

                                service.HashSet<List<AppAlarmStatus>>("AlarmStatusData", "data", listStatus);
                            }
                            else
                            {
                                //AppAlarmStatus appAlarmStatus4 = new AppAlarmStatus();
                                //appAlarmStatus4.IsAlarm = 0;
                                //appAlarmStatus4.Channel_ID = info.nChannelID;
                                //appAlarmStatus4.Alarm_ID = info.nChannelID.ToString();
                                //appAlarmStatus4.AlarmHostIP = hostIp;
                                //appAlarmStatus4.LastModificationTime = info.stuTime.ToString();
                                //StatusDB.Add(appAlarmStatus4);
                            }
                        }

                        Console.WriteLine($"报警输入源事件信息：IP:{hostIp},通道:{info.nChannelID+1},动作:{ GetAction(info.nAction)},报警时间:{DateTime.Now.ToString()}");
                        loghelper.WriteLog($"报警输入：IP:{hostIp},通道:{info.nChannelID+1},动作:{ GetAction(info.nAction)}");
                    }
                    break;
                case EM_ALARM_TYPE.ALARM_DEFENCE_ARMMODE_CHANGE://撤布防状态变化写入
                    {
                     
                      NET_ALARM_DEFENCE_ARMMODECHANGE_INFO info = (NET_ALARM_DEFENCE_ARMMODECHANGE_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_DEFENCE_ARMMODECHANGE_INFO));
                      var alarmstatuslist= listAlarm.Where(u => u.AlarmHost.AlarmHostIP == hostIp);

                        var alarm = alarmstatuslist.Where(u => u.Channel_ID == info.nDefenceID+1).FirstOrDefault();
                        if (alarm==null)
                        {
                            break;
                        }
                        if (service.HashExists("AlarmStatusData", "data"))
                        {
                            var listStatus = service.HashGet<List<AppAlarmStatus>>("AlarmStatusData", "data");
                           
                            var appAlarmStatus = listStatus.Find(u => u.Alarm_ID == alarm.Alarm_ID);
                           
                            if (appAlarmStatus!=null)
                            {
                                if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                {
                                    appAlarmStatus.IsDefence = 1;//布防
                                }
                                else if (info.emDefenceStatus == EM_DEFENCEMODE.DISARMING)
                                {
                                    appAlarmStatus.IsDefence = 2;//撤防
                                }
                                else
                                {
                                    appAlarmStatus.IsDefence = -1;//未知
                                }
                                appAlarmStatus.LastModificationTime = DateTime.Now.ToString();

                                listStatus.Remove(listStatus.Where(u => u.Alarm_ID == appAlarmStatus.Alarm_ID).FirstOrDefault());
                                listStatus.Add(appAlarmStatus);

                                service.HashSet<List<AppAlarmStatus>>("AlarmStatusData", "data", listStatus);
                            }
                           
                        }
                        //更新AlarmManage数据库数据
                      
                        AlarmManage appAlarmManage = DBHelpstate.FindList(u => u.Alarm.AlarmHost.AlarmHostIP == hostIp).Where(x => x.Alarm.Channel_ID == info.nDefenceID + 1).OrderByDescending(u=>u.UpdateTime).FirstOrDefault();

                        if (appAlarmManage != null&&appAlarmManage.AlarmTime!=null)
                        {
                               
                           if (appAlarmManage.WithdrawTime==null)
                              {   //判断异常
                                    if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                    {
                                        appAlarmManage.DefenceTime = DateTime.Now;
                                        appAlarmManage.WithdrawRemark = "新增布防";
                                    }
                                    else
                                    {
                                        appAlarmManage.WithdrawTime =DateTime.Now;
                                        appAlarmManage.WithdrawType = WithdrawType.AlarmWithdraw;
                                      
                                    }
                                appAlarmManage.UpdateTime = DateTime.Now;
                                DBHelpstate.Update(appAlarmManage);
                            }
                          else
                              {

                                AlarmManage appAlarmManage3 = new AlarmManage();
                                appAlarmManage3.AlarmID = listAlarm.Where(u => u.AlarmHost.AlarmHostIP == hostIp).Where(x => x.Channel_ID == info.nDefenceID + 1).FirstOrDefault().ID;
                                appAlarmManage3.ID = Guid.NewGuid();
                              
                                appAlarmManage3.CreateTime = DateTime.Now;


                                if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                     {
                                      appAlarmManage.DefenceTime = DateTime.Now;
                                      appAlarmManage.UpdateTime = DateTime.Now;
                                      appAlarmManage.WithdrawRemark = "撤防后布防";
                                      DBHelpstate.Update(appAlarmManage);
                                      }
                                else if (info.emDefenceStatus == EM_DEFENCEMODE.DISARMING)
                                    {
                                        appAlarmManage3.WithdrawTime = DateTime.Now;
                                       // appAlarmManage3.WithdrawType = WithdrawType.FieldUse;
                                        appAlarmManage3.UpdateTime = DateTime.Now;
                                        DBHelpstate.Add(appAlarmManage3);
                                    }
                               }
                            
 
                        }
                        else if(appAlarmManage == null)
                          {
                            AlarmManage appAlarmManage3 = new AlarmManage();
                            appAlarmManage3.AlarmID = listAlarm.Where(u => u.AlarmHost.AlarmHostIP == hostIp).Where(x => x.Channel_ID == info.nDefenceID + 1).FirstOrDefault().ID;
                            appAlarmManage3.ID = Guid.NewGuid();
                            appAlarmManage3.UpdateTime = DateTime.Now;
                            appAlarmManage3.CreateTime = DateTime.Now;
                            if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                            {
                                    appAlarmManage3.DefenceTime = DateTime.Now;
                            }
                            else
                            {
                                appAlarmManage3.WithdrawTime =DateTime.Now;
                                appAlarmManage3.WithdrawType = WithdrawType.FieldUse;
                               // appAlarmManage3.WithdrawMan = "系统默认";
                            }
                           
                            DBHelpstate.Add(appAlarmManage3);
                        }
                        else if (appAlarmManage.AlarmTime == null)
                        {
                            if (appAlarmManage.WithdrawTime != null&&appAlarmManage.DefenceTime==null)
                            {   //判断异常
                                if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                {
                                    appAlarmManage.DefenceTime = DateTime.Now;
                                    appAlarmManage.WithdrawRemark = "用户布防";
                                }
                                else
                                {
                                    appAlarmManage.WithdrawTime = DateTime.Now;
                                    appAlarmManage.WithdrawType = WithdrawType.AlarmWithdraw;

                                }
                                appAlarmManage.UpdateTime = DateTime.Now;
                                DBHelpstate.Update(appAlarmManage);
                            }
                            else
                            {

                                AlarmManage appAlarmManage3 = new AlarmManage();
                                appAlarmManage3.AlarmID = listAlarm.Where(u => u.AlarmHost.AlarmHostIP == hostIp).Where(x => x.Channel_ID == info.nDefenceID + 1).FirstOrDefault().ID;
                                appAlarmManage3.ID = Guid.NewGuid();

                                appAlarmManage3.CreateTime = DateTime.Now;


                                if (info.emDefenceStatus == EM_DEFENCEMODE.ARMING)
                                {
                                    appAlarmManage3.DefenceTime = DateTime.Now;
                                    //  appAlarmManage.WithdrawRemark = "用户布防";
                                }
                                else
                                {

                                    if (info.emDefenceStatus == EM_DEFENCEMODE.DISARMING)
                                    {
                                        appAlarmManage3.WithdrawTime = DateTime.Now;
                                        // appAlarmManage3.WithdrawType = WithdrawType.FieldUse;
                                        // appAlarmManage3.WithdrawMan = "系统默认";
                                    }
                                    appAlarmManage3.UpdateTime = DateTime.Now;
                                    DBHelpstate.Add(appAlarmManage3);
                                }
                            }
                        }

                        Console.WriteLine($"防区布撤防状态改变事件信息：IP:{hostIp},状态:{GetDefence(info.emDefenceStatus)}, 防区号:{info.nDefenceID+1},时间：{DateTime.Now.ToString()}");
                        loghelper.WriteLog($"防区布撤防信息：IP:{hostIp}, 防区号:{info.nDefenceID+1},状态:{GetDefence(info.emDefenceStatus)}");
                    }
                    break;
                default:
                    Console.WriteLine(lCommand.ToString("X"));
                    break;
            }
            return true;
        }

        private static string GetStatus(EM_ALARM_MODE mode)
        {
            switch (mode)
            {
                case EM_ALARM_MODE.UNKNOWN:
                    return "未知";
                case EM_ALARM_MODE.DISARMING:
                    return "撤防";
                case EM_ALARM_MODE.ARMING:
                    return "布防";
                case EM_ALARM_MODE.FORCEON:
                    return "强制布防";
                case EM_ALARM_MODE.PARTARMING:
                    return "部分布防";
                default:
                    return "未知";
            }
        }
        private static string GetScene(EM_SCENE_MODE mode)
        {
            switch (mode)
            {
                case EM_SCENE_MODE.UNKNOWN:
                    return "未知";
                case EM_SCENE_MODE.OUTDOOR:
                    return "外出模式";
                case EM_SCENE_MODE.INDOOR:
                    return "室内模式";
                case EM_SCENE_MODE.WHOLE:
                    return "全局模式";
                case EM_SCENE_MODE.RIGHTNOW:
                    return "立即模式";
                case EM_SCENE_MODE.SLEEPING:
                    return "就寝模式";
                case EM_SCENE_MODE.CUSTOM:
                    return "自定义模式";
                default:
                    return "未知";
            }
        }
        private static string GetTrigger(EM_TRIGGER_MODE mode)
        {
            switch (mode)
            {
                case EM_TRIGGER_MODE.UNKNOWN:
                    return "未知";
                case EM_TRIGGER_MODE.NET:
                    return "网络用户";
                case EM_TRIGGER_MODE.KEYBOARD:
                    return "键盘";
                case EM_TRIGGER_MODE.REMOTECONTROL:
                    return "遥控器";
                default:
                    return "未知";
            }
        }
        private static string GetAction(int action)
        {
            if (action == 0)
            {
                return "开始";
            }
            else if (action == 1)
            {
                return "停止";
            }
            else
            {
                return "";
            }
        }
        private static string GetDefence(EM_DEFENCEMODE mode)
        {
            switch (mode)
            {
                case EM_DEFENCEMODE.UNKNOWN:
                    return "未知";
                case EM_DEFENCEMODE.ARMING:
                    return "布防";
                case EM_DEFENCEMODE.DISARMING:
                    return "撤防";
                default:
                    return "未知";
            }
        }

        public static void QueryAlarmAll()
        {
            //EFDBHelp<Alarm> DBHelpAlarm = new EFDBHelp<Alarm>();
            //EFDBHelp<AlarmHost> DBHelpAlarmHost = new EFDBHelp<AlarmHost>();
            //EFDBHelp<AlarmManageState> DBHelpstate = new EFDBHelp<AlarmManageState>();

            listAlarmHost = DBHelpAlarmHost.FindList(u=>u.ID!=null).ToList();
            listAlarm= DBHelpAlarm.FindList(u => u.ID != null).ToList();

            foreach (var item in listAlarm)
            {
                item.AlarmHost = listAlarmHost.Find(u => u.ID == item.AlarmHostID);
            }



        }

    }
}
