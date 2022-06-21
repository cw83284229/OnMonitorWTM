using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using OnMonitor.Model.Equipment;


namespace OnMonitor.ViewModel.Equipment.DepartmentVMs
{
    public partial class DepartmentListVM : BasePagedListVM<Department_View, DepartmentSearcher>
    {

        protected override IEnumerable<IGridColumn<Department_View>> InitGridHeader()
        {
            return new List<GridColumn<Department_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Cost_code),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Department_View> GetSearchQuery()
        {
            var query = DC.Set<Department>()
                .Select(x => new Department_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Cost_code = x.Cost_code,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Department_View : Department{

    }
}
