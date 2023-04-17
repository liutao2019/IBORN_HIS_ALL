using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Management;

namespace UFC.Pharmacy
{
    /// <summary>
    /// [��������: �����浥��ѡ�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucIMAListSelecct : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucIMAListSelecct()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

                this.Query();
            }
        }

        public delegate void SelectListHandler(string listCode, string state, Neusoft.NFC.Object.NeuObject targetDept);

        public event SelectListHandler SelecctListEvent;

        #region �����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject deptInfo = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����Ȩ������
        /// </summary>
        private string class2Priv = "0310";

        /// <summary>
        /// ״̬
        /// </summary>
        private string state = "2";

        /// <summary>
        /// ��/����״̬��
        /// </summary>
        private System.Collections.Hashtable hsInOutState = null;

        /// <summary>
        /// �ɹ�״̬��
        /// </summary>
        private System.Collections.Hashtable hsStockState = null;

        /// <summary>
        /// ��ⵥ����ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        private int inIntervalDays = 30;

        /// <summary>
        /// ���ⵥ����ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        private int outIntervalDays = 15;

        /// <summary>
        /// �ɹ�������ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        private int stockIntervalDays = 30;

        /// <summary>
        /// ���˵�Ȩ����
        /// </summary>
        private System.Collections.Hashtable markPrivType = null;

        /// <summary>
        /// ��ǰ������Ȩ������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privType = null;
      
        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.NFC.Object.NeuObject DeptInfo
        {
            get
            {
                return this.deptInfo;
            }
            set
            {
                this.deptInfo = value;

                if (value != null)
                {
                    this.lbInfo.Text = value.Name + " - �����б�";
                }
            }
        }

        /// <summary> 
        /// ����Ȩ�� �������� 0310��� 0320����  0312�ɹ�
        /// </summary>
        public string Class2Priv
        {
            set
            {
                this.class2Priv = value;

                switch (value)
                {
                    case "0310":    //���
                        this.rbIn.Checked = true;
                        break;
                    case "0320":    //c����
                        this.rbOut.Checked = true;
                        break;
                    case "0312":    //�ɹ�
                        this.rbStock.Checked = true;
                        break;
                }

                this.SetPrivState();
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���ܰ�ť
        /// </summary>
        public bool IsShowFunButton
        {
            get
            {
                return this.btnOK.Visible;
            }
            set
            {
                this.btnOK.Visible = value;
                this.btnCancel.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����ѡ��CheckBox
        /// </summary>
        public bool IsShowTypeCheck
        {
            get
            {
                return this.rbIn.Visible;
            }
            set
            {
                this.rbIn.Visible = value;
                this.rbOut.Visible = value;
                this.rbStock.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ����ѡ��״̬
        /// </summary>
        public bool IsSelectState
        {
            get
            {
                return this.cmbState.Enabled;
            }
            set
            {
                this.cmbState.Enabled = value;
            }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;

                this.SetPrivState();
            }
        }

        /// <summary>
        /// ��ⵥ����ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        public int InIntervalDays
        {
            get
            {
                return this.inIntervalDays;
            }
            set
            {
                this.inIntervalDays = value;
            }
        }

        /// <summary>
        /// ���ⵥ����ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        public int OutIntervalDays
        {
            get
            {
                return outIntervalDays;
            }
            set
            {
                outIntervalDays = value;
            }
        }

        /// <summary>
        /// �ɹ�������ʾʱ Ĭ�ϼ���ʱ����
        /// </summary>
        public int StockIntervalDays
        {
            get
            {
                return stockIntervalDays;
            }
            set
            {
                stockIntervalDays = value;
            }
        }

        /// <summary>
        /// ��ȡ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return Neusoft.NFC.Function.NConvert.ToDateTime(this.dtpBegin.Text).Date;
            }
        }

        /// <summary>
        /// ��ȡ��ֹʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return Neusoft.NFC.Function.NConvert.ToDateTime(this.dtpEnd.Text).Date.AddDays(1);
            }
        }

        /// <summary>
        /// ���˵�Ȩ����
        /// </summary>
        public System.Collections.Hashtable MarkPrivType
        {
            get
            {
                return this.markPrivType;
            }
            set
            {
                this.markPrivType = value;
            }
        }

        /// <summary>
        /// �����״̬��
        /// </summary>
        public System.Collections.Hashtable InOutStateCollection
        {
            get
            {
                return this.hsInOutState;
            }
            set
            {
                this.hsInOutState = value;
            }
        }

        /// <summary>
        /// �ɹ�״̬��
        /// </summary>
        public System.Collections.Hashtable StockStateCollection
        {
            get
            {
                return this.hsStockState;
            }
            set
            {
                this.hsStockState = value;
            }
        }

        /// <summary>
        /// ��ǰ������Ȩ������
        /// </summary>
        public Neusoft.NFC.Object.NeuObject PrivType
        {
            get
            {
                return privType;
            }
            set
            {
                privType = value;
            }
        }

        #endregion

        /// <summary>
        /// ���ݶ���Ȩ����״̬������ʾ
        /// </summary>
        private void SetPrivState()
        {
            switch(this.class2Priv)
            {
                case "0310":            //���
                case "0320":            //����
                    string[] stateCollection = new string[this.hsInOutState.Count];
                    ((System.Collections.ICollection)this.hsInOutState.Values).CopyTo(stateCollection,0);
                    this.cmbState.DataSource = stateCollection;

                    if (this.hsInOutState.ContainsKey(this.state))
                        this.cmbState.Text = this.hsInOutState[this.state].ToString();

                    break;
                case "0312":            //�ɹ�

                    string[] stockStateCollection = new string[this.hsStockState.Count];
                    ((System.Collections.ICollection)this.hsStockState.Values).CopyTo(stockStateCollection, 0);
                    this.cmbState.DataSource = stockStateCollection;

                    if (this.hsStockState.ContainsKey(this.state))
                        this.cmbState.Text = this.hsStockState[this.state].ToString();

                    break;
            }           
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            Neusoft.NFC.Management.DataBaseManger dataManager = new Neusoft.NFC.Management.DataBaseManger();
            DateTime sysTime = dataManager.GetDateTimeFromSysDateTime();

            if (this.rbOut.Checked)
                this.dtpBegin.Value = sysTime.AddDays(-this.outIntervalDays);
            if (this.rbIn.Checked)
                this.dtpBegin.Value = sysTime.AddDays(-this.inIntervalDays);
            if (this.rbStock.Checked)
                this.dtpBegin.Value = sysTime.AddDays(-this.stockIntervalDays);

            this.dtpEnd.Value = sysTime;

            #region ���������/�ɹ�״̬��

            this.hsInOutState = new System.Collections.Hashtable();
            this.hsInOutState.Add("0", "����");
            this.hsInOutState.Add("1", "����");
            this.hsInOutState.Add("2", "��׼");

            this.hsStockState = new System.Collections.Hashtable();
            this.hsStockState.Add("0", "�ƻ�");
            this.hsStockState.Add("1", "�ɹ�");
            this.hsStockState.Add("2", "���");
            this.hsStockState.Add("3", "���");

            #endregion

            this.IsShowTypeCheck = false;

            this.Clear();
        }

        /// <summary>
        /// �趨Fp����ʾ
        /// </summary>
        /// <param name="fpLabel">��������ʾ</param>
        /// <param name="fpVisible">��Ч����ʾ</param>
        /// <param name="fpWidth">�п��</param>
        public void InitFp(string[] fpLabel, bool[] fpVisible, float[] fpWidth)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (fpLabel.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Label = fpLabel[i];
                if (fpVisible.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Visible = fpVisible[i];
                if (fpWidth.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Width = fpWidth[i];
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public virtual void Query()
        {
            this.Clear();
            
            if (this.rbIn.Checked)
            {
                this.QueryIn();
                return;
            }
            if (this.rbOut.Checked)
            {
                this.QueryOut();
                return;
            }
            if (this.rbStock.Checked)
            {
                this.QueryStock();
                return;
            }
        }

        /// <summary>
        /// ��ⵥ�ݲ�ѯ
        /// </summary>
        protected virtual void QueryIn()
        {
         
        }

        /// <summary>
        /// ���ⵥ�ݲ�ѯ
        /// </summary>
        protected virtual void QueryOut()
        {
        
        }

        /// <summary>
        /// �ɹ����ݲ�ѯ
        /// </summary>
        protected virtual void QueryStock()
        {
           
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.class2Priv)
            {
                case "0310":            //���
                case "0320":            //����

                    foreach(string strInOutState in this.hsInOutState.Keys)
                    {
                        if ((this.hsInOutState[strInOutState] as string) == this.cmbState.Text)
                        {
                            this.state = strInOutState;
                            return;
                        }
                    }

                    break;
                case "0312":            //�ɹ�

                    foreach (string strStockState in this.hsStockState.Keys)
                    {
                        if ((this.hsStockState[strStockState] as string) == this.cmbState.Text)
                        {
                            this.state = strStockState;
                            return;
                        }
                    }

                    break;
            }           
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
                return;

            if (this.SelecctListEvent != null)
            {
                string listCode = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColList].Text;
                Neusoft.NFC.Object.NeuObject company = new Neusoft.NFC.Object.NeuObject();
                company.ID = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColTargetID].Text;
                company.Name = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColTargetName].Text;

                this.SelecctListEvent(listCode, this.State, company);
            }

            this.ParentForm.Close();

        }


        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Query();
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ParentForm.Close();
        }


        /// <summary>
        /// ������
        /// </summary>
        protected enum ColumnSet
        {
            /// <summary>
            /// ���ݺ�
            /// </summary>
            ColList,
            /// <summary>
            /// �ͻ�����
            /// </summary>
            ColDeliveryNO,
            /// <summary>
            /// ����
            /// </summary>
            ColType,
            /// <summary>
            /// Ŀ�굥λ
            /// </summary>
            ColTargetName,
            /// <summary>
            /// Ŀ�굥λ����
            /// </summary>
            ColTargetID
        }       
    }


}
