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
using FS.HISFC.Models.Order;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 整合的入出转管理类]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Fee : IntegrateBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {

        #region liuq 2007-8-23 追加
        #region 门诊内部分发票（废弃）

        /// <summary>
        /// 门诊按照执行科室,最小费用等分发票
        /// </summary>
        /// <param name="payKindCode">患者的费用类别</param>
        /// <param name="feeItemLists">患者的总体费用明细</param>
        /// <returns>成功 分好的费用明细,每个ArrayList代表一组应该生成发票的费用明细 失败 null</returns>
        public ArrayList SplitInvoice(string payKindCode, ref ArrayList feeItemLists)
        {

            // 获得是否按照执行科室分发票,默认不刷新参数,默认值为 false即不按照执行科室分发票.
            bool isSplitByExeDept = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.IS_SPLIT_INVOICE_BY_EXEDEPT, false, false);

            //分组后发票
            ArrayList exeGroupList = new ArrayList();

            if (isSplitByExeDept)
            {
                //按照执行科室分组
                exeGroupList = CollectFeeItemListsByExeDeptCode(feeItemLists);
            }
            else
            {
                exeGroupList = feeItemLists;
            }

            //获得是否按照最小分发票,默认不刷新参数,默认值为 false即不按照最小分发票.
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
        /// 获得对应支付方式的按照最小费用条目分发票的明细条目
        /// </summary>
        /// <param name="payKindCode">患者的支付方式类别</param>
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
        /// 按照最小费用分明细
        /// </summary>
        /// <param name="payKindCode">患者的支付方式类别</param>
        /// <param name="feeItemList">费用明细</param>
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
        /// 按照最小费用限制数量分明细
        /// </summary>
        /// <param name="payKindCode">患者的支付方式类别</param>
        /// <param name="feeItemLists">费用明细</param>
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
        /// 按照最小费用排序
        /// </summary>
        /// <param name="feeItemLists">费用明细</param>
        /// <returns>成功 排序好的处方明细 失败 null</returns>
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
        /// 按照执行科室排序
        /// </summary>
        /// <param name="feeItemLists">费用明细</param>
        /// <returns>成功 排序好的处方明细 失败 null</returns>
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
        /// 按照时间排序
        /// </summary>
        /// <param name="feeItemLists">费用明细</param>
        /// <returns>成功 排序好的处方明细 失败 null</returns>
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
        /// 排序类
        /// </summary>
        private class SortFeeItemListByExeDeptCode : IComparer
        {
            #region IComparer 成员

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
        /// 排序类
        /// </summary>
        private class SortFeeItemListByFeeCode : IComparer
        {
            #region IComparer 成员

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
        /// 排序类
        /// </summary>
        private class SortFeeItemListByChargeDate : IComparer
        {
            #region IComparer 成员

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

        #region 更据接口实现分发票
        /// <summary>
        /// 更据接口实现分发票
        /// </summary>
        /// <param name="register"></param>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SplitInvoice(FS.HISFC.Models.Registration.Register register, ref ArrayList feeItemLists)
        {
            //为了使速度更快，默认分发票接口里面不在走下面的代码，直接返回
            ArrayList finalSplitList = new ArrayList();

            finalSplitList.Add(feeItemLists);

            return finalSplitList;
        }

        #endregion

        #region 变量

        /// <summary>
        /// 费用类业务层 {2CEA3B1D-2E59-44ac-9226-7724413173C5} 对业务层的引用全部改为非静态的变量
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 消费的账户信息
        /// </summary>
        protected FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();

        /// <summary>
        /// item
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// /// 发票业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// 财务组业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup employeeFinanceGroupManager = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();

        /// <summary>
        /// 控制类业务层
        /// </summary>
        protected FS.FrameWork.Management.ControlParam controlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 门诊业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 门诊医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderOutpatientManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 终端预约业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Terminal.Terminal terminalManager = new FS.HISFC.BizLogic.Terminal.Terminal();

        protected FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctBizLogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();

        /// <summary>
        /// 药品业务层
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
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Manager();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 本地医保业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Interface interfaceManager = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 当前医保公费接口
        /// </summary>
        protected MedcareInterfaceProxy medcareInterfaceProxy = new MedcareInterfaceProxy();

        /// <summary>
        /// 合同单位类
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// 患者实体(liu.xq)
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
        /// 是否忽略医保公费接口
        /// </summary>
        private bool isIgnoreMedcareInterface = false;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 体检业务层
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
        /// 员工
        /// </summary>
        FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

        //复合项目明细业务层
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 系统序列号管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Sequence seqManager = new FS.HISFC.BizLogic.Manager.Sequence();

        /// <summary>
        /// 调号业务层{BF01254E-3C73-43d4-A644-4B258438294E}
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceJumpRecord invoiceJumpRecordMgr = new FS.HISFC.BizLogic.Fee.InvoiceJumpRecord();

        /// <summary>
        /// 门诊收费是否需要更新发票号
        /// </summary>
        protected bool isNeedUpdateInvoiceNO = true;

        /// <summary>
        /// 是否忽略在院状态更新住院主表
        /// </summary>
        protected bool isIgnoreInstate = false;
        /// <summary>
        /// 欠费提示类型
        /// </summary>
        private MessType messType = MessType.Y;
        /// <summary>
        /// //是否启用分诊系统 1 启用 其他 不启用
        /// </summary>
        string pValue = "";

        #region 门诊帐户
        /// <summary>
        /// 门诊帐户类业务层
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 账户密码输入
        /// </summary>
        protected static FS.HISFC.BizProcess.Interface.Account.IPassWord ipassWord = null;
        #endregion
        /// <summary>
        /// 物资收费
        /// </summary>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        protected static FS.HISFC.BizProcess.Integrate.Material.Material materialManager = new FS.HISFC.BizProcess.Integrate.Material.Material();

        /// <summary>
        /// 授权收费
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.EmpowerFee empowerFeeManager = new FS.HISFC.BizLogic.Fee.EmpowerFee();

        /// <summary>
        /// 床位费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.BedFeeItem feeBedFeeItem = new FS.HISFC.BizLogic.Fee.BedFeeItem();

        /// <summary>
        /// 退费业务层 {5C327B2F-2B74-45bb-8435-4E5118215BD2}
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
        /// 获取价格接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice IGetItemPrice = null;

        /// <summary>
        /// 获取四舍五舍接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        //{282BD4C3-4086-4d4c-BE3D-68FC3205E4B7}
        /// <summary>
        /// 挂号明细管理
        /// </summary>
        FS.HISFC.BizLogic.Registration.RegDetail regDetailMgr = new FS.HISFC.BizLogic.Registration.RegDetail();
        /// <summary>
        /// 挂号支付方式管理
        /// </summary>
        FS.HISFC.BizLogic.Registration.RegPayMode regPayModeMgr = new FS.HISFC.BizLogic.Registration.RegPayMode();

        /// <summary>
        /// 门诊账户
        /// </summary>
        Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

        /// <summary>
        /// 套餐业务明细数据
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
        /// 套餐业务明细数据
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
        /// 消费记录管理类
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
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helperPayModes = null;

        #endregion

        /// <summary>
        /// 设置数据库事务
        /// </summary>
        /// <param name="trans">数据库事务</param>
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

            #region 门诊帐户

            accountManager.SetTrans(trans);

            accountPay.SetTrans(trans);

            #endregion

            #region 套餐

            PackageDetailMgr.SetTrans(trans);
            PackageCostMgr.SetTrans(trans);

            #endregion
        }

        #region 属性

        /// <summary>
        /// 是否忽略在院状态更新住院主表
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
        /// 门诊收费是否需要更新发票号
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
        /// 当前医保公费接口
        /// </summary>
        public MedcareInterfaceProxy MedcareInterfaceProxy
        {
            get
            {
                return medcareInterfaceProxy;
            }
        }

        /// <summary>
        /// 是否忽略医保公费接口
        /// </summary>
        public bool IsIgnoreMedcareInterface
        {
            set
            {
                this.isIgnoreMedcareInterface = value;
            }
        }
        /// <summary>
        /// 是否判断欠费，欠费是否提示
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
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
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
        /// 支付方式列表
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper PayModesHelper
        {
            get
            {
                if (this.helperPayModes == null)
                {
                    this.helperPayModes = new FS.FrameWork.Public.ObjectHelper();
                    //初始化支付方式信息
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

        #region 私有方法

        /// <summary>
        /// 判断收费需要的参数是否合法
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">患者费用明细实体</param>
        /// <returns>成功: true 失败 false</returns>
        private bool IsValidFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (patient == null)
            {
                this.Err = Language.Msg("患者基本信息没有赋值");

                return false;
            }

            if (feeItemList == null)
            {
                this.Err = Language.Msg("患者费用信息没有赋值");

                return false;
            }

            //if (feeItemList.FT.TotCost == 0)
            //{
            //    this.Err = Language.Msg("费用总额不能为零：\n单价或数量不能为0");

            //    return false;
            //}

            if (patient.PVisit.PatientLocation.NurseCell.ID == null || patient.PVisit.PatientLocation.NurseCell.ID.Trim() == string.Empty)
            {
                this.Err = Language.Msg("表现层患者护士站编码没有赋值!");

                return false;
            }

            if (feeItemList.ExecOper.Dept.ID == null || feeItemList.ExecOper.Dept.ID == string.Empty)
            {
                this.Err = Language.Msg("表现层执行科室没有赋值!");

                return false;
            }

            if (feeItemList.FTRate.ItemRate < 0)
            {
                this.Err = Language.Msg("收费比例赋值错误!");

                return false;
            }
            feeItemList.Item.Price = Math.Round(feeItemList.Item.Price, 4);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.Item.Price, 10, 4))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("的价格精度不符合,标准的精度应该为小数点前6位,小数点后4位");

                return false;
            }
            feeItemList.Item.Qty = Math.Round(feeItemList.Item.Qty, 4);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.Item.Qty, 9, 4))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("的数量精度不符合,标准的精度应该为小数点前5位,小数点后4位");

                return false;
            }
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
            if (!FS.FrameWork.Public.String.IsPrecisionValid(feeItemList.FT.TotCost, 10, 2))
            {
                this.Err = feeItemList.Item.Name + Language.Msg("的金额精度不符合,标准的精度应该为小数点前8位,小数点后2位");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareFeeItemList : IComparer
        {
            #region IComparer 成员

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
        /// 设置处方号
        /// </summary>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <param name="trans">数据库事务</param>
        /// <returns>成功 1 失败 -1</returns>
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
        /// 按照最小费用排序
        /// </summary>
        /// <param name="feeItemLists">费用明细</param>
        /// <returns>成功 排序好的处方明细 失败 null</returns>
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
        /// 把医嘱信息转换为费用信息
        /// 
        /// {F5477FAB-9832-4234-AC7F-ED49654948B4} 增加参数 传入patient信息
        /// </summary>
        /// <param name="orderList">医嘱信息</param>
        /// <returns>成功 费用信息 失败 null</returns>
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
                    MessageBox.Show(Language.Msg("取项目:") + feeItemList.Item.Name + Language.Msg("的价格出错!") + this.Err);

                    return null;
                }
                feeItemList.Item.Price = price;

                // {54B0C254-3897-4241-B3BD-17B19E204C8C}
                // 原始价格（本来应收价格，不考虑合同单位因素）
                feeItemList.Item.DefPrice = orgPrice;

                //录入界面已经将QTY 赋值
                feeItemList.Item.Qty = order.Qty;// *order.HerbalQty;
                //增加付数的赋值 {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                feeItemList.Days = order.HerbalQty;

                feeItemList.Item.PriceUnit = order.Unit;//单位重新付
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
                //// 原始总费用（本来应收费用，不考虑合同单位因素）
                //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
                //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //新计价规则，先计算出单片价格进行4舍5入，再求总价
                //住院结算会针对收费数量进行拆分，若不如此处理，则拆分出来的费用条目金额会有出入
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

                #region {10C9E65E-7122-4a89-A0BE-0DF62B65C647} 写入复合项目编码、名称
                feeItemList.UndrugComb.ID = order.Package.ID;
                feeItemList.UndrugComb.Name = order.Package.Name;
                feeItemList.UndrugComb.Qty = order.Package.Qty;

                #endregion

                feeItemLists.Add(feeItemList);
            }

            return feeItemLists;
        }

        /// <summary>
        /// 设置物资扣库科室
        /// </summary>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public void GetMatLoadDataDept(FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            //return ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            f.StockOper.Dept.ID = f.ExecOper.Dept.ID;
        }

        /// <summary>
        /// 婴儿的费用是否可以收取到妈妈身上
        /// </summary>
        private string motherPayAllFee = "";

        /// <summary>
        /// 住院批量收费
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <param name="feeItemLists">费用或医嘱信息实体</param>
        /// <param name="payType">收费类型(划价或者收费)</param>
        /// <param name="transType">正交易 反交易</param>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        private int FeeManager(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists, ChargeTypes payType, TransTypes transType)
        {
            #region liu.xq
            this.RadtIntegrate.SetTrans(this.trans);
            patient = this.RadtIntegrate.GetPatientInfomation(patient.ID);

            if (patient.IsStopAcount)
            {
                this.Err = "该患者已经封帐!不能继续录入费用或退费,请与住院处联系!";

                return -1;
            }

            //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} 婴儿的费用是否可以收取到妈妈身上
            if (IsMotherPayAllFee(patient))
            {
                //if (string.IsNullOrEmpty(motherPayAllFee))
                //{
                //    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                //}

                //if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
                //{
                //    if (patient.IsBaby) //住院流水号好有B,代表是婴儿
                //    {
                string motherInpatientNO = this.RadtIntegrate.QueryBabyMotherInpatientNO(patient.ID);
                if (string.IsNullOrEmpty(motherInpatientNO) || motherInpatientNO == "-1")
                {
                    this.Err = "没有找到婴儿的母亲住院流水号" + this.RadtIntegrate.Err;

                    return -1;
                }

                patient = this.RadtIntegrate.GetPatientInfomation(motherInpatientNO);//用妈妈的基本信息替换婴儿的基本信息

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
            ////{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F}完毕

            #endregion


            this.SetDB(inpatientManager);

            if (feeItemLists == null || feeItemLists.Count == 0)
            {
                return -1;
            }

            //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E}增加医疗组处理
            if (HsMedicalTeam == null)
            {
                HsMedicalTeam = new Hashtable();
                List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> medicalTeamForDoctList = new List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct>();

                medicalTeamForDoctList = this.medicalTeamForDoctBizLogic.QueryQueryMedicalTeamForDoctInfo();

                //添加哈希表
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

            //取集合的第一个元素判断是费用明细(FeeItemList还是Order)
            long returnValue = 0;
            this.MedcareInterfaceProxy.SetPactTrans(this.trans);
            //如果费用接口没有初始化,那么根据患者的合同单位初始化费用接口
            //if (medcareInterfaceProxy != null)
            //{
            //    returnValue = MedcareInterfaceProxy.SetPactCode(patient.Pact.ID);

            //    if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
            //    {
            //        this.Err = "合同单位初始化费用接口失败：" + MedcareInterfaceProxy.ErrMsg;

            //        return -1;
            //    }
            //}

            //判断有效性
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                //有效性判断
                if (!this.IsValidFee(patient, feeItemList))
                {
                    return -1;
                }
                // //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E}增加医疗组处理
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

                // 没有维护医疗组
                // 保存患者住院医生ID 南庄需求

                if (string.IsNullOrEmpty(feeItemList.MedicalTeam.ID))
                {
                    feeItemList.MedicalTeam.ID = patient.PVisit.AdmittingDoctor.ID;
                }
            }

            //执行费用接口,对比例等进行计算后重新赋值
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

                //if (feeItemList.User01 != "1" && payType == ChargeTypes.Fee) //用以患者费用比例修改,重新调用的时候不需要在计算费用比例
                //{
                //    returnValue = MedcareInterfaceProxy.RecomputeFeeItemListInpatient(patient, feeItemList);

                //    if (returnValue == -1 && this.isIgnoreMedcareInterface == false)
                //    {
                //        this.Err = MedcareInterfaceProxy.ErrMsg;

                //        return -1;
                //    }
                //}


                //为防止最后余额不符，统一转换为2位。

                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                feeItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.OwnCost, 2);
                feeItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.PayCost, 2);
                feeItemList.FT.PubCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.PubCost, 2);
                feeItemList.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.RebateCost, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);


                //防止空调床位拆分后记录为0
                //if (feeItemList.FT.TotCost == 0)
                //{
                //    return 1;
                //}
            }

            //重新分配处方号
            this.SetRecipeNO(ref feeItemLists, this.trans);

            #region 物资收费处理
            //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
            //if (transType == TransTypes.Positive)
            //{
            //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
            //    {
            //        //物资的扣库科室是登录科室（与加载的列表是相同的科室）
            //        if (f.Item.ItemType != EnumItemType.Drug)
            //        {
            //            GetMatLoadDataDept(f);
            //        }
            //    }
            //    //物资收费处理
            //    if (materialManager.MaterialFeeOutput(feeItemLists) < 0)
            //    {
            //        this.Err = materialManager.Err;
            //        return -1;
            //    }
            //}
            //else
            //{

            //    //退库
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
            //对明细循环处理
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {

                //如果是收费操作,那么进行正交易和反交易的特殊赋值
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

                    //如果是反交易,需要判断更新可退数量和把金额,数量进行负数处理
                    if (transType == TransTypes.Negative)
                    {

                        #region 查找原记录
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
                        if (feeItemListTemp == null)
                        {
                            this.Err = "获得项目基本信息出错!" + this.inpatientManager.Err;
                            return -1;
                        }

                        #endregion

                        #region 更新可退数量
                        //更新可退数量 然后取得新的处方号,药品和非药品分别更新(表不同)
                        if (feeItemList.Item.ItemType == EnumItemType.Drug)
                        {
                            if (feeItemListTemp.NoBackQty > 0)
                            {
                                if (inpatientManager.UpdateNoBackQtyForDrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, feeItemListTemp.NoBackQty, feeItemListTemp.BalanceState) < 1)
                                {
                                    this.Err = Language.Msg("更新原有记录可退数量出错!") + feeItemList.Item.Name + Language.Msg("费用已经被退费或结算!") + inpatientManager.Err;

                                    return -1;
                                }
                            }
                            //获得新的处方号

                            feeItemList.RecipeNO = inpatientManager.GetDrugRecipeNO();
                        }
                        else
                        {
                            if (feeItemListTemp.NoBackQty > 0)
                            {
                                if (inpatientManager.UpdateNoBackQtyForUndrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, feeItemListTemp.NoBackQty, feeItemListTemp.BalanceState) < 1)
                                {
                                    this.Err = Language.Msg("更新原有记录可退数量出错!") + feeItemList.Item.Name + Language.Msg("费用已经被退费或结算!") + inpatientManager.Err;

                                    return -1;
                                }
                            }
                            //获得新的处方号
                            feeItemList.RecipeNO = inpatientManager.GetUndrugRecipeNO();
                        }
                        #endregion

                        #region 全退/半退处理

                        feeItemList.CancelRecipeNO = feeItemListTemp.RecipeNO;
                        feeItemList.CancelSequenceNO = feeItemList.SequenceNO;

                        //原始价格和金额
                        feeItemList.Item.DefPrice = feeItemListTemp.Item.DefPrice;
                        //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);

                        //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                        //新计价规则，先计算出单片价格进行4舍5入，再求总价
                        //住院结算会针对收费数量进行拆分，若不如此处理，则拆分出来的费用条目金额会有出入
                        feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                        feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);

                        feeItemList.SplitFeeFlag = feeItemListTemp.SplitFeeFlag;

                        //全退
                        if (feeItemList.Item.Qty == feeItemListTemp.Item.Qty)//说明是全退
                        {
                            //修改医技退费更新取消确认数量bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
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
                            //保持划价时间跟收费时间同步
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

                            feeItemList.Item.Qty = feeItemListTemp.Item.Qty - qty;//数量
                            //修改医技退费更新取消确认数量bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                            feeItemList.ConfirmedQty = feeItemList.ConfirmedQty - qty;

                            if (feeItemListTemp.UndrugComb.Qty != 0)
                            {
                                feeItemList.UndrugComb.Qty = FS.FrameWork.Public.String.FormatNumber(feeItemListTemp.UndrugComb.Qty * (feeItemList.Item.Qty / feeItemListTemp.Item.Qty), 2);
                            }
                            feeItemList.NoBackQty = feeItemList.Item.Qty; //
                            //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                            //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);

                            //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                            //新计价规则，先计算出单片价格进行4舍5入，再求总价
                            //住院结算会针对收费数量进行拆分，若不如此处理，则拆分出来的费用条目金额会有出入
                            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;

                            //重新计算费用
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
                            //保持划价时间跟收费时间同步
                            if (feeItemList.ChargeOper.OperTime <= DateTime.MinValue)
                            {
                                feeItemList.ChargeOper.OperTime = feeItemListTemp.FeeOper.OperTime;
                            }
                            feeItemList.TransType = TransTypes.Positive;

                            #region 冲负
                            //负记录的处方号和正记录的处方号一致
                            feeItemListTemp.RecipeNO = feeItemList.RecipeNO;
                            feeItemListTemp.SequenceNO = feeItemList.SequenceNO;
                            feeItemListTemp.Item.Qty = -feeItemListTemp.Item.Qty;
                            feeItemListTemp.NoBackQty = 0;//不置零的话，在退费申请时可见
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
                            if (feeItemListTemp.FT.User03 != "NOCHANGEDATE")//修改患者费用比例时是否勾选，不更改收费日期
                            {
                                feeItemListTemp.FeeOper.OperTime = dtNow;
                            }
                            feeItemListTemp.ChargeOper.ID = inpatientManager.Operator.ID;
                            //保持划价时间跟收费时间同步
                            if (feeItemListTemp.ChargeOper.OperTime <= DateTime.MinValue)
                            {
                                feeItemListTemp.ChargeOper.OperTime = feeItemListTemp.FeeOper.OperTime;
                            }

                            feeItemListTemp.BalanceState = "0";
                            feeItemListTemp.BalanceOper.ID = string.Empty;
                            feeItemListTemp.BalanceOper.Name = string.Empty;
                            feeItemListTemp.BalanceOper.OperTime = DateTime.MinValue;

                            feeItemListTemp.NoBackQty = 0;//可退数量为零
                            if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                            {
                                if (inpatientManager.InsertMedItemList(patient, feeItemListTemp) == -1)
                                {
                                    this.Err = Language.Msg("插入药品明细出错!") + inpatientManager.Err;
                                    return -1;
                                }
                            }
                            else
                            {
                                if (inpatientManager.InsertFeeItemList(patient, feeItemListTemp) == -1)
                                {
                                    this.Err = Language.Msg("插入非药品明细出错!") + inpatientManager.Err;
                                    return -1;
                                }
                            }
                            alFeeItemLists.Add(feeItemListTemp);
                            #endregion
                        }

                        #endregion

                    }

                    if (feeItemList.FT.User03 != "NOCHANGEDATE")//修改患者费用比例时是否勾选，不更改收费日期
                    {
                        DateTime feedate = feeItemList.FeeOper.OperTime.Date;
                        DateTime now = dtNow.Date;
                        if(feedate >= now)
                            feeItemList.FeeOper.OperTime = dtNow;
                    }
                    //保持划价时间跟收费时间同步
                    if (feeItemList.ChargeOper.OperTime <= DateTime.MinValue)
                    {
                        feeItemList.ChargeOper.OperTime = feeItemList.FeeOper.OperTime;
                    }
                    if (string.IsNullOrEmpty(feeItemList.Patient.Pact.ID))
                    {
                        feeItemList.Patient.Pact.ID = patient.Pact.ID;
                        feeItemList.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    }
                    //结算序号在对于收费应为0 而直接收费应该为当前患者的结算序号+1
                    //feeItemList.BalanceNO = patient.BalanceNO;
                    feeItemList.FeeOper.ID = inpatientManager.Operator.ID;

                    // 设置收费员科室
                    if (feeItemList.FeeOper.Dept == null || string.IsNullOrEmpty(feeItemList.FeeOper.Dept.ID))
                    {
                        FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
                        if (oper != null)
                        {
                            feeItemList.FeeOper.Dept = oper.Dept;
                        }
                    }
                }

                //给明细记录的收费划价标志赋值
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

                //插入处方明细表,分别为药品,非药品
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    if (inpatientManager.InsertMedItemList(patient, feeItemList) == -1)
                    {
                        this.Err = Language.Msg("插入药品明细出错!") + inpatientManager.Err;

                        return -1;
                    }
                }
                else
                {
                    if (inpatientManager.InsertFeeItemList(patient, feeItemList) == -1)
                    {
                        this.Err = Language.Msg("插入非药品明细出错!") + inpatientManager.Err;

                        return -1;
                    }
                }
            }

            //获取最新的费用数组
            //alFeeItemLists.AddRange(feeItemLists);
            feeItemLists.AddRange(alFeeItemLists);

            ///费用待遇接口处理

            //由于划价和收费流程,以上代码均通用,下面为收费与划价不同的流程,收费需要按照最小费用(MinFee.ID)汇总,插入费用汇总表
            //并更新住院主表
            if (payType == ChargeTypes.Fee)
            {

                //获得按照MinFee.ID分组后的数据集合
                //ArrayList sorList = this.CollectFeeItemLists(alFeeItemLists);
                ArrayList sorList = this.CollectFeeItemLists(feeItemLists);

                int iReturn = 0;
                //创建一个费用
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
                        this.Err = Language.Msg("插入费用汇总信息出错!");

                        return -1;
                    }
                }

                //如果忽略在院状态,比如直接收费患者,那么在更新住院主表的时候不判断在院状态是否为'O'
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
                    this.Err = Language.Msg("更新住院主表失败!") + inpatientManager.Err;

                    return -1;
                }

                //如果返回为0 说明不符合in_state <> 0条件，让前台不可以再录入费用.
                if (iReturn == 0)
                {
                    this.Err = patient.Name + Language.Msg("已经结算或者处于封账状态，不能继续录入费用!请与住院处联系!");

                    return -1;
                }
                FS.FrameWork.Models.NeuObject civilworkerObject = this.managerIntegrate.GetConstansObj("civilworker", patient.Pact.ID);

                if (patient.Pact.PayKind.ID == "03" && (ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) != 0)
                {
                    if (inpatientManager.UpdateBursaryTotMedFee(patient.ID, ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) < 1)
                    {
                        this.Err = "更新公费患者日限额累计失败!" + this.Err;
                        return -1;
                    }
                }
                else if (civilworkerObject != null && !string.IsNullOrEmpty(civilworkerObject.ID))
                {
                    //市公医更新公费患者日限额累计
                    if (inpatientManager.UpdateSGYBursaryTotMedFee(patient.ID, ftBursaryTotMedFee.PayCost + ftBursaryTotMedFee.PubCost) < 1)
                    {
                        this.Err = "更新公费患者日限额累计失败!" + this.Err;
                        return -1;
                    }

                }
            }

            return 1;

        }

        /// <summary>
        /// 收费函数中获取医疗组，静态保存医疗组内容
        /// </summary>
        private static Hashtable HsMedicalTeam = null;

        /// <summary>
        /// 收费函数
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">费用明细实体</param>
        /// <param name="payType">划价 收费标志</param>
        /// <param name="transType">收费正交易 反交易标志</param>
        /// <returns>成功 1 失败 -1</returns>
        private int FeeManager(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList,
            ChargeTypes payType, TransTypes transType)
        {
            ArrayList temp = new ArrayList();

            temp.Add(feeItemList);

            return this.FeeManager(patient, ref temp, payType, transType);

        }

        /// <summary>
        /// 是否将婴儿的费用记在母亲上
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private bool IsMotherPayAllFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                if (patient.IsBaby) //住院流水号好有B,代表是婴儿
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 公有方法

        public string GetUndrugCode()
        {
            this.SetDB(itemManager);
            return itemManager.GetUndrugCode();
        }

        #region 住院
        /// <summary>
        /// 获得有效的,项目类别为手术的项目数组
        /// </summary>
        /// <returns>成功:项目数组 失败返回null</returns>
        public ArrayList QueryOperationItems()
        {
            this.SetDB(itemManager);

            return itemManager.QueryOperationItems();
        }

        /// <summary>
        /// 获取有效得ICD手术项目
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOperationICDItems()
        {
            this.SetDB(itemManager);
            return itemManager.QueryOperationICDItems();
        }

        /// <summary>
        /// 患者汇总费用查询-按住院流水号和发票号查// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
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
        /// 患者汇总费用查询-按住院流水号和发票号查（折后价 = 医院折后价 - 医保价）
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNOAndCI(string PatientID)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalFeeListInfoByInPatientNOAndCI(PatientID);
        }


        /// <summary>
        /// 患者汇总费用查询-按住院流水号和发票号查（折后价 = 医院折后价 - 医保价）
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalAllFeeListInfoByInPatientNO(string inPatientNO)
        {
            this.SetDB(itemManager);
            return itemManager.GetPatientTotalAllFeeListInfoByInPatientNO(inPatientNO);
        }

        /// <summary>
        /// 患者小孩汇总费用查询-按住院流水号和发票号查//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加小孩费用清单汇总
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
        /// 查询患者费用汇总清单信息（按住院流水号、发票号、护士站）// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
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
        /// 患者明细费用查询-按住院流水号和发票号查// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
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
        /// 患者明细费用查询-按住院流水号和发票号查//给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
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
        /// 患者明细费用查询-按住院流水号和发票号查//给要导出的控件绑定数据源 医院编码{940D2882-F02B-488f-A8E3-07689B0D064D}
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
        /// 患者明细费用查询-按住院流水号和发票号查//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加妈妈本身费用清单汇总
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
        /// 患者明细费用查询-按住院流水号和发票号查//给要导出的控件绑定数据源 //{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加妈妈本身费用清单汇总
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
        /// 患者明细费用查询-按住院流水号和发票号查//给要导出的控件绑定数据源//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}添加小孩费用清单汇总
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
        /// 患者一日费用查询-按住院流水号和发票号查// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
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
        /// 患者预结算报表 {34a15202-a3f9-4d3e-9bad-c7e6783b540c}
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
        /// 患者预结算报表
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
        /// 获得非药品信息
        /// </summary>
        /// <param name="undrugCode"></param>
        /// <returns>成功 非药品信息 失败 null</returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetUndrugByCode(string undrugCode)
        {
            this.SetDB(itemManager);

            return itemManager.GetUndrugByCode(undrugCode);
        }
        /// <summary>
        /// 通过处方号，得到费用明细
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <returns>null 错误 ArrayList Fee.OutPatient.FeeItemList实体集合</returns>
        public ArrayList QueryFeeDetailFromRecipeNO(string recipeNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryFeeDetailFromRecipeNO(recipeNO);
        }

        /// <summary>
        /// 获得门诊卡号流水,默认为9位字长,前面补0
        /// </summary>
        /// <returns>成功 门诊卡号 失败 null</returns>
        public string GetAutoCardNO()
        {
            this.SetDB(outpatientManager);

            return outpatientManager.GetAutoCardNO();
        }

        /// <summary>
        /// 根据处方号和处方项目流水号更新院注已确认数量
        /// </summary>
        /// <param name="moOrder">医嘱流水号</param>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSquence">处方内流水号</param>
        /// <param name="qty">院注次数</param>
        /// <returns>成功: >= 1 失败: -1 没有更新到数据返回 0</returns>
        public int UpdateConfirmInject(string moOrder, string recipeNO, string recipeSquence, int qty)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateConfirmInject(moOrder, recipeNO, recipeSquence, qty);
        }

        /// <summary>
        /// 根据警戒线判断患者是否欠费
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="totCost">本次收费金额</param>
        /// <returns>true 欠费 false 不欠费</returns>
        public bool IsPatientLackFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return IsPatientLackFee(patient, 0);
        }

        /// <summary>
        /// 根据警戒线判断患者是否欠费
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="totCost">本次收费金额</param>
        /// <returns>true 欠费 false 不欠费</returns>
        public bool IsPatientLackFee(FS.HISFC.Models.RADT.PatientInfo patient, decimal totCost)
        {
            //未启用警戒线
            if (!patient.PVisit.AlertFlag)
            {
                return false;
            }
            else
            {
                //警戒线设置类型为M 金额判断时，小于此金额，则认为是欠费提醒
                //警戒线设置类型为D 日期判断是，在此日期内，则不判断欠费，否则按M 金额判断

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
        /// 计算住院本次收费的费用
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="feeItemLists">费用信息</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT ComputeInpatientFee(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList feeItemLists)
        {
            if (IsMotherPayAllFee(patient))
            {
                string motherInpatientNO = this.RadtIntegrate.QueryBabyMotherInpatientNO(patient.ID);
                if (string.IsNullOrEmpty(motherInpatientNO) || motherInpatientNO == "-1")
                {
                    this.Err = "没有找到婴儿的母亲住院流水号" + this.RadtIntegrate.Err;
                    return null;
                }
                patient = this.RadtIntegrate.GetPatientInfomation(motherInpatientNO);//用妈妈的基本信息替换婴儿的基本信息
            }

            //拆分复合项目
            ArrayList alFeeInfo = this.ConvertFeeItemToSingle(patient, feeItemLists);

            //取集合的第一个元素判断是费用明细
            long returnValue = 0;
            this.MedcareInterfaceProxy.SetPactTrans(this.trans);
            //如果费用接口没有初始化,那么根据患者的合同单位初始化费用接口
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
        /// 判断是否开锁状态
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>true 开锁，flase 已锁</returns>
        [Obsolete("不推荐使用了，用境界线里面的D来替换", true)]
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
        /// 是否封帐状态
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
        /// 查询所有合同单位
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitAll()
        {
            this.SetDB(pactManager);

            return pactManager.QueryPactUnitAll();
        }
        /// <summary>
        /// 获得门诊合同单位信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitOutPatient()
        {
            this.SetDB(pactManager);
            return pactManager.QueryPactUnitOutPatient();
        }
        /// <summary>
        /// 获得住院合同单位信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitInPatient()
        {
            this.SetDB(pactManager);
            return pactManager.QueryPactUnitInPatient();
        }
        /// <summary>
        /// 提交函数
        /// </summary>
        /// 按着HIS4.5.0.1的commit方式修改
        public void Commit()
        {
            //this.trans.Commit();
            //if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null)
            //{
            //    medcareInterfaceProxy.Commit();
            //}
            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null && medcareInterfaceProxy.PactCode != "" && medcareInterfaceProxy.PactCode != null)
            {
                if (medcareInterfaceProxy.Commit() < 0) //沈阳医保 0成功 -1失败
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
        /// 提交公费接口函数
        /// </summary>
        /// 这里单独给药房退费退药时使用了，其他地方如果需要也可以使用
        public int MedcareInterfaceCommit()
        {

            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null && medcareInterfaceProxy.PactCode != "" && medcareInterfaceProxy.PactCode != null)
            {
                if (medcareInterfaceProxy.Commit() < 0) //沈阳医保 0成功 -1失败
                {
                    medcareInterfaceProxy.Rollback();
                    return -1;
                }
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// 回滚公费接口函数
        /// </summary>
        public void MedcareInterfaceRollback()
        {
            if (!this.isIgnoreMedcareInterface && medcareInterfaceProxy != null)
            {
                medcareInterfaceProxy.Rollback();
            }
        }
        /// <summary>
        /// 回滚函数
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
        /// 项目收费
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int FeeItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Fee, TransTypes.Positive);
        }

        /// <summary>
        /// 项目收费
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <returns>成功 1 失败 -1</returns>
        public int FeeItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Fee, TransTypes.Positive);
        }

        /// <summary>
        /// 项目退费
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int QuitItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Fee, TransTypes.Negative);
        }

        /// <summary>
        /// 项目退费
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <returns>成功 1 失败 -1</returns>
        public int QuitItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Fee, TransTypes.Negative);
        }

        /// <summary>
        /// 项目划价
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">费用明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int ChargeItem(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.FeeManager(patient, feeItemList, ChargeTypes.Charge, TransTypes.Positive);
        }

        /// <summary>
        /// 项目划价
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <returns>成功 1 失败 -1</returns>
        public int ChargeItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeItemLists)
        {
            return this.FeeManager(patient, ref feeItemLists, ChargeTypes.Charge, TransTypes.Positive);
        }

        /// <summary>
        /// 循环插入结算明细
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <param name="balanceLists">结算明细集合</param>
        /// <returns>成功 1 失败 -1</returns>
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
        /// 获得发票默认起始号
        /// </summary>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>成功 发票默认起始号 失败 null</returns>
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
        /// 获得所有发票组信息
        /// </summary>
        /// <returns>成功 发票组信息 失败 null</returns>
        public ArrayList QueryFinaceGroupAll()
        {
            this.SetDB(employeeFinanceGroupManager);

            return employeeFinanceGroupManager.QueryFinaceGroupIDAndNameAll();
        }

        /// <summary>
        /// 验证发票区间是否合法
        /// </summary>
        /// <param name="startNO">开始号</param>
        /// <param name="endNO">结束号</param>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>合法 True, 不合法 false</returns>
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
        /// 获得发票大类的DataSet
        /// </summary>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="ds">发票大类的DataSet</param>
        /// <returns>成功 1 失败 -1</returns>
        public int GetInvoiceClass(string invoiceType, ref DataSet ds)
        {
            this.SetDB(outpatientManager);
            // TODO: 编译不过去，临时改一下
            return outpatientManager.GetInvoiceClass(invoiceType, ref ds);
        }

        /// <summary>
        /// 获得患者药品信息
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public ArrayList QueryMedcineList(string inpatientNO, DateTime beginTime, DateTime endTime, string deptCode)
        {
            this.SetDB(inpatientManager);

            return inpatientManager.QueryMedItemListsByInpatientNO(inpatientNO, beginTime, endTime, deptCode);

        }

        /// <summary>
        /// 获得患者非药品信息
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="deptCode">科室编码</param>
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
        /// 根据执行科室查费用信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="execDeptCode">执行科室代码</param>
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
        /// 根据执行科室查询非药品费用信息
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
        /// 检索药品和非药品明细单条记录---通过主键{5C2A9C83-D165-434c-ACA4-86F23E956442}
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSequence">处方内流水号</param>
        /// <param name="isPharmacy">是否药品 Drug(true)是 UnDrug(false)非药品</param>
        /// <returns>成功: 药品和非药品明细单条记录 失败: null</returns>
        public FS.HISFC.Models.Fee.Inpatient.FeeItemList GetItemListByRecipeNO(string recipeNO, int recipeSequence, EnumItemType isPharmacy)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.GetItemListByRecipeNO(recipeNO, recipeSequence, isPharmacy);
        }

        #region 非药品项目信息

        /// <summary>
        /// 获得非药品信息（不管是否有效）
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemCode)
        {
            this.SetDB(itemManager);

            //houwb 查询有效的应该是方法GetValidItemByUndrugCode
            //return itemManager.GetValidItemByUndrugCode(itemCode);
            return itemManager.GetItemByUndrugCode(itemCode);
        }

        //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} 取出非药品及物资
        /// <summary>
        /// 取出非药品或物资
        /// </summary>
        /// <param name="itemCode">非药品或物资编码</param>
        /// <param name="price">单价、非药品可传入0</param>
        /// <returns>非药品或物资实体</returns>
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
        /// 项目是否被使用过
        /// </summary>
        /// <param name="itemCode">项目ID</param>
        /// <returns>true:使用</returns>
        public bool IsUsed(string itemCode)
        {
            this.SetDB(itemManager);
            return itemManager.IsUsed(itemCode);
        }

        /// <summary>
        /// 删除非药品信息
        /// </summary>
        /// <param name="undrugCode">非药品编码</param>
        /// <returns>成功 1 失败 -1 未删除到数据 0</returns>
        public int DeleteUndrugItemByCode(string undrugID)
        {
            this.SetDB(itemManager);
            return itemManager.DeleteUndrugItemByCode(undrugID);
        }

        /// <summary>
        /// 获得所有可能的项目信息
        /// </summary>
        /// <returns>成功 有效的可用项目信息, 失败 null</returns>
        public ArrayList QueryValidItems()
        {
            this.SetDB(itemManager);
            return itemManager.QueryValidItems();
        }

        /// <summary>
        /// 获得所有项目信息
        /// </summary>
        /// <returns>成功 所有项目信息, 失败 null</returns>
        public List<FS.HISFC.Models.Fee.Item.Undrug> QueryAllItemsList()
        {
            this.SetDB(itemManager);
            return itemManager.QueryAllItemList();
        }

        #endregion

        #region 非药品组套

        /// <summary>
        ///  插入非药品组合项目
        /// </summary>
        /// <param name="undrugComb">非药品组合项目实体</param>
        /// <returns>成功: 1 失败 : -1 没有插入数据 0</returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public int InsertUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);
            //return undrugCombManager.InsertUndrugComb(undrugComb);
        }

        /// <summary>
        /// 更新 非药品组套中的数据
        /// </summary>
        /// <param name="undrugComb">非药品组合项目实体</param>
        /// <returns>成功: 1 失败 : -1 没有更新到数据 0</returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public int UpdateUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.UpdateUndrugComb(undrugComb);
        }

        /// <summary>
        ///  删除非药品组合项目
        /// </summary>
        /// <param name="undrugComb">非药品组合项目实体</param>
        /// <returns>成功: 1 失败 : -1 没有删除到数据 0</returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public int DeleteUndrugComb(FS.HISFC.Models.Fee.Item.UndrugComb undrugComb)
        {
            return -1;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.DeleteUndrugComb(undrugComb);
        }

        /// <summary>
        /// 通过组合项目编码获取一条组合项目
        /// </summary>
        /// <param name="undrugCombCode">组合项目编码</param>
        /// <returns>成功: 一条组合项目 失败: null</returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public FS.HISFC.Models.Fee.Item.UndrugComb GetUndrugCombByCode(string undrugCombCode)
        {
            FS.HISFC.Models.Fee.Item.UndrugComb com = new FS.HISFC.Models.Fee.Item.UndrugComb();
            return com;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.GetUndrugCombByCode(undrugCombCode);
        }

        /// <summary>
        /// 通过组合项目编码获取一条有效组合项目
        /// </summary>
        /// <param name="undrugCombCode">组合项目编码</param>
        /// <returns>成功: 一条有效组合项目 失败: null</returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public FS.HISFC.Models.Fee.Item.UndrugComb GetUndrugCombValidByCode(string undrugCombCode)
        {
            FS.HISFC.Models.Fee.Item.UndrugComb com = new FS.HISFC.Models.Fee.Item.UndrugComb();
            return com;
            //this.SetDB(undrugCombManager);

            //return undrugCombManager.GetUndrugCombValidByCode(undrugCombCode);
        }
        /// <summary>
        /// 获取复合项目的总价格
        /// </summary>
        /// <param name="undrugCombCode">复合项目编码</param>
        /// <returns></returns>
        public decimal GetUndrugCombPrice(string undrugCombCode)
        {
            this.SetDB(undrugPackAgeMgr);

            return undrugPackAgeMgr.GetUndrugCombPrice(undrugCombCode);
        }

        #endregion

        /// <summary>
        /// 更新非药品明细可退数量
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSequence">处方内流水号</param>
        /// <param name="qty">可退数量</param>
        /// <param name="balanceState">结算状态</param>
        /// <returns>成功: 1 失败 : -1 没有更新数据: 0</returns>
        public int UpdateNoBackQtyForUndrug(string recipeNO, int recipeSequence, decimal qty, string balanceState)
        {
            if (balanceState == "5")//{139695FB-AA0D-485F-BFBF-56928F441250}添加体检的可退数量操作
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
        /// 更新非药品明细可退数量
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSequence">处方内流水号</param>
        /// <param name="qty">可退数量</param>
        /// <param name="balanceState">结算状态</param>
        /// <returns>成功: 1 失败 : -1 没有更新数据: 0</returns>
        public int UpdateClinicNoBackQtyForUndrug(string recipeNO, int recipeSequence, decimal qty)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateNoBackQty(recipeNO, recipeSequence, qty);
        }

        /// <summary>
        /// 更新非药品明细确认数量
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSequence">处方内流水号</param>
        /// <param name="confrimNum">确认数量</param>
        /// <param name="balanceState">结算状态</param>
        /// <returns>成功: 1 失败 : -1 没有更新数据: 0</returns>
        public int UpdateConfirmNumForUndrug(string recipeNO, int recipeSequence, decimal confrimNum, string balanceState)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateConfirmNumForUndrug(recipeNO, recipeSequence, confrimNum, balanceState);
        }

        /// <summary>
        /// 更新非药品明细扩展标记
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="recipeSequence">处方内流水号</param>
        /// <param name="extFlag2">扩展标记2</param>
        /// <param name="balanceState">结算状态</param>
        /// <returns>成功: 1 失败 : -1 没有更新数据: 0</returns>
        public int UpdateExtFlagForUndrug(string recipeNO, int recipeSequence, string extFlag2, string balanceState)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.UpdateExtFlagForUndrug(recipeNO, recipeSequence, extFlag2, balanceState);
        }

        /// <summary>
        /// 获得患者和执行科室已经确认的非药品收费明细
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="execDeptCode">科室代码</param>
        /// <returns>成功:患者非药品信息 失败:null 没有找到记录 ArrayList.Count = 0</returns>
        public ArrayList QueryExeFeeItemListsByInpatientNOAndDept(string inpatientNO, string execDeptCode)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.QueryExeFeeItemListsByInpatientNOAndDept(inpatientNO, execDeptCode);
        }

        /// <summary>
        /// 查询床位费
        /// </summary>
        /// <param name="minFeeCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedFeeItemByMinFeeCode(string minFeeCode)
        {
            this.SetDB(inpatientManager);
            return feeBedFeeItem.QueryBedFeeItemByMinFeeCode(minFeeCode);
        }

        /// <summary>
        /// 查询床位费（用于个人）
        /// </summary>
        /// <param name="minFeeCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.BedFeeItem QueryBedFeeItemForPatient(string inpatientNO, string bedNO, string bedGradeKey)
        {
            this.SetDB(feeBedFeeItem);
            return feeBedFeeItem.QueryBedGradeForPatient(inpatientNO, bedNO, bedGradeKey);
        }

        /// <summary>
        /// 保存床位费（个人）
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
        /// 获取新非药品处方号
        /// </summary>
        /// <returns></returns>
        public string GetUndrugRecipeNO()
        {
            this.SetDB(inpatientManager);
            return inpatientManager.GetUndrugRecipeNO();
        }

        /// <summary>
        /// 查询有效项目
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetValidItemByUndrugCode(string itemID)
        {
            this.SetDB(itemManager);
            return itemManager.GetValidItemByUndrugCode(itemID);
        }

        #region 获取价格

        /// <summary>
        /// 复合项目明细
        /// </summary>
        private Hashtable hsUndrugZTDetail = null;

        /// <summary>
        /// 获取非药品明细价格的加成（优惠）比例
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
        /// 保存合同单位静态函数，避免每次查询价格都取合同单位信息
        /// </summary>
        private Hashtable htPactInfo = new Hashtable();

        /// <summary>
        /// 住院取价格函数，根据合同单位和项目信息获取患者收费价格和原始价格
        /// </summary>
        /// <param name="pactCode">合同单位</param>
        /// <param name="item">收费项目</param>
        /// <param name="feePrice">收费价格</param>
        /// <param name="orgPrice">原始价格（本来应收价格，不考虑合同单位因素）</param>
        /// <returns></returns>
        [Obsolete("废弃原来的获取价格的函数，采用新的获取价格函数")]
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
                    this.Err = "根据合同单位获取价格出错！";
                    return -1;
                }
                if (item.ItemType == EnumItemType.Drug)
                {
                    decimal defaultPrice = ((FS.HISFC.Models.Pharmacy.Item)item).PriceCollection.RetailPrice;
                    decimal purchasePrice = ((FS.HISFC.Models.Pharmacy.Item)item).RetailPrice2;
                    if (pactinfo.PriceForm == "购入价" && purchasePrice != 0)
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
                    if (pactinfo.PriceForm == "特诊价" && item.SpecialPrice != 0)
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
        /// 住院取价格函数，根据合同单位和项目信息获取患者收费价格和原始价格
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="item">收费项目</param>
        /// <param name="feePrice">收费价格</param>
        /// <param name="orgPrice">原始价格（本来应收价格，不考虑合同单位因素）</param>
        /// <param name="rate">价格加成（优惠）比例</param>
        /// <returns></returns>
        public int GetPriceForInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Item item, ref decimal feePrice, ref decimal orgPrice, decimal rate)
        {
            decimal price = GetPriceForInpatient(patientInfo, item, ref feePrice, ref orgPrice);
            feePrice = feePrice * rate;

            return 1;
        }

        /// <summary>
        /// 住院取价格函数，根据合同单位和项目信息获取患者收费价格和原始价格
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="item">收费项目</param>
        /// <param name="feePrice">收费价格</param>
        /// <param name="orgPrice">原始价格（本来应收价格，不考虑合同单位因素）</param>
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

        #region  //{B9303CFE-755D-4585-B5EE-8C1901F79450}重写获取价格函数
        /// <summary>
        /// 根据价格形式获取价格
        /// </summary>
        /// <param name="PriceForm">价格形式</param>
        /// <param name="age">年龄</param>
        /// <param name="UnitPrice">默认价格</param>
        /// <param name="ChildPrice">儿童价</param>
        /// <param name="SPPrice">特诊价</param>
        /// <param name="PurchasePrice">购入价</param>
        /// <returns></returns>
        [Obsolete("废弃原来的获取价格的函数，采用新的获取价格函数")]
        public decimal GetPrice(string PriceForm, int age, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice)
        {
            if (PriceForm == "三甲价")//三甲
            {
                return UnitPrice;
            }
            else if (PriceForm == "儿童价")//儿童
            {
                if (age <= 14)
                {
                    return ChildPrice;
                }
                return UnitPrice;
            }
            else if (PriceForm == "购入价")
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
            else if (PriceForm == "特诊价")
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
        /// 根据价格形式获取价格
        /// </summary>
        /// <param name="PriceForm">价格形式</param>
        /// <param name="age">年龄</param>
        /// <param name="UnitPrice">默认价格</param>
        /// <param name="ChildPrice">儿童价</param>
        /// <param name="SPPrice">特诊价</param>
        /// <param name="PurchasePrice">购入价</param>
        /// <param name="rate">价格加成（优惠）比例</param>
        /// <returns></returns>
        public decimal GetPrice(string itemCode, FS.HISFC.Models.Registration.Register register, int age, decimal UnitPrice, decimal ChildPrice, decimal SPPrice, decimal PurchasePrice, ref decimal orgPrice, decimal rate)
        {
            decimal price = this.GetPrice(itemCode, register, age, UnitPrice, ChildPrice, SPPrice, PurchasePrice, ref orgPrice);

            return price * rate;
        }

        /// <summary>
        /// 根据价格形式获取价格
        /// </summary>
        /// <param name="PriceForm">价格形式</param>
        /// <param name="age">年龄</param>
        /// <param name="UnitPrice">默认价格</param>
        /// <param name="ChildPrice">儿童价</param>
        /// <param name="SPPrice">特诊价</param>
        /// <param name="PurchasePrice">购入价</param>
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
                //项目比例
                decimal rate = this.itemManager.GetItemRate(itemCode);
                //价格
                decimal price = UnitPrice;

                if (register != null)
                {
                    price = this.GetPrice(register.Pact.PriceForm, age, UnitPrice, ChildPrice, SPPrice, PurchasePrice);
                }
                //原始价格
                orgPrice = price;
                //新价格
                return price * rate;
            }
        }

        #endregion

        #endregion

        #endregion

        #region 门诊

        #region 参数

        /// <summary>
        /// 获得指定控制参数
        /// </summary>
        /// <param name="controlID">控制类ID</param>
        /// <param name="defaultValue">默认值，没有找到返回此值</param>
        /// <returns>控制参数</returns>
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

        #region 门诊收费函数

        /// <summary>
        /// 获得当前接口插件
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="controlCode">反射插件参数编码</param>
        /// <param name="defalutInstance">默认插件</param>
        /// <returns>成功T类型实例 错误 返回默认实例</returns>
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
                    MessageBox.Show("反射失败!请确认您选择的dll和uc的正确和完整! 将采用默认插件");

                    return defalutInstance;
                }

                object obj = objPlugin.Unwrap();

                defalutInstance = default(T);

                return (T)obj;
            }
            catch (Exception e)
            {
                MessageBox.Show("当前插件参数维护错误! 将采用默认插件" + e.Message);

                return defalutInstance;
            }
        }

        /// <summary>
        /// 获得患者的未收费项目信息
        /// </summary>
        /// <param name="clinicNO">挂号流水号</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public System.Collections.ArrayList QueryChargedFeeItemListsByClinicNO(string clinicNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryChargedFeeItemListsByClinicNO(clinicNO);
        }

        /// <summary>
        /// 获得患者的已收费项目信息
        /// </summary>
        /// <param name="clinicNO">挂号流水号</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public System.Collections.ArrayList QueryFeeItemListsByClinicNO(string clinicNO)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryFeeItemListsByClinicNO(clinicNO);
        }

        /// <summary>
        /// 获得患者一次看诊的所有费用信息
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="isFee">ALL表示全部</param>
        /// <param name="subFlag">ALL表示全部</param>
        /// <param name="costSource">ALL表示全部</param>
        /// <returns></returns>
        public System.Collections.ArrayList QueryAllFeeItemListsByClinicNO(string clinicNO, string isFee, string subFlag, string costSource)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.QueryAllFeeItemListsByClinicNO(clinicNO, isFee, subFlag, costSource);
        }

        /// <summary>
        /// 控制参数帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 转换科室帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsInvertDept = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 分处方号忽略类别
        /// </summary>
        private static bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// 每次用量可否为空
        /// </summary>
        public static bool isDoseOnceCanNull = false;

        /// <summary>
        /// 不打小票的科室
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper printRecipeHeler = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 获得[最大的流水号]和处方号
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
        /// 针对收费项目列表按照 系统类别，执行科室，付数 声称处方号
        /// 同一系统类别，统一执行科室，同一付数的项目处方号相同
        /// 对已经分配好处方号的项目不进行重新分配
        /// </summary>
        /// <param name="feeDetails">费用信息</param>
        /// <param name="t">数据库Trans</param>
        /// <param name="errText">错误信息</param>
        /// <returns>false失败 true成功</returns>
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
                    //分处方
                    return iSplitRecipe.SplitRecipe(r, feeDetails, ref errText);
                    //if (returnValue < 0)
                    //{
                    //    return false;
                    //}

                }
                else
                {
                    #region 默认的实现
                    bool isDealCombNO = false; //是否优先处理组合号
                    int noteCounts = 0;        //获得单张处方最多的项目数

                    //是否优先处理组合号
                    isDealCombNO = controlParamIntegrate.GetControlParam<bool>(Const.DEALCOMBNO, false, true);

                    //获得单张处方最多的项目数, 默认项目数 5
                    noteCounts = controlParamIntegrate.GetControlParam<int>(Const.NOTECOUNTS, false, 5);

                    //是否忽略系统类别
                    isDecSysClassWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(Const.DEC_SYS_WHENGETRECIPE, false, false);

                    //是否优先处理暂存记录
                    bool isDecTempSaveWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(Const.处方号优先考虑分方记录, false, false);

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


                        if (isDealCombNO)//优先处理组合号，将所有的组合号再重新分组
                        {
                            //挑选出没有组合号的项目
                            foreach (FeeItemList f in temp)
                            {
                                if (f.Order.Combo.ID == null || f.Order.Combo.ID == string.Empty)
                                {
                                    noCombAll.Add(f);
                                }
                            }
                            //从整体数组中删除没有组合号的项目
                            foreach (FeeItemList f in noCombAll)
                            {
                                temp.Remove(f);
                            }
                            //针对同一处方最多项目数再重新分组
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
                            //如果剩余的项目条目> 0说明还有组合的项目
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
                                string recipeNo = null;//处方流水号
                                int noteSeq = 1;//处方内项目流水号

                                string tempRecipeNO = string.Empty;
                                int tempSequence = 0;
                                this.GetRecipeNoAndMaxSeq(tempNoComb, ref tempRecipeNO, ref tempSequence, r.ID);

                                if (tempRecipeNO != string.Empty && tempSequence > 0)
                                {
                                    tempSequence += 1;
                                    foreach (FeeItemList f in tempNoComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                        errText = "获得处方号出错!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempNoComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                string recipeNo = null;//处方流水号
                                int noteSeq = 1;//处方内项目流水号

                                string tempRecipeNO = string.Empty;
                                int tempSequence = 0;
                                this.GetRecipeNoAndMaxSeq(tempComb, ref tempRecipeNO, ref tempSequence, r.ID);

                                if (tempRecipeNO != string.Empty && tempSequence > 0)
                                {
                                    tempSequence += 1;
                                    foreach (FeeItemList f in tempComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                        errText = "获得处方号出错!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempComb)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                        else //不优先处理组合号
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
                                string recipeNO = null;//处方流水号
                                int recipeSequence = 1;//处方内项目流水号

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
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                        errText = "获得处方号出错!";
                                        return false;
                                    }
                                    foreach (FeeItemList f in tempCounts)
                                    {
                                        feeDetails.Add(f);
                                        if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
        /// 重新生成收费序列【每次医嘱保存,根据此序列形成多张收费处方】
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public bool SetRecipeFeeSeqOutPatient(Register r, ArrayList feeDetails, ref string errText)
        {
            try
            {
                //没有收费项目，直接返回
                if (feeDetails == null || feeDetails.Count <= 0)
                {
                    return true;
                }

                // 获得是否分开（项目次数超过医保数量）收费,默认为值为 false即不按照超过数量分收费序列.
                bool isSplitByExceeded = controlParamIntegrate.GetControlParam<bool>(Const.IS_SPLIT_RECIPE_SEQ_BY_EXCEED, false, false);


                //1、药品和非药品分开；2、非药品全部在一起；3、药品根据取药药房分开；4、同一组合号COMB_NO在一起，否则辅材删除和带出有问题
                Hashtable hsRecipeFeeSeq = new Hashtable();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    #region 非药品(辅材)与主项目在一起-先过滤后面统一处理

                    if (f.Item.IsMaterial)
                    {
                        continue;
                    }

                    #endregion

                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        #region 药品根据取药药房分开

                        string pharmacyDeptCode = f.ExecOper.Dept.ID;  //药品的取药药房
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
                        #region 非药品(非辅材)全部在一起

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

                //通过HashTable来分别赋值同一组的收费序列【处理主项目】
                IDictionaryEnumerator iDE = hsRecipeFeeSeq.GetEnumerator();
                while (iDE.MoveNext())
                {
                    ArrayList al = iDE.Value as ArrayList;
                    if (al == null || al.Count <= 0)
                    {
                        continue;
                    }

                    //如果同一组合中，已经有收费序列，则使用旧的收费序列，否则使用新序列
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

                //处理相同组号的辅材，收费序列号与主项目一致【处理辅材】
                Hashtable hsSubFeeByCombNo = new Hashtable();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    //找出同一组合号的主项目收费序列
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
                    //找到辅材的主项目的收费序列赋值
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
        /// 门诊明细数据校验
        /// </summary>
        /// <param name="f">费用实体</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功 true 失败 false</returns>
        public bool IsFeeItemListDataValid(FeeItemList f, ref string errText)
        {
            string itemName = f.Item.Name;
            if (f == null)
            {
                errText = itemName + "获得费用实体出错!";

                return false;
            }
            if (f.Item.ID == null || f.Item.ID == string.Empty)
            {
                errText = itemName + "项目编码没有付值";

                return false;
            }
            if (f.Item.Name == null || f.Item.Name == string.Empty)
            {
                errText = itemName + "项目名称没有付值";

                return false;
            }
            //if (f.Item.IsPharmacy)
            if (f.Item.ItemType == EnumItemType.Drug && f.FTSource != "0")
            {
                #region 根据参数&& !isDoseOnceCanNull 来判断是否需要输入各个值 刘兴强20070828
                if ((f.Order.Frequency.ID == null || f.Order.Frequency.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "频次代码没有付值";

                    return false;
                }
                if ((f.Order.Usage.ID == null || f.Order.Usage.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "用法代码没有付值";

                    return false;
                }
                if (f.Order.DoseOnce == 0 && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "每次用量没有付值";

                    return false;
                }
                if ((f.Order.DoseUnit == null || f.Order.DoseUnit == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "每次用量单位没有付值";

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
                    //    errText = itemName + "的规格没有付值";

                    //    return false;
                    //}
                    if (f.Item.PackQty == 0)
                    {
                        errText = itemName + "包装数量没有付值";

                        return false;
                    }
                }
            }
            if (f.Item.PriceUnit == null || f.Item.PriceUnit == string.Empty)
            {
                errText = itemName + "计价单位没有付值";

                return false;
            }

            if (f.Item.MinFee.ID == null || f.Item.MinFee.ID == string.Empty)
            {
                errText = itemName + "最小费用没有付值";

                return false;
            }
            if (f.Item.SysClass.ID == null || f.Item.SysClass.Name == string.Empty)
            {
                errText = itemName + "系统类别没有付值";

                return false;
            }
            if (f.Item.Qty == 0)
            {
                errText = itemName + "数量没有付值";

                return false;
            }
            //四舍五入费用处理，暂时屏蔽{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.Item.Qty < 0)
            //{
            //    errText = itemName + "数量不能小于0";

            //    return false;
            //}
            if (f.Item.Qty > 99999)
            {
                errText = itemName + "数量不能大于99999";

                return false;
            }

            if (f.Days == 0)
            {
                errText = itemName + "草药付数没有付值";

                return false;
            }
            if (f.Days < 0)
            {
                errText = itemName + "草药付数不能小于0";

                return false;
            }

            if (f.Item.Price < 0)
            {
                errText = itemName + "单价不能小于0";

                return false;
            }

            //对于自备药等 允许收取费用为0项目
            if (this.IsFeeWhenPriceZero == "0")
            {
                if (f.Item.ID != "999")
                {
                    if (f.Item.Price == 0 && f.Item.User03 != "全免")
                    {
                        errText = itemName + "单价没有付值";

                        return false;
                    }
                    //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost == 0)
                    //{
                    //    errText = itemName + "项目金额没有付值";

                    //    return false;
                    //}
                }
            }

            //四舍五入费用处理，暂时屏蔽{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost < 0)
            //{
            //    errText = itemName + "项目金额为负";

            //    return false;
            //}
            ////{8DF48FD8-14E9-464a-A368-256B19A0EE54} 修改又会比例
            //if (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) != FS.FrameWork.Public.String.FormatNumber
            //    (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost /*+ f.FT.RebateCost*/, 2))
            //{
            //    errText = itemName + "金额与单价数量不符";

            //    return false;
            //}

            if (f.Item.ID == "999" && f.Item.ItemType == EnumItemType.Drug)
            {
            }
            else
            {
                if (f.ExecOper.Dept.ID == null || f.ExecOper.Dept.ID == string.Empty)
                {
                    errText = itemName + "执行科室代码没有付值";

                    return false;
                }
                if (f.ExecOper.Dept.Name == null || f.ExecOper.Dept.Name == string.Empty)
                {
                    errText = itemName + "执行科室名称没有付值";

                    return false;
                }
            }

            return true;
        }

        #region 删除　集体体检汇总划价信息
        /// <summary>
        /// 根据体检流水号和发票组合号删除体检汇总信息　
        /// </summary>
        /// <param name="ClinicNO">体检流水号</param>
        /// <param name="RecipeNO">发票组合号</param>
        /// <returns></returns>
        public int DeleteFeeItemListByClinicNOAndRecipeNO(string ClinicNO, string RecipeNO)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeItemListByClinicNOAndRecipeNO(ClinicNO, RecipeNO);
        }
        #endregion


        /// <summary>
        /// 更新发票主表表FIN_OPB_INVOICEINFO
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
        /// 更新发票明细表FIN_OPB_INVOICEDETAIL
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
        /// 更新支付情况表FIN_OPB_PAYMODE
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
        public static string invoiceStytle = "0";//发票方式

        /// <summary>
        /// 获得体检所有收费序列
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
        /// 拆分协定处方
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList SplitNostrumDetail(FS.HISFC.Models.Registration.Register rInfo, FeeItemList f, ref string errText)
        {
            List<FS.HISFC.Models.Pharmacy.Nostrum> listDetail = this.PharmarcyManager.QueryNostrumDetail(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (listDetail == null)
            {
                errText = "获得协定处方明细出错!" + PharmarcyManager.Err;

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
                    errText = "获得医嘱流水号出错!";

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
                    errText = "查找协定处方明细出错!";

                    continue;
                }

                feeDetail = new FeeItemList();
                feeDetail.Item = item;
                feeCode = item.MinFee.ID;
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - rInfo.Birthday.Ticks)).TotalDays / 365);
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}增加获取购入价
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
                //收取的协定处方以最小单位收取,明细数量 = 界面上输入的协定处方数量 * 对应明细项目数量 / 协定处方包装数
                if (f.FeePack == "0")//最小单位
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty / f.Item.PackQty;
                }
                else //收取的协定处方以包装单位收取,明细数量 = 界面上输入的协定处方数量 * 对应明细项目数量
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

                //自费如此，如果加上公费需要重新计算!!!
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
                //            if (pactRate.Rate.PayRate == 1)//自费
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
                if (f.FT.RebateCost > 0)//有减免
                {
                    if (rInfo.Pact.PayKind.ID != "01")
                    {
                        errText = "暂时不允许非自费患者减免!";

                        return null;
                    }
                    //减免单独算
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
                if (f.SpecialPrice > 0)//有特殊自费
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
        /// 拆分协定处方
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
                            errText = f.Item.Name + "是协定处方,但是没有维护明细或者明细已经停用！请与信息科联系！";
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
        /// 门诊收费函数
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5}
        /// 
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", false, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }

        /// <summary>
        /// 门诊收费函数
        /// 
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// 
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="isTempInvoice">是否使用临时发票</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="invoiceFeeDetails">发票明细信息</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", isTempInvoice, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }

        /// <summary>
        /// 门诊收费函数
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5} 
        /// 增加参数，允许指定发票类型
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="isTempInvoice">是否使用临时发票</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="invoiceFeeDetails">发票明细信息</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <param name="oper">操作者</param>
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

            //是否才分协定处方
            bool isSplitNostrum = controlParamIntegrate.GetControlParam<bool>(Const.Split_NostrumDetail, false, false);

            //获得收费时间
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //获得收费操作员
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //返回值
            int iReturn = 0;
            //定义处方号
            string recipeNO = string.Empty;

            //如果是收费，获得发票信息
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//收费
            {
                #region 收费流程

                /*
                 * 1、
                 * 发票已经在预览界面分配完毕,直接插入就可以了.
                 *
                 * 2、
                 * 门诊结算，应该区分优惠金额和赠送金额
                 * ECO(优惠)；PY(套餐优惠)
                 * ACD(帐户赠送)；PD(套餐赠送)； 
                 * 
                 * */


                #region 分别统计套餐内套餐外项目总金额，折扣金额，优惠金额

                //费用进来的时候不会带进来赠送金额，套餐赠送和赠送都是在确定支付方式之后才产生的

                ////{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                //套餐内项目金额统计
                decimal totPackage = 0.0m;
                decimal totPackageReal = 0.0m;
                decimal totPackageGift = 0.0m;   
                decimal totPackageEco = 0.0m;

                //套餐外项目金额统计
                decimal totNormal = 0.0m;
                decimal totNormalReal = 0.0m;
                decimal totNormalGift = 0.0m;
                decimal totNormalEco = 0.0m;

                ArrayList packageItems = new ArrayList();
                ArrayList normalItems = new ArrayList();

                foreach (FeeItemList feeItemList in feeDetails)
                {
                    //初始化赠送金额，此操作一定要做
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
                            case "PR": //套餐实收
                                packageRealPay += pm.FT.TotCost;
                                break;
                            case "PD": //套餐赠送
                                packageGiftPay += pm.FT.TotCost;
                                break;
                            case "PY": //套餐优惠
                                packageEcoPay += pm.FT.TotCost;
                                break;
                            case "DC": //赠送
                                normalGiftPay += pm.FT.TotCost;
                                break;
                            case "CO": //积分当赠送金额处理
                                normalGiftPay += pm.FT.TotCost;
                                break;
                            case "RC": //优惠
                                normalEcoPay += pm.FT.TotCost;
                                break;
                            default:  //默认都是实收
                                normalRealPay += pm.FT.TotCost;
                                break;
                        }
                    }
                }

                //如果套餐中所扣金额大于套餐内项目金额
                if (totPackageReal + totPackageGift < packageRealPay + packageGiftPay + packageEcoPay)
                {
                    MessageBox.Show("套餐项目金额大于需要抵扣的金额！");
                    return false;
                }

                decimal packageweight = totPackageReal + totPackageGift - packageRealPay - packageGiftPay - packageEcoPay;
                decimal normalweight = totNormalReal;

                //normalGiftPay
                //normalRealPay
                //分配给套餐内项目的赠送金额
                //decimal normalRealPayforPackage = 0.0m;
                decimal normalGiftPayforPackage = 0.0m;

                //分配给套餐外项目的赠送金额
                //decimal normalRealPayforNormal = 0.0m;
                decimal normalGiftPayforNormal = 0.0m;

                normalGiftPayforPackage = Math.Floor((packageweight * normalGiftPay * 100) / ((packageweight + normalweight) == 0 ? 1 : (packageweight + normalweight))) / 100;
                normalGiftPayforNormal = normalGiftPay - normalGiftPayforPackage;
                //极端情况，如果套餐外项目只有赠送金额，则赠送金额不能大于应收金额（下取整会导致这种情况产生）
                if (normalGiftPayforNormal > totNormalReal)
                {
                    normalGiftPayforNormal = totNormal - totNormalEco;
                    normalGiftPayforPackage = normalGiftPay - normalGiftPayforNormal;
                }
                #endregion 

                #region 套餐内项目分配赠送优惠

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
                        //赠送和优惠都分配完了，不继续循环
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
                        //赠送金额每次往上取整，导致赠送金额有可能会多扣1分钱
                        //于是在计算优惠金额的时候，优惠金额会少分配，导致最后优惠金额没分配完报错
                        //因此设置一个标记，这个项目的赠送金额是被少分配了的话，此处优惠金额设置为
                        //总花费减原有优惠减去赠送金额
                        //即使 的出来的 数字大于packageEcoCost，也会在后面的判断中被调整
                        //这种情况往往发生在最后几个项目
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
                        //套餐外项目只需要分配赠送金额，优惠金额已经在打折的时候算好了
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
                    MessageBox.Show("赠送金额和优惠金额分配失败！");
                    return false;
                }

                #endregion 

                #region 作废旧方法
                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                #region 优惠金额和赠送金额处理

                ////套餐明细
                //Dictionary<string, FeeItemList> dictPackDetail = new Dictionary<string, FeeItemList>();

                //decimal totCost = 0;          //总金额

                //decimal feeTotEcoCost = 0;      //费用明细优惠金额(需要减掉单个项目的优惠金额)
                //decimal feeTotDonateCost = 0;   //费用明细赠送金额(需要减掉单个项目的赠送金额)

                //decimal balanceTotEcoCost = 0;     //发票优惠金额(需要减掉单个项目原本的优惠金额)
                //decimal balanceTotDonateCost = 0;  //发票赠送金额(需要减掉单个项目原本的优惠金额)

                //foreach (BalancePay pm in payModes)
                //{
                //    FS.FrameWork.Models.NeuObject objPay = this.PayModesHelper.GetObjectFromID(pm.PayType.ID);
                //    if (objPay != null)
                //    {
                //        //总金额
                //        totCost += pm.FT.TotCost;

                //        //优惠金额
                //        if ((objPay as Models.Base.Const).UserCode == "ECO" || (objPay as Models.Base.Const).UserCode == "PY")
                //        {
                //            feeTotEcoCost += pm.FT.TotCost;
                //        }

                //        //赠送金额
                //        if ((objPay as Models.Base.Const).UserCode == "ACD" || (objPay as Models.Base.Const).UserCode == "PD")
                //        {
                //            feeTotDonateCost += pm.FT.TotCost;
                //        }

                //        #region 套餐明细

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

                ////1、发票实体优惠金额(需要减掉单个项目原本的优惠金额)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    //优惠金额
                //    balanceTotEcoCost -= f.FT.RebateCost;
                //    //赠送金额
                //    balanceTotDonateCost -= f.FT.DonateCost;

                //}

                ////套餐明细匹配
                ////(逻辑与业务不契合，此处不应该)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    if (dictPackDetail.ContainsKey(f.Item.ID))
                //    {
                //        FeeItemList pd = dictPackDetail[f.Item.ID];

                //        if ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) > pd.FT.OwnCost)
                //        {
                //            //优惠金额
                //            f.FT.RebateCost += pd.FT.RebateCost;
                //            //赠送金额
                //            f.FT.DonateCost += pd.FT.DonateCost;
                //        }
                //        else
                //        {
                //            //优惠金额
                //            f.FT.RebateCost += Math.Round(pd.FT.RebateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (pd.FT.OwnCost != 0 ? pd.FT.OwnCost : 1)), 2);
                //            //赠送金额
                //            f.FT.DonateCost += Math.Round(pd.FT.DonateCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (pd.FT.OwnCost != 0 ? pd.FT.OwnCost : 1)), 2);
                //        }
                //    }
                //}

                ////2、费用明细优惠金额和赠送金额(需要减掉单个项目的优惠金额和赠送金额)
                //foreach (FeeItemList f in feeDetails)
                //{
                //    //优惠金额
                //    feeTotEcoCost -= f.FT.RebateCost;
                //    //赠送金额
                //    feeTotDonateCost -= f.FT.DonateCost;
                //}
                #endregion
                #endregion 

                #region 获得发票序列,多张发票发票号不同

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "获得发票流水号失败!" + outpatientManager.Err;

                    return false;
                }
                //获得特殊显示类别
                /////GetSpDisplayValue(myCtrl, t);
                //第一个发票号
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region 插入发票明细表

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //总发票处理
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

                            //插入发票明细表 fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "插入发票明细出错!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region 协定处方
                ArrayList noSplitDrugList = new ArrayList();
                if (isSplitNostrum)
                {

                    if (SplitNostrumDetail(r, ref feeDetails, ref noSplitDrugList, ref errText) < 0)
                    {
                        return false;
                    }
                }

                #endregion

                #region 药品信息列表,生成处方号

                ArrayList drugLists = new ArrayList();

                //这里对分方有用，不要轻易改动
                r.User02 = "收费";

                //重新生成处方号,如果已有处方号,明细不重新赋值.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region 插入费用明细

                #region 作废旧流程
                //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                //优惠金额的累计值
                //decimal gatherEcoCost = 0;
                ////赠送金额的累计值
                //decimal gatherDonateCost = 0;
                //int feeIndex = 0;
                #endregion 
                foreach (FeeItemList f in feeDetails)
                {
                    //验证数据
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }
                    if (f.Item.SpecialFlag1 == "1"||f.Item.SpecialFlag1=="2") f.NoBackQty = 0;
                    //如果没有处方号,重新赋值
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "获得处方号出错!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq 判断是否已有发票号序号，没有则赋值

                    //{1A5CC61F-01F9-4dee-A6A8-580200C10EB4}
                    if (string.IsNullOrEmpty(f.InvoiceCombNO) || f.InvoiceCombNO == "NULL")
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }

                    #endregion
                    
                    #region 2007-8-28 liuq 判断是否已有发票号，没有初始化为12个0

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
                   
                    #region 作废旧流程
                    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //优惠金额，赠送金额
                    //feeIndex++;
                    //if (feeIndex == feeDetails.Count)
                    //{
                    //    //优惠金额
                    //    f.FT.RebateCost += feeTotEcoCost - gatherEcoCost;

                    //    //赠送金额
                    //    f.FT.DonateCost += feeTotDonateCost - gatherDonateCost;
                    //}
                    //else
                    //{
                    //    //优惠金额
                    //    f.FT.RebateCost += Math.Round(feeTotEcoCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);
                    //    gatherEcoCost += Math.Round(feeTotEcoCost * ((f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost) / (totCost != 0 ? totCost : 1)), 2);

                    //    //赠送金额
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
                            errText = "请选择医生";
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

                    //如果患者为团体体检，那么所有项目都插入终端审核。
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //如果项目流水号为空，说明没有经过划价流程，那么插入终端审核信息。
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "获得医嘱流水号出错!";
                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//其他患者如果项目为需要终端审核项目则插入终端审核信息。
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
                                            errText = "获得医嘱流水号出错!";
                                            return false;
                                        }

                                        Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);
                                        if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                        {
                                            errText = "处理终端申请确认表失败!" + ConfirmIntegrate.Err;
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //没有付值医嘱流水号,赋值新的医嘱流水号
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "获得医嘱流水号出错!";
                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//个人体检更新收费标记
                    {
                        iReturn = ExamiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "更新体检收费标记失败!" + ExamiIntegrate.Err;
                            return false;
                        }
                    }

                    //如果删除划价保存中的组合项目主项目信息,保留明细.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "删除组套失败!" + outpatientManager.Err;
                            return false;
                        }
                        //不知道谁修改的，偶尔删除组套费用失败...
                        //前面已经把组套医嘱的id放入费用的User03，此处再删一次  houwb
                        else if (iReturn == 0)
                        {
                            iReturn = outpatientManager.DeletePackageByMoOrder(f.User03);
                            if (iReturn == -1)
                            {
                                errText = "删除组套失败!" + outpatientManager.Err;
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
                        errText = "查询项目信息失败！";
                        return false;
                    }

                    if (chargeItemCount == 0)//说明不存在 //if(feeTemp == null)
                    {
                        if (f.FTSource != "0" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "可能已经被其他操作员删除,请刷新后再收费!";

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
                            errText = "插入费用明细失败!" + outpatientManager.Err;

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
                            errText = "更新费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region 回写医嘱信息

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn <= 0 && !f.Item.IsMaterial && f.Item.ItemType == EnumItemType.Drug)
                        {
                            errText = "没有更新到医嘱信息，请向医生确认是否已经删除该医嘱:" + f.Item.Name + ",或重新刷卡调出该患者收费信息." + orderOutpatientManager.Err;

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
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

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
                                //如果是复合项目
                                if (!string.IsNullOrEmpty(f.UndrugComb.ID))
                                {
                                    //取复合项目维护的明细数量
                                    FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo = undrugPackAgeMgr.GetUndrugComb(f.UndrugComb.ID, f.Item.ID);
                                    if (undrugCombo == null)
                                    {
                                        errText = "获取复合项目" + f.UndrugComb.ID + "的非药品项目：" + f.Item.ID + "失败，原因：" + itemManager.Err;
                                        return false;
                                    }

                                    if (order.Qty != f.Item.Qty / undrugCombo.Qty)
                                    {
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region 加入发药申请列表

                    //如果是药品,并且没有被确认过,而且不需要终端确认,那么加入发药申请列表.
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

                    #region 插入医技预约表

                    //需要医技预约,插入终端预约信息.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "插入医技预约信息出错!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }
                    #endregion
                }

                #endregion

                #region 集体体检更新收费标记

                if (r.ChkKind == "2")//集体体检
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = ExamiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "更新体检收费标记失败!" + ExamiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region 发药窗口信息

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
                //插入发药申请信息,返回发药窗口,显示在发票上
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
                    errText = "处理药品明细失败!" + PharmarcyManager.Err;

                    return false;
                }

                //'如果有药品,那么设置发票的显示发药窗口信息.
                if (drugLists.Count > 0)
                {
                    //{02F6E9D7-E311-49a4-8FE4-BF2AC88B889B}屏蔽掉小版本代码，采用核心版本的代码
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

                #region 插入发票主表

                //优惠金额
                decimal gatherEcoCost = 0;
                //赠送金额
                decimal gatherDonateCost = 0;
                int invoIndex = 0;

                decimal totRealCost = totNormalReal + totPackageReal;
                decimal balanceTotEcoCost = packageEcoPay;
                decimal balanceTotGiftCost = packageGiftPay + normalGiftPay;
                decimal balanceTotEcoCostForCount = packageEcoPay;
                decimal balanceTotGiftCostForCount = packageGiftPay + normalGiftPay;
                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
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

                    #region 作废旧方法
                    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //赠送账户金额，算作优惠金额
                    //try
                    //{
                    //    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //    invoIndex++;
                    //    if (invoIndex == invoices.Count)
                    //    {
                    //        //优惠金额
                    //        balance.FT.RebateCost += balanceTotEcoCost - gatherEcoCost;

                    //        //赠送金额
                    //        balance.FT.DonateCost += balanceTotDonateCost - gatherDonateCost;
                    //    }
                    //    else
                    //    {
                    //        //优惠金额
                    //        balance.FT.RebateCost += Math.Round(balanceTotEcoCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //        gatherEcoCost += Math.Round(balanceTotEcoCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);

                    //        //赠送金额
                    //        balance.FT.DonateCost += Math.Round(balanceTotDonateCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //        gatherDonateCost += Math.Round(balanceTotDonateCost * (balance.FT.OwnCost + balance.FT.PubCost + balance.FT.PayCost / (totCost != 0 ? totCost : 1)), 2);
                    //    }
                    //}
                    //catch (Exception ex) { }
                    #endregion

                    #region 分配发票赠送金额与优惠金额
                    try
                    {
                        //赠送和优惠都分配完了，不继续循环
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

                    //体检标志
                    string tempExamineFlag = null;
                    //获得体检标志 0 普通患者 1 个人体检 2 团体体检
                    //如果没有赋值,默认为普通患者
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

                    //=====去掉CanceledInvoiceNO=string.Empty 路志鹏================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//限额代码
                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    #region 不在此判断是否存在发票号，造成锁表
                    //if (invoiceType == "0")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("已经存在发票号为: " + balance.Invoice.ID +
                    //            " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "因发票号重复暂时取消本次结算!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    //else if (invoiceType == "1")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("已经存在票据号为: " + balance.PrintedInvoiceNO +
                    //            " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "因发票号重复暂时取消本次结算!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    #endregion
                    //插入发票主表fin_opb_invoice
                    //不让更改姓名
                    Register rold = registerManager.GetByClinic(r.ID);

                    //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    //如果是直接收费，此时挂号记录还没插入挂号表
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
                        errText = "插入结算表出错!" + outpatientManager.Err;

                        return false;
                    }
                }

                if (balanceTotEcoCost > 0 || balanceTotGiftCost > 0)
                {
                    MessageBox.Show("分配发票赠送金额与优惠金额出错！");
                    return false;
                }
                #endregion

                #region 发票号走号，最后发票走下一个号码

                if (!isTempInvoice)//临时发票号码不走下一个号码
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;

                    if (invoiceNo.Length >= 12 && invoiceNo.StartsWith("9"))
                    {
                        // 为临时发票，记帐患者有可能是临时发票
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
                            {//发票头暂时先保存00
                                errText = this.invoiceServiceManager.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion



                #region 插入支付方式信息

                int payModeSeq = 1;
                string payInvoiceNo = string.Empty;

                //会员支付+会员代付
                Dictionary<string, List<BalancePay>> dictAcc = new Dictionary<string, List<BalancePay>>();

                //套餐支付结算
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

                    //会员支付+会员代付处理
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

                    //套餐支付处理
                    if (p.UsualObject != null && (p.UsualObject as List<PackageDetail>) != null &&
                        (p.UsualObject as List<PackageDetail>).Count > 0)
                    {
                        //{DD31280F-7321-42BB-B150-4C63018ED85F} 代支付，备注写上
                        packDeteails = p.UsualObject as List<PackageDetail>;
                        List<PackageDetail> listDeteails = packDeteails.Where(t => t.CardNO != r.PID.CardNO).ToList();
                        if (listDeteails != null && listDeteails.Count > 0)
                        {
                            p.Memo = listDeteails[0].CardNO + "代" + r.PID.CardNO + "消费,套餐支付总额:" + listDeteails.Sum(t => t.Detail_Cost) + ",套餐实收:"+listDeteails.Sum(t=>t.Real_Cost) ;
                        }
                        
                    }

                    iReturn = outpatientManager.InsertBalancePay(p);
                    if (iReturn == -1)
                    {
                        errText = "插入支付方式表出错!" + outpatientManager.Err;

                        return false;
                    }

                    //功能已停用
                    //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                    //判断该支付方式是否计算积分
                    //if (obj.Name.Contains(p.PayType.ID.ToString()))
                    //{
                    //    if (accountPay.UpdateCoupon(r.PID.CardNO, p.FT.TotCost, payInvoiceNo) <= 0)
                    //    {
                    //        errText = "计算积分出错!" + accountPay.Err;
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
                //        errText = "计算现金流积分出错!" + errInfo;
                //        return false;
                //    }

                //}

                #region 账户扣费

                if (dictAcc != null && dictAcc.Count > 0)
                {
                    //结算患者
                    FS.HISFC.Models.RADT.PatientInfo selfPatient = accountManager.GetPatientInfoByCardNO(r.PID.CardNO);
                    if (selfPatient == null || string.IsNullOrEmpty(selfPatient.PID.CardNO))
                    {
                        errText = "查询患者基本信息失败!";
                        return false;
                    }

                    //会员患者
                    FS.HISFC.Models.RADT.PatientInfo empPatient = new FS.HISFC.Models.RADT.PatientInfo();

                    foreach (List<BalancePay> bpList in dictAcc.Values)
                    {
                        decimal baseCost = 0;                    //基本账户金额
                        decimal donateCost = 0;                  //赠送账户金额

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

                        string accountNo = bp.AccountNo;      //账户
                        string accountTypeCode = bp.AccountTypeCode;   //账户类型
                        List<AccountDetail> accLists = accountManager.GetAccountDetail(accountNo, accountTypeCode, "1");
                        if (accLists == null || accLists.Count <= 0)
                        {
                            errText = "查找账户失败!";
                            return false;
                        }
                        AccountDetail detailAcc = accLists[0];
                        if (Math.Abs(baseCost) > detailAcc.BaseVacancy)
                        {
                            errText = string.Format("会员账户中基本账户余额不足！\r\n需缴费：{0}元；基本账户余额：{1}元", Math.Abs(baseCost).ToString("F2"), detailAcc.BaseVacancy.ToString("F2"));
                            return false;
                        }
                        if (Math.Abs(donateCost) > detailAcc.DonateVacancy)
                        {
                            errText = string.Format("会员账户中赠送账户余额不足！\r\n需缴费：{0}元；基本账户余额：{1}元", Math.Abs(donateCost).ToString("F2"), detailAcc.DonateVacancy.ToString("F2"));
                            return false;
                        }

                        if (bp.IsEmpPay)
                        {
                            empPatient = accountManager.GetPatientInfoByCardNO(detailAcc.CardNO);
                            if (empPatient == null || string.IsNullOrEmpty(empPatient.PID.CardNO))
                            {
                                errText = string.Format("查找授权患者【{0}】基本信息失败!", detailAcc.CardNO);
                                return false;
                            }
                        }
                        else
                        {
                            empPatient = selfPatient;
                        }

                        //会员账户结算
                        int returnValue = accountPay.OutpatientPay(selfPatient, accountNo, accountTypeCode, baseCost, donateCost, payInvoiceNo, empPatient, PayWayTypes.C, 1);
                        if (returnValue < 0)
                        {
                            errText = "账户结算出错!" + accountPay.Err;
                            return false;
                        }

                    }
                }

                #endregion

                #region 套餐消费

                if (packDeteails != null && packDeteails.Count > 0)
                {
                    ArrayList alPack = new ArrayList(packDeteails);
                    
                    //if (this.NewCostPackageDetail(alPack, payInvoiceNo,r, ref errText) < 0)
                    //{
                    //    errText = "套餐结算失败！\r\n" + errText + this.Err;
                    //    return false;
                    //}
                    //会员患者{351D714B-0153-483e-B1AB-697C5A9A9BAD}
                    FS.HISFC.Models.RADT.PatientInfo p = accountManager.GetPatientInfoByCardNO(r.PID.CardNO);
                    if (this.NewCostPackageDetailByType(alPack, payInvoiceNo, p, "MZ", r.ID, ref errText) < 0) 
                    {
                        errText = "套餐结算失败！\r\n" + errText + this.Err;
                        return false;
                    }
                }

                #endregion

                #endregion

                #region 插入挂号记录、更新看诊标记

                string noRegRules = controlParamIntegrate.GetControlParam(Const.NO_REG_CARD_RULES, false, "9");
                
                //输入姓名患者收费,那么插入挂号信息,如果已经插入过,那么忽略.
                //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                if (r.PID.CardNO.Substring(0, 1) == noRegRules || r.User01 == "1")
                {
                    r.InputOper.OperTime = DateTime.Now;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;

                    #region 只接收费，调整四级初诊和操作时间{3C5D4918-96F0-4ba8-AC85-6DA86914465D}
                    r.HospitalFirstVisit = registerManager.IsFirstRegister("1", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.RootDeptFirstVisit = registerManager.IsFirstRegister("2", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.DoctFirstVist = registerManager.IsFirstRegister("4", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.IsFirst = registerManager.IsFirstRegister("3", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? false : true;
                    #endregion

                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                /*{067831BF-DDA5-4ac3-958A-4DD0BE5B085F}收费反映，收费处选择的收费类别会影响患者主表的合同单位，现决定能够修改患者信息主表的权限只有患者基本信息修改，所以这里去掉
                else
                {
                    if (registerManager.UpdatePatientInfoForNewClinicFee(r) <= 0)
                    {
                        errText = "更新挂号信息失败!" + registerManager.Err;
                        return false;
                    }

                    //if (registerManager.UpdateRegInfoForClinicFee(r) <= 0){//69C503A2-4C1C-44D4-82A3-174ABDAC34C1}关闭更新挂号表信息
                    //{
                    //    errText = "更新挂号信息失败!" + registerManager.Err;
                    //    return false;
                    //}
                }
                 * */

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "更新看诊标志出错！" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "更新看诊科室、医生出错！" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "更新分诊标志出错！" + managerIntegrate.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else//划价
            {
                #region 划价流程
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
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //处理划价保存信息.
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
        /// 门诊收费函数
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
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

            //获得收费时间
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //获得收费操作员
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //返回值
            int iReturn = 0;
            //定义处方号
            string recipeNO = string.Empty;

            //如果是收费，获得发票信息
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//收费
            {
                #region 收费流程
                //发票已经在预览界面分配完毕,直接插入就可以了.

                #region//获得发票序列,多张发票发票号不同,共享一个发票序列,通过发票序列号,可以查询一次收费的多张发票.

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "获得发票流水号失败!" + outpatientManager.Err;

                    return false;
                }
                //获得特殊显示类别
                /////GetSpDisplayValue(myCtrl, t);
                //第一个发票号
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //插入发票明细表

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //总发票处理
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

                            //插入发票明细表 fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "插入发票明细出错!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region//药品信息列表,生成处方号

                ArrayList drugLists = new ArrayList();

                //重新生成处方号,如果已有处方号,明细不重新赋值.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//插入费用明细

                foreach (FeeItemList f in feeDetails)
                {
                    //验证数据
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }

                    //如果没有处方号,重新赋值
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "获得处方号出错!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq 判断是否已有发票号序号，没有则赋值
                    if (string.IsNullOrEmpty(f.InvoiceCombNO))
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq 判断是否已有发票号，没有初始化为12个0
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
                            errText = "请选择医生";
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

                    //如果患者为团体体检，那么所有项目都插入终端审核。
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //如果项目流水号为空，说明没有经过划价流程，那么插入终端审核信息。
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "获得医嘱流水号出错!";
                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//其他患者如果项目为需要终端审核项目则插入终端审核信息。
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
                                    errText = "获得医嘱流水号出错!";

                                    return false;
                                }

                                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!" + ConfirmIntegrate.Err;

                                    return false;
                                }
                            }
                        }
                    }
                    //没有付值医嘱流水号,赋值新的医嘱流水号
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "获得医嘱流水号出错!";

                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//个人体检更新收费标记
                    {
                        iReturn = ExamiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "更新体检收费标记失败!" + ExamiIntegrate.Err;

                            return false;
                        }
                    }

                    //如果删除划价保存中的组合项目主项目信息,保留明细.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "删除组套失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    FeeItemList feeTemp = new FeeItemList();
                    feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    if (feeTemp == null)//说明不存在
                    {
                        if (f.FTSource != "0" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "可能已经被其他操作员删除,请刷新后再收费!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "插入费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "更新费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//回写医嘱信息

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn == -1)
                        {
                            errText = "更新医嘱信息出错!" + orderOutpatientManager.Err;

                            return false;
                        }
                    }

                    #endregion

                    //如果是药品,并且没有被确认过,而且不需要终端确认,那么加入发药申请列表.
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
                    //需要医技预约,插入终端预约信息.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "插入医技预约信息出错!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }

                }

                #endregion

                #region 集体体检更新收费标记

                if (r.ChkKind == "2")//集体体检
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = ExamiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "更新体检收费标记失败!" + ExamiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//发药窗口信息

                string drugSendInfo = null;
                //插入发药申请信息,返回发药窗口,显示在发票上
                iReturn = PharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                if (iReturn == -1)
                {
                    errText = "处理药品明细失败!" + PharmarcyManager.Err;

                    return false;
                }
                //如果有药品,那么设置发票的显示发药窗口信息.
                if (drugLists.Count > 0)
                {
                    foreach (Balance invoice in invoices)
                    {
                        invoice.DrugWindowsNO = drugSendInfo;
                    }
                }

                #region//插入发票主表

                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
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
                    //体检标志
                    string tempExamineFlag = null;
                    //获得体检标志 0 普通患者 1 个人体检 2 团体体检
                    //如果没有赋值,默认为普通患者
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

                    //=====去掉CanceledInvoiceNO=string.Empty 路志鹏================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//限额代码
                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    if (invoiceStytle == "0")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("已经存在发票号为: " + balance.Invoice.ID +
                                " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "因发票号重复暂时取消本次结算!";

                                return false;
                            }
                        }
                    }
                    else if (invoiceStytle == "1")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("已经存在票据号为: " + balance.PrintedInvoiceNO +
                                " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "因发票号重复暂时取消本次结算!";

                                return false;
                            }
                        }
                    }
                    //插入发票主表fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "插入结算表出错!" + outpatientManager.Err;

                        return false;
                    }
                }



                #region 发票号走号，最后发票走下一个号码

                if (!isTempInvoice)//临时发票号码不走下一个号码
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
                        {//发票头暂时先保存00
                            errText = this.invoiceServiceManager.Err;
                            return false;
                        }
                    }
                }

                #endregion


                #endregion

                #endregion

                #region 插入支付方式信息

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
                //        errText = "插入支付方式表出错!" + outpatientManager.Err;

                //        return false;
                //    }

                //    if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                //    {
                //        bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                //        if (!returnValue)
                //        {
                //            errText = "扣取门诊账户失败!" + "\n" + this.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                #region//如果不是直接收费患者和体检患者，更新看诊标志

                string noRegRules = controlParamIntegrate.GetControlParam(Const.NO_REG_CARD_RULES, false, "9");

                //输入姓名患者收费,那么插入挂号信息,如果已经插入过,那么忽略.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = feeTime;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;

                    #region 只接收费，调整四级初诊和操作时间{3C5D4918-96F0-4ba8-AC85-6DA86914465D}
                    r.HospitalFirstVisit = registerManager.IsFirstRegister("1", r.PID.CardNO, r.SeeDoct.Dept.ID, r.SeeDoct.ID, DateTime.MinValue) > 0 ? "0" : "1";
                    r.RootDeptFirstVisit = registerManager.IsFirstRegister("2",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?"0" : "1";
                    r.DoctFirstVist = registerManager.IsFirstRegister("4",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?"0" : "1";
                    r.IsFirst = registerManager.IsFirstRegister("3",r.PID.CardNO,r.SeeDoct.Dept.ID,r.SeeDoct.ID,DateTime.MinValue)>0?false : true;
                    #endregion

                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "更新看诊标志出错！" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "更新看诊科室、医生出错！" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "更新分诊标志出错！";
                                return false;
                            }
                        }
                    }
                }
                ////如果是医保患者,更新本地医保结算信息表 fin_ipr_siinmaininfo
                //if (r.Pact.PayKind.ID == "02")
                //{
                //    //设置已结算标志
                //    r.SIMainInfo.IsBalanced = true;
                //    // iReturn = interfaceManager.update(r);
                //    if (iReturn < 0)
                //    {
                //        errText = "更新医保患者结算信息出错!" + interfaceManager.Err;
                //        return false;
                //    }
                //}

                #endregion



                #endregion
            }
            else//划价
            {
                #region 划价流程

                #region 防止出错，在该地方赋值 划价保存费用来源
                foreach (FeeItemList f in feeDetails)
                {
                    f.FTSource = "0";//划价保存费用来源
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
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //处理划价保存信息.
                bool returnValue = this.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }

            //处理适应症{E4C0E5CF-D93F-48f9-A53C-9ADCCED97A7E}
            FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient iAdptIllnessOutPatient = null;
            iAdptIllnessOutPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient;
            if (iAdptIllnessOutPatient != null)
            {
                //保存适应症信息
                int returnValue = iAdptIllnessOutPatient.SaveOutPatientFeeDetail(r, ref feeDetails);
                if (returnValue < 0)
                {
                    return false;
                }

            }

            return true;
        }


        /// <summary>
        /// 插入或者更新划价信息
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeItemLists">费用信息</param>
        /// <param name="chargeTime">收费时间</param>
        /// <param name="errText">错误信息</param>
        /// <returns>true成功 false 失败</returns>
        public bool SetChargeInfo(Register r, ArrayList feeItemLists, DateTime chargeTime, ref string errText)
        {
            bool returnValue = false;
            int iReturn = 0;
            string recipeSeq = null;//收费序列

            //重新生成收费序列【gumzh-2014-10-31】
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
                    errText = "获得收费序列号出错!";

                    return false;
                }
            }

            #region 这里先全部删除

            foreach (FeeItemList f in feeItemLists)
            {
                if (!string.IsNullOrEmpty(f.RecipeNO))
                {
                    iReturn = outpatientManager.DeleteFeeItemListByMoOrder(f.Order.ID);
                    if (iReturn == -1)
                    {
                        errText = "删除费用明细失败!" + outpatientManager.Err;

                        return false;
                    }
                }
            }

            #endregion

            //重新生成处方号,如果已有处方号,明细不重新赋值.
            returnValue = this.SetRecipeNOOutpatient(r, feeItemLists, ref errText);
            if (!returnValue)
            {
                return false;
            }

            //houwb 处理相同组号的 收费序列号一致
            Hashtable hsRecipeSeqByCombNo = new Hashtable();

            //相同处方号 收费序列号一致
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
                //验证数据合法性
                if (!this.IsFeeItemListDataValid(f, ref errText))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(f.FTSource) || f.FTSource == "0")
                {
                    f.FTSource = "0";
                    //划价保存
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
                if (f.Order.ID == null || f.Order.ID == string.Empty)//没有付值医嘱流水号
                {
                    f.Order.ID = orderManager.GetNewOrderID();
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        errText = "获得医嘱流水号出错!";

                        return false;
                    }
                }

                //为避免同一组合、相同处方号不在一个收费序列
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

                #region 集体体检,在体检划价时已经处理,这里屏蔽
                //if (r.ChkKind == "2")//团体体检
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
                //        errText = "处理终端申请确认表失败!" + myConfirm.Err;

                //        return false;
                //    }

                //    if (f.Item.IsNeedBespeak)
                //    {
                //        ////iReturn = terminalManager.MedTechApply(f, this.trans);
                //        if (iReturn == -1)
                //        {
                //            errText = "插入医技预约信息出错!" + f.Name + terminalManager.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                if (iReturn == -1)
                {
                    if (outpatientManager.DBErrCode == 1)//主键重复，直接更新
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "更新费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        errText = "插入费用明细失败!" + outpatientManager.Err;

                        return false;
                    }
                }
            }

            return true;
        }


        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region 门诊账户使用的划价收费函数

        /// <summary>
        /// 账户终端收费函数
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">费用信息</param>
        /// <param name="errText">错误信息</param>
        /// <returns>true成功 false 失败</returns>
        public bool SaveFeeToAccount(Register r, string recipeNO, int sequenceNO, ref string errText)
        {

            FeeItemList f = outpatientManager.GetFeeItemList(recipeNO, sequenceNO);
            if (f == null)
            {
                errText = "查询费用信息失败！" + outpatientManager.Err;
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
                errText = "更新费用收费标记失败！" + outpatientManager.Err;
                return false;
            }

            if (f.FTSource == "1")
            {
                iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, feeOper);
                if (iReturn == -1)
                {
                    errText = "更新医嘱信息出错!" + orderOutpatientManager.Err;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 账户划价函数
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeItemLists">费用信息</param>
        /// <param name="chargeTime">划价时间</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool SetChargeInfoToAccount(Register r, ArrayList feeItemLists, DateTime chargeTime, ref string errText)
        {
            #region 删除申请表
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
                                errText = "删除发药申请信息细失败！" + ConfirmIntegrate.Err;
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
                                errText = "获得医嘱流水号出错!";

                                return false;
                            }
                            if (ConfirmIntegrate.DelTecApply(f.RecipeNO, f.SequenceNO.ToString()) < 0)
                            {
                                errText = "删除终端申请信息失败！" + ConfirmIntegrate.Err;
                                return false;
                            }
                            undrugList.Add(f);
                        }
                    }
                }
            }
            #endregion

            #region 删除药品调剂头表
            foreach (string recipeNO in dicRecipe.Keys)
            {
                if (PharmarcyManager.DeleteDrugStoRecipe(recipeNO, dicRecipe[recipeNO]) < 0)
                {
                    MessageBox.Show("删除调剂头表信息失败！" + PharmarcyManager.Err);
                    return false;
                }
            }
            #endregion

            #region 正常划价

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

            #region 插入药品申请表
            string drugSendInfo = null;
            //插入发药申请信息,返回发药窗口,显示在发票上
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
                    errText = "处理药品明细失败!" + PharmarcyManager.Err;

                    return false;
                }
            }
            #endregion

            #region 插入终端项目申请
            foreach (FeeItemList f in undrugList)
            {
                Terminal.Result result = ConfirmIntegrate.ServiceInsertTerminalApply(f, r);

                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                {
                    errText = "处理终端申请确认表失败!" + ConfirmIntegrate.Err;

                    return false;
                }
            }
            #endregion

            return true;
        }

        #endregion


        /// <summary>
        /// 发票打印方法
        /// </summary>
        /// <param name="invoicePrintDll">发票打印dll位置</param>
        /// <param name="rInfo">患者基本信息</param>
        /// <param name="invoices">发票集合</param>
        /// <param name="invoiceDetails">发票明细集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="alPayModes">支付方式集合</param>
        /// <param name="t">数据库事务</param>
        /// <param name="isPreView">是否预览</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int PrintInvoice(string invoicePrintDll, Register rInfo, ArrayList invoices, ArrayList invoiceDetails,
            ArrayList feeDetails, ArrayList alPayModes, bool isPreView, ref string errText)
        {

            int iReturn = 0;//返回值
            ArrayList alTempPayModes = new ArrayList();//临时支付方式

            if (alPayModes != null)
            {
                foreach (BalancePay p in alPayModes)
                {
                    alTempPayModes.Add(p);
                }
            }

            // 更改发票打印类获取方式；兼容原来方式
            // 2011-08-04
            bool blnNewPrintStyle = false;

            if (string.IsNullOrEmpty(invoicePrintDll))
            {
                blnNewPrintStyle = true;
                //errText = "没有维护发票打印方案!请维护";
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
                    #region 发票打印旧方式
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
                    #region 新方式

                    iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
                    if (iInvoicePrint == null)
                    {
                        errText = "请维护打印票据，查找打印票据失败！";
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
        /// 发票打印方法
        /// </summary>
        /// <param name="invoicePrintDll">发票打印dll位置</param>
        /// <param name="rInfo">患者基本信息</param>
        /// <param name="invoices">发票集合</param>
        /// <param name="invoiceDetails">发票明细集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="invoiceFeeDetails">发票费用明细信息（按发票分组后的费用明细，每个对象对应该发票下的费用明细）</param>
        /// <param name="alPayModes">支付方式集合</param>
        /// <param name="t">数据库事务</param>
        /// <param name="isPreView">是否预览</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int PrintInvoice(string invoicePrintDll, Register rInfo, ArrayList invoices, ArrayList invoiceDetails,
            ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList alPayModes, bool isPreView, ref string errText)
        {

            int iReturn = 0;//返回值
            //ArrayList alTempPayModes = new ArrayList();//临时支付方式

            //if (alPayModes != null)
            //{
            //    foreach (BalancePay p in alPayModes)
            //    {
            //        alTempPayModes.Add(p);
            //    }
            //}

            // 更改发票打印类获取方式；兼容原来方式
            // 2011-08-04
            bool blnNewPrintStyle = false;

            if (string.IsNullOrEmpty(invoicePrintDll))
            {
                blnNewPrintStyle = true;
                //errText = "没有维护发票打印方案!请维护";
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
                //循环发票明细项，根据发票号找到对应的发票明细  
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
                    #region 发票打印旧方式
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
                    #region 新方式

                    iInvoicePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)) as IInvoicePrint;
                    if (iInvoicePrint == null)
                    {
                        errText = "请维护打印票据，查找打印票据失败！";
                        return -1;
                    }

                    iInvoicePrint.SetTrans(this.trans);

                    iReturn = iInvoicePrint.SetPrintValue(rInfo, invoice, invoiceDetailsTemp, invoiceFeeDetailsTemp, alPayModes, isPreView);

                    if (iReturn == -1)
                    {
                        return 0;
                    }

                    alPrint.Add(iInvoicePrint);
                    //break; 考虑分发票情况，屏蔽

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
        /// 设置分发票后的支付方式
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
                        payMode += "现金: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.TotCost -= totCost;
                        payMode += "现金: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "PS" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "医保卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "医保卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
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
                        //payMode += "医保卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost,2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "CH" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "支票: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "支票: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "SB" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "社保卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.RealCost -= totCost;
                        payMode += "社保卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
                if (p.PayType.ID.ToString() == "YS" && p.FT.RealCost > 0)
                {
                    if (p.FT.RealCost <= totCost)
                    {
                        totCost -= p.FT.RealCost;
                        payMode += "院内账户: " + FS.FrameWork.Public.String.FormatNumberReturnString(p.FT.RealCost, 2);
                        p.FT.RealCost = 0;
                    }
                    else
                    {
                        p.FT.TotCost -= totCost;
                        payMode += "院内账户: " + FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 2);
                        break;
                    }
                }
            }

            if (cardCost > 0)
            {
                payMode += "银行卡: " + FS.FrameWork.Public.String.FormatNumberReturnString(cardCost, 2);
            }
        }

        /// <summary>
        /// 获得处方号
        /// </summary>
        /// <returns></returns>
        public string GetRecipeNO()
        {
            this.SetDB(outpatientManager);

            return outpatientManager.GetRecipeNO();
        }

        /// <summary>
        /// 通过医嘱项目流水号或者体检项目流水号，得到费用明细
        /// </summary>
        /// <param name="MOOrder">医嘱项目流水号或者体检项目流水号</param>
        /// <returns>null 错误 ArrayList Fee.OutPatient.FeeItemList实体集合</returns>
        public ArrayList QueryFeeDetailFromMOOrder(string MOOrder)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailFromMOOrder(MOOrder);
        }

        /// <summary>
        /// 根据医嘱或者体检项目流水号删除明细
        /// </summary>
        /// <param name="MOOrder">医嘱或者体检项目流水号</param>
        /// <returns>成功: >= 1 失败: -1 没有删除到数据返回 0</returns>
        public int DeleteFeeItemListByMoOrder(string MOOrder)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DeleteFeeItemListByMoOrder(MOOrder);
        }

        #region 查询发票组合号是否已经收费
        /// <summary>
        /// 根据发票组合号查询体检汇总信息是否已经收费　
        /// </summary>
        /// <param name="RecipeSeq">发票组合号</param>
        /// <returns>0 已收费， 1 未收费 ，-1 查询出错</returns>
        public int IsFeeItemListByRecipeNO(string RecipeSeq)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.IsFeeItemListByRecipeNO(RecipeSeq);
        }
        #endregion

        /// <summary>
        /// 根据病历号和时间段得到患者未收费明细
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="dtFrom">开始时间</param>
        /// <param name="dtTo">结束时间</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemLists(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemLists(cardNO, dtFrom, dtTo);
        }


        /// <summary>
        /// 根据病历号和时间段得到患者未收费明细
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="dtFrom">开始时间</param>
        /// <param name="dtTo">结束时间</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemListsZs(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemListsForZs(cardNO, dtFrom, dtTo);
        }

        /// <summary>
        /// 根据病历号和时间段得到患者未收费明细
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="dtFrom">开始时间</param>
        /// <param name="dtTo">结束时间</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryOutpatientFeeItemListsZsSubjob(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return outpatientManager.QueryFeeItemListsForZsSubjob(cardNO, dtFrom, dtTo);
        }

        #region 获得收费序列号

        /// <summary>
        /// 获得收费序列号
        /// </summary>
        /// <returns>成功:收费序列号 失败:null</returns>
        public string GetRecipeSequence()
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetRecipeSequence();
        }

        #endregion

        #endregion

        /// <summary>
        /// 获取合同单位列表
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPackList()
        {
            this.SetDB(pactManager);

            return pactManager.QueryPactUnitAll();
        }

        #endregion

        #region 门诊医生站相关add by sunm

        /// <summary>
        /// 插入门诊费用明细
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int InsertFeeItemList(FeeItemList feeItemList)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.InsertFeeItemList(feeItemList);
        }

        /// <summary>
        /// 更新门诊费用明细
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int UpdateFeeItemList(FeeItemList feeItemList)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.UpdateFeeItemList(feeItemList);
        }

        /// <summary>
        /// 根据处方号和处方内流水号删除费用明细
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
        /// 根据组合号和流水号删除费用明细
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
        /// 通过患者流水号和组合号得到费用明细
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
        /// 通过患者流水号和收费序号得到未收费的费用明细
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
        /// 通过患者流水号和收费序号得到费用明细
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeSeq"></param>
        /// <param name="feeFlag">ALL 全部 0划价 1收费 3预收费团体体检 4 药品预审核</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndRecipeSeq(string clinicCode, string recipeSeq, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(clinicCode, recipeSeq, feeFlag);
        }

        /// <summary>
        /// 通过患者流水号和看诊序号得到费用明细
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeSeq"></param>
        /// <param name="feeFlag">ALL 全部 0划价 1收费 3预收费团体体检 4 药品预审核</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNO(string clinicCode, string SeeNO, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndSeeNO(clinicCode, SeeNO, feeFlag);
        }

        /// <summary>
        /// 通过患者流水号和看诊序号得到费用明细
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="feeFlag">ALL 全部 0划价 1收费 3预收费团体体检 4 药品预审核</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNONotNull(string clinicCode, string feeFlag)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.QueryFeeDetailByClinicCodeAndSeeNONotNull(clinicCode, feeFlag);
        }

        #endregion

        #region 院内注射

        /// <summary>
        /// 获得院注信息根据用法
        /// </summary>
        /// <param name="usageCode"></param>
        /// <returns></returns>
        public ArrayList GetInjectInfoByUsage(string usageCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetInjectInfoByUsage(usageCode);
        }

        /// <summary>
        /// 删除用法项目信息
        /// </summary>
        /// <param name="Usage"></param>
        /// <returns></returns>
        public int DelInjectInfo(string Usage)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.DelInjectInfo(Usage);
        }

        /// <summary>
        /// 插如用法项目信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertInjectInfo(FS.HISFC.Models.Order.OrderSubtbl obj)
        {
            SetDB(outpatientManager);
            return outpatientManager.InsertInjectInfo(obj);
        }

        #endregion

        #region addby xuewj 2009-8-26 执行单管理 单项目维护 {0BB98097-E0BE-4e8c-A619-8B4BCA715001}

        /// <summary>
        /// 好像是执行单维护用的
        /// </summary>
        /// <param name="nruseID">护士站编码</param>
        /// <param name="sysClass">系统类别</param>
        /// <param name="validState">有效状态</param>
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
        /// 根据处方号和处方内流水号查询退费申请，用于医生站判断药房是否已经做过退费申请了。
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

        #region 枚举

        /// <summary>
        /// 收费函数操作类型
        /// </summary>
        private enum ChargeTypes
        {
            /// <summary>
            /// 划价
            /// </summary>
            Charge = 0,

            /// <summary>
            /// 收费
            /// </summary>
            Fee = 1,

            /// <summary>
            /// 划价记录转收费
            /// </summary>
            ChargeToFee = 2,

        }

        #endregion

        #region 门诊帐户

        /// <summary>
        /// 根据处方号和处方内流水号更新已扣账户标志
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequenceNO">处方内流水号</param>
        /// <param name="isAccounted">true 已经扣取账户 false 没有扣取账户</param>
        /// <returns>成功 1 失败 -1 不符合更新条件 0</returns>
        public int UpdateAccountByRecipeNO(string recipeNO, int sequenceNO, bool isAccounted)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateAccountFlag(recipeNO, sequenceNO, isAccounted);
        }

        /// <summary>
        /// 根据医嘱流水号和项目编码更新已扣账户标志
        /// </summary>
        /// <param name="itemCode">项目编码</param>
        /// <param name="moOrder">医嘱流水号</param>
        /// <param name="isAccounted">true 已经扣取账户 false 没有扣取账户</param>
        /// <returns>成功 1 失败 -1 不符合更新条件 0</returns>
        public int UpdateAccountByMoOrderAndItemCode(string itemCode, string moOrder, bool isAccounted)
        {
            this.SetDB(outpatientManager);

            return outpatientManager.UpdateAccountFlag(itemCode, moOrder, isAccounted);
        }


        /// <summary>
        /// 帐户支付
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="money">金额</param>
        /// <param name="reMark">标识</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns> 1 成功 0取消收费 -1失败</returns>
        public int AccountPay(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string deptCode, string invoiceType)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.AccountPayManager(patient, money, reMark, invoiceType, deptCode, 0);
            if (!bl) return -1;
            this.Err = accountManager.Err;
            return 1;
        }

        /// <summary>
        /// 退费入户
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="money">金额</param>
        /// <param name="reMark">标识</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns>1成功 -1失败</returns>
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
        /// 得到帐户余额
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="vacancy">余额</param>
        /// <returns>0:帐户停用或帐户不存在 -1查询失败 1成功</returns>
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
        /// 验证帐户密码
        /// </summary>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns>true 成功　false失败</returns>
        public bool CheckAccountPassWord(HISFC.Models.RADT.Patient patient)
        {
            // 预交金扣费时，是否需要验证密码 0 = 不需要；1 = 需要；
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
        /// 通过物理卡号查找门诊卡号
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <param name="markType">卡类型</param>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns>bool true 成功　false 失败</returns>
        public bool GetCardNoByMarkNo(string markNo, ref string cardNo)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.GetCardNoByMarkNo(markNo, ref cardNo);
            this.Err = accountManager.Err;
            return bl;
        }

        /// <summary>
        /// 通过物理卡号查找门诊卡号
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <param name="markType">卡类型</param>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns>bool true 成功　false 失败</returns>
        public bool GetCardNoByMarkNo(string markNo, NeuObject markType, ref string cardNo)
        {
            this.SetDB(accountManager);
            bool bl = accountManager.GetCardNoByMarkNo(markNo, markType, ref cardNo);
            this.Err = accountManager.Err;
            return bl;
        }

        /// <summary>
        /// 根据门诊卡号查找密码
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns>用户密码</returns>
        public string GetPassWordByCardNO(string cardNO)
        {
            this.SetDB(accountManager);
            return accountManager.GetPassWordByCardNO(cardNO);
        }

        /// <summary>
        /// 根据门诊卡号查找患者信息
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string cardNO)
        {
            this.SetDB(accountManager);
            return accountManager.GetPatientInfoByCardNO(cardNO);
        }


        /// <summary>
        /// 根据卡规则读出卡号
        /// </summary>
        /// <param name="markNo">输入卡号</param>
        /// <param name="accountCard">根据规则读出的卡信息</param>
        /// <returns>1成功(已经发放) 0卡还为发放 -1失败</returns>
        public int ValidMarkNO(string markNo, ref HISFC.Models.Account.AccountCard accountCard)
        {
            this.SetDB(accountManager);
            return accountManager.GetCardByRule(markNo, ref accountCard);
        }
        #endregion

        #region 发票跳号{BF01254E-3C73-43d4-A644-4B258438294E}
        /// <summary>
        /// 插入发票调号表
        /// </summary>
        /// <param name="invoiceJumpRecord"></param>
        /// <returns></returns>
        public int InsertInvoiceJumpRecord(FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceJumpRecord)
        {

            //{BF01254E-3C73-43d4-A644-4B258438294E}
            this.SetDB(this.invoiceJumpRecordMgr);
            this.SetDB(invoiceServiceManager);
            //去最大序号
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

            //更新使用号
            returnValue = this.invoiceServiceManager.UpdateUsedNO(invoiceJumpRecord.NewUsedNO, invoiceJumpRecord.Invoice.AcceptOper.ID, invoiceJumpRecord.Invoice.Type.ID);
            if (returnValue < 0)
            {
                this.Err = this.invoiceServiceManager.Err;
            }

            return 1;

        }
        #endregion

        #region IInterfaceContainer 成员

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
        /// 根据传入的项目函数内自动赋值，
        /// </summary>
        /// <param name="pInfo">患者实体</param>
        /// <param name="item">项目信息，收费数量要包含在项目实体item.qty中</param>
        /// <param name="execDept">执行科室代码</param>
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
            ////在院科室
            //((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////护士站
            //((FS.HISFC.Models.RADT                                                                               .PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            ////执行科室
            //ItemList.ExecOper.Dept.ID = execDept;
            ////扣库科室
            //ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////开方科室
            //ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            ////开方医生
            //ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //医生
            ////根据传入的实体处理价格
            //decimal price = 0;
            //decimal orgPrice = 0;

            //if(this.GetPriceForInpatient(pInfo.Pact.ID,item,ref price,ref orgPrice)==-1)          
            //{
            //    this.Err = "取项目:" + item.Name + "的r价格出错!" + pactUnitManager.Err;
            //    return -1;
            //}
            //item.Price = price;

            //// 原始总费用（本来应收费用，不考虑合同单位因素）
            //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //item.DefPrice = orgPrice;

            ////药品默认按最小单位收费,显示价格也为最小单位价格,存入数据库的为包装单位价格
            ////if (item.IsPharmacy)//药品
            //if (item.ItemType == EnumItemType.Drug)//药品
            //{
            //    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);

            //    // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //    orgPrice = FS.FrameWork.Public.String.FormatNumber(item.DefPrice / item.PackQty, 4);
            //}

            ///* 外部已经赋值部分：价格、数量、单位、是否药品
            // * ItemList.Item.Price = 0;ItemList.Item.Qty;  
            // * ItemList.Item.PriceUnit = "次"; 
            // * ItemList.Item.IsPharmacy = false;
            // */

            //ItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * price, 2);

            //// 原始总费用（本来应收费用，不考虑合同单位因素）
            //// {54B0C254-3897-4241-B3BD-17B19E204C8C}
            //ItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ItemList.Item.Qty * orgPrice, 2);

            ////ItemList.FT.OwnCost = ItemList.FT.TotCost;

            //ItemList.PayType = PayTypes.Balanced;
            //ItemList.IsBaby = false;
            //ItemList.BalanceNO = 0;
            //ItemList.BalanceState = "0";
            ////可退数量
            //ItemList.NoBackQty = item.Qty;

            ////操作员
            //ItemList.FeeOper.ID = operCode;
            //ItemList.ChargeOper.ID = operCode;
            //ItemList.ChargeOper.OperTime = dtNow;
            //ItemList.FeeOper.OperTime = dtNow;

            //#region {3C6A1DD7-7522-418b-89A5-4B973ED320C3}
            //ItemList.FT.OwnCost = ItemList.FT.TotCost;
            //ItemList.TransType = TransTypes.Positive;
            //#endregion

            ////调用收费函数
            //return this.FeeItem(pInfo, ItemList);
            //200自动收费（默认为系统补收）
            return this.FeeAutoItem(pInfo, item, "200", execDept, pactManager.Operator.ID);
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region 账户新增
        /// <summary>
        /// 根据处方号执行科室查找药品
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="deptCode">执行科室</param>
        /// <returns></returns>
        public int GetDrugUnFeeCount(string recipeNO, string deptCode)
        {
            this.SetDB(outpatientManager);
            return outpatientManager.GetDrugUnFeeCount(recipeNO, deptCode);
        }

        /// <summary>
        /// 作废账户支付的发票，用于门诊医生扣除挂号费的退费
        /// 此处扣除账户的挂号费和诊金
        /// </summary>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="invoiceSeq">发票序号</param>
        /// <returns>1 作废成功返还账户金额，0 非账户支付，不能作废 -1 错误</returns>
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

        #region 患者证件

        /// <summary>
        /// 更新患者信息证件信息
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
        #region 日结后收费判断

        /// <summary>
        /// 日结后收费判断设置
        /// </summary>
        /// <param name="feeOperCode">收款员编码</param>
        /// <param name="isInpatient">是否住院</param>
        /// <param name="errTxt">错误信息</param>
        /// <returns></returns>
        public bool AfterDayBalanceCanFee(string feeOperCode, bool isInpatient, ref string errTxt)
        {
            string canFeeType = controlParamIntegrate.GetControlParam<string>("100035", true, "0");
            //不判断
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
                    //日结后不许继续收费
                    if (canFeeType == "1")
                    {
                        errTxt = "日结后不可以再收费!";
                        return false;
                    }
                    //日结后只有财务授权后才可收费
                    if (canFeeType == "2")
                    {
                        //是否授权
                        if (empowerFeeManager.QueryIsEmpower(feeOperCode))
                        {
                            return true;
                        }
                        else
                        {
                            errTxt = "日结后没有经过授权不许收费！";
                            return false;
                        }
                    }

                }
            }

            return true;
        }


        #endregion

        #region 发票号规则 {95BEABF4-8309-4d5d-9523-52288F9154BB}
        /// <summary>
        /// 已不用，改用参数
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        private bool isTempInvoice = false;

        /// <summary>
        /// 是否取临时发票号
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
        /// 获得发票号
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="invoiceNO">发票电脑流水号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 失败 1 成功!</returns>
        public int GetInvoiceNO(string invoiceType, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            FS.HISFC.Models.Base.Employee oper = this.inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            return GetInvoiceNO(oper, invoiceType, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// 获得发票号
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        /// <param name="oper">操作员基本信息</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="invoiceNO">发票电脑流水号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 失败 1 成功!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceType, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            return GetInvoiceNO(oper, invoiceType, false, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// 获得发票号
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        /// <param name="oper">操作员基本信息</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="isTempInvoice">是否取临时发票号</param>
        /// <param name="invoiceNO">发票电脑流水号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 失败 1 成功!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceType, bool isTempInvoice, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            /*{95BEABF4-8309-4d5d-9523-52288F9154BB}-By Maokb -20101206
          * 重新处理发票号规则，包括门诊住院统一一种方式
          * 1、发票流水号是系列号，唯一标示一张发票，不能重复（原程序可以重复）
          * 2、发票领用处理的是实际发票号，输入实际发票号必须在发票领用范围内。
          * 3、发票处理字头（实际发票号还有英文字头）
          * 4、门诊住院挂号预交等走同一套取发票程序。
          * 5、增加取临时发票号处理（门诊预交金自助结算）
          * 6、MZINVOICE 常数 ID为员工编号，Name为正式发票号，Memo为电脑发票号（发票流水号）
          * */

            //如果是取临时发票号
            if (isTempInvoice)
            {
                invoiceNO = this.GetNewSysInvoice(invoiceType);
                invoiceNO = invoiceNO.PadLeft(12, '0');
                realInvoiceNO = invoiceNO;
                return 1;
            }

            #region 取正式发票号和实际发票号

            //取发票号规则
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            //系统流水号设计规则;          
            //"1"发票号走 YYMMDDXXABCD的方式，每个工号每天一个从0开始的流水号；实际发票号采用领用的方式。         
            //"2"发票号走实际发票号：这种情况下发票号有可能重号，不建议；实际发票号采用领用的方式。      

            //"3"发票号走 YYMMDDXXABCD的方式，每个工号每天一个从0开始的流水号；实际发票号不采用领用的方式，界面输入。      
            //"4"发票号走实际发票号：这种情况下发票号有可能重号，不建议；实际发票号不采用领用的方式，界面输入。



            //如果是预先分配发票形式
            bool isDistributeRealInvoice = (getInvoiceType == "1" || getInvoiceType == "2") ? true : false;
            NeuObject objInvoice = new NeuObject();

            #region 获取实际发票号

            if (isDistributeRealInvoice)
            {
                //从分配发票中获取实际发票号码
                realInvoiceNO = this.GetNextInvoiceNO(invoiceType, oper);
                if (string.IsNullOrEmpty(realInvoiceNO))
                {
                    errText = this.Err;
                    return -1;
                }
            }
            else
            {
                //从常数表中获取实际发票号码
                objInvoice = managerIntegrate.GetConstansObj("INVOICE-" + invoiceType, oper.ID);

                //没有维护发票起始号
                if (objInvoice == null || string.IsNullOrEmpty(objInvoice.ID.Trim()))
                {

                    if (FS.FrameWork.Management.PublicTrans.Trans == null)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    }
                    managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    con.ID = oper.ID;
                    con.Name = "1";//默认从1开始
                    con.Memo = "1";//默认从1开始
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID;
                    con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                    int iReturn = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errText = "插入操作员初试发票失败!" + managerIntegrate.Err;

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

            #region 获取系统流水号
            switch (getInvoiceType)
            {

                case "1":
                case "3"://发票号走 YYMMDDXXABCD的方式，每个工号每天一个从0开始的流水号
                    #region YYMMDDXXABCD

                    objInvoice = managerIntegrate.GetConstansObj("INVOICE-" + invoiceType, oper.ID);
                    if (objInvoice == null)
                    {
                        errText = "获得发票信息出错!" + managerIntegrate.Err;

                        return -1;
                    }

                    // 读取收费员维护自写义码，如果没有就自动生成一个
                    #region 发票自定义码

                    string tmpOperCode = string.Empty;
                    NeuObject objCashier = managerIntegrate.GetConstansObj("InvoiceUserCode", oper.ID);
                    if (objCashier != null && !string.IsNullOrEmpty(objCashier.Name))
                    {
                        tmpOperCode = objCashier.Name;
                    }
                    else//自动生成一个
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
                        //插入数据
                        if (managerIntegrate.InsertConstant("InvoiceUserCode", con) < 0)
                        {
                            if (this.trans == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            errText = "插入收费员发票自定义码失败，" + managerIntegrate.Err;
                            return -1;
                        }

                        //最后判断是否其他收费员存在此号码
                        if (constManager.IsExistName("InvoiceUserCode", oper.ID, tmpOperCode))
                        {
                            if (this.trans == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            errText = "插入收费员发票自定义码失败，已存在此号码，请退出界面！";
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

                    //没有维护发票起始号
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
                        con.Name = realInvoiceNO;//默认从1开始
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
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
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
                            invoiceNO = now.ToString("yyMMdd") + tmpOperCode + "0001";//不要报错了，直接变成今天的流水号
                        }

                    }

                    #endregion
                    break;
                case "2"://普通模式  
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
        /// 获得发票号
        /// </summary>
        /// <param name="oper">人员基本信息</param>
        /// <param name="ctrl">控制参数类</param>
        /// <param name="invoiceNO">发票电脑号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <param name="t">数据库事务</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 失败 1 成功!</returns>
        public int GetInvoiceNO(FS.HISFC.Models.Base.Employee oper, ref string invoiceNO, ref string realInvoiceNO, ref string errText, FS.FrameWork.Management.Transaction trans)
        {
            string invoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");

            NeuObject objInvoice = new NeuObject();

            switch (invoiceType)
            {
                case "2"://普通模式

                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);

                    //没有维护发票起始号
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
                        con.Name = "1";//默认从1开始
                        con.Memo = "1";//默认从1开始
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;

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
                            errText = "获得发票失败!" + this.Err;

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
                            errText = "获得发票失败!" + this.Err;

                            return -1;
                        }

                        invoiceNO = invoiceNOTemp;
                        realInvoiceNO = objInvoice.Name;
                    }

                    break;

                case "0": //广医模式
                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);

                    //没有维护发票起始号
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
                        con.Name = "1";//默认从1开始
                        con.Memo = "1";//默认从1开始
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();

                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;

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
                case "1": //中山医模式!

                    objInvoice = managerIntegrate.GetConstansObj("MZINVOICE", oper.ID);
                    if (objInvoice == null)
                    {
                        errText = "获得发票信息出错!" + managerIntegrate.Err;

                        return -1;
                    }

                    Employee empl = managerIntegrate.GetEmployeeInfo(oper.ID);
                    if (empl == null)
                    {
                        errText = "获得操作员基本信息出错!" + managerIntegrate.Err;

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

                    //没有维护发票起始号
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
                        con.Name = "1";//默认从1开始
                        con.IsValid = true;
                        con.OperEnvironment.ID = oper.ID;
                        con.OperEnvironment.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                        con.Memo = con.OperEnvironment.OperTime.Year.ToString().Substring(2, 2) + con.OperEnvironment.OperTime.Month.ToString().PadLeft(2, '0') +
                            con.OperEnvironment.OperTime.Day.ToString().PadLeft(2, '0') + tmpOperCode + "0001";
                        int iReturn = managerIntegrate.InsertConstant("MZINVOICE", con);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
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
                            errText = "发票转换日期出错!" + ex.Message;
                            return -1;
                        }

                        realInvoiceNO = objInvoice.Name;
                    }

                    break;
            }

            return 1;
        }


        #region 发票号走号
        /// <summary>
        /// 发票号走号
        /// 
        /// {F6C3D16B-8F52-4449-814C-5262F90C583B}
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="invoiceCount"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int UseInvoiceNO(string invoiceType, int invoiceCount, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            //取发票号规则
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            return UseInvoiceNO(oper, getInvoiceType, invoiceType, invoiceCount, ref invoiceNO, ref realInvoiceNO, ref errText);
        }
        /// <summary>
        /// 发票号走号
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceStytle">取发票方式</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
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
        /// 发票号走号
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="oper">收费员</param>
        /// <param name="invoiceStytle">取发票方式</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
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
                    //实际发票号码
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
                        errText = "实际发票号有误!";
                        return -1;
                    }
                    //发票流水号
                    //更新发票流水号    
                    int len = invoiceNO.Length;
                    //获得发票除了后四位的字符串,代表发票的日期和收款员,格式为yymmddxx(年,月,日,操作员2位工号)
                    string orgInvoice = invoiceNO.Substring(0, len - 4);
                    //获得后四位发票序列号
                    string addInvoice = invoiceNO.Substring(len - 4, 4);

                    //获得下一张发票号
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
                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    break;
                case "2":
                    //实际发票号
                    for (int i = 0; i < invoiceCount; i++)
                    {
                        lastRealInvoice = this.GetNewInvoiceNO(invoiceType, oper);
                    }
                    con.ID = oper.ID;
                    //更新发票流水号                    
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);//(NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
                case "3":
                case "4":
                default:
                    //实际发票号
                    //更新发票流水号   
                    con.ID = oper.ID;
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);// (NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
            }

            return 1;
        }

        #endregion

        #region 取实际发票号--  结算时用，自动发票号+1  {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// <summary>
        /// 取实际发票号--  结算时用，自动发票号+1
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <returns></returns>
        public string GetNewInvoiceNO(string invoiceType)
        {
            FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            return GetNewInvoiceNO(invoiceType, oper);
        }
        /// <summary>
        /// 取实际发票号--  结算时用，自动发票号+1
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <returns></returns>
        public string GetNewInvoiceNO(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        {
            int leftQty = 0;//发票剩余数目
            int alarmQty = 0;//发票预警数目
            string finGroupID = string.Empty;//发票组代码
            string newInvoiceNO = string.Empty;//获得的发票号

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
        /// 取实际发票号--界面显示用，显示当前下一个发票号
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <returns></returns>
        public string GetNextInvoiceNO(string invoiceType)
        {
            FS.HISFC.Models.Base.Employee oper = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            return GetNextInvoiceNO(invoiceType, oper);
        }
        /// <summary>
        /// 取实际发票号--界面显示用，显示当前下一个发票号，只获取新的发票号码，不更新发票领用
        /// {77300F2F-DF25-40e5-ADDF-53EB16F22C88}
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string GetNextInvoiceNO(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        {
            int leftQty = 0;//发票剩余数目
            int alarmQty = 0;//发票预警数目
            string finGroupID = string.Empty;//发票组代码
            string newInvoiceNO = string.Empty;//获得的发票号

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
                System.Windows.Forms.MessageBox.Show(Language.Msg("剩余发票") + leftQty.ToString() + Language.Msg("张,请补领发票!"));
            }

            return newInvoiceNO;
        }

        #region 不用
        ///// <summary>
        ///// 获得新的发票号 -只获取新的发票号码，不更新发票领用
        ///// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        ///// </summary>
        ///// <param name="invoiceType"></param>
        ///// <param name="oper"></param>
        ///// <returns></returns>
        //public string GetNextInvoiceNONotUpdate(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        //{
        //    int leftQty = 0;//发票剩余数目
        //    int alarmQty = 0;//发票预警数目
        //    string finGroupID = string.Empty;//发票组代码
        //    string newInvoiceNO = string.Empty;//获得的发票号

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
        //        System.Windows.Forms.MessageBox.Show(Language.Msg("剩余发票") + leftQty.ToString() + Language.Msg("张,请补领发票!"));
        //    }

        //    return newInvoiceNO;
        //}
        #endregion

        /// <summary>
        /// 更新实际发票号
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="realInvoiceNo">下一发票号</param>
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
        /// 更新实际发票号
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="strEmpCode"></param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="invoiceNO">下一电脑号</param>
        /// <param name="realInvoiceNo">下一发票号</param>
        /// <returns></returns>
        public int UpdateNextInvoiceNO(string strEmpCode, string invoiceType, string invoiceNO, string realInvoiceNo)
        {
            //取发票号规则
            string getInvoiceType = controlParamIntegrate.GetControlParam<string>(Const.GET_INVOICE_NO_TYPE, false, "0");
            //如果实际号和电脑号一致的

            int iRes = invoiceServiceManager.UpdateRealInvoiceNo(strEmpCode, invoiceType, realInvoiceNo);
            if (iRes <= 0)
            {
                this.Err = invoiceServiceManager.Err;
                //{2FB3E463-C00D-4ff4-974A-5D7AA0DB9CA0}
                return iRes;
            }

            switch (getInvoiceType)
            {
                //实际号和电脑号一直的情况下
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
        /// 获取电脑发票号最近的更新时间（常数类型）
        /// </summary>
        /// <param name="emplCode"></param>
        /// <param name="invoiceType"></param>
        /// <param name="recentUpdate">最近更新时间</param>
        /// <returns></returns>
        public int GetRecentUpdateInvoiceTime(string emplCode, string invoiceType, ref DateTime recentUpdate)
        {
            this.SetDB(this.invoiceServiceManager);
            return this.invoiceServiceManager.GetRecentUpdateInvoiceTime(emplCode, invoiceType, ref recentUpdate);
        }

        /// <summary>
        /// 更新电脑发票号 常数类型(调用前判断发票是否已使用)
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
        /// 获取新的发票系列号，流水号+1
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <returns></returns>
        public string GetNewSysInvoice(string invoiceType)
        {
            //根据发票类型获取新流水号，同时流水号加1
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
        /// 获得下一张发票号
        /// </summary>
        /// <param name="invoiceType">获得发票号方式</param>
        /// <param name="invoiceNO">当前电脑发票号</param>
        /// <param name="realInvoiceNO">当前实际发票号</param>
        /// <param name="nextInvoiceNO">下一张电脑发票号</param>
        /// <param name="nextRealInvoiceNO">下一张实际发票号</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 错误 1 正确</returns>
        public int GetNextInvoiceNO(string invoiceType, string invoiceNO, string realInvoiceNO, ref string nextInvoiceNO, ref string nextRealInvoiceNO, ref string errText)
        {
            return GetNextInvoiceNO(invoiceType, invoiceNO, realInvoiceNO, ref nextInvoiceNO, ref nextRealInvoiceNO, 1, ref errText);
        }

        /// <summary>
        /// 获得下N张发票号
        /// </summary>
        /// <param name="invoiceType">获得发票号方式</param>
        /// <param name="invoiceNO">当前电脑发票号</param>
        /// <param name="realInvoiceNO">当前实际发票号</param>
        /// <param name="nextInvoiceNO">下一张电脑发票号</param>
        /// <param name="nextRealInvoiceNO">下一张实际发票号</param>
        /// <param name="count">下几张发票</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 错误 1 正确</returns>
        public int GetNextInvoiceNO(string invoiceType, string invoiceNO, string realInvoiceNO, ref string nextInvoiceNO, ref string nextRealInvoiceNO, int count, ref string errText)
        {
            switch (invoiceType)
            {
                case "2"://普通模式

                    string invoiceNOTemp = string.Empty;

                    for (int i = 0; i < count; i++)
                    {
                        //invoiceNOTemp = this.GetNewInvoiceNO(FS.HISFC.Models.Fee.EnumInvoiceType.C);
                        invoiceNOTemp = this.GetNewInvoiceNO("C");
                        if (invoiceNOTemp == null || invoiceNOTemp == string.Empty)
                        {
                            errText = "获得发票失败!";

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

                case "0"://广医方式
                    //广医方式的发票号,为纯数字,所以直接增加1即可
                    nextInvoiceNO = ((NConvert.ToInt32(invoiceNO) + count).ToString()).PadLeft(12, '0');
                    //广医方式的实际发票号,根电脑号一样,同步增加
                    nextRealInvoiceNO = NConvert.ToInt32(nextInvoiceNO).ToString();

                    break;
                case "1"://中山方式
                    //因为中山方式的发票最后四位决定发票的序列号,所以一定要大于4位,并且最后四位为数字才是合法发票
                    if (invoiceNO.Length < 4)
                    {
                        errText = "发票号长度不符合!";

                        return -1;
                    }
                    //获得中山发票的长度
                    int len = invoiceNO.Length;
                    //获得发票除了后四位的字符串,代表发票的日期和收款员,格式为yymmddxx(年,月,日,操作员2位工号)
                    string orgInvoice = invoiceNO.Substring(0, len - 4);
                    //获得后四位发票序列号
                    string addInvoice = invoiceNO.Substring(len - 4, 4);

                    //获得下一张发票号
                    nextInvoiceNO = orgInvoice + (NConvert.ToInt32(addInvoice) + count).ToString().PadLeft(4, '0');
                    //实际发票号为字头+数字,数字部分直接增加1即可
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
        /// 当选择系统发票时候,重打调用,只更新发票打印号
        /// </summary>
        /// <param name="invoiceNO">当前发票号</param>
        /// <param name="realInvoiceNO">当前发票打印号</param>
        /// <param name="errText">错误编码</param>
        /// <returns>成功1  失败 -1</returns>
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
                errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// 获得发票号
        /// </summary>
        /// <param name="invoiceNO">发票电脑号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <param name="errText">错误信息</param>
        /// <returns>-1 失败 1 成功!</returns>
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
                errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                return -1;
            }

            return returnValue;
        }

        /// <summary>
        /// 保存发票扩展表
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
                invoiceExtend.RealInvoiceNo = realInvoiceNO.Substring(realInvoiceNO.Length - 8);//保存后8位
            }
            invoiceExtend.InvvoiceHead = invoiceHead;
            invoiceExtend.InvoiceState = "1";//有效
            int i = this.invoiceServiceManager.InsertInvoiceExtend(invoiceExtend);
            if (i <= 0)
            {
                this.Err = this.invoiceServiceManager.Err;
            }
            return i;
        }


        ///// <summary>
        ///// 发票号切换提示
        ///// </summary>
        ///// <param name="operCode">操作员</param>
        ///// <param name="invoiceType">发票类型</param>
        ///// <param name="endInvoiceNO">本次收费最大发票号</param>
        ///// <param name="invoiceCount">发票张数</param>
        ///// <param name="errText">错误信息</param>
        ///// <returns>-1失败 0提示 1成功</returns>
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
        //            errText = "该操作员不存在发票，请领用发票！";
        //            return -1;
        //        }
        //    }
        //    long invoiceNO = long.Parse(resultValue);
        //    invoiceNO += invoiceCount;
        //    if (invoiceNO == long.Parse(endInvoiceNO))
        //    {
        //        return 1;
        //    }
        //    MessageBox.Show("发票号段切换，请换纸！");
        //    return 0;

        //}
        #endregion

        #region 计算未扣费用
        /// <summary>
        /// 计算未扣费用,按待遇计算
        /// 
        /// 包含事务
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
        /// 计算处方费用，按医保合同单位处理
        /// 医生站调用
        /// 
        /// 返回 小于等于 0 计算失败，  等于 1 计算成功， 等于 2 计算成功且报销超标
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="arlFeeItemList">费用信息</param>
        /// <param name="blnNeedAddup">是否需要累计</param>
        /// <param name="arlMoneyInfo">
        /// arlMoneyInfo[0] = 累计总金额
        /// arlMoneyInfo[1] = 累计报销金额
        /// arlMoneyInfo[2] = 累计自费金额
        /// arlMoneyInfo[3] = 累计医院减免金额
        /// arlMoneyInfo[4] = 总金额
        /// arlMoneyInfo[5] = 医保报销金额
        /// arlMoneyInfo[6] = 自费金额
        /// arlMoneyInfo[7] = 医院减免金额
        /// </param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public int CalculatOrderFee(FS.HISFC.Models.Registration.Register regInfo, ArrayList arlFeeItemList, bool blnNeedAddup, ref decimal[] arlMoneyInfo, ref string strMsg)
        {
            //此处不再初始化，因为有时候 可能会从外面传入值
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
                strMsg = "参数错误";
                return -1;
            }

            if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
            {
                //strMsg = "无费用信息";
                //return -1;
                //没有费用信息时，也要查询最新的已收费信息，防止更换患者后，信息显示错误
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
        /// 计算处方费用，按医保合同单位处理
        /// 医生站调用
        /// 
        /// 返回 小于等于 0 计算失败，  等于 1 计算成功， 等于 2 计算成功且报销超标
        /// </summary>
        /// <param name="regInfo">挂号信息</param>
        /// <param name="arlFeeItemList">费用信息</param>
        /// <param name="blnNeedAddup">是否需要累计</param>
        /// <param name="arlMoneyInfo">
        /// arlMoneyInfo[0] = 累计总金额
        /// arlMoneyInfo[1] = 累计报销金额
        /// arlMoneyInfo[2] = 累计自费金额
        /// arlMoneyInfo[3] = 累计医院减免金额
        /// arlMoneyInfo[4] = 总金额
        /// arlMoneyInfo[5] = 医保报销金额
        /// arlMoneyInfo[6] = 自费金额
        /// arlMoneyInfo[7] = 医院减免金额
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
                strMsg = "参数错误";
                return -1;
            }

            //FS.HISFC.Models.Registration.Register register = this.registerManager.GetByClinic(regInfo.ID);
            //if (register == null || string.IsNullOrEmpty(register.ID))
            //{
            //    strMsg = "当前患者不存在挂号记录";
            //    return -1;
            //}

            if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
            {
                strMsg = "无费用信息";
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


        #region 转换组套为明细

        private static string clinicCode = string.Empty;
        private static Dictionary<string, Undrug> dictionaryPatientItems = new Dictionary<string, Undrug>();

        /// <summary>
        /// 转换组套为明细（门诊用）
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

            //如果dictionary不包含这个患者的信息，则清空信息
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
        /// 转换组套为明细（住院用）
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
        /// 转换组套为明细
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

                #region  转化为实体
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
                feeitemlist.Patient.ID = f.Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = reg.PID.CardNO;//门诊卡号 
                feeitemlist.Order.ID = "";//医嘱流水号

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = "";//order.CheckPartRecord;//检体 
                feeitemlist.Order.Combo.ID = f.Order.Combo.ID;//组合号
                //if (f.Order.Unit == "[复合项]")
                //{
                //feeitemlist.IsGroup = true;
                feeitemlist.UndrugComb.ID = f.Item.ID;
                feeitemlist.UndrugComb.Name = f.Item.Name;
                feeitemlist.UndrugComb.Qty = f.Item.Qty;//存储复合项目明细数量
                //}

                //if (order.Item.IsPharmacy && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl )
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && !((FS.HISFC.Models.Pharmacy.Item)f.Item).IsSubtbl)
                {
                    feeitemlist.ExecOper.Dept.ID = f.Order.StockDept.Clone().ID;//传扣库科室
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
                            //执行科室按照原来基本信息维护的执行科室
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
                feeitemlist.InjectCount = f.Order.InjectCount;//院内次数

                if (f.Order.Item.PackQty <= 0)
                {
                    feeitemlist.Item.PackQty = 1;
                }
                //自批价项目
                ////if (order.Item.Price == 0)
                ////{
                ////    order.Item.Price = order.Item.Price;
                ////}
                //by zuowy 根据收费是否是最小单位 确定收费 改时慎重
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
                        feeitemlist.FeePack = "1";//开立单位 1:包装单位 0:最小单位
                    }
                    else
                    {
                        if (feeitemlist.Item.PackQty == 0)
                        {
                            feeitemlist.Item.PackQty = 1;
                        }
                        feeitemlist.FT.OwnCost = feeitemlist.Item.Qty * feeitemlist.Item.Price / feeitemlist.Item.PackQty; //order.Qty * order.Item.Price / order.Item.PackQty;

                        feeitemlist.FeePack = "0";//开立单位 1:包装单位 0:最小单位
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

                feeitemlist.Days = f.Days;//草药付数
                feeitemlist.RecipeOper.Dept = f.RecipeOper.Dept.Clone();//开方科室信息
                feeitemlist.RecipeOper.Name = f.RecipeOper.Name;//开方医生信息
                feeitemlist.RecipeOper.ID = f.RecipeOper.ID;
                feeitemlist.Order.DoseUnit = f.Order.DoseUnit;//用量单位
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
                        feeitemlist.Order.DoseOnce = f.Order.DoseOnce;//每次用量
                    }
                }
                feeitemlist.Order.Frequency = f.Order.Frequency;//频次信息

                feeitemlist.Order.Combo.IsMainDrug = f.Order.Combo.IsMainDrug;//是否主药
                feeitemlist.Item.ID = item.ID;
                feeitemlist.Item.Name = item.Name;
                //if (order.Item.IsPharmacy)//是否药品
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//是否药品
                {
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)f.Item).BaseDose;//基本计量
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)f.Item).Quality;//药品性质
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)f.Item).DosageForm;//剂型

                    feeitemlist.IsConfirmed = false;//是否终端确认
                    feeitemlist.Item.PackQty = f.Item.PackQty;//包装数量
                }
                else
                {
                    //这里有啥用的？？
                    if (f.Order.ReTidyInfo != "SUBTBL")
                    {
                        feeitemlist.IsConfirmed = false;
                        feeitemlist.Item.PackQty = 1;//包装数量
                    }
                    else//附材中的复合项目
                    {
                        feeitemlist.IsConfirmed = false;//FS.neHISFC.Components.Function.NConvert.ToBoolean(order.Mark2);
                        feeitemlist.Item.PackQty = 1;
                    }
                }

                //feeitemlist.Order.Item.IsPharmacy = order.Item.IsPharmacy;//是否药品
                feeitemlist.Order.Item.ItemType = feeitemlist.Item.ItemType;//是否药品
                //if (order.Item.IsPharmacy)//药品
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//药品
                {
                    feeitemlist.Item.Specs = item.Specs;
                }
                feeitemlist.IsUrgent = f.Order.IsEmergency;//是否加急
                feeitemlist.Order.Sample = f.Order.Sample;//样本信息
                feeitemlist.Memo = f.Order.Memo;//备注
                feeitemlist.Item.MinFee = item.MinFee;//最小费用
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
                //feeitemlist.Item.Price = order.Item.Price;//价格

                feeitemlist.Item.PriceUnit = f.Item.PriceUnit;//价格单位
                if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.HerbalQty > 0)
                {

                }
                //feeitemlist.FT.TotCost = order.FT.TotCost;
                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.TotCost, 2);
                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.OwnCost, 2);
                // feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                //feeitemlist.FT = Round(feeitemlist.FT, 2);//取两位

                //{B9303CFE-755D-4585-B5EE-8C1901F79450} 用药品超标金额保存原来的总费用
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
                feeitemlist.Item.SysClass = item.SysClass;//系统类别
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
                feeitemlist.Order.Usage = f.Order.Usage;//用法
                feeitemlist.RecipeSequence = f.RecipeSequence;//收费序列
                //feeitemlist.RecipeNO = order.ReciptNO;//处方号  xingz
                feeitemlist.SequenceNO = f.SequenceNO;//处方流水号
                feeitemlist.FTSource = "1";//来自医嘱
                if (f.Order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//是附材
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
        /// 将组套转换成明细（住院用）
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

                //复合项目需要存的内容
                feeitemlist.UndrugComb.ID = f.Item.ID;
                feeitemlist.UndrugComb.Name = f.Item.Name;
                feeitemlist.UndrugComb.Qty = f.Item.Qty;//存储复合项目明细数量
                feeitemlist.UndrugComb.MinFee.ID = f.Item.MinFee.ID;

                //数量
                feeitemlist.Item.Qty = info.Qty * f.Item.Qty;
                feeitemlist.NoBackQty = info.Qty * f.NoBackQty;
                if (feeitemlist.Item.PackQty == 0)
                {
                    feeitemlist.Item.PackQty = 1;
                }

                //取价格
                decimal price = 0.00M;
                decimal orgPrice = 0.00M;
                this.GetPriceForInpatient(patient, item, ref price, ref orgPrice);
                feeitemlist.Item.Price = price;
                feeitemlist.Item.DefPrice = orgPrice;
                //金额
                feeitemlist.FT.TotCost = feeitemlist.Item.Price * feeitemlist.Item.Qty / feeitemlist.Item.PackQty;
                feeitemlist.FT.OwnCost = feeitemlist.FT.TotCost;
                feeitemlist.FT.PayCost = 0;
                feeitemlist.FT.PubCost = 0;

                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.TotCost, 2);
                feeitemlist.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.OwnCost, 2);
                //确认标记
                feeitemlist.Item.IsNeedConfirm = item.IsNeedConfirm;
                //主键，处方号不变，处方内流水号变化

                alFeeItemList.Add(feeitemlist);
            }

            return alFeeItemList;
        }

        #endregion



        #endregion

        #region 长嘱停止核对时自动退费申请


        /// <summary>
        /// 保存退费申请信息
        /// </summary>
        /// <param name="combNo">组合号</param>
        /// <param name="dcTimes">退费的执行档次数</param>
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
                //嘱托、描述医嘱不判断退费 houwb 2011-4-15
                if (!order.OrderType.IsCharge || order.Item.ID == "999")
                {
                    continue;
                }

                ArrayList alExeOrder = this.orderManager.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                //附材的先不判断
                //考虑到附材添加后和医嘱执行次数一致，不会出现相同退费次数，退今天的医嘱，昨天的附材的情况
                if (dcTimes > alExeOrder.Count && !order.IsSubtbl)
                {
                    this.Err = order.Item.Name + "退费次数大于最大收费次数，请重新选择退费次数!";
                    return -1;
                }

                ArrayList feeItemLists = new ArrayList();

                //执行档按照使用情况倒序排列
                alExeOrder.Sort(execComPare);

                //婴儿的费用是否可以收取到妈妈身上
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                }

                //住院号
                string patientNo = "";

                int i = 1;
                foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
                {
                    //未收费的执行档不在判断
                    if (!exec.IsCharge)
                    {
                        continue;
                    }
                    if (i > dcTimes)
                    {
                        break;
                    }

                    //允许退停用之前费用
                    //if (exec.DateUse > order.EndTime)
                    //{

                    //婴儿的费用收在妈妈的身上 
                    if (motherPayAllFee == "1")
                    {
                        patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                        //没有找到母亲住院号，则认为此患者不是婴儿
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
                        this.Err = exec.Order.Item.Name + "，使用时间【" + exec.DateUse.ToString() + "】已经没有可退数量！";
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

                string errMsg = string.Empty;//错误信息
                int returnValue = 0;//返回值
                DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //获得退费申请号
                string applyBillCode = this.returnMgr.GetReturnApplyBillCode();
                if (applyBillCode == null || applyBillCode == string.Empty)
                {
                    this.Err = "获取申请单号出错！" + this.returnMgr.Err;
                    return -1;
                }
                //取患者信息
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();


                //循环处理退费数据
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
                {
                    patientInfo.ID = feeItemList.Patient.ID;

                    if (this.QuitFeeApply(patientInfo, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                    {
                        return -1;
                    }

                    //#region 物资退费申请
                    ////物资退费申请
                    //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                    //{
                    //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                    //    {
                    //        this.Err = Language.Msg("物资退费失败!" + this.inpatientManager.Err);

                    //        return -1;
                    //    }
                    //}
                    //#endregion

                    ////如果草药付数没有赋值,默认赋值为1
                    //if (feeItemList.Days == 0)
                    //{
                    //    feeItemList.Days = 1;
                    //}

                    //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                    //if (feeItemListTemp == null)
                    //{
                    //    this.Err = Language.Msg("获得项目基本信息出错!" + this.inpatientManager.Err);

                    //    return -1;
                    //}

                    //if (feeItemList.MateList.Count > 0)
                    //{
                    //    feeItemListTemp.MateList = feeItemList.MateList;
                    //}
                    ////向退费单中填写记录
                    //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug 
                    //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                    //{
                    //    if (feeItemList.Item.User01 == "1")
                    //    {
                    //        feeItemList.User01 = "送住院处";
                    //    }
                    //    else
                    //    {
                    //        feeItemList.User01 = "送药房";
                    //    }
                    //}
                    //else
                    //{
                    //    feeItemList.User01 = "送住院处";
                    //}
                    //if (feeItemList.Memo != "OLD")
                    //{
                    //    feeItemList.User02 = applyBillCode;
                    //}
                    ////对已经保存过的退费申请不进行处理
                    //if (feeItemList.Memo == "OLD")
                    //{
                    //    continue;
                    //}

                    ////更新费用表中的可退数量字段
                    ////如果是药品则更新药品的退药数量，否则更新非药品
                    //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                    //if (returnValue == -1)
                    //{
                    //    this.Err = errMsg;

                    //    return -1;
                    //}

                    ////临时项目赋予退费申请号
                    //feeItemListTemp.User02 = applyBillCode;

                    ////如果是药品并且已经摆药，则调用退药申请；否则调用退费申请。
                    //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug 
                    //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                    //{
                    //    //退药申请,使用数据库中取得的实体和用户操作的数量
                    //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    //    {
                    //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    //    }

                    //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    //    info.ID = feeItemListTemp.Patient.ID;

                    //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                    //    {
                    //        this.Err = Language.Msg("退药申请失败!" + this.PharmarcyManager.Err);

                    //        return -1;
                    //    }
                    //}
                    //else//对于非药品和未摆药的药品，直接做退费申请
                    //{
                    //    //使用数据库中取得的实体和用户操作的数量
                    //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    //    if (feeItemList.FTRate.ItemRate != 0)
                    //    {
                    //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    //    }

                    //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    //    info.ID = feeItemListTemp.Patient.ID;

                    //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                    //    {
                    //        this.Err = Language.Msg("插入退费申请失败!") + this.returnMgr.Err;

                    //        return -1;
                    //    }


                    //    //没有摆药的药品在退费申请的同时，作废摆药申请
                    //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    //    {
                    //        //取摆药申请记录，判断其状态是否发生并发。（不在CancelApplyOut中判断并发是因为有些收费后的医嘱没有发送到药房，不存在摆药申请记录）
                    //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //        if (applyOut == null)
                    //        {
                    //            this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                    //            return -1;
                    //        }

                    //        //如果取到的实体ID为""，则表示医嘱并未发送。未发送的医嘱不允许退费，不然发送时药房会对此退费的项目进行发药。
                    //        if (applyOut.ID == string.Empty)
                    //        {
                    //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】没有发送到药房，请先发送，然后再做退费申请!");

                    //            return -1;
                    //        }

                    //        //并发
                    //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    //        {
                    //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已被其他操作员退费，请刷新当前数据!");

                    //            return -1;
                    //        }

                    //        //作废摆药申请
                    //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //        if (returnValue == -1)
                    //        {
                    //            this.Err = Language.Msg("作废摆药申请出错!") + PharmarcyManager.Err;

                    //            return -1;
                    //        }
                    //        if (returnValue == 0)
                    //        {
                    //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已摆药，请重新检索数据");

                    //            return -1;
                    //        }

                    //        //如果是部分退费(用户退药的数量小于费用表中的可退数量),要对剩余的药品做摆药申请.
                    //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                    //        {
                    //            //取收费对应的摆药申请记录
                    //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                    //            if (applyOutTemp == null)
                    //            {
                    //                this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                    //                return -1;
                    //            }

                    //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                    //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                    //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                    //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态

                    //            applyOutTemp.ID = "";
                    //            //将剩余数量发送摆药申请  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                    //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                    //            {
                    //                this.Err = Language.Msg("重新插入发药申请出错!") + this.PharmarcyManager.Err;

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
                    //        this.Err = Language.Msg("判断是否存在患者库存时出错") + this.PharmarcyManager.Err;

                    //        return -1;
                    //    }
                    //    //对患者库存进行清零操作
                    //    if (patientDrugStorageList.Count > 0)
                    //    {
                    //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                    //        storageBase.Quantity = -storageBase.Quantity;
                    //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                    //        storageBase.PrivType = "AAAA";	//记录住院退费标记
                    //        if (storageBase.ID == string.Empty)
                    //        {
                    //            storageBase.ID = applyBillCode;
                    //            storageBase.SerialNO = 0;
                    //        }

                    //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                    //        {
                    //            this.Err = Language.Msg("更新患者库存出错!") + this.PharmarcyManager.Err;

                    //            return -1;
                    //        }
                    //    }
                    //}
                }
            }
            return 1;
        }

        /// <summary>
        /// 保存退费申请信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="combNo">组合号</param>
        /// <param name="dcTimes">退费次数</param>
        /// <param name="quitConfirm">是否直接退费</param>
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

            //住院号
            string patientNo = "";

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            {
                //嘱托、描述医嘱不判断退费 houwb 2011-4-15
                if (!order.OrderType.IsCharge || order.Item.ID == "999")
                {
                    continue;
                }

                ArrayList alExeOrder = this.orderManager.QueryExecOrderByOneOrder(order.ID, order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");

                //附材的先不判断
                //考虑到附材添加后和医嘱执行次数一致，不会出现相同退费次数，退今天的医嘱，昨天的附材的情况
                if (dcTimes > alExeOrder.Count && !order.IsSubtbl)
                {
                    this.Err = order.Item.Name + "退费次数大于最大收费次数，请重新选择退费次数!";
                    return -1;
                }

                ArrayList feeItemLists = new ArrayList();

                //执行档按照使用情况倒序排列
                alExeOrder.Sort(execComPare);

                //婴儿的费用是否可以收取到妈妈身上
                if (string.IsNullOrEmpty(motherPayAllFee))
                {
                    motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
                }

                int i = 1;
                foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
                {
                    //未收费的执行档不在判断
                    if (!exec.IsCharge)
                    {
                        continue;
                    }
                    if (i > dcTimes)
                    {
                        break;
                    }

                    //婴儿的费用收在妈妈的身上 
                    if (motherPayAllFee == "1")
                    {
                        patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                        //没有找到母亲住院号，则认为此患者不是婴儿
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
                        this.Err = exec.Order.Item.Name + "，使用时间【" + exec.DateUse.ToString() + "】已经没有可退数量！";
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

                string errMsg = string.Empty;//错误信息
                int returnValue = 0;//返回值
                DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //获得退费申请号
                string applyBillCode = string.Empty;

                applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
                if (applyBillCode == null || applyBillCode == string.Empty)
                {
                    this.Err = errMsg;
                    return -1;
                }

                #region 循环退费或申请

                ArrayList alQuitFeeItems = new ArrayList();

                //循环处理退费数据
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
                {
                    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                    info.ID = feeItemList.Patient.ID;

                    //如果收费科室不是本科室或病区，则只能做申请
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
        /// 是否是本科室 根据登陆科室校验
        /// </summary>
        /// <param name="deptCode">需要校验的科室</param>
        /// <returns></returns>
        private bool CheckIsSameDept(FS.FrameWork.Models.NeuObject deptObj)
        {
            try
            {
                ArrayList alDept = this.managerIntegrate.QueryDepartment(deptObj.ID);
                alDept.AddRange(this.managerIntegrate.QueryNurseStationByDept(deptObj));
                alDept.Add(deptObj);

                //当登陆科室为当前科室、对应病区、对应科室时，认定为自己的科室！
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
        /// 保存退费申请信息
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

            //婴儿的费用是否可以收取到妈妈身上
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            //住院号
            string patientNo = "";

            #region 获取费用

            foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
            {
                //未收费的执行档不在判断
                if (!exec.IsCharge)
                {
                    continue;
                }

                //婴儿的费用收在妈妈的身上 
                if (motherPayAllFee == "1")
                {
                    patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                    //没有找到母亲住院号，则认为此患者不是婴儿
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

            string errMsg = string.Empty;//错误信息
            int returnValue = 0;//返回值
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //获得退费申请号
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                this.Err = errMsg;
                return -1;
            }

            //循环处理退费数据
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = feeItemList.Patient.ID;
                if (this.QuitFeeApply(info, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                {
                    return -1;
                }
                //#region 物资退费申请
                ////物资退费申请
                //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                //{
                //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                //    {
                //        this.Err = Language.Msg("物资退费失败!" + this.inpatientManager.Err);

                //        return -1;
                //    }
                //}
                //#endregion

                ////如果草药付数没有赋值,默认赋值为1
                //if (feeItemList.Days == 0)
                //{
                //    feeItemList.Days = 1;
                //}

                //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                //if (feeItemListTemp == null)
                //{
                //    this.Err = Language.Msg("获得项目基本信息出错!" + this.inpatientManager.Err);

                //    return -1;
                //}

                //if (feeItemList.MateList.Count > 0)
                //{
                //    feeItemListTemp.MateList = feeItemList.MateList;
                //}
                ////向退费单中填写记录
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    if (feeItemList.Item.User01 == "1")
                //    {
                //        feeItemList.User01 = "送住院处";
                //    }
                //    else
                //    {
                //        feeItemList.User01 = "送药房";
                //    }
                //}
                //else
                //{
                //    feeItemList.User01 = "送住院处";
                //}
                //if (feeItemList.Memo != "OLD")
                //{
                //    feeItemList.User02 = applyBillCode;
                //}
                ////对已经保存过的退费申请不进行处理
                //if (feeItemList.Memo == "OLD")
                //{
                //    continue;
                //}

                ////更新费用表中的可退数量字段
                ////如果是药品则更新药品的退药数量，否则更新非药品
                //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                //if (returnValue == -1)
                //{
                //    this.Err = errMsg;

                //    return -1;
                //}

                ////临时项目赋予退费申请号
                //feeItemListTemp.User02 = applyBillCode;

                ////如果是药品并且已经摆药，则调用退药申请；否则调用退费申请。
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    //退药申请,使用数据库中取得的实体和用户操作的数量
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                //    {
                //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("退药申请失败!" + this.PharmarcyManager.Err);

                //        return -1;
                //    }
                //}
                //else//对于非药品和未摆药的药品，直接做退费申请
                //{
                //    //使用数据库中取得的实体和用户操作的数量
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                //    if (feeItemList.FTRate.ItemRate != 0)
                //    {
                //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("插入退费申请失败!") + this.returnMgr.Err;

                //        return -1;
                //    }


                //    //没有摆药的药品在退费申请的同时，作废摆药申请
                //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //    {
                //        //取摆药申请记录，判断其状态是否发生并发。（不在CancelApplyOut中判断并发是因为有些收费后的医嘱没有发送到药房，不存在摆药申请记录）
                //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (applyOut == null)
                //        {
                //            this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }

                //        //如果取到的实体ID为""，则表示医嘱并未发送。未发送的医嘱不允许退费，不然发送时药房会对此退费的项目进行发药。
                //        if (applyOut.ID == string.Empty)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】没有发送到药房，请先发送，然后再做退费申请!");

                //            return -1;
                //        }

                //        //并发
                //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已被其他操作员退费，请刷新当前数据!");

                //            return -1;
                //        }

                //        //作废摆药申请
                //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (returnValue == -1)
                //        {
                //            this.Err = Language.Msg("作废摆药申请出错!") + PharmarcyManager.Err;

                //            return -1;
                //        }
                //        if (returnValue == 0)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已摆药，请重新检索数据");

                //            return -1;
                //        }

                //        //如果是部分退费(用户退药的数量小于费用表中的可退数量),要对剩余的药品做摆药申请.
                //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                //        {
                //            //取收费对应的摆药申请记录
                //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //            if (applyOutTemp == null)
                //            {
                //                this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }

                //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态

                //            applyOutTemp.ID = "";
                //            //将剩余数量发送摆药申请  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                //            {
                //                this.Err = Language.Msg("重新插入发药申请出错!") + this.PharmarcyManager.Err;

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
                //        this.Err = Language.Msg("判断是否存在患者库存时出错") + this.PharmarcyManager.Err;

                //        return -1;
                //    }
                //    //对患者库存进行清零操作
                //    if (patientDrugStorageList.Count > 0)
                //    {
                //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                //        storageBase.Quantity = -storageBase.Quantity;
                //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                //        storageBase.PrivType = "AAAA";	//记录住院退费标记
                //        if (storageBase.ID == string.Empty)
                //        {
                //            storageBase.ID = applyBillCode;
                //            storageBase.SerialNO = 0;
                //        }

                //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                //        {
                //            this.Err = Language.Msg("更新患者库存出错!") + this.PharmarcyManager.Err;

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
                //患者库存时，存在部分执行档没有对应收费记录的情况
                feeItemListTempArray = inpatientManager.GetItemListByExecSQNAll(inPatientNo, execSeq, itemType);
                if (feeItemListTempArray == null || feeItemListTempArray.Count == 0)
                {
                    return new ArrayList();
                }

                //该版本-患者库存，数量QTY为0的时候，也插入费用记录中，所以要区分
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
        /// 保存退费申请信息,根据传入参数自动确认退费
        /// 非本科室退费申请可以自动确认
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

            //婴儿的费用是否可以收取到妈妈身上
            if (string.IsNullOrEmpty(motherPayAllFee))
            {
                motherPayAllFee = this.controlParamIntegrate.GetControlParam<string>(SysConst.Use_Mother_PayAllFee, false, "0");
            }

            //住院号
            string patientNo = "";

            #region 获取费用

            foreach (FS.HISFC.Models.Order.ExecOrder exec in alExeOrder)
            {
                //未收费的执行档不在判断
                if (!exec.IsCharge)
                {
                    continue;
                }

                //婴儿的费用收在妈妈的身上 
                if (motherPayAllFee == "1")
                {
                    patientNo = this.RadtIntegrate.QueryBabyMotherInpatientNO(exec.Order.Patient.ID);

                    //没有找到母亲住院号，则认为此患者不是婴儿
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
                    this.Err = exec.Order.Item.Name + "，使用时间【" + exec.DateUse.ToString() + "】已经没有可退数量！";
                    return -1;
                }
                feeItemLists.AddRange(feeItemListTempArray);

                //ArrayList feeItemListTempArray = inpatientManager.GetItemListByExecSQN(patientNo, exec.ID, exec.Order.Item.ItemType);

                //if (feeItemListTempArray == null || feeItemListTempArray.Count == 0)
                //{
                //    this.Err = exec.Order.Item.Name + "，使用时间【" + exec.DateUse.ToString() + "】已经没有可退数量！";
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

            string errMsg = string.Empty;//错误信息
            int returnValue = 0;//返回值
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //获得退费申请号
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                this.Err = errMsg;
                return -1;
            }

            //循环处理退费数据
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists)
            {
                FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = feeItemList.Patient.ID;
                if (this.QuitFeeApply(info, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                {
                    return -1;
                }
                //#region 物资退费申请
                ////物资退费申请
                //if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
                //{
                //    if (materialManager.ApplyMaterialFeeBack(feeItemList.MateList, false) < 0)
                //    {
                //        this.Err = Language.Msg("物资退费失败!" + this.inpatientManager.Err);

                //        return -1;
                //    }
                //}
                //#endregion

                ////如果草药付数没有赋值,默认赋值为1
                //if (feeItemList.Days == 0)
                //{
                //    feeItemList.Days = 1;
                //}

                //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = feeItemList;

                //if (feeItemListTemp == null)
                //{
                //    this.Err = Language.Msg("获得项目基本信息出错!" + this.inpatientManager.Err);

                //    return -1;
                //}

                //if (feeItemList.MateList.Count > 0)
                //{
                //    feeItemListTemp.MateList = feeItemList.MateList;
                //}
                ////向退费单中填写记录
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    if (feeItemList.Item.User01 == "1")
                //    {
                //        feeItemList.User01 = "送住院处";
                //    }
                //    else
                //    {
                //        feeItemList.User01 = "送药房";
                //    }
                //}
                //else
                //{
                //    feeItemList.User01 = "送住院处";
                //}
                //if (feeItemList.Memo != "OLD")
                //{
                //    feeItemList.User02 = applyBillCode;
                //}
                ////对已经保存过的退费申请不进行处理
                //if (feeItemList.Memo == "OLD")
                //{
                //    continue;
                //}

                ////更新费用表中的可退数量字段
                ////如果是药品则更新药品的退药数量，否则更新非药品
                //returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                //if (returnValue == -1)
                //{
                //    this.Err = errMsg;

                //    return -1;
                //}

                ////临时项目赋予退费申请号
                //feeItemListTemp.User02 = applyBillCode;

                ////如果是药品并且已经摆药，则调用退药申请；否则调用退费申请。
                //if (feeItemListTemp.Item.ItemType == EnumItemType.Drug
                //    && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                //{
                //    //退药申请,使用数据库中取得的实体和用户操作的数量
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                //    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                //    {
                //        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.PharmarcyManager.ApplyOutReturn(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("退药申请失败!" + this.PharmarcyManager.Err);

                //        return -1;
                //    }
                //}
                //else//对于非药品和未摆药的药品，直接做退费申请
                //{
                //    //使用数据库中取得的实体和用户操作的数量
                //    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                //    if (feeItemList.FTRate.ItemRate != 0)
                //    {
                //        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                //    }

                //    FS.HISFC.Models.RADT.PatientInfo info = new FS.HISFC.Models.RADT.PatientInfo();
                //    info.ID = feeItemListTemp.Patient.ID;

                //    if (this.returnMgr.Apply(info, feeItemListTemp, nowTime) == -1)
                //    {
                //        this.Err = Language.Msg("插入退费申请失败!") + this.returnMgr.Err;

                //        return -1;
                //    }


                //    //没有摆药的药品在退费申请的同时，作废摆药申请
                //    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                //    {
                //        //取摆药申请记录，判断其状态是否发生并发。（不在CancelApplyOut中判断并发是因为有些收费后的医嘱没有发送到药房，不存在摆药申请记录）
                //        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (applyOut == null)
                //        {
                //            this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                //            return -1;
                //        }

                //        //如果取到的实体ID为""，则表示医嘱并未发送。未发送的医嘱不允许退费，不然发送时药房会对此退费的项目进行发药。
                //        if (applyOut.ID == string.Empty)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】没有发送到药房，请先发送，然后再做退费申请!");

                //            return -1;
                //        }

                //        //并发
                //        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已被其他操作员退费，请刷新当前数据!");

                //            return -1;
                //        }

                //        //作废摆药申请
                //        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //        if (returnValue == -1)
                //        {
                //            this.Err = Language.Msg("作废摆药申请出错!") + PharmarcyManager.Err;

                //            return -1;
                //        }
                //        if (returnValue == 0)
                //        {
                //            this.Err = Language.Msg("项目【") + feeItemListTemp.Item.Name + Language.Msg("】已摆药，请重新检索数据");

                //            return -1;
                //        }

                //        //如果是部分退费(用户退药的数量小于费用表中的可退数量),要对剩余的药品做摆药申请.
                //        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                //        {
                //            //取收费对应的摆药申请记录
                //            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                //            if (applyOutTemp == null)
                //            {
                //                this.Err = Language.Msg("获得申请信息出错!") + this.PharmarcyManager.Err;

                //                return -1;
                //            }

                //            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                //            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                //            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                //            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态

                //            applyOutTemp.ID = "";
                //            //将剩余数量发送摆药申请  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                //            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                //            {
                //                this.Err = Language.Msg("重新插入发药申请出错!") + this.PharmarcyManager.Err;

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
                //        this.Err = Language.Msg("判断是否存在患者库存时出错") + this.PharmarcyManager.Err;

                //        return -1;
                //    }
                //    //对患者库存进行清零操作
                //    if (patientDrugStorageList.Count > 0)
                //    {
                //        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                //        storageBase.Quantity = -storageBase.Quantity;
                //        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                //        storageBase.PrivType = "AAAA";	//记录住院退费标记
                //        if (storageBase.ID == string.Empty)
                //        {
                //            storageBase.ID = applyBillCode;
                //            storageBase.SerialNO = 0;
                //        }

                //        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                //        {
                //            this.Err = Language.Msg("更新患者库存出错!") + this.PharmarcyManager.Err;

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
                            msg += "项目:" + apply.Item.Name + "执行科室非本科室，仅作退费申请！" + "\n";
                        }
                    }

                    if (confirm)
                    {
                        int ret = returnMgr.ConfirmApply(apply);

                        if (ret < 0)
                        {
                            this.Err = "确认退费出错，项目:" + apply.Item.Name + "," + this.PharmarcyManager.Err;

                            return -1;
                        }
                    }
                }
            }

            return 1;
        }

        #endregion

        #region 出院收取床位费等

        /// <summary>
        /// 获取公费患者计算限额的日期
        /// </summary>
        /// <param name="pInfo">患者信息</param>
        /// <returns></returns>
        public DateTime GetAdjustOverLimitFromDate(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            feeMgr.SetTrans(this.trans);

            DateTime dtRealBegin = new DateTime();
            DataSet dsBalanceInfo = new DataSet();
            if (feeMgr.GetBeforeBalanceInfo(pInfo, ref dsBalanceInfo) == -1)
            {
                this.Err = "获取患者计算限额日期出错！" + feeMgr.Err;
                return System.DateTime.MinValue;
            }
            if (dsBalanceInfo.Tables[0].Rows.Count <= 0)
            {
                //以前没有做过中结，没有中结信息,则计算限额从本次入院开始
                //inDays = ( (TimeSpan)( patientInfo.PVisit.OutTime.Date - patientInfo.PVisit.InTime.Date ) ).Days;
                dtRealBegin = pInfo.PVisit.InTime;
            }
            else
            {
                //处理分段结算＼合同单位不同的情况
                //没有都是分段变更公费合同单位的情况，如果有不处理
                //（１）如果患者身份中间没有做变更，从住院日期开始算起；
                //（２）如果患者身份作变更，从本次身份变更的日期开始计算限额
                foreach (DataRow drRow in dsBalanceInfo.Tables[0].Rows)
                {
                    //是否有变更
                    bool hsChange = false;
                    //说明本次中结合同单位相同
                    if (drRow[0].ToString().Equals(pInfo.Pact.PayKind.ID) && !hsChange)
                    {
                        //从本次结算开始结算限额
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
        /// 调整公费患者床位费
        /// </summary>
        /// <param name="pInfo">住院患者信息</param>
        /// <param name="isOut">是否是出院调整</param>
        /// <returns></returns>
        public int AdjustOverLimitBed(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isOut)
        {
            if (pInfo == null)
            {
                this.Err = "患者实体没有初始化！";
                return -1;
            }
            if (pInfo.Pact.PayKind.ID != "03")
            {
                return 1;
            }

            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            feeMgr.SetTrans(this.trans);

            //患者的床位限额，患者从入院开始限额固定
            decimal dayLimit = pInfo.FT.BedLimitCost;
            //患者从入院开始到调整时的住院天数
            int inDays = 1;
            if (pInfo.PVisit.InTime == null || pInfo.PVisit.InTime == DateTime.MinValue)
            {
                this.Err = "患者入院日期没有赋值！请查询！";
                return -1;
            }
            //获取系统时间
            DateTime dtSys = feeMgr.GetDateTimeFromSysDateTime();
            DateTime dtTNN = new DateTime(2008, 4, 18);
            DateTime dtRealBegin = this.GetAdjustOverLimitFromDate(pInfo);
            DateTime dtEnd = feeMgr.GetDateTimeFromSysDateTime();
            if (dtRealBegin == System.DateTime.MinValue)
            {
                dtRealBegin = pInfo.PVisit.InTime;
            }
            //[[[[[[出院调整时：住院天数=出院日期-入院日期]]]]]]
            //-------------------New Deal---------------
            //处理为：如果为本次住院为同一个合同单位，则从住院日期开始进行调整
            //如果住院期间发生合同单位变更则从当前合同单位变更的起始日期进行调整
            if (isOut)
            {
                if (pInfo.PVisit.OutTime == DateTime.MinValue || pInfo.PVisit.OutTime == null)
                {
                    this.Err = "患者的出院日期没有赋值！";
                    return -1;
                }
                if (pInfo.PVisit.OutTime < pInfo.PVisit.InTime)
                {
                    this.Err = "患者出院日期小于入院日期！请查询！";
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
                    this.Err = "患者出院日期小于入院日期！请查询！";
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
                    this.Err = "患者入院时间大于系统时间！请查询！";
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
                //【张琦修改】inDays = ( (TimeSpan)( pInfo.PVisit.OutTime.Date - dtRealBegin.Date ) ).Days;
                inDays = ((TimeSpan)(dtSys.Date - dtRealBegin.Date)).Days;
                //每天晚上自动调整时当天床位费在调整范围之内，所以天数+1
                //inDays = inDays + 1;
            }
            //如果是住院当天，次数为1
            if (inDays == 0)
            {
                inDays = 1;
            }
            //患者到当前时刻总的床位限额：
            decimal totLimit = pInfo.FT.BedLimitCost * inDays;
            //患者到目前为止总的床位费
            FT bedFee = null;
            //获取患者到目前为止总的床位费
            //bedFee = feeMgr.GetPatientBedFee(pInfo.ID);
            bedFee = feeMgr.GetPatientBedFee(pInfo.ID, dtRealBegin.ToString(), dtEnd.ToString());
            if (bedFee == null)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //如果限额=记账额，则无需调整。
            if (bedFee.PubCost + bedFee.PayCost == totLimit)
            {
                return 1;
            }

            //调整记录实体定义
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            //如果患者总的床位费记账部分+自负部分>床位总的限额 调整限额
            if (bedFee.PubCost + bedFee.PayCost > totLimit)
            {
                //本次调整金额
                decimal adjFee = bedFee.PayCost + bedFee.PubCost - totLimit;
                //费用赋值
                ItemList.FT.TotCost = 0;
                ItemList.FT.OwnCost = adjFee;
            }
            //如果患者不超标，并且有限额剩余，且累计自费金额>0
            else if (bedFee.OwnCost > 0)
            {
                //本次调整金额
                decimal adjFee = 0;
                //本次调整前限额剩余空间金额
                decimal adjSpan = totLimit - bedFee.PayCost - bedFee.PubCost;
                //本次调整金额为限额剩余空间金额和累计自费金额之间的小者
                adjFee = bedFee.OwnCost < adjSpan ? bedFee.OwnCost : adjSpan;
                //费用赋值
                ItemList.FT.TotCost = 0;
                ItemList.FT.OwnCost = -adjFee;
            }
            else
            {
                return 1;
            }
            //获取合同单位比例
            FS.HISFC.Models.Base.PactInfo pactUnitInfo = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            pactManagment.SetTrans(this.trans);
            pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);
            //自负比例
            decimal payRate = pactUnitInfo.Rate.PayRate;

            //在院科室
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //护士站
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            //执行科室
            ItemList.ExecOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //扣库科室
            ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //开方科室
            ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //开方医生
            ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //医生
            //价格为0
            ItemList.Item.Price = 0;
            //数量为1
            ItemList.Item.Qty = ItemList.FT.OwnCost > 0 ? 1 : -1;//数量
            //单位
            ItemList.Item.PriceUnit = "次";
            //非药品
            //ItemList.Item.IsPharmacy = false;
            ItemList.Item.ItemType = EnumItemType.UnDrug;
            ItemList.PayType = PayTypes.Balanced;
            ItemList.IsBaby = false;
            ItemList.BalanceNO = 0;
            ItemList.BalanceState = "0";
            //可退数量
            ItemList.NoBackQty = 1;
            ItemList.ChargeOper.ID = feeMgr.Operator.ID;
            //划价时间
            ItemList.ChargeOper.OperTime = dtSys;
            ItemList.FeeOper.ID = feeMgr.Operator.ID;
            ItemList.FeeOper.OperTime = dtSys;
            ItemList.ChargeOper.OperTime = dtSys;

            //床位费的最小费用代码
            ItemList.Item.MinFee.ID = "004";
            //项目代码
            ItemList.Item.ID = "F004";
            //项目名称
            ItemList.Item.Name = "超标床位费";
            //自负金额 = (总金额(0)-自费金额)*自负比例
            ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((-ItemList.FT.OwnCost) * payRate, 2);
            //记账金额 = 总金额(0)-自费金额-自负金额
            ItemList.FT.PubCost = -ItemList.FT.OwnCost - ItemList.FT.PayCost;
            //处方号
            ItemList.RecipeNO = feeMgr.GetUndrugRecipeNO();
            ItemList.SequenceNO = 1;
            //插入明细表
            if (feeMgr.InsertFeeItemList(pInfo, ItemList) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //插入汇总表
            if (feeMgr.InsertFeeInfo(pInfo, ItemList) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }
            //更新住院主表
            if (feeMgr.UpdateInMainInfoFee(pInfo.ID, ItemList.FT) == -1)
            {
                this.Err = feeMgr.Err;
                return -1;
            }


            return 1;
        }

        /// <summary>
        /// 出院补收床位费
        /// </summary>
        /// <param name="pInfo">患者实体</param>
        /// <returns></returns>
        public int SupplementBedFee(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            DateTime dt = pInfo.PVisit.PreOutTime;
            //判断患者出院日期是否有值，如果没有赋值在此处不做处理
            if (dt == DateTime.MinValue ||
                dt == null)
            {
                dt = pInfo.PVisit.OutTime;
            }

            //补收规则收费为收的
            FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee ruleFeeManager = new InpatientRuleFee();
            if (this.trans != null)
            {
                ruleFeeManager.SetTrans(this.trans);
            }

            if (ruleFeeManager.DoRuleFee(pInfo, new FS.HISFC.Models.Fee.Inpatient.FTSource("220"), pInfo.PVisit.InTime, dt) == -1)
            {
                this.Err = "补收定时收费信息失败！" + ruleFeeManager.Err;
                return -1;
            }

            //患者出院时，根据患者上次固定费用收取时间，判断是否应该补收床位费

            //如果患者没有床位号，则不进行收取。
            if (pInfo.PVisit.PatientLocation.Bed.ID == null ||
                pInfo.PVisit.PatientLocation.Bed.ID == "")
            {
                return 1;
            }

            ArrayList alFeeInfo = new ArrayList();

            //用接诊时间处理
            DateTime dtArriveDate = RadtIntegrate.GetArriveDate(pInfo.ID);
            if (dtArriveDate < pInfo.PVisit.InTime)
            {
                dtArriveDate = pInfo.PVisit.InTime;
            }
            //处理本次收费补收次数
            int days = 0;
            if (pInfo.FT.PreFixFeeDateTime == DateTime.MinValue ||
                pInfo.FT.PreFixFeeDateTime == null ||
                pInfo.FT.PreFixFeeDateTime < dtArriveDate.AddDays(-1).Date.AddHours(23))
            {
                //如果没有上次收取固定费用收取时间，则照收
                pInfo.FT.PreFixFeeDateTime = dtArriveDate.AddDays(-1).Date.AddHours(23);
                //return 1;
            }

            days = ((TimeSpan)(dt.Date - pInfo.FT.PreFixFeeDateTime.Date)).Days;
            //收取未收取完的费用
            days = days - 1;

            //这里增加判断如果是还未收取过固定费用，则天数不再减一
            if (pInfo.FT.PreFixFeeDateTime > dtArriveDate.AddMinutes(-2)
                && pInfo.FT.PreFixFeeDateTime < dtArriveDate.AddMinutes(2))
            {
                days = days + 1;
            }

            #region 收取本次床位费

            //变量定义、帮定事务
            //根据床位号获取床位等级业务层
            FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
            //根据床位等级获取床位获取收费列表业务层
            FS.HISFC.BizLogic.Fee.BedFeeItem bedFeeMgr = new FS.HISFC.BizLogic.Fee.BedFeeItem();
            //更新上次固定费用收取时间业务层函数
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            //根据项目代码获取项目实体
            FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
            //患者管理类
            FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();

            //帮定事务
            if (this.trans != null)
            {
                bedMgr.SetTrans(this.trans);
                bedFeeMgr.SetTrans(this.trans);
                feeMgr.SetTrans(this.trans);
                itemMgr.SetTrans(this.trans);
                radtInpatient.SetTrans(this.trans);
            }

            DateTime dtCurrent = bedMgr.GetDateTimeFromSysDateTime();

            //根据床号获取床位等级
            FS.HISFC.Models.Base.Bed objBed = bedMgr.GetBedInfo(pInfo.PVisit.PatientLocation.Bed.ID);
            if (objBed == null)
            {
                this.Err = "获取床位信息出错！" + bedMgr.Err;
                return -1;
            }
            //当前床位不属于该患者，不收取
            if (objBed.InpatientNO.Equals(pInfo.ID) == false)
            {
                return 1;
            }

            if (objBed.BedGrade.ID == "")
            {
                this.Err = "患者床位[" + objBed.ID + "]没有维护等级";
                return -1;
            }

            ArrayList alBed = new ArrayList();   //床位位数
            alBed.Add(objBed);     //主床

            //获取包床等费用
            //获得请包床信息，请假床算作主床
            ArrayList alOtherBed = bedMgr.GetOtherBedList(pInfo.ID);
            if (alOtherBed == null)
            {
                this.Err = "获得包床信息出错!" + bedMgr.Err;
                return -1;
            }
            alBed.AddRange(alOtherBed);     //包床

            //主床位费用
            ArrayList alBedItem = bedFeeMgr.QueryBedFeeItemByMinFeeCode(objBed.BedGrade.ID);
            if (alBedItem == null)
            {
                this.Err = objBed.ID + "床未维护收费项目!";
                return -1;
            }
            //包床位费用
            foreach (FS.HISFC.Models.Base.Bed objBedTemp in alOtherBed)
            {
                ArrayList alTemp = bedFeeMgr.QueryBedFeeItemByMinFeeCode(objBedTemp.BedGrade.ID);
                if (alTemp == null)
                {
                    this.Err = objBedTemp.ID + "床未维护收费项目!";
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
                    this.Err = objBedTemp.ID + "床未维护收费项目!";
                    return -1;
                }
                if (alBedItem.Count == 0)
                {
                    continue;
                }
                ArrayList alNormalItemSupply = new ArrayList();   //正常床位费
                ArrayList alBabyItemSupply = new ArrayList();    //婴儿床位费
                foreach (FS.HISFC.Models.Fee.BedFeeItem b in alBedItem)
                {
                    if (b.IsBabyRelation)
                    {
                        //婴儿相关
                        alBabyItemSupply.Add(b);
                    }
                    else
                    {
                        alNormalItemSupply.Add(b);
                    }
                }

                //循环收取各种固定费用-正常床位费用
                foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alNormalItemSupply)
                {
                    if (bedItem.ValidState != EnumValidState.Valid)
                    {
                        continue;
                    }

                    #region 判断非在院患者(包床W,挂床H,请假R)是否收取该项目

                    if (objBedTemp.Status.ID.ToString() == "W" || objBedTemp.Status.ID.ToString() == "H" || objBedTemp.Status.ID.ToString() == "R")
                    {
                        //如果收费项目对于非在院患者不收取费用,则不处理此项目
                        if (bedItem.ExtendFlag == "0")
                        {
                            continue;
                        }
                    }

                    #endregion

                    // 判断该项目是否和时间有关，比如空调费、取暖费
                    //如果项目不应该收取，则下一个循环
                    if (bedItem.IsTimeRelation)
                    {
                        //结束日期 >= 起始日期,认为不跨年度
                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                        {
                            //如果当前时间不在设置时间范围内，则不收取此项目费用


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--当前日期在计费有效期外
                        }
                        else
                        { //结束日期 < 起始日期 :跨年度
                            //如果当前时间不在设置时间范围内，则不收取此项目费用
                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--当前日期在计费有效期外
                        }
                    }

                    //如果是数据结构中存在个人床位费的价格，则使用
                    FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(pInfo.ID, pInfo.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        bedItem.Qty = personFeeItem.Qty;
                        bedItem.ID = personFeeItem.ID;
                        bedItem.Name = personFeeItem.Name;
                    }

                    //根据床位费用项目获取收费实体
                    FS.HISFC.Models.Fee.Item.Undrug undrug = itemMgr.GetItemByUndrugCode(bedItem.ID);
                    if (undrug == null)
                    {
                        this.Err = "床位费用项目【" + bedItem.ID + "】系统没有找到！";
                        return -1;
                    }
                    if (!undrug.IsValid)
                    {
                        this.Err = "床位费用项目【(" + undrug.UserCode + ")" + undrug.Name + "】已经停用，请联系财务科删除后再办理出院！";
                        return -1;
                    }
                    if (undrug.ID == "")
                    {
                        this.Err = "床位费用对应收费项目没有维护！";
                        return -1;
                    }

                    //计算项目价格,根据合同单位和项目计算价格
                    decimal price = undrug.Price;
                    decimal orgPrice = undrug.DefPrice;
                    if (this.GetPriceForInpatient(pInfo, undrug, ref price, ref orgPrice) == -1)
                    //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                    {
                        this.Rollback();
                        this.Err = "获取项目:" + undrug.ID + "的价格时出错!" + this.Err;
                        return -1;
                    }
                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                    {
                        price = personFeeItem.Price;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    //包床的费用明细名称增加"包床" gumzh
                    if (objBedTemp.Status.ID.ToString() == "W")
                    {
                        undrug.Name += "-包床费";
                    }

                    if (days > 0)
                    {
                        for (int i = 0; i < days; i++)
                        {
                            undrug.Qty = 1;
                            //收取床位费
                            //220（默认为出院补收，不回收）
                            //不用患者当前科室，用当前系统登录科室
                            // if (this.FeeAutoItem(pInfo, undrug,"220",pInfo.PVisit.PatientLocation.NurseCell.ID, "000000") == -1)
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                            //选择的出院日期大于今天，将费用状态改为210，以便自动退费
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

                    //如果是入院=出院 或者是收尾的情况，则补收一条
                    if (dtArriveDate.Date == dt.Date || bedItem.IsOutFeeFlag)
                    {
                        undrug.Qty = 1;
                        //收取床位费 AAAAAA用来退费用，出院登记收，召回退
                        //210自动收费（默认为出院登记补收）
                        //不用患者当前科室，用当前系统登录科室
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                        //if (this.FeeAutoItem(pInfo, undrug,"210", pInfo.PVisit.PatientLocation.NurseCell.ID, "000000") == -1)
                        if (this.FeeAutoItem(pInfo, undrug, "210", ((FS.HISFC.Models.Base.Employee)itemMgr.Operator).Dept.ID, "000000", this.itemManager.GetDateTimeFromSysDateTime(), ref itemList) == -1)
                        {
                            return -1;
                        }
                        alFeeInfo.Add(itemList);
                    }

                }

                #region 婴儿床位费用收取

                if (pInfo.IsHasBaby && alBabyItemSupply.Count > 0 && objBedTemp.Status.ID.ToString() != "W")
                {
                    #region 婴儿床位费处理

                    try
                    {
                        //获得该母亲的所有的有效婴儿-返回的是婴儿在主表的信息
                        ArrayList alBaby = radtInpatient.QueryBabiesByMother(pInfo.ID);
                        if (alBaby != null && alBaby.Count > 0)
                        {
                            for (int k = 0; k < alBaby.Count; k++)
                            {
                                FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[k] as FS.HISFC.Models.RADT.PatientInfo;

                                if (babyInfo.FT.PreFixFeeDateTime == null || babyInfo.FT.PreFixFeeDateTime == DateTime.MinValue
                                        || babyInfo.FT.PreFixFeeDateTime.Date <= new DateTime(2000, 1, 1, 0, 0, 0))
                                {
                                    //如果没有上次收取固定费用收取时间，则通过本次收取的固定时间收取
                                    DateTime dtBabyReg = babyInfo.PVisit.InTime.Date;//婴儿登记日期，本次收取固定费用日期
                                    babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyReg.Year, dtBabyReg.Month, dtBabyReg.Day, 23, 30, 0);
                                }
                                else
                                {
                                    //如果有上次收取固定费用收取时间，则+1，收取固定费用日期，同时取时间点为23:30:00
                                    DateTime dtBabyLast = babyInfo.FT.PreFixFeeDateTime.Date.AddDays(1);
                                    babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyLast.Year, dtBabyLast.Month, dtBabyLast.Day, 23, 30, 0);
                                }

                                //在婴儿出生日期之前的床位费用不应该收取,同时之前没有收取的也需要补收取
                                System.TimeSpan spanDay = dt.Date - babyInfo.FT.PreFixFeeDateTime.Date;
                                int addDays = spanDay.Days;

                                #region 费用收取处理

                                foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alBabyItemSupply)
                                {
                                    //判断是否有效
                                    if (bedItem.ValidState != EnumValidState.Valid)
                                    {
                                        continue;
                                    }

                                    #region 判断非在院患者(包床W,挂床H,请假R)是否收取该项目

                                    if (objBedTemp.Status.ID.ToString() == "W" || objBedTemp.Status.ID.ToString() == "H" || objBedTemp.Status.ID.ToString() == "R")
                                    {
                                        //如果收费项目对于非在院患者不收取费用,则不处理此项目
                                        if (bedItem.ExtendFlag == "0")
                                        {
                                            continue;
                                        }
                                    }

                                    #endregion

                                    #region 判断该项目是否和时间有关，比如空调费、取暖费

                                    //如果项目不应该收取，则下一个循环
                                    if (bedItem.IsTimeRelation)
                                    {
                                        //结束日期 >= 起始日期,认为不跨年度
                                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                        {
                                            //如果当前时间不在设置时间范围内，则不收取此项目费用
                                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                                continue;//--当前日期在计费有效期外
                                        }
                                        else
                                        {
                                            //结束日期 < 起始日期 :跨年度
                                            //如果当前时间不在设置时间范围内，则不收取此项目费用
                                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                                continue;//--当前日期在计费有效期外
                                        }
                                    }

                                    #endregion

                                    //如果是数据结构中存在个人床位费的价格，则使用
                                    FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(babyInfo.ID, babyInfo.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                                    {
                                        bedItem.Qty = personFeeItem.Qty;
                                        bedItem.ID = personFeeItem.ID;
                                        bedItem.Name = personFeeItem.Name;
                                    }

                                    //根据床位费用项目获取收费实体
                                    FS.HISFC.Models.Fee.Item.Undrug undrug = itemMgr.GetItemByUndrugCode(bedItem.ID);
                                    if (undrug == null)
                                    {
                                        this.Err = "床位费用项目【" + bedItem.ID + "】系统没有找到！";
                                        return -1;
                                    }
                                    if (!undrug.IsValid)
                                    {
                                        this.Err = "床位费用项目【(" + undrug.UserCode + ")" + undrug.Name + "】已经停用，请联系财务科删除后再办理出院！";
                                        return -1;
                                    }
                                    if (undrug.ID == "")
                                    {
                                        this.Err = "床位费用对应收费项目没有维护！";
                                        return -1;
                                    }
                                    //计算项目价格,根据合同单位和项目计算价格
                                    decimal price = undrug.Price;
                                    decimal orgPrice = undrug.DefPrice;
                                    if (this.GetPriceForInpatient(babyInfo, undrug, ref price, ref orgPrice) == -1)
                                    {
                                        this.Rollback();
                                        this.Err = "获取项目:" + undrug.ID + "的价格时出错!" + this.Err;
                                        return -1;
                                    }
                                    if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                                    {
                                        price = personFeeItem.Price;
                                    }
                                    undrug.Price = price;
                                    undrug.DefPrice = orgPrice;
                                    //包床的费用明细名称增加"包床" gumzh
                                    if (objBedTemp.Status.ID.ToString() == "W")
                                    {
                                        undrug.Name += "-包床费";
                                    }

                                    if (addDays > 0)
                                    {
                                        for (int i = 0; i < addDays; i++)
                                        {
                                            undrug.Qty = 1;
                                            //收取床位费
                                            //220（默认为出院补收，不回收）
                                            //不用患者当前科室，用当前系统登录科室
                                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = null;
                                            //选择的出院日期大于今天，将费用状态改为210，以便自动退费
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

                                    //如果是入院=出院 或者是收尾的情况，则补收一条
                                    if (babyInfo.PVisit.InTime.Date == dt.Date || bedItem.IsOutFeeFlag)
                                    {
                                        undrug.Qty = 1;
                                        //收取床位费 AAAAAA用来退费用，出院登记收，召回退
                                        //210自动收费（默认为出院登记补收）
                                        //不用患者当前科室，用当前系统登录科室
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
                        this.Err = "收取婴儿床位费失败!" + ex.Message;
                        return -1;
                    }

                    #endregion
                }

                #endregion

            }

            #region HL7消息发送
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

            //更新固定费用收取时间为出院日期的前一天
            //就是上次固定费用收取时间+本次收取天数。            
            pInfo.FT.PreFixFeeDateTime = pInfo.FT.PreFixFeeDateTime.AddDays(days);
            if (feeMgr.UpdateFixFeeDateByPerson(pInfo.ID, pInfo.FT.PreFixFeeDateTime.ToString()) == -1)
            {
                this.Err = "更新上次固定费用收取时间出错！" + feeMgr.Err;
                return -1;
            }

            //更新孩子的固定费用收取时间与母亲同一时刻
            if (pInfo.IsHasBaby)
            {
                //获得该母亲的所有的有效婴儿-返回的是婴儿在主表的信息
                ArrayList alBaby = radtInpatient.QueryBabiesByMother(pInfo.ID);
                if (alBaby != null && alBaby.Count > 0)
                {
                    for (int k = 0; k < alBaby.Count; k++)
                    {
                        FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[k] as FS.HISFC.Models.RADT.PatientInfo;
                        babyInfo.FT.PreFixFeeDateTime = pInfo.FT.PreFixFeeDateTime;
                        if (feeMgr.UpdateFixFeeDateByPerson(babyInfo.ID, babyInfo.FT.PreFixFeeDateTime.ToString()) == -1)
                        {
                            this.Err = "更新上次固定费用收取时间出错！" + feeMgr.Err;
                            return -1;
                        }
                    }
                }

            }

            return 1;

            #endregion
        }

        /// <summary>
        /// 出院召回自动退补收的费用
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public int QuitSupplementFee(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            //更新上次固定费用收取时间业务层函数
            DateTime operDate = inpatientManager.GetDateTimeFromSysDateTime();

            //查找费用来为21%的费用信息，因为是出院登记时补收的费用，所以召回时进行退费，避免重收
            ArrayList alFee = inpatientManager.QueryFeeItemLists(pInfo.ID, new FS.HISFC.Models.Fee.Inpatient.FTSource("21%"));
            if (alFee == null)
            {
                this.Err = "获取出院补收费用信息出错！" + inpatientManager.Err;
                return -1;
            }

            if (alFee.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFee)
                {
                    //可退为零的不处理
                    if (feeItemList.NoBackQty <= 0)
                    {
                        continue;
                    }

                    //负交易的不处理
                    if (feeItemList.TransType == TransTypes.Negative)
                    {
                        continue;
                    }

                    //结算状态为1的不处理
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
                        this.Err = "退费失败！" + this.Err;
                        return -1;
                    }
                }
            }

            //更新固定费用收取时间为召回日期的前一天
            //这样就不会收取中间的费用
            if (pInfo.FT.PreFixFeeDateTime > DateTime.MinValue)
            {
                pInfo.FT.PreFixFeeDateTime = operDate.Date.AddMinutes(-30);

                if (inpatientManager.UpdateFixFeeDateByPerson(pInfo.ID, pInfo.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Err = "更新上次固定费用收取时间出错！" + inpatientManager.Err;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        ///  自动收费函数
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
        ///  自动收费函数
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

            //根据传入的实体处理价格
            decimal price = 0;
            decimal orgPrice = 0;

            if (this.GetPriceForInpatient(pInfo, item, ref price, ref orgPrice) == -1)
            {
                this.Err = "取项目:" + item.Name + "的价格出错!" + this.Err;
                return -1;
            }
            item.Price = price;
            // 原始总费用（本来应收费用，不考虑合同单位因素）
            item.DefPrice = orgPrice;

            return this.FeeAutoItem(pInfo, item, ftsource, execDept, operCode, chargeDate, ref feeItemList);
        }


        /// <summary>
        ///  自动收费函数（不会重新取价格）
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
            //在院科室
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //护士站
            ((FS.HISFC.Models.RADT.PatientInfo)ItemList.Patient).PVisit.PatientLocation.NurseCell.ID = pInfo.PVisit.PatientLocation.NurseCell.ID;
            //执行科室
            ItemList.ExecOper.Dept.ID = execDept;
            //扣库科室
            ItemList.StockOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //开方科室
            ItemList.RecipeOper.Dept.ID = pInfo.PVisit.PatientLocation.Dept.ID;
            //开方医生
            ItemList.RecipeOper.ID = pInfo.PVisit.AdmittingDoctor.ID; //医生

            //药品默认按最小单位收费,显示价格也为最小单位价格,存入数据库的为包装单位价格
            //if (item.IsPharmacy)//药品
            if (item.ItemType == EnumItemType.Drug)//药品
            {
                item.Price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);

                // {54B0C254-3897-4241-B3BD-17B19E204C8C}
                item.DefPrice = FS.FrameWork.Public.String.FormatNumber(item.DefPrice / item.PackQty, 4);
            }

            /* 外部已经赋值部分：价格、数量、单位、是否药品
             * ItemList.Item.Price = 0;ItemList.Item.Qty;  
             * ItemList.Item.PriceUnit = "次"; 
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
            //可退数量
            ItemList.NoBackQty = item.Qty;

            //操作员
            ItemList.FeeOper.ID = operCode;
            ItemList.ChargeOper.ID = operCode;
            ItemList.ChargeOper.OperTime = chargeDate;
            ItemList.FeeOper.OperTime = dtNow;

            ItemList.FT.OwnCost = ItemList.FT.TotCost;
            ItemList.TransType = TransTypes.Positive;

            //费用来源
            ItemList.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource(ftsource);

            feeItemList = ItemList;

            //调用收费函数
            return this.FeeItem(pInfo, ItemList);
        }

        #endregion

        #region 住院退费申请和直接退费整合

        /// <summary>
        ///  退费函数（包括申请和直接退费）
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="feeItemList">费用信息</param>
        /// <param name="isApply">是否申请，否则为直接退费</param>
        /// <param name="applyBillCode">申请单号</param>
        /// <returns></returns>
        public int QuitFeeApply(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, bool isApply, string applyBillCode, DateTime quitFeeDateTime, ref string msg)
        {
            //获取最新时间
            if (quitFeeDateTime <= DateTime.MinValue)
            {
                quitFeeDateTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            }

            //如果
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
            if (feeItemListTemp == null)
            {
                this.Err = "获得项目基本信息出错!" + this.inpatientManager.Err;
                return -1;
            }
            //判断可退数量
            if (feeItemListTemp.NoBackQty <= 0)//说明没有可退的
            {
                this.Err = string.Format("项目：{0}{1}的费用已经没有可退的数量，不需要重复退费！\r\n\r\n可能原因：已进行手工退费！", feeItemListTemp.Item.Name, string.IsNullOrEmpty(feeItemListTemp.UndrugComb.Name) ? "" : "【" + feeItemListTemp.UndrugComb.Name + "】");
                return -1;
            }
            else if (feeItemListTemp.NoBackQty < feeItemListTemp.Item.Qty)//说明已经有申请了，不需要在重复申请
            {
                this.Err = string.Format("项目：{0}{1}的费用已经申请了数量{2}，请先取消申请或确认后再退费！", feeItemListTemp.Item.Name, string.IsNullOrEmpty(feeItemListTemp.UndrugComb.Name) ? "" : "【" + feeItemListTemp.UndrugComb.Name + "】", feeItemListTemp.Item.Qty - feeItemListTemp.NoBackQty);
                return -1;
            }

            if (isApply)
            {
                #region 退费申请

                //如果草药付数没有赋值,默认赋值为1
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }

                if (feeItemList.MateList.Count > 0)
                {
                    feeItemListTemp.MateList = feeItemList.MateList;
                }
                //向退费单中填写记录
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    if (feeItemList.Item.User01 == "1")
                    {
                        feeItemList.User01 = "送住院处";
                    }
                    else
                    {
                        feeItemList.User01 = "送药房";
                    }
                }
                else
                {
                    feeItemList.User01 = "送住院处";
                }
                if (feeItemList.Memo != "OLD")
                {
                    feeItemList.User02 = applyBillCode;
                }
                //对已经保存过的退费申请不进行处理
                if (feeItemList.Memo == "OLD")
                {
                    return 1;
                }

                //更新费用表中的可退数量字段
                //如果是药品则更新药品的退药数量，否则更新非药品
                string errMsg = string.Empty;
                int returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                if (returnValue == -1)
                {
                    this.Err = errMsg;
                    return -1;
                }

                //临时项目赋予退费申请号
                feeItemListTemp.User02 = applyBillCode;

                //如果是药品并且已经摆药，则调用退药申请；否则调用退费申请。
                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    #region 已摆药情况
                    //退药申请,使用数据库中取得的实体和用户操作的数量
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    {
                        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    }

                    feeItemListTemp.Item.Memo = feeItemList.Item.Memo;

                    if (this.PharmarcyManager.ApplyOutReturn(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "退药申请失败!" + this.PharmarcyManager.Err;
                        return -1;
                    }

                    #endregion  //end 已摆药情况

                }
                else//对于非药品和未摆药的药品，直接做退费申请
                {
                    #region 未摆药情况
                    //使用数据库中取得的实体和用户操作的数量
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    if (feeItemList.FTRate.ItemRate != 0)
                    {
                        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    }

                    if (this.returnMgr.Apply(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "插入退费申请失败!" + this.returnMgr.Err;
                        return -1;
                    }


                    //没有摆药的药品在退费申请的同时，作废摆药申请
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        //取摆药申请记录，判断其状态是否发生并发。（不在CancelApplyOut中判断并发是因为有些收费后的医嘱没有发送到药房，不存在摆药申请记录）
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (applyOut == null)
                        {
                            this.Err = "获得申请信息出错!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //如果取到的实体ID为""，则表示医嘱并未发送。未发送的医嘱不允许退费，不然发送时药房会对此退费的项目进行发药。
                        if (applyOut.ID == string.Empty)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】没有发送到药房，请先发送，然后再做退费申请!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //并发
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】可能已被其他操作员退费，请先执行确认退费后，再重试!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //作废摆药申请
                        returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            this.Err = "作废摆药申请出错!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                        if (returnValue == 0)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】已摆药，请重新检索数据" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //如果是部分退费(用户退药的数量小于费用表中的可退数量),要对剩余的药品做摆药申请.
                        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                        {
                            //取收费对应的摆药申请记录
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                this.Err = "获得申请信息出错!" + this.PharmarcyManager.Err;
                                return -1;
                            }
                            applyOutTemp.RecipeNO = feeItemList.RecipeNO;
                            applyOutTemp.SequenceNO = feeItemList.SequenceNO;//用扩展字段保存原始处方内流水号
                            applyOutTemp.Operation.ApplyOper.OperTime = quitFeeDateTime;
                            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态

                            applyOutTemp.ID = "";
                            //将剩余数量发送摆药申请  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                            {
                                this.Err = "重新插入发药申请出错!" + this.PharmarcyManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion //end 未摆药情况
                }

                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                {
                    ArrayList patientDrugStorageList = this.PharmarcyManager.QueryStorageList(patientInfo.ID, feeItemListTemp.Item.ID);
                    if (patientDrugStorageList == null)
                    {
                        this.Err = "判断是否存在患者库存时出错!" + this.PharmarcyManager.Err;
                        return -1;
                    }
                    //对患者库存进行清零操作
                    if (patientDrugStorageList.Count > 0)
                    {
                        FS.HISFC.Models.Pharmacy.StorageBase storageBase = patientDrugStorageList[0] as FS.HISFC.Models.Pharmacy.StorageBase;
                        storageBase.Quantity = -storageBase.Quantity;
                        storageBase.Operation.Oper.ID = this.inpatientManager.Operator.ID;
                        storageBase.PrivType = "AAAA";	//记录住院退费标记
                        if (storageBase.ID == string.Empty)
                        {
                            storageBase.ID = applyBillCode;
                            storageBase.SerialNO = 0;
                        }

                        if (this.PharmarcyManager.UpdateStorage(storageBase) == -1)
                        {
                            this.Err = "更新患者库存出错!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region 直接退费
                //如果草药付数没有赋值,默认赋值为1
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }

                if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                {
                    #region 药品已发药 形成退药申请

                    //向退费单中填写记录
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug && feeItemListTemp.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged)
                    {
                        if (feeItemList.Item.User01 == "1")
                        {
                            feeItemList.User01 = "送住院处";
                        }
                        else
                        {
                            feeItemList.User01 = "送药房";
                        }
                    }
                    else
                    {
                        feeItemList.User01 = "送住院处";
                    }
                    if (feeItemList.Memo != "OLD")
                    {
                        feeItemList.User02 = applyBillCode;
                    }
                    //对已经保存过的退费申请不进行处理
                    if (feeItemList.Memo == "OLD")
                    {
                        return 1;
                    }

                    //更新费用表中的可退数量字段
                    //如果是药品则更新药品的退药数量，否则更新非药品
                    string errMsg = string.Empty;
                    int returnValue = UpdateNoBackQty(feeItemList, ref errMsg);
                    if (returnValue == -1)
                    {
                        this.Err = errMsg;
                        return -1;
                    }

                    //临时项目赋予退费申请号
                    feeItemListTemp.User02 = applyBillCode;
                    feeItemListTemp.ExecOrder.DateUse = feeItemList.ExecOrder.DateUse; //用药时间 
                    feeItemListTemp.Order.Usage.ID = feeItemList.Order.Usage.ID;//用法
                    feeItemListTemp.Order.Usage.Name = feeItemList.Order.Usage.Name;
                    feeItemListTemp.Order.Frequency.ID = feeItemList.Order.Frequency.ID;
                    feeItemListTemp.Order.Frequency.Name = feeItemList.Order.Frequency.Name;
                    feeItemListTemp.Order.DoseOnce = feeItemList.Order.DoseOnce;
                    feeItemListTemp.Order.DoseUnit = feeItemList.Order.DoseUnit;



                    //退药申请,使用数据库中取得的实体和用户操作的数量
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;
                    if (feeItemListTemp.StockOper.Dept.ID == string.Empty)
                    {
                        feeItemListTemp.StockOper.Dept.ID = feeItemListTemp.ExecOper.Dept.ID;
                    }
                    if (this.PharmarcyManager.ApplyOutReturn(patientInfo, feeItemListTemp, quitFeeDateTime) == -1)
                    {
                        this.Err = "退药申请失败!" + this.PharmarcyManager.Err;
                        return -1;
                    }

                    #endregion

                    msg += feeItemListTemp.Item.Name + "\n";
                }
                else//对于非药品和未摆药的药品，直接做退费
                {
                    //使用数据库中取得的实体和用户操作的数量
                    feeItemListTemp.Item.Qty = feeItemList.Item.Qty;

                    if (feeItemList.FTRate.ItemRate != 0)
                    {
                        feeItemListTemp.Item.Price = feeItemListTemp.Item.Price * feeItemListTemp.FTRate.ItemRate;
                    }

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemListByQuit = feeItemList.Clone();
                    //直接退费
                    if (this.QuitItem(patientInfo, feeItemListByQuit) == -1)
                    {
                        return -1;
                    }

                    //没有摆药的药品在退费申请的同时，作废摆药申请
                    //if (feeItemListTemp.Item.IsPharmacy)
                    if (feeItemListTemp.Item.ItemType == EnumItemType.Drug)
                    {
                        #region 取摆药申请记录，判断其状态是否发生并发。
                        //（不在CancelApplyOut中判断并发是因为有些收费后的医嘱没有发送到药房，不存在摆药申请记录）

                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (applyOut == null)
                        {
                            this.Err = "获得申请信息出错!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //如果取到的实体ID为""，则表示医嘱并未发送。未发送的医嘱不允许退费，不然发送时药房会对此退费的项目进行发药。
                        if (applyOut.ID == string.Empty)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】没有发送到药房，请先发送，然后再做退费申请!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //并发
                        if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】已被其他操作员退费，请刷新当前数据!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        //作废摆药申请
                        int returnValue = this.PharmarcyManager.CancelApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            this.Err = "作废摆药申请出错!" + this.PharmarcyManager.Err;
                            return -1;
                        }
                        if (returnValue == 0)
                        {
                            this.Err = "项目【" + feeItemListTemp.Item.Name + "】已摆药，请重新检索数据!" + this.PharmarcyManager.Err;
                            return -1;
                        }

                        #endregion

                        //如果是部分退费(用户退药的数量小于费用表中的可退数量),要对剩余的药品做摆药申请.
                        if (feeItemList.Item.Qty < feeItemListTemp.NoBackQty)
                        {
                            #region 部分退费重新发送申请

                            //取收费对应的摆药申请记录
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.PharmarcyManager.GetApplyOut(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                this.Err = "获得申请信息出错!" + this.PharmarcyManager.Err;
                                return -1;
                            }

                            //增加半退重新给申请赋值处方号和处方内流水号
                            applyOutTemp.RecipeNO = feeItemListByQuit.RecipeNO;
                            applyOutTemp.SequenceNO = feeItemListByQuit.SequenceNO;

                            applyOutTemp.Operation.ApplyOper.OperTime = quitFeeDateTime;
                            applyOutTemp.Operation.ApplyQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                            applyOutTemp.Operation.ApproveQty = feeItemListTemp.NoBackQty - feeItemList.Item.Qty;//数量等于剩余数量
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态
                            //将剩余数量发送摆药申请  
                            if (this.PharmarcyManager.ApplyOut(applyOutTemp) != 1)
                            {
                                this.Err = "重新插入发药申请出错!" + this.PharmarcyManager.Err;
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
        /// 更新项目的可退数量
        /// </summary>
        /// <param name="feeItemList">费用基本信息实体</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>成功 1 失败 -1</returns>
        private int UpdateNoBackQty(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, ref string errMsg)
        {
            int returnValue = 0;

            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                //更新费用明细表中的可退数量
                returnValue = this.inpatientManager.UpdateNoBackQtyForDrug(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.Qty, feeItemList.BalanceState);
                if (returnValue == -1)
                {
                    errMsg = Language.Msg("更新药品可退数量出错!") + this.inpatientManager.Err;

                    return -1;
                }
            }
            else
            {
                //更新费用明细表中的可退数量
                returnValue = this.inpatientManager.UpdateNoBackQtyForUndrug(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.Qty, feeItemList.BalanceState);
                if (returnValue == -1)
                {
                    errMsg = Language.Msg("更新非药品可退数量出错!") + this.inpatientManager.Err;

                    return -1;
                }
            }
            //对退药并发进行判断
            if (returnValue == 0)
            {
                errMsg = Language.Msg("项目“") + feeItemList.Item.Name + Language.Msg("”已经被退费，不能重复退费。\r\n\r\n可能原因：已进行手工退费！");

                return -1;
            }

            return 1;
        }
        #endregion

        #region 收取门诊卡费用
        /// <summary>
        /// 收取门诊卡费用
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int SaveAccountCardFee(ref AccountCardFee cardFee)
        {
            if (cardFee == null)
            {
                this.Err = "参数为空！";
                return -1;
            }

            //获得收费时间
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
                // 退费
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
                    // 发票头暂时先保存00
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
                this.Err = "参数为空！";
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
                this.Err = "参数为空！";
                return -1;
            }

            //获得收费时间
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
                // 退费
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
                    // 发票头暂时先保存00
                    this.Err = this.invoiceServiceManager.Err;
                    return -1;
                }
            }

            return iRes;
        }

        #endregion

        //{282BD4C3-4086-4d4c-BE3D-68FC3205E4B7}
        #region 挂号明细费用和支付方式

        public int SaveRegFeeList(ArrayList detailFeeList)
        {
            if (detailFeeList == null)
            {
                this.Err = "参数为空！";
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
                this.Err = "参数为空！";
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

                //本地积分已停用
                //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                //判断该支付方式是否计算积分
                //if (obj.Name.Contains(payMode.Mode_Code.ToString()))
                //{
                //    if (accountPay.UpdateCoupon(regObj.PID.CardNO, payMode.Tot_cost, payMode.InvoiceNo) <= 0)
                //    {
                //        this.Err = "计算积分出错!" + accountPay.Err;
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
            //        this.Err = "计算现金流积分出错!" + errText;
            //        return -1;
            //    }

            //}

            return iRes;
        }

        #endregion

        #region 退费申请业务

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据,附加条件是是否被确认
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <param name="isCharged">是否被确认的数据</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged)
        {
            this.SetDB(returnMgr);
            return returnMgr.QueryReturnApplys(inpatientNO, isCharged);
        }

        #endregion

        #region 身份变更时，对调整记录进行处理 add xf
        /// <summary>
        /// 根据或者新的合同单位，更新患者历史未结算的调整记录
        /// 一般用于身份变更时由公费变更为其他合同单位。
        /// </summary>
        /// <param name="pInfo">患者基本信息</param>
        /// <param name="isPhamacy">是否药品</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRate(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isPhamacy)
        {
            /**
             * 根据患者合同单位获取合同单位的基本比例
             * 1、如果患者新的合同单位是医保或者自费
             * 则原调整记录没有用，删除患者的调整记录
             * 2、如果患者新的合同单位是公费
             * 按照新的合同单位比例更新调整记录。
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //获取新的合同单位的比例
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //更新调整记录
                return inpateintMgr.UpdateAdjustedItem(pInfo.ID, payRate, isPhamacy);
            }
            return 1;
        }

        /// <summary>
        /// 根据或者新的合同单位，更新患者历史未结算的调整记录--FeeInfo
        /// </summary>
        /// <param name="pInfo">患者基本信息</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRateFeeinfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            /**
             * 根据患者合同单位获取合同单位的基本比例
             * 1、如果患者新的合同单位是医保或者自费
             * 则原调整记录没有用，删除患者的调整记录
             * 2、如果患者新的合同单位是公费
             * 按照新的合同单位比例更新调整记录。
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //获取新的合同单位的比例
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //更新调整记录
                return inpateintMgr.UpdateAdustedFeeInfo(pInfo.ID, payRate);
            }
            return 1;
        }

        /// <summary>
        /// 根据或者新的合同单位，更新患者历史未结算的调整记录
        /// 一般用于身份变更时由公费变更为其他合同单位。
        /// </summary>
        /// <param name="pInfo">患者基本信息</param>
        /// <param name="isPhamacy">是否药品</param>
        /// <param name="isPhamacy">是否删除所有调整记录（当新合同单位和原合同单位限额不相等时调用）</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRate(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isPhamacy, bool isDeteleAllAdjustedItem)
        {
            /**
             * 根据患者合同单位获取合同单位的基本比例
             * 0、如果患者新的合同单位是公费且日限额与原合同单位日限额不同时，删除患者的调整记录
             * 1、如果患者新的合同单位是医保或者自费
             * 则原调整记录没有用，删除患者的调整记录
             * 2、如果患者新的合同单位是公费
             * 按照新的合同单位比例更新调整记录。
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (isDeteleAllAdjustedItem)
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);
            }
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedItem(pInfo.ID, isPhamacy);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //获取新的合同单位的比例
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //更新调整记录
                return inpateintMgr.UpdateAdjustedItem(pInfo.ID, payRate, isPhamacy);
            }
            return 1;
        }

        /// <summary>
        /// 根据或者新的合同单位，更新患者历史未结算的调整记录--FeeInfo
        /// </summary>
        /// <param name="pInfo">患者基本信息</param>
        /// <returns></returns>
        public int UpdateAdjustedItemRateFeeinfo(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isDeteleAllAdjustedItem)
        {
            /**
             * 根据患者合同单位获取合同单位的基本比例
                      * 0、如果患者新的合同单位是公费且日限额与原合同单位日限额不同时，删除患者的调整记录
             * 1、如果患者新的合同单位是医保或者自费
             * 则原调整记录没有用，删除患者的调整记录
             * 2、如果患者新的合同单位是公费
             * 按照新的合同单位比例更新调整记录。
             **/
            FS.HISFC.BizLogic.Fee.InPatient inpateintMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            inpateintMgr.SetTrans(this.trans);
            if (isDeteleAllAdjustedItem)
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);
            }
            if (pInfo.Pact.PayKind.ID == "01" || pInfo.Pact.PayKind.ID == "02")
            {
                //删除调整记录
                return inpateintMgr.DeleteAdjustedFeeInfo(pInfo.ID);

            }
            else if (pInfo.Pact.PayKind.ID == "03")
            {
                //获取新的合同单位的比例
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                pactManagment.SetTrans(this.trans);
                PactInfo pactUnitInfo = pactManagment.GetPactUnitInfoByPactCode(pInfo.Pact.ID);

                decimal payRate = pactUnitInfo.Rate.PayRate;
                //更新调整记录
                return inpateintMgr.UpdateAdustedFeeInfo(pInfo.ID, payRate);
            }
            return 1;
        }

        #endregion

        #region 固定费用

        public int DoBedItemFee(ArrayList bedItems,
            FS.HISFC.Models.RADT.PatientInfo patient, int days, DateTime operDate, DateTime chargeDate, FS.HISFC.Models.Base.Bed bed)
        {
            //非药品管理类
            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //合同管理类


            FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //常数管理类


            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            //事务
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
                //床位信息实体
                FS.HISFC.Models.Fee.BedFeeItem bedItem = new FS.HISFC.Models.Fee.BedFeeItem();

                //判断当天有没有已经收取床位费，false即没有收取
                bool feeFlag = inpatientManager.QueryBedFeeByPersonAndFeeDate(patient.ID, patient.FT.PreFixFeeDateTime.ToString());
                if (feeFlag == false && patient.PVisit.PatientLocation.Dept.ID == "1001" && !patient.PID.ID.Contains("L") )//妇产科患者，且非急诊留观
                {
                    for (int row = 0; row < bedItems.Count; row++)
                    {
                        //取待收取的床位信息


                        bedItem = bedItems[row] as FS.HISFC.Models.Fee.BedFeeItem;

                        //如果床位无效，则不进行费用收取


                        if (bedItem.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid) continue;

                        //关闭的床位不收床位费.转科后不释放床位时床位状态设为C . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
                        if (bed.Status.ID.ToString() == "C")
                        {
                            continue;
                        }

                        #region 判断非在院患者(包床W,挂床H,请假R)是否收取该项目  writed by cuipeng  2005-11
                        if (bed.Status.ID.ToString() == "W" || bed.Status.ID.ToString() == "H" || bed.Status.ID.ToString() == "R")
                        {
                            //如果收费项目对于非在院患者不收取费用,则不处理此项目


                            if (bedItem.ExtendFlag == "0")
                            {
                                continue;
                            }
                            else
                            {
                                //中山特殊 由于数据库有设置所以暂时保留


                                //对于包床患者,固定费用收取名称为"陪人费",金额为床位费的2倍.
                                if (bed.Status.ID.ToString() == "W")
                                {
                                    FS.FrameWork.Models.NeuObject obj = constant.GetConstant("FIN_FIXITEM", "BEDWRAP");
                                    //if (obj == null)
                                    //{
                                    //    this.Err = constant.Err;
                                    //    this.WriteErr();
                                    //    continue;
                                    //}

                                    ////取原项目(床位费)单价
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

                                    ////指定收费项目编码(陪人费项目编码)
                                    //bedItem.ID = peiItem.ID;
                                    //bedItem.Name = peiItem.Name;
                                    ////bedItem.ID = obj.Name;

                                    ////单价为床位费的2倍


                                    //bedItem.User01 = (tempItem.Price * 2).ToString();

                                }
                            }
                        }
                        #endregion

                        #region 判断该项目是否和时间有关，比如空调费、取暖费
                        if (bedItem.IsTimeRelation)
                        {
                            //结束日期 >= 起始日期,认为不跨年度
                            if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                            {
                                //如果当前时间不在设置时间范围内，则不收取此项目费用


                                if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                    || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                    continue;//--当前日期在计费有效期外


                            }

                            else
                            { //结束日期 < 起始日期 :跨年度


                                //如果当前时间不在设置时间范围内，则不收取此项目费用


                                if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                    && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                    continue;//--当前日期在计费有效期外


                            }
                        }
                        #endregion

                        #region 对于设置跟婴儿有关的固定费用,根据婴儿是否存在而收费


                        bool isBaby = false;//是否婴儿,默认不是婴儿
                                            //中大五院的婴儿床位费处理放在HisTimeJob中处理，因为母亲和婴儿的费用分开 gumzh
                        if (false)
                        {
                            if (bedItem.IsBabyRelation)
                            {
                                if (patient.BabyCount == 0)
                                    //婴儿不存在,则不收取此项费用
                                    continue;
                                else
                                {
                                    //婴儿存在,每个婴儿收取一份


                                    isBaby = true;
                                    bedItem.Qty = bedItem.Qty * patient.BabyCount;
                                }
                            }
                        }

                        #endregion

                        //计算项目数量,如果为0则默认是1
                        if (bedItem.Qty == 0)
                            bedItem.Qty = 1;
                        //根据用户设置的数量倍数,计算应收取数量
                        bedItem.Qty = bedItem.Qty * days;
                        //如果是数据结构中存在个人床位费的价格，则使用
                        FS.HISFC.Models.Fee.BedFeeItem personFeeItem = this.QueryBedFeeItemForPatient(patient.ID, patient.PVisit.PatientLocation.Bed.ID, bedItem.PrimaryKey);
                        if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                        {
                            bedItem.ID = personFeeItem.ID;
                            bedItem.Name = personFeeItem.Name;
                            bedItem.Qty = personFeeItem.Qty;
                        }

                        //取收费项目实体信息
                        FS.HISFC.Models.Fee.Item.Undrug undrug = item.GetValidItemByUndrugCode(bedItem.ID);
                        if (undrug == null)
                        {
                            this.Err = item.Err;
                            continue;
                        }
                        //计算项目价格,根据合同单位和项目计算价格
                        decimal price = 0;
                        decimal orgPrice = 0;

                        if (this.GetPriceForInpatient(patient, undrug, ref price, ref orgPrice) == -1)
                        //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                        {
                            this.Rollback();
                            this.Err = "获取项目:" + undrug.ID + "的价格时出错!" + pactMgr.Err;
                            return -1;
                        }

                        if (personFeeItem != null && string.IsNullOrEmpty(personFeeItem.PrimaryKey) == false)
                        {
                            price = personFeeItem.Price;
                        }

                        //取得的价格不为0,则使用取后的价格
                        if (price != 0)
                        {
                            undrug.Price = price;
                            undrug.DefPrice = orgPrice;
                        }
                        else
                        {
                            undrug.DefPrice = undrug.Price;
                        }

                        //包床单价固定为床位费的2倍. writed by cuipeng 2005-11
                        if (bed.Status.ID.ToString() == "W")
                        {
                            //undrug.Price = FS.FrameWork.Function.NConvert.ToDecimal(bedItem.User01);
                            //包床的费用明细名称增加"包床" gumzh
                            undrug.Name += "-包床费";
                        }

                        //计费单价为0, 不需要计费
                        if (undrug.Price == 0)
                        {
                            this.Err = "计费单价为0:" + undrug.Name;
                            continue;
                        }


                        undrug.Qty = bedItem.Qty;
                        //医保患者接口
                        //中山一没有需要处理的
                        //实体赋值
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
                            patient.PVisit.AdmittingDoctor.ID = "日计费";

                        //feeItem.Order.ReciptDoctor.ID =
                        feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
                        feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                        //feeItem.IsBrought = "";
                        feeItem.ChargeOper.ID = "日计费";
                        feeItem.ChargeOper.OperTime = chargeDate;
                        feeItem.FeeOper.ID = "日计费";
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
                        //---------------------------公费床位超标调整0818------------------------
                        #region 公费床位超标调整
                        if (patient.Pact.PayKind.ID == "03")
                        {
                            feeItem.FT.OwnCost = 0;//这句一定要加，区别医保收取固定费用后在调整的做法
                                                   //床位限额
                            decimal BedLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.BedLimitCost * days, 2);
                            //监护床位限额
                            decimal IcuLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.AirLimitCost * days, 2);

                            /*字典表中TYPE为BEDLIMITMINFEE
                            CODE为1为普通床，NAME中存的是普通床最小费用CODE
                            CODE为2为监护床，NAME中存的是监护床最小费用CODE
                            */
                            FS.FrameWork.Models.NeuObject conBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "1");
                            string bedMinFeeCode = "";
                            if (conBedMinFee != null)
                            {
                                if (string.IsNullOrEmpty(conBedMinFee.Name))
                                {
                                    this.Err = "请在字典表中维护type为BEDLIMITMINFEE,CODE=1,NAME=普通床最小费用代码！";
                                }
                                bedMinFeeCode = conBedMinFee.Name;//普通床最小费用代码
                            }

                            FS.FrameWork.Models.NeuObject conICUBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "2");
                            string icuBedMinFeeCode = "";
                            if (conICUBedMinFee != null)
                            {
                                if (string.IsNullOrEmpty(conICUBedMinFee.Name))
                                {
                                    this.Err = "请在字典表中维护type为BEDLIMITMINFEE,CODE=2,NAME=监护床最小费用代码！";
                                }
                                icuBedMinFeeCode = conICUBedMinFee.Name;//监护床最小费用代码
                            }
                            ////判断当天是否已经收过空调费
                            //decimal AirFee = 0;
                            //DateTime FeeBegin = new DateTime(operDate.Year, 10, 26, 0, 0, 0);
                            //DateTime FeeEnd = new DateTime(operDate.Year, 4, 26, 0, 0, 0);
                            //if (operDate > FeeBegin || operDate < FeeEnd)
                            //{
                            //    if (this.inpatientManager.GetAirFee(patient.ID, ref AirFee) > 0)//字典表维护空调费项目type为AIRFEEITEM
                            //    {
                            //        BedLimit = BedLimit - AirFee;
                            //    }
                            //}

                            FS.FrameWork.Models.NeuObject billObj = constant.GetConstant("BILLPACT", patient.Pact.ID);

                            #region 判断超标 处理费用
                            FS.HISFC.Models.Base.FTRate Rate = this.ComputeFeeRate(patient.Pact.ID, feeItem.Item);
                            if (Rate == null)
                            {
                                return -1;
                            }
                            feeItem.User01 = "1";//用作判断标记在FeeManager中不重新调用计算比例函数

                            bool computeLimit = true;//项目是否计算入限额

                            if (billObj != null && billObj.ID.Length >= 0 && billObj.Name == "市公费")
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
                                    #region 普通床超标处理
                                    if (patient.FT.BedOverDeal == "1")
                                    {//超标自理
                                     //不超标
                                        if (feeItem.FT.TotCost <= BedLimit)
                                        {
                                            BedLimit = BedLimit - feeItem.FT.TotCost;
                                        }
                                        else
                                        {//超标部分转为自费
                                            feeItem.FT.OwnCost = feeItem.FT.TotCost - BedLimit;
                                            BedLimit = 0;
                                        }
                                    }
                                    else if (patient.FT.BedOverDeal == "2")
                                    {
                                        ////超标不计，报销限额内，剩下的舍掉
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
                                    #region 监护床超标处理


                                    //调用床位收费函数并且最小费用是010的一定是监护床,如果不是没法处理.
                                    //监护床相关床位费也应该维护成010,否则也没法处理

                                    //超标自理
                                    if (patient.FT.BedOverDeal == "1")
                                    {
                                        if (IcuLimit >= feeItem.FT.TotCost)
                                        {
                                            //监护床标准大于监护床费，不超标								
                                            IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                        }
                                        else
                                        {
                                            //超标，超标部分自费
                                            feeItem.FT.OwnCost = feeItem.FT.TotCost - IcuLimit;
                                            IcuLimit = 0;
                                        }
                                    }
                                    else if (patient.FT.BedOverDeal == "2")
                                    {//超标不计，报销限额内，剩下的舍掉
                                     //超标
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

                        if (string.IsNullOrEmpty(feeItem.Order.ID))//没有付值医嘱流水号
                        {
                            feeItem.Order.ID = orderManager.GetNewOrderID();
                            if (feeItem.ID == null || feeItem.Order.ID == string.Empty)
                            {
                                this.Err = "获得医嘱流水号出错!";
                                return -1;
                            }
                        }

                        if (string.IsNullOrEmpty(feeItem.ExecOrder.ID))
                        {
                            feeItem.ExecOrder.ID = orderManager.GetNewOrderExecID();
                            if (string.IsNullOrEmpty(feeItem.ExecOrder.ID))
                            {
                                this.Err = "获得医嘱执行流水号出错!";
                                return -1;
                            }

                        }

                        if (this.FeeItem(patient, feeItem) == -1)
                        {
                            this.Rollback();
                            this.Err = "调用住院收费业务层出错!" + this.Err;
                            return -1;
                        }
                        alFeeInfo.Add(feeItem);
                    }

                    //发送消息
                    #region HL7消息发送
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
                }


                if (inpatientManager.UpdateFixFeeDateByPerson(patient.ID, patient.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Rollback();
                    this.Err = "更新患者上次收取费用时间时出错!";
                    return -1;
                }
     

                this.Commit();
            }
            catch (Exception e)
            {
                this.Rollback();
                this.Err = "姓名为:" + patient.PVisit.Name + "住院流水号为:" +
                    patient.ID + "收取固定费用失败!" + e.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 求患者费用比例
        /// </summary>
        /// <param name="PactID">合同单位代码</param>
        /// <param name="item">药品费药品信息</param>
        /// <returns>失败null；成功 FS.HISFC.Models.Fee.FtRate</returns>
        private FS.HISFC.Models.Base.FTRate ComputeFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

            PactItemRate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            try
            {
                //项目
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);
                if (ObjPactItemRate == null)
                {
                    //最小费用
                    ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.MinFee.ID);
                    if (ObjPactItemRate == null)
                    {
                        //取合同单位的比例
                        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                        PactManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.Err = "获得合同单位信息出错，" + PactManagment.Err;
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
                            this.Err = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                    }
                }
            }
            catch
            {
                this.Err = "获得合同单位信息出错";
                return null;
            }

            return ObjPactItemRate.Rate;
        }

        /// <summary>
        ///  计算总费用的各个组成部分的值
        /// </summary>
        /// <param name="ItemList"></param>
        /// <param name="rate">各部分之间的比例</param>
        /// <returns>-1失败，0成功</returns>
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

        #region 计算医保余额
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
                //更新医保住院主表
                if (-1 == this.inpatientManager.UpdateInMainInfo(patient.ID, ft))
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 计算患者费用汇总信息（处理起付线和报销比例）
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="ft">根据fin_ipb_feeinfo 来获取费用信息</param>
        /// <returns></returns>
        public int ComputePatientSumFee(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FS.HISFC.Models.Base.FT ft)
        {
            //暂时用广州医保（2） 来查询起伏线
            ArrayList alInsurancedeal = this.inpatientManager.QueryInsurancedeal("2", patientInfo.PVisit.MedicalType.ID);
            if (alInsurancedeal != null && alInsurancedeal.Count > 0)
            {
                foreach (FS.HISFC.Models.SIInterface.Insurance insurance in alInsurancedeal)
                {
                    //满足区间条件
                    if (ft.PubCost > insurance.BeginCost && ft.PubCost <= insurance.EndCost)
                    {
                        //按区间比例
                        decimal dtKJZtot = ft.PubCost - insurance.BeginCost;
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(dtKJZtot * insurance.Rate, 2);//个人自付部分= （总费用-自费-乙类自付-起伏线）* 自付比 
                        ft.PubCost = ft.TotCost - ft.OwnCost - ft.PayCost - ft.SupplyCost;//记账金额
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.DefTotCost = insurance.BeginCost;//起伏线
                        ft.RealCost = ft.SupplyCost + ft.OwnCost + ft.PayCost + insurance.BeginCost;//实付金额 = 个人自付部门+纯自费+乙类自付+起伏线
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

        #region 套餐消费

        /// <summary>
        /// 套餐消费函数 
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CostPackageDetail(ArrayList details, string InvoiceNO, ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "套餐明细为空或者发票号为空！";
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

                    //更新原有记录的可用数量和确认数量
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }

                    if (oldDetail.RtnQTY < detail.Item.Qty)
                    {
                        ErrInfo = detail.Item.Name + "：此类项目可消费数量不足！";
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
        /// 套餐消费函数 
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCostPackageDetail(ArrayList details, string InvoiceNO,Register r ,ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "套餐明细为空或者发票号为空！";
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

                    //更新原有记录的可用数量和确认数量
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }

                    if (oldDetail.RtnQTY < detail.Item.Qty)
                    {
                        ErrInfo = detail.Item.Name + "：此类项目可消费数量不足！";
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
        /// 套餐消费函数 
        /// {6974FE57-7E0F-4c8f-AFC8-675CA7536C61}{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCostPackageDetailByType(ArrayList details, string InvoiceNO, FS.HISFC.Models.RADT.PatientInfo  r, string costtype, string costclinic, ref string ErrInfo)
        {
            if (details == null || string.IsNullOrEmpty(InvoiceNO))
            {
                ErrInfo = "套餐明细为空或者发票号为空！";
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
                        //更新原有记录的可用数量和确认数量
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
                        //更新原有记录的可用数量和确认数量
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);

                        if (oldDetail == null)
                        {
                            ErrInfo = this.PackageDetailMgr.Err;
                            return -1;
                        }

                        if (oldDetail.RtnQTY < detail.Item.Qty)
                        {
                            ErrInfo = detail.Item.Name + "：此类项目可消费数量不足！";
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
        /// 套餐消费取消函数门诊{351D714B-0153-483e-B1AB-697C5A9A9BAD}
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
                    //更新原有记录
                    cost.Cancel_Flag = "1";
                    cost.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.CancelTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();

                    if (this.PackageCostMgr.UpdateByCostType(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //插入负记录
                    cost.Trans_Type = "2";
                    cost.Amount = -cost.Amount;
                    cost.Tot_Cost = -cost.Tot_Cost;
                    cost.Real_Cost = -cost.Real_Cost;
                    cost.Gift_cost = -cost.Gift_cost;
                    cost.Etc_cost = -cost.Etc_cost;

                    cost.Balance_flag = "";
                    cost.Balance_no = "";
                    //操作时间 {99F3E6D7-1287-46db-8301-BDF640DC7A74}
                    cost.OperTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();
                    if (this.PackageCostMgr.Insert(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //更新原有记录的可用数量和确认数量
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(cost.PackageClinic, "1", cost.DetailSeq);

                    if (oldDetail == null)
                    {
                        ErrInfo = this.PackageDetailMgr.Err;
                        return -1;
                    }
                    if (oldDetail.Cancel_Flag != "0"&&!string.IsNullOrEmpty(cost.NewPackageClinic)&&!string.IsNullOrEmpty(cost.NewDetailSeq)) //套餐已退费
                    {
                        oldDetail = this.PackageDetailMgr.QueryDetailByClinicSeq(cost.NewPackageClinic, "1", cost.NewDetailSeq);
                    }
                    if (oldDetail.ConfirmQTY < qty)
                    {
                        ErrInfo = "更新套餐记录表处错";
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
                ///更新套餐主表的状态为未使用
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
        /// 套餐消费取消函数(住院的套餐){351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// 
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int NewCancelCostAndPackageDetail(string CostInvoiceNO, string costype, ref  string ErrInfo)
        {
            if (costype == "ZY")
            {
                // 查出套餐主表未被退费的记录{351D714B-0153-483e-B1AB-697C5A9A9BAD}
                HISFC.BizLogic.MedicalPackage.Fee.Package feepackmanager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
                ArrayList alcostPack = feepackmanager.QueryByCostInvoiceNoNoRtn(CostInvoiceNO);
                foreach (var item in alcostPack)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package pack = item as FS.HISFC.Models.MedicalPackage.Fee.Package;//套餐主表
                    if (pack != null)
                    {
                        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail detailManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
                        //根据套餐主表查出套餐明细记录
                        ArrayList packdetaillist = detailManager.QueryDetailByClinicNO(pack.ID, "0");
                        if (packdetaillist.Count != 0)
                        {
                            //回滚明细记录的确认数量和可退数量
                            if (CancelPackageDetailNum(packdetaillist, ref ErrInfo) <= 0)
                            {
                                return -1;
                            }
                        }
                    }
                }
                //插入套餐消费记录负记录
                if (CancelPackageCost(CostInvoiceNO, "ZY", ref ErrInfo) <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 套餐明细数量回滚（住院套餐）{351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CancelPackageDetailNum(ArrayList details,ref string ErrInfo)
        {
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                //更新原有记录的可用数量和确认数量
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
                    ErrInfo = "更新套餐记录表处错";
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
        /// 套餐消费表记录取消(负记录){351D714B-0153-483e-B1AB-697C5A9A9BAD}
        /// {DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        /// </summary>
        /// <param name="details"></param>
        /// <param name="InvoiceNO"></param>
        /// <returns></returns>
        public int CancelPackageCost(string CostInvoiceNO,string costtype, ref string ErrInfo)
        {
            try
            {
                ArrayList tmp = this.PackageCostMgr.QueryByInvoiceNOByType(CostInvoiceNO, "0", costtype);//套餐消费记录
                if (tmp == null)
                {
                    ErrInfo = this.PackageCostMgr.Err;
                    return -1;
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost in tmp)
                {
                    decimal qty = cost.Amount;
                    //更新原有记录
                    cost.Cancel_Flag = "1";
                    cost.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    cost.CancelTime = this.PackageDetailMgr.GetDateTimeFromSysDateTime();

                    if (this.PackageCostMgr.UpdateByCostType(cost) < 0)
                    {
                        ErrInfo = this.PackageCostMgr.Err;
                        return -1;
                    }

                    //插入负记录
                    cost.Trans_Type = "2";
                    cost.Amount = -cost.Amount;
                    cost.Tot_Cost = -cost.Tot_Cost;
                    cost.Real_Cost = -cost.Real_Cost;
                    cost.Gift_cost = -cost.Gift_cost;
                    cost.Etc_cost = -cost.Etc_cost;

                    cost.Balance_flag = "";
                    cost.Balance_no = "";
                    //操作时间 {99F3E6D7-1287-46db-8301-BDF640DC7A74}
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

        #region 查询消费内容
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
                    string msg = "套餐编码：" + dt.Rows[i]["PACKAGE_ID"].ToString() + "套餐名称：" + dt.Rows[i]["PACKAGE_NAME"].ToString();
                    packages.Add(msg);
                }
            }

            return packages;

        }
        
        #endregion

    }

    /// <summary>
    /// 医嘱执行档排序
    /// </summary>
    public class ExecOrderCompare : IComparer
    {
        #region IComparer 成员

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
