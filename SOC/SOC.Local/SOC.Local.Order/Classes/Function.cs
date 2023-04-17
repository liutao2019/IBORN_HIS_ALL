using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.Classes
{
    class Function
    {
        /// <summary>
        /// 药品基本信息帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper phaHelper = null;

        private static FS.HISFC.Models.Pharmacy.Item phaItem = null;

        private static FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 错误信息
        /// </summary>
        private static string ErrInfo = "";

        private static FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 翻译皮试信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [免试] 2 [需皮试] 3 [+] 4 [-]</returns>
        public static string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            return outOrderMgr.TransHypotest(HypotestCode);
        }

        /// <summary>
        /// 获取药品基本信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Pharmacy.Item GetPhaItem(string itemCode)
        {
            if (phaHelper == null)
            {
                phaHelper = new FS.FrameWork.Public.ObjectHelper();
            }

            try
            {
                if (phaHelper.GetObjectFromID(itemCode) == null)
                {
                    phaItem = new FS.HISFC.Models.Pharmacy.Item();
                    phaItem = phaIntegrate.GetItem(itemCode);
                    if (phaItem == null)
                    {
                        ErrInfo = phaIntegrate.Err;
                        return null;
                    }
                    phaHelper.ArrayObject.Add(phaItem);
                }
                else
                {
                    phaItem = phaHelper.GetObjectFromID(itemCode) as FS.HISFC.Models.Pharmacy.Item;
                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return null;
            }
            return phaItem;
        }

        /// <summary>
        /// 显示床位号
        /// </summary>
        /// <param name="orgBedNo"></param>
        /// <returns></returns>
        public static string BedDisplay(string orgBedNo)
        {
            if (orgBedNo == "")
            {
                return orgBedNo;
            }

            string tempBedNo = "";

            if (orgBedNo.Length > 4)
            {
                tempBedNo = orgBedNo.Substring(4);
            }
            else
            {
                return orgBedNo;
            }
            return tempBedNo;

        }

        /// <summary>
        /// 在xml中取医院logo赋予picturebox
        /// </summary>
        /// <param name="xmlpath">xml路径（绝对）  PS：从根目录开始</param>
        /// <param name="root">xml根节点</param>
        /// <param name="secondNode">要查找的目标节点</param>
        /// <param name="erro">错误信息</param>
        public static string GetHospitalLogo(string xmlpath, string root, string secondNode, string erro)
        {

            xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + xmlpath;
            return FS.SOC.Public.XML.SettingFile.ReadSetting(xmlpath, root, secondNode, erro);

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
        /// 画组合号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }


        /// <summary>
        /// 处理farpoint文字过长过行问题(按字节) by huangchw 12-11-15
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="limitedLength">限制长度</param>
        /// <returns>项目名按字节长度截断的ArrayList</returns>
        public static ArrayList SubItemNameFP(string itemName, int limitedLength)
        {
            if (itemName == null || limitedLength < 1)
            {
                return null;
            }

            ArrayList al = new ArrayList();
            string tempItemName = null;
            int tempLength = 0;

            int itemLength = CheckItemNameLength(itemName);
            while (itemLength > limitedLength)
            {
                #region 截取限制长度的字符串tempItemName
                tempLength = 0;
                tempItemName = null;
                foreach (char chr in itemName)
                {
                    if (tempLength <= limitedLength - 2)
                    {
                        if (chr >= 0x4e00 && chr <= 0x9fa5)  //汉字双字节，长度判断加2
                        {
                            tempLength = tempLength + 2;
                        }
                        else
                        {
                            tempLength = tempLength + 1;
                        }
                        tempItemName = tempItemName + chr;
                    }
                    else
                    {
                        break;
                    }
                }
                #endregion

                al.Add(tempItemName);

                itemName = itemName.Substring(tempItemName.Length);

                itemLength = CheckItemNameLength(itemName);
            }
            al.Add(itemName);

            return al;
        }

        /// <summary>
        /// 获取项目名长度(按字节算)--subItemNameFP()中调用
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        private static int CheckItemNameLength(string itemName)
        {
            int itemLength = 0;
            foreach (char chr in itemName)
            {
                if (chr >= 0x4e00 && chr <= 0x9fa5)  //汉字长度加2
                {
                    itemLength = itemLength + 2;
                }
                else
                {
                    itemLength = itemLength + 1;
                }
            }
            return itemLength;
        }

        /// <summary>
        /// 图标通知
        /// </summary>
        static System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 图标通知事件
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tipText"></param>
        /// <param name="tipIcon"></param>
        public static void ShowBalloonTip(int timeout, string tipTitle, string tipText, System.Windows.Forms.ToolTipIcon tipIcon)
        {
            if (notify == null)
            {
                notify = new System.Windows.Forms.NotifyIcon();
                notify.Icon = FS.SOC.Local.Order.Properties.Resources.HIS;
            }
            notify.Visible = true;
            notify.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }
    }



    /// <summary>
    /// 医嘱按照SortID排序
    /// </summary>
    public class ExecOrderCompare : System.Collections.IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder exec1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder exec2 = y as FS.HISFC.Models.Order.ExecOrder;

                if (exec1.Order.SortID > exec2.Order.SortID)
                {
                    return -1;
                }
                else if (exec1.Order.SortID < exec2.Order.SortID)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
