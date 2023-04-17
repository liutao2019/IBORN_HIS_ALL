using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;
using System.Collections;
using System.Data;
using System.Drawing;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface
{
    /// <summary>
    /// 通用报表接口
    /// </summary>
    public class ICommonReportController : FS.SOC.HISFC.BizProcess.CommonInterface.ControllerFactroy
    {
        /// <summary>
        /// 过滤时发生
        /// </summary>
        /// <param name="filterStr"></param>
        public delegate void FilterHandler(string filterStr);

        /// <summary>
        /// 行选择时发生
        /// </summary>
        /// <param name="row"></param>
        public delegate void SelectRowHanlder(int row);

        /// <summary>
        /// 单元格数据选择时
        /// </summary>
        /// <returns></returns>
        public delegate void SelectCellHanlder(Object values);

        public static string XmlSerialization(object obj)
        {
            XElement xelement = GetXElement(obj, obj, null);
            return xelement.ToString(SaveOptions.DisableFormatting);
        }

        private static XElement GetXElement(Object childObj, Object mainObj, PropertyInfo pInfo)
        {
            if (childObj == null)
            {
                return null;
            }
            else
            {
                ArrayList listMain = new ArrayList();
                listMain.Add(new XAttribute("Type", childObj.GetType().FullName + "," + (childObj.GetType().Assembly.GlobalAssemblyCache ? childObj.GetType().Assembly.ToString() : childObj.GetType().Assembly.ManifestModule.Name.Replace(".dll", ""))));
                if (childObj is IDictionary)
                {
                    foreach (DictionaryEntry de in (IDictionary)childObj)
                    {
                        Type t = de.Value.GetType();
                        if (t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                        {
                            listMain.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), de.Value));
                        }
                        else
                        {

                            listMain.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), GetXElement(de.Value, de.Value, null)));
                        }
                    }
                    return new XElement("Properties", listMain.ToArray());
                }
                else if (childObj is ICollection)
                {
                    IEnumerator itor = ((ICollection)childObj).GetEnumerator();
                    while (itor.MoveNext())
                    {
                        if (itor.Current.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                        {
                            listMain.Add(new XElement(itor.GetType().Name, itor.Current));
                        }
                        else
                        {
                            listMain.Add(GetXElement(itor.Current, itor.Current, null));
                        }
                    }

                    return new XElement("Librarys", listMain.ToArray());
                }
                else
                {
                    PropertyInfo[] properties = childObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
                    if (properties != null && properties.Length != 0)
                    {
                        foreach (System.Reflection.PropertyInfo p in properties)
                        {
                            if (!p.CanRead || !p.CanWrite)
                            {
                                continue;
                            }
                            if (p.PropertyType.IsValueType)
                            {
                                if (p.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length > 0)
                                {
                                    listMain.Add(GetXElement(p.GetValue(childObj, null), childObj, p));
                                }
                                else
                                {
                                    listMain.Add(new XElement(p.Name, p.GetValue(childObj, null)));
                                }
                            }
                            else if (p.PropertyType.IsSealed)
                            {
                                Object o = p.GetValue(childObj, null);
                                if (p.PropertyType == typeof(System.Drawing.Font))
                                {
                                    System.Drawing.FontConverter fc = new System.Drawing.FontConverter();
                                    o = fc.ConvertToInvariantString((Font)o);
                                }
                                else
                                {
                                    if (Nullable.Equals(null, o))
                                    {
                                        o = "";
                                    }
                                }
                                listMain.Add(new XElement(p.Name, o));
                            }
                            else if (p.PropertyType.IsGenericType)
                            {
                                if (p.PropertyType.GetInterface(typeof(IDictionary<Object, Object>).Name) != null)
                                {
                                    IDictionary collection = p.GetValue(childObj, null) as IDictionary;
                                    if (collection != null)
                                    {
                                        List<XElement> list = new List<XElement>();
                                        foreach (DictionaryEntry de in collection)
                                        {
                                            Type t = de.Value.GetType();
                                            if (t.IsSealed || t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                                            {
                                                list.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), de.Value));
                                            }
                                            else
                                            {
                                                list.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), GetXElement(de.Value, de.Value, p)));
                                            }
                                        }
                                        listMain.Add(new XElement(p.Name, list.ToArray()));
                                    }
                                    else
                                    {
                                        listMain.Add(new XElement(p.Name, null));
                                    }
                                }
                                else if (p.PropertyType.GetInterface(typeof(ICollection<Object>).Name) != null)
                                {
                                    ICollection collection = p.GetValue(childObj, null) as ICollection;
                                    if (collection != null)
                                    {
                                        List<XElement> list = new List<XElement>();
                                        foreach (Object o in collection)
                                        {
                                            Type t = o.GetType();
                                            if (t.IsSealed || t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                                            {
                                                list.Add(new XElement(t.Name, o));
                                            }
                                            else
                                            {
                                                list.Add(GetXElement(o, o, p));
                                            }
                                        }
                                        listMain.Add(new XElement(p.Name, list.ToArray()));
                                    }
                                    else
                                    {
                                        listMain.Add(new XElement(p.Name, null));
                                    }
                                }
                            }
                            else if (p.PropertyType.GetInterface(typeof(System.Collections.IDictionary).Name) != null)
                            {
                                IDictionary collection = p.GetValue(childObj, null) as IDictionary;
                                if (collection != null)
                                {
                                    List<XElement> list = new List<XElement>();
                                    foreach (DictionaryEntry de in collection)
                                    {
                                        Type t = de.Value.GetType();
                                        if (t.IsSealed || t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                                        {
                                            list.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), de.Value));
                                        }
                                        else
                                        {
                                            list.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache ? t.Assembly.ToString() : t.Assembly.ManifestModule.Name.Replace(".dll", ""))), new XAttribute("Name", de.Key.ToString()), GetXElement(de.Value, de.Value, p)));
                                        }
                                    }
                                    listMain.Add(new XElement(p.Name, list.ToArray()));
                                }
                                else
                                {
                                    listMain.Add(new XElement(p.Name, null));
                                }
                            }
                            else if (p.PropertyType.GetInterface(typeof(System.Collections.ICollection).Name) != null)
                            {
                                ICollection collection = p.GetValue(childObj, null) as ICollection;
                                if (collection != null)
                                {
                                    List<XElement> list = new List<XElement>();
                                    foreach (Object o in collection)
                                    {
                                        Type t = o.GetType();
                                        if (t.IsSealed || t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                                        {
                                            list.Add(new XElement(t.Name, o));
                                        }
                                        else
                                        {
                                            list.Add(GetXElement(o, o, p));
                                        }
                                    }
                                    listMain.Add(new XElement(p.Name, list.ToArray()));
                                }
                                else
                                {
                                    listMain.Add(new XElement(p.Name, null));
                                }
                            }
                            else if (p.PropertyType.IsArray)
                            {
                                Object[] collection = p.GetValue(childObj, null) as Object[];
                                if (collection != null)
                                {
                                    List<XElement> list = new List<XElement>();
                                    foreach (Object o in collection)
                                    {
                                        Type t = o.GetType();
                                        if (t.IsSealed || t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
                                        {
                                            list.Add(new XElement(t.Name, o));
                                        }
                                        else
                                        {
                                            list.Add(GetXElement(o, mainObj, p));
                                        }
                                    }
                                    listMain.Add(new XElement(p.Name, list.ToArray()));
                                }
                                else
                                {
                                    listMain.Add(new XElement(p.Name, null));
                                }
                            }
                            else
                            {
                                listMain.Add(GetXElement(p.GetValue(childObj, null), p.GetValue(childObj, null), p));
                            }
                        }

                    }
                    return new XElement(pInfo == null ? childObj.GetType().Name : pInfo.Name, listMain.ToArray());
                }

            }
        }

        public static T XmlDeSerialization<T>(string xml)
        {
            XElement xElement = XElement.Load(new System.IO.StringReader(xml), LoadOptions.PreserveWhitespace);

            return (T)GetObject(xElement);
        }

        private static Object GetObject(XElement xElement)
        {
            if (xElement == null)
            {
                return null;
            }

            Type type = Type.GetType(xElement.Attribute("Type").Value);
            if (type.IsValueType == false && type.IsSealed)
            {
                return xElement.Value;
            }
            else
            {
                Object o = Activator.CreateInstance(type);
                if (o != null)
                {

                    foreach (PropertyInfo p in o.GetType().GetProperties())
                    {
                        if (!p.CanRead || !p.CanWrite)
                        {
                            continue;
                        }

                        if (p.PropertyType.IsEnum)
                        {
                            if (xElement.Element(p.Name) != null)
                            {
                                p.SetValue(o, System.Enum.Parse(p.PropertyType, xElement.Element(p.Name).Value), null);
                            }

                        }
                        else if (p.PropertyType.IsValueType)
                        {
                            if (p.PropertyType.GetProperties().Length > 0)
                            {
                                p.SetValue(o, GetObject(xElement.Element(p.Name)), null);
                            }
                            else
                            {
                                if (xElement.Element(p.Name) != null)
                                {
                                    p.SetValue(o, Convert.ChangeType(xElement.Element(p.Name).Value, p.PropertyType), null);
                                }
                            }
                        }
                        else if (p.PropertyType.IsSealed)
                        {
                            if (xElement.Element(p.Name) != null)
                            {
                                if (p.PropertyType==typeof(  System.Drawing.Font))
                                {
                                    System.Drawing.FontConverter fc = new System.Drawing.FontConverter();
                                    System.Drawing.Font f = (System.Drawing.Font)fc.ConvertFromString(xElement.Element(p.Name).Value);
                                    p.SetValue(o, f,null);
                                }
                                else
                                {
                                    p.SetValue(o, Convert.ChangeType(xElement.Element(p.Name).Value, p.PropertyType), null);
                                }
                            }
                        }
                        else if (p.PropertyType.IsGenericType)
                        {
                            //调用一个方法
                            System.Reflection.MethodInfo m = o.GetType().GetMethod("SetGenericTypeValue");

                            if (p.PropertyType.GetInterface(typeof(IDictionary<Object, Object>).Name) != null)
                            {
                                if (xElement.Element(p.Name) != null)
                                {
                                    var objects = from obj in xElement.Element(p.Name).Elements("Property") select obj;
                                    if (objects != null)
                                    {
                                        IDictionary dictionary = objects.ToDictionary(k => k.Attribute("Name").Value,
                                            v => GetObject(v));
                                        if (m != null)
                                        {
                                            m.Invoke(o, new object[] { o, dictionary, p });
                                        }
                                        //p.SetValue(o, dictionary, null);
                                    }
                                }
                            }
                            else if (p.PropertyType.GetInterface(typeof(ICollection<Object>).Name) != null)
                            {
                                if (xElement.Element(p.Name) != null)
                                {
                                    var objects = from obj in xElement.Element(p.Name).Elements() select obj;
                                    if (objects != null)
                                    {
                                        ICollection dictionary = objects.ToDictionary(k => k.GetHashCode(),
                                            v => GetObject(v)
                                            ).Values.ToList();

                                        if (m != null)
                                        {
                                            m.Invoke(o, new object[] { o, dictionary, p });
                                        }
                                        //p.SetValue(o, dictionary, null);
                                    }
                                }
                            }
                        }
                        else if (p.PropertyType.GetInterface(typeof(IDictionary).Name) != null)
                        {
                            if (xElement.Element(p.Name) != null)
                            {
                                var objects = from obj in xElement.Element(p.Name).Elements("Property") select obj;
                                if (objects != null)
                                {
                                    IDictionary dictionary = new Hashtable(objects.ToDictionary(k => k.Attribute("Name").Value,
                                        v => GetObject(v)));
                                    p.SetValue(o, dictionary, null);
                                }
                            }
                        }
                        else if (p.PropertyType.GetInterface(typeof(ICollection).Name) != null)
                        {
                            if (xElement.Element(p.Name) != null)
                            {
                                var objects = from obj in xElement.Element(p.Name).Elements() select obj;
                                if (objects != null)
                                {
                                    ICollection dictionary = new ArrayList(objects.ToDictionary(k => k.GetHashCode(),
                                         v => GetObject(v)).Values.ToList());
                                    p.SetValue(o, dictionary, null);
                                }
                            }
                        }
                        else if (p.PropertyType.IsArray)
                        {
                            if (xElement.Element(p.Name) != null)
                            {

                                var objects = from obj in xElement.Element(p.Name).Elements() select obj;
                                if (objects != null)
                                {
                                    object[] dictionary = objects.ToDictionary(k => k.GetHashCode(),
                                       v => GetObject(v)).Values.ToArray();
                                    p.SetValue(o, dictionary, null);
                                }
                            }
                        }
                        else
                        {
                            p.SetValue(o, GetObject(xElement.Element(p.Name)), null);
                        }
                    }
                }

                return o;
            }
        }
    }
}
