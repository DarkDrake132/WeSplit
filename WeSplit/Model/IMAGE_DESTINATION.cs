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
    
    public partial class IMAGE_DESTINATION
    {
        public int idJourney { get; set; }
        public int id { get; set; }
        public string imageLink { get; set; }
    
        public virtual JOURNEY JOURNEY { get; set; }
    }
}
