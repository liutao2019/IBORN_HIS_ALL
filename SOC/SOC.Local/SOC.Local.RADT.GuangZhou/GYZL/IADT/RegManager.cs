using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.GuangZhou.GYZL.IADT
{
    public class RegManager:FS.FrameWork.Management.Database
    {
        public int InsertRegExtendInfo(FS.HISFC.Models.Registration.RegisterExtend regExtendObj)
        {
            string sql = @"INSERT INTO FIN_OPR_REGISTER_EXTEND (CLINIC_CODE, BOOKINGTYPE_ID, BOOKINGTYPE_NAME)VALUES ('{0}', '{1}', '{2}')";
            
            if (this.ExecNoQuery(string.Format(sql, regExtendObj.ID, regExtendObj.BookingTypeId, regExtendObj.BookingTypeName)) < 0)
            {
                this.Err = "插入预约挂号类型出错," + this.Err;
                return -1;
            }

            return 1;
        }
    }
}
