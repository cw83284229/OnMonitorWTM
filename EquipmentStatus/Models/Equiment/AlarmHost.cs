using System;
using System.Collections.Generic;

namespace EFmodel
{
    public partial class AlarmHost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlarmHost()
        {
            Alarms = new HashSet<Alarm>();
        }

        public Guid ID { get; set; }

        public Guid monitorRoomId { get; set; }

       
        public string AlarmHost_ID { get; set; }

     
        public string User { get; set; }

      
        public string Password { get; set; }

     
        public string AlarmHostType { get; set; }

     
        public string AlarmHostIP { get; set; }

        public int? AlarmChannelCount { get; set; }

      
        public string install_time { get; set; }

      
        public string category { get; set; }

       
        public string Remark { get; set; }

        
        public DateTime? CreateTime { get; set; }

        
        public string CreateBy { get; set; }

      
        public DateTime? UpdateTime { get; set; }

      
        public string UpdateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alarm> Alarms { get; set; }

        public virtual MonitorRooms MonitorRoom { get; set; }
    }
}
