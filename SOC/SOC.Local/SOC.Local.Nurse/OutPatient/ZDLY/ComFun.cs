using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Nurse.OutPatient.ZDLY
{
    public class ComFun
    {
        /// <summary>
        /// 在xml中取医院logo路径赋予picturebox
        /// </summary>
        /// <param name="xmlpath">xml路径（绝对）  PS：从根目录开始</param>
        /// <param name="root">xml根节点</param>
        /// <param name="secondNode">要查找的目标节点</param>
        /// <param name="erro">错误信息</param>
        public string GetHospitalLogo(string xmlpath, string root, string secondNode, string erro)
        {
            xmlpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + xmlpath;
            return SOC.Public.XML.SettingFile.ReadSetting(xmlpath, root, secondNode, erro);
        }
    }
}
