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
    
    public partial class DaysName
    {
        public DaysName()
        {
            this.Shifts = new HashSet<Shift>();
            this.Shifts1 = new HashSet<Shift>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<Shift> Shifts1 { get; set; }
    }
}
