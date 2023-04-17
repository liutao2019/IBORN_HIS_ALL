using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    public  class PactItemRate
    {
        /// <summary>
        /// 保存合同单位明细信息
        /// </summary>
        /// <param name="listAdd"></param>
        /// <param name="listModify"></param>
        /// <param name="listDelete"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int Save(List<FS.HISFC.Models.Base.PactItemRate> listAdd, List<FS.HISFC.Models.Base.PactItemRate> listModify, List<FS.HISFC.Models.Base.PactItemRate> listDelete, ref string errorInfo)
        {
            FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRateMgr = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            pactItemRateMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (listDelete != null)
            {
                foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in listDelete)
                {
                    if (pactItemRateMgr.Delete(pactItemRate) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "删除失败，原因：" + pactItemRateMgr.Err;
                        return -1;
                    }
                }
            }

            if (listModify != null)
            {
                foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in listModify)
                {
                    if (pactItemRateMgr.Update(pactItemRate) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "更新失败，原因：" + pactItemRateMgr.Err;
                        return -1;
                    }
                }
            }

            if (listAdd != null)
            {
                foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in listAdd)
                {
                    if (pactItemRateMgr.Insert(pactItemRate) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "插入失败，原因：" + pactItemRateMgr.Err;
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
