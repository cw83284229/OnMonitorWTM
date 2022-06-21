using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
  public  class Department : TopBasePoco
    {

        [Display(Name = "部门名称")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Name { get; set; }

        [Display(Name = "费用代码")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Cost_code { get; set; }

    }
}
