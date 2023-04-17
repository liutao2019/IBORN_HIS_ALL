using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: ����˿�ҵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// 
    /// ��ע��
    /// ����˿�ȡ�����ҩƷ ������ָ��������˾��ȡ
    /// </summary>
    public class BackInPriv : IPhaInManager
    {
        public BackInPriv(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.SetPhaManagerProperty(ucPhaManager);
            }
        }

        #region �����

        private ucPhaIn phaInManager = null;

        private DataTable dt = null;

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �洢����ӵ�����
        /// </summary>
        private System.Collections.Hashtable hsInData = new Hashtable();

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucPhaListSelect ucListSelect = null;

        /// <summary>
        /// ����Ƿ���Ҫ��׼
        /// </summary>
        private bool IsNeedApprove = false;

        /// <summary>
        /// ����ӡ����
        /// </summary>
        private ArrayList alPrintData = null;

        /// <summary>
        /// ҩƷ��ѡ���б���ʾ�ؼ�
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucDrugList ucBackDrugSelectedList = null;

        #endregion

        /// <summary>
        /// /��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsNeedApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.In_Need_Approve, true, true);
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="ucPhaManager"></param>
        protected void SetPhaManagerProperty(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.phaInManager = ucPhaManager;

            if (this.phaInManager != null)
            {
                //����Ŀ�������Ϣ  ��ʾ������λ�б�
                this.phaInManager.SetTargetDept( true, false, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P );
                //�������������  
                this.phaInManager.IsShowItemSelectpanel = false;

                //{1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿����
                this.phaInManager.IsShowInputPanel = true;
                this.ShowSelectData();

                //���ù�������ť��ʾ
                this.phaInManager.SetToolBarButton( false, true, false, false, true );
                this.phaInManager.SetToolBarButtonVisible( false, true, false, false, true, true, false );
                //������Ŀ�б���
                this.phaInManager.SetItemListWidth( 2 );
                //����Fp
                this.phaInManager.Fp.EditModePermanent = false;
                this.phaInManager.Fp.EditModeReplace = true;
                this.phaInManager.FpSheetView.DataAutoSizeColumns = false;

                this.phaInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler( value_EndTargetChanged );
                this.phaInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler( value_EndTargetChanged );

                this.phaInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler( value_FpKeyEvent );
                this.phaInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler( value_FpKeyEvent );

                this.phaInManager.Fp.EditModeOff -= new EventHandler( Fp_EditModeOff );
                this.phaInManager.Fp.EditModeOff += new EventHandler( Fp_EditModeOff );
            }
        }

        /// <summary>
        /// ������ʾ����
        /// 
        /// {1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿���� �޸�������ʾ���ҽṹΪ���½ṹ
        /// </summary>
        /// <returns></returns>
        private int ShowSelectData()
        {
            string[] filterStr = new string[4] { "SPELL_CODE", "WB_CODE", "REGULAR_SPELL", "REGULAR_WB" };
            string[] label = new string[] { "ҩƷ����", "����", "��Ʒ����", "���", "����","�����", "����� [��װ��λ]", "ƴ����", "�����", "ͨ����ƴ����", "ͨ���������" };
            int[] width = new int[] { 60, 100, 220, 80, 80, 120, 170, 60, 60, 60, 60 };
            bool[] visible = new bool[] { false, true, true, true, true, true, true, false, false, false, false };

            //����˿�ȡ�����ҩƷ ������ָ��������˾��ȡ
            //����˿�ȡ����ڿ��������0��ҩƷ

            //this.phaInManager.SetSelectData("3", false, new string[] { "Pharmacy.Item.GetStorageForBackIn" }, filterStr, this.phaInManager.DeptInfo.ID);

            //this.phaInManager.SetSelectFormat(label, width, visible);

            //��ʼ��    {1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿���� �޸�������ʾ���ҽṹΪ���½ṹ
            this.InitBackDrugSelectedListUC();
          
            this.ucBackDrugSelectedList.ShowInfoList( "Pharmacy.Item.GetStorageForBackIn", filterStr, this.phaInManager.DeptInfo.ID );
            this.ucBackDrugSelectedList.SetFormat( label, width, visible );

            return 1;
        }

        /// <summary>
        /// ��ʼ���˿�ҩƷ�б�ѡ��ؼ�
        /// 
        /// {1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿���� �޸�������ʾ���ҽṹΪ���½ṹ
        /// </summary>
        /// <returns></returns>
        private int InitBackDrugSelectedListUC()
        {
            if (this.ucBackDrugSelectedList == null)
            {
                this.ucBackDrugSelectedList = new ucDrugList();
                this.ucBackDrugSelectedList.Caption = "��ǰ���ҩƷ�б�";
                this.ucBackDrugSelectedList.DataAutoCellType = false;
                this.ucBackDrugSelectedList.Height = 180;
                this.ucBackDrugSelectedList.ChooseDataEvent += new ucDrugList.ChooseDataHandler( ucBackDrugSelectedList_ChooseDataEvent );
            }

            return 1;
        }

        /// <summary>
        ///  ҩƷ�б�����ѡ���¼�����
        ///  
        ///  {1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿���� �޸�������ʾ���ҽṹΪ���½ṹ
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        private void ucBackDrugSelectedList_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            this.AddItem( sv, activeRow );   
        }

        /// <summary>
        /// ��ʵ����Ϣ����DataTable��
        /// </summary>
        /// <param name="input">�����Ϣ</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Pharmacy.Input input)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                input.RetailCost = (input.Quantity - input.Operation.ReturnQty) / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;

                this.dt.Rows.Add(new object[] { 
                                                input.Item.Name,                            //��Ʒ����
                                                input.Item.Specs,                           //���
                                                input.Item.PriceCollection.RetailPrice,     //���ۼ�
                                                input.BatchNO,                              //����
                                                input.Item.PackUnit,                        //��װ��λ
                                                (input.Quantity - input.Operation.ReturnQty) / input.Item.PackQty,        //�������
                                                input.RetailCost,                           //�����   
                                                0,                                          //�˿�����
                                                0,                                          //�˿���
                                                input.InvoiceNO,                            //��Ʊ��
                                                input.InvoiceType,                          //��Ʊ���
                                                input.Memo,                                 //��ע
                                                input.Item.ID,                              //ҩƷ����
                                                input.GroupNO,                              //����
                                                input.Item.NameCollection.SpellCode,        //ƴ����
                                                input.Item.NameCollection.WBCode,           //�����
                                                input.Item.NameCollection.UserCode          //�Զ�����
                            
                                           }
                                );
            }
            #region {CAD2CB10-14FE-472c-A7D7-9BAA5061730C}
            catch (System.Data.ConstraintException cex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩƷ��ѡ�����ظ�ѡ��"));

                return -1;
            }
            #endregion
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
        /// ����������
        /// </summary>
        /// <param name="listNO">��ⵥ��</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        protected virtual int AddInData(string listNO, string state)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ��ݵ��ݼ������� ���Ժ�...");
            Application.DoEvents();

            ArrayList alDetail = this.itemManager.QueryInputInfoByListID(this.phaInManager.DeptInfo.ID, listNO, this.phaInManager.TargetDept.ID, state);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return -1;
            }

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.Input input in alDetail)
            {
                input.PrivType = this.phaInManager.PrivType.ID;             //�������
                input.SystemType = this.phaInManager.PrivType.Memo;         //ϵͳ����

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInData.Add(this.GetKey(input), input);
                }
                else
                {
                    return -1;
                }
            }

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.SetFocusSelect();

            return 1;
        }

        /// <summary>
        /// ����ҩƷ���������κż�������˿���Ϣ
        /// </summary>
        /// <param name="drugNO"></param>
        /// <param name="groupNO"></param>
        /// <returns></returns>
        protected virtual int AddInData(string drugNO, int groupNO)
        {
            if (this.hsInData.ContainsKey(drugNO + groupNO))
            {
                MessageBox.Show(Language.Msg("��ҩƷ�����"));
                return 0;
            }
            ArrayList alDetail = this.itemManager.QueryStorageList(this.phaInManager.DeptInfo.ID, drugNO, groupNO);
            if (alDetail == null || alDetail.Count == 0)
            {
                MessageBox.Show(Language.Msg("δ��ȡ��Ч�Ŀ����ϸ��Ϣ" + this.itemManager.Err));
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Storage storage = alDetail[0] as FS.HISFC.Models.Pharmacy.Storage;

            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

            input.StockDept = storage.StockDept;                //������
            input.TargetDept = this.phaInManager.TargetDept;    //Ŀ�����
            input.Company = this.phaInManager.TargetDept;
            input.Item = storage.Item;
            input.GroupNO = storage.GroupNO;
            input.Quantity = storage.StoreQty;                  //����� = �����
            input.BatchNO = storage.BatchNO;
            input.ValidTime = storage.ValidTime;
            input.PlaceNO = storage.PlaceNO;
            input.InvoiceNO = storage.InvoiceNO;
            input.Producer = storage.Producer;
            input.Memo = storage.Memo;
            input.PrivType = this.phaInManager.PrivType.ID;             //�������
            input.SystemType = this.phaInManager.PrivType.Memo;         //ϵͳ����

            if (this.AddDataToTable(input) == 1)
            {
                this.hsInData.Add(this.GetKey(input), input);
            }

            this.SetFormat();

            this.SetFocusSelect();

            return 1;
        }

        /// <summary>
        /// ����Fp��ʾ
        /// </summary>
        private void SetFormat()
        {
            if (this.phaInManager.FpSheetView == null)
                return;

            this.phaInManager.FpSheetView.DefaultStyle.Locked = true;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 65F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].Visible = false;           //�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColDrugNO].Visible = false;           //ҩƷ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = true;           //����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Visible = false;      //��Ʊ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColGroupNO].Visible = false;           //����

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColBackQty].Locked = false;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = 150F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColBackQty].BackColor = System.Drawing.Color.SeaShell;
        }

        #region IPhaInManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                //{1DED4697-A590-47b3-B727-92A4AA05D2ED} ��������˿���� �޸�������ʾ���ҽṹΪ���½ṹ
                if (this.ucBackDrugSelectedList == null)
                {
                    this.ucBackDrugSelectedList = new ucDrugList();
                }

                return this.ucBackDrugSelectedList;
            }
        }

        public System.Data.DataTable InitDataTable()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            this.dt = new DataTable();

            this.dt.Columns.AddRange(
                                    new System.Data.DataColumn[] {
                                                                    new DataColumn("��Ʒ����",  dtStr),
                                                                    new DataColumn("���",      dtStr),
                                                                    new DataColumn("���ۼ�",    dtDec),
                                                                    new DataColumn("����",      dtStr),
                                                                    new DataColumn("��װ��λ",  dtStr),
                                                                    new DataColumn("�������",  dtDec),
                                                                    new DataColumn("�����",  dtDec),
                                                                    new DataColumn("�˿�����",  dtDec),
                                                                    new DataColumn("�˿���",  dtDec),
                                                                    new DataColumn("��Ʊ��",    dtStr),
                                                                    new DataColumn("��Ʊ����",  dtStr),
                                                                    new DataColumn("��ע",      dtStr),
                                                                    new DataColumn("ҩƷ����",  dtStr),
                                                                    new DataColumn("����",      dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr)
                                                                   }
                                  );

            DataColumn[] keys = new DataColumn[3];

            keys[0] = this.dt.Columns["ҩƷ����"];
            keys[1] = this.dt.Columns["����"];
            keys[2] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugNO = sv.Cells[activeRow, 0].Text;
            int groupNO = NConvert.ToInt32(sv.Cells[activeRow, 1].Value);

            return this.AddInData(drugNO, groupNO);
        }

        public int ShowApplyList()
        {
            return 1;
        }

        public int ShowInList()
        {
            try
            {
                if (this.ucListSelect == null)
                    this.ucListSelect = new ucPhaListSelect();

                this.ucListSelect.Init();
                this.ucListSelect.DeptInfo = this.phaInManager.DeptInfo;
                System.Collections.Hashtable hsState = new Hashtable();
                hsState.Add("0", "δ¼��Ʊ");
                hsState.Add("1", "��¼��Ʊδ��׼");
                hsState.Add("2", "�Ѻ�׼");
                this.ucListSelect.InOutStateCollection = hsState;

                this.ucListSelect.State = "2";                  //�����״̬
                System.Collections.Hashtable hs = new Hashtable();
                hs.Add(this.phaInManager.PrivType.ID, null);
                this.ucListSelect.MarkPrivType = hs;

                this.ucListSelect.Class2Priv = "0310";          //���

                this.ucListSelect.SelecctListEvent -= new ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);
                this.ucListSelect.SelecctListEvent += new ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg(ex.Message));
                return -1;
            }

            return 1;
        }

        public int ShowOutList()
        {
            return 1;
        }

        public int ShowStockList()
        {
            return 1;
        }

        public int ImportData()
        {
            return 1;
        }

        public bool Valid()
        {
            if (this.phaInManager.TargetDept.ID == "")
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ѡ���˿���ң�"));
                return false;
            }
            try
            {
                bool isHaveQty = false;
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (NConvert.ToDecimal(dr["�˿�����"]) > NConvert.ToDecimal(dr["�������"]))
                    {
                        System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + " �˿��������ڵ�ǰ����� �����˿�");
                        return false;
                    }
                    if (NConvert.ToDecimal(dr["�˿�����"]) > 0)
                    {
                        isHaveQty = true;
                    }
                }

                if (!isHaveQty)
                {
                    System.Windows.Forms.MessageBox.Show("�������˿�����");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex)
        {
            try
            {
                if (sv != null && delRowIndex >= 0)
                {
                    DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ������������?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    string[] keys = this.GetKey(sv, delRowIndex);
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        this.phaInManager.Fp.StopCellEditing();

                        this.hsInData.Remove(dr["ҩƷ����"].ToString() + dr["����"].ToString());
                        this.dt.Rows.Remove(dr);

                        this.phaInManager.Fp.StartCellEditing(null, false);
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

        public int Clear()
        {
            try
            {
                this.dt.Rows.Clear();

                this.dt.AcceptChanges();

                this.hsInData.Clear();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ����ղ�����������" + ex.Message));
                return -1;
            }

            return 1;
        }

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
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("���˷����쳣 " + ex.Message));
            }
            this.SetFormat();
        }

        public void SetFocusSelect()
        {
            if (this.phaInManager.FpSheetView != null)
            {
                if (this.phaInManager.FpSheetView.Rows.Count > 0)
                {
                    this.phaInManager.SetFpFocus();

                    this.phaInManager.FpSheetView.ActiveRowIndex = this.phaInManager.FpSheetView.Rows.Count - 1;
                    this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColBackQty;
                }
                else
                {
                    this.phaInManager.SetFocus();
                }
            }
        }

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

            DataTable dtAddMofity = this.dt.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
                return;

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            //��ȡ�˿ⵥ��
            // //{59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ������ⵥ�Ż�ȡ��ʽ
            string inListNO = phaIntegrate.GetInOutListNO(this.phaInManager.DeptInfo.ID, true);
            if (inListNO == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("��ȡ������ⵥ�ų���" + itemManager.Err));
                return;
            }

            //��־�Ƿ���ڱ������
            bool isSaveOperation = false;
            this.alPrintData = new ArrayList();
            foreach (DataRow dr in dtAddMofity.Rows)
            {
                decimal backQty = NConvert.ToDecimal(dr["�˿�����"]);
                if (backQty == 0)
                {
                    continue;
                }

                string key = this.GetKey(dr);
                //{DCE152D1-295C-4cc6-9EAA-39321A234569} 
                FS.HISFC.Models.Pharmacy.Input input = (this.hsInData[key] as FS.HISFC.Models.Pharmacy.Input).Clone();

                backQty = backQty * input.Item.PackQty;

                #region ��ȡ�����ε�ǰ��� �ж��Ƿ������˿�

                decimal storeQty = 0;
                this.itemManager.GetStorageNum(this.phaInManager.DeptInfo.ID, input.Item.ID, input.GroupNO, out storeQty);
                if (storeQty < backQty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(input.Item.Name + " ����������� �˿���������"),"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                #endregion

                #region �˿���Ϣ��ֵ

                input.InListNO = inListNO;                                      //�˿ⵥ��
                input.Quantity = - backQty;                                     //�������(����)
                input.RetailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                input.StoreQty = storeQty + input.Quantity;                     //����������
                input.StoreCost = input.StoreQty / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;

                input.Operation.ApplyOper.ID = this.phaInManager.OperInfo.ID;
                input.Operation.ApplyOper.OperTime = sysTime;
                input.Operation.Oper = input.Operation.ApplyOper;
                //���ݲ�ͬ��Ʊ������������Ʋ�������״̬
                input.State = "0";
                input.InvoiceNO = dr["��Ʊ��"].ToString();
                if (input.InvoiceNO != "")
                {
                    input.Operation.ExamQty = input.Quantity;
                    input.Operation.ExamOper = input.Operation.Oper;
                    input.State = "1";                                      //ֱ�Ӹ���״̬Ϊ ���(��Ʊ���)״̬
                }
                if (!this.IsNeedApprove)                                    //�����׼ ֱ������״̬"2"
                {
                    input.State = "2";
                    input.Operation.ExamQty = input.Quantity;
                    input.Operation.ExamOper = input.Operation.Oper;
                    input.Operation.ApproveOper = input.Operation.Oper;
                }

                #endregion

                #region �˿Ᵽ��

                int parm = this.itemManager.Input(input, "1", this.IsNeedApprove ? "0" : "1");
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���� [" + input.Item.Name + "] �������� " + this.itemManager.Err));
                    return;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���ݿ����ѱ���ˣ���ˢ�����ԣ�"));
                    return;
                }

                #endregion

                isSaveOperation = true;

                this.alPrintData.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isSaveOperation)
            {
                MessageBox.Show(Language.Msg("����˿�����ɹ�"));

                DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ��ӡ�˿ⵥ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    this.Print();
                }

            }

            this.Clear();
        }

        public int Print()
        {
            if (this.phaInManager.IInPrint != null)
            {
                this.phaInManager.IInPrint.SetData(this.alPrintData, this.phaInManager.PrivType.Memo);
                this.phaInManager.IInPrint.Print();
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Pharmacy.Input input)
        {
            return input.Item.ID + input.GroupNO.ToString() + input.BatchNO;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            return dr["ҩƷ����"].ToString() + dr["����"].ToString() + dr["����"].ToString();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private string[] GetKey(FarPoint.Win.Spread.SheetView sv, int rowIndex)
        {
            string[] keys = new string[]{
                                                sv.Cells[rowIndex, (int)ColumnSet.ColDrugNO].Text,
                                                sv.Cells[rowIndex, (int)ColumnSet.ColGroupNO].Text,
                                                sv.Cells[rowIndex,(int)ColumnSet.ColBatchNO].Text
                                            };

            return keys;
        }

        #endregion

        #region IPhaInManager ��Ա

        public int Dispose()
        {
            return 1;
        }

        #endregion

        private void ucListSelect_SelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            this.phaInManager.TargetDept = targetDept;

            this.Clear();

            this.AddInData(listCode, state);
        }

        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            //����˿���Ŀ�б�������Ϊ ���ҩƷ�б� �˴��Ͳ���Ҫ���ظ�ˢ��
            //this.ShowSelectData();
        }

        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.phaInManager.FpSheetView != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColBackQty)
                    {
                        this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                        return;
                    }
                    if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceNO)
                    {
                        if (this.phaInManager.FpSheetView.ActiveRowIndex == this.phaInManager.FpSheetView.Rows.Count - 1)
                        {
                            this.phaInManager.SetFocus();
                        }
                        if (this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Visible && !this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Locked)
                        {
                            this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceType;
                        }
                        else
                        {
                            this.phaInManager.FpSheetView.ActiveRowIndex++;
                            this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColBackQty;
                        }
                        return;
                    }
                }
            }
        }

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColBackQty)
            {
                DataRow dr = this.dt.Rows.Find(this.GetKey(this.phaInManager.FpSheetView, this.phaInManager.FpSheetView.ActiveRowIndex));
                if (dr != null)
                {
                    dr["�˿���"] = NConvert.ToDecimal(dr["�˿�����"]) * NConvert.ToDecimal(dr["���ۼ�"]);

                    dr.EndEdit();
                }
            }
        }

        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ����	0
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���		1
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ���ۼ�		2
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ����		3
            /// </summary>
            ColBatchNO,
            /// <summary>
            /// ��װ��λ	4
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// �������	5
            /// </summary>
            ColInNum,
            /// <summary>
            /// �����	6
            /// </summary>
            ColInCost,
            /// <summary>
            /// �˿�����
            /// </summary>
            ColBackQty,
            /// <summary>
            /// �˿���
            /// </summary>
            ColBackCost,
            /// <summary>
            /// ��Ʊ��		7
            /// </summary>
            ColInvoiceNO,
            /// <summary>
            /// ��Ʊ����		8
            /// </summary>
            ColInvoiceType,
            /// <summary>
            /// ��ע	    14
            /// </summary>
            ColMemo,
            /// <summary>
            /// ҩƷ����    15 
            /// </summary>
            ColDrugNO,
            /// <summary>
            /// ����
            /// </summary>
            ColGroupNO,
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
