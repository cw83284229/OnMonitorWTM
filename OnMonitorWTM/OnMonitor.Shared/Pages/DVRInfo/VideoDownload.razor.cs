using BootstrapBlazor.Components;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WtmBlazorUtils;

namespace OnMonitor.Shared.Pages.DVRInfo
{
    public partial  class VideoDownload
    {
        private Table<VideoInfo> dataTable { get; set; } = new Table<VideoInfo>();

        /// <summary>
        /// 获得 弹窗注入服务
        /// </summary>
        [Inject]
        private DialogService? DialogService { get; set; }
        private List<VideoInfo> listvideoInfo { get; set; } = new  List<VideoInfo>();
        [Display(Name = "镜头编号")]
        public string CameraId;
        [Display(Name = "账号")]
        public string urse;
        [Display(Name = "密码")]
        public string pwd;
        public DateTime datetimeStart { get; set; } = DateTime.Now;
        public DateTime datetimeEnd { get; set; }=DateTime.Now;
        private int progressLong { get; set; }
        
        private string VideoId { get; set; }
        private List<SelectedItem> AllCameras = new List<SelectedItem>();
        protected override async Task OnInitializedAsync()
        {
            AllCameras = await WtmBlazor.Api.CallItemsApi("/api/CameraRepair/GetCameras", placeholder: WtmBlazor.Localizer["Sys.PleaseSelect"]);
            var dvrinfo = await WtmBlazor.Api.CallAPI<Dictionary<string, long>>($"/api/DVRInfo/GetVideoFileName");
            int i = 1;
            foreach (var item in dvrinfo.Data)
            {
                VideoInfo videoInfo = new VideoInfo();
                videoInfo.id = i++;
                videoInfo.name = item.Key;
                videoInfo.Size = item.Value;
                listvideoInfo.Add(videoInfo);
            }
            dataTable.Items = listvideoInfo;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 获取视频文件
        /// </summary>
        /// <param name="dataChannelName"></param>
        /// <returns></returns>
      
        private async Task GeneratingVideoFile()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(nameof(DVRVerification.urse), urse);
            dic.Add(nameof(DVRVerification.urseChanged), EventCallback.Factory.Create<string>(this, v => urse = v));
            dic.Add(nameof(DVRVerification.pwd), pwd);
            dic.Add(nameof(DVRVerification.pwdChanged), EventCallback.Factory.Create<string>(this, v => pwd = v));

            var result = await DialogService.ShowModal<DVRVerification>(new ResultDialogOption()
            {
                Title = "iDPBG视频监控系统",
                ComponentParamters = dic
              
            });

            CameraId = AllCameras.Where(u => u.Active == true).LastOrDefault().Text;


            var videodata = await WtmBlazor.Api.CallAPI<Dictionary<string, string>>($"/api/DVRInfo/BackupsVideoByTime?Camera_ID={CameraId}&&startTime={datetimeStart.ToString("yyyy-MM-dd HH:mm:ss")}&&endTime={datetimeEnd.ToString("yyyy-MM-dd HH:mm:ss")}&&username={urse}&&password={pwd}");
            if (videodata.StatusCode==System.Net.HttpStatusCode.OK)
            {
                VideoId = videodata.Data.Where(u => u.Key == "m_DownloadID").FirstOrDefault().Value;

                if (VideoId != null)
                {
                    while (true)
                    {
                        var intdata = await WtmBlazor.Api.CallAPI<Dictionary<string, int>>($"/api/DVRInfo/DownloadVideoFilePlan?DownloadID={VideoId}");
                        if (intdata.StatusCode==System.Net.HttpStatusCode.OK)
                        {
                            if (intdata.Data == null)
                            {
                                await WtmBlazor.Toast.Success("视频下载成功");
                                await base.OnInitializedAsync();
                                break;
                            }
                            long nDownLoadSize = 0;
                            long nTotalSize = 0;
                            foreach (var item in intdata.Data)
                            {
                                if (item.Key == "nDownLoadSize")
                                {
                                    nDownLoadSize = item.Value;
                                }
                                if (item.Key == "nTotalSize")
                                {
                                    nTotalSize = item.Value;
                                }
                            }
                            progressLong = (int)(nDownLoadSize * 100/ nTotalSize);
                        }
                      



                        StateHasChanged();
                        await Task.Delay(1000);
                    }

                }
            }
            else
            {
                await WtmBlazor.Toast.Error("下载失败");
            }
          
        }

        /// <summary>
        /// 删除视频文件
        /// </summary>
        /// <returns></returns>
        private async Task DeleteVideoFile(VideoInfo videoInfo)
        {

            var result = await WtmBlazor.Api.CallAPI<string>($"/api/DVRInfo/DeleteVideoFile?fileName={videoInfo.name}",HttpMethodEnum.POST,videoInfo.name);

           var deleteResult= JsonSerializer.Deserialize<string>(result.Data);
            if (deleteResult == "删除成功")
            {

               await WtmBlazor.Toast.Success("删除成功");
                listvideoInfo.Remove(videoInfo);
                StateHasChanged();
            }
            else
            {
                await WtmBlazor.Toast.Success("删除失败");
            }//await PostsData(dataChannelName, $"/api/DVRInfo/SetChannelName", (s) => "Sys.OprationSuccess", method: HttpMethodEnum.POST);
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <returns></returns>
        private async Task DownloadVideoFile(VideoInfo videoInfo)
        {

            var dvrinfo = await WtmBlazor.Api.CallAPI<string>($"/api/DVRInfo/DownloadVideoByFileName?fileName={videoInfo.name}");
              
            base.Redirect(dvrinfo.Data);
        }

      
      
        
        }

 




    public class VideoInfo
    { 
      [Display(Name ="序号")]
      public int id { get; set; }
      [Display(Name = "文件名称")]
      public string name { get; set; }
       [Display(Name = "视频大小")]
      public long Size { get; set; }
    }


}