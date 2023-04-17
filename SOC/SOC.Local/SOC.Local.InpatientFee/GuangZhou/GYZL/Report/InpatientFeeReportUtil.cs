using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYZL.Report
{
    /// <summary>
    /// 在院患者费用明细帐报表工具类
    /// </summary>
    public class InpatientFeeReportUtil : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 根据住院号查询住院流水号
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientNo(string patientNo)
        {
            string strSql = @"select i.inpatient_no
                                from fin_ipr_inmaininfo i
                               where i.in_state in ('I','B')
                                 and i.patient_no like '%{0}'";

            try
            {
                strSql = string.Format(strSql, patientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList al = null;
            try
            {
                al = new ArrayList();
                while (this.Reader.Read())
                {
                    al.Add(this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
            return al;
        }

        /// <summary>
        /// 根据住院流水号查询患者信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public InpatientInfo GetInpatientInfo(string inpatientNo)
        {
            string strSql = @"select i.inpatient_no,
                                     i.patient_no||'【'||i.in_times||'】' inpatient_no,
                                     i.name,
                                     i.bed_no
                                from fin_ipr_inmaininfo i
                               where i.inpatient_no = '{0}'";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            InpatientInfo inpatient = new InpatientInfo();
            try
            {

                if (this.Reader.Read())
                {
                    inpatient.InPatientNo = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                    inpatient.PatientNo = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                    inpatient.InPatientName = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                    inpatient.BedNo = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
            return inpatient;
        }

        /// <summary>
        /// 根据住院流水号、开始结束时间查询汇总费用
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<InpatientFee> QueryInpatientFee(string inPatientNo, DateTime start, DateTime end)
        {
            string strSql = @"--期初余额
                              select inpatient_no,
                                     '1000-01-01',
                                     2,
                                     sum(tot) tot
                                from
                              (
                              select i.inpatient_no inpatient_no,
                                     sum(i.tot_cost) tot
                                from fin_ipb_itemlist i
                               where i.fee_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and i.inpatient_no = '{0}'
                               group by i.inpatient_no

                               union all

                              select m.inpatient_no inpatient_no,
                                     sum(m.tot_cost) tot
                                from fin_ipb_medicinelist m
                               where m.fee_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and m.inpatient_no = '{0}'
                               group by m.inpatient_no
 
                               union all
 
                              select b.inpatient_no,
                                     -sum(b.tot_cost)
                                from fin_ipb_balancehead b
                               where b.balance_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and b.inpatient_no = '{0}'
                               group by b.inpatient_no
                              )
                               group by inpatient_no
 
                               union all
                              --应收
                              select inpatient_no,
                                     fee_date,
                                     0,
                                     sum(tot) tot
                                from
                              (
                              select i.inpatient_no inpatient_no,
                                     to_char(i.fee_date,'yyyy-mm-dd') fee_date,
                                     sum(i.tot_cost) tot
                                from fin_ipb_itemlist i
                               where i.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and i.fee_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 and i.inpatient_no = '{0}'
                               group by inpatient_no, to_char(i.fee_date,'yyyy-mm-dd')

                               union all

                              select m.inpatient_no inpatient_no,
                                     to_char(m.fee_date,'yyyy-mm-dd') fee_date,
                                     sum(m.tot_cost) tot
                                from fin_ipb_medicinelist m
                               where m.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and m.fee_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 and m.inpatient_no = '{0}'
                               group by inpatient_no, to_char(m.fee_date,'yyyy-mm-dd')
                              )
                               group by inpatient_no, fee_date

                               union all
                              --已结
                              select b.inpatient_no,
                                     to_char(b.balance_date,'yyyy-mm-dd') balance_date,
                                     1,
                                     sum(b.tot_cost)
                                from fin_ipb_balancehead b
                               where b.balance_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and b.balance_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 and b.inpatient_no = '{0}'
                               group by b.inpatient_no,to_char(b.balance_date,'yyyy-mm-dd') ";

            try
            {
                strSql = string.Format(strSql, inPatientNo, start, end);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            List<InpatientFee> feeList = new List<InpatientFee>();
            try
            {
                while (this.Reader.Read())
                {
                    InpatientFee inpatientFee = new InpatientFee();
                    inpatientFee.InpatientNo = this.Reader.GetString(0);
                    inpatientFee.FeeDate = this.Reader.GetString(1);
                    int feeType = this.Reader.GetInt32(2);
                    if (feeType == 0)
                    {
                        inpatientFee.Type = FeeType.应收;
                    }
                    else if(feeType == 1)
                    {
                        inpatientFee.Type = FeeType.已结;
                    }
                    else if (feeType == 2)
                    {
                        inpatientFee.Type = FeeType.期初;
                    }
                    inpatientFee.Fee = this.Reader.GetDecimal(3);
                    feeList.Add(inpatientFee);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
            return feeList;
        }
    }
}
