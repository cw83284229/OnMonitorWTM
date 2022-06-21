using System;
using System.Collections.Generic;




namespace EFmodel
{
   

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

       
        public string DVR_ID { get; set; }

      
        public string Home_server { get; set; }

        public int? Hard_drive { get; set; }

     
        public string DVR_IP { get; set; }

      
        public string DVR_port { get; set; }

      
        public string DVR_usre { get; set; }

       
        public string DVR_possword { get; set; }

       
        public string install_time { get; set; }

       
        public string Manufacturer { get; set; }

       
        public string DVR_type { get; set; }

       
        public string DVR_SN { get; set; }

        public int? DVR_Channel { get; set; }

       
        public string department { get; set; }

      
        public string Cost_code { get; set; }

    
        public string Remark { get; set; }

       
        public DateTime? CreateTime { get; set; }

       
        public string CreateBy { get; set; }

        
        public DateTime? UpdateTime { get; set; }

       
        public string UpdateBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cmaeras> Cmaeras { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DVRInfoChecks> DVRInfoChecks { get; set; }

        public virtual MonitorRooms MonitorRoom { get; set; }
    }
}
