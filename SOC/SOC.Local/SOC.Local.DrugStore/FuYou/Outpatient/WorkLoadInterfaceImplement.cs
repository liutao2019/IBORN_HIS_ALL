using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    /// <summary>
    /// [功能描述: 门诊药房工作量本地化实现]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// </summary>
    public class WorkLoadInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientWorkLoad
    {
        FS.FrameWork.Models.NeuObject drugOper = new FS.FrameWork.Models.NeuObject();
        FS.FrameWork.Models.NeuObject sendOper = new FS.FrameWork.Models.NeuObject();

        #region 妇幼门诊工作量的实现

        /// <summary>
        /// 工作量初始化
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Init(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            frmWorkLoad frmWorkLoad = new frmWorkLoad();
            frmWorkLoad.Init(dept.ID);
            frmWorkLoad.ShowDialog();
            if (frmWorkLoad.DrugedOperCode != "-1")
            {
                this.drugOper.ID = frmWorkLoad.DrugedOperCode;
                this.drugOper.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(frmWorkLoad.DrugedOperCode);
            }
            else 
            {
                return "-1";
            }
            if (frmWorkLoad.SendedOperCode != "-1")
            {
                this.sendOper.ID = frmWorkLoad.SendedOperCode;
                this.sendOper.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(frmWorkLoad.SendedOperCode);
            }
            else
            {
                return "-1";
            }

            return "配药：" + this.drugOper.Name + "    发药：" + this.sendOper.Name + "";
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
            frmWorkLoad frmWorkLoad = new frmWorkLoad();
            frmWorkLoad.Init(dept.ID, this.drugOper.ID, this.sendOper.ID, "");
            frmWorkLoad.ShowDialog();

            //临时变量记录工作量分配，等人员分配都合理的情况下才赋值给全局变量，防止点击【取消】修改已经分配好的配发药人
            FS.FrameWork.Models.NeuObject tmpOper1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject tmpOper2 = new FS.FrameWork.Models.NeuObject();
            if (frmWorkLoad.DrugedOperCode != "-1")
            {
                tmpOper1.ID = frmWorkLoad.DrugedOperCode;
                tmpOper1.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(frmWorkLoad.DrugedOperCode);
            }
            else
            {
                return "配药：" + this.drugOper.Name + "    发药：" + this.sendOper.Name + "";
            }
            if (frmWorkLoad.SendedOperCode != "-1")
            {
                tmpOper2.ID = frmWorkLoad.SendedOperCode;
                tmpOper2.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(frmWorkLoad.SendedOperCode);
            }
            else
            {
                return "配药：" + this.drugOper.Name + "    发药：" + this.sendOper.Name + "";
            }

            this.drugOper = tmpOper1;
            this.sendOper = tmpOper2;

            return "配药：" + this.drugOper.Name + "    发药：" + this.sendOper.Name + "";
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
                Common.WorkLoadManager workLoadMgr = new FS.SOC.Local.DrugStore.Common.WorkLoadManager();
                workLoadMgr.SetOutpatientWorkLoad(drugRecipe.RecipeNO, drugRecipe.StockDept.ID, dept.ID, "0", this.drugOper.ID, drugRecipe.RecipeQty);
                workLoadMgr.SetOutpatientWorkLoad(drugRecipe.RecipeNO, drugRecipe.StockDept.ID, dept.ID, "1", this.sendOper.ID, drugRecipe.RecipeQty);
            }
            else if (type == "1")
            {
                //配药发药，计入配药工作量
            }
            else if (type == "2")
            {
                //发药，计入发药工作量
            }
            return "";
        }

        #endregion
    }
}
