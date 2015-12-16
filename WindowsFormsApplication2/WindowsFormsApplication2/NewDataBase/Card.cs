//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication2.NewDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        public int CardID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public string CardNo { get; set; }
        public Nullable<short> UserID { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public Nullable<System.DateTime> DateOfExpiry { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> CardStatus { get; set; }
        public Nullable<byte> ReasonID { get; set; }
        public Nullable<byte> CardTypeID { get; set; }
        public Nullable<bool> active { get; set; }
    
        public virtual CardType CardType { get; set; }
        public virtual Reason Reason { get; set; }
    }
}
