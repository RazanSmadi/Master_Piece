//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace razansmadi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class transaction
    {
        public int transactionId { get; set; }
        public string userid { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> totalpay { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
