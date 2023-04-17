using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.SOC.HISFC.Components.InPateintOrder.Controls
{
    /// <summary>
    /// [��������: ҽ���ֽ�ؼ���˵]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOrderExecConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExecConfirm()
        {
            InitializeComponent();
        }

        #region ����

        protected FS.HISFC.BizLogic.Order.Order orderManagement = new FS.HISFC.BizLogic.Order.Order();
        protected FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();
        protected FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();

        protected DateTime dt;

        /// <summary>
        /// ҩƷȫѡ���
        /// </summary>
        bool tab0AllSelect = false;

        /// <summary>
        /// ��ҩƷȫѡ���
        /// </summary>
        bool tab1AllSelect = false;

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ����
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �����б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        bool bOnQuery = false;

        /// <summary>
        /// �÷���Ӧ�ķֽ�ʱ��
        /// </summary>
        private Hashtable hsUsageAndTime = new Hashtable();

        /// <summary>
        /// Сʱ�Ʒ�ҽ����Ƶ�δ���
        /// </summary>
        string frequencyID = "";

        #endregion

        #region ����

        //protected ArrayList al = new ArrayList();

        ///// <summary>
        ///// ��Ա�б�
        ///// </summary>
        //private ArrayList alPatients
        //{
        //    get
        //    {
        //        if (al == null) al = new ArrayList();
        //        return al;
        //    }
        //    set
        //    {
        //        this.al = value;
        //    }
        //}

        /// <summary>
        /// ��Ա�б�
        /// </summary>
        private ArrayList alPatients = new ArrayList();

        /// <summary>
        /// Ĭ�Ϸֽ�ִ������
        /// </summary>
        protected int intDyas = 1;

        /// <summary>
        /// Ĭ�Ϸֽ�ִ������
        /// </summary>
        [Category("�ֽ�����"), Description("Ĭ�Ϸֽ�ִ������")]
        public int Days
        {
            set
            {
                this.intDyas = value;
                //this.txtDays.Value = (decimal)value;
                this.txtDays.Maximum = this.intDyas;
            }
            get
            {
                return this.intDyas;
            }
        }

        /// <summary>
        /// ��ʾ����Ϣ
        /// </summary>
        [Category("�ֽ�����"), Description("��ʾ���û�����ʾ��Ϣ��")]
        public string Tip
        {
            get
            {
                return this.neuLabel3.Text;
            }
            set
            {
                this.neuLabel3.Text = value;
            }
        }

        /// <summary>
        /// Ƿ���ж�ģʽ
        /// </summary>
        protected EnumLackFee lackfee = EnumLackFee.���ж�Ƿ��;

        /// <summary>
        /// Ƿ�Ѳ���
        /// </summary>
        [Category("�ֽ�����"), Description("Ƿ���ж�ģʽ")]
        public EnumLackFee Ƿ�Ѳ���
        {
            get
            {
                return this.lackfee;
            }
            set
            {
                this.lackfee = value;
            }
        }

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        private bool lackFeeDealModel = true;

        /// <summary>
        /// Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒ�
        /// </summary>
        [Category("�ֽ�����"), Description("Ƿ�ѻ�����˱���ʱ�Ƿ�ͬʱ�Ʒѣ�")]
        public bool LackFeeDealModel
        {
            get
            {
                return lackFeeDealModel;
            }
            set
            {
                lackFeeDealModel = value;
            }
        }

        /// <summary>
        /// ��ǰ������Ϊ�����治�ɹ�ʱ���Ƿ�������������������ߣ�
        /// </summary>
        protected bool isSaveErrContinue = true;

        /// <summary>
        /// ���������Ƿ���������������߱���
        /// </summary>
        [Category("�ֽ�����"), Description("��ǰ������Ϊ�����治�ɹ�ʱ���Ƿ�������������������ߣ�")]
        public bool IsSaveErrContinue
        {
            get
            {
                return this.isSaveErrContinue;
            }
            set
            {
                this.isSaveErrContinue = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��FpSpread
        /// </summary>
        private void InitControl()
        {
            this.fpOrderExecBrowser1.IsShowRowHeader = false;
            this.fpOrderExecBrowser2.IsShowRowHeader = false;
            this.TabControl1.SelectedIndex = 1;
            this.TabControl1.SelectedIndex = 0;

            this.fpOrderExecBrowser1.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);
            this.fpOrderExecBrowser2.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);

            this.fpOrderExecBrowser1.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);
            this.fpOrderExecBrowser2.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);

            #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
            this.deptHelper.ArrayObject = this.deptManager.GetDeptmentAllValid();
            #endregion

            //ȡСʱ�շ� {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
            frequencyID = controlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.MetConstant.Hours_Frequency_ID, true);

            FS.HISFC.BizLogic.Manager.Constant myCnst = new FS.HISFC.BizLogic.Manager.Constant();

            #region ������

            ArrayList alItems = myCnst.GetAllList("USAGEDIVTIME");

            ArrayList alItemsUsage = myCnst.GetAllList("USAGE");

            FS.HISFC.Models.Base.Const None = new FS.HISFC.Models.Base.Const();
            None.ID = "NONE";
            None.Name = "��ҩƷͨ��";
            None.UserCode = "NONE";
            alItemsUsage.Add(None);

            foreach (FS.HISFC.Models.Base.Const cnst in alItemsUsage)
            {
                if (!hsUsageAndTime.Contains(cnst.ID))
                {
                    cnst.Memo = "";

                    if (cnst.UserCode.Length > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const usage in alItems)
                        {
                            if (usage.ID == cnst.UserCode)
                            {
                                cnst.Memo = usage.Memo;
                            }
                        }
                    }

                    hsUsageAndTime.Add(cnst.ID, cnst);
                }
            }
            #endregion
        }

        /// <summary>
        /// ��ѯִ�е�--�ֽ�ҽ��
        /// </summary>
        /// <returns></returns>
        public int RefreshExec()
        {
            string DeptCode = ""; //����

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.orderManagement.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            ArrayList alOrders = null;
            FS.HISFC.BizProcess.Integrate.RADT pManager = new FS.HISFC.BizProcess.Integrate.RADT();
            try
            {
                #region �ֽ���˹���ҽ��
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڷֽ⣬���Ժ�...");
                Application.DoEvents();

                this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                pManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                //��ȡȫ����Ƶ��,��ÿ��ҽ��ʹ�ã�����ÿ���������ݿ�
                ArrayList alFrequency = new ArrayList();
                alFrequency = this.frequencyManagement.GetAll(DeptCode);
                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(alFrequency);

                //�ֽ�Ĳ���ʱ��,����ֽ�ʱÿ��ҽ������ѯϵͳʱ��
                DateTime dt = new DateTime();
                dt = this.orderManagement.GetDateTimeFromSysDateTime();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                string errInfo = "";

                //��ÿ�����ߵ�ҽ���ֽ�
                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    //������Ϣ
                    patientInfo = alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (patientInfo == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("��ȡ���߻�����Ϣ����������ϢΪ�գ�");
                        return -1;
                    }
                    else if (Classes.Function.CheckPatientState(patientInfo.ID, ref patientInfo, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(errInfo);
                        return -1;
                    }

                    //��ҽ�������ڼ���ҽ��״̬Ϊ1 �� 2��ҽ��
                    alOrders = orderManagement.QueryValidOrderWithSubtbl(patientInfo.ID, FS.HISFC.Models.Order.EnumType.LONG);
                    if (alOrders == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(orderManagement.Err);
                        return -1;
                    }

                    FS.HISFC.Models.Order.Inpatient.Order order = null;

                    #region �ֽ��������

                    orderManagement.AlAllOrders = alOrders;
                    orderManagement.HsUsageAndTime = this.hsUsageAndTime;

                    #endregion

                    //�ֽ⻼�ߵ�ҽ��
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        order = (FS.HISFC.Models.Order.Inpatient.Order)alOrders[j];

                        #region ���Ŀ���

                        DeptCode = order.ReciptDept.ID;//��������
                        //ҽ��ʵ���еĻ�����Ժ�������¸�ֵ
                        order.Patient.PVisit.PatientLocation.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                        order.Patient.PVisit.PatientLocation.NurseCell.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;

                        #endregion

                        #region ��ȡƵ��ʱ���

                        //���Ϊ����Ƶ�ξ͸�Ƶ�θ�ֵ������Ϊԭ����Ƶ��
                        FS.HISFC.Models.Order.Frequency f = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID)
                            as FS.HISFC.Models.Order.Frequency;
                        if (order.Frequency.Times.Length == f.Times.Length && order.Frequency.Time != f.Time)
                        {
                            //����Ƶ��
                        }
                        else
                        {
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID).Clone();

                            if (order.Frequency == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }

                        #endregion

                        //{97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
                        //Сʱ�շ�ҽ�����ֽ�ʱ�䵽��ǰʱ��
                        if (order.Frequency.ID == frequencyID)
                        {
                            if (orderManagement.DecomposeOrderToNow(order, 0, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }
                        else
                        {
                            //��ҽ�����зֽ�
                            if (orderManagement.DecomposeOrder(order, this.intDyas, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                #endregion
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //MessageBox.Show("�ֽ����" + ex.Message + this.orderManagement.iNum.ToString());
                MessageBox.Show("�ֽ����" + ex.Message);
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.RefreshQuery();
            return 0;
        }

        /// <summary>
        /// ���²�ѯ��ʾ
        /// </summary>
        /// <returns></returns>
        protected int RefreshQuery()
        {
            if (bOnQuery) 
                return 0;
            bOnQuery = true;
            try
            {
                this.txtName.Items.Clear();
                ArrayList alOrders = null;

                #region ��ѯ��ʾ
                this.fpOrderExecBrowser1.Clear();
                this.fpOrderExecBrowser2.Clear();

                FS.FrameWork.Public.ObjectHelper orderNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Public.ObjectHelper deptNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Models.NeuObject objTmp = new FS.FrameWork.Models.NeuObject();

                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo p = this.alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    //if (feeManagement.IsPatientLackFee(p) == true) //Ƿ�ѻ���
                    //{
                    //    switch (this.lackfee)
                    //    {
                    //        case EnumLackFee.���ж�Ƿ��:
                    //            break;
                    //        case EnumLackFee.Ƿ�Ѳ�����ֽ�:
                    //            MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                    //                string.Format("����{0}�Ѿ�Ƿ��,ʣ����{1}.���ܽ��зֽ������", p.Name, p.FT.LeftCost.ToString())));
                    //            continue;
                    //            break;
                    //        case EnumLackFee.Ƿ����ʾ�ʲ�����ֽ�:
                    //            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                    //                string.Format("����{0}�Ѿ�Ƿ��,ʣ����{1}.�Ƿ�����ֽ������", p.Name, p.FT.LeftCost.ToString())), "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                    //            {
                    //                continue;
                    //            }
                    //            break;

                    //    }
                    //}
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ" + p.Name + "�����Ժ�...");

                    #region ��ѯ�ֽ����

                    alOrders = orderManagement.QueryExecOrderIsExec(p.ID, "1", false);//��ѯȷ�ϵ�ҩƷ 

                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;

                        #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}

                        string drugDept = this.deptHelper.GetName(order.Order.StockDept.ID);

                        #endregion

                        if (order.Order.OrderType.IsDecompose)
                        {
                            this.fpOrderExecBrowser1.AddRow(order);
                            objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                            if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                            {
                                orderNameHlpr.ArrayObject.Add(objTmp);
                            }
                            #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
                            if (!string.IsNullOrEmpty(drugDept))
                            {
                                objTmp = new FS.FrameWork.Models.NeuObject(drugDept, drugDept, "");
                                if (deptNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    deptNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }
                            #endregion
                        }
                    }
                    //��ҩƷ
                    alOrders = orderManagement.QueryExecOrderIsExec(p.ID, "2", false);//��ѯδִ�еķ�ҩƷ
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;
                        //��ʾ��Ҫ��ʿվȷ�ϵķ�ҩƷ
                        if ((((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm == false ||
                            order.Order.ExeDept.ID == order.Order.ReciptDept.ID ||
                            order.Order.ExeDept.ID == NurseStation.ID)) //��ʿվ�շѻ���ִ�п��ң�������  
                        {
                            if (order.Order.OrderType.IsDecompose) //����ҽ��
                            {
                                this.fpOrderExecBrowser2.AddRow(order);
                                objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                                if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    orderNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }
                        }
                    }

                    #endregion

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                }
                #endregion

                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "ȫ��", "");
                orderNameHlpr.ArrayObject.Insert(0, objTmp);
                this.txtName.AddItems(orderNameHlpr.ArrayObject);
                this.txtName.Tag = "ALL";
                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "ȫ��", "");
                deptNameHlpr.ArrayObject.Insert(0, objTmp);
                this.txtDrugDeptName.AddItems(deptNameHlpr.ArrayObject);
                this.txtDrugDeptName.Tag = "ALL";

                this.fpOrderExecBrowser1.RefreshComboNo();
                this.fpOrderExecBrowser2.RefreshComboNo();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                //��ʿ���¼���ʱ���ȼ���2�� Ȼ���ڼ���1���ʱ�� ɾ����������ݣ�������ɾ����
                this.fpOrderExecBrowser1.DeleteRow(this.intDyas);
                this.fpOrderExecBrowser2.DeleteRow(this.intDyas);
                this.tabPage1.Text = "ҩƷ" + "��" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "����";
                this.tabPage2.Text = "��ҩƷ" + "��" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "����";
            }
            catch
            {
            }
            bOnQuery = false;

            return 0;
        }

        /// <summary>
        /// ��ʿվ����
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                return ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Clone();
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <returns></returns>
        public int ComfirmExec()
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("�Ƿ�ȷ��Ҫ���棿", 422) == DialogResult.No)
            {
                return -1;
            }
            this.btnSave.Enabled = false;
            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
            Application.DoEvents();
            dt = this.orderManagement.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Order.ExecOrder order = null;
            string inpatientNo = "";
            List<FS.HISFC.Models.Order.ExecOrder> alOrders = null;
            FS.HISFC.Models.RADT.PatientInfo patient = null;

            //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB} Ƿ���ж�
            FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.M;
            switch (Ƿ�Ѳ���)
            {
                case EnumLackFee.���ж�Ƿ��:
                    messType = FS.HISFC.Models.Base.MessType.N;
                    break;
                case EnumLackFee.Ƿ�Ѳ�����ֽ�:
                    messType = FS.HISFC.Models.Base.MessType.Y;
                    break;
                case EnumLackFee.Ƿ����ʾ�ʲ�����ֽ�:
                    messType = FS.HISFC.Models.Base.MessType.M;
                    break;
            }

            orderIntegrate.MessageType = messType;
            //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB}

            string lackFeeInfo = "";
            string errInfo = "";

            ///�Ƿ�Ƿ��
            bool isLackFee = false;

            #region ҩƷ

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;

                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //��һ������
                        {
                            if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return -1;
                            }

                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;

                                //FS.FrameWork.Management.PublicTrans.RollBack();;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                #region {C88D3BEB-EA3F-455f-BD5D-0A997699CC2C}
                                //�����ʾ��ĳ�����߳����Ժ󣬿�ѡ���Ƿ�����������˷ֽ�
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.�Ƿ����ִ�зֽ��������ߵĲ�����", orderIntegrate.Err)), "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = true;
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                                #endregion
                            }
                            //}{B3173852-136F-4c4b-9FAC-E15EB879C619}����������Ū��}��֪��Ϊʲô����ôдҪ������
                            else
                            {
                                //orderIntegrate.fee.Commit();
                                FS.FrameWork.Management.PublicTrans.Commit();
                                //FS.FrameWork.Management.Transaction 
                                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                                //t.BeginTransaction();
                                //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            }
                        }//{B3173852-136F-4c4b-9FAC-E15EB879C619}}�����}Ӧ�������ﰡ
                        inpatientNo = order.Order.Patient.ID;
                        //patient = radtIntegrate.GetPatientInfomation(inpatientNo);
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;
                        //Ƿ����ʾ
                        if (feeManagement.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name;
                            }
                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
                    }
                    alOrders.Add(order);

                }
            }
            if (patient != null) //��һ������
            {
                if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    // orderIntegrate.fee.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //FS.FrameWork.Management.Transaction 
                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();
                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }
            }

            #endregion

            #region ��ҩƷ

            alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
            patient = null;
            inpatientNo = "";
            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //��һ������
                        {
                            if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return -1;
                            }


                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                //FS.FrameWork.Management.PublicTrans.RollBack();;
                                #region {C88D3BEB-EA3F-455f-BD5D-0A997699CC2C}
                                //�����ʾ��ĳ�����߳����Ժ󣬿�ѡ���Ƿ�����������˷ֽ�
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.�Ƿ����ִ�зֽ��������ߵĲ�����", orderIntegrate.Err)), "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = false; ;
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ������ݣ����Ժ�...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                                #endregion
                            }
                            else
                            {
                                //orderIntegrate.fee.Commit();
                                FS.FrameWork.Management.PublicTrans.Commit();
                                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                                ////t.BeginTransaction();
                                //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            }
                        }
                        inpatientNo = order.Order.Patient.ID;
                        //patient = radtIntegrate.GetPatientInfomation(inpatientNo);
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;

                        //Ƿ����ʾ
                        if (feeManagement.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "��  " + patient.Name;
                            }

                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();

                    }
                    alOrders.Add(order);

                }
            }
            if (patient != null) //��һ������
            {
                if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    //orderIntegrate.fee.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //FS.FrameWork.Management.Transaction 
                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();
                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }
            }

            #endregion

            //orderIntegrate.fee.Commit();
            //FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.btnSave.Enabled = true;
            bOnQuery = false;
            this.RefreshQuery();

            string ExecTip = "����δ�ֽⱣ����Ŀ��\r\n\r\n";
            bool show = false;
            if (fpOrderExecBrowser1.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage1.Text;
                show = true;
            }
            if (fpOrderExecBrowser2.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage2.Text;
                show = true;
            }
            if (show)
            {
                Classes.Function.ShowBalloonTip(3, "ע��", ExecTip + "\r\n", ToolTipIcon.Warning);
            }

            if (!string.IsNullOrEmpty(lackFeeInfo))
            {
                if (messType == FS.HISFC.Models.Base.MessType.N && !lackFeeDealModel)
                {
                    MessageBox.Show("���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, "Ƿ����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {                    
                    Classes.Function.ShowBalloonTip(10, "Ƿ����ʾ", "���»��ߴ���Ƿ�������\r\n" + lackFeeInfo, System.Windows.Forms.ToolTipIcon.Info);
                }
            }

            return 0;
        }

        /// <summary>
        /// ѡ�������Ŀ
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        private void CheckFilteredData()
        {
            for (int i = 0; i < this.CurrentBrowser.fpSpread.Sheets[0].Rows.Count; i++)
            {
                if (this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].BackColor == Color.LightSkyBlue)
                {
                    FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                    order = this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                    {
                        //����Ƚ��������ͬ����Էֿ����˴���Ҫ
                        for (int j = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; j >= 0; j--)
                        {
                            FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[j].Tag;
                            if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                            {
                                this.CurrentBrowser.fpSpread.Sheets[0].Cells[j, 1].Value = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ������Ϲ��˺���
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        /// <param name="isMatchAll"></param>
        /// <param name="prmsDate">ִ�п�ʼ������ʱ��</param>
        /// <param name="prmsStr">���ơ�ҩ��</param>
        protected void SetFilteredFlag(bool isMatchAll, DateTime[] prmsDate, string[] prmsStr)
        {
            bool isHaveFilter = true;
            bool isAllOrderNames = (prmsStr[0] == "ȫ��");
            bool isAllDeptNames = (prmsStr[1] == "ȫ��");
            bool isAllTime = (prmsDate[0].ToString() == prmsDate[1].ToString());
            if (isAllOrderNames && isAllDeptNames && isAllTime)
            {
                isHaveFilter = false;
            }

            FS.HISFC.Models.Order.ExecOrder order = null;
            //��ʼ����ʾ��ҩƷ
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text == prmsStr[1])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text.Contains(prmsStr[1]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
            }
            //��ҩƷ
            else
            {
                //�ָ�ԭ������ɫ
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region �¼�
        //���ֽ�ҽ��
        private void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            order = this.CurrentBrowser.CurrentExecOrder;

            if (order == null) return;
            if (MessageBox.Show("ȷ�ϲ��ֽ�" + order.DateUse.ToString() + "��ҽ��[" + order.Order.Item.Name + "] ?", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            //����ִ�е�ҽ��Ϊ�����
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.orderManagement.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = this.orderManagement.Operator;
            ArrayList alDel = new ArrayList();
            if (order.Order.Combo.ID == "" || order.Order.Combo.ID == "0")
            {
                if (this.orderManagement.DcExecImmediate((FS.HISFC.Models.Order.Order)order.Order, obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(this.orderManagement.Err);
                    return;
                }
                this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(this.fpOrderExecBrowser1.fpSpread.Sheets[0].ActiveRowIndex, 1);
            }
            else //���ҽ����������Ϻ���ͬ��ʹ��ʱ����ͬ��ҽ��
            {
                for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                    if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                    {
                        if (this.orderManagement.DcExecImmediate(objorder, obj) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(this.orderManagement.Err);
                            return;
                        }
                        //alDel.Add(i);
                        this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(i, 1);
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            this.tabPage1.Text = "��" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "����";
            this.tabPage2.Text = "��" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "����";

        }
        /// <summary>
        /// ���ص�ǰ��FpSpreadҳ
        /// </summary>
        protected fpOrderExecBrowser CurrentBrowser
        {
            get
            {
                if (this.TabControl1.SelectedIndex == 0)
                {
                    return this.fpOrderExecBrowser1;
                }
                else
                {
                    return this.fpOrderExecBrowser2;
                }
            }
        }


        /// <summary>
        /// ���¼��㰴ť�¼� �Ѿ�����ʹ�� Ĭ��Ϊ1��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaculate_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("ȷ��Ҫ���¼��㲢�ֽ�ҽ����?\n������Ҫһ��ʱ�䣡", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.intDyas = (int)(this.txtDays.Value);
                RefreshExec();
            }
        }

        /// <summary>
        /// fpSpread_ButtonClicked�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.CurrentBrowser.fpSpread.Sheets[0].RowCount <= 0)
            {
                return;
            }
            if (e.Column == 1)
            {
                string checkValue = this.CurrentBrowser.fpSpread.Sheets[0].Cells[e.Row, e.Column].Text;

                FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                order = this.CurrentBrowser.CurrentExecOrder;
                if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                {
                    for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                    {
                        FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                        if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                        {
                            this.CurrentBrowser.fpSpread.Sheets[0].Cells[i, 1].Text = checkValue;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// �����ѯ������,��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //�س�����
            if (e.KeyCode != Keys.Enter)
                return;
            string name = this.txtName.Text.Trim();
            if (name == "") return;

            this.SetDrugFlag(name, false);
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked)
            { //ȫѡ
                b = true;
            }
            else
            {//ȡ��
                b = false;
            }

            //������©��Ĭ��ȫ��ѡ��

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                tab0AllSelect = b;
            }

            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                tab1AllSelect = b;
            }
            /*
            if (this.TabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                    tab0AllSelect = b;
                }
            }
            else
            {
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                    tab1AllSelect = b;
                }
            }
             * */
        }
        /// <summary>
        /// ���¼���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnCalculate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(string.Format("�Ƿ�ֽ�{0}���ҽ����Ϣ��", this.txtDays.Value.ToString())),
                "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //{5617A654-9738-4db4-A006-5A80B44F0841} ���¼���ʱ��Ҫ�Ի����б�ֵ
                this.alPatients = this.GetSelectedTreeNodes();

                intDyas = (int)this.txtDays.Value;
                this.RefreshExec();
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TabControl1.SelectedIndex == 0)
            {
                this.chkAll.Checked = tab0AllSelect;
            }
            else
            {
                this.chkAll.Checked = tab1AllSelect;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ComfirmExec();
            this.btnSave.Enabled = true;

        }

        private void txtDays_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }


        private void txtDrugDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            CheckFilteredData();
        }

        private void dtpBeginTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }
        #endregion

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            TreeView tv = sender as TreeView;
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return null;
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            //{5617A654-9738-4db4-A006-5A80B44F0841} ��ѯʱҲ��Ҫ��������ֵ
            intDyas = (int)this.txtDays.Value;

            this.alPatients = alValues;
            this.RefreshExec();
            return 0;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return ComfirmExec();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(this.TabControl1);
            return 0;
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ShowPageSetup();
            p.PrintPreview(this.TabControl1);
            return 0;
        }
        #endregion

        /// <summary>
        /// ���ƶ�ҽ����������ɫ��ʾ
        /// </summary>
        /// <param name="filterStr">����ƥ�������</param>
        /// <param name="isMatchingAll">�Ƿ���ȫ��ƥ��</param>
        protected void SetDrugFlag(string filterStr, bool isMatchingAll)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            //��ʼ����ʾ��ҩƷ
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
            //��ҩƷ
            else
            {
                //�ָ�ԭ������ɫ
                bool b = false;
                //�ָ�ԭ������ɫ
                //�����ɫ��ʾ
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumLackFee
    {
        ���ж�Ƿ��,
        Ƿ�Ѳ�����ֽ�,
        Ƿ����ʾ�ʲ�����ֽ�
    }
}
