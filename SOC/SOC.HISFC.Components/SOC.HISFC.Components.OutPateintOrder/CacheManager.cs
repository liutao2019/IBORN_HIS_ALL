using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.OutPatientOrder
{
    /// <summary>
    /// 缓存
    /// </summary>
    public static class CacheManager
    {
        #region 业务层缓存

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
                return CacheManager.regMgr;
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
        private static FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr = null;

        /// <summary>
        /// 医嘱业务
        /// </summary>
        public static FS.HISFC.BizLogic.Order.OutPatient.Order OrderMgr
        {
            get
            {
                if (orderMgr == null)
                {
                    orderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                }
                return CacheManager.orderMgr;
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
                return CacheManager.feeIntegrate;
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
                return CacheManager.interMgr;
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

        #endregion

        #region 控制参数缓存

        #endregion
    }
}
