namespace EFmodel
{
    using System;
    using System.Collections.Generic;
   
    public partial class MonitorRooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonitorRooms()
        {
            DVRs = new HashSet<DVRs>();
            AlarmHosts = new HashSet<AlarmHost>();
        }

        public Guid ID { get; set; }

      
        public string Factory { get; set; }

     
        public string RoomLocation { get; set; }

      
        public string RoomType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DVRs> DVRs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmHost> AlarmHosts { get; set; }
    }
}
