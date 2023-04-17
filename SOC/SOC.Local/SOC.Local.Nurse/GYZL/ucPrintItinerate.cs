using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Nurse.GYZL
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
    /// </summary>
    public partial class ucPrintItinerate : UserControl, Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region ����
        
        private ArrayList alPrint = new ArrayList();
        
        private Neusoft.HISFC.BizLogic.Nurse.Inject injectMgr = new Neusoft.HISFC.BizLogic.Nurse.Inject();


        private Neusoft.HISFC.BizProcess.Integrate.Order orderMgr = new Neusoft.HISFC.BizProcess.Integrate.Order();
        
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
                int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / iSet));

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
            Neusoft.HISFC.Models.Nurse.Inject info = null;

            //��ƿ����
            int jpNum = 1;
            //��ֵ����ӡ
            for (int i = 0; i < al.Count; i++)
            {
                info = (Neusoft.HISFC.Models.Nurse.Inject)al[i];
                

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
               

                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString();
                string strHypo = string.Empty;
                switch (info.Hypotest)
                {
                    case "2":
                        strHypo = "��Ƥ��";
                        break;
                    case "3":
                        strHypo = "����";
                        break;
                    case "4":
                        strHypo = "����";
                        break;
                    default:
                        strHypo = " ";
                        break;
                }
                this.neuSpread1_Sheet1.Cells[0, 3].Text = strHypo;
                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Memo;
                this.neuSpread1_Sheet1.Cells[0, 5].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 6].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 7].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 8].Text = " ";
                this.neuSpread1_Sheet1.Cells[0, 9].Text = " ";
            }
            info = (Neusoft.HISFC.Models.Nurse.Inject)al[0];
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
            Neusoft.FrameWork.WinForms.Classes.Print p = null;
            if (p == null)
            {
                p = new Neusoft.FrameWork.WinForms.Classes.Print();
                //neusoft.Common.Class.Function.GetPageSize("Inject2", ref p);
            }
            //System.Windows.Forms.Control c = this;
            //c.Width = this.Width;
            //c.Height = this.Height;
            //p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPage(12, 1, this.pnlPrint);
        }
    }
}
