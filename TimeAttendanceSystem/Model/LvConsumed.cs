//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeAttendanceSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LvConsumed
    {
        public string EmpLvType { get; set; }
        public int EmpID { get; set; }
        public string LeaveType { get; set; }
        public Nullable<short> CompanyID { get; set; }
        public Nullable<double> JanConsumed { get; set; }
        public Nullable<double> FebConsumed { get; set; }
        public Nullable<double> MarchConsumed { get; set; }
        public Nullable<double> AprConsumed { get; set; }
        public Nullable<double> MayConsumed { get; set; }
        public Nullable<double> JuneConsumed { get; set; }
        public Nullable<double> JulyConsumed { get; set; }
        public Nullable<double> AugustConsumed { get; set; }
        public Nullable<double> SepConsumed { get; set; }
        public Nullable<double> OctConsumed { get; set; }
        public Nullable<double> NovConsumed { get; set; }
        public Nullable<double> DecConsumed { get; set; }
        public Nullable<double> TotalForYear { get; set; }
        public Nullable<double> YearRemaining { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> GrandTotalRemaining { get; set; }
    
        public virtual LvType LvType { get; set; }
    }
}
