//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TASModel.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DailySumShift
    {
        public int ShiftDate { get; set; }
        public Nullable<byte> ShiftID { get; set; }
        public Nullable<System.DateTime> SummaryDate { get; set; }
        public Nullable<short> TotalEmp { get; set; }
        public Nullable<short> PresentEmp { get; set; }
        public Nullable<short> AbsentEmp { get; set; }
        public Nullable<short> LateIn { get; set; }
        public Nullable<short> LateOut { get; set; }
        public Nullable<short> EarlyIn { get; set; }
        public Nullable<short> EarlyOut { get; set; }
        public Nullable<short> OverTime { get; set; }
        public Nullable<short> LeavesEmp { get; set; }
        public Nullable<short> ShortLeaveEmp { get; set; }
        public Nullable<short> HalfLeaveEmp { get; set; }
        public Nullable<short> CompanyID { get; set; }
    
        public virtual Shift Shift { get; set; }
    }
}
