using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.SOC.Local.SocialSecurity.Zdly.PublicFee
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
        
        /// <summary>
        /// 获取记账次数
        /// </summary>
        /// <param name="mCardNo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetJZCount(string cardNO, string pactCode, string mCardNo)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            return FS.FrameWork.Function.NConvert.ToInt32(
                dbMgr.ExecSqlReturnOne(string.Format("select count(distinct t.invoice_seq) from fin_opb_invoiceinfo t  where t.card_no ='{0}' and t.pact_code='{1}' and t.mcard_no='{2}' and t.cancel_flag='1' and t.oper_date >=trunc(sysdate)", 
                cardNO, pactCode, mCardNo), "0"));
        }

        /// <summary>
        /// 是否本院职工
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public static bool IsHospitalPub(FS.HISFC.Models.Registration.Register r, ref string errInfo)
        {
            if (r.Pact.PayKind.ID != "03")
            {
                return false;
            }

            string sql = @"select 1 from com_pactcompare cd where cd.parent_pact='0301' and cd.pact_code='{0}'";
            sql = string.Format(sql, r.Pact.ID);

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string num = dbMgr.ExecSqlReturnOne(sql);
            if (num == null || num.Equals("-1"))
            {
                return false;
            }
            else
            {
                return FS.FrameWork.Function.NConvert.ToBoolean(num);
            }

            return false;
        }

        /// <summary>
        /// 是否区公医
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public static bool IsQuPub(FS.HISFC.Models.Registration.Register r, ref string errInfo)
        {
            if (r.Pact.PayKind.ID != "03")
            {
                return false;
            }

            string sql = @"select 1 from com_pactcompare cd where cd.parent_pact='0302' and cd.pact_code='{0}'";
            sql = string.Format(sql, r.Pact.ID);

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string num = dbMgr.ExecSqlReturnOne(sql);
            if (num == null || num.Equals("-1"))
            {
                return false;
            }
            else
            {
                return FS.FrameWork.Function.NConvert.ToBoolean(num);
            }

            return false;
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

        /// <summary>
        /// 不上传项目
        /// </summary>
        private static Dictionary<string, string> dictionarySplitFee= new Dictionary<string, string>();

        /// <summary>
        /// 判断是否是高收费项目
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public static bool IsSplitFeeFlag(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (feeItemList.SplitFeeFlag)
            {
                return true;
            }
            else
            {
                if (dictionarySplitFee.Count == 0)
                {
                    FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    ArrayList alMinFee = conMgr.GetList("NOUPMINFEE");
                    foreach (FS.FrameWork.Models.NeuObject obj in alMinFee)
                    {
                        dictionarySplitFee[obj.ID] = obj.Name;
                    }
                }

                return dictionarySplitFee.ContainsKey(feeItemList.Item.MinFee.ID);
            }

        }
    }
}
