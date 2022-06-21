using OnMonitor.Model.Repair;
using System.Collections.Generic;

namespace OnMonitor.Models
{
   
    public class AnalyzeModel
    {

        /// <summary>
        /// 报表类别
        /// </summary>
        public string FormsType { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string FormsMame { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public List<CategoryDetails> CategoryDetailsList { get; set; }

        public string Remark { get; set; }


    }
    public class CategoryDetails
    {
        /// <summary>
        /// 报表类别
        /// </summary>
        public string FormsType { get; set; }
        /// <summary>
        /// 报表横轴
        /// </summary>
        public string FormsRefer { get; set; }
        /// <summary>
        /// 主机总数
        /// </summary>
        public int DVRTotal { get; set; }
        /// <summary>
        /// 镜头总数
        /// </summary>
        public int CameraTotal { get; set; }
        /// <summary>
        /// 门磁总数
        /// </summary>
        public int AlarmTotal { get; set; }


        /// <summary>
        /// 异常镜头
        /// </summary>
        public int CameraAnomaly { get; set; }
        /// <summary>
        /// 异常主机
        /// </summary>
        public int DVRAnomaly { get; set; }
        /// <summary>
        /// 异常门磁
        /// </summary>
        public int AlarmAnomaly { get; set; }

        /// <summary>
        /// 重复维修
        /// </summary>
        public int RepeatRepair { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public float Proportion { get; set; }



    }

}
