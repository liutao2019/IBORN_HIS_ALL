using System;
using  System.Collections;
using FS.HISFC.Models;
using FS.FrameWork.Models;
using System.Data;
using FS.FrameWork.Function;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// FeeReport 的摘要说明。
	/// </summary>
	public class FeeReport:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 住院处报表
		/// </summary>
		public FeeReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		
		#region 特约单位报表
		/// <summary>
		/// 获得住院特约患者的统计报表信息
		/// </summary>
		/// <param name="dtBegin">开始时间</param>
		/// <param name="dtEnd">结束时间</param>
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
				this.Err = "参数付值出错!";
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
		/// 获取日结金额
		/// </summary>
		/// <param name="BeginDate">起始时间</param>
		/// <param name="EndDate">结束时间</param>
		/// <param name="OperID">操作员标识</param>
		/// <returns></returns>
		public string GetPrepayCost(string BeginDate,string EndDate,string OperID)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CA'
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCost", ref strSQL ) == -1  )
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 获取日结金额现金部分
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayCostByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql参数			
	
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCostByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 获取日结金额支票
		/// </summary>
		/// <param name="BeginDate">起始时间</param>
		/// <param name="EndDate">结束时间</param>
		/// <param name="OperID">操作员标识</param>
		/// <returns></returns>
		public string GetPrepayCheck(string BeginDate,string EndDate,string OperID)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--银行转账
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCheck", ref strSQL ) == -1  )
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 根据部门获得日结金额支票部分
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayCheckByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql参数			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayCheckByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 获取日结金额支票
		/// </summary>
		/// <param name="BeginDate">起始时间</param>
		/// <param name="EndDate">结束时间</param>
		/// <param name="OperID">操作员标识</param>
		/// <returns></returns>
		public string GetPrepayOther(string BeginDate,string EndDate,string OperID)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--其它
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayOther", ref strSQL ) == -1  )
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 获取日结金额汇票
		/// </summary>
		/// <param name="BeginDate">起始时间</param>
		/// <param name="EndDate">结束时间</param>
		/// <param name="OperID">操作员标识</param>
		/// <returns></returns>
		public string GetPrepayPostal(string BeginDate,string EndDate,string OperID)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='PO'--其它
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayPostal", ref strSQL ) == -1  )
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,OperID);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		/// <summary>
		/// 根据部门获得日结金额汇票部分
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="DeptList"></param>
		/// <returns></returns>
		public string GetPrepayPostalByDept(string BeginDate,string EndDate,string DeptList) 
		{
			#region Sql参数			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayPostalByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}
		#region delete By Maokb
		//		/// <summary>
		//		/// 获得退住院预交
		//		/// </summary>
		//		/// <param name="BeginDate"></param>
		//		/// <param name="EndDate"></param>
		//		/// <param name="OperID"></param>
		//		/// <param name="TransType"></param>
		//		/// <returns></returns>
		//		public string GetReturnInPrepayCost(string BeginDate,string EndDate,string OperID,string TransType) 
		//		{
		//			#region Sql参数			
		//			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
		//			// WHERE  PARENT_CODE='[父级编码]'  
		//			// AND  CURRENT_CODE='[本级编码]' 
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
		//				this.Err = "获得日结金额出错!";
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
		//		/// 用血金
		//		/// </summary>
		//		/// <param name="BeginDate"></param>
		//		/// <param name="EndDate"></param>
		//		/// <param name="OperID"></param>
		//		/// <returns></returns>
		//		public string GetReturnYXCost(string BeginDate,string EndDate,string OperID) 
		//		{
		//			#region Sql参数			
		//			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
		//			// WHERE  PARENT_CODE='[父级编码]'  
		//			// AND  CURRENT_CODE='[本级编码]' 
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
		//				this.Err = "获得用血金出错!";
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
		/// 获得退住院预交补收和返还的标志
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="OperID"></param>
		/// <param name="TransType"></param>
		/// <returns></returns>
		public string GetReturnInPrepaySupplyflag(string BeginDate,string EndDate,string OperID,string TransType) 
		{
			#region Sql参数			
			//			select  sum(PREPAY_COST) from fin_ipb_balancehead
			// WHERE  PARENT_CODE='[父级编码]'  
			// AND  CURRENT_CODE='[本级编码]' 
			// AND  BALANCE_OPERCODE = '{0}' 
			//  AND  BALANCE_DATE >=to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
			// AND  BALANCE_DATE <=to_date('{2}','yyyy-mm-dd HH24:mi:ss') 
			//and TRANS_TYPE = {3} 
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetReturnInPrepaySupplyflag", ref strSQL ) == -1  ) 
			{
				this.Err = "获得日结金额出错!";
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
			#region Sql参数			
		
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetPrepayOtherByDept", ref strSQL ) == -1  ) 
			{
				this.Err = "获得日结金额出错!";
				return "";
			}			
			strSQL = string.Format(strSQL,BeginDate,EndDate,DeptList);
			strCost = this.ExecSqlReturnOne( strSQL );

			return strCost;
		}

		/// <summary>
		/// 根据部门获取支票
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptCheck(string BeginDate,string EndDate,string Dept)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--银行转账
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptCheck", ref strSQL ) == -1  )
			{
				this.Err = "获得日结金额出错!";
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
			//					this.Err = "获得SQL语句出错";
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
		/// 获得操作员日结实收日结中的退预交
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
					this.Err = "查询预交金出错!";
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
						this.Err = "获得SQL语句出错";
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
							this.Err = "获得SQL语句出错";
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
							this.Err = "获得SQL语句出错";
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
		/// 根据部门获取现金
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptCost(string BeginDate,string EndDate,string Dept)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--银行转账
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptCost", ref strSQL ) == -1  )
				{
					this.Err = "获得日结金额出错!";
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
				//						this.Err = "获得SQL语句出错";
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
		/// 根据部门获取其他（银行卡）
		/// </summary>
		/// <param name="BeginDate"></param>
		/// <param name="EndDate"></param>
		/// <param name="Dept"></param>
		/// <returns></returns>
		public string GetByDeptOther(string BeginDate,string EndDate,string Dept)
		{
			#region Sql参数			
			//select sum(prepay_cost) from FIN_IPB_INPREPAY
			//where PARENT_CODE='[父级编码]' 
			//and CURRENT_CODE='[本级编码]' 
			//and OPER_DATE >= to_date('2005-1-31','yyyy-mm-dd HH24:mi:ss')
			//and OPER_DATE <= to_date('2005-2-1','yyyy-mm-dd HH24:mi:ss')
			//and pay_way='CH'--银行转账
			//and oper_code='001406'
			#endregion
			string strCost = "";
			string strSQL = "";
			try
			{
				if( this.Sql.GetCommonSql( "Fee.FeeReport.GetByDeptOther", ref strSQL ) == -1  )
				{
					this.Err = "获得日结金额出错!";
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
				//						this.Err = "获得SQL语句出错";
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
		/// 预交张数
		/// </summary>
		/// <returns></returns>
		public string GetReceiptNum(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.GetReceiptNum", ref strSQL ) == -1  )
			{
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// 预交作废票据子号
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
				this.Err = "获得出错!";
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
		/// 获得票据区间的最小号
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
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// 获得票据区间的最大号
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
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}
		/// <summary>
		/// 获得票据区间 --住院处个人实收日报使用
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
				this.Err = "没有找到Fee.FeeReport.GetReceiptZone字段";
				return "ERR";
			}
			strSql = System.String.Format(strSql,dtBegin,dtEnd,OperID);
			return this.ExecSqlReturnOne(strSql);
		}
		/// <summary>
		/// 转押金
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
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// 获得公费合计
		/// </summary>
		/// <param name="strID">住院号</param>
		/// <returns></returns>
		public string GetPub(string strID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.pub", ref strSQL ) == -1  )
			{
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,strID);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}
		/// <summary>
		/// 获得自费合计
		/// </summary>
		/// <param name="strID">住院号</param>
		/// <returns></returns>
		public string GetOwn(string strID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.own", ref strSQL ) == -1  )
			{
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,strID);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;
		}

		/// <summary>
		/// 取患者日清单总费用
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
					if(this.Reader[0] == System.DBNull.Value)//已清总费用
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
					if(this.Reader[0] == System.DBNull.Value)//日清单未清预交金
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
		/// 先这样
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
		/// 根据住院号查费用
		/// </summary>
		/// <param name="strID">住院号</param>
		/// <returns></returns>
		public ArrayList GetOldFeeCos(string strID)
		{
			ArrayList List = new ArrayList();
			string strSql = "";
			//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[父级编码]' and CURRENT_CODE ='[本级编码]' and REPORT_TYPE = '{1}'
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
					obj.ID = Reader[0].ToString(); //处方号	
					obj.Name = Reader[1].ToString();//住院号
					obj.User01 = Reader[2].ToString();//费用总额
					obj.User02 = Reader[3].ToString();//公费
					obj.User03 = Reader[4].ToString();//自费					
					obj.Memo = Reader[5].ToString();//备注
					
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
		/// 按指定 标准查询欠费
		/// </summary>
		/// <param name="dFreeCost">金额</param>
		/// <param name="strNurseCell"></param>
		/// <returns></returns>
		public ArrayList GetAleryMoney0(decimal dFreeCost,string strNurseCell)
		{
			string strSql1 = "";
			ArrayList List = new ArrayList();
			string strSql = "";
			try
			{
				//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[父级编码]' and CURRENT_CODE ='[本级编码]' and REPORT_TYPE = '{1}'
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
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//床号
					obj.PID.PatientNO = Reader[1].ToString();//住院号
					obj.Name = Reader[2].ToString();//姓名
					obj.Pact.PayKind.ID = Reader[3].ToString();//结算类别名称
					obj.Pact.PayKind.Name = Reader[4].ToString();//结算类别标示
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//未清金额	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//预付金额	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//结余金额					
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
		/// 按比例查询
		/// </summary>
		/// <param name="dScale">比例</param>
		/// <param name="strNurseCell">病区</param>
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
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//床号
					obj.PID.PatientNO = Reader[1].ToString();//住院号
					obj.Name = Reader[2].ToString();//姓名
					obj.Pact.PayKind.ID = Reader[3].ToString();//结算类别名称
					obj.Pact.PayKind.Name = Reader[4].ToString();//结算类别标示
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//未清金额	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//预付金额	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//结余金额					
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
		/// 根据警戒线查询
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
				//select distinct REPORT_CODE, REPORT_NAME from fin_com_feecodestat where PARENT_CODE = '[父级编码]' and CURRENT_CODE ='[本级编码]' and REPORT_TYPE = '{1}'
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
					obj.PVisit.PatientLocation.Bed.ID = Reader[0].ToString();//床号
					obj.PID.PatientNO = Reader[1].ToString();//住院号
					obj.Name = Reader[2].ToString();//姓名
					obj.Pact.PayKind.ID = Reader[3].ToString();//结算类别名称
					obj.Pact.PayKind.Name = Reader[4].ToString();//结算类别标示
					obj.FT.TotCost = Convert.ToDecimal(Reader[5].ToString());//未清金额	
					obj.FT.PrepayCost = Convert.ToDecimal(Reader[6].ToString());//预付金额	
					obj.FT.SupplyCost = Convert.ToDecimal(Reader[7].ToString());//结余金额					
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
			#region Sql参数
			
			#endregion
			string strSql = "";
			#region "接口"
			//insert into FIN_IPB_PREPAYSTAT
			//(
			//PARENT_CODE,CURRENT_CODE,STATIC_NO,BEGIN_DATE,END_DATE,OPER_CODE,PREPAY_CASH,
			//PREPAY_CHECK,PREPAY_OTHER,FOREGIFT_COST,RECEIPT_ZONE,PREPAY_NUM,RETURN_NO
			//)VALUES
			//('[父级编码]',
			// '[本级编码]',
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
		/// 获取总的住院费
		/// </summary>
		/// <param name="BeginDate">开始时间</param>
		/// <param name="EndDate">结束时间</param>
		/// <param name="OperID">操作员</param>
		/// <returns></returns>
		public string GetFeeSum(string BeginDate,string EndDate,string OperID)
		{
			string strRet = "";
			string strSQL = "";
			
			if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetSumFee", ref strSQL ) == -1  )
			{
				this.Err = "获得出错!";
				return "";
			}	
			strSQL = string.Format(strSQL,OperID,BeginDate,EndDate);
			strRet = this.ExecSqlReturnOne( strSQL );

			return strRet;		
		}
	
		/// <summary>
		/// 根据部门获得医院净收入
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
		/// 初始化统计大类
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
		/// 获取实际日结最大时间
		/// </summary>
		/// <returns></returns>
		public string GetMaxTimeDayReport(string strOper)
		{
			#region Sql参数			
			//select max(end_date) from FIN_IPB_PREPAYSTAT
			#endregion
			string strCost = "";
			try
			{
				string strSQL = "";
			
				if( this.Sql.GetCommonSql( "Fee.FeeReport.DayReport.GetDayReportMaxDate", ref strSQL ) == -1  )
				{
					this.Err = "获得最大时间出错!";
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
		/// 根据住院号取得
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
		/// 获得结算清单公费值
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
		/// 获得结算清单自费
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
		/// 获取职工生育保险费用结算数据
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
					obj.ProCreateNO = this.Reader[0].ToString();//个人电脑号
					obj.User01 = this.Reader[1].ToString();//姓名
					obj.SSN = this.Reader[2].ToString();//就医凭证号
					obj.PID.PatientNO = this.Reader[3].ToString();//住院号
					obj.Memo = this.Reader[4].ToString();//产式或术式
					if(this.Reader[5].ToString()!="")
						obj.PVisit.InTime = Convert.ToDateTime(this.Reader[5].ToString());//入院日期
					else obj.PVisit.InTime = Convert.ToDateTime(strDate);
					if(this.Reader[6].ToString()!="")
						obj.PVisit.OutTime = Convert.ToDateTime(this.Reader[6].ToString());//出院日期
					else obj.PVisit.OutTime = Convert.ToDateTime(strDate);
					obj.PVisit.Memo = this.Reader[7].ToString();//住院天数
					if(this.Reader[8].ToString()!="")
						obj.FT.TotCost = Convert.ToDecimal(this.Reader[8].ToString());//费用合计
					else obj.FT.TotCost=0;
					if(this.Reader[9].ToString()!="")
						obj.FT.PubCost = Convert.ToDecimal(this.Reader[9].ToString());//公费金额
					else obj.FT.PubCost = 0;
					if(this.Reader[10].ToString()!="")
						obj.FT.OwnCost = Convert.ToDecimal(this.Reader[10].ToString());//自费金额
					else obj.FT.OwnCost = 0;
					//实际住院医疗费分类
					if(this.Reader[11].ToString()!="")
						obj.FT.Memo = this.Reader[11].ToString();//床费
					else obj.FT.Memo = "0";
					if(this.Reader[12].ToString()!="")
						obj.FT.ID = this.Reader[12].ToString();//诊金
					else obj.FT.ID = "0";
					if(this.Reader[13].ToString()!="")
						obj.FT.Name = this.Reader[13].ToString();//检查费
					else obj.FT.Name = "0";
					if(this.Reader[14].ToString()!="")
						obj.FT.User01 = this.Reader[14].ToString();//治疗费
					else obj.FT.User01 = "0";
					if(this.Reader[15].ToString()!="")
						obj.FT.User02 = this.Reader[15].ToString();//护理费
					else obj.FT.User02 = "0";
					if(this.Reader[16].ToString()!="")
						obj.FT.User03 = this.Reader[16].ToString();//手术费
					else obj.FT.User03 = "0";
					if(this.Reader[17].ToString()!="")
						obj.FT.DerateCost = Convert.ToDecimal(this.Reader[17].ToString());//化验费
					else obj.FT.DerateCost = 0;
					if(this.Reader[18].ToString()!="")
						obj.FT.RebateCost = Convert.ToDecimal(this.Reader[18].ToString());//药费
					else obj.FT.RebateCost = 0;
					if(this.Reader[19].ToString()!="")
						obj.FT.BalancedCost = Convert.ToDecimal(this.Reader[19].ToString());//其它	
					else obj.FT.BalancedCost = 0;
					
					//产前检查实际费用
					if(this.Reader[20].ToString()!="")
						obj.Kin.User01 = this.Reader[20].ToString();//合计
					else obj.Kin.User01 = "0";
					if(this.Reader[21].ToString()!="")
						obj.Kin.User02 = this.Reader[21].ToString();//记账费用
					else obj.Kin.User02 = "0";
					if(this.Reader[22].ToString()!="")
						obj.Kin.User03 = this.Reader[22].ToString();//支付费用
					else obj.Kin.User03 = "0";
					//总额
					if(this.Reader[23].ToString()!="")
						obj.User01 = this.Reader[23].ToString();//总医疗费用
					else obj.User01 = "0";
					if(this.Reader[24].ToString()!="")
						obj.User02 = this.Reader[24].ToString();//总记账费用
					else obj.User02 = "0";
					if(this.Reader[25].ToString()!="")
						obj.User03 = this.Reader[25].ToString();//总自付费用
					else obj.User03 = "0";

					if(this.Reader[26].ToString()!="")
						obj.ID = this.Reader[26].ToString();//特需服务费用
					else obj.ID = "0";
					if(this.Reader[27].ToString()!="")
						obj.Name = this.Reader[27].ToString();//按额定标准申报结算金额
					else obj.Name = "0";
					if(this.Reader[28].ToString()!="")
						obj.Memo = this.Reader[28].ToString();//主要诊断
					if(this.Reader[29].ToString()!="")
						obj.PVisit.RegistTime = Convert.ToDateTime(this.Reader[29].ToString());//结算日期
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
		/// 获得住院病区医保用药一览表
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
		/// 住院病区医保患者医保用药一览表
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
		/// 获得甲乙类药品费用明细
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
		/// 获得住院总费用根据条件
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
					this.Err = "获得最出错!";
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
		/// 住院医保病人费用构成表
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
		/// 住院药费月报表，查询住院人数，费用、费用比例等。
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
		/// 资料变动日志表
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
				//根据时间区间 和 病区查询
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
		/// 手术记帐日志表
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
					info.ID = Reader[0].ToString(); // 住院号
					info.Name =Reader[1].ToString();//姓名
					((FS.HISFC.Models.RADT.PatientInfo)info.Patient).PVisit.PatientLocation.NurseCell.Name = Reader[2].ToString();// 病区
					info.Item.MinFee.Name = Reader[3].ToString();//最小费用名称 
					info.FeeOper.ID = Reader[4].ToString();//操作员
					info.ExecOper.Dept.Name =Reader[5].ToString();// 执行科室
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
		/// 取每个住院流水号对应的 最小费用列表
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
					info.ID = Reader[0].ToString(); //住院号
					info.Item.MinFee.ID =Reader[1].ToString(); //最小费用代码
					info.Item.MinFee.Name =Reader[2].ToString(); //最小费用名称
					info.ExecOper.Dept.Name = Reader[3].ToString();// 执行科室
					info.FT.TotCost =Convert.ToDecimal(Reader[3]);//合计 
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
		/// 餐费记帐明细日志表
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
		/// 查住院处应收名细
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
		/// 查全院应收名细按病区汇总
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
		/// 获得住院处应付名细
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
		/// 获得住院应付名细按病区汇总
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
		/// 中山一院明细记账日志表
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
		/// 中山一院确认记账明细表
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
		/// 	中山一院综合记账明细表
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
		/// 中山一院直接（补）记账明细表（住院处）
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
		/// 中山一院药记账明细表
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
		/// 中山一院转区人员日志表（住院处）
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
//				//这里用暂时用CShiftData 来存储数据。
//				while(this.Reader.Read())
//				{
//					//住院号	姓名	原区	床号	标志	新区	床号	工号
//					info = new CShitData();
//					info.ClinicNo =Reader[0].ToString(); // 住院号
//					info.ShitCause =Reader[1].ToString(); //病人姓名
//					info.OldDataName = Reader[2].ToString();//原区
//					info.OldDataCode = Reader[3].ToString();//暂存原床好
//					info.Name = Reader[4].ToString(); //存标志
//					info.NewDataName = Reader[5].ToString();//新区
//					info.NewDataCode = Reader[6].ToString();//暂存新床号
//					info.OperCode = Reader[7].ToString();  //工号
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
		/// 获得全院收费名细按统计大类分组
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
		/// 获得未清预交金
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
		/// 获得患者住院费用明细按
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
		/// 获得患者住院费用明细按
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
		/// 获得患者费用明细，按费用日期排序--Edit By MaoKb
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
		/// 获得患者住院费用明细按
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
		/// 根据时间
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
		/// 出院人员日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="pactlist"></param>
		/// <returns></returns>
		public DataSet GetOutPersonRegedit(string Begin,string End ,string deptlist,string pactlist)
		{
			//出院人员日志表
			System.Data.DataSet  ds = new DataSet();
			try
			{
				string strSql = "";
				int Result = 0;
				switch(pactlist)
				{
					case "生育保险": Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit1",ref strSql);break;
					case "市医保":   Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit2",ref strSql);break;
					case "东莞医保": Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit3",ref strSql);break;
					case "全部":     Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegedit4",ref strSql);break;
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
		/// 出院人员日志表  当天结算 非当天出院的 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <param name="pactlist"></param>
		/// <returns></returns>
		public ArrayList GetOutPersonRegeditBefore(string Begin,string End ,string deptlist,string pactlist)
		{
			//出院人员日志表
			ArrayList list = new ArrayList(); //要返回的数组
			try
			{
				string strSql = "";
				int Result = 0;
				switch(pactlist)
				{
					case "生育保险":Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore1",ref strSql);break;
					case "市医保"  :Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore2",ref strSql);break;
					case "东莞医保":Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore3",ref strSql);break;
					case "全部"    :Result = this.Sql.GetCommonSql("Fee.FeeReport.GetOutPersonRegeditBefore4",ref strSql);break;
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
					info.ID = this.Reader[0].ToString(); //住院号
					info.Name =this.Reader[1].ToString();//姓名
					info.FeeOper.ID = this.Reader[2].ToString();//工号
					info.User01 = this.Reader[3].ToString(); //性别
					info.FeeOper.Name =this.Reader[4].ToString();// 年龄
					((FS.HISFC.Models.RADT.PatientInfo)info.Patient).PVisit.PatientLocation.NurseCell.Name = this.Reader[5].ToString();//病区
					info.User02 = this.Reader[6].ToString();//出院日期 
					info.FeeOper.ID = this.Reader[7].ToString();//--床日
					info.FT.TotCost =Convert.ToDecimal(this.Reader[8]);//总费用
					info.FT.RebateCost =Convert.ToDecimal(this.Reader[9]);//托收额
					info.FT.PrepayCost=Convert.ToDecimal(this.Reader[10]);//总预交
					info.FT.OwnCost = Convert.ToDecimal(this.Reader[11]);//清帐额 
					info.FT.PubCost = Convert.ToDecimal(this.Reader[12]);//预交款
					info.FT.ReturnCost= Convert.ToDecimal(this.Reader[13]);//补退
					info.User03 = this.Reader[14].ToString();//标
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
		///  登记未入院人员日志表 
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptlist"></param>
		/// <returns></returns>
		public DataSet GetUnderOutPersonRegedit(string Begin,string End,string deptlist)
		{
			//未入院登记人员日志表
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
		/// 婴儿入院日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetBabyPersonRegedit(string Begin,string End)
		{
			//婴儿入院日志表 
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
		/// 预收明细日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <param name="deptList2"></param>
		/// <returns></returns>
		public DataSet GetPrepayDetail(string Begin,string End,string deptList,string deptList2 )
		{
			//预收明细日志表 分两部分来做 
			//1。当天入院病人 交的预交金
			//2。非当天入院病人交的预交金
			System.Data.DataSet  ds = new DataSet();  //存储当天入院病人记录
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

				this.ExecQuery(strSql,ref ds); //查询

				DataSet myds = new DataSet(); //存储非当天入院病人记录

				string Sql2= "";
				if(this.Sql.GetCommonSql("Fee.FeeReport.GetPrepayDetailmanager2",ref Sql2) !=-1)
				{
					Sql2 = string.Format(Sql2,Begin,End,deptList,deptList2);
					this.ExecQuery(Sql2,ref myds); //查询
					if(myds!=null) //有数据 
					{
						if(myds.Tables.Count > 0) //有数据
						{
							if(myds.Tables[0].Rows.Count>0) //有行数
							{
								ds.Tables[0].Rows.Add(new object[]{"非当天入院"});
								foreach(DataRow row in myds.Tables[0].Rows)
								{
									ds.Tables[0].Rows.Add(new object[]{row[0],row[1],row[2],row[3],row[4],row[5],row[6]}); //把数据加到后边
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
		/// 明细日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <param name="OperCode"></param>
		/// <returns></returns>
		public DataSet GetDayDetail(string Begin,string End,string deptList,string OperCode)
		{
			//预收明细日志表 分两部分来做 
			//1。当天入院病人 交的预交金
			//2。非当天入院病人交的预交金
			System.Data.DataSet  ds = new DataSet();  //存储当天入院病人记录
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

				this.ExecQuery(strSql,ref ds); //查询

			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
			return ds;
		}

		/// <summary>
		/// 直接（确认）收费日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="deptList"></param>
		/// <returns></returns>
		public DataSet GetFeeIndirect(string Begin,string End,string deptList)
		{
			//直接（确认）收费日志表
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
		/// 中山一院入院登记人员日志表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="DeptID"></param>
		/// <param name="DeptId2"></param>
		/// <returns></returns>
		public DataSet GetAllPersonInHospital(string Begin,string End,string DeptID,string DeptId2 )
		{
			//中山一院入院登记人员日志表
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
		/// 中山医院病人出院结算明细
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strBalanceType">结算类型</param>
		/// <param name="deptList">科室列表</param>
		/// <returns></returns>

		public DataSet GetPatientItemFeeByBalanceType(string Begin,string End,string strBalanceType,string deptList )
		{
			//中山一出院结算
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
		/// （1）	特约单位医疗费结算表（院内报表）
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetSpecialPactUnit(string Begin,string End)
		{
			//（1）	特约单位医疗费结算表（院内报表）
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
		/// 15.	特约单位报表 （2）	特约单位的对帐单（院外的付款通知单）
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetSpecialPactUnitInform(string Begin,string End)
		{
			//特约单位的对帐单（院外的付款通知单）
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
		/// 全院公费医疗费用总结算表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubMedicalFee(string Begin,string End)
		{
			//全院公费医疗费用总结算表
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
		/// 公费医疗费用明细表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubMedicalFeeDetail(string Begin,string End)
		{
			//公费医疗费用明细表
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
		/// 医疗机构住院医药费报销明细表（住院部分的上报省公医）
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strPact"></param>
		/// <returns></returns>
		public DataSet GetPubPactunitMedicalFeeDetail(string Begin,string End,string strPact)//string MCard,int iBit)
		{
			//（3）	医疗机构住院医药费报销明细表
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
		/// 医疗机构住院医药费报销明细表(省公医)
		/// </summary>
		/// <param name="Begin">开始时间</param>
		/// <param name="End">结束时间</param>
		/// <param name="strPact">医疗卡号前两位</param>
		/// <param name="flag">1、在职 ；2、离休</param>
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
		/// 公费医疗住院医药费报销明细表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="MCardType"></param>
		/// <returns></returns>
		public DataSet GetPubPactunitDrugFeeDetail(string Begin,string End,string MCardType)
		{
			//（4）	公费医疗住院医药费报销明细表（住院部分的上报市公医和各区）
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
		/// （5）	广东省公医办委托门诊住院医疗费报销（全院的上报省公医报表）
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubDelegateMedicalFee(string Begin,string End)
		{
			//广东省公医办委托门诊住院医疗费报销（全院的上报省公医报表）
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
		/// 广州市公费医疗预防实施管理委员会委托门诊住院医药费报销表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
		public DataSet GetPubDelegateClinicMedicalFee(string Begin,string End)
		{
			//广州市公费医疗预防实施管理委员会委托门诊住院医药费报销表
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
		/// 广州市公费医疗干部职工特殊仪器治疗报销明细表
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="strID"></param>
		/// <returns></returns>
		public DataSet GetPubInstrumnetFeeDetail(string Begin,string End,string strID)
		{
			//广州市公费医疗干部职工特殊仪器治疗报销明细表
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
		/// 获得发票领用的操作员
		/// </summary>
		/// <param name="Begin">开始时间</param>
		/// <param name="End">结束时间</param>
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
					obj.ID = Reader["GET_PERSON_CODE"].ToString(); //操作员代码	
					obj.Name = Reader["employname"].ToString();//操作员姓名
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
		/// 获得一保患者费用信息
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
		/// 获得医保在院患者
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
					obj.ID = Reader["住院号"].ToString();
					obj.Name = Reader["姓名"].ToString();
					obj.Memo = Reader["医保类"].ToString();
					obj.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["预交款"].ToString());
					obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["住院总费用"].ToString());
					obj.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["医疗费用"].ToString());
					obj.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["乙类自负"].ToString());
					obj.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["统筹自付"].ToString());
					obj.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["超限自付"].ToString());
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
		/// 获得应付费用明细
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
		/// 获得应付费用汇总Fee.FeeReport.GetPrepayList
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
		/// 获得预交金明细
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
		/// 返回退预交
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
		/// 获得实付
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
		/// 根据条件获得结算金额
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
		/// 结算清单总额
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
		/// 已清结算清单
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
		/// 求出出院代药
		/// </summary>
		/// <param name="strID"></param>
		/// <param name="strTag"></param>
		/// <returns></returns>
		public DataSet GetBrought(string strID,string strTag)
		{
			//此函数根本查不出数据但不知道什么地方调用，因此屏蔽掉 --By Maokb
			this.Err = "此处调用FS.HISFC.BizLogic.Fee.FeeReport.GetBrouht出错，函数需要更改";
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
		/// 出院带药信息
		/// </summary>
		/// <param name="strID">住院流水号</param>
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
		///  获取一个病人的医疗保险记录
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
		/// 获得生育保险
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
		/// 获得发票合计
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
		//		/// 获得日结暂存收入和净收入
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
		//					if(FeeType=="0")//净收入
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
		//					else//暂存收入
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
		//					{//正交易
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
		//					{//暂存收入
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
		/// 获得患者费用结构  Edit By MaoKb--05.8.20
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
		/// 住院费用分类汇总---分类小计------------ Edit By MaoKb
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
		/// 患者一日清单
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
		/// 住院患者费用分类汇总-- Edit By MaoKb
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
		
		#region 公费报表部分
		/// <summary>
		/// 公费医疗住院医药费报销明细表
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

			//根据传入的串分解成
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
			#region 修改后
			//			if(this.ExecQuery(strSql)==-1) return null;
			//			while(this.Reader.Read()) {					
			//				obj.User01 = Reader["人次"].ToString();
			//				obj.User02 = Reader["天数"].ToString();
			//				obj.FeeCost1 = Reader["中药"].ToString();
			//				obj.FeeCost2 = Reader["西药"].ToString();
			//				obj.FeeCost3 = Reader["进口药"].ToString();
			//				obj.FeeCost4 = Reader["一般检查费"].ToString();
			//				obj.FeeCost5 = Reader["检查费"].ToString();
			//				obj.FeeCost6 = Reader["一般治疗费"].ToString();
			//				obj.FeeCost7 = Reader["治疗费"].ToString();
			//				obj.FeeCost8 = Reader["床位费"].ToString();
			//				obj.FeeCost9 = Reader["其它"].ToString();
			//
			//				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["小计"].ToString());
			//				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["自负金额"].ToString());
			//
			//				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["实际记账"].ToString());
			//						
			//			}
			//			this.Reader.Close();	
			//
			//			return obj;
			#endregion
		}
		/// <summary>
		/// 离休省公医
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

			//根据传入的串分解成
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
		/// 市公医
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

			//根据传入的串分解成
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
		/// 省公医
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		private FS.HISFC.Models.Fee.Report objPubDelegateMedicalFee(string strSql) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			if(this.ExecQuery(strSql)==-1) return null;
			while(this.Reader.Read()) 
			{					
				obj.User01 = Reader["人次"].ToString();
				obj.User02 = Reader["天数"].ToString();
				obj.FeeCost1 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["中药"].ToString());
				obj.FeeCost2 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["西药"].ToString());
				obj.FeeCost3 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["进口药"].ToString());
				obj.FeeCost4 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["一般检查费"].ToString());
				obj.FeeCost5 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["检查费"].ToString());
				obj.FeeCost6 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["一般治疗费"].ToString());
				obj.FeeCost7 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["治疗费"].ToString());
				obj.FeeCost8 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["床位费"].ToString());
				obj.FeeCost9 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["其它"].ToString());

				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["小计"].ToString());
				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["自负金额"].ToString());

				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["实际记账"].ToString());
						
			}
			this.Reader.Close();	

			return obj;
		}
		/// <summary>
		/// 市公医
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		private FS.HISFC.Models.Fee.Report objPubDelegateMedicalFeeSI(string strSql) 
		{
			FS.HISFC.Models.Fee.Report obj = new FS.HISFC.Models.Fee.Report();
			if(this.ExecQuery(strSql)==-1) return null;
			while(this.Reader.Read()) 
			{					
				obj.User01 = Reader["人次"].ToString();
				obj.User02 = Reader["天数"].ToString();

				obj.FeeCost1 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["中药"].ToString());
				obj.FeeCost2 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["西药"].ToString());
				obj.FeeCost3 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["进口药"].ToString());
				obj.FeeCost4 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["一般检查费"].ToString());
				obj.FeeCost5 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["检查费"].ToString());
				obj.FeeCost6 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["一般治疗费"].ToString());
				obj.FeeCost7 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["治疗费"].ToString());
				obj.FeeCost8 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["床位费"].ToString());
				obj.FeeCost9 = FS.FrameWork.Function.NConvert.ToDecimal(Reader["其它"].ToString());

				obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["小计"].ToString());
				obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["自负金额"].ToString());
				obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["实际记账"].ToString());
						
			}
			this.Reader.Close();	

			return obj;
		}
		/// <summary>
		/// 全院公费医疗费用总结算表
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
					//根据传入的串分解成
					//公医
					if(Type== "GY") 
					{
						//						for(int i=1;i<s.Length;i++) {
						if (this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitsubstr",ref strSqlWhere)==-1) return null;
						//							strTag = s[i].ToString();
						strSqlWhere = string.Format(strSqlWhere,strID);
					}
					//特约单位
					if(Type == "TT") 
					{
						if (this.Sql.GetCommonSql("Fee.FeeReport.GetSpecialPactUnitlength",ref strSqlWhere)==-1) return null;
						
						strSqlWhere = string.Format(strSqlWhere,3);
					}
					//本院职工
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
						obj.User01 = Reader["人次"].ToString();
						obj.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["小计"].ToString());
						obj.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["自付"].ToString());
						obj.FT.SupplyCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["诊金"].ToString());

						obj.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader["实报"].ToString());
						
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
		/// 获得特殊检查项目
		/// </summary>
		/// <param name="dtBegin">开始时间</param>
		/// <param name="dtEnd">结束时间</param>
		/// <returns>数据级</returns>
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
		/// 查询统计大类  
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
		/// 查询统计大类  
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
//		/// 获取数据
//		/// </summary>
//		/// <param name="obj"></param>
//		/// <returns></returns>
//		private string [] GetString(FS.HISFC.Models.Fee.Useless.OldFee obj)
//		{
//			string [] str = new string[40];
//			str[0] = obj.ID;//处方号
//			str[1] = obj.User01;//交易方式
//			str[2] = obj.patientInfo.Patient.PID.CardNO;//就诊卡号
//			str[3] = obj.patientInfo.Patient.PID.PatientNO;//住院号
//			str[4] = "All";//最小费用
//			str[5] = obj.patientInfo.Name;//姓名
//			str[6] = obj.patientInfo.Patient.Sex.ID.ToString();//性别
//			str[7] = obj.patientInfo.PayKind.ID;//结算类别
//			str[8] = obj.patientInfo.Patient.ProCeatePcNO;//生育保险电脑号
//			str[9] = obj.patientInfo.Patient.SSN;//医疗证号
//			str[10] = obj.patientInfo.Patient.Pact.ID;//合同单位
//			str[11] = "All";//执行科室
//			str[12] = "All";//医生代码
//			str[13] = obj.COST.ToString();//费用总额
//			str[14] = obj.PUBCOST.ToString();//公费
//			str[15] = obj.PAYCOST.ToString();//自付
//			str[16] = "0";//自费
//			str[17] = "0";//入账标示
//			str[18] = obj.Memo;//备注
//			str[19] = this.Operator.ID;//操作员标记
//			str[20] = System.DateTime.Now.ToString();//处方号
//			str[21] = obj.BEDFEE.ToString();//床位费
//			str[22] = obj.DIAGFEE.ToString();//诊金
//			str[23] = obj.CHECKFEE.ToString();//检查
//			str[24] = obj.CUREFEE.ToString();//治疗费
//			str[25] = obj.NURSEFEE.ToString();//治疗费
//			str[26] = obj.OPERATIONFEE.ToString();//手术费
//			str[27] = obj.ASSAYFEE.ToString();//化验费
//			str[28] = obj.DRUGFEE.ToString();//药费
//			str[29] = obj.OTHERFEE.ToString();//其他费
//			str[30] = obj.CLINICFEE.ToString();//门诊费
//			str[31] = obj.SPELLFEE.ToString();//特需费 
//			str[32] = obj.STANDFEE.ToString();//申报金额
//			str[33] = obj.DECLAREDATE.ToString();//申报月份
//			str[34] = obj.CLINICPAYFEE.ToString(); //门诊自费
//			str[35] = obj.patientInfo.PVisit.InTime.ToString(); //入院日期
//			str[36] = obj.patientInfo.PVisit.OutTime.ToString(); //出院日期 
//			str[37] = obj.patientInfo.ID;
//			str[38] = obj.ItemType; //项目类别
//			str[39] = obj.SpellItemType;  //特殊项目类别
//			return str;
//		}
//		/// <summary>
//		/// 更新生育保险
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
		/// 更新生育保险
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
//		/// 插入生育保险费用
//		/// </summary>
//		/// <param name="obj1"></param>
//		/// <param name="oPatientInfo1"></param>
//		/// <returns></returns>
//		public int InsertOldFee(FS.HISFC.Models.Fee.Useless.OldFee obj1,FS.HISFC.Models.RADT.PatientInfo oPatientInfo1 )
//		{
//			#region Sql参数
//			
//			#endregion
//			string strSql = "";
//			#region "接口"
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
		/// 广州市企业职工生育保险高额医疗费结算明细表
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
//		/// 根据住院流水号获取以前存过的数据的信息
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
		/// 获取产前检查的信息
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
		#region 插入生育保险费用
		//		/// <summary>
		//		/// 插入生育保险费用
		//		/// </summary>
		//		/// <param name="obj"></param>
		//		/// <param name="oPatientInfo"></param>
		//		/// <returns></returns>
		//		public int InsertOldFee(FS.FrameWork.Models.NeuObject obj,FS.HISFC.Models.RADT.PatientInfo oPatientInfo )
		//		{
		//			#region Sql参数
		//			
		//			#endregion
		//			string strSql = "";
		//			#region "接口"
		//			
		//			#endregion	
		//			decimal dPub=0,dOwn=0;
		//			decimal dTotal = 0;//总额
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
		//					obj.ID,//处方号
		//					oPatientInfo.User01,//交易方式
		//					oPatientInfo.Patient.PID.CardNO,//就诊卡号
		//					oPatientInfo.Patient.PID.PatientNO,//住院号
		//					"All",//最小费用
		//					oPatientInfo.Name,//姓名
		//					oPatientInfo.Patient.Sex.ID.ToString(),//性别
		//					oPatientInfo.PayKind.ID,//结算类别
		//					obj.User01,//生育保险电脑号
		//					obj.Name,//医疗证号
		//					oPatientInfo.Patient.Pact.ID,//合同单位
		//					"All",//执行科室
		//					"All",//医生代码
		//					dTotal,//费用总额
		//					obj.User02,//公费					
		//					0,//自付
		//					obj.User03,//自费
		//					"0",//入账标示
		//					obj.Memo,//备注
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
		#region 特殊项目维护

		/// <summary>
		/// 获取特殊项目
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
					obj.ID = Reader["MARK"].ToString();//非药品
					obj.Name = Reader["item_name"].ToString();//非药品项目名成
					obj.Memo = Reader["tot_cost"].ToString();//价格
					obj.User01 = Reader["feeitem_code"].ToString();//费用编吗
					obj.User02 = Reader["feeitem_name"].ToString();//费用密更
					obj.User03 = Reader["SPECIALITEM_CODE"].ToString();//价格上限
					
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
		/// 插入特殊项目
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
				//PARENT_CODE,  --父级医疗机构编码
				//CURRENT_CODE, --本级医疗机构编码
				//STATIC_NO,    --统计序号
				//KIND,         --种类
				//BEGIN_DATE,   --开始时间
				//END_DATE,     --结束日期
				//OPER_CODE,    --操作员代码
				//STAT_CODE,    --统计大类
				//TOT_COST,     --费用金额
				//OWN_COST,     --自费医疗
				//PAY_COST,     --自付医疗
				//PUB_COST,     --公费医疗
				//MARK          --备注
				//)
				//VALUES
				//(
				//'[父级编码]',--父级医疗机构编码
				//'[本级编码]',--本级医疗机构编码
				//'{0}',    --统计序号
				//'{1}',         --种类
				//'{2}',   --开始时间
				//'{3}',     --结束日期
				//'{4}',    --操作员代码
				//{5},    --统计大类
				//{6},     --费用金额
				//{7},     --自费医疗
				//{8},     --自付医疗
				//{9},     --公费医疗
				//'{10}'          --备注
				//)
				#endregion
			
				if (this.Sql.GetCommonSql("Fee.ucSpeciallyItem.Insert",ref strSql)==-1) return -1;
		
				strSql = string.Format(strSql,
					obj.ID,  //非药品代码
					obj.Name,//非药品名称
					obj.User01, //费用代码
					obj.User02,//费用名称
					"",
					obj.Memo,//价格
				
					this.Operator.ID,//备注
								
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
		#region 导入门诊数据
		/// <summary>
		/// 插入门诊数据
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
		/// 查询要插入的月份是否存在
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
		/// 删除选定月份的数据
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
		#region 住院患者期帐查询
		/// <summary>
		/// 患者期帐
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
		/// 生育保险医疗费用结算申报表
		/// </summary>
		/// <param name="Begin"> 开始时间</param>
		/// <param name="End">结束时间</param>
		/// <param name="ItemType">项目类型</param>
		/// <param name="SpecalItem">申报金额</param>
		/// <param name="StandFee">申报金额</param>
		/// <param name="ApplyNum">申报例数</param>
		/// <param name="childbearing">单纯分娩例数</param>
		/// <param name="ClinicTot">产前检查小记</param>
		/// <param name="ClinicPay">产检自付</param>
		/// <param name="ClinicReg">产检记帐金额</param>
		/// <param name="InHosTot">住院合计</param>
		/// <param name="InHosPay">住院自付</param>
		/// <param name="InHosAbove">住院符合规定一万元以上</param>
		/// <param name="InHosBelow">住院符合规定一万一下</param>
		/// <param name="InHosAndClinicTot">住院和门诊总记帐费用</param>
		/// <param name="InhosAndClinicAbove">总费用 符合规定一万元以上记帐</param>
		/// <param name="InHosAndClinicBelow">总费用 符合规定一万元以下记帐</param>
		/// <param name="AllTot">住院和门诊总费用</param>
		/// <param name="StandFeeTot">申报按规定金额</param>
		/// <returns>正常返回1 异常返回-1 </returns>
		public int GetOldFeeTotal(string Begin,string End,string ItemType,string SpecalItem , decimal StandFee ,out decimal ApplyNum,out decimal childbearing ,out decimal ClinicTot,out decimal ClinicPay,out decimal ClinicReg,out decimal InHosTot,out decimal InHosPay,out decimal InHosAbove,out decimal InHosBelow,out decimal InHosAndClinicTot,out decimal InhosAndClinicAbove,out decimal InHosAndClinicBelow,out decimal AllTot,out decimal StandFeeTot)
		{
			try
			{
				#region 
				decimal InhosPub = 0;//住院总记帐
				ApplyNum = 0; //申报个数 
				childbearing = 0;  //单纯分娩个数
				ClinicTot = 0;  //门诊总费用
				ClinicPay = 0;// 门诊自付
				ClinicReg = 0; //门诊记帐
				InHosTot = 0;//住院总费用
				InHosPay = 0;//住院自付
				InHosAbove = 0;//住院 符合规定一万元以上
				InHosBelow = 0;//住院符合规定一万元一下 
				InHosAndClinicTot = 0;//住院和门诊记帐总费用 
				InhosAndClinicAbove = 0;//申报符合规定一万元以下
				InHosAndClinicBelow = 0;//申报符合规定一万元以上
				AllTot = 0;//申报总额
				StandFeeTot = 0;//申报按规定结算金额
				#endregion  
				string strSql = "";
				string strSql2 = "";
				string strSql3 = "";
				if(SpecalItem =="0")
				{
					//阴式分娩//剖宫产//严重高危妊娠 等等
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
					//阴式分娩//剖宫产//严重高危妊娠 等等
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
					//门诊总费用
					ClinicTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
					// 门诊自付
					ClinicPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
					//门诊记帐
					ClinicReg = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
					//住院总费用
					InHosTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
					//住院总记帐
					InhosPub = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
					//住院自付
					InHosPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
					//住院和门诊总记帐
					InHosAndClinicTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
					//申报按规定结算金额
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
				//住院一万元以下
				InHosBelow = InhosPub - InHosAbove; 
				//申报符合规定一万元以下金额
				InHosAndClinicBelow =  ClinicReg + InHosBelow ; 
				//申报符合规定一万元以上金额
				InhosAndClinicAbove = InHosAbove;
				AllTot = InHosAndClinicBelow + InhosAndClinicAbove;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				ApplyNum = 0; //申报个数 
				childbearing = 0;  //单纯分娩个数
				ClinicTot = 0;  //门诊总费用
				ClinicPay = 0;// 门诊自付
				ClinicReg = 0; //门诊记帐
				InHosTot = 0;//住院总费用
				InHosPay = 0;//住院自付
				InHosAbove = 0;//住院 符合规定一万元以上
				InHosBelow = 0;//住院符合规定一万元一下 
				InHosAndClinicTot = 0;//住院和门诊记帐总费用 
				InhosAndClinicAbove = 0;//申报符合规定一万元以下
				InHosAndClinicBelow = 0;//申报符合规定一万元以上
				AllTot = 0;//申报总额
				StandFeeTot = 0;//申报按规定结算金额
				return -1;
			}
			return 1;
        }

        #region 桥头医院财务报表

        /// <summary>
        /// 按收费员查询项目收入
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
                this.Err = "查找索引为" + sqlID + "的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行" + sqlID + "的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 查询发票
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
                this.Err = "查找索引为FS.ClinicFee.QueryClinicInvoice的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, queryType, value, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行FS.ClinicFee.QueryClinicInvoice的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 查询发票对应的费用明细
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
                this.Err = "查找索引为FS.ClinicFee.QueryFeedetailByInvoiceForPrint的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, invoiceNo);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行FS.ClinicFee.QueryFeedetailByInvoiceForPrint的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 查询门诊医生使用项目情况
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
                this.Err = "查询SQL语句：" + sqlID + " 失败!" + this.Err;
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end, itemCode);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "查询SQL语句：" + sqlID + " 失败!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句 " + sqlID + " 失败!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 查询住院项目使用情况{B6EC1AC8-5BDE-4b31-A070-1A16AF19099E}
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
                this.Err = "查询SQL语句：" + sqlID + " 失败!" + this.Err;
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, begin, end, itemCode);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "查询SQL语句：" + sqlID + " 失败!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句 " + sqlID + " 失败!" + ex.Message;
                return -1;
            }

            return 1;

        }
        #endregion

        #region 广医肿瘤财务报表

        /// <summary>
        /// 获取门诊发票统计名称
        /// </summary>
        /// <returns>门诊发票统计名称集合</returns>
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
        /// 按收费员查询项目收入-门诊缴款汇总表
        /// </summary>
        /// <param name="sqlID">sql文</param>
        /// <param name="oper_id">收费员代码</param>
        /// <param name="begin">起始时间</param>
        /// <param name="end">截止时间</param>
        /// <param name="dsResult">返回结果集</param>
        /// <returns>1：成功；-1：失败</returns>
        public int GYZLQueryItemIncomeByOper(string sqlID, string oper_id ,string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "查找索引为" + sqlID + "的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr,oper_id, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行" + sqlID + "的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 按科室查询项目收入-门诊缴款汇总表
        /// </summary>
        /// <param name="sqlID">sql文</param>
        /// <param name="statClass">统计大类编码</param>
        /// <param name="deptID">科室代码</param>
        /// <param name="begin">起始时间</param>
        /// <param name="end">截止时间</param>
        /// <param name="dsResult">返回结果集</param>
        /// <returns>1：成功；-1：失败</returns>
        public int GYZLQueryItemIncomeByDept(string sqlID, string statClass, string deptID, string begin, string end, ref DataSet dsResult)
        {
            if (dsResult == null)
            {
                dsResult = new DataSet();
            }
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "查找索引为" + sqlID + "的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, statClass,deptID, begin, end);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行" + sqlID + "的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;

        }


        /// <summary>
        /// 获取公费记账单患者信息
        /// </summary>
        /// <param name="sqlID"></param>
        /// <returns></returns>
        public ArrayList GYZLQueryPubOutPatientInfo(string sqlID)
        {
            string sqlStr = string.Empty;

            if (this.Sql.GetCommonSql(sqlID, ref sqlStr) == -1)
            {
                this.Err = "查找索引为" + sqlID + "的SQL语句失败!";
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
                this.Err = "执行SQL语句失败!" + ex.Message;
                return  null;;
            }
        }

        #endregion

        #region 其他

        /// <summary>
        /// 查询门诊患者的发票信息
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
                this.Err = "查询索引为FS.OutPatient.GetPatientInvoiceInfoByDate的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, cardNO, beginDate, endDate);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行索引为FS.OutPatient.GetPatientInvoiceInfoByDate的SQL语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行索引为FS.OutPatient.GetPatientInvoiceInfoByDate的SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 根据发票电脑号查询发票明细信息
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
                this.Err = "查找索引为FS.OutPatient.GetPatientInvoiceDetailByInvoiceNO的SQL语句失败!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, invoiceNO);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err = "执行索引为FS.OutPatient.GetPatientInvoiceDetailByInvoiceNO的sql语句失败!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行索引为FS.OutPatient.GetPatientInvoiceDetailByInvoiceNO的SQL语句失败!" + ex.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region 工作量维护和查询

        /// <summary>
        /// 通过查询类型获得科室编码和名称
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
                this.Err = "获取Sql语句出错,索引号:FS.WorkLoad.GetDeptByItemQuery";
                WriteErr();
                return null;
            }

            try
            {
                querySql = string.Format(querySql, type);
            }
            catch (Exception ex)
            {
                this.Err = "参数格式化错误" + ex.Message;
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
                this.Err = "获取科室信息失败";
                this.WriteErr();
                return null;
            }

            return al;
        }

        /// <summary>
        /// 查询已维护的项目查询信息
        /// </summary>
        /// <param name="deptCode">权限科室</param>
        /// <param name="queryType">查询类型</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryItemQueryMend(string deptCode, string queryType, ref DataSet ds)
        {
            string querySql = "";

            if (this.Sql.GetCommonSql("FS.WorkLoad.GetItemQueryInfoByDeptQueryType", ref querySql) == -1)
            {
                this.Err = "获取Sql语句出错,索引号:FS.WorkLoad.GetItemQueryInfoByDeptQueryType";
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
                this.Err = "参数格式化错误" + ex.Message;
                WriteErr();
                ds = null;
                return -1;
            }

            return this.ExecQuery(querySql, ref ds);
        }

        /// <summary>
        /// 获取查询类型序号
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
        /// 插入项目查询信息维护,单体插入
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
                this.Err = "获取Sql语句出错,索引号:FS.WorkLoad.InsertItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                insertSql = string.Format(insertSql, FS.FrameWork.Function.NConvert.ToInt32(ament.ID),     // 主键
                                                  ament.Name,    //查询类型
                                                  ament.User01,  //科室代码
                                                  ament.User02,  //项目代码
                                                  ament.User03,   //项目名称
                                                  FS.FrameWork.Function.NConvert.ToInt32(ament.Memo),  //顺序号
                                                  this.Operator.ID);  //操作员
                if (ExecNoQuery(insertSql) < 0)
                {
                    this.Err = "插入数据失败,错误号:" + this.ErrCode;
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
        /// 更新
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int UpdateItemInfoAment(FS.FrameWork.Models.NeuObject amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "更新实体为空!";
                WriteErr();
                return 0;
            }

            string updateSql = "";

            if (this.Sql.GetCommonSql("FS.WorkLoad.UpdateItemQueryInfo", ref updateSql) == -1)
            {
                this.Err = "索引更新语句出错,索引号:FS.WorkLoad.UpdateItemQueryInfo";
                WriteErr();
                return -1;
            }

            try
            {
                updateSql = string.Format(updateSql, FS.FrameWork.Function.NConvert.ToInt32(amentObj.ID),     // 主键
                                                   amentObj.Name,    //查询类型
                                                   amentObj.User01,  //科室代码
                                                   amentObj.User02,  //项目代码
                                                   amentObj.User03,   //项目名称
                                                   FS.FrameWork.Function.NConvert.ToInt32(amentObj.Memo),  //顺序号
                                                   this.Operator.ID);  //操作员
                if (ExecNoQuery(updateSql) < 0)
                {
                    this.Err = "更新数据失败,错误号:" + this.ErrCode;
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
        /// 删除
        /// </summary>
        /// <param name="amentObj"></param>
        /// <returns></returns>
        public int DeleteItemInfoAment(FS.FrameWork.Models.NeuObject amentObj)
        {
            if (amentObj == null)
            {
                this.Err = "信息实体为空,请核对后再进行删除!";
                WriteErr();
                return 0;
            }

            string deleteSql = "";
            if (this.Sql.GetCommonSql("FS.WorkLoad.DeleteItemQueryInfo", ref deleteSql) == -1)
            {
                this.Err = "获取Sql语句失败,索引号:FS.WorkLoad.DeleteItemQueryInfo";
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
        /// 删除查询类型下的具体项目
        /// </summary>
        /// <param name="queryType">查询类型</param>
        /// <param name="deptCode">科室编码</param>
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
        /// 从常数表中删除查询类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DeleteItemQueryConst(string code)
        {
            string deleteSql = "";
            if (this.Sql.GetCommonSql("FS.WorkLoad.DeleteItemQueryConst", ref deleteSql) == -1)
            {
                this.Err = "获取Sql语句出错,索引号:FS.WorkLoad.DeleteItemQueryConst";
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
        /// 插入常数
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
                this.Err = "获取查询类型序号失败";
                WriteErr();
                return -1;
            }

            if (this.Sql.GetCommonSql("FS.WorkLoad.Const.Insert", ref insertSql) == -1)
            {
                this.Err = "插入常数失败,查询索引号出错,FS.WorkLoad.Const.Insert";
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
