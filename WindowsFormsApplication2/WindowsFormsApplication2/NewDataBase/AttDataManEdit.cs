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
    
    public partial class AttDataManEdit
    {
        public int AttEditID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public Nullable<System.DateTime> EditDateTime { get; set; }
        public Nullable<System.DateTime> OldTimeIn { get; set; }
        public Nullable<System.DateTime> OldTimeOut { get; set; }
        public Nullable<System.DateTime> NewTimeIn { get; set; }
        public Nullable<System.DateTime> NewTimeOut { get; set; }
        public string EmpDate { get; set; }
        public Nullable<bool> Process { get; set; }
        public string OldDutyCode { get; set; }
        public Nullable<System.TimeSpan> OldDutyTime { get; set; }
        public string NewDutyCode { get; set; }
        public Nullable<System.TimeSpan> NewDutyTime { get; set; }
        public Nullable<int> UserID { get; set; }
        public string OldRemarks { get; set; }
        public string NewRemarks { get; set; }
        public Nullable<short> OldShiftMin { get; set; }
        public Nullable<short> NewShiftMin { get; set; }
    }
}
