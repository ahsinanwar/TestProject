using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadersCommLibrary
{
    public class EmpCard
    {
        public Int32 FPID { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public int Privilage { get; set; }
        public bool Enable { get; set; }
        public EmpCard(Int32 _FPID, string _CardNo,string _Name,int _Privilage,bool _Enable)
        {
            this.FPID = _FPID;
            this.CardNo = _CardNo;
            this.Name = _Name;
            this.Privilage = _Privilage;
            this.Enable = _Enable;
        }
    }
}
