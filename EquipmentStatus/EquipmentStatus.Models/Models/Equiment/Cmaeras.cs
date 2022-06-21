namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cmaeras
    {
        public Guid ID { get; set; }

        public Guid DVRId { get; set; }

        public int channel_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Camera_ID { get; set; }

        [StringLength(50)]
        public string Build { get; set; }

        [StringLength(50)]
        public string floor { get; set; }

        [StringLength(50)]
        public string Direction { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public int MonitorClassification { get; set; }

        public int CameraTpye { get; set; }

        public Guid DepartmentId { get; set; }

        [StringLength(50)]
        public string install_time { get; set; }

        [StringLength(50)]
        public string manufacturer { get; set; }

        [StringLength(50)]
        public string category { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public virtual Departments Departments { get; set; }

        public virtual DVRs DVRs { get; set; }
    }
}
