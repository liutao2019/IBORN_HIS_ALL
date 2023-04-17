using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucNurseQuitFee<br></br>
    /// [��������: סԺ�˷�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQuitFee : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        /// <summary>
        /// ucQuitFee<br></br>
        /// [��������: סԺ�˷�UC]<br></br>
        /// [�� �� ��: ����]<br></br>
        /// [����ʱ��: 2006-11-06]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucQuitFee()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���תҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���ù���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// סԺ�շ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phamarcyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �˷�����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// סԺ���߻�����Ϣ
        /// </summary>
        protected PatientInfo patientInfo = null;

        /// <summary>
        /// ҩƷδ���б�
        /// </summary>
        protected DataTable dtDrug = new DataTable();

        /// <summary>
        /// ҩƷDV
        /// </summary>
        protected DataView dvDrug = new DataView();

        /// <summary>
        /// ҩƷ�����б�
        /// </summary>
        protected DataTable dtDrugQuit = new DataTable();

        /// <summary>
        /// ��ҩƷδ���б�
        /// </summary>
        protected DataTable dtUndrug = new DataTable();

        /// <summary>
        /// ��ҩƷδ��dv
        /// </summary>
        protected DataView dvUndrug = new DataView();

        /// <summary>
        /// ��ҩƷ�����б�
        /// </summary>
        protected DataTable dtUndrugQuit = new DataTable();

        /// <summary>
        /// δ��ҩƷ��������·��
        /// </summary>
        protected string filePathUnQuitDrug = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QuitFeeUnQuitDrug.xml";

        /// <summary>
        /// δ�˷�ҩƷ��������·��
        /// </summary>
        protected string filePathUnQuitUndrug = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QuitFeeUnQuitUndrug.xml";

        /// <summary>
        /// �˷Ѳ�������
        /// </summary>
        protected Operations operation;

        /// <summary>
        /// �ɲ�������Ŀ���
        /// </summary>
        protected ItemTypes itemType;

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �Ƿ�����ֹ�����סԺ��
        /// </summary>
        protected bool isCanInputInpatientNO = true;

        /// <summary>
        /// ת����С����ID,Name��
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper objectHelperMinFee = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ƿ���Ը����˷�����
        /// </summary>
        protected bool isChangeItemQty = true;
        public bool isCanQuitOtherFee = true;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private DataTable dtMate = new DataTable();
        /// <summary>
        /// �����շ�
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Material.Material mateInteger = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        protected bool isCombItemAllQuit = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449}����
        #endregion

        #region ����

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        public bool IsCombItemAllQuit
        {
            get
            {
                return this.isCombItemAllQuit;
            }
            set
            {
                this.isCombItemAllQuit = value;
            }
        }//{F4912030-EF65-4099-880A-8A1792A3B449}����

        /// <summary>
        /// �˷Ѳ�������
        /// </summary>
        [Category("�ؼ�����"), Description("���û��߻���˷ѵĲ�������")]
        public Operations Operation
        {
            set
            {
                this.operation = value;
            }
            get
            {
                return this.operation;
            }
        }

        /// <summary>
        /// �ɲ�������Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���û��߻�ÿɲ�������Ŀ���")]
        public ItemTypes ItemType
        {
            set
            {
                this.itemType = value;
            }
            get
            {
                return this.itemType;
            }
        }

        /// <summary>
        /// סԺ���߻�����Ϣ
        /// </summary>
        public PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;

                this.SetPatientInfomation();
            }
        }

        /// <summary>
        /// �Ƿ�����ֹ�����סԺ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����ֹ�����סԺ��")]
        public bool IsCanInputInpatientNO
        {
            get
            {
                return this.isCanInputInpatientNO;
            }
            set
            {
                this.isCanInputInpatientNO = value;

                this.ucQueryPatientInfo.Enabled = this.isCanInputInpatientNO;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ���Ը����˷����� True:���� False:������")]
        public bool IsChangeItemQty
        {
            get
            {
                return isChangeItemQty;
            }
            set
            {
                isChangeItemQty = value;
                if (this.DesignMode)
                    return;
                if (!value)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
            }
        }
        [Category("�ؼ�����"), Description("�Ƿ��������������ҷ��� True:���� False:������")]
        public bool IsCanQuitOtherDeptFee
        {
            get
            {
                return isCanQuitOtherFee;
            }
            set
            {
                isCanQuitOtherFee = value;
            }
        }



        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private string operationPriv = string.Empty;

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ȩ�ޣ����磺0830+23��0830 �������Ȩ�ޣ�23 ��������Ȩ�ޣ�Ϊ���������ҪȨ��Ҳ����ʹ��")]
        public string OperationPriv
        {
            get
            {
                return operationPriv;
            }
            set
            {
                operationPriv = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ÿɲ�������Ŀ���
        /// </summary>
        protected virtual void SetItemType()
        {
            switch (this.itemType)
            {
                case ItemTypes.Pharmarcy:
                    this.fpUnQuit_SheetDrug.Visible = true;
                    this.fpUnQuit_SheetUndrug.Visible = false;
                    this.fpQuit_SheetDrug.Visible = true;
                    this.fpQuit_SheetUndrug.Visible = false;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

                    break;

                case ItemTypes.Undrug:
                    this.fpUnQuit_SheetDrug.Visible = false;
                    this.fpUnQuit_SheetUndrug.Visible = true;
                    this.fpQuit_SheetDrug.Visible = false;
                    this.fpQuit_SheetUndrug.Visible = true;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetUndrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetUndrug;

                    break;

                case ItemTypes.All:
                    this.fpUnQuit_SheetDrug.Visible = true;
                    this.fpUnQuit_SheetUndrug.Visible = true;
                    this.fpQuit_SheetDrug.Visible = true;
                    this.fpQuit_SheetUndrug.Visible = true;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

                    break;
            }
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected virtual void SetPatientInfomation()
        {
            this.txtName.Text = this.patientInfo.Name;
            this.txtPact.Text = this.patientInfo.Pact.Name;
            this.txtDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBed.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID;
            this.dtpBeginTime.Focus();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0);
            this.dtpEndTime.Value = nowTime;

            ArrayList minFeeList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (minFeeList == null)
            {
                MessageBox.Show("�����С���ó���!" + this.managerIntegrate.Err);

                return -1;
            }

            this.objectHelperMinFee.ArrayObject = minFeeList;

            this.InitFp();
            return 1;
        }

        /// <summary>
        /// ���δ�˷ѵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetriveDrug(DateTime beginTime, DateTime endTime)
        {
            string flag = string.Empty;

            if (this.operation == Operations.QuitFee)
            {
                flag = "1";
            }
            //�˷�����ʱ��ʾ��ҩƷ��
            else if (this.operation == Operations.Apply)
            {
                flag = "1,2";
            }
            else
            {
                flag = "1,2";
            }

            ArrayList drugList = this.inpatientManager.QueryMedItemListsCanQuit(this.patientInfo.ID, beginTime, endTime, flag);
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("���ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }

            this.SetDrugList(drugList);

            return 1;
        }

        /// <summary>
        /// ����ҩƷ�б�
        /// </summary>
        /// <param name="drugList"></param>
        protected virtual void SetDrugList(ArrayList drugList)
        {
            foreach (FeeItemList feeItemList in drugList)
            {
                DataRow row = this.dtDrug.NewRow();

                //��ȡҩƷ������Ϣ,������ʱΪ�˻��ƴ����
                FS.HISFC.Models.Base.Item phamarcyItem = this.phamarcyIntegrate.GetItem(feeItemList.Item.ID);
                if (phamarcyItem == null)
                {
                    phamarcyItem = new FS.HISFC.Models.Base.Item();

                }

                if (phamarcyItem.PackQty == 0)
                {
                    phamarcyItem.PackQty = 1;
                }

                //feeItemList.Item.IsPharmacy = true;
                feeItemList.Item.ItemType = EnumItemType.Drug;
                row["ҩƷ����"] = feeItemList.Item.Name;
                row["���"] = feeItemList.Item.Specs;
                row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
                row["�۸�"] = feeItemList.Item.Price;
                row["��������"] = feeItemList.NoBackQty;
                row["��λ"] = feeItemList.Item.PriceUnit;
                row["���"] = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / phamarcyItem.PackQty, 2);
                FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                if (deptInfo == null)
                {
                    deptInfo = new FS.HISFC.Models.Base.Department();
                    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                }

                row["ִ�п���"] = deptInfo.Name;


                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);
                if (empl.Name == string.Empty)
                {
                    row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                }
                else
                {
                    row["����ҽʦ"] = empl.Name;
                }

                //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                row["��������"] = feeItemList.FeeOper.OperTime;
                row["�Ƿ�ҩ"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';

                row["����"] = feeItemList.Item.ID;
                row["ҽ����"] = feeItemList.Order.ID;
                row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
                row["������"] = feeItemList.RecipeNO;
                row["������ˮ��"] = feeItemList.SequenceNO;

                row["ƴ����"] = phamarcyItem.SpellCode;
                row["��������"] = feeItemList.FeeOper.Dept.ID;

                this.dtDrug.Rows.Add(row);
            }
        }

        /// <summary>
        /// ���δ�˷ѵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetriveUnrug(DateTime beginTime, DateTime endTime)
        {
            ArrayList undrugList = this.inpatientManager.QueryFeeItemListsCanQuit(this.patientInfo.ID, beginTime, endTime, false);
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("��÷�ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }

            this.SetUndrugList(undrugList);

            return 1;
        }

        /// <summary>
        /// ���÷�ҩƷ�б�
        /// </summary>
        /// <param name="undrugList"></param>
        protected virtual void SetUndrugList(ArrayList undrugList)
        {
            dtMate.Rows.Clear();
            foreach (FeeItemList feeItemList in undrugList)
            {
                DataRow row = this.dtUndrug.NewRow();

                //��÷�ҩƷ��Ϣ,������Ҫ��Ϊ�˻�ü�����
                FS.HISFC.Models.Fee.Item.Undrug undrugItem = this.undrugManager.GetValidItemByUndrugCode(feeItemList.Item.ID);
                if (undrugItem == null)
                {
                    undrugItem = new FS.HISFC.Models.Fee.Item.Undrug();
                }
                if (undrugItem.PackQty == 0)
                {
                    undrugItem.PackQty = 1;
                }
                //feeItemList.Item.IsPharmacy = false;
                feeItemList.Item.ItemType = EnumItemType.Drug;
                row["��Ŀ����"] = feeItemList.Item.Name;
                row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
                row["�۸�"] = feeItemList.Item.Price;
                row["��������"] = feeItemList.NoBackQty;
                row["��λ"] = feeItemList.Item.PriceUnit;
                row["���"] = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / undrugItem.PackQty, 2);
                FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                if (deptInfo == null)
                {
                    deptInfo = new FS.HISFC.Models.Base.Department();
                    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                }

                row["ִ�п���"] = deptInfo.Name;

                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);
                if (empl.Name == string.Empty)
                {
                    row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                }
                else
                {
                    row["����ҽʦ"] = empl.Name;
                }
                //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                row["��������"] = feeItemList.FeeOper.OperTime;
                row["�Ƿ�ִ��"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';
                row["����"] = feeItemList.Item.ID;
                row["ҽ����"] = feeItemList.Order.ID;
                row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
                row["������"] = feeItemList.RecipeNO;
                row["������ˮ��"] = feeItemList.SequenceNO;

                row["ƴ����"] = undrugItem == null ? string.Empty : undrugItem.SpellCode;
                row["��������"] = feeItemList.FeeOper.Dept.ID;
                row["������ˮ��"] = feeItemList.UpdateSequence.ToString();
                row["��ʶ"] = ((int)feeItemList.Item.ItemType).ToString();
                this.dtUndrug.Rows.Add(row);
                //���ݷ�ҩƷ��ȡ���յ�������Ϣ
                GetMateData(feeItemList);
            }
            SetUndragFp(string.Empty);
        }

        private void SetUndragFp(string strVal)
        {
            string filter = "��Ŀ���� like '" + strVal + "%'" + " OR " + "ƴ���� like '" + strVal + "%'";
            DataRow[] vdr = dtUndrug.Select(filter);
            if (vdr == null || vdr.Length == 0)
                return;
            foreach (DataRow dr in vdr)
            {
                SetUndrugRow(dr);
            }
        }

        /// <summary>
        /// ���ݷ�ҩƷ��ȡ���յ�������Ϣ
        /// </summary>
        /// <param name="feeItem">��ҩƷ�շ���Ϣ</param>
        private void GetMateData(FeeItemList feeItem)
        {
            string outNo = feeItem.UpdateSequence.ToString();
            List<HISFC.Models.FeeStuff.Output> list = mateInteger.QueryOutput(outNo);
            DataRow row = null;
            foreach (HISFC.Models.FeeStuff.Output item in list)
            {
                row = dtMate.NewRow();
                row["��Ŀ����"] = item.StoreBase.Item.Name;
                row["��������"] = this.objectHelperMinFee.GetName(item.StoreBase.Item.MinFee.ID);
                row["���"] = item.StoreBase.Item.Specs;
                row["�۸�"] = item.StoreBase.Item.Price;
                row["��������"] = item.StoreBase.Quantity - item.ReturnApplyNum - item.StoreBase.Returns;
                row["��λ"] = item.StoreBase.Item.PriceUnit;
                row["���"] = NConvert.ToDecimal(row["�۸�"]) * NConvert.ToInt32(row["��������"]);
                row["������ˮ��"] = item.ID;
                row["����"] = item.StoreBase.Item.ID;
                row["������"] = item.StoreBase.StockNO;

                dtMate.Rows.Add(row);
            }
        }

        private void SetUndrugRow(DataRow dr)
        {
            this.fpUnQuit_SheetUndrug.Rows.Add(this.fpUnQuit_SheetUndrug.Rows.Count, 1);
            int rowIndex = this.fpUnQuit_SheetUndrug.Rows.Count - 1;
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 0].Text = dr["��Ŀ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 1].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 2].Text = dr["�۸�"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 3].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 4].Text = dr["��λ"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 5].Text = dr["���"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 6].Text = dr["ִ�п���"].ToString();

            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 7].Text = dr["����ҽʦ"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 8].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 9].Text = dr["�Ƿ�ִ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 10].Text = dr["����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 11].Text = dr["ҽ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 12].Text = dr["ҽ��ִ�к�"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 13].Text = dr["������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 14].Text = dr["������ˮ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 15].Text = dr["ƴ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 16].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 17].Text = dr["������ˮ��"].ToString();
            //��ʾ��������
            SetMateRow(dr, rowIndex);
        }



        /// <summary>
        /// ��ʾ��������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        private void SetMateRow(DataRow dr, int rowIndex)
        {

            int index = 0;
            //string itemCode = dr["����"].ToString();
            string outNo = dr["������ˮ��"].ToString();
            //string strfilter = "���� ='" + itemCode + "' And " + "������ˮ�� ='" + outNo + "'";
            string strfilter = "������ˮ�� ='" + outNo + "'";
            DataRow[] vdr = dtMate.Select(strfilter);
            if (vdr == null || vdr.Length == 0)
                return;
            if (vdr.Length == 1)
                return;
            fpUnQuit_SheetUndrug.RowHeader.Cells[rowIndex, 0].Text = "+";
            fpUnQuit_SheetUndrug.RowHeader.Cells[rowIndex, 0].BackColor = Color.YellowGreen;
            foreach (DataRow row in vdr)
            {
                fpUnQuit_SheetUndrug.Rows.Add(fpUnQuit_SheetUndrug.Rows.Count, 1);
                index = fpUnQuit_SheetUndrug.Rows.Count - 1;
                this.fpUnQuit_SheetUndrug.Cells[index, 0].Text = row["��Ŀ����"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.fpUnQuit_SheetUndrug.Cells[index, 1].Text = row["��������"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 2].Text = row["�۸�"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 3].Text = row["��������"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 4].Text = row["��λ"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 5].Text = row["���"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 10].Text = row["����"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 17].Text = row["������ˮ��"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 18].Text = row["������"].ToString();
                this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text = ".";
                this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].BackColor = System.Drawing.Color.SkyBlue;
                this.fpUnQuit_SheetUndrug.Rows[index].Visible = false;
            }

        }

        private void RetrieveReturnApply(bool isPharmarcy)
        {
            ArrayList returnApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, isPharmarcy);

            if (returnApplys == null)
            {
                MessageBox.Show("����˷�������Ϣ����");

                return;
            }

            foreach (FS.HISFC.Models.Fee.ReturnApply retrunApply in returnApplys)
            {
                //ҩƷ
                //if (retrunApply.Item.IsPharmacy)
                if (retrunApply.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.RowCount, 1);

                    int index = this.fpQuit_SheetDrug.RowCount - 1;

                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.ItemName, retrunApply.Item.Name);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Specs, retrunApply.Item.Specs);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Price, retrunApply.Item.Price);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, retrunApply.Item.Qty);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Unit, retrunApply.Item.PriceUnit);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.FeeDate, retrunApply.CancelOper.OperTime);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsConfirm, retrunApply.IsConfirmed);
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsApply, true);
                    //�������ϴ�����
                    retrunApply.CancelRecipeNO = retrunApply.RecipeNO;
                    //�������ϴ����ڲ���ˮ��
                    retrunApply.CancelSequenceNO = retrunApply.SequenceNO;
                    this.fpQuit_SheetDrug.Rows[index].Tag = retrunApply;
                }
                else
                {
                    this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);

                    int index = this.fpQuit_SheetUndrug.RowCount - 1;

                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, retrunApply.Item.Name);


                    FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
                    FS.HISFC.BizLogic.Fee.Item feeItemManager = new FS.HISFC.BizLogic.Fee.Item();
                    undrugInfo = feeItemManager.GetUndrugByCode(retrunApply.Item.ID);
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(undrugInfo.MinFee.ID));

                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Price, retrunApply.Item.Price);
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, retrunApply.Item.Qty);
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Unit, retrunApply.Item.PriceUnit);
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsConfirm, retrunApply.IsConfirmed);
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsApply, true);
                    FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                    deptInfo = this.managerIntegrate.GetDepartment(retrunApply.ExecOper.Dept.ID);
                    if (deptInfo == null)
                    {
                        deptInfo = new FS.HISFC.Models.Base.Department();
                        deptInfo.Name = retrunApply.ExecOper.Dept.ID;
                    }
                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ExecDept, deptInfo.Name);
                    //�������ϴ�����
                    retrunApply.CancelRecipeNO = retrunApply.RecipeNO;
                    //�������ϴ����ڲ���ˮ��
                    retrunApply.CancelSequenceNO = retrunApply.SequenceNO;
                    this.fpQuit_SheetUndrug.Rows[index].Tag = retrunApply;
                }
            }
        }

        /// <summary>
        /// ��ȡδ�˷���Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Retrive(bool isRetrieveRetrunApply)
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show(Language.Msg("�����뻼�߻�����Ϣ!"));

                return -1;
            }

            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            //���ݴ��ڿɲ�������Ŀ���,��ȡδ�˷ѵ���Ŀ��Ϣ
            switch (this.itemType)
            {
                case ItemTypes.Pharmarcy:
                    this.RetriveDrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(true);
                    }

                    break;

                case ItemTypes.Undrug:
                    this.RetriveUnrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(false);
                    }

                    break;

                case ItemTypes.All:
                    this.RetriveDrug(beginTime, endTime);
                    this.RetriveUnrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(true);
                        this.RetrieveReturnApply(false);
                    }

                    break;
            }

            this.fpUnQuit_SheetDrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpUnQuit_SheetUndrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            return 1;
        }

        /// <summary>
        /// ��ʼ��Fp�к�Sheet��ʾ˳�����Ϣ,Sheet�����ݰ�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitFp()
        {
            this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
            this.fpQuit_SheetDrug.Columns[(int)DrugColumns.FeeDate].Visible = false;

            this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

            #region ��ʼ��ҩƷ��Ϣ

            this.dtDrug.Reset();

            //������ڱ���ҩƷ���������ļ�,ֱ�Ӷ�ȡ�����ļ�����DataTable,���Ѵ����źʹ�����ˮ����Ϊ����
            //������������Ҫ��Ϊ���Ժ�Ĳ��Ҷ�Ӧ��Ŀ��
            if (System.IO.File.Exists(this.filePathUnQuitDrug))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathUnQuitDrug, dtDrug, ref dvDrug, this.fpUnQuit_SheetDrug);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);

                dtDrug.PrimaryKey = new DataColumn[] { dtDrug.Columns["������"], dtDrug.Columns["������ˮ��"] };
            }
            else//���û���ҵ������ļ�,��Ĭ�ϵ���˳�����������DataTable,����ͬ��Ϊ�����źʹ�����ˮ��
            {
                this.dtDrug.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ҩƷ����", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("ִ�п���", typeof(string)),
                    new DataColumn("����ҽʦ", typeof(string)),
                    new DataColumn("��������", typeof(DateTime)),
                    new DataColumn("�Ƿ�ҩ", typeof(string)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("ҽ����", typeof(string)),
                    new DataColumn("ҽ��ִ�к�", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("��װ��", typeof(decimal)),
                    new DataColumn("�Ƿ��װ��λ", typeof(string)),
                    new DataColumn("ƴ����", typeof(string)),
                    new DataColumn("��������")
                 });

                dtDrug.PrimaryKey = new DataColumn[] { dtDrug.Columns["������"], dtDrug.Columns["������ˮ��"] };

                dvDrug = new DataView(dtDrug);

                //�󶨵���Ӧ��Farpoint
                this.fpUnQuit_SheetDrug.DataSource = dvDrug;

                //���浱ǰ����˳��,���Ƶ���Ϣ��XML
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            }

            #endregion

            #region ��ʼ��δ�˷�ҩƷ��Ϣ

            dtUndrug.Reset();

            //������ڱ��ط�ҩƷ���������ļ�,ֱ�Ӷ�ȡ�����ļ�����DataTable,���Ѵ����źʹ�����ˮ����Ϊ����
            //������������Ҫ��Ϊ���Ժ�Ĳ��Ҷ�Ӧ��Ŀ��
            if (System.IO.File.Exists(this.filePathUnQuitUndrug))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathUnQuitUndrug, dtUndrug, ref dvUndrug, this.fpUnQuit_SheetUndrug);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);

                dtUndrug.PrimaryKey = new DataColumn[] { dtUndrug.Columns["������"], dtUndrug.Columns["������ˮ��"] };
                this.fpUnQuit_SheetUndrug.DataSource = null;
            }
            else
            {
                this.dtUndrug.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��Ŀ����", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("ִ�п���", typeof(string)),
                    new DataColumn("����ҽʦ", typeof(string)),
                    new DataColumn("��������", typeof(DateTime)),
                    new DataColumn("�Ƿ�ִ��", typeof(string)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("ҽ����", typeof(string)),
                    new DataColumn("ҽ��ִ�к�", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("ƴ����", typeof(string)),
                    new DataColumn("��������",typeof(string)),
                    new DataColumn("������ˮ��",typeof(string)),
                    new DataColumn("������",typeof(string)),
                    //��0ҩƷ 2����
                    new DataColumn("��ʶ",typeof(string))
                 });

                dtUndrug.PrimaryKey = new DataColumn[] { dtUndrug.Columns["������"], dtUndrug.Columns["������ˮ��"] };
                dvUndrug = new DataView(dtUndrug);

                //�󶨵���Ӧ��Farpoint
                //this.fpUnQuit_SheetUndrug.DataSource = dvUndrug;

                ////���浱ǰ����˳��,���Ƶ���Ϣ��XML
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
            }
            this.dtMate.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��Ŀ����", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("������ˮ��",typeof(string)),
                    new DataColumn("������",typeof(string))
                });
            dtMate.PrimaryKey = new DataColumn[] { dtMate.Columns["������"], dtMate.Columns["������ˮ��"] };

            #endregion

            return 1;
        }


        /// <summary>
        /// ͨ��������ȡ����ҩƷ��
        /// </summary>
        /// <param name="Name">����</param>
        /// <returns>����� �ɹ�   ��1ʧ��</returns>
        protected int GetColumnIndexFromNameForfpfpUnQuitDrug(string Name)
        {
            for (int i = 0; i < this.fpUnQuit_SheetDrug.Columns.Count; i++)
            {
                if (this.fpUnQuit_SheetDrug.Columns[i].Label == Name)
                    return i;
            }
            FS.FrameWork.WinForms.Classes.Function.Msg("ȱ����" + Name, 211);

            return -1;
        }

        /// <summary>
        /// ͨ��������ȡ���˷�ҩƷ��
        /// </summary>
        /// <param name="Name">����</param>
        /// <returns>����� �ɹ�   ��1ʧ��</returns>
        protected int GetColumnIndexFromNameForfpfpUnQuitUnDrug(string Name)
        {
            for (int i = 0; i < this.fpUnQuit_SheetUndrug.Columns.Count; i++)
            {
                if (this.fpUnQuit_SheetUndrug.Columns[i].Label == Name)
                    return i;
            }
            FS.FrameWork.WinForms.Classes.Function.Msg("ȱ����" + Name, 211);

            return -1;
        }
        /// <summary>
        /// ���˲�����Ŀ
        /// </summary>
        protected virtual void SetFilter()
        {
            string filterString = string.Empty;

            filterString = this.txtFilter.Text.Trim();

            //ȥ�����ܵ��¹��˱����������ַ�
            filterString = FS.FrameWork.Public.String.TakeOffSpecialChar(filterString, new string[] { "[", "'" });

            //�����ǰ�ҳ����δ��ҩƷʱ�Ĺ���
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                dvDrug.RowFilter = "ҩƷ���� like '" + filterString + "%'" + " OR " + "ƴ���� like '" + filterString + "%'";

                //���¶�ȡ�е�˳��Ϳ�ȵ���Ϣ
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            }
            //�����ǰ�ҳ����δ�˷�ҩƷʱ�Ĺ���
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                //dvUndrug.RowFilter = "��Ŀ���� like '" + filterString + "%'" + " OR " + "ƴ���� like '" + filterString + "%'";
                this.SetUndragFp(filterString);
                //���¶�ȡ�е�˳��Ϳ�ȵ���Ϣ
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
            }
        }

        /// <summary>
        /// ������������������λ��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sv">Ҫ���ҵ�SheetView</param>
        /// <returns>�ɹ� ������λ�� ʧ�� -1</returns>
        private int FindColumn(string name, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.Columns.Count; i++)
            {
                if (sv.Columns[i].Label == name)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʾѡ����˷���Ϣ
        /// </summary>
        /// <param name="feeItemList">������Ϣʵ��</param>
        protected virtual void SetItemToDeal(FeeItemList feeItemList)
        {
            //this.txtItemName.Text = feeItemList.Item.Name;
            //this.txtItemName.Tag = feeItemList;
            //this.txtPrice.Text = feeItemList.Item.Price.ToString();
            //this.mtbQty.Text = feeItemList.NoBackQty.ToString();
            //this.txtUnit.Text = feeItemList.Item.PriceUnit;
            HISFC.Models.FeeStuff.Output outItem = null;
            bool isFind = false;
            if (feeItemList.MateList.Count == 0)
            {
                isFind = false;

            }
            else
            {
                DataRow[] vdr = dtMate.Select("������ˮ�� ='" + feeItemList.MateList[0].ID + "'");
                if (vdr.Length <= 1)
                {
                    isFind = false;
                }
                else
                {
                    isFind = true;
                }

            }
            if (!isFind)
            {
                this.txtItemName.Text = feeItemList.Item.Name;
                this.txtItemName.Tag = feeItemList;
                this.txtPrice.Text = feeItemList.Item.Price.ToString();
                this.mtbQty.Text = feeItemList.NoBackQty.ToString();
                this.txtUnit.Text = feeItemList.Item.PriceUnit;
            }
            else
            {
                outItem = feeItemList.MateList[0];
                this.txtItemName.Text = outItem.StoreBase.Item.Name;
                this.txtItemName.Tag = feeItemList;
                this.txtPrice.Text = outItem.StoreBase.Item.Price.ToString();
                this.mtbQty.Text = outItem.StoreBase.Quantity.ToString();
                this.txtUnit.Text = feeItemList.Item.PriceUnit;
            }

            neuPanel1.Focus();
            neuPanel3.Focus();
            neuPanel4.Focus();
            gbQuitItem.Focus();
            this.mtbQty.Focus();
        }

        /// <summary>
        /// ˫����Ŀ�����¼�
        /// </summary>
        protected virtual void ChooseUnquitItem()
        {
            string recipeNO = string.Empty;//������
            int recipeSequence = 0;//��������Ŀ��ˮ��
            decimal noBackQty = 0;//��������
            //bool isPharmarcy = false;//�Ƿ�ҩƷ
            EnumItemType isPharmarcy = EnumItemType.UnDrug;

            //�жϵ�ǰ�������Ϣ�Ƿ�ΪҩƷ,����ҳ��ҩƷҳ,��ô˵���������ҩƷ,����Ϊ��ҩƷ
            isPharmarcy = this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug ? EnumItemType.Drug : EnumItemType.UnDrug;

            if (this.fpUnQuit.ActiveSheet.RowCount == 0)
            {
                return;
            }

            int index = this.fpUnQuit.ActiveSheet.ActiveRowIndex;
            #region �Ƿ��������������ҷ���
            if (!isCanQuitOtherFee && operation == Operations.Apply)
            {
                string FeeOperDept = fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("ִ�п���", this.fpUnQuit.ActiveSheet));
                if (FeeOperDept != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID)
                {
                    MessageBox.Show("���������������ҷ���");
                    return;
                }
            }
            #endregion

            #region ����(��ҩƷ��Ӧ��������)
            //�Ƿ��Ƕ�������
            List<HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
            string headerText = this.fpUnQuit.ActiveSheet.RowHeader.Cells[index, 0].Text;
            //��������
            if (isPharmarcy == EnumItemType.UnDrug)
            {
                if (headerText == "+" || headerText == "-")
                {
                    if (!this.ckbAllQuit.Checked)
                    {

                        if (!this.ckbAllQuit.Checked && headerText != ".")
                        {
                            MessageBox.Show("��ѡ��Ҫ�˷ѵ�������Ϣ��");
                            if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text == "+")
                            {
                                this.ExpandOrCloseRow(false, index + 1);
                            }
                            return;
                        }
                    }
                }
                outitemList = QuiteAllMate(index);
                index = this.FinItemRowIndex(index);
            }

            #endregion

            recipeNO = this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������", this.fpUnQuit.ActiveSheet));
            recipeSequence = NConvert.ToInt32(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������ˮ��", this.fpUnQuit.ActiveSheet)));
            noBackQty = NConvert.ToDecimal(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("��������", this.fpUnQuit.ActiveSheet)));

            //��÷��õ�������Ϣ,��ΪDataTable�в��ܰ�Tag,��Ϣ��ȫ
            FeeItemList feeItemList = this.inpatientManager.GetItemListByRecipeNO(recipeNO, recipeSequence, isPharmarcy);
            if (feeItemList == null)
            {
                MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);

                return;
            }
            //��NoBackQty�����б��浱ǰ�Ŀ�������,��Ϊ������ϵĿ��ܱ������˶��.
            feeItemList.NoBackQty = noBackQty;

            //feeItemList.MateList.Add(outitemList.);
            if (feeItemList.Item.ItemType != EnumItemType.Drug && outitemList.Count > 0)
            {
                feeItemList.MateList = outitemList;
            }

            //{F4912030-EF65-4099-880A-8A1792A3B449}
            //�Ǹ�����Ŀ�����Ҹ�����Ŀ����ȫ�ˣ����ﴦ��
            //ѭ����������moorder��packcodeһ������Ŀ��һ������.
            //����Ĭ�ϱ���ȫ�ˣ���ʹ������Ŀ��������1����Ȼ��������̫�鷳��
            if (this.isCombItemAllQuit && isPharmarcy == EnumItemType.UnDrug && !string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
            {
                MessageBox.Show("��ѡ����˷���Ŀ���븴����Ŀ������С���ǰ���ã�������Ŀ����ȫ�ˣ���ע��!");

                ArrayList recipeNOs = this.inpatientManager.QueryRecipesByMoOrder(feeItemList.Order.ID, feeItemList.UndrugComb.ID);
                if (recipeNOs == null)
                {
                    MessageBox.Show("û���ҵ���ͬ�ĸ�����Ŀ!" + this.inpatientManager.Err);

                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject o in recipeNOs)
                {
                    FeeItemList f = this.inpatientManager.GetItemListByRecipeNO(o.ID, NConvert.ToInt32(o.Name), isPharmarcy);
                    if (f == null)
                    {
                        MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);

                        return;
                    }

                    this.QuitOperation(f.Clone());
                }

                return;
            }//{F4912030-EF65-4099-880A-8A1792A3B449}����

            //���ȫ�˰�ťΪѡ��,��ô��ȫ�˵�ǰ�Ŀ�������.
            //{7A07D8BE-FEEA-42b4-B515-4699951333E8} ������Ϊ1�Ĳ���������
            if (this.ckbAllQuit.Checked || (this.ckbAllQuit.Checked == false && feeItemList.Days != 1 && feeItemList.Item.ItemType == EnumItemType.Drug))
            {
                this.QuitOperation(feeItemList.Clone());
            }
            else//����,������������������ĶԻ���,��������Ҫ�˵�����
            {
                this.SetItemToDeal(feeItemList.Clone());
            }
        }

        /// <summary>
        /// ȫ�˷�ҩƷ������
        /// </summary>
        /// <param name="isAllQuite">�Ƿ�ȫ��</param>
        /// <returns></returns>
        private List<HISFC.Models.FeeStuff.Output> QuiteAllMate(int index)
        {
            string headerText = this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text;
            string stockNo = string.Empty; //���ʳ������
            string outNo = string.Empty; //���ʳ������
            List<HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            FS.HISFC.Models.FeeStuff.Output outItem = null;
            //ȫ�˷�ҩƷ
            if (headerText != ".")
            {
                outNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������ˮ��", fpUnQuit_SheetUndrug));
                DataRow[] vdr = dtMate.Select("������ˮ�� ='" + outNo + "'");
                if (vdr.Length == 0)
                    return list;

                foreach (DataRow dr in vdr)
                {
                    outItem = this.GetOutPutByDataRow(dr);
                    if (outItem == null)
                    {
                        MessageBox.Show("����������Ŀ��Ϣʧ��");
                        return null;
                    }
                    list.Add(outItem);
                }
            }
            //ȫ������
            if (headerText == ".")
            {
                outItem = QuiteMate(index);
                if (outItem == null)
                {
                    MessageBox.Show("����������Ŀ��Ϣʧ��");
                    return null;
                }
                list.Add(outItem);
            }
            return list;
        }


        /// <summary>
        /// ��ǰ�˷ѵ�����(��ҩƷ���յ�����)
        /// </summary>
        /// <param name="index">��ǰѡ�е���</param>
        /// <param name="IsAllQuite">�Ƿ�ȫ��</param>
        /// <returns></returns>
        private HISFC.Models.FeeStuff.Output QuiteMate(int index)
        {
            string headerText = this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text;
            string stockNo = string.Empty; //���ʳ������
            string outNo = string.Empty; //���ʳ������
            HISFC.Models.FeeStuff.Output outItem = null;
            if (headerText == ".")
            {
                stockNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������", fpUnQuit_SheetUndrug));
                outNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������ˮ��", fpUnQuit_SheetUndrug));
                DataRow dr = this.FindMateRow(stockNo, outNo);
                if (dr == null)
                {
                    MessageBox.Show(Language.Msg("������ʻ�����Ϣ����!") + this.mateInteger.Err);
                    return null;
                }
                outItem = this.GetOutPutByDataRow(dr);
                if (outItem == null)
                {
                    MessageBox.Show(Language.Msg("������ʻ�����Ϣ����!") + this.mateInteger.Err);
                    return null;
                }
            }
            return outItem;
        }

        /// <summary>
        /// ����dtmate�е�DataRow�γ�OutPutʵ��
        /// </summary>
        /// <param name="dr">dtmate�е�DataRow</param>
        /// <returns></returns>
        private HISFC.Models.FeeStuff.Output GetOutPutByDataRow(DataRow dr)
        {
            HISFC.Models.FeeStuff.Output outItem = new FS.HISFC.Models.FeeStuff.Output();
            outItem.StoreBase.Item.Name = dr["��Ŀ����"].ToString();
            outItem.StoreBase.Item.MinFee.ID = this.objectHelperMinFee.GetID(dr["��������"].ToString());
            outItem.StoreBase.Item.Specs = dr["���"].ToString();
            outItem.StoreBase.Item.Price = NConvert.ToDecimal(dr["�۸�"]);
            outItem.StoreBase.Quantity = NConvert.ToDecimal(dr["��������"]);
            if (this.ckbAllQuit.Checked)
            {
                outItem.ReturnApplyNum = NConvert.ToDecimal(dr["��������"]);
            }
            outItem.StoreBase.Item.PriceUnit = dr["��λ"].ToString();
            outItem.ID = dr["������ˮ��"].ToString();
            outItem.StoreBase.Item.ID = dr["����"].ToString();
            //������
            outItem.StoreBase.StockNO = dr["������"].ToString();
            return outItem;
        }

        /// <summary>
        /// �������������յķ�ҩƷ����Ӧ����
        /// </summary>
        /// <param name="rowIndex">�������ڵ���</param>
        /// <returns></returns>
        private int FinItemRowIndex(int rowIndex)
        {
            for (int i = rowIndex; i >= 0; i--)
            {
                if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[i, 0].Text != ".")
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// �����Ƿ����������Ŀ
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <returns>�ɹ� �Ѿ����ڷ��õ�index, û���ҵ� -1</returns>
        protected virtual int FindQuitItem(FeeItemList feeItemList)
        {
            //�����ҩƷ,������ҩƷҳ���ұ����Ѿ��˹�����Ŀ
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                for (int i = 0; i < this.fpQuit_SheetDrug.RowCount; i++)
                {
                    if (this.fpQuit_SheetDrug.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    if (this.fpQuit_SheetDrug.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList temp = this.fpQuit_SheetDrug.Rows[i].Tag as FeeItemList;

                        if (temp.RecipeNO == feeItemList.RecipeNO && temp.SequenceNO == feeItemList.SequenceNO)
                        {
                            return i;
                        }
                    }
                }
            }
            else //����Ƿ�ҩƷ,�����˷�ҩƷҳ���ұ����Ѿ��˹�����Ŀ
            {
                for (int i = 0; i < this.fpQuit_SheetUndrug.RowCount; i++)
                {
                    if (this.fpQuit_SheetUndrug.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    if (this.fpQuit_SheetUndrug.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList temp = this.fpQuit_SheetUndrug.Rows[i].Tag as FeeItemList;

                        if (temp.RecipeNO == feeItemList.RecipeNO && temp.SequenceNO == feeItemList.SequenceNO)
                        {
                            return i;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// ����δ����Ŀ
        /// </summary>
        /// <param name="feeItemList">��Ŀ��Ϣʵ��</param>
        /// <returns>�ɹ� ��ǰ�� ʧ�� null</returns>
        protected virtual DataRow FindUnquitItem(FeeItemList feeItemList)
        {
            DataRow rowFind = null;

            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                rowFind = dtDrug.Rows.Find(new object[] { feeItemList.RecipeNO, feeItemList.SequenceNO });
            }
            else
            {
                rowFind = dtUndrug.Rows.Find(new object[] { feeItemList.RecipeNO, feeItemList.SequenceNO });
            }

            return rowFind;
        }

        /// <summary>
        /// ���һ��������Ŀ
        /// </summary>
        /// <param name="feeItemList">��Ŀ��Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetNewQuitItem(FeeItemList feeItemList)
        {
            //ҩƷ
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.RowCount, 1);

                int index = this.fpQuit_SheetDrug.RowCount - 1;

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.ItemName, feeItemList.Item.Name);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Specs, feeItemList.Item.Specs);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Price, feeItemList.Item.Price);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, feeItemList.NoBackQty);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Unit, feeItemList.Item.PriceUnit);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2));
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.FeeDate, feeItemList.FeeOper.OperTime);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsConfirm, feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? true : false);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsApply, false);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.RecipeNO, feeItemList.RecipeNO);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.SequuenceNO, feeItemList.SequenceNO.ToString());
                //�������ϴ�����
                feeItemList.CancelRecipeNO = feeItemList.RecipeNO;
                //���������ڲ�������ˮ��
                feeItemList.CancelSequenceNO = feeItemList.SequenceNO;
                this.fpQuit_SheetDrug.Rows[index].Tag = feeItemList;

            }
            else
            {
                this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);

                int index = this.fpQuit_SheetUndrug.RowCount - 1;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, feeItemList.Item.Name);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(feeItemList.Item.MinFee.ID));
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Price, feeItemList.Item.Price);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, feeItemList.NoBackQty);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Unit, feeItemList.Item.PriceUnit);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2));
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsConfirm, feeItemList.IsConfirmed);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsApply, false);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.RecipeNO, feeItemList.RecipeNO);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.SequuenceNO, feeItemList.SequenceNO);
                FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                if (deptInfo == null)
                {
                    deptInfo = new FS.HISFC.Models.Base.Department();
                    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                }
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ExecDept, deptInfo.Name);
                //�������ϴ�����
                feeItemList.CancelRecipeNO = feeItemList.RecipeNO;
                //���������ڲ�������ˮ��
                feeItemList.CancelSequenceNO = feeItemList.SequenceNO;
                this.fpQuit_SheetUndrug.Rows[index].Tag = feeItemList;
            }

            return 1;
        }

        /// <summary>
        /// ���һ���Ѿ����ڵ��˷���Ϣ
        /// </summary>
        /// <param name="feeItemList">������Ϣʵ��</param>
        /// <param name="index">�ҵ����Ѿ����ڵ��˷Ѽ�¼����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetExistQuitItem(FeeItemList feeItemList, int index)
        {
            //ҩƷ
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                FeeItemList temp = this.fpQuit_SheetDrug.Rows[index].Tag as FeeItemList;

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, feeItemList.NoBackQty + temp.NoBackQty);

                temp.NoBackQty += feeItemList.NoBackQty;

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * temp.NoBackQty / feeItemList.Item.PackQty, 2));
            }
            else
            {
                FeeItemList temp = this.fpQuit_SheetUndrug.Rows[index].Tag as FeeItemList;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, feeItemList.NoBackQty + temp.NoBackQty);

                temp.NoBackQty += feeItemList.NoBackQty;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * temp.NoBackQty, 2));
                SetFeeItemList(temp, feeItemList);
            }

            return 1;
        }

        private void SetFeeItemList(FeeItemList temp, FeeItemList feeItemList)
        {
            if (feeItemList.MateList.Count == 0)
                return;
            if (temp.MateList.Count == 0)
            {
                temp.MateList.Add(feeItemList.MateList[0]);
            }
            else
            {

                foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
                {
                    bool isFind = false;
                    foreach (HISFC.Models.FeeStuff.Output tempItem in temp.MateList)
                    {
                        if (tempItem.ID == outItem.ID && tempItem.StoreBase.StockNO == outItem.StoreBase.StockNO)
                        {
                            isFind = true;
                            tempItem.ReturnApplyNum += outItem.ReturnApplyNum;
                        }
                    }
                    if (!isFind)
                    {
                        temp.MateList.Add(outItem);
                    }
                }

            }
        }

        /// <summary>
        /// �������б�ֵ
        /// </summary>
        /// <param name="feeItemList">������Ŀ��Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetQuitItem(FeeItemList feeItemList)
        {
            int findIndex = -1;

            findIndex = this.FindQuitItem(feeItemList);

            //û���ҵ�,˵��û���˷Ѳ���
            if (findIndex == -1)
            {
                this.SetNewQuitItem(feeItemList.Clone());
            }
            else//�Ѿ��������˷���Ϣ 
            {
                this.SetExistQuitItem(feeItemList.Clone(), findIndex);
            }

            return 1;
        }

        /// <summary>
        /// �˷Ѳ���
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitOperation(FeeItemList feeItemList)
        {
            DataRow findRow = this.FindUnquitItem(feeItemList.Clone());

            if (findRow == null)
            {
                MessageBox.Show("������Ŀ����!");

                return -1;
            }

            decimal orgNoBackQty = NConvert.ToDecimal(findRow["��������"]);

            if (orgNoBackQty < feeItemList.NoBackQty)
            {
                MessageBox.Show(Language.Msg("�����������ܴ���") + orgNoBackQty.ToString());

                return -1;
            }

            //findRow["��������"] = NConvert.ToDecimal(findRow["��������"]) - feeItemList.NoBackQty;
            //findRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(findRow["��������"]) / feeItemList.Item.PackQty;

            int index = this.fpUnQuit.ActiveSheet.ActiveRowIndex;
            //ȫ�����ʷ�ҩƷ�Ŀ�������Ϊ���ʵ�����
            if (this.fpUnQuit.ActiveSheet.RowHeader.Cells[index, 0].Text == "." && this.ckbAllQuit.Checked)
            {
                feeItemList.NoBackQty = feeItemList.MateList[0].ReturnApplyNum;
            }
            findRow["��������"] = NConvert.ToDecimal(findRow["��������"]) - feeItemList.NoBackQty;

            findRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(findRow["��������"]) / feeItemList.Item.PackQty;
            //ͬ����ҩƷ����
            UpdateFpData(findRow, false);

            #region ͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            string rowHeader = string.Empty;
            DataRow mateRow = null;
            if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
            {

                foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
                {
                    mateRow = FindMateRow(outItem);
                    if (mateRow == null)
                    {
                        MessageBox.Show("������Ŀ����!");
                        return -1;
                    }
                    mateRow["��������"] = NConvert.ToDecimal(mateRow["��������"]) - outItem.ReturnApplyNum;
                    mateRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(mateRow["��������"]) / feeItemList.Item.PackQty;
                    UpdateFpData(mateRow, true);
                }
            }
            #endregion

            this.SetQuitItem(feeItemList.Clone());

            return 1;
        }

        /// <summary>
        /// ���·�ҩƷFarpoint����
        /// </summary>
        /// <param name="dr">��ҩƷ����</param>
        private void UpdateFpData(DataRow dr, bool isMate)
        {
            //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            string stockNo = string.Empty;
            string outNo = string.Empty;
            string recipeNO = string.Empty;
            string recipeSequence = string.Empty;
            bool isFind = false;
            for (int i = 0; i < fpUnQuit_SheetUndrug.Rows.Count; i++)
            {
                if (isMate)
                {
                    stockNo = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������", this.fpUnQuit_SheetUndrug));
                    outNo = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������ˮ��", this.fpUnQuit_SheetUndrug));
                    if (stockNo == dr["������"].ToString() && outNo == dr["������ˮ��"].ToString())
                    {
                        isFind = true;
                    }
                }
                else
                {
                    recipeNO = fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������", this.fpUnQuit_SheetUndrug));
                    recipeSequence = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������ˮ��", this.fpUnQuit_SheetUndrug));
                    if (recipeNO == dr["������"].ToString() && recipeSequence == dr["������ˮ��"].ToString())
                    {
                        isFind = true;
                    }
                }
                if (isFind)
                {
                    this.fpUnQuit_SheetUndrug.Cells[i, this.FindColumn("��������", this.fpUnQuit_SheetUndrug)].Text = dr["��������"].ToString();
                    this.fpUnQuit_SheetUndrug.Cells[i, this.FindColumn("���", this.fpUnQuit_SheetUndrug)].Text = dr["���"].ToString();
                    return;
                }
            }

        }

        /// <summary>
        /// �������ʷ�ҩƷ
        /// </summary>
        /// <param name="outItem">���ʳ����¼</param>
        /// <returns></returns>
        private DataRow FindMateRow(HISFC.Models.FeeStuff.Output outItem)
        {
            string outNo = string.Empty;
            string stockNo = string.Empty;

            outNo = outItem.ID;
            stockNo = outItem.StoreBase.StockNO;
            return FindMateRow(stockNo, outNo);

        }
        /// <summary>
        /// �������ʷ�ҩƷ
        /// </summary>
        /// <param name="stockNo">������</param>
        /// <param name="outNo">������ˮ��</param>
        /// <returns></returns>
        private DataRow FindMateRow(string stockNo, string outNo)
        {
            DataRow dr = dtMate.Rows.Find(new object[] { stockNo, outNo });
            return dr;
        }

        /// <summary>
        /// ȡ���˷Ѳ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int CancelQuitOperation()
        {
            if (this.fpQuit.ActiveSheet.RowCount == 0)
            {
                return -1;
            }

            int index = this.fpQuit.ActiveSheet.ActiveRowIndex;

            object quitItem = this.fpQuit.ActiveSheet.Rows[index].Tag;
            if (quitItem == null)
            {
                return -1;
            }
            //������˷���Ŀ(��������)
            if (quitItem.GetType() == typeof(FeeItemList))
            {
                FeeItemList feeItemList = this.fpQuit.ActiveSheet.Rows[index].Tag as FeeItemList;

                DataRow rowFind = this.FindUnquitItem(feeItemList);
                if (rowFind == null)
                {
                    MessageBox.Show("����δ����Ŀ����");

                    return -1;
                }

                rowFind["��������"] = NConvert.ToDecimal(rowFind["��������"]) + feeItemList.NoBackQty;
                rowFind["���"] = feeItemList.Item.Price * NConvert.ToDecimal(rowFind["��������"]) / feeItemList.Item.PackQty;

                UpdateFpData(rowFind, false);

                #region ͬ��fpUnQuit_SheetUndrug��DataTable�е�����
                //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
                string rowHeader = string.Empty;
                DataRow mateRow = null;
                if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                {

                    foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
                    {
                        mateRow = FindMateRow(outItem);
                        if (mateRow == null)
                        {
                            MessageBox.Show("������Ŀ����!");
                            return -1;
                        }
                        mateRow["��������"] = NConvert.ToDecimal(mateRow["��������"]) + outItem.ReturnApplyNum;
                        mateRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(mateRow["��������"]) / feeItemList.Item.PackQty;
                        UpdateFpData(mateRow, true);
                    }
                }
                #endregion
                this.fpQuit.ActiveSheet.Rows.Remove(index, 1);
                //feeItemList.NoBackQty = 0;
                //this.fpQuit.ActiveSheet.SetValue(index, (int)DrugColumns.Qty, 0);
                //this.fpQuit.ActiveSheet.SetValue(index, (int)DrugColumns.Cost, 0);
            }

            return 1;
        }

        /// <summary>
        /// ��֤�Ϸ���
        /// </summary>
        /// <returns>�ɹ� True ʧ�� false</returns>
        protected virtual bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveApply()
        {
            //��֤�Ϸ���
            if (!this.IsValid())
            {
                return -1;
            }

            List<FeeItemList> feeItemLists = this.GetConfirmItem();

            if (feeItemLists.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з��ÿ���!"));

                return -1;
            }

            //��ʼ����
            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string errMsg = string.Empty;//������Ϣ
            int returnValue = 0;//����ֵ
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //����˷������
            string applyBillCode = this.GetApplyBillCode(ref errMsg);
            if (applyBillCode == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errMsg);

                return -1;
            }

            //ѭ�������˷�����
            foreach (FeeItemList feeItemList in feeItemLists)
            {
                //�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }

                FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
                if (feeItemListTemp == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!" + this.inpatientManager.Err));

                    return -1;
                }
                //���˷ѵ�����д��¼
                //if (feeItemListTemp.Item.IsPharmacy && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    if (feeItemList.Item.User01 == "1")
                    {
                        feeItemList.User01 = "��סԺ��";
                    }
                    else
                    {
                        feeItemList.User01 = "��ҩ��";
                    }
                }
                else
                {
                    feeItemList.User01 = "��סԺ��";
                }
                if (feeItemList.Memo != "OLD")
                {
                    feeItemList.User02 = applyBillCode;
                }
                //���Ѿ���������˷����벻���д���
                if (feeItemList.Memo == "OLD")
                {
                    continue;
                }

                //���·��ñ��еĿ��������ֶ�
                //�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errMsg);

                    return -1;
                }

                //��ʱ��Ŀ�����˷������
                feeItemListTemp.User02 = applyBillCode;

                //�����ҩƷ�����Ѿ���ҩ���������ҩ���룻��������˷����롣
                //if (feeItemListTemp.Item.IsPharmacy && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    {
                        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    }
                    if (this.phamarcyIntegrate.ApplyOutReturn(this.patientInfo, feeItemListTemp, nowTime) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        MessageBox.Show(Language.Msg("��ҩ����ʧ��!" + this.phamarcyIntegrate.Err));
                        return -1;
                    }
                }
                else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�����
                {
                    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    if (this.returnApplyManager.Apply(this.patientInfo, feeItemListTemp, nowTime) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�����˷�����ʧ��!") + this.returnApplyManager.Err);

                        return -1;
                    }

                    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                    //if (feeItemListTemp.Item.IsPharmacy)
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        //ȡ��ҩ�����¼���ж���״̬�Ƿ���������������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.phamarcyIntegrate.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (applyOut == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���������Ϣ����!") + this.phamarcyIntegrate.Err);

                            return -1;
                        }

                        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                        if (applyOut.ID == string.Empty)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!"));

                            return -1;
                        }

                        //����
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���ѱ���������Ա�˷ѣ���ˢ�µ�ǰ����!"));

                            return -1;
                        }

                        //���ϰ�ҩ����
                        returnValue = this.phamarcyIntegrate.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ϰ�ҩ�������!") + phamarcyIntegrate.Err);

                            return -1;
                        }
                        if (returnValue == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������"));

                            return -1;
                        }

                        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                        {
                            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.phamarcyIntegrate.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���������Ϣ����!") + this.phamarcyIntegrate.Err);

                                return -1;
                            }

                            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬
                            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                            if (this.phamarcyIntegrate.ApplyOut(applyOutTemp) != 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���²��뷢ҩ�������!") + this.phamarcyIntegrate.Err);

                                return -1;
                            }
                        }
                    }
                }

                //if (feeItemListTemp.Item.IsPharmacy)
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                {
                    ArrayList patientDrugStorageList = this.phamarcyIntegrate.QueryStorageList(this.patientInfo.ID, feeItemListTemp.Item.ID);
                    if (patientDrugStorageList == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�ж��Ƿ���ڻ��߿��ʱ����") + this.phamarcyIntegrate.Err);

                        return -1;
                    }
                    //�Ի��߿������������
                    if (patientDrugStorageList.Count > 0)
                    {
                        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                        storageBase.Quantity = -storageBase.Quantity;
                        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                        storageBase.PrivType = "AAAA";	//��¼סԺ�˷ѱ��
                        if (storageBase.ID == string.Empty)
                        {
                            storageBase.ID = applyBillCode;
                            storageBase.SerialNO = 0;
                        }

                        if (this.phamarcyIntegrate.UpdateStorage(storageBase) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���»��߿�����!") + this.phamarcyIntegrate.Err);

                            return -1;
                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�!"));

            return 1;
        }

        /// <summary>
        /// ������Ŀ�Ŀ�������
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int UpdateNoBackQty(FeeItemList feeItemList, ref string errMsg)
        {
            int returnValue = 0;

            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                //���·�����ϸ���еĿ�������
                returnValue = this.inpatientManager.UpdateNoBackQtyForDrug(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.Qty, feeItemList.BalanceState);
                if (returnValue == -1)
                {
                    errMsg = Language.Msg("����ҩƷ������������!") + this.inpatientManager.Err;

                    return -1;
                }
            }
            else
            {
                //���·�����ϸ���еĿ�������
                returnValue = this.inpatientManager.UpdateNoBackQtyForUndrug(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.Qty, feeItemList.BalanceState);
                if (returnValue == -1)
                {
                    errMsg = Language.Msg("���·�ҩƷ������������!") + this.inpatientManager.Err;

                    return -1;
                }
            }
            //����ҩ���������ж�
            if (returnValue == 0)
            {
                errMsg = Language.Msg("��Ŀ��") + feeItemList.Item.Name + Language.Msg("���Ѿ����˷ѣ������ظ��˷ѡ�");

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����˷������
        /// </summary>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ�  ����˷������ ʧ�� null</returns>
        private string GetApplyBillCode(ref string errMsg)
        {
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                errMsg = Language.Msg("��ȡ�˷����뷽�ų���!");

                return null;
            }

            return applyBillCode;
        }

        /// <summary>
        /// �˷Ѳ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveFee()
        {
            List<FeeItemList> feeItemLists = this.GetConfirmItem();
            List<FS.HISFC.Models.Fee.ReturnApply> returnApplys = this.GetRetrunApplyItem();

            if (feeItemLists.Count <= 0 && returnApplys.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з��ÿ���!"));

                return -1;
            }

            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.MessageType = FS.HISFC.Models.Base.MessType.N;//����ʾǷ��
            if (returnApplys.Count > 0)
            {
                this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }

            foreach (FeeItemList feeItemList in feeItemLists)
            {
                //if (feeItemList.Item.IsPharmacy) 
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    if (this.phamarcyIntegrate.CancelApplyOut(feeItemList.Clone()) == -1)
                    {
                        this.feeIntegrate.Rollback();

                        MessageBox.Show(Language.Msg("�˷�ʧ��! ��ҩƷ�����Ѿ���ҩ������������סԺ�ţ�ˢ������!") + this.phamarcyIntegrate.Err);

                        return -1;
                    }
                }

                if (this.feeIntegrate.QuitItem(this.patientInfo, feeItemList.Clone()) <= 0)
                {
                    this.feeIntegrate.Rollback();

                    MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);

                    return -1;
                }
            }

            foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in returnApplys)
            {
                returnApply.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                returnApply.ConfirmOper.ID = this.returnApplyManager.Operator.ID;
                returnApply.ConfirmOper.OperTime = this.returnApplyManager.GetDateTimeFromSysDateTime();

                if (this.returnApplyManager.ConfirmApply(returnApply) <= 0)
                {
                    this.feeIntegrate.Rollback();

                    MessageBox.Show(Language.Msg("��׼�˷�����ʧ��!,���������б仯") + this.returnApplyManager.Err);

                    return -1;
                }

                FeeItemList feeTemp = returnApply as FeeItemList;

                if (this.feeIntegrate.QuitItem(this.patientInfo, feeTemp.Clone()) == -1)
                {
                    this.feeIntegrate.Rollback();

                    MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);

                    return -1;
                }
            }


            this.feeIntegrate.Commit();

            MessageBox.Show("�˷ѳɹ�!");

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ����˷ѵ���Ŀ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ���� ʧ�� null</returns>
        private List<FeeItemList> GetConfirmItem()
        {
            //ArrayList returnPharmacyApplys = new ArrayList(); 
            //ArrayList returnItemApplys = new ArrayList(); 
            //switch (this.itemType)
            //{
            //    case ItemTypes.Pharmarcy:
            //        returnPharmacyApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, true);

            //        if (returnPharmacyApplys == null)
            //        {
            //            MessageBox.Show("����˷�������Ϣ����");
            //            return null;
            //        } 
            //        break;

            //    case ItemTypes.Undrug:
            //        returnItemApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, false);

            //        if (returnItemApplys == null)
            //        {
            //            MessageBox.Show("����˷�������Ϣ����");

            //            return null;
            //        }

            //        break;

            //    case ItemTypes.All:
            //        returnPharmacyApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, true);

            //        if (returnPharmacyApplys == null)
            //        {
            //            MessageBox.Show("����˷�������Ϣ����");
            //            return null;
            //        }
            //        returnItemApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, false);

            //        if (returnItemApplys == null)
            //        {
            //            MessageBox.Show("����˷�������Ϣ����");
            //            return null;
            //        } 
            //        break;
            //}



            //List<FeeItemList> feeItemLists = new List<FeeItemList>();

            //foreach (FeeItemList objPharm in returnPharmacyApplys)
            //{ 
            //    if (objPharm.NoBackQty > 0)
            //    {
            //        objPharm.Item.Qty = objPharm.NoBackQty;
            //        objPharm.NoBackQty = 0;
            //        objPharm.FT.TotCost = objPharm.Item.Price * objPharm.Item.Qty / objPharm.Item.PackQty;
            //        objPharm.FT.OwnCost = objPharm.FT.TotCost;
            //        objPharm.IsNeedUpdateNoBackQty = true;

            //        feeItemLists.Add(objPharm);
            //    }
            //}
            //foreach (FeeItemList objItem in returnItemApplys)
            //{
            //    if (objItem.NoBackQty > 0)
            //    {
            //        objItem.Item.Qty = objItem.NoBackQty;
            //        objItem.NoBackQty = 0;
            //        objItem.FT.TotCost = objItem.Item.Price * objItem.Item.Qty / objItem.Item.PackQty;
            //        objItem.FT.OwnCost = objItem.FT.TotCost;
            //        objItem.IsNeedUpdateNoBackQty = true;

            //        feeItemLists.Add(objItem);
            //    }
            //}

            List<FeeItemList> feeItemLists = new List<FeeItemList>();

            for (int j = 0; j < this.fpQuit.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpQuit.Sheets[j].RowCount; i++)
                {
                    if (this.fpQuit.Sheets[j].Rows[i].Tag != null && this.fpQuit.Sheets[j].Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList feeItemList = this.fpQuit.Sheets[j].Rows[i].Tag as FeeItemList;
                        if (feeItemList.NoBackQty > 0)
                        {
                            feeItemList.Item.Qty = feeItemList.NoBackQty;
                            feeItemList.NoBackQty = 0;
                            feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                            feeItemList.IsNeedUpdateNoBackQty = true;

                            feeItemLists.Add(feeItemList);
                        }
                    }
                }
            }

            return feeItemLists;
        }

        /// <summary>
        /// ����˷ѵ���Ŀ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ���� ʧ�� null</returns>
        private List<FS.HISFC.Models.Fee.ReturnApply> GetRetrunApplyItem()
        {
            List<FS.HISFC.Models.Fee.ReturnApply> feeItemLists = new List<FS.HISFC.Models.Fee.ReturnApply>();

            for (int j = 0; j < this.fpQuit.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpQuit.Sheets[j].RowCount; i++)
                {
                    if (this.fpQuit.Sheets[j].Rows[i].Tag != null && this.fpQuit.Sheets[j].Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        FS.HISFC.Models.Fee.ReturnApply feeItemList = this.fpQuit.Sheets[j].Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;

                        FeeItemList feeTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
                        if (feeTemp == null)
                        {
                            MessageBox.Show(Language.Msg("������Ŀ����!") + feeItemList.Item.Name + this.inpatientManager.Err);
                            continue;
                        }
                        feeItemList.Item.MinFee = feeTemp.Item.MinFee;
                        feeItemList.NoBackQty = 0;
                        feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                        feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                        feeItemList.IsNeedUpdateNoBackQty = false;
                        feeItemList.RecipeOper.ID = this.inpatientManager.Operator.ID;
                        feeItemList.RecipeOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.inpatientManager.Operator).Dept.ID;
                        feeItemList.CancelOper.ID = this.inpatientManager.Operator.ID;
                        feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                        feeItemList.FeeOper.ID = this.inpatientManager.Operator.ID;

                        feeItemLists.Add(feeItemList);

                    }
                }
            }

            return feeItemLists;
        }

        /// <summary>
        /// ��ѯ���˷���Ŀ
        /// </summary>
        /// <param name="isNoBackQtyOverZero">�Ƿ������������0</param>
        /// <returns>�ɹ�:��ѯ���˷���Ŀ ʧ�� null û�в��ҵ����� ArrayList.Count = 0</returns>
        protected virtual ArrayList QueryUnQuitItems(bool isNoBackQtyOverZero)
        {
            ArrayList noBackQtyOverZeroFeeItemList = new ArrayList();
            ArrayList allFeeItemList = new ArrayList();

            for (int i = 0; i < this.fpUnQuit.Sheets.Count; i++)
            {
                for (int j = 0; j < this.fpUnQuit.Sheets[i].RowCount; j++)
                {
                    decimal tempNoBackQty = NConvert.ToDecimal(this.fpUnQuit.Sheets[i].GetValue(j, this.FindColumn("��������", this.fpUnQuit.Sheets[i])).ToString());

                    FeeItemList feeItemList = new FeeItemList();
                    if (this.fpUnQuit.Sheets[i].RowHeader.Cells[j, 0].Text == ".")
                        continue;
                    feeItemList.RecipeNO = this.fpUnQuit.Sheets[i].GetValue(j, this.FindColumn("������", this.fpUnQuit.Sheets[i])).ToString();
                    feeItemList.SequenceNO = NConvert.ToInt32(this.fpUnQuit.Sheets[i].GetValue(j, this.FindColumn("������ˮ��", this.fpUnQuit.Sheets[i])).ToString());
                    feeItemList.Item.ItemType = this.fpUnQuit.Sheets[i] == this.fpUnQuit_SheetDrug ? EnumItemType.Drug : EnumItemType.UnDrug;

                    if (tempNoBackQty > 0)
                    {
                        noBackQtyOverZeroFeeItemList.Add(feeItemList);
                    }

                    allFeeItemList.Add(feeItemList);
                }
            }

            if (isNoBackQtyOverZero)
            {
                return noBackQtyOverZeroFeeItemList;
            }
            else
            {
                return allFeeItemList;
            }
        }

        /// <summary>
        /// �����п�
        /// </summary>
        protected virtual void SetColumns()
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn ucSetCol = new FS.HISFC.Components.Common.Controls.ucSetColumn();

            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                ucSetCol.SetDataTable(this.filePathUnQuitDrug, this.fpUnQuit_SheetDrug);
            }
            else
            {
                ucSetCol.SetDataTable(this.filePathUnQuitUndrug, this.fpUnQuit_SheetUndrug);
            }

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucSetCol);
        }

        /// <summary>
        /// �۵���ʾ��������
        /// </summary>
        /// <param name="isExpand"></param>
        /// <param name="index"></param>
        private void ExpandOrCloseRow(bool isExpand, int index)
        {

            for (int i = index; i < fpUnQuit_SheetUndrug.Rows.Count; i++)
            {
                if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[i, 0].Text == "." && this.fpUnQuit_SheetUndrug.Rows[i].Visible == isExpand)
                {
                    this.fpUnQuit_SheetUndrug.Rows[i].Visible = !isExpand;
                }
                else
                {
                    break;
                }
            }
            if (isExpand)
            {
                fpUnQuit_SheetUndrug.RowHeader.Cells[index - 1, 0].Text = "+";
            }
            else
            {
                fpUnQuit_SheetUndrug.RowHeader.Cells[index - 1, 0].Text = "-";
            }
        }
        #endregion

        #region ���з���

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public virtual int Save()
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뻼��!"));

                return -1;
            }

            switch (this.operation)
            {
                case Operations.QuitFee:
                case Operations.Confirm:
                    return this.SaveFee();

                case Operations.Apply:

                    return this.SaveApply();
            }

            return 1;
        }

        /// <summary>
        /// ��պ���
        /// </summary>
        public virtual void Clear()
        {
            this.ClearItemList();

            this.txtItemName.Text = string.Empty;
            this.txtItemName.Tag = null;
            this.txtPrice.Text = "0";
            this.mtbQty.Text = "0";
            this.txtUnit.Text = "0";
            this.txtName.Text = string.Empty;
            this.txtPact.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtFilter.Text = string.Empty;
            this.txtBed.Text = string.Empty;
            this.ucQueryPatientInfo.Text = string.Empty;
            this.ucQueryPatientInfo.txtInputCode.SelectAll();
            this.ucQueryPatientInfo.txtInputCode.Focus();
            this.patientInfo = null;

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
        }

        /// <summary>
        /// �����ʾ�б�
        /// </summary>
        public virtual void ClearItemList()
        {
            this.dtDrug.Clear();
            this.dtUndrug.Clear();
            this.fpQuit_SheetDrug.RowCount = 0;
            this.fpQuit_SheetUndrug.RowCount = 0;
            this.fpUnQuit_SheetDrug.RowCount = 0;
            this.fpUnQuit_SheetUndrug.RowCount = 0;

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
        }

        #endregion

        #region �ؼ�����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ȡ��", "ȡ������������ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("������", "������ʾ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Clear();
                    break;
                case "ȡ��":
                    this.CancelQuitOperation();
                    break;
                case "������":
                    this.SetColumns();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ���߻�����Ϣ
        /// </summary>
        private void ucQueryInpatientNO_myEvent()
        {
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            this.patientInfo = patientTemp;

            this.SetPatientInfomation();
        }

        /// <summary>
        /// Uc��Loade�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ucQuitFee_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                try
                {
                    this.Init();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// ��ȡδ�˷ѵ�ҩƷ,��ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            this.ClearItemList();

            this.Retrive(true);

            this.txtFilter.Focus();
        }

        /// <summary>
        /// �˷��л�ҩƷ,��ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUnQuit_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                if (this.fpQuit.ActiveSheet != null)
                {
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                }
            }
            else
            {
                if (this.fpQuit.ActiveSheet != null)
                {
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetUndrug;
                }
            }
        }

        /// <summary>
        /// �˷��л�ҩƷ,��ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpQuit_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpQuit.ActiveSheet == this.fpQuit_SheetDrug)
            {
                if (this.fpUnQuit.ActiveSheet != null)
                {
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;
                }
            }
            else
            {
                if (this.fpUnQuit.ActiveSheet != null)
                {
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetUndrug;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.SetFilter();
        }

        private void fpUnQuit_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
            }
        }

        private void fpUnQuit_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ChooseUnquitItem();
        }

        private void mtbQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal qty = 0;

                try
                {
                    qty = NConvert.ToDecimal(this.mtbQty.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("������Ϸ������֣�") + ex.Message);
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();

                    return;
                }

                if (qty <= 0)
                {
                    MessageBox.Show(Language.Msg("�˷���������С�ڻ��ߵ���0"));
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();

                    return;
                }

                if (qty != FS.FrameWork.Function.NConvert.ToInt32(qty))
                {
                    MessageBox.Show(Language.Msg("�˷���������������С�������������룡"));
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();
                    return;
                }

                if (this.txtItemName.Tag == null)
                {
                    return;
                }

                FeeItemList feeItemList = this.txtItemName.Tag as FeeItemList;

                feeItemList.NoBackQty = qty;

                //�˵�������
                if (feeItemList.MateList.Count > 0)
                {
                    feeItemList.MateList[0].ReturnApplyNum = qty;
                }

                this.QuitOperation(feeItemList);

                this.txtFilter.Focus();
            }
        }

        private void fpQuit_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CancelQuitOperation();
        }

        private void dtpBeginTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtpEndTime.Focus();
            }
        }

        private void dtpEndTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnRead.Focus();
            }
        }

        private void btnRead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.txtFilter.Focus();
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.fpUnQuit.ActiveSheet.ActiveRowIndex--;
                this.fpUnQuit.ActiveSheet.AddSelection(this.fpUnQuit.ActiveSheet.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpUnQuit.ActiveSheet.ActiveRowIndex++;
                this.fpUnQuit.ActiveSheet.AddSelection(this.fpUnQuit.ActiveSheet.ActiveRowIndex, 0, 1, 0);
            }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ҩƷ�˷�����Ϣ
        /// </summary>
        protected enum DrugColumns
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ���
            /// </summary>
            Specs,

            /// <summary>
            /// ����
            /// </summary>
            Price,

            /// <summary>
            /// �˷�����
            /// </summary>
            Qty,

            /// <summary>
            /// ��λ
            /// </summary>
            Unit,

            /// <summary>
            /// ���
            /// </summary>
            Cost,

            /// <summary>
            /// �Ʒ�����
            /// </summary>
            FeeDate,

            /// <summary>
            /// �Ƿ�ҩ
            /// </summary>
            IsConfirm,

            /// <summary>
            /// �Ƿ��˷�����
            /// </summary>
            IsApply,
            /// <summary>
            /// ������ˮ��
            /// </summary>
            RecipeNO,
            /// <summary>
            /// �����ڲ���ˮ��
            /// </summary>
            SequuenceNO
        }

        /// <summary>
        /// ҩƷ�˷�����Ϣ
        /// </summary>
        protected enum UndrugColumns
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ��������
            /// </summary>
            FeeName,

            /// <summary>
            /// ����
            /// </summary>
            Price,

            /// <summary>
            /// �˷�����
            /// </summary>
            Qty,

            /// <summary>
            /// ��λ
            /// </summary>
            Unit,

            /// <summary>
            /// ���
            /// </summary>
            Cost,

            /// <summary>
            /// ִ�п���
            /// </summary>
            ExecDept,

            /// <summary>
            /// �Ƿ�ҩ
            /// </summary>
            IsConfirm,

            /// <summary>
            /// �Ƿ��˷�����
            /// </summary>
            IsApply,
            /// <summary>
            /// ������ˮ��
            /// </summary>
            RecipeNO,
            /// <summary>
            /// �����ڲ���ˮ��
            /// </summary>
            SequuenceNO

        }

        /// <summary>
        /// �˷ѹ���
        /// </summary>
        public enum Operations
        {
            /// <summary>
            /// ֱ���˷�
            /// </summary>
            QuitFee = 0,

            /// <summary>
            /// �˷�����
            /// </summary>
            Apply,

            /// <summary>
            /// �˷�ȷ��
            /// </summary>
            Confirm,
        }

        /// <summary>
        /// �ɲ�����Ŀ����
        /// </summary>
        public enum ItemTypes
        {
            /// <summary>
            /// ����
            /// </summary>
            All = 0,

            /// <summary>
            /// ҩƷ
            /// </summary>
            Pharmarcy,

            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug
        }

        #endregion

        private void fpUnQuit_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.RowHeader && this.fpUnQuit_SheetUndrug.RowHeader.Cells[e.Row, 0].Text == "+" &&
                this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                ExpandOrCloseRow(false, e.Row + 1);
                return;
            }
            if (e.RowHeader && fpUnQuit_SheetUndrug.RowHeader.Cells[e.Row, 0].Text == "-" &&
                this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                ExpandOrCloseRow(true, e.Row + 1);
                return;
            }

        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (string.IsNullOrEmpty(this.operationPriv))
            {
                return 1;
            }
            else
            {
                //����ǹ���Ա�������ֱ�ӽ���
                if (((FS.HISFC.Models.Base.Employee)inpatientManager.Operator).IsManager)
                {
                    return 1;
                }

                string[] privs = this.operationPriv.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                if (privs.Length == 0)
                {
                    return 1;
                }
                else if (privs.Length == 1)//ֻ�ж���û�ж���Ȩ��
                {
                    if (CommonController.Instance.JugePrive(privs[0]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
                else
                {
                    string class2Code = privs[0];
                    string class3Code = privs[1];
                    if (CommonController.Instance.JugePrive(privs[0], privs[1]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}
