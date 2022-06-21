using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.DVRVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class DVRApiTest
    {
        private DVRController _controller;
        private string _seed;

        public DVRApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<DVRController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new DVRSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            DVRVM vm = _controller.Wtm.CreateVM<DVRVM>();
            DVR v = new DVR();
            
            v.monitorRoomId = AddMonitorRoom();
            v.DVR_ID = "MhUTPl2s1Gi8BPLnsccKvG7qwU";
            v.Home_server = "0NyHD6i1FRHP4Ep7tHGvixGBXFGoPnQk7Nt5k17IwBF";
            v.Hard_drive = 70;
            v.DVR_IP = "wjRXNWpNxcJd0wDbCFWGF5iK8OQPd8BnQVrAzmzjQGvPJaV";
            v.DVR_port = "LdYigdcRW1hpEIbjnodlXjXV";
            v.DVR_usre = "FF0UeboWUMapaTdgSLWil6Fuk";
            v.DVR_possword = "cI6oS8dvijrNFGQl0E8iq0AhkUK2mgQ791N";
            v.install_time = "4kbVRLiRugR9RDT9ReznEpQFjtPmeo";
            v.Manufacturer = "SREzSNrru4AVBBVfxmovH9y";
            v.DVR_type = "QZRgNv";
            v.DVR_SN = "S9PwYxJCLfX0G";
            v.DVR_Channel = 40;
            v.department = "3uGK";
            v.Cost_code = "3jAqKZZAcCojROXFUlvJgdyjpkdVjMRKx5nLCZlrw";
            v.Remark = "QlwY0x246S8UmFkzCs7D6rzOxWBMc1GipbDOJOydh7I4ORGodTOUJZWzMfzKMQ5HzWkJriUGXolSNolx7pAI7OEZ34DQP5n3jS4Bm";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DVR>().Find(v.ID);
                
                Assert.AreEqual(data.DVR_ID, "MhUTPl2s1Gi8BPLnsccKvG7qwU");
                Assert.AreEqual(data.Home_server, "0NyHD6i1FRHP4Ep7tHGvixGBXFGoPnQk7Nt5k17IwBF");
                Assert.AreEqual(data.Hard_drive, 70);
                Assert.AreEqual(data.DVR_IP, "wjRXNWpNxcJd0wDbCFWGF5iK8OQPd8BnQVrAzmzjQGvPJaV");
                Assert.AreEqual(data.DVR_port, "LdYigdcRW1hpEIbjnodlXjXV");
                Assert.AreEqual(data.DVR_usre, "FF0UeboWUMapaTdgSLWil6Fuk");
                Assert.AreEqual(data.DVR_possword, "cI6oS8dvijrNFGQl0E8iq0AhkUK2mgQ791N");
                Assert.AreEqual(data.install_time, "4kbVRLiRugR9RDT9ReznEpQFjtPmeo");
                Assert.AreEqual(data.Manufacturer, "SREzSNrru4AVBBVfxmovH9y");
                Assert.AreEqual(data.DVR_type, "QZRgNv");
                Assert.AreEqual(data.DVR_SN, "S9PwYxJCLfX0G");
                Assert.AreEqual(data.DVR_Channel, 40);
                Assert.AreEqual(data.department, "3uGK");
                Assert.AreEqual(data.Cost_code, "3jAqKZZAcCojROXFUlvJgdyjpkdVjMRKx5nLCZlrw");
                Assert.AreEqual(data.Remark, "QlwY0x246S8UmFkzCs7D6rzOxWBMc1GipbDOJOydh7I4ORGodTOUJZWzMfzKMQ5HzWkJriUGXolSNolx7pAI7OEZ34DQP5n3jS4Bm");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            DVR v = new DVR();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.monitorRoomId = AddMonitorRoom();
                v.DVR_ID = "MhUTPl2s1Gi8BPLnsccKvG7qwU";
                v.Home_server = "0NyHD6i1FRHP4Ep7tHGvixGBXFGoPnQk7Nt5k17IwBF";
                v.Hard_drive = 70;
                v.DVR_IP = "wjRXNWpNxcJd0wDbCFWGF5iK8OQPd8BnQVrAzmzjQGvPJaV";
                v.DVR_port = "LdYigdcRW1hpEIbjnodlXjXV";
                v.DVR_usre = "FF0UeboWUMapaTdgSLWil6Fuk";
                v.DVR_possword = "cI6oS8dvijrNFGQl0E8iq0AhkUK2mgQ791N";
                v.install_time = "4kbVRLiRugR9RDT9ReznEpQFjtPmeo";
                v.Manufacturer = "SREzSNrru4AVBBVfxmovH9y";
                v.DVR_type = "QZRgNv";
                v.DVR_SN = "S9PwYxJCLfX0G";
                v.DVR_Channel = 40;
                v.department = "3uGK";
                v.Cost_code = "3jAqKZZAcCojROXFUlvJgdyjpkdVjMRKx5nLCZlrw";
                v.Remark = "QlwY0x246S8UmFkzCs7D6rzOxWBMc1GipbDOJOydh7I4ORGodTOUJZWzMfzKMQ5HzWkJriUGXolSNolx7pAI7OEZ34DQP5n3jS4Bm";
                context.Set<DVR>().Add(v);
                context.SaveChanges();
            }

            DVRVM vm = _controller.Wtm.CreateVM<DVRVM>();
            var oldID = v.ID;
            v = new DVR();
            v.ID = oldID;
       		
            v.DVR_ID = "hwHIrxvBcgvzrs0YOXE5b7oJbCuoImi2eV";
            v.Home_server = "rPxUOpAdJ02KUVXyAzsxQY8bf6GEA06ybVliMuXnLFlQzM0Iu";
            v.Hard_drive = 5;
            v.DVR_IP = "oxUurGcmDfdXYNIDtEshw9vpcbc";
            v.DVR_port = "juZSeCmnGAM4Bk52yoRs0zFjPoxEYGmCO";
            v.DVR_usre = "hl6cPT6FGR5UT3Kxbcyu8cr";
            v.DVR_possword = "29gb3QOsP7Cy22OQQd3j0YHwd4XjabyU8yJOQqt";
            v.install_time = "o1B";
            v.Manufacturer = "dIfaNneU8vAE67qH2lO7AGX";
            v.DVR_type = "tmplMNrZObewvSb4FQckacMOBzRmKGWxkne268d4";
            v.DVR_SN = "WmQHjqjiC";
            v.DVR_Channel = 63;
            v.department = "BWVuuhsPXy8jo7up62uS80LhgzBvSL2yqqTmpGDgYqfn";
            v.Cost_code = "X565GkMjEwiSUI7FpZQzklbf7O5k6";
            v.Remark = "Ed";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.monitorRoomId", "");
            vm.FC.Add("Entity.DVR_ID", "");
            vm.FC.Add("Entity.Home_server", "");
            vm.FC.Add("Entity.Hard_drive", "");
            vm.FC.Add("Entity.DVR_IP", "");
            vm.FC.Add("Entity.DVR_port", "");
            vm.FC.Add("Entity.DVR_usre", "");
            vm.FC.Add("Entity.DVR_possword", "");
            vm.FC.Add("Entity.install_time", "");
            vm.FC.Add("Entity.Manufacturer", "");
            vm.FC.Add("Entity.DVR_type", "");
            vm.FC.Add("Entity.DVR_SN", "");
            vm.FC.Add("Entity.DVR_Channel", "");
            vm.FC.Add("Entity.department", "");
            vm.FC.Add("Entity.Cost_code", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DVR>().Find(v.ID);
 				
                Assert.AreEqual(data.DVR_ID, "hwHIrxvBcgvzrs0YOXE5b7oJbCuoImi2eV");
                Assert.AreEqual(data.Home_server, "rPxUOpAdJ02KUVXyAzsxQY8bf6GEA06ybVliMuXnLFlQzM0Iu");
                Assert.AreEqual(data.Hard_drive, 5);
                Assert.AreEqual(data.DVR_IP, "oxUurGcmDfdXYNIDtEshw9vpcbc");
                Assert.AreEqual(data.DVR_port, "juZSeCmnGAM4Bk52yoRs0zFjPoxEYGmCO");
                Assert.AreEqual(data.DVR_usre, "hl6cPT6FGR5UT3Kxbcyu8cr");
                Assert.AreEqual(data.DVR_possword, "29gb3QOsP7Cy22OQQd3j0YHwd4XjabyU8yJOQqt");
                Assert.AreEqual(data.install_time, "o1B");
                Assert.AreEqual(data.Manufacturer, "dIfaNneU8vAE67qH2lO7AGX");
                Assert.AreEqual(data.DVR_type, "tmplMNrZObewvSb4FQckacMOBzRmKGWxkne268d4");
                Assert.AreEqual(data.DVR_SN, "WmQHjqjiC");
                Assert.AreEqual(data.DVR_Channel, 63);
                Assert.AreEqual(data.department, "BWVuuhsPXy8jo7up62uS80LhgzBvSL2yqqTmpGDgYqfn");
                Assert.AreEqual(data.Cost_code, "X565GkMjEwiSUI7FpZQzklbf7O5k6");
                Assert.AreEqual(data.Remark, "Ed");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            DVR v = new DVR();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.monitorRoomId = AddMonitorRoom();
                v.DVR_ID = "MhUTPl2s1Gi8BPLnsccKvG7qwU";
                v.Home_server = "0NyHD6i1FRHP4Ep7tHGvixGBXFGoPnQk7Nt5k17IwBF";
                v.Hard_drive = 70;
                v.DVR_IP = "wjRXNWpNxcJd0wDbCFWGF5iK8OQPd8BnQVrAzmzjQGvPJaV";
                v.DVR_port = "LdYigdcRW1hpEIbjnodlXjXV";
                v.DVR_usre = "FF0UeboWUMapaTdgSLWil6Fuk";
                v.DVR_possword = "cI6oS8dvijrNFGQl0E8iq0AhkUK2mgQ791N";
                v.install_time = "4kbVRLiRugR9RDT9ReznEpQFjtPmeo";
                v.Manufacturer = "SREzSNrru4AVBBVfxmovH9y";
                v.DVR_type = "QZRgNv";
                v.DVR_SN = "S9PwYxJCLfX0G";
                v.DVR_Channel = 40;
                v.department = "3uGK";
                v.Cost_code = "3jAqKZZAcCojROXFUlvJgdyjpkdVjMRKx5nLCZlrw";
                v.Remark = "QlwY0x246S8UmFkzCs7D6rzOxWBMc1GipbDOJOydh7I4ORGodTOUJZWzMfzKMQ5HzWkJriUGXolSNolx7pAI7OEZ34DQP5n3jS4Bm";
                context.Set<DVR>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            DVR v1 = new DVR();
            DVR v2 = new DVR();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.monitorRoomId = AddMonitorRoom();
                v1.DVR_ID = "MhUTPl2s1Gi8BPLnsccKvG7qwU";
                v1.Home_server = "0NyHD6i1FRHP4Ep7tHGvixGBXFGoPnQk7Nt5k17IwBF";
                v1.Hard_drive = 70;
                v1.DVR_IP = "wjRXNWpNxcJd0wDbCFWGF5iK8OQPd8BnQVrAzmzjQGvPJaV";
                v1.DVR_port = "LdYigdcRW1hpEIbjnodlXjXV";
                v1.DVR_usre = "FF0UeboWUMapaTdgSLWil6Fuk";
                v1.DVR_possword = "cI6oS8dvijrNFGQl0E8iq0AhkUK2mgQ791N";
                v1.install_time = "4kbVRLiRugR9RDT9ReznEpQFjtPmeo";
                v1.Manufacturer = "SREzSNrru4AVBBVfxmovH9y";
                v1.DVR_type = "QZRgNv";
                v1.DVR_SN = "S9PwYxJCLfX0G";
                v1.DVR_Channel = 40;
                v1.department = "3uGK";
                v1.Cost_code = "3jAqKZZAcCojROXFUlvJgdyjpkdVjMRKx5nLCZlrw";
                v1.Remark = "QlwY0x246S8UmFkzCs7D6rzOxWBMc1GipbDOJOydh7I4ORGodTOUJZWzMfzKMQ5HzWkJriUGXolSNolx7pAI7OEZ34DQP5n3jS4Bm";
                v2.monitorRoomId = v1.monitorRoomId; 
                v2.DVR_ID = "hwHIrxvBcgvzrs0YOXE5b7oJbCuoImi2eV";
                v2.Home_server = "rPxUOpAdJ02KUVXyAzsxQY8bf6GEA06ybVliMuXnLFlQzM0Iu";
                v2.Hard_drive = 5;
                v2.DVR_IP = "oxUurGcmDfdXYNIDtEshw9vpcbc";
                v2.DVR_port = "juZSeCmnGAM4Bk52yoRs0zFjPoxEYGmCO";
                v2.DVR_usre = "hl6cPT6FGR5UT3Kxbcyu8cr";
                v2.DVR_possword = "29gb3QOsP7Cy22OQQd3j0YHwd4XjabyU8yJOQqt";
                v2.install_time = "o1B";
                v2.Manufacturer = "dIfaNneU8vAE67qH2lO7AGX";
                v2.DVR_type = "tmplMNrZObewvSb4FQckacMOBzRmKGWxkne268d4";
                v2.DVR_SN = "WmQHjqjiC";
                v2.DVR_Channel = 63;
                v2.department = "BWVuuhsPXy8jo7up62uS80LhgzBvSL2yqqTmpGDgYqfn";
                v2.Cost_code = "X565GkMjEwiSUI7FpZQzklbf7O5k6";
                v2.Remark = "Ed";
                context.Set<DVR>().Add(v1);
                context.Set<DVR>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DVR>().Find(v1.ID);
                var data2 = context.Set<DVR>().Find(v2.ID);
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

                v.Factory = "zpCcFHKsjRMEwPJf07PH0ePr9QFtQxmkojrRA";
                v.RoomLocation = "MwSNEy39hP";
                v.RoomType = "dr72yxClmxKL7u";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
