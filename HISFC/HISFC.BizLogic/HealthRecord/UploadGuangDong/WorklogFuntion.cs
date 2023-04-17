using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.UploadGuangDong
{
    /// <summary>
    /// ��ȡ����ҽ��������ҵ��������ϴ�
    /// ��ҳ�������������Ӥ������
    /// </summary>
    public class WorklogFuntion : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        /// <param name="dsM">����dataset</param>
        /// <param name="Type">����������</param>
        /// <param name="fromTime">��ʼʱ��</param>
        /// <param name="toTime">����ʱ��</param>
        /// <returns></returns>
        public int GetMSpecial(ref DataSet dsM, string Type, DateTime fromTime, DateTime toTime)
        {
            string sql = string.Empty;
            string query = string.Empty;
            switch (Type)
            {
                case "tWorkLog":
                    query = "Report.DoctWordStat.WorkLogUpload";
                    break;
                case "tEmergeLog":
                    query = "Report.DoctWordStat.EmergeLogUpload";
                    break;
                case "tSpecialLog":
                    query = "Report.DoctWordStat.SpecialLogUpload";
                    break;
                case "TZyWardWorklog":
                    query = "Report.DoctWordStat.TZyWardWorklog";
                    break;
            }
            if (this.Sql.GetSql(query, ref sql) == -1)
            {
                this.Err += "GetSQlWrong!";
                return -1;
            }
            sql = string.Format(sql, fromTime, toTime);
            return this.ExecQuery(sql, ref dsM);
        }

        public string GetWorkLogUploadSql(string Type, DateTime fromTime, DateTime toTime)
        {
            #region
            string sql = string.Empty;
            string query = string.Empty;
            switch (Type)
            {
                case "tWorkLog":
                    query = "Report.DoctWordStat.WorkLogUpload";//ҽ�����﹤����־
                    break;
                case "tEmergeLog":
                    query = "Report.DoctWordStat.EmergeLogUpload";//���﹤����־
                    break;
                case "tSpecialLog":
                    query = "Report.DoctWordStat.SpecialLogUpload";//ר�����ﲡ����
                    break;
                case "TZyWardWorklog":
                    query = "Report.DoctWordStat.TZyWardWorklog";//סԺ������־
                    break;
            }
            if (this.Sql.GetSql(query, ref sql) == -1)
            {
                this.Err += "GetSQlWrong!";
                return string.Empty;
            }
            sql = string.Format(sql, fromTime, toTime);
            return sql;
            #endregion
        }

        /// <summary>
        /// ��������סԺ����
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="intimes"></param>
        /// <returns></returns>
        public int UpdatePatientInTimes(string patientNo, int intimes)
        {
            string sqlStr = "";

            if (string.IsNullOrEmpty(patientNo))
            {
                this.Err = "����סԺ��ˮ��Ϊ��!";
                return -1;
            }
            if (intimes <= 0)
            {
                this.Err = "סԺ������������";
                return -1;
            }

            if (this.Sql.GetSql("RADT.Inpatient.UpdatePatientInTimes", ref sqlStr) == -1)
            {
                this.Err = "��ȡSql��������,������:";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, patientNo, intimes);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(sqlStr);
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
            if (Sql.GetSql("RADT.InPatient.UpdatePatient.2", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[84];
                try
                {
                    s[0] = PatientInfo.ID; // --סԺ��ˮ��
                    s[1] = PatientInfo.Name; //--����
                    s[2] = PatientInfo.PID.PatientNO; //  --סԺ��
                    s[3] = PatientInfo.PID.CardNO; //  --���￨��
                    s[4] = PatientInfo.SSN; //  --ҽ��֤��
                    s[5] = PatientInfo.PVisit.MedicalType.ID; //    --ҽ�����id
                    s[6] = PatientInfo.Sex.ID.ToString(); //  --�Ա�
                    s[7] = PatientInfo.IDCard; //  --���֤��
                    s[8] = PatientInfo.Memo; //  --ƴ��
                    s[9] = PatientInfo.Birthday.ToString(); //  --����
                    s[10] = PatientInfo.Profession.ID; //  --ְҵ����
                    s[11] = PatientInfo.CompanyName; //  --������λ
                    s[12] = PatientInfo.PhoneBusiness; //  --������λ�绰
                    s[13] = PatientInfo.User01; //  --��λ�ʱ�
                    s[14] = PatientInfo.AddressHome; //  --���ڻ��ͥ��ַ
                    s[15] = PatientInfo.PhoneHome; //  --��ͥ�绰
                    s[16] = PatientInfo.User02; //  --���ڻ��ͥ�ʱ�
                    s[17] = PatientInfo.DIST; // --����name
                    s[18] = PatientInfo.AreaCode; //  --�����ش���
                    s[19] = PatientInfo.Nationality.ID; //  --����id
                    s[20] = PatientInfo.Nationality.Name; // --����name
                    s[21] = PatientInfo.Kin.Name; //  --��ϵ������
                    s[22] = PatientInfo.Kin.RelationPhone; //  --��ϵ�˵绰
                    s[23] = PatientInfo.Kin.RelationAddress; //  --��ϵ�˵�ַ
                    s[24] = PatientInfo.Kin.Relation.ID; //  --��ϵ�˹�ϵid
                    s[25] = PatientInfo.Kin.Relation.Name; //  --��ϵ�˹�ϵname
                    s[26] = PatientInfo.MaritalStatus.ID.ToString(); //  --����״��id
                    s[27] = PatientInfo.MaritalStatus.Name; // --����״��name
                    s[28] = PatientInfo.Country.ID; //  --����id
                    s[29] = PatientInfo.Country.Name; //--��������
                    s[30] = PatientInfo.Height; //  --���
                    s[31] = PatientInfo.Weight; //  --����
                    s[32] = PatientInfo.Profession.Name; //  --ְҵid
                    s[33] = PatientInfo.BloodType.ID.ToString(); //  --ABOѪ��
                    s[34] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //  --�ش󼲲���־
                    s[35] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --������־
                    s[36] = PatientInfo.PVisit.InTime.ToString(); //  --��Ժ����
                    s[37] = PatientInfo.PVisit.PatientLocation.Dept.ID; //  --���Ҵ���
                    s[38] = PatientInfo.PVisit.PatientLocation.Dept.Name; // --��������
                    s[39] = PatientInfo.Pact.PayKind.ID; // --�������id 1-�Է�  2-���� 3-������ְ 4-�������� 5-���Ѹ߸�
                    s[40] = PatientInfo.Pact.PayKind.Name; //  --�����������
                    s[41] = PatientInfo.Pact.ID; // --��ͬ����
                    s[42] = PatientInfo.Pact.Name; // --��ͬ��λ����
                    s[43] = PatientInfo.PVisit.PatientLocation.Bed.ID; // --����
                    s[44] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //--����Ԫ����
                    s[45] = PatientInfo.PVisit.PatientLocation.NurseCell.Name; // --����Ԫ����
                    s[46] = PatientInfo.PVisit.AdmittingDoctor.ID; //--ҽʦ����(סԺ)
                    s[47] = PatientInfo.PVisit.AdmittingDoctor.Name; //--ҽʦ����(סԺ)
                    s[48] = PatientInfo.PVisit.AttendingDoctor.ID; // --ҽʦ����(����)
                    s[49] = PatientInfo.PVisit.AttendingDoctor.Name; //--ҽʦ����(����)
                    s[50] = PatientInfo.PVisit.ConsultingDoctor.ID; // --ҽʦ����(����)
                    s[51] = PatientInfo.PVisit.ConsultingDoctor.Name; //--ҽʦ����(����)
                    s[52] = PatientInfo.PVisit.TempDoctor.ID; //--ҽʦ����(ʵϰ)
                    s[53] = PatientInfo.PVisit.TempDoctor.Name; //--ҽʦ����(ʵϰ)
                    s[54] = PatientInfo.PVisit.AdmittingNurse.ID; // --��ʿ����(����)
                    s[55] = PatientInfo.PVisit.AdmittingNurse.Name; // --��ʿ����(����)
                    s[56] = PatientInfo.PVisit.Circs.ID; // --��Ժ���id
                    s[57] = PatientInfo.PVisit.Circs.Name; // --��Ժ���name
                    s[58] = PatientInfo.PVisit.AdmitSource.ID; // --��Ժ;��id
                    s[59] = PatientInfo.PVisit.AdmitSource.Name; // --��Ժ;��name
                    s[60] = PatientInfo.PVisit.InSource.ID; // --��Ժ��Դid 1 -���� 2 -���� 3 -ת�� 4 -תԺ
                    s[61] = PatientInfo.PVisit.InSource.Name; // --��Ժ��Դname
                    s[62] = PatientInfo.PVisit.InState.ID.ToString(); // --סԺ�Ǽ�  i-�������� -��Ժ�Ǽ� o-��Ժ���� p-ԤԼ��Ժ n-�޷���Ժ
                    s[63] = PatientInfo.PVisit.PreOutTime.ToString(); // --��Ժ����(ԤԼ)
                    s[64] = PatientInfo.PVisit.OutTime.ToString(); // --��Ժ����
                    try
                    {
                        if (PatientInfo.PVisit.ICULocation == null)
                        {
                            s[65] = "0";
                        }
                        else
                        {
                            s[65] = "1"; // --�Ƿ���ICU
                        }
                        s[66] = Operator.ID;
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    s[67] = PatientInfo.Memo;
                    s[68] = PatientInfo.FT.BloodLateFeeCost.ToString(); //Ѫ���ɽ�
                    s[69] = PatientInfo.FT.BedLimitCost.ToString(); //��λ����
                    s[70] = PatientInfo.FT.AirLimitCost.ToString(); //�յ�����
                    s[71] = PatientInfo.ProCreateNO; //�������յ��Ժ�
                    s[72] = PatientInfo.FT.FixFeeInterval.ToString(); //���Ѽ��
                    s[73] = PatientInfo.FT.BedOverDeal.ToString(); //���괦��
                    s[74] = PatientInfo.ExtendFlag; //�Ƿ��������޶�� 0 ��ͬ�� 1 ͬ��
                    s[75] = PatientInfo.ExtendFlag1;
                    s[76] = PatientInfo.ExtendFlag2;
                    s[77] = PatientInfo.ClinicDiagnose; //�������
                    s[78] = PatientInfo.MainDiagnose; //סԺ�����
                    s[79] = PatientInfo.DoctorReceiver.ID;//��סҽʦ
                    s[80] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[81] = PatientInfo.NormalName;
                    s[82] = PatientInfo.IDCardType.ID;//֤������
                    strSql = string.Format(strSql, s, PatientInfo.InTimes);
                    //    strSql = string.Format(strSql, PatientInfo.InTimes);

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

            //���Ӥ��������Ϣ�޸�,��ͬʱ����Ӥ�����е���Ϣ
            if (PatientInfo.ID.IndexOf("B") > 0)
            {
                return UpdateBabyInfo(PatientInfo);
            }

            return 1;
        }
        /// <summary>
        /// ����Ӥ����
        /// </summary>
        /// <param name="babyInfo"></param>
        /// <returns></returns>
        public int UpdateBabyInfo(FS.HISFC.Models.RADT.PatientInfo babyInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.UpdateBabyInfo.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[13];
                s[0] = babyInfo.ID; //Ӥ��סԺ��ˮ��
                s[1] = babyInfo.User01; //�������
                s[2] = babyInfo.Name; //����
                s[3] = babyInfo.Sex.ID.ToString(); //�Ա�
                s[4] = babyInfo.Birthday.ToString(); //����
                s[5] = babyInfo.Height; //���
                s[6] = babyInfo.Weight; //����
                s[7] = babyInfo.BloodType.ID.ToString(); //Ѫ�ͱ���
                s[8] = babyInfo.PVisit.InTime.ToString(); //��Ժ����
                s[9] = babyInfo.PVisit.PreOutTime.ToString(); //��Ժ����(ԤԼ)
                s[10] = Operator.ID; //����Ա
                s[11] = GetSysDateTime().ToString(); //��������
                s[12] = babyInfo.PID.MotherInpatientNO; //ĸ��סԺ��ˮ��
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

    }
}
