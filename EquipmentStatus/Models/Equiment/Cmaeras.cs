using System;


namespace EFmodel
{
  
  
    public partial class Cmaeras
    {
        public Guid ID { get; set; }

        public Guid DVRId { get; set; }

        public int channel_ID { get; set; }

       
        public string Camera_ID { get; set; }

      
        public string Build { get; set; }

      
        public string floor { get; set; }

       
        public string Direction { get; set; }

     
        public string Location { get; set; }

        public int MonitorClassification { get; set; }

        public int CameraTpye { get; set; }

        public Guid DepartmentId { get; set; }

       
        public string install_time { get; set; }

       
        public string manufacturer { get; set; }

       
        public string category { get; set; }

      
        public string Remark { get; set; }

      
        public DateTime? CreateTime { get; set; }

        
        public string CreateBy { get; set; }

        
        public DateTime? UpdateTime { get; set; }

       
        public string UpdateBy { get; set; }

        public virtual Departments Departments { get; set; }

        public virtual DVRs DVRs { get; set; }
    }
}
