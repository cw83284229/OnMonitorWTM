using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Repair.AlarmRepairVMs;
using OnMonitor.Model.Repair;
using OnMonitor.DataAccess;
using OnMonitor.Model.Equipment;


namespace OnMonitor.Test
{
    [TestClass]
    public class AlarmRepairApiTest
    {
        private AlarmRepairController _controller;
        private string _seed;

        public AlarmRepairApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<AlarmRepairController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new AlarmRepairSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            AlarmRepairVM vm = _controller.Wtm.CreateVM<AlarmRepairVM>();
            AlarmRepair v = new AlarmRepair();
            
            v.AlarmId = AddAlarm();
            v.TestTime = DateTime.Parse("2021-08-06 11:16:49");
            v.AnomalyTime = DateTime.Parse("2022-07-31 11:16:49");
            v.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.HostAnomaly;
            v.Registrar = "WXa";
            v.RepairState = OnMonitor.Model.Repair.RepairState.Undeal;
            v.RepairedTime = DateTime.Parse("2021-08-06 11:16:49");
            v.Accendant = "7gWbQtMvAR64yUQ9QqC1lRj7RMkPrA1xj";
            v.RepairDetails = "ZomZYS6dTnOr";
            v.RepairFirm = "2yJBiz1NdJRvaZC0zmesxrVzHlUuCswog";
            v.Supervisor = "tcDfJaEhtEAIA1a3exw";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<AlarmRepair>().Find(v.ID);
                
                Assert.AreEqual(data.TestTime, DateTime.Parse("2021-08-06 11:16:49"));
                Assert.AreEqual(data.AnomalyTime, DateTime.Parse("2022-07-31 11:16:49"));
                Assert.AreEqual(data.AlarmAnomalyState, OnMonitor.Model.Repair.AlarmAnomalyState.HostAnomaly);
                Assert.AreEqual(data.Registrar, "WXa");
                Assert.AreEqual(data.RepairState, OnMonitor.Model.Repair.RepairState.Undeal);
                Assert.AreEqual(data.RepairedTime, DateTime.Parse("2021-08-06 11:16:49"));
                Assert.AreEqual(data.Accendant, "7gWbQtMvAR64yUQ9QqC1lRj7RMkPrA1xj");
                Assert.AreEqual(data.RepairDetails, "ZomZYS6dTnOr");
                Assert.AreEqual(data.RepairFirm, "2yJBiz1NdJRvaZC0zmesxrVzHlUuCswog");
                Assert.AreEqual(data.Supervisor, "tcDfJaEhtEAIA1a3exw");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            AlarmRepair v = new AlarmRepair();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.AlarmId = AddAlarm();
                v.TestTime = DateTime.Parse("2021-08-06 11:16:49");
                v.AnomalyTime = DateTime.Parse("2022-07-31 11:16:49");
                v.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.HostAnomaly;
                v.Registrar = "WXa";
                v.RepairState = OnMonitor.Model.Repair.RepairState.Undeal;
                v.RepairedTime = DateTime.Parse("2021-08-06 11:16:49");
                v.Accendant = "7gWbQtMvAR64yUQ9QqC1lRj7RMkPrA1xj";
                v.RepairDetails = "ZomZYS6dTnOr";
                v.RepairFirm = "2yJBiz1NdJRvaZC0zmesxrVzHlUuCswog";
                v.Supervisor = "tcDfJaEhtEAIA1a3exw";
                context.Set<AlarmRepair>().Add(v);
                context.SaveChanges();
            }

            AlarmRepairVM vm = _controller.Wtm.CreateVM<AlarmRepairVM>();
            var oldID = v.ID;
            v = new AlarmRepair();
            v.ID = oldID;
       		
            v.TestTime = DateTime.Parse("2021-05-12 11:16:49");
            v.AnomalyTime = DateTime.Parse("2023-01-26 11:16:49");
            v.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.ProAnomaly;
            v.Registrar = "1TAyoNKsZsQ4geU1cpxZ1B65Komoraqk7pi4aJgUiu8Lsj";
            v.RepairState = OnMonitor.Model.Repair.RepairState.Untreated;
            v.RepairedTime = DateTime.Parse("2022-02-13 11:16:49");
            v.Accendant = "m0KAJetkaQXl15lStTxrCT6d4AgGtJt7Cf";
            v.RepairDetails = "vpnrWvYzzcbNQh5AYV0qqi0aJLhL7jqxO3";
            v.RepairFirm = "7GPzgYyh";
            v.Supervisor = "34vaYFCwOqbtcWIXZV5fXUomdVMMMzJEBdgNp";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.AlarmId", "");
            vm.FC.Add("Entity.TestTime", "");
            vm.FC.Add("Entity.AnomalyTime", "");
            vm.FC.Add("Entity.AlarmAnomalyState", "");
            vm.FC.Add("Entity.Registrar", "");
            vm.FC.Add("Entity.RepairState", "");
            vm.FC.Add("Entity.RepairedTime", "");
            vm.FC.Add("Entity.Accendant", "");
            vm.FC.Add("Entity.RepairDetails", "");
            vm.FC.Add("Entity.RepairFirm", "");
            vm.FC.Add("Entity.Supervisor", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<AlarmRepair>().Find(v.ID);
 				
                Assert.AreEqual(data.TestTime, DateTime.Parse("2021-05-12 11:16:49"));
                Assert.AreEqual(data.AnomalyTime, DateTime.Parse("2023-01-26 11:16:49"));
                Assert.AreEqual(data.AlarmAnomalyState, OnMonitor.Model.Repair.AlarmAnomalyState.ProAnomaly);
                Assert.AreEqual(data.Registrar, "1TAyoNKsZsQ4geU1cpxZ1B65Komoraqk7pi4aJgUiu8Lsj");
                Assert.AreEqual(data.RepairState, OnMonitor.Model.Repair.RepairState.Untreated);
                Assert.AreEqual(data.RepairedTime, DateTime.Parse("2022-02-13 11:16:49"));
                Assert.AreEqual(data.Accendant, "m0KAJetkaQXl15lStTxrCT6d4AgGtJt7Cf");
                Assert.AreEqual(data.RepairDetails, "vpnrWvYzzcbNQh5AYV0qqi0aJLhL7jqxO3");
                Assert.AreEqual(data.RepairFirm, "7GPzgYyh");
                Assert.AreEqual(data.Supervisor, "34vaYFCwOqbtcWIXZV5fXUomdVMMMzJEBdgNp");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            AlarmRepair v = new AlarmRepair();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.AlarmId = AddAlarm();
                v.TestTime = DateTime.Parse("2021-08-06 11:16:49");
                v.AnomalyTime = DateTime.Parse("2022-07-31 11:16:49");
                v.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.HostAnomaly;
                v.Registrar = "WXa";
                v.RepairState = OnMonitor.Model.Repair.RepairState.Undeal;
                v.RepairedTime = DateTime.Parse("2021-08-06 11:16:49");
                v.Accendant = "7gWbQtMvAR64yUQ9QqC1lRj7RMkPrA1xj";
                v.RepairDetails = "ZomZYS6dTnOr";
                v.RepairFirm = "2yJBiz1NdJRvaZC0zmesxrVzHlUuCswog";
                v.Supervisor = "tcDfJaEhtEAIA1a3exw";
                context.Set<AlarmRepair>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            AlarmRepair v1 = new AlarmRepair();
            AlarmRepair v2 = new AlarmRepair();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.AlarmId = AddAlarm();
                v1.TestTime = DateTime.Parse("2021-08-06 11:16:49");
                v1.AnomalyTime = DateTime.Parse("2022-07-31 11:16:49");
                v1.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.HostAnomaly;
                v1.Registrar = "WXa";
                v1.RepairState = OnMonitor.Model.Repair.RepairState.Undeal;
                v1.RepairedTime = DateTime.Parse("2021-08-06 11:16:49");
                v1.Accendant = "7gWbQtMvAR64yUQ9QqC1lRj7RMkPrA1xj";
                v1.RepairDetails = "ZomZYS6dTnOr";
                v1.RepairFirm = "2yJBiz1NdJRvaZC0zmesxrVzHlUuCswog";
                v1.Supervisor = "tcDfJaEhtEAIA1a3exw";
                v2.AlarmId = v1.AlarmId; 
                v2.TestTime = DateTime.Parse("2021-05-12 11:16:49");
                v2.AnomalyTime = DateTime.Parse("2023-01-26 11:16:49");
                v2.AlarmAnomalyState = OnMonitor.Model.Repair.AlarmAnomalyState.ProAnomaly;
                v2.Registrar = "1TAyoNKsZsQ4geU1cpxZ1B65Komoraqk7pi4aJgUiu8Lsj";
                v2.RepairState = OnMonitor.Model.Repair.RepairState.Untreated;
                v2.RepairedTime = DateTime.Parse("2022-02-13 11:16:49");
                v2.Accendant = "m0KAJetkaQXl15lStTxrCT6d4AgGtJt7Cf";
                v2.RepairDetails = "vpnrWvYzzcbNQh5AYV0qqi0aJLhL7jqxO3";
                v2.RepairFirm = "7GPzgYyh";
                v2.Supervisor = "34vaYFCwOqbtcWIXZV5fXUomdVMMMzJEBdgNp";
                context.Set<AlarmRepair>().Add(v1);
                context.Set<AlarmRepair>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<AlarmRepair>().Find(v1.ID);
                var data2 = context.Set<AlarmRepair>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddMonitorRoom()
        {
            MonitorRoom v = new MonitorRoom();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Factory = "Ci6Z1C10hgLX5HTN73nq3FTR2zHzU0d3c";
                v.RoomLocation = "BQwucYWWRQFXCuN3yBKl2phJLXtbGH";
                v.RoomType = "dBX18LoGdiWPCAlema1Ye1X4mHbyh";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddAlarmHost()
        {
            AlarmHost v = new AlarmHost();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.MonitorRoomId = AddMonitorRoom();
                v.AlarmHost_ID = "f8YoOhkwcU0MLv78SpT8Er6q";
                v.User = "IgKIyy";
                v.Password = "Dd8945tHcdwpvWrtJQmKY9FVpouVCqqd2CSYzmVdxHqEpfOHAINDP";
                v.AlarmHostType = "iKsV26WQeCcEQRZPauy1pTzFWn025xGKG2iqd7X5kOBgJIvJFb1bj";
                v.AlarmHostIP = "jOPZv0PtxJsQ2qp3H5nd2L27WmcZ3jKXmT";
                v.AlarmChannelCount = 96;
                v.install_time = "46MwmlDiChG6MEuP78m1xdDacNuoILGiNuUxm3katy1Zdb2Gx8xw";
                v.category = "ko1juht";
                v.Remark = "Y8szWJ2zk4kkdirXL1MYjDo2N";
                context.Set<AlarmHost>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddAlarm()
        {
            Alarm v = new Alarm();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.AlarmHostID = AddAlarmHost();
                v.Alarm_ID = "n7kU8t2rPcrbswzXoWRzkHJhP3EYkCX7L7m0Dwx59zGgIbb3m";
                v.Channel_ID = 54;
                v.Build = "vRu0zS";
                v.floor = "qgiyx45P1rV2j3CIQghUS7YBWg0XIOhYnyCX0ptK";
                v.Location = "cXWsNPxyLPfKZYH3R87qmgAF3DtYQHbrPWtILmfxuFSsxNb3isPQt8";
                v.GeteType = "G8WIPLp0ml";
                v.SensorType = "qwYyT6j0eZhA0P6nuIV3HAgwEnJHAjt1bigGBH7W7fNzT";
                v.department = "yGtN";
                v.Cost_code = "qhRCbarbNAhDBZMyE7xUDlPc";
                v.install_time = "oBjzet";
                v.category = "9R943JqgmCx1mZ1yMplfE09a86SA4o58tSCb3eo5VakkQdBy";
                v.InsideCamera_ID = "8VhKkE8sBTcc5yziFSX1WsLlKZWGJWVMEb6";
                v.OutsideCamera_ID = "MsbC2DJnEtgLEZmY4AdUxevGt0AZNx9JNDmvbCe89ww";
                v.IsAlertor = true;
                v.IsOpenOrClosed = false;
                v.Remark = "MEqai";
                context.Set<Alarm>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
