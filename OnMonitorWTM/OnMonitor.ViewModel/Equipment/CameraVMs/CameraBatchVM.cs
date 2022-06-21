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
    public partial class CameraBatchVM : BaseBatchVM<Camera, Camera_BatchEdit>
    {
        public CameraBatchVM()
        {
            ListVM = new CameraListVM();
            LinkedVM = new Camera_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Camera_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
