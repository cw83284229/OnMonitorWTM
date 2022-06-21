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


namespace OnMonitor.ViewModel.Project.ProjectChangeCameraVMs
{
    public partial class ProjectChangeCameraListVM : BasePagedListVM<ProjectChangeCamera_View, ProjectChangeCameraSearcher>
    {

        protected override IEnumerable<IGridColumn<ProjectChangeCamera_View>> InitGridHeader()
        {
            return new List<GridColumn<ProjectChangeCamera_View>>{
                this.MakeGridHeader(x => x.ProjectName),
                this.MakeGridHeader(x => x.Camera_ID_view),
                this.MakeGridHeader(x => x.ChangeLocation),
                this.MakeGridHeader(x => x.TransformationStatus),
                this.MakeGridHeader(x => x.IsDismantle),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ProjectChangeCamera_View> GetSearchQuery()
        {
            var query = DC.Set<ProjectChangeCamera>()
                .CheckEqual(Searcher.ProjectManagesId, x=>x.ProjectManagesId)
                .CheckEqual(Searcher.CameraId, x=>x.CameraId)
                .CheckEqual(Searcher.TransformationStatus, x=>x.TransformationStatus)
                .CheckEqual(Searcher.IsDismantle, x=>x.IsDismantle)
                .Select(x => new ProjectChangeCamera_View
                {
				    ID = x.ID,
                    ProjectName = x.ProjectManages.ProjectName,
                    Camera_ID_view = x.Camera.Camera_ID,
                    ChangeLocation = x.ChangeLocation,
                    TransformationStatus = x.TransformationStatus,
                    IsDismantle = x.IsDismantle,
                    Remark = x.Remark,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ProjectChangeCamera_View : ProjectChangeCamera{
        [Display(Name = "工程名称")]
        public String ProjectName { get; set; }
        [Display(Name = "镜头号")]
        public String Camera_ID_view { get; set; }

    }
}
