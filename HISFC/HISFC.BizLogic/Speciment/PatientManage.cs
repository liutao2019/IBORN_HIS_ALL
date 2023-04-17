using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;
using FS.HISFC.Models.HealthRecord;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ���˻�����Ϣ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-22]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class PatientManage : FS.FrameWork.Management.Database
    {
        private PatientInfo patientInfo = new PatientInfo();
        private SpecPatient specPatient = new SpecPatient();

        #region HIS patient ת�� �� Spec Patient
        /// <summary>
        /// HIS patient ת�� �� Spec Patient
        /// </summary>
        /// <returns></returns>
        private void PatientInfoToPa()
        {        
            //��ͥסַ
            specPatient.Address = patientInfo.AddressHome;
            //����
            specPatient.Home = patientInfo.DIST;
            //��ϵ�绰
            specPatient.ContactNum = patientInfo.Kin.RelationPhone;
            //�Ա�
            specPatient.Gender = Convert.ToChar(patientInfo.Sex.ID);
            //��ͥ�绰
            specPatient.HomePhoneNum = patientInfo.PhoneHome;
            //���֤��
            specPatient.IdCardNo = patientInfo.IDCard;
            specPatient.IcCardNo = patientInfo.PID.CardNO;
            specPatient.CardNo = patientInfo.PID.CaseNO;
            //����
            specPatient.Nation = patientInfo.Nationality.Name;
            //����
            specPatient.Nationality = patientInfo.Country.Name;
            //����
            specPatient.Birthday = Convert.ToDateTime(patientInfo.Birthday);
            //����
            specPatient.PatientName = patientInfo.Name;
            //Ѫ��
            specPatient.BloodType = patientInfo.BloodType.ID.ToString();
            specPatient.IsMarried = patientInfo.MaritalStatus.ID.ToString();

        }
        #endregion

        /// <summary>
        ///  ��His����ȡ�Ĳ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private SpecPatient SetSpecPatient()
        {
            SpecPatient patient = new SpecPatient();
            patient.IcCardNo = Reader.IsDBNull(0)? "" : Reader["IC_CARDNO"].ToString();
            patient.CardNo = Reader.IsDBNull(1)? "" : Reader["CARD_NO"].ToString();
            patient.InHosNum = Reader.IsDBNull(2)? "" : Reader["INPATIENT_NO"].ToString();
            patient.PatientName = Reader.IsDBNull(3) ? "" : Reader["NAME"].ToString();
            patient.IdCardNo = Reader.IsDBNull(4)  ? "" : Reader["IDENNO"].ToString();
            patient.Gender = Reader.IsDBNull(5)  ? ' ' : Convert.ToChar(Reader["SEX_CODE"].ToString());
            patient.Birthday = Reader.IsDBNull(6) ? DateTime.MinValue : Convert.ToDateTime(Reader["BIRTHDAY"].ToString());// ("yyyy-MM-dd");
            patient.Address = Reader.IsDBNull(7)  ? "" : Reader["HOME"].ToString();
            patient.Home = Reader.IsDBNull(8)  ? "" : Reader["DISTRICT"].ToString();
            patient.Nation= Reader.IsDBNull(9)  ? "" : Reader["NATION_CODE"].ToString();
            patient.Nationality = Reader.IsDBNull(10)  ? "" : Reader["COUN_CODE"].ToString();
            patient.ContactNum = Reader.IsDBNull(11)  ? "" : Reader["LINKMAN_TEL"].ToString();
            patient.HomePhoneNum = Reader.IsDBNull(12)  ? "" : Reader["HOME_TEL"].ToString();
            patient.BloodType = Reader.IsDBNull(13)  ? "" : Reader["BLOOD_CODE"].ToString();
            patient.IsMarried = Reader.IsDBNull(14)  ? "" : Reader["MARI"].ToString();
            return patient;
        }

        #region ���ò�������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="patient">�걾�ⲡ��ʵ��</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecPatient patient)
        {
            string[] str = new string[]
						{
							patient.PatientID.ToString(),
                            patient.PatientName,
                            patient.IcCardNo,
                            patient.CardNo,
                            patient.ContactNum,
                            patient.HomePhoneNum,
                            patient.Nationality,
                            patient.Nation,
                            patient.IdCardNo,
                            patient.Gender.ToString(),
                            patient.BloodType,
                            patient.Home,
                            patient.Address,
                            patient.IsMarried,
                            patient.Comment,
                            patient.Birthday.ToString()
                        };
            return str;
        }
        #endregion

        /// <summary>
        /// �ӱ걾������ȡ�Ĳ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private SpecPatient SetPatient()
        {
            SpecPatient specPatient = new SpecPatient();
            specPatient.PatientID = Convert.ToInt32(this.Reader["PATIENTID"].ToString());
            specPatient.PatientName = this.Reader["PNAME"].ToString();
            specPatient.IcCardNo = this.Reader["IC_CARDNO"].ToString();
            specPatient.CardNo = this.Reader["CARD_NO"].ToString();
            specPatient.ContactNum = this.Reader["CONTACTNUM"].ToString();
            specPatient.HomePhoneNum = this.Reader["HOMEPHONENUM"].ToString();
            specPatient.Nationality = this.Reader["NATIONALITY"].ToString();
            specPatient.Nation = this.Reader["NATION"].ToString();
            specPatient.IdCardNo = this.Reader["IDCARDNO"].ToString();
            specPatient.Gender = Convert.ToChar(this.Reader["GENDER"].ToString());
            specPatient.BloodType = this.Reader["BLOOD_CODE"].ToString();
            specPatient.Home = this.Reader["HOME"].ToString();
            specPatient.Address = this.Reader["ADDRESS"].ToString();
            specPatient.IsMarried = this.Reader["ISMARR"].ToString();
            specPatient.Comment = this.Reader["MARK"].ToString();
            specPatient.Birthday = Convert.ToDateTime(Reader["BIRTHDAY"].ToString());
            return specPatient;
        }


        #region ��ȡ����
        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.PatientManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        #region ���²���
        /// <summary>
        /// ���²���
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args"></param>
        /// <returns></returns>
        private int UpdatePatient(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }
        #endregion

        /// <summary>
        /// ����������ȡ�����б�
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetPatientInfo(string sqlIndex, string[] parm,string patientFrom)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            SpecPatient patient;
            ArrayList arrList = new ArrayList();
            while (this.Reader.Read())
            {
                patient = new SpecPatient();
                if (patientFrom == "H")
                {
                    patient = SetSpecPatient();
                }
                if (patientFrom == "S")
                {
                    patient = SetPatient();
                }
                arrList.Add(patient);
            }
            this.Reader.Close();
            return arrList;
        }

        #region  ����סԺ��ˮ�Ż�ȡסԺ����
        /// <summary>
        /// ����סԺ��ˮ�Ż�ȡסԺ����
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns>סԺ����</returns>
        public string GetIndeptName(string inPatientNo)
        {
            string deptName="";
            string[] parm = new string[] { inPatientNo};
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.PatientManage.GetDeptByPatientNo", ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;           
            while (this.Reader.Read())
            {
                if (!Reader.IsDBNull(0))
                    deptName =  Reader[0].ToString();
                break;
            }
            this.Reader.Close();
            return deptName;
        }
        #endregion

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion

        #region �걾�ⲡ����Ϣ����
       /// <summary>
        /// ���벡����Ϣ
        /// </summary>
        /// <param name="specPatient">�걾�ⲡ����Ϣ</param>
        /// <returns></returns>
        public int InsertPatient(SpecPatient specPatient)
        {
            try
            {
                return UpdatePatient("Speciment.BizLogic.PatientManage.Insert", GetParam(specPatient));
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }
       #endregion

        /// <summary>
        /// ���²�����Ϣ
        /// </summary>
        /// <param name="specPatient"></param>
        /// <returns></returns>
        public int UpdatePatient(SpecPatient specPatient)
        {
            try
            {
                return UpdatePatient("Speciment.BizLogic.PatientManage.Update", GetParam(specPatient));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����sql��ѯ������Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList GetPatientInfo(string sql)
        {
            if(this.ExecQuery(sql)==-1)
            {
                return null;
            }
            ArrayList arrSpecPatient = new ArrayList();
            while (this.Reader.Read())
            {
                SpecPatient patient = SetPatient();
                arrSpecPatient.Add(patient);
            }
            Reader.Close();
            return arrSpecPatient;
        }

        /// <summary>
        /// ����CardNo��ȡ������Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public SpecPatient GetPatientInfoCardNo(string cardNo)
        {
            ArrayList arr = new ArrayList();
            arr = this.GetPatientInfo("Speciment.BizLogic.PatientManage.SelectPatientByCardNo", new string[] { cardNo }, "H");
            if (arr.Count > 0)
            {
                return arr[0] as SpecPatient;
            }
            return null;
        }

        /// <summary>
        /// ����IcCardNO��ȡ������Ϣ
        /// </summary>
        /// <param name="icCardNo"></param>
        /// <returns></returns>
        public SpecPatient GetPatientInfoIcCardNo(string icCardNo)
        {
            ArrayList arr = new ArrayList();
            arr = this.GetPatientInfo("Speciment.BizLogic.PatientManage.SelectPatientByICCardNo", new string[] { icCardNo }, "H");
            if (arr.Count > 0)
            {
                return arr[0] as SpecPatient;
            }
            return null;
        }

        /// <summary>
        /// ����סԺ��ˮ�Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="inHosNum"></param>
        /// <returns></returns>
        public SpecPatient GetPatientInfoInNum(string inHosNum)
        {
            ArrayList arr = new ArrayList();
            arr = this.GetPatientInfo("Speciment.BizLogic.PatientManage.SelectPatientByInpatientNo", new string[] { inHosNum }, "H");
            if (arr.Count > 0)
            {
                return arr[0] as SpecPatient;
            }
            return null;
        }

        /// <summary>
        /// ���ݲ����Ŵ��������л�ȡ������Ϣ
        /// </summary>
        /// <param name="patNo"></param> 
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList GetPatientForOldData(string patNo, string name ,string sql , ref string val)
        {  
            if (this.ExecQuery(sql)== -1)
                return null;
            FS.FrameWork.Models.NeuObject neuo;
            ArrayList arrList = new ArrayList();
            while (this.Reader.Read())
            {
                neuo = new FS.FrameWork.Models.NeuObject();
                neuo.ID = Reader["CARD_NO"] == null ? "" : Reader["CARD_NO"].ToString();
                neuo.Name = Reader["PNAME"] == null ? "" : Reader["PNAME"].ToString();
                neuo.Memo = this.Reader["SEX_CODE"].ToString() == "M" ? "��" : "Ů";
                if (neuo.ID == patNo && neuo.Name == name)
                {
                    val = "3";
                    ArrayList tmp = new ArrayList();
                    tmp.Add(neuo);
                    return tmp;
                }
                arrList.Add(neuo);
            }
            this.Reader.Close();
            return arrList;
        }
    }
}
