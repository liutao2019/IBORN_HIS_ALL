using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ҩƷ�б�ؼ�
    /// 
    /// ��ʱ������ʾͣ�ù���
    /// <�޸ļ�¼>
    ///    1.����Farpoint��Reset���������������ҩƷ�б�ı���ɫ������header����ɫ by Sunjh 2010-9-10 {15AA72F9-C0F6-41b0-B650-296AC3B913BF}
    ///    2.���ص�ǰѡ���ҩƷ���� by Sunjh 2010-9-26 {96CB270E-EBA7-4330-BBC5-9E430F5650F5}
    ///    3.�Ѵ����������ӵ�fp�� by Sunjh 2010-10-1 {1A398A34-0718-47ed-AAE9-36336430265E}
    ///    4.ҩƷѡ���б������Ч����ʾ by Sunjh 2010-10-27 {018DFF45-0D2F-42a5-B049-1D751275A142}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucDrugList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugList()
        {
            InitializeComponent();

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
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
        /// �Ƿ������������ѡ����Ŀ
        /// </summary>
        private bool isUseNumChooseData = false;

        /// <summary>
        /// �����ֶ�����
        /// </summary>
        protected    string[] filterField = null;

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
        /// �Ƿ���ʾ�߼�����ѡ��
        /// </summary>
        [Description("����ʱ �Ƿ�����ͨ��ҩƷ������ ���ҽ�����ʾ�б�����ΪҩƷʱ����Ч"),Category("����"),DefaultValue(false)]
        public bool ShowAdvanceFilter
        {
            get
            {
                return this.showAdvanceFilter;
            }
            set
            {
                this.showAdvanceFilter = value;
                this.cmbItemType.Visible = value;
                this.lbItemType.Visible = value;
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

        /// <summary>
        /// ���ص�ǰѡ���ҩƷ���� by Sunjh 2010-9-26 {96CB270E-EBA7-4330-BBC5-9E430F5650F5}
        /// </summary>
        public string CurrDrugCode
        {
            get 
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
                {
                    return this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
                }
                else
                {
                    return "";
                }
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

        /// <summary>
        /// ��ǰSheetView
        /// {F42D8B9A-8836-4f84-8379-A71FB3A626E5} 20100524
        /// </summary>
        public FarPoint.Win.Spread.SheetView CurrentSheetView
        {
            get
            {
                return this.neuSpread1_Sheet1;
            }
        }
        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            System.Collections.ArrayList alItemType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alItemType != null)
            {
                this.cmbItemType.AddItems(alItemType);
            }
            //{1DED4697-A590-47b3-B727-92A4AA05D2ED} �����б���ʾʱ��������
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

        }

        protected virtual void FarPointReset()
        {
            //����Farpoint��Reset���������������ҩƷ�б�ı���ɫ������header����ɫ by Sunjh 2010-9-10 {15AA72F9-C0F6-41b0-B650-296AC3B913BF}
            //this.neuSpread1_Sheet1.Reset();            

            this.neuSpread1.BackColor = System.Drawing.Color.White;

            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 37F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;

            //{1DED4697-A590-47b3-B727-92A4AA05D2ED} �����б���ʾʱ��������
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Filter()
        {
            string filter = " ";
            #region ���������ַ�{7CF023E5-BB8D-4f85-A4AA-7AB7D457454C}

            string queryString = this.txtQueryCode.Text.Trim();

            if (queryString.Contains("%") == true)
            {
                queryString = queryString.Replace("%", "[%]");
            }

            #endregion
            if (this.filterField != null)      //��ʹ��Ĭ�Ϲ����ֶ�
            {
                #region ʹ���Զ�������ֶ�
                if (this.filterField.Length == 0)
                    return;

                filter = "(" + this.filterField[0] + " LIKE '%" + queryString + "%' )";

                for (int i = 1; i < this.filterField.Length; i++)
                {
                    filter += "OR (" + this.filterField[i] + " LIKE '%" + queryString + "%' )";

                }
                #endregion
            }
            else                                //ʹ��Ĭ�Ϲ����ֶ�
            {
                #region ʹ��Ĭ�Ϲ����ֶν��й���

                filter = string.Format("ƴ���� LIKE '%{0}%' OR ����� LIKE '%{1}%' OR �Զ����� LIKE '%{2}%' OR ��Ʒ���� LIKE '%{3}%'", queryString,
                queryString, queryString, queryString);
                
                #endregion
            }

            //���ù�������
            if (this.dv != null)
            {
                this.dv.RowFilter = filter;
            }
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
            if (width != null && visible != null && visible.Length > 0)
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
        public virtual void ShowPharmacyList()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
                DataSet dataSet = new DataSet();

                this.ShowAdvanceFilter = true;

                //string[] sqlIndex = new string[2] { "Pharmacy.Item.ValibInfo", "Pharmacy.Item.GetAvailableList.Where" };
                string[] sqlIndex = new string[1] { "Pharmacy.Item.ValibInfo" };

                string itemType = "A";
                if (this.cmbItemType.Tag != null && this.cmbItemType.Text != "")
                {
                    itemType = this.cmbItemType.Tag.ToString();
                }
                databaseManager.ExecQuery(sqlIndex, ref dataSet,itemType);

                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }
                this.filterField = new string[13]{"ҩƷ����","ͨ����","ƴ��","���","�Զ�����","ͨ����ƴ��","����","����ƴ��",
														"ͨ�������","ѧ��","ѧ��ƴ��","ѧ�����","�������"};
                this.DataAutoHeading = true;
                this.DataAutoWidth = false;
                int[] widthCollect = new int[6] { 10,120,100,60,40,40};
                bool[] visibleCollect = new bool[19] {false,true,true,true,true,false,false,false,false,true,false,false,true,false,false,true,false,false,false };
                if (dataSet.Tables.Count > 0)
                {
                    this.DataTable = dataSet.Tables[0];                  
                }

                this.SetFormat(null, widthCollect, visibleCollect);
                //{3FF156FD-6AB7-4468-9BA6-69F2E143AF3C}
                for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
                {
                    if (this.neuSpread1_Sheet1.Columns[i].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                    {
                        FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        numCellType.DecimalPlaces = 4;
                        this.neuSpread1_Sheet1.Columns[i].CellType = numCellType;
                    }
                }
               
                //this.SetpharmacyFormat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
        /// <param name="showStorageFlag">�Ƿ���ʾ���</param>
        public virtual void ShowDeptStorage(string deptCode, bool isBatch, int showStorageFlag)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                this.FarPointReset();
                FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
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

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Contains("custom_code"))
                {
                    this.filterField = new string[] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" ,"REGULAR_SPELL",
                                                "REGULAR_WB","OTHER_SPELL","OTHER_WB","FORMAL_SPELL","FORMAL_WB","custom_code"};
                }
                else
                {
                    this.filterField = new string[] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" ,"REGULAR_SPELL",
                                                "REGULAR_WB","OTHER_SPELL","OTHER_WB","FORMAL_SPELL","FORMAL_WB"};
                }

                this.DataAutoWidth = false;

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(dr[14]) != 0)
                        {
                            dr[5] = Math.Round(FS.FrameWork.Function.NConvert.ToDecimal(dr[5]) / FS.FrameWork.Function.NConvert.ToDecimal(dr[14]), 2);
                        }
                    }
                    //{28017834-5C98-4d3c-A8CD-7BE2F95C6D74} ���˲���ʾ0���ҩƷ
                    System.Data.DataView view = dataSet.Tables[0].DefaultView;
                    view.RowFilter = "STORE_SUM > 0";
                    this.DataTable = view.ToTable();
                }

                //ʹ�ÿؼ�Ĭ�ϵ���ʾ��ʽ
                this.SetFormatForStorage(showStorageFlag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��ʾ���ҩƷ�б�  ����ʾҩ��ͣ�õ�ҩƷ ��ҩ���Ƿ�ͣ���޹�
        /// ���������̵���Ϣ{98F0BF7A-5F41-4de3-884F-B38E71B41A8C}
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="isBatch">�Ƿ����Ź���</param>
        /// <param name="showStorageFlag">�Ƿ���ʾ���</param>
        public virtual void ShowDeptStorageWithSpecialCheck(string deptCode, bool isBatch, int showStorageFlag)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                this.FarPointReset();
                FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
                DataSet dataSet = new DataSet();
                string sqlIndex;
                if (isBatch)
                    sqlIndex = "Pharmacy.Item.GetStorageListByBatch";
                else
                    sqlIndex = "Pharmacy.Item.GetStorageListWithSpecialCheck";

                databaseManager.ExecQuery(sqlIndex, ref dataSet, deptCode);
                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }

                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Contains("custom_code"))
                {
                    this.filterField = new string[] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" ,"REGULAR_SPELL",
                                                "REGULAR_WB","OTHER_SPELL","OTHER_WB","FORMAL_SPELL","FORMAL_WB","custom_code"};
                }
                else
                {
                    this.filterField = new string[] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" ,"REGULAR_SPELL",
                                                "REGULAR_WB","OTHER_SPELL","OTHER_WB","FORMAL_SPELL","FORMAL_WB"};
                }

                this.DataAutoWidth = false;

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(dr[14]) != 0)
                        {
                            dr[5] = Math.Round(FS.FrameWork.Function.NConvert.ToDecimal(dr[5]) / FS.FrameWork.Function.NConvert.ToDecimal(dr[14]), 2);
                        }
                    }
                    //{28017834-5C98-4d3c-A8CD-7BE2F95C6D74} ���˲���ʾ0���ҩƷ
                    System.Data.DataView view = dataSet.Tables[0].DefaultView;
                    view.RowFilter = "STORE_SUM > 0";
                    this.DataTable = view.ToTable();
                }

                //ʹ�ÿؼ�Ĭ�ϵ���ʾ��ʽ
                this.SetFormatForStorage(showStorageFlag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��ʾ���ҩƷ�б� ���ݲ��������Ƿ���ʾ�����ͣ�õ�ҩƷ 
        /// {D06724D9-C415-4a6b-8E93-0FF175CB7A8A} 20091230
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="isBatch">�Ƿ�������ʾ</param>
        /// <param name="isStoreInvalid">�Ƿ������Ч</param>
        public virtual void ShowDeptStorageAndDict(string deptCode, bool isBatch, bool isFilterStoreInvalid)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ҩƷ��Ϣ...");
            Application.DoEvents();
            try
            {
                this.FarPointReset();

                FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
                DataSet dataSet = new DataSet();
                string sqlIndex;
                if (isBatch)
                {
                    if (isFilterStoreInvalid)
                    {
                        sqlIndex = "Pharmacy.Item.QueryDrugList.StoreAndDic.ByBatch";
                    }
                    else
                    {
                        sqlIndex = "Pharmacy.Item.QueryDrugList.StoreAndDic.ByBatch.AllState";
                    }
                }
                else
                {
                    if (isFilterStoreInvalid)
                    {
                        sqlIndex = "Pharmacy.Item.QueryDrugList.StoreAndDic.NotByBatch";
                    }
                    else
                    {
                        sqlIndex = "Pharmacy.Item.QueryDrugList.StoreAndDic.NotByBatch.AllState";
                    }
                }

                databaseManager.ExecQuery(sqlIndex, ref dataSet, deptCode, "ALL");
                if (dataSet == null)
                {
                    MessageBox.Show("�����б����ݷ�������\n" + databaseManager.Err);
                    return;
                }

                this.filterField = new string[] { "ҩƷ����", "��Ʒ��ƴ����", "��Ʒ�������", "ͨ����ƴ����", "ͨ���������" };

                this.DataAutoHeading = false;
                this.DataAutoWidth = false;
                this.DataAutoCellType = false;

                if (dataSet.Tables.Count > 0)
                {
                    this.DataTable = dataSet.Tables[0];
                }

                //ʹ�ÿؼ�Ĭ�ϵ���ʾ��ʽ
                this.SetFormatForStorageAndDict(FS.FrameWork.Function.NConvert.ToInt32(isBatch));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
        
        /// <summary>
        /// ��ʾ���ҩƷʱ���и�ʽ��
        /// {D06724D9-C415-4a6b-8E93-0FF175CB7A8A} 20091230
        /// </summary>
        private void SetFormatForStorage(int isShowStorage)
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
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 60F;
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��λ��";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 57F;
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 58F;
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = FS.FrameWork.Function.NConvert.ToBoolean(isShowStorage);

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(15).Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(15).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(12).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(14).Visible = false;
            //ҩƷѡ���б������Ч����ʾ by Sunjh 2010-10-27 {018DFF45-0D2F-42a5-B049-1D751275A142}
            if (this.neuSpread1_Sheet1.Columns.Count > 16)
            {
                this.neuSpread1_Sheet1.Columns.Get( 16 ).Label = "��Ч��";
                this.neuSpread1_Sheet1.Columns.Get( 16 ).Width = 70F;
                this.neuSpread1_Sheet1.Columns.Get( 16 ).Visible = true;
            }
        }

        /// <summary>
        /// ��ʾ���ҩƷʱ���и�ʽ��
        /// {D06724D9-C415-4a6b-8E93-0FF175CB7A8A} 20091230
        /// </summary>
        private void SetFormatForStorageAndDict(int showBatchNO)
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "ҩƷ����";
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 160F;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 80F;
            this.neuSpread1_Sheet1.Columns.Get(2).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(3).Label = "����";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 70F;
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = FS.FrameWork.Function.NConvert.ToBoolean(showBatchNO);

            FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateTimeCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();
            markDateTimeCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDate;
            this.neuSpread1_Sheet1.Columns.Get(4).CellType = markDateTimeCellType;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��Ч��";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 66F;
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 70F;
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            markNumCellType.DecimalPlaces = 3;
            this.neuSpread1_Sheet1.Columns[5].CellType = markNumCellType;
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "��λ";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 35F;
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(7).Label = "����";
            this.neuSpread1_Sheet1.Columns.Get(7).Width = 110F;
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = true;

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCellType2 = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            markNumCellType2.DecimalPlaces = 2;
            this.neuSpread1_Sheet1.Columns[8].CellType = markNumCellType2;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "����";
            this.neuSpread1_Sheet1.Columns.Get(8).Width = 60F;
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = true;

            this.neuSpread1_Sheet1.Columns[9].CellType = markNumCellType2;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "���ۼ�";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 60F;
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(10).Label = "ҽ��";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 39F;
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(11).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(12).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(13).Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(13).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(14).Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns.Get(14).Visible = false;
        }

        /// <summary>
        /// ������������Ҫ���б�
        /// </summary>
        /// <param name="sqlIndex">������SQL�����XML�е�����</param>
        /// <param name="filterField">���ڶԸ��б�������ֶΣ���SQL��������������ѡ��</param>
        /// <param name="formatStr">��SQL�����и�ʽ��������</param>
        public void ShowInfoList(string sqlIndex, string[] filterField, params string[] formatStr)
        {
            this.FarPointReset();
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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�.....");
            Application.DoEvents();

            try
            {
                this.FarPointReset();
                this.DataAutoCellType = false;
                this.DataAutoHeading = false;
                this.DataAutoWidth = false;
                //{1DED4697-A590-47b3-B727-92A4AA05D2ED} �����б���ʾʱ��������
                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

                FS.FrameWork.Management.DataBaseManger databaseManager = new FS.FrameWork.Management.DataBaseManger();
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
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// �Ѵ����������ӵ�fp�� by Sunjh 2010-10-1 {1A398A34-0718-47ed-AAE9-36336430265E}
        /// </summary>
        /// <param name="al">al�е�ʵ��ΪFS.HISFC.Object.Pharmacy.Item</param>
        public void AddDataToFP(DataTable dTable)
        {
            this.neuSpread1_Sheet1.Rows.Count = dTable.Rows.Count;
            this.DataTable = dTable;
            this.neuSpread1_Sheet1.Columns[0].Visible = false;//��ҩƷ���������أ�{62AAA983-5F51-4786-BAF9-FB032B84A23D}

        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void txtQueryCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 1);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ChooseDataEvent != null && this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    this.ChooseDataEvent(this.neuSpread1_Sheet1, this.neuSpread1_Sheet1.ActiveRowIndex);
                }
            }
            if (this.isUseNumChooseData && char.IsDigit(((char)e.KeyCode)))
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                {
                    return;
                }

                if ((FS.FrameWork.Function.NConvert.ToInt32((char)e.KeyCode) - 48) <= this.neuSpread1_Sheet1.Rows.Count)
                {
                    if (this.ChooseDataEvent != null)
                    {
                        this.ChooseDataEvent(this.neuSpread1_Sheet1,FS.FrameWork.Function.NConvert.ToInt32((char)e.KeyCode) - 49);
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

        protected virtual  void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbItemType.Text != "")
            {
                this.captionLabel.Text = "ҩƷѡ��" + this.cmbItemType.Text;

                this.ShowPharmacyList();
            }
        }




    }
}
