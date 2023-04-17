using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    /// <summary>
    /// [功能描述: 门诊药房工作量本地化实现]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-06]<br></br>
    /// 说明：
    /// 1、各项目工作量实现时屏蔽其他项目工作量实现方法，每个项目一个region
    /// </summary>
    public class WorkLoadInterfaceImplement : Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientWorkLoad
    {
        /// <summary>
        /// 工作量初始化
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Init(Neusoft.FrameWork.Models.NeuObject dept, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return "";
        }

        /// <summary>
        /// 工作量设置
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Reassigned(Neusoft.FrameWork.Models.NeuObject dept, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            return "";
        }

        /// <summary>
        /// 工作量重新初始化，用于更换人员时直接更换工作分配情况
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        public string Set(Neusoft.FrameWork.Models.NeuObject dept, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, Neusoft.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            if (type == "0")
            {
                
            }
            else if (type == "1")
            {
                
            }
            else if (type == "2")
            {

            }
            return "";
        }

    }
}
