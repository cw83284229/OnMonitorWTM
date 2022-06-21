using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using OnMonitor.ViewModel.Repair.CameraRepairVMs;
using OnMonitor.Model.Equipment;
using OnMonitor.Model.Repair;

namespace OnMonitor.Controllers
{
    [Area("Repair")]
    [AuthorizeJwt]
    [ActionDescription("镜头维修")]
    [ApiController]
    [Route("api/CameraRepair")]
	public partial class CameraRepairController : BaseApiController
    {
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(CameraRepairSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<CameraRepairListVM>();
                vm.Searcher = searcher;
                return Content(vm.GetJson(enumToString: false));
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }


        [ActionDescription("搜索重复")]
        [HttpPost("SearchRepeat")]
        public IActionResult SearchRepeat(CameraRepairSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<CameraRepairListVM>();
                vm.Searcher = searcher;



                return Content(vm.GetJson(enumToString: false));
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }
        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public CameraRepairVM Get(string id)
        {
            var vm = Wtm.CreateVM<CameraRepairVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(CameraRepairVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(CameraRepairVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }


        [ActionDescription("Sys.Edit2")]
        [HttpPut("Edit2")]
        public IActionResult Edit2(CameraRepairVM vm)
        {
            var vm2 = Wtm.CreateVM<CameraRepairVM>(vm.Entity.ID);
            vm.Entity.CameraId = vm2.Entity.CameraId;
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }


        [HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<CameraRepairBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(CameraRepairSearcher searcher)
        {
            var vm = Wtm.CreateVM<CameraRepairListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<CameraRepairListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<CameraRepairImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(CameraRepairImportVM vm)
        {

            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm.EntityList.Count);
            }
        }


        [HttpGet("GetCameras")]
        public ActionResult GetCameras()
        {
            return Ok(DC.Set<Camera>().Where(u=>!string.IsNullOrEmpty(u.Build)).GetSelectListItems(Wtm, x => x.Camera_ID));
        }
        [HttpGet("GetMonitorRoom")]
        public ActionResult GetMonitorRoom()
        {
            return Ok(DC.Set<MonitorRoom>().GetSelectListItems(Wtm, x => x.RoomLocation));
        }
        [HttpGet("GetRepairCameras")]
        public ActionResult GetRepairCameras()
        {
            return Ok(DC.Set<CameraRepair>().Where(u=>u.RepairState!=RepairState.Treated).GetSelectListItems(Wtm,x=>x.Camera.Camera_ID));
        }
    }
}
