using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Pharmacy.In
{
    /*  
     * ������Ӳɹ���Ϣʱ ͬʱ���������Ϣ ����hsInputData�� �洢������ԴΪ�ɹ� �洢�ɹ���ˮ��
     *                                      �ɹ���Ϣ����hsStockData�� ͬʱ��ȡ��ҩƷ��Ϣ
     *                    ����ʱ ���ݲɹ���ˮ�Ż�ȡ�ɹ���Ϣ����ȷ��
     * �����ⲿ������Ϣʱ ͬʱ���������Ϣ ����hsInputData�� �洢������ԴΪ���� ͬʱ��ȡ��ҩƷ��Ϣ 
     *                     ����ʱ ���������ˮ�Ž���������Ϣȷ��
     * ����ֱ�����ӵ�ҩƷ ͬʱ���������Ϣ ����hsInputData�� �洢������ԴΪ�ֹ�ѡ��
     *                    ����ʱ ֱ��ȷ��
     * 
     * 
     *  �޸Ļ����ڵ�frmEasyChoose���� ����FpLabel FpWidth FpVisible ����Fp�������ʾ 
     * 
     * 
     * ��Ҫ��������޸ĵ������ �����ŷ����仯ʱ ��α�֤��hsData�ڵ����ݽ��д���
     * pS ����������������Ҳ����,ֻ�Ƕ�dataset����ô����?
     * �Դ�����Ĵ���һֱ���Ǻܷ��� ������
     * 
     * ҩƷ����12λ
     * 
     *  ������  ��Ʊ�������� ��ƱĬ����һ��
     *
     * ���ӷ�Ʊʱ�䣬�б��ǣ�һ�����ʱ�Ĺ����
     * 
     */



    /// <summary>
    /// [��������: һ���������ʵ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// 
    /// <˵��>
    ///     1���ɹ���⵼���ļ���ʽҪ�� ���� ���� ��� ���� ����� ���� ��Ч��
    /// </˵��>
    /// <�޸ļ�¼>
    ///     <ʱ��>2007-07</ʱ��>
    ///     <�޸���>Liangjz</�޸���>
    ///     <�޸�˵��>1������ҩ��������⹦��</�޸�˵��>
    /// </�޸ļ�¼>
    /// <�޸�˵��>
    ///        1.�޸�һ������ж��Ƿ�¼�뷢Ʊ��û����֤null��Bug by Sunjh 2010-8-25 {003645CF-57A3-4e52-B227-90D33A79B78F}
    ///        2.�޸��������ֶζ������������BUG by Sunjh 2010-9-6 {80C07687-CF0A-450b-942D-8153A9F10BC0}
    ///     </�޸�˵��>
    /// </summary>
    public class CommonInPriv : IPhaInManager
    {
        public CommonInPriv(bool isSpecial, FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.isSpecialIn = isSpecial;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.SetPhaManagerProperty(ucPhaManager);
            }
        }

        #region �����

        private bool isSpecialIn = false;
        //{73CBD808-5BA4-4adc-8F58-AEF257F82FC9}  ��Private ����Ϊ Protected
        protected ucPhaIn phaInManager = null;

        private FarPoint.Win.Spread.SheetView svTemp = null;
        //{73CBD808-5BA4-4adc-8F58-AEF257F82FC9} ��Private ����Ϊ Protected
        protected DataTable dt = null;

        ucCommonInDetail ucDetail = null;

        /// <summary>
        /// һ������Ƿ���Ҫ��׼
        /// </summary>
        private bool IsNeedApprove = true;

        /// <summary>
        /// �������
        /// </summary>
        private System.Collections.Hashtable hsInputData = new System.Collections.Hashtable();

        /// <summary>
        /// �ɹ�����
        /// </summary>
        private System.Collections.Hashtable hsStockData = new System.Collections.Hashtable();

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucPhaListSelect ucListSelect = null;

        /// <summary>
        /// ֻ��Fp��Ԫ������
        /// </summary>
        private FarPoint.Win.Spread.CellType.TextCellType tReadOnly = new FarPoint.Win.Spread.CellType.TextCellType();

        /// <summary>
        /// CheckBox��Ԫ������
        /// </summary>
        private FarPoint.Win.Spread.CellType.CheckBoxCellType chkCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

        /// <summary>
        /// ǰ�޸����ݼ�ֵ
        /// </summary>
        private string privKey = "";

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private DateTime sysTime = System.DateTime.MinValue;

        /// <summary>
        /// �Ƿ��жϵ�ǰѡ��Ĺ�����˾�������Ϣ�ڵĹ�����˾����ͬ
        /// </summary>
        private bool isJudgeDefaultCompany = false;

        /// <summary>
        /// ���ݵ���ؼ�
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucImportData ucImport = null;

        /// <summary>
        /// ����ӡ����
        /// </summary>
        private ArrayList alPrintData = null;

        #endregion

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="ucPhaManager"></param>
        private void SetPhaManagerProperty(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.phaInManager = ucPhaManager;

            if (this.phaInManager != null)
            {
                //���ý�����ʾ
                this.phaInManager.IsShowItemSelectpanel = false;
                this.phaInManager.IsShowInputPanel = true;
                //����Ŀ�������Ϣ  ����ҩ��/ҩ�����в�ͬ����
                if (this.phaInManager.DeptInfo.Memo == "PI")
                {
                    this.phaInManager.SetTargetDept(true, true, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);
                    //{EDD03CD1-6E6D-4e2c-8FCD-D8C73E21A34A}
                    //this.phaInManager.TargetDept = new FS.FrameWork.Models.NeuObject();

                    //���ù�������ť��ʾ
                    this.phaInManager.SetToolBarButton(true, false, false, true, true, true);
                    this.phaInManager.SetToolBarButtonVisible(true, false, false, true, true, true, true);
                }
                else
                {
                    //{EDD03CD1-6E6D-4e2c-8FCD-D8C73E21A34A}
                    this.phaInManager.SetTargetDept(false, true,true, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);

                    //this.phaInManager.TargetDept = new FS.FrameWork.Models.NeuObject();

                    //���ù�������ť��ʾ
                    //this.phaInManager.SetToolBarButton(true, false, false, true, true, true);
                    this.phaInManager.SetToolBarButtonVisible(true, false, false, false, true, true, false);
                }

                //��Ϣ˵������
                this.phaInManager.ShowInfo = "���²���������˫���ɽ����޸�";
                //����Fp����
                this.phaInManager.Fp.EditModePermanent = false;
                this.phaInManager.Fp.EditModeReplace = false;
                
                this.phaInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(phaInManager_EndTargetChanged);
                this.phaInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(phaInManager_EndTargetChanged);

                this.phaInManager.Fp.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(Fp_CellDoubleClick);

                this.svTemp = this.phaInManager.FpSheetView;
            }
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="errStr">��ʾ��Ϣ</param>
        private void ShowMsg(string strMsg)
        {
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(Language.Msg(strMsg));
        }

        /// <summary>
        /// /��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            //��ȡ���Ʋ����ж��Ƿ���Ҫ��׼
            FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();

            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsNeedApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.In_Need_Approve, true, true);

            //�򿪴�������
            this.sysTime = ctrlManager.GetDateTimeFromSysDateTime().Date;
        }

        /// <summary>
        /// ת��
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataSource">������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��</param>
        /// <returns></returns>
        protected FS.HISFC.Models.Pharmacy.Input ConvertToInput(FS.HISFC.Models.Pharmacy.Item item,string dataSource)
        {
            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

            input.Item = item;

            #region ʵ�帳ֵ

            input.SpecialFlag = NConvert.ToInt32(this.isSpecialIn).ToString();  //�Ƿ�������� 0 �� 1 ��
            input.StockDept = this.phaInManager.DeptInfo;                       //������
            input.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
            input.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
            input.Company = this.phaInManager.TargetDept;                       //������λ 
            input.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ

            input.User01 = dataSource;                                          //������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��
          
            #endregion

            return input;
        }

        /// <summary>
        /// ��ʵ����Ϣ����DataTable��
        /// </summary>
        /// <param name="input">�����Ϣ Input.User01�洢������Դ</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Pharmacy.Input input)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            //�в�ҩ�Զ���������Ϊ"1"
            if (input.Item.Type.ID == "C" && (input.BatchNO == "" || input.BatchNO == null))
            {
                input.BatchNO = "1";
            }

            try
            {
                input.RetailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                input.PurchaseCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.PurchasePrice;

                this.dt.Rows.Add(new object[] { 
                                                true,
                                                input.DeliveryNO,                           //�ͻ�����
                                                input.Item.Name,                            //��Ʒ����
                                                input.Item.Specs,                           //���
                                                input.Item.PriceCollection.RetailPrice,     //���ۼ�
                                                input.Item.PackUnit,                        //��װ��λ
                                                input.Item.PackQty,                         //��װ����
                                                input.Quantity / input.Item.PackQty,        //�������
                                                input.RetailCost,                           //�����
                                                input.BatchNO,                              //����
                                                input.ValidTime,                            //��Ч��
                                                input.InvoiceNO,                            //��Ʊ��
                                                input.InvoiceType,                          //��Ʊ���
                                                input.Item.PriceCollection.PurchasePrice,   //�����
                                                input.PurchaseCost,                         //������
                                                input.Item.Product.Producer.Name,           //��������
                                                input.Item.ID,                              //ҩƷ����
                                                input.ID,                                   //��ˮ��
                                                input.User01,                               //������Դ
                                                input.Item.NameCollection.SpellCode,        //ƴ����
                                                input.Item.NameCollection.WBCode,           //�����
                                                input.Item.NameCollection.UserCode          //�Զ�����
                            
                                           }
                                );
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + e.Message));

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + ex.Message));

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        /// <param name="sv"></param>
        protected virtual void SetFormat( )
        {
            if (this.svTemp == null)
                return;

            this.tReadOnly.ReadOnly = true;

            this.svTemp.DefaultStyle.Locked = true;

            this.svTemp.Columns[(int)ColumnSet.ColIsApprove].Width = 38F;
            this.svTemp.Columns[(int)ColumnSet.ColDeliveryNO].Width = 60F;
            this.svTemp.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.svTemp.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.svTemp.Columns[(int)ColumnSet.ColRetailPrice].Width = 65F;
            this.svTemp.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;
            this.svTemp.Columns[(int)ColumnSet.ColPackQty].Width = 60F;
            this.svTemp.Columns[(int)ColumnSet.ColBatchNO].Width = 90F;
            this.svTemp.Columns[(int)ColumnSet.ColInvoiceNO].Width = 80F;

            this.svTemp.Columns[(int)ColumnSet.ColValidTime].Visible = true;        //��Ч��
            this.svTemp.Columns[(int)ColumnSet.ColInvoiceType].Visible = false;      //��Ʊ����
            this.svTemp.Columns[(int)ColumnSet.ColProducerName].Visible = false;     //��������
            this.svTemp.Columns[(int)ColumnSet.ColDrugID].Visible = false;           //ҩƷ����
            this.svTemp.Columns[(int)ColumnSet.ColInBillNO].Visible = false;         //��ˮ��
            this.svTemp.Columns[(int)ColumnSet.ColDataSource].Visible = false;       //������Դ
            this.svTemp.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.svTemp.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.svTemp.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����

            this.svTemp.Columns[(int)ColumnSet.ColIsApprove].Locked = false;
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="listCode">���뵥��</param>
        /// <param name="state">״̬</param>
        /// <returns>�ɹ�����1 </ʧ�ܷ���-1returns>
        protected virtual int AddApplyData(string listCode,string state)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList al = itemManager.QueryApplyIn(this.phaInManager.DeptInfo.ID, listCode, "0");
            if (al == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("δ��ȷ��ȡ�ⲿ���������Ϣ" + itemManager.Err));
                return -1;
            }

            this.Clear();

            FS.FrameWork.Models.NeuObject applyCompany = new FS.FrameWork.Models.NeuObject();

            foreach (FS.HISFC.Models.Pharmacy.Input input in al)
            {
                FS.HISFC.Models.Pharmacy.Item tempItem = itemManager.GetItem(input.Item.ID);

                if (Function.SetPrice(this.phaInManager.DeptInfo.ID, tempItem.ID, ref tempItem) == -1)
                {
                    return -1;
                }

                input.Item = tempItem;                               //ҩƷʵ����Ϣ

                input.User01 = "2";                                 //������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��
                input.Quantity = input.Operation.ApplyQty;

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInputData.Add(input.Item.ID + input.BatchNO, input);
                }

                applyCompany = input.Company;
            }

            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.Company compay = consManager.QueryCompanyByCompanyID(applyCompany.ID);
            applyCompany.Name = compay.Name;
            applyCompany.Memo = "1";

            this.phaInManager.TargetDept = applyCompany;

            this.CompuateSum();

            return 1;
        }

        /// <summary>
        /// ���Ӳɹ�����
        /// </summary>
        /// <param name="listCode">�ɹ��ƻ�����</param>
        /// <param name="state">״̬</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int AddStockData(string listCode,string state)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList alStock = itemManager.QueryStockPlanByCompany(this.phaInManager.DeptInfo.ID, listCode, this.phaInManager.TargetDept.ID);
            if (alStock == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡ�ɹ���ϸ��Ϣ��������" + itemManager.Err));
                return -1;
            }

            this.Clear();

            foreach (FS.HISFC.Models.Pharmacy.StockPlan info in alStock)
            {
                if (info.State == "3")              //��ϸ��״̬Ϊ'3'�� ˵���ѽ��й���⴦�� ���ٴ���ʾ
                    continue;

                FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

                //���ڲ�ҩĬ������Ϊ"1"
                FS.HISFC.Models.Pharmacy.Item tempItem = itemManager.GetItem(info.Item.ID);
                if (tempItem != null && tempItem.Type.ID.ToString() == "C")
                    input.BatchNO = "1";

                input.Item = tempItem;                                              //ҩƷʵ����Ϣ
                //{C03DD304-AE71-4b6a-BC63-F385DB162EB7}
                input.Item.Product.Producer = info.Item.Product.Producer;

                input.SpecialFlag = NConvert.ToInt32(this.isSpecialIn).ToString();  //�Ƿ�������� 0 �� 1 ��
                input.StockDept = this.phaInManager.DeptInfo;                       //������
                input.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
                input.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
                input.Company = this.phaInManager.TargetDept;                       //������λ 
                input.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ

                input.Quantity = info.StockApproveQty - info.InQty;                      //����

                input.DeliveryNO = listCode;                                        //�ͻ����� ����Ϊ�ɹ�����
                input.StockNO = info.ID;

                input.User01 = "1";                                                 //������Դ 1�ɹ��� 2���뵥 0�ֹ�ѡ��

                if (input.ValidTime == System.DateTime.MinValue)
                {
                    input.ValidTime = this.sysTime.AddYears(5);
                }

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInputData.Add(input.Item.ID + input.BatchNO, input);

                    this.hsStockData.Add(info.ID, info);
                }
            }

            this.SetFormat();

            this.CompuateSum();

            return 1;
        }

        /// <summary>
        /// ���ر��ŵ��ݲ��
        /// </summary>
        /// <param name="checkAll">�Ƿ�����м�¼����ͳ�� True ͳ�����м�¼ False ֻͳ��Checkѡ�м�¼</param>
        /// <param name="retailCost">���۽��</param>
        /// <param name="purchaseCost">������</param>
        /// <param name="balanceCost">���</param>
        public virtual void CompuateSum()
        {
            decimal retailCost = 0;
            decimal purchaseCost = 0;
            decimal balanceCost = 0;

            if (this.dt != null)
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    retailCost += NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["���ۼ�"]);
                    purchaseCost += NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);                    
                }

                balanceCost = (retailCost - purchaseCost);

                this.phaInManager.TotCostInfo = string.Format("���۽��:{0} ������:{1} ���:{2}", retailCost.ToString("N"), purchaseCost.ToString("N"), balanceCost.ToString("N"));
            }
        }

        #region IPhaInManager ��Ա

        /// <summary>
        /// ��ϸ��Ϣ¼��ؼ�
        /// </summary>
        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                ucDetail = new ucCommonInDetail();

                ucDetail.Init();

                ucDetail.PrivDept = this.phaInManager.DeptInfo;

                ucDetail.IsManagerPurchasePrice = true;

                ucDetail.InInstanceCompleteEvent -= new ucCommonInDetail.InstanceCompleteHandler(ucDetail_InInstanceCompleteEvent);
                ucDetail.InInstanceCompleteEvent += new ucCommonInDetail.InstanceCompleteHandler(ucDetail_InInstanceCompleteEvent);

                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //�����ռ�ֵ�¼�
                ucDetail.ClearPriKey -= new ucCommonInDetail.InstanceCompleteHandler(ucDetail_ClearPriKey);
                ucDetail.ClearPriKey += new ucCommonInDetail.InstanceCompleteHandler(ucDetail_ClearPriKey);

                return ucDetail;
            }
        }

        /// <summary>
        /// ���ع���DataSet
        /// </summary>
        /// <param name="sv">�����õ�Fp</param>
        /// <returns></returns>
        public virtual System.Data.DataTable InitDataTable()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            this.dt = new DataTable();

            this.dt.Columns.AddRange(
                                    new System.Data.DataColumn[] {
                                                                    new DataColumn("��׼",      dtBol),
                                                                    new DataColumn("�ͻ�����",  dtStr),
                                                                    new DataColumn("��Ʒ����",  dtStr),
                                                                    new DataColumn("���",      dtStr),
                                                                    new DataColumn("���ۼ�",    dtDec),
                                                                    new DataColumn("��װ��λ",  dtStr),
                                                                    new DataColumn("��װ����",  dtDec),
                                                                    new DataColumn("�������",  dtDec),
                                                                    new DataColumn("�����",  dtDec),
                                                                    new DataColumn("����",      dtStr),
                                                                    new DataColumn("��Ч��",    dtDate),
                                                                    new DataColumn("��Ʊ��",    dtStr),
                                                                    new DataColumn("��Ʊ����",  dtStr),
                                                                    new DataColumn("�����",    dtDec),
                                                                    new DataColumn("������",  dtDec),
                                                                    new DataColumn("��������",  dtStr),
                                                                    new DataColumn("ҩƷ����",  dtStr),
                                                                    new DataColumn("��ˮ��",    dtStr),
                                                                    new DataColumn("������Դ",  dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr)
                                                                   }
                                  );

            DataColumn[] keys = new DataColumn[2];

            keys[0] = this.dt.Columns["ҩƷ����"];
            keys[1] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }

        /// <summary>
        /// ����ҩƷ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            return 1;
        }

        /// <summary>
        /// ��ʾ�����б�
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowApplyList()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            string offerID = "";
            if (this.phaInManager.TargetDept == null || this.phaInManager.TargetDept.ID == "")
                offerID = "AAAA";
            else
                offerID = this.phaInManager.TargetDept.ID;

            //�ⲿ�������
            ArrayList al = itemManager.QueryApplyInList(this.phaInManager.DeptInfo.ID, offerID, "0");
            if (al == null)
            {
                this.ShowMsg("��ȡ�����б�ʧ��" + itemManager.Err);
                return -1;
            }

            #region ���ݹ�����λ���й���

            ArrayList alList = new ArrayList();
            if (this.phaInManager.TargetDept.ID != "")
            {
                foreach (FS.FrameWork.Models.NeuObject info in al)
                {
                    if (info.Memo != this.phaInManager.TargetDept.ID)
                        continue;
                    alList.Add(info);
                }
            }
            else
            {
                alList = al;
            }

            #endregion

            #region ����ѡ�񴰿� ���е���ѡ��

            FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            string[] fpLabel = { "���뵥��", "������λ" };
            float[] fpWidth = { 120F, 120F };
            bool[] fpVisible = { true, true, false, false, false, false };

            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alList, ref selectObj) == 1)
            {
                FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

                targeDept.ID = selectObj.Memo;              //������˾����
                targeDept.Name = selectObj.Name;            //������˾����
                targeDept.Memo = "1";                       //Ŀ�굥λ���� �ⲿ������˾

                this.AddApplyData(selectObj.ID, "");
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ��ʾ��ⵥ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowInList()
        {
            return 1;
        }

        /// <summary>
        /// ��ʾ���ⵥ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ShowOutList()
        {
            return 1;
        }

        /// <summary>
        /// ��ʾ�ɹ����б�
        /// </summary>
        /// <returns></returns>
        public int ShowStockList()
        {
            try
            {
                if (this.ucListSelect == null)
                    this.ucListSelect = new ucPhaListSelect();

                this.ucListSelect.Init();
                this.ucListSelect.DeptInfo = this.phaInManager.DeptInfo;
                this.ucListSelect.Class2Priv = "0312";          //�ɹ�
                this.ucListSelect.State = "2";                  //�����״̬
                this.ucListSelect.IsSelectState = false;                

                this.ucListSelect.SelecctListEvent -= new ucIMAListSelecct.SelectListHandler(ucListSelect_StockSelecctListEvent);
                this.ucListSelect.SelecctListEvent += new ucIMAListSelecct.SelectListHandler(ucListSelect_StockSelecctListEvent);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <returns></returns>
        public int ImportData()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("�������ݽ������ǰδ�������� �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 1;
            }

            this.Clear();

            if (this.ucImport == null)
            {
                this.ucImport = new FS.HISFC.Components.Common.Controls.ucImportData();
            }

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucImport);

            if (this.ucImport.Result == DialogResult.OK && this.ucImport.ImportData != null)
            {
                if (this.ucImport.ImportData.Tables[0].Columns.Count != 8)
                {
                    MessageBox.Show(Language.Msg("�����ļ���ʽ����ȷ ���鵼���ļ���Ϣ"));
                    return -1;
                }
                int iCount = 0;
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                foreach (DataRow dr in this.ucImport.ImportData.Tables[0].Rows)
                {
                    string drugCode = dr[0].ToString();                 //����
                    if (drugCode == null || drugCode == "")
                    {
                        continue;
                    }

                    string batchNO = dr[3].ToString();                  //����
                    decimal purchasePrice = NConvert.ToDecimal(dr[4]);  //�����
                    decimal qty = NConvert.ToDecimal(dr[5]);            //����
                    string invoiceNO = dr[6].ToString();
                    DateTime validTime = NConvert.ToDateTime(dr[7]);    //��Ч��

                    FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugCode);

                    FS.HISFC.Models.Pharmacy.Input input = this.ConvertToInput(item,"0");

                    input.BatchNO = batchNO;                                        //����
                    input.Item.PriceCollection.PurchasePrice = purchasePrice;       //�����
                    input.Quantity = qty * item.PackQty;                            //�������
                    input.InvoiceNO = invoiceNO;                                    //��Ʊ��
                    input.ValidTime = validTime;                                    //��Ч��

                    input.RetailCost = qty * item.PriceCollection.RetailPrice;      //���۽��
                    input.PurchaseCost = qty * input.Item.PriceCollection.PurchasePrice;

                    if (this.AddDataToTable(input) == 1)
                    {
                        this.hsInputData.Add(input.Item.ID + input.BatchNO, input);
                        iCount++;
                    }
                }

                this.SetFormat();

                if (this.svTemp != null)
                {
                    this.svTemp.ActiveRowIndex = this.svTemp.Rows.Count - 1;
                }

                MessageBox.Show(Language.Msg("���γɹ�����" + iCount.ToString() + "����¼"));

                this.CompuateSum();
            }           

            return 1;
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns>��д��Ч ����True ���򷵻� False</returns>
        public virtual bool Valid()
        {
            if (this.phaInManager.TargetDept.ID == "")
            {
                MessageBox.Show(Language.Msg("��ѡ�񹩻���˾"));
                return false;
            }

            if (this.dt.Rows.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ȷ��ѡ��������ҩƷ"));
                return false;
            }

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            DateTime sysTime = dataManager.GetDateTimeFromSysDateTime();

            foreach (DataRow dr in this.dt.Rows)
            {
                if (NConvert.ToDecimal(dr["�������"]) <= 0)
                {
                    MessageBox.Show(Language.Msg(dr["��Ʒ����"].ToString() + "  ������������� �����������С�ڵ���0"));
                    return false;
                }
                if (dr["����"].ToString() == "")
                {
                    MessageBox.Show(Language.Msg("����������"));
                    return false;
                }
                if (NConvert.ToDateTime(dr["��Ч��"]) < sysTime)
                {
                    MessageBox.Show(Language.Msg(dr["��Ʒ����"].ToString() + "  ��Ч��Ӧ���ڵ�ǰ����"));
                    return false;
                }
                if (dr["��Ʊ��"].ToString().Length > 10)
                {
                    MessageBox.Show(Language.Msg(dr["��Ʒ����"].ToString() + "  ��Ʊ�Ź������֧��10λ"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sv">��ִ��ɾ����Fp</param>
        /// <param name="delRowIndex">��ɾ����������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public virtual int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex)
        {
            try
            {
                if (sv != null && delRowIndex >= 0)
                {
                    string[] keys = new string[]{
                                                sv.Cells[delRowIndex, (int)ColumnSet.ColDrugID].Text,
                                                sv.Cells[delRowIndex, (int)ColumnSet.ColBatchNO].Text
                                            };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {                        
                        FS.HISFC.Models.Pharmacy.Input input = this.hsInputData[dr["ҩƷ����"].ToString() + dr["����"].ToString()] as FS.HISFC.Models.Pharmacy.Input;
                        if (input.StockNO != null && this.hsStockData.ContainsKey(input.StockNO))
                        {
                            this.hsStockData.Remove(input.StockNO);
                        }

                        this.hsInputData.Remove(dr["ҩƷ����"].ToString() + dr["����"].ToString());

                        this.dt.Rows.Remove(dr);
                        //�ϼƼ���
                        this.CompuateSum();
                    }
                }
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ݱ�ִ��ɾ��������������" + e.Message));
                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ݱ�ִ��ɾ��������������" + ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���������ʾ
        /// </summary>
        /// <returns></returns>
        public virtual int Clear()
        {
            try
            {
                this.dt.Rows.Clear();

                this.dt.AcceptChanges();

                this.ucDetail.Clear(true);

                this.hsInputData.Clear();

                this.hsStockData.Clear();

                this.privKey = "";

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ����ղ�����������" + ex.Message));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Filter(string filterStr)
        {
            if (this.dt == null)
                return;

            //��ù�������
            string queryCode = "%" + filterStr + "%";

            string filter = Function.GetFilterStr(this.dt.DefaultView, queryCode);

            //this.dt.DefaultView.RowFilter = "ƴ���� like '*" + filterStr + "*'";

            this.dt.DefaultView.RowFilter = filter;

            //string filter = string.Format("(ƴ���� LIKE '{0}') OR (����� LIKE '{0}') OR (�Զ����� LIKE '{0}') OR (��Ʒ���� LIKE '{0}')", queryCode);

            //try
            //{
            //    this.dt.DefaultView.RowFilter = filter;
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("���˷����쳣 " + ex.Message));
            //}
            this.SetFormat();
        }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual void SetFocusSelect()
        {
            this.ucDetail.Select();
            this.ucDetail.Focus();
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Save()
        {
            if (!this.Valid())
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б������..���Ժ�");
            System.Windows.Forms.Application.DoEvents();

            #region ������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //itemManager.SetTrans(t.Trans);
            //phaIntegrate.SetTrans(t.Trans);

            #endregion

            #region ��ȡ��������Ϣ

            string strNewGroupNO = itemManager.GetNewGroupNO();
            if (strNewGroupNO == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                System.Windows.Forms.MessageBox.Show(Language.Msg("δ��ȷ��ȡ��������ˮ��" + itemManager.Err));
                return;
            }
            int newGroupNO = NConvert.ToInt32(strNewGroupNO);

            #endregion

            #region ���汾�α����ҩƷ���� ����ͬһ����ҩƷ���������ɲ�ͬ�����κ� �����˿�Ȳ����޷�Ψһ��־һ�����

            System.Collections.Hashtable hsItem = new Hashtable();

            #endregion

            //�����������
            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();
            //��ⵥ�ݺ�
            string inListNO = null;

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            this.alPrintData = new ArrayList();

            foreach (DataRow dr in this.dt.Rows)
            {
                string key = dr["ҩƷ����"].ToString() + dr["����"].ToString();

                input = this.hsInputData[key] as FS.HISFC.Models.Pharmacy.Input;

                #region ������ݸ�ֵ����

                if (inListNO == null)
                {
                    inListNO = input.InListNO;
                }

                input.GroupNO = newGroupNO;                                         //���κ�

                if (hsItem.ContainsKey(input.Item.ID))
                {
                    input.GroupNO = NConvert.ToInt32(itemManager.GetNewGroupNO());
                }
                else
                {
                    hsItem.Add(input.Item.ID, null);
                }

                #region �����������ⵥ�ݺ� ���ȡ����ⵥ�ݺ�

                if (inListNO == "" || inListNO == null)
                {
                    //{59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ������ⵥ�Ż�ȡ��ʽ
                    inListNO = phaIntegrate.GetInOutListNO(this.phaInManager.DeptInfo.ID, true);
                    if (inListNO == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.ShowMsg("��ȡ������ⵥ�ų���" + itemManager.Err);
                        return;
                    }
                }

                #endregion

                input.InListNO = inListNO;                                          //��ⵥ�ݺ�

                #region ������Ϣ��ÿ������������������Ϣʵ��ʱ��ֵ

                input.SpecialFlag = NConvert.ToInt32(this.isSpecialIn).ToString();  //�Ƿ�������� 0 �� 1 ��
                input.StockDept = this.phaInManager.DeptInfo;                       //������
                input.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
                input.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
                input.Company = this.phaInManager.TargetDept;                       //������λ 
                input.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ
                // {D28CC3CF-C502-4987-BC01-1AEBF2F9D17F} sel ���������������Եĸ�ֵ
                input.CommonPurchasePrice=input.PriceCollection.PurchasePrice;       //һ�����ʱ�Ĺ����
                //input.InvoiceDate=;                                               //��Ʊ�ϵķ�Ʊ����
                #endregion

                #region ���������� һ�����϶������� һ����������ο����Ǳ��������

                //input.StoreQty = input.Quantity;               //����������
                //input.StoreCost = Math.Round(input.StoreQty / input.Item.PackQty * input.Item.PriceCollection.RetailPrice, 3);

                //�޸��������ֶζ������������BUG by Sunjh 2010-9-6 {80C07687-CF0A-450b-942D-8153A9F10BC0}
                decimal storageNum = 0;
                if (itemManager.GetStorageNum(input.StockDept.ID, input.Item.ID, out storageNum) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMsg("��ȡ�������ʱ����" + itemManager.Err);
                    return;
                }
                input.StoreQty = storageNum + input.Quantity;               //����������
                input.StoreCost = Math.Round(input.StoreQty / input.Item.PackQty * input.Item.PriceCollection.RetailPrice, 3);


                #endregion

                if (input.Operation.ApplyOper.ID == "")
                {
                    input.Operation.ApplyQty = input.Quantity;                          //���������
                    input.Operation.ApplyOper.ID = this.phaInManager.OperInfo.ID;
                    input.Operation.ApplyOper.OperTime = sysTime;
                }

                input.Operation.Oper.ID = this.phaInManager.OperInfo.ID;
                input.Operation.Oper.OperTime = sysTime;

                #region ���ݲ�ͬ������� ���������Ϣ״̬

                input.State = "0";
                //�޸�һ������ж��Ƿ�¼�뷢Ʊ��û����֤null��Bug by Sunjh 2010-8-25 {003645CF-57A3-4e52-B227-90D33A79B78F}
                if (input.InvoiceNO != null && input.InvoiceNO != "")                //�����뷢Ʊ�� ֱ������״̬Ϊ��Ʊ���
                {
                    input.Operation.ExamQty = input.Quantity;
                    input.Operation.ExamOper.OperTime = input.Operation.Oper.OperTime;
                    input.Operation.ExamOper.ID = input.Operation.Oper.ID;
                    input.State = "1";
                }

                //���Ʋ����趨һ����ⲻ��Ҫ��׼ �� �������Ϊ�������
                if (!this.IsNeedApprove || this.isSpecialIn)
                {
                    input.State = "2";
                    input.Operation.ExamQty = input.Quantity;
                    input.Operation.ExamOper.OperTime = input.Operation.Oper.OperTime;
                    input.Operation.ExamOper.ID = input.Operation.Oper.ID;
                    input.Operation.ApplyOper.OperTime = input.Operation.Oper.OperTime;
                    input.Operation.ApproveOper.ID = input.Operation.Oper.ID;
                    //{BC502D46-48CE-4ced-A6AA-20E1B0132D40}  �Ժ�׼ʱ����и�ֵ
                    input.Operation.ApproveOper = input.Operation.ExamOper;

                    //{476ED544-49A6-4070-9ACB-C581F403347D} ���ֵ��¼���������Ϣ����
                    if (itemManager.UpdateBaseItemWithInputInfo(input) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.ShowMsg("��� ����ʧ��" + itemManager.Err);
                        return;
                    }
                }

                #endregion

                //������λ���� 1 Ժ�ڿ��� 2 ������˾ 3 ��չ
                //{24E12384-34F7-40c1-8E2A-3967CECAF615} ���ݸ�ֵ
                if (this.phaInManager.DeptInfo.Memo == "PI")                //��ǰ��¼����Ϊҩ��
                {
                    input.SourceCompanyType = "2";
                }
                else
                {
                    input.SourceCompanyType = "1";
                }

                if (itemManager.Input(input.Clone(), "1", input.State == "2" ? "1" : "0") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMsg("��� ����ʧ��" + itemManager.Err);
                    return;
                }

                #endregion

                #region ���»�λ�� {EE43F167-1551-429b-A886-66FA60457D60}
                FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
                if (itemManager.SetPlaceNoOptimize(input.PlaceNO, ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.ID, input.Item.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMsg("���������Ϣ���»�λ�ŷ�������" + itemManager.Err);
                    return;
                }
                #endregion

                #region ���ݲ�ͬ������Դ��ԭʼ���ݽ��и���

                switch (dr["������Դ"].ToString())
                {
                    case "0":           //�ֹ�ѡ��
                        break;
                    case "2":           //����

                        if (itemManager.ApproveApplyIn(input) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.ShowMsg("�����׼ʧ��" + itemManager.Err);
                            return;
                        }

                        break;
                    case "1":           //�ɹ�

                        #region ���²ɹ���¼

                        FS.HISFC.Models.Pharmacy.StockPlan stockPlan;
                        if (this.hsStockData.ContainsKey(input.StockNO))
                            stockPlan = this.hsStockData[input.StockNO] as FS.HISFC.Models.Pharmacy.StockPlan;
                        else
                            continue;

                        if (NConvert.ToBoolean(dr["��׼"]))
                        {
                            stockPlan.State = "3";
                        }
                        else
                        {
                            //��������С�ں�׼���� ˵��δ��׼���  ������Ϊȫ����׼
                            if ((stockPlan.StockApproveQty - stockPlan.InQty) > input.Quantity)
                                stockPlan.State = "2";
                            else
                                stockPlan.State = "3";
                        }
                        //�˴����Բɹ���Ϣ���������ҽ��д���
                        stockPlan.InQty += input.Quantity;                          //ʵ�������
                        stockPlan.InOper.ID = input.Operation.Oper.ID;              //�����
                        stockPlan.InOper.OperTime = input.Operation.Oper.OperTime;  //�������
                        stockPlan.InListNO = inListNO;                              //��ⵥ�ݺ�

                        if (itemManager.UpdateStockPlanForIn(stockPlan.ID,stockPlan.InQty,stockPlan.InListNO,stockPlan.InOper,stockPlan.State) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.ShowMsg("���������Ϣ���²ɹ���Ϣ��������" + itemManager.Err);
                            return;
                        }

                        #endregion
                        break;
                }

                #endregion

                this.alPrintData.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.ShowMsg("��Ᵽ��ɹ�");

            DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ��ӡ��ⵥ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                this.Print();
            }

            this.Clear();

            string strErr = "";
            FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("PHA", "InvoiceType", out strErr, input.InvoiceType);

        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            if (this.phaInManager.IInPrint != null)
            {
                this.phaInManager.IInPrint.SetData(this.alPrintData, this.phaInManager.PrivType.Memo);
                this.phaInManager.IInPrint.Print();
            }

            return 1;
        }

        #endregion

        #region IPhaInManager ��Ա

        public int Dispose()
        {
            //{9E282C1A-071F-4833-8AE3-EC64CA71FD8F} ���Ӷ���Դ�ͷź����ĵ���
            this.phaInManager.Fp.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(Fp_CellDoubleClick);
            return 1;
        }

        #endregion

        private void ucDetail_InInstanceCompleteEvent(ref FS.FrameWork.Models.NeuObject msg)
        {
            FS.HISFC.Models.Pharmacy.Input tempInput = this.ucDetail.InInstance.Clone();

            if (tempInput != null)
            {
                if (tempInput.Item.ID == "")
                {
                    return;
                }

                #region �ж��Ƿ���ڹ�����˾

                if (this.phaInManager.TargetDept.ID == "")
                {
                    MessageBox.Show(Language.Msg("��ѡ�񹩻���λ"));

                    //֪ͨucDetail�� ��������
                    if (msg == null)
                    {
                        msg = new FS.FrameWork.Models.NeuObject();
                    }
                    msg.User01 = "-1";      //��־�Ƿ�����

                    this.phaInManager.SetDeptFocus();
                    
                    return;
                }

                #endregion

                #region �Ƿ��жϴ�ʱѡ��Ĺ�����˾��ҩƷ������Ϣά���Ĺ�����˾

                if (this.isJudgeDefaultCompany)
                {
                    if (tempInput.Item.Product.Company.ID != "" && this.phaInManager.TargetDept.ID != tempInput.Item.Product.Company.ID)
                    {
                        DialogResult rs = MessageBox.Show(Language.Msg("��ǰѡ��Ĺ�����λ��ҩƷά����Ĭ�Ϲ�����λ��ͬ �Ƿ����?"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
                        if (rs == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                #endregion

                string key = tempInput.Item.ID + tempInput.BatchNO;

                #region �жϸ�ҩƷ��Ϣ�Ƿ���� ���������ɾ��ԭ��Ϣ ���¸�ֵ

                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //������
                //if (this.privKey != "" && this.privKey.Substring(0, 12) != key.Substring(0, 12))
                //{
                //    this.privKey = "";
                //}
                ////������ ɾ��ԭ��Ϣ ������� �����ظ��������
                //if (this.privKey.Length == 12)          
                //{
                //    if (this.hsInputData.ContainsKey(this.privKey))
                //    {
                //        this.hsInputData.Remove(this.privKey);
                //        string[] keys = new string[] { this.privKey.Substring(0,12), "" };
                //        DataRow drFind = this.dt.Rows.Find(keys);
                //        if (drFind != null)
                //        {
                //            this.dt.Rows.Remove(drFind);
                //        }
                //    }
                //}
                //������ ɾ��ԭ��Ϣ ������� �����ظ��������
                if (this.privKey != "" && this.privKey != key)
                {
                    if (this.hsInputData.ContainsKey(this.privKey))
                    {
                        this.hsInputData.Remove(this.privKey);
                        string[] keys = new string[] { tempInput.Item.ID, this.privKey.Substring(12, this.privKey.Length - 12) };
                        DataRow drFind = this.dt.Rows.Find(keys);
                        if (drFind != null)
                        {
                            this.dt.Rows.Remove(drFind);
                        }
                    }
                }
                this.privKey = key;

                //����ͬҩƷ/���� ɾ��ԭ���� 
                if (this.hsInputData.ContainsKey(key))
                {
                    this.hsInputData.Remove(key);
                    string[] keys = new string[]{tempInput.Item.ID,tempInput.BatchNO};
                    DataRow drFind = this.dt.Rows.Find(keys);
                    if (drFind != null)
                    {
                        this.dt.Rows.Remove(drFind);
                    }
                }

                #endregion

                #region ʵ�帳ֵ

                tempInput.SpecialFlag = NConvert.ToInt32(this.isSpecialIn).ToString();  //�Ƿ�������� 0 �� 1 ��
                tempInput.StockDept = this.phaInManager.DeptInfo;                       //������
                tempInput.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
                tempInput.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
                tempInput.Company = this.phaInManager.TargetDept;                       //������λ 
                tempInput.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ

                if (msg.User02 == "1")
                {
                    tempInput.User01 = "0";                                             //������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��
                }
                #endregion

                if (this.AddDataToTable(tempInput) == 1)
                {
                    this.hsInputData.Add(key, tempInput);

                    this.SetFormat();

                    if (this.svTemp != null)
                    {
                        this.svTemp.ActiveRowIndex = this.svTemp.Rows.Count - 1;
                    }
                }

                this.CompuateSum();
            }
        }

        private void ucListSelect_StockSelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            //������λ
            this.phaInManager.TargetDept = targetDept;
            //���Ӳɹ�����
            this.AddStockData(listCode, state);
        }

        private void phaInManager_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            return;
        }

        private void Fp_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] keys = new string[]{
                                                this.svTemp.Cells[e.Row, (int)ColumnSet.ColDrugID].Text,
                                                this.svTemp.Cells[e.Row, (int)ColumnSet.ColBatchNO].Text
                                            };
            DataRow dr = this.dt.Rows.Find(keys);
            if (dr != null)
            {
                this.privKey = dr["ҩƷ����"].ToString() + dr["����"].ToString();

                FS.HISFC.Models.Pharmacy.Input input = this.hsInputData[dr["ҩƷ����"].ToString() + dr["����"].ToString()] as FS.HISFC.Models.Pharmacy.Input;

                this.ucDetail.InInstance = input.Clone();
            }
        }

        //�������
        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        public void ucDetail_ClearPriKey(ref FS.FrameWork.Models.NeuObject sender)
        {
            this.privKey = "";
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��׼
            /// </summary>
            ColIsApprove,
            /// <summary>
            /// �ͻ�����
            /// </summary>
            ColDeliveryNO,
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ���ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ��װ��λ
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// �������
            /// </summary>
            ColInQty,
            /// <summary>
            /// �����
            /// </summary>
            ColInCost,
            /// <summary>
            /// ����
            /// </summary>
            ColBatchNO,
            /// <summary>
            /// ��Ч��
            /// </summary>
            ColValidTime,
            /// <summary>
            /// ��Ʊ��
            /// </summary>
            ColInvoiceNO,
            /// <summary>
            /// ��Ʊ����
            /// </summary>
            ColInvoiceType,
            /// <summary>
            /// �����
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// ������
            /// </summary>
            ColPurchaseCost,
            /// <summary>
            /// ��������
            /// </summary>
            ColProducerName,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugID,
            /// <summary>
            /// ��ˮ��
            /// </summary>
            ColInBillNO,
            /// <summary>
            /// ������Դ
            /// </summary>
            ColDataSource,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode
        }
    }
}
