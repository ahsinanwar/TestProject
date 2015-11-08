using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadersCommLibrary
{
    public class EmpUpload
    {
        public string FPID { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int Privilege { get; set; }
        public string Password { get; set; }
        public string Enable { get; set; }
        public int Flag { get; set; }
        public string FP1 { get; set; }


        public EmpUpload(string _FPID, string _Name,int _Index, int _Privilege, string _Password, string _Enable, int _Flag, string _FP1)
        {
            this.FPID = _FPID;
            this.Name = _Name;
            this.Index = _Index;
            this.Privilege = _Privilege;
            this.Password = _Password;
            this.Enable = _Enable;
            this.Flag = _Flag;
            this.FP1 = string.Copy(_FP1);
        }
    }
}
