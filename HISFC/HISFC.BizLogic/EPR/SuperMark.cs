using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizLogic.EPR
{
    public class SuperMark:FS.FrameWork.Management.Database 
    {
        
		/// <summary>
		/// �����ϼ��޸ĺۼ�
		/// </summary>
		/// <param name="supermark">Ȩ��ʵ��</param>
        /// <param name="img">�޸ĺۼ�</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img) 
		{
			//ѡ��
			FS.FrameWork.Models.NeuObject obj = GetSuperMark(supermark);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (obj == null)
            {
                return InsertSuperMark(supermark, img);
            }
            else
            {
                return UpdateSuperMark(supermark, img);
            }
		}
        /// <summary>
		/// �޸�һ���ϼ��޸ļ�¼
		/// </summary>
		/// <returns></returns>
		public int UpdateSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.EMR.UpdateSuperMark",ref strSql)==-1) return -1;
			strSql = string.Format(strSql, supermark.ID,supermark.Name,FS.FrameWork.Management.Connection.Operator.ID);

			return this.InputBlob(strSql, img);
		}
        /// <summary>
        /// ɾ��һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int DeleteSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.DeleteSuperMark", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, supermark.ID, supermark.Name);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int InsertSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.InsertSuperMark", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, supermark.ID, supermark.Name, supermark.Memo, supermark.User01,supermark.User02, supermark.User03);

            return this.InputBlob(strSql, img);
        }

        /// <summary>
		/// ����ϼ��޸ĺۼ�
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject GetSuperMark(FS.FrameWork.Models.NeuObject obj)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.EMR.GetSuperMark",ref strSql)==-1) return null;
			strSql = string.Format(strSql,obj.ID, obj.Name);
            
			ArrayList al =  this.myGetSuperMark(strSql);
			if(al ==null || al.Count == 0) return null;
			return al[0] as FS.FrameWork.Models.NeuObject;
		}

        /// <summary>
		/// ����ϼ��޸ĺۼ�
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public byte[] GetSuperMarkImage(FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("EPR.EMR.GetSuperMarkImage", ref strSql) == -1) return null;
            strSql = string.Format(strSql, obj.ID, obj.Name);
            return this.OutputBlob(strSql);
        } 

        #region "˽��"
        private ArrayList myGetSuperMark(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject supermark = new FS.FrameWork.Models.NeuObject();
                supermark.ID = this.Reader[0].ToString();
                supermark.Name = this.Reader[1].ToString();
                supermark.Memo = this.Reader[2].ToString();
                supermark.User01 = this.Reader[3].ToString();
                supermark.User02 = this.Reader[4].ToString();
                supermark.User03 = this.Reader[5].ToString();
                al.Add(supermark);
            }
            this.Reader.Close();
            return al;
        }

		#endregion

    }
}
