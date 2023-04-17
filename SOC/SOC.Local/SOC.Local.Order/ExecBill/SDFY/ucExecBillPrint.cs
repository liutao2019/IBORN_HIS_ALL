using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ExecBill.SDFY
{
    public partial class ucExecBillPrint : UserControl
    {
        public ucExecBillPrint()
        {
            InitializeComponent();
        }

        private ArrayList alOrders = new ArrayList();

        public ArrayList AlOrders
        {
            set
            {
                this.alOrders = value;
                this.SetItems();
            }
        }

        private string title = "";

        public string Title
        {
            set
            {
                this.title = value;
                this.lblTitle.Text = value;
            }
        }

        private string titleDate = "";

        public string TitleDate
        {
            set
            {
                this.titleDate = value;
                this.neuLblExecTime.Text = value;
            }
        }

        private string page = "";

        public string Page
        {
            set
            {
                this.page = value;
                this.lblPage.Text = "第" + value + "页";
            }
        }

        private void SetItems()
        {
            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);

            foreach (string s in alOrders)
            {
                string[] items = s.Split('|');

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                for (int i = 0; i < items.Length - 1; i++)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, i].Text = items[i];
                }
                if (this.neuSpread1_Sheet1.Rows.Count == 1)
                {
                    continue;
                }
                else
                {
                    if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text
                    != this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 1].Text)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count - 1, 1);
                    }
                }
                //int length = System.Text.Encoding.GetEncoding("gb2312").GetBytes("呵呵哈哈哈哈哈哈哈哈哈呵呵呵哈哈哈").Length;

                ////名称过长自动换行
                //if (System.Text.Encoding.GetEncoding("gb2312").GetBytes(items[4]).Length > length)
                //{
                //    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Height = 2 * this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Height;
                //}
            }
            #region 内容

            //只显示下面的边框
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "")
                    {
                        continue;
                    }

                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;

                    if (i != this.neuSpread1_Sheet1.RowCount - 1)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "┏" || this.neuSpread1_Sheet1.Cells[i, 3].Text == "┃")
                        {
                            this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder3;
                        }
                        if (this.neuSpread1_Sheet1.RowCount > 1)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, 1].Text != this.neuSpread1_Sheet1.Cells[i + 1, 1].Text)
                            {
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                            }
                        }
                        if (i > 0 && this.neuSpread1_Sheet1.Cells[i - 1, 0].Text == "")
                        {
                            this.neuSpread1_Sheet1.Rows[i - 1].Border = bevelBorder2;
                        }
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                    }

                    this.neuSpread1_Sheet1.Cells[i, 1].Border = bevelBorder2;
                    this.neuSpread1_Sheet1.Cells[i, 2].Border = bevelBorder2;
                }
            }

            #endregion
        }
    }
}
