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
    
    public partial class ClientInfo
    {
        public ClientInfo()
        {
            this.ClientMACs = new HashSet<ClientMAC>();
        }
    
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public Nullable<short> LiscenceTypeID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual ICollection<ClientMAC> ClientMACs { get; set; }
    }
}
