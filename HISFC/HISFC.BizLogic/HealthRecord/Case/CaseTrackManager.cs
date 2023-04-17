using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.HealthRecord.Case
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: ��������ҵ���]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-09-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class CaseTrackManager : FS.FrameWork.Management.Database
    {
        public CaseTrackManager()
        {
        }

        /// <summary>
        /// ��ѯ���ټ�¼
        /// </summary>
        /// <param name="caseID">������</param>
        /// <returns>nullʧ��</returns>
        public List<FS.HISFC.Models.HealthRecord.Case.CaseTrack> QueryTrackRecordByCaseID(string caseID)
        {
            if (caseID == null || caseID == string.Empty)
            {
                this.Err = "������Ϊ��";
                return null;
            }

            string strsql = "";
            if (this.Sql.GetSql("CaseManager.Track.SelectByCaseID", ref strsql) == -1)
            {
                this.Err = "��ȡ CaseManager.Track.SelectByCaseID ʧ��";
                return null;
            }

            try
            {
                strsql = string.Format(strsql, caseID);
            }
            catch (Exception ex)
            {
                this.Err = "QueryTrackRecordByCaseID ���ַ���ʧ��" + ex.Message;
                return null;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                this.Err = "ִ�� QueryTrackRecordByCaseID ʧ��";
                return null;
            }

            List<FS.HISFC.Models.HealthRecord.Case.CaseTrack> listTrack = new List<FS.HISFC.Models.HealthRecord.Case.CaseTrack>();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.HealthRecord.Case.CaseTrack track = new FS.HISFC.Models.HealthRecord.Case.CaseTrack();

                track.ID = this.Reader.IsDBNull(0) ? "" : this.Reader[0].ToString();
                track.PatientCase.ID = this.Reader.IsDBNull(1) ? "" : this.Reader[1].ToString();

                track.UseCaseEnv.ID = this.Reader.IsDBNull(2) ? "" : this.Reader[2].ToString();//ʹ���˱��
                track.UseCaseEnv.Name = this.Reader.IsDBNull(3) ? "" : this.Reader[3].ToString();//����

                track.UseCaseEnv.Dept.ID = this.Reader.IsDBNull(4) ? "" : this.Reader[4].ToString();//ʹ�ÿ��ұ��
                track.UseCaseEnv.Dept.Name = this.Reader.IsDBNull(5) ? "" : this.Reader[5].ToString();//ʹ�ÿ�������

                track.UseCaseEnv.OperTime = this.Reader.IsDBNull(6) ? DateTime.MinValue : FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());//ʹ��ʱ��

                track.UseCaseEnv.User01 = this.Reader.IsDBNull(7) ? "" : this.Reader[7].ToString();//����ʹ�����ͱ���
                track.UseCaseEnv.User02 = this.Reader.IsDBNull(8) ? "" : this.Reader[8].ToString();//����ʹ����������

                listTrack.Add(track);
            }

            this.Reader.Close();

            return listTrack;
        }

        /// <summary>
        /// ������ټ�¼
        /// </summary>
        /// <param name="caseTrack">��������ʵ��</param>
        /// <returns>-1ʧ�ܣ�1�ɹ�</returns>
        public int InsertTrackRecord(FS.HISFC.Models.HealthRecord.Case.CaseTrack caseTrack, EnumTrackType trackType)
        {
            if (caseTrack == null)
            {
                this.Err = "��������ʵ��Ϊ��";
                return -1;
            }

            string strsql = "";
            if (this.Sql.GetSql("CaseManager.Track.InsertTrackInfo", ref strsql) == -1)
            {
                this.Err = "��ȡ CaseManager.Track.InsertTrackInfo ʧ��";
                return -1;
            }

            switch (trackType)
            {
                case EnumTrackType.CLINIC_CURE:
                    caseTrack.User01 = "01";
                    break;
                case EnumTrackType.INPATIENT_CURE:
                    caseTrack.User01 = "02";
                    break;
                case EnumTrackType.CASE_LEND:
                    caseTrack.User01 = "03";
                    break;
                case EnumTrackType.CASE_CHECK:
                    caseTrack.User01 = "04";
                    break;
                case EnumTrackType.CASE_QUALITY:
                    caseTrack.User01 = "05";
                    break;
                case EnumTrackType.FANGLIAO:
                    caseTrack.User01 = "06";
                    break;
                case EnumTrackType.CASE_VISIT:
                    caseTrack.User01 = "07";
                    break;
                default:
                    caseTrack.User01 = "03";
                    break;
            }

            try
            {
                strsql = string.Format(strsql, /*caseTrack.ID*/this.GetTrackID(),
                                               caseTrack.PatientCase.ID,
                                               caseTrack.UseCaseEnv.OperTime.ToString(),
                                               caseTrack.UseCaseEnv.ID,
                                               caseTrack.UseCaseEnv.Dept.ID,
                                               caseTrack.User01);
            }
            catch (Exception ex)
            {
                this.Err = "InsertTrackRecord ���ַ���ʧ��" + ex.Message;
                return -1;
            }

            if (this.ExecNoQuery(strsql) <= 0)
            {
                this.Err = "InsertTrackRecord ִ��ʧ��";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ز���������ˮ��
        /// </summary>
        /// <returns></returns>
        private string GetTrackID()
        {
            return this.GetSequence("CaseManager.Track.GetSequence");
        }
    }

    public enum EnumTrackType
    {
        /// <summary>
        /// �������
        /// </summary>
        CLINIC_CURE = 1,
        /// <summary>
        /// סԺ����
        /// </summary>
        INPATIENT_CURE,
        /// <summary>
        /// ��������
        /// </summary>
        CASE_LEND,
        /// <summary>
        /// ��������
        /// </summary>
        CASE_CHECK,
        /// <summary>
        /// �����ʼ�
        /// </summary>
        CASE_QUALITY,
        /// <summary>
        /// ��������¼��
        /// </summary>
        FANGLIAO,
        /// <summary>
        /// �������
        /// </summary>
        CASE_VISIT
    }
}