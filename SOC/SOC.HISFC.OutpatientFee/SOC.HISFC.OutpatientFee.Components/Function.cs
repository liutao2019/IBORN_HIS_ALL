using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FS.SOC.HISFC.OutpatientFee.Components
{
    public class Function
    {
        #region 字符串替换

        public static String ReplaceValues(String template, Dictionary<String, Object> map)
        {
            if (HasReplaceableValues(template))
            {
                StringBuilder tempSb = new StringBuilder(template);
                string tempKey = string.Empty;
                foreach (string key in map.Keys)
                {
                    tempKey = string.Concat('&', key);
                    if (template.Contains(tempKey))
                    {
                        if (map[key] != null)
                        {
                            tempSb.Replace(tempKey, map[key].ToString());
                        }
                    }
                }

                return tempSb.ToString();
            }
            else
            {
                return template;
            }
        }

        public static bool HasReplaceableValues(String str)
        {
            return ((str != null) && (str.IndexOf("&") > -1));
        }

        public static bool HasSelect(string str)
        {
            return ((str != null) && (str.ToUpper().IndexOf("SELECT") > -1));
        }

        #endregion

        #region 权限相关

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        #endregion

        #region 业务对象

        /// <summary>
        /// 日结管理类
        /// </summary>
        private static FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport dayReportBiz = null;
        /// <summary>
        /// 日结管理类
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport GetDayReportBizProcess()
        {
            if (dayReportBiz == null)
            {
                dayReportBiz = new FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport();
            }
            return dayReportBiz;
        }

        /// <summary>
        /// 诊断管理类
        /// </summary>
        private static FS.SOC.HISFC.OutpatientFee.BizProcess.Diagnose diagnoseBiz = null;
        /// <summary>
        /// 诊断管理类
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.OutpatientFee.BizProcess.Diagnose GetDiagnoseBizProcess()
        {
            if (diagnoseBiz == null)
            {
                diagnoseBiz = new FS.SOC.HISFC.OutpatientFee.BizProcess.Diagnose();
            }
            return diagnoseBiz;
        }

        #endregion

        #region 图片

        /// <summary>
        /// 获取患者图片（大 40*40）
        /// </summary>
        /// <returns></returns>
        public static ImageList PatientLargeImageList()
        {
            ImageList list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_男));
            list.Images.Add("1", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_女));

            list.Images.Add("2", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_男));
            list.Images.Add("3", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_女));

            list.Images.Add("4", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_男));
            list.Images.Add("5", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_女));

            list.Images.Add("6", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_男));
            list.Images.Add("7", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_女));

            list.Images.Add("8", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G顾客));
            list.ImageSize = new Size(40, 40);

            return list;

            list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_男));
            list.Images.Add("1", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_女));

            list.Images.Add("2", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_男));
            list.Images.Add("3", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_女));

            list.Images.Add("4", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_男));
            list.Images.Add("5", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_女));

            list.Images.Add("6", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_男));
            list.Images.Add("7", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_女));

            list.Images.Add("8", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G顾客));
            list.ImageSize = new Size(20, 20);

            return list;

        }

        /// <summary>
        /// 获取患者图片（小 20*20）
        /// </summary>
        /// <returns></returns>
        public static ImageList PatientSmallImageList()
        {

            ImageList list = new ImageList();
            list.Images.Add("0", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_男));
            list.Images.Add("1", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_成人_女));

            list.Images.Add("2", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_男));
            list.Images.Add("3", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_儿童_女));

            list.Images.Add("4", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_男));
            list.Images.Add("5", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_老人_女));

            list.Images.Add("6", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_男));
            list.Images.Add("7", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R人物_婴儿_女));

            list.Images.Add("8", FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G顾客));
            list.ImageSize = new Size(20, 20);
            return list;
        }

        #endregion
    }
}
