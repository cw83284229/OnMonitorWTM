using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.CameraVMs
{
    public partial class CameraSearcher : BaseSearcher
    {
        [Display(Name = "监控室")]
        public Guid? monitorRoomId { get; set; }
        [Display(Name = "主机号")]
        public Guid? DVRId { get; set; }
        [Display(Name = "镜头号")]
        public String Camera_ID { get; set; }
        [Display(Name = "楼栋")]
        public String Build { get; set; }
        [Display(Name = "楼层")]
        public String floor { get; set; }
        [Display(Name = "方向")]
        public String Direction { get; set; }
        [Display(Name = "位置")]
        public String Location { get; set; }
        [Display(Name = "归属部门")]
        public Guid? DepartmentId { get; set; }
        [Display(Name = "设备厂商")]
        public String manufacturer { get; set; }

        protected override void InitVM()
        {
        }

    }
}
