using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class InpatientDayReport : AbstractInaptientDayReport
    {
        public override string SelectAll
        {
            get { throw new NotImplementedException(); }
        }

        public override string Insert
        {
            get {
                return @"PKG_IPB.prc_ipb_invoice_daybalance,opercode,22,1,{0}, balancer,22,1,{1}, beginTimeStr,22,1,{2},endTimeStr,22,1,{3},Par_ErrCode,13,2,1,Par_ErrText,22,2,1";
            }
        }

        public override string Delete
        {
            get
            {
                return @"PKG_IPB.prc_ipb_invoice_daybalance_ca,opercode,22,1,{0}, balanceNO,22,1,{1},Par_ErrCode,13,2,1,Par_ErrText,22,2,1";
            }
        }

        public override string SelectLastDate
        {
            get
            {
                return @"select a.end_date,a.balance_no from fin_ipb_daybalance a
                                    where a.end_date=
                                    (
                                    SELECT MAX(t.end_date)
                                    FROM fin_ipb_daybalance t
                                    WHERE  t.oper_code = '{0}'
                                    )
                                    and a.oper_code='{0}'";
            }
        }

        public override string SelectByMonth
        {
            get
            {
                return @"select balance_no,t.oper_date from fin_ipb_daybalance t
                                       where to_char(t.oper_date,'yyyy-mm')='{0}' and t.oper_code='{1}'
                                       order by oper_date";
            }
        }

        public override string SelectDateByBalanceNO
        {
            get
            {
                return @"select balance_no,t.begin_date,t.end_date,t.oper_code from fin_ipb_daybalance t
                                    where balance_no='{0}'";
            }
        }
    }
}
