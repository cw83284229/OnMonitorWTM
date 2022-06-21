using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnMonitor.Shared.Pages.DVROperation;
using OnMonitor.ViewModel.Repair.CameraRepairVMs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OnMonitor.Shared.Pages.Repair.CameraRepair
{
    public partial class Create
    {
        private CameraRepairVM Model = new CameraRepairVM();
        private ValidateForm vform { get; set; }
        [NotNull]
        private Button IsSubmitDisable;
        private List<SelectedItem> AllCameras = new List<SelectedItem>();
        private List<SelectedItem> AllRepairCameras = new List<SelectedItem>();
        private List<SelectedItem> AllProjectCameras = new List<SelectedItem>();
        private IEnumerable<string> CameraText = new List<string>();

        protected override async Task OnInitializedAsync()
        {

            AllCameras = await WtmBlazor.Api.CallItemsApi("/api/CameraRepair/GetCameras", placeholder: WtmBlazor.Localizer["Sys.PleaseSelect"]);
            AllRepairCameras = await WtmBlazor.Api.CallItemsApi("/api/CameraRepair/GetRepairCameras", placeholder: WtmBlazor.Localizer["Sys.PleaseSelect"]);
            AllProjectCameras = await WtmBlazor.Api.CallItemsApi("/api/ProjectChangeCamera/GetProjectChangeCameras", placeholder: WtmBlazor.Localizer["Sys.PleaseSelect"]);
            Model.Entity.AnomalyTime = DateTime.Now;
            Model.Entity.CollectTime = DateTime.Now;
            Model.Entity.Registrar = UserInfo.Name;

            CameraText = from a in AllCameras select new { camera = a.Text }.camera;

            var RepairText = from a in AllRepairCameras select new { camera = a.Text }.camera;
            var ProjectText = from a in AllProjectCameras select new { camera = a.Text }.camera;
            CameraText = CameraText.Except(RepairText);
            CameraText = CameraText.Except(ProjectText);
            await base.OnInitializedAsync();
        }

        private async Task Submit(EditContext context)
        {
            var guid = Guid.Parse(AllCameras.Where(u => u.Text == Model.Camera_ID).FirstOrDefault().Value);
            Model.Entity.CameraId = guid;
            await PostsForm(vform, "/api/CameraRepair/add", (s) => "Sys.OprationSuccess");
        }


        public void OnClose()
        {
            CloseDialog();
        }
        public async Task CameraPictrue(string cameraId)
        {

            // var DD = IsAccessable("/api/DVRInfo/GetChannelPicture/{Camera_ID}");

            var data = await WtmBlazor.Api.CallAPI($"/api/DVRInfo/GetChannelPicture?Camera_ID={cameraId}");


            if (data.Data != "NO" && data.Data != null)
            {

                await OpenDialog<CameraPicture>($"编号：{cameraId}", x => x.Camera_ID == data.Data, Size.ExtraLarge);
            }
            else
            {
                await OpenDialog<CameraPicture>($"编号：{cameraId}", x => x.Camera_ID == null, Size.ExtraLarge);
            }
        }

        private Task OnValueChanged(string val)
        {
            var DD = CameraText.Where(u => u.Equals(val));
            if (DD.Count() == 0)
            {
                vform.SetError<CameraRepairVM>(u => u.Camera_ID, "无法找到资源标号");
                IsSubmitDisable.SetDisable(true);
            }
            else
            {
                IsSubmitDisable.SetDisable(false);
            }

            return Task.CompletedTask;
        }
    }
}
