using System;
using  System.Collections;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using System.Data;
using FS.FrameWork.Function;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// FeeReport ��ժҪ˵����
	/// </summary>
	public class FeeReport:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// סԺ������
		/// </summary>
		public FeeReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		
		#region ��Լ��λ����
		/// <summary>
		/// ���סԺ��Լ���ߵ�ͳ�Ʊ�����Ϣ
		/// </summary>
		/// <param name="dtBegin">��ʼʱ��</param>
		/// <param name="dtEnd">����ʱ��</param>
		/// <returns></returns>
		public ArrayList GetInpatientPartInfo(DateTime dtBegin, DateTime dtEnd)
		{
			string strSql = null;

			if(this.Sql.GetCommonSql("Fee.Report.GetInpatientPartInfo.Select", ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}

			if(FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString()) < 0)
			{
				this.Err = "������ֵ����!";
				return null;
			}

			FS.FrameWork.Models.NeuObject obj = null;
			ArrayList al = new ArrayList();
			try
			{
				this.ExecQuery(strSql);

				while(this.Reader.Read())
				{
					obj = new NeuObject();

					obj.User01 = Reader[0].ToString();
					obj.User02 = Reader[1].ToString();
					obj.User03 = Reader[2].ToString();

					al.Add(obj);
				}
				this.Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}
				return null;
			}
		}

		#endregion
		
		/// <summary>
		/// ��ȡ�ս���
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="OperID">����Ա��ʶ</param>
		/// <returns></returns>
		public string GetPrepayCost(string BeginDate,string EndDate,string OperID)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CA'
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCost", ref strSQL ) == -1  )
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ��ȡ�ս����ֽ𲿷�
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayCostByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql����			
	
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCostByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ��ȡ�ս���֧Ʊ
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="OperID">����Ա��ʶ</param>
		/// <returns></returns>
		public string GetPrepayCheck(string BeginDate,string EndDate,string OperID)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--����ת��
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCheck", ref strSQL ) == -1  )
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ���ݲ��Ż���ս���֧Ʊ����
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayCheckByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql����			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCheckByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ��ȡ�ս���֧Ʊ
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="OperID">����Ա��ʶ</param>
		/// <returns></returns>
		public string GetPrepayOther(string BeginDate,string EndDate,string OperID)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--����
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayOther", ref strSQL ) == -1  )
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ��ȡ�ս����Ʊ
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="OperID">����Ա��ʶ</param>
		/// <returns></returns>
		public string GetPrepayPostal(string BeginDate,string EndDate,string OperID)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='PO'--����
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayPostal", ref strSQL ) == -1  )
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// ���ݲ��Ż���ս����Ʊ����
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayPostalByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql����			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayPostalByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		#region delete By Maokb
		//		/// <summary>
		//		/// �����סԺԤ��
		//		/// </summary>
		//		/// <param name="BeginDate"></param>
		//		/// <param name="EndDate"></param>
		//		/// <param name="OperID"></param>
		//		/// <param name="TransType"></param>
		//		/// <returns></returns>
		//		public string GetReturnInPrepayCost(string BeginDate,string EndDate,string OperID,string TransType) 
		//		{
		//			#region Sql����			
		//			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
		//			// WHERE  PARENT_CODE='[��������]'  
		//			// AND  CURRENT_CODE='[��������]' 
		//			// AND  BALANCE_OPERCODE = '{0}' 
		//			//  AND  BALANCE_DATE >=to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
		//			// AND  BALANCE_DATE <=to_date('{2}','yyyy-mm-dd HH24:mi:ss') 
		//			//and TRANS_TYPE = {3} 
		//			#endregion
		//			string strCost = "";
		//			string strSQL = "";
		//			
		//			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetReturnInPrepay", ref strSQL ) == -1  ) 
		//			{
		//				this.Err = "����ս������!";
		//				return "";
		//			}	
		//		
		//			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID,TransType);
		//			
		//			strCost = this.ExecSqlReturnOne( strSQL );
		//
		//			return strCost;
		//		}
		
		//		/// <summary>
		//		/// ��Ѫ��
		//		/// </summary>
		//		/// <param name="BeginDate"></param>
		//		/// <param name="EndDate"></param>
		//		/// <param name="OperID"></param>
		//		/// <returns></returns>
		//		public string GetReturnYXCost(string BeginDate,string EndDate,string OperID) 
		//		{
		//			#region Sql����			
		//			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
		//			// WHERE  PARENT_CODE='[��������]'  
		//			// AND  CURRENT_CODE='[��������]' 
		//			// AND  BALANCE_OPERCODE = '{0}' 
		//			//  AND  BALANCE_DATE >=to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
		//			// AND  BALANCE_DATE <=to_date('{2}','yyyy-mm-dd HH24:mi:ss') 
		//			//and TRANS_TYPE = {3} 
		//			#endregion
		//			string strCost = "";
		//			string strSQL = "";
		//			
		//			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetReturnYX", ref strSQL ) == -1  ) 
		//			{
		//				this.Err = "�����Ѫ�����!";
		//				return "";
		//			}	
		//		
		//			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
		//			
		//			strCost = this.ExecSqlReturnOne( strSQL );
		//
		//			return strCost;
		//		}
		#endregion
		/// <summary>
		/// �����סԺԤ�����պͷ����ı�־
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <param name="TransType"></param>
		/// <returns></returns>
		public string GetReturnInPrepaySupplyflag(string BeginDate,string EndDate,string OperID,string TransType) 
		{
			#region Sql����			
			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
			// WHERE  PARENT_CODE='[��������]'  
			// AND  CURRENT_CODE='[��������]' 
			// AND  BALANCE_OPERCODE = '{0}' 
			//  AND  BALANCE_DATE >=to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
			// AND  BALANCE_DATE <=to_date('{2}','yyyy-mm-dd HH24:mi:ss') 
			//and TRANS_TYPE = {3} 
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetReturnInPrepaySupplyflag", ref strSQL ) == -1  ) 
			{
				this.Err = "����ս������!";
				return "";
			}	
		
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID,TransType);
			
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayOtherByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql����			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayOtherByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "����ս������!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}

		/// <summary>
		/// ���ݲ��Ż�ȡ֧Ʊ
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptCheck(string BeginDate,string EndDate,string Dept)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--����ת��
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptCheck", ref strSQL ) == -1  )
			{
				this.Err = "����ս������!";
				return "";
			}	
			else
			{
				strSQL = string.Format(strSQL,BeginDate,EndDate,Dept);
			}
			//			if(Dept!="ALL")
			//			{
			//				if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptWhere",ref strSQL) ==-1)
			//				{
			//					this.Err = "���SQL������";
			//					return"";
			//				}
			//				else
			//				{
			//					strWhere = string.Format(strWhere,Dept);
			//				}
			//				strSQL += strWhere;
			//			}
			//			
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		
		/// <summary>
		/// ��ò���Ա�ս�ʵ���ս��е���Ԥ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <param name="BalanceState"></param>
		/// <returns></returns>
		public string GetPrePay(string Begin,string End,string Oper,string BalanceState)
		{
			string strCost = "";
			string strSQL = "",strWhere = "",strStat = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptCost.1", ref strSQL ) == -1  )
				{
					this.Err = "��ѯԤ�������!";
					return "";
				}	
				else
				{
					strSQL = string.Format(strSQL,Begin,End);
				}
				if(Oper!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptCostByOper.1",ref strWhere) ==-1)
					{
						this.Err = "���SQL������";
						return"";
					}
					else
					{
						strWhere = string.Format(strWhere,Oper);
					}
					strSQL += strWhere;
				}		
				if(BalanceState!="ALL")
				{
					if(BalanceState=="0")
					{
						if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptCost.state.0",ref strStat) ==-1)
						{
							this.Err = "���SQL������";
							return"";
						}
						else
						{
							strStat = string.Format(strStat);
						}
					}
					else
					{
						if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptCostByOper.1",ref strStat) ==-1)
						{
							this.Err = "���SQL������";
							return"";
						}
						else
						{
							strStat = string.Format(strStat);
						}
					}
					strSQL += strStat;
				}
				strCost = this.ExecSqlReturnOne( strSQL );
			}
			catch{}
			
			return strCost;
		}
		/// <summary>
		/// ���ݲ��Ż�ȡ�ֽ�
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptCost(string BeginDate,string EndDate,string Dept)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--����ת��
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptCost", ref strSQL ) == -1  )
				{
					this.Err = "����ս������!";
					return "";
				}	
				else
				{
					strSQL = string.Format(strSQL,BeginDate,EndDate,Dept);
				}
				//				if(Dept!="ALL")
				//				{
				//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptWhere",ref strSQL) ==-1)
				//					{
				//						this.Err = "���SQL������";
				//						return"";
				//					}
				//					else
				//					{
				//						strWhere = string.Format(strWhere,Dept);
				//					}
				//					strSQL += strWhere;
				//				}
			
				strCost = this.ExecSqlReturnOne( strSQL );
			}
			catch{}
			return strCost;
		}

		/// <summary>
		/// ���ݲ��Ż�ȡ���������п���
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptOther(string BeginDate,string EndDate,string Dept)
		{
			#region Sql����			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[��������]' 
			//and CURRENT_CODE='[��������]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--����ת��
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptOther", ref strSQL ) == -1  )
				{
					this.Err = "����ս������!";
					return "";
				}	
				else
				{
					strSQL = string.Format(strSQL,BeginDate,EndDate,Dept);
				}
				//				if(Dept!="ALL")
				//				{
				//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetByDeptWhere",ref strSQL) ==-1)
				//					{
				//						this.Err = "���SQL������";
				//						return"";
				//					}
				//					else
				//					{
				//						strWhere = string.Format(strWhere,Dept);
				//					}
				//					strSQL += strWhere;
				//				}
			
				strCost = this.ExecSqlReturnOne( strSQL );
			}
			catch{}
			return strCost;
		}
        		

		
		/// <summary>
		/// Ԥ������
		/// </summary>
		/// <returns></returns>
		public string GetReceiptNum(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetReceiptNum", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// Ԥ������Ʊ���Ӻ�
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public string GetOutReceipt(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			ArrayList List = new ArrayList();

			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetOutReceipt", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			else
			{
				strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			}
			this.ExecQuery(strSQL);
			while(this.Reader.Read())
			{
				strRet += Reader[0].ToString()+" "+"|"+" ";	
			}
			return strRet;
		}

		/// <summary>
		/// ���Ʊ���������С��
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public string GetMinReceiptNo(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetMinReceipt", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// ���Ʊ�����������
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public string GetMaxReceiptNo(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetMaxReceipt", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}
		/// <summary>
		/// ���Ʊ������ --סԺ������ʵ���ձ�ʹ��
		/// </summary>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public string GetReceiptZone(string dtBegin,string dtEnd,string OperID) 
		{
			string strSql = "";
			if(this.Sql.GetCommonSql("Fee.FeeReport.GetReceiptZone",ref strSql) == -1) 
			{
				this.Err = "û���ҵ�Fee.FeeReport.GetReceiptZone�ֶ�";
				return "ERR";
			}
			strSql = System.String.Format(strSql,dtBegin,dtEnd,OperID);
			return this.ExecSqlReturnOne(strSql);
		}
		/// <summary>
		/// תѺ��
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public string GetTransCost(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetTransCost", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// ��ù��Ѻϼ�
		/// </summary>
		/// <param name="strID">סԺ��</param>
		/// <returns></returns>
		public string GetPub(string strID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.pub", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,strID);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}
		/// <summary>
		/// ����ԷѺϼ�
		/// </summary>
		/// <param name="strID">סԺ��</param>
		/// <returns></returns>
		public string GetOwn(string strID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.own", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,strID);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// ȡ�������嵥�ܷ���
		/// </summary>
		/// <param name="inPatientID"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject GetPatientFeeTot(string inPatientID,DateTime dt) {
		 	string strSql = "";
			FS.FrameWork.Models.NeuObject obj = new NeuObject();
			obj.ID = "0";
			obj.Name = "0";
			obj.User01 = "0";
			//-------------------------------------------------------
			if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientDuimalFee.Tot1",ref strSql) == -1) {
				this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,inPatientID,dt);
			try {
				if(this.ExecQuery(strSql) == -1)
					return null;
				while(this.Reader.Read()) {
					if(this.Reader[0] == System.DBNull.Value)//�����ܷ���
						obj.User02 = "0";
					else
						obj.User02 = this.Reader[0].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception e) {
				this.Err = e.ToString();
				return null;
			}
			//--------------------------------------------------------
			if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientDuimalFee.Tot2",ref strSql) == -1) {
				this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,inPatientID,dt);
			try {
				if(this.ExecQuery(strSql) == -1)
					return null;
				while(this.Reader.Read()) {
					if(this.Reader[0] == System.DBNull.Value)//���嵥δ��Ԥ����
						obj.User03 = "0";
					else
						obj.User03 = this.Reader[0].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception e) {
				this.Err = e.ToString();
				return null;
			}
			ddddd(inPatientID,dt,ref obj);
			return obj;
		}
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dt"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int ddddd(string id,DateTime dt,ref FS.FrameWork.Models.NeuObject obj) {
			string strSql = "";
			try {
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientDuimalFee.Tot4",ref strSql) == -1) 
				{
					this.Err = "Can't Find Sql";
					return -1;
				}
				strSql = System.String.Format(strSql,id,dt);
				if(this.ExecQuery(strSql) == -1)
					return -1;
				while(this.Reader.Read()) 
				{
					if(this.Reader[0] == System.DBNull.Value)
						obj.ID = "0";
					else
						obj.ID = Reader[0].ToString();
				}
				this.Reader.Close();
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientDuimalFee.Tot3",ref strSql) == -1) {
					this.Err = "Can't Find Sql";
					return -1;
				}
				strSql = System.String.Format(strSql,id,dt);
				if(this.ExecQuery(strSql) == -1)
					return -1;
				while(this.Reader.Read()) {
//					if(this.Reader[0] == System.DBNull.Value)
//						obj.ID = "0";
//					else
//						obj.ID = Reader[0].ToString();
					if(this.Reader[0] == System.DBNull.Value)
						obj.Name = "0";
					else
					    obj.Name = Reader[0].ToString();
					if(this.Reader[1] == System.DBNull.Value)
						obj.User01 = "0";
					else
					    obj.User01 = Reader[1].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception e) {
				this.Err = e.ToString();
				return -1;
			}
			return 0;
		}
		/// <summary>
		/// ����סԺ�Ų����
		/// </summary>
		/// <param name="strID">סԺ��</param>
		/// <returns></returns>
		public ArrayList GetOldFeeCos(string strID)
		{
			ArrayList List = new ArrayList();
			string strSql = "";
			//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' and REPORT_TYPE = '{1}'
			if (this.Sql.GetCommonSql("Fee.FeeReport.SelectByPatient",ref strSql)==-1) return null;
			try
			{
				FS.FrameWork.Models.NeuObject obj;
				FS.HISFC.Models.RADT.PatientInfo oInfo;
				strSql = string.Format(strSql,strID);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{
					obj = new FS.FrameWork.Models.NeuObject();
					oInfo = new FS.HISFC.Models.RADT.PatientInfo();
					obj.ID = Reader[0].ToString(); //������	
					obj.Name = Reader[1].ToString();//סԺ��
					obj.User01 = Reader[2].ToString();//�����ܶ�
					obj.User02 = Reader[3].ToString();//����
					obj.User03 = Reader[4].ToString();//�Է�					
					obj.Memo = Reader[5].ToString();//��ע
					
					List.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;
				List = null;
			}
			return List;

		}
		/// <summary>
		/// ��ָ�� ��׼��ѯǷ��
		/// </summary>
		/// <param name="dFreeCost">���</param>
		/// <param name="strNurseCell"></param>
		/// <returns></returns>
		public ArrayList GetAleryMoney0(decimal dFreeCost,string strNurseCell)
		{
			string strSql1 = "";
			ArrayList List = new ArrayList();
			string strSql = "";
			try
			{
				//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' and REPORT_TYPE = '{1}'
				if (this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.0",ref strSql)==-1) return null;
				strSql = string.Format(strSql,dFreeCost);
			
				FS.HISFC.Models.RADT.PatientInfo obj;
				if(strNurseCell!="")
				{
					this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.Where",ref strSql1);
					strSql1 = string.Format(strSql1,strNurseCell);
				}
				strSql = strSql +" "+ strSql1;
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new FS.HISFC.Models.RADT.PatientInfo();
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//����
					obj.PID.PatientNO = Reader[1].ToString();//סԺ��
					obj.Name = Reader[2].ToString();//����
					obj.Pact.PayKind.ID = Reader[3].ToString();//�����������
					obj.Pact.PayKind.Name = Reader[4].ToString();//��������ʾ
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//δ����	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//Ԥ�����	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//������					
					List.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;
				List = null;
			}
			return List;

		}

		/// <summary>
		/// ��������ѯ
		/// </summary>
		/// <param name="dScale">����</param>
		/// <param name="strNurseCell">����</param>
		/// <returns></returns>
		public ArrayList GetAleryMoney1(decimal dScale,string strNurseCell)
		{
			string strSql1 = "";
			ArrayList List = new ArrayList();
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.1",ref strSql)==-1) return null;
				strSql = string.Format(strSql,dScale);
				FS.HISFC.Models.RADT.PatientInfo obj;
				if(strNurseCell!="")
				{
					this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.Where",ref strSql1);
					strSql1 = string.Format(strSql1,strNurseCell);
				}
				strSql = strSql +" "+ strSql1;
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new FS.HISFC.Models.RADT.PatientInfo();
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//����
					obj.PID.PatientNO = Reader[1].ToString();//סԺ��
					obj.Name = Reader[2].ToString();//����
					obj.Pact.PayKind.ID = Reader[3].ToString();//�����������
					obj.Pact.PayKind.Name = Reader[4].ToString();//��������ʾ
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//δ����	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//Ԥ�����	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//������					
					List.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;
				List = null;
			}
			return List;

		}

		/// <summary>
		/// ���ݾ����߲�ѯ
		/// </summary>
		/// <param name="strNurseCell"></param>
		/// <returns></returns>
		public ArrayList GetAleryMoney2(string strNurseCell)
		{
			string strSql1 = "";
			ArrayList List = new ArrayList();
			string strSql = "";
			try
			{
				//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[��������]' and CURRENT_CODE ='[��������]' and REPORT_TYPE = '{1}'
				if (this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.2",ref strSql)==-1) return null;
				//			strSql = string.Format(strSql);
				FS.HISFC.Models.RADT.PatientInfo obj;
				if(strNurseCell!="")
				{
					this.Sql.GetCommonSql("Fee.FeeReport.AleryMoney.Where",ref strSql1);
					strSql1 = string.Format(strSql1,strNurseCell);
				}
				strSql = strSql +" "+ strSql1;
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new FS.HISFC.Models.RADT.PatientInfo();
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//����
					obj.PID.PatientNO = Reader[1].ToString();//סԺ��
					obj.Name = Reader[2].ToString();//����
					obj.Pact.PayKind.ID = Reader[3].ToString();//�����������
					obj.Pact.PayKind.Name = Reader[4].ToString();//��������ʾ
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//δ����	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//Ԥ�����	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//������					
					List.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;
				List = null;
			}
			return List;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int PrePayDayStat(FS.HISFC.Models.Fee.FeeCodeStat obj)
		{
			#region Sql����
			
			#endregion
			string strSql = "";
			#region "�ӿ�"
			//insert into FIN_IPB_PREPAYSTAT
			//(
			//PARENT_CODE,CURRENT_CODE,STATIC_NO,BEGIN_DATE,END_DATE,OPER_CODE,PREPAY_CASH,
			//PREPAY_CHECK,PREPAY_OTHER,FOREGIFT_COST,RECEIPT_ZONE,PREPAY_NUM,RETURN_NO
			//)VALUES
			//('[��������]',
			// '[��������]',
			//'{0}',
			//to_date('{1}','yyyy-mm-dd HH24:mi:ss'),
			//to_date('{2}','yyyy-mm-dd HH24:mi:ss'),
			//			'{3}',
			//			'{4}',
			//			'{5}',
			//			'{6}',
			//			'{7}',
			//      '{8}',
			//      '{9}',
			//      '{10}'
			//)

			#endregion	
			
			if (this.Sql.GetCommonSql("Fee.FeeCodeStat.InsertFeeCodeStat",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,
					obj.Name,
					obj.Name,
					obj.ReportType,
					obj.MinFee.ID,
					obj.FeeStat.ID,
					obj.FeeStat.ID,
					obj.StatCate,
					obj.ExecDept.ID,
					obj.CenterStat,
					obj.SortID,
					obj.ValidState,
					this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql) ;
		}

		/// <summary>
		/// ��ȡ�ܵ�סԺ��
		/// </summary>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="OperID">����Ա</param>
		/// <returns></returns>
		public string GetFeeSum(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetSumFee", ref strSQL ) == -1  )
			{
				this.Err = "��ó���!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;		
		}
	
		/// <summary>
		/// ���ݲ��Ż��ҽԺ������
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public ArrayList GetDetailedByDept(string BeginDate,string EndDate,string Dept)
		{	
			ArrayList arr = new ArrayList();
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetDayReportDetailByDept",ref strSql)==-1) return null;
				FS.FrameWork.Models.NeuObject obj ;
				strSql = string.Format(strSql,Dept,BeginDate,EndDate);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new NeuObject();
					obj.ID = Reader[1].ToString();
					obj.Name = Reader[2].ToString();
					obj.Memo = Reader[0].ToString();
					arr.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return arr;			

		}

		/// <summary>
		/// ��ʼ��ͳ�ƴ���
		/// </summary>
		/// <returns></returns>
		public ArrayList InitStatItem()
		{	
			ArrayList arr = new ArrayList();
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.InitStatType",ref strSql)==-1) return null;
				FS.FrameWork.Models.NeuObject obj ;				
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{
                    obj = new NeuObject();
					obj.ID = Reader[1].ToString();
					obj.Name = Reader[2].ToString();
					obj.Memo = Reader[0].ToString();
					arr.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return arr;			

		}
		
		/// <summary>
		/// ��ȡʵ���ս����ʱ��
		/// </summary>
		/// <returns></returns>
		public string GetMaxTimeDayReport(string strOper)
		{
			#region Sql����			
			//select max(end_date) from FIN_IPB_PREPAYSTAT
			#endregion
			string strCost = "";
			try
			{
				string strSQL = "";
			
				if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetDayReportMaxDate", ref strSQL ) == -1  )
				{
					this.Err = "������ʱ�����!";
					return "";
				}	
				else
				{
					strSQL = string.Format(strSQL,strOper);
				}
				strCost = this.ExecSqlReturnOne( strSQL );
			}
			catch{}

			return strCost;
		}


		/// <summary>
		/// ����סԺ��ȡ��
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public ArrayList GetBillFeeCode(string strID)
		{	
			ArrayList arr = new ArrayList();
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetDayReportFeeCode",ref strSql)==-1) return null;
				FS.FrameWork.Models.NeuObject obj ;
				strSql = string.Format(strSql,strID);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new NeuObject();
					obj.ID = Reader[1].ToString();
					obj.Name = Reader[2].ToString();
					obj.Memo = Reader[0].ToString();
					arr.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return arr;			

		}

		/// <summary>
		/// ��ý����嵥����ֵ
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject GetBillFee(string strID)
		{	
			FS.FrameWork.Models.NeuObject obj = null;
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetBillFee",ref strSql)==-1) return null;
				
				strSql = string.Format(strSql,strID);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new NeuObject();
					obj.User01 = Reader[0].ToString();
					obj.User02 = Reader[1].ToString();
					obj.User03 = Reader[2].ToString();					
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return obj;

        }
       
        /// <summary>
		/// ��ý����嵥�Է�
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public FS.FrameWork.Models.NeuObject GetBillOwnFee(string strID)
		{	
			FS.FrameWork.Models.NeuObject obj = null;
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetBillOwnFee",ref strSql)==-1) return null;
				
				strSql = string.Format(strSql,strID);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new NeuObject();
					obj.User01 = Reader[0].ToString();
					obj.User02 = Reader[1].ToString();
					obj.User03 = Reader[2].ToString();	
					obj.ID = Reader[3].ToString();
					obj.Name = Reader[4].ToString();
					obj.Memo = Reader[5].ToString();
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return obj;			

		}

		/// <summary>
		/// ��ȡְ���������շ��ý�������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>

		public ArrayList GetOldFeeReport(string Begin,string End)
		{	
			ArrayList arr = new ArrayList();
			string strSql = "",strSql1 = "";
			string strDate = "1950-01-01";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetOldFeeReport",ref strSql)==-1) return null;
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetOldFeeReportWhere",ref strSql1)==-1) return null;
				FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
				strSql1 = string.Format(strSql1,Begin,End);
				strSql = strSql +" "+ strSql1;
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new FS.HISFC.Models.RADT.PatientInfo();
					obj.ProCreateNO = this.Reader[0].ToString();//���˵��Ժ�
					obj.User01 = this.Reader[1].ToString();//����
					obj.SSN = this.Reader[2].ToString();//��ҽƾ֤��
					obj.PID.PatientNO = this.Reader[3].ToString();//סԺ��
					obj.Memo = this.Reader[4].ToString();//��ʽ����ʽ
					if(this.Reader[5].ToString()!="")
						obj.PVisit.InTime = Convert.ToDateTime(this.Reader[5].ToString());//��Ժ����
					else obj.PVisit.InTime = Convert.ToDateTime(strDate);
					if(this.Reader[6].ToString()!="")
						obj.PVisit.OutTime = Convert.ToDateTime(this.Reader[6].ToString());//��Ժ����
					else obj.PVisit.OutTime = Convert.ToDateTime(strDate);
					obj.PVisit.Memo = this.Reader[7].ToString();//סԺ����
					if(this.Reader[8].ToString()!="")
						obj.FT.TotCost = Convert.ToDecimal(this.Reader[8].ToString());//���úϼ�
					else obj.FT.TotCost=0;
					if(this.Reader[9].ToString()!="")
						obj.FT.PubCost = Convert.ToDecimal(this.Reader[9].ToString());//���ѽ��
					else obj.FT.PubCost = 0;
					if(this.Reader[10].ToString()!="")
						obj.FT.OwnCost = Convert.ToDecimal(this.Reader[10].ToString());//�Էѽ��
					else obj.FT.OwnCost = 0;
					//ʵ��סԺҽ�Ʒѷ���
					if(this.Reader[11].ToString()!="")
						obj.FT.Memo = this.Reader[11].ToString();//����
					else obj.FT.Memo = "0";
					if(this.Reader[12].ToString()!="")
						obj.FT.ID = this.Reader[12].ToString();//���
					else obj.FT.ID = "0";
					if(this.Reader[13].ToString()!="")
						obj.FT.Name = this.Reader[13].ToString();//����
					else obj.FT.Name = "0";
					if(this.Reader[14].ToString()!="")
						obj.FT.User01 = this.Reader[14].ToString();//���Ʒ�
					else obj.FT.User01 = "0";
					if(this.Reader[15].ToString()!="")
						obj.FT.User02 = this.Reader[15].ToString();//�����
					else obj.FT.User02 = "0";
					if(this.Reader[16].ToString()!="")
						obj.FT.User03 = this.Reader[16].ToString();//������
					else obj.FT.User03 = "0";
					if(this.Reader[17].ToString()!="")
						obj.FT.DerateCost = Convert.ToDecimal(this.Reader[17].ToString());//�����
					else obj.FT.DerateCost = 0;
					if(this.Reader[18].ToString()!="")
						obj.FT.RebateCost = Convert.ToDecimal(this.Reader[18].ToString());//ҩ��
					else obj.FT.RebateCost = 0;
					if(this.Reader[19].ToString()!="")
						obj.FT.BalancedCost = Convert.ToDecimal(this.Reader[19].ToString());//����	
					else obj.FT.BalancedCost = 0;
					
					//��ǰ���ʵ�ʷ���
					if(this.Reader[20].ToString()!="")
						obj.Kin.User01 = this.Reader[20].ToString();//�ϼ�
					else obj.Kin.User01 = "0";
					if(this.Reader[21].ToString()!="")
						obj.Kin.User02 = this.Reader[21].ToString();//���˷���
					else obj.Kin.User02 = "0";
					if(this.Reader[22].ToString()!="")
						obj.Kin.User03 = this.Reader[22].ToString();//֧������
					else obj.Kin.User03 = "0";
					//�ܶ�
					if(this.Reader[23].ToString()!="")
						obj.User01 = this.Reader[23].ToString();//��ҽ�Ʒ���
					else obj.User01 = "0";
					if(this.Reader[24].ToString()!="")
						obj.User02 = this.Reader[24].ToString();//�ܼ��˷���
					else obj.User02 = "0";
					if(this.Reader[25].ToString()!="")
						obj.User03 = this.Reader[25].ToString();//���Ը�����
					else obj.User03 = "0";

					if(this.Reader[26].ToString()!="")
						obj.ID = this.Reader[26].ToString();//����������
					else obj.ID = "0";
					if(this.Reader[27].ToString()!="")
						obj.Name = this.Reader[27].ToString();//�����׼�걨������
					else obj.Name = "0";
					if(this.Reader[28].ToString()!="")
						obj.Memo = this.Reader[28].ToString();//��Ҫ���
					if(this.Reader[29].ToString()!="")
						obj.PVisit.RegistTime = Convert.ToDateTime(this.Reader[29].ToString());//��������
					else obj.PVisit.RegistTime = Convert.ToDateTime(strDate);
					
					arr.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return arr;			

		}

		private string  GetSqlMedicine(string Begin,string End,string Dept)
		{
			string strSql = "";
			try
			{
				
				
				
			}
			catch{}
			return strSql;
		}

		/// <summary>
		/// ���סԺ����ҽ����ҩһ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Dept"></param>
		/// <param name="InState"></param>
		/// <returns></returns>
		public DataSet GetDataSetMedicine(string Begin,string End,string Dept,string InState)
		{
			DataSet dts = new DataSet();
			string strSql = "";
			string strGroup = "";
			string strWhere = "";
			string strWhere1 = "";
			if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetSqlMedicineBase",ref strSql)==-1) return null;
				
			strSql = string.Format(strSql,Begin,End);	
			if(Dept!="1")
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetSqlMedicineWhere",ref strWhere)==-1) return null;
				strWhere = string.Format(strWhere,Dept);
				strSql = strSql +" "+ strWhere;
			}
			if(InState!="1")
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetSqlMedicineWhere1",ref strWhere1)==-1) return null;
				strWhere1 = string.Format(strWhere1,InState);	
				strSql = strSql +" "+ strWhere1;
			}
			
			if (this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetSqlMedicineGroup",ref strGroup)==-1) return null;
				
			strGroup = string.Format(strGroup);	
			strSql = strSql +" "+ strGroup;
			try
			{
				this.ExecQuery(strSql,ref dts);
			}
			catch{}
			//			this.r
			return dts;
		}
		
		/// <summary>
		/// סԺ����ҽ������ҽ����ҩһ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Pact"></param>
		/// <param name="InState"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetDataSetMedicinePatient(string Begin,string End,string Pact,string InState,string NurseCell)
		{
			DataSet dts = new DataSet();
			string strSql = "";
			//			string strGroup = "";
			string strWhere = "";
			string strWhere1 = "";
			string strWhere2 = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineByPanient",ref strSql)==-1) return null;
				
				strSql = string.Format(strSql,Begin,End);	
				if(Pact!="ALL")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByttWhere",ref strWhere)==-1) return null;
					strWhere = string.Format(strWhere,Pact);
					strSql = strSql +" "+ strWhere;
				}
				if(NurseCell!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineByNurse",ref strWhere1)==-1) return null;
					strWhere1 = string.Format(strWhere1,NurseCell);	
					strSql = strSql +" "+ strWhere1;
				}
				if(InState!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineByInstat",ref strWhere2)==-1) return null;
					strWhere2 = string.Format(strWhere1,InState);	
					strSql = strSql +" "+ strWhere2;
				}		
			
			
				this.ExecQuery(strSql,ref dts);
			}
			catch{}
			//			this.r
			return dts;
		}
		/// <summary>
		/// ��ü�����ҩƷ������ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="InState"></param>
		/// <param name="NurseCell"></param>
		/// <param name="Pact"></param>
		/// <returns></returns>
		public DataSet GetDataSetMedicineType(string Begin,string End,string InState,string NurseCell,string Pact)
		{
			DataSet dts = new DataSet();
			string strSql = "";
			string strWhere1 = "";
			string strWhere2 = "",strWhere3 = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineType",ref strSql)==-1) return null;
				
				strSql = string.Format(strSql,Begin,End);	
			
				if(NurseCell!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineTypeByNurse",ref strWhere1)==-1) return null;
					strWhere1 = string.Format(strWhere1,NurseCell);	
					strSql = strSql +" "+ strWhere1;
				}
				if(InState!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetSqlMedicineTypeByInstat",ref strWhere2)==-1) return null;
					strWhere2 = string.Format(strWhere2,InState);	
					strSql = strSql +" "+ strWhere2;
				}	
				if(Pact!="ALL")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByttWhere ",ref strWhere3)==-1) return null;
					strWhere3 = string.Format(strWhere3,Pact);	
					strSql = strSql +" "+ strWhere3;
				}
			
				this.ExecQuery(strSql,ref dts);
			}
			catch{}
			return dts;
		}

		/// <summary>
		/// ���סԺ�ܷ��ø�������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <param name="InState"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetFeeStructeSum(string Begin,string End,string NurseCell,string InState,string Dept)
		{			
			string strCost = "";
			string strSql = "";			
			string strNurse = "";
			string strInstate = "";
			string strDept = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetFeeStructeSum", ref strSql ) == -1  )
				{
					this.Err = "��������!";
					return "";
				}	
				strSql = string.Format(strSql,Begin,End);	
				if(NurseCell!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByNurse",ref strNurse)==-1) return null;
					strNurse = string.Format(strNurse,NurseCell);	
					strSql = strSql +" "+ strNurse;
				}
				if(InState!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByInstate",ref strInstate)==-1) return null;
					strInstate = string.Format(strInstate,InState);	
					strSql = strSql +" "+ strInstate;
				}	
				if(Dept!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByDept",ref strDept)==-1) return null;
					strDept = string.Format(strDept,Dept);	
					strSql = strSql +" "+ strDept;
				}	
				strCost = this.ExecSqlReturnOne( strSql );
			}
			catch{}

			return strCost;
		}

		///
		/// <summary>
		/// סԺҽ�����˷��ù��ɱ�
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="InState"></param>
		/// <param name="NurseCell"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public DataSet GetFeeStructe(string Begin,string End,string InState,string NurseCell,string Dept)
		{
			DataSet dts = new DataSet();
			string strSql = "";
			string strTable = "";
			string strNurse = "";
			string strInstate = "";
			string strDept = "";
			string strGroup = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeSelect",ref strSql)==-1) return null;
				
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotTable",ref strTable)==-1) return null;
				
				else strTable = string.Format(strTable,Begin,End);	
				strSql = strSql +" "+ strTable;
			
				if(NurseCell!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByNurse",ref strNurse)==-1) return null;
					else strNurse = string.Format(strNurse,NurseCell);	
					strSql = strSql +" "+ strNurse;
				}
				if(InState!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByInstate",ref strInstate)==-1) return null;
					else strInstate = string.Format(strInstate,InState);	
					strSql = strSql +" "+ strInstate;
				}	
				if(Dept!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotByDept",ref strDept)==-1) return null;
					else strDept = string.Format(strDept,Dept);	
					strSql = strSql +" "+ strDept;
				}	
			
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeTypeTotGroup",ref strGroup)==-1) return null;
				strSql = strSql +" "+ strGroup;
				this.ExecQuery(strSql,ref dts);
			}
			catch{}
			return dts;
		}


		/// <summary>
		/// סԺҩ���±�����ѯסԺ���������á����ñ����ȡ�
		/// Rework by Maokb
		/// </summary>
		/// <param name="NurseCell"></param>
		/// <param name="InState"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="QueryType"></param>
		/// <returns></returns>
		public DataSet GetStructeChange(string  NurseCell ,string InState, string Begin,string End,string QueryType)
		{
			string strSql = "";
			string strNurse = "";
			string strInstate = "";
			string strGroup = "";
			System.Data.DataSet  dts = new DataSet();
			try
			{				
				
				if(QueryType=="0")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeByNurse",ref strSql)==-1) return null;
				
					else strSql = string.Format(strSql,Begin,End);	
					
				}
				else
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeTypeStructeFeeByMonth",ref strSql)==-1) return null;
				
					else strSql = string.Format(strSql,Begin,End);						
				}
				if(NurseCell!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeWhereByNurse",ref strNurse)==-1) return null;
					else strNurse = string.Format(strNurse,NurseCell);	
					strSql = strSql +" "+ strNurse;
				}
				if(InState!="1")
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeWhereByInstate",ref strInstate)==-1) return null;
					strInstate = string.Format(strInstate,InState);	
					strSql = strSql +" "+ strInstate;
				}
				if(QueryType=="0")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeStructeFeeGroupBy",ref strGroup)== -1) return null;
					strSql = strSql + " " + strGroup;
				}
				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return dts;
		}



		/// <summary>
		/// ���ϱ䶯��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetShiftDataChange( string Begin,string End,string deptlist)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				//����ʱ������ �� ������ѯ
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetShiftDataChange.IsDate",ref strSql) ==-1 )
				{
					this.Err = this.Sql.Err;
					return null ;
				}
				strSql = string.Format(strSql,Begin,End,deptlist);
				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
			}
			return ds;
		}


		/// <summary>
		/// ����������־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public ArrayList  GetanaerecordData(string Begin,string End,string deptlist)
		{
			ArrayList list = new ArrayList();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetanaerecordData",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Inpatient.FeeInfo info =null;
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
					info.ID = Reader[0].ToString(); // סԺ��
					info.Name =Reader[1].ToString();//����
					((FS.HISFC.Models.RADT.PatientInfo)info.Patient).PVisit.PatientLocation.NurseCell.Name = Reader[2].ToString();// ����
					info.Item.MinFee.Name = Reader[3].ToString();//��С�������� 
					info.FeeOper.ID = Reader[4].ToString();//����Ա
					info.ExecOper.Dept.Name =Reader[5].ToString();// ִ�п���
					list.Add(info);
					info= null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return list;

		}
		/// <summary>
		/// ȡÿ��סԺ��ˮ�Ŷ�Ӧ�� ��С�����б�
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public ArrayList getOperationDetail(string Begin,string End,string deptlist)
		{
			ArrayList list = new ArrayList();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.getOperationDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Inpatient.FeeInfo info =null;
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
					info.ID = Reader[0].ToString(); //סԺ��
					info.Item.MinFee.ID =Reader[1].ToString(); //��С���ô���
					info.Item.MinFee.Name =Reader[2].ToString(); //��С��������
					info.ExecOper.Dept.Name = Reader[3].ToString();// ִ�п���
					info.FT.TotCost =Convert.ToDecimal(Reader[3]);//�ϼ� 
					list.Add(info);
					info =null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{ 
				if(this.Reader!=null)
				{
					this.Reader.Close();
				}
				this.Err = ee.Message;
				return null;
			}
			return list ;
		}

		/// <summary>
		/// �ͷѼ�����ϸ��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetDinnerData(string Begin,string End,string deptlist)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetDinnerData",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

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
		/// 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public DataSet GetFeeAllPrepayCost(string Begin,string End,string Dept) 
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "",strWhere = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAllCost",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(Dept!="") 
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAllCost.Where",ref strWhere) ==-1) 
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else 
					{
						strWhere = string.Format(strWhere,Dept);
					}
					strSql = strSql +" "+ strWhere;
				}

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
		/// ��סԺ��Ӧ����ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeeAccontList(string Begin,string End,string NurseCell)
		{
			string strSql = "";
			string strWhere = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontListWhere",ref strWhere) ==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
				}
				strSql = strSql +" "+ strWhere;

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
		/// ��ȫԺӦ����ϸ����������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeeAccontNurseList(string Begin,string End,string NurseCell)
		{
			System.Data.DataSet  ds = new DataSet();
			string strGroupBy = "";
			string strWhere = "";
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontNurseList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontNurseListWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
				}
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontNurseListGroupBy",ref strGroupBy)==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}				
				strSql = strSql +" "+strWhere+" "+strGroupBy;

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
		/// ���סԺ��Ӧ����ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeeAccontPayList(string Begin,string End,string NurseCell)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontPayList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ���סԺӦ����ϸ����������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeeAccontPayListByNurse(string Begin,string End,string NurseCell)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontPayListByNurse",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ��ɽһԺ��ϸ������־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <returns></returns>
		public DataSet GetItemMedicineList(string Begin,string End,string deptList )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetItemMedicineList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptList);
				}

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
		/// ��ɽһԺȷ�ϼ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="OperCode"></param>
		/// <returns></returns>
		public DataSet GetItemMedicineListQueren(string Begin,string End,string deptlist,string OperCode )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetItemMedicineListQueren",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist,OperCode);
				}

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
		/// 	��ɽһԺ�ۺϼ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="OperCode"></param>
		/// <returns></returns>
		public DataSet GetItemMedicineListZonghe(string Begin,string End,string deptlist,string OperCode )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetItemMedicineListZonghe",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist,OperCode);
				}

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
		/// ��ɽһԺֱ�ӣ�����������ϸ��סԺ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetItemMedicineListZhijiebu(string Begin,string End,string deptlist )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetItemMedicineListZhijiebu",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

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
		/// ��ɽһԺҩ������ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetItemMedicineDetial(string Begin,string End,string deptlist )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetItemMedicineDetial",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

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
		/// ��ɽһԺת����Ա��־��סԺ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet  GetShitDateInfo(string Begin,string End)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetShitDateInfo",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;

//			ArrayList List = new ArrayList();
//			try
//			{
//				string strSql = "";
//				if(this.Sql.GetCommonSql("Fee.FeeReport.GetShitDateInfo",ref strSql) ==-1)
//				{
//					this.Err = this.Sql.Err;
//					return null;
//				}
//				else
//				{
//					strSql = string.Format(strSql,Begin,End);
//				}
//
//				this.ExecQuery(strSql);
//				FS.HISFC.Models.CShitData info = null;
//				//��������ʱ��CShiftData ���洢���ݡ�
//				while(this.Reader.Read())
//				{
//					//סԺ��	����	ԭ��	����	��־	����	����	����
//					info = new CShitData();
//					info.ClinicNo =Reader[0].ToString(); // סԺ��
//					info.ShitCause =Reader[1].ToString(); //��������
//					info.OldDataName = Reader[2].ToString();//ԭ��
//					info.OldDataCode = Reader[3].ToString();//�ݴ�ԭ����
//					info.Name = Reader[4].ToString(); //���־
//					info.NewDataName = Reader[5].ToString();//����
//					info.NewDataCode = Reader[6].ToString();//�ݴ��´���
//					info.OperCode = Reader[7].ToString();  //����
//					List.Add(info);
//					info = null;
//				}
//			}
//			catch(Exception ee)
//			{
//				this.Err = ee.Message;
//				return null;
//			}
//			return List;
		}
		/// <summary>
		/// ���ȫԺ�շ���ϸ��ͳ�ƴ������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeeAccontItemList(string Begin,string End,string NurseCell)
		{
			string strSql = "",strWhere = "",strGroup = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontItemList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontItemListWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
					strSql +=" "+strWhere;
				}	
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeAccontItemListGroupBy",ref strGroup) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				strSql +=" "+ strGroup;

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
		/// ���δ��Ԥ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetUnBalancePrepay(string Begin,string End,string NurseCell)
		{
			string strSql = "";
			string strWhere = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{
				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetUnBalancePrepay",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetUnBalancePrepayWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
				}
				strSql = strSql +" "+strWhere;

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
		/// ��û���סԺ������ϸ��
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <returns></returns>
		public DataSet GetPatientConsumeItemList(string iPatientNo )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientConsumeItemList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,iPatientNo);
				}

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
		/// ��û���סԺ������ϸ��
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <returns></returns>
		public DataSet GetPatientConsumeItemListSort(string iPatientNo )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientConsumeItemListSort",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,iPatientNo);
				}

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
		/// ��û��߷�����ϸ����������������--Edit By MaoKb
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="filter"></param>
		/// <param name="balance"></param>
		/// <returns></returns>
		public DataSet GetPatientConsumeItemListSort(string iPatientNo ,string Begin,string End,string filter,string balance) 
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientConsumeItemListByDate",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,iPatientNo,Begin,End,filter,balance);
				}

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
		/// ��û���סԺ������ϸ��
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <returns></returns>
		public DataSet GetPatientConsumeItemListSum(string iPatientNo )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.PatientConsumeItemListSum",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,iPatientNo);
				}

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
		/// ����ʱ��
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPatientConsumeItemListSum(string iPatientNo,string Begin,string End ) 
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.PatientConsumeItemListByDateSum",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,iPatientNo,Begin,End);
				}

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
		/// ��Ժ��Ա��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="pactlist"></param>
		/// <returns></returns>
		public DataSet GetOutPersonRegedit(string Begin,string End ,string deptlist,string pactlist)
		{
			//��Ժ��Ա��־��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				int Result = 0;
				switch(pactlist)
				{
					case "��������": Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit1",ref strSql);break;
					case "��ҽ��":   Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit2",ref strSql);break;
					case "��ݸҽ��": Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit3",ref strSql);break;
					case "ȫ��":     Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit4",ref strSql);break;
				}
				if(Result ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}
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
		/// ��Ժ��Ա��־��  ������� �ǵ����Ժ�� 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="pactlist"></param>
		/// <returns></returns>
		public ArrayList GetOutPersonRegeditBefore(string Begin,string End ,string deptlist,string pactlist)
		{
			//��Ժ��Ա��־��
			ArrayList list = new ArrayList(); //Ҫ���ص�����
			try
			{
				string strSql = "";
				int Result = 0;
				switch(pactlist)
				{
					case "��������":Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore1",ref strSql);break;
					case "��ҽ��"  :Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore2",ref strSql);break;
					case "��ݸҽ��":Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore3",ref strSql);break;
					case "ȫ��"    :Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore4",ref strSql);break;
				}
				if(Result ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}
				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Inpatient.FeeInfo info =null;
				while(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
					info.ID = this.Reader[0].ToString(); //סԺ��
					info.Name =this.Reader[1].ToString();//����
					info.FeeOper.ID = this.Reader[2].ToString();//����
					info.User01 = this.Reader[3].ToString(); //�Ա�
					info.FeeOper.Name =this.Reader[4].ToString();// ����
					((FS.HISFC.Models.RADT.PatientInfo)info.Patient).PVisit.PatientLocation.NurseCell.Name = this.Reader[5].ToString();//����
					info.User02 = this.Reader[6].ToString();//��Ժ���� 
					info.FeeOper.ID = this.Reader[7].ToString();//--����
					info.FT.TotCost =Convert.ToDecimal(this.Reader[8]);//�ܷ���
					info.FT.RebateCost =Convert.ToDecimal(this.Reader[9]);//���ն�
					info.FT.PrepayCost=Convert.ToDecimal(this.Reader[10]);//��Ԥ��
					info.FT.OwnCost = Convert.ToDecimal(this.Reader[11]);//���ʶ� 
					info.FT.PubCost = Convert.ToDecimal(this.Reader[12]);//Ԥ����
					info.FT.ReturnCost= Convert.ToDecimal(this.Reader[13]);//����
					info.User03 = this.Reader[14].ToString();//��
					list.Add(info);
					info = null;
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return list;
		}
		/// <summary>
		///  �Ǽ�δ��Ժ��Ա��־�� 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetUnderOutPersonRegedit(string Begin,string End,string deptlist)
		{
			//δ��Ժ�Ǽ���Ա��־��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetUnderOutPersonRegedit",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptlist);
				}

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
		/// Ӥ����Ժ��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetBabyPersonRegedit(string Begin,string End)
		{
			//Ӥ����Ժ��־�� 
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetBabyPersonRegedit",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// Ԥ����ϸ��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <param name="deptList2"></param>
		/// <returns></returns>
		public DataSet GetPrepayDetail(string Begin,string End,string deptList,string deptList2 )
		{
			//Ԥ����ϸ��־�� ������������ 
			//1��������Ժ���� ����Ԥ����
			//2���ǵ�����Ժ���˽���Ԥ����
			System.Data.DataSet  ds = new DataSet();  //�洢������Ժ���˼�¼
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPrepayDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptList,deptList2);
				}

				this.ExecQuery(strSql,ref ds); //��ѯ

				DataSet myds = new DataSet(); //�洢�ǵ�����Ժ���˼�¼

				string Sql2= "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPrepayDetailmanager2",ref Sql2) !=-1)
				{
					Sql2 = string.Format(Sql2,Begin,End,deptList,deptList2);
					this.ExecQuery(Sql2,ref myds); //��ѯ
					if(myds!=null) //������ 
					{
						if(myds.Tables.Count > 0) //������
						{
							if(myds.Tables[0].Rows.Count>0) //������
							{
								ds.Tables[0].Rows.Add(new object[]{"�ǵ�����Ժ"});
								foreach(DataRow row in myds.Tables[0].Rows)
								{
									ds.Tables[0].Rows.Add(new object[]{row[0],row[1],row[2],row[3],row[4],row[5],row[6]}); //�����ݼӵ����
								}
							}
						}
					}
				}

			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}


		/// <summary>
		/// ��ϸ��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <param name="OperCode"></param>
		/// <returns></returns>
		public DataSet GetDayDetail(string Begin,string End,string deptList,string OperCode)
		{
			//Ԥ����ϸ��־�� ������������ 
			//1��������Ժ���� ����Ԥ����
			//2���ǵ�����Ժ���˽���Ԥ����
			System.Data.DataSet  ds = new DataSet();  //�洢������Ժ���˼�¼
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptList,OperCode);
				}

				this.ExecQuery(strSql,ref ds); //��ѯ

			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}

		/// <summary>
		/// ֱ�ӣ�ȷ�ϣ��շ���־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <returns></returns>
		public DataSet GetFeeIndirect(string Begin,string End,string deptList)
		{
			//ֱ�ӣ�ȷ�ϣ��շ���־��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeIndirect",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptList);
				}

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
		/// ��ɽһԺ��Ժ�Ǽ���Ա��־��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="DeptID"></param>
		/// <param name="DeptId2"></param>
		/// <returns></returns>
		public DataSet GetAllPersonInHospital(string Begin,string End,string DeptID,string DeptId2 )
		{
			//��ɽһԺ��Ժ�Ǽ���Ա��־��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetAllPersonInHospital",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,DeptID,DeptId2);
				}

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
		/// ��ɽҽԺ���˳�Ժ������ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strBalanceType">��������</param>
		/// <param name="deptList">�����б�</param>
		/// <returns></returns>

		public DataSet GetPatientItemFeeByBalanceType(string Begin,string End,string strBalanceType,string deptList )
		{
			//��ɽһ��Ժ����
			System.Data.DataSet  dts = new DataSet();
			try
			{
				string strSql = "";
//				string strWhere = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByBalanceType",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,deptList);
				}
//				if(strBalanceType!="1")
//				{
//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByBalanceTypeWhere",ref strWhere) ==-1)
//					{
//						this.Err = this.Sql.Err;
//						return null;
//					}
//					else
//					{
//						strWhere = string.Format(strWhere,strBalanceType,deptList);
//					}
//				}
//				strSql = strSql +" "+strWhere;

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return dts;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetPatientItemFeeBytt(string Begin,string End,string NurseCell)
		{
			string strSql = "";
			string strWhere = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{
				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByNEWTT",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell.ToUpper()!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientItemFeeByttWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
					strSql += " "+ strWhere;
				}
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
		/// ��1��	��Լ��λҽ�Ʒѽ����Ժ�ڱ���
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetSpecialPactUnit(string Begin,string End)
		{
			//��1��	��Լ��λҽ�Ʒѽ����Ժ�ڱ���
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnit",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// 15.	��Լ��λ���� ��2��	��Լ��λ�Ķ��ʵ���Ժ��ĸ���֪ͨ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetSpecialPactUnitInform(string Begin,string End)
		{
			//��Լ��λ�Ķ��ʵ���Ժ��ĸ���֪ͨ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitInform",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ȫԺ����ҽ�Ʒ����ܽ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubMedicalFee(string Begin,string End)
		{
			//ȫԺ����ҽ�Ʒ����ܽ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubMedicalFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ����ҽ�Ʒ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubMedicalFeeDetail(string Begin,string End)
		{
			//����ҽ�Ʒ�����ϸ��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubMedicalFeeDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ҽ�ƻ���סԺҽҩ�ѱ�����ϸ��סԺ���ֵ��ϱ�ʡ��ҽ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strPact"></param>
		/// <returns></returns>
		public DataSet GetPubPactunitMedicalFeeDetail(string Begin,string End,string strPact)//string MCard,int iBit)
		{
			//��3��	ҽ�ƻ���סԺҽҩ�ѱ�����ϸ��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubPactunitMedicalFeeDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,strPact);
				}

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
		/// ҽ�ƻ���סԺҽҩ�ѱ�����ϸ��(ʡ��ҽ)
		/// </summary>
		/// <param name="Begin">��ʼʱ��</param>
		/// <param name="End">����ʱ��</param>
		/// <param name="strPact">ҽ�ƿ���ǰ��λ</param>
		/// <param name="flag">1����ְ ��2������</param>
		/// <returns></returns>
		public DataSet GetPubPactunitSGYList(string Begin,string End,string strPact,string flag) 
		{//string MCard,int iBit)
			
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(flag=="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubPactunitSGY.1",ref strSql) ==-1) 
					{
						this.Err = this.Sql.Err;
						return null;
					}
				}
				else 
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubPactunitSGY.2",ref strSql) ==-1) 
					{
						this.Err = this.Sql.Err;
						return null;
					}					
				}
				strSql = string.Format(strSql,Begin,End,strPact);

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
		/// ����ҽ��סԺҽҩ�ѱ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="MCardType"></param>
		/// <returns></returns>
		public DataSet GetPubPactunitDrugFeeDetail(string Begin,string End,string MCardType)
		{
			//��4��	����ҽ��סԺҽҩ�ѱ�����ϸ��סԺ���ֵ��ϱ��й�ҽ�͸�����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubPactunitDrugFeeDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End,MCardType);
				}

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
		/// ��5��	�㶫ʡ��ҽ��ί������סԺҽ�Ʒѱ�����ȫԺ���ϱ�ʡ��ҽ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubDelegateMedicalFee(string Begin,string End)
		{
			//�㶫ʡ��ҽ��ί������סԺҽ�Ʒѱ�����ȫԺ���ϱ�ʡ��ҽ����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubDelegateMedicalFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// �����й���ҽ��Ԥ��ʵʩ����ίԱ��ί������סԺҽҩ�ѱ�����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubDelegateClinicMedicalFee(string Begin,string End)
		{
			//�����й���ҽ��Ԥ��ʵʩ����ίԱ��ί������סԺҽҩ�ѱ�����
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubDelegateClinicMedicalFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// �����й���ҽ�Ƹɲ�ְ�������������Ʊ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strID"></param>
		/// <returns></returns>
		public DataSet GetPubInstrumnetFeeDetail(string Begin,string End,string strID)
		{
			//�����й���ҽ�Ƹɲ�ְ�������������Ʊ�����ϸ��
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubInstrumnetFeeDetail",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

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
		/// ��÷�Ʊ���õĲ���Ա
		/// </summary>
		/// <param name="Begin">��ʼʱ��</param>
		/// <param name="End">����ʱ��</param>
		/// <returns></returns>
		public ArrayList GetBillOper(string Begin,string End)
		{
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj = null;
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.FeeReport.GetOperQueryBill",ref strSql)==-1) return null;
			try
			{
				strSql = string.Format(strSql,Begin,End);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{				
					obj = new NeuObject();
					obj.ID = Reader["GET_PERSON_CODE"].ToString(); //����Ա����	
					obj.Name = Reader["employname"].ToString();//����Ա����
					al.Add(obj);
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;
				
			}
			return al;

		}

		/// <summary>
		/// ���һ�����߷�����Ϣ
		/// </summary>
		/// <param name="strNo"></param>
		/// <returns></returns>
		public DataSet GetSiPatientFee(string strNo)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetSiPatientFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,strNo);
				}

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
		/// ���ҽ����Ժ����
		/// </summary>
		/// <param name="strNo"></param>
		/// <returns></returns>
		public ArrayList GetSiPatientFeeRetal(string strNo)
		{
			ArrayList arr = new ArrayList();
			string strSql = "";
			try
			{
				if (this.Sql.GetCommonSql("Fee.FeeReport.GetSiPatientFee",ref strSql)==-1) return null;
				FS.HISFC.Models.SIInterface.SIMainInfo obj ;
				strSql = string.Format(strSql,strNo);
				if(this.ExecQuery(strSql)==-1) return null;
				while(this.Reader.Read())
				{					
					obj = new FS.HISFC.Models.SIInterface.SIMainInfo();
					obj.ID = Reader["סԺ��"].ToString();
					obj.Name = Reader["����"].ToString();
					obj.Memo = Reader["ҽ����"].ToString();
					obj.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["Ԥ����"].ToString());
					obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["סԺ�ܷ���"].ToString());
					obj.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ҽ�Ʒ���"].ToString());
					obj.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�����Ը�"].ToString());
					obj.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ͳ���Ը�"].ToString());
					obj.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�����Ը�"].ToString());
					arr.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ex)
			{
				string Error = ex.Message;				
			}
			return arr;		
		}

		/// <summary>
		/// ���Ӧ��������ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeePayItemList(string Begin,string End,string NurseCell)
		{
			string strSql = "",strWhere = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPayFeeList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPayFeeListWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
					strSql +=" "+strWhere;
				}	
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
		/// ���Ӧ�����û���Fee.FeeReport.GetPrepayList
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="NurseCell"></param>
		/// <returns></returns>
		public DataSet GetFeePayStat(string Begin,string End,string NurseCell)
		{
			string strSql = "",strWhere = "",strGroup = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPayStat",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(NurseCell!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPayStatWhere",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,NurseCell);
					}
					strSql +=" "+strWhere;
				}	
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPayStatGroup",ref strGroup) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				strSql +=" "+strGroup;
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
		/// ���Ԥ������ϸ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <returns></returns>
		public DataSet GetPrepayList(string Begin,string End,string Oper)
		{
			string strSql = "",strWhere = "";
			System.Data.DataSet  ds = new DataSet();
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPrepayList",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(Oper!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPrepayListWhereByOper",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,Oper);
					}
					strSql +=" "+strWhere;
				}					
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
		/// ������Ԥ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <returns></returns>
		public string GetFeeRetCost(string Begin,string End,string Oper)
		{
			string strSql = "",strWhere = "";			
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeRetCost",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(Oper!="1")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetFeeRetCostByOper",ref strWhere)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,Oper);
					}
					strSql +=" "+strWhere;
				}	
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return this.ExecSqlReturnOne(strSql);;
		}


		/// <summary>
		/// ���ʵ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <param name="PayWay"></param>
		/// <returns></returns>
		public string GetFeeSumBalanPay(string Oper,string Begin,string End,string PayWay)
		{
			string strSql = "";			
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetReturnFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Oper,Begin,End,PayWay);
				}	
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return this.ExecSqlReturnOne(strSql);;
		}
		

		/// <summary>
		/// ����������ý�����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <param name="PayWay"></param>
		/// <param name="TransType"></param>
		/// <param name="TransKind"></param>
		/// <returns></returns>
		public string RetBalancePay(string Begin,string End,string Oper,string PayWay,string TransType,string TransKind)
		{
			string strSql = "",strWhere = "",strPayWay = "",strOper = "",strTransType = "",strTransKind = "";			
			try
			{				
				if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetReturnFeeBase",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(Oper!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetReturnFeeBaseByOper",ref strOper)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strOper = string.Format(strOper,Oper);
					}
					strSql +=" "+strWhere;
				}	
				if(PayWay!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetReturnFeeBaseByPayWay",ref strPayWay)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strPayWay = string.Format(strPayWay,PayWay);
					}
					strSql +=" "+strPayWay;
				}
				if(TransType!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetReturnFeeBaseByTransType",ref strTransType)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strTransType = string.Format(strTransType,TransType);
					}
					strSql +=" "+strTransType;
				}	
				if(TransKind!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetReturnFeeBaseByTransType",ref strTransKind)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strTransType = string.Format(strTransKind,TransKind);
					}
					strSql +=" "+strTransKind;
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return this.ExecSqlReturnOne(strSql);;
		}


		/// <summary>
		/// �����嵥�ܶ�
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public DataSet GetTotBalanceBill(string strID)
		{
			DataSet dts = new DataSet();

			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetTotBalanceBill.1",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,strID);
				}

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}

			return dts;
		}

		/// <summary>
		/// ��������嵥
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public DataSet GetTotBalancedBill(string strID)
		{
			DataSet dts = new DataSet();

			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetTotBalancedBill.1",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,strID);
				}

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			
			return dts;
		}

		/// <summary>
		/// �����Ժ��ҩ
		/// </summary>
		/// <param name="strID"></param>
		/// <param name="strTag"></param>
		/// <returns></returns>
		public DataSet GetBrought(string strID,string strTag)
		{
			//�˺��������鲻�����ݵ���֪��ʲô�ط����ã�������ε� --By Maokb
			this.Err = "�˴�����FS.HISFC.BizLogic.Fee.FeeReport.GetBrouht����������Ҫ����";
			return null;
			//			DataSet dts = new DataSet();
			//
			//			try
			//			{
			//				string strSql = "";
			//				if(this.Sql.GetCommonSql("Fee.FeeReport.Brought.1",ref strSql) ==-1)
			//				{
			//					this.Err = this.Sql.Err;
			//					return null;
			//				}
			//				else
			//				{
			//					strSql = string.Format(strSql,strID,strTag);
			//				}
			//
			//				this.ExecQuery(strSql,ref dts);
			//			}
			//			catch(Exception ee)
			//			{
			//				this.Err = ee.Message;
			//				return null;
			//			}
			//			
			//			return dts;
		}

		/// <summary>
		/// ��Ժ��ҩ��Ϣ
		/// </summary>
		/// <param name="strID">סԺ��ˮ��</param>
		/// <returns></returns>
		public DataSet GetBrought(string strID)
		{
			DataSet dts = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetBrouht.ByPatientNo",ref strSql)==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,strID);
				}
				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return dts;
		}
		/// <summary>
		///  ��ȡһ�����˵�ҽ�Ʊ��ռ�¼
		/// </summary>
		/// <param name="PatientNO"></param>
		/// <returns></returns>
		public DataSet GetOldFeeItemList(string PatientNO)
		{
			DataSet dts = new DataSet();

			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.getOldFeeItemList2",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,PatientNO);
				}

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			
			return dts;
		}
		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet getOldFeeItemList(string Begin,string End) 
		{
			DataSet dts = new DataSet();

			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.getOldFeeItemList",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,Begin,End);
				}

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			
			return dts;
		}
		/// <summary>
		/// ��÷�Ʊ�ϼ�
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="Oper"></param>
		/// <returns></returns>

		public DataSet GetBalanceBillTotCost(string Begin,string End,string Oper)
		{
			string strGroup = "",strWhere = "";
			DataSet dts = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetBalanceBillTotCost",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}
				if(Oper.ToUpper()!="ALL")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetBalanceBillTotCost.where",ref strWhere) ==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strWhere = string.Format(strWhere,Oper);
					}
					strSql += strWhere+" ";

				}
				if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetBalanceBillTotCost.Groupby",ref strGroup) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strGroup = string.Format(strGroup);
				}
				strSql += strGroup+" ";
			
				

				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			
			return dts;
		}
		#region delete BY maokb

		//		/// <summary>
		//		/// ����ս��ݴ�����;�����
		//		/// </summary>
		//		/// <param name="Begin"></param>
		//		/// <param name="End"></param>
		//		/// <param name="Oper"></param>
		//		/// <param name="FeeType"></param>
		//		/// <param name="Tag"></param>
		//		/// <returns></returns>
		//		public DataSet GetDayReportTot(string Begin,string End,string Oper,string FeeType,string Tag)
		//		{
		//			string strGroup = "",strWhere = "",strFeetype = "",strTem = "",strTag = "",strRet = "";
		//			DataSet dts = new DataSet();
		//			try
		//			{
		//				string strSql = "";
		//				if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTot",ref strSql) ==-1)
		//				{
		//					this.Err = this.Sql.Err;
		//					return null;
		//				}
		//				else
		//				{
		//					strSql = string.Format(strSql,Begin,End);
		//				}
		//				if(Oper.ToUpper()!="ALL")
		//				{
		//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTotWhere",ref strWhere) ==-1)
		//					{
		//						this.Err = this.Sql.Err;
		//						return null;
		//					}
		//					else
		//					{
		//						strWhere = string.Format(strWhere,Oper);
		//					}
		//					strSql += strWhere+" ";
		//
		//				}
		//				if(FeeType.ToUpper()!="ALL")
		//				{
		//					if(FeeType=="0")//������
		//					{
		//						if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTotByFeetype",ref strFeetype) ==-1)
		//						{
		//							this.Err = this.Sql.Err;
		//							return null;
		//						}
		//						else
		//						{
		//							strWhere = string.Format(strFeetype);
		//						}
		//						strSql += strFeetype+" ";
		//					}
		//					else//�ݴ�����
		//					{
		//						if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTotByTem",ref strTem) ==-1)
		//						{
		//							this.Err = this.Sql.Err;
		//							return null;
		//						}
		//						else
		//						{
		//							strTem = string.Format(strTem);
		//						}
		//						strSql += strTem+" ";
		//					}
		//				}
		//								
		//				if(Tag!="ALL") 
		//				{
		//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTotGroupby",ref strGroup) ==-1) 
		//					{
		//						this.Err = this.Sql.Err;
		//						return null;
		//					}
		//					else 
		//					{
		//						strGroup = string.Format(strGroup);
		//					}
		//					if(Tag=="0") 
		//					{//������
		//						if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportAdd",ref strTag) ==-1) 
		//						{
		//							this.Err = this.Sql.Err;
		//							return null;
		//						}
		//						else 
		//						{
		//							strTag = string.Format(strTag);
		//						}
		//						strGroup += strTag+" ";
		//					}
		//					else 
		//					{//�ݴ�����
		//						if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportSub",ref strRet) ==-1) 
		//						{
		//							this.Err = this.Sql.Err;
		//							return null;
		//						}
		//						else 
		//						{
		//							strRet = string.Format(strRet);
		//						}
		//						strGroup += strRet+" ";
		//					}
		//
		//				}
		//				else 
		//				{
		//					if(this.Sql.GetCommonSql("Fee.FeeReport.GetDayReportTotGroupbyBNO",ref strGroup) ==-1) 
		//					{
		//						this.Err = this.Sql.Err;
		//						return null;
		//					}
		//					else 
		//					{
		//						strGroup = string.Format(strGroup);
		//					}
		//				}
		//				strSql += strGroup+" ";
		//				
		//
		//				this.ExecQuery(strSql,ref dts);
		//			}
		//			catch(Exception ee)
		//			{
		//				this.Err = ee.Message;
		//				return null;
		//			}
		//			
		//			return dts;
		//		}
		#endregion

		/// <summary>
		/// ��û��߷��ýṹ  Edit By MaoKb--05.8.20
		/// </summary>
		/// <param name="id"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public DataSet GetFeeStructure(string id,string begin,string end,string flag)
		{
			string strSql="";
			DataSet dts = new DataSet();
			try
			{
				if(flag=="All")
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientFeeStructure",ref strSql)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strSql=string.Format(strSql,id,begin,end);
					}
				}
				else
				{
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientFeeStructure.1",ref strSql)==-1)
					{
						this.Err = this.Sql.Err;
						return null;
					}
					else
					{
						strSql=string.Format(strSql,id,begin,end,flag);
					}
				}
				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
			return dts;

		}

		/// <summary>
		/// סԺ���÷������---����С��------------ Edit By MaoKb
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="filter"></param>
		/// <param name="balance"></param>
		/// <returns></returns>
		public DataSet GetFeeItemListByDate(string iPatientNo,string Begin,string End ,string filter,string balance) 
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.PatientConsumeItemListByDateSum.1",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,iPatientNo,Begin,End,filter,balance);
				}

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
		/// ����һ���嵥
		/// </summary>
		/// <param name="inPatientNo"></param>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public DataSet GetPatientDuimalFee(string inPatientNo,string begin,string end)
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientDuimalFee",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,inPatientNo,begin,end);
				}

				if(this.ExecQuery(strSql,ref ds)==-1)return null;
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		/// <summary>
		/// סԺ���߷��÷������-- Edit By MaoKb
		/// </summary>
		/// <param name="iPatientNo"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="filter"></param>
		/// <param name="balance"></param>
		/// <returns></returns>
		public DataSet GetFeeItemListSortByDate(string iPatientNo ,string Begin,string End,string filter,string balance) 
		{
			System.Data.DataSet  ds = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPatientConsumeItemListByDate.1",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,iPatientNo,Begin,End,filter,balance);
				}

				if(this.ExecQuery(strSql,ref ds)==-1)return null;
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		
		#region ���ѱ�����
		/// <summary>
		/// ����ҽ��סԺҽҩ�ѱ�����ϸ��
		/// </summary>
		/// <returns></returns>
		public DataSet GetSpecialPactunit1(string Begin,string End, string MCardNO) 
		{
			DataSet dts = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.DayReport.GetSpecialPactunit1",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql,Begin,End,MCardNO);
				}
				
				this.ExecQuery(strSql,ref dts);
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			
			return dts;
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.Report getPubDelegateMedicalFee(string Begin,string End,string MCardType) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			string strSql = "",strTag = "";

			if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubDelegateMedicalFee",ref strSql)==-1) return null;

			//���ݴ���Ĵ��ֽ��
			string[] s = MCardType.Split('_');
			if(s.Length>1) 
			{
				for(int i=0;i<s.Length;i++) 
				{
					
					strTag += "'"+s[i].ToString()+"'"+",";				
						
				}
				strTag = strTag.TrimEnd(',');
			}
			else 
			{
				strTag = "'"+MCardType+"'";
			}
			strSql = string.Format(strSql,Begin,End,strTag);

			return this.objPubDelegateMedicalFee(strSql);
			#region �޸ĺ�
			//			if(this.ExecQuery(strSql)==-1) return null;
			//			while(this.Reader.Read()) {					
			//				obj.User01 = Reader["�˴�"].ToString();
			//				obj.User02 = Reader["����"].ToString();
			//				obj.FeeCost1 = Reader["��ҩ"].ToString();
			//				obj.FeeCost2 = Reader["��ҩ"].ToString();
			//				obj.FeeCost3 = Reader["����ҩ"].ToString();
			//				obj.FeeCost4 = Reader["һ�����"].ToString();
			//				obj.FeeCost5 = Reader["����"].ToString();
			//				obj.FeeCost6 = Reader["һ�����Ʒ�"].ToString();
			//				obj.FeeCost7 = Reader["���Ʒ�"].ToString();
			//				obj.FeeCost8 = Reader["��λ��"].ToString();
			//				obj.FeeCost9 = Reader["����"].ToString();
			//
			//				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["С��"].ToString());
			//				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�Ը����"].ToString());
			//
			//				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ʵ�ʼ���"].ToString());
			//						
			//			}
			//			this.Reader.Close();	
			//
			//			return obj;
			#endregion
		}
		/// <summary>
		/// ����ʡ��ҽ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="MCardType"></param>
		/// <param name="bit"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.Report getPubDelegateMedicalFee(string Begin,string End,string MCardType,string bit) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			string strSql = "",strTag = "";

			if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubDelegateMedicalFeeLX",ref strSql)==-1) return null;

			//���ݴ���Ĵ��ֽ��
			string[] s = MCardType.Split('_');
			if(s.Length>1) 
			{
				for(int i=0;i<s.Length;i++) 
				{
					
					strTag += "'"+s[i].ToString()+"'"+",";				
						
				}
				strTag = strTag.TrimEnd(',');
			}
			else 
			{
				strTag = MCardType;
			}
			strSql = string.Format(strSql,Begin,End,strTag);

			return this.objPubDelegateMedicalFee(strSql);
		
		}
		/// <summary>
		/// �й�ҽ
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="MCardType"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.Report getPubDelegateMedicalFeeSIGY(string Begin,string End,string MCardType) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			string strSql = "",strTag = "";

			if(this.Sql.GetCommonSql("Fee.FeeReport.GetPubDelegateMedicalFeeLX",ref strSql)==-1) return null;

			//���ݴ���Ĵ��ֽ��
			string[] s = MCardType.Split('_');
			if(s.Length>1) 
			{
				for(int i=1;i<s.Length;i++) 
				{
					
					strTag += "'"+s[i].ToString()+"'"+",";				
						
				}
				strTag = strTag.TrimEnd(',');
			}
			else 
			{
				strTag = MCardType;
			}
			strSql = string.Format(strSql,Begin,End,strTag);

			return this.objPubDelegateMedicalFeeSI(strSql);
		
		}
		/// <summary>
		/// ʡ��ҽ
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		private FS.HISFC.Models.Fee.Report objPubDelegateMedicalFee(string strSql) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			if(this.ExecQuery(strSql)==-1) return null;
			while(this.Reader.Read()) 
			{					
				obj.User01 = Reader["�˴�"].ToString();
				obj.User02 = Reader["����"].ToString();
				obj.FeeCost1 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��ҩ"].ToString());
				obj.FeeCost2 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��ҩ"].ToString());
				obj.FeeCost3 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����ҩ"].ToString());
				obj.FeeCost4 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["һ�����"].ToString());
				obj.FeeCost5 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����"].ToString());
				obj.FeeCost6 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["һ�����Ʒ�"].ToString());
				obj.FeeCost7 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["���Ʒ�"].ToString());
				obj.FeeCost8 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��λ��"].ToString());
				obj.FeeCost9 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����"].ToString());

				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["С��"].ToString());
				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�Ը����"].ToString());

				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ʵ�ʼ���"].ToString());
						
			}
			this.Reader.Close();	

			return obj;
		}
		/// <summary>
		/// �й�ҽ
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		private FS.HISFC.Models.Fee.Report objPubDelegateMedicalFeeSI(string strSql) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			if(this.ExecQuery(strSql)==-1) return null;
			while(this.Reader.Read()) 
			{					
				obj.User01 = Reader["�˴�"].ToString();
				obj.User02 = Reader["����"].ToString();

				obj.FeeCost1 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��ҩ"].ToString());
				obj.FeeCost2 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��ҩ"].ToString());
				obj.FeeCost3 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����ҩ"].ToString());
				obj.FeeCost4 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["һ�����"].ToString());
				obj.FeeCost5 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����"].ToString());
				obj.FeeCost6 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["һ�����Ʒ�"].ToString());
				obj.FeeCost7 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["���Ʒ�"].ToString());
				obj.FeeCost8 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["��λ��"].ToString());
				obj.FeeCost9 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["����"].ToString());

				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["С��"].ToString());
				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�Ը����"].ToString());
				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ʵ�ʼ���"].ToString());
						
			}
			this.Reader.Close();	

			return obj;
		}
		/// <summary>
		/// ȫԺ����ҽ�Ʒ����ܽ����
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strID"></param>
		/// <param name="Type"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Fee.Report getPubMedical(string Begin,string End,string strID,string Type)
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			string strSqlWhere = "",strSqlGroup = "",strSql = "";
			
			if(strID!="") 
			{
				try 
				{
					if (this.Sql.GetCommonSql("Fee.FeeReport.getPubMedical",ref strSql)==-1) return null;
				
					strSql = string.Format(strSql,Begin,End);
					//���ݴ���Ĵ��ֽ��
					//��ҽ
					if(Type== "GY") 
					{
						//						for(int i=1;i<s.Length;i++) {
						if (this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitsubstr",ref strSqlWhere)==-1) return null;
						//							strTag = s[i].ToString();
						strSqlWhere = string.Format(strSqlWhere,strID);
					}
					//��Լ��λ
					if(Type == "TT") 
					{
						if (this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitlength",ref strSqlWhere)==-1) return null;
						
						strSqlWhere = string.Format(strSqlWhere,3);
					}
					//��Ժְ��
					if(Type == "BY") 
					{
						if (this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitByBM",ref strSqlWhere)==-1) return null;
						
						strSqlWhere = string.Format(strSqlWhere,strID);
					}
					strSql = strSql +" And "+strSqlWhere;
					if(this.Sql.GetCommonSql("Fee.FeeReport.getPubMedicalGroup",ref strSqlGroup)==-1) return null;
					strSql = strSql + " "+strSqlGroup;
					if(this.ExecQuery(strSql)==-1) return null;
					while(this.Reader.Read()) 
					{					
						obj.User01 = Reader["�˴�"].ToString();
						obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["С��"].ToString());
						obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["�Ը�"].ToString());
						obj.FT.SupplyCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["���"].ToString());

						obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["ʵ��"].ToString());
						
					}
					this.Reader.Close();					
				
				}
				catch(Exception ex) 
				{
					string Error = ex.Message;				
				}
			}

			return obj;
		}

		/// <summary>
		/// �����������Ŀ
		/// </summary>
		/// <param name="dtBegin">��ʼʱ��</param>
		/// <param name="dtEnd">����ʱ��</param>
		/// <returns>���ݼ�</returns>
		public DataSet GetSpCheckItem(DateTime dtBegin, DateTime dtEnd)
		{
			DataSet dsTemp = new DataSet();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetSpCheckItem", ref strSql) == -1) 
				{
					this.Err = this.Sql.Err;
					dsTemp = null;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString());
				}
				
				this.ExecQuery(strSql, ref dsTemp);

				return dsTemp;
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			finally
			{
				dsTemp = null;
			}
		}
		
		#endregion
		/// <summary>
		/// ��ѯͳ�ƴ���  
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="InpatientNO"></param>
		/// <returns></returns>
		public DataSet GetCodeState1(string InpatientNO ,string Code )
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetCodeState",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,InpatientNO,Code);
				}

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
		/// ��ѯͳ�ƴ���  
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="InpatientNO"></param>
		/// <returns></returns>
		public ArrayList  GetCodeState2(string InpatientNO,string Code )
		{
			try
			{
				ArrayList list = new ArrayList();
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetCodeState",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,InpatientNO,Code);
				}

				this.ExecQuery(strSql);
				FS.HISFC.Models.Fee.Useless.OldFee oldFee = new FS.HISFC.Models.Fee.Useless.OldFee();
				while(this.Reader.Read())
				{
					oldFee.ID = this.Reader[0].ToString();
					oldFee.Name = this.Reader[1].ToString();
					oldFee.COST = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
					list.Add(oldFee);
				}
				this.Reader.Close();
				return list;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}
	//	/// <summary>
//		/// ��ȡ����
//		/// </summary>
//		/// <param name="obj"></param>
//		/// <returns></returns>
//		private string [] GetString(FS.HISFC.Models.Fee.Useless.OldFee obj)
//		{
//			string [] str = new string[40];
//			str[0] = obj.ID;//������
//			str[1] = obj.User01;//���׷�ʽ
//			str[2] = obj.patientInfo.Patient.PID.CardNO;//���￨��
//			str[3] = obj.patientInfo.Patient.PID.PatientNO;//סԺ��
//			str[4] = "All";//��С����
//			str[5] = obj.patientInfo.Name;//����
//			str[6] = obj.patientInfo.Patient.Sex.ID.ToString();//�Ա�
//			str[7] = obj.patientInfo.PayKind.ID;//�������
//			str[8] = obj.patientInfo.Patient.ProCeatePcNO;//�������յ��Ժ�
//			str[9] = obj.patientInfo.Patient.SSN;//ҽ��֤��
//			str[10] = obj.patientInfo.Patient.Pact.ID;//��ͬ��λ
//			str[11] = "All";//ִ�п���
//			str[12] = "All";//ҽ������
//			str[13] = obj.COST.ToString();//�����ܶ�
//			str[14] = obj.PUBCOST.ToString();//����
//			str[15] = obj.PAYCOST.ToString();//�Ը�
//			str[16] = "0";//�Է�
//			str[17] = "0";//���˱�ʾ
//			str[18] = obj.Memo;//��ע
//			str[19] = this.Operator.ID;//����Ա���
//			str[20] = System.DateTime.Now.ToString();//������
//			str[21] = obj.BEDFEE.ToString();//��λ��
//			str[22] = obj.DIAGFEE.ToString();//���
//			str[23] = obj.CHECKFEE.ToString();//���
//			str[24] = obj.CUREFEE.ToString();//���Ʒ�
//			str[25] = obj.NURSEFEE.ToString();//���Ʒ�
//			str[26] = obj.OPERATIONFEE.ToString();//������
//			str[27] = obj.ASSAYFEE.ToString();//�����
//			str[28] = obj.DRUGFEE.ToString();//ҩ��
//			str[29] = obj.OTHERFEE.ToString();//������
//			str[30] = obj.CLINICFEE.ToString();//�����
//			str[31] = obj.SPELLFEE.ToString();//����� 
//			str[32] = obj.STANDFEE.ToString();//�걨���
//			str[33] = obj.DECLAREDATE.ToString();//�걨�·�
//			str[34] = obj.CLINICPAYFEE.ToString(); //�����Է�
//			str[35] = obj.patientInfo.PVisit.InTime.ToString(); //��Ժ����
//			str[36] = obj.patientInfo.PVisit.OutTime.ToString(); //��Ժ���� 
//			str[37] = obj.patientInfo.ID;
//			str[38] = obj.ItemType; //��Ŀ���
//			str[39] = obj.SpellItemType;  //������Ŀ���
//			return str;
//		}
//		/// <summary>
//		/// ������������
//		/// </summary>
//		/// <param name="obj"></param>
//		/// <param name="oPatientInfo"></param>
//		/// <returns></returns>
//		public int UpdateOldFee(FS.HISFC.Models.Fee.Useless.OldFee obj ,FS.HISFC.Models.RADT.PatientInfo oPatientInfo)
//		{
//			string strSql = "";
//			if (this.Sql.GetCommonSql("Fee.FeeReport.UpdateOldFee",ref strSql)==-1) return -1;
//			string []str = GetString(obj);
//			try
//			{   				
//				strSql = string.Format(strSql,str);
//
//				return this.ExecNoQuery(strSql);
//			}
//			catch(Exception ex)
//			{
//				this.ErrCode=ex.Message;
//				this.Err=ex.Message;
//				return -1;
//			}
//		}
		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="StaDate"></param>
		/// <param name="oPatientInfo"></param>
		/// <returns></returns>
		public int UpdateOldFeeStaDate(string StaDate ,FS.HISFC.Models.RADT.PatientInfo oPatientInfo)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.FeeReport.UpdateOldFeeStaDate",ref strSql)==-1) return -1;
			try
			{   				
				strSql = string.Format(strSql,oPatientInfo.ID,oPatientInfo.PVisit.InTime.ToString(),StaDate);

				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
//		/// <summary>
//		/// �����������շ���
//		/// </summary>
//		/// <param name="obj1"></param>
//		/// <param name="oPatientInfo1"></param>
//		/// <returns></returns>
//		public int InsertOldFee(FS.HISFC.Models.Fee.Useless.OldFee obj1,FS.HISFC.Models.RADT.PatientInfo oPatientInfo1 )
//		{
//			#region Sql����
//			
//			#endregion
//			string strSql = "";
//			#region "�ӿ�"
//			
//			#endregion	
//
//			if (this.Sql.GetCommonSql("Fee.FeeReport.InsertOldFee",ref strSql)==-1) return -1;
//			try
//			{
//				string []str1 = GetString(obj1);
//				strSql = string.Format(strSql,str1);
//				if(this.ExecNoQuery(strSql) <= 0)
//				{
//					return UpdateOldFee(obj1,oPatientInfo1);
//				}
//				else
//				{
//					return 1;
//				}
//			}
//			catch(Exception ex)
//			{
//				this.Err=ex.Message;
//				this.ErrCode=ex.Message;
//				return -1;
//			}
//			
//		}
		/// <summary>
		/// ��������ҵְ���������ո߶�ҽ�Ʒѽ�����ϸ��
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetOldFeeLimitReport(string Begin,string End)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeLimitReport",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,Begin,End);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
//		/// <summary>
//		/// ����סԺ��ˮ�Ż�ȡ��ǰ��������ݵ���Ϣ
//		/// </summary>
//		/// <param name="InpatientNo"></param>
//		/// <returns></returns>
//		public FS.HISFC.Models.FeeOldFee GetOldFeeList(string InpatientNo)
//		{
//			try
//			{
//				string strSql = "";
//				if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeList",ref strSql) ==-1)
//				{
//					this.Err = this.Sql.Err;
//					return null;
//				}
//				strSql = string.Format(strSql,InpatientNo);
//				this.ExecQuery(strSql);
//				FS.HISFC.Models.Fee.Useless.OldFee oldFee = new FS.HISFC.Models.Fee.Useless.OldFee();
//				while(this.Reader.Read())
//				{
//					oldFee.PUBCOST		= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
//					oldFee.PAYCOST		= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
//					oldFee.CLINICFEE	= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
//					oldFee.CLINICPAYFEE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
//					oldFee.STANDFEE		= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString()); 
//					oldFee.DECLAREDATE  = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
//					oldFee.Memo			= this.Reader[6].ToString();
//					oldFee.ItemType = this.Reader[7].ToString();
//					oldFee.SpellItemType = this.Reader[8].ToString();
//				}
//				this.Reader.Close();
//				return oldFee;
//			}
//			catch(Exception ee)
//			{
//				this.Err = ee.Message;
//				return null;
//			}
//		}

		/// <summary>
		/// ��ȡ��ǰ������Ϣ
		/// </summary>
		/// <param name="ProcreateNo"></param>
		/// <param name="HappenNo"></param>
		/// <returns></returns>
		public DataSet GetClinicOldFeeLimitReport(string ProcreateNo,string HappenNo)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetClinicOldFeeLimitReport",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql,ProcreateNo,HappenNo);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}
		#region �����������շ���
		//		/// <summary>
		//		/// �����������շ���
		//		/// </summary>
		//		/// <param name="obj"></param>
		//		/// <param name="oPatientInfo"></param>
		//		/// <returns></returns>
		//		public int InsertOldFee(FS.FrameWork.Models.NeuObject obj,FS.HISFC.Models.RADT.PatientInfo oPatientInfo )
		//		{
		//			#region Sql����
		//			
		//			#endregion
		//			string strSql = "";
		//			#region "�ӿ�"
		//			
		//			#endregion	
		//			decimal dPub=0,dOwn=0;
		//			decimal dTotal = 0;//�ܶ�
		//			string strPub = this.GetPub(oPatientInfo.Patient.PID.PatientNO);
		//			string strOwn = this.GetOwn(oPatientInfo.Patient.PID.PatientNO);
		//			if(strPub=="")
		//			{
		//				strPub = "0";
		//			}
		//			
		//			if(strOwn=="")
		//			{
		//				strOwn = "0";
		//			}
		//			
		//			dOwn = NConvert.ToDecimal(obj.User03);//+Convert.ToDecimal(strOwn);
		//			dPub = NConvert.ToDecimal(obj.User02);//+Convert.ToDecimal(strPub);
		//			dTotal = dPub+dOwn;
		//			if (this.Sql.GetCommonSql("Fee.FeeReport.InsertOldFee",ref strSql)==-1) return -1;
		//			try
		//			{
		//				strSql = string.Format(strSql,
		//					obj.ID,//������
		//					oPatientInfo.User01,//���׷�ʽ
		//					oPatientInfo.Patient.PID.CardNO,//���￨��
		//					oPatientInfo.Patient.PID.PatientNO,//סԺ��
		//					"All",//��С����
		//					oPatientInfo.Name,//����
		//					oPatientInfo.Patient.Sex.ID.ToString(),//�Ա�
		//					oPatientInfo.PayKind.ID,//�������
		//					obj.User01,//�������յ��Ժ�
		//					obj.Name,//ҽ��֤��
		//					oPatientInfo.Patient.Pact.ID,//��ͬ��λ
		//					"All",//ִ�п���
		//					"All",//ҽ������
		//					dTotal,//�����ܶ�
		//					obj.User02,//����					
		//					0,//�Ը�
		//					obj.User03,//�Է�
		//					"0",//���˱�ʾ
		//					obj.Memo,//��ע
		//					//
		//					this.Operator.ID);
		//			}
		//			catch(Exception ex)
		//			{
		//				this.Err=ex.Message;
		//				this.ErrCode=ex.Message;
		//				return -1;
		//			}
		//			return this.ExecNoQuery(strSql) ;
		//		}
		#endregion 
		#region ������Ŀά��

		/// <summary>
		/// ��ȡ������Ŀ
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSpecialItem()
		{
			ArrayList list = new ArrayList();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.ucSpeciallyItem.Selete.1",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql);
				}

				this.ExecQuery(strSql);
				FS.FrameWork.Models.NeuObject obj =null;
				while(this.Reader.Read()) 
				{
					obj = new NeuObject();
					obj.ID = Reader["ITEM_CODE"].ToString();
					obj.Name = Reader["ITEM_NAME"].ToString();
					obj.Memo = Reader["UNIT_PRICE"].ToString();
					list.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			return list;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSpecialItemAll()
		{
			ArrayList list = new ArrayList();
			try 
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.ucSpeciallyItem.Selete",ref strSql) ==-1) 
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else 
				{
					strSql = string.Format(strSql);
				}

				this.ExecQuery(strSql);
				FS.FrameWork.Models.NeuObject obj =null;
			
				while(this.Reader.Read()) 
				{
					obj = new NeuObject();
					obj.ID = Reader["MARK"].ToString();//��ҩƷ
					obj.Name = Reader["item_name"].ToString();//��ҩƷ��Ŀ����
					obj.Memo = Reader["tot_cost"].ToString();//�۸�
					obj.User01 = Reader["feeitem_code"].ToString();//���ñ���
					obj.User02 = Reader["feeitem_name"].ToString();//�����ܸ�
					obj.User03 = Reader["SPECIALITEM_CODE"].ToString();//�۸�����
					
					list.Add(obj);
					obj = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee) 
			{
				this.Err = ee.Message;
				return null;
			}
			return list;
		}

		/// <summary>
		/// ����������Ŀ
		/// </summary>
		/// <returns></returns>
		public int InsertSpecialItem(FS.FrameWork.Models.NeuObject obj,string strID)
		{
			
			string strSql = "";		
			try 
			{
				#region SQl
				//			INSERT INTO FIN_IPB_DAYREPORTDETAIL
				//(
				//PARENT_CODE,  --����ҽ�ƻ�������
				//CURRENT_CODE, --����ҽ�ƻ�������
				//STATIC_NO,    --ͳ�����
				//KIND,         --����
				//BEGIN_DATE,   --��ʼʱ��
				//END_DATE,     --��������
				//OPER_CODE,    --����Ա����
				//STAT_CODE,    --ͳ�ƴ���
				//TOT_COST,     --���ý��
				//OWN_COST,     --�Է�ҽ��
				//PAY_COST,     --�Ը�ҽ��
				//PUB_COST,     --����ҽ��
				//MARK          --��ע
				//)
				//VALUES
				//(
				//'[��������]',--����ҽ�ƻ�������
				//'[��������]',--����ҽ�ƻ�������
				//'{0}',    --ͳ�����
				//'{1}',         --����
				//'{2}',   --��ʼʱ��
				//'{3}',     --��������
				//'{4}',    --����Ա����
				//{5},    --ͳ�ƴ���
				//{6},     --���ý��
				//{7},     --�Է�ҽ��
				//{8},     --�Ը�ҽ��
				//{9},     --����ҽ��
				//'{10}'          --��ע
				//)
				#endregion
			
				if (this.Sql.GetCommonSql("Fee.ucSpeciallyItem.Insert",ref strSql)==-1) return -1;
		
				strSql = string.Format(strSql,
					obj.ID,  //��ҩƷ����
					obj.Name,//��ҩƷ����
					obj.User01, //���ô���
					obj.User02,//��������
					"",
					obj.Memo,//�۸�
				
					this.Operator.ID,//��ע
								
					obj.User03);
				return this.ExecQuery(strSql);
			}
			catch(Exception ex) 
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strID"></param>
		/// <returns></returns>
		public int DelSpecialItem(string strID) 
		{

			string strSql = "";

			if (this.Sql.GetCommonSql("Fee.ucSpeciallyItem.Delete", ref strSql) == -1) 
				return -1;
			
			try 
			{   				
				strSql = string.Format(strSql, strID);

				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex) 
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return -1;
			}      			

		}
		#endregion
		#region ������������
		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int InsertIntoImportedData(object[] obj)
		{
			string strSql = "";
			if(this.Sql.GetCommonSql("Fee.FeeReport.InsertIntoImportedData.Insert", ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql, obj);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ��ѯҪ������·��Ƿ����
		/// </summary>
		/// <param name="month"></param>
		/// <returns></returns>
		public int GetExistMonth(string month)
		{
			string strSql = null;
			string temp = "";
			if(this.Sql.GetCommonSql("Fee.FeeReport.GetExistMonth.Select", ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql, month);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
			temp =  this.ExecSqlReturnOne(strSql);
			if(temp != null || temp != "")
			{
				return FS.FrameWork.Function.NConvert.ToInt32(temp);
			}
			else
			{
				return -1;
			}
		}
		/// <summary>
		/// ɾ��ѡ���·ݵ�����
		/// </summary>
		/// <param name="month"></param>
		/// <returns></returns>
		public int DeleteMonthData(string month)
		{
			string strSql = null;
			
			if(this.Sql.GetCommonSql("Fee.FeeReport.DeleteMonthData.Delete", ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql, month);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		#endregion
		#region סԺ�������ʲ�ѯ
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public DataSet GetPatientDayFee(string inpatientNo)
		{
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				if(this.Sql.GetCommonSql("Fee.Report.GetPatientDayFee",ref strSql) ==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
				else
				{
					strSql = string.Format(strSql, inpatientNo);
				}

				this.ExecQuery(strSql,ref ds);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}

		#endregion
		/// <summary>
		/// ��������ҽ�Ʒ��ý����걨��
		/// </summary>
		/// <param name="Begin"> ��ʼʱ��</param>
		/// <param name="End">����ʱ��</param>
		/// <param name="ItemType">��Ŀ����</param>
		/// <param name="SpecalItem">�걨���</param>
		/// <param name="StandFee">�걨���</param>
		/// <param name="ApplyNum">�걨����</param>
		/// <param name="childbearing">������������</param>
		/// <param name="ClinicTot">��ǰ���С��</param>
		/// <param name="ClinicPay">�����Ը�</param>
		/// <param name="ClinicReg">������ʽ��</param>
		/// <param name="InHosTot">סԺ�ϼ�</param>
		/// <param name="InHosPay">סԺ�Ը�</param>
		/// <param name="InHosAbove">סԺ���Ϲ涨һ��Ԫ����</param>
		/// <param name="InHosBelow">סԺ���Ϲ涨һ��һ��</param>
		/// <param name="InHosAndClinicTot">סԺ�������ܼ��ʷ���</param>
		/// <param name="InhosAndClinicAbove">�ܷ��� ���Ϲ涨һ��Ԫ���ϼ���</param>
		/// <param name="InHosAndClinicBelow">�ܷ��� ���Ϲ涨һ��Ԫ���¼���</param>
		/// <param name="AllTot">סԺ�������ܷ���</param>
		/// <param name="StandFeeTot">�걨���涨���</param>
		/// <returns>��������1 �쳣����-1 </returns>
		public int GetOldFeeTotal(string Begin,string End,string ItemType,string SpecalItem , decimal StandFee ,out decimal ApplyNum,out decimal childbearing ,out decimal ClinicTot,out decimal ClinicPay,out decimal ClinicReg,out decimal InHosTot,out decimal InHosPay,out decimal InHosAbove,out decimal InHosBelow,out decimal InHosAndClinicTot,out decimal InhosAndClinicAbove,out decimal InHosAndClinicBelow,out decimal AllTot,out decimal StandFeeTot)
		{
			try
			{
				#region 
				decimal InhosPub = 0;//סԺ�ܼ���
				ApplyNum = 0; //�걨���� 
				childbearing = 0;  //�����������
				ClinicTot = 0;  //�����ܷ���
				ClinicPay = 0;// �����Ը�
				ClinicReg = 0; //�������
				InHosTot = 0;//סԺ�ܷ���
				InHosPay = 0;//סԺ�Ը�
				InHosAbove = 0;//סԺ ���Ϲ涨һ��Ԫ����
				InHosBelow = 0;//סԺ���Ϲ涨һ��Ԫһ�� 
				InHosAndClinicTot = 0;//סԺ����������ܷ��� 
				InhosAndClinicAbove = 0;//�걨���Ϲ涨һ��Ԫ����
				InHosAndClinicBelow = 0;//�걨���Ϲ涨һ��Ԫ����
				AllTot = 0;//�걨�ܶ�
				StandFeeTot = 0;//�걨���涨������
				#endregion  
				string strSql = "";
				string strSql2 = "";
				string strSql3 = "";
				if(SpecalItem =="0")
				{
					//��ʽ����//�ʹ���//���ظ�Σ���� �ȵ�
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal1",ref strSql) ==-1) 
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql = string.Format(strSql,Begin,End,ItemType);

					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal2",ref strSql2) ==-1)
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql2 = string.Format(strSql2,Begin,End,ItemType);

					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal3",ref strSql3) ==-1)
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql3 = string.Format(strSql3,Begin,End,ItemType);
				}
				else
				{
					//��ʽ����//�ʹ���//���ظ�Σ���� �ȵ�
					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal12",ref strSql) ==-1) 
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql = string.Format(strSql,Begin,End,SpecalItem);

					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal21",ref strSql2) ==-1)
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql2 = string.Format(strSql2,Begin,End,SpecalItem);

					if(this.Sql.GetCommonSql("Fee.FeeReport.GetOldFeeTotal31",ref strSql3) ==-1)
					{
						this.Err = this.Sql.Err;
						return -1;
					}
					strSql3 = string.Format(strSql3,Begin,End,SpecalItem);
				}

				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					//�����ܷ���
					ClinicTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
					// �����Ը�
					ClinicPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
					//�������
					ClinicReg = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
					//סԺ�ܷ���
					InHosTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
					//סԺ�ܼ���
					InhosPub = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
					//סԺ�Ը�
					InHosPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
					//סԺ�������ܼ���
					InHosAndClinicTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
					//�걨���涨������
					StandFeeTot  = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				}
				this.ExecQuery(strSql2);
				while(this.Reader.Read())
				{
					ApplyNum++;
				}
				this.ExecQuery(strSql3);
				while(this.Reader.Read())
				{
					InHosAbove = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
				}
				//סԺһ��Ԫ����
				InHosBelow = InhosPub - InHosAbove; 
				//�걨���Ϲ涨һ��Ԫ���½��
				InHosAndClinicBelow =  ClinicReg + InHosBelow ; 
				//�걨���Ϲ涨һ��Ԫ���Ͻ��
				InhosAndClinicAbove = InHosAbove;
				AllTot = InHosAndClinicBelow + InhosAndClinicAbove;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				ApplyNum = 0; //�걨���� 
				childbearing = 0;  //�����������
				ClinicTot = 0;  //�����ܷ���
				ClinicPay = 0;// �����Ը�
				ClinicReg = 0; //�������
				InHosTot = 0;//סԺ�ܷ���
				InHosPay = 0;//סԺ�Ը�
				InHosAbove = 0;//סԺ ���Ϲ涨һ��Ԫ����
				InHosBelow = 0;//סԺ���Ϲ涨һ��Ԫһ�� 
				InHosAndClinicTot = 0;//סԺ����������ܷ��� 
				InhosAndClinicAbove = 0;//�걨���Ϲ涨һ��Ԫ����
				InHosAndClinicBelow = 0;//�걨���Ϲ涨һ��Ԫ����
				AllTot = 0;//�걨�ܶ�
				StandFeeTot = 0;//�걨���涨������
				return -1;
			}
			return 1;
        }

        #region ��ͷҽԺ���񱨱�

        /// <summary>
        /// ���շ�Ա��ѯ��Ŀ����
        /// </summary>
        /// <param name="sqlID"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryItemIncomeByOper(string sqlID, string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��������Ϊ" + sqlID + "��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ��" + sqlID + "��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// ��ѯ��Ʊ
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="value"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryInvoiceList(string queryType, string value, string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql("FS.ClinicFee.QueryClinicInvoice", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFS.ClinicFee.QueryClinicInvoice��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, queryType, value, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ��FS.ClinicFee.QueryClinicInvoice��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// ��ѯ��Ʊ��Ӧ�ķ�����ϸ
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryInvoiceFeedetail(string invoiceNo, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql("FS.ClinicFee.QueryFeedetailByInvoiceForPrint", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFS.ClinicFee.QueryFeedetailByInvoiceForPrint��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, invoiceNo);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ��FS.ClinicFee.QueryFeedetailByInvoiceForPrint��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// ��ѯ����ҽ��ʹ����Ŀ���
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemFlag"></param>
        /// <returns></returns>
        public int QueryItemReportForOutPatient(string begin, string end, string itemCode, string itemFlag, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }

            string sqlID = string.Empty;
            string sqlStr = string.Empty;

            if (itemFlag == "0")
            {
                sqlID = "FS.QiaoTou.OutPatientFeeReport.QueryItemList";
            }
            else if (itemFlag == "1")
            {
                sqlID = "FS.QiaoTou.OutPatientFeeReport.QueryDrugList";
            }

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��ѯSQL��䣺" + sqlID + " ʧ��!" + this.Err;
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end, itemCode);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "��ѯSQL��䣺" + sqlID + " ʧ��!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL��� " + sqlID + " ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// ��ѯסԺ��Ŀʹ�����{B6EC1AC8-5BDE-4b31-A070-1A16AF19099E}
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemFlag"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryItemReportForInPatient(string begin, string end, string itemCode, string itemFlag, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }

            string sqlID = string.Empty;
            string sqlStr = string.Empty;

            if (itemFlag == "0")
            {
                sqlID = "FS.QiaoTou.InPatientFeeReport.QueryItemList";
            }
            else if (itemFlag == "1")
            {
                sqlID = "FS.QiaoTou.InPatientFeeReport.QueryDrugList";
            }

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��ѯSQL��䣺" + sqlID + " ʧ��!" + this.Err;
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end, itemCode);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "��ѯSQL��䣺" + sqlID + " ʧ��!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL��� " + sqlID + " ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }
        #endregion

        #region ��ҽ�������񱨱�

        /// <summary>
        /// ��ȡ���﷢Ʊͳ������
        /// </summary>
        /// <returns>���﷢Ʊͳ�����Ƽ���</returns>
        public DataSet GetFeeStatNameByReportCode(string report_code)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                string strSql = "";
                if (this.Sql.GetCommonSql("GYZL.Report.GetfsFeeStatNameByReportCode", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
                else
                {
                    strSql = string.Format(strSql, report_code);
                }
                this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ���շ�Ա��ѯ��Ŀ����-����ɿ���ܱ�
        /// </summary>
        /// <param name="sqlID">sql��</param>
        /// <param name="oper_id">�շ�Ա����</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">��ֹʱ��</param>
        /// <param name="dsResult">���ؽ����</param>
        /// <returns>1���ɹ���-1��ʧ��</returns>
        public int GYZLQueryItemIncomeByOper(string sqlID, string oper_id ,string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��������Ϊ" + sqlID + "��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr,oper_id, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ��" + sqlID + "��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// �����Ҳ�ѯ��Ŀ����-����ɿ���ܱ�
        /// </summary>
        /// <param name="sqlID">sql��</param>
        /// <param name="statClass">ͳ�ƴ������</param>
        /// <param name="deptID">���Ҵ���</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">��ֹʱ��</param>
        /// <param name="dsResult">���ؽ����</param>
        /// <returns>1���ɹ���-1��ʧ��</returns>
        public int GYZLQueryItemIncomeByDept(string sqlID, string statClass, string deptID, string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��������Ϊ" + sqlID + "��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, statClass,deptID, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ��" + sqlID + "��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;

        }


        /// <summary>
        /// ��ȡ���Ѽ��˵�������Ϣ
        /// </summary>
        /// <param name="sqlID"></param>
        /// <returns></returns>
        public ArrayList GYZLQueryPubOutPatientInfo(string sqlID)
        {
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "��������Ϊ" + sqlID + "��SQL���ʧ��!";
                return  null;;
            }

           FS.FrameWork.Models.NeuObject obj = null;
			ArrayList al = new ArrayList();
			try
			{
                this.ExecQuery(sqlStr);

				while(this.Reader.Read())
				{
					obj = new NeuObject();
					obj.ID = Reader[0].ToString();
					obj.Name = Reader[1].ToString();
					al.Add(obj);
				}
				this.Reader.Close();
				return al;
            }
            catch (Exception ex)
            {
                this.Err = "ִ��SQL���ʧ��!" + ex.Message;
                return  null;;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ѯ���ﻼ�ߵķ�Ʊ��Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryOutPatientInvoiceInfoByDate(string cardNO, string beginDate, string endDate, ref DataSet dsResult)
        {
            dsResult = new DataSet();
            string sqlStr = "";
            if (this.Sql.GetCommonSql("FS.OutPatient.GetPatientInvoiceInfoByDate", ref sqlStr) == -1)
            {
                this.Err = "��ѯ����ΪFS.OutPatient.GetPatientInvoiceInfoByDate��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, cardNO, beginDate, endDate);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ������ΪFS.OutPatient.GetPatientInvoiceInfoByDate��SQL���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ������ΪFS.OutPatient.GetPatientInvoiceInfoByDate��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ݷ�Ʊ���ԺŲ�ѯ��Ʊ��ϸ��Ϣ
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryOutPatientInvoiceDetailByInvoiceNo(string invoiceNO, ref DataSet dsResult)
        {
            dsResult = new DataSet();
            string sqlStr = "";

            if (this.Sql.GetCommonSql("FS.OutPatient.GetPatientInvoiceDetailByInvoiceNO", ref sqlStr) == -1)
            {
                this.Err = "��������ΪFS.OutPatient.GetPatientInvoiceDetailByInvoiceNO��SQL���ʧ��!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, invoiceNO);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "ִ������ΪFS.OutPatient.GetPatientInvoiceDetailByInvoiceNO��sql���ʧ��!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "ִ������ΪFS.OutPatient.GetPatientInvoiceDetailByInvoiceNO��SQL���ʧ��!" + ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region ������ά���Ͳ�ѯ

        /// <summary>
        /// ͨ����ѯ���ͻ�ÿ��ұ��������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetDeptByItemQueryType(string type)
        {
            string querySql = "";
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj;

            if (this.Sql.GetCommonSql("FS.WorkLoad.GetDeptByItemQuery", ref querySql) == -1)
            {
                this.Err = "��ȡSql������,������:FS.WorkLoad.GetDeptByItemQuery";
                WriteErr();
                return null;
            }

            try
            {
                querySql = string.Format(querySql, type);
            }
            catch (Exception ex)
            {
                this.Err = "������ʽ������" + ex.Message;
                WriteErr();
                return null;
            }

            if (this.ExecQuery(querySql) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ȡ������Ϣʧ��";
                this.WriteErr();
                return null;
            }

            return al;
        }

        /// <summary>
        /// ��ѯ��ά������Ŀ��ѯ��Ϣ
        /// </summary>
        /// <param name="deptCode">Ȩ�޿���</param>
        /// <param name="queryType">��ѯ����</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryItemQueryMend(string deptCode, string queryType, ref DataSet ds)
        {
            string querySql = "";

            if (this.Sql.GetCommonSql("FS.WorkLoad.GetItemQueryInfoByDeptQueryType", ref querySql) == -1)
            {
                this.Err = "��ȡSql������,������:FS.WorkLoad.GetItemQueryInfoByDeptQueryType";
                WriteErr();
                ds = null;
                return -1;
            }
            try
            {
                querySql = string.Format(querySql, deptCode, queryType);
            }
            catch (Exception ex)
            {
                this.Err = "������ʽ������" + ex.Message;
                WriteErr();
                ds = null;
                return -1;
            }

            return this.ExecQuery(querySql, ref ds);
        }

        /// <summary>
        /// ��ȡ��ѯ�������
        /// </summary>
        /// <returns></returns>
        public string GetTypeSequence()
        {
            string querySql = "SELECT SEQ_FIN_COM_ITEMQUERY_TYPE.NEXTVAL FROM DUAL";

            try
            {
                return this.ExecSqlReturnOne(querySql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return "";
            }
        }

        /// <summary>
        /// ������Ŀ��ѯ��Ϣά��,�������
        /// </summary>
        /// <param name="ament"></param>
        /// <returns></returns>
        public int InsertItemInfoAment(FS.FrameWork.Models.NeuObject ament)
        {
            if (ament == null)
            {
                return 0;
            }

            string insertSql = string.Empty;

            if (this.Sql.GetCommonSql("FS.WorkLoad.InsertItemQueryInfo", ref insertSql) == -1)
            {
                this.Err = "��ȡSql������,������:FS.WorkLoad.InsertItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                insertSql = string.Format(insertSql, FS.FrameWork.Function.NConvert.ToInt32(ament.ID),     // ����
                                                  ament.Name,    //��ѯ����
                                                  ament.User01,  //���Ҵ���
                                                  ament.User02,  //��Ŀ����
                                                  ament.User03,   //��Ŀ����
                                                  FS.FrameWork.Function.NConvert.ToInt32(ament.Memo),  //˳���
                                                  this.Operator.ID);  //����Ա
                if (ExecNoQuery(insertSql) < 0)
                {
                    this.Err = "��������ʧ��,�����:" + this.ErrCode;
                    WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int UpdateItemInfoAment(FS.FrameWork.Models.NeuObject amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "����ʵ��Ϊ��!";
                WriteErr();
                return 0;
            }

            string updateSql = "";

            if (this.Sql.GetCommonSql("FS.WorkLoad.UpdateItemQueryInfo", ref updateSql) == -1)
            {
                this.Err = "��������������,������:FS.WorkLoad.UpdateItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                updateSql = string.Format(updateSql, FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID),     // ����
                                                   amentObj.Name,    //��ѯ����
                                                   amentObj.User01,  //���Ҵ���
                                                   amentObj.User02,  //��Ŀ����
                                                   amentObj.User03,   //��Ŀ����
                                                   FS.FrameWork.Function.NConvert.ToInt32(amentObj.Memo),  //˳���
                                                   this.Operator.ID);  //����Ա
                if (ExecNoQuery(updateSql) < 0)
                {
                    this.Err = "��������ʧ��,�����:" + this.ErrCode;
                    WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int DeleteItemInfoAment(FS.FrameWork.Models.NeuObject amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "��Ϣʵ��Ϊ��,��˶Ժ��ٽ���ɾ��!";
                WriteErr();
                return 0;
            }

            string deleteSql = "";
            if (this.Sql.GetCommonSql("FS.WorkLoad.DeleteItemQueryInfo", ref deleteSql) == -1)
            {
                this.Err = "��ȡSql���ʧ��,������:FS.WorkLoad.DeleteItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                deleteSql = string.Format(deleteSql, FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }
            return this.ExecNoQuery(deleteSql);
        }

        /// <summary>
        /// ɾ����ѯ�����µľ�����Ŀ
        /// </summary>
        /// <param name="queryType">��ѯ����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns></returns>
        public int DeleteItemInfoAment(string queryType, string deptCode)
        {
            string deleteSql = "";
            if (this.Sql.GetCommonSql("FS.WorkLoad.DeleteItemQueryList", ref deleteSql) == -1)
            {
                this.Err = "FS.WorkLoad.DeleteItemQueryList";
                WriteErr();
                return -1;
            }

            try
            {
                deleteSql = string.Format(deleteSql, queryType, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }
            return this.ExecNoQuery(deleteSql);
        }

        /// <summary>
        /// �ӳ�������ɾ����ѯ����
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DeleteItemQueryConst(string code)
        {
            string deleteSql = "";
            if (this.Sql.GetCommonSql("FS.WorkLoad.DeleteItemQueryConst", ref deleteSql) == -1)
            {
                this.Err = "��ȡSql������,������:FS.WorkLoad.DeleteItemQueryConst";
                WriteErr();
                return -1;
            }

            try
            {
                deleteSql = string.Format(deleteSql, code);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }
            return this.ExecNoQuery(deleteSql);
        }

        /// <summary>
        /// ���볣��
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="sortid"></param>
        /// <returns></returns>
        public int insertConstSub(string type, FS.FrameWork.Models.NeuObject obj)
        {
            string insertSql = "";
            string getseqSql = "SELECT SEQ_FIN_COM_ITEMQUERY.NEXTVAL FROM DUAL";
            string code = string.Empty;
            int sortid = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);

            try
            {
                code = this.ExecSqlReturnOne(getseqSql);
            }
            catch
            {
                this.Err = "��ȡ��ѯ�������ʧ��";
                WriteErr();
                return -1;
            }

            if (this.Sql.GetCommonSql("FS.WorkLoad.Const.Insert", ref insertSql) == -1)
            {
                this.Err = "���볣��ʧ��,��ѯ�����ų���,FS.WorkLoad.Const.Insert";
                WriteErr();
                return -1;
            }
            try
            {
                insertSql = string.Format(insertSql, type, code, obj.Name, obj.Memo, obj.User01, obj.User02, obj.User03, sortid, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
                return -1;
            }

            return this.ExecNoQuery(insertSql);
        }

        #endregion

    }

}
