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
using FS.HISFC.BizLogic.Privilege.Service;
using FS.HISFC.BizLogic.Privilege;
using FS.HISFC.BizLogic.Privilege.Model;
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
    public partial class ucConfirmQuitFee : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
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
        public ucConfirmQuitFee()
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
        /// �ն�ȷ��
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Terminal.Confirm terminalIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();


        private bool isNeedPasswordConfirmation = false;
        private bool isPrintReceipt = false;
        private bool isCanQuitOtherDeptFee = false;

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
        /// �Ƿ�����޸��˷���Ŀ
        /// </summary>
        private bool isChangItem = true;

        /// <summary>
        /// �����շ�ҵ���
        /// </summary>
        private HISFC.BizProcess.Integrate.Material.Material mateIntegrate = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        protected bool isCombItemAllQuit = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449}����


        private Hashtable hsQuitFeeByPackage = new Hashtable();

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
        /// �Ƿ񰴲�����ѯ����
        /// </summary>
        bool isInNurseCode = true;

        /// <summary>
        /// �Ƿ񰴲�����ѯ����// {15CDA661-3D42-4c15-A32B-F88CC1CD7907}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ񰴲�����ѯ����")]
        public bool IsInNurseCode
        {
            set { isInNurseCode = value; }
            get { return isInNurseCode; }
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
                switch (this.itemType)
                {
                    case ItemTypes.Pharmarcy:
                        this.fpQuit_SheetDrug.Visible = true;
                        this.fpQuit_SheetUndrug.Visible = false;
                        break;
                    case ItemTypes.Undrug:
                        this.fpQuit_SheetDrug.Visible = false;
                        this.fpQuit_SheetUndrug.Visible = true;
                        break;
                    case ItemTypes.All:
                    default:
                        this.fpQuit_SheetDrug.Visible = true;
                        this.fpQuit_SheetUndrug.Visible = true;
                        break;
                }
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
        /// �Ƿ��ӡ�˷�ƾ֤
        /// </summary>
        public bool IsPrintReceipt
        {
            get { return isPrintReceipt; }
            set { isPrintReceipt = value; }
        }
        /// <summary>
        /// �˷��Ƿ���Ҫ����У��
        /// </summary>
        public bool IsNeedPasswordConfirmation
        {
            get { return isNeedPasswordConfirmation; }
            set { isNeedPasswordConfirmation = value; }
        }
        /// <summary>
        /// �Ƿ����ȷ���˷ѷǱ����ҷ���
        /// </summary>
        public bool IsCanQuitOtherDeptFee
        {
            get { return isCanQuitOtherDeptFee; }
            set { isCanQuitOtherDeptFee = value; }
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

        [Category("�ؼ�����"), Description("�Ƿ�����޸��˷���Ŀ")]
        public bool IsChangItem
        {
            get
            {
                return isChangItem;
            }
            set
            {
                isChangItem = value;
            }
        }

        /// <summary>
        /// �Ƿ������������Ŀ�˷�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ������������Ŀ�˷ѣ�True:���� False:������")]
        private bool isQuitFeeByPackage = false;
        public bool IsQuitFeeByPackage
        {
            get
            {
                return isQuitFeeByPackage;
            }
            set
            {
                isQuitFeeByPackage = value;
            }
        }

        [Category("�ؼ�����"), Description("סԺ�������Ĭ������0סԺ�ţ�5����")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryPatientInfo.DefaultInputType;
            }
            set
            {
                this.ucQueryPatientInfo.DefaultInputType = value;
            }
        }

        [Category("�ؼ�����"), Description("�����Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryPatientInfo.PatientInState;
            }
            set
            {
                this.ucQueryPatientInfo.PatientInState = value;
            }
        }

        string hideMedDeptQuitFee = "";
        /// <summary>
        /// ����ʾҩ��������Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("����ʾ��ҩ��������Ŀ�����ҩ���ԡ�;������")]
        public string HideMedDeptQuitFee
        {
            get
            {
                return this.hideMedDeptQuitFee;
            }
            set
            {
                this.hideMedDeptQuitFee = value;
            }
        }
        #endregion

        #region ˽�з���

        private Hashtable depts = new Hashtable();

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected virtual void SetPatientInfomation()
        {
            this.txtName.Text = this.patientInfo.Name;
            this.txtPact.Text = this.patientInfo.Pact.Name;
            this.txtDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBed.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID;
            //this.dtpBeginTime.Focus();

            //��������
            this.ClearItemList();

            this.Retrive(true);

            this.txtFilter.Focus();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0);
            //this.dtpEndTime.Value = nowTime;

            ArrayList minFeeList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (minFeeList == null)
            {
                MessageBox.Show("�����С���ó���!" + this.managerIntegrate.Err);

                return -1;
            }

            this.objectHelperMinFee.ArrayObject = minFeeList;
            feeIntegrate.MessageType = FS.HISFC.Models.Base.MessType.N;
            this.fpQuit_SheetDrug.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Cell;
            this.fpQuit_SheetDrug.Columns[0].Locked = false;
            //this.InitFp();
            if (!IsChangItem)
            {
                this.fpQuit_SheetDrug.Columns[0].Locked = true;
                this.fpQuit_SheetUndrug.Columns[0].Locked = true;
            }

            //���Ҳ��������Ŀ���
            depts.Clear();
            ArrayList alDept = managerIntegrate.QueryDepartment((this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Nurse.ID);
            if (alDept != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    if (depts.ContainsKey(obj.ID) == false)
                    {
                        depts.Add(obj.ID, null);
                    }
                }
            }

            if (depts.ContainsKey((this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID) == false)
            {
                depts.Add((this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, null);
            }

            return 1;
        }

        ///// <summary>
        ///// ����ҩƷ�б�
        ///// </summary>
        ///// <param name="drugList"></param>
        //protected virtual void SetDrugList(ArrayList drugList)
        //{
        //    foreach (FeeItemList feeItemList in drugList)
        //    {
        //        DataRow row = this.dtDrug.NewRow();

        //        //��ȡҩƷ������Ϣ,������ʱΪ�˻��ƴ����
        //        FS.HISFC.Models.Base.Item phamarcyItem = this.phamarcyIntegrate.GetItem(feeItemList.Item.ID);
        //        if (phamarcyItem == null)
        //        {
        //            phamarcyItem = new FS.HISFC.Models.Base.Item();

        //        }

        //        if (phamarcyItem.PackQty == 0) 
        //        {
        //            phamarcyItem.PackQty = 1;
        //        }

        //        feeItemList.Item.IsPharmacy = true;
        //        row["ҩƷ����"] = feeItemList.Item.Name;
        //        row["���"] = feeItemList.Item.Specs;
        //        row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
        //        row["�۸�"] = feeItemList.Item.Price;
        //        row["��������"] = feeItemList.NoBackQty;
        //        row["��λ"] = feeItemList.Item.PriceUnit;
        //        row["���"] = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / phamarcyItem.PackQty, 2);
        //        FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

        //        deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
        //        if (deptInfo == null)
        //        {
        //            deptInfo = new FS.HISFC.Models.Base.Department();
        //            deptInfo.Name = feeItemList.ExecOper.Dept.ID;
        //        }

        //        row["ִ�п���"] = deptInfo.Name;


        //        FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
        //        empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);
        //        if (empl.Name == string.Empty)
        //        {
        //            row["����ҽʦ"] = feeItemList.RecipeOper.ID;
        //        }
        //        else
        //        {
        //            row["����ҽʦ"] = empl.Name;
        //        }

        //        //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
        //        row["��������"] = feeItemList.FeeOper.OperTime;
        //        row["�Ƿ�ҩ"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';

        //        row["����"] = feeItemList.Item.ID;
        //        row["ҽ����"] = feeItemList.Order.ID;
        //        row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
        //        row["������"] = feeItemList.RecipeNO;
        //        row["������ˮ��"] = feeItemList.SequenceNO;

        //        row["ƴ����"] = phamarcyItem.SpellCode;

        //        this.dtDrug.Rows.Add(row);
        //    }
        //}

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
                feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
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

                this.dtUndrug.Rows.Add(row);
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
            if (!isPharmarcy)
            {
                //��������
                ArrayList al = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, HISFC.Models.Base.EnumItemType.MatItem);

                if (al == null)
                {
                    MessageBox.Show("����˷�������Ϣ����");

                    return;
                }
                returnApplys.AddRange(al);
            }

            decimal return_DrugCost = 0;
            decimal return_UndrugCost = 0;

            //Hashtable �����ж��Ƿ�������Ŀ
            Hashtable hsPackageItem = new Hashtable();

            foreach (FS.HISFC.Models.Fee.ReturnApply retrunApply in returnApplys)
            {
                //ҩƷ
                if (retrunApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.RowCount, 1);

                    int index = this.fpQuit_SheetDrug.RowCount - 1;
                    this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.ItemState, false);

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
                    return_DrugCost = return_DrugCost + FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2);
                }
                else
                {
                    #region ����������Ϣ

                    //���ҷ�ҩƷ�������ʵĳ����¼
                    List<HISFC.Models.FeeStuff.Output> outlist = returnApplyManager.QueryOutPutByApplyNo(retrunApply.ID, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    //List<HISFC.Models.Fee.ReturnApplyMet> metApplyList = returnApplyManager.QueryReturnApplyMetByApplyNo(retrunApply.ID, FS.HISFC.Models.Base.CancelTypes.Canceled);

                    retrunApply.MateList = outlist;
                    #endregion

                    if (isInNurseCode)// {15CDA661-3D42-4c15-A32B-F88CC1CD7907}
                    {
                        if (!string.IsNullOrEmpty(((FS.HISFC.Models.RADT.PatientInfo)retrunApply.Patient).PVisit.PatientLocation.NurseCell.ID))
                        {
                            string dept = (this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                            if (((FS.HISFC.Models.RADT.PatientInfo)retrunApply.Patient).PVisit.PatientLocation.NurseCell.ID != dept)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                    else
                    {
                        //�Ǳ����ҷ��ã�����ȷ���˷�
                        if (!this.isCanQuitOtherDeptFee)
                        {
                            if (depts.ContainsKey(retrunApply.ExecOper.Dept.ID) == false)
                            {
                                continue;
                            }
                            //if (retrunApply.ExecOper.Dept.ID != (this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                            //{
                            //    continue;
                            //}
                        }
                    }
                    if (HideMedDeptQuitFee.Contains(retrunApply.ExecOper.Dept.ID))
                    {

                        continue;
                    }

                    if (isQuitFeeByPackage)
                    {
                        if (string.IsNullOrEmpty(retrunApply.UndrugComb.ID) == false)
                        {
                            if (hsPackageItem.ContainsKey(retrunApply.ApplyBillNO +"|"+ retrunApply.FeePack + "|" + retrunApply.TransType.ToString() + "|" + retrunApply.UndrugComb.ID))
                            {
                                ((ArrayList)hsPackageItem[retrunApply.ApplyBillNO +"|"+ retrunApply.FeePack + "|" + retrunApply.TransType.ToString() + "|" + retrunApply.UndrugComb.ID]).Add(retrunApply);
                            }
                            else
                            {
                                //�½�
                                ArrayList al = new ArrayList();
                                al.Add(retrunApply);
                                hsPackageItem[retrunApply.ApplyBillNO +"|"+ retrunApply.FeePack + "|" + retrunApply.TransType.ToString() + "|" + retrunApply.UndrugComb.ID] = al;
                            }
                            continue;
                        }
                    }

                    this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);

                    int index = this.fpQuit_SheetUndrug.RowCount - 1;

                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemState, false);

                    this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, retrunApply.Item.Name);

                    if (retrunApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
                        FS.HISFC.BizLogic.Fee.Item feeItemManager = new FS.HISFC.BizLogic.Fee.Item();
                        undrugInfo = feeItemManager.GetUndrugByCode(retrunApply.Item.ID);
                        this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(undrugInfo.MinFee.ID));
                    }
                    else
                    {
                        FS.HISFC.Models.FeeStuff.MaterialItem mateItem = new FS.HISFC.Models.FeeStuff.MaterialItem();
                        mateItem = mateIntegrate.GetMetItem(retrunApply.Item.ID);
                        if (mateItem == null)
                        {
                            MessageBox.Show("����������Ŀ����" + mateIntegrate.Err);
                            return;
                        }
                        this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(mateItem.MinFee.ID));
                    }
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
                    return_UndrugCost = return_UndrugCost + FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2);
                }
            }

            if (isQuitFeeByPackage)
            {
                if (hsPackageItem.Count > 0)
                {
                    foreach (DictionaryEntry de in hsPackageItem)
                    {
                        string packagecode = de.Key.ToString().Split('|')[3];
                        ArrayList al = de.Value as ArrayList;
                        if (al != null && al.Count > 0)
                        {
                            FS.HISFC.Models.Fee.ReturnApply retrunApply = al[0] as FS.HISFC.Models.Fee.ReturnApply;

                            decimal sumCost = 0.00M;
                            foreach (FS.HISFC.Models.Fee.ReturnApply feeItemList in al)
                            {
                                if (feeItemList.Item.PackQty == 0)
                                {
                                    feeItemList.Item.PackQty = 1;
                                }
                                sumCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                            }

                            //���Ҹ�����Ŀ
                            FS.HISFC.Models.Fee.Item.Undrug undrug = this.feeIntegrate.GetUndrugByCode(packagecode);

                            undrug.Price = this.feeIntegrate.GetUndrugCombPrice(packagecode);

                            retrunApply.Item = undrug;
                            retrunApply.Item.Qty = 1;
                            if (retrunApply.Item.PackQty == 0)
                            {
                                retrunApply.Item.PackQty = 1;
                            }
                            retrunApply.Item.Price = sumCost;

                            this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);
                            int index = this.fpQuit_SheetUndrug.RowCount - 1;
                            this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemState, false);
                            this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, retrunApply.Item.Name);
                            if (retrunApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
                                FS.HISFC.BizLogic.Fee.Item feeItemManager = new FS.HISFC.BizLogic.Fee.Item();
                                undrugInfo = feeItemManager.GetUndrugByCode(retrunApply.Item.ID);
                                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(undrugInfo.MinFee.ID));
                            }
                            else
                            {
                                FS.HISFC.Models.FeeStuff.MaterialItem mateItem = new FS.HISFC.Models.FeeStuff.MaterialItem();
                                mateItem = mateIntegrate.GetMetItem(retrunApply.Item.ID);
                                if (mateItem == null)
                                {
                                    MessageBox.Show("����������Ŀ����" + mateIntegrate.Err);
                                    return;
                                }
                                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(mateItem.MinFee.ID));
                            }
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
                           
                            return_UndrugCost = return_UndrugCost + FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2);

                            hsQuitFeeByPackage.Add("Quit" + retrunApply.ID + retrunApply.UndrugComb.ID, retrunApply);
                        }
                    }
                }
            }

            if (this.fpQuit_SheetDrug.Rows.Count != 0 && isPharmarcy == true)
            {
                this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.Rows.Count, 1);
                this.fpQuit_SheetDrug.Cells[this.fpQuit_SheetDrug.Rows.Count - 1, (int)DrugColumns.ItemName].Text = "�ϼƣ�";
                this.fpQuit_SheetDrug.Cells[this.fpQuit_SheetDrug.Rows.Count - 1, (int)DrugColumns.Cost].Text = return_DrugCost.ToString();
            }
            if (this.fpQuit_SheetUndrug.Rows.Count != 0 && isPharmarcy == false)
            {
                this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.Rows.Count, 1);
                this.fpQuit_SheetUndrug.Cells[this.fpQuit_SheetUndrug.Rows.Count - 1, (int)UndrugColumns.FeeName].Text = "�ϼƣ�";
                this.fpQuit_SheetUndrug.Cells[this.fpQuit_SheetUndrug.Rows.Count - 1, (int)UndrugColumns.Cost].Text = return_UndrugCost.ToString();
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ã����Ժ�...");
            Application.DoEvents();
            //DateTime beginTime = this.dtpBeginTime.Value;
            //DateTime endTime = this.dtpEndTime.Value;

            //���ݴ��ڿɲ�������Ŀ���,��ȡδ�˷ѵ���Ŀ��Ϣ
            switch (this.itemType)
            {
                case ItemTypes.Pharmarcy:
                    this.RetrieveReturnApply(true);
                    break;
                case ItemTypes.Undrug:
                    this.RetrieveReturnApply(false);
                    break;

                case ItemTypes.All:
                    this.RetrieveReturnApply(true);
                    this.RetrieveReturnApply(false);
                    break;
            }

            //this.fpUnQuit_SheetDrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            //this.fpUnQuit_SheetUndrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
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
            if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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
            if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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
            if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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
            if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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
            }

            return 1;
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

                feeItemList.NoBackQty = 0;
                this.fpQuit.ActiveSheet.SetValue(index, (int)DrugColumns.Qty, 0);
                this.fpQuit.ActiveSheet.SetValue(index, (int)DrugColumns.Cost, 0);
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
        /// ������Ŀ�Ŀ�������
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int UpdateNoBackQty(FeeItemList feeItemList, ref string errMsg)
        {
            int returnValue = 0;

            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
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
            #region סԺ��ʿ.���ù���.���ù���
            //ZYHS-05-01-01	�˷�	�˷����̷�������һ�������ˣ����������˵���Ŀ��������
            //��������ˣ�Ҫ���빤�ź͵�¼���룬��˺󣬲��ܹ����·��ü��˺ʹ�ӡ�˵�ƾ֤��
            //���ֿ���ֵ��ʱ���ֻ��һ����ʿ����ʱҪ�����˵�����������Ȩ�û�ʿͬʱ���о�
            //���˺�����˵�Ȩ�ޡ�	ԭʼ����	2010-8-2	�ŷ�	��Ч																																																																						

            #region ����У��
            if (isNeedPasswordConfirmation == true)
            {
            ucPasswordConfirmation:
                bool hasConfirmation = false;
                ucPasswordConfirmation upc = new ucPasswordConfirmation();
                UserLogic ul = new UserLogic();
                upc.Uid = ul.Operator.ID;
                upc.lbUserInfo.Text = "�û����" + ul.Operator.ID;
                Models.Base.Employee epy = ul.Operator as Models.Base.Employee;

                upc.ShowDialog();
                //User u = ul.Get(upc.Uid);
                if (epy != null)
                {
                    if (epy.Password == upc.Pwd)
                    {
                        hasConfirmation = true;
                    }
                }
                if (hasConfirmation == false)
                {
                    if (MessageBox.Show("��������Ƿ���������", "��ʾ", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        goto ucPasswordConfirmation;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            #endregion
            #region ƾ����ӡ

            #endregion
            #endregion
            List<FeeItemList> feeItemLists = this.GetConfirmItem();
            List<FS.HISFC.Models.Fee.ReturnApply> returnApplys = this.GetRetrunApplyItem();

            if (feeItemLists.Count <= 0 && returnApplys.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з��ÿ���!"));

                return -1;
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ArrayList alQuitFeeItemList = new ArrayList();

            if (returnApplys.Count > 0)
            {
                this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            foreach (FeeItemList feeItemList in feeItemLists)
            {
                if (feeItemList.ConfirmedQty > 0 && depts.ContainsKey(feeItemList.ExecOper.Dept.ID))
                {
                    if (string.IsNullOrEmpty(feeItemList.Order.ID) == false && dictionary.ContainsKey(feeItemList.Order.ID + feeItemList.ExecOrder.ID) == false)
                    {
                        if (this.terminalIntegrate.CancelInaptientTerminal(feeItemList.Order.ID, feeItemList.ExecOrder.ID) <= 0)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.terminalIntegrate.Err);
                            return -1;
                        }
                        dictionary.Add(feeItemList.Order.ID + feeItemList.ExecOrder.ID, null);
                    }


                    //����ȷ������
                    if (feeIntegrate.UpdateConfirmNumForUndrug(feeItemList.RecipeNO, feeItemList.SequenceNO, 0, feeItemList.BalanceState) <= 0)
                    {
                        feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸȷ������ʧ�ܣ�" + this.feeIntegrate.Err));
                        return -1;
                    }

                }
                else if (feeItemList.ConfirmedQty > 0)
                {
                    feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show(string.Format("��֪ͨ��{1}�������˷�ȷ�ϣ���{0}  {2}{3}��", string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.Name : feeItemList.UndrugComb.Name, CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID), feeItemList.ConfirmedQty, feeItemList.Item.PriceUnit));
                    return -1;
                }

                alQuitFeeItemList.Add(feeItemList);

                //if (this.feeIntegrate.QuitItem(this.patientInfo, feeItemList.Clone()) == -1)
                //{
                //    this.feeIntegrate.Rollback();
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);

                //    return -1;
                //}

            }
            stApply.Clear();
            foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in returnApplys)
            {
                if (!stApply.Contains(returnApply.RecipeNO + "|" + returnApply.SequenceNO))
                {
                    stApply.Add(returnApply.RecipeNO + "|" + returnApply.SequenceNO, returnApply);
                }
                else
                {
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).Item.Qty += returnApply.Item.Qty;
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).NoBackQty += returnApply.NoBackQty;
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).FT.TotCost += returnApply.FT.TotCost;
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).FT.PubCost += returnApply.FT.PubCost;
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).FT.OwnCost += returnApply.FT.OwnCost;
                    ((FS.HISFC.Models.Fee.ReturnApply)stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO]).FT.PayCost += returnApply.FT.PayCost;
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
                    MessageBox.Show(Language.Msg("��׼�˷�����ʧ��!���ݿ����б仯.") + this.returnApplyManager.Err);
                    return -1;
                }
                // if (returnApply.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)               
                //{ //�����������
                //���������˷������
                if (this.returnApplyManager.UpdateReturnApplyState(returnApply.ApplyMateList, FS.HISFC.Models.Base.CancelTypes.Valid) <= 0)
                {
                    this.feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����������Ϣʧ�ܣ�") + this.returnApplyManager.Err);

                    return -1;
                }
                //}
                if (stApply.Contains(returnApply.RecipeNO + "|" + returnApply.SequenceNO))
                {
                    FeeItemList feeTemp = stApply[returnApply.RecipeNO + "|" + returnApply.SequenceNO] as FeeItemList;
                    FeeItemList myFeeTemp = feeTemp.Clone();
                    myFeeTemp.IsNeedUpdateNoBackQty = true;

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(myFeeTemp.RecipeNO, myFeeTemp.SequenceNO, myFeeTemp.Item.ItemType);
                    if (feeItemListTemp == null)
                    {
                        this.feeIntegrate.Rollback();
                        MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);
                        return -1;
                    }
                    if (feeItemListTemp.ConfirmedQty > 0 && depts.ContainsKey(myFeeTemp.ExecOper.Dept.ID))
                    {
                        //if (string.IsNullOrEmpty(feeItemListTemp.Order.ID) == false && dictionary.ContainsKey(feeItemListTemp.Order.ID + feeItemListTemp.ExecOrder.ID) == false)
                        //{
                        //    if (this.terminalIntegrate.CancelInaptientTerminal(feeItemListTemp.Order.ID, feeItemListTemp.ExecOrder.ID) <= 0)
                        //    {
                        //        this.feeIntegrate.Rollback();
                        //        FS.FrameWork.Management.PublicTrans.RollBack();
                        //        MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.terminalIntegrate.Err);
                        //        return -1;
                        //    }
                        //    dictionary.Add(feeItemListTemp.Order.ID + feeItemListTemp.ExecOrder.ID, null);
                        //}

                        //�޸�ҽ���˷Ѹ���ȡ��ȷ������bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                        if (string.IsNullOrEmpty(feeItemListTemp.Order.ID) == false && dictionary.ContainsKey(feeItemListTemp.Order.ID + feeItemListTemp.ExecOrder.ID + feeItemListTemp.RecipeNO + feeItemListTemp.SequenceNO) == false)
                        {
                            if (this.terminalIntegrate.CancelInaptientTerminal2(returnApply) <= 0)
                            {
                                this.feeIntegrate.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.terminalIntegrate.Err);
                                return -1;
                            }
                            dictionary.Add(feeItemListTemp.Order.ID + feeItemListTemp.ExecOrder.ID + feeItemListTemp.RecipeNO + feeItemListTemp.SequenceNO, null);
                        }

                        //����ȷ������
                        if (feeIntegrate.UpdateConfirmNumForUndrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, 0, feeItemListTemp.BalanceState) <= 0)
                        {
                            feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸȷ������ʧ�ܣ�" + this.feeIntegrate.Err));
                            return -1;
                        }
                    }
                    else if (feeItemListTemp.ConfirmedQty > 0)
                    {
                        feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        string key = (string.IsNullOrEmpty(feeItemListTemp.UndrugComb.ID) ? feeItemListTemp.Item.ID : feeItemListTemp.UndrugComb.ID) + feeItemListTemp.ExecOrder.ID + feeItemListTemp.TransType.ToString();
                        MessageBox.Show(string.Format("��֪ͨ��{1}�������˷�ȷ�ϣ���{0}  {2}{3}��", string.IsNullOrEmpty(feeItemListTemp.UndrugComb.ID) ? feeItemListTemp.Item.Name : feeItemListTemp.UndrugComb.Name, CommonController.Instance.GetDepartmentName(feeItemListTemp.ExecOper.Dept.ID), feeItemListTemp.ConfirmedQty, feeItemListTemp.Item.PriceUnit));
                        return -1;
                    }

                    alQuitFeeItemList.Add(myFeeTemp);
                    //if (this.feeIntegrate.QuitItem(this.patientInfo, myFeeTemp) == -1)
                    //{
                    //    this.feeIntegrate.Rollback();
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);

                    //    return -1;
                    //}

                    stApply.Remove(returnApply.RecipeNO + "|" + returnApply.SequenceNO);
                }
            }

            //���з���һ���˷�
            if (this.feeIntegrate.QuitItem(this.patientInfo, ref alQuitFeeItemList) == -1)
            {
                this.feeIntegrate.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("�˷�ʧ��!") + this.feeIntegrate.Err);
                return -1;
            }

            //����ȷ����ϸ������Ϊ��Ч�շѼ�¼�Ĵ����� {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
            this.terminalIntegrate.UpdateTerminalDetailRecipe(alQuitFeeItemList);
            this.feeIntegrate.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�˷ѳɹ�!");
            if (IsPrintReceipt == true)
            {
                ucQuitFeeReceipt uqr = new ucQuitFeeReceipt();
                uqr.lbTitle.Text = "�˷���˵�";
                uqr.neuSpread1.Sheets[0].RowCount = 0;
                int i = 0;
                foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in returnApplys)
                {
                    uqr.neuSpread1.Sheets[0].RowCount++;
                    uqr.neuSpread1.Sheets[0].Cells[i, 0].Text = returnApply.Item.Name;
                    uqr.neuSpread1.Sheets[0].Cells[i, 1].Text = returnApply.Item.Specs;
                    uqr.neuSpread1.Sheets[0].Cells[i, 2].Text = returnApply.Item.Price.ToString();
                    uqr.neuSpread1.Sheets[0].Cells[i, 3].Text = returnApply.Item.Qty.ToString();
                    uqr.neuSpread1.Sheets[0].Cells[i, 4].Text = returnApply.Item.PriceUnit;
                    uqr.neuSpread1.Sheets[0].Cells[i, 5].Text = FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Price * returnApply.Item.Qty / returnApply.Item.PackQty, 2).ToString();
                    uqr.neuSpread1.Sheets[0].Cells[i, 6].Text = returnApply.CancelOper.OperTime.ToString();
                    uqr.neuSpread1.Sheets[0].Cells[i, 7].Text = returnApply.RecipeNO;
                    uqr.neuSpread1.Sheets[0].Cells[i, 8].Text = returnApply.SequenceNO.ToString();
                }
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.PrintPage(0, 0, uqr);

            }
            this.Clear();

            return 1;
        }

       Hashtable stApply = new  Hashtable();

        /// <summary>
        /// ����˷ѵ���Ŀ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ���� ʧ�� null</returns>
        private List<FeeItemList> GetConfirmItem()
        {
            List<FeeItemList> feeItemLists = new List<FeeItemList>();

            for (int j = 0; j < this.fpQuit.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpQuit.Sheets[j].RowCount; i++)
                {
                    if (this.fpQuit.Sheets[j].Rows[i].Tag != null && this.fpQuit.Sheets[j].Rows[i].Tag.GetType() == typeof(FeeItemList))
                    {
                        if (this.fpQuit.Sheets[j].Cells[i, 0].Text.ToLower() == "false")
                        {
                            continue;
                        }

                        FeeItemList feeItemList = this.fpQuit.Sheets[j].Rows[i].Tag as FeeItemList;
                        //if (feeItemList.NoBackQty > 0)
                        if (feeItemList.Item.Qty > 0)
                        {
                            //feeItemList.Item.Qty = feeItemList.NoBackQty;
                            //feeItemList.NoBackQty = 0;
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

            Hashtable hsApply = new Hashtable();


            for (int j = 0; j < this.fpQuit.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpQuit.Sheets[j].RowCount; i++)
                {
                    if (this.fpQuit.Sheets[j].Rows[i].Tag != null && this.fpQuit.Sheets[j].Rows[i].Tag.GetType() == typeof(FS.HISFC.Models.Fee.ReturnApply))
                    {
                        if (this.fpQuit.Sheets[j].Cells[i, 0].Text.ToLower() == "false") continue;
                        FS.HISFC.Models.Fee.ReturnApply temp = this.fpQuit.Sheets[j].Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        List<FS.HISFC.Models.Fee.ReturnApply> listReturnApply = new List<FS.HISFC.Models.Fee.ReturnApply>();
                        if (temp.Item.ItemType == EnumItemType.UnDrug && isQuitFeeByPackage)
                        {
                            if (hsQuitFeeByPackage.ContainsKey("Quit" + temp.ID + temp.UndrugComb.ID))
                            {
                                //�������ݿ�
                                ArrayList altemp = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, false);
                                foreach (FS.HISFC.Models.Fee.ReturnApply tempReturnApply in altemp)
                                {
                                    if (tempReturnApply.ApplyBillNO == temp.ApplyBillNO && tempReturnApply.UndrugComb.ID == temp.UndrugComb.ID)
                                    {
                                        if (!hsApply.ContainsKey(tempReturnApply.ID))
                                        {
                                            listReturnApply.Add(tempReturnApply);
                                            hsApply.Add(tempReturnApply.ID, tempReturnApply.ID);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                listReturnApply.Add(temp);
                            }
                        }
                        else
                        {
                            listReturnApply.Add(temp);
                        }

                        foreach (FS.HISFC.Models.Fee.ReturnApply feeItemList in listReturnApply)
                        {
                            //FeeItemList feeTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.IsPharmacy);
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
                            //��Ҫҽ����ˮ�ţ�ҽ��ִ�е���
                            feeItemList.Order.ID = feeTemp.Order.ID;
                            feeItemList.ExecOrder.ID = feeTemp.ExecOrder.ID;

                            feeItemList.UndrugComb.ID = feeTemp.UndrugComb.ID;
                            feeItemList.UndrugComb.Name = feeTemp.UndrugComb.Name;

                            //����ҽ������Ĳ���{BEFC715C-80A5-43fb-8FEA-A48FF419CDD4}
                            //feeItemList.RecipeOper.ID = this.inpatientManager.Operator.ID;
                            feeItemList.RecipeOper.ID = feeTemp.RecipeOper.ID;

                            //feeItemList.RecipeOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.inpatientManager.Operator).Dept.ID;
                            //{B13D20B4-3004-495a-AF2E-E0FF28A6E29D}
                            feeItemList.RecipeOper.Dept.ID = feeTemp.RecipeOper.Dept.ID;
                            feeItemList.CancelOper.ID = this.inpatientManager.Operator.ID;
                            feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                            feeItemList.ChargeOper.OperTime = feeTemp.ChargeOper.OperTime;
                            feeItemList.FeeOper.ID = this.inpatientManager.Operator.ID;
                            //���ʳ�����ˮ��
                            feeItemList.UpdateSequence = feeTemp.UpdateSequence;
                            //{47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                            feeItemList.ExtCode = feeTemp.ExtCode;//�˷�ԭ��¼������
                            feeItemList.ConfirmedQty = feeTemp.ConfirmedQty;//ԭ������ȷ������
                            feeItemLists.Add(feeItemList);
                        }
                    }
                }
            }

            return feeItemLists;
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

            //switch (this.operation) 
            //{
            //    case Operations.QuitFee:
            //    case Operations.Confirm:
            return this.SaveFee();

            //    case Operations.Apply:

            //return this.SaveApply();
            //}

            //return 1;
        }

        /// <summary>
        /// ��պ���
        /// </summary>
        public virtual void Clear()
        {
            this.ClearItemList();
            this.txtName.Text = string.Empty;
            this.txtPact.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtFilter.Text = string.Empty;
            this.txtBed.Text = string.Empty;
            this.ucQueryPatientInfo.Text = string.Empty;
            this.ucQueryPatientInfo.txtInputCode.SelectAll();
            this.ucQueryPatientInfo.txtInputCode.Focus();
            hsQuitFeeByPackage.Clear();
            this.patientInfo = null;
        }

        /// <summary>
        /// �����ʾ�б�
        /// </summary>
        public virtual void ClearItemList()
        {
            this.dtDrug.Clear();
            this.dtUndrug.Clear();
            hsQuitFeeByPackage.Clear();
            this.fpQuit_SheetDrug.RowCount = 0;
            this.fpQuit_SheetUndrug.RowCount = 0;
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
                    //this.SetColumns();
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
                catch { }
            }
        }

        private void fpQuit_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CancelQuitOperation();
        }

        #endregion

        #region ö��

        /// <summary>
        /// ҩƷ�˷�����Ϣ
        /// </summary>
        protected enum DrugColumns
        {
            //�Ƿ��˷�
            ItemState = 0,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName = 1,

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
            /// �Ƿ��˷�
            /// </summary>
            ItemState = 0,

            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName = 1,

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

        ///// <summary>
        ///// �˷ѹ���
        ///// </summary>
        //public enum Operations 
        //{
        //    /// <summary>
        //    /// ֱ���˷�
        //    /// </summary>
        //    QuitFee = 0,

        //    /// <summary>
        //    /// �˷�����
        //    /// </summary>
        //    Apply,

        //    /// <summary>
        //    /// �˷�ȷ��
        //    /// </summary>
        //    Confirm,
        //}

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

        #region ������Ŀ�˷��Ƿ����ȫ��
        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpQuit_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (!this.isCombItemAllQuit)
            {
                return;
            }

            if (this.fpQuit.ActiveSheet != this.fpQuit_SheetUndrug)
            {
                return;
            }

            if (e.Column == 0)
            {
                if (this.fpQuit_SheetUndrug.RowCount <= 0)
                {
                    return;
                }

                bool isClicked = this.fpQuit_SheetUndrug.Cells[e.Row, 0].Text.ToUpper() == "TRUE" ? true : false;

                if (this.fpQuit_SheetUndrug.Rows[e.Row].Tag == null)
                {
                    return;
                }
                FeeItemList f = this.fpQuit_SheetUndrug.Rows[e.Row].Tag as FeeItemList;

                f = this.inpatientManager.GetItemListByRecipeNO(f.RecipeNO, f.SequenceNO, FS.HISFC.Models.Base.EnumItemType.UnDrug);
                if (f == null)
                {
                    MessageBox.Show("�����ϸ��ϸ����!" + this.inpatientManager.Err);

                    return;
                }


                if (!string.IsNullOrEmpty(f.Order.ID) && !string.IsNullOrEmpty(f.UndrugComb.ID)) //˵���Ǹ�����Ŀ
                {
                    for (int i = 0; i < this.fpQuit_SheetUndrug.RowCount; i++)
                    {
                        if (this.fpQuit_SheetUndrug.Rows[i].Tag == null)
                        {
                            continue;
                        }

                        FeeItemList fCompare = this.fpQuit_SheetUndrug.Rows[i].Tag as FeeItemList;
                        fCompare = this.inpatientManager.GetItemListByRecipeNO(fCompare.RecipeNO, fCompare.SequenceNO, FS.HISFC.Models.Base.EnumItemType.UnDrug);
                        if (fCompare == null)
                        {
                            MessageBox.Show("�����ϸ��ϸ����!" + this.inpatientManager.Err);

                            return;
                        }

                        if (f.Order.ID == fCompare.Order.ID && f.UndrugComb.ID == fCompare.UndrugComb.ID)
                        {
                            this.fpQuit_SheetUndrug.Cells[i, 0].Value = isClicked;
                        }
                    }
                }

            }
        }
        #endregion

        //�����˷�ȷ�Ͻ���ȫѡ����
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked)
            { //ȫѡ
                b = true;
            }
            else
            {//ȡ��
                b = false;
            }
            if (this.fpQuit.ActiveSheet == this.fpQuit_SheetDrug)
            {
                for (int i = 0; i < this.fpQuit_SheetDrug.Rows.Count; i++)
                {
                    this.fpQuit_SheetDrug.Cells[i, 0].Value = b;
                }
            }
            else
            {
                for (int i = 0; i < this.fpQuit_SheetUndrug.Rows.Count; i++)
                {
                    this.fpQuit_SheetUndrug.Cells[i, 0].Value = b;
                }
            }
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ������
        /// <summary>
        /// ������ѡ��Ļ��߻�����Ϣ
        /// </summary>
        /// <param name="neuObject">���߻�����Ϣʵ��</param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID == "")
            {
                return -1;
            }

            QueryInpatientNo(this.patientInfo.ID);
            return 0;
        }

        private void QueryInpatientNo(string inpatientno)
        {
            //���
            this.Clear();
            this.fpQuit_SheetDrug.RowCount = 0;
            this.fpQuit_SheetUndrug.RowCount = 0;

            //�ж��Ƿ��иû���
            if (inpatientno == null || inpatientno == "")
            {
                if (this.ucQueryPatientInfo.Err == "")
                {
                    ucQueryPatientInfo.Err = "�˻��߲���Ժ!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryPatientInfo.Err, 211);

                this.ucQueryPatientInfo.Focus();
                return;
            }

            PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(inpatientno);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            this.patientInfo = this.radtIntegrate.GetPatientInfomation(inpatientno);
            if (this.patientInfo == null)
            {
                MessageBox.Show(this.radtIntegrate.Err);
            }

            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);

                this.patientInfo.ID = null;

                return;
            }
            this.ucQueryPatientInfo.TextBox.Text = this.patientInfo.PID.PatientNO;
            this.SetPatientInfomation();
        }

        #endregion
    }
}
