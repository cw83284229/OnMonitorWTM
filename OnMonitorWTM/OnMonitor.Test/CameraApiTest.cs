using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.CameraVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class CameraApiTest
    {
        private CameraController _controller;
        private string _seed;

        public CameraApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<CameraController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new CameraSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            CameraVM vm = _controller.Wtm.CreateVM<CameraVM>();
            Camera v = new Camera();
            
            v.DVRId = AddDVR();
            v.channel_ID = 48;
            v.Camera_ID = "ZTKYozKbL7IqZ5pwmcTDdEjRC41rVLiVaA";
            v.Build = "s0nZ";
            v.floor = "qzlxlaKh";
            v.Direction = "qE6tWpOMdgoaKVF994d6ZuDB90tkEnDI1i9tU";
            v.Location = "KD7M";
            v.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.CommonRoom;
            v.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
            v.DepartmentId = AddDepartment();
            v.install_time = "ozyvRlwAasXnthpfiCmXr5iG";
            v.manufacturer = "X1YlcUwqsLaDnxDJ4TLkfalO28Acjh1";
            v.category = "J1mwX3yMcVNJXeVKd9sQKsJemmkJnSSrGUHfeCqNA7aD";
            v.Camera_IP = "uIBMCCy5uX7j7tPZeULZ4mI5JHh";
            v.Camera_port = "cE5C2wL25UQH8eQKKN5Emnv0b7GMOJWZakGkDuttFk0";
            v.Camera_usre = "rYtrAOmLoZZMOHWmQ2c0Cq9I7ZsalAlvlwfV4835";
            v.DVR_possword = "jUyhJk8fYBJPqOtCakfqf2v4uqFIyL0USNGAVmOfUyWCtF";
            v.Remark = "tXqXH2VlaaCDWrbFLQhgYCT9hjhWfOJTGW4PzNzK";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Camera>().Find(v.ID);
                
                Assert.AreEqual(data.channel_ID, 48);
                Assert.AreEqual(data.Camera_ID, "ZTKYozKbL7IqZ5pwmcTDdEjRC41rVLiVaA");
                Assert.AreEqual(data.Build, "s0nZ");
                Assert.AreEqual(data.floor, "qzlxlaKh");
                Assert.AreEqual(data.Direction, "qE6tWpOMdgoaKVF994d6ZuDB90tkEnDI1i9tU");
                Assert.AreEqual(data.Location, "KD7M");
                Assert.AreEqual(data.MonitorClassification, OnMonitor.Model.Equipment.MonitorClassification.CommonRoom);
                Assert.AreEqual(data.CameraTpye, OnMonitor.Model.Equipment.CameraTpye.SmartIPC);
                Assert.AreEqual(data.install_time, "ozyvRlwAasXnthpfiCmXr5iG");
                Assert.AreEqual(data.manufacturer, "X1YlcUwqsLaDnxDJ4TLkfalO28Acjh1");
                Assert.AreEqual(data.category, "J1mwX3yMcVNJXeVKd9sQKsJemmkJnSSrGUHfeCqNA7aD");
                Assert.AreEqual(data.Camera_IP, "uIBMCCy5uX7j7tPZeULZ4mI5JHh");
                Assert.AreEqual(data.Camera_port, "cE5C2wL25UQH8eQKKN5Emnv0b7GMOJWZakGkDuttFk0");
                Assert.AreEqual(data.Camera_usre, "rYtrAOmLoZZMOHWmQ2c0Cq9I7ZsalAlvlwfV4835");
                Assert.AreEqual(data.DVR_possword, "jUyhJk8fYBJPqOtCakfqf2v4uqFIyL0USNGAVmOfUyWCtF");
                Assert.AreEqual(data.Remark, "tXqXH2VlaaCDWrbFLQhgYCT9hjhWfOJTGW4PzNzK");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            Camera v = new Camera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DVRId = AddDVR();
                v.channel_ID = 48;
                v.Camera_ID = "ZTKYozKbL7IqZ5pwmcTDdEjRC41rVLiVaA";
                v.Build = "s0nZ";
                v.floor = "qzlxlaKh";
                v.Direction = "qE6tWpOMdgoaKVF994d6ZuDB90tkEnDI1i9tU";
                v.Location = "KD7M";
                v.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.CommonRoom;
                v.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
                v.DepartmentId = AddDepartment();
                v.install_time = "ozyvRlwAasXnthpfiCmXr5iG";
                v.manufacturer = "X1YlcUwqsLaDnxDJ4TLkfalO28Acjh1";
                v.category = "J1mwX3yMcVNJXeVKd9sQKsJemmkJnSSrGUHfeCqNA7aD";
                v.Camera_IP = "uIBMCCy5uX7j7tPZeULZ4mI5JHh";
                v.Camera_port = "cE5C2wL25UQH8eQKKN5Emnv0b7GMOJWZakGkDuttFk0";
                v.Camera_usre = "rYtrAOmLoZZMOHWmQ2c0Cq9I7ZsalAlvlwfV4835";
                v.DVR_possword = "jUyhJk8fYBJPqOtCakfqf2v4uqFIyL0USNGAVmOfUyWCtF";
                v.Remark = "tXqXH2VlaaCDWrbFLQhgYCT9hjhWfOJTGW4PzNzK";
                context.Set<Camera>().Add(v);
                context.SaveChanges();
            }

            CameraVM vm = _controller.Wtm.CreateVM<CameraVM>();
            var oldID = v.ID;
            v = new Camera();
            v.ID = oldID;
       		
            v.channel_ID = 40;
            v.Camera_ID = "gJSa1VCMkX4FbBm1xP";
            v.Build = "pdb0MN8vdE4tU9mFt";
            v.floor = "mugTUtJSQOawVh";
            v.Direction = "A0xnWeFzsCnieENSkNzE724Pxl5TJlRUuw5QKt6VxaeVako";
            v.Location = "b96ncUJrhlqkAsvzBdNgS";
            v.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.MaterialsRoom;
            v.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
            v.install_time = "pbdK4azeixpJKRkhbqdCieEtST5wTYhqG2L";
            v.manufacturer = "c3y4V5nctZeBQyxBwphK809y";
            v.category = "zE0jwrY1uup0NmshCygnnOM5WHTEhf";
            v.Camera_IP = "Khqxj3X2OgxIReFMy9cDJJLYPpkKAp";
            v.Camera_port = "3n8xju7pl9SgVZD8UOsPBEYIQrkD6eJLBabgI3XidsB1zKb";
            v.Camera_usre = "ciUpnIrleWatm9yt89e6g8qtz5zf";
            v.DVR_possword = "ea1H0AJCscqZ";
            v.Remark = "93ccYtEaLqfzQwKRu2MPx8AbcJ8ydrEw9uJQ5";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DVRId", "");
            vm.FC.Add("Entity.channel_ID", "");
            vm.FC.Add("Entity.Camera_ID", "");
            vm.FC.Add("Entity.Build", "");
            vm.FC.Add("Entity.floor", "");
            vm.FC.Add("Entity.Direction", "");
            vm.FC.Add("Entity.Location", "");
            vm.FC.Add("Entity.MonitorClassification", "");
            vm.FC.Add("Entity.CameraTpye", "");
            vm.FC.Add("Entity.DepartmentId", "");
            vm.FC.Add("Entity.install_time", "");
            vm.FC.Add("Entity.manufacturer", "");
            vm.FC.Add("Entity.category", "");
            vm.FC.Add("Entity.Camera_IP", "");
            vm.FC.Add("Entity.Camera_port", "");
            vm.FC.Add("Entity.Camera_usre", "");
            vm.FC.Add("Entity.DVR_possword", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Camera>().Find(v.ID);
 				
                Assert.AreEqual(data.channel_ID, 40);
                Assert.AreEqual(data.Camera_ID, "gJSa1VCMkX4FbBm1xP");
                Assert.AreEqual(data.Build, "pdb0MN8vdE4tU9mFt");
                Assert.AreEqual(data.floor, "mugTUtJSQOawVh");
                Assert.AreEqual(data.Direction, "A0xnWeFzsCnieENSkNzE724Pxl5TJlRUuw5QKt6VxaeVako");
                Assert.AreEqual(data.Location, "b96ncUJrhlqkAsvzBdNgS");
                Assert.AreEqual(data.MonitorClassification, OnMonitor.Model.Equipment.MonitorClassification.MaterialsRoom);
                Assert.AreEqual(data.CameraTpye, OnMonitor.Model.Equipment.CameraTpye.SmartIPC);
                Assert.AreEqual(data.install_time, "pbdK4azeixpJKRkhbqdCieEtST5wTYhqG2L");
                Assert.AreEqual(data.manufacturer, "c3y4V5nctZeBQyxBwphK809y");
                Assert.AreEqual(data.category, "zE0jwrY1uup0NmshCygnnOM5WHTEhf");
                Assert.AreEqual(data.Camera_IP, "Khqxj3X2OgxIReFMy9cDJJLYPpkKAp");
                Assert.AreEqual(data.Camera_port, "3n8xju7pl9SgVZD8UOsPBEYIQrkD6eJLBabgI3XidsB1zKb");
                Assert.AreEqual(data.Camera_usre, "ciUpnIrleWatm9yt89e6g8qtz5zf");
                Assert.AreEqual(data.DVR_possword, "ea1H0AJCscqZ");
                Assert.AreEqual(data.Remark, "93ccYtEaLqfzQwKRu2MPx8AbcJ8ydrEw9uJQ5");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            Camera v = new Camera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DVRId = AddDVR();
                v.channel_ID = 48;
                v.Camera_ID = "ZTKYozKbL7IqZ5pwmcTDdEjRC41rVLiVaA";
                v.Build = "s0nZ";
                v.floor = "qzlxlaKh";
                v.Direction = "qE6tWpOMdgoaKVF994d6ZuDB90tkEnDI1i9tU";
                v.Location = "KD7M";
                v.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.CommonRoom;
                v.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
                v.DepartmentId = AddDepartment();
                v.install_time = "ozyvRlwAasXnthpfiCmXr5iG";
                v.manufacturer = "X1YlcUwqsLaDnxDJ4TLkfalO28Acjh1";
                v.category = "J1mwX3yMcVNJXeVKd9sQKsJemmkJnSSrGUHfeCqNA7aD";
                v.Camera_IP = "uIBMCCy5uX7j7tPZeULZ4mI5JHh";
                v.Camera_port = "cE5C2wL25UQH8eQKKN5Emnv0b7GMOJWZakGkDuttFk0";
                v.Camera_usre = "rYtrAOmLoZZMOHWmQ2c0Cq9I7ZsalAlvlwfV4835";
                v.DVR_possword = "jUyhJk8fYBJPqOtCakfqf2v4uqFIyL0USNGAVmOfUyWCtF";
                v.Remark = "tXqXH2VlaaCDWrbFLQhgYCT9hjhWfOJTGW4PzNzK";
                context.Set<Camera>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Camera v1 = new Camera();
            Camera v2 = new Camera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DVRId = AddDVR();
                v1.channel_ID = 48;
                v1.Camera_ID = "ZTKYozKbL7IqZ5pwmcTDdEjRC41rVLiVaA";
                v1.Build = "s0nZ";
                v1.floor = "qzlxlaKh";
                v1.Direction = "qE6tWpOMdgoaKVF994d6ZuDB90tkEnDI1i9tU";
                v1.Location = "KD7M";
                v1.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.CommonRoom;
                v1.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
                v1.DepartmentId = AddDepartment();
                v1.install_time = "ozyvRlwAasXnthpfiCmXr5iG";
                v1.manufacturer = "X1YlcUwqsLaDnxDJ4TLkfalO28Acjh1";
                v1.category = "J1mwX3yMcVNJXeVKd9sQKsJemmkJnSSrGUHfeCqNA7aD";
                v1.Camera_IP = "uIBMCCy5uX7j7tPZeULZ4mI5JHh";
                v1.Camera_port = "cE5C2wL25UQH8eQKKN5Emnv0b7GMOJWZakGkDuttFk0";
                v1.Camera_usre = "rYtrAOmLoZZMOHWmQ2c0Cq9I7ZsalAlvlwfV4835";
                v1.DVR_possword = "jUyhJk8fYBJPqOtCakfqf2v4uqFIyL0USNGAVmOfUyWCtF";
                v1.Remark = "tXqXH2VlaaCDWrbFLQhgYCT9hjhWfOJTGW4PzNzK";
                v2.DVRId = v1.DVRId; 
                v2.channel_ID = 40;
                v2.Camera_ID = "gJSa1VCMkX4FbBm1xP";
                v2.Build = "pdb0MN8vdE4tU9mFt";
                v2.floor = "mugTUtJSQOawVh";
                v2.Direction = "A0xnWeFzsCnieENSkNzE724Pxl5TJlRUuw5QKt6VxaeVako";
                v2.Location = "b96ncUJrhlqkAsvzBdNgS";
                v2.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.MaterialsRoom;
                v2.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.SmartIPC;
                v2.DepartmentId = v1.DepartmentId; 
                v2.install_time = "pbdK4azeixpJKRkhbqdCieEtST5wTYhqG2L";
                v2.manufacturer = "c3y4V5nctZeBQyxBwphK809y";
                v2.category = "zE0jwrY1uup0NmshCygnnOM5WHTEhf";
                v2.Camera_IP = "Khqxj3X2OgxIReFMy9cDJJLYPpkKAp";
                v2.Camera_port = "3n8xju7pl9SgVZD8UOsPBEYIQrkD6eJLBabgI3XidsB1zKb";
                v2.Camera_usre = "ciUpnIrleWatm9yt89e6g8qtz5zf";
                v2.DVR_possword = "ea1H0AJCscqZ";
                v2.Remark = "93ccYtEaLqfzQwKRu2MPx8AbcJ8ydrEw9uJQ5";
                context.Set<Camera>().Add(v1);
                context.Set<Camera>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Camera>().Find(v1.ID);
                var data2 = context.Set<Camera>().Find(v2.ID);
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

                v.Factory = "PR";
                v.RoomLocation = "Big0VncOlm";
                v.RoomType = "n8CAy9AwVpseIlEogefqN9nvfue62rvLx";
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
                v.DVR_ID = "9tQoHqyxVJ23tnulwSjPNzACPwvcSL7edsMx8bBJTEJN";
                v.Home_server = "qpTjlKaSQ3T9IucAKQsoGPPsLGwjOPcESS";
                v.Hard_drive = 71;
                v.DVR_IP = "juFG4sp";
                v.DVR_port = "nVz3OHZEUPspAZI1isHnOgfYER1JBNpUsnGMgFdNi4";
                v.DVR_usre = "BwmrPjsQMAy7Qj455BwGjtvsecrhscbfws4Zz1";
                v.DVR_possword = "Ahzag5Yx1LnFqMbhf4jYqFdM1nnIrvfRwykm";
                v.install_time = "DPd3U1TK3JtxP9RB5bE";
                v.Manufacturer = "za5ia10DPScFc7EnFV4pUCK1LNjTZTgI8L";
                v.DVR_type = "hROuN7B6AJRK6xonLAzjdcpgXDBKU9";
                v.DVR_SN = "EFumcbWbddpS";
                v.DVR_Channel = 41;
                v.department = "3MGpVloJxxG3S9JOmyFhhAobRtdKYzlN0UICfEJ56SdgfD";
                v.Cost_code = "YEf7xrXFPYbfARQPndLTfdn0bvgOov34uFznbKs0mGDFo7";
                v.Remark = "5R0g9GAvKry5rXUMxTAvGzaiUJwMR9RxffeIgt1BUihqFVMWekuz9olo5uEANTTFcG5y5XejcqGJcP6afGQRZjZr2RtF5u8mcz8550Kcpb11raEBPpHVGMT5Kpy2flWXQUwWd";
                context.Set<DVR>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddDepartment()
        {
            Department v = new Department();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "BX1zoMuotaEEbiPAgumnJRyX2GO7N9rgiht1COdn";
                v.Cost_code = "KsNsr1Wktoki8rx4QVMvkZp";
                context.Set<Department>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
