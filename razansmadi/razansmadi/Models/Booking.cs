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
    
    public partial class Booking
    {
        public int BookingID { get; set; }
        public Nullable<int> ChaletID { get; set; }
        public string id { get; set; }
        public Nullable<System.DateTime> CheckInDate { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public Nullable<int> CheckInTime { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<bool> PaymentStatus { get; set; }
        public Nullable<int> NumOfAdult { get; set; }
        public Nullable<int> NumOfKids { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Chalet Chalet { get; set; }
    }
}
