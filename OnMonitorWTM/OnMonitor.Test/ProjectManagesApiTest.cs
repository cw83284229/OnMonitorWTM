using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Project.ProjectManagesVMs;
using OnMonitor.Model.Project;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class ProjectManagesApiTest
    {
        private ProjectManagesController _controller;
        private string _seed;

        public ProjectManagesApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<ProjectManagesController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new ProjectManagesSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            ProjectManagesVM vm = _controller.Wtm.CreateVM<ProjectManagesVM>();
            ProjectManages v = new ProjectManages();
            
            v.ProjectManageType = "nuhBAosfKgh8ZHVd4NCfMLfHqxc7BPSjDQ08Af30kflS";
            v.ProjectName = "uycjRZAESZZ041Puyzbbko0xO3iR3SolaX3";
            v.ProjectOrder = "KSYK";
            v.StartWorkDate = "Mpn8t02hGW2j6m6";
            v.CompleteDate = "Hxmwy7uIGHLvRmMocVrCPlgEzt56X4j";
            v.AcceptanceData = "xsLKIlf6Tx1pF";
            v.ManufacturerName = "6pBJ";
            v.ProjectSpecifications = "ewAoVOT8VEkPiTmZn";
            v.Build = "YJ8c89qbtXZq9KfJtncwg2KY7sU68dLz5G8NyJN";
            v.Floor = "9SJuImqxhneps1";
            v.ExcelDataId = AddFileAttachment();
            v.LayoutInfoId = AddFileAttachment();
            v.PowerInfo = "SXwcsdNI";
            v.AlarmInfo = "7sf1qBM5tlYgq5";
            v.AcceptanceResult = "8tYtfxwPuBpJa5ILXH";
            v.Remark = "tH1q8W35bKIlP3z0sljfx46uqM3NnYvKwmFKuvJ4cbawxl2";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProjectManages>().Find(v.ID);
                
                Assert.AreEqual(data.ProjectManageType, "nuhBAosfKgh8ZHVd4NCfMLfHqxc7BPSjDQ08Af30kflS");
                Assert.AreEqual(data.ProjectName, "uycjRZAESZZ041Puyzbbko0xO3iR3SolaX3");
                Assert.AreEqual(data.ProjectOrder, "KSYK");
                Assert.AreEqual(data.StartWorkDate, "Mpn8t02hGW2j6m6");
                Assert.AreEqual(data.CompleteDate, "Hxmwy7uIGHLvRmMocVrCPlgEzt56X4j");
                Assert.AreEqual(data.AcceptanceData, "xsLKIlf6Tx1pF");
                Assert.AreEqual(data.ManufacturerName, "6pBJ");
                Assert.AreEqual(data.ProjectSpecifications, "ewAoVOT8VEkPiTmZn");
                Assert.AreEqual(data.Build, "YJ8c89qbtXZq9KfJtncwg2KY7sU68dLz5G8NyJN");
                Assert.AreEqual(data.Floor, "9SJuImqxhneps1");
                Assert.AreEqual(data.PowerInfo, "SXwcsdNI");
                Assert.AreEqual(data.AlarmInfo, "7sf1qBM5tlYgq5");
                Assert.AreEqual(data.AcceptanceResult, "8tYtfxwPuBpJa5ILXH");
                Assert.AreEqual(data.Remark, "tH1q8W35bKIlP3z0sljfx46uqM3NnYvKwmFKuvJ4cbawxl2");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            ProjectManages v = new ProjectManages();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ProjectManageType = "nuhBAosfKgh8ZHVd4NCfMLfHqxc7BPSjDQ08Af30kflS";
                v.ProjectName = "uycjRZAESZZ041Puyzbbko0xO3iR3SolaX3";
                v.ProjectOrder = "KSYK";
                v.StartWorkDate = "Mpn8t02hGW2j6m6";
                v.CompleteDate = "Hxmwy7uIGHLvRmMocVrCPlgEzt56X4j";
                v.AcceptanceData = "xsLKIlf6Tx1pF";
                v.ManufacturerName = "6pBJ";
                v.ProjectSpecifications = "ewAoVOT8VEkPiTmZn";
                v.Build = "YJ8c89qbtXZq9KfJtncwg2KY7sU68dLz5G8NyJN";
                v.Floor = "9SJuImqxhneps1";
                v.ExcelDataId = AddFileAttachment();
                v.LayoutInfoId = AddFileAttachment();
                v.PowerInfo = "SXwcsdNI";
                v.AlarmInfo = "7sf1qBM5tlYgq5";
                v.AcceptanceResult = "8tYtfxwPuBpJa5ILXH";
                v.Remark = "tH1q8W35bKIlP3z0sljfx46uqM3NnYvKwmFKuvJ4cbawxl2";
                context.Set<ProjectManages>().Add(v);
                context.SaveChanges();
            }

            ProjectManagesVM vm = _controller.Wtm.CreateVM<ProjectManagesVM>();
            var oldID = v.ID;
            v = new ProjectManages();
            v.ID = oldID;
       		
            v.ProjectManageType = "MjCXgWxu5f4NOekwsmp9C";
            v.ProjectName = "YEJHOSv1Is05lg9wEhHMFExlGFjejddJ7utm1Mq";
            v.ProjectOrder = "q0d7rthCF4IsBRrgmrPmWDQZlxjj7TLVIA5gKscGEdhFeMz31";
            v.StartWorkDate = "o";
            v.CompleteDate = "46FDsVG6l0dKvkWt65j0Fng7pQ4jy6quMTcouBp9H";
            v.AcceptanceData = "LFFQGQbvH9cLbJTTiqwBJrf9wsCq7q";
            v.ManufacturerName = "DrmZAIb100u4wuSO6vCkRaIvKyWbWPX6aPr7YF68U9Fea6zG8G9k";
            v.ProjectSpecifications = "172l3Bvfrp";
            v.Build = "wEHVo";
            v.Floor = "GY1ylsH428135FkVV9maCERqpDy1zwq8ik";
            v.PowerInfo = "s";
            v.AlarmInfo = "VZqyZ5oK3JL87";
            v.AcceptanceResult = "d";
            v.Remark = "DHKBBPfFItqTDFrQF41TTwqtgxHHNQ9i10yrtj598";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ProjectManageType", "");
            vm.FC.Add("Entity.ProjectName", "");
            vm.FC.Add("Entity.ProjectOrder", "");
            vm.FC.Add("Entity.StartWorkDate", "");
            vm.FC.Add("Entity.CompleteDate", "");
            vm.FC.Add("Entity.AcceptanceData", "");
            vm.FC.Add("Entity.ManufacturerName", "");
            vm.FC.Add("Entity.ProjectSpecifications", "");
            vm.FC.Add("Entity.Build", "");
            vm.FC.Add("Entity.Floor", "");
            vm.FC.Add("Entity.ExcelDataId", "");
            vm.FC.Add("Entity.LayoutInfoId", "");
            vm.FC.Add("Entity.PowerInfo", "");
            vm.FC.Add("Entity.AlarmInfo", "");
            vm.FC.Add("Entity.AcceptanceResult", "");
            vm.FC.Add("Entity.Remark", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ProjectManages>().Find(v.ID);
 				
                Assert.AreEqual(data.ProjectManageType, "MjCXgWxu5f4NOekwsmp9C");
                Assert.AreEqual(data.ProjectName, "YEJHOSv1Is05lg9wEhHMFExlGFjejddJ7utm1Mq");
                Assert.AreEqual(data.ProjectOrder, "q0d7rthCF4IsBRrgmrPmWDQZlxjj7TLVIA5gKscGEdhFeMz31");
                Assert.AreEqual(data.StartWorkDate, "o");
                Assert.AreEqual(data.CompleteDate, "46FDsVG6l0dKvkWt65j0Fng7pQ4jy6quMTcouBp9H");
                Assert.AreEqual(data.AcceptanceData, "LFFQGQbvH9cLbJTTiqwBJrf9wsCq7q");
                Assert.AreEqual(data.ManufacturerName, "DrmZAIb100u4wuSO6vCkRaIvKyWbWPX6aPr7YF68U9Fea6zG8G9k");
                Assert.AreEqual(data.ProjectSpecifications, "172l3Bvfrp");
                Assert.AreEqual(data.Build, "wEHVo");
                Assert.AreEqual(data.Floor, "GY1ylsH428135FkVV9maCERqpDy1zwq8ik");
                Assert.AreEqual(data.PowerInfo, "s");
                Assert.AreEqual(data.AlarmInfo, "VZqyZ5oK3JL87");
                Assert.AreEqual(data.AcceptanceResult, "d");
                Assert.AreEqual(data.Remark, "DHKBBPfFItqTDFrQF41TTwqtgxHHNQ9i10yrtj598");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            ProjectManages v = new ProjectManages();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ProjectManageType = "nuhBAosfKgh8ZHVd4NCfMLfHqxc7BPSjDQ08Af30kflS";
                v.ProjectName = "uycjRZAESZZ041Puyzbbko0xO3iR3SolaX3";
                v.ProjectOrder = "KSYK";
                v.StartWorkDate = "Mpn8t02hGW2j6m6";
                v.CompleteDate = "Hxmwy7uIGHLvRmMocVrCPlgEzt56X4j";
                v.AcceptanceData = "xsLKIlf6Tx1pF";
                v.ManufacturerName = "6pBJ";
                v.ProjectSpecifications = "ewAoVOT8VEkPiTmZn";
                v.Build = "YJ8c89qbtXZq9KfJtncwg2KY7sU68dLz5G8NyJN";
                v.Floor = "9SJuImqxhneps1";
                v.ExcelDataId = AddFileAttachment();
                v.LayoutInfoId = AddFileAttachment();
                v.PowerInfo = "SXwcsdNI";
                v.AlarmInfo = "7sf1qBM5tlYgq5";
                v.AcceptanceResult = "8tYtfxwPuBpJa5ILXH";
                v.Remark = "tH1q8W35bKIlP3z0sljfx46uqM3NnYvKwmFKuvJ4cbawxl2";
                context.Set<ProjectManages>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ProjectManages v1 = new ProjectManages();
            ProjectManages v2 = new ProjectManages();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectManageType = "nuhBAosfKgh8ZHVd4NCfMLfHqxc7BPSjDQ08Af30kflS";
                v1.ProjectName = "uycjRZAESZZ041Puyzbbko0xO3iR3SolaX3";
                v1.ProjectOrder = "KSYK";
                v1.StartWorkDate = "Mpn8t02hGW2j6m6";
                v1.CompleteDate = "Hxmwy7uIGHLvRmMocVrCPlgEzt56X4j";
                v1.AcceptanceData = "xsLKIlf6Tx1pF";
                v1.ManufacturerName = "6pBJ";
                v1.ProjectSpecifications = "ewAoVOT8VEkPiTmZn";
                v1.Build = "YJ8c89qbtXZq9KfJtncwg2KY7sU68dLz5G8NyJN";
                v1.Floor = "9SJuImqxhneps1";
                v1.ExcelDataId = AddFileAttachment();
                v1.LayoutInfoId = AddFileAttachment();
                v1.PowerInfo = "SXwcsdNI";
                v1.AlarmInfo = "7sf1qBM5tlYgq5";
                v1.AcceptanceResult = "8tYtfxwPuBpJa5ILXH";
                v1.Remark = "tH1q8W35bKIlP3z0sljfx46uqM3NnYvKwmFKuvJ4cbawxl2";
                v2.ProjectManageType = "MjCXgWxu5f4NOekwsmp9C";
                v2.ProjectName = "YEJHOSv1Is05lg9wEhHMFExlGFjejddJ7utm1Mq";
                v2.ProjectOrder = "q0d7rthCF4IsBRrgmrPmWDQZlxjj7TLVIA5gKscGEdhFeMz31";
                v2.StartWorkDate = "o";
                v2.CompleteDate = "46FDsVG6l0dKvkWt65j0Fng7pQ4jy6quMTcouBp9H";
                v2.AcceptanceData = "LFFQGQbvH9cLbJTTiqwBJrf9wsCq7q";
                v2.ManufacturerName = "DrmZAIb100u4wuSO6vCkRaIvKyWbWPX6aPr7YF68U9Fea6zG8G9k";
                v2.ProjectSpecifications = "172l3Bvfrp";
                v2.Build = "wEHVo";
                v2.Floor = "GY1ylsH428135FkVV9maCERqpDy1zwq8ik";
                v2.ExcelDataId = v1.ExcelDataId; 
                v2.LayoutInfoId = v1.LayoutInfoId; 
                v2.PowerInfo = "s";
                v2.AlarmInfo = "VZqyZ5oK3JL87";
                v2.AcceptanceResult = "d";
                v2.Remark = "DHKBBPfFItqTDFrQF41TTwqtgxHHNQ9i10yrtj598";
                context.Set<ProjectManages>().Add(v1);
                context.Set<ProjectManages>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<ProjectManages>().Find(v1.ID);
                var data2 = context.Set<ProjectManages>().Find(v2.ID);
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

                v.FileName = "R1tU";
                v.FileExt = "oYX6KJE";
                v.Path = "DTOGJ";
                v.Length = 19;
                v.UploadTime = DateTime.Parse("2022-01-13 09:06:43");
                v.SaveMode = "s";
                v.ExtraInfo = "PBfvWiMwlpMO";
                v.HandlerInfo = "9irtyR19XAJGzjL2S";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
