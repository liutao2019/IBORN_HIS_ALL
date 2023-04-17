using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    public class ComItemExtendInfo
    {
        /// <summary>
        /// 保存基础项目扩展信息
        /// </summary>
        /// <param name="listAdd"></param>
        /// <param name="listModify"></param>
        /// <param name="listDelete"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int Save(List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listAdd, 
                        List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listModify,
                        List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listDelete, ref string errorInfo)
        {
            FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendInfoMgr = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            comItemExtendInfoMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (listDelete != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.ComItemExtend item in listDelete)
                {
                    if (comItemExtendInfoMgr.Delete(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "删除失败，原因：" + comItemExtendInfoMgr.Err;
                        return -1;
                    }
                }
            }

            if (listModify != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.ComItemExtend item in listModify)
                {
                    if (comItemExtendInfoMgr.Update(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "更新失败，原因：" + comItemExtendInfoMgr.Err;
                        return -1;
                    }
                }
            }

            if (listAdd != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.ComItemExtend item in listAdd)
                {
                    if (comItemExtendInfoMgr.Insert(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errorInfo = "插入失败，原因：" + comItemExtendInfoMgr.Err;
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
