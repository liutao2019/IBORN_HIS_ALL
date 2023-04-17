using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;


namespace FS.SOC.Local.Pharmacy.ShenZhen.Extend.BinHai
{
    /// <summary>
    /// [��������: ���ݲ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucBillRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillRePrint()
        {
            InitializeComponent();
            this.txtFilter.Click += new EventHandler(txtFilter_Click);
        }

        void txtFilter_Click(object sender, EventArgs e)
        {
            this.txtFilter.SelectAll();
        }        

        #region �����

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private string billtype = "NO";

        /// <summary>
        /// ����״̬
        /// </summary>
        private string billstate = "A";

        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        private bool isprint = false;

        /// <summary>
        /// �������뾫��
        /// </summary>
        private int decimals = 4; 
       
        /// <summary>
        /// ҩƷҵ�������
        /// </summary>
        //FS.SOC.HISFC.BizLogic.Pharmacy.InOut InOutManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ�����ҵ�������
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.InOut inOutMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        /// <summary>
        /// ���ҹ�����
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ���������ͼ
        /// </summary>
        private DataView dvInput = null;

        /// <summary>
        /// ����������ͼ
        /// </summary>
        private DataView dvOutput = null;

        /// <summary>
        /// ��ⵥ��ӡʵ��
        /// </summary>
        protected object inputInstance;

        /// <summary>
        /// ���ⵥ��ӡʵ��
        /// </summary>
        protected object outputInstance;

        /// <summary>
        /// ��ʾ����Ƿ����ӡ����һ��
        /// </summary>
        private bool isShowAsPrint = true;

        #endregion 

    

        #region ����

        /// <summary>
        /// ��������[I��ⵥ O���ⵥ D������]
        /// </summary>
        [Browsable(false)]
        public string BillType
        {
            get
            {
                return this.billtype;
            }
            set
            {
                if (value != this.billtype)
                {
                    this.billtype = value;

                    this.txtFilter.Text = "";

                    this.lbStatus.Visible = false;
                    this.cmbStatus.Visible = false;

                    if (value == "I")
                    {
                        cmbDept.Visible = false;
                        this.SetFpForInput();
                    }
                    else if (value == "O")
                    {
                        cmbDept.Visible = true;
                        
                        this.SetFpForOutput();

                    }
                }
            }
        }
        
        /// <summary>
        /// ����״̬
        /// </summary>
        [Browsable(false)]
        public string BillState
        {
            get
            {
                if (this.billstate == null || this.billstate == "")
                {

                    return "A";
                }
                else
                {

                    return this.billstate;
                }
            }
            set
            {
                this.billstate = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        [Browsable(false)]
        public int ActiveSheet
        {
            get
            {
                return this.neuSpread1.ActiveSheetIndex;
            }
            set
            {
                this.neuSpread1.ActiveSheetIndex = value;
            }
        }
        
        /// <summary>
        /// �Ƿ��ӡ
        /// </summary>
        [Browsable(false)]
        public new bool IsPrint
        {
            get
            {
                return this.isprint;
            }
            set
            {
                this.isprint = value;
            }
        }

        /// <summary>
        /// ���ݿ⾫��
        /// </summary>
        [Description("�����������ʾ����"), Category("����"), DefaultValue(4)]
        public int Decimals
        {
            get
            {
                return decimals;
            }
            set
            {
                decimals = value;
            }
        }

        #region 
        ///// <summary>
        ///// �Ƿ���ʾ�������б�
        ///// </summary>
        //public bool IsShowAttempBill
        //{
        //    get
        //    {
        //        return this.isShowAttempBill;
        //    }
        //    set
        //    {
        //        this.isShowAttempBill = value;
        //    }
        //}
        #endregion 


        /// <summary>
        /// �̵㵥��ʾ����Ƿ����ӡ����һ��
        /// </summary>
        [Description("�̵㵥�Ƿ��ҩƷ������ͳ��"),Category("����"),Browsable(true)]
        public bool IsShowAsPrint
        {
            get 
            {
               return isShowAsPrint ; 
            }
            set
            { 
                isShowAsPrint = value; 
            }
        }
        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ȫ��ѡ", "���ѡ����", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��ѡ, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ȫ��ѡ")
            {
                this.SelectAll(false);
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return 1;
        }
     
        #endregion      

        #region ����

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.InitFp();
            this.neuSpread1.ActiveSheetIndex = 0;

            DateTime dt = this.inOutMgr.GetDateTimeFromSysDateTime();
            this.dtEnd.Value = dt;
            dt = dt.AddDays(-3);
            this.dtBegin.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);

            this.InitStatus();
            this.InitDept();
        }


        private void InitDept()
        {
            ArrayList aldept = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
           
            
            aldept = deptMgr.GetDeptmentAll();

            //�õ������б�
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            aldept.Add(obj);
            cmbDept.AddItems(aldept);
            cmbDept.SelectedIndex = aldept.Count - 1;
        
        
        }

        /// <summary>
        /// ��ʼ��(���)״̬
        /// </summary>
        private void InitStatus()
        {
            try
            {
                //��ʾ���ƣ�ȡIDֵ
                FS.FrameWork.Models.NeuObject ob;
                ArrayList al = new ArrayList();
                this.cmbStatus.Text = "��ѡ��";

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "A";
                ob.Name = "ȫ��";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "2";
                ob.Name = "��׼";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "1";
                ob.Name = "��Ʊ[���]";
                al.Add(ob);

                ob = new FS.FrameWork.Models.NeuObject();
                ob.ID = "0";
                ob.Name = "����";
                al.Add(ob);

                //������Դ��ʼ��
                this.cmbStatus.DataSource = al;
                this.cmbStatus.DisplayMember = "Name";
                this.cmbStatus.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ʼ����ѯ״̬ʧ��>>" + ex.Message));
            }
        }


        /// <summary>
        /// ��ʼ��fp
        /// </summary>
        private void InitFp()
        {
            #region ����
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 2;
            this.neuSpread1_Sheet1.RowCount = 30;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Text = "����˵��";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Text = "һ���Ķ�˵��";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Text = "    1������������˵��ʱ����δ�����κβ��������������ȷ��β������������Ķ�";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Text = "    2�����Ķ�ʱ�����һ�μ�ס��˵�����ݣ������޷�����һ���Ķ�һ�߲���";
            this.neuSpread1_Sheet1.Cells.Get(4, 1).Text = "    3����������Ҫ�鿴��˵��ʱ��ѡ��[����]��ť";
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Text = "��������ѡ��";
            this.neuSpread1_Sheet1.Cells.Get(6, 1).Text = "    1������൥�����������б���ѡ��õ������͡����ң���ɫ��������Ч[���������е���]";
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Font = new System.Drawing.Font("����", 9F);
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Text = "    2��ѡ��ʼ�ͽ���ʱ�䣬����Ĭ��Ϊ4��";
            this.neuSpread1_Sheet1.Cells.Get(8, 1).Text = "    3���������ⵥ��ѡ��״̬������Ĭ��Ϊȫ��";
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Text = "������β�ѯ";
            this.neuSpread1_Sheet1.Cells.Get(10, 1).Text = "    1��ȷ���ڶ�����ɺ���[��ѯ]��ť����ʾѡ�������ڵ����б�";
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Text = "�ġ�Ϊ��ˢ��";
            this.neuSpread1_Sheet1.Cells.Get(12, 1).Text = "    1�������������ʱ������󣬿��������������������ݱ䶯����ʱ��[ˢ��]";
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Text = "�塢�鿴��ϸ";
            this.neuSpread1_Sheet1.Cells.Get(14, 1).Text = "    1����ѯ��ɺ�˫���б�[���ܱ�]��Ŀ����ʾ��ϸ";
            this.neuSpread1_Sheet1.Cells.Get(15, 1).Text = "    2����ⵥӦ��ѡ��[ѡ��]�����򹳺���Ч[����ѡ�л�ȡ��]������Ĭ������������";
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Text = "������δ�ӡ";
            this.neuSpread1_Sheet1.Cells.Get(17, 1).Text = "    1���ڻ��ܱ��е���ѡ��Ҫ��ӡ�ĵ���";
            this.neuSpread1_Sheet1.Cells.Get(18, 1).Text = "    2��ѡ��[��ӡ]��ť���ɴ�ӡ";
            this.neuSpread1_Sheet1.Cells.Get(19, 1).Text = "    3����ⵥ���԰���Ʊ��ӡ��Ӧ��ѡ��[ѡ��]�����򹳺���Ч[����ѡ�л�ȡ��]������Ĭ������������";          
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "����˵��";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "����˵��";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 732F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "����";
            #endregion

            #region ��ϸ
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 15;
            this.neuSpread1_Sheet2.RowCount = 50;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((System.Byte)(206)), ((System.Byte)(93)), ((System.Byte)(90))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "��ϸ";
            #endregion
        }

        #endregion

        #region ��ѯ����

        /// <summary>
        /// ��ѯ ����ʱ���������͡�״̬���¼���ѯ
        /// </summary>
        public void Query()
        {
            this.neuSpread1.ActiveSheetIndex = 0;
            this.txtFilter.Text = "";
            if (this.BillType == "NO")
            {
                MessageBox.Show(this, Language.Msg("��ѡ�񵥾�����"), "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }
            else if (BillType == "I")
            {
                this.QueryInputData();
            }
            else if (BillType == "O")
            {
                this.QueryOutputData();
            }
           
            else
            {
                MessageBox.Show(this, Language.Msg("�޷�ʶ��ĵ�������") + this.BillType, "����>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
           
        }

        #endregion

        #region ��ӡ����

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("û�пɴ�ӡ������"));
                return;
            }
            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show(Language.Msg("û��ѡ��Ҫ��ӡ�ĵ���"));
                return;
            }
            //this.txtFilter.Text = "";
            this.neuSpread1_Sheet2.RowCount = 0;
            this.isprint = true;
            //���
            if (this.BillType == "I")
            {
                this.PrintInputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
            //����
            if (this.BillType == "O")
            {
                this.PrintOutputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
          
            this.neuSpread1.ActiveSheetIndex = 1;
        }

        #endregion

        #region ��ⵥ

        /// <summary>
        /// ��ȡ��ⵥ�б�
        /// </summary>
        public void QueryInputData()
        {
            DataSet ds = new DataSet();
            if (this.inOutMgr.ExecQuery("Pharmacy.Report.GetInputList", ref ds, this.privDept.ID, this.BillState, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString()) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ��ⵥ�б����"));
                return;
            }
            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show(Language.Msg("����ⵥ����"));
                return;
            }

            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;

            this.dvInput = ds.Tables[0].DefaultView;

            this.SetFpForInputTot();
        }

        /// <summary>
        /// ��ȡ�����ϸ��Ϣ
        /// </summary>
        /// <param name="billno">��ⵥ��</param>
        /// <returns>inputʵ������</returns>
        private ArrayList QueryInputDetail(string billNO)
        {
            if (billNO == null || billNO == "")
            {
                return null;
            }

            ArrayList al = this.inOutMgr.QueryInputInfoByListID(this.privDept.ID, billNO, "AAAA", this.BillState == "A" ? this.BillState.PadLeft(4, 'A') : this.BillState);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("���ݵ��ݺŻ�ȡ�����Ϣʧ��"));
                return null;
            }
            return al;
        }

        /// <summary>
        /// ���ݷ�Ʊ��ȡ�����ϸ��Ϣ
        /// </summary>
        /// <param name="alInputBillNO">��ⵥ��</param>
        /// <param name="alInvoice">��Ʊ��</param>
        /// <returns>inputʵ������</returns>
        private ArrayList QueryInputDetail(ArrayList alInputBillNO, ArrayList alInvoice)
        {
            if (alInputBillNO.Count == 0)
            {
                return null;
            }
            try
            {
                ArrayList alAllInput = new ArrayList();     //���в�ѯ�������ϸ����    
                //��ȡ����Ҫ��ѯ�ĵ��ݵ�������Ϣ				
                foreach (string inputBillNO in alInputBillNO)
                {
                    ArrayList al = this.QueryInputDetail(inputBillNO);
                    if (al == null)
                    {
                        MessageBox.Show(Language.Msg("���ݵ��ݺŻ�ȡ�����б�ʧ��"));
                    }
                    if (al.Count > 0)
                    {
                        alAllInput.AddRange(al);
                    }
                }
                if (alInvoice.Count == 0)
                {
                    return alAllInput;
                }

                //��Ҫ��ʾ�������ϸ����
                ArrayList alNeedShowInput = new ArrayList();
                //����Ʊ�ţ����ݺ���Ϊ��������HashTable
                System.Collections.Hashtable hsInvoiceList = new Hashtable();

                //ȡ�����з�Ʊ�������Ϣ ���ݹ�����������
                foreach (FS.FrameWork.Models.NeuObject invoice in alInvoice)
                {
                    if (!hsInvoiceList.ContainsKey(invoice.ID + invoice.Name))
                    {
                        hsInvoiceList.Add(invoice.ID + invoice.Name,null);
                    }
                }

                foreach (FS.HISFC.Models.Pharmacy.Input input in alAllInput)
                {
                    if (hsInvoiceList.ContainsKey(input.InvoiceNO + input.InListNO))
                    {
                        alNeedShowInput.Add(input);
                    }
                }

               
                return alNeedShowInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���ݷ�Ʊ��ȡ�����ϸ��Ϣ����>>" + ex.Message));
                return null;
            }

        }

        /// <summary>
        /// ��ӡ����ʾ�����ϸ��Ϣ 
        /// </summary>
        /// <param name="activerow">�������ڻ���fp���к�</param>
        private void PrintInputBill(int activeRowIndex)
        {
            /*2007-1-20 by zengft
             * ����û�ѡ���ˡ�ѡ�С�������Ϊ����Ʊ��ӡ��ⵥ��������ʾ���б�ѡ�з�Ʊ���������
             * ����û�û��ѡ���κΡ�ѡ�С�������Ϊȫ����ӡ��������ʾ���ŵ�������(���¼�������)
             * ����û�ѡ���˶��ŵ����е����ݣ�ֻ��ʾ��ǰ��еĵ�����ѡ�е�����
             * �����Ϊ���û��ڻ��ܱ�ѡ�е���һ�У����һ��ܱ���ʾ�������ǰ����ݺźͷ�Ʊ�ŷֿ���
             */           

            ArrayList alInvoice = new ArrayList();          //ѡ�д�ӡ�ķ�Ʊ��
            ArrayList alSelectListNO = new ArrayList();     //ѡ�е���ⵥ��

            bool isMergePrint = false;                      //�Ƿ�ϲ���ʾ�ʹ�ӡ

            //��еĵ��ݺ�
            string activeInputBillNO = this.neuSpread1_Sheet1.Cells[activeRowIndex, (int)ColIndex.ListNOIndex].Text.Trim();
            //��ǰ���ݺ�
            string tempInputBillNO = activeInputBillNO;

            #region ��ȡ���ݺźͷ�Ʊ��

            try
            {
                //alSelectListNO.Add(tempInputBillNO);
              
                //for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                //{
                //    if(activeInputBillNO==this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.ListNOIndex].Text.Trim())
                //    {
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.PrintIndex].Text == "True")//����ѡ��
                    //{
                    //    #region ���ֶ൥��ѡ�񣬴�ӡ��ʱ��ѯ�ʣ���ʾ��ϸ��ѯ���Ƿ�ϲ�

                    //    if (this.IsPrint && !isMergePrint && activeInputBillNO != this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.ListNOIndex].Text.Trim())
                    //    {
                    //        if (MessageBox.Show(this, Language.Msg("��ѡ���˲�ͬ���ݵ�����\n��ӡ����ʱ����ϲ���һ��\n�Ƿ������"), "����>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    //        {
                    //            //����û���ͬ��ϲ�,ȡ�����зǻ�еĵ���ѡ��
                    //            for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
                    //            {
                    //                this.neuSpread1_Sheet1.Cells[j, (int)ColIndex.PrintIndex].Text = "False";
                    //            }
                    //            //��յ�������
                    //            alSelectListNO.Clear();

                    //            this.neuSpread1.ActiveSheetIndex = 0;
                    //            return;
                    //        }

                    //        //��֤һ��ͬ��ϲ�������ʾ
                    //        isMergePrint = true;
                    //    }

                    //    #endregion

                    //    //�ѵ��ݺż���
                    //    if (tempInputBillNO != this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.ListNOIndex].Text.Trim())
                    //    {
                    //        //���£���ֹ�ظ���ӵ��ݺ�
                    //        tempInputBillNO = this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.ListNOIndex].Text.Trim();
                    //        if (activeInputBillNO != tempInputBillNO)
                    //        {
                    //            alSelectListNO.Add(tempInputBillNO);
                    //        }
                //    //    }

                //        //�ѷ�Ʊ�ż���
                //        FS.FrameWork.Models.NeuObject ob = new FS.FrameWork.Models.NeuObject();
                //        ob.ID = this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.InvoiceIndex].Text.Trim();
                //        ob.Name = this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.ListNOIndex].Text.Trim();

                //        alInvoice.Add(ob);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ȡѡ�еķ�Ʊ�š����ݺų���>>" + ex.Message);
            }
            #endregion

            //��ȡ����
            ArrayList alInputDetail = this.QueryInputDetail(activeInputBillNO);
            if (alInputDetail == null || alInputDetail.Count == 0)
            {
                MessageBox.Show(Language.Msg("û�л�������ϸ����"));

                return;
            }

            //��ʾ����
            this.AddInputDataToFp(alInputDetail);

            //��ӡ
            if (this.IsPrint)
            {
                SOC.HISFC.Components.Pharmacy.Function.PrintBill("0310", ((FS.HISFC.Models.Pharmacy.Input)alInputDetail[0]).PrivType, alInputDetail);

                this.IsPrint = false;
            }
        }

        /// <summary>
        /// �����ϸ���ݸ�ֵ
        /// </summary>
        /// <param name="al"></param>
        private void AddInputDataToFp(ArrayList al)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant constant = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

            //��Ҫ����ϸ�ĵ��ݺ���ʾ�����������ж��ŵ��ݵĿ���
            string tempBillNO = ((FS.HISFC.Models.Pharmacy.Input)al[0]).InListNO;
            //this.txtFilter.Text = "" + tempBillNO;

            //�����
            if (this.neuSpread1_Sheet2.RowCount > 0)
            {
                this.neuSpread1_Sheet2.RowCount = 0;
            }
            this.neuSpread1_Sheet2.Rows.Add(0, al.Count + 1);

            //���и�ֵ
            if (this.neuSpread1_Sheet2.ColumnCount < 11)
            {
                this.neuSpread1_Sheet2.ColumnCount = 11;
            }
            decimal PurchaseTot = 0;
            decimal RetailTot = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;

                //�൥�ݺ�
                if (tempBillNO != input.InListNO)
                {
                    tempBillNO = input.InListNO;
                    this.txtFilter.Text += "+" + tempBillNO;
                }

                this.neuSpread1_Sheet2.Cells[i, 0].Text = input.InvoiceNO;
                try
                {
                    if (input.Company.Name == null || input.Company.Name == "")
                    {
                        input.Company.Name = (constant.QueryCompanyByCompanyID(input.Company.ID)).Name;
                    }
                }
                catch
                {
                    try
                    {
                        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                        input.Company.Name = (deptMgr.GetDeptmentById(input.Company.ID)).Name;

                    }
                    catch
                    { }
                }
                this.neuSpread1_Sheet2.Cells[i, 1].Text = input.Company.Name;
                this.neuSpread1_Sheet2.Cells[i, 2].Text = input.BatchNO;
                this.neuSpread1_Sheet2.Cells[i, 3].Text = input.Item.Name;

                //��λ������������ʾ��ͬһ��
                this.neuSpread1_Sheet2.Cells[i, 4].Text = " " + input.Item.Specs;

                //��װ��λ��������������Ϊ0
                if (input.Item.PackQty == 0) input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                if (input.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 5].Text = (count).ToString() + input.Item.MinUnit;
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 5].Text = (count / input.Item.PackQty).ToString() + input.Item.PackUnit;
                }
                this.neuSpread1_Sheet2.Cells[i, 6].Value = System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 7].Value = System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 8].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 9].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 10].Value = System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty
                    - System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                PurchaseTot += System.Math.Round(input.Item.PriceCollection.PurchasePrice, decimals) * count / input.Item.PackQty;
                RetailTot += System.Math.Round(input.Item.PriceCollection.RetailPrice, decimals) * count / input.Item.PackQty;
            }
            this.neuSpread1_Sheet2.Cells[al.Count, 3].Text = "��  �ƣ�";
            this.neuSpread1_Sheet2.Cells[al.Count, 7].Text = PurchaseTot.ToString("F2");
            this.neuSpread1_Sheet2.Cells[al.Count, 9].Text = RetailTot.ToString("F2");
        }

        /// <summary>
        /// ��ⵥ��������fp
        /// </summary>
        private void SetFpForInputTot()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            if (this.neuSpread1_Sheet1.ColumnCount > 5)
            {
                this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).Visible = false;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).CellType = checkBoxCellType1;
                this.neuSpread1_Sheet1.Columns.Get((int)ColIndex.PrintIndex).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Columns.Get(1).Width = 42F;
                this.neuSpread1_Sheet1.Columns.Get(2).Width = 109F;
                this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
                this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
                this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            }
        }

        /// <summary>
        /// ����ⵥ��ʼ��fp
        /// </summary>
        public void SetFpForInput()
        {
            #region ������Ϣ
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "���ݺ�";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "ѡ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "��Ʊ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "������˾";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "״̬";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "��ʽ";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "���ݺ�";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "ѡ��";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 42F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "��Ʊ��";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 109F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "������˾";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "״̬";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "��ʽ";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "��ѯ��";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "��ⵥ����";
            this.neuSpread1_Sheet1.Columns.Get(0).MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;                

            #endregion

            #region ��ϸ��Ϣ
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 11;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Text = "��Ʊ��";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Text = "ҩƷ��Դ";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Text = "����";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Text = "ҩƷ����";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Text = "���";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Text = "����";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Text = "�����";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Text = "������";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Text = "���ۼ�";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 9).Text = "���۽��";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 10).Text = "���";
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "��Ʊ��";
            this.neuSpread1_Sheet2.Columns.Get(0).CellType = textCellType1;
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 71F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "ҩƷ��Դ";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 105F;
            this.neuSpread1_Sheet2.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "ҩƷ����";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 86F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "���";
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 87F;
            this.neuSpread1_Sheet2.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            numberCellType1.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(6).CellType = numberCellType1;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "�����";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 70F;
            numberCellType2.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(7).CellType = numberCellType2;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "������";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 80F;
            numberCellType3.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(8).CellType = numberCellType3;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "���ۼ�";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 70F;
            numberCellType4.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(9).CellType = numberCellType4;
            this.neuSpread1_Sheet2.Columns.Get(9).Label = "���۽��";
            this.neuSpread1_Sheet2.Columns.Get(9).Width = 80F;
            numberCellType5.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(10).CellType = numberCellType5;
            this.neuSpread1_Sheet2.Columns.Get(10).Label = "���";
            this.neuSpread1_Sheet2.Columns.Get(10).Width = 80F;
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "�����ϸ";
            #endregion
        }

        #endregion

        #region ���ⵥ

        /// <summary>
        /// ��ȡ���ⵥ�б�
        /// </summary>
        public void QueryOutputData()
        {
            DataSet ds = new DataSet();
            if (this.inOutMgr.ExecQuery("Pharmacy.Report.GetOutputList", ref ds, this.privDept.ID, this.BillState, this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(),this.cmbDept.Tag.ToString()) == -1)
            {
                MessageBox.Show("��ȡ���ⵥ�б����");
                return;
            }

            if (ds.Tables == null || ds.Tables.Count == 0)
            {
                MessageBox.Show(Language.Msg("�޳��ⵥ����"));
                return;
            }

            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;

            this.dvOutput = ds.Tables[0].DefaultView;

            this.SetFpForOutputTot();
        }

        /// <summary>
        /// ���ⵥ��������fp
        /// </summary>
        private void SetFpForOutputTot()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 114F;
        }

        /// <summary>
        /// ��ȡ���ⵥ��ϸ
        /// </summary>
        /// <param name="billno">���ݺ�</param>
        /// <returns>outputʵ������</returns>
        private ArrayList QueryOutputDetail(string billNO)
        {
            if (billNO == null || billNO == "")
            {
                return null;
            }

            //ArrayList al = this.itemManager.QueryOutputInfo(this.privDept.ID, billNO, this.BillState);
            ArrayList al = this.inOutMgr.QueryOutputInfo(this.privDept.ID, billNO, this.BillState);
            if (al.Count == 0)
            {
                return null;
            }
            try
            {

                //Sort noSort = new Sort();
                //al.Sort(noSort);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���������������>>" + ex.Message));
            }
            return al;
        }

        /// <summary>
        /// ��ӡ����ʾ������ϸ
        /// </summary>
        /// <param name="activerow">�������</param>
        private void PrintOutputBill(int activerow)
        {
            //��õ��ݺ�
            string billNO = this.neuSpread1_Sheet1.Cells[activerow, (int)ColIndex.BillNOIndex].Text.Trim();
            if (billNO == null || billNO == "")
            {
                return;
            }
            ArrayList al = new ArrayList();
            al = this.QueryOutputDetail(billNO);
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�л�ó�����ϸ����"));
                return;
            }
            this.AddOutputDataToFp(al);

            //��ӡ
            if (this.IsPrint)
            {
                //�ӿڴ�ӡ����
                SOC.HISFC.Components.Pharmacy.Function.PrintBill("0320", ((FS.HISFC.Models.Pharmacy.Output)al[0]).PrivType, al);
                this.IsPrint = false;
            }
        }

        /// <summary>
        /// �����ⵥ��ʼ��fp
        /// </summary>
        private void SetFpForOutput()
        {
            #region ����
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "���ⵥ��";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "��ʽ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "���õ�λ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "���õ�λ";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "������";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "״̬";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Text = "��ⵥ��";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "���ⵥ��";
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��ʽ";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 68F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���õ�λ";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 65F;
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "���õ�λ";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 211F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "������";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 97F;
            this.neuSpread1_Sheet1.Columns.Get(5).Label = "״̬";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 112F;
            this.neuSpread1_Sheet1.Columns.Get(6).Label = "��ⵥ��";
            this.neuSpread1_Sheet1.Columns.Get(6).Width = 114F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "�������";
            #endregion

            #region ��ϸ
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 9;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 0).Text = "����";
            this.neuSpread1_Sheet2.Columns.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 1).Text = "ҩ��";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 2).Text = "���";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 3).Text = "��λ";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 4).Text = "��������";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 5).Text = "�����";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 6).Text = "������";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 7).Text = "���ۼ�";
            this.neuSpread1_Sheet2.ColumnHeader.Cells.Get(0, 8).Text = "���۽��";
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.Columns.Get(0).Label = "����";
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 71F;
            this.neuSpread1_Sheet2.Columns.Get(1).Label = "ҩ��";
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 115F;
            this.neuSpread1_Sheet2.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet2.Columns.Get(2).Width = 138F;
            this.neuSpread1_Sheet2.Columns.Get(3).Label = "��λ";
            this.neuSpread1_Sheet2.Columns.Get(3).Width = 45F;
            this.neuSpread1_Sheet2.Columns.Get(4).Label = "��������";
            this.neuSpread1_Sheet2.Columns.Get(4).Width = 87F;
            numberCellType1.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(5).CellType = numberCellType1;
            this.neuSpread1_Sheet2.Columns.Get(5).Label = "�����";
            this.neuSpread1_Sheet2.Columns.Get(5).Width = 70F;
            //���������ع����
            if (this.BillType == "D")
            {
                this.neuSpread1_Sheet2.Columns.Get(5).Visible = false;
            }
            numberCellType2.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(6).CellType = numberCellType2;
            this.neuSpread1_Sheet2.Columns.Get(6).Label = "������";
            this.neuSpread1_Sheet2.Columns.Get(6).Width = 80F;
            if (this.BillType == "D")
            {
                this.neuSpread1_Sheet2.Columns.Get(6).Visible = false;
            }
            numberCellType3.DecimalPlaces = 4;
            this.neuSpread1_Sheet2.Columns.Get(7).CellType = numberCellType3;
            this.neuSpread1_Sheet2.Columns.Get(7).Label = "���ۼ�";
            this.neuSpread1_Sheet2.Columns.Get(7).Width = 70F;
            numberCellType4.DecimalPlaces = 2;
            this.neuSpread1_Sheet2.Columns.Get(8).CellType = numberCellType4;
            this.neuSpread1_Sheet2.Columns.Get(8).Label = "���۽��";
            this.neuSpread1_Sheet2.Columns.Get(8).Width = 80F;
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(107)), ((System.Byte)(105)), ((System.Byte)(107)));
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "������ϸ";
            #endregion
        }

        /// <summary>
        /// fp������ϸ���ݸ�ֵ
        /// </summary>
        /// <param name="al"></param>
        private void AddOutputDataToFp(ArrayList al)
        {
            //��ʾ���ݺ�
            string tempbillno = ((FS.HISFC.Models.Pharmacy.Output)al[0]).OutListNO;
            //this.txtFilter.Text = "" + tempbillno;

            //���fp
            if (this.neuSpread1_Sheet2.RowCount > 0)
            {
                this.neuSpread1_Sheet2.RowCount = 0;
            }

            //��fp�����
            this.neuSpread1_Sheet2.Rows.Add(0, al.Count + 1);

            //fp���и�ֵ
            if (this.neuSpread1_Sheet2.ColumnCount < 9)
            {
                this.neuSpread1_Sheet2.ColumnCount = 9;
            }
            decimal PurchaseTot = 0;
            decimal RetailTot = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.neuSpread1_Sheet2.Cells[i, 0].Text = output.BatchNO;
                this.neuSpread1_Sheet2.Cells[i, 1].Text = output.Item.Name;
                this.neuSpread1_Sheet2.Cells[i, 2].Text = output.Item.Specs;
                if (output.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = output.Item.MinUnit;
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = output.Item.PackUnit;
                }
                //��װ������������
                if (output.Item.PackQty == 0)
                {
                    output.Item.PackQty = 1;
                }
                decimal count = 0;
                count = output.Quantity;

                if (output.ShowState == "0")
                {
                    this.neuSpread1_Sheet2.Cells[i, 4].Text = System.Convert.ToString(count);
                }
                else
                {
                    this.neuSpread1_Sheet2.Cells[i, 4].Text = System.Convert.ToString(count / output.Item.PackQty);
                }
                this.neuSpread1_Sheet2.Cells[i, 5].Value = System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 6].Value = System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals) * count / output.Item.PackQty;
                this.neuSpread1_Sheet2.Cells[i, 7].Value = System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals);
                this.neuSpread1_Sheet2.Cells[i, 8].Value = System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals) * count / output.Item.PackQty;
                PurchaseTot += System.Math.Round(output.Item.PriceCollection.PurchasePrice, decimals) * count / output.Item.PackQty;
                RetailTot += System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals) * count / output.Item.PackQty;
            }
            this.neuSpread1_Sheet2.Cells[al.Count, 1].Text = "��  �ƣ�";
            this.neuSpread1_Sheet2.Cells[al.Count, 6].Text = PurchaseTot.ToString("F2");
            this.neuSpread1_Sheet2.Cells[al.Count, 8].Text = RetailTot.ToString("F2");
        }

        #endregion

        #region ���ѡ��

        /// <summary>
        /// ��ѡ
        /// </summary>
        /// <param name="isSelect">�Ƿ�ѡ��</param>
        public void SelectAll(bool isSelect)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Cells.Get(0, 1).Text == "����˵��")
                {
                    return;
                }

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColIndex.PrintIndex].Value = isSelect;
                }
            }
        }

        #endregion

        #endregion

        #region ������

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //�������ң��ӽ�㵥��
            if (e.Tag != null && e.Parent != null && e.Parent.Tag != null)
            {
                this.BillType = (e.Tag as FS.FrameWork.Models.NeuObject).ID;
                this.privDept = e.Parent.Tag as FS.FrameWork.Models.NeuObject;
                if (this.neuSpread1.ActiveSheetIndex != 0)
                {
                    this.neuSpread1.ActiveSheetIndex = 0;
                }
            }
            return base.OnSetValue(neuObject, e);
        }        

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                tvPrivTree tvPriv = this.tv as tvPrivTree;
                
            }
            base.OnLoad(e);
        }

        private void cmbStatus_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (this.cmbStatus.Text != null && this.cmbStatus.Text != "")
            {
                this.BillState = this.cmbStatus.SelectedValue.ToString();
            }

        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1 || this.BillType != "I")
            {
                return;
            }
            if (e.Column == (int)ColIndex.PrintIndex)
            {
                if (this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text == "True")
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = "False";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = "True";
                }
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1)
            {
                return;
            }
            this.IsPrint = false;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveRowIndex = e.Row;
            //���
            if (this.BillType == "I")
            {
                this.PrintInputBill(e.Row);
            }
            //����
            if (this.BillType == "O")
            {
                this.PrintOutputBill(e.Row);
            }
            if (this.BillType != "NO")
            {
                this.neuSpread1.ActiveSheetIndex = 1;
            }
         
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {
            if (this.neuSpread1.ActiveSheetIndex == 1 && this.dvInput == null && this.dvOutput == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtFilter.Text.Trim())==false)
            {
                if (this.txtFilter.Text.Trim().Substring(0, 1) == "��")
                {
                    return;
                }
            }
            string tempfilter = this.txtFilter.Text.Trim();
            try
            {
                if (this.BillType=="I")
                {
                    if (this.dvInput != null)
                    {
                        this.dvInput.RowFilter = "(���ݺ� LIKE '%" + tempfilter + "%')"
                            + "OR (��Ʊ�� LIKE '%" + tempfilter + "%')"
                            + "OR (״̬ LIKE '%" + tempfilter + "%')"
                            + "OR (��ʽ LIKE '%" + tempfilter + "%')"
                            + "OR (��Ʊ�� LIKE '%" + tempfilter + "%')"
                            + "OR (������˾ LIKE '%" + tempfilter + "%')";

                        this.neuSpread1_Sheet1.DataSource = this.dvInput;

                        this.SetFpForInputTot();
                    }
                }
                else if (this.BillType == "O")
                {
                    if (this.dvOutput != null)
                    {
                        this.dvOutput.RowFilter = "(���ⵥ�� LIKE '%" + tempfilter + "%')"
                            + "OR (���õ�λ LIKE '%" + tempfilter + "%')"
                            + "OR (״̬ LIKE '%" + tempfilter + "%')"
                            + "OR (��ʽ LIKE '%" + tempfilter + "%')"
                            + "OR (��ⵥ�� LIKE '%" + tempfilter + "%')"
                            + "OR (������ LIKE '%" + tempfilter + "%')";

                        this.neuSpread1_Sheet1.DataSource = this.dvOutput;

                        this.SetFpForOutputTot();
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��ȡ������������>>" + ex.Message));
            }
        }
       
        #endregion

        #region ������

        private enum ColIndex
        {
            /// <summary>
            /// ���ݺ���
            /// </summary>
            BillNOIndex = 0,
            /// <summary>
            /// ��ӡѡ����
            /// </summary>
            PrintIndex = 1,
            /// <summary>
            /// ��Ʊ����
            /// </summary>
            InvoiceIndex = 2,
            /// <summary>
            /// ��ⵥ�ݺ���
            /// </summary>
            ListNOIndex = 6
        }

        #endregion        
    }

    internal class Sort : System.Collections.IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Output o1 = x as FS.HISFC.Models.Pharmacy.Output;

            FS.HISFC.Models.Pharmacy.Output o2 = y as FS.HISFC.Models.Pharmacy.Output;

            return NConvert.ToInt32(o1.ID) - NConvert.ToInt32(o2.ID);
        }

        #endregion
    }
    /// <summary>
    /// �̵㵥��ҩƷ�������
    /// </summary>
    internal class SortCheck : System.Collections.IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Check ch1 = x as FS.HISFC.Models.Pharmacy.Check;

            FS.HISFC.Models.Pharmacy.Check ch2 = y as FS.HISFC.Models.Pharmacy.Check;
            string drugType1 = GetDrugCode(ch1.Item.Type.ID) + ch1.PlaceNO;
            string drugType2 = GetDrugCode(ch2.Item.Type.ID) + ch2.PlaceNO;
            return string.Compare(drugType1, drugType2);
        }
        private string GetDrugCode(string drugtype)
        {
            string drugType = drugtype;
            if (drugtype == "Z" || drugtype == "P" || drugtype == "C")
            {
                return "L";
            }
            return drugType;
        }
        #endregion 
    }
}
