using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSDK.Models
{
  public  class DVRInfoDto
    {
        /// <summary>
        /// 主机号
        /// </summary>
        public String DVR_IP { get; set; }
        /// <summary>
        /// 主机序列号
        /// </summary>
        public String DVR_SN { get; set; }
        /// <summary>
        /// 主机时间
        /// </summary>
        public String DVR_DateTine { get; set; }
        /// <summary>
        /// 90天存储检查
        /// </summary>
        public int Checnk90DaysVideo { get; set; }
        /// <summary>
        /// 硬盘容量
        /// </summary>
        public int HardTotal { get; set; }
        /// <summary>
        /// 硬盘数量
        /// </summary>
        public int HardCount { get; set; }
        /// <summary>
        /// 通道数量
        /// </summary>
        public int ChannelTotal { get; set; }

        /// <summary>
        /// 硬盘清单
        /// </summary>
        public List<DiskInfo> DiskInfos { get; set; }

        /// <summary>
        /// 通道清单
        /// </summary>
        public List<DVRChannelInfo> ChannelInfos { get; set; }
    

    }

    public class DVRDisk

    {
        public int Number { get; set; }
        public long Disk { get; set; }
    }
    public class Channelname

    {
        public int Number { get; set; }
        public string Name { get; set; }
    }
}

