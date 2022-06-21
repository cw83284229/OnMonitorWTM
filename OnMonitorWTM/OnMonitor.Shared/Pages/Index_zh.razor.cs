using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using OnMonitor.Monitor;

namespace OnMonitor.Shared.Pages
{
    public partial class Index_zh
    {

        private List<ReportFormsDto> listForms;

        private Chart LineChart { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //var listFormsData = await WtmBlazor.Api.CallAPI<List<ReportFormsDto>>($"/api/ReportForms/GetReportFormsByMonitorRoom");

            //listForms = listFormsData.Data;


            await base.OnInitializedAsync();
        }



        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                // Logger.Log("Bar 正在加载数据 ...");
            }
        }

        private Task OnAfterInit()
        {
            // Logger.Log("Bar 初始化完毕");
            return Task.CompletedTask;
        }

        //   private Task OnAfterUpdate(ChartAction action) => InvokeAsync(() => Logger.Log($"Bar 图更新数据操作完毕 -- {action}"));

        private async Task<ChartDataSource> OnInitAsync(bool stacked)
        {
            var listFormsData = await WtmBlazor.Api.CallAPI<List<ReportFormsDto>>($"/api/ReportForms/GetReportFormsByMonitorRoom");

            listForms = listFormsData.Data;

            var ds = new ChartDataSource();
            ds.Options.Title = "iDPBG监控信息图";
            ds.Options.X.Title = "监控室";
            ds.Options.X.Stacked = true;
            ds.Options.Y.Stacked = false;

            ds.Labels = listForms.Select(u => u.DVRRoom);


            ds.Data.Add(new ChartDataset()
            {
                Label = "镜头总数",
                Data = listForms.Select(u => u.CameraTotal).Cast<object>()
            });
            ds.Data.Add(new ChartDataset()
            {
                Fill = true,
                Label = "DVR总数",
                Data = listForms.Select(u => u.DVRTotal).Cast<object>()
            });

           
            return await Task.FromResult(ds);
        }

        private async Task<ChartDataSource> OnInit(float tension, bool hasNull)
        {
            var listFormsData = await WtmBlazor.Api.CallAPI<List<ReportFormsDto>>($"/api/ReportForms/GetReportFormsByMonitorRoom");

            listForms = listFormsData.Data;

            var ds = new ChartDataSource();
            ds.Options.Title = "异常曲线图";
            ds.Options.X.Title = "监控室";
            //ds.Options.Y.Title = "笔录";
            ds.Labels = listForms.Select(u => u.DVRRoom);
            ds.Data.Add(new ChartDataset()
            {
                Label = "异常数量",
                // Fill=true,
                Data = listForms.Select(u => u.CameraAnomaly).Cast<object>()
            });
            ds.Data.Add(new ChartDataset()
            {
                // Fill = true,
                Label = "维修数量",
                Data = listForms.Select(u => u.CameraAnomalyRepair).Cast<object>()
            });
          
            return await Task.FromResult(ds);
        }

        private CancellationTokenSource _chartCancellationTokenSource = new();

    


     
    }



}
