using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    public class Delegate
    {      
        /// <summary>
        /// 过滤文本发生变化
        /// </summary>
        public delegate void FilterTextChangeHander();

        /// <summary>
        /// 项目选择发生变化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public delegate void ItemSelectedChange<T>(T t);
        
        /// <summary>
        ///  项目选择发生变化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="t"></param>
        /// <param name="e"></param>
        public delegate void ItemSelectedChange<T, E>(T t, E e);
    }
}
