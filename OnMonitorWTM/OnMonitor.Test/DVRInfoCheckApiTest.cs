using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Repair.DVRInfoCheckVMs;
using OnMonitor.Model.Repair;
using OnMonitor.DataAccess;
using OnMonitor.Model.Equipment;


namespace OnMonitor.Test
{
    [TestClass]
    public class DVRInfoCheckApiTest
    {
        private DVRInfoCheckController _controller;
        private string _seed;

        public DVRInfoCheckApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<DVRInfoCheckController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new DVRInfoCheckSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            DVRInfoCheckVM vm = _controller.Wtm.CreateVM<DVRInfoCheckVM>();
            DVRInfoCheck v = new DVRInfoCheck();
            
            v.DVRId = AddDVR();
            v.DVR_SN = "A9L3LRJA7qxGSwTavqppC3YqhymSrDHSfUD2gfZOtxU45UutBg";
            v.DVR_Channel = 22;
            v.DiskTotal = 1;
            v.DVRDISK = "LSJxTGX";
            v.DVRChannelInfo = "P9fFDdPWW6h4qv4Dk";
            v.DVR_Online = OnMonitor.Model.Repair.CheckState.Inactive;
            v.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Normal;
            v.DiskChenk = OnMonitor.Model.Repair.CheckState.Inactive;
            v.SNChenk = OnMonitor.Model.Repair.CheckState.Normal;
            v.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Anomaly;
            v.Remark = "4pY2DI8Te5xfDciIWjHHImPoYGa6Qazf1evY53bz5a2h";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DVRInfoCheck>().Find(v.ID);
                
                Assert.AreEqual(data.DVR_SN, "A9L3LRJA7qxGSwTavqppC3YqhymSrDHSfUD2gfZOtxU45UutBg");
                Assert.AreEqual(data.DVR_Channel, 22);
                Assert.AreEqual(data.DiskTotal, 1);
                Assert.AreEqual(data.DVRDISK, "LSJxTGX");
                Assert.AreEqual(data.DVRChannelInfo, "P9fFDdPWW6h4qv4Dk");
                Assert.AreEqual(data.DVR_Online, OnMonitor.Model.Repair.CheckState.Inactive);
                Assert.AreEqual(data.TimeInfoChenk, OnMonitor.Model.Repair.CheckState.Normal);
                Assert.AreEqual(data.DiskChenk, OnMonitor.Model.Repair.CheckState.Inactive);
                Assert.AreEqual(data.SNChenk, OnMonitor.Model.Repair.CheckState.Normal);
                Assert.AreEqual(data.VideoCheck90Day, OnMonitor.Model.Repair.CheckState.Anomaly);
                Assert.AreEqual(data.Remark, "4pY2DI8Te5xfDciIWjHHImPoYGa6Qazf1evY53bz5a2h");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            DVRInfoCheck v = new DVRInfoCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DVRId = AddDVR();
                v.DVR_SN = "A9L3LRJA7qxGSwTavqppC3YqhymSrDHSfUD2gfZOtxU45UutBg";
                v.DVR_Channel = 22;
                v.DiskTotal = 1;
                v.DVRDISK = "LSJxTGX";
                v.DVRChannelInfo = "P9fFDdPWW6h4qv4Dk";
                v.DVR_Online = OnMonitor.Model.Repair.CheckState.Inactive;
                v.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v.DiskChenk = OnMonitor.Model.Repair.CheckState.Inactive;
                v.SNChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Anomaly;
                v.Remark = "4pY2DI8Te5xfDciIWjHHImPoYGa6Qazf1evY53bz5a2h";
                context.Set<DVRInfoCheck>().Add(v);
                context.SaveChanges();
            }

            DVRInfoCheckVM vm = _controller.Wtm.CreateVM<DVRInfoCheckVM>();
            var oldID = v.ID;
            v = new DVRInfoCheck();
            v.ID = oldID;
       		
            v.DVR_SN = "rO4S9kJJxbpIlWbXGNNoB420q0tzrYkIKFTngpkZnKBVE";
            v.DVR_Channel = 15;
            v.DiskTotal = 33;
            v.DVRDISK = "dBc";
            v.DVRChannelInfo = "3W2y";
            v.DVR_Online = OnMonitor.Model.Repair.CheckState.Normal;
            v.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Anomaly;
            v.DiskChenk = OnMonitor.Model.Repair.CheckState.Normal;
            v.SNChenk = OnMonitor.Model.Repair.CheckState.Inactive;
            v.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Normal;
            v.Remark = "9xBhN73Au05fwGKzoaK7uOpoJGfiLidlNcdUP";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DVRId", "");
            vm.FC.Add("Entity.DVR_SN", "");
            vm.FC.Add("Entity.DVR_Channel", "");
            vm.FC.Add("Entity.DiskTotal", "");
            vm.FC.Add("Entity.DVRDISK", "");
            vm.FC.Add("Entity.DVRChannelInfo", "");
            vm.FC.Add("Entity.DVR_Online", "");
            vm.FC.Add("Entity.TimeInfoChenk", "");
            vm.FC.Add("Entity.DiskChenk", "");
            vm.FC.Add("Entity.SNChenk", "");
            vm.FC.Add("Entity.VideoCheck90Day", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DVRInfoCheck>().Find(v.ID);
 				
                Assert.AreEqual(data.DVR_SN, "rO4S9kJJxbpIlWbXGNNoB420q0tzrYkIKFTngpkZnKBVE");
                Assert.AreEqual(data.DVR_Channel, 15);
                Assert.AreEqual(data.DiskTotal, 33);
                Assert.AreEqual(data.DVRDISK, "dBc");
                Assert.AreEqual(data.DVRChannelInfo, "3W2y");
                Assert.AreEqual(data.DVR_Online, OnMonitor.Model.Repair.CheckState.Normal);
                Assert.AreEqual(data.TimeInfoChenk, OnMonitor.Model.Repair.CheckState.Anomaly);
                Assert.AreEqual(data.DiskChenk, OnMonitor.Model.Repair.CheckState.Normal);
                Assert.AreEqual(data.SNChenk, OnMonitor.Model.Repair.CheckState.Inactive);
                Assert.AreEqual(data.VideoCheck90Day, OnMonitor.Model.Repair.CheckState.Normal);
                Assert.AreEqual(data.Remark, "9xBhN73Au05fwGKzoaK7uOpoJGfiLidlNcdUP");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            DVRInfoCheck v = new DVRInfoCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DVRId = AddDVR();
                v.DVR_SN = "A9L3LRJA7qxGSwTavqppC3YqhymSrDHSfUD2gfZOtxU45UutBg";
                v.DVR_Channel = 22;
                v.DiskTotal = 1;
                v.DVRDISK = "LSJxTGX";
                v.DVRChannelInfo = "P9fFDdPWW6h4qv4Dk";
                v.DVR_Online = OnMonitor.Model.Repair.CheckState.Inactive;
                v.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v.DiskChenk = OnMonitor.Model.Repair.CheckState.Inactive;
                v.SNChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Anomaly;
                v.Remark = "4pY2DI8Te5xfDciIWjHHImPoYGa6Qazf1evY53bz5a2h";
                context.Set<DVRInfoCheck>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DVRInfoCheck v1 = new DVRInfoCheck();
            DVRInfoCheck v2 = new DVRInfoCheck();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DVRId = AddDVR();
                v1.DVR_SN = "A9L3LRJA7qxGSwTavqppC3YqhymSrDHSfUD2gfZOtxU45UutBg";
                v1.DVR_Channel = 22;
                v1.DiskTotal = 1;
                v1.DVRDISK = "LSJxTGX";
                v1.DVRChannelInfo = "P9fFDdPWW6h4qv4Dk";
                v1.DVR_Online = OnMonitor.Model.Repair.CheckState.Inactive;
                v1.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v1.DiskChenk = OnMonitor.Model.Repair.CheckState.Inactive;
                v1.SNChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v1.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Anomaly;
                v1.Remark = "4pY2DI8Te5xfDciIWjHHImPoYGa6Qazf1evY53bz5a2h";
                v2.DVRId = v1.DVRId; 
                v2.DVR_SN = "rO4S9kJJxbpIlWbXGNNoB420q0tzrYkIKFTngpkZnKBVE";
                v2.DVR_Channel = 15;
                v2.DiskTotal = 33;
                v2.DVRDISK = "dBc";
                v2.DVRChannelInfo = "3W2y";
                v2.DVR_Online = OnMonitor.Model.Repair.CheckState.Normal;
                v2.TimeInfoChenk = OnMonitor.Model.Repair.CheckState.Anomaly;
                v2.DiskChenk = OnMonitor.Model.Repair.CheckState.Normal;
                v2.SNChenk = OnMonitor.Model.Repair.CheckState.Inactive;
                v2.VideoCheck90Day = OnMonitor.Model.Repair.CheckState.Normal;
                v2.Remark = "9xBhN73Au05fwGKzoaK7uOpoJGfiLidlNcdUP";
                context.Set<DVRInfoCheck>().Add(v1);
                context.Set<DVRInfoCheck>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DVRInfoCheck>().Find(v1.ID);
                var data2 = context.Set<DVRInfoCheck>().Find(v2.ID);
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

                v.Factory = "AYRozz5KlZelL637gV4a2Ihpmk3DiekZ0X2tFTNfBeDG4gEUy";
                v.RoomLocation = "xCgGvEyl36M8lqXraGfiKlGWSVbNGOUFZSUkHZPsEG";
                v.RoomType = "kO";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddDVR()
        {
            DVR v = new DVR();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.monitorRoomId = AddMonitorRoom();
                v.DVR_ID = "Lx7qImHBJoiS6ZMtqXTPXBu6BOrSsJEA54mNuC8";
                v.Home_server = "Dc5Ob4cHSzO9l0HE7nQsDWa0iF6uZEM";
                v.Hard_drive = 97;
                v.DVR_IP = "hUPHPJWaookM5K8xV2lBX";
                v.DVR_port = "GXnQLinPOcyCq0tWJMzzxHbyh2FOlC2k1J8z1g1JyESdia";
                v.DVR_usre = "8XTjqmhfpHOV3CmnY";
                v.DVR_possword = "7oLiiy7LDciEuWOKxe7perc11Lb46Y8PPb";
                v.install_time = "NYTYV";
                v.Manufacturer = "6zB7p";
                v.DVR_type = "WuzBCJtpaWhC";
                v.DVR_SN = "bCy2lN6gg9Ydy4Eyy0GsWpk4oro00yP03XA";
                v.DVR_Channel = 67;
                v.department = "iRPnp5sisUDLJUzlqY6SDhbvSiD";
                v.Cost_code = "ce4YFSU7f8rdKrc79DWiCf579SystdFqiKkFvC4mp8B3I9";
                v.Remark = "DS8F5SZmBjD9QY7gGn0yinv26G9uQMpWYFQUJ4kT51ltxNA9pIeyPnuK5HQ3UXx081M34x2C8p3cr6IdpG2a0vICgwZHTBcMweAoK90jThvW21qI";
                context.Set<DVR>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
