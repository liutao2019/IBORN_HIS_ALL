using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Speciment
{
   public class BarCodeManage : FS.FrameWork.Management.Database
    {
       public string GetBarCode(string codeType)
       {
           if (codeType == "S")
           {
               return this.GetSequence("Speciment.BizLogic.BarCodeManage.SouceBarCode");
           }
           return this.GetSequence("Speciment.BizLogic.BarCodeManage.SubSouceBarCode");
       }
    }
}
