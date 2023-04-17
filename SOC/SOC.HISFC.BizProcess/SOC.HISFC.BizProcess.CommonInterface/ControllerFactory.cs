using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    /// <summary>
    /// 接口控制类
    /// </summary>
    public abstract class ControllerFactroy : Controller
    {
        private static ControllerFactroy factory;

        private static object lockHelper = new object();

        private static Dictionary<string, Object> interfaceCache = new Dictionary<string, object>();

        public static ControllerFactroy Instance
        {
            get
            {
                return CreateFactory();
            }
        }

        public static ControllerFactroy CreateFactory()
        {
            if (factory == null)
            {
                lock (lockHelper)
                {
                    factory = factory ?? FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ControllerFactroy), typeof(ControllerFactroy)) as ControllerFactroy;

                    if (factory == null)
                    {
                        factory = factory ?? new DefaultControllerFactroy();
                    }
                }
            }
            return factory;
        }

        /// <summary>
        /// 通用创建接口方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soureType"></param>
        /// <param name="defaultType"></param>
        /// <returns></returns>
        public T CreateInferface<T>(Type soureType, T defaultType)
        {
            return this.CreateInferface<T>(soureType, defaultType, -1);
        }

        /// <summary>
        /// 通用创建接口方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soureType"></param>
        /// <param name="index"></param>
        /// <param name="defaultType"></param>
        /// <returns></returns>
        public virtual T CreateInferface<T>(Type soureType, T defaultType,int index)
        {
            T factory = default(T);
            object o=null;
            if (interfaceCache.ContainsKey(typeof(T).FullName + soureType.FullName))
            {
                if (interfaceCache[typeof(T).FullName + soureType.FullName] is Control)
                {
                    if (((Control)interfaceCache[typeof(T).FullName + soureType.FullName]).IsDisposed)
                    {
                        o = null;
                    }
                }
                else
                {
                    o = interfaceCache[typeof(T).FullName + soureType.FullName];
                }
            }

            if (o == null)
            {
                if (index == -1)
                {
                    o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(soureType, typeof(T));
                }
                else
                {
                    o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(soureType, typeof(T), index);
                }
                interfaceCache[typeof(T).FullName + soureType.FullName] = o;
            }

            if (o is T)
            {
                factory = (T)o;
            }

            if (factory == null)
            {
                factory = defaultType;
            }


            return factory;
        }

    }

    public class DefaultControllerFactroy : ControllerFactroy
    {
        
    }

}
