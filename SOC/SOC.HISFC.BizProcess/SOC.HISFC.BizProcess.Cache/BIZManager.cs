using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.Cache
{
    /// <summary>
    /// 业务缓存类
    /// </summary>
    public static class BIZManager
    {
        #region 业务层缓存

        /// <summary>
        /// 非药品业务
        /// </summary>
        private static FS.HISFC.BizLogic.Fee.Item feeItemMgr = null;

        /// <summary>
        /// 非药品业务
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.Item FeeItemMgr
        {
            get
            {
                if (feeItemMgr == null)
                {
                    feeItemMgr = new FS.HISFC.BizLogic.Fee.Item();
                }
                return feeItemMgr;
            }
        }

        /// <summary>
        /// 住院费用管理
        /// </summary>
        private static FS.HISFC.BizLogic.Fee.InPatient inPatientFeeMgr = null;

        /// <summary>
        /// 住院费用管理
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.InPatient InPatientFeeMgr
        {
            get
            {
                if (inPatientFeeMgr == null)
                {
                    inPatientFeeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
                }
                return inPatientFeeMgr;
            }
        }

        /// <summary>
        /// 接口业务
        /// </summary>
        private static FS.HISFC.BizLogic.Fee.Interface interfaceMgr = null;

        /// <summary>
        /// 接口业务
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.Interface InterfaceMgr
        {
            get
            {
                if (interfaceMgr == null)
                {
                    interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
                }
                return BIZManager.interfaceMgr;
            }
        }

        /// <summary>
        /// 控制参数管理
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlManager = null;

        /// <summary>
        /// 控制参数管理
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Common.ControlParam ContrlManager
        {
            get
            {
                if (contrlManager == null)
                {
                    contrlManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                }

                return contrlManager;
            }
        }

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant conManager = null;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        public static FS.HISFC.BizLogic.Manager.Constant ConManager
        {
            get
            {
                if (conManager == null)
                {
                    conManager = new FS.HISFC.BizLogic.Manager.Constant();
                }

                return conManager;
            }
        }

        /// <summary>
        /// 分诊管理类
        /// </summary>
        private static FS.HISFC.BizLogic.Nurse.Assign assignMgr = null;

        /// <summary>
        /// 分诊管理类
        /// </summary>
        public static FS.HISFC.BizLogic.Nurse.Assign AssignMgr
        {
            get
            {
                if (assignMgr == null)
                {
                    assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
                }
                return BIZManager.assignMgr;
            }
        }

        /// <summary>
        /// 挂号交叉业务层
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Registration.Registration regInterMgr = null;

        /// <summary>
        /// 挂号交叉业务层
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Registration.Registration RegInterMgr
        {
            get
            {
                if (regInterMgr == null)
                {
                    regInterMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
                }
                return regInterMgr;
            }
        }

        /// <summary>
        /// 挂号管理
        /// </summary>
        private static FS.HISFC.BizLogic.Registration.Register regMgr = null;

        /// <summary>
        /// 挂号管理
        /// </summary>
        public static FS.HISFC.BizLogic.Registration.Register RegMgr
        {
            get
            {
                if (regMgr == null)
                {
                    regMgr = new FS.HISFC.BizLogic.Registration.Register();
                }
                return BIZManager.regMgr;
            }
        }

        /// <summary>
        /// 账户管理
        /// </summary>
        private static FS.HISFC.BizLogic.Fee.Account accountMgr = null;

        /// <summary>
        /// 账户管理
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.Account AccountMgr
        {
            get
            {
                if (accountMgr == null)
                {
                    accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                }
                return accountMgr;
            }
        }

        /// <summary>
        /// 医嘱业务
        /// </summary>
        private static FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = null;

        /// <summary>
        /// 医嘱业务
        /// </summary>
        public static FS.HISFC.BizLogic.Order.OutPatient.Order OutOrderMgr
        {
            get
            {
                if (outOrderMgr == null)
                {
                    outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                }
                return BIZManager.outOrderMgr;
            }
        }

        /// <summary>
        /// 医嘱业务
        /// </summary>
        private static FS.HISFC.BizLogic.Order.Order inOrderMgr = null;

        /// <summary>
        /// 医嘱业务
        /// </summary>
        public static FS.HISFC.BizLogic.Order.Order InOrderMgr
        {
            get
            {
                if (inOrderMgr == null)
                {
                    inOrderMgr = new FS.HISFC.BizLogic.Order.Order();
                }
                return BIZManager.inOrderMgr;
            }
        }

        /// <summary>
        /// 医嘱业务
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Order orderIntegrate = null;

        /// <summary>
        /// 医嘱业务
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Order OrderIntegrate
        {
            get
            {
                if (orderIntegrate == null)
                {
                    orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
                }
                return BIZManager.orderIntegrate;
            }
        }

        /// <summary>
        /// 住院入出转管理
        /// </summary>
        private static FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = null;

        /// <summary>
        /// 住院入出转管理
        /// </summary>
        public static FS.HISFC.BizLogic.RADT.InPatient InPatientMgr
        {
            get
            {
                if (inPatientMgr == null)
                {
                    inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                }
                return inPatientMgr;
            }
        }

        /// <summary>
        /// 费用交叉业务
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = null;

        /// <summary>
        /// 费用交叉业务
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Fee FeeIntegrate
        {
            get
            {
                if (feeIntegrate == null)
                {
                    feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                }
                return BIZManager.feeIntegrate;
            }
        }

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Manager interMgr = null;

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Manager InterMgr
        {
            get
            {
                if (interMgr == null)
                {
                    interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                }
                return BIZManager.interMgr;
            }
        }

        /// <summary>
        /// 入出转交叉业务
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = null;

        /// <summary>
        /// 入出转交叉业务
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.RADT RadtIntegrate
        {
            get
            {
                if (radtIntegrate == null)
                {
                    radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                }

                return radtIntegrate;
            }
        }

        /// <summary>
        /// 药品交叉业务
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = null;

        /// <summary>
        /// 药品交叉业务
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.Pharmacy PhaIntegrate
        {
            get
            {
                if (phaIntegrate == null)
                {
                    phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
                }

                return BIZManager.phaIntegrate;
            }
        }

        /// <summary>
        /// 门诊收费业务
        /// </summary>
        private static FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = null;

        /// <summary>
        /// 门诊收费业务
        /// </summary>
        public static FS.HISFC.BizLogic.Fee.Outpatient OutFeeMgr
        {
            get
            {
                if (outFeeMgr == null)
                {
                    outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
                }
                return BIZManager.outFeeMgr;
            }
        }

        /// <summary>
        /// 诊断业务
        /// </summary>
        private static FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = null;

        /// <summary>
        /// 诊断业务
        /// </summary>
        public static FS.HISFC.BizLogic.HealthRecord.Diagnose DiagMgr
        {
            get
            {
                if (diagMgr == null)
                {
                    diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                }

                return BIZManager.diagMgr;
            }
        }

        /// <summary>
        /// 诊断交叉业务
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase healthIntegrate = null;

        /// <summary>
        /// 诊断交叉业务
        /// </summary>
        public static FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase HealthIntegrate
        {
            get
            {
                if (healthIntegrate == null)
                {
                    healthIntegrate = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
                }

                return BIZManager.healthIntegrate;
            }
        }

        #endregion

        #region 常数缓存

        /// <summary>
        /// 常数缓存列表
        /// </summary>
        private static Dictionary<string, ArrayList> dicConList = null;

        /// <summary>
        /// 获取常数列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ArrayList GetConList(string type)
        {
            if (dicConList == null)
            {
                dicConList = new Dictionary<string, ArrayList>();
            }

            if (dicConList.ContainsKey(type))
            {
                return dicConList[type];
            }
            ArrayList alCon = ConManager.GetList(type);

            if (alCon != null)
            {
                dicConList.Add(type, alCon);
            }
            return alCon;
        }

        /// <summary>
        /// 获取常数列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ArrayList GetConList(FS.HISFC.Models.Base.EnumConstant constType)
        {
            if (dicConList == null)
            {
                dicConList = new Dictionary<string, ArrayList>();
            }

            if (dicConList.ContainsKey(constType.ToString()))
            {
                return dicConList[constType.ToString()];
            }
            ArrayList alCon = ConManager.GetList(constType);

            if (alCon != null)
            {
                dicConList.Add(constType.ToString(), alCon);
            }
            return alCon;
        }

        #endregion

        #region 控制参数缓存

        #endregion

        /// <summary>
        /// 登陆人员
        /// </summary>
        private static FS.HISFC.Models.Base.Employee logEmpl = null;

        /// <summary>
        /// 登陆人员
        /// </summary>
        public static FS.HISFC.Models.Base.Employee LogEmpl
        {
            get
            {
                //if (logEmpl == null)
                //{

                //每次都重取的目的是为了防止更改登陆的时候，静态变量没改变
                logEmpl = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator);
                //}
                return BIZManager.logEmpl;
            }
        }
    }
}
