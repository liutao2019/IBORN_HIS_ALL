using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// [��������: ��ʷҽ����ѯ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='sunm'
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��='������ʷҽ����ѯʹ��'
    ///		�޸�����='��סԺ���еı��ؼ����е���棬�޸Ļ��ߺ�ҽ��Ϊ����'
    ///  />
    /// </summary>
    public partial class ucOrderHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderHistory()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
                //{DF8058FF-72C0-404f-8F36-6B4057B6F6CD}
                this.fpOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpSpread1_MouseUp);
            }
        }

        #region ����
        public delegate void CopyAllClickDelegate(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        ///{5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// ҽ��������
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();


        /// ҽ����չ������Ϣ����
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend orderExtMgr2 = new FS.HISFC.BizLogic.Order.OutPatient.OrderLisExtend();

        /// <summary>
        /// ���ݿ������
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// ����"����ҽ��ʱ"��ťʱ
        /// </summary>
        public event CopyAllClickDelegate CopyAllClicked;
        /// <summary>
        /// �Ҽ��˵�{DF8058FF-72C0-404f-8F36-6B4057B6F6CD}
        /// </summary>
        protected FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();

        private DateTime dtBegin = new DateTime();
        private DateTime dtEndTime = new DateTime();

        protected FS.HISFC.Models.Registration.Register myReg = new FS.HISFC.Models.Registration.Register();
        /// <summary>
        /// ����ҽ��
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDoct = null;

        /// <summary>
        /// ��������
        /// </summary>
        protected FS.FrameWork.Models.NeuObject reciptDept = null;

        private ArrayList allPrintData = new ArrayList();
        /// <summary>
        /// �Ƿ���ʾ����ҽ���ؼ�
        /// </summary>
        private bool isShowCopyAllClick = false;
        /// <summary>
        /// �Ƿ����ȷ���˷�����ҽ��������ҽ��// {F53BD032-1D92-4447-8E20-6C38033AA607}
        /// </summary>
        private bool isCanOrderReturnForOther = true;
        /// <summary>
        /// ���ﴦ����ӡ
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint IOutPatientPrint = null;

        /// <summary>
        /// �Ƿ���ʾ����ҽ���ؼ�
        /// </summary>
        [Category("��������"), Description("�Ƿ���ʾ����ҽ���ؼ�"), DefaultValue(true)]
        public bool IsShowCopyAllClick
        {
            get
            {
                return isShowCopyAllClick;
            }
            set
            {
                isShowCopyAllClick = value;
            }
        }

        /// <summary>
        /// �Ƿ����ȷ���˷�����ҽ��������ҽ��// {F53BD032-1D92-4447-8E20-6C38033AA607}
        /// </summary>
        [Category("��������"), Description("�Ƿ����ȷ���˷�����ҽ��������ҽ��"), DefaultValue(true)]
        public bool IsCanOrderReturnForOther
        {
            get
            {
                return isCanOrderReturnForOther;
            }
            set
            {
                isCanOrderReturnForOther = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��ִ��ҽ�������˷�
        /// </summary>
        private bool isCanOrederReturnDone = true;
        #endregion

        #region ����

        /// <summary>
        /// Ĭ�ϲ�ѯ����
        /// </summary>
        private int defaultQueryDays = 90;

        /// <summary>
        /// ��ʷҽ��Ĭ�ϲ�ѯ�������
        /// </summary>
        [Description("��ʷҽ��Ĭ�ϲ�ѯ�������"), Category("��ѯ����")]
        public int DefaultQueryDays
        {
            get
            {
                return defaultQueryDays;
            }
            set
            {
                defaultQueryDays = value;
            }
        }
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public virtual FS.HISFC.Models.Registration.Register Patient
        {
            set
            {
                if (value == null)
                    return;
                //DateTime dtNow = CacheManager.OrderMgr.GetDateTimeFromSysDateTime();
                //DateTime dtBegin = Convert.ToDateTime("1900-01-01");
                myReg = value;
                Clear();
                this.txtCardNo.Text = value.PID.CardNO;
                ArrayList al = CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                string strDig = "";
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                {
                    strDig = strDig + diag.DiagInfo.ICD10.Name+";";
                }
                if (myReg != null && !string.IsNullOrEmpty(myReg.ID))
                {
                    this.lblPatientInfo.Text = "������" + myReg.Name + "   �Ա�" + myReg.Sex.Name + "   ���䣺" + CacheManager.OutOrderMgr.GetAge(myReg.Birthday) + "   ��ϣ�" + strDig;
                }
            }
        }
        #endregion

        #region ����

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Patient = neuObject as FS.HISFC.Models.Registration.Register;
            //this.dtpBeginTime.Value = CacheManager.OrderMgr.GetDateTimeFromSysDateTime().AddMonths(-3);
            this.dtpEndTime.Value = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            // �Ƿ���ʾ��ִ��ҽ�������˷�
            isCanOrederReturnDone = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ95", false, "0"));

            this.Query(myReg.PID.CardNO);
            return 0;
        }

        private void Clear()
        {
            this.txtCardNo.Text = "";
            this.lblPatientInfo.Text = "";
            this.fpOrder_Sheet1.RowCount = 0;
            this.fpFee_Sheet1.RowCount = 0;
        }

        public void Query(string patientID)// //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} �޸��ۺϲ�ѯ
        {
            if (this.dtpEndTime.Value < this.dtpBeginTime.Value)
            {
                MessageBox.Show("��ֹʱ��С�ڿ�ʼʱ�䣡");
                return;
            }

            try
            {
                //ArrayList al = manager.QueryValidPatientsByCardNO(patientID, this.dtpBeginTime.Value.Date);

                this.treeView1.Nodes[0].Nodes.Clear();
                ArrayList al = CacheManager.RegInterMgr.QueryRegList(patientID, this.dtpBeginTime.Value.Date, this.dtpEndTime.Value.Date.AddDays(1), "1");
                if (al == null)
                {
                    return;
                }
                if (al.Count == 0)
                {
                    FS.HISFC.Models.RADT.Patient patient = CacheManager.RadtIntegrate.GetPatient(patientID);
                    if (patient != null)
                    {
                        this.lblPatientInfo.Text = "������" + patient.Name + "   �Ա�" + patient.Sex.Name + "   ���䣺" + CacheManager.OutOrderMgr.GetAge(patient.Birthday);
                    }
                    return;
                }

                al.Sort(new RegInfoCompare());

                FS.HISFC.Models.Registration.Register reg = null;
                int i = 0;
                TreeNode defaultnode = null;
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    string no = obj.ID;

                    reg = obj as FS.HISFC.Models.Registration.Register;

                    if (reg != null)
                    {
                        this.Patient = reg;
                    }

                    TreeNode node = new TreeNode("��" + reg.DoctorInfo.SeeDate.Date.ToString("yyyy-MM-dd") + "��" + obj.Name);
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    node.Tag = obj;
                    //if (i == 0) defaultnode = node;
                    this.treeView1.Nodes[0].Nodes.Add(node);
                  
                    ArrayList alSeeNO = CacheManager.OutOrderMgr.QuerySeeNoListByCardNo(reg.ID, reg.PID.CardNO);
                    foreach (FS.FrameWork.Models.NeuObject seeNO in alSeeNO)
                    {
                        TreeNode nodeChild = new TreeNode(seeNO.User02 + "��" + seeNO.ID + "��");
                        nodeChild.ImageIndex = 3;
                        nodeChild.SelectedImageIndex = 3;
                        nodeChild.Tag = seeNO;
                        node.Nodes.Add(nodeChild);
                    }
                    if (i == 0)
                    {
                        defaultnode = node; //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} �޸��ۺϲ�ѯ
                        //this.treeView1.SelectedNode = node;
                    }
                    i++;
                }
                if (defaultnode != null) //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} �޸��ۺϲ�ѯ
                    this.treeView1.SelectedNode = defaultnode;
                this.treeView1.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region �¼�

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag == null) return;
                FS.FrameWork.Models.NeuObject obj = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                ArrayList al = new ArrayList();
                if (e.Node.Tag.GetType() == typeof(FS.HISFC.Models.Registration.Register))
                {
                    FS.HISFC.Models.Registration.Register regObj = e.Node.Tag as FS.HISFC.Models.Registration.Register;
                    this.Patient = e.Node.Tag as FS.HISFC.Models.Registration.Register;
                    if (regObj != null)
                    {
                        #region ��ѯ���ӷ�
                        ArrayList alSeeNo = CacheManager.OutOrderMgr.QuerySeeNoListByCardNo(regObj.ID, regObj.PID.CardNO);
                        if (alSeeNo != null && alSeeNo.Count > 0)
                        {
                            ArrayList alTemp = new ArrayList();
                            foreach (FS.FrameWork.Models.NeuObject seeObj in alSeeNo)
                            {
                                //{63E3D61E-ECEE-4fcf-BF83-6597EAD9D81A}��ʷҽ����ѯִ��״��
                                alTemp = CacheManager.OutOrderMgr.QueryOrderHistoryExec(myReg.ID, seeObj.ID);
                                if (alTemp != null)
                                {
                                    al.AddRange(alTemp);
                                }
                            }
                        }

                        al.Sort(new OutOrderCompare());

                        if (al.Count > 0)// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
                        {
                            allPrintData = new ArrayList();
                            allPrintData = al;
                        }
                        FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpOrder_Sheet1, al, FS.HISFC.Models.Base.ServiceTypes.C);
                        #endregion

                        #region ��ѯ�ֹ���
                        this.fpFee_Sheet1.RowCount = 0;

                        this.fpFee_Sheet1.Columns[0].Visible = false;
                        this.fpFee_Sheet1.Columns[10].Visible = false;
                        this.fpFee_Sheet1.Columns[12].Visible = false;
                        this.fpFee_Sheet1.Columns[14].Visible = false;
                        this.fpFee_Sheet1.Columns[17].Visible = false;

                        ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(regObj.ID, "ALL", "0", "0");
                        if (alFee != null && alFee.Count > 0)
                        {
                            this.fpFee_Sheet1.RowCount = alFee.Count;
                            this.fpFee_Sheet1.Columns.Count = 18;
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList f = null;
                            FS.HISFC.Models.Fee.Item.Undrug undrugObj = null;
                            FS.HISFC.Models.Pharmacy.Item phaItemObj = null;

                            for (int i = 0; i < alFee.Count; i++)
                            {
                                f = alFee[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    //phaItemObj = phaIntegrate.GetItem(f.Item.ID);
                                    phaItemObj = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(f.Item.ID);
                                    if (phaItemObj == null)
                                    {
                                        MessageBox.Show(CacheManager.FeeIntegrate.Err);
                                        return;
                                    }
                                    //�Զ�����
                                    this.fpFee_Sheet1.Cells[i, 1].Text = phaItemObj.UserCode;
                                    //��Ŀ����
                                    this.fpFee_Sheet1.Cells[i, 2].Text = f.Item.Name + "[" + phaItemObj.Specs + "]";
                                }
                                else
                                {
                                    //undrugObj = CacheManager.FeeIntegrate.GetItem(f.Item.ID);
                                    undrugObj = SOC.HISFC.BizProcess.Cache.Fee.GetItem(f.Item.ID);
                                    if (undrugObj == null)
                                    {
                                        MessageBox.Show(CacheManager.FeeIntegrate.Err);
                                        return;
                                    }
                                    //�Զ�����
                                    this.fpFee_Sheet1.Cells[i, 1].Text = undrugObj.UserCode;
                                    //��Ŀ����
                                    this.fpFee_Sheet1.Cells[i, 2].Text = f.Item.Name;
                                }
                                //��Ŀ����
                                this.fpFee_Sheet1.Cells[i, 0].Text = f.Item.ID;
                                //����
                                this.fpFee_Sheet1.Cells[i, 3].Text = "";
                                //��λ
                                this.fpFee_Sheet1.Cells[i, 6].Text = f.Item.PriceUnit;

                                if (f.FeePack == "1")//��װ��λ
                                {
                                    this.fpFee_Sheet1.Cells[i, 4].Text = f.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                                    this.fpFee_Sheet1.Cells[i, 5].Text = (f.Item.Qty / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                                }
                                else
                                {
                                    this.fpFee_Sheet1.Cells[i, 4].Text = (f.Item.Price / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                                    this.fpFee_Sheet1.Cells[i, 5].Text = (f.Item.Qty).ToString("F4").TrimEnd('0').TrimEnd('.');
                                }

                                //����/����
                                this.fpFee_Sheet1.Cells[i, 7].Text = f.Order.HerbalQty.ToString();
                                //����
                                this.fpFee_Sheet1.Cells[i, 8].Text = f.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                                //ÿ��������λ
                                this.fpFee_Sheet1.Cells[i, 9].Text = f.Order.DoseUnit;
                                //Ƶ��
                                this.fpFee_Sheet1.Cells[i, 10].Text = f.Order.Frequency.ID;
                                this.fpFee_Sheet1.Cells[i, 11].Text = f.Order.Frequency.Name;
                                //�÷�
                                this.fpFee_Sheet1.Cells[i, 12].Text = f.Order.Usage.ID;
                                this.fpFee_Sheet1.Cells[i, 13].Text = f.Order.Usage.Name;
                                //ִ�п���
                                this.fpFee_Sheet1.Cells[i, 14].Text = f.ExecOper.Dept.ID;
                                this.fpFee_Sheet1.Cells[i, 15].Text = f.ExecOper.Dept.Name;
                                //���
                                this.fpFee_Sheet1.Cells[i, 16].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString("F4").TrimEnd('0').TrimEnd('.');
                                //��Ϻ�
                                this.fpFee_Sheet1.Cells[i, 17].Text = f.Order.Combo.ID;


                                if (f.PayType == FS.HISFC.Models.Base.PayTypes.Charged)
                                {
                                    this.fpFee_Sheet1.RowHeader.Rows[i].BackColor = Color.FromArgb(128, 255, 128);
                                }
                                else if (f.PayType == FS.HISFC.Models.Base.PayTypes.Balanced)
                                {
                                    this.fpFee_Sheet1.RowHeader.Rows[i].BackColor = Color.FromArgb(106, 174, 242);
                                }

                            }

                            Components.Order.Classes.Function.DrawCombo(this.fpFee_Sheet1, 17, 3);
                        }
                        #endregion
                    }

                    HISFC.Components.Order.Classes.HistoryOrderClipboard.ClinicCode = ((FS.FrameWork.Models.NeuObject)e.Node.Tag).ID;
                }
                else
                {
                    this.Patient = e.Node.Parent.Tag as FS.HISFC.Models.Registration.Register;
                    al = CacheManager.OutOrderMgr.QueryOrder(this.myReg.ID, obj.ID);

                    al.Sort(new OutOrderCompare());

                    if (al.Count > 0)// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
                    {
                        allPrintData = new ArrayList();
                        allPrintData = al;
                    }
                    HISFC.Components.Order.Classes.HistoryOrderClipboard.ClinicCode = ((FS.FrameWork.Models.NeuObject)e.Node.Parent.Tag).ID;

                    FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpOrder_Sheet1, al, FS.HISFC.Models.Base.ServiceTypes.C);

                    this.fpFee_Sheet1.RowCount = 0;
                }

                this.fpOrder_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region ����ճ��

        /// <summary>
        /// ����
        /// </summary>
        private void CopyOrder()
        {
            if (this.fpOrder_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            this.fpOrder.Focus();

            ArrayList list = new ArrayList();

            //��ȡѡ���е�ҽ��ID
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            //for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            //{
            //    for (int col = 0; col < this.fpOrder_Sheet1.Columns.Count; col++)
            //    {
            //        if (this.fpOrder_Sheet1.IsSelected(row, col))
            //        {
            //            if (this.fpOrder_Sheet1.Rows[row].Tag != null)
            //            {
            //                try
            //                {
            //                    outOrder = fpOrder_Sheet1.Rows[row].Tag as FS.HISFC.Models.Order.OutPatient.Order;
            //                    if (outOrder.IsSubtbl)
            //                    {
            //                        continue;
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //            }

            //            list.Add(fpOrder_Sheet1.Cells[row, 0].Value.ToString());
            //            break;
            //        }
            //    }
            //}
            for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            {
                if (fpOrder_Sheet1.IsSelected(row, 0))
                {
                    outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                    outOrder = fpOrder_Sheet1.Rows[row].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (outOrder != null && !outOrder.IsSubtbl)
                    {
                        list.Add(outOrder);
                    }
                }
            }

            if (list.Count <= 0)
            {
                return;
            }

            //����ӵ�COPY�б�
            for (int count = 0; count < list.Count; count++)
            {
                Classes.Function.AlCopyOrders = list;
                HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            }
            string type = "1";
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Type = FS.HISFC.Models.Base.ServiceTypes.C;

            //Ȼ��copy�б�ŵ���������
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
        }

        /// <summary>
        /// �Ҽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            int ActiveRowIndex = -1;

            if (e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Items.Clear();
                FarPoint.Win.Spread.Model.CellRange c = fpOrder.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    //this.fpSpread1.ActiveSheet.ActiveRowIndex = c.Row;{97B9173B-834D-49a1-936D-E4D04F98E4BA}
                    this.fpOrder.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                    FS.HISFC.Models.Order.OutPatient.Order tmp = fpOrder_Sheet1.Rows[ActiveRowIndex].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (tmp.Item.ID == "F00000000716" || tmp.Item.ID == "F00000023411")
                    {
                        ToolStripMenuItem mnuLisOrder = new ToolStripMenuItem();
                        mnuLisOrder.Text = "�������ϼ�����Ϣ";
                        mnuLisOrder.Click += new EventHandler(mnuLisOrder_Click);
                        this.contextMenu1.Items.Add(mnuLisOrder);
                    }

                }
                else
                {
                    ActiveRowIndex = -1;
                }
                if (ActiveRowIndex < 0) return;


                //ToolStripMenuItem mnuCopyGroup = new ToolStripMenuItem();
                //mnuCopyGroup.Text = "���Ƴ�����";
                //mnuCopyGroup.Click += new EventHandler(mnuCopyGroup_Click);
                //this.contextMenu1.Items.Add(mnuCopyGroup);

                ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem();
                mnuCopyOrder.Text = "����";
                mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                this.contextMenu1.Items.Add(mnuCopyOrder);

                //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                ToolStripMenuItem mnuQuitOrder = new ToolStripMenuItem();
                mnuQuitOrder.Text = "ҽ���˷�";
                mnuQuitOrder.Click += new EventHandler(mnuQuitOrder_Click);
                this.contextMenu1.Items.Add(mnuQuitOrder);

                //ToolStripMenuItem mnuQuitAllOrder = new ToolStripMenuItem();
                //mnuQuitAllOrder.Text = "�����˷�";
                //mnuQuitAllOrder.Click += new EventHandler(mnuQuitAllOrder_Click);
                //this.contextMenu1.Items.Add(mnuQuitAllOrder);

               


                this.contextMenu1.Show(this.fpOrder, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        ///{5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// ҽ���˷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuQuitOrder_Click(object sender, EventArgs e)
        {
            if (this.fpOrder_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            this.fpOrder.Focus();

            ArrayList list = new ArrayList();
            ArrayList rowsIndex = new ArrayList();

            //��ȡѡ���е�ҽ��ID
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            {
                if (fpOrder_Sheet1.IsSelected(row, 0))
                {
                    outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                    outOrder = fpOrder_Sheet1.Rows[row].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (outOrder != null)
                    {
                        if (outOrder.Status == 0)
                        {
                            string ErrInfo = "��";
                            ErrInfo += outOrder.Item.Name;
                            ErrInfo += "��δ�����շѣ��޷������˷Ѳ�����";
                            MessageBox.Show(ErrInfo);
                            return;
                        }
                        // {F53BD032-1D92-4447-8E20-6C38033AA607}
                        if (outOrder.ReciptDoctor.ID != FS.FrameWork.Management.Connection.Operator.ID && !this.isCanOrderReturnForOther)
                        {
                            string ErrInfo = "����������ҽ������ҽ���ķ��ã�";
                            MessageBox.Show(ErrInfo);
                            return;
                        }

                        //������������Ŀ��Ҫ�ж��Ƿ��ѽ��ջ��ѱ���
                        if (isCanOrederReturnDone)
                        {

                            if ((outOrder.Item.SysClass.ID.ToString() == "UC" && outOrder.ExeDept.ID == "6003")
                                || outOrder.Item.SysClass.ID.ToString() == "UL")
                            {
                                string sampleState = string.Empty;
                                string SQL = @"select sample_state from fin_opb_feedetail where RECIPE_NO = '{0}' and mo_order = '{1}' and TRANS_TYPE = '1' and cancel_flag = '1'";
                                SQL = string.Format(SQL, outOrder.ReciptNO, outOrder.ID);
                                DataSet ds = new DataSet();
                                dbManager.ExecQuery(SQL, ref ds);
                                DataTable dt = new DataTable();
                                dt = ds.Tables[0];
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    sampleState = dt.Rows[0]["sample_state"].ToString();
                                    if (sampleState == "2")
                                    {
                                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip("��Ŀ��" + outOrder.Item.Name + "���ѵǼ�,����ȡ���Ǽ����˷ѣ�", 2);
                                        return;
                                    }
                                    else if (sampleState == "3")
                                    {
                                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip("��Ŀ��" + outOrder.Item.Name + "���ѽ���,�������˷ѣ�", 2);
                                        return;
                                    }
                                    else if (sampleState == "4")
                                    {
                                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip("��Ŀ��" + outOrder.Item.Name + "���ѳ�����,�������˷ѣ�", 2);
                                        return;
                                    }
                                }
                            }
                        }

                        rowsIndex.Add(row);
                        list.Add(outOrder);
                    }
                }
            }

            if (list.Count <= 0)
            {
                return;
            }

            string refundReason = "";
            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (currDept.HospitalName.Contains("����"))
            {
                frmOrderRefundReason Reason = new frmOrderRefundReason();
                Reason.ShowDialog();
                refundReason = Reason.refundReason;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in list)
                {
                    if (orderMgr.UpdateOrderCanChargeByOrderID(order.ID, FS.FrameWork.Management.Connection.Operator.ID, refundReason) <= 0) 
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        string ErrInfo = "���������˷ѱ�ʶʧ�ܣ�";
                        ErrInfo += this.orderMgr.Err;
                        MessageBox.Show(ErrInfo);
                        return;
                    }
                }

                this.SetFontColor(rowsIndex, true);
            }
            catch(Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                string ErrInfo = "���������˷ѱ�ʶʧ�ܣ�";
                ErrInfo += ex.Message;
                MessageBox.Show(ErrInfo);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("�����ɹ�");
        }


        /// <summary>
        ///
        /// ���ϻ�����Ϣ�޸�{97B9173B-834D-49a1-936D-E4D04F98E4BA}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLisOrder_Click(object sender, EventArgs e)
        {
            //��ȡѡ���е�ҽ��ID
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;


            outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
            outOrder = fpOrder_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Order.OutPatient.Order;
            FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet tanshi;
            if (outOrder != null)
            {
                FS.HISFC.Models.Order.OutPatient.OrderLisExtend orderExtObj = orderExtMgr2.QueryByClinicCodOrderID(outOrder.Patient.ID, outOrder.ID);
                if (orderExtObj == null)
                {
                    orderExtObj = new FS.HISFC.Models.Order.OutPatient.OrderLisExtend();
                    tanshi = new FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet(orderExtObj);
                    tanshi.ShowDialog();
                    orderExtObj = tanshi.orderExtObj;

                }
                else
                {
                    tanshi = new FS.HISFC.Components.Order.OutPatient.Forms.frmTanShiInfoSet(orderExtObj);
                    tanshi.InitInfo();
                    tanshi.ShowDialog();
                    orderExtObj = tanshi.orderExtObj;

                }
                orderExtObj.ClinicCode = outOrder.Patient.ID;
                orderExtObj.Indications = outOrder.Item.Name;
                orderExtObj.MoOrder = outOrder.ID;
                orderExtObj.Oper.ID = orderExtMgr2.Operator.ID;
                if (tanshi.confirmSave == true)
                {
                    if (orderExtMgr2.InsertOrderExtend(orderExtObj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("������չ��Ϣ�����" + orderExtMgr2.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void SetFontColor(ArrayList rowsIndex,bool quit)
        {
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            foreach (int index in rowsIndex)
            {
                if (quit)
                {
                    this.fpOrder_Sheet1.RowHeader.Rows[index].ForeColor = Color.Red;
                    this.fpOrder_Sheet1.Rows[index].ForeColor = Color.Red;
                    outOrder = fpOrder_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    outOrder.QuitFlag = 1;
                }
                else
                {
                    this.fpOrder_Sheet1.RowHeader.Rows[index].ForeColor = Color.Black;
                    this.fpOrder_Sheet1.Rows[index].ForeColor = Color.Black;
                    outOrder = fpOrder_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    outOrder.QuitFlag = 0;
                }
            }
        }

        /// <summary>
        /// {5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// �����˷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuQuitAllOrder_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �˵����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
        }

        #endregion

        private void ucOrderHistory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            this.dtpEndTime.Value = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = this.dtpBeginTime.Value.AddDays(0 - defaultQueryDays);
            if(!isShowCopyAllClick)
            {
                this.button1.Visible = false;
            }


            if (IOutPatientPrint == null)// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
            {
                IOutPatientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint)) as FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint;
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text;

                if (Regex.IsMatch(cardNo, @"^\d*$"))
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    int rev = CacheManager.FeeIntegrate.ValidMarkNO(cardNo, ref accountCard);

                    if (rev > 0)
                    {
                        cardNo = accountCard.Patient.PID.CardNO;
                    }
                    //���ش�����
                    else
                    {
                        MessageBox.Show(CacheManager.FeeIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    cardNo = cardNo.PadLeft(10, '0');

                    this.txtCardNo.Text = cardNo;

                    this.Query(cardNo);
                }
                else
                {
                    Components.Common.Forms.frmQueryPatientByName frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByName();
                    frmQuery.QueryByName(cardNo);
                    frmQuery.SelectedPatient += new FS.HISFC.Components.Common.Forms.frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                    frmQuery.ShowDialog(this);

                    #region �ɵ�����

                    /*
                    ArrayList alPatietnInfo = CacheManager.RegInterMgr.QueryValidPatientsByName(cardNo);
                    if (alPatietnInfo == null || alPatietnInfo.Count < 1)
                    {
                        MessageBox.Show("δ�ҵ�������Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (alPatietnInfo.Count == 1)
                    {
                        FS.HISFC.Models.Registration.Register regTemp = null;
                        regTemp = alPatietnInfo[0] as FS.HISFC.Models.Registration.Register;
                        cardNo = regTemp.PID.CardNO;
                        cardNo = cardNo.PadLeft(10, '0');
                        this.txtCardNo.Text = cardNo;

                        this.Query(cardNo);
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Controls.NeuListView lvPatientInfo = new FS.FrameWork.WinForms.Controls.NeuListView();

                        System.Windows.Forms.ColumnHeader colCardNo = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colName = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colIdenNo = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colSexCode = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colBirthday = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colHomeTel = new ColumnHeader();
                        System.Windows.Forms.ColumnHeader colHome = new ColumnHeader();

                        colCardNo.Text = "������";
                        colCardNo.Width = 90;
                        colName.Text = "����";
                        colName.Width = 60;
                        colIdenNo.Text = "���֤��";
                        colIdenNo.Width = 140;
                        colSexCode.Text = "�Ա�";
                        colSexCode.Width = 40;
                        colBirthday.Text = "��������";
                        colBirthday.Width = 90;
                        colHomeTel.Text = "��ͥ�绰";
                        colHomeTel.Width = 90;
                        colHome.Text = "��ͥסַ";
                        colHome.Width = 200;

                        lvPatientInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                colCardNo,
                                                colName,
                                                colIdenNo,
                                                colSexCode,
                                                colBirthday,
                                                colHomeTel,
                                                colHome});

                        lvPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
                        lvPatientInfo.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        lvPatientInfo.FullRowSelect = true;
                        lvPatientInfo.GridLines = true;
                        lvPatientInfo.Name = "lvPatientInfo";
                        lvPatientInfo.Size = new System.Drawing.Size(500, 250);
                        lvPatientInfo.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
                        lvPatientInfo.View = System.Windows.Forms.View.Details;

                        foreach (FS.HISFC.Models.Registration.Register obj in alPatietnInfo)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = obj.PID.CardNO;
                            item.Tag = obj.PID.CardNO;
                            item.SubItems.Add(obj.Name);
                            item.SubItems.Add(obj.IDCard);
                            item.SubItems.Add(obj.Sex.Name);
                            item.SubItems.Add(obj.Birthday.ToShortDateString());
                            item.SubItems.Add(obj.PhoneHome);
                            item.SubItems.Add(obj.AddressHome);

                            lvPatientInfo.Items.Add(item);
                        }

                        lvPatientInfo.DoubleClick += new EventHandler(lvPatientInfo_DoubleClick);

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(lvPatientInfo, FormBorderStyle.None);
                    }
                     * */
                    #endregion
                }
            }
        }

        void frmQuery_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtCardNo.Text = patientInfo.PID.CardNO;
            Query(patientInfo.PID.CardNO);
        }

        void lvPatientInfo_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem listItem = new ListViewItem();
            string cardNo = string.Empty;
            if ((sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems.Count > 0)
            {
                listItem = (sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems[0];

                if (listItem != null)
                {
                    cardNo = listItem.SubItems[0].Text;
                    cardNo = cardNo.PadLeft(10, '0');
                    this.txtCardNo.Text = cardNo;

                    this.Query(cardNo);
                }
            }

            ((sender as ListView).Parent as Form).Close();
        }

        /// <summary>
        /// �������д���
        /// </summary>
        private void CopyAllOrder()
        {
            for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            {
                fpOrder_Sheet1.AddSelection(row, 0, 1, fpOrder_Sheet1.ColumnCount);
            }
            CopyOrder();
        }

        /// <summary>
        /// �������д���
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.CopyAllOrder();
            if (this.CopyAllClicked != null)
            {
                this.CopyAllClicked(myReg);
            }
        }

        /// <summary>
        /// �������� ������ţ����������
        /// </summary>
        public class OutOrderCompare : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                try
                {
                    FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                    FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                    if (FS.FrameWork.Function.NConvert.ToInt32(order1.SeeNO) > FS.FrameWork.Function.NConvert.ToInt32(order2.SeeNO))
                    {
                        return 1;
                    }
                    else if (FS.FrameWork.Function.NConvert.ToInt32(order1.SeeNO) == FS.FrameWork.Function.NConvert.ToInt32(order2.SeeNO))
                    {
                        if (order1.SortID > order2.SortID)
                        {
                            return 1;
                        }
                        else if (order1.SortID == order2.SortID)
                        {
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return 0;
                }
            }

            #endregion
        }

        /// <summary>
        /// �Һ��б��� �Һ�ʱ�� ��������
        /// </summary>
        public class RegInfoCompare : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Registration.Register reg1 = x as FS.HISFC.Models.Registration.Register;
                FS.HISFC.Models.Registration.Register reg2 = y as FS.HISFC.Models.Registration.Register;

                if (reg1.DoctorInfo.SeeDate > reg2.DoctorInfo.SeeDate)
                {
                    return -1;
                }
                else if (reg2.DoctorInfo.SeeDate == reg1.DoctorInfo.SeeDate)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            #endregion
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.txtCardNo.Text.Length <= 0)
            {
                MessageBox.Show("��ǰ����Ϊ�գ�");
                return;
            }

            this.Query(this.txtCardNo.Text);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
        {
            if (e.KeyData == Keys.Enter)
            {

                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.txtCardNo.Text = frmQuery.PatientInfo.PID.CardNO;
                    txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory GetCaseInfo()
        {
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
            caseHistory = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.myReg.ID);

            return caseHistory;
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //2012-10-9 11:20:56 houwb 
                    //��ɽ������һ��ҽ��ͬʱ���������ҵ���������˰����ֻ��һ��
                    //�޸�Ϊ�������Ҹ��ݵ�½����ȡ��ҽ����½��������Լ��������ˣ�

                    ////������Ű���Ϣ��ȥ�Ű������Ϊ�������� {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
                    //FS.HISFC.Models.Registration.Schema schema = CacheManager.RegInterMgr.GetSchema(GetReciptDoct().ID, this.CacheManager.OrderMgr.GetDateTimeFromSysDateTime());
                    //if (schema != null && schema.Templet.Dept.ID != "")
                    //{
                    //    this.reciptDept = schema.Templet.Dept.Clone();
                    //}
                    ////û���Ű�ȡ��½������Ϊ��������
                    //else
                    //{
                    this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoct()).Dept.Clone(); //��������
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return this.reciptDept;
        }

        /// <summary>
        /// ��ÿ���ҽ��
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDoct()
        {
            try
            {
                if (this.reciptDoct == null)
                    this.reciptDoct = CacheManager.OutOrderMgr.Operator.Clone();
            }
            catch { }
            return this.reciptDoct;
        }
        /// <summary>
        /// ��ӡ���е��� // {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
        /// </summary>        
        public void PrintAllBill(ArrayList alData, bool IsPreview)
        {
            if (alData == null || alData.Count <= 0)
            {
                return;
            }

            List<FS.HISFC.Models.Order.OutPatient.Order> alOrder = new List<FS.HISFC.Models.Order.OutPatient.Order>(); //����ҽ��

            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = this.GetCaseInfo();


            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alData)
            {
                order.SeeNO = this.myReg.DoctorInfo.SeeNO.ToString();
                order.Item.Extend1 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).ID;
                order.Item.Extend2 = CacheManager.OutOrderMgr.QueryApplyTypeByItemCode(order.Item.ID).Name;
                alOrder.Add(order);
            }

            #region ���ýӿ�ʵ�ִ�ӡ
            if (IOutPatientPrint != null && (alData.Count > 0 || caseHistory != null))
            {
                if (IOutPatientPrint.OnOutPatientPrint(myReg, this.GetReciptDept(), this.GetReciptDoct(), alOrder, alOrder, false, IsPreview, "0", false) != 1)
                {
                    MessageBox.Show(IOutPatientPrint.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }
        private void btPrint_Click(object sender, EventArgs e)// {78E78281-8B9D-41d8-9D94-9EC43DB43FD7}
        {
            if (allPrintData.Count > 0)
            {
                this.PrintAllBill(allPrintData,true);
            }
        }


        /// <summary>
        ///  //{D7C6FBD0-6BE3-4e44-9DE5-6293AAE1037F} �޸��ۺϲ�ѯ
        /// </summary>
        public void SetTime()
        {
            this.dtpEndTime.Value = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = this.dtpBeginTime.Value.AddDays(0 - defaultQueryDays);
        }
    }
}
