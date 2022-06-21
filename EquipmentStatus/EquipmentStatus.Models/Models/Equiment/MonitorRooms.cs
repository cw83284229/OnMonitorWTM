namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MonitorRooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MonitorRooms()
        {
            DVRs = new HashSet<DVRs>();
            AlarmHosts = new HashSet<AlarmHost>();
        }

        public Guid ID { get; set; }

        [StringLength(50)]
        public string Factory { get; set; }

        [StringLength(50)]
        public string RoomLocation { get; set; }

        [StringLength(50)]
        public string RoomType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DVRs> DVRs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmHost> AlarmHosts { get; set; }
    }
}
