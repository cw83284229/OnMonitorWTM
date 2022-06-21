using System;
using System.Collections.Generic;


namespace EFmodel
{
    

    public partial class DVRInfoChecks
    {
        public Guid ID { get; set; }

        public Guid DVRId { get; set; }

     
        public string DVR_SN { get; set; }

        public int? DVR_Channel { get; set; }

        public int? DiskTotal { get; set; }

        public string DVRDISK { get; set; }

        public string DVRChannelInfo { get; set; }

        public CheckState DVR_Online { get; set; }

        public CheckState TimeInfoChenk { get; set; }

        public CheckState DiskChenk { get; set; }

        public CheckState SNChenk { get; set; }

        public CheckState VideoCheck90Day { get; set; }
        public int VideoStarageTime { get; set; }
      
        public string Remark { get; set; }

        public virtual DVRs DVRs { get; set; }

        public DateTime? CreateTime { get; set; }

        
        public string CreateBy { get; set; }

      
        public DateTime? UpdateTime { get; set; }

      
        public string UpdateBy { get; set; }
    }



    public enum CheckState
    {
        /// <summary>
        /// 未检查
        /// </summary>
       
        Inactive,
        /// <summary>
        /// 正常
        /// </summary>
      
        Normal,
        /// <summary>
        /// 异常
        /// </summary>
      
        Anomaly,


    }
}
