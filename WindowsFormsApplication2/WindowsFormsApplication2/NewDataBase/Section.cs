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
    
    public partial class Section
    {
        public Section()
        {
            this.Emps = new HashSet<Emp>();
        }
    
        public short SectionID { get; set; }
        public string SectionName { get; set; }
        public Nullable<short> DeptID { get; set; }
    
        public virtual ICollection<Emp> Emps { get; set; }
    }
}
