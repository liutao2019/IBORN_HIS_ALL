using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Ticker
{
    public class TickerWriteOffRecord
    {
        public int ID { set; get; }

        public string TickerNO { set; get; }

        public string Card_NO { set; get; }

        public decimal WriteOffFee { set; get; }

        public string ConSutype { set; get; }

        public string Oper_ID { set; get; }

        public string Oper_Name { set; get; }

        public DateTime CreateDate { set; get; }


        //{E40946A4-FEB0-4842-BEAB-472BD85F1829}
        public string Title { set; get; }

        public string Tcontent { set; get; }//{E40946A4-FEB0-4842-BEAB-472BD85F1829}

        public string ReceivePersonName { set; get; }//{E40946A4-FEB0-4842-BEAB-472BD85F1829}
    }
}
