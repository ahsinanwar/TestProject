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
    
    public partial class Reader
    {
        public Reader()
        {
            this.PollDatas = new HashSet<PollData>();
        }
    
        public short RdrID { get; set; }
        public string RdrName { get; set; }
        public byte RdrDutyID { get; set; }
        public string IpAdd { get; set; }
        public short IpPort { get; set; }
        public byte RdrTypeID { get; set; }
        public bool Status { get; set; }
        public Nullable<short> LocID { get; set; }
    
        public virtual ICollection<PollData> PollDatas { get; set; }
        public virtual ReaderType ReaderType { get; set; }
    }
}
