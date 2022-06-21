using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Project.CameraLayoutVMs;
using OnMonitor.Model.Project;
using OnMonitor.DataAccess;
using OnMonitor.Model.Equipment;


namespace OnMonitor.Test
{
    [TestClass]
    public class CameraLayoutApiTest
    {
        private CameraLayoutController _controller;
        private string _seed;

        public CameraLayoutApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<CameraLayoutController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new CameraLayoutSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            CameraLayoutVM vm = _controller.Wtm.CreateVM<CameraLayoutVM>();
            CameraLayout v = new CameraLayout();
            
            v.monitorRoomId = AddMonitorRoom();
            v.Build = "q2jqRUhjD";
            v.Floor = "shNJ9pYwe89CQd7qDDLDkhB5xyQgiVjBXfyN2ZPxah6z";
            v.ExcelDataId = AddFileAttachment();
            v.LayoutInfoId = AddFileAttachment();
            v.LayoutInfoPDFId = AddFileAttachment();
            v.Remark = "VCYKuHxhV9dSBkvRsygF9FVLWvnNthprXtgUIFymcyLu3W59";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<CameraLayout>().Find(v.ID);
                
                Assert.AreEqual(data.Build, "q2jqRUhjD");
                Assert.AreEqual(data.Floor, "shNJ9pYwe89CQd7qDDLDkhB5xyQgiVjBXfyN2ZPxah6z");
                Assert.AreEqual(data.Remark, "VCYKuHxhV9dSBkvRsygF9FVLWvnNthprXtgUIFymcyLu3W59");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            CameraLayout v = new CameraLayout();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.monitorRoomId = AddMonitorRoom();
                v.Build = "q2jqRUhjD";
                v.Floor = "shNJ9pYwe89CQd7qDDLDkhB5xyQgiVjBXfyN2ZPxah6z";
                v.ExcelDataId = AddFileAttachment();
                v.LayoutInfoId = AddFileAttachment();
                v.LayoutInfoPDFId = AddFileAttachment();
                v.Remark = "VCYKuHxhV9dSBkvRsygF9FVLWvnNthprXtgUIFymcyLu3W59";
                context.Set<CameraLayout>().Add(v);
                context.SaveChanges();
            }

            CameraLayoutVM vm = _controller.Wtm.CreateVM<CameraLayoutVM>();
            var oldID = v.ID;
            v = new CameraLayout();
            v.ID = oldID;
       		
            v.Build = "TMMjx9Qo4jWfxEwmToAJxuVwShe1cEalJcp9";
            v.Floor = "fWTOKYP";
            v.Remark = "Y";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.monitorRoomId", "");
            vm.FC.Add("Entity.Build", "");
            vm.FC.Add("Entity.Floor", "");
            vm.FC.Add("Entity.ExcelDataId", "");
            vm.FC.Add("Entity.LayoutInfoId", "");
            vm.FC.Add("Entity.LayoutInfoPDFId", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<CameraLayout>().Find(v.ID);
 				
                Assert.AreEqual(data.Build, "TMMjx9Qo4jWfxEwmToAJxuVwShe1cEalJcp9");
                Assert.AreEqual(data.Floor, "fWTOKYP");
                Assert.AreEqual(data.Remark, "Y");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            CameraLayout v = new CameraLayout();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.monitorRoomId = AddMonitorRoom();
                v.Build = "q2jqRUhjD";
                v.Floor = "shNJ9pYwe89CQd7qDDLDkhB5xyQgiVjBXfyN2ZPxah6z";
                v.ExcelDataId = AddFileAttachment();
                v.LayoutInfoId = AddFileAttachment();
                v.LayoutInfoPDFId = AddFileAttachment();
                v.Remark = "VCYKuHxhV9dSBkvRsygF9FVLWvnNthprXtgUIFymcyLu3W59";
                context.Set<CameraLayout>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            CameraLayout v1 = new CameraLayout();
            CameraLayout v2 = new CameraLayout();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.monitorRoomId = AddMonitorRoom();
                v1.Build = "q2jqRUhjD";
                v1.Floor = "shNJ9pYwe89CQd7qDDLDkhB5xyQgiVjBXfyN2ZPxah6z";
                v1.ExcelDataId = AddFileAttachment();
                v1.LayoutInfoId = AddFileAttachment();
                v1.LayoutInfoPDFId = AddFileAttachment();
                v1.Remark = "VCYKuHxhV9dSBkvRsygF9FVLWvnNthprXtgUIFymcyLu3W59";
                v2.monitorRoomId = v1.monitorRoomId; 
                v2.Build = "TMMjx9Qo4jWfxEwmToAJxuVwShe1cEalJcp9";
                v2.Floor = "fWTOKYP";
                v2.ExcelDataId = v1.ExcelDataId; 
                v2.LayoutInfoId = v1.LayoutInfoId; 
                v2.LayoutInfoPDFId = v1.LayoutInfoPDFId; 
                v2.Remark = "Y";
                context.Set<CameraLayout>().Add(v1);
                context.Set<CameraLayout>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<CameraLayout>().Find(v1.ID);
                var data2 = context.Set<CameraLayout>().Find(v2.ID);
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

                v.Factory = "LlqGIDI5cjws6dbRM6nAE78U3hjrj4WV";
                v.RoomLocation = "BoZkQLWizh83uWLIG9n3nToTZ7jJrDXx2uw8JZzv8WvxSR";
                v.RoomType = "M41SZZqxom4TnQYMsX5AR3qbU";
                context.Set<MonitorRoom>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "yNCVKW5Wyz9EUoTo";
                v.FileExt = "u6yRj7";
                v.Path = "VY";
                v.Length = 16;
                v.UploadTime = DateTime.Parse("2022-07-26 10:48:53");
                v.SaveMode = "in7xSRNFHI02";
                v.ExtraInfo = "2eGB0QZDU";
                v.HandlerInfo = "LJ2WWvdDGPaP";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
