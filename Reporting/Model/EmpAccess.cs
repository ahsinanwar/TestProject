//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reporting.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpAccess
    {
        public int AccessID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public Nullable<byte> RdrID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> Access { get; set; }
    
        public virtual Emp Emp { get; set; }
    }
}
