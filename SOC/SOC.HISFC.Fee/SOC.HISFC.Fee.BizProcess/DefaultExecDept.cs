using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using System.Data;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    /// <summary>
    /// [功能描述: 默认执行科室综合业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class DefaultExecDept : AbstractBizProcess
    {
        FS.SOC.HISFC.Fee.BizLogic.DefaultExecDept pactInfoMgr = new FS.SOC.HISFC.Fee.BizLogic.DefaultExecDept();

        /// <summary>
        /// 已经作废：貌似存储《key:功能分类，执行科室列表》
        /// 其实可以统一用《key:开立科室+（项目编码或功能分类），执行科室列表》代替了
        /// </summary>
        //private static Dictionary<string, List<FS.FrameWork.Models.NeuObject>> dictionaryFunctionClass = new Dictionary<string, List<FS.FrameWork.Models.NeuObject>>();

        /// <summary>
        /// 这里存储《key:开立科室+（项目编码或功能分类），执行科室列表》
        /// </summary>
        private static Dictionary<string, List<FS.FrameWork.Models.NeuObject>> dictionaryRecipeFunctionClass = new Dictionary<string, List<FS.FrameWork.Models.NeuObject>>();

        /// <summary>
        /// 这里存储《key:开立科室，所有科室列表（开立科室放在第一位）》
        /// </summary>
        private static Dictionary<string, List<FS.FrameWork.Models.NeuObject>> dictionaryAllExecDept = new Dictionary<string, List<FS.FrameWork.Models.NeuObject>>();

        /// <summary>
        /// 是否使用默认执行科室设置
        /// 此处只是根据开立科室、项目编码、功能分类的设置
        /// </summary>
        private int isUsedefaultExecSet = -1;

        /// <summary>
        /// 保存执行科室信息
        /// </summary>
        /// <param name="listAdd"></param>
        /// <param name="listModify"></param>
        /// <param name="listDelete"></param>
        /// <returns></returns>
        public int Save(List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listAdd, List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listModify, List<FS.SOC.HISFC.Fee.Models.DefaultExecDept> listDelete)
        {
            this.BeginTransaction();
            this.SetDB(pactInfoMgr);

            if (listDelete != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.DefaultExecDept pactItemRate in listDelete)
                {
                    if (pactInfoMgr.Delete(pactItemRate.ID) < 0)
                    {
                        this.RollBack();
                        this.err = "删除失败，" + pactInfoMgr.Err;
                        return -1;
                    }
                }
            }

            if (listModify != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.DefaultExecDept pactInfo in listModify)
                {
                    if (pactInfoMgr.Update(pactInfo) <= 0)
                    {
                        this.RollBack();
                        this.err = "更新失败，" + pactInfoMgr.Err;
                        return -1;
                    }
                }
            }

            if (listAdd != null)
            {
                foreach (FS.SOC.HISFC.Fee.Models.DefaultExecDept pactInfo in listAdd)
                {
                    if (pactInfoMgr.Insert(pactInfo) <= 0)
                    {
                        this.RollBack();
                        this.err = "插入失败，" + pactInfoMgr.Err;
                        return -1;
                    }
                }
            }

            this.Commit();

            return 1;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            this.SetDB(this.pactInfoMgr);
            return this.pactInfoMgr.GetID();
        }

        /// <summary>
        /// 查询对照信息（DataTable）
        /// </summary>
        /// <param name="compareID"></param>
        /// <returns></returns>
        public DataTable QueryForDataSet(string compareID)
        {
            return this.pactInfoMgr.QueryForDataSet(compareID);
        }

        /// <summary>
        /// 获取执行科室
        /// </summary>
        /// <param name="recipeDept">开方科室</param>
        /// <param name="undrug">物价项目</param>
        /// <returns>null 失败</returns>
        public List<FS.FrameWork.Models.NeuObject> GetExecDept(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            if (recipeDept == null || string.IsNullOrEmpty(recipeDept.ID))
            {
                this.err = "获取执行科室失败，开方科室不能为空";
                return null;
            }

            if (undrug == null || string.IsNullOrEmpty(undrug.ID))
            {
                this.err = "获取执行科室失败，项目不能为空";
                return null;
            }
            this.pactInfoMgr.SetTrans(this.Trans);
            List<FS.FrameWork.Models.NeuObject> execDepts = null;

            /* 默认执行科室获取规则
             * 1、基本信息维护了执行科室，则直接返回此执行科室
             * 2、根据开立科室、项目编码获取执行科室，如果有返回列表
             * 3、根据开立科室、功能分类获取执行科室，如果有返回列表
             * 4、根据分院编码、功能分类获取执行科室，如果有返回列表（此功能暂无）
             * 5、直接返回所有科室，默认开立科室排列在第一位
             * 
             * 还需完善
             * 1、现在基本信息维护3个执行科室，应该能够根据开立科室等获取正确的其中一个默认执行科室
             * */

            #region 找物价项目维护的执行科室信息
            if (!string.IsNullOrEmpty(undrug.ExecDept) && !undrug.ExecDept.Equals("无"))
            {
                string[] depts = undrug.ExecDept.Split('|');
                execDepts = new List<FS.FrameWork.Models.NeuObject>();
                foreach (string code in depts)
                {
                    if (!string.IsNullOrEmpty(code))
                    {
                        FS.FrameWork.Models.NeuObject obj = CommonController.CreateInstance().GetDepartment(code);
                        //if (obj == null)
                        //{
                        //    this.err = "获取执行科室失败，获取科室信息失败";
                        //    return null;
                        //}
                        if (obj != null)
                        {
                            execDepts.Add(obj);
                        }
                    }
                }

                if (execDepts != null && execDepts.Count > 0)
                {
                    return execDepts;
                }
            }
            #endregion

            if (isUsedefaultExecSet == -1)
            {
                FS.FrameWork.Management.ControlParam controlMgr = new FS.FrameWork.Management.ControlParam();

                //这里的控制参数 jing.zhao自己搞一个吧
                string value = controlMgr.QueryControlerInfo("", false);

                if (!string.IsNullOrEmpty(value) && value != "-1")
                {
                    isUsedefaultExecSet = 1;
                }
                else
                {
                    isUsedefaultExecSet = 0;
                }
            }

            if (isUsedefaultExecSet == 1)
            {
                #region 根据开立科室、项目编码获取

                if (!dictionaryRecipeFunctionClass.ContainsKey(recipeDept.ID + undrug.ID))
                {
                    execDepts = this.pactInfoMgr.QueryExecDept("RecipeDept-FunctionClass", recipeDept.ID, undrug.ID);
                    if (execDepts != null && execDepts.Count > 0)
                    {
                        dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ID] = execDepts;
                        return execDepts;
                    }
                }
                else
                {
                    return dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ID];
                }

                #endregion

                #region 根据开立科室、功能分类获取

                if (!string.IsNullOrEmpty(undrug.ItemPriceType))
                {
                    if (!dictionaryRecipeFunctionClass.ContainsKey(recipeDept.ID + undrug.ItemPriceType))
                    {
                        execDepts = this.pactInfoMgr.QueryExecDept("RecipeDept-FunctionClass", recipeDept.ID, undrug.ItemPriceType);
                        if (execDepts != null && execDepts.Count > 0)
                        {
                            dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ItemPriceType] = execDepts;
                            return execDepts;
                        }
                    }
                    else
                    {
                        return dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ItemPriceType];
                    }
                }

                #endregion
            }

            #region 都不符合上面规则时,返回所有科室列表

            //这个比较慢 也缓存一下吧...

            if (!dictionaryAllExecDept.ContainsKey(recipeDept.ID))
            {
                execDepts = new List<FS.FrameWork.Models.NeuObject>();
                execDepts.Add(recipeDept);

                //安全期间  CommonController.CreateInstance().QueryDepartment()挨个复制吧
                foreach (FS.FrameWork.Models.NeuObject obj in CommonController.CreateInstance().QueryDepartment())
                {
                    if (obj.ID != recipeDept.ID)
                    {
                        execDepts.Add(obj);
                    }
                }

                dictionaryAllExecDept.Add(recipeDept.ID, execDepts);
                return execDepts;
            }
            else
            {
                return dictionaryAllExecDept[recipeDept.ID];
            }

            #endregion

            #region 旧的作废

            ////找物价项目维护的执行科室信息
            //if (!string.IsNullOrEmpty(undrug.ExecDept) && !undrug.ExecDept.Equals("无"))
            //{
            //    string[] depts = undrug.ExecDept.Split('|');
            //    execDepts = new List<FS.FrameWork.Models.NeuObject>();
            //    foreach (string code in depts)
            //    {
            //        FS.FrameWork.Models.NeuObject obj = CommonController.CreateInstance().GetDepartment(code);
            //        if (obj == null)
            //        {
            //            this.err = "获取执行科室失败，获取科室信息失败";
            //            return null;
            //        }
            //        execDepts.Add(obj);
            //    }

            //    if (execDepts.Count > 0)
            //    {
            //        return execDepts;
            //    }
            //}

            ////执行科室维护为空
            ////取对应的功能分类
            //if (string.IsNullOrEmpty(undrug.ItemPriceType))
            //{
            //    //返回开立科室
            //    execDepts = new List<FS.FrameWork.Models.NeuObject>();
            //    execDepts.Add(recipeDept);
            //    return execDepts;
            //}

            ////继续找根据开方科室和功能分类找执行科室
            //if (!dictionaryRecipeFunctionClass.ContainsKey(recipeDept.ID + undrug.ItemPriceType))
            //{
            //    execDepts = this.pactInfoMgr.QueryExecDept("RecipeDept-FunctionClass", recipeDept.ID, undrug.ItemPriceType);
            //    if (execDepts == null)
            //    {
            //        this.err = "获取执行科室失败，获取科室信息失败";
            //        return null;
            //    }

            //    //如果执行科室不为空，则返回
            //    if (execDepts.Count > 0)
            //    {
            //        dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ItemPriceType] = execDepts;
            //        return execDepts;
            //    }
            //}
            //else
            //{
            //    return dictionaryRecipeFunctionClass[recipeDept.ID + undrug.ItemPriceType];
            //}

            //if (!dictionaryFunctionClass.ContainsKey(undrug.ItemPriceType))
            //{
            //    //继续找功能分类对应的执行科室
            //    execDepts = this.pactInfoMgr.QueryExecDept("FunctionClass", undrug.ItemPriceType);
            //    if (execDepts == null)
            //    {
            //        this.err = "获取执行科室失败，获取科室信息失败";
            //        return null;
            //    }

            //    //如果执行科室不为空，则返回
            //    if (execDepts.Count > 0)
            //    {
            //        dictionaryFunctionClass[undrug.ItemPriceType] = execDepts;
            //        return execDepts;
            //    }
            //}
            //else
            //{
            //    return dictionaryFunctionClass[undrug.ItemPriceType];
            //}

            ////返回开立科室
            //execDepts = new List<FS.FrameWork.Models.NeuObject>();
            //execDepts.Add(recipeDept);
            //return execDepts;

            #endregion
        }
    }
}