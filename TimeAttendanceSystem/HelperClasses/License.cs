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
        [JsonProperty("users")]
        public String users { get; set; }
        [JsonProperty("LiscenceTypeID")]
        public short LiscenceTypeID { get; set; }
        

    }
}
