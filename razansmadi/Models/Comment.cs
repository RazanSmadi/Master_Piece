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
    
    public partial class Comment
    {
        public int Comment_ID { get; set; }
        public string userid { get; set; }
        public string comment1 { get; set; }
        public Nullable<System.DateTime> Comment_Date { get; set; }
        public Nullable<int> ChaletID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Chalet Chalet { get; set; }
    }
}
