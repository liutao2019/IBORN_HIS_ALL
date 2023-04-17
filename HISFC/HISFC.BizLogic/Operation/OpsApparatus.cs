using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Operation
{
	/// [��������: ���������豸���������ϣ�������]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-27]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	/// �������ڻ�û���õ�
	public class OpsApparatus : FS.FrameWork.Management.Database 
	{
		public OpsApparatus()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ��Ա����
		/// <summary>
		/// �����������������
		/// </summary>
		/// <returns></returns>
		public string GetNewApparatusNo()
		{
			string strNewNo = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsApparatus.GetNewApparatusNo.1",ref strSql) == -1) 
			{
				return string.Empty;
			}

			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					strNewNo = Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				this.Reader.Close();
				return strNewNo;            
			}
			this.Reader.Close();
			strNewNo = strNewNo.PadLeft(8,'0');
			
			return strNewNo;
		}
		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="thisApparatus">�������϶���</param>
		/// <returns>0 success -1 fail</returns>
		public int AddOpsApparatus(FS.HISFC.Models.Operation.OpsApparatus thisApparatus)
		{
			string strSql = string.Empty;			
			
			string strStatus = FS.FrameWork.Function.NConvert.ToInt32(thisApparatus.IsValid).ToString();
			if(this.Sql.GetSql("Operator.OpsApparatus.AddOpsApparatus.1",ref strSql) == -1) 
			{
				return -1;
			}

			try
			{						
				strSql = string.Format(strSql,thisApparatus.ID.ToString(),thisApparatus.Name,thisApparatus.UserCode,	//3
					thisApparatus.SpellCode,thisApparatus.TradeMark,thisApparatus.AppaSource,thisApparatus.AppaModel,	//7
					thisApparatus.BuyDate.ToString(),thisApparatus.Price.ToString(),thisApparatus.Unit,			//10
					strStatus,thisApparatus.Producer,thisApparatus.Saler,thisApparatus.Level,thisApparatus.Remark,	//15
					thisApparatus.User.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			
			if(this.ExecNoQuery(strSql) == -1) 
			{
				return -1;
			}

			return 0;
		}
		/// <summary>
		/// �޸�����������Ϣ
		/// </summary>
		/// <param name="thisApparatus">�������϶���</param>
		/// <returns>0 success -1 fail</returns>
		public int UpdateOpsApparatus(FS.HISFC.Models.Operation.OpsApparatus thisApparatus)
		{
			string strSql = string.Empty;
			string strStatus = FS.FrameWork.Function.NConvert.ToInt32(thisApparatus.IsValid).ToString();
			if(this.Sql.GetSql("Operator.OpsApparatus.UpdateOpsApparatus.1",ref strSql) == -1) 
			{
				return -1;			
			}
			
			try
			{								
				strSql = string.Format(strSql,thisApparatus.ID.ToString(),thisApparatus.Name,thisApparatus.UserCode,	//3
					thisApparatus.SpellCode,thisApparatus.TradeMark,thisApparatus.AppaSource,thisApparatus.AppaModel,	//7
					thisApparatus.BuyDate.ToString(),thisApparatus.Price.ToString(),thisApparatus.Unit,			//10
					strStatus,thisApparatus.Producer,thisApparatus.Saler,thisApparatus.Level,thisApparatus.Remark,	//15
					thisApparatus.User.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
		
			if(this.ExecNoQuery(strSql) == -1) 
			{
				return -1;
			}

			return 0;
		}
		/// <summary>
		/// ɾ����������
		/// </summary>
		/// <param name="ApparatusId">�������ϱ��</param>
		/// <returns>0 success -1 fail</returns>
		public int DelOpsApparatus(string ApparatusId)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsApparatus.DelOpsApparatus.1",ref strSql) == -1) 
			{
				return -1;
			}
			
			try
			{	
				strSql = string.Format(strSql,ApparatusId);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;			
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ��ȡ���������б�
		/// </summary>
		/// <returns>�������϶�������</returns>
		public ArrayList GetOpsApparatus()
		{
			ArrayList OpsApparatusAl = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsApparatus.GetOpsApparatus.1",ref strSql) == -1) 
			{
				// TODO: should return null
				return OpsApparatusAl;
			}
			
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsApparatus thisApparatus = new FS.HISFC.Models.Operation.OpsApparatus();
					
					thisApparatus.ID = Reader[0].ToString();			//�������ϴ���
					thisApparatus.Name = Reader[1].ToString();			//������������										
					thisApparatus.UserCode = Reader[2].ToString();		//������					
					thisApparatus.SpellCode = Reader[3].ToString();		//ƴ����					
					thisApparatus.TradeMark = Reader[4].ToString();		//Ʒ��					
					thisApparatus.AppaSource = Reader[5].ToString();	//���� 					
					thisApparatus.AppaModel = Reader[6].ToString();		//�ͺ�					
					thisApparatus.BuyDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[7].ToString());		//��������					
					thisApparatus.Price =  System.Convert.ToDecimal(Reader[8].ToString());					//�۸�					
					thisApparatus.Unit = Reader[9].ToString();		//��λ					
					string strStatus = Reader[10].ToString();			//1����/0δ�� 
					thisApparatus.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(strStatus); 					
					thisApparatus.Producer = Reader[11].ToString();		//��������					
					thisApparatus.Saler = Reader[12].ToString();		//������					
					thisApparatus.Level = Reader[13].ToString();		//1����2��ͨ					
					thisApparatus.Remark = Reader[14].ToString();		//��ע
					
					OpsApparatusAl.Add(thisApparatus);
				}
			}
			catch(Exception ex)
			{
				this.Err="�������������Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				this.Reader.Close();
				return OpsApparatusAl;
			}
			this.Reader.Close();
			return OpsApparatusAl;
		}
		#endregion
	}
}
