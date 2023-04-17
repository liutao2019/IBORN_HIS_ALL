using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class UndrugGroup:AbstractUndrugGroup
    {
        public override string SelectAll
        {
            get {
                return @"select u.package_code,
                               u.package_name,
                               u.item_code,
                               a.item_name,
                               u.sort_id,
                               u.qty,
                               decode(a.valid_state, '1', u.valid_state, '0') valid_state,
                               a.spell_code,
                               a.wb_code,
                               a.input_code,
                               decode(unitflag,
                                      '1',
                                      fun_get_packageprice(u.package_code),
                                      unit_price) unit_price,
                               decode(unitflag,
                                      '1',
                                      fun_get_packageprice1(u.package_code),
                                      UNIT_PRICE1) UNIT_PRICE1,
                               decode(unitflag,
                                      '1',
                                      fun_get_packageprice2(u.package_code),
                                      UNIT_PRICE2) UNIT_PRICE2,
                               (select f.empl_name
                                  from com_employee f
                                 where f.empl_code = u.oper_code
                                   and f.valid_state = '1'),
                               u.oper_date,
                               nvl(u.item_rate,1)
                          from fin_com_undrugztinfo u
                          left join fin_com_undruginfo a
                            on u.item_code = a.item_code
                        ";
            }
        }

        public override string Insert
        {
            get { return string.Empty; }
        }

        public override string WhereByID
        {
            get {
                return @"
                            where  u.package_code='{0}' ";
                 }
        }

        public override string WhereByDetailID
        {
            get
            {
                return @"
                            where  u.item_code='{0}' ";
            }
        }
    }
}
