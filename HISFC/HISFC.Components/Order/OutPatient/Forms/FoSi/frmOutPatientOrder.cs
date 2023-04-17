using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Order.OutPatient.Forms.FoSi
{
    /// <summary>
    /// ����ҽ��վ������
    /// </summary>
    public partial class frmOutPatientOrder : Neusoft.FrameWork.WinForms.Forms.frmBaseForm, Neusoft.FrameWork.WinForms.Classes.IPreArrange, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmOutPatientOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.iControlable = this.ucOutPatientOrder1 as Neusoft.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOutPatientOrder1;
                this.panelToolBar.Visible = false;
                this.FormClosing += new FormClosingEventHandler(frmOutPatientOrder_FormClosing);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        void frmOutPatientOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucOutPatientOrder1.QuitPass();
            this.ucOutPatientOrder1.ReleaseLisInterface();
            this.ucOutPatientOrder1.ReleasePacsInterface();

            return;
        }

        #region ����

        /// <summary>
        /// �˻�������ҵ���  {184209CF-569F-4355-896D-FB33FF6C506F} 
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        private Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();

        private HISFC.Components.Order.OutPatient.Classes.Function Function = new HISFC.Components.Order.OutPatient.Classes.Function();

        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

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
        private Neusoft.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// �������б�
        /// </summary>
        Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// �Ƿ�༭����
        /// </summary>
        bool isEditGroup = false;

        public ArrayList Diagnoses = null;

        /// <summary>
        /// �Ƿ��״�¼����ϣ���һ����Ҫ��������б�
        /// </summary>
        private bool isFirst = true;

        #endregion

        #region �¼�

        private void frmOutPatientOrder_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

            ////this.AddOrderHandle();
            this.initButton(false);

            this.tbAddOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Yҽ��);
            this.tbComboOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�);
            this.tbCancelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbDelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sɾ��);
            this.tbOperation.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z���);
            this.tbSaveOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B����);
            this.tbCheck.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tb1Exit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbExitOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbGroup.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z����);

            this.tbSeePatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.X��һ��);
            this.tbRefreshPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sˢ��);
            this.tbQueryOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);
            this.tbPatientTree.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.G�˿�);

            this.tbPrintOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbRegEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.J����);
            this.tbOutEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C��Ժ�Ǽ�);
            this.tbInEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Zת��);
            this.tbDiseaseReport.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.J��������);
            this.tbHerbal.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C��ҩ);
            //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} �㼶��ʽ����ҽ�� yangw 20101024
            this.tbLevelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z�Ӽ�);
            this.tbLisResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tbPacsResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S�豸);

            this.tbLisReportPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S���뵥);
            this.tbPacsReportPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S���뵥);
            this.tbPass.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B����);
            this.tbCalculatSubl.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�);

            this.tbStation.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.X��һ��);
            this.SetMenuVisible();

            this.panelTree.Height = this.Height - 162;
            this.panelTree.Visible = false;

            this.ucOutPatientTree1.TreeDoubleClick += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.TreeDoubleClickHandler(ucOutPatientTree1_TreeDoubleClick);

            this.isAccountTerimal = controlIntegrate.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            this.isAccountMode = controlIntegrate.GetControlParam<bool>("MZ0300", true, false);

            this.ucOutPatientOrder1.IsAccountTerimal = this.isAccountTerimal;
            this.ucOutPatientOrder1.IsAccountMode = this.isAccountMode;
            this.ucOutPatientTree1.IsAccountMode = this.isAccountMode;
            this.ucOutPatientOrder1.OnRefreshGroupTree += new EventHandler(ucOutPatientOrder1_OnRefreshGroupTree);
            this.ucOutPatientTree1.CopyRecipeByPatientTree += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.CopyRecipeByPatientTreeHandler(ucOutPatientTree1_CopyRecipeByPatientTree);
            this.ucOutPatientTree1.DeleteRecipeByPatientTree += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.DeleteRecipeByPatientTreeHandler(ucOutPatientTree1_DeleteRecipeByPatientTree);

            foreach (TabPage tb in this.tabControl1.TabPages)
            {
                tb.Text = Neusoft.FrameWork.Management.Language.Msg(tb.Text);
            }

            Application.DoEvents();

            if (this.dcpInstance == null)
            {
                this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
            }

            if (this.dcpInstance != null)
            {
                this.dcpInstance.LoadNotice(this, Neusoft.HISFC.Models.Base.ServiceTypes.C);
            }

            try
            {

                Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting();
                Neusoft.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

                if (setting == null)
                {
                    //MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.tbPass.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                    this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.tbPass.CheckedChanged += new EventHandler(tbPass_CheckedChanged);
        }

        void tbPass_CheckedChanged(object sender, EventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;            

        Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting();
            Neusoft.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

            if (setting == null)
            {
                MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                setting.Setting4 = Neusoft.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                if (userSetmgr.Update(setting) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(userSetmgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
            }
        }

        void ucOutPatientOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        /// <summary>
        /// ����������ڵ���ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    this.Tag = this.ucOutPatientTree1.neuTreeView2.SelectedNode.Tag;
                }

                return;

                if (isAccountMode)
                {
                    #region �˴����ٿ۷ѣ�ҽ������ʱ���շ�
                    if (this.Tag is Neusoft.HISFC.Models.Registration.Register)
                    {
                        //�ж��˻����̵ĹҺ��շ����
                        bool isAccount = false;
                        decimal vacancy = 0m;
                        Neusoft.HISFC.Models.Registration.Register r = (Neusoft.HISFC.Models.Registration.Register)Tag;

                        if (isAccountTerimal && r.IsAccount)
                        {

                            if (feeMgr.GetAccountVacancy(r.PID.CardNO, ref vacancy) <= 0)
                            {
                                MessageBox.Show(feeMgr.Err);
                                return;
                            }
                            isAccount = true;
                        }
                        if (isAccount && r.IsFee == false)
                        {
                            #region �˻���ȡ�Һŷ�

                            if (!feeMgr.CheckAccountPassWord(r))
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

                                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                                if (feeMgr.AccountPay(r, r.OwnCost, "�Һ��շ�", (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID, string.Empty) < 0)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���˻����ʧ�ܣ�" + feeMgr.Err);
                                    return;
                                }
                                Neusoft.HISFC.BizProcess.Integrate.Registration.Registration registerManager = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();
                                r.SeeDoct.ID = orderManager.Operator.ID;
                                r.SeeDoct.Dept.ID = (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID;
                                if (registerManager.UpdateAccountFeeState(r.ID, r.SeeDoct.ID, r.SeeDoct.Dept.ID, orderManager.GetDateTimeFromSysDateTime()) == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���¹Һű����շ�״̬����");
                                    return;
                                }
                                Neusoft.FrameWork.Management.PublicTrans.Commit();
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

        private void ucOutPatientTree1_CopyRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister, int copyNum)
        {
            this.ucOutPatientOrder1.CopyRecipeByPatientTree(selectRegister, copyNum);
        }

        private void ucOutPatientTree1_DeleteRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister)
        {
            this.ucOutPatientOrder1.DeleteRecipeByPatientTree(selectRegister);
        }


        /// <summary>
        /// ѡ������
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOutPatientOrder1.AddGroupOrder(alOrders);

            ////{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //��ҩ������ҩ��������
            //ArrayList alHerbal = new ArrayList(); //��ҩ

            //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ش�����Ϣ�����Ժ�", 10, false);
            //Application.DoEvents();
            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //int count = 1;
            //foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrders)
            //{
            //    //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(count);
            //    //Application.DoEvents();

            //    if (this.ucOutPatientOrder1.Patient != null && ucOutPatientOrder1.IsDesignMode)
            //    {
            //        #region �жϿ���Ȩ��

            //        string error = "";

            //        int ret = 1;

            //        //����Ȩ
            //        ret = Components.Order.Classes.Function.JudgeEmplPriv(order, orderManager.Operator,
            //            (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept, Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref error);

            //        if (ret <= 0)
            //        {
            //            MessageBox.Show(error);
            //            continue;
            //        }

            //        //����ʷ�ж�
            //        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.ucOutPatientOrder1.Patient.PID, order, ref error);

            //        if (ret <= 0)
            //        {
            //            return;
            //        }
            //        #endregion
            //    }

            //    if (order.Item.SysClass.ID.ToString() == "PCC") //��ҩ
            //    {
            //        Classes.Function.ReComputeQty(order);

            //        alHerbal.Add(order);
            //    }
            //    else
            //    {
            //        this.ucOutPatientOrder1.AddNewOrder(order, 0);
            //    }
            //    count += 1;
            //}
            //if (alHerbal.Count > 0)
            //{
            //    this.ucOutPatientOrder1.AddHerbalOrders(alHerbal);
            //}
            //this.ucOutPatientOrder1.RefreshOrderState();
            //this.ucOutPatientOrder1.RefreshCombo();

            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="isDisign"></param>
        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;
            //�������治�����ӡ�����������ӡ���ݺͱ������ݲ�һ��
            this.tbPrintOrder.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbPatientTree.Enabled = !isDisign;
            this.tbCalculatSubl.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            this.tbHerbal.Enabled = isDisign;
            this.tbLevelOrder.Enabled = isDisign;
            this.tbOperation.Enabled = false;
            this.tbDelOrder.Enabled = isDisign;
            this.tbDelOneOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            this.tbFilter.Enabled = !isDisign;
            this.tbFilter.Visible = false;
            this.tbQueryOrder.Enabled = !isDisign;

            this.tbSaveOrder.Enabled = isDisign;
            this.tbSeePatient.Enabled = !isDisign;

            this.tbLisReportPrint.Enabled = !isDisign;
            this.tbPacsReportPrint.Enabled = !isDisign;

            this.tbStation.Enabled = !isDisign;
            if (isDisign) //����
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.panelTree.Visible = true;
                this.panel2.Visible = true;
                if (this.btnShow.Visible != true)
                {
                    this.panel2.Width = 170;
                    this.panelTree.Width = 170;
                }
                this.panelTree.Controls.Add(tvGroup);
                //this.SetTree(tvGroup);
                this.neuPanel1.Visible = false;
            }
            else
            {
                this.neuPanel1.Visible = true;
                this.panelTree.Visible = false;
                this.panel2.Visible = false;
                if (tvGroup != null) tvGroup.Visible = false;
                //this.ucOutPatientOrder1.Patient = new Neusoft.HISFC.Models.Registration.Register();
            }
        }

        /// <summary>
        /// ���ס�������ʾ�˵�
        /// </summary>
        /// <param name="isEdit">�Ƿ�����ģʽ</param>
        private void initButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbPatientTree.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            //this.tbExitOrder.Enabled = !isEdit;
            this.isEditGroup = isEdit;
            this.tbQueryOrder.Enabled = !isEdit;
            this.tbLisReportPrint.Enabled = !isEdit;
            this.tbPacsReportPrint.Enabled = !isEdit;

            this.tbSeePatient.Enabled = !isEdit;
            //{CF7BCF69-95C3-4dcf-A61C-451E26C56161}
            this.tbComboOrder.Enabled = isEdit;
            //this.tbCalculatSubl.Enabled = isEdit;
            this.tbHerbal.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;
            this.tbDelOrder.Enabled = isEdit;//{11F97F55-F747-4ad9-A74F-086635D5EBD9}
            this.tbDelOneOrder.Enabled = isEdit;
            if (isEdit) //����
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;
                this.panelTree.Visible = true;
                this.panel2.Visible = true;
                if (this.btnShow.Visible != true)
                {
                    this.panel2.Width = 170;
                    this.panelTree.Width = 170;
                }
                this.panelTree.Controls.Add(tvGroup);
            }
            else
            {
                this.panelTree.Visible = false;
                this.panel2.Visible = false;
                if (tvGroup != null)
                {
                    tvGroup.Visible = false;
                }
            }
        }

        /// <summary>
        /// �˵����ÿɼ���
        /// </summary>
        private void SetMenuVisible()
        {
            Neusoft.HISFC.BizProcess.Integrate.Manager dictionaryMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            ArrayList alset = dictionaryMgr.GetConstantList("OUTPATMENU");

            Neusoft.FrameWork.Public.ObjectHelper setHelper = new Neusoft.FrameWork.Public.ObjectHelper();

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
                }
            }

            this.toolStrip1.Items.Clear();
            foreach (Neusoft.HISFC.Models.Base.Const conObj in alset)
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
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //����PatienId��ˮ��{17537415-C168-450d-BBCC-93CFFA19DB82}

                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    if (isFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isFirst = false;
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("��û��ѡ���ߣ�");
                    }
                    else
                    {
                        //(this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).PatientId = ucOutPatientTree1.PatientInfo.ID;
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).RegInfo = ucOutPatientTree1.PatientInfo;
                    }
                    //this.neuPanel1.Visible = false;
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucPatientCase))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).diagnoses = this.Diagnoses;
                    if (this.Diagnoses != null)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).SetDiagNose();
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("��û��ѡ���ߣ�");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).Reg = ucOutPatientTree1.PatientInfo;
                    }
                    //this.neuPanel1.Visible = false;
                }
                //����л�tabҳʱ��û�е�����б��еĻ��ߣ���ʷҽ����ˢ�� houwb {B9D1936C-4564-4e35-A158-40E8688267FF}
                else if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Patient = this.ucOutPatientTree1.PatientInfo;
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                }

                //����������ʾ
                if (this.ucOutPatientOrder1.IsDesignMode)
                {
                    if (this.CurrentControl.GetType() != typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
                    {
                        this.panel2.Visible = false;
                    }
                    else
                    {
                        this.panel2.Visible = true;
                    }
                }
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
                    this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    string msg = "";
                    Hashtable hs = new Hashtable();
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in e)
                    {
                        if (!hs.ContainsKey(diag.DiagInfo.ICD10.Name))
                        {
                            hs.Add(diag.DiagInfo.ICD10.Name, "");
                            this.dcpInstance.CheckDiseaseReport(this, this.ucOutPatientOrder1.Patient, Neusoft.HISFC.Models.Base.ServiceTypes.C, diag.DiagInfo.ICD10.Name, out msg);
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

            if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
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
        /// ��������ҽ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOutPatientOrder_CopyHistoryClicked(Neusoft.HISFC.Models.Registration.Register register)
        {
            
            if (!this.ucOutPatientOrder1.IsDesignMode)
            {
                return;
            }
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text.Contains("ҽ��"))
                {
                    tabControl1.SelectedTab = tabPage;
                    break;
                }
            }

            if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
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
            this.ucOutPatientOrder1.PasteOrder();
        }

        void frmOutPatientOrder_transportAlDiag(ArrayList arrayDiagnoses)
        {
            if (arrayDiagnoses.Count > 0)
            {
                this.Diagnoses = arrayDiagnoses;
            }
            return;
        }

        /// <summary>
        /// �����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.ucOutPatientOrder1.SetInputItemVisible(false);
                if (e.ClickedItem == this.tbAddOrder)//����
                {
                    //�˴����β�ѯ����ҽ����Ч�ʵ�
                    //this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                    //this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("��ǰ��δѡ�л��ߣ�");
                        return;
                    }

                    #region �շѴ����������ٴο���

                    if (!ucOutPatientOrder1.CheckCanAdd())
                    {
                        return;
                    }

                    #endregion

                    this.statusBar1.Panels[1].Text = "(��ɫ���¿�)(��ɫ���շ�)(��ɫ������)";

                    //���δ����ģ�����ʱ�Զ�����{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
                    if (!this.ucOutPatientOrder1.Patient.IsSee)
                    {
                        this.ucOutPatientTree1.AutoTriage();
                    }
                    if (this.ucOutPatientOrder1.Add() == 0)
                        this.initButton(true);

                    if (!this.ucOutPatientOrder1.isHaveDiag())
                    {
                        //����������ʾ��Ͻ���
                        foreach (TabPage tabPage in tabControl1.TabPages)
                        {
                            if (tabPage.Text.Contains("���"))
                            {
                                tabControl1.SelectedTab = tabPage;
                                break;
                            }
                        }
                    }
                }
                else if (e.ClickedItem == this.tbGroup)//����
                {
                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.tbGroup.CheckState = CheckState.Unchecked;
                        this.neuPanel1.Visible = true;
                    }
                    else
                    {
                        this.tbGroup.CheckState = CheckState.Checked;
                        this.neuPanel1.Visible = false;
                    }

                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.ucOutPatientOrder1.SetEditGroup(true);
                        this.ucOutPatientOrder1.Patient = null;
                        this.initButtonGroup(true);
                    }
                    else
                    {
                        this.ucOutPatientOrder1.SetEditGroup(false);
                        this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                        this.initButtonGroup(false);
                        this.panelTree.Visible = false;
                        this.panel2.Visible = false;
                    }

                }
                else if (e.ClickedItem == this.tbHerbal)
                {
                    this.ucOutPatientOrder1.HerbalOrder();
                }
                else if (e.ClickedItem == this.tbLevelOrder)
                {
                    //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} �㼶��ʽ����ҽ�� yangw 20101024
                    this.ucOutPatientOrder1.AddLevelOrders();
                }
                else if (e.ClickedItem == this.tbCheck)
                {
                    this.ucOutPatientOrder1.AddTest();
                }

                else if (e.ClickedItem == this.tbDelOrder)//ɾ��
                {
                    this.ucOutPatientOrder1.Del();
                }
                else if (e.ClickedItem == this.tbDelOneOrder)//ɾ��
                {
                    this.ucOutPatientOrder1.DeleteSingleOrder();
                }
                else if (e.ClickedItem == this.tbQueryOrder)//��ѯ
                {
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                    this.ucOutPatientOrder1.Retrieve();
                }
                else if (e.ClickedItem == this.tbPrintOrder)//��ӡ
                {
                    this.ucOutPatientOrder1.PrintRecipe(false);
                }
                else if (e.ClickedItem == this.tbComboOrder)//���
                {
                    this.ucOutPatientOrder1.ComboOrder();
                }
                else if (e.ClickedItem == this.tbCancelOrder)//ȡ�����
                {
                    this.ucOutPatientOrder1.CancelCombo();
                }
                else if (e.ClickedItem == this.tbExitOrder)//�˳�ҽ��
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
                        this.ucOutPatientOrder1.SetEditGroup(false);
                        this.initButtonGroup(false);
                    }
                    else
                    {
                        if (this.ucOutPatientOrder1.ExitOrder() == 0)
                        {
                            this.initButton(false);
                            //�˳�ʱ�ж�ȡ��������
                            this.ucOutPatientTree1.CancelTriage();
                        }
                    }
                }

                else if (e.ClickedItem == this.tbRegEmerPatient)//����
                {
                    if (this.ucOutPatientOrder1.RegisterEmergencyPatient() < 0)
                    {
                    }
                    else
                    {
                        //MessageBox.Show("���۳ɹ���");

                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (e.ClickedItem == this.tbOutEmerPatient) //����
                {
                    if (this.ucOutPatientOrder1.OutEmergencyPatient() > 0)
                    {
                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }

                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (e.ClickedItem == this.tbInEmerPatient) //תסԺ
                {
                    if (this.ucOutPatientOrder1.InEmergencyPatient() > 0)
                    {
                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                else if (e.ClickedItem == this.tbSeePatient)//���
                {
                    if (this.ucOutPatientOrder1.DiagOut() < 0)
                    {
                    }
                    else
                    {
                        MessageBox.Show(this.ucOutPatientOrder1.Patient.Name + " ����ɹ���");

                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                else if (e.ClickedItem == this.tbRefreshPatient)//ˢ��
                {
                    ucOutPatientTree1.RefreshTreeView();
                    ucOutPatientTree1.RefreshTreePatientDone();
                }
                else if (e.ClickedItem == this.tbPatientTree)//�б�
                {
                    this.neuPanel1.Visible = !this.neuPanel1.Visible;
                }
                else if (e.ClickedItem == this.tbSaveOrder)//����
                {
                    try
                    {
                        this.tbSaveOrder.Enabled = false;

                        if (isEditGroup)
                        {
                            SaveGroup();
                        }
                        else
                        {
                            if (this.ucOutPatientOrder1.Save() == -1)
                            {
                            }
                            else
                            {
                                this.initButton(false);
                                ucOutPatientTree1.RefreshTreeView();

                                ucOutPatientTree1.FreshSeePatientNode();
                            }
                        }
                        this.statusBar1.Panels[1].Text = "";
                    }
                    catch
                    {
                    }
                    finally
                    {
                        this.tbSaveOrder.Enabled = true;
                    }
                }
                else if (e.ClickedItem == this.tb1Exit)//�˳�
                {
                    if (this.ucOutPatientOrder1 != null && this.ucOutPatientOrder1.IsDesignMode) //���ڿ���״̬
                    {
                        DialogResult result = MessageBox.Show(this, Neusoft.FrameWork.Management.Language.Msg("ҽ��Ŀǰ���ڿ���ģʽ���Ƿ񱣴�?"), "��ʾ", MessageBoxButtons.YesNoCancel);
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
                else if (e.ClickedItem == this.tbDiseaseReport)     //  {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2} ��Ⱦ�����濨
                {
                    if (this.dcpInstance == null)
                    {
                        this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
                    }

                    if (this.dcpInstance != null)
                    {
                        Neusoft.HISFC.Models.RADT.Patient patient = this.ucOutPatientTree1.PatientInfo as Neusoft.HISFC.Models.RADT.Patient;

                        this.dcpInstance.RegisterDiseaseReport(this, patient, Neusoft.HISFC.Models.Base.ServiceTypes.C);
                    }
                }
                //LIS�����ӡ
                else if (e.ClickedItem == this.tbLisResultPrint)
                {
                    this.ucOutPatientOrder1.QueryLisResult();
                }
                //PACS�����ӡ
                else if (e.ClickedItem == this.tbPacsResultPrint)
                {
                    ucOutPatientOrder1.QueryPacsReport();
                }
                else if (e.ClickedItem == this.tbStation)
                {
                    this.ucOutPatientTree1.Call();
                }
                #region ���뵥��ӡ

                //LIS���뵥��ӡ
                else if (e.ClickedItem == this.tbLisReportPrint)
                {
                    this.ucOutPatientOrder1.LisReportPrint();
                }
                //PACS���뵥��ӡ
                else if (e.ClickedItem == this.tbPacsReportPrint)
                {
                    this.ucOutPatientOrder1.PacsReportPrint();
                }

                #endregion

                else if (e.ClickedItem == this.tbPass)
                {
                    this.tbPass.Checked = !this.tbPass.Checked;
                    //this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
                else if (e.ClickedItem == this.tbCalculatSubl)
                {
                    this.ucOutPatientOrder1.CalculatSubl(false);
                }

                //����������ʾ
                if (this.ucOutPatientOrder1.IsDesignMode)
                {
                    if (this.CurrentControl.GetType() != typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
                    {
                        this.panel2.Visible = false;
                    }
                    else
                    {
                        this.panel2.Visible = true;
                    }
                }

                if (this.CurrentControl != null && this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    //������Ϻ��Զ���ת����������
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                }
                if (this.CurrentControl != null && this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    //���ƴ������Զ���ת����������
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveGroup()
        {
            Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager group = new Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = (Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.ucOutPatientOrder1.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                //if (this.ucOutPatientOrder1.neuSpread1.ActiveSheet.IsSelected(i, 0))
                //{
                Neusoft.HISFC.Models.Order.OutPatient.Order order = this.ucOutPatientOrder1.GetObjectFromFarPoint(i, this.ucOutPatientOrder1.neuSpread1.ActiveSheetIndex).Clone();
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

        #region IPreArrange ��Ա   {B17077E6-7E65-45fb-BA25-F2883EB6BA27}

        public int PreArrange()
        {
            //����ȡ������ 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            if (this.ucOutPatientTree1.InitControl() == -1)
            {
                return -1;
            }

            if (this.ucOutPatientTree1.RefreshTreeView() == -1)
            {
                return -1;
            }

            Neusoft.HISFC.BizProcess.Integrate.Manager managerInte = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            Neusoft.HISFC.Models.Base.Department currentDept = managerInte.GetDepartment((orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID);

            if (currentDept == null || currentDept.ID.Length < 0)
                return -1;

            if (currentDept.DeptType.ID.ToString() == "I")
            {
                if (MessageBox.Show("����ǰ��½�Ŀ���ΪסԺ���ң��Ƿ������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return -1;
                }
            }

            //���ڷ������̣��ܶ�ط���Ҫ�ж����ң���Ϊѡ�����Һ�������ֵ��ucOutPatientOrder
            this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

            return 1;
        }

        #endregion

        #region IInterfaceContainer ��Ա    {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2}

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP) };
            }
        }

        #endregion
    }
}