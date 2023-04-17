using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// ���Ĺ黹
    /// </summary>
    public class CaseCard : FS.FrameWork.Management.Database
    {
        #region ���Ŀ� ��������ά��
        /// <summary>
        /// ��ȡ���еĽ��Ŀ�����Ϣ 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetCardInfo(ref System.Data.DataSet ds)
        {
            try
            {
                string strSql = GeCardSql();
                //��ѯ
                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        private string GeCardSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.GetCardInfo", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ���ݿ��Ż�ȡ��Ϣ 
        /// </summary>
        /// <param name="CardID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.ReadCard GetCardInfo(string CardID)
        {
            FS.HISFC.Models.HealthRecord.ReadCard info = new FS.HISFC.Models.HealthRecord.ReadCard();
            try
            {
                string strSql = "";
                string strSql1 = GeCardSql();
                if (strSql1 == null)
                {
                    return null;
                }
                if (this.Sql.GetSql("Case.CaseCard.GetCardInfo.1", ref strSql) == -1) return null;
                strSql1 += strSql;
                strSql1 = string.Format(strSql1, CardID);
                //��ѯ
                this.ExecQuery(strSql1);
                while (this.Reader.Read())
                {
                    info.CardID = this.Reader[0].ToString(); //����
                    info.EmployeeInfo.ID = this.Reader[1].ToString(); //Ա����
                    info.EmployeeInfo.Name = this.Reader[2].ToString();//Ա������
                    info.DeptInfo.ID = this.Reader[3].ToString();//���Ҵ���
                    info.DeptInfo.Name = this.Reader[4].ToString();//��������
                    info.User01 = this.Reader[5].ToString();//����Ա
                    info.EmployeeInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());//����ʱ��
                    info.ValidFlag = this.Reader[7].ToString();//��Ч
                    info.CancelOperInfo.Name = this.Reader[8].ToString();//������
                    info.CancelDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());//����ʱ��
                }
                this.Reader.Close();
                return info;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ���Ŀ����Ƿ��Ѿ����� 
        /// </summary>
        /// <param name="CardID"></param>
        /// <returns> -1 ���� ��1 ���� ��2 ������ </returns>
        public int IsExist(string CardID)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.GetCardInfo.1", ref strSql) == -1) return -1;
                strSql = string.Format(strSql, CardID);
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    return 1;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 2;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.HealthRecord.ReadCard info)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.Insert", ref strSql) == -1) return -1;
                string[] Str = GetInfo(info);
                strSql = string.Format(strSql, Str);
                //��ѯ
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.HealthRecord.ReadCard info)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.Update", ref strSql) == -1) return -1;
                string[] Str = GetInfo(info);
                strSql = string.Format(strSql, Str);
                //��ѯ
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        private string[] GetInfo(FS.HISFC.Models.HealthRecord.ReadCard obj)
        {
            string[] str = new string[10];
            try
            {
                str[0] = obj.CardID; //����
                str[1] = obj.EmployeeInfo.ID; //Ա����
                str[2] = obj.EmployeeInfo.Name;//Ա������
                str[3] = obj.DeptInfo.ID;//���Ҵ���
                str[4] = obj.DeptInfo.Name;//��������
                str[5] = obj.User01;//����Ա
                str[6] = obj.EmployeeInfo.OperTime.ToString();//����ʱ��
                str[7] = obj.ValidFlag;//��Ч
                str[8] = obj.CancelOperInfo.Name;//������
                str[9] = obj.CancelDate.ToString();//����ʱ��
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return str;
        }
        #endregion

        #region  ��������
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int LendCase(FS.HISFC.Models.HealthRecord.Lend info)
        {
            string[] arrStr = getInfo(info);
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.LendCase", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, arrStr);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���²����ı�־ 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="PaptientNO"></param>
        /// <returns></returns>
        public int UpdateBase(FS.HISFC.Models.HealthRecord.EnumServer.LendType type, string CaseNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.UpdateBase", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, type.ToString(), CaseNO);
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// �黹 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int ReturnCase(FS.HISFC.Models.HealthRecord.Lend info)
        {
            string[] arrStr = getInfo(info);
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.ReturnCase", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, arrStr);
            return this.ExecNoQuery(strSql);
        }
        private string[] getInfo(FS.HISFC.Models.HealthRecord.Lend info)
        {
            string[] str = new string[28];
            str[0] = info.CaseBase.PatientInfo.ID;//סԺ��ˮ��
            str[1] = info.CaseBase.CaseNO;//����סԺ��
            str[2] = info.CaseBase.PatientInfo.Name; //��������
            str[3] = info.CaseBase.PatientInfo.Sex.ID.ToString();//�Ա�
            str[4] = info.CaseBase.PatientInfo.Birthday.ToString();//��������
            str[5] = info.CaseBase.PatientInfo.PVisit.InTime.ToString();//��Ժ����
            str[6] = info.CaseBase.PatientInfo.PVisit.OutTime.ToString();//��Ժ����
            str[7] = info.CaseBase.InDept.ID; //��Ժ���Ҵ���
            str[8] = info.CaseBase.InDept.Name; //��Ժ��������
            str[9] = info.CaseBase.OutDept.ID;  //��Ժ���Ҵ���
            str[10] = info.CaseBase.OutDept.Name; //��Ժ��������
            str[11] = info.EmployeeInfo.ID;//�����˴���
            str[12] = info.EmployeeInfo.Name;//����������
            str[13] = info.EmployeeDept.ID; //���������ڿ��Ҵ���
            str[14] = info.EmployeeDept.Name; //���������ڿ�������
            str[15] = info.LendDate.ToString(); //��������
            str[16] = info.PrerDate.ToString(); //Ԥ������
            str[17] = info.LendKind; //��������
            str[18] = info.LendStus;//����״̬ 1���/2����
            str[19] = info.ID; //����Ա����
            str[20] = info.OperInfo.OperTime.ToString(); //����ʱ��
            str[21] = info.ReturnOperInfo.ID;   //�黹����Ա����
            str[22] = info.ReturnDate.ToString();   //ʵ�ʹ黹����
            str[23] = info.CardNO;//����
            str[24] = info.Memo; //�������
            str[25] = info.LendNum;// ����
            str[26] = info.SeqNO; //����
            //str[27] = info.PatientInfo.PatientInfo.InTimes.ToString();//סԺ����
            return str;
        }
        /// <summary>
        /// ���ݿ��Ų�ѯ��Ҫ�黹����Ϣ
        /// </summary>
        /// <param name="LendCardNo"></param>
        /// <returns></returns>
        public ArrayList QueryLendInfo(string LendCardNo)
        {
            string StrSql = GetLendSql();
            if (StrSql == null)
            {
                return null;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.GetLendSql.where", ref strSql) == -1) return null;
            StrSql += strSql;
            StrSql = string.Format(StrSql, LendCardNo);
            this.ExecQuery(StrSql);
            return QueryLendInfoBase();
        }
        /// <summary>
        /// ���ݲ����Ų���������Ϣ
        /// </summary>
        /// <param name="CaseNO"></param>
        /// <returns></returns>
        public ArrayList QueryLendInfoByCaseNO(string CaseNO) 
        {
            string StrSql = GetLendSql();
            if (StrSql == null)
            {
                return null;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.QueryLendInfo.where", ref strSql) == -1) return null;
            StrSql += strSql;
            StrSql = string.Format(StrSql, CaseNO);
            this.ExecQuery(StrSql);
            return QueryLendInfoBase();
        }
        /// <summary>
        /// ˽�к���
        /// </summary>
        /// <returns></returns>
        private ArrayList QueryLendInfoBase()
        {
            try
            {
                ArrayList list = new ArrayList();
                FS.HISFC.Models.HealthRecord.Lend info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Lend();
                    info.CaseBase.PatientInfo.ID = this.Reader[0].ToString();//סԺ��ˮ��
                    info.CaseBase.CaseNO = this.Reader[1].ToString();//����סԺ��
                    info.CaseBase.PatientInfo.Name = this.Reader[2].ToString(); //��������
                    info.CaseBase.PatientInfo.Sex.ID = this.Reader[3].ToString();//�Ա�
                    info.CaseBase.PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());//��������
                    info.CaseBase.PatientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());//��Ժ����
                    info.CaseBase.PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());//��Ժ����
                    info.CaseBase.InDept.ID = this.Reader[7].ToString(); //��Ժ���Ҵ���
                    info.CaseBase.InDept.Name = this.Reader[8].ToString(); //��Ժ��������
                    info.CaseBase.OutDept.ID = this.Reader[9].ToString();  //��Ժ���Ҵ���
                    info.CaseBase.OutDept.Name = this.Reader[10].ToString(); //��Ժ��������
                    info.EmployeeInfo.ID = this.Reader[11].ToString();//�����˴���
                    info.EmployeeInfo.Name = this.Reader[12].ToString();//����������
                    info.EmployeeDept.ID = this.Reader[13].ToString(); //���������ڿ��Ҵ���
                    info.EmployeeDept.Name = this.Reader[14].ToString(); //���������ڿ�������
                    info.LendDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString()); //��������
                    info.PrerDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString()); //Ԥ������
                    info.LendKind = this.Reader[17].ToString(); //��������
                    info.LendStus = this.Reader[18].ToString();//����״̬ 1���/2����
                    info.ID = this.Reader[19].ToString(); //����Ա����
                    info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20].ToString()); //����ʱ��
                    info.ReturnOperInfo.ID = this.Reader[21].ToString();   //�黹����Ա����
                    info.ReturnDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString());   //ʵ�ʹ黹����
                    info.CardNO = this.Reader[23].ToString();//����
                    info.Memo = this.Reader[24].ToString(); //�������
                    info.LendNum = this.Reader[25].ToString();//����
                    info.SeqNO = this.Reader[26].ToString(); //�������
                    //info.CaseBase.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[27].ToString());//סԺ����
                    list.Add(info);
                }
                this.Reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        private string GetLendSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.GetLendSql", ref strSql) == -1) return null;
            return strSql;
        }

        /// <summary>
        /// ���趨where��������������Ϣ
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ArrayList QueryLendInfoSetWhere(string strWhere)
        {
            string StrSql = GetLendSql();
            if (StrSql == null)
            {
                return null;
            }

            StrSql += strWhere;
            StrSql = string.Format(StrSql);

            if (this.ExecQuery(StrSql) == -1)
            {
                return null;
            }

            return QueryLendInfoBase();

        }

        /// <summary>
        /// ���½�����Ϣ��״̬ add by lk
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateLendInfo(FS.HISFC.Models.HealthRecord.Lend info)
        {
            string strSql = "";

            //if (this.Sql.GetSql("Case.CaseCard.UpdateLendCase.stus", ref strSql) == -1)
            //{
            //    return -1;

            //}
            if (this.Sql.GetSql("Case.CaseCard.ReturnCase", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, 
                    //info.SeqNO,
                                               info.CaseBase.PatientInfo.ID,
                                               info.CaseBase.CaseNO,//����סԺ��
                    info.CaseBase.PatientInfo.Name, //��������
                    info.CaseBase.PatientInfo.Sex.ID,//�Ա�
                    info.CaseBase.PatientInfo.Birthday,//��������
                    info.CaseBase.PatientInfo.PVisit.InTime,//��Ժ����
                    info.CaseBase.PatientInfo.PVisit.OutTime,//��Ժ����
                    info.CaseBase.InDept.ID,//��Ժ���Ҵ���
                    info.CaseBase.InDept.Name,//��Ժ��������
                    info.CaseBase.OutDept.ID,  //��Ժ���Ҵ���
                    info.CaseBase.OutDept.Name,//��Ժ��������
                    info.EmployeeInfo.ID,//�����˴���
                    info.EmployeeInfo.Name,//����������
                    info.EmployeeDept.ID, //���������ڿ��Ҵ���
                    info.EmployeeDept.Name, //���������ڿ�������
                    info.LendDate, //��������
                    info.PrerDate, //Ԥ������
                    info.LendKind, //��������
                    info.LendStus,//����״̬ 1���/2����
                    info.ID, //����Ա����
                    info.OperInfo.OperTime, //����ʱ��
                    info.ReturnOperInfo.ID,//�黹����Ա����
                    info.ReturnDate,  //ʵ�ʹ黹����
                    info.CardNO,//����
                    info.Memo ,//�������
                    info.LendNum.ToString(),//����
                    info.SeqNO,//��ˮ��
                    info.CaseBase.PatientInfo.InTimes.ToString()//סԺ����
                    
                     );

                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return 1;
        }

       /// <summary>
        /// ��ѯ����������Ϣ����ҽ�����Ӳ�������������Ϣ
       /// </summary>
        /// <param name="Type">���� PaperCase ֽ�ʲ������� ElectronCase ���Ӳ���ҽ����������</param>
       /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.Lend> QueryNeedBack(FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType Type )
        {
            string StrSql = string.Empty;
            //if (Type == FS.HISFC.Models.HealthRecord.EnumServer.LendCaseType.Return)
            //{
            //    if (this.Sql.GetSql("Case.CaseCard.QueryNeedBack", ref StrSql) == -1) return null;
            //}
            //else 
            //{
                if (this.Sql.GetSql("Case.CaseCard.ElectronCase.LendPetition", ref StrSql) == -1) return null;
                StrSql = string.Format(StrSql, (int)Type);
            //}
            this.ExecQuery(StrSql);
            try
            {
                List<FS.HISFC.Models.HealthRecord.Lend> list = new List<FS.HISFC.Models.HealthRecord.Lend>();
                FS.HISFC.Models.HealthRecord.Lend info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Lend();

                    info.CardNO = this.Reader[11].ToString();//����
                    info.EmployeeInfo.Name = this.Reader[12].ToString();//����������
                    info.EmployeeDept.Name = this.Reader[14].ToString(); //���������ڿ�������
                    info.LendDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString()); //��������
                    list.Add(info);
                }
                this.Reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���������ѯ������Ϣ
        /// </summary>
        /// <param name="personCode">�����˱��</param>
        /// <param name="deptCode">���ұ��</param>
        /// <param name="LendState">����״̬</param>
        /// <param name="dtBegin">���Ŀ�ʼ����</param>
        /// <param name="dtEnd">���Ľ�������</param>
        /// <param name="ds">���� ���ݼ�</param>
        /// <returns></returns>
        public int QueryLendCaseByMoreCondition(string personCode, string deptCode, string LendState, DateTime dtBegin, DateTime dtEnd, ref DataSet ds)
        {
            try
            {
                //if (this.Sql.GetSql("HealthReacord.Case.CaseStroe.Select1", ref strSql) == -1) return -1;
                string strSql = "";
                strSql = @"
SELECT 
l.PATIENT_NO as סԺ��,
l.NAME as ����,
l.OUT_DATE as ��Ժ����,
l.EMPL_NAME as ������ ,
l.DEPT_NAME as ���Ŀ���,
l.LEND_DATE as ��������,
(SELECT NAME  FROM  COM_DICTIONARY WHERE TYPE='CASE_LEND_TYPE' AND CODE= l.LEND_KIND) as ��������,
--days (CURRENT TIMESTAMP) - days(l.LEND_DATE) as ��������,
trunc(sysdate) - trunc(l.LEND_DATE) as ��������,
CASE WHEN l.len_stus='1' THEN 'δ��' WHEN l.len_stus='2' THEN '�ѻ�' WHEN l.len_stus='3' THEN '����ҽ���δ���' WHEN l.len_stus='4' THEN '���벡����δͨ��' END AS �黹���
FROM MET_CAS_LEND  l
WHERE (l.EMPL_CODE='{0}' OR 'ALL'='{0}')
AND (l.DEPT_CODE='{1}' OR 'ALL'='{1}')
AND (l.LEN_STUS='{2}' OR 'ALL'='{2}')
--AND l.LEND_DATE BETWEEN '{3}' AND '{4}'
AND l.LEND_DATE BETWEEN to_date('{3}','yyyy-mm-dd HH24:mi:ss') AND to_date('{4}','yyyy-mm-dd HH24:mi:ss')
ORDER BY  l.LEND_DATE
";
                try
                {
                    //��ѯ
                    strSql = string.Format(strSql, personCode, deptCode, LendState, dtBegin, dtEnd);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }



                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }


        }
        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="DtBegin">���Ŀ�ʼ����</param>
        /// <param name="dtEnd">���Ľ�������</param>
        /// <param name="lendState">����״̬</param>
        /// <param name="ds">�������ݼ�</param>
        /// <returns></returns>
        public int QueryLendCaseByLendDate(DateTime DtBegin, DateTime dtEnd, string lendState, ref DataSet ds)
        {
            try
            {
                //if (this.Sql.GetSql("HealthReacord.Case.CaseStroe.Select1", ref strSql) == -1) return -1;
                DtBegin = DtBegin.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                dtEnd = dtEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                string strSql = "";
                strSql = @"
SELECT 
l.PATIENT_NO as סԺ��,
l.NAME as ����,
l.OUT_DATE as ��Ժ����,
l.EMPL_NAME as ������ ,
l.DEPT_NAME as ���Ŀ���,
l.LEND_DATE as ��������,
(SELECT NAME  FROM  COM_DICTIONARY WHERE TYPE='CASE_LEND_TYPE' AND CODE= l.LEND_KIND) as ��������,
--days (CURRENT TIMESTAMP) - days(l.LEND_DATE) as ��������
trunc(sysdate) - trunc(l.LEND_DATE) as ��������
FROM MET_CAS_LEND  l
WHERE  l.LEND_DATE BETWEEN to_date('{3}','yyyy-mm-dd HH24:mi:ss') AND to_date('{4}','yyyy-mm-dd HH24:mi:ss')
--l.LEND_DATE BETWEEN '{0}' AND '{1}'
AND (l.LEN_STUS='{2}' OR 'ALL'='{2}')
";
                try
                {
                    //��ѯ
                    strSql = string.Format(strSql, DtBegin, dtEnd, lendState);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }



                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }


        }
        /// <summary>
        /// ���ݲ����Ų���������Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryUNCallBack(DateTime dtBegin, DateTime dtEnd)
        {
            string StrSql = @"SELECT patient_no,out_date  FROM DONGGUAN_CASEHISTORY_CALLBACK  WHERE IS_CALLBACK='0' and out_date BETWEEN '{0}' and '{1}'";
            StrSql = string.Format(StrSql, dtBegin, dtEnd);
            this.ExecQuery(StrSql);
            try
            {
                ArrayList list = new ArrayList();
                FS.HISFC.Models.HealthRecord.Lend info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.Lend();

                    info.CardNO = this.Reader[0].ToString();//����
                    info.LendDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString()); //��������
                    list.Add(info);
                }
                this.Reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// ������������������Ϣ
        /// </summary>
        /// <param name="Name">����</param>
        /// <returns></returns>
        public ArrayList QueryLendInfoByName(string Name)
        {
            string StrSql = GetLendSql();
            if (StrSql == null)
            {
                return null;
            }
            string strSql = "WHERE NAME LIKE '{0}'";
            //if (this.Sql.GetSql("Case.CaseCard.QueryLendInfo.where", ref strSql) == -1) return null;
            StrSql += strSql;
            StrSql = string.Format(StrSql, Name);
            this.ExecQuery(StrSql);
            return QueryLendInfoBase();
        }
        /// <summary>
        /// ����Ƿ������ѯ���Ӳ���
        /// �����н����δ���գ��鵵��
        /// ����������δ���
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="OperCode">����Ա��</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject IsCheckAllowQueryEmr(string inpatientNo, string OperCode)
        {
            FS.FrameWork.Models.NeuObject neuobj = new FS.FrameWork.Models.NeuObject();
            int callBackCount = this.CheckCallBack(inpatientNo);
            int lendCaseCount = 0;
            if (callBackCount == 0)
            {
                neuobj = new FS.FrameWork.Models.NeuObject();
                neuobj.ID = "1";
                neuobj.Memo = "δ���գ��鵵�������Բ�ѯ�û��߲�����";
            }
            else if (callBackCount > 0)
            {
                lendCaseCount = this.CheckLendCase(inpatientNo, OperCode);
                if (lendCaseCount == 0)
                {
                    neuobj = new FS.FrameWork.Models.NeuObject();
                    neuobj.ID = "0";
                    neuobj.Memo = "�ѻ��գ��鵵�����޽�������ܲ�ѯ�û��߲�����";
                }
                else if (lendCaseCount > 0)
                {
                    neuobj = new FS.FrameWork.Models.NeuObject();
                    neuobj.ID = "1";
                    neuobj.Memo = "�ѻ��գ��鵵�����н�������Բ�ѯ�û��߲�����";
                }
                else
                {
                    neuobj = new FS.FrameWork.Models.NeuObject();
                    neuobj.ID = "-1";
                    neuobj.Memo = "δ�Ҽ�������䣺Case.LendCase��";
                }
            }
            else
            {
                neuobj = new FS.FrameWork.Models.NeuObject();
                neuobj.ID = "-1";
                neuobj.Memo = "δ�Ҽ�������䣺Case.CheckCallBack��";
            }
            return neuobj;
        }
        /// <summary>
        /// ����Ƿ����
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns></returns>
        private int CheckCallBack(string inpatientNo)
        {
            string StrSql = string.Empty;
            if (this.Sql.GetSql("Case.CheckCallBack", ref StrSql) == -1) return -1;

            StrSql = string.Format(StrSql, inpatientNo);
            this.ExecQuery(StrSql);
            try
            {
                int count = 0;
                while (this.Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32( this.Reader[0].ToString());//��������
                }
                this.Reader.Close();
                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return 0;
            }
        }
        /// <summary>
        /// ����Ƿ���
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="OperCode">����Ա��</param>
        /// <returns></returns>
        public int CheckLendCase(string inpatientNo, string OperCode)
        {
            string StrSql = string.Empty;
            if (this.Sql.GetSql("Case.LendCase", ref StrSql) == -1) return -1;

            StrSql = string.Format(StrSql, inpatientNo,OperCode);
            this.ExecQuery(StrSql);
            try
            {
                int count = 0;
                while (this.Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());//��������
                }
                this.Reader.Close();
                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return 0;
            }
        }


        /// <summary>
        /// ��ѯ�����ύ��Ϣ
        /// </summary>
        /// <param name="DtBegin">�ύ��ʼ����</param>
        /// <param name="dtEnd">�ύ��������</param>
        /// <param name="Status">�ύ״̬</param>
        /// <param name="ds">�������ݼ�</param>
        /// <returns></returns>
        public int QueryCaseCommitByCommitDateAndStatus(DateTime DtBegin, DateTime dtEnd, string Status, ref DataSet ds)
        {
            try
            {
                DtBegin = DtBegin.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                dtEnd = dtEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.ElectronCase.EmrQcCommit", ref strSql) == -1) return -1;
                try
                {
                    //��ѯ
                    strSql = string.Format(strSql, DtBegin, dtEnd, Status);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }



                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }


        }

        /// <summary>
        /// ���µ��Ӳ����ύ״̬
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateEmrQcCommit(FS.HISFC.Models.HealthRecord.Lend info)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.ElectronCase.EmrQcCommit.Update", ref strSql) == -1) return -1;

                strSql = string.Format(strSql, info.ID,info.LendStus,info.LendDate,info.PrerDate,info.OperInfo.ID);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ѯ�����ύ��Ϣ
        /// </summary>
        /// <param name="inpatientNo">�ύ��ʼ����</param>
        /// <param name="Status">�ύ״̬</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.Lend> QueryCaseCommitByInpatienNoAndStatus(string inpatientNo, string Status)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.CaseCard.ElectronCase.EmrQcCommit.ByinpatientNo", ref strSql) == -1) return null;
                try
                {
                    //��ѯ
                    strSql = string.Format(strSql, inpatientNo, Status);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return null;
                }

                this.ExecQuery(strSql);
                try
                {
                    List<FS.HISFC.Models.HealthRecord.Lend> list = new List<FS.HISFC.Models.HealthRecord.Lend>();
                    FS.HISFC.Models.HealthRecord.Lend info = null;
                    while (this.Reader.Read())
                    {
                        info = new FS.HISFC.Models.HealthRecord.Lend();

                        info.ID = this.Reader[0].ToString();//����
                        info.Name = this.Reader[1].ToString();//����������
                        info.EmployeeDept.Name = this.Reader[2].ToString(); //���������ڿ�������
                        info.LendDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3].ToString()); //��������
                        list.Add(info);
                    }
                    this.Reader.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }


        }
        #endregion

        #region ����
        [Obsolete("����,�� QueryLendInfo ����")]
        public ArrayList GetLendInfo(string LendCardNo)
        {
            string StrSql = GetLendSql();
            if (StrSql == null)
            {
                return null;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.CaseCard.GetLendSql.where", ref strSql) == -1) return null;
            StrSql += strSql;
            StrSql = string.Format(StrSql, LendCardNo);
            this.ExecQuery(StrSql);
            return QueryLendInfoBase();
        }
        #endregion
    }
}
