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
        [JsonProperty("Name")]
        public String Name { get; set; }
     
        [JsonProperty("Licensetype")]
        public License  Licensetype{ get; set; }
        [JsonProperty("MAC")]
        public List<String>  MAC { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("createdby")]
        public String CreatedBy { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
     
     

    }
    
}
