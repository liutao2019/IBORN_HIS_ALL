using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Registration.GuangZhou.Zdly
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

        /// <summary>
        /// 科室地点
        /// </summary>
        /// <param name="statCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static string GetDeptMemo(string statCode, string deptCode)
        {
            string strResult = "";
            string sql = @"select a.mark from com_deptstat a where a.stat_code='{0}' and a.dept_code='{1}'";

            try
            {
                FS.FrameWork.Management.DataBaseManger databaseMgr = new FS.FrameWork.Management.DataBaseManger();
                sql = string.Format(sql, statCode, deptCode);
                strResult = databaseMgr.ExecSqlReturnOne(sql, "");

            }
            catch
            {

            }


            return strResult;
        }

        public static string GetHospitalName()
        {
            try
            {
                XElement e = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "HISDefaultValue.xml");
                return e.Elements().Where(t => t.Attributes().Where(a => a.Name == "ID" && a.Value == "HospitalName").Count() > 0)
                    .Elements("Function").FirstOrDefault().Element("Value").Value;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }

        }
    }
}
