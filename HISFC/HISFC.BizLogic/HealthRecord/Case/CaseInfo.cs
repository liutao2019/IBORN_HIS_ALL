using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.HealthRecord;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Case
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: ���߲���������Ϣά��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-21]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='��ȫ'
    ///		�޸�ʱ��='2007-09-10'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class CaseInfo : FS.FrameWork.Management.Database
    {
        #region ���ݿ��������

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="CaseInfo">������</param>
        /// <returns>�����쳣���أ�1 �ɹ�����1 ����ʧ�ܷ��� 0</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo)
        {

            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseInfo.Insert", ref strSql) == -1) return -1;
            try
            {
                //����
                strSql = string.Format(strSql, GetInfo(caseInfo));

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

        }

        private string[] GetInfo(FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo)
        {
            string[] str = new string[13];

            str[0] = caseInfo.ID;         //������ˮ�� 
            str[1] = caseInfo.Patient.PID.CardNO; //��ǰ������
            str[2] = caseInfo.Patient.User01;     //���ﲡ����
            str[3] = caseInfo.Patient.User02;     //���ﲡ����
            str[4] = caseInfo.Patient.User03;     //סԺ������
            str[5] = caseInfo.Cabinet.ID; //���������
            str[6] = caseInfo.GridNO;     //��������
            str[7] = caseInfo.CaseState.ID;      //����״̬���룬��Ӧ������CASE02
            str[8] = caseInfo.LoseState.ID;      //��ʧԭ����룬��Ӧ������CASE03
            str[9] = caseInfo.InType.ToString(); //�������ͣ�0���ˡ�1������
            str[10] = caseInfo.InEmployee.ID;    //������Ա����
            str[11] = caseInfo.InDept.ID;        //���ڿ��ұ���
            str[12] = caseInfo.EmpiID;           //������������

            return str;
        }

        /// <summary>
        /// ���²�����¼
        /// </summary>
        /// <param name="caseInfo">������</param>
        /// <returns>Ӱ�������-�ɹ�;-1-�쳣��0ʧ��</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo)
        {
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseInfo.Update", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = GetInfo(caseInfo);
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;

                return -1;
            }

            //��ִ��SQL��䷵��
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="empiID"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public int CreatePatientInfo(string cardNO, string empiID, ref string caseID)
        {
            FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo = new FS.HISFC.Models.HealthRecord.Case.CaseInfo();

            caseInfo.ID = this.GetSequence("HealthRecord.Case.CaseID"); //������ˮ��
            caseInfo.Patient.PID.CardNO = cardNO; //��ǰ������
            caseInfo.Patient.User01 = cardNO;     //���ﲡ����
            caseInfo.EmpiID = empiID;     //������������

            caseID = caseInfo.ID; //�������ò�����ˮ��

            return this.Insert(caseInfo);
        }

        /// <summary>
        /// ��ȡ���в�����Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllCaseInfo()
        {
            ArrayList arrayList = null;
            string strSql = "";

            if (this.Sql.GetSql("HealthReacord.Case.CaseInfo.SelectAll", ref strSql) == -1) return null;

            try
            {
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo = null;
                arrayList = new ArrayList();
                while (this.Reader.Read())
                {
                    caseInfo = new FS.HISFC.Models.HealthRecord.Case.CaseInfo();
                    caseInfo.ID = this.Reader[0].ToString(); //������ˮ��
                    caseInfo.Patient.PID.CardNO = this.Reader[1].ToString();  //��ǰ������
                    caseInfo.Patient.User01 = this.Reader[2].ToString();  //���ﲡ����
                    caseInfo.Patient.User02 = this.Reader[3].ToString();  //���ﲡ����
                    caseInfo.Patient.User03 = this.Reader[4].ToString();  //סԺ������
                    caseInfo.Patient.Name = this.Reader[5].ToString();    //��������
                    caseInfo.Patient.Sex.ID = this.Reader[6].ToString();  //�����Ա�
                    caseInfo.Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7]); //���߳�������
                    caseInfo.Patient.IDCard = this.Reader[8].ToString();  //�������֤��
                    caseInfo.Cabinet.ID = this.Reader[9].ToString();  //���������
                    caseInfo.GridNO = this.Reader[10].ToString();  //��������
                    caseInfo.CaseState.ID = this.Reader[11].ToString();  //����״̬
                    caseInfo.LoseState.ID = this.Reader[12].ToString();  //��ʧԭ��
                    caseInfo.InType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13]);  //������������
                    caseInfo.InEmployee.ID = this.Reader[14].ToString();  //������Ա���
                    caseInfo.InDept.ID = this.Reader[15].ToString();   //���ڿ��ұ��
                    caseInfo.EmpiID = this.Reader[16].ToString();  //������������

                    arrayList.Add(caseInfo);
                }

                return arrayList;
            }
            catch(Exception Ex)
            {
                this.Err = Ex.Message;
                return null;
            }
        }

        #endregion


        #region ��ѯ

        /// <summary>
        ///�����ݲ��������ѯ��������Ϣ
        /// </summary>
        /// <param name="">������ˮ��</param>
        /// <returns>��Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Case.CaseInfo</returns>  
        public ArrayList Query(string caseID)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("HealthReacord.Case.CaseInfo.Select", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, caseID);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    caseInfo = new FS.HISFC.Models.HealthRecord.Case.CaseInfo();
                    caseInfo.ID = this.Reader[0].ToString();           //������ˮ�� 
                    //caseInfo..PatientNO = this.Reader[1].ToString(); //��ǰ������
                    //caseInfo.PID.ID = this.Reader[2].ToString();      //���ﲡ����
                    //caseInfo.PID.CardNO = this.Reader[3].ToString();  //���ﲡ����
                    //caseInfo.PID.CaseNO = this.Reader[4].ToString();//סԺ������
                    caseInfo.Cabinet.ID = this.Reader[5].ToString(); //���������
                    caseInfo.Cabinet.GridCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString()); //��������
                    caseInfo.CaseState.ID = this.Reader[7].ToString(); //����״̬���룬��Ӧ������CASE02
                    caseInfo.LoseState.ID = this.Reader[8].ToString(); //��ʧԭ����룬��Ӧ������CASE03
                    caseInfo.InType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9].ToString());//�������ͣ�0���ˡ�1������
                    caseInfo.InEmployee.ID = this.Reader[10].ToString(); //������Ա����
                    caseInfo.InDept.ID = this.Reader[11].ToString();   //���ڿ��ұ���
                    List.Add(caseInfo);
                    caseInfo = null;
                }

                return List;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���ݲ����Ż�ȡ����������Ϣ
        /// </summary>
        /// <param name="caseInfo">����������Ϣ</param>
        /// <param name="cardNo">������</param>
        /// <returns>��1��ʧ�ܣ�0�������ڣ�1���ɹ� </returns>
        public int GetCaseInfoByCardNo(ref FS.HISFC.Models.HealthRecord.Case.CaseInfo caseInfo, string cardNo)
        {
            string selectSql = string.Empty;

            int callReturn = this.Sql.GetSql("FS.HISFC.Management.HealthRecord.Case.CaseInfo.GetCaseInfoByCardNo", ref selectSql);
            if (callReturn == -1)
            {
                return -1;
            }

            try
            {
                selectSql = string.Format(selectSql, cardNo);
                callReturn = this.ExecQuery(selectSql);
                if (callReturn == -1)
                {
                    this.Err = "���ݲ����Ż�ȡ����������Ϣʧ��" + this.Err;

                    return -1;
                }

                if (this.Reader.Read())
                {
                    caseInfo.ID = this.Reader[0].ToString();
                    caseInfo.CaseState.Name = this.Reader[1].ToString();
                    // ��������
                    caseInfo.InType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    caseInfo.InDept.Name = this.Reader[3].ToString();
                    caseInfo.InEmployee.Name = this.Reader[4].ToString();
                    caseInfo.Patient.Name = this.Reader[5].ToString();
                    caseInfo.Patient.Sex.ID = this.Reader[6].ToString();
                    caseInfo.Patient.Birthday = Convert.ToDateTime(this.Reader[7].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception exception)
            {
                this.Err = "���ݲ����Ż�ȡ����������Ϣʧ��" + exception.Message;

                return -1;
            }

            return 1;
        }
        #endregion
    }
}
