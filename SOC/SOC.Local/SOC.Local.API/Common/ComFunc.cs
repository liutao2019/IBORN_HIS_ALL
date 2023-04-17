using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.API.Common
{
    public class ComFunc
    {

        /// <summary>
        /// 在xml中取医院logo赋予picturebox
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

    /// <summary>
    /// 大小比较器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CommonComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, Int32> _comparer;
        public CommonComparer(Func<T, T, Int32> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            _comparer = comparer;
        }
        public Int32 Compare(T x, T y)
        {
            return _comparer(x, y);
        }
    }

    /// <summary>
    /// 相等比较器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CommonEqualsComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;
        public CommonEqualsComparer(Func<T, T, bool> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");
            _comparer = comparer;
        }
        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }
        public int GetHashCode(T obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }
}
