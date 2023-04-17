using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.GuangZhou.Classes
{
    class Function
    {
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
            return SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
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
        /// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }



        /// <summary>
        ///  按汉字长度截取
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="length">截取的汉字长度</param>
        /// <returns></returns>
        public static string GetStrintByLength(string str, int length)
        {

            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }

                if (len > length * 2)
                {
                    return str.Substring(0, i - 1);
                }

            }
            return str;
        }

        /// <summary>
        /// 获取频次代表的每天次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        public static decimal GetFrequencyCountByOneDay(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1;
            }
            string id = frequencyID.ToLower().Replace(".", "");
            if (id == "qhh")//每0.5小时一次
            {
                return 48;
            }
            else if (id == "qh")//每1小时一次
            {
                return 24;
            }
            else if (id == "q4h")//每4小时一次
            {
                return 6;
            }
            else if (id == "q6h")//每6小时一次
            {
                return 4;
            }
            else if (id == "q8h")//每8小时一次
            {
                return 3;
            }
            else if (id == "q12h")//每12小时一次
            {
                return 2;
            }
            else if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "fid")//每天五次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                System.Collections.ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');

                    //返回一天几次
                    //return str.Length;

                    //tid为3次 Q3D为1/3次
                    try
                    {
                        return str.Length / FS.FrameWork.Function.NConvert.ToDecimal(obj.Days[0]);
                    }
                    catch
                    {
                        return str.Length;
                    }
                }
                return 100;
            }
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

    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //return;
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Log/Order/LocalOrder") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Order/LocalOrder");
            }
            //保存一周的日志
            System.IO.File.Delete("./Log/Order/LocalOrder/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Order/LocalOrder/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}
