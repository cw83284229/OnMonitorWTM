using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using OnMonitor.Model.Repair;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Repair.DVRInfoCheckVMs
{
    public partial class DVRInfoCheckTemplateVM : BaseTemplateVM
    {
        public ExcelPropety DVR_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVRId);
        [Display(Name = "序列号")]
        public ExcelPropety DVR_SN_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVR_SN);
        [Display(Name = "通道数量")]
        public ExcelPropety DVR_Channel_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVR_Channel);
        [Display(Name = "硬盘总量")]
        public ExcelPropety DiskTotal_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DiskTotal);
        [Display(Name = "硬盘信息")]
        public ExcelPropety DVRDISK_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVRDISK);
        [Display(Name = "通道信息")]
        public ExcelPropety DVRChannelInfo_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVRChannelInfo);
        [Display(Name = "在线状态")]
        public ExcelPropety DVR_Online_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DVR_Online);
        [Display(Name = "时间检查")]
        public ExcelPropety TimeInfoChenk_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.TimeInfoChenk);
        [Display(Name = "硬盘检查")]
        public ExcelPropety DiskChenk_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.DiskChenk);
        [Display(Name = "SN检查")]
        public ExcelPropety SNChenk_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.SNChenk);
        [Display(Name = "90天检查")]
        public ExcelPropety VideoCheck90Day_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.VideoCheck90Day);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<DVRInfoCheck>(x => x.Remark);

	    protected override void InitVM()
        {
            DVR_Excel.DataType = ColumnDataType.ComboBox;
            DVR_Excel.ListItems = DC.Set<DVR>().GetSelectListItems(Wtm, y => y.DVR_ID);
        }

    }

    public class DVRInfoCheckImportVM : BaseImportVM<DVRInfoCheckTemplateVM, DVRInfoCheck>
    {

    }

}
