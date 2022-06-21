using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Project;


namespace OnMonitor.ViewModel.Project.ProjectManagesVMs
{
    public partial class ProjectManagesListVM : BasePagedListVM<ProjectManages_View, ProjectManagesSearcher>
    {

        protected override IEnumerable<IGridColumn<ProjectManages_View>> InitGridHeader()
        {
            return new List<GridColumn<ProjectManages_View>>{
                this.MakeGridHeader(x => x.ProjectManageType),
                this.MakeGridHeader(x => x.ProjectName),
                this.MakeGridHeader(x => x.ProjectOrder),
                this.MakeGridHeader(x => x.StartWorkDate),
                this.MakeGridHeader(x => x.CompleteDate),
                this.MakeGridHeader(x => x.AcceptanceData),
                this.MakeGridHeader(x => x.ManufacturerName),
                this.MakeGridHeader(x => x.ProjectSpecifications),
                this.MakeGridHeader(x => x.Build),
                this.MakeGridHeader(x => x.Floor),
                this.MakeGridHeader(x => x.ExcelDataId).SetFormat(ExcelDataIdFormat),
                this.MakeGridHeader(x => x.LayoutInfoId).SetFormat(LayoutInfoIdFormat),
                this.MakeGridHeader(x => x.PowerInfo),
                this.MakeGridHeader(x => x.AlarmInfo),
                this.MakeGridHeader(x => x.AcceptanceResult),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> ExcelDataIdFormat(ProjectManages_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.ExcelDataId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.ExcelDataId,640,480),
            };
        }

        private List<ColumnFormatInfo> LayoutInfoIdFormat(ProjectManages_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.LayoutInfoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.LayoutInfoId,640,480),
            };
        }


        public override IOrderedQueryable<ProjectManages_View> GetSearchQuery()
        {
            var query = DC.Set<ProjectManages>()
                .CheckContain(Searcher.ProjectName, x=>x.ProjectName)
                .CheckContain(Searcher.ProjectOrder, x=>x.ProjectOrder)
                .CheckContain(Searcher.StartWorkDate, x=>x.StartWorkDate)
                .CheckContain(Searcher.CompleteDate, x=>x.CompleteDate)
                .CheckContain(Searcher.ManufacturerName, x=>x.ManufacturerName)
                .CheckContain(Searcher.Build, x=>x.Build)
                .CheckContain(Searcher.Floor, x=>x.Floor)
                .Select(x => new ProjectManages_View
                {
				    ID = x.ID,
                    ProjectManageType = x.ProjectManageType,
                    ProjectName = x.ProjectName,
                    ProjectOrder = x.ProjectOrder,
                    StartWorkDate = x.StartWorkDate,
                    CompleteDate = x.CompleteDate,
                    AcceptanceData = x.AcceptanceData,
                    ManufacturerName = x.ManufacturerName,
                    ProjectSpecifications = x.ProjectSpecifications,
                    Build = x.Build,
                    Floor = x.Floor,
                    ExcelDataId = x.ExcelDataId,
                    LayoutInfoId = x.LayoutInfoId,
                    PowerInfo = x.PowerInfo,
                    AlarmInfo = x.AlarmInfo,
                    AcceptanceResult = x.AcceptanceResult,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ProjectManages_View : ProjectManages{

    }
}
