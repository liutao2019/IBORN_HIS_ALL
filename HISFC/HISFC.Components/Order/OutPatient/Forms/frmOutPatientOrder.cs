using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    /// <summary>
    /// ����ҽ��վ������
    /// </summary>
    public partial class frmOutPatientOrder : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmOutPatientOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.iControlable = this.ucOutPatientOrder1 as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOutPatientOrder1;
                this.panelToolBar.Visible = false;
                this.FormClosing += new FormClosingEventHandler(frmOutPatientOrder_FormClosing);

                //this.FontSize = 28;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        void frmOutPatientOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Classes.LogManager.Write("����ʼ�˳����桿");
            this.ucOutPatientOrder1.QuitPass();
            this.ucOutPatientOrder1.ReleaseLisInterface();
            this.ucOutPatientOrder1.ReleasePacsInterface();

            Classes.LogManager.Write("���ɹ��˳����桿");
            return;
        }

        #region ����

        /// <summary>
        /// �Ƿ�����˻����̣�
        /// </summary>
        private bool isAccountMode = false;

        /// <summary>
        /// �����˻������Ƿ�ʹ���ն˿۷����� 1�ն��շ� 0�����շ�
        /// </summary>
        private bool isAccountTerimal = false;

        /// <summary>
        /// ��Ⱦ���ϱ���
        /// </summary>
        private FS.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// ����ҽ��������չ��ťʵ�ֽӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule IOrderExtendModule = null;

        /// <summary>
        /// ����ҽ��վѡ�������Ŀ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL IOrderChooseUl = null;

        FS.HISFC.BizProcess.Interface.Common.ILis IResultPrint = null;
        /// <summary>
        /// �������б�
        /// </summary>
        FS.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// �Ƿ�༭����
        /// </summary>
        //bool isEditGroup = false;

        /// <summary>
        /// ������Һ�֮�����Ч����
        /// </summary>
        private decimal validDays = 1;

        /// <summary>
        /// �Ƿ����õ������뵥����
        /// </summary>
        private bool isPacsApplyEnable = false;

        /// <summary>
        /// �Ƿ���������ҽ��վ��¼סԺ����
        /// </summary>
        private bool isHosDeptCanLogin = false;

        /// <summary>
        /// ���������ﻼ��˫�������ҽ���Ƿ���ʾ
        /// </summary>
        private bool isDoubleClickedShowTips = false;

        /// <summary>
        /// �Ƿ�༭����
        /// </summary>
        //public bool IsEditGroup
        //{
        //    get
        //    {
        //        return isEditGroup;
        //    }
        //    set
        //    {
        //        isEditGroup = value;
        //        if (this.tvGroup != null)
        //        {
        //            this.tvGroup.IsEditGroup = value;
        //        }

        //        operMode = EnumOperMode.Group;
        //    }
        //}

        public ArrayList Diagnoses = null;

        /// <summary>
        /// �Ƿ��״�¼����ϣ���һ����Ҫ��������б�
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// �Ƿ��״�¼��������Ӳ���
        /// </summary>
        private bool isCaseFirst = true;

        /// <summary>
        /// ���ÿ�������
        /// </summary>
        FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe ucOutPatientRecipe1 = null;

        /// <summary>
        /// ����ҽ���˵�
        /// </summary>
        Dictionary<string,FS.HISFC.Models.Base.Const> dicOutMenu = null;

        #endregion

        #region �¼�

        private void frmOutPatientOrder_Load(object sender, EventArgs e)
        {
            try
            {
                Classes.LogManager.Write("����ʼ������ҽ��from���桿");
                this.WindowState = FormWindowState.Maximized;

                this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

                this.InitMenu();
                OperMode = EnumOperMode.Query;

                //this.panelTree.Height = this.Height - 162;
                //this.panelTree.Visible = false;

                this.ucOutPatientTree1.TreeDoubleClick += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.TreeDoubleClickHandler(ucOutPatientTree1_TreeDoubleClick);

                this.isAccountTerimal = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, false, "0"));
                this.isAccountMode = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("MZ0300", false, "0"));
                //this.isAccountTerimal = controlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, false, false);
                //this.isAccountMode = controlIntegrate.GetControlParam<bool>("MZ0300", false, false);

                this.validDays = FS.FrameWork.Function.NConvert.ToDecimal(Classes.Function.GetBatchControlParam("200022", false, "0"));

                this.isDoubleClickedShowTips = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ53", false, "0"));

                this.ucOutPatientOrder1.IsAccountTerimal = this.isAccountTerimal;
                this.ucOutPatientOrder1.IsAccountMode = this.isAccountMode;
                this.ucOutPatientTree1.IsAccountMode = this.isAccountMode;
                this.ucOutPatientOrder1.OnRefreshGroupTree += new EventHandler(ucOutPatientOrder1_OnRefreshGroupTree);

                foreach (TabPage tb in this.tabControl1.TabPages)
                {
                    tb.Text = FS.FrameWork.Management.Language.Msg(tb.Text);
                }

                Application.DoEvents();

                #region ��ʼ����Ⱦ���ϱ��ӿ�

                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    this.dcpInstance.LoadNotice(this, FS.HISFC.Models.Base.ServiceTypes.C);
                }
                #endregion

                #region ��ʼ����չ��ť�ӿ�

                if (this.IOrderExtendModule == null)
                {
                    this.IOrderExtendModule = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule;
                }

                #endregion

                #region ��ʼ��ѡ�������Ŀ�ӿ�
                if (this.IOrderChooseUl == null)
                {
                    this.IOrderChooseUl = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL;
                }
                #endregion

                #region ��ȡ��Ա�Զ������

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(CacheManager.LogEmpl.ID);

                if (setting == null)
                {
                    //MessageBoxShow(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.tbPass.Checked = FS.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                    this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
                #endregion

                #region  add by lijp 2011-11-25 �������뵥��� {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

                this.tblEditPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S���뵥);

                try
                {
                    isPacsApplyEnable = CacheManager.ContrlManager.GetControlParam<bool>("200212");
                }
                catch
                {
                    isPacsApplyEnable = false;
                }

                #endregion

                #region ��ʼ�����ô���
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("����"))
                    {
                        FS.FrameWork.Models.NeuObject neuObject = CacheManager.ConManager.GetConstant("DeptItem", CacheManager.LogEmpl.Dept.ID);

                        if (object.Equals(neuObject, null) || object.Equals(neuObject.ID, ""))
                        {
                            tabControl1.TabPages.Remove(tabPage);
                        }
                        else
                        {
                            tabControl1.SelectedTab = tabPage;
                            if (tabPage.Controls.Count > 0
                                && tabPage.Controls[0].GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                            {
                                ucOutPatientRecipe1 = tabPage.Controls[0] as FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe;
                            }
                            tabControl1.SelectedIndex = 0;
                        }
                    }
                }


                #endregion

                this.tbPass.CheckedChanged += new EventHandler(tbPass_CheckedChanged);

                Classes.LogManager.Write("��������ʼ������ҽ��from���桿");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void tbPass_CheckedChanged(object sender, EventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;


            FS.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();
            FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

            if (setting == null)
            {
                MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (userSetmgr.Update(setting) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        void ucOutPatientOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        /// <summary>
        /// ˫���������ڵ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isDoubleClick">�Ƿ�˫�����ڵ�</param>
        private void ucOutPatientTree1_TreeDoubleClick(object sender, HISFC.Components.Order.OutPatient.Controls.ClickEventArgs e, bool isDoubleClick)
        {
            try
            {
                if (this.ucOutPatientTree1.neuTreeView1.Visible)
                {
                    this.tree = this.ucOutPatientTree1.neuTreeView1;
                    TreeViewEventArgs mye = new TreeViewEventArgs(this.ucOutPatientTree1.neuTreeView1.SelectedNode);

                    this.tree_AfterSelect(e.Message, mye);
                    if (this.CurrentControl.Name.Equals(this.ucOutPatientOrder1.Name) == false)
                    {
                        this.ucOutPatientOrder1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #region add by liuww ���ô��� 2013-05-13
                    if (!object.Equals(null, this.ucOutPatientRecipe1))
                    {
                        this.ucOutPatientRecipe1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #endregion

                    this.Tag = this.ucOutPatientTree1.neuTreeView1.SelectedNode.Tag;
                }

                if (this.ucOutPatientTree1.neuTreeView2.Visible)
                {
                    this.tree = this.ucOutPatientTree1.neuTreeView2;
                    TreeViewEventArgs mye = new TreeViewEventArgs(this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    this.tree_AfterSelect(e.Message, mye);

                    if (this.CurrentControl.Name.Equals(this.ucOutPatientOrder1.Name) == false)
                    {
                        this.ucOutPatientOrder1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    }
                    #region add by liuww ���ô��� 2013-05-13
                    if (!object.Equals(null, this.ucOutPatientRecipe1))
                    {
                        this.ucOutPatientRecipe1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #endregion

                    this.Tag = this.ucOutPatientTree1.neuTreeView2.SelectedNode.Tag;
                }

                if (this.Tag is FS.HISFC.Models.Registration.Register)
                {
                    FS.HISFC.Models.Registration.Register register = (FS.HISFC.Models.Registration.Register)Tag;
                    this.Text = "�����ţ�" + register.PID.CardNO.PadRight(12) + "������" + register.Name.PadRight(8) + "�Ա�" + register.Sex.Name.PadRight(4) + "�绰��" + register.PhoneHome + "סַ��" + register.AddressHome;
                }

                if (this.ucOutPatientTree1.neuTreeView2.SelectedNode != null
                    && this.ucOutPatientTree1.neuTreeView2.SelectedNode.Nodes.Count > 0
                    && isDoubleClick && isDoubleClickedShowTips)
                {
                    //����ҽ��,����ʾ�Ƿ��¿���ҽ��
                    DialogResult dr = MessageBox.Show("�Ѵ���ҽ��,�Ƿ��¿���?", "��ʾ", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        AddOrder(false);
                    }

                }
                else
                {
                    if (isDoubleClick)
                    {
                        AddOrder(false);
                    }
                }

                return;

                if (isAccountMode)
                {
                    #region �˴����ٿ۷ѣ�ҽ������ʱ���շ�
                    if (this.Tag is FS.HISFC.Models.Registration.Register)
                    {
                        //�ж��˻����̵ĹҺ��շ����
                        bool isAccount = false;
                        decimal vacancy = 0m;
                        FS.HISFC.Models.Registration.Register r = (FS.HISFC.Models.Registration.Register)Tag;

                        if (isAccountTerimal && r.IsAccount)
                        {

                            if (CacheManager.FeeIntegrate.GetAccountVacancy(r.PID.CardNO, ref vacancy) <= 0)
                            {
                                MessageBox.Show(CacheManager.FeeIntegrate.Err);
                                return;
                            }
                            isAccount = true;
                        }
                        if (isAccount && r.IsFee == false)
                        {
                            #region �˻���ȡ�Һŷ�

                            if (!CacheManager.FeeIntegrate.CheckAccountPassWord(r))
                            {
                                this.ucOutPatientTree1.neuTreeView1.SelectedNode = null;
                                this.ucOutPatientTree1.PatientInfo = null;
                                return;
                            }

                            if (isAccount && !r.IsFee)
                            {
                                if (vacancy < r.OwnCost)
                                {
                                    MessageBox.Show("�˻����㣬�뽻�ѣ�");
                                    return;
                                }

                                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                if (CacheManager.FeeIntegrate.AccountPay(r, r.OwnCost, "�Һ��շ�", CacheManager.LogEmpl.Dept.ID, string.Empty) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���˻����ʧ�ܣ�" + CacheManager.FeeIntegrate.Err);
                                    return;
                                }
                                r.SeeDoct.ID = CacheManager.LogEmpl.ID;
                                r.SeeDoct.Dept.ID = CacheManager.LogEmpl.ID;

                                FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
                                if (CacheManager.RegInterMgr.UpdateAccountFeeState(r.ID, r.SeeDoct.ID, r.SeeDoct.Dept.ID, funMgr.GetDateTimeFromSysDateTime()) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���¹Һű����շ�״̬����\r\n" + CacheManager.RegInterMgr.Err);
                                    return;
                                }
                                FS.FrameWork.Management.PublicTrans.Commit();
                                r.IsFee = true;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            catch { }
            finally { }
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOutPatientOrder1.AddGroupOrder(alOrders);
        }


        /// <summary>
        /// ���ݴ�ӡ��ťʵ�֣����ơ�������ָ�������飩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmPrint_Click(object sender, EventArgs e)
        {
            bool IsPreview = false;

            if (!((ToolStripMenuItem)sender).Tag.Equals(null))
            {
                IsPreview = FS.FrameWork.Function.NConvert.ToBoolean(((ToolStripMenuItem)sender).Tag);

            }
            this.ucOutPatientOrder1.PrintAllBill(((ToolStripMenuItem)sender).Name, IsPreview);


            if (((ToolStripMenuItem)sender).Name.Equals("0") || ((ToolStripMenuItem)sender).Name.Equals("9"))
            {
                if (!object.Equals(ucOutPatientRecipe1, null))
                {
                    this.ucOutPatientRecipe1.PrintAgain(((ToolStripMenuItem)sender).Name, IsPreview);
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
            //if (object.Equals(this.Tag, null))
            //{
            //    MessageBox.Show("����ѡ����Ҫ�鿴�Ļ��ߣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //if (IResultPrint == null)
            //{

            //    IResultPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Forms.frmOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;

            //}

            //if (!((ToolStripMenuItem)sender).Name.Equals(null))
            //{
            //    IResultPrint.ResultType = ((ToolStripMenuItem)sender).Name.ToString();
            //    IResultPrint.SetPatient(this.Tag as FS.HISFC.Models.RADT.Patient);
            //    IResultPrint.ShowResultByPatient();

            //}

            FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType resultType = (FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType)FS.FrameWork.Function.NConvert.ToInt32(((ToolStripMenuItem)sender).Name);

            ucOutPatientOrder1.QueryMedicalResult(resultType);
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

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private EnumOperMode operMode = EnumOperMode.Query;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private EnumOperMode OperMode
        {
            set
            {
                operMode = value;
                //if (operMode == EnumOperMode.Group)
                //{
                //    this.isEditGroup = true;
                //}
                this.SetButton();
            }
        }

        /// <summary>
        /// ����ģʽ
        /// </summary>
        private enum EnumOperMode
        {
            /// <summary>
            /// ����
            /// </summary>
            Add,

            /// <summary>
            /// ����
            /// </summary>
            Group,

            /// <summary>
            /// ��ѯ����
            /// </summary>
            Query
        }

        /// <summary>
        /// ���ò˵���ʾ
        /// </summary>
        /// <param name="isDisign"></param>
        private void SetButton()
        {
            #region ���ò˵��Ŀɼ���

            //����
            this.tbGroup.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbGroup.Name);
            //��ӡ
            this.tbPrintOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintOrder.Name);
            //ԤԼ��Ժ
            this.tbOutPreIn.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOutPreIn.Name);
            //����
            this.tbPrintAgain.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintAgain.Name);
            //��ӡԤ��
            this.tbPrintOrderPreview.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintOrderPreview.Name);
            //��������
            this.tbAddOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbAddOrder.Name);
            //�¿���
            this.tbAddNewOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbAddNewOrder.Name);

            //��չ1 ֻ���ڿ���ģʽ��ʹ��
            tbExtend1.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbExtend1.Name);
            //��չ2 �κ�ʱ�򶼿���ʹ��
            tbExtend2.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbExtend2.Name);
            //��չ3 ֻ���ڷǿ���ģʽ��ʹ��
            tbExtend3.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbExtend3.Name);
            //������Ŀ����
            tbChooseUL.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbChooseUL.Name);
            //���б�
            this.tbPatientTree.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPatientTree.Name);
            //���
            this.tbComboOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbComboOrder.Name);
            //ȡ�����
            this.tbCancelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbCancelOrder.Name);
            //���㸽��
            this.tbCalculatSubl.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbCalculatSubl.Name);
            //��Ӽ�顢����ҽ��
            this.tbCheck.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbCheck.Name);
            //��ҩ����
            this.tbHerbal.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbHerbal.Name);
            //�㼶ҽ��
            this.tbLevelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbLevelOrder.Name);
            //��������
            this.tbOperation.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOperation.Name);
            //ɾ�����
            this.tbDelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbDelOrder.Name);
            //ɾ����������
            this.tbDeleteOne.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbDeleteOne.Name);
            //�˳�ҽ��
            this.tbExitOrder.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbExitOrder.Name);
            //����
            this.tbFilter.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbFilter.Name);
            //��ѯ����
            this.tbQueryOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbQueryOrder.Name);
            //�༭���뵥
            this.tblEditPacsApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tblEditPacsApply.Name);
            //���洦��
            this.tbSaveOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbSaveOrder.Name);
            //���
            this.tbDiagOut.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDiagOut.Name);
            //ȡ������
            tbBackNoSee.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbBackNoSee.Name);
            //LIS���뵥��ӡ
            this.tbLisApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbLisApply.Name);
            //PACS���뵥��ӡ
            this.tbPacsApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPacsApply.Name);
            //LIS����鿴
            this.tbLisResult.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbLisResult.Name);
            //PACS����鿴
            this.tbPacsResult.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbPacsResult.Name);
            //�к�
            this.tbCall.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbCall.Name);
            //�ӳٽк�
            tbDelayCall.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDelayCall.Name);
            //�˳�����
            tbClose.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbClose.Name);
            //����
            tbRegEmerPatient.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbRegEmerPatient.Name);
            //����
            tbOutEmerPatient.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOutEmerPatient.Name);
            //תסԺ
            this.tbInEmerPatient.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbInEmerPatient.Name);

            //��Ⱦ������
            tbDiseaseReport.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDiseaseReport.Name);
            //ҽ��ԤԼ
            tbMedTechOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbMedTechOrder.Name);
            //Ժ���ϱ�
            tbInfectionReport.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbInfectionReport.Name);

            //������ҩ
            tbPass.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPass.Name);
            //ѡ��ʱ�� �鿴��ʷ�����б�
            tbChooseTime.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbChooseTime.Name);

            //ҽ�ƽ����Ŀǰ�����е�ҽ�ƽ��������һ��鿴��
            tbResultPrint.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbResultPrint.Name);
            //��Ⱦ������
            this.tbSwipCard.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbSwipCard.Name);
            // {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            #endregion

            #region �������ס������б�Ŀɼ���

            if (operMode == EnumOperMode.Query)
            {
                pnPatientList.Visible = true;
                panelTree.Visible = false;
                //������ʲô����֪��...
                panel2.Visible = false;
            }
            else if (operMode == EnumOperMode.Add
                || operMode == EnumOperMode.Group)
            {
                pnPatientList.Visible = false;
                panelTree.Visible = true;
                //������ʲô����֪��...
                panel2.Visible = true;

                if (!(CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe)
                    || CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder)))
                {
                    this.panel2.Visible = false;
                }
            }

            if (operMode == EnumOperMode.Add
                || operMode == EnumOperMode.Group)
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
                    //tvGroup.IsEditGroup = this.isEditGroup;
                    tvGroup.IsEditGroup = operMode == EnumOperMode.Group ? true : false;

                    tvGroup.Init();
                    tvGroup.SelectOrder -= new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);

                    this.panelTree.Controls.Add(tvGroup);
                    tvGroup.Dock = DockStyle.Fill;
                }
            }

            #endregion


            //�������˵��

            //if (operMode == EnumOperMode.Add) //����
            //{
            //    if (tvGroup == null)
            //    {
            //        tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
            //        tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
            //        tvGroup.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
            //        tvGroup.IsEditGroup = this.isEditGroup;
            //        tvGroup.Init();
            //        tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
            //    }
            //    tvGroup.Dock = DockStyle.Fill;
            //    tvGroup.Visible = true;

            //    this.panelTree.Visible = true;
            //    this.panel2.Visible = true;
            //    if (this.btnShow.Visible != true)
            //    {
            //        this.panel2.Width = 250;
            //        this.panelTree.Width = 250;
            //    }
            //    this.panelTree.Controls.Add(tvGroup);
            //    this.pnPatientList.Visible = false;
            //}
            //else
            //{
            //    this.pnPatientList.Visible = true;
            //    this.panelTree.Visible = false;
            //    this.panel2.Visible = false;
            //    if (tvGroup != null)
            //    {
            //        tvGroup.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// ��ʼ���˵����� �����Զ���˵������ÿɼ���
        /// </summary>
        private void InitMenu()
        {
            #region ���ò˵���ť

            this.tbAddReg.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T���);

            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            this.tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�ײ�);


            //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}
            this.tbAppoint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.YԤԼ);

            this.tbMessage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ);


            this.tbAddOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��);
            this.tbAddNewOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X�½�);
            this.tbMedTechOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.YԤԼ);
            this.tbInfectionReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.S�ֶ�¼��);
            //������չ��ť
            this.tbExtend1.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);
            this.tbExtend2.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);
            this.tbExtend3.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);

            this.tbChooseUL.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);

            this.tbComboOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�);
            this.tbCancelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbDelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��);

            this.tbDeleteOne.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��);

            this.tbOperation.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z���);
            this.tbSaveOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
            this.tbCheck.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tbClose.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbExitOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbGroup.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z����);

            this.tbDiagOut.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��);
            tbBackNoSee.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);

            this.tbRefreshPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��);
            this.tbQueryOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);
            this.tbPatientTree.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G�˿�);

            this.tbPrintOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbOutPreIn.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);
            this.tbPrintAgain.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbPrintOrderPreview.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��);

            this.tbRegEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J����);
            this.tbOutEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��Ժ�Ǽ�);
            this.tbInEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Zת��);
            this.tbDiseaseReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J��������);
            this.tbHerbal.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ҩ);
            //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} �㼶��ʽ����ҽ�� yangw 20101024
            this.tbLevelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z�Ӽ�);
            this.tbLisResult.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tbPacsResult.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S�豸);

            this.tbLisApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S���뵥);
            this.tbPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S���뵥);
            this.tbPass.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
            this.tbCalculatSubl.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�);

            this.tbCall.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��);

            this.tbDelayCall.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��);

            this.tbChooseTime.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.L��ʷ��Ϣ);

            this.tbResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tbSwipCard.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����);
            // {DD27333B-4CBF-4bb2-845D-8D28D616937E}


            this.tbOutPreIn.TextImageRelation = TextImageRelation.ImageAboveText;// {6BF1F99D-7307-4d05-B747-274D24174895}
            #endregion

            ArrayList alMenu = CacheManager.GetConList("OUTPATMENU");
            //foreach (FS.HISFC.Models.Base.Const con in alMenu)
            //{
            //    if (con.IsValid && !dicOutMenu.ContainsKey(con.ID))
            //    {
            //        dicOutMenu.Add(con.ID, con);
            //    }
            //}

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
                }
            }

            this.toolStrip1.Items.Clear();
            dicOutMenu = new Dictionary<string, FS.HISFC.Models.Base.Const>();
            foreach (FS.HISFC.Models.Base.Const conObj in alMenu)
            {
                if (!conObj.IsValid)
                {
                    continue;
                }

                if (!dicOutMenu.ContainsKey(conObj.ID))
                {
                    dicOutMenu.Add(conObj.ID, conObj);
                }

                if (hsTb.Contains(conObj.ID.Trim()))
                {
                    if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripButton))
                    {
                        tButton = new ToolStripButton();
                        tButton = hsTb[conObj.ID.Trim()] as ToolStripButton;

                        tButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(conObj.Memo))
                        {
                            tButton.Text = tButton.Text + "(&" + conObj.Memo + ")";
                        }
                        tButton.Visible = true;
                        this.toolStrip1.Items.Add(tButton);
                    }
                    else if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripDropDownButton))
                    {
                        tdButton = new ToolStripDropDownButton();
                        tdButton = hsTb[conObj.ID.Trim()] as ToolStripDropDownButton;

                        tdButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(conObj.Memo))
                        {
                            tdButton.Text = tdButton.Text + "(&" + conObj.Memo + ")";
                        }
                        tdButton.Visible = true;

                        #region �Զ���������ť
                        //�Զ���������ť1    OUTPATPRINT
                        if (!string.IsNullOrEmpty(conObj.UserCode) 
                            && (string.Equals(conObj.UserCode, "OUTPATPRINT") 
                            || string.Equals(conObj.UserCode, "OUTPATPREVIEW")))
                        {
                            ArrayList dropItemList = CacheManager.GetConList(conObj.UserCode);
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
                                        drop.Click += new System.EventHandler(this.tmPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }

                        //�Զ���������ť2
                        if (!string.IsNullOrEmpty(conObj.UserCode) 
                            && string.Equals(conObj.UserCode, "ResultPrint"))
                        {
                            ArrayList dropItemList = CacheManager.GetConList(conObj.UserCode);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //����PatienId��ˮ��{17537415-C168-450d-BBCC-93CFFA19DB82}
                #region ���¼�����
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    if (isFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isFirst = false;
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("��û��ѡ���ߣ�");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).RegInfo = ucOutPatientTree1.PatientInfo;
                    }
                }
                #endregion

                #region ���ﲡ������

                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase))
                {
                    if (isCaseFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).transportAlDiag -= new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).transportAlDiag += new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isCaseFirst = false;
                    }

                    //�Լ�¼����ϣ�����Ҫ���洫����
                    //(this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).diagnoses = this.Diagnoses;
                    //if (this.Diagnoses != null)
                    //{
                    //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SetDiagNose();
                    //}

                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("��û��ѡ���ߣ�");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).RegObj = ucOutPatientTree1.PatientInfo;
                    }

                    //this.neuPanel1.Visible = false;
                }

                #endregion
                #region ��ʷҽ������
                //����л�tabҳʱ��û�е�����б��еĻ��ߣ���ʷҽ����ˢ�� houwb {B9D1936C-4564-4e35-A158-40E8688267FF}
                else if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Patient = this.ucOutPatientTree1.PatientInfo;
                }
                #endregion

                #region ���ô�������
                else if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    //if (ucOutPatientTree1.PatientInfo == null)
                    //{
                    //    MessageBox.Show("��û��ѡ���ߣ�");
                    //}
                    //else
                    //{
                    //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe).Patient = ucOutPatientTree1.PatientInfo;
                    //}
                }
                #endregion

                #region ��������
                else
                {
                    if (this.ucOutPatientTree1.neuTreeView1.Visible)
                    {
                        (this.CurrentControl as FS.FrameWork.WinForms.Controls.ucBaseControl).SetValue(this.ucOutPatientTree1.PatientInfo, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    else
                    {
                        (this.CurrentControl as FS.FrameWork.WinForms.Controls.ucBaseControl).SetValue(this.ucOutPatientTree1.PatientInfo, this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    }
                }
                #endregion

                this.SetButton();
            }
        }

        /// <summary>
        /// ��ϱ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOutPatientOrder_SaveClicked(object sender, ArrayList e)
        {
            if (e != null)
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    string msg = "";
                    Hashtable hs = new Hashtable();
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in e)
                    {
                        if (!hs.ContainsKey(diag.DiagInfo.ICD10.Name))
                        {
                            hs.Add(diag.DiagInfo.ICD10.Name, "");
                            this.dcpInstance.CheckDiseaseReport(this, this.ucOutPatientOrder1.Patient, FS.HISFC.Models.Base.ServiceTypes.C, diag.DiagInfo.ICD10.Name, out msg);
                        }
                    }
                }
            }


            if (!this.ucOutPatientOrder1.IsDesignMode)
            {
                return;
            }


            //����������ʾ��Ͻ���
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text.Contains("ҽ��"))
                {
                    tabControl1.SelectedTab = tabPage;
                    break;
                }
            }

            if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
                //{0BEB97B8-373D-45d0-A186-12502DD0AADE}
                if (this.ucOutPatientOrder1.Patient.ID.Trim() != this.ucOutPatientTree1.PatientInfo.ID.Trim())
                {
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }

                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                    return;
                }

                this.statusBar1.Panels[1].Text = "(��ɫ���¿�)(��ɫ���շ�)(��ɫ������)";

                //���δ����ģ�����ʱ�Զ�����{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
                //if (!this.ucOutPatientOrder1.Patient.IsSee)
                //{
                //    this.ucOutPatientTree1.AutoTriage();
                //}
                //if (this.ucOutPatientOrder1.Add() == 0)
                //    this.initButton(true);

                this.ucOutPatientOrder1.SetInPutFocos();
            }
        }

        /// <summary>
        /// ���ڴ�Ⱦ���ϱ�
        /// </summary>
        /// <param name="arrayDiagnoses"></param>
        void frmOutPatientOrder_transportAlDiag(ArrayList arrayDiagnoses)
        {
            if (arrayDiagnoses.Count > 0)
            {
                this.Diagnoses = arrayDiagnoses;
            }
            return;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isNew">�Ƿ�ǿ���¿���</param>
        private void AddOrder(bool isNew)
        {
            if (this.ucOutPatientOrder1.Patient == null)
            {
                MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                return;
            }
            //if (ucOutPatientOrder1.Patient.PID.CardNO.StartsWith("9")
            //    && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            //{
            //    MessageBox.Show("��ǰ��������ʱ�շѻ��ߣ����ܿ�������ʹ���ֹ���������");
            //    return;
            //}

            #region ��Чʱ���ڿ���ҽ��

            if (!FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                DateTime dtSys = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime(); //���ݿ�ϵͳʱ��
                DateTime dtSee = this.ucOutPatientOrder1.Patient.DoctorInfo.SeeDate;//����ʱ��
                TimeSpan span = dtSys.Subtract(dtSee);  //ʱ���

                bool isEmergency = this.ucOutPatientOrder1.Patient.DoctorInfo.Templet.RegLevel.IsEmergency; //����

                if (isEmergency && span.Days > validDays)
                {
                    MessageBox.Show("���ﻼ�ߣ��ѳ�����Чʱ�䣺" + validDays + " * 24Сʱ�����ܿ���ҽ����");
                    return;
                }
                if (!isEmergency && span.Days > this.validDays)
                {
                    MessageBox.Show("�ѳ�����Чʱ�䣺" + validDays + "�죬���ܿ���ҽ����");
                    return;
                }
            }
            #endregion

            #region �շѴ����������ٴο���

            if (!isNew)
            {
                if (!ucOutPatientOrder1.CheckCanAdd())
                {
                    return;
                }
            }

            #endregion

            this.statusBar1.Panels[1].Text = "(��ɫ���¿�)(��ɫ���շ�)(��ɫ������)";

            //���δ����ģ�����ʱ�Զ�����{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
            if (!this.ucOutPatientOrder1.Patient.IsSee)
            {
                this.ucOutPatientTree1.AutoTriage();
            }

            if (this.ucOutPatientOrder1.Add(isNew) == 0)
            {
                OperMode = EnumOperMode.Add;
            }
            else
            {
                return;
            }

            if (!this.ucOutPatientOrder1.isHaveDiag())
            {
                //����������ʾ��Ͻ���
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("���ﲡ��"))
                    {
                        tabControl1.SelectedTab = tabPage;
                        break;
                    }
                }
            }
            else
            {
                //{0FF7F0D0-E31A-474b-A2F6-DB1E2CF843C6}
                //����������ʾ��Ͻ���
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("ҽ��"))
                    {
                        tabControl1.SelectedTab = tabPage;
                        break;
                    }
                }
            }

            #region �������ô���

            this.AddOrderOutsource(isNew);

            #endregion
        }

        /// <summary>
        /// ���ô�������
        /// </summary>
        /// <param name="isNew"></param>
        private void AddOrderOutsource(bool isNew)
        {
            if (this.tabControl1.SelectedTab.Text.Contains("����"))
            {
                if (this.ucOutPatientRecipe1.Add(isNew) == 0)
                {
                    OperMode = EnumOperMode.Add;
                }
            }
            //if (!object.Equals(ucOutPatientRecipe1, null))
            //{
            //    if (this.ucOutPatientRecipe1.Add(isNew) == 0)
            //    {
            //        this.SetButton(true);
            //    }
            //}
        }

        /// <summary>
        /// �����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            #region ����
            if (e.ClickedItem == this.tbAddOrder)//����
            {
                //{82FC5ABE-B85B-4011-AE0F-8042A89CD327}
                //if (this.ucOutPatientOrder1.Patient == null)
                //{
                //    MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                //    return;
                //}
                //if (this.ucOutPatientOrder1.DiagIn() < 0)
                //{
                //    MessageBox.Show("����ʧ�ܣ�");
                //    return;
                //}
                AddOrder(false);
            }
            #endregion

            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            #region �鿴�ײ�
            else if (e.ClickedItem == this.tbPackage)
            {
                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("���ȼ������ߣ�");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
               // FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                
                //frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                //frmpackage.detailVisible = true;
                //frmpackage.ShowDialog();

                FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                frmpackage.DetailVisible = true;//{187A73EB-008A-4A25-A6CB-28CAE0E629A7}����ҽ��վ�鿴�ײ���ϸ
                frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                frmpackage.ShowDialog();
            }
            #endregion

            //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}

            #region ���ԤԼ
            else if (e.ClickedItem == this.tbAppoint)
            {
                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {

                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                // FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();

                //frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                //frmpackage.detailVisible = true;
                //frmpackage.ShowDialog();

                FS.HISFC.Components.Order.OutPatient.Forms.frmAppointment frmappoint = new FS.HISFC.Components.Order.OutPatient.Forms.frmAppointment();

                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {
                    frmappoint.ShowDialog();
                }
                else
                {
                    frmappoint.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);

                    frmappoint.ShowDialog();
                }


                //Form form = new Form();
                //FS.HISFC.Components.Operation.ucAppointNew uc = new FS.HISFC.Components.Operation.ucAppointNew();
                //form.Size = uc.Size;
                //form.Controls.Add(uc);
                //form.StartPosition = FormStartPosition.CenterScreen;
                //form.ShowDialog();
                //MessageBox.Show("���سɹ�1111222����");
            }
            #endregion

            //{93020DA1-B5F5-4a29-BBD7-D9BE76E4919A}

            #region ���ŷ���
            else if (e.ClickedItem == this.tbMessage)
            {
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

                FS.HISFC.Models.RADT.PatientInfo selectedPatient = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);//.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
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

            #region �¿���
            else if (e.ClickedItem == this.tbAddNewOrder)//����
            {
                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                    return;
                }
                //{82FC5ABE-B85B-4011-AE0F-8042A89CD327}
                //if (this.ucOutPatientOrder1.DiagIn() < 0)
                //{
                //    MessageBox.Show("����ʧ�ܣ�");
                //    return;
                //}
                AddOrder(true);
            }
            #endregion

            #region ҽ��ԤԼ
            else if (e.ClickedItem == this.tbMedTechOrder)//����
            {
                MTOrder.frmMTApply newfrm = new MTOrder.frmMTApply();
                newfrm.PatientInfo = this.Tag as FS.HISFC.Models.Registration.Register;
                newfrm.ShowDialog();
            }
            #endregion

            #region Ժ���ϱ�
            else if (e.ClickedItem == this.tbInfectionReport)//Ժ���ϱ�
            {
                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("����ѡ����Ҫ�ϱ��Ļ��ߣ�");
                    return;
                }
                this.ucOutPatientOrder1.Patient.Memo = "2";//�����ʶ
                tbInfectionReport.Tag = this.ucOutPatientOrder1.Patient;
                this.tbInfectionReport_Click(this.tbInfectionReport);
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbGroup)//����
            {
                if (this.tbGroup.CheckState == CheckState.Checked)
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                    OperMode = EnumOperMode.Query;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                    OperMode = EnumOperMode.Group;

                    this.ucOutPatientOrder1.SetEditGroup(true);
                    this.ucOutPatientOrder1.Patient = null;
                }
            }
            #endregion

            #region ��ҩ����
            else if (e.ClickedItem == this.tbHerbal)
            {
                this.ucOutPatientOrder1.HerbalOrder();
            }
            #endregion

            #region �㼶��ʽ����ҽ��
            else if (e.ClickedItem == this.tbLevelOrder)
            {
                //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} �㼶��ʽ����ҽ�� yangw 20101024
                this.ucOutPatientOrder1.AddLevelOrders();
            }
            #endregion

            #region ��Ӽ�顢����ҽ��

            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOutPatientOrder1.AddTest();
            }
            #endregion

            #region ɾ��
            else if (e.ClickedItem == this.tbDelOrder)
            {
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
                {
                    this.ucOutPatientOrder1.Del();
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    this.ucOutPatientRecipe1.Del();
                }
            }
            #endregion

            #region ɾ��
            else if (e.ClickedItem == this.tbDeleteOne)
            {
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
                {
                    this.ucOutPatientOrder1.DeleteSingleOrder();
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    this.ucOutPatientRecipe1.DeleteSingleOrder();
                }
            }
            #endregion

            #region ��ѯ
            else if (e.ClickedItem == this.tbQueryOrder)//��ѯ
            {
                this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                this.ucOutPatientOrder1.Retrieve();
            }
            #endregion

            #region ��ӡ
            else if (e.ClickedItem == this.tbPrintOrder)//��ӡ
            {
                this.ucOutPatientOrder1.PrintRecipe();
            }
            #endregion



            #region ԤԼ��Ժ
            else if (e.ClickedItem == this.tbOutPreIn)// {6BF1F99D-7307-4d05-B747-274D24174895}
            {
                this.ucOutPatientOrder1.PrePayIn();
            }
            #endregion
            /*
            #region ����
            else if (e.ClickedItem == this.tbPrintAgain)
            {
                this.ucOutPatientOrder1.PrintAgainAll();
            }
            #endregion
            */

            #region ���
            else if (e.ClickedItem == this.tbComboOrder)//���
            {
                this.ucOutPatientOrder1.ComboOrder();
            }
            #endregion

            #region ȡ�����
            else if (e.ClickedItem == this.tbCancelOrder)//ȡ�����
            {
                this.ucOutPatientOrder1.CancelCombo();
            }
            #endregion

            #region �˳�ҽ��
            else if (e.ClickedItem == this.tbExitOrder)//�˳�ҽ��
            {
                if (operMode == EnumOperMode.Group)
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    //this.initButtonGroup(false);
                    OperMode = EnumOperMode.Query;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }
                else
                {
                    if (this.ucOutPatientOrder1.ExitOrder() == 0)
                    {
                        OperMode = EnumOperMode.Query;

                        //�˳�ʱ�ж�ȡ��������
                        this.ucOutPatientTree1.CancelTriage();
                    }
                    if (!object.Equals(this.ucOutPatientRecipe1, null))
                    {
                        if (this.ucOutPatientRecipe1.ExitOrder() == 0)
                        {
                        }
                    }
                }
            }
            #endregion

            #region ����

            else if (e.ClickedItem == this.tbRegEmerPatient)//����
            {
                if (this.ucOutPatientOrder1.RegisterEmergencyPatient() < 0)
                {
                }
                else
                {
                    //MessageBox.Show("���۳ɹ���");

                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbOutEmerPatient) //����
            {
                if (this.ucOutPatientOrder1.OutEmergencyPatient() > 0)
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region ����תסԺ

            else if (e.ClickedItem == this.tbInEmerPatient) //תסԺ
            {
                if (this.ucOutPatientOrder1.InEmergencyPatient() > 0)
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    //ucOutPatientTree1.RefreshTreePatientDone();
                }
            }
            #endregion

            #region ���
            else if (e.ClickedItem == this.tbDiagOut)//���
            {
                if (this.ucOutPatientOrder1.DiagOut() < 0)
                {
                }
                else
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    //ucOutPatientTree1.RefreshTreePatientDone();
                }
            }
            #endregion

            #region ȡ������
            else if (e.ClickedItem == this.tbBackNoSee)//���
            {
                if (this.ucOutPatientOrder1.CanCelDiag() < 0)
                {
                }
                else
                {
                    MessageBox.Show(this.ucOutPatientOrder1.Patient.Name + " ȡ������ɹ���");

                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region ˢ����Ա�б�
            else if (e.ClickedItem == this.tbRefreshPatient)//ˢ��
            {
                ucOutPatientTree1.RefreshNoSeePatient();
                ucOutPatientTree1.RefreshSeePatient();
            }
            #endregion

            #region ��Ա�б�
            else if (e.ClickedItem == this.tbPatientTree)//�б�
            {
                this.pnPatientList.Visible = !this.pnPatientList.Visible;
            }
            #endregion

            #region ���洦��
            else if (e.ClickedItem == this.tbSaveOrder)//����
            {
                this.tbSaveOrder.Enabled = false;
                try
                {
                    if (operMode == EnumOperMode.Group)
                    {
                        SaveGroup();
                    }
                    else
                    {
                        if (this.tabControl1.SelectedTab.Text.Contains("����"))
                        {
                            Classes.LogManager.Write("��ʼ���沩�ô���!");

                            if (this.ucOutPatientRecipe1.Save() == -1)
                            {
                                return;
                            }
                            else
                            {
                                Classes.LogManager.Write("�������沩�ô���!");
                            }

                        }

                        Classes.LogManager.Write("��ʼ���洦��!");
                        if (this.ucOutPatientOrder1.Save() == -1)
                        {
                        }
                        else
                        {
                            Classes.LogManager.Write("�������洦��!");

                            OperMode = EnumOperMode.Query;

                            Classes.LogManager.Write("��ʼˢ�»����б�!");
                            ucOutPatientTree1.RefreshNoSeePatient();
                            //ucOutPatientTree1.RefreshTreePatientDone();
                            ucOutPatientTree1.FreshSeePatientNode();

                            Classes.LogManager.Write("����ˢ�»����б�!");
                        }



                    }
                    this.statusBar1.Panels[1].Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("frmOutPatientOrder" + ex.Message);
                }
                finally
                {
                    this.tbSaveOrder.Enabled = true;
                }
            }
            #endregion

            #region �˳�����
            else if (e.ClickedItem == this.tbClose)//�˳�
            {
                if (this.ucOutPatientOrder1 != null && this.ucOutPatientOrder1.IsDesignMode) //���ڿ���״̬
                {
                    DialogResult result = MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ��Ŀǰ���ڿ���ģʽ���Ƿ񱣴�?"), "��ʾ", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (this.ucOutPatientOrder1.Save() == 0)
                        {
                            //�˳�ʱ�ж�ȡ��������
                            this.ucOutPatientTree1.CancelTriage();
                            this.Close();
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        //�˳�ʱ�ж�ȡ��������
                        this.ucOutPatientTree1.CancelTriage();
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            #endregion

            #region ��Ⱦ�����濨
            else if (e.ClickedItem == this.tbDiseaseReport)     //  {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2} ��Ⱦ�����濨
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    FS.HISFC.Models.RADT.Patient patient = this.ucOutPatientTree1.PatientInfo as FS.HISFC.Models.RADT.Patient;

                    this.dcpInstance.RegisterDiseaseReport(this, patient, FS.HISFC.Models.Base.ServiceTypes.C);
                }
            }
            #endregion

            #region ˢ��
            else if (e.ClickedItem == this.tbSwipCard) // {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}  
            {
                string mCardNo = "";
                string error = "";

                if (Function.OperMCard(ref mCardNo, ref error) < 0)
                {
                    MessageBox.Show("����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���\n" + error);
                    return;
                }
                mCardNo = ";" + mCardNo;
                this.ucOutPatientTree1.QueryPatientInfo(mCardNo);
            }
            #endregion
            #region LIS�����ӡ
            //LIS�����ӡ
            else if (e.ClickedItem == this.tbLisResult)
            {
                this.ucOutPatientOrder1.QueryLisResult();
            }
            #endregion

            #region PACS�����ӡ
            else if (e.ClickedItem == this.tbPacsResult)
            {
                ucOutPatientOrder1.QueryPacsReport();
            }
            #endregion

            #region �Ŷӽк�
            else if (e.ClickedItem == this.tbCall)
            {
                this.ucOutPatientTree1.Call();
            }
            #endregion

            #region ����
            else if (e.ClickedItem == this.tbDelayCall)
            {
                this.ucOutPatientTree1.DelayCall();
            }
            #endregion

            #region ���뵥��ӡ

            //LIS���뵥��ӡ
            else if (e.ClickedItem == this.tbLisApply)
            {
                this.ucOutPatientOrder1.LisReportPrint();
            }
            //PACS���뵥��ӡ
            else if (e.ClickedItem == this.tbPacsApply)
            {
                this.ucOutPatientOrder1.PacsReportPrint();
            }

            #endregion

            #region �������뵥���

            else if (e.ClickedItem == this.tblEditPacsApply)
            {
                this.ucOutPatientOrder1.EditPascApply();
            }
            #endregion

            #region ������ҩ����
            else if (e.ClickedItem == this.tbPass)
            {
                this.tbPass.Checked = !this.tbPass.Checked;

                this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(CacheManager.LogEmpl.ID);

                if (setting == null)
                {
                    setting = new FS.HISFC.Models.Base.UserDefaultSetting();
                    setting.Empl.ID = CacheManager.LogEmpl.ID;
                    setting.Oper.ID = CacheManager.LogEmpl.ID;
                    FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
                    setting.Oper.OperTime = funMgr.GetDateTimeFromSysDateTime();
                    setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

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
                    setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

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

            #region ���㸽��
            else if (e.ClickedItem == this.tbCalculatSubl)
            {
                this.ucOutPatientOrder1.CalculatSubl(false);
            }
            #endregion


            #region ��ʱ��β�ѯ���ﻼ��
            else if (e.ClickedItem == this.tbChooseTime)
            {
                this.QueryPatientByTimeSpan();
            }
            #endregion


            #region ����������ʾ
            ////����������ʾ
            //if (this.ucOutPatientOrder1.IsDesignMode)
            //{
            //    if (this.CurrentControl.GetType() != typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
            //    {
            //        this.panel2.Visible = false;
            //    }
            //    else
            //    {
            //        this.panel2.Visible = true;
            //    }
            //}
            #endregion

            #region ��ϱ��棨������ת����������ʹ�Ⱦ��������ʾ��
            //if (this.CurrentControl != null
            //    && this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
            //{
            //    //������Ϻ��Զ���ת����������
            //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
            //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
            //}
            #endregion


            #region �°���Ӳ������--���ﲡ��

            //���ܲ������ϣ��˴���ʱ���������Ǻ����ýӿڹ�tabҳ�ķ�ʽʵ��

            //{4F2B8C3A-A728-4668-9879-37BF75DBE6E2}
            //else if (e.ClickedItem == this.tbEmergency)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_Emergency_Record);
            //}
            //else if (e.ClickedItem == this.tbFirst)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_First);
            //}
            //else if (e.ClickedItem == this.tbSecond)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_Second_Record);
            //}
            #endregion


            #region ��չ��ť1

            //��չ1 ֻ���ڿ���ģʽ��ʹ��
            else if (e.ClickedItem == this.tbExtend1)
            {
                if (IOrderExtendModule != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                        return;
                    }
                    if (IOrderExtendModule.DoOrderExtend1(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "����", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion
            #region ��չ��ť2
            //��չ2 �κ�ʱ����ʹ��
            else if (e.ClickedItem == this.tbExtend2)//����
            {
                if (IOrderExtendModule != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                        return;
                    }
                    if (IOrderExtendModule.DoOrderExtend2(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "����", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion
            #region ��չ��ť3
            //��չ3 ֻ���ڷǿ���ģʽ��ʹ��
            else if (e.ClickedItem == this.tbExtend3)//����
            {
                if (IOrderExtendModule != null)
                {
                    //if (this.ucOutPatientOrder1.Patient == null)
                    //{
                    //    MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                    //    return;
                    //}
                    if (IOrderExtendModule.DoOrderExtend3(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "����", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion

            #region ������Ŀ��������
            else if (e.ClickedItem == this.tbChooseUL)
            {
                if (this.IOrderChooseUl != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
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
                            this.ucOutPatientOrder1.AddGroupOrder(alOrders);
                        }
                    }
                }
            }
            #endregion

            #region ���Һ�
            else if (e.ClickedItem == this.tbAddReg)
            {
                this.ucOutPatientTree1.AddNewReg();
                this.ucOutPatientTree1.RefreshNoSeePatient();
            }
            #endregion

            if (tvGroup != null)
            {
                if (ucOutPatientOrder1.Patient != null)
                {
                    tvGroup.Pact = ucOutPatientOrder1.Patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }
            }
        }


        /// <summary>
        /// ����ʱ��β�ѯ���ﻼ��
        /// </summary>
        private void QueryPatientByTimeSpan()
        {
            FS.FrameWork.WinForms.Forms.frmChooseDate chooseForm = new FS.FrameWork.WinForms.Forms.frmChooseDate();
            chooseForm.ShowDialog();
            //if (!this.ucOutPatientTree1.BAlreadyState)
            //{
            //    this.ucOutPatientTree1.BAlreadyState = true;
            //}
            this.ucOutPatientTree1.RefreshSeePatientByDate(new DateTime[] { chooseForm.DateBegin, chooseForm.DateEnd });

        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.ucOutPatientOrder1.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                //if (this.ucOutPatientOrder1.neuSpread1.ActiveSheet.IsSelected(i, 0))
                //{
                FS.HISFC.Models.Order.OutPatient.Order order = this.ucOutPatientOrder1.GetObjectFromFarPoint(i, this.ucOutPatientOrder1.neuSpread1.ActiveSheetIndex).Clone();
                if (order == null)
                {
                    MessageBox.Show("���ҽ������");
                }
                else
                {
                    string s = order.Item.Name;
                    string sno = order.Combo.ID;
                    //����ҽ������ Ĭ�Ͽ���ʱ��Ϊ ���
                    order.BeginTime = new DateTime(order.BeginTime.Year, order.BeginTime.Month, order.BeginTime.Day, 0, 0, 0);
                    al.Add(order);
                }
                //}
            }
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
                this.ucOutPatientOrder1.neuSpread1.ActiveSheet.RowCount = 0;
            }
        }

        #endregion

        #region IPreArrange ��Ա

        /// <summary>
        /// ��½ǰ�ĸ����ж�
        /// </summary>
        /// <returns></returns>
        public int PreArrange()
        {
            Classes.LogManager.Write("����ʼ����ҽ��from�����ʼ��ǰ ������");

            //����ȡ������ 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            if (this.ucOutPatientTree1.InitControl() == -1)
            {
                return -1;
            }

            if (this.ucOutPatientTree1.RefreshNoSeePatient() == -1)
            {
                return -1;
            }

            FS.HISFC.Models.Base.Department currentDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(CacheManager.LogEmpl.Dept.ID);

            if (currentDept == null || currentDept.ID.Length < 0)
            {
                return -1;
            }

            isHosDeptCanLogin = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ08", false, "0"));

            if (currentDept.DeptType.ID.ToString() == "I")
            {
                if (isHosDeptCanLogin)
                {
                    if (MessageBox.Show("����ǰ��½�Ŀ���ΪסԺ���ң��Ƿ������",
                                         "ѯ��",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else
                {
                    MessageBox.Show("����ǰ��½�Ŀ���ΪסԺ���ң�������ѡ��", "����ҽ��վ�������¼סԺ����");
                    return -1;
                }
            }

            //���ڷ������̣��ܶ�ط���Ҫ�ж����ң���Ϊѡ�����Һ�������ֵ��ucOutPatientOrder
            this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

            Classes.LogManager.Write("����������ҽ��from�����ʼ��ǰ ������");
            return 1;
        }

        #endregion

        #region IInterfaceContainer ��Ա    {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2}

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] 
                { 
                    typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP) 
                };
            }
        }

        #endregion

        #region EMR���ﲡ���ӿ�  Ŀǰ����

        /// <summary>
        /// �������ﲡ��
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="type"></param>
        public void SetOutpatientCase(FS.HISFC.Models.Registration.Register reg, CaseType type)
        {
            #region ���ܲ������ϣ��˴���ʱ���������Ǻ����ýӿڹ�tabҳ�ķ�ʽʵ��


            //if (reg == null || string.IsNullOrEmpty(reg.ID))
            //{
            //    MessageBox.Show("��ѡ������Ϣ");
            //    return;
            //}
            //FS.HISFC.BizProcess.Integrate.EMRService.EMRServiceOutpatient emrService = new FS.HISFC.BizProcess.Integrate.EMRService.EMRServiceOutpatient();
            //long emrID = 0;
            //if (emrService.GetEmrRegId(reg.ID, ref emrID) < 0)
            //{
            //    MessageBox.Show("��ȡEMR������ˮ��ʧ��" + emrService.Err);
            //    return;
            //}
            //FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface emrInterface = new FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface();
            //FS.HISFC.Models.Base.Employee emp = CacheManager.OrderMgr.Operator as FS.HISFC.Models.Base.Employee;
            //string queryEmp = @"select e.ID from vemr_emp e where e.EMPL_CODE='{0}'";
            //string querydept = @"select d.id from vemr_department d where d.dept_code='{0}'";

            //queryEmp = string.Format(queryEmp, emp.ID);
            //querydept = string.Format(querydept, emp.Dept.ID);
            //string empid = CacheManager.OrderMgr.ExecSqlReturnOne(queryEmp);
            //string deptID = CacheManager.OrderMgr.ExecSqlReturnOne(querydept);
            //if (empid == "-1" || deptID == "-1")
            //{
            //    MessageBox.Show("����Ա��Ż��߿�����EMRϵͳ������");
            //    return;
            //}

            //emrInterface.CreatNewOutSetByPatient(emrID, Convert.ToInt64(empid), CacheManager.OrderMgr.GetDateTimeFromSysDateTime());
            //FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde emrUIInterface = new FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde();
            //System.Windows.Forms.Control con = emrUIInterface.CreatNewOutRecord(emrID, Convert.ToInt64(deptID), Convert.ToInt64(empid), type.ToString());
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(con);

            #endregion
        }

        /// <summary>
        /// ��������
        /// </summary>
        public enum CaseType
        {
            /// <summary>
            /// ����
            /// </summary>
            Out_Emergency_Record,

            /// <summary>
            /// ����
            /// </summary>
            Out_First,

            /// <summary>
            /// ����
            /// </summary>
            Out_Second_Record
        }
        #endregion
    }
}
