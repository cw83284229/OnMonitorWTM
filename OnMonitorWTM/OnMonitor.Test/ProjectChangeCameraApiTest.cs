using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Project.ProjectChangeCameraVMs;
using OnMonitor.Model.Project;
using OnMonitor.DataAccess;
using OnMonitor.Model.Equipment;


namespace OnMonitor.Test
{
    [TestClass]
    public class ProjectChangeCameraApiTest
    {
        private ProjectChangeCameraController _controller;
        private string _seed;

        public ProjectChangeCameraApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<ProjectChangeCameraController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new ProjectChangeCameraSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProjectChangeCameraVM vm = _controller.Wtm.CreateVM<ProjectChangeCameraVM>();
            ProjectChangeCamera v = new ProjectChangeCamera();
            
            v.ProjectManagesId = AddProjectManages();
            v.CameraId = AddCamera();
            v.ChangeLocation = "l3m88TJwscynQ101ATtaCeYI7p3GOpLUtKgLvtuImEeJ2fR1TaU5om";
            v.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.Completed;
            v.IsDismantle = true;
            v.Remark = "eP6DXgzdHoEFOl";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProjectChangeCamera>().Find(v.ID);
                
                Assert.AreEqual(data.ChangeLocation, "l3m88TJwscynQ101ATtaCeYI7p3GOpLUtKgLvtuImEeJ2fR1TaU5om");
                Assert.AreEqual(data.TransformationStatus, OnMonitor.Model.Project.TransformationStatus.Completed);
                Assert.AreEqual(data.IsDismantle, true);
                Assert.AreEqual(data.Remark, "eP6DXgzdHoEFOl");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            ProjectChangeCamera v = new ProjectChangeCamera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ProjectManagesId = AddProjectManages();
                v.CameraId = AddCamera();
                v.ChangeLocation = "l3m88TJwscynQ101ATtaCeYI7p3GOpLUtKgLvtuImEeJ2fR1TaU5om";
                v.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.Completed;
                v.IsDismantle = true;
                v.Remark = "eP6DXgzdHoEFOl";
                context.Set<ProjectChangeCamera>().Add(v);
                context.SaveChanges();
            }

            ProjectChangeCameraVM vm = _controller.Wtm.CreateVM<ProjectChangeCameraVM>();
            var oldID = v.ID;
            v = new ProjectChangeCamera();
            v.ID = oldID;
       		
            v.ChangeLocation = "vD6xaTygQUWMKzTpouo7bFfxhcD4eA3kYMhhEy6TLOgmSKsB2";
            v.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.OnGoing;
            v.IsDismantle = true;
            v.Remark = "0RsGLiujE1HMqUt3v8c4K3MA2sbfgdZIVc";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ProjectManagesId", "");
            vm.FC.Add("Entity.CameraId", "");
            vm.FC.Add("Entity.ChangeLocation", "");
            vm.FC.Add("Entity.TransformationStatus", "");
            vm.FC.Add("Entity.IsDismantle", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProjectChangeCamera>().Find(v.ID);
 				
                Assert.AreEqual(data.ChangeLocation, "vD6xaTygQUWMKzTpouo7bFfxhcD4eA3kYMhhEy6TLOgmSKsB2");
                Assert.AreEqual(data.TransformationStatus, OnMonitor.Model.Project.TransformationStatus.OnGoing);
                Assert.AreEqual(data.IsDismantle, true);
                Assert.AreEqual(data.Remark, "0RsGLiujE1HMqUt3v8c4K3MA2sbfgdZIVc");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            //ProjectChangeCamera v = new ProjectChangeCamera();
            //using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            //{
        		
            //    v.ProjectManagesId = AddProjectManages();
            //    v.CameraId = AddCamera();
            //    v.ChangeLocation = "l3m88TJwscynQ101ATtaCeYI7p3GOpLUtKgLvtuImEeJ2fR1TaU5om";
            //    v.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.Completed;
            //    v.IsDismantle = true;
            //    v.Remark = "eP6DXgzdHoEFOl";
            //    context.Set<ProjectChangeCamera>().Add(v);
            //    context.SaveChanges();
            //}
            //var rv = _controller.Get(v.ID.ToString());
            //Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ProjectChangeCamera v1 = new ProjectChangeCamera();
            ProjectChangeCamera v2 = new ProjectChangeCamera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectManagesId = AddProjectManages();
                v1.CameraId = AddCamera();
                v1.ChangeLocation = "l3m88TJwscynQ101ATtaCeYI7p3GOpLUtKgLvtuImEeJ2fR1TaU5om";
                v1.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.Completed;
                v1.IsDismantle = true;
                v1.Remark = "eP6DXgzdHoEFOl";
                v2.ProjectManagesId = v1.ProjectManagesId; 
                v2.CameraId = v1.CameraId; 
                v2.ChangeLocation = "vD6xaTygQUWMKzTpouo7bFfxhcD4eA3kYMhhEy6TLOgmSKsB2";
                v2.TransformationStatus = OnMonitor.Model.Project.TransformationStatus.OnGoing;
                v2.IsDismantle = true;
                v2.Remark = "0RsGLiujE1HMqUt3v8c4K3MA2sbfgdZIVc";
                context.Set<ProjectChangeCamera>().Add(v1);
                context.Set<ProjectChangeCamera>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ProjectChangeCamera>().Find(v1.ID);
                var data2 = context.Set<ProjectChangeCamera>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "DpsiHqZJ2";
                v.FileExt = "rKI";
                v.Path = "Nu7GwTdhp";
                v.Length = 60;
                v.UploadTime = DateTime.Parse("2023-03-04 10:10:27");
                v.SaveMode = "v";
                v.ExtraInfo = "4eaSnJSTjXI7kqq5SkE";
                v.HandlerInfo = "pytGPs7VIXLbmB";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddProjectManages()
        {
            ProjectManages v = new ProjectManages();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ProjectManageType = "MpbcBREOdEb3sjzdsllAa6tFWDFrMrGQniGTWEiUWoKZUgIo";
                v.ProjectName = "q3LLOblo32M9vFCOf8vmvwq80AHmf3kCW68wBkdOPjpBrrU";
                v.ProjectOrder = "PFkqYyVYK9wiEHuo1j4a5i335bCBtcPYpEXCWy6NASNl";
                v.StartWorkDate = "vPGiQTvqNG";
                v.CompleteDate = "zccvaf32ahETUasHyZUtIYlT6ZoQz2k";
                v.AcceptanceData = "haOevZsrmHAQXtjr15tpmT4u";
                v.ManufacturerName = "eX";
                v.ProjectSpecifications = "AExnIepAl3E";
                v.Build = "T4W8SwFyuItvdOt1o4UILTbyAhX6DwkFFhSb7jf";
                v.Floor = "6c150CbSp8Gk";
                v.ExcelDataId = AddFileAttachment();
                v.LayoutInfoId = AddFileAttachment();
                v.PowerInfo = "0zT";
                v.AlarmInfo = "Kn";
                v.AcceptanceResult = "0J2JF";
                v.Remark = "P92ct3qOd4Z9GmdU";
                context.Set<ProjectManages>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddMonitorRoom()
        {
            MonitorRoom v = new MonitorRoom();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Factory = "QcJk0RevzpFKuILUZke7PrK";
                v.RoomLocation = "Zv";
                v.RoomType = "GFfCbKC4CpeklPaSFyaR9YJGtMla";
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
                v.DVR_ID = "wF3Up67rvMdbG333dAi9WAUM";
                v.Home_server = "Ca7xnXNRcbtA7";
                v.Hard_drive = 60;
                v.DVR_IP = "E7ID7Kg95KNiovZoslg37SuNiwBSMwnfb0fYfEa1ki";
                v.DVR_port = "TC";
                v.DVR_usre = "k2gtydhLyGZ1NhqUK3i";
                v.DVR_possword = "sMeT6DD";
                v.install_time = "r0";
                v.Manufacturer = "rESgzAQd4qN6v";
                v.DVR_type = "OqieLMwJ0KWtHmUi3DEoo6ZNDwwjQ9D02GLt";
                v.DVR_SN = "tsGLxZ588wV2pbP5pgN4LSiFzw7GjrdIOvrGtUBu";
                v.DVR_Channel = 11;
                v.department = "qk664EWHXTgC6NkE8ChJA1iNsgJYIeb6QB3rKYCMeUNyP0zL";
                v.Cost_code = "n";
                v.Remark = "E0J7KIwK1cNx3e6";
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

                v.Name = "VH5LLt1cwFJikjexOwsVT0tci3CyfZpisWyPtOPU";
                v.Cost_code = "bxbnwj2pgr2pQD6ZBPrvNHpcPaTO2v5E";
                context.Set<Department>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddCamera()
        {
            Camera v = new Camera();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.DVRId = AddDVR();
                v.channel_ID = 56;
                v.Camera_ID = "7zFsKwyipp0S1yQBBwdG7c3Pv9cE";
                v.Build = "quQmo4k59P";
                v.floor = "UVd4WxguMzR1bzpp8ksIVoAdyhXgS3hbi";
                v.Direction = "g0uGV9SqlLFdcZuCNGBcBYadvyCyWQNzhvxmw";
                v.Location = "U0QV8j0w";
                v.MonitorClassification = OnMonitor.Model.Equipment.MonitorClassification.CommonRoom;
                v.CameraTpye = OnMonitor.Model.Equipment.CameraTpye.Analog;
                v.DepartmentId = AddDepartment();
                v.install_time = "w571jCPbsAfaSvle7KRXotREBFN8wUbV8pRnPPdXkiTfqp";
                v.manufacturer = "FZ8ExfRmpSwhkg7SOdtNzi2TCWsVd9MpTLai1sVREfxC4qH";
                v.category = "bNKC93gdtYM7m2jCO";
                v.Camera_IP = "5DZoGONxxgH3UfHrWKrWUQd9YW25Vhitgvtz";
                v.Camera_port = "pWIVy7SqHV8pDnluHXKzGdw0p";
                v.Camera_usre = "4MHJT2J9EdW2Y1v1sCCUGfvAxcenmV2kmxuxc";
                v.DVR_possword = "LQ9pLses4xNjIUxtvPrlKQSUxgMdmI1Ww9O81JsuBgA";
                v.Remark = "KIkeXO";
                context.Set<Camera>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
