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
    
    public partial class RosterType
    {
        public RosterType()
        {
            this.RosterApps = new HashSet<RosterApp>();
            this.Shifts = new HashSet<Shift>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Repeat { get; set; }
    
        public virtual ICollection<RosterApp> RosterApps { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
