using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.Material
{
    /// <summary>
    /// [��������: ҩƷ��̬������ ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-07]<br></br>
    /// 
    /// <˵��>
    /// </˵��>
    /// </summary>
    public class MaterialSort
    {
        /// <summary>
        /// �����ʳ������ݰ������ʱ�������
        /// </summary>
        /// <param name="alApplyOut"></param>
        public static void SortApplyOutByItemID(ref List<FS.HISFC.Models.FeeStuff.Output> alOut)
        {
            CompareApplyOutByItemID compareInstance = new CompareApplyOutByItemID();
            alOut.Sort(compareInstance);
        }

        /// <summary>
        /// �����ʳ������ݰ��ղ��ż����ʱ�������
        /// </summary>
        /// <param name="alApplyOut"></param>
        public static void SortOutputByDeptAndItemID(ref List<FS.HISFC.Models.FeeStuff.Output> alOut)
        {
            CompareOutputByDeptAndItemID compareInstance = new CompareOutputByDeptAndItemID();
            alOut.Sort(compareInstance);
        }

        /// <summary>
        /// ������������ݰ������ʱ�������
        /// </summary>
        /// <param name="alApplyOut"></param>
        public static void SortInputByItemID(ref List<FS.HISFC.Models.FeeStuff.Input> alInput)
        {
            CompareInputByItemID compareInstance = new CompareInputByItemID();
            alInput.Sort(compareInstance);
        }

        /// <summary>
        /// ������������ݰ������ʱ�������
        /// </summary>
        /// <param name="alStockPlan"></param>
        public static void SortStockPlanByCompany(ref List<FS.HISFC.Models.FeeStuff.InputPlan> alStockPlan)
        {
            CompareStockPlanByCompany compareInstance = new CompareStockPlanByCompany();
            alStockPlan.Sort(compareInstance);
        }
    }

    /// <summary>
    /// ��������������
    /// </summary>
    internal class CompareApplyOutByItemID : IComparer<FS.HISFC.Models.FeeStuff.Output>
    {
        public int Compare(FS.HISFC.Models.FeeStuff.Output o1, FS.HISFC.Models.FeeStuff.Output o2)
        {
            //FS.HISFC.Models.Material.Output o1 = (x as FS.HISFC.Models.Material.Output).Clone();
            //FS.HISFC.Models.Material.Output o2 = (y as FS.HISFC.Models.Material.Output).Clone();

            string oX = o1.StoreBase.Item.ID;
            string oY = o2.StoreBase.Item.ID;     

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = string.Compare(oX.ToString(), oY.ToString());
            }

            return nComp;
        }

    }

    /// <summary>
    /// ��������������
    /// </summary>
    internal class CompareOutputByDeptAndItemID : IComparer<FS.HISFC.Models.FeeStuff.Output>
    {
        public int Compare(FS.HISFC.Models.FeeStuff.Output o1, FS.HISFC.Models.FeeStuff.Output o2)
        {
            //FS.HISFC.Models.Material.Output o1 = (x as FS.HISFC.Models.Material.Output).Clone();
            //FS.HISFC.Models.Material.Output o2 = (y as FS.HISFC.Models.Material.Output).Clone();

            string oX = o1.StoreBase.TargetDept.ID + o1.StoreBase.Item.ID;
            string oY = o2.StoreBase.TargetDept.ID + o2.StoreBase.Item.ID;

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = string.Compare(oX.ToString(), oY.ToString());
            }

            return nComp;
        }

    }

    /// <summary>
    /// �������������
    /// </summary>
    internal class CompareInputByItemID : IComparer<FS.HISFC.Models.FeeStuff.Input>
    {
        public int Compare(FS.HISFC.Models.FeeStuff.Input o1, FS.HISFC.Models.FeeStuff.Input o2)
        {
            //FS.HISFC.Models.Material.Input o1 = (x as FS.HISFC.Models.Material.Input).Clone();
            //FS.HISFC.Models.Material.Input o2 = (y as FS.HISFC.Models.Material.Input).Clone();

            string oX = o1.StoreBase.Item.ID;
            string oY = o2.StoreBase.Item.ID;

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = string.Compare(oX.ToString(), oY.ToString());
            }

            return nComp;
        }

    }

    /// <summary>
    /// �ɹ��ƻ�������
    /// </summary>
    internal class CompareStockPlanByCompany : IComparer<FS.HISFC.Models.FeeStuff.InputPlan>
    {
        public int Compare(FS.HISFC.Models.FeeStuff.InputPlan o1, FS.HISFC.Models.FeeStuff.InputPlan o2)
        {
            string oX = o1.Company.ID + o1.StoreBase.Item.ID;
            string oY = o2.Company.ID + o2.StoreBase.Item.ID;

            int nComp;

            if (oX == null)
            {
                nComp = (oY != null) ? -1 : 0;
            }
            else if (oY == null)
            {
                nComp = 1;
            }
            else
            {
                nComp = string.Compare(oX.ToString(), oY.ToString());
            }

            return nComp;
        }

    }

}
