using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSDK.Models
{
   public class DVRChannelInfo
    {
        public int Number { get; set; }
        public string ChannelName { get; set; }

        /// <summary>
        /// 视频压缩格式
        /// </summary>
        public string emCompression { set; get; }
        /// <summary>
        /// 视频分辨率
        /// </summary>	
        public string Resolution { set; get; }
        /// <summary>
        /// 视频宽度
        /// </summary>
        public int nWidth { set; get; }
        /// <summary>
        /// 视频高度
        /// </summary>
        public int nHeight { set; get; }
        /// <summary>
        /// 码流控制模式 可变码流
        /// </summary>
        public string emBitRateControl { set; get; }
        /// <summary>
        /// 视频最大限定码流kbps
        /// </summary>
        public int nBitRate { set; get; }
        /// <summary>
        /// 视频帧率
        /// </summary>
        public float nFrameRate { set; get; }
        /// <summary>
        /// I帧率限定
        /// </summary>
        public int nIFrameInterval { set; get; }
    }
}
