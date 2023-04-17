using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FarPoint.Win;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ�����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOrderConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderConfirm()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ҽ������
        /// </summary>
        private int LongOrderCount = 0;

        /// <summary>
        /// ��ʱҽ������
        /// </summary>
        private int ShortOrderCount = 0;

        /// <summary>
        /// �����ļ�
        /// </summary>
        string strFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath +
            FS.FrameWork.WinForms.Classes.Function.SettingPath + "fpOrderConfirm.xml";

        //DataTable dtMain;
        //DataSet myDataSet;

        /// <summary>
        /// ������ϸ
        /// </summary>
        DataTable dtChild1;

        /// <summary>
        /// ������ϸ
        /// </summary>
        DataTable dtChild2;

        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        FS.FrameWork.Models.NeuObject OrderId = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        FS.FrameWork.Models.NeuObject ComboNo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰѡ�еĵ�Ԫ��
        /// </summary>
        protected FarPoint.Win.Spread.Cell CurrentCellName;

        /// <summary>
        /// ѡ�л��ߵ�סԺ��ˮ��
        /// </summary>
        string PatientId = "";

        /// <summary>
        /// ����ҽ������
        /// </summary>
        string speOrderType = "";

        /// <summary>
        /// �������ÿ���
        /// </summary>
        Hashtable hsDepts = new Hashtable();

        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        protected ArrayList alpatientinfos;

        /// <summary>
        /// IOP�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IOP iop = null;

        /// <summary>
        /// ��ǰ��½����
        /// </summary>
        private FS.HISFC.Models.Base.Department currentDept = null;

        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ�Ƿ���Ƿ���ʾ
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.Y;

        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ�Ƿ���Ƿ���ʾ
        /// </summary>
        [Category("�������"), Description("Y���ж�Ƿ��,����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        /// <summary>
        /// ��ǰҽ������
        /// </summary>
        protected FS.HISFC.Models.Order.EnumType myShowOrderType = FS.HISFC.Models.Order.EnumType.LONG;

        /// <summary>
        /// ��ʾҽ������
        /// </summary>
        public FS.HISFC.Models.Order.EnumType ShowOrderType
        {
            get
            {
                return this.myShowOrderType;
            }
            set
            {
                this.myShowOrderType = value;
                if (this.myShowOrderType == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.fpSpread.ActiveSheetIndex = 0;
                }
                else
                {
                    this.fpSpread.ActiveSheetIndex = 1;
                }
            }
        }

        /// <summary>
        /// ����Ա����
        /// </summary>
        protected FS.HISFC.Models.Base.Employee myOperator;

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        protected FS.HISFC.Models.Base.Employee Operator
        {
            get
            {
                if (myOperator == null)
                    myOperator = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                return myOperator;
            }
        }

        /// <summary>
        /// ���û�ʿվ����ҽ��������,���Ÿ���
        /// </summary>
        [Category("�������"), Description("���û�ʿվ����ҽ��������,���Ÿ��� ����:CONS ����:DEPTXXX ҽ��:DEPTXXX ����:OTHER")]
        public string SpeOrderType
        {
            set
            {
                this.speOrderType = value;
            }
            get
            {
                return this.speOrderType;
            }
        }

        /// <summary>
        /// �˷������Ƿ��Զ�ȷ��
        /// </summary>
        private bool autoQuitFeeApply = false;

        /// <summary>
        /// �˷������Ƿ��Զ�ȷ��
        /// </summary>
        [Category("�������"), Description("�Ƿ�ֱ���˷ѣ����ڷǱ������շ���Ŀ��ֻ��������")]
        public bool AutoQuitFeeApply
        {
            set
            {
                this.autoQuitFeeApply = value;
            }
            get
            {
                return this.autoQuitFeeApply;
            }
        }

        /// <summary>
        /// ֹͣҽ���˷�ʱ��Ĭ���˷�����
        /// 1 ֹͣʱ�����շ�������0 Ĭ����0 
        /// </summary>
        private int defaultQuitQty = 1;

        /// <summary>
        /// ֹͣҽ���˷�ʱ��Ĭ���˷�����
        /// 0 Ĭ����0�� 1 ֹͣʱ�����շ�������
        /// </summary>
        [Category("�������"), Description("ֹͣҽ���˷�ʱ��Ĭ���˷�������0 Ĭ����0�� 1 ֹͣʱ�����շ�����")]
        public int DefaultQuitQty
        {
            get
            {
                return defaultQuitQty;
            }
            set
            {
                defaultQuitQty = value;
            }
        }

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        private bool lackDealModel = true;

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        [Category("�������"), Description("Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒѣ�")]
        public bool LackDealModel
        {
            get
            {
                return lackDealModel;
            }
            set
            {
                lackDealModel = value;
            }
        }



        /// <summary>
        /// ֹͣҽ���Ƿ��Զ��˿ڷ�ҩ
        /// </summary>
        private bool isQuitPOQty = false;

        /// <summary>
        /// ֹͣҽ���Ƿ��Զ��˿ڷ�ҩ
        /// </summary>
        [Category("�������"), Description("ֹͣҽ���Ƿ��Զ��˿ڷ�ҩ��")]
        public bool IsQuitPOQty
        {
            get
            {
                return isQuitPOQty;
            }
            set
            {
                isQuitPOQty = value;
            }
        }

        /// <summary>
        /// ��Ժ��ҩ�Ƿ��жϾ�����
        /// </summary>
        private bool iSCDCharge = false;
        /// <summary>
        /// ��Ժ��ҩ�Ƿ��жϾ�����
        /// </summary>
        [Category("�������"), Description("��Ժ��ҩ�Ƿ��жϾ����ߣ�")]
        public bool ISCDCharge
        {
            get
            {
                return iSCDCharge;
            }
            set
            {
                iSCDCharge = value;
            }
        }

        #endregion

        #region ����

        #region ��ʼ��

        /// <summary>
        /// ��ʼ�������ؼ�
        /// </summary>
        private void InitControl()
        {
            this.InitFp();
            ucSubtblManager1 = new ucSubtblManager();
            this.ucSubtblManager1.IsVerticalShow = true;
            this.ucSubtblManager1.SetRightShow();
            this.DockingManager();
            this.ucSubtblManager1.ShowSubtblFlag += new ucSubtblManager.ShowSubtblFlagEvent(ucSubtblManager1_ShowSubtblFlag);

            #region Ԥֹͣҽ����ҽ��״̬����ɫ��ʾ
            //addby xuewj 2010-11-3 {344D145C-A30A-4ad1-86ED-CBCF80C751FA}

            base.OnStatusBarInfo(null, "(��ɫ���¿�)(��ɫ�����)(��ɫ��ִ��)(��ɫ������)(��ɫ��Ԥֹͣ)");

            ArrayList alDeptALL = this.deptManager.GetDeptmentAll();

            foreach (FS.HISFC.Models.Base.Department dept in alDeptALL)
            {
                if (!this.hsDepts.Contains(dept.ID))
                {
                    hsDepts.Add(dept.ID, dept);
                }
            }

            #endregion
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tv = sender as TreeView;
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            this.InitControl();

            return null;
        }

        /// <summary>
        /// ��ʼ��fpTreeView1
        /// </summary>
        private void InitFp()
        {
            this.fpSpread.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread_ChildViewCreated);

            this.fpSpread.Sheets[0].SheetName = "����ҽ��";
            this.fpSpread.Sheets[1].SheetName = "��ʱҽ��";
            this.fpSpread.Sheets[0].Columns[0].Visible = false;
            this.fpSpread.Sheets[0].Columns[1].Label = "��ˣ۳��ڣ�";
            this.fpSpread.Sheets[0].Columns[2].Label = "��������";
            this.fpSpread.Sheets[0].Columns[3].Label = "����";
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}��� �Ա�������
            this.fpSpread.Sheets[0].Columns[4].Label = "�Ա�";
            this.fpSpread.Sheets[0].Columns[5].Label = "����";
            this.fpSpread.Sheets[0].Columns[6].Label = "����ҽ��";
            this.fpSpread.Sheets[0].Columns[7].Label = "��ܰ��ʾ��������ֽⳤ��ҽ����";
            this.fpSpread.Sheets[0].ColumnHeader.Columns[7].ForeColor = Color.Red;
            this.fpSpread.Sheets[0].RowCount = 0;
            this.fpSpread.Sheets[0].ColumnCount = 8;
            this.fpSpread.Sheets[0].Columns[1].Width = 100;
            this.fpSpread.Sheets[0].Columns[2].Width = 100;
            this.fpSpread.Sheets[0].Columns[6].Width = 100;
            this.fpSpread.Sheets[0].Columns[7].Width = 200;
            this.fpSpread.Sheets[0].GrayAreaBackColor = Color.WhiteSmoke;

            this.fpSpread.Sheets[1].Columns[0].Visible = false;
            this.fpSpread.Sheets[1].Columns[1].Label = "��ˣ���ʱ��";
            this.fpSpread.Sheets[1].Columns[2].Label = "��������";
            this.fpSpread.Sheets[1].Columns[3].Label = "����";
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            this.fpSpread.Sheets[1].Columns[4].Label = "�Ա�";
            this.fpSpread.Sheets[1].Columns[5].Label = "����";
            this.fpSpread.Sheets[1].Columns[6].Label = "����ҽ��";
            this.fpSpread.Sheets[1].RowCount = 0;
            this.fpSpread.Sheets[1].ColumnCount = 7;
            this.fpSpread.Sheets[1].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[1].Columns[1].Width = 100;
            this.fpSpread.Sheets[1].Columns[2].Width = 100;
            this.fpSpread.Sheets[1].Columns[6].Width = 100;

            this.fpSpread.Sheets[0].DataAutoSizeColumns = false;
            this.fpSpread.Sheets[1].DataAutoSizeColumns = false;

            this.fpSpread.Sheets[0].Rows.Get(-1).BackColor = Color.LightSkyBlue;
            this.fpSpread.Sheets[1].Rows.Get(-1).BackColor = Color.LightSkyBlue;

            //this.fpSpread.Sheets[0].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(ucOrderConfirm_CellChanged);
            //this.fpSpread.Sheets[1].CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(ucOrderConfirm_CellChanged);
            this.fpSpread.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellClick);
            this.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);
        }

        #endregion

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            this.alpatientinfos = alValues;

            this.QueryOrder();

            #region {839D3A8A-49FA-4d47-A022-6196EB1A5715}
            if (this.tv != null && this.tv.CheckBoxes)
            {
                foreach (TreeNode parentNode in tv.Nodes)
                {
                    SetTree(parentNode);
                }
            }
            #endregion
            return 0;
        }
        public override void Refresh()
        {
            this.fpSpread.StopCellEditing();
            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            string strInpatientNo = ""; //��ǰ������
            string strName = "";//��ǰ��Ŀ��

            List<string> OrderIDs = new List<string>();

            #region ����ҽ��

            for (int i = 0; i < this.fpSpread.Sheets[0].Rows.Count; i++) //����ҽ��
            {
                #region ҽ������
                strInpatientNo = this.fpSpread.Sheets[0].Cells[i, 0].Text;//��ǰ�Ļ���
                strName = this.fpSpread.Sheets[0].Cells[i, 2].Text;//��ǰ�Ļ���

                //��ǰ���ߵ�ҽ���б�ҳ sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[0].GetChildView(i, 0);

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//ҽ����Ŀ
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//ҽ����Ŀ����
                            if (!string.IsNullOrEmpty(orderid))
                            {
                                OrderIDs.Add(orderid);
                            }
                        }
                    }
                }
                #endregion
            }
            
            #endregion

            #region ��ʱҽ��

            for (int i = 0; i < this.fpSpread.Sheets[1].Rows.Count; i++) //��ʱҽ��
            {
                #region ҽ������
                strInpatientNo = this.fpSpread.Sheets[1].Cells[i, 0].Text;//��ǰ�Ļ���
                strName = this.fpSpread.Sheets[1].Cells[i, 2].Text;//��ǰ�Ļ���

                //��ǰ���ߵ�ҽ���б�ҳ sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[1].GetChildView(i, 0);

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//ҽ����Ŀ
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//ҽ����Ŀ����
                            if (!string.IsNullOrEmpty(orderid))
                            {
                                OrderIDs.Add(orderid);
                            }
                        }

                    }
                }
                #endregion
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (CacheManager.InOrderMgr.StopOrder(OrderIDs) == -1)
                FS.FrameWork.Management.PublicTrans.RollBack();
            else
                FS.FrameWork.Management.PublicTrans.Commit();

            base.Refresh();
        }
        private int SetTree(TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Checked)
                {
                    if (node.Tag != null)
                    {
                        if (node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                        {
                            FS.HISFC.Models.RADT.PatientInfo patientInfo = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                            if (node.Checked)
                            {
                                switch (patientInfo.Sex.ID.ToString())
                                {
                                    case "F":
                                        //��
                                        if (patientInfo.ID.IndexOf("B") > 0)
                                            node.ImageIndex = 10;	//Ӥ��Ů
                                        else
                                            node.ImageIndex = 6;	//����Ů
                                        break;
                                    case "M":
                                        if (patientInfo.ID.IndexOf("B") > 0)
                                            node.ImageIndex = 8;	//Ӥ����
                                        else
                                            node.ImageIndex = 4;	//������
                                        break;
                                    default:
                                        node.ImageIndex = 4;
                                        break;
                                }
                                FS.HISFC.Components.Common.Classes.Function.DelLabel((node.Tag as FS.HISFC.Models.RADT.PatientInfo).ID);
                            }
                        }
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    SetTree(node);
                }
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("�Ƿ�ȷ��Ҫ����?", 422) == DialogResult.No)
            {
                return -1;
            }
            this.fpSpread.StopCellEditing();

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
            string errInfo = "";

            string strInpatientNo = ""; //��ǰ������
            string strName = "";//��ǰ��Ŀ��

            string strComboNo = "";

            string lackFeeInfo = "";

            string stockDeptId = string.Empty;

            ///�Ƿ�Ƿ��
            bool isLackFee = false;

            #region ����ҽ��

            for (int i = 0; i < this.fpSpread.Sheets[0].Rows.Count; i++) //����ҽ��
            {
                #region ҽ������
                strInpatientNo = this.fpSpread.Sheets[0].Cells[i, 0].Text;//��ǰ�Ļ���
                strName = this.fpSpread.Sheets[0].Cells[i, 2].Text;//��ǰ�Ļ���
                strComboNo = "";

                //��ǰ���ߵ�ҽ���б�ҳ sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[0].GetChildView(i, 0);
                ArrayList alOrders = new ArrayList();

                if (Classes.Function.CheckPatientState(strInpatientNo, ref patientInfo, ref errInfo) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(errInfo);
                    return -1;
                }

                isLackFee = false;
                //Ƿ����ʾ
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo))
                {
                    if (!lackFeeInfo.Contains(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patientInfo.Name))
                    {
                        lackFeeInfo += "\r\n" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patientInfo.Name;
                    }

                    if (!lackDealModel)
                    {
                        isLackFee = true;
                    }
                }

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//ҽ����Ŀ
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڴ�����ҽ��...");
                            Application.DoEvents();
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//ҽ����Ŀ����
                            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(orderid);
                            if (order == null)
                            {
                                //CacheManager.OrderIntegrate.fee.Rollback();
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                if (MessageBox.Show("����[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] ����[" + patientInfo.Name + "]" + "\nҽ���Ѿ������仯����ˢ�º��ٴ���ˣ�\n�Ƿ���������������ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return -1;
                                }
                                else
                                {
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                    //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                }
                            }

                            //�˴�����˷�����
                            if (sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag != null)
                            {
                                order.Item.User03 = sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                            }
                            order.Patient.Name = strName;
                            alOrders.Add(order);
                        }
                    }

                    //if (Classes.Function.CheckMoneyAlert(patientInfo, alOrders, messType) == -1)
                    //{
                    //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //    return -1;
                    //}

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (CacheManager.OrderIntegrate.SaveChecked(patientInfo, alOrders, true, empl.Nurse.ID, this.autoQuitFeeApply, !isLackFee, iSCDCharge) == -1)
                    {
                        //CacheManager.OrderIntegrate.fee.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        if (MessageBox.Show("����[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] ����[" + patientInfo.Name + "]\n" + CacheManager.OrderIntegrate.Err + "\n�Ƿ���������������ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            return -1;
                        }
                        else
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                    }
                    else
                    {
                        //CacheManager.OrderIntegrate.fee.Commit();
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        //�����Զ��˷�ʱ���޷�ֱ���˷���Ŀ����ʾ��Ϣ
                        if (!string.IsNullOrEmpty(CacheManager.OrderIntegrate.Err))
                        {
                            MessageBox.Show(CacheManager.OrderIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CacheManager.OrderIntegrate.Err = "";
                        }
                    }
                }
                #endregion
            }

            #endregion

            #region ��ʱҽ��

            for (int i = 0; i < this.fpSpread.Sheets[1].Rows.Count; i++) //��ʱҽ��
            {
                #region ҽ������
                strInpatientNo = this.fpSpread.Sheets[1].Cells[i, 0].Text;//��ǰ�Ļ���
                strName = this.fpSpread.Sheets[1].Cells[i, 2].Text;//��ǰ�Ļ���
                strComboNo = "";

                //��ǰ���ߵ�ҽ���б�ҳ sv
                FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[1].GetChildView(i, 0);
                ArrayList alOrders = new ArrayList();
                if (Classes.Function.CheckPatientState(strInpatientNo, ref patientInfo, ref errInfo) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(errInfo);
                    return -1;
                }

                isLackFee = false;
                //Ƿ����ʾ
                if (CacheManager.FeeIntegrate.IsPatientLackFee(patientInfo))
                {
                    if (!lackFeeInfo.Contains(patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patientInfo.Name))
                    {
                        lackFeeInfo += "\r\n" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patientInfo.Name;
                    } 
                    
                    if (!lackDealModel)
                    {
                        isLackFee = true;
                    }
                }

                if (sv != null)
                {
                    for (int j = 0; j < sv.Rows.Count; j++)//ҽ����Ŀ
                    {
                        if (sv.Cells[j, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                        {
                            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڴ�����ʱҽ��...");
                            Application.DoEvents();
                            string orderid = sv.Cells[j, int.Parse(OrderId.ID)].Text;//ҽ����Ŀ����
                            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(orderid);
                            if (order == null)
                            {
                                //CacheManager.OrderIntegrate.fee.Rollback();
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                if (MessageBox.Show("����[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] ����[" + patientInfo.Name + "]\n" + "ҽ���Ѿ������仯����ˢ�º��ٴ���ˣ�\n�Ƿ���������������ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return -1;
                                }
                                else
                                {
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                    //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); 
                                    //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                }
                            }

                            //�˴�����˷�����
                            if (sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag != null)
                            {
                                order.Item.User03 = sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                            }
                            order.Patient.Name = strName;

                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                stockDeptId = order.StockDept.ID;
                            }

                            alOrders.Add(order);
                        }

                    }

                    if (Classes.Function.CheckMoneyAlert(patientInfo, alOrders, messType) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    CacheManager.OrderIntegrate.MessageType = messType;
                    if (CacheManager.OrderIntegrate.SaveChecked(patientInfo, alOrders, false, empl.Nurse.ID, this.autoQuitFeeApply, !isLackFee, iSCDCharge) == -1)
                    {
                        //CacheManager.OrderIntegrate.fee.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                        if (MessageBox.Show("����[" + patientInfo.PVisit.PatientLocation.Bed.ID.Substring(3) + "] ����[" + patientInfo.Name + "]\n" + CacheManager.OrderIntegrate.Err + "\n�Ƿ���������������ߣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            return -1;
                        }
                        else
                        {
                            CacheManager.OrderIntegrate.Err = "";
                            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                    }
                    else
                    {
                        //CacheManager.OrderIntegrate.fee.Commit();
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //FS.FrameWork.Management.Transaction 
                        //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //this.CacheManager.OrderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //CacheManager.OrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        //�����Զ��˷�ʱ���޷�ֱ���˷���Ŀ����ʾ��Ϣ
                        if (!string.IsNullOrEmpty(CacheManager.OrderIntegrate.Err))
                        {
                            MessageBox.Show(CacheManager.OrderIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CacheManager.OrderIntegrate.Err = "";
                        }
                    }

                    #region addby xuewj 2010-03-12 HL7��Ϣ send��op---receiver��of

                    if (this.iop == null)
                    {
                        this.iop = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IOP)) as FS.HISFC.BizProcess.Interface.IHE.IOP;
                    }
                    if (this.iop != null)
                    {
                        this.iop.PlaceOrder(alOrders);
                    }

                    #endregion

                }
                #endregion
            }

            #endregion

            /// CacheManager.OrderIntegrate.fee.Commit();
            //FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.QueryOrder();

            string showTip = "������δ���ҽ����\r\n\r\n";
            bool show = false;

            if (fpSpread.Sheets[0].Rows.Count > 0) //����ҽ��
            {
                showTip += "����ҽ��\r\n\r\n";
                show = true;
            }

            if (fpSpread.Sheets[1].Rows.Count > 0) //����ҽ��
            {
                showTip += "��ʱҽ��\r\n\r\n";
                show = true;
            }
            if (show)
            {
                Classes.Function.ShowBalloonTip(3, "ע��", showTip + "\r\n", ToolTipIcon.Warning);
            }

            if (!string.IsNullOrEmpty(lackFeeInfo))
            {
                if (messType == FS.HISFC.Models.Base.MessType.N && !lackDealModel)
                {
                    MessageBox.Show("���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, "Ƿ����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lackFeeInfo = "";
                }
                else
                {
                    Components.Order.Classes.Function.ShowBalloonTip(10, "Ƿ����ʾ", "���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, System.Windows.Forms.ToolTipIcon.Info);
                }
            }

            #region ҩ���̵���ʾ
            if (!string.IsNullOrEmpty(stockDeptId))
            {
                string strSql = @"select * from pha_com_checkstatic t where t.drug_dept_code = '{0}' and t.foper_time <= sysdate and t.check_state = '0'";
                strSql = string.Format(strSql, stockDeptId);
                if (FS.FrameWork.Function.NConvert.ToInt32(CacheManager.InOrderMgr.ExecSqlReturnOne(strSql)) > 0)
                {
                    MessageBox.Show("��ܰ��ʾ,ҩ���̵��ڼ䣬������ʹ��ҩƷ�����ݻ���ҩ��ȡҩ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// �Ƿ���Ҫ��ҽ�������"@"���ı�־
        /// </summary>
        /// <param name="operFlag"></param>
        /// <param name="isShowSubtblFlag">�Ƿ���ʾ����</param>
        /// <param name="sender"></param>
        void ucSubtblManager1_ShowSubtblFlag(string operFlag, bool isShowSubtblFlag, object sender)
        {
            string s = this.CurrentCellName.Text;
            if (!isShowSubtblFlag)
            {
                //����ҽ����־
                if (s.Substring(0, 1) == "@")
                {
                    this.CurrentCellName.Text = s.Substring(1);
                }
            }
            else
            {
                if (s.Substring(0, 1) != "@")
                {
                    this.CurrentCellName.Text = "@" + s;
                }
            }
            if (this.dockingManager != null)
                this.dockingManager.HideAllContents();
        }

        #region �������ڴ�����

        public Crownwood.Magic.Docking.DockingManager dockingManager;
        private Crownwood.Magic.Docking.Content content;
        private Crownwood.Magic.Docking.WindowContent wc;
        ucSubtblManager ucSubtblManager1 = null;

        /// <summary>
        /// ���Ĺ�����
        /// </summary>
        public void DockingManager()
        {
            this.dockingManager = new Crownwood.Magic.Docking.DockingManager(this, Crownwood.Magic.Common.VisualStyle.IDE);
            this.dockingManager.OuterControl = this.panelMain;		//��OuterControl�����Ŀؼ�����ͣ�����ڵ�Ӱ��

            content = new Crownwood.Magic.Docking.Content(this.dockingManager);
            content.Control = ucSubtblManager1;

            Size ucSize = content.Control.Size;

            content.Title = "���Ĺ���";
            content.FullTitle = "���Ĺ���";
            content.AutoHideSize = ucSize;
            content.DisplaySize = ucSize;

            this.dockingManager.Contents.Add(content);
        }
        #endregion

        /// <summary>
        /// ��ѯҽ��
        /// </summary>
        private void QueryOrder()
        {
            if (this.alpatientinfos == null) return;
            this.fpSpread.ChildViewCreated += new FarPoint.Win.Spread.ChildViewCreatedEventHandler(fpSpread_ChildViewCreated);

            this.myShowOrderType = FS.HISFC.Models.Order.EnumType.SHORT;//��ʱҽ����ʼ��
            this.fpSpread.Sheets[1].DataSource = CreateDataSetShort(this.alpatientinfos);

            this.myShowOrderType = FS.HISFC.Models.Order.EnumType.LONG;//����ҽ����ʼ��
            this.fpSpread.Sheets[0].DataSource = CreateDataSetLong(this.alpatientinfos);

            this.fpSpread.Sheets[0].Columns[0].Visible = false;
            this.fpSpread.Sheets[0].Columns[2].Locked = true;
            this.fpSpread.Sheets[0].Columns[3].Locked = true;
            this.fpSpread.Sheets[0].Columns[7].ForeColor = Color.Red;
            this.fpSpread.Sheets[0].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            Classes.Function.DrawCombo(this.fpSpread.Sheets[0], (int)EnumOrderColumns.CombNo, (int)EnumOrderColumns.ConbFlag, 1);

            this.fpSpread.Sheets[1].Columns[0].Visible = false;
            this.fpSpread.Sheets[1].Columns[2].Locked = true;
            this.fpSpread.Sheets[1].Columns[3].Locked = true;
            this.fpSpread.Sheets[1].GrayAreaBackColor = Color.WhiteSmoke;
            this.fpSpread.Sheets[1].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            Classes.Function.DrawCombo(this.fpSpread.Sheets[1], (int)EnumOrderColumns.CombNo, (int)EnumOrderColumns.ConbFlag, 1);


            this.ExpandAll();//չ��

            this.RefreshView();//ˢ���б���Ϣ

            SetQuitTimes();
        }

        /// <summary>
        /// Ӥ���ķ����Ƿ������ȡ����������
        /// </summary>
        private string motherPayAllFee = "";

        /// <summary>
        /// �����˷Ѵ���
        /// </summary>
        private void SetQuitTimes()
        {
            if (this.defaultQuitQty == 0)
            {
            }
            else if (this.defaultQuitQty == 1)
            {
                string patientNo = "";
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = CacheManager.ContrlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
                }

                //�Ƿ���ڿڷ�ҩ
                bool isPO = false;

                for (int k = 0; k <= 1; k++)
                {
                    for (int i = 0; i < this.fpSpread.Sheets[k].Rows.Count; i++)
                    {
                        this.fpSpread.BackColor = System.Drawing.Color.Azure;
                        try
                        {
                            FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[k].GetChildView(i, 0);

                            if (sv != null)
                            {
                                for (int j = 0; j < sv.Rows.Count; j++)
                                {
                                    //ֹͣʱ��
                                    if (sv.Cells[j, (int)EnumOrderColumns.EndDate].Text != "")
                                    {
                                        DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(sv.Cells[j, (int)EnumOrderColumns.EndDate].Text);

                                        FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Text);

                                        #region ����ҽ��

                                        if (order.OrderType.IsDecompose)
                                        {
                                            if (order.Status == 4)
                                            {
                                                sv.RowHeader.Rows[j].Label = "����";
                                                sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                                sv.RowHeader.Rows[j].ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                sv.RowHeader.Rows[j].ForeColor = Color.Black;

                                                ArrayList alExeOrder = CacheManager.InOrderMgr.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                                                int dcTimes = 0;

                                                foreach (FS.HISFC.Models.Order.ExecOrder exe in alExeOrder)
                                                {
                                                    if (!SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(exe.Order.Usage.ID))
                                                    {
                                                        if (exe.IsCharge)
                                                        {
                                                            if (exe.DateUse > order.EndTime)
                                                            {
                                                                //����Ӧ����ʾӦ�˵�ִ�е�����
                                                                dcTimes++;

                                                                #region
                                                                //Ӥ���ķ���������������� 
                                                                //if (motherPayAllFee == "1")
                                                                //{
                                                                //    patientNo = this.radtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                                //    //û���ҵ�ĸ�׵�סԺ�ţ���Ϊ�˻��߲���Ӥ��
                                                                //    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                                //    {
                                                                //        patientNo = exe.Order.Patient.ID;
                                                                //    }
                                                                //}
                                                                //else
                                                                //{
                                                                //    patientNo = exe.Order.Patient.ID;
                                                                //}
                                                                //ArrayList feeItemListTempArray = feeManager.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                                //if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                                //{
                                                                //    continue;
                                                                //}
                                                                //else
                                                                //{
                                                                //    dcTimes++;
                                                                //}
                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                //��������ֹͣ����Ĭ����1�����ң�������Ժ�������Ժ����Ҫ��ȡ�������
                                                                if (order.Item.SysClass.ID.ToString() == "UN"
                                                                    && exe.DateUse.Date == order.EndTime.Date
                                                                    && exe.DateUse.Date != order.MOTime.Date)
                                                                {
                                                                    dcTimes++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (this.isQuitPOQty)
                                                    {
                                                        if (exe.DateUse > order.EndTime && exe.IsCharge)
                                                        {
                                                            //����Ӧ����ʾӦ�˵�ִ�е�����
                                                            dcTimes++;

                                                            #region
                                                            //Ӥ���ķ���������������� 
                                                            //if (motherPayAllFee == "1")
                                                            //{
                                                            //    patientNo = this.radtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                            //    //û���ҵ�ĸ�׵�סԺ�ţ���Ϊ�˻��߲���Ӥ��
                                                            //    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                            //    {
                                                            //        patientNo = exe.Order.Patient.ID;
                                                            //    }
                                                            //}
                                                            //else
                                                            //{
                                                            //    patientNo = exe.Order.Patient.ID;
                                                            //}
                                                            //ArrayList feeItemListTempArray = feeManager.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                            //if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                            //{
                                                            //    continue;
                                                            //}
                                                            //else
                                                            //{
                                                            //    dcTimes++;
                                                            //}
                                                            #endregion
                                                        }

                                                    }
                                                    else
                                                    {
                                                        isPO = true;
                                                    }
                                                }

                                                //��ʶ ��ʶ
                                                if (!order.OrderType.isCharge || order.Item.ID == "999")
                                                {
                                                    //sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                                }
                                                sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag = dcTimes;
                                                //�˴������յĴ���
                                                sv.RowHeader.Rows[j].Tag = dcTimes;

                                                if (dcTimes > 0)
                                                {
                                                    sv.RowHeader.Rows[j].Label = "��" + dcTimes.ToString();
                                                    sv.RowHeader.Rows[j].BackColor = Color.Pink;
                                                }
                                                else
                                                {
                                                    sv.RowHeader.Rows[j].Label = "��" + dcTimes.ToString();
                                                }
                                            }
                                        }
                                        #endregion

                                        #region ��ʱҽ��

                                        else
                                        {
                                            ArrayList alExeOrder = CacheManager.InOrderMgr.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                                            int dcTimes = 0;

                                            foreach (FS.HISFC.Models.Order.ExecOrder exe in alExeOrder)
                                            {
                                                if (!exe.Order.OrderType.IsDecompose && exe.IsCharge)
                                                {
                                                    //Ӥ���ķ���������������� 
                                                    if (motherPayAllFee == "1")
                                                    {
                                                        patientNo = CacheManager.RadtIntegrate.QueryBabyMotherInpatientNO(exe.Order.Patient.ID);
                                                        //û���ҵ�ĸ�׵�סԺ�ţ���Ϊ�˻��߲���Ӥ��
                                                        if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                                                        {
                                                            patientNo = exe.Order.Patient.ID;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        patientNo = exe.Order.Patient.ID;
                                                    }

                                                    ArrayList feeItemListTempArray = CacheManager.InPatientFeeMgr.GetItemListByExecSQN(patientNo, exe.ID, exe.Order.Item.ItemType);

                                                    if (feeItemListTempArray == null || feeItemListTempArray.Count <= 0)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        dcTimes++;
                                                    }
                                                }
                                            }

                                            //��ʶ ��ʶ
                                            if (!order.OrderType.isCharge || order.Item.ID == "999")
                                            {
                                                //sv.Cells[j, (int)EnumOrderColumns.OrderType].Tag = "NO";
                                            }

                                            //����Ĭ�϶���ȫ��
                                            //int dcTimes = 1;
                                            //if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(order.Usage.ID))
                                            //{
                                            //    if (!this.isQuitPOQty)
                                            //    {
                                            //        dcTimes = 0;
                                            //        isPO = true;
                                            //    }
                                            //}

                                            sv.Cells[j, (int)EnumOrderColumns.MoOrderID].Tag = dcTimes;
                                            //�˴������յĴ���
                                            sv.RowHeader.Rows[j].Tag = dcTimes;

                                            if (dcTimes > 0)
                                            {
                                                sv.RowHeader.Rows[j].Label = "��" + dcTimes.ToString();
                                                sv.RowHeader.Rows[j].BackColor = Color.Pink;
                                            }
                                            else
                                            {
                                                sv.RowHeader.Rows[j].Label = "��" + dcTimes.ToString();
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        catch { }

                        if (isPO)
                        {
                            Classes.Function.ShowBalloonTip(3, "��ʾ", "\r\n�ڷ�ҩ�����˷�������ѡ���˷Ѵ�����\r\n", ToolTipIcon.Info);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// չ��ȫ���ڵ�
        /// </summary>
        private void ExpandAll()
        {
            for (int j = 0; j < this.fpSpread.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpSpread.Sheets[j].Rows.Count; i++)
                {
                    this.fpSpread.Sheets[j].ExpandRow(i, true);
                    SheetView sv = this.fpSpread.Sheets[j].GetChildView(i, 0);
                    this.SetChildViewStyle(sv);
                }
            }
        }

        /// <summary>
        /// �������� (û���˰ɣ�
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iSheet"></param>
        /// <returns></returns>
        [Obsolete("û���˰ɣ�", true)]
        private int GetColumnIndex(string name, int iSheet)
        {
            DataTable dt = null;
            if (iSheet == 0)
            {
                dt = dtChild1;
            }
            else
            {
                dt = dtChild2;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == name)
                    return i;
            }
            MessageBox.Show("ȱ����" + Name);
            return -1;
        }

        /// <summary>
        /// ���س���ҽ����dataSet
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        private DataSet CreateDataSetLong(ArrayList alPatient)
        {
            DataTable dtMain;
            DataSet myDataSet;

            //���崫��DataSet
            myDataSet = new DataSet();
            myDataSet.EnforceConstraints = false;//�Ƿ���ѭԼ������
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtInt = System.Type.GetType("System.Int32");
            //�����********************************************************
            //Main Table

            dtMain = myDataSet.Tables.Add("TableMain");
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            dtMain.Columns.AddRange(new DataColumn[] 
            { 
                new DataColumn("ID", dtStr), 
                new DataColumn("���", dtBool), 
                new DataColumn("��������", dtStr), 
                new DataColumn("����", dtStr),
                new DataColumn("�Ա�",dtStr),
                new DataColumn("����",dtStr),
                new DataColumn("����ҽ��",dtStr),
                new DataColumn ("��ܰ��ʾ��������ֽⳤ��ҽ����",dtStr )
            });
            //ChildTable1

            dtChild1 = myDataSet.Tables.Add("TableChild1");
            dtChild1.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",dtStr),
                new DataColumn("ҽ����ˮ��", dtStr),
                new DataColumn("��Ϻ�", dtStr),
                new DataColumn("���", dtBool),
                new DataColumn("ϵͳ���",dtStr),
                new DataColumn("ҽ������", dtStr),
                new DataColumn("��", dtStr),
                new DataColumn("���", dtStr),
                new DataColumn("ÿ����", dtStr),
                new DataColumn("Ƶ��", dtStr),
                new DataColumn("�÷�", dtStr),
                new DataColumn("����", dtStr),
                new DataColumn("������", dtStr),
                new DataColumn("ҽ������", dtStr),
                new DataColumn("��", dtBool),
                new DataColumn("��ʼʱ��", dtStr),
                new DataColumn("ֹͣʱ��", dtStr),
                new DataColumn("����ҽ��", dtStr),
                new DataColumn("ִ�п���", dtStr),
                new DataColumn("����ʱ��", dtStr),
                new DataColumn("ֹͣҽ��", dtStr),
                new DataColumn("��ע", dtStr),
                new DataColumn("˳���", dtStr),
                new DataColumn("��ע",dtStr),
                new DataColumn("״̬",dtStr),
                new DataColumn("Ƥ��",dtStr),
                new DataColumn("����",dtStr)

            });

            this.OrderId.ID = "1";
            this.ComboNo.ID = "2";

            this.fpSpread.Sheets[0].RowCount = 0;

            string tempCombNo = "";
            this.LongOrderCount = 0;
            FS.HISFC.Models.RADT.PatientInfo p = null;
            for (int i = 0; i < alPatient.Count; i++)
            {
                p = (FS.HISFC.Models.RADT.PatientInfo)alPatient[i];

                //��ѯδ��˵�ҽ��--�жϲ�ѯҽ������
                ArrayList al = CacheManager.InOrderMgr.QueryIsConfirmOrder(p.ID, myShowOrderType, false);

                al = this.DealOperationOrder(al);

                if (al.Count > 0)
                {
                    this.LongOrderCount = this.LongOrderCount + al.Count;
                    //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
                    //dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, p.PVisit.PatientLocation.Bed.ID.Substring(4) });//�����
                    dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, p.PVisit.PatientLocation.Bed.ID.Substring(4), p.Sex.Name, p.Age.ToString(),p.PVisit.AttendingDoctor.Name,""});
                    for (int j = 0; j < al.Count; j++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = al[j] as FS.HISFC.Models.Order.Inpatient.Order;

                        if (o.IsPermission) //�Ѿ���Ȩ�޵�ҩƷ
                            o.Item.Name = "���̡�" + o.Item.Name;

                        # region ͬһ�����ȡһ�ξͿ�����
                        if (tempCombNo != o.Combo.ID)
                        {
                            int count = CacheManager.InOrderMgr.QuerySubtbl(o.Combo.ID).Count;
                            tempCombNo = o.Combo.ID;
                            if (count > 0)
                                o.Item.Name = "@" + o.Item.Name; //��ʾ����
                        }
                        # endregion
                        if (o.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))//ҩƷ
                        {
                            FS.HISFC.Models.Pharmacy.Item item = o.Item as FS.HISFC.Models.Pharmacy.Item;

                            dtChild1.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                o.DoseOnce.ToString()+item.DoseUnit ,
                                o.Frequency.ID,
                                o.Usage.Name,
                                o.Item.Qty ==0 ? "":(o.Item.Qty.ToString()+o.Unit),
                                o.FirstUseNum,
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });

                        }
                        else if (o.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                        {

                            dtChild1.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                "" ,
                                o.Frequency.ID,
                                "",
                                o.Item.Qty.ToString()+o.Unit,
                                o.FirstUseNum,
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });
                        }
                    }
                }
            }
            //�������ʾ
            myDataSet.Relations.Add("TableChild1", dtMain.Columns["ID"], dtChild1.Columns["ID"]);

            return myDataSet;
        }

        /// <summary>
        /// ������ʱҽ����DataSet
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        private DataSet CreateDataSetShort(ArrayList alPatient)
        {
            DataTable dtMain;
            DataSet myDataSet;

            //ataTable dtChild1;
            //���崫��DataSet
            myDataSet = new DataSet();
            myDataSet.EnforceConstraints = false;//�Ƿ���ѭԼ������

            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtInt = System.Type.GetType("System.Int32");
            //�����********************************************************

            //Main Table
            dtMain = myDataSet.Tables.Add("TableMain");
            //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
            dtMain.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("ID", dtStr), 
                new DataColumn("���", dtBool), 
                new DataColumn("��������", dtStr), 
                new DataColumn("����", dtStr),
                new DataColumn("�Ա�",dtStr),
                new DataColumn("����",dtStr),
                new DataColumn("����ҽ��",dtStr),
            });

            //ChildTable1
            dtChild2 = myDataSet.Tables.Add("TableChild1");
            dtChild2.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",dtStr),
                new DataColumn("ҽ����ˮ��", dtStr),
                new DataColumn("��Ϻ�", dtStr),
                new DataColumn("���", dtBool),
                new DataColumn("ϵͳ���",dtStr),
                new DataColumn("ҽ������", dtStr),
                new DataColumn("��", dtStr),
                new DataColumn("���", dtStr),
                new DataColumn("ÿ����", dtStr),  
                new DataColumn("Ƶ��", dtStr),
                new DataColumn("�÷�", dtStr),
                new DataColumn("����", dtStr),
                new DataColumn("����", dtStr), 
                new DataColumn("ҽ������", dtStr),
                new DataColumn("��", dtBool),	  
                new DataColumn("��ʼʱ��", dtStr),
                new DataColumn("ֹͣʱ��", dtStr),
                new DataColumn("����ҽ��", dtStr),
                new DataColumn("ִ�п���", dtStr),
                new DataColumn("����ʱ��", dtStr),
                new DataColumn("ֹͣҽ��", dtStr),
                new DataColumn("��ע", dtStr),
                new DataColumn("˳���", dtStr),
                new DataColumn("��ע",dtStr),
                new DataColumn("״̬",dtStr),
                new DataColumn("Ƥ��",dtStr),
                new DataColumn("����",dtStr)
            });
            this.OrderId.ID = "1";
            this.ComboNo.ID = "2";


            this.fpSpread.Sheets[1].RowCount = 0;

            string tempCombNo = "";
            this.ShortOrderCount = 0;
            for (int i = 0; i < alPatient.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = (FS.HISFC.Models.RADT.PatientInfo)alPatient[i];
                //��ѯδ��˵�ҽ��--�жϲ�ѯҽ������
                ArrayList al = CacheManager.InOrderMgr.QueryIsConfirmOrder(p.ID, myShowOrderType, false);	//��ѯδ��˵�ҽ��

                al = this.DealOperationOrder(al);

                if (al.Count > 0)
                {
                    this.ShortOrderCount = this.ShortOrderCount + al.Count;

                    //{C3C32101-297D-40c1-97BA-46938537002B}  ��λ�Ž�ȡ
                    string bedNO = p.PVisit.PatientLocation.Bed.ID;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }
                    //{4ED608A1-4AAA-433a-B0F8-ABE8EA029E1C}
                    dtMain.Rows.Add(new Object[] { p.ID, false, p.Name, bedNO, p.Sex.Name, p.Age.ToString(), p.PVisit.AttendingDoctor.Name });//�����
                    for (int j = 0; j < al.Count; j++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = al[j] as FS.HISFC.Models.Order.Inpatient.Order;

                        if (o.IsPermission) //
                            o.Item.Name = "���̡�" + o.Item.Name;
                        

                        # region ͬһ�����ȡһ�ξͿ�����
                        if (tempCombNo != o.Combo.ID)
                        {
                            int count = CacheManager.InOrderMgr.QuerySubtbl(o.Combo.ID).Count;
                            tempCombNo = o.Combo.ID;
                            if (count > 0)
                                o.Item.Name = "@" + o.Item.Name; //��ʾ����
                        }
                        # endregion
                        if (o.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                        {
                            FS.HISFC.Models.Pharmacy.Item item = o.Item as FS.HISFC.Models.Pharmacy.Item;

                            dtChild2.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                o.DoseOnce.ToString()+item.DoseUnit ,
                                o.Frequency.ID,
                                o.Usage.Name,
                                o.Item.Qty ==0 ? "":(o.Item.Qty.ToString()+o.Unit),
                                o.HerbalQty==0 ? "":o.HerbalQty.ToString(),
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });

                        }
                        else if (o.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                        {
                            dtChild2.Rows.Add(new Object[] 
                            {
                                o.Patient.ID,
                                o.ID,
                                o.Combo.ID,
                                false,
                                o.Item.SysClass.Name,
                                o.Item.Name,
                                "",
                                o.Item.Specs,
                                "" ,
                                o.Frequency.ID,
                                "",
                                o.Item.Qty.ToString()+o.Unit,
                                "",
                                o.OrderType.Name,
                                o.IsEmergency,
                                o.BeginTime.ToString("MM-dd HH:mm"),
                                o.EndTime.ToString("MM-dd HH:mm") == "01-01 00:00" ? "":o.EndTime.ToString("MM-dd HH:mm"),
                                o.ReciptDoctor.Name,
                                o.ExeDept.Name,
                                o.MOTime,
                                o.DCOper.Name,
                                o.Memo,
                                o.SortID,
                                o.Note,
                                Classes.Function.OrderStatus(o.Status),
                                CacheManager.OutOrderMgr.TransHypotest(o.HypoTest),
                                o.Dripspreed
                            });
                        }

                    }
                }
            }
            //����
            myDataSet.Relations.Add("TableChild1", dtMain.Columns["ID"], dtChild2.Columns["ID"]);

            return myDataSet;
        }

        /// <summary>
        /// ����������ʾ��ҽ��
        /// </summary>
        /// <param name="alOrders"></param>
        /// <returns></returns>
        private ArrayList DealOperationOrder(ArrayList alOrders)
        {
            ArrayList alNews = new ArrayList();

            if (this.currentDept == null)
            {
                this.currentDept = hsDepts[(this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID] as FS.HISFC.Models.Base.Department;
            }

            if (this.currentDept == null)
            {
                MessageBox.Show("��ȡ��ǰ���ҳ���");
                return null;
            }

            //�����ã�Ĭ�ϲ鿴ȫ��,ԭ�㷨����Ч
            if (this.speOrderType.Length <= 0)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    /*
                     *0 ��ͨ
                     *1 ����
                     *2 ����
                     *3 ICU
                     *4 CCU
                     */
                    //��½Ϊ�����ҵĻ�ʿ
                    //���ֿ�����ҽ����ֹͣ��ҽ��
                    #region �¿�����ҽ��
                    if (order.Status == 0)
                    {
                        if (this.currentDept.SpecialFlag == "1")
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "2" ||
                                    (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                        else
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "2" &&
                                     (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                    }
                    #endregion

                    #region ֹͣ��ҽ��
                    else
                    {
                        //ϵͳû�д�ֹͣ���ң�����ֹͣ��ҽ�� �����˶����Կ��� ���
                        order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                        alNews.Add(order);
                    }
                    #endregion
                }
            }
            else
            {
                //ֻҪ����һ��������Ϊ���Կ���
                string[] speStr = this.speOrderType.Split(',');

                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
                {
                    if (order.SpeOrderType.Length <= 0)
                    {
                        /*
                         *0 ��ͨ
                         *1 ����
                         *2 ����
                         *3 ICU
                         *4 CCU
                         */
                        //��½Ϊ�����ҵĻ�ʿ
                        if (this.currentDept.SpecialFlag == "1")
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "2" ||
                                    (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag == "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                        else
                        {
                            if ((hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "2" &&
                                     (hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department).SpecialFlag != "1")
                            {
                                order.ReciptDept = hsDepts[order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                                alNews.Add(order);
                            }
                        }
                    }
                    else
                    {
                        bool isAdd = false;

                        foreach (string s in speStr)
                        {
                            if (order.SpeOrderType.IndexOf(s) >= 0)
                            {
                                isAdd = true;
                                break;
                            }
                        }

                        if (isAdd)
                            alNews.Add(order);
                    }
                }
            }

            return alNews;
        }

        /// <summary>
        /// ����ҽ����ϸ��ʾ
        /// </summary>
        /// <param name="sv"></param>
        public void SetChildViewStyle(FarPoint.Win.Spread.SheetView sv)
        {
            this.SetChildViewStyle(sv, true);
        }

        /// <summary>
        /// ����ҽ����ϸ��ʾ
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="SetChildViewStyle"></param>
        public void SetChildViewStyle(FarPoint.Win.Spread.SheetView sv, bool SetChildViewStyle)
        {
            try
            {
                //Make the header font italic
                sv.ColumnHeader.DefaultStyle.Font = this.fpSpread.Font;
                sv.ColumnHeader.DefaultStyle.Border = new EmptyBorder();
                sv.ColumnHeader.DefaultStyle.BackColor = Color.White;
                sv.ColumnHeader.DefaultStyle.ForeColor = Color.Black;
                //Change the sheet corner color
                sv.SheetCornerStyle.BackColor = Color.White;
                sv.SheetCornerStyle.Border = new EmptyBorder();

                //Clear the autotext
                sv.RowHeader.AutoText = HeaderAutoText.Blank;

                sv.RowHeader.DefaultStyle.BackColor = Color.Honeydew;
                sv.RowHeader.DefaultStyle.ForeColor = Color.Black;

                sv.ColumnHeaderVisible = true;
                sv.RowHeaderVisible = SetChildViewStyle;
                sv.RowHeaderAutoText = HeaderAutoText.Numbers;
                for (int i = 0; i < sv.RowCount; i++)
                {
                    sv.Rows[i].Height = 20;
                }
                //sv.CellChanged += new SheetViewEventHandler(sv_CellChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sv.DataAutoSizeColumns = false;
            sv.OperationMode = OperationMode.SingleSelect;


            //hide or show the ID column
            sv.Columns[(int)EnumOrderColumns.PatientID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.MoOrderID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.CombNo].Visible = false;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Visible = true;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Width = 15;
            sv.Columns[(int)EnumOrderColumns.CheckFlag].Locked = true;

            sv.Columns[(int)EnumOrderColumns.SysClassName].Width = 60;
            sv.Columns[(int)EnumOrderColumns.SysClassName].Locked = true;

            sv.Columns[(int)EnumOrderColumns.ItemName].Width = 200;
            sv.Columns[(int)EnumOrderColumns.ItemName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.ConbFlag].Width = 15;
            sv.Columns[(int)EnumOrderColumns.ConbFlag].Locked = true;
            sv.Columns[(int)EnumOrderColumns.Specs].Width = 62;
            sv.Columns[(int)EnumOrderColumns.Specs].Locked = true;
            sv.Columns[(int)EnumOrderColumns.DoseOne].Width = 48;
            sv.Columns[(int)EnumOrderColumns.DoseOne].Locked = true;
            sv.Columns[(int)EnumOrderColumns.FrequencyID].Width = 37;
            sv.Columns[(int)EnumOrderColumns.FrequencyID].Locked = true;
            sv.Columns[(int)EnumOrderColumns.UsageName].Width = 33;
            sv.Columns[(int)EnumOrderColumns.UsageName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.Qty].Width = 35;
            sv.Columns[(int)EnumOrderColumns.Qty].Locked = true;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Width = 42;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Font = new Font(this.fpSpread.Font.Name, this.fpSpread.Font.Size, System.Drawing.FontStyle.Bold);
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sv.Columns[(int)EnumOrderColumns.FuOrFirstDays].Locked = true;
            sv.Columns[(int)EnumOrderColumns.OrderType].Width = 59;
            sv.Columns[(int)EnumOrderColumns.OrderType].Locked = true;
            sv.Columns[(int)EnumOrderColumns.EmergencyFlag].Width = 19;
            sv.Columns[(int)EnumOrderColumns.EmergencyFlag].Visible = false;
            sv.Columns[(int)EnumOrderColumns.BeginDate].Width = 63;
            sv.Columns[(int)EnumOrderColumns.BeginDate].Locked = true;
            sv.Columns[(int)EnumOrderColumns.EndDate].Width = 63;
            sv.Columns[(int)EnumOrderColumns.EndDate].Locked = true;
            sv.Columns[(int)EnumOrderColumns.RecipeDoctName].Width = 59;
            sv.Columns[(int)EnumOrderColumns.RecipeDoctName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.ExecDeptName].Width = 59;
            sv.Columns[(int)EnumOrderColumns.ExecDeptName].Locked = true;
            sv.Columns[(int)EnumOrderColumns.MoDate].Width = 59;
            sv.Columns[(int)EnumOrderColumns.MoDate].Locked = true;

            sv.Columns[(int)EnumOrderColumns.MoDate].Visible = false;
            sv.Columns[(int)EnumOrderColumns.SortID].Visible = false;
            sv.Columns[(int)EnumOrderColumns.Memo].Font = new Font(this.fpSpread.Font.Name, this.fpSpread.Font.Size, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// ˢ���б���Ϣ
        /// </summary>
        protected void RefreshView()
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < this.fpSpread.Sheets[k].Rows.Count; i++) //����ҽ��-��ʱҽ��
                {
                    this.fpSpread.BackColor = System.Drawing.Color.Azure;
                    try
                    {
                        FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[k].GetChildView(i, 0);
                        if (sv != null)
                        {
                            sv.Columns[(int)EnumOrderColumns.Specs].Font = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
                            sv.Columns[(int)EnumOrderColumns.DoseOne].Font = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
                            for (int j = 0; j < sv.Rows.Count; j++)
                            {
                                //ҽ����Ŀ
                                string note = sv.Cells[j, (int)EnumOrderColumns.Tip].Text;//��ע
                                if (sv.Cells.Get(j, (int)EnumOrderColumns.MoState).Text == "ֹͣ/ȡ��") sv.Rows[j].BackColor = Color.FromArgb(255, 222, 222);//ҽ��״̬��ҽ���������
                                sv.SetNote(j, (int)EnumOrderColumns.ItemName, note);
                                if ((bool)sv.Cells[j, (int)EnumOrderColumns.EmergencyFlag].Value)
                                {
                                    sv.Rows[j].Label = "��";
                                    sv.RowHeader.Rows[j].BackColor = System.Drawing.Color.Pink;
                                }
                                int hypotest = 0;
                                if (sv.Cells[j, (int)EnumOrderColumns.Hypotest].Text == "����")
                                {
                                    hypotest = 3;
                                }
                                else if (sv.Cells[j, (int)EnumOrderColumns.Hypotest].Text == "����")
                                {
                                    hypotest = 4;
                                }
                                //int hypotest = FS.FrameWork.Function.NConvert.ToInt32(sv.Cells[j, 24].Text);//Ƥ��
                                #region û���˰ɣ���
                                string sTip = "�費��Ƥ��";//Function.TipHypotest;
                                if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length > 3)
                                {
                                    if ((sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3) == "�ۣ���"
                                    || sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3) == "�ۣ���"))
                                    {
                                        sv.Cells[j, (int)EnumOrderColumns.ItemName].Text = sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(0, sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - 3);
                                    }
                                }
                                try
                                {
                                    if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length > 3)
                                        if (sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - sTip.Length, sTip.Length) == sTip)
                                            sv.Cells[j, (int)EnumOrderColumns.ItemName].Text = sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Substring(0, sv.Cells[j, (int)EnumOrderColumns.ItemName].Text.Length - sTip.Length);
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.Message);
                                }
                                #endregion
                                sv.Cells[j, (int)EnumOrderColumns.ItemName].ForeColor = Color.Black;
                                if (hypotest == 3)
                                {
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].Text += "�ۣ���";//Ƥ��
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].ForeColor = Color.Red;
                                }
                                else if (hypotest == 4)
                                {
                                    sv.Cells[j, (int)EnumOrderColumns.ItemName].Text += "�ۣ���";
                                }
                                else if (hypotest == 2)
                                {
                                }

                                //������ʾ��ע����ע�ں����Ѿ�����ʾ��
                                //��ʾ˳���
                                //if (sv.RowHeader.Cells[j, 0].Text != "��")
                                //    sv.RowHeader.Cells[j, 0].Text = sv.Cells[j, 21].Text;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ˢ��ҽ����ע��Ϣ����", "Sorry");
                    }
                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �ú���û��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpSpread_ChildViewCreated(object sender, FarPoint.Win.Spread.ChildViewCreatedEventArgs e)
        {
            this.SetChildViewStyle(e.SheetView);
        }

        void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader || e.Row < 0)
                return;

            //�жϵ�ǰ��ͣ�������Ƿ�����ʾ ��δ��ʾ ����ʾͣ������
            try
            {
                if (e.View.Sheets[0].Columns[2].Label == "��Ϻ�") //��childtable1
                {
                    if (this.content != null && this.content.Visible == false)
                    {
                        if (wc == null && this.dockingManager != null)
                        {
                            wc = this.dockingManager.AddContentWithState(content, Crownwood.Magic.Docking.State.DockRight);
                            this.dockingManager.AddContentToWindowContent(content, wc);
                        }
                        if (this.dockingManager != null)
                            this.dockingManager.ShowContent(this.content);
                    }
                    if (this.ucSubtblManager1 != null && !e.RowHeader && !e.ColumnHeader)		//������б������б���
                    {
                        ucSubtblManager1.OrderID = this.OrderId.Name;
                        ucSubtblManager1.ComboNo = this.ComboNo.Name;
                        this.CurrentCellName = e.View.Sheets[0].Cells[e.View.Sheets[0].ActiveRowIndex, (int)EnumOrderColumns.ItemName];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        FarPoint.Win.Spread.CellClickEventArgs cellClickEvent = null;
        int curRow = 0;

        void fpSpread_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0)
                return;
            if (e.ColumnHeader == true)
                return;
            if (e.Column > 0)
            {
                try
                {
                    int active = this.fpSpread.ActiveSheetIndex;
                    if (e.View.Sheets.Count <= active) active = 0;
                    curRow = active;
                    if (e.View.Sheets[active].Columns[2].Label == "��Ϻ�") //�ӱ�1
                    {
                        if (e.Button == MouseButtons.Left) //���
                        {
                            this.OrderId.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.OrderId.ID)].Text;
                            this.ComboNo.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.ComboNo.ID)].Text;
                            this.PatientId = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.PatientID].Text;//סԺ��ˮ��

                            if (e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                            {
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text = "False";
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor = Color.White;
                            }
                            else
                            {
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text = "True";
                                e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor = Color.Blue;
                            }

                            // {6B70B558-72C9-4DEF-874F-DABD0A9B5198}               ;
                            //{CB5C628A-EA63-41e7-9D38-3F3DF2E78834}

                            if (e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text.ToUpper() == "TRUE")
                            {
                                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                                order = CacheManager.InOrderMgr.QueryOneOrder(this.OrderId.Name);
                               
                                string flag =CacheManager.InOrderMgr.GetDrugSpecialFlag(order.Item.ID);
                                if (flag == "1")
                                    MessageBox.Show("��ҩƷ����A���߾�ʾҩƷ��ע��˶�ҩƷ���ơ�����÷���������ʹ��Ũ�ȼ����ٵȣ���˶ԣ���");
                                if (flag == "2")
                                    MessageBox.Show("��ҩƷ����B���߾�ʾҩƷ��ע��˶�ҩƷ���ơ�����÷��������ȣ���");
                                if (flag == "4")
                                    MessageBox.Show("��ҩƷ�����׻���ҩƷ��ע��˶�ҩƷ���ơ����ȣ���");

                            }


                            //������ϵ�ҽ��ѡ����Ϣ
                            for (int i = 0; i < e.View.Sheets[active].RowCount; i++)
                            {
                                if (e.View.Sheets[active].Cells[i, int.Parse(this.ComboNo.ID)].Text == this.ComboNo.Name
                                    && i != e.Row)
                                {
                                    e.View.Sheets[active].Cells[i, (int)EnumOrderColumns.CheckFlag].Text = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].Text;
                                    e.View.Sheets[active].Cells[i, (int)EnumOrderColumns.CheckFlag].BackColor = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.CheckFlag].BackColor;
                                }
                            }
                        }
                        else//�Ҽ�
                        {
                            this.OrderId.Name = e.View.Sheets[active].Cells[e.Row, int.Parse(this.OrderId.ID)].Text;
                            string strItemName = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.ItemName].Text;
                            this.PatientId = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.PatientID].Text;//סԺ��ˮ�� 
                            cellClickEvent = e;
                            ContextMenu menu = new ContextMenu();
                            MenuItem mnuTip = new MenuItem("��ע");//��ע
                            mnuTip.Click += new EventHandler(mnuTip_Click);

                            //ȡ���޸�ȡҩ���ҹ�����

                            //MenuItem mnuChangeDept = new MenuItem("�޸�ȡҩ����");//�޸�ȡҩ����
                            //mnuChangeDept.Click += new EventHandler(mnuChangeDept_Click);

                            //menu.MenuItems.Add(mnuChangeDept);


                            MenuItem mnuDcTimes = new MenuItem("�˷Ѵ���");
                            mnuDcTimes.Click += new EventHandler(mnuDcTimes_Click);

                            menu.MenuItems.Add(mnuTip);

                            //�������棬ֹͣʱ�䲻Ϊ��
                            if (active == 0 && e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.EndDate].Text != "")
                            {
                                menu.MenuItems.Add(mnuDcTimes);
                            }

                            this.fpSpread.ContextMenu = menu;
                            //Function.PopMenu(menu, obj.Item.ID, false);

                        }
                    }
                    else if (e.View.Sheets[active].Columns[2].Label == "��������")//����
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (e.View.Sheets[active].Cells[e.Row, 1].Text.ToUpper() == "TRUE")
                            {
                                e.View.Sheets[active].Cells[e.Row, 1].Text = "false";

                            }
                            else
                            {
                                e.View.Sheets[active].Cells[e.Row, 1].Text = "True";

                            }
                            //�����ӱ��ѡ��
                            try
                            {
                                FarPoint.Win.Spread.SheetView sv = e.View.Sheets[active].GetChildView(e.Row, 0);//(FarPoint.Win.Spread.SpreadView).GetChildWorkbooks()[e.Row];
                                if (sv.Columns[3].Label == "���")
                                {
                                    for (int i = 0; i < sv.Rows.Count; i++)
                                    {
                                        sv.Cells[i, 3].Text = e.View.Sheets[active].Cells[e.Row, 1].Text;
                                    }

                                    #region ע��
                                    //string A = "";
                                    //string B = "";
                                    //string C = "";

                                    //for (int i = 0; i < sv.Rows.Count; i++) 
                                    //{
                                    //    //e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text;
                                    //    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                                    //    string b = OrderId.ID;
                                    //    string a = e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text;
                                    //    return;
                                    //    order = CacheManager.InOrderMgr.QueryOneOrder(e.View.Sheets[active].Cells[e.Row, (int)EnumOrderColumns.MoOrderID].Text);

                                    //    string flag = CacheManager.InOrderMgr.GetDrugSpecialFlag(order.Item.ID);
                                    //    if (flag == "1")
                                    //        A += order.Item.Name + " ";
                                    //    if (flag == "2")
                                    //        B += order.Item.Name + " ";
                                    //    if (flag == "3")
                                    //        C += order.Item.Name + " ";
                                    //}

                                    //string message = "";
                                    //if (A != "")
                                    //    message += A + ",����A��";
                                    //if(B!="")
                                    //    message += B + ",����B��";
                                    //if(C != "")
                                    //    message += C + ",����C��";
                                    //if(message!="")
                                    //    MessageBox.Show(message+"��ΣҩƷ����˶ԣ���");
                                    #endregion

                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }
                            this.OrderId.Name = "";
                            this.ComboNo.Name = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// �˷Ѵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuDcTimes_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].GetChildView(curRow, 0);
            if (cellClickEvent == null)
            {
                return;
            }

            int dcTimes = 0;
            //�����˷ѵ�������
            int dcMaxTimes = 0;

            try
            {
                dcTimes = FS.FrameWork.Function.NConvert.ToInt32(cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag);

                dcMaxTimes = FS.FrameWork.Function.NConvert.ToInt32(cellClickEvent.View.Sheets[curRow].RowHeader.Rows[cellClickEvent.Row].Tag);

                //if (dcMaxTimes <= 0)
                //{
                //    MessageBox.Show("����ҽ��ֹͣʱ��֮��û����ִ��������");
                //    return;
                //}

                FS.FrameWork.WinForms.Controls.NeuNumericUpDown nuTimes = new FS.FrameWork.WinForms.Controls.NeuNumericUpDown();

                nuTimes.Font = new Font("����", 14, FontStyle.Bold);
                nuTimes.Dock = DockStyle.Fill;

                Form baseForm = new Form();
                baseForm.Size = new Size(300, 110);

                FS.FrameWork.WinForms.Controls.NeuLabel lblTip = new FS.FrameWork.WinForms.Controls.NeuLabel();
                lblTip.Text = "�˷Ѵ���Ĭ��Ϊҽ��ֹͣʱ����ִ�д���";
                lblTip.Dock = DockStyle.Bottom;

                FS.FrameWork.WinForms.Controls.NeuButton btOK = new FS.FrameWork.WinForms.Controls.NeuButton();
                btOK.Text = "��  ��";
                btOK.Dock = DockStyle.Bottom;
                btOK.Click += new EventHandler(btOK_Click);

                baseForm.Controls.Add(btOK);
                baseForm.Controls.Add(lblTip);
                baseForm.Controls.Add(nuTimes);

                //�������˷ѵ�����������ֽ����󣬲����˷�
                nuTimes.Maximum = 1000;
                nuTimes.Minimum = 0;

                if (cellClickEvent != null)
                {
                    baseForm.Text = cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.ItemName].Text;
                    nuTimes.Value = dcTimes;
                    //ҽ��ֹͣʱ��֮ǰ�Ĳ������Զ��˷ѣ����������ֻ���ֶ��˷�
                    //�������˷Ѵ�������������������п��˷�����ʱ����������
                    //nuTimes.Maximum = dcMaxTimes;
                    nuTimes.Focus();
                }
                baseForm.MaximizeBox = false;
                baseForm.MinimizeBox = false;
                baseForm.StartPosition = FormStartPosition.CenterScreen;
                baseForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btOK_Click(object sender, EventArgs e)
        {
            //FarPoint.Win.Spread.SheetView sv = this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].GetChildView(this.fpSpread.Sheets[this.fpSpread.ActiveSheetIndex].ActiveRowIndex, 0);

            if (cellClickEvent != null)
            {
                string combNo = cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.CombNo].Text;

                for (int i = 0; i < cellClickEvent.View.Sheets[curRow].Rows.Count; i++)
                {
                    //���С�����ҽ������Ϊ0
                    if (cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.OrderType].Tag != null && cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.OrderType].Tag.ToString() == "NO")
                    {
                        continue;
                    }

                    if (cellClickEvent.View.Sheets[curRow].Cells[i, (int)EnumOrderColumns.CombNo].Text == combNo)
                    {
                        cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag =
                            ((((sender as FS.FrameWork.WinForms.Controls.NeuButton).Parent as Form).Controls[2]) as FS.FrameWork.WinForms.Controls.NeuNumericUpDown).Value;

                        cellClickEvent.View.Sheets[curRow].RowHeader.Rows[i].Label = "��" + cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.MoOrderID].Tag.ToString();
                    }
                }
            }
            ((sender as FS.FrameWork.WinForms.Controls.NeuButton).Parent as Form).Close();
        }

        /// <summary>
        /// ��ע����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTip_Click(object sender, EventArgs e)
        {
            ucTip ucTip1 = new ucTip();
            string OrderID = this.OrderId.Name;
            int iHypotest = CacheManager.InOrderMgr.QueryOrderHypotest(OrderID);
            if (iHypotest == -1)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                return;
            }
            #region ��ҩƷҽ������ʾƤ��ҳ
            FS.HISFC.Models.Order.Order o = CacheManager.OutOrderMgr.QueryOneOrder(this.OrderId.Name);
            //if (o.Item.IsPharmacy == false)
            if (o.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                ucTip1.Hypotest = 1;
            }
            #endregion
            ucTip1.Tip = CacheManager.InOrderMgr.QueryOrderNote(OrderID);
            ucTip1.Hypotest = iHypotest;
            ucTip1.OKEvent += new myTipEvent(ucTip1_OKEvent);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTip1);
        }

        /// <summary>
        /// �޸�ִ�п����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangeDept_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.OrderIntegrate.QueryOneOrder(this.OrderId.Name);
            FS.FrameWork.Models.NeuObject dept = ucChangeStoreDept.ChangeStoreDept(order);
            if (dept == null) return;
            order.StockDept = dept;
            if (CacheManager.InOrderMgr.UpdateOrder(order) < 0)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                return;
            }
        }

        /// <summary>
        /// ��ע�¼�
        /// </summary>
        /// <param name="Tip"></param>
        /// <param name="Hypotest"></param>
        private void ucTip1_OKEvent(string Tip, int Hypotest)
        {
            if (CacheManager.InOrderMgr.UpdateFeedback(this.PatientId, this.OrderId.Name, Tip, Hypotest) == -1)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                CacheManager.OutOrderMgr.Err = "";
                return;
            }

            //SheetView sv=  this.fpSpread.ActiveSheet.GetChildView(this.fpSpread.ActiveSheet.ActiveRowIndex, 0);
            if (Hypotest == 3)
            {
                cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Hypotest].Text = "����";
            }
            else if (Hypotest == 4)
            {
                cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Hypotest].Text = "����";
            }

            cellClickEvent.View.Sheets[curRow].Cells[cellClickEvent.Row, (int)EnumOrderColumns.Tip].Text = Tip;
            FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfoByPatientNO(this.PatientId);
            RefreshView();
        }

        #endregion
    }

    /// <summary>
    /// ������
    /// </summary>
    public enum EnumOrderColumns
    {
        /// <summary>
        /// ����סԺ��ˮ��
        /// </summary>
        PatientID = 0,

        /// <summary>
        /// ҽ����ˮ��
        /// </summary>
        MoOrderID = 1,

        /// <summary>
        /// ��Ϻ�
        /// </summary>
        CombNo = 2,

        /// <summary>
        /// ���
        /// </summary>
        CheckFlag = 3,

        /// <summary>
        /// ϵͳ���
        /// </summary>
        SysClassName = 4,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        ItemName = 5,

        /// <summary>
        /// ����
        /// </summary>
        ConbFlag = 6,

        /// <summary>
        /// ���
        /// </summary>
        Specs = 7,

        /// <summary>
        /// ÿ����
        /// </summary>
        DoseOne = 8,

        /// <summary>
        /// Ƶ��
        /// </summary>
        FrequencyID = 9,

        /// <summary>
        /// �÷�����
        /// </summary>
        UsageName = 10,

        /// <summary>
        /// ����
        /// </summary>
        Qty = 11,

        /// <summary>
        /// ������������
        /// </summary>
        FuOrFirstDays = 12,

        /// <summary>
        /// ҽ������
        /// </summary>
        OrderType = 13,

        /// <summary>
        /// �Ӽ����
        /// </summary>
        EmergencyFlag = 14,

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        BeginDate = 15,

        /// <summary>
        /// ֹͣʱ��
        /// </summary>
        EndDate = 16,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        RecipeDoctName = 17,

        /// <summary>
        /// ִ�п���
        /// </summary>
        ExecDeptName = 18,

        /// <summary>
        /// ����ʱ��
        /// </summary>
        MoDate = 19,

        /// <summary>
        /// ֹͣҽ��
        /// </summary>
        DcDoctName = 20,

        /// <summary>
        /// ��ע
        /// </summary>
        Memo = 21,

        /// <summary>
        /// ˳���
        /// </summary>
        SortID = 22,

        /// <summary>
        /// ��ע
        /// </summary>
        Tip = 23,

        /// <summary>
        /// ҽ��״̬
        /// </summary>
        MoState = 24,

        /// <summary>
        /// Ƥ��
        /// </summary>
        Hypotest = 25
    }
}
