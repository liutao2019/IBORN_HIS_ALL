using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// ��ȡ��󷽺�
    /// </summary>
    /// <returns></returns>
    public delegate int GetMaxSubCombNoEvent(FS.HISFC.Models.Order.Inpatient.Order order,int sheetIndex);
    public delegate int GetSameSubCombNoOrderEvent(int sortID, ref FS.HISFC.Models.Order.Inpatient.Order order);

    /// <summary>
    /// ɾ�����
    /// </summary>
    /// <param name="subCombNo"></param>
    /// <returns></returns>
    public delegate int DeleteSubCombNoEvent(int subCombNo,bool isLong);

    /// <summary>
    /// [��������: ҽ����Ŀѡ��ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucItemSelect : UserControl
    {
        public ucItemSelect()
        {
            InitializeComponent();
            
        }
        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler CatagoryChanged;

        #region ��ʼ��

        public void Init()
        {
            if (DesignMode)
            {
                return;
            }
            if (FS.FrameWork.Management.Connection.Operator.ID == "")
            {
                return;
            }

            #region ����tip
            tooltip.SetToolTip(this.ucInputItem1, "����ƴ�����ѯ������ҽ��(ESCȡ���б�)");
            tooltip.SetToolTip(this.txtQuantity, "����������(�س��������)");
            tooltip.SetToolTip(this.dtBegin, "����ҽ����ʼִ��ʱ��");
            tooltip.SetToolTip(this.dtEnd, "����ҽ������ִ��ʱ��");
            #endregion
            try
            {
                this.ucInputItem1.DeptCode = CacheManager.LogEmpl.Dept.ID;//���ҿ��Լ����ҵ�ҩƷ��Ŀ
                this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
                this.ucOrderInputByType1.InitControl(null, null, null);

                this.cmbOrderType1.DropDownStyle = ComboBoxStyle.DropDownList;

                this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.InHos;

                this.isCanEditUnitAndQTY = controlParam.GetControlParam("ZYLZ01", true, false);// {4D67D981-6763-4ced-814E-430B518304E2}
                //��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��
                this.ucInputItem1.DrugSendType = "I";
                this.ucInputItem1.Init();//��ʼ����Ŀ�б�
            }
            catch { }
            try
            {
                SetLongOrShort(false);
                ArrayList alResQuality = CacheManager.InterMgr.QueryConstantList("LongOrderResQuality");
                if (alResQuality != null)
                {
                    this.hsResQuality = new Hashtable();
                    foreach (FS.FrameWork.Models.NeuObject info in alResQuality)
                    {
                        this.hsResQuality.Add(info.ID, null);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dtEnd.MinDate = DateTime.MinValue;
            this.dtEnd.Value = DateTime.Today.AddDays(1);
            this.dtEnd.Checked = false;


            AddEvent();
        }

        /// <summary>
        /// ����¼�
        /// </summary>
        private void AddEvent()
        {
            this.DeleteEvent();
            this.ucOrderInputByType1.ItemSelected += new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.cmbOrderType1.SelectedIndexChanged += new System.EventHandler(this.cmbOrderType1_SelectedIndexChanged);

            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new EventHandler(txtQuantity_Leave);

            this.cmbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.TextChanged += new EventHandler(cmbUnit_TextChanged);
            this.ucOrderInputByType1.Leave += new EventHandler(ucOrderInputByType1_Leave);

            this.dtBegin.ValueChanged += new System.EventHandler(this.dtBegin_ValueChanged);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);

            this.dtBegin.CloseUp += new EventHandler(dtBegin_CloseUp);
            this.dtEnd.CloseUp += new EventHandler(dtEnd_CloseUp);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucItemSelect����¼�!\r\n");
        }

        /// <summary>
        /// ɾ���¼�
        /// </summary>
        private void DeleteEvent()
        {
            this.ucOrderInputByType1.ItemSelected -= new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.cmbOrderType1.SelectedIndexChanged -= new System.EventHandler(this.cmbOrderType1_SelectedIndexChanged);

            this.ucInputItem1.SelectedItem -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            this.txtQuantity.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave -= new EventHandler(txtQuantity_Leave);

            this.cmbUnit.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.TextChanged -= new EventHandler(cmbUnit_TextChanged);
            this.ucOrderInputByType1.Leave -= new EventHandler(ucOrderInputByType1_Leave);

            this.dtBegin.ValueChanged -= new System.EventHandler(this.dtBegin_ValueChanged);
            this.dtEnd.ValueChanged -= new EventHandler(dtEnd_ValueChanged);

            this.dtBegin.CloseUp -= new EventHandler(dtBegin_CloseUp);
            this.dtEnd.CloseUp -= new EventHandler(dtEnd_CloseUp);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucItemSelectɾ���¼�!\r\n");
        }
        #endregion

        #region ����
        /// <summary>
        /// ҽ���仯ʱ����
        /// </summary>
        public event ItemSelectedDelegate OrderChanged;//

        /// <summary>
        /// ��ȡ��󷽺�
        /// </summary>
        public event GetMaxSubCombNoEvent GetMaxSubCombNo;

        /// <summary>
        /// �����ͬ����ҽ��
        /// </summary>
        public event GetSameSubCombNoOrderEvent GetSameSubCombNoOrder;

        /// <summary>
        /// ɾ�����
        /// </summary>
        public event DeleteSubCombNoEvent DeleteSubComnNo;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public Operator OperatorType = Operator.Query;

        /// <summary>
        /// ��ǰ��
        /// </summary>
        public int CurrentRow = -1;

        /// <summary>
        /// �Ƿ�������ױ༭����
        /// </summary>
        public bool EditGroup = false;
    
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        protected bool dirty = false;

        /// <summary>
        /// ToolTip
        /// </summary>
        protected ToolTip tooltip = new ToolTip();

        /// <summary>
        /// ����ҽ��������ҩƷ����
        /// </summary>
        System.Collections.Hashtable hsResQuality = new Hashtable();
        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// סԺ��ʱҽ���Ƿ���Ա༭�����͵�λ// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private bool isCanEditUnitAndQTY = false;
        /// <summary>
        /// �洢ҩƷ��Ϣ��ֻ�ܴ洢ʵʱ�Բ��ߵ���Ϣ
        /// </summary>
        [Obsolete("���ϣ���order�����GetPHAItem����", true)]
        Hashtable hsPhaItem = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        protected FS.HISFC.Models.Order.Inpatient.Order order = null;

        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        [DefaultValue(null)]
        public FS.HISFC.Models.Order.Inpatient.Order Order
        {
            get
            {
                return this.order;
            }
            set
            {
                try
                {
                    if (value == null)
                    {
                        return;
                    }

                    this.DeleteEvent();

                    this.order = value;

                    if (this.isNurseCreate)
                    {
                        if (this.order.ReciptDoctor.ID != FS.FrameWork.Management.Connection.Operator.ID)
                        {
                            MessageBox.Show("��ʿ�������޸����˿�����ҽ��!");
                            return;
                        }
                    }

                    dirty = false;

                    this.LongOrShort = (int)this.order.OrderType.Type;
                    this.ucOrderInputByType1.IsNew = false;//�޸ľ�ҽ��

                    this.ucOrderInputByType1.Order = value;

                    this.ucInputItem1.FeeItem = this.order.Item;
                    this.cmbOrderType1.Tag = this.order.OrderType.ID;

                    ShowOrder(this.order);//��������ҽ��

                    dirty = true;
                }
                catch
                {
                }
                finally
                {
                    AddEvent();
                }
            }
        }

        protected int longOrShort = 0;

        /// <summary>
        /// ���� 0 or��ʱҽ�� 1
        /// </summary>
        public int LongOrShort
        {
            get
            {
                return longOrShort;
            }
            set
            {
                if (DesignMode) return;
                if (longOrShort == value) return;
                if (value == 0)
                {
                    this.SetLongOrShort(false);
                    
                }
                else
                {
                    this.SetLongOrShort(true);   
                }
                longOrShort = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʿ����
        /// </summary>
        private bool isNurseCreate = false;

        /// <summary>
        /// �Ƿ�ʿ����
        /// </summary>
        [DefaultValue(false)]
        public bool IsNurseCreate
        {
            set
            {
                this.isNurseCreate = value;
            }
        }
        
        #endregion

        #region �¼�
        protected bool bPermission = false;//�Ƿ�֪��ͬ����

        private void SetQty(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            if (inOrder.Item.ID == "999")
            {
                this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//���Ը���
                this.cmbUnit.Enabled = this.txtQuantity.Enabled;
            }
            else
            {
                if (!isCanEditUnitAndQTY)// {4D67D981-6763-4ced-814E-430B518304E2}
                {
                    if (!inOrder.OrderType.IsDecompose && inOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        // 1 ��װ��λ����ȡ�� 3 ��װ��λÿ����ȡ��
                        Components.Order.Classes.Function.GetSplitType(ref inOrder);

                        if (!string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType)
                            && "1��3".Contains(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType))
                        {
                            cmbUnit.Enabled = false;
                        }
                        else
                        {
                            this.cmbUnit.Enabled = true;
                        }

                        //��������Ϊ�󲿷�ҩƷ���������޸����������˸�������ҩ�������Һ�ȣ�
                        //�����ʾ������ҩ
                        if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).SpecialFlag2 == "1")
                        {
                            txtQuantity.Enabled = true;
                            cmbUnit.Enabled = true;
                        }
                        else
                        {
                            txtQuantity.Enabled = false;
                            this.cmbUnit.Enabled = false;
                        }
                    }
                    else
                    {
                        //if (inOrder.Item.SysClass.ID.ToString() == "UL"
                        //    || inOrder.Item.SysClass.ID.ToString() == "UC")
                        //{
                        //    this.txtQuantity.Enabled = false;
                        //    cmbUnit.Enabled = false;
                        //}
                        //else
                        {
                            txtQuantity.Enabled = true;
                            cmbUnit.Enabled = true;
                        }
                    }
                }
                else
                {

                    txtQuantity.Enabled = true;
                    cmbUnit.Enabled = true;

                    Components.Order.Classes.Function.GetSplitType(ref inOrder);

                    // {4D67D981-6763-4ced-814E-430B518304E2}
                    if (!inOrder.OrderType.IsDecompose && inOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType)
                            && "1��3".Contains(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType))
                        {
                            cmbUnit.Enabled = false;
                        }
                        else
                        {
                            this.cmbUnit.Enabled = true;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// ҽ���仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucOrderInputByType1_ItemSelected(FS.HISFC.Models.Order.Inpatient.Order order, string changedField)
        {
            this.DeleteEvent();
            dirty = true;
            
            if (order.OrderType.IsDecompose == false
                && order.Item.ItemType == EnumItemType.Drug && order.Frequency.ID != "")
            {
                this.txtQuantity.Text = order.Qty.ToString();
                this.cmbUnit.Tag = order.Unit;
            }
            
            this.myOrderChanged(order,changedField);
            dirty = false;
            this.AddEvent();
        }

        protected void myOrderChanged(object sender,string enumOrderFieldList)
        {
            try
            {
                if (this.CurrentRow == -1)
                {
                    this.CurrentRow = 0;
                    this.OperatorType = Operator.Add;//���
                }
                else
                {
                    this.OperatorType = Operator.Modify;
                }

                this.order = sender as FS.HISFC.Models.Order.Inpatient.Order;//�ؼ������Ķ���
                this.ucOrderInputByType1.IsNew = false;//�޸ľ�ҽ��
                this.ucOrderInputByType1.Order = this.order;

                this.OrderChanged(order, enumOrderFieldList);
            }
            catch { }
        }

        /// <summary>
        /// �����仯-������һ�������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e == null || e.KeyChar == 13)
            {
                if (this.cmbUnit.Enabled)
                {
                    this.cmbUnit.Focus();
                }
                else
                {
                    this.txtCombNo.Focus();
                }
            }
        }

        /// <summary>
        /// ��λkeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null) return;
            if (e == null || e.KeyChar == 13)
            {
                this.txtCombNo.Focus();
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }

            if (this.order.Qty == FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value))
            {
                return;
            }

            if (this.order.Qty != FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value))
            {
                this.ucOrderInputByType1.IsQtyChanged = true;
                this.order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value);
                myOrderChanged(this.order, ColmSet.Z����);
            }
            else
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
            }

            //this.txtQuantity_KeyPress(sender, null);
        }

        /// <summary>
        /// У�鿪ʼʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_ValueChanged(object sender, System.EventArgs e)
        {
            /*
             * 1����ʼʱ�䲻�����ڵ���ʱ��24Сʱ
             * 2����ʼʱ�䲻��������һ��ҽ���Ŀ�ʼʱ��
             * 
             * */

            if (this.order == null)
            {
                return;
            }

            //��ʱʹ�ñ���ʱ���ж�
            //Ӧ���ڴ˴���ȡϵͳʱ�� ������Ч������ ��ʹ�ñ���ʱ���ж�
            //ֻ�в�¼ҽ���������ÿ���ʱ��С�ڵ�ǰʱ��
            //if (this.dtBegin.Value < DateTime.Now &&
            //    this.order.OrderType.ID != "BL" && //��¼ҽ��
            //    this.order.OrderType.ID != "BZ")//��¼����
            //{
            //    this.dtBegin.Value = this.order.BeginTime;
            //    return;
            //}

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            if (dtBegin.Value < order.MOTime.AddHours(-24))
            {
                if (order.BeginTime > new DateTime(2000, 1, 1))
                {
                    dtBegin.Value = order.BeginTime;
                }
                else
                {
                    dtBegin.Value = dtNow;
                }
                Components.Order.Classes.Function.ShowBalloonTip(5, "����", "��ʼʱ��ѡ����󣬲������ڵ�ǰʱ��24Сʱ��\r\n\r\n������ѡ��ʼʱ�䣡", ToolTipIcon.Warning);
                return;
            }

            if (this.order.BeginTime != this.dtBegin.Value)
            {
                this.dtBegin.Value = new DateTime(this.dtBegin.Value.Year, this.dtBegin.Value.Month, this.dtBegin.Value.Day, this.dtBegin.Value.Hour, this.dtBegin.Value.Minute, 0);
                this.order.BeginTime = this.dtBegin.Value;
                dirty = true;
                myOrderChanged(this.order, ColmSet.K��ʼʱ��);
                dirty = false;
            }
        }

        /// <summary>
        /// ֹͣʱ��仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            if (this.order == null) 
                return;

            if (this.txtQuantity.Text == "")
                return;

            if (this.dtEnd.Value.Date <= this.dtBegin.Value.Date 
                && this.dtEnd.Checked)
            {
                this.dtEnd.Value = this.dtBegin.Value;

                Components.Order.Classes.Function.ShowBalloonTip(5, "����", "����ʱ��ѡ����󣬲������ڿ�ʼʱ�䣡\r\n\r\n������ѡ�����ʱ�䣡", ToolTipIcon.Warning);

                return;
            }

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            try
            {
                if (this.dtEnd.Checked == false)
                {
                    this.order.EndTime = System.DateTime.MinValue;
                }
                else
                {
                    if (dtEnd.Value < dtNow.AddHours(-24))
                    {
                        dtBegin.Value = dtNow;
                        Components.Order.Classes.Function.ShowBalloonTip(5, "����", "����ʱ��ѡ����󣬲������ڵ�ǰʱ��24Сʱ��\r\n\r\n������ѡ�����ʱ�䣡", ToolTipIcon.Warning);
                        return;
                    }
                    this.order.EndTime = this.dtEnd.Value;//ֹͣʱ��
                }
                dirty = true;
                myOrderChanged(this.order, ColmSet.Tֹͣʱ��);
                dirty = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtBegin_CloseUp(object sender, System.EventArgs e)
        {
            this.dtBegin.Value = new DateTime(this.dtBegin.Value.Year, this.dtBegin.Value.Month, this.dtBegin.Value.Day, 0, 0, 0);
        
        }


        private void dtEnd_CloseUp(object sender, System.EventArgs e)
        {
            if (this.dtEnd.Value.Date <= this.dtBegin.Value.Date && this.dtEnd.Checked)
            {
                MessageBox.Show("ҽ����ֹʱ�䲻��С����ʼʱ�䣬�����", "��ʾ");
            }

            this.dtEnd.Value = new DateTime(this.dtEnd.Value.Year, this.dtEnd.Value.Month, this.dtEnd.Value.Day, 23, 59, 59);
        }

        private Panel PanelEnd
        {
            get
            {
                return this.panelEndDate;
            }
        }

        /// <summary>
        /// ����ϵͳ���
        /// </summary>
        /// <param name="isShort"></param>
        /// <param name="alSysClass"></param>
        /// <returns></returns>
        private ArrayList FilterSysClassForNurse(bool isShort, ArrayList alSysClass)
        {
            System.Collections.ArrayList al = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "ȫ��";
            al.Add(objAll);
            
            //��ʿҽ������Щ����

            System.Collections.ArrayList rAl = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MR")//��ҩƷ��ת�ƣ�ת��
                {

                }
                else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UO")//����
                {
                }
                //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UC")//���
                //{
                //}
                //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UL")	//����
                //{
                //}
                else if (obj.ID.Length >= 1 && obj.ID.Substring(0, 1) == "P")//ҩ
                {
                }
                else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MC")//����
                {
                }
                else
                {
                    rAl.Add(obj);
                }
            }
            return rAl;
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        /// <param name="b"></param>
        protected void SetLongOrShort(bool isShort)
        {
            dirty = false;

            //����ҽ��ֹͣ����
            this.PanelEnd.Visible = !isShort;
            PanelEnd.Visible = false;

            this.cmbOrderType1.AddItems(FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderType(isShort));
             
            if (this.isNurseCreate)
            {
                this.ucInputItem1.AlCatagory = this.FilterSysClassForNurse(isShort, SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(isShort));
            }
            else
            {
                this.ucInputItem1.AlCatagory = FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(isShort);
            }
            if (cmbOrderType1.Items.Count > 0)
            {
                this.cmbOrderType1.SelectedIndex = 0;
            }

            ucInputItem1.LongOrder = !isShort;
        }

        /// <summary>
        /// ҽ�����ͱ仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrderType1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbOrderType1.SelectedIndex < 0)
            {
                return;
            }
            FS.HISFC.Models.Order.OrderType obj = null;

            if (this.LongOrShort == 0) //����ҽ��
            {
                obj = SOC.HISFC.BizProcess.Cache.Order.GetOrderType(false)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
            }
            else //��ʱҽ��
            {
                obj = SOC.HISFC.BizProcess.Cache.Order.GetOrderType(true)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
                //��Ժ��ҩ����ٴ�ҩ������Ҫ��������
            }
        
            if (obj.IsCharge == false)
            {
                this.ucInputItem1.IsCanInputName = false;
            }
            else
            {
                this.ucInputItem1.IsCanInputName = true;
            }

            this.ucInputItem1.Focus();

        }

        /// <summary>
        /// ҽ�����͸���
        /// </summary>
        /// <param name="charge">�Ƿ��շ�ҽ������</param>
        private void GeChargeableOrderType(bool charge)
        {
            return;

            //�жϵ�ǰҽ���շ�����
            FS.HISFC.Models.Order.OrderType ordertype = this.cmbOrderType1.SelectedItem as FS.HISFC.Models.Order.OrderType;
            if (ordertype != null)
            {
                if (ordertype.IsCharge == charge)
                    return;
            }
            //�����ϣ����ҵ�һ�����ϵ��շ�����
            foreach (FS.HISFC.Models.Order.OrderType obj in this.cmbOrderType1.alItems)
            {
                if (obj.IsCharge == charge)
                {
                    this.cmbOrderType1.Tag = obj.ID;
                    return;
                }
            }
        }

        private void cmbUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string unit = this.cmbUnit.Text.Trim();
                if (FS.FrameWork.Public.String.ValidMaxLengh(unit, 16) == false)
                {
                    MessageBox.Show("��λ����!", "��ʾ");
                    return;
                }
                if (this.order.Unit != unit && dirty == true)
                {
                    this.order.Unit = unit;//���µ�λ
                    myOrderChanged(this.order,ColmSet.Z������λ);
                }
            }
            catch { }
        }
       
        /// <summary>
        /// ��ǰѡ���ҽ������
        /// </summary>
        public FS.HISFC.Models.Order.OrderType SelectedOrderType
        {
            get
            {
                return this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
            }
        }

        /// <summary>
        /// ��Ŀ���ѡ��仯
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = sender;
                if (obj.ID.Length > 0 && obj.ID.Substring(0, 1) == "M")
                {
                    GeChargeableOrderType(false);
                }
                else
                {
                    GeChargeableOrderType(true);
                }
            }
            catch { }
            if (CatagoryChanged != null) CatagoryChanged(sender);
        }

        void ucOrderInputByType1_Leave(object sender, EventArgs e)
        {
            this.ucInputItem1.Focus();
        }

        /// <summary>
        /// ������Ŀ������Ƿ�ɼ�
        /// </summary>
        /// <param name="vlue"></param>
        public void SetInputControlVisible(bool vlue)
        {
            this.ucInputItem1.SetVisibleForms(vlue);
        }

        /// <summary>
        /// ����������ý��㣬ֻ������leave�¼���Ч
        /// </summary>
        public void SetFocus()
        {
            this.ucInputItem1.Focus();
        }

        /// <summary>
        /// ѡ����Ŀ
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                this.DeleteEvent();

                this.Clear(false);

                if (this.ucInputItem1.FeeItem == null)
                {
                    return;
                }

                //�ж�ȱҩ��ͣ��
                FS.HISFC.Models.Pharmacy.Item itemObj = null;
                string errInfo = "";
                
                FS.HISFC.Models.Order.OrderType obj = null;

                obj = FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderType(longOrShort == 1)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;

                if (ucInputItem1.FeeItem.ID != "999" && obj.IsCharge)
                {
                    if (Components.Order.Classes.Function.CheckDrugState(null,
                        new FS.FrameWork.Models.NeuObject(this.ucInputItem1.FeeItem.User02, this.ucInputItem1.FeeItem.User03, ""),
                        (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem,
                        false, ref itemObj, ref errInfo) == -1)
                    {
                        MessageBox.Show(errInfo);
                        return;
                    }

                    //{1BBD2F14-49A6-468b-BB08-19BF0499CEF4}
                    if (itemObj != null && itemObj.Product != null && !string.IsNullOrEmpty(itemObj.Product.Caution))
                    {
                        MessageBox.Show(itemObj.Product.Caution);
                    }
                }

                if (!this.EditGroup)		//��ʵ�ֶ������޸Ĺ���ʱ �����֪��ͬ����������ж�
                {
                    //�жϵ�ǰ�������Ŀ�Ƿ�֪��ͬ����
                    this.bPermission = Classes.Function.IsPermission(this.patientInfo,
                        (FS.HISFC.Models.Order.OrderType)this.cmbOrderType1.SelectedItem,
                        (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem);
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //���ظ�
                {
                    this.txtQuantity.Focus();
                    return;
                }

                //�շ�ҽ��ȴ��999����ķ���
                if (this.ucInputItem1.FeeItem.ID == "999" && obj.IsCharge)
                {
                    return;
                }
                //����ʷ�ж�
                //��Ŀ�仯-ָ������
                this.CurrentRow = -1;

                this.OperatorType = Operator.Add;

                //������ҽ��
                this.SetNewOrder();


                if (!((FS.HISFC.Models.Order.OrderType)this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex]).IsCharge)
                {
                    this.cmbOrderType1.SelectedIndex = 0;
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //���ظ�
                {
                    this.txtQuantity.Focus();
                    int length = this.txtQuantity.DecimalPlaces == 0 ? this.txtQuantity.Value.ToString().Length : this.txtQuantity.Value.ToString().Length + 1 + this.txtQuantity.DecimalPlaces;
                    this.txtQuantity.Select(0, length);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.AddEvent();
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ȡҽ����Ϣ-���ƿؼ���ʾ״̬
        /// </summary>
        /// <param name="myOrder"></param>
        protected int ShowOrder(FS.HISFC.Models.Order.Inpatient.Order myOrder)
        {
            if (myOrder == null) 
                return 0;

            this.txtCombNo.Value = myOrder.SubCombNO;

            #region ����ҽ������ʱ��

            try//����ֹͣʱ��
            {
                //��ʼʱ��
                if (this.order.BeginTime > new DateTime(2000, 1, 1))
                {
                    dtBegin.Value = order.BeginTime;
                }

                //����ʱ��
                if (this.order.EndTime > new DateTime(2000, 1, 1)
                    && PanelEnd.Visible)
                {
                    this.dtEnd.Checked = true;//����С���ڣ����ý�������
                    this.dtEnd.Value = this.order.EndTime;
                }
            }
            catch
            {
            }

            #endregion

            //��Ŀ
            if (myOrder.Item.ItemType == EnumItemType.Drug)
            {
                if (this.LongOrShort == 0) //����ҽ��������ʾ����
                {
                }
                else
                {
                    //ҩƷ ��ʱҽ����Ƶ��Ϊ�գ�Ĭ��Ϊ��Ҫʱ�����prn
                    if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                    {
                        myOrder.Frequency.ID = Classes.Function.GetDefaultFrequencyID();//��ʱҽ��Ĭ��Ϊ��Ҫʱִ��
                    }
                }

                this.txtQuantity.Text = myOrder.Qty.ToString(); //����

                this.cmbUnit.Items.Clear();

                if (myOrder.Item.ID == "999" || !myOrder.OrderType.IsCharge)
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//���Ը���
                    this.cmbUnit.Enabled = this.txtQuantity.Enabled;
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Item item = Components.Order.Controls.Order.GetPHAItem(myOrder.Item.ID);

                    this.cmbUnit.Items.Add(item.MinUnit);//��λ
                    this.cmbUnit.Items.Add(item.PackUnit);//��λ
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;//ֻ��ѡ��

                    if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                    {
                        if (this.cmbUnit.Items.Count > 0)
                        {
                            this.cmbUnit.SelectedIndex = 0;
                            myOrder.Unit = this.cmbUnit.Text;
                            myOrder.Item.PriceUnit = myOrder.Unit;
                        }
                    }
                    else
                    {
                        this.cmbUnit.Text = myOrder.Unit;
                    }

                    if (myOrder.OrderType.IsCharge) //�Զ���ҩƷ
                    {
                        if (item.PackQty == 0)//����װ����
                        {
                            MessageBox.Show("��ҩƷ�İ�װ����Ϊ�㣡");
                            return -1;
                        }
                        if (item.BaseDose == 0)//����������
                        {
                            MessageBox.Show("��ҩƷ�Ļ�������Ϊ�㣡");
                            return -1;
                        }
                        if (item.DosageForm.ID == "")//������
                        {
                            MessageBox.Show("��ҩƷ�ļ���Ϊ�գ�");
                            return -1;
                        }
                    }

                    SetQty(myOrder);
                }
            }
            else if (myOrder.Item.ItemType == EnumItemType.UnDrug)//��ҩƷ
            {
                //���ִ�п���Ϊ��--�������ƿ���
                //if (myOrder.ExeDept.ID == "")
                //{
                //    if (item.ExecDept == "")
                //    {
                //        myOrder.ExeDept.ID = myOrder.Patient.PVisit.PatientLocation.Dept.ID;////ִ�п���?????������Ҫ�޸�
                //        myOrder.ExeDept.Name = myOrder.Patient.PVisit.PatientLocation.Dept.Name;
                //    }
                //    else if (item.ExecDepts != null && item.ExecDepts.Count > 0)
                //    {
                //        try
                //        {
                //            myOrder.ExeDept.ID = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).ExecDepts[0].ToString();
                //        }
                //        catch { }
                //    }
                //}

                if (myOrder.Item.ID != "999")
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item);
                    if (myOrder.CheckPartRecord == ""
                        && myOrder.Item.SysClass.ID.ToString() == "UC") //�����岿λ
                    {
                        myOrder.CheckPartRecord = item.CheckBody;
                    }
                    if (myOrder.Sample.Name == "" 
                        && myOrder.Item.SysClass.ID.ToString() == "UL") //�����岿λ
                    {
                        myOrder.Sample.Name = item.CheckBody;
                    }
                }

                this.cmbUnit.Items.Clear();

                if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                {
                    string unit = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).PriceUnit;
                    if (unit == null || unit == "")
                    {
                        unit = "��";
                    }
                    this.cmbUnit.Items.Add(unit);
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;
                        myOrder.Unit = this.cmbUnit.Text;
                        myOrder.Item.PriceUnit = myOrder.Unit;
                    }
                }
                else
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.Text = myOrder.Unit;
                }
                if (myOrder.Qty == 0)
                {
                    //this.txtQuantity.Text = "1.00"; //����
                    //myOrder.Qty = 1;
                }
                else
                {
                    this.txtQuantity.Text = myOrder.Qty.ToString();
                }
                if (myOrder.Frequency.ID == "")
                {
                    myOrder.Frequency.ID = "QD";//��ʱҽ��Ĭ��QD
                }
            }
            else
            {
                MessageBox.Show("�޷�ʶ������ͣ�");
                return -1;
            }

            this.SetQty(myOrder);

            return 0;
        }

        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                //bool isRefresh = false;
                ////{CE481BFE-9211-48eb-8921-50D04858CB39} ����value != null���ж� Added by Gengxl
                //if (value != null 
                //    && this.patientInfo != null
                //    && this.patientInfo.ID != value.ID)
                //{
                //    isRefresh = true;
                //}
                this.patientInfo = value;
                //{112B7DB5-0462-4432-AD9D-17A7912FFDBE}  ������Ϣ
                this.ucInputItem1.Patient = value;

                //if (isRefresh)
                //{
                //    if (this.patientInfo.Pact.PayKind.ID == "02")
                //    {
                //        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("����ˢ��ҽ����Ŀ�����..");
                //        Application.DoEvents();

                //        //this.ucInputItem1.RefreshSIFlag();

                //        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //    }
                //}
            }
        }

        protected FS.FrameWork.Models.NeuObject myReciptDept = null;

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            if (this.myReciptDept == null)
            {
                myReciptDept = new FS.FrameWork.Models.NeuObject();
                this.myReciptDept.ID = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.ID; //��������
                this.myReciptDept.Name = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.Name;
            }
            return this.myReciptDept;
        }

        protected FS.FrameWork.Models.NeuObject myReciptDoc = null;

        /// <summary>
        /// ��ȡ����ҽ��
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDoc()
        {
            if (this.myReciptDoc == null)
            {
                myReciptDoc = new FS.FrameWork.Models.NeuObject();
                myReciptDoc = CacheManager.InOrderMgr.Operator.Clone();
            }
            return this.myReciptDoc;
        }

        /// <summary>
        /// ������ҽ��
        /// </summary>
        protected void SetNewOrder()
        {
            if (this.DesignMode)
            {
                return;
            }

            //�������ҽ������
            this.order = new FS.HISFC.Models.Order.Inpatient.Order();//��������ҽ��

            order.Patient = this.patientInfo;

            dirty = false;
            try
            {
                if (this.ucInputItem1.FeeItem.ID == "999")//�Լ�¼����Ŀ
                {
                    this.order.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                }
                else
                {
                    string err = "";
                    //ҩƷ
                    if (this.ucInputItem1.FeeItem.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                    {
                        this.order.Item = Components.Order.Controls.Order.GetPHAItem(ucInputItem1.FeeItem.ID);

                        this.order.Item.User01 = this.ucInputItem1.FeeItem.User01;
                        this.order.Item.User02 = this.ucInputItem1.FeeItem.User02;//����ȡҩҩ��
                        this.order.Item.User03 = this.ucInputItem1.FeeItem.User03;

                        order.StockDept.ID = ucInputItem1.FeeItem.User02;
                        order.StockDept.Name = ucInputItem1.FeeItem.User03;

                        if (this.GetReciptDept() != null)
                        {
                            order.ReciptDept.ID = this.GetReciptDept().ID;
                            order.ReciptDept.Name = this.GetReciptDept().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            order.ReciptDoctor.ID = this.GetReciptDoc().ID;
                            order.ReciptDoctor.Name = this.GetReciptDoc().Name;
                        }


                        if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref order, out err) == -1)
                        {
                            MessageBox.Show(err);
                            return;
                        }
                        if (order == null)
                        {
                            MessageBox.Show("ת������!", "ucItemSelect");
                            return;
                        }
                        int ret = 0;
                        string error = "";
                        // {E97273E4-CF5A-47bf-97C6-8025504486C4}
                        if (null != this.patientInfo && !EditGroup)
                        {
                            ret = Components.Order.Classes.Function.JudgePatientAllergy("2", this.patientInfo.PID, this.order, ref error);
                        }
                        else
                        {
                            ret = 1;
                        }

                        if (ret <= 0)
                        {
                            return;
                        }
                    }
                    else//��ҩƷ
                    {
                        try
                        {
                            FS.HISFC.Models.Fee.Item.Undrug itemTemp = SOC.HISFC.BizProcess.Cache.Fee.GetItem(ucInputItem1.FeeItem.ID);
                            if (itemTemp == null)
                            {
                                MessageBox.Show("��÷�ҩƷ��Ϣʧ�ܣ�\r\n" + ucInputItem1.FeeItem.Name);
                                return;
                            }
                            this.order.Item = itemTemp;

                            this.order.Item.Qty = this.txtQuantity.Value;

                            if (CacheManager.OrderIntegrate.FillFeeItem(ref order, out err) == -1)
                            {
                                MessageBox.Show(err);
                                return;
                            }
                            if (order == null)
                            {
                                MessageBox.Show("ת������!", "ucItemSelect");
                                return;
                            }

                            //���Ҫ���Ƿ�Ϊ�� ��ʱ�ɴ��жϸ���ĿΪ��黹�Ǽ���		
                            if (itemTemp.SysClass.ID.ToString() == "UL")
                            {
                                //���ø�����Ŀ��ϸ����������롢��������
                                this.order.Sample.Name = itemTemp.CheckBody;
                            }
                            else
                            {
                                this.order.CheckPartRecord = itemTemp.CheckBody;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ת������!", "ucItemSelect");
                        }
                    }
                    //����֪��ͬ����
                    this.order.IsPermission = bPermission;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.order.SubCombNO == 0)
            {
                this.order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Value);
            }
            else
            {
                this.txtCombNo.Value = this.order.SubCombNO;
            }

            #region ����ҽ������ʱ��

            try//����ֹͣʱ��
            {

                if (order.MOTime == DateTime.MinValue)
                {
                    this.order.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();//����ʱ��
                }

                //��ʼʱ��
                if (order.BeginTime < new DateTime(2000, 1, 1))
                {
                    this.order.BeginTime = Classes.Function.GetDefaultMoBeginDate(this.order.OrderType.IsDecompose ? 0 : 1);
                    dtBegin.Value = order.BeginTime;
                }

                //����ʱ��
                if (this.order.EndTime <= this.dtEnd.MaxDate)
                {
                    if (this.order.EndTime == DateTime.MinValue) //��С���ڲ����ý�������
                    {
                        this.dtEnd.Checked = false;
                    }
                    else
                    {
                        this.dtEnd.Checked = true;//����С���ڣ����ý�������
                        this.dtEnd.Value = this.order.EndTime;
                    }
                }

                //ΪʲôҪ����������
                if (this.PanelEnd.Visible)//��ʱҽ������Ҫ��ʾֹͣʱ��
                {
                    this.dtEnd.Value = DateTime.Today.AddDays(1);
                    this.dtEnd.Checked = false;
                }
            }
            catch
            {
            }

            #endregion

            if (this.order.StockDept.ID == null || order.StockDept.ID == "")
            {
                order.StockDept.ID = ucInputItem1.FeeItem.User02; //�ۿ����,����Ҫ����Ҫע��
                order.StockDept.Name = ucInputItem1.FeeItem.User03;//�ۿ����
            }

            //{E57F256E-1722-4b36-809A-D46BD7A9AB55}
            ucOrderInputByType1.SetQtyValue -= new SetQtyValue(ucOrderInputByType1_SetQtyValue);
            ucOrderInputByType1.SetQtyValue += new SetQtyValue(ucOrderInputByType1_SetQtyValue);

            ////��������
            //if (order.HerbalQty == 0)
            //{
            //    order.HerbalQty = 1;
            //}
            //Components.Order.Classes.Function.ReComputeQty(order);

            this.order.OrderType = this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;

            //��ʾ������
            if (ShowOrder(this.order) == -1)
            {
                return;
            }

            //this.order.ReciptDept = CacheManager.LogEmpl.Dept.Clone();//��������
            order.ReciptDept.ID = CacheManager.LogEmpl.Dept.ID;

            //�����ҿ�������
            if (!string.IsNullOrEmpty(this.order.ReciptDept.ID) && string.IsNullOrEmpty(this.order.ExeDept.ID))
            {
                if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.order.ReciptDept.ID).SpecialFlag)))
                {
                    order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.order.ReciptDept.ID);
                }
            }

            this.order.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;//¼����
            this.order.Oper.Name = FS.FrameWork.Management.Connection.Operator.Name;

            if (this.order.OrderType.ID == "CZ")        //����ҽ��
            {
                if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {                    
                    string drugQuality = ((FS.HISFC.Models.Pharmacy.Item)this.order.Item).Quality.ID;
                    if (this.hsResQuality.ContainsKey(drugQuality))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.order.Item.Name + " ������ҩƷ������������ҽ��"),"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (this.txtQuantity.Enabled)
            {
                this.txtCombNo.Focus();
                //this.txtQuantity.Focus();//focus
            }
            else
            {
                this.txtCombNo.Focus();
                //this.ucOrderInputByType1.Focus();
            }

            this.ucOrderInputByType1.IsNew = true;//�µ�
            
            //��ʼ������Ŀ��Ϣ ����ҽ��Ƶ��
            Classes.Function.SetDefaultOrderFrequency(this.order);
            if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                this.order.Usage.ID = (this.order.Item as FS.HISFC.Models.Pharmacy.Item).Usage.ID;
                this.order.Usage.Name = Classes.Function.HelperUsage.GetName(this.order.Usage.ID);
            }
        
            this.ucOrderInputByType1.Order = this.order;//���ݸ�ѡ������
            dirty = true;
            myOrderChanged(this.order,ColmSet.ALL);
        }

        /// <summary>
        /// ucOrderInputByType1������������󣬷�����������ʾ
        /// </summary>
        /// <param name="inOrder"></param>
        void ucOrderInputByType1_SetQtyValue(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            this.txtQuantity.Text = inOrder.Qty.ToString(); //����

            #region ��λ

            if (inOrder.Item.ID != "999")
            {
                cmbUnit.Text = inOrder.Unit;
                inOrder.Item.PriceUnit = inOrder.Unit;
            }
            #endregion
        }

        #endregion

        #region  ������ҽ�������޸ĺ��� 

        /// <summary>
        /// ���ҽ����ʾ
        /// </summary>
        /// <param name="isResetBeginTime">�������ʱ���»�ȡ��ʼʱ��</param>
        public void Clear(bool isResetBeginTime)
        {
            try
            {
                this.order = null;
                //this.ucInputItem1.txtItemCode.Text = "";			//��Ŀ����
                //this.ucInputItem1.txtItemName.Text = "";			//��Ŀ����
                this.txtQuantity.Text = "1";					//����
                this.dtEnd.Checked = false;
                ucInputItem1.Clear();
                this.cmbUnit.Items.Clear();
                this.ucOrderInputByType1.Clear();
                this.txtCombNo.Value = this.GetMaxSubCombNo(null, -1);
                this.ucOrderInputByType1.IsQtyChanged = false;
                //Ĭ��ҽ������Ϊ����ҽ������ʱҽ������ֹ�������Ƶ�ǰ������ҽ������ѡ�񡰳�Ժ��ҩ��ʱ������һ�����˿�ҽ��ʱҲ�ǡ���Ժ��ҩ�������
                //this.cmbOrderType1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            int length = this.txtQuantity.DecimalPlaces == 0 ? this.txtQuantity.Value.ToString().Length : this.txtQuantity.Value.ToString().Length + 1 + this.txtQuantity.DecimalPlaces;
            this.txtQuantity.Select(0, length);
        }

        private void txtCombNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e != null && e.KeyChar == 13)
            {
                if (this.order.Item.ItemType != EnumItemType.Drug)//��ҩƷ���� �¼�
                {
                    //this.ucInputItem1.Focus();
                    this.ucOrderInputByType1.Focus();
                }
                else
                {
                    this.ucOrderInputByType1.Focus();
                }
            }
        }
        #endregion      

        private void txtCombNo_Enter(object sender, EventArgs e)
        {
            this.txtCombNo.Select(0, this.txtCombNo.Value.ToString().Length);
        }

        private void txtCombNo_Leave(object sender, EventArgs e)
        {            
            if (this.order != null)
            {
                if (this.txtCombNo.Value <= 0)
                {
                    MessageBox.Show("���ű������0��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal getValue = GetMaxSubCombNo(order, -1);

                //����¼������µķ��Ź�������
                if (((Int32)this.txtCombNo.Value) > getValue + 100)
                {
                    txtCombNo.Value = getValue;
                }

                if (((Int32)this.txtCombNo.Value) != this.order.SubCombNO)
                {
                    int subCombTemp = this.order.SubCombNO;
                    this.order.SubCombNO = (Int32)this.txtCombNo.Value;
                    order.SortID = 0;

                    if (this.GetSameSortID((Int32)this.txtCombNo.Value, this.order) == -1)
                    {
                        this.order.SubCombNO = subCombTemp;
                        this.txtCombNo.Value = subCombTemp;
                        this.txtCombNo.Focus();
                        return;
                    }

                    //�˴�ɾ������б���
                    if (this.DeleteSubComnNo != null)
                    {
                        this.DeleteSubComnNo(subCombTemp, order.OrderType.IsDecompose);
                    }

                    this.myOrderChanged(this.order, ColmSet.Z���);
                }
            }
        }

        /// <summary>
        /// ��������ͬ����
        /// </summary>
        private int GetSameSortID(int sortID, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;
            if (this.GetSameSubCombNoOrder(sortID, ref orderTemp) == -1)
            {
                return -1;
            }

            if (orderTemp != null)
            {
                if (Classes.Function.ValidComboOrder(order, orderTemp, true) == -1)
                {
                    return -1;
                }
                order.Frequency = orderTemp.Frequency;
                order.HerbalQty = orderTemp.HerbalQty;
                order.FirstUseNum = orderTemp.FirstUseNum;
                order.BeginTime = orderTemp.BeginTime;
                order.EndTime = orderTemp.EndTime;

                if (!Classes.Function.IsSameUsage(order.Usage.ID, orderTemp.Usage.ID))
                {
                    order.Usage = orderTemp.Usage;
                }
                order.InjectCount = orderTemp.InjectCount;
                //order.ExeDept = orderTemp.ExeDept;
                order.Combo.ID = orderTemp.Combo.ID;
                order.SubCombNO = orderTemp.SubCombNO;
                //order.SortID = orderTemp.SortID + 1;
                order.SortID = 0;
            }
            //�޸ķ���ʱ��ȡ�����
            else
            {
                order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();
                //order.SortID = FS.FrameWork.Function.NConvert.ToInt32((order.SubCombNO.ToString() + "00001"));
                order.SortID = 0;
            }
            return 1;
        }


        #region ͳһ������Ϣ������

        /// <summary>
        /// ͳһ����
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        public DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public DialogResult MessageBoxShow(string text)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text);
        }

        public DialogResult MessageBoxShow(string text, string caption)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption);
        }

        public DialogResult MessageBoxShow(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption, buttons, icon, defaultButton);
        }
        #endregion
        ///// <summary>
        ///// ��ʼʱ��
        ///// add by zhaorong at 2013-7-23 ��ʼʱ��Ĭ��ʱ�䣺00:00:00
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dtBegin_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTime beginTime = this.dtBegin.Value;
        //    this.dtBegin.Value = beginTime.Date
        //}
        ///// <summary>
        ///// ����ʱ��
        ///// add by zhaorong at 2013-7-23 ����ʱ��Ĭ��ʱ�䣺23:59:59
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dtEnd_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTime endTime = this.dtEnd.Value;
        //    this.dtEnd.Value =endTime
        //}
    }
    /// <summary>
    /// ҽ������
    /// </summary>
    public enum Operator
    {
        Add, 
        Modify, 
        Delete, 
        Query
    }
}
