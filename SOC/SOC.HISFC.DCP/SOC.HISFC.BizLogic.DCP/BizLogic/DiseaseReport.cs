using System;
using System.Collections;
using System.Collections.Generic;

namespace FS.SOC.HISFC.BizLogic.DCP
{
    /// <summary>
    /// DiseaseReport<br></br>
    /// [��������: DiseaseReport]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-8-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class DiseaseReport : FS.SOC.HISFC.BizLogic.DCP.BizLogic.DataBase
    {
        public DiseaseReport()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��̬/����

        /// <summary>
        /// ���濨���
        /// </summary>
        private const int ReportNOMedian = 5;

        #endregion

        #region ��������

        /// <summary>
        /// ��ȡ��Ⱦ�����濨�Ĳ���
        /// </summary>
        /// <param name="commonReport">ʵ��</param>
        /// <returns>��������</returns>
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
        /// ��ȡ����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <returns>����ʵ������</returns>
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
                this.Err = "��ȡȡ���濨��Ϣʱ��ִ��SQL����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ���濨���
        /// </summary>
        /// <returns></returns>
        private string GetCommonReportNO()
        {
            string strSQL = "";
            string no = "1";

            //������ȱ�����+1
            if (this.GetSQL("DCP.CommonReport.GetNO", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.GetNO�ֶ�";

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
                this.Err = "��ȡȡ���濨���ʱ��ִ��SQL����" + ex.Message;
                return "a";
            }
            finally
            {
                this.Reader.Close();
            }

            //ȫ����
            no = this.GetDateTimeFromSysDateTime().ToString( "yyyy" ) + no.PadLeft( ReportNOMedian, '0' );

            return no;
        }

        /// <summary>
        /// ��ȡ���濨����ˮ��
        /// </summary>
        /// <returns>���濨����ˮ��</returns>
        public string GetCommonReportID()
        {
            string reportID = this.GetSequence("DCP.CommonReport.GetID");
            reportID = reportID.PadLeft(10, '0');
            return reportID;
        }

        /// <summary>
        /// ���봫Ⱦ��������Ϣ
        /// </summary>
        /// <param name="commonReport">��Ⱦ������ʵ��</param>
        /// <returns>-1ʧ��</returns>
        public int InsertCommonReport(FS.HISFC.DCP.Object.CommonReport commonReport)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Insert�ֶ�";
                return -1;
            }
            string reportID = this.GetCommonReportID();//��ȡ����		
            commonReport.ID = reportID;
            string reportNO = this.GetCommonReportNO();//��ȡ���		
            commonReport.ReportNO = reportNO;
            try
            {
                string[] strParm = this.myGetCommonReportParm(commonReport);  //ȡ�����б�

                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���±��濨
        /// </summary>
        /// <param name="commonReport">����ʵ��</param>
        /// <returns></returns>
        public int UpdateCommonReport(FS.HISFC.DCP.Object.CommonReport commonReport)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.UpdateByID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.UpdateByID�ֶ�";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetCommonReportParm(commonReport);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���濨ɾ��
        /// </summary>
        /// <param name="ReportID">���</param>
        /// <returns></returns>
        public int DeleteCommonReport(string ReportID)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.DeleteByID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.DeleteByID�ֶ�";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ReportID);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ�������б��濨
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        /// <returns>ʵ������</returns>
        public ArrayList GetCommonReportList(string deptCode)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByDept", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByDept�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, deptCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ���ݻ��ߺŻ�ȡ���߱��濨
        /// </summary>
        /// <param name="patientNO">���￨��|סԺ��</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByPatientNO(string patientNO)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByPatientNO", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByPatientNO�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, patientNO);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ���ݻ���������ȡ���߱��濨
        /// </summary>
        /// <param name="patientName">���￨��|סԺ��</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByPatientName(string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
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
        /// ����״̬��ȡ���濨
        /// </summary>
        /// <param name="state">����״̬(����in)</param>
        /// <returns>����ʵ������</returns>
        public ArrayList GetvReportListByState(string state)
        {
            if (state.IndexOf("'") == -1)
            {
                state = "'" + state + "'";
            }
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereStateIn", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereStateIn�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ���ݻ�������ģ����ȡ���濨
        /// </summary>
        /// <param name="state">����</param>
        /// <returns>����ʵ������</returns>
        public ArrayList GetReportListByPatientName(string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByName", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByName�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, patientName);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ������ˮ�Ų�ѯ����
        /// </summary>
        /// <param name="reportID">������ˮ��</param>
        /// <returns>����ʵ�� ����null</returns>
        public FS.HISFC.DCP.Object.CommonReport GetCommonReportByID(string reportID)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByID", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByID�ֶ�";
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
        /// ���ݱ�Ų�ѯ���濨(��ȷ��ѯ)
        /// </summary>
        /// <param name="reportNO"></param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.CommonReport GetCommonReportByNO(string reportNO)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();
            if(this.GetSQL("DCP.CommonReport.Query",ref strSQL) == -1) 
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByNO", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByNO�ֶ�";
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
        /// ��ȡ����ĳ״̬�ı��濨
        /// </summary>
        /// <param name="state">״̬</param>
        /// <param name="deptCode">����</param>
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
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByStateAndDept", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByStateAndDept�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state, deptCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ��ȡ����ĳ״̬�ı��濨
        /// </summary>
        /// <param name="state">״̬</param>
        /// <param name="doctorCode">����ҽ��</param>
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
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByStateAndDoctor", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByStateAndDoctor�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, state, doctorCode);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ��������ѯ
        /// </summary>
        /// <param name="doctDept">���Ҵ���</param>
        /// <param name="dateType">�ֶ�</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="state">״̬</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByMore(string dateType, string beginDate, string endDate, string reportState, string doctDept)
        {
            string state = reportState;
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByMore", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByMore�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere, dateType, beginDate, endDate, state, doctDept);
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ��������ѯ
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="dateType">�ֶ�</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="state">״̬</param>
        /// <param name="diseaseCode">��������</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByMore(string dateType, string beginDate, string endDate, string reportState, string doctDept, string diseaseCode)
        {
            string state = reportState;
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
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
        /// ����where
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns>ʵ�����飬����null</returns>
        public ArrayList GetCommonReportListByWhere(string sqlWhere)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            if (sqlWhere == null || sqlWhere == "")
            {
                return null;
            }
            return this.myGetCommonReport(strSQL + sqlWhere);
        }

        /// <summary>
        /// ���ݱ���ʱ���ȡ�����б�
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns></returns>
        public ArrayList GetCommonReportListByReportTime(DateTime beginTime, DateTime endTime)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string strSQLWhere = "";
            if (this.GetSQL("DCP.CommonReport.WhereByMore", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByMore�ֶ�";
                return null;
            }
            strSQLWhere = string.Format(strSQLWhere,"report_date", beginTime.ToString(), endTime.ToString(),"AAA","AAA");
            return this.myGetCommonReport(strSQL + strSQLWhere);
        }

        /// <summary>
        /// ���ݱ����źͻ�����Ϣ��ѯ���濨
        /// </summary>
        /// <returns></returns>
        public ArrayList GetCommonReportListByNOAndPatientInfo(string reportNO,string patientNO,string patientName)
        {
            string strSQL = "";
            if (this.GetSQL("DCP.CommonReport.Query", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
                return null;
            }
            string sqlWhere = "";

            if (this.GetSQL("DCP.CommonReport.WhereByNOAndPatientInfo", ref sqlWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.WhereByNOAndPatientInfo�ֶ�";
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
        /// ��ȡ�����Ѿ��ϱ��ı��濨���������ڼ���ظ�����
        /// </summary>
        /// <param name="patient">���߻�����Ϣ</param>
        /// <param name="diseaseCode">��������</param>
        /// <returns>null����</returns>
        public ArrayList GetPatientReportedCommonReport(FS.HISFC.Models.RADT.Patient patient, string diseaseCode)
        {
            string sqlWhere = "";
            if (this.Sql.GetSql("DCP.CommonReport.WhereIsReregister", ref sqlWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.CommonReport.Query�ֶ�";
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

        #region ��������

        /// <summary>
        /// ��ȡ��Ⱦ�������Ĳ���
        /// </summary>
        /// <param name="additionReport">ʵ��</param>
        /// <returns>��������</returns>
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
        /// ��ȡ����
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <returns>����ʵ������</returns>
        private FS.HISFC.DCP.Object.AdditionReport myGetAdditionReport(string strSQL)
        {
            FS.HISFC.DCP.Object.AdditionReport additionReport= new FS.HISFC.DCP.Object.AdditionReport();
            additionReport.Report = new FS.HISFC.DCP.Object.CommonReport();
            this.ProgressBarText = "���ڼ������濨��Ϣ...";
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
                this.Err = "��ȡȡ���濨��Ϣʱ��ִ��SQL����" + ex.Message;
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
        /// �޸ĸ�����Ϣ
        /// </summary>
        /// <param name="additionReport">����ʵ��</param>
        /// <returns></returns>
        public int UpdateAdditionReport(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.UpdateByID", ref strSql) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.UpdateByID�ֶ�";
                return -1;
            }

            try
            {
                string[] strParam=this.myGetAdditionReportParm(additionReport);
                strSql = string.Format(strSql, strParam);
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSql)+this.SetReportXML(additionReport);
        }

        /// <summary>
        /// ���Ӹ�����Ϣ
        /// </summary>
        /// <param name="additionReport">����ʵ��</param>
        /// <returns></returns>
        public int InsertAdditionReport(FS.HISFC.DCP.Object.AdditionReport additionReport)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.Insert", ref strSql) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.Insert�ֶ�";
                return -1;
            }

            try
            {
                string[] strParam = this.myGetAdditionReportParm(additionReport);
                strSql=string.Format(strSql, strParam);
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return -1;
            }
            
            return this.ExecQuery(strSql)+this.SetReportXML(additionReport);
        }

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="reportNO">�������</param>
        /// <returns></returns>
        public int DeleteAdditionReport(string reportNO)
        {
            string strSql = "";
            if (this.GetSQL("DCP.AdditionReport.DeleteByID", ref strSql) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.DeleteByID�ֶ�";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, reportNO);
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSql);
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="reportNO">�������</param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionReport(string reportNO)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("DCP.AdditionReport.Query", ref strSql) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.Query�ֶ�";
                return null;
            }
            if (this.GetSQL("DCP.AdditionReport.WhereByID", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.WhereByID�ֶ�";
                return null;
            }
            try
            {
                strWhere = string.Format(strWhere, reportNO);
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetAdditionReport(strSql+strWhere);
        }

        /// <summary>
        /// ���ݱ��濨�źͼ������Ʋ�����ʷ�������������ʱ��Ϊ����
        /// </summary>
        /// <param name="reportNO"></param>
        /// <returns></returns>
        public FS.HISFC.DCP.Object.AdditionReport GetAdditionReportByIDAndDisease(string reportNO, string diseaseNO)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("DCP.AdditionReport.Query", ref strSql) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.Query�ֶ�";
                return null;
            }
            if (this.GetSQL("DCP.AdditionReport.WhereByIDAndDisease", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�DCP.AdditionReport.WhereByIDAndDisease�ֶ�";
                return null;
            }
            try
            {
                strWhere = string.Format(strWhere, reportNO,diseaseNO);
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetAdditionReport(strSql + strWhere);
        }

        /// <summary>
        /// ���>4000���ϵ�XML���д洢
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
                    this.Err = "ת��XML���ݴ���";
                    return -1;
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "��ʼ��SQL������" + e.Message;
                this.WriteErr();
                return -1;
            }
        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} ���Ӳ�����ԭ�����
        //2015-1-5-yeph

        /// <summary>
        ///������ԭ�����
        /// </summary>
        /// <param name="report">������ʵ��</param>
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

        // {9A497C15-596A-420d-8AA8-27766FFB760E} ���Ӳ��벻����ԭ���
        //2015-1-5-yeph

        /// <summary>
        ///���벻����ԭ��
        /// </summary>
        /// <param name="report">������ʵ��</param>
        /// <returns></returns>

        public int InsertReportOfNot(FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {
            string strSQL = @"insert into met_inf_reasonofnot( clino_no,diag_name,card_no ,reasonofnot ,othername,doctorname,doctordept ,oper_date )
                               values( '{0}','{1}','{2}','{3}', '{4}', '{5}','{6}',to_date('{7}','yyyy-mm-dd hh24:mi:ss'))";

            string[] strParm = this.myGetCommonReportOfNotParm(report);  //ȡ�����б�

            strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            return this.ExecNoQuery(strSQL);

        }

        // {9A497C15-596A-420d-8AA8-27766FFB760E} ���²�����ԭ��
        //2015-1-5-yeph
        /// <summary>
        /// ���²�����ԭ��
        /// </summary>
        /// <param name="report">������ʵ��</param>
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
                string[] strParm = this.myGetCommonReportOfNotParm(report);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��sqlʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }
        // {9A497C15-596A-420d-8AA8-27766FFB760E} ��ȡ������ԭ��
        //2015-1-5-yeph
        
        /// <summary>
        /// ��ȡ������ԭ��
        /// </summary>
        /// <param name="strSQL">sql���</param>
        /// <returns>����ʵ������</returns>

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
                this.Err = "��ȡȡ���濨��Ϣʱ��ִ��SQL����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }


  
        /// <summary>
        /// ��ѯ��Ϣ
        /// </summary>
        /// <param name="report">������ʵ��</param>
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

        // {9A497C15-596A-420d-8AA8-27766FFB760E} �ж��Ƿ��Ѿ����������ԭ����ʱ����where
        //2015-1-5-yeph
        /// <summary>
        /// �ж��Ƿ��Ѿ����������ԭ����ʱ����where
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns>ʵ�����飬����null</returns>
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

        #region �Բ�����������
        #endregion
    }
}
