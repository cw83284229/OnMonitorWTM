namespace EFmodel
{
    using System;
    using System.Collections.Generic;
   

    public partial class Departments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departments()
        {
            Cmaeras = new HashSet<Cmaeras>();
        }

        public Guid ID { get; set; }

      
        public string Name { get; set; }

      
        public string Cost_code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cmaeras> Cmaeras { get; set; }
    }
}
