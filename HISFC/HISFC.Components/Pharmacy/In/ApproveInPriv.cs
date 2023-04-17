using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Data;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Pharmacy.In
{
    /*
     * ҩ���׼ʱ����Ҫ��ʾ���ⵥ�б� Ӧ��ʾ��ⵥ�б� ͨ����ⵥ��׼ 
     * ҩ����׼ʱ��ʾ���ⵥ�б� 
     * 
     * ��DataTable���������ʱ��׼Ĭ��False 
     * 
     * DataTable�������洢�����ˮ��(ҩ���׼)�������ˮ��(ҩ����׼)
     * Input.User03 �洢��ˮ��
     * 
     * ��ʾ���ⵥ�б�ʱ �����û�ѡ��������ų��ⵥ�ϵ�����Ӧ��ʾ.. Ŀǰͨ��hsListNO����
     * 
    **/
    /// <summary>
    /// [��������: ��׼���ҵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public class ApproveInPriv : IPhaInManager
    {
        public ApproveInPriv(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.SetPhaManagerProperty(ucPhaManager);
        }

        #region �����

        private FS.HISFC.Components.Pharmacy.In.ucPhaIn phaInManager;

        private System.Data.DataTable dt = null;

        /// <summary>
        /// �������������Ƿ�Ϊҩ��
        /// </summary>
        private bool isPIDept = true;

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �洢��������
        /// </summary>
        private System.Collections.Hashtable hsInData = new Hashtable();

        /// <summary>
        /// ��׼ʱ�Ƿ������޸ķ�Ʊ��/�����
        /// </summary>
        private bool isApproveEdit = true;

        /// <summary>
        /// ������Ϣ ҩ���׼�洢���ݺ� ҩ����׼�洢ҩƷ����+���ݺ� ��ʾ�����ŵ����Ƿ�ͬʱ��׼
        /// </summary>
        private System.Collections.Hashtable hsListNO = new Hashtable();

        /// <summary>
        /// ����ѡ��ؼ�
        /// </summary>
        private ucPhaListSelect ucListSelect = null;

        #endregion

        /// <summary>
        /// �������ؼ�����
        /// </summary>
        private void SetPhaManagerProperty(FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {       
            this.phaInManager = ucPhaManager;

            //���ý�����ʾ
            this.phaInManager.IsShowInputPanel = false;
            this.phaInManager.IsShowItemSelectpanel = true;
            //����Ŀ�굥λѡ�� ���ù�������ť״̬
            if (this.phaInManager.DeptInfo.Memo == "PI")
            {
                this.isPIDept = true;
                this.phaInManager.SetTargetDept(true, false, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);

                this.phaInManager.SetToolBarButton(false, false, false, false, true);

                this.phaInManager.SetToolBarButtonVisible(false, false, false, false, true, true, false);
            }
            else
            {
                this.isPIDept = false;
                this.phaInManager.SetTargetDept(false, true, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);

                this.phaInManager.SetToolBarButton(false, false, true, false, true);

                this.phaInManager.SetToolBarButtonVisible(false, false, true, false, true,true,false);
            }
            //��ʾѡ����Ϣ
            if (this.phaInManager.TargetDept.ID != "")
            {
                this.ShowSelectData();
            }
            this.phaInManager.ShowInfo = "��ⵥ:";
            //������Ŀ�б���
            this.phaInManager.SetItemListWidth(2);

            this.phaInManager.Fp.EditModeReplace = true;
            this.phaInManager.FpSheetView.DataAutoSizeColumns = false;
           
            this.phaInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);
            this.phaInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

            this.phaInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);
            this.phaInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

            this.phaInManager.Fp.EditModeOff += new EventHandler(Fp_EditModeOff);

            System.EventHandler eFun = new EventHandler(this.ChangeCheck);
            this.phaInManager.AddToolBarButton("ѡ��", "��δѡ�����ݽ��з���ѡ��", FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�, 0, true, eFun);            
        }

        /// <summary>
        /// ���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeCheck(object sender, System.EventArgs args)
        {
            this.phaInManager.Fp.StopCellEditing();//{B98F0F85-A2D6-420d-9668-06D7AE79E092}
            foreach (DataRow dr in this.dt.Rows)
            {
                dr.EndEdit();
                dr["��׼"] = !NConvert.ToBoolean(dr["��׼"]);
                dr.EndEdit();               
            }
        }

        /// <summary>
        /// ����Fp��ʾ
        /// </summary>
        private void SetFormat()
        {
            this.phaInManager.FpSheetView.DefaultStyle.Locked = true;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Width = 38F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColSpecs].Width = 70F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].Width = 60F;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;            
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Width = 70F;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColBatchNO].Visible = true;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceType].Visible = false;      //��Ʊ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColProduceName].Visible = false;      //��������
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColDrugNO].Visible = false;           //ҩƷ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColKey].Visible = false;               //����
            
            //���ݵ�ǰ�����Ƿ�Ϊҩ�����÷�Ʊ�š�����۵���ʾ
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Visible = this.isPIDept;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Visible = this.isPIDept;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchaseCost].Visible = this.isPIDept;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Locked = false;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColMemo].Width = this.isPIDept?100F:250F;

            if (this.isApproveEdit)
            {
                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].Locked = false;
                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].Locked = false;
                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].Locked = false;

                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColIsApprove].BackColor = System.Drawing.Color.SeaShell;
                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColInvoiceNO].BackColor = System.Drawing.Color.SeaShell;
                this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColPurchasePrice].BackColor = System.Drawing.Color.SeaShell;
            }
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyNum].Visible = true;

        }

        /// <summary>
        /// ��DataTable�������������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int AddDataToTable(FS.HISFC.Models.Pharmacy.Input input)
        {
            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                input.RetailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                input.PurchaseCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.PurchasePrice;

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
                                                isApprove,                                  //��׼
                                                input.Item.Name,                            //��Ʒ����
                                                input.Item.Specs,                           //���
                                                input.BatchNO,                              //����
                                                input.Item.PriceCollection.RetailPrice,     //���ۼ�                                                
                                                input.Item.PackUnit,                        //��װ��λ
                                                input.Quantity / input.Item.PackQty,        //�������
                                                input.RetailCost,                           //�����                                                
                                                input.InvoiceNO,                            //��Ʊ��
                                                input.InvoiceType,                          //��Ʊ���
                                                input.Item.PriceCollection.PurchasePrice,   //�����
                                                input.PurchaseCost,                         //������
                                                input.Item.Product.Producer.Name,           //��������
                                                input.Operation.ApplyQty/input.Item.PackQty ,//��������
                                                input.Memo,                                 //��ע
                                                input.Item.ID,                              //ҩƷ����                    //��������
                                                input.Item.NameCollection.SpellCode,        //ƴ����
                                                input.Item.NameCollection.WBCode,           //�����
                                                input.Item.NameCollection.UserCode,         //�Զ�����
                                                input.User03                            
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
        /// ����ѡ������
        /// </summary>
        private void ShowSelectData()
        {
            string targetNO = this.phaInManager.TargetDept.ID;
            if (targetNO == "" || targetNO == null)
                targetNO = "AAAA";

            if (this.isPIDept)                              //ҩ��
            {
                string[] filterStr = new string[1] { "INVOICE_NO" };
                string[] label = new string[] { "��Ʊ��", "������λ����", "������λ����" };
                int[] width = new int[] { 60, 60, 120 };
                bool[] visible = new bool[] { true, false, true };

                this.phaInManager.SetSelectData("3",false,new string[] { "Pharmacy.Item.GetInputInvoiceList" }, filterStr, this.phaInManager.DeptInfo.ID, "1", targetNO);

                this.phaInManager.SetSelectFormat(label, width, visible);
            }
            else
            {

                string[] filterStr = new string[3] { "SPELL_CODE", "WB_CODE", "TRADE_NAME" };
                string[] label = new string[] { "������ˮ��", "���ⵥ�ݺ�", "ҩƷ����", "��Ʒ����", "���", "����", "��װ��λ", "��С��λ", "ƴ����", "�����" };
                int[] width = new int[] { 60, 60, 60, 120, 80, 60, 60, 60, 60, 60 };
                bool[] visible = new bool[] { false, false, false, true, true, true, false, true, false, false };

                this.phaInManager.SetSelectData("3", false,new string[] { "Pharmacy.Item.GetOutputInfoForInput" }, filterStr, this.phaInManager.DeptInfo.ID, "A", "1", targetNO);

                this.phaInManager.SetSelectFormat(label, width, visible);

                /* ��ȡ��Ϊ δ�˿����� 
                 * SELECT  T.OUT_BILL_CODE,
				        T.OUT_LIST_CODE,
				        T.DRUG_CODE,
				        T.TRADE_NAME,
				        T.SPECS,
				        T.OUT_NUM - T.RETURN_NUM,
				        T.PACK_UNIT,
				        T.MIN_UNIT,
				        S.SPELL_CODE,
				        S.WB_CODE
				FROM    PHA_COM_OUTPUT T,PHA_COM_BASEINFO S
				WHERE   T.PARENT_CODE =  fun_get_parentcode 
				AND     T.CURRENT_CODE =  fun_get_currentcode 
				AND     T.PARENT_CODE = S.PARENT_CODE
				AND     T.CURRENT_CODE = S.CURRENT_CODE
				AND     T.DRUG_CODE = S.DRUG_CODE
				AND     (T.CLASS3_MEANING_CODE = '{1}' OR '{1}' = 'A')
				AND     T.OUT_STATE = '{2}'
				AND     T.DRUG_STORAGE_CODE = '{0}'
				AND	    (T.DRUG_DEPT_CODE = '{3}' OR '{3}' = 'AAAA')
                AND     T.OUT_NUM - T.RETURN_NUM > 0
                */
            }
        }

        /// <summary>
        /// Ŀ�굥λ��Ϣ���
        /// </summary>
        /// <param name="targetNO">Ŀ�굥λ����</param>
        private int FillTargetInfo(string targetNO)
        {
            if (this.isPIDept)          //ҩ��
            {
                FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
                FS.HISFC.Models.Pharmacy.Company company = phaConsManager.QueryCompanyByCompanyID(targetNO);
                if (company == null)
                {
                    MessageBox.Show(Language.Msg("�޷���ȡ����������λ��Ϣ"));
                    return -1;
                }

                this.phaInManager.TargetDept = company;
                this.phaInManager.TargetDept.Memo = "1";
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(targetNO);
                if (dept == null)
                {
                    MessageBox.Show(Language.Msg("�޷���ȡ������¼���������Ϣ��"));
                    return -1;
                }

                this.phaInManager.TargetDept = dept;
                this.phaInManager.TargetDept.Memo = "0";
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

            if (this.phaInManager.ShowInfo == "")
            {
                this.phaInManager.ShowInfo = "��ⵥ:" + listNO;
            }
            else
            {
                if (this.phaInManager.ShowInfo == "��ⵥ:")
                {
                    this.phaInManager.ShowInfo = "��ⵥ:" + listNO;
                }
                else
                {
                    this.phaInManager.ShowInfo = this.phaInManager.ShowInfo + "��" + listNO;
                }
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
                    this.phaInManager.ShowInfo = "";
                    foreach (string strListNO in this.hsListNO.Keys)
                    {
                        if (this.phaInManager.ShowInfo == "")
                        {
                            this.phaInManager.ShowInfo = "��ⵥ��: " + strListNO;
                        }
                        else
                        {
                            this.phaInManager.ShowInfo = this.phaInManager.ShowInfo + " �� " + strListNO;
                        }
                    }
                }
                else
                {
                    this.hsListNO[listNO] = iCount - 1;
                }
            }
        }

        #region ҩ���׼���غ��� 

        /// <summary>
        /// ���ݷ�Ʊ����Ӵ���׼����
        /// </summary>
        /// <param name="invoiceNO"></param>
        private int AddInDataByInvoiceNO(string invoiceNO)
        {
            ArrayList alDetail = this.itemManager.QueryInputInfoByInvoice(this.phaInManager.DeptInfo.ID, invoiceNO, "1");
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return -1;
            }
            
            //this.phaInManager.ShowInfo = "��ⵥ�ݺ�";

            foreach (FS.HISFC.Models.Pharmacy.Input input in alDetail)
            {
                //��������
                input.User03 = input.ID;

                //�ж��Ƿ��ظ�����
                if (this.hsInData.ContainsKey(this.GetKey(input)))
                {
                    MessageBox.Show(Language.Msg("�÷�Ʊ�Ѽ���ѡ��!"));
                    return 0;
                }

                if (!hsListNO.ContainsKey(input.InListNO))
                {
                    this.AddListNO(input.InListNO);
                }
                //����Ŀ�굥λ��Ϣ
                if (this.phaInManager.TargetDept.ID == "")
                {
                    this.FillTargetInfo(input.TargetDept.ID);
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

        #region ҩ����׼���ݼ��غ���

        /// <summary>
        /// ���ݳ�����ˮ�Ż�ȡ����׼����
        /// </summary>
        /// <param name="outNO">������ˮ��</param>
        /// <returns></returns>
        private int AddOutDataByOutNO(string outNO)
        {
            ArrayList alDetail = this.itemManager.QueryOutputList(outNO);
            if (alDetail == null || alDetail.Count <= 0)
            {
                MessageBox.Show(Language.Msg("���ݳ�����ˮ�Ż�ȡ�������ݷ�������"));
                return -1;
            }

            foreach (FS.HISFC.Models.Pharmacy.Output output in alDetail)
            {
                FS.HISFC.Models.Pharmacy.Input input = this.InputConvert(output);

                if (this.hsInData.ContainsKey(this.GetKey(input)))
                {
                    MessageBox.Show(Language.Msg("�������Ѽ���ѡ��!"));
                    return 0;
                }

                //�Ƿ��Ѱ����õ���
                if (!this.hsListNO.ContainsKey(input.OutListNO))
                {
                    if (this.hsListNO.Count > 0)
                    {
                        string msg = Language.Msg(string.Format("��ҩƷ��ⵥ�ݺ��뵱ǰ��ⵥ�ݺŲ�ͬ ��ҩƷ��ⵥ��{0} ȷ�ϼ�����?", input.OutListNO));
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
                    if (this.hsListNO.ContainsKey(input.OutListNO))
                    {
                        this.hsListNO[input.OutListNO] = ((int)this.hsListNO[input.OutListNO]) + 1;
                    }
                    else
                    {                      
                        this.AddListNO(input.OutListNO);
                    }
                }
            }

            this.SetFormat();

            return 1;
        }        

        /// <summary>
        /// ��Ӵ���׼�ĳ�������
        /// </summary>
        /// <param name="outListNO">���ⵥ�ݺ�</param>
        private void AddOutDataByListNO(string outListNO)
        {
            ArrayList alDetail = this.itemManager.QueryOutputInfo(this.phaInManager.TargetDept.ID, outListNO, "1");
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������׼���ݷ�������" + this.itemManager.Err));
                return;
            }
            if (alDetail.Count == 0)
            {
                MessageBox.Show(Language.Msg("�õ��ݿ����ѱ���׼"));
                return;
            }

            //������Fp���г�ʼ�� ��߼����ٶ�
            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.Output output in alDetail)
            {
                //��Ϊ��ļ�¼�����д���
                if (output.Quantity == 0)
                {
                    continue;
                }
                //���˿��������ڳ��������� �����д��� �ü�¼��ȫ������
                if (output.Quantity == output.Operation.ReturnQty)
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.Input input = this.InputConvert(output);

                if (this.AddDataToTable(input) == 1)
                {
                    this.hsInData.Add(this.GetKey(input), input);
                }
                else
                {
                    ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();
                    MessageBox.Show(Language.Msg("���س���ʵ����Ϣʱ��������"));
                    return;
                }
            }

            if (this.phaInManager.FpSheetView.Rows.Count == 0)
            {
                ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();
                MessageBox.Show(Language.Msg("�õ����ڲ�������Ч�Ĵ���׼��¼ ҩƷ�����˿���׼���"));
                return;
            }

            //���浱ǰ������Ϣ
            this.hsListNO.Add(outListNO, alDetail.Count);

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();

            return;
        }

        /// <summary>
        /// ���ʵ�帳ֵ
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Pharmacy.Input InputConvert(FS.HISFC.Models.Pharmacy.Output output)
        {
            FS.HISFC.Models.Pharmacy.Input input = new FS.HISFC.Models.Pharmacy.Input();

            #region Input ��Ϣ���

            //����Ŀ�굥λ��Ϣ
            if (this.phaInManager.TargetDept.ID == "")
            {
                this.FillTargetInfo(input.TargetDept.ID);
            }

            input.StockDept = this.phaInManager.DeptInfo;                   //�������
            input.PrivType = this.phaInManager.PrivType.ID;                 //������
            input.SystemType = this.phaInManager.PrivType.Memo;             //ϵͳ����
            input.State = "2";                                              //״̬ ��׼
            input.Company = this.phaInManager.TargetDept;
            input.TargetDept = this.phaInManager.TargetDept;                //Ŀ�굥
            input.Item = output.Item;                                       //ҩƷʵ����Ϣ
            input.OutBillNO = output.ID;                                    //������ˮ��
            input.OutListNO = output.OutListNO;                             //���ⵥ�ݺ�
            input.OutSerialNO = output.SerialNO;                            //���
            input.SerialNO = output.SerialNO;
            input.BatchNO = output.BatchNO;                                 //����
            input.ValidTime = output.ValidTime;                             //��Ч��
            input.Quantity = output.Quantity;                               //����
            input.PlaceNO = output.PlaceNO;                                 //��λ��
            input.GroupNO = output.GroupNO;                                 //����
            input.Operation = output.Operation;                             //������Ϣ

            #endregion

            //�洢������Ϣ
            //������������ �����γ���ʱ ͬһҩƷ��ͬ���� ��ˮ����ͬ ��������Ų�ͬ
            input.User03 = output.ID + output.SerialNO;

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
                this.phaInManager.TotCostInfo = string.Format("�����ܽ��:{0} �����ܽ��:{1}", retailCost.ToString("N"), purchaseCost.ToString("N"));
            }
            else
            {
                this.phaInManager.TotCostInfo = string.Format("�����ܽ��:{0}", retailCost.ToString("N"));
            }
        }

        #region ��ֵ��ȡ

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Pharmacy.Input input)
        {
            //return input.Item.ID + input.BatchNO + input.InvoiceNO;
            return input.User03;
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            //return dr["ҩƷ����"].ToString() + dr["����"].ToString() + dr["��Ʊ��"].ToString();
            return dr["����"].ToString();
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <returns></returns>
        private string[] GetKey()
        {
            //string[] keys = new string[]{
            //                                    this.phaInManager.FpSheetView.Cells[this.phaInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text,
            //                                    this.phaInManager.FpSheetView.Cells[this.phaInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColBatchNO].Text,
            //                                    this.phaInManager.FpSheetView.Cells[this.phaInManager.FpSheetView.ActiveRowIndex,(int)ColumnSet.ColInvoiceNO].Text
            //                                };

            string[] keys = new string[]{
                                                this.phaInManager.FpSheetView.Cells[this.phaInManager.FpSheetView.ActiveRowIndex, (int)ColumnSet.ColKey].Text                                              
                                            };

            return keys;
        }

        #endregion

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
                                                                    new DataColumn("��׼",      dtBol),
                                                                    new DataColumn("��Ʒ����",  dtStr),
                                                                    new DataColumn("���",      dtStr),
                                                                    new DataColumn("����",      dtStr),
                                                                    new DataColumn("���ۼ�",    dtDec),
                                                                    new DataColumn("��װ��λ",  dtStr),
                                                                    new DataColumn("�������",  dtDec),
                                                                    new DataColumn("�����",  dtDec),                                                                    
                                                                    new DataColumn("��Ʊ��",    dtStr),
                                                                    new DataColumn("��Ʊ����",  dtStr),
                                                                    new DataColumn("�����",    dtDec),
                                                                    new DataColumn("������",  dtDec),
                                                                    new DataColumn("��������",  dtStr),
                                                                    new DataColumn("��������",  dtDec),
                                                                    new DataColumn("��ע",      dtStr),
                                                                    new DataColumn("ҩƷ����",  dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr),
                                                                    new DataColumn("����",    dtStr)
                                                                   }
                                  );
            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            //DataColumn[] keys = new DataColumn[3];

            //keys[0] = this.dt.Columns["ҩƷ����"];
            //keys[1] = this.dt.Columns["����"];
            //keys[2] = this.dt.Columns["��Ʊ��"];

            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dt.Columns["����"];

            this.dt.PrimaryKey = keys;

            return this.dt;
        }

        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string dataNO = sv.Cells[activeRow, 0].Text;
            if (this.isPIDept)              //ҩ��
            {
                if (this.AddInDataByInvoiceNO(dataNO) == 1)
                {
                    this.SetFocusSelect();
                }
                return 1;
            }
            else                            //ҩ��
            {
                if (this.AddOutDataByOutNO(dataNO) == 1)
                {
                    this.SetFocusSelect();
                }
                return 1;
            }
        }

        public int ShowApplyList()
        {
            return 1;
        }

        public int ShowInList()
        {
            return 1;
        }

        public int ShowOutList()
        {
            if (this.ucListSelect == null)
                this.ucListSelect = new ucPhaListSelect();

            this.ucListSelect.Init();
            this.ucListSelect.DeptInfo = this.phaInManager.DeptInfo;

            System.Collections.Hashtable hsInOutState = new Hashtable();
            hsInOutState.Add("1", "����");
            this.ucListSelect.InOutStateCollection = hsInOutState;
            this.ucListSelect.State = "1";                  //�����״̬
            this.ucListSelect.Class2Priv = "0320";          //����
            this.ucListSelect.PrivType = this.phaInManager.PrivType;

            this.ucListSelect.SelecctListEvent -= new ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);
            this.ucListSelect.SelecctListEvent += new ucIMAListSelecct.SelectListHandler(ucListSelect_SelecctListEvent);

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucListSelect);

            #region ����ԭ���ڵ�����ʽ

            //string targetNO = "AAAA";
            //if (this.phaInManager.TargetDept.ID != "" && this.phaInManager.TargetDept.ID != null)
            //{
            //    targetNO = this.phaInManager.TargetDept.ID;
            //}
            ////ȡ����׼��¼ �����¼״̬Ϊ"1"��
            //ArrayList alList = this.itemManager.QueryOutputListForApproveInput(targetNO, this.phaInManager.DeptInfo.ID, "A");
            //if (alList == null)
            //{
            //    MessageBox.Show(Language.Msg("��ȡ���ⵥ�����ݳ���" + this.itemManager.Err));
            //    return -1;
            //}

            ////��������ѡ�񵥾�
            //FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            //string[] fpLabel = { "��ⵥ��", "������λ" };
            //float[] fpWidth = { 120F, 120F };
            //bool[] fpVisible = { true, true, false, false, false, false };

            //if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alList, ref selectObj) == 1)
            //{
            //    this.Clear();

            //    FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

            //    targeDept.ID = selectObj.Memo;              //������ұ���
            //    targeDept.Name = selectObj.Name;            //�����������
            //    targeDept.Memo = "0";                       //Ŀ�굥λ���� �ڲ�����

            //    if (this.phaInManager.TargetDept.ID != targeDept.ID)
            //    {
            //        this.phaInManager.TargetDept = targeDept;
            //        this.ShowSelectData();
            //    }

            //    this.AddOutDataByListNO(selectObj.ID);

            //    this.SetFocusSelect();

            //    if (this.phaInManager.FpSheetView != null)
            //    {
            //        this.phaInManager.FpSheetView.ActiveRowIndex = 0;
            //    }
            //}

            #endregion

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
                        this.phaInManager.Fp.StopCellEditing();

                        //�Ƴ����ݺ�
                        FS.HISFC.Models.Pharmacy.Input tempInput = this.hsInData[this.GetKey(dr)] as FS.HISFC.Models.Pharmacy.Input;
                        this.RemoveListNO(tempInput.OutListNO);
                        //�����ʵ�弯�����Ƴ�
                        this.hsInData.Remove(this.GetKey(dr));
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

                this.hsListNO.Clear();
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
                    if (this.isPIDept)              //ҩ��
                    {
                        this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColInvoiceNO;
                    }
                    else
                    {
                        this.phaInManager.FpSheetView.ActiveColumnIndex = (int)ColumnSet.ColIsApprove;
                    }
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
                MessageBox.Show(Language.Msg("��ѡ�����׼����"));
                return;
            }
            if (isHaveUnCheck)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("����δѡ������ ȷ�϶���ЩҩƷ�����к�׼��?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                    return;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //phaIntegrate.SetTrans(t.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            string inListNO = "";

            foreach (DataRow dr in this.dt.Rows)
            {
                if (!NConvert.ToBoolean(dr["��׼"]))
                    continue;

                string key = this.GetKey(dr);

                //{7F9E7287-5803-4b42-9CFD-61A17FF1A5D4}  ��Hash���ȡ����ʱ�����Clone����
                FS.HISFC.Models.Pharmacy.Input input = (this.hsInData[key] as FS.HISFC.Models.Pharmacy.Input).Clone();

                input.Operation.ApproveOper.OperTime = sysTime;                 //��׼����
                input.Operation.ApproveOper.ID = this.phaInManager.OperInfo.ID; //��׼��
                input.Operation.Oper = input.Operation.ApproveOper;

                if (input.ID == "" || input.InListNO == "" || input.GroupNO == 0)
                {
                    #region ҩ������׼ʱ ������¼ 

                    if (inListNO == "" && (input.InListNO == "" || input.InListNO == null))
                    {
                        #region ��ȡ����ⵥ��

                        if (input.OutListNO != "")
                        {
                            inListNO = input.OutListNO;
                        }
                        else
                        {
                            // //{59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ������ⵥ�Ż�ȡ��ʽ
                            inListNO = phaIntegrate.GetInOutListNO(this.phaInManager.DeptInfo.ID, true);
                            if (inListNO == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("��ȡ������ⵥ�ų���" + itemManager.Err));
                                return;
                            }                      
                        }

                        input.InListNO = inListNO;
                        #endregion
                    }
                    else
                    {
                        input.InListNO = inListNO;
                    }

                    decimal storageQty = 0;
                    if (this.itemManager.GetStorageNum(this.phaInManager.DeptInfo.ID, input.Item.ID, out storageQty) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("��ȡ�������ʱ����" + this.itemManager.Err));
                        return;
                    }

                    input.StoreQty = storageQty + input.Quantity;
                    input.StoreCost = input.StoreQty / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;

                    input.Operation.ApplyOper = input.Operation.ApproveOper.Clone();
                    #endregion
                }

                if (this.isApproveEdit)
                {
                    input.InvoiceNO = dr["��Ʊ��"].ToString().Trim();
                    input.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(dr["�����"]);
                }

                input.StockDept.Memo = this.phaInManager.DeptInfo.Memo;         //���������� PIҩ�� Pҩ��

                //���¿���� ��ҩ�ⲻ���и��� ��ҩ�����¿��
                if (this.itemManager.ApproveInput(input,input.StockDept.Memo == "PI"?"0":"1") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���� " + dr["��Ʒ����"].ToString() + " ʱ��������") + this.itemManager.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

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

        private void Fp_EditModeOff(object sender, EventArgs e)
        {
            if (this.phaInManager.FpSheetView.ActiveColumnIndex == (int)ColumnSet.ColPurchasePrice)
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

        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            this.ShowSelectData();
        }

        private void ucListSelect_SelecctListEvent(string listCode, string state, FS.FrameWork.Models.NeuObject targetDept)
        {
            targetDept.Memo = "0";            //Ŀ�굥λ���� �ڲ�����

            if (this.phaInManager.TargetDept.ID != targetDept.ID)
            {
                this.phaInManager.TargetDept = targetDept;
                this.ShowSelectData();
            }

            this.Clear();

            this.AddOutDataByListNO(listCode);

            this.SetFocusSelect();

            if (this.phaInManager.FpSheetView != null)
            {
                this.phaInManager.FpSheetView.ActiveRowIndex = 0;
            }
        }


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
            /// ��������
            /// </summary>
            ColApplyNum,
            /// <summary>
            /// ��ע	    14
            /// </summary>
            ColMemo,
            
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
            ColKey
        }        
    }
}
