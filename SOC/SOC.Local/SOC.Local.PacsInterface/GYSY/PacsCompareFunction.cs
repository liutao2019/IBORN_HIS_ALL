using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.PacsInterface.GYSY
{
    /// <summary>
    /// 获取pacs对照信息功能类
    /// </summary>
    public class PacsCompareFunction
    {

        private static FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        ///获取PACS项目
        /// [参数: ref System.Data.DataSet dsHISItem - 项目数据集]
        /// [返回: int,1-成功,-1-失败]
        /// </summary>
        /// <param name="dsPACSItem">项目数据集</param>
        /// <returns>1-成功,-1-失败</returns>
        public static int GetPacsItemList(ref System.Data.DataSet dsPacsItem)
        {
            int intReturn = 0;

            string sql = string.Empty;
            //
            // 获取SQL语句
            //

            intReturn = dataManager.Sql.GetSql("Management.Order.PacsCompare.GetPacsItemList", ref sql);
            if (intReturn == -1)
            {
                dataManager.Err = "获取HIS项目获取SQL语句失败(" + dataManager.Err + ")";
                return -1;
            }

            //
            // 执行SQL语句
            //
            intReturn = dataManager.ExecQuery(sql, ref dsPacsItem);
            if (intReturn == -1)
            {
                dataManager.Err = "获取HIS项目失败(" + dataManager.Err + ")";
                return -1;
            }
            //
            // 成功返回
            //
            return 1;
        }



        /// <summary>
        /// 在xml中取医院logo赋予picturebox
        /// </summary>
        /// <param name="xmlpath">xml路径（绝对）  PS：从根目录开始</param>
        /// <param name="root">xml根节点</param>
        /// <param name="secondNode">要查找的目标节点</param>
        /// <param name="erro">错误信息</param>
        public static string GetHospitalLogo(string xmlpath, string root, string secondNode, string erro)
        {
            
            xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + xmlpath;
            return FS.SOC.Public.XML.SettingFile.ReadSetting(xmlpath, root, secondNode, erro);

        }

        /// <summary>
        /// 获得年龄
        /// </summary>
        /// <param name="dtBirthday"></param>
        /// <returns></returns>
        public static string GetAge(DateTime dtBirthday)
        {
            FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

            DateTime systime = orderManager.GetDateTimeFromSysDateTime();
            orderManager = null;
            TimeSpan span = new TimeSpan(systime.Ticks - dtBirthday.Ticks);
            string strAge = "";
            if (span.Days / 365 <= 0)
            {
                if (span.Days / 30 <= 0)
                {
                    strAge = span.Days.ToString() + "天";
                }
                else
                {
                    strAge = System.Convert.ToString(span.Days / 30) + "月";
                }
            }
            else
            {
                strAge = System.Convert.ToString(span.Days / 365) + "岁";
                if (span.Days / 365 < 5)
                {
                    int diff = span.Days - (span.Days / 365) * 365;
                    if (diff > 30)
                    {
                        strAge = strAge + System.Convert.ToString(span.Days / 30) + "月";
                    }
                    else
                    {
                        strAge = strAge + System.Convert.ToString(diff) + "天";
                    }
                }
            }
            return strAge;
        }

    }
}
