using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FS.HISFC.Components.Common.Controls;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: �����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// <�޸ļ�¼>
    ///   1���޸�˫���޸�ҩƷʱ����ѡ��ҩƷ�󱣴治�϶�ֱ�Ӹ��ǵ����� by Sunjh 2010-8-23 {9EEBBBFA-AB66-41aa-A9DB-0E48FF995EFB}
    ///   2������ֱ��ɾ���ⲿ������빦�� by Sunjh 2010-8-23 {EB33BF6F-D122-4330-8D89-BB8695DD5A48}
    /// </�޸ļ�¼>
    /// </summary>
    public class OuterApplyPriv : IPhaInManager
    {
        public OuterApplyPriv(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.SetPhaManagerProperty(ucPhaManager);
            }
        }

        #region �����

        private ucPhaIn phaInManager = null;

        private FarPoint.Win.Spread.SheetView svTemp = null;

        private DataTable dt = null;

        ucCommonInDetail ucDetail = null;

        /// <summary>
        /// �������
        /// </summary>
        private System.Collections.Hashtable hsInputData = new System.Collections.Hashtable();

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

                    this.phaInManager.TargetDept = new FS.FrameWork.Models.NeuObject();

                    //���ù�������ť��ʾ
                    this.phaInManager.SetToolBarButtonVisible(true, false, false, true, true, true, true);
                }
                else
                {
                    this.phaInManager.SetTargetDept(false, true, true, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);

                    this.phaInManager.TargetDept = new FS.FrameWork.Models.NeuObject();

                    //���ù�������ť��ʾ
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

            //�򿪴�������
            this.sysTime = ctrlManager.GetDateTimeFromSysDateTime().Date;
        }

        /// <summary>
        /// ת��
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataSource">������Դ 1 �ɹ��� 2 ���뵥 0 �ֹ�ѡ��</param>
        /// <returns></returns>
        protected FS.HISFC.Models.Pharmacy.Input ConvertToInput(FS.HISFC.Models.Pharmacy.Item item, string dataSource)
        {
            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

            input.Item = item;

            #region ʵ�帳ֵ

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
        protected virtual void SetFormat()
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

            this.svTemp.Columns[(int)ColumnSet.ColValidTime].Visible = false;        //��Ч��
            this.svTemp.Columns[(int)ColumnSet.ColInvoiceType].Visible = false;      //��Ʊ����
            this.svTemp.Columns[(int)ColumnSet.ColProducerName].Visible = false;     //��������
            this.svTemp.Columns[(int)ColumnSet.ColDrugID].Visible = false;           //ҩƷ����
            this.svTemp.Columns[(int)ColumnSet.ColInBillNO].Visible = false;         //��ˮ��
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
        protected virtual int AddApplyData(string listCode, string state)
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

                input.Item = tempItem;                               //ҩƷʵ����Ϣ
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

            this.SetFormat();

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
            return 1;
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <returns></returns>
        public int ImportData()
        {           
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

                        //����ֱ��ɾ���ⲿ������빦�� by Sunjh 2010-8-23 {EB33BF6F-D122-4330-8D89-BB8695DD5A48}
                        if (MessageBox.Show("�Ƿ�ɾ����ǰ������Ϣ������ֱ���ύ����", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                            if (itemManager.DeleteApplyIn(input.ID) == -1)
                            {
                                MessageBox.Show("ɾ��ʧ��!");
                            }
                        }
                        else
                        {
                            return -1;
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

            this.dt.DefaultView.RowFilter = filter;

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

            //�����������
            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();
            //��ⵥ�ݺ�
            string inListNO = null;

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();
            foreach (DataRow dr in this.dt.Rows)
            {
                string key = dr["ҩƷ����"].ToString() + dr["����"].ToString();

                input = this.hsInputData[key] as FS.HISFC.Models.Pharmacy.Input;

                if (inListNO == null)
                    inListNO = input.InListNO;

                #region �����������ⵥ�ݺ� ���ȡ����ⵥ�ݺ�

                if (inListNO == "" || inListNO == null)
                {
                    // //{59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ������ⵥ�Ż�ȡ��ʽ
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

                input.StockDept = this.phaInManager.DeptInfo;                       //������
                input.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
                input.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
                input.Company = this.phaInManager.TargetDept;                       //������λ 
                input.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ

                #endregion

                if (input.Operation.ApplyOper.ID == "")
                {
                    input.Operation.ApplyQty = input.Quantity;                          //���������
                    input.Operation.ApplyOper.ID = this.phaInManager.OperInfo.ID;
                    input.Operation.ApplyOper.OperTime = sysTime;
                }

                input.Operation.Oper.ID = this.phaInManager.OperInfo.ID;
                input.Operation.Oper.OperTime = sysTime;
                input.Operation.ApplyQty = input.Quantity;                          //���������

                input.State = "0";

                if (itemManager.SetApplyIn(input) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMsg("��� ����ʧ��" + itemManager.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.ShowMsg("�ⲿ������뱣��ɹ�");

            this.Clear();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            return 1;
        }

        #endregion

        #region IPhaInManager ��Ա

        public int Dispose()
        {
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
                        DialogResult rs = MessageBox.Show(Language.Msg("��ǰѡ��Ĺ�����λ��ҩƷά����Ĭ�Ϲ�����λ��ͬ �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (rs == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                #endregion

                string key = tempInput.Item.ID + tempInput.BatchNO;

                #region �жϸ�ҩƷ��Ϣ�Ƿ���� ���������ɾ��ԭ��Ϣ ���¸�ֵ

                if (this.privKey != "" && this.privKey.Substring(0, 12) != key.Substring(0, 12))
                {
                    this.privKey = "";                    
                }

                if (!this.hsInputData.ContainsKey(this.privKey))
                {
                    tempInput.ID = "";//�޸�˫���޸�ҩƷʱ����ѡ��ҩƷ�󱣴治�϶�ֱ�Ӹ��ǵ����� by Sunjh 2010-8-23 {9EEBBBFA-AB66-41aa-A9DB-0E48FF995EFB}
                }

                //������ ɾ��ԭ��Ϣ ������� �����ظ��������
                if (this.privKey.Length == 12)
                {
                    if (this.hsInputData.ContainsKey(this.privKey))
                    {
                        this.hsInputData.Remove(this.privKey);
                        string[] keys = new string[] { this.privKey.Substring(0, 12), "" };
                        DataRow drFind = this.dt.Rows.Find(keys);
                        if (drFind != null)
                        {
                            this.dt.Rows.Remove(drFind);
                        }
                    }
                }
                //����ͬҩƷ/���� ɾ��ԭ���� 
                if (this.hsInputData.ContainsKey(key))
                {
                    this.hsInputData.Remove(key);
                    string[] keys = new string[] { tempInput.Item.ID, tempInput.BatchNO };
                    DataRow drFind = this.dt.Rows.Find(keys);
                    if (drFind != null)
                    {
                        this.dt.Rows.Remove(drFind);
                    }
                }

                #endregion

                #region ʵ�帳ֵ

                tempInput.StockDept = this.phaInManager.DeptInfo;                       //������
                tempInput.PrivType = this.phaInManager.PrivType.ID;                     //�û�����
                tempInput.SystemType = this.phaInManager.PrivType.Memo;                 //ϵͳ����
                tempInput.Company = this.phaInManager.TargetDept;                       //������λ 
                tempInput.TargetDept = this.phaInManager.TargetDept;                    //Ŀ�굥λ = ������λ

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
