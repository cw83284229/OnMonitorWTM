using BootstrapBlazor.Components;
using OnMonitor.Model.Repair;
using OnMonitor.Shared.Pages.DVROperation;
using OnMonitor.ViewModel.Repair.CameraRepairVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Shared.Pages.Repair.CameraRepair
{
    public partial  class Index
    {
        private CameraRepairSearcher SearchModel  = new CameraRepairSearcher();
        private Table<CameraRepair_View> dataTable;
       
        private List<SelectedItem> AllCameras = new List<SelectedItem>();
        private List<SelectedItem> AllRooms = new List<SelectedItem>();
        private QueryData<CameraRepair_View> queryData  = new QueryData<CameraRepair_View>();
        private int fourclass=0; 
        private int fourclass2= 0;
        
        private DateTimeRangeValue innerValue = new DateTimeRangeValue();

        protected override async Task OnInitializedAsync()
        {
            var opts3 = new QueryPageOptions() { PageItems=100000};
            var fivedata = await StartSearch<CameraRepair_View>("/api/CameraRepair/search", new CameraRepairSearcher() { AnomalyGrade = new AnomalyGrade[] { AnomalyGrade.four, AnomalyGrade.Five } }, opts3);
            fourclass2 = fivedata.Items.Where(u=>u.AnomalyGrade==AnomalyGrade.Five).Count();
            fourclass = fivedata.Items.Where(u => u.AnomalyGrade == AnomalyGrade.four).Count();

            AllCameras = await WtmBlazor.Api.CallItemsApi("/api/CameraRepair/GetCameras", placeholder: WtmBlazor.Localizer["Sys.All"]);
            AllRooms = await WtmBlazor.Api.CallItemsApi("/api/CameraRepair/GetMonitorRoom", placeholder: WtmBlazor.Localizer["Sys.All"]);


            await base.OnInitializedAsync();
        }

        private async Task<QueryData<CameraRepair_View>> OnSearch(QueryPageOptions opts)
        {
            var opts2 = new QueryPageOptions();
            opts2.IsPage = true;
            opts2.PageItems = 100000;
          

            
            queryData = await StartSearch<CameraRepair_View>("/api/CameraRepair/search", SearchModel, opts2);
          

            // 处理 SearchText 模糊搜索
            if (!string.IsNullOrEmpty(opts.SearchText))
                {
                SearchModel.Location = opts.SearchText;
                var querya = await StartSearch<CameraRepair_View>("/api/CameraRepair/search", SearchModel, opts);

                SearchModel.Location = null;
                return querya;
                
                }

            return await StartSearch<CameraRepair_View>("/api/CameraRepair/search", SearchModel, opts);
        }

        private void DoSearch()
        {
            dataTable.QueryAsync();
        }

        private async Task OnCreateClick(IEnumerable<CameraRepair_View> items)
        {
            if (await OpenDialog<Create>(WtmBlazor.Localizer["新增异常"], null, Size.Medium) == DialogResult.Yes)
            {
                await dataTable.QueryAsync();
            }
        }

        private async Task OnEditClick(CameraRepair_View item)
        {
            if (await OpenDialog<Edit>(WtmBlazor.Localizer["Sys.Edit"], x => x.id == item.ID.ToString()) == DialogResult.Yes)
            {
                await dataTable.QueryAsync();
            }

        }
        private async Task<bool> OnEditDoubleClick(CameraRepair_View item, ItemChangedType changedType)
        {
            CameraRepairVM vM = new CameraRepairVM();
            if (item.RepairState == RepairState.Treated)
            {
                item.RepairedTime = DateTime.Now;
                item.Accendant = UserInfo.Name;
            }
            if (item.AnomalyIdentification == AnomalyIdentification.Confirmed)
            {
                item.Supervisor = UserInfo.Name;
                
            }
            vM.Entity = item;

            await PostsData(vM, $"/api/CameraRepair/edit2", method: HttpMethodEnum.PUT);

            return true;
        }
        private async Task OnDetailsClick(CameraRepair_View item)
        {
            await OpenDialog<Details>(WtmBlazor.Localizer["Sys.Details"], x => x.id == item.ID.ToString());
        }

        private async Task OnBatchDeleteClick()
        {
            if (dataTable.SelectedRows?.Any() == true)
            {
                await PostsData(dataTable.SelectedRows.Select(x => x.ID).ToList(), $"/api/CameraRepair/batchdelete", (s) => WtmBlazor.Localizer["Sys.BatchDeleteSuccess", s]);
                await dataTable.QueryAsync();
            }
            else
            {
                await WtmBlazor.Toast.Information(WtmBlazor.Localizer["Sys.Info"], WtmBlazor.Localizer["Sys.SelectOneRowMin"]);
            }
        }

        private async Task OnDeleteClick(CameraRepair_View item)
        {
            await PostsData(new List<string> { item.ID.ToString() }, $"/api/CameraRepair/batchdelete", (s) => "Sys.OprationSuccess");
            await dataTable.QueryAsync();
        }

        private async Task OnExportClick(IEnumerable<CameraRepair_View> items)
        {
            if (dataTable.SelectedRows?.Any() == true)
            {
                await Download("/api/CameraRepair/ExportExcelByIds", dataTable.SelectedRows.Select(x => x.ID.ToString()).ToList());
            }
            else
            {
                await Download("/api/CameraRepair/ExportExcel", SearchModel);
            }
        }
        private async Task OnImportClick(IEnumerable<CameraRepair_View> items)
        {
            if (await OpenDialog<Import>(WtmBlazor.Localizer["Sys.Import"]) == DialogResult.Yes)
            {
                await dataTable.QueryAsync();
            }
        }

        public async Task CameraPictrue(string cameraId)
        {
          

            var data = await WtmBlazor.Api.CallAPI($"/api/DVRInfo/GetChannelPicture?Camera_ID={cameraId}");

            
            if (data.Data != "NO" && data.Data != null)
            {

                await OpenDialog<CameraPicture>("视频监控", x => x.Camera_ID == data.Data, Size.ExtraLarge);
            }
            else
            {
                await OpenDialog<CameraPicture>("视频监控", x => x.Camera_ID == $"获取图片失败{data.Data}{data.ErrorMsg}{data.StatusCode}", Size.ExtraLarge);
            }
        }
    }
}
