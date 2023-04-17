using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Neusoft.HISFC.Models.Fee.Inpatient;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.BizProcess.Interface.Order;
using Neusoft.HISFC.Models.Order;

namespace SOC.Local.HISWebServiceTest
{
    public class ChargeConfirm 
    {
        #region 变量
        /// <summary>
        /// 人员信息业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Person personManager = new Neusoft.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 住院费用业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.InPatient inpatientManager = new Neusoft.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 费用综合业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeIntergrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 如出转业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();
        /// <summary>
        /// 医嘱管理业务层
        /// </summary>
        protected Neusoft.HISFC.BizLogic.Order.Order orderMgr = new Neusoft.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 药品医嘱执行档信息
        /// </summary>
        Neusoft.HISFC.Models.Order.ExecOrder execDrugOrder = new Neusoft.HISFC.Models.Order.ExecOrder();
        /// <summary>
        /// 非药品业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Fee.Item undrugManager = new Neusoft.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 科室业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Department departmentManager = new Neusoft.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 患者基本信息实体
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 医嘱基本信息实体
        /// </summary>
        private Neusoft.HISFC.Models.Order.Inpatient.Order order = new Neusoft.HISFC.Models.Order.Inpatient.Order();
        /// <summary>
        /// 患者费用信息实体
        /// </summary>
        private Neusoft.HISFC.Models.Fee.Item.Undrug undrug = new Neusoft.HISFC.Models.Fee.Item.Undrug();
        /// <summary>
        /// 执行科室信息实体
        /// </summary>
        private Neusoft.HISFC.Models.Base.Department dept = new Department();
        /// <summary>
        /// 处理适住院应症
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient IAdptIllnessInPatient = null;
        /// <summary>
        /// 是否判断欠费，如何提示(Y：判断欠费，不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费)
        /// </summary>
        private Neusoft.HISFC.Models.Base.MessType messtype = Neusoft.HISFC.Models.Base.MessType.M;
        /// <summary>
        /// 是否判断数量
        /// </summary>
        private bool isJudgeQty = true;
        /// <summary>
        /// 患者基本信息实体
        /// </summary>
        public Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.patientInfo = value;
            }
            get
            {
                return this.patientInfo;
            }
        }
        /// <summary>
        /// 医嘱基本信息实体
        /// </summary>
        public Neusoft.HISFC.Models.Order.Inpatient.Order Order
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
        public Neusoft.HISFC.Models.Fee.Item.Undrug Undrug
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
        public Neusoft.HISFC.Models.Base.Department Dept
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
        private Neusoft.FrameWork.Models.NeuObject recipeDept = null;
        public Neusoft.FrameWork.Models.NeuObject RecipeDept
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
        private Neusoft.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FTSource();
        public Neusoft.HISFC.Models.Fee.Inpatient.FTSource FTSource
        {
            set
            {
                ftSource = value;
            }
        }
        #endregion

        /// <summary>
        /// 保存收费信息
        /// </summary>
        /// <param name="execOrderID">医嘱执行编号</param>
        /// <param name="itemCode">非药品编号</param>
        /// <param name="itemQty">非药品数量</param>
        /// <param name="doctCode">开方医生编号</param>
        /// <param name="execDept">执行科室编号</param>
        /// <param name="comBoNo">组合序号</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns>-1 失败,1 成功</returns>
        public int SaveChargeInfo(string execOrderID, string itemCode, decimal itemQty, string doctCode, string execDeptCode,string comBoNo, ref string errInfo)
        {
            IAdptIllnessInPatient = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)) as Neusoft.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient;
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
            if (this.feeIntergrate.IsPatientLackFee(this.patientInfo))
            {
                //欠费判断
                if (this.messtype == MessType.M)
                {
                    //errInfo = "此患者已欠费，是否继续？";//==================================================

                    //return -1;
                }
                else if (this.messtype == MessType.Y)
                {
                    errInfo = "此患者已欠费，不允许继续收费!";

                    return -1;
                }
            }
            ArrayList firstInputFeeItemlist = new ArrayList();
            //保存本次收费项目明细信息
            this.feeItemCollection = new List<FeeItemList>();
            //操作时间
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            #region 处理费用信息
            FeeItemList feeItemList = new FeeItemList();
            bool isNew = true;
            //项目信息赋值
            int returnValue = this.SetItem(PayTypes.Balanced, operTime, ref isNew, ref feeItemList, ref errInfo);

            //如果获得的项目信息为空,不处理
            if (returnValue <= 0)
            {
                return -1;
            }
            //如果是新录入项目
            if (isNew)
            {
                feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;
                firstInputFeeItemlist.Add(feeItemList.Clone());

                this.feeItemCollection.Add(feeItemList.Clone());
            }
            #endregion

            //调用整体收费函数,收取第一次录入的费用
            if (this.feeIntergrate.FeeItem(this.patientInfo, ref firstInputFeeItemlist) == -1)
            {
                errInfo = this.feeIntergrate.Err;
                this.feeIntergrate.MedcareInterfaceProxy.Disconnect();

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
            this.feeIntergrate.MedcareInterfaceProxy.CloseAll();

            #region 保存医嘱信息
            int sequenceNO = 0;
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in firstInputFeeItemlist)
            {
                sequenceNO = feeItem.SequenceNO;
            }
            int saveOrderResult = this.SavaOrderInfo(execOrderID,sequenceNO, comBoNo, ref errInfo);
            if (saveOrderResult<0)
            {
                return -1;
            }
            #endregion

            errInfo = "收费成功!";

            return 1;
        }
        /// <summary>
        /// 保存医嘱信息
        /// </summary>
        /// <param name="execOrderID">医嘱执行编号</param>
        /// <param name="sequenceNO">处方流水号</param>
        /// <param name="comBoNo">组合序号</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1 失败,0 成功</returns>
        public int SavaOrderInfo(string execOrderID, int sequenceNO, string comBoNo, ref string errInfo)
        {
            #region 非药品执行档医嘱信息
            //非药品执行单流水号
            string execId = this.orderMgr.GetNewOrderExecID();
            //住院科室代码
            order.InDept = this.execDrugOrder.Order.InDept;
            //住院护理站代码

            //患者信息(住院流水号、住院病历号等)
            order.Patient = this.PatientInfo;
            //医嘱流水号
            order.ID = this.orderMgr.GetNewOrderID();
            //开立医生
            Neusoft.FrameWork.Models.NeuObject recipeDoct = this.personManager.GetPersonByID(this.RecipeDoctCode) as Neusoft.FrameWork.Models.NeuObject;
            //医嘱开立时间
            order.MOTime = this.execDrugOrder.Order.MOTime;
            //是否婴儿医嘱
            order.IsBaby = this.execDrugOrder.Order.IsBaby;
            //婴儿序号
            order.BabyNO = this.execDrugOrder.Order.BabyNO;
            //项目属性
            
            //是否包含附材
            order.IsHaveSubtbl = this.execDrugOrder.Order.IsHaveSubtbl;
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
            order.SequenceNO = sequenceNO;
            //项目频次编码
            order.Frequency = this.execDrugOrder.Order.Frequency;
            //分解时间
            order.CurMOTime = this.execDrugOrder.Order.CurMOTime;
            //医嘱状态
            order.Status = this.execDrugOrder.Order.Status;
            //组合序号
            if(!string.IsNullOrEmpty(comBoNo))
            {
                order.Combo.ID = comBoNo;
            }
            //医嘱类型
            order.OrderType.IsDecompose = this.execDrugOrder.Order.OrderType.IsDecompose;
            order.OrderType.ID = this.execDrugOrder.Order.OrderType.ID;
            order.OrderType.Name = this.execDrugOrder.Order.OrderType.Name;
            #endregion

            //当前操作者
            Neusoft.HISFC.Models.Base.Employee empl = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;
            //当前患者
            Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo = this.PatientInfo;
            //检查患者状态
            if (Neusoft.HISFC.Components.Order.Classes.Function.CheckPatientState(this.PatientInfo.ID, ref myPatientInfo, ref errInfo) == -1)
            {
                return -1;
            }

            #region 非药品执行档医嘱扩展信息
            Neusoft.HISFC.Models.Order.ExecOrder objExec = new Neusoft.HISFC.Models.Order.ExecOrder();
            //赋值医嘱项目
            objExec.Order = order;
            //标记该项目是否是终端确认项目
            if (order.Item.GetType() == typeof(Neusoft.HISFC.Models.Fee.Item.Undrug))
            {
                objExec.Order.Item.IsNeedConfirm = ((Neusoft.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm;
            }
            //更新执行档记帐标志
            objExec.IsCharge = true;
            objExec.ChargeOper.ID = empl.ID;
            objExec.ChargeOper.Dept.ID = empl.Dept.ID;
            objExec.ChargeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            //对于已收费项目 设置执行标记为已执行。
            objExec.IsExec = true;
            objExec.ExecOper.ID = empl.ID;
            objExec.ExecOper.Dept = order.ExeDept;
            objExec.ExecOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            //插入执行档
            objExec.ID = execId;
            if (objExec.ID == "-1" || objExec.ID == "")
            {
                errInfo = "插入执行档ID不能为空";
                return -1;
            } 
            objExec.IsExec = !objExec.Order.Item.IsNeedConfirm;
            objExec.IsValid = true;
            objExec.DateUse = this.execDrugOrder.Order.BeginTime;
            objExec.DateDeco = this.execDrugOrder.Order.CurMOTime;
            objExec.DrugFlag = 0; //默认为不需要发送

            if (this.orderMgr.InsertExecOrder(objExec) < 0)
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
            #region 根据医嘱执行号获取用户信息
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
            Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = this.execDrugOrder.Order.Patient;
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
            Neusoft.HISFC.Models.Fee.Item.Undrug tempUndrug = this.undrugManager.GetItemByUndrugCode(itemCode);
            if (tempUndrug == null)
            {
                errorInfo = this.undrugManager.Err;

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
            Neusoft.HISFC.Models.Base.Employee employee = this.personManager.GetPersonByID(this.RecipeDoctCode);
            if (employee == null)
            {
                errorInfo = "该开方医生不存在!请验证后输入";

                return false;
            }
            #endregion

            #region 处方科室
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
            if (isJudgeQty && itemQty<=0)
            {
                errorInfo = "“"+itemName+"”的执行数量不能小于等于零!";
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
            Neusoft.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);//this.Undrug.ExecDept执行科室
            if (dept==null)
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
        /// <param name="isNewItem">是否为新项目</param>
        /// <param name="feeItemList">非药品费用信息</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns></returns>
        private int SetItem(PayTypes payType, DateTime operTime, ref bool isNewItem, ref FeeItemList feeItemList, ref string errorInfo)
        {
            Neusoft.HISFC.Models.Base.Employee operObj = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Clone();
            //操作人ID
            feeItemList.FeeOper.ID = operObj.ID;
            //操作人科室
            feeItemList.FeeOper.Dept.ID = operObj.Dept.ID;
            //基本项目信息
            feeItemList.Item = this.Undrug;
            feeItemList.Item.SpecialFlag2 = "0";
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
            //数量
            feeItemList.Item.Qty = this.Undrug.Qty;
            //价格
            feeItemList.Item.Price = this.Undrug.Price;
            //单位
            feeItemList.Item.PriceUnit = this.Undrug.PriceUnit;
            //计算总额
            feeItemList.FT.TotCost = Neusoft.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty, 2);
            //自费金额
            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            if (isNewItem)
            {
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
                isNewItem = true;
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
            }

            return 1;
        }
        
        #region 事务管理
        /// <summary>
        /// 事务管理
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.inpatientManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.feeIntergrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.departmentManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.undrugManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
        }
        #endregion
    }
}
