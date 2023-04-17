using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    class cTable
    {
        private string item;
        private ArrayList arrSelectBy;

        public string Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        public ArrayList ArrSelectBy
        {
            get
            {
                return arrSelectBy;
            }
            set
            {
                arrSelectBy = value;
            }
        }
    }
}
