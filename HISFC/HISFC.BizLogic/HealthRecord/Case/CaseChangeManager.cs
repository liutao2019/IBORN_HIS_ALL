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
    public class CaseChangeManager : FS.FrameWork.Management.Database
    {
        public CaseChangeManager()
        {
        }

        /// <summary>
        /// ����������ʶ
        /// </summary>
        /// <returns></returns>
        public string GetChangeID()
        {
            return this.GetSequence("CaseManager.CaseChange.GetSequence");
        }

        /// <summary>
        /// ָ�������ŵĲ������������Ƿ����
        /// </summary>
        /// <param name="oldCode">�ɲ�����</param>
        /// <returns>true����;  false������</returns>
        public bool IsApplyExist(string oldCode)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.CaseChange.ApplyExist", ref strsql) == -1)
            {
                return true;
            }
            try
            {
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }

            string retv = this.ExecSqlReturnOne(strsql);
            if (retv == null || retv == string.Empty)
            {
                return true;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(retv) != 0)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// ���ݾɲ�����ȡ�ò�����������
        /// </summary>
        /// <param name="oldCode">�ɲ�����</param>
        /// <returns>null ʧ��</returns>
        public FS.HISFC.Models.HealthRecord.Case.CaseChange GetChangeApplyByOldCode(string oldCode)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.CaseChange.SelectApplyByID", ref strsql) == -1)
            {
                this.Err = "��ȡ  CaseManager.CaseChange.SelectApplyByID  ʧ��";
                return null;
            }
            try
            {
                strsql = string.Format(strsql, oldCode);
            }
            catch (Exception ex)
            {
                this.Err = "GetChangeApplyByOldCode ���ַ���ʧ��" + ex.Message;
                return null;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                this.Err = "GetChangeApplyByOldCodeִ��ʧ���˳ɹ�֮ĸ";
                return null;
            }

            if (this.Reader.Read())
            {
                FS.HISFC.Models.HealthRecord.Case.CaseChange change = new FS.HISFC.Models.HealthRecord.Case.CaseChange();

                change.ID = this.Reader[0].ToString();
                change.OldCardNO = this.Reader[1].ToString();
                change.NewCardNO = this.Reader[2].ToString();
                change.DoctorEnv.ID = this.Reader[3].ToString();
                change.DoctorEnv.Name = this.Reader[4].ToString();
                change.DoctorEnv.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                change.Memo = this.Reader[6].ToString();

                this.Reader.Close();

                return change;

            }

            return null;
        }

        /// <summary>
        /// �����µĲ�����������
        /// </summary>
        /// <param name="change">��������ʵ��</param>
        /// <returns>-1ʧ��; 1�ɹ�</returns>
        public int InsertApply(FS.HISFC.Models.HealthRecord.Case.CaseChange change)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.CaseChange.InsertApply", ref strsql) == -1)
            {
                this.Err = "��ȡ  CaseManager.CaseChange.InsertApply ʧ��";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql,
                                       change.ID,
                                       change.OldCardNO,
                                       change.NewCardNO,
                                       change.DoctorEnv.ID,
                                       change.DoctorEnv.OperTime.ToString(),
                                       change.Memo);
            }
            catch (Exception ex)
            {
                this.Err = "InsertApply ���ַ���ʧ���˳ɹ�֮ĸ" + ex.Message;
                return -1;
            }

            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ȷ�ϲ�����������
        /// </summary>
        /// <param name="change">��������ʵ��</param>
        /// <returns>-1ʧ��, 1�ɹ�</returns>
        public int UpdateApply(FS.HISFC.Models.HealthRecord.Case.CaseChange change)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.CaseChange.UpdateApply", ref strsql) == -1)
            {
                this.Err = "��ȡ  CaseManager.CaseChange.UpdateApply  ʧ��";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql,
                                       change.OperEnv.ID,
                                       change.OperEnv.OperTime.ToString(),
                                       "1",/*change.IsValid*/
                                       change.ChargeCost.ToString(),
                                       change.ID,
                                       change.OldCardNO);
            }
            catch (Exception ex)
            {
                this.Err = "UpdateApply ���ַ���ʧ��" + ex.Message;
                return -1;
            }
            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }

            return 1;
        }
    }
}
