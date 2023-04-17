using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.BizLogic.Manager;

namespace FS.HISFC.Components.Order.Classes
{
    //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}

    public class PatientInfoByJZ:DataBase
    {
        public string CARD_NO { get; set; }
        public string NAME { get; set; }
        public string BIRTHDAY { get; set; }
        public string SEX_CODE { get; set; }
        public string HOME_TEL { get; set; }
        public string IDENNO { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string CRMID { get; set; }
    }
}
