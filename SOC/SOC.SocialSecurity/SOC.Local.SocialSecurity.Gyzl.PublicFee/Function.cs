using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.SocialSecurity.Gyzl.PublicFee
{
    class Function
    {
        /// <summary>
        /// 获取本院职工的工号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public static string GetEmplCode(string cardNO)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            return dbMgr.ExecSqlReturnOne(string.Format("select t.markno from fin_opb_accountcard t where t.card_no='{0}' and t.type='0'", cardNO), "");
        }

        public static bool IsDrug(EnumItemType itemType, FS.FrameWork.Models.NeuObject minFee)
        {
            if (itemType == EnumItemType.Drug)
            {
                return true;
            }
            else if (minFee == null)
            {
                return false;
            }
            else if ("001".Equals(minFee.ID) || "002".Equals(minFee.ID) || "003".Equals(minFee.ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
