using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.HelperClasses
{
    public partial class Package
    {
        [JsonProperty("Client ID")]
        public int ClientID { get; set; }
        [JsonProperty("Name")]
        public String Name { get; set; }
        [JsonProperty("License")]
        public License  license{ get; set; }
        [JsonProperty("MAC")]
        public String mac { get; set; }
        [JsonProperty("isActive")]
        public bool isActive { get; set; }
        
        [JsonProperty("createdby")]
        public String createdby { get; set; }
        [JsonProperty("key")]
        public String key { get; set; }


    }

}
