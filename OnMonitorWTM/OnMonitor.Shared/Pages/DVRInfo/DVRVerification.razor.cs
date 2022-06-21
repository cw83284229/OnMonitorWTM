using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnMonitor.Shared.Pages.DVRInfo
{
  public partial  class DVRVerification : ComponentBase, IResultDialog
    {

      [Parameter]
        public string urse { get; set; }
        [Parameter]
        public EventCallback<string> urseChanged { get; set; }
        [Parameter]
        public string pwd { get; set; }
        [Parameter]
        public EventCallback<string> pwdChanged { get; set; }

        public async Task OnClose(DialogResult result)
        {
            if (result == DialogResult.Yes)
            {
                if (urseChanged.HasDelegate)
                {
                    await urseChanged.InvokeAsync(urse);
                }
                if (pwdChanged.HasDelegate)
                {
                    await pwdChanged.InvokeAsync(pwd);
                }
            }
        }
    }



}

