using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC队列模板综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class QueueTemplate : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        /// <summary>
        /// 保存队列模板
        /// </summary>
        /// <param name="queueTemplate"></param>
        /// <param name="saveType"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public  int SaveQueueTemplate(FS.SOC.HISFC.Assign.Models.QueueTemplate queueTempalte, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ref string error)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.QueueTemplate queueTemplateMgr = new FS.SOC.HISFC.Assign.BizLogic.QueueTemplate();
            queueTemplateMgr.SetTrans(this.Trans);

            //删除模板
            int result = -1;
            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert)
            {
                result = queueTemplateMgr.IsExist(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断模板是否重复失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "队列模板已存在，不需要重复保存";
                    return 0;
                }

                result = queueTemplateMgr.IsExistQueueName(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断队列模板名称是否重复失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "队列模板名称已存在，请核对后保存";
                    return 0;
                }

                result = queueTemplateMgr.Insert(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "插入模板失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                result = queueTemplateMgr.IsExist(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断模板是否重复失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "队列模板已存在，不需要重复保存";
                    return 0;
                }
                result = queueTemplateMgr.IsExistQueueName(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "判断队列模板名称是否重复失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "队列模板名称已存在，请核对后保存";
                    return 0;
                }

                result = queueTemplateMgr.Update(queueTempalte);
                if (result < 0)
                {
                    this.RollBack();
                    error = "更新模板失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete)
            {
                //删除模板
                result = queueTemplateMgr.Delete(queueTempalte.ID);
                if (result < 0)
                {
                    this.RollBack();
                    error = "删除模板失败，原因：" + queueTemplateMgr.Err;
                    return -1;
                }
            }

            this.Commit();

            return 1;
        }
    }
}
