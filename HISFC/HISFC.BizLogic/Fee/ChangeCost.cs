using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// ChangeCost ��ժҪ˵����
	/// </summary>
	public class ChangeCost  :FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public ChangeCost()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		CHANGE_CODE     VARCHAR2(14)          'ROOT'  ת��ҽ�ƻ�������                                              
//		FEE_CODE        VARCHAR2(3)                   ��С���ô��� ���Ϊ all ��Ϊȫ������                          
//		CHANGE_TYPE     VARCHAR2(1)                   ת������,1 ����ת�룬2 סԺת�� 3 ��Ժת��                    
//		CLINIC_NO       VARCHAR2(14)                  ҽ����ˮ�� 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="changeType"></param>
		/// <param name="ClinicNo"></param>
		/// <returns></returns>
		public ArrayList  GetChangeCost(string changeType,string ClinicNo)
		{
			ArrayList List=null;
			string strSql="";
			//SELECT TOT_COST,OWN_COST,PAY_COST,PUB_COST,ECO_COST ,CHANGE_TYPE ,FEE_CODE  FROM FIN_COM_CHANGECOST WHERE PARENT_CODE ='[��������]' and CURRENT_CODE ='[��������]'AND change_code ='[��������]'  AND CLINIC_NO ='{0}' AND CHANGE_TYPE ='{1}';
			FS.HISFC.Models.Fee.TransferFee  cost = null;
			if (this.Sql.GetCommonSql("Management.Fee.GetChangeCost",ref strSql)==-1) return null;
			try
			{
				strSql = string.Format(strSql,ClinicNo,changeType);
				this.ExecQuery(strSql);
				List = new ArrayList();
				while(this.Reader.Read())
				{
					cost = new FS.HISFC.Models.Fee.TransferFee();
					try
					{
						cost.FT.TotCost =Convert.ToDecimal(Reader[0]);
					}
					catch(Exception ){cost.FT.TotCost = 0;}
					try
					{
						cost.FT.OwnCost =Convert.ToDecimal(Reader[1]);
					}
					catch(Exception ){cost.FT.OwnCost = 0;}
					try
					{
						cost.FT.PayCost =Convert.ToDecimal(Reader[2]);
					}
					catch(Exception ){cost.FT.PayCost=0;}
					try
					{
						cost.FT.PubCost =Convert.ToDecimal(Reader[3]);
					}
					catch(Exception ){cost.FT.PubCost=0;}
					try
					{
						cost.FT.RebateCost =Convert.ToDecimal(Reader[4]);
					}
					catch(Exception ){cost.FT.RebateCost = 0;}
					try
					{
						cost.Type.ID =Reader[5].ToString();
					}
					catch(Exception ee){ string Error = ee.Message;}
					try
					{
						cost.MinFee.ID = Reader[6].ToString();
					}
					catch(Exception ee){string Error = ee.Message;}
					List.Add(cost);
					cost =null;
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return List;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateChangeCost(FS.HISFC.Models.Fee.TransferFee  info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.TransferFee.UpdateChangeCost",ref strSql)==-1)return -1;
			try
			{
				string OperCode =this.Operator.ID;
				//update fin_com_changecost set tot_cost ={0}, own_cost ={1}, pay_cost ={2}, pub_cost ={3},eco_cost ={4}  where clinic_no ='{5}' and change_type ='{6}'  and fee_code ='{7}'and  CHANGE_CODE ='[��������]' AND  parent_code = '[��������]' AND  current_code ='[��������]' 
				strSql = string.Format(strSql,info.FT.TotCost ,info.FT.OwnCost,info.FT.PayCost,info.FT.PubCost,info.FT.RebateCost,info.ID,info.Type.ID,info.MinFee.ID);
			}
			catch(Exception ee)
			{
				string Error = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertChangeCost(FS.HISFC.Models.Fee.TransferFee info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.TransferFee.InsertChangeCost",ref strSql)==-1)return -1;
			try
			{
				string OperCode =this.Operator.ID;
				//insert into fin_com_changecost values ('[��������]', '[��������]', '[��������]', '{0}', '{1}',  '{2}', '{3}','{4}', '{5}', {6}, {7}, {8},   {9}, {10}, '{11}', SYSDATE,'','{12}','{13}', SYSDATE)
				
				strSql = string.Format(strSql,info.MinFee.ID,info.Type.ID,info.ID,info.Name,info.Pact.PayKind.ID, info.Pact.ID,info.FT.TotCost,info.FT.OwnCost,info.FT.PayCost,info.FT.PubCost,info.FT.RebateCost,OperCode,info.BalanceState,OperCode);
			}
			catch(Exception ee)
			{
				string Error = ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteChangeCost(FS.HISFC.Models.Fee.TransferFee info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.TransferFee.DeleteChangeCost",ref strSql)==-1)return -1;
			try
			{
				string OperCode =this.Operator.ID;
				//DELETE  FIN_COM_CHANGECOST  WHERE CHANGE_CODE ='[��������]' AND  FEE_CODE ='{0}' AND CLINIC_NO  ='{1}'AND  parent_code = '[��������]' AND  current_code ='[��������]' 
				strSql = string.Format(strSql,info.MinFee.ID,info.ID);
			}
			catch(Exception ee)
			{
				string Error = ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public bool UpdateOrInsertChangeCost(FS.HISFC.Models.Fee.TransferFee info )
		{
			bool Result= true;;
			int temp =0;
			temp =IsAlreadyInChangeCost(info.MinFee.ID,info.Type.ID,info.ID );
			if(temp>0)
			{
				//����
				if(UpdateChangeCost(info)<=0)
				{
					Result =false;//����ʧ��
				}
			}
			else if(temp ==0)
			{
				//����
				if(InsertChangeCost(info)<=0)
				{
					Result = false; //����ʧ��
				}
			}
			else
			{
				//�����ˡ�
				Result = false;
			}
			return Result ;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="feecode"></param>
		/// <param name="changeType"></param>
		/// <param name="clinicNo"></param>
		/// <returns></returns>
		public int IsAlreadyInChangeCost(string feecode ,string changeType ,string clinicNo)
		{
			string strSql = "";
			//select * from fin_com_changecost where FEE_CODE  ='{0}'and  CLINIC_NO ='{1}' and CHANGE_TYPE ='{2}'  and change_code ='[��������]' and PARENT_CODE ='[��������]' and CURRENT_CODE ='[��������]'
			if (this.Sql.GetCommonSql("Fee.TransferFee.IsAlreadyInChangeCost",ref strSql)==-1)return -1;
			try
			{
				strSql= string.Format(strSql,feecode,changeType,clinicNo);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			this.ExecQuery(strSql);
			while(this.Reader.Read())
			{
				if(Reader[0]!=DBNull.Value) 
				{
					return 1;
				}
				else 
				{
					return 0;
				}
			}
			return 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Patinfo"></param>
		/// <returns></returns>
		public int UpdateInmaininfo(FS.HISFC.Models.RADT.PatientInfo Patinfo )
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.TransferFee.UpdateInmaininfo",ref strSql)==-1)return -1;
			try
			{
				//update fin_ipr_inmaininfo set CHANGE_TOTCOST ={0},own_cost ={1},pay_cost ={2},pub_cost={3},eco_cost={[4} ,CHANGE_PREPAYCOST  ={5} where  PARENT_CODE ='[��������]' and current_code ='[��������]' and  patient_no = '{6}'
				strSql = string.Format(strSql,Patinfo.FT.TotCost ,Patinfo.FT.OwnCost,Patinfo.FT.PayCost,Patinfo.FT.PubCost,Patinfo.FT.RebateCost,Patinfo.FT.PrepayCost,Patinfo.PID.PatientNO);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ClinicNo"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.TransferFee   SumTotCost(string ClinicNo)
		{

			FS.HISFC.Models.Fee.TransferFee cost = null;
			string strSql = "";
			//select sum( TOT_COST ),sum(OWN_COST),sum(PAY_COST),sum(PUB_COST),sum(ECO_COST) from fin_com_changecost  where clinic_no ='{0}'  and change_code ='[��������]' and PARENT_CODE ='[��������]' and CURRENT_CODE ='[��������]'AND BALANCE_STATE ='0' 
			if (this.Sql.GetCommonSql("Fee.TransferFee.SumTotCost",ref strSql)==-1)return null;
			try
			{
				strSql= string.Format(strSql,ClinicNo);

				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					cost = new FS.HISFC.Models.Fee.TransferFee();
					if(Reader[0]!=DBNull.Value)
					{
						cost.FT.TotCost  = Convert.ToDecimal(Reader[0]);
					}
					if(Reader[1]!=DBNull.Value)
					{
						cost.FT.OwnCost  = Convert.ToDecimal(Reader[1]);
					}
					if(Reader[2]!=DBNull.Value)
					{
						cost.FT.PayCost  = Convert.ToDecimal(Reader[2]);
					}
					if(Reader[3]!=DBNull.Value)
					{
						cost.FT.PubCost  = Convert.ToDecimal(Reader[3]);
					}
					if(Reader[4]!=DBNull.Value)
					{
						cost.FT.RebateCost  = Convert.ToDecimal(Reader[4]);
					}
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return cost;
		}
	}
}
