using OnMonitor.Monitor.Alarm;
using System;

namespace EquipmentStatus
{
    class Program
    {
       
        /// <summary>
        ///  Main the Enter of exe
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // DVRInfoCheck.GetDVRInfoCheckService();
           // DVRInfoCheck.GetDVRInfoCheck();//开启主机轮询
           DVRInfoCheck.GetDVRInfoCheckStart(null,null);
        //   AlarmStatusDetection.TaskLoginStartListen();//开启门磁检测




            Console.WriteLine("请等待");
            Console.ReadLine();
         
        }
    }
}
