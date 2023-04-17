using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    public class CompoundPrintImplment:FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint
    {
        #region ICompoundPrint 成员

        public int PrintCompound(System.Collections.ArrayList allData)
        {
            throw new NotImplementedException();
        }

        public int PrintCompoundLabel(System.Collections.ArrayList allData)
        {
            if (allData == null || allData.Count == 0)
            {
                return -1;
            }

            CompandGroupSort compoundGroupSort = new CompandGroupSort();

            allData.Sort(compoundGroupSort);

            int printIndex = 0;

            Hashtable hsIndex = new Hashtable();

            Hashtable hsCompoundGroup = new Hashtable();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in allData)
            {
                if (hsCompoundGroup.Contains(applyInfo.CompoundGroup))
                {
                    ArrayList alCompoundData = (hsCompoundGroup[applyInfo.CompoundGroup] as ArrayList).Clone() as ArrayList;
                    alCompoundData.Add(applyInfo);
                    hsCompoundGroup[applyInfo.CompoundGroup] = alCompoundData;
                }
                else
                { 
                    printIndex ++;
                    ArrayList alCompoundData = new ArrayList();
                    alCompoundData.Add(applyInfo);
                    hsCompoundGroup.Add(applyInfo.CompoundGroup, alCompoundData);
                    hsIndex.Add( printIndex,applyInfo.CompoundGroup);
                }
            }

            for (int index = 0; index < hsIndex.Count; index++)
            {
                ucCompoundPrintLable uclabelPrint = new ucCompoundPrintLable();

                string compoundGroup = hsIndex[index + 1].ToString();

                ArrayList alCompoundPrintData = hsCompoundGroup[compoundGroup] as ArrayList;

                uclabelPrint.SetValue(alCompoundPrintData, index + 1);

            }

            return 1;
        }

        public int PrintCompoundList(System.Collections.ArrayList allData)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// 按照批次号排序
    /// </summary>
    class CompandGroupSort : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
            FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
            string str1 = o1.CompoundGroup + o1.UseTime + o1.Item.ID;
            string str2 = o2.CompoundGroup + o1.UseTime + o2.Item.ID;

            return string.Compare(str1, str2);
        }
    } 
}
