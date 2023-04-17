using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Class
{
    public class Function
    {
        /// <summary>
        /// �������֤�Ż�ȡ����
        /// </summary>
        /// <param name="idNO">���֤��</param>
        /// <returns></returns>
        public static string  GetBirthDayFromIdNO(string idNO,ref string err)
        {
            
            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return "-1";
            }
            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            string datestr = "";
            if (idNO.Length >= 16)
            {
                datestr = idNO.Substring(6, 8);
                string year = datestr.Substring(0, 4);
                string month = datestr.Substring(4, 2);
                string day = datestr.Substring(6, 2);
                datestr = year + "-" + month + "-" + day;
            }
            return datestr;
        }
        /// <summary>
        /// �������֤�Ż�ȡ�Ա�
        /// </summary>
        /// <param name="idNO">���֤��</param>
        /// <returns></returns>
        public static FS.FrameWork.Models.NeuObject GetSexFromIdNO(string idNO,ref string err)
        {
            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return null;
            }

            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }

            FrameWork.Models.NeuObject sexobj = new FS.FrameWork.Models.NeuObject();
            if (idNO.Length >= 16)
            {
                int flag = FS.FrameWork.Function.NConvert.ToInt32((idNO.Substring(16, 1)));
                HISFC.Models.Base.SexEnumService sexlist = new FS.HISFC.Models.Base.SexEnumService();
                if (flag % 2 == 0)
                {
                    sexobj.ID = HISFC.Models.Base.EnumSex.F.ToString();
                    sexobj.Name = sexlist.GetName(HISFC.Models.Base.EnumSex.F);
                }
                else
                {
                    sexobj.ID = HISFC.Models.Base.EnumSex.M.ToString();
                    sexobj.Name = sexlist.GetName(HISFC.Models.Base.EnumSex.M);
                }
            }
            return sexobj;
        }


       
    }
}
