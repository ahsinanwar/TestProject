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
    
    public partial class ClientMAC
    {
        public short MacClientID { get; set; }
        public string MACAddress { get; set; }
        public Nullable<int> ClientTabID { get; set; }
        public Nullable<bool> IsUsing { get; set; }
        public Nullable<bool> FirstTime { get; set; }
    
        public virtual ClientInfo ClientInfo { get; set; }
    }
}
