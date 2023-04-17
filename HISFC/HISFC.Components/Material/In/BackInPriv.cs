using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Material.In
{
    public class BackInPriv : IMatManager
    {
        #region ���췽��

        public BackInPriv(In.ucMatIn ucMatInManager)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.SetMatManagerProperty(ucMatInManager);
            }
        }

        #endregion

        #region �����

        private In.ucMatIn matInManager = null;

        private DataTable dt = null;

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �洢����ӵ�����
        /// </summary>
        private System.Collections.Hashtable hsInData = new Hashtable();
        /// <summary>
        /// ��������ҵ����{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private Material.ucMatListSelect ucListSelect = null;
        /// <summary>
        /// �����б�����ʾ������{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private int visibleColumns = 3;
        /// <summary>
        /// ����Ƿ���Ҫ��׼
        /// </summary>
        private bool IsNeedApprove = false;

        private List<FS.HISFC.Models.Material.Input> alInput = null;

        #endregion

        #region ����

        /// <summary>
        /// /��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            //��ȡ���Ʋ����ж��Ƿ���Ҫ��׼
            FS.HISFC.BizLogic.Manager.Controler ctrlManager = new FS.HISFC.BizLogic.Manager.Controler();
            string ctlApprove = ctrlManager.QueryControlerInfo("500002");
            //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            visibleColumns = controlIntegrate.GetControlParam<int>("MT0002", true);
            if (ctlApprove == "0")
                this.IsNeedApprove = false;
            else
                this.IsNeedApprove = true;
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="ucPhaManager"></param>
        protected void SetMatManagerProperty(In.ucMatIn ucPhaManager)
        {
            this.matInManager = ucPhaManager;

            if (this.matInManager != null)
            {
                //���ý�����ʾ
                this.matInManager.IsShowItemSelectpanel = false;
                //����Ŀ�������Ϣ
                this.matInManager.SetTargetDept(true, false, FS.HISFC.Models.IMA.EnumModuelType.Material, FS.HISFC.Models.Base.EnumDepartmentType.L);
                //�������������
                if (this.matInManager.TargetDept.ID != "")
                {
                    this.ShowSelectData();
                }
                //���ù�������ť��ʾ
                this.matInManager.SetToolBarButton(false, true, false, false, true);
                //{17B337D1-FE4C-4576-BB3C-7FFAD8C8D27C}����˿�ʱ��Ӧ����ʾ�ɹ���
                //this.matInManager.SetToolBarButtonVisible(false, true, false, true, true, true, false);
                this.matInManager.SetToolBarButtonVisible(false, true, false, false, true, true, false);
                //������Ŀ�б���{7019A2A6-ADCA-4984-944B-C4F1A312449A}
                this.matInManager.SetItemListWidth(visibleColumns);
                //����Fp
                this.matInManager.Fp.EditModePermanent = false;
                this.matInManager.Fp.EditModeReplace = true;
                this.matInManager.FpSheetView.DataAutoSizeColumns = false;

                this.matInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);
                this.matInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

                this.matInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);
                this.matInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

                this.matInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
                this.matInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);
            }
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <returns></returns>
        private int ShowSelectData()
        {
            string[] filterStr = new string[4] { "SPELL_CODE", "WB_CODE", "REGULAR_SPELL", "REGULAR_WB" };
            string[] label = new string[] { "��Ʒ����", "����", "��Ʒ����", "���", "�����", "ƴ����", "�����", "�Զ�����" };
            int[] width = new int[] { 60, 60, 120, 80, 60, 60, 60, 60, 60 };
            bool[] visible = new bool[] { false, false, true, true, true, true, false, false, false };
            //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            this.matInManager.DeptCode = this.matInManager.DeptInfo.ID;
            this.matInManager.SetSelectData("0", false, new string[] { "Material.Store.GetStockForBackIn" }, filterStr, this.matInManager.DeptInfo.ID);

            this.matInManager.SetSelectFormat(label, width, visible);

            return 1;
        }

        /// <summary>
        /// ��ʵ����Ϣ����DataTable��
        /// </summary>
        /// <param name="input">�����Ϣ</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Material.Input input)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                input.InCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.PurchasePrice;
                decimal storeQty = 0;
                this.storeManager.GetStoreQty(this.matInManager.DeptInfo.ID, input.StoreBase.Item.ID, input.StoreBase.StockNO, out storeQty);

                this.dt.Rows.Add(new object[] { 
												  input.StoreBase.Item.Name,                            //��Ʒ����
												  input.StoreBase.Item.Specs,                           //���
												  input.StoreBase.BatchNO,                              //����
												  input.StoreBase.PriceCollection.PurchasePrice,		//�����												  
												  input.StoreBase.Item.MinUnit,                         //��С��λ��������λ��
                                                  storeQty,						        //�������
												  input.InCost,											//�����   
												  0,													//�˿�����
												  0,													//�˿���
												  input.InvoiceNO,										//��Ʊ��
                                                  input.InvoiceTime,
												  input.Memo,											//��ע
												  input.StoreBase.Item.ID,								//��Ʒ
												  input.StoreBase.StockNO,                              //����
												  input.StoreBase.Item.SpellCode,						//ƴ����
												  input.StoreBase.Item.WBCode,							//�����
												  input.StoreBase.Item.UserCode							//�Զ�����                            					
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
        /// ����������
        /// </summary>
        /// <param name="listNO">��ⵥ��</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        protected virtual int AddInData(string listNO, string state)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ��ݵ��ݼ������� ���Ժ�...");
            Application.DoEvents();

            ArrayList alDetail = this.storeManager.QueryInputDetailByListNO(this.matInManager.DeptInfo.ID, listNO, this.matInManager.TargetDept.ID, state);
            if (alDetail == null)
            {
                Function.ShowMsg(this.storeManager.Err);
                return -1;
            }

            ((System.ComponentModel.ISupportInitialize)(this.matInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Material.Input input in alDetail)
            {
                //{6039DDA1-44F2-42d3-B7F7-544E69F5FE26} ��������Ϊ0��������Ŀ����ʾ
                if (input.StoreBase.Quantity - input.StoreBase.Returns == 0)
                {
                    continue;
                }
                //---------------
                input.StoreBase.PrivType = this.matInManager.PrivType.ID;             //�������
                input.StoreBase.SystemType = this.matInManager.PrivType.Memo;         //ϵͳ����

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInData.Add(this.GetKey(input), input);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }
            //{6039DDA1-44F2-42d3-B7F7-544E69F5FE26}��������Ϊ0��������Ŀ����ʾ
            if (this.matInManager.FpSheetView.RowCount == 0)
            {
                MessageBox.Show("���ŵ����Ѿ�����ȫ���˿⡣");
            }
            //---------------
            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.matInManager.Fp)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.SetFocusSelect();

            return 1;
        }

        /// <summary>
        /// ������Ʒ���������κż�������˿���Ϣ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="stockNO"></param>
        /// <returns></returns>
        protected virtual int AddInData(string itemCode, int stockNO)
        {
            if (this.hsInData.ContainsKey(itemCode + stockNO.ToString()))
            {
                MessageBox.Show("����Ʒ�����");
                return 0;
            }
            List<FS.HISFC.Models.Material.StoreDetail> alDetail = this.storeManager.QueryStoreList(this.matInManager.DeptInfo.ID, itemCode, stockNO.ToString(), true);
            if (alDetail == null || alDetail.Count == 0)
            {
                MessageBox.Show("δ��ȡ��Ч�Ŀ����ϸ��Ϣ" + this.storeManager.Err);
                return -1;
            }

            FS.HISFC.Models.Material.StoreDetail storeDetail = alDetail[0];

            FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

            input.StoreBase = storeDetail.StoreBase;									//��������Ϣ
            input.StoreBase.Quantity = storeDetail.StoreBase.StoreQty;                  //����� = �����

            input.Memo = storeDetail.Memo;

            if (this.AddDataToTable(input) == 1)
            {
                this.hsInData.Add(itemCode + stockNO.ToString(), input);
            }

            this.SetFormat();

            this.SetFocusSelect();

            return 1;
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Material.Input input)
        {
            return input.StoreBase.Item.ID + input.StoreBase.StockNO.ToString();
        }

        /// <summary>
        /// /��ȡ����ֵ 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            return dr["��Ʒ����"].ToString() + dr["����"].ToString();
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
                                                sv.Cells[rowIndex, (int)ColumnSet.ColItemCode].Text,
                                                sv.Cells[rowIndex, (int)ColumnSet.ColGroupNO].Text
                                            };

            return keys;
        }

        #endregion

        #region IPhaInManager ��Ա

        /// <summary>
        /// ��ϸ����ؼ�
        /// </summary>
        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                return null;
            }
        }


        /// <summary>
        /// ������Ʒ�˿�
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
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",      dtStr),
												 new DataColumn("����",      dtStr),
												 new DataColumn("�����",	 dtStr),
												 new DataColumn("��λ",		 dtStr),
												 new DataColumn("�������",  dtDec),
												 new DataColumn("�����",  dtDec),
												 new DataColumn("�˿�����",  dtDec),
												 new DataColumn("�˿���",  dtDec),
												 new DataColumn("��Ʊ��",    dtStr),
                                                 new DataColumn("��Ʊ����",  dtDate),
												 new DataColumn("��ע",      dtStr),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("����",      dtStr),
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr)
											 }
                );

            DataColumn[] keys = new DataColumn[2];

            keys[0] = this.dt.Columns["��Ʒ����"];
            keys[1] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }


        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugNO = sv.Cells[activeRow, 0].Text;
            int groupNO = NConvert.ToInt32(sv.Cells[activeRow, 1].Value);

            this.matInManager.AddNote((int)ColumnSet.ColItemCode, (int)ColumnSet.ColItemName);

            return this.AddInData(drugNO, groupNO);
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
            return 1;
        }


        public int ShowInList()
        {
            try
            {
                if (this.ucListSelect == null)
                {
                    this.ucListSelect = new ucMatListSelect();
                }
                //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
                this.ucListSelect.DeptInfo = this.matInManager.DeptInfo;
                this.ucListSelect.Init();
                System.Collections.Hashtable hsState = new Hashtable();
                hsState.Add("0", "δ¼��Ʊ");
                hsState.Add("1", "��¼��Ʊδ��׼");
                hsState.Add("2", "�Ѻ�׼");
                this.ucListSelect.InOutStateCollection = hsState;

                this.ucListSelect.State = "2";                  //�����״̬
                System.Collections.Hashtable hs = new Hashtable();
                hs.Add(this.matInManager.PrivType.ID, null);
                this.ucListSelect.MarkPrivType = hs;

                this.ucListSelect.Class2Priv = "0510";          //���

                this.ucListSelect.SelecctListEvent -= new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);
                this.ucListSelect.SelecctListEvent += new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
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


        /// <summary>
        /// ����Fp��ʾ
        /// </summary>
        public virtual void SetFormat()
        {
            if (this.matInManager.FpSheetView == null)
                return;

            this.matInManager.FpSheetView.DefaultStyle.Locked = true;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColItemName].Width = 120F;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Width = 65F;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColBackCost].CellType = numberCellType;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColStatUnit].Width = 60F;

            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].Visible = false;           //�����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColItemCode].Visible = false;           //��Ʒ����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = false;          //����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColGroupNO].Visible = false;          //����

            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColBackQty].Locked = false;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceDate].Locked = false;

            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = 150F;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColBackQty].BackColor = System.Drawing.Color.SeaShell;
            this.matInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceDate].BackColor = System.Drawing.Color.SeaShell;
        }

        public bool Valid()
        {
            if (this.matInManager.TargetDept.ID == "")
            {
                System.Windows.Forms.MessageBox.Show("��ѡ���˿���ң�");
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
                    DialogResult rs = MessageBox.Show("ȷ��ɾ������������?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    string[] keys = new string[]{
													sv.Cells[delRowIndex, (int)ColumnSet.ColItemCode].Text,
													sv.Cells[delRowIndex, (int)ColumnSet.ColGroupNO].Text
												};
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        this.matInManager.Fp.StopCellEditing();

                        this.hsInData.Remove(dr["��Ʒ����"].ToString() + dr["����"].ToString());
                        this.dt.Rows.Remove(dr);

                        this.matInManager.Fp.StartCellEditing(null, false);
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
                System.Windows.Forms.MessageBox.Show("ִ����ղ�����������" + ex.Message);
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
                System.Windows.Forms.MessageBox.Show("���˷����쳣 " + ex.Message);
            }
            this.SetFormat();
        }

        public void SetFocusSelect()
        {
            if (this.matInManager.FpSheetView != null)
            {
                if (this.matInManager.FpSheetView.Rows.Count > 0)
                {
                    this.matInManager.SetFpFocus();

                    this.matInManager.FpSheetView.ActiveRowIndex = this.matInManager.FpSheetView.Rows.Count - 1;
                    this.matInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColBackQty;
                }
                else
                {
                    this.matInManager.SetFocus();
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

            DataTable dtAddMofity = this.dt.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
                return;

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            #region ��ȡ�˿ⵥ��

            string inListNO = this.storeManager.GetInListNO(this.matInManager.DeptInfo.ID);

            if (inListNO == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��ȡ�����˿ⵥ�ų���" + storeManager.Err);
                return;
            }

            #endregion

            //��־�Ƿ���ڱ������
            bool isSaveOperation = false;
            this.alInput = new List<FS.HISFC.Models.Material.Input>();

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                decimal backQty = NConvert.ToDecimal(dr["�˿�����"]);
                if (backQty == 0)
                {
                    continue;
                }

                FS.HISFC.Models.Material.Input input = this.hsInData[this.GetKey(dr)] as FS.HISFC.Models.Material.Input;

                //��������������������һ��
                //				input.InCost = this.GetPrice(input.InCost);
                //������������������

                #region ��ȡ�����ε�ǰ��� �ж��Ƿ������˿�

                decimal storeQty = 0;
                this.storeManager.GetStoreQty(this.matInManager.DeptInfo.ID, input.StoreBase.Item.ID, input.StoreBase.StockNO, out storeQty);
                if (storeQty < backQty)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(input.StoreBase.Item.Name + " ����������� �˿���������");
                    return;
                }

                #endregion

                input.StoreBase.PrivType = this.matInManager.PrivType.ID;
                input.StoreBase.SystemType = this.matInManager.PrivType.Memo;
                input.StoreBase.Company.Name = this.matInManager.TargetDept.Name;

                #region �˿���Ϣ��ֵ

                input.InListNO = inListNO;													//�˿ⵥ��
                input.StoreBase.Quantity = -backQty;										//�������(����)
                input.PackInQty = input.StoreBase.Quantity * input.StoreBase.Item.PackQty;

                input.InCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.PurchasePrice;
                input.StoreBase.StoreQty = storeQty;										//���ǰ�������
                input.StoreBase.StoreCost = input.StoreBase.StoreQty * input.StoreBase.PriceCollection.PurchasePrice;

                input.StoreBase.Operation.ApplyOper.ID = this.matInManager.OperInfo.ID;
                input.StoreBase.Operation.ApplyOper.OperTime = sysTime;
                input.StoreBase.Operation.Oper = input.StoreBase.Operation.ApplyOper;
                //���ݲ�ͬ��Ʊ������������Ʋ�������״̬
                input.StoreBase.State = "0";
                input.InvoiceNO = dr["��Ʊ��"].ToString();
                input.InvoiceTime = FS.FrameWork.Function.NConvert.ToDateTime(dr["��Ʊ����"]);

                if (input.InvoiceNO != "")
                {
                    input.StoreBase.Operation.ExamQty = input.StoreBase.Quantity;
                    input.StoreBase.Operation.ExamOper = input.StoreBase.Operation.Oper;
                    input.StoreBase.State = "1";										//ֱ�Ӹ���״̬Ϊ ���(��Ʊ���)״̬
                }
                if (!this.IsNeedApprove)												//�����׼ ֱ������״̬"2"
                {
                    input.StoreBase.State = "2";
                    input.StoreBase.Operation.ExamQty = input.StoreBase.Quantity;
                    input.StoreBase.Operation.ExamOper = input.StoreBase.Operation.Oper;
                    input.StoreBase.Operation.ApproveOper = input.StoreBase.Operation.Oper;
                }

                #endregion

                #region �˿Ᵽ��

                int parm = this.storeManager.Input(input, "1", this.IsNeedApprove ? "0" : "1");
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���� [" + input.StoreBase.Item.Name + "] �������� " + this.storeManager.Err);
                    return;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ݿ����ѱ���ˣ���ˢ�����ԣ�");
                    return;
                }

                #endregion

                isSaveOperation = true;

                alInput.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isSaveOperation)
            {
                MessageBox.Show("����˿�����ɹ�");
            }

            if (alInput.Count > 0)
            {
                if (MessageBox.Show("�Ƿ��ӡ?", "��ʾ:", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    /*
                    Material.uc ucMat = new Material.ucMatInput();
                    ucMat.Decimals = 2;
                    ucMat.MaxRowNo = 17;

                    ucMat.SetDataForInput(alInput, 1, this.storeManager.Operator.ID, "1");
                     * */

                    //ucMat.SetDataForInput(alInput, 1, this.itemManager.Operator.ID, "1");
                    //{86B8ED47-06CF-4a8e-8768-2AE929E3E8E7}��ӡ
                    this.Print();
                }
            }

            this.Clear();
        }

        public void SaveCheck(bool IsHeaderCheck)
        {

        }

        public int Print()
        {
            if(matInManager.IInPrint !=null)
            {
                this.matInManager.IInPrint.SetData(this.alInput);
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
            this.matInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

            this.matInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

            this.matInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
        }

        #endregion

        #region ����

        private void ucListSelect_SelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            this.matInManager.TargetDept = targetDept;

            this.Clear();

            this.AddInData(listCode, state);
        }


        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            this.ShowSelectData();
        }


        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.matInManager.FpSheetView != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.matInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColBackQty)
                    {
                        this.matInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                        return;
                    }
                    if (this.matInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceNO)
                    {
                        this.matInManager.FpSheetView.ActiveRowIndex++;
                        this.matInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColBackQty;
                        return;
                    }
                }
            }
        }


        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.matInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColBackQty)
            {
                DataRow dr = this.dt.Rows.Find(this.GetKey(this.matInManager.FpSheetView, this.matInManager.FpSheetView.ActiveRowIndex));
                //string[] keys = new string[] { this.matInManager.FpSheetView.Cells[this.matInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemCode].Text, this.matInManager.FpSheetView.Cells[this.matInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColGroupNO].Text };
                //DataRow dr = this.dt.Rows.Find(keys);
                if (dr != null)
                {
                    dr["�˿���"] = NConvert.ToDecimal(dr["�˿�����"]) * NConvert.ToDecimal(dr["�����"]);

                    dr.EndEdit();
                }
            }
        }

        #endregion

        #region ��ö��

        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ����	
            /// </summary>
            ColItemName,
            /// <summary>
            /// ���		
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ����		
            /// </summary>
            ColBatchNO,
            /// <summary>
            /// �����
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// ������λ	
            /// </summary>
            ColStatUnit,
            /// <summary>
            /// �������	
            /// </summary>
            ColInNum,
            /// <summary>
            /// �����	
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
            /// ��Ʊ��		
            /// </summary>
            ColInvoiceNO,
            /// <summary>
            /// ��Ʊ����
            /// </summary>
            ColInvoiceDate,
            /// <summary>
            /// ��ע	    
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��Ʒ����     
            /// </summary>
            ColItemCode,
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
        /// <summary>
        /// ����˰ǰ��˰��۸�
        /// </summary>
        /// <param name="decPrice"></param>
        /// <returns></returns>
        //		private decimal GetPrice(decimal decPrice)
        //		{
        //			if(!this.uc.rbnPRe.Checked)
        //			{
        //				return decPrice;
        //			}
        //			else
        //			{
        //				return Math.Round(decPrice*1.17,2);   //1.17��˰��
        //			}
        //		}

        #endregion
    }
}
