using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Material.In
{
    /// <summary>
    /// [��������: �������ҵ����]<br></br>
    /// [�� �� ��: �]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     1���������Ӷ༶�������
    ///             1) ͨ��Ȩ�����ô������Ӷ���Ȩ��
    ///             2) ��д���������� �Բ�ͬ�����Ȩ�޷��ز�ͬʵ��
    ///             3) ���ݲ�ͬ�����Ȩ�޶������������ò�ͬ��״̬
    ///             4) ��ȫ��������ʱ �γɿɳ�������������
    /// </˵��>
    /// </summary>
    public class InnerApplyPriv : IMatManager
    {
        #region ���췽��

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="isBakcIn"></param>
        /// <param name="ucMatApplyManager">�������FALSE , �˿����TRUE </param>
        public InnerApplyPriv(bool isBakcIn, In.ucMatIn ucMatApplyManager)
        {
            this.isBack = isBakcIn;

            this.listNO = "";

            this.SetMaterialProperty(ucMatApplyManager);

        }

        #endregion

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
        private In.ucMatIn MatApplyManager;

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
        private FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �ֵ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();
        /// <summary>
        /// ��������ҵ����{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����б�����ʾ������{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private int visibleColumns = 3;

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Controler myControler = new FS.HISFC.BizLogic.Manager.Controler();

        private FS.HISFC.BizLogic.Manager.Department myDept = new FS.HISFC.BizLogic.Manager.Department();

        private FS.HISFC.BizLogic.Manager.Person myPerson = new FS.HISFC.BizLogic.Manager.Person();

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

        private void SetMaterialProperty(In.ucMatIn ucMatApplyManager)
        {
            this.MatApplyManager = ucMatApplyManager;
            //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            visibleColumns = controlIntegrate.GetControlParam<int>("MT0002", true);
            if (this.MatApplyManager != null)
            {
                //���ý�����ʾ
                this.MatApplyManager.IsShowItemSelectpanel = true;
                this.MatApplyManager.IsShowInputPanel = false;

                //�����ڲ�����Ŀ�������Ϣ
                this.MatApplyManager.SetTargetDept(false, true, FS.HISFC.Models.IMA.EnumModuelType.Material, FS.HISFC.Models.Base.EnumDepartmentType.L);
                //�����ڲ�������ʾ�Ĵ�ѡ������
                //this.MatApplyManager.SetSelectData("1", false, null, null, null);
                this.ShowSelectData();
                //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
                this.MatApplyManager.SetItemListWidth(visibleColumns);
                //���ù�������ť��ʾ
                this.MatApplyManager.SetToolBarButtonVisible(true, false, false, false, true, true, false);
                //������Ϣ��ʾ
                this.MatApplyManager.ShowInfo = "";
                //Fp ����
                this.MatApplyManager.FpSheetView.DataAutoSizeColumns = false;
                this.MatApplyManager.Fp.EditModeReplace = true;

                this.MatApplyManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);
                this.MatApplyManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

                this.MatApplyManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);
                this.MatApplyManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

                this.MatApplyManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
                this.MatApplyManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);

                this.MatApplyManager.FpSheetView.DataAutoSizeColumns = false;
                this.MatApplyManager.FpSheetView.DataAutoCellTypes = false;
                this.SetFormat();

            }
        }

        /// <summary>
        /// ��DataTable����������
        /// </summary>
        /// <param name="apply">������Ϣ</param>
        /// <param name="dataSource">������Դ 0 ԭʼ���� 1 �������</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Material.Apply apply, string dataSource)
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
                    //�ڲ�������������۸�Ӧ�������ۼ� ����������Ŀ¼�еĵ��� by yuyun {3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                    //cost = apply.Operation.ApplyQty * apply.Item.UnitPrice;
                    cost = apply.Operation.ApplyQty * apply.ApplyPrice;
                    unit = apply.Item.MinUnit;
                    //price = apply.Item.UnitPrice;
                    price = apply.ApplyPrice;
                }
                else
                {
                    qty = apply.Operation.ApplyQty / apply.Item.PackQty;
                    //�ڲ�������������۸�Ӧ�������ۼ� ����������Ŀ¼�еĵ��� by yuyun {3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                    //cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.Item.PackPrice;
                    cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.ApplyPrice;
                    unit = apply.Item.PackUnit;
                    //price = apply.Item.PackPrice;
                    price = apply.ApplyPrice;
                }
                this.dt.Rows.Add(new object[] { 
												  apply.Item.Name,									//��Ʒ����
												  apply.Item.Specs,									//���
												  price,											//���ۼ�
												  unit,												//��װ��λ
												  qty,												//��������
												  cost,												//������
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
        protected virtual int AddApplyToTable(FS.HISFC.Models.Material.Apply apply, string dataSource)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                FS.HISFC.BizLogic.Material.MetItem managerItem = new FS.HISFC.BizLogic.Material.MetItem();
                apply.Item = managerItem.GetMetItemByMetID(apply.Item.ID);

                decimal price = 0;
                decimal qty = 0;
                decimal cost = 0;
                string unit = "";

                if (this.isMinUnit)
                {
                    qty = apply.Operation.ApplyQty;
                    //�ڲ�������������۸�Ӧ�������ۼ� ����������Ŀ¼�еĵ��� by yuyun {3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                    //cost = apply.Operation.ApplyQty * apply.Item.UnitPrice;
                    cost = apply.Operation.ApplyQty * apply.ApplyPrice;
                    unit = apply.Item.MinUnit;
                    //price = apply.Item.UnitPrice;
                    price = apply.ApplyPrice;
                }
                else
                {
                    qty = apply.Operation.ApplyQty / apply.Item.PackQty;
                    //�ڲ�������������۸�Ӧ�������ۼ� ����������Ŀ¼�еĵ��� by yuyun {3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                    //cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.Item.PackPrice;
                    cost = apply.Operation.ApplyQty / apply.Item.PackQty * apply.ApplyPrice;
                    unit = apply.Item.PackUnit;
                    //price = apply.Item.PackPrice;
                    price = apply.ApplyPrice;
                }
                this.dt.Rows.Add(new object[] { 
												  apply.Item.Name,                                //��Ʒ����
												  apply.Item.Specs,                               //���
												  price,					                      //���ۼ�
												  unit,											  //��װ��λ
												  qty,											  //��������
												  cost,                                           //������
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

            foreach (FS.HISFC.Models.Material.Apply info in alDetail)
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
        /// ������Ʒ������� ������������
        /// </summary>
        /// <param name="apply">��Ʒʵ��</param>
        /// <returns></returns>
        private int AddDrugData(FS.HISFC.Models.Material.Apply apply)
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
        /// ������Ʒ������� �����������{3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
        /// </summary>
        /// <param name="drugNO">��Ʒ����</param>
        /// <param name="outBillNO">�������</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO, string outBillNO, decimal applyPrice)
        {

            FS.HISFC.Models.Material.MaterialItem item = itemManager.GetMetItemByMetID(drugNO);

            if (item == null)
            {
                System.Windows.Forms.MessageBox.Show("������Ʒ������Ϣʧ��");
                return -1;
            }

            FS.HISFC.Models.Material.Apply apply = new FS.HISFC.Models.Material.Apply();

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
            //�ڲ�������������۸�Ӧ�������ۼ� ����������Ŀ¼�еĵ��� by yuyun {3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
            apply.ApplyPrice = applyPrice;

            if (this.AddDataToTable(apply, "1") == 1)
            {
                this.hsApplyData.Add(apply.Item.ID, apply);

                //				this.SetFormat();

                this.SetFocusSelect();

            }

            return 1;
        }

        /// <summary>
        /// ������Ʒ������� ����������� --�˿�������
        /// </summary>
        /// <param name="itemCode">��Ʒ����</param>
        /// <param name="outNo">������ˮ��</param>
        /// <param name="storeNo">������</param>
        /// <returns></returns>
        private int AddDrugData(string itemCode, string outNo, string storeNo)
        {

            FS.HISFC.Models.Material.MaterialItem item = itemManager.GetMetItemByMetID(itemCode);

            if (item == null)
            {
                System.Windows.Forms.MessageBox.Show("������Ʒ������Ϣʧ��");
                return -1;
            }

            FS.HISFC.Models.Material.Apply apply = new FS.HISFC.Models.Material.Apply();

            apply.Item = item;

            if (this.hsApplyData.Contains(apply.Item.ID))
            {
                System.Windows.Forms.MessageBox.Show("����Ʒ�����");
                return 0;
            }
            apply.OutNo = outNo;
            apply.StockNO = storeNo;
            //��ȡ��Ӧ���ſ����Ϣ�е����ۼ�{3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
            List<FS.HISFC.Models.Material.StoreDetail> listStoreBase = this.storeManager.QueryStoreListAll(this.MatApplyManager.TargetDept.ID, itemCode, storeNo, true);
            apply.ApplyPrice = listStoreBase[0].StoreBase.PriceCollection.RetailPrice;
            //----------------------
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
        /// ������Ʒ��������˿�����{3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
        /// </summary>
        /// <param name="drugNO">��Ʒ����</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO, decimal applyPrice)
        {
            return this.AddDrugData(drugNO, null, applyPrice);
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
                string[] label = new string[] { "�������", "���ⵥ�ݺ�", "������ˮ��", "������", "��Ʒ����", "��Ʒ����", "���", "����", "��װ��λ", "��С��λ", "ƴ����", "�����" };
                int[] width = new int[] { 60, 60, 60, 60, 60, 120, 80, 60, 60, 60, 60, 60 };
                bool[] visible = new bool[] { false, false, false, false, false, true, true, true, true, true, true, true };

                this.MatApplyManager.SetSelectData("3", false, new string[] { "Material.Store.GetOutputInfoForInput" }, filterStr, this.MatApplyManager.DeptInfo.ID, "A", "2", this.MatApplyManager.TargetDept.ID);

                this.MatApplyManager.SetSelectFormat(label, width, visible);
            }
            else
            {
                this.MatApplyManager.SetSelectData("1", false, null, null, null);
            }

            this.MatApplyManager.SetItemListWidth(3);

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
            this.MatApplyManager.FpSheetView.DataAutoSizeColumns = false;

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColItemName].Width = 150F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 80F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 70F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].Width = 80F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].Width = 100F;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].CellType = numberCellType;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].CellType = numberCellType;

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColItemID].Visible = false;           //��Ʒ����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColNO].Visible = false;               //���
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColDataSource].Visible = false;       //������Դ
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����

            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = 200F;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].Locked = false;
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
            //					using (Pharmacy.ucPhaAlter uc = new ucPhaAlter())
            //					{
            //						uc.DeptCode = deptCode;
            //						uc.SetData();
            //						uc.Focus();
            //						FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
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
            //				FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
            //				foreach (FS.FrameWork.Models.NeuObject temp in alDetail)
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
                    retailCost += FS.FrameWork.Function.NConvert.ToDecimal(dr["��������"]) * FS.FrameWork.Function.NConvert.ToDecimal(dr["����"]);
                }

                this.MatApplyManager.TotCostInfo = string.Format("�����ܽ��:{0} ", retailCost.ToString("N"));
            }
        }

        #endregion

        #region IMatManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
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

            if (isBack)
            {
                drugID = sv.Cells[activeRow, 4].Value.ToString();

                string outbillNO = sv.Cells[activeRow, 1].Value.ToString();
                string outNo = sv.Cells[activeRow, 2].Value.ToString();
                string stockNo = sv.Cells[activeRow, 3].Value.ToString();
                if (this.isBack)
                {
                    return this.AddDrugData(drugID, outNo, stockNo);
                }
                else
                {
                    //{3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                    //return this.AddDrugData(drugID, outbillNO);
                    return -1;
                }
            }
            else
            {
                //�Զ���������ʱ����ѶԵ� {7019A2A6-ADCA-4984-944B-C4F1A312449A}
                //drugID = sv.Cells[activeRow, 0].Value.ToString();
                drugID = sv.Cells[activeRow, 11].Value.ToString();
                //{3F8F98B1-44E9-4828-90C4-0931F6DA7B87}
                decimal applyPrice = NConvert.ToDecimal(sv.Cells[activeRow, 3].Text);
                //-----------------------
                return this.AddDrugData(drugID, applyPrice);
            }
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, FS.HISFC.Models.Material.Input input)
        {
            return 1;
        }

        public int ShowApplyList()
        {
            ArrayList alTemp = new ArrayList();
            //��ȡ������Ϣ{CAC9F782-773F-4507-AD2D-C0F73513FF42}
            string currentDeptID = string.Empty;
            currentDeptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            if (this.MatApplyManager.DeptInfo.Memo == "L")
            {
                alTemp = this.storeManager.QueryApplySimple(this.MatApplyManager.DeptInfo.ID, currentDeptID, this.MatApplyManager.Class2Priv.ID, "0", "12");
            }
            else
            {
                alTemp = this.storeManager.QueryApplySimple(this.MatApplyManager.DeptInfo.ID, currentDeptID, this.MatApplyManager.Class2Priv.ID, "0", "13");
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
            //				foreach (FS.FrameWork.Models.NeuObject info in alTemp)
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
            //			FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            //			string[] fpLabel = { "���뵥��", "������λ" };
            //			float[] fpWidth = { 120F, 120F };
            //			bool[] fpVisible = { true, true, false, false, false, false };

            FS.FrameWork.Models.NeuObject selectObject = new FS.FrameWork.Models.NeuObject();

            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alTemp, ref selectObject) == 1)
            {
                this.Clear();

                FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

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
                    if (FS.FrameWork.Function.NConvert.ToDecimal(dr["��������"]) <= 0)
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

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].CellType = numberCellType;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].CellType = numberCellType;

            DataTable dtAddMofity = this.dt.GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
                return;

            //��������			

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //��ȡϵͳʱ��
            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            if (this.listNO == "")
            {
                //��ȡ�����뵥��
                this.listNO = this.storeManager.GetApplyNO(this.MatApplyManager.DeptInfo.ID);
            }

            foreach (DataRow dr in dtAddMofity.Rows)
            {
                string key = dr["��Ʒ����"].ToString();

                FS.HISFC.Models.Material.Apply apply = this.hsApplyData[key] as FS.HISFC.Models.Material.Apply;

                apply.Operation.ApplyOper.OperTime = sysTime;
                apply.Operation.Oper.OperTime = sysTime;
                apply.Operation.ApproveOper.OperTime = sysTime;
                apply.Operation.ApplyOper.ID = this.MatApplyManager.OperInfo.ID;
                apply.Operation.Oper.ID = this.MatApplyManager.OperInfo.ID;
                apply.TargetDept.ID = this.MatApplyManager.TargetDept.ID;

                apply.Operation.ApplyQty = NConvert.ToDecimal(dr["��������"].ToString());
                apply.ApplyPrice = NConvert.ToDecimal(dr["����"].ToString());
                apply.ApplyCost = NConvert.ToDecimal(dr["������"].ToString());
                //0 ����״̬ 1 ���ƻ� 9 �������� 3 ȫ������{E227592A-8043-4f34-BC14-4E84026B1FA6}
                apply.Extend1 = "0";
                //0 ������� 1 �����깺{5247C9D6-E20B-492d-94CF-37420A6E0DF4}
                apply.Extend3 = "0";

                apply.Class2Type = this.MatApplyManager.Class2Priv.ID;
                apply.PrivType = this.MatApplyManager.PrivType.ID;
                apply.SystemType = this.MatApplyManager.PrivType.Memo;

                apply.Memo = dr["��ע"].ToString();

                if (apply.ID == "")
                {
                    apply.ApplyListNO = this.listNO;              //���뵥�ݺ�

                    serialNO++;

                    apply.SerialNO = serialNO;

                    if (this.storeManager.InsertApply(apply) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.storeManager.Err);
                        return;
                    }
                }
                else
                {
                    int parm = this.storeManager.UpdateApply(apply);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show("�������������и���ʧ��" + this.storeManager.Err);
                        return;
                    }
                    if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show("�����뵥�ѱ���ˣ��޷������޸�!��ˢ������");
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            System.Windows.Forms.MessageBox.Show("��������ɹ�");

            this.Clear();
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].CellType = numberCellType;
            this.MatApplyManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].CellType = numberCellType;
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

                FS.HISFC.Models.Material.Apply apply = this.hsApplyData[key] as FS.HISFC.Models.Material.Apply;

                if (!hsHave.ContainsKey(listNO))
                {
                    //��������			

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();

                    this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.myControler.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                    //��ȡϵͳʱ��
                    DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

                    hsHave.Add(listNO, apply);

                    int parm = -1;

                    parm = this.storeManager.UpdateApplyCheck(apply.StockDept.ID, apply.ApplyListNO, apply.SerialNO, "U", apply.Operation.Oper.ID, apply.Operation.Oper.OperTime);

                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show("��������ʧ��!" + this.storeManager.Err);
                        return -1;
                    }
                    if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show("�����뵥״̬�ѽ������ı�!");
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

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

        #region IMatManager ��Ա

        //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        //���ͷŵ��¼���Դ
        public void Dispose()
        {
            this.MatApplyManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

            this.MatApplyManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

            this.MatApplyManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);

        }

        #endregion

        #region ����

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.MatApplyManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColApplyQty)
            {
                string[] keys = new string[] { this.MatApplyManager.FpSheetView.Cells[this.MatApplyManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColItemID].Text };
                DataRow dr = this.dt.Rows.Find(keys);

                if (dr != null)
                {
                    dr["������"] = FS.FrameWork.Function.NConvert.ToDecimal(dr["��������"]) * FS.FrameWork.Function.NConvert.ToDecimal(dr["����"]);

                    dr.EndEdit();

                    this.CompuateSum();
                }
            }
        }

        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
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
                    }
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

#endregion
    }
}
