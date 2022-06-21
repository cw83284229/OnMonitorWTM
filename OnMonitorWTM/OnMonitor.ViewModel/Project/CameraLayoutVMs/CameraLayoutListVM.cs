using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Project;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Project.CameraLayoutVMs
{
    public partial class CameraLayoutListVM : BasePagedListVM<CameraLayout_View, CameraLayoutSearcher>
    {

        protected override IEnumerable<IGridColumn<CameraLayout_View>> InitGridHeader()
        {
            return new List<GridColumn<CameraLayout_View>>{
                this.MakeGridHeader(x => x.RoomLocation_view),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.Floor),
                this.MakeGridHeader(x => x.ExcelDataId).SetFormat(ExcelDataIdFormat),
                this.MakeGridHeader(x => x.LayoutInfoId).SetFormat(LayoutInfoIdFormat),
                this.MakeGridHeader(x => x.LayoutInfoPDFId).SetFormat(LayoutInfoPDFIdFormat),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> ExcelDataIdFormat(CameraLayout_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.ExcelDataId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.ExcelDataId,640,480),
            };
        }

        private List<ColumnFormatInfo> LayoutInfoIdFormat(CameraLayout_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.LayoutInfoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.LayoutInfoId,640,480),
            };
        }

        private List<ColumnFormatInfo> LayoutInfoPDFIdFormat(CameraLayout_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.LayoutInfoPDFId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.LayoutInfoPDFId,640,480),
            };
        }


        public override IOrderedQueryable<CameraLayout_View> GetSearchQuery()
        {
            var query = DC.Set<CameraLayout>()
                .CheckEqual(Searcher.monitorRoomId, x=>x.monitorRoomId)
                .CheckContain(Searcher.Build, x=>x.Build)
                .CheckContain(Searcher.Floor, x=>x.Floor)
                .Select(x => new CameraLayout_View
                {
				    ID = x.ID,
                    RoomLocation_view = x.monitorRoom.RoomLocation,
                    Build = x.Build,
                    Floor = x.Floor,
                    ExcelDataId = x.ExcelDataId,
                    LayoutInfoId = x.LayoutInfoId,
                    LayoutInfoPDFId = x.LayoutInfoPDFId,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class CameraLayout_View : CameraLayout{
        [Display(Name = "监控室")]
        public String RoomLocation_view { get; set; }

    }
}
