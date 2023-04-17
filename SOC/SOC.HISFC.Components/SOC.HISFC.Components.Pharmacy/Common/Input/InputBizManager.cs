using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.Components.Pharmacy.Base;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Input
{
    /// <summary>
    /// [功能描述: 入库业务管理（保留工厂模式）]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public class InputBizManager : IPharmacyBizManager
    {
        /// <summary>
        /// 根据入库类别获取业务流程管接口的实现
        /// </summary>
        /// <param name="inputType">入库类别</param>
        /// <returns></returns>
        public Base.IBaseBiz GetBizImplement(FS.FrameWork.Models.NeuObject inputType)
        {
            if (inputType.Memo == "11")
            {
                //一般入库
                return new Common.Input.CommonInput();
            }
            else if (inputType.Memo == "12")
            {
                return new Common.Input.OuterInputApply();
            }
            else if (inputType.Memo == "1A")
            {
                //发票入库：发票补录
                return new Common.Input.InvoiceInput();
            }
            else if (inputType.Memo == "19")
            {
                //入库退库：外退
                if (inputType.ID == "06")
                {
                    return new Common.Input.BackCommonInput();
                }
                //入库退库：特殊入库后退库
                return new Common.Input.BackSpecialInput();
            }
            else if (inputType.Memo == "1C")
            {
                //特殊入库：科室回笼
                Common.Input.SpecialInput specialInput = new SpecialInput();
                specialInput.PriveType = inputType;
                return specialInput;
            }
            else if (inputType.Memo == "13")
            {
                //内部入库申请：科室领药申请
                return new Common.Plan.InnerInputApply();
            }
            else if (inputType.Memo == "16")
            {
                //核准入库
                return new Common.Input.ApproveInput();
            }
            else if (inputType.Memo == "2A")
            {
                //即入即出
                return new Common.Input.OutputAfterInput();
            }
            else if (inputType.Memo == "18")
            {
                //内部入库退库申请
                return new Common.Plan.InnerBackInputApply();
            }
            else if (inputType.Memo == "1F")
            {
                //财务核准入库
                return new Fin.ApproveInput();
            }
            return null;
        }

        /// <summary>
        /// 根据入库类别获取业务流程对应的数据选择列表接口的实现
        /// </summary>
        /// <param name="inputType">入库类别</param>
        /// <returns></returns>
        public SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList GetDataChooseListImplement(FS.FrameWork.Models.NeuObject inputType)
        {
            switch (inputType.Memo)
            {
                default:
                    object interfaceImplement = InterfaceManager.GetDataChooseListControl();
                    if (interfaceImplement == null || !(interfaceImplement is FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList))
                    {
                        return new Base.ucDataChooseList();
                    }
                    if (interfaceImplement is System.Windows.Forms.Control)
                    {
                        return interfaceImplement as FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList;
                    }
                    break;

            }
            return null;
        }

        /// <summary>
        /// 获取入库类别获取业务流程对应的数据明细显示控件接口的实现
        /// </summary>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail GetDataDetailImplement(FS.FrameWork.Models.NeuObject inputType)
        {
            switch (inputType.Memo)
            {
                default:
                    //一般入库
                    object interfaceImplement = InterfaceManager.GetDataDetailControl();
                    if (interfaceImplement == null || !(interfaceImplement is SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail))
                    {
                        return new Base.ucDataDetail();
                    }
                    if (interfaceImplement is System.Windows.Forms.Control)
                    {
                        return interfaceImplement as SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail;
                    }
                    break;
            }
            return null;
        }

    }
}
