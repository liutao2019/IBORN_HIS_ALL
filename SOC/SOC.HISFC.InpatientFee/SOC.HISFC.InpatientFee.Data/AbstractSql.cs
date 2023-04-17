using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data
{
    /// <summary>
    /// [功能描述: sql语句数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractSql<T>
    {
        private static T t = default(T);

        static AbstractSql()
        {
            getCurrent();
        }

        /// <summary>
        /// 获取当前实例
        /// </summary>
        /// <returns></returns>
        private static void getCurrent()
        {
            System.Reflection.Assembly assembly = typeof(T).Assembly;
            Type[] types = assembly.GetTypes();
            if (types != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    if (types[i].BaseType.FullName.Equals(typeof(T).FullName))
                    {
                        Object[] objs = types[i].GetCustomAttributes(typeof(DataBaseAttribute), false);
                        if (objs != null && objs.Length > 0)
                        {
                            for (int j = 0; j < objs.Length; j++)
                            {
                                if (((objs[j] as DataBaseAttribute).DbType == FS.FrameWork.Management.Connection.DBType))
                                {
                                    t = (T)assembly.CreateInstance(types[i].FullName, false);
                                    return;
                                }
                            }

                            //如果没有默认的实现对象，则抛出异常
                            throw new Exception(assembly.FullName + "没有找到" + typeof(T).FullName + "对应的实现类");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前实例
        /// </summary>
        public static T Current
        {
            get
            {
                return t;
            }
        }

        /// <summary>
        /// 查询所有的sql文
        /// </summary>
        public abstract string SelectAll
        {
            get;
        }

        /// <summary>
        /// 插入sql文
        /// </summary>
        public abstract string Insert
        {
            get;
        }

        /// <summary>
        /// 更新sql文
        /// </summary>
        public virtual string Update
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 自动生成ID的sql文
        /// </summary>
        public virtual string AutoID
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 删除的sql文
        /// </summary>
        public virtual string Delete
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 主键的sql文
        /// </summary>
        public virtual string WhereByKey
        {
            get
            {
                return string.Empty;
            }
        }

    }
}
