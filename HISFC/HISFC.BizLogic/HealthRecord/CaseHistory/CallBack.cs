using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.HealthRecord.CaseHistory
{
    /// <summary>
    /// [instruction: case history callback manager]
    /// [create date: Mar.4 2010]
    /// [create by zhao.chf]
    /// [����5.0�汾 2011-8-2 chengym]
    /// </summary>
    public class CallBack : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ��ʼ����������б�
        /// </summary>
        /// <param name="arraySpe"></param>
        public CallBack(string arraySpe,int firTimeOut ,int DeaTimeOut,int SecTimeOut ):base()
        {
            arraySpecifyDept = arraySpe;
            firstTimeOut = firTimeOut;
            secondTimeOut = SecTimeOut;
            deathTimeOut = DeaTimeOut;
        }

        public CallBack()
            : base()
        { ;}
        private string arraySpecifyDept;
        /// <summary>
        /// ������տ���
        /// </summary>
        public string ArraySpecifyDept
        {
            get { return arraySpecifyDept; }
            set { arraySpecifyDept = value; }
        }
        /// <summary>
        /// ��ʱ����
        /// </summary>
        private int firstTimeOut = 5;
        /// <summary>
        /// ���ʱ����
        /// </summary>
        private int secondTimeOut = 8;

        private int deathTimeOut = 7;
        #region ��������ҵ��
        /// <summary>
        /// ����sql�������ѯ���ݵ��ڴ�ʵ��
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>ʵ�弯</returns>
        private List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> GetCallBackInfo(string sql)
        {
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb = new List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack>();
            try
            {
                if (this.ExecQuery(sql) < 0)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.CaseHistory.CallBack c = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();
                    c.Patient.ID = this.Reader[0].ToString();//סԺ��ˮ��
                    c.Patient.PID.PatientNO = this.Reader[1].ToString();//סԺ��
                    c.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[2].ToString();//��������
                    c.Patient.PVisit.AdmittingDoctor.ID = this.Reader[3].ToString();//סԺҽ������
                    c.Patient.PVisit.OutTime = Convert.ToDateTime(this.Reader[4].ToString());//��Ժ����
                    c.IsCallback = this.Reader[5].ToString();//�Ƿ����  1 �ѻ��� 0 δ����
                    c.CallbackOper.ID = this.Reader[6].ToString();//������
                    if (c.IsCallback == "1")
                    {
                        c.CallbackOper.OperTime = Convert.ToDateTime(this.Reader[7].ToString());//��������
                    }
                    c.IsDocument = this.Reader[8].ToString();//�Ƿ�鵵
                    c.DocumentOper.ID = this.Reader[9].ToString();//�鵵��
                    if (c.IsDocument == "1")
                    {
                        c.DocumentOper.OperTime = Convert.ToDateTime(this.Reader[10].ToString());//�鵵����
                    }
                    c.Patient.Name = this.Reader[11].ToString();//��������
                    c.Patient.PVisit.PatientLocation.Dept.Name = this.Reader[12].ToString();//������������
                    c.Patient.PVisit.AdmittingDoctor.Name = this.Reader[13].ToString();//ҽ������
                    c.CallbackOper.Name = this.Reader[14].ToString();//����������
                    c.DocumentOper.Name = this.Reader[15].ToString();//15�鵵������
                    c.Patient.PVisit.PatientLocation.Bed.ID = this.Reader[16].ToString();//��λ��
                    c.Patient.PVisit.ZG.ID = this.Reader[17].ToString();// ��Ժת�����
                    cb.Add(c);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return cb;
        }

        /// <summary>
        /// ���ݳ�Ժʱ�䷶Χ���Ҳ����ٻ�����
        /// </summary>
        /// <param name="isCallback">�Ƿ����</param>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns>��������ʵ�弯</returns>
        public List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> QueryCaseHistoryCallBackInfo(string isCallback, DateTime dtBegin, DateTime dtEnd,string deptCode)
        {
            string sql = string.Empty;
            try
            {
                if (isCallback == "1")
                {
                    if ((this.Sql.GetSql("Case.Callback.QueryInfo.1", ref sql)) < 0)
                    {
                        return null;
                    }
                }
                else
                {
                    if ((this.Sql.GetSql("Case.Callback.QueryInfo.1.UnCallBack", ref sql)) < 0)
                    {
                        return null;
                    }
                }
                sql = string.Format(sql, isCallback, dtBegin, dtEnd, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
            }

            return GetCallBackInfo(sql);
        }

        /// <summary>
        /// ���ݻ���ʱ������ѻ��յĲ�����Ϣ
        /// </summary>
        /// <param name="callBackDateBegin">��ʼʱ��</param>
        /// <param name="callBackDateEnd">����ʱ��</param>
        /// <param name="deptCode">���Ҵ���</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> QueryCaseHistoryCallBackInfo(DateTime callBackDateBegin, DateTime callBackDateEnd, string deptCode)
        {
            string sql = string.Empty;
            string where = string.Empty;

            try
            {
                if ((this.Sql.GetSql("Case.Callback.QueryInfo.2", ref sql)) < 0)
                {
                    return null;
                }
                sql = string.Format(sql, callBackDateBegin, callBackDateEnd, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }

            return GetCallBackInfo(sql);
        }

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ�������ձ� �鲻�ҵ������򷵻ؿ� 
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>������Ϣ</returns>
        public List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> QueryCaseHistorycallBackInfoByInpatientNO(string inpatientNO)
        {
            string sql = string.Empty;
            string where = string.Empty;

            try
            {
                if ((this.Sql.GetSql("Case.Callback.QueryInfo", ref sql)) < 0)
                {
                    return null;
                }
                if (this.Sql.GetSql("Case.Callback.QueryInfo.Where.1", ref where) < 0)
                {
                    return null;
                }
                sql += where;
                sql = string.Format(sql, inpatientNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }

            return GetCallBackInfo(sql);

        }



        /// <summary>
        /// ���²���������Ϣ ���ղ���
        /// </summary>
        /// <param name="cb">����ʵ��</param>
        /// <returns>�ɹ� �Ǹ� ʧ��-1</returns>
        public int UpdateCaseHistoryCallBackInfo(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {

            //1�� סԺ�����Բ��˳�Ժ��5���ڻ��գ���������7���ڻ��գ�
            int sevenTimeout = 0; //�����������Ϊ1 δ����Ϊ0
            int tenTimeout = 0; //��ʱ��ʹ��
            this.TimeOutOfCallBack(cb.Patient.PVisit.ZG.ID, cb.Patient.PVisit.OutTime, cb.CallbackOper.OperTime, cb.Patient.PVisit.PatientLocation.Dept.ID, ref sevenTimeout, ref tenTimeout);
            string sql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Case.Callback.UpdateCallbackInfo.1", ref sql) < 0)
                {
                    return -1;
                }

                sql = string.Format(sql, cb.Patient.ID, cb.IsCallback, cb.CallbackOper.ID, cb.CallbackOper.OperTime, sevenTimeout, tenTimeout);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// �������ճ���
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>�ɹ� �Ǹ� ʧ��-1</returns>
        public int CancelCaseHistoryCallBackInfo(string inpatientNO)
        {
            string sql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Case.Callback.UpdateCallbackInfo.1", ref sql) < 0)
                {
                    return -1;
                }
                sql = string.Format(sql, inpatientNO, "0", "", "0001-01-01 00:00:00", 0, 0);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

       
        #endregion

        #region ��Ժ�Ǽǽ������

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ�������ձ� �鲻�ҵ������򷵻ؿ� 
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>������Ϣ</returns>
        public FS.HISFC.Models.HealthRecord.CaseHistory.CallBack QueryCaseHistorycallBackInfoByIninpatientNO(string inpatientNO)
        {
            string sql = string.Empty;
            string where = string.Empty;

            try
            {
                if ((this.Sql.GetSql("Case.Callback.QueryInfo", ref sql)) < 0)
                {
                    return null;
                }
                //if (this.Sql.GetSql("Case.Callback.QueryInfo.2.Where.1", ref where) < 0)
                //{
                //    return null;
                //}
                where = @"  where c.inpatient_no  = '{0}'";
                sql += where;
                sql = string.Format(sql, inpatientNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }

            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb = GetCallBackInfo(sql);
            if (cb == null || cb.Count <= 0)
            { return null; }
            return cb[0];

        }


        /// <summary>
        /// ��Ժ�Ǽ�ʱ���벡����Ϣ���������ձ�
        /// </summary>
        /// <param name="cb">��������ʵ��</param>
        /// <returns>�ɹ� �Ǹ� ʧ��-1</returns>
        public int InsertInpatientOutInfo(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {
            string sql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Case.Callback.InsertCallbackInfo.1", ref sql) < 0)
                {
                    return -1;
                }
                sql = string.Format(sql, cb.Patient.ID, cb.Patient.PID.PatientNO, cb.Patient.PVisit.PatientLocation.Dept.ID, cb.Patient.PVisit.AdmittingDoctor.ID, cb.Patient.PVisit.OutTime, "0");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// �Ѿ��ٻع��Ĳ�������Ժ�Ǽ� ���粡��û�л��� �򸲸�֮ǰ����סԺ��ˮ��֮���������Ϣ
        /// </summary>
        /// <param name="cb">�����ջ���Ϣ��</param>
        /// <returns>�ɹ��Ǹ� ʧ�ܸ�</returns>
        public int UpdateInpatientOutInfo(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {
            string sql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Case.Callback.UpdateCallbackInfo.2", ref sql) < 0)
                {
                    return -1;
                }
                sql = string.Format(sql, cb.Patient.ID, cb.Patient.PID.PatientNO, cb.Patient.PVisit.AdmittingDoctor.ID, cb.Patient.PVisit.AdmittingDoctor.ID, cb.Patient.PVisit.OutTime, "0");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ��Ժ�Ǽ��ٻ� �Ҳ��˲���δ���� ɾ���������ձ��в�����ؼ�¼  P.S.�˺�����ʱ���� ��Ϊ������Ǵ���
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns>�ɹ��Ǹ� ʧ��-1</returns>
        public int DelNotCallbackInfo(string inpatientNO)
        {
            string sql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Case.Calllback.DeleteCallbackInfo.1", ref sql) < 0)
                {
                    return -1;
                }
                sql = string.Format(sql, inpatientNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
        #endregion

        #region ����ҵ��
        /// <summary>
        /// �ж�δ���ղ����Ƿ�ʱ �˴�����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="outDate">��Ժ����</param>
        /// <param name="seven">����</param>
        /// <param name="ten">ʮ��</param>
        private void OutTimeJudger(string inpatientNO, DateTime outDate, out int seven, out int ten)
        {

            DateTime curDate = this.GetDateTimeFromSysDateTime();

            //�ж��Ƿ�ʱ 7�����10��
            TimeSpan tsFrom = new TimeSpan(outDate.Ticks);
            TimeSpan tsTo = new TimeSpan(curDate.Ticks);
            TimeSpan ts = tsTo.Subtract(tsFrom).Duration();
            int daysDiff = ts.Days;
            if (daysDiff > 9)
            {
                if (daysDiff > 12)
                {
                    seven = 0;
                    ten = 1;
                }
                else
                {
                    seven = 1;
                    ten = 0;
                }
            }
            else
            {
                seven = 0;
                ten = 0;
            }
        }
        /// <summary>
        /// ���������ʲ�ѯ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetCaseCallbackPercent(DateTime begin, DateTime end)
        {
            ArrayList al = new ArrayList();
            string sql = string.Empty;
            if (this.Sql.GetSql("Case.Callback.QueryInfo.CallBackPerCent", ref sql) < 0)
            {
                return null;
            }
            try
            {
                sql = string.Format(sql, begin, end);

                if (this.ExecQuery(sql) < 0)
                {
                    return null;
                }

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();
                    cb.Patient.ID = this.Reader[0].ToString(); //���Ҵ���
                    cb.Patient.Name = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1]).ToString(); //�ܲ�����
                    cb.Patient.Memo = (FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]) * 100).ToString("F2") + "%"; //���������
                    cb.Patient.UserCode = this.Reader[3].ToString() + "(" + (FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3]) * 10).ToString() + ")"; // �����첡����������
                    cb.Patient.WBCode = (FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]) * 100).ToString("F2") + "%"; //ʮ�������
                    cb.Patient.SpellCode = this.Reader[5].ToString() + "(" + (FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]) * 30).ToString() + ")"; // ��10�� ������
                    cb.Patient.PID.PatientNO = this.Reader[6].ToString();
                    cb.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[7].ToString();
                    al.Add(cb);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ������������ϸ��ѯ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="dept_code"></param>
        /// <returns></returns>
        public ArrayList GetCaseCallbackPercentDetail(DateTime begin, DateTime end,string dept_code)
        {
            ArrayList al = new ArrayList();
            string sql = string.Empty;
            if (this.Sql.GetSql("Case.Callback.QueryInfo.CallBackPerCentDetail", ref sql) < 0)
            {
                return null;
            }
            try
            {
                sql = string.Format(sql, begin, end,dept_code);

                if (this.ExecQuery(sql) < 0)
                {
                    return null;
                }

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();
                    cb.Patient.PID.PatientNO = this.Reader[0].ToString(); //סԺ��
                    cb.Patient.Name = this.Reader[1].ToString(); //����
                    cb.Patient.PVisit.AdmittingDoctor.Name = this.Reader[2].ToString(); //ҽ������
                    cb.Patient.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3].ToString());//��Ժ����
                    cb.CallbackOper.ID = this.Reader[4].ToString(); // ������Ա
                    cb.CallbackOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());//��������
                    cb.CallbackOper.Memo = this.Reader[6].ToString(); //�������
                    al.Add(cb);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ��ȡ��ʱ���յĲ�����ϸ��Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList GetTimeOutSpecifyInfo(string deptCode, DateTime begin, DateTime end,params string[] isCallBack)
        {
            ArrayList al = new ArrayList();
            string sql = string.Empty;
            if (this.Sql.GetSql("Case.Callback.QueryInfo.OutTimeCallBack", ref sql) < 0)
            {
                return null;
            }
            try
            {
                sql = string.Format(sql, deptCode, begin, end);
                if (this.ExecQuery(sql) < 0)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.CaseHistory.CallBack c = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();
                    c.Patient.ID = this.Reader[0].ToString();
                    c.Patient.PID.PatientNO = this.Reader[1].ToString();
                    c.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[2].ToString();
                    c.Patient.PVisit.AdmittingDoctor.ID = this.Reader[3].ToString();
                    c.Patient.PVisit.OutTime = Convert.ToDateTime(this.Reader[4].ToString());
                    c.IsCallback = this.Reader[5].ToString();
                    c.CallbackOper.ID = this.Reader[6].ToString();
                    if (c.IsCallback == "1")
                    {
                        c.CallbackOper.OperTime = Convert.ToDateTime(this.Reader[7].ToString());
                    }
                    c.IsDocument = this.Reader[8].ToString();
                    c.DocumentOper.ID = this.Reader[9].ToString();
                    if (c.IsDocument == "1")
                    {
                        c.DocumentOper.OperTime = Convert.ToDateTime(this.Reader[10].ToString());
                    }
                    c.Patient.Name = this.Reader[11].ToString();
                    c.Patient.PVisit.PatientLocation.Dept.Name = this.Reader[12].ToString();
                    c.Patient.PVisit.AdmittingDoctor.Name = this.Reader[13].ToString();
                    c.CallbackOper.Name = this.Reader[14].ToString();
                    c.DocumentOper.Name = this.Reader[15].ToString();
                    c.SevenDaysTimeout = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16]);
                    c.FourteenDaysTimeout = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17]);
                    c.Patient.PVisit.PatientLocation.NurseCell.Name = this.Reader[18].ToString();
                    al.Add(c);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ���ݳ�Ժʱ��Ϳ��Ҳ�ѯ���յĲ�������
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="ds">������ݼ�</param>
        public void GetIsCallbackNum(string deptCode, DateTime begin, DateTime end, ref DataSet ds)
        {
//            string sql = @"SELECT  
//	                            (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE = c.dept_code)   ��������,
//	                            count(c.inpatient_no) ���ղ�����
//                            FROM DONGGUAN_CASEHISTORY_CALLBACK c
//                            WHERE c.IS_CALLBACK = '1' 
//                            AND c.OUT_DATE BETWEEN '{0}' AND '{1}'
//                            AND (c.DEPT_CODE = '{2}' OR 'ALL' = '{2}' or '' = '{2}')
//                            GROUP BY c.DEPT_CODE";
            string sql = @"SELECT  
                        (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE = c.dept_code)   ��������,
                        count(c.inpatient_no) ���ղ�����
                         FROM MET_CAS_CALLBACK c
                         WHERE c.IS_CALLBACK = '1' 
                        AND c.OUT_DATE BETWEEN to_date('{0}','yyyy-mm-dd HH24:mi:ss')  AND to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
                        AND (c.DEPT_CODE = '{2}' OR 'ALL' = '{2}' or '' = '{2}')
                        GROUP BY c.DEPT_CODE";
            try
            {
                sql = string.Format(sql, begin, end, deptCode);
            }
            catch (System.FormatException formatEx)
            {
                this.Err = formatEx.Message.ToString();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
            }
            catch
            {
                this.Err = "δ֪�쳣��";
            }

            this.ExecQuery(sql, ref ds);
        }



        /// <summary>
        /// ���ݻ���ʱ��Ϳ��Ҳ�ѯ���յĲ�������
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="ds">������ݼ�</param>
        public void GetIsCallbackNumByCallDate(string deptCode, DateTime begin, DateTime end, ref DataSet ds)
        {
//            string sql = @"SELECT  
//	                            (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE = c.dept_code)   ��������,
//	                            count(c.inpatient_no) ���ղ�����
//                            FROM DONGGUAN_CASEHISTORY_CALLBACK c
//                            WHERE c.IS_CALLBACK = '1' 
//                            AND c.callback_oper_date BETWEEN '{0}' AND '{1}'
//                            AND (c.DEPT_CODE = '{2}' OR 'ALL' = '{2}' or '' = '{2}')
//                            GROUP BY c.DEPT_CODE";
            string sql = @"SELECT  
                        (SELECT d.DEPT_NAME FROM COM_DEPARTMENT d WHERE d.DEPT_CODE = c.dept_code)   ��������,
                        count(c.inpatient_no) ���ղ�����
                         FROM MET_CAS_CALLBACK c
                         WHERE c.IS_CALLBACK = '1' 
                        AND c.callback_oper_date BETWEEN to_date('{0}','yyyy-mm-dd HH24:mi:ss')  AND to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
                        AND (c.DEPT_CODE = '{2}' OR 'ALL' = '{2}' or '' = '{2}')
                        GROUP BY c.DEPT_CODE";
            try
            {
                sql = string.Format(sql, begin, end, deptCode);
            }
            catch (System.FormatException formatEx)
            {
                this.Err = formatEx.Message.ToString();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
            }
            catch
            {
                this.Err = "δ֪�쳣��";
            }

            this.ExecQuery(sql, ref ds);
        }
        #endregion

        #region �޸Ĳ�������ʱ��
        public int UpdateCallBackDateByInpatientNO(string inpatientNO, DateTime callDate)
        {
            string sql = @"
                             UPDATE DONGGUAN_CASEHISTORY_CALLBACK 
                             SET CALLBACK_OPER_DATE = '{1}'
                             WHERE INPATIENT_NO = '{0}'
                             AND IS_CALLBACK = '1'
                        ";
            try
            {
                sql = string.Format(sql, inpatientNO, callDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
        #endregion

        /// <summary>
        /// ʱ����ڰ�����������������
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private int GetAllGeneralHolidaysByDateTime(DateTime dtBegin,DateTime dtEnd)
        {
            DateTime[] ret = null;

            string temp = "Saturday;Sunday";


            ArrayList al = new ArrayList();

            for (DateTime dt = dtBegin; dt <= dtEnd; dt = dt.AddDays(1))
            {
                if (temp.Contains(dt.DayOfWeek.ToString()))
                {
                    al.Add(dt);
                }
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(5);
                }
            }
            int Tot = 0;
            if (al != null)
            {
                Tot = al.Count;
            }
            return Tot;
        }

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ�������ձ� �鲻�ҵ������򷵻ؿ� 
        /// QueryCaseHistorycallBackInfoByInpatientNO  ��
        /// </summary>
        /// <param name="patientNO">סԺ��ˮ��</param>
        /// <param name="Type">����״̬ 1�ѻ��� 0δ����</param>
        /// <returns>������Ϣ</returns>
        public List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> QueryCaseHistorycallBackInfoByPatientNO(string patientNO,string Type)
        {
            string sql = string.Empty;

            try
            {
                if (Type == "1")
                {
                    if (this.Sql.GetSql("Case.Callback.QueryInfo.CallBack", ref sql) < 0)
                    {
                        return null;
                    }
                    sql = string.Format(sql, patientNO,patientNO.TrimStart('0'));
                }
                else
                {
                    if (this.Sql.GetSql("Case.Callback.QueryInfo.UnCallBack", ref sql) < 0)
                    {
                        return null;
                    }
                    sql = string.Format(sql, patientNO);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return null;
            }

            return GetCallBackInfo(sql);

        }


        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="cb">����ʵ��</param>
        /// <returns>�ɹ� �Ǹ� ʧ��-1</returns>
        public int InsertCaseHistoryCallBackInfo(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {
            //1�� סԺ�����Բ��˳�Ժ��5���ڻ��գ���������7���ڻ��գ�
            string sql = string.Empty;
            int sevenTimeout = 0;
            int tenTimeout = 0;
            if (cb == null)
            {
                return -1;
            }

            DateTime dtOutDate = cb.Patient.PVisit.OutTime;
            DateTime dtOperDate = cb.CallbackOper.OperTime;
            this.TimeOutOfCallBack(cb.Patient.PVisit.ZG.ID, dtOutDate, dtOperDate, cb.Patient.PVisit.PatientLocation.Dept.ID, ref sevenTimeout, ref tenTimeout);
            //TimeSpan tsFrom = new TimeSpan(dtOutDate.Ticks);
            //TimeSpan tsTo = new TimeSpan(dtOperDate.Ticks);
            //TimeSpan ts = tsTo.Subtract(tsFrom).Duration();
            //int daysDiff = ts.Days;

            ////int SunSatDay = this.GetAllGeneralHolidaysByDateTime(dtOutDate, dtOperDate);//����������

            ////daysDiff = daysDiff - SunSatDay;

            //if (cb.Patient.PVisit.ZG.ID == "4")//�������
            //{
            //    if (daysDiff > 7)
            //    {
            //        sevenTimeout = 1;
            //        tenTimeout = 0;
            //    }
            //    else
            //    {
            //        sevenTimeout = 0;
            //        tenTimeout = 0;
            //    }
            //}
            //else
            //{
            //    if (daysDiff > firstTimeOut)
            //    {
            //        sevenTimeout = 1;
            //        tenTimeout = 0;
            //    }
            //    else
            //    {
            //        sevenTimeout = 0;
            //        tenTimeout = 0;
            //    }
            //}
            try
            {
                if (this.Sql.GetSql("Case.Callback.InsertCallbackInfo", ref sql) < 0)
                {
                    return -1;
                }

                sql = string.Format(sql, cb.Patient.ID,cb.Patient.PVisit.PatientLocation.Dept.ID,cb.Patient.PVisit.AdmittingDoctor.ID,
                                         cb.Patient.PVisit.OutTime,cb.IsCallback, cb.CallbackOper.ID, cb.CallbackOper.OperTime, 
                                         cb.IsDocument,cb.DocumentOper.ID,cb.DocumentOper.OperTime,cb.Patient.PID.PatientNO,sevenTimeout,
                                         tenTimeout);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message.ToString();
                return -1;
            }

            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// ���㳬ʱ
        /// </summary>
        /// <param name="ZgType">ת������</param>
        /// <param name="OutDate">��Ժ����</param>
        /// <param name="CallBackDate">��������</param>
        /// <param name="deptCode">��������--�����ж��������</param>
        /// <param name="sevenTimeout">��ʱ 7��</param>
        /// <param name="tenTimeout">��ʱ 10�� �ݲ���</param>
        private void TimeOutOfCallBack(string ZgType, DateTime OutDate, DateTime CallBackDate, string deptCode, ref int sevenTimeout, ref int tenTimeout)
        {
            //1�� סԺ�����Բ��˳�Ժ��5���ڻ��գ���������7���ڻ��գ�
            string sql = string.Empty;

            string[] dept = arraySpecifyDept.Split(',');

            DateTime dtOutDate = OutDate;
            DateTime dtOperDate = CallBackDate;

            TimeSpan tsFrom = new TimeSpan(dtOutDate.Ticks);
            TimeSpan tsTo = new TimeSpan(dtOperDate.Ticks);
            TimeSpan ts = tsTo.Subtract(tsFrom).Duration();
            int daysDiff = ts.Days;

            //int SunSatDay = this.GetAllGeneralHolidaysByDateTime(dtOutDate, dtOperDate);//����������

            //daysDiff = daysDiff - SunSatDay;

            if (dept != null && dept.Length > 0 && dept[0]!="") //������� 
            {
                foreach (string str in dept)
                {
                    if (deptCode == str)
                    {
                        if (daysDiff > 14)
                        {
                            sevenTimeout = 0;
                            tenTimeout = 1;
                        }
                        else
                        {
                            sevenTimeout = 0;
                            tenTimeout = 0;
                        }
                    }
                }
            }
            else//���������
            {
                if (ZgType == "4")//�������
                {
                    if (daysDiff > deathTimeOut && daysDiff<=secondTimeOut)
                    {
                        sevenTimeout = 1;
                        tenTimeout = 0;
                    }
                    else if (daysDiff > secondTimeOut)
                    {
                        sevenTimeout = 0;
                        tenTimeout = 1;
                    }
                    else
                    {
                        sevenTimeout = 0;
                        tenTimeout = 0;
                    }
                }
                else
                {
                    if (daysDiff > firstTimeOut && daysDiff <= secondTimeOut)
                    {
                        sevenTimeout = 1;
                        tenTimeout = 0;
                    }
                    else if (daysDiff > secondTimeOut)
                    {
                        sevenTimeout = 0;
                        tenTimeout = 1;
                    }
                    else
                    {
                        sevenTimeout = 0;
                        tenTimeout = 0;
                    }
                }
            }
        }
    }
}
