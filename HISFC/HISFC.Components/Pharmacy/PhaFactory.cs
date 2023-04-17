using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ����ģ�� ���ظ����������ʵ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
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
                case "11":          //һ�����
                    return new HISFC.Components.Pharmacy.In.CommonInPriv( false, ucPhaManager );
                case "1C":          //�������
                    return new HISFC.Components.Pharmacy.In.CommonInPriv( true, ucPhaManager );
                case "13":          //�ڲ��������
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
                case "18":          //�ڲ�����˿�����
                    return new HISFC.Components.Pharmacy.In.InnerApplyPriv( true, ucPhaManager );
                case "1A":          //��Ʊ���
                    return new HISFC.Components.Pharmacy.In.InvoiceInPriv( ucPhaManager );
                case "16":          //��׼���
                    return new HISFC.Components.Pharmacy.In.ApproveInPriv( ucPhaManager );     
                case "19":            //����˿�
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
                case "21":          //һ�����
                    return new HISFC.Components.Pharmacy.Out.CommonOutPriv( false, ucPhaManager );
                case "26":          //�������
                    return new HISFC.Components.Pharmacy.Out.CommonOutPriv( true, ucPhaManager );
                case "24":          //��������
                    return new HISFC.Components.Pharmacy.Out.ApplyOutPriv( ucPhaManager );
                case "25":          //��������
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
