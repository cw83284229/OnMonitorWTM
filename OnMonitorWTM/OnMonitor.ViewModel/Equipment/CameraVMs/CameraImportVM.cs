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
    public partial class CameraTemplateVM : BaseTemplateVM
    {
        [Display(Name = "主机号")]
        public ExcelPropety DVR_Excel = ExcelPropety.CreateProperty<Camera>(x => x.DVRId);
        [Display(Name = "通道号")]
        public ExcelPropety channel_ID_Excel = ExcelPropety.CreateProperty<Camera>(x => x.channel_ID);
        [Display(Name = "镜头号")]
        public ExcelPropety Camera_ID_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Camera_ID);
        [Display(Name = "楼栋")]
        public ExcelPropety Build_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Build);
        [Display(Name = "楼层")]
        public ExcelPropety floor_Excel = ExcelPropety.CreateProperty<Camera>(x => x.floor);
        [Display(Name = "方向")]
        public ExcelPropety Direction_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Direction);
        [Display(Name = "位置")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Location);
        [Display(Name = "监控类别")]
        public ExcelPropety MonitorClassification_Excel = ExcelPropety.CreateProperty<Camera>(x => x.MonitorClassification);
        [Display(Name = "设备类型")]
        public ExcelPropety CameraTpye_Excel = ExcelPropety.CreateProperty<Camera>(x => x.CameraTpye);
        public ExcelPropety Department_Excel = ExcelPropety.CreateProperty<Camera>(x => x.DepartmentId);
        [Display(Name = "安装时间")]
        public ExcelPropety install_time_Excel = ExcelPropety.CreateProperty<Camera>(x => x.install_time);
        [Display(Name = "设备厂商")]
        public ExcelPropety manufacturer_Excel = ExcelPropety.CreateProperty<Camera>(x => x.manufacturer);
        [Display(Name = "安装厂商")]
        public ExcelPropety category_Excel = ExcelPropety.CreateProperty<Camera>(x => x.category);
        [Display(Name = "IP")]
        public ExcelPropety Camera_IP_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Camera_IP);
        [Display(Name = "端口")]
        public ExcelPropety Camera_port_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Camera_port);
        [Display(Name = "账号")]
        public ExcelPropety Camera_usre_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Camera_usre);
        [Display(Name = "密码")]
        public ExcelPropety DVR_possword_Excel = ExcelPropety.CreateProperty<Camera>(x => x.DVR_possword);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Camera>(x => x.Remark);

	    protected override void InitVM()
        {
            DVR_Excel.DataType = ColumnDataType.ComboBox;
            DVR_Excel.ListItems = DC.Set<DVR>().GetSelectListItems(Wtm, y => y.DVR_ID);
            Department_Excel.DataType = ColumnDataType.ComboBox;
            Department_Excel.ListItems = DC.Set<Department>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class CameraImportVM : BaseImportVM<CameraTemplateVM, Camera>
    {
        public override DuplicatedInfo<Camera> SetDuplicatedCheck()
        {
            //筛选重复数据

            var rv = CreateFieldsInfo();
            rv.AddGroup(SimpleField(x => x.Camera_ID), SimpleField(x => x.channel_ID));

            return rv;

        }

    }

}
