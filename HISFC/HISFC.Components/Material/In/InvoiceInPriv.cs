using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using FS.FrameWork.Function;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Material.In
{
    public class InvoiceInPriv : IMatManager
    {
        #region ���췽��

        public InvoiceInPriv(Material.In.ucMatIn ucMatInManager)
        {
            this.SetMatManagerProperty(ucMatInManager);
        }

        #endregion

        #region �����

        /// <summary>
        /// ���ؼ�
        /// </summary>
        private ucMatIn ucInManager = null;

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucMatListSelect ucListSelect = null;

        /// <summary>
        /// �Ƿ񰴴��װ��ʽ���
        /// </summary>
        private bool isUsePackIn = false;

        /// <summary>
        /// �洢����ӵ�����
        /// </summary>
        private System.Collections.Hashtable hsInData = new Hashtable();

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();

        #endregion

        #region ����

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


        #endregion

        #region ����

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="ucPhaManager"></param>
        private void SetMatManagerProperty(Material.In.ucMatIn ucMatInManager)
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
                this.ucInManager.SetToolBarButtonVisible(false, true, false, false, true, false, false);
                //������ʾ�Ĵ�ѡ������
                this.ShowSelectData();
                //�����п��
                this.ucInManager.SetItemListWidth(3);
                //��Ϣ˵������
                this.ucInManager.ShowInfo = "F5 ��ת����Ŀѡ���";
                //����Fp����
                this.ucInManager.Fp.EditModePermanent = false;
                this.ucInManager.Fp.EditModeReplace = true;

                this.ucInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

                this.ucInManager.EndTargetChanged -= new In.ucMatIn.DataChangedHandler(value_EndTargetChanged);
                this.ucInManager.EndTargetChanged += new In.ucMatIn.DataChangedHandler(value_EndTargetChanged);

                this.ucInManager.Fp.EditModeOn += new EventHandler(Fp_EditModeOn);
                this.ucInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);
            }
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
                decimal inQty = 0;				//������� (���ݲ����԰�װ��λ����С��λ��ʾ)
                decimal inPrice = 0;			//��⹺��� ���ݲ���������װ�۸����С��λ�۸�
                string inUnit = "";			//��ⵥλ (���ݲ����԰�װ��λ����С��λ��ʾ)

                if (this.isUsePackIn)			//����װ��λ���
                {
                    inQty = input.PackInQty;	//��װ�������
                    inPrice = input.StoreBase.Item.PackPrice;							//��װ��λ�۸�
                    inUnit = input.StoreBase.Item.PackUnit;								//��װ��λ
                }
                else
                {
                    inQty = input.StoreBase.Quantity;									//��С�������
                    inPrice = input.StoreBase.PriceCollection.PurchasePrice;			//��С��λ�۸�
                    inUnit = input.StoreBase.Item.MinUnit;								//��С��λ
                }

                this.dt.Rows.Add(new object[] { 												 
												  input.StoreBase.Item.Name,                            //��Ʒ����
												  input.StoreBase.Item.Specs,                           //���												 												  
												  inUnit,												//��װ��λ
												  input.StoreBase.Item.PackQty,                         //��װ����
												  inQty, 												//�������
												  inPrice,												//�����
												  input.InCost,                                         //������ (����۽��)
												  input.StoreBase.BatchNO,                              //����
												  input.StoreBase.ValidTime,                            //��Ч��
												  input.InvoiceNO,										//��Ʊ��
												  input.InvoiceTime,									//��Ʊ����
												  input.StoreBase.PriceCollection.RetailPrice,			//���ۼ� ��С��λ���ۼ�
												  input.StoreBase.RetailCost,							//���۽��
												  input.StoreBase.Producer.Name,						//��������
												  input.StoreBase.Item.ID,                              //��Ŀ����
												  input.ID,												//��ˮ��
												  input.StoreBase.StockNO,								//������
												  input.StoreBase.Item.SpellCode,						//ƴ����
												  input.StoreBase.Item.WbCode,							//�����
												  input.StoreBase.Item.UserCode,						//�Զ�����													  
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
        /// ������ʾ����
        /// </summary>
        /// <returns></returns>
        private int ShowSelectData()
        {
            string[] filterStr = new string[2] { "SPELL_CODE", "WB_CODE" };
            string[] label = new string[] { "��Ʒ����", "��Ʒ����", "���", "�������", "������λ", "�����ˮ��", "ƴ����", "�����" };
            int[] width = new int[] { 60, 120, 80, 60, 120, 60, 60, 60 };
            bool[] visible = new bool[] { false, true, true, true, true, false, false, false };
            string targetNO = this.ucInManager.TargetDept.ID;
            if (targetNO == "")
                targetNO = "AAAA";

            this.ucInManager.SetSelectData("3", false, new string[] { "Material.Store.GetStoreListForInput" }, filterStr, this.ucInManager.DeptInfo.ID, "0", targetNO);

            this.ucInManager.SetSelectFormat(label, width, visible);

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

            ArrayList alDetail = new ArrayList();
            alDetail = this.itemManager.QueryInputDetailByListNO(this.ucInManager.DeptInfo.ID, listNO, "AAAA", "A");
            if (alDetail == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return -1;
            }

            ((System.ComponentModel.ISupportInitialize)(this.ucInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Material.Input input in alDetail)
            {
                this.AddDataToTable(input);
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //�Ѿ�����ˮ����������
                //this.hsInData.Add(input.StoreBase.Item.ID + input.ID, input);
                this.hsInData.Add(input.ID, input);
            }

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.ucInManager.Fp)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm(); ;

            this.SetFocusSelect();

            return 1;
        }


        /// <summary>
        /// ����Fp��ʾ
        /// </summary>
        public virtual void SetFormat()
        {
            if (this.ucInManager.FpSheetView == null)
                return;

            this.ucInManager.FpSheetView.DefaultStyle.Locked = true;
            this.ucInManager.FpSheetView.DataAutoSizeColumns = false;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Width = 65F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColUnit].Width = 60F;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numberCellType;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].Visible = true;           //�����			
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColItemID].Visible = false;           //��Ʒ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = false;          //����					
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProducerName].Visible = false;      //��������
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailCost].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPackQty].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColValidTime].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPackQty].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInBillNO].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColStockNO].Visible = false;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].Locked = false;



            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].BackColor = System.Drawing.Color.SeaShell;
        }


        /// <summary>
        /// �ܽ����ʾ����
        /// </summary>
        /// <param name="checkAll"></param>
        /// <param name="retailCost"></param>
        /// <param name="purchaseCost"></param>
        /// <param name="balanceCost"></param>
        protected void CompuateSum()
        {
            decimal retailCost = 0;
            decimal purchaseCost = 0;
            decimal balanceCost = 0;

            foreach (DataRow dr in this.dt.Rows)
            {
                retailCost += NConvert.ToDecimal(dr["�����"]);
                purchaseCost += NConvert.ToDecimal(dr["������"]);
            }

            balanceCost = retailCost - purchaseCost;

            this.ucInManager.TotCostInfo = string.Format("�����ܽ��:{0} �����ܽ��:{1}", retailCost.ToString("N"), purchaseCost.ToString("N"));
        }


        #endregion

        #region IMatManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                // TODO:  ��� InvoiceInPriv.InputModualUC getter ʵ��
                return null;
            }
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        /// <returns></returns>
        public DataTable InitDataTable()
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
												 new DataColumn("��λ",  dtStr),
												 new DataColumn("��װ����",	 dtDec),
												 new DataColumn("�������",  dtDec),
												 new DataColumn("�����",    dtDec),	
												 new DataColumn("������",  dtDec),
												 new DataColumn("����",      dtStr),
												 new DataColumn("��Ч��",	 dtDate),
												 new DataColumn("��Ʊ��",    dtStr),
												 new DataColumn("��Ʊ����",  dtDate),
												 new DataColumn("���ۼ�",	 dtDec),
												 new DataColumn("���۽��",  dtDec),
												 new DataColumn("��������",  dtStr),
												 new DataColumn("��Ŀ����",  dtStr),
												 new DataColumn("��ˮ��",	 dtStr),
												 new DataColumn("������",dtStr),
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr)
												 
											 }
                );

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dt.Columns["��ˮ��"];
            //			keys[0] = this.dt.Columns["������"];

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
            string inNO = sv.Cells[activeRow, 5].Text;
            //			string stockNO = sv.Cells[activeRow, 6].Text;

            ArrayList al = this.itemManager.QueryInputDetailByInNO(inNO, "0");

            FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

            foreach (FS.HISFC.Models.Material.Input info in al)
            {
                input = info;
            }

            if (input == null)
            {
                MessageBox.Show(this.itemManager.Err);
                return -1;
            }
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //�Ѿ�����ˮ����������
            if (this.hsInData.ContainsKey(input.ID))//input.StoreBase.Item.ID + input.ID
            {
                MessageBox.Show("����Ʒ�����");
                return -1;
            }

            if (this.AddDataToTable(input) == 1)
            {
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //�Ѿ�����ˮ����������
                this.hsInData.Add(input.ID, input);//input.StoreBase.Item.ID + input.ID
            }

            this.SetFormat();

            this.SetFocusSelect();

            return 1;
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

        /// <summary>
        /// ���뵥�б�
        /// </summary>
        /// <returns></returns>
        public int ShowApplyList()
        {
            return 1;
        }

        /// <summary>
        /// ��ⵥ�б�
        /// </summary>
        /// <returns></returns>
        public int ShowInList()
        {
            try
            {
                if (this.ucListSelect == null)
                    this.ucListSelect = new ucMatListSelect();

                this.ucListSelect.Init();
                this.ucListSelect.DeptInfo = this.ucInManager.DeptInfo;
                this.ucListSelect.State = "0";                  //�����״̬
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

        /// <summary>
        /// ���ⵥ�б�
        /// </summary>
        /// <returns></returns>
        public int ShowOutList()
        {
            // TODO:  ��� InvoiceInPriv.ShowOutList ʵ��
            return 1;
        }

        /// <summary>
        /// ��ʾ�����Ŀ�б�
        /// </summary>
        /// <returns></returns>
        public int ShowStockList()
        {
            // TODO:  ��� InvoiceInPriv.ShowStockList ʵ��
            return 1;
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            bool isHave = false;
            for (int i = 0; i < this.ucInManager.FpSheetView.Rows.Count; i++)
            {
                if (this.ucInManager.FpSheetView.Cells[i, (int)ColumnSet.ColInvoiceNO].Text == "")
                {
                    isHave = true;
                    break;
                }
            }
            if (isHave)
            {
                //this.ucInManager.FpSheetView.Cells[i,(int)ColumnSet.ColTradeName].Text
                if (MessageBox.Show("��������û�����뷢Ʊ��,�Ƿ������", "��ʾ:", System.Windows.Forms.MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
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
                    DialogResult rs = MessageBox.Show("ȷ��ɾ������������?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                    //�Ѿ�����ˮ����������
                    //string[] keys = new string[]{
                    //                                sv.Cells[delRowIndex, (int)ColumnSet.ColItemID].Text,
                    //                                sv.Cells[delRowIndex, (int)ColumnSet.ColBatchNO].Text
                    //                            };
                    string[] keys = new string[]{
                                                    sv.Cells[delRowIndex, (int)ColumnSet.ColInBillNO].Text
                                                };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                        //�Ѿ�����ˮ����������
                        //this.hsInData.Remove(dr["��Ʒ����"].ToString() + dr["����"].ToString());
                        this.hsInData.Remove(dr["��ˮ��"].ToString());
                        this.dt.Rows.Remove(dr);
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
            this.SetFormat();
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
                    this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                }
                else
                {
                    this.ucInManager.SetFocus();
                }
            }
        }

        // <summary>
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

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            ArrayList alInput = new ArrayList();

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                string key = dr["��ˮ��"].ToString() + dr["������"].ToString();

                //û�����뷢Ʊ�ŵĲ�����
                if (dr["��Ʊ��"] == null || dr["��Ʊ��"].ToString().Trim() == "")
                {
                    continue;
                }

                //				FS.HISFC.Models.Material.Input input = this.hsInData[key] as FS.HISFC.Models.Material.Input;
                FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

                input.StoreBase.Operation.ExamOper.ID = this.ucInManager.OperInfo.ID.ToString();                //������
                input.StoreBase.Operation.ExamOper.OperTime = sysTime;                                //����ʱ��
                input.InvoiceNO = dr["��Ʊ��"].ToString().Trim();
                input.InvoiceTime = NConvert.ToDateTime(dr["��Ʊ����"].ToString().Trim());
                input.ID = dr["��ˮ��"].ToString();
                input.StoreBase.StockNO = dr["������"].ToString();
                //				input.StoreBase.PriceCollection.PurchasePrice = NConvert.ToDecimal(dr["�����"]);
                //				input.StoreBase.PurchaseCost = NConvert.ToDecimal(dr["������"]);

                int parm = this.itemManager.ExamInput(input);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.itemManager.Err);
                    return;
                }
                if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ݿ����ѱ���ˣ���ˢ������");
                    return;
                }

                alInput.Add(input);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("��¼��Ʊ�ɹ�");

            //			if(alInput.Count > 0)
            //			{
            //				Local.GyHis.Material.ucMatInput ucMat = new Local.GyHis.Material.ucMatInput();
            //				ucMat.Decimals = 2;
            //				ucMat.MaxRowNo = 17;
            //
            //				ucMat.SetDataForInput(alInput,1,this.itemManager.Operator.ID,"1");
            //
            //			}

            //������ʾ
            this.Clear();
            this.ShowSelectData();

            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numCellType;
        }

        public void SaveCheck(bool IsHeaderCheck)
        {

        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            // TODO:  ��� InvoiceInPriv.Print ʵ��
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
            this.ucInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

            this.ucInManager.EndTargetChanged -= new In.ucMatIn.DataChangedHandler(value_EndTargetChanged);

            this.ucInManager.Fp.EditModeOn -= new EventHandler(Fp_EditModeOn);
            this.ucInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
        }

        #endregion

        #region �¼�

        private void ucListSelect_SelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            if (state == "2")
            {
                MessageBox.Show(Language.Msg("���ܶ��Ѻ�׼�������ٴν��з�Ʊ���"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ucInManager.TargetDept = targetDept;

            this.Clear();

            this.AddInData(listCode, state);
        }


        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            this.Clear();

            this.ShowSelectData();
        }


        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.ucInManager.FpSheetView != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceNO)
                    {
                        if (this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].Visible && !this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceTime].Locked)
                        {
                            this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceTime;
                        }
                        return;
                    }
                    if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceTime)
                    {
                        if (this.ucInManager.FpSheetView.ActiveRowIndex == this.ucInManager.FpSheetView.Rows.Count - 1)
                        {
                            this.ucInManager.SetFocus();
                        }
                        else
                        {
                            this.ucInManager.FpSheetView.ActiveRowIndex++;
                            this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                        }
                        return;
                    }
                }
            }
        }


        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
            {
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //�Ѿ�����ˮ����������
                //string[] keys = new string[] { this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColBatchNO].Text, this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemID].Text };
                string[] keys = new string[] { this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColInBillNO].Text };
                DataRow dr = this.dt.Rows.Find(keys);
                if (dr != null)
                {
                    dr["������"] = NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);

                    dr.EndEdit();

                    this.CompuateSum();
                }
            }
        }


        private void Fp_EditModeOn(object sender, EventArgs e)
        {
            if (this.ucInManager.FpSheetView.ActiveRowIndex == (int)ColumnSet.ColPurchasePrice)
            {
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                //�Ѿ�����ˮ����������
                //string[] keys = new string[] { this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColBatchNO].Text, this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemID].Text };
                string[] keys = new string[] { this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColInBillNO].Text };
                DataRow dr = this.dt.Rows.Find(keys);
                if (dr != null)
                {
                    dr["������"] = NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);

                    this.CompuateSum();
                }
            }
        }


        #endregion

        #region ��ö��

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ��װ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// �����������װ��λ������
            /// </summary>
            ColInQty,
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
            /// <summary>
            /// ��������
            /// </summary>
            ColProducerName,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ColItemID,
            /// <summary>
            /// ��ˮ��
            /// </summary>
            ColInBillNO,
            /// <summary>
            /// ������
            /// </summary>
            ColStockNO,
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

        #endregion
    }
}
