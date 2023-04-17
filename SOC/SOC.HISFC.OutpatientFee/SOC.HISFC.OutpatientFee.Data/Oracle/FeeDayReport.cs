using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
     class FeeDayReport:AbstractFeeDayReport
    {
        public override string SelectAll
        {
            get { return @"" ; }
        }

        public override string Insert
        {
            get
            {
                return "pkg_rep.prc_opb_daybalance,opercode,22,1,{0}," +
                    "begindate,22,1,{1}," +
                    "endate,22,1,{2}," +
                    "Par_ErrCode,13,2,1," +
                    "Par_ErrText,22,2,1";
            }
        }

        public override string Delete
        {
            get
            {
                return @"pkg_rep.prc_opb_daybalance_cancel,
                            balanceOper,22,1,{0},
                            balanceNO,22,1,{1},
                            Par_ErrCode,13,2,1, 
                            Par_ErrText,22,2,1";
            }
        }
        //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        public override string SelectLastDate
        {
            get {
                return @"select a.end_date,a.balance_no from fin_opb_dayreport a
                                    where a.end_date=
                                    (
                                    SELECT MAX(t.end_date)
                                    FROM FIN_OPB_DAYREPORT t
                                    WHERE  t.oper_code = '{0}'
                                    and t.hospital_id='{1}'
                                    )
                                    and a.oper_code='{0}' 
                                    and a.hospital_id='{1}'
                                       ";
            }
        }

        
        public override string SelectLastFeeDate
        {
            get {
                return @"select min(oper_date) from (select a.oper_date from fin_opb_invoiceinfo a where  a.balance_flag ='0' and a.oper_code ='{0}')";
            }
        }
        //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        public override string SelectByMonth
        {
            get
            {
                return @"select balance_no,t.oper_date,t.check_flag from fin_opb_dayreport t
                                       where to_char(t.oper_date,'yyyy-mm')='{0}' and t.oper_code ='{1}'
                                        and  t.oper_code='{1}'
                                        and t.hospital_id='{2}'
                                       order by oper_date";
            }
        }

        public override string SelectDateByBalanceNO
        {
            get
            {
                return @"select balance_no,t.begin_date,t.end_date,t.oper_code from fin_opb_dayreport t
                                    where balance_no='{0}'";
            }
        }
    }
}
