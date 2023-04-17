using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Pharmacy.In
{
    /*
     *  ������  ��Ʊ�������� ��ƱĬ����һ��
     * 
     * 
    ***/
    public class InvoiceInPriv : IPhaInManager 
    {
        public InvoiceInPriv(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.SetPhaManagerProperty(ucPhaManager);
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
                this.phaInManager.IsShowItemSelectpanel = true;
                //����Ŀ�������Ϣ
                this.phaInManager.SetTargetDept(true, false, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);
                //�������������
                if (this.phaInManager.TargetDept.ID != "")
                {
                    this.ShowSelectData();
                }
                //��ʾ��Ϣ�������  
                this.phaInManager.ShowInfo = "";
                //���ù�������ť��ʾ
                this.phaInManager.SetToolBarButton(false, true, false, false, true);
                this.phaInManager.SetToolBarButtonVisible(false, true, false, false, true, true, false);
                //����Fp�����
                this.phaInManager.Fp.EditModeReplace = true;
                this.phaInManager.FpSheetView.DataAutoSizeColumns = false;

                this.phaInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);
                this.phaInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

                this.phaInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);
                this.phaInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

                this.phaInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
                this.phaInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);           
            }
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
                input.RetailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;

                this.dt.Rows.Add(new object[] { 
                                                input.Item.Name,                            //��Ʒ����
                                                input.Item.Specs,                           //���
                                                input.Item.PriceCollection.RetailPrice,     //���ۼ�
                                                input.BatchNO,                              //����
                                                input.Item.PackUnit,                        //��װ��λ
                                                input.Quantity / input.Item.PackQty,        //�������
                                                input.RetailCost,                           //�����                                                
                                                input.InvoiceNO,                            //��Ʊ��
                                                input.InvoiceType,                          //��Ʊ���
                                                input.Item.PriceCollection.PurchasePrice,   //�����
                                                input.PurchaseCost,                         //������
                                                input.Item.Product.Producer.Name,           //��������
                                                input.Operation.ApplyOper.ID,               //������
                                                input.Operation.ApplyOper.OperTime,         //��������
                                                input.Memo,                                 //��ע
                                                input.Item.ID,                              //ҩƷ����
                                                input.Item.NameCollection.SpellCode,        //ƴ����
                                                input.Item.NameCollection.WBCode,           //�����
                                                input.Item.NameCollection.UserCode,         //�Զ�����
                                                input.GroupNO.ToString()                            
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
        /// ������ʾ����
        /// </summary>
        /// <returns></returns>
        private int ShowSelectData()
        {
            string[] filterStr = new string[2] { "SPELL_CODE", "WB_CODE" };
            string[] label = new string[] { "ҩƷ����", "ҩƷ��Ʒ��", "���", "�������", "������λ", "�����ˮ��", "ƴ����", "�����", "ͨ����ƴ����", "ͨ���������" };
            int[] width = new int[] { 60, 120, 80, 60, 120, 60, 60, 60, 60, 60 };
            bool[] visible = new bool[] { false, true, true, true, true, false, false, false, false, false };
            string targetNO = this.phaInManager.TargetDept.ID;
            if (targetNO == "")
                targetNO = "AAAA";

            this.phaInManager.SetSelectData("3", false,new string[] { "Pharmacy.Item.GetPharmacyListForInput" }, filterStr, this.phaInManager.DeptInfo.ID,"0", targetNO);

            this.phaInManager.SetSelectFormat(label, width, visible);

            return 1;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="listNO">��ⵥ��</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        protected virtual int AddInData(string listNO,string state)
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
                this.AddDataToTable(input);

                this.hsInData.Add(this.GetKey(input), input);
            }

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

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
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchaseCost].Visible = false;     //������
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColDrugNO].Visible = false;           //ҩƷ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = false;          //����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyOper].Visible = false;        //������
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyDate].Visible = false;        //��������
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Visible = false;      //��Ʊ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColProduceName].Visible = false;      //��������
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColGroupNO].Visible = false;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Locked = false;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMem].Locked = false;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMem].Width = 150F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].BackColor = System.Drawing.Color.SeaShell;
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

            this.phaInManager.TotCostInfo = string.Format("�����ܽ��:{0} �����ܽ��:{1}", retailCost.ToString("N"), purchaseCost.ToString("N"));
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Pharmacy.Input input)
        {
            return input.Item.ID + input.BatchNO + input.GroupNO.ToString();
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private string[] GetKey(FarPoint.Win.Spread.SheetView sv,int rowIndex)
        {
            string[] keys = new string[]{
                                                sv.Cells[rowIndex, (int)ColumnSet.ColDrugNO].Text,
                                                sv.Cells[rowIndex, (int)ColumnSet.ColBatchNO].Text,
                                                sv.Cells[rowIndex,(int)ColumnSet.ColGroupNO].Text
                                            };
            return keys;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            return dr["ҩƷ����"].ToString() + dr["����"].ToString() + dr["����"].ToString();
        }

        #region IPhaInManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                return null;
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
                                                                    new DataColumn("��Ʊ��",    dtStr),                                                                
                                                                    new DataColumn("��Ʊ����",  dtStr),
                                                                    new DataColumn("�����",    dtDec),
                                                                    new DataColumn("������",  dtDec),
                                                                    new DataColumn("��������",  dtStr),
                                                                    new DataColumn("������",    dtStr),
                                                                    new DataColumn("��������",  dtDate),
                                                                    new DataColumn("��ע",      dtStr),
                                                                    new DataColumn("ҩƷ����",  dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr),
                                                                    new DataColumn("����",      dtStr)
                                                                   }
                                  );

            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;
         
            DataColumn[] keys = new DataColumn[3];

            keys[0] = this.dt.Columns["ҩƷ����"];
            keys[1] = this.dt.Columns["����"];
            keys[2] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string inNO = sv.Cells[activeRow, 5].Text;

            FS.HISFC.Models.Pharmacy.Input input = this.itemManager.GetInputInfoByID(inNO);
            if (input == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return -1;
            }
            if (this.hsInData.ContainsKey(this.GetKey(input)))
            {
                MessageBox.Show(Language.Msg("��ҩƷ�����"));
                return -1;
            }

            if (this.AddDataToTable(input) == 1)
            {
                this.hsInData.Add(this.GetKey(input), input);
            }

            this.SetFormat();

            this.SetFocusSelect();
     
            return 1;
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
                this.ucListSelect.State = "0";                  //�����״̬
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
            for (int i = 0; i < this.phaInManager.FpSheetView.Rows.Count; i++)
            {
                if (this.phaInManager.FpSheetView.Cells[i, (int)ColumnSet.ColInvoiceNO].Text == "")
                {									
                    MessageBox.Show(Language.Msg("������ " + this.phaInManager.FpSheetView.Cells[i,(int)ColumnSet.ColTradeName].Text + " ��Ʊ��"));
                    return false;
                }
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

                    DataRow dr = this.dt.Rows.Find(this.GetKey(sv,delRowIndex));
                    if (dr != null)
                    {
                        this.hsInData.Remove(this.GetKey(dr));

                        this.dt.Rows.Remove(dr);                        
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
                    this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
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

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                string key = this.GetKey(dr);

                FS.HISFC.Models.Pharmacy.Input input = this.hsInData[key] as FS.HISFC.Models.Pharmacy.Input;

                input.Operation.ExamOper.ID = this.phaInManager.OperInfo.ID;                //������
                input.Operation.ExamOper.OperTime = sysTime;                                //����ʱ��
                input.InvoiceNO = dr["��Ʊ��"].ToString().Trim();
                input.InvoiceType = dr["��Ʊ����"].ToString().Trim();
                input.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(dr["�����"]);
                input.PurchaseCost = NConvert.ToDecimal(dr["������"]);

                int parm = this.itemManager.ExamInput(input);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.itemManager.Err));
                    return;
                }
                if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���ݿ����ѱ���ˣ���ˢ������"));
                    return;
                }                
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ȷ�ϳɹ�"));

            //������ʾ
            this.Clear();
            this.ShowSelectData();
        }

        public int Print()
        {
            return 1;
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
            this.Clear();

            this.ShowSelectData();
        }

        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.phaInManager.FpSheetView != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceNO)
                    {
                        if (this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Visible && !this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Locked)
                        {
                            this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceType;
                        }
                        else
                        {
                            this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColPurchasePrice;
                        }
                        return;
                    }
                    if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceType)
                    {
                        this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColPurchasePrice;
                        return;
                    }
                    if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
                    {
                        if (this.phaInManager.FpSheetView.ActiveRowIndex == this.phaInManager.FpSheetView.Rows.Count - 1)
                        {
                            this.phaInManager.SetFocus();
                        }
                        else
                        {
                            this.phaInManager.FpSheetView.ActiveRowIndex++;
                            this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                        }
                        return;
                    }
                }
            }
        }

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
            {
                DataRow dr = this.dt.Rows.Find(this.GetKey(this.phaInManager.FpSheetView,this.phaInManager.FpSheetView.ActiveRowIndex));
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
            if (this.phaInManager.FpSheetView.ActiveRowIndex == (int)ColumnSet.ColPurchasePrice)
            {
                DataRow dr = this.dt.Rows.Find(this.GetKey(this.phaInManager.FpSheetView,this.phaInManager.FpSheetView.ActiveRowIndex));
                if (dr != null)
                {
                    dr["������"] = NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);

                    this.CompuateSum();
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
			/// ��Ʊ��		7
			/// </summary>
			ColInvoiceNO,
			/// <summary>
			/// �ڲ���		8
			/// </summary>
			ColInvoiceType,
			/// <summary>
			/// �����		9
			/// </summary>
			ColPurchasePrice,
            /// <summary>
            /// ������    10
            /// </summary>
            ColPurchaseCost,
			/// <summary>
			/// ��������	11
			/// </summary>
			ColProduceName,
			/// <summary>
			/// ������		12
			/// </summary>
			ColApplyOper,
			/// <summary>
			/// ��������	13
			/// </summary>
			ColApplyDate,
			/// <summary>
			/// ��ע	    14
			/// </summary>
			ColMem,
            /// <summary>
            /// ҩƷ����    15 
            /// </summary>
            ColDrugNO,
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
            ColGroupNO
        }
    }
}
