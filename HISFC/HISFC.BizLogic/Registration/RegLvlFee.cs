using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// �Һż��������
	/// </summary>
	public class RegLvlFee:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// �Һŷѹ�����
		/// </summary>
		public RegLvlFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		/// <summary>
		/// �Һŷ�ʵ��
		/// </summary>
		protected FS.HISFC.Models.Registration.RegLvlFee regFee ;
		
		/// <summary>
		///ArrayList
		/// </summary>
		protected ArrayList al = null;
		#endregion 

		#region ����
		
        /// <summary>
        /// ����һ���Һŷ�
        /// </summary>
        /// <param name="regFee"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.RegLvlFee regFee)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Insert.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, regFee.ID, regFee.Pact.ID, regFee.RegLevel.ID, "",
                    regFee.RegFee, regFee.ChkFee, regFee.OwnDigFee, regFee.OthFee,
                    regFee.Oper.ID, regFee.Oper.OperTime.ToString(), regFee.PubDigFee);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.RegFee.Insert.1]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            return this.ExecNoQuery(sql);
        }
		#endregion

		#region ɾ��
		
        /// <summary>
        /// ɾ��һ���Һŷ���Ϣ
        /// </summary>
        /// <param name="myId"></param>
        /// <returns></returns>
        public int Delete(string myId)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Delete.1", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, myId);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.RegFee.Delete.1]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ͬ��λɾ���Һŷ�
        /// </summary>
        /// <param name="pactID"></param>
        /// <returns></returns>
        public int DeleteByPact(string pactID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Delete.Pact", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, pactID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.RegFee.Delete.Pact]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���ɺ�ͬ��λ�ҺŷѸ���Ϊ�º�ͬ��λ�Һŷ�
        /// </summary>
        /// <param name="newPact"></param>
        /// <param name="oldPact"></param>
        /// <returns></returns>
        public int CopyByPact(string newPact, string oldPact)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Copy", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, oldPact, newPact);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.RegFee.Copy]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
		#endregion

        #region ����
        /// <summary>
        /// ���º�ͬ��λ�Һŷѷ�����Ϣ
        /// </summary>
        /// <param name="info"></param>
        public int Update(FS.HISFC.Models.Registration.RegLvlFee info)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Update.1", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, info.ID, info.RegFee, info.ChkFee,
                    info.OwnDigFee, info.OthFee, info.PubDigFee);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.RegFee.Update.1]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region ��ѯ

        #region ��ͬ��λ��Һŷ�
        /// <summary>
		/// ����ͬ��λ��ѯ�Һŷ���Ϣ
		/// </summary>
		/// <param name="pactID"></param>
		/// <returns></returns>
		public ArrayList Query(string pactID)
		{
			string sql="",where="";

			if(this.Sql.GetCommonSql("Registration.RegFee.Query.2",ref sql)==-1)return null;
			if(this.Sql.GetCommonSql("Registration.RegFee.Query.3",ref where)==-1)return null;

			sql=sql+" "+where ;
			try
			{
				sql=string.Format(sql,pactID);
			}
			catch(Exception e)
			{
				this.Err="��ѯ�Һż���ʱ����![Registration.RegFee.Query.3]"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}

			return QueryPact(sql);			
		}
        /// <summary>
        /// ����ͬ��λ��ѯ�Һŷ���Ϣ(�Ƿ�����Һż���)
        /// </summary>
        /// <param name="pactID"></param>
        /// <returns></returns>
        public ArrayList Query(string pactID,bool Flag)
        {
            string sql = "", where = "";
            if (Flag == true)
            {

                if (this.Sql.GetCommonSql("Registration.RegFee.Query.10", ref sql) == -1) return null;
            }
            else 
            {
                if (this.Sql.GetCommonSql("Registration.RegFee.Query.2", ref sql) == -1) return null;
                if (this.Sql.GetCommonSql("Registration.RegFee.Query.3", ref where) == -1) return null;
            }
            sql = sql + " " + where;
            try
            {
                sql = string.Format(sql, pactID);
            }
            catch (Exception e)
            {
                this.Err = "��ѯ�Һż���ʱ����![Registration.RegFee.Query.3]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return QueryPact(sql);
        }


		/// <summary>
		/// ���ݺ�ͬ��λ���Һż����ѯ�Һŷ���Ϣ
		/// </summary>
		/// <param name="pactID"></param>
		/// <param name="regLevel"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.RegLvlFee Get(string pactID,string regLevel)
		{			
			string sql="",where="";

			if(this.Sql.GetCommonSql("Registration.RegFee.Query.2",ref sql)==-1)return null;
			if(this.Sql.GetCommonSql("Registration.RegFee.Query.4",ref where)==-1)return null;

			sql=sql+" "+where ;
			try
			{
				sql=string.Format(sql,pactID,regLevel);
			}
			catch(Exception e)
			{
				this.Err="��ѯ�Һż���ʱ����![Registration.RegFee.Query.4]"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			//ȡ��Ϣ
			ArrayList al = this.QueryPact(sql);
			if(al == null) 
				return null;

			if(al.Count == 0) 
				return null;

			return al[0] as FS.HISFC.Models.Registration.RegLvlFee;
		}

        /// <summary>
        /// ��ѯ���еĹҺŷ�
        /// </summary>
        /// <returns></returns>
        public ArrayList Query()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.RegFee.Query.2", ref sql) == -1) 
                return null;

            return this.QueryPact(sql);
        }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList QueryPact(string sql)
		{
			if(this.ExecQuery(sql) == -1)return null;
			try
			{
				this.al = new ArrayList();

				while(this.Reader.Read())
				{
					this.regFee = new FS.HISFC.Models.Registration.RegLvlFee();

					//��ˮ��
					regFee.ID = this.Reader[0].ToString();
					//��ͬ��λ
					regFee.Pact.ID = this.Reader[1].ToString() ;
					//�Һż���
					regFee.RegLevel.ID = this.Reader[2].ToString();
					
					//�Һŷ�
					regFee.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString() );
					//����
					regFee.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString() );
					//����
					regFee.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString() );
					//���ӷ�
					regFee.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString() );
					//����Ա
					regFee.Oper.ID = this.Reader[8].ToString();
					//����ʱ��
					regFee.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
					
					regFee.PubDigFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString()) ;

					this.al.Add( regFee);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ѯ�Һŷѳ���!"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return al;
		}		
		#endregion
		#endregion
	}
}
