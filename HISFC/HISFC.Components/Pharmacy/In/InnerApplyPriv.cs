using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: �ڲ��������ҵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// �ڲ�����˿����� �������ⵥ��ˮ��
    /// 
    /// Pharmacy.Item.DeleteApplyOut
    /// Pharmacy.Item.UpdateApplyOutNum
    /// </summary>
    public class InnerApplyPriv : IPhaInManager
    {
        /// <summary>
        /// �������
        /// 
        /// ʣ����ݾ������Զ�����������δ�� 
        /// ����ģ������������δ��
        /// </summary>
        /// <param name="isBackIn">True �˿����� False �����������</param>
        /// <param name="ucPhaManager"></param>
        public InnerApplyPriv(bool isBackIn, FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            this.isBack = isBackIn;

            this.listNO = "";

            this.SetPhaManagerProperty(ucPhaManager);
        }

        #region �����

        /// <summary>
        /// �Ƿ��˿�����
        /// </summary>
        private bool isBack = false;

        private ucPhaIn phaInManager = null;

        private FarPoint.Win.Spread.SheetView svTemp = null;

        private DataTable dt = null;

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �洢����ӵ���������
        /// </summary>
        private System.Collections.Hashtable hsApplyData = new Hashtable();

        /// <summary>
        /// ���ε������뵥��
        /// </summary>
        private string listNO = "";

        private FarPoint.Win.Spread.CellType.NumberCellType numPriceCell = null;

        private FarPoint.Win.Spread.CellType.NumberCellType numQtyCell = null;

        /// <summary>
        /// �Ƿ�����ݴ����ݲ���
        /// 
        ///  {37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�
        /// </summary>
        private bool isTemporaryFun = false;

        /// <summary>
        ///  �洢����ӡ���� {0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636}
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

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
                this.phaInManager.IsShowInputPanel = false;
                //Ŀ���������
                this.phaInManager.SetTargetDept(false, true, FS.HISFC.Models.IMA.EnumModuelType.Phamacy, FS.HISFC.Models.Base.EnumDepartmentType.P);
                //FpSheetView
                this.svTemp = this.phaInManager.FpSheetView;

                //������ʾ����
                if (this.phaInManager.TargetDept.ID != "")
                {
                    this.ShowSelectData();
                }
                //���ù�������ť��ʾ
                this.phaInManager.SetToolBarButton(true, false, false, false, true);
                this.phaInManager.SetToolBarButtonVisible(true, false, false, false, true, true, false);
                //������Ϣ��ʾ
                this.phaInManager.ShowInfo = "";
                //Fp ����
                this.phaInManager.FpSheetView.DataAutoSizeColumns = false;
                this.phaInManager.Fp.EditModeReplace = true;

                this.phaInManager.EndTargetChanged -= new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);
                this.phaInManager.EndTargetChanged += new ucIMAInOutBase.DataChangedHandler(value_EndTargetChanged);

                this.phaInManager.FpKeyEvent -= new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);
                this.phaInManager.FpKeyEvent += new ucIMAInOutBase.FpKeyHandler(value_FpKeyEvent);

                this.phaInManager.FpSheetView.DataAutoSizeColumns = false;
                this.phaInManager.FpSheetView.DataAutoCellTypes = false;
                this.SetFormat();

                if (!isBack)
                {
                    System.EventHandler eFun = new EventHandler(NumAlterHandler);

                    //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
                    this.phaInManager.AddToolBarButton("������", "���ݿ���������Զ���������", FS.FrameWork.WinForms.Classes.EnumImageList.B����, 2, false, eFun);

                    System.EventHandler eOutFun = new EventHandler(OutAlterHandler);

                    this.phaInManager.AddToolBarButton("������", "��������������Զ���������", FS.FrameWork.WinForms.Classes.EnumImageList.F�ֽ�, 3, false, eOutFun);

                    System.EventHandler stencilFun = new EventHandler(StencilHandler);

                    this.phaInManager.AddToolBarButton("ģ��", "��������ģ���γ�����ƻ�", FS.FrameWork.WinForms.Classes.EnumImageList.F����, 4, false, stencilFun);

                    //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
                    System.EventHandler temporaryListFun = new EventHandler(TemporaryListHandler);
                    this.phaInManager.AddToolBarButton("�ݴ浥", "��¼����ݴ����뵥�б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, 5, true, temporaryListFun);

                    //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
                    System.EventHandler temporarySaveFun = new EventHandler(TemporarySaveHandler);
                    this.phaInManager.AddToolBarButton("�ݴ�", "�Ե�ǰ��¼������뵥���ݽ����ݴ�", FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�, 7, false, temporarySaveFun);

                    //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
                    System.EventHandler clearFun = new EventHandler(ClearHandler);
                    this.phaInManager.AddToolBarButton("���", "���ݳ�ʼ�� �����¼����Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Q���,9, true, clearFun);
                }

                this.InitConfig();
            }
        }

        /// <summary>
        /// ��ʼ�������ļ�
        /// </summary>
        private void InitConfig()
        {
            HISFC.Components.Pharmacy.Function fun = new Function();
            System.Xml.XmlDocument doc = fun.GetConfig();

            if (doc != null)
            {
                System.Xml.XmlNode valueNode = doc.SelectSingleNode("/Setting/Group[@ID='Pharmacy']/Fun[@ID='InnerApply']");
                if (valueNode != null)
                {
                    bool isShowStock = NConvert.ToBoolean(valueNode.Attributes["IsShowStock"].Value);

                    this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColStoreQty].Visible = isShowStock;
                }
            }
        }

        /// <summary>
        /// �������
        /// 
        /// {37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void ClearHandler(object sender, System.EventArgs args)
        {
            this.Clear();
        }

        /// <summary>
        /// �����ݴ�
        /// 
        /// {37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void TemporarySaveHandler(object sender, System.EventArgs args)
        {
            this.isTemporaryFun = true;

            this.Save();            

            this.isTemporaryFun = false;
        }

        /// <summary>
        /// �ݴ����뵥�б�
        /// 
        /// {37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void TemporaryListHandler(object sender, System.EventArgs args)
        {
            this.isTemporaryFun = true;

            this.ShowApplyList();

            this.isTemporaryFun = false;
        }

        /// <summary>
        /// Handler����ί�о�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void NumAlterHandler(object sender, System.EventArgs args)
        {
            this.FindByAlter("0", this.phaInManager.DeptInfo.ID);
        }

        /// <summary>
        /// Handler����ί�о�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OutAlterHandler(object sender, System.EventArgs args)
        {
            this.FindByAlter("1", this.phaInManager.DeptInfo.ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void StencilHandler(object sender, System.EventArgs args)
        {
            this.AddStencilData();
        }

        /// <summary>
        /// ��DataTable����������
        /// </summary>
        /// <param name="applyOut">������Ϣ</param>
        /// <param name="dataSource">������Դ 0 ԭʼ���� 1 �������</param>
        /// <returns></returns>
        protected virtual int AddDataToTable(FS.HISFC.Models.Pharmacy.ApplyOut applyOut,string dataSource)
        {
            if (!Function.JudgePriceConsinstency(this.phaInManager.TargetDept.ID, applyOut.Item))
            {
                MessageBox.Show(Language.Msg("��ҩƷ�Ѿ��������ҵ��ۣ�����ֱ�ӽ���������롣������֪ͨҩ�⼴����ȫԺ����"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucDrugStoreQuery uc = new ucDrugStoreQuery(applyOut.Item.ID);
                using (uc)
                {
                    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = applyOut.Item.Name + " ȫԺ���ֲ�";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                }
                return -1;
            }

            if (this.dt == null)
            {
                this.InitDataTable();
            }

            try
            {
                decimal storeQty = 0;
                //{613A769A-C540-4a2c-949D-28B31F0BC482}
                decimal lowQty = 0;
                decimal topQty = 0;

                //{2418802A-9F8E-4390-AEBA-700C984820A0}�����������һ�в���ʾʱ,û��ȡ��ҩƷ���,����˿�����ʱ����治��
                //if (this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColStoreQty].Visible)
                //{
                   // this.itemManager.GetStorageNum(this.phaInManager.DeptInfo.ID, applyOut.Item.ID, out storeQty);
                    this.itemManager.GetStorageLowTopNum(this.phaInManager.DeptInfo.ID, applyOut.Item.ID, out storeQty, out lowQty, out topQty);
                //}
                //{2418802A-9F8E-4390-AEBA-700C984820A0}
                decimal cost = applyOut.Operation.ApplyQty / applyOut.Item.PackQty * applyOut.Item.PriceCollection.RetailPrice;
                //{613A769A-C540-4a2c-949D-28B31F0BC482}
                string lostRate = string.Empty;
                if (lowQty != 0)
                {
                    lostRate = string.Format("{0:P}", storeQty / lowQty);
                }

                    
                this.dt.Rows.Add(new object[] { 
                                                applyOut.Item.Name,                                     //��Ʒ����
                                                applyOut.Item.Specs,                                    //���
                                                applyOut.Item.PriceCollection.RetailPrice,              //���ۼ�
                                                applyOut.Item.PackUnit,                                 //��װ��λ
                                                applyOut.Item.MinUnit,                                 //��С��λ
                                                System.Math.Round(storeQty / applyOut.Item.PackQty,2),
                                                applyOut.Operation.ApplyQty / applyOut.Item.PackQty,    //��������                                                
                                                cost,                                                   //������
                                                lostRate,                                             //ȱʧ����{613A769A-C540-4a2c-949D-28B31F0BC482}
                                                applyOut.Memo,                                          //��ע
                                                applyOut.Item.ID,                                       //ҩƷ����
                                                applyOut.ID,                                            //��ˮ��
                                                dataSource,
                                                applyOut.Item.NameCollection.SpellCode,                 //ƴ����
                                                applyOut.Item.NameCollection.WBCode,                    //�����
                                                applyOut.Item.NameCollection.UserCode,                  //�Զ�����
                                                applyOut.Item.ID + applyOut.OutBillNO
                            
                                           }
                                    );
                
                this.dt.DefaultView.AllowDelete = true;
                this.dt.DefaultView.AllowEdit = true;
                this.dt.DefaultView.AllowNew = true;
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
        /// �������뵥��������������
        /// </summary>
        /// <param name="listNO"></param>
        /// <returns></returns>
        private int AddApplyData(string listNO)
        {
            //{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�
            string applyState = "0";
            if (this.isTemporaryFun)
            {
                applyState = "A";
            }
            ////{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�
            ArrayList alDetail = this.itemManager.QueryApplyOutInfoByListCode(this.phaInManager.DeptInfo.ID, listNO, applyState);
            if (alDetail == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("δ��ȷ��ȡ�ڲ����������Ϣ" + this.itemManager.Err));
                return -1;
            }

            this.Clear();
            //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636}    ���뵥��ӡ
            this.alPrintData.Clear();

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).BeginInit();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alDetail)
            {
                info.Item = this.itemManager.GetItem(info.Item.ID);
                if (info.Item == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡҩƷ������Ϣʧ��" + this.itemManager.Err));
                    return -1;
                }

                if (this.AddDataToTable(info, "0") == -1)
                    return -1;

                this.listNO = info.BillNO;

                this.hsApplyData.Add(this.GetKey(info), info);
            }

            this.dt.AcceptChanges();

            this.CompuateSum();

            this.SetFormat();

            ((System.ComponentModel.ISupportInitialize)(this.phaInManager.Fp)).EndInit();

            return 1;
        }

        /// <summary>
        /// ����ҩƷ�����������
        /// </summary>
        /// <param name="drugNO">ҩƷ����</param>
        /// <param name="outBillNO">������ˮ��</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO,string outBillNO,decimal applyQty)
        {
            //ȡҩƷ�ֵ���Ϣ
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugNO);
            if (item == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ������Ϣʧ��"));
                return -1;
            }

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

            applyOut.Item = item;

            if (outBillNO != null)
            {
                applyOut.OutBillNO = outBillNO;
            }

            if (this.hsApplyData.Contains( this.GetKey(applyOut)))
            {
                MessageBox.Show(Language.Msg("��ҩƷ�����"));
                return 0;
            }           

            applyOut.Days = 1;
            applyOut.ApplyDept = this.phaInManager.DeptInfo;        //�������
            applyOut.StockDept = this.phaInManager.TargetDept;      //������ (Ŀ�����)
            applyOut.State = "0";                                   //״̬ ����
            applyOut.SystemType = this.phaInManager.PrivType.Memo;
            applyOut.PrivType = this.phaInManager.PrivType.ID;

            if (applyQty != -1)
            {
                applyOut.Operation.ApplyQty = applyQty;
            }

            if (this.AddDataToTable(applyOut, "1") == 1)
            {
                this.hsApplyData.Add(this.GetKey(applyOut), applyOut);

                this.SetFormat();

                this.SetFocusSelect();

            }

            return 1;
        }

        /// <summary>
        /// ����ҩƷ�����������
        /// </summary>
        /// <param name="drugNO">ҩƷ����</param>
        /// <param name="outBillNO">������ˮ��</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO, string outBillNO)
        {
            return AddDrugData(drugNO, outBillNO, -1);
        }

        /// <summary>
        /// ����ҩƷ��������˿�����
        /// </summary>
        /// <param name="drugNO">ҩƷ����</param>
        /// <returns></returns>
        private int AddDrugData(string drugNO)
        {
            return this.AddDrugData(drugNO, null);
        }


        /// <summary>
        /// ģ��������ʾ
        /// </summary>
        public void AddStencilData()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("����ģ�����ɼƻ���Ϣ�������ǰ��ʾ������ �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return;

            this.Clear();

            ArrayList alOpenDetail = Function.ChooseDrugStencil(this.phaInManager.DeptInfo.ID, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Apply);

            if (alOpenDetail != null && alOpenDetail.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڸ�����ѡģ������������Ϣ..."));
                Application.DoEvents();

                int i = 0;
                foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alOpenDetail)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, alOpenDetail.Count);
                    Application.DoEvents();

                    this.AddDrugData(info.Item.ID);
                    i++;
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
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
                string[] label = new string[] { "������ˮ��", "���ⵥ�ݺ�", "ҩƷ����", "��Ʒ����", "���", "����", "��װ��λ", "��С��λ", "ƴ����", "�����" };
                int[] width = new int[] { 60, 60, 60, 120, 80, 60, 60, 60, 60, 60 };
                bool[] visible = new bool[] { false, false, false, true, true, true, false, true, false, false };

                this.phaInManager.SetSelectData("3", false,new string[] { "Pharmacy.Item.GetOutputInfoForInput" }, filterStr, this.phaInManager.DeptInfo.ID, "A", "2", this.phaInManager.TargetDept.ID);

                this.phaInManager.SetSelectFormat(label, width, visible);

                #region Sql���

                /*
                SELECT  T.OUT_BILL_CODE,
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

                #endregion
            }
            else
            {
                this.phaInManager.SetSelectData("1",Function.IsOutByBatchNO, null, null, null);
            }

            this.phaInManager.SetItemListWidth(2);

            return 1;
        }

        /// <summary>
        /// ��ʽ��Fp��ʾ
        /// </summary>
        private void SetFormat()
        {
            if (this.svTemp == null)
                return;

            this.svTemp.DefaultStyle.Locked = true;

            this.svTemp.Columns[(int)ColumnSet.ColTradeName].Width = 130F;
            this.svTemp.Columns[(int)ColumnSet.ColSpecs].Width = 80F;
            this.svTemp.Columns[(int)ColumnSet.ColRetailPrice].Width = 80F;
            this.svTemp.Columns[(int)ColumnSet.ColPackUnit].Width = 60F;
            this.svTemp.Columns[(int)ColumnSet.ColApplyCost].Width = 95F;

            this.svTemp.Columns[(int)ColumnSet.ColKey].Visible = false;              //����
            this.svTemp.Columns[(int)ColumnSet.ColDrugID].Visible = false;           //ҩƷ����
            this.svTemp.Columns[(int)ColumnSet.ColNO].Visible = false;               //��ˮ��
            this.svTemp.Columns[(int)ColumnSet.ColDataSource].Visible = false;       //������Դ
            this.svTemp.Columns[(int)ColumnSet.ColSpellCode].Visible = false;        //ƴ����
            this.svTemp.Columns[(int)ColumnSet.ColWBCode].Visible = false;           //�����
            this.svTemp.Columns[(int)ColumnSet.ColUserCode].Visible = false;         //�Զ�����

            this.svTemp.Columns[(int)ColumnSet.ColMemo].Width = 200F;
            this.svTemp.Columns[(int)ColumnSet.ColMemo].Locked = false;

            numPriceCell = new FarPoint.Win.Spread.CellType.NumberCellType();
            numPriceCell.DecimalPlaces = 4;
            numPriceCell.MinimumValue = 0;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColRetailPrice].CellType = this.numPriceCell;

            numQtyCell = new FarPoint.Win.Spread.CellType.NumberCellType();
            numQtyCell.DecimalPlaces = 2;
            numQtyCell.MinimumValue = 0;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].CellType = this.numQtyCell;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyCost].CellType = this.numQtyCell;

            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].Locked = false;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColApplyQty].BackColor = System.Drawing.Color.SeaShell;
            this.phaInManager.FpSheetView.Columns[(int)ColumnSet.ColLostRate].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
        }

        ///<summary>
        ///����ҩƷ�����߼�������
        ///</summary>
        ///<param name="alterFlag">���ɷ�ʽ 0 ������ 1 ������</param>
        ///<param name="deptCode">�ⷿ����</param>
        ///<returns>�ɹ�����0��ʧ�ܷ��أ�1</returns>
        public void FindByAlter(string alterFlag, string deptCode)
        {
            if (this.hsApplyData.Count > 0)
            {
                DialogResult result;
                result = MessageBox.Show("�����������ɽ������ǰ���ݣ��Ƿ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                if (result == DialogResult.No)
                    return;
            }

            try
            {
                this.Clear();

                ArrayList alDetail = new ArrayList();
                if (alterFlag == "1")
                {
                    #region �������� ���������Ĳ��� ������������Ϣ
                    using (HISFC.Components.Pharmacy.ucPhaAlter uc = new ucPhaAlter())
                    {
                        uc.DeptCode = deptCode;
                        uc.SetData();
                        uc.Focus();
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                        if (uc.ApplyInfo != null)
                        {
                            alDetail = uc.ApplyInfo;
                        }
                    }
                    #endregion
                }
                else
                {
                    //{F4D82F23-CCDC-45a6-86A1-95D41EF856B8} ���ĵ��ú���
                    alDetail = this.itemManager.QueryDrugListByNumAlter(deptCode);
                    if (alDetail == null)
                    {
                        MessageBox.Show(Language.Msg("��������������ִ����Ϣ����δ��ȷִ��\n" + this.itemManager.Err));
                        return;
                    }
                }

                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                foreach (FS.FrameWork.Models.NeuObject temp in alDetail)
                {
                    this.AddDrugData(temp.ID,null,NConvert.ToDecimal(temp.User03));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// ���ر��ŵ��ݲ��
        /// </summary>
        public virtual void CompuateSum()
        {
            decimal retailCost = 0;

            if (this.dt != null)
            {
                for (int i = 0; i < this.phaInManager.FpSheetView.Rows.Count; i++)
                {
                    retailCost += NConvert.ToDecimal(this.phaInManager.FpSheetView.Cells[i, (int)ColumnSet.ColApplyCost].Text);
                }

                this.phaInManager.TotCostInfo = string.Format("�����ܽ��:{0} ", retailCost.ToString("N"));
            }
        }

        /// <summary>
        /// �˿�����ʱ�ж��Ƿ����㹻
        /// </summary>
        /// <param name="drugCode">ҩƷ���� </param>
        /// <param name="applyQty">�����˿����� </param>
        /// <returns>����㹻����True ���򷵻�False</returns>
        private bool IsEnoughStore(string drugCode,decimal applyQty)
        {
            decimal storeQty = applyQty;

            this.itemManager.GetStorageNum(this.phaInManager.DeptInfo.ID, drugCode, out storeQty);

            if (storeQty < applyQty)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetKey(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            if (applyOut.OutBillNO == null)
            {
                applyOut.OutBillNO = "";
            }
            return applyOut.Item.ID + applyOut.OutBillNO;
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string GetKey(DataRow dr)
        {
            return dr["����"].ToString();
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="findIndex"></param>
        /// <returns></returns>
        private string[] GetFindKey(FarPoint.Win.Spread.SheetView sv, int findIndex)
        {
            return new string[] { sv.Cells[findIndex, (int)ColumnSet.ColKey].Text };
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
                                                                    new DataColumn("��װ��λ",  dtStr),
                                                                    new DataColumn("��С��λ",  dtStr),
                                                                    new DataColumn("���ƿ��",  dtDec),
                                                                    new DataColumn("��������",  dtDec),
                                                                    new DataColumn("������",  dtDec),
                                                                    new DataColumn("ȱʧ����",dtStr),//{613A769A-C540-4a2c-949D-28B31F0BC482}
                                                                    new DataColumn("��ע",      dtStr),                                                                    
                                                                    new DataColumn("ҩƷ����",  dtStr),
                                                                    new DataColumn("��ˮ��",    dtStr),
                                                                    new DataColumn("������Դ",  dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr),
                                                                    new DataColumn("����",      dtStr)
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
        /// ����ҩƷ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        public int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugID = "";
            if (isBack)
            {
                drugID = sv.Cells[activeRow, 2].Value.ToString();

                string outbillNO = sv.Cells[activeRow, 0].Value.ToString();
                return this.AddDrugData(drugID, outbillNO);
            }
            else
            {
                drugID = sv.Cells[activeRow, 0].Value.ToString();
                return this.AddDrugData(drugID);
            }
        }

        public int ShowApplyList()
        {
            //{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�
            string applyState = "0";
            if (this.isTemporaryFun)
            {
                applyState = "A";
            }

            ////{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�  ��ȡ������Ϣ
            ArrayList alTemp = this.itemManager.QueryApplyOutList(this.phaInManager.DeptInfo.ID, this.phaInManager.PrivType.Memo, applyState);
            if (alTemp == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡ������Ϣʧ��" + this.itemManager.Err));
                return -1;
            }
            ArrayList alList = new ArrayList();
            //���ݵ�ǰѡ��Ĺ�����λ����
            if (this.phaInManager.TargetDept.ID != "")
            {
                foreach (FS.FrameWork.Models.NeuObject info in alTemp)
                {
                    if (info.Memo != this.phaInManager.TargetDept.ID)
                        continue;
                    alList.Add(info);
                }
            }
            else
            {
                alList = alTemp;
            }

            //��������ѡ�񵥾�
            FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            string[] fpLabel = { "���뵥��", "������λ" };
            float[] fpWidth = { 120F, 120F };
            bool[] fpVisible = { true, true, false, false, false, false };

            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alList, ref selectObj) == 1)
            {
                this.Clear();

                FS.FrameWork.Models.NeuObject targeDept = new FS.FrameWork.Models.NeuObject();

                targeDept.ID = selectObj.Memo;              //������˾����
                targeDept.Name = selectObj.Name;            //������˾����
                targeDept.Memo = "0";                       //Ŀ�굥λ���� �ڲ�����

                if (this.phaInManager.TargetDept.ID != targeDept.ID)
                {
                    this.phaInManager.TargetDept = targeDept;
                    this.ShowSelectData();
                }

                this.AddApplyData(selectObj.ID);

                this.SetFocusSelect();

                if (this.svTemp != null)
                {
                    this.phaInManager.Fp.StartCellEditing(null, false);
                }
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

        public int ImportData()
        {
            return 1;
        }

        public bool Valid()
        {
            if (this.phaInManager.TargetDept.ID == "")
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ѡ�񹩻����ң�"));
                return false;
            }
            try
            {
                foreach (DataRow dr in this.dt.Rows)
                {
                    if (NConvert.ToDecimal(dr["��������"]) <= 0)
                    {
                        System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + "������������С�ڵ�����");
                        return false;
                    }

                    //{99136B29-4E44-44aa-84DC-F3F24F2E98DE}�˿�����ʱ�����������ܴ��ڿ��
                    if (isBack)
                    {
                        if (NConvert.ToDecimal(dr["��������"]) > NConvert.ToDecimal(dr["���ƿ��"]))
                        {
                            System.Windows.Forms.MessageBox.Show(dr["��Ʒ����"].ToString() + "�����������ܴ��ڱ��ƿ��");
                            return false;
                        }
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

        public int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex)
        {
            try
            {
                if (sv != null && delRowIndex >= 0)
                {
                    DialogResult rs = MessageBox.Show("ȷ�϶���ѡ�����ݽ���ɾ����", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    DataRow dr = this.dt.Rows.Find(this.GetFindKey(sv, delRowIndex));
                    if (dr != null)
                    {
                        #region �����Ƴ�

                        if (dr["��ˮ��"].ToString() != "")
                        {
                            int parm = this.itemManager.DeleteApplyOut(dr["��ˮ��"].ToString());
                            if (parm == -1)
                            {
                                MessageBox.Show(Language.Msg(this.itemManager.Err));
                                return -1;
                            }
                            if (parm == 0)
                            {
                                MessageBox.Show(Language.Msg("��������ѱ��������� ������"));
                                return -1;
                            }
                            MessageBox.Show(Language.Msg("ɾ���ɹ�"));
                        }

                        #endregion

                        this.phaInManager.Fp.StopCellEditing();

                        this.hsApplyData.Remove(this.GetKey(dr));

                        this.dt.Rows.Remove(dr);
                       
                        this.phaInManager.Fp.StartCellEditing(null, false);

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

        public int Clear()
        {
            try
            {
                this.dt.Rows.Clear();

                this.dt.AcceptChanges();

                this.hsApplyData.Clear();

                this.phaInManager.TotCostInfo = "";

                this.listNO = "";
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
            if (this.svTemp != null)
            {
                if (this.svTemp.Rows.Count > 0)
                {
                    this.phaInManager.SetFpFocus();

                    this.svTemp.ActiveRowIndex = this.svTemp.Rows.Count - 1;
                    this.svTemp.ActiveColumnIndex = (int)ColumnSet.ColApplyQty;
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

            //{37D3D84C-702A-4090-8CB0-B9993279C735}   Ϊ��ʵ���ݴ� ÿ�δ���ȫ������
            //if (dtAddMofity == null || dtAddMofity.Rows.Count <= 0)
            //    return;

            //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
            this.alPrintData.Clear();

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            if (this.listNO == "")
            {
                #region ��ȡ�����뵥�ݺ�

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
                //phaIntegrate.SetTrans(t.Trans);

                // //{59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ������ⵥ�Ż�ȡ��ʽ
                listNO = phaIntegrate.GetInOutListNO(this.phaInManager.DeptInfo.ID, false);
                if (listNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡ�����뵥�ݺŷ�������" + this.itemManager.Err));
                    return;
                }

                #endregion
            }

            ////{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ� �Ƿ��ݴ�����
            bool isTemporaryData = false;
            string msg = "��������ɹ�";

            //{37D3D84C-702A-4090-8CB0-B9993279C735}  ��������ݴ�
            foreach (DataRow dr in this.dt.Rows)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsApplyData[this.GetKey(dr)] as FS.HISFC.Models.Pharmacy.ApplyOut;

                #region ���뵥��Ϣ��ֵ

                applyOut.Operation.ApplyOper.OperTime = sysTime;

                applyOut.Memo = dr["��ע"].ToString();

                if (this.isBack)
                {
                    if (!this.IsEnoughStore(applyOut.Item.ID, applyOut.Operation.ApplyQty))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show(Language.Msg(applyOut.Item.Name + " �������С�ڱ����˿��������� ������˿�����"));
                        return;
                    }
                }

                applyOut.Operation.ApplyQty = FS.FrameWork.Function.NConvert.ToDecimal( dr["��������"] ) * applyOut.Item.PackQty;

                #endregion

                //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�
                if (isTemporaryFun)     //ѡ�����ݴ����
                {
                    if (applyOut.State == "0" && (string.IsNullOrEmpty(applyOut.ID) == false))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�������ύ���� ���ܽ������뵥�ݴ��������ѡ�񱣴�����ύ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    applyOut.State = "A";

                    msg = "�ݴ�����ɹ�";
                }

                if (applyOut.ID == "")
                {
                    #region �²�������

                    applyOut.BillNO = this.listNO;              //���뵥�ݺ�

                    if (this.itemManager.InsertApplyOut(applyOut) == -1)                    
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg(this.itemManager.Err));
                        return;
                    }

                    #endregion
                }
                else
                {
                    #region ����ԭ������

                    int parm = this.itemManager.UpdateApplyOutNum(applyOut.ID, applyOut.Operation.ApplyQty);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show(Language.Msg("�������������и���ʧ��" + this.itemManager.Err));
                        return;
                    }
                    if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        System.Windows.Forms.MessageBox.Show(Language.Msg("�����뵥�ѱ���ˣ��޷������޸�!��ˢ������"));
                        return;
                    }

                    #endregion
                }

                //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�    �����Ƿ�����ݴ�����
                if (applyOut.State == "A")
                {
                    isTemporaryData = true;
                }

                //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
                this.alPrintData.Add(applyOut);
            }

            //{37D3D84C-702A-4090-8CB0-B9993279C735}   ��������ݴ�    �ݴ������ύ
            if (isTemporaryData && (isTemporaryFun == false))
            {
                if (this.itemManager.UpdateApplyOutState(this.phaInManager.DeptInfo.ID, this.listNO, "0") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�ύ������뵥ʧ��") + this.itemManager.Err,"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                msg = "�ύ�������ɹ�";
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg(msg));

            //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
            if (isTemporaryFun == false)            //���ݴ����
            {
                this.Print();
            }

            this.Clear();
        }

        public int Print()
        {
            //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
            DialogResult rs = MessageBox.Show("�Ƿ��ӡ���뵥?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                if (this.phaInManager.IInPrint != null)
                {
                    this.phaInManager.IInPrint.SetData(this.alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType.InnerApplyIn);
                    this.phaInManager.IInPrint.Print();
                }
            }

            return 1;
        }

        #endregion

        #region IPhaInManager ��Ա

        public int Dispose()
        {
            return 1;
        }

        #endregion

        private void value_EndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            this.Clear();
            //{08E013EE-8E71-4f12-9480-5E94C9CE60AB}
            //this.InitStockInfo();
            this.ShowSelectData();
        }

        private void value_FpKeyEvent(Keys key)
        {
            if (this.svTemp != null)
            {
                if (key == Keys.Enter)
                {
                    if (this.svTemp.ActiveColumnIndex == (int)ColumnSet.ColApplyQty)
                    {
                        decimal applyQty = NConvert.ToDecimal(this.svTemp.Cells[this.svTemp.ActiveRowIndex, (int)ColumnSet.ColApplyQty].Text);
                        decimal price = NConvert.ToDecimal(this.svTemp.Cells[this.svTemp.ActiveRowIndex, (int)ColumnSet.ColRetailPrice].Text);
                        this.svTemp.Cells[this.svTemp.ActiveRowIndex, (int)ColumnSet.ColApplyCost].Text = (applyQty * price).ToString();

                        this.CompuateSum();
                    }

                    if (this.svTemp.ActiveRowIndex == this.svTemp.Rows.Count - 1)
                    {
                        this.phaInManager.SetFocus();
                    }
                    else
                    {                      
                        this.svTemp.ActiveRowIndex++;
                        this.svTemp.ActiveColumnIndex = (int)ColumnSet.ColApplyQty;
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
            /// ��С��λ
            /// </summary>
            ColMinUnit,
            /// <summary>
            /// ���ƿ��
            /// </summary>
            ColStoreQty,
            /// <summary>
            /// ��������
            /// </summary>
            ColApplyQty,
            /// <summary>
            /// ������
            /// </summary>
            ColApplyCost,
            /// <summary>
            /// ȱʧ����{613A769A-C540-4a2c-949D-28B31F0BC482}
            /// </summary>
            ColLostRate,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ColDrugID,
            /// <summary>
            /// ��ˮ��
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
            ColUserCode,
            /// <summary>
            /// ����
            /// </summary>
            ColKey
        }
    }
}
