using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// ���ﴦ������ ��Ŀѡ��ؼ�
    /// </summary>
    public partial class ucOutPatientRecipeItemSelect : UserControl
    {
        public ucOutPatientRecipeItemSelect()
        {
            InitializeComponent();
        }


        #region ����
        protected ToolTip tooltip = new ToolTip(); //ToolTip

        protected bool dirty = false;//���µ�ʱ��

        /// <summary>
        /// �Ƿ��д���Ȩ
        /// </summary>
        //public bool isHaveOrderPower = false;
        /// <summary>
        /// �Ƿ�����ϸ����Ȩ
        /// </summary>
        //public bool isControlDrugOrder = false;

        /// <summary>
        /// ��ǰ��
        /// </summary>
        public int CurrentRow = -1;

        /// <summary>
        /// �Ƿ�������ױ༭����
        /// </summary>
        public bool EditGroup = false;

        protected bool isLisDetail = false;

        /// <summary>
        /// �Ƿ�֪��ͬ����
        /// </summary>
        protected bool bPermission = false;

        /// <summary>
        /// �Ƿ�Ĭ�Ͽ������������ϸ���Ŀ��������ͬ
        /// </summary>
        private bool isDaysLikePreOrder = false;

        /// <summary>
        /// �Ƿ�Ƶ�Ρ��÷���ͬʱ�����޸���Ž�����ϣ���Ϻ�Ƶ�Ρ��÷�����
        /// </summary>
        private bool isChangeSubCombNoAlways = false;

        /// <summary>
        /// �Ƿ���㸨��
        /// </summary>
        public bool isChangeSubComb = false;

        /// <summary>
        /// �Ƿ���ʾ�����㸨��ѡ��
        /// </summary>
        private bool isChkChangeSubComb = false;

        /// <summary>
        /// ҩƷ�����Ƿ������޸�
        /// </summary>
        private bool isDrugCanEditQTY = true;

        /// <summary>
        /// �Ƿ���Կ�������ҽ��
        /// </summary>
        protected string pValue = "0";

        /// <summary>
        /// ������Ŀǰ�����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// ҽ���仯ʱ����
        /// </summary>
        public event ItemSelectedDelegate OrderChanged;
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public Operator OperatorType = Operator.Query;

        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler CatagoryChanged;

        /// <summary>
        /// �Ƿ��ڱ༭����״̬��
        /// </summary>
        private bool isEditGroup = false;

        /// <summary>
        /// �Ƿ��ڱ༭����״̬��
        /// </summary>
        public bool IsEditGroup
        {
            get
            {
                return isEditGroup;
            }
            set
            {
                isEditGroup = value;
            }
        }

        #endregion

        #region ����

        protected FS.HISFC.Models.Order.OutPatient.Order order;

        /// <summary>
        /// ҽ��
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order CurrOrder
        {
            get
            {
                return this.order;
            }
            set
            {
                if (DesignMode)
                {
                    return;
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    DeleteEvent();
                    this.order = value;

                    if (dirty == false)//���Ǳ仯ʱ��--����ʱ��
                    {
                        this.ucOrderInputByType1.IsNew = false;//�޸ľ�ҽ��

                        this.ucOrderInputByType1.Order = value;

                        this.ucInputItem1.FeeItem = this.order.Item;

                        ReadOrder(this.order);//��������ҽ��
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("CurrOrder" + ex.Message);
                }
                finally
                {
                    AddEvent();
                }
            }

        }

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = null;

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            set
            {
                if (DesignMode)
                {
                    return;
                }
                this.patientInfo = value;
                this.ucInputItem1.Patient = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾLis��ϸ��Ϣ
        /// </summary>
        public bool IsLisDetail
        {
            set
            {
                this.isLisDetail = value;
            }
        }

        #endregion

        #region �������

        public delegate int GetMaxSubCombNoEvent(FS.HISFC.Models.Order.OutPatient.Order order);

        /// <summary>
        /// ��ȡ��󷽺�
        /// </summary>
        public event GetMaxSubCombNoEvent GetMaxSubCombNo;

        public delegate int GetSameSubCombNoOrderEvent(int sortID, ref FS.HISFC.Models.Order.OutPatient.Order order);

        /// <summary>
        /// �����ͬ����ҽ��
        /// </summary>
        public event GetSameSubCombNoOrderEvent GetSameSubCombNoOrder;

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��������ǰ����,���ڼ�����Ŀʱ��ʾ������
        /// </summary>
        int progress = 1;

        /// <summary>
        /// ��ʼ������
        /// </summary>
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
            Classes.LogManager.Write("����ʼ��ʼ����Ŀѡ��ؼ���");

            #region ����tip
            tooltip.SetToolTip(this.ucInputItem1, "����ƴ�����ѯ������ҽ��(ESCȡ���б�)");
            tooltip.SetToolTip(this.txtQTY, "����������(�س��������)");
            #endregion
            try
            {
                //this.ucInputItem1.DeptCode = "";//���ҿ���ȫ����Ŀ
                this.ucInputItem1.DeptCode = CacheManager.LogEmpl.Dept.ID;
                this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
                this.ucInputItem1.FontSize = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                this.ucOrderInputByType1.InitControl(null, null, null);

                this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.Clinic;

                //��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��
                this.ucInputItem1.DrugSendType = "O";

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����Ŀ�б���Ϣ..", 0, false);
                Application.DoEvents();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress, 100);
                Application.DoEvents();

                this.ucInputItem1.Init();//��ʼ����Ŀ�б�

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


                //��Ź���{98522448-B392-4d67-8C4D-A10F605AFDA5}
                if (this.GetMaxSubCombNo != null)
                {
                    this.txtCombNo.Text = FS.FrameWork.Function.NConvert.ToDecimal(this.GetMaxSubCombNo(null)).ToString();
                }

                this.ucInputItem1.Focus();

                IBeforeAddItem = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem)) as FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem;

            }
            catch { }

            //�����Ƿ���Կ�������ҽ��
            this.pValue = Classes.Function.GetBatchControlParam("200004", false, "0");

            isChangeSubCombNoAlways = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ29", false, "0"));
            isDaysLikePreOrder = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ30", false, "0"));
            isChkChangeSubComb = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ50", false, "0"));
            if (isChkChangeSubComb)
            {
                this.chkDrugEmerce.Visible = true;
            }

            this.AddEvent();

            Classes.LogManager.Write("��������ʼ����Ŀѡ��ؼ���");
        }

        void cmbUnit_Leave(object sender, EventArgs e)
        {
            this.cmbUnit_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// �޸���Ŀ���
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            //try
            //{
            //    FS.FrameWork.Models.NeuObject obj = sender;
            //    if (obj.ID.Length > 0 && obj.ID.Substring(0, 1) == "M")
            //    {
            //        this.ucInputItem1.IsCanInputName = false;
            //    }
            //    else
            //    {
            //        this.ucInputItem1.IsCanInputName = true;
            //    }
            //}
            //catch { }
        }

        #endregion

        #region ����

        /// <summary>
        /// �����¼�
        /// </summary>
        private void AddEvent()
        {
            DeleteEvent();

            //��λ
            cmbUnit.Leave += new EventHandler(cmbUnit_Leave);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.Leave += new EventHandler(cmbUnit_Leave);

            //����
            this.txtQTY.ValueChanged += new System.EventHandler(this.txtQTY_ValueChanged);
            //this.txtQTY.ValueChanged += new EventHandler(txtQTY_ValueChanged);
            txtQTY.ValueChanged += new EventHandler(txtQTY_Leave); 
            this.txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            this.txtQTY.Leave += new EventHandler(txtQTY_Leave);

            //��Ŀÿ������Ƶ�εȱ仯
            this.ucOrderInputByType1.ItemSelected += new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.ucOrderInputByType1.FocusLost += new ucOrderInputByType.FocusLostHandler(ucOrderInputByType1_Leave);

            //ѡ����Ŀ�¼�
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            //���
            txtCombNo.KeyDown += new KeyEventHandler(txtCombNo_KeyDown);
            txtCombNo.Leave += new EventHandler(txtCombNo_Leave);
        }

        /// <summary>
        /// ɾ���¼�
        /// </summary>
        private void DeleteEvent()
        {
            //��λ
            cmbUnit.Leave -= new EventHandler(cmbUnit_Leave);
            this.cmbUnit.SelectedIndexChanged -= new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.Leave -= new EventHandler(cmbUnit_Leave);

            //����
            this.txtQTY.ValueChanged -= new System.EventHandler(this.txtQTY_ValueChanged);
            this.txtQTY.ValueChanged -= new EventHandler(txtQTY_ValueChanged);
            this.txtQTY.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            this.txtQTY.Leave -= new EventHandler(txtQTY_Leave);

            //��Ŀÿ������Ƶ�εȱ仯
            this.ucOrderInputByType1.ItemSelected -= new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.ucOrderInputByType1.FocusLost -= new ucOrderInputByType.FocusLostHandler(ucOrderInputByType1_Leave);

            //ѡ����Ŀ�¼�
            this.ucInputItem1.SelectedItem -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            //���
            txtCombNo.KeyDown -= new KeyEventHandler(txtCombNo_KeyDown);
            txtCombNo.Leave -= new EventHandler(txtCombNo_Leave);
        }

        private ArrayList OrderCatatagory()
        {
            System.Collections.ArrayList al = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "ȫ��";
            al.Add(objAll);
            //����Щ����

            System.Collections.ArrayList rAl = new ArrayList();
            //foreach (FS.FrameWork.Models.NeuObject obj in al)
            //{
            //if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MR")//��ҩƷ��ת�ƣ�ת��
            //{

            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MF")//��ʳ
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UN")//������
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UJ")	//����
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MC")//����
            //{
            //}
            //else
            //{
            //rAl.Add(obj);
            //}
            //}
            rAl = al;
            return rAl;
        }

        /// <summary>
        /// ������������Ϣ�Ƿ�ɼ�
        /// {6531391D-6FB3-47f9-A634-BE11D8C830E0}
        /// </summary>
        /// <param name="isVisible"></param>
        private void SetQtyControlVisible(bool isVisible)
        {

            /*
            txtQTY.Visible = isVisible;
            txtQTY.TabStop = isVisible;
            neuLabel1.Visible = isVisible;
            neuLabel1.TabStop = isVisible;
            cmbUnit.Visible = isVisible;
            cmbUnit.TabStop = isVisible;
             * */

            txtQTY.Visible = true;
            txtQTY.TabStop = true;
            neuLabel1.Visible = true;
            neuLabel1.TabStop = true;
            cmbUnit.Visible = true;
            cmbUnit.TabStop = true;
        }

        /// <summary>
        /// ��ռ�¼������
        /// </summary>
        public void ClearDays()
        {
            this.ucOrderInputByType1.UseDays = 1;
        }

        /// <summary>
        /// ���ҽ����ʾ
        /// </summary>
        /// <param name="isGetFoucus"></param>
        public void Clear(bool isGetFoucus)
        {
            try
            {
                this.order = null;
                this.ucInputItem1.txtItemCode.Text = "";			//��Ŀ����
                this.ucInputItem1.txtItemName.Text = "";			//��Ŀ����
                //this.txtQTY.Text = "";					//����
                SetQtyValue("");
                this.ucInputItem1.SetVisibleForms(false);
                this.cmbUnit.Items.Clear();
                this.ucOrderInputByType1.Clear();
                isGetFoucus = true;
                if (isGetFoucus)
                {
                    this.ucInputItem1.Focus();
                }
                this.ucOrderInputByType1.IsQtyChanged = false;
            }
            catch (Exception ex)
            {
                this.MessageBoxShow(ex.Message);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="qty"></param>
        private void SetQtyValue(string qty)
        {
            try
            {
                if (string.IsNullOrEmpty(qty))
                {
                    txtQTY.DecimalPlaces = 0;
                    txtQTY.Text = "";
                }
                else
                {
                    decimal decQTY = FS.FrameWork.Function.NConvert.ToDecimal(qty);

                    txtQTY.DecimalPlaces = decQTY.ToString("F6").TrimEnd('0').Length - decQTY.ToString("F6").IndexOf('.');
                    txtQTY.Text = qty;
                }
            }
            catch
            {
                txtQTY.Text = qty;
            }
        }

        /// <summary>
        /// ��ȡҽ����Ϣ-���ƿؼ���ʾ״̬
        /// </summary>
        /// <param name="myOrder"></param>
        protected int ReadOrder(FS.HISFC.Models.Order.OutPatient.Order myOrder)
        {
            if (myOrder == null)
            {
                return 0;
            }

            txtQTY.Enabled = true;//ReadOnly���ǿ���ͨ�����¼�ͷ�޸�����
            cmbUnit.Enabled = true;

            //��Ŀ
            if (myOrder.Item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                FS.HISFC.Models.Pharmacy.Item item = ((FS.HISFC.Models.Pharmacy.Item)myOrder.Item);
                if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                {
                    myOrder.Frequency.ID = Components.Order.Classes.Function.GetDefaultFrequencyID();//����ҽ��Ĭ��Ϊ��Ҫʱִ��
                }

                //this.txtQTY.Text = myOrder.Qty.ToString(); //����
                SetQtyValue(myOrder.Qty.ToString());//����
                this.cmbUnit.Items.Clear();

                if (myOrder.Item.ID != "999") //�Զ���ҩƷ
                {
                    if (item.PackQty == 0)//����װ����
                    {
                        MessageBoxShow("��ҩƷ�İ�װ����Ϊ�㣡");
                        return -1;
                    }
                    if (item.BaseDose == 0)//����������
                    {
                        MessageBoxShow("��ҩƷ�Ļ�������Ϊ�㣡");
                        return -1;
                    }
                    if (item.DosageForm.ID == "")//������
                    {
                        MessageBoxShow("��ҩƷ�ļ���Ϊ�գ�");
                        return -1;
                    }
                }

                if (myOrder.StockDept.ID == null || myOrder.StockDept.ID == "")
                {
                    myOrder.StockDept.ID = item.User02; //ȡҩҩ��,����Ҫ����Ҫע��
                    myOrder.StockDept.Name = item.User03;//ȡҩҩ��
                }

                #region ��λ

                if (myOrder.Item.ID == "999")
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//���Ը���
                    this.cmbUnit.Enabled = this.txtQTY.Enabled;
                }
                else
                {
                    this.cmbUnit.Items.Add((this.ucInputItem1.FeeItem as FS.HISFC.Models.Pharmacy.Item).MinUnit);//��λ 
                    this.cmbUnit.Items.Add((this.ucInputItem1.FeeItem as FS.HISFC.Models.Pharmacy.Item).PackUnit);//��λ
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;//ֻ��ѡ��
                    this.cmbUnit.Enabled = true;

                    if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                    {
                        if (this.cmbUnit.Items.Count > 0)
                        {
                            this.cmbUnit.SelectedIndex = 0;
                        }
                        myOrder.Unit = this.cmbUnit.Text;
                    }
                    else
                    {
                        this.cmbUnit.Text = myOrder.Unit;
                    }

                    // 1 ��װ��λ����ȡ�� 3 ��װ��λÿ����ȡ��
                    Components.Order.Classes.Function.GetSplitType(ref myOrder);

                    if (((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).LZSplitType != null
                        && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).SplitType)
                        && "1��3".Contains(((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).SplitType))
                    {
                        cmbUnit.Enabled = false;
                    }
                    else
                    {
                        this.cmbUnit.Enabled = true;
                    }
                }

                #endregion

                if (myOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    txtQTY.Enabled = false;//ReadOnly���ǿ���ͨ�����¼�ͷ�޸�����
                    cmbUnit.Enabled = false;
                }

                //ҩƷ�����Ƿ������޸�
                this.isDrugCanEditQTY = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ51", false, "1"));
                if (!isDrugCanEditQTY)
                {
                    if ("P".Equals(myOrder.Item.SysClass.ID.ToString())
                        || "PCZ".Equals(myOrder.Item.SysClass.ID.ToString()))
                    {
                        if (!"999".Equals(myOrder.Item.ID))
                        {
                            txtQTY.Enabled = false;//ReadOnly���ǿ���ͨ�����¼�ͷ�޸�����
                            cmbUnit.Enabled = false;
                        }
                    }
                }
                
            }
            else if (myOrder.Item.ItemType == EnumItemType.UnDrug)//��ҩƷ
            {
                FS.HISFC.Models.Fee.Item.Undrug item = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item);

                //���ִ�п���Ϊ��--�������ƿ���
                //if (myOrder.ExeDept.ID == "")
                //{
                //    if (item.ExecDept == "")
                //    {
                //        myOrder.ExeDept = myOrder.Patient.PVisit.PatientLocation.Dept.Clone();////ִ�п���?????������Ҫ�޸�
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
                if (myOrder.CheckPartRecord == "" && myOrder.Item.SysClass.ID.ToString() == "UC") //�����岿λ
                {
                    myOrder.CheckPartRecord = item.CheckBody;
                }
                if (myOrder.Sample.Name == "" && myOrder.Item.SysClass.ID.ToString() == "UL") //�����岿λ
                {
                    myOrder.Sample.Name = item.CheckBody;
                }
                if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                {
                   myOrder.Frequency.ID = Components.Order.Classes.Function.GetDefaultFrequencyID();//����ҽ��Ĭ��QD
                }

                //this.ShowTotal(true);

                this.cmbUnit.Items.Clear();

                if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                {
                    string unit = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).PriceUnit;
                    if (unit == null || unit == "") unit = "��";
                    this.cmbUnit.Items.Add(unit);
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;

                        myOrder.Unit = this.cmbUnit.Text;
                    }
                }
                else
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.Text = myOrder.Unit;
                }
                if (myOrder.Qty == 0)
                {
                    //this.txtQTY.Text = "1.00"; //����
                    SetQtyValue("1.00");
                    myOrder.Qty = 1;
                }
                else
                {
                    //this.txtQTY.Text = myOrder.Qty.ToString();
                    SetQtyValue(myOrder.Qty.ToString());
                }
            }
            else
            {
                MessageBoxShow("�޷�ʶ������ͣ�");
                return -1;
            }

            //��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
            if (myOrder.SubCombNO > 0)
            {
                this.txtCombNo.Text = FS.FrameWork.Function.NConvert.ToDecimal(myOrder.SubCombNO).ToString();
            }
            else
            {
                this.txtCombNo.Text = this.GetMaxSubCombNo(myOrder).ToString();
            }

            return 0;
        }

        /// <summary>
        /// ������ҽ��
        /// </summary>
        protected void SetNewOrder()
        {
            if (this.DesignMode)
                return;
            //�������ҽ������
            this.order = new FS.HISFC.Models.Order.OutPatient.Order();//��������ҽ��

            dirty = false;
            try
            {
                if (this.ucInputItem1.FeeItem.ID == "999")//�Լ�¼����Ŀ
                {
                    this.order.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                }
                else
                {
                    this.SetQtyControlVisible(false);

                    //ҩƷ
                    if (this.ucInputItem1.FeeItem.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                    {
                        //this.order.Item = pharmacyManager.GetItem(this.ucInputItem1.FeeItem.ID);
                        order.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ucInputItem1.FeeItem.ID);
                        this.order.Item.User01 = this.ucInputItem1.FeeItem.User01;
                        this.order.Item.User02 = this.ucInputItem1.FeeItem.User02;//����ȡҩҩ��
                        this.order.Item.User03 = this.ucInputItem1.FeeItem.User03;

                        this.SetQtyControlVisible(false);
                    }
                    else//��ҩƷ
                    {
                        this.SetQtyControlVisible(true);

                        try
                        {
                            if (((FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem).PriceUnit != "[������]")
                            {
                                FS.HISFC.Models.Fee.Item.Undrug itemTemp = null;
                                //itemTemp = itemManager.GetItem(this.ucInputItem1.FeeItem.ID);
                                itemTemp = SOC.HISFC.BizProcess.Cache.Fee.GetItem(ucInputItem1.FeeItem.ID);

                                this.order.Item = itemTemp;

                                //ִ�п��Ҹ�ֵ ������Ŀͬʱ��ִֵ�п��� 
                                //----Edit By liangjz 07-03
                                //if (itemTemp.ExecDept != null && itemTemp.ExecDept != "")
                                //{
                                //    this.order.ExeDept.ID = itemTemp.ExecDept;

                                //    if (order.ExeDept.ID.Length > 4)
                                //    {
                                //        order.ExeDept.ID = order.ExeDept.ID.Substring(0, 4);
                                //    }
                                //}
                                //else
                                //{
                                //    this.order.ExeDept = this.order.Patient.PVisit.PatientLocation.Dept.Clone();
                                //}

                                //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                                //string deptid = this.ucOrderInputByType1.SetExecDept(this.order.Item.ID);
                                //if (!string.IsNullOrEmpty(deptid))
                                //{
                                //    this.order.ExeDept.ID = deptid;
                                //}
                                //-----

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
                            else
                            {
                                FS.HISFC.Models.Fee.Item.Undrug itemTemp = null;
                                itemTemp = (FS.HISFC.Models.Fee.Item.Undrug)this.ucInputItem1.FeeItem;
                                this.order.Item = itemTemp;
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
                                //this.order.Item.MinFee.ID = "fh";
                            }
                        }
                        catch
                        {
                            MessageBoxShow("ת������!", "ucItemSelect");
                        }
                    }

                }
            }
            catch
            {
                return;
            }

            this.order.SubCombNO = this.GetMaxSubCombNo(this.order);

            this.ucOrderInputByType1.SetQtyValue += new SetQtyValue(ucOrderInputByType1_SetQtyValue);

            #region
            ////��ʾ������  add by liuww 2012-06-08
            if (ReadOrder(this.order) == -1)
            {
                return;
            }
            #endregion

            //����ҽ������ʱ��
            FS.FrameWork.Management.DataBaseManger manager = new FS.FrameWork.Management.DataBaseManger();
            DateTime dtNow = manager.GetDateTimeFromSysDateTime();

            this.order.MOTime = dtNow;//����ʱ��
            this.order.BeginTime = dtNow;//��ʼʱ��
            this.order.Item.PriceUnit = this.cmbUnit.Text;
            this.order.Unit = this.cmbUnit.Text;

            this.order.ReciptDept = CacheManager.LogEmpl.Dept.Clone();//��������
            this.order.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;//¼����
            this.order.Oper.Name = FS.FrameWork.Management.Connection.Operator.Name;

            //ҽ������
            //this.order.OrderType = this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;


            if (this.txtQTY.Enabled)
            {
                this.txtQTY.Focus();//focus
                this.txtQTY.Select(0, this.txtQTY.Text.Length);
            }
            else
            {
                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.txtCombNo.Focus();
                //this.ucOrderInputByType1.Focus();
            }
            //if (this.cmbUnit.Items.Count > 0) this.cmbUnit.SelectedIndex = 0;//Ĭ��ѡ���һ����
            this.ucOrderInputByType1.IsNew = true;//�µ�

            //��ʼ������Ŀ��Ϣ ����ҽ��Ƶ���÷�            
            if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                this.order.Usage.ID = (this.order.Item as FS.HISFC.Models.Pharmacy.Item).Usage.ID;
                this.order.Usage.Name = Order.Classes.Function.HelperUsage.GetName(this.order.Usage.ID);
            }
            else
            {
            }

            if (this.order.HerbalQty == 0)
            {
                //this.order.HerbalQty = 1;//���²�ҩ����
                if (isDaysLikePreOrder & this.order.Item.ItemType == EnumItemType.Drug)
                {
                    this.order.HerbalQty = ucOrderInputByType1.UseDays > 0 ? ucOrderInputByType1.UseDays : 1;
                }
                else
                {
                    this.order.HerbalQty = 1;//���²�ҩ����
                }
            }

            dirty = true; //�µ�
            this.ucOrderInputByType1.Order = this.order;//���ݸ�ѡ������
            dirty = false;//���¹���

            this.myOrderChanged(this.order, EnumOrderFieldList.Item);
        }

        /// <summary>
        /// ��������������ʾ������
        /// </summary>
        /// <param name="inOrder"></param>
        void ucOrderInputByType1_SetQtyValue(FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            this.txtQTY.Text = outOrder.Qty.ToString(); //����

            #region ��λ

            if (outOrder.Item.ID == "999")
            {
                this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//���Ը���
                this.cmbUnit.Enabled = this.txtQTY.Enabled;
            }
            else
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    // 1 ��װ��λ����ȡ�� 3 ��װ��λÿ����ȡ��
                    Components.Order.Classes.Function.GetSplitType(ref outOrder);

                    if (((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).LZSplitType != null
                        && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).SplitType)
                        && "1��3".Contains(((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).SplitType))
                    {
                        cmbUnit.Enabled = false;
                    }
                    else
                    {
                        this.cmbUnit.Enabled = true;
                    }

                    //ҩƷ�����Ƿ������޸�
                    this.isDrugCanEditQTY = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ51", false, "1"));
                    if (!isDrugCanEditQTY)
                    {
                        if ("P".Equals(outOrder.Item.SysClass.ID.ToString())
                            || "PCZ".Equals(outOrder.Item.SysClass.ID.ToString()))
                        {
                            cmbUnit.Enabled = false;
                        }
                    }
                }
                cmbUnit.Text = outOrder.Unit;
                outOrder.Item.PriceUnit = outOrder.Unit;
            }
            #endregion
        }

        /// <summary>
        /// ��������ͬ����
        /// ���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
        /// </summary>
        private int GetSameSortID(int sortID, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            FS.HISFC.Models.Order.OutPatient.Order orderTemp = null;
            if (this.GetSameSubCombNoOrder(sortID, ref orderTemp) == -1)
            {
                return -1;
            }

            if (orderTemp != null)
            {
                //���ӿ��Ʋ�������ͬ�÷���Ƶ�εģ��޸�����Ƿ��������
                if (Classes.Function.ValidComboOrder(order, orderTemp, true, isChangeSubCombNoAlways) == -1)
                {
                    return -1;
                }

                order.Frequency = orderTemp.Frequency.Clone();
                order.HerbalQty = orderTemp.HerbalQty;

                if (!Classes.Function.IsSameUsage(order.Usage.ID, orderTemp.Usage.ID))
                {
                    order.Usage = orderTemp.Usage;
                }
                order.InjectCount = orderTemp.InjectCount;
                //order.ExeDept = orderTemp.ExeDept;
                order.Combo.ID = orderTemp.Combo.ID;
                order.SubCombNO = orderTemp.SubCombNO;
            }
            //�޸ķ���ʱ��ȡ�����
            else
            {
                order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();
            }
            return 1;
        }

        /// <summary>
        /// �Ƿ���Ը������¼��޸�ѡ�񴦷�
        /// </summary>
        /// <returns></returns>
        public bool IsCanChangeSelectOrder()
        {
            return this.ucOrderInputByType1.IsCanChangeSelectOrder();
        }

        protected void myOrderChanged(object sender, EnumOrderFieldList enumOrderFieldList)
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

                this.order = sender as FS.HISFC.Models.Order.OutPatient.Order;//�ؼ������Ķ���

                this.OrderChanged(order, enumOrderFieldList);
            }
            catch (Exception ex)
            {
                MessageBoxShow(ex.Message);
                return;
            }
        }

        /// <summary>
        /// ��������ҽ������֣�
        /// </summary>
        private void DealGroupOrder(FS.FrameWork.Models.NeuObject group)
        {
            if (group == null || group.ID.Length <= 0)
            {
                return;
            }
            ArrayList alGroupDetail = null;

            try
            {
                ////alGroupDetail = this.CacheManager.InterMgr.GetComGroupTailByGroupID(group.ID);
            }
            catch
            {
                MessageBoxShow("���������ϸ��Ϣ����");
                return;
            }
            if (alGroupDetail == null || alGroupDetail.Count <= 0)
            {
                return;
            }
            ////OutPatient.frmGroupDetail frm = new frmGroupDetail();

            ////frm.alGroupDel = alGroupDetail;
            ////frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ////frm.ShowDialog();
            ////if (frm.alOrderItem.Count <= 0)
            ////{
            ////    return;
            ////}
            ////for (int i = 0; i < frm.alOrderItem.Count; i++)
            ////{
            ////    this.ucItem1.FeeItem = (FS.neHISFC.Components.Object.neuObject)frm.alOrderItem[i];
            ////    this.CurrentRow = -1;
            ////    this.SetOrder();
            ////}
        }

        protected virtual void ucOrderInputByType1_ItemSelected(FS.HISFC.Models.Order.OutPatient.Order order, EnumOrderFieldList changedField)
        {
            DeleteEvent();
            dirty = true;

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                this.cmbUnit.Tag = order.Unit;
                this.cmbUnit.Text = order.Unit;

                SetQtyControlVisible(false);
            }
            else
            {
                SetQtyControlVisible(true);
            }
            //this.txtQTY.Text = order.Qty.ToString();
            SetQtyValue(order.Qty.ToString());

            this.myOrderChanged(order, changedField);
            dirty = false;
            AddEvent();
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����仯-������һ�������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null)
            {
                return;
            }

            if (e == null || e.KeyChar == 13)
            {
                if (e != null)
                {
                    //if (this.order.Item.IsPharmacy == false)//��ҩƷ���� �¼�
                    if (this.order.Item.ItemType != EnumItemType.Drug)//��ҩƷ���� �¼�
                    {
                        //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                        this.txtCombNo.Focus();
                        this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        //this.ucOrderInputByType1.Focus();
                    }
                    else
                    {
                        if (this.cmbUnit.CanFocus)
                        {
                            this.cmbUnit.Focus();
                        }
                        else
                        {
                            this.txtCombNo.Focus();
                            this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        }
                    }
                }
            }
        }

        private void txtQTY_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
                return;

            if (this.order == null)
            {
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }
            else if (this.order.Qty == FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text))
            {
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }
            //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
            else if (this.order.Qty == 0)
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
            }
            else
            {
                this.ucOrderInputByType1.IsQtyChanged = true;
            }

            if (this.order.Qty != FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text))
            {
                this.order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text);
                myOrderChanged(this.order, EnumOrderFieldList.Qty);
            }

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (order.Item.SysClass.ID.ToString() != "PCC")
                {
                    if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                    {
                        Components.Order.Classes.Function.ShowBalloonTip(4, "����", "��Ŀ��" + order.Item.Name + "������������ע���޸ģ�\r\n\r\n������������С����", System.Windows.Forms.ToolTipIcon.Warning);
                    }
                }
            }
        }

        private void txtQTY_Enter(object sender, EventArgs e)
        {
            this.txtQTY.Select(0, this.txtQTY.Text.Length);
        }

        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                DeleteEvent();
                this.ucInputItem1.SetVisibleForms(false);

                if (DesignMode)
                {
                    return;
                }

                if (this.ucInputItem1.FeeItem == null)
                    return;

                this.ucOrderInputByType1.IsQtyChanged = false;

                if (!this.EditGroup)		//��ʵ�ֶ������޸Ĺ���ʱ �����֪��ͬ����������ж�
                {
                    if (this.patientInfo != null)
                    {
                        #region �ж���Ŀ����Ȩ��

                        string error = "";

                        int ret = 1;

                        FS.HISFC.Models.Order.OutPatient.Order addOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                        addOrder.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;

                        //Ȩ���ж�
                        ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(addOrder, CacheManager.LogEmpl, CacheManager.LogEmpl.Dept, FS.HISFC.Models.Base.DoctorPrivType.LevelDrug, true, ref error);

                        if (ret <= 0)
                        {
                            MessageBoxShow(error, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        //����ʷ�ж�
                        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.patientInfo.PID, addOrder, ref error);

                        if (ret <= 0)
                        {
                            return;
                        }

                        #endregion

                        //�ж�ȱҩ��ͣ��
                        FS.HISFC.Models.Pharmacy.Item itemObj = null;
                        FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
                        string errInfo = "";

                        FS.HISFC.Models.Order.Order orderTemp = new FS.HISFC.Models.Order.Order();
                        orderTemp.Item = (Item)this.ucInputItem1.FeeItem;
                        if (orderTemp.Item.ItemType == EnumItemType.Drug)
                        {
                            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(null,
                                new FS.FrameWork.Models.NeuObject(this.ucInputItem1.FeeItem.User02, this.ucInputItem1.FeeItem.User03, ""), null,
                                (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem,
                                  true, ref itemObj, ref storage, ref errInfo) <= 0)
                            {
                                MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            //FS.HISFC.Models.Fee.Item.Undrug undrugObj = null;
                            //if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckUnDrugState(orderTemp, ref undrugObj, ref errInfo) == -1)
                            //{
                            //    MessageBoxShow(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                        }

                        if (this.patientInfo != null)
                        {
                            #region ҩ������ж� {30C09D02-8A87-4078-9420-023A6AC61DE9}
                            ArrayList alDrugAllergy = CacheManager.DiagMgr.QueryDrugAllergyByNo(this.patientInfo.PID.CardNO);

                            if (alDrugAllergy != null && alDrugAllergy.Count > 0)
                            {
                                foreach (FS.HISFC.Models.HealthRecord.Diagnose drugAllergyObj in alDrugAllergy)
                                {
                                    if ((this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ID == drugAllergyObj.DiagInfo.ID)
                                    {
                                        MessageBoxShow("���߶Դ���ҩ�������������ѡ��");
                                        return;
                                    }
                                }
                            }
                            #endregion
                        }

                        //ѡ����Ŀ�ж�
                        if (IBeforeAddItem != null)
                        {
                            ArrayList al = new ArrayList();
                            al.Add(orderTemp);

                            if (this.IBeforeAddItem.OnBeforeAddItemForOutPatient(this.patientInfo, CacheManager.LogEmpl, CacheManager.LogEmpl.Dept, al) == -1)
                            {
                                if (!string.IsNullOrEmpty(IBeforeAddItem.ErrInfo))
                                {
                                    MessageBoxShow(IBeforeAddItem.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                return;
                            }
                        }
                    }
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //���ظ�
                {
                    if ((this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ItemType == EnumItemType.Drug)
                    {
                        SetQtyControlVisible(false);
                        //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                        this.txtCombNo.Focus();
                        this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        //this.ucOrderInputByType1.Focus();
                    }
                    else
                    {
                        SetQtyControlVisible(true);

                        this.txtQTY.Focus();
                        this.txtQTY.Select(0, this.txtQTY.Text.Length);
                    }

                    return;
                }

                //��Ŀ�仯-ָ������
                this.CurrentRow = -1;

                this.OperatorType = Operator.Add;

                //������ҽ��
                this.SetNewOrder();

                //�����仯
                if (((ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ItemType == EnumItemType.Drug
                    && ucInputItem1.FeeItem.ID != "999")
                    || !this.txtQTY.Enabled)
                {
                    SetQtyControlVisible(false);
                    //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    this.txtCombNo.Focus();
                    this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                    //this.ucOrderInputByType1.Focus();
                }
                else
                {
                    SetQtyControlVisible(true);

                    this.txtQTY.Focus();
                    this.txtQTY.Select(0, this.txtQTY.Text.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucInputItem1_SelectedItem" + ex.Message);
            }
            finally
            {
                AddEvent();
            }
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
        /// ��λkeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null) return;
            if (e == null || e.KeyChar == 13)
            {
                //���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.txtCombNo.Focus();
                this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
            }
        }

        /// <summary>
        /// ��λѡ��仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string unit = this.cmbUnit.Text.Trim();
                if (FS.FrameWork.Public.String.ValidMaxLengh(unit, 16) == false)
                {
                    MessageBoxShow("��λ����!", "��ʾ");
                    return;
                }

                if (this.order.Unit != unit)
                {
                    #region �ж��Ƿ�����С��λ

                    if (this.order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.order.Item.ID == "999")
                        {
                            this.order.MinunitFlag = "1";
                        }
                        else
                        {
                            if (this.cmbUnit.SelectedIndex == 0)
                            {
                                this.order.MinunitFlag = "1";
                            }
                            else
                            {
                                this.order.MinunitFlag = "0";
                            }
                        }
                    }
                    #endregion

                    this.order.Unit = unit;//���µ�λ
                    myOrderChanged(this.order, EnumOrderFieldList.Unit);
                }
            }
            catch { }
        }

        #endregion

        private void txtQTY_ValueChanged(object sender, EventArgs e)
        {
            int places = txtQTY.Value.ToString("F6").TrimEnd('0').Length - txtQTY.Value.ToString("F6").IndexOf('.');

            this.txtQTY.DecimalPlaces = places;

            return;
        }

        /// <summary>
        /// ��ȡ��ǰ�ؼ������� ֻ��������ת
        /// </summary>
        /// <returns></returns>
        public bool RecycleTab()
        {
            return ucOrderInputByType1.RecycleTab();
        }


        #region ���ӷ��Ź��� {98522448-B392-4d67-8C4D-A10F605AFDA5}

        private void txtCombNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucOrderInputByType1.Focus();
            }
        }

        private void txtCombNo_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
            {
                return;
            }

            int i = (FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text));
            txtCombNo.Value = i;

            if ((FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text)) != this.order.SubCombNO)
            {
                int subCombTemp = this.order.SubCombNO;
                this.order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text);

                if (this.GetSameSortID(FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text), this.order) == -1)
                {
                    this.order.SubCombNO = subCombTemp;
                    this.txtCombNo.Text = subCombTemp.ToString();
                    this.txtCombNo.Focus();
                    return;
                }
                //Components.Order.Classes.Function.ReComputeQty(order);

                this.CurrOrder = this.order;
                // ��һ�ν�������ҽ���������棬������׿��������޸ĵ�һ��ҽ�������ʵ����Ϲ��ܣ����ִ���{81639443-4D8D-4e2e-8D9B-48F52F6F12AC}
                //this.OrderChanged(this.order, EnumOrderFieldList.SubComb);
                this.myOrderChanged(this.order, EnumOrderFieldList.SubComb);
            }
        }
        //���Ĳ�����ʾ���㸨��

        private void chkDrugEmerce_CheckedChanged(object sender, EventArgs e)
        {
            this.isChangeSubComb = this.chkDrugEmerce.Checked;
        }
        //Ĭ�ϼ��㸨��
        public void changeChkDrugEmerce()
        {
            this.chkDrugEmerce.Checked = false;
            this.isChangeSubComb = false;
        }
        #endregion

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
    }

    ///// <summary>
    ///// ҽ������
    ///// </summary>
    //public enum Operator
    //{
    //    Add,
    //    Modify,
    //    Delete,
    //    Query
    //}

    ///// <summary>
    ///// ҽ���仯����
    ///// </summary>
    //public enum EnumOrderFieldList
    //{
    //    /// <summary>
    //    /// !
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("!")]
    //    WarningFlag = 0,

    //    /// <summary>
    //    /// ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��")]
    //    Warning = 1,

    //    /// <summary>
    //    /// ҽ������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ҽ������")]
    //    OrderType = 2,

    //    /// <summary>
    //    /// ҽ����ˮ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ҽ����ˮ��")]
    //    OrderID = 3,

    //    /// <summary>
    //    /// ҽ��״̬
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ҽ��״̬")]
    //    Status = 4,

    //    /// <summary>
    //    /// ��Ϻ�
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��Ϻ�")]
    //    CombNo = 5,

    //    /// <summary>
    //    /// ��ҩ
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��ҩ")]
    //    MainDrug = 6,

    //    /// <summary>
    //    /// ��Ŀ����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��Ŀ����")]
    //    ItemCode = 7,

    //    /// <summary>
    //    /// ҽ������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ҽ������")]
    //    ItemName = 8,

    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("���")]
    //    CombFlag = 9,

    //    /// <summary>
    //    /// ����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����")]
    //    Qty = 10,

    //    /// <summary>
    //    /// ������λ
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("������λ")]
    //    Unit = 11,

    //    /// <summary>
    //    /// ÿ������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ÿ������")]
    //    DoseOnce = 12,

    //    /// <summary>
    //    /// ��λ
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��λ")]
    //    DoseUnit = 13,

    //    /// <summary>
    //    /// ����/����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����/����")]
    //    HerbalQty = 14,

    //    /// <summary>
    //    /// Ƶ�α���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("Ƶ�α���")]
    //    FrequencyCode = 15,

    //    /// <summary>
    //    /// Ƶ������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("Ƶ������")]
    //    Frequency = 16,

    //    /// <summary>
    //    /// �÷�����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("�÷�����")]
    //    UsageCode = 17,

    //    /// <summary>
    //    /// �÷�����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("�÷�����")]
    //    Usage = 18,

    //    /// <summary>
    //    /// Ժע����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("Ժע����")]
    //    InjNum = 19,

    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("���")]
    //    Specs = 20,

    //    /// <summary>
    //    /// ����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����")]
    //    Price = 21,

    //    /// <summary>
    //    /// ���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("���")]
    //    TotalCost = 22,

    //    /// <summary>
    //    /// ��ʼʱ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��ʼʱ��")]
    //    BeginDate = 23,

    //    /// <summary>
    //    /// ����ҽ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����ҽ��")]
    //    ReciptDoct = 24,

    //    /// <summary>
    //    /// ִ�п��ұ���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ִ�п��ұ���")]
    //    ExecDeptCode = 25,

    //    /// <summary>
    //    /// ִ�п���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ִ�п���")]
    //    ExeDept = 26,

    //    /// <summary>
    //    /// �Ӽ�
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("�Ӽ�")]
    //    Emc = 27,

    //    /// <summary>
    //    /// ��鲿λ
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��鲿λ")]
    //    CheckBody = 28,

    //    /// <summary>
    //    /// ��������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��������")]
    //    Sample = 29,

    //    /// <summary>
    //    /// ȡҩҩ������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ȡҩҩ������")]
    //    DrugDeptCode = 30,

    //    /// <summary>
    //    /// ȡҩҩ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ȡҩҩ��")]
    //    DrugDept = 31,

    //    /// <summary>
    //    /// ��ע
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��ע")]
    //    Memo = 32,

    //    /// <summary>
    //    /// ¼���˱���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("¼���˱���")]
    //    InputOperCode = 33,

    //    /// <summary>
    //    /// ¼����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("¼����")]
    //    InputOper = 34,

    //    /// <summary>
    //    /// ��������
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��������")]
    //    ReciptDept = 35,

    //    /// <summary>
    //    /// ����ʱ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����ʱ��")]
    //    MoDate = 36,

    //    /// <summary>
    //    /// ֹͣʱ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ֹͣʱ��")]
    //    EndDate = 37,

    //    /// <summary>
    //    /// ֹͣ�˱���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ֹͣ�˱���")]
    //    DCOperCode = 38,

    //    /// <summary>
    //    /// ֹͣ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("ֹͣ��")]
    //    DCOper = 39,

    //    /// <summary>
    //    /// ˳���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("˳���")]
    //    OrderNo = 40,

    //    /// <summary>
    //    /// Ƥ�Դ���
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("Ƥ�Դ���")]
    //    HypoTestCode = 41,

    //    /// <summary>
    //    /// Ƥ��
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("Ƥ��")]
    //    HypoTest = 42,


    //    /// <summary>
    //    /// ������Ŀ���루�¼ӣ�
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��Ŀ")]
    //    Item = 43,

    //    /// <summary>
    //    /// ����
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("����")]
    //    SubComb = 44,

    //    /// <summary>
    //    /// ��ҩ��ʽ
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("��ҩ��ʽ")]
    //    SpeUsage=45
    //}
}