
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnMonitor.Models
{    
       public class DVRcheckinfoModel
    {


        /// <summary>
        /// 主机号
        /// </summary>
        [Display(Name = "主机号")]
        public string DVR_ID { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [Display(Name ="DVR显示SN")]
        public string DVR_SN { get; set; }
        /// <summary>
        /// 数据库序列号
        /// </summary>
        [Display(Name ="数据库SN")]
        public string DataDVR_SN { get; set; }
        /// <summary>
        /// DVR类型
        /// </summary>
        [Display(Name = "DVR类型")]
        public string DVR_type { get; set; }
        /// <summary>
        /// DVR 通道数量
        /// </summary>
        [Display(Name = "DVR通道数量")]
        public int? DVR_ChannelTotal { get; set; }
        /// <summary>
        /// 数据库通道数量
        /// </summary>
        [Display(Name = "数据库通道数")]
        public int? DataChannelTotal { get; set; }
        /// <summary>
        /// 硬盘总量
        /// </summary>
        [Display(Name = "DVR硬盘容量")]
        public int? DiskTotal { get; set; }
        /// <summary>
        /// 数据库硬盘总量
        /// </summary>
        [Display(Name = "数据库硬盘容量")]
        public int? DataDiskTotal { get; set; }
        /// <summary>
        /// 硬盘信息
        /// </summary>
        [Display(Name = "DVR硬盘信息")]
        public List<DVRDisk> DVRDISK { get; set; }
        /// <summary>
        /// DVR通道位置信息?josn类型
        /// </summary>
        [Display(Name = "通道检查")]
        public List<DVRChannelInfoModel> DVRChannelInfo { get; set; }
        /// <summary>
        /// dvr时间
        /// </summary>
        [Display(Name = "DVR时间")]
        public string DVRTime { get; set; }
        /// <summary>
        ///系统时间
        /// </summary>
        [Display(Name = "系统时间")]
        public string SystemTime { get; set; }

        /// <summary>
        /// 主机在线状态
        /// </summary>
        [Display(Name = "在线状态")]
        public bool? DVR_Online { get; set; }
        /// <summary>
        /// 时间信息检查
        /// </summary>
        [Display(Name = "时间检查")]
        public bool? TimeInfoChenk { get; set; }
        /// <summary>
        /// 硬盘信息检查
        /// </summary>
        [Display(Name = "硬盘检查")]
        public bool? DiskChenk { get; set; }

        /// <summary>
        /// SN信息检查
        /// </summary>
        [Display(Name = "SN检查")]
        public bool? SNChenk { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
        }
        /// <summary>
        /// 镜头信息比对
        /// </summary>
        public class DVRChannelInfoModel
        {
            [Display(Name = "通道号")]
            public int Channelid { get; set; }
            
            [Display(Name = "数据库名称")]
            public string DataChannelName { get; set; }
            [Display(Name ="DVR显示名称")]
            public string DVRChannelName { get; set; }
            [Display(Name ="检查结果")]
           
            public bool? ChannelNameCheck { get; set; }
            [Display(Name ="图形检测")]
            public bool? ImageCheck { get; set; }

        }
       
}
