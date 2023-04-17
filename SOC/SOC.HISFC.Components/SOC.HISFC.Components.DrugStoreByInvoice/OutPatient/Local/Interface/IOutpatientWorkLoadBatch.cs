using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Local.Interface
{
   public interface IOutpatientWorkLoadBatch
    {
          /// <summary>
        /// 工作量初始化
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        string Init(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 工作量设置
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        string Set(FS.FrameWork.Models.NeuObject dept, List<FS.HISFC.Models.Pharmacy.DrugRecipe> drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 工作量设置
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        string Set(FS.FrameWork.Models.NeuObject dept, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 工作量重新初始化，用于更换人员时直接更换工作分配情况
        /// </summary>
        /// <param name="dept">需要设置工作量的科室</param>
        /// <param name="type">工作量类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>返回工作量分配情况，如发药：***，配药：***，核准：***</returns>
        string Reassigned(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);
    }
    }

