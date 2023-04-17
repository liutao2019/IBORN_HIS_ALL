using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class RegisterDayReport : AbstractRegisterDayReport
    {
        public override string SelectLastDate
        {
            get
            {
                return @"select a.end_date,a.balance_no from fin_opr_daybalance a
                                    where a.end_date=
                                    (
                                    SELECT MAX(t.end_date)
                                    FROM fin_opr_daybalance t
                                    WHERE  t.oper_code = '{0}'
                                    )
                                    and a.oper_code='{0}'";
            }
        }

        public override string SelectLastFeeDate
        {
            get
            {
                return @"select min(oper_date) from (select a.oper_date from fin_opr_register a where  a.balance_flag ='0' and a.oper_code ='{0}') ";
            }
        }
        public override string SelectByMonth
        {
            get
            {
                return @"select balance_no,t.oper_date,t.invoice_check_flag from fin_opr_daybalance t
                                       where to_char(t.oper_date,'yyyy-mm')='{0}' and t.oper_code ='{1}'
                                        and t.oper_code='{1}'
                                       order by oper_date";
            }
        }

        public override string SelectDateByBalanceNO
        {
            get
            {
                return @"select balance_no,t.begin_date,t.end_date,t.oper_code from fin_opr_daybalance t
                                    where balance_no='{0}'";
            }
        }

        public override string SelectAll
        {
            get { return @""; }
        }

        public override string Insert
        {
            get { return @""; }
        }

        public override string Delete
        {
            get
            {
                return @"delete from fin_opr_daybalance t where t.oper_code='{0}' and  t.balance_no ='{1}' and t.invoice_check_flag = '0'";
            }
        }
    }
}
