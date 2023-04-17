using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// �Һż��������
	/// </summary>
	public class RegLevel:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// �Һż��������
		/// </summary>
		public RegLevel()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		/// <summary>
		/// �Һż���ʵ��
		/// </summary>
		protected FS.HISFC.Models.Registration.RegLevel regLvl = null;

		/// <summary>
		///ArrayList
		/// </summary>
		protected ArrayList al = null;
		#endregion

		#region ����
		/// <summary>
		/// ����һ���Һż���
		/// </summary>
		/// <param name="regLevel"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Registration.RegLevel regLevel)
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Insert",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,regLevel.ID,regLevel.Name,regLevel.UserCode,regLevel.SortID.ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsValid).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsExpert).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsFaculty).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsDefault).ToString(),regLevel.Oper.ID,
					regLevel.Oper.OperTime.ToString(),FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsSpecial)
                    /*{156C449B-60A9-4536-B4FB-D00BC6F476A1}*/, FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsEmergency));
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Insert]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region ɾ��
		/// <summary>
		/// ���ݹҺż������ɾ��һ���Һż���
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string ID)
		{
			string sql="";
			
			if(this.Sql.GetCommonSql("Registration.RegLevel.Delete",ref sql) == -1) return -1;

			try
			{
				sql=string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Delete]��ʽ��ƥ��!" + e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region ��ѯ
		/// <summary>
		/// ��ѯȫ���Һż���
		/// </summary>
		/// <returns></returns>
		public ArrayList Query()
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql)==-1) return null;
			
			return this.queryBase(sql);
		}
		/// <summary>
		/// ���Ƿ���Ч��ѯ�Һż���
		/// </summary>
		/// <param name="isValid"></param>
		/// <returns></returns>
		public ArrayList Query(bool isValid)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.3",ref where) == -1)return null;

			sql = sql + " " +where;
			try
			{
				sql = string.Format(sql,FS.FrameWork.Function.NConvert.ToInt32(isValid).ToString());
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Query3]��ʽ��ƥ��!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return this.queryBase(sql);
		}
		/// <summary>
		/// ���ݴ������һ���Һż���
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.RegLevel Query(string ID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.4",ref where) == -1)return null;

			sql = sql + " " +where;
			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Query4]��ʽ��ƥ��!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			if(this.queryBase(sql) == null)return null ;

			if(al == null)
			{
				return null;
			}
			else if(al.Count == 0)
			{
				return new FS.HISFC.Models.Registration.RegLevel () ;
			}
			else
			{
				return (FS.HISFC.Models.Registration.RegLevel)al[0];
			}
		}
		/// <summary>
		/// ����sql��ѯ�Һż�����Ϣ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList queryBase(string sql)
		{
			if(this.ExecQuery(sql) == -1) return null;
			try
			{
				this.al = new ArrayList();

				while(this.Reader.Read())
				{
					this.regLvl = new FS.HISFC.Models.Registration.RegLevel();
					//���
					this.regLvl.ID = this.Reader[2].ToString();
					//����
					this.regLvl.Name = this.Reader[3].ToString();
					//������
					this.regLvl.UserCode = this.Reader[4].ToString();
					//��ʾ˳��
					this.regLvl.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
					//�Ƿ���Ч
					this.regLvl.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
					//�Ƿ�ר�Һ�
					this.regLvl.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
					//�Ƿ�ר�ƺ�
					this.regLvl.IsFaculty = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
					//�Ƿ������
					this.regLvl.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
					//�Ƿ�Ĭ��
					this.regLvl.IsDefault = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[10].ToString());
					//����Ա
					this.regLvl.Oper.ID = this.Reader[11].ToString();
					//����ʱ��
					this.regLvl.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    this.regLvl.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13].ToString());

					this.al.Add(this.regLvl);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = "��ѯ�Һż������!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return al;
		}
		#endregion
	}
}
