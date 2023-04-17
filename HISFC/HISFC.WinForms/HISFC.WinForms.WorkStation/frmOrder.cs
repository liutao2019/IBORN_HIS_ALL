using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
//using FS.Emr.Path.HIS50;
using FS.HISFC.Models.Order;

namespace FS.HISFC.WinForms.WorkStation
{
    /// <summary>
    /// ҽ������������
    /// </summary>
    public partial class frmOrder : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region ����

        /// <summary>
        /// ��ǰ�ؼ�
        /// </summary>
        private Control CurrentControl;

        /// <summary>
        /// ��ѯ�����б�
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucQueryInpatient ucQuerypatient = new FS.HISFC.Components.Common.Controls.ucQueryInpatient();

        /// <summary>
        /// ��Ⱦ���ϱ���
        /// </summary>
        private FS.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// ���Ӳ�������ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.EMR.IEMR emrInstance = null;

        /// <summary>
        /// �������б�
        /// </summary>
        FS.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// lisҽ���ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.ILis IResultPrint = null;

        /// <summary>
        /// סԺҽ��վѡ�������Ŀ�ӿ�
        /// </summary>
        private SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL IOrderChooseUl = null;

        /// <summary>
        /// �Ƿ�ǩ����Ӳ���ҽ��
        /// </summary>
        private bool isEMROrder = false;

        /// <summary>
        /// �Ƿ�ǩ����Ӳ���ҽ��
        /// </summary>
        public bool IsEMROrder
        {
            get
            {
                return isEMROrder;
            }
            set
            {
                isEMROrder = value;
                this.panel2.Visible = !isEMROrder;
            }
        }

        /// <summary>
        /// �ٴ�·��
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        /// <summary>
        /// �Ƿ�༭����ģʽ
        /// </summary>
        bool isEditGroup = false;

        bool isInit = false;

        /// <summary>
        /// �Ƿ����õ������뵥����
        /// </summary>
        private bool isPacsApplyEnable = false;

        #endregion

        /// <summary>
        /// ҽ������
        /// </summary>
        public frmOrder()
        {

            InitializeComponent();

            if (!DesignMode)
            {
                this.SetTree(this.tvDoctorPatientList1);
                this.iQueryControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOrder1;
                this.tbMTOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.YԤԼ);
                this.tbInfectionReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.S�ֶ�¼��);
                this.tbGroup.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z����);
                this.tbEMR.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
                this.tbEMR.Text = "����";
                this.tbQueryOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);
                this.tbExitOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
                this.tbPrintOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
                this.tbRecipePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);

                this.tbGroup.CheckState = CheckState.Unchecked;
                this.Resize += new EventHandler(frmOrder_Resize);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ucOrder1.refreshGroup += new FS.HISFC.Components.Order.Controls.RefreshGroupTree(ucOrder1_refreshGroup);
                this.neuTextBox1.MouseDoubleClick += new MouseEventHandler(neuTextBox1_MouseDoubleClick);

                this.panel2.Visible = !isEMROrder;
            }
        }

        /// <summary>
        /// ˫������ѡ���� ���������ҿ���ҽ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode parent = new TreeNode();

            foreach (TreeNode node in this.tvDoctorPatientList1.Nodes)
            {
                if (node.Text.IndexOf("������") >= 0)
                {
                    parent = node;
                    break;
                }
            }

            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucQuerypatient) == DialogResult.OK)
            {
                TreeNode node = new TreeNode();

                if (FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.Dept.ID
                    == (this.consultation.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                {
                    //FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "����";
                }
                else
                {
                    //FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "ҽ��";
                }

                node.Tag = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient;
                node.Text = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Name;

                parent.Nodes.Add(node);
                parent.ExpandAll();
            }
        }

        /// <summary>
        /// ˢ��������
        /// </summary>
        private void ucOrder1_refreshGroup()
        {
            this.tvGroup.RefrshGroup();
        }

        void frmOrder_Resize(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.panelTree.Height = this.Height - 162;
            }
        }

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrder_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.WindowState = FormWindowState.Maximized;
                this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
                tbBatchDeal.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
                this.AddOrderHandle();
                this.initButton(false);

                //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
                this.tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�ײ�);
                this.tbItem.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�ײ�);
                this.tbAddOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��);
                this.tbComboOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�);
                this.tbCancelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
                this.tbDelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��);
                this.tbOperation.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z���);
                this.tbSaveOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
                this.tbCheck.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
                this.tb1Exit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
                this.tbEMR.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
                this.tsbHerbal.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ);
                /// ���Ӽ�����Ŀѡ��ť
                this.tbChooseUL.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);
                ///{FB86E7D8-A148-4147-B729-FD0348A3D670}  ����ҽ��������ť
                this.tbRetidyOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��);

                this.tbLevelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z�Ӽ�);

                this.tbAssayCure.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);

                this.tbDiseaseReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J��������);
                this.tbFilter.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C����);

                this.tbChooseDoct.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H��ҽʦ);

                this.tbLisResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
                this.tbResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);

                this.tbPacsResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S�豸);
                this.tbDcAllLongOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��);
                this.tbPass.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
                this.tbPrintAgain.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��);
                this.tbMSG.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ); //{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
                this.panelTree.Height = this.Height - 162;
                this.toolStrip1.Focus();
                string treeSearchMode = "1";

                treeSearchMode = controlParamMgr.GetControlParam<string>("200303", true, "1");

                if (treeSearchMode == "2")
                {
                    this.ucQueryInpatientNo1.Visible = true;
                }
                else
                {
                    this.ucQueryInpatientNo1.Visible = false;
                }

                this.SetMenuVisible();


                this.IsDoubleSelectValue = false;

                this.ucOrder1.OnRefreshGroupTree += new EventHandler(ucOrder1_OnRefreshGroupTree);

                Application.DoEvents();

                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.IOrderChooseUl == null)
                {
                    this.IOrderChooseUl = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL;
                }

                if (this.dcpInstance != null)
                {
                    this.dcpInstance.LoadNotice(this, FS.HISFC.Models.Base.ServiceTypes.I);
                }

                #region  add by lijp 2011-11-25 �������뵥��� {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

                this.tblEditPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S���뵥);

                try
                {
                    isPacsApplyEnable = controlParamMgr.GetControlParam<bool>("PACSZY");
                }
                catch
                {
                    isPacsApplyEnable = false;
                }

                #endregion

                try
                {
                    FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                    FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(consultation.Operator.ID);

                    if (setting == null)
                    {
                        //MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.tbPass.Checked = FS.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                        this.ucOrder1.EnabledPass = this.tbPass.Checked;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        void ucOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        #region ˽�к���

        /// <summary>
        /// �˵����ÿɼ���
        /// </summary>
        private void SetMenuVisible()
        {
            FS.HISFC.BizProcess.Integrate.Manager dictionaryMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList alset = dictionaryMgr.GetConstantList("INPATMENU");

            FS.FrameWork.Public.ObjectHelper setHelper = new FS.FrameWork.Public.ObjectHelper();

            setHelper.ArrayObject = alset;

            ToolStripButton tButton = null;
            ToolStripDropDownButton tdButton = null;

            Hashtable hsTb = new Hashtable();
            foreach (object obj in toolStrip1.Items)
            {
                if (obj.GetType() == typeof(ToolStripButton))
                {
                    tButton = obj as ToolStripButton;
                    if (!hsTb.Contains(tButton.Name))
                    {
                        hsTb.Add(tButton.Name, tButton);
                    }
                }
                else if (obj.GetType() == typeof(ToolStripDropDownButton))
                {
                    tdButton = obj as ToolStripDropDownButton;
                    if (!hsTb.Contains(tdButton.Name))
                    {
                        hsTb.Add(tdButton.Name, tdButton);
                    }

                    if (tdButton.DropDownItems.Count > 0)
                    {
                        foreach (ToolStripDropDownItem o in tdButton.DropDownItems)
                        {
                            if (!hsTb.Contains(o.Name))
                            {
                                hsTb.Add(o.Name, o);
                            }
                        }
                    }
                }
            }

            this.toolStrip1.Items.Clear();
            foreach (FS.HISFC.Models.Base.Const conObj in alset)
            {
                if (conObj.IsValid && hsTb.Contains(conObj.ID.Trim()))
                {
                    if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripButton))
                    {
                        tButton = new ToolStripButton();
                        tButton = hsTb[conObj.ID.Trim()] as ToolStripButton;

                        tButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(setHelper.GetObjectFromID(tButton.Name).Memo))
                        {
                            tButton.Text = tButton.Text + "(&" + setHelper.GetObjectFromID(tButton.Name).Memo + ")";
                        }
                        tButton.Visible = true;
                        this.toolStrip1.Items.Add(tButton);
                    }
                    else if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripDropDownButton))
                    {
                        tdButton = new ToolStripDropDownButton();
                        tdButton = hsTb[conObj.ID.Trim()] as ToolStripDropDownButton;

                        tdButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(setHelper.GetObjectFromID(tdButton.Name).Memo))
                        {
                            tdButton.Text = tdButton.Text + "(&" + setHelper.GetObjectFromID(tdButton.Name).Memo + ")";
                        }
                        tdButton.Visible = true;

                        #region

                        //�Զ���������ť
                        if (!string.IsNullOrEmpty(conObj.UserCode)
                            && string.Equals(conObj.UserCode, "ResultPrint"))
                        {
                            ArrayList dropItemList = dictionaryMgr.GetConstantList(conObj.UserCode);
                            if (dropItemList.Count > 0)
                            {
                                tdButton.DropDownItems.Clear();
                                System.Windows.Forms.ToolStripMenuItem drop;
                                foreach (FS.HISFC.Models.Base.Const item in dropItemList)
                                {
                                    if (item.IsValid)
                                    {
                                        drop = new ToolStripMenuItem();
                                        drop.Name = item.ID;
                                        drop.Text = item.Name;
                                        drop.Tag = item.Memo;
                                        drop.Click += new System.EventHandler(this.tmResultPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ArrayList dropItemList = dictionaryMgr.GetConstantList(conObj.UserCode);
                            if (dropItemList.Count > 0)
                            {
                                tdButton.DropDownItems.Clear();
                                System.Windows.Forms.ToolStripMenuItem drop;
                                foreach (FS.HISFC.Models.Base.Const item in dropItemList)
                                {
                                    if (conObj.IsValid)
                                    {
                                        drop = new ToolStripMenuItem();
                                        drop.Text = item.Name;
                                        drop.Click += new System.EventHandler(this.tmPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }
                        #endregion

                        this.toolStrip1.Items.Add(tdButton);
                    }

                    if (conObj.UserCode.Trim() == "�����")
                    {
                        this.toolStrip1.Items.Add(new ToolStripSeparator());
                    }
                }
            }
        }

        /// <summary>
        /// ҽ�ƽ����ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmResultPrint_Click(object sender, EventArgs e)
        {
            FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType resultType = (FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType)FS.FrameWork.Function.NConvert.ToInt32(((ToolStripMenuItem)sender).Name);

            ucOrder1.QueryMedicalResult(resultType);
        }

        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;

            if (isEMROrder)
            {
                if (isDisign)
                {
                    panel2.Visible = true;
                }
                else if (tbGroup.Enabled && tbGroup.CheckState == CheckState.Checked)
                {
                    this.panel2.Visible = true;
                }
                else
                {
                    this.panel2.Visible = false;
                }
            }
            else
            {
                this.panel2.Visible = true;
            }       

            tbRefresh.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbPrintOrder.Enabled = !isDisign;
            this.tbRecipePrint.Enabled = !isDisign;
            this.tbResultPrint.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            //this.tbOperation.Enabled = false;
            this.tbOperation.Enabled = isDisign;
            this.tblEditPacsApply.Enabled = !isDisign;
            this.tbPrintAgain.Enabled = !isDisign;
            this.tbAssayCure.Enabled = isDisign;
            this.tbDelOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            //this.tbFilter.Enabled = !isDisign;
            this.tbQueryOrder.Enabled = !isDisign;
            this.tbSaveOrder.Enabled = isDisign;
            this.tsbHerbal.Enabled = isDisign;
            this.tbChooseUL.Enabled = isDisign;
            this.tbLevelOrder.Enabled = isDisign;
            this.tbDcAllLongOrder.Enabled = !isDisign;
            //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            this.tbChooseDoct.Enabled = isDisign;
            if (isDisign) //����
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();

                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                if (ucOrder1.Patient != null)
                {
                    tvGroup.Pact = ucOrder1.Patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
                //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
                //�жϵ�ǰ��Ա�Ƿ�ҽ��
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).EmployeeType.ID.ToString() == FS.HISFC.Models.Base.EnumEmployeeType.D.ToString())
                {
                    this.tbChooseDoct.Enabled = false;
                }
                else
                {
                    this.tbChooseDoct.Enabled = true;
                }
                if (this.ucOrder1.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.tsbHerbal.Enabled = false;
                    this.tbChooseUL.Enabled = false;
                    this.tbLevelOrder.Enabled = false;
                    this.tbOperation.Enabled = false;
                    this.tblEditPacsApply.Enabled = !isDisign;
                }
            }
            else
            {
                this.tvDoctorPatientList1.Visible = true;
                if (tvGroup != null)
                    tvGroup.Visible = false;
            } 
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOrder1.AddGroupOrder(alOrders);
        }

        /// <summary>
        /// ���ݴ�ӡ��ťʵ�֣����ơ�������ָ�������飩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmPrint_Click(object sender, EventArgs e)
        {
           this.ucOrder1.PrintAgain(((ToolStripMenuItem)sender).Text);
        }

        /// <summary>
        /// ���ð�ť
        /// </summary>
        /// <param name="isEdit">�Ƿ���ģʽ</param>
        private void InitButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbPrintOrder.Enabled = !isEdit;
            this.tbRecipePrint.Enabled = !isEdit;
            this.tbResultPrint.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            this.tbRefresh.Enabled = !isEdit;
            this.isEditGroup = isEdit;
            //{EB959BC4-9120-478a-B527-74A1D7EF4C9E}
            this.tbComboOrder.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;
            //{74E478F5-BDDD-4637-9F5A-E251AF9AA72F}
            this.tbRetidyOrder.Enabled = !isEdit;
            this.tbDcAllLongOrder.Enabled = !isEdit;
            this.tbDelOrder.Enabled = isEdit;

            if (isEdit) //����
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
            }
            else
            {
                this.tvDoctorPatientList1.Visible = true;
                if (tvGroup != null)
                    tvGroup.Visible = false;
            }

            //{C1EA8590-F089-49cb-A045-F0D7A09D2C3E}
            //if (tbGroup.CheckState == CheckState.Checked)
            //{
            //    this.panel2.Visible = true;
            //}
            //else
            //{
            //    if (isEMROrder && isEdit)
            //    {
            //        panel2.Visible = true;
            //    }
            //    else
            //    {
            //        this.panel2.Visible = false;
            //    }
            //}      
        }

        /// <summary>
        /// ί��
        /// </summary>
        private void AddOrderHandle()
        {
            this.ucOrder1.OrderCanCancelComboChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanCancelComboChanged);
            this.ucOrder1.OrderCanOperatorChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanOperatorChanged);
            this.ucOrder1.OrderCanSetCheckChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanSetCheckChanged);
        }

        void ucOrder1_OrderCanSetCheckChanged(bool b)
        {
            this.tbCheck.Enabled = b;
        }

        void ucOrder1_OrderCanOperatorChanged(bool b)
        {
            this.tbOperation.Enabled = b;
            this.tbAssayCure.Enabled = b;
            this.tsbHerbal.Enabled = b;
            this.tbChooseUL.Enabled = b;
            this.tbLevelOrder.Enabled = b;
        }

        void ucOrder1_OrderCanCancelComboChanged(bool b)
        {
            this.tbCancelOrder.Enabled = b;
        }
        #endregion

        /// <summary>
        /// ��ǰ����ʵ��
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        #region ��װ���ã����ⲿ���ã�����Ӳ�����

        /// <summary>
        /// ��ǰ����
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// ��ǰ�����б�ֻ�����ⲿ����
        /// </summary>
        protected FS.HISFC.Components.Order.Controls.tvDoctorPatientList TvDoctorPatientList
        {
            get
            {
                return this.tvDoctorPatientList1;
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TvDoctorPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.tvDoctorPatientList1_AfterSelect(sender, e);
        }

        #endregion

        FS.HISFC.BizProcess.Integrate.RADT inpatientManager = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Order.Consultation consultation = new FS.HISFC.BizLogic.Order.Consultation();

        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Components.Order.Controls.ReciptPatientType GetPatientType()
        {
            if (tvDoctorPatientList1 != null && tvDoctorPatientList1.SelectedNode != null
                && tvDoctorPatientList1.SelectedNode.Tag != null)
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent != null)
                {
                    switch (tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString())
                    {
                        case "patient"://�ֹܻ���|patient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.PrivatePatient;
                            break;
                        case "DeptPatient"://�����һ���|DeptPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
                            break;
                        case "ConsultationPatient"://���ﻼ��|ConsultationPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.ConsultationPatient;
                            break;
                        case "PermissionPatient"://��Ȩ����|PermissionPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.AuthorizedPatient;
                            break;
                        case "QueryPatient"://���һ���|QueryPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.FindedPatient;
                            break;
                        case "TeamPatient"://ҽ�����ڻ���|TeamPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.MedicsPatient;
                            break;
                        default:
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
                            break;
                    }
                }
            }

            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            ucOrder1.PatientType = this.GetPatientType();

            #region ����

            if (e.ClickedItem == this.tbAddOrder)
            {
                //ѡ���ӽڵ�
                if (this.tvDoctorPatientList1.SelectedNode.Parent != null && this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null)
                {
                    string inpatientNo;
                    ArrayList co = null;

                    int count = 0;
                    count = this.tvDoctorPatientList1.SelectedNode.Parent.GetNodeCount(false);
                    //�ж���ѡ�ڵ㸸�ڵ����Ϊ���ﻼ��,���ж����޿���ҽ����Ȩ��/
                    //������ǻ��ﻼ������Ҫ�����ж�,�����Խ��п���ҽ��
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Text == ("���ﻼ��" + "(" + count.ToString() + ")"))
                    {
                        patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                        //����λ�Ž�λ
                        string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //����סԺ����ʾ
                        string patientNO = patient.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient.ID;
                        }

                        inpatientNo = patient.ID;
                        co = consultation.QueryConsulation(inpatientNo);
                        bool iSCreateConsulationOrder = false;
                        if (co != null || co.Count != 0)
                        {
                            for (int i = 0; i < co.Count; i++)
                            {
                                FS.HISFC.Models.Order.Consultation obj = co[i] as FS.HISFC.Models.Order.Consultation;
                                //���ݻ��ﻼ����Ч�Ļ��ﵥ��Ϣ,�ж�ҽ���Ƿ��жԸû��ﻼ�߿���ҽ��Ȩ��
                                if ((FS.FrameWork.Management.Connection.Operator.ID == obj.DoctorConsultation.ID) &&
                                    (obj.EndTime >= consultation.GetDateTimeFromSysDateTime())
                                    && (obj.IsCreateOrder))
                                {
                                    if (this.ucOrder1.Add() == 0)
                                        this.initButton(true);
                                    iSCreateConsulationOrder = true;

                                    break;//{3541798B-AF9C-415c-AFAA-8BD22A34A808}
                                }
                                else if (string.IsNullOrEmpty(obj.DoctorConsultation.ID) &&
                                    (obj.EndTime >= consultation.GetDateTimeFromSysDateTime())
                                    && (obj.IsCreateOrder))
                                {
                                    if (this.ucOrder1.Add() == 0)
                                        this.initButton(true);
                                    iSCreateConsulationOrder = true;
                                }

                            }
                        }


                        if (iSCreateConsulationOrder.Equals(false))
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Բ���,��û�жԸû��߿���ҽ����Ȩ��!"), "��ʾ");
                            return;
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.PatientInfo patient1 = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                        //����λ�Ž�λ
                        string bedNO = patient1.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //����סԺ����ʾ
                        string patientNO = patient1.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient1.ID;
                        }

                        //�ж��Ƿ���ٻ���
                        if (patient1.PVisit.PatientLocation.Bed != null && patient1.PVisit.PatientLocation.Bed.Status.ID.ToString() == FS.HISFC.Models.Base.EnumBedStatus.R.ToString())
                        {
                            //��ٻ��߲��ܿ�ҽ�����������٣���ҪΪ��ֹ��ٺ��������´�ִ��ʱ�䲻�ԣ����bug�е�2
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������У����迪��ҽ����������"));
                            return;
                        }

                        if (this.ucOrder1.Add() == 0)
                            this.initButton(true);
                    }

                    //ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                    //ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);


                    if (this.iClinicPath == null)
                    {
                       // iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
                       //as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
                    }

                    if (iClinicPath != null)
                    {
                        iClinicPath.ExecutePathOrder(this.Patient.ID);
                    }
                }
            }
            #endregion

            #region �ײͲ鿴
            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            else if (e.ClickedItem == this.tbPackage)
            {
                if (this.ucOrder1.Patient == null || string.IsNullOrEmpty(this.ucOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("���ȼ������ߣ�");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                frmpackage.DetailVisible = true;//{e87b8fc3-e03c-43eb-be1a-97473bc93ebb}
                frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOrder1.Patient.PID.CardNO);
                frmpackage.ShowDialog();
            }

            #endregion 

            else if (e.ClickedItem == this.tbItem)  //{048e65b9-0b30-4049-9d66-e74fbe28c2fa}
            {
                if (this.ucOrder1.Patient == null || string.IsNullOrEmpty(this.ucOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("���ȼ������ߣ�");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Components.Common.Forms.frmUnExecutedItem frmItem = new FS.HISFC.Components.Common.Forms.frmUnExecutedItem();
                frmItem.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOrder1.Patient.PID.CardNO);
                frmItem.ShowDialog();
            }

            #region ���
            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOrder1.AddTest();
            }
            #endregion

            #region ˢ��
            else if (e.ClickedItem == this.tbRefresh)
            {
                //ˢ��
                this.tvDoctorPatientList1.RefreshInfo();
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbGroup)
            {
                if (this.tbGroup.CheckState == CheckState.Unchecked)
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                    this.ucOrder1.SetEditGroup(true);
                    this.ucOrder1.SetPatient(null);
                    this.InitButtonGroup(true);
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                    this.ucOrder1.SetEditGroup(false);
                    this.InitButtonGroup(false);


                    if (this.tvDoctorPatientList1.SelectedNode != null &&
                        this.tvDoctorPatientList1.SelectedNode.Tag != null)
                    {
                        this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                    }
                }
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbOperation)
            {
                FS.HISFC.Models.RADT.PatientInfo pi = (FS.HISFC.Models.RADT.PatientInfo)this.tvDoctorPatientList1.SelectedNode.Tag;
                frmOperation frmOpt = new frmOperation(pi);
                frmOpt.ShowDialog();
            }
            #endregion

            #region
            else if (e.ClickedItem == this.tbAssayCure)
            {
                this.ucOrder1.AddAssayCure();
            }
            #endregion

            #region ɾ��
            else if (e.ClickedItem == this.tbDelOrder)
            {
                this.ucOrder1.Delete();
            }
            #endregion

            #region ��ѯ
            else if (e.ClickedItem == this.tbQueryOrder)
            {
                try
                {
                    this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                }
                catch { }
            }
            #endregion

            #region ��ӡ
            else if (e.ClickedItem == this.tbPrintOrder)
            {
                this.ucOrder1.PrintOrder();
            }
            #endregion


            #region ������ӡ
            else if (e.ClickedItem == this.tbRecipePrint)// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}
            {
                this.ucOrder1.RecipePrint();
            }
            #endregion

            #region ���
            else if (e.ClickedItem == this.tbComboOrder)
            {
                this.ucOrder1.ComboOrder();
            }
            #endregion

            #region ȡ�����
            else if (e.ClickedItem == this.tbCancelOrder)
            {
                this.ucOrder1.CancelCombo();
            }
            #endregion

            #region �˳�ҽ��
            else if (e.ClickedItem == this.tbExitOrder)
            {
                if (this.isEditGroup)
                {
                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.tbGroup.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        this.tbGroup.CheckState = CheckState.Checked;
                    }
                    this.ucOrder1.SetEditGroup(false);
                }
                else
                {
                    if (this.ucOrder1.ExitOrder() == 0)
                        this.initButton(false);

                    //����ʧȥ���߽��㣬�˴�����ˢ���б������ˢ�£����ˢ�°�ť
                    //tvDoctorPatientList1.RefreshInfo();
                }
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbInValid)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Invalid);
            }
            else if (e.ClickedItem == this.tbValid)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Valid);
            }
            else if (e.ClickedItem == this.tbAll)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.All);
            }
            else if (e.ClickedItem == this.tbToday)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Today);
            }
            else if (e.ClickedItem == this.tbNew)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.New);
            }
            else if (e.ClickedItem == this.tbUCULOrder)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.UC_ULOrder);
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbSaveOrder)
            {
                if (isEditGroup)
                {
                    this.ucOrder1.SaveGroup();
                }
                else
                {
                    if (this.ucOrder1.Save() == -1)
                    {
                    }
                    else
                    {
                        this.initButton(false);
                    }
                }
            }
            #endregion

            #region ��ҩ
            else if (e.ClickedItem == this.tsbHerbal)
            {
                this.ucOrder1.HerbalOrder();
            }
            #endregion

            #region �㼶ҽ��
            else if (e.ClickedItem == this.tbLevelOrder)
            {
                this.ucOrder1.AddLevelOrders();
            }
            #endregion

            #region ��ҽʦ
            else if (e.ClickedItem == this.tbChooseDoct)//{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            {
                this.ucOrder1.ChooseDoctor();
            }
            #endregion

            #region �˳�����
            else if (e.ClickedItem == this.tb1Exit)
            {
                if (this.ucOrder1.IsDesignMode) //���ڿ���״̬
                {
                    DialogResult result = MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ��Ŀǰ���ڿ���ģʽ���Ƿ񱣴�?"), "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        if (this.ucOrder1.Save() == 0)
                            this.Close();

                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            #endregion

            #region ����ҽ��
            else if (e.ClickedItem == this.tbRetidyOrder)
            {
                if (this.ucOrder1.IsDesignMode == false)
                {
                    this.ucOrder1.ReTidyOrder();
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ǿ���״̬�²��������ҽ������"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            #endregion

            #region ��Ⱦ������
            else if (e.ClickedItem == this.tbDiseaseReport)
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                    this.dcpInstance.RegisterDiseaseReport(this, patientInfo, FS.HISFC.Models.Base.ServiceTypes.I);
                }
            }
            #endregion

            #region ҽ�ƽ��
            else if (e.ClickedItem == this.tbLisResultPrint)
            {
                this.ucOrder1.QueryLisResult();
            }

            else if (e.ClickedItem == this.tbResultPrint)
            {

            }
            #endregion

            #region PACS����鿴
            else if (e.ClickedItem == this.tbPacsResultPrint)
            {
                this.ucOrder1.QueryPacsReport();
            }
            #endregion

            #region ����ȫͣ
            //ֹͣȫ������ҽ�� houwb 2011-3-11 {46E8908F-4248-4a40-89B1-530CA5796CD4}
            else if (e.ClickedItem == this.tbDcAllLongOrder)
            {
                this.ucOrder1.DcAllLongOrder("");
            }
            #endregion

            #region �������뵥���

            else if (e.ClickedItem == this.tblEditPacsApply)
            {
                this.ucOrder1.EditPascApply();
            }

            #endregion

            #region ������ҩ

            else if (e.ClickedItem == this.tbPass)
            {
                this.tbPass.Checked = !this.tbPass.Checked;

                this.ucOrder1.EnabledPass = this.tbPass.Checked;

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(consultation.Operator.ID);

                if (setting == null)
                {
                    setting = new FS.HISFC.Models.Base.UserDefaultSetting();
                    setting.Empl.ID = consultation.Operator.ID;
                    setting.Oper.ID = consultation.Operator.ID;
                    setting.Oper.OperTime = consultation.GetDateTimeFromSysDateTime();
                    setting.Setting5 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                    if (userSetmgr.Insert(setting) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
                else
                {
                    setting.Setting5 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (userSetmgr.Update(setting) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }

            #endregion
            #region ��Ϣ����{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
            else if (e.ClickedItem == this.tbMSG)
            {
                FS.HISFC.Models.RADT.PatientInfo selectedPatient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;//.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (selectedPatient == null || string.IsNullOrEmpty(selectedPatient.PID.CardNO))
                {
                    MessageBox.Show("���ȼ������ߣ�");
                    return;
                }
                FS.HISFC.Components.Order.Forms.frmAddMsgForm msg = new FS.HISFC.Components.Order.Forms.frmAddMsgForm();
                msg.patient = selectedPatient;
                msg.Init();
                msg.ShowDialog();
            }
            #endregion


            #region ����ҽ��

            else if (e.ClickedItem == this.tbPostOperat)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��
                ucOrder1.AddDCOrder("5");
            }

            #endregion

            #region ת��ҽ��

            else if (e.ClickedItem == this.tbSwitchDept)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��
                ucOrder1.AddDCOrder("2");
            }

            #endregion

            #region ����ҽ��

            else if (e.ClickedItem == this.tbDead)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��
                ucOrder1.AddDCOrder("3");
            }

            #endregion

            #region ��ǰҽ��

            else if (e.ClickedItem == this.tbPreOperat)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��
                ucOrder1.AddDCOrder("4");
            }

            #endregion

            #region ��Ժҽ��

            else if (e.ClickedItem == this.tbLeaveHos)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ��
                ucOrder1.AddDCOrder("1");
            }

            #endregion

            #region �޸Ĵ�������

            else if (e.ClickedItem == this.tbTreatmentType)
            {
                //1����Ժҽ����2��ת��ҽ����3������ҽ����4����ǰҽ����5������ҽ����6���޸Ĵ�������
                ucOrder1.AddDCOrder("6");
            }

            #endregion

            #region ���Ӳ����ӿ�
            else if (e.ClickedItem == this.tbEMR)
            {
                if (this.emrInstance == null)
                {
                    this.emrInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.EMR.IEMR)) as FS.HISFC.BizProcess.Interface.EMR.IEMR;
                }

                if (this.emrInstance != null)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                    //{E3ED6553-0A5D-43e9-8D0C-DD24E42E8258}
                    if (this.emrInstance.EMRRegister(this, patientInfo, FS.HISFC.Models.Base.ServiceTypes.I) == -1)
                    {
                        MessageBox.Show(this.emrInstance.ErrMsg);
                    }
                }
            }
            #endregion

            #region ҽ��ԤԼ
            if (e.ClickedItem == this.tbMTOrder)//����
            {
                FS.HISFC.Components.MTOrder.frmMTApply newfrm = new FS.HISFC.Components.MTOrder.frmMTApply();
                newfrm.PatientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (ucOrder1.SelectedOrder != null)
                    newfrm.SelectedOrderID = ucOrder1.SelectedOrder.ID;
                newfrm.ShowDialog();
            }
            #endregion

            #region Ժ���ϱ�
            if (e.ClickedItem == this.tbInfectionReport)//Ժ���ϱ�
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent == null || this.tvDoctorPatientList1.SelectedNode.Parent.Tag == null)
                {
                    MessageBox.Show("����ѡ����Ҫ�ϱ��Ļ��ߣ�");
                    return;
                }
                FS.HISFC.Models.RADT.PatientInfo patientInfo= this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                patientInfo.Memo = "1";//סԺ��ʶ
                tbInfectionReport.Tag = patientInfo;
                this.tbInfectionReport_Click(this.tbInfectionReport);
            }
            #endregion

            #region ������Ŀ��������

            if (e.ClickedItem == this.tbChooseUL)
            {
                if (this.IOrderChooseUl != null)
                {
                    if (this.ucOrder1.Patient == null)
                    {
                        MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                        return;
                    }
                    ArrayList alOrders = new ArrayList();
                    if (this.IOrderChooseUl.GetChooseUL(ref alOrders) < 0)
                    {
                        MessageBox.Show(IOrderChooseUl.Err, "����", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        if (alOrders != null && alOrders.Count > 0)
                        {
                            this.ucOrder1.AddGroupOrder(alOrders);
                        }
                    }
                }
            }

            #endregion


            if (tvGroup != null)
            {
                if (ucOrder1.Patient != null)
                {
                    tvGroup.Pact = patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [Obsolete("����,��HISFC.Components.Order.Controls.ucOrder��SaveGroup����", true)]
        private void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();

            try
            {
                group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            #region ������ʱһ�𱣴�����{11F97F55-F747-4ad9-A74F-086635D5EBD9}
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[0].Rows.Count; i++)//����ҽ��
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                FS.HISFC.Models.Order.Inpatient.Order longorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 0);
                if (longorderTemp == null)
                {
                    continue;
                }

                //FS.HISFC.Models.Order.Inpatient.Order longorder = this.ucOrder1.GetObjectFromFarPoint(i, 0).Clone();
                FS.HISFC.Models.Order.Inpatient.Order longorder = longorderTemp.Clone();
                if (longorder == null)
                {
                    MessageBox.Show("���ҽ������");
                }
                else
                {
                    string s = longorder.Item.Name;
                    string sno = longorder.Combo.ID;
                    //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
                    longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
                    al.Add(longorder);
                }
            }
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[1].Rows.Count; i++)//��ʱҽ��
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                FS.HISFC.Models.Order.Inpatient.Order shortorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 1);
                if (shortorderTemp == null)
                {
                    continue;
                }
                //FS.HISFC.Models.Order.Inpatient.Order shortorder = this.ucOrder1.GetObjectFromFarPoint(i, 1).Clone();
                FS.HISFC.Models.Order.Inpatient.Order shortorder = shortorderTemp.Clone();
                if (shortorder == null)
                {
                    MessageBox.Show("���ҽ������");
                }
                else
                {
                    string s = shortorder.Item.Name;
                    string sno = shortorder.Combo.ID;
                    //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
                    shortorder.BeginTime = new DateTime(shortorder.BeginTime.Year, shortorder.BeginTime.Month, shortorder.BeginTime.Day, 0, 0, 0);
                    al.Add(shortorder);
                }
            }
            #endregion
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //if (this.CurrentControl.GetType() == typeof(FS.Pathway.His50.ucHisEcp))
                //{
                //    if (this.ucOrder1.GetUnSavedOrders().Count > 0)
                //    {
                //        MessageBox.Show("����δ�����ҽ����");
                //        this.tabControl1.SelectedIndex = 0;
                //        return;
                //    }

                //    //if (!isInit)
                //    //{
                //    //    this.isInit = true;

                //    //    (this.CurrentControl as FS.Pathway.His50.ucHisEcp).pathWayForm.ucPathwayDay.sendOrderList
                //    //          += new Ecp.WinForms.ucPathwayDay.SendOrderList(ucPathwayDay_sendOrderList);
                //    //}
                //}


                //if (this.CurrentControl.GetType() == typeof(ucEcp))
                //{
                //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
                //}

                //סԺ�ٴ�·�� {CE9F7C07-A6B3-4ab4-BC47-2F88411BB541}
                //if (this.CurrentControl.GetType() == typeof(ucEcp))
                //{
                //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
                //}
            }
        }

        void ucPathwayDay_sendOrderList(ArrayList alOrders)
        {
            //ToolStripItemClickedEventArgs e = new ToolStripItemClickedEventArgs(this.tbAddOrder);

            //if (this.ucOrder1.IsDesignMode == false)
            //{
            //    this.toolStrip1_ItemClicked_1(null, e);
            //}

            //this.ucOrder1.PasteOrder(list);

            //this.tabControl1.SelectedIndex = 0;

            alOrders.Reverse();
            this.tabControl1.SelectedIndex = 0;
            //this.ucOrder1.SetPatient(this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo);
            //this.patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (!this.ucOrder1.IsDesignMode)
            {
                if (this.ucOrder1.Add() == 0)
                {
                    this.initButton(true);
                }
            }

            this.ucOrder1.AddPathwayOrders(alOrders);
            //foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            //{
            //    if (order.Item.SysClass.ID.ToString() == "PCC")
            //    {
            //        continue;
            //    }
            //    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
            //    {
            //        order.FirstUseNum = "1";
            //        this.ucOrder1.AddNewOrder(order, 0);
            //    }
            //    else
            //    {
            //        this.ucOrder1.AddNewOrder(order, 1);
            //    }
            //}
            //this.ucOrder1.RefreshCombo();
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                return;
            }

            if (this.ucOrder1.IsDesignMode)
            {
                MessageBox.Show("����״̬���ܲ�ѯ����");
                return;
            }
            this.tvDoctorPatientList1.QueryPaitent(this.ucQueryInpatientNo1.InpatientNo, this.consultation.Operator as FS.HISFC.Models.Base.Employee);
        }

        private void tvDoctorPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDoctorPatientList1.SelectedNode.Tag != null
               && this.tvDoctorPatientList1.SelectedNode.Tag.ToString() != ""
               && this.tvDoctorPatientList1.SelectedNode.Parent != null)
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null
                    && this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() != "")
                {
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() == "QueryPatient")
                    {
                        if (this.CurrentControl.GetType().FullName == "FS.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = false;
                        }
                    }
                    else
                    {
                        if (this.CurrentControl.GetType().FullName == "FS.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = true;

                            patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                            string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                            if (bedNO.Length > 4)
                            {
                                bedNO = bedNO.Substring(4);
                            }
                            this.Text = "�����ڲ����Ļ���Ϊ סԺ�ţ�" + patient.PID.PatientNO + " ������" + patient.Name + " �Ա�" + patient.Sex.Name + " ���䣺" + consultation.GetAge(patient.Birthday) + " ����:" + bedNO;
                        }
                    }
                }
            }
            else
            {
                this.Text = "";
            }

            //סԺ�ٴ�·�� {CE9F7C07-A6B3-4ab4-BC47-2F88411BB541}
            //if (this.CurrentControl.GetType() == typeof(ucEcp))
            //{
            //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

            //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
            //    this.tabControl1.SelectedIndex = 0;
            //}
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!DesignMode)
            {
                this.ucOrder1.ReleaseLisInterface();
                this.ucOrder1.ReleasePacsInterface();
                this.ucOrder1.QuitPass();
            }
        }
        /// <summary>
        /// Ժ���ϱ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInfectionReport_Click(object sender)
        {
            if (object.Equals(this.Tag, null))
            {
                MessageBox.Show("����ѡ����Ҫ�ϱ��Ļ��ߣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IResultPrint == null)
            {
                IResultPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Forms.frmOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
            }

            if (!((ToolStripButton)sender).Name.Equals(null))
            {
                IResultPrint.ResultType = ((ToolStripButton)sender).Name.ToString();
                IResultPrint.SetPatient(((ToolStripButton)sender).Tag as FS.HISFC.Models.RADT.Patient);
                IResultPrint.ShowResultByPatient();
            }
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP) ,
                    typeof(FS.HISFC.BizProcess.Interface.Common.IPacs)
                };
            }
        }

        #endregion

    }
}