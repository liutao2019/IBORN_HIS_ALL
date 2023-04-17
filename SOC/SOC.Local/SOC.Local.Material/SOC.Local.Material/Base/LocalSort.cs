using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Material.Base
{
    public class LocalSort
    {
        internal class CompareByCustomerCode : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic baseLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic();

                if (x is FS.HISFC.BizLogic.Material.Object.Output && y is FS.HISFC.BizLogic.Material.Object.Output)
                {
                    oX = baseLogic.GetBaseInfoByID((x as FS.HISFC.BizLogic.Material.Object.Output).Clone().BaseInfo.ID).UserCode;
                    oY = baseLogic.GetBaseInfoByID((y as FS.HISFC.BizLogic.Material.Object.Output).Clone().BaseInfo.ID).UserCode;
                }
                else if (x is FS.HISFC.BizLogic.Material.Object.Input && y is FS.HISFC.BizLogic.Material.Object.Input)
                {
                    oX = baseLogic.GetBaseInfoByID((x as FS.HISFC.BizLogic.Material.Object.Input).Clone().StockInfo.BaseInfo.ID).UserCode;
                    oY = baseLogic.GetBaseInfoByID((y as FS.HISFC.BizLogic.Material.Object.Input).Clone().StockInfo.BaseInfo.ID).UserCode;
                }
                else if (x is FS.HISFC.BizLogic.Material.Object.CheckDetail && y is FS.HISFC.BizLogic.Material.Object.CheckDetail)
                {
                    oX = baseLogic.GetBaseInfoByID((x as FS.HISFC.BizLogic.Material.Object.CheckDetail).Clone().BaseInfo.ID).UserCode;
                    oY = baseLogic.GetBaseInfoByID((y as FS.HISFC.BizLogic.Material.Object.CheckDetail).Clone().BaseInfo.ID).UserCode;
                }
                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
    }
}
