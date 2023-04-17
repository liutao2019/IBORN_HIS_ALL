using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /// <summary>
    /// [��������: ҩƷ�ɹ��ƻ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucSplitPlan : UserControl
    {
        public ucSplitPlan()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ԭʼ�ƻ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.StockPlan originalStockPlan = null;

        /// <summary>
        /// ��ֺ�ƻ���Ϣ
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.StockPlan> splitPlan = null;

        /// <summary>
        /// ������˾������
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// ���ڽ��
        /// </summary>
        private DialogResult result = DialogResult.Cancel;
        #endregion

        #region ����

        /// <summary>
        /// ԭʼ�ƻ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.StockPlan OriginalStockPlan
        {
            get
            {
                return this.originalStockPlan;
            }
            set
            {
                this.originalStockPlan = value;

                this.SetPlanInfo();
            }
        }

        /// <summary>
        /// ��ֺ�ƻ���Ϣ
        /// </summary>
        public List<FS.HISFC.Models.Pharmacy.StockPlan> SplitPlan
        {
            get
            {
                return this.splitPlan;
            }
            set
            {
                this.splitPlan = value;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            this.alCompany = phaConsManager.QueryCompany("1");
            if (this.alCompany == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������˾�б�������") + phaConsManager.Err);
                return;
            }

            this.neuSpread1_Sheet1.Columns[0].Locked = true;
            this.neuSpread1_Sheet1.Columns[2].Locked = true;
        }

        /// <summary>
        /// ԭʼ�ƻ���Ϣ��ʾ
        /// </summary>
        private void SetPlanInfo()
        {
            this.lbOriginalPlan.Text = string.Format("��Ʒ����: {0} ���: {1} �ƻ�����: {2} ��λ: {3}", this.originalStockPlan.Item.Name, 
                this.originalStockPlan.Item.Specs, (this.originalStockPlan.StockApproveQty / this.originalStockPlan.Item.PackQty).ToString(), 
                this.originalStockPlan.Item.PackUnit);

            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.neuSpread1_Sheet1.Rows.Add(0, 1);
            this.neuSpread1_Sheet1.Cells[0, 0].Text = this.originalStockPlan.Company.Name;
            this.neuSpread1_Sheet1.Cells[0, 1].Text = (this.originalStockPlan.StockApproveQty / this.originalStockPlan.Item.PackQty).ToString();
            this.neuSpread1_Sheet1.Cells[0, 2].Text = this.originalStockPlan.Item.PackUnit;

            this.neuSpread1_Sheet1.Rows[0].Tag = this.originalStockPlan.Company;

            this.SetStockQtyInfo();
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns>�ɹ�����True  ʧ�ܷ���False</returns>
        protected bool IsValid()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "" && FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i,1].Text) != 0)
                {
                    MessageBox.Show(Language.Msg("�����õ� " + (i+1).ToString() + " �й�����˾"));
                    return false;
                }
            }

            if (this.lbSpareInfo.Visible)
            {
                MessageBox.Show(Language.Msg("����������ò�ƽ,����������"));
                return false;
            }            

            return true;
        }

        /// <summary>
        /// ���ƻ���Ϣ���
        /// </summary>
        protected int SplitStockPlan()
        {
            if (!this.IsValid())
            {
                return -1;
            }

            this.splitPlan = new List<FS.HISFC.Models.Pharmacy.StockPlan>();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "")
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.StockPlan alterStockPlan = this.originalStockPlan.Clone();
                //���ڷ�ԭʼ��¼ ������ˮ��Ϊ�� ����ʱ���в��� ����һֱ���и��²���
                if (i > 0)
                {
                    alterStockPlan.ID = "";
                }

                alterStockPlan.Company = this.neuSpread1_Sheet1.Rows[i].Tag as FS.FrameWork.Models.NeuObject;

                alterStockPlan.StockApproveQty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 1].Text) * this.originalStockPlan.Item.PackQty;

                this.splitPlan.Add(alterStockPlan);
            }

            return 1;
        }

        /// <summary>
        /// ���òɹ�������Ϣ
        /// </summary>
        private void SetStockQtyInfo()
        {
            decimal totQty = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                totQty = totQty + FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 1].Text);
            }

            this.lbTotInfo.Text = "��ǰ�ɹ�������:" + totQty.ToString() + " " + this.originalStockPlan.Item.PackUnit;

            decimal spareQty = this.originalStockPlan.StockApproveQty / this.originalStockPlan.Item.PackQty - totQty;
            if (spareQty == 0)
            {
                this.lbSpareInfo.Visible = false;
            }
            else
            {
                this.lbSpareInfo.Visible = true;

                this.lbSpareInfo.Text = "ʣ��ɹ�����:" + spareQty.ToString();
            }
        }        

        /// <summary>
        /// ������˾���� 
        /// </summary>
        /// <param name="rowIndex"></param>
        private void PopCompany(int rowIndex)
        {
            FS.FrameWork.Models.NeuObject companyObj = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref companyObj) == 1)
            {
                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = companyObj.Name;
                this.neuSpread1_Sheet1.Rows[rowIndex].Tag = companyObj;
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);

            this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = this.originalStockPlan.Item.PackUnit;

            //���õ�ǰѡ����
            this.neuSpread1_Sheet1.ActiveRowIndex = rowIndex;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0 && this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex,1);
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
                return;

            if (e.Column == 0)
            {
                this.PopCompany(e.Row);
            }
        }

        /// <summary>
        /// �������ʷҩƷ�ɹ���¼�ĵ�������
        /// </summary>
        private void fpStockApprove_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //��ǰ��¼���С���
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;
            int j = this.neuSpread1_Sheet1.ActiveColumnIndex;

            //�س������� 13 �ո������ 32
            if (e.KeyChar == 32)
            {
                this.PopCompany(i);
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SplitStockPlan() == -1)
            {
                this.result = DialogResult.Cancel;
            }
            else
            {
                this.result = DialogResult.OK;

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.Cancel;

            this.Close();
        }

        private void neuSpread1_EditModeOff(object sender, EventArgs e)
        {
            this.SetStockQtyInfo();
        }
    }
}
