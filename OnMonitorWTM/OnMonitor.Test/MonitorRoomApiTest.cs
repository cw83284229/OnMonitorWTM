using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.MonitorRoomVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class MonitorRoomApiTest
    {
        private MonitorRoomController _controller;
        private string _seed;

        public MonitorRoomApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<MonitorRoomController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new MonitorRoomSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            MonitorRoomVM vm = _controller.Wtm.CreateVM<MonitorRoomVM>();
            MonitorRoom v = new MonitorRoom();
            
            v.Factory = "x76glvuJ0B3Q7IZihoQhxIlHhp6adseYnL";
            v.RoomLocation = "bkeiaU";
            v.RoomType = "Z03j1MDGUZfI7TSBxThP";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<MonitorRoom>().Find(v.ID);
                
                Assert.AreEqual(data.Factory, "x76glvuJ0B3Q7IZihoQhxIlHhp6adseYnL");
                Assert.AreEqual(data.RoomLocation, "bkeiaU");
                Assert.AreEqual(data.RoomType, "Z03j1MDGUZfI7TSBxThP");
            }
        }

        [TestMethod]
        public void EditTest()
        {
            MonitorRoom v = new MonitorRoom();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Factory = "x76glvuJ0B3Q7IZihoQhxIlHhp6adseYnL";
                v.RoomLocation = "bkeiaU";
                v.RoomType = "Z03j1MDGUZfI7TSBxThP";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
            }

            MonitorRoomVM vm = _controller.Wtm.CreateVM<MonitorRoomVM>();
            var oldID = v.ID;
            v = new MonitorRoom();
            v.ID = oldID;
       		
            v.Factory = "5rnzbQbiAiqtkoGiWsaArRpL8ZIj8";
            v.RoomLocation = "2jhbRpVLZXvGBYYG0GFpOnBKM9h3c";
            v.RoomType = "KbdZhhtSGZRM5yRflwfMtfusGgLHbmAc2Tof8q6";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Factory", "");
            vm.FC.Add("Entity.RoomLocation", "");
            vm.FC.Add("Entity.RoomType", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<MonitorRoom>().Find(v.ID);
 				
                Assert.AreEqual(data.Factory, "5rnzbQbiAiqtkoGiWsaArRpL8ZIj8");
                Assert.AreEqual(data.RoomLocation, "2jhbRpVLZXvGBYYG0GFpOnBKM9h3c");
                Assert.AreEqual(data.RoomType, "KbdZhhtSGZRM5yRflwfMtfusGgLHbmAc2Tof8q6");
            }

        }

		[TestMethod]
        public void GetTest()
        {
            MonitorRoom v = new MonitorRoom();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Factory = "x76glvuJ0B3Q7IZihoQhxIlHhp6adseYnL";
                v.RoomLocation = "bkeiaU";
                v.RoomType = "Z03j1MDGUZfI7TSBxThP";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            MonitorRoom v1 = new MonitorRoom();
            MonitorRoom v2 = new MonitorRoom();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Factory = "x76glvuJ0B3Q7IZihoQhxIlHhp6adseYnL";
                v1.RoomLocation = "bkeiaU";
                v1.RoomType = "Z03j1MDGUZfI7TSBxThP";
                v2.Factory = "5rnzbQbiAiqtkoGiWsaArRpL8ZIj8";
                v2.RoomLocation = "2jhbRpVLZXvGBYYG0GFpOnBKM9h3c";
                v2.RoomType = "KbdZhhtSGZRM5yRflwfMtfusGgLHbmAc2Tof8q6";
                context.Set<MonitorRoom>().Add(v1);
                context.Set<MonitorRoom>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<MonitorRoom>().Find(v1.ID);
                var data2 = context.Set<MonitorRoom>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
