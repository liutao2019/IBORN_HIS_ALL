using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Neusoft.NFC.Management;
using Neusoft.NFC.Function;
using System.Windows.Forms;

namespace Neusoft.UFC.Material.Apply
{
    /// <summary>
    /// [��������: �������ҵ����]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public class InApplyPriv :IMatManager
    {
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="isBakcIn"></param>
        /// <param name="ucMatApplyManager">�������FALSE , �˿����TRUE </param>
        public InApplyPriv(bool isBakcIn, Apply.ucApply ucMatApplyManager)
        {
            this.isBack = isBakcIn;

            this.listNO = "";

            this.SetMaterialProperty(ucMatApplyManager);

        }


        #region �����

        /// <summary>
        /// �Ƿ��˿�����
        /// </summary>
        private bool isBack = false;

        /// <summary>
        /// �Ƿ���С��λ
        /// </summary>
        private bool isMinUnit = true;

        /// <summary>
        /// ���������
        /// </summary>
        private Apply.ucApply MatApplyManager;

        /// <summary>
        /// SheetView����
        /// </summary>
        private FarPoint.Win.Spread.SheetView svTemp = null;

        /// <summary>
        /// DataTable����
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// Hashtable���� �洢����ӵ���������
        /// </summary>
        private System.Collections.Hashtable hsApplyData = new System.Collections.Hashtable();

        /// <summary>
        /// ���ε��������
        /// </summary>
        private string listNO = "";

        /// <summary>
        /// ���������
        /// </summary>
        private int serialNO = 0;

        public string showInfo = "";

        /// <summary>
        /// ��������
        /// </summary>
        private Neusoft.HISFC.Management.Material.Store storeManager = new Neusoft.HISFC.Management.Material.Store();

        /// <summary>
        /// �ֵ������
        /// </summary>
        private Neusoft.HISFC.Management.Material.MetItem itemManager = new Neusoft.HISFC.Management.Material.MetItem();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private Neusoft.HISFC.Management.Manager.Controler myControler = new Neusoft.HISFC.Management.Manager.Controler();

        private Neusoft.HISFC.Management.Manager.Department myDept = new Neusoft.HISFC.Management.Manager.Department();

        private Neusoft.HISFC.Management.Manager.Person myPerson = new Neusoft.HISFC.Management.Manager.Person();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���С��λ����
        /// </summary>
        public bool IsMinUnit
        {
            get
            {
                return this.isMinUnit;
            }
            set
            {
                this.isMinUnit = value;
            }
        }
        #endregion

        #region ����

        private void SetMaterialProperty(Apply.ucApply ucMatApplyManager)
        {
            this.MatApplyManager = ucMatApplyManager;

            if (this.MatApplyManager != null)
            {
                //���ý�����ʾ
                this.MatApplyManager.IsShowItemSelectpanel = true;
                this.MatApplyManager.IsShowInputPanel = false;

                //�����������������Ŀ����Һ���Ŀѡ���б�
                if (this.MatApplyManager.IOType == "1")
                {
                    //�����ڲ����뻹���ⲿ��������Ŀ����Һ���Ŀ�б�
                    if (this.MatApplyManager.DeptInfo.Memo == "L")
                    {
                        //�����ⲿ����Ŀ�������Ϣ
                        this.MatApplyManager.SetTargetDept(true, false, Neusoft.HISFC.Object.IMA.EnumModuelType.Material,Neusoft.HISFC.Object.Base.EnumDepartmentType.L);
                        //�����ⲿ������ʾ�Ĵ�ѡ������
                        this.MatApplyManager.SetSelectData("0", false, null, null, null);
                    }
                    else
                    {
                        //�����ڲ�����Ŀ�������Ϣ
                        this.MatApplyManager.SetTargetDept(false, true, Neusoft.HISFC.Object.IMA.EnumModuelType.Material, Neusoft.HISFC.Object.Base.EnumDepartmentType.L);
                        //�����ڲ�������ʾ�Ĵ�ѡ������
                        this.MatApplyManager.SetSelectData("1", false, null, null, null);
                    }

                }
                else
                {
                    //���ó�������Ŀ�������Ϣ
                    this.MatApplyManager.SetTargetDept(false, true, Neusoft.HISFC.Object.IMA.EnumModuelType.Material, Neusoft.HISFC.Object.Base.EnumDepartmentType.L);
                    //���ó���������ʾ�Ĵ�ѡ������
                    this.MatApplyManager.SetSelectData("2", false, null, null, null);
                }

                this.MatApplyManager.SetItemListWidth(2);
                //���ù�������ť��ʾ
                this.MatApplyManager.SetToolButton(true, false, false, true, false);
                this.MatApplyManager.SetToolBarButtonVisible(true, false, false, false, true, true, false);
                //������Ϣ��ʾ
                this.MatApplyManager.ShowInfo = "";
                //Fp ����
                this.MatApplyManager.FpSheetView.DataAutoSizeColumns = true;
                this.MatApplyManager.Fp.EditModeReplace = true;

                this.MatApplyManager.EndTargetChanged -= new Apply.ucApply.DataChangedHandler(value_EndTargetChanged);
                this.MatApplyManager.EndTargetChanged += new Apply.ucApply.DataChangedHandler(value_EndTargetChanged);

                this.MatApplyManager.FpKeyEvent -= new Apply.ucApply.FpKeyHandler(value_FpKeyEvent);
                this.MatApplyManager.FpKeyEvent += new Apply.ucApply.FpKeyHandler(value_FpKeyEvent);

                this.MatApplyManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
                this.MatApplyManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);

            }
        }

        /// <summary>
        /// ��DataTable����������
        /// </summary>
        /// <param name="apply">������Ϣ</param>
        /// <param name="dataSource">������Դ 0 ԭʼ���� 1 �������</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(Neusoft.HISFC.Object.Material.Apply apply, string dataSource)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                decimal price = 0;
                decimal qty = 0;
                decimal cost = 0;
                string unit = "";

                if (this.isMinUnit)
                {
                    qty = apply.Operation.ApplyQty;
                    cost = apply.Operation.ApplyQty * apply.Item.UnitPrice;
                    unit = apply.Item.MinUnit;
                    price = apply.Item.UnitPrice;
                }
                else
                {
                    qty = apply.Operation.ApplyQty / apply.Item.PackQty;
                    cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.Item.PackPrice;
                    unit = apply.Item.PackUnit;
                    price = apply.Item.PackPrice;
                }
                this.dt.Rows.Add(new object[] { 
												  apply.Item.Name,									//��Ʒ����
												  apply.Item.Specs,									//���
												  price,											//���ۼ�
												  unit,												//��װ��λ
												  qty,												//��������
												  cost,												//������
												  apply.OutQty,
												  apply.OutCost,
												  apply.Memo,										//��ע
												  apply.Item.ID,									//��Ʒ����
												  apply.SerialNO,									//���
												  dataSource,
												  apply.Item.SpellCode,							//ƴ����
												  apply.Item.WBCode,								//�����
												  apply.Item.UserCode								//�Զ�����                            
											  }
                    );

                this.dt.DefaultView.AllowDelete = true;
                this.dt.DefaultView.AllowEdit = true;
                this.dt.DefaultView.AllowNew = true;
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
        /// �������뵥��Ϣ��DataTable����������
        /// </summary>
        /// <param name="apply">������Ϣ</param>
        /// <param name="dataSource">������Դ 0 ԭʼ���� 1 �������</param>
        /// <returns></returns>
        protected virtual int AddApplyToTable(Neusoft.HISFC.Object.Material.Apply apply, string dataSource)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                Neusoft.HISFC.Management.Material.MetItem managerItem = new Neusoft.HISFC.Management.Material.MetItem();
                apply.Item = managerItem.GetMetItemByMetID(apply.Item.ID);

                decimal price = 0;
                decimal qty = 0;
                decimal cost = 0;
                string unit = "";

                if (this.isMinUnit)
                {
                    qty = apply.Operation.ApplyQty;
                    cost = apply.Operation.ApplyQty * apply.Item.UnitPrice;
                    unit = apply.Item.MinUnit;
                    price = apply.Item.UnitPrice;
                }
                else
                {
                    qty = apply.Operation.ApplyQty / apply.Item.PackQty;
                    cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.Item.PackPrice;
                    unit = apply.Item.PackUnit;
                    price = apply.Item.PackPrice;
                }
                this.dt.Rows.Add(new object[] { 
												  apply.Item.Name,                                //��Ʒ����
												  apply.Item.Specs,                               //���
												  price,					                      //���ۼ�
												  unit,											  //��װ��λ
												  qty,											  //��������
												  cost,                                           //������
												  apply.OutQty,
												  apply.OutCost,
												  apply.Memo,                                     //��ע
												  apply.Item.ID,                                  //��Ʒ����
												  apply.SerialNO,                                 //���
												  dataSource,
												  apply.Item.SpellCode,                          //ƴ����
												  apply.Item.WBCode,                             //�����
												  apply.Item.UserCode                            //�Զ�����                            
											  }
                    );

                this.dt.DefaultView.AllowDelete = true;
                this.dt.DefaultView.AllowEdit = true;
                this.dt.DefaultView.AllowNew = true;
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
        /// �������뵥��������������
        /// </summary>
        /// <param name="listNO"></param>
        /// <returns></returns>
        private int AddApplyData(string listNO)
        {
            ArrayList alDetail = this.storeManager.QueryApplyDetailByListNO(this.MatApplyManager.DeptInfo.ID, listNO, "0");
            if (alDetail == null)
            {
                System.Windows.Forms.MessageBox.Show("δ��ȷ��ȡ�ڲ����������Ϣ" + this.storeManager.Err);
                return -1;
            }

            this.Clear();

            ((System.ComponentModel.ISupportInitialize)(this.MatApplyManager.Fp)).BeginInit();

            int i = 0;

            foreach (Neusoft.HISFC.Object.Material.Apply info in alDetail)
            {
                if (this.AddApplyToTable(info, "0") == -1)
                    return -1;

                this.listNO = info.ApplyListNO;

                if (i < info.SerialNO)
                {
                    i = info.SerialNO;
                }

                this.hsApplyData.Add(info.Item.ID, info);
            }

            this.listNO = listNO;

            this.serialNO = i;

            this.dt.AcceptChanges();

            this.CompuateSum();

            ((System.ComponentModel.ISupportInitialize)(this.MatApplyManager.Fp)).EndInit();

            return 1;
        }

        /// <summary>
        /// �������뵥��������������
        /// </summary>
        /// <param name="listNO"></param>
        /// <returns></returns>
        private int AddApplyDataSelf(string listNO)
        {
            ArrayList alDetail = this.storeManager.QueryApplyDetailByListNOSelf(this.MatApplyManager.DeptInfo.ID, listNO, "MU");
            if (alDetail == null)
            {
                System.Windows.Forms.MessageBox.Show("δ��ȷ��ȡ�ڲ����������Ϣ" + this.storeManager.Err);
                return -1;
            }

            this.Clear();

            ((System.ComponentModel.ISupportInitialize)(this.MatApplyManager.Fp)).BeginInit();

            int i = 0;

            foreach (Neusoft.HISFC.Object.Material.Apply info in alDetail)
            {
                if (this.AddApplyToTable(info, "0") == -1)
                    return -1;

                this.listNO = info.ApplyListNO;

                if (i < info.SerialNO)
                {
                    i = info.SerialNO;
                }

                if (info == alDetail[0])
                {
                    Neusoft.HISFC.Object.Base.Employee person = myPerson.GetPersonByID(info.Operation.ApproveOper.ID);

                    Neusoft.HISFC.Object.Base.Department dept = myDept.GetDeptmentById(info.StockDept.ID);

                    if (person != null && dept != null)
                    {
                        this.showInfo = "���뵥:" + info.ApplyListNO + " �������:" + dept.Name + " ��������:" + person.Name;
                    }
                }
                this.hsApplyData.Add(info.Item.ID, info);
            }

            this.listNO = listNO;

            this.serialNO = i;

            this.dt.AcceptChanges();

            this.CompuateSum();

            ((System.ComponentModel.ISupportInitialize)(this.MatApplyManager.Fp)).EndInit();

            return 1;
        }

        /// <summary>
        /// ������Ʒ������� ������������
        /// </summary>
        /// <param name="apply">��Ʒʵ��</param>
        /// <returns></returns>
        private int AddDrugData(Neusoft.HISFC.Object.Material.Apply apply)
        {
            if (this.hsApplyData.Contains(apply.Item.ID))
            {
                System.Windows.Forms.MessageBox.Show("����Ʒ�����");
                return 0;
            }

            apply.StockDept = this.MatApplyManager.DeptInfo;         //������ (Ŀ�����)
            apply.TargetDept = this.MatApplyManager.TargetDept;      //Ŀ�����
            apply.State = "0";                                       //״̬ ����
            apply.SystemType = this.MatApplyManager.PrivType.Memo;
            apply.PrivType = this.MatApplyManager.PrivType.ID;

            if (this.AddDataToTable(apply, "1") == 1)
            {
                this.hsApplyData.Add(apply.Item.ID, apply);
                this.SetFocusSelect();
            }

            return 1;
        }

        /// <summary>
        /// ������Ʒ������� �����������
        /// </summary>
        /// <param name="drugNO">��Ʒ����</param>
        /// <param name="outBillNO">�������</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO, string outBillNO)
        {

            Neusoft.HISFC.Object.Material.MaterialItem item = itemManager.GetMetItemByMetID(drugNO);

            if (item == null)
            {
                System.Windows.Forms.MessageBox.Show("������Ʒ������Ϣʧ��");
                return -1;
            }

            Neusoft.HISFC.Object.Material.Apply apply = new Neusoft.HISFC.Object.Material.Apply();

            apply.Item = item;

            if (this.hsApplyData.Contains(apply.Item.ID))
            {
                System.Windows.Forms.MessageBox.Show("����Ʒ�����");
                return 0;
            }

            if (outBillNO != null)
            {
                apply.ApplyListNO = outBillNO;
            }

            apply.StockDept = this.MatApplyManager.DeptInfo;       //������ (Ŀ�����)
            apply.TargetDept = this.MatApplyManager.TargetDept;      //�������
            apply.State = "0";                                   //״̬ ����
            apply.SystemType = this.MatApplyManager.PrivType.Memo;
            apply.PrivType = this.MatApplyManager.PrivType.ID;

            if (this.AddDataToTable(apply, "1") == 1)
            {
                this.hsApplyData.Add(apply.Item.ID, apply);

                //				this.SetFormat();

                this.SetFocusSelect();

            }

            return 1;
        }

        /// <summary>
        /// ������Ʒ��������˿�����
        /// </summary>
        /// <param name="drugNO">��Ʒ����</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO)
        {
            return this.AddDrugData(drugNO, null);
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <returns></returns>
        private int ShowSelectData()
        {
            if (this.isBack)
            {
                string[] filterStr = new string[3] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" };
                string[] label = new string[] { "�������", "���ⵥ�ݺ�", "��Ʒ����", "��Ʒ����", "���", "����", "��װ��λ", "��С��λ", "ƴ����", "�����" };
                int[] width = new int[] { 60, 60, 60, 120, 80, 60, 60, 60, 60, 60 };
                bool[] visible = new bool[] { false, false, false, true, true, true, false, true, false, false };

                this.MatApplyManager.SetSelectData("3", false, new string[] { "Material.Store.GetOutputInfoForInput" }, filterStr, this.MatApplyManager.DeptInfo.ID, "A", "2", this.MatApplyManager.TargetDept.ID);

                this.MatApplyManager.SetSelectFormat(label, width, visible);
            }
            else
            {
                if (this.MatApplyManager.IOType == "1" && this.MatApplyManager.DeptInfo.Memo != "L")
                {
                    this.MatApplyManager.SetSelectData("1", false, null, null, null);
                }

            }

            this.MatApplyManager.SetItemListWidth(2);

            return 1;
        }

        /// <summary>
        /// ��ʽ��Fp��ʾ
        /// </summary>
        public virtual void SetFormat()
        {
            if (this.MatApplyManager.FpSheetView == null)
                return;

            this.MatApplyManager.FpSheetView.DefaultStyle.Locked = true;

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColItemName].Width = 150F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 100F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 80F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColPackUnit].Width = 80F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].Width = 80F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].Width = 100F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColOutQty].Width = 100F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColOutCost].Width = 100F;

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColItemID].Visible = false;           //��Ʒ����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColNO].Visible = false;               //���
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColDataSource].Visible = false;       //������Դ
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = 200F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].Locked = false;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColOutQty].Locked = false;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].BackColor = System.Drawing.Color.SeaShell;
        }

        ///<summary>
        ///������Ʒ�����߼�������
        ///</summary>
        ///<param name="alterFlag">���ɷ�ʽ 0 ������ 1 ������</param>
        ///<param name="deptCode">�ⷿ����</param>
        ///<returns>�ɹ�����0��ʧ�ܷ��أ�1</returns>
        public void FindByAlter(string alterFlag, string deptCode)
        {
            //			if (this.hsApplyData.Count > 0)
            //			{
            //				DialogResult result;
            //				result = MessageBox.Show("�����������ɽ������ǰ���ݣ��Ƿ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            //					MessageBoxOptions.RightAlign);
            //				if (result == DialogResult.No)
            //					return;
            //			}
            //
            //			try
            //			{
            //				this.Clear();
            //
            //				ArrayList alDetail = new ArrayList();
            //				if (alterFlag == "1")
            //				{
            //					#region �������� ���������Ĳ��� ������������Ϣ
            //					using (UFC.Pharmacy.ucPhaAlter uc = new ucPhaAlter())
            //					{
            //						uc.DeptCode = deptCode;
            //						uc.SetData();
            //						uc.Focus();
            //						Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);
            //
            //						if (uc.ApplyInfo != null)
            //						{
            //							alDetail = uc.ApplyInfo;
            //						}
            //					}
            //					#endregion
            //				}
            //				else
            //				{
            //					alDetail = this.storeManager.FindByAlter("0", deptCode, System.DateTime.MinValue, System.DateTime.MaxValue);
            //					if (alDetail == null)
            //					{
            //						MessageBox.Show(Language.Msg("��������������ִ����Ϣ����δ��ȷִ��\n" + this.storeManager.Err));
            //						return;
            //					}
            //				}
            //
            //				Neusoft.HISFC.Object.Pharmacy.Item item = new Neusoft.HISFC.Object.Pharmacy.Item();
            //				foreach (Neusoft.NFC.Object.NeuObject temp in alDetail)
            //				{
            //					//this.AddDrugInfo(temp.ID, "", NConvert.ToDecimal(temp.User03));
            //					this.AddDrugData(temp.ID);
            //				}
            //
            //			}
            //			catch (Exception ex)
            //			{
            //				MessageBox.Show(Language.Msg(ex.Message));
            //			}
        }

        /// <summary>
        /// ���ر��ŵ��ݲ��
        /// </summary>
        public virtual void CompuateSum()
        {
            decimal retailCost = 0;

            if (this.dt != null)
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    retailCost += Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"]) * Neusoft.NFC.Function.NConvert.ToDecimal(dr["����"]);
                }

                this.MatApplyManager.TotCostInfo = string.Format("�����ܽ��:{0} ", retailCost.ToString("N"));
            }
        }

        #endregion

        #region IMatManager ��Ա

        public Neusoft.NFC.Interface.Controls.ucBaseControl InputModualUC
        {
            get { return null; }
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
												 new DataColumn("����",    dtDec),
												 new DataColumn("��λ",  dtStr),
												 new DataColumn("��������",  dtDec),
												 new DataColumn("������",  dtDec),
												 new DataColumn("��������",dtDec),//-liuxq add
												 new DataColumn("���Ž��",dtDec),//-liuxq add
												 new DataColumn("��ע",      dtStr),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",    dtStr),
												 new DataColumn("������Դ",  dtStr),
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr)
												 
											 }
                );

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dt.Columns["��Ʒ����"];

            this.SetFormat();

            this.dt.PrimaryKey = keys;

            this.dt.DefaultView.AllowDelete = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowNew = true;

            return this.dt;
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugID = "";

            if (this.MatApplyManager.IOType == "0")
            {
                drugID = sv.Cells[activeRow, 0].Value.ToString();
                //ȡ��Ʒ�ֵ���Ϣ							
                Neusoft.HISFC.Object.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(drugID);
                //�������뵥ʵ��
                Neusoft.HISFC.Object.Material.Apply apply = new Neusoft.HISFC.Object.Material.Apply();

                apply.Item = item;
                apply.ApplyPrice = NConvert.ToDecimal(sv.Cells[activeRow, 4].Value.ToString());

                return this.AddDrugData(apply);
            }
            else
            {
                if (isBack)
                {
                    drugID = sv.Cells[activeRow, 0].Value.ToString();

                    string outbillNO = sv.Cells[activeRow, 0].Value.ToString();
                    return this.AddDrugData(drugID, outbillNO);
                }
                else
                {
                    drugID = sv.Cells[activeRow, 0].Value.ToString();
                    return this.AddDrugData(drugID);
                }
            }
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, Neusoft.HISFC.Object.Material.Input input)
        {
            string drugID = "";

            if (this.MatApplyManager.IOType == "0")
            {
                drugID = input.StoreBase.Item.ID;
                //ȡ��Ʒ�ֵ���Ϣ							
                Neusoft.HISFC.Object.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(drugID);
                //�������뵥ʵ��
                Neusoft.HISFC.Object.Material.Apply apply = new Neusoft.HISFC.Object.Material.Apply();

                apply.Item = item;
                apply.ApplyPrice = input.StoreBase.PriceCollection.PurchasePrice;

                return this.AddDrugData(apply);
            }

            return 1;
        }

        public int ShowApplyList()
        {
            string class2Type = "";

            if (this.MatApplyManager.IOType == "1")
            {
                class2Type = "0510";
            }
            else
            {
                class2Type = "0520";
            }

            //��ȡ������Ϣ
            ArrayList alTemp = new ArrayList();

         
                if (this.MatApplyManager.DeptInfo.Memo == "L")
                {
                    alTemp = this.storeManager.QueryApplySimple(this.MatApplyManager.DeptInfo.ID, class2Type, "0", "12");
                }
                else
                {
                    alTemp = this.storeManager.QueryApplySimple(this.MatApplyManager.DeptInfo.ID, class2Type, "0", "13");
                }
            if (alTemp == null)
            {
                System.Windows.Forms.MessageBox.Show("��ȡ������Ϣʧ��" + this.storeManager.Err);
                return -1;
            }
            //			ArrayList alList = new ArrayList();
            //���ݵ�ǰѡ��Ĺ�����λ����
            //			if (this.MatApplyManager.TargetDept.ID != "")
            //			{
            //				foreach (Neusoft.NFC.Object.NeuObject info in alTemp)
            //				{
            //					if (info.Memo != this.MatApplyManager.TargetDept.ID)
            //						continue;
            //					alList.Add(info);
            //				}
            //			}
            //			else
            //			{
            //				alList = alTemp;
            //			}
            //			
            //			//��������ѡ�񵥾�			
            //			Neusoft.NFC.Object.NeuObject selectObj = new Neusoft.NFC.Object.NeuObject();
            //			string[] fpLabel = { "���뵥��", "������λ" };
            //			float[] fpWidth = { 120F, 120F };
            //			bool[] fpVisible = { true, true, false, false, false, false };

            Neusoft.NFC.Object.NeuObject selectObject = new Neusoft.NFC.Object.NeuObject();

            if (Neusoft.NFC.Interface.Classes.Function.ChooseItem(alTemp, ref selectObject) == 1)
            {
                this.Clear();

                Neusoft.NFC.Object.NeuObject targeDept = new Neusoft.NFC.Object.NeuObject();

                //				targeDept.ID = selectObj.Memo;              //������˾����
                //				targeDept.Name = selectObj.Name;            //������˾����
                //				targeDept.Memo = "0";                       //Ŀ�굥λ���� �ڲ�����
                //
                //				if (this.MatApplyManager.TargetDept.ID != targeDept.ID)
                //				{
                //					this.MatApplyManager.TargetDept = targeDept;
                //					this.ShowSelectData();
                //				}

                    this.AddApplyData(selectObject.ID);

                this.SetFocusSelect();

                if (this.svTemp != null)
                    this.svTemp.ActiveRowIndex = 0;
            }

            return 1;
        }

        public int ShowInList()
        {
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

        public bool Valid()
        {
            if (this.MatApplyManager.TargetDept.ID == "")
            {
                System.Windows.Forms.MessageBox.Show("��ѡ�񹩻����ң�");
                return false;
            }
            try
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"]) <= 0)
                    {
                        System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + "������������С�ڵ�����");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
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
                    System.Windows.Forms.DialogResult rs = MessageBox.Show("ȷ�϶���ѡ�����ݽ���ɾ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    string[] keys = new string[] { sv.Cells[delRowIndex, (int)ColumnSet.ColItemID].Text };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        #region �����Ƴ�

                        if (dr["���"].ToString() != "" && dr["���"].ToString() != "0")
                        {
                            int parm = this.storeManager.DeleteApply(this.listNO, NConvert.ToInt32(dr["���"].ToString()), this.MatApplyManager.DeptInfo.ID);
                            if (parm == -1)
                            {
                                System.Windows.Forms.MessageBox.Show(this.storeManager.Err);
                                return -1;
                            }
                            if (parm == 0)
                            {
                                System.Windows.Forms.MessageBox.Show("��������ѱ��������� ������!");
                                return -1;
                            }
                            System.Windows.Forms.MessageBox.Show("ɾ���ɹ�");
                        }

                        #endregion

                        this.MatApplyManager.Fp.StopCellEditing();

                        this.hsApplyData.Remove(dr["��Ʒ����"].ToString());

                        this.dt.Rows.Remove(dr);

                        this.MatApplyManager.Fp.StartCellEditing(null, false);

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

        //public void SetFormat()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public int Clear()
        {
            try
            {
                this.dt.Rows.Clear();

                this.dt.AcceptChanges();

                this.hsApplyData.Clear();

                this.MatApplyManager.TotCostInfo = "";

                this.listNO = "";

                this.serialNO = 0;
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
            if (this.svTemp != null)
            {
                if (this.svTemp.Rows.Count > 0)
                {
                    this.MatApplyManager.SetFpFocus();

                    this.svTemp.ActiveRowIndex = this.svTemp.Rows.Count - 1;
                    this.svTemp.ActiveColumnIndex = (int)ColumnSet.ColApplyQty;
                }
                else
                {
                    this.MatApplyManager.SetFocus();
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
            Neusoft.NFC.Management.Transaction t = new Transaction(Neusoft.NFC.Management.Connection.Instance);
            t.BeginTransaction();
            this.storeManager.SetTrans(t.Trans);

            //��ȡϵͳʱ��
            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            if (this.listNO == "")
            {
                //��ȡ�����뵥��
                this.listNO = this.storeManager.GetApplyNO(this.MatApplyManager.DeptInfo.ID);
            }

            //			int serialNO = 0;

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                string key = dr["��Ʒ����"].ToString();

                Neusoft.HISFC.Object.Material.Apply apply = this.hsApplyData[key] as Neusoft.HISFC.Object.Material.Apply;

                apply.Operation.ApplyOper.OperTime = sysTime;
                apply.Operation.Oper.OperTime = sysTime;
                apply.Operation.ApproveOper.OperTime = sysTime;
                apply.Operation.ApplyOper.ID = this.MatApplyManager.OperInfo.ID;
                apply.Operation.Oper.ID = this.MatApplyManager.OperInfo.ID;
                apply.TargetDept.ID = this.MatApplyManager.TargetDept.ID;

                apply.Operation.ApplyQty = NConvert.ToDecimal(dr["��������"].ToString());
                apply.ApplyPrice = NConvert.ToDecimal(dr["����"].ToString());
                apply.ApplyCost = NConvert.ToDecimal(dr["������"].ToString());


                if (this.MatApplyManager.IOType == "1")
                {
                    apply.Class2Type = "0510";

                    if (this.isBack)
                    {
                        apply.SystemType = "18";
                        apply.State = "M";
                    }
                    else
                    {
                        if (this.MatApplyManager.DeptInfo.Memo == "L")
                        {
                            apply.SystemType = "12";
                        }
                        else
                        {
                            apply.SystemType = "13";
                        }
                    }

                }
                else
                {
                    apply.Class2Type = "0520";
                    apply.SystemType = "24";
                }


                apply.Memo = dr["��ע"].ToString();

                if (apply.ID == "")
                {
                    apply.ApplyListNO = this.listNO;              //���뵥�ݺ�

                    serialNO++;

                    apply.SerialNO = serialNO;

                    if (this.storeManager.InsertApply(apply) == -1)
                    {
                        t.RollBack();
                        MessageBox.Show(this.storeManager.Err);
                        return;
                    }
                }
                else
                {
                    int parm = this.storeManager.UpdateApply(apply);
                    if (parm == -1)
                    {
                        t.RollBack();
                        System.Windows.Forms.MessageBox.Show("�������������и���ʧ��" + this.storeManager.Err);
                        return;
                    }
                    if (parm == 0)
                    {
                        t.RollBack();
                        System.Windows.Forms.MessageBox.Show("�����뵥�ѱ���ˣ��޷������޸�!��ˢ������");
                        return;
                    }
                }
            }

            t.Commit();

            System.Windows.Forms.MessageBox.Show("��������ɹ�");

            this.Clear();
        }

        public void SaveCheck(bool IsHeaderCheck)
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

            //��������			
            Neusoft.NFC.Management.Transaction t = new Transaction(Neusoft.NFC.Management.Connection.Instance);
            t.BeginTransaction();
            this.storeManager.SetTrans(t.Trans);
            this.myControler.SetTrans(t.Trans);

            //���ʿ��������Ƿ���Ҫ�ⷿ���
            Neusoft.HISFC.Object.Base.Controler controler = this.myControler.QueryControlInfoByCode("WZ0001");


            //��ȡϵͳʱ��
            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            foreach (DataRow dr in dt.Rows)
            {
                string key = dr["��Ʒ����"].ToString();

                Neusoft.HISFC.Object.Material.Apply apply = this.hsApplyData[key] as Neusoft.HISFC.Object.Material.Apply;

                apply.Operation.ApplyOper.OperTime = sysTime;
                apply.Operation.Oper.OperTime = sysTime;
                apply.Operation.ApproveOper.OperTime = sysTime;
                apply.Operation.ApplyOper.ID = this.MatApplyManager.OperInfo.ID;
                apply.Operation.Oper.ID = this.MatApplyManager.OperInfo.ID;
                apply.TargetDept.ID = this.MatApplyManager.TargetDept.ID;
                apply.OutQty = Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"].ToString());
                apply.OutCost = Neusoft.NFC.Function.NConvert.ToDecimal(dr["���Ž��"].ToString());
                apply.Operation.ApplyQty = Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"].ToString());
                decimal applyCost = apply.Operation.ApplyQty * Neusoft.NFC.Function.NConvert.ToDecimal(dr["����"].ToString());
                int parm = -1;

                if (IsHeaderCheck)
                {
                    parm = this.storeManager.UpdateApplyHeaderCheck(apply.StockDept.ID, apply.ApplyListNO, apply.SerialNO, "M", apply.Operation.Oper.ID, apply.Operation.Oper.OperTime, apply.OutQty, apply.OutCost);
                }
                else
                {
                    if (controler != null && controler.ControlerValue == "1")
                    {
                        parm = this.storeManager.UpdateApplyCheckAndNum(apply.StockDept.ID, apply.ApplyListNO, apply.SerialNO, "MU", apply.Operation.Oper.ID, apply.Operation.Oper.OperTime, apply.Operation.ApplyQty, applyCost);
                    }
                    else
                    {
                        parm = this.storeManager.UpdateApplyCheckAndNum(apply.StockDept.ID, apply.ApplyListNO, apply.SerialNO, "M", apply.Operation.Oper.ID, apply.Operation.Oper.OperTime, apply.Operation.ApplyQty, applyCost);
                    }
                }
                if (parm == -1)
                {
                    t.RollBack();
                    System.Windows.Forms.MessageBox.Show("�������������и���ʧ��" + this.storeManager.Err);
                    return;
                }
                if (parm == 0)
                {
                    t.RollBack();
                    System.Windows.Forms.MessageBox.Show("�����뵥�ѱ���ˣ��޷������޸�!��ˢ������");
                    return;
                }

            }

            t.Commit();

            System.Windows.Forms.MessageBox.Show("���뵥��˳ɹ�");

            this.Clear();
        }

        public int Print()
        {
            return 1;
        }

        public int Cancel()
        {
            DialogResult r;

            r = MessageBox.Show("ȷ��Ҫ���ϸ������𣿲������ɳ�����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (r == DialogResult.No)
            {
                return 1;
            }

            this.dt.DefaultView.RowFilter = "1=1";
            for (int i = 0; i < this.dt.DefaultView.Count; i++)
            {
                this.dt.DefaultView[i].EndEdit();
            }

            if (this.listNO == "")
            {
                MessageBox.Show("�����뵥��δ��Ч��");
                return 0;
            }

            System.Collections.Hashtable hsHave = new Hashtable();

            foreach (DataRow dr in dt.Rows)
            {
                string key = dr["��Ʒ����"].ToString();

                Neusoft.HISFC.Object.Material.Apply apply = this.hsApplyData[key] as Neusoft.HISFC.Object.Material.Apply;

                if (!hsHave.ContainsKey(listNO))
                {
                    //��������			
                    Neusoft.NFC.Management.Transaction t = new Transaction(Neusoft.NFC.Management.Connection.Instance);
                    t.BeginTransaction();
                    this.storeManager.SetTrans(t.Trans);
                    this.myControler.SetTrans(t.Trans);


                    //��ȡϵͳʱ��
                    DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

                    hsHave.Add(listNO, apply);

                    int parm = -1;

                    parm = this.storeManager.UpdateApplyCheck(apply.StockDept.ID, apply.ApplyListNO, apply.SerialNO, "U", apply.Operation.Oper.ID, apply.Operation.Oper.OperTime);

                    if (parm == -1)
                    {
                        t.RollBack();
                        System.Windows.Forms.MessageBox.Show("��������ʧ��!" + this.storeManager.Err);
                        return -1;
                    }
                    if (parm == 0)
                    {
                        t.RollBack();
                        System.Windows.Forms.MessageBox.Show("�����뵥״̬�ѽ������ı�!");
                        return -1;
                    }

                    t.Commit();

                    System.Windows.Forms.MessageBox.Show("���뵥���ϳɹ�");
                }
                else
                {
                    continue;
                }

            }

            this.Clear();

            return 1;
        }

        public int ImportData()
        {
            return 1;
        }

        #endregion

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.MatApplyManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColApplyQty)
            {
                string[] keys = new string[] { this.MatApplyManager.FpSheetView.Cells[this.MatApplyManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemID].Text };
                DataRow dr = this.dt.Rows.Find(keys);

                if (dr != null)
                {
                    dr["������"] = Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"]) * Neusoft.NFC.Function.NConvert.ToDecimal(dr["����"]);

                    dr.EndEdit();

                    this.CompuateSum();
                }
            }
            if (this.MatApplyManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColOutQty)
            {
                string[] keys = new string[] { this.MatApplyManager.FpSheetView.Cells[this.MatApplyManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemID].Text };
                DataRow dr = this.dt.Rows.Find(keys);

                if (dr != null)
                {
                    dr["���Ž��"] = Neusoft.NFC.Function.NConvert.ToDecimal(dr["��������"]) * Neusoft.NFC.Function.NConvert.ToDecimal(dr["����"]);

                    dr.EndEdit();
                }
            }
        }


        private void value_EndTargetChanged(Neusoft.NFC.Object.NeuObject changeData, object param)
        {
            this.Clear();

            this.ShowSelectData();
        }


        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.MatApplyManager.FpSheetView != null)
            {
                if (key == System.Windows.Forms.Keys.Enter)
                {
                    if (this.MatApplyManager.FpSheetView.ActiveRowIndex == this.MatApplyManager.FpSheetView.Rows.Count - 1)
                    {
                        this.MatApplyManager.SetFocus();
                    }
                    else
                    {
                        this.MatApplyManager.FpSheetView.ActiveRowIndex++;
                        this.MatApplyManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColApplyQty;
                        if (this.MatApplyManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColOutQty)
                        {
                            this.MatApplyManager.SetFocus();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// ������
        /// </summary>
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
            ColRetailPrice,
            /// <summary>
            /// ��λ
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// ��������
            /// </summary>
            ColApplyQty,
            /// <summary>
            /// ������
            /// </summary>
            ColApplyCost,
            /// <summary>
            /// ��������
            /// </summary>
            ColOutQty,
            /// <summary>
            /// ���Ž��
            /// </summary>
            ColOutCost,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ColItemID,
            /// <summary>
            /// ���
            /// </summary>
            ColNO,
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
