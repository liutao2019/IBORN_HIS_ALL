using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.Common
{
    public static class Function 
    {
        public static bool IsPreview()
        {
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                return true;
            }
            return false;
        }

        public static Neusoft.HISFC.Models.Base.PageSize GetPrintPage(bool isLandScape)
        {
            Neusoft.HISFC.BizLogic.Manager.PageSize pageManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();


            Neusoft.HISFC.Models.Base.PageSize pageSize = null;
            if (isLandScape)
            {
               // pageSize = pageManager.GetPageSize("RecipeLand");

                if (pageSize == null)
                {
                    pageSize = new Neusoft.HISFC.Models.Base.PageSize("A5", 895, 579);
                }

                return pageSize;
               
            }
            else 
            {
               //pageSize = pageManager.GetPageSize("Recipe");

                if (pageSize == null)
                {
                    pageSize = new Neusoft.HISFC.Models.Base.PageSize("A5", 579, 895);
                }

                return pageSize;

            }


            return new Neusoft.HISFC.Models.Base.PageSize("A5", 579, 895);
        }


    }

}