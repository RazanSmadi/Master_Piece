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
    
    public partial class Feature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feature()
        {
            this.Chalets = new HashSet<Chalet>();
        }
    
        public int Features_ID { get; set; }
        public Nullable<bool> Air_conditioning { get; set; }
        public Nullable<bool> Laundry { get; set; }
        public Nullable<bool> Refrigerator { get; set; }
        public Nullable<bool> Washer { get; set; }
        public Nullable<bool> Barbeque { get; set; }
        public Nullable<bool> Sauna { get; set; }
        public Nullable<bool> Wifi { get; set; }
        public Nullable<bool> indoorSwimmingPool { get; set; }
        public Nullable<bool> Microwave { get; set; }
        public Nullable<bool> SwimmingPool { get; set; }
        public Nullable<bool> YouthGames { get; set; }
        public Nullable<bool> KidsGames { get; set; }
        public Nullable<bool> OutdoorShower { get; set; }
        public Nullable<bool> Tv_Cable { get; set; }
        public Nullable<int> Carpark { get; set; }
        public Nullable<bool> Features1 { get; set; }
        public Nullable<bool> Features2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chalet> Chalets { get; set; }
    }
}
