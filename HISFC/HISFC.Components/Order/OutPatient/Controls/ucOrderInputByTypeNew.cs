using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// ��Ŀѡ��仯
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="changedField"></param>
    public delegate void ItemSelectedDelegateNew(Neusoft.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField);
    /// <summary>
    /// ҽ������ؼ�
    /// </summary>
    public partial class ucOrderInputByTypeNew : UserControl
    {
        public ucOrderInputByTypeNew()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.lnkTime.Visible = false;
            }
        }

        #region ����
        /// <summary>
        /// ��Ŀ�仯ѡ�񼰱仯�¼�
        /// </summary>
        public event ItemSelectedDelegateNew ItemSelected;

        /// <summary>
        /// �����Ƿ���Ϊ�޸Ĺ�
        /// </summary>
        private bool isQtyChanged = false;

        /// <summary>
        /// �����Ƿ���Ϊ�޸Ĺ�
        /// </summary>
        public bool IsQtyChanged
        {
            get
            {
                return isQtyChanged;
            }
            set
            {
                isQtyChanged = value;
            }
        }

        /// <summary>
        /// ʧȥ����ʱ���¼�����
        /// </summary>
        /// <param name="obj"></param>
        public delegate void FocusLostHandler(object sender, EventArgs e);

        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ִ�п���
        /// </summary>
        private ArrayList deptAll = new ArrayList();

        /// <summary>
        /// ʧȥ����ʱ���¼�
        /// </summary>
        public event FocusLostHandler FocusLost;

        /// <summary>
        /// �뿪�¼�
        /// </summary>
        //public new event System.EventHandler FocusLost;

        protected Neusoft.HISFC.Models.Order.OutPatient.Order myorder = null;
        protected bool dirty;
        protected bool isUndrugShowFrequency = true;
        public bool IsNew = true;
        private bool qtyChanged = false;

        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        Dictionary<string, Neusoft.FrameWork.Models.NeuObject> dictDept = new Dictionary<string, Neusoft.FrameWork.Models.NeuObject>();
        #endregion

        #region ����

        /// <summary>
        /// ��ǰOrder
        /// </summary>
        public Neusoft.HISFC.Models.Order.OutPatient.Order Order
        {
            get
            {
                this.GetOrder();
                return this.myorder;
            }
            set
            {
                if (value == null)
                    return;
                this.myorder = value;

                this.SetOrder();
            }
        }


        /// <summary>
        /// �Ƿ��ҩƷ��ʾƵ��
        /// </summary>
        public bool IsUndrugShowFrequency
        {
            get
            {
                return isUndrugShowFrequency;
            }
            set
            {
                isUndrugShowFrequency = value;
            }
        }

        #endregion

        #region ����
        protected void SetPanelVisible(int i)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            switch (i)
            {
                case 1:
                    this.panel1.Visible = true;
                    break;
                case 2:
                    this.panel2.Visible = true;
                    break;
                case 3:
                    this.panel3.Visible = true;
                    break;
            }
            this.panelFrequency.BringToFront();
            this.panelFrequency.Visible = true;
            this.txtDay.Visible = true;// {59C74550-5948-4321-A029-CB3CA6A822FD}
        }
        protected int GetVisiblePanel()
        {
            if (this.panel1.Visible)
                return 1;
            if (this.panel2.Visible)
                return 2;
            if (this.panel3.Visible)
                return 3;

            return 0;
        }
        private ComboBox MemoComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbMemo1;
                    case 2:
                        return this.cmbMemo2;
                    case 3:
                        return this.cmbMemo3;
                    default:
                        return null;
                }

            }

        }
        private Neusoft.FrameWork.WinForms.Controls.NeuComboBox UsageComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbUsage1;
                    case 2:
                        return this.cmbUsage2;
                    case 3:
                        return this.cmbUsage3;
                    default:
                        return null;
                }
            }
        }

        Neusoft.HISFC.BizProcess.Integrate.Manager interManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҽ��
        /// </summary>
        protected void SetOrder()
        {
            if (this.myorder.Item.ID == "999")
            {
                return;

            }

            dirty = true;
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}

            try
            {
                //this.qtyChanged = false;
                //ҩƷ
                //if (this.myorder.Item.IsPharmacy)
                //Ƶ�����¸���ֵ  �ܶ��ֶε�ֵ������....{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                this.myorder.Frequency = (Neusoft.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(this.myorder.Frequency.ID) as Neusoft.HISFC.Models.Order.Frequency).Clone();
                if (this.myorder.Item.ItemType == EnumItemType.Drug)
                {
                    Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();
                    Neusoft.HISFC.Models.Pharmacy.Item item = phaIntegrate.GetItem(this.myorder.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("����ҽ��������ҩƷʧ��");
                        return;
                    }
                    (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).MinUnit = item.MinUnit;
                    (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).PackUnit = item.PackUnit;
                    (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).SplitType = item.SplitType;
                    #region ��ҩ
                    //��ҩ
                    if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                    {
                        if (this.GetVisiblePanel() != 2)
                            this.SetPanelVisible(2);
                        if (this.myorder.HerbalQty > 0)
                        {
                            this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            //this.myorderTerm.DoseOnce = this.myorderTerm.Item.Qty / this.myorderTerm.HerbalQty;
                        }
                        else
                        {
                            this.myorder.HerbalQty = 1;
                            this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            //this.myorderTerm.DoseOnce = this.myorderTerm.Item.Qty;
                        }
                        this.cmbMemo2.Text = this.myorder.Memo;
                        //��ҩҽ����Ϣ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                        this.txtDoseOnce1.Text = this.myorder.DoseOnce.ToString();
                        //this.cmbSpeUsage.Tag = this.myorder.HerbalUsage.ID;
                        this.cmbDoseUnit2.Items.Clear();
                        ArrayList alDoseUnit2 = new ArrayList();
                        alDoseUnit2.Add(new Neusoft.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));                        
                        this.cmbDoseUnit2.AddItems(alDoseUnit2);
                        this.cmbDoseUnit2.Tag = ((Neusoft.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;
                    }
                    #endregion
                    #region ��ҩ

                    else//��ҩ���г�ҩ
                    {
                        if (this.GetVisiblePanel() != 1)
                            this.SetPanelVisible(1);
                        if (this.IsNew)
                        {
                            if ((this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).OnceDose == 0M)//һ�μ���Ϊ�㣬Ĭ����ʾ��������
                                this.txtDoseOnce.Text = (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                            else
                                this.txtDoseOnce.Text = (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).OnceDose.ToString();

                            //this.txtMinUnit.Text = (this.myorderTerm.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit;
                            this.cmbDoseUnit2.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            alDoseUnit1.Add(new Neusoft.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            try
                            {
                                if (!string.IsNullOrEmpty(item.ExtendData2) && Neusoft.FrameWork.Function.NConvert.ToDecimal(item.ExtendData1) > 0)
                                {
                                    alDoseUnit1.Add(new Neusoft.FrameWork.Models.NeuObject(item.ExtendData2, item.ExtendData2, ""));
                                }
                            }
                            catch
                            {
                            }
                            this.cmbDoseUnit1.AddItems(alDoseUnit1);
                            this.cmbDoseUnit1.SelectedIndexChanged -= new EventHandler(cmbDoseUnit1_SelectedIndexChanged);
                            this.cmbDoseUnit1.Tag = ((Neusoft.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;
                            this.cmbDoseUnit1.SelectedIndexChanged += new EventHandler(cmbDoseUnit1_SelectedIndexChanged);

                            this.myorder.DoseOnce = decimal.Parse(this.txtDoseOnce.Text);
                            this.txtDay.Value = this.myorder.HerbalQty == 0 ? 1 : this.myorder.HerbalQty;
                         
                        }
                        else
                        {

                            this.txtDoseOnce.Text = this.myorder.DoseOnce.ToString();
                            //this.txtMinUnit.Text = (this.myorderTerm.Item as Neusoft.HISFC.Models.Pharmacy.Item).DoseUnit;
                            this.cmbDoseUnit1.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            alDoseUnit1.Add(new Neusoft.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            try
                            {
                                if (!string.IsNullOrEmpty(item.ExtendData2) && Neusoft.FrameWork.Function.NConvert.ToDecimal(item.ExtendData1) > 0)
                                {
                                    alDoseUnit1.Add(new Neusoft.FrameWork.Models.NeuObject(item.ExtendData2, item.ExtendData2, ""));
                                }
                            }
                            catch
                            {
                            }
                            this.cmbDoseUnit1.AddItems(alDoseUnit1);
                            this.cmbDoseUnit1.SelectedIndexChanged -= new EventHandler(cmbDoseUnit1_SelectedIndexChanged);
                            this.cmbDoseUnit1.Tag = string.IsNullOrEmpty(myorder.DoseUnit) ? ((Neusoft.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : myorder.DoseUnit;
                            this.cmbDoseUnit1.SelectedIndexChanged += new EventHandler(cmbDoseUnit1_SelectedIndexChanged);

                            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}��������������λ
                            this.txtDay.Value = this.myorder.HerbalQty;
                            ArrayList alUnits = new ArrayList();
                            //������Բ�ְ�װ��λ  ����ʾ��С��λ  ����ֻ��ʾ��װ��λ  
                            //0���ﰴ��С��λ����ȡ��
                            //1���ﰴ��С��λÿ��ȡ��
                            //2���ﰴ��װ��λ����ȡ��
                            //3���ﰴ��װ��λÿ��ȡ��
                            if ((this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).SplitType == "0" || (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).SplitType == "1")
                            {
                                alUnits.Add(new Neusoft.FrameWork.Models.NeuObject((this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            }
                            alUnits.Add(new Neusoft.FrameWork.Models.NeuObject((this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).PackUnit, (this.myorder.Item as Neusoft.HISFC.Models.Pharmacy.Item).PackUnit, ""));

                            this.cmbUsage1.SelectedIndexChanged -= new EventHandler(cmbUsage1_SelectedIndexChanged);
                            this.cmbUsage1.Tag = this.myorder.Usage.ID;
                            this.cmbUsage1.SelectedIndexChanged += new EventHandler(cmbUsage1_SelectedIndexChanged);
                            this.cmbUsage3.SelectedIndexChanged -= new EventHandler(cmbUsage3_SelectedIndexChanged);
                            this.cmbUsage3.Tag = this.myorder.Usage.ID;
                            this.cmbUsage3.SelectedIndexChanged += new EventHandler(cmbUsage3_SelectedIndexChanged);
                        }
                        //�ɲ����Ա༭ÿ��������λ
                        //if (this.myorderTerm.Item.ID == "999")
                        //    this.txtMinUnit.Enabled = true;
                        //else
                        //    this.txtMinUnit.Enabled = false;
                        this.cmbMemo1.Text = this.myorder.Memo;
                        this.chkDrugEmerce.Checked = this.myorder.IsEmergency;
                    }
                    #endregion
                }
                else//��ҩƷ-����ҽ��
                {
                    if (this.GetVisiblePanel() != 3)
                        this.SetPanelVisible(3);

                    this.cmbExeDept.Visible = true;
                    neuLabel7.Visible = true;

                    this.panelFrequency.Visible = false;//����ҽ�������ǲ�����ʾ��ҩƷƵ��
                    this.txtDay.Visible = false; //{59C74550-5948-4321-A029-CB3CA6A822FD}

                    //ִ�п���
                    if (myorder.ExeDept.ID != null && myorder.ExeDept.ID != "")
                    {
                        this.cmbExeDept.Tag = this.myorder.ExeDept.ID;
                    }
                    else
                    {
                        if (myorder.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug))
                        {
                            Neusoft.HISFC.Models.Fee.Item.Undrug undrug = myorder.Item as Neusoft.HISFC.Models.Fee.Item.Undrug;
                            this.cmbExeDept.Tag = undrug.ExecDept;
                        }
                    }

                    #region {0A4BC81A-2F2B-4dae-A8E6-C8DC1F87AA32}
                    if (this.myorder.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.txtSample.ClearItems();
                        this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperSample.ArrayObject);
                        this.neuLabel9.Text = "����:";
                        this.txtSample.Text = this.myorder.Sample.Name;
                    }
                    else
                    {
                        this.txtSample.ClearItems();
                        this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperCheckPart.ArrayObject);
                        this.neuLabel9.Text = "��λ:";
                        if (string.IsNullOrEmpty(this.myorder.CheckPartRecord))
                        {
                            this.txtSample.Text = this.myorder.Sample.Name;
                        }
                        else
                        {
                            this.txtSample.Text = this.myorder.CheckPartRecord;
                        }
                    }
                    #endregion

                    this.cmbMemo3.Text = this.myorder.Memo;
                    this.chkEmerce.Checked = this.myorder.IsEmergency;

                    //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                    //this.cmbCheckBody.Tag = this.myorder.CheckPartRecord;
                }

            }
            catch
            {
            };

            if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
            {
                this.cmbFrequency.SelectedIndexChanged -= new EventHandler(cmbFrequency_SelectedIndexChanged);
                this.cmbFrequency.Tag = this.myorder.Frequency.ID;
                this.cmbFrequency.SelectedIndexChanged += new EventHandler(cmbFrequency_SelectedIndexChanged);

                this.myorder.Frequency.Time = ((Neusoft.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Time;

                this.lnkTime.Text = this.myorder.Frequency.Time;
            }
            this.cmbUsage1.SelectedIndexChanged -=new EventHandler(cmbUsage1_SelectedIndexChanged);
            this.cmbUsage1.Tag = this.myorder.Usage.ID;
            this.cmbUsage1.SelectedIndexChanged += new EventHandler(cmbUsage1_SelectedIndexChanged);

            this.cmbUsage2.SelectedIndexChanged -= new EventHandler(this.Mouse_Leave);
            this.cmbUsage2.Tag = this.myorder.Usage.ID;
            this.cmbUsage2.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            this.cmbUsage3.SelectedIndexChanged -=new EventHandler(cmbUsage3_SelectedIndexChanged);
            this.cmbUsage3.Tag = this.myorder.Usage.ID;
            this.cmbUsage3.SelectedIndexChanged += new EventHandler(cmbUsage3_SelectedIndexChanged);
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            dirty = false;
        }

        /// <summary>
        /// ���ݷ�ҩƷ���������Ȩ�޵�Ĭ��ִ�п��� zhao.chf
        /// </summary>
        /// <param name="undrugCode"></param>
        public ArrayList AutoLoadDefaultDept(string undrugCode)
        {
            //DongGuan.HISFC.Manager.DefaultDept defaultDeptSettingMgr = new DongGuan.HISFC.Manager.DefaultDept();
            //ArrayList al = defaultDeptSettingMgr.GetDefaultDept(undrugCode);
            //if (al == null)
            //{
            //    MessageBox.Show(defaultDeptSettingMgr.Err);
            //    return null;
            //}

            //return al;
            return null;
        }

        /// <summary>
        /// �����Ŀ��Ϣ
        /// </summary>
        protected virtual void GetOrder()
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;
            if (UsageComboBox == null || this.UsageComboBox.SelectedItem == null)
            {
                this.myorder.Usage.ID = "";
                this.myorder.Usage.Name = "";
            }
            else
            {
                this.myorder.Usage.ID = this.UsageComboBox.SelectedItem.ID;
                this.myorder.Usage.Name = this.UsageComboBox.SelectedItem.Name;
            }
            if (this.cmbFrequency.SelectedItem == null)
            {
                this.myorder.Frequency.ID = "";
                this.myorder.Frequency.Name = "";
                this.myorder.Frequency.Time = "";
            }
            else
            {
                this.myorder.Frequency.ID = this.cmbFrequency.SelectedItem.ID;
                this.myorder.Frequency.Name = this.cmbFrequency.SelectedItem.Name;
                this.myorder.Frequency.Time = this.lnkTime.Text;
            }

            switch (this.GetVisiblePanel())
            {
                case 1://��
                    try
                    {
                        this.myorder.DoseOnce = decimal.Parse(this.txtDoseOnce.Text);
                    }
                    catch
                    {
                        MessageBox.Show("ÿ���������벻��ȷ!", "��ʾ");
                        return;
                    }
                    ((Neusoft.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = (this.cmbDoseUnit1.Text);
                    this.myorder.Memo = Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemo1.Text);
                    this.myorder.IsEmergency = this.chkDrugEmerce.Checked;

                    //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}������Ժ��ע�����
                    this.myorder.HerbalQty = txtDay.Value;

                    break;
                case 2://��
                    this.myorder.HerbalQty = decimal.Parse(this.txtFu.Text);

                    this.myorder.Memo = Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemo2.Text);
                    this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce1.Text);
                    if (this.cmbSpeUsage.Tag != null)
                    {
                        //this.myorder.HerbalUsage.ID = this.cmbSpeUsage.Tag.ToString();
                    }
                    ((Neusoft.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = this.cmbDoseUnit2.Text;


                    break;
                case 3://��
                    if (this.cmbExeDept.Tag != null)
                    {
                        this.myorder.ExeDept.ID = this.cmbExeDept.Tag.ToString();
                        this.myorder.ExeDept.Name = this.cmbExeDept.Text;
                    }
                    this.myorder.Memo = Neusoft.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemo3.Text);
                    this.myorder.IsEmergency = this.chkEmerce.Checked;
                    this.myorder.Sample.Name = this.txtSample.Text;
                    if (this.txtSample.Tag != null)
                        this.myorder.Sample.ID = this.txtSample.Tag.ToString();
                    //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                    //if (this.cmbCheckBody.Tag != null)
                    //{
                    //    this.myorder.CheckPartRecord = this.cmbCheckBody.Tag.ToString();
                    //}
                    break;
                default:
                    break;
            }


        }
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            if (Neusoft.FrameWork.Management.Connection.Operator.ID == "")
                return;
            if (DesignMode == false)
            {
                ArrayList al1 = new ArrayList();
                try
                {
                    al1 = this.interManager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.ORDERMEMO);
                }
                catch
                {
                    return;
                }
                this.cmbMemo1.AddItems(al1);
                this.cmbMemo2.AddItems(al1);
                this.cmbMemo3.AddItems(al1);
                //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}��ҩ�����÷�
                ArrayList alSpeUsage = new ArrayList();
                alSpeUsage = interManager.GetConstantList("SPEUSAGE");
                if (alSpeUsage != null && alSpeUsage.Count > 0)
                {
                    this.cmbSpeUsage.AddItems(alSpeUsage);
                }
            }
            //ѡ��ĳ����Ŀʱ�������ת������ houwb
            //this.Leave += new EventHandler(ucOrderTermInputByTypeNew_Leave);
        }

        void ucOrderTermInputByTypeNew_Leave(object sender, EventArgs e)
        {
            if (FocusLost != null)
            {
                FocusLost(sender, e);
            }
        }

        /// <summary>
        /// �ı�����ת
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                this.GetOrder();
                //if (((Control)sender).Name == "cmbMemo1")
                //{
                //    //this.txtDrugQty.Focus();
                //}
                //else 
                if (((Control)sender).Name.Length > 7 && ((Control)sender).Name.Substring(0, 7) == "cmbMemo")
                {
                    if (this.FocusLost != null)
                        this.FocusLost(sender, null);
                }
                else if (((Control)sender).Name == "cmbExeDept" && this.isUndrugShowFrequency == false)
                {
                    if (this.FocusLost != null)
                        this.FocusLost(sender, null);
                }
                else if ((sender == this.txtDoseOnce && this.cmbDoseUnit1.Enabled == false)
                    || sender == this.cmbDoseUnit1
                    || sender == this.txtFu //��ҩ����ת��Ƶ����??
                    )
                {
                    this.cmbFrequency.Focus();
                }
                else if (sender == this.txtDoseOnce1)
                {
                    this.txtFu.Focus();
                    this.txtFu.SelectAll();
                }
                else if (sender == this.cmbExeDept)
                {
                    this.cmbMemo3.Focus();
                }
                else if (sender == this.cmbFrequency)
                {
                    switch (this.GetVisiblePanel())
                    {
                        case 1:
                            //{59C74550-5948-4321-A029-CB3CA6A822FD}
                            this.txtDay.Focus();
                            this.txtDay.Select(0, this.txtDay.Value.ToString().Length);
                            //this.cmbUsage1.Focus();
                            break;
                        case 2:
                            this.cmbUsage2.Focus();
                            break;
                        default:
                            if (this.FocusLost != null)
                                this.FocusLost(sender, null);
                            break;
                    }
                }
                //{59C74550-5948-4321-A029-CB3CA6A822FD}
                else if (sender == this.txtDay)
                {
                    this.cmbUsage1.Focus();
                }
                else if (sender == this.txtDoseOnce && this.cmbDoseUnit1.Enabled == true)
                {
                    this.cmbDoseUnit1.Focus();
                }
                else
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
                }
                e.Handled = true;

            }
        }

        private void cmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            string time = "";
            if (myorder == null)
            {
                MessageBox.Show("����ѡ����Ŀ��");
                return;
            }
            dirty = true;
            if (this.myorder.Frequency.ID == this.cmbFrequency.SelectedItem.ID)
                time = this.myorder.Frequency.Time;//���Ƶ��ʱ���,��ȻҲ������IsNew������
            this.myorder.Frequency = ((Neusoft.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
            if (time != "")
                this.myorder.Frequency.Time = time;//����ʱ���
            //this.txtFrequency.Text = this.myorderTerm.Frequency.Name;
            this.lnkTime.Text = this.myorder.Frequency.Time;

            this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}

            #region addby xuewj 2009-10-24 Ƶ���޸�ʱ�޸�Ժע����

            this.CalculateInjNum();

            #endregion

            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.Frequency);
            }
            dirty = false;

        }

        void CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            if (this.myorder == null)
                return;
            switch (this.GetVisiblePanel())
            {
                case 1:
                    this.myorder.IsEmergency = this.chkDrugEmerce.Checked;
                    break;
                case 2:
                    this.myorder.IsEmergency = false;
                    break;
                default:
                    this.myorder.IsEmergency = this.chkEmerce.Checked;
                    break;

            }
            if (this.ItemSelected != null)
                this.ItemSelected(this.myorder, EnumOrderFieldList.Emc);
        }

        void Mouse_Leave(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            if (this.dirty)
                return;
            if (this.myorder == null)
                return;
            switch (((Control)sender).Name)
            {
                case "txtDoseOnce":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.DoseOnce);
                    }

                    break;
                case "txtFrequency":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Frequency);
                    }
                    break;
                case "txtFu":
                    this.Order.HerbalQty = decimal.Parse(this.txtFu.Text);
                    this.CalculateTotal();
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Fu);
                    }

                    break;
                case "txtDay"://{59C74550-5948-4321-A029-CB3CA6A822FD}
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Day);
                    }

                    break;
                //case "txtMinUnit":
                //    if (this.ItemSelected != null)
                //    {
                //        this.ItemSelected(this.OrderTerm, EnumOrderTermFieldList.MinUnit);
                //    }

                //    break;
                case "txtSample":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Sample);
                    }

                    break;
                case "cmbExeDept":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.ExeDept);
                    }

                    break;
                //CheckBody{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                case "cmbCheckBody":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.CheckBody);
                    }

                    break;
                case "cmbMemo1":
                    if (this.cmbMemo1.Text.Contains("Ƥ��"))
                    {
                        this.myorder.HypoTest = 2;
                    }
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                    }

                    break;
                case "cmbMemo2":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                    }

                    break;
                case "cmbMemo3":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                    }

                    break;
                case "cmbUsage1":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                    }
                    break;
                case "cmbUsage2":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                    }
                    break;
                //�����仯{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                case "txtDrugQty":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.DrugQty);
                    }
                    break;
                //��λ�仯{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                case "cmbDrugUnit":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.DrugUnit);
                    }
                    break;
                //Ժ��ע������ı�{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                case "txtOutInjTimes":
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.InjNum);
                    }
                    break;
                case "cmbSpeUsage":
                    {
                        if (this.ItemSelected != null)
                        {
                            this.ItemSelected(this.Order, EnumOrderFieldList.SpeUsage);
                        }
                        break;
                    }
                default:
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Item);
                    }
                    break;
            }

        }
        #endregion

        #region	"��������"
        /// <summary>
        /// ���
        /// </summary>
        public virtual void Clear()
        {
            this.IsNew = true;
            this.myorder = null;
            this.txtDoseOnce.Text = "0";				//ÿ������
            this.cmbDoseUnit1.Tag = "";				//ÿ��������λ
            this.cmbMemo1.Text = "";				//��ע
            this.cmbMemo2.Text = "";
            this.cmbMemo3.Text = "";
            this.txtFu.Text = "0";					//����
            this.cmbExeDept.Text = "";				//ִ�п���
            this.chkEmerce.Checked = false;			//�Ӽ�
            this.chkDrugEmerce.Checked = false;		//�Ӽ�
            this.txtSample.Text = "";
            this.cmbFrequency.Tag = "";
            this.cmbFrequency.Text = "";
            //this.txtFrequency.Text = "";
            this.cmbUsage1.Text = "";
            this.cmbUsage1.Tag = "";
            this.cmbUsage2.Text = "";
            this.cmbUsage2.Tag = "";
            this.cmbUsage3.Text = "";
            this.cmbUsage3.Tag = "";


            this.txtDay.Value = 1;
            //this.cmbCheckBody.Tag = "";
            this.IsNew = false;
            this.qtyChanged = false;
            if (myorder != null)
            {
                myorder.Item.User02 = qtyChanged ? "1" : "0";
            }
        }

        /// <summary>
        /// ������ת
        /// </summary>
        public void SetShortKey()
        {
            if (this.txtDoseOnce.Focused || this.txtFu.Focused || this.cmbExeDept.Focused)
            {
                if (this.FocusLost != null)
                    this.FocusLost(null, null);
            }
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        /// <param name="alFrequency"></param>
        /// <param name="alDept"></param>
        public virtual void InitControl(ArrayList alFrequency, ArrayList alDept, ArrayList alUsage)
        {
            try
            {
                if (alDept == null)
                {
                    alDept = interManager.GetDepartment();
                }
                this.cmbExeDept.AddItems(alDept);
                this.cmbExeDept.IsListOnly = true;

                if (alFrequency == null)
                {
                    alFrequency = Neusoft.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject;
                }

                this.cmbFrequency.ShowID = true;
                this.cmbFrequency.AddItems(alFrequency);
                this.cmbFrequency.IsListOnly = true;

                //��ʼ���÷�
                this.cmbUsage1.AddItems(Neusoft.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject);
                this.cmbUsage2.AddItems(Neusoft.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject);
                this.cmbUsage3.AddItems(Neusoft.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject);

                //��ҩʹ�õ������÷�{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                //ArrayList alHerbalUsage = interManager.QueryConstantList("HERBALUSAGE");
                //if (alHerbalUsage != null && alHerbalUsage.Count > 0)
                //{
                //    this.cmbUsage2.AddItems(alHerbalUsage);
                //}

                this.cmbUsage1.IsListOnly = true;
                this.cmbUsage2.IsListOnly = true;
                this.cmbUsage3.IsListOnly = true;
                //��ʼ����������
                this.txtSample.AddItems(interManager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.LABSAMPLE));
                this.txtSample.AppendItems(interManager.GetConstantList("CHECKPART"));
                //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                //this.cmbCheckBody.AddItems(interManager.GetConstantList("CHECKBODY"));

                //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                ArrayList deptList = this.feeIntegrate.QueryDeptList("ALL", "1");
                foreach (Neusoft.FrameWork.Models.NeuObject neuObj in deptList)
                {
                    dictDept.Add(neuObj.Memo + "|" + neuObj.ID, neuObj);
                }
            }
            catch
            {
            }
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            this.txtDay.KeyPress += new KeyPressEventHandler(txtDay_KeyPress);
            this.txtDay.Leave += new EventHandler(txtDay_Leave);
            this.txtFu.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage2.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage3.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbDoseUnit1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo2.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo3.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            //��������뿪��ı�עˢ��
            this.cmbMemo1.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemo2.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemo3.TextChanged += new EventHandler(Mouse_Leave);
            this.txtSample.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbExeDept.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsage1.SelectedIndexChanged += new EventHandler(cmbUsage1_SelectedIndexChanged);
            this.cmbUsage2.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsage3.SelectedIndexChanged += new EventHandler(cmbUsage3_SelectedIndexChanged);
            this.cmbMemo1.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemo2.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbFrequency.SelectedIndexChanged += new EventHandler(cmbFrequency_SelectedIndexChanged);
            this.cmbMemo3.TextChanged += new EventHandler(Mouse_Leave);
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}            
            this.txtDoseOnce.KeyPress += new KeyPressEventHandler(txtDoseOnce_KeyPress);
            this.cmbDoseUnit1.SelectedIndexChanged += new EventHandler(cmbDoseUnit1_SelectedIndexChanged);
            this.txtFu.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbExeDept.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.chkEmerce.CheckedChanged += new EventHandler(CheckedChanged);
            this.chkDrugEmerce.CheckedChanged += new EventHandler(CheckedChanged);
            this.txtDoseOnce.Leave += new EventHandler(txtDoseOnce_Leave);

            this.txtDoseOnce1.KeyPress += new KeyPressEventHandler(txtDoseOnce1_KeyPress);
            this.cmbSpeUsage.SelectedIndexChanged += new EventHandler(Mouse_Leave);
        }

        void cmbDoseUnit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            if (myorder == null)
            {
                return;
            }
            this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
            this.myorder.DoseUnit = this.cmbDoseUnit1.Text;
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.MinUnit);
            }
        }

        void txtDoseOnce_Leave(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            if (myorder == null)
            {
                return;
            }
            this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
            this.myorder.DoseOnce = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce.Text);
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
            }
        }

        void txtDay_Leave(object sender, EventArgs e)
        {
            if (this.IsNew)
                return;
            if (myorder == null)
            {
                return;
            }
            this.Order.HerbalQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Value);
            this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
            #region addby xuewj 2009-10-24  �޸�����ʱͬ��Ժע

            this.CalculateInjNum();

            #endregion
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.Day);
            }
        }

        void txtDoseOnce1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.IsNew)
                    return;
                if (myorder == null)
                {
                    return;
                }
                if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce1.Text);
                    this.myorder.Qty = Math.Round(this.myorder.HerbalQty * this.myorder.DoseOnce / ((HISFC.Models.Pharmacy.Item)this.myorder.Item).BaseDose, 2);
                }
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }
                this.txtFu.Focus();
            }
        }

        void cmbUsage1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myorder == null)
            {
                return;
            }
            this.CalculateInjNum();//{59C74550-5948-4321-A029-CB3CA6A822FD}
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.Usage);
            }
        }

        void cmbUsage3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myorder == null)
            {
                return;
            }

            this.myorder.Usage.ID = this.cmbUsage3.Tag.ToString();
            this.myorder.Usage.Name = this.cmbUsage3.Text;

            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, EnumOrderFieldList.Usage);
            }
        }

        void cmbDrugUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (myorder == null)
                {
                    return;
                }
                Mouse_Leave(sender, e);
                if (this.FocusLost != null)
                {
                    this.FocusLost(sender, e);
                }
            }
        }

        void txtDoseOnce_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.IsNew)
                    return;
                if (myorder == null)
                {
                    return;
                }
                this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
                this.myorder.DoseOnce = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce.Text);
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }
                if (this.cmbDoseUnit1.Items != null && this.cmbDoseUnit1.Items.Count > 1)
                {
                    this.cmbDoseUnit1.Focus();
                }
                else
                {
                    this.cmbFrequency.Focus();
                }
            }
        }

        void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.IsNew)
                    return;
                if (myorder == null)
                {
                    return;
                }
                this.txtDay.Leave -= new EventHandler(txtDay_Leave);
                this.Order.HerbalQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Value);
                this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
                #region addby xuewj 2009-10-24  �޸�����ʱͬ��Ժע

                this.CalculateInjNum();

                #endregion
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Day);
                }
                this.cmbUsage1.Focus();
                this.txtDay.Leave += new EventHandler(txtDay_Leave);
            }
        }
        /// <summary>
        /// ���ý���
        /// </summary>
        public new void Focus()
        {
            qtyChanged = false;
            if (myorder != null)
            {
                myorder.Item.User02 = qtyChanged ? "1" : "0";
            }
            switch (GetVisiblePanel())
            {
                case 1:
                    this.txtDoseOnce.Focus();
                    this.txtDoseOnce.SelectAll();
                    break;
                case 2:
                    this.txtDoseOnce1.Focus();
                    this.txtDoseOnce1.SelectAll();
                    break;
                case 3:
                    this.cmbMemo3.Focus();
                    this.cmbMemo3.SelectAll();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ���ݷ�ҪƷ�����ȡ��ҩƷִ�п���{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        /// </summary>
        /// <param name="list"></param>
        public string SetExecDept(string itemID)
        {
            string id = string.Empty;
            ArrayList undrugDeptList = new ArrayList();
            if (dictDept.Count != 0)
            {
                foreach (string itemCode in dictDept.Keys)
                {
                    if (itemCode.Contains(itemID))
                    {
                        undrugDeptList.Add(dictDept[itemCode]);
                        if (dictDept[itemCode].User02 == "1")
                        {
                            id = dictDept[itemCode].ID;
                        }
                    }
                }
                if (undrugDeptList.Count != 0)
                {
                    this.cmbExeDept.AddItems(undrugDeptList);
                }
                else
                {
                    this.cmbExeDept.AddItems(deptAll);
                }
            }
            return id;
        }

        #endregion

        /// <summary>
        /// ʱ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (this.myorder == null || this.myorder.Frequency == null || this.myorder.Frequency.Times.Length > 5)
                return;

            Neusoft.HISFC.Components.Order.Forms.frmSpecialFrequency f = new Neusoft.HISFC.Components.Order.Forms.frmSpecialFrequency();

            f.IsDoseCanModified = false;//����ҽ�� �����޸�����Ƶ�εļ���

            f.Frequency = this.myorder.Frequency.Clone();

            if (this.myorder.ExecDose == "")
                f.Dose = this.myorder.DoseOnce.ToString();
            else
                f.Dose = this.myorder.ExecDose;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.myorder.Frequency = f.Frequency.Clone();
                if (f.Dose.IndexOf("-") > 0)
                {
                    this.myorder.ExecDose = f.Dose;
                    this.myorder.Memo = "ʱ�䣺" + f.Frequency.Time + " ������" + f.Dose;
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.Memo);
                }
                else
                {
                    this.myorder.DoseOnce = Neusoft.FrameWork.Function.NConvert.ToDecimal(f.Dose);
                    this.myorder.ExecDose = "";
                    this.myorder.Memo = "";
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.Memo);
                }
                if (this.ItemSelected != null)
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Frequency);
            }
        }
        /// <summary>
        /// �Զ��������� add by  {59C74550-5948-4321-A029-CB3CA6A822FD}
        /// </summary>
        private void CalculateTotal()
        {
            if (myorder == null)
            {
                return;
            }

            if (myorder.Item.SysClass.ID.ToString() == "M")
            {
                return;
            }

            //��Ϊ�޸Ĺ������󣬲��ټ���
            if (isQtyChanged)
            {
                return;
            }


            dirty = true;
            //��ҩ����
            decimal Days = 0;
            Days = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Value);
            //���û����������ʱ�򲻴���
            if (Days <= 0)
            {
                return;
            }

            //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
            if (Classes.Function.ReComputeQty(this.Order) == -1)
            {
                return;
            }

            //ҩƷ�����͵�λ
            ArrayList alUnits = new ArrayList();
            Neusoft.HISFC.Models.Pharmacy.Item phaItem = this.Order.Item as Neusoft.HISFC.Models.Pharmacy.Item;
            alUnits.Add(new Neusoft.FrameWork.Models.NeuObject(phaItem.PackUnit, phaItem.PackUnit, ""));
            if (phaItem.SplitType == "0" || phaItem.SplitType == "1")
            {
                alUnits.Add(new Neusoft.FrameWork.Models.NeuObject(phaItem.MinUnit, phaItem.MinUnit, ""));
            }

            dirty = false;
        }

        /// <summary>
        /// �Զ�����Ժע����
        /// </summary>
        private void CalculateInjNum()
        {
            if (myorder == null)
            {
                return;
            }
            dirty = true;
            decimal Frequence = 0;
            if (this.cmbFrequency.SelectedItem != null)
            {
                this.myorder.Frequency = ((Neusoft.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
            }

            if (this.myorder.Frequency.Days[0] == "0" || string.IsNullOrEmpty(this.myorder.Frequency.Days[0]))
            {
                this.myorder.Frequency.Days[0] = "1";
                Frequence = this.myorder.Frequency.Times.Length;
            }
            else
            {
                try
                {
                    Frequence = Math.Round(this.myorder.Frequency.Times.Length / Neusoft.FrameWork.Function.NConvert.ToDecimal(this.myorder.Frequency.Days[0]), 2);
                }
                catch
                {
                    Frequence = this.myorder.Frequency.Times.Length;
                }
            }
            //�÷�
            if (this.cmbUsage1.Tag != null)
            {
                this.myorder.Usage.ID = this.cmbUsage1.Tag.ToString();
                this.myorder.Usage.Name = this.cmbUsage1.Text;
            }
            //Ժע����
            if (this.cmbUsage1.Tag != null && Classes.Function.hsUsageAndSub.Contains(cmbUsage1.Tag.ToString()))
            {
                this.myorder.InjectCount = (int)Math.Ceiling((double)(Frequence * this.myorder.HerbalQty));
            }
            else
            {
                this.myorder.InjectCount = 0;
            }
            dirty = false;
        }

        /// <summary>
        /// ����ҩƷ������Լ������������ܵ�λ
        /// </summary>
        /// <param name="itm">ҩƷ��Ŀ</param>
        /// <param name="DoseOnce">ÿ������</param>
        /// <param name="BaseDose">��������</param>
        /// <param name="Frequence">Ƶ��</param>
        /// <param name="Days">����</param>
        /// <param name="qty">����</param>
        /// <param name="unit">��λ</param>
        public static void SetQtyBySplitType(Neusoft.HISFC.Models.Pharmacy.Item itm, decimal DoseOnce, decimal BaseDose, decimal Frequence, decimal Days, ref decimal baseQty, ref string unit, ref string unitFlag)
        {
            //0���ﰴ��С��λ����ȡ��
            //1���ﰴ��С��λÿ��ȡ��
            //2���ﰴ��װ��λ����ȡ��
            //3���ﰴ��װ��λÿ��ȡ��
            switch (itm.SplitType)
            {
                case "0":
                    baseQty = Math.Ceiling(Math.Round(Math.Ceiling(DoseOnce / BaseDose * Frequence * Days),6));
                    unit = itm.MinUnit;
                    unitFlag = "1";
                    break;
                case "1":
                    baseQty = Math.Ceiling(Math.Round(Math.Ceiling(DoseOnce / BaseDose) * Frequence * Days,6));
                    unit = itm.MinUnit;
                    unitFlag = "1";
                    break;
                case "2":
                    baseQty = Math.Ceiling(Math.Round(((DoseOnce / BaseDose * Frequence * Days) / itm.PackQty),6));
                    unit = itm.PackUnit;
                    unitFlag = "0";
                    break;
                case "3":
                    baseQty = Math.Ceiling(Math.Round((System.Math.Ceiling((DoseOnce / BaseDose) / itm.PackQty)) * Frequence * Days,6));
                    unit = itm.PackUnit;
                    unitFlag = "0";
                    break;
                default:
                    baseQty = Math.Round(DoseOnce / BaseDose, 2) * Frequence * Days;
                    unit = itm.MinUnit;
                    unitFlag = "1";
                    break;
            }
        }
    }
}
