using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Account.GuangZhou.Zdly
{
    /// <summary>
    /// Zdly的功能函数
    /// </summary>
    public class Function
    {
        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();

        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
               ArrayList al= CommonController.Instance.QueryConstant("YkDept");
               if (al != null)
               {
                   foreach (FS.FrameWork.Models.NeuObject obj in al)
                   {
                       dictionaryYKDept[obj.ID] = obj.Name;
                   }
               }
            }

            return dictionaryYKDept.ContainsKey(dept);
        }


        /// <summary>
        /// 根据身份证号获取性别
        /// </summary>
        /// <param name="idNO">身份证号</param>
        /// <returns></returns>
        public static FS.FrameWork.Models.NeuObject GetSexFromIdNO(string idNO, ref string err)
        {
            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return null;
            }

            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }

            int flag = FS.FrameWork.Function.NConvert.ToInt32((idNO.Substring(16, 1)));
            FrameWork.Models.NeuObject sexobj = new FS.FrameWork.Models.NeuObject();
            SexEnumService sexlist = new FS.HISFC.Models.Base.SexEnumService();
            if (flag % 2 == 0)
            {
                sexobj.ID = EnumSex.F.ToString();
                sexobj.Name = sexlist.GetName(EnumSex.F);
            }
            else
            {
                sexobj.ID = EnumSex.M.ToString();
                sexobj.Name = sexlist.GetName(EnumSex.M);
            }
            return sexobj;
        }

        /// <summary>
        /// 根据身份证号获取生日
        /// </summary>
        /// <param name="idNO">身份证号</param>
        /// <returns></returns>
        public static string GetBirthDayFromIdNO(string idNO, ref string err)
        {

            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return "-1";
            }
            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            string datestr = idNO.Substring(6, 8);
            string year = datestr.Substring(0, 4);
            string month = datestr.Substring(4, 2);
            string day = datestr.Substring(6, 2);
            datestr = year + "-" + month + "-" + day;
            return datestr;
        }

        public static string GetMCardNoByCardNo(string CardNo)
        {
            string sql = string.Format(@"select t.markno from fin_opb_accountcard t where t.card_no='{0}' and t.type='1' and t.state='1'", CardNo);
            FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
            System.Data.DataSet ds = new System.Data.DataSet();
            outpatientManager.ExecQuery(sql, ref ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            else
            {
                return "";
            }
            
        }
        private static FS.HISFC.BizProcess.Interface.Account.IOperCard IOperCard = null;
        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <returns></returns>
        private static FS.HISFC.BizProcess.Interface.Account.IOperCard GetIOperCard()
        {
            if (IOperCard == null)
            {
                IOperCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<FS.HISFC.BizProcess.Interface.Account.IOperCard>(typeof(Function));
            }
            return IOperCard;
        }

        /// <summary>
        /// 读取病历号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 读取物理卡号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
    }
}
