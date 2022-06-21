using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVRApi.Models
{
    public class DVRDateTimeDto
    {
        /// <summary>
        /// DVR时间
        /// </summary>
        public string DVRTime { get; set; }
        /// <summary>
        /// 服务器时间
        /// </summary>
        public string ServerTime { get; set; }
        /// <summary>
        /// 时间比对结果
        /// </summary>

        public bool IsOk { get; set; }
    }
}
