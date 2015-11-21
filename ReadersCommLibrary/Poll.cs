using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadersCommLibrary
{
    public class Poll
    {
        public Int32 FPID { get; set; }
        public DateTime EntryDateTime { get; set; }

        public Poll(Int32 FPID, DateTime EntryDateTime)
        {
            this.FPID = FPID;
            this.EntryDateTime = EntryDateTime;
        }
    }
}
