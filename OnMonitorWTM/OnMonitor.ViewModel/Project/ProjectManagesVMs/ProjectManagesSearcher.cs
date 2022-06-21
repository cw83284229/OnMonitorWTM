using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.ProjectManagesVMs
{
    public partial class ProjectManagesSearcher : BaseSearcher
    {
        [Display(Name = "工程名称")]
        public String ProjectName { get; set; }
        [Display(Name = "工程单号")]
        public String ProjectOrder { get; set; }
        [Display(Name = "开始时间")]
        public String StartWorkDate { get; set; }
        [Display(Name = "完工时间")]
        public String CompleteDate { get; set; }
        [Display(Name = "施工厂商")]
        public String ManufacturerName { get; set; }
        [Display(Name = "楼栋")]
        public String Build { get; set; }
        [Display(Name = "楼层")]
        public String Floor { get; set; }

        protected override void InitVM()
        {
        }

    }
}
