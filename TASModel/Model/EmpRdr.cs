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
    
    public partial class EmpRdr
    {
        public int ID { get; set; }
        public int EmpID { get; set; }
        public short RdrID { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
    
        public virtual Reader Reader { get; set; }
    }
}