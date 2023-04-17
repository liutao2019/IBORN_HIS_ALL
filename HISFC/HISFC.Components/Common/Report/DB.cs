using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Common.Report
{
    public class DB:FS.FrameWork.Management.Database
    {
      
        /// <summary>
        /// ��ѯ��Ϣ
        /// </summary>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryDataBySql(string sql,ref System.Data.DataTable  dt,params string[] p )
        {
            string strsql = sql;
            //if (this.Sql.GetSql("Manager.Bed.QueryBedInfo", ref strsql) == -1)
            //{
            //    return -1;
            //}
            try
            {
                strsql = string.Format(strsql,p);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dt = ds.Tables[0];
            return 1;
        }
        /// <summary>
        /// ��ѯ��Ϣ
        /// </summary>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryDataBySqlId(string sqlId, ref System.Data.DataTable dt, params string[] p)
        {
            string strsql = string.Empty;
            if (this.Sql.GetSql(sqlId, ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, p);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dt = ds.Tables[0];
            return 1;
        }
        /// <summary>
        /// ��ѯ��Ϣ
        /// </summary>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryDataBySql(string sql, ref System.Data.DataTable dt, System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> p)
        {
            string pStr = string.Empty;

            foreach (FS.FrameWork.Models.NeuObject  no in p)
            {
                pStr = pStr + no.Name.ToString() + ",";
            }
            return this.QueryDataBySql(sql,ref dt, pStr.Split(','));
          
        }
        /// <summary>
        /// ��ѯ��Ϣ
        /// </summary>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryDataBySqlId(string sqlId, ref System.Data.DataTable dt, System.Collections.Generic.List<FS.FrameWork.Models .NeuObject> p)
        {
            string pStr = string.Empty;

            foreach (FS.FrameWork.Models.NeuObject no in p)
            {
                pStr = pStr + no.Name.ToString() + "$";
            }
            return this.QueryDataBySqlId(sqlId, ref dt, pStr.Split('$'));
        }
    }
}
