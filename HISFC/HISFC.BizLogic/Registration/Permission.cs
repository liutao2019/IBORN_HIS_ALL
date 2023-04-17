using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// �Һſ��ҹ�����
	/// </summary>
	public class Permission:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// �Һſ��ҹ�����
		/// </summary>
		public Permission()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����
		/// </summary>
		protected ArrayList al=null;
		/// <summary>
		/// neuObject
		/// </summary>
		protected FS.FrameWork.Models.NeuObject obj=null;

        #region ��ѯ
        /// <summary>
		/// ����ܲ���ĳһ���ڵ���Ա�б�
		/// </summary>
		/// <param name="formName"></param>
		/// <returns></returns>
		public ArrayList Query(string formName)
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.Permission.Query.1",ref sql)==-1)return null;

			try
			{
				sql=string.Format(sql,formName,FS.FrameWork.Management.Connection.Hospital.ID);
			}
			catch(Exception e)
			{
				this.Err="[Registration.Permission.Query.1]��ʽ��ƥ��!"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}

			this.al=new ArrayList();

			try
			{
				if(this.ExecQuery(sql)==-1)return null;
				while(this.Reader.Read())
				{
					obj=new FS.FrameWork.Models.NeuObject();
					obj.ID=this.Reader[0].ToString();//Ա������
					obj.Name=this.Reader[1].ToString();//Ա������

					this.al.Add(obj);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ȡ��������"+formName+"����Ա�б����!"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return al;
		}
		/// <summary>
		/// ��ѯ��Ա����Һſ���
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public ArrayList Query(FS.FrameWork.Models.NeuObject person)//{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}
		{
			string sql="";
			if(this.Sql.GetCommonSql("Registration.Permission.Query.2",ref sql)==-1)return null;

			this.al=new ArrayList();
			try
			{
				sql=string.Format(sql,person.ID);
				if(this.ExecQuery(sql)==-1)return null;

				while(this.Reader.Read())
				{
					this.obj=new FS.FrameWork.Models.NeuObject();

					this.obj.ID=this.Reader[2].ToString();//Ա������
					this.obj.User01=this.Reader[3].ToString();//�Һſ���
					this.obj.User02=this.Reader[4].ToString();//����Ա
					this.obj.User03=this.Reader[5].ToString();//����ʱ��

                    //�޸ĹҺ�Ȩ�� {E2B7B9D5-6FE1-4849-AAEC-ABD916075049}
                    if (this.Reader.FieldCount >= 6)
                    {

                        this.obj.Memo = this.Reader[6].ToString();
                    }

					this.al.Add(this.obj);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ѯ��Ա�Һſ���Ȩ�޳���!"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return this.al;

        }
        /// <summary>
        /// ��ѯ��Ա����Һſ��ң��ų���{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public ArrayList QueryOutContain(FS.FrameWork.Models.NeuObject person)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Permission.Query.3", ref sql) == -1) return null;

            this.al = new ArrayList();
            try
            {
                sql = string.Format(sql, person.ID);
                if (this.ExecQuery(sql) == -1) return null;

                while (this.Reader.Read())
                {
                    this.obj = new FS.FrameWork.Models.NeuObject();

                    this.obj.ID = person.ID;//Ա������
                    this.obj.User01 = this.Reader[0].ToString();//�Һſ���

                    this.al.Add(this.obj);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "��ѯ��Ա�Һſ���Ȩ�޳���!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.al;

        }
        #endregion

        #region ����
        /// <summary>
        /// �Ǽ���Ա�Һſ��ұ�{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}
		/// </summary>
		/// <param name="permission"></param>
		/// <returns></returns>
		public int Insert(FS.FrameWork.Models.NeuObject permission)
		{
			string sql="";
			if(this.Sql.GetCommonSql("Registration.Permission.Insert",ref sql)==-1)return -1;

			try
			{
                ////�޸ĹҺ�Ȩ�� {E2B7B9D5-6FE1-4849-AAEC-ABD916075049}
				sql=string.Format(sql,permission.ID,permission.User01,permission.User02,permission.Memo);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="����Һ�ԱȨ�ޱ����![Registration.Permission.Insert]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
        }
        #endregion

        #region ɾ��
        /// <summary>
		/// ɾ���Һ�Ա����ĹҺſ���
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public int Delete(string userID)
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.Permission.Delete",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,userID);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ���Һ�ԱȨ�ޱ����![Registration.Permission.Delete]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
        }
        #endregion
    }
}
