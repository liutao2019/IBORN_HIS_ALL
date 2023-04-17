using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.WinForms.WorkStation
{
    /// <summary>
    /// {2A5F9B85-CA08-4476-A5A4-56F34F0C28AC}
    /// ��ʿҽ������������
    /// </summary>
    public partial class frmNurseOrderCreate : FS.FrameWork.WinForms.Forms.frmBaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        private Control CurrentControl;

        FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 
        /// </summary>
        public frmNurseOrderCreate()
        {
            
            InitializeComponent();

            this.SetTree(this.tvNursePatientList1);
            this.ucOrder1.IsNurseCreate = true;
            this.iQueryControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IQueryControlable;
            this.iControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IControlable;
            this.CurrentControl = this.ucOrder1;
            
            this.tbGroup.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z����);
            this.tbQueryOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ);
            this.tbExitOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tbPrintOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ);
            this.tbGroup.CheckState = CheckState.Unchecked;
            this.Resize += new EventHandler(frmOrder_Resize);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.tbGroup.Visible = false;
            this.tbOperation.Visible = false;
            this.tbCheck.Visible = false;
            this.tbAssayCure.Visible = false;
            this.tbPrintOrder.Visible = false;
            this.tsbHerbal.Visible = false;
            this.tbQueryOrder.Visible = false;
            this.tbRetidyOrder.Visible = false;
            this.tbFilter.Visible = false;
            this.toolStripSeparator4.Visible = false;
            this.toolStripSeparator5.Visible = false;
            this.tvNursePatientList1.Refresh();
        }
        //{A5409134-55B5-42d9-A264-25060169A64B}
        private FS.FrameWork.Public.ObjectHelper frequencyHelper = new FS.FrameWork.Public.ObjectHelper();


        void frmOrder_Resize(object sender, EventArgs e)
        {
            this.panelTree.Height = this.Height - 162;

        }


        FS.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;//����
        bool isEditGroup = false;
        private void frmOrder_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
            this.AddOrderHandle();
            this.initButton(false);

            this.tbAddOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��);
            this.tbComboOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�);
            this.tbCancelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��);
            this.tbDelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��);
            this.tbOperation.Image = FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.Z���);
            this.tbSaveOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.B����);
            this.tbCheck.Image = FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.H����);
            this.tb1Exit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage( FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�);
            this.tsbHerbal.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ);
            ///{FB86E7D8-A148-4147-B729-FD0348A3D670}  ����ҽ��������ť
            this.tbRetidyOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T�ײ�);

            #region add by xuewj ���ӻ��ư�ť {1F2B9330-7A32-4da4-8D60-3A4568A2D1D8}
            this.tbAssayCure.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H����);
            #endregion

            //����ѡ��ҽ����ť{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            this.tbChooseDoct.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H��ҽʦ);
            this.panelTree.Height = this.Height - 162;
            //{A5409134-55B5-42d9-A264-25060169A64B}
            ArrayList alFrequency = FS.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject;
            if (alFrequency != null)
            {
                this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
            }

        }


        #region ˽�к���
        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;
            tbRefresh.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            //this.tbOperation.Enabled = false;
            this.tbOperation.Enabled = isDisign;
            this.tbAssayCure.Enabled = isDisign;
            this.tbDelOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            this.tbFilter.Enabled = !isDisign;
            this.tbQueryOrder.Enabled = !isDisign;
            this.tbSaveOrder.Enabled = isDisign;
            this.tsbHerbal.Enabled = isDisign;
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
                
                this.tvNursePatientList1.Visible = false;
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

            }
            else
            {
                this.tvNursePatientList1.Visible = true;
                if(tvGroup!=null) tvGroup.Visible = false;
            }
        }

        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //��ҩ������ҩ��������
            ArrayList alHerbal = new ArrayList(); //��ҩ

            foreach(FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            {
                FS.HISFC.Models.Order.Inpatient.Order myorder = order.Clone();
                myorder.Patient.PVisit.PatientLocation.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                if (fillOrder(ref myorder) != -1)
                {
                    if (order.Item.SysClass.ID.ToString() == "PCC") //��ҩ
                    {
                        alHerbal.Add(order);
                    }
                    else
                    {
                        if (myorder.OrderType.IsDecompose)
                        {
                            this.ucOrder1.AddNewOrder(myorder, 0);
                        }
                        else
                        {
                            this.ucOrder1.AddNewOrder(myorder, 1);
                        }
                    }
                }
            }

            if (alHerbal.Count > 0)
            {
                this.ucOrder1.AddHerbalOrders(alHerbal);
            }
            this.ucOrder1.RefreshCombo();
           
        }

        private int fillOrder(ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string err = "";
            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            //if (order.Item.IsPharmacy)
            {
                //��ʿ��������ҩƷ {5DE2C8F9-2E5D-43d6-9CAD-A5E0F60AC94B
                return -1;
                //if (FS.HISFC.BizProcess.Integrate.Order.FillPharmacyItemWithStockDept(null, ref order, out err) == -1)
                //{
                //    MessageBox.Show(err);
                //    return -1;
                //}
            }
            else
            {
                if (orderIntegrate.FillFeeItem(ref order, out err) == -1)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }
            //{A5409134-55B5-42d9-A264-25060169A64B}
            FS.FrameWork.Models.NeuObject trueFrequency = this.frequencyHelper.GetObjectFromID(order.Frequency.ID);
            if (trueFrequency != null)
            {
                order.Frequency = trueFrequency as FS.HISFC.Models.Order.Frequency;
            }

            return 0;
        }
        private void initButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            this.tbRefresh.Enabled = !isEdit;
            this.tbExitOrder.Enabled = isEdit;
            this.isEditGroup = isEdit;
            //{EB959BC4-9120-478a-B527-74A1D7EF4C9E}
            this.tbComboOrder.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;

            if (isEdit) //����
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();
                    tvGroup.SelectOrder+=new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;
               
                this.tvNursePatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
            }
            else
            {
                this.tvNursePatientList1.Visible = true;
                if (tvGroup != null) tvGroup.Visible = false;
            }
        }

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
        }

        void ucOrder1_OrderCanCancelComboChanged(bool b)
        {
            this.tbCancelOrder.Enabled = b;
        }
        #endregion

        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizProcess.Integrate.RADT inpatientManager = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Order.Consultation consultation = new FS.HISFC.BizLogic.Order.Consultation();
        protected string inpatientNo;
        ArrayList co = null;

        #region �¼�
        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbAddOrder)
            {
                /// <summary>
                /// [��������: ����ҽ��]<br></br>
                /// [�� �� ��: ]<br></br>
                /// [����ʱ��: ]<br></br>
                /// <�޸ļ�¼
                ///		�޸���='����'
                ///		�޸�ʱ��='2007-8-25'
                ///		�޸�Ŀ��='�Ի���ҽʦ�ܷ���ҽ�����п���'
                ///		�޸�����='�жϻ��ﻼ�����޿���ҽ����Ȩ��'
                ///  />
                /// </summary>
                //ѡ���ӽڵ�
                if (this.tvNursePatientList1.SelectedNode.Parent != null && this.tvNursePatientList1.SelectedNode.Parent.Tag != null)
                {
                    int count = 0;
                    count = this.tvNursePatientList1.SelectedNode.Parent.GetNodeCount(false);
                    //�ж���ѡ�ڵ㸸�ڵ����Ϊ���ﻼ��,���ж����޿���ҽ����Ȩ��/������ǻ��ﻼ������Ҫ�����ж�,�����Խ��п���ҽ��
                    if (this.tvNursePatientList1.SelectedNode.Parent.Text == ("���ﻼ��" + "(" + count.ToString() + ")"))
                    {
                        patient = this.tvNursePatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                        this.Text = "�����ڲ����Ļ���Ϊ סԺ�ţ�" + patient.ID + "������" + patient.Name + "�Ա�" + patient.Sex.Name + " ����:" + patient.PVisit.PatientLocation.Bed.ID;
                        inpatientNo = patient.ID;
                        co = consultation.QueryConsulation(this.inpatientNo);
                        if (co != null || co.Count != 0)
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
                                }
                                else
                                {
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Բ���,��û�жԸû��߿���ҽ����Ȩ��!"), "��ʾ");
                                    return;
                                }
                            }
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.PatientInfo patient1 = this.tvNursePatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                        this.Text = "�����ڲ����Ļ���Ϊ סԺ�ţ�" + patient1.ID + "������" + patient1.Name + " �Ա�" + patient1.Sex.Name + " ����:"+ patient1.PVisit.PatientLocation.Bed.ID;
                        if (this.ucOrder1.Add() == 0)
                            this.initButton(true);
                    }
                }
            }
            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOrder1.AddTest();
            }
            else if (e.ClickedItem == this.tbRefresh)
            {
                //ˢ��
                this.tvNursePatientList1.Refresh();
            }
            else if (e.ClickedItem == this.tbGroup)
            {
                if (this.tbGroup.CheckState == CheckState.Checked)
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                }

                if (this.tbGroup.CheckState == CheckState.Checked)
                {
                    this.ucOrder1.SetEditGroup(true);
                    this.ucOrder1.SetPatient(null);
                    this.initButtonGroup(true);

                }
                else
                {
                    this.ucOrder1.SetEditGroup(false);
                    this.initButtonGroup(false);
                }

            }
            else if (e.ClickedItem == this.tbOperation)
            {
                //FS.HISFC.Models.RADT.PatientInfo pi = (FS.HISFC.Models.RADT.PatientInfo)this.tvDoctorPatientList1.SelectedNode.Tag;
                //frmOperation frmOpt = new frmOperation(pi);
                //frmOpt.ShowDialog();

                //ucOperation operation = new ucOperation(pi);
                //operation.Show();
                //operation.Show();
                //UFC.Operation.ucApplicationForm appForm = new UFC.Operation.ucApplicationForm();
                //appForm.PatientInfo = pi;
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(appForm);
                //appForm.Show();
                //UFC.Operation.ucApplication ucApply = new UFC.Operation.ucApplication(pi, pi);
                FS.HISFC.Models.RADT.PatientInfo pi = (FS.HISFC.Models.RADT.PatientInfo)this.tvNursePatientList1.SelectedNode.Tag;
                frmOperation frmOpt = new frmOperation(pi);
                frmOpt.ShowDialog();
            }
            else if (e.ClickedItem == this.tbAssayCure)
            {
                this.ucOrder1.AddAssayCure();
            }
            else if (e.ClickedItem == this.tbDelOrder)
            {
                this.ucOrder1.Delete();
            }
            else if (e.ClickedItem == this.tbQueryOrder)
            {
                try
                {

                    this.ucOrder1.Query(this.tvNursePatientList1.SelectedNode, this.tvNursePatientList1.SelectedNode.Tag);
                }
                catch { }
            }
            else if (e.ClickedItem == this.tbPrintOrder)
            {
                if (CurrentControl != null)
                {
                    try
                    {
                        FS.FrameWork.WinForms.Controls.ucBaseControl control = CurrentControl as FS.FrameWork.WinForms.Controls.ucBaseControl;
                        if (control != null)
                            control.Print(null, null);
                    }
                    catch { }
                }

            }
            else if (e.ClickedItem == this.tbComboOrder)
            {
                this.ucOrder1.ComboOrder();
            }
            else if (e.ClickedItem == this.tbCancelOrder)
            {
                this.ucOrder1.CancelCombo();
            }
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
                    this.initButtonGroup(false);

                }
                else
                {
                    if (this.ucOrder1.ExitOrder() == 0)
                        this.initButton(false);
                    tvNursePatientList1.Refresh();
                }
            }
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
            else if (e.ClickedItem == this.tbSaveOrder)
            {
                //
                if (isEditGroup)
                {
                    SaveGroup();
                }
                else
                {
                    if (this.ucOrder1.Save() == -1)
                    {
                    }
                    else
                    {
                        this.initButton(false);
                        tvNursePatientList1.Refresh();
                    }
                }
            }
            else if (e.ClickedItem == this.tsbHerbal)
            {
                this.ucOrder1.HerbalOrder();
            }
            else if (e.ClickedItem == this.tbChooseDoct)//{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            {
                this.ucOrder1.ChooseDoctor();
            }
            else if (e.ClickedItem == this.tb1Exit)
            {
                if (this.ucOrder1.IsDesignMode) //���ڿ���״̬
                {
                    DialogResult result = MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ��Ŀǰ���ڿ���ģʽ���Ƿ񱣴�?"), "��ʾ", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (this.ucOrder1.Save() == 0) this.Close();

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
            ///{FB86E7D8-A148-4147-B729-FD0348A3D670}  ����ҽ��������ť
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
        }


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
            for (int i = 0; i < this.ucOrder1.fpOrder.ActiveSheet.Rows.Count; i++)
            {
                if (this.ucOrder1.fpOrder.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = this.ucOrder1.GetObjectFromFarPoint(i, this.ucOrder1.fpOrder.ActiveSheetIndex).Clone();
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
                }
            }
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
            }
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];
            }
        }
    }
}