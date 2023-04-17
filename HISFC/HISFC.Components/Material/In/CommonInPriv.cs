using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using FS.FrameWork.Function;
using System.Collections;
using FS.HISFC.Components.Common.Controls;
using System.ComponentModel;

namespace FS.HISFC.Components.Material.In
{
    /// <summary>
    /// [��������: һ�����ҵ����]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     1�� ���üƼ۵�λ��⻹�Ǵ��װ��λ��� ��ͨ��IsUsePackIn���Խ��п��� Ĭ��ֵΪFalse
    /// 
    /// </˵��>
    /// <�����>
    ///     1������ۡ����ۼ��������� GetPrice ˰ǰ��˰����������ȷ
    ///         ��װ�����δ��ȷ��ֵ
    /// </�����>
    /// </summary>
    public class CommonInPriv : IMatManager
    {
        public CommonInPriv(bool isSpecial, In.ucMatIn ucMatInManager)
        {
            this.isSpecial = isSpecial;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.SetMatManagerProperty(ucMatInManager);
            }
        }


        #region �����

        /// <summary>
        /// ���ʿ�������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// ������Ŀ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// �ɹ��ƻ�������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Plan inputPlanManager = new FS.HISFC.BizLogic.Material.Plan();

        /// <summary>
        /// ������˾����������ҵ���� {5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany companyManager = new FS.HISFC.BizLogic.Material.ComCompany();

        /// <summary>
        /// ��������ҵ���� {5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private FS.HISFC.BizLogic.Material.Baseset bsManager = new FS.HISFC.BizLogic.Material.Baseset();

        /// <summary>
        /// s����������ʱ����
        /// </summary>
        FS.FrameWork.Models.NeuObject companyTemp = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool isSpecial = false;

        /// <summary>
        /// ������ �����б�����ʾ������ {7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private int visibleColumns = 3;

        /// <summary>
        /// ����  �Ƿ��������Ŀ¼���еĵ��ۺ���������
        /// </summary>
        private bool isUpdateUnitPrice = true;

        /// <summary>
        /// ��������
        /// </summary>
        private ArrayList alCompany = null;

        /// <summary>
        ///  ��������
        /// </summary>
        private In.ucMatIn ucInManager = null;

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucMatListSelect ucListSelect = null;

        /// <summary>
        /// �򿪴���ʱ����
        /// </summary>
        private DateTime sysDate = System.DateTime.MaxValue;

        private DataTable dt = null;

        /// <summary>
        /// ����ӡ����
        /// </summary>
        private List<FS.HISFC.Models.Material.Input> alInput = null;

        /// <summary>
        /// �������
        /// </summary>
        private System.Collections.Hashtable hsInputData = new System.Collections.Hashtable();

        /// <summary>
        /// �ɹ�����
        /// </summary>
        private System.Collections.Hashtable hsStockData = new System.Collections.Hashtable();

        //-------//liuxq ���ε��������ʱ���棩
        //		/// <summary>
        //		/// ǰ�޸����ݼ�ֵ
        //		/// </summary>
        //		private string privKey = "";
        //
        //		/// <summary>
        //		/// �Ƿ��жϵ�ǰѡ��Ĺ�����˾�������Ϣ�ڵĹ�����˾����ͬ
        //		/// </summary>
        //		private bool isJudgeDefaultCompany = false;
        //
        //		/// <summary>
        //		/// EditModeǰ��Ŀʵ��
        //		/// </summary>
        //		private FS.HISFC.Models.Material.Input privInput = null;
        //-------//liuxq ���ε��������ʱ���棩

        /// <summary>
        /// ��Ч���Զ���������
        /// </summary>
        private int autoValidYear = 5;
        /// <summary>
        /// �Ƿ񰴴��װ��ʽ���
        /// </summary>
        private bool isUsePackIn = false;

        /// <summary>
        /// ���뵥�Ƿ��Դ��װ��λ����
        /// </summary>
        private bool isPackApply = false;

        /// <summary>
        /// �������Ƿ��ظ�
        /// </summary>
        private bool isRepeatedStockNO = false;

        #endregion

        #region ����       
        /// <summary>
        /// ��Ч���Զ���������
        /// </summary>
        public int AutoValidYear
        {
            get
            {
                return this.autoValidYear;
            }
            set
            {
                this.autoValidYear = value;
            }
        }


        /// <summary>
        /// �Ƿ�ʹ�ô��װ��ʽ���
        /// </summary>
        public bool IsUsePackIn
        {
            get
            {
                return this.isUsePackIn;
            }
            set
            {
                this.isUsePackIn = value;
            }
        }

        /// <summary>
        /// ���뵥�Ƿ��Դ��װ��λ����
        /// </summary>
        public bool IsPackApply
        {
            get
            {
                return this.isPackApply;
            }
            set
            {
                this.isPackApply = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            this.sysDate = storeManager.GetDateTimeFromSysDateTime().Date;
            //�ɼ�����{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            this.visibleColumns = controlIntegrate.GetControlParam<int>("MT0002", true);
            //����ʱ�Ƿ���µ��ۺͳ���{5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
            this.isUpdateUnitPrice = controlIntegrate.GetControlParam<bool>("MT0003", true);

            return 1;
        }


        /// <summary>
        /// ��������������
        /// </summary>
        /// <returns></returns>
        private int SetMatManagerProperty(In.ucMatIn ucMatInManager)
        {
            this.ucInManager = ucMatInManager;

            if (this.ucInManager != null)
            {
                //���ý�����ʾ
                this.ucInManager.IsShowInputPanel = false;
                this.ucInManager.IsShowItemSelectpanel = true;
                //����Ŀ�������Ϣ
                this.ucInManager.SetTargetDept(true, true, FS.HISFC.Models.IMA.EnumModuelType.Material, FS.HISFC.Models.Base.EnumDepartmentType.L);
                //���ù�������ť��ʾ
                //{EA342FD4-AAE1-403f-9A48-C19368DC56AB} һ����ⲻ��Ҫ�������뵥
                //this.ucInManager.SetToolBarButtonVisible(true, false, false, true, true, true, false);
                this.ucInManager.SetToolBarButtonVisible(false, false, false, true, true, true, false);
                //������ʾ�Ĵ�ѡ������
                //by yuyun 08-8-11{5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
                this.ucInManager.DeptCode = this.ucInManager.DeptInfo.ID;
                //----------------------
                this.ucInManager.SetSelectData("0", false, null, null, null);
                //�����п��{7019A2A6-ADCA-4984-944B-C4F1A312449A}
                this.ucInManager.SetItemListWidth(visibleColumns);
                //��Ϣ˵������
                this.ucInManager.ShowInfo = "F5 ��ת����Ŀѡ���";
                //����Fp����
                this.ucInManager.Fp.EditModePermanent = false;
                this.ucInManager.Fp.EditModeReplace = true;

                this.ucInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(ucInManager_FpKeyEvent);

                this.ucInManager.EndTargetChanged -= new In.ucMatIn.DataChangedHandler(ucInManager_EndTargetChanged);
                this.ucInManager.EndTargetChanged += new In.ucMatIn.DataChangedHandler(ucInManager_EndTargetChanged);

                this.ucInManager.Fp.EditModeOn += new EventHandler(Fp_EditModeOn);
                this.ucInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);

                this.ucInManager.Fp.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(Fp_CellDoubleClick);
                this.ucInManager.Fp.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(Fp_CellDoubleClick);

                this.ucInManager.Fp.KeyDown -= new KeyEventHandler(Fp_KeyDown);
                this.ucInManager.Fp.KeyDown += new KeyEventHandler(Fp_KeyDown);
            }

            return 1;
        }
    

        /// <summary>
        /// ��ʵ����Ϣ����DataTable��
        /// </summary>
        /// <param name="input">�����Ϣ Input.User01�洢������Դ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Material.Input input)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                decimal inQty = 0;				//������� (���ݲ����԰�װ��λ����С��λ��ʾ)
                decimal inPrice = 0;			//��⹺��� ���ݲ���������װ�۸����С��λ�۸�
                string inUnit = "";				//��ⵥλ (���ݲ����԰�װ��λ����С��λ��ʾ)

                if (this.isUsePackIn)			//����װ��λ���
                {
                    inQty = input.StoreBase.Quantity;                                   //��װ�������
                    inPrice = input.StoreBase.Item.PackPrice;							//��װ��λ�۸�
                    inUnit = input.StoreBase.Item.PackUnit;								//��װ��λ
                }
                else
                {
                    inQty = input.StoreBase.Quantity * input.StoreBase.Item.PackQty;	//��С�������
                    inPrice = input.StoreBase.PriceCollection.PurchasePrice;			//��С��λ�۸�
                    inUnit = input.StoreBase.Item.MinUnit;								//��С��λ
                }

                input.StoreBase.RetailCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.RetailPrice;
                input.InCost = inQty * inPrice;
                input.StoreBase.PurchaseCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.PurchasePrice;
                //�������ֵ
                if (input.StoreBase.BatchNO == null)
                {
                    input.StoreBase.BatchNO = "";
                }
                this.dt.Rows.Add(new object[] {     
												  true,
												  input.StoreBase.Item.Name,                            //��Ʒ����
												  input.StoreBase.Item.Specs,                           //���
												  inQty,			    								//�������												  
												  inUnit,						                        //��װ��λ
												  input.StoreBase.Item.PackQty,                         //��װ����
												  inPrice,												//�����
												  input.InCost,									        //������ (����۽��)
												  input.StoreBase.BatchNO,                              //����
												  input.StoreBase.ValidTime,                            //��Ч��
                                                  //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ   {5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}                 
												  input.StoreBase.Producer.Name,						//��������
												  input.InvoiceNO,										//��Ʊ��
												  input.InvoiceTime,									//��Ʊ����
												  input.StoreBase.PriceCollection.RetailPrice,			//���ۼ� ��С��λ���ۼ�
												  input.StoreBase.RetailCost,							//���۽��
                                                  //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ
												  input.StoreBase.Item.ID,                              //��Ŀ����
												  input.ID,												//��ˮ��
												  input.User01,											//������Դ
												  input.StoreBase.Item.SpellCode,						//ƴ����
												  input.StoreBase.Item.WbCode,							//�����
												  input.StoreBase.Item.UserCode,						//�Զ�����	
												  input.User03,
                                                  //{461BD435-B028-4ba8-8D83-34BA69BA1758}
                                                  input.StoreBase.Producer.ID
											  }
                    );
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + e.Message);

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("DataTable�ڸ�ֵ��������" + ex.Message);

                return -1;
            }

            return 1;
        }


        /// <summary>
        /// ����Dr�����ݶ�ʵ����и�ֵ
        /// </summary>
        /// <param name="dr">���ݱ�</param>
        /// <param name="sysTime">��ǰʱ��</param>
        /// <param name="input">ref ���ʵ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int GetInputFormDataRow(DataRow dr, DateTime sysTime, ref FS.HISFC.Models.Material.Input input)
        {
            input.StoreBase.ValidTime = NConvert.ToDateTime(dr["��Ч��"]);
            //Ĭ����Ч�ڸ������ÿ���Ϊ0006-01-01 �� 0001-01-01 ���Ը��ݵ�ǰʱ��ǰ�������ж��Ƿ���������Ч��
            if (input.StoreBase.ValidTime < this.sysDate.AddYears(-5))
            {
                input.StoreBase.ValidTime = this.sysDate.AddYears(this.autoValidYear);
            }

            if (this.isUsePackIn)				//��װ��λ��� �۸����Ϊ��װ��λ�۸�
            {
                input.PackInQty = NConvert.ToDecimal(dr["�������"]);
                input.StoreBase.Quantity = NConvert.ToDecimal(dr["�������"]) * input.StoreBase.Item.PackQty;
                input.StoreBase.PriceCollection.PurchasePrice = NConvert.ToDecimal(dr["�����"]) / input.StoreBase.Item.PackQty;
            }
            else								//��С��λ��� 
            {
                input.PackInQty = NConvert.ToDecimal(dr["�������"]) / input.StoreBase.Item.PackQty;
                input.StoreBase.Quantity = NConvert.ToDecimal(dr["�������"]);
                input.StoreBase.PriceCollection.PurchasePrice = this.GetPrice(NConvert.ToDecimal(dr["�����"]));
            }
            input.InCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.PurchasePrice;

            //�����ۼ�Ϊ�� ��ֵ���ۼ�Ϊ�����
            if (input.StoreBase.PriceCollection.RetailPrice == 0)
            {
                input.StoreBase.PriceCollection.RetailPrice = input.StoreBase.PriceCollection.PurchasePrice;
            }                      

            //�������� ��Ĭ�ϲֿ⣩ �Ƿ��� Ĭ��Flase  ��������			
            input.StoreBase.BatchNO = dr["����"].ToString();
            input.InvoiceNO = dr["��Ʊ��"].ToString();
            input.InvoiceTime = NConvert.ToDateTime(dr["��Ʊ����"]);

            //{0637D5E9-BE00-4df7-B09D-23236A4259CF}
            input.StoreBase.Producer.ID = dr["��������ID"].ToString();     //��������ID
            input.StoreBase.Producer.Name = dr["��������"].ToString();  //������������
            //------------------------------------------------

            #region ������Ϣ��ÿ������������������Ϣʵ��ʱ��ֵ

            input.StoreBase.StockDept = this.ucInManager.DeptInfo;                       //������
            input.StoreBase.PrivType = this.ucInManager.PrivType.ID;                     //�û�����
            input.StoreBase.SystemType = this.ucInManager.PrivType.Memo;                 //ϵͳ����
            input.StoreBase.Company = this.ucInManager.TargetDept;                       //������λ 
            input.StoreBase.TargetDept = this.ucInManager.TargetDept;                    //Ŀ�굥λ = ������λ

            #endregion

            input.StoreBase.Operation.Oper.ID = this.ucInManager.OperInfo.ID;
            input.StoreBase.Operation.Oper.OperTime = sysTime;
            return 1;
        }


        /// <summary>
        /// ��ʽ��
        /// </summary>
        public virtual void SetFormat()
        {
            if (this.ucInManager.FpSheetView == null)
            {
                return;
            }

            this.ucInManager.FpSheetView.DefaultStyle.Locked = true;
            this.ucInManager.FpSheetView.DataAutoSizeColumns = false;

            this.ucInManager.FpSheetView.DefaultStyle.CellType = Function.GetReadOnlyCellType();

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Width = 38F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 65F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColUnit].Width = 60F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPackQty].Width = 60F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Width = 80F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Width = 80F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProducerName].Width = 120F;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numberCellType;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPackQty].Visible = false;			//��װ��λ
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColItemID].Visible = false;				//��Ʒ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInBillNO].Visible = false;			//��ˮ��
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColDataSource].Visible = false;			//������Դ
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;			//ƴ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;				//�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;			//�Զ�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColKey].Visible = false;                //����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Visible = false;             //���ۼ�
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailCost].Visible = false;              //���۽��

            //{0637D5E9-BE00-4df7-B09D-23236A4259CF}
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProducerID].Visible = false;              //��������ID

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Locked = false;			//��׼
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInQty].Locked = false;				//�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Locked = false;		//�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProducerName].Locked = false;    //��������
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Locked = false;				//����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColValidTime].Locked = false;			//��Ч��
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;			//��Ʊ��
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].Locked = false;			//��Ʊ����

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProducerName].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColValidTime].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInQty].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].BackColor = System.Drawing.Color.SeaShell;
        }


        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="listCode">���뵥��</param>
        /// <param name="state">״̬</param>
        /// <returns>�ɹ�����1 </ʧ�ܷ���-1returns>
        protected virtual int AddApplyData(string listCode, string state)
        {
            ArrayList al = this.storeManager.QueryApplyDetailByListNO(this.ucInManager.DeptInfo.ID, listCode, "0");
            if (al == null)
            {
                System.Windows.Forms.MessageBox.Show("δ��ȷ��ȡ�ⲿ���������Ϣ" + this.storeManager.Err);
                return -1;
            }

            this.Clear();

            FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

            foreach (FS.HISFC.Models.Material.Apply apply in al)
            {
                FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();
                FS.HISFC.Models.Material.MaterialItem tempItem = new FS.HISFC.Models.Material.MaterialItem();

                input.StoreBase.Item = itemManager.GetMetItemByMetID(apply.Item.ID);//�����ֵ�ʵ��	
                input.PlanListNO = apply.ID;										//�����ˮ��				
                input.StoreBase.StockDept = apply.StockDept;						//��沿��
                input.StoreBase.PrivType = this.ucInManager.PrivType.ID;			//ϵͳȨ��
                input.StoreBase.SystemType = this.ucInManager.PrivType.Memo;		//�û�����Ȩ��
                input.StoreBase.TargetDept = this.ucInManager.TargetDept;			//Ŀ�겿��					

                //�����������
                if (this.IsPackApply)//�����Ƿ�һ���װ��λ����
                {
                    input.StoreBase.Quantity = apply.Operation.ApplyQty;
                }
                else
                {
                    input.StoreBase.Quantity = apply.Operation.ApplyQty / input.StoreBase.Item.PackQty;
                }

                input.StoreBase.PriceCollection.PurchasePrice = input.StoreBase.Item.UnitPrice;


                input.User01 = "2";													//������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��
                input.User03 = this.GetKey();										//��ȡ����ֵ

                if (this.AddDataToTable(input) == 1)
                {
                    //��Input�����ڼ�����Ϣ
                    this.hsInputData.Add(this.GetKey(input), input);

                    this.SetFocusSelect();
                }
            }

            this.CompuateSum();

            return 1;
        }


        /// <summary>
        /// ���Ӳɹ�����
        /// </summary>
        /// <param name="listCode">�ɹ��ƻ�����</param>
        /// <param name="state">״̬</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int AddStockData(string listCode, string state)
        {
            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();
            ArrayList alStock = planManager.QueryInPlanDetailCom(this.ucInManager.DeptInfo.ID, listCode, this.ucInManager.TargetDept.ID);
            if (alStock == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ�ɹ���ϸ��Ϣ��������" + itemManager.Err);
                return -1;
            }

            this.Clear();

            foreach (FS.HISFC.Models.Material.InputPlan info in alStock)
            {
                if (info.State == "6")              //��ϸ��״̬Ϊ'6'�� ˵���ѽ��й���⴦�� ���ٴ���ʾ
                    continue;

                FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

                input.StoreBase.Item = this.itemManager.GetMetItemByMetID(info.StoreBase.Item.ID);	//�����ֵ�ʵ��	
                input.PlanListNO = info.PlanListCode;												//�����ˮ��				
                input.StoreBase.StockDept.ID = this.ucInManager.DeptInfo.ID;//info.StorageCode;		//��沿��
                input.StoreBase.PrivType = this.ucInManager.PrivType.ID;							//ϵͳȨ��
                input.StoreBase.SystemType = this.ucInManager.PrivType.Memo;						//�û�����Ȩ��
                input.StoreBase.TargetDept = this.ucInManager.TargetDept;							//Ŀ�겿��	
                input.StoreBase.Company = this.ucInManager.TargetDept;
                input.InvoiceNO = info.InvoiceNo;
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                input.StoreBase.Producer = info.Producer;
                //�����������
                if (this.IsPackApply)//�����Ƿ�һ���װ��λ����
                {
                    //input.StoreBase.Quantity = info.StockNum;
                    input.StoreBase.Quantity = info.PlanNum;
                }
                else
                {
                    //input.StoreBase.Quantity = info.StockNum / input.StoreBase.Item.PackQty;
                    input.StoreBase.Quantity = info.PlanNum;

                }

                input.StoreBase.PriceCollection.PurchasePrice = input.StoreBase.Item.UnitPrice;


                input.User01 = "1";													//������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��
                input.User03 = this.GetKey();										//��ȡ����ֵ

                if (this.AddDataToTable(input) == 1)
                {
                    //��Input�����ڼ�����Ϣ
                    this.hsInputData.Add(this.GetKey(input), input);

                    this.hsStockData.Add(this.GetKey(input), info);

                    this.SetFocusSelect();
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
            decimal purchaseCost = 0;

            if (this.dt != null)
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    purchaseCost += NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);
                }

                this.ucInManager.TotCostInfo = string.Format("������:{0}", purchaseCost.ToString("C4"));
            }
        }


        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <returns></returns>
        private string GetKey()
        {
            return System.Guid.NewGuid().ToString();
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            return dr["����"].ToString();
        }


        /// <summary>
        /// ������Ŀʵ���ȡ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Material.Input input)
        {
            return input.User03;
        }


        /// <summary>
        /// ��Fp�ڻ�ȡ����
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        /// <returns></returns>
        private string GetKey(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            return sv.Cells[activeRow, (int)ColumnSet.ColKey].Text;
        }


        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        /// <returns></returns>
        private string[] GetFindKey(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            return new string[] { sv.Cells[activeRow, (int)ColumnSet.ColKey].Text };
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="columnIndex"></param>
        private void GetCompany(int columnIndex)
        {
            //��¼��ǰλ��
            int i = this.ucInManager.FpSheetView.ActiveRowIndex;
            int j = this.ucInManager.FpSheetView.ActiveColumnIndex;
            if (this.ucInManager.FpSheetView.RowCount == 0)
            {
                return;
            }

            if (i < 0)
            {
                return;
            }

            FS.HISFC.Models.Material.Input inputTemp = new FS.HISFC.Models.Material.Input();
            inputTemp = this.ucInManager.FpSheetView.Rows[i].Tag as FS.HISFC.Models.Material.Input;

            if (columnIndex == (int)ColumnSet.ColProducerName)
            {
                if (this.alCompany == null)
                {
                    FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();

                    this.alCompany = company.QueryCompany("0", "A");

                    if (this.alCompany == null)
                    {
                        MessageBox.Show("��ȡ�������ҳ���");
                        return;
                    }
                }

                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref this.companyTemp) == 0)
                {
                    return;
                }
                else
                {
                    this.ucInManager.FpSheetView.Cells[i, (int)ColumnSet.ColProducerName].Value = companyTemp.Name;

                    //{0637D5E9-BE00-4df7-B09D-23236A4259CF} ������������ID
                    this.ucInManager.FpSheetView.Cells[i, (int)ColumnSet.ColProducerID].Value = companyTemp.ID;

                    this.ucInManager.FpSheetView.Cells[i, (int)ColumnSet.ColProducerName].Tag = this.companyTemp;
                }
            }
        }

        /// <summary>
        /// ���ʼӼ۴��� by yuyun 08-8-4{2F0031DE-9957-48f3-A3B3-F207D0696D56}
        /// </summary>
        /// <param name="input"></param>
        private void SetRetailPrice(ref FS.HISFC.Models.Material.Input input)
        {
            ArrayList al = bsManager.QueryAddRateByRateKind(input.StoreBase.Item.AddRule);
            FS.HISFC.Models.Material.MaterialAddRate addRate = new FS.HISFC.Models.Material.MaterialAddRate();
            switch (input.StoreBase.Item.AddRule)
            {
                //����ǰ��۸�Ӽ� ����ͨ������۲��Ҷ�Ӧ�ļӼ���
                case "P":
                    foreach (object obj in al)
                    {
                        FS.HISFC.Models.Material.MaterialAddRate tempAddRate = obj as FS.HISFC.Models.Material.MaterialAddRate;
                        if (input.StoreBase.PriceCollection.PurchasePrice >= tempAddRate.PriceLow && input.StoreBase.PriceCollection.PurchasePrice < tempAddRate.PriceHigh)
                        {
                            addRate = tempAddRate;

                            break;
                        }
                    }
                    break;
                //����ǰ����Ӽ� ����ͨ�������Ҷ�Ӧ�ļӼ���
                case "S":
                    foreach (object obj in al)
                    {
                        FS.HISFC.Models.Material.MaterialAddRate tempAddRate = obj as FS.HISFC.Models.Material.MaterialAddRate;
                        if (input.StoreBase.Item.Specs == tempAddRate.Specs)
                        {
                            addRate = tempAddRate;

                            break;
                        }
                    }
                    break;
                //����ǹ̶��Ӽ��� ��ֻȡ��һ���Ӽ�������
                case "R":
                    addRate = al[0] as FS.HISFC.Models.Material.MaterialAddRate;
                    break;
                default:
                    break;
            }
            //������ҵļӼ���Ϊ�գ������ۼ۵��ڹ���ۣ��������ۼ�  ����  �����*��1+�Ӽ۱��ʣ�+ ���ӷ�
            if (addRate == null || string.IsNullOrEmpty(addRate.ID))
            {
                input.StoreBase.PriceCollection.RetailPrice = input.StoreBase.PriceCollection.PurchasePrice;
            }
            else
            {
                input.StoreBase.PriceCollection.RetailPrice = input.StoreBase.PriceCollection.PurchasePrice * (1 + addRate.AddRate) + addRate.AppendFee;
            }
            input.StoreBase.Extend = input.StoreBase.PriceCollection.RetailPrice.ToString();
        }

        #endregion

        #region IMatInManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable InitDataTable()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            this.dt = new DataTable();

            this.dt.Columns.AddRange(
                new System.Data.DataColumn[] {
												 new DataColumn("��׼",		 dtBol),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",      dtStr),																							
												 new DataColumn("�������",  dtDec),
												 new DataColumn("��λ",      dtStr),
												 new DataColumn("��װ����",	 dtDec),
												 new DataColumn("�����",    dtDec),	
												 new DataColumn("�����",  dtDec),
												 new DataColumn("����",      dtStr),
												 new DataColumn("��Ч��",	 dtDate),
                                                 //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ{5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
                                                 new DataColumn("��������",  dtStr),
												 new DataColumn("��Ʊ��",    dtStr),
												 new DataColumn("��Ʊ����",  dtDate),
												 new DataColumn("���ۼ�",	 dtDec),
												 new DataColumn("���۽��",  dtDec),
												 //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ
												 new DataColumn("��Ŀ����",  dtStr),
												 new DataColumn("��ˮ��",	 dtStr),
												 new DataColumn("������Դ",  dtStr),
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr),
												 new DataColumn("����",		 dtStr),
                                                 //{461BD435-B028-4ba8-8D83-34BA69BA1758} ����һ����������ID��
                                                 new DataColumn("��������ID", dtStr)
											 }
                );

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            this.dt.DefaultView.AllowDelete = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowNew = true;

            return this.dt;
        }

        /// <summary>
        /// ��Ŀ��Ϣ����
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        /// <returns></returns>
        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            //-----by yuyun 08-7-25 ��һ�б���Զ�����  ԭ�Զ������г����ʱ���{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            //string itemNO = sv.Cells[activeRow, 0].Text;
            string itemNO = sv.Cells[activeRow, 10].Text;

            FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(itemNO);
            if (item == null)
            {
                MessageBox.Show("������Ŀ�����ȡ��Ŀ��Ϣʧ�� ����: " + itemNO);
                return -1;
            }

            foreach (DataRow dr in this.dt.Rows)
            {
                if (dr["��Ŀ����"].ToString() == item.ID)
                {
                    DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg(item.Name + " ����Ŀ�Ѿ���ӹ����Ƿ�ȷ�����������ŵ���"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.No)
                    {
                        isRepeatedStockNO = false;
                        return -1;
                    }
                    else
                    {
                        isRepeatedStockNO = true;
                    }
                }
            }

            this.ucInManager.AddNote((int)ColumnSet.ColItemID, (int)ColumnSet.ColTradeName);

            FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

            input.StoreBase.Item = item;				//��Ŀ��Ϣ
            input.StoreBase.PriceCollection.PurchasePrice = input.StoreBase.Item.UnitPrice;
            input.User01 = "0";							//������Դ
            input.User03 = this.GetKey();				//��ȡ����ֵ
            //-----˫��ѡ��������Ŀʱ  ������Ŀ¼�е����ɳ��Ҹ�ֵ����������ΪĬ�����ɳ��� by yuyun 08-7-25{5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
            input.StoreBase.Producer.ID = item.Factory.ID;
            FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();
            company = companyManager.QueryCompanyByCompanyID(item.Factory.ID, "1", "0");

            if (company!= null && !string.IsNullOrEmpty(company.ID))
            {
                input.StoreBase.Producer.Name = company.Name; 
            }
            //-------------------
            //-----Ĭ����Ч�����ǵ�ǰ���ڼ�һ�� by yuyun 08-7-25
            input.StoreBase.ValidTime = DateTime.Now.AddYears(1);
            //-------------------
            if (this.AddDataToTable(input) == 1)
            {
                //��Input�����ڼ�����Ϣ
                this.hsInputData.Add(this.GetKey(input), input);

                this.SetFocusSelect();

                return 1;
            }

            return 1; ;
        }

        /// <summary>
        /// ������Ʒ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        public int AddItem(FarPoint.Win.Spread.SheetView sv, FS.HISFC.Models.Material.Input input)
        {
            return 0;
        }

        public int ShowApplyList()
        {
            ArrayList alTemp = new ArrayList();
            //��ȡ������Ϣ{CAC9F782-773F-4507-AD2D-C0F73513FF42}
            string currentDeptID = string.Empty;
            currentDeptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            //��ȡ������Ϣ
            alTemp = this.storeManager.QueryApplySimple(this.ucInManager.DeptInfo.ID, currentDeptID, "0510", "0", "12");

            if (alTemp == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ������Ϣʧ��" + this.storeManager.Err);
                return -1;
            }

            FS.FrameWork.Models.NeuObject selectObject = new FS.FrameWork.Models.NeuObject();

            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alTemp, ref selectObject) == 1)
            {
                this.Clear();

                FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

                this.AddApplyData(selectObject.ID, "0");
                this.SetFocusSelect();

                if (this.ucInManager.FpSheetView != null)
                    this.ucInManager.FpSheetView.ActiveRowIndex = 0;
            }

            return 1;
        }

        public int ShowInList()
        {
            // TODO:  ��� CommonInPriv.ShowInList ʵ��
            return 0;
        }

        public int ShowOutList()
        {
            // TODO:  ��� CommonInPriv.ShowOutList ʵ��
            return 0;
        }

        public int ShowStockList()
        {
            try
            {
                if (this.ucListSelect == null)
                    this.ucListSelect = new ucMatListSelect();

                this.ucListSelect.Init();
                this.ucListSelect.DeptInfo = this.ucInManager.DeptInfo;
                this.ucListSelect.Class2Priv = "0512";          //�ɹ�
                this.ucListSelect.State = "2";                  //�����״̬
                this.ucListSelect.IsSelectState = false;

                this.ucListSelect.SelecctListEvent -= new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_StockSelecctListEvent);
                this.ucListSelect.SelecctListEvent += new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_StockSelecctListEvent);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            if (this.ucInManager.TargetDept.ID == "")
            {
                System.Windows.Forms.MessageBox.Show("��ѡ�񹩻���λ��");
                return false;
            }
            try
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (NConvert.ToDecimal(dr["�������"]) <= 0)
                    {
                        System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + "�����������С�ڵ����㣡");
                        return false;
                    }
                    if (NConvert.ToDecimal(dr["�����"]) <= 0)
                    {
                        System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + "����۲���С�ڵ����㣡");
                        return false;
                    }
                    if(NConvert.ToDateTime(dr["��Ч��"]) <= itemManager.GetDateTimeFromSysDateTime())
                    {
                        System.Windows.Forms.MessageBox.Show("��Ч��Ӧ���ڵ�ǰʱ�䣡");
                        return false;
                    }
                    if(dr["��������"].ToString() == string.Empty)
                    {
                        System.Windows.Forms.MessageBox.Show("��ѡ���������ң�");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="delRowIndex"></param>
        /// <returns></returns>
        public int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex)
        {
            try
            {
                if (sv != null && delRowIndex >= 0)
                {
                    DataRow dr = this.dt.Rows.Find(this.GetFindKey(sv, delRowIndex));
                    if (dr != null)
                    {
                        this.hsInputData.Remove(this.GetKey(sv, delRowIndex));

                        this.dt.Rows.Remove(dr);
                        //�ϼƼ���
                        this.CompuateSum();
                    }
                }
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show("�����ݱ�ִ��ɾ��������������" + e.Message);
                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("�����ݱ�ִ��ɾ��������������" + ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            this.hsInputData.Clear();

            this.dt.Rows.Clear();

            this.dt.AcceptChanges();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filterStr"></param>
        public void Filter(string filterStr)
        {
            if (this.dt == null)
                return;

            //��ù�������
            string queryCode = "%" + filterStr + "%";

            string filter = Function.GetFilterStr(this.dt.DefaultView, queryCode);

            try
            {
                this.dt.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("���˷����쳣 " + ex.Message);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void SetFocusSelect()
        {
            if (this.ucInManager.FpSheetView != null)
            {
                if (this.ucInManager.FpSheetView.Rows.Count > 0)
                {
                    this.ucInManager.SetFpFocus();

                    this.ucInManager.FpSheetView.ActiveRowIndex = this.ucInManager.FpSheetView.Rows.Count - 1;
                    this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInQty;
                }
                else
                {
                    this.ucInManager.SetFocus();
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save()
        {
            if (!this.Valid())
            {
                return;
            }

            this.dt.DefaultView.RowFilter = "1=1";
            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numberCellType;

            DataTable dtAddMofity = this.dt.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
                return;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б������..���Ժ�");
            System.Windows.Forms.Application.DoEvents();

            #region ������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            //�����������
            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();
            //��ⵥ�ݺ�
            string inListNO = null;
            this.alInput = new List<FS.HISFC.Models.Material.Input>();

            try
            {
                FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();
                FS.HISFC.Models.Material.InputPlan inputPlan = new FS.HISFC.Models.Material.InputPlan();
                int serialNO = 0;
                string stockNO = null;

                foreach (DataRow dr in dtAddMofity.Rows)
                {
                    string key = this.GetKey(dr);

                    input = this.hsInputData[key] as FS.HISFC.Models.Material.Input;


                    inputPlan = this.hsStockData[key] as FS.HISFC.Models.Material.InputPlan;
                    //�����ݱ��ڻ�ȡ���������Ϣʵ��
                    this.GetInputFormDataRow(dr, sysTime, ref input);

                    serialNO++;											//����˳���
                    input.StoreBase.SerialNO = serialNO;

                    #region �¿�����(����)��Ϣ ÿһ������¼����һ���¿����ţ����κţ�

                    if ((stockNO == null) || (isRepeatedStockNO == true))
                    {
                        input.StoreBase.StockNO = this.storeManager.GetNewStockNO();
                        if (input.StoreBase.StockNO == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("δ��ȷ��ȡ��������ˮ��" + storeManager.Err);
                            return;
                        }
                        stockNO = input.StoreBase.StockNO;
                    }
                    else
                    {
                        input.StoreBase.StockNO = stockNO;
                    }

                    #endregion

                    #region �����������ⵥ�ݺ� ���ȡ����ⵥ�ݺ�

                    //��ⵥ��
                    if (inListNO == null)
                    {
                        inListNO = input.InListNO;
                    }

                    if (inListNO == "" || inListNO == null)
                    {
                        inListNO = this.storeManager.GetInListNO(this.ucInManager.DeptInfo.ID);
                        if (inListNO == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("��ȡ������ⵥ�ų���" + this.storeManager.Err);
                            return;
                        }
                    }

                    input.InListNO = inListNO;

                    #endregion

                    #region ���ǰ�������

                    decimal storeQty = 0;
                    if (this.storeManager.GetStoreQty(input.StoreBase.StockDept.ID, input.StoreBase.Item.ID, out storeQty) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg("��ȡ�������ʱ����" + storeManager.Err);
                        return;
                    }
                    input.StoreBase.StoreQty = storeQty;               //���ǰ�������
                    input.StoreBase.StoreCost = Math.Round(input.StoreBase.StoreQty / input.StoreBase.Item.PackQty * input.StoreBase.PriceCollection.PurchasePrice, 3);

                    #endregion

                    #region ���ݲ�ͬ������� ���������Ϣ״̬

                    if (input.StoreBase.Operation.ApplyOper.ID == "")
                    {
                        input.StoreBase.Operation.ApplyQty = input.StoreBase.Quantity;                          //���������
                        input.StoreBase.Operation.ApplyOper = input.StoreBase.Operation.Oper;
                    }

                    input.StoreBase.State = "0";
                    if (input.InvoiceNO != "")                //�����뷢Ʊ�� ֱ������״̬Ϊ��Ʊ���
                    {
                        input.StoreBase.Operation.ExamQty = input.StoreBase.Quantity;
                        input.StoreBase.Operation.ExamOper = input.StoreBase.Operation.Oper;
                        input.StoreBase.State = "1";
                    }

                    //�������Ϊ�������
                    if (this.isSpecial)
                    {
                        input.StoreBase.State = "2";
                        input.StoreBase.Operation.ExamQty = input.StoreBase.Quantity;
                        input.StoreBase.Operation.ExamOper = input.StoreBase.Operation.Oper;
                        input.StoreBase.Operation.ApplyOper = input.StoreBase.Operation.Oper;
                    }

                    #endregion

                    //���ʼӼ۴��� by yuyun 08-8-4{2F0031DE-9957-48f3-A3B3-F207D0696D56}
                    //input.StoreBase.PriceCollection.RetailPrice = input.StoreBase.PriceCollection.PurchasePrice;
                    this.SetRetailPrice(ref input);

                    input.StoreBase.RetailCost = input.StoreBase.StoreQty;
                    input.InFormalTime = input.StoreBase.Operation.Oper.OperTime;		//��ʽ������� һ�����ʱ���ǵ�ǰ����ʱ��
                    input.StoreBase.Returns = 0.0000M;

                    //{0637D5E9-BE00-4df7-B09D-23236A4259CF}
                    //input.StoreBase.Producer.ID = this.companyTemp.ID;

                    if (this.storeManager.Input(input.Clone(), "1", input.StoreBase.State == "2" ? "1" : "0") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMsg("��� ����ʧ��" + this.storeManager.Err);

                        return;
                    }

                    #region ��������Ŀ¼�е����ۺ��������� by yuyun 08-7-28{5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
                    if (isUpdateUnitPrice == true)
                    {
                        if (this.storeManager.UpdateUnitPriceAndFactory(input.StoreBase.Item.ID, input.StoreBase.PriceCollection.PurchasePrice, input.StoreBase.Producer.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMsg("����Ŀ¼�����ۺ���������ʧ��" + this.storeManager.Err);

                            return;
                        } 
                    }
                    #endregion

                    #region ���ݲ�ͬ������Դ�����ݽ��и���

                    switch (dr["������Դ"].ToString())
                    {
                        case "0":           //�ֹ�ѡ��
                            break;
                        case "2":           //����

                            if (this.storeManager.UpdateApplyApproveState(input.PlanListNO, "1", input.StoreBase.Operation.Oper) == -1)//input.StoreBase.StockDept.ID
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                Function.ShowMsg("�����׼ʧ��" + itemManager.Err);

                                return;
                            }

                            break;
                        case "1":           //�ɹ�

                            if (this.storeManager.UpdatePlanInputState(inputPlan.StorageCode, inputPlan.PlanListCode, inputPlan.PlanNo, "3", input.StoreBase.Operation.Oper.ID, input.StoreBase.Operation.Oper.OperTime) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                Function.ShowMsg("�����׼ʧ��" + itemManager.Err);

                                return;
                            }
                            break;
                    }

                    #endregion

                    alInput.Add(input);

                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg(ex.Message);

                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (this.isSpecial)
            {
                Function.ShowMsg("������Ᵽ��ɹ�");
            }
            else
            {
                Function.ShowMsg("һ����Ᵽ��ɹ�");
            }

            if (alInput.Count > 0)
            {
                if (MessageBox.Show("�Ƿ��ӡ?", "��ʾ:", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Print();
                }

            }
            this.Clear();

            FarPoint.Win.Spread.CellType.NumberCellType noCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            noCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = noCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = noCellType;
        }

        public void SaveCheck(bool IsHeaderCheck)
        {

        }

        public int Print()
        {
            if(this.ucInManager.IInPrint != null)
            {
                this.ucInManager.IInPrint.SetData(this.alInput);
            }
            return 1;
        }

        public int Cancel()
        {
            // TODO:  ��� InApplyPriv.Print ʵ��
            return 1;
        }

        public int ImportData()
        {
            return 1;
        }

        #endregion

        #region IMatManager ��Ա

        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        //���ͷŵ��¼���Դ
        public void Dispose()
        {
            this.ucInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(ucInManager_FpKeyEvent);

            this.ucInManager.EndTargetChanged -= new In.ucMatIn.DataChangedHandler(ucInManager_EndTargetChanged);

            this.ucInManager.Fp.EditModeOn -= new EventHandler(Fp_EditModeOn);
            this.ucInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);

            this.ucInManager.Fp.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(Fp_CellDoubleClick);

            this.ucInManager.Fp.KeyDown -= new KeyEventHandler(Fp_KeyDown);
        }

        #endregion

        #region �¼�������

        private void Fp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColProducerName)
                {
                    this.GetCompany((int)ColumnSet.ColProducerName);
                }
            }
        }

        private void ucInManager_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            return;
        }

        private void ucInManager_FpKeyEvent(Keys key)
        {
            if (key == Keys.Enter)
            {
                #region �س���ת
                #region ϵͳ�л���ʱ�����⴦��
                if (this.ucInManager.PrivType.Memo == "1C")
                {
                    if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInQty)
                    {
                        if (this.ucInManager.FpSheetView.ActiveRowIndex == this.ucInManager.FpSheetView.Rows.Count - 1)
                        {
                            this.ucInManager.SetFocus();
                        }
                        else
                        {
                            this.ucInManager.FpSheetView.ActiveRowIndex++;
                            this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInQty;
                        }
                    }
                    return;
                }
                #endregion

                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColProducerName)
                {
                    if (this.ucInManager.FpSheetView.ActiveRowIndex == this.ucInManager.FpSheetView.Rows.Count - 1)
                    {
                        this.ucInManager.SetFocus();
                    }
                    else
                    {
                        this.ucInManager.FpSheetView.ActiveRowIndex++;
                        this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInQty;
                    }

                    return;
                }
                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInQty)
                {
                    this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColPurchasePrice;

                    return;
                }
                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
                {
                    this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColBatchNO;

                    return;
                }

                this.ucInManager.FpSheetView.ActiveColumnIndex++;

                while (!this.ucInManager.FpSheetView.Columns[this.ucInManager.FpSheetView.ActiveColumnIndex].Visible)
                {
                    if (this.ucInManager.FpSheetView.Columns.Count > this.ucInManager.FpSheetView.ActiveColumnIndex)
                        this.ucInManager.FpSheetView.ActiveColumnIndex++;
                }

                #endregion
            }

            if (key == Keys.F5)
            {
                this.ucInManager.SetFocus();
            }
        }

        private void Fp_EditModeOn(object sender, EventArgs e)
        {


            /*  ����EditModeOn����

            if (this.hsInputData.Contains(this.GetKey(this.ucInManager.FpSheetView,this.ucInManager.FpSheetView.ActiveRowIndex)))
            {
                this.privInput = this.hsInputData[this.GetKey(this.ucInManager.FpSheetView,this.ucInManager.FpSheetView.ActiveRowIndex)] as FS.HISFC.Models.Material.Input;

                this.privKey = this.GetKey(this.ucInManager.FpSheetView,this.ucInManager.FpSheetView.ActiveRowIndex);
            }
            else
            {
                this.privInput = null;
            }

            */
        }

        private void Fp_EditModeOff(object sender, EventArgs e)
        {

            if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInQty || this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
            {
                DataRow dr = this.dt.Rows.Find(this.GetFindKey(this.ucInManager.FpSheetView, this.ucInManager.FpSheetView.ActiveRowIndex));
                if (dr != null)
                {
                    dr["�����"] = NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);

                    dr.EndEdit();

                    this.CompuateSum();
                }
            }

            /*  �������´��� EditModeOff ��������ʾ

            if (this.privInput != null)
            {
                int iRow = this.ucInManager.FpSheetView.ActiveRowIndex;
                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColBatchNO)
                {
                    if (this.hsInputData.ContainsKey(this.privKey))
                    {
                        this.privInput.StoreBase.BatchNO = this.ucInManager.FpSheetView.Cells[iRow,(int)ColumnSet.ColBatchNO].Text;

                        this.hsInputData.Remove(this.privKey);

                        this.hsInputData.Add(this.GetKey(this.ucInManager.FpSheetView,this.ucInManager.FpSheetView.ActiveRowIndex),this.privInput);
                    }
                }

                if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
                {
                    if (this.hsInputData.ContainsKey(this.privKey))
                    {
                        this.privInput.StoreBase.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.ucInManager.FpSheetView.Cells[iRow,(int)ColumnSet.ColBatchNO].Text);

                        this.hsInputData.Remove(this.privKey);

                        this.hsInputData.Add(this.GetKey(this.ucInManager.FpSheetView,this.ucInManager.FpSheetView.ActiveRowIndex),this.privInput);
                    }
                }
            }
			
            */
        }

        private void Fp_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            this.GetCompany(e.Column);
        }

        private void ucListSelect_StockSelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            //������λ
            this.ucInManager.TargetDept = targetDept;
            //���Ӳɹ�����
            this.AddStockData(listCode, state);
        }       

        /// <summary>
        /// ����˰ǰ��˰��۸�
        /// </summary>
        /// <param name="decPrice"></param>
        /// <returns></returns>
        private decimal GetPrice(decimal decPrice)
        {
            //if (this.ucInManager.rbnPRe.Checked)
            //{
            return decPrice;
            //}
            //else
            //{
            //    return Math.Round(decPrice * (decimal)1.17, 2);   //1.17��˰��
            //}
        }
        #endregion

        #region ��ö��
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
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// �����������װ��λ������
            /// </summary>
            ColInQty,
            /// <summary>
            /// ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// �����
            /// </summary>
            ColPurchasePrice,
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
            //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ    {5C88E1AE-FCB7-4d88-B23B-7F67291CBB04}
            /// <summary>
            /// ��������
            /// </summary>
            ColProducerName,
            /// <summary>
            /// ��Ʊ��
            /// </summary>
            ColInvoiceNO,
            /// <summary>
            /// ��Ʊ����
            /// </summary>
            ColInvoiceTime,
            /// <summary>
            /// ���ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ���۽��
            /// </summary>
            ColRetailCost,
            //-----by yuyun 08-7-25 Ϊ��������������һ����������ʾ  �����ɳ�����ǰ            
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ColItemID,
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
            ColUserCode,
            /// <summary>
            /// ����
            /// </summary>
            ColKey,
            /// <summary>
            /// ��������ID {0637D5E9-BE00-4df7-B09D-23236A4259CF}
            /// </summary>
            ColProducerID
        }
       #endregion
    }
}
