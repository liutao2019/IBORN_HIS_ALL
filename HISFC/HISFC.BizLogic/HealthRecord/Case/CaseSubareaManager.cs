using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.HealthRecord.Case
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: ��������վά��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-09-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class CaseSubareaManager : FS.FrameWork.Management.Database
    {
        public CaseSubareaManager()
        {
        }

        /// <summary>
        /// ���ݷ�������ȡ�ð����Ļ���վ
        /// </summary>
        /// <param name="subareaID">��������</param>
        /// <returns>nullʧ��</returns>
        public List<FS.HISFC.Models.HealthRecord.Case.CaseSubarea> QueryBySubareaID(string subareaID)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.Subarea.SelectBySubareaID", ref strsql) == -1)
            {
                this.Err = "��ȡ CaseManager.Subarea.SelectBySubareaID ʧ��";
                return null;
            }

            try
            {
                strsql = string.Format(strsql, subareaID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }

            List<FS.HISFC.Models.HealthRecord.Case.CaseSubarea> listSubarea = new List<FS.HISFC.Models.HealthRecord.Case.CaseSubarea>();

            while (this.Reader.Read())
            {
                FS.HISFC.Models.HealthRecord.Case.CaseSubarea subarea = new FS.HISFC.Models.HealthRecord.Case.CaseSubarea();

                subarea.SubArea.ID = this.Reader.IsDBNull(0) ? "" : this.Reader[0].ToString();
                subarea.SubArea.Name = this.Reader.IsDBNull(1) ? "" : this.Reader[1].ToString();

                subarea.NurseStation.ID = this.Reader.IsDBNull(2) ? "" : this.Reader[2].ToString();
                subarea.NurseStation.Name = this.Reader.IsDBNull(3) ? "" : this.Reader[3].ToString();

                listSubarea.Add(subarea);
            }

            this.Reader.Close();

            return listSubarea;
        }

        /// <summary>
        /// �����¼�¼
        /// </summary>
        /// <param name="subarea">ʵ��</param>
        /// <returns>-1ʧ�ܣ�1�ɹ�</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Case.CaseSubarea subarea)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.Subarea.Insert", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, subarea.SubArea.ID, subarea.NurseStation.ID);
            }
            catch (Exception ex)
            {
                return -1;
            }

            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �жϷ�������վ�Ƿ��Ѿ�����
        /// </summary>
        /// <param name="subarea">ʵ��</param>
        /// <returns>true����, false������</returns>
        public bool IsExist(FS.HISFC.Models.HealthRecord.Case.CaseSubarea subarea)
        {
            string strsql = "";
            if (this.Sql.GetSql("CaseManager.Subarea.NurseStationIsExist", ref strsql) == -1)
            {
                return true;
            }

            try
            {
                strsql = string.Format(strsql, subarea.SubArea.ID, subarea.NurseStation.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }

            string s = this.ExecSqlReturnOne(strsql);
            if( s==null || s==string.Empty)
            {
                return true;
            }

            int retv = FS.FrameWork.Function.NConvert.ToInt32(s);

            if (retv > 0)
            {
                return true;
            }

            return false;
        }
    }
}
