using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.HISFC.Components.Nurse.Print
{
    public partial class ucPrintItinerateLarge : UserControl
    {
        #region 域
        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        #endregion

        public ucPrintItinerateLarge()
        {
            InitializeComponent();
        }

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        public void Init(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.Models.Nurse.Inject info = null;
            
            for (int i = 0; i < al.Count; i++)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[i];
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Order.Combo.ID;
                if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                {
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Name;
                }
                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Item.Name;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.Item.Specs;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = info.Item.Order.Frequency.ID;
                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.Order.Usage.Name;//info.Item.DoseOnce.ToString() + info.Item.DoseUnit.ToString();
                this.neuSpread1_Sheet1.Cells[0, 5].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 6].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 7].Text = " ";
            }
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            this.lbCard.Text = info.Patient.PID.CardNO;
            this.lbName.Text = info.Patient.Name;
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "男";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "女";
            }
            else
            {
                this.lbSex.Text = "";
            }
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "拔针";

            #region 设置界面用来配合打印

            System.Windows.Forms.Control c = this;
            c.Width = this.Width;
            c.Height = this.Height;

            #endregion

            //打印机
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //FS.Common.Class.Function.GetPageSize("Inject4", ref p);
            p.PrintPage(40, 0, c);
        }
    }
}
