using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using OnMonitor.Models;
using OnMonitor.Monitor;
using System.Collections.Generic;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Shared.Pages.DVROperation
{
    public partial class DVRInfo
    {
        private Table<DVRChannelInfoModel> dataTable { get; set; }
        private DVRcheckinfoModel DVRcheckinfo { get; set; }=new DVRcheckinfoModel();
        public List<DVRDisk> disk = new List<DVRDisk>();

        [Parameter]
        public string DVRId { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var dvrinfo = await WtmBlazor.Api.CallAPI<DVRcheckinfoModel>($"/api/DVRInfo/GetDVRInfoCheckByDVR_ID?DVR_ID={DVRId}");

            DVRcheckinfo = dvrinfo.Data;
            disk = DVRcheckinfo.DVRDISK;
            dataTable.Items = DVRcheckinfo.DVRChannelInfo;

          

            await base.OnInitializedAsync();
        }

        private async Task ChannelNameRectity(string  dataChannelName)
        {
            string cameraId = dataChannelName.Substring(0,5);
            await PostsData(dataChannelName, $"/api/DVRInfo/SetChannelName", (s) => "Sys.OprationSuccess", method: HttpMethodEnum.POST);
        }


        private async Task DVRChannelNameRectity(string DVRId)
        {
            foreach (var item in DVRcheckinfo.DVRChannelInfo)
            {
               string CameraId=item.DataChannelName.Split(" ")[0];
                IDictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Camera_ID", CameraId);
              var requst=  await WtmBlazor.Api.CallAPI($"/api/DVRInfo/SetChannelName", method: HttpMethodEnum.POST,dic );
                if (requst.Data.Contains("ok"))
                {
                    await WtmBlazor.Toast.Success(WtmBlazor.Localizer["Sys.Info"], $"{CameraId}修改成功");
                }
                else
                {
                    await WtmBlazor.Toast.Error(WtmBlazor.Localizer["Sys.Info"], $"{CameraId}修改失败");
                }
            }
           await base.OnInitializedAsync();
        }



    }
}
