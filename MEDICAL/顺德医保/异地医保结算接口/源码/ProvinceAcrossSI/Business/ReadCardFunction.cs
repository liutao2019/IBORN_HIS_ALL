using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ProvinceAcrossSI.Business
{
    class ReadCardFunction
    {
         [DllImport("SSCardDriver.dll")]
        public static extern long iReadCardBas(int iType, byte[] pOutInfo);

        [DllImport("SSCardDriver.dll")]
        public static extern long iReadCardBas_HSM_Step1(int iType, byte[] pOutInfo);

        [DllImport("SSCardDriver.dll")]
        public static extern long iReadCardBas_HSM_Step2(byte[] pKey, byte[] pOutInfo);
    }
}
