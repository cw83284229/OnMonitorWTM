using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.Equipment.DepartmentVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.DataAccess;


namespace OnMonitor.Test
{
    [TestClass]
    public class DepartmentApiTest
    {
        private DepartmentController _controller;
        private string _seed;

        public DepartmentApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<DepartmentController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new DepartmentSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            DepartmentVM vm = _controller.Wtm.CreateVM<DepartmentVM>();
            Department v = new Department();
            
            v.Name = "6IJSBeHOfdPP9q6q0kbaLani29pBR7JDj7";
            v.Cost_code = "PtJsB35mvReVw1j2PLRFbGOPGjFRTA2Y";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Department>().Find(v.ID);
                
                Assert.AreEqual(data.Name, "6IJSBeHOfdPP9q6q0kbaLani29pBR7JDj7");
                Assert.AreEqual(data.Cost_code, "PtJsB35mvReVw1j2PLRFbGOPGjFRTA2Y");
            }
        }

        [TestMethod]
        public void EditTest()
        {
            Department v = new Department();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "6IJSBeHOfdPP9q6q0kbaLani29pBR7JDj7";
                v.Cost_code = "PtJsB35mvReVw1j2PLRFbGOPGjFRTA2Y";
                context.Set<Department>().Add(v);
                context.SaveChanges();
            }

            DepartmentVM vm = _controller.Wtm.CreateVM<DepartmentVM>();
            var oldID = v.ID;
            v = new Department();
            v.ID = oldID;
       		
            v.Name = "7dfif109Hcs2GYyt9gGrs0wbuGPKmMm3eT7m";
            v.Cost_code = "qaC4LUtKBAvqntXTcYAglWrcU5HRz9";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Cost_code", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Department>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "7dfif109Hcs2GYyt9gGrs0wbuGPKmMm3eT7m");
                Assert.AreEqual(data.Cost_code, "qaC4LUtKBAvqntXTcYAglWrcU5HRz9");
            }

        }

		[TestMethod]
        public void GetTest()
        {
            Department v = new Department();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "6IJSBeHOfdPP9q6q0kbaLani29pBR7JDj7";
                v.Cost_code = "PtJsB35mvReVw1j2PLRFbGOPGjFRTA2Y";
                context.Set<Department>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Department v1 = new Department();
            Department v2 = new Department();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "6IJSBeHOfdPP9q6q0kbaLani29pBR7JDj7";
                v1.Cost_code = "PtJsB35mvReVw1j2PLRFbGOPGjFRTA2Y";
                v2.Name = "7dfif109Hcs2GYyt9gGrs0wbuGPKmMm3eT7m";
                v2.Cost_code = "qaC4LUtKBAvqntXTcYAglWrcU5HRz9";
                context.Set<Department>().Add(v1);
                context.Set<Department>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Department>().Find(v1.ID);
                var data2 = context.Set<Department>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
