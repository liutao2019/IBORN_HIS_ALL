using System;
using System.Data;
namespace FS.HISFC.BizLogic.Manager 
{
	/// <summary>
	/// AllObjects ��ժҪ˵����
	/// </summary>
	public class AllObjects:DataBase
	{
		public AllObjects()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �õ�������Ҫ����޸�(ˢ�¡�����)�Ķ���
		/// </summary>
		/// <returns></returns>
		public DataSet GetAllObject(string owner)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				//��ȡSQL
				if(this.GetSQL("HISFC.Management.GetAllObject",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
                strSql = string.Format(strSql, owner);
				//��ѯ 
				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}

		/// <summary>
		/// �޸�����
		/// </summary>
		/// <param name="Ownobject"></param>
		/// <returns></returns>
		public int AlterSql(string Ownobject)
		{
			string strSql = "";
			int  Return = 0;
			try
			{
				if(this.GetSQL("HISFC.Management.AlterSql",ref strSql)==-1 )
				{
					this.Err = this.Sql.Err;
					return -1;
				}
				else
				{
					//��ʽ��SQL
					strSql = string.Format(strSql,Ownobject);
				}
				// ִ��SQL
				Return = this.ExecNoQuery(strSql);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return Return;
		}

        /// <summary>
        /// ��ѯ����������
        /// </summary>
        /// <returns>�����̼���</returns>
        public DataSet QueryLockSession()
        {
            DataSet ds = new DataSet();

            string strsql = "";
            try
            {
                if (this.GetSQL("Manager.QueryLockSession", ref strsql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
                this.ExecQuery(strsql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="serial"></param>
        /// <returns>1,�ɹ���-1ʧ��</returns>
        public int AlterSessionState(string sid, string serial)
        {
            string strsql = "";
            try
            {
                if (this.GetSQL("Manager.KillLockSession", ref strsql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
                strsql = string.Format(strsql, sid, serial);
                if (this.ExecNoQuery(strsql) == -1)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

	}
}
