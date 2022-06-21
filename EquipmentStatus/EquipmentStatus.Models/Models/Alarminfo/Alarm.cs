namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alarm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alarm()
        {
            AlarmManageStates = new HashSet<AlarmManage>();
        }

        public Guid ID { get; set; }

        public Guid AlarmHostID { get; set; }

        [StringLength(55)]
        public string Alarm_ID { get; set; }

        public int? Channel_ID { get; set; }

        [StringLength(55)]
        public string Build { get; set; }

        [StringLength(55)]
        public string floor { get; set; }

        [StringLength(55)]
        public string Location { get; set; }

        [StringLength(55)]
        public string GeteType { get; set; }

        [StringLength(55)]
        public string SensorType { get; set; }
        [StringLength(50)]
        public string department { get; set; }

        [StringLength(50)]
        public string Cost_code { get; set; }

        [StringLength(55)]
        public string install_time { get; set; }

        [StringLength(55)]
        public string category { get; set; }

        [StringLength(55)]
        public string InsideCamera_ID { get; set; }

        [StringLength(55)]
        public string OutsideCamera_ID { get; set; }

        public bool IsAlertor { get; set; }

        public bool? IsOpenOrClosed { get; set; }

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

        public virtual AlarmHost AlarmHost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmManage> AlarmManageStates { get; set; }
    }
}
