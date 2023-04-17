using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// ��Ŀѡ��仯
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="changedField"></param>
    public delegate void ItemSelectedDelegate(FS.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField);

    /// <summary>
    /// ������������������ʾ
    /// </summary>
    /// <param name="inOrder"></param>
    public delegate void SetQtyValue(FS.HISFC.Models.Order.OutPatient.Order outOrder);

    /// <summary>
    /// ҽ������ؼ�
    /// </summary>
    public partial class ucOrderInputByType : UserControl
    {
        public ucOrderInputByType()
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
        public event ItemSelectedDelegate ItemSelected;

        /// <summary>
        /// ��������
        /// </summary>
        public event SetQtyValue SetQtyValue;

        /// �ϸ���Ŀ������
        /// </summary>
        private decimal useDays = 1;

        /// <summary>
        /// �ϸ���Ŀ������
        /// </summary>
        public decimal UseDays
        {
            get
            {
                return useDays;
            }
            set
            {
                useDays = value;
            }
        }

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
                if (isQtyChanged)
                {
                    //Components.Order.Classes.Function.ShowBalloonTip(0, "��ʾ", "����Ϊ�޸������������Զ�����������", ToolTipIcon.Warning);
                }
                //Ϊ�˱�֤��ҩ��ȷ�˴����� 
                //isQtyChanged = false;
            }
        }

        /// <summary>
        /// ʧȥ����ʱ���¼�����
        /// </summary>
        /// <param name="obj"></param>
        public delegate void FocusLostHandler(object sender, EventArgs e);

        /// <summary>
        /// ʧȥ����ʱ���¼�
        /// ʧȥ����ʱ����Ŀ����ؼ���ȡ����
        /// </summary>
        public event FocusLostHandler FocusLost;

        /// <summary>
        /// ��ǰҽ����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order myorder = null;

        protected bool dirty;
        protected bool isUndrugShowFrequency = false;
        public bool IsNew = true;

        /// <summary>
        /// �����Ƿ���Ϊ�޸Ĺ�
        /// </summary>
        private bool qtyChanged = false;

        /// <summary>
        /// ��������ڶ���λ����Ŀ
        /// </summary>
        private Hashtable hsSecondUnitItem = new Hashtable();

        /// <summary>
        /// �Ƿ�����ҩƷ�����Կ�����С��λ��ÿ������
        /// </summary>
        private bool isAllDrugCanUseMinUnit = false;

        /// <summary>
        /// ÿ��������ģʽ 0 ԭʼģʽ��1 ÿ��������������������2 ֻ���������ʱ�š�ÿ����
        /// </summary>
        private int isDoseUnitInputMode = -1;

        /// <summary>
        /// Ĭ��ÿ������ʾ 0 ����������ʾ��1 ��С��λ������ʾ
        /// </summary>
        private string defultDoceOnceUnit = "0";

        #endregion

        #region ����

        /// <summary>
        /// ��ǰOrder
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order Order
        {
            get
            {
                this.GetOrder();
                return this.myorder;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
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
                this.neuLabel17.Visible = value;
                this.cmbUsageUndrug.Visible = value;
            }
        }

        #endregion

        #region ����


        /// <summary>
        /// �����¼�
        /// </summary>
        private void AddEvent()
        {
            DeleteEvent();

            //�÷�
            this.cmbUsageP.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsagePCC.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsageUndrug.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            //��ҩ�����÷�����ҩ��ʽ��
            this.cmbPCCSpeUsage.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //ÿ������
            this.txtDoseOnceP.TextChanged += new EventHandler(Mouse_Leave);
            //if (isDoseUnitInputMode != 1 && isDoseUnitInputMode != 2)
            //{
            //    this.txtDoseOnceP.TextChanged += new EventHandler(Mouse_Leave);
            //}
            this.txtDoseOncePCC.TextChanged += new EventHandler(Mouse_Leave);
            //this.cmbDoseUnitP.TextChanged += new EventHandler(Mouse_Leave);

            //ÿ��������λ
            this.cmbDoseUnitP.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbDoseUnitPCC.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //Ƶ��
            this.cmbFrequency.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbFrequencyUndrug.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //����/����
            this.txtDay.TextChanged += new EventHandler(Mouse_Leave);
            this.txtDaysUndrug.TextChanged += new EventHandler(Mouse_Leave);
            this.txtFu.TextChanged += new EventHandler(Mouse_Leave);

            //��ע
            this.cmbMemoP.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemoPCC.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemoUndrug.TextChanged += new EventHandler(Mouse_Leave);

            //��������
            this.txtSample.TextChanged += new EventHandler(Mouse_Leave);
            txtSample.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            //ִ�п���
            this.cmbExeDept.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //�Ӽ�
            this.chkEmerceUndrug.CheckedChanged += new EventHandler(Mouse_Leave);
            this.chkEmerceDrug.CheckedChanged += new EventHandler(Mouse_Leave);

            #region ItemKeyPress

            this.txtFu.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDay.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsagePCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbDoseUnitP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoPCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOncePCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnceP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbExeDept.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.txtDaysUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequencyUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            #endregion
        }
                                           

        /// <summary>
        /// ɾ���¼�
        /// </summary>
        private void DeleteEvent()
        {
            //�÷�
            this.cmbUsageP.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsagePCC.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsageUndrug.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            //��ҩ�����÷�����ҩ��ʽ��
            this.cmbPCCSpeUsage.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //ÿ������
            this.txtDoseOnceP.TextChanged -= new EventHandler(Mouse_Leave);
            //if (isDoseUnitInputMode != 1 && isDoseUnitInputMode != 2)
            //{
            //    this.txtDoseOnceP.TextChanged -= new EventHandler(Mouse_Leave);
            //}
            this.txtDoseOncePCC.TextChanged -= new EventHandler(Mouse_Leave);

            //ÿ��������λ
            this.cmbDoseUnitP.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbDoseUnitPCC.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //Ƶ��
            this.cmbFrequency.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbFrequencyUndrug.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //����/����
            this.txtDay.TextChanged -= new EventHandler(Mouse_Leave);
            this.txtDaysUndrug.TextChanged -= new EventHandler(Mouse_Leave);
            this.txtFu.TextChanged -= new EventHandler(Mouse_Leave);

            //��ע
            this.cmbMemoP.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemoPCC.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemoUndrug.TextChanged -= new EventHandler(Mouse_Leave);

            //��������
            this.txtSample.TextChanged -= new EventHandler(Mouse_Leave);
            txtSample.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            //ִ�п���
            this.cmbExeDept.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //�Ӽ�
            this.chkEmerceUndrug.CheckedChanged -= new EventHandler(Mouse_Leave);
            this.chkEmerceDrug.CheckedChanged -= new EventHandler(Mouse_Leave);

            #region ItemKeyPress

            this.txtFu.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDay.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsagePCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.cmbDoseUnitP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoPCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOncePCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnceP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbExeDept.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.txtDaysUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequencyUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            #endregion
        }

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
        }

        protected int GetVisiblePanel()
        {
            //������ʾƵ�κ�����
            if (this.myorder != null && this.myorder.Item != null && this.myorder.Item.SysClass.ID.ToString() == "UZ")
            {
                this.plFreq.Visible = true;
            }
            else
            {
                this.plFreq.Visible = false;
            }

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
                        return this.cmbMemoP;
                    case 2:
                        return this.cmbMemoPCC;
                    case 3:
                        return this.cmbMemoUndrug;
                    default:
                        return null;
                }
            }
        }

        private FS.FrameWork.WinForms.Controls.NeuComboBox UsageComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbUsageP;
                    case 2:
                        return this.cmbUsagePCC;
                    case 3:
                        return this.cmbUsageUndrug;
                    default:
                        return null;
                }
            }
        }

        #region Ĭ��ִ�п��ҽӿ�

        /// <summary>
        /// ����ִ�п����б�
        /// </summary>
        /// <param name="item"></param>
        /// <param name="execDeptCode"></param>
        private void SetExecDept(FS.HISFC.Models.Order.Order order, string execDeptCode, bool isNew)
        {
            string execDept = "";
            ArrayList alExecDept = null;

            Components.Order.Classes.Function.SetExecDept(true, order.ReciptDept.ID, order.Item.ID, execDeptCode, ref execDept, ref alExecDept);
            if (alExecDept == null
                || alExecDept.Count == 0)
            {
                alExecDept = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
            }

            cmbExeDept.AddItems(alExecDept);

            if (isNew)
            {
                cmbExeDept.Tag = execDept;
            }
            else
            {
                cmbExeDept.Tag = execDeptCode;
            }

            //try
            //{
            //    cmbExeDept.AddItems(Components.Order.Classes.Function.GetExecDepts(((FS.HISFC.Models.Base.Employee)CacheManager.InterMgr.Operator).Dept, order));
            //}
            //catch
            //{
            //    cmbExeDept.AddItems(SOC.HISFC.BizProcess.Cache.Common.GetValidDept());
            //}

            //string[] execDept = execDeptCode.Split('|');

            //try
            //{
            //    for (int k = 0; k < execDept.Length; k++)
            //    {
            //        if (!string.IsNullOrEmpty(execDept[k]))
            //        {
            //            execDeptCode = execDept[k];
            //            if (SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString() ==
            //                SOC.HISFC.BizProcess.Cache.Common.GetDept(execDept[k]).DeptType.ID.ToString())
            //            {
            //                execDeptCode = execDept[k];
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    execDeptCode = order.ReciptDept.ID;
            //}

            //cmbExeDept.Tag = execDeptCode;

            //if (string.IsNullOrEmpty(cmbExeDept.Text)
            //    || cmbExeDept.SelectedIndex == -1)
            //{
            //    if (cmbExeDept.alItems.Count > 0)
            //    {
            //        try
            //        {
            //            bool isRecipt = false;

            //            foreach (FS.FrameWork.Models.NeuObject obj in cmbExeDept.alItems)
            //            {
            //                if (obj.ID == execDeptCode)
            //                {
            //                    cmbExeDept.Tag = obj.ID;
            //                    isRecipt = false;
            //                    break;
            //                }
            //                if (obj.ID == order.ReciptDept.ID)
            //                {
            //                    isRecipt = true;
            //                }
            //            }

            //            if (isRecipt)
            //            {
            //                cmbExeDept.Tag = order.ReciptDept.ID;
            //            }
            //            else
            //            {
            //                string ReciptDeptTypeID = SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString();
            //                for (int i = 0; i < cmbExeDept.alItems.Count; i++)
            //                {
            //                    if (ReciptDeptTypeID == 
            //                        SOC.HISFC.BizProcess.Cache.Common.GetDept(((FS.FrameWork.Models.NeuObject)cmbExeDept.alItems[i]).ID).DeptType.ID.ToString())
            //                    {
            //                        cmbExeDept.SelectedIndex = i;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        catch
            //        {
            //        }

            //        if (cmbExeDept.SelectedIndex == -1)
            //        {
            //            cmbExeDept.SelectedIndex = 0;
            //        }
            //    }
            //}

            //if (cmbExeDept.Tag != null && !string.IsNullOrEmpty(cmbExeDept.Tag.ToString()))
            //{
            //    order.ExeDept.ID = cmbExeDept.Tag.ToString();
            //}
        }

        #endregion

        /// <summary>
        /// ����ҽ��
        /// </summary>
        protected void SetOrder()
        {
            dirty = true;
            try
            {
                this.DeleteEvent();
                cmbDoseUnitP.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbDoseUnitPCC.DropDownStyle = ComboBoxStyle.DropDownList;

                //Ƶ�����¸���ֵ  �ܶ��ֶε�ֵ������....{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                if (!string.IsNullOrEmpty(this.myorder.Frequency.ID) && !object.Equals(this.myorder.Frequency, null))
                {
                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(this.myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();
                }
                if (this.myorder.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item item = null;

                    if (myorder.Item.ID != "999")
                    {
                        item = CacheManager.PhaIntegrate.GetItem(this.myorder.Item.ID);
                        if (item == null)
                        {
                            MessageBox.Show("����ҽ��������ҩƷʧ��");
                            return;
                        }
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit = item.MinUnit;
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit = item.PackUnit;
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType = item.SplitType;
                    }

                    #region ��ҩ
                    //��ҩ
                    if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                    {
                        if (this.GetVisiblePanel() != 2)
                        {
                            this.SetPanelVisible(2);
                        }

                        this.cmbDoseUnitPCC.Items.Clear();
                        ArrayList alDoseUnit2 = new ArrayList();

                        if (this.myorder.Item.ID == "999")
                        {
                            cmbDoseUnitPCC.DropDownStyle = ComboBoxStyle.DropDown;
                            alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(myorder.DoseUnit, myorder.DoseUnit, ""));
                        }
                        else
                        {
                            alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));

                            if (isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//�����ж�
                            {
                                alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            }
                        }

                        this.cmbDoseUnitPCC.AddItems(alDoseUnit2);

                        if (this.IsNew)
                        {
                            if (this.myorder.Item.ID != "999")
                            {
                                if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)//һ�μ���Ϊ�㣬Ĭ����ʾ��������
                                {
                                    //add by liuww 2013-04-02
                                    //this.txtDoseOncePCC.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                                    this.txtDoseOncePCC.Text = "0";
                                }
                                else
                                {
                                    this.txtDoseOncePCC.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                                }

                                //Ĭ��Ƶ��
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID))
                                {
                                    this.cmbFrequency.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID;
                                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();

                                    if (myorder.Frequency != null)
                                    {
                                        this.lnkTime.Text = myorder.Frequency.Time;
                                    }
                                }

                                //Ĭ���÷�
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID))
                                {
                                    this.cmbUsagePCC.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID;
                                    this.myorder.Usage = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage;
                                }

                                this.myorder.DoseOnce = decimal.Parse(this.txtDoseOncePCC.Text);
                                this.myorder.DoseOnceDisplay = this.myorder.DoseOnce.ToString();
                                this.myorder.DoseUnit = this.cmbDoseUnitPCC.Text;

                                this.myorder.HerbalQty = 1;
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                        }
                        else
                        {
                            this.cmbDoseUnitPCC.Text = string.IsNullOrEmpty(myorder.DoseUnit) ? ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : this.myorder.DoseUnit;
                            this.txtDoseOnceP.Text = string.IsNullOrEmpty(this.myorder.DoseOnceDisplay.Trim()) ? this.myorder.DoseOnce.ToString() : this.myorder.DoseOnceDisplay;


                            if (this.myorder.HerbalQty > 0)
                            {
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                            else
                            {
                                this.myorder.HerbalQty = 1;
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                            this.cmbMemoPCC.Text = this.myorder.Memo;

                            this.txtDoseOncePCC.Text = this.myorder.DoseOnce.ToString();

                            this.cmbUsagePCC.Tag = this.myorder.Usage.ID;

                            if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                            {
                                this.cmbFrequency.Tag = myorder.Frequency.ID;

                                this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                            }
                        }
                    }
                    #endregion

                    #region ��ҩ
                    else//��ҩ���г�ҩ
                    {
                        if (this.GetVisiblePanel() != 1)
                        {
                            this.SetPanelVisible(1);
                        }

                        this.cmbDoseUnitP.Items.Clear();
                        ArrayList alDoseUnit1 = new ArrayList();

                        if (this.myorder.Item.ID == "999")
                        {
                            cmbDoseUnitP.DropDownStyle = ComboBoxStyle.DropDown;
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(myorder.DoseUnit, myorder.DoseUnit, ""));
                        }
                        else
                        {
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            if (this.isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//�����ж�
                            {
                                alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            }
                        }
                        this.cmbDoseUnitP.AddItems(alDoseUnit1);
                        //{557CD6B4-D471-4e6f-ADD8-A618F4DB0318} �¿��ĲŸ�ֵ�������¿��ľͲ���ֵ
                        if (!string.IsNullOrEmpty(item.OnceDoseUnit) && this.IsNew)
                        {
                            this.cmbDoseUnitP.Tag = item.OnceDoseUnit;
                            this.txtDoseOnceP.Text = item.OnceDose.ToString();
                            this.myorder.DoseUnit = item.OnceDoseUnit;
                        }

                        #region �����

                        if (this.IsNew)
                        {
                            if (myorder.Item.ID != "999")
                            {
                                //Ĭ��Ƶ��
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID))
                                {
                                    this.cmbFrequency.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID;
                                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();

                                    if ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem != null)
                                    {
                                        this.lnkTime.Text = myorder.Frequency.Time;
                                    }
                                }
                                //Ĭ���÷�
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID))
                                {
                                    this.cmbUsageP.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID;
                                    ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.Name = this.cmbUsageP.Text;
                                    this.myorder.Usage = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage;
                                }

                                // {4D67D981-6763-4ced-814E-430B518304E2}
                                if (string.IsNullOrEmpty(this.myorder.DoseUnit))
                                {
                                    //��������
                                    if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)//һ�μ���Ϊ�㣬Ĭ����ʾ��������
                                    {
                                        //add by liuww 
                                        //this.txtDoseOnceP.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                                        this.txtDoseOnceP.Text = "0";
                                        this.cmbDoseUnitP.Text = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;

                                        if (this.isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//�����ж�
                                        {
                                            if (defultDoceOnceUnit == "1")
                                            {
                                                this.txtDoseOnceP.Text = "1";
                                                this.cmbDoseUnitP.Tag = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).MinUnit;
                                                this.cmbDoseUnitP.Text = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).MinUnit;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.txtDoseOnceP.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                                    }
                                }
                            }
                            this.myorder.DoseOnce = decimal.Parse(this.txtDoseOnceP.Text);
                            this.myorder.DoseOnceDisplay = this.myorder.DoseOnce.ToString();
                            this.myorder.DoseUnit = this.cmbDoseUnitP.Text;
                            this.txtDay.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();
                            if (myorder.HerbalQty > 0)
                            {
                                this.useDays = myorder.HerbalQty;
                            }
                        }
                        #endregion

                        #region �޸�
                        else
                        {
                            this.txtDoseOnceP.Text = string.IsNullOrEmpty(this.myorder.DoseOnceDisplay.Trim()) ? this.myorder.DoseOnce.ToString() : this.myorder.DoseOnceDisplay;

                            this.cmbDoseUnitP.Text = string.IsNullOrEmpty(myorder.DoseUnit) ? ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : myorder.DoseUnit;
                            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}��������������λ
                            this.txtDay.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();

                            if (myorder.HerbalQty > 0)
                            {
                                this.useDays = myorder.HerbalQty;
                            }

                            //ò����������û�а�...

                            //ArrayList alUnits = new ArrayList();
                            ////������Բ�ְ�װ��λ  ����ʾ��С��λ  ����ֻ��ʾ��װ��λ  
                            ////0 ��С��λ����ȡ��" ���ݿ�ֵ 0
                            ////��װ��λ����ȡ��" ���ݿ�ֵ 1  �ڷ��ر����г�ҩ��������ҩ�϶�
                            ////��С��λÿ��ȡ��" ���ݿ�ֵ 2  ����϶�����
                            ////��װ��λÿ��ȡ��" ���ݿ�ֵ 3  ����û����   
                            //if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType == "0" || (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType == "2")
                            //{
                            //    alUnits.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            //}
                            //alUnits.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit, ""));
                            this.cmbUsageP.Tag = this.myorder.Usage.ID;

                            this.cmbMemoP.Text = this.myorder.Memo;
                            this.chkEmerceDrug.Checked = this.myorder.IsEmergency;

                            if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                            {
                                this.cmbFrequency.Tag = myorder.Frequency.ID;

                                this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else//��ҩƷ-����ҽ��
                {
                    if (this.GetVisiblePanel() != 3)
                    {
                        this.SetPanelVisible(3);
                    }
                    //this.cmbExeDept.Visible = true;
                    //neuLabel7.Visible = true;

                    //this.panelFrequency.Visible = false;//����ҽ�������ǲ�����ʾ��ҩƷƵ��
                    //this.txtDay.Visible = false;

                    txtSample.Visible = true;
                    neuLabel9.Visible = true;
                    if (this.myorder.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.txtSample.ClearItems();
                        this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperSample.ArrayObject);
                        this.neuLabel9.Text = "����:";
                        this.txtSample.Text = this.myorder.Sample.Name;
                    }
                    else if (this.myorder.Item.SysClass.ID.ToString() == "UC")
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
                    else
                    {
                        txtSample.Visible = false;
                        neuLabel9.Visible = false;
                    }


                    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);

                    if (cmbExeDept.Tag != null)
                    {
                        myorder.ExeDept.ID = cmbExeDept.Tag.ToString();
                    }

                    if (IsNew)
                    {
                        //if (myorder.Item.ID != "999")
                        //{
                        //    if (myorder.Item.ItemType == EnumItemType.UnDrug)
                        //    {
                        //        FS.HISFC.Models.Fee.Item.Undrug undrug = myorder.Item as FS.HISFC.Models.Fee.Item.Undrug;
                        //        //this.cmbExeDept.Tag = undrug.ExecDept;
                        //        SetExecDept(myorder, undrug.ExecDept, IsNew);
                        //    }
                        //}

                        this.txtDaysUndrug.Text = "1";
                    }
                    else
                    {
                        //ִ�п���
                        //if (myorder.ExeDept.ID != null && myorder.ExeDept.ID != "")
                        //{
                        //    //this.cmbExeDept.Tag = this.myorder.ExeDept.ID;
                        //    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);
                        //}

                        this.cmbMemoUndrug.Text = this.myorder.Memo;
                        this.chkEmerceUndrug.Checked = this.myorder.IsEmergency;

                        this.txtDaysUndrug.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();

                        if (myorder.HerbalQty > 0)
                        {
                            this.useDays = myorder.HerbalQty;
                        }

                        this.cmbUsageUndrug.Tag = this.myorder.Usage.ID;

                        if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                        {
                            this.cmbFrequencyUndrug.Tag = myorder.Frequency.ID;

                            this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                        }

                        //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                        //this.cmbCheckBody.Tag = this.myorder.CheckPartRecord;
                    }
                }

                this.AddEvent();
            }
            catch
            {
            };
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            dirty = false;
            if (IsNew)
            {
                this.CalculateTotal(myorder);
                //if (this.ItemSelected != null)
                //{
                //    this.ItemSelected(this.myorder, EnumOrderFieldList.Item);
                //}
            }
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

            this.DeleteEvent();

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


            switch (this.GetVisiblePanel())
            {
                case 1://��

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

                    try
                    {
                        this.myorder.DoseOnce = this.txtDoseOnceP.ComputeValue;//decimal.Parse(this.txtDoseOnce.Text);

                        if (this.txtDoseOnceP.InputText.Length > 8)
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim().Substring(0, 8);
                        }
                        else
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("ÿ���������벻��ȷ!", "��ʾ");
                        return;
                    }
                    ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = (this.cmbDoseUnitP.Text);
                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoP.Text,"'");
                    this.myorder.IsEmergency = this.chkEmerceDrug.Checked;

                    //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}������Ժ��ע�����
                    this.myorder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(txtDay.Text);

                    break;
                case 2://��

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

                    this.myorder.HerbalQty = decimal.Parse(this.txtFu.Text);

                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoPCC.Text);
                    this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOncePCC.Text);
                    if (this.cmbPCCSpeUsage.Tag != null)
                    {
                        //this.myorder.HerbalUsage.ID = this.cmbSpeUsage.Tag.ToString();
                    }
                    ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = this.cmbDoseUnitPCC.Text;


                    break;
                case 3://��

                    if (this.cmbFrequencyUndrug.SelectedItem == null)
                    {
                        this.myorder.Frequency.ID = "";
                        this.myorder.Frequency.Name = "";
                        this.myorder.Frequency.Time = "";
                    }
                    else
                    {
                        this.myorder.Frequency.ID = this.cmbFrequencyUndrug.SelectedItem.ID;
                        this.myorder.Frequency.Name = this.cmbFrequencyUndrug.SelectedItem.Name;
                        this.myorder.Frequency.Time = this.lnkTime.Text;
                    }

                    if (this.cmbExeDept.Tag != null)
                    {
                        this.myorder.ExeDept.ID = this.cmbExeDept.Tag.ToString();
                        this.myorder.ExeDept.Name = this.cmbExeDept.Text;
                    }
                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoUndrug.Text);
                    this.myorder.IsEmergency = this.chkEmerceUndrug.Checked;

                    if (myorder.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.myorder.Sample.Name = this.txtSample.Text;

                        if (this.txtSample.Tag != null)
                        {
                            this.myorder.Sample.ID = this.txtSample.Tag.ToString();
                        }
                    }
                    else
                    {
                        myorder.CheckPartRecord = txtSample.Text;
                    }

                    this.myorder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(txtDaysUndrug.Text);
                    //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                    //if (this.cmbCheckBody.Tag != null)
                    //{
                    //    this.myorder.CheckPartRecord = this.cmbCheckBody.Tag.ToString();
                    //}
                    break;
                default:
                    break;
            }


            this.AddEvent();

        }
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            //Classes.LogManager.Write("����ʼ��ʼ����Ŀ����ؼ���");

            if (FS.FrameWork.Management.Connection.Operator.ID == "")
                return;
            if (DesignMode == false)
            {
                ArrayList al1 = new ArrayList();
                try
                {
                    al1 = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.ORDERMEMO);
                }
                catch
                {
                    return;
                }
                this.cmbMemoP.AddItems(al1);
                this.cmbMemoPCC.AddItems(al1);
                this.cmbMemoUndrug.AddItems(al1);
                //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}��ҩ�����÷�
                ArrayList alSpeUsage = new ArrayList();
                alSpeUsage = CacheManager.GetConList("SPEUSAGE");
                if (alSpeUsage != null && alSpeUsage.Count > 0)
                {
                    this.cmbPCCSpeUsage.AddItems(alSpeUsage);
                }

                ArrayList alSecondUnitItems = new ArrayList();
                alSecondUnitItems = CacheManager.GetConList("SecondUnitItem");

                foreach (FS.HISFC.Models.Base.Const item in alSecondUnitItems)
                {
                    hsSecondUnitItem.Add(item.ID, item);
                }

                this.txtDoseOnceP.DotNum = 8;

                this.isAllDrugCanUseMinUnit = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("MZ0093", false, "0"));
                isDoseUnitInputMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ39", false, "0"));

                string val = Classes.Function.GetBatchControlParam("HNMZ41", false, "00");
                defultDoceOnceUnit = val.Substring(0, 1);

                //this.isAllDrugCanUseMinUnit = this.ctrlMgr.GetControlParam<bool>("MZ0093", false, false);
                //isDoseUnitInputMode = this.ctrlMgr.GetControlParam<int>("HNMZ39", false, 0);

                //string val = this.ctrlMgr.GetControlParam<string>("HNMZ41", false, "00");
                //defultDoceOnceUnit = val.Substring(0, 1);
            }
            //Classes.LogManager.Write("��������ʼ����Ŀ����ؼ���");

        }

        void ucOrderTermInputByType_Leave(object sender, EventArgs e)
        {
            if (FocusLost != null)
            {
                FocusLost(sender, e);
            }
        }

        /// <summary>
        /// �ı�����ת
        /// �˴�ֻ������ת�ɣ�ҽ���ı�ŵ�Mouse_Leave����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemKeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;

            if (e.KeyChar == 13)
            {
                this.GetOrder();

                if (((Control)sender).Name.Length > 7
                    && ((Control)sender).Name.Substring(0, 7) == "cmbMemo"
                    && sender != this.cmbMemoUndrug)
                {
                    if (this.FocusLost != null)
                    {
                        this.FocusLost(sender, null);
                    }
                }
                //else if (((Control)sender).Name == "cmbExeDept" 
                //    && this.isUndrugShowFrequency == false)
                //{
                //    if (this.FocusLost != null)
                //        this.FocusLost(sender, null);
                //}
                else if (sender == this.txtDoseOnceP)
                {
                    if ((this.cmbDoseUnitP.Items != null && this.cmbDoseUnitP.Items.Count > 1)
                        || (myorder != null && myorder.Item != null && myorder.Item.ID == "999"))
                    {
                        this.cmbDoseUnitP.Focus();
                    }
                    else
                    {
                        this.cmbFrequency.Focus();
                    }
                }
                else if (sender == this.txtDoseOncePCC)
                {
                    if (myorder != null && myorder.Item != null && myorder.Item.ID == "999")
                    {
                        cmbDoseUnitPCC.Focus();
                    }
                    else
                    {
                        this.txtFu.Focus();
                        this.txtFu.Select(0, txtFu.Value.ToString().Length);
                    }
                }
                else if (sender == this.cmbDoseUnitP)
                {
                    this.cmbFrequency.Focus();
                }

                else if (sender == this.cmbDoseUnitPCC)
                {
                    this.txtFu.Focus();
                }
                else if (sender == this.txtFu)
                {
                    //this.cmbFrequency.Focus();
                    this.cmbUsagePCC.Focus();
                }
                //{59C74550-5948-4321-A029-CB3CA6A822FD}
                else if (sender == this.txtDay)
                {
                    this.cmbUsageP.Focus();
                }
                else if (sender == this.cmbUsageP)
                {
                    this.cmbMemoP.Focus();
                }
                else if (sender == this.cmbExeDept)
                {
                    if (isUndrugShowFrequency)
                    {
                        cmbUsageUndrug.Focus();
                    }
                    else
                    {
                        if (this.Order != null
                            && "UC��UL".Contains(this.Order.Item.SysClass.ID.ToString()))
                        {
                            this.txtSample.Focus();
                        }
                        else
                        {
                            this.cmbMemoUndrug.Focus();
                        }
                    }
                }
                else if (sender == this.txtSample)
                {
                    this.cmbMemoUndrug.Focus();
                }
                else if (sender == this.cmbUsageUndrug)
                {
                    if (this.Order != null
                        && "UC��UL".Contains(this.Order.Item.SysClass.ID.ToString()))
                    {
                        this.txtSample.Focus();
                    }
                    else
                    {
                        this.cmbMemoUndrug.Focus();
                    }
                }
                else if (sender == this.cmbFrequency)
                {
                    switch (this.GetVisiblePanel())
                    {
                        case 1:
                            //{59C74550-5948-4321-A029-CB3CA6A822FD}
                            this.txtDay.Focus();
                            this.txtDay.Select(0, this.txtDay.Text.Length);
                            break;
                        case 2:
                            this.cmbUsagePCC.Focus();
                            break;
                        default:
                            if (this.FocusLost != null)
                                this.FocusLost(sender, null);
                            break;
                    }
                }
                else if (sender == this.cmbMemoUndrug)
                {
                    if (this.plFreq.Visible)
                    {
                        this.cmbFrequencyUndrug.Focus();
                    }
                    else
                    {
                        if (this.FocusLost != null)
                        {
                            this.FocusLost(sender, null);
                        }
                    }
                }
                else if (sender == this.cmbFrequencyUndrug)
                {
                    this.txtDaysUndrug.Focus();
                    this.txtDaysUndrug.Select(0, this.txtDaysUndrug.Text.Length);
                }
                else if (sender == this.txtDaysUndrug)
                {
                    if (this.FocusLost != null)
                    {
                        this.FocusLost(sender, null);
                    }
                }
                else
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// �Ƿ���Ը������¼��޸�ѡ�񴦷�
        /// </summary>
        /// <returns></returns>
        public bool IsCanChangeSelectOrder()
        {
            if (this.cmbUsageP.Focused ||
                this.cmbUsagePCC.Focused ||
                this.cmbUsageUndrug.Focused ||
                cmbDoseUnitP.Focused ||
                cmbDoseUnitPCC.Focused ||
                cmbFrequency.Focused ||
                cmbFrequencyUndrug.Focused ||
                cmbMemoP.Focused ||
                cmbMemoPCC.Focused ||
                cmbMemoUndrug.Focused ||
                cmbExeDept.Focused ||
                cmbPCCSpeUsage.Focused ||
                txtSample.Focused)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Mouse_Leave(object sender, EventArgs e)
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;

            #region ÿ������

            if ((Control)sender == txtDoseOnceP)
            {
                FS.HISFC.Models.Pharmacy.Item item = this.myorder.Item as FS.HISFC.Models.Pharmacy.Item;

                //1 ÿ��������������������
                if (isDoseUnitInputMode == 1)
                {
                    this.myorder.DoseOnce = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose * txtDoseOnceP.ComputeValue;
                    this.txtDoseOnceP.Text = this.myorder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.myorder.DoseOnceDisplay = txtDoseOnceP.Text;
                }
                //2 ֻ���������ʱ�š�ÿ����
                else if (isDoseUnitInputMode == 2)
                {
                    //ͨ������ ��/���ж�������Ƿ���
                    if (txtDoseOnceP.Text.Contains("/") && !txtDoseOnceP.Text.StartsWith("/") && !txtDoseOnceP.Text.EndsWith("/"))
                    {
                        this.myorder.DoseOnce = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose * txtDoseOnceP.ComputeValue;
                    }
                    else
                    {
                        this.myorder.DoseOnce = txtDoseOnceP.ComputeValue;
                    }
                    this.txtDoseOnceP.Text = this.myorder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.myorder.DoseOnceDisplay = txtDoseOnceP.Text;
                }
                else
                {
                    //��Ϊ��ʾ�ļ�������
                    string doseOnce = this.txtDoseOnceP.Text;
                    if (doseOnce.EndsWith("+") && myorder.Item.ID != "999")
                    {
                        if (this.myorder != null && this.myorder.Item.ItemType == EnumItemType.Drug)
                        {
                            try
                            {
                                if (isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//�����ж�
                                {
                                    this.cmbDoseUnitP.Text = item.MinUnit;
                                    this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                }
                                else
                                {
                                    if (hsSecondUnitItem.ContainsKey(this.myorder.Item.ID))
                                    {
                                        this.cmbDoseUnitP.Text = item.MinUnit;
                                        this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                    }
                                    else
                                    {
                                        this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    if (this.txtDoseOnceP.InputText.Length > 8)
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim().Substring(0, 8);
                    }
                    else
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim();
                    }

                    this.myorder.DoseOnce = this.txtDoseOnceP.ComputeValue;
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }
            }
            //��ҩÿ������
            else if ((Control)sender == txtDoseOncePCC)
            {
                if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                {
                    try
                    {
                        this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOncePCC.Text);
                        this.myorder.DoseOnceDisplay = this.txtDoseOncePCC.InputText.Trim();
                        if (myorder.Item.ID != "999")
                        {
                            this.myorder.Qty = Math.Round(this.myorder.HerbalQty * this.myorder.DoseOnce / ((HISFC.Models.Pharmacy.Item)this.myorder.Item).BaseDose, 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ÿ�����������\r\n" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtDoseOncePCC.Focus();
                        this.txtDoseOncePCC.SelectAll();
                        return;
                    }
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }

            }
            #endregion

            #region ÿ��������λ
            //��ҩÿ������
            else if ((Control)sender == cmbDoseUnitP)
            {
                if (myorder == null)
                {
                    return;
                }
                this.myorder.DoseUnit = this.cmbDoseUnitP.Text;

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseUnit);
                }
            }

            else if ((Control)sender == cmbDoseUnitPCC)
            {
                if (myorder == null)
                {
                    return;
                }
                this.myorder.DoseUnit = this.cmbDoseUnitPCC.Text;

                this.CalculateTotal(this.myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseUnit);
                }
            }
            #endregion

            #region Ƶ��
            else if ((Control)sender == cmbFrequency)
            {
                string time = "";
                if (this.myorder.Frequency.ID == this.cmbFrequency.SelectedItem.ID)
                {
                    time = this.myorder.Frequency.Time;//���Ƶ��ʱ���,��ȻҲ������IsNew������
                }
                this.myorder.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
                if (time != "")
                {
                    this.myorder.Frequency.Time = time;//����ʱ���
                }

                this.lnkTime.Text = this.myorder.Frequency.Time;

                this.CalculateTotal(myorder);

                CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Frequency);
                }
            }
            //������Ŀ��Ƶ��
            else if ((Control)sender == cmbFrequencyUndrug)
            {
                this.Order.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequencyUndrug.SelectedItem).Clone();
                this.CalculateTotal(myorder);
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Frequency);
                }

            }
            #endregion

            #region ����/����
            else if ((Control)sender == txtFu)
            {
                this.Order.HerbalQty = decimal.Parse(this.txtFu.Text);
                this.CalculateTotal(myorder);
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.HerbalQty);
                }

            }

            else if ((Control)sender == txtDay)//{59C74550-5948-4321-A029-CB3CA6A822FD}
            {
                try
                {
                    this.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Text);
                }
                catch
                {
                    MessageBox.Show("��������������������룡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.HerbalQty);
                }

                if (Order.HerbalQty > 0)
                {
                    this.useDays = Order.HerbalQty;
                }

            }
            //������Ŀ������
            else if ((Control)sender == txtDaysUndrug)//{59C74550-5948-4321-A029-CB3CA6A822FD}
            {
                try
                {
                    this.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDaysUndrug.Text);
                }
                catch
                {
                    MessageBox.Show("��������������������룡", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                //this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.HerbalQty);
                }

                if (Order.HerbalQty > 0)
                {
                    this.useDays = Order.HerbalQty;
                }

            }
            #endregion

            #region ��������
            else if ((Control)sender == txtSample)
            {
                if (this.ItemSelected != null)
                {
                    if (Order != null && Order.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Sample);
                    }
                    else
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.CheckBody);
                    }
                }

            }
            #endregion

            #region ִ�п���
            else if ((Control)sender == cmbExeDept)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.ExeDept);
                }

            }
            #endregion

            #region ��鲿λ
            //else if((Control)sender==  )
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.CheckBody);
            //    }

            //    }
            #endregion

            #region ��ע
            else if ((Control)sender == cmbMemoP)
            {
                if (this.cmbMemoP.Text.Contains("Ƥ��"))
                {
                    this.myorder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                }
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            else if ((Control)sender == cmbMemoPCC)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            else if ((Control)sender == cmbMemoUndrug)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            #endregion

            #region �÷�
            else if ((Control)sender == cmbUsageP)
            {
                this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }
            else if ((Control)sender == cmbUsagePCC)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }

            else if ((Control)sender == cmbUsageUndrug)
            {
                this.myorder.Usage.ID = this.cmbUsageUndrug.Tag.ToString();
                this.myorder.Usage.Name = this.cmbUsageUndrug.Text;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }
            #endregion

            #region ����

            //�����仯{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            //else if((Control)sender==  txtDrugQty)
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.DrugQty);
            //    }
            //    }
            #endregion

            #region �Ӽ�
            else if ((Control)sender == chkEmerceDrug)
            {
                this.myorder.IsEmergency = this.chkEmerceDrug.Checked;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Emc);
                }
            }
            else if ((Control)sender == chkEmerceUndrug)
            {
                this.myorder.IsEmergency = this.chkEmerceUndrug.Checked;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Emc);
                }
            }
            #endregion

            #region ��ҩ��ʽ

            else if ((Control)sender == cmbPCCSpeUsage)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.SpeUsage);
                }
            }
            #endregion
            //else
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.Item);
            //    }
            //}
        }
        #endregion

        #region	"��������"
        /// <summary>
        /// ���
        /// </summary>
        public virtual void Clear()
        {
            this.DeleteEvent();

            this.IsNew = true;
            this.myorder = null;
            this.txtDoseOnceP.Text = "0";				//ÿ������
            //this.txtMultiple.Text = "0";
            this.cmbDoseUnitP.Tag = "";				//ÿ��������λ
            this.cmbMemoP.Text = "";				//��ע
            this.cmbMemoPCC.Text = "";
            this.cmbMemoUndrug.Text = "";
            this.txtFu.Text = "0";					//����
            this.cmbExeDept.Text = "";				//ִ�п���
            this.chkEmerceUndrug.Checked = false;			//�Ӽ�
            this.chkEmerceDrug.Checked = false;		//�Ӽ�
            this.txtSample.Text = "";
            this.cmbFrequency.Tag = "";
            this.cmbFrequency.Text = "";
            //this.txtFrequency.Text = "";
            this.cmbFrequencyUndrug.Tag = "";
            this.cmbFrequencyUndrug.Text = "";
            this.cmbUsageP.Text = "";
            this.cmbUsageP.Tag = "";
            this.cmbUsagePCC.Text = "";
            this.cmbUsagePCC.Tag = "";
            this.cmbUsageUndrug.Text = "";
            this.cmbUsageUndrug.Tag = "";


            this.txtDay.Text = "1";
            this.txtDaysUndrug.Text = "1";
            //this.cmbCheckBody.Tag = "";
            this.qtyChanged = false;
            if (myorder != null)
            {
                myorder.Item.User02 = qtyChanged ? "1" : "0";
            }


            this.AddEvent();
        }

        /// <summary>
        /// ������ת
        /// </summary>
        public void SetShortKey()
        {
            if (this.txtDoseOnceP.Focused || this.txtFu.Focused || this.cmbExeDept.Focused)
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
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��������ʾ..", 50, false);
                Application.DoEvents();

                if (alDept == null)
                {
                    //alDept = CacheManager.InterMgr.GetDepartment();
                    alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
                }
                this.cmbExeDept.AddItems(alDept);
                this.cmbExeDept.IsListOnly = true;

                if (alFrequency == null)
                {
                    alFrequency = FS.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject.Clone() as ArrayList;
                }

                this.cmbFrequency.IsShowID = true;
                this.cmbFrequency.AddItems(alFrequency);
                this.cmbFrequency.IsListOnly = true;

                this.cmbFrequencyUndrug.IsShowID = true;
                this.cmbFrequencyUndrug.AddItems(alFrequency);
                this.cmbFrequencyUndrug.IsListOnly = true;

                //��ʼ���÷�
                //��Ժע�÷����ȡ��Ч�÷�����Ϊ����洢���û����Զ������
                ArrayList alInjectUsage = new ArrayList();//constantMgr.GetAllList("InjectUsage");
                ArrayList alTemp = null;
                if (alTemp == null || alTemp.Count == 0)
                {
                    alTemp = new ArrayList();
                    foreach (FS.HISFC.Models.Base.Const con in FS.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject)
                    {
                        alTemp.Add(con.Clone());
                    }
                }


                foreach (FS.HISFC.Models.Base.Const usageObj in alTemp)
                {
                    usageObj.UserCode = "";
                    alInjectUsage.Add(usageObj);
                }

                this.cmbUsageP.AddItems(alInjectUsage);
                this.cmbUsagePCC.AddItems(alInjectUsage);
                this.cmbUsageUndrug.AddItems(alInjectUsage);

                this.cmbUsageP.IsListOnly = true;
                this.cmbUsagePCC.IsListOnly = true;
                this.cmbUsageUndrug.IsListOnly = true;
                //��ʼ����������
                this.txtSample.AddItems(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.LABSAMPLE));
                this.txtSample.AppendItems(CacheManager.GetConList("CHECKPART"));
                //��鲿λ{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                //this.cmbCheckBody.AddItems(CacheManager.GetConList("CHECKBODY"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucOrderInputByType" + ex.Message);
            }

            if (isDoseUnitInputMode == -1)
            {
                isDoseUnitInputMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ39", false, "0"));
                //isDoseUnitInputMode = this.ctrlMgr.GetControlParam<int>("HNMZ39", false, 0);
            }
            this.AddEvent();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
                    this.txtDoseOnceP.Focus();
                    this.txtDoseOnceP.SelectAll();
                    break;
                case 2:
                    this.txtDoseOncePCC.Focus();
                    this.txtDoseOncePCC.SelectAll();
                    break;
                case 3:
                    this.cmbExeDept.Focus();
                    cmbExeDept.SelectAll();

                    //�����÷���ת
                    //this.cmbUsageUndrug.Focus();
                    //this.cmbUsageUndrug.SelectAll();
                    //this.cmbMemo3.Focus();
                    //this.cmbMemo3.SelectAll();
                    break;
                default:
                    break;
            }
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

            FS.HISFC.Components.Order.Forms.frmSpecialFrequency f = new FS.HISFC.Components.Order.Forms.frmSpecialFrequency();

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
                    this.myorder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(f.Dose);
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
        private void CalculateTotal(FS.HISFC.Models.Order.OutPatient.Order orderObj)
        {
            if (orderObj == null)
            {
                return;
            }

            if (orderObj.Item.SysClass.ID.ToString() == "M")
            {
                return;
            }

            //��Ϊ�޸Ĺ������󣬲��ټ���
            //��ҩƷ��Ϊ��Ҫ��Ϊ���������������ǻ��Զ�����  ������Ϊ���ư�
            if (isQtyChanged)// && Order.Item.SysClass.ID.ToString() != "UZ")
            {
                //Components.Order.Classes.Function.ShowBalloonTip(4, "��ʾ", "֮ǰ����Ϊ�޸��������˴������Զ����㣡", ToolTipIcon.Warning);
                return;
            }


            dirty = true;
            //��ҩ����
            decimal Days = 0;
            if (this.GetVisiblePanel() == 1)
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Text);
            }
            else if (this.GetVisiblePanel() == 3)
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDaysUndrug.Text);
            }
            else
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtFu.Text);
            }

            //���û����������ʱ�򲻴���
            if (Days <= 0)
            {
                dirty = false;
                return;
            }
            else
            {
                orderObj.HerbalQty = Days;
            }

            //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
            if (Components.Order.Classes.Function.ReComputeQty(orderObj) == -1)
            {
                dirty = false;
                return;
            }

            if (SetQtyValue != null)
            {
                SetQtyValue(orderObj);
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
                this.myorder.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
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
                    Frequence = Math.Round(this.myorder.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(this.myorder.Frequency.Days[0]), 2);
                }
                catch
                {
                    Frequence = this.myorder.Frequency.Times.Length;
                }
            }
            //�÷�
            if (this.cmbUsageP.Tag != null)
            {
                this.myorder.Usage.ID = this.cmbUsageP.Tag.ToString();
                this.myorder.Usage.Name = this.cmbUsageP.Text;
            }
            //Ժע����
            if (this.cmbUsageP.Tag != null
                //&& Classes.Function.HsUsageAndSub.Contains(cmbUsage1.Tag.ToString())
                && Classes.Function.CheckIsInjectUsage(cmbUsageP.Tag.ToString())
                )
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
        /// ��ȡ��ǰ�ؼ������� ֻ��������ת
        /// </summary>
        /// <returns></returns>
        public bool RecycleTab()
        {
            if (this.ActiveControl != null)
            {
                if (this.ActiveControl.Name == "cmbMemo1"
                   || this.ActiveControl.Name == "cmbMemo2"
                   || this.ActiveControl.Name == "cmbMemo3"
                   || this.ActiveControl.Name == "txtDays1")
                {
                    if (this.FocusLost != null)
                        this.FocusLost(this.ActiveControl, null);
                    return true;
                }
            }
            return false;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (this.txtDay.Focused )
            {
                if (keyData == Keys.Up )
                {
                    this.txtDay.Value += 1;
                }
                else if (keyData == Keys.Down)
                {
                    if (txtDay.Value >= 1)
                    {
                        txtDay.Value -= 1;
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else if (txtDaysUndrug.Focused)
            {
                if (keyData == Keys.Up)
                {
                    this.txtDaysUndrug.Value += 1;
                }
                else if (keyData == Keys.Down)
                {
                    if (txtDaysUndrug.Value >= 1)
                    {
                        txtDaysUndrug.Value -= 1;
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

    }
}