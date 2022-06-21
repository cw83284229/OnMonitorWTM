using OnMonitor.Model.AlarmManages;
using WalkingTec.Mvvm.Core;


namespace OnMonitor.ViewModel.AlarmManages.AlarmManageVMs
{
    public partial class AlarmManageBatchVM : BaseBatchVM<AlarmManage, AlarmManage_BatchEdit>
    {
        public AlarmManageBatchVM()
        {
            ListVM = new AlarmManageListVM();
            LinkedVM = new AlarmManage_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class AlarmManage_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
