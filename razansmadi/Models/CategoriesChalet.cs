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
    
    public partial class CategoriesChalet
    {
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> ChaletID { get; set; }
        public string note { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Chalet Chalet { get; set; }
    }
}