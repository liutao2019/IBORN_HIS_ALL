using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.Example.Outpatient
{
    /// <summary>
    /// [功能描述: 门诊药房工作量本地化实现]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、各项目工作量实现时屏蔽其他项目工作量实现方法，每个项目一个region
    /// </summary>
    public class WorkLoadInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientWorkLoad
    {
        #region 门诊工作量的实现
        /// <summary>
        /// 工作量初始化
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Init(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return "工作量设置已启动，配药扫描处方条码后扫描工号条码，发药记录系统登录人员";
        }

        /// <summary>
        /// 工作量设置
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Reassigned(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return "工作量设置已启动，扫描处方条码后扫描工号条码，发药记录系统登录人员";
        }

        /// <summary>
        /// 工作量重新初始化，用于更换人员时直接更换工作分配情况
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Set(FS.FrameWork.Models.NeuObject dept, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            if (type == "0")
            {
                //直接发药，计入发药工作量
                Outpatient.frmWorkLoad1 frmWorkLoad = new frmWorkLoad1("1", drugRecipe.StockDept.ID, dept.ID, drugRecipe.RecipeQty, drugRecipe.RecipeNO, drugRecipe.PatientName);
                frmWorkLoad.ShowDialog();
            }
            else if (type == "1")
            {
                //配药发药，计入配药工作量
                Outpatient.frmWorkLoad1 frmWorkLoad = new frmWorkLoad1("0", drugRecipe.StockDept.ID, dept.ID, drugRecipe.RecipeQty, drugRecipe.RecipeNO, drugRecipe.PatientName);
                frmWorkLoad.ShowDialog();
            }
            else if (type == "2")
            {
                //发药，计入发药工作量
                Common.WorkLoadManager workLoadManager = new FS.SOC.Local.DrugStore.Common.WorkLoadManager();
                workLoadManager.SetOutpatientWorkLoad(drugRecipe.RecipeNO, drugRecipe.StockDept.ID, dept.ID, "1", drugRecipe.SendOper.ID, drugRecipe.RecipeQty);
            }
            return "";
        }

        #endregion
    }
}
