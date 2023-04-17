﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Statistics
{
    /// <summary>
    /// 家庭首次套餐时间数据统计节点，输入一个patient的crmid即可统计所有信息
    /// 创建者：胡云贵
    /// 创建时间2020年7月13日
    /// {7930AB5C-6E33-4855-87E8-B87749639B88}
    /// </summary>
    public class FamilyFirstPackageStatistics : FS.HISFC.BizProcess.Interface.Statistics.IStatistics
    {
        private FS.HISFC.Models.RADT.PatientInfo currentPatientInfo;
        private Object currentObject;

        #region 构造方法
        public FamilyFirstPackageStatistics()
        {
            
        }
        #endregion

        public int insert(Object o)
        {
            return 0;
        }

        public int delete(Object o)
        {
            return 0;
        }

        public int update(Object o)
        {
            return 0;
        }

        public int select(Object o)
        {
            return 0;
        }

        /// <summary>
        /// 数据统计项目
        /// </summary>
        /// <returns></returns>
        public int SetValue(Object o)
        {
            return 0;
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
            #region 家庭首次套餐时间统计
            //{BB562733-DDE5-4768-9717-56A11D2CCE2B}
            if (string.IsNullOrEmpty(currentPatientInfo.CrmID))
            {
                return 0;
            }

            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                            <req>
                              <crmId>{0}</crmId>
                            </req>";
            try
            {
                FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);

                FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                server.Url = url;
                string res = server.FamilyOldestPackageDatePoint(string.Format(req, currentPatientInfo.CrmID));
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("家庭首次套餐时间异常！" + e.Message);
            }
            
            #endregion
            return 0;
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
