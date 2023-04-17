using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// Cpactunitstat ��ժҪ˵����
	/// </summary>
	public class Cpactunitstat :FS.FrameWork.Management.Database 
	{
		/// <summary>
		/// 
		/// </summary>
		public Cpactunitstat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///  ��ѯ��ͬ��λ�����
		/// </summary>
		/// <returns></returns>
		public ArrayList GetCpackunitstat()
		{
			ArrayList List = null;
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.Cpactunitstat.GetCpackunitstat",ref strSql)==-1) return null;
			try
			{
				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.PactStat info = null;
				List = new ArrayList();
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.PactStat();
					info.ID = Reader[0].ToString();
					info.Name =Reader[1].ToString();
					info.ID= Reader[2].ToString();
					info.Name =Reader[3].ToString();
					//info. =Reader[4].ToString();
					//info.WBCode  = Reader[5].ToString();
					//info.InputCode =Reader[6].ToString();
					if(Reader[7]!=DBNull.Value)
					{
						info.SortId = Convert.ToInt32(Reader[7]);
					}
					string Temp = Reader[8].ToString();
					if(Temp =="0")
					{
						info.ValidState = "����";
					}
					else if(Temp=="1")
					{
						info.ValidState ="ͣ��";
					}
					else if(Temp=="2")
					{
						info.ValidState ="����";
					}
					else
					{
						info.ValidState ="";
					}
					List.Add(info);
					info = null;
				}

			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return List ;
		}

		/// <summary>
		/// ���º�ͬ��λ�����
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public  int UpdateCpackunitstat(FS.HISFC.Models.Fee.PactStat info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.Cpactunitstat.UpdateCpackunitstat",ref strSql)==-1)return -1;
			try
			{
				string OperId =this.Operator.ID;
				//UPDATE  com_pactunitstat set (group_name = '{1}', pact_code ='{2}' , spell_code ='{3}', wb_code ='{4}',input_code ='{5}', SORT_ID ={6}, VALID_STATE ='{7}', OPER_CODE ='{8}' , oper_date =sysdate ) where GROUP_ID ='{0}'   and parent_code = '[��������]' and current_code ='[��������]' 
				strSql = string.Format(strSql,info.ID,info.Name,info.ID,info.SpellCode,info.WBCode, info.UserCode,info.SortId,info.ValidState,OperId);
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertCpackunitstat(FS.HISFC.Models.Fee.PactStat info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.Cpactunitstat.InsertCpackunitstat",ref strSql)==-1)return -1;
			try
			{
				//insert into com_pactunitstat  values ('[��������]','[��������]' ,'{0}', '{1}','{2}' ,'{3}' ,'{4}' ,'{5}' ,{6} , '{7}','{8}',sysdate)
				string OperId =this.Operator.ID;
				strSql = string.Format(strSql,info.ID,info.Name,info.ID,info.SpellCode,info.WBCode,info.UserCode,info.SortId,info.ValidState,OperId);
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
	}
}
