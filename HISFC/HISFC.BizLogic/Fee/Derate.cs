using System;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// Derate ��ժҪ˵����
	/// </summary>
	public class Derate :FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public Derate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}
		/// <summary>
        /// �õ�����ķ��ü���� ����С���÷���{BD300517-D927-43c0-A1D3-8FB99BD10298}
		/// </summary>
		/// <param name="clinicNo"></param>
		/// <returns></returns>
        //public ArrayList GetFeeCodeAndDerateCost(string clinicNo)
        //{
        //    //SELECT FEE_CODE ,SUM(DERATE_COST) FROM FIN_COM_DERATE  WHERE CLINIC_NO ='{0}' AND  PARENT_CODE = '[��������]'  AND CURRENT_CODE ='[��������]' GROUP BY  FEE_CODE
        //    ArrayList List = null;
        //    string  strSql="";
        //    if (this.Sql.GetCommonSql("Management.Fee.GetFeeCodeAndDerateCost1",ref strSql)==-1) return null;
        //    try
        //    {
        //        strSql = string.Format(strSql,clinicNo);
        //        this.ExecQuery(strSql);
        //        FS.HISFC.Models.Fee.Rate info = null;
        //        List = new ArrayList();
        //        while(this.Reader.Read())
        //        {
        //            info = new FS.HISFC.Models.Fee.Rate();
        //            if(Reader[0]!=null)
        //            {
        //                info.FeeCode =Reader[0].ToString(); //סԺ��ˮ��
        //            }
        //            try
        //            {
        //                info.derate_Cost = Convert.ToDecimal(Reader[1]);
        //            }
        //            catch(Exception ee)
        //            {
        //                string Error = ee.Message;
        //                info.derate_Cost = Convert.ToDecimal(0);
        //            }
        //            List.Add(info);
        //            info = null;
        //        }
        //        this.Reader.Close();
        //    }
        //    catch(Exception ee)
        //    {
        //        string Error = ee.Message;
        //    }
        //    return List;
        //}
        /// <summary>
        /// �õ�����ķ��ü���� ����С���÷���{BD300517-D927-43c0-A1D3-8FB99BD10298}
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public ArrayList GetFeeCodeAndDerateCost(string clinicNo, string balanceNO)
        {
            //SELECT FEE_CODE ,SUM(DERATE_COST) FROM FIN_COM_DERATE  WHERE CLINIC_NO ='{0}' AND  PARENT_CODE = '[��������]'  AND CURRENT_CODE ='[��������]' GROUP BY  FEE_CODE
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetCommonSql("Management.Fee.GetFeeCodeAndDerateCost1", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, clinicNo, balanceNO);
                this.ExecQuery(strSql);
                //{8D6068F9-058A-4a25-976A-FB4C68834FA9}
                //FS.HISFC.Models.Fee.Rate info = null;
                FS.HISFC.Models.Fee.Inpatient.FeeInfo info;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    //{8D6068F9-058A-4a25-976A-FB4C68834FA9}
                    //info = new FS.HISFC.Models.Fee.Rate();
                    info = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    if (Reader[0] != null)
                    {
                        //{8D6068F9-058A-4a25-976A-FB4C68834FA9}
                        //info.FeeCode =Reader[0].ToString(); //סԺ��ˮ��
                        info.Item.MinFee.ID = Reader[0].ToString(); //
                    }
                    try
                    {
                        //{8D6068F9-058A-4a25-976A-FB4C68834FA9}
                        //info.derate_Cost = Convert.ToDecimal(Reader[1]);
                        info.FT.OwnCost = Convert.ToDecimal(Reader[1]);
                        info.FT.TotCost = Convert.ToDecimal(Reader[1]);
                    }
                    catch (Exception ee)
                    {
                        string Error = ee.Message;
                        //{8D6068F9-058A-4a25-976A-FB4C68834FA9}
                        //info.derate_Cost = Convert.ToDecimal(0);
                        info.FT.TotCost = Convert.ToDecimal(0);
                        info.FT.OwnCost = Convert.ToDecimal(0);
                    }
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
            }
            return List;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="clinicNo">ҽ����ˮ��</param>
		/// <returns></returns>
		public ArrayList GetDerate(string clinicNo)
		{
			ArrayList List = null;
			string  strSql="";
			if (this.Sql.GetCommonSql("Management.Fee.GetDerate",ref strSql)==-1) return null;
			try
			{
				//SELECT CLINIC_NO ,HAPPEN_NO,DERATE_KIND,RECIPE_NO,SEQUENCE_NO,DERATE_TYPE,FEE_CODE,DERATE_COST,DERATE_CAUSE,CONFIRM_OPERCODE,CONFIRM_NAME,DEPT_CODE ,BALANCE_NO ,BALANCE_STATE ,OPER_CODE ,OPER_DATE  FROM FIN_COM_DERATE WHERE CLINIC_NO  = '{0}' and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
				strSql = string.Format(strSql,clinicNo);
				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Rate info = null;
				List = new ArrayList();
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Rate();
					if(Reader[0]!=DBNull.Value )
					{
						info.clinicNo =Reader[0].ToString(); //סԺ��ˮ��
					}
					if(Reader[1]!=DBNull.Value )
					{
						try
						{
							info.happenNo =Convert.ToInt32(Reader[1]); //�������
						}
						catch(Exception ee)
						{
							this.Err = ee.Message;
						}
					}
					if(Reader[2]!=DBNull.Value )
					{
						info.derateKind =Reader[2].ToString(); //��������
					}
					if(Reader[3]!=DBNull.Value )
					{
						info.recipeNo =Reader[3].ToString();//������
					}			
					if(Reader[4]!=DBNull.Value )
					{
						info.sequenceNo =Convert.ToInt32(Reader[4]); //��������Ŀ��ˮ��
					}
					else
					{
						info.sequenceNo =0;
					}
					if(Reader[5]!=DBNull.Value )
					{
						info.derateType =Reader[5].ToString(); //��������
					}
					if(Reader[6]!=DBNull.Value )
					{
						info.FeeCode =Reader[6].ToString();  //��С����
					}
					if(Reader[7]!=DBNull.Value )
					{
						try
						{
							info.derate_Cost =Convert.ToDecimal(Reader[7]); //������
						}
						catch(Exception ee)
						{
							string Error =ee.Message;
						}
					}
					if(Reader[8]!=DBNull.Value )
					{
						info.derate_cause =Reader[8].ToString(); //����ԭ��
					}
					if(Reader[9]!=DBNull.Value )
					{
						info.confirmOpercode =Reader[9].ToString(); //��׼��
					}
					if(Reader[10]!=DBNull.Value )
					{
						info.confirmName =Reader[10].ToString(); // ��׼�˱���
					}
					if(Reader[11]!=DBNull.Value )
					{
						info.deptCode =Reader[11].ToString(); //  ����
					}
					if(Reader[12]!=DBNull.Value )
					{
						info.balanceState =Reader[12].ToString(); // ����״̬
					}
					if(Reader[13]!=DBNull.Value )
					{
						info.opercode =Reader[13].ToString(); // ����״̬
					}
					if(Reader[14]!=DBNull.Value )
					{
						info.operdate =Convert.ToDateTime(Reader[14]);
					}
					List.Add(info);
					info = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return List;
		}
		/// <summary>
		/// ���� ���ü���� fin_com_derate һ����¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public  int UpdateDerate(FS.HISFC.Models.Fee.Rate info)
		{
			string strSql = "";
			//UPDATE FIN_COM_DERATE  SET DERATE_KIND  = '{2}' ,RECIPE_NO ='{3}' ,SEQUENCE_NO='{4}',DERATE_TYPE ='{5}',FEE_CODE  ='{6}',DERATE_COST ='{7}',CONFIRM_OPERCODE ='{8}', CONFIRM_NAME='{9}',DEPT_CODE ='{10}' ,OPER_CODE ='{11}',OPER_DATE = sysdate WHERE  CLINIC_NO= '{0}' AND  HAPPEN_NO ='{1}'AND  PARENT_CODE = '[��������]' AND  CURRENT_CODE ='[��������]' 
			if(this.Sql.GetCommonSql("Management.Fee.UpdateDerate",ref strSql)==-1 ) return -1;
			try
			{
				string  OperCode = this.Operator.ID;
				strSql = string.Format(strSql,info.clinicNo,info.happenNo,info.derateKind,info.recipeNo,info.sequenceNo,info.derateType,info.FeeCode,info.derate_Cost,info.confirmOpercode,info.confirmName,info.deptCode,OperCode);
			}
			catch(Exception ee)
			{
				this.Err =ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ɾ�� ���ü���� fin_com_derateһ����¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteDerate(FS.HISFC.Models.Fee.Rate info)
		{
			string strSql = "";
			// DELETE FIN_COM_DERATE  WHERE  CLINIC_NO ='{0}',HAPPEN_NO ='{1}'  and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
			if(this.Sql.GetCommonSql("Management.Fee.DeleteDerate",ref strSql)==-1 ) return -1;
			try
			{
				strSql = string.Format(strSql,info.clinicNo,info.happenNo);
			}
			catch(Exception ee)
			{
				this.Err =ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ���� ���ü���� fin_com_derate һ���µļ�¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertDerate(FS.HISFC.Models.Fee.Rate info)
		{
			string strSql = "";
			//INSERT  INTO  FIN_COM_DERATE  ( PARENT_CODE ,CURRENT_CODE,CLINIC_NO ,HAPPEN_NO ,DERATE_KIND ,RECIPE_NO ,SEQUENCE_NO ,DERATE_TYPE ,FEE_CODE, DERATE_COST,DERATE_CAUSE,CONFIRM_OPERCODE,CONFIRM_NAME,DEPT_CODE,OPER_CODE ,OPER_DATE  ) VALUES ('[��������]','[��������]','{0}',(select max(happen_no)+1 from fin_com_derate where parent_code ='[��������]' and current_code ='[��������]'),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',sysdate)
			if(this.Sql.GetCommonSql("Management.Fee.InsertDerate",ref strSql)==-1 ) return -1;
			try
			{
				string  OperCode = this.Operator.ID;
				int sequenceNo =0;
				try
				{
					sequenceNo =Convert.ToInt32(info.sequenceNo);
				}
				catch(Exception )
				{
					sequenceNo = 0;
				}
				info.happenNo =  GetHappenNO(info.clinicNo);
				strSql  =  string.Format(strSql,info.clinicNo,info.happenNo,info.derateKind,info.recipeNo,sequenceNo,info.derateType,info.FeeCode,info.derate_Cost,info.derate_cause,info.confirmOpercode,info.confirmName,info.deptCode,info.balanceState,OperCode);
			}
			catch(Exception ee)
			{
				this.Err =ee.Message;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ����סԺ��ˮ�źͽ�����Ų�Ѷ ���ü����¼
		/// </summary>
		/// <param name="Clinic"></param>
		/// <param name="BalanceNo"></param>
		/// <returns></returns>
        public ArrayList GetDerateByClinicAndBalance(string Clinic, int BalanceNo)
        {
            ArrayList List = null;
            string strSql = "";
            //SELECT CLINIC_NO ,HAPPEN_NO,DERATE_KIND,RECIPE_NO,SEQUENCE_NO,DERATE_TYPE,FEE_CODE,DERATE_COST,DERATE_CAUSE,CONFIRM_OPERCODE,CONFIRM_NAME,DEPT_CODE ,BALANCE_NO ,BALANCE_STATE  FROM FIN_COM_DERATE WHERE CLINIC_NO  = '{0}'  and  BALANCE_NO = '{1}' and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' 
            if (this.Sql.GetCommonSql("Management.Fee.GetDerateByClinicAndBalance", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Clinic, BalanceNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.Fee.Rate info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Fee.Rate();
                    info.clinicNo = Reader[0].ToString(); //סԺ��ˮ��
                    info.happenNo = Convert.ToInt32(Reader[1]); //�������
                    info.derateKind = Reader[2].ToString(); //��������
                    info.recipeNo = Reader[3].ToString();//������
                    info.sequenceNo = Convert.ToInt32(Reader[4]); //��������Ŀ��ˮ��
                    info.sequenceNo = 0;
                    info.derateType = Reader[5].ToString(); //��������
                    info.FeeCode = Reader[6].ToString();  //��С����
                    info.derate_Cost = Convert.ToDecimal(Reader[7]); //������
                    info.derate_cause = Reader[8].ToString(); //����ԭ��
                    info.confirmOpercode = Reader[9].ToString(); //��׼��
                    info.confirmName = Reader[10].ToString(); // ��׼�˱���
                    info.deptCode = Reader[11].ToString(); //  ����
                    info.BalanceNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[12].ToString());//�����
                    info.balanceState = Reader[13].ToString(); // ����״̬
                    info.opercode = Reader[14].ToString(); // ����Ա
                    info.operdate = Convert.ToDateTime(Reader[15]);// ����ʱ��
                    List.Add(info);
                }
                this.Reader.Close(); //�ر�reader
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }
		/// <summary>
		/// �õ���סԺ��ˮ���õ�������Ŀ�б�
		/// </summary>
		/// <param name="ClinicNo"></param>
		/// <returns></returns>
		public ArrayList GetItemList(string ClinicNo)
		{
			ArrayList List = null;
			try
			{
				//select recipe_no,sequence_no ,drug_name , own_cost from fin_ipb_medicinelist where inpatient_no ='{0}' and parent_code ='[��������]' and current_code ='[��������]' union select recipe_no , sequence_no , item_name, own_cost  from fin_ipb_itemlist  where inpatient_no ='{0}' and parent_code ='[��������]' and current_code ='[��������]'

				string strSql = "";
				if (this.Sql.GetCommonSql("Management.Fee.zjyGetItemList",ref strSql)==-1) return null;
				List = new ArrayList();
				FS.HISFC.Models.Fee.Item.Undrug ItemInfo = null;
				strSql = string.Format(strSql ,ClinicNo);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					//
					ItemInfo = new FS.HISFC.Models.Fee.Item.Undrug();
					if(Reader[0]!=null)
					{
						//������
						ItemInfo.ID  = Reader[0].ToString();
					}
					if(Reader[1]!=null)
					{
						//��������ˮ��
						ItemInfo.GBCode =Convert.ToString( Reader[1]);
					}
					if(Reader[2]!=null)
					{
						//ҩƷ/��ҩƷ����
						ItemInfo.Name = Reader[2].ToString();
					}
					if(Reader[3]!=null)
					{
						//�Ը����
						ItemInfo.Price = Convert.ToDecimal(Reader[3]);
					}
					else
					{
						ItemInfo.Price=0;
					}
					List.Add(ItemInfo);
					ItemInfo= null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
			}
			return List;
		}
		/// <summary>
		/// ����סԺ�ź���Ŀ���Ƶõ������źʹ�����Ŀ��ˮ��
		/// </summary>
		/// <param name="ClinicNo"></param>
		/// <param name="ItemName"></param>
		/// <returns></returns>
		public ArrayList GetItemListByClinicAndFeeName(string ClinicNo,string ItemName)
		{
			ArrayList List = null;
			try
			{
				//select recipe_no,sequence_no ,drug_name , own_cost from fin_ipb_medicinelist where inpatient_no ='{0}' and parent_code ='[��������]' and current_code ='[��������]' union select recipe_no , sequence_no , item_name, own_cost  from fin_ipb_itemlist  where inpatient_no ='{0}' and parent_code ='[��������]' and current_code ='[��������]'

				string strSql = "";
				if (this.Sql.GetCommonSql("Management.Fee.GetItemListByClinicAndFeeName",ref strSql)==-1) return null;
				List = new ArrayList();
				FS.HISFC.Models.Fee.Item.Undrug ItemInfo = null;
				strSql = string.Format(strSql ,ClinicNo,ItemName);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					//
					ItemInfo = new FS.HISFC.Models.Fee.Item.Undrug();
					if(Reader[0]!=null)
					{
						//������
						ItemInfo.ID  = Reader[0].ToString();
					}
					if(Reader[1]!=null)
					{
						//��������ˮ��
						ItemInfo.GBCode =Convert.ToString( Reader[1]);
					}
					if(Reader[2]!=null)
					{
						//ҩƷ/��ҩƷ����
						ItemInfo.Name = Reader[2].ToString();
					}
					if(Reader[3]!=null)
					{
						//�Ը����
						ItemInfo.Price = Convert.ToDecimal(Reader[3]);
					}
					else
					{
						ItemInfo.Price=0;
					}
					List.Add(ItemInfo);
					ItemInfo= null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
			}
			return List;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="clinic"></param>
		/// <returns></returns>
		public int IsBalance(string clinic )
		{
			//�ж��Ƿ��Ѿ����������
			int Result =0;
			try
			{
				string  strSql="";
				if (this.Sql.GetCommonSql("Management.Fee.IsBalance",ref strSql)==-1) return -1;
				// SELECT DISTINCT  INPATIENT_NO FROM  fin_ipr_inmaininfo where INPATIENT_NO ='{0}' and IN_STATE IN( 'R', 'I', 'B', 'P')  and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
				strSql = string.Format(strSql,clinic);
				Result =this.ExecQuery(strSql);
				
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				Result =-1;
			}
			return Result ;
		}
		/// <summary>
		/// ����סԺ�� �õ����һ�ε�סԺ��ˮ��
		/// </summary>
		/// <param name="patientno"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject  GetLastClinicNO(string patientno )
		{
			FS.FrameWork.Models.NeuObject obj =null;
			string strSql ="";
			//select inpatient_no ,dept_code  from fin_ipr_inmaininfo where inpatient_no =(select Max(inpatient_no) from fin_ipr_inmaininfo where inpatient_no = '{0}' and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]') and PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]'
			if (this.Sql.GetCommonSql("Management.Fee.GetLastClinicNO",ref strSql)==-1) return null;
			strSql = string.Format(strSql,patientno);
			this.ExecQuery(strSql);
			while(this.Reader.Read())
			{
				obj = new FS.FrameWork.Models.NeuObject();
				try
				{
					if(Reader[0]!=DBNull.Value)
					{
						obj.ID =  Reader[0].ToString();     //סԺ��ˮ��                 
					}
					if(Reader[1]!=DBNull.Value)
					{
						obj.Name = Reader[1].ToString(); //���Ҵ���
					}
				}
				catch(Exception ee)
				{
					this.Err = ee.Message;
				}
			}
			this.Reader.Close();
			return obj;
		}

		/// <summary>
		/// ���ݼ����ܶ� ������С��Ŀ�ļ�����
		/// </summary>
		/// <param name="clinicNo"></param>
		/// <param name="DerateCost">�����ܶ�</param>
		/// <param name="DerateTotal">��̯���ܷ���</param>
		/// <returns></returns>
		public ArrayList GetFeeCodeAndDerateCost(string clinicNo,decimal DerateCost,decimal DerateTotal)
		{
			//select fee_code,sum(own_cost) from fin_ipb_feeinfo where parent_code ='[��������]' and current_code ='[��������]' and inpatient_no ='{0}' and balance_state ='0' group by fee_code
			ArrayList costList = null;
			string  strSql="";
			if (this.Sql.GetCommonSql("Management.Fee.GetFeeCodeAndDerateCost2",ref strSql)==-1) return null;
			try
			{
				ArrayList List = null;
				strSql = string.Format(strSql,clinicNo);
				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Inpatient.FeeInfo info = null;
				List = new ArrayList();
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
					info.Item.MinFee.ID = Reader[0].ToString();
					info.FT.OwnCost = Convert.ToDecimal(Reader[1]);
					List.Add(info);
					info= null;
				}
				this.Reader.Close();
				costList = CalculateDerateCost(DerateCost,List,DerateTotal);
			}
			catch(Exception ee)
			{
				//��������null
				this.Err = ee.Message;
				costList =null;
			}
			return costList;
		}
/// <summary>
/// ȷ�еļ�����С��Ŀ�ļ�����
/// </summary>
/// <param name="DerateCost"></param>
/// <param name="List"></param>
/// <param name="DerateTotal"></param>
/// <returns></returns>
		private ArrayList  CalculateDerateCost(decimal DerateCost,ArrayList List ,decimal DerateTotal)
		{
			ArrayList costList =null;
			try
			{
				decimal costSum =0;
				if(List!=null&&DerateCost>=0)
				{
					costList = new ArrayList();
					if(List.Count==0)
					{
						return List;
					}
					FS.HISFC.Models.Fee.Inpatient.FeeInfo  FeeRate = null;
					if(List.Count >1)
					{
						for(int i=0;i<List.Count-1;i++)
						{
							FeeRate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo(); 
							FeeRate.Item.MinFee.ID =((FS.HISFC.Models.Fee.Inpatient.FeeInfo)List[i]).Item.MinFee.ID;
							decimal temp = ((FS.HISFC.Models.Fee.Inpatient.FeeInfo)List[i]).FT.OwnCost;
							FeeRate.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((temp /DerateTotal) * DerateCost,2);
							FeeRate.FT.OwnCost = FeeRate.FT.TotCost;
							costSum = costSum + FeeRate.FT.TotCost;
							costList.Add(FeeRate);
							FeeRate = null;
						}
						FeeRate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
						FeeRate.Item.MinFee.ID =((FS.HISFC.Models.Fee.Inpatient.FeeInfo)List[List.Count-1]).Item.MinFee.ID;
						FeeRate.FT.TotCost = DerateCost -costSum  ;
						FeeRate.FT.OwnCost = FeeRate.FT.TotCost;
						costList.Add(FeeRate);
						FeeRate = null;
					}
					else
					{
						FeeRate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                        //FeeRate.Item.ID = ((FS.HISFC.Models.Fee.Inpatient.FeeInfo)List[List.Count - 1]).Item.MinFee.ID;
                        //luzhp 2008-3-31������
                        //{372537A2-51BD-4113-A1CE-991C98391ECD}
						FeeRate.Item.MinFee.ID =((FS.HISFC.Models.Fee.Inpatient.FeeInfo)List[0]).Item.MinFee.ID;
						FeeRate.FT.TotCost = DerateCost ;
						FeeRate.FT.OwnCost = FeeRate.FT.TotCost;
						costList.Add(FeeRate);
					}
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				costList = null;
			}
			return costList ;
		}
		/// <summary>
		/// �õ��Էѵ��ܶ�
		/// </summary>
		/// <param name="ClinicNo"></param>
		/// <returns></returns>
		public decimal SelectFeeDerate(string ClinicNo)
		{
			//select sum(own_cost) from fin_ipb_feeinfo where parent_code ='[��������]' and current_code ='[��������]' and inpatient_no ='{0}' and balance_state ='0' group by fee_code
			decimal Result =0;
			string  strSql="";
			if (this.Sql.GetCommonSql("Management.Fee.SelectFeeDerate",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,ClinicNo);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					Result = Convert.ToDecimal(Reader[0]);
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return Result ;
		}

		/// <summary>
		/// ��ȡ�������
		/// </summary>
		/// <param name="ClinicNo"></param>
		/// <returns></returns>
		public int GetHappenNO(string ClinicNo)
		{
			int HappenNo =0;
			string  strSql="";
			if (this.Sql.GetCommonSql("Management.Fee.GetHappenNO",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,ClinicNo);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					HappenNo = Convert.ToInt32(Reader[0]);
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return HappenNo ;
        }

        #region ��������{BD300517-D927-43c0-A1D3-8FB99BD10298}

        /// <summary>
        /// ����סԺ��ˮ��ȡ�Ѿ��������ϸ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList GetDeratedDetail(string inpatientNO)
        {
            return this.QueryDerateInfoByIndex("Fee.Inpatient.Derate.QueryDeratedInfo.ClinicNO", inpatientNO);
        }

        /// <summary>
        /// ������ѯ
        /// </summary>
        /// <returns></returns>
        private string PatientQuerySelect()
        {
            string sql = string.Empty;
            if (Sql.GetSql("Fee.Inpatient.Derate.selectBase", ref sql) == -1)
            {
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ����where������ѯ��Ϣ
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryDerateInfoByIndex(string sqlIndex, params string[] args)
        {
            string selectSql = this.PatientQuerySelect();

            string whereSql = "";

            int returnValue = this.Sql.GetCommonSql(sqlIndex, ref whereSql);

            if (returnValue < 0)
            {
                this.Err = "��ѯ" + sqlIndex + "��Ӧ��sql���ʧ��";
                return null;
            }

            this.ExecQuery(selectSql + " " + whereSql, args);

            FS.HISFC.Models.Fee.DerateFee info = null;

            ArrayList al = new ArrayList();

            while (this.Reader.Read())
            {
                info = new FS.HISFC.Models.Fee.DerateFee();
                if (!this.Reader.IsDBNull(0))
                {
                    info.InpatientNO = Reader[0].ToString(); //סԺ��ˮ��
                }

                if (!this.Reader.IsDBNull(1))
                {
                    info.HappenNO = Convert.ToInt32(Reader[1].ToString()); //������� 
                }

                if (!this.Reader.IsDBNull(2))
                {
                    info.DerateKind.ID = Reader[2].ToString(); //��������
                }

                if (!this.Reader.IsDBNull(3))
                {
                    info.RecipeNO = Reader[3].ToString();//������
                }

                if (!this.Reader.IsDBNull(4))
                {
                    info.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32((Reader[4].ToString())); //��������Ŀ��ˮ��
                }

                if (!this.Reader.IsDBNull(5))
                {
                    info.DerateType.ID = Reader[5].ToString(); //��������
                }

                if (!this.Reader.IsDBNull(6))
                {
                    info.FeeCode = Reader[6].ToString();  //��С����
                }

                if (!this.Reader.IsDBNull(7))
                {
                    info.DerateCost = Convert.ToDecimal(Reader[7]); //������
                }

                if (!this.Reader.IsDBNull(8))
                {
                    info.DerateCause = Reader[8].ToString(); //����ԭ��
                }

                if (!this.Reader.IsDBNull(9))
                {
                    info.ConfirmOperator.ID = Reader[9].ToString(); //��׼�˱���
                }

                if (!this.Reader.IsDBNull(10))
                {
                    info.ConfirmOperator.Name = Reader[10].ToString(); // ��׼��
                }

                if (!this.Reader.IsDBNull(11))
                {
                    info.DerateOper.Dept.ID = Reader[11].ToString(); //  ����
                }

                if (!this.Reader.IsDBNull(12))
                {
                    info.BalanceNO = Convert.ToInt32(Reader[12].ToString()); // �������
                }

                if (!this.Reader.IsDBNull(13))//����״̬ 0:δ���㣻1:�ѽ���
                {
                    info.BalanceState = this.Reader[13].ToString(); //����״̬ 0:δ���㣻1:�ѽ���
                }

                if (!this.Reader.IsDBNull(14))//��Ʊ��
                {
                    info.InvoiceNO = Reader[14].ToString();
                }

                if (!this.Reader.IsDBNull(15))//�����˴���
                {
                    info.CancelDerateOper.ID = Reader[15].ToString();
                }

                if (!this.Reader.IsDBNull(16))//����ʱ��
                {
                    info.CancelDerateOper.OperTime = Convert.ToDateTime(Reader[16].ToString());
                }

                if (!this.Reader.IsDBNull(17))//����Ա
                {
                    info.DerateOper.ID = Reader[17].ToString();
                }

                if (!this.Reader.IsDBNull(18))//��������
                {
                    info.DerateOper.OperTime = Convert.ToDateTime(Reader[18]);
                }

                if (!this.Reader.IsDBNull(19))//��Ŀ����
                {
                    info.ItemCode = this.Reader[19].ToString();
                }

                if (!this.Reader.IsDBNull(20))//��Ŀ����
                {
                    info.ItemName = this.Reader[20].ToString();
                }

                if (!this.Reader.IsDBNull(21))//��С����
                {
                    info.FeeName = this.Reader[21].ToString();
                }
                if (!this.Reader.IsDBNull(22))
                {
                    info.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[22]);
                }
                if (!this.Reader.IsDBNull(23))
                {
                    info.DerateKind.Name = this.Reader[23].ToString();
                }
                if (!this.Reader.IsDBNull(24))
                {
                    info.DerateType.Name = this.Reader[24].ToString();
                }
                if (!this.Reader.IsDBNull(25))
                {
                    info.DerateOper.Dept.Name = this.Reader[25].ToString();
                }
                al.Add(info);
            }
            this.Reader.Close();
            return al;

        }

        /// <summary>
        /// ����ʵ�帳ֵ
        /// </summary>
        /// <param name="derateFeeObj"></param>
        /// <returns></returns>
        private string[] getArgs(FS.HISFC.Models.Fee.DerateFee derateFeeObj)
        {
            string[] strArgs = new string[] {
                   derateFeeObj.InpatientNO,//0}', --ҽ����ˮ��
                   derateFeeObj.HappenNO.ToString(),//{1}', --�������
                   derateFeeObj.DerateKind.ID,//{2}', --�������� 0 �ܶ� 1 ��С���� 2 ��Ŀ���� 3 �������ʹ�ã�����С���ã�
                   derateFeeObj.RecipeNO,//{3}', --������
                   derateFeeObj.SequenceNO.ToString(),//{4}', --��������Ŀ��ˮ��
                   derateFeeObj.DerateType.ID,//{5}', --��������
                   derateFeeObj.FeeCode,//{6}', --��С����
                   derateFeeObj.DerateCost.ToString(),//{7}', --������
                   derateFeeObj.DerateCause,//{8}', --����ԭ��
                   derateFeeObj.ConfirmOperator.ID,//{9}', --��׼��Ա������
                   derateFeeObj.ConfirmOperator.Name,//{10}', --��׼������
                   derateFeeObj.DerateOper.Dept.ID,//{11}', --���Ҵ���
                   derateFeeObj.BalanceNO.ToString(),//{12}', --�������
                   derateFeeObj.BalanceState,//{13}', --����״̬ 0:δ���㣻1:�ѽ���
                   derateFeeObj.InvoiceNO,//{14}', --��Ʊ��
                   derateFeeObj.CancelDerateOper.ID,//{15}', --�����˴���
                   derateFeeObj.CancelDerateOper.OperTime.ToString(),//{16}', --����ʱ��
                   derateFeeObj.DerateOper.ID,//{17}', --����Ա
                   derateFeeObj.DerateOper.OperTime.ToString(),//{18}', --��������
                   derateFeeObj.ItemCode,//{19}', --��Ŀ����
                   derateFeeObj.ItemName,//{20}', --��Ŀ����
                   derateFeeObj.FeeName,//'{21}', --��С����
                   (FS.FrameWork.Function.NConvert.ToInt32(derateFeeObj.IsValid)).ToString()

            };
            return strArgs;

        }

        /// <summary>
        /// ���»��������
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="derateList"></param>
        /// <returns></returns>
        private int InsertOrUpdateSingleTable(string sqlIndex, FS.HISFC.Models.Fee.DerateFee derateObj)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetCommonSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }
            string[] args = this.getArgs(derateObj);
            return this.ExecNoQuery(sql, args);

        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="derateList">����ʵ��</param>
        /// <returns></returns>
        public int InsertDerateFeeInfo(FS.HISFC.Models.Fee.DerateFee derateObj)
        {
            return this.InsertOrUpdateSingleTable("Fee.Inpatient.Derate.NewInsert", derateObj);

        }

        /// <summary>
        /// ���¼�����Ϣ
        /// </summary>
        /// <param name="derateList">����ʵ��</param>
        /// <returns></returns>
        public int UpdateDerateFeeInfo(FS.HISFC.Models.Fee.DerateFee derateObj)
        {
            return this.InsertOrUpdateSingleTable("Fee.Inpatient.Derate.NewUpdate", derateObj);
        }

        /// <summary>
        /// ���ݾ�סԺ��,��Ʊ��,��ѯ������Ϣ
        /// </summary>
        /// <param name="clinicNO">סԺ��</param>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns></returns>
        public ArrayList QueryDerateDetailByClinicNOAndInvoiceNO(string clinicNO, string invoiceNO)
        {
            return this.QueryDerateInfoByIndex("Fee.Inpatient.Derate.Where_1", clinicNO, invoiceNO);
        }

        /// <summary>
        /// ������ˮ��ȡ�����ܽ��
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public string GetTotDerateFeeByClinicNO(string clinicNO)
        {
            string strSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("Fee.Inpatient.Derate.Query1", ref strSql);

            if (returnValue < 0)
            {
                this.Err = "û���ҵ�Fee.Inpatient.Derate.Query1��Ӧ��sql���";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, clinicNO);
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                return null;
            }

            this.ExecQuery(strSql);

            FS.FrameWork.Models.NeuObject obj = null;

            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
            }
            this.Reader.Close();
            return obj.ID;
        }

        /// <summary>
        /// ��ѯ���ߵ��ܷ��ú��Ѽ�����ܷ���
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public System.Data.DataSet GetTotFeeAndDerateByInpatientNO(string inpatientNO)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Fee.Inpatient.Derate.QueryTotFee", ref ds, inpatientNO);
            return ds;
        }

        /// <summary>
        /// ��ѯ���ߵ���С���ú��Ѽ������С����
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public System.Data.DataSet GetMinFeeAndDerateByInpatientNO(string inpatientNO)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Fee.Inpatient.Derate.QueryMinFee", ref ds, inpatientNO);
            return ds;
        }

        /// <summary>
        /// ��ѯ���ߵķ�����ϸ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public System.Data.DataSet GetFeeDetailByInpatientNO(string inpatientNO)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Fee.Inpatient.Derate.QueryFeeDetail", ref ds, inpatientNO);
            return ds;
        }

        /// <summary>
        /// ��ѯ���ߵ��ܷ��ú��Ѽ�����ܷ���
        /// </summary>
        /// <param name="clinicNO">������ˮ��</param>
        /// <returns></returns>{39C593C5-8217-48cc-AED6-80B79244BDA5}

        public System.Data.DataSet GetTotDerateFee(string clinicNO)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Fee.Inpatient.Derate.QueryTotFee", ref ds, clinicNO);
            return ds;
        }

        #endregion
    }
}
