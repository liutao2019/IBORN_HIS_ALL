using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
    #region SNOPMED
    /// <summary>
    /// SNOPMED������
    /// </summary>
    public class SNOMED : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// �ٴ�·����Ŀ������
        /// </summary>
        public SNOMED()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���SNOPMED

        /// <summary>
        /// ���������Ŀ�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSNOPMED()
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Select�ֶ�!";
                return null;
            }

            return myGetSNOPMED(strSQL);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentCode">��������</param>
        /// <param name="bChild">�Ƿ��������</param>
        /// <returns></returns>
        public ArrayList GetSNOPMED(string parentCode, bool bChild)
        {
            string strSQL = "";
            string strSQLWhere = "";
            //ȡSQL���
            if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Select�ֶ�!";
                return null;
            }
            if (bChild)
            {
                //ȡSQL���
                if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Where.2", ref strSQLWhere) == -1)
                {
                    this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Where.2�ֶ�!";
                    return null;
                }
            }
            else
            {
                //ȡSQL���
                if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Where.3", ref strSQLWhere) == -1)
                {
                    this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Where.3�ֶ�!";
                    return null;
                }
            }
            try
            {
                strSQLWhere = string.Format(strSQLWhere, parentCode);
            }
            catch { return null; }

            return myGetSNOPMED(strSQL + " " + strSQLWhere);
        }
        /// <summary>
        /// ��õ�����Ŀ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.EPR.SNOMED GetSNOPMED(string id)
        {
            string strSQL = "";
            string strSQLWhere = "";

            //ȡSQL���
            if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Select�ֶ�!";
                return null;
            }

            //ȡSQL���
            if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Where.1", ref strSQLWhere) == -1)
            {
                this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Where.1�ֶ�!";
                return null;
            }
            try
            {
                strSQLWhere = string.Format(strSQLWhere, id);
            }
            catch { return null; }

            ArrayList al = myGetSNOPMED(strSQL + " " + strSQLWhere);
            if (al == null)
                return null;
            else if (al.Count == 0)
                return null;
            else
                return al[0] as FS.HISFC.Models.EPR.SNOMED;


        }

        /// <summary>
        /// ���������Ŀ�б�
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetSNOPMEDs()
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("ClinicalPath.SNOPMED.Get.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�ClinicalPath.SNOPMED.Get.Select�ֶ�!";
                return null;
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strSQL, ref ds) == -1)
                return null;

            return ds;
        }

        /// <summary>
        /// �����Ŀ��Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList myGetSNOPMED(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            while (this.Reader.Read())
            {
                FS.HISFC.Models.EPR.SNOMED item = new FS.HISFC.Models.EPR.SNOMED();
                item.ID = this.Reader[0].ToString();
                item.Name = this.Reader[1].ToString();
                item.SNOPCode = this.Reader[2].ToString();
                item.EnglishName = this.Reader[3].ToString();
                item.DiagnoseCode = this.Reader[4].ToString();
                item.ParentCode = this.Reader[5].ToString();
                item.Memo = this.Reader[6].ToString();
                item.SpellCode = this.Reader[7].ToString();
                item.WBCode = this.Reader[8].ToString();
                item.UserCode = this.Reader[9].ToString();
                item.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                try
                {
                    item.User01 = this.Reader[11].ToString(); //isleaf
                }
                catch { }
                al.Add(item);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region ����SNOPMED
        /// <summary>
        /// ���µ�����Ŀ�б�
        /// <param name="code">����</param>
        /// </summary>
        /// <returns></returns>
        public int UpdateSNOPMED(FS.HISFC.Models.EPR.SNOMED s)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("EPR.SNOMED.Update.1", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�EPR.SNOMED.Update.1�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, s.ParentCode, s.ID, s.SNOPCode, s.Name, s.EnglishName, s.SpellCode, s.WBCode, s.DiagnoseCode, s.Memo, s.UserCode, s.SortID);
            }
            catch { return -1; }
            return this.ExecNoQuery(strSQL);
            
        }

        /// <summary>
        /// ���µ�����Ŀ�б�ĸ�������
        /// <param name="code">����</param>
        /// </summary>
        /// <returns></returns>
        public int UpdateSNOPMEDParentCode(FS.HISFC.Models.EPR.SNOMED s)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("EPR.SNOMED.Update.2", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�EPR.SNOMED.Update.2�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, s.ParentCode);
            }
            catch { return -1; }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region ɾ��SNOPMED
        /// <summary>
        /// ɾ������Ϊcode�ļ�¼
        /// <param name="code">����</param>
        /// </summary>
        /// <returns></returns>
        public int DelSNOPMEDByCode(string code)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("EPR.SNOMED.Delet.2", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�EPR.SNOMED.Delet.2�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, code);
            }
            catch { return -1; }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ����������Ϊparentcode�ļ�¼
        /// <param name="parentcode">��������</param>
        /// </summary>
        /// <returns></returns>
        public int DelSNOPMEDByPCode(string parentcode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("EPR.SNOMED.Delet.1", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�EPR.SNOMED.Delet.1�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, parentcode);
            }
            catch { return -1; }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region ����һ��SNOPMED
        /// <summary>
        /// ����һ��SNOPMED��¼
        /// <param name="s">snomed</param>
        /// </summary>
        /// <returns></returns>
        public int InsertSNOMED(FS.HISFC.Models.EPR.SNOMED s)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.Sql.GetSql("EPR.SNOMED.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�EPR.SNOMED.Delet.1�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, s.ParentCode, s.ID, s.SNOPCode, s.Name, s.EnglishName, s.SpellCode, s.WBCode, s.DiagnoseCode, s.Memo, s.UserCode, s.SortID);
            }
            catch { return -1; }
            return this.ExecNoQuery(strSQL);

        }

        #endregion

    }
    #endregion
}