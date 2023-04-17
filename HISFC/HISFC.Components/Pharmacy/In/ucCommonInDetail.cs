using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: һ�������Ϣ�Ǽ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// �����:
    ///     ����ʾ��Ŀ�б�ʱ �Ҳ�ά���ؼ���ʾ�Ż�
    /// <�޸ļ�¼>
    ///    1.һ��������������׼�ĺ���ʾ������֧�ִ�ҩƷ������Ϣ�����һ������¼���Զ���ȡ����䣬�޸�����ͬ������ҩƷ������Ϣ by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
    ///    2.һ������������ҩƷ��������Ƿ��бꡢ��Դ(����)��ʾ by Sunjh 2010-11-1 {3EA56CF7-B007-4d96-A2F4-17D2C3A1A38E}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucCommonInDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCommonInDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �¼�����
        /// </summary>
        /// <param name="sender">sender˵�� User01 ��־�Ƿ����� -1  �������� User02 ��־�Ƿ�Ϊ�ֹ�ѡ��ҩƷ 1 �ֹ�ѡ�� 0 ���ֹ�ѡ��</param>
        public delegate void InstanceCompleteHandler(ref FS.FrameWork.Models.NeuObject sender);

        public event InstanceCompleteHandler InInstanceCompleteEvent;

        public event InstanceCompleteHandler ClearPriKey;

        #region �� �� ��

        /// <summary>
        /// �Ƿ����Ź���
        /// </summary>
        private bool isManagerBatchNO = true;

        /// <summary>
        /// �Ƿ������Ʊ����
        /// </summary>
        private bool isManagerInvoiceType = false;

        /// <summary>
        /// �Ƿ������������/��������
        /// </summary>
        private bool isManagerFac = false;

        /// <summary>
        /// �Ƿ�����չ��Ϣ
        /// </summary>
        private bool isManagerExtend = false;

        /// <summary>
        /// һ�����ʱ �Ƿ�Ĭ���ϴη�Ʊ��Ϣ
        /// </summary>
        private bool isDefaultPrivInvoiceNO = true;

        /// <summary>
        /// ��ǰ���������ҩƷʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = null;

        /// <summary>
        /// ��ǰ���������ʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Input inInstance = null;

        /// <summary>
        /// �Ƿ��ѽ����˳�ʼ��
        /// </summary>
        private bool isInit = false;

        /// <summary>
        /// ����״̬���б���
        /// </summary>
        private int privItemListWidth = 250;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = null;

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region ��    ��

        /// <summary>
        /// �Ƿ����Ź���
        /// </summary>
        public bool IsManagerBatchNO
        {
            get
            {
                return this.isManagerBatchNO;
            }
            set
            {
                this.isManagerBatchNO = value;
            }
        }

        /// <summary>
        /// �Ƿ����Ʊ����
        /// </summary>
        public bool IsManagerInvoiceType
        {
            get
            {
                return this.isManagerInvoiceType;
            }
            set
            {
                this.isManagerInvoiceType = value;
            }
        }

        /// <summary>
        /// �Ƿ������������/��������
        /// </summary>
        public bool IsManagerFac
        {
            get
            {
                return this.isManagerFac;
            }
            set
            {
                this.isManagerFac = value;
            }
        }

        /// <summary>
        /// �Ƿ�����չ��Ϣ
        /// </summary>
        public bool IsManagerExtend
        {
            get
            {
                return this.isManagerExtend;
            }
            set
            {
                this.isManagerExtend = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��Ŀѡ���б�
        /// </summary>
        public bool IsShowItemSelect
        {
            get
            {
                return !this.splitContainer1.Panel1Collapsed;
            }
            set
            {
                this.splitContainer1.Panel1Collapsed = !value;
            }
        }

        /// <summary>
        /// �Ƿ��������Ϣ
        /// </summary>
        public bool IsManagerPurchasePrice
        {
            get
            {
                return this.ntbPurchasePrice.Enabled;
            }
            set
            {
                this.ntbPurchasePrice.Enabled = value;
            }
        }

        /// <summary>
        /// һ�����ʱ �Ƿ�Ĭ���ϴη�Ʊ��Ϣ
        /// </summary>
        public bool IsDefaultPrivInvoiceNO
        {
            get
            {
                return this.isDefaultPrivInvoiceNO;
            }
            set
            {
                this.isDefaultPrivInvoiceNO = value;
            }
        }

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        public FS.FrameWork.Models.NeuObject PrivDept
        {
            get
            {
                return this.privDept;
            }
            set
            {
                this.privDept = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ǰ���������ҩƷʵ��
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            set
            {
                this.Clear(true);

                this.item = value;

                if (value != null)
                {
                    this.SetItem(value);
                }
            }
        }

        /// <summary>
        /// ��ǰ���������ʵ��
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Input InInstance
        {
            get
            {
                if (this.inInstance == null)
                {
                    this.inInstance = new FS.HISFC.Models.Pharmacy.Input();
                }

                this.GetInInstance();

                return this.inInstance;
            }
            set
            {
                this.Clear(true);

                this.inInstance = value;

                if (value != null)
                {
                    this.SetInInstance(value);

                    this.item = value.Item;
                }
            }
        }

        #region �� ʼ ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public  virtual void Init()
        {
            if (!isInit)
            {
                this.ucDrugList1.ShowPharmacyList();    //����ҩƷ�б�

                FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                System.Collections.ArrayList alInvoiceType = consManager.GetList("InvoiceType");

                this.cmbInvoiceType.AddItems(alInvoiceType);

                FS.HISFC.BizLogic.Pharmacy.Constant phaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();
                System.Collections.ArrayList alProduce = phaCons.QueryCompany("0");
                this.cmbProduce.AddItems(alProduce);

                isInit = true;                          //�ѽ����˳�ʼ��
            }
        }

        #endregion

        #region ��    ��

        /// <summary>
        /// ����ҩƷ��Ϣ���ý�����ʾ
        /// </summary>
        /// <param name="item">ҩƷ��Ϣ</param>
        private void SetItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            //һ������������ҩƷ��������Ƿ��бꡢ��Դ(����)��ʾ by Sunjh 2010-11-1 {3EA56CF7-B007-4d96-A2F4-17D2C3A1A38E}
            decimal storeQty = 0;
            string strStoreQty = "0";
            if (itemManager.GetStorageNum(this.privDept.ID, item.ID, out storeQty) == -1)
            {
                strStoreQty = "��ѯʧ��";
            }
            else
            {
                strStoreQty = storeQty.ToString() + item.MinUnit;
            }
            this.lbDrugName.Text = string.Format("��Ʒ����:{0}  ���:{1}  ��Դ:{2}  �����:{3}", item.Name, item.Specs, item.Product.ProducingArea, strStoreQty);

            this.lbDrugPackInfo.Text = string.Format("���ۼ�:{0}  ��װ����:{1}  ��װ��λ:{2}  ��С��λ:{3}  {4}",
                item.PriceCollection.RetailPrice.ToString(), item.PackQty.ToString(), item.PackUnit, item.MinUnit, item.TenderOffer.IsTenderOffer == true ? "���б꡿" : "");

            this.lbUnit.Text = item.PackUnit;

            //�� �� ��
            this.ntbPurchasePrice.Text = item.PriceCollection.PurchasePrice.ToString();

            this.cmbProduce.Tag = item.Product.Producer.ID;

            #region ��λ�� {EE43F167-1551-429b-A886-66FA60457D60}
            
            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            this.txtPlaceNO.Text = itemManager.GetPlaceNoOptimize(((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.ID, item.ID);

            #endregion 

            //��ȡ��׼�ĺ� by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
            this.txtApproveCode.Text = item.Product.ApprovalInfo;
            //��ȡ�ϴ������Ϣ by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
            bool isFillLastInputInfo = this.ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.CommonInput_Auto_FillInfo, true, false);
            if (isFillLastInputInfo)
            {
                FS.HISFC.Models.Pharmacy.Input tempInput = itemManager.GetLastInBillRecord(item.ID);
                this.txtBatchNO.Text = tempInput.BatchNO;
                this.dtValidTime.Value = tempInput.ValidTime;
                this.cmbProduce.Tag = tempInput.Producer.ID;
            }            
        }

        /// <summary>
        /// ���������Ϣ���ý�����ʾ
        /// </summary>
        /// <param name="input">�����Ϣ</param>
        private void SetInInstance(FS.HISFC.Models.Pharmacy.Input inInstance)
        {
            this.SetItem(inInstance.Item);

            //�������
            this.ntbInQty.Text = Math.Round(inInstance.Quantity / inInstance.Item.PackQty,2).ToString("N");
            //�����
            this.ntbInCost.Text = inInstance.RetailCost.ToString();
            //���۽��
            this.ntbPurchaseCost.Text = inInstance.PurchaseCost.ToString();
            //��    ��
            this.txtBatchNO.Text = inInstance.BatchNO;
            //�� Ч ��
            this.dtValidTime.Value = inInstance.ValidTime;
            if (inInstance.InvoiceNO != "" && inInstance.InvoiceNO != null)
            {
                //�� Ʊ ��
                this.txtInvoiceNO.Text = inInstance.InvoiceNO;
            }
            //��Ʊ����
            this.cmbInvoiceType.Text = inInstance.InvoiceType;
            //�� �� ��
            this.ntbPurchasePrice.Text = inInstance.Item.PriceCollection.PurchasePrice.ToString();
            //��������
            this.cmbProduce.Text = inInstance.Item.Product.Producer.Name;
            this.cmbProduce.Tag = inInstance.Item.Product.Producer.ID;
            //�ͻ�����
            this.txtDeliveryNO.Text = inInstance.DeliveryNO;
            ////��λ��
            this.txtPlaceNO.Text = inInstance.PlaceNO;           
            //��ע
            this.txtMemo.Text = inInstance.Memo;

            //{48151096-C1E2-4a47-A371-854C5E587378} ��Ʊ���ڸ�ֵ
            this.dtpInvoiceDate.Value = inInstance.InvoiceDate;
            //��ȡ��׼�ĺ� by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
            this.txtApproveCode.Text = inInstance.Item.Product.ApprovalInfo;
        }

        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        private void GetInInstance()
        {
            if (this.inInstance == null)
                this.inInstance = new FS.HISFC.Models.Pharmacy.Input();

            if (this.item == null || this.item.ID == "")
                return;

            this.inInstance.Item = this.item;                               //ҩƷ��Ϣ
            this.inInstance.Quantity = NConvert.ToDecimal(this.ntbInQty.NumericValue) * this.item.PackQty;          //�������
            this.inInstance.RetailCost = NConvert.ToDecimal(this.ntbInCost.NumericValue);       //�����
            this.inInstance.BatchNO =  FS.FrameWork.Public.String.TakeOffSpecialChar( this.txtBatchNO.Text.Trim());                  //��    ��
            this.inInstance.ValidTime = this.dtValidTime.Value.Date;                     //�� Ч ��
            this.inInstance.InvoiceNO = FS.FrameWork.Public.String.TakeOffSpecialChar( this.txtInvoiceNO.Text.Trim());              //�� Ʊ ��
            this.inInstance.InvoiceType = this.cmbInvoiceType.Text.Trim();          //��Ʊ����
            this.inInstance.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.ntbPurchasePrice.NumericValue);     //�����
            if (this.cmbProduce.Tag != null)                                //��������
            {
                //{3F6FF86C-0C62-44de-B09B-595B297DD832}
                this.inInstance.Producer.ID = this.cmbProduce.Tag.ToString();
                this.inInstance.Producer.Name = this.cmbProduce.Text.Trim();

                //{C03DD304-AE71-4b6a-BC63-F385DB162EB7}
                this.inInstance.Item.Product.Producer.ID = this.cmbProduce.Tag.ToString();
                this.inInstance.Item.Product.Producer.Name = this.cmbProduce.Text.Trim();
            }
            this.inInstance.DeliveryNO = this.txtDeliveryNO.Text.Trim();           //�ͻ�����
            this.inInstance.PlaceNO = FS.FrameWork.Public.String.TakeOffSpecialChar( this.txtPlaceNO.Text.Trim());                 //��λ��
            this.inInstance.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar( this.txtMemo.Text);                       //��ע
            
            //{48151096-C1E2-4a47-A371-854C5E587378} ��Ʊ���ڸ�ֵ
            this.inInstance.InvoiceDate = this.dtpInvoiceDate.Value;
            //��ȡ��׼�ĺ� by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
            this.inInstance.Item.Product.ApprovalInfo = this.txtApproveCode.Text;
        }

        /// <summary>
        /// ������Ч���ж�
        /// </summary>
        /// <returns>�޴��󷵻�True ���򷵻�False</returns>
        private bool Valid()
        {
            //if (this.txtBatchNO.Text == "")
            //{
            //    MessageBox.Show(Language.Msg("����������"));
            //    return false;
            //}
            if (NConvert.ToDecimal(this.ntbInQty.Text) <= 0)
            {
                MessageBox.Show(Language.Msg("����ȷ����������� �������Ӧ���ڵ�����"));
                this.errorProvider1.SetError(this.ntbInQty, "����������������Ҵ���0");
                this.ntbInQty.Focus();
                return false;
            }
            else
            {
                this.errorProvider1.Clear();
            }
            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void ComputeCost()
        {
            if (this.item == null)
            {
                MessageBox.Show("��ѡ������ҩƷ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal qty = NConvert.ToDecimal(this.ntbInQty.Text);
            this.ntbInCost.Text = (qty * this.item.PriceCollection.RetailPrice).ToString();

            decimal purchasePrice = NConvert.ToDecimal(this.ntbPurchasePrice.Text);
            if (purchasePrice != 0)
                this.ntbPurchaseCost.NumericValue = qty * purchasePrice;
            else
                this.ntbPurchaseCost.Text = (qty * this.item.PriceCollection.PurchasePrice).ToString();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="clearDrugInfo">�Ƿ������ʾ��ҩƷ������Ϣ</param>
        public void Clear(bool clearDrugInfo)
        {
            if (clearDrugInfo)
            {
                this.lbDrugName.Text = "��Ʒ����    ��� "; 
                this.lbDrugPackInfo.Text = "�� �� ��   ��װ����   ��װ��λ   ��С��λ ";
                this.lbUnit.Text = "��λ";
                //�� �� ��
                this.ntbPurchasePrice.NumericValue = 0;
            }
            //�������
            this.ntbInQty.NumericValue = 0;
            //�����
            this.ntbInCost.NumericValue = 0;
            //��    ��
            this.txtBatchNO.Text = "";
            if (!this.isDefaultPrivInvoiceNO)
            {        //�� Ʊ ��
                this.txtInvoiceNO.Text = "";
            }
            //��Ʊ����
            this.cmbInvoiceType.Text = "";
            //�� �� ��
            this.ntbPurchasePrice.NumericValue = 0;
            //��������
            this.cmbProduce.Text = "";
            this.cmbProduce.Tag = null;
            //�ͻ�����
            this.txtDeliveryNO.Text = "";
            //��λ��
            this.txtPlaceNO.Text = "";
            //��ע
            this.txtMemo.Text = "";
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        public new void Focus()
        {
            this.ucDrugList1.SetFocusSelect();
        }      

        #endregion

        private void ntbInQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.Valid())
                {
                    this.ComputeCost();
                    if (this.isManagerBatchNO)
                        this.txtBatchNO.Focus();
                    else
                        this.dtValidTime.Focus();
                }
            }
        }

        private void txtBatchNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtValidTime.Focus();
            }
        }

        private void dtValidTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInvoiceNO.Focus();
            }
        }

        private void txtInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.isManagerInvoiceType)
                    this.cmbInvoiceType.Focus();
                else
                    this.ntbPurchasePrice.Focus();

                if (!this.IsManagerPurchasePrice)
                {
                    KeyEventArgs eKey = new KeyEventArgs(Keys.Enter);
                    this.ntbPurchasePrice_KeyDown(this.ntbPurchasePrice, eKey);
                }
            }
        }

        private void comInvoiceType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ntbPurchasePrice.Focus();
            }
        }

        private void ntbPurchasePrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (NConvert.ToDecimal(this.ntbPurchasePrice.Text) <= 0)
                {
                    MessageBox.Show(Language.Msg("����ȷ���빺��� �����Ӧ���ڵ�����"));
                    this.errorProvider1.SetError(this.ntbPurchasePrice, "����۱��������Ҵ���0");
                    this.ntbPurchasePrice.Focus();
                    return;
                }
                else
                {
                    this.errorProvider1.Clear();
                }

                this.ComputeCost();
                if (this.isManagerFac)
                    this.cmbProduce.Focus();
                else if (this.isManagerExtend)
                    this.txtDeliveryNO.Focus();
                else
                    this.btnOK.Focus();
            }
        }

        private void comboProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.isManagerExtend)
                    this.txtDeliveryNO.Focus();
                else
                    this.btnOK.Focus();
            }
        }

        private void txtDeliveryNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject msg = new FS.FrameWork.Models.NeuObject();

            if (this.item == null)
            {
                return;
            }

            if (this.InInstance.BatchNO == "")
            {
                MessageBox.Show(Language.Msg("����������"));
                this.txtBatchNO.Focus();
                return;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtBatchNO.Text.Trim(), 16))
            {
                MessageBox.Show(Language.Msg("���Ź���:���ܶ���16λ"));
                this.txtBatchNO.Focus();
                return;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtPlaceNO.Text.Trim(), 16))
            {
                MessageBox.Show(Language.Msg("��λ�Ź���:���ܶ���16λ"));
                this.txtPlaceNO.Focus();
                return;
            }
            ///
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtInvoiceNO.Text.Trim(), 10))
            {
                MessageBox.Show(Language.Msg("��Ʊ����:���ܶ���10λ"));
                this.txtInvoiceNO.Focus();
                return;
            }

            #region ҩƷ������ж� {B8E97393-400C-4ec9-ADE5-1F1C541A3E68}
            decimal addrate = 1.15m;
            decimal purchasePrice = addrate * this.inInstance.Item.PriceCollection.PurchasePrice;
            if (purchasePrice != this.inInstance.Item.PriceCollection.RetailPrice)
            {
                DialogResult dr = MessageBox.Show(Language.Msg("����۳��ԼӼ�ϵ�����������ۼۣ��Ƿ������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    this.ntbPurchasePrice.Focus();
                    return;
                }

            }
            #endregion

            if (this.InInstanceCompleteEvent != null)
            {
                if (this.item.User01 == "1")    //�ֹ�ѡ��ҩƷ
                {
                    this.item.User01 = "";
                    msg.User02 = "1";
                }
                else                           //���ֹ�ѡ��ҩƷ 
                {
                    msg.User02 = "0";
                }              

                this.InInstanceCompleteEvent(ref msg);
            }
            //���ݸ�ֵ�Ƿ����仯 �������Ƿ����ý���
            if (msg.User01 != "-1")
            {
                this.Focus();
            }
        }

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (sv != null && activeRow >= 0)
            {
                string drugID;
                drugID = sv.Cells[activeRow, 0].Value.ToString();
                //ȡҩƷ�ֵ���Ϣ
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                this.item = itemManager.GetItem(drugID);
                if (this.item != null)
                {
                    //{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}  Э������У�� ����δά����ϸ��Э�����������ܽ���������
                    if (this.item.IsNostrum == true)
                    {
                        List<FS.HISFC.Models.Pharmacy.Nostrum> nostrumList = itemManager.QueryNostrumDetail( this.item.ID );
                        if (nostrumList == null || nostrumList.Count <= 0)
                        {
                            MessageBox.Show( this.item.Name + " ΪЭ������������δ������ϸ����ά�������ܽ���������" );
                            return;
                        }
                    }
                    this.Clear(true);
                    //��־�Ƿ����¼ӵ�ҩƷ 
                    this.item.User01 = "1";

                    if (Function.SetPrice(this.privDept.ID, this.item.ID, ref this.item) == -1)
                    {
                        return;
                    }

                    //�������
                    this.SetItem(this.item);

                    this.ntbInQty.Focus();

                    //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                    //˫����ռ�ֵ
                    if (this.ClearPriKey != null)
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        this.ClearPriKey(ref obj);
                    }
                }
                else
                {
                    MessageBox.Show(Language.Msg("����ҩƷ������Ϣʧ��"));
                    this.ucDrugList1.SetFocusSelect();
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                this.btnOK_Click(null, System.EventArgs.Empty);
            }
            if (keyData == Keys.F6)
            {
                this.ucDrugList1.Select();
                this.ucDrugList1.SetFocusSelect();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnShowItemSelectPanel_Click(object sender, EventArgs e)
        {
            this.ucDrugList1.Visible = true;

            this.btnShowItemSelectPanel.Visible = false;

            this.splitContainer1.SplitterDistance = this.privItemListWidth;                     
        }

        private void ucDrugList1_CloseClickEvent(object sender, System.EventArgs e)
        {
            this.ucDrugList1.Visible = false;

            this.btnShowItemSelectPanel.Visible = true;

            this.privItemListWidth = this.splitContainer1.SplitterDistance;

            this.splitContainer1.SplitterDistance = this.btnShowItemSelectPanel.Width + 1;
        }

    }
}
