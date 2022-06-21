using OnMonitor.Model.Equipment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Repair
{
    public partial class DVRInfoCheck : BasePoco
    {

        /// <summary>
        /// 主机号
        /// </summary>
        [Display(Name ="主机号")]
        public Guid DVRId { get; set; }
        public DVR DVR { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [StringLength(55)]
        [Display(Name = "序列号")]
        public string DVR_SN { get; set; }

        /// <summary>
        /// DVR 通道数量
        /// </summary>
        [Display(Name = "通道数量")]
        public int? DVR_Channel { get; set; }
        /// <summary>
        /// 硬盘总量
        /// </summary>
        [Display(Name = "硬盘总量")]
        public int? DiskTotal { get; set; }
        /// <summary>
        /// 硬盘信息？josn类型
        /// </summary>
        [Display(Name = "硬盘信息")]
        public string DVRDISK { get; set; }
        /// <summary>
        /// DVR通道位置信息?josn类型
        /// </summary>
        [Display(Name = "通道信息")]
        public string DVRChannelInfo { get; set; }

        /// <summary>
        /// 主机在线状态
        /// </summary>
        [Display(Name = "在线状态")]
        public CheckState DVR_Online { get; set; }
        /// <summary>
        /// 时间信息检查
        /// </summary>
        [Display(Name = "时间检查")]
        public CheckState TimeInfoChenk { get; set; }

        /// <summary>
        /// 硬盘信息检查
        /// </summary>
        [Display(Name = "硬盘检查")]
        public CheckState DiskChenk { get; set; }

        /// <summary>
        /// SN信息检查
        /// </summary>
        [Display(Name = "SN检查")]
        public CheckState SNChenk { get; set; }

        /// <summary>
        /// 90天存储检查
        /// </summary>
        [Display(Name = "90天检查")]
        public CheckState VideoCheck90Day { get; set; }

        /// <summary>
        /// 90天存储检查
        /// </summary>
        [Display(Name = "存储时间")]
        public int VideoStarageTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(55)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

    }

    public enum CheckState
    {

        [Display(Name = "未检查")]
        Inactive,

        [Display(Name = "正常")]
        Normal,

        [Display(Name = "异常")]
        Anomaly,


    }


}
