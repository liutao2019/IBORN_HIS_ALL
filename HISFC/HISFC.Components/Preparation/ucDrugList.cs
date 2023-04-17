using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.Preparation
{
    /// <summary>
    /// ҩƷ�б�ؼ�
    /// 
    /// ��ʱ������ʾͣ�ù���
    /// </summary>
    public partial class ucDrugList : UserControl
    {
        public ucDrugList()
        {
            InitializeComponent();

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            //��ʱ����CheckBox
            this.showStopCk.Enabled = false;
            this.showStopCk.Checked = false;
        }

        public delegate void ChooseDataHandler(FarPoint.Win.Spread.SheetView sv,int activeRow);

        /// <summary>
        /// ˫����س�ѡ��ʱ���� ���ز���Fp��Row
        /// </summary>
        public event ChooseDataHandler ChooseDataEvent;

        /// <summary>
        /// ����رհ�ť
        /// </summary>
        public event System.EventHandler CloseClickEvent;

        #region �����

        /// <summary>
        /// Fp����ԴDataSet
        /// </summary>
        private DataTable dt = null;
        /// <summary>
        /// Fp����Դ���ɵ�DataView
        /// </summary>
        private DataView dv = null;
        /// <summary>
        /// ����ʾ�������б�
        /// </summary>
        private TreeView tvList = null;

        /// <summary>
        /// �Ƿ���ʾ�߼�����ѡ��
        /// </summary>
        private bool showAdvanceFilter = false;

        /// <summary>
        /// �߼�������ʾ��� 0 ��ǰδ��ʾ�߼������� 1 ��ǰ��ʾ�߼�������
        /// </summary>
        private int advanceFilterFlag = 0;

        /// <summary>
        /// �Ƿ������������ѡ����Ŀ
        /// </summary>
        private bool isUseNumChooseData = false;

        /// <summary>
        /// �����ֶ�����
        /// </summary>
        private string[] filterField = null;

        #endregion

        #region ����

        /// <summary>
        /// ��������ʾ�ı�
        /// </summary>
        [Description("�ϲ���������ʾ�ı� ��ShowCatpion����ΪTrueʱ����Ч"),Category("����"),DefaultValue("ҩƷѡ��")]
        public string Caption
        {
            get
            {
                return this.captionLabel.Text;
            }
            set
            {
                this.captionLabel.Text = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾCaption������
        /// </summary>
        [Description("�Ƿ���ʾ������"),Category("����"),DefaultValue(true)]
        public bool ShowCaption
        {
            get
            {
                return this.groupBox1.Visible;
            }
            set
            {
                this.groupBox1.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�رհ�ť
        /// </summary>
        [Description("�Ƿ���ʾ�رհ�ť"),Category("����"),DefaultValue(true)]
        public bool ShowCloseButton
        {
            get
            {
                return this.closeButton.Visible;
            }
            set
            {
                this.closeButton.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾͣ��ҩƷ
        /// </summary>
        [Description("��ʾ�б�ΪҩƷʱ �Ƿ���ʾͣ��ҩƷ"),Category("����"),DefaultValue(true)]
        public bool ShowStop
        {
            get
            {
                return this.showStopCk.Checked;
            }
            set
            {
                this.showStopCk.Checked = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�߼�����ѡ��
        /// </summary>
        [Description("����ʱ �Ƿ���ʾ�߼�����ѡ�� ���ҽ�����ʾ�б�����ΪҩƷʱ����Ч"),Category("����"),DefaultValue(false)]
        public bool ShowAdvanceFilter
        {
            get
            {
                return this.showAdvanceFilter;
            }
            set
            {
                if (value)
                {
                    if (!this.showAdvanceFilter)
                    {
                        this.advanceFilterFlag = 1;
                        this.panelFilter.Height = 105;
                    }
                }
                else
                {
                    if (this.showAdvanceFilter)
                    {
                        this.advanceFilterFlag = 0;
                        this.panelFilter.Height = 30;
                    }
                }
                this.showAdvanceFilter = value;
                this.lnbAdvanceFilter.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ������������ѡ����Ŀ
        /// </summary>
        [Description("�Ƿ������������(������)ѡ����Ŀ"), Category("����"), DefaultValue(false)]
        public bool IsUseNumChooseData
        {
            get
            {
                return this.isUseNumChooseData;
            }
            set
            {
                this.isUseNumChooseData = value;
            }
        }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string[] FilterField
        {
            get
            {
                return this.filterField;
            }
            set
            {
                this.filterField = value;
            }
        }

        #endregion

        #region ������

        /// <summary>
        /// �Ƿ���ʾ�����б�
        /// </summary>
        [Description("�Ƿ���ʾ�����б�"), Category("����")]
        public bool ShowTreeView
        {
            get
            {
                return !this.neuSpread1.Visible;
            }
            set
            {
                if (this.tvList != null && value)
                {
                    this.neuSpread1.Visible = false;
                    this.panelFilter.Visible = false;
                    this.tvList.Dock = DockStyle.Fill;
                    this.panelList.Controls.Add(this.tvList);
                }
                else
                {
                    this.neuSpread1.Visible = true;
                    this.panelFilter.Visible = true;
                }
            }
        }

        /// <summary>
        /// ��ʾ�������б�
        /// </summary>
        public TreeView TreeView
        {
            set
            {
                this.tvList = value;
            }
        }

        #endregion

        #region Fp����

        /// <summary>
        /// �Ƿ���ʾFp�б���
        /// </summary>
        [Description("��ʾ�б�ʱ �Ƿ���ʾ�б���"), Category("����"), DefaultValue(true)]
        public bool ShowFpRowHeader
        {
            get
            {
                return this.neuSpread1_Sheet1.RowHeader.Visible;
            }
            set
            {
                this.neuSpread1_Sheet1.RowHeader.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ����DataSet����Դ����������
        /// </summary>
        [Description("�Ƿ����DataSet����Դ����Fp��Ԫ������"),Category("����"),DefaultValue(true)]
        public bool DataAutoCellType
        {
            get
            {
                return this.neuSpread1_Sheet1.DataAutoCellTypes;
            }
            set
            {
                this.neuSpread1_Sheet1.DataAutoCellTypes = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��DataSet����Դ�ڵ��б���
        /// </summary>
        [Description("�Ƿ����DataSet����Դ����Fp��Ԫ�б���"),Category("����"),DefaultValue(true)]
        public bool DataAutoHeading
        {
            get
            {
                return this.neuSpread1_Sheet1.DataAutoHeadings;
            }
            set
            {
                this.neuSpread1_Sheet1.DataAutoHeadings = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��DataSet����Դ�ڵ��п��
        /// </summary>
        [Description("�Ƿ����DataSet����Դ����Fp�п��"),Category("����"),DefaultValue(true)]
        public bool DataAutoWidth
        {
            get
            {
                return this.neuSpread1_Sheet1.DataAutoSizeColumns;
            }
            set
            {
                this.neuSpread1_Sheet1.DataAutoSizeColumns = value;
            }
        }

        /// <summary>
        /// Fp����Դ
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                return this.dt;
            }
            set
            {
                this.dt = value;
                if( value != null )
                {
                    this.dv = new DataView( this.dt );

                    this.neuSpread1_Sheet1.DataSource = this.dv;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Filter()
        {
            string filter = " ";
            if (this.filterField != null)      //��ʹ��Ĭ�Ϲ����ֶ�
            {
                #region ʹ���Զ�������ֶ�
                if (this.filterField.Length == 0)
                    return;

                if (this.ckBlurFilter.Checked)
                    filter = "(" + this.filterField[0] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                else
                    filter = "(" + this.filterField[0] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";
                for (int i = 1; i < this.filterField.Length; i++)
                {
                    if (this.ckBlurFilter.Checked)
                        filter += "OR (" + this.filterField[i] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                    else
                        filter += "OR (" + this.filterField[i] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";

                }
                #endregion
            }
            else                                //ʹ��Ĭ�Ϲ����ֶ�
            {
                if (this.showAdvanceFilter)
                {
                    switch (this.cmbFilterField.Text)
                    {
                        #region ���ù����ַ���
                        case "ȫ��":
                            if (this.ckBlurFilter.Checked)
                                filter = string.Format("ƴ���� LIKE '%{0}%' OR ����� LIKE '%{1}%' OR �Զ����� LIKE '%{2}%' OR ��Ʒ���� LIKE '%{3}%'", this.txtQueryCode.Text.Trim(),
                                this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim());
                            else
                                filter = string.Format("ƴ���� LIKE '%{0}' OR ����� LIKE '%{1}' OR �Զ����� LIKE '%{2}' OR ��Ʒ���� LIKE '%{3}'", this.txtQueryCode.Text.Trim(),
                                this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim());
                            break;
                        case "ƴ����":
                            if (this.ckBlurFilter.Checked)
                                filter = "(ƴ���� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                            else
                                filter = "(ƴ���� LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";
                            break;
                        case "�����":
                            if (this.ckBlurFilter.Checked)
                                filter = "(����� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                            else
                                filter = "(����� LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";
                            break;
                        case "�Զ�����":
                            if (this.ckBlurFilter.Checked)
                                filter = "(�Զ����� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                            else
                                filter = "(�Զ����� LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";
                            break;
                        case "��Ʒ����":
                            if (this.ckBlurFilter.Checked)
                                filter = "(��Ʒ���� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                            else
                                filter = "(��Ʒ���� LIKE '%" + this.txtQueryCode.Text.Trim() + "' )";
                            break;
                        #endregion
                    }
                }
                else
                {
                    #region ʹ��Ĭ�Ϲ����ֶν��й���
                    if (this.ckBlurFilter.Checked)
                        filter = string.Format("ƴ���� LIKE '%{0}%' OR ����� LIKE '%{1}%' OR �Զ����� LIKE '%{2}%' OR ��Ʒ���� LIKE '%{3}%'", this.txtQueryCode.Text.Trim(),
                        this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim());
                    else
                        filter = string.Format("ƴ���� LIKE '%{0}' OR ����� LIKE '%{1}' OR �Զ����� LIKE '%{2}' OR ��Ʒ���� LIKE '%{3}'", this.txtQueryCode.Text.Trim(),
                        this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim(), this.txtQueryCode.Text.Trim());
                    #endregion
                }
            }
            //���ù�������
            this.dv.RowFilter = filter;
            this.neuSpread1_Sheet1.ActiveRowIndex = 0;

        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        /// <param name="label">�б���</param>
        /// <param name="width">���</param>
        /// <param name="visible">�Ƿ���ʾ</param>
        public virtual void SetFormat(string[] label, int[] width, bool[] visible)
        {
            if (label != null && label.Length > 0)
                this.neuSpread1_Sheet1.DataAutoHeadings = false;
            if (width != null && visible.Length > 0)
                this.neuSpread1_Sheet1.DataAutoSizeColumns = false;

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (label != null && label.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Label = label[i];
                if (width != null && width.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Width = width[i];
                if (visible != null && visible.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Visible = visible[i];
            }
        }

        /// <summary>
        /// ��ʾҩƷ�б�  ����ʾҩ��ͣ�õ�ҩƷ ��ҩ���Ƿ�ͣ���޹�
        /// </summary>
        public virtual void ShowPharmacyList(string drugType)
        {
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ���ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                Neusoft.NFC.Management.DataBaseManger databaseManager = new Neusoft.NFC.Management.DataBaseManger();
                DataSet dataSet = new DataSet();
                string[] sqlIndex = new string[2] { "Pharmacy.Item.ValibInfo", "Preparation.Item.GetList.QueryByType" };

                databaseManager.ExecQuery(sqlIndex, ref dataSet, drugType);
                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }
                this.filterField = new string[7]{"SPELL_CODE","WB_CODE","REGULAR_NAME","REGULAR_SPELL",
														"REGULAR_WB","CUSTOM_CODE","TRADE_NAME"};
                this.DataTable = dataSet.Tables[0];

                this.SetpharmacyFormat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            }
        }
        
        /// <summary>
        /// ����ҩƷ��ʽ��
        /// </summary>
        private void SetpharmacyFormat()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��Ʒ����";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 150F;
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(2).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(3).Label = "���ۼ�";
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��װ��λ";
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "��װ����";
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = true;

            for (int i = 6; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Visible = false;
            }

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "ͨ����";
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "ͨ�����Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(12).Visible = false;
        }

        /// <summary>
        /// ��ʾ���ҩƷ�б�  ����ʾҩ��ͣ�õ�ҩƷ ��ҩ���Ƿ�ͣ���޹�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        public virtual void ShowDeptStorage(string deptCode, bool isBatch)
        {
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ������ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                Neusoft.NFC.Management.DataBaseManger databaseManager = new Neusoft.NFC.Management.DataBaseManger();
                DataSet dataSet = new DataSet();
                string sqlIndex;
                if (isBatch)
                    sqlIndex = "Pharmacy.Item.GetStorageListByBatch";
                else
                    sqlIndex = "Pharmacy.Item.GetStorageListNoBatch";

                databaseManager.ExecQuery(sqlIndex, ref dataSet, deptCode);
                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }
                this.filterField = new string[3] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" };
                this.DataTable = dataSet.Tables[0];

                //ʹ�ÿؼ�Ĭ�ϵ���ʾ��ʽ
                this.SetFormatForStorage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            }
        }
        
        /// <summary>
        /// ��ʾ���ҩƷʱ���и�ʽ��
        /// </summary>
        private void SetFormatForStorage()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��Ʒ����";
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 150F;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(2).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(3).Label = "����";
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��λ��";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 58F;
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;
        }

        /// <summary>
        /// ������������Ҫ���б�
        /// </summary>
        /// <param name="sqlIndex">������SQL�����XML�е�����</param>
        /// <param name="filterField">���ڶԸ��б�������ֶΣ���SQL��������������ѡ��</param>
        /// <param name="formatStr">��SQL�����и�ʽ��������</param>
        public void ShowInfoList(string sqlIndex, string[] filterField, params string[] formatStr)
        {
            string[] sqlIndexex = { sqlIndex };
            this.ShowInfoList(sqlIndexex, filterField, formatStr);
        }
       
        /// <summary>
        /// ������������Ҫ���б�
        /// </summary>
        /// <param name="sqlIndex">������SQL�����XML�е��������� ��һλΪselect���� ����Ϊwhere����</param>
        /// <param name="filterField">���ڶԸ��б���м������ֶΣ���sql��������������ѡ��</param>
        /// <param name="formatStr">��sql�����и�ʽ��������</param>
        public void ShowInfoList(string[] sqlIndex, string[] filterField, params string[] formatStr)
        {
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�.....");
            Application.DoEvents();

            try
            {
                this.DataAutoCellType = false;
                this.DataAutoHeading = false;
                this.DataAutoWidth = false;

                Neusoft.NFC.Management.DataBaseManger databaseManager = new Neusoft.NFC.Management.DataBaseManger();
                DataSet dataSet = new DataSet();
                databaseManager.ExecQuery(sqlIndex, ref dataSet, formatStr);
                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }
                this.DataTable = dataSet.Tables[0];
                this.FilterField = filterField;
            }
            catch (Exception ex)
            {
                MessageBox.Show("�����б����ݷ�������\n" + ex.Message);
            }
            finally
            {
                Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��ȡ��ʾ���ݵĵ�һ�е�ָ���п��
        /// </summary>
        /// <param name="columnNum">������������</param>
        /// <param name="width">���صĿ��</param>
        public void GetColumnWidth(int columnNum, ref int width)
        {
            int iNum = 0;
            width = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Visible)
                {
                    width = width + (int)this.neuSpread1_Sheet1.Columns[i].Width;
                    iNum = iNum + 1;
                    if (iNum > columnNum - 1)
                        break;
                }
            }
        }

        /// <summary>
        /// ���ù��˿�Ϊ����ȫѡ״̬
        /// </summary>
        public void SetFocusSelect()
        {
            this.Select();
            this.txtQueryCode.Select();
            this.txtQueryCode.Focus();
            this.txtQueryCode.SelectAll();
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            try
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;

                if (this.dt != null)
                    this.dt.Clear();                
            }
            catch(Exception ex)
            {
                MessageBox.Show(Neusoft.NFC.Management.Language.Msg(ex.Message));
            }
        }

        private void lnbAdvanceFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.advanceFilterFlag == 0)            //δ��ʾ�߼�������
            {
                this.panelFilter.Height = 105;
                this.advanceFilterFlag = 1;
            }
            else                                       //����ʾ�߼�������
            {
                this.panelFilter.Height = 30;
                this.advanceFilterFlag = 0;
            }
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            if (this.chkRealTimeFilter.Checked)
                this.Filter();
        }

        private void txtQueryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.chkRealTimeFilter.Checked)
                {
                    this.Filter();
                }
                else
                {
                    if (this.ChooseDataEvent != null && this.neuSpread1_Sheet1.Rows.Count > 0)
                    {
                        this.ChooseDataEvent(this.neuSpread1_Sheet1,this.neuSpread1_Sheet1.ActiveRowIndex);
                    }
                }
            }
            if (this.isUseNumChooseData && char.IsDigit(((char)e.KeyCode)))
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                {
                    return;
                }

                if ((Neusoft.NFC.Function.NConvert.ToInt32((char)e.KeyCode) - 48) <= this.neuSpread1_Sheet1.Rows.Count)
                {
                    if (this.ChooseDataEvent != null)
                    {
                        this.ChooseDataEvent(this.neuSpread1_Sheet1,Neusoft.NFC.Function.NConvert.ToInt32((char)e.KeyCode) - 49);
                    }
                    e.Handled = true;
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (this.CloseClickEvent != null)
            {
                this.CloseClickEvent(null, System.EventArgs.Empty);
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ChooseDataEvent != null)
            {
                this.ChooseDataEvent(this.neuSpread1_Sheet1,e.Row);
            }
        }
    }
}
