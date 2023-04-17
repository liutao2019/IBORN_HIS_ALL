using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.StatisticsPoint
{
    /// <summary>
    /// 套餐收费数据统计接口实现类
    /// 创建者：胡云贵
    /// 创建时间2020年7月13日
    /// {7930AB5C-6E33-4855-87E8-B87749639B88}
    /// </summary>
    public class PackageChargeStatisticsPoint : FS.HISFC.BizProcess.Interface.StatisticsPoint.IStatisticsPoint
    {

        private FS.HISFC.Models.RADT.PatientInfo currentPatientInfo;
        private Object currentObject;

        #region 配置参数
        /// <summary>
        /// 是否启用家庭首次套餐时间时间节点模块
        /// </summary>
        private bool IsFamilyFirstPackageInUse = false;

        /// <summary>
        /// 是否启用首次到院时间节点模块
        /// </summary>
        private bool IsFirstArriveStatisticsInUse = false;

        #endregion

        #region 业务类
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region 数据统计接口初始化
        private FS.HISFC.BizProcess.Interface.Statistics.IStatistics familyFirstpackageStatistic = new FS.HISFC.BizProcess.Integrate.Statistics.FamilyFirstPackageStatistics();
        private FS.HISFC.BizProcess.Interface.Statistics.IStatistics firstArriveStatistics = new FS.HISFC.BizProcess.Integrate.Statistics.FirstArriveStatistics();
        #endregion

        #region 构造方法
        public PackageChargeStatisticsPoint()
        {
            //初始化配置参数
            this.IsFamilyFirstPackageInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0003", false, false);
            this.IsFirstArriveStatisticsInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0004", false, false);
        }
        #endregion

        /// <summary>
        /// 数据统计项目
        /// </summary>
        /// <returns></returns>
        public int SetValue(Object o)
        {
            this.currentObject = o;
            return 1;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.currentPatientInfo = patientInfo.Clone();
            return 1;
        }

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        public int DoStatistics()
        {
            if (IsFamilyFirstPackageInUse)
            {
                familyFirstpackageStatistic.SetPatient(currentPatientInfo);
                familyFirstpackageStatistic.DoStatistics();
            }
            if (IsFirstArriveStatisticsInUse)
            {
                firstArriveStatistics.SetPatient(currentPatientInfo);
                firstArriveStatistics.DoStatistics();
            }
            return 1;
        }

        /// <summary>
        /// 后期操作
        /// </summary>
        /// <returns></returns>
        public int DoAfter()
        {
            return 0;
        }

        
    }
}
