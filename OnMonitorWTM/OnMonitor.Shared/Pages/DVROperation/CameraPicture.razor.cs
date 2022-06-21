using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnMonitor.Shared.Pages.DVROperation
{
    public partial class CameraPicture
    {

        [Parameter]
        public string Camera_ID { get; set; }

        public string picturepath { get; set; }

        protected override async Task OnInitializedAsync()
        {
           

            picturepath =WtmBlazor.Api.Client.BaseAddress+"/images/Picture/"+Camera_ID;

            await base.OnInitializedAsync();
        }
    }
}
