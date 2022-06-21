using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.AlarmVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class AlarmApiTest
    {
        private AlarmController _controller;
        private string _seed;

        public AlarmApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<AlarmController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new AlarmSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            AlarmVM vm = _controller.Wtm.CreateVM<AlarmVM>();
            Alarm v = new Alarm();
            
            v.AlarmHostID = AddAlarmHost();
            v.Alarm_ID = "UvdJ7UruNyQbMRn1l";
            v.Channel_ID = 69;
            v.Build = "oXU7dbnHdHNXPiEHi5KXPa3j4HSvCSccQ2dMOYN";
            v.floor = "4fPil7daAkeKEi24v7QiR3oVJl94SoXm0roQwJ5vex0";
            v.Location = "6fYIAXgUKKbGgZ";
            v.GeteType = "oJWaD92Lyf6W3AfRggP2CUu5q6UjVhjp";
            v.SensorType = "7p9VVSe5MVLdwkbD8HIs62B30jv4blQ0pGolrlEJZQuNY6WB";
            v.department = "mp";
            v.Cost_code = "kHztXQxvy";
            v.install_time = "0Ufnp";
            v.category = "GuKaRsdYHhu2tmSYDXSEyO2oy0IHFetRP6uxynrnTuP";
            v.InsideCamera_ID = "V0IfBkLmhO2p8XnPj59jvqi0ZsIYkmzUy8dT40zn7SyLpuIZRI0XK";
            v.OutsideCamera_ID = "0leg0Sg00saa9cKCVYh9FSx0DYUw2N7KEMYZSH6OD7g";
            v.IsAlertor = true;
            v.IsOpenOrClosed = true;
            v.Remark = "Ess8IlrbrwI6d7l4hMWn2sunAPbsLEEZBDyu";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Alarm>().Find(v.ID);
                
                Assert.AreEqual(data.Alarm_ID, "UvdJ7UruNyQbMRn1l");
                Assert.AreEqual(data.Channel_ID, 69);
                Assert.AreEqual(data.Build, "oXU7dbnHdHNXPiEHi5KXPa3j4HSvCSccQ2dMOYN");
                Assert.AreEqual(data.floor, "4fPil7daAkeKEi24v7QiR3oVJl94SoXm0roQwJ5vex0");
                Assert.AreEqual(data.Location, "6fYIAXgUKKbGgZ");
                Assert.AreEqual(data.GeteType, "oJWaD92Lyf6W3AfRggP2CUu5q6UjVhjp");
                Assert.AreEqual(data.SensorType, "7p9VVSe5MVLdwkbD8HIs62B30jv4blQ0pGolrlEJZQuNY6WB");
                Assert.AreEqual(data.department, "mp");
                Assert.AreEqual(data.Cost_code, "kHztXQxvy");
                Assert.AreEqual(data.install_time, "0Ufnp");
                Assert.AreEqual(data.category, "GuKaRsdYHhu2tmSYDXSEyO2oy0IHFetRP6uxynrnTuP");
                Assert.AreEqual(data.InsideCamera_ID, "V0IfBkLmhO2p8XnPj59jvqi0ZsIYkmzUy8dT40zn7SyLpuIZRI0XK");
                Assert.AreEqual(data.OutsideCamera_ID, "0leg0Sg00saa9cKCVYh9FSx0DYUw2N7KEMYZSH6OD7g");
                Assert.AreEqual(data.IsAlertor, true);
                Assert.AreEqual(data.IsOpenOrClosed, true);
                Assert.AreEqual(data.Remark, "Ess8IlrbrwI6d7l4hMWn2sunAPbsLEEZBDyu");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            Alarm v = new Alarm();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.AlarmHostID = AddAlarmHost();
                v.Alarm_ID = "UvdJ7UruNyQbMRn1l";
                v.Channel_ID = 69;
                v.Build = "oXU7dbnHdHNXPiEHi5KXPa3j4HSvCSccQ2dMOYN";
                v.floor = "4fPil7daAkeKEi24v7QiR3oVJl94SoXm0roQwJ5vex0";
                v.Location = "6fYIAXgUKKbGgZ";
                v.GeteType = "oJWaD92Lyf6W3AfRggP2CUu5q6UjVhjp";
                v.SensorType = "7p9VVSe5MVLdwkbD8HIs62B30jv4blQ0pGolrlEJZQuNY6WB";
                v.department = "mp";
                v.Cost_code = "kHztXQxvy";
                v.install_time = "0Ufnp";
                v.category = "GuKaRsdYHhu2tmSYDXSEyO2oy0IHFetRP6uxynrnTuP";
                v.InsideCamera_ID = "V0IfBkLmhO2p8XnPj59jvqi0ZsIYkmzUy8dT40zn7SyLpuIZRI0XK";
                v.OutsideCamera_ID = "0leg0Sg00saa9cKCVYh9FSx0DYUw2N7KEMYZSH6OD7g";
                v.IsAlertor = true;
                v.IsOpenOrClosed = true;
                v.Remark = "Ess8IlrbrwI6d7l4hMWn2sunAPbsLEEZBDyu";
                context.Set<Alarm>().Add(v);
                context.SaveChanges();
            }

            AlarmVM vm = _controller.Wtm.CreateVM<AlarmVM>();
            var oldID = v.ID;
            v = new Alarm();
            v.ID = oldID;
       		
            v.Alarm_ID = "dzNazMTECbj1SylOLyHKJw";
            v.Channel_ID = 70;
            v.Build = "Cu58229jzw9DTROoSNlHLH6t9ckK2ZGaXf51FizuL4ArvTBOJ";
            v.floor = "x1ohMe";
            v.Location = "S6aLGUJ7yjHePRG6KyduoDS22DbyYIh";
            v.GeteType = "kh6lJHRhUt7PPksSPYwDMBxhOhsaGzLnG5rZWe57Wse3gfZ1kg2d1V";
            v.SensorType = "opa4fLq1yoC8leyNegRdaMk8gLWOBHjizMKhpvvdlHfhk4j";
            v.department = "p0olPfXQl12qG6feCiLn6Ow";
            v.Cost_code = "2xlK6ZRtGPFPLYGX1rqjZM4qZ50z92fBGHTWFEr5t";
            v.install_time = "1dOiDS";
            v.category = "RzmtfckMrJn";
            v.InsideCamera_ID = "CygwWvTers0BibqQdOkjiPot9MYRvg";
            v.OutsideCamera_ID = "4sp7z";
            v.IsAlertor = false;
            v.IsOpenOrClosed = true;
            v.Remark = "DVjwkidOVfktoaXTsRIuK";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.AlarmHostID", "");
            vm.FC.Add("Entity.Alarm_ID", "");
            vm.FC.Add("Entity.Channel_ID", "");
            vm.FC.Add("Entity.Build", "");
            vm.FC.Add("Entity.floor", "");
            vm.FC.Add("Entity.Location", "");
            vm.FC.Add("Entity.GeteType", "");
            vm.FC.Add("Entity.SensorType", "");
            vm.FC.Add("Entity.department", "");
            vm.FC.Add("Entity.Cost_code", "");
            vm.FC.Add("Entity.install_time", "");
            vm.FC.Add("Entity.category", "");
            vm.FC.Add("Entity.InsideCamera_ID", "");
            vm.FC.Add("Entity.OutsideCamera_ID", "");
            vm.FC.Add("Entity.IsAlertor", "");
            vm.FC.Add("Entity.IsOpenOrClosed", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Alarm>().Find(v.ID);
 				
                Assert.AreEqual(data.Alarm_ID, "dzNazMTECbj1SylOLyHKJw");
                Assert.AreEqual(data.Channel_ID, 70);
                Assert.AreEqual(data.Build, "Cu58229jzw9DTROoSNlHLH6t9ckK2ZGaXf51FizuL4ArvTBOJ");
                Assert.AreEqual(data.floor, "x1ohMe");
                Assert.AreEqual(data.Location, "S6aLGUJ7yjHePRG6KyduoDS22DbyYIh");
                Assert.AreEqual(data.GeteType, "kh6lJHRhUt7PPksSPYwDMBxhOhsaGzLnG5rZWe57Wse3gfZ1kg2d1V");
                Assert.AreEqual(data.SensorType, "opa4fLq1yoC8leyNegRdaMk8gLWOBHjizMKhpvvdlHfhk4j");
                Assert.AreEqual(data.department, "p0olPfXQl12qG6feCiLn6Ow");
                Assert.AreEqual(data.Cost_code, "2xlK6ZRtGPFPLYGX1rqjZM4qZ50z92fBGHTWFEr5t");
                Assert.AreEqual(data.install_time, "1dOiDS");
                Assert.AreEqual(data.category, "RzmtfckMrJn");
                Assert.AreEqual(data.InsideCamera_ID, "CygwWvTers0BibqQdOkjiPot9MYRvg");
                Assert.AreEqual(data.OutsideCamera_ID, "4sp7z");
                Assert.AreEqual(data.IsAlertor, false);
                Assert.AreEqual(data.IsOpenOrClosed, true);
                Assert.AreEqual(data.Remark, "DVjwkidOVfktoaXTsRIuK");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            Alarm v = new Alarm();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.AlarmHostID = AddAlarmHost();
                v.Alarm_ID = "UvdJ7UruNyQbMRn1l";
                v.Channel_ID = 69;
                v.Build = "oXU7dbnHdHNXPiEHi5KXPa3j4HSvCSccQ2dMOYN";
                v.floor = "4fPil7daAkeKEi24v7QiR3oVJl94SoXm0roQwJ5vex0";
                v.Location = "6fYIAXgUKKbGgZ";
                v.GeteType = "oJWaD92Lyf6W3AfRggP2CUu5q6UjVhjp";
                v.SensorType = "7p9VVSe5MVLdwkbD8HIs62B30jv4blQ0pGolrlEJZQuNY6WB";
                v.department = "mp";
                v.Cost_code = "kHztXQxvy";
                v.install_time = "0Ufnp";
                v.category = "GuKaRsdYHhu2tmSYDXSEyO2oy0IHFetRP6uxynrnTuP";
                v.InsideCamera_ID = "V0IfBkLmhO2p8XnPj59jvqi0ZsIYkmzUy8dT40zn7SyLpuIZRI0XK";
                v.OutsideCamera_ID = "0leg0Sg00saa9cKCVYh9FSx0DYUw2N7KEMYZSH6OD7g";
                v.IsAlertor = true;
                v.IsOpenOrClosed = true;
                v.Remark = "Ess8IlrbrwI6d7l4hMWn2sunAPbsLEEZBDyu";
                context.Set<Alarm>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Alarm v1 = new Alarm();
            Alarm v2 = new Alarm();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.AlarmHostID = AddAlarmHost();
                v1.Alarm_ID = "UvdJ7UruNyQbMRn1l";
                v1.Channel_ID = 69;
                v1.Build = "oXU7dbnHdHNXPiEHi5KXPa3j4HSvCSccQ2dMOYN";
                v1.floor = "4fPil7daAkeKEi24v7QiR3oVJl94SoXm0roQwJ5vex0";
                v1.Location = "6fYIAXgUKKbGgZ";
                v1.GeteType = "oJWaD92Lyf6W3AfRggP2CUu5q6UjVhjp";
                v1.SensorType = "7p9VVSe5MVLdwkbD8HIs62B30jv4blQ0pGolrlEJZQuNY6WB";
                v1.department = "mp";
                v1.Cost_code = "kHztXQxvy";
                v1.install_time = "0Ufnp";
                v1.category = "GuKaRsdYHhu2tmSYDXSEyO2oy0IHFetRP6uxynrnTuP";
                v1.InsideCamera_ID = "V0IfBkLmhO2p8XnPj59jvqi0ZsIYkmzUy8dT40zn7SyLpuIZRI0XK";
                v1.OutsideCamera_ID = "0leg0Sg00saa9cKCVYh9FSx0DYUw2N7KEMYZSH6OD7g";
                v1.IsAlertor = true;
                v1.IsOpenOrClosed = true;
                v1.Remark = "Ess8IlrbrwI6d7l4hMWn2sunAPbsLEEZBDyu";
                v2.AlarmHostID = v1.AlarmHostID; 
                v2.Alarm_ID = "dzNazMTECbj1SylOLyHKJw";
                v2.Channel_ID = 70;
                v2.Build = "Cu58229jzw9DTROoSNlHLH6t9ckK2ZGaXf51FizuL4ArvTBOJ";
                v2.floor = "x1ohMe";
                v2.Location = "S6aLGUJ7yjHePRG6KyduoDS22DbyYIh";
                v2.GeteType = "kh6lJHRhUt7PPksSPYwDMBxhOhsaGzLnG5rZWe57Wse3gfZ1kg2d1V";
                v2.SensorType = "opa4fLq1yoC8leyNegRdaMk8gLWOBHjizMKhpvvdlHfhk4j";
                v2.department = "p0olPfXQl12qG6feCiLn6Ow";
                v2.Cost_code = "2xlK6ZRtGPFPLYGX1rqjZM4qZ50z92fBGHTWFEr5t";
                v2.install_time = "1dOiDS";
                v2.category = "RzmtfckMrJn";
                v2.InsideCamera_ID = "CygwWvTers0BibqQdOkjiPot9MYRvg";
                v2.OutsideCamera_ID = "4sp7z";
                v2.IsAlertor = false;
                v2.IsOpenOrClosed = true;
                v2.Remark = "DVjwkidOVfktoaXTsRIuK";
                context.Set<Alarm>().Add(v1);
                context.Set<Alarm>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Alarm>().Find(v1.ID);
                var data2 = context.Set<Alarm>().Find(v2.ID);
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

                v.Factory = "pIRR5a";
                v.RoomLocation = "DYgC9q2lC1I4OXimd2McLUS01Irv0lUpUI3kJUmYQ6L";
                v.RoomType = "2B4YgDbYazh441qSscVqLlvYuS9A6qhSz3OC0XpDg4Tg";
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
                v.AlarmHost_ID = "5Ua9MW5Gbdnpjc";
                v.User = "C9tmYn09GyqWkUw8y4NeZIqCQ0fj8";
                v.Password = "qHDAesuwg0i0HWs94244oBxm3rKE5uMQLfC4JpDJwh1EGZ";
                v.AlarmHostType = "oTm5x2pLs";
                v.AlarmHostIP = "V916wHtKKXePR599QyQQpDLoH";
                v.AlarmChannelCount = 81;
                v.install_time = "UGgXdnxRFlrC2IVRL7TejWtfoWEPDpOVot8HmdZkr";
                v.category = "axVbLzgeERLV5MeO6mpgWzA8nOnPsylPL4D5";
                v.Remark = "BL2D3wE4glW44gszDHGxC3qfABcVTs56mxML0Ax33yuhfemEb";
                context.Set<AlarmHost>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
