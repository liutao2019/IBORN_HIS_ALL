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
    /// <summary>
    /// 
    /// </summary>
    public class ApproveInPriv : IMatManager
    {
        #region ���췽��

        public ApproveInPriv(Material.In.ucMatIn ucMatInManager)
        {
            this.SetMatManagerProperty(ucMatInManager);
        }

        #endregion

        #region �����

        /// <summary>
        /// ���ؼ�
        /// </summary>
        private ucMatIn ucInManager = null;

        private System.Data.DataTable dt = null;

        /// <summary>
        /// �������������Ƿ�Ϊ�ֿ�
        /// </summary>
        private bool isPIDept = true;

        /// <summary>
        /// �Ƿ񰴴��װ��ʽ���
        /// </summary>
        private bool isUsePackIn = false;

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.BizLogic.Material.Store storeManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �����ֵ������
        /// </summary>
        FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();
        /// <summary>
        /// ��������ҵ����{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����б�����ʾ������{7019A2A6-ADCA-4984-944B-C4F1A312449A}
        /// </summary>
        private int visibleColumns = 3;
        /// <summary>
        /// �洢��������
        /// </summary>
        private System.Collections.Hashtable hsInData = new Hashtable();

        /// <summary>
        /// ��׼ʱ�Ƿ������޸ķ�Ʊ��/�����
        /// </summary>
        private bool isApproveEdit = true;

        /// <summary>
        /// ������Ϣ �ֿ��׼�洢���ݺ� �������Һ�׼�洢��Ʒ����+���ݺ� ��ʾ�����ŵ����Ƿ�ͬʱ��׼
        /// </summary>
        private System.Collections.Hashtable hsListNO = new Hashtable();

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucMatListSelect ucListSelect = null;

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
        /// �������ؼ�����
        /// </summary>
        private void SetMatManagerProperty(Material.In.ucMatIn ucMatInManager)
        {
            this.ucInManager = ucMatInManager;
            //ͨ���������������б�����ʾ���� {7019A2A6-ADCA-4984-944B-C4F1A312449A}
            visibleColumns = controlIntegrate.GetControlParam<int>("MT0002", true);
            //���ý�����ʾ{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            this.ucInManager.IsShowInputPanel = false;
            this.ucInManager.IsShowItemSelectpanel = true;
            //����Ŀ�굥λѡ�� ���ù�������ť״̬
            if (this.ucInManager.DeptInfo.Memo == "L")
            {
                this.isPIDept = true;
                this.ucInManager.SetTargetDept(true, true, FS.HISFC.Models.IMA.EnumModuelType.Material, FS.HISFC.Models.Base.EnumDepartmentType.L);
                this.ucInManager.SetToolBarButtonVisible(false, false, false, false, true, true, false);
            }
            else
            {
                this.isPIDept = false;
                this.ucInManager.SetTargetDept(false, true, FS.HISFC.Models.IMA.EnumModuelType.Material, FS.HISFC.Models.Base.EnumDepartmentType.L);
                this.ucInManager.SetToolBarButtonVisible(false, false, true, false, true, true, false);
            }
            //��ʾѡ����Ϣ
            if (this.ucInManager.TargetDept.ID != "")
            {
                this.ShowSelectData();
            }
            //������Ŀ�б���{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            this.ucInManager.SetItemListWidth(visibleColumns);

            this.ucInManager.Fp.EditModeReplace = true;
            this.ucInManager.FpSheetView.DataAutoSizeColumns = false;

            this.ucInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

            this.ucInManager.EndTargetChanged -= new In.ucMatIn.DataChangedHandler(value_EndTargetChanged);
            this.ucInManager.EndTargetChanged += new In.ucMatIn.DataChangedHandler(value_EndTargetChanged);

            this.ucInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);

            //���ӡ�ѡ�񡱰�ť
            System.EventHandler eHan = new EventHandler(this.ChangeCheck);
            this.ucInManager.AddToolBarButton("ѡ��", "�Ե�ǰ���ݷ���ѡ��", FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, 0, true, eHan);
        }

        /// <summary>
        /// ���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeCheck(object sender, System.EventArgs args)
        {
            foreach (DataRow dr in this.dt.Rows)
            {
                dr["��׼"] = !NConvert.ToBoolean(dr["��׼"]);
            }
        }

        /// <summary>
        /// ����Fp��ʾ
        /// </summary>
        public virtual void SetFormat()
        {
            this.ucInManager.FpSheetView.DefaultStyle.Locked = true;
            this.ucInManager.FpSheetView.DataAutoSizeColumns = false;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Width = 38F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 60F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Width = 70F;

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numberCellType;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColProduceName].Visible = false;      //��������
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColDrugNO].Visible = false;           //��Ʒ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColStockNO].Visible = false;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInBillNO].Visible = false;


            //���ݵ�ǰ�����Ƿ�Ϊ�ֿ�����÷�Ʊ�š�����۵���ʾ
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Visible = this.isPIDept;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Visible = this.isPIDept;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchaseCost].Visible = this.isPIDept;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;

            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = this.isPIDept ? 100F : 250F;

            if (this.isApproveEdit)
            {
                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Locked = false;
                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Locked = false;

                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].BackColor = System.Drawing.Color.SeaShell;
                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
                this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].BackColor = System.Drawing.Color.SeaShell;
            }
            //{99EE1131-D261-4772-A51C-3AB108A2F822}��׼��ⲻ�����޸Ĺ����
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Locked = true;
        }

        /// <summary>
        /// ��DataTable�������������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int AddDataToTable(FS.HISFC.Models.Material.Input input)
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
                input.StoreBase.RetailCost = input.StoreBase.Quantity * input.StoreBase.PriceCollection.RetailPrice;
                input.StoreBase.PurchaseCost = inQty * inPrice;

                bool isApprove = false;

                if (this.isPIDept)
                    isApprove = false;
                else
                    isApprove = true;

                if (input.InvoiceNO == null)
                {
                    input.InvoiceNO = "";
                }

                this.dt.Rows.Add(new object[] { 
												  isApprove,												//��׼
												  input.StoreBase.Item.Name,								//��Ʒ����
												  input.StoreBase.Item.Specs,								//���
												  input.StoreBase.BatchNO,									//����
												  input.StoreBase.PriceCollection.RetailPrice,				//���ۼ�                                                
												  input.StoreBase.Item.PackUnit,							//��װ��λ
												  inQty,//input.StoreBase.Quantity / input.StoreBase.Item.PackQty,  //�������
												  inPrice,//input.StoreBase.PriceCollection.PurchasePrice,			//�����
												  input.StoreBase.PurchaseCost,								//������												  
												  input.StoreBase.PurchaseCost,              //�����                                                
												  input.InvoiceNO,											//��Ʊ��												  
												  input.StoreBase.Producer.Name,							//��������
												  input.Memo,												//��ע
												  input.StoreBase.Item.ID,									//��Ʒ����
												  input.ID,													//��ˮ��
												  input.StoreBase.StockNO,
												  input.StoreBase.Item.SpellCode,							//ƴ����
												  input.StoreBase.Item.WBCode,								//�����
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
        /// ����ѡ������
        /// </summary>
        private void ShowSelectData()
        {
            string targetNO = this.ucInManager.TargetDept.ID;
            if (targetNO == "" || targetNO == null)
                targetNO = "AAAA";

            if (this.isPIDept)                              //�ֿ�
            {
                string[] filterStr = new string[1] { "INVOICE_NO" };
                string[] label = new string[] { "��Ʊ��", "������λ����", "������λ����", "��ⵥ" };
                int[] width = new int[] { 60, 60, 120, 60 };
                bool[] visible = new bool[] { true, false, true, true };

                this.ucInManager.SetSelectData("3", false, new string[] { "Material.Store.GetInputInvoiceList" }, filterStr, this.ucInManager.DeptInfo.ID, "1", targetNO);

                this.ucInManager.SetSelectFormat(label, width, visible);
            }
            else
            {
                string[] filterStr = new string[3] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" };
                string[] label = new string[] { "������ˮ��", "���ⵥ�ݺ�", "��Ʒ����", "��Ʒ����", "���", "����", "��װ��λ", "��С��λ", "ƴ����", "�����" };
                int[] width = new int[] { 60, 60, 60, 120, 80, 60, 60, 60, 60, 60 };
                bool[] visible = new bool[] { false, false, false, true, true, true, false, true, false, false };

                this.ucInManager.SetSelectData("3", false, new string[] { "Material.Store.QueryOutputListForApproveInput" }, filterStr, this.ucInManager.DeptInfo.ID, "A", "1", targetNO);

                this.ucInManager.SetSelectFormat(label, width, visible);
            }
        }

        /// <summary>
        /// Ŀ�굥λ��Ϣ���
        /// </summary>
        /// <param name="targetNO">Ŀ�굥λ����</param>
        private int FillTargetInfo(string targetNO)
        {
            if (this.isPIDept)          //�ֿ�
            {
                FS.HISFC.BizLogic.Material.ComCompany comManager = new FS.HISFC.BizLogic.Material.ComCompany();
                FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();

                if (company == null)
                {
                    MessageBox.Show("�޷���ȡ����������λ��Ϣ");
                    return -1;
                }

                this.ucInManager.TargetDept = company;
                this.ucInManager.TargetDept.Memo = "1";
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(targetNO);
                if (dept == null)
                {
                    MessageBox.Show("�޷���ȡ������¼���������Ϣ��");
                    return -1;
                }

                this.ucInManager.TargetDept = dept;
                this.ucInManager.TargetDept.Memo = "0";
            }

            return 1;
        }

        /// <summary>
        /// ���õ��ݺ���ʾ
        /// </summary>
        /// <param name="listNO"></param>
        private void AddListNO(string listNO)
        {
            if (this.hsListNO.ContainsKey(listNO))
            {
                return;
            }

            this.hsListNO.Add(listNO, 1);

            if (this.ucInManager.ShowInfo == "��ʾ��Ϣ:")
            {
                this.ucInManager.ShowInfo = "��ⵥ��: " + listNO;
            }
            else
            {
                this.ucInManager.ShowInfo = this.ucInManager.ShowInfo + " �� " + listNO;
            }
        }

        /// <summary>
        /// �Ƴ����ݺ�
        /// </summary>
        /// <param name="listNO"></param>
        private void RemoveListNO(string listNO)
        {
            if (this.hsListNO.ContainsKey(listNO))
            {
                int iCount = (int)this.hsListNO[listNO];
                if (iCount == 1)
                {
                    this.hsListNO.Remove(listNO);
                    this.ucInManager.ShowInfo = "";
                    foreach (string strListNO in this.hsListNO.Keys)
                    {
                        if (this.ucInManager.ShowInfo == "")
                        {
                            this.ucInManager.ShowInfo = "��ⵥ��: " + strListNO;
                        }
                        else
                        {
                            this.ucInManager.ShowInfo = this.ucInManager.ShowInfo + " �� " + strListNO;
                        }
                    }
                }
                else
                {
                    this.hsListNO[listNO] = iCount - 1;
                }
            }
        }

        #region �ֿ��׼���غ���

        /// <summary>
        /// ���ݷ�Ʊ����Ӵ���׼����
        /// </summary>
        /// <param name="invoiceNO"></param>
        private int AddInDataByInvoiceNO(string invoiceNO)
        {
            //��ȡ����׼�������
            ArrayList alDetail = this.storeManager.QueryInputDetailByInvoice(this.ucInManager.DeptInfo.ID, invoiceNO, "1");
            if (alDetail == null)
            {
                MessageBox.Show(this.storeManager.Err);
                return -1;
            }

            //this.ucInManager.ShowInfo = "��ⵥ�ݺ�";

            foreach (FS.HISFC.Models.Material.Input input in alDetail)
            {
                //��������
                //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                input.User03 = input.ID + input.StoreBase.StockNO;
               // input.User03 = input.ID;

                //�ж��Ƿ��ظ�����
                if (this.hsInData.ContainsKey(this.GetKey(input)))
                {
                    MessageBox.Show("�÷�Ʊ�Ѽ���ѡ��!");
                    return 0;
                }

                if (!hsListNO.ContainsKey(input.InListNO))
                {
                    this.AddListNO(input.InListNO);
                }
                //����Ŀ�굥λ��Ϣ
                if (this.ucInManager.TargetDept.ID == "")
                {
                    this.FillTargetInfo(input.StoreBase.TargetDept.ID);
                }

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInData.Add(this.GetKey(input), input);
                }
            }

            this.SetFormat();

            return 1;
        }


        #endregion

        #region �������Һ�׼���ݼ��غ���

        /// <summary>
        /// ���ݳ�����ˮ�Ż�ȡ����׼����
        /// </summary>
        /// <param name="outNO">������ˮ��</param>
        /// <returns></returns>
        private int AddOutDataByOutNO(string outNO)
        {
            #region ��ʱƾ��2007-4-3
            /*
			ArrayList alDetail = this.storeManager.QueryOutputList(outNO);
			if (alDetail == null || alDetail.Count <= 0)
			{
				MessageBox.Show("���ݳ�����ˮ�Ż�ȡ�������ݷ�������");
				return -1;
			}

			foreach (FS.HISFC.Models.Material.Output output in alDetail)
			{
				FS.HISFC.Models.Material.Input input = this.InputConvert(output);

				if (this.hsInData.ContainsKey(this.GetKey(input)))
				{
					MessageBox.Show("�������Ѽ���ѡ��!");
					return 0;
				}

				//�Ƿ��Ѱ����õ���
				if (!this.hsListNO.ContainsKey(input.OutListNO))
				{
					if (this.hsListNO.Count > 0)
					{
						string msg = MessageBox.Show(string.Format("����Ʒ��ⵥ�ݺ��뵱ǰ��ⵥ�ݺŲ�ͬ ����Ʒ��ⵥ��{0} ȷ�ϼ�����?", input.OutListNO));
						DialogResult rs = MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
						if (rs == DialogResult.No)
						{
							return 0;
						}
					}
				}

				if (this.AddDataToTable(input) == 1)
				{
					this.hsInData.Add(this.GetKey(input), input);
					//���浥�ݺ�
					if (this.hsListNO.ContainsKey(input.OutNO))
					{
						this.hsListNO[input.OutNO] = ((int)this.hsListNO[input.OutNO]) + 1;
					}
					else
					{                      
						this.AddListNO(input.OutNO);
					}
				}
			}

			this.SetFormat();
			*/
            #endregion
            return 1;
        }

        /// <summary>
        /// ��Ӵ���׼�ĳ�������
        /// </summary>
        /// <param name="outListNO">���ⵥ�ݺ�</param>
        private void AddOutDataByListNO(string outListNO)
        {
            List<FS.HISFC.Models.Material.Output> alDetail = this.storeManager.QueryOutputByListNO(this.ucInManager.TargetDept.ID, outListNO, "1", this.ucInManager.DeptInfo.ID);

            if (alDetail == null)
            {
                MessageBox.Show("��ȡ������׼���ݷ�������" + this.storeManager.Err);
                return;
            }
            if (alDetail.Count == 0)
            {
                MessageBox.Show("�õ��ݿ����ѱ���׼");
                return;
            }

            //������Fp���г�ʼ�� ��߼����ٶ�
            ((System.ComponentModel.ISupportInitialize)(this.ucInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Material.Output output in alDetail)
            {
                //��Ϊ��ļ�¼�����д���{8764C351-D36D-4331-B21B-8E7D4847D260}
                if (output.StoreBase.Quantity - output.StoreBase.Returns <= 0)
                {
                    continue;
                }

                FS.HISFC.Models.Material.Input input = this.InputConvert(output);

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInData.Add(this.GetKey(input), input);
                }
                else
                {
                    MessageBox.Show("���س���ʵ����Ϣʱ��������");
                    return;
                }
            }
            //���浱ǰ������Ϣ
            this.hsListNO.Add(outListNO, alDetail.Count);

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.ucInManager.Fp)).EndInit();

            return;
        }

        /// <summary>
        /// ���ʵ�帳ֵ
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Material.Input InputConvert(FS.HISFC.Models.Material.Output output)
        {
            FS.HISFC.Models.Material.Input input = new FS.HISFC.Models.Material.Input();

            #region Input ��Ϣ���

            //����Ŀ�굥λ��Ϣ
            //if (this.ucInManager.TargetDept.ID == "")
            //{
            //	this.FillTargetInfo(input.StoreBase.TargetDept.ID);
            //}
            input.StoreBase.Item = this.itemManager.GetMetItemByMetID(output.StoreBase.Item.ID);
            input.StoreBase.StockDept = this.ucInManager.DeptInfo;                  //�������
            input.StoreBase.PrivType = this.ucInManager.PrivType.ID;                //������
            input.StoreBase.SystemType = this.ucInManager.PrivType.Memo;            //ϵͳ����
            input.StoreBase.State = "2";                                            //״̬ ��׼
            input.StoreBase.Company = this.ucInManager.TargetDept;
            //Ŀ�������ʱ������ʵ�����޴��ֶ�
            //input.TargetDept = this.ucInManager.TargetDept;						//Ŀ�굥λ
            //
            input.ID = output.ID;													//������ˮ��
            input.OutNO = output.ID;

            input.PlanListNO = output.OutListNO;									//���ⵥ�ݺ�

            input.StoreBase.SerialNO = output.StoreBase.SerialNO;                   //���
            input.StoreBase.BatchNO = output.StoreBase.BatchNO;                     //����
            input.StoreBase.ValidTime = output.StoreBase.ValidTime;                 //��Ч��
            input.StoreBase.Quantity = output.StoreBase.Quantity - output.StoreBase.Returns;  //����{8764C351-D36D-4331-B21B-8E7D4847D260}
                //output.StoreBase.Quantity;                   //����
            input.InCost = input.StoreBase.Quantity * output.StoreBase.PriceCollection.PurchasePrice;

            input.StoreBase.PlaceNO = output.StoreBase.PlaceNO;                     //��λ��
            input.StoreBase.StockNO = output.StoreBase.StockNO;                     //����
            input.StoreBase.Operation = output.StoreBase.Operation;                 //������Ϣ
            input.StoreBase.PriceCollection.PurchasePrice = output.StoreBase.PriceCollection.PurchasePrice;
            input.StoreBase.PriceCollection.RetailPrice = output.StoreBase.PriceCollection.RetailPrice;
            input.StoreBase.RetailCost = output.StoreBase.RetailCost;
            input.StoreBase.PriceCollection.PriceRate = output.StoreBase.PriceCollection.PriceRate;

            #endregion

            //�洢������Ϣ
            //������������ �����γ���ʱ ͬһ��Ʒ��ͬ���� ��ˮ����ͬ ��������Ų�ͬ
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            input.User03 = output.ID + output.StoreBase.StockNO;
           //input.User03 = output.ID + output.StoreBase.SerialNO;

            return input;
        }


        #endregion

        /// <summary>
        /// ���ܽ�����
        /// </summary>
        public void CompuateSum()
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

            if (this.isPIDept)
            {
                this.ucInManager.TotCostInfo = string.Format("�����ܽ��:{0} �����ܽ��:{1}", retailCost.ToString("N"), purchaseCost.ToString("N"));
            }
            else
            {
                this.ucInManager.TotCostInfo = string.Format("�����ܽ��:{0}", retailCost.ToString("N"));
            }
        }

        #region ��ֵ��ȡ

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Material.Input input)
        {
            return input.User03;
        }


        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //return dr["��ˮ��"].ToString();
            return dr["��ˮ��"].ToString() + dr["������"].ToString();
        }


        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <returns></returns>
        private string[] GetKey()
        {
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //string[] keys = new string[]{
            //                                this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColInBillNO].Text                                              
            //                            };
            string[] keys = new string[]{
											this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColInBillNO].Text,this.ucInManager.FpSheetView.Cells[this.ucInManager.FpSheetView.ActiveRowIndex,(int)ColumnSet.ColStockNO].Text                                              
										};

            return keys;
        }

        #endregion

        #endregion

        #region IMatManager ��Ա

        public FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get
            {
                // TODO:  ��� ApproveInPriv.InputModualUC getter ʵ��
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
												 new DataColumn("��׼",      dtBol),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("���",      dtStr),
												 new DataColumn("����",      dtStr),
												 new DataColumn("���ۼ�",    dtDec),
												 new DataColumn("��λ",  dtStr),												 
												 new DataColumn("�������",  dtDec),
												 new DataColumn("�����",    dtDec),
												 new DataColumn("������",  dtDec),
												 new DataColumn("�����",  dtDec),                                                                    
												 new DataColumn("��Ʊ��",    dtStr),												 
												 new DataColumn("��������",  dtStr),
												 new DataColumn("��ע",      dtStr),
												 new DataColumn("��Ʒ����",  dtStr),
												 new DataColumn("��ˮ��",	 dtStr),
												 new DataColumn("������",dtStr),
												 new DataColumn("ƴ����",    dtStr),
												 new DataColumn("�����",    dtStr),
												 new DataColumn("�Զ�����",  dtStr)
											 }
                );
            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            DataColumn[] keys = new DataColumn[2];
            keys[0] = this.dt.Columns["��ˮ��"];
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            keys[1] = this.dt.Columns["������"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }


        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string dataNO = sv.Cells[activeRow, 0].Text;

            //********************���ݲֿ�Ͷ������ҵĲ�ͬ���ֱ����˴����޸�

            if (this.isPIDept)              //�ֿ�
            {
                if (this.AddInDataByInvoiceNO(dataNO) == 1)
                {
                    this.SetFocusSelect();
                }
                return 1;
            }
            else                            //��������
            {
                if (this.AddOutDataByOutNO(dataNO) == 1)
                {
                    this.SetFocusSelect();
                }
                return 1;
            }
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
            // TODO:  ��� ApproveInPriv.ShowApplyList ʵ��
            return 1;
        }


        public int ShowInList()
        {
            // TODO:  ��� ApproveInPriv.ShowInList ʵ��
            return 1;
        }


        public int ShowOutList()
        {
            #region ԭ���ķ�����ʱƾ��
            /*
			string targetNO = "AAAA";
			if (this.ucInManager.TargetDept.ID != "" && this.ucInManager.TargetDept.ID != null)//�ֿ�
			{
				targetNO = this.ucInManager.TargetDept.ID;
			}

			ArrayList alList = this.storeManager.QueryOutputListForApproveInput(targetNO, this.ucInManager.DeptInfo.ID, "A");
			if (alList == null)
			{
				MessageBox.Show("��ȡ���ⵥ�����ݳ���" + this.storeManager.Err);
				return -1;
			}

			//��������ѡ�񵥾�
			FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
			string[] fpLabel = { "��ⵥ��", "������λ" };
			float[] fpWidth = { 120F, 120F };
			bool[] fpVisible = { true, true, false, false, false, false };

			if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alList, ref selectObj) == 1)
			{
				this.Clear();

				FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

				targeDept.ID = selectObj.Memo;              //������ұ���
				targeDept.Name = selectObj.Name;            //�����������
				targeDept.Memo = "0";                       //Ŀ�굥λ���� �ڲ�����

				if (this.ucInManager.TargetDept.ID != targeDept.ID)
				{
					this.ucInManager.TargetDept = targeDept;
					this.ShowSelectData();
				}

//				this.AddOutDataByListNO(selectObj.ID);

				this.SetFocusSelect();

				if (this.ucInManager.FpSheetView != null)
				{
					this.ucInManager.FpSheetView.ActiveRowIndex = 0;
				}
			}
			*/
            #endregion

            #region �·���
            try
            {
                if (this.ucListSelect == null)
                    this.ucListSelect = new ucMatListSelect();

                this.ucListSelect.Init();
                this.ucListSelect.DeptInfo = this.ucInManager.TargetDept;
                //{23CC782D-27F9-42ca-B8C2-2A4F079CECBF}��׼���ʱ�������ĳ��ⵥӦ����Ŀ�����Ϊ����Ȩ�޿��ҵ�
                this.ucListSelect.DeptCode = this.ucInManager.DeptInfo.ID;
                this.ucListSelect.State = "2";                  //�����״̬
                this.ucListSelect.Class2Priv = "0520";          //����

                //this.ucListSelect.SelecctListEvent -= new ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);
                this.ucListSelect.SelecctListEvent -= new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);
                this.ucListSelect.SelecctListEvent += new FS.HISFC.Components.Common.Controls.ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return -1;
            }
            #endregion
            return 1;
        }


        public int ShowStockList()
        {
            // TODO:  ��� ApproveInPriv.ShowStockList ʵ��
            return 1;
        }


        public bool Valid()
        {
            // TODO:  ��� ApproveInPriv.Valid ʵ��
            return true;
        }


        public int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex)
        {
            try
            {
                if (sv != null && delRowIndex >= 0)
                {
                    string[] keys = this.GetKey();

                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        this.ucInManager.Fp.StopCellEditing();

                        //�Ƴ����ݺ�
                        FS.HISFC.Models.Material.Input tempInput = this.hsInData[this.GetKey(dr)] as FS.HISFC.Models.Material.Input;
                        this.RemoveListNO(tempInput.InListNO);
                        //�����ʵ�弯�����Ƴ�
                        this.hsInData.Remove(this.GetKey(dr));
                        this.dt.Rows.Remove(dr);

                        this.ucInManager.Fp.StartCellEditing(null, false);
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

                this.hsListNO.Clear();
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
            if (this.ucInManager.FpSheetView != null)
            {
                if (this.ucInManager.FpSheetView.Rows.Count > 0)
                {
                    this.ucInManager.SetFpFocus();

                    this.ucInManager.FpSheetView.ActiveRowIndex = this.ucInManager.FpSheetView.Rows.Count - 1;
                    if (this.isPIDept)              //�ֿ�
                    {
                        this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                    }
                    else
                    {
                        this.ucInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColIsApprove;
                    }
                }
                else
                {
                    this.ucInManager.SetFocus();
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
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].CellType = numberCellType;
            this.ucInManager.FpSheetView.Columns[(int)ColumnSet.ColInCost].CellType = numberCellType;

            #region �ж��Ƿ�ѡ���˺�׼����

            bool isHaveCheck = false;
            bool isHaveUnCheck = false;
            foreach (DataRow dr in this.dt.Rows)
            {
                if (NConvert.ToBoolean(dr["��׼"]))
                    isHaveCheck = true;
                else
                    isHaveUnCheck = true;
            }

            if (!isHaveCheck)
            {
                MessageBox.Show("��ѡ�����׼����");
                return;
            }
            if (isHaveUnCheck)
            {
                DialogResult rs = MessageBox.Show("����δѡ������ ȷ�϶���Щ��Ʒ�����к�׼��?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return;
            }

            #endregion

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.HISFC.BizLogic.Material.Store store = new FS.HISFC.BizLogic.Material.Store();
            this.storeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //store.SetTrans(t.Trans);

            DateTime sysTime = this.storeManager.GetDateTimeFromSysDateTime();

            int serialNO = 0;
            string inListNO = "";

            foreach (DataRow dr in this.dt.Rows)
            {
                if (!NConvert.ToBoolean(dr["��׼"]))
                    continue;

                string key = this.GetKey(dr);

                FS.HISFC.Models.Material.Input input = this.hsInData[key] as FS.HISFC.Models.Material.Input;

                input.StoreBase.Operation.ApproveOper.OperTime = sysTime;                 //��׼����
                input.StoreBase.Operation.ApproveOper.ID = this.ucInManager.OperInfo.ID;  //��׼��
                input.StoreBase.Operation.Oper = input.StoreBase.Operation.ApproveOper;
                input.StoreBase.PriceCollection.PurchasePrice = NConvert.ToDecimal(dr["�����"].ToString());
                //{23D6A9A2-F151-4deb-8A07-9F0480B48D90}
                //input.StoreBase.PriceCollection.RetailPrice = input.StoreBase.PriceCollection.PurchasePrice;
                input.StoreBase.PriceCollection.RetailPrice = NConvert.ToDecimal(dr["���ۼ�"].ToString());
                input.InvoiceNO = dr["��Ʊ��"].ToString();
                input.ID = dr["��ˮ��"].ToString();
                input.StoreBase.StockNO = dr["������"].ToString();
                input.StoreBase.Item.PackPrice = input.StoreBase.PriceCollection.PurchasePrice * input.StoreBase.Item.PackQty;
                //input.OutNO = "";
                //input.PlanListNO = "";

                input.StoreBase.StockDept.Memo = this.ucInManager.DeptInfo.Memo;         //���������� PI�ֿ� P��������

                //���±��,�˴�������Ҫ�޸�
                if (this.isPIDept)
                {
                    if (this.storeManager.ApproveInputUpdate(input) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���� " + dr["��Ʒ����"].ToString() + " ʱ��������");
                        return;
                    }
                }
                else
                {
                    input.StoreBase.State = "2";
                    serialNO++;											//����˳���
                    input.StoreBase.SerialNO = serialNO;

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
                    input.StoreBase.Operation.Oper.Dept.ID = this.ucInManager.DeptInfo.ID;

                    #endregion

                    if (this.storeManager.ApproveInputDept(input, true) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���� " + dr["��Ʒ����"].ToString() + " ʱ��������");
                        return;
                    }
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�");

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

        public int Print()
        {
            // TODO:  ��� ApproveInPriv.Print ʵ��
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

            this.ucInManager.Fp.EditModeOff -= new EventHandler(Fp_EditModeOff);
        }

        #endregion

        #region ����

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
            {
                string[] keys = this.GetKey();

                DataRow dr = this.dt.Rows.Find(keys);
                if (dr != null)
                {
                    dr["������"] = NConvert.ToDecimal(dr["�������"]) * NConvert.ToDecimal(dr["�����"]);

                    dr.EndEdit();

                    this.CompuateSum();
                }
            }
        }

        private void value_FpKeyEvent(System.Windows.Forms.Keys key)
        {
            if (this.ucInManager.FpSheetView != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.ucInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColInvoiceNO)
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

        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            this.ShowSelectData();
        }

        private void ucListSelect_SelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            //			this.ucInManager.TargetDept = targetDept;

            this.Clear();

            this.AddOutDataByListNO(listCode);
        }

        #endregion

        #region ��ö��

        private enum ColumnSet
        {
            /// <summary>
            /// ��׼        0
            /// </summary>
            ColIsApprove,
            /// <summary>
            /// ��Ʒ����	0
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���		1
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ����		3
            /// </summary>
            ColBatchNO,
            /// <summary>
            /// ���ۼ�		2
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ��װ��λ	4
            /// </summary>
            ColPackUnit,
            /// <summary>
            /// �������	5
            /// </summary>
            ColInNum,
            /// <summary>
            /// �����		9
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// ������    10
            /// </summary>
            ColPurchaseCost,
            /// <summary>
            /// �����	6
            /// </summary>
            ColInCost,
            /// <summary>
            /// ��Ʊ��		7
            /// </summary>
            ColInvoiceNO,
            /// <summary>
            /// ��������	11
            /// </summary>
            ColProduceName,
            /// <summary>
            /// ��ע	    14
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��Ʒ����    15 
            /// </summary>
            ColDrugNO,
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
