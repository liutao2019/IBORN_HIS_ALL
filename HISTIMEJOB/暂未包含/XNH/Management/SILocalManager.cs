using System;
using System.Collections;
using Neusoft.HISFC.Models.SIInterface;
using Neusoft.HISFC.Models.Fee;
using System.Data;
using Neusoft.FrameWork.Function;
using System.IO;

namespace HISTIMEJOB.XNH.Management
{
    public class SILocalManager : Neusoft.FrameWork.Management.Database 
    {
         /// <summary>
        /// 新农合本地医保接口业务管理
        /// </summary>
        public SILocalManager()
        {
			// TODO: 在此处添加构造函数逻辑
			//
			// 获取当前操作员信息
			this.currentOperator = this.Operator;
			// 获取当前科室信息
			this.currentDepartment = ((Neusoft.HISFC.Models.Base.Employee)this.Operator).Dept;
		}
        #region 变量
		/// <summary>
		/// 当前操作员
		/// </summary>
		Neusoft.FrameWork.Models.NeuObject currentOperator = new Neusoft.FrameWork.Models.NeuObject();
		/// <summary>
		/// 当前科室
		/// </summary>
		Neusoft.FrameWork.Models.NeuObject currentDepartment = new Neusoft.FrameWork.Models.NeuObject();
		//
		// 变量
		//
		/// <summary>
		/// SQL语句
		/// </summary>
		string SQL = "";
		/// <summary>
		/// SQL语句的查询子句
		/// </summary>
		string SELECT = "";
		/// <summary>
		/// SQL语句的条件子句
		/// </summary>
		string WHERE = "";
		int returnvalue = -1;

		/// <summary>
		/// 返回值
		/// </summary>
		int intReturn = 0;
		/// <summary>
		/// 默认本院
		/// </summary>
		string hosID = "1047";
		/// <summary>
		/// 转换项目
		/// </summary>
		ArrayList alReverseItems = new ArrayList();
		/// <summary>
		/// 结果集记录数
		/// </summary>
		int resultRows = 0;

		/// <summary>
		/// 连接信息数组
		/// </summary>
		string [] connection = new string[15];


#endregion
		/// <summary>
		/// 初始化变量
		/// </summary>
		private void InitVar()
		{
			this.SQL = "";
			this.SELECT = "";
			this.WHERE = "";
			this.intReturn = 0;
		}


		/// <summary>
		/// 构造SQL语句
		/// </summary>
		private void CreateSQL()
		{
			this.SQL = this.SELECT + " " + this.WHERE;
		}

		#region 番禺新农合
        /// <summary>
        /// 获取新农合科室列表
        /// </summary>
        /// <param name="al"></param>
        /// <param name="classType">新农合类型  PYNB:番禺新农合</param>
        /// <returns></returns>
		public int GetNBDeptList(ref ArrayList al,string classType)
		{
            string strSql = @"SELECT PDEPT_ID,PDEPT_NAME FROM fin_xnh_dept WHERE CLASS_TYPE = '{0}'";

			Neusoft.FrameWork.Models.NeuObject obj;
		
			try
			{
                strSql = string.Format(strSql, classType);
				this.ExecQuery(strSql);
				while(Reader.Read())
				{
					obj = new Neusoft.FrameWork.Models.NeuObject();
					obj.ID = Reader[0].ToString();
					obj.Name = Reader[1].ToString();					
					al.Add(obj);
				}

				Reader.Close();
				return 1;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return -1;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }

		}
        /// <summary>
        /// 获取新农合医保合作医院代码列表
        /// </summary>
        /// <param name="al"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        public int GetNBHospitalList(ref ArrayList al, string classType)
		{
			string strSql = "";

            if (this.Sql.GetSql("PYMedicare.NB.HospitalGet", ref strSql) == -1)
            {
                strSql = @"select PHOSPITAL_ID,PHOSPITAL_NAME from PYNB_COM_HOSPITAL_LIST where CLASS_TYPE = '{0}'";
            }
			Neusoft.FrameWork.Models.NeuObject obj;
		
			
			try
			{
                strSql = string.Format(strSql, classType);
				this.ExecQuery(strSql);
				obj = new Neusoft.FrameWork.Models.NeuObject();
				obj.ID = "";
				obj.Name = "";					
				al.Add(obj);
				while(Reader.Read())
				{
					obj = new Neusoft.FrameWork.Models.NeuObject();
					obj.ID = Reader[0].ToString();
					obj.Name = Reader[1].ToString();					
					al.Add(obj);
				}

				Reader.Close();
				return 1;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return -1;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }

		}
        /// <summary>
        /// 获取新农合科室对照列表
        /// </summary>
        /// <param name="al"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
		public int GetNBDeptMatch(ref ArrayList al,string classType)
		{
			string strSql = "";

            if (this.Sql.GetSql("PYMedicare.NB.DeptMatch", ref strSql) == -1)
            {
                strSql = @"SELECT * FROM fin_xnh_deptmatch where class_type='{0}'";
            }
			Neusoft.FrameWork.Models.NeuObject obj;
			
			try
			{
                strSql = string.Format(strSql, classType);
				this.ExecQuery(strSql);
				while(Reader.Read())
				{
					obj = new Neusoft.FrameWork.Models.NeuObject();
					obj.ID = Reader[0].ToString();
					obj.Name = Reader[1].ToString();	
					obj.Memo = Reader[2].ToString();
					obj.User01 = Reader[3].ToString();
					obj.User02 = Reader[4].ToString();
					al.Add(obj);
				}

				Reader.Close();
				return 1;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return -1;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }

		}
        /// <summary>
        /// 保存新农合科室对照(新增、修改、删除)
        /// </summary>
        /// <param name="EditMode">1：新增 2：修改 3：删除</param>
        /// <param name="OperatorID"></param>
        /// <param name="DataObj"></param>
        /// <returns></returns>
		public int SaveNBDeptMatch(string EditMode,string OperatorID,Neusoft.FrameWork.Models.NeuObject DataObj)
		{
            string strSql = @"insert into fin_xnh_deptmatch
                               (
                                 DEPT_ID,
                                 DEPT_NAME,
                                 CLASS_TYPE,
                                 PDEPT_ID,
                                 PDEPT_NAME,
                                 CREATE_USER,
                                 CREATE_DATE
                                )
                                values
                                (
                                 '{0}','{1}','{5}','{2}','{3}','{4}',SYSDATE
                                )";
			
			Neusoft.FrameWork.Models.NeuObject obj;
			// 保存科室匹配
			if (EditMode == "1")       //新增
			{

                strSql = @"insert into fin_xnh_deptmatch
                               (
                                 DEPT_ID,
                                 DEPT_NAME,
                                 CLASS_TYPE,
                                 PDEPT_ID,
                                 PDEPT_NAME,
                                 CREATE_USER,
                                 CREATE_DATE
                                )
                                values
                                (
                                 '{0}','{1}','{5}','{2}','{3}','{4}',SYSDATE
                                )";

				try
				{					
					strSql = string.Format (strSql,DataObj.ID,DataObj.Name ,DataObj.User01 ,DataObj.User02 ,OperatorID,DataObj.User03);	
				
					this.ExecQuery(strSql);					
					return 1;
				}
				catch(Exception ex)
				{					
					this.ErrCode = ex.Message;
					this.Err = ex.Message;
					return -1;
				}
			}
			else if (EditMode == "2")  //修改
			{

                strSql = @" update fin_xnh_deptmatch
  set
  PDEPT_ID = '{1}',
  PDEPT_NAME = '{2}',
  CREATE_USER ='{3}' ,
  CREATE_DATE = SYSDATE
where  DEPT_ID = '{0}'  and CLASS_TYPE = '{4}'";

				try
				{
					
					strSql = string.Format (strSql,DataObj.ID ,DataObj.User01 ,DataObj.User02,OperatorID ,DataObj.User03);					
					this.ExecQuery(strSql);
					
					return 1;
				}
				catch(Exception ex)
				{
					this.ErrCode = ex.Message;
					this.Err = ex.Message;
					return -1;
				}
			}
			else if (EditMode == "3")  //删除
			{

                strSql = @"DELETE fin_xnh_deptmatch 
where  DEPT_ID = '{0}'  and CLASS_TYPE = '{1}'";

				try
				{
					strSql =string.Format (strSql,DataObj.ID,DataObj.User03);
					this.ExecQuery(strSql);
					
					return 1;
				}
				catch(Exception ex)
				{
					this.ErrCode = ex.Message;
					this.Err = ex.Message;
					return -1;
				}
			}
			return 1;
		}
		/// <summary>
		/// 获取医院非药品信息
		/// </summary>
		/// <param name="dsItem"></param>
		public void QueryHosUndrug(ref System.Data.DataSet dsItem)
		{

            this.SQL = @"SELECT fin_com_undruginfo.item_code 编码,
               fin_com_undruginfo.item_name 名称,
               fin_com_undruginfo.unit_price 单价,
               '' 规格,
               '' 剂型,
               fin_com_undruginfo.spell_code 拼音码,
               fin_com_undruginfo.wb_code 五笔码,
               fin_com_undruginfo.Input_Code 自定义码
        FROM fin_com_undruginfo
        WHERE  fin_com_undruginfo.valid_state = '0'";
			if (this.ExecQuery(this.SQL, ref dsItem) == -1)
			{
				this.Err = "获取医院非药品项目失败！" + this.Err;
			
			}		
		}		
		/// <summary>
		/// 获取医院药品信息
		/// </summary>
		/// <param name="dsItem"></param>
		public void QueryHosMedicine(ref System.Data.DataSet dsItem)
		{
            this.SQL = @"SELECT pha_com_baseinfo.drug_code 编码,
           pha_com_baseinfo.trade_name 名称,
           pha_com_baseinfo.retail_price 价格,
           pha_com_baseinfo.specs 规格,
           (SELECT com_dictionary.name
            FROM com_dictionary
            WHERE com_dictionary.parent_code = fun_get_parentcode()
                  AND com_dictionary.current_code = fun_get_currentcode()
                  AND com_dictionary.type = 'DOSAGEFORM'
                  AND com_dictionary.code = pha_com_baseinfo.dose_model_code
                  AND ROWNUM = 1) 剂型,
           pha_com_baseinfo.spell_code 拼音码,
           pha_com_baseinfo.wb_code 五笔码,
           pha_com_baseinfo.custom_code 自定义码
    FROM pha_com_baseinfo
    WHERE  pha_com_baseinfo.valid_state = '0'";
			if (this.ExecQuery(this.SQL, ref dsItem) == -1)
			{
				this.Err = "获取医院药品项目失败！" + this.Err;
				
			}
			
		}
		
		/// <summary>
		/// 农保明细费用
		/// </summary>
		/// <param name="dsItem"></param>
		public void GetNBFeeByItem(string inpatientNo,ref System.Data.DataSet dsItem)
		{
            //还得关联医院代码
            this.SQL = @"select
b.center_item_code,b.center_item_name,b.center_item_model,
a.item_code,a.item_name,b.item_model,a.qty,a.current_unit,a.tot_cost,
c.item_rade,a.tot_cost * c.item_rade,a.charge_date
from fin_ipb_itemlist a,fin_com_compare b,fin_xnh_siitem c
where a.item_code = b.his_code
  and a.item_code = c.item_code
  and a.inpatient_no = '{0}'";
            try
            {
             
                this.SQL = string.Format(this.SQL, inpatientNo);
                if (this.ExecQuery(this.SQL, ref dsItem) == -1)
                {
                    this.Err = "获取项目明细失败！" + this.Err;
                }	
            }
            catch (Exception)
            {
                
                throw;
            }
					
		}

		/// <summary>
		/// 农保费用汇总
		/// </summary>
		/// <param name="dsItem"></param>
		public void GetNBFeeAllByItem(string patientNo,string pactCode,ref System.Data.DataSet dsItem)
		{
			//
			// 获取SQL语句
			//
			if (this.Sql.GetSql("RMedicare.Function.PYNB.GetNBAllFeeByItem", ref this.SQL) == -1)
            {
                #region 屏蔽
//                this.SQL = @"select * from (select
//a.item_code 医院编码,a.item_name 医院名称,a.unit_price 单价,sum(a.qty) 数量,sum(a.tot_cost) 金额,
//nvl(c.item_rade,0) 折扣比,nvl(sum(a.tot_cost * c.item_rade),0) 折扣额 ,
//NVL(b.center_code,'B000000000') 农保编码,NVL(b.center_name,'其它自费项目') 农保名称,a.current_unit 单位
//from fin_ipb_itemlist a,fin_com_compare b,fin_xnh_siitem c
//where a.item_code = b.his_code(+)
//  and b.center_code = c.item_code(+)
//  and a.inpatient_no ='{0}'
//	and b.pact_code='{1}'
//
//having sum(a.qty) > 0
//group by b.center_code,b.center_name,
//a.item_code,a.item_name,a.unit_price,c.item_rade,a.current_unit
//order by b.center_code,b.center_name,
//a.item_code,a.item_name,a.unit_price,c.item_rade,a.current_unit
//)
//union all
//select * from (
//select
//aa.drug_code,aa.drug_name,aa.unit_price,sum(aa.qty),sum(aa.tot_cost) tot_cost,
//nvl(cc.item_rade,0),nvl(sum(aa.tot_cost * cc.item_rade),0) rade_tot_cost,
//NVL(bb.center_code,'B000000000'),NVL(bb.center_name,'其它自费项目'),aa.current_unit
//from FIN_IPB_MEDICINELIST aa,fin_com_compare bb,fin_xnh_siitem cc
//where aa.drug_code = bb.his_code(+)
//  and bb.center_code = cc.item_code(+)
//  and aa.inpatient_no = '{0}'
//	and bb.pact_code='{1}'
//  having sum(aa.qty) > 0
//group by bb.center_code,bb.center_name,
//aa.drug_code,aa.drug_name,aa.unit_price,cc.item_rade,aa.current_unit
//order by bb.center_code,bb.center_name,
                //aa.drug_code,aa.drug_name,aa.unit_price,cc.item_rade,aa.current_unit)";

                #endregion
                this.SQL = @"select * from (select
a.item_code 医院编码,a.item_name 医院名称,a.unit_price 单价,sum(a.qty) 数量,sum(a.tot_cost) 金额,
1-nvl(c.item_rade,0) 折扣比,nvl(sum(a.tot_cost *(1-nvl(c.item_rade,0))),0) 折扣额 ,
NVL(b.center_code,'B000000000') 农保编码,NVL(b.center_name,'其它自费项目') 农保名称,a.current_unit 单位
from fin_ipb_itemlist a,fin_com_compare b,fin_xnh_siitem c
where a.item_code = b.his_code(+)
  and b.center_code = c.item_code(+)
  and a.inpatient_no ='{0}'
  and b.pact_code='{1}'

having sum(a.qty) > 0
group by b.center_code,b.center_name,
a.item_code,a.item_name,a.unit_price,c.item_rade,a.current_unit
order by b.center_code,b.center_name,
a.item_code,a.item_name,a.unit_price,c.item_rade,a.current_unit
)
union all
select * from (
select
aa.drug_code,aa.drug_name,aa.unit_price,sum(aa.qty),sum(aa.tot_cost) tot_cost,
1-nvl(cc.item_rade,0),nvl(sum(aa.tot_cost * (1-nvl(cc.item_rade,0))),0) rade_tot_cost,
NVL(bb.center_code,'B000000000'),NVL(bb.center_name,'其它自费项目'),aa.current_unit
from FIN_IPB_MEDICINELIST aa,fin_com_compare bb,fin_xnh_siitem cc
where aa.drug_code = bb.his_code(+)
  and bb.center_code = cc.item_code(+)
  and aa.inpatient_no = '{0}'
  and bb.pact_code='{1}'
  having sum(aa.qty) > 0
group by bb.center_code,bb.center_name,
aa.drug_code,aa.drug_name,aa.unit_price,cc.item_rade,aa.current_unit
order by bb.center_code,bb.center_name,
aa.drug_code,aa.drug_name,aa.unit_price,cc.item_rade,aa.current_unit)
";


                
            }
            this.SQL = string.Format(SQL, patientNo, pactCode);

			if (this.ExecQuery(this.SQL, ref dsItem) == -1)
			{
				this.Err = "获取项目汇总失败！" + this.Err;				
			}			
		}


        /// <summary>
        /// 查询指定日期的费用明细
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtFee"></param>
        /// <param name="dsItem"></param>
        public int  QueryFeeDetail(string inpatientNo,string pactCode, DateTime dtFee, ref System.Data.DataSet dsItem)
        {
            string strSql = @"select * from (

select m.fee_date 费用日期 ,m.drug_code 项目编码,m.drug_name 项目名称,m.unit_price 单价,m.qty 数量,m.tot_cost 金额,
1-nvl(s.item_rade,0) 折扣比,m.tot_cost-round(m.tot_cost*(nvl(s.item_rade,0)),2) 折扣额,
NVL(c.center_code,'B000000000') 农保编码,NVL(c.center_name,'其它自费项目') 农保名称,m.current_unit 单位 
from fin_ipb_medicinelist m ,fin_com_compare c,fin_xnh_siitem s
where m.inpatient_no='{0}' 
and c.pact_code='{1}'
and m.drug_code = c.his_code(+)
and c.center_code = s.item_code(+)
and (m.fee_date between to_date('{2}','yyyy-MM-dd hh24:mi:ss') and to_date('{3}','yyyy-MM-dd hh24:mi:ss'))
order by fee_date  )

union all

select * from (
select m.fee_date 费用日期 ,m.item_code 项目编码,m.item_name 项目名称,m.unit_price 单价,m.qty 数量,m.tot_cost 金额,
1-nvl(s.item_rade,0) 折扣比,m.tot_cost-round(m.tot_cost*(nvl(s.item_rade,0)),2) 折扣额,
NVL(c.center_code,'B000000000') 农保编码,NVL(c.center_name,'其它自费项目') 农保名称,m.current_unit 单位 
from fin_ipb_itemlist m ,fin_com_compare c,fin_xnh_siitem s
where m.inpatient_no='{0}' 
and c.pact_code='{1}'
and m.item_code = c.his_code(+)
and c.center_code = s.item_code(+)
and (m.fee_date between to_date('{2}','yyyy-MM-dd hh24:mi:ss') and to_date('{3}','yyyy-MM-dd hh24:mi:ss'))
order by fee_date  )";
            try
            {
                DateTime dtBegin = new DateTime(dtFee.Year, dtFee.Month, dtFee.Day, 0, 0, 0);
                DateTime dtEnd = new DateTime(dtFee.Year, dtFee.Month, dtFee.Day, 23, 59, 59);
                strSql = string.Format(strSql, inpatientNo, pactCode, dtBegin,dtEnd);
                if (this.ExecQuery(strSql, ref dsItem) == -1)
                {
                    this.Err = "查询指定日期的费用明细出错！";
                    return -1;
                }	

            }
            catch (Exception exp)
            {

                this.Err = "查询指定日期的费用明细出错！"+exp.Message;
                return -1;
            }
            return 1;
 
        }


		#region 三大目录表相关 
		/// <summary>
		/// 根据农保中心同步本地三大目录表
		/// </summary>
		/// <param name="obj"></param>
		public void InsertNBSDML(Neusoft.FrameWork.Models.NeuObject obj)
		{

			if (this.Sql.GetSql("RMedicare.Function.PYNB.InsertNBSDML", ref this.SQL) == -1)
			{
                this.SQL = @"insert into fin_xnh_siitem(
item_code,item_name,item_flag,item_rade,unit_type,update_date)
values('{0}','{1}','{2}',{3},'{4}',sysdate)";			
			}
            
			this.SQL = string.Format (this.SQL,obj.ID,obj.Name,obj.User01,Convert.ToDecimal(obj.User02),obj.User03);
			this.ExecNoQuery(this.SQL);			
		}

		/// <summary>
		/// 获取本地三大目录表
		/// </summary>
		/// <param name="al"></param>
		/// <returns></returns>
		public int GetLocalNBSDML(ref ArrayList al)
		{
			// 
			string strSql = "";

            if (this.Sql.GetSql("RMedicare.Function.PYNB.GetLocalNBSDML", ref strSql) == -1)
            {
                strSql = @"select
item_code,item_name,item_rade,unit_type
from PYNM_ITEM_RADE
order by item_code";
            }
			Neusoft.FrameWork.Models.NeuObject obj;	
			
			try
			{
				this.ExecQuery(strSql);
				while(Reader.Read())
				{
					obj = new Neusoft.FrameWork.Models.NeuObject();
					obj.ID = Reader[0].ToString();
					obj.Name = Reader[1].ToString();	
					obj.User01 = Reader[2].ToString();
					obj.User02 = Reader[3].ToString();
					obj.User03 = Reader[4].ToString();
					al.Add(obj);
				}

				Reader.Close();
				return 1;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return -1;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
		}


		/// <summary>
		/// 获取本地三大目录表
		/// </summary>
		/// <param name="al"></param>
		/// <returns></returns>
		public int GetLocalNBSDML(ref System.Data.DataSet dsItem)
		{
			// 
			string strSql = "";

            if (this.Sql.GetSql("RMedicare.Function.PYNB.GetLocalNBSDML", ref strSql) == -1)
            {
                strSql = @"select
item_code,item_name,item_rade,unit_type
from PYNM_ITEM_RADE
order by item_code";
            }
			if (this.ExecQuery(strSql, ref dsItem) == -1)
			{
				this.Err = "获取本地三大目录失败！" + this.Err;				
			}	
			return 0;
		}

		/// <summary>
		/// 根据项目表同步对照表
		/// </summary>
		public void SetNBSDML()
		{
            //待考虑是否有存在必要
			if (this.Sql.GetSql("RMedicare.Function.PYNB.SetNBSDML", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}					
			
			this.ExecNoQuery(this.SQL);			
		}


		/// <summary>
		/// 根据项目表同步对照表
		/// </summary>
		public void SynNBSDMLItem()
		{
			if (this.Sql.GetSql("RMedicare.Function.PYNB.SetNBSDML", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}					
			
			this.ExecNoQuery(this.SQL);			
		}

		/// <summary>
		/// 获取对照表
		/// </summary>
		/// <param name="dsItem"></param>
		public void GetSynNBSDMLItem(ref System.Data.DataSet dsItem)
		{
			//RMedicare.Function.PYNB.GetLocalNBSDML
			
			//
			// 获取SQL语句
			//
			if (this.Sql.GetSql("RMedicare.Function.PYNB.GetCompNBSDML", ref this.SQL) == -1)
			{
				this.Err = "获取项目对照表失败！" + this.Err;				
			}					

			if (this.ExecQuery(this.SQL, ref dsItem) == -1)
			{
				this.Err = "获取项目对照表失败！" + this.Err;				
			}			
		}


		/// <summary>
		/// 保存对照关系
		/// </summary>
		/// <param name="obj"></param>
		public void UpdateNBCompItem(Neusoft.FrameWork.Models.NeuObject obj)
		{
			if (this.Sql.GetSql("RMedicare.Function.PYNB.UpdateNBCompItem", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}					
			this.SQL = string.Format (this.SQL,obj.ID,obj.User01,obj.User02);

			this.ExecNoQuery(this.SQL);	
		}


		#endregion

		#region 获取出院病人相关信息
		/// <summary>
		/// 获取出院病人的相关信息
		/// </summary>
		/// <param name="patientNo"></param>
		/// <param name="outDiag"></param>
		/// <param name="totCost"></param>
		public void GetPatOutInfo(string patientNo, ref string outDiag, ref string totCost,ref string zginfo)
		{
			if (this.Sql.GetSql("RMedicare.Function.PYNB.Getinmaininfo", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}	
			this.SQL = string.Format (this.SQL,patientNo);

			try
			{
				this.ExecQuery(SQL);
				if (Reader.Read())
				{
					outDiag = Reader["diag_name"].ToString();
					totCost = Reader["tot_cost"].ToString();	
					zginfo  = Reader["zg"].ToString();
				}

				Reader.Close();
				return ;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return ;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
		}

		/// <summary>
		/// 获取出院病人的相关信息(对于一个人有多条数据则有问题)
		/// </summary>
		/// <param name="patientNo"></param>
		/// <param name="outDiag"></param>
		/// <param name="totCost"></param>
		public void GetPatOutInfo(string patientNo, ref string outDiag, ref string totCost,ref string zginfo,ref string outDate)
		{
			if (this.Sql.GetSql("RMedicare.Function.PYNB.Getinmaininfo", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}	
			this.SQL = string.Format (this.SQL,patientNo);

			try
			{
				this.ExecQuery(SQL);
				if (Reader.Read())
				{
					outDiag = Reader["diag_name"].ToString();
					totCost = Reader["tot_cost"].ToString();	
					zginfo  = Reader["zg"].ToString();
					outDate=Reader["out_date"].ToString();
				}

				Reader.Close();
				return ;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return ;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
		}

	
		/// <summary>
		/// 根据住院号获取出院病人的相关信息
		/// </summary>
		/// <param name="patientNo">住院号</param>	
		/// <param name="myNBpatient">病人实体</param>
        public Neusoft.FrameWork.Models.NeuObject GetPatOutInfo(string patientNo, ref Models.PYNBMainInfo myNBpatient)
		{			
			Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
			if (this.Sql.GetSql("RMedicare.Function.PYNB.NBGetinmaininfo", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}	
			this.SQL = string.Format (this.SQL,patientNo);

			try
			{
				this.ExecQuery(SQL);
				if (Reader.Read())
				{
					obj.ID = Reader["inpatient_no"].ToString();
					obj.Name = Reader["name"].ToString();
					obj.Memo = Reader["in_date"].ToString (); //入院日期
					obj.User01 = Reader["pdept_id"].ToString (); //入院科别
					obj.User02 = Reader["pdept_name"].ToString (); //入院科别
					obj.User03 = Reader["CLINIC_DIAGNOSE"].ToString();	//入院诊断		
					myNBpatient.Card_no = Reader["idenno"].ToString();	// 医疗证号	
					myNBpatient.OutDiagnosis=Reader["diag_name"].ToString();     //出院诊断
					myNBpatient.Out_date=Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["out_date"]);			//出院日期
					myNBpatient.Stock_date=Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["in_date"]);			//入院日期	
					myNBpatient.User01=Reader["tot_cost"].ToString();	   //总金额？
					myNBpatient.User02 = Reader["diag_name"].ToString();					
				}

				Reader.Close();
				return obj;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return obj;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
		}
		
		/// <summary>
		/// 根据流水号获取病人相关信息
		/// </summary>
		/// <param name="patientNo">农保号</param>
		/// <param name="inPatientNo">流水号</param>
		/// <param name="myNBpatient">病人实体</param>
		/// <returns></returns>
        public Neusoft.FrameWork.Models.NeuObject GetPatOutInfo(ref string cardNo, string inPatientNo, ref Models.PYNBMainInfo myNBpatient)
		{			
			Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
			if (this.Sql.GetSql("RMedicare.Function.PYNB.NBGetPatientinfo", ref this.SQL) == -1)
			{
                this.SQL = @"select a.*,b.pdept_id,b.pdept_name from fin_ipr_inmaininfo a,fin_xnh_deptmatch b
where a.inpatient_no = '{0}' 
  and a.dept_code = b.dept_id(+)
order by a.in_date desc ";	
			}	
			this.SQL = string.Format (this.SQL,inPatientNo);

            try
            {
                this.ExecQuery(SQL);
                if (Reader.Read())
                {
                    obj.ID = Reader["inpatient_no"].ToString();
                    obj.Name = Reader["name"].ToString();
                    obj.Memo = Reader["in_date"].ToString(); //入院日期
                    obj.User01 = Reader["pdept_id"].ToString(); //入院科别
                    obj.User02 = Reader["pdept_name"].ToString(); //入院科别
                    obj.User03 = Reader["CLINIC_DIAGNOSE"].ToString();	//入院诊断	

                    myNBpatient.Memo3 = Reader["PACT_CODE"].ToString();	//合同单位代码	
                    myNBpatient.Medi_nn = Reader["PATIENT_NO"].ToString();	//住院号	

                    myNBpatient.Card_no = Reader["idenno"].ToString();	// 医疗证号/农保号
                    myNBpatient.OutDiagnosis = Reader["diag_name"].ToString();     //出院诊断
                    myNBpatient.Out_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["out_date"].ToString());  //出院日期
                    myNBpatient.Stock_date = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["in_date"].ToString());			//入院日期			
                    cardNo = Reader["idenno"].ToString();	// 医疗证号/农保号
                }

                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
               
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return obj;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
		}


		#region 这个方法好像没用了			
		/// <summary>
		/// 获取出院病人的相关信息
		/// </summary>
		/// <param name="patientNo"></param>		
		public Neusoft.FrameWork.Models.NeuObject GetPatOutInfo(string patientNo ,ref string cardNo)
		{
			Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
			if (this.Sql.GetSql("RMedicare.Function.PYNB.NBGetinmaininfo", ref this.SQL) == -1)
			{
				this.Err = "获取SQL！" + this.Err;				
			}	
			this.SQL = string.Format (this.SQL,patientNo);

			try
			{
				this.ExecQuery(SQL);
				if (Reader.Read())
				{
					obj.ID = Reader["inpatient_no"].ToString();
					obj.Name = Reader["name"].ToString();
					obj.Memo = Reader["in_date"].ToString (); //入院日期
					obj.User01 = Reader["pdept_id"].ToString (); //入院科别
					obj.User02 = Reader["pdept_name"].ToString (); //入院科别
					obj.User03 = Reader["CLINIC_DIAGNOSE"].ToString();	//入院诊断		
	                cardNo = Reader["idenno"].ToString();	// 医疗证号						
				}

				Reader.Close();
				return obj;
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return obj;
			}
		}
		#endregion
		#endregion

		#region 根据住院号获取病人地址
		public string GetAddressByPatientNo(string patientNo)
		{
			string strSql="select a.home from FIN_IPR_INMAININFO a where a.patient_no='"+patientNo+"'";
			string result="";
			try
			{
				this.ExecQuery(strSql);
				if (Reader.Read())
				{
					result=Reader[0].ToString(); 
				}
				Reader.Close();
				return result;
			}
			catch
            {
			return "出错";
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }

		}
		#endregion

		#region 每日费用明细相关
		/// <summary>
		/// 每日费用明细相关
		/// </summary>
		/// <param name="dateStart">开始时间</param>
		/// <param name="dateEnd">截止时间</param>
		/// <param name="dsItem">查询结果</param>
		public void GetNBFeeByDaily(string dateStart, string dateEnd, ref System.Data.DataSet dsItem)
		{
			//
			// 获取SQL语句
			//
			if (this.Sql.GetSql("RMedicare.Function.PYNB.GetNBFeeByDaily", ref this.SQL) == -1)
			{
				this.Err = "获取每日费用明细失败！" + this.Err;				
			}					

			SQL=string.Format(SQL,dateStart,dateEnd);

			if (this.ExecQuery(this.SQL, ref dsItem) == -1)
			{
				this.Err = "获取每日费用明细失败！" + this.Err;				
			}			
		}
		#endregion


        #region 住院登记
        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
                return "";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }
        /// <summary>
        /// 更新医保结算主表信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateSiMainInfo(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = "";
            string balNo = this.GetBalNo(obj.ID);

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            //balNo = (Convert.ToInt32(balNo) + 1).ToString();
            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update.SOC.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, obj.ID, balNo, obj.SIMainInfo.InvoiceNo, obj.PVisit.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.PatientLocation.Dept.ID, obj.PVisit.PatientLocation.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, obj.PVisit.PatientLocation.Bed.ID,
                    obj.PVisit.InTime.ToString(), obj.PVisit.InTime.ToString(), obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.OutTime, obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name,
                    obj.SIMainInfo.BalanceDate.ToString(), obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost,
                    obj.SIMainInfo.ItemPayCost, obj.SIMainInfo.BaseCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.ItemYLCost,
                    obj.SIMainInfo.OwnCost, obj.SIMainInfo.OverTakeOwnCost, obj.SIMainInfo.Memo, this.Operator.ID,
                    obj.SIMainInfo.RegNo, obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost,
                    Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid), Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), 0, 0);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 插入医保结算信息表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertSIMainInfo(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            string balNo = GetBalNo(obj.ID);

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            balNo = (Convert.ToInt32(balNo) + 1).ToString();
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo.SOC.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.ID, balNo, obj.SIMainInfo.InvoiceNo, obj.PVisit.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.PatientLocation.Dept.ID, obj.PVisit.PatientLocation.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, obj.PVisit.PatientLocation.Bed.ID,
                    obj.PVisit.InTime.ToString(), obj.PVisit.InTime.ToString(), obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, this.Operator.ID, obj.SIMainInfo.HosNo, obj.SIMainInfo.RegNo,
                    obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost, obj.PVisit.OutTime.ToString(),
                    obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                    obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                    obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,
                    obj.SIMainInfo.OverTakeOwnCost, Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced),obj.SIMainInfo.TypeCode);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 作废医保信息住院主表
        /// </summary>
        /// <param name="clinicNo">门诊流水号 /住院流水号</param>
        /// <param name="typeCode">类型 1：门诊 2：住院</param>
        /// <returns></returns>
        public int CancleSiRegster(string clinicNo, string typeCode)
        {
            // string balNo = GetBalNo(clinicNo);

            //if (balNo == null || balNo == "")
            //{
            //    balNo = "0";
            //}

            //balNo = (Convert.ToInt32(balNo) + 1).ToString();
            string sql = @"UPDATE fin_ipr_siinmaininfo   --医保信息住院主表
             SET VALID_FLAG='0'             
					 WHERE   inpatient_no = '{0}'
					 and valid_flag = '1'
					 and type_code='{1}'";
            try
            {
                sql = string.Format(sql, clinicNo, typeCode);
                return this.ExecNoQuery(sql);

            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
		#endregion 


        /// <summary>
        ///  /// <summary>
        /// 更新医保信息住院主表
        /// </summary>
        /// <param name="strRegNo">就医登记号 </param>
        /// <param name="typeCode">类型 1：门诊 2：住院</param>
        /// <returns></returns>
        /// </summary>
        /// <param name="strRegNo"></param>
        /// <returns></returns>
        public int UpdateSiMainInfo(string strRegNo,string typeCode,string memo)
        {

            string sql = @"UPDATE fin_ipr_siinmaininfo   --医保信息住院主表
             SET REMARK='{2}'             
					 WHERE   reg_no= '{0}'
					 and valid_flag = '1'
					 and type_code='{1}'";
            try
            {
                sql = string.Format(sql, strRegNo, typeCode, memo);
                return this.ExecNoQuery(sql);

            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 根据住院流水号获取结算头信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetBalanceHeadById(string pactCode,string inpatientNo)
        {
            ArrayList alBalanceHead = new ArrayList();
            Neusoft.HISFC.Models.Fee.Inpatient.Balance BalanceHead = null;
            string strSql = @"select c.patient_no, c.invoice_no,c.name,c.oper_date,c.oper_code,(select e.empl_name from com_employee e where e.empl_code=c.oper_code and e.valid_state='1') oper_name
,c.tot_cost,c.pub_cost,c.own_cost from fin_ipr_siinmaininfo c where c.pact_code='{0}' and c.inpatient_no='{1}'";
            strSql = string.Format(strSql, pactCode, inpatientNo);

            try
            {
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    BalanceHead = new Neusoft.HISFC.Models.Fee.Inpatient.Balance();
                    BalanceHead.Name = Reader["patient_no"].ToString();
                    BalanceHead.Invoice.Name = Reader["name"].ToString();
                    BalanceHead.Invoice.ID = Reader["invoice_no"].ToString();
                    BalanceHead.BalanceOper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader["oper_date"].ToString());
                    BalanceHead.BalanceOper.ID = Reader["oper_code"].ToString();
                    BalanceHead.BalanceOper.Name = Reader["oper_name"].ToString();
                    BalanceHead.FT.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["tot_cost"].ToString());
                    BalanceHead.FT.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["pub_cost"].ToString());
                    BalanceHead.FT.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader["own_cost"].ToString());

                    alBalanceHead.Add(BalanceHead);
                }

                Reader.Close();
                return alBalanceHead;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        #endregion


        #region 新农合本地预约结算
        /// <summary>

        /// <summary>
        /// 获得单条已对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref Compare objCompare)
        {
            string strSql = @"SELECT pact_code,   --合同单位
     his_code,   --本地项目编码
     center_code,   --医保收费项目编码
    -- center_sys_class,   --项目类别 X-西药 Z-中药 L-诊疗项目 F-医疗服务设施
		 (SELECT fin_xnh_siitem.item_flag FROM fin_xnh_siitem WHERE  fin_xnh_siitem.item_code = FIN_COM_COMPARE.center_code) AS item_flag,
     center_name,   --医保收费项目中文名称
     center_ename,   --医保收费项目英文名称
     center_specs,   --医保规格
     center_dose,   --医保剂型编码
     center_spell,   --医保拼音代码
     center_fee_code,   --医保费用分类代码 1 床位费 2西药费3中药费4中成药5中草药6检查费7治疗费8放射费9手术费10化验费11输血费12输氧费13其他
     center_item_type,   --医保目录级别 1 基本医疗范围 2 广东省厅补充
     center_item_grade,   --医保目录等级 1 甲类(统筹全部支付) 2 乙类(准予部分支付) 3 自费
     center_rate,   --自负比例
     center_price,   --基准价格
     center_memo,   --限制使用说明(医保备注)
     his_spell,   --医院拼音
     his_wb_code,   --医院五笔码
     his_user_code,   --医院自定义码
     his_specs,   --医院规格
     his_price,   --医院基本价格
     his_dose,   --医院剂型
     oper_code,   --操作员
     oper_date,
     his_name,
     REGULARNAME    --操作时间

FROM fin_com_compare   --医疗保险对照表
WHERE   pact_code = '{0}'
AND    his_code = '{1}'
";

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    Neusoft.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();


                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新药品费用明细表
        /// 根据处方号，处方号，交易类型
        /// </summary>
        /// <returns></returns>
        public int UpdateMedItemList(Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_medicinelist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType=(item.TransType==Neusoft.HISFC.Models.Base.TransTypes.Positive?"1":"2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新非药品费用明细表
        /// </summary>
        /// 根据处方号，处方号流水号，交易类型
        /// <returns></returns>
        public int UpdateItemList(Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_itemlist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新非药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取农保预结算上次执行时间
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetJobLastExecDate(string inpatientNo,string pactCode)
        {
            string strSql = @"select A.lasttime from FIN_XNH_Localbalance A  WHERE  A.inpatientno='{0}' AND A.pactcode='{1}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, pactCode);
                return this.ExecSqlReturnOne(strSql,"");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算上次执行时间出错！" + exe.Message;
                return "";
            }
 
        }

        /// <summary>
        /// 获取农保预结算下次执行时间
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public string GetJobNextExecDate(string jobCode)
        {
            string strSql = @"select j.next_dtime from com_job j where j.job_code='{0}'";
            try
            {
                strSql = string.Format(strSql, jobCode);
                return this.ExecSqlReturnOne(strSql, "");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算下次执行时间出错！" + exe.Message;
                return "";
            }

        }

        /// <summary>
        /// 插入或更新预结算时间表
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertOrUpdateLocalBalanceTime(Neusoft.HISFC.Models.RADT.PatientInfo patient,DateTime dt)
        {
            string strSql = @"insert into FIN_XNH_Localbalance(inpatientno,pactcode,lasttime,instate) values('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}')";
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                //唯一键错误
                if (-1 == this.ExecNoQuery(strSql))
                {
                    strSql = @"update FIN_XNH_Localbalance set lasttime=to_date('{2}','yyyy-MM-dd hh24:mi:ss'),instate='{3}' where inpatientno='{0}' and pactcode='{1}'";
                    strSql=string.Format(strSql, patient.ID, patient.Pact.ID,dt,patient.PVisit.InState.ID.ToString());
                    return this.ExecNoQuery(strSql);
                }

            }
            catch (Exception exe)
            {
                this.Err = "插入或更新预结算时间表出错！" + exe.Message;
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 获取费用汇总记录
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Base.FT QueryFeeInfo(string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"SELECT f.tot_cost,f.own_cost,f.pub_cost,f.pay_cost  from fin_ipb_feeinfo f where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
	        try
			{
                strSql = string.Format(strSql, recipeNo, feeCode, execDept);
				this.ExecQuery(strSql);
                Neusoft.HISFC.Models.Base.FT ft = null;
				if (Reader.Read())
				{
                    ft = new Neusoft.HISFC.Models.Base.FT();
                    ft.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
				}
				Reader.Close();
                return ft;
			}
			catch(Exception exp)
            {
                this.Err= "获取费用汇总记录出错"+exp.Message;
                return null;
			}
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新费用汇总
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public int UpdateFeeInfo(Neusoft.HISFC.Models.Base.FT ft,string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"update fin_ipb_feeinfo f set f.tot_cost={3},f.own_cost={4},f.pub_cost={5},f.pay_cost={6} 
where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept,ft.TotCost,ft.OwnCost,ft.PubCost,ft.PayCost);
                return  this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新费用汇总记录出错" + exp.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取住院主表信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Base.FT QueryInMainInfo(string inpatientNo)
        {
            string strSql = @"select i.tot_cost,i.own_cost,i.pub_cost,i.pay_cost  from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql,inpatientNo);
                this.ExecQuery(strSql);
                Neusoft.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new Neusoft.HISFC.Models.Base.FT();
                    ft.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                }
                Reader.Close();
                return ft;
            }
            catch (Exception exp)
            {
                this.Err = "获取住院主表记录出错" + exp.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新住院主表记录
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int UpdateInMainInfo(Neusoft.HISFC.Models.Base.FT ft, string inpatientNo)
        {
            string strSql = @"update fin_ipr_inmaininfo i set i.tot_cost={1},i.own_cost={2},i.pub_cost={3},i.pay_cost={4} where  i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, ft.TotCost, ft.OwnCost, ft.PubCost, ft.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新住院主表记录出错" + exp.Message;
                return -1;
            }
        }

        #endregion


    }

    
}
