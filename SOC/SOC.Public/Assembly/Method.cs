using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.Assembly
{
    /// <summary>
    /// [功能描述: 程序集相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-6]<br></br>
    /// </summary>
    public class Method
    {
        /// <summary>
        /// 反射调用指定程序集中指定类的某个方法
        /// </summary>
        /// <param name="methodInfo">方法相关信息：程序集，类名称，方法名称，参数等</param>
        /// <param name="listPropert">指定类需要设置的属性信息</param>
        /// <returns>方法的返回值</returns>
        public static object Invoke(FS.SOC.Public.Assembly.Models.SOCMethod methodInfo, List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert)
        {
            //反射dll
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(methodInfo.Class.Dll.Name);
            
            //获取dll中的class
            Type tClass = assembly.GetType(methodInfo.Class.FullName);
            
            //在class中查找方法，可能有重载的，需要参数类型处理
            System.Reflection.MethodInfo method = tClass.GetMethod(methodInfo.Name, methodInfo.ParameterTypes);
            
            //创建实例
            object instance = Activator.CreateInstance(tClass);

            //给实例的属性赋值
            if (listPropert != null)
            {
                foreach (FS.SOC.Public.Assembly.Models.SOCPropert propert in listPropert)
                {
                    //抽象化当前属性
                    System.ComponentModel.PropertyDescriptor propertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(instance)[propert.Name];

                    //字符串表示的属性值转换成属性相对应的类型
                    object value = propertyDescriptor.Converter.ConvertFromInvariantString(propert.Value);

                    //设置属性的值
                    propertyDescriptor.SetValue(instance, value);
                }
            }

            //反射调用方法
            object returnValue = method.Invoke(instance, methodInfo.Parameters);

            return returnValue;
        }

        /// <summary>
        /// 反射调用指定程序集中指定类的某个方法
        /// </summary>
        /// <param name="dllName">程序集，需要后缀名dll</param>
        /// <param name="classFullName">类全每次</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameterTypes">参数类型</param>
        /// <param name="parameters">参数值</param>
        /// <param name="listPropert">类实例需要设置的属性值</param>
        /// <returns>方法的返回值</returns>
        public static object Invoke(string dllName, string classFullName, string methodName, Type[] parameterTypes, object[] parameters, List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert)
        {
            //反射dll
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllName);

            //获取dll中的class
            Type tClass = assembly.GetType(classFullName);

            //在class中查找方法，可能有重载的，需要参数类型处理
            System.Reflection.MethodInfo method = tClass.GetMethod(methodName, parameterTypes);

            //创建实例
            object instance = Activator.CreateInstance(tClass);

            //给实例的属性赋值
            if (listPropert != null)
            {
                foreach (FS.SOC.Public.Assembly.Models.SOCPropert propert in listPropert)
                {
                    //抽象化当前属性
                    System.ComponentModel.PropertyDescriptor propertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(instance)[propert.Name];

                    //字符串表示的属性值转换成属性相对应的类型
                    object value = propertyDescriptor.Converter.ConvertFromInvariantString(propert.Value);

                    //设置属性的值
                    propertyDescriptor.SetValue(instance, value);
                }
            }

            //反射调用方法
            object returnValue = method.Invoke(instance, parameters);

            return returnValue;
        }

        /// <summary>
        /// 反射调用指定程序集中指定类的某个方法
        /// </summary>
        /// <param name="dllName">程序集，需要后缀名dll</param>
        /// <param name="classFullName">类全每次</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="parameterTypes">参数类型</param>
        /// <param name="parameters">参数值</param>
        /// <returns>方法的返回值</returns>
        public static object Invoke(string dllName, string classFullName, string methodName, Type[] parameterTypes, object[] parameters)
        {
            return Invoke(dllName, classFullName, methodName, parameterTypes, parameters, null);
        }

        /// <summary>
        /// 反射调用指定程序集中指定类的某个方法
        /// </summary>
        /// <param name="dllName">程序集，需要后缀名dll</param>
        /// <param name="classFullName">类全每次</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>方法的返回值</returns>
        public static object Invoke(string dllName, string classFullName, string methodName)
        {
            //反射dll
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllName);

            //获取dll中的class
            Type tClass = assembly.GetType(classFullName);

            //在class中查找方法，可能有重载的，需要参数类型处理
            System.Reflection.MethodInfo[] mis = tClass.GetMethods();
            foreach (System.Reflection.MethodInfo method in mis)
            {
                if (method.Name == methodName && method.GetParameters().Length == 0)
                {
                    //创建实例
                    object instance = Activator.CreateInstance(tClass);

                    //反射调用方法
                    object returnValue = method.Invoke(instance, null);

                    return returnValue;
                }
            }
            return null;
        }
    }
}
