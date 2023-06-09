using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.WinForms.Report.Function
{
    public class DrugDosage
    {
        private static FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 剂型帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper dosageHelper = null;

        /// <summary>
        /// 剂型
        /// </summary>
        private static System.Collections.Hashtable hsDosage = new Hashtable();

        /// <summary>
        /// 获取剂型
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public static string GetStaticDosage(string drugCode)
        {
            if (hsDosage.ContainsKey(drugCode))
            {
                if (hsDosage[drugCode] != null)
                {
                    return hsDosage[drugCode].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (dosageHelper == null)
                {
                    InitData();
                }
                FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(drugCode);
                hsDosage.Add(drugCode, dosageHelper.GetName(item.DosageForm.ID));
                return dosageHelper.GetName(item.DosageForm.ID);
            }
        }

        /// <summary>
        /// 基础数据初始化
        /// </summary>
        private static void InitData()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alList = consManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            if (alList != null)
            {
                dosageHelper = new FS.FrameWork.Public.ObjectHelper(alList);
            }
        }
    }
}
