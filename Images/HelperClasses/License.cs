using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.HelperClasses
{
    public partial class License
    {
        [JsonProperty("TypeId")]
        public short TypeId { get; set; }
        [JsonProperty("NoOfDevices")]
        public int NoOfDevices { get; set; }
        [JsonProperty("NoOfUsers")]
        public int NoOfUsers { get; set; }
        [JsonProperty("NoOfEmployees")]
        public int NoOfEmployees { get; set; }
        [JsonProperty("LicenseName")]
        public String LicenseName { get; set; }
        

    }
    
}
