using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Common
{
    /// <summary>
    /// [功能描述: 常用函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-05-31]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Function
    {
        public static void DrawCombo(object sender, int column, int drawColumn, int childViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (childViewLevel == 0)
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
                                o.Cells[i, drawColumn].Text = "┓";
                                try
                                {
                                    if (o.Cells[i - 1, drawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, drawColumn].Text = "┛";
                                    }
                                    else if (o.Cells[i - 1, drawColumn].Text == "┓")
                                    {
                                        o.Cells[i - 1, drawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, drawColumn].Text = "┃";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, drawColumn].Text == "┃")
                                    {
                                        o.Cells[i - 1, drawColumn].Text = "┛";
                                    }
                                    else if (o.Cells[i - 1, drawColumn].Text == "┓")
                                    {
                                        o.Cells[i - 1, drawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, drawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, drawColumn].Text == "┃") o.Cells[i, drawColumn].Text = "┛";
                            if (i == o.RowCount - 1 && o.Cells[i, drawColumn].Text == "┓") o.Cells[i, drawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (childViewLevel == 1)
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
                                    c.Cells[j, drawColumn].Text = "┓";
                                    try
                                    {
                                        if (c.Cells[j - 1, drawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, drawColumn].Text = "┛";
                                        }
                                        else if (c.Cells[j - 1, drawColumn].Text == "┓")
                                        {
                                            c.Cells[j - 1, drawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, drawColumn].Text = "┃";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, drawColumn].Text == "┃")
                                        {
                                            c.Cells[j - 1, drawColumn].Text = "┛";
                                        }
                                        else if (c.Cells[j - 1, drawColumn].Text == "┓")
                                        {
                                            c.Cells[j - 1, drawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, drawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, drawColumn].Text == "┃") c.Cells[j, drawColumn].Text = "┛";
                                if (j == c.RowCount - 1 && c.Cells[j, drawColumn].Text == "┓") c.Cells[j, drawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
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
        /// <param name="drawColumn"></param>
        public static void DrawCombo(object sender, int column, int drawColumn)
        {
            DrawCombo(sender, column, drawColumn, 0);
        }

    }
}
