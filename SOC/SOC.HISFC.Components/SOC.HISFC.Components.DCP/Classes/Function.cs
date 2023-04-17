using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DCP.Classes
{
    public class Function
    {
        static Function()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 需要报卡的诊断（备注作为匹配项）
        /// </summary>
        public static System.Collections.Hashtable hsDiag;

        public static System.Data.DataTable dtDiag = null;

        private static FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        /// <summary>
        /// 转换状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns>string类型</returns>
        public static string ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState state)
        {
            return ((int)state).ToString();
        }

        /// <summary>
        /// 转换患者类型
        /// </summary>
        /// <param name="type">患者类型</param>
        /// <returns>汉字名称</returns>
        public static string ConverPatientType(FS.SOC.HISFC.DCP.Enum.PatientType type)
        {
            string name = "";
            switch (type)
            {
                case FS.SOC.HISFC.DCP.Enum.PatientType.C:
                    name = "门诊";
                    break;
                case FS.SOC.HISFC.DCP.Enum.PatientType.I:
                    name = "住院";
                    break;
                case FS.SOC.HISFC.DCP.Enum.PatientType.O:
                    name = "其他";
                    break;
                default:
                    break;
            }
            return name;
        }

        /// <summary>
        /// 检查患者是否已经报卡
        /// </summary>
        /// <param name="patient">患者实体</param>
        /// <param name="diseaseCode">疾病代码</param>
        /// <returns>true已经报卡</returns>
        public static bool CheckPatientNeedReport(FS.HISFC.Models.RADT.Patient patient, string diseaseCode,ref bool isMustReport)
        {
            //根据姓名和患者号检索
            string sql = @" where (patient_no = '{0}' or patient_name = '{1}') and DISEASE_CODE in ('{2}') and state not in ('3','4') order by report_date desc";
            sql = string.Format(sql, patient.PID.CardNO, patient.Name, diseaseCode.Replace(",", "','"));

            FS.SOC.HISFC.BizLogic.DCP.DiseaseReport dpMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

            System.Collections.ArrayList alReport = dpMgr.GetCommonReportListByWhere(sql);

            //没有报卡
            if (alReport == null || alReport.Count == 0)
            {
                return true;
            }

            //有报卡
            foreach (FS.HISFC.DCP.Object.CommonReport report in alReport)
            {
                //可能需要考虑报卡时间，比如跨年度后的数据不认为是报过卡的
                if (dpMgr.GetDateTimeFromSysDateTime().AddYears(-1) >= report.ReportTime)
                {
                    //超过一年的直接上报
                    return true;
                }

                //是否同类疾病
                if (diseaseCode.IndexOf(report.Disease.ID) != -1)
                {
                    string msg = "患者已经报卡："
                        + "卡号：" + report.ReportNO
                        + "疾病名称：" + report.Disease.Name
                        + "报卡时间：" + report.ReportTime.ToString()
                        + "\n是否继续报卡";
                    isMustReport = false;
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(Language.Msg(msg), Language.Msg("提示"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            return true;
        }


        // {9A497C15-596A-420d-8AA8-27766FFB760E} 检查患者是否已经填写不报卡原因
        //2015-1-5-yeph

        /// <summary>
        /// 检查患者是否已经填写不报卡原因
        /// </summary>
        /// <param name="patient">患者实体</param>
        /// <param name="diseaseCode">疾病代码</param>
        /// <returns>true已经报卡</returns>
        public static bool CheckPatientNeedResonOfNot(FS.HISFC.Models.RADT.Patient patient, string diseaseName,ref bool isReport)
        {
            //根据姓名和患者号检索
            string sql = @" 
                          
                               WHERE CLINO_NO = '{0}'
                             AND DIAG_NAME='{1}'
                            ";
            sql = string.Format(sql,((FS.FrameWork.Models.NeuObject)(patient)).ID, diseaseName);

            FS.SOC.HISFC.BizLogic.DCP.DiseaseReport dpMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

            System.Collections.ArrayList alReport = dpMgr.GetReasonOfNotNeed(sql);

            //没有报卡
            if (alReport == null || alReport.Count == 0)
            {
                return true;
            }

            //有报卡
            else
            {
               
                    string msg = "患者已经填写过不填卡原因："
                        + "疾病名称：" + diseaseName
                        + "\n是否继续填写";
                    isReport = false;
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(Language.Msg(msg), Language.Msg("提示"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                    
                }
            
            return true;
        }



        /// <summary>
        /// 根据诊断名称判断是否传染病
        /// </summary>
        /// <param name="diagName">诊断名称</param>
        /// <param name="diseaseCode">疾病代码串[可能多种]</param>
        /// <returns>true是传染病</returns>
        public static bool CheckDiagNose(string diagName, out string diseaseCode)
        {
            //去掉空格
            diagName = diagName.Trim();
            diseaseCode = "";

            if (string.IsNullOrEmpty(diagName))
            {
                return false;
            }

            if (hsDiag == null || dtDiag == null)
            {
                hsDiag = new System.Collections.Hashtable();
                
                System.Collections.ArrayList alDiag = commonProcess.QueryConstantList("INFDIAGNOSE");

                foreach (FS.HISFC.Models.Base.Const con in alDiag)
                {
                    if (!hsDiag.Contains(con.Memo))
                    {
                        hsDiag.Add(con.Memo, con);
                    }
                }
            }
            

            //精确匹配
            if (hsDiag.Contains(diagName))
            {
                diseaseCode = ((FS.HISFC.Models.Base.Const)hsDiag[diagName]).UserCode;
                return true;
            }

            //模糊匹配
            string sql = "select code,name,mark,input_code from com_dictionary t where t.type='INFDIAGNOSE' and '{0}' like mark";
            FS.FrameWork.Management.DataBaseManger dbManager = new DataBaseManger();
            if (dbManager.ExecQuery(string.Format(sql, FS.FrameWork.Public.String.TakeOffSpecialChar(diagName))) > 0)
            {
                try
                {
                    if (dbManager.Reader.Read())
                    {
                        diseaseCode = dbManager.Reader[3].ToString();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    if (dbManager.Reader != null && !dbManager.Reader.IsClosed)
                    {
                        dbManager.Reader.Close();
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查用户是否有权限审核
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="privType"></param>
        /// <returns></returns>
        public static bool CheckUserPriv(string operCode, string privType)
        {
            List<FS.FrameWork.Models.NeuObject> alPriv = null;

            FS.SOC.HISFC.BizProcess.DCP.Permission permissionProcess = new FS.SOC.HISFC.BizProcess.DCP.Permission();
            alPriv = permissionProcess.QueryUserPriv(operCode, privType);
            if (alPriv == null || alPriv.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断是否为非负的数字
        /// </summary>
        /// <param name="xpressions">字符串</param>
        /// <returns>true 是</returns>
        public static bool IsUnInt(string expressions)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(expressions, @"^\d+$");
        }

        public static bool IsControl(string expressions)
        {
            //^[A-Za-z]+$　　//匹配由26个英文字母组成的字符串
            //^[A-Z]+$　　//匹配由26个英文字母的大写组成的字符串
            //^[a-z]+$　　//匹配由26个英文字母的小写组成的字符串
            //^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串
            //^\w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串

            return false;

        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// 取当前操作员是否有某一权限。
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <returns>True 有权限, False 无权限</returns>
        public static bool JugePrive(string class2Code)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //权限管理类
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //取操作员拥有权限的科室
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);

            if (al == null || al.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检测是否是管理员
        /// </summary>
        /// <param name="operCode">操作员工号</param>
        /// <returns>true 是，false 否</returns>
        public static bool JugeManager(string operCode)
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            return ((FS.HISFC.Models.Base.Employee)privManager.Operator).IsManager;
        }

    }
}
