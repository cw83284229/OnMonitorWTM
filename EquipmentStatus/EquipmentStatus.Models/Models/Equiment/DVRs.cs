namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DVRs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DVRs()
        {
            Cmaeras = new HashSet<Cmaeras>();
            DVRInfoChecks = new HashSet<DVRInfoChecks>();
        }

        public Guid ID { get; set; }

        public Guid monitorRoomId { get; set; }

        [Required]
        [StringLength(50)]
        public string DVR_ID { get; set; }

        [StringLength(50)]
        public string Home_server { get; set; }

        public int? Hard_drive { get; set; }

        [StringLength(50)]
        public string DVR_IP { get; set; }

        [StringLength(50)]
        public string DVR_port { get; set; }

        [StringLength(50)]
        public string DVR_usre { get; set; }

        [StringLength(50)]
        public string DVR_possword { get; set; }

        [StringLength(50)]
        public string install_time { get; set; }

        [StringLength(50)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string DVR_type { get; set; }

        [StringLength(50)]
        public string DVR_SN { get; set; }

        public int? DVR_Channel { get; set; }

        [StringLength(50)]
        public string department { get; set; }

        [StringLength(50)]
        public string Cost_code { get; set; }

        [StringLength(150)]
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
        public virtual ICollection<Cmaeras> Cmaeras { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DVRInfoChecks> DVRInfoChecks { get; set; }

        public virtual MonitorRooms MonitorRooms { get; set; }
    }
}
