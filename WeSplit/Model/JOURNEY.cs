//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeSplit.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class JOURNEY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JOURNEY()
        {
            this.IMAGE_DESTINATION = new HashSet<IMAGE_DESTINATION>();
            this.MEMBERs = new HashSet<MEMBER>();
        }
    
        public int id { get; set; }
        public string C_location { get; set; }
        public string title { get; set; }
        public Nullable<int> isFinish { get; set; }
        public string thumbnailLink { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMAGE_DESTINATION> IMAGE_DESTINATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBERs { get; set; }
    }
}
