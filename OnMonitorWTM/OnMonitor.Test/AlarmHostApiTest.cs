using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.AlarmHostVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class AlarmHostApiTest
    {
        private AlarmHostController _controller;
        private string _seed;

        public AlarmHostApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<AlarmHostController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new AlarmHostSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            AlarmHostVM vm = _controller.Wtm.CreateVM<AlarmHostVM>();
            AlarmHost v = new AlarmHost();
            
            v.MonitorRoomId = AddMonitorRoom();
            v.AlarmHost_ID = "uJN4p0avzbaWNDMJRPzBLno55CHPA";
            v.User = "y7lEb9uXQ4O4fWzbfk";
            v.Password = "NUuUbYppbI3f7zPRSMCzpI0BXVkdijho1S";
            v.AlarmHostType = "6N4";
            v.AlarmHostIP = "GH9Gq7rpNI5YvNpVKn7joBeg4r";
            v.AlarmChannelCount = 11;
            v.install_time = "EzbOVfbjte2D6yEia5fjFWiwgYRhqxcgcaISfJZlwUWFMJyyUozK";
            v.category = "8nCx27jb3gSsqmpZCUHkJg4";
            v.Remark = "toGhG3q3ySJXzD";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<AlarmHost>().Find(v.ID);
                
                Assert.AreEqual(data.AlarmHost_ID, "uJN4p0avzbaWNDMJRPzBLno55CHPA");
                Assert.AreEqual(data.User, "y7lEb9uXQ4O4fWzbfk");
                Assert.AreEqual(data.Password, "NUuUbYppbI3f7zPRSMCzpI0BXVkdijho1S");
                Assert.AreEqual(data.AlarmHostType, "6N4");
                Assert.AreEqual(data.AlarmHostIP, "GH9Gq7rpNI5YvNpVKn7joBeg4r");
                Assert.AreEqual(data.AlarmChannelCount, 11);
                Assert.AreEqual(data.install_time, "EzbOVfbjte2D6yEia5fjFWiwgYRhqxcgcaISfJZlwUWFMJyyUozK");
                Assert.AreEqual(data.category, "8nCx27jb3gSsqmpZCUHkJg4");
                Assert.AreEqual(data.Remark, "toGhG3q3ySJXzD");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            AlarmHost v = new AlarmHost();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.MonitorRoomId = AddMonitorRoom();
                v.AlarmHost_ID = "uJN4p0avzbaWNDMJRPzBLno55CHPA";
                v.User = "y7lEb9uXQ4O4fWzbfk";
                v.Password = "NUuUbYppbI3f7zPRSMCzpI0BXVkdijho1S";
                v.AlarmHostType = "6N4";
                v.AlarmHostIP = "GH9Gq7rpNI5YvNpVKn7joBeg4r";
                v.AlarmChannelCount = 11;
                v.install_time = "EzbOVfbjte2D6yEia5fjFWiwgYRhqxcgcaISfJZlwUWFMJyyUozK";
                v.category = "8nCx27jb3gSsqmpZCUHkJg4";
                v.Remark = "toGhG3q3ySJXzD";
                context.Set<AlarmHost>().Add(v);
                context.SaveChanges();
            }

            AlarmHostVM vm = _controller.Wtm.CreateVM<AlarmHostVM>();
            var oldID = v.ID;
            v = new AlarmHost();
            v.ID = oldID;
       		
            v.AlarmHost_ID = "9ilz7a1kvtbgXU1KIOQUYQVt";
            v.User = "naY3v8lZdNhx8A0qC5y1t7D5j7uZtOqqwyK4DLvxYACGKua";
            v.Password = "wS1dKoNmkPxNbpJEhzj8Ek4XTNBGo6GEwmI3lhIYU3XqpI9";
            v.AlarmHostType = "HOC0Q7qXYO5pAX6BbFGXBT3ZvWzSR2JODhjeaX2Y0f1eLT7NX6EnH";
            v.AlarmHostIP = "zI46D1pQIQxxZuyUMA0yenzdRLzmwcQvurRGIEJaB6Hv5YsvUC5Ib";
            v.AlarmChannelCount = 47;
            v.install_time = "Z";
            v.category = "vBOwH9iv34T2O0eqyrmnOPdPJwcqbNJtGOqNwVAICh8oR6j9aw";
            v.Remark = "OZtrmGjQTQlCffG1NqsbBPKv4qEzqn4";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.MonitorRoomId", "");
            vm.FC.Add("Entity.AlarmHost_ID", "");
            vm.FC.Add("Entity.User", "");
            vm.FC.Add("Entity.Password", "");
            vm.FC.Add("Entity.AlarmHostType", "");
            vm.FC.Add("Entity.AlarmHostIP", "");
            vm.FC.Add("Entity.AlarmChannelCount", "");
            vm.FC.Add("Entity.install_time", "");
            vm.FC.Add("Entity.category", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<AlarmHost>().Find(v.ID);
 				
                Assert.AreEqual(data.AlarmHost_ID, "9ilz7a1kvtbgXU1KIOQUYQVt");
                Assert.AreEqual(data.User, "naY3v8lZdNhx8A0qC5y1t7D5j7uZtOqqwyK4DLvxYACGKua");
                Assert.AreEqual(data.Password, "wS1dKoNmkPxNbpJEhzj8Ek4XTNBGo6GEwmI3lhIYU3XqpI9");
                Assert.AreEqual(data.AlarmHostType, "HOC0Q7qXYO5pAX6BbFGXBT3ZvWzSR2JODhjeaX2Y0f1eLT7NX6EnH");
                Assert.AreEqual(data.AlarmHostIP, "zI46D1pQIQxxZuyUMA0yenzdRLzmwcQvurRGIEJaB6Hv5YsvUC5Ib");
                Assert.AreEqual(data.AlarmChannelCount, 47);
                Assert.AreEqual(data.install_time, "Z");
                Assert.AreEqual(data.category, "vBOwH9iv34T2O0eqyrmnOPdPJwcqbNJtGOqNwVAICh8oR6j9aw");
                Assert.AreEqual(data.Remark, "OZtrmGjQTQlCffG1NqsbBPKv4qEzqn4");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            AlarmHost v = new AlarmHost();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.MonitorRoomId = AddMonitorRoom();
                v.AlarmHost_ID = "uJN4p0avzbaWNDMJRPzBLno55CHPA";
                v.User = "y7lEb9uXQ4O4fWzbfk";
                v.Password = "NUuUbYppbI3f7zPRSMCzpI0BXVkdijho1S";
                v.AlarmHostType = "6N4";
                v.AlarmHostIP = "GH9Gq7rpNI5YvNpVKn7joBeg4r";
                v.AlarmChannelCount = 11;
                v.install_time = "EzbOVfbjte2D6yEia5fjFWiwgYRhqxcgcaISfJZlwUWFMJyyUozK";
                v.category = "8nCx27jb3gSsqmpZCUHkJg4";
                v.Remark = "toGhG3q3ySJXzD";
                context.Set<AlarmHost>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            AlarmHost v1 = new AlarmHost();
            AlarmHost v2 = new AlarmHost();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.MonitorRoomId = AddMonitorRoom();
                v1.AlarmHost_ID = "uJN4p0avzbaWNDMJRPzBLno55CHPA";
                v1.User = "y7lEb9uXQ4O4fWzbfk";
                v1.Password = "NUuUbYppbI3f7zPRSMCzpI0BXVkdijho1S";
                v1.AlarmHostType = "6N4";
                v1.AlarmHostIP = "GH9Gq7rpNI5YvNpVKn7joBeg4r";
                v1.AlarmChannelCount = 11;
                v1.install_time = "EzbOVfbjte2D6yEia5fjFWiwgYRhqxcgcaISfJZlwUWFMJyyUozK";
                v1.category = "8nCx27jb3gSsqmpZCUHkJg4";
                v1.Remark = "toGhG3q3ySJXzD";
                v2.MonitorRoomId = v1.MonitorRoomId; 
                v2.AlarmHost_ID = "9ilz7a1kvtbgXU1KIOQUYQVt";
                v2.User = "naY3v8lZdNhx8A0qC5y1t7D5j7uZtOqqwyK4DLvxYACGKua";
                v2.Password = "wS1dKoNmkPxNbpJEhzj8Ek4XTNBGo6GEwmI3lhIYU3XqpI9";
                v2.AlarmHostType = "HOC0Q7qXYO5pAX6BbFGXBT3ZvWzSR2JODhjeaX2Y0f1eLT7NX6EnH";
                v2.AlarmHostIP = "zI46D1pQIQxxZuyUMA0yenzdRLzmwcQvurRGIEJaB6Hv5YsvUC5Ib";
                v2.AlarmChannelCount = 47;
                v2.install_time = "Z";
                v2.category = "vBOwH9iv34T2O0eqyrmnOPdPJwcqbNJtGOqNwVAICh8oR6j9aw";
                v2.Remark = "OZtrmGjQTQlCffG1NqsbBPKv4qEzqn4";
                context.Set<AlarmHost>().Add(v1);
                context.Set<AlarmHost>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<AlarmHost>().Find(v1.ID);
                var data2 = context.Set<AlarmHost>().Find(v2.ID);
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

                v.Factory = "dl9C";
                v.RoomLocation = "Um1hPsNoCfCAPYH2LiIC";
                v.RoomType = "sGMElYtVEPjETFa4xpgDbkejuEgTG035JuXcMqvrrG";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
