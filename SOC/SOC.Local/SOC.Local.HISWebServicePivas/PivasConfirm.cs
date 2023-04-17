using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Management;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;

namespace SOC.Local.HISWebServiceTest
{
    public class PivasConfirm
    {
        #region 变量
        /// <summary>
        /// 费用公共业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 如出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 住院收费业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 人员信息业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 医嘱管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 非药品业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 科室业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department departmentManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 患者基本信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 医嘱基本信息实体
        /// </summary>
        private FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
        /// <summary>
        /// 药品医嘱执行档信息
        /// </summary>
        private FS.HISFC.Models.Order.ExecOrder execDrugOrder = new FS.HISFC.Models.Order.ExecOrder();
        /// <summary>
        /// 非药品医嘱执行档信息
        /// </summary>
        private FS.HISFC.Models.Order.ExecOrder execUndrugOrder = new FS.HISFC.Models.Order.ExecOrder();
        /// <summary>
        /// 患者费用信息实体
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
        /// <summary>
        /// 执行科室信息实体
        /// </summary>
        private FS.HISFC.Models.Base.Department dept = new Department();
        /// <summary>
        /// 住院患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }
        /// <summary>
        /// 处理适住院应症
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient IAdptIllnessInPatient = null;
        /// <summary>
        /// 是否判断欠费，如何提示(Y：判断欠费，不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费)
        /// </summary>
        private FS.HISFC.Models.Base.MessType messtype = FS.HISFC.Models.Base.MessType.M;
        /// <summary>
        /// 是否判断数量
        /// </summary>
        private bool isJudgeQty = true;
        /// <summary>
        /// 医嘱基本信息实体
        /// </summary>
        public FS.HISFC.Models.Order.Inpatient.Order Order
        {
            set
            {
                this.order = value;
            }
            get
            {
                return this.order;
            }
        }
        /// <summary>
        /// 患者费用信息实体
        /// </summary>
        public FS.HISFC.Models.Fee.Item.Undrug Undrug
        {
            set
            {
                this.undrug = value;
            }
            get
            {
                return this.undrug;
            }
        }
        /// <summary>
        /// 执行科室信息实体
        /// </summary>
        public FS.HISFC.Models.Base.Department Dept
        {
            set
            {
                this.dept = value;
            }
            get
            {
                return this.dept;
            }
        }
        /// <summary>
        /// 本次收费项目集合
        /// </summary>
        private List<FeeItemList> feeItemCollection = new List<FeeItemList>();
        public List<FeeItemList> FeeItemCollection
        {
            get
            {
                return this.feeItemCollection;
            }
        }
        /// <summary>
        /// 开方医生
        /// </summary>
        private string recipeDoctCode;
        public string RecipeDoctCode
        {
            get
            {
                return this.recipeDoctCode;
            }
            set
            {
                this.recipeDoctCode = value;
            }
        }
        /// <summary>
        /// 处方科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject recipeDept = null;
        public FS.FrameWork.Models.NeuObject RecipeDept
        {
            set
            {
                this.recipeDept = value;
            }
            get
            {
                return this.recipeDept;
            }
        }
        /// <summary>
        /// 是否验证及时停用标记
        /// </summary>
        private bool isJudgeValid = true;
        public bool IsJudgeValid
        {
            get
            {
                return this.isJudgeValid;
            }
            set
            {
                this.isJudgeValid = value;
            }
        }
        /// <summary>
        /// 费用来源
        /// </summary>
        private FS.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FTSource();
        public FS.HISFC.Models.Fee.Inpatient.FTSource FTSource
        {
            set
            {
                ftSource = value;
            }
        }
        #endregion

        #region 申请退费业务
        /// <summary>
        /// 保存申请退费信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="execOrderID"></param>
        /// <param name="noBackQty"></param>
        /// <param name="errInfo"></param>
        /// <returns>成功 1 失败 -1</returns>
        public int ApplyQuit(string inPatientNo, string itemType, string execOrderID,decimal noBackQty, ref string errInfo)
        {
            //验证患者合法性
            if (!this.IsPatientValid(inPatientNo, ref errInfo))
            {
                return -1;
            }
            //获得退费项目
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = this.GetConfirmItem(inPatientNo, itemType, execOrderID, noBackQty, ref errInfo);
            string msg = string.Empty;
            if (feeItemList == null)
            {
                errInfo = "没有费用可退，或您输入的申退数量大于可退数量!";

                return -1;
            }
            else//处理退费数据
            {
                //当前时间
                DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

                //获得退费申请号
                string applyBillCode = this.GetApplyBillCode(ref errInfo);
                if (applyBillCode == null)
                {
                    return -1;
                }

                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                feeItemList.User03 = nowTime.ToString("yyyy-MM-dd");

                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    if (feeItemList.ConfirmedQty > 0)
                    {
                        string key = (string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.ID) + feeItemList.ExecOrder.ID + feeItemList.TransType.ToString();
                        if (dictionary.ContainsKey(key) == false)
                        {
                            msg += string.Format(Environment.NewLine + @"{1}【{0}  {2}{3}】", string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.Name : feeItemList.UndrugComb.Name, FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID), feeItemList.ConfirmedQty, feeItemList.Item.PriceUnit);
                            dictionary[key] = msg;
                        }
                    }
                }
                //退费申请
                if (this.feeIntegrate.QuitFeeApply(this.patientInfo, feeItemList, true, applyBillCode, nowTime, ref errInfo) == -1)
                {
                    errInfo = "退费失败!" + this.feeIntegrate.Err;

                    return -1;
                }
            }
            errInfo = "申请成功!" + (string.IsNullOrEmpty(msg) ? "" : @"请通知以下科室进行退费确认：" + Environment.NewLine + Environment.NewLine + msg);

            return 1;
        }
        /// <summary>
        /// 获得退费项目
        /// </summary>
        /// <param name="inPatientNo">住院流水号</param>
        /// <param name="itemType">类型 1药品 2非药品</param>
        /// <param name="execOrderID">医嘱执行档单号</param>
        /// <param name="noBackQty">申退数量</param>
        /// <returns>成功 已退项目集合 失败 null</returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList GetConfirmItem(string inpatientNO, string itemType, string execOrderID,decimal noBackQty,ref string errInfo)
        {
            //根据住院流水号、处方号、处方流水号获取药品退费项目
            FeeItemList feeItem = null;
            //药品
            if ("1".Equals(itemType))
            {
                ArrayList feeItemList = this.inpatientManager.GetItemListByExecSQN(this.PatientInfo.ID, execOrderID, EnumItemType.Drug);
                if (feeItemList.Count<=0)
                {
                    return new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                }
                feeItem = feeItemList[0] as FeeItemList;
            }
            //非药品
            else if ("2".Equals(itemType))
            {
                ArrayList feeItemList = this.inpatientManager.GetItemListByExecSQN(this.PatientInfo.ID, execOrderID, EnumItemType.UnDrug);
                if (feeItemList.Count <= 0)
                {
                    return new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                }
                feeItem = feeItemList[0] as FeeItemList;
            }
            if (feeItem != null && feeItem.NoBackQty > 0)
            {
                //申退数量不能大于可退数量
                if (feeItem.NoBackQty < noBackQty)
                {
                    return new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                }
                feeItem.Item.Qty = noBackQty;
                feeItem.NoBackQty = 0;
                feeItem.FT.TotCost = feeItem.Item.Price * feeItem.Item.Qty / feeItem.Item.PackQty;
                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                feeItem.IsNeedUpdateNoBackQty = true;
                if ("1".Equals(itemType))
                {
                    feeItem.ExecOper.Dept.User01 = "1";//是否需要申请
                }
            }
            return feeItem;
        }
        /// <summary>
        /// 验证合法性
        /// </summary>
        /// <returns>成功 True 失败 false</returns>
        protected virtual bool IsPatientValid(string inpatientNo, ref string errInfo)
        {
            if (inpatientNo == null || inpatientNo == string.Empty)
            {
                errInfo = "该患者不存在!请验证后输入";
                return false;
            }

            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(inpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                errInfo = "该患者不存在!请验证后输入";
                return false;
            }
            this.patientInfo = patientTemp;
            return true;
        }
        /// <summary>
        /// 获得退费申请号
        /// </summary>
        /// <param name="errInfo">错误信息</param>
        /// <returns>成功  获得退费申请号 失败 null</returns>
        public string GetApplyBillCode(ref string errInfo)
        {
            string applyBillCode = string.Empty;

            applyBillCode = this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                errInfo = "获取退费申请方号出错!";
                return null;
            }

            return applyBillCode;
        }
        #endregion

        #region 确认收费业务
        /// <summary>
        /// 保存收费信息
        /// </summary>
        /// <param name="execOrderID">医嘱执行编号</param>
        /// <param name="itemCode">非药品编号</param>
        /// <param name="itemQty">非药品数量</param>
        /// <param name="doctCode">开方医生编号</param>
        /// <param name="execDeptCode">执行科室编号</param>
        /// <param name="comBoNo">组合序号</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>-1 失败,1 成功</returns>
        public int SaveChargeInfo(string execOrderID, string itemCode, decimal itemQty, string doctCode, string execDeptCode, string comBoNo, ref string errInfo)
        {
            IAdptIllnessInPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient;
            //医嘱执行信息、非药品信息、非药品数量、开方医生、处方科室是否合法
            if (!this.IsValid(execOrderID, itemCode, itemQty, doctCode, execDeptCode, ref errInfo))
            {
                return -1;
            }
            //判断患者账户状态
            if (this.inpatientManager.GetStopAccount(this.patientInfo.ID) == "1")
            {
                errInfo = "该患者处于封帐状态，不能进行收费！";

                return -1;
            }
            //判断患者当前费用状态
            if (this.feeIntegrate.IsPatientLackFee(this.patientInfo))
            {
                //欠费判断
                if (this.messtype == MessType.M)
                {
                    //errInfo = "此患者已欠费，是否继续？";

                    //return -1;
                }
                else if (this.messtype == MessType.Y)
                {
                    errInfo = "此患者已欠费，不允许继续收费!";

                    return -1;
                }
            }
           
            #region 保存医嘱信息
            int saveOrderResult = this.SavaOrderInfo(execOrderID, comBoNo, ref errInfo);
            if (saveOrderResult < 0)
            {
                return -1;
            }
            #endregion

            #region 处理费用信息
            ArrayList firstInputFeeItemlist = new ArrayList();
            //保存本次收费项目明细信息
            this.feeItemCollection = new List<FeeItemList>();
            //操作时间
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //费用信息实体
            FeeItemList feeItemList = new FeeItemList();
            //项目信息赋值
            int returnValue = this.SetItem(PayTypes.Balanced, operTime, ref feeItemList, ref errInfo);
            //如果获得的项目信息为空,不处理
            if (returnValue <= 0)
            {
                return -1;
            }
            //新录入项目
            firstInputFeeItemlist.Add(feeItemList.Clone());
            this.feeItemCollection.Add(feeItemList.Clone());
            //调用整体收费函数,收取第一次录入的费用
            if (this.feeIntegrate.FeeItem(this.patientInfo, ref firstInputFeeItemlist) == -1)
            {
                errInfo = this.feeIntegrate.Err;
                this.feeIntegrate.MedcareInterfaceProxy.Disconnect();

                return -1;
            }
            if (IAdptIllnessInPatient != null)
            {
                ArrayList feeList = new ArrayList(feeItemCollection);
                int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                if (resultValue < 0)
                {
                    return -1;
                }
            }
            this.feeIntegrate.MedcareInterfaceProxy.CloseAll();
            #endregion

            errInfo = "收费成功!您的执行档单号为：" + feeItemList.ExecOrder.ID;

            return 1;
        }
        /// <summary>
        /// 保存医嘱信息
        /// </summary>
        /// <param name="execOrderID">医嘱执行编号</param>
        /// <param name="comBoNo">组合序号</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1 失败,0 成功</returns>
        public int SavaOrderInfo(string execOrderID, string comBoNo, ref string errInfo)
        {
            #region 非药品执行档医嘱信息
            //非药品执行单流水号
            string execId = this.orderMgr.GetNewOrderExecID();
            //住院科室代码
            order.InDept = this.execDrugOrder.Order.InDept;
            //患者信息(住院流水号、住院病历号等)
            order.Patient = this.PatientInfo;
            //医嘱流水号
            order.ID = this.orderMgr.GetNewOrderID();
            //开立医生
            FS.FrameWork.Models.NeuObject recipeDoct = this.personManager.GetPersonByID(this.RecipeDoctCode) as FS.FrameWork.Models.NeuObject;
            //医嘱开立时间
            order.MOTime = this.execDrugOrder.Order.MOTime;
            //是否婴儿医嘱
            order.IsBaby = this.execDrugOrder.Order.IsBaby;
            //婴儿序号
            order.BabyNO = this.execDrugOrder.Order.BabyNO;
            //是否附材
            order.IsSubtbl = true;
            //非药品项目信息
            order.Item = this.Undrug;
            //此处存可退费数量
            order.Item.User03 = this.Undrug.Qty.ToString();
            //开立科室
            order.ReciptDept = this.RecipeDept;
            //开单医生
            order.ReciptDoctor = recipeDoct;
            //执行科室
            order.ExeDept = this.Dept;
            //项目数量
            order.Qty = this.Undrug.Qty;
            //总量单位
            order.Unit = this.execDrugOrder.Order.Unit;
            //处方号
            order.ReciptNO = this.execDrugOrder.Order.ReciptNO;
            //处方流水号
            order.SequenceNO = 0;
            //项目频次编码
            order.Frequency = this.execDrugOrder.Order.Frequency;
            //分解时间
            order.CurMOTime = this.execDrugOrder.Order.CurMOTime;
            //开始时间
            order.BeginTime = this.execDrugOrder.Order.BeginTime;
            //结束时间
            order.EndTime = this.execDrugOrder.Order.EndTime;
            //医嘱状态
            order.Status = this.execDrugOrder.Order.Status;
            //组合序号
            if (!string.IsNullOrEmpty(comBoNo))
            {
                order.Combo.ID = comBoNo;
            }
            //医嘱类型
            order.OrderType.IsDecompose = this.execDrugOrder.Order.OrderType.IsDecompose;
            order.OrderType.ID = this.execDrugOrder.Order.OrderType.ID;
            order.OrderType.IsCharge = true;
            #endregion

            //当前操作者
            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //当前患者
            FS.HISFC.Models.RADT.PatientInfo myPatientInfo = this.PatientInfo;
            //检查患者状态
            if (FS.HISFC.Components.Order.Classes.Function.CheckPatientState(this.PatientInfo.ID, ref myPatientInfo, ref errInfo) == -1)
            {
                return -1;
            }

            #region 非药品执行档医嘱扩展信息
            //赋值医嘱项目
            this.execUndrugOrder.Order = order;
            //标记该项目是否是终端确认项目
            if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
            {
                this.execUndrugOrder.Order.Item.IsNeedConfirm = ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm;
            }
            //更新执行档记帐标志
            this.execUndrugOrder.IsCharge = true;
            this.execUndrugOrder.ChargeOper.ID = empl.ID;
            this.execUndrugOrder.ChargeOper.Dept.ID = empl.Dept.ID;
            this.execUndrugOrder.ChargeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            //对于已收费项目 设置执行标记为已执行。
            this.execUndrugOrder.IsExec = true;
            this.execUndrugOrder.ExecOper.ID = empl.ID;
            this.execUndrugOrder.ExecOper.Dept = order.ExeDept;
            this.execUndrugOrder.ExecOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            //插入执行档
            this.execUndrugOrder.ID = execId;
            //是否确认
            this.execUndrugOrder.IsConfirm = true;
            if (this.execUndrugOrder.ID == "-1" || this.execUndrugOrder.ID == "")
            {
                errInfo = "插入执行档ID不能为空";
                return -1;
            }
            this.execUndrugOrder.IsExec = !execUndrugOrder.Order.Item.IsNeedConfirm;
            this.execUndrugOrder.IsValid = true;
            //医嘱应执行时间
            this.execUndrugOrder.DateUse = this.execDrugOrder.DateUse;
            //分解时间
            this.execUndrugOrder.DateDeco = this.orderMgr.GetDateTimeFromSysDateTime();
            this.execUndrugOrder.DrugFlag = 0; //默认为不需要发送

            if (this.orderMgr.InsertExecOrder(this.execUndrugOrder) < 0)
            {
                errInfo = "保存非药品执行档医嘱失败！";
                return -1;
            }
            #endregion

            return 0;
        }
        /// <summary>
        /// 判断患者信息、非药品信息、非药品数量、开方医生、处方科室、执行科室是否合法
        /// </summary>
        /// <param name="execOrderID">医嘱执行号</param>
        /// <param name="itemCode">非药品项目编码</param>
        /// <param name="itemQty">非药品项目数量</param>
        /// <param name="doctCode">开方医生</param>
        /// <param name="execDeptCode">判断执行科室</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>-1不合法,0合法</returns>
        public virtual bool IsValid(string execOrderID, string itemCode, decimal itemQty, string doctCode, string execDeptCode, ref string errorInfo)
        {
            #region 根据药品医嘱执行号获取用户信息
            string itemType = "1";//1 药品， 2 非药品
            this.execDrugOrder = this.orderMgr.QueryExecOrderByExecOrderID(execOrderID, itemType);
            if (execDrugOrder == null || execDrugOrder.ID == null || execDrugOrder.ID == string.Empty)
            {
                errorInfo = "医嘱执行号为" + execOrderID + "的执行信息不存在!请验证后输入";

                return false;
            }
            #endregion

            #region 判断患者信息
            //患者住院流水号
            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.execDrugOrder.Order.Patient;
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                errorInfo = "该患者不存在!请验证后输入";

                return false;
            }
            this.PatientInfo = patientTemp;
            #endregion

            #region 判断非药品信息
            if (itemCode == null || itemCode == string.Empty)
            {
                errorInfo = "该非药品信息不存在!请验证后输入";

                return false;
            }
            FS.HISFC.Models.Fee.Item.Undrug tempUndrug = this.undrugManager.GetItemByUndrugCode(itemCode);
            if (tempUndrug == null)
            {
                errorInfo = string.IsNullOrEmpty(this.undrugManager.Err) ? "该非药品信息不存在!请验证后输入" : this.undrugManager.Err;

                return false;
            }
            if (tempUndrug.ValidState != "1")
            {
                errorInfo = "“" + tempUndrug.Name + "”已经停用!请重新选择有效的项目";

                return false;
            }
            tempUndrug.Qty = itemQty;
            this.Undrug = tempUndrug;
            #endregion

            #region 判断开方医生
            if (!string.IsNullOrEmpty(doctCode))
            {
                this.RecipeDoctCode = doctCode;
            }
            else
            {
                errorInfo = "该开方医生不存在!请验证后输入";

                return false;
            }
            FS.HISFC.Models.Base.Employee employee = this.personManager.GetPersonByID(this.RecipeDoctCode);
            if (employee == null || string.IsNullOrEmpty(employee.ID) || string.IsNullOrEmpty(employee.Name))
            {
                errorInfo = "该开方医生不存在!请验证后输入";

                return false;
            }
            #endregion

            #region 判断开方医生科室
            if (this.RecipeDept != null && this.RecipeDept.ID != "")
            {
                employee.Dept = this.RecipeDept;
            }
            else
            {
                this.RecipeDept = employee.Dept;
            }
            #endregion

            //非药品项目名称
            string itemName = this.undrug.Name;

            #region 判断数量
            if (isJudgeQty && itemQty <= 0)
            {
                errorInfo = "“" + itemName + "”的执行数量不能小于等于零!";
                return false;
            }
            #endregion

            #region 收费比率
            decimal feeRate = 1;
            if (feeRate <= 0)
            {
                errorInfo = "费用比例不能小于0或等于0";

                return false;
            }
            #endregion

            #region 判断执行科室
            if (execDeptCode == string.Empty)
            {
                errorInfo = "“" + itemName + "”的执行科室不存在!请验证后输入";

                return false;
            }
            FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);//this.Undrug.ExecDept执行科室
            if (dept == null)
            {
                errorInfo = "“" + itemName + "”的执行科室不存在!请验证后输入";

                return false;
            }
            this.Dept = dept;
            #endregion

            return true;
        }
        /// <summary>
        /// 项目信息赋值
        /// </summary>
        /// <param name="payType">支付状态</param>
        /// <param name="operTime">操作时间</param>
        /// <param name="feeItemList">非药品费用信息</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns></returns>
        private int SetItem(PayTypes payType, DateTime operTime, ref FeeItemList feeItemList, ref string errorInfo)
        {
            FS.HISFC.Models.Base.Employee operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            //操作人ID
            feeItemList.FeeOper.ID = operObj.ID;
            //操作人科室
            feeItemList.FeeOper.Dept.ID = operObj.Dept.ID;
            //医嘱类型
            feeItemList.Order.OrderType.ID = this.execDrugOrder.Order.OrderType.ID;
            //医嘱流水号
            feeItemList.Order.ID = this.Order.ID;
            //医嘱执行档单号
            feeItemList.ExecOrder.ID = this.execUndrugOrder.ID;
            //基本项目信息
            feeItemList.Item = this.Undrug;
            //费用来源
            feeItemList.FTSource = this.ftSource;
            //费用比例{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            feeItemList.FTRate.ItemRate = 1;
            //是否组套
            if (this.isJudgeValid)
            {
                feeItemList.IsGroup = this.Undrug.UnitFlag == "1" ? true : false;
            }
            //付数
            if (feeItemList.Days == 0)
            {
                feeItemList.Days = 1;
            }
            //清空item中的扩展标记
            feeItemList.Item.SpecialFlag = "";
            feeItemList.Item.SpecialFlag1 = "";
            //数量
            feeItemList.Item.Qty = this.Undrug.Qty;
            //价格
            feeItemList.Item.Price = this.Undrug.Price;
            //单位
            feeItemList.Item.PriceUnit = this.Undrug.PriceUnit;
            //计算总额
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty, 2);
            //自费金额
            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            //原始价格
            feeItemList.Item.DefPrice = this.Undrug.Price;
            //交易类型
            feeItemList.TransType = TransTypes.Positive;
            //患者基本信息
            feeItemList.Patient = this.PatientInfo.Clone();
            //开立医生
            feeItemList.RecipeOper.ID = this.recipeDoctCode;
            //开立医生科室
            feeItemList.RecipeOper.Dept.ID = this.RecipeDept.ID;
            //执行科室
            feeItemList.ExecOper.Dept = this.Dept;
            //收费状态信息
            feeItemList.PayType = PayTypes.Charged;
            //划价人ID
            feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
            //划价人时间
            feeItemList.ChargeOper.OperTime = operTime;
            //结算序号
            feeItemList.BalanceNO = 0;
            //结算状态
            feeItemList.BalanceState = "0";
            if (payType == PayTypes.Balanced)
            {
                //收费状态信息
                feeItemList.PayType = PayTypes.Balanced;
                //执行人
                feeItemList.FeeOper.ID = this.inpatientManager.Operator.ID;
                //执行时间
                feeItemList.FeeOper.OperTime = operTime;
                //可退数量
                feeItemList.NoBackQty = feeItemList.Item.Qty;
            }

            return 1;
        }
        
        #endregion

        #region 事务管理
        /// <summary>
        /// 事务管理
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.inpatientManager.SetTrans(trans);
            this. feeIntegrate.SetTrans(trans);
            this.radtIntegrate.SetTrans(trans);
            this.inpatientManager.SetTrans(trans);
            this.personManager.SetTrans(trans);
            this.orderMgr.SetTrans(trans);
            this.undrugManager.SetTrans(trans);
            this.departmentManager.SetTrans(trans);
        }
        #endregion
    }
}
