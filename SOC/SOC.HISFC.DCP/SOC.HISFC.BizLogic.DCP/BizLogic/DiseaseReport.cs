using System;
using System.Collections;
using System.Collections.Generic;

namespace FS.SOC.HISFC.BizLogic.DCP
{
    /// <summary>
    /// DiseaseReport<br></br>
    /// [功能描述: DiseaseReport]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2008-8-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class DiseaseReport : FS.SOC.HISFC.BizLogic.DCP.BizLogic.DataBase
    {
        public DiseaseReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 静态/常量

        /// <summary>
        /// 报告卡编号
        /// </summary>
        private const int ReportNOMedian = 5;

        #endregion

        #region 主卡管理

        /// <summary>
        /// 获取传染病报告卡的参数
        /// </summary>
        /// <param name="commonReport">实体</param>
        /// <returns>参数数组</returns>
        private string[] myGetCommonReportParm(FS.HISFC.DCP.Object.CommonReport commonReport)
        {
            string[] strParm = {
								   commonReport.ID,	
                                   commonReport.ReportNO,
								   commonReport.Patient.PID.CardNO,
                                   commonReport.PatientType,
								   commonReport.Patient.Name,
								   commonReport.PatientParents,
								   commonReport.Patient.IDCard,
								   commonReport.Patient.Sex.ID.ToString(),
								   commonReport.Patient.Birthday.ToString(),
								   commonReport.Patient.Age,
								   commonReport.AgeUnit,
								   commonReport.Patient.Profession.ID,
								   commonReport.Patient.CompanyName,
								   commonReport.Patient.PhoneHome,
								   commonReport.HomeArea,
								   commonReport.HomeProvince.ID,
								   commonReport.HomeCity.ID,
								   commonReport.HomeCouty.ID,
								   commonReport.HomeTown.ID,
								   commonReport.Patient.AddressHome,
								   commonReport.PatientDept.ID,
								   commonReport.Disease.Memo,
								   commonReport.Disease.ID,
								   commonReport.Disease.Name,
								   commonReport.InfectDate.ToString(),
								   commonReport.DiagnosisTime.ToString(),
								   commonReport.DeadDate.ToString(),
								   commonReport.CaseClass1.ID,
								   commonReport.CaseClass2,
								   commonReport.InfectOtherFlag,
								   commonReport.State,
								   commonReport.AddtionFlag,
                                   commonReport.SexDiseaseFlag,
								   commonReport.Memo,
								   commonReport.ReportDoctor.ID,
								   commonReport.DoctorDept.ID,
								   commonReport.ReportTime.ToString(),
								   commonReport.CancelOper.ID,
								   commonReport.CancelTime.ToString(),
								   commonReport.ModifyOper.ID,
								   commonReport.ModifyTime.ToString(),
								   commonReport.ApproveOper.ID,
								   commonReport.ApproveTime.ToString(),
								   commonReport.OperCase,
                                   commonReport.CorrectFlag,
                                   commonReport.CorrectReportNO,
                                   commonReport.CorrectedReportNO,
								   commonReport.Oper.ID,
								   commonReport.OperDept.ID,
								   commonReport.OperTime.ToString(),
								   commonReport.ExtendInfo1,
								   commonReport.ExtendInfo2,
								   commonReport.ExtendInfo3
			};
            return strParm;
        }

        /// <summary>
        /// 获取报卡
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>报卡实体数组</returns>
        private ArrayList myGetCommonReport(string strSQL)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.DCP.Object.CommonReport commonReport;

            if (this.ExecQuery( strSQL ) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    commonReport = new FS.HISFC.DCP.Object.CommonReport();

                    commonReport.ID = this.Reader[0].ToString();
                    commonReport.ReportNO = this.Reader[1].ToString();
                    commonReport.Patient.PID.CardNO = this.Reader[2].ToString();
                    commonReport.Patient.PID.PatientNO = this.Reader[2].ToString();
                    commonReport.PatientType = this.Reader[3].ToString();
                    commonReport.Patient.Name = this.Reader[4].ToString();
                    commonReport.PatientParents = this.Reader[5].ToString();
                    commonReport.Patient.IDCard = this.Reader[6].ToString();
                    commonReport.Patient.Sex.ID = this.Reader[7].ToString();
                    commonReport.Patient.Birthday = System.Convert.ToDateTime(this.Reader[8].ToString());
                    commonReport.Patient.Age = this.Reader[9].ToString();
                    commonReport.AgeUnit = this.Reader[10].ToString();
                    commonReport.Patient.Profession.ID = this.Reader[11].ToString();
                    commonReport.Patient.CompanyName = this.Reader[12].ToString();
                    commonReport.Patient.PhoneHome = this.Reader[13].ToString();
                    commonReport.HomeArea = this.Reader[14].ToString();
                    commonReport.HomeProvince.ID = this.Reader[15].ToString();
                    commonReport.HomeCity.ID = this.Reader[16].ToString();
                    commonReport.HomeCouty.ID = this.Reader[17].ToString();
                    commonReport.HomeTown.ID = this.Reader[18].ToString();
                    commonReport.Patient.AddressHome = this.Reader[19].ToString();
                    commonReport.PatientDept.ID = this.Reader[20].ToString();
                    commonReport.Disease.Memo = this.Reader[21].ToString();
                    commonReport.Disease.ID = this.Reader[22].ToString();
                    commonReport.Disease.Name = this.Reader[23].ToString();
                    commonReport.InfectDate = System.Convert.ToDateTime(this.Reader[24].ToString());
                    commonReport.DiagnosisTime = System.Convert.ToDateTime(this.Reader[25].ToString());
                    commonReport.DeadDate = System.Convert.ToDateTime(this.Reader[26].ToString());
                    commonReport.CaseClass1.ID = this.Reader[27].ToString();
                    commonReport.CaseClass2 = this.Reader[28].ToString();
                    commonReport.InfectOtherFlag = this.Reader[29].ToString();
                    commonReport.State = this.Reader[30].ToString();
                    commonReport.AddtionFlag = this.Reader[31].ToString();
                    commonReport.SexDiseaseFlag = this.Reader[32].ToString();
                    commonReport.Memo = this.Reader[33].ToString();
                    commonReport.ReportDoctor.ID = this.Reader[34].ToString();
                    commonReport.DoctorDept.ID = this.Reader[35].ToString();
                    commonReport.ReportTime = System.Convert.ToDateTime(this.Reader[36].ToString());
                    commonReport.CancelOper.ID = this.Reader[37].ToString();
                    commonReport.CancelTime = System.Convert.ToDateTime(this.Reader[38].ToString());
                    commonReport.ModifyOper.ID = this.Reader[39].ToString();
                    commonReport.ModifyTime = System.Convert.ToDateTime(this.Reader[40].ToString());
                    commonReport.ApproveOper.ID = this.Reader[41].ToString();
                    commonReport.ApproveTime = System.Convert.ToDateTime(this.Reader[42].ToString());
                    commonReport.OperCase = this.Reader[43].ToString();
                    commonReport.CorrectFlag = this.Reader[44].ToString();
                    commonReport.CorrectReportNO = this.Reader[45].ToString();
                    commonReport.CorrectedReportNO = this.Reader[46].ToString();
                    commonReport.Oper.ID = this.Reader[47].ToString();
                    commonReport.OperDept.ID = this.Reader[48].ToString();
                    commonReport.OperTime = System.Convert.ToDateTime(this.Reader[49].ToString());
                    commonReport.ExtendInfo1 = this.Reader[50].ToString();
                    commonReport.ExtendInfo2 = this.Reader[51].ToString();
                    commonReport.ExtendInfo3 = this.Reader[52].ToString();

                    al.Add(commonReport);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取取报告卡信息时，执行SQL出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 报告卡编号
        /// </summary>
        /// <returns></returns>
        private string GetCommonReportNO()
        {
            string strSQL = "";
            string no = "1";

            //返回年度报卡数+1
            if (this.GetSQL("DCP.CommonReport.GetNO", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.GetNO字段";

                return this.GetDateTimeFromSysDateTime().ToString( "yyyy" ) + no.PadLeft( ReportNOMedian, '0' );
            }
            this.ExecQuery(strSQL);
            try
            {
                while (this.Reader.Read())
                {
                    no = this.Reader[0].ToString();
                }
                if (no == null || no == "")
                {
                    return this.GetDateTimeFromSysDateTime().ToString( "yyyy" ) + no.PadLeft( ReportNOMedian, '0' );
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取取报告卡编号时，执行SQL出错" + ex.Message;
                return "a";
            }
            finally
            {
                this.Reader.Close();
            }

            //全年编号
            no = this.GetDateTimeFromSysDateTime().ToString( "yyyy" ) + no.PadLeft( ReportNOMedian, '0' );

            return no;
        }

        /// <summary>
        /// 获取报告卡的流水号
        /// </summary>
        /// <returns>报告卡的流水号</returns>
        public string GetCommonReportID()
        {
            string reportID = this.GetSequence("DCP.CommonReport.GetID");
            reportID = reportID.PadLeft(10, '0');
            return reportID;
        }

        /// <summary>
        /// 插入传染病报卡信息
        /// </summary>
        /// <param name="commonReport">传染病报卡实体</param>
        /// <returns>-1失败</returns>
        public int InsertCommonReport(FS.HISFC.DCP.Object.CommonReport commonReport)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Insert", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Insert字段";
                return -1;
            }
            string reportID = this.GetCommonReportID();//获取序数		
            commonReport.ID = reportID;
            string reportNO = this.GetCommonReportNO();//获取编号		
            commonReport.ReportNO = reportNO;
            try
            {
                string[] strParm = this.myGetCommonReportParm(commonReport);  //取参数列表

                strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新报告卡
        /// </summary>
        /// <param name="commonReport">报卡实体</param>
        /// <returns></returns>
        public int UpdateCommonReport(FS.HISFC.DCP.Object.CommonReport commonReport)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.UpdateByID", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.UpdateByID字段";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCommonReportParm(commonReport);  //取参数列表
                strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 报告卡删除
        /// </summary>
        /// <param name="ReportID">编号</param>
        /// <returns></returns>
        public int DeleteCommonReport(string ReportID)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.DeleteByID", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.DeleteByID字段";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ReportID);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取科室所有报告卡
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <returns>实体数组</returns>
        public ArrayList GetCommonReportList(string deptCode)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByDept", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByDept字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, deptCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据患者号获取患者报告卡
        /// </summary>
        /// <param name="patientNO">门诊卡号|住院号</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByPatientNO(string patientNO)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByPatientNO", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByPatientNO字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, patientNO);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据患者姓名获取患者报告卡
        /// </summary>
        /// <param name="patientName">门诊卡号|住院号</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByPatientName(string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByPatientName", ref strSQLWhere) == -1)
            {
                strSQLWhere = @" where patient_name  =  '{0}'";
            }
            strSQLWhere = string.Format(strSQLWhere, patientName);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据状态获取报告卡
        /// </summary>
        /// <param name="state">报卡状态(可以in)</param>
        /// <returns>报卡实体数组</returns>
        public ArrayList GetvReportListByState(string state)
        {
            if (state.IndexOf("'") == -1)
            {
                state = "'" + state + "'";
            }
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereStateIn", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereStateIn字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据患者姓名模糊获取报告卡
        /// </summary>
        /// <param name="state">姓名</param>
        /// <returns>报卡实体数组</returns>
        public ArrayList GetReportListByPatientName(string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByName", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByName字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, patientName);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据流水号查询报卡
        /// </summary>
        /// <param name="reportID">报卡流水号</param>
        /// <returns>报卡实体 错误null</returns>
        public FS.HISFC.DCP.Object.CommonReport GetCommonReportByID(string reportID)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByID", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByID字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, reportID);
            al = this.myGetCommonReport(strSQL + strSQLWhere);
            if (al.Count == 1)
            {
                return al[0] as FS.HISFC.DCP.Object.CommonReport;
            }

            return null;
        }

        /// <summary>
        /// 根据编号查询报告卡(精确查询)
        /// </summary>
        /// <param name="reportNO"></param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.CommonReport GetCommonReportByNO(string reportNO)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();
            if(this.GetSQL("DCP.CommonReport.Query",ref strSQL) == -1) 
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByNO", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByNO字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, reportNO);

            al = this.myGetCommonReport(strSQL + strSQLWhere);

            if (al != null && al.Count >= 1)
            {
                return al[0] as FS.HISFC.DCP.Object.CommonReport;
            }

            return null;
        }      

        /// <summary>
        /// 获取科室某状态的报告卡
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="deptCode">科室</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByStateAndDept(string state, string deptCode)
        {
            string strSQL = "";
            if (state.IndexOf("'") == -1)
            {
                state = "'" + state + "'";
            }
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByStateAndDept", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByStateAndDept字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state, deptCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 获取科室某状态的报告卡
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="doctorCode">报告医生</param>
        /// <returns></returns>
        public ArrayList GetReportListByStateAndDoctor(string state, string doctorCode)
        {
            string strSQL = "";
            if (state.IndexOf("'") == -1)
            {
                state = "'" + state + "'";
            }
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByStateAndDoctor", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByStateAndDoctor字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state, doctorCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="doctDept">科室代码</param>
        /// <param name="dateType">字段</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByMore(string dateType, string beginDate, string endDate, string reportState, string doctDept)
        {
            string state = reportState;
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByMore", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByMore字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, dateType, beginDate, endDate, state, doctDept);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="dateType">字段</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="state">状态</param>
        /// <param name="diseaseCode">疾病编码</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByMore(string dateType, string beginDate, string endDate, string reportState, string doctDept, string diseaseCode)
        {
            string state = reportState;
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("SOC.DCP.CommonReport.WhereByMore", ref strSQLWhere) == -1)
            {
                strSQLWhere = @" where  met_inf_report.{0} >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                  and   met_inf_report.{0}< to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                  and  (met_inf_report.state in ('{3}') or 'AAA' in ('{3}'))
                                  and  (met_inf_report.doctor_dept = '{4}' or 'AAA' in ('{4}'))
                                  and  (met_inf_report.disease_code = '{5}' or 'AAA' in ('{5}'))
                                  order by met_inf_report.state,met_inf_report.report_id 
                                  
                ";
            }
            strSQLWhere = string.Format(strSQLWhere, dateType, beginDate, endDate, state, doctDept, diseaseCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 传入where
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns>实体数组，或者null</returns>
        public ArrayList GetCommonReportListByWhere(string sqlWhere)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            if (sqlWhere == null || sqlWhere == "")
            {
                return null;
            }
            return this.myGetCommonReport(strSQL + sqlWhere);
        }

        /// <summary>
        /// 根据报卡时间获取报卡列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByReportTime(DateTime beginTime, DateTime endTime)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByMore", ref strSQLWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByMore字段";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere,"report_date", beginTime.ToString(), endTime.ToString(),"AAA","AAA");
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// 根据报告编号和患者信息查询报告卡
        /// </summary>
        /// <returns></returns>
        public ArrayList GetCommonReportListByNOAndPatientInfo(string reportNO,string patientNO,string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            string sqlWhere = "";

            if (this.GetSQL("DCP.CommonReport.WhereByNOAndPatientInfo", ref sqlWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.WhereByNOAndPatientInfo字段";
                return null;
            }
            try
            {
                sqlWhere = string.Format(sqlWhere, reportNO, patientNO, patientName);
            }
            catch(Exception e)
            {
                this.Err += e.Message;
            }
            return this.myGetCommonReport(strSQL + sqlWhere);
        }
        /// <summary>
        /// 获取患者已经上报的报告卡，仅仅用于检测重复报卡
        /// </summary>
        /// <param name="patient">患者基本信息</param>
        /// <param name="diseaseCode">疾病代码</param>
        /// <returns>null出错</returns>
        public ArrayList GetPatientReportedCommonReport(FS.HISFC.Models.RADT.Patient patient, string diseaseCode)
        {
            string sqlWhere = "";
            if (this.Sql.GetSql("DCP.CommonReport.WhereIsReregister", ref sqlWhere) == -1)
            {
                this.Err = "没有找到DCP.CommonReport.Query字段";
                return null;
            }
            if (sqlWhere == null || sqlWhere == "")
            {
                return null;
            }
            if (diseaseCode.IndexOf("'") == -1)
            {
                diseaseCode = "'" + diseaseCode + "'";
            }
            string carNO = patient.PID.CardNO;
            if (carNO == null || carNO == "")
            {
                carNO = patient.PID.ID;
            }
            string age = this.GetAge(patient.Birthday);

            sqlWhere = string.Format(sqlWhere, diseaseCode, carNO, patient.Name, patient.Sex.ID, age, patient.PhoneHome, patient.AddressHome);
            return this.GetCommonReportListByWhere(sqlWhere);
        }
        #endregion

        #region 附卡管理

        /// <summary>
        /// 获取传染病附卡的参数
        /// </summary>
        /// <param name="additionReport">实体</param>
        /// <returns>参数数组</returns>
        private string[] myGetAdditionReportParm(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string[] strParm = {
                                additionReport.Report.ReportNO,
                                additionReport.PatientNO,
                                additionReport.PatientName,
                                additionReport.Memo
			};
            return strParm;
        }

        /// <summary>
        /// 获取报卡
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>报卡实体数组</returns>
        private FS.HISFC.DCP.Object.AdditionReport myGetAdditionReport(string strSQL)
        {
            FS.HISFC.DCP.Object.AdditionReport additionReport= new FS.HISFC.DCP.Object.AdditionReport();
            additionReport.Report = new FS.HISFC.DCP.Object.CommonReport();
            this.ProgressBarText = "正在检索报告卡信息...";
            this.ProgressBarValue = 0;
            this.ExecQuery(strSQL);
            try
            {
                while (this.Reader.Read())
                {
                    additionReport.Report.ReportNO = this.Reader[0].ToString();
                    additionReport.PatientNO = this.Reader[1].ToString();
                    additionReport.PatientName = this.Reader[2].ToString();
                    additionReport.ReportXML = this.Reader[3].ToString();
                    additionReport.Memo = this.Reader[4].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取取报告卡信息时，执行SQL出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            this.ProgressBarValue = -1;
            return additionReport;
        }

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <param name="additionReport">附卡实体</param>
        /// <returns></returns>
        public int UpdateAdditionReport(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.UpdateByID", ref strSql) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.UpdateByID字段";
                return -1;
            }

            try
            {
                string[] strParam=this.myGetAdditionReportParm(additionReport);
                strSql = string.Format(strSql, strParam);
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSql)+this.SetReportXML(additionReport);
        }

        /// <summary>
        /// 增加附卡信息
        /// </summary>
        /// <param name="additionReport">附卡实体</param>
        /// <returns></returns>
        public int InsertAdditionReport(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.Insert", ref strSql) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.Insert字段";
                return -1;
            }

            try
            {
                string[] strParam = this.myGetAdditionReportParm(additionReport);
                strSql=string.Format(strSql, strParam);
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return -1;
            }
            
            return this.ExecQuery(strSql)+this.SetReportXML(additionReport);
        }

        /// <summary>
        /// 删除附卡信息
        /// </summary>
        /// <param name="reportNO">报卡编号</param>
        /// <returns></returns>
        public int DeleteAdditionReport(string reportNO)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.DeleteByID", ref strSql) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.DeleteByID字段";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, reportNO);
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSql);
        }

        /// <summary>
        /// 获取附卡信息
        /// </summary>
        /// <param name="reportNO">报卡编号</param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionReport(string reportNO)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("DCP.AdditionReport.Query", ref strSql) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.Query字段";
                return null;
            }
            if (this.GetSQL("DCP.AdditionReport.WhereByID", ref strWhere) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.WhereByID字段";
                return null;
            }
            try
            {
                strWhere = string.Format(strWhere, reportNO);
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetAdditionReport(strSql+strWhere);
        }

        /// <summary>
        /// 根据报告卡号和疾病名称查找历史附卡，以最近的时间为最新
        /// </summary>
        /// <param name="reportNO"></param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionReportByIDAndDisease(string reportNO, string diseaseNO)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("DCP.AdditionReport.Query", ref strSql) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.Query字段";
                return null;
            }
            if (this.GetSQL("DCP.AdditionReport.WhereByIDAndDisease", ref strWhere) == -1)
            {
                this.Err = "没有找到DCP.AdditionReport.WhereByIDAndDisease字段";
                return null;
            }
            try
            {
                strWhere = string.Format(strWhere, reportNO,diseaseNO);
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetAdditionReport(strSql + strWhere);
        }

        /// <summary>
        /// 针对>4000以上的XML进行存储
        /// </summary>
        /// <param name="additionReport"></param>
        /// <returns></returns>
        public int SetReportXML(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string strSql = "update met_inf_additionreport set additionxml=:r where report_no='{0}'";
            try
            {
                strSql = string.Format(strSql, additionReport.Report.ReportNO);
                if (this.InputLong(strSql, additionReport.ReportXML) == -1)
                {
                    this.Err = "转换XML数据错误";
                    return -1;
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "初始化SQL语句出错" + e.Message;
                this.WriteErr();
                return -1;
            }
        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} 增加不报卡原因参数
        //2015-1-5-yeph

        /// <summary>
        ///不报卡原因参数
        /// </summary>
        /// <param name="report">不报卡实体</param>
        /// <returns></returns>

        private string[] myGetCommonReportOfNotParm(FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {
            string[] strParm = {
								   report.Patient.PID.PatientNO,
                                   report.DiagName.ToString(),
                                   report.Patient.PID.CardNO,	
                                   report.ReasonOfNot1,
                                   report.OtherName,
                                   report.ReportDoctor.ID,
								   report.DoctorDept.ID,
								   report.ReportTime.ToString()
                                  
			};
            return strParm;
        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} 增加插入不报卡原因表
        //2015-1-5-yeph

        /// <summary>
        ///插入不报卡原因
        /// </summary>
        /// <param name="report">不报卡实体</param>
        /// <returns></returns>

        public int InsertReportOfNot(FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {
            string strSQL = @"insert into met_inf_reasonofnot( clino_no,diag_name,card_no ,reasonofnot ,othername,doctorname,doctordept ,oper_date )
                               values( '{0}','{1}','{2}','{3}', '{4}', '{5}','{6}',to_date('{7}','yyyy-mm-dd hh24:mi:ss'))";

            string[] strParm = this.myGetCommonReportOfNotParm(report);  //取参数列表

            strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数。
            return this.ExecNoQuery(strSQL);

        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} 更新不报卡原因
        //2015-1-5-yeph
        /// <summary>
        /// 更新不报卡原因
        /// </summary>
        /// <param name="report">不报卡实体</param>
        /// <returns></returns>
        public int UpdateReportOfNot(FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {
            string strSQL = @"update met_inf_reasonofnot set clino_no ='{0}',
                                                             diag_name='{1}', 
                                                            card_no ='{2}',
                                                            reasonofnot = '{3}' ,
                                                            othername = '{4}',
                                                            doctorname = '{5}',
                                                            doctordept = '{6}' ,
                                                            oper_date= to_date('{7}','yyyy-mm-dd hh24:mi:ss')
                               where clino_no = '{0}'
                               and diag_name='{1}'
                                ";
           
            try
            {
                string[] strParm = this.myGetCommonReportOfNotParm(report);  //取参数列表
                strSQL = string.Format(strSQL, strParm);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql时候出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }
        // {9A497C15-596A-420d-8AA8-27766FFB760E} 获取不报卡原因
        //2015-1-5-yeph
        
        /// <summary>
        /// 获取不报卡原因
        /// </summary>
        /// <param name="strSQL">sql语句</param>
        /// <returns>报卡实体数组</returns>

        private ArrayList myGetReasonOfNot(string strSQL)
        {
            ArrayList al = new ArrayList();
            FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report;

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    report = new FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot();

                    report.Patient.PID.CardNO = this.Reader[0].ToString();
                    report.DiagName = this.Reader[1].ToString();
                    report.Patient.PID.ID= this.Reader[2].ToString();
                    report.ReasonOfNot1 = this.Reader[3].ToString();
                    report.OtherName = this.Reader[4].ToString();
                    report.ReportDoctor.ID = this.Reader[5].ToString();
                    report.DoctorDept.ID = this.Reader[6].ToString();
                    report.ReportTime = System.Convert.ToDateTime(this.Reader[7].ToString());
              
                    al.Add(report);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取取报告卡信息时，执行SQL出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }


  
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="report">不报卡实体</param>
        /// <returns></returns>

        public FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot GetReasonOfNot(string clino_no, string diagname)
        {
            string strSQL = @"SELECT clino_no,
                                    diag_name,
                                    card_no,
                                   REASONOFNOT,
                                   OTHERNAME,
                                   DOCTORNAME,
                                   DOCTORDEPT,
                                   OPER_DATE                                           
                                   
                              FROM MET_INF_REASONOFNOT
                             WHERE CLINO_NO = '{0}'
                             AND DIAG_NAME='{1}'
                                  ";

            ArrayList al = new ArrayList();

            strSQL = string.Format(strSQL, clino_no, diagname);

            al = this.myGetReasonOfNot(strSQL);

            if (al != null && al.Count >= 1)
            {
                return al[0] as FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot;
            }

            return null;
        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} 判断是否已经填报过不报卡原因是时传入where
        //2015-1-5-yeph
        /// <summary>
        /// 判断是否已经填报过不报卡原因是时传入where
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns>实体数组，或者null</returns>
        public ArrayList GetReasonOfNotNeed(string sqlWhere)
        {
            string strSQL = @" SELECT clino_no,
                                    diag_name,
                                    card_no,
                                   REASONOFNOT,
                                   OTHERNAME,
                                   DOCTORNAME,
                                   DOCTORDEPT,
                                   OPER_DATE                                           
                                   
                              FROM MET_INF_REASONOFNOT";
            
            if (sqlWhere == null || sqlWhere == "")
            {
                return null;
            }
            return this.myGetReasonOfNot(strSQL + sqlWhere);
        }



        #endregion

        #region 性病报卡卡管理
        #endregion
    }
}
