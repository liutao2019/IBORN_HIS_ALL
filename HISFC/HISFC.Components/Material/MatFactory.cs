using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Material;

namespace FS.HISFC.Components.Material
{
    public class MatFactory  : FS.HISFC.Components.Material.IFactory
    {
        public MatFactory()
        {

        }

        public IMatManager GetInInstance(FS.FrameWork.Models.NeuObject inPrivType, In.ucMatIn ucMatManager)
        {
            switch (inPrivType.Memo)
            {
                case "11":          //һ�����
                    return new Material.In.CommonInPriv(false, ucMatManager);
                case "13":          //�ڲ��������
                    if (inPrivType.ID == "02")
                    {
                        return new Material.In.InnerApplyPriv(false, ucMatManager); 
                    }
                    else 
                    {
                        return new Material.In.BuyApplyPriv(false, ucMatManager); 
                    }
                case "1A":          //��Ʊ���
                    return new Material.In.InvoiceInPriv(ucMatManager);
                case "16":          //��׼���
                    return new Material.In.ApproveInPriv(ucMatManager);
                case "18":          //�ڲ�����˿�����
                    return new Material.In.InnerApplyPriv(true, ucMatManager);
                case "19":			//����˿�
                    return new Material.In.BackInPriv(ucMatManager);
                case "1C":          //ϵͳ�л����
                    return new Material.In.CommonInPriv(true, ucMatManager);
                default:
                    return null;
                //				case "1C":          //�������
                //					return new Pharmacy.In.CommonInPriv(true,ucMatManager);
                    
                //				case "18":          //�ڲ�����˿�����
                //					return new Pharmacy.In.InnerApplyPriv(true,ucMatManager);
                //				case "1A":          //��Ʊ���
                //					return new Pharmacy.In.InvoiceInPriv(ucMatManager);
                //				case "16":          //��׼���
                //					return new Pharmacy.In.ApproveInPriv(ucMatManager);     
                //				case "19":          //����˿�
                //					return new Pharmacy.In.BackInPriv(ucMatManager);
            }
        }

        public IMatManager GetOutInstance(FS.FrameWork.Models.NeuObject outPrivType, Out.ucMatOut ucMatManager)
        {
            //			return new Material.Out.CommonOutPriv(false,ucMatManager);
            switch (outPrivType.Memo)
            {
                case "21":          //һ�����
                    return new Material.Out.CommonOutPriv(false, ucMatManager);
                case "26":          //�������
                    return new Material.Out.CommonOutPriv(true, ucMatManager);
                //case "24":          //��������
                //    return null;
                case "25":          //��������
                    return new Material.Out.ExamOutPriv(ucMatManager);
                case "22":			//�����˿�
                    return new Material.Out.BackOutPriv(ucMatManager);
            }
            return null;
        }

        //public IMatManager GetApplyInstance(FS.FrameWork.Models.NeuObject applyPrivType, Apply.ucApply ucMatApply)
        //{
        //    //���������
        //    //return new Material.Apply.InApplyPriv(false,ucMatApply);
        //    switch (applyPrivType.Memo)
        //    {
        //        case "13":          //�ڲ��������
        //            return new Material.Apply.InApplyPriv(false, ucMatApply);
        //        case "18":          //�ڲ�����˿�����
        //            return new Material.Apply.InApplyPriv(true, ucMatApply);
        //        case "24":          //��������
        //            return new Material.Apply.InApplyPriv(true, ucMatApply);
        //        default:
        //            return new Material.Apply.InApplyPriv(false, ucMatApply);

        //    }

        //    //return null;
        //}
    }
    
}
