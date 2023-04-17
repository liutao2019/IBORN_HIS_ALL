using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.MedicalInsurance.Proxy
{
    public class DefaultMedical : IBorn.SI.BI.IMedical
    {
        #region IMedical 成员

        public IBorn.SI.BI.IBalance CreateBalance()
        {
            return new DefaultBalance();
        }

        public IBorn.SI.BI.ICompare CreateCompare()
        {
            return new DefaultCompare();
        }

        public IBorn.SI.BI.IRADT CreateRADT()
        {
            return new DefaultRADT();
        }

        public IBorn.SI.BI.IUpload CreateUpload()
        {
            return new DefaultUpload();
        }

        #endregion


    }
}
