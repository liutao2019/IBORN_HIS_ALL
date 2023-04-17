using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Output
{
    /// <summary>
    /// [功能描述: 出库业务管理（保留工厂模式）]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public class OutputBizManager : Base.IPharmacyBizManager
    {
        /// <summary>
        /// 根据出库类别获取业务流程管接口的实现
        /// </summary>
        /// <param name="outputType">出库类别</param>
        /// <returns></returns>
        public Base.IBaseBiz GetBizImplement(FS.FrameWork.Models.NeuObject outputType)
        {
            if (outputType.Memo == "21")
            {
                //一般出库
                CommonOutput CommonOutput = new CommonOutput();

                //按批出库需要单独处理，这里主要是把自定义权限类别传入
                CommonOutput.PriveType = outputType.Clone();

                return CommonOutput;
            }
            else if (outputType.Memo == "22")
            {
                //出库退库
                return new BackOutput();
            }
            else if (outputType.Memo == "25")
            {
                //出库审批
                ExamOutput ExamOutput = new ExamOutput();

                //按批出库需要单独处理，这里主要是把自定义权限类别传入
                ExamOutput.PriveType = outputType.Clone();

                return ExamOutput;
            }
            else if (outputType.Memo == "26")
            {
                //特殊出库：主要是科室领药
                SpecialOutput SpecialOutput = new SpecialOutput();

                //报损需要单独处理，这里主要是把自定义权限类别传入
                SpecialOutput.PriveType = outputType.Clone();

                return SpecialOutput;
            }
            else if (outputType.Memo == "29")
            {
                //特殊出库：主要是科室领药
                ChangePriceOutput ChangePriceOutput = new ChangePriceOutput();

                //报损需要单独处理，这里主要是把自定义权限类别传入
                ChangePriceOutput.PriveType = outputType.Clone();

                return ChangePriceOutput;
            }
            else if (outputType.Memo == "32")
            {
                //按批出库
                BatchCommonOutput BatchOutput = new BatchCommonOutput();

                //把自定义权限类别传入
                BatchOutput.PriveType = outputType.Clone();

                return BatchOutput;
            }
            else if (outputType.Memo == "33")
            {
                //按批出库
                BatchSpecialOutput BatchOutput = new BatchSpecialOutput();

                //把自定义权限类别传入
                BatchOutput.PriveType = outputType.Clone();

                return BatchOutput;
            }
            else if (outputType.Memo == "1F")
            {
                //财务核准出库
                return new Fin.ApproveOuput();
            }
            else if (outputType.Memo == "24")
            {
                ApplyOutOutput applyOut = new ApplyOutOutput();
                applyOut.PriveType = outputType.Clone();
                return applyOut;
            }

            else if (outputType.Memo == "34")
            {
                //财务核准出库
                return new ExamBackOutput();
            }

            return null;
        }

        /// <summary>
        /// 根据出库类别获取业务流程对应的数据选择列表接口的实现
        /// </summary>
        /// <param name="outputType">出库类别</param>
        /// <returns></returns>
        public SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataChooseList GetDataChooseListImplement(FS.FrameWork.Models.NeuObject outputType)
        {
            switch (outputType.Memo)
            {
                default:
                    //一般出库
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
        /// 获取出库类别获取业务流程对应的数据明细显示控件接口的实现
        /// </summary>
        /// <param name="outputType"></param>
        /// <returns></returns>
        public SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IDataDetail GetDataDetailImplement(FS.FrameWork.Models.NeuObject outputType)
        {
            switch (outputType.Memo)
            {
                default:
                    //一般出库
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
