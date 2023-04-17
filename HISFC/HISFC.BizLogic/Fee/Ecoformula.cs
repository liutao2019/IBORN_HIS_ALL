using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// Ecoformula ��ժҪ˵����
	/// </summary>
	public class Ecoformula :  FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public Ecoformula()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//		ECO_FLAG       VARCHAR2(1)                     �Ż�׼���־ 0 ���� 1 �Ծ����¼     
		//		CLINIC_CODE    VARCHAR2(14)            'AAAA'  �����¼                             
		//		CARD_NO        VARCHAR2(10)                    ���￨��                             
		//		PACTCODE_FLAG  VARCHAR2(1)    Y                ��ͬ��λ                             
		//		ICDCODE_FLAG   VARCHAR2(1)    Y                �����ֱ�־                           
		//		DATE_FLAG      VARCHAR2(1)    Y                ʱ�α�־                             
		//		ECOREAL_FLAG   VARCHAR2(1)    Y                �Ż�ԭ���ϵ 0 ȡ����Ż� 1 ȡ���Ż� 
		//		SPECIL_FORMULA VARCHAR2(2000) Y                �������ʽ 

		/// <summary>
		/// ��ѯ�Ż��ײͱ������
		/// </summary>
		/// <param name="ecoflag"></param>
		/// <param name="clinic"></param>
		/// <returns></returns>
		public ArrayList GetEcoformulaInfo(string ecoflag,string clinic)
		{
			ArrayList List = new ArrayList();
			string strSql ="";
			//select ECO_FLAG ,CLINIC_CODE ,PACTCODE_FLAG ,ICDCODE_FLAG,DATE_FLAG ,ECOREAL_FLAG from  fin_com_ecoformula where PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' and ECO_FLAG ='{0}'  and   CLINIC_CODE = '{1}' 
			if(this.Sql.GetCommonSql("Management.Fee.GetEcoformulaInfo",ref strSql)==-1) return null;
			try
			{
				FS.HISFC.Models.Fee.Useless.EcoFormula info ;
				strSql = string.Format(strSql,ecoflag ,clinic);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Useless.EcoFormula();
					info.EcoFlag = Reader[0].ToString();
					info.ClinicCode = Reader[1].ToString();
					info.PactcodeFlag = Reader[2].ToString();
					info.IcdcodeFlag = Reader[3].ToString();
					info.DateFlag = Reader[4].ToString();
					info.EcorealFlag = Reader[5].ToString();
					List.Add(info);
					info = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				List = null;
			}

			return List;
		}
		/// <summary>
		/// 
		/// ��ѯ�Ż��ײͱ������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetEcoformulaAll()
		{
			ArrayList List = new ArrayList();
			string strSql ="";
			//select ECO_FLAG ,CLINIC_CODE ,PACTCODE_FLAG ,ICDCODE_FLAG,DATE_FLAG ,ECOREAL_FLAG from  fin_com_ecoformula where PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
			if(this.Sql.GetCommonSql("Management.Fee.GetEcoformulaAll",ref strSql)==-1) return null;
			try
			{
				FS.HISFC.Models.Fee.Useless.EcoFormula info ;
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Useless.EcoFormula();
					try
					{
						info.EcoFlag = Reader[0].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}
					try
					{
						info.ClinicCode = Reader[1].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}

					try
					{
						info.PactcodeFlag = Reader[2].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}
					try
					{
						info.IcdcodeFlag = Reader[3].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}
					try
					{
						info.DateFlag = Reader[4].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}
					try
					{
						info.EcorealFlag = Reader[5].ToString();
					}
					catch(Exception ee)
					{
						string Error  =ee.Message;
					}

					List.Add(info);
					info = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
				List = null;
			}

			return List;
		}

		/// <summary>
		/// �����Ż��ײͱ������
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateEcoformula(FS.HISFC.Models.Fee.Useless.EcoFormula info)
		{
			string strSql = "";
			//update fin_com_ecoformula set PACTCODE_FLAG= '{2}' ,ICDCODE_FLAG='{3}' ,DATE_FLAG='{4}',ECOREAL_FLAG='{5}',SPECIL_FORMULA='{6}'  where ECO_FLAG= '{0}' and CLINIC_CODE='{1}'and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' 
			if(this.Sql.GetCommonSql("Management.Fee.UpdateEcoformula",ref strSql)==-1 ) return -1;
			try
			{
				strSql = string.Format(strSql,info.EcoFlag,info.ClinicCode,info.PactcodeFlag,info.IcdcodeFlag,info.DateFlag,info.EcorealFlag);
			}
			catch(Exception ee)
			{
				this.Err =ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ɾ���Ż��ײͱ������
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteEcoformula(FS.HISFC.Models.Fee.Useless.EcoFormula info)
		{
			string strSql = "";
			// delete fin_com_ecoformula where ECO_FLAG ='{0}' and CLINIC_CODE ='{1}' and CARD_NO ='{2}' and  PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
			if (this.Sql.GetCommonSql("Management.Fee.DeleteEcoformula",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,info.EcoFlag,info.ClinicCode);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ���� �Ż��ײͱ������
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertEcoformula(FS.HISFC.Models.Fee.Useless.EcoFormula info)
		{
			string strSql = "";
			//insert into fin_com_ecoformula (PARENT_CODE,CURRENT_CODE,ECO_FLAG ,CLINIC_CODE ,PACTCODE_FLAG ,ICDCODE_FLAG,DATE_FLAG ,ECOREAL_FLAG,OPER_CODE,OPER_DATE ) values('[��������]','[��������]','{0}','{1}','{2}','{3}','{4}','{5}','{6}',,sysdate)
			if (this.Sql.GetCommonSql("Management.Fee.InsertEcoformula",ref strSql)==-1)return -1;
			try
			{
				string OperCode = this.Operator.ID;
				strSql = string.Format(strSql,info.EcoFlag,info.ClinicCode,info.PactcodeFlag,info.IcdcodeFlag,info.DateFlag,info.EcorealFlag,OperCode);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		private  int SelectDataByClinic_no(string Clinic_Code)
		{     
			string strSql ="";
			int Return = -1 ;
			//select CLINIC_CODE from fin_com_ecoformula  where parent_code ='[��������]'   and current_code='[��������]'   and CLINIC_CODE  = '{1}'  and icdcode_flag ='1' 
			if(this.Sql.GetCommonSql("Management.Fee.SelectDateByClinic_no",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,Clinic_Code);
				Return = this.ExecQuery(strSql);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				Return = -1;
			}
			return Return;
		}             
		/// <summary>
		///  �����SQL�������������-1,���û�и�סԺ�Ż���￨�ţ�����0 ������м�¼���򷵻� �Ϸⶥ�� 
		/// </summary>
		/// <param name="Clinic_Code">סԺ�Ż���￨�� </param>
		/// <param name="feeCode">��С���ñ���</param>
		/// <param name="dt">����</param>
		/// <returns></returns>
		public  decimal  GetCost(string Clinic_Code,string feeCode ,System.DateTime dt )
		{
			//
			decimal Result =0;
			try
			{
				int temp = SelectDataByClinic_no(Clinic_Code);
				if(temp>0)
				{
					string  IcdCode = GetIcdCode(Clinic_Code);
					if (IcdCode=="-1")
					{
						//
						return -1;
					}
					if(IcdCode =="")
					{
						return 0;
					}
					if(IcdCode!="")
					{
						Result =Convert.ToInt32( GetCostByIcode(feeCode, IcdCode, dt));
					}
				}
				else if(temp ==0)
				{
					Result = 0;
				}
				else
				{
					Result =-1;
				}

			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				Result =-1;
			}
			return Result;
		}
		/// <summary>
		/// �õ�icdcode ��������      
		/// </summary>
		/// <param name="Clinic_Code"></param>
		/// <returns></returns>
		private  string GetIcdCode(string Clinic_Code)
		{
			//SELECT icd_code FROM met_com_diagnose WHERE parent_code = '[��������]'  AND current_code = '[��������]'  AND inpatient_no = '{0}'   AND diag_kind = '17'  AND diag_flag = '0'
			string Icdcode = "";
			string strSql ="";
			if(this.Sql.GetCommonSql("Management.Fee.GetIcdCode",ref strSql)==-1) return "";
			try
			{
				strSql = string.Format(strSql,Clinic_Code);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					Icdcode = this.Reader[0].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message; 
				Icdcode =  "-1";
			}
			return Icdcode ;
		}
		private string GetCostByIcode(string feecode,string IcdCode,System.DateTime dt)
		{
			string Cost ="";
			string strSql ="";
			//select cost from fin_com_ecoicdfee where parent_code  ='[��������]' and current_code ='[��������]' and fee_code = '{0}'  and begin_date  < to_date('{2}','yyyy-mm-dd hh24:mi:ss')   and end_date > to_date('{2}','yyyy-mm-dd hh24:mi:ss')  and icd_code = '{1}'
			if(this.Sql.GetCommonSql("Management.Fee.GetCostByIcode",ref strSql)==-1) return "-1";
			try
			{
				strSql = string.Format(strSql,feecode,IcdCode,dt);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					Cost = this.Reader[0].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return   "-1";
			}
			return Cost;
		}
	}
}
