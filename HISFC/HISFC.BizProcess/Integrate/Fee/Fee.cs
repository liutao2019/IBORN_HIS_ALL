using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using System.Collections;
using System.Data;
using FS.HISFC.Models.Fee.Outpatient;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using System.Reflection;
using FS.HISFC.BizProcess.Interface.FeeInterface;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.Fee.Item;
using System.Text.RegularExpressions;
using FS.HISFC.Models.MedicalPackage.Fee;
using System.Linq;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ���ϵ����ת������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Fee : IntegrateBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {

        #region liuq 2007-8-23 ׷��
        #region �����ڲ��ַ�Ʊ��������

        /// <summary>
        /// ���ﰴ��ִ�п���,��С���õȷַ�Ʊ
        /// </summary>
        /// <param name="payKindCode">���ߵķ������</param>
        /// <param name="feeItemLists">���ߵ����������ϸ</param>
        /// <returns>�ɹ� �ֺõķ�����ϸ,ÿ��ArrayList����һ��Ӧ�����ɷ�Ʊ�ķ�����ϸ ʧ�� null</returns>
        public ArrayList SplitInvoice(string payKindCode, ref ArrayList feeItemLists)
        {

            // ����Ƿ���ִ�п��ҷַ�Ʊ,Ĭ�ϲ�ˢ�²���,Ĭ��ֵΪ false��������ִ�п��ҷַ�Ʊ.
            bool isSplitByExeDept = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.IS_SPLIT_INVOICE_BY_EXEDEPT, false, false);

            //�����Ʊ
            ArrayList exeGroupList = new ArrayList();

            if (isSplitByExeDept)
            {
                //����ִ�п��ҷ���
                exeGroupList = CollectFeeItemListsByExeDeptCode(feeItemLists);
            }
            else
            {
                exeGroupList = feeItemLists;
            }

            //����Ƿ�����С�ַ�Ʊ,Ĭ�ϲ�ˢ�²���,Ĭ��ֵΪ false����������С�ַ�Ʊ.
            bool isSplitByFeeCode = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.IS_SPLIT_INVOICE_BY_FEECODE, false, false);

            ArrayList finalSplitList = new ArrayList();

            if (isSplitByFeeCode)
            {
                foreach (ArrayList groupList in exeGroupList)
                {
                    ArrayList spList = this.SplitInvoiceByFeeCode(payKindCode, groupList);

                    foreach (ArrayList list in spList)
                    {
                        finalSplitList.Add(list);
                    }
                }
            }
            else
            {
                finalSplitList = exeGroupList;
            }

            //feeItemLists = new ArrayList();

            foreach (ArrayList list in finalSplitList)
            {
                foreach (FeeItemList f in list)
                {
                    feeItemLists.Add(f);
                }
            }

            return finalSplitList;
        }



        /// <summary>
        /// ��ö�Ӧ֧����ʽ�İ�����С������Ŀ�ַ�Ʊ����ϸ��Ŀ
        /// </summary>
        /// <param name="payKindCode">���ߵ�֧����ʽ���</param>
        /// <returns></returns>
        private int GetSplitCount(string payKindCode)
        {
            int count = 0;

            switch (payKindCode)
            {
                case "01":
                    count = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLIT_INVOICE_BY_FEECODE_ZF_COUNT, false, 5);
                    break;
                case "02":
                    count = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLIT_INVOICE_BY_FEECODE_YB_COUNT, false, 5);
                    break;
                case "03":
                    count = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLIT_INVOICE_BY_FEECODE_GF_COUNT, false, 5);
                    break;
            }

            return count;
        }

        /// <summary>
        /// ������С���÷���ϸ
        /// </summary>
        /// <param name="payKindCode">���ߵ�֧����ʽ���</param>
        /// <param name="feeItemList">������ϸ</param>
        /// <returns></returns>
        private ArrayList SplitInvoiceByFeeCode(string payKindCode, ArrayList feeItemList)
        {
            ArrayList sortList = this.CollectFeeItemListsByFeeCode(feeItemList);

            ArrayList finalList = new ArrayList();

            foreach (ArrayList list in sortList)
            {
                ArrayList sortFeeCodeList = this.SplitByFeeCodeCount(payKindCode, list);

                foreach (ArrayList fList in sortFeeCodeList)
                {
                    finalList.Add(fList);
                }
            }

            return finalList;
        }

        /// <summary>
        /// ������С����������������ϸ
        /// </summary>
        /// <param name="payKindCode">���ߵ�֧����ʽ���</param>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns></returns>
        private ArrayList SplitByFeeCodeCount(string payKindCode, ArrayList feeItemLists)
        {
            int count = this.GetSplitCount(payKindCode);

            ArrayList splitArrayList = new ArrayList();
            ArrayList groupList = new ArrayList();

            while (feeItemLists.Count > count)
            {
                groupList = new ArrayList();

                for (int i = 0; i < count; i++)
                {
                    FeeItemList f = feeItemLists[0] as FeeItemList;

                    groupList.Add(f);
                }
                foreach (FeeItemList f in groupList)
                {
                    feeItemLists.Remove(f);
                }
                splitArrayList.Add(groupList);
            }
            if (feeItemLists.Count > 0)
            {
                splitArrayList.Add(feeItemLists);
            }

            return splitArrayList;
        }

        /// <summary>
        /// ������С��������
        /// </summary>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns>�ɹ� ����õĴ�����ϸ ʧ�� null</returns>
        private ArrayList CollectFeeItemListsByFeeCode(ArrayList feeItemLists)
        {
            feeItemLists.Sort(new SortFeeItemListByFeeCode());

            ArrayList sorList = new ArrayList();

            while (feeItemLists.Count > 0)
            {
                ArrayList sameFeeItemLists = new ArrayList();
                FeeItemList compareItem = feeItemLists[0] as FeeItemList;
                foreach (FeeItemList f in feeItemLists)
                {
                    if (f.Item.MinFee.ID == compareItem.Item.MinFee.ID)
                    {
                        sameFeeItemLists.Add(f);
                    }
                    else
                    {
                        break;
                    }
                }
                sorList.Add(sameFeeItemLists);
                foreach (FeeItemList f in sameFeeItemLists)
                {
                    feeItemLists.Remove(f);
                }
            }

            return sorList;
        }

        /// <summary>
        /// ����ִ�п�������
        /// </summary>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns>�ɹ� ����õĴ�����ϸ ʧ�� null</returns>
        private ArrayList CollectFeeItemListsByExeDeptCode(ArrayList feeItemLists)
        {
            feeItemLists.Sort(new SortFeeItemListByExeDeptCode());

            ArrayList sorList = new ArrayList();

            while (feeItemLists.Count > 0)
            {
                ArrayList sameFeeItemLists = new ArrayList();
                FeeItemList compareItem = feeItemLists[0] as FeeItemList;
                foreach (FeeItemList f in feeItemLists)
                {
                    if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID)
                    {
                        sameFeeItemLists.Add(f);
                    }
                    else
                    {
                        break;
                    }
                }
                sorList.Add(sameFeeItemLists);
                foreach (FeeItemList f in sameFeeItemLists)
                {
                    feeItemLists.Remove(f);
                }
            }

            return sorList;
        }


        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns>�ɹ� ����õĴ�����ϸ ʧ�� null</returns>
        private ArrayList CollectFeeItemListsByChargeDate(ArrayList feeItemLists)
        {
            feeItemLists.Sort(new SortFeeItemListByChargeDate());

            ArrayList sorList = new ArrayList();

            while (feeItemLists.Count > 0)
            {
                ArrayList sameFeeItemLists = new ArrayList();
                FS.HISFC.Models.Fee.Inpatient.FeeItemList compareItem = feeItemLists[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
                {
                    if (f.ChargeOper.OperTime.Date == compareItem.ChargeOper.OperTime.Date)
                    {
                        sameFeeItemLists.Add(f);
                    }
                }
                sorList.Add(sameFeeItemLists);
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in sameFeeItemLists)
                {
                    feeItemLists.Remove(f);
                }
            }

            return sorList;
        }

        /// <summary>
        /// ������
        /// </summary>
        private class SortFeeItemListByExeDeptCode : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                if (x is FeeItemList && y is FeeItemList)
                {
                    return ((FeeItemList)x).ExecOper.Dept.ID.CompareTo(
                        ((FeeItemList)y).ExecOper.Dept.ID);
                }
                else
                {
                    return -1;
                }
            }

            #endregion
        }

        /// <summary>
        /// ������
        /// </summary>
        private class SortFeeItemListByFeeCode : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                if (x is FeeItemList && y is FeeItemList)
                {
                    return ((FeeItemList)x).Item.MinFee.ID.CompareTo(
                        ((FeeItemList)y).Item.MinFee.ID);
                }
                else
                {
                    return -1;
                }
            }

            #endregion
        }

        /// <summary>
        /// ������
        /// </summary>
        private class SortFeeItemListByChargeDate : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                if (x is FS.HISFC.Models.Fee.Inpatient.FeeItemList && y is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                {
                    return ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)x).ChargeOper.OperTime.ToString("yyyyMMdd").CompareTo(
                        ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)y).ChargeOper.OperTime.ToString("yyyyMMdd"));
                }
                else
                {
                    return -1;
                }
            }

            #endregion
        }

        #endregion

        #endregion

        #region ���ݽӿ�ʵ�ַַ�Ʊ
        /// <summary>
        /// ���ݽӿ�ʵ�ַַ�Ʊ
        /// </summary>
        /// <param name="register"></param>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SplitInvoice(FS.HISFC.Models.Registration.Register register, ref ArrayList feeItemLists)
        {
            //Ϊ��ʹ�ٶȸ��죬Ĭ�Ϸַ�Ʊ�ӿ����治��������Ĵ��룬ֱ�ӷ���
            ArrayList finalSplitList = new ArrayList();

            finalSplitList.Add(feeItemLists);

            return finalSplitList;
        }

        #endregion

        #region ����

        /// <summary>
        /// ������ҵ��� {2CEA3B1D-2E59-44ac-9226-7724413173C5} ��ҵ��������ȫ����Ϊ�Ǿ�̬�ı���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ���ѵ��˻���Ϣ
        /// </summary>
        protected FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();

        /// <summary>
        /// item
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// /// ��Ʊҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup employeeFinanceGroupManager = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        protected FS.FrameWork.Management.ControlParam controlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ����ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderOutpatientManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// �ն�ԤԼҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Terminal.Terminal terminalManager = new FS.HISFC.BizLogic.Terminal.Terminal();

        protected FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctBizLogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmarcyManager = null;

        protected FS.HISFC.BizProcess.Integrate.Pharmacy PharmarcyManager
        {
            get
            {
                if (pharmarcyManager == null)
                {
                    pharmarcyManager = new Pharmacy();
                }
                return pharmarcyManager;
            }
        }

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Manager();

        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// ����ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Interface interfaceManager = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// ��ǰҽ�����ѽӿ�
        /// </summary>
        protected MedcareInterfaceProxy medcareInterfaceProxy = new MedcareInterfaceProxy();

        /// <summary>
        /// ��ͬ��λ��
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ����ʵ��(liu.xq)
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = null;

        protected FS.HISFC.BizProcess.Integrate.RADT RadtIntegrate
        {
            get
            {
                if (radtIntegrate == null)
                {
                    radtIntegrate = new RADT();
                }
                return radtIntegrate;
            }
        }

        /// <summary>
        /// �Ƿ����ҽ�����ѽӿ�
        /// </summary>
        private bool isIgnoreMedcareInterface = false;

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examiIntegrate = null;

        protected FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager ExamiIntegrate
        {
            get
            {
                if (examiIntegrate == null)
                {
                    examiIntegrate = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();
                }
                return examiIntegrate;
            }
        }


        /// <summary>
        /// Ա��
        /// </summary>
        FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

        //������Ŀ��ϸҵ���
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// ϵͳ���кŹ���ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Sequence seqManager = new FS.HISFC.BizLogic.Manager.Sequence();

        /// <summary>
        /// ����ҵ���{BF01254E-3C73-43d4-A644-4B258438294E}
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceJumpRecord invoiceJumpRecordMgr = new FS.HISFC.BizLogic.Fee.InvoiceJumpRecord();

        /// <summary>
        /// �����շ��Ƿ���Ҫ���·�Ʊ��
        /// </summary>
        protected bool isNeedUpdateInvoiceNO = true;

        /// <summary>
        /// �Ƿ������Ժ״̬����סԺ����
        /// </summary>
        protected bool isIgnoreInstate = false;
        /// <summary>
        /// Ƿ����ʾ����
        /// </summary>
        private MessType messType = MessType.Y;
        /// <summary>
        /// //�Ƿ����÷���ϵͳ 1 ���� ���� ������
        /// </summary>
        string pValue = "";

        #region �����ʻ�
        /// <summary>
        /// �����ʻ���ҵ���
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// �˻���������
        /// </summary>
        protected static FS.HISFC.BizProcess.Interface.Account.IPassWord ipassWord = null;
        #endregion
        /// <summary>
        /// �����շ�
        /// </summary>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        protected static FS.HISFC.BizProcess.Integrate.Material.Material materialManager = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// ��Ȩ�շ�
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.EmpowerFee empowerFeeManager = new FS.HISFC.BizLogic.Fee.EmpowerFee();

        /// <summary>
        /// ��λ�ѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Fee.BedFeeItem feeBedFeeItem = new FS.HISFC.BizLogic.Fee.BedFeeItem();

        /// <summary>
        /// �˷�ҵ��� {5C327B2F-2B74-45bb-8435-4E5118215BD2}
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnMgr = new FS.HISFC.BizLogic.Fee.ReturnApply();

        Terminal.Confirm confirmIntegrate = null;

        public Terminal.Confirm ConfirmIntegrate
        {
            get
            {
                if (confirmIntegrate == null)
                {
                    confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
                }
                return confirmIntegrate;
            }
        }

        /// <summary>
        /// ��ȡ�۸�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice IGetItemPrice = null;

        /// <summary>
        /// ��ȡ��������ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        //{282BD4C3-4086-4d4c-BE3D-68FC3205E4B7}
        /// <summary>
        /// �Һ���ϸ����
        /// </summary>
        FS.HISFC.BizLogic.Registration.RegDetail regDetailMgr = new FS.HISFC.BizLogic.Registration.RegDetail();
        /// <summary>
        /// �Һ�֧����ʽ����
        /// </summary>
        FS.HISFC.BizLogic.Registration.RegPayMode regPayModeMgr = new FS.HISFC.BizLogic.Registration.RegPayMode();

        /// <summary>
        /// �����˻�
        /// </summary>
        Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

        /// <summary>
        /// �ײ�ҵ����ϸ����
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail packageDetailMgr = null;

        public FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail PackageDetailMgr
        {
            get
            {
                if (packageDetailMgr == null)
                {
                    packageDetailMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
                }
                return packageDetailMgr;
            }
        }

        /// <summary>
        /// �ײ�ҵ����ϸ����
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = null;

        public FS.HISFC.BizLogic.MedicalPackage.Fee.Package PackageMgr
        {
            get
            {
                if (packageMgr == null)
                {
                    packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
                }
                return packageMgr;
            }
        }

        /// <summary>
        /// ���Ѽ�¼������
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost packageCostMgr = null;

        public FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost PackageCostMgr
        {
            get
            {
                if (packageCostMgr == null)
                {
                    packageCostMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost();
                }
                return packageCostMgr;
            }
        }

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        /// <summary>
        /// ֧����ʽ�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helperPayModes = null;

        #endregion

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            itemManager.SetTrans(trans);
            inpatientManager.SetTrans(trans);
            controlManager.SetTrans(trans);
            invoiceServiceManager.SetTrans(trans);
            employeeFinanceGroupManager.SetTrans(trans);
            //medcareInterfaceProxy.SetTrans(trans);
            outpatientManager.SetTrans(trans);
            orderManager.SetTrans(trans);
            orderOutpatientManager.SetTrans(trans);
            terminalManager.SetTrans(trans);
            PharmarcyManager.SetTrans(trans);
            registerManager.SetTrans(trans);
            interfaceManager.SetTrans(trans);
            managerIntegrate.SetTrans(trans);
            controlParamIntegrate.SetTrans(trans);
            ExamiIntegrate.SetTrans(trans);
            userManager.SetTrans(trans);
            undrugPackAgeMgr.SetTrans(trans);
            empowerFeeManager.SetTrans(trans);
            seqManager.SetTrans(trans);
            ConfirmIntegrate.SetTrans(trans);
            this.PackageMgr.SetTrans(trans);

            #region �����ʻ�

            accountManager.SetTrans(trans);

            accountPay.SetTrans(trans);

            #endregion

            #region �ײ�

            PackageDetailMgr.SetTrans(trans);
            PackageCostMgr.SetTrans(trans);

            #endregion
        }

        #region ����

        /// <summary>
        /// �Ƿ������Ժ״̬����סԺ����
        /// </summary>
        public bool IsIgnoreInstate
        {
            get
            {
                return this.isIgnoreInstate;
            }
            set
            {
                this.isIgnoreInstate = value;
            }
        }

        /// <summary>
        /// �����շ��Ƿ���Ҫ���·�Ʊ��
        /// </summary>
        public bool IsNeedUpdateInvoiceNO
        {
            get
            {
                return this.isNeedUpdateInvoiceNO;
            }
            set
            {
                this.isNeedUpdateInvoiceNO = value;
            }
        }

        /// <summary>
        /// ��ǰҽ�����ѽӿ�
        /// </summary>
        public MedcareInterfaceProxy MedcareInterfaceProxy
        {
            get
            {
                return medcareInterfaceProxy;
            }
        }

        /// <summary>
        /// �Ƿ����ҽ�����ѽӿ�
        /// </summary>
        public bool IsIgnoreMedcareInterface
        {
            set
            {
                this.isIgnoreMedcareInterface = value;
            }
        }
        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ�Ƿ���Ƿ���ʾ
        /// </summary>
        public MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        public string IsFeeWhenPriceZero
        {
            get
            {
                if (this.isFeeWhenPriceZero == "-1")
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    this.isFeeWhenPriceZero = con.GetControlParam<string>("FEE001", false, "0");
                }
                return this.isFeeWhenPriceZero;
            }
        }

        /// <summary>
        /// ֧����ʽ�б�
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper PayModesHelper
        {
            get
            {
                if (this.helperPayModes == null)
                {
                    this.helperPayModes = new FS.FrameWork.Public.ObjectHelper();
                    //��ʼ��֧����ʽ��Ϣ
                    ArrayList alPayModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
                    if (alPayModes != null && alPayModes.Count > 0)
                    {
                        this.helperPayModes.ArrayObject = alPayModes;
                    }
                }

                return this.helperPayModes;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// �ж��շ���Ҫ�Ĳ����Ƿ�Ϸ�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">���߷�����ϸʵ��</param>
        /// <returns>�ɹ�: true ʧ�� false</returns>
        private bool IsValidFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (patient == null)
            {
                this.Err = Language.Msg("���߻�����Ϣû�и�ֵ");

                return false;
            }

            if (feeItemList == null)
            {
                this.Err = Language.Msg("���߷�����Ϣû�и�ֵ");

                return false;
            }

            //if (feeItemList.FT.TotCost == 0)
            //{
            //    this.Err = Language.Msg("�����ܶ��Ϊ�㣺\n���ۻ���������Ϊ0");

            //    return false;
            //}

            if (patient.PVisit.PatientLocation.NurseCell.ID == null || patient.PVisit.PatientLocation.NurseCell.ID.Trim() == string.Empty)
            {
                this.Err = Language.Msg("���ֲ㻼�߻�ʿվ����û�и�ֵ!");

                return false;
            }

            if (feeItemList.ExecOper.Dept.ID == null || feeItemList.ExecOper.Dept.ID == string.Empty)
            {
                this.Err = Language.Msg("���ֲ�ִ�п���û�и�ֵ!");

                return false;
            }

            if (feeItemList.FTRate.ItemRate < 0)
            {
                this.Err = Language.Msg("�շѱ�����ֵ����!");

                return false;
            }
            feeItemList.Item.Price = Math.Round(feeItemList.Item.Price, 4);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.Item.Price, 10, 4))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("�ļ۸񾫶Ȳ�����,��׼�ľ���Ӧ��ΪС����ǰ6λ,С�����4λ");

                return false;
            }
            feeItemList.Item.Qty = Math.Round(feeItemList.Item.Qty, 4);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.Item.Qty, 9, 4))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("���������Ȳ�����,��׼�ľ���Ӧ��ΪС����ǰ5λ,С�����4λ");

                return false;
            }
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.FT.TotCost, 10, 2))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("�Ľ��Ȳ�����,��׼�ľ���Ӧ��ΪС����ǰ8λ,С�����2λ");

                return false;
            }

            return true;
        }

        /// <summary>
        /// ������
        /// </summary>
        private class CompareFeeItemList : IComparer
        {
            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                if (x is FS.HISFC.Models.Fee.Inpatient.FeeItemList && y is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                {
                    return ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)x).Item.MinFee.ID.CompareTo(
                        ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)y).Item.MinFee.ID);
                }
                else
                {
                    return -1;
                }
            }

            #endregion
        }

        /// <summary>
        /// ���ô�����
        /// </summary>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <param name="trans">���ݿ�����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int SetRecipeNO(ref ArrayList feeItemLists, System.Data.IDbTransaction trans)
        {
            this.SetDB(inpatientManager);
            inpatientManager.SetTrans(trans);

            ArrayList existRecipeNOLists = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                if (feeItemList.RecipeNO != null && feeItemList.RecipeNO != string.Empty)
                {
                    existRecipeNOLists.Add(feeItemList);
                }
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in existRecipeNOLists)
            {
                feeItemLists.Remove(feeItemList);
            }

            ArrayList sortByMinFeeLists = this.CollectFeeItemListsByChargeDate(feeItemLists);

            if (feeItemLists.Count > 0)
            {
                sortByMinFeeLists.Add(feeItemLists);
            }

            foreach (ArrayList list in sortByMinFeeLists)
            {
                string recipeNO = string.Empty;
                int recipeSequenceNO = 1;
                FS.HISFC.Models.Fee.Inpatient.FeeItemList temp = list[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                //if (temp.Item.IsPharmacy)
                if (temp.Item.ItemType == EnumItemType.Drug)
                {
                    recipeNO = inpatientManager.GetDrugRecipeNO();
                }
                else
                {
                    recipeNO = inpatientManager.GetUndrugRecipeNO();
                }

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in list)
                {
                    feeItemList.RecipeNO = recipeNO;
                    feeItemList.SequenceNO = recipeSequenceNO;

                    recipeSequenceNO++;
                }
            }

            feeItemLists = new ArrayList();

            feeItemLists.AddRange(existRecipeNOLists);

            foreach (ArrayList list in sortByMinFeeLists)
            {
                feeItemLists.AddRange(list);
            }

            return 1;
        }

        /// <summary>
        /// ������С��������
        /// </summary>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns>�ɹ� ����õĴ�����ϸ ʧ�� null</returns>
        private ArrayList CollectFeeItemLists(ArrayList feeItemLists)
        {
            feeItemLists.Sort(new CompareFeeItemList());

            ArrayList sorList = new ArrayList();

            while (feeItemLists.Count > 0)
            {
                ArrayList sameFeeItemLists = new ArrayList();
                FS.HISFC.Models.Fee.Inpatient.FeeItemList compareItem = feeItemLists[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
                {
                    if (f.RecipeNO == compareItem.RecipeNO)
                    {
                        if (f.Item.MinFee.ID == compareItem.Item.MinFee.ID)
                        {
                            if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID)
                            {
                                sameFeeItemLists.Add(f);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                sorList.Add(sameFeeItemLists);
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in sameFeeItemLists)
                {
                    feeItemLists.Remove(f);
                }
            }

            return sorList;
        }

        /// <summary>
        /// ��ҽ����Ϣת��Ϊ������Ϣ
        /// 
        /// {F5477FAB-9832-4234-AC7F-ED49654948B4} ���Ӳ��� ����patient��Ϣ
        /// </summary>
        /// <param name="orderList">ҽ����Ϣ</param>
        /// <returns>�ɹ� ������Ϣ ʧ�� null</returns>
        private ArrayList ConvertOrderToFeeItemList(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList orderList)
        {
            ArrayList feeItemLists = new ArrayList();

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                feeItemList.Item = order.Item.Clone();

                if (order.HerbalQty == 0)
                {
                    order.HerbalQty = 1;
                }

                //{F5477FAB-9832-4234-AC7F-ED49654948B4}
                decimal price = feeItemList.Item.Price;
                decimal orgPrice = 0;
                if (this.GetPriceForInpatient(patient, feeItemList.Item, ref price, ref orgPrice) == -1)
                {
                    MessageBox.Show(Language.Msg("ȡ��Ŀ:") + feeItemList.Item.Name + Language.Msg("�ļ۸����!") + this.Err);

                    return null;
                }
                feeItemList.Item.Price = price;

                // {54B0C254-3897-4241-B3BD-17B19E204C8C}
                // ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�
                feeItemList.Item.DefPrice = orgPrice;

                //¼������Ѿ���QTY ��ֵ
                feeItemList.Item.Qty = order.Qty;// *order.HerbalQty;
                //���Ӹ����ĸ�ֵ {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                feeItemList.Days = order.HerbalQty;

                feeItemList.Item.PriceUnit = order.Unit;//��λ���¸�
                feeItemList.RecipeOper.Dept = order.ReciptDept.Clone();
                feeItemList.RecipeOper.ID = order.ReciptDoctor.ID;
                feeItemList.RecipeOper.Name = order.ReciptDoctor.Name;
                feeItemList.ExecOper = order.ExecOper.Clone();
                feeItemList.StockOper.Dept = order.StockDept.Clone();
                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }

                //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);
                //// ԭʼ�ܷ��ã�����Ӧ�շ��ã������Ǻ�ͬ��λ���أ�
                //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
                //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //�¼Ƽ۹����ȼ������Ƭ�۸����4��5�룬�����ܼ�
                //סԺ���������շ��������в�֣�������˴������ֳ����ķ�����Ŀ�����г���
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);


                feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                feeItemList.IsBaby = order.IsBaby;
                feeItemList.IsEmergency = order.IsEmergency;
                feeItemList.Order = order.Clone();
                feeItemList.ExecOrder.ID = order.User03;
                feeItemList.NoBackQty = feeItemList.Item.Qty;
                feeItemList.FTRate.OwnRate = 1;
                feeItemList.BalanceState = "0";
                feeItemList.ChargeOper = order.Oper.Clone();
                feeItemList.FeeOper = order.Oper.Clone();
                feeItemList.TransType = TransTypes.Positive;

                #region {10C9E65E-7122-4a89-A0BE-0DF62B65C647} д�븴����Ŀ���롢����
                feeItemList.UndrugComb.ID = order.Package.ID;
                feeItemList.UndrugComb.Name = order.Package.Name;
                feeItemList.UndrugComb.Qty = order.Package.Qty;

                #endregion

                feeItemLists.Add(feeItemList);
            }

            return feeItemLists;
        }

        /// <summary>
        /// �������ʿۿ����
        /// </summary>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public void GetMatLoadDataDept(FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            //return ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            f.StockOper.Dept.ID = f.ExecOper.Dept.ID;
        }

        /// <summary>
        /// Ӥ���ķ����Ƿ������ȡ����������
        /// </summary>
        private string motherPayAllFee = "";

        /// <summary>
        /// סԺ�����շ�
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <param name="feeItemLists">���û�ҽ����Ϣʵ��</param>
        /// <param name="payType">�շ�����(���ۻ����շ�)</param>
        /// <param name="transType">������ ������</param>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        private int FeeManager(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists, ChargeTypes payType, TransTypes transType)
        {
            #region liu.xq
            this.RadtIntegrate.SetTrans(this.trans);
            patient = this.RadtIntegrate.GetPatientInfomation(patient.ID);

            if (patient.IsStopAcount)
            {
                this.Err = "�û����Ѿ�����!���ܼ���¼����û��˷�,����סԺ����ϵ!";

                return -1;
            }

            //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} Ӥ���ķ����Ƿ������ȡ����������
            if (IsMotherPayAllFee(patient))
            {
                //if (string.IsNullOrEmpty(motherPayAllFee))
                //{
                //    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                //}

                //if (motherPayAllFee == "1")//Ӥ���ķ���������������� 
                //{
                //    if (patient.IsBaby) //סԺ��ˮ�ź���B,������Ӥ��
                //    {
                string motherInpatientNO = this.RadtIntegrate.QueryBabyMotherInpatientNO(patient.ID);
                if (string.IsNullOrEmpty(motherInpatientNO) || motherInpatientNO == "-1")
                {
                    this.Err = "û���ҵ�Ӥ����ĸ��סԺ��ˮ��" + this.RadtIntegrate.Err;

                    return -1;
                }

                patient = this.RadtIntegrate.GetPatientInfomation(motherInpatientNO);//������Ļ�����Ϣ�滻Ӥ���Ļ�����Ϣ

                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = null;

                object obj = feeItemLists[0];
                if (obj is FS.HISFC.Models.Order.Inpatient.Order)
                {
                    feeItemLists = this.ConvertOrderToFeeItemList(patient, feeItemLists);
                    for (int i = 0; i < feeItemLists.Count; i++)
                    {
                        feeItemList = feeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        feeItemList.IsBaby = true;
                    }
                }
                else
                {
                    for (int i = 0; i < feeItemLists.Count; i++)
                    {
                        feeItemList = feeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        feeItemList.IsBaby = true;
                    }
                }
            }
            else
            {
                object obj = feeItemLists[0];
                if (obj is FS.HISFC.Models.Order.Inpatient.Order)
                {
                    feeItemLists = this.ConvertOrderToFeeItemList(patient, feeItemLists);
                }
            }
            //}
            //else
            //{
            //    object obj = feeItemLists[0];
            //    if (obj is FS.HISFC.Models.Order.Inpatient.Order)
            //    {
            //        feeItemLists = this.ConvertOrderToFeeItemList(patient, feeItemLists);
            //    }
            //}
            ////{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F}���

            #endregion


            this.SetDB(inpatientManager);

            if (feeItemLists == null || feeItemLists.Count == 0)
            {
                return -1;
            }

            //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E}����ҽ���鴦��
            if (HsMedicalTeam == null)
            {
                HsMedicalTeam = new Hashtable();
                List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> medicalTeamForDoctList = new List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct>();

                medicalTeamForDoctList = this.medicalTeamForDoctBizLogic.QueryQueryMedicalTeamForDoctInfo();

                //��ӹ�ϣ��
                foreach (FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct item in medicalTeamForDoctList)
                {
                    string strAdd = item.MedcicalTeam.Dept.ID + "|" + item.Doct.ID;
                    if (HsMedicalTeam.Contains(strAdd))
                    {
                        continue;
                    }

                    HsMedicalTeam.Add(strAdd, item);
                }
            }

            //ȡ���ϵĵ�һ��Ԫ���ж��Ƿ�����ϸ(FeeItemList����Order)
            long returnValue = 0;
            this.MedcareInterfaceProxy.SetPactTrans(this.trans);
            //������ýӿ�û�г�ʼ��,��ô���ݻ��ߵĺ�ͬ��λ��ʼ�����ýӿ�
            if (medcareInterfaceProxy != null)
            {
                returnValue = MedcareInterfaceProxy.SetPactCode(patient.Pact.ID);

                if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
                {
                    this.Err = MedcareInterfaceProxy.ErrMsg;

                    return -1;
                }
            }

            //�ж���Ч��
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                //��Ч���ж�
                if (!this.IsValidFee(patient, feeItemList))
                {
                    return -1;
                }
                // //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E}����ҽ���鴦��
                if (HsMedicalTeam != null && HsMedicalTeam.Count > 0)
                {
                    if (string.IsNullOrEmpty(feeItemList.MedicalTeam.ID))
                    {
                        //string patientDept = ((FS.HISFC.Models.RADT.PatientInfo)feeItemList.Patient).PVisit.PatientLocation.Dept.ID;
                        string patientDept = patient.PVisit.PatientLocation.Dept.ID;

                        if (HsMedicalTeam.Contains(patientDept + "|" + feeItemList.RecipeOper.ID))
                        {
                            FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct obj = HsMedicalTeam[patientDept + "|" + feeItemList.RecipeOper.ID] as FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct;
                            feeItemList.MedicalTeam = obj.MedcicalTeam;
                        }
                        else if (HsMedicalTeam.Contains(patientDept + "|" + patient.PVisit.AdmittingDoctor.ID))
                        {
                            FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct obj = HsMedicalTeam[patientDept + "|" + patient.PVisit.AdmittingDoctor.ID] as FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct;
                            feeItemList.MedicalTeam = obj.MedcicalTeam;
                        }
                    }
                }

                // û��ά��ҽ����
                // ���滼��סԺҽ��ID ��ׯ����

                if (string.IsNullOrEmpty(feeItemList.MedicalTeam.ID))
                {
                    feeItemList.MedicalTeam.ID = patient.PVisit.AdmittingDoctor.ID;
                }
            }

            //ִ�з��ýӿ�,�Ա����Ƚ��м�������¸�ֵ
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                if (feeItemList.FT.DefTotCost == 0)
                {
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        feeItemList.FT.DefTotCost = feeItemList.FT.TotCost;
                    }
                    else
                    {
                        feeItemList.FT.DefTotCost = feeItemList.Item.Qty * feeItemList.Item.DefPrice;
                    }
                }
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);

                if (feeItemList.User01 != "1" && payType == ChargeTypes.Fee) //���Ի��߷��ñ����޸�,���µ��õ�ʱ����Ҫ�ڼ�����ñ���
                {
                    returnValue = MedcareInterfaceProxy.RecomputeFeeItemListInpatient(patient, feeItemList);

                    if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
                    {
                        this.Err = MedcareInterfaceProxy.ErrMsg;

                        return -1;
                    }
                }


                //Ϊ��ֹ���������ͳһת��Ϊ2λ��

                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                feeItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.OwnCost, 2);
                feeItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.PayCost, 2);
                feeItemList.FT.PubCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.PubCost, 2);
                feeItemList.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.RebateCost, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);


                //��ֹ�յ���λ��ֺ��¼Ϊ0
                //if (feeItemList.FT.TotCost == 0)
                //{
                //    return 1;
                //}
            }

            //���·��䴦����
            this.SetRecipeNO(ref feeItemLists, this.trans);

            #region �����շѴ���
            //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
            //if (transType == TransTypes.Positive)
            //{
            //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
            //    {
            //        //���ʵĿۿ�����ǵ�¼���ң�����ص��б�����ͬ�Ŀ��ң�
            //        if (f.Item.ItemType != EnumItemType.Drug)
            //        {
            //            GetMatLoadDataDept(f);
            //        }
            //    }
            //    //�����շѴ���
            //    if (materialManager.MaterialFeeOutput(feeItemLists) < 0)
            //    {
            //        this.Err = materialManager.Err;
            //        return -1;
            //    }
            //}
            //else
            //{

            //    //�˿�
            //    if (materialManager.MaterialFeeOutputBack(feeItemLists) < 0)
            //    {
            //        this.Err = materialManager.Err;
            //        return -1;
            //    }
            //}
            #endregion

            ArrayList alFeeItemLists = new ArrayList();

            ArrayList alSendQuitFeeItemLists = new ArrayList();

            DateTime dtNow = inpatientManager.GetDateTimeFromSysDateTime();
            //����ϸѭ������
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {

                //������շѲ���,��ô���������׺ͷ����׵����⸳ֵ
                if (payType == ChargeTypes.Fee)
                {

                    if (feeItemList.FTRate.ItemRate == 0)
                    {
                        feeItemList.FTRate.ItemRate = 1;
                    }

                    if (feeItemList.Item.PackQty == 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }

                    //����Ƿ�����,��Ҫ�жϸ��¿��������Ͱѽ��,�������и�������
                    if (transType == TransTypes.Negative)
                    {

                        #region ����ԭ��¼
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
                        if (feeItemListTemp == null)
                        {
                            this.Err = "�����Ŀ������Ϣ����!" + this.inpatientManager.Err;
                            return -1;
                        }

                        #endregion

                        #region ���¿�������
                        //���¿������� Ȼ��ȡ���µĴ�����,ҩƷ�ͷ�ҩƷ�ֱ����(��ͬ)
                        if (feeItemList.Item.ItemType == EnumItemType.Drug)
                        {
                            if (feeItemListTemp.NoBackQty > 0)
                            {
                                if (inpatientManager.UpdateNoBackQtyForDrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, feeItemListTemp.NoBackQty, feeItemListTemp.BalanceState) < 1)
                                {
                                    this.Err = Language.Msg("����ԭ�м�¼������������!") + feeItemList.Item.Name + Language.Msg("�����Ѿ����˷ѻ����!") + inpatientManager.Err;

                                    return -1;
                                }
                            }
                            //����µĴ�����

                            feeItemList.RecipeNO = inpatientManager.GetDrugRecipeNO();
                        }
                        else
                        {
                            if (feeItemListTemp.NoBackQty > 0)
                            {
                                if (inpatientManager.UpdateNoBackQtyForUndrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, feeItemListTemp.NoBackQty, feeItemListTemp.BalanceState) < 1)
                                {
                                    this.Err = Language.Msg("����ԭ�м�¼������������!") + feeItemList.Item.Name + Language.Msg("�����Ѿ����˷ѻ����!") + inpatientManager.Err;

                                    return -1;
                                }
                            }
                            //����µĴ�����
                            feeItemList.RecipeNO = inpatientManager.GetUndrugRecipeNO();
                        }
                        #endregion

                        #region ȫ��/���˴���

                        feeItemList.CancelRecipeNO = feeItemListTemp.RecipeNO;
                        feeItemList.CancelSequenceNO = feeItemList.SequenceNO;

                        //ԭʼ�۸�ͽ��
                        feeItemList.Item.DefPrice = feeItemListTemp.Item.DefPrice;
                        //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);

                        //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                        //�¼Ƽ۹����ȼ������Ƭ�۸����4��5�룬�����ܼ�
                        //סԺ���������շ��������в�֣�������˴������ֳ����ķ�����Ŀ�����г���
                        feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                        feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);

                        feeItemList.SplitFeeFlag = feeItemListTemp.SplitFeeFlag;

                        //ȫ��
                        if (feeItemList.Item.Qty == feeItemListTemp.Item.Qty)//˵����ȫ��
                        {
                            //�޸�ҽ���˷Ѹ���ȡ��ȷ������bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                            feeItemList.ConfirmedQty = feeItemList.ConfirmedQty - feeItemList.Item.Qty;

                            feeItemList.Item.Qty = -feeItemListTemp.Item.Qty;
                            feeItemList.UndrugComb.Qty = -feeItemListTemp.UndrugComb.Qty;
                            feeItemList.NoBackQty = 0; //
                            feeItemList.FT.TotCost = -feeItemListTemp.FT.TotCost;
                            feeItemList.FT.OwnCost = -feeItemListTemp.FT.OwnCost;
                            feeItemList.FT.PayCost = -feeItemListTemp.FT.PayCost;
                            feeItemList.FT.PubCost = -feeItemListTemp.FT.PubCost;
                            feeItemList.FT.RebateCost = -feeItemListTemp.FT.RebateCost;
                            feeItemList.FT.DefTotCost = -feeItemListTemp.FT.DefTotCost;

                            feeItemList.TransType = TransTypes.Negative;

                            feeItemList.FeeOper.ID = inpatientManager.Operator.ID;

                            feeItemList.ChargeOper.ID = inpatientManager.Operator.ID;
                            //���ֻ���ʱ����շ�ʱ��ͬ��
                            if (feeItemList.ChargeOper.OperTime <= DateTime.MinValue)
                            {
                                feeItemList.ChargeOper.OperTime = feeItemListTemp.FeeOper.OperTime;
                            }

                            feeItemList.BalanceState = "0";
                            feeItemList.BalanceOper.ID = string.Empty;
                            feeItemList.BalanceOper.Name = string.Empty;
                            feeItemList.BalanceOper.OperTime = DateTime.MinValue;
                        }
                        else
                        {
                            decimal qty = feeItemList.Item.Qty;

                            feeItemList.Item.Qty = feeItemListTemp.Item.Qty - qty;//����
                            //�޸�ҽ���˷Ѹ���ȡ��ȷ������bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                            feeItemList.ConfirmedQty = feeItemList.ConfirmedQty - qty;

                            if (feeItemListTemp.UndrugComb.Qty != 0)
                            {
                                feeItemList.UndrugComb.Qty = FS.FrameWork.Public.String.FormatNumber(feeItemListTemp.UndrugComb.Qty * (feeItemList.Item.Qty / feeItemListTemp.Item.Qty), 2);
                            }
                            feeItemList.NoBackQty = feeItemList.Item.Qty; //
                            //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                            //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);

                            //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                            //�¼Ƽ۹����ȼ������Ƭ�۸����4��5�룬�����ܼ�
                            //סԺ���������շ��������в�֣�������˴������ֳ����ķ�����Ŀ�����г���
                            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;

                            //���¼������
                            returnValue = MedcareInterfaceProxy.RecomputeFeeItemListInpatient(patient, feeItemList);
                            if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
                            {
                                this.Err = MedcareInterfaceProxy.ErrMsg;

                                return -1;
                            }

                            feeItemList.FeeOper.ID = inpatientManager.Operator.ID;
                            if (string.IsNullOrEmpty(feeItemList.BalanceState))
                            {
                                feeItemList.BalanceState = "0";
                            }
                            feeItemList.ChargeOper.ID = inpatientManager.Operator.ID;
                            //���ֻ���ʱ����շ�ʱ��ͬ��
                            if (feeItemList.ChargeOper.OperTime <= DateTime.MinValue)
                            {
                                feeItemList.ChargeOper.OperTime = feeItemListTemp.FeeOper.OperTime;
                            }
                            feeItemList.TransType = TransTypes.Positive;

                            #region �帺
                            //����¼�Ĵ����ź�����¼�Ĵ�����һ��
                            feeItemListTemp.RecipeNO = feeItemList.RecipeNO;
                            feeItemListTemp.SequenceNO = feeItemList.SequenceNO;
                            feeItemListTemp.Item.Qty = -feeItemListTemp.Item.Qty;
                            feeItemListTemp.NoBackQty = 0;//������Ļ������˷�����ʱ�ɼ�
                            feeItemListTemp.UndrugComb.Qty = -feeItemListTemp.UndrugComb.Qty;

                            feeItemListTemp.FT.TotCost = -feeItemListTemp.FT.TotCost;
                            feeItemListTemp.FT.OwnCost = -feeItemListTemp.FT.OwnCost;
                            feeItemListTemp.FT.PayCost = -feeItemListTemp.FT.PayCost;
                            feeItemListTemp.FT.PubCost = -feeItemListTemp.FT.PubCost;
                            feeItemListTemp.FT.RebateCost = -feeItemListTemp.FT.RebateCost;
                            feeItemListTemp.FT.DefTotCost = -feeItemListTemp.FT.DefTotCost;

                            feeItemListTemp.TransType = TransTypes.Negative;
                            feeItemListTemp.FeeOper.OperTime = feeItemList.FeeOper.OperTime;
                            feeItemListTemp.CancelRecipeNO = feeItemList.CancelRecipeNO;
                            feeItemListTemp.CancelSequenceNO = feeItemList.CancelSequenceNO;

                            feeItemListTemp.FeeOper.ID = inpatientManager.Operator.ID;
                            if (feeItemListTemp.FT.User03 != "NOCHANGEDATE")//�޸Ļ��߷��ñ���ʱ�Ƿ�ѡ���������շ�����
                            {
                                feeItemListTemp.FeeOper.OperTime = dtNow;
                            }
                            feeItemListTemp.ChargeOper.ID = inpatientManager.Operator.ID;
                            //���ֻ���ʱ����շ�ʱ��ͬ��
                            if (feeItemListTemp.ChargeOper.OperTime <= DateTime.MinValue)
                            {
                                feeItemListTemp.ChargeOper.OperTime = feeItemListTemp.FeeOper.OperTime;
                            }

                            feeItemListTemp.BalanceState = "0";
                            feeItemListTemp.BalanceOper.ID = string.Empty;
                            feeItemListTemp.BalanceOper.Name = string.Empty;
                            feeItemListTemp.BalanceOper.OperTime = DateTime.MinValue;

                            feeItemListTemp.NoBackQty = 0;//��������Ϊ��
                            if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                            {
                                if (inpatientManager.InsertMedItemList(patient, feeItemListTemp) == -1)
                                {
                                    this.Err = Language.Msg("����ҩƷ��ϸ����!") + inpatientManager.Err;
                                    return -1;
                                }
                            }
                            else
                            {
                                if (inpatientManager.InsertFeeItemList(patient, feeItemListTemp) == -1)
                                {
                                    this.Err = Language.Msg("�����ҩƷ��ϸ����!") + inpatientManager.Err;
                                    return -1;
                                }
                            }
                            alFeeItemLists.Add(feeItemListTemp);
                            #endregion
                        }

                        #endregion

                    }

                    if (feeItemList.FT.User03 != "NOCHANGEDATE")//�޸Ļ��߷��ñ���ʱ�Ƿ�ѡ���������շ�����
                    {
                        DateTime feedate = feeItemList.FeeOper.OperTime.Date;
                        DateTime now = dtNow.Date;
                        if(feedate >= now)
                            feeItemList.FeeOper.OperTime = dtNow;
                    }
                    //���ֻ���ʱ����շ�ʱ��ͬ��
                    if (feeItemList.ChargeOper.OperTime <= DateTime.MinValue)
                    {
                        feeItemList.ChargeOper.OperTime = feeItemList.FeeOper.OperTime;
                    }
                    if (string.IsNullOrEmpty(feeItemList.Patient.Pact.ID))
                    {
                        feeItemList.Patient.Pact.ID = patient.Pact.ID;
                        feeItemList.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    }
                    //��������ڶ����շ�ӦΪ0 ��ֱ���շ�Ӧ��Ϊ��ǰ���ߵĽ������+1
                    //feeItemList.BalanceNO = patient.BalanceNO;
                    feeItemList.FeeOper.ID = inpatientManager.Operator.ID;

                    // �����շ�Ա����
                    if (feeItemList.FeeOper.Dept == null || string.IsNullOrEmpty(feeItemList.FeeOper.Dept.ID))
                    {
                        FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
                        if (oper != null)
                        {
                            feeItemList.FeeOper.Dept = oper.Dept;
                        }
                    }
                }

                //����ϸ��¼���շѻ��۱�־��ֵ
                if (payType == ChargeTypes.Fee)
                {
                    if (feeItemList.PayType == PayTypes.SendDruged)
                    {
                        feeItemList.PayType = PayTypes.SendDruged;
                    }
                    else
                    {
                        feeItemList.PayType = PayTypes.Balanced;
                    }

                }
                else
                {
                    feeItemList.PayType = PayTypes.Charged;
                }

                //���봦����ϸ��,�ֱ�ΪҩƷ,��ҩƷ
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    if (inpatientManager.InsertMedItemList(patient, feeItemList) == -1)
                    {
                        this.Err = Language.Msg("����ҩƷ��ϸ����!") + inpatientManager.Err;

                        return -1;
                    }
                }
                else
                {
                    if (inpatientManager.InsertFeeItemList(patient, feeItemList) == -1)
                    {
                        this.Err = Language.Msg("�����ҩƷ��ϸ����!") + inpatientManager.Err;

                        return -1;
                    }
                }
            }

            //��ȡ���µķ�������
            //alFeeItemLists.AddRange(feeItemLists);
            feeItemLists.AddRange(alFeeItemLists);

            ///���ô����ӿڴ���

            //���ڻ��ۺ��շ�����,���ϴ����ͨ��,����Ϊ�շ��뻮�۲�ͬ������,�շ���Ҫ������С����(MinFee.ID)����,������û��ܱ�
            //������סԺ����
            if (payType == ChargeTypes.Fee)
            {

                //��ð���MinFee.ID���������ݼ���
                //ArrayList sorList = this.CollectFeeItemLists(alFeeItemLists);
                ArrayList sorList = this.CollectFeeItemLists(feeItemLists);

                int iReturn = 0;
                //����һ������
                FT ftMain = new FT();
                FT ftBursaryTotMedFee = new FT();
                FS.HISFC.Models.Fee.Inpatient.FeeItemList temp = null;
                foreach (ArrayList list in sorList)
                {
                    temp = (list[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Clone();
                    temp.FT = new FT();

                    feeItemLists.AddRange(list);

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemTot in list)
                    {
                        if (feeItemTot.FT.DefTotCost == 0)
                        {
                            if (feeItemTot.Item.ItemType == EnumItemType.Drug)
                            {
                                feeItemTot.FT.DefTotCost = feeItemTot.FT.TotCost;
                            }
                            else
                            {
                                feeItemTot.FT.DefTotCost = feeItemTot.Item.Qty * feeItemTot.Item.DefPrice;
                            }
                        }
                        feeItemTot.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemTot.FT.DefTotCost, 2);
                        temp.FT.TotCost += feeItemTot.FT.TotCost;
                        temp.FT.OwnCost += feeItemTot.FT.OwnCost;
                        temp.FT.PayCost += feeItemTot.FT.PayCost;
                        temp.FT.PubCost += feeItemTot.FT.PubCost;
                        temp.FT.RebateCost += feeItemTot.FT.RebateCost;
                        temp.FT.DefTotCost += feeItemTot.FT.DefTotCost;
                        temp.SplitFeeFlag = feeItemTot.SplitFeeFlag;

                        ftMain.TotCost += feeItemTot.FT.TotCost;
                        ftMain.OwnCost += feeItemTot.FT.OwnCost;
                        ftMain.PayCost += feeItemTot.FT.PayCost;
                        ftMain.PubCost += feeItemTot.FT.PubCost;
                        ftMain.RebateCost += feeItemTot.FT.RebateCost;
                        ftMain.DefTotCost += feeItemTot.FT.DefTotCost;

                        if (feeItemTot.Item.ItemType == EnumItemType.Drug)
                        {
                            ftBursaryTotMedFee.TotCost += feeItemTot.FT.TotCost;
                            ftBursaryTotMedFee.OwnCost += feeItemTot.FT.OwnCost;
                            ftBursaryTotMedFee.PayCost += feeItemTot.FT.PayCost;
                            ftBursaryTotMedFee.PubCost += feeItemTot.FT.PubCost;
                            ftBursaryTotMedFee.RebateCost += feeItemTot.FT.RebateCost;
                            ftBursaryTotMedFee.DefTotCost += feeItemTot.FT.DefTotCost;
                        }
                    }

                    iReturn = inpatientManager.InsertAndUpdateFeeInfo(patient, temp);

                    if (iReturn <= 0)
                    {
                        this.Err = Language.Msg("������û�����Ϣ����!");

                        return -1;
                    }
                }

                //���������Ժ״̬,����ֱ���շѻ���,��ô�ڸ���סԺ�����ʱ���ж���Ժ״̬�Ƿ�Ϊ'O'
                if (this.isIgnoreInstate)
                {
                    iReturn = inpatientManager.UpdateInMainInfoFeeForDirQuit(patient.ID, ftMain);
                }
                else
                {
                    iReturn = inpatientManager.UpdateInMainInfoFee(patient.ID, ftMain);
                }

                if (iReturn == -1)
                {
                    this.Err = Language.Msg("����סԺ����ʧ��!") + inpatientManager.Err;

                    return -1;
                }

                //�������Ϊ0 ˵��������in_state <> 0��������ǰ̨��������¼�����.
                if (iReturn == 0)
                {
                    this.Err = patient.Name + Language.Msg("�Ѿ�������ߴ��ڷ���״̬�����ܼ���¼�����!����סԺ����ϵ!");

                    return -1;
                }
                FS.FrameWork.Models.NeuObject civilworkerObject = this.managerIntegrate.GetConstansObj("civilworker", patient.Pact.ID);

                if (patient.Pact.PayKind.ID == "03" && (ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) != 0)
                {
                    if (inpatientManager.UpdateBursaryTotMedFee(patient.ID, ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) < 1)
                    {
                        this.Err = "���¹��ѻ������޶��ۼ�ʧ��!" + this.Err;
                        return -1;
                    }
                }
                else if (civilworkerObject != null && !string.IsNullOrEmpty(civilworkerObject.ID))
                {
                    //�й�ҽ���¹��ѻ������޶��ۼ�
                    if (inpatientManager.UpdateSGYBursaryTotMedFee(patient.ID, ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) < 1)
                    {
                        this.Err = "���¹��ѻ������޶��ۼ�ʧ��!" + this.Err;
                        return -1;
                    }

                }
            }

            return 1;

        }

        /// <summary>
        /// �շѺ����л�ȡҽ���飬��̬����ҽ��������
        /// </summary>
        private static Hashtable HsMedicalTeam = null;

        /// <summary>
        /// �շѺ���
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <param name="payType">���� �շѱ�־</param>
        /// <param name="transType">�շ������� �����ױ�־</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int FeeManager(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList,
            ChargeTypes payType, TransTypes transType)
        {
            ArrayList temp = new ArrayList();

            temp.Add(feeItemList);

            return this.FeeManager(patient, ref temp, payType, transType);

        }

        /// <summary>
        /// �Ƿ�Ӥ���ķ��ü���ĸ����
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private bool IsMotherPayAllFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            if (motherPayAllFee == "1")//Ӥ���ķ���������������� 
            {
                if (patient.IsBaby) //סԺ��ˮ�ź���B,������Ӥ��
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region ���з���

        public string GetUndrugCode()
        {
            this.SetDB(itemManager);
            return itemManager.GetUndrugCode();
        }

        #region סԺ
        /// <summary>
        /// �����Ч��,��Ŀ���Ϊ��������Ŀ����
        /// </summary>
        /// <returns>�ɹ�:��Ŀ���� ʧ�ܷ���null</returns>
        public ArrayList QueryOperationItems()
        {
            this.SetDB(itemManager);

            return itemManager.QueryOperationItems();
        }

        /// <summary>
        /// ��ȡ��Ч��ICD������Ŀ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOperationICDItems()
        {
            this.SetDB(itemManager);
            return itemManager.QueryOperationICDItems();
        }

        /// <summary>
        /// ���߻��ܷ��ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeListInfoByInPatientNO(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ���߻��ܷ��ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų飨�ۺ�� = ҽԺ�ۺ�� - ҽ���ۣ�
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNOAndCI(string inPatientNO)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeListInfoByInPatientNOAndCI(inPatientNO);
        }

        /// <summary>
        /// ����С�����ܷ��ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}���С�������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientChildTotalFeeListInfoByInPatientNO(string inPatientNO)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientChildTotalFeeListInfoByInPatientNO(inPatientNO);
        }
        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ����סԺ��ˮ�š���Ʊ�š���ʿվ��// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(string inPatientNO, string invoiceNo,string nurseCode)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(inPatientNO, invoiceNo, nurseCode);
        }
        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientDetailFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientDetailFeeListInfoByInPatientNO(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//��Ҫ�����Ŀؼ�������Դ{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientDetailFeeDTInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeDTInfoByInPatientNO(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//��Ҫ�����Ŀؼ�������Դ ҽԺ����{940D2882-F02B-488f-A8E3-07689B0D064D}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientTotalFeeDTInfoByInPatientNOHospitalDrugNo(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeDTInfoByInPatientNOHospitalDrugNo(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}������豾������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientMontherDetailFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientMontherTotalFeeListInfoByInPatientNO(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//��Ҫ�����Ŀؼ�������Դ //{105E05C7-E3E0-43B6-B88F-480861F1F4B6}������豾������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientMontherDetailFeeDTInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientMontherTotalFeeDTInfoByInPatientNO(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ������ϸ���ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�//��Ҫ�����Ŀؼ�������Դ//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}���С�������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientChildDetailFeeDTInfoByInPatientNO(string inPatientNO)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientChildTotalFeeDTInfoByInPatientNO(inPatientNO);
        }

        /// <summary>
        /// ����һ�շ��ò�ѯ-��סԺ��ˮ�źͷ�Ʊ�Ų�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientOneDayFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo,string date)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientOneDayFeeListInfoByInPatientNO(inPatientNO, invoiceNo, date);
        }

        /// <summary>
        /// ����Ԥ���㱨�� {34a15202-a3f9-4d3e-9bad-c7e6783b540c}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetBalanceFeeByInPatienNo(string inPatientNO, string invoiceNo)
        {
           this.SetDB(itemManager);
           return itemManager.GetBalanceFeeByInPatienNo(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ����Ԥ���㱨��
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetDTBalanceFeeByInPatienNo(string inPatientNO, string invoiceNo)
        {
            this.SetDB(itemManager);
            return itemManager.GetDTBalanceFeeByInPatienNo(inPatientNO, invoiceNo);
        }

        /// <summary>
        /// ��÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="undrugCode"></param>
        /// <returns>�ɹ� ��ҩƷ��Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetUndrugByCode(string undrugCode)
        {
            this.SetDB(itemManager);

            return itemManager.GetUndrugByCode(undrugCode);
        }
        /// <summary>
        /// ͨ�������ţ��õ�������ϸ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <returns>null ���� ArrayList Fee.OutPatient.FeeItemListʵ�弯��</returns>
        public ArrayList QueryFeeDetailFromRecipeNO(string recipeNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryFeeDetailFromRecipeNO(recipeNO);
        }

        /// <summary>
        /// ������￨����ˮ,Ĭ��Ϊ9λ�ֳ�,ǰ�油0
        /// </summary>
        /// <returns>�ɹ� ���￨�� ʧ�� null</returns>
        public string GetAutoCardNO()
        {
            this.SetDB(outpatientManager);

            return outpatientManager.GetAutoCardNO();
        }

        /// <summary>
        /// ���ݴ����źʹ�����Ŀ��ˮ�Ÿ���Ժע��ȷ������
        /// </summary>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSquence">��������ˮ��</param>
        /// <param name="qty">Ժע����</param>
        /// <returns>�ɹ�: >= 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
        public int UpdateConfirmInject(string moOrder, string recipeNO, string recipeSquence, int qty)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateConfirmInject(moOrder, recipeNO, recipeSquence, qty);
        }

        /// <summary>
        /// ���ݾ������жϻ����Ƿ�Ƿ��
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="totCost">�����շѽ��</param>
        /// <returns>true Ƿ�� false ��Ƿ��</returns>
        public bool IsPatientLackFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return IsPatientLackFee(patient, 0);
        }

        /// <summary>
        /// ���ݾ������жϻ����Ƿ�Ƿ��
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="totCost">�����շѽ��</param>
        /// <returns>true Ƿ�� false ��Ƿ��</returns>
        public bool IsPatientLackFee(FS.HISFC.Models.RADT.PatientInfo patient, decimal totCost)
        {
            //δ���þ�����
            if (!patient.PVisit.AlertFlag)
            {
                return false;
            }
            else
            {
                //��������������ΪM ����ж�ʱ��С�ڴ˽�����Ϊ��Ƿ������
                //��������������ΪD �����ж��ǣ��ڴ������ڣ����ж�Ƿ�ѣ�����M ����ж�

                if (patient.PVisit.AlertType.ID.ToString() == "D")
                {
                    DateTime dtNow = inpatientManager.GetDateTimeFromSysDateTime();
                    if (dtNow >= patient.PVisit.BeginDate
                        && dtNow < patient.PVisit.EndDate)
                    {
                        return false;
                    }
                    else
                    {
                        if (patient.FT.LeftCost - totCost > patient.PVisit.MoneyAlert)
                        {
                            return false;
                        }
                    }
                }
                else if (patient.PVisit.AlertType.ID.ToString() == "M")
                {
                    if (patient.FT.LeftCost - totCost > patient.PVisit.MoneyAlert)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ����סԺ�����շѵķ���
        /// </summary>
        /// <param name="patient">������Ϣ</param>
        /// <param name="feeItemLists">������Ϣ</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT ComputeInpatientFee(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList feeItemLists)
        {
            if (IsMotherPayAllFee(patient))
            {
                string motherInpatientNO = this.RadtIntegrate.QueryBabyMotherInpatientNO(patient.ID);
                if (string.IsNullOrEmpty(motherInpatientNO) || motherInpatientNO == "-1")
                {
                    this.Err = "û���ҵ�Ӥ����ĸ��סԺ��ˮ��" + this.RadtIntegrate.Err;
                    return null;
                }
                patient = this.RadtIntegrate.GetPatientInfomation(motherInpatientNO);//������Ļ�����Ϣ�滻Ӥ���Ļ�����Ϣ
            }

            //��ָ�����Ŀ
            ArrayList alFeeInfo = this.ConvertFeeItemToSingle(patient, feeItemLists);

            //ȡ���ϵĵ�һ��Ԫ���ж��Ƿ�����ϸ
            long returnValue = 0;
            this.MedcareInterfaceProxy.SetPactTrans(this.trans);
            //������ýӿ�û�г�ʼ��,��ô���ݻ��ߵĺ�ͬ��λ��ʼ�����ýӿ�
            if (medcareInterfaceProxy != null)
            {
                returnValue = MedcareInterfaceProxy.SetPactCode(patient.Pact.ID);
                if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
                {
                    this.Err = MedcareInterfaceProxy.ErrMsg;
                    return null;
                }
            }

            FS.HISFC.Models.Base.FT ft = null;
            FS.HISFC.Models.Base.FT patientFt = patient.FT.Clone();
            returnValue = MedcareInterfaceProxy.QueryFeeDetailsInpatient(patient, ref alFeeInfo);
            if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
            {
                this.Err = MedcareInterfaceProxy.ErrMsg;
                return null;
            }

            ft = patient.FT.Clone();
            patient.FT = patientFt.Clone();

            return ft;
        }

        /// <summary>
        /// �ж��Ƿ���״̬
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>true ������flase ����</returns>
        [Obsolete("���Ƽ�ʹ���ˣ��þ����������D���滻", true)]
        public bool IsUnLock(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DateTime dtNow = inpatientManager.GetDateTimeFromSysDateTime();
            if (dtNow >= patient.PVisit.BeginDate
                && dtNow < patient.PVisit.EndDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// �Ƿ����״̬
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public bool GetStopAccount(string inpatientNo)
        {
            if (inpatientManager.GetStopAccount(inpatientNo) == "1")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ��ѯ���к�ͬ��λ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitAll()
        {
            this.SetDB(pactManager);

            return pactManager.QueryPactUnitAll();
        }
        /// <summary>
        /// ��������ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitOutPatient()
        {
            this.SetDB(pactManager);
            return pactManager.QueryPactUnitOutPatient();
        }
        /// <summary>
        /// ���סԺ��ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitInPatient()
        {
            this.SetDB(pactManager);
            return pactManager.QueryPactUnitInPatient();
        }
        /// <summary>
        /// �ύ����
        /// </summary>
        /// ����HIS4.5.0.1��commit��ʽ�޸�
        public void Commit()
        {
            //this.trans.Commit();
            //if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null)
            //{
            //    medcareInterfaceProxy.Commit();
            //}
            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null && medcareInterfaceProxy.PactCode != "" && medcareInterfaceProxy.PactCode != null)
            {
                if (medcareInterfaceProxy.Commit() < 0) //����ҽ�� 0�ɹ� -1ʧ��
                {
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    //this.trans.Rollback();
                }
                else
                {
                    //this.trans.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //{A6CDF67A-DEBE-4ce6-AC8B-CC0CAB9B1B0E}
                    medcareInterfaceProxy.Disconnect();
                }
            }
            else if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null && medcareInterfaceProxy.PactCode == "")
            {
                //this.trans.Commit()
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                //this.trans.Commit();
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }
        /// <summary>
        /// �ύ���ѽӿں���
        /// </summary>
        /// ���ﵥ����ҩ���˷���ҩʱʹ���ˣ������ط������ҪҲ����ʹ��
        public int MedcareInterfaceCommit()
        {

            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null && medcareInterfaceProxy.PactCode != "" && medcareInterfaceProxy.PactCode != null)
            {
                if (medcareInterfaceProxy.Commit() < 0) //����ҽ�� 0�ɹ� -1ʧ��
                {
                    medcareInterfaceProxy.Rollback();
                    return -1;
                }
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// �ع����ѽӿں���
        /// </summary>
        public void MedcareInterfaceRollback()
        {
            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null)
            {
                medcareInterfaceProxy.Rollback();
            }
        }
        /// <summary>
        /// �ع�����
        /// </summary>
        public void Rollback()
        {
            //this.trans.Rollback();
            FS.FrameWork.Management.PublicTrans.RollBack();
            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null)
            {
                medcareInterfaceProxy.Rollback();
            }
        }



        /// <summary>
        /// ��Ŀ�շ�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int FeeItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Fee, TransTypes.Positive);
        }

        /// <summary>
        /// ��Ŀ�շ�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int FeeItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Fee, TransTypes.Positive);
        }

        /// <summary>
        /// ��Ŀ�˷�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QuitItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Fee, TransTypes.Negative);
        }

        /// <summary>
        /// ��Ŀ�˷�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QuitItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Fee, TransTypes.Negative);
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemList">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int ChargeItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Charge, TransTypes.Positive);
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="feeItemLists">������ϸ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int ChargeItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Charge, TransTypes.Positive);
        }

        /// <summary>
        /// ѭ�����������ϸ
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <param name="balanceLists">������ϸ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int InsertBalanceList(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList balanceLists)
        {
            this.SetDB(inpatientManager);

            int returnValue = 0;

            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList in balanceLists)
            {
                returnValue = inpatientManager.InsertBalanceList(patient, balanceList);
                if (returnValue == -1)
                {
                    return -1;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// ��÷�ƱĬ����ʼ��
        /// </summary>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <returns>�ɹ� ��ƱĬ����ʼ�� ʧ�� null</returns>
        //public string GetInvoiceDefaultStartCode(FS.HISFC.Models.Fee.InvoiceTypeEnumService invoiceType)
        //{
        //    this.SetDB(invoiceServiceManager);

        //    return invoiceServiceManager.GetDefaultStartCode(invoiceType);
        //}

        public string GetInvoiceDefaultStartCode(string invoiceType)
        {
            this.SetDB(invoiceServiceManager);

            return invoiceServiceManager.GetDefaultStartCode(invoiceType);
        }

        /// <summary>
        /// ������з�Ʊ����Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ʊ����Ϣ ʧ�� null</returns>
        public ArrayList QueryFinaceGroupAll()
        {
            this.SetDB(employeeFinanceGroupManager);

            return employeeFinanceGroupManager.QueryFinaceGroupIDAndNameAll();
        }

        /// <summary>
        /// ��֤��Ʊ�����Ƿ�Ϸ�
        /// </summary>
        /// <param name="startNO">��ʼ��</param>
        /// <param name="endNO">������</param>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <returns>�Ϸ� True, ���Ϸ� false</returns>
        //public bool InvoicesIsValid(int startNO, int endNO, FS.HISFC.Models.Fee.InvoiceTypeEnumService invoiceType)
        //{
        //    this.SetDB(invoiceServiceManager);

        //    return invoiceServiceManager.InvoicesIsValid(startNO, endNO, invoiceType);
        //}
        public bool InvoicesIsValid(int startNO, int endNO, string invoiceType)
        {
            this.SetDB(invoiceServiceManager);

            return invoiceServiceManager.InvoicesIsValid(startNO, endNO, invoiceType);
        }

        /// <summary>
        /// ��÷�Ʊ�����DataSet
        /// </summary>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <param name="ds">��Ʊ�����DataSet</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int GetInvoiceClass(string invoiceType, ref DataSet ds)
        {
            this.SetDB(outpatientManager);
            // TODO: ���벻��ȥ����ʱ��һ��
            return outpatientManager.GetInvoiceClass(invoiceType, ref ds);
        }

        /// <summary>
        /// ��û���ҩƷ��Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryMedcineList(string inpatientNO, DateTime beginTime, DateTime endTime, string deptCode)
        {
            this.SetDB(inpatientManager);

            return inpatientManager.QueryMedItemListsByInpatientNO(inpatientNO, beginTime, endTime, deptCode);

        }

        /// <summary>
        /// ��û��߷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByInpatientNO(string inpatientNO, DateTime beginTime, DateTime endTime, string deptCode)
        {
            this.SetDB(inpatientManager);

            return inpatientManager.QueryFeeItemListsByInpatientNO(inpatientNO, beginTime, endTime, deptCode);
        }

        public ArrayList GetMedItemsForInpatient(string inpatientNO, DateTime beginTime, DateTime endTime)
        {
            return inpatientManager.GetMedItemsForInpatient(inpatientNO, beginTime, endTime);
        }

        //{CBF49C01-5D42-407a-9F85-4E81081D562E}
        /// <summary>
        /// ����ִ�п��Ҳ������Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="execDeptCode">ִ�п��Ҵ���</param>
        /// <returns></returns>
        public ArrayList GetMedItemsForInpatientByExecDeptCode(string inpatientNo, DateTime beginTime, DateTime endTime, string execDeptCode)
        {
            return inpatientManager.GetMedItemsForInpatientByExecDept(inpatientNo, beginTime, endTime, execDeptCode);
        }

        public ArrayList QueryFeeItemLists(string inpatientNO, DateTime beginTime, DateTime endTime)
        {
            return inpatientManager.QueryFeeItemLists(inpatientNO, beginTime, endTime);
        }

        //{CBF49C01-5D42-407a-9F85-4E81081D562E}
        /// <summary>
        /// ����ִ�п��Ҳ�ѯ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="execDeptCode"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByExecDeptCode(string inpatientNO, DateTime beginTime, DateTime endTime, string execDeptCode)
        {
            return inpatientManager.QueryFeeItemListsByExecDeptCode(inpatientNO, beginTime, endTime, execDeptCode);
        }
        /// <summary>
        /// ����ҩƷ�ͷ�ҩƷ��ϸ������¼---ͨ������{5C2A9C83-D165-434c-ACA4-86F23E956442}
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequence">��������ˮ��</param>
        /// <param name="isPharmacy">�Ƿ�ҩƷ Drug(true)�� UnDrug(false)��ҩƷ</param>
        /// <returns>�ɹ�: ҩƷ�ͷ�ҩƷ��ϸ������¼ ʧ��: null</returns>
        public FS.HISFC.Models.Fee.Inpatient.FeeItemList GetItemListByRecipeNO(string recipeNO, int recipeSequence, EnumItemType isPharmacy)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.GetItemListByRecipeNO(recipeNO, recipeSequence, isPharmacy);
        }

        #region ��ҩƷ��Ŀ��Ϣ

        /// <summary>
        /// ��÷�ҩƷ��Ϣ�������Ƿ���Ч��
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemCode)
        {
            this.SetDB(itemManager);

            //houwb ��ѯ��Ч��Ӧ���Ƿ���GetValidItemByUndrugCode
            //return itemManager.GetValidItemByUndrugCode(itemCode);
            return itemManager.GetItemByUndrugCode(itemCode);
        }

        //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ȡ����ҩƷ������
        /// <summary>
        /// ȡ����ҩƷ������
        /// </summary>
        /// <param name="itemCode">��ҩƷ�����ʱ���</param>
        /// <param name="price">���ۡ���ҩƷ�ɴ���0</param>
        /// <returns>��ҩƷ������ʵ��</returns>
        public FS.HISFC.Models.Base.Item GetUndrugAndMatItem(string itemCode, decimal price)
        {
            this.SetDB(itemManager);
            if (itemCode.StartsWith("F"))
            {
                return itemManager.GetValidItemByUndrugCode(itemCode);
            }
            else
            {
                FS.HISFC.Models.FeeStuff.MaterialItem matItem = materialManager.GetMetItem(itemCode);
                if (matItem == null)
                {
                    return null;
                }
                matItem.ItemType = EnumItemType.MatItem;
                matItem.Price = price;
                (matItem as FS.HISFC.Models.Base.Item).Specs = matItem.Specs;
                matItem.SysClass.ID = "U";
                return matItem;
            }
        }

        /// <summary>
        /// ��Ŀ�Ƿ�ʹ�ù�
        /// </summary>
        /// <param name="itemCode">��ĿID</param>
        /// <returns>true:ʹ��</returns>
        public bool IsUsed(string itemCode)
        {
            this.SetDB(itemManager);
            return itemManager.IsUsed(itemCode);
        }

        /// <summary>
        /// ɾ����ҩƷ��Ϣ
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1 δɾ�������� 0</returns>
        public int DeleteUndrugItemByCode(string undrugID)
        {
            this.SetDB(itemManager);
            return itemManager.DeleteUndrugItemByCode(undrugID);
        }

        /// <summary>
        /// ������п��ܵ���Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ч�Ŀ�����Ŀ��Ϣ, ʧ�� null</returns>
        public ArrayList QueryValidItems()
        {
            this.SetDB(itemManager);
            return itemManager.QueryValidItems();
        }

        /// <summary>
        /// ���������Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ��Ϣ, ʧ�� null</returns>
        public List<FS.HISFC.Models.Fee.Item.Undrug> QueryAllItemsList()
        {
            this.SetDB(itemManager);
            return itemManager.QueryAllItemList();
        }

        #endregion

        #region ��ҩƷ����

        /// <summary>
        ///  �����ҩƷ�����Ŀ
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�в������� 0</returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public int InsertUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);
            //return undrugCombManager.InsertUndrugComb(undrugComb);
        }

        /// <summary>
        /// ���� ��ҩƷ�����е�����
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и��µ����� 0</returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public int UpdateUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.UpdateUndrugComb(undrugComb);
        }

        /// <summary>
        ///  ɾ����ҩƷ�����Ŀ
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û��ɾ�������� 0</returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public int DeleteUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.DeleteUndrugComb(undrugComb);
        }

        /// <summary>
        /// ͨ�������Ŀ�����ȡһ�������Ŀ
        /// </summary>
        /// <param name="undrugCombCode">�����Ŀ����</param>
        /// <returns>�ɹ�: һ�������Ŀ ʧ��: null</returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public FS.HISFC.Models.Fee.Item.UndrugComb GetUndrugCombByCode(string undrugCombCode)
        {
            FS.HISFC.Models.Fee.Item.UndrugComb com = new FS.HISFC.Models.Fee.Item.UndrugComb();
            return com;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.GetUndrugCombByCode(undrugCombCode);
        }

        /// <summary>
        /// ͨ�������Ŀ�����ȡһ����Ч�����Ŀ
        /// </summary>
        /// <param name="undrugCombCode">�����Ŀ����</param>
        /// <returns>�ɹ�: һ����Ч�����Ŀ ʧ��: null</returns>
        [Obsolete("����,������Ŀ�ѹ鲢����ҩƷ", true)]
        public FS.HISFC.Models.Fee.Item.UndrugComb GetUndrugCombValidByCode(string undrugCombCode)
        {
            FS.HISFC.Models.Fee.Item.UndrugComb com = new FS.HISFC.Models.Fee.Item.UndrugComb();
            return com;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.GetUndrugCombValidByCode(undrugCombCode);
        }
        /// <summary>
        /// ��ȡ������Ŀ���ܼ۸�
        /// </summary>
        /// <param name="undrugCombCode">������Ŀ����</param>
        /// <returns></returns>
        public decimal GetUndrugCombPrice(string undrugCombCode)
        {
            this.SetDB(undrugPackAgeMgr);

            return undrugPackAgeMgr.GetUndrugCombPrice(undrugCombCode);
        }

        #endregion

        /// <summary>
        /// ���·�ҩƷ��ϸ��������
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequence">��������ˮ��</param>
        /// <param name="qty">��������</param>
        /// <param name="balanceState">����״̬</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и�������: 0</returns>
        public int UpdateNoBackQtyForUndrug(string recipeNO, int recipeSequence, decimal qty, string balanceState)
        {
            if (balanceState == "5")//{139695FB-AA0D-485F-BFBF-56928F441250}������Ŀ�����������
            {
               return  UpdateClinicNoBackQtyForUndrug(recipeNO, recipeSequence, qty);
            }
            else
            {
                this.SetDB(inpatientManager);
                return inpatientManager.UpdateNoBackQtyForUndrug(recipeNO, recipeSequence, qty, balanceState);
            }
        }

        /// <summary>
        /// ���·�ҩƷ��ϸ��������
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequence">��������ˮ��</param>
        /// <param name="qty">��������</param>
        /// <param name="balanceState">����״̬</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и�������: 0</returns>
        public int UpdateClinicNoBackQtyForUndrug(string recipeNO, int recipeSequence, decimal qty)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateNoBackQty(recipeNO, recipeSequence, qty);
        }

        /// <summary>
        /// ���·�ҩƷ��ϸȷ������
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequence">��������ˮ��</param>
        /// <param name="confrimNum">ȷ������</param>
        /// <param name="balanceState">����״̬</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и�������: 0</returns>
        public int UpdateConfirmNumForUndrug(string recipeNO, int recipeSequence, decimal confrimNum, string balanceState)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateConfirmNumForUndrug(recipeNO, recipeSequence, confrimNum, balanceState);
        }

        /// <summary>
        /// ���·�ҩƷ��ϸ��չ���
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequence">��������ˮ��</param>
        /// <param name="extFlag2">��չ���2</param>
        /// <param name="balanceState">����״̬</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и�������: 0</returns>
        public int UpdateExtFlagForUndrug(string recipeNO, int recipeSequence, string extFlag2, string balanceState)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateExtFlagForUndrug(recipeNO, recipeSequence, extFlag2, balanceState);
        }

        /// <summary>
        /// ��û��ߺ�ִ�п����Ѿ�ȷ�ϵķ�ҩƷ�շ���ϸ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="execDeptCode">���Ҵ���</param>
        /// <returns>�ɹ�:���߷�ҩƷ��Ϣ ʧ��:null û���ҵ���¼ ArrayList.Count = 0</returns>
        public ArrayList QueryExeFeeItemListsByInpatientNOAndDept(string inpatientNO, string execDeptCode)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.QueryExeFeeItemListsByInpatientNOAndDept(inpatientNO, execDeptCode);
        }

        /// <summary>
        /// ��ѯ��λ��
        /// </summary>
        /// <param name="minFeeCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedFeeItemByMinFeeCode(string minFeeCode)
        {
            this.SetDB(inpatientManager);
            return feeBedFeeItem.QueryBedFeeItemByMinFeeCode(minFeeCode);
        }

        /// <summary>
        /// ��ѯ��λ�ѣ����ڸ��ˣ�
        /// </summary>
        /// <param name="minFeeCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.BedFeeItem QueryBedFeeItemForPatient(string inpatientNO, string bedNO, string bedGradeKey)
        {
            this.SetDB(feeBedFeeItem);
            return feeBedFeeItem.QueryBedGradeForPatient(inpatientNO, bedNO, bedGradeKey);
        }

        /// <summary>
        /// ���洲λ�ѣ����ˣ�
        /// </summary>
        /// <param name="bedFeeItem"></param>
        /// <returns></returns>
        public int SaveBedFeeItemForPatient(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {
            this.SetDB(feeBedFeeItem);

            int i = feeBedFeeItem.UpdateBedFeeItemForPatient(bedFeeItem);
            if (i == 0)
            {
                i = feeBedFeeItem.InsertBedFeeItemForPatient(bedFeeItem);
                if (i < 0)
                {
                    this.Err = feeBedFeeItem.Err;
                    return -1;
                }
            }
            else if (i < 0)
            {
                this.Err = feeBedFeeItem.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ�·�ҩƷ������
        /// </summary>
        /// <returns></returns>
        public string GetUndrugRecipeNO()
        {
            this.SetDB(inpatientManager);
            return inpatientManager.GetUndrugRecipeNO();
        }

        /// <summary>
        /// ��ѯ��Ч��Ŀ
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetValidItemByUndrugCode(string itemID)
        {
            this.SetDB(itemManager);
            return itemManager.GetValidItemByUndrugCode(itemID);
        }

        #region ��ȡ�۸�

        /// <summary>
        /// ������Ŀ��ϸ
        /// </summary>
        private Hashtable hsUndrugZTDetail = null;

        /// <summary>
        /// ��ȡ��ҩƷ��ϸ�۸�ļӳɣ��Żݣ�����
        /// </summary>
        /// <param name="ztCode"></param>
        /// <param name="detailCode"></param>
        /// <returns></returns>
        public decimal GetItemRateForZT(string ztCode, string detailCode)
        {
            if (hsUndrugZTDetail == null)
            {
                hsUndrugZTDetail = new Hashtable();

            }
            List<FS.HISFC.Models.Fee.Item.UndrugComb> ztList = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
            FS.HISFC.BizLogic.Manager.UndrugztManager undrugZTMgr = new FS.HISFC.BizLogic.Manager.UndrugztManager();

            if (!hsUndrugZTDetail.ContainsKey(ztCode))
            {
                undrugZTMgr.QueryUnDrugztDetail(ztCode, ref ztList);
                hsUndrugZTDetail.Add(ztCode, ztList);
            }

            List<FS.HISFC.Models.Fee.Item.UndrugComb> listZT = hsUndrugZTDetail[ztCode] as List<FS.HISFC.Models.Fee.Item.UndrugComb>;

            if (listZT != null)
            {
                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in listZT)
                {
                    if (undrugComb.ID == detailCode)
                    {
                        return undrugComb.ItemRate;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// �����ͬ��λ��̬����������ÿ�β�ѯ�۸�ȡ��ͬ��λ��Ϣ
        /// </summary>
        private Hashtable htPactInfo = new Hashtable();

        /// <summary>
        /// סԺȡ�۸��������ݺ�ͬ��λ����Ŀ��Ϣ��ȡ�����շѼ۸��ԭʼ�۸�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="item">�շ���Ŀ</param>
        /// <param name="feePrice">�շѼ۸�</param>
        /// <param name="orgPrice">ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�</param>
        /// <returns></returns>
        [Obsolete("����ԭ���Ļ�ȡ�۸�ĺ����������µĻ�ȡ�۸���")]
        private int GetPriceForInpatient(string pactCode, FS.HISFC.Models.Base.Item item, ref decimal feePrice, ref decimal orgPrice)
        {
            if (IGetItemPrice == null)
            {
                IGetItemPrice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice)) as FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice;
            }

            if (IGetItemPrice != null)
            {
                if (item.ItemType == EnumItemType.Drug)
                {
                    decimal defaultPrice = ((FS.HISFC.Models.Pharmacy.Item)item).PriceCollection.RetailPrice;
                    decimal purchasePrice = ((FS.HISFC.Models.Pharmacy.Item)item).RetailPrice2;

                    feePrice = IGetItemPrice.GetPriceForInpatient(item.ID, null, defaultPrice, defaultPrice, defaultPrice, purchasePrice, ref orgPrice);
                }
                else
                {
                    feePrice = IGetItemPrice.GetPriceForInpatient(item.ID, null, item.Price, item.ChildPrice, item.SpecialPrice, item.Price, ref orgPrice);
                }
            }
            else
            {
                PactInfo pactinfo = null;
                if (htPactInfo.Contains(pactCode))
                {
                    pactinfo = htPactInfo[pactCode] as PactInfo;
                }
                else
                {
                    pactinfo = this.pactManager.GetPactUnitInfoByPactCode(pactCode);
                    htPactInfo.Add(pactCode, pactinfo);
                }
                if (pactinfo == null)
                {
                    this.Err = "���ݺ�ͬ��λ��ȡ�۸����";
                    return -1;
                }
                if (item.ItemType == EnumItemType.Drug)
                {
                    decimal defaultPrice = ((FS.HISFC.Models.Pharmacy.Item)item).PriceCollection.RetailPrice;
                    decimal purchasePrice = ((FS.HISFC.Models.Pharmacy.Item)item).RetailPrice2;
                    if (pactinfo.PriceForm == "�����" && purchasePrice != 0)
                    {
                        feePrice = purchasePrice;
                    }
                    else
                    {
                        feePrice = defaultPrice;
                    }
                }
                else
                {
                    if (pactinfo.PriceForm == "�����" && item.SpecialPrice != 0)
                    {
                        feePrice = item.SpecialPrice;
                    }
                    else
                    {
                        feePrice = item.Price;
                    }
                }
                orgPrice = item.Price;
            }
            return 1;
        }

        /// <summary>
        /// סԺȡ�۸��������ݺ�ͬ��λ����Ŀ��Ϣ��ȡ�����շѼ۸��ԭʼ�۸�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="item">�շ���Ŀ</param>
        /// <param name="feePrice">�շѼ۸�</param>
        /// <param name="orgPrice">ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�</param>
        /// <param name="rate">�۸�ӳɣ��Żݣ�����</param>
        /// <returns></returns>
        public int GetPriceForInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Item item, ref decimal feePrice, ref decimal orgPrice, decimal rate)
        {
            decimal price = GetPriceForInpatient(patientInfo, item, ref feePrice, ref orgPrice);
            feePrice = feePrice * rate;

            return 1;
        }

        /// <summary>
        /// סԺȡ�۸��������ݺ�ͬ��λ����Ŀ��Ϣ��ȡ�����շѼ۸��ԭʼ�۸�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="item">�շ���Ŀ</param>
        /// <param name="feePrice">�շѼ۸�</param>
        /// <param name="orgPrice">ԭʼ�۸񣨱���Ӧ�ռ۸񣬲����Ǻ�ͬ��λ���أ�</param>
        /// <returns></returns>
        public int GetPriceForInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Item item, ref decimal feePrice, ref decimal orgPrice)
        {
            if (IGetItemPrice == null)
            {
                IGetItemPrice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice)) as FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice;
            }

            if (IGetItemPrice != null)
            {
                if (item.ItemType == EnumItemType.Drug)
                {
                    decimal defaultPrice = ((FS.HISFC.Models.Pharmacy.Item)item).PriceCollection.RetailPrice;
                    decimal purchasePrice = ((FS.HISFC.Models.Pharmacy.Item)item).RetailPrice2;

                    feePrice = IGetItemPrice.GetPriceForInpatient(item.ID, patientInfo, defaultPrice, defaultPrice, defaultPrice, purchasePrice, ref orgPrice);
                }
                else
                {
                    feePrice = IGetItemPrice.GetPriceForInpatient(item.ID, patientInfo, item.Price, item.ChildPrice, item.SpecialPrice, item.Price, ref orgPrice);
                }

                return 1;
            }
            else
            {
                return GetPriceForInpatient(patientInfo.Pact.ID, item, ref feePrice, ref orgPrice);
            }
        }

        #region  //{B9303CFE-755D-4585-B5EE-8C1901F79450}��д��ȡ�۸���
        /// <summary>
        /// ���ݼ۸���ʽ��ȡ�۸�
        /// </summary>
        /// <param name="PriceForm">�۸���ʽ</param>
        /// <param name="age">����</param>
        /// <param name="UnitPrice">Ĭ�ϼ۸�</param>
        /// <param name="ChildPrice">��ͯ��</param>
        /// <param name="SPPrice">�����</param>
        /// <param name="PurchasePrice">�����</param>
        /// <returns></returns>
        [Obsolete("����ԭ���Ļ�ȡ�۸�ĺ����������µĻ�ȡ�۸���")]
        public decimal GetPrice(string PriceForm, int age, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice)
        {
            if (PriceForm == "���׼�")//����
            {
                return UnitPrice;
            }
            else if (PriceForm == "��ͯ��")//��ͯ
            {
                if (age <= 14)
                {
                    return ChildPrice;
                }
                return UnitPrice;
            }
            else if (PriceForm == "�����")
            {
                if (PurchasePrice != 0)
                {
                    return PurchasePrice;
                }
                else
                {
                    return UnitPrice;
                }
            }
            else if (PriceForm == "�����")
            {
                return SPPrice;
            }
            //else if (age <= 14)
            //{
            //    return ChildPrice;
            //}
            else
            {
                return UnitPrice;
            }
        }

        /// <summary>
        /// ���ݼ۸���ʽ��ȡ�۸�
        /// </summary>
        /// <param name="PriceForm">�۸���ʽ</param>
        /// <param name="age">����</param>
        /// <param name="UnitPrice">Ĭ�ϼ۸�</param>
        /// <param name="ChildPrice">��ͯ��</param>
        /// <param name="SPPrice">�����</param>
        /// <param name="PurchasePrice">�����</param>
        /// <param name="rate">�۸�ӳɣ��Żݣ�����</param>
        /// <returns></returns>
        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, int age, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice, decimal rate)
        {
            decimal price = this.GetPrice(itemCode, register, age, UnitPrice, ChildPrice, SPPrice, PurchasePrice, ref orgPrice);

            return price * rate;
        }

        /// <summary>
        /// ���ݼ۸���ʽ��ȡ�۸�
        /// </summary>
        /// <param name="PriceForm">�۸���ʽ</param>
        /// <param name="age">����</param>
        /// <param name="UnitPrice">Ĭ�ϼ۸�</param>
        /// <param name="ChildPrice">��ͯ��</param>
        /// <param name="SPPrice">�����</param>
        /// <param name="PurchasePrice">�����</param>
        /// <returns></returns>
        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, int age, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice)
        {
            if (IGetItemPrice == null)
            {
                IGetItemPrice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice)) as FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice;
            }

            if (IGetItemPrice != null)
            {
                return IGetItemPrice.GetPrice(itemCode, register, UnitPrice, ChildPrice, SPPrice, PurchasePrice, ref orgPrice);
            }
            else
            {
                //��Ŀ����
                decimal rate = this.itemManager.GetItemRate(itemCode);
                //�۸�
                decimal price = UnitPrice;

                if (register != null)
                {
                    price = this.GetPrice(register.Pact.PriceForm, age, UnitPrice, ChildPrice, SPPrice, PurchasePrice);
                }
                //ԭʼ�۸�
                orgPrice = price;
                //�¼۸�
                return price * rate;
            }
        }

        #endregion

        #endregion

        #endregion

        #region ����

        #region ����

        /// <summary>
        /// ���ָ�����Ʋ���
        /// </summary>
        /// <param name="controlID">������ID</param>
        /// <param name="defaultValue">Ĭ��ֵ��û���ҵ����ش�ֵ</param>
        /// <returns>���Ʋ���</returns>
        public string GetControlValue(string controlID, string defaultValue)
        {
            string tempValue = string.Empty;

            if (controlerHelper == null || controlerHelper.ArrayObject == null || controlerHelper.ArrayObject.Count <= 0)
            {
                tempValue = controlManager.QueryControlerInfo(controlID);
            }
            else
            {
                NeuObject obj = controlerHelper.GetObjectFromID(controlID);

                if (obj == null)
                {
                    tempValue = controlManager.QueryControlerInfo(controlID);
                }
                else
                {
                    tempValue = ((FS.HISFC.Models.Base.ControlParam)obj).ControlerValue;
                }
            }

            if (tempValue == null || tempValue == string.Empty)
            {
                return defaultValue;
            }
            else
            {
                return tempValue;
            }
        }

        #endregion

        #region �����շѺ���

        /// <summary>
        /// ��õ�ǰ�ӿڲ��
        /// </summary>
        /// <typeparam name="T">�ӿ�����</typeparam>
        /// <param name="controlCode">��������������</param>
        /// <param name="defalutInstance">Ĭ�ϲ��</param>
        /// <returns>�ɹ�T����ʵ�� ���� ����Ĭ��ʵ��</returns>
        public T GetPlugIns<T>(string controlCode, T defalutInstance)
        {
            string controlValue = controlParamIntegrate.GetControlParam<string>(controlCode, true, string.Empty);

            if (controlValue == string.Empty)
            {
                return defalutInstance;
            }

            string dllName = string.Empty;
            string namesSpaceAndUcName = string.Empty;

            try
            {
                dllName = controlValue.Split('|')[0];
                namesSpaceAndUcName = controlValue.Split('|')[1];

                Assembly assemblyName = System.Reflection.Assembly.LoadFrom(Application.StartupPath + dllName);

                System.Runtime.Remoting.ObjectHandle objPlugin = null;

                objPlugin = System.Activator.CreateInstance(assemblyName.ToString(), namesSpaceAndUcName);

                if (objPlugin == null)
                {
                    MessageBox.Show("����ʧ��!��ȷ����ѡ���dll��uc����ȷ������! ������Ĭ�ϲ��");

                    return defalutInstance;
                }

                object obj = objPlugin.Unwrap();

                defalutInstance = default(T);

                return (T)obj;
            }
            catch (Exception e)
            {
                MessageBox.Show("��ǰ�������ά������! ������Ĭ�ϲ��" + e.Message);

                return defalutInstance;
            }
        }

        /// <summary>
        /// ��û��ߵ�δ�շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public System.Collections.ArrayList QueryChargedFeeItemListsByClinicNO(string clinicNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryChargedFeeItemListsByClinicNO(clinicNO);
        }

        /// <summary>
        /// ��û��ߵ����շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public System.Collections.ArrayList QueryFeeItemListsByClinicNO(string clinicNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryFeeItemListsByClinicNO(clinicNO);
        }

        /// <summary>
        /// ��û���һ�ο�������з�����Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="isFee">ALL��ʾȫ��</param>
        /// <param name="subFlag">ALL��ʾȫ��</param>
        /// <param name="costSource">ALL��ʾȫ��</param>
        /// <returns></returns>
        public System.Collections.ArrayList QueryAllFeeItemListsByClinicNO(string clinicNO, string isFee, string subFlag, string costSource)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryAllFeeItemListsByClinicNO(clinicNO, isFee, subFlag, costSource);
        }

        /// <summary>
        /// ���Ʋ���������
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ת�����Ұ�����
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsInvertDept = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �ִ����ź������
        /// </summary>
        private static bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// ÿ�������ɷ�Ϊ��
        /// </summary>
        public static bool isDoseOnceCanNull = false;

        /// <summary>
        /// ����СƱ�Ŀ���
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper printRecipeHeler = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���[������ˮ��]�ʹ�����
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeItemLists"></param>
        /// <param name="recipeNO"></param>
        /// <param name="sequence"></param>
        public void GetRecipeNoAndMaxSeq(ArrayList feeItemLists, ref string recipeNO, ref int sequence, string clinicCode)
        {
            if (feeItemLists == null || feeItemLists.Count <= 0)
            {
                return;
            }

            foreach (FeeItemList feeItem in feeItemLists)
            {
                if (feeItem.RecipeNO != null && feeItem.RecipeNO.Length > 0)
                {
                    recipeNO = feeItem.RecipeNO;

                    sequence = NConvert.ToInt32(outpatientManager.GetMaxSeqByRecipeNO(recipeNO, clinicCode));

                    break;
                }
            }
        }

        FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe iSplitRecipe = null;

        /// <summary>
        /// ����շ���Ŀ�б��� ϵͳ���ִ�п��ң����� ���ƴ�����
        /// ͬһϵͳ���ͳһִ�п��ң�ͬһ��������Ŀ��������ͬ
        /// ���Ѿ�����ô����ŵ���Ŀ���������·���
        /// </summary>
        /// <param name="feeDetails">������Ϣ</param>
        /// <param name="t">���ݿ�Trans</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>falseʧ�� true�ɹ�</returns>
        public bool SetRecipeNOOutpatient(Register r, ArrayList feeDetails, ref string errText)
        {
            bool isTj = false;
            foreach (FeeItemList f in feeDetails)
            {
                if ( (f.Item.SpecialFlag1 == "1" || f.Item .SpecialFlag1 =="2" )&& f.FTSource == "3")
                {
                    isTj = true;
                }
                else
                {
                   
                    isTj = false;
                   // break;
                }
            }
            
            if (isTj)
            {
                string recipeNo = outpatientManager.GetRecipeNO();
                int tempSequence = 1;
                foreach (FeeItemList f in feeDetails)
                {
                    if (string.IsNullOrEmpty(f.RecipeNO))
                    {
                        f.RecipeNO = recipeNo;
                        f.SequenceNO = tempSequence;
                       
                    }
                    tempSequence++;
                }
            }
            else
            {
                if (iSplitRecipe == null)
                {
                    iSplitRecipe = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe)) as FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe;
                }
                if (iSplitRecipe != null)
                {
                    //�ִ���
                    return iSplitRecipe.SplitRecipe(r, feeDetails, ref errText);
                    //if (returnValue < 0)
                    //{
                    //    return false;
                    //}

                }
                else
                {
                    #region Ĭ�ϵ�ʵ��
                    bool isDealCombNO = false; //�Ƿ����ȴ�����Ϻ�
                    int noteCounts = 0;        //��õ��Ŵ���������Ŀ��

                    //�Ƿ����ȴ�����Ϻ�
                    isDealCombNO = controlParamIntegrate.GetControlParam<bool>(Const.DEALCOMBNO, false, true);

                    //��õ��Ŵ���������Ŀ��, Ĭ����Ŀ�� 5
                    noteCounts = controlParamIntegrate.GetControlParam<int>(Const.NOTECOUNTS, false, 5);

                    //�Ƿ����ϵͳ���
                    isDecSysClassWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(Const.DEC_SYS_WHENGETRECIPE, false, false);

                    //�Ƿ����ȴ����ݴ��¼
                    bool isDecTempSaveWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(Const.���������ȿ��Ƿַ���¼, false, false);

                    ArrayList sortList = new ArrayList();


                    while (feeDetails.Count > 0)
                    {
                        ArrayList sameNotes = new ArrayList();
                        FeeItemList compareItem = feeDetails[0] as FeeItemList;
                        foreach (FeeItemList f in feeDetails)
                        {
                            if (isDecSysClassWhenGetRecipeNO)
                            {
                                if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && f.Days == compareItem.Days && (isDecTempSaveWhenGetRecipeNO ? f.RecipeSequence == compareItem.RecipeSequence : true))
                                {
                                    sameNotes.Add(f);
                                }
                            }
                            else
                            {
                                if (f.Item.SysClass.ID.ToString() == compareItem.Item.SysClass.ID.ToString()
                                    && f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && f.Days == compareItem.Days && (isDecTempSaveWhenGetRecipeNO ? f.RecipeSequence == compareItem.RecipeSequence : true))
                                {
                                    sameNotes.Add(f);
                                }
                            }

                        }
                        sortList.Add(sameNotes);
                        foreach (FeeItemList f in sameNotes)
                        {
                            feeDetails.Remove(f);
                        }
                    }

                    foreach (ArrayList temp in sortList)
                    {
                        ArrayList combAll = new ArrayList();
                        ArrayList noCombAll = new ArrayList();
                        ArrayList noCombUnits = new ArrayList();
                        ArrayList noCombFinal = new ArrayList();


                        if (isDealCombNO)//���ȴ�����Ϻţ������е���Ϻ������·���
                        {
                            //��ѡ��û����Ϻŵ���Ŀ
                            foreach (FeeItemList f in temp)
                            {
                                if (f.Order.Combo.ID == null || f.Order.Combo.ID == string.Empty)
                                {
                                    noCombAll.Add(f);
                                }
                            }
                            //������������ɾ��û����Ϻŵ���Ŀ
                            foreach (FeeItemList f in noCombAll)
                            {
                                temp.Remove(f);
                            }
                            //���ͬһ���������Ŀ�������·���
                            while (noCombAll.Count > 0)
                            {
                                noCombUnits = new ArrayList();
                                foreach (FeeItemList f in noCombAll)
                                {
                                    if (noCombUnits.Count < noteCounts)
                                    {
                                        noCombUnits.Add(f);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                noCombFinal.Add(noCombUnits);
                                foreach (FeeItemList f in noCombUnits)
                                {
                                    noCombAll.Remove(f);
                                }
                            }
                            //���ʣ�����Ŀ��Ŀ> 0˵��������ϵ���Ŀ
                            if (temp.Count > 0)
                            {
                                while (temp.Count > 0)
                                {
                                    ArrayList combNotes = new ArrayList();
                                    FeeItemList compareItem = temp[0] as FeeItemList;
                                    foreach (FeeItemList f in temp)
                                    {
                                        if (f.Order.Combo.ID == compareItem.Order.Combo.ID)
                                        {
                                            combNotes.Add(f);
                                        }
                                    }
                                    combAll.Add(combNotes);
                                    foreach (FeeItemList f in combNotes)
                                    {
                                        temp.Remove(f);
                                    }
                                }
                            }
                            foreach (ArrayList tempNoComb in noCombFinal)
                            {
                                string recipeNo = null;//������ˮ��
                                int noteSeq = 1;//��������Ŀ��ˮ��

                                string tempRecipeNO = string.Empty;
                                int tempSequence = 0;
                                this.GetRecipeNoAndMaxSeq(tempNoComb, ref tempRecipeNO, ref tempSequence, r.ID);

                                if (tempRecipeNO != string.Empty && tempSequence > 0)
                                {
                                    tempSequence += 1;
                                    foreach (FeeItemList f in tempNoComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = tempRecipeNO;
                                            f.SequenceNO = tempSequence;
                                            tempSequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    recipeNo = outpatientManager.GetRecipeNO();
                                    if (recipeNo == null || recipeNo == string.Empty)
                                    {
                                        errText = "��ô����ų���!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempNoComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = recipeNo;
                                            f.SequenceNO = noteSeq;
                                            noteSeq++;
                                        }
                                    }
                                }
                            }
                            foreach (ArrayList tempComb in combAll)
                            {
                                string recipeNo = null;//������ˮ��
                                int noteSeq = 1;//��������Ŀ��ˮ��

                                string tempRecipeNO = string.Empty;
                                int tempSequence = 0;
                                this.GetRecipeNoAndMaxSeq(tempComb, ref tempRecipeNO, ref tempSequence, r.ID);

                                if (tempRecipeNO != string.Empty && tempSequence > 0)
                                {
                                    tempSequence += 1;
                                    foreach (FeeItemList f in tempComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = tempRecipeNO;
                                            f.SequenceNO = tempSequence;
                                            tempSequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    recipeNo = outpatientManager.GetRecipeNO();
                                    if (recipeNo == null || recipeNo == string.Empty)
                                    {
                                        errText = "��ô����ų���!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = recipeNo;
                                            f.SequenceNO = noteSeq;
                                            noteSeq++;
                                        }
                                    }
                                }
                            }
                        }
                        else //�����ȴ�����Ϻ�
                        {
                            ArrayList counts = new ArrayList();
                            ArrayList countUnits = new ArrayList();
                            while (temp.Count > 0)
                            {
                                countUnits = new ArrayList();
                                foreach (FeeItemList f in temp)
                                {
                                    if (countUnits.Count < noteCounts)
                                    {
                                        countUnits.Add(f);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                counts.Add(countUnits);
                                foreach (FeeItemList f in countUnits)
                                {
                                    temp.Remove(f);
                                }
                            }

                            //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                            Hashtable hs = new Hashtable();


                            foreach (ArrayList tempCounts in counts)
                            {
                                string recipeNO = null;//������ˮ��
                                int recipeSequence = 1;//��������Ŀ��ˮ��

                                string tempRecipeNO = string.Empty;
                                int tempSequence = 0;
                                this.GetRecipeNoAndMaxSeq(tempCounts, ref tempRecipeNO, ref tempSequence, r.ID);
                                //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                                if (hs.Contains(tempRecipeNO))
                                {
                                    tempSequence = FS.FrameWork.Function.NConvert.ToInt32((hs[tempRecipeNO] as FS.FrameWork.Models.NeuObject).Name);
                                }
                                else
                                {
                                    FS.FrameWork.Models.NeuObject obj = new NeuObject();
                                    obj.ID = tempRecipeNO;
                                    obj.Name = tempSequence.ToString();
                                    hs.Add(tempRecipeNO, obj);
                                }

                                if (tempRecipeNO != string.Empty && tempSequence > 0)
                                {
                                    tempSequence += 1;
                                    foreach (FeeItemList f in tempCounts)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = tempRecipeNO;
                                            f.SequenceNO = tempSequence;
                                            tempSequence++;
                                        }
                                    }
                                    //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                                    if (hs.Contains(tempRecipeNO))
                                    {
                                        (hs[tempRecipeNO] as FS.FrameWork.Models.NeuObject).Name = tempSequence.ToString();
                                    }
                                }
                                else
                                {
                                    recipeNO = outpatientManager.GetRecipeNO();
                                    if (recipeNO == null || recipeNO == string.Empty)
                                    {
                                        errText = "��ô����ų���!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempCounts)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            f.RecipeNO = recipeNO;
                                            f.SequenceNO = recipeSequence;
                                            recipeSequence++;
                                        }
                                    }//{B24B174D-F261-4c6b-94C9-EEED0F736013}
                                    if (!hs.Contains(tempRecipeNO))
                                    {
                                        FS.FrameWork.Models.NeuObject obj = new NeuObject();
                                        obj.ID = recipeNO;
                                        obj.Name = recipeSequence.ToString();
                                        hs.Add(recipeNO, obj);
                                    }
                                }


                            }
                        }
                    }
                    #endregion
                }
            }
            return true;
        }

        /// <summary>
        /// ���������շ����С�ÿ��ҽ������,���ݴ������γɶ����շѴ�����
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public bool SetRecipeFeeSeqOutPatient(Register r, ArrayList feeDetails, ref string errText)
        {
            try
            {
                //û���շ���Ŀ��ֱ�ӷ���
                if (feeDetails == null || feeDetails.Count <= 0)
                {
                    return true;
                }

                // ����Ƿ�ֿ�����Ŀ��������ҽ���������շ�,Ĭ��ΪֵΪ false�������ճ����������շ�����.
                bool isSplitByExceeded = controlParamIntegrate.GetControlParam<bool>(Const.IS_SPLIT_RECIPE_SEQ_BY_EXCEED, false, false);


                //1��ҩƷ�ͷ�ҩƷ�ֿ���2����ҩƷȫ����һ��3��ҩƷ����ȡҩҩ���ֿ���4��ͬһ��Ϻ�COMB_NO��һ�𣬷��򸨲�ɾ���ʹ���������
                Hashtable hsRecipeFeeSeq = new Hashtable();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    #region ��ҩƷ(����)������Ŀ��һ��-�ȹ��˺���ͳһ����

                    if (f.Item.IsMaterial)
                    {
                        continue;
                    }

                    #endregion

                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        #region ҩƷ����ȡҩҩ���ֿ�

                        string pharmacyDeptCode = f.ExecOper.Dept.ID;  //ҩƷ��ȡҩҩ��
                        if (hsRecipeFeeSeq.Contains(pharmacyDeptCode))
                        {
                            ArrayList alPharmacyDeptList = hsRecipeFeeSeq[pharmacyDeptCode] as ArrayList;
                            alPharmacyDeptList.Add(f);
                        }
                        else
                        {
                            ArrayList alPharmacyDeptList = new ArrayList();
                            alPharmacyDeptList.Add(f);
                            hsRecipeFeeSeq.Add(pharmacyDeptCode, alPharmacyDeptList);
                        }

                        #endregion
                    }
                    else if (f.Item.ItemType == EnumItemType.UnDrug)
                    {
                        #region ��ҩƷ(�Ǹ���)ȫ����һ��

                        if (isSplitByExceeded)
                        {
                            if (hsRecipeFeeSeq.Contains("undrug") && !f.IsExceeded)
                            {
                                ArrayList alUndrug = hsRecipeFeeSeq["undrug"] as ArrayList;
                                alUndrug.Add(f);
                            }
                            else if (hsRecipeFeeSeq.Contains("exceeded") && f.IsExceeded)
                            {
                                ArrayList alUndrug = hsRecipeFeeSeq["exceeded"] as ArrayList;
                                alUndrug.Add(f);
                            }
                            else if (f.IsExceeded)
                            {
                                ArrayList alUndrug = new ArrayList();
                                alUndrug.Add(f);
                                hsRecipeFeeSeq.Add("exceeded", alUndrug);
                            }
                            else
                            {
                                ArrayList alUndrug = new ArrayList();
                                alUndrug.Add(f);
                                hsRecipeFeeSeq.Add("undrug", alUndrug);
                            }

                        }
                        else
                        {
                            if (hsRecipeFeeSeq.Contains("undrug"))
                            {
                                ArrayList alUndrug = hsRecipeFeeSeq["undrug"] as ArrayList;
                                alUndrug.Add(f);
                            }
                            else
                            {
                                ArrayList alUndrug = new ArrayList();
                                alUndrug.Add(f);
                                hsRecipeFeeSeq.Add("undrug", alUndrug);
                            }
                        }

                        #endregion
                    }
                }

                //ͨ��HashTable���ֱ�ֵͬһ����շ����С���������Ŀ��
                IDictionaryEnumerator iDE = hsRecipeFeeSeq.GetEnumerator();
                while (iDE.MoveNext())
                {
                    ArrayList al = iDE.Value as ArrayList;
                    if (al == null || al.Count <= 0)
                    {
                        continue;
                    }

                    //���ͬһ����У��Ѿ����շ����У���ʹ�þɵ��շ����У�����ʹ��������
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList fTemp = al[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    string recipeFeeSeq = fTemp.RecipeSequence;
                    if (string.IsNullOrEmpty(recipeFeeSeq))
                    {
                        recipeFeeSeq = this.outpatientManager.GetRecipeSequence();
                    }

                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in al)
                    {
                        f.RecipeSequence = recipeFeeSeq;
                    }
                }

                //������ͬ��ŵĸ��ģ��շ����к�������Ŀһ�¡������ġ�
                Hashtable hsSubFeeByCombNo = new Hashtable();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    //�ҳ�ͬһ��Ϻŵ�����Ŀ�շ�����
                    if (f.Item.IsMaterial)
                    {
                        continue;
                    }

                    string combNO = f.Order.Combo.ID;
                    if (!hsSubFeeByCombNo.Contains(combNO))
                    {
                        hsSubFeeByCombNo.Add(combNO, f.RecipeSequence);
                    }
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    //�ҵ����ĵ�����Ŀ���շ����и�ֵ
                    if (f.Item.IsMaterial)
                    {
                        string combNO = f.Order.Combo.ID;
                        string recipeFeeSeq = (hsSubFeeByCombNo[combNO] == null ? "" : hsSubFeeByCombNo[combNO].ToString());
                        if (string.IsNullOrEmpty(recipeFeeSeq))
                        {
                            recipeFeeSeq = this.outpatientManager.GetRecipeSequence();
                        }
                        f.RecipeSequence = recipeFeeSeq;
                    }
                }

            }
            catch (Exception ex) { }

            return true;

        }

        /// <summary>
        /// ������ϸ����У��
        /// </summary>
        /// <param name="f">����ʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        public bool IsFeeItemListDataValid(FeeItemList f, ref string errText)
        {
            string itemName = f.Item.Name;
            if (f == null)
            {
                errText = itemName + "��÷���ʵ�����!";

                return false;
            }
            if (f.Item.ID == null || f.Item.ID == string.Empty)
            {
                errText = itemName + "��Ŀ����û�и�ֵ";

                return false;
            }
            if (f.Item.Name == null || f.Item.Name == string.Empty)
            {
                errText = itemName + "��Ŀ����û�и�ֵ";

                return false;
            }
            //if (f.Item.IsPharmacy)
            if (f.Item.ItemType == EnumItemType.Drug && f.FTSource != "0")
            {
                #region ���ݲ���&& !isDoseOnceCanNull ���ж��Ƿ���Ҫ�������ֵ ����ǿ20070828
                if ((f.Order.Frequency.ID == null || f.Order.Frequency.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "Ƶ�δ���û�и�ֵ";

                    return false;
                }
                if ((f.Order.Usage.ID == null || f.Order.Usage.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "�÷�����û�и�ֵ";

                    return false;
                }
                if (f.Order.DoseOnce == 0 && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "ÿ������û�и�ֵ";

                    return false;
                }
                if ((f.Order.DoseUnit == null || f.Order.DoseUnit == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "ÿ��������λû�и�ֵ";

                    return false;
                }
                #endregion
            }

            if (f.Item.ID != "999")
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    //if (f.Item.Specs == null || f.Item.Specs == string.Empty)
                    //{
                    //    errText = itemName + "�Ĺ��û�и�ֵ";

                    //    return false;
                    //}
                    if (f.Item.PackQty == 0)
                    {
                        errText = itemName + "��װ����û�и�ֵ";

                        return false;
                    }
                }
            }
            if (f.Item.PriceUnit == null || f.Item.PriceUnit == string.Empty)
            {
                errText = itemName + "�Ƽ۵�λû�и�ֵ";

                return false;
            }

            if (f.Item.MinFee.ID == null || f.Item.MinFee.ID == string.Empty)
            {
                errText = itemName + "��С����û�и�ֵ";

                return false;
            }
            if (f.Item.SysClass.ID == null || f.Item.SysClass.Name == string.Empty)
            {
                errText = itemName + "ϵͳ���û�и�ֵ";

                return false;
            }
            if (f.Item.Qty == 0)
            {
                errText = itemName + "����û�и�ֵ";

                return false;
            }
            //����������ô�����ʱ����{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.Item.Qty < 0)
            //{
            //    errText = itemName + "��������С��0";

            //    return false;
            //}
            if (f.Item.Qty > 99999)
            {
                errText = itemName + "�������ܴ���99999";

                return false;
            }

            if (f.Days == 0)
            {
                errText = itemName + "��ҩ����û�и�ֵ";

                return false;
            }
            if (f.Days < 0)
            {
                errText = itemName + "��ҩ��������С��0";

                return false;
            }

            if (f.Item.Price < 0)
            {
                errText = itemName + "���۲���С��0";

                return false;
            }

            //�����Ա�ҩ�� ������ȡ����Ϊ0��Ŀ
            if (this.IsFeeWhenPriceZero == "0")
            {
                if (f.Item.ID != "999")
                {
                    if (f.Item.Price == 0 && f.Item.User03 != "ȫ��")
                    {
                        errText = itemName + "����û�и�ֵ";

                        return false;
                    }
                    //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost == 0)
                    //{
                    //    errText = itemName + "��Ŀ���û�и�ֵ";

                    //    return false;
                    //}
                }
            }

            //����������ô�����ʱ����{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost < 0)
            //{
            //    errText = itemName + "��Ŀ���Ϊ��";

            //    return false;
            //}
            ////{8DF48FD8-14E9-464a-A368-256B19A0EE54} �޸��ֻ����
            //if (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) != FS.FrameWork.Public.String.FormatNumber
            //    (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost /*+ f.FT.RebateCost*/, 2))
            //{
            //    errText = itemName + "����뵥����������";

            //    return false;
            //}

            if (f.Item.ID == "999" && f.Item.ItemType == EnumItemType.Drug)
            {
            }
            else
            {
                if (f.ExecOper.Dept.ID == null || f.ExecOper.Dept.ID == string.Empty)
                {
                    errText = itemName + "ִ�п��Ҵ���û�и�ֵ";

                    return false;
                }
                if (f.ExecOper.Dept.Name == null || f.ExecOper.Dept.Name == string.Empty)
                {
                    errText = itemName + "ִ�п�������û�и�ֵ";

                    return false;
                }
            }

            return true;
        }

        #region ɾ�������������ܻ�����Ϣ
        /// <summary>
        /// ���������ˮ�źͷ�Ʊ��Ϻ�ɾ����������Ϣ��
        /// </summary>
        /// <param name="ClinicNO">�����ˮ��</param>
        /// <param name="RecipeNO">��Ʊ��Ϻ�</param>
        /// <returns></returns>
        public int DeleteFeeItemListByClinicNOAndRecipeNO(string ClinicNO, string RecipeNO)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeItemListByClinicNOAndRecipeNO(ClinicNO, RecipeNO);
        }
        #endregion


        /// <summary>
        /// ���·�Ʊ�����FIN_OPB_INVOICEINFO
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="balanceFlag"></param>
        /// <param name="balanceNo"></param>
        /// <param name="dtBalanceDate"></param>
        /// <returns></returns>
        public int UpdateInvoiceForDayBalance(System.DateTime dtBegin, System.DateTime dtEnd, string balanceFlag, string balanceNo, System.DateTime dtBalanceDate)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.UpdateInvoiceForDayBalance(dtBegin, dtEnd, balanceFlag, balanceNo, dtBalanceDate);
        }
        /// <summary>
        /// ���·�Ʊ��ϸ��FIN_OPB_INVOICEDETAIL
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="balanceFlag"></param>
        /// <param name="balanceNo"></param>
        /// <param name="dtBalanceDate"></param>
        /// <returns></returns>
        public int UpdateInvoiceDetailForDayBalance(System.DateTime dtBegin, System.DateTime dtEnd, string balanceFlag, string balanceNo, System.DateTime dtBalanceDate)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.UpdateInvoiceForDayBalance(dtBegin, dtEnd, balanceFlag, balanceNo, dtBalanceDate);
        }
        /// <summary>
        /// ����֧�������FIN_OPB_PAYMODE
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="balanceFlag"></param>
        /// <param name="balanceNo"></param>
        /// <param name="dtBalanceDate"></param>
        /// <returns></returns>
        public int UpdatePayModeForDayBalance(System.DateTime dtBegin, System.DateTime dtEnd, string balanceFlag, string balanceNo, System.DateTime dtBalanceDate)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.UpdatePayModeForDayBalance(dtBegin, dtEnd, balanceFlag, balanceNo, dtBalanceDate);
        }
        public static string invoiceStytle = "0";//��Ʊ��ʽ

        /// <summary>
        /// �����������շ�����
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private ArrayList GetRecipeSequenceForChk(ArrayList feeItemLists)
        {
            ArrayList list = new ArrayList();

            foreach (FeeItemList f in feeItemLists)
            {
                if (list.IndexOf(f.RecipeSequence) >= 0)
                {
                    continue;
                }
                else
                {
                    list.Add(f.RecipeSequence);
                }
            }

            return list;
        }

        /// <summary>
        /// ���Э������
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList SplitNostrumDetail(FS.HISFC.Models.Registration.Register rInfo, FeeItemList f, ref string errText)
        {
            List<FS.HISFC.Models.Pharmacy.Nostrum> listDetail = this.PharmarcyManager.QueryNostrumDetail(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (listDetail == null)
            {
                errText = "���Э��������ϸ����!" + PharmarcyManager.Err;

                return null;
            }
            decimal price = 0;
            decimal count = 0;
            string feeCode = string.Empty;
            string itemType = string.Empty;
            decimal totCost = 0;
            decimal packQty = 0m;
            FeeItemList feeDetail = null;
            if (f.Order.ID == null || f.Order.ID == string.Empty)
            {
                f.Order.ID = this.orderManager.GetNewOrderID();
                if (f.Order.ID == null || f.Order.ID == string.Empty)
                {
                    errText = "���ҽ����ˮ�ų���!";

                    return null;
                }
            }
            string comboNO = string.Empty;
            if (string.IsNullOrEmpty(f.Order.Combo.ID))
            {
                comboNO = f.Order.Combo.ID;
            }
            else
            {
                comboNO = orderManager.GetNewOrderComboID();
            }
            foreach (FS.HISFC.Models.Pharmacy.Nostrum nosItem in listDetail)
            {
                FS.HISFC.Models.Pharmacy.Item item = PharmarcyManager.GetItem(nosItem.Item.ID);
                if (item == null)
                {
                    errText = "����Э��������ϸ����!";

                    continue;
                }

                feeDetail = new FeeItemList();
                feeDetail.Item = item;
                feeCode = item.MinFee.ID;
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - rInfo.Birthday.Ticks)).TotalDays / 365);
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
                    string priceForm = rInfo.Pact.PriceForm;
                    price = this.GetPrice(priceForm, age, item.Price, item.SpecialPrice, item.ChildPrice, item.PriceCollection.PurchasePrice);
                    //if (item.SysClass.ID.ToString() != "PCC")
                    //{
                    //    price = this.GetPrice(priceObj);
                    //}
                    //else
                    //{
                    //    price = item.Price;
                    //}
                    packQty = item.PackQty;
                    price = FS.FrameWork.Public.String.FormatNumber(
                            NConvert.ToDecimal(price / packQty), 4);
                }
                catch (Exception e)
                {
                    errText = e.Message;

                    return null;
                }
                //��ȡ��Э����������С��λ��ȡ,��ϸ���� = �����������Э���������� * ��Ӧ��ϸ��Ŀ���� / Э��������װ��
                if (f.FeePack == "0")//��С��λ
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty / f.Item.PackQty;
                }
                else //��ȡ��Э�������԰�װ��λ��ȡ,��ϸ���� = �����������Э���������� * ��Ӧ��ϸ��Ŀ����
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty;
                }
                totCost = price * count;

                feeDetail.Patient = f.Patient.Clone();
                feeDetail.Name = feeDetail.Item.Name;
                feeDetail.ID = feeDetail.Item.ID;
                feeDetail.RecipeOper = f.RecipeOper.Clone();
                feeDetail.Item.Price = price;
                feeDetail.Days = NConvert.ToDecimal(f.Days);
                feeDetail.FT.TotCost = totCost;
                feeDetail.Item.Qty = count;
                feeDetail.FeePack = f.FeePack;

                //�Է���ˣ�������Ϲ�����Ҫ���¼���!!!
                feeDetail.FT.OwnCost = totCost;
                feeDetail.ExecOper = f.ExecOper.Clone();
                feeDetail.Item.PriceUnit = item.MinUnit == string.Empty ? "g" : item.MinUnit;
                if (item.IsMaterial)
                {
                    feeDetail.Item.IsNeedConfirm = true;
                }
                else
                {
                    feeDetail.Item.IsNeedConfirm = false;
                }
                feeDetail.Order = f.Order;
                feeDetail.UndrugComb.ID = f.Item.ID;
                feeDetail.UndrugComb.Name = f.Item.Name;
                feeDetail.Order.Combo.ID = f.Order.Combo.ID;
                feeDetail.Item.IsMaterial = f.Item.IsMaterial;
                feeDetail.RecipeSequence = f.RecipeSequence;
                feeDetail.FTSource = f.FTSource;
                feeDetail.FeePack = f.FeePack;
                feeDetail.IsNostrum = true;
                feeDetail.Invoice = f.Invoice;
                feeDetail.InvoiceCombNO = f.InvoiceCombNO;
                feeDetail.NoBackQty = feeDetail.Item.Qty;
                feeDetail.Order.Combo.ID = comboNO;
                //if (this.rInfo.Pact.PayKind.ID == "03")
                //{
                //    FS.HISFC.Models.Base.PactItemRate pactRate = null;

                //    if (pactRate == null)
                //    {
                //        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, feeDetail.Item.ID);
                //    }
                //    if (pactRate != null)
                //    {
                //        if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                //        {
                //            if (pactRate.Rate.PayRate == 1)//�Է�
                //            {
                //                feeDetail.ItemRateFlag = "1";
                //            }
                //            else
                //            {
                //                feeDetail.ItemRateFlag = "3";
                //            }
                //        }
                //        else
                //        {
                //            feeDetail.ItemRateFlag = "2";

                //        }
                //        if (f.ItemRateFlag == "3")
                //        {
                //            feeDetail.OrgItemRate = f.OrgItemRate;
                //            feeDetail.NewItemRate = f.NewItemRate;
                //            feeDetail.ItemRateFlag = "2";
                //        }
                //    }
                //    else
                //    {
                //        if (f.ItemRateFlag == "3")
                //        {

                //            if (rowFindZT["ZF"].ToString() != "1")
                //            {
                //                feeDetail.OrgItemRate = f.OrgItemRate;
                //                feeDetail.NewItemRate = f.NewItemRate;
                //                feeDetail.ItemRateFlag = "2";
                //            }
                //        }
                //        else
                //        {
                //            feeDetail.OrgItemRate = f.OrgItemRate;
                //            feeDetail.NewItemRate = f.NewItemRate;
                //            feeDetail.ItemRateFlag = f.ItemRateFlag;
                //        }
                //    }
                //}

                alTemp.Add(feeDetail);
            }
            if (alTemp.Count > 0)
            {
                if (f.FT.RebateCost > 0)//�м���
                {
                    if (rInfo.Pact.PayKind.ID != "01")
                    {
                        errText = "��ʱ��������Էѻ��߼���!";

                        return null;
                    }
                    //���ⵥ����
                    decimal rebateRate =
                        FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost / f.FT.OwnCost, 2);
                    decimal tempFix = 0;
                    decimal tempRebateCost = 0;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost) * rebateRate;
                        tempRebateCost += feeTemp.FT.RebateCost;
                    }
                    tempFix = f.FT.RebateCost - tempRebateCost;
                    FeeItemList fFix = alTemp[0] as FeeItemList;
                    fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                }
            }
            if (alTemp.Count > 0)
            {
                if (f.SpecialPrice > 0)//�������Է�
                {
                    decimal tempPrice = 0m;
                    string id = string.Empty;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        if (feeTemp.Item.Price > tempPrice)
                        {
                            id = feeTemp.Item.ID;
                            tempPrice = feeTemp.Item.Price;
                        }
                    }

                    foreach (FeeItemList fee in alTemp)
                    {
                        if (fee.Item.ID == id)
                        {
                            fee.SpecialPrice = f.SpecialPrice;

                            break;
                        }
                    }
                }
            }

            return alTemp;
        }

        /// <summary>
        /// ���Э������
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private int SplitNostrumDetail(Register rInfo, ref ArrayList feeItemLists, ref ArrayList drugList, ref string errText)
        {
            ArrayList itemList = new ArrayList();
            foreach (FeeItemList f in feeItemLists)
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    if (!f.IsConfirmed)
                    {
                        if (!f.Item.IsNeedConfirm && f.Item.ID != "999")
                        {
                            drugList.Add(f);
                        }
                    }
                    if (f.IsNostrum)
                    {
                        ArrayList al = SplitNostrumDetail(rInfo, f, ref errText);
                        if (al == null)
                        {
                            return -1;
                        }
                        if (al.Count == 0)
                        {
                            errText = f.Item.Name + "��Э������,����û��ά����ϸ������ϸ�Ѿ�ͣ�ã�������Ϣ����ϵ��";
                            return -1;
                        }
                        itemList.AddRange(al);
                        continue;
                    }
                }
                itemList.Add(f);

            }
            feeItemLists.Clear();
            feeItemLists.AddRange(itemList);
            return 1;
        }


        /// <summary>
        /// �����շѺ���
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5}
        /// 
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", false, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }

        /// <summary>
        /// �����շѺ���
        /// 
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// 
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="isTempInvoice">�Ƿ�ʹ����ʱ��Ʊ</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="invoiceFeeDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", isTempInvoice, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }

        /// <summary>
        /// �����շѺ���
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5} 
        /// ���Ӳ���������ָ����Ʊ����
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="isTempInvoice">�Ƿ�ʹ����ʱ��Ʊ</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="invoiceFeeDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <param name="oper">������</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, string invoiceType, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText, Employee oper)
        {

            //Terminal.Confirm ConfirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();
            //SOC.HISFC.BizLogic.Pharmacy.Item socItemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

            if (this.trans != null)
            {
                ConfirmIntegrate.SetTrans(this.trans);
                bookingIntegrate.SetTrans(this.trans);
                //socItemMgr.SetTrans(this.trans);
            }

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //�Ƿ�ŷ�Э������
            bool isSplitNostrum = controlParamIntegrate.GetControlParam<bool>(Const.Split_NostrumDetail, false, false);

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //����շѲ���Ա
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //����ֵ
            int iReturn = 0;
            //���崦����
            string recipeNO = string.Empty;

            //������շѣ���÷�Ʊ��Ϣ
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//�շ�
            {
                #region �շ�����

                /*
                 * 1��
                 * ��Ʊ�Ѿ���Ԥ������������,ֱ�Ӳ���Ϳ�����.
                 *
                 * 2��
                 * ������㣬Ӧ�������Żݽ������ͽ��
                 * ECO(�Ż�)��PY(�ײ��Ż�)
                 * ACD(�ʻ�����)��PD(�ײ�����)�� 
                 * 
                 * */


                #region �ֱ�ͳ���ײ����ײ�����Ŀ�ܽ��ۿ۽��Żݽ��

                //���ý�����ʱ�򲻻���������ͽ��ײ����ͺ����Ͷ�����ȷ��֧����ʽ֮��Ų�����

                ////{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                //�ײ�����Ŀ���ͳ��
                decimal totPackage = 0.0m;
                decimal totPackageReal = 0.0m;
                decimal totPackageGift = 0.0m;   
                decimal totPackageEco = 0.0m;

                //�ײ�����Ŀ���ͳ��
                decimal totNormal = 0.0m;
                decimal totNormalReal = 0.0m;
                decimal totNormalGift = 0.0m;
                decimal totNormalEco = 0.0m;

                ArrayList packageItems = new ArrayList();
                ArrayList normalItems = new ArrayList();

                foreach (FeeItemList feeItemList in feeDetails)
                {
                    //��ʼ�����ͽ��˲���һ��Ҫ��
                    feeItemList.FT.DonateCost = 0.0m;

                    if (feeItemList.IsPackage == "1")
                    {
                        totPackage += feeItemList.FT.OwnCost;
                        totPackageReal += (feeItemList.FT.OwnCost - feeItemList.FT.RebateCost);
                        totPackageGift += feeItemList.FT.DonateCost;
                        totPackageEco += feeItemList.FT.RebateCost;
                    }
                    else
                    {
                        totNormal += feeItemList.FT.OwnCost;
                        totNormalReal += (feeItemList.FT.OwnCost - feeItemList.FT.RebateCost);
                        totNormalGift += feeItemList.FT.DonateCost;
                        totNormalEco += feeItemList.FT.RebateCost + feeItemList.FT.DiscountCardEco;
                    }
                }

                decimal packageRealPay = 0.0m;
                decimal packageGiftPay = 0.0m;
                decimal packageEcoPay = 0.0m;

                decimal normalRealPay = 0.0m;
                decimal normalGiftPay = 0.0m;
                decimal normalEcoPay = 0.0m;

                foreach (BalancePay pm in payModes)
                {
                    FS.FrameWork.Models.NeuObject objPay = this.PayModesHelper.GetObjectFromID(pm.PayType.ID);
                    if (objPay != null)
                    {

                        switch ((objPay as Models.Base.Const).ID)
                        {
                            case "PR": //�ײ�ʵ��
                                packageRealPay += pm.FT.TotCost;
                                break;
                            case "PD": //�ײ�����
                                packageGiftPay += pm.FT.TotCost;
                                break;
                            case "PY": //�ײ��Ż�
                                packageEcoPay += pm.FT.TotCost;
                                break;
                            case "DC": //����
                                normalGiftPay += pm.FT.TotCost;
                                break;
                            case "CO": //���ֵ����ͽ���
                                normalGiftPay += pm.FT.TotCost;
                                break;
                            case "RC": //�Ż�
                                normalEcoPay += pm.FT.TotCost;
                                break;
                            default:  //Ĭ�϶���ʵ��
                                normalRealPay += pm.FT.TotCost;
                                break;
                        }
                    }
                }

                //����ײ������۽������ײ�����Ŀ���
                if (totPackageReal + totPackageGift < packageRealPay + packageGiftPay + packageEcoPay)
                {
                    MessageBox.Show("�ײ���Ŀ��������Ҫ�ֿ۵Ľ�");
                    return false;
                }

                decimal packageweight = totPackageReal + totPackageGift - packageRealPay - packageGiftPay - packageEcoPay;
                decimal normalweight = totNormalReal;

                //normalGiftPay
                //normalRealPay
                //������ײ�����Ŀ�����ͽ��
                //decimal normalRealPayforPackage = 0.0m;
                decimal normalGiftPayforPackage = 0.0m;

                //������ײ�����Ŀ�����ͽ��
                //decimal normalRealPayforNormal = 0.0m;
                decimal normalGiftPayforNormal = 0.0m;

                normalGiftPayforPackage = Math.Floor((packageweight * normalGiftPay * 100) / ((packageweight + normalweight) == 0 ? 1 : (packageweight + normalweight))) / 100;
                normalGiftPayforNormal = normalGiftPay - normalGiftPayforPackage;
                //�������������ײ�����Ŀֻ�����ͽ������ͽ��ܴ���Ӧ�ս���ȡ���ᵼ���������������
                if (normalGiftPayforNormal > totNormalReal)
                {
                    normalGiftPayforNormal = totNormal - totNormalEco;
                    normalGiftPayforPackage = normalGiftPay - normalGiftPayforNormal;
                }
                #endregion 

                #region �ײ�����Ŀ���������Ż�

                decimal packageGiftCost = normalGiftPayforPackage + packageGiftPay;
                decimal packageGiftCostForCount = normalGiftPayforPackage + packageGiftPay;

                decimal packageEcoCost = packageEcoPay;
                decimal packageEcoCostForCount = packageEcoPay;

                decimal normalGiftCost = normalGiftPayforNormal;
                decimal normalGiftCostForCount = normalGiftPayforNormal;

                bool adjustFlag = false;

                foreach (FeeItemList feeItemList in feeDetails)
                {
                    adjustFlag = false;
                    if (feeItemList.IsPackage == "1")
                    {
                        //���ͺ��Żݶ��������ˣ�������ѭ��
                        if (packageGiftCost == 0 && packageEcoCost == 0)
                        {
                            continue;
                        }

                        decimal giftweight = Math.Ceiling((feeItemList.FT.OwnCost - feeItemList.FT.RebateCost) * packageGiftCostForCount * 100 / (totPackageReal == 0?1:totPackageReal))/100;
                        if (giftweight > feeItemList.FT.OwnCost - feeItemList.FT.RebateCost)
                        {
                            giftweight = feeItemList.FT.OwnCost - feeItemList.FT.RebateCost;
                        }

                        if (giftweight > packageGiftCost)
                        {
                            giftweight = packageGiftCost;
                            adjustFlag = true;
                        }

                        packageGiftCost -= giftweight;
                        feeItemList.FT.DonateCost = giftweight;

                        decimal ecoweight = Math.Ceiling((feeItemList.FT.OwnCost - feeItemList.FT.RebateCost) * packageEcoCostForCount * 100 / (totPackageReal == 0 ? 1 : totPackageReal)) / 100;

                        //{0A673BE8-A0B0-4239-AB82-039620DFFC89}
                        //{076C5EC2-2657-43ad-AEAB-2D7C8726B387}
                        //���ͽ��ÿ������ȡ�����������ͽ���п��ܻ���1��Ǯ
                        //�����ڼ����Żݽ���ʱ���Żݽ����ٷ��䣬��������Żݽ��û�����걨��
                        //�������һ����ǣ������Ŀ�����ͽ���Ǳ��ٷ����˵Ļ����˴��Żݽ������Ϊ
                        //�ܻ��Ѽ�ԭ���Żݼ�ȥ���ͽ��
                        //��ʹ �ĳ����� ���ִ���packageEcoCost��Ҳ���ں�����ж��б�����
                        //�������������������󼸸���Ŀ
                        if (adjustFlag)
                        {
                            ecoweight = packageEcoCost;
                        }

                        if (ecoweight > feeItemList.FT.OwnCost - feeItemList.FT.DonateCost - feeItemList.FT.RebateCost)
                        {
                            ecoweight = feeItemList.FT.OwnCost - feeItemList.FT.DonateCost - feeItemList.FT.RebateCost;
                        }

                        if (ecoweight > packageEcoCost)
                        {
                            ecoweight = packageEcoCost;
                        }

                        packageEcoCost -= ecoweight;
                        feeItemList.FT.RebateCost += ecoweight;
                        feeItemList.FT.PackageEco = ecoweight;
                    }
                    else
                    {
                        //�ײ�����Ŀֻ��Ҫ�������ͽ��Żݽ���Ѿ��ڴ��۵�ʱ�������
                        if (normalGiftCost == 0)
                        {
                            continue;
                        }
                        decimal giftweight = Math.Ceiling((feeItemList.FT.OwnCost - feeItemList.FT.RebateCost) * normalGiftPayforNormal * 100 / (totNormalReal == 0 ? 1 : totNormalReal)) / 100;
                        if (giftweight > feeItemList.FT.OwnCost - feeItemList.FT.RebateCost)
                        {
                            giftweight = feeItemList.FT.OwnCost - feeItemList.FT.RebateCost;
                        }

                        if (giftweight > normalGiftCost)
                        {
                            giftweight = normalGiftCost;
                        }

                        normalGiftCost -= giftweight;
                        feeItemList.FT.DonateCost = giftweight;
                    }
                }

                if (packageEcoCost > 0 || packageGiftCost > 0 || normalGiftCost > 0)
                {
                    MessageBox.Show("���ͽ����Żݽ�����ʧ�ܣ�");
                    return false;
                }

                #endregion 

                #region ���Ͼɷ���
                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                #region �Żݽ������ͽ���

                ////�ײ���ϸ
                //Dictionary<string, FeeItemList> dictPackDetail = new Dictionary<string, FeeItemList>();

                //decimal totCost = 0;          //�ܽ��

                //decimal feeTotEcoCost = 0;      //������ϸ�Żݽ��(��Ҫ����������Ŀ���Żݽ��)
                //decimal feeTotDonateCost = 0;   //������ϸ���ͽ��(��Ҫ����������Ŀ�����ͽ��)

                //decimal balanceTotEcoCost = 0;     //��Ʊ�Żݽ��(��Ҫ����������Ŀԭ�����Żݽ��)
                //decimal balanceTotDonateCost = 0;  //��Ʊ���ͽ��(��Ҫ����������Ŀԭ�����Żݽ��)

                //foreach (BalancePay pm in payModes)
                //{
                //    FS.FrameWork.Models.NeuObject objPay = this.PayModesHelper.GetObjectFromID(pm.PayType.ID);
                //    if (objPay != null)
                //    {
                //        //�ܽ��
                //        totCost += pm.FT.TotCost;

                //        //�Żݽ��
                //        if ((objPay as Models.Base.Const).UserCode == "ECO" || (objPay as Models.Base.Const).UserCode == "PY")
                //        {
                //            feeTotEcoCost += pm.FT.TotCost;
                //        }

                //        //���ͽ��
                //        if ((objPay as Models.Base.Const).UserCode == "ACD" || (objPay as Models.Base.Const).UserCode == "PD")
                //        {
                //            feeTotDonateCost += pm.FT.TotCost;
                //        }

                //        #region �ײ���ϸ

                //        if (pm.UsualObject != null && (pm.UsualObject as List<PackageDetail>) != null &&
                //            (pm.UsualObject as List<PackageDetail>).Count > 0)
                //        {
                //            foreach (PackageDetail pd in pm.UsualObject as List<PackageDetail>)
                //            {
                //                if (!dictPackDetail.ContainsKey(pd.Item.ID))
                //                {
                //                    FeeItemList fPd = new FeeItemList();

                //                    fPd.Item.ID = pd.Item.ID;
                //                    fPd.FT.OwnCost = pd.Real_Cost + pd.Etc_cost + pd.Gift_cost;
                //                    fPd.FT.RebateCost = pd.Etc_cost;
                //                    fPd.FT.DonateCost = pd.Gift_cost;

                //                    dictPackDetail.Add(fPd.Item.ID, fPd);
                //                }
                //                else
                //                {
                //                    FeeItemList fPd = dictPackDetail[pd.Item.ID];
                //                    fPd.FT.OwnCost += pd.Real_Cost + pd.Etc_cost + pd.Gift_cost;
                //                    fPd.FT.RebateCost += pd.Etc_cost;
                //                    fPd.FT.DonateCost += pd.Gift_cost;
                //                }
                //            }
                //        }

                //        #endregion
                //    }
                //}
                //balanceTotEcoCost = feeTotEcoCost;
                //balanceTotDonateCost = feeTotDonateCost;

                ////1����Ʊʵ���Żݽ��(��Ҫ����������Ŀԭ�����Żݽ��)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    //�Żݽ��
                //    balanceTotEcoCost -= f.FT.RebateCost;
                //    //���ͽ��
                //    balanceTotDonateCost -= f.FT.DonateCost;

                //}

                ////�ײ���ϸƥ��
                ////(�߼���ҵ�����ϣ��˴���Ӧ��)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    if (dictPackDetail.ContainsKey(f.Item.ID))
                //    {
                //        FeeItemList pd = dictPackDetail[f.Item.ID];

                //        if ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) > pd.FT.OwnCost)
                //        {
                //            //�Żݽ��
                //            f.FT.RebateCost += pd.FT.RebateCost;
                //            //���ͽ��
                //            f.FT.DonateCost += pd.FT.DonateCost;
                //        }
                //        else
                //        {
                //            //�Żݽ��
                //            f.FT.RebateCost += Math.Round(pd.FT.RebateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (pd.FT.OwnCost != 0 ? pd.FT.OwnCost : 1)), 2);
                //            //���ͽ��
                //            f.FT.DonateCost += Math.Round(pd.FT.DonateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (pd.FT.OwnCost != 0 ? pd.FT.OwnCost : 1)), 2);
                //        }
                //    }
                //}

                ////2��������ϸ�Żݽ������ͽ��(��Ҫ����������Ŀ���Żݽ������ͽ��)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    //�Żݽ��
                //    feeTotEcoCost -= f.FT.RebateCost;
                //    //���ͽ��
                //    feeTotDonateCost -= f.FT.DonateCost;
                //}
                #endregion
                #endregion 

                #region ��÷�Ʊ����,���ŷ�Ʊ��Ʊ�Ų�ͬ

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err;

                    return false;
                }
                //���������ʾ���
                /////GetSpDisplayValue(myCtrl, t);
                //��һ����Ʊ��
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region ���뷢Ʊ��ϸ��

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //�ܷ�Ʊ����
                            if (balanceList.Memo == "5")
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(((Balance)balanceList.BalanceBase).CombNO))
                            {
                                ((Balance)balanceList.BalanceBase).CombNO = invoiceCombNO;
                            }
                            balanceList.BalanceBase.BalanceOper.ID = operID;
                            balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                            balanceList.BalanceBase.IsDayBalanced = false;
                            balanceList.BalanceBase.CancelType = CancelTypes.Valid;
                            balanceList.ID = balanceList.ID.PadLeft(12, '0');

                            //���뷢Ʊ��ϸ�� fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "���뷢Ʊ��ϸ����!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region Э������
                ArrayList noSplitDrugList = new ArrayList();
                if (isSplitNostrum)
                {

                    if (SplitNostrumDetail(r, ref feeDetails, ref noSplitDrugList, ref errText) < 0)
                    {
                        return false;
                    }
                }

                #endregion

                #region ҩƷ��Ϣ�б�,���ɴ�����

                ArrayList drugLists = new ArrayList();

                //����Էַ����ã���Ҫ���׸Ķ�
                r.User02 = "�շ�";

                //�������ɴ�����,������д�����,��ϸ�����¸�ֵ.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region ���������ϸ

                #region ���Ͼ�����
                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                //�Żݽ����ۼ�ֵ
                //decimal gatherEcoCost = 0;
                ////���ͽ����ۼ�ֵ
                //decimal gatherDonateCost = 0;
                //int feeIndex = 0;
                #endregion 
                foreach (FeeItemList f in feeDetails)
                {
                    //��֤����
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }
                    if (f.Item.SpecialFlag1 == "1"||f.Item.SpecialFlag1=="2") f.NoBackQty = 0;
                    //���û�д�����,���¸�ֵ
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "��ô����ų���!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq �ж��Ƿ����з�Ʊ����ţ�û����ֵ

                    //{1A5CC61F-01F9-4dee-A6A8-580200C10EB4}
                    if (string.IsNullOrEmpty(f.InvoiceCombNO) || f.InvoiceCombNO == "NULL")
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }

                    #endregion
                    
                    #region 2007-8-28 liuq �ж��Ƿ����з�Ʊ�ţ�û�г�ʼ��Ϊ12��0

                    if (string.IsNullOrEmpty(f.Invoice.ID))
                    {
                        f.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    }

                    #endregion

                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    f.TransType = TransTypes.Positive;
                    f.Patient.PID.CardNO = r.PID.CardNO;
                   
                    #region ���Ͼ�����
                    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //�Żݽ����ͽ��
                    //feeIndex++;
                    //if (feeIndex == feeDetails.Count)
                    //{
                    //    //�Żݽ��
                    //    f.FT.RebateCost += feeTotEcoCost - gatherEcoCost;

                    //    //���ͽ��
                    //    f.FT.DonateCost += feeTotDonateCost - gatherDonateCost;
                    //}
                    //else
                    //{
                    //    //�Żݽ��
                    //    f.FT.RebateCost += Math.Round(feeTotEcoCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);
                    //    gatherEcoCost += Math.Round(feeTotEcoCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);

                    //    //���ͽ��
                    //    f.FT.DonateCost += Math.Round(feeTotDonateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);
                    //    gatherDonateCost += Math.Round(feeTotDonateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);
                    //}
                    #endregion

                    //f.Patient = r.Clone();
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                    if (((Register)f.Patient).DoctorInfo.Templet.Dept.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Dept.ID == string.Empty)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept.Clone();
                    }
                    if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Doct = r.DoctorInfo.Templet.Doct.Clone();
                    }
                    if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                    {
                        f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Doct.User01;
                    }

                    if (f.ChargeOper.OperTime == DateTime.MinValue)
                    {
                        f.ChargeOper.OperTime = feeTime;
                    }
                    if (f.ChargeOper.ID == null || f.ChargeOper.ID == string.Empty)
                    {
                        f.ChargeOper.ID = operID;
                    }
                    if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        if (r.SeeDoct.ID != null && r.SeeDoct.ID != "")
                        {
                            ((Register)f.Patient).DoctorInfo.Templet.Doct.ID = r.SeeDoct.ID;
                        }
                        else
                        {
                            errText = "��ѡ��ҽ��";
                            return false;
                        }
                    }

                    if (f.RecipeOper.ID == null || f.RecipeOper.ID == string.Empty)
                    {
                        f.RecipeOper.ID = ((Register)f.Patient).DoctorInfo.Templet.Doct.ID;
                    }

                    f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.ExamineFlag = r.ChkKind;

                    //�������Ϊ������죬��ô������Ŀ�������ն���ˡ�
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //�����Ŀ��ˮ��Ϊ�գ�˵��û�о����������̣���ô�����ն������Ϣ��
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";
                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//�������������ĿΪ��Ҫ�ն������Ŀ������ն������Ϣ��
                    {
                        if (!f.IsConfirmed)
                        {
                            //if (f.Item.IsNeedConfirm)
                            if (f.Item.ItemType == EnumItemType.UnDrug)
                            {
                                if (f.Item.NeedConfirm == EnumNeedConfirm.Outpatient || f.Item.NeedConfirm == EnumNeedConfirm.All || f.Item.SpecialFlag2 == "1")
                                {
                                    if (f.Item.SpecialFlag2 == "0")
                                    {
                                        f.IsConfirmed = true;
                                    }
                                    else
                                    {
                                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                                        {
                                            f.Order.ID = orderManager.GetNewOrderID();
                                        }
                                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                                        {
                                            errText = "���ҽ����ˮ�ų���!";
                                            return false;
                                        }

                                        Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);
                                        if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                        {
                                            errText = "�����ն�����ȷ�ϱ�ʧ��!" + ConfirmIntegrate.Err;
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //û�и�ֵҽ����ˮ��,��ֵ�µ�ҽ����ˮ��
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "���ҽ����ˮ�ų���!";
                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//�����������շѱ��
                    {
                        iReturn = ExamiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "��������շѱ��ʧ��!" + ExamiIntegrate.Err;
                            return false;
                        }
                    }

                    //���ɾ�����۱����е������Ŀ����Ŀ��Ϣ,������ϸ.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "ɾ������ʧ��!" + outpatientManager.Err;
                            return false;
                        }
                        //��֪��˭�޸ĵģ�ż��ɾ�����׷���ʧ��...
                        //ǰ���Ѿ�������ҽ����id������õ�User03���˴���ɾһ��  houwb
                        else if (iReturn == 0)
                        {
                            iReturn = outpatientManager.DeletePackageByMoOrder(f.User03);
                            if (iReturn == -1)
                            {
                                errText = "ɾ������ʧ��!" + outpatientManager.Err;
                                return false;
                            }
                        }
                    }
                    //FeeItemList feeTemp = new FeeItemList();
                    //feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    //{39B2599D-2E90-4b3d-A027-4708A70E45C3}
                    int chargeItemCount = outpatientManager.GetChargeItemCount(f.RecipeNO, f.SequenceNO);
                    //6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A
                   
                    
                    f.Patient.Pact = r.Pact;
                    if (chargeItemCount == -1)
                    {
                        errText = "��ѯ��Ŀ��Ϣʧ�ܣ�";
                        return false;
                    }

                    if (chargeItemCount == 0)//˵�������� //if(feeTemp == null)
                    {
                        if (f.FTSource != "0" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "�����Ѿ�����������Աɾ��,��ˢ�º����շ�!";

                            return false;
                        }
                        if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                        {
                            //f.RecipeOper.Dept.ID = f.DoctDeptInfo.ID;
                            if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                            {
                                if (r != null) f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Dept.ID;
                                else
                                    f.RecipeOper.Dept.ID = (feeDetails[0] as FeeItemList).RecipeOper.Dept.ID;
                            }
                        }
                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���������ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                        {
                            f.RecipeOper.Dept.ID = f.DoctDeptInfo.ID;
                            if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                            {
                                if (r != null) f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Dept.ID;
                                else
                                    f.RecipeOper.Dept.ID = (feeDetails[0] as FeeItemList).RecipeOper.Dept.ID;
                            }
                        }
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���·�����ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region ��дҽ����Ϣ

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn <= 0 && !f.Item.IsMaterial && f.Item.ItemType == EnumItemType.Drug)
                        {
                            errText = "û�и��µ�ҽ����Ϣ������ҽ��ȷ���Ƿ��Ѿ�ɾ����ҽ��:" + f.Item.Name + ",������ˢ�������û����շ���Ϣ." + orderOutpatientManager.Err;

                            return false;
                        }

                        bool isCanModifyUnDrug = false;
                        isCanModifyUnDrug = this.controlParamIntegrate.GetControlParam<bool>("MZ9934", true, false);

                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Order.OutPatient.Order order = orderOutpatientManager.QueryOneOrder(f.Patient.ID, f.Order.ID, f.RecipeNO);

                            if (order != null && order.ID.Length > 0)
                            {
                                if (f.FeePack == "1")
                                {
                                    if (order.Qty * order.Item.PackQty != f.Item.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                            }
                        }
                        else if (!isCanModifyUnDrug)
                        {

                            FS.HISFC.Models.Order.OutPatient.Order order = orderOutpatientManager.QueryOneOrder(f.Patient.ID, f.Order.ID, f.RecipeNO);

                            if (order != null && order.ID.Length > 0)
                            {
                                //����Ǹ�����Ŀ
                                if (!string.IsNullOrEmpty(f.UndrugComb.ID))
                                {
                                    //ȡ������Ŀά������ϸ����
                                    FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo = undrugPackAgeMgr.GetUndrugComb(f.UndrugComb.ID, f.Item.ID);
                                    if (undrugCombo == null)
                                    {
                                        errText = "��ȡ������Ŀ" + f.UndrugComb.ID + "�ķ�ҩƷ��Ŀ��" + f.Item.ID + "ʧ�ܣ�ԭ��" + itemManager.Err;
                                        return false;
                                    }

                                    if (order.Qty != f.Item.Qty / undrugCombo.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "�շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region ���뷢ҩ�����б�

                    //�����ҩƷ,����û�б�ȷ�Ϲ�,���Ҳ���Ҫ�ն�ȷ��,��ô���뷢ҩ�����б�.
                    //if (f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed && f.Item.ID != "999")
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugLists.Add(f);
                            }
                        }
                    }
                    #endregion

                    #region ����ҽ��ԤԼ��

                    //��Ҫҽ��ԤԼ,�����ն�ԤԼ��Ϣ.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "����ҽ��ԤԼ��Ϣ����!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }
                    #endregion
                }

                #endregion

                #region �����������շѱ��

                if (r.ChkKind == "2")//�������
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = ExamiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "��������շѱ��ʧ��!" + ExamiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region ��ҩ������Ϣ

                string drugSendInfo = null;

                if (isSplitNostrum)
                {
                    drugLists.Clear();
                    foreach (FeeItemList item in noSplitDrugList)
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            if (item.Order.ID == f.Order.ID)
                            {
                                item.RecipeNO = f.RecipeNO;
                                item.FeeOper = f.FeeOper;
                                break;
                            }
                        }
                    }
                    drugLists.AddRange(noSplitDrugList);
                }
                FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
                int isPartSend = 0;
                try
                {
                    isPartSend = FS.FrameWork.Function.NConvert.ToInt32(ctrlManager.QueryControlerInfo("HNGYZL", false));
                }
                catch
                {
                    isPartSend = 0;
                }
                //���뷢ҩ������Ϣ,���ط�ҩ����,��ʾ�ڷ�Ʊ��
                if (isPartSend == 1)
                {
                    iReturn = PharmarcyManager.ApplyOut(r, drugLists, feeTime, string.Empty, false, out drugSendInfo);
                }
                else
                {
                    iReturn = PharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                }
                if (iReturn == -1)
                {
                    errText = "����ҩƷ��ϸʧ��!" + PharmarcyManager.Err;

                    return false;
                }

                //'�����ҩƷ,��ô���÷�Ʊ����ʾ��ҩ������Ϣ.
                if (drugLists.Count > 0)
                {
                    //{02F6E9D7-E311-49a4-8FE4-BF2AC88B889B}���ε�С�汾���룬���ú��İ汾�Ĵ���
                    //foreach (Balance invoice in invoices)
                    //{
                    //    invoice.DrugWindowsNO = drugSendInfo;
                    //}
                    foreach (Balance invoice in invoices)
                    {
                        string tempInvoiceNo = string.Empty;
                        for (int i = 0; i < drugLists.Count; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList oneFeeItem = new FeeItemList();
                            oneFeeItem = drugLists[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                            //if (oneFeeItem.Item.IsPharmacy)
                            if (oneFeeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                tempInvoiceNo = oneFeeItem.Invoice.ID;
                            }
                            if (invoice.Invoice.ID == tempInvoiceNo)
                            {
                                invoice.DrugWindowsNO = drugSendInfo;
                            }
                        }
                    }
                }

                #endregion

                #region ���뷢Ʊ����

                //�Żݽ��
                decimal gatherEcoCost = 0;
                //���ͽ��
                decimal gatherDonateCost = 0;
                int invoIndex = 0;

                decimal totRealCost = totNormalReal + totPackageReal;
                decimal balanceTotEcoCost = packageEcoPay;
                decimal balanceTotGiftCost = packageGiftPay + normalGiftPay;
                decimal balanceTotEcoCostForCount = packageEcoPay;
                decimal balanceTotGiftCostForCount = packageGiftPay + normalGiftPay;
                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }
                    if (string.IsNullOrEmpty(balance.CombNO))
                    {
                        balance.CombNO = invoiceCombNO;
                    }
                    balance.BalanceOper.ID = operID;
                    balance.BalanceOper.OperTime = feeTime;
                    balance.Patient.Pact = r.Pact;

                    #region ���Ͼɷ���
                    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //�����˻��������Żݽ��
                    //try
                    //{
                    //    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //    invoIndex++;
                    //    if (invoIndex == invoices.Count)
                    //    {
                    //        //�Żݽ��
                    //        balance.FT.RebateCost += balanceTotEcoCost - gatherEcoCost;

                    //        //���ͽ��
                    //        balance.FT.DonateCost += balanceTotDonateCost - gatherDonateCost;
                    //    }
                    //    else
                    //    {
                    //        //�Żݽ��
                    //        balance.FT.RebateCost += Math.Round(balanceTotEcoCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //        gatherEcoCost += Math.Round(balanceTotEcoCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);

                    //        //���ͽ��
                    //        balance.FT.DonateCost += Math.Round(balanceTotDonateCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //        gatherDonateCost += Math.Round(balanceTotDonateCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //    }
                    //}
                    //catch (Exception ex) { }
                    #endregion

                    #region ���䷢Ʊ���ͽ�����Żݽ��
                    try
                    {
                        //���ͺ��Żݶ��������ˣ�������ѭ��
                        if (balanceTotEcoCost != 0 || balanceTotGiftCost != 0)
                        {
                            decimal giftweight = Math.Ceiling((balance.FT.OwnCost - balance.FT.RebateCost) * balanceTotGiftCostForCount * 100 / (totRealCost == 0?1: totRealCost)) / 100;

                            if (giftweight > balance.FT.OwnCost - balance.FT.RebateCost)
                            {
                                giftweight = balance.FT.OwnCost - balance.FT.RebateCost;
                            }

                            if (giftweight > balanceTotGiftCost)
                            {
                                giftweight = balanceTotGiftCost;
                            }

                            balanceTotGiftCost -= giftweight;
                            balance.FT.DonateCost = giftweight;

                            decimal ecoweight = Math.Ceiling((balance.FT.OwnCost - balance.FT.RebateCost) * balanceTotEcoCostForCount * 100 / (totRealCost == 0?1: totRealCost)) / 100;
                            if (ecoweight > balance.FT.OwnCost - balance.FT.DonateCost - balance.FT.RebateCost)
                            {
                                ecoweight = balance.FT.OwnCost - balance.FT.DonateCost - balance.FT.RebateCost;
                            }

                            if (ecoweight > balanceTotEcoCost)
                            {
                                ecoweight = balanceTotEcoCost;
                            }

                            balanceTotEcoCost -= ecoweight;
                            balance.FT.RebateCost += ecoweight;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    #endregion 

                    //����־
                    string tempExamineFlag = null;
                    //�������־ 0 ��ͨ���� 1 ������� 2 �������
                    //���û�и�ֵ,Ĭ��Ϊ��ͨ����
                    if (r.ChkKind.Length > 0)
                    {
                        tempExamineFlag = r.ChkKind;
                    }
                    else
                    {
                        tempExamineFlag = "0";
                    }
                    balance.ExamineFlag = tempExamineFlag;
                    balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                    //=====ȥ��CanceledInvoiceNO=string.Empty ·־��================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//�޶����
                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    #region ���ڴ��ж��Ƿ���ڷ�Ʊ�ţ��������
                    //if (invoiceType == "0")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("�Ѿ����ڷ�Ʊ��Ϊ: " + balance.Invoice.ID +
                    //            " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    //else if (invoiceType == "1")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("�Ѿ�����Ʊ�ݺ�Ϊ: " + balance.PrintedInvoiceNO +
                    //            " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    #endregion
                    //���뷢Ʊ����fin_opb_invoice
                    //���ø�������
                    Register rold = registerManager.GetByClinic(r.ID);

                    //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    //�����ֱ���շѣ���ʱ�Һż�¼��û����Һű�
                    if (!string.IsNullOrEmpty(rold.Name))
                    {
                        balance.Patient.Name = rold.Name;
                        r.Name = rold.Name;
                    }
                    else
                    {
                        balance.Patient.Name = r.Name;
                    }

                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "�����������!" + outpatientManager.Err;

                        return false;
                    }
                }

                if (balanceTotEcoCost > 0 || balanceTotGiftCost > 0)
                {
                    MessageBox.Show("���䷢Ʊ���ͽ�����Żݽ�����");
                    return false;
                }
                #endregion

                #region ��Ʊ���ߺţ����Ʊ����һ������

                if (!isTempInvoice)//��ʱ��Ʊ���벻����һ������
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;

                    if (invoiceNo.Length >= 12 && invoiceNo.StartsWith("9"))
                    {
                        // Ϊ��ʱ��Ʊ�����ʻ����п�������ʱ��Ʊ
                    }
                    else
                    {
                        int invoicesCount = invoices.Count;
                        foreach (Balance invoiceObj in invoices)
                        {
                            if (invoiceObj.Memo == "5")
                            {
                                invoicesCount = invoices.Count - 1;
                                continue;
                            }
                        }
                        if (this.UseInvoiceNO(oper, invoiceStytle, invoiceType, invoicesCount, ref invoiceNo, ref realInvoiceNo, ref errText) < 0)
                        {
                            return false;
                        }

                        foreach (Balance invoiceObj in invoices)
                        {
                            if (invoiceObj.Memo == "5")
                            {
                                continue;
                            }
                            if (this.InsertInvoiceExtend(invoiceObj.Invoice.ID, invoiceType, invoiceObj.PrintedInvoiceNO, "00") < 1)
                            {//��Ʊͷ��ʱ�ȱ���00
                                errText = this.invoiceServiceManager.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion



                #region ����֧����ʽ��Ϣ

                int payModeSeq = 1;
                string payInvoiceNo = string.Empty;

                //��Ա֧��+��Ա����
                Dictionary<string, List<BalancePay>> dictAcc = new Dictionary<string, List<BalancePay>>();

                //�ײ�֧������
                List<PackageDetail> packDeteails = new List<PackageDetail>();

                //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
                //FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("JFZFFS", "1");

                //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                //FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

                //decimal cashCouponAmount = 0.0m;

                FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
                foreach (BalancePay p in payModes)
                {
                    p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    p.TransType = TransTypes.Positive;
                    p.Squence = payModeSeq.ToString();
                    p.IsDayBalanced = false;
                    p.IsAuditing = false;
                    p.IsChecked = false;
                    p.InputOper.ID = operID;
                    p.InputOper.OperTime = feeTime;
                    if (string.IsNullOrEmpty(p.InvoiceCombNO))
                    {
                        //p.InvoiceCombNO = mainInvoiceCombNO;
                        if (string.IsNullOrEmpty(mainInvoiceCombNO))
                        {
                            p.InvoiceCombNO = invoiceCombNO;
                        }
                        else
                        {
                            p.InvoiceCombNO = mainInvoiceCombNO;
                        }
                    }
                    p.CancelType = CancelTypes.Valid;

                    payModeSeq++;

                    payInvoiceNo = p.Invoice.ID;

                    //��Ա֧��+��Ա��������
                    if ((!string.IsNullOrEmpty(p.AccountNo) && !string.IsNullOrEmpty(p.AccountTypeCode)) ||
                        p.PayType.ID == "YS" || p.PayType.ID == "DC")
                    {
                        string key = p.AccountNo + "-" + p.AccountTypeCode;
                        if (dictAcc.ContainsKey(key))
                        {
                            List<BalancePay> bpList = dictAcc[key];
                            bpList.Add(p);
                        }
                        else
                        {
                            List<BalancePay> bpList = new List<BalancePay>();
                            bpList.Add(p);
                            dictAcc.Add(key, bpList);
                        }
                    }

                    //�ײ�֧������
                    if (p.UsualObject != null && (p.UsualObject as List<PackageDetail>) != null &&
                        (p.UsualObject as List<PackageDetail>).Count > 0)
                    {
                        //{DD31280F-7321-42BB-B150-4C63018ED85F} ��֧������עд��
                        packDeteails = p.UsualObject as List<PackageDetail>;
                        List<PackageDetail> listDeteails = packDeteails.Where(t => t.CardNO != r.PID.CardNO).ToList();
                        if (listDeteails != null && listDeteails.Count > 0)
                        {
                            p.Memo = listDeteails[0].CardNO + "��" + r.PID.CardNO + "����,�ײ�֧���ܶ�:" + listDeteails.Sum(t => t.Detail_Cost) + ",�ײ�ʵ��:"+listDeteails.Sum(t=>t.Real_Cost) ;
                        }
                        
                    }

                    iReturn = outpatientManager.InsertBalancePay(p);
                    if (iReturn == -1)
                    {
                        errText = "����֧����ʽ�����!" + outpatientManager.Err;

                        return false;
                    }

                    //������ͣ��
                    //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                    //�жϸ�֧����ʽ�Ƿ�������
                    //if (obj.Name.Contains(p.PayType.ID.ToString()))
                    //{
                    //    if (accountPay.UpdateCoupon(r.PID.CardNO, p.FT.TotCost, payInvoiceNo) <= 0)
                    //    {
                    //        errText = "������ֳ���!" + accountPay.Err;
                    //        return false;
                    //    }
                    //}

                    //if (cashCouponPayMode.Name.Contains(p.PayType.ID.ToString()))
                    //{
                    //    cashCouponAmount += p.FT.TotCost;
                    //}
                }

                //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                //if (cashCouponAmount > 0 || cashCouponAmount < 0)
                //{
                //    string errInfo = string.Empty;
                //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
                //    if (cashCouponPrc.CashCouponSave("MZSF", r.PID.CardNO, payInvoiceNo, cashCouponAmount, ref errInfo) <= 0)
                //    {
                //        errText = "�����ֽ������ֳ���!" + errInfo;
                //        return false;
                //    }

                //}

                #region �˻��۷�

                if (dictAcc != null && dictAcc.Count > 0)
                {
                    //���㻼��
                    FS.HISFC.Models.RADT.PatientInfo selfPatient = accountManager.GetPatientInfoByCardNO(r.PID.CardNO);
                    if (selfPatient == null || string.IsNullOrEmpty(selfPatient.PID.CardNO))
                    {
                        errText = "��ѯ���߻�����Ϣʧ��!";
                        return false;
                    }

                    //��Ա����
                    FS.HISFC.Models.RADT.PatientInfo empPatient = new FS.HISFC.Models.RADT.PatientInfo();

                    foreach (List<BalancePay> bpList in dictAcc.Values)
                    {
                        decimal baseCost = 0;                    //�����˻����
                        decimal donateCost = 0;                  //�����˻����

                        BalancePay bp = new BalancePay();
                        for (int k = 0; k < bpList.Count; k++)
                        {
                            bp = bpList[k];
                            if (bp.PayType.ID == "YS")
                            {
                                baseCost -= bp.FT.TotCost;
                            }
                            else if (bp.PayType.ID.ToString() == "DC")
                            {
                                donateCost -= bp.FT.TotCost;
                            }
                        }

                        string accountNo = bp.AccountNo;      //�˻�
                        string accountTypeCode = bp.AccountTypeCode;   //�˻�����
                        List<AccountDetail> accLists = accountManager.GetAccountDetail(accountNo, accountTypeCode, "1");
                        if (accLists == null || accLists.Count <= 0)
                        {
                            errText = "�����˻�ʧ��!";
                            return false;
                        }
                        AccountDetail detailAcc = accLists[0];
                        if (Math.Abs(baseCost) > detailAcc.BaseVacancy)
                        {
                            errText = string.Format("��Ա�˻��л����˻����㣡\r\n��ɷѣ�{0}Ԫ�������˻���{1}Ԫ", Math.Abs(baseCost).ToString("F2"), detailAcc.BaseVacancy.ToString("F2"));
                            return false;
                        }
                        if (Math.Abs(donateCost) > detailAcc.DonateVacancy)
                        {
                            errText = string.Format("��Ա�˻��������˻����㣡\r\n��ɷѣ�{0}Ԫ�������˻���{1}Ԫ", Math.Abs(donateCost).ToString("F2"), detailAcc.DonateVacancy.ToString("F2"));
                            return false;
                        }

                        if (bp.IsEmpPay)
                        {
                            empPatient = accountManager.GetPatientInfoByCardNO(detailAcc.CardNO);
                            if (empPatient == null || string.IsNullOrEmpty(empPatient.PID.CardNO))
                            {
                                errText = string.Format("������Ȩ���ߡ�{0}��������Ϣʧ��!", detailAcc.CardNO);
                                return false;
                            }
                        }
                        else
                        {
                            empPatient = selfPatient;
                        }

                        //��Ա�˻�����
                        int returnValue = accountPay.OutpatientPay(selfPatient, accountNo, accountTypeCode, baseCost, donateCost, payInvoiceNo, empPatient, PayWayTypes.C, 1);
                        if (returnValue < 0)
                        {
                            errText = "�˻��������!" + accountPay.Err;
                            return false;
                        }

                    }
                }

                #endregion

                #region �ײ�����

                if (packDeteails != null && packDeteails.Count > 0)
                {
                    ArrayList alPack = new ArrayList(packDeteails);
                    
                    //if (this.NewCostPackageDetail(alPack, payInvoiceNo,r, ref errText) < 0)
                    //{
                    //    errText = "�ײͽ���ʧ�ܣ�\r\n" + errText + this.Err;
                    //    return false;
                    //}
                    //��Ա����{351D714B-0153-483e-B1AB-697C5A9A9BAD}
                    FS.HISFC.Models.RADT.PatientInfo p = accountManager.GetPatientInfoByCardNO(r.PID.CardNO);
                    if (this.NewCostPackageDetailByType(alPack, payInvoiceNo, p, "MZ", r.ID, ref errText) < 0) 
                    {
                        errText = "�ײͽ���ʧ�ܣ�\r\n" + errText + this.Err;
                        return false;
                    }
                }

                #endregion

                #endregion

                #region ����Һż�¼�����¿�����

                string noRegRules = controlParamIntegrate.GetControlParam(Const.NO_REG_CARD_RULES, false, "9");
                
                //�������������շ�,��ô����Һ���Ϣ,����Ѿ������,��ô����.
                //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                if (r.PID.CardNO.Substring(0, 1) == noRegRules || r.User01 == "1")
                {
                    r.InputOper.OperTime = DateTime.Now;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;

                    #region ֻ���շѣ������ļ�����Ͳ���ʱ��{3C5D4918-96F0-4ba8-AC85-6DA86914465D}
                    r.HospitalFirstVisit = registerManager.IsFirstRegister("1", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.RootDeptFirstVisit = registerManager.IsFirstRegister("2", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.DoctFirstVist = registerManager.IsFirstRegister("4", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.IsFirst = registerManager.IsFirstRegister("3", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? false : true;
                    #endregion

                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                /*{067831BF-DDA5-4ac3-958A-4DD0BE5B085F}�շѷ�ӳ���շѴ�ѡ����շ�����Ӱ�컼������ĺ�ͬ��λ���־����ܹ��޸Ļ�����Ϣ�����Ȩ��ֻ�л��߻�����Ϣ�޸ģ���������ȥ��
                else
                {
                    if (registerManager.UpdatePatientInfoForNewClinicFee(r) <= 0)
                    {
                        errText = "���¹Һ���Ϣʧ��!" + registerManager.Err;
                        return false;
                    }

                    //if (registerManager.UpdateRegInfoForClinicFee(r) <= 0){//69C503A2-4C1C-44D4-82A3-174ABDAC34C1}�رո��¹Һű���Ϣ
                    //{
                    //    errText = "���¹Һ���Ϣʧ��!" + registerManager.Err;
                    //    return false;
                    //}
                }
                 * */

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "���¿����־����" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "���¿�����ҡ�ҽ������" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "���·����־����" + managerIntegrate.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else//����
            {
                #region ��������
                //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                string noRegRules = controlParamIntegrate.GetControlParam<string>(Const.NO_REG_CARD_RULES, false, "9");
                if (r.PID.CardNO.Substring(0, 1) == noRegRules || r.User01 == "1")
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = outpatientManager.Operator.ID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //�����۱�����Ϣ.
                bool returnValue = this.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }


            return true;
        }



        /// <summary>
        /// �����շѺ���
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFeeSaveFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {

            Terminal.Confirm ConfirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();

            if (this.trans != null)
            {
                ConfirmIntegrate.SetTrans(this.trans);
                bookingIntegrate.SetTrans(this.trans);
            }

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //����շѲ���Ա
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //����ֵ
            int iReturn = 0;
            //���崦����
            string recipeNO = string.Empty;

            //������շѣ���÷�Ʊ��Ϣ
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//�շ�
            {
                #region �շ�����
                //��Ʊ�Ѿ���Ԥ������������,ֱ�Ӳ���Ϳ�����.

                #region//��÷�Ʊ����,���ŷ�Ʊ��Ʊ�Ų�ͬ,����һ����Ʊ����,ͨ����Ʊ���к�,���Բ�ѯһ���շѵĶ��ŷ�Ʊ.

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err;

                    return false;
                }
                //���������ʾ���
                /////GetSpDisplayValue(myCtrl, t);
                //��һ����Ʊ��
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //���뷢Ʊ��ϸ��

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //�ܷ�Ʊ����
                            if (balanceList.Memo == "5")
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(((Balance)balanceList.BalanceBase).CombNO))
                            {
                                ((Balance)balanceList.BalanceBase).CombNO = invoiceCombNO;
                            }
                            balanceList.BalanceBase.BalanceOper.ID = operID;
                            balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                            balanceList.BalanceBase.IsDayBalanced = false;
                            balanceList.BalanceBase.CancelType = CancelTypes.Valid;
                            balanceList.ID = balanceList.ID.PadLeft(12, '0');

                            //���뷢Ʊ��ϸ�� fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "���뷢Ʊ��ϸ����!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region//ҩƷ��Ϣ�б�,���ɴ�����

                ArrayList drugLists = new ArrayList();

                //�������ɴ�����,������д�����,��ϸ�����¸�ֵ.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//���������ϸ

                foreach (FeeItemList f in feeDetails)
                {
                    //��֤����
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }

                    //���û�д�����,���¸�ֵ
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "��ô����ų���!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq �ж��Ƿ����з�Ʊ����ţ�û����ֵ
                    if (string.IsNullOrEmpty(f.InvoiceCombNO))
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq �ж��Ƿ����з�Ʊ�ţ�û�г�ʼ��Ϊ12��0
                    if (string.IsNullOrEmpty(f.Invoice.ID))
                    {
                        f.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    }
                    #endregion
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    f.TransType = TransTypes.Positive;
                    f.Patient.PID.CardNO = r.PID.CardNO;
                    //f.Patient = r.Clone();
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                    if (((Register)f.Patient).DoctorInfo.Templet.Dept.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Dept.ID == string.Empty)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept.Clone();
                    }
                    if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Doct = r.DoctorInfo.Templet.Doct.Clone();
                    }
                    if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                    {
                        f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Doct.User01;
                    }

                    if (f.ChargeOper.OperTime == DateTime.MinValue)
                    {
                        f.ChargeOper.OperTime = feeTime;
                    }
                    if (f.ChargeOper.ID == null || f.ChargeOper.ID == string.Empty)
                    {
                        f.ChargeOper.ID = operID;
                    }
                    if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        if (r.SeeDoct.ID != null && r.SeeDoct.ID != "")
                        {
                            ((Register)f.Patient).DoctorInfo.Templet.Doct.ID = r.SeeDoct.ID;
                        }
                        else
                        {
                            errText = "��ѡ��ҽ��";
                            return false;
                        }
                    }

                    if (f.RecipeOper.ID == null || f.RecipeOper.ID == string.Empty)
                    {
                        f.RecipeOper.ID = ((Register)f.Patient).DoctorInfo.Templet.Doct.ID;
                    }

                    f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.ExamineFlag = r.ChkKind;

                    //�������Ϊ������죬��ô������Ŀ�������ն���ˡ�
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //�����Ŀ��ˮ��Ϊ�գ�˵��û�о����������̣���ô�����ն������Ϣ��
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";
                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//�������������ĿΪ��Ҫ�ն������Ŀ������ն������Ϣ��
                    {
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.IsNeedConfirm)
                            {
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    f.Order.ID = orderManager.GetNewOrderID();
                                }
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";

                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!" + ConfirmIntegrate.Err;

                                    return false;
                                }
                            }
                        }
                    }
                    //û�и�ֵҽ����ˮ��,��ֵ�µ�ҽ����ˮ��
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "���ҽ����ˮ�ų���!";

                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//�����������շѱ��
                    {
                        iReturn = ExamiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "��������շѱ��ʧ��!" + ExamiIntegrate.Err;

                            return false;
                        }
                    }

                    //���ɾ�����۱����е������Ŀ����Ŀ��Ϣ,������ϸ.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "ɾ������ʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    FeeItemList feeTemp = new FeeItemList();
                    feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    if (feeTemp == null)//˵��������
                    {
                        if (f.FTSource != "0" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "�����Ѿ�����������Աɾ��,��ˢ�º����շ�!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���������ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���·�����ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//��дҽ����Ϣ

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn == -1)
                        {
                            errText = "����ҽ����Ϣ����!" + orderOutpatientManager.Err;

                            return false;
                        }
                    }

                    #endregion

                    //�����ҩƷ,����û�б�ȷ�Ϲ�,���Ҳ���Ҫ�ն�ȷ��,��ô���뷢ҩ�����б�.
                    //if (f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed)
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugLists.Add(f);
                            }
                        }
                    }
                    //��Ҫҽ��ԤԼ,�����ն�ԤԼ��Ϣ.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "����ҽ��ԤԼ��Ϣ����!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }

                }

                #endregion

                #region �����������շѱ��

                if (r.ChkKind == "2")//�������
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = ExamiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "��������շѱ��ʧ��!" + ExamiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//��ҩ������Ϣ

                string drugSendInfo = null;
                //���뷢ҩ������Ϣ,���ط�ҩ����,��ʾ�ڷ�Ʊ��
                iReturn = PharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                if (iReturn == -1)
                {
                    errText = "����ҩƷ��ϸʧ��!" + PharmarcyManager.Err;

                    return false;
                }
                //�����ҩƷ,��ô���÷�Ʊ����ʾ��ҩ������Ϣ.
                if (drugLists.Count > 0)
                {
                    foreach (Balance invoice in invoices)
                    {
                        invoice.DrugWindowsNO = drugSendInfo;
                    }
                }

                #region//���뷢Ʊ����

                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }
                    if (string.IsNullOrEmpty(balance.CombNO))
                    {
                        balance.CombNO = invoiceCombNO;
                    }
                    balance.BalanceOper.ID = operID;
                    balance.BalanceOper.OperTime = feeTime;
                    balance.Patient.Pact = r.Pact;
                    //����־
                    string tempExamineFlag = null;
                    //�������־ 0 ��ͨ���� 1 ������� 2 �������
                    //���û�и�ֵ,Ĭ��Ϊ��ͨ����
                    if (r.ChkKind.Length > 0)
                    {
                        tempExamineFlag = r.ChkKind;
                    }
                    else
                    {
                        tempExamineFlag = "0";
                    }
                    balance.ExamineFlag = tempExamineFlag;
                    balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                    //=====ȥ��CanceledInvoiceNO=string.Empty ·־��================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//�޶����
                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    if (invoiceStytle == "0")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("�Ѿ����ڷ�Ʊ��Ϊ: " + balance.Invoice.ID +
                                " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                                return false;
                            }
                        }
                    }
                    else if (invoiceStytle == "1")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("�Ѿ�����Ʊ�ݺ�Ϊ: " + balance.PrintedInvoiceNO +
                                " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                                return false;
                            }
                        }
                    }
                    //���뷢Ʊ����fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "�����������!" + outpatientManager.Err;

                        return false;
                    }
                }



                #region ��Ʊ���ߺţ����Ʊ����һ������

                if (!isTempInvoice)//��ʱ��Ʊ���벻����һ������
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;
                    int invoicesCount = invoices.Count;
                    foreach (Balance invoiceObj in invoices)
                    {
                        if (invoiceObj.Memo == "5")
                        {
                            invoicesCount = invoices.Count - 1;
                            continue;
                        }
                    }
                    if (this.UseInvoiceNO((FS.HISFC.Models.Base.Employee)this.feeBedFeeItem.Operator, invoiceStytle, "C", invoicesCount, ref invoiceNo, ref realInvoiceNo, ref errText) < 0)
                    {
                        return false;
                    }

                    foreach (Balance invoiceObj in invoices)
                    {
                        if (invoiceObj.Memo == "5")
                        {
                            continue;
                        }
                        if (this.InsertInvoiceExtend(invoiceObj.Invoice.ID, "C", invoiceObj.PrintedInvoiceNO, "00") < 1)
                        {//��Ʊͷ��ʱ�ȱ���00
                            errText = this.invoiceServiceManager.Err;
                            return false;
                        }
                    }
                }

                #endregion


                #endregion

                #endregion

                #region ����֧����ʽ��Ϣ

                //int payModeSeq = 1;

                //foreach (BalancePay p in payModes)
                //{
                //    p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                //    p.TransType = TransTypes.Positive;
                //    p.Squence = payModeSeq.ToString();
                //    p.IsDayBalanced = false;
                //    p.IsAuditing = false;
                //    p.IsChecked = false;
                //    p.InputOper.ID = operID;
                //    p.InputOper.OperTime = feeTime;
                //    if (string.IsNullOrEmpty(p.InvoiceCombNO))
                //    {
                //        p.InvoiceCombNO = mainInvoiceCombNO;
                //    }
                //    p.CancelType = CancelTypes.Valid;

                //    payModeSeq++;

                //    //realCost += p.FT.RealCost;

                //    iReturn = outpatientManager.InsertBalancePay(p);
                //    if (iReturn == -1)
                //    {
                //        errText = "����֧����ʽ�����!" + outpatientManager.Err;

                //        return false;
                //    }

                //    if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                //    {
                //        bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                //        if (!returnValue)
                //        {
                //            errText = "��ȡ�����˻�ʧ��!" + "\n" + this.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                #region//�������ֱ���շѻ��ߺ���컼�ߣ����¿����־

                string noRegRules = controlParamIntegrate.GetControlParam(Const.NO_REG_CARD_RULES, false, "9");

                //�������������շ�,��ô����Һ���Ϣ,����Ѿ������,��ô����.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = feeTime;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;

                    #region ֻ���շѣ������ļ�����Ͳ���ʱ��{3C5D4918-96F0-4ba8-AC85-6DA86914465D}
                    r.HospitalFirstVisit = registerManager.IsFirstRegister("1", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.RootDeptFirstVisit = registerManager.IsFirstRegister("2",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?"0" : "1";
                    r.DoctFirstVist = registerManager.IsFirstRegister("4",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?"0" : "1";
                    r.IsFirst = registerManager.IsFirstRegister("3",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?false : true;
                    #endregion

                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "���¿����־����" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "���¿�����ҡ�ҽ������" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "���·����־����";
                                return false;
                            }
                        }
                    }
                }
                ////�����ҽ������,���±���ҽ��������Ϣ�� fin_ipr_siinmaininfo
                //if (r.Pact.PayKind.ID == "02")
                //{
                //    //�����ѽ����־
                //    r.SIMainInfo.IsBalanced = true;
                //    // iReturn = interfaceManager.update(r);
                //    if (iReturn < 0)
                //    {
                //        errText = "����ҽ�����߽�����Ϣ����!" + interfaceManager.Err;
                //        return false;
                //    }
                //}

                #endregion



                #endregion
            }
            else//����
            {
                #region ��������

                #region ��ֹ�����ڸõط���ֵ ���۱��������Դ
                foreach (FeeItemList f in feeDetails)
                {
                    f.FTSource = "0";//���۱��������Դ
                }
                #endregion

                string noRegRules = controlParamIntegrate.GetControlParam<string>(Const.NO_REG_CARD_RULES, false, "9");
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = outpatientManager.Operator.ID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //�����۱�����Ϣ.
                bool returnValue = this.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }

            //������Ӧ֢{E4C0E5CF-D93F-48f9-A53C-9ADCCED97A7E}
            FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient iAdptIllnessOutPatient = null;
            iAdptIllnessOutPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient;
            if (iAdptIllnessOutPatient != null)
            {
                //������Ӧ֢��Ϣ
                int returnValue = iAdptIllnessOutPatient.SaveOutPatientFeeDetail(r, ref feeDetails);
                if (returnValue < 0)
                {
                    return false;
                }

            }

            return true;
        }


        /// <summary>
        /// ������߸��»�����Ϣ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeItemLists">������Ϣ</param>
        /// <param name="chargeTime">�շ�ʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>true�ɹ� false ʧ��</returns>
        public bool SetChargeInfo(Register r, ArrayList feeItemLists, DateTime chargeTime, ref string errText)
        {
            bool returnValue = false;
            int iReturn = 0;
            string recipeSeq = null;//�շ�����

            //���������շ����С�gumzh-2014-10-31��
            returnValue = this.SetRecipeFeeSeqOutPatient(r, feeItemLists, ref errText);
            if (!returnValue)
            {
                return false;
            }


            foreach (FeeItemList f in feeItemLists)
            {
                if (!string.IsNullOrEmpty(f.RecipeSequence))
                {
                    recipeSeq = f.RecipeSequence;
                    break;
                }
            }
            if (string.IsNullOrEmpty(recipeSeq))
            {
                recipeSeq = outpatientManager.GetRecipeSequence();
                if (recipeSeq == null || recipeSeq == string.Empty)
                {
                    errText = "����շ����кų���!";

                    return false;
                }
            }

            #region ������ȫ��ɾ��

            foreach (FeeItemList f in feeItemLists)
            {
                if (!string.IsNullOrEmpty(f.RecipeNO))
                {
                    iReturn = outpatientManager.DeleteFeeItemListByMoOrder(f.Order.ID);
                    if (iReturn == -1)
                    {
                        errText = "ɾ��������ϸʧ��!" + outpatientManager.Err;

                        return false;
                    }
                }
            }

            #endregion

            //�������ɴ�����,������д�����,��ϸ�����¸�ֵ.
            returnValue = this.SetRecipeNOOutpatient(r, feeItemLists, ref errText);
            if (!returnValue)
            {
                return false;
            }

            //houwb ������ͬ��ŵ� �շ����к�һ��
            Hashtable hsRecipeSeqByCombNo = new Hashtable();

            //��ͬ������ �շ����к�һ��
            Hashtable hsRecipeSeqByRecipeNO = new Hashtable();
            foreach (FeeItemList f in feeItemLists)
            {
                if (!string.IsNullOrEmpty(f.RecipeSequence))
                {
                    if (!hsRecipeSeqByCombNo.Contains(f.Order.Combo.ID))
                    {
                        hsRecipeSeqByCombNo.Add(f.Order.Combo.ID, f.RecipeSequence);
                    }
                }

                if (!string.IsNullOrEmpty(f.RecipeSequence))
                {
                    if (!hsRecipeSeqByRecipeNO.Contains(f.RecipeNO))
                    {
                        hsRecipeSeqByRecipeNO.Add(f.RecipeNO, f.RecipeSequence);
                    }
                }
            }

            foreach (FeeItemList f in feeItemLists)
            {
                //��֤���ݺϷ���
                if (!this.IsFeeItemListDataValid(f, ref errText))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(f.FTSource) || f.FTSource == "0")
                {
                    f.FTSource = "0";
                    //���۱���
                    f.ChargeOper.ID = outpatientManager.Operator.ID;
                    f.ChargeOper.OperTime = chargeTime;
                }

                f.Patient = r.Clone();


                f.Patient.PID.CardNO = r.PID.CardNO;

                ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                if (((Register)f.Patient).DoctorInfo.Templet.Dept.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Dept.ID == string.Empty)
                {
                    ((Register)f.Patient).DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept.Clone();
                }
                if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                {
                    ((Register)f.Patient).DoctorInfo.Templet.Doct = r.DoctorInfo.Templet.Doct.Clone();
                }
                if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                {
                    f.RecipeOper.Dept.ID = r.User01;

                }

                f.PayType = PayTypes.Charged;
                f.TransType = TransTypes.Positive;
                if (f.Item.SpecialFlag1 != "1"||f.Item .SpecialFlag1!="2")
                    f.NoBackQty = f.Item.Qty;
                f.ExamineFlag = r.ChkKind;
                if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                {
                    f.RecipeOper.Dept.ID = r.User01;
                    if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                    {
                        if (r != null) f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Dept.ID;
                      
                    }
                }
                if (f.Order.ID == null || f.Order.ID == string.Empty)//û�и�ֵҽ����ˮ��
                {
                    f.Order.ID = orderManager.GetNewOrderID();
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        errText = "���ҽ����ˮ�ų���!";

                        return false;
                    }
                }

                //Ϊ����ͬһ��ϡ���ͬ�����Ų���һ���շ�����
                if (f.RecipeSequence == null || f.RecipeSequence == string.Empty)
                {
                    if (hsRecipeSeqByCombNo.Contains(f.Order.Combo.ID))
                    {
                        f.RecipeSequence = hsRecipeSeqByCombNo[f.Order.Combo.ID].ToString();
                    }
                    else if (hsRecipeSeqByRecipeNO.Contains(f.RecipeNO))
                    {
                        f.RecipeSequence = hsRecipeSeqByRecipeNO[f.RecipeNO].ToString();
                    }
                    else
                    {
                        f.RecipeSequence = recipeSeq;
                    }
                }

                if (f.InvoiceCombNO == null || f.InvoiceCombNO == string.Empty)
                {
                    f.InvoiceCombNO = "NULL";
                }

                iReturn = outpatientManager.InsertFeeItemList(f);

                #region �������,����컮��ʱ�Ѿ�����,��������
                //if (r.ChkKind == "2")//�������
                //{

                //    FS.HISFC.Models.Terminal.TerminalApply terminalApply = new FS.HISFC.Models.Terminal.TerminalApply();
                //    terminalApply.Item = f;
                //    terminalApply.Patient = r;
                //    terminalApply.InsertOperEnvironment.OperTime = chargeTime;
                //    terminalApply.InsertOperEnvironment.ID = outpatientManager.Operator.ID;

                //    terminalApply.PatientType = "4";

                //    iReturn = terminalManager.InsertMedTechItem(terminalApply);
                //    if (iReturn == -1)
                //    {
                //        errText = "�����ն�����ȷ�ϱ�ʧ��!" + myConfirm.Err;

                //        return false;
                //    }

                //    if (f.Item.IsNeedBespeak)
                //    {
                //        ////iReturn = terminalManager.MedTechApply(f, this.trans);
                //        if (iReturn == -1)
                //        {
                //            errText = "����ҽ��ԤԼ��Ϣ����!" + f.Name + terminalManager.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                if (iReturn == -1)
                {
                    if (outpatientManager.DBErrCode == 1)//�����ظ���ֱ�Ӹ���
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���·�����ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        errText = "���������ϸʧ��!" + outpatientManager.Err;

                        return false;
                    }
                }
            }

            return true;
        }


        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �����˻�ʹ�õĻ����շѺ���

        /// <summary>
        /// �˻��ն��շѺ���
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">������Ϣ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>true�ɹ� false ʧ��</returns>
        public bool SaveFeeToAccount(Register r, string recipeNO, int sequenceNO, ref string errText)
        {

            FeeItemList f = outpatientManager.GetFeeItemList(recipeNO, sequenceNO);
            if (f == null)
            {
                errText = "��ѯ������Ϣʧ�ܣ�" + outpatientManager.Err;
                return false;
            }
            DateTime feeTime = outpatientManager.GetDateTimeFromSysDateTime();
            string feeOper = outpatientManager.Operator.ID;
            f.FeeOper.ID = feeOper;
            f.FeeOper.OperTime = feeTime;
            f.PayType = PayTypes.Balanced;
            int iReturn;
            iReturn = outpatientManager.UpdateFeeDetailFeeFlag(f);
            if (iReturn <= 0)
            {
                errText = "���·����շѱ��ʧ�ܣ�" + outpatientManager.Err;
                return false;
            }

            if (f.FTSource == "1")
            {
                iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, feeOper);
                if (iReturn == -1)
                {
                    errText = "����ҽ����Ϣ����!" + orderOutpatientManager.Err;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �˻����ۺ���
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeItemLists">������Ϣ</param>
        /// <param name="chargeTime">����ʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool SetChargeInfoToAccount(Register r, ArrayList feeItemLists, DateTime chargeTime, ref string errText)
        {
            #region ɾ�������
            ArrayList drugLists = new ArrayList();
            ArrayList undrugList = new ArrayList();
            Terminal.Confirm ConfirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            Dictionary<string, string> dicRecipe = new Dictionary<string, string>();
            foreach (FeeItemList f in feeItemLists)
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    if (!f.IsConfirmed)
                    {
                        if (!f.Item.IsNeedConfirm)
                        {
                            if (PharmarcyManager.DelApplyOut(f.RecipeNO, f.SequenceNO.ToString()) < 0)
                            {
                                errText = "ɾ����ҩ������Ϣϸʧ�ܣ�" + ConfirmIntegrate.Err;
                                return false;
                            }
                            if (!dicRecipe.ContainsKey(f.RecipeNO))
                            {
                                dicRecipe.Add(f.RecipeNO, f.ExecOper.Dept.ID);
                            }
                            else
                            {
                                if (dicRecipe[f.RecipeNO] != f.ExecOper.Dept.ID)
                                {
                                    dicRecipe.Add(f.RecipeNO, f.ExecOper.Dept.ID);
                                }
                            }
                            drugLists.Add(f);
                        }
                    }
                }
                else
                {
                    if (!f.IsConfirmed)
                    {
                        if (f.Item.IsNeedConfirm)
                        {
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                            }
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                errText = "���ҽ����ˮ�ų���!";

                                return false;
                            }
                            if (ConfirmIntegrate.DelTecApply(f.RecipeNO, f.SequenceNO.ToString()) < 0)
                            {
                                errText = "ɾ���ն�������Ϣʧ�ܣ�" + ConfirmIntegrate.Err;
                                return false;
                            }
                            undrugList.Add(f);
                        }
                    }
                }
            }
            #endregion

            #region ɾ��ҩƷ����ͷ��
            foreach (string recipeNO in dicRecipe.Keys)
            {
                if (PharmarcyManager.DeleteDrugStoRecipe(recipeNO, dicRecipe[recipeNO]) < 0)
                {
                    MessageBox.Show("ɾ������ͷ����Ϣʧ�ܣ�" + PharmarcyManager.Err);
                    return false;
                }
            }
            #endregion

            #region ��������

            foreach (FeeItemList f in feeItemLists)
            {
                f.IsAccounted = true;
                f.FT.TotCost = f.FT.OwnCost;
                if (string.IsNullOrEmpty((f.Patient as Register).DoctorInfo.Templet.Doct.ID))
                {
                    (f.Patient as Register).DoctorInfo.Templet.Doct = outpatientManager.Operator;
                }
            }

            bool resultValue = SetChargeInfo(r, feeItemLists, chargeTime, ref errText);
            if (!resultValue) return false;
            #endregion

            #region ����ҩƷ�����
            string drugSendInfo = null;
            //���뷢ҩ������Ϣ,���ط�ҩ����,��ʾ�ڷ�Ʊ��
            if (drugLists.Count > 0)
            {
                foreach (FeeItemList f in drugLists)
                {
                    if (((Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Doct = outpatientManager.Operator;
                    }
                }

                int iReturn = PharmarcyManager.ApplyOut(r, drugLists, string.Empty, chargeTime, false, out drugSendInfo);
                if (iReturn == -1)
                {
                    errText = "����ҩƷ��ϸʧ��!" + PharmarcyManager.Err;

                    return false;
                }
            }
            #endregion

            #region �����ն���Ŀ����
            foreach (FeeItemList f in undrugList)
            {
                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                {
                    errText = "�����ն�����ȷ�ϱ�ʧ��!" + ConfirmIntegrate.Err;

                    return false;
                }
            }
            #endregion

            return true;
        }

        #endregion


        /// <summary>
        /// ��Ʊ��ӡ����
        /// </summary>
        /// <param name="invoicePrintDll">��Ʊ��ӡdllλ��</param>
        /// <param name="rInfo">���߻�����Ϣ</param>
        /// <param name="invoices">��Ʊ����</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="alPayModes">֧����ʽ����</param>
        /// <param name="t">���ݿ�����</param>
        /// <param name="isPreView">�Ƿ�Ԥ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int PrintInvoice(string invoicePrintDll, Register rInfo, ArrayList invoices, ArrayList invoiceDetails,
            ArrayList feeDetails, ArrayList alPayModes, bool isPreView, ref string errText)
        {

            int iReturn = 0;//����ֵ
            ArrayList alTempPayModes = new ArrayList();//��ʱ֧����ʽ

            if (alPayModes != null)
            {
                foreach (BalancePay p in alPayModes)
                {
                    alTempPayModes.Add(p);
                }
            }

            // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
            // 2011-08-04
            bool blnNewPrintStyle = false;

            if (string.IsNullOrEmpty(invoicePrintDll))
            {
                blnNewPrintStyle = true;
                //errText = "û��ά����Ʊ��ӡ����!��ά��";
                //return -1;
            }

            invoicePrintDll = Application.StartupPath + invoicePrintDll;
            ArrayList alPrint = new ArrayList();
            IInvoicePrint iInvoicePrint = null;

            for (int i = 0; i < invoices.Count; i++)
            {
                Balance invoice = invoices[i] as Balance;
                if (invoice.Memo == "5")
                {
                    continue;
                }

                ArrayList invoiceDetailsTemp = ((ArrayList)invoiceDetails[0])[i] as ArrayList;

                if (!blnNewPrintStyle)
                {
                    #region ��Ʊ��ӡ�ɷ�ʽ
                    object obj = null;
                    Assembly a = Assembly.LoadFrom(invoicePrintDll);
                    System.Type[] types = a.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.GetInterface("IInvoicePrint") != null)
                        {
                            try
                            {
                                obj = System.Activator.CreateInstance(type);
                                iInvoicePrint = obj as IInvoicePrint;

                                iInvoicePrint.SetTrans(this.trans);
                                if (invoices.Count > 1 && rInfo.Pact.PayKind.ID == "01")
                                {
                                    string payMode = string.Empty;
                                    DealSplitPayMode(alTempPayModes, invoice, ref payMode);
                                    iInvoicePrint.SetPayModeType = "1";
                                    iInvoicePrint.SplitInvoicePayMode = payMode;
                                }

                                iReturn = iInvoicePrint.SetPrintValue(rInfo, invoice, invoiceDetailsTemp, feeDetails, alPayModes, isPreView);

                                if (iReturn == -1)
                                {
                                    return 0;
                                }

                                alPrint.Add(obj);
                                break;
                            }
                            catch (Exception ex)
                            {
                                errText = ex.Message;

                                return 0;
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region �·�ʽ

                    iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
                    if (iInvoicePrint == null)
                    {
                        errText = "��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�";
                        return -1;
                    }

                    iInvoicePrint.SetTrans(this.trans);
                    if (invoices.Count > 1 && rInfo.Pact.PayKind.ID == "01")
                    {
                        string payMode = string.Empty;
                        DealSplitPayMode(alTempPayModes, invoice, ref payMode);
                        iInvoicePrint.SetPayModeType = "1";
                        iInvoicePrint.SplitInvoicePayMode = payMode;
                    }
                    iReturn = iInvoicePrint.SetPrintValue(rInfo, invoice, invoiceDetailsTemp, feeDetails, alPayModes, isPreView);

                    if (iReturn == -1)
                    {
                        return 0;
                    }

                    alPrint.Add(iInvoicePrint);
                    //break;

                    #endregion
                }
            }
            for (int i = 0; i < alPrint.Count; i++)//foreach(object objPrint in alPrint)
            {
                if (i == 0)
                {
                    iInvoicePrint = alPrint[i] as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;
                }
                iReturn = ((IInvoicePrint)alPrint[i]).Print();
                if (iReturn == -1)
                {
                    return 0;
                }
            }

            if (alPrint.Count > 0 && feeDetails.Count > 0)
            {
                try
                {
                    FeeItemList feeTemp = feeDetails[0] as FeeItemList;

                    if (iInvoicePrint != null && printRecipeHeler.GetObjectFromID(((Register)feeTemp.Patient).DoctorInfo.Templet.Doct.ID) == null)
                    {
                        iInvoicePrint.SetPrintOtherInfomation(rInfo, invoices, null, feeDetails);
                        iInvoicePrint.PrintOtherInfomation();
                    }
                }
                catch (Exception ex)
                {
                    errText = ex.Message;

                    return 0;
                }
            }

            return 1;
        }
        /// ��Ʊ��ӡ����
        /// </summary>
        /// <param name="invoicePrintDll">��Ʊ��ӡdllλ��</param>
        /// <param name="rInfo">���߻�����Ϣ</param>
        /// <param name="invoices">��Ʊ����</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="invoiceFeeDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
        /// <param name="alPayModes">֧����ʽ����</param>
        /// <param name="t">���ݿ�����</param>
        /// <param name="isPreView">�Ƿ�Ԥ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int PrintInvoice(string invoicePrintDll, Register rInfo, ArrayList invoices, ArrayList invoiceDetails,
            ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList alPayModes, bool isPreView, ref string errText)
        {

            int iReturn = 0;//����ֵ
            //ArrayList alTempPayModes = new ArrayList();//��ʱ֧����ʽ

            //if (alPayModes != null)
            //{
            //    foreach (BalancePay p in alPayModes)
            //    {
            //        alTempPayModes.Add(p);
            //    }
            //}

            // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
            // 2011-08-04
            bool blnNewPrintStyle = false;

            if (string.IsNullOrEmpty(invoicePrintDll))
            {
                blnNewPrintStyle = true;
                //errText = "û��ά����Ʊ��ӡ����!��ά��";
                //return -1;
            }

            invoicePrintDll = Application.StartupPath + invoicePrintDll;
            ArrayList alPrint = new ArrayList();
            IInvoicePrint iInvoicePrint = null;

            for (int i = 0; i < invoices.Count; i++)
            {
                Balance invoice = invoices[i] as Balance;
                if (invoice.Memo == "5")
                {
                    continue;
                }

                ArrayList invoiceDetailsTemp1 = ((ArrayList)invoiceDetails[0])[0] as ArrayList;
                ArrayList invoiceFeeDetailsTemp = ((ArrayList)invoiceFeeDetails[0])[i] as ArrayList;
                ArrayList invoiceDetailsTemp = new ArrayList();
                //ѭ����Ʊ��ϸ����ݷ�Ʊ���ҵ���Ӧ�ķ�Ʊ��ϸ  
                for (int j = 0; j < invoiceDetailsTemp1.Count; j++)
                {
                    BalanceList balanceList = invoiceDetailsTemp1[j] as BalanceList;
                    if (invoice.Invoice.ID == balanceList.BalanceBase.Invoice.ID)
                    {
                        invoiceDetailsTemp.Add(invoiceDetailsTemp1[j]);
                    }
                }

                if (!blnNewPrintStyle)
                {
                    #region ��Ʊ��ӡ�ɷ�ʽ
                    object obj = null;
                    Assembly a = Assembly.LoadFrom(invoicePrintDll);
                    System.Type[] types = a.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.GetInterface("IInvoicePrint") != null)
                        {
                            try
                            {
                                obj = System.Activator.CreateInstance(type);
                                iInvoicePrint = obj as IInvoicePrint;

                                iInvoicePrint.SetTrans(this.trans);
                                //if (invoices.Count > 1 && rInfo.Pact.PayKind.ID == "01")
                                //{
                                //    string payMode = string.Empty;
                                //    DealSplitPayMode(alTempPayModes, invoice, ref payMode);
                                //    iInvoicePrint.SetPayModeType = "1";
                                //    iInvoicePrint.SplitInvoicePayMode = payMode;
                                //}

                                iReturn = iInvoicePrint.SetPrintValue(rInfo, invoice, invoiceDetailsTemp, invoiceFeeDetailsTemp, alPayModes, isPreView);

                                if (iReturn == -1)
                                {
                                    return 0;
                                }

                                alPrint.Add(obj);
                                break;
                            }
                            catch (Exception ex)
                            {
                                errText = ex.Message;

                                return 0;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region �·�ʽ

                    iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
                    if (iInvoicePrint == null)
                    {
                        errText = "��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�";
                        return -1;
                    }

                    iInvoicePrint.SetTrans(this.trans);

                    iReturn = iInvoicePrint.SetPrintValue(rInfo, invoice, invoiceDetailsTemp, invoiceFeeDetailsTemp, alPayModes, isPreView);

                    if (iReturn == -1)
                    {
                        return 0;
                    }

                    alPrint.Add(iInvoicePrint);
                    //break; ���Ƿַ�Ʊ���������

                    #endregion
                }
            }
            for (int i = 0; i < alPrint.Count; i++)//foreach(object objPrint in alPrint)
            {
                if (i == 0)
                {
                    iInvoicePrint = alPrint[i] as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;
                }
                iReturn = ((IInvoicePrint)alPrint[i]).Print();
                if (iReturn == -1)
                {
                    return 0;
                }
            }

            if (alPrint.Count > 0 && feeDetails.Count > 0)
            {
                try
                {
                    FeeItemList feeTemp = feeDetails[0] as FeeItemList;

                    if (iInvoicePrint != null && printRecipeHeler.GetObjectFromID(((Register)feeTemp.Patient).DoctorInfo.Templet.Doct.ID) == null)
                    {
                        iInvoicePrint.SetPrintOtherInfomation(rInfo, invoices, null, feeDetails);
                        iInvoicePrint.PrintOtherInfomation();
                    }
                }
                catch (Exception ex)
                {
                    errText = ex.Message;

                    return 0;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���÷ַ�Ʊ���֧����ʽ
        /// </summary>
        /// <param name="alPayModes"></param>
        /// <param name="invoice"></param>
        /// <param name="payMode"></param>
        private void DealSplitPayMode(ArrayList alPayModes, Balance invoice, ref string payMode)
        {
            decimal totCost = invoice.FT.PayCost + invoice.FT.PubCost + invoice.FT.OwnCost;
            decimal cardCost = 0m;
            foreach (BalancePay p in alPayModes)
            {
                if (p.PayType.ID.ToString() == "CA" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "�ֽ�: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.TotCost -= totCost;
                        payMode += "�ֽ�: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "PS" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "ҽ����: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "ҽ����: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if ((p.PayType.ID.ToString() == "CD" || p.PayType.ID.ToString() == "DB") && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        cardCost += p.FT.RealCost;
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        cardCost += totCost;
                        //payMode += "ҽ����: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost,2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "CH" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "֧Ʊ: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "֧Ʊ: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "SB" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "�籣��: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "�籣��: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "YS" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "Ժ���˻�: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.TotCost -= totCost;
                        payMode += "Ժ���˻�: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
            }

            if (cardCost > 0)
            {
                payMode += "���п�: " + FS.FrameWork.Public.String.FormatNumberReturnString(cardCost, 2);
            }
        }

        /// <summary>
        /// ��ô�����
        /// </summary>
        /// <returns></returns>
        public string GetRecipeNO()
        {
            this.SetDB(outpatientManager);

            return outpatientManager.GetRecipeNO();
        }

        /// <summary>
        /// ͨ��ҽ����Ŀ��ˮ�Ż��������Ŀ��ˮ�ţ��õ�������ϸ
        /// </summary>
        /// <param name="MOOrder">ҽ����Ŀ��ˮ�Ż��������Ŀ��ˮ��</param>
        /// <returns>null ���� ArrayList Fee.OutPatient.FeeItemListʵ�弯��</returns>
        public ArrayList QueryFeeDetailFromMOOrder(string MOOrder)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailFromMOOrder(MOOrder);
        }

        /// <summary>
        /// ����ҽ�����������Ŀ��ˮ��ɾ����ϸ
        /// </summary>
        /// <param name="MOOrder">ҽ�����������Ŀ��ˮ��</param>
        /// <returns>�ɹ�: >= 1 ʧ��: -1 û��ɾ�������ݷ��� 0</returns>
        public int DeleteFeeItemListByMoOrder(string MOOrder)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeItemListByMoOrder(MOOrder);
        }

        #region ��ѯ��Ʊ��Ϻ��Ƿ��Ѿ��շ�
        /// <summary>
        /// ���ݷ�Ʊ��ϺŲ�ѯ��������Ϣ�Ƿ��Ѿ��շѡ�
        /// </summary>
        /// <param name="RecipeSeq">��Ʊ��Ϻ�</param>
        /// <returns>0 ���շѣ� 1 δ�շ� ��-1 ��ѯ����</returns>
        public int IsFeeItemListByRecipeNO(string RecipeSeq)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.IsFeeItemListByRecipeNO(RecipeSeq);
        }
        #endregion

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ�����δ�շ���ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemLists(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemLists(cardNO, dtFrom, dtTo);
        }


        /// <summary>
        /// ���ݲ����ź�ʱ��εõ�����δ�շ���ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemListsZs(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemListsForZs(cardNO, dtFrom, dtTo);
        }

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ�����δ�շ���ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemListsZsSubjob(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemListsForZsSubjob(cardNO, dtFrom, dtTo);
        }

        #region ����շ����к�

        /// <summary>
        /// ����շ����к�
        /// </summary>
        /// <returns>�ɹ�:�շ����к� ʧ��:null</returns>
        public string GetRecipeSequence()
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetRecipeSequence();
        }

        #endregion

        #endregion

        /// <summary>
        /// ��ȡ��ͬ��λ�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPackList()
        {
            this.SetDB(pactManager);

            return pactManager.QueryPactUnitAll();
        }

        #endregion

        #region ����ҽ��վ���add by sunm

        /// <summary>
        /// �������������ϸ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int InsertFeeItemList(FeeItemList feeItemList)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.InsertFeeItemList(feeItemList);
        }

        /// <summary>
        /// �������������ϸ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int UpdateFeeItemList(FeeItemList feeItemList)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.UpdateFeeItemList(feeItemList);
        }

        /// <summary>
        /// ���ݴ����źʹ�������ˮ��ɾ��������ϸ
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="recipeSequence"></param>
        /// <returns></returns>
        public int DeleteFeeItemListByRecipeNO(string recipeNO, string recipeSequence)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeItemListByRecipeNO(recipeNO, recipeSequence);
        }

        /// <summary>
        /// ������Ϻź���ˮ��ɾ��������ϸ
        /// </summary>
        /// <param name="combNO"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int DeleteFeeDetailByCombNoAndClinicCode(string combNO, string clinicCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeDetailByCombNoAndClinicCode(combNO, clinicCode);
        }

        /// <summary>
        /// ͨ��������ˮ�ź���Ϻŵõ�������ϸ
        /// </summary>
        /// <param name="combNO"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailbyComoNOAndClinicCode(string combNO, string clinicCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailbyComoNOAndClinicCode(combNO, clinicCode);
        }

        /// <summary>
        /// ͨ��������ˮ�ź��շ���ŵõ�δ�շѵķ�����ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryValidFeeDetailbyClinicCodeAndRecipeSeq(string clinicCode, string recipeNO)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryValidFeeDetailbyClinicCodeAndRecipeSeq(clinicCode, recipeNO);
        }

        /// <summary>
        /// ͨ��������ˮ�ź��շ���ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeSeq"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndRecipeSeq(string clinicCode, string recipeSeq, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(clinicCode, recipeSeq, feeFlag);
        }

        /// <summary>
        /// ͨ��������ˮ�źͿ�����ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeSeq"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNO(string clinicCode, string SeeNO, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndSeeNO(clinicCode, SeeNO, feeFlag);
        }

        /// <summary>
        /// ͨ��������ˮ�źͿ�����ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNONotNull(string clinicCode, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndSeeNONotNull(clinicCode, feeFlag);
        }

        #endregion

        #region Ժ��ע��

        /// <summary>
        /// ���Ժע��Ϣ�����÷�
        /// </summary>
        /// <param name="usageCode"></param>
        /// <returns></returns>
        public ArrayList GetInjectInfoByUsage(string usageCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetInjectInfoByUsage(usageCode);
        }

        /// <summary>
        /// ɾ���÷���Ŀ��Ϣ
        /// </summary>
        /// <param name="Usage"></param>
        /// <returns></returns>
        public int DelInjectInfo(string Usage)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DelInjectInfo(Usage);
        }

        /// <summary>
        /// �����÷���Ŀ��Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertInjectInfo(FS.HISFC.Models.Order.OrderSubtbl obj)
        {
            SetDB(outpatientManager);
            return outpatientManager.InsertInjectInfo(obj);
        }

        #endregion

        #region addby xuewj 2009-8-26 ִ�е����� ����Ŀά�� {0BB98097-E0BE-4e8c-A619-8B4BCA715001}

        /// <summary>
        /// ������ִ�е�ά���õ�
        /// </summary>
        /// <param name="nruseID">��ʿվ����</param>
        /// <param name="sysClass">ϵͳ���</param>
        /// <param name="validState">��Ч״̬</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryItemOutExecBill(string nruseID, string sysClass, string validState, ref DataSet ds)
        {
            this.SetDB(itemManager);
            return itemManager.QueryItemOutExecBill(nruseID, sysClass, validState, ref ds);
        }

        #endregion

        #region{CA82280B-51B6-4462-B63E-43F4ECF456A3}

        public ArrayList QueryDeptList(string itemID, string type)
        {
            this.SetDB(itemManager);
            return itemManager.GetDeptNeuobjByItemID(itemID, type);
        }

        #endregion

        /// <summary>
        /// ���ݴ����źʹ�������ˮ�Ų�ѯ�˷����룬����ҽ��վ�ж�ҩ���Ƿ��Ѿ������˷������ˡ�
        /// {5C327B2F-2B74-45bb-8435-4E5118215BD2}
        /// create by lh 10-05-24
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public ArrayList QueryReturnApplysByRecipeNoSequenceNo(string inpatientNO, string recipeNo, string sequenceNo)
        {
            return returnMgr.QueryReturnApplysByRecipeNoSequenceNo(inpatientNO, recipeNo, sequenceNo);
        }
        //{5C327B2F-2B74-45bb-8435-4E5118215BD2}
        public string GetReturnApplySequence()
        {
            return returnMgr.GetReturnApplySequence();
        }
        //{5C327B2F-2B74-45bb-8435-4E5118215BD2}
        public int InsertReturnApply(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return returnMgr.InsertReturnApply(returnApply);
        }
        #endregion

        #region ö��

        /// <summary>
        /// �շѺ�����������
        /// </summary>
        private enum ChargeTypes
        {
            /// <summary>
            /// ����
            /// </summary>
            Charge = 0,

            /// <summary>
            /// �շ�
            /// </summary>
            Fee = 1,

            /// <summary>
            /// ���ۼ�¼ת�շ�
            /// </summary>
            ChargeToFee = 2,

        }

        #endregion

        #region �����ʻ�

        /// <summary>
        /// ���ݴ����źʹ�������ˮ�Ÿ����ѿ��˻���־
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">��������ˮ��</param>
        /// <param name="isAccounted">true �Ѿ���ȡ�˻� false û�п�ȡ�˻�</param>
        /// <returns>�ɹ� 1 ʧ�� -1 �����ϸ������� 0</returns>
        public int UpdateAccountByRecipeNO(string recipeNO, int sequenceNO, bool isAccounted)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateAccountFlag(recipeNO, sequenceNO, isAccounted);
        }

        /// <summary>
        /// ����ҽ����ˮ�ź���Ŀ��������ѿ��˻���־
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="isAccounted">true �Ѿ���ȡ�˻� false û�п�ȡ�˻�</param>
        /// <returns>�ɹ� 1 ʧ�� -1 �����ϸ������� 0</returns>
        public int UpdateAccountByMoOrderAndItemCode(string itemCode, string moOrder, bool isAccounted)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateAccountFlag(itemCode, moOrder, isAccounted);
        }


        /// <summary>
        /// �ʻ�֧��
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="money">���</param>
        /// <param name="reMark">��ʶ</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns> 1 �ɹ� 0ȡ���շ� -1ʧ��</returns>
        public int AccountPay(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string deptCode, string invoiceType)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.AccountPayManager(patient, money, reMark, invoiceType, deptCode, 0);
            if (!bl) return -1;
            this.Err = accountManager.Err;
            return 1;
        }

        /// <summary>
        /// �˷��뻧
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="money">���</param>
        /// <param name="reMark">��ʶ</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int AccountCancelPay(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string deptCode, string invoiceType)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.AccountPayManager(patient, money, reMark, invoiceType, deptCode, 1);
            if (!bl)
            {
                this.Err = accountManager.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �õ��ʻ����
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <param name="vacancy">���</param>
        /// <returns>0:�ʻ�ͣ�û��ʻ������� -1��ѯʧ�� 1�ɹ�</returns>
        public int GetAccountVacancy(string cardNO, ref decimal vacancy)
        {
            this.SetDB(accountManager);
            int resultValue = accountManager.GetVacancy(cardNO, ref vacancy);
            this.Err = accountManager.Err;
            return resultValue;
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        private FS.HISFC.BizProcess.Interface.Account.IPassWord GetIPassWord()
        {
            if (ipassWord == null)
            {
                System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance("HISFC.Components.Account", "FS.HISFC.Components.Account.Controls.ucPassWord");
                if (obj == null)
                {
                    return null; ;
                }
                ipassWord = obj.Unwrap() as FS.HISFC.BizProcess.Interface.Account.IPassWord;
            }
            return ipassWord;
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// ��֤�ʻ�����
        /// </summary>
        /// <param name="cardNo">���￨��</param>
        /// <returns>true �ɹ���falseʧ��</returns>
        public bool CheckAccountPassWord(HISFC.Models.RADT.Patient patient)
        {
            // Ԥ����۷�ʱ���Ƿ���Ҫ��֤���� 0 = ����Ҫ��1 = ��Ҫ��
            // {5CCCF7F7-E9A5-4982-A5AF-C3ADB99DD9F0}
            string strValue = controlParamIntegrate.GetControlParam<string>(AccountConstant.NeedCheckAccountPW, true, "0");
            if (strValue == "1")
            {
                GetIPassWord();
                ipassWord.Patient = patient;
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ipassWord as Control);
                if (ipassWord.IsOK)
                {
                    if (ipassWord.ValidPassWord)
                        return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// ͨ�������Ų������￨��
        /// </summary>
        /// <param name="markNo">������</param>
        /// <param name="markType">������</param>
        /// <param name="cardNo">���￨��</param>
        /// <returns>bool true �ɹ���false ʧ��</returns>
        public bool GetCardNoByMarkNo(string markNo, ref string cardNo)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.GetCardNoByMarkNo(markNo, ref cardNo);
            this.Err = accountManager.Err;
            return bl;
        }

        /// <summary>
        /// ͨ�������Ų������￨��
        /// </summary>
        /// <param name="markNo">������</param>
        /// <param name="markType">������</param>
        /// <param name="cardNo">���￨��</param>
        /// <returns>bool true �ɹ���false ʧ��</returns>
        public bool GetCardNoByMarkNo(string markNo, NeuObject markType, ref string cardNo)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.GetCardNoByMarkNo(markNo, markType, ref cardNo);
            this.Err = accountManager.Err;
            return bl;
        }

        /// <summary>
        /// �������￨�Ų�������
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns>�û�����</returns>
        public string GetPassWordByCardNO(string cardNO)
        {
            this.SetDB(accountManager);
            return accountManager.GetPassWordByCardNO(cardNO);
        }

        /// <summary>
        /// �������￨�Ų��һ�����Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string cardNO)
        {
            this.SetDB(accountManager);
            return accountManager.GetPatientInfoByCardNO(cardNO);
        }


        /// <summary>
        /// ���ݿ������������
        /// </summary>
        /// <param name="markNo">���뿨��</param>
        /// <param name="accountCard">���ݹ�������Ŀ���Ϣ</param>
        /// <returns>1�ɹ�(�Ѿ�����) 0����Ϊ���� -1ʧ��</returns>
        public int ValidMarkNO(string markNo, ref HISFC.Models.Account.AccountCard accountCard)
        {
            this.SetDB(accountManager);
            return accountManager.GetCardByRule(markNo, ref accountCard);
        }
        #endregion

        #region ��Ʊ����{BF01254E-3C73-43d4-A644-4B258438294E}
        /// <summary>
        /// ���뷢Ʊ���ű�
        /// </summary>
        /// <param name="invoiceJumpRecord"></param>
        /// <returns></returns>
        public int InsertInvoiceJumpRecord(FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord)
        {

            //{BF01254E-3C73-43d4-A644-4B258438294E}
            this.SetDB(this.invoiceJumpRecordMgr);
            this.SetDB(invoiceServiceManager);
            //ȥ������
            string happenNO = this.invoiceJumpRecordMgr.GetMaxHappenNO(invoiceJumpRecord.Invoice.AcceptOper.ID, invoiceJumpRecord.Invoice.Type.ID);

            if (happenNO == "-1")
            {
                this.Err = this.invoiceJumpRecordMgr.Err;
                return -1;
            }

            invoiceJumpRecord.HappenNO = int.Parse(happenNO) + 1;
            invoiceJumpRecord.Oper.OperTime = this.invoiceJumpRecordMgr.GetDateTimeFromSysDateTime();

            int returnValue = 0;
            returnValue = this.invoiceJumpRecordMgr.InsertTable(invoiceJumpRecord);

            if (returnValue < 0)
            {
                this.Err = this.invoiceJumpRecordMgr.Err;
                return -1;
            }

            //����ʹ�ú�
            returnValue = this.invoiceServiceManager.UpdateUsedNO(invoiceJumpRecord.NewUsedNO, invoiceJumpRecord.Invoice.AcceptOper.ID, invoiceJumpRecord.Invoice.Type.ID);
            if (returnValue < 0)
            {
                this.Err = this.invoiceServiceManager.Err;
            }

            return 1;

        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {

                Type[] type = new Type[4];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.ISplitInvoice);
                type[1] = typeof(IFeeOweMessage);
                type[2] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient);
                type[3] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe);
                return type;
            }
        }

        #endregion

        /// <summary>
        /// ���ݴ������Ŀ�������Զ���ֵ��
        /// </summary>
        /// <param name="pInfo">����ʵ��</param>
        /// <param name="item">��Ŀ��Ϣ���շ�����Ҫ��������Ŀʵ��item.qty��</param>
        /// <param name="execDept">ִ�п��Ҵ���</param>
        /// <returns></returns>
        public int FeeAutoItem(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Base.Item item,
            string execDept)
        {
            //FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            //FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

            //pactUnitManager.SetTrans(this.trans);

            //string operCode = pactUnitManager.Operator.ID;
            //DateTime dtNow = pactUnitManager.GetDateTimeFromSysDateTime();

            //ItemList.Item = item;
            ////��Ժ����
            //((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////��ʿվ
            //((FS.HISFC.Models.RADT                                                                               .PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            ////ִ�п���
            //ItemList.ExecOper.Dept.ID = execDept;
            ////�ۿ����
            //ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////��������
            //ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////����ҽ��
            //ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //ҽ��
            ////���ݴ����ʵ�崦��۸�
            //decimal price = 0;
            //decimal orgPrice = 0;

            //if(this.GetPriceForInpatient(pInfo.Pact.ID,item,ref price,ref orgPrice)==-1)          
            //{
            //    this.Err = "ȡ��Ŀ:" + item.Name + "��r�۸����!" + pactUnitManager.Err;
            //    return -1;
            //}
            //item.Price = price;

            //// ԭʼ�ܷ��ã�����Ӧ�շ��ã������Ǻ�ͬ��λ���أ�
            //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //item.DefPrice = orgPrice;

            ////ҩƷĬ�ϰ���С��λ�շ�,��ʾ�۸�ҲΪ��С��λ�۸�,�������ݿ��Ϊ��װ��λ�۸�
            ////if (item.IsPharmacy)//ҩƷ
            //if (item.ItemType == EnumItemType.Drug)//ҩƷ
            //{
            //    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);

            //    // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //    orgPrice = FS.FrameWork.Public.String.FormatNumber(item.DefPrice / item.PackQty, 4);
            //}

            ///* �ⲿ�Ѿ���ֵ���֣��۸���������λ���Ƿ�ҩƷ
            // * ItemList.Item.Price = 0;ItemList.Item.Qty;  
            // * ItemList.Item.PriceUnit = "��"; 
            // * ItemList.Item.IsPharmacy = false;
            // */

            //ItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * price, 2);

            //// ԭʼ�ܷ��ã�����Ӧ�շ��ã������Ǻ�ͬ��λ���أ�
            //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //ItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * orgPrice, 2);

            ////ItemList.FT.OwnCost = ItemList.FT.TotCost;

            //ItemList.PayType = PayTypes.Balanced;
            //ItemList.IsBaby = false;
            //ItemList.BalanceNO = 0;
            //ItemList.BalanceState = "0";
            ////��������
            //ItemList.NoBackQty = item.Qty;

            ////����Ա
            //ItemList.FeeOper.ID = operCode;
            //ItemList.ChargeOper.ID = operCode;
            //ItemList.ChargeOper.OperTime = dtNow;
            //ItemList.FeeOper.OperTime = dtNow;

            //#region {3C6A1DD7-7522-418b-89A5-4B973ED320C3}
            //ItemList.FT.OwnCost = ItemList.FT.TotCost;
            //ItemList.TransType = TransTypes.Positive;
            //#endregion

            ////�����շѺ���
            //return this.FeeItem(pInfo, ItemList);
            //200�Զ��շѣ�Ĭ��Ϊϵͳ���գ�
            return this.FeeAutoItem(pInfo, item, "200", execDept, pactManager.Operator.ID);
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �˻�����
        /// <summary>
        /// ���ݴ�����ִ�п��Ҳ���ҩƷ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="deptCode">ִ�п���</param>
        /// <returns></returns>
        public int GetDrugUnFeeCount(string recipeNO, string deptCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetDrugUnFeeCount(recipeNO, deptCode);
        }

        /// <summary>
        /// �����˻�֧���ķ�Ʊ����������ҽ���۳��Һŷѵ��˷�
        /// �˴��۳��˻��ĹҺŷѺ����
        /// </summary>
        /// <param name="invoiceNo">��Ʊ��</param>
        /// <param name="invoiceSeq">��Ʊ���</param>
        /// <returns>1 ���ϳɹ������˻���0 ���˻�֧������������ -1 ����</returns>
        public int LogOutInvoiceByAccout(FS.HISFC.Models.Registration.Register patient, string invoiceNo, string invoiceSeq)
        {
            this.SetDB(outpatientManager);
            this.SetDB(accountManager);
            decimal payCost = 0;
            int rev = outpatientManager.LogOutInvoiceByAccout(invoiceNo, invoiceSeq, ref payCost);
            if (rev == -1)
            {
                return 1;
            }
            rev = this.AccountCancelPay(patient, 0 - Math.Abs(payCost), invoiceNo, (outpatientManager.Operator as Employee).Dept.ID, "C");
            if (rev == -1)
            {
                return -1;
            }
            return 1;
        }

        #endregion

        #region ����֤��

        /// <summary>
        /// ���»�����Ϣ֤����Ϣ
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public int UpdatePatientIden(FS.HISFC.Models.RADT.Patient pInfo)
        {
            if (!string.IsNullOrEmpty(pInfo.IDCardType.ID) && !string.IsNullOrEmpty(pInfo.IDCard))
            {
                if (accountManager.InsertIdenInfo(pInfo) == -1)
                {
                    if (accountManager.DBErrCode == 1)
                    {
                        if (accountManager.UpdateIdenInfo(pInfo) == -1)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion

        //{645F3DDE-4206-4f26-9BC5-307E33BD882C}
        #region �ս���շ��ж�

        /// <summary>
        /// �ս���շ��ж�����
        /// </summary>
        /// <param name="feeOperCode">�տ�Ա����</param>
        /// <param name="isInpatient">�Ƿ�סԺ</param>
        /// <param name="errTxt">������Ϣ</param>
        /// <returns></returns>
        public bool AfterDayBalanceCanFee(string feeOperCode, bool isInpatient, ref string errTxt)
        {
            string canFeeType = controlParamIntegrate.GetControlParam<string>("100035", true, "0");
            //���ж�
            if (canFeeType == "0")
            {
                return true;
            }
            else
            {
                bool returnValue = false;
                DateTime now = empowerFeeManager.GetDateTimeFromSysDateTime();
                DateTime begin = FS.FrameWork.Function.NConvert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime end = FS.FrameWork.Function.NConvert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59");
                if (isInpatient)
                {
                    returnValue = empowerFeeManager.QueryIsDayBalance(feeOperCode, begin.ToString(), end.ToString());
                }
                if (returnValue)
                {
                    //�ս��������շ�
                    if (canFeeType == "1")
                    {
                        errTxt = "�ս�󲻿������շ�!";
                        return false;
                    }
                    //�ս��ֻ�в�����Ȩ��ſ��շ�
                    if (canFeeType == "2")
                    {
                        //�Ƿ���Ȩ
                        if (empowerFeeManager.QueryIsEmpower(feeOperCode))
                        {
                            return true;
                        }
                        else
                        {
                            errTxt = "�ս��û�о�����Ȩ�����շѣ�";
                            return false;
                        }
                    }

                }
            }

            return true;
        }


        #endregion

        #region ��Ʊ�Ź��� {95BEABF4-8309-4d5d-9523-52288F9154BB}
        /// <summary>
        /// �Ѳ��ã����ò���
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        private bool isTempInvoice = false;

        /// <summary>
        /// �Ƿ�ȡ��ʱ��Ʊ��
        /// </summary>
        public bool IsTempInvoice
        {
            get
            {
                return this.isTempInvoice;
            }
            set
            {
                this.isTempInvoice = value;
            }
        }


        /// <summary>
        /// ��÷�Ʊ��
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="invoiceNO">��Ʊ������ˮ��</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ʧ�� 1 �ɹ�!</returns>
        public int GetInvoiceNO(string invoiceType, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            FS.HISFC.Models.Base.Employee oper = this.inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            return GetInvoiceNO(oper, invoiceType, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// ��÷�Ʊ��
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        /// <param name="oper">����Ա������Ϣ</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="invoiceNO">��Ʊ������ˮ��</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ʧ�� 1 �ɹ�!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceType, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            return GetInvoiceNO(oper, invoiceType, false, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// ��÷�Ʊ��
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        /// <param name="oper">����Ա������Ϣ</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="isTempInvoice">�Ƿ�ȡ��ʱ��Ʊ��</param>
        /// <param name="invoiceNO">��Ʊ������ˮ��</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ʧ�� 1 �ɹ�!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceType, bool isTempInvoice, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            /*{95BEABF4-8309-4d5d-9523-52288F9154BB}-By Maokb -20101206
          * ���´���Ʊ�Ź��򣬰�������סԺͳһһ�ַ�ʽ
          * 1����Ʊ��ˮ����ϵ�кţ�Ψһ��ʾһ�ŷ�Ʊ�������ظ���ԭ��������ظ���
          * 2����Ʊ���ô������ʵ�ʷ�Ʊ�ţ�����ʵ�ʷ�Ʊ�ű����ڷ�Ʊ���÷�Χ�ڡ�
          * 3����Ʊ������ͷ��ʵ�ʷ�Ʊ�Ż���Ӣ����ͷ��
          * 4������סԺ�Һ�Ԥ������ͬһ��ȡ��Ʊ����
          * 5������ȡ��ʱ��Ʊ�Ŵ�������Ԥ�����������㣩
          * 6��MZINVOICE ���� IDΪԱ����ţ�NameΪ��ʽ��Ʊ�ţ�MemoΪ���Է�Ʊ�ţ���Ʊ��ˮ�ţ�
          * */

            //�����ȡ��ʱ��Ʊ��
            if (isTempInvoice)
            {
                invoiceNO = this.GetNewSysInvoice(invoiceType);
                invoiceNO = invoiceNO.PadLeft(12, '0');
                realInvoiceNO = invoiceNO;
                return 1;
            }

            #region ȡ��ʽ��Ʊ�ź�ʵ�ʷ�Ʊ��

            //ȡ��Ʊ�Ź���
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            //ϵͳ��ˮ����ƹ���;          
            //"1"��Ʊ���� YYMMDDXXABCD�ķ�ʽ��ÿ������ÿ��һ����0��ʼ����ˮ�ţ�ʵ�ʷ�Ʊ�Ų������õķ�ʽ��         
            //"2"��Ʊ����ʵ�ʷ�Ʊ�ţ���������·�Ʊ���п����غţ������飻ʵ�ʷ�Ʊ�Ų������õķ�ʽ��      

            //"3"��Ʊ���� YYMMDDXXABCD�ķ�ʽ��ÿ������ÿ��һ����0��ʼ����ˮ�ţ�ʵ�ʷ�Ʊ�Ų��������õķ�ʽ���������롣      
            //"4"��Ʊ����ʵ�ʷ�Ʊ�ţ���������·�Ʊ���п����غţ������飻ʵ�ʷ�Ʊ�Ų��������õķ�ʽ���������롣



            //�����Ԥ�ȷ��䷢Ʊ��ʽ
            bool isDistributeRealInvoice = (getInvoiceType == "1" || getInvoiceType == "2") ? true : false;
            NeuObject objInvoice = new NeuObject();

            #region ��ȡʵ�ʷ�Ʊ��

            if (isDistributeRealInvoice)
            {
                //�ӷ��䷢Ʊ�л�ȡʵ�ʷ�Ʊ����
                realInvoiceNO = this.GetNextInvoiceNO(invoiceType, oper);
                if (string.IsNullOrEmpty(realInvoiceNO))
                {
                    errText = this.Err;
                    return -1;
                }
            }
            else
            {
                //�ӳ������л�ȡʵ�ʷ�Ʊ����
                objInvoice = managerIntegrate.GetConstansObj("INVOICE-" + invoiceType, oper.ID);

                //û��ά����Ʊ��ʼ��
                if (objInvoice == null || string.IsNullOrEmpty(objInvoice.ID.Trim()))
                {

                    if (FS.FrameWork.Management.PublicTrans.Trans == null)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    }
                    managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = oper.ID;
                    con.Name = "1";//Ĭ�ϴ�1��ʼ
                    con.Memo = "1";//Ĭ�ϴ�1��ʼ
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID;
                    con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                    int iReturn = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    realInvoiceNO = "1";
                }
                else
                {
                    realInvoiceNO = objInvoice.Name;
                }
            }
            #endregion

            #region ��ȡϵͳ��ˮ��
            switch (getInvoiceType)
            {

                case "1":
                case "3"://��Ʊ���� YYMMDDXXABCD�ķ�ʽ��ÿ������ÿ��һ����0��ʼ����ˮ��
                    #region YYMMDDXXABCD

                    objInvoice = managerIntegrate.GetConstansObj("INVOICE-" + invoiceType, oper.ID);
                    if (objInvoice == null)
                    {
                        errText = "��÷�Ʊ��Ϣ����!" + managerIntegrate.Err;

                        return -1;
                    }

                    // ��ȡ�շ�Աά����д���룬���û�о��Զ�����һ��
                    #region ��Ʊ�Զ�����

                    string tmpOperCode = string.Empty;
                    NeuObject objCashier = managerIntegrate.GetConstansObj("InvoiceUserCode", oper.ID);
                    if (objCashier != null && !string.IsNullOrEmpty(objCashier.Name))
                    {
                        tmpOperCode = objCashier.Name;
                    }
                    else//�Զ�����һ��
                    {
                        FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
                        if (this.trans == null)
                        {
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            constManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                        else
                        {
                            managerIntegrate.SetTrans(this.trans);
                            constManager.SetTrans(this.trans);
                        }

                        tmpOperCode = constManager.GetMaxName("InvoiceUserCode");

                        int num = 0;
                        try
                        {
                            num = Convert.ToInt32(tmpOperCode, 10);
                        }
                        catch
                        {
                            num = 99;
                        }

                        if (num >= 99)
                        {
                            tmpOperCode = "00";
                        }
                        else
                        {
                            tmpOperCode = (num + 1).ToString().PadLeft(2, '0');
                        }
                        FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                        con.ID = oper.ID;
                        con.Name = tmpOperCode;
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        //��������
                        if (managerIntegrate.InsertConstant("InvoiceUserCode", con) < 0)
                        {
                            if (this.trans == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            errText = "�����շ�Ա��Ʊ�Զ�����ʧ�ܣ�" + managerIntegrate.Err;
                            return -1;
                        }

                        //����ж��Ƿ������շ�Ա���ڴ˺���
                        if (constManager.IsExistName("InvoiceUserCode", oper.ID, tmpOperCode))
                        {
                            if (this.trans == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            errText = "�����շ�Ա��Ʊ�Զ�����ʧ�ܣ��Ѵ��ڴ˺��룬���˳����棡";
                            return -1;
                        }
                        if (this.trans == null)
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                    }

                    if (tmpOperCode.Length < 2)
                    {
                        tmpOperCode = tmpOperCode.PadLeft(2, '0');
                    }

                    #endregion

                    //û��ά����Ʊ��ʼ��
                    if (objInvoice == null || string.IsNullOrEmpty(objInvoice.ID.Trim()))
                    {
                        if (this.trans == null)
                        {
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                        else
                        {
                            managerIntegrate.SetTrans(this.trans);
                            inpatientManager.SetTrans(this.trans);
                        }
                        FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                        con.ID = oper.ID;
                        con.Name = realInvoiceNO;//Ĭ�ϴ�1��ʼ
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                        con.Memo = con.OperEnvironment.OperTime.ToString("yyMMdd") + tmpOperCode + "0001";
                        int iReturn = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (iReturn <= 0)
                        {
                            if (this.trans == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;
                            return -1;
                        }
                        if (this.trans == null)
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        invoiceNO = con.Memo;
                    }
                    else
                    {
                        invoiceNO = objInvoice.Memo;
                        DateTime now = inpatientManager.GetDateTimeFromSysDateTime();
                        if (invoiceNO == null || invoiceNO == string.Empty || invoiceNO.Length < 6)
                        {
                            invoiceNO = now.ToString("yyMMdd") + tmpOperCode + "0001";
                        }
                        try
                        {
                            DateTime dtInvoice = new DateTime(2000 + FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(0, 2)), FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(2, 2)), FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(4, 2)));
                            if (now.Date > dtInvoice)
                            {
                                invoiceNO = now.ToString("yyMMdd") + tmpOperCode + "0001";
                            }
                        }
                        catch (Exception ex)
                        {
                            invoiceNO = now.ToString("yyMMdd") + tmpOperCode + "0001";//��Ҫ�����ˣ�ֱ�ӱ�ɽ������ˮ��
                        }

                    }

                    #endregion
                    break;
                case "2"://��ͨģʽ  
                case "4":
                    invoiceNO = realInvoiceNO;
                    break;
                default:
                    invoiceNO = realInvoiceNO;
                    break;

            }
            #endregion

            #endregion

            return 1;
        }

        /// <summary>
        /// ��÷�Ʊ��
        /// </summary>
        /// <param name="oper">��Ա������Ϣ</param>
        /// <param name="ctrl">���Ʋ�����</param>
        /// <param name="invoiceNO">��Ʊ���Ժ�</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="t">���ݿ�����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ʧ�� 1 �ɹ�!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, ref string invoiceNO, ref string realInvoiceNO, ref string errText, FS.FrameWork.Management.Transaction trans)
        {
            string invoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            NeuObject objInvoice = new NeuObject();

            switch (invoiceType)
            {
                case "2"://��ͨģʽ

                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);

                    //û��ά����Ʊ��ʼ��
                    if (objInvoice == null || objInvoice.ID == null || objInvoice.ID == string.Empty)
                    {
                        if (FS.FrameWork.Management.PublicTrans.Trans == null)
                        {
                            //trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                            //trans.BeginTransaction();
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        }
                        managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                        con.ID = oper.ID;
                        con.Name = "1";//Ĭ�ϴ�1��ʼ
                        con.Memo = "1";//Ĭ�ϴ�1��ʼ
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //invoiceNO = objInvoice.Name;
                        //realInvoiceNO = objInvoice.Name;
                        //string invoiceNOTemp = this.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                        //{BCB3B25A-69CD-4dfe-84D2-21D2239A7467}
                        if (FS.FrameWork.Management.PublicTrans.Trans == null)
                        {
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }

                        string invoiceNOTemp = this.GetNewInvoiceNO("C");
                        //{BCB3B25A-69CD-4dfe-84D2-21D2239A7467}
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        if (invoiceNOTemp == null || invoiceNOTemp == string.Empty)
                        {
                            errText = "��÷�Ʊʧ��!" + this.Err;

                            return -1;
                        }

                        invoiceNO = invoiceNOTemp;
                        realInvoiceNO = objInvoice.Name;
                    }
                    else
                    {
                        //string invoiceNOTemp = this.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                        string invoiceNOTemp = this.GetNewInvoiceNO("C");
                        if (invoiceNOTemp == null || invoiceNOTemp == string.Empty)
                        {
                            errText = "��÷�Ʊʧ��!" + this.Err;

                            return -1;
                        }

                        invoiceNO = invoiceNOTemp;
                        realInvoiceNO = objInvoice.Name;
                    }

                    break;

                case "0": //��ҽģʽ
                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);

                    //û��ά����Ʊ��ʼ��
                    if (objInvoice == null || objInvoice.ID == null || objInvoice.ID == string.Empty)
                    {
                        //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                        //trans.BeginTransaction(); by niuxy

                        if (FS.FrameWork.Management.PublicTrans.Trans == null)
                        {
                            //trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                            //trans.BeginTransaction();
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        }
                        managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                        con.ID = oper.ID;
                        con.Name = "1";//Ĭ�ϴ�1��ʼ
                        con.Memo = "1";//Ĭ�ϴ�1��ʼ
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        invoiceNO = objInvoice.Name;
                        realInvoiceNO = objInvoice.Name;
                    }
                    else
                    {
                        invoiceNO = objInvoice.Name.PadLeft(12, '0');
                        realInvoiceNO = NConvert.ToInt32(objInvoice.Name).ToString();
                    }
                    break;
                case "1": //��ɽҽģʽ!

                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);
                    if (objInvoice == null)
                    {
                        errText = "��÷�Ʊ��Ϣ����!" + managerIntegrate.Err;

                        return -1;
                    }

                    Employee empl = managerIntegrate.GetEmployeeInfo(oper.ID);
                    if (empl == null)
                    {
                        errText = "��ò���Ա������Ϣ����!" + managerIntegrate.Err;

                        return -1;
                    }

                    string tmpOperCode = empl.UserCode;
                    oper.UserCode = empl.UserCode;

                    if (oper == null || oper.UserCode == null || oper.UserCode == string.Empty || oper.UserCode.Length > 2)
                    {
                        tmpOperCode = "XX";
                    }
                    else
                    {
                        tmpOperCode = empl.UserCode;
                    }

                    //û��ά����Ʊ��ʼ��
                    if (objInvoice == null || objInvoice.ID == null || objInvoice.ID == string.Empty)
                    {
                        //FS.FrameWork.Management.Transaction 
                        if (FS.FrameWork.Management.PublicTrans.Trans == null)
                        {
                            //trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                            //trans.BeginTransaction();
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        }
                        managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                        con.ID = oper.ID;
                        con.Name = "1";//Ĭ�ϴ�1��ʼ
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                        con.Memo = con.OperEnvironment.OperTime.Year.ToString().Substring(2, 2) + con.OperEnvironment.OperTime.Month.ToString().PadLeft(2, '0') +
                            con.OperEnvironment.OperTime.Day.ToString().PadLeft(2, '0') + tmpOperCode + "0001";
                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        invoiceNO = con.Memo;
                    }
                    else
                    {
                        invoiceNO = objInvoice.Memo;
                        DateTime now = inpatientManager.GetDateTimeFromSysDateTime();
                        if (invoiceNO == null || invoiceNO == string.Empty)
                        {
                            invoiceNO = now.Year.ToString().Substring(2, 2) + now.Month.ToString().PadLeft(2, '0') +
                                now.Day.ToString().PadLeft(2, '0') + tmpOperCode + "0001";
                        }
                        try
                        {
                            DateTime dtInvoice = new DateTime(2000 + FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(0, 2)), FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(2, 2)), FS.FrameWork.Function.NConvert.ToInt32(invoiceNO.Substring(4, 2)));
                            if (now.Date > dtInvoice)
                            {
                                invoiceNO = now.Year.ToString().Substring(2, 2) + now.Month.ToString().PadLeft(2, '0') +
                                    now.Day.ToString().PadLeft(2, '0') + tmpOperCode + "0001";
                            }
                        }
                        catch (Exception ex)
                        {
                            errText = "��Ʊת�����ڳ���!" + ex.Message;
                            return -1;
                        }

                        realInvoiceNO = objInvoice.Name;
                    }

                    break;
            }

            return 1;
        }


        #region ��Ʊ���ߺ�
        /// <summary>
        /// ��Ʊ���ߺ�
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="invoiceCount"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int UseInvoiceNO(string invoiceType, int invoiceCount, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            //ȡ��Ʊ�Ź���
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            return UseInvoiceNO(oper, getInvoiceType, invoiceType, invoiceCount, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// ��Ʊ���ߺ�
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceStytle">ȡ��Ʊ��ʽ</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="invoiceCount"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int UseInvoiceNO(string invoiceStytle, string invoiceType, int invoiceCount, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            return this.UseInvoiceNO(oper, invoiceStytle, invoiceType, invoiceCount, ref invoiceNO, ref realInvoiceNO, ref errText);
        }

        /// <summary>
        /// ��Ʊ���ߺ�
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="oper">�շ�Ա</param>
        /// <param name="invoiceStytle">ȡ��Ʊ��ʽ</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="invoiceCount"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int UseInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceStytle, string invoiceType, int invoiceCount, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            //string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
            //string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;
            string lastRealInvoice = "";
            int returnValue = 0;
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            switch (invoiceStytle)
            {
                case "1":
                    //ʵ�ʷ�Ʊ����
                    for (int i = 0; i < invoiceCount; i++)
                    {
                        lastRealInvoice = this.GetNewInvoiceNO(invoiceType, oper);
                    }

                    if (string.IsNullOrEmpty(lastRealInvoice))
                    {
                        errText = inpatientManager.Err;
                        return -1;
                    }

                    if (lastRealInvoice != realInvoiceNO.PadLeft(lastRealInvoice.Length, '0'))
                    {
                        errText = "ʵ�ʷ�Ʊ������!";
                        return -1;
                    }
                    //��Ʊ��ˮ��
                    //���·�Ʊ��ˮ��    
                    int len = invoiceNO.Length;
                    //��÷�Ʊ���˺���λ���ַ���,����Ʊ�����ں��տ�Ա,��ʽΪyymmddxx(��,��,��,����Ա2λ����)
                    string orgInvoice = invoiceNO.Substring(0, len - 4);
                    //��ú���λ��Ʊ���к�
                    string addInvoice = invoiceNO.Substring(len - 4, 4);

                    //�����һ�ŷ�Ʊ��
                    string nextInvoiceNO = orgInvoice + (NConvert.ToInt32(addInvoice) + 1).ToString().PadLeft(4, '0');

                    con.ID = oper.ID; // this.outpatientManager.Operator.ID;
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);// (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = nextInvoiceNO;

                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue <= 0)
                    {
                        errText = "���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                        return -1;
                    }
                    break;
                case "2":
                    //ʵ�ʷ�Ʊ��
                    for (int i = 0; i < invoiceCount; i++)
                    {
                        lastRealInvoice = this.GetNewInvoiceNO(invoiceType, oper);
                    }
                    con.ID = oper.ID;
                    //���·�Ʊ��ˮ��                    
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);//(NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
                case "3":
                case "4":
                default:
                    //ʵ�ʷ�Ʊ��
                    //���·�Ʊ��ˮ��   
                    con.ID = oper.ID;
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);// (NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
            }

            return 1;
        }

        #endregion

        #region ȡʵ�ʷ�Ʊ��--  ����ʱ�ã��Զ���Ʊ��+1  {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// <summary>
        /// ȡʵ�ʷ�Ʊ��--  ����ʱ�ã��Զ���Ʊ��+1
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <returns></returns>
        public string GetNewInvoiceNO(string invoiceType)
        {
            FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            return GetNewInvoiceNO(invoiceType, oper);
        }
        /// <summary>
        /// ȡʵ�ʷ�Ʊ��--  ����ʱ�ã��Զ���Ʊ��+1
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <returns></returns>
        public string GetNewInvoiceNO(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        {
            int leftQty = 0;//��Ʊʣ����Ŀ
            int alarmQty = 0;//��ƱԤ����Ŀ
            string finGroupID = string.Empty;//��Ʊ�����
            string newInvoiceNO = string.Empty;//��õķ�Ʊ��

            alarmQty = FS.FrameWork.Function.NConvert.ToInt32(controlManager.QueryControlerInfo("100002"));

            finGroupID = inpatientManager.GetFinGroupInfoByOperCode(oper.ID).ID;

            if (finGroupID == null || finGroupID == string.Empty)
            {
                finGroupID = " ";
            }

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeState = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

            if (this.trans != null)
            {
                feeCodeState.SetTrans(this.trans);
            }

            newInvoiceNO = inpatientManager.GetNewInvoiceNO(oper.ID, invoiceType, alarmQty, ref leftQty, finGroupID);

            if (newInvoiceNO == null || newInvoiceNO == string.Empty)
            {
                this.SetDB(inpatientManager);

                return null;
            }

            return newInvoiceNO;
        }
        #endregion



        /// <summary>
        /// ȡʵ�ʷ�Ʊ��--������ʾ�ã���ʾ��ǰ��һ����Ʊ��
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <returns></returns>
        public string GetNextInvoiceNO(string invoiceType)
        {
            FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            return GetNextInvoiceNO(invoiceType, oper);
        }
        /// <summary>
        /// ȡʵ�ʷ�Ʊ��--������ʾ�ã���ʾ��ǰ��һ����Ʊ�ţ�ֻ��ȡ�µķ�Ʊ���룬�����·�Ʊ����
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string GetNextInvoiceNO(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        {
            int leftQty = 0;//��Ʊʣ����Ŀ
            int alarmQty = 0;//��ƱԤ����Ŀ
            string finGroupID = string.Empty;//��Ʊ�����
            string newInvoiceNO = string.Empty;//��õķ�Ʊ��

            alarmQty = FS.FrameWork.Function.NConvert.ToInt32(controlManager.QueryControlerInfo("100002"));

            finGroupID = inpatientManager.GetFinGroupInfoByOperCode(oper.ID).ID;

            if (finGroupID == null || finGroupID == string.Empty)
            {
                finGroupID = " ";
            }

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeState = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

            if (this.trans != null)
            {
                feeCodeState.SetTrans(this.trans);
            }

            newInvoiceNO = inpatientManager.GetNextInvoiceNO(oper.ID, invoiceType, alarmQty, ref leftQty, finGroupID);

            if (newInvoiceNO == null || newInvoiceNO == string.Empty)
            {
                this.SetDB(inpatientManager);
                this.Err = inpatientManager.Err;
                return null;
            }

            if (leftQty < alarmQty)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("ʣ�෢Ʊ") + leftQty.ToString() + Language.Msg("��,�벹�췢Ʊ!"));
            }

            return newInvoiceNO;
        }

        #region ����
        ///// <summary>
        ///// ����µķ�Ʊ�� -ֻ��ȡ�µķ�Ʊ���룬�����·�Ʊ����
        ///// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        ///// </summary>
        ///// <param name="invoiceType"></param>
        ///// <param name="oper"></param>
        ///// <returns></returns>
        //public string GetNextInvoiceNONotUpdate(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        //{
        //    int leftQty = 0;//��Ʊʣ����Ŀ
        //    int alarmQty = 0;//��ƱԤ����Ŀ
        //    string finGroupID = string.Empty;//��Ʊ�����
        //    string newInvoiceNO = string.Empty;//��õķ�Ʊ��

        //    alarmQty = FS.FrameWork.Function.NConvert.ToInt32(controlManager.QueryControlerInfo("100002"));

        //    finGroupID = inpatientManager.GetFinGroupInfoByOperCode(oper.ID).ID;

        //    if (finGroupID == null || finGroupID == string.Empty)
        //    {
        //        finGroupID = " ";
        //    }

        //    FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeState = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

        //    if (this.trans != null)
        //    {
        //        feeCodeState.SetTrans(this.trans);
        //    }

        //    newInvoiceNO = inpatientManager.GetNextInvoiceNONotUpdate(oper.ID, invoiceType, alarmQty, ref leftQty, finGroupID);

        //    if (newInvoiceNO == null || newInvoiceNO == string.Empty)
        //    {
        //        this.SetDB(inpatientManager);
        //        this.Err = inpatientManager.Err;
        //        return null;
        //    }

        //    if (leftQty < alarmQty)
        //    {
        //        System.Windows.Forms.MessageBox.Show(Language.Msg("ʣ�෢Ʊ") + leftQty.ToString() + Language.Msg("��,�벹�췢Ʊ!"));
        //    }

        //    return newInvoiceNO;
        //}
        #endregion

        /// <summary>
        /// ����ʵ�ʷ�Ʊ��
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <param name="realInvoiceNo">��һ��Ʊ��</param>
        /// <returns></returns>
        public int UpdateNextInvoiceNO(string strEmpCode, string invoiceType, string realInvoiceNo)
        {
            int iRes = invoiceServiceManager.UpdateRealInvoiceNo(strEmpCode, invoiceType, realInvoiceNo);
            if (iRes <= 0)
            {
                this.Err = invoiceServiceManager.Err;
            }

            return iRes;
        }


        /// <summary>
        /// ����ʵ�ʷ�Ʊ��
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType">��Ʊ����</param>
        /// <param name="invoiceNO">��һ���Ժ�</param>
        /// <param name="realInvoiceNo">��һ��Ʊ��</param>
        /// <returns></returns>
        public int UpdateNextInvoiceNO(string strEmpCode, string invoiceType, string invoiceNO, string realInvoiceNo)
        {
            //ȡ��Ʊ�Ź���
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
            //���ʵ�ʺź͵��Ժ�һ�µ�

            int iRes = invoiceServiceManager.UpdateRealInvoiceNo(strEmpCode, invoiceType, realInvoiceNo);
            if (iRes <= 0)
            {
                this.Err = invoiceServiceManager.Err;
                //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
                return iRes;
            }

            switch (getInvoiceType)
            {
                //ʵ�ʺź͵��Ժ�һֱ�������
                case "2":
                case "4":
                    break;
                default:
                    iRes = invoiceServiceManager.UpdateNextInvoliceNo(strEmpCode, "INVOICE-" + invoiceType, invoiceNO);
                    break;
            }

            return iRes;
        }

        /// <summary>
        /// ��ȡ���Է�Ʊ������ĸ���ʱ�䣨�������ͣ�
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="recentUpdate">�������ʱ��</param>
        /// <returns></returns>
        public int GetRecentUpdateInvoiceTime(string emplCode, string invoiceType, ref DateTime recentUpdate)
        {
            this.SetDB(this.invoiceServiceManager);
            return this.invoiceServiceManager.GetRecentUpdateInvoiceTime(emplCode, invoiceType, ref recentUpdate);
        }

        /// <summary>
        /// ���µ��Է�Ʊ�� ��������(����ǰ�жϷ�Ʊ�Ƿ���ʹ��)
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public int UpdateNextInvoliceNo(string emplCode, string invoiceType, string invoiceNo)
        {
            this.SetDB(this.invoiceServiceManager);
            return this.invoiceServiceManager.UpdateNextInvoliceNo(emplCode, invoiceType, invoiceNo);
        }

        /// <summary>
        /// ��ȡ�µķ�Ʊϵ�кţ���ˮ��+1
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public string GetNewSysInvoice(string invoiceType)
        {
            //���ݷ�Ʊ���ͻ�ȡ����ˮ�ţ�ͬʱ��ˮ�ż�1
            string rtnString = "";
            switch (invoiceType)
            {
                case "A":
                    rtnString = this.seqManager.GetNewZHInvoiceNO();
                    break;

                case "C":
                    rtnString = this.seqManager.GetNewMzInvoiceNO();
                    break;
                case "R":
                    rtnString = this.seqManager.GetNewGHInvoiceNO();
                    break;
                case "P":
                    rtnString = this.seqManager.GetNewYJInvoiceNO();
                    break;
                case "I":
                    rtnString = this.seqManager.GetNewJSInvoiceNO();
                    break;
                default:
                    return "-1";
            }
            return rtnString;
        }


        /// <summary>
        /// �����һ�ŷ�Ʊ��
        /// </summary>
        /// <param name="invoiceType">��÷�Ʊ�ŷ�ʽ</param>
        /// <param name="invoiceNO">��ǰ���Է�Ʊ��</param>
        /// <param name="realInvoiceNO">��ǰʵ�ʷ�Ʊ��</param>
        /// <param name="nextInvoiceNO">��һ�ŵ��Է�Ʊ��</param>
        /// <param name="nextRealInvoiceNO">��һ��ʵ�ʷ�Ʊ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ���� 1 ��ȷ</returns>
        public int GetNextInvoiceNO(string invoiceType, string invoiceNO, string realInvoiceNO, ref string nextInvoiceNO, ref string nextRealInvoiceNO, ref string errText)
        {
            return GetNextInvoiceNO(invoiceType, invoiceNO, realInvoiceNO, ref nextInvoiceNO, ref nextRealInvoiceNO, 1, ref errText);
        }

        /// <summary>
        /// �����N�ŷ�Ʊ��
        /// </summary>
        /// <param name="invoiceType">��÷�Ʊ�ŷ�ʽ</param>
        /// <param name="invoiceNO">��ǰ���Է�Ʊ��</param>
        /// <param name="realInvoiceNO">��ǰʵ�ʷ�Ʊ��</param>
        /// <param name="nextInvoiceNO">��һ�ŵ��Է�Ʊ��</param>
        /// <param name="nextRealInvoiceNO">��һ��ʵ�ʷ�Ʊ��</param>
        /// <param name="count">�¼��ŷ�Ʊ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ���� 1 ��ȷ</returns>
        public int GetNextInvoiceNO(string invoiceType, string invoiceNO, string realInvoiceNO, ref string nextInvoiceNO, ref string nextRealInvoiceNO, int count, ref string errText)
        {
            switch (invoiceType)
            {
                case "2"://��ͨģʽ

                    string invoiceNOTemp = string.Empty;

                    for (int i = 0; i < count; i++)
                    {
                        //invoiceNOTemp = this.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                        invoiceNOTemp = this.GetNewInvoiceNO("C");
                        if (invoiceNOTemp == null || invoiceNOTemp == string.Empty)
                        {
                            errText = "��÷�Ʊʧ��!";

                            return -1;
                        }
                    }

                    if (count == 0)
                    {
                        invoiceNOTemp = invoiceNO;
                    }

                    nextInvoiceNO = invoiceNOTemp;
                    nextRealInvoiceNO = nextRealInvoiceNO = (NConvert.ToInt32(realInvoiceNO) + count).ToString();

                    break;

                case "0"://��ҽ��ʽ
                    //��ҽ��ʽ�ķ�Ʊ��,Ϊ������,����ֱ������1����
                    nextInvoiceNO = ((NConvert.ToInt32(invoiceNO) + count).ToString()).PadLeft(12, '0');
                    //��ҽ��ʽ��ʵ�ʷ�Ʊ��,�����Ժ�һ��,ͬ������
                    nextRealInvoiceNO = NConvert.ToInt32(nextInvoiceNO).ToString();

                    break;
                case "1"://��ɽ��ʽ
                    //��Ϊ��ɽ��ʽ�ķ�Ʊ�����λ������Ʊ�����к�,����һ��Ҫ����4λ,���������λΪ���ֲ��ǺϷ���Ʊ
                    if (invoiceNO.Length < 4)
                    {
                        errText = "��Ʊ�ų��Ȳ�����!";

                        return -1;
                    }
                    //�����ɽ��Ʊ�ĳ���
                    int len = invoiceNO.Length;
                    //��÷�Ʊ���˺���λ���ַ���,����Ʊ�����ں��տ�Ա,��ʽΪyymmddxx(��,��,��,����Ա2λ����)
                    string orgInvoice = invoiceNO.Substring(0, len - 4);
                    //��ú���λ��Ʊ���к�
                    string addInvoice = invoiceNO.Substring(len - 4, 4);

                    //�����һ�ŷ�Ʊ��
                    nextInvoiceNO = orgInvoice + (NConvert.ToInt32(addInvoice) + count).ToString().PadLeft(4, '0');
                    //ʵ�ʷ�Ʊ��Ϊ��ͷ+����,���ֲ���ֱ������1����
                    string regex = @"(\d+)";
                    Match mstr = Regex.Match(realInvoiceNO, regex);
                    string realInvoiceNONum = mstr.Groups[1].Value;
                    nextRealInvoiceNO = (NConvert.ToInt32(realInvoiceNONum) + count).ToString();

                    if (nextRealInvoiceNO.Length < realInvoiceNO.Length)
                    {
                        nextRealInvoiceNO = realInvoiceNO.Substring(0, realInvoiceNO.Length - nextRealInvoiceNO.Length) + nextRealInvoiceNO;
                    }

                    break;
            }

            return 1;
        }

        /// <summary>
        /// ��ѡ��ϵͳ��Ʊʱ��,�ش����,ֻ���·�Ʊ��ӡ��
        /// </summary>
        /// <param name="invoiceNO">��ǰ��Ʊ��</param>
        /// <param name="realInvoiceNO">��ǰ��Ʊ��ӡ��</param>
        /// <param name="errText">�������</param>
        /// <returns>�ɹ�1  ʧ�� -1</returns>
        public int UpdateOnlyRealInvoiceNO(string invoiceNO, string realInvoiceNO, ref string errText)
        {
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            con.ID = outpatientManager.Operator.ID;
            realInvoiceNO = (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
            con.Name = realInvoiceNO;
            con.Memo = invoiceNO;

            con.IsValid = true;
            con.OperEnvironment.ID = outpatientManager.Operator.ID;
            con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
            int returnValue = managerIntegrate.UpdateConstant("MZINVOICE", con);
            if (returnValue <= 0)
            {
                errText = "���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// ��÷�Ʊ��
        /// </summary>
        /// <param name="invoiceNO">��Ʊ���Ժ�</param>
        /// <param name="realInvoiceNO">ʵ�ʷ�Ʊ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>-1 ʧ�� 1 �ɹ�!</returns>
        public int UpdateInvoiceNO(string invoiceNO, string realInvoiceNO, ref string errText)
        {
            string invoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            int returnValue = 0;
            string nextInvoiceNO = string.Empty;
            string nextRealInvoiceNO = string.Empty;

            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            con.ID = outpatientManager.Operator.ID;
            returnValue = this.GetNextInvoiceNO(invoiceType, invoiceNO, realInvoiceNO, ref nextInvoiceNO, ref nextRealInvoiceNO, ref errText);
            if (returnValue < 0)
            {
                return -1;
            }

            if (invoiceType == "1")
            {
                con.Name = nextRealInvoiceNO;
                con.Memo = nextInvoiceNO;
            }
            else if (invoiceType == "2")
            {
                con.Name = nextRealInvoiceNO;
                con.Memo = nextInvoiceNO;
            }
            else
            {
                con.Name = nextInvoiceNO;
                con.Memo = nextRealInvoiceNO;
            }

            con.IsValid = true;
            con.OperEnvironment.ID = outpatientManager.Operator.ID;
            con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
            returnValue = managerIntegrate.UpdateConstant("MZINVOICE", con);
            if (returnValue <= 0)
            {
                errText = "���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err;

                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// ���淢Ʊ��չ��
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="invoiceType"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="invoiceHead"></param>
        /// <returns></returns>
        public int InsertInvoiceExtend(string invoiceNO, string invoiceType, string realInvoiceNO, string invoiceHead)
        {
            FS.HISFC.Models.Fee.InvoiceExtend invoiceExtend = new FS.HISFC.Models.Fee.InvoiceExtend();
            invoiceExtend.ID = invoiceNO;
            invoiceExtend.InvoiceType = invoiceType;
            if (realInvoiceNO.Length < 8)
            {
                invoiceExtend.RealInvoiceNo = realInvoiceNO;
            }
            else
            {
                invoiceExtend.RealInvoiceNo = realInvoiceNO.Substring(realInvoiceNO.Length - 8);//�����8λ
            }
            invoiceExtend.InvvoiceHead = invoiceHead;
            invoiceExtend.InvoiceState = "1";//��Ч
            int i = this.invoiceServiceManager.InsertInvoiceExtend(invoiceExtend);
            if (i <= 0)
            {
                this.Err = this.invoiceServiceManager.Err;
            }
            return i;
        }


        ///// <summary>
        ///// ��Ʊ���л���ʾ
        ///// </summary>
        ///// <param name="operCode">����Ա</param>
        ///// <param name="invoiceType">��Ʊ����</param>
        ///// <param name="endInvoiceNO">�����շ����Ʊ��</param>
        ///// <param name="invoiceCount">��Ʊ����</param>
        ///// <param name="errText">������Ϣ</param>
        ///// <returns>-1ʧ�� 0��ʾ 1�ɹ�</returns>
        //public int InvoiceMessage(string operCode,string invoiceType,string endInvoiceNO,int invoiceCount,ref string errText)
        //{
        //    bool isMessage = controlParamIntegrate.GetControlParam<bool>(SysConst.Use_CutOverInvoiceNO_Mess,false,false);
        //    if(!isMessage) return 1;
        //    string resultValue = invoiceServiceManager.QueryUsedInvoiceNO(operCode,invoiceType);
        //    if (string.IsNullOrEmpty(resultValue)) 
        //    {
        //        resultValue = invoiceServiceManager.QueryNoUsedInvoiceNO(operCode, invoiceType);
        //        if (string.IsNullOrEmpty(resultValue))
        //        {
        //            errText = "�ò���Ա�����ڷ�Ʊ�������÷�Ʊ��";
        //            return -1;
        //        }
        //    }
        //    long invoiceNO = long.Parse(resultValue);
        //    invoiceNO += invoiceCount;
        //    if (invoiceNO == long.Parse(endInvoiceNO))
        //    {
        //        return 1;
        //    }
        //    MessageBox.Show("��Ʊ�Ŷ��л����뻻ֽ��");
        //    return 0;

        //}
        #endregion

        #region ����δ�۷���
        /// <summary>
        /// ����δ�۷���,����������
        /// 
        /// ��������
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="decUnFeeMoney"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int QueryUnFeeByCarNo(string cardNo, out decimal decUnFeeMoney, out string strMsg)
        {
            decUnFeeMoney = 0;
            strMsg = "";
            int iRes = 0;
            int iRegValidDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS, false, 10000);

            DateTime dtNow = registerManager.GetDateTimeFromSysDateTime();

            ArrayList arrRegInfo = registerManager.Query(cardNo, dtNow.AddDays(-iRegValidDays));

            FS.HISFC.Models.Registration.Register regInfo = null;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            for (int idx = arrRegInfo.Count - 1; idx >= 0; idx--)
            {
                regInfo = arrRegInfo[idx] as FS.HISFC.Models.Registration.Register;
                if (regInfo == null)
                {
                    continue;
                }

                ArrayList arlFeeItemList = outpatientManager.QueryChargedFeeItemListsByClinicNO(regInfo.ID);
                if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
                {
                    continue;
                }
                ArrayList arlFeeAll = this.ConvertFeeItemToSingle(regInfo, arlFeeItemList);
                if (arlFeeAll == null || arlFeeAll.Count <= 0)
                {
                    continue;
                }

                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.medcareInterfaceProxy.BeginTranscation();

                this.medcareInterfaceProxy.SetPactCode(regInfo.Pact.ID);
                this.medcareInterfaceProxy.IsLocalProcess = true;

                if (!this.medcareInterfaceProxy.IsInBlackList(regInfo))
                {
                    iRegValidDays = this.medcareInterfaceProxy.LocalBalanceOutpatient(regInfo, ref arlFeeItemList, null);
                    if (iRegValidDays <= 0)
                    {
                        strMsg = this.medcareInterfaceProxy.ErrMsg;
                        if (FS.FrameWork.Management.PublicTrans.Trans != null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        return iRegValidDays;
                    }
                }

                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in arlFeeItemList)
                {
                    decUnFeeMoney += item.FT.OwnCost - item.FT.RebateCost;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
        /// <summary>
        /// ���㴦�����ã���ҽ����ͬ��λ����
        /// ҽ��վ����
        /// 
        /// ���� С�ڵ��� 0 ����ʧ�ܣ�  ���� 1 ����ɹ��� ���� 2 ����ɹ��ұ�������
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="arlFeeItemList">������Ϣ</param>
        /// <param name="blnNeedAddup">�Ƿ���Ҫ�ۼ�</param>
        /// <param name="arlMoneyInfo">
        /// arlMoneyInfo[0] = �ۼ��ܽ��
        /// arlMoneyInfo[1] = �ۼƱ������
        /// arlMoneyInfo[2] = �ۼ��Էѽ��
        /// arlMoneyInfo[3] = �ۼ�ҽԺ������
        /// arlMoneyInfo[4] = �ܽ��
        /// arlMoneyInfo[5] = ҽ���������
        /// arlMoneyInfo[6] = �Էѽ��
        /// arlMoneyInfo[7] = ҽԺ������
        /// </param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int CalculatOrderFee(FS.HISFC.Models.Registration.Register regInfo, ArrayList arlFeeItemList, bool blnNeedAddup, ref decimal[] arlMoneyInfo, ref string strMsg)
        {
            //�˴����ٳ�ʼ������Ϊ��ʱ�� ���ܻ�����洫��ֵ
            if (arlMoneyInfo == null || arlMoneyInfo.Length < 8)
            {
                arlMoneyInfo = new decimal[8];
            }
            //arlMoneyInfo = new ArrayList();

            decimal decTotalMoney = 0;
            decimal decPubMoney = 0;
            decimal decOwnMoney = 0;
            decimal decRebateMoney = 0;
            strMsg = string.Empty;
            int iRes = 1;

            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID) || regInfo.Pact == null || string.IsNullOrEmpty(regInfo.Pact.ID))
            {
                strMsg = "��������";
                return -1;
            }

            if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
            {
                //strMsg = "�޷�����Ϣ";
                //return -1;
                //û�з�����Ϣʱ��ҲҪ��ѯ���µ����շ���Ϣ����ֹ�������ߺ���Ϣ��ʾ����
                blnNeedAddup = true;
            }

            if (blnNeedAddup)
            {
                List<Balance> lstInvoice = null;

                DateTime dtCurrent = this.outpatientManager.GetDateTimeFromSysDateTime();
                iRes = this.outpatientManager.QueryInvoiceInfoByPactAndDate(regInfo.PID.CardNO, dtCurrent.Date, dtCurrent, regInfo.Pact.ID, out lstInvoice);
                if (iRes <= 0)
                {
                    strMsg = this.outpatientManager.Err;
                    return -1;
                }

                if (lstInvoice != null && lstInvoice.Count > 0)
                {
                    foreach (Balance balance in lstInvoice)
                    {
                        decTotalMoney += balance.FT.TotCost;
                        decPubMoney += balance.FT.PubCost;
                        decOwnMoney += balance.FT.OwnCost + balance.FT.PayCost;
                        decRebateMoney += FS.FrameWork.Function.NConvert.ToDecimal(balance.User01);
                    }

                    arlMoneyInfo[0] = decTotalMoney;
                    arlMoneyInfo[1] = decPubMoney;
                    arlMoneyInfo[2] = decOwnMoney;
                    arlMoneyInfo[3] = decRebateMoney;
                }
                else
                {
                    arlMoneyInfo[0] = 0;
                    arlMoneyInfo[1] = 0;
                    arlMoneyInfo[2] = 0;
                    arlMoneyInfo[3] = 0;
                }
                //arlMoneyInfo[0] = decTotalMoney;
                //arlMoneyInfo[1] = decPubMoney;
                //arlMoneyInfo[2] = decOwnMoney;
                //arlMoneyInfo[3] = decRebateMoney;
            }
            else
            {
                //arlMoneyInfo.Add(0);
                //arlMoneyInfo.Add(0);
                //arlMoneyInfo.Add(0);
                //arlMoneyInfo.Add(0);
            }

            if (arlFeeItemList != null && arlFeeItemList.Count > 0)
            {
                arlFeeItemList = this.ConvertFeeItemToSingle(regInfo, arlFeeItemList);

                try
                {
                    this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.medcareInterfaceProxy.BeginTranscation();

                    this.medcareInterfaceProxy.SetPactCode(regInfo.Pact.ID);
                    this.medcareInterfaceProxy.IsLocalProcess = true;

                    ArrayList arlOther = null;
                    if (blnNeedAddup)
                    {
                        arlOther = new ArrayList();
                        arlOther.Add(arlMoneyInfo[1]);
                    }

                    bool blnInBlackList = this.controlParamIntegrate.GetControlParam<bool>("I00016", false, true);

                    if (blnInBlackList)
                    {
                        if (!this.medcareInterfaceProxy.IsInBlackList(regInfo))
                        {
                            iRes = this.medcareInterfaceProxy.LocalBalanceOutpatient(regInfo, ref arlFeeItemList, arlOther);
                        }
                    }
                    else
                    {
                        iRes = this.medcareInterfaceProxy.LocalBalanceOutpatient(regInfo, ref arlFeeItemList, arlOther);
                    }
                    if (iRes <= 0)
                    {
                        strMsg = this.medcareInterfaceProxy.ErrMsg;
                        return iRes;
                    }

                    if (iRes == 2)
                    {
                        strMsg = this.medcareInterfaceProxy.ErrMsg;
                    }


                    decTotalMoney = 0;
                    decPubMoney = 0;
                    decOwnMoney = 0;
                    decRebateMoney = 0;
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in arlFeeItemList)
                    {
                        decPubMoney += item.FT.PubCost;
                        decOwnMoney += item.FT.OwnCost + item.FT.PayCost;
                        decRebateMoney += item.FT.RebateCost;
                    }
                    decTotalMoney = decPubMoney + decOwnMoney;

                    arlMoneyInfo[4] = decTotalMoney;
                    arlMoneyInfo[5] = decPubMoney;
                    arlMoneyInfo[6] = decOwnMoney;
                    arlMoneyInfo[7] = decRebateMoney;
                }
                catch (Exception ex)
                {
                    strMsg = ex.Message + this.medcareInterfaceProxy.ErrMsg;
                    return -1;
                }
            }
            else
            {
                arlMoneyInfo[4] = 0;
                arlMoneyInfo[5] = 0;
                arlMoneyInfo[6] = 0;
                arlMoneyInfo[7] = 0;
            }

            return iRes;
        }

        /// <summary>
        /// ���㴦�����ã���ҽ����ͬ��λ����
        /// ҽ��վ����
        /// 
        /// ���� С�ڵ��� 0 ����ʧ�ܣ�  ���� 1 ����ɹ��� ���� 2 ����ɹ��ұ�������
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="arlFeeItemList">������Ϣ</param>
        /// <param name="blnNeedAddup">�Ƿ���Ҫ�ۼ�</param>
        /// <param name="arlMoneyInfo">
        /// arlMoneyInfo[0] = �ۼ��ܽ��
        /// arlMoneyInfo[1] = �ۼƱ������
        /// arlMoneyInfo[2] = �ۼ��Էѽ��
        /// arlMoneyInfo[3] = �ۼ�ҽԺ������
        /// arlMoneyInfo[4] = �ܽ��
        /// arlMoneyInfo[5] = ҽ���������
        /// arlMoneyInfo[6] = �Էѽ��
        /// arlMoneyInfo[7] = ҽԺ������
        /// </param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int BudgetFeeByPactUnit(FS.HISFC.Models.Registration.Register regInfo, ArrayList arlFeeItemList, bool blnNeedAddup, out ArrayList arlMoneyInfo, out string strMsg)
        {
            arlMoneyInfo = new ArrayList();

            decimal decTotalMoney = 0;
            decimal decPubMoney = 0;
            decimal decOwnMoney = 0;
            decimal decRebateMoney = 0;
            strMsg = string.Empty;
            int iRes = 1;

            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID) || regInfo.Pact == null || string.IsNullOrEmpty(regInfo.Pact.ID))
            {
                strMsg = "��������";
                return -1;
            }

            //FS.HISFC.Models.Registration.Register register = this.registerManager.GetByClinic(regInfo.ID);
            //if (register == null || string.IsNullOrEmpty(register.ID))
            //{
            //    strMsg = "��ǰ���߲����ڹҺż�¼";
            //    return -1;
            //}

            if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
            {
                strMsg = "�޷�����Ϣ";
                return -1;
            }

            if (blnNeedAddup)
            {
                List<Balance> lstInvoice = null;

                DateTime dtCurrent = this.outpatientManager.GetDateTimeFromSysDateTime();
                iRes = this.outpatientManager.QueryInvoiceInfoByPactAndDate(regInfo.PID.CardNO, dtCurrent.Date, dtCurrent, regInfo.Pact.ID, out lstInvoice);
                if (iRes <= 0)
                {
                    strMsg = this.outpatientManager.Err;
                    return -1;
                }

                if (lstInvoice != null && lstInvoice.Count > 0)
                {
                    foreach (Balance balance in lstInvoice)
                    {
                        decTotalMoney += balance.FT.TotCost;
                        decPubMoney += balance.FT.PubCost;
                        decOwnMoney += balance.FT.OwnCost + balance.FT.PayCost;
                        decRebateMoney += FS.FrameWork.Function.NConvert.ToDecimal(balance.User01);
                    }
                }

                arlMoneyInfo.Add(decTotalMoney);
                arlMoneyInfo.Add(decPubMoney);
                arlMoneyInfo.Add(decOwnMoney);
                arlMoneyInfo.Add(decRebateMoney);

            }
            else
            {
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
            }

            if (arlFeeItemList != null && arlFeeItemList.Count > 0)
            {
                arlFeeItemList = this.ConvertFeeItemToSingle(regInfo, arlFeeItemList);

                try
                {
                    this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.medcareInterfaceProxy.BeginTranscation();

                    this.medcareInterfaceProxy.SetPactCode(regInfo.Pact.ID);
                    this.medcareInterfaceProxy.IsLocalProcess = true;

                    ArrayList arlOther = null;
                    if (blnNeedAddup)
                    {
                        arlOther = new ArrayList();
                        arlOther.Add(arlMoneyInfo[1]);
                    }

                    bool blnInBlackList = this.controlParamIntegrate.GetControlParam<bool>("I00016", false, true);

                    if (blnInBlackList)
                    {
                        if (!this.medcareInterfaceProxy.IsInBlackList(regInfo))
                        {
                            iRes = this.medcareInterfaceProxy.LocalBalanceOutpatient(regInfo, ref arlFeeItemList, arlOther);
                        }
                    }
                    else
                    {
                        iRes = this.medcareInterfaceProxy.LocalBalanceOutpatient(regInfo, ref arlFeeItemList, arlOther);
                    }
                    if (iRes <= 0)
                    {
                        strMsg = this.medcareInterfaceProxy.ErrMsg;
                        return iRes;
                    }

                    if (iRes == 2)
                    {
                        strMsg = this.medcareInterfaceProxy.ErrMsg;
                    }


                    decTotalMoney = 0;
                    decPubMoney = 0;
                    decOwnMoney = 0;
                    decRebateMoney = 0;
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in arlFeeItemList)
                    {
                        decPubMoney += item.FT.PubCost;
                        decOwnMoney += item.FT.OwnCost + item.FT.PayCost;
                        decRebateMoney += item.FT.RebateCost;
                    }
                    decTotalMoney = decPubMoney + decOwnMoney;

                    arlMoneyInfo.Add(decTotalMoney);
                    arlMoneyInfo.Add(decPubMoney);
                    arlMoneyInfo.Add(decOwnMoney);
                    arlMoneyInfo.Add(decRebateMoney);
                }
                catch (Exception ex)
                {
                    strMsg = ex.Message + this.medcareInterfaceProxy.ErrMsg;
                    return -1;
                }
            }
            else
            {
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
                arlMoneyInfo.Add(0);
            }

            return iRes;
        }


        #region ת������Ϊ��ϸ

        private static string clinicCode = string.Empty;
        private static Dictionary<string, Undrug> dictionaryPatientItems = new Dictionary<string, Undrug>();

        /// <summary>
        /// ת������Ϊ��ϸ�������ã�
        /// </summary>
        /// <param name="arlFeeItemList"></param>
        /// <param name="reg"></param>
        /// <returns></returns>
        private ArrayList ConvertFeeItemToSingle(Register reg, ArrayList arlFeeItemList)
        {
            if ((arlFeeItemList == null) || (arlFeeItemList.Count <= 0))
            {
                return null;
            }

            //���dictionary������������ߵ���Ϣ���������Ϣ
            if (!clinicCode.Equals(reg.ID))
            {
                clinicCode = reg.ID;
                dictionaryPatientItems.Clear();
            }

            ArrayList feeDetailsTemp = new ArrayList();
            foreach (FeeItemList info in arlFeeItemList)
            {
                if (info.Item.ItemType == EnumItemType.UnDrug)
                {
                    Undrug itemTemp = null;
                    if (info.Item is Undrug)
                    {
                        itemTemp = info.Item as Undrug;
                    }

                    if (itemTemp == null || string.IsNullOrEmpty(itemTemp.UnitFlag))
                    {
                        if (dictionaryPatientItems.ContainsKey(info.Item.ID))
                        {
                            itemTemp = dictionaryPatientItems[info.Item.ID].Clone();
                        }
                        else
                        {
                            itemTemp = this.itemManager.GetUndrugByCode(info.Item.ID);
                            if (itemTemp != null)
                            {
                                dictionaryPatientItems[info.Item.ID] = itemTemp.Clone();
                            }
                        }
                    }

                    if ((itemTemp != null) && (itemTemp.UnitFlag == "1"))
                    {
                        ArrayList al = this.ChangeZtToSingle(reg, info);
                        if ((al != null) && (al.Count > 0))
                        {
                            feeDetailsTemp.AddRange(al);
                        }
                    }
                    else
                    {
                        feeDetailsTemp.Add(info);
                    }
                }
                else
                {
                    feeDetailsTemp.Add(info);
                }
            }
            return feeDetailsTemp;
        }

        /// <summary>
        /// ת������Ϊ��ϸ��סԺ�ã�
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="arlFeeItemList"></param>
        /// <returns></returns>
        private ArrayList ConvertFeeItemToSingle(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList arlFeeItemList)
        {
            if ((arlFeeItemList == null) || (arlFeeItemList.Count <= 0))
            {
                return null;
            }

            ArrayList feeDetailsTemp = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList info in arlFeeItemList)
            {
                if (info.Item.ItemType == EnumItemType.UnDrug)
                {
                    Undrug itemTemp = null;
                    if (info.Item is Undrug)
                    {
                        itemTemp = info.Item as Undrug;
                    }

                    if (itemTemp == null || string.IsNullOrEmpty(itemTemp.UnitFlag))
                    {
                        if (dictionaryPatientItems.ContainsKey(info.Item.ID))
                        {
                            itemTemp = dictionaryPatientItems[info.Item.ID].Clone();
                        }
                        else
                        {
                            itemTemp = this.itemManager.GetUndrugByCode(info.Item.ID);
                            if (itemTemp != null)
                            {
                                dictionaryPatientItems[info.Item.ID] = itemTemp.Clone();
                            }
                        }
                    }

                    if ((itemTemp != null) && (itemTemp.UnitFlag == "1"))
                    {
                        ArrayList al = this.ChangeZtToSingle(patient, info);
                        if ((al != null) && (al.Count > 0))
                        {
                            feeDetailsTemp.AddRange(al);
                        }
                    }
                    else
                    {
                        feeDetailsTemp.Add(info);
                    }
                }
                else
                {
                    feeDetailsTemp.Add(info);
                }
            }
            return feeDetailsTemp;
        }

        /// <summary>
        /// ת������Ϊ��ϸ
        /// </summary>
        /// <param name="f"></param>
        /// <param name="reg"></param>
        /// <param name="pactInfo"></param>
        /// <returns></returns>
        public ArrayList ChangeZtToSingle(FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            ArrayList alZt = this.managerIntegrate.QueryUndrugPackageDetailByCode(f.Item.ID);
            if (alZt == null)
            {
                return null;
            }

            ArrayList alFeeItemList = new ArrayList();
            DateTime nowDate = outpatientManager.GetDateTimeFromSysDateTime();
            int age = (int)((new TimeSpan(nowDate.Ticks - reg.Birthday.Ticks)).TotalDays / 365);

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist;

            decimal rate = 1;
            foreach (FS.HISFC.Models.Fee.Item.Undrug info in alZt)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = null;

                if (dictionaryPatientItems.ContainsKey(info.ID))
                {
                    item = dictionaryPatientItems[info.ID].Clone();
                }
                else
                {
                    item = this.GetItem(info.ID);
                    if (item == null)
                    {
                        return null;
                    }
                    dictionaryPatientItems[info.ID] = item.Clone();
                }


                feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

                #region  ת��Ϊʵ��
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeitemlist.Item = new FS.HISFC.Models.Pharmacy.Item();

                }
                else
                {
                    feeitemlist.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                }
                feeitemlist.User01 = f.RecipeNO;
                feeitemlist.User02 = f.SequenceNO.ToString();
                feeitemlist.User03 = f.Order.ID;
                feeitemlist.Patient.PID.ID = f.Patient.ID;

                rate = GetItemRateForZT(f.Item.ID, item.ID);
                decimal orgPrice = 0;
                feeitemlist.Item.Price = this.GetPrice(feeitemlist.Item.ID, reg, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price, ref orgPrice, rate);
                feeitemlist.OrgPrice = orgPrice;

                feeitemlist.Item.Qty = f.Item.Qty * info.Qty;
                feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = f.Patient.ID;//������ˮ��
                feeitemlist.Patient.PID.CardNO = reg.PID.CardNO;//���￨�� 
                feeitemlist.Order.ID = "";//ҽ����ˮ��

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = "";//order.CheckPartRecord;//���� 
                feeitemlist.Order.Combo.ID = f.Order.Combo.ID;//��Ϻ�
                //if (f.Order.Unit == "[������]")
                //{
                //feeitemlist.IsGroup = true;
                feeitemlist.UndrugComb.ID = f.Item.ID;
                feeitemlist.UndrugComb.Name = f.Item.Name;
                feeitemlist.UndrugComb.Qty = f.Item.Qty;//�洢������Ŀ��ϸ����
                //}

                //if (order.Item.IsPharmacy && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl )
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && !((FS.HISFC.Models.Pharmacy.Item)f.Item).IsSubtbl)
                {
                    feeitemlist.ExecOper.Dept.ID = f.Order.StockDept.Clone().ID;//���ۿ����
                    feeitemlist.ExecOper.Dept.Name = f.Order.StockDept.Clone().Name;
                }
                else
                {
                    if (!string.IsNullOrEmpty(f.ExecOper.Dept.ID))
                    {
                        feeitemlist.ExecOper.Dept.ID = f.ExecOper.Dept.ID;
                        feeitemlist.ExecOper.Dept.Name = f.ExecOper.Dept.Name;
                    }
                    else
                    {
                        if (item.ExecDept != "")
                        {
                            //ִ�п��Ұ���ԭ��������Ϣά����ִ�п���
                            feeitemlist.ExecOper.Dept.ID = item.ExecDept.Split('|')[0].ToString();
                            feeitemlist.ExecOper.Dept.Name = feeitemlist.ExecOper.Dept.Name;
                        }
                        else
                        {
                            feeitemlist.ExecOper.Dept.ID = f.ExecOper.Dept.ID;
                            feeitemlist.ExecOper.Dept.Name = feeitemlist.ExecOper.Dept.Name;
                        }
                    }
                }
                feeitemlist.InjectCount = f.Order.InjectCount;//Ժ�ڴ���

                if (f.Order.Item.PackQty <= 0)
                {
                    feeitemlist.Item.PackQty = 1;
                }
                //��������Ŀ
                ////if (order.Item.Price == 0)
                ////{
                ////    order.Item.Price = order.Item.Price;
                ////}
                //by zuowy �����շ��Ƿ�����С��λ ȷ���շ� ��ʱ����
                //if (order.Item.IsPharmacy)
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeitemlist.Item.SpecialFlag = f.Order.Item.SpecialFlag;
                    if (f.Item.PriceUnit == ((FS.HISFC.Models.Pharmacy.Item)f.Item).MinUnit || f.Item.PriceUnit == "")
                    {
                        //xingz
                        feeitemlist.Item.Qty = f.Item.PackQty * f.Item.Qty;//f.Order.Item.PackQty * order.Qty;
                        feeitemlist.FT.TotCost = f.Item.Qty * feeitemlist.Item.Price;// order.Qty * order.Item.Price;

                        feeitemlist.Order.Item.PriceUnit = item.PriceUnit;
                        feeitemlist.FeePack = "1";//������λ 1:��װ��λ 0:��С��λ
                    }
                    else
                    {
                        if (feeitemlist.Item.PackQty == 0)
                        {
                            feeitemlist.Item.PackQty = 1;
                        }
                        feeitemlist.FT.OwnCost = feeitemlist.Item.Qty * feeitemlist.Item.Price / feeitemlist.Item.PackQty; //order.Qty * order.Item.Price / order.Item.PackQty;

                        feeitemlist.FeePack = "0";//������λ 1:��װ��λ 0:��С��λ
                    }
                }
                else
                {
                    feeitemlist.FT.OwnCost = feeitemlist.Item.Qty * feeitemlist.Item.Price;
                    feeitemlist.FeePack = "1";
                }

                if (f.Order.HerbalQty == 0)
                {
                    feeitemlist.Order.HerbalQty = 1;
                }

                feeitemlist.Days = f.Days;//��ҩ����
                feeitemlist.RecipeOper.Dept = f.RecipeOper.Dept.Clone();//����������Ϣ
                feeitemlist.RecipeOper.Name = f.RecipeOper.Name;//����ҽ����Ϣ
                feeitemlist.RecipeOper.ID = f.RecipeOper.ID;
                feeitemlist.Order.DoseUnit = f.Order.DoseUnit;//������λ
                //if (order.Item.IsPharmacy)
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).SysClass.ID.ToString() == "PCC")
                    {
                        if (f.Order.HerbalQty == 0)
                        {
                            f.Order.HerbalQty = 1;
                        }

                        feeitemlist.Order.DoseOnce = f.Order.DoseOnce;

                    }
                    else
                    {
                        feeitemlist.Order.DoseOnce = f.Order.DoseOnce;//ÿ������
                    }
                }
                feeitemlist.Order.Frequency = f.Order.Frequency;//Ƶ����Ϣ

                feeitemlist.Order.Combo.IsMainDrug = f.Order.Combo.IsMainDrug;//�Ƿ���ҩ
                feeitemlist.Item.ID = item.ID;
                feeitemlist.Item.Name = item.Name;
                //if (order.Item.IsPharmacy)//�Ƿ�ҩƷ
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//�Ƿ�ҩƷ
                {
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)f.Item).BaseDose;//��������
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)f.Item).Quality;//ҩƷ����
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)f.Item).DosageForm;//����

                    feeitemlist.IsConfirmed = false;//�Ƿ��ն�ȷ��
                    feeitemlist.Item.PackQty = f.Item.PackQty;//��װ����
                }
                else
                {
                    //������ɶ�õģ���
                    if (f.Order.ReTidyInfo != "SUBTBL")
                    {
                        feeitemlist.IsConfirmed = false;
                        feeitemlist.Item.PackQty = 1;//��װ����
                    }
                    else//�����еĸ�����Ŀ
                    {
                        feeitemlist.IsConfirmed = false;//FS.neHISFC.Components.Function.NConvert.ToBoolean(order.Mark2);
                        feeitemlist.Item.PackQty = 1;
                    }
                }

                //feeitemlist.Order.Item.IsPharmacy = order.Item.IsPharmacy;//�Ƿ�ҩƷ
                feeitemlist.Order.Item.ItemType = feeitemlist.Item.ItemType;//�Ƿ�ҩƷ
                //if (order.Item.IsPharmacy)//ҩƷ
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//ҩƷ
                {
                    feeitemlist.Item.Specs = item.Specs;
                }
                feeitemlist.IsUrgent = f.Order.IsEmergency;//�Ƿ�Ӽ�
                feeitemlist.Order.Sample = f.Order.Sample;//������Ϣ
                feeitemlist.Memo = f.Order.Memo;//��ע
                feeitemlist.Item.MinFee = item.MinFee;//��С����
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//����״̬
                //feeitemlist.Item.Price = order.Item.Price;//�۸�

                feeitemlist.Item.PriceUnit = f.Item.PriceUnit;//�۸�λ
                if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.HerbalQty > 0)
                {

                }
                //feeitemlist.FT.TotCost = order.FT.TotCost;
                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.TotCost, 2);
                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.OwnCost, 2);
                // feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                //feeitemlist.FT = Round(feeitemlist.FT, 2);//ȡ��λ

                //{B9303CFE-755D-4585-B5EE-8C1901F79450} ��ҩƷ�������ԭ�����ܷ���
                if (feeitemlist.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (f.Item.PriceUnit == ((FS.HISFC.Models.Pharmacy.Item)f.Item).MinUnit || f.Item.PriceUnit == "")
                    {
                        feeitemlist.FT.ExcessCost = f.Item.Qty * ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).ChildPrice;
                    }
                    else
                    {
                        feeitemlist.FT.ExcessCost = f.Order.Qty * ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).ChildPrice / feeitemlist.Item.PackQty;
                        feeitemlist.FT.ExcessCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.ExcessCost, 2);
                    }
                }
                feeitemlist.Item.SysClass = item.SysClass;//ϵͳ���
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//��������
                feeitemlist.Order.Usage = f.Order.Usage;//�÷�
                feeitemlist.RecipeSequence = f.RecipeSequence;//�շ�����
                //feeitemlist.RecipeNO = order.ReciptNO;//������  xingz
                feeitemlist.SequenceNO = f.SequenceNO;//������ˮ��
                feeitemlist.FTSource = "1";//����ҽ��
                if (f.Order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//�Ǹ���
                }

                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                feeitemlist.Item.IsNeedConfirm = item.IsNeedConfirm;

                feeitemlist.NoBackQty = f.Item.Qty * feeitemlist.Item.Qty;
                #endregion

                alFeeItemList.Add(feeitemlist);
            }

            return alFeeItemList;
        }

        /// <summary>
        /// ������ת������ϸ��סԺ�ã�
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public ArrayList ChangeZtToSingle(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            ArrayList alZt = this.managerIntegrate.QueryUndrugPackageDetailByCode(f.Item.ID);
            if (alZt == null)
            {
                return null;
            }

            ArrayList alFeeItemList = new ArrayList();
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeitemlist;
            foreach (FS.HISFC.Models.Fee.Item.Undrug info in alZt)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = null;
                if (dictionaryPatientItems.ContainsKey(info.ID))
                {
                    item = dictionaryPatientItems[info.ID].Clone();
                }
                else
                {
                    item = this.GetItem(info.ID);
                    dictionaryPatientItems[info.ID] = item.Clone();
                }

                feeitemlist = f.Clone();

                feeitemlist.Item = item;

                //������Ŀ��Ҫ�������
                feeitemlist.UndrugComb.ID = f.Item.ID;
                feeitemlist.UndrugComb.Name = f.Item.Name;
                feeitemlist.UndrugComb.Qty = f.Item.Qty;//�洢������Ŀ��ϸ����
                feeitemlist.UndrugComb.MinFee.ID = f.Item.MinFee.ID;

                //����
                feeitemlist.Item.Qty = info.Qty * f.Item.Qty;
                feeitemlist.NoBackQty = info.Qty * f.NoBackQty;
                if (feeitemlist.Item.PackQty == 0)
                {
                    feeitemlist.Item.PackQty = 1;
                }

                //ȡ�۸�
                decimal price = 0.00M;
                decimal orgPrice = 0.00M;
                this.GetPriceForInpatient(patient, item, ref price, ref orgPrice);
                feeitemlist.Item.Price = price;
                feeitemlist.Item.DefPrice = orgPrice;
                //���
                feeitemlist.FT.TotCost = feeitemlist.Item.Price * feeitemlist.Item.Qty / feeitemlist.Item.PackQty;
                feeitemlist.FT.OwnCost = feeitemlist.FT.TotCost;
                feeitemlist.FT.PayCost = 0;
                feeitemlist.FT.PubCost = 0;

                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.TotCost, 2);
                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.OwnCost, 2);
                //ȷ�ϱ��
                feeitemlist.Item.IsNeedConfirm = item.IsNeedConfirm;
                //�����������Ų��䣬��������ˮ�ű仯

                alFeeItemList.Add(feeitemlist);
            }

            return alFeeItemList;
        }

        #endregion



        #endregion

        #region ����ֹͣ�˶�ʱ�Զ��˷�����


        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <param name="combNo">��Ϻ�</param>
        /// <param name="dcTimes">�˷ѵ�ִ�е�����</param>
        /// <returns></returns>
        public int SaveApply(string combNo, int dcTimes)
        {
            if (dcTimes <= 0)
            {
                return 1;
            }
            ArrayList alOrders = this.orderManager.QueryOrderByCombNO(combNo, true);
            ExecOrderCompare execComPare = new ExecOrderCompare();

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            {
                //���С�����ҽ�����ж��˷� houwb 2011-4-15
                if (!order.OrderType.IsCharge || order.Item.ID == "999")
                {
                    continue;
                }

                ArrayList alExeOrder = this.orderManager.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                //���ĵ��Ȳ��ж�
                //���ǵ�������Ӻ��ҽ��ִ�д���һ�£����������ͬ�˷Ѵ������˽����ҽ��������ĸ��ĵ����
                if (dcTimes > alExeOrder.Count && !order.IsSubtbl)
                {
                    this.Err = order.Item.Name + "�˷Ѵ�����������շѴ�����������ѡ���˷Ѵ���!";
                    return -1;
                }

                ArrayList feeItemLists = new ArrayList();

                //ִ�е�����ʹ�������������
                alExeOrder.Sort(execComPare);

                //Ӥ���ķ����Ƿ������ȡ����������
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                }

                //סԺ��
                string patientNo = "";

                int i = 1;
                foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
                {
                    //δ�շѵ�ִ�е������ж�
                    if (!exec.IsCharge)
                    {
                        continue;
                    }
                    if (i > dcTimes)
                    {
                        break;
                    }

                    //������ͣ��֮ǰ����
                    //if (exec.DateUse > order.EndTime)
                    //{

                    //Ӥ���ķ���������������� 
                    if (motherPayAllFee == "1")
                    {
                        patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                        //û���ҵ�ĸ��סԺ�ţ�����Ϊ�˻��߲���Ӥ��
                        if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                        {
                            patientNo = exec.Order.Patient.ID;
                        }
                    }
                    else
                    {
                        patientNo = exec.Order.Patient.ID;
                    }

                    ArrayList feeItemListTempArray = this.GetFeeListByExecSeq(patientNo, exec.ID, exec.Order.Item.ItemType);
                    if (feeItemListTempArray == null)
                    {
                        this.Err = exec.Order.Item.Name + "��ʹ��ʱ�䡾" + exec.DateUse.ToString() + "���Ѿ�û�п���������";
                        return -1;
                    }
                    feeItemLists.AddRange(feeItemListTempArray);
                    //}
                    i++;
                }

                if (feeItemLists.Count <= 0)
                {
                    //return 1;
                    continue;
                }

                string errMsg = string.Empty;//������Ϣ
                int returnValue = 0;//����ֵ
                DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //����˷������
                string applyBillCode = this.returnMgr.GetReturnApplyBillCode();
                if (applyBillCode == null || applyBillCode == string.Empty)
                {
                    this.Err = "��ȡ���뵥�ų���" + this.returnMgr.Err;
                    return -1;
                }
                //ȡ������Ϣ
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();


                //ѭ�������˷�����
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
                {
                    patientInfo.ID = feeItemList.Patient.ID;

                    if (this.QuitFeeApply(patientInfo, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                    {
                        return -1;
                    }

                    //#region �����˷�����
                    ////�����˷�����
                    //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                    //{
                    //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                    //    {
                    //        this.Err = Language.Msg("�����˷�ʧ��!" + this.inpatientManager.Err);

                    //        return -1;
                    //    }
                    //}
                    //#endregion

                    ////�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                    //if (feeItemList.Days == 0)
                    //{
                    //    feeItemList.Days = 1;
                    //}

                    //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                    //if (feeItemListTemp == null)
                    //{
                    //    this.Err = Language.Msg("�����Ŀ������Ϣ����!" + this.inpatientManager.Err);

                    //    return -1;
                    //}

                    //if (feeItemList.MateList.Count > 0)
                    //{
                    //    feeItemListTemp.MateList = feeItemList.MateList;
                    //}
                    ////���˷ѵ�����д��¼
                    //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug 
                    //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                    //{
                    //    if (feeItemList.Item.User01 == "1")
                    //    {
                    //        feeItemList.User01 = "��סԺ��";
                    //    }
                    //    else
                    //    {
                    //        feeItemList.User01 = "��ҩ��";
                    //    }
                    //}
                    //else
                    //{
                    //    feeItemList.User01 = "��סԺ��";
                    //}
                    //if (feeItemList.Memo != "OLD")
                    //{
                    //    feeItemList.User02 = applyBillCode;
                    //}
                    ////���Ѿ���������˷����벻���д���
                    //if (feeItemList.Memo == "OLD")
                    //{
                    //    continue;
                    //}

                    ////���·��ñ��еĿ��������ֶ�
                    ////�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                    //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                    //if (returnValue == -1)
                    //{
                    //    this.Err = errMsg;

                    //    return -1;
                    //}

                    ////��ʱ��Ŀ�����˷������
                    //feeItemListTemp.User02 = applyBillCode;

                    ////�����ҩƷ�����Ѿ���ҩ���������ҩ���룻��������˷����롣
                    //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug 
                    //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                    //{
                    //    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    //    {
                    //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    //    }

                    //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    //    info.ID = feeItemListTemp.Patient.ID;

                    //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                    //    {
                    //        this.Err = Language.Msg("��ҩ����ʧ��!" + this.PharmarcyManager.Err);

                    //        return -1;
                    //    }
                    //}
                    //else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�����
                    //{
                    //    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    //    if (feeItemList.FTRate.ItemRate != 0)
                    //    {
                    //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    //    }

                    //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    //    info.ID = feeItemListTemp.Patient.ID;

                    //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                    //    {
                    //        this.Err = Language.Msg("�����˷�����ʧ��!") + this.returnMgr.Err;

                    //        return -1;
                    //    }


                    //    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                    //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    //    {
                    //        //ȡ��ҩ�����¼���ж���״̬�Ƿ���������������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��
                    //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //        if (applyOut == null)
                    //        {
                    //            this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                    //            return -1;
                    //        }

                    //        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                    //        if (applyOut.ID == string.Empty)
                    //        {
                    //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!");

                    //            return -1;
                    //        }

                    //        //����
                    //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    //        {
                    //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���ѱ���������Ա�˷ѣ���ˢ�µ�ǰ����!");

                    //            return -1;
                    //        }

                    //        //���ϰ�ҩ����
                    //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //        if (returnValue == -1)
                    //        {
                    //            this.Err = Language.Msg("���ϰ�ҩ�������!") + PharmarcyManager.Err;

                    //            return -1;
                    //        }
                    //        if (returnValue == 0)
                    //        {
                    //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������");

                    //            return -1;
                    //        }

                    //        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                    //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                    //        {
                    //            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                    //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //            if (applyOutTemp == null)
                    //            {
                    //                this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                    //                return -1;
                    //            }

                    //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                    //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                    //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                    //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬

                    //            applyOutTemp.ID = "";
                    //            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                    //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                    //            {
                    //                this.Err = Language.Msg("���²��뷢ҩ�������!") + this.PharmarcyManager.Err;

                    //                return -1;
                    //            }
                    //        }
                    //    }
                    //}

                    ////if (feeItemListTemp.Item.IsPharmacy)
                    //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    //{
                    //    ArrayList patientDrugStorageList = this.PharmarcyManager.QueryStorageList(feeItemListTemp.Order.Patient.ID, 
                    //        feeItemListTemp.Item.ID);
                    //    if (patientDrugStorageList == null)
                    //    {
                    //        this.Err = Language.Msg("�ж��Ƿ���ڻ��߿��ʱ����") + this.PharmarcyManager.Err;

                    //        return -1;
                    //    }
                    //    //�Ի��߿������������
                    //    if (patientDrugStorageList.Count > 0)
                    //    {
                    //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                    //        storageBase.Quantity = -storageBase.Quantity;
                    //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                    //        storageBase.PrivType = "AAAA";	//��¼סԺ�˷ѱ��
                    //        if (storageBase.ID == string.Empty)
                    //        {
                    //            storageBase.ID = applyBillCode;
                    //            storageBase.SerialNO = 0;
                    //        }

                    //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                    //        {
                    //            this.Err = Language.Msg("���»��߿�����!") + this.PharmarcyManager.Err;

                    //            return -1;
                    //        }
                    //    }
                    //}
                }
            }
            return 1;
        }

        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="combNo">��Ϻ�</param>
        /// <param name="dcTimes">�˷Ѵ���</param>
        /// <param name="quitConfirm">�Ƿ�ֱ���˷�</param>
        /// <returns></returns>
        public int SaveApply(FS.HISFC.Models.RADT.PatientInfo patient, string combNo, int dcTimes, bool quitConfirm, ref Hashtable hsQuitFeeWarning)
        {
            if (dcTimes <= 0)
            {
                return 1;
            }
            ArrayList alOrders = this.orderManager.QueryOrderByCombNO(combNo, true);

            ArrayList alSubtblOrders = this.orderManager.QueryOrderByCombNO(combNo, false);
            alOrders.AddRange(alSubtblOrders);

            ExecOrderCompare execComPare = new ExecOrderCompare();

            //סԺ��
            string patientNo = "";

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            {
                //���С�����ҽ�����ж��˷� houwb 2011-4-15
                if (!order.OrderType.IsCharge || order.Item.ID == "999")
                {
                    continue;
                }

                ArrayList alExeOrder = this.orderManager.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                //���ĵ��Ȳ��ж�
                //���ǵ�������Ӻ��ҽ��ִ�д���һ�£����������ͬ�˷Ѵ������˽����ҽ��������ĸ��ĵ����
                if (dcTimes > alExeOrder.Count && !order.IsSubtbl)
                {
                    this.Err = order.Item.Name + "�˷Ѵ�����������շѴ�����������ѡ���˷Ѵ���!";
                    return -1;
                }

                ArrayList feeItemLists = new ArrayList();

                //ִ�е�����ʹ�������������
                alExeOrder.Sort(execComPare);

                //Ӥ���ķ����Ƿ������ȡ����������
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                }

                int i = 1;
                foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
                {
                    //δ�շѵ�ִ�е������ж�
                    if (!exec.IsCharge)
                    {
                        continue;
                    }
                    if (i > dcTimes)
                    {
                        break;
                    }

                    //Ӥ���ķ���������������� 
                    if (motherPayAllFee == "1")
                    {
                        patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                        //û���ҵ�ĸ��סԺ�ţ�����Ϊ�˻��߲���Ӥ��
                        if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                        {
                            patientNo = exec.Order.Patient.ID;
                        }
                    }
                    else
                    {
                        patientNo = exec.Order.Patient.ID;
                    }

                    ArrayList feeItemListTempArray = this.GetFeeListByExecSeq(patientNo, exec.ID, exec.Order.Item.ItemType);
                    if (feeItemListTempArray == null)
                    {
                        this.Err = exec.Order.Item.Name + "��ʹ��ʱ�䡾" + exec.DateUse.ToString() + "���Ѿ�û�п���������";
                        return -1;
                    }
                    feeItemLists.AddRange(feeItemListTempArray);

                    i++;
                }

                if (feeItemLists.Count <= 0)
                {
                    //return 1;
                    continue;
                }

                string errMsg = string.Empty;//������Ϣ
                int returnValue = 0;//����ֵ
                DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //����˷������
                string applyBillCode = string.Empty;

                applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
                if (applyBillCode == null || applyBillCode == string.Empty)
                {
                    this.Err = errMsg;
                    return -1;
                }

                #region ѭ���˷ѻ�����

                ArrayList alQuitFeeItems = new ArrayList();

                //ѭ�������˷�����
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
                {
                    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    info.ID = feeItemList.Patient.ID;

                    //����շѿ��Ҳ��Ǳ����һ�������ֻ��������
                    if (!this.CheckIsSameDept(feeItemList.FeeOper.Dept))
                    {
                        if (!hsQuitFeeWarning.Contains(feeItemList.Item.Name))
                        {
                            hsQuitFeeWarning.Add(feeItemList.Item.Name, null);
                        }

                        if (this.QuitFeeApply(info, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (this.QuitFeeApply(info, feeItemList, !quitConfirm, applyBillCode, nowTime, ref errMsg) == -1)
                        {
                            return -1;
                        }
                    }
                }
                #endregion
            }

            return 1;
        }

        /// <summary>
        /// �Ƿ��Ǳ����� ���ݵ�½����У��
        /// </summary>
        /// <param name="deptCode">��ҪУ��Ŀ���</param>
        /// <returns></returns>
        private bool CheckIsSameDept(FS.FrameWork.Models.NeuObject deptObj)
        {
            try
            {
                ArrayList alDept = this.managerIntegrate.QueryDepartment(deptObj.ID);
                alDept.AddRange(this.managerIntegrate.QueryNurseStationByDept(deptObj));
                alDept.Add(deptObj);

                //����½����Ϊ��ǰ���ҡ���Ӧ��������Ӧ����ʱ���϶�Ϊ�Լ��Ŀ��ң�
                foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                {
                    if (dept.ID == ((FS.HISFC.Models.Base.Employee)this.orderManager.Operator).Dept.ID)
                    {
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <param name="alExecs"></param>
        /// <returns></returns>
        public int SaveApply(ArrayList alExecs)
        {
            if (alExecs.Count <= 0)
            {
                return 1;
            }

            ArrayList alExeOrder = alExecs;


            ArrayList feeItemLists = new ArrayList();

            //Ӥ���ķ����Ƿ������ȡ����������
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            //סԺ��
            string patientNo = "";

            #region ��ȡ����

            foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
            {
                //δ�շѵ�ִ�е������ж�
                if (!exec.IsCharge)
                {
                    continue;
                }

                //Ӥ���ķ���������������� 
                if (motherPayAllFee == "1")
                {
                    patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                    //û���ҵ�ĸ��סԺ�ţ�����Ϊ�˻��߲���Ӥ��
                    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                    {
                        patientNo = exec.Order.Patient.ID;
                    }
                }
                else
                {
                    patientNo = exec.Order.Patient.ID;
                }

            }

            if (feeItemLists.Count <= 0)
            {
                return 1;
            }

            #endregion

            string errMsg = string.Empty;//������Ϣ
            int returnValue = 0;//����ֵ
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //����˷������
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                this.Err = errMsg;
                return -1;
            }

            //ѭ�������˷�����
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = feeItemList.Patient.ID;
                if (this.QuitFeeApply(info, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                {
                    return -1;
                }
                //#region �����˷�����
                ////�����˷�����
                //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                //{
                //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                //    {
                //        this.Err = Language.Msg("�����˷�ʧ��!" + this.inpatientManager.Err);

                //        return -1;
                //    }
                //}
                //#endregion

                ////�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                //if (feeItemList.Days == 0)
                //{
                //    feeItemList.Days = 1;
                //}

                //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                //if (feeItemListTemp == null)
                //{
                //    this.Err = Language.Msg("�����Ŀ������Ϣ����!" + this.inpatientManager.Err);

                //    return -1;
                //}

                //if (feeItemList.MateList.Count > 0)
                //{
                //    feeItemListTemp.MateList = feeItemList.MateList;
                //}
                ////���˷ѵ�����д��¼
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    if (feeItemList.Item.User01 == "1")
                //    {
                //        feeItemList.User01 = "��סԺ��";
                //    }
                //    else
                //    {
                //        feeItemList.User01 = "��ҩ��";
                //    }
                //}
                //else
                //{
                //    feeItemList.User01 = "��סԺ��";
                //}
                //if (feeItemList.Memo != "OLD")
                //{
                //    feeItemList.User02 = applyBillCode;
                //}
                ////���Ѿ���������˷����벻���д���
                //if (feeItemList.Memo == "OLD")
                //{
                //    continue;
                //}

                ////���·��ñ��еĿ��������ֶ�
                ////�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                //if (returnValue == -1)
                //{
                //    this.Err = errMsg;

                //    return -1;
                //}

                ////��ʱ��Ŀ�����˷������
                //feeItemListTemp.User02 = applyBillCode;

                ////�����ҩƷ�����Ѿ���ҩ���������ҩ���룻��������˷����롣
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                //    {
                //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("��ҩ����ʧ��!" + this.PharmarcyManager.Err);

                //        return -1;
                //    }
                //}
                //else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�����
                //{
                //    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                //    if (feeItemList.FTRate.ItemRate != 0)
                //    {
                //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("�����˷�����ʧ��!") + this.returnMgr.Err;

                //        return -1;
                //    }


                //    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //    {
                //        //ȡ��ҩ�����¼���ж���״̬�Ƿ���������������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��
                //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (applyOut == null)
                //        {
                //            this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }

                //        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                //        if (applyOut.ID == string.Empty)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!");

                //            return -1;
                //        }

                //        //����
                //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���ѱ���������Ա�˷ѣ���ˢ�µ�ǰ����!");

                //            return -1;
                //        }

                //        //���ϰ�ҩ����
                //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (returnValue == -1)
                //        {
                //            this.Err = Language.Msg("���ϰ�ҩ�������!") + PharmarcyManager.Err;

                //            return -1;
                //        }
                //        if (returnValue == 0)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������");

                //            return -1;
                //        }

                //        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                //        {
                //            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //            if (applyOutTemp == null)
                //            {
                //                this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }

                //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬

                //            applyOutTemp.ID = "";
                //            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                //            {
                //                this.Err = Language.Msg("���²��뷢ҩ�������!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }
                //        }
                //    }
                //}

                ////if (feeItemListTemp.Item.IsPharmacy)
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //{
                //    ArrayList patientDrugStorageList = this.PharmarcyManager.QueryStorageList(feeItemListTemp.Order.Patient.ID,
                //        feeItemListTemp.Item.ID);
                //    if (patientDrugStorageList == null)
                //    {
                //        this.Err = Language.Msg("�ж��Ƿ���ڻ��߿��ʱ����") + this.PharmarcyManager.Err;

                //        return -1;
                //    }
                //    //�Ի��߿������������
                //    if (patientDrugStorageList.Count > 0)
                //    {
                //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                //        storageBase.Quantity = -storageBase.Quantity;
                //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                //        storageBase.PrivType = "AAAA";	//��¼סԺ�˷ѱ��
                //        if (storageBase.ID == string.Empty)
                //        {
                //            storageBase.ID = applyBillCode;
                //            storageBase.SerialNO = 0;
                //        }

                //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                //        {
                //            this.Err = Language.Msg("���»��߿�����!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }
                //    }
                //}
            }
            return 1;
        }

        private ArrayList GetFeeListByExecSeq(string inPatientNo, string execSeq, EnumItemType itemType)
        {
            ArrayList feeItemListTempArray = inpatientManager.GetItemListByExecSQN(inPatientNo, execSeq, itemType);

            if (feeItemListTempArray == null || feeItemListTempArray.Count == 0)
            {
                //���߿��ʱ�����ڲ���ִ�е�û�ж�Ӧ�շѼ�¼�����
                feeItemListTempArray = inpatientManager.GetItemListByExecSQNAll(inPatientNo, execSeq, itemType);
                if (feeItemListTempArray == null || feeItemListTempArray.Count == 0)
                {
                    return new ArrayList();
                }

                //�ð汾-���߿�棬����QTYΪ0��ʱ��Ҳ������ü�¼�У�����Ҫ����
                decimal qty = (feeItemListTempArray[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Item.Qty;
                if (qty == 0)
                {
                    return new ArrayList();
                }

                return null;
            }
            return feeItemListTempArray;
        }

        /// <summary>
        /// �����˷�������Ϣ,���ݴ�������Զ�ȷ���˷�
        /// �Ǳ������˷���������Զ�ȷ��
        /// </summary>
        /// <param name="alExecs"></param>
        /// <param name="quitConfirm"></param>
        /// /// <param name="msg"></param>
        /// <returns></returns>
        public int SaveApply(ArrayList alExecs, bool quitConfirm, ref string msg)
        {
            if (alExecs.Count <= 0)
            {
                return 1;
            }

            ArrayList alExeOrder = alExecs;


            ArrayList feeItemLists = new ArrayList();

            //Ӥ���ķ����Ƿ������ȡ����������
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            //סԺ��
            string patientNo = "";

            #region ��ȡ����

            foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
            {
                //δ�շѵ�ִ�е������ж�
                if (!exec.IsCharge)
                {
                    continue;
                }

                //Ӥ���ķ���������������� 
                if (motherPayAllFee == "1")
                {
                    patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                    //û���ҵ�ĸ��סԺ�ţ�����Ϊ�˻��߲���Ӥ��
                    if (patientNo == "-1" || string.IsNullOrEmpty(patientNo))
                    {
                        patientNo = exec.Order.Patient.ID;
                    }
                }
                else
                {
                    patientNo = exec.Order.Patient.ID;
                }

                ArrayList feeItemListTempArray = this.GetFeeListByExecSeq(patientNo, exec.ID, exec.Order.Item.ItemType);
                if (feeItemListTempArray == null)
                {
                    this.Err = exec.Order.Item.Name + "��ʹ��ʱ�䡾" + exec.DateUse.ToString() + "���Ѿ�û�п���������";
                    return -1;
                }
                feeItemLists.AddRange(feeItemListTempArray);

                //ArrayList feeItemListTempArray = inpatientManager.GetItemListByExecSQN(patientNo, exec.ID, exec.Order.Item.ItemType);

                //if (feeItemListTempArray == null || feeItemListTempArray.Count == 0)
                //{
                //    this.Err = exec.Order.Item.Name + "��ʹ��ʱ�䡾" + exec.DateUse.ToString() + "���Ѿ�û�п���������";
                //    return -1;
                //}
                //else
                //{
                //    feeItemLists.AddRange(feeItemListTempArray);
                //}

            }

            if (feeItemLists.Count <= 0)
            {
                return 1;
            }

            #endregion

            string errMsg = string.Empty;//������Ϣ
            int returnValue = 0;//����ֵ
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //����˷������
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                this.Err = errMsg;
                return -1;
            }

            //ѭ�������˷�����
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = feeItemList.Patient.ID;
                if (this.QuitFeeApply(info, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                {
                    return -1;
                }
                //#region �����˷�����
                ////�����˷�����
                //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                //{
                //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                //    {
                //        this.Err = Language.Msg("�����˷�ʧ��!" + this.inpatientManager.Err);

                //        return -1;
                //    }
                //}
                //#endregion

                ////�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                //if (feeItemList.Days == 0)
                //{
                //    feeItemList.Days = 1;
                //}

                //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                //if (feeItemListTemp == null)
                //{
                //    this.Err = Language.Msg("�����Ŀ������Ϣ����!" + this.inpatientManager.Err);

                //    return -1;
                //}

                //if (feeItemList.MateList.Count > 0)
                //{
                //    feeItemListTemp.MateList = feeItemList.MateList;
                //}
                ////���˷ѵ�����д��¼
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    if (feeItemList.Item.User01 == "1")
                //    {
                //        feeItemList.User01 = "��סԺ��";
                //    }
                //    else
                //    {
                //        feeItemList.User01 = "��ҩ��";
                //    }
                //}
                //else
                //{
                //    feeItemList.User01 = "��סԺ��";
                //}
                //if (feeItemList.Memo != "OLD")
                //{
                //    feeItemList.User02 = applyBillCode;
                //}
                ////���Ѿ���������˷����벻���д���
                //if (feeItemList.Memo == "OLD")
                //{
                //    continue;
                //}

                ////���·��ñ��еĿ��������ֶ�
                ////�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                //if (returnValue == -1)
                //{
                //    this.Err = errMsg;

                //    return -1;
                //}

                ////��ʱ��Ŀ�����˷������
                //feeItemListTemp.User02 = applyBillCode;

                ////�����ҩƷ�����Ѿ���ҩ���������ҩ���룻��������˷����롣
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                //    {
                //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("��ҩ����ʧ��!" + this.PharmarcyManager.Err);

                //        return -1;
                //    }
                //}
                //else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�����
                //{
                //    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                //    if (feeItemList.FTRate.ItemRate != 0)
                //    {
                //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("�����˷�����ʧ��!") + this.returnMgr.Err;

                //        return -1;
                //    }


                //    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //    {
                //        //ȡ��ҩ�����¼���ж���״̬�Ƿ���������������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��
                //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (applyOut == null)
                //        {
                //            this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }

                //        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                //        if (applyOut.ID == string.Empty)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!");

                //            return -1;
                //        }

                //        //����
                //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���ѱ���������Ա�˷ѣ���ˢ�µ�ǰ����!");

                //            return -1;
                //        }

                //        //���ϰ�ҩ����
                //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (returnValue == -1)
                //        {
                //            this.Err = Language.Msg("���ϰ�ҩ�������!") + PharmarcyManager.Err;

                //            return -1;
                //        }
                //        if (returnValue == 0)
                //        {
                //            this.Err = Language.Msg("��Ŀ��") + feeItemListTemp.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������");

                //            return -1;
                //        }

                //        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                //        {
                //            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //            if (applyOutTemp == null)
                //            {
                //                this.Err = Language.Msg("���������Ϣ����!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }

                //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬

                //            applyOutTemp.ID = "";
                //            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                //            {
                //                this.Err = Language.Msg("���²��뷢ҩ�������!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }
                //        }
                //    }
                //}

                ////if (feeItemListTemp.Item.IsPharmacy)
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //{
                //    ArrayList patientDrugStorageList = this.PharmarcyManager.QueryStorageList(feeItemListTemp.Order.Patient.ID,
                //        feeItemListTemp.Item.ID);
                //    if (patientDrugStorageList == null)
                //    {
                //        this.Err = Language.Msg("�ж��Ƿ���ڻ��߿��ʱ����") + this.PharmarcyManager.Err;

                //        return -1;
                //    }
                //    //�Ի��߿������������
                //    if (patientDrugStorageList.Count > 0)
                //    {
                //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                //        storageBase.Quantity = -storageBase.Quantity;
                //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                //        storageBase.PrivType = "AAAA";	//��¼סԺ�˷ѱ��
                //        if (storageBase.ID == string.Empty)
                //        {
                //            storageBase.ID = applyBillCode;
                //            storageBase.SerialNO = 0;
                //        }

                //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                //        {
                //            this.Err = Language.Msg("���»��߿�����!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }
                //    }
                //}
            }

            if (quitConfirm)
            {
                ArrayList alReturns = this.returnMgr.QueryReturnApplys(patientNo);

                foreach (FS.HISFC.Models.Fee.ReturnApply apply in alReturns)
                {
                    bool confirm = true;

                    if (apply.Item.ItemType == EnumItemType.UnDrug)
                    {
                        if (apply.ExecOper.Dept.ID != (returnMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                        {
                            confirm = false;
                            msg += "��Ŀ:" + apply.Item.Name + "ִ�п��ҷǱ����ң������˷����룡" + "\n";
                        }
                    }

                    if (confirm)
                    {
                        int ret = returnMgr.ConfirmApply(apply);

                        if (ret < 0)
                        {
                            this.Err = "ȷ���˷ѳ�����Ŀ:" + apply.Item.Name + "," + this.PharmarcyManager.Err;

                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region ��Ժ��ȡ��λ�ѵ�

        /// <summary>
        /// ��ȡ���ѻ��߼����޶������
        /// </summary>
        /// <param name="pInfo">������Ϣ</param>
        /// <returns></returns>
        public DateTime GetAdjustOverLimitFromDate(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            feeMgr.SetTrans(this.trans);

            DateTime dtRealBegin = new DateTime();
            DataSet dsBalanceInfo = new DataSet();
            if (feeMgr.GetBeforeBalanceInfo(pInfo, ref dsBalanceInfo) == -1)
            {
                this.Err = "��ȡ���߼����޶����ڳ���" + feeMgr.Err;
                return System.DateTime.MinValue;
            }
            if (dsBalanceInfo.Tables[0].Rows.Count <= 0)
            {
                //��ǰû�������нᣬû���н���Ϣ,������޶�ӱ�����Ժ��ʼ
                //inDays = ( (TimeSpan)( patientInfo.PVisit.OutTime.Date - patientInfo.PVisit.InTime.Date ) ).Days;
                dtRealBegin = pInfo.PVisit.InTime;
            }
            else
            {
                //����ֶν���ܺ�ͬ��λ��ͬ�����
                //û�ж��Ƿֶα�����Ѻ�ͬ��λ�����������в�����
                //�����������������м�û�����������סԺ���ڿ�ʼ����
                //������������������������ӱ�����ݱ�������ڿ�ʼ�����޶�
                foreach (DataRow drRow in dsBalanceInfo.Tables[0].Rows)
                {
                    //�Ƿ��б��
                    bool hsChange = false;
                    //˵�������н��ͬ��λ��ͬ
                    if (drRow[0].ToString().Equals(pInfo.Pact.PayKind.ID) && !hsChange)
                    {
                        //�ӱ��ν��㿪ʼ�����޶�
                        //inDays = ( (TimeSpan)( patientInfo.PVisit.OutTime.Date -
                        //    ( FS.NFC.Function.NConvert.ToDateTime( drRow[1].ToString() ) ).Date ) ).Days;
                        dtRealBegin = (FS.FrameWork.Function.NConvert.ToDateTime(drRow[1].ToString()));
                    }
                    else
                    {
                        hsChange = true;
                        dtRealBegin = (FS.FrameWork.Function.NConvert.ToDateTime(drRow[2].ToString()));
                    }
                }
            }
            return dtRealBegin;
        }

        /// <summary>
        /// �������ѻ��ߴ�λ��
        /// </summary>
        /// <param name="pInfo">סԺ������Ϣ</param>
        /// <param name="isOut">�Ƿ��ǳ�Ժ����</param>
        /// <returns></returns>
        public int AdjustOverLimitBed(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isOut)
        {
            if (pInfo == null)
            {
                this.Err = "����ʵ��û�г�ʼ����";
                return -1;
            }
            if (pInfo.Pact.PayKind.ID != "03")
            {
                return 1;
            }

            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            feeMgr.SetTrans(this.trans);

            //���ߵĴ�λ�޶���ߴ���Ժ��ʼ�޶�̶�
            decimal dayLimit = pInfo.FT.BedLimitCost;
            //���ߴ���Ժ��ʼ������ʱ��סԺ����
            int inDays = 1;
            if (pInfo.PVisit.InTime == null || pInfo.PVisit.InTime == DateTime.MinValue)
            {
                this.Err = "������Ժ����û�и�ֵ�����ѯ��";
                return -1;
            }
            //��ȡϵͳʱ��
            DateTime dtSys = feeMgr.GetDateTimeFromSysDateTime();
            DateTime dtTNN = new DateTime(2008, 4, 18);
            DateTime dtRealBegin = this.GetAdjustOverLimitFromDate(pInfo);
            DateTime dtEnd = feeMgr.GetDateTimeFromSysDateTime();
            if (dtRealBegin == System.DateTime.MinValue)
            {
                dtRealBegin = pInfo.PVisit.InTime;
            }
            //[[[[[[��Ժ����ʱ��סԺ����=��Ժ����-��Ժ����]]]]]]
            //-------------------New Deal---------------
            //����Ϊ�����Ϊ����סԺΪͬһ����ͬ��λ�����סԺ���ڿ�ʼ���е���
            //���סԺ�ڼ䷢����ͬ��λ�����ӵ�ǰ��ͬ��λ�������ʼ���ڽ��е���
            if (isOut)
            {
                if (pInfo.PVisit.OutTime == DateTime.MinValue || pInfo.PVisit.OutTime == null)
                {
                    this.Err = "���ߵĳ�Ժ����û�и�ֵ��";
                    return -1;
                }
                if (pInfo.PVisit.OutTime < pInfo.PVisit.InTime)
                {
                    this.Err = "���߳�Ժ����С����Ժ���ڣ����ѯ��";
                    return -1;
                }

                //if (pInfo.PVisit.InTime.Date < dtTNN && pInfo.PVisit.OutTime.Date > dtTNN)
                //{
                //    inDays = ( (TimeSpan)( pInfo.PVisit.OutTime.Date - dtTNN ) ).Days;
                //}
                //else
                //{
                //    inDays = ( (TimeSpan)( pInfo.PVisit.OutTime.Date - pInfo.PVisit.InTime.Date ) ).Days;
                //}
                inDays = ((TimeSpan)(pInfo.PVisit.OutTime.Date - dtRealBegin.Date)).Days;
                if (inDays < 0)
                {
                    this.Err = "���߳�Ժ����С����Ժ���ڣ����ѯ��";
                    return -1;
                }
                if (inDays == 0)
                {
                    inDays = 1;
                }
            }
            else
            {
                if (pInfo.PVisit.InTime > dtSys)
                {
                    this.Err = "������Ժʱ�����ϵͳʱ�䣡���ѯ��";
                    return -1;
                }
                //if (pInfo.PVisit.InTime.Date < dtTNN)
                //{
                //    inDays = ((TimeSpan)(dtSys.Date - dtTNN)).Days;
                //}
                //else
                //{
                //    inDays = ((TimeSpan)(dtSys.Date - pInfo.PVisit.InTime.Date)).Days;
                //}
                //�������޸ġ�inDays = ( (TimeSpan)( pInfo.PVisit.OutTime.Date - dtRealBegin.Date ) ).Days;
                inDays = ((TimeSpan)(dtSys.Date - dtRealBegin.Date)).Days;
                //ÿ�������Զ�����ʱ���촲λ���ڵ�����Χ֮�ڣ���������+1
                //inDays = inDays + 1;
            }
            //�����סԺ���죬����Ϊ1
            if (inDays == 0)
            {
                inDays = 1;
            }
            //���ߵ���ǰʱ���ܵĴ�λ�޶
            decimal totLimit = pInfo.FT.BedLimitCost * inDays;
            //���ߵ�ĿǰΪֹ�ܵĴ�λ��
            FT bedFee = null;
            //��ȡ���ߵ�ĿǰΪֹ�ܵĴ�λ��
            //bedFee = feeMgr.GetPatientBedFee(pInfo.ID);
            bedFee = feeMgr.GetPatientBedFee(pInfo.ID, dtRealBegin.ToString(), dtEnd.ToString());
            if (bedFee == null)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //����޶�=���˶�����������
            if (bedFee.PubCost + bedFee.PayCost == totLimit)
            {
                return 1;
            }

            //������¼ʵ�嶨��
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            //��������ܵĴ�λ�Ѽ��˲���+�Ը�����>��λ�ܵ��޶� �����޶�
            if (bedFee.PubCost + bedFee.PayCost > totLimit)
            {
                //���ε������
                decimal adjFee = bedFee.PayCost + bedFee.PubCost - totLimit;
                //���ø�ֵ
                ItemList.FT.TotCost = 0;
                ItemList.FT.OwnCost = adjFee;
            }
            //������߲����꣬�������޶�ʣ�࣬���ۼ��Էѽ��>0
            else if (bedFee.OwnCost > 0)
            {
                //���ε������
                decimal adjFee = 0;
                //���ε���ǰ�޶�ʣ��ռ���
                decimal adjSpan = totLimit - bedFee.PayCost - bedFee.PubCost;
                //���ε������Ϊ�޶�ʣ��ռ�����ۼ��Էѽ��֮���С��
                adjFee = bedFee.OwnCost < adjSpan ? bedFee.OwnCost : adjSpan;
                //���ø�ֵ
                ItemList.FT.TotCost = 0;
                ItemList.FT.OwnCost = -adjFee;
            }
            else
            {
                return 1;
            }
            //��ȡ��ͬ��λ����
            FS.HISFC.Models.Base.PactInfo pactUnitInfo = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            pactManagment.SetTrans(this.trans);
            pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);
            //�Ը�����
            decimal payRate = pactUnitInfo.Rate.PayRate;

            //��Ժ����
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //��ʿվ
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            //ִ�п���
            ItemList.ExecOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //�ۿ����
            ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //��������
            ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //����ҽ��
            ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //ҽ��
            //�۸�Ϊ0
            ItemList.Item.Price = 0;
            //����Ϊ1
            ItemList.Item.Qty = ItemList.FT.OwnCost > 0 ? 1 : -1;//����
            //��λ
            ItemList.Item.PriceUnit = "��";
            //��ҩƷ
            //ItemList.Item.IsPharmacy = false;
            ItemList.Item.ItemType = EnumItemType.UnDrug;
            ItemList.PayType = PayTypes.Balanced;
            ItemList.IsBaby = false;
            ItemList.BalanceNO = 0;
            ItemList.BalanceState = "0";
            //��������
            ItemList.NoBackQty = 1;
            ItemList.ChargeOper.ID = feeMgr.Operator.ID;
            //����ʱ��
            ItemList.ChargeOper.OperTime = dtSys;
            ItemList.FeeOper.ID = feeMgr.Operator.ID;
            ItemList.FeeOper.OperTime = dtSys;
            ItemList.ChargeOper.OperTime = dtSys;

            //��λ�ѵ���С���ô���
            ItemList.Item.MinFee.ID = "004";
            //��Ŀ����
            ItemList.Item.ID = "F004";
            //��Ŀ����
            ItemList.Item.Name = "���괲λ��";
            //�Ը���� = (�ܽ��(0)-�Էѽ��)*�Ը�����
            ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((-ItemList.FT.OwnCost) * payRate, 2);
            //���˽�� = �ܽ��(0)-�Էѽ��-�Ը����
            ItemList.FT.PubCost = -ItemList.FT.OwnCost - ItemList.FT.PayCost;
            //������
            ItemList.RecipeNO = feeMgr.GetUndrugRecipeNO();
            ItemList.SequenceNO = 1;
            //������ϸ��
            if (feeMgr.InsertFeeItemList(pInfo, ItemList) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //������ܱ�
            if (feeMgr.InsertFeeInfo(pInfo, ItemList) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //����סԺ����
            if (feeMgr.UpdateInMainInfoFee(pInfo.ID, ItemList.FT) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }


            return 1;
        }

        /// <summary>
        /// ��Ժ���մ�λ��
        /// </summary>
        /// <param name="pInfo">����ʵ��</param>
        /// <returns></returns>
        public int SupplementBedFee(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            DateTime dt = pInfo.PVisit.PreOutTime;
            //�жϻ��߳�Ժ�����Ƿ���ֵ�����û�и�ֵ�ڴ˴���������
            if (dt == DateTime.MinValue ||
                dt == null)
            {
                dt = pInfo.PVisit.OutTime;
            }

            //���չ����շ�Ϊ�յ�
            FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee ruleFeeManager = new InpatientRuleFee();
            if (this.trans != null)
            {
                ruleFeeManager.SetTrans(this.trans);
            }

            if (ruleFeeManager.DoRuleFee(pInfo, new FS.HISFC.Models.Fee.Inpatient.FTSource("220"), pInfo.PVisit.InTime, dt) == -1)
            {
                this.Err = "���ն�ʱ�շ���Ϣʧ�ܣ�" + ruleFeeManager.Err;
                return -1;
            }

            //���߳�Ժʱ�����ݻ����ϴι̶�������ȡʱ�䣬�ж��Ƿ�Ӧ�ò��մ�λ��

            //�������û�д�λ�ţ��򲻽�����ȡ��
            if (pInfo.PVisit.PatientLocation.Bed.ID == null ||
                pInfo.PVisit.PatientLocation.Bed.ID == "")
            {
                return 1;
            }

            ArrayList alFeeInfo = new ArrayList();

            //�ý���ʱ�䴦��
            DateTime dtArriveDate = RadtIntegrate.GetArriveDate(pInfo.ID);
            if (dtArriveDate < pInfo.PVisit.InTime)
            {
                dtArriveDate = pInfo.PVisit.InTime;
            }
            //�������շѲ��մ���
            int days = 0;
            if (pInfo.FT.PreFixFeeDateTime == DateTime.MinValue ||
                pInfo.FT.PreFixFeeDateTime == null ||
                pInfo.FT.PreFixFeeDateTime < dtArriveDate.AddDays(-1).Date.AddHours(23))
            {
                //���û���ϴ���ȡ�̶�������ȡʱ�䣬������
                pInfo.FT.PreFixFeeDateTime = dtArriveDate.AddDays(-1).Date.AddHours(23);
                //return 1;
            }

            days = ((TimeSpan)(dt.Date - pInfo.FT.PreFixFeeDateTime.Date)).Days;
            //��ȡδ��ȡ��ķ���
            days = days - 1;

            //���������ж�����ǻ�δ��ȡ���̶����ã����������ټ�һ
            if (pInfo.FT.PreFixFeeDateTime > dtArriveDate.AddMinutes(-2)
                && pInfo.FT.PreFixFeeDateTime < dtArriveDate.AddMinutes(2))
            {
                days = days + 1;
            }

            #region ��ȡ���δ�λ��

            //�������塢�ﶨ����
            //���ݴ�λ�Ż�ȡ��λ�ȼ�ҵ���
            FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
            //���ݴ�λ�ȼ���ȡ��λ��ȡ�շ��б�ҵ���
            FS.HISFC.BizLogic.Fee.BedFeeItem bedFeeMgr = new FS.HISFC.BizLogic.Fee.BedFeeItem();
            //�����ϴι̶�������ȡʱ��ҵ��㺯��
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            //������Ŀ�����ȡ��Ŀʵ��
            FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
            //���߹�����
            FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();

            //�ﶨ����
            if (this.trans != null)
            {
                bedMgr.SetTrans(this.trans);
                bedFeeMgr.SetTrans(this.trans);
                feeMgr.SetTrans(this.trans);
                itemMgr.SetTrans(this.trans);
                radtInpatient.SetTrans(this.trans);
            }

            DateTime dtCurrent = bedMgr.GetDateTimeFromSysDateTime();

            //���ݴ��Ż�ȡ��λ�ȼ�
            FS.HISFC.Models.Base.Bed objBed = bedMgr.GetBedInfo(pInfo.PVisit.PatientLocation.Bed.ID);
            if (objBed == null)
            {
                this.Err = "��ȡ��λ��Ϣ����" + bedMgr.Err;
                return -1;
            }
            //��ǰ��λ�����ڸû��ߣ�����ȡ
            if (objBed.InpatientNO.Equals(pInfo.ID) == false)
            {
                return 1;
            }

            if (objBed.BedGrade.ID == "")
            {
                this.Err = "���ߴ�λ[" + objBed.ID + "]û��ά���ȼ�";
                return -1;
            }

            ArrayList alBed = new ArrayList();   //��λλ��
            alBed.Add(objBed);     //����

            //��ȡ�����ȷ���
            //����������Ϣ����ٴ���������
            ArrayList alOtherBed = bedMgr.GetOtherBedList(pInfo.ID);
            if (alOtherBed == null)
            {
                this.Err = "��ð�����Ϣ����!" + bedMgr.Err;
                return -1;
            }
            alBed.AddRange(alOtherBed);     //����

            //����λ����
            ArrayList alBedItem = bedFeeMgr.QueryBedFeeItemByMinFeeCode(objBed.BedGrade.ID);
            if (alBedItem == null)
            {
                this.Err = objBed.ID + "��δά���շ���Ŀ!";
                return -1;
            }
            //����λ����
            foreach (FS.HISFC.Models.Base.Bed objBedTemp in alOtherBed)
            {
                ArrayList alTemp = bedFeeMgr.QueryBedFeeItemByMinFeeCode(objBedTemp.BedGrade.ID);
                if (alTemp == null)
                {
                    this.Err = objBedTemp.ID + "��δά���շ���Ŀ!";
                    return -1;
                }
                alBedItem.AddRange(alTemp);
            }
            if (alBedItem.Count == 0)
            {
                return 1;
            }

            DateTime operDate = dt.AddDays(-1); //pInfo.PVisit.PreOutTime.AddDays(-1);

            foreach (FS.HISFC.Models.Base.Bed objBedTemp in alBed)
            {
                alBedItem = new ArrayList();
                alBedItem = bedFeeMgr.QueryBedFeeItemByMinFeeCode(objBedTemp.BedGrade.ID);
                if (alBedItem == null)
                {
                    this.Err = objBedTemp.ID + "��δά���շ���Ŀ!";
                    return -1;
                }
                if (alBedItem.Count == 0)
                {
                    continue;
                }
                ArrayList alNormalItemSupply = new ArrayList();   //������λ��
                ArrayList alBabyItemSupply = new ArrayList();    //Ӥ����λ��
                foreach (FS.HISFC.Models.Fee.BedFeeItem b in alBedItem)
                {
                    if (b.IsBabyRelation)
                    {
                        //Ӥ�����
                        alBabyItemSupply.Add(b);
                    }
                    else
                    {
                        alNormalItemSupply.Add(b);
                    }
                }

                //ѭ����ȡ���̶ֹ�����-������λ����
                foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alNormalItemSupply)
                {
                    if (bedItem.ValidState != EnumValidState.Valid)
                    {
                        continue;
                    }

                    #region �жϷ���Ժ����(����W,�Ҵ�H,���R)�Ƿ���ȡ����Ŀ

                    if (objBedTemp.Status.ID.ToString() == "W" || objBedTemp.Status.ID.ToString() == "H" || objBedTemp.Status.ID.ToString() == "R")
                    {
                        //����շ���Ŀ���ڷ���Ժ���߲���ȡ����,�򲻴������Ŀ
                        if (bedItem.ExtendFlag == "0")
                        {
                            continue;
                        }
                    }

                    #endregion

                    // �жϸ���Ŀ�Ƿ��ʱ���йأ�����յ��ѡ�ȡů��
                    //�����Ŀ��Ӧ����ȡ������һ��ѭ��
                    if (bedItem.IsTimeRelation)
                    {
                        //�������� >= ��ʼ����,��Ϊ�������
                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                        {
                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����
                        }
                        else
                        { //�������� < ��ʼ���� :�����
                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����
                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����
                        }
                    }

                    //��������ݽṹ�д��ڸ��˴�λ�ѵļ۸���ʹ��
                    FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(pInfo.ID, pInfo.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        bedItem.Qty = personFeeItem.Qty;
                        bedItem.ID = personFeeItem.ID;
                        bedItem.Name = personFeeItem.Name;
                    }

                    //���ݴ�λ������Ŀ��ȡ�շ�ʵ��
                    FS.HISFC.Models.Fee.Item.Undrug undrug = itemMgr.GetItemByUndrugCode(bedItem.ID);
                    if (undrug == null)
                    {
                        this.Err = "��λ������Ŀ��" + bedItem.ID + "��ϵͳû���ҵ���";
                        return -1;
                    }
                    if (!undrug.IsValid)
                    {
                        this.Err = "��λ������Ŀ��(" + undrug.UserCode + ")" + undrug.Name + "���Ѿ�ͣ�ã�����ϵ�����ɾ�����ٰ����Ժ��";
                        return -1;
                    }
                    if (undrug.ID == "")
                    {
                        this.Err = "��λ���ö�Ӧ�շ���Ŀû��ά����";
                        return -1;
                    }

                    //������Ŀ�۸�,���ݺ�ͬ��λ����Ŀ����۸�
                    decimal price = undrug.Price;
                    decimal orgPrice = undrug.DefPrice;
                    if (this.GetPriceForInpatient(pInfo, undrug, ref price, ref orgPrice) == -1)
                    //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                    {
                        this.Rollback();
                        this.Err = "��ȡ��Ŀ:" + undrug.ID + "�ļ۸�ʱ����!" + this.Err;
                        return -1;
                    }
                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        price = personFeeItem.Price;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    //�����ķ�����ϸ��������"����" gumzh
                    if (objBedTemp.Status.ID.ToString() == "W")
                    {
                        undrug.Name += "-������";
                    }

                    if (days > 0)
                    {
                        for (int i = 0; i < days; i++)
                        {
                            undrug.Qty = 1;
                            //��ȡ��λ��
                            //220��Ĭ��Ϊ��Ժ���գ������գ�
                            //���û��ߵ�ǰ���ң��õ�ǰϵͳ��¼����
                            // if (this.FeeAutoItem(pInfo, undrug,"220",pInfo.PVisit.PatientLocation.NurseCell.ID, "000000") == -1)
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                            //ѡ��ĳ�Ժ���ڴ��ڽ��죬������״̬��Ϊ210���Ա��Զ��˷�
                            if (pInfo.FT.PreFixFeeDateTime.AddDays(i + 1) > dtCurrent)
                            {
                                if (this.FeeAutoItem(pInfo, undrug, "210", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", pInfo.FT.PreFixFeeDateTime.AddDays(i + 1), ref itemList) == -1)
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                if (this.FeeAutoItem(pInfo, undrug, "220", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", pInfo.FT.PreFixFeeDateTime.AddDays(i + 1), ref itemList) == -1)
                                {
                                    return -1;
                                }
                            }
                            alFeeInfo.Add(itemList);
                        }
                    }

                    //�������Ժ=��Ժ ��������β�����������һ��
                    if (dtArriveDate.Date == dt.Date || bedItem.IsOutFeeFlag)
                    {
                        undrug.Qty = 1;
                        //��ȡ��λ�� AAAAAA�����˷��ã���Ժ�Ǽ��գ��ٻ���
                        //210�Զ��շѣ�Ĭ��Ϊ��Ժ�Ǽǲ��գ�
                        //���û��ߵ�ǰ���ң��õ�ǰϵͳ��¼����
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                        //if (this.FeeAutoItem(pInfo, undrug,"210", pInfo.PVisit.PatientLocation.NurseCell.ID, "000000") == -1)
                        if (this.FeeAutoItem(pInfo, undrug, "210", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", this.itemManager.GetDateTimeFromSysDateTime(), ref itemList) == -1)
                        {
                            return -1;
                        }
                        alFeeInfo.Add(itemList);
                    }

                }

                #region Ӥ����λ������ȡ

                if (pInfo.IsHasBaby && alBabyItemSupply.Count > 0 && objBedTemp.Status.ID.ToString() != "W")
                {
                    #region Ӥ����λ�Ѵ���

                    try
                    {
                        //��ø�ĸ�׵����е���ЧӤ��-���ص���Ӥ�����������Ϣ
                        ArrayList alBaby = radtInpatient.QueryBabiesByMother(pInfo.ID);
                        if (alBaby != null && alBaby.Count > 0)
                        {
                            for (int k = 0; k < alBaby.Count; k++)
                            {
                                FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[k] as FS.HISFC.Models.RADT.PatientInfo;

                                if (babyInfo.FT.PreFixFeeDateTime == null || babyInfo.FT.PreFixFeeDateTime == DateTime.MinValue
                                        || babyInfo.FT.PreFixFeeDateTime.Date <= new DateTime(2000, 1, 1, 0, 0, 0))
                                {
                                    //���û���ϴ���ȡ�̶�������ȡʱ�䣬��ͨ��������ȡ�Ĺ̶�ʱ����ȡ
                                    DateTime dtBabyReg = babyInfo.PVisit.InTime.Date;//Ӥ���Ǽ����ڣ�������ȡ�̶���������
                                    babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyReg.Year, dtBabyReg.Month, dtBabyReg.Day, 23, 30, 0);
                                }
                                else
                                {
                                    //������ϴ���ȡ�̶�������ȡʱ�䣬��+1����ȡ�̶��������ڣ�ͬʱȡʱ���Ϊ23:30:00
                                    DateTime dtBabyLast = babyInfo.FT.PreFixFeeDateTime.Date.AddDays(1);
                                    babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyLast.Year, dtBabyLast.Month, dtBabyLast.Day, 23, 30, 0);
                                }

                                //��Ӥ����������֮ǰ�Ĵ�λ���ò�Ӧ����ȡ,ͬʱ֮ǰû����ȡ��Ҳ��Ҫ����ȡ
                                System.TimeSpan spanDay = dt.Date - babyInfo.FT.PreFixFeeDateTime.Date;
                                int addDays = spanDay.Days;

                                #region ������ȡ����

                                foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alBabyItemSupply)
                                {
                                    //�ж��Ƿ���Ч
                                    if (bedItem.ValidState != EnumValidState.Valid)
                                    {
                                        continue;
                                    }

                                    #region �жϷ���Ժ����(����W,�Ҵ�H,���R)�Ƿ���ȡ����Ŀ

                                    if (objBedTemp.Status.ID.ToString() == "W" || objBedTemp.Status.ID.ToString() == "H" || objBedTemp.Status.ID.ToString() == "R")
                                    {
                                        //����շ���Ŀ���ڷ���Ժ���߲���ȡ����,�򲻴������Ŀ
                                        if (bedItem.ExtendFlag == "0")
                                        {
                                            continue;
                                        }
                                    }

                                    #endregion

                                    #region �жϸ���Ŀ�Ƿ��ʱ���йأ�����յ��ѡ�ȡů��

                                    //�����Ŀ��Ӧ����ȡ������һ��ѭ��
                                    if (bedItem.IsTimeRelation)
                                    {
                                        //�������� >= ��ʼ����,��Ϊ�������
                                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                        {
                                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����
                                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                                continue;//--��ǰ�����ڼƷ���Ч����
                                        }
                                        else
                                        {
                                            //�������� < ��ʼ���� :�����
                                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����
                                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                                continue;//--��ǰ�����ڼƷ���Ч����
                                        }
                                    }

                                    #endregion

                                    //��������ݽṹ�д��ڸ��˴�λ�ѵļ۸���ʹ��
                                    FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(babyInfo.ID, babyInfo.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                                    {
                                        bedItem.Qty = personFeeItem.Qty;
                                        bedItem.ID = personFeeItem.ID;
                                        bedItem.Name = personFeeItem.Name;
                                    }

                                    //���ݴ�λ������Ŀ��ȡ�շ�ʵ��
                                    FS.HISFC.Models.Fee.Item.Undrug undrug = itemMgr.GetItemByUndrugCode(bedItem.ID);
                                    if (undrug == null)
                                    {
                                        this.Err = "��λ������Ŀ��" + bedItem.ID + "��ϵͳû���ҵ���";
                                        return -1;
                                    }
                                    if (!undrug.IsValid)
                                    {
                                        this.Err = "��λ������Ŀ��(" + undrug.UserCode + ")" + undrug.Name + "���Ѿ�ͣ�ã�����ϵ�����ɾ�����ٰ����Ժ��";
                                        return -1;
                                    }
                                    if (undrug.ID == "")
                                    {
                                        this.Err = "��λ���ö�Ӧ�շ���Ŀû��ά����";
                                        return -1;
                                    }
                                    //������Ŀ�۸�,���ݺ�ͬ��λ����Ŀ����۸�
                                    decimal price = undrug.Price;
                                    decimal orgPrice = undrug.DefPrice;
                                    if (this.GetPriceForInpatient(babyInfo, undrug, ref price, ref orgPrice) == -1)
                                    {
                                        this.Rollback();
                                        this.Err = "��ȡ��Ŀ:" + undrug.ID + "�ļ۸�ʱ����!" + this.Err;
                                        return -1;
                                    }
                                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                                    {
                                        price = personFeeItem.Price;
                                    }
                                    undrug.Price = price;
                                    undrug.DefPrice = orgPrice;
                                    //�����ķ�����ϸ��������"����" gumzh
                                    if (objBedTemp.Status.ID.ToString() == "W")
                                    {
                                        undrug.Name += "-������";
                                    }

                                    if (addDays > 0)
                                    {
                                        for (int i = 0; i < addDays; i++)
                                        {
                                            undrug.Qty = 1;
                                            //��ȡ��λ��
                                            //220��Ĭ��Ϊ��Ժ���գ������գ�
                                            //���û��ߵ�ǰ���ң��õ�ǰϵͳ��¼����
                                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                                            //ѡ��ĳ�Ժ���ڴ��ڽ��죬������״̬��Ϊ210���Ա��Զ��˷�
                                            if (babyInfo.FT.PreFixFeeDateTime.AddDays(i + 1) > dtCurrent)
                                            {
                                                if (this.FeeAutoItem(babyInfo, undrug, "210", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", babyInfo.FT.PreFixFeeDateTime.AddDays(i + 1), ref itemList) == -1)
                                                {
                                                    return -1;
                                                }
                                            }
                                            else
                                            {
                                                if (this.FeeAutoItem(babyInfo, undrug, "220", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", babyInfo.FT.PreFixFeeDateTime.AddDays(i + 1), ref itemList) == -1)
                                                {
                                                    return -1;
                                                }
                                            }
                                            alFeeInfo.Add(itemList);
                                        }
                                    }

                                    //�������Ժ=��Ժ ��������β�����������һ��
                                    if (babyInfo.PVisit.InTime.Date == dt.Date || bedItem.IsOutFeeFlag)
                                    {
                                        undrug.Qty = 1;
                                        //��ȡ��λ�� AAAAAA�����˷��ã���Ժ�Ǽ��գ��ٻ���
                                        //210�Զ��շѣ�Ĭ��Ϊ��Ժ�Ǽǲ��գ�
                                        //���û��ߵ�ǰ���ң��õ�ǰϵͳ��¼����
                                        FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                                        if (this.FeeAutoItem(babyInfo, undrug, "210", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", this.itemManager.GetDateTimeFromSysDateTime(), ref itemList) == -1)
                                        {
                                            return -1;
                                        }
                                        alFeeInfo.Add(itemList);
                                    }


                                }

                                #endregion
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ȡӤ����λ��ʧ��!" + ex.Message;
                        return -1;
                    }

                    #endregion
                }

                #endregion

            }

            #region HL7��Ϣ����
            if (alFeeInfo.Count > 0)
            {
                object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
                if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
                {
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                    int param = curIOrderControl.SendFeeInfo(pInfo, alFeeInfo, true);
                    if (param == -1)
                    {
                        this.Rollback();
                        this.Err = curIOrderControl.Err;
                        return -1;
                    }
                }
            }

            #endregion

            //���¹̶�������ȡʱ��Ϊ��Ժ���ڵ�ǰһ��
            //�����ϴι̶�������ȡʱ��+������ȡ������            
            pInfo.FT.PreFixFeeDateTime = pInfo.FT.PreFixFeeDateTime.AddDays(days);
            if (feeMgr.UpdateFixFeeDateByPerson(pInfo.ID, pInfo.FT.PreFixFeeDateTime.ToString()) == -1)
            {
                this.Err = "�����ϴι̶�������ȡʱ�����" + feeMgr.Err;
                return -1;
            }

            //���º��ӵĹ̶�������ȡʱ����ĸ��ͬһʱ��
            if (pInfo.IsHasBaby)
            {
                //��ø�ĸ�׵����е���ЧӤ��-���ص���Ӥ�����������Ϣ
                ArrayList alBaby = radtInpatient.QueryBabiesByMother(pInfo.ID);
                if (alBaby != null && alBaby.Count > 0)
                {
                    for (int k = 0; k < alBaby.Count; k++)
                    {
                        FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[k] as FS.HISFC.Models.RADT.PatientInfo;
                        babyInfo.FT.PreFixFeeDateTime = pInfo.FT.PreFixFeeDateTime;
                        if (feeMgr.UpdateFixFeeDateByPerson(babyInfo.ID, babyInfo.FT.PreFixFeeDateTime.ToString()) == -1)
                        {
                            this.Err = "�����ϴι̶�������ȡʱ�����" + feeMgr.Err;
                            return -1;
                        }
                    }
                }

            }

            return 1;

            #endregion
        }

        /// <summary>
        /// ��Ժ�ٻ��Զ��˲��յķ���
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public int QuitSupplementFee(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            //�����ϴι̶�������ȡʱ��ҵ��㺯��
            DateTime operDate = inpatientManager.GetDateTimeFromSysDateTime();

            //���ҷ�����Ϊ21%�ķ�����Ϣ����Ϊ�ǳ�Ժ�Ǽ�ʱ���յķ��ã������ٻ�ʱ�����˷ѣ���������
            ArrayList alFee = inpatientManager.QueryFeeItemLists(pInfo.ID, new FS.HISFC.Models.Fee.Inpatient.FTSource("21%"));
            if (alFee == null)
            {
                this.Err = "��ȡ��Ժ���շ�����Ϣ����" + inpatientManager.Err;
                return -1;
            }

            if (alFee.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFee)
                {
                    //����Ϊ��Ĳ�����
                    if (feeItemList.NoBackQty <= 0)
                    {
                        continue;
                    }

                    //�����׵Ĳ�����
                    if (feeItemList.TransType == TransTypes.Negative)
                    {
                        continue;
                    }

                    //����״̬Ϊ1�Ĳ�����
                    if (feeItemList.BalanceState == "1")
                    {
                        continue;
                    }

                    if (feeItemList.Item.PackQty == 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }

                    feeItemList.Item.Qty = feeItemList.NoBackQty;
                    feeItemList.NoBackQty = 0;
                    feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                    feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                    feeItemList.IsNeedUpdateNoBackQty = true;

                    if (this.QuitItem(pInfo, feeItemList) == -1)
                    {
                        this.Err = "�˷�ʧ�ܣ�" + this.Err;
                        return -1;
                    }
                }
            }

            //���¹̶�������ȡʱ��Ϊ�ٻ����ڵ�ǰһ��
            //�����Ͳ�����ȡ�м�ķ���
            if (pInfo.FT.PreFixFeeDateTime > DateTime.MinValue)
            {
                pInfo.FT.PreFixFeeDateTime = operDate.Date.AddMinutes(-30);

                if (inpatientManager.UpdateFixFeeDateByPerson(pInfo.ID, pInfo.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Err = "�����ϴι̶�������ȡʱ�����" + inpatientManager.Err;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        ///  �Զ��շѺ���
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="item"></param>
        /// <param name="execDept"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int FeeAutoItem(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Base.Item item, string ftsource,
            string execDept, string operCode)
        {
            return FeeAutoItem(pInfo, item, ftsource, execDept, operCode, this.itemManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        ///  �Զ��շѺ���
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="item"></param>
        /// <param name="execDept"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int FeeAutoItem(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Base.Item item, string ftsource,
            string execDept, string operCode, DateTime chargeDate)
        {
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = null;

            //���ݴ����ʵ�崦��۸�
            decimal price = 0;
            decimal orgPrice = 0;

            if (this.GetPriceForInpatient(pInfo, item, ref price, ref orgPrice) == -1)
            {
                this.Err = "ȡ��Ŀ:" + item.Name + "�ļ۸����!" + this.Err;
                return -1;
            }
            item.Price = price;
            // ԭʼ�ܷ��ã�����Ӧ�շ��ã������Ǻ�ͬ��λ���أ�
            item.DefPrice = orgPrice;

            return this.FeeAutoItem(pInfo, item, ftsource, execDept, operCode, chargeDate, ref feeItemList);
        }


        /// <summary>
        ///  �Զ��շѺ�������������ȡ�۸�
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="item"></param>
        /// <param name="execDept"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int FeeAutoItem(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Base.Item item, string ftsource,
            string execDept, string operCode, DateTime chargeDate, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

            pactUnitManager.SetTrans(this.trans);


            DateTime dtNow = pactUnitManager.GetDateTimeFromSysDateTime();

            ItemList.Item = item;
            //��Ժ����
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //��ʿվ
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            //ִ�п���
            ItemList.ExecOper.Dept.ID = execDept;
            //�ۿ����
            ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //��������
            ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //����ҽ��
            ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //ҽ��

            //ҩƷĬ�ϰ���С��λ�շ�,��ʾ�۸�ҲΪ��С��λ�۸�,�������ݿ��Ϊ��װ��λ�۸�
            //if (item.IsPharmacy)//ҩƷ
            if (item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                item.Price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);

                // {54B0C254-3897-4241-B3BD-17B19E204C8C}
                item.DefPrice = FS.FrameWork.Public.String.FormatNumber(item.DefPrice / item.PackQty, 4);
            }

            /* �ⲿ�Ѿ���ֵ���֣��۸���������λ���Ƿ�ҩƷ
             * ItemList.Item.Price = 0;ItemList.Item.Qty;  
             * ItemList.Item.PriceUnit = "��"; 
             * ItemList.Item.IsPharmacy = false;
             */
            if (item.PackQty == 0)
            {
                item.PackQty = 1;
            }
            ItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * item.Price / item.PackQty, 2);
            ItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * item.DefPrice / item.PackQty, 2);
            ItemList.FT.OwnCost = ItemList.FT.TotCost;

            ItemList.PayType = PayTypes.Balanced;
            ItemList.IsBaby = false;
            ItemList.BalanceNO = 0;
            ItemList.BalanceState = "0";
            //��������
            ItemList.NoBackQty = item.Qty;

            //����Ա
            ItemList.FeeOper.ID = operCode;
            ItemList.ChargeOper.ID = operCode;
            ItemList.ChargeOper.OperTime = chargeDate;
            ItemList.FeeOper.OperTime = dtNow;

            ItemList.FT.OwnCost = ItemList.FT.TotCost;
            ItemList.TransType = TransTypes.Positive;

            //������Դ
            ItemList.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource(ftsource);

            feeItemList = ItemList;

            //�����շѺ���
            return this.FeeItem(pInfo, ItemList);
        }

        #endregion

        #region סԺ�˷������ֱ���˷�����

        /// <summary>
        ///  �˷Ѻ��������������ֱ���˷ѣ�
        /// </summary>
        /// <param name="patientInfo">������Ϣ</param>
        /// <param name="feeItemList">������Ϣ</param>
        /// <param name="isApply">�Ƿ����룬����Ϊֱ���˷�</param>
        /// <param name="applyBillCode">���뵥��</param>
        /// <returns></returns>
        public int QuitFeeApply(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, bool isApply, string applyBillCode, DateTime quitFeeDateTime, ref string msg)
        {
            //��ȡ����ʱ��
            if (quitFeeDateTime <= DateTime.MinValue)
            {
                quitFeeDateTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            }

            //���
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
            if (feeItemListTemp == null)
            {
                this.Err = "�����Ŀ������Ϣ����!" + this.inpatientManager.Err;
                return -1;
            }
            //�жϿ�������
            if (feeItemListTemp.NoBackQty <= 0)//˵��û�п��˵�
            {
                this.Err = string.Format("��Ŀ��{0}{1}�ķ����Ѿ�û�п��˵�����������Ҫ�ظ��˷ѣ�\r\n\r\n����ԭ���ѽ����ֹ��˷ѣ�", feeItemListTemp.Item.Name, string.IsNullOrEmpty(feeItemListTemp.UndrugComb.Name) ? "" : "��" + feeItemListTemp.UndrugComb.Name + "��");
                return -1;
            }
            else if (feeItemListTemp.NoBackQty < feeItemListTemp.Item.Qty)//˵���Ѿ��������ˣ�����Ҫ���ظ�����
            {
                this.Err = string.Format("��Ŀ��{0}{1}�ķ����Ѿ�����������{2}������ȡ�������ȷ�Ϻ����˷ѣ�", feeItemListTemp.Item.Name, string.IsNullOrEmpty(feeItemListTemp.UndrugComb.Name) ? "" : "��" + feeItemListTemp.UndrugComb.Name + "��", feeItemListTemp.Item.Qty - feeItemListTemp.NoBackQty);
                return -1;
            }

            if (isApply)
            {
                #region �˷�����

                //�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }

                if (feeItemList.MateList.Count > 0)
                {
                    feeItemListTemp.MateList = feeItemList.MateList;
                }
                //���˷ѵ�����д��¼
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
                    return 1;
                }

                //���·��ñ��еĿ��������ֶ�
                //�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                string errMsg = string.Empty;
                int returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                if (returnValue == -1)
                {
                    this.Err = errMsg;
                    return -1;
                }

                //��ʱ��Ŀ�����˷������
                feeItemListTemp.User02 = applyBillCode;

                //�����ҩƷ�����Ѿ���ҩ���������ҩ���룻��������˷����롣
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    #region �Ѱ�ҩ���
                    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    {
                        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    }

                    feeItemListTemp.Item.Memo = feeItemList.Item.Memo;

                    if (this.PharmarcyManager.ApplyOutReturn(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "��ҩ����ʧ��!" + this.PharmarcyManager.Err;
                        return -1;
                    }

                    #endregion  //end �Ѱ�ҩ���

                }
                else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�����
                {
                    #region δ��ҩ���
                    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    if (feeItemList.FTRate.ItemRate != 0)
                    {
                        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    }

                    if (this.returnMgr.Apply(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "�����˷�����ʧ��!" + this.returnMgr.Err;
                        return -1;
                    }


                    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        //ȡ��ҩ�����¼���ж���״̬�Ƿ���������������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (applyOut == null)
                        {
                            this.Err = "���������Ϣ����!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                        if (applyOut.ID == string.Empty)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //����
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "�������ѱ���������Ա�˷ѣ�����ִ��ȷ���˷Ѻ�������!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //���ϰ�ҩ����
                        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            this.Err = "���ϰ�ҩ�������!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                        if (returnValue == 0)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "���Ѱ�ҩ�������¼�������" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                        {
                            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                this.Err = "���������Ϣ����!" + this.PharmarcyManager.Err;
                                return -1;
                            }
                            applyOutTemp.RecipeNO = feeItemList.RecipeNO;
                            applyOutTemp.SequenceNO = feeItemList.SequenceNO;//����չ�ֶα���ԭʼ��������ˮ��
                            applyOutTemp.Operation.ApplyOper.OperTime = quitFeeDateTime;
                            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬

                            applyOutTemp.ID = "";
                            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                            {
                                this.Err = "���²��뷢ҩ�������!" + this.PharmarcyManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion //end δ��ҩ���
                }

                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                {
                    ArrayList patientDrugStorageList = this.PharmarcyManager.QueryStorageList(patientInfo.ID, feeItemListTemp.Item.ID);
                    if (patientDrugStorageList == null)
                    {
                        this.Err = "�ж��Ƿ���ڻ��߿��ʱ����!" + this.PharmarcyManager.Err;
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

                        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                        {
                            this.Err = "���»��߿�����!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region ֱ���˷�
                //�����ҩ����û�и�ֵ,Ĭ�ϸ�ֵΪ1
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }

                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    #region ҩƷ�ѷ�ҩ �γ���ҩ����

                    //���˷ѵ�����д��¼
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
                        return 1;
                    }

                    //���·��ñ��еĿ��������ֶ�
                    //�����ҩƷ�����ҩƷ����ҩ������������·�ҩƷ
                    string errMsg = string.Empty;
                    int returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                    if (returnValue == -1)
                    {
                        this.Err = errMsg;
                        return -1;
                    }

                    //��ʱ��Ŀ�����˷������
                    feeItemListTemp.User02 = applyBillCode;
                    feeItemListTemp.ExecOrder.DateUse = feeItemList.ExecOrder.DateUse; //��ҩʱ�� 
                    feeItemListTemp.Order.Usage.ID = feeItemList.Order.Usage.ID;//�÷�
                    feeItemListTemp.Order.Usage.Name = feeItemList.Order.Usage.Name;
                    feeItemListTemp.Order.Frequency.ID = feeItemList.Order.Frequency.ID;
                    feeItemListTemp.Order.Frequency.Name = feeItemList.Order.Frequency.Name;
                    feeItemListTemp.Order.DoseOnce = feeItemList.Order.DoseOnce;
                    feeItemListTemp.Order.DoseUnit = feeItemList.Order.DoseUnit;



                    //��ҩ����,ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    {
                        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    }
                    if (this.PharmarcyManager.ApplyOutReturn(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "��ҩ����ʧ��!" + this.PharmarcyManager.Err;
                        return -1;
                    }

                    #endregion

                    msg += feeItemListTemp.Item.Name + "\n";
                }
                else//���ڷ�ҩƷ��δ��ҩ��ҩƷ��ֱ�����˷�
                {
                    //ʹ�����ݿ���ȡ�õ�ʵ����û�����������
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    if (feeItemList.FTRate.ItemRate != 0)
                    {
                        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    }

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListByQuit = feeItemList.Clone();
                    //ֱ���˷�
                    if (this.QuitItem(patientInfo, feeItemListByQuit) == -1)
                    {
                        return -1;
                    }

                    //û�а�ҩ��ҩƷ���˷������ͬʱ�����ϰ�ҩ����
                    //if (feeItemListTemp.Item.IsPharmacy)
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        #region ȡ��ҩ�����¼���ж���״̬�Ƿ���������
                        //������CancelApplyOut���жϲ�������Ϊ��Щ�շѺ��ҽ��û�з��͵�ҩ���������ڰ�ҩ�����¼��

                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (applyOut == null)
                        {
                            this.Err = "���������Ϣ����!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //���ȡ����ʵ��IDΪ""�����ʾҽ����δ���͡�δ���͵�ҽ���������˷ѣ���Ȼ����ʱҩ����Դ��˷ѵ���Ŀ���з�ҩ��
                        if (applyOut.ID == string.Empty)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "��û�з��͵�ҩ�������ȷ��ͣ�Ȼ�������˷�����!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //����
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "���ѱ���������Ա�˷ѣ���ˢ�µ�ǰ����!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //���ϰ�ҩ����
                        int returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            this.Err = "���ϰ�ҩ�������!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                        if (returnValue == 0)
                        {
                            this.Err = "��Ŀ��" + feeItemListTemp.Item.Name + "���Ѱ�ҩ�������¼�������!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        #endregion

                        //����ǲ����˷�(�û���ҩ������С�ڷ��ñ��еĿ�������),Ҫ��ʣ���ҩƷ����ҩ����.
                        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                        {
                            #region �����˷����·�������

                            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                this.Err = "���������Ϣ����!" + this.PharmarcyManager.Err;
                                return -1;
                            }

                            //���Ӱ������¸����븳ֵ�����źʹ�������ˮ��
                            applyOutTemp.RecipeNO = feeItemListByQuit.RecipeNO;
                            applyOutTemp.SequenceNO = feeItemListByQuit.SequenceNO;

                            applyOutTemp.Operation.ApplyOper.OperTime = quitFeeDateTime;
                            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//��������ʣ������
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬
                            //��ʣ���������Ͱ�ҩ����  
                            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                            {
                                this.Err = "���²��뷢ҩ�������!" + this.PharmarcyManager.Err;
                                return -1;
                            }

                            #endregion
                        }
                    }
                }

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ������Ŀ�Ŀ�������
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int UpdateNoBackQty(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, ref string errMsg)
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
                errMsg = Language.Msg("��Ŀ��") + feeItemList.Item.Name + Language.Msg("���Ѿ����˷ѣ������ظ��˷ѡ�\r\n\r\n����ԭ���ѽ����ֹ��˷ѣ�");

                return -1;
            }

            return 1;
        }
        #endregion

        #region ��ȡ���￨����
        /// <summary>
        /// ��ȡ���￨����
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int SaveAccountCardFee(ref AccountCardFee cardFee)
        {
            if (cardFee == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            string invoice = string.Empty;
            string print_invoice = string.Empty;
            string strErr = string.Empty;
            bool blnUseInvoice = false;
            int iRes = 0;

            string invoiceType = "R";

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                blnUseInvoice = true;
                iRes = this.GetInvoiceNO(employee, invoiceType, ref invoice, ref print_invoice, ref strErr);
                if (iRes <= 0)
                {
                    this.Err = strErr;
                    return -1;
                }

                cardFee.InvoiceNo = invoice;
                cardFee.Print_InvoiceNo = print_invoice;
            }

            if (cardFee.TransType == TransTypes.Positive)
            {
                cardFee.FeeOper.ID = employee.ID;
                cardFee.FeeOper.OperTime = feeTime;
            }
            cardFee.Oper.ID = employee.ID;
            cardFee.Oper.OperTime = feeTime;

            iRes = 0;
            iRes = accountManager.InsertAccountCardFee(cardFee);
            if (iRes <= 0)
            {
                this.Err = accountManager.Err;
                return -1;
            }

            if (!blnUseInvoice && cardFee.TransType == TransTypes.Negative)
            {
                // �˷�
                iRes = accountManager.CancelAccountCardFee(cardFee.InvoiceNo, cardFee.TransType, cardFee.FeeType);
                if (iRes <= 0)
                {
                    this.Err = accountManager.Err;
                    return -1;
                }
            }

            if (blnUseInvoice)
            {
                invoiceStytle = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
                if (this.UseInvoiceNO(employee, invoiceStytle, invoiceType, 1, ref invoice, ref print_invoice, ref strErr) < 0)
                {
                    return -1;
                }

                if (this.InsertInvoiceExtend(invoice, invoiceType, print_invoice, "00") < 1)
                {
                    // ��Ʊͷ��ʱ�ȱ���00
                    this.Err = this.invoiceServiceManager.Err;
                    return -1;
                }
            }

            return iRes;
        }

        public int SaveAccountCardFee(List<AccountCardFee> lstCardFee)
        {
            if (lstCardFee == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            int iRes = 0;
            foreach (AccountCardFee cardFee in lstCardFee)
            {
                iRes = accountManager.InsertAccountCardFee(cardFee);
                if (iRes <= 0)
                {
                    this.Err = accountManager.Err;
                    return -1;
                }
            }

            return iRes;
        }

        public int SaveAccountCardFee(ref AccountCardFee cardFee, bool needPrint)
        {
            if (cardFee == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            string invoice = string.Empty;
            string print_invoice = string.Empty;
            string strErr = string.Empty;
            bool blnUseInvoice = false;
            int iRes = 0;

            string invoiceType = "R";

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                blnUseInvoice = true;

                iRes = this.GetInvoiceNO(employee, invoiceType, ref invoice, ref print_invoice, ref strErr);

                if (iRes <= 0)
                {
                    this.Err = strErr;
                    return -1;
                }

                cardFee.InvoiceNo = invoice;
                cardFee.Print_InvoiceNo = print_invoice;

                if (needPrint == false)
                {
                    blnUseInvoice = false;
                    cardFee.Print_InvoiceNo = "";
                }
            }

            if (cardFee.TransType == TransTypes.Positive)
            {
                cardFee.FeeOper.ID = employee.ID;
                cardFee.FeeOper.OperTime = feeTime;
            }
            cardFee.Oper.ID = employee.ID;
            cardFee.Oper.OperTime = feeTime;

            iRes = 0;
            iRes = accountManager.InsertAccountCardFee(cardFee);
            if (iRes <= 0)
            {
                this.Err = accountManager.Err;
                return -1;
            }

            if (!blnUseInvoice && cardFee.TransType == TransTypes.Negative)
            {
                // �˷�
                iRes = accountManager.CancelAccountCardFee(cardFee.InvoiceNo, cardFee.TransType, cardFee.FeeType);
                if (iRes <= 0)
                {
                    this.Err = accountManager.Err;
                    return -1;
                }
            }

            if (blnUseInvoice)
            {
                invoiceStytle = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
                if (this.UseInvoiceNO(employee, invoiceStytle, invoiceType, 1, ref invoice, ref print_invoice, ref strErr) < 0)
                {
                    return -1;
                }

                if (this.InsertInvoiceExtend(invoice, invoiceType, print_invoice, "00") < 1)
                {
                    // ��Ʊͷ��ʱ�ȱ���00
                    this.Err = this.invoiceServiceManager.Err;
                    return -1;
                }
            }

            return iRes;
        }

        #endregion

        //{282BD4C3-4086-4d4c-BE3D-68FC3205E4B7}
        #region �Һ���ϸ���ú�֧����ʽ

        public int SaveRegFeeList(ArrayList detailFeeList)
        {
            if (detailFeeList == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            int iRes = 0;
            foreach (HISFC.Models.Registration.RegisterFeeDetail feedetail in detailFeeList)
            {
                iRes = this.regDetailMgr.Insert(feedetail);
                if (iRes <= 0)
                {
                    this.Err = regDetailMgr.Err;
                    return -1;
                }
            }

            return iRes;
        }

        //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
        public int SaveRegPayModeList(HISFC.Models.Registration.Register regObj, ArrayList payModeList)
        {
            if (payModeList == null)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            int iRes = 0;
            //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
            FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
            //FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("JFZFFS", "1");
            //FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

            //decimal cashCouponAmount = 0.0m;

            FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            foreach (HISFC.Models.Registration.RegisterPayMode payMode in payModeList)
            {
                iRes = this.regPayModeMgr.Insert(payMode);
                if (iRes <= 0)
                {
                    this.Err = regPayModeMgr.Err;
                    return -1;
                }

                //���ػ�����ͣ��
                //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                //�жϸ�֧����ʽ�Ƿ�������
                //if (obj.Name.Contains(payMode.Mode_Code.ToString()))
                //{
                //    if (accountPay.UpdateCoupon(regObj.PID.CardNO, payMode.Tot_cost, payMode.InvoiceNo) <= 0)
                //    {
                //        this.Err = "������ֳ���!" + accountPay.Err;
                //        return -1;
                //    }
                //}

                //if (cashCouponPayMode.Name.Contains(payMode.Mode_Code.ToString()))
                //{
                //    cashCouponAmount += payMode.Tot_cost;
                //}
            }

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (cashCouponAmount > 0 || cashCouponAmount < 0)
            //{
            //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    string errText = string.Empty;
            //    if (cashCouponPrc.CashCouponSave("GHSF", regObj.PID.CardNO, regObj.InvoiceNO, cashCouponAmount, ref errText) <= 0)
            //    {
            //        this.Err = "�����ֽ������ֳ���!" + errText;
            //        return -1;
            //    }

            //}

            return iRes;
        }

        #endregion

        #region �˷�����ҵ��

        /// <summary>
        /// ���ݻ���סԺ��ˮ�ţ�ȡ�˻��ߵ��˷���������,�����������Ƿ�ȷ��
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="isCharged">�Ƿ�ȷ�ϵ�����</param>
        /// <returns>�ɹ�:�˷��������� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged)
        {
            this.SetDB(returnMgr);
            return returnMgr.QueryReturnApplys(inpatientNO, isCharged);
        }

        #endregion

        #region ��ݱ��ʱ���Ե�����¼���д��� add xf
        /// <summary>
        /// ���ݻ����µĺ�ͬ��λ�����»�����ʷδ����ĵ�����¼
        /// һ��������ݱ��ʱ�ɹ��ѱ��Ϊ������ͬ��λ��
        /// </summary>
        /// <param name="pInfo">���߻�����Ϣ</param>
        /// <param name="isPhamacy">�Ƿ�ҩƷ</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRate(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isPhamacy)
        {
            /**
             * ���ݻ��ߺ�ͬ��λ��ȡ��ͬ��λ�Ļ�������
             * 1����������µĺ�ͬ��λ��ҽ�������Է�
             * ��ԭ������¼û���ã�ɾ�����ߵĵ�����¼
             * 2����������µĺ�ͬ��λ�ǹ���
             * �����µĺ�ͬ��λ�������µ�����¼��
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //��ȡ�µĺ�ͬ��λ�ı���
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //���µ�����¼
                return inpateintMgr.UpdateAdjustedItem(pInfo.ID, payRate, isPhamacy);
            }
            return 1;
        }

        /// <summary>
        /// ���ݻ����µĺ�ͬ��λ�����»�����ʷδ����ĵ�����¼--FeeInfo
        /// </summary>
        /// <param name="pInfo">���߻�����Ϣ</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRateFeeinfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            /**
             * ���ݻ��ߺ�ͬ��λ��ȡ��ͬ��λ�Ļ�������
             * 1����������µĺ�ͬ��λ��ҽ�������Է�
             * ��ԭ������¼û���ã�ɾ�����ߵĵ�����¼
             * 2����������µĺ�ͬ��λ�ǹ���
             * �����µĺ�ͬ��λ�������µ�����¼��
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //��ȡ�µĺ�ͬ��λ�ı���
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //���µ�����¼
                return inpateintMgr.UpdateAdustedFeeInfo(pInfo.ID, payRate);
            }
            return 1;
        }

        /// <summary>
        /// ���ݻ����µĺ�ͬ��λ�����»�����ʷδ����ĵ�����¼
        /// һ��������ݱ��ʱ�ɹ��ѱ��Ϊ������ͬ��λ��
        /// </summary>
        /// <param name="pInfo">���߻�����Ϣ</param>
        /// <param name="isPhamacy">�Ƿ�ҩƷ</param>
        /// <param name="isPhamacy">�Ƿ�ɾ�����е�����¼�����º�ͬ��λ��ԭ��ͬ��λ�޶���ʱ���ã�</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRate(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isPhamacy, bool isDeteleAllAdjustedItem)
        {
            /**
             * ���ݻ��ߺ�ͬ��λ��ȡ��ͬ��λ�Ļ�������
             * 0����������µĺ�ͬ��λ�ǹ��������޶���ԭ��ͬ��λ���޶ͬʱ��ɾ�����ߵĵ�����¼
             * 1����������µĺ�ͬ��λ��ҽ�������Է�
             * ��ԭ������¼û���ã�ɾ�����ߵĵ�����¼
             * 2����������µĺ�ͬ��λ�ǹ���
             * �����µĺ�ͬ��λ�������µ�����¼��
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (isDeteleAllAdjustedItem)
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);
            }
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //��ȡ�µĺ�ͬ��λ�ı���
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //���µ�����¼
                return inpateintMgr.UpdateAdjustedItem(pInfo.ID, payRate, isPhamacy);
            }
            return 1;
        }

        /// <summary>
        /// ���ݻ����µĺ�ͬ��λ�����»�����ʷδ����ĵ�����¼--FeeInfo
        /// </summary>
        /// <param name="pInfo">���߻�����Ϣ</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRateFeeinfo(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isDeteleAllAdjustedItem)
        {
            /**
             * ���ݻ��ߺ�ͬ��λ��ȡ��ͬ��λ�Ļ�������
                      * 0����������µĺ�ͬ��λ�ǹ��������޶���ԭ��ͬ��λ���޶ͬʱ��ɾ�����ߵĵ�����¼
             * 1����������µĺ�ͬ��λ��ҽ�������Է�
             * ��ԭ������¼û���ã�ɾ�����ߵĵ�����¼
             * 2����������µĺ�ͬ��λ�ǹ���
             * �����µĺ�ͬ��λ�������µ�����¼��
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (isDeteleAllAdjustedItem)
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);
            }
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //ɾ��������¼
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //��ȡ�µĺ�ͬ��λ�ı���
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //���µ�����¼
                return inpateintMgr.UpdateAdustedFeeInfo(pInfo.ID, payRate);
            }
            return 1;
        }

        #endregion

        #region �̶�����

        public int DoBedItemFee(ArrayList bedItems,
            FS.HISFC.Models.RADT.PatientInfo patient, int days, DateTime operDate, DateTime chargeDate, FS.HISFC.Models.Base.Bed bed)
        {
            //��ҩƷ������
            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //��ͬ������


            FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //����������


            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            //����
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.feeInpatient.Connection);
            //trans.BeginTransaction();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            item.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            pactMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            constant.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                ArrayList alFeeInfo = new ArrayList();
                //��λ��Ϣʵ��
                FS.HISFC.Models.Fee.BedFeeItem bedItem = new FS.HISFC.Models.Fee.BedFeeItem();
                for (int row = 0; row < bedItems.Count; row++)
                {
                    //ȡ����ȡ�Ĵ�λ��Ϣ


                    bedItem = bedItems[row] as FS.HISFC.Models.Fee.BedFeeItem;

                    //�����λ��Ч���򲻽��з�����ȡ


                    if (bedItem.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid) continue;

                    //�رյĴ�λ���մ�λ��.ת�ƺ��ͷŴ�λʱ��λ״̬��ΪC . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
                    if (bed.Status.ID.ToString() == "C")
                    {
                        continue;
                    }

                    #region �жϷ���Ժ����(����W,�Ҵ�H,���R)�Ƿ���ȡ����Ŀ  writed by cuipeng  2005-11
                    if (bed.Status.ID.ToString() == "W" || bed.Status.ID.ToString() == "H" || bed.Status.ID.ToString() == "R")
                    {
                        //����շ���Ŀ���ڷ���Ժ���߲���ȡ����,�򲻴������Ŀ


                        if (bedItem.ExtendFlag == "0")
                        {
                            continue;
                        }
                        else
                        {
                            //��ɽ���� �������ݿ�������������ʱ����


                            //���ڰ�������,�̶�������ȡ����Ϊ"���˷�",���Ϊ��λ�ѵ�2��.
                            if (bed.Status.ID.ToString() == "W")
                            {
                                FS.FrameWork.Models.NeuObject obj = constant.GetConstant("FIN_FIXITEM", "BEDWRAP");
                                //if (obj == null)
                                //{
                                //    this.Err = constant.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////ȡԭ��Ŀ(��λ��)����
                                //FS.HISFC.Models.Fee.Item.Undrug tempItem = item.GetValidItemByUndrugCode(bedItem.ID);

                                //if (tempItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}
                                //FS.HISFC.Models.Fee.Item.Undrug peiItem = item.GetValidItemByUndrugCode(obj.Name);
                                //if (peiItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////ָ���շ���Ŀ����(���˷���Ŀ����)
                                //bedItem.ID = peiItem.ID;
                                //bedItem.Name = peiItem.Name;
                                ////bedItem.ID = obj.Name;

                                ////����Ϊ��λ�ѵ�2��


                                //bedItem.User01 = (tempItem.Price * 2).ToString();

                            }
                        }
                    }
                    #endregion

                    #region �жϸ���Ŀ�Ƿ��ʱ���йأ�����յ��ѡ�ȡů��
                    if (bedItem.IsTimeRelation)
                    {
                        //�������� >= ��ʼ����,��Ϊ�������
                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                        {
                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����


                        }

                        else
                        { //�������� < ��ʼ���� :�����


                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����


                        }
                    }
                    #endregion

                    #region �������ø�Ӥ���йصĹ̶�����,����Ӥ���Ƿ���ڶ��շ�


                    bool isBaby = false;//�Ƿ�Ӥ��,Ĭ�ϲ���Ӥ��
                    //�д���Ժ��Ӥ����λ�Ѵ������HisTimeJob�д�����Ϊĸ�׺�Ӥ���ķ��÷ֿ� gumzh
                    if (false)
                    {
                        if (bedItem.IsBabyRelation)
                        {
                            if (patient.BabyCount == 0)
                                //Ӥ��������,����ȡ�������
                                continue;
                            else
                            {
                                //Ӥ������,ÿ��Ӥ����ȡһ��


                                isBaby = true;
                                bedItem.Qty = bedItem.Qty * patient.BabyCount;
                            }
                        }
                    }

                    #endregion

                    //������Ŀ����,���Ϊ0��Ĭ����1
                    if (bedItem.Qty == 0)
                        bedItem.Qty = 1;
                    //�����û����õ���������,����Ӧ��ȡ����
                    bedItem.Qty = bedItem.Qty * days;
                    //��������ݽṹ�д��ڸ��˴�λ�ѵļ۸���ʹ��
                    FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(patient.ID, patient.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        bedItem.ID = personFeeItem.ID;
                        bedItem.Name = personFeeItem.Name;
                        bedItem.Qty = personFeeItem.Qty;
                    }

                    //ȡ�շ���Ŀʵ����Ϣ
                    FS.HISFC.Models.Fee.Item.Undrug undrug = item.GetValidItemByUndrugCode(bedItem.ID);
                    if (undrug == null)
                    {
                        this.Err = item.Err;
                        continue;
                    }
                    //������Ŀ�۸�,���ݺ�ͬ��λ����Ŀ����۸�
                    decimal price = 0;
                    decimal orgPrice = 0;

                    if (this.GetPriceForInpatient(patient, undrug, ref price, ref orgPrice) == -1)
                    //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                    {
                        this.Rollback();
                        this.Err = "��ȡ��Ŀ:" + undrug.ID + "�ļ۸�ʱ����!" + pactMgr.Err;
                        return -1;
                    }

                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        price = personFeeItem.Price;
                    }

                    //ȡ�õļ۸�Ϊ0,��ʹ��ȡ��ļ۸�
                    if (price != 0)
                    {
                        undrug.Price = price;
                        undrug.DefPrice = orgPrice;
                    }
                    else
                    {
                        undrug.DefPrice = undrug.Price;
                    }

                    //�������۹̶�Ϊ��λ�ѵ�2��. writed by cuipeng 2005-11
                    if (bed.Status.ID.ToString() == "W")
                    {
                        //undrug.Price = FS.FrameWork.Function.NConvert.ToDecimal(bedItem.User01);
                        //�����ķ�����ϸ��������"����" gumzh
                        undrug.Name += "-������";
                    }

                    //�Ʒѵ���Ϊ0, ����Ҫ�Ʒ�
                    if (undrug.Price == 0)
                    {
                        this.Err = "�Ʒѵ���Ϊ0:" + undrug.Name;
                        continue;
                    }


                    undrug.Qty = bedItem.Qty;
                    //ҽ�����߽ӿ�
                    //��ɽһû����Ҫ�����
                    //ʵ�帳ֵ
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    feeItem.IsBaby = isBaby;
                    feeItem.Item = undrug;
                    feeItem.NoBackQty = undrug.Qty;
                    feeItem.RecipeNO = inpatientManager.GetUndrugRecipeNO();
                    feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    //feeItem.Order.InDept.ID =
                    feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.NurseStation.ID = 
                    ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
                    //feeItem.Order.ReciptDept.ID =
                    feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.ExeDept.ID =
                    feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                        patient.PVisit.AdmittingDoctor.ID = "�ռƷ�";

                    //feeItem.Order.ReciptDoctor.ID =
                    feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
                    feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    //feeItem.IsBrought = "";
                    feeItem.ChargeOper.ID = "�ռƷ�";
                    feeItem.ChargeOper.OperTime = chargeDate;
                    feeItem.FeeOper.ID = "�ռƷ�";
                    feeItem.FeeOper.OperTime = operDate;
                    feeItem.SequenceNO = row;
                    feeItem.BalanceNO = 0;
                    feeItem.BalanceState = "0";
                    feeItem.FT.TotCost = undrug.Qty * undrug.Price;
                    if (undrug.PackQty == 0)
                    {
                        undrug.PackQty = 1;
                    }
                    feeItem.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(undrug.Qty * undrug.Price / undrug.PackQty, 2);
                    feeItem.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(undrug.Qty * undrug.DefPrice / undrug.PackQty, 2);
                    feeItem.FT.OwnCost = undrug.Qty * undrug.Price;
                    feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("200");
                    //---------------------------���Ѵ�λ�������0818------------------------
                    #region ���Ѵ�λ�������
                    if (patient.Pact.PayKind.ID == "03")
                    {
                        feeItem.FT.OwnCost = 0;//���һ��Ҫ�ӣ�����ҽ����ȡ�̶����ú��ڵ���������
                        //��λ�޶�
                        decimal BedLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.BedLimitCost * days, 2);
                        //�໤��λ�޶�
                        decimal IcuLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.AirLimitCost * days, 2);

                        /*�ֵ����TYPEΪBEDLIMITMINFEE
                        CODEΪ1Ϊ��ͨ����NAME�д������ͨ����С����CODE
                        CODEΪ2Ϊ�໤����NAME�д���Ǽ໤����С����CODE
                        */
                        FS.FrameWork.Models.NeuObject conBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "1");
                        string bedMinFeeCode = "";
                        if (conBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conBedMinFee.Name))
                            {
                                this.Err = "�����ֵ����ά��typeΪBEDLIMITMINFEE,CODE=1,NAME=��ͨ����С���ô��룡";
                            }
                            bedMinFeeCode = conBedMinFee.Name;//��ͨ����С���ô���
                        }

                        FS.FrameWork.Models.NeuObject conICUBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "2");
                        string icuBedMinFeeCode = "";
                        if (conICUBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conICUBedMinFee.Name))
                            {
                                this.Err = "�����ֵ����ά��typeΪBEDLIMITMINFEE,CODE=2,NAME=�໤����С���ô��룡";
                            }
                            icuBedMinFeeCode = conICUBedMinFee.Name;//�໤����С���ô���
                        }
                        ////�жϵ����Ƿ��Ѿ��չ��յ���
                        //decimal AirFee = 0;
                        //DateTime FeeBegin = new DateTime(operDate.Year, 10, 26, 0, 0, 0);
                        //DateTime FeeEnd = new DateTime(operDate.Year, 4, 26, 0, 0, 0);
                        //if (operDate > FeeBegin || operDate < FeeEnd)
                        //{
                        //    if (this.inpatientManager.GetAirFee(patient.ID, ref AirFee) > 0)//�ֵ��ά���յ�����ĿtypeΪAIRFEEITEM
                        //    {
                        //        BedLimit = BedLimit - AirFee;
                        //    }
                        //}

                        FS.FrameWork.Models.NeuObject billObj = constant.GetConstant("BILLPACT", patient.Pact.ID);

                        #region �жϳ��� �������
                        FS.HISFC.Models.Base.FTRate Rate = this.ComputeFeeRate(patient.Pact.ID, feeItem.Item);
                        if (Rate == null)
                        {
                            return -1;
                        }
                        feeItem.User01 = "1";//�����жϱ����FeeManager�в����µ��ü����������

                        bool computeLimit = true;//��Ŀ�Ƿ�������޶�

                        if (billObj != null && billObj.ID.Length >= 0 && billObj.Name == "�й���")
                        {
                            FS.FrameWork.Models.NeuObject unlimitObj = constant.GetConstant("UNLIMITITEM", feeItem.Item.ID);

                            if (unlimitObj != null && unlimitObj.ID.Length >= 1)
                            {
                                computeLimit = false;
                            }
                        }
                        if (feeItem.Item.MinFee.ID == bedMinFeeCode && computeLimit)
                        {
                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region ��ͨ�����괦��
                                if (patient.FT.BedOverDeal == "1")
                                {//��������
                                    //������
                                    if (feeItem.FT.TotCost <= BedLimit)
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {//���겿��תΪ�Է�
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - BedLimit;
                                        BedLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {
                                    ////���겻�ƣ������޶��ڣ�ʣ�µ����
                                    if (feeItem.FT.TotCost > BedLimit)
                                    {
                                        feeItem.FT.TotCost = BedLimit;
                                        BedLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        else if (feeItem.Item.MinFee.ID == icuBedMinFeeCode && computeLimit)
                        {

                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region �໤�����괦��


                                //���ô�λ�շѺ���������С������010��һ���Ǽ໤��,�������û������.
                                //�໤����ش�λ��ҲӦ��ά����010,����Ҳû������

                                //��������
                                if (patient.FT.BedOverDeal == "1")
                                {
                                    if (IcuLimit >= feeItem.FT.TotCost)
                                    {
                                        //�໤����׼���ڼ໤���ѣ�������								
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {
                                        //���꣬���겿���Է�
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - IcuLimit;
                                        IcuLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {//���겻�ƣ������޶��ڣ�ʣ�µ����
                                    //����
                                    if (feeItem.FT.TotCost > IcuLimit)
                                    {
                                        feeItem.FT.TotCost = IcuLimit;
                                        IcuLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        this.ComputeCost(feeItem, Rate);
                    }
                    #endregion
                    //-----------------------------------------------------------------------
                    if (this.FeeItem(patient, feeItem) == -1)
                    {
                        this.Rollback();
                        this.Err = "����סԺ�շ�ҵ������!" + this.Err;
                        return -1;
                    }
                    alFeeInfo.Add(feeItem);
                }
                if (inpatientManager.UpdateFixFeeDateByPerson(patient.ID, patient.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Rollback();
                    this.Err = "���»����ϴ���ȡ����ʱ��ʱ����!";
                    return -1;
                }


                //������Ϣ
                #region HL7��Ϣ����
                object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
                if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
                {
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                    int param = curIOrderControl.SendFeeInfo(patient, alFeeInfo, true);
                    if (param == -1)
                    {
                        this.Rollback();
                        this.Err = curIOrderControl.Err;
                        return -1;
                    }
                }

                #endregion

                this.Commit();
            }
            catch (Exception e)
            {
                this.Rollback();
                this.Err = "����Ϊ:" + patient.PVisit.Name + "סԺ��ˮ��Ϊ:" +
                    patient.ID + "��ȡ�̶�����ʧ��!" + e.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ���߷��ñ���
        /// </summary>
        /// <param name="PactID">��ͬ��λ����</param>
        /// <param name="item">ҩƷ��ҩƷ��Ϣ</param>
        /// <returns>ʧ��null���ɹ� FS.HISFC.Models.Fee.FtRate</returns>
        private FS.HISFC.Models.Base.FTRate ComputeFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

            PactItemRate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            try
            {
                //��Ŀ
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);
                if (ObjPactItemRate == null)
                {
                    //��С����
                    ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.MinFee.ID);
                    if (ObjPactItemRate == null)
                    {
                        //ȡ��ͬ��λ�ı���
                        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                        PactManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.Err = "��ú�ͬ��λ��Ϣ����" + PactManagment.Err;
                            return null;
                        }
                        try
                        {
                            ObjPactItemRate = new FS.HISFC.Models.Base.PactItemRate();
                            ObjPactItemRate.Rate.PayRate = PactUnitInfo.Rate.PayRate;
                            ObjPactItemRate.Rate.OwnRate = PactUnitInfo.Rate.OwnRate;
                        }
                        catch
                        {
                            this.Err = "��ú�ͬ��λ��Ϣ����" + PactManagment.Err;
                            return null;
                        }
                    }
                }
            }
            catch
            {
                this.Err = "��ú�ͬ��λ��Ϣ����";
                return null;
            }

            return ObjPactItemRate.Rate;
        }

        /// <summary>
        ///  �����ܷ��õĸ�����ɲ��ֵ�ֵ
        /// </summary>
        /// <param name="ItemList"></param>
        /// <param name="rate">������֮��ı���</param>
        /// <returns>-1ʧ�ܣ�0�ɹ�</returns>
        private int ComputeCost(FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList, FS.HISFC.Models.Base.FTRate rate)
        {
            if (ItemList.SplitFeeFlag)
            {
                ItemList.FT.PayCost = 0;
                ItemList.FT.PubCost = 0;
                ItemList.FT.OwnCost = ItemList.FT.TotCost;
            }
            else
            {
                if (ItemList.FT.OwnCost == 0)
                {
                    if (ItemList.FT.DefTotCost > 0 && ItemList.FT.DefTotCost != ItemList.FT.TotCost)
                    {
                        ItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(ItemList.FT.DefTotCost * rate.OwnRate, 2);
                        ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.DefTotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                        ItemList.FT.PubCost = ItemList.FT.DefTotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
                        ItemList.FT.OwnCost = ItemList.FT.TotCost - ItemList.FT.PubCost - ItemList.FT.PayCost;

                    }
                    else
                    {
                        ItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(ItemList.FT.TotCost * rate.OwnRate, 2);
                        ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                        ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
                    }
                }
                else
                {

                    ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                    ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;

                }
            }
            return 0;

        }

        #endregion

        #region ����ҽ�����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="dtEndTime"></param>
        /// <returns></returns>
        public int ComputeSiFreeCost(FS.HISFC.Models.RADT.PatientInfo patient, DateTime dtEndTime)
        {
            FS.HISFC.Models.Base.PactInfo pact = this.pactManager.GetPactUnitInfoByPactCode(patient.Pact.ID);
            if (pact.PactDllName.ToLower() == "gzsi.dll")
            {
                FS.HISFC.Models.Base.FT ft = this.inpatientManager.QueryPatientSumFee(patient.ID, patient.PVisit.InTime.ToString(), dtEndTime.ToString());
                if (ft != null)
                {
                    patient.PVisit.MedicalType.ID = this.inpatientManager.GetSiEmplType(patient.ID);
                    if (patient.PVisit.MedicalType.ID == "-1")
                    {
                        ft.RealCost = ft.OwnCost;
                        ft.PayCost = 0;
                        if (-1 == this.inpatientManager.UpdateInMainInfo(patient.ID, ft))
                        {
                            this.Err = inpatientManager.Err;
                            return -1;
                        }
                    }
                    //
                    if (-1 == inpatientManager.ComputePatientOwnFee(patient.ID, ref ft))
                    {
                        return -1;
                    }
                    if (-1 == this.ComputePatientSumFee(patient, ref ft))
                    {
                        return -1;
                    }


                }
                //����ҽ��סԺ����
                if (-1 == this.inpatientManager.UpdateInMainInfo(patient.ID, ft))
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ���㻼�߷��û�����Ϣ���������ߺͱ���������
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="ft">����fin_ipb_feeinfo ����ȡ������Ϣ</param>
        /// <returns></returns>
        public int ComputePatientSumFee(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FS.HISFC.Models.Base.FT ft)
        {
            //��ʱ�ù���ҽ����2�� ����ѯ�����
            ArrayList alInsurancedeal = this.inpatientManager.QueryInsurancedeal("2", patientInfo.PVisit.MedicalType.ID);
            if (alInsurancedeal != null && alInsurancedeal.Count > 0)
            {
                foreach (FS.HISFC.Models.SIInterface.Insurance insurance in alInsurancedeal)
                {
                    //������������
                    if (ft.PubCost > insurance.BeginCost && ft.PubCost <= insurance.EndCost)
                    {
                        //���������
                        decimal dtKJZtot = ft.PubCost - insurance.BeginCost;
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(dtKJZtot * insurance.Rate, 2);//�����Ը�����= ���ܷ���-�Է�-�����Ը�-����ߣ�* �Ը��� 
                        ft.PubCost = ft.TotCost - ft.OwnCost - ft.PayCost - ft.SupplyCost;//���˽��
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.DefTotCost = insurance.BeginCost;//�����
                        ft.RealCost = ft.SupplyCost + ft.OwnCost + ft.PayCost + insurance.BeginCost;//ʵ����� = �����Ը�����+���Է�+�����Ը�+�����
                        break;
                    }
                }
                return 1;
            }
            else
            {
                return -1;
            }

        }

        #endregion

        #region �ײ�����

        /// <summary>
        /// �ײ����Ѻ��� 
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CostPackageDetail(ArrayList details, string InvoiceNO, ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "�ײ���ϸΪ�ջ��߷�Ʊ��Ϊ�գ�";
                return -1;
            }

            try
            {
                int i = 1;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                {
                    
                    FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost = new FS.HISFC.Models.MedicalPackage.Fee.PackageCost();
                    cost.InvoiceNO = InvoiceNO;
                    cost.SequenceNO = i.ToString();
                    cost.Trans_Type = "1";
                    cost.PackageClinic = detail.ID;
                    cost.DetailSeq = detail.SequenceNO;
                    cost.Amount = detail.Item.Qty;
                    cost.Unit = detail.Unit;
                    cost.Tot_Cost = detail.Detail_Cost;
                    cost.Real_Cost = detail.Real_Cost;
                    cost.Gift_cost = detail.Gift_cost;
                    cost.Etc_cost = detail.Etc_cost;
                    cost.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    cost.Cancel_Flag = "0";

                    if (this.PackageCostMgr.Insert(cost) <= 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //����ԭ�м�¼�Ŀ���������ȷ������
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }

                    if (oldDetail.RtnQTY < detail.Item.Qty)
                    {
                        ErrInfo = detail.Item.Name + "��������Ŀ�������������㣡";
                        return -1;
                    }

                    oldDetail.RtnQTY -= detail.Item.Qty;
                    oldDetail.ConfirmQTY += detail.Item.Qty;

                    if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// �ײ����Ѻ��� 
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCostPackageDetail(ArrayList details, string InvoiceNO,Register r ,ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "�ײ���ϸΪ�ջ��߷�Ʊ��Ϊ�գ�";
                return -1;
            }

            try
            {
                int i = 1;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost = new FS.HISFC.Models.MedicalPackage.Fee.PackageCost();
                    cost.Use_Card_NO = r.PID.CardNO;
                    if (r.PID.CardNO != detail.CardNO)
                    {
                        cost.Has_Card_NO = detail.CardNO;
                        
                    }
                    else
                    {
                        cost.Has_Card_NO = r.PID.CardNO;
                    }
                    cost.InvoiceNO = InvoiceNO;
                    cost.SequenceNO = i.ToString();
                    cost.Trans_Type = "1";
                    cost.PackageClinic = detail.ID;
                    cost.DetailSeq = detail.SequenceNO;
                    cost.Amount = detail.Item.Qty;
                    cost.Unit = detail.Unit;
                    cost.Tot_Cost = detail.Detail_Cost;
                    cost.Real_Cost = detail.Real_Cost;
                    cost.Gift_cost = detail.Gift_cost;
                    cost.Etc_cost = detail.Etc_cost;
                    cost.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    cost.Cancel_Flag = "0";

                    if (this.PackageCostMgr.Insert(cost) <= 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //����ԭ�м�¼�Ŀ���������ȷ������
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }

                    if (oldDetail.RtnQTY < detail.Item.Qty)
                    {
                        ErrInfo = detail.Item.Name + "��������Ŀ�������������㣡";
                        return -1;
                    }

                    oldDetail.RtnQTY -= detail.Item.Qty;
                    oldDetail.ConfirmQTY += detail.Item.Qty;

                    if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �ײ����Ѻ��� 
        /// {6974FE57-7E0F-4c8f-AFC8-675CA7536C61}{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCostPackageDetailByType(ArrayList details, string InvoiceNO, FS.HISFC.Models.RADT.PatientInfo  r, string costtype, string costclinic, ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "�ײ���ϸΪ�ջ��߷�Ʊ��Ϊ�գ�";
                return -1;
            }
            try
            {
                int i = 1;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost = new FS.HISFC.Models.MedicalPackage.Fee.PackageCost();
                    cost.Use_Card_NO = r.PID.CardNO;
                    if (r.PID.CardNO != detail.CardNO)
                    {
                        cost.Has_Card_NO = detail.CardNO;

                    }
                    else
                    {
                        cost.Has_Card_NO = r.PID.CardNO;
                    }
                    //{6974FE57-7E0F-4c8f-AFC8-675CA7536C61}
                    cost.CARDNO = r.PID.CardNO;
                    cost.COSTID = PackageCostMgr.GetNewCostid();
                    cost.COSTCLINIC = costclinic;
                    cost.COST_TYPE = costtype;
                    cost.ITEM_CODE = detail.Item.ID;
                    cost.HOSPITAL_ID = detail.HospitalID;
                    cost.HOSPITAL_NAME = detail.HospitalName;

                    cost.InvoiceNO = InvoiceNO;
                    cost.SequenceNO = i.ToString();
                    cost.Trans_Type = "1";
                    cost.PackageClinic = detail.ID;
                    cost.DetailSeq = detail.SequenceNO;
                    cost.Amount = detail.Item.Qty;
                    cost.Unit = detail.Unit;
                    cost.Tot_Cost = detail.Detail_Cost;
                    cost.Real_Cost = detail.Real_Cost;
                    cost.Gift_cost = detail.Gift_cost;
                    cost.Etc_cost = detail.Etc_cost;
                    cost.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    cost.Cancel_Flag = "0";
                    
                    if (this.PackageCostMgr.Insert(cost) <= 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    if (costtype == "ZY")
                    {
                        //����ԭ�м�¼�Ŀ���������ȷ������
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);
                        if (oldDetail == null)
                        {
                            ErrInfo = this.PackageDetailMgr.Err;
                            return -1;
                        }
                        oldDetail.RtnQTY = 0;
                        oldDetail.ConfirmQTY= detail.Item.Qty;
                        if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                        {
                            ErrInfo = this.PackageDetailMgr.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        //����ԭ�м�¼�Ŀ���������ȷ������
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                        if (oldDetail == null)
                        {
                            ErrInfo = this.PackageDetailMgr.Err;
                            return -1;
                        }

                        if (oldDetail.RtnQTY < detail.Item.Qty)
                        {
                            ErrInfo = detail.Item.Name + "��������Ŀ�������������㣡";
                            return -1;
                        }

                        oldDetail.RtnQTY -= detail.Item.Qty;
                        oldDetail.ConfirmQTY += detail.Item.Qty;

                        if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                        {
                            ErrInfo = this.PackageDetailMgr.Err;
                            return -1;
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �ײ�����ȡ����������{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CancelCostPackageDetail(string InvoiceNO, ref string ErrInfo)
        {
            try
            {
                ArrayList tmp = this.PackageCostMgr.QueryByInvoiceNOByType(InvoiceNO, "0", "MZ");

                if (tmp == null)
                {
                    ErrInfo = this.PackageCostMgr.Err;
                    return -1;
                }
                List<string> clincodelist = new List<string>();
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost in tmp)
                {
                    decimal qty = cost.Amount;
                    //����ԭ�м�¼
                    cost.Cancel_Flag = "1";
                    cost.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.CancelTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();

                    if (this.PackageCostMgr.UpdateByCostType(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //���븺��¼
                    cost.Trans_Type = "2";
                    cost.Amount = -cost.Amount;
                    cost.Tot_Cost = -cost.Tot_Cost;
                    cost.Real_Cost = -cost.Real_Cost;
                    cost.Gift_cost = -cost.Gift_cost;
                    cost.Etc_cost = -cost.Etc_cost;

                    cost.Balance_flag = "";
                    cost.Balance_no = "";
                    //����ʱ�� {99F3E6D7-1287-46db-8301-BDF640DC7A74}
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    if (this.PackageCostMgr.Insert(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //����ԭ�м�¼�Ŀ���������ȷ������
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(cost.PackageClinic, "1", cost.DetailSeq);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }
                    if (oldDetail.Cancel_Flag != "0"&&!string.IsNullOrEmpty(cost.NewPackageClinic)&&!string.IsNullOrEmpty(cost.NewDetailSeq)) //�ײ����˷�
                    {
                        oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(cost.NewPackageClinic, "1", cost.NewDetailSeq);
                    }
                    if (oldDetail.ConfirmQTY < qty)
                    {
                        ErrInfo = "�����ײͼ�¼����";
                        return -1;
                    }

                    oldDetail.RtnQTY += qty;
                    oldDetail.ConfirmQTY -= qty;

                    if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }
                    if (!clincodelist.Contains(oldDetail.ID))
                    {
                        clincodelist.Add(oldDetail.ID);
                    }
                    
                }
                ///�����ײ������״̬Ϊδʹ��
                ///
                this.PackageMgr.UpdatePackageCostFlag2(string.Join(",", clincodelist.ToArray()),"0");
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }



        /// <summary>
        /// �ײ�����ȡ������(סԺ���ײ�){351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// 
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCancelCostAndPackageDetail(string CostInvoiceNO, string costype, ref  string ErrInfo)
        {
            if (costype == "ZY")
            {
                // ����ײ�����δ���˷ѵļ�¼{351D714B-0153-483e-B1AB-697C5A9A9BAD}
                HISFC.BizLogic.MedicalPackage.Fee.Package feepackmanager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
                ArrayList alcostPack = feepackmanager.QueryByCostInvoiceNoNoRtn(CostInvoiceNO);
                foreach (var item in alcostPack)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package pack = item as FS.HISFC.Models.MedicalPackage.Fee.Package;//�ײ�����
                    if (pack != null)
                    {
                        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail detailManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
                        //�����ײ��������ײ���ϸ��¼
                        ArrayList packdetaillist = detailManager.QueryDetailByClinicNO(pack.ID, "0");
                        if (packdetaillist.Count != 0)
                        {
                            //�ع���ϸ��¼��ȷ�������Ϳ�������
                            if (CancelPackageDetailNum(packdetaillist, ref ErrInfo) <= 0)
                            {
                                return -1;
                            }
                        }
                    }
                }
                //�����ײ����Ѽ�¼����¼
                if (CancelPackageCost(CostInvoiceNO, "ZY", ref ErrInfo) <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// �ײ���ϸ�����ع���סԺ�ײͣ�{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CancelPackageDetailNum(ArrayList details,ref string ErrInfo)
        {
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                //����ԭ�м�¼�Ŀ���������ȷ������
                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);
                decimal qty = oldDetail.Item.Qty;
                if (oldDetail == null)
                {
                    ErrInfo = this.PackageDetailMgr.Err;
                    return -1;
                }

                if (oldDetail == null)
                {
                    ErrInfo = this.PackageDetailMgr.Err;
                    return -1;
                }

                if (oldDetail.ConfirmQTY < qty)
                {
                    ErrInfo = "�����ײͼ�¼����";
                    return -1;
                }

                oldDetail.RtnQTY += qty;
                oldDetail.ConfirmQTY -= qty;

                if (this.PackageDetailMgr.Update(oldDetail) <= 0)
                {
                    ErrInfo = this.PackageDetailMgr.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// �ײ����ѱ��¼ȡ��(����¼){351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CancelPackageCost(string CostInvoiceNO,string costtype, ref string ErrInfo)
        {
            try
            {
                ArrayList tmp = this.PackageCostMgr.QueryByInvoiceNOByType(CostInvoiceNO, "0", costtype);//�ײ����Ѽ�¼
                if (tmp == null)
                {
                    ErrInfo = this.PackageCostMgr.Err;
                    return -1;
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost in tmp)
                {
                    decimal qty = cost.Amount;
                    //����ԭ�м�¼
                    cost.Cancel_Flag = "1";
                    cost.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.CancelTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();

                    if (this.PackageCostMgr.UpdateByCostType(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //���븺��¼
                    cost.Trans_Type = "2";
                    cost.Amount = -cost.Amount;
                    cost.Tot_Cost = -cost.Tot_Cost;
                    cost.Real_Cost = -cost.Real_Cost;
                    cost.Gift_cost = -cost.Gift_cost;
                    cost.Etc_cost = -cost.Etc_cost;

                    cost.Balance_flag = "";
                    cost.Balance_no = "";
                    //����ʱ�� {99F3E6D7-1287-46db-8301-BDF640DC7A74}
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    if (this.PackageCostMgr.Insert(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }
            return 1;
        }



        #endregion

        #region ��ѯ��������
        public List<string> queryPackageContainUnDrug(string undrugId)
        {
            //{CE949D37-D860-4b2a-88B7-FFFC11918999}
            FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager um = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();
            List<string> packages = new List<string>();


            System.Data.DataTable dt = um.queryPackageContainUnDrug(undrugId);

            if (dt == null)
            {
                return null;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string msg = "�ײͱ��룺" + dt.Rows[i]["PACKAGE_ID"].ToString() + "�ײ����ƣ�" + dt.Rows[i]["PACKAGE_NAME"].ToString();
                    packages.Add(msg);
                }
            }

            return packages;

        }
        
        #endregion

    }

    /// <summary>
    /// ҽ��ִ�е�����
    /// </summary>
    public class ExecOrderCompare : IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;

            if (execOrder1.DateUse > execOrder2.DateUse)
            {
                return -1;
            }
            else if (execOrder1.DateUse == execOrder2.DateUse)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}
