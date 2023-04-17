using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品工厂模型 返回各入出库类型实例]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-12]<br></br>
    /// </summary>
    public class PhaFactory : IFactory
    {
        public PhaFactory()
        {
 
        }

        public FS.HISFC.Components.Pharmacy.In.IPhaInManager GetInInstance(FS.FrameWork.Models.NeuObject inPrivType, FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager)
        {
            switch (inPrivType.Memo)
            {
                case "11":          //一般入库
                    return new HISFC.Components.Pharmacy.In.CommonInPriv( false, ucPhaManager );
                case "1C":          //特殊入库
                    return new HISFC.Components.Pharmacy.In.CommonInPriv( true, ucPhaManager );
                case "13":          //内部入库申请
                    return new HISFC.Components.Pharmacy.In.InnerApplyPriv( false, ucPhaManager );

                /*
                 * try
                {
                    AppDomainSetup dllDomin = new AppDomainSetup();

                    AppDomain innerDomin = AppDomain.CreateDomain("InnerApply", null, dllDomin);

                    UFC.Pharmacy.In.IPhaInManager inInterface = (UFC.Pharmacy.In.IPhaInManager)innerDomin.CreateInstanceAndUnwrap(System.Windows.Forms.Application.StartupPath + "\\PharmacyNotice.dll","PharmacyNotice.InnerApplyPriv",true, System.Reflection.BindingFlags.Default, null, new object[] { false, ucPhaManager }, null, null,null);

                    AppDomain.Unload(innerDomin);

                    return inInterface;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    return null;
                }
                */
                case "18":          //内部入库退库申请
                    return new HISFC.Components.Pharmacy.In.InnerApplyPriv( true, ucPhaManager );
                case "1A":          //发票入库
                    return new HISFC.Components.Pharmacy.In.InvoiceInPriv( ucPhaManager );
                case "16":          //核准入库
                    return new HISFC.Components.Pharmacy.In.ApproveInPriv( ucPhaManager );     
                case "19":            //入库退库
                    return new HISFC.Components.Pharmacy.In.BackInPriv( ucPhaManager );
            }

            EnumIMAInType enumType = EnumIMAInTypeService.GetEnumFromName(inPrivType.Memo);

            switch (enumType)
            {
                case EnumIMAInType.OuterApply:
                    return new HISFC.Components.Pharmacy.In.OuterApplyPriv( ucPhaManager );
            }
            return null;
        }

        public FS.HISFC.Components.Pharmacy.In.IPhaInManager GetOutInstance(FS.FrameWork.Models.NeuObject outPrivType, FS.HISFC.Components.Pharmacy.Out.ucPhaOut ucPhaManager)
        {            
            switch (outPrivType.Memo)
            {
                case "21":          //一般出库
                    return new HISFC.Components.Pharmacy.Out.CommonOutPriv( false, ucPhaManager );
                case "26":          //特殊出库
                    return new HISFC.Components.Pharmacy.Out.CommonOutPriv( true, ucPhaManager );
                case "24":          //出库申请
                    return new HISFC.Components.Pharmacy.Out.ApplyOutPriv( ucPhaManager );
                case "25":          //出库审批
                    return new HISFC.Components.Pharmacy.Out.ExamOutPriv( ucPhaManager );
                case "22":
                    return new HISFC.Components.Pharmacy.Out.BackOutPriv( ucPhaManager );                    
            }

            EnumIMAOutType enumType = EnumIMAOutTypeService.GetEnumFromName(outPrivType.Memo);

            switch (enumType)
            {
                case EnumIMAOutType.TransferOutput:
                    return new HISFC.Components.Pharmacy.Out.TransferOutput( ucPhaManager );
            }
            return null;
        }
    }
}
