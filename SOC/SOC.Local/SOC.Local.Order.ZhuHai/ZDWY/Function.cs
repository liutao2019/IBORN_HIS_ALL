using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY
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

        public static FS.HISFC.Models.Base.PageSize GetPrintPage(bool isLandScape)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageManager = new FS.HISFC.BizLogic.Manager.PageSize();


            FS.HISFC.Models.Base.PageSize pageSize = null;
            if (isLandScape)
            {
                // pageSize = pageManager.GetPageSize("RecipeLand");

                if (pageSize == null)
                {
                    //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 895, 579);
                    pageSize = new FS.HISFC.Models.Base.PageSize("A5", 880, 550);
                }

                return pageSize;

            }

            if (pageSize == null)
            {
                //pageSize = new FS.HISFC.Models.Base.PageSize("A5", 579, 895);
                pageSize = new FS.HISFC.Models.Base.PageSize("A5", 550, 880);
            }

            return pageSize;
        }

        /// <summary>
        /// 药品性质转换类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsDrugQuaulity = null;

        /// <summary>
        /// 获取药品性质的分方类别
        /// 3、毒麻精一；2、精二；1、普通；0、非药品
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public static int GetItemQaulity(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //3、毒麻精一；2、精二；1、普通；0、非药品
            if (hsDrugQuaulity == null)
            {
                hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                {
                    hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                }
            }

            int quaulityType = 0;
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((order.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                if (quaulity != null && quaulity.ID.Length > 0)
                {
                    if (quaulity.Memo.Contains("精二")

                        || quaulity.UserCode.Contains("P2")//精二
                        )
                    {
                        quaulityType = 2;
                    }
                    else if (quaulity.Memo.Contains("麻")
                        || quaulity.Memo.Contains("精一")

                        || quaulity.UserCode.Contains("P1")//精一
                        || quaulity.UserCode.Contains("P")//精神类
                        )
                    {
                        quaulityType = 3;
                    }
                    //{564D4750-E736-432b-A5B6-B0B178AF1A11}
                    else if (quaulity.Memo.Contains("毒")
                        || quaulity.UserCode.Contains("S"))//毒药
                    {
                        quaulityType = 4;
                    }
                    else
                    {
                        quaulityType = 1;
                    }
                }
            }

            return quaulityType;
        }

        //{5FFE7DFF-358B-4e65-B798-1A2D7478A251}
        /// <summary>
        /// 获取药品性质的分方类别
        /// 3、毒麻精一；2、精二；1、普通；0、非药品
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public static int GetItemQualityForPrint(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //3、毒麻精一；2、精二；1、普通；0、非药品
            if (hsDrugQuaulity == null)
            {
                hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                {
                    hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                }
            }

            int quaulityType = 0;
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((order.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                if (quaulity != null && quaulity.ID.Length > 0)
                {
                    if (quaulity.Memo.Contains("精二")

                        || quaulity.UserCode.Contains("P2")//精二
                        )
                    {
                        quaulityType = 2;
                    }
                    else if (quaulity.Memo.Contains("麻")
                        || quaulity.Memo.Contains("精一")

                        || ((quaulity.UserCode.Contains("P1") 
                        || quaulity.UserCode.Contains("P")) && !quaulity.Memo.Contains("普"))
                        )
                    {
                        quaulityType = 3;
                    }
                    //{564D4750-E736-432b-A5B6-B0B178AF1A11}
                    else if (quaulity.Memo.Contains("毒")
                        || quaulity.UserCode.Contains("S"))//毒药
                    {
                        quaulityType = 4;
                    }
                    else
                    {
                        quaulityType = 1;
                    }
                }
            }

            return quaulityType;
        }

        public static string GetShowTime(DateTime dt,DateTime queryDate)
        {
            if(queryDate<new DateTime(2000,1,1))
            {
                FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
                queryDate = dbMgr.GetDateTimeFromSysDateTime();
            }
            int hour = dt.Hour;
            if (dt.Date == queryDate.Date)
            {
                //return "今" + dt.ToString("HH");
                // {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                return hour <= 12 ? hour.ToString() + "a     " : (hour - 12).ToString() + "p     ";// dt.ToString("HH:mm");// {E97273E4-CF5A-47bf-97C6-8025504486C4}
            }
            else if (dt.Date == queryDate.Date.AddDays(1))
            {
                //return "明" + dt.ToString("HH");
                return queryDate.Month.ToString() + "." + queryDate.Day.ToString() + " " + (hour <= 12 ? hour.ToString() + "a     " : (hour - 12).ToString() + "p     "); //dt.ToString("HH:mm");// {E97273E4-CF5A-47bf-97C6-8025504486C4}
            }
            //else if (dt.Date == queryDate.Date.AddDays(-1))
            //{
            //    return "昨" + dt.ToString("HH");
            //}
            else
            {
                return hour <= 12 ? hour.ToString() + "a     " : (hour - 12).ToString() + "p     "; ;
            }
        }
    }
}
