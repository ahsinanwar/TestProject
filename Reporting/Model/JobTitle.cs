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
    
    public partial class JobTitle
    {
        public JobTitle()
        {
            this.Emps = new HashSet<Emp>();
        }
    
        public short JobID { get; set; }
        public string JobTitle1 { get; set; }
    
        public virtual ICollection<Emp> Emps { get; set; }
    }
}
