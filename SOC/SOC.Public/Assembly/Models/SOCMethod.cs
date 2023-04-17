using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.Assembly.Models
{
    /// <summary>
    /// [功能描述: 程序集相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-6]<br></br>
    /// </summary>
    public class SOCMethod
    {
        
        private string name = "";
        private object[] parameters = null;
        private Type[] parameterTypes = null;

        private SOCClass curClass = new SOCClass();


        /// <summary>
        /// 方法属于的类信息
        /// </summary>
        public SOCClass Class
        {
            get { return curClass; }
            set { curClass = value; }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type[] ParameterTypes
        {
            get { return parameterTypes; }
            set { parameterTypes = value; }
        }

        /// <summary>
        /// 调用方法的参数
        /// </summary>
        public object[] Parameters
        {
            get 
            {
                return this.parameters; 
            }
            set 
            {
                this.parameters = value; 
            }
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name
        {
            get 
            {
                return this.name; 
            }
            set
            {
                this.name = value;
            }
        }

        
    }
}
