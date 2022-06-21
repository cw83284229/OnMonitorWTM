using OnMonitor.Model.AlarmManages;
using System;
using System.Collections.Generic;


namespace EFmodel
{
    public partial class Alarm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alarm()
        {
            AlarmManageStates = new HashSet<AlarmManage>();
        }

        public Guid ID { get; set; }

        public Guid AlarmHostID { get; set; }

       
        public string Alarm_ID { get; set; }

        public int? Channel_ID { get; set; }

     
        public string Build { get; set; }

      
        public string floor { get; set; }

       
        public string Location { get; set; }

       
        public string GeteType { get; set; }

       
        public string SensorType { get; set; }
       
        public string department { get; set; }

      
        public string Cost_code { get; set; }

       
        public string install_time { get; set; }

       
        public string category { get; set; }

        
        public string InsideCamera_ID { get; set; }

       
        public string OutsideCamera_ID { get; set; }

        public bool IsAlertor { get; set; }

        public bool? IsOpenOrClosed { get; set; }

        
        public string Remark { get; set; }

      
        public DateTime? CreateTime { get; set; }

    
        public string CreateBy { get; set; }

     
        public DateTime? UpdateTime { get; set; }

    
        public string UpdateBy { get; set; }

        public virtual AlarmHost AlarmHost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlarmManage> AlarmManageStates { get; set; }
    }
}
