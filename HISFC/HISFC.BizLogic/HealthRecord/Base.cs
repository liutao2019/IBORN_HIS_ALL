using System;
using System.Collections;
using System.Data;


namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// BaseDML ��ժҪ˵����
    /// </summary>
    public class Base : FS.FrameWork.Management.Database
    {
        public Base()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ������ҳ ���߻�����Ϣ��������

        #region ����
        /// <summary>
        /// ���»�����סԺ����ĵǼǲ������
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="caseState">����״̬: 0 ���財�� 1 ��Ҫ���� 2 ҽ��վ�γɲ��� 3 �������γɲ��� 4������� </param>
        /// <returns> �ɹ�����</returns>
        public int UpdateMainInfoCaseFlag(string inpatientNO, string caseState)
        {
            string strSQL = "";

            if (Sql.GetSql("CASE.BaseDML.UpdateMainInfoCaseFlag.Update", ref strSQL) == 0)
            {
                try
                {
                    strSQL = string.Format(strSQL, inpatientNO, caseState);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }

            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ���»�����סԺ����ĵǼǲ������
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ�� </param>
        /// <param name="caseSendFlag">�������벡���ҷ�0δ1��  </param>
        /// <returns> �ɹ�����</returns>
        public int UpdateMainInfoCaseSendFlag(string inpatientNO, string caseSendFlag)
        {
            string strSQL = "";

            if (Sql.GetSql("CASE.BaseDML.UpdateMainInfoCaseFlag.Update", ref strSQL) == 0)
            {
                try
                {
                    strSQL = string.Format(strSQL, inpatientNO, caseSendFlag);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
            }

            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ���²�������
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public int UpdateBaseInfo(FS.HISFC.Models.HealthRecord.Base b)
        {
            string strSql = "";
            //if (this.Sql.GetSql("CASE.BaseDML.UpdateBaseInfo.Update", ref strSql) == -1) return -1;
            if (this.Sql.GetSql("CASE.BaseDML.UpdateBaseInfo.Update.HIS50", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, GetBaseInfo(b));
        }
        #endregion

        /// <summary>
        /// ��ѯδ�Ǽǲ�����Ϣ�Ļ��ߵ������Ϣ,��met_com_diagnose����ȡ
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="diagType">������,Ҫ��ȡ�����������%</param>
        /// <returns>�����Ϣ����</returns>
        public ArrayList QueryInhosDiagnoseInfo(string inpatientNO, string diagType)
        {
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetInhosDiagInfo.Select", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, inpatientNO, diagType);

            return this.myGetDiagInfo(strSql);
        }

        /// <summary>
        /// �Ӳ����������л�ȡ��Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base GetCaseBaseInfo(string inpatientNO)
        {
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            if (strSQL == null)
            {
                return null;
            }
            string str = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfo.Select.where", ref str) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, inpatientNO);
            ArrayList arrList = this.myGetCaseBaseInfo(strSQL);
            if (arrList == null)
            {
                return null;
            }
            if (arrList.Count > 0)
            {
                info = (FS.HISFC.Models.HealthRecord.Base)arrList[0];
            }
            return info;
        }

        /// <summary>
        /// ���ݲ����Ż�ȡ��Ϣ
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <returns></returns>
        public ArrayList QueryCaseBaseInfoByCaseNO(string CaseNo)
        {
            ArrayList list = new ArrayList();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            string str = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfoByCaseNum.Select.where", ref str) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, CaseNo);
            return this.myGetCaseBaseInfo(strSQL);
        }

        /// <summary>
        /// �����Զ���������ѯ������Ϣ
        /// </summary>
        /// <param name="where">where ������Ϣ</param>
        /// <returns></returns>
        public ArrayList QueryCaseBaseInfoByOwnConditions(string where)
        {
            ArrayList list = new ArrayList();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            strSQL += where;
            return this.myGetCaseBaseInfo(strSQL);
        }

        /// <summary>
        /// �򲡰������в���һ����¼
        /// </summary>
        /// <param name="b"></param>
        /// <returns> �ɹ����� 1 ʧ�ܷ��أ�1 ��0  </returns>
        public int InsertBaseInfo(FS.HISFC.Models.HealthRecord.Base b)
        {
            string strSql = "";
            //if (this.Sql.GetSql("CASE.BaseDML.InsertBaseInfo.Insert", ref strSql) == -1) return -1;
            if (this.Sql.GetSql("CASE.BaseDML.InsertBaseInfo.Insert.HIS50", ref strSql) == -1) return -1;

            return this.ExecNoQuery(strSql, GetBaseInfo(b));
        }

        /// <summary>
        /// ����סԺ�ź�סԺ������ѯסԺ��ˮ�� 
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="InNum"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByInpatientAndInNum(string inpatientNO, string InNum)
        {
            //�ȴӲ��������в�ѯ ���û�в鵽 ����סԺ�����в�ѯ 
            ArrayList list = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetPatientInfo.GetPatientInfo", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, inpatientNO, InNum);
            this.ExecQuery(strSql);
            FS.HISFC.Models.RADT.PatientInfo info = null;
            while (this.Reader.Read())
            {
                info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = this.Reader[0].ToString();
                list.Add(info);
                info = null;
            }
            if (list == null)
            {
                return list;
            }
            if (list.Count == 0)
            {
                //��ѯסԺ���� ��ȡ������Ϣ
                if (this.Sql.GetSql("RADT.Inpatient.PatientInfoGetByTime", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return null;
                }
                strSql = string.Format(strSql, inpatientNO, InNum);
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.PatientInfo();
                    info.ID = this.Reader[0].ToString();
                    list.Add(info);
                    info = null;
                }
            }
            return list;
        }
        /// <summary>
        /// ����סԺ�Ų�ѯ ������Ϣ��סԺ��Ϣ
        /// </summary>
        /// <param name="PatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfo(string PatientNO)
        {
            //�ȴӲ��������в�ѯ ���û�в鵽 ����סԺ�����в�ѯ 
            ArrayList list = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetPatientInfo.GetPatientInfo", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, PatientNO);
            this.ExecQuery(strSql);
            FS.HISFC.Models.HealthRecord.Base info = null;
            while (this.Reader.Read())
            {
                info = new FS.HISFC.Models.HealthRecord.Base();
                info.OutDept.Name = this.Reader[0].ToString(); //��Ժ����
                info.PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString()); //��Ժ����
                info.PatientInfo.Name = this.Reader[2].ToString(); //����
                info.PatientInfo.Sex.ID = this.Reader[3].ToString(); //�Ա�
                info.CaseNO = this.Reader[4].ToString(); //������
                info.PatientInfo.PID.PatientNO = this.Reader[5].ToString(); //סԺ��
                info.PatientInfo.ID = this.Reader[6].ToString(); //סԺ��ˮ��
                info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[7]); //סԺ����
                info.PatientInfo.User01 = this.Reader[8].ToString();
                list.Add(info);
                info = null;
            }

            return list;
        }

        /// <summary>
        /// ����������ѯ ������Ϣ��סԺ��Ϣ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByName(string name)
        {
            //�ȴӲ��������в�ѯ ���û�в鵽 ����סԺ�����в�ѯ 
            ArrayList list = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetPatientInfo.GetPatientInfo.ByName", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, name);
            this.ExecQuery(strSql);
            FS.HISFC.Models.HealthRecord.Base info = null;
            while (this.Reader.Read())
            {
                info = new FS.HISFC.Models.HealthRecord.Base();
                info.OutDept.Name = this.Reader[0].ToString(); //��Ժ����
                info.PatientInfo.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString()); //��Ժ����
                info.PatientInfo.Name = this.Reader[2].ToString(); //����
                info.PatientInfo.Sex.ID = this.Reader[3].ToString(); //�Ա�
                info.CaseNO = this.Reader[4].ToString(); //������
                info.PatientInfo.PID.PatientNO = this.Reader[5].ToString(); //סԺ��
                info.PatientInfo.ID = this.Reader[6].ToString(); //סԺ��ˮ��
                info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[7]); //סԺ����
                info.PatientInfo.User01 = this.Reader[8].ToString();
                list.Add(info);
                info = null;
            }

            return list;
        }

        /// <summary>
        /// ����met_cas_base��������� ��Ժ��� ��Ժ��Ҫ��� ��һ����
        /// </summary>
        /// <param name="inpatienNO"></param>
        /// <param name="ClinicDiagName"></param>
        /// <param name="InHospDiagName"></param>
        /// <param name="frmType"></param>
        /// <returns></returns>
        public int UpdateBaseDiagAndOperationNew(string inpatienNO,string ClinicDiagCode, string ClinicDiagName, string InHospDiagName, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {

            FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.HealthRecord.Operation op = new Operation();
            if (this.Trans != null)
            {
                dia.SetTrans(Trans);
                op.SetTrans(Trans);
            }
            FS.HISFC.Models.HealthRecord.Diagnose ClinicDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.CLINIC, frmType);
            FS.HISFC.Models.HealthRecord.Diagnose InhosDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.IN, frmType);
            FS.HISFC.Models.HealthRecord.Diagnose OutDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OUT, frmType);
            FS.HISFC.Models.HealthRecord.OperationDetail ops = op.GetFirstOperation(inpatienNO, frmType);
            if (ClinicDiag == null || InhosDiag == null || OutDiag == null || ops == null)
            {
                return -1;
            }
            string[] str = new string[14];
            str[0] = inpatienNO;
            if (ClinicDiag != null && ClinicDiag.DiagInfo.ICD10.ID != "")
            {
                str[1] = ClinicDiag.DiagInfo.ICD10.ID;
                str[2] = ClinicDiag.DiagInfo.ICD10.Name;
            }
            else
            {
                str[1] = ClinicDiagCode;
                str[2] = ClinicDiagName;
            }
            if (InhosDiag != null && InhosDiag.DiagInfo.ICD10.ID != "")
            {
                str[3] = InhosDiag.DiagInfo.ICD10.ID;
                str[4] = InhosDiag.DiagInfo.ICD10.Name;
            }
            else
            {
                str[3] = "";
                str[4] = InHospDiagName;
            }
            str[5] = OutDiag.DiagInfo.ICD10.ID;
            str[6] = OutDiag.DiagInfo.ICD10.Name;
            str[7] = OutDiag.DiagOutState;
            str[8] = OutDiag.CLPA;
            str[9] = ops.OperationInfo.ID;
            str[10] = ops.OperationInfo.Name;
            str[11] = ops.FirDoctInfo.ID;
            str[12] = ops.FirDoctInfo.Name;
            str[13] = ops.OperationDate.ToString();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.UpdateBaseDiagAndOperation", ref strSql) == -1) return -1;

            strSql = string.Format(strSql, str);
            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// ������ϱ���������еĳ�Ժ���ںͳ�Ժ���� 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DiagnoseAndOperation(FS.FrameWork.Models.NeuObject obj, string InpatientNo)
        {
            //obj.User01 ��Ժ����
            //obj.User02 ��Ժ����
            string strSql1 = "";
            string strSql2 = "";
            //���
            if (this.Sql.GetSql("CASE.Diagnose.DiagnoseAndOperation.1", ref strSql1) == -1) return -1;
            //���� 
            if (this.Sql.GetSql("CASE.Diagnose.DiagnoseAndOperation.2", ref strSql2) == -1) return -1;
            strSql1 = string.Format(strSql1, InpatientNo, obj.User01);
            strSql2 = string.Format(strSql2, InpatientNo, obj.User01, obj.User02);
            if (this.ExecNoQuery(strSql1) != -1)
            {
                return this.ExecNoQuery(strSql2);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// �ж�ĳ��סԺ�ŵ�ĳ����Ժ�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <param name="PatientNo"></param>
        /// <param name="InNum"></param>
        /// <returns>û�м�¼ ���� 0 ,��ѯʧ�ܷ���-1 ,סԺ��,סԺ��ˮ��,סԺ����ȫ��ͬ ���� 1 סԺ��סԺ������ͬ ,סԺ��ˮ�Ų�ͬ ����2</returns>
        public int ExistCase(string InpatientNO, string PatientNo, string InNum)
        {
            string strSQL = GetCaseSql();
            string str = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfoByCaseNum.Select.ExistCase", ref str) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return -1;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, PatientNo, InNum);
            ArrayList List = this.myGetCaseBaseInfo(strSQL);
            if (List == null)
            {
                return -1; //��ѯ���� 
            }
            if (List.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Base obj in List)
                {
                    if (obj.PatientInfo.ID == InpatientNO) //סԺ��ˮ����ͬ סԺ����ͬ סԺ������ͬ 
                    {
                        return 1; //һ��ִ�и��²��� 
                    }
                }
                return 2; //סԺ����ͬ,סԺ������ͬ סԺ��ˮ�Ų�ͬ ,һ����סԺ������д���� 
            }
            return 0; //û�в鵽��صļ�¼ һ��ִ�в������
        }

        /// <summary>
        /// ��ȡһ��ʱ��Ļ���
        /// </summary>
        /// <param name="BeginTime">��ʼʱ��</param>
        /// <param name="EndTime">����ʱ��</param>
        /// <param name="DeptCode">���ұ���</param>
        /// <returns></returns>
        public ArrayList QueryPatientOutHospital(string BeginTime, string EndTime, string DeptCode)
        {
            ArrayList list = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.QueryPatientOutHospital", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, BeginTime, EndTime, DeptCode);
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo patientObj = new FS.HISFC.Models.RADT.PatientInfo();
                    patientObj.Name = this.Reader[0].ToString();
                    patientObj.PID.PatientNO = this.Reader[1].ToString();
                    patientObj.ID = this.Reader[2].ToString();
                    list.Add(patientObj);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û���סԺ�����Ϣ����!" + ex.Message;
                return null;
            }
            return list;
        }
        /// <summary>
        /// ��ȡ�ֹ�¼�벡��ʱ��סԺ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetCaseInpatientNO()
        {
            string str = this.GetSequence("CASE.BaseDML.GetCaseInpatientNO");
            if (str == null || str == "")
            {
                return str;
            }
            else
            {
                str = "BA" + str.PadLeft(12, '0');
            }
            return str;
        }
        #endregion

        //{7D094A18-0FC9-4e8b-A8E6-901E55D4C20C}

        #region  ˽�к���

        /// <summary>
        /// ��ʵ�� ת����ַ�������
        /// </summary>
        /// <param name="b"> ������ʵ����</param>
        /// <returns>ʧ�ܷ���null</returns>
        private string[] GetBaseInfo(FS.HISFC.Models.HealthRecord.Base b)
        {
            string[] s = new string[196];
            try
            {
                s[0] = b.PatientInfo.ID;//סԺ��ˮ��

                s[1] = b.PatientInfo.PID.PatientNO;//סԺ������

                s[2] = b.PatientInfo.PID.CardNO;//����

                s[3] = b.PatientInfo.Name;//����

                s[4] = b.Nomen;//������

                s[5] = b.PatientInfo.Sex.ID.ToString();//�Ա�

                s[6] = b.PatientInfo.Birthday.ToString();//��������

                s[7] = b.PatientInfo.Country.ID;//����

                s[8] = b.PatientInfo.Nationality.ID;//����

                s[9] = b.PatientInfo.Profession.ID;//ְҵ

                s[10] = b.PatientInfo.BloodType.ID.ToString();//Ѫ�ͱ���

                s[11] = b.PatientInfo.MaritalStatus.ID.ToString();//���

                s[12] = b.PatientInfo.Age.ToString();//����

                s[13] = b.AgeUnit;//���䵥λ

                s[14] = b.PatientInfo.IDCard;//���֤��

                s[15] = b.PatientInfo.PVisit.InSource.ID;//������Դ

                s[16] = b.PatientInfo.Pact.PayKind.ID;//��������

                s[17] = b.PatientInfo.Pact.ID;//��ͬ����

                s[18] = b.PatientInfo.SSN;//ҽ�����Ѻ�

                s[19] = b.PatientInfo.DIST;//����

                s[20] = b.PatientInfo.AreaCode;//������

                s[21] = b.PatientInfo.AddressHome;//��ͥסַ

                s[22] = b.PatientInfo.PhoneHome;//��ͥ�绰

                s[23] = b.PatientInfo.HomeZip;//סַ�ʱ�

                s[24] = b.PatientInfo.AddressBusiness;//��λ��ַ

                s[25] = b.PatientInfo.PhoneBusiness;//��λ�绰

                s[26] = b.PatientInfo.BusinessZip;//��λ�ʱ�

                s[27] = b.PatientInfo.Kin.Name;//��ϵ��

                s[28] = b.PatientInfo.Kin.RelationLink;//�뻼�߹�ϵ

                s[29] = b.PatientInfo.Kin.RelationPhone;//��ϵ�绰

                s[30] = b.PatientInfo.Kin.RelationAddress;//��ϵ��ַ

                s[31] = b.ClinicDoc.ID;//�������ҽ��

                s[32] = b.ClinicDoc.Name;//�������ҽ������

                s[33] = b.ComeFrom;//ת��ҽԺ

                s[34] = b.PatientInfo.PVisit.InTime.ToString();//��Ժ����

                s[35] = b.PatientInfo.InTimes.ToString();//סԺ����

                s[36] = b.InDept.ID;//��Ժ���Ҵ���

                s[37] = b.InDept.Name;//��Ժ��������

                s[38] = b.PatientInfo.PVisit.InSource.ID;//��Ժ��Դ

                s[39] = b.PatientInfo.PVisit.Circs.ID;//��Ժ״̬

                s[40] = b.DiagDate.ToString();//ȷ������

                s[41] = b.OperationDate.ToString();//��������

                s[42] = b.PatientInfo.PVisit.OutTime.ToString();//��Ժ����

                s[43] = b.OutDept.ID;//��Ժ���Ҵ���

                s[44] = b.OutDept.Name;//��Ժ��������

                s[45] = b.PatientInfo.PVisit.ZG.ID;//ת�����

                s[46] = b.DiagDays.ToString();//ȷ������

                s[47] = b.InHospitalDays.ToString();//סԺ����

                s[48] = b.DeadDate.ToString();//��������

                s[49] = b.DeadReason;//����ԭ��

                s[50] = b.CadaverCheck;//ʬ��

                s[51] = b.DeadKind;//��������

                s[52] = b.BodyAnotomize;//ʬ����ʺ�

                s[53] = b.Hbsag;//�Ҹα��濹ԭ

                s[54] = b.HcvAb;//���β�������

                s[55] = b.HivAb;//�������������ȱ�ݲ�������

                s[56] = b.CePi;//�ż�_��Ժ����

                s[57] = b.PiPo;//���_Ժ����

                s[58] = b.OpbOpa;//��ǰ_�����

                s[59] = b.ClX;//�ٴ�_X�����

                s[60] = b.ClCt;//�ٴ�_CT����

                s[61] = b.ClMri;//�ٴ�_MRI����

                s[62] = b.ClPa;//�ٴ�_�������

                s[63] = b.FsBl;//����_�������

                s[64] = b.SalvTimes.ToString();//���ȴ���

                s[65] = b.SuccTimes.ToString();//�ɹ�����

                s[66] = b.TechSerc;//ʾ�̿���

                s[67] = b.VisiStat;//�Ƿ�����

                s[68] = b.VisiPeriod.ToString();//�������

                s[69] = b.InconNum.ToString();//Ժ�ʻ������ 70 Զ�̻������

                s[70] = b.OutconNum.ToString();//Ժ�ʻ������ 70 Զ�̻������

                s[71] = b.AnaphyFlag;//ҩ�����

                s[72] = b.FirstAnaphyPharmacy.ID;//����ҩ������

                s[73] = b.SecondAnaphyPharmacy.ID;//����ҩ������

                s[74] = b.CoutDate.ToString();//���ĺ��Ժ����

                s[75] = b.PatientInfo.PVisit.AdmittingDoctor.ID;//סԺҽʦ����

                s[76] = b.PatientInfo.PVisit.AdmittingDoctor.Name;//סԺҽʦ����

                s[77] = b.PatientInfo.PVisit.AttendingDoctor.ID;//����ҽʦ����

                s[78] = b.PatientInfo.PVisit.AttendingDoctor.Name;//����ҽʦ����

                s[79] = b.PatientInfo.PVisit.ConsultingDoctor.ID;//����ҽʦ����

                s[80] = b.PatientInfo.PVisit.ConsultingDoctor.Name;//����ҽʦ����

                s[81] = b.PatientInfo.PVisit.ReferringDoctor.ID;//�����δ���

                s[82] = b.PatientInfo.PVisit.ReferringDoctor.Name;//����������

                s[83] = b.RefresherDoc.ID;//����ҽʦ����

                s[84] = b.RefresherDoc.Name;//����ҽ������

                s[85] = b.GraduateDoc.ID;//�о���ʵϰҽʦ����

                s[86] = b.GraduateDoc.Name;//�о���ʵϰҽʦ����

                s[87] = b.PatientInfo.PVisit.TempDoctor.ID;//ʵϰҽʦ����

                s[88] = b.PatientInfo.PVisit.TempDoctor.Name;//ʵϰҽʦ����

                s[89] = b.CodingOper.ID;//����Ա����

                s[90] = b.CodingOper.Name;//����Ա����

                s[91] = b.MrQuality;//��������

                s[92] = b.MrEligible;//�ϸ񲡰�

                s[93] = b.QcDoc.ID;//�ʿ�ҽʦ����

                s[94] = b.QcDoc.Name;//�ʿ�ҽʦ����

                s[95] = b.QcNurse.ID;//�ʿػ�ʿ����

                s[96] = b.QcNurse.Name;//�ʿػ�ʿ����

                s[97] = b.CheckDate.ToString();//���ʱ��

                s[98] = b.YnFirst;//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ

                s[99] = b.RhBlood;//RhѪ��(����)

                s[100] = b.ReactionBlood;//��Ѫ��Ӧ�����ޣ�

                s[101] = b.BloodRed;//��ϸ����

                s[102] = b.BloodPlatelet;//ѪС����

                s[103] = b.BloodPlasma;//Ѫ����

                s[104] = b.BloodWhole;//ȫѪ��

                s[105] = b.BloodOther;//������Ѫ��

                s[106] = b.XNum;//X���

                s[107] = b.CtNum;//CT��

                s[108] = b.MriNum;//MRI��

                s[109] = b.PathNum;//�����

                s[110] = b.DsaNum;//DSA��

                s[111] = b.PetNum;//PET��

                s[112] = b.EctNum;//ECT��

                s[113] = b.XQty.ToString();//X�ߴ���

                s[114] = b.CTQty.ToString();//CT����

                s[115] = b.MRQty.ToString();//MR����

                s[116] = b.DSAQty.ToString();//DSA����

                s[117] = b.PetQty.ToString();//PET����

                s[118] = b.EctQty.ToString();//ECT����

                s[119] = b.PatientInfo.Memo;//˵��

                s[120] = b.BarCode;//�鵵�����

                s[121] = b.LendStat;//��������״̬(O��� I�ڼ�)

                s[122] = b.PatientInfo.CaseState;//����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч

                s[123] = b.OperInfo.ID;//����Ա

                //				s[124]=b.OperDate.ToString() ;//����ʱ��
                s[124] = b.VisiPeriodWeek; //������� ��
                s[125] = b.VisiPeriodMonth; //������� ��
                s[126] = b.VisiPeriodYear;//������� ��
                s[127] = b.SpecalNus.ToString();  // ���⻤��(��)                                        
                s[128] = b.INus.ToString(); //I������ʱ��(��)                                     
                s[129] = b.IINus.ToString(); //II������ʱ��(��)                                    
                s[130] = b.IIINus.ToString(); //III������ʱ��(��)                                   
                s[131] = b.StrictNuss.ToString(); //��֢�໤ʱ��( Сʱ)                                 
                s[132] = b.SuperNus.ToString(); //�ؼ�����ʱ��(Сʱ)     
                s[133] = b.PackupMan.ID; //����Ա
                s[134] = b.Disease30; //������ 
                s[135] = b.IsHandCraft;//�ֹ�¼�벡�� ��־

                s[136] = b.ClinicDiag.ID;
                s[137] = b.ClinicDiag.Name;
                s[138] = b.InHospitalDiag.ID;
                s[139] = b.InHospitalDiag.Name;
                s[140] = b.OutDiag.ID;//��Ժ����� ����
                s[141] = b.OutDiag.Name;//��Ժ����� ����
                s[142] = b.OutDiag.User01;//��Ժ����� �������
                s[143] = b.OutDiag.User02;//��Ժ����ϲ���������
                s[144] = b.FirstOperation.ID;//��һ����������
                s[145] = b.FirstOperation.Name;//��һ����������
                s[146] = b.FirstOperationDoc.ID;//��һ������ҽʦ����
                s[147] = b.FirstOperationDoc.Name;//��һ������ҽʦ����
                s[148] = b.SyndromeFlag; //�Ƿ��в���֢
                s[149] = b.InfectionNum.ToString();//Ժ�ڸ�Ⱦ���� 
                s[150] = b.OperationCoding.ID;//��������Ա 
                s[151] = b.InfectionPosition.ID; //Ժ�ڸ�Ⱦ��λ����
                s[152] = b.InfectionPosition.Name; //Ժ�ڸ�Ⱦ��λ����

                s[153] = b.PathologicalDiagCode;//������ϱ���-��ҽ2010-2-2
                s[154] = b.PathologicalDiagName;//�����������-��ҽ2010-2-2
                s[155] = b.InjuryOrPoisoningCauseCode;//�����ж����ⲿ���ر���-��ҽ2010-2-2
                s[156] = b.InjuryOrPoisoningCause;//�����ж����ⲿ����-��ҽ2010-2-2

                s[157] = b.CaseNO;//������
                s[158] = b.Out_Type; //��Ժ��ʽ��1������ 2���Զ� 3��תԺ��
                s[159] = b.Cure_Type; //�������1����      2����      3��������
                s[160] = b.Use_CHA_Med; //������ҩ�Ƽ���0��δ֪   1����    2���ޣ�
                s[161] = b.Save_Type; //���ȷ�����1����     2����       3��������
                s[162] = b.Ever_Sickintodeath; //�Ƿ����Σ�أ������ǡ�����������
                s[163] = b.Ever_Firstaid; //�Ƿ���ּ�֢�������ǡ�����������
                s[164] = b.Ever_Difficulty; //�Ƿ������������������ǡ�������
                s[165] = b.ReactionLiquid; //��Һ��Ӧ�������С������ޡ�����δ�䣩
                s[166] = b.InfectionDiseasesReport; //��Ⱦ������
                s[167] = b.FourDiseasesReport; //�Ĳ�����
                s[168] = b.OutDept.Memo;//ת����ҽԺ

                s[169] = b.BabyAge;//����һ�������� 
                s[170] = b.BabyBirthWeight;//�������������� 
                s[171] = b.BabyInWeight; //��������Ժ����
                s[172] = b.DutyNurse.ID; //���λ�ʿ����
                s[173] = b.DutyNurse.Name;//���λ�ʿ����
                s[174] = b.HighReceiveHopital;//����ҽ�ƻ���
                s[175] = b.LowerReceiveHopital;//��������
                s[176] = b.ComeBackInMonth;//��Ժ31������סԺ��־ 1 �� 2 ��
                s[177] = b.ComeBackPurpose;//31����סԺĿ��
                s[178] = b.OutComeDay.ToString(); //­�����˻��߻���ʱ�� ��Ժǰ��
                s[179] = b.OutComeHour.ToString(); //­�����˻��߻���ʱ�� ��ԺǰСʱ
                s[180] = b.OutComeMin.ToString(); //­�����˻��߻���ʱ�� ��Ժǰ����
                s[181] = b.InComeDay.ToString(); //­�����˻��߻���ʱ�� ��Ժ����
                s[182] = b.InComeHour.ToString(); //­�����˻��߻���ʱ�� ��Ժ��Сʱ
                s[183] = b.InComeMin.ToString(); //­�����˻��߻���ʱ�� ��Ժ�����
                s[184] = b.Dept_Change; //ת�ƿƱ�
                s[185] = b.PatientInfo.Kin.Memo; //���ϵ��ע
                s[186] = b.CurrentAddr;//��סַ
                s[187] = b.CurrentPhone;//��סַ�绰
                s[188] = b.CurrentZip;//��סַ�ʱ�
                s[189] = b.InRoom;//��Ժ����
                s[190] = b.OutRoom;//��Ժ����
                s[191] = b.InPath;//��Ժ;�� 1���� 2���� 3����ҽ�ƻ���ת�� 9����
                s[192] = b.ExampleType;//�������� Aһ�� B�� C���� DΣ�� 
                s[193] = b.ClinicPath;//�ٴ�·������ 1�� 2��
                s[194] = b.UploadStatu;//�㶫ʡ�����ϴ���־ 1 δ�ϴ� 2���ϴ�
                s[195] = b.IsDrgs;//drgs������� 0 �� 1 �� 
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

        }
        /// <summary>
        /// ��ȡ��sql���
        /// </summary>
        /// <returns></returns>
        private string GetCaseSql()
        {
            string strSQL = "";
            //if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfo.Select", ref strSQL) == -1)
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfo.Select.HIS50", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            return strSQL;
        }
        /// <summary>
        /// ����SQL��ѯ��������������ҳ����Ϣ
        /// zhangjunyi@FS.com �޸�
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>ʧ�ܷ��� null �ɹ����ط�����������Ϣ</returns>
        private ArrayList myGetCaseBaseInfo(string strSQL)
        {
            //ִ�вٲ�ѯ����
            this.ExecQuery(strSQL);
            //��ȡ����
            //			FS.HISFC.Models.HealthRecord.Base b = ReaderBase();
            ArrayList list = new ArrayList();
            FS.HISFC.Models.HealthRecord.Base b = null;
            try
            {
                while (this.Reader.Read())
                {
                    b = new FS.HISFC.Models.HealthRecord.Base();
                    b.PatientInfo.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//סԺ��ˮ��
                    b.PatientInfo.PID.PatientNO = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString();//סԺ������

                    b.PatientInfo.PID.CardNO = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//�����

                    b.PatientInfo.Name = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//����
                    b.PatientInfo.Name = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();
                    b.PatientInfo.PID.Name = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();

                    b.Nomen = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//������

                    b.PatientInfo.Sex.ID = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//�Ա�

                    b.PatientInfo.Birthday = System.Convert.ToDateTime(this.Reader[6] == DBNull.Value ? "0001-01-01" : this.Reader[6].ToString());//��������

                    b.PatientInfo.Country.ID = this.Reader[7] == DBNull.Value ? string.Empty : this.Reader[7].ToString();//����

                    b.PatientInfo.Nationality.ID = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();//����

                    b.PatientInfo.Profession.ID = this.Reader[9] == DBNull.Value ? string.Empty : this.Reader[9].ToString();//ְҵ

                    b.PatientInfo.BloodType.ID = this.Reader[10] == DBNull.Value ? string.Empty : this.Reader[10].ToString();//Ѫ�ͱ���

                    b.PatientInfo.MaritalStatus.ID = this.Reader[11] == DBNull.Value ? string.Empty : this.Reader[11].ToString();//���

                    b.PatientInfo.Age = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();//����

                    b.AgeUnit = this.Reader[13] == DBNull.Value ? string.Empty : this.Reader[13].ToString();//���䵥λ

                    b.PatientInfo.IDCard = this.Reader[14] == DBNull.Value ? string.Empty : this.Reader[14].ToString();//���֤��

                    b.PatientInfo.PVisit.InSource.ID = this.Reader[15] == DBNull.Value ? string.Empty : this.Reader[15].ToString();//������Դ

                    b.PatientInfo.Pact.PayKind.ID = this.Reader[16] == DBNull.Value ? string.Empty : this.Reader[16].ToString();//��������

                    b.PatientInfo.Pact.ID = this.Reader[17] == DBNull.Value ? string.Empty : this.Reader[17].ToString();//��ͬ����

                    b.PatientInfo.SSN = this.Reader[18] == DBNull.Value ? string.Empty : this.Reader[18].ToString();//ҽ�����Ѻ�

                    b.PatientInfo.DIST = this.Reader[19] == DBNull.Value ? string.Empty : this.Reader[19].ToString();//����

                    b.PatientInfo.AreaCode = this.Reader[20] == DBNull.Value ? string.Empty : this.Reader[20].ToString();//������

                    b.PatientInfo.AddressHome = this.Reader[21] == DBNull.Value ? string.Empty : this.Reader[21].ToString();//��ͥסַ

                    b.PatientInfo.PhoneHome = this.Reader[22] == DBNull.Value ? string.Empty : this.Reader[22].ToString();//��ͥ�绰

                    b.PatientInfo.HomeZip = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString();//סַ�ʱ�

                    b.PatientInfo.AddressBusiness = this.Reader[24] == DBNull.Value ? string.Empty : this.Reader[24].ToString();//��λ��ַ

                    b.PatientInfo.PhoneBusiness = this.Reader[25] == DBNull.Value ? string.Empty : this.Reader[25].ToString();//��λ�绰

                    b.PatientInfo.BusinessZip = this.Reader[26] == DBNull.Value ? string.Empty : this.Reader[26].ToString();//��λ�ʱ�

                    b.PatientInfo.Kin.Name = this.Reader[27] == DBNull.Value ? string.Empty : this.Reader[27].ToString();//��ϵ��

                    b.PatientInfo.Kin.RelationLink = this.Reader[28] == DBNull.Value ? string.Empty : this.Reader[28].ToString();//�뻼�߹�ϵ

                    b.PatientInfo.Kin.RelationPhone = this.Reader[29] == DBNull.Value ? string.Empty : this.Reader[29].ToString();//��ϵ�绰

                    b.PatientInfo.Kin.RelationAddress = this.Reader[30] == DBNull.Value ? string.Empty : this.Reader[30].ToString();//��ϵ��ַ

                    b.ClinicDoc.ID = this.Reader[31] == DBNull.Value ? string.Empty : this.Reader[31].ToString();//�������ҽ��

                    b.ClinicDoc.Name = this.Reader[32] == DBNull.Value ? string.Empty : this.Reader[32].ToString();//�������ҽ������

                    b.ComeFrom = this.Reader[33] == DBNull.Value ? string.Empty : this.Reader[33].ToString();//ת��ҽԺ

                    b.PatientInfo.PVisit.InTime = System.Convert.ToDateTime(this.Reader[34] == DBNull.Value ? "0001-01-01" : this.Reader[34].ToString());//��Ժ����

                    b.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35] == DBNull.Value ? "1" : this.Reader[35].ToString());//סԺ����

                    b.InDept.ID = this.Reader[36] == DBNull.Value ? string.Empty : this.Reader[36].ToString();//��Ժ���Ҵ���

                    b.InDept.Name = this.Reader[37] == DBNull.Value ? string.Empty : this.Reader[37].ToString();//��Ժ��������

                    b.PatientInfo.PVisit.InSource.ID = this.Reader[38] == DBNull.Value ? string.Empty : this.Reader[38].ToString();//��Ժ��Դ

                    b.PatientInfo.PVisit.Circs.ID = this.Reader[39] == DBNull.Value ? string.Empty : this.Reader[39].ToString();//��Ժ״̬

                    b.DiagDate = System.Convert.ToDateTime(this.Reader[40] == DBNull.Value ? "0001-01-01" : this.Reader[40].ToString());//ȷ������

                    b.OperationDate = System.Convert.ToDateTime(this.Reader[41] == DBNull.Value ? "0001-01-01" : this.Reader[41].ToString());//��������

                    b.PatientInfo.PVisit.OutTime = System.Convert.ToDateTime(this.Reader[42] == DBNull.Value ? "0001-01-01" : this.Reader[42].ToString());//��Ժ����

                    b.OutDept.ID = this.Reader[43] == DBNull.Value ? string.Empty : this.Reader[43].ToString();//��Ժ���Ҵ���

                    b.OutDept.Name = this.Reader[44] == DBNull.Value ? string.Empty : this.Reader[44].ToString();//��Ժ��������

                    b.PatientInfo.PVisit.ZG.ID = this.Reader[45] == DBNull.Value ? string.Empty : this.Reader[45].ToString();//ת�����

                    b.DiagDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[46] == DBNull.Value ? "1" : this.Reader[46].ToString());//ȷ������

                    b.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[47] == DBNull.Value ? "0" : this.Reader[47].ToString());//סԺ����

                    b.DeadDate = System.Convert.ToDateTime(this.Reader[48] == DBNull.Value ? "0001-01-01" : this.Reader[48].ToString());//��������

                    b.DeadReason = this.Reader[49] == DBNull.Value ? string.Empty : this.Reader[49].ToString();//����ԭ��

                    b.CadaverCheck = this.Reader[50] == DBNull.Value ? string.Empty : this.Reader[50].ToString();//ʬ��

                    b.DeadKind = this.Reader[51] == DBNull.Value ? string.Empty : this.Reader[51].ToString();//��������

                    b.BodyAnotomize = this.Reader[52] == DBNull.Value ? string.Empty : this.Reader[52].ToString();//ʬ����ʺ�

                    b.Hbsag = this.Reader[53] == DBNull.Value ? string.Empty : this.Reader[53].ToString();//�Ҹα��濹ԭ

                    b.HcvAb = this.Reader[54] == DBNull.Value ? string.Empty : this.Reader[54].ToString();//���β�������

                    b.HivAb = this.Reader[55] == DBNull.Value ? string.Empty : this.Reader[55].ToString();//�������������ȱ�ݲ�������

                    b.CePi = this.Reader[56] == DBNull.Value ? string.Empty : this.Reader[56].ToString();//�ż�_��Ժ����

                    b.PiPo = this.Reader[57] == DBNull.Value ? string.Empty : this.Reader[57].ToString();//���_Ժ����

                    b.OpbOpa = this.Reader[58] == DBNull.Value ? string.Empty : this.Reader[58].ToString();//��ǰ_�����

                    b.ClX = this.Reader[59] == DBNull.Value ? string.Empty : this.Reader[59].ToString();//�ٴ�_X�����

                    b.ClCt = this.Reader[60] == DBNull.Value ? string.Empty : this.Reader[60].ToString();//�ٴ�_CT����

                    b.ClMri = this.Reader[61] == DBNull.Value ? string.Empty : this.Reader[61].ToString();//�ٴ�_MRI����

                    b.ClPa = this.Reader[62] == DBNull.Value ? string.Empty : this.Reader[62].ToString();//�ٴ�_�������

                    b.FsBl = this.Reader[63] == DBNull.Value ? string.Empty : this.Reader[63].ToString();//����_�������

                    b.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[64] == DBNull.Value ? "0" : this.Reader[64].ToString());//���ȴ���

                    b.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[65] == DBNull.Value ? "0" : this.Reader[65].ToString());//�ɹ�����

                    b.TechSerc = this.Reader[66] == DBNull.Value ? string.Empty : this.Reader[66].ToString();//ʾ�̿���

                    b.VisiStat = this.Reader[67] == DBNull.Value ? string.Empty : this.Reader[67].ToString();//�Ƿ�����

                    b.VisiPeriod = System.Convert.ToDateTime(this.Reader[68] == DBNull.Value ? "0001-01-01" : this.Reader[68].ToString());//�������

                    b.InconNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69] == DBNull.Value ? "0" : this.Reader[69].ToString());//Ժ�ʻ������ 

                    b.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[70] == DBNull.Value ? "0" : this.Reader[70].ToString());//70 Զ�̻������

                    b.AnaphyFlag = this.Reader[71] == DBNull.Value ? string.Empty : this.Reader[71].ToString();//ҩ�����

                    b.FirstAnaphyPharmacy.ID = this.Reader[72] == DBNull.Value ? string.Empty : this.Reader[72].ToString();//����ҩ������

                    b.SecondAnaphyPharmacy.ID = this.Reader[73] == DBNull.Value ? string.Empty : this.Reader[73].ToString();//����ҩ������

                    b.CoutDate = System.Convert.ToDateTime(this.Reader[74] == DBNull.Value ? "0001-01-01" : this.Reader[74].ToString());//���ĺ��Ժ����

                    b.PatientInfo.PVisit.AdmittingDoctor.ID = this.Reader[75] == DBNull.Value ? string.Empty : this.Reader[75].ToString();//סԺҽʦ����

                    b.PatientInfo.PVisit.AdmittingDoctor.Name = this.Reader[76] == DBNull.Value ? string.Empty : this.Reader[76].ToString();//סԺҽʦ����

                    b.PatientInfo.PVisit.AttendingDoctor.ID = this.Reader[77] == DBNull.Value ? string.Empty : this.Reader[77].ToString();//����ҽʦ����

                    b.PatientInfo.PVisit.AttendingDoctor.Name = this.Reader[78] == DBNull.Value ? string.Empty : this.Reader[78].ToString();//����ҽʦ����

                    b.PatientInfo.PVisit.ConsultingDoctor.ID = this.Reader[79] == DBNull.Value ? string.Empty : this.Reader[79].ToString();//����ҽʦ����

                    b.PatientInfo.PVisit.ConsultingDoctor.Name = this.Reader[80] == DBNull.Value ? string.Empty : this.Reader[80].ToString();//����ҽʦ����

                    b.PatientInfo.PVisit.ReferringDoctor.ID = this.Reader[81] == DBNull.Value ? string.Empty : this.Reader[81].ToString();//�����δ���

                    b.PatientInfo.PVisit.ReferringDoctor.Name = this.Reader[82] == DBNull.Value ? string.Empty : this.Reader[82].ToString();//����������

                    b.RefresherDoc.ID = this.Reader[83] == DBNull.Value ? string.Empty : this.Reader[83].ToString();//����ҽʦ����

                    b.RefresherDoc.Name = this.Reader[84] == DBNull.Value ? string.Empty : this.Reader[84].ToString();//����ҽ������

                    b.GraduateDoc.ID = this.Reader[85] == DBNull.Value ? string.Empty : this.Reader[85].ToString();//�о���ʵϰҽʦ����

                    b.GraduateDoc.Name = this.Reader[86] == DBNull.Value ? string.Empty : this.Reader[86].ToString();//�о���ʵϰҽʦ����

                    b.PatientInfo.PVisit.TempDoctor.ID = this.Reader[87] == DBNull.Value ? string.Empty : this.Reader[87].ToString();//ʵϰҽʦ����

                    b.PatientInfo.PVisit.TempDoctor.Name = this.Reader[88] == DBNull.Value ? string.Empty : this.Reader[88].ToString();//ʵϰҽʦ����

                    b.CodingOper.ID = this.Reader[89] == DBNull.Value ? string.Empty : this.Reader[89].ToString();//����Ա����

                    b.CodingOper.Name = this.Reader[90] == DBNull.Value ? string.Empty : this.Reader[90].ToString();//����Ա����

                    b.MrQuality = this.Reader[91] == DBNull.Value ? string.Empty : this.Reader[91].ToString();//��������

                    b.MrEligible = this.Reader[92] == DBNull.Value ? string.Empty : this.Reader[92].ToString();//�ϸ񲡰�

                    b.QcDoc.ID = this.Reader[93] == DBNull.Value ? string.Empty : this.Reader[93].ToString();//�ʿ�ҽʦ����

                    b.QcDoc.Name = this.Reader[94] == DBNull.Value ? string.Empty : this.Reader[94].ToString();//�ʿ�ҽʦ����

                    b.QcNurse.ID = this.Reader[95] == DBNull.Value ? string.Empty : this.Reader[95].ToString();//�ʿػ�ʿ����

                    b.QcNurse.Name = this.Reader[96] == DBNull.Value ? string.Empty : this.Reader[96].ToString();//�ʿػ�ʿ����

                    b.CheckDate = System.Convert.ToDateTime(this.Reader[97] == DBNull.Value ? "0001-01-01" : this.Reader[97].ToString());//���ʱ��

                    b.YnFirst = this.Reader[98] == DBNull.Value ? string.Empty : this.Reader[98].ToString();//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ

                    b.RhBlood = this.Reader[99] == DBNull.Value ? string.Empty : this.Reader[99].ToString();//RhѪ��(����)

                    b.ReactionBlood = this.Reader[100] == DBNull.Value ? string.Empty : this.Reader[100].ToString();//��Ѫ��Ӧ�����ޣ�

                    b.BloodRed = this.Reader[101] == DBNull.Value ? "0" : this.Reader[101].ToString();//��ϸ����

                    b.BloodPlatelet = this.Reader[102] == DBNull.Value ? "0" : this.Reader[102].ToString();//ѪС����

                    b.BloodPlasma = this.Reader[103] == DBNull.Value ? "0" : this.Reader[103].ToString();//Ѫ����

                    b.BloodWhole = this.Reader[104] == DBNull.Value ? "0" : this.Reader[104].ToString();//ȫѪ��

                    b.BloodOther = this.Reader[105] == DBNull.Value ? "0" : this.Reader[105].ToString();//������Ѫ��

                    b.XNum = this.Reader[106] == DBNull.Value ? string.Empty : this.Reader[106].ToString();//X���

                    b.CtNum = this.Reader[107] == DBNull.Value ? string.Empty : this.Reader[107].ToString();//CT��

                    b.MriNum = this.Reader[108] == DBNull.Value ? string.Empty : this.Reader[108].ToString();//MRI��

                    b.PathNum = this.Reader[109] == DBNull.Value ? string.Empty : this.Reader[109].ToString();//�����

                    b.DsaNum = this.Reader[110] == DBNull.Value ? string.Empty : this.Reader[110].ToString();//DSA��

                    b.PetNum = this.Reader[111] == DBNull.Value ? string.Empty : this.Reader[111].ToString();//PET��

                    b.EctNum = this.Reader[112] == DBNull.Value ? string.Empty : this.Reader[112].ToString();//ECT��

                    b.XQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[113] == DBNull.Value ? "0" : this.Reader[113].ToString());//X�ߴ���

                    b.CTQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[114] == DBNull.Value ? "0" : this.Reader[114].ToString());//CT����

                    b.MRQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[115] == DBNull.Value ? "0" : this.Reader[115].ToString());//MR����

                    b.DSAQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[116] == DBNull.Value ? "0" : this.Reader[116].ToString());//DSA����

                    b.PetQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[117] == DBNull.Value ? "0" : this.Reader[117].ToString());//PET����

                    b.EctQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[118] == DBNull.Value ? "0" : this.Reader[118].ToString());//ECT����

                    b.PatientInfo.Memo = this.Reader[119] == DBNull.Value ? string.Empty : this.Reader[119].ToString();//˵��

                    b.BarCode = this.Reader[120] == DBNull.Value ? string.Empty : this.Reader[120].ToString();//�鵵�����

                    b.LendStat = this.Reader[121] == DBNull.Value ? string.Empty : this.Reader[121].ToString();//��������״̬(O��� I�ڼ�)

                    b.PatientInfo.CaseState = this.Reader[122] == DBNull.Value ? string.Empty : this.Reader[122].ToString();//����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч

                    b.OperInfo.ID = this.Reader[123] == DBNull.Value ? string.Empty : this.Reader[123].ToString();//����Ա

                    b.OperInfo.OperTime = System.Convert.ToDateTime(this.Reader[124] == DBNull.Value ? "0001-01-01" : this.Reader[124].ToString());//����ʱ��
                    b.VisiPeriodWeek = this.Reader[125] == DBNull.Value ? string.Empty : this.Reader[125].ToString();
                    b.VisiPeriodMonth = this.Reader[126] == DBNull.Value ? string.Empty : this.Reader[126].ToString();
                    b.VisiPeriodYear = this.Reader[127] == DBNull.Value ? string.Empty : this.Reader[127].ToString();
                    b.SpecalNus = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[128] == DBNull.Value ? 0 : this.Reader[128]); 	// ���⻤��(��)                                        
                    b.INus = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[129] == DBNull.Value ? 0 : this.Reader[129]); 	//I������ʱ��(��)                                     
                    b.IINus = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[130] == DBNull.Value ? 0 : this.Reader[130]);	//II������ʱ��(��)                                    
                    b.IIINus = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[131] == DBNull.Value ? 0 : this.Reader[131]);	//III������ʱ��(��)                                   
                    b.StrictNuss = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[132] == DBNull.Value ? 0 : this.Reader[132]);	//��֢�໤ʱ��( Сʱ)                                 
                    b.SuperNus = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[133] == DBNull.Value ? 0 : this.Reader[133]);	//�ؼ�����ʱ��(Сʱ) 
                    b.PackupMan.ID = this.Reader[134] == DBNull.Value ? string.Empty : this.Reader[134].ToString(); // ������
                    b.Disease30 = this.Reader[135] == DBNull.Value ? string.Empty : this.Reader[135].ToString();// ������ 
                    b.IsHandCraft = this.Reader[136] == DBNull.Value ? string.Empty : this.Reader[136].ToString(); //�ֶ�¼����
                    b.ClinicDiag.ID = this.Reader[137] == DBNull.Value ? string.Empty : this.Reader[137].ToString(); //������� ����
                    b.ClinicDiag.Name = this.Reader[138] == DBNull.Value ? string.Empty : this.Reader[138].ToString();//������� ����
                    b.InHospitalDiag.ID = this.Reader[139] == DBNull.Value ? string.Empty : this.Reader[139].ToString(); //��Ժ��� ����
                    b.InHospitalDiag.Name = this.Reader[140] == DBNull.Value ? string.Empty : this.Reader[140].ToString();//��Ժ��� ����
                    b.OutDiag.ID = this.Reader[141] == DBNull.Value ? string.Empty : this.Reader[141].ToString();//��Ժ����� ����
                    b.OutDiag.Name = this.Reader[142] == DBNull.Value ? string.Empty : this.Reader[142].ToString();//��Ժ����� ����
                    b.OutDiag.User01 = this.Reader[143] == DBNull.Value ? string.Empty : this.Reader[143].ToString();//��Ժ����� �������
                    b.OutDiag.User02 = this.Reader[144] == DBNull.Value ? string.Empty : this.Reader[144].ToString();//��Ժ����ϲ���������
                    b.FirstOperation.ID = this.Reader[145] == DBNull.Value ? string.Empty : this.Reader[145].ToString();//��һ����������
                    b.FirstOperation.Name = this.Reader[146] == DBNull.Value ? string.Empty : this.Reader[146].ToString();//��һ����������
                    b.FirstOperationDoc.ID = this.Reader[147] == DBNull.Value ? string.Empty : this.Reader[147].ToString();//��һ������ҽʦ����
                    b.FirstOperationDoc.Name = this.Reader[148] == DBNull.Value ? string.Empty : this.Reader[148].ToString();//��һ������ҽʦ����
                    b.SyndromeFlag = this.Reader[149] == DBNull.Value ? string.Empty : this.Reader[149].ToString();//�Ƿ��в���֢ 1 �� 0 ��
                    b.InfectionNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[150] == DBNull.Value ? "0" : this.Reader[150].ToString()); //Ժ�ڸ�Ⱦ����
                    b.OperationCoding.ID = this.Reader[151] == DBNull.Value ? string.Empty : this.Reader[151].ToString();//��������Ա
                    b.InfectionPosition.ID = this.Reader[152] == DBNull.Value ? string.Empty : this.Reader[152].ToString();//ҽԺ��Ⱦ����
                    b.InfectionPosition.Name = this.Reader[153] == DBNull.Value ? string.Empty : this.Reader[153].ToString();//ҽԺ��Ⱦ����
                    b.PathologicalDiagCode = this.Reader[154] == DBNull.Value ? string.Empty : this.Reader[154].ToString();//������ϱ���
                    b.PathologicalDiagName= this.Reader[155] == DBNull.Value ? string.Empty : this.Reader[155].ToString();//�����������
                    b.InjuryOrPoisoningCauseCode = this.Reader[156] == DBNull.Value ? string.Empty : this.Reader[156].ToString();//�����ж����ⲿ���ر���
                    b.InjuryOrPoisoningCause = this.Reader[157] == DBNull.Value ? string.Empty : this.Reader[157].ToString();//�����ж����ⲿ��������
                    b.CaseNO = this.Reader[158] == DBNull.Value ? string.Empty : this.Reader[158].ToString();//������
                    b.Out_Type = this.Reader[159] == DBNull.Value ? string.Empty : this.Reader[159].ToString();////��Ժ��ʽ��1������ 2���Զ� 3��תԺ��
                    b.Cure_Type = this.Reader[160] == DBNull.Value ? string.Empty : this.Reader[160].ToString();//�������1����      2����      3��������
                    b.Use_CHA_Med = this.Reader[161] == DBNull.Value ? string.Empty : this.Reader[161].ToString();//������ҩ�Ƽ���0��δ֪   1����    2���ޣ�
                    b.Save_Type = this.Reader[162] == DBNull.Value ? string.Empty : this.Reader[162].ToString();//���ȷ�����1����     2����       3��������
                    b.Ever_Sickintodeath = this.Reader[163] == DBNull.Value ? string.Empty : this.Reader[163].ToString();//�Ƿ����Σ�أ������ǡ�����������
                    b.Ever_Firstaid = this.Reader[164] == DBNull.Value ? string.Empty : this.Reader[164].ToString();//�Ƿ���ּ�֢�������ǡ�����������
                    b.Ever_Difficulty = this.Reader[165] == DBNull.Value ? string.Empty : this.Reader[165].ToString();//�Ƿ������������������ǡ�������
                    b.ReactionLiquid = this.Reader[166] == DBNull.Value ? string.Empty : this.Reader[166].ToString();//��Һ��Ӧ�������С������ޡ�����δ�䣩
                    b.InfectionDiseasesReport = this.Reader[167] == DBNull.Value ? string.Empty : this.Reader[167].ToString();//��Ⱦ������
                    b.FourDiseasesReport = this.Reader[168] == DBNull.Value ? string.Empty : this.Reader[168].ToString();//�Ĳ�����
                    b.OutDept.Memo = this.Reader[169] == DBNull.Value ? string.Empty : this.Reader[169].ToString();//ת����ҽԺ

                    b.BabyAge = this.Reader[170] == DBNull.Value ? string.Empty : this.Reader[170].ToString(); //����һ��������
                    b.BabyBirthWeight = this.Reader[171] == DBNull.Value ? string.Empty : this.Reader[171].ToString();//��������������
                    b.BabyInWeight= this.Reader[172] == DBNull.Value ? string.Empty : this.Reader[172].ToString();//��������Ժ����
                    b.DutyNurse.ID = this.Reader[173] == DBNull.Value ? string.Empty : this.Reader[173].ToString();//���λ�ʿ
                    b.DutyNurse.Name = this.Reader[174] == DBNull.Value ? string.Empty : this.Reader[174].ToString();//���λ�ʿ����
                    b.HighReceiveHopital = this.Reader[175] == DBNull.Value ? string.Empty : this.Reader[175].ToString();//����ҽ�ƻ���
                    b.LowerReceiveHopital = this.Reader[176] == DBNull.Value ? string.Empty : this.Reader[176].ToString();//��������
                    b.ComeBackInMonth = this.Reader[177] == DBNull.Value ? string.Empty : this.Reader[177].ToString();//��Ժ31������סԺ��־ 1 �� 2 ��
                    b.ComeBackPurpose = this.Reader[178] == DBNull.Value ? string.Empty : this.Reader[178].ToString();//31����סԺĿ��
                    b.OutComeDay = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[179] == DBNull.Value ? "0" : this.Reader[179].ToString());////­�����˻��߻���ʱ�� ��Ժǰ��
                    b.OutComeHour = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[180] == DBNull.Value ? "0" : this.Reader[180].ToString());//­�����˻��߻���ʱ�� ��ԺǰСʱ
                    b.OutComeMin = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[181] == DBNull.Value ? "0" : this.Reader[181].ToString());//­�����˻��߻���ʱ�� ��Ժǰ����
                    b.InComeDay = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[182] == DBNull.Value ? "0" : this.Reader[182].ToString());//­�����˻��߻���ʱ�� ��Ժ����
                    b.InComeHour = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[183] == DBNull.Value ? "0" : this.Reader[183].ToString());//­�����˻��߻���ʱ�� ��Ժ��Сʱ
                    b.InComeMin = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[184] == DBNull.Value ? "0" : this.Reader[184].ToString());//­�����˻��߻���ʱ�� ��Ժ�����
                    b.Dept_Change = this.Reader[185] == DBNull.Value ? string.Empty : this.Reader[185].ToString();//ת�ƿƱ�
                    b.PatientInfo.Kin.Memo = this.Reader[186] == DBNull.Value ? string.Empty : this.Reader[186].ToString();//���ϵ��ע
                    b.CurrentAddr = this.Reader[187] == DBNull.Value ? string.Empty : this.Reader[187].ToString();//��סַ
                    b.CurrentPhone=this.Reader[188] == DBNull.Value ? string.Empty : this.Reader[188].ToString(); ;//��סַ�绰
                    b.CurrentZip = this.Reader[189] == DBNull.Value ? string.Empty : this.Reader[189].ToString(); ;//��סַ�ʱ�
                    b.InRoom = this.Reader[190] == DBNull.Value ? string.Empty : this.Reader[190].ToString(); ;//��Ժ����
                    b.OutRoom = this.Reader[191] == DBNull.Value ? string.Empty : this.Reader[191].ToString(); ;//��Ժ����
                    b.InPath = this.Reader[192] == DBNull.Value ? string.Empty : this.Reader[192].ToString(); ;//��Ժ;�� 1���� 2���� 3����ҽ�ƻ���ת�� 9����
                    b.ExampleType = this.Reader[193] == DBNull.Value ? string.Empty : this.Reader[193].ToString(); ;//�������� Aһ�� B�� C���� DΣ��
                    b.ClinicPath = this.Reader[194] == DBNull.Value ? string.Empty : this.Reader[194].ToString(); ;//��Ժ;�� 1�� 2��
                    b.UploadStatu = this.Reader[195] == DBNull.Value ? string.Empty : this.Reader[195].ToString(); ;//�ϴ���־ 1�� 2��
                    b.IsDrgs = this.Reader[196] == DBNull.Value ? string.Empty : this.Reader[196].ToString(); ;//�²�����ҳDRGS 1�� 2��
                    list.Add(b);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û��߲�����Ϣ����!" + ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// �õ�δ�ȵǼǲ�����Ϣ�Ļ��ߵ������Ϣ
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList myGetDiagInfo(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.DiagnoseBase dg;
            this.ExecQuery(strSql);

            try
            {
                while (this.Reader.Read())
                {
                    dg = new FS.HISFC.Models.HealthRecord.DiagnoseBase();

                    dg.ID = this.Reader[0] == DBNull.Value ? string.Empty : Reader[0].ToString();//סԺ��ˮ��
                    dg.Patient.ID = this.Reader[0] == DBNull.Value ? string.Empty : Reader[0].ToString();//סԺ��ˮ��
                    dg.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1] == DBNull.Value ? 0 : Reader[1]);//�������
                    dg.Patient.PID.CardNO = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//���￨��
                    dg.DiagType.ID = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//������
                    dg.ICD10.ID = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//��ϴ���
                    dg.ICD10.Name = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//�������
                    dg.DiagDate = System.Convert.ToDateTime(this.Reader[6] == DBNull.Value ? "0001-01-01" : this.Reader[6].ToString());//�������
                    dg.Doctor.ID = this.Reader[7] == DBNull.Value ? string.Empty : this.Reader[7].ToString();//���ҽ������
                    dg.Doctor.Name = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();//���ҽʦ����
                    dg.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9] == DBNull.Value ? "0" : this.Reader[9].ToString());//�Ƿ���Ч0��Ч1��Ч
                    dg.Dept.ID = this.Reader[10] == DBNull.Value ? string.Empty : this.Reader[10].ToString();//����
                    dg.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11] == DBNull.Value ? "0" : this.Reader[11].ToString());//�Ƿ������
                    dg.Memo = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();//��ע
                    al.Add(dg);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û���סԺ�����Ϣ����!" + ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return al;
        }
        /// <summary>
        /// ��������DateTimeʱ���
        /// </summary>
        /// <param name="flag">"YYYY"����|"MM"|��|"DD"��</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">����ʱ��</param>
        /// <returns>double</returns>
        private double DateDiff(string flag, DateTime dateBegin, DateTime dateEnd)
        {
            double diff = 0;
            try
            {
                TimeSpan TS = new TimeSpan(dateEnd.Ticks - dateBegin.Ticks);

                switch (flag.ToLower())
                {
                    case "m":
                        diff = Convert.ToDouble(TS.TotalMinutes);
                        break;
                    case "s":
                        diff = Convert.ToDouble(TS.TotalSeconds);
                        break;
                    case "t":
                        diff = Convert.ToDouble(TS.Ticks);
                        break;
                    case "mm":
                        diff = Convert.ToDouble(TS.TotalMilliseconds);
                        break;
                    case "yyyy":
                        diff = Convert.ToDouble(TS.TotalDays / 365);
                        break;
                    case "q":
                        diff = Convert.ToDouble((TS.TotalDays / 365) / 4);
                        break;
                    case "dd":
                        diff = Convert.ToDouble((TS.TotalDays));
                        break;
                    default:
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                }
            }
            catch
            {

                diff = -1;
            }

            return diff;
        }
        #endregion

        /// <summary>
        /// �������պ͵�ǰʱ��ó����ߵ���������䵥λ
        /// ID �������� Name�������䵥λ
        /// </summary>
        /// <param name="bornDate">���ߵó�������</param>
        /// <returns>FS.FrameWork.Models.NeuObject</returns>
        public new FS.FrameWork.Models.NeuObject GetAge(DateTime bornDate)
        {
            DateTime nowDate;
            double temp;

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            nowDate = this.GetDateTimeFromSysDateTime();


            temp = DateDiff("YYYY", bornDate, nowDate);
            obj.Name = "Y";

            if (temp < 0) //С��һ��
            {
                temp = DateDiff("DD", bornDate, nowDate);

                if (temp < 28)
                {
                    obj.Name = "D";
                }
                else
                {
                    obj.Name = "M";
                }
            }

            obj.ID = temp.ToString();

            return obj;
        }
        /// <summary>
        /// ͨ��������ȡ����
        /// </summary>
        /// <param name="dtBirth"></param>
        /// <param name="dtInDate"></param>
        /// <returns></returns>
        public string GetAgeByFun(DateTime dtBirth, DateTime dtInDate)
        {
            string strSql = "";
            string strAge = "";
            if (this.Sql.GetSql("Case.BaseDML.GetAge.ByFunGetAgeNew", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, dtBirth, dtInDate);
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    strAge = Reader[0].ToString(); //����
                }
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return strAge;
        }
        /// <summary>
        /// ��ȡ��Ժ�Ǽ���Ժ���� 
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Location GetDeptIn(string inpatienNo)
        {
            string strSql = "";
            FS.HISFC.Models.RADT.Location info = null;
            if (this.Sql.GetSql("Case.BaseDML.GetDeptIn.1", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                info = null;
            }
            return info;
        }
        /// <summary>
        /// ��ȡ������Ժ���� 
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Location GetDeptIn1(string inpatienNo)
        {
            string strSql = "";
            FS.HISFC.Models.RADT.Location info = null;
            if (this.Sql.GetSql("Case.BaseDML.GetDeptIn.11", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                info = null;
            }
            return info;
        }
        /// <summary>
        /// ��ȡ��Ժ����
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Location GetDeptOut(string inpatienNo)
        {
            string strSql = "";
            FS.HISFC.Models.RADT.Location info = null;
            if (this.Sql.GetSql("Case.BaseDML.GetDeptOut", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, inpatienNo);
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.Location();
                    info.Dept.ID = Reader[0].ToString(); //���ұ���
                    info.Dept.Name = Reader[1].ToString();//��������
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                info = null;
            }
            return info;
        }
        /// <summary>
        /// ��������ͳ��
        /// </summary>
        /// <param name="inpatienId">סԺ��ˮ��</param>
        /// <param name="type">���ͣ���ƴ�룩</param>
        /// <returns></returns>
        public int GetNursingNum(string inpatienId, string type)
        {
            string strSql = "";
            int iReturn = 0;

            this.Sql.GetSql("CASE.BaseDML.GetNursingNum", ref strSql);

            try
            {
                strSql = string.Format(strSql, inpatienId, type);
                //��ѯ
                if (this.ExecQuery(strSql) < 0)
                {
                    this.Err = "ִ��sqlʧ��!";
                    return -1;
                }
                this.Reader.Read();
                iReturn = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                this.Reader.Close();

            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                this.Reader.Close();
                return -1;
            }
            return iReturn;
        }
        /// <summary>
        /// ��ȡ��Ժ־��Ժ���
        /// </summary>
        /// <param name="InPatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryCaseDiagnoseByInpatientNo(string InPatientNO)
        {
            string strSql = string.Empty;
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.Diagnose diagnoseObj;
            if (Sql.GetSql("FS.HISFC.Management.HealthRecord.QueryHealthRecordCustomDiagnose", ref strSql) == -1)
            {
                Err = "��ȡ[FS.HISFC.Management.HealthRecord.QueryHealthRecordCustomDiagnose]�ӣѣ�������";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, InPatientNO);
                this.ExecQuery(strSql);

                while (Reader.Read())
                {
                    diagnoseObj = new FS.HISFC.Models.HealthRecord.Diagnose();
                    diagnoseObj.ID = this.Reader["INPATIENT_NO"].ToString();
                    diagnoseObj.DiagInfo.ICD10.ID = this.Reader["ICD_CODE"].ToString();
                    diagnoseObj.DiagInfo.Name = this.Reader["DIAG_NAME"].ToString();
                    diagnoseObj.DiagInfo.Doctor.ID = this.Reader["DOCT_CODE"].ToString();
                    diagnoseObj.DiagInfo.Doctor.Name = this.Reader["DOCT_NAME"].ToString();
                    diagnoseObj.MainFlag = this.Reader["MAIN_FLAG"].ToString();
                    diagnoseObj.DiagInfo.DiagType.ID = this.Reader["DIAG_KIND"].ToString();
                    diagnoseObj.DiagOutState = this.Reader["DIAG_OUTSTATE"].ToString();
                    diagnoseObj.Memo = this.Reader["DIAG_OUTSTATE_NAME"].ToString();
                    al.Add(diagnoseObj);
                }

            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
            finally
            {
                Reader.Close();
            }
            return al;


        }
        /// <summary>
        /// ��������ͳ��
        /// </summary>
        /// <param name="inpatienId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int StateHl(string inpatienId, string type)
        {
            string strSql = "";
            int iReturn = 0;
            if (type == "T")
            {
                this.Sql.GetSql("Case.State.GetTjhl", ref strSql);
            }
            else if (type == "Y")
            {
                this.Sql.GetSql("Case.State.GetYjhl", ref strSql);
            }
            else if (type == "E")
            {
                this.Sql.GetSql("Case.State.GetEjhl", ref strSql);
            }
            else if (type == "S")
            {
                this.Sql.GetSql("Case.State.GetSjhl", ref strSql);
            }
            else if (type == "TS")
            {
                this.Sql.GetSql("Case.State.GetTShl", ref strSql);
            }
            else if (type == "ZZ")
            {
                this.Sql.GetSql("Case.State.GetZZJH", ref strSql);
            }
            try
            {
                strSql = string.Format(strSql, inpatienId);
                //��ѯ
                if (this.ExecQuery(strSql) < 0)
                {
                    this.Err = "ִ��sqlʧ��!";
                    return -1;
                }
                this.Reader.Read();
                iReturn = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                this.Reader.Close();

            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                this.Reader.Close();
                return -1;
            }
            return iReturn;
        }
        /// <summary>
        /// ��ȡҽԺ����
        /// </summary>
        /// <returns></returns>
        public string GetHosName()
        {
            string hosName = string.Empty;
            string strSql = @"select hos_name from com_hospitalinfo where rownum =1 ";
            this.ExecQuery(strSql);

            while (this.Reader.Read())
            {
                hosName = string.Empty;
                hosName = this.Reader[0].ToString(); //ҽԺ����
            }
            return hosName;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int UpdatePatient(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            #region "�ӿ�˵��"

            //			/�ӿ����� RADT.InPatient.UpdatePatient.1
            //			/max 66
            //					<!-- 0  --סԺ��ˮ��,1 --���� 2   --סԺ��   ,3   --���￨��  ,4   --ҽ��֤��
            //			    ,5     --ҽ�����,   ,6   --�Ա�   ,7   --���֤��  ,8   --ƴ��     ,9   --����
            //			    ,10   --ְҵ����     ,11   --������λ    ,12   --������λ�绰      ,13   --��λ�ʱ�
            //			    ,14   --���ڻ��ͥ��ַ     ,15   --��ͥ�绰   ,16   --���ڻ��ͥ�ʱ�   , 17  --����name
            //			    ,18   --�����ش���        ,19   --����id    ,20  --����name    ,21   --��ϵ������
            //			    ,22   --��ϵ�˵绰       ,23   --��ϵ�˵�ַ     ,24   --��ϵ�˹�ϵid , 25   --��ϵ�˹�ϵid 
            //			    ,26   --����״��id              ,27  --����״��name  ,28   --����id     29 --��������
            //			    ,30   --���           ,31   --����         ,32   -- ְλid    ,33   --ABOѪ��
            //			    ,34   --�ش󼲲���־    ,35   --������־            
            //			    ,36   --��Ժ����      ,37   --���Ҵ���   , 38  --��������  , 39  --�������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
            //			    ,40   --�����������   , 41  --��ͬ����   , 42  --��ͬ��λ����  , 43  --����
            //			   , 44 --����Ԫ����  , 45  --����Ԫ����, 46 --ҽʦ����(סԺ), 47 --ҽʦ����(סԺ)
            //			   , 48 --ҽʦ����(����) , 49 --ҽʦ����(����) , 50 --ҽʦ����(����) , 51 --ҽʦ����(����)
            //			   , 52 --ҽʦ����(ʵϰ) , 53 --ҽʦ����(ʵϰ), 54  --��ʿ����(����), 55  --��ʿ����(����)
            //			   , 56  --��Ժ���id  , 57  --��Ժ���name   , 58  --��Ժ;��id    , 59  --��Ժ;��name      
            //			   , 60  --��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ    , 61  --��Ժ��Դname
            //			   , 62  --סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
            //			  ,  63  --��Ժ����(ԤԼ)  , 64  --��Ժ���� , 65  --�Ƿ���ICU 0 no 1 yes ,66 ����Ա -->

            #endregion

            string strSql = string.Empty;
            if (Sql.GetSql("Case.BaseDML.UpdatePatient.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[22];
                try
                {
                    s[0] = PatientInfo.ID; // --סԺ��ˮ��
                    s[1] = PatientInfo.Name; //--����
                    s[2] = PatientInfo.Sex.ID.ToString(); //  --�Ա�
                    s[3] = PatientInfo.IDCard; //  --���֤��
                    s[4] = PatientInfo.Birthday.ToString(); //  --����
                    s[5] = PatientInfo.Profession.ID; //  --ְҵ 
                    s[6] = PatientInfo.CompanyName; //  --������λ
                    s[7] = PatientInfo.PhoneBusiness; //  --������λ�绰
                    s[8] = PatientInfo.BusinessZip; //  --��λ�ʱ�
                    s[9] = PatientInfo.AddressHome; //  --���ڻ��ͥ��ַ
                    s[10] = PatientInfo.PhoneHome; //  --��ͥ�绰
                    s[11] = PatientInfo.HomeZip; //  --���ڻ��ͥ�ʱ�
                    s[12] = PatientInfo.DIST; // --����name
                    s[13] = PatientInfo.Nationality.ID; //  --����id
                    s[14] = PatientInfo.Kin.Name; //  --��ϵ������
                    s[15] = PatientInfo.Kin.RelationPhone; //  --��ϵ�˵绰
                    s[16] = PatientInfo.Kin.RelationAddress; //  --��ϵ�˵�ַ
                    s[17] = PatientInfo.Kin.Relation.ID; //  --��ϵ�˹�ϵid
                    s[18] = PatientInfo.MaritalStatus.ID.ToString(); //  --����״��id
                    s[19] = PatientInfo.Country.ID; //  --����id
                    s[20] = PatientInfo.BloodType.ID.ToString(); //  --ABOѪ��
                    s[21] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --������־
                    strSql = string.Format(strSql, s);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            int parm = ExecNoQuery(strSql);
            if (parm != 1)
            {
                return parm;
            }
            return 1;
        }
        /// <summary>
        /// ���»�����Ϣ�����ǻ�������  ������com_patientinfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int UpdatePatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("Case.BaseDML.UpdatePatientInfo.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[22];
                try
                {
                    s[0] = PatientInfo.PID.CardNO; //���￨��
                    s[1] = PatientInfo.Name; //����
                    s[2] = PatientInfo.Sex.ID.ToString(); //�Ա�
                    s[3] = PatientInfo.IDCard; //  --���֤��
                    s[4] = PatientInfo.Birthday.ToString(); //  --����
                    s[5] = PatientInfo.Profession.ID; //  --ְҵ 
                    s[6] = PatientInfo.CompanyName; //  --������λ
                    s[7] = PatientInfo.PhoneBusiness; //  --������λ�绰
                    s[8] = PatientInfo.BusinessZip; //  --��λ�ʱ�
                    s[9] = PatientInfo.AddressHome; //  --���ڻ��ͥ��ַ
                    s[10] = PatientInfo.PhoneHome; //  --��ͥ�绰
                    s[11] = PatientInfo.HomeZip; //  --���ڻ��ͥ�ʱ�
                    s[12] = PatientInfo.DIST; // --����name
                    s[13] = PatientInfo.Nationality.ID; //  --����id
                    s[14] = PatientInfo.Kin.Name; //  --��ϵ������
                    s[15] = PatientInfo.Kin.RelationPhone; //  --��ϵ�˵绰
                    s[16] = PatientInfo.Kin.RelationAddress; //  --��ϵ�˵�ַ
                    s[17] = PatientInfo.Kin.Relation.ID; //  --��ϵ�˹�ϵid
                    s[18] = PatientInfo.MaritalStatus.ID.ToString(); //  --����״��id
                    s[19] = PatientInfo.Country.ID; //  --����id
                    s[20] = PatientInfo.BloodType.ID.ToString(); //  --ABOѪ��
                    s[21] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --������־
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    return -1;
                }
                return ExecNoQuery(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }


        }
        /// <summary>
        /// ��ȡ�����ύ��Ϣ
        /// ���Ʊ༭״̬ �ύ������༭
        /// </summary>
        /// <param name="InPatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public int GetEmrQcCommit(string inpatientNO)
        {
            int iReturn = 0;
            string strSql = @"select count(1) from emr_qc_commit  q
where q.inpatient_no='{0}' and q.status in ('1','2','3')  ";
            try
            {
                strSql = string.Format(strSql, inpatientNO);
                //��ѯ
                if (this.ExecQuery(strSql) < 0)
                {
                    this.Err = "ִ��sqlʧ��!";
                    return -1;
                }
                this.Reader.Read();
                iReturn = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return -1;
            }
            return iReturn;
        }

        /// <summary>
        /// ȡ������
        /// chengym 2012-11-19 �ܲ����µ��Ӳ����������㷨
        /// </summary>
        /// <param name="birthday">��������</param>
        /// <param name="endDate">��������</param>
        /// <param name="age">����(������title)</param>
        /// <returns></returns>
        public string EmrGetAge(DateTime birthday, DateTime endDate, ref int age)
        {
            string result = "";

            int diffYear = endDate.Year - birthday.Year;
            int diffMonth = endDate.Month - birthday.Month;
            int diffDay = endDate.Day - birthday.Day;

            //δ�����յ�ʱ�������1��
            if (diffYear > 0)
            {
                if (diffMonth < 0)
                {
                    diffYear = diffYear - 1;
                    diffMonth = diffMonth + 12;
                }
                else if (diffMonth == 0 && diffDay < 0)
                {
                    diffYear = diffYear - 1;
                    diffMonth = 11;
                }
            }

            //һ������
            if (diffYear == 0)
            {
                TimeSpan ts = endDate - birthday;
                int diffDayReal = ts.Days;
                //����㷨������  ���´���һ���µ�û�а����� ��5��28�ճ��� ��ǰʱ��7��4�ձ���� ��ʾ N��N���� ��Ϊ���30��İ����� Ӧ�ÿ��Խ���

                //һ������,��һ���¼����ϣ� 2012-6-2 ֣ѫmodify,����Ҫ����0,���5��28�ճ�������ǰ����Ϊ6��2�գ���ʾΪ1���µ�Bug
                if (diffMonth >= 1 && diffDay >= 0) //diffDayReal>=30
                {
                    result = diffMonth.ToString() + " ��";
                }
                //����һ�£�һ�����ϣ�Ҫ�����㼸��
                //2012-6-2 ֣ѫ diffDay���ܲ�����㣬��Ϊ������2��������������
                else if (diffDayReal > 7)//(diffDay > 7)
                {
                    //result = (diffDay / 7).ToString() + " ��" + (diffDay % 7).ToString() + " ��";
                    result = (diffDayReal / 7).ToString() + " ��" + (diffDayReal % 7).ToString() + " ��";
                }
                else
                {
                    //result = diffDay.ToString() + " ��";
                    result = diffDayReal.ToString() + " ��";
                }
            }
            //�������£�һ������Ҫ��ȷ�������㼸����
            else if (diffYear > 0 && diffYear < 3)
            {
                result = diffYear.ToString() + " ��" + diffMonth.ToString() + " ��";
            }
            //��������
            else
            {
                result = diffYear.ToString() + " ��";
            }
            age = diffYear;
            return result;
        }

        #region �㶫ʡ�����ϴ�
        /// <summary>
        /// �Ӳ����������л�ȡ��Ϣ --�����ϴ���2010-4-29
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns></returns>
        public ArrayList GetCaseBaseInfo(DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            if (strSQL == null)
            {
                return null;
            }
            string str = @"    where out_date>=to_date('{0}','yyyy-mm-dd HH24:mi:ss')  and out_date<=to_date('{1}','yyyy-mm-dd HH24:mi:ss') ";

            strSQL += str;
            strSQL = string.Format(strSQL, dtBegin, dtEnd);
            return this.myGetCaseBaseInfo(strSQL);
        }

        /// <summary>
        /// ���²����ϴ���־��˳�¸��ײ����ӿ���ʹ��
        /// </summary>
        /// <param name="patientNo">סԺ��ˮ��</param>
        /// <returns></returns>
        public int UpdateBaseUploadFlat(string patientNo)
        {
            string strSql = "update  met_cas_base  set x_numb='2'  where inpatient_no='{0}'";

            try
            {
                strSql = string.Format(strSql, patientNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���Ӳ����鵵����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="caseUpdateStus">��</param>
        /// <param name="sendStus">����״̬</param>
        /// <param name="sendFlow">����״̬</param>
        /// <returns></returns>
        public int UpdateBaseCaseStus(string inpatientNO, string caseUpdateStus, string sendStus, string sendFlow)
        {
            string strSQL = "";

            if (Sql.GetSql("CASE.BaseDML.UpdateCaseStus.Update", ref strSQL) == 0)
            {
                try
                {
                    strSQL = string.Format(strSQL, inpatientNO, caseUpdateStus, sendStus, sendFlow);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;

                    return -1;
                }
            }

            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ��ѯԺ�д���
        /// </summary>
        /// <param name="inpatien_NO">סԺ��ˮ��</param>
        /// <returns></returns>
        public int QueryInfCount(string inpatien_NO)
        {
            string strSql = "";
            int iReturn = 0;
            if (this.Sql.GetSql("CASE.BaseDML.GetInfectionReport", ref strSql) == -1)
            {
                this.Err = "��ȡCASE.BaseDML.GetInfectionReport����!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, inpatien_NO);
                //��ѯ
                if (this.ExecQuery(strSql) < 0)
                {
                    this.Err = "ִ��sqlʧ��!";
                    return -1;
                }
                this.Reader.Read();
                iReturn = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                this.Reader.Close();

            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                this.Reader.Close();
                return -1;
            }
            return iReturn;
        }

        /// <summary>
        /// �ϴ�ʱ�����������ʱͬʱ���²�������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        public int UpdateMetCasBaseTimes(string inpatientNo, int inTimes)
        {
            //��ȡSql���
            string strSql = @"UPDATE MET_CAS_BASE SET IN_TIMES = {1} WHERE INPATIENT_NO = '{0}'";
            try
            {
                #region ��ʽ��SQL
                strSql = string.Format(strSql, inpatientNo, inTimes.ToString());
                #endregion

            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �Ӳ����������л�ȡ��Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Base GetCaseBaseInfoFromEmrView(string inpatientNO)
        {
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            strSQL.Replace("met_cas_base", "view_met_cas_base");
            if (strSQL == null)
            {
                return null;
            }
            string str = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfo.Select.where", ref str) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, inpatientNO);
            ArrayList arrList = this.myGetCaseBaseInfo(strSQL);
            if (arrList == null)
            {
                return null;
            }
            if (arrList.Count > 0)
            {
                info = (FS.HISFC.Models.HealthRecord.Base)arrList[0];
            }
            return info;
        }
        /// <summary>
        /// ��ѯסԺ�������ϴ�״̬ casesend_flag
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public string GetCaseUploadState(string inpatientNO)
        {
            string UploadState = string.Empty;
            string strSql = @"select t.casesend_flag from fin_ipr_inmaininfo t where t.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNO);
            }
            catch
            {
            }
            this.ExecQuery(strSql);

            while (this.Reader.Read())
            {
                UploadState = string.Empty;
                UploadState = this.Reader[0].ToString(); //�����ϴ�״̬
            }
            return UploadState;
        }
        /// <summary>
        /// ����סԺ�š���Ժ�����ж��Ƿ����֮ǰ����δ�ϴ�������
        /// ����ֵ�� 0 ��Ҫ�ϴ� 1 �Ѿ��ϴ�
        /// </summary>
        /// <param name="patient_no">סԺ��</param>
        /// <param name="out_date">��Ժ����</param>
        /// <returns></returns>
        public ArrayList GetIsHavedNoUpload(string patient_no, DateTime out_date)
        {
            int iReturn = 0;
            string strSql = @"select t.inpatient_no,t.patient_no,t.in_date from fin_ipr_inmaininfo t where t.patient_no= '{0}'
and t.out_date < to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and (t.in_state ='B' or t.in_state='O')
and t.patient_no like '0%'
and t.casesend_flag='0'";
            ArrayList al = new ArrayList();
            try
            {
                strSql = string.Format(strSql, patient_no, out_date.Date.ToString("yyyy-MM-dd")+" 00:00:00");
                //��ѯ
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = Reader[0].ToString();
                    patientInfo.PID.PatientNO = Reader[1].ToString();
                    patientInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[2].ToString());
                    al.Add(patientInfo);
                }
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return al;
        }
        #endregion
        #region  ��ݸ��ͷ���ӵķ���
        /// <summary>
        /// ���²��������������Ӧ���ֶ�
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdateMainInfo(FS.HISFC.Models.HealthRecord.Base patient)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("CASE.BaseDML.UpdateMainBaseInfo.Update.HIS50", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[30];
                try
                {

                    s[0] = patient.PatientInfo.ID;//סԺ��ˮ��
                    s[1] = patient.PatientInfo.PID.PatientNO;//סԺ������
                    s[2] = patient.PatientInfo.PID.CardNO;//����
                    s[3] = patient.PatientInfo.Name;//����
                    s[4] = patient.PatientInfo.Sex.ID.ToString();//�Ա�
                    s[5] = patient.PatientInfo.Birthday.ToString();//��������
                    s[6] = patient.PatientInfo.Country.ID;//����
                    s[7] = patient.PatientInfo.Nationality.ID;//����
                    s[8] = patient.PatientInfo.Profession.ID;//ְҵ
                    s[9] = patient.PatientInfo.MaritalStatus.ID.ToString();//���
                    s[10] = patient.PatientInfo.IDCard;//���֤��
                    s[11] = patient.InAvenue;//������Դ
                    s[12] = patient.PatientInfo.Pact.PayKind.ID;//��������
                    s[13] = patient.PatientInfo.Pact.ID;//��ͬ����
                    s[14] = patient.PatientInfo.SSN;//ҽ�����Ѻ�
                    s[15] = patient.PatientInfo.DIST;//����
                    s[16] = patient.PatientInfo.AreaCode;//������
                    s[17] = patient.PatientInfo.AddressHome;//��ͥסַ
                    s[18] = patient.PatientInfo.PhoneHome;//��ͥ�绰
                    s[19] = patient.PatientInfo.HomeZip;//סַ�ʱ�
                    s[20] = patient.PatientInfo.AddressBusiness;//��λ��ַ
                    s[21] = patient.PatientInfo.PhoneBusiness;//��λ�绰
                    s[22] = patient.PatientInfo.BusinessZip;//��λ�ʱ�
                    s[23] = patient.PatientInfo.Kin.Name;//��ϵ��
                    s[24] = patient.PatientInfo.Kin.RelationLink;//�뻼�߹�ϵ
                    s[25] = patient.PatientInfo.Kin.RelationPhone;//��ϵ�绰                   
                    s[26] = patient.PatientInfo.PVisit.InTime.ToString();//��Ժ����
                    s[27] = patient.PatientInfo.InTimes.ToString();//��Ժ����
                    s[28] = patient.InAvenue;//��Ժ��Դ��;����
                    s[29] = patient.PatientInfo.PVisit.Circs.ID;//��Ժ״̬
                }
                catch (Exception ex)
                {
                    Err = "��ֵʱ�����" + ex.Message;
                    WriteErr();
                    return -1;
                }
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "��ֵʱ�����" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);

        }

        #endregion
        #region  ����
        /// <summary>
        /// ����סԺ�ź�סԺ������ѯסԺ��ˮ�� 
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="InNum"></param>
        /// <returns></returns>
        [Obsolete(" ����,��QueryPatientInfoByInpatientAndInNum ����", true)]
        public ArrayList GetPatientInfo(string inpatientNO, string InNum)
        {
            //�ȴӲ��������в�ѯ ���û�в鵽 ����סԺ�����в�ѯ 
            ArrayList list = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetPatientInfo.GetPatientInfo", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, inpatientNO, InNum);
            this.ExecQuery(strSql);
            FS.HISFC.Models.RADT.PatientInfo info = null;
            while (this.Reader.Read())
            {
                info = new FS.HISFC.Models.RADT.PatientInfo();
                info.ID = this.Reader[0].ToString();
                list.Add(info);
                info = null;
            }
            if (list == null)
            {
                return list;
            }
            if (list.Count == 0)
            {
                //��ѯסԺ���� ��ȡ������Ϣ
                if (this.Sql.GetSql("RADT.Inpatient.PatientInfoGetByTime", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return null;
                }
                strSql = string.Format(strSql, inpatientNO, InNum);
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.RADT.PatientInfo();
                    info.ID = this.Reader[0].ToString();
                    list.Add(info);
                    info = null;
                }
            }
            return list;
        }
        /// <summary>
        /// ��ѯδ�Ǽǲ�����Ϣ�Ļ��ߵ������Ϣ,��met_com_diagnose����ȡ
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <param name="diagType">������,Ҫ��ȡ�����������%</param>
        /// <returns>�����Ϣ����</returns>
        [Obsolete("����,�� QueryInhosDiagnoseInfo ����", true)]
        public ArrayList GetInhosDiagInfo(string inpatientNO, string diagType)
        {
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetInhosDiagInfo.Select", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSql = string.Format(strSql, inpatientNO, diagType);

            return this.myGetDiagInfo(strSql);
        }
        /// <summary>
        /// ���ݲ����Ż�ȡ��Ϣ
        /// </summary>
        /// <param name="CaseNo"></param>
        /// <returns></returns
        [Obsolete("���� �� QueryCaseBaseInfoByCaseNO ����", true)]
        public ArrayList GetCaseBaseInfoByCaseNum(string CaseNo)
        {
            ArrayList list = new ArrayList();
            //��ȡ��sql���
            string strSQL = GetCaseSql();
            string str = "";
            if (this.Sql.GetSql("CASE.BaseDML.GetCaseBaseInfoByCaseNum.Select.where", ref str) == -1)
            {
                this.Err = "��ȡSQL���ʧ��";
                return null;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, CaseNo);
            return this.myGetCaseBaseInfo(strSQL);
        }
        /// <summary>
        /// ������� 
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������� ��ͬ��λ ����", true)]
        public ArrayList GetPayKindCode()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "01";
            //info.Name = "�Է�";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "02";
            //info.Name = "ҽ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "03";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "04";
            //info.Name = "��Լ��λ";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "05";
            //info.Name = "��Ժְ��";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// Ѫ���б�
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������� ���� BLOODTYPE ����", true)]
        public ArrayList GetBloodType()
        {
            //Ѫ���б� 
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "0";
            //info.Name = "U";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "A";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "B";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "AB";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "4";
            //info.Name = "O";
            //list.Add(info);

            return list;
            #region  סԺ���õ��б�
            //			ArrayList list = new ArrayList();
            //			FS.HISFC.Object.Base.SpellCode info = null;
            //			ArrayList list2 = FS.HISFC.Models.RADT.BloodType.List();
            //			foreach(FS.FrameWork.Models.NeuObject obj in list2)
            //			{
            //				info = new FS.HISFC.Object.Base.SpellCode();
            //				info.ID = obj.ID;
            //				info.Name = obj.Name;
            //				list.Add(info);
            //			}
            #endregion
        }
        /// <summary>
        /// ��Ѫ��Ӧ
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������ó��� BloodReaction ����", true)]
        public ArrayList GetReactionBlood()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��";
            //list.Add(info);

            return list;
        }
        [Obsolete("�������� ���� RHSTATE ����", true)]
        public ArrayList GetRHType()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "��";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������� ���� CASEQUALITY ����", true)]
        public ArrayList GetCaseQC()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "0";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "��";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ��Ϸ������
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������ó��� DIAGNOSEACCORD ����", true)]
        public ArrayList GetDiagAccord()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "0";
            //info.Name = "δ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "������";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "���϶�";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ҩ�����
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������ó��� PHARMACYALLERGIC ����", true)]
        public ArrayList GetHbsagList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "0";
            //info.Name = "δ��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "����";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ������Դ  
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������� ���� INSOURCE ����", true)]
        public ArrayList GetPatientSource()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "����";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "4";
            //info.Name = "��ʡ";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "5";
            //info.Name = "�۰�̨";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "6";
            //info.Name = "���";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// ��ȡ�Ա��б�
        /// </summary>
        /// <returns></returns>
        [Obsolete("��������ö�� ����", true)]
        public ArrayList GetSexList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "M";
            //info.Name = "��";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "F";
            //info.Name = "Ů";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "U";
            //info.Name = "δ֪";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "O";
            //info.Name = "����";
            //list.Add(info);

            return list;
        }
        /// <summary>
        /// �����б�
        /// </summary>
        /// <returns></returns>
        [Obsolete("��������ö�ٴ���", true)]
        public ArrayList GetMaryList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = null;
            //ArrayList list2 = FS.HISFC.Models.RADT.MaritalStatus.List();
            //foreach (FS.FrameWork.Models.NeuObject obj in list2)
            //{
            //    info = new FS.HISFC.Object.Base.SpellCode();
            //    info.ID = obj.ID;
            //    info.Name = obj.Name;
            //    list.Add(info);
            //}
            return list;
        }
        /// <summary>
        /// ����met_cas_base��������� ��Ժ��� ��Ժ��Ҫ��� ��һ����
        /// </summary>
        /// <param name="inpatienNO"></param>
        /// <param name="frmType"></param>
        /// <returns></returns>
        [Obsolete("����,��UpdateBaseDiagAndOperationNew����", true)]
        public int UpdateBaseDiagAndOperation(string inpatienNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {

            FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.HealthRecord.Operation op = new Operation();
            if (this.Trans != null)
            {
                dia.SetTrans(Trans);
                op.SetTrans(Trans);
            }
            FS.HISFC.Models.HealthRecord.Diagnose ClinicDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.CLINIC, frmType);
            FS.HISFC.Models.HealthRecord.Diagnose InhosDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.IN, frmType);
            FS.HISFC.Models.HealthRecord.Diagnose OutDiag = dia.GetFirstDiagnose(inpatienNO, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OUT, frmType);
            FS.HISFC.Models.HealthRecord.OperationDetail ops = op.GetFirstOperation(inpatienNO, frmType);
            if (ClinicDiag == null || InhosDiag == null || OutDiag == null || ops == null)
            {
                return -1;
            }
            string[] str = new string[14];
            str[0] = inpatienNO;
            str[1] = ClinicDiag.DiagInfo.ICD10.ID;
            str[2] = ClinicDiag.DiagInfo.ICD10.Name;
            str[3] = InhosDiag.DiagInfo.ICD10.ID;
            str[4] = InhosDiag.DiagInfo.ICD10.Name;
            str[5] = OutDiag.DiagInfo.ICD10.ID;
            str[6] = OutDiag.DiagInfo.ICD10.Name;
            str[7] = OutDiag.DiagOutState;
            str[8] = OutDiag.CLPA;
            str[9] = ops.OperationInfo.ID;
            str[10] = ops.OperationInfo.Name;
            str[11] = ops.FirDoctInfo.ID;
            str[12] = ops.FirDoctInfo.Name;
            str[13] = ops.OperationDate.ToString();
            string strSql = "";
            if (this.Sql.GetSql("CASE.BaseDML.UpdateBaseDiagAndOperation", ref strSql) == -1) return -1;

            strSql = string.Format(strSql, str);
            return this.ExecNoQuery(strSql);

        }
        #endregion
    }
}
