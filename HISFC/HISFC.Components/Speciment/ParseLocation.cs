using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public class ParseLocation
    {
        private static IceBoxManage boxManage = new IceBoxManage();

        public static IceBoxManage BoxManage
        {
            set
            {
                boxManage = value;
            }
        }

        private static string GetLocation(string barcode)
        {
            string boxId = Convert.ToInt32(barcode.Substring(2, 3)).ToString();
            IceBox iceBox = boxManage.GetIceBoxById(boxId);
            if (iceBox == null || iceBox.IceBoxId <= 0)
            {
                return "";
            }
            string loc1 = barcode.Replace("C", "层").Replace("J", "架");
            string loc2 = "";
            string loc3 = "";
            if (barcode.Length > 11)
            {
                loc2 = iceBox.IceBoxName + loc1.Substring(5, 7);
                loc3 = " 深:" + loc1.Substring(12, 1) + " 高:" + loc1.Substring(13, 1);
                loc2 += loc3;
            }
            else
            {
                loc2 = iceBox.IceBoxName + loc1.Substring(5);
            }
            loc2 = loc2.Replace("（", "(").Replace("）", ")");

            return loc2;

        }
        public static string ParseSpecBox(string boxBarcode)
        {
            return GetLocation(boxBarcode);
        }
        public static string ParseShelf(string shelfBarCode)
        {
            return GetLocation(shelfBarCode);
        }
    }
}
