using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.OrderPrint
{
    public class Function : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 药品信息
        /// </summary>
        private Hashtable hsPhaItem = new Hashtable();

        private FS.HISFC.Models.Pharmacy.Item phaItem = null;

        private FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 翻译皮试信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string TransHypotest(string itemCode, FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            try
            {
                if (this.hsPhaItem.Contains(itemCode))
                {
                    phaItem = hsPhaItem[itemCode] as FS.HISFC.Models.Pharmacy.Item;
                }
                else
                {
                    phaItem = itemMgr.GetItem(itemCode);
                    if (phaItem != null)
                    {
                        hsPhaItem.Add(phaItem.ID, phaItem);
                    }
                }

                if (phaItem != null && phaItem.IsAllergy)
                {
                    return orderMgr.TransHypotest(HypotestCode);
                }
            }
            catch
            {
                return "";
            }

            return "";
        }

        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        public static void DrawCombo1(object sender, int column, int DrawColumn)
        {
            Function.DrawCombo1(sender, column, DrawColumn, 0);
        }

        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        /// <param name="ChildViewLevel"></param>
        public static void DrawCombo1(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "画"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //是头
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "┏";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "┃";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┃") o.Cells[i, DrawColumn].Text = "┗";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┏") o.Cells[i, DrawColumn].Text = "";
                            o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "画"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //是头
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "┏";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┃") c.Cells[j, DrawColumn].Text = "┗";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┏") c.Cells[j, DrawColumn].Text = "";
                                c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

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
        public static int GetItemQaulity(FS.HISFC.Models.Order.Inpatient.Order order)
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
                    else if (quaulity.Memo.Contains("毒")
                        || quaulity.Memo.Contains("麻")
                        || quaulity.Memo.Contains("精一")

                        || quaulity.UserCode.Contains("P1")//精一
                        //|| quaulity.UserCode.Contains("P")//精神类  //{65BAEEB6-D491-4616-925A-EE90F0325048}由于易制毒的药品要放在易制毒里面打印，所以usercode为P但是处方不能打印成麻精一处方
                        || quaulity.UserCode.Contains("S")//毒药
                        )
                    {
                        quaulityType = 3;
                    }
                    else if (quaulity.Memo.Contains("普")

                    || quaulity.UserCode.Contains("O1")//精二
                    )
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

        /// <summary>
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }


        /// <summary>
        /// 括号在左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        /// <param name="ChildViewLevel"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "画"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //是头
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "┏";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "┃";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "┗";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "┏")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┃") o.Cells[i, DrawColumn].Text = "┗";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "┏") o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Black;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "画"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //是头
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "┏";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "┗";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "┏")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┃") c.Cells[j, DrawColumn].Text = "┗";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "┏") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Black;
                                #endregion

                            }
                        }
                    }
                    break;
            }

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

    }
}
