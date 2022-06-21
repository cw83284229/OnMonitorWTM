using OnMonitor.Model.AlarmManages;
using OnMonitor.Monitor.Alarm;
using System;
using System.Configuration;

namespace EquipmentStatus
{
    class Program
    {
        internal static readonly string OperationTypeKey = ConfigurationManager.AppSettings["OperationType"];//获取配置文件信息
        /// <summary>
        ///  Main the Enter of exe
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            DVRInfoCheck.GetDVRInfoCheckStart(null, null);



            if (OperationTypeKey.Contains("AlarmStatus"))
            {
                Console.WriteLine("门磁记录服务开启" + DateTime.Now);
               AlarmStatusDetection.TaskLoginStartListen();//开启门磁检测
            }
            else if (OperationTypeKey.Contains("DVRCheck"))
            {
                Console.WriteLine("DVR检测已开启" + DateTime.Now);
                DVRInfoCheck.GetDVRInfoCheck();//开启主机轮询
            }
        


            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
