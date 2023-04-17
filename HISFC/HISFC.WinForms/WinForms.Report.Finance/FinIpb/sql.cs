using System;
using System.Collections;
using System.Data;
using FS.HISFC.Models.Fee.Item;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.Report.Finance.FinIpb
{
    public class sql : FS.FrameWork.Management.Database 
    {
        /// <summary>
        /// 获取住院患者的项目信息，包括药品和非药品
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList GetItemList(string InpatientNo, string beginTime, string endTime)
        {
            string sql = string.Empty;
            if (Sql.GetSql("WinForms.Report.Finance.FinIpb.ucFinIpbPatientDayFee2.2", ref sql) == -1)
            {
                return null;
            }
            sql = " " + string.Format(sql,InpatientNo, beginTime, endTime);
            ArrayList items = new ArrayList(); //用于返回非药品信息的数组
			
			//执行当前Sql语句
			if (this.ExecQuery(sql) == -1)
			{
				this.Err = this.Sql.Err;

				return null;
			}

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    Undrug item = new Undrug();

                    item.ID = this.Reader[0].ToString();//非药品编码 
                    item.Name = this.Reader[1].ToString(); //非药品名称 
                    item.Specs = this.Reader[2].ToString(); //规格
                    item.Qty = NConvert.ToDecimal(this.Reader[3].ToString());//数量
                    item.PriceUnit = this.Reader[4].ToString();//单位
                    item.Price = NConvert.ToDecimal(this.Reader[5].ToString()); //默认价代替总额(为了方便)

                    items.Add(item);
                }//循环结束

				//关闭Reader
				this.Reader.Close();
				
				return items;
            }
            catch (Exception e)
            {
                this.Err = "获得项目基本信息出错！" + e.Message;
                this.WriteErr();

                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }



                items = null;

                return null;
            }	
        }

        public ArrayList GetMinFeeList(string InpatientNo, string beginTime, string endTime)
        {
            string sql = string.Empty;
            if (Sql.GetSql("WinForms.Report.Finance.FinIpb.ucFinIpbPatientDayFee2.1", ref sql) == -1)
            {
                return null;
            }
            sql = " " + string.Format(sql,InpatientNo, beginTime, endTime);
            ArrayList minFees = new ArrayList(); //用于最小费用金额信息的数组

            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj=new FS.FrameWork.Models.NeuObject ();
                    obj.ID = this.Reader[0].ToString(); //最小费用名字
                    obj.Memo = this.Reader[1].ToString(); //该最小费用对应的金额

                    minFees.Add(obj);
                }//循环结束

                //关闭Reader
                this.Reader.Close();

                return minFees;
            }
            catch (Exception e)
            {
                this.Err = "获得项目基本信息出错！" + e.Message;
                this.WriteErr();

                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                minFees = null;

                return null;
            }
        }



        public FS.FrameWork.Models.NeuObject GetPaykindId(string intpatientNo)
        {
            string strSql = @" select  i.paykind_code,i.pact_code,i.in_state,i.PREPAY_COST from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, intpatientNo);
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = this.Sql.Err;

                    return null;
                }

                FS.FrameWork.Models.NeuObject item = null;
                while (this.Reader.Read())
                {
                    item = new  FS.FrameWork.Models.NeuObject();
                    item.User01 = this.Reader[0].ToString();//paykind_id 
                    item.User02 = this.Reader[1].ToString(); //pact_code 
                    item.User03 = this.Reader[2].ToString(); //in_state 
                    item.Memo = this.Reader[3].ToString();//预交金
                }//循环结束
                this.Reader.Close();

                return item;
            }
            catch (Exception exp)
            {
                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                this.Err = this.Sql.Err;
                return null;
            }
          
        }

        public int UpdateInMainInfoFreeCost(string inpatientNo, decimal deTotOwnPay)
        {
             string strSql = @" update fin_ipr_inmaininfo i set i.free_cost={1} where i.inpatient_no='{0}'";
             try
             {
                 strSql = string.Format(strSql, inpatientNo, deTotOwnPay);
                 return this.ExecNoQuery(strSql);
             }
            catch(Exception exp)
            {
                this.Err = this.Sql.Err+exp.Message;
                return -1;
            }
 
        }
        public string GetSiEmplType(string inpatientNo)
        {
            string sql = "select si.empl_type from fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.valid_flag='1'";
            string str = "1";
            try
            {
                sql = string.Format(sql, inpatientNo);
                str = this.ExecSqlReturnOne(sql, "1");
                return str;
            }
            catch (Exception e)
            {

                return "1";
            }
        }
    }
}
