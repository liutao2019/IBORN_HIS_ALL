using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common
{
    public static class Function 
    {
        public static bool IsPreview()
        {
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                return true;
            }
            return false;
        }

        public static FS.HISFC.Models.Base.PageSize getPrintPage(bool isLandScape)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageManager = new FS.HISFC.BizLogic.Manager.PageSize();


            FS.HISFC.Models.Base.PageSize pageSize = null;
            if (isLandScape)
            {
               // pageSize = pageManager.GetPageSize("RecipeLand");

                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 895, 579);
                }

                return pageSize;
               
            }
            else 
            {
               //pageSize = pageManager.GetPageSize("Recipe");

                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 579, 895);
                }

                return pageSize;

            }


            return new FS.HISFC.Models.Base.PageSize("A5", 579, 895);
        }


    }

}