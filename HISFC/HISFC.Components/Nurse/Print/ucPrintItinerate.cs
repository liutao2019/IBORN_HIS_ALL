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
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
    /// </summary>
    public partial class ucPrintItinerate : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region ����
        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        private int iSet = 8;
        #endregion

        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
                this.lblReprint.Visible = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ucPrintItinerate()
        {
            InitializeComponent();
        }

        private void ucPrintItinerate_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="al"></param>
        public void Init(ArrayList al)
        {
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = al.GetRange(iSet * (i - 1), iSet);
                        this.Print(alPrint, i, icount);
                    }
                    else
                    {
                        int num = al.Count % iSet;
                        if (al.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = al.GetRange(iSet * (i - 1), num);
                        this.Print(alPrint, i, icount);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="current">��ǰҳ����</param>
        /// <param name="total">��ҳ��</param>
        private void Print(ArrayList al, int current, int total)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.Models.Nurse.Inject info = null;

            //��ƿ����
            int jpNum = 1;
            //��ֵ����ӡ
            for (int i = 0; i < al.Count; i++)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[i];
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                if (info.Item.Order.Combo.ID.Length <= 2)
                {
                    this.neuSpread1_Sheet1.Cells[0, 0].Text =
                        info.Item.Order.Combo.ID;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[0, 0].Text =
                        info.Item.Order.Combo.ID.Substring(info.Item.Order.Combo.ID.Length - 2, 2);
                }
                //this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Order.Combo.ID;
                if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                {
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Name;
                }
                
                //jpNum = FS.FrameWork.Function.NConvert.ToInt32(info.Memo);
                //if (jpNum == 0)
                //{
                //    jpNum = 1;
                //}
                //this.neuSpread1_Sheet1.Cells[0, 2].Text =
                //    Math.Round(info.Item.Order.DoseOnce / jpNum, 3).ToString() + info.Item.Order.DoseUnit.ToString();

                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString();
                this.neuSpread1_Sheet1.Cells[0, 3].Text = "";
                this.neuSpread1_Sheet1.Cells[0, 4].Text = "";
                this.neuSpread1_Sheet1.Cells[0, 5].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 6].Text = " ";
            }
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            this.lbCard.Text = info.Patient.PID.CardNO;
            this.lbName.Text = info.Patient.Name;
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "��";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "Ů";
            }
            else
            {
                this.lbSex.Text = "";
            }

            this.lbPage.Text = "��" + current.ToString()
                + "ҳ" + "/" + "��" + total.ToString() + "ҳ";

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "����";
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            this.Print();

        }

        /// <summary>
        /// 
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = null;
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();
                //FS.Common.Class.Function.GetPageSize("Inject2", ref p);
            }
            //System.Windows.Forms.Control c = this;
            //c.Width = this.Width;
            //c.Height = this.Height;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPage(12, 1, this.pnlPrint);
        }
    }
}
