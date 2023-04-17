using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.OutpatientFee.Data.Oracle
{
    [DataBase(Neusoft.FrameWork.Management.Connection.EnumDBType.ORACLE)]
     class PayMode:AbstractPayMode
    {
        public override string UpdateForDayBalance
        {
            get {
                return @"update fin_opb_paymode
                                set BALANCE_FLAG = '1',
                                    BALANCE_NO = '{3}',
                                    BALANCE_OPCD = '{2}',
                                    BALANCE_DATE = sysdate
                                where   OPER_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                and   OPER_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                and   OPER_CODE = '{2}'"; }
        }

        public override string UpdateForCancelDayBalance
        {
            get {
                return @"update fin_opb_paymode
                                    set BALANCE_FLAG = '0',
                                        BALANCE_NO = '',
                                        BALANCE_OPCD = '',
                                        BALANCE_DATE =null
                                    where   BALANCE_NO='{0}'
                                    and   OPER_CODE = '{1}'";
            }
        }
    }
}
