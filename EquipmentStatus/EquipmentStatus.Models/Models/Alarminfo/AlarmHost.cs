namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AlarmHost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlarmHost()
        {
            Alarms = new HashSet<Alarm>();
        }

        public Guid ID { get; set; }

        public Guid monitorRoomId { get; set; }

        [StringLength(55)]
        public string AlarmHost_ID { get; set; }

        [StringLength(55)]
        public string User { get; set; }

        [StringLength(55)]
        public string Password { get; set; }

        [StringLength(55)]
        public string AlarmHostType { get; set; }

        [StringLength(55)]
        public string AlarmHostIP { get; set; }

        public int? AlarmChannelCount { get; set; }

        [StringLength(55)]
        public string install_time { get; set; }

        [StringLength(55)]
        public string category { get; set; }

        [StringLength(55)]
        public string Remark { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alarm> Alarms { get; set; }

        public virtual MonitorRooms MonitorRooms { get; set; }
    }
}
