using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    public class PactInfo
    {
        /// <summary>
        /// 保存合同单位明细信息
        /// </summary>
        /// <param name="listAdd"></param>
        /// <param name="listModify"></param>
        /// <param name="listDelete"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int Save(List<FS.HISFC.Models.Base.PactInfo> listAdd, List<FS.HISFC.Models.Base.PactInfo> listModify, List<FS.HISFC.Models.Base.PactInfo> listDelete, ref string errorInfo)
        {
            FS.SOC.HISFC.Fee.BizLogic.PactInfo pactInfoMgr = new FS.SOC.HISFC.Fee.BizLogic.PactInfo();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            pactInfoMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //if (listDelete != null)
            //{
            //    foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in listDelete)
            //    {
            //        if (pactInfoMgr.Delete(pactItemRate) <= 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            errorInfo = "删除失败，原因：" + pactInfoMgr.Err;
            //            return -1;
            //        }
            //    }
            //}

            if (listModify != null)
            {
                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in listModify)
                {
                    if (pactInfoMgr.Update(pactInfo) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "更新失败，原因：" + pactInfoMgr.Err;
                        return -1;
                    }
                }
            }

            if (listAdd != null)
            {
                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in listAdd)
                {
                    if (pactInfoMgr.Insert(pactInfo) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "插入失败，原因：" + pactInfoMgr.Err;
                        return -1;
                    }
                }
            }

            //保存后接口内容
            if (InterfaceManager.GetISaveAllPactInfo() != null)
            {
                if (InterfaceManager.GetISaveAllPactInfo().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, new ArrayList(listAdd)) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errorInfo = InterfaceManager.GetISaveAllPactInfo().Err;
                    return -1;
                }

                if (InterfaceManager.GetISaveAllPactInfo().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, new ArrayList(listModify)) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errorInfo = InterfaceManager.GetISaveAllPactInfo().Err;
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
