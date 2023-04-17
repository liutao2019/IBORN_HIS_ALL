using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Xml.Linq;
using System.Reflection;
using System.Xml;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    [Serializable]
    public enum EnumDataSourceType
    {
        Sql,
        Custom,
        Dictionary,
        DepartmentType,
        EmployeeType,
        CurrentOper,
        CurrentDept
    }

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

        public static string XmlSerialization(object obj)
        {
            XElement xelement = GetXElement(obj, obj,null);
            return xelement.ToString( SaveOptions.DisableFormatting);
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
                            listMain.Add(new XElement("Property", new XAttribute("Type", t.FullName + "," + (t.Assembly.GlobalAssemblyCache?t.Assembly.ToString():t.Assembly.ManifestModule.Name.Replace(".dll",""))), new XAttribute("Name", de.Key.ToString()), de.Value));
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
                            if (!p.CanRead||!p.CanWrite)
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
                                if (Nullable.Equals(null, o))
                                {
                                    o = "";
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
                                            if (t.IsSealed||t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Length == 0)
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
            if (type.IsValueType==false&&type.IsSealed)
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
                                p.SetValue(o, Enum.Parse(p.PropertyType, xElement.Element(p.Name).Value), null);
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
                        else if(p.PropertyType.IsSealed)
                        {
                            if (xElement.Element(p.Name) != null)
                            {
                                p.SetValue(o, Convert.ChangeType(xElement.Element(p.Name).Value, p.PropertyType), null);
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

        public interface ITopQueryCondition
        {
            /// <summary>
            /// 初始化
            /// </summary>
            /// <returns></returns>
            int Init();

            /// <summary>
            /// 增加控件
            /// </summary>
            /// <param name="list"></param>
            void AddControls(List<CommonReportQueryInfo> list);

            /// <summary>
            /// 获取数据
            /// </summary>
            /// <param name="common"></param>
            /// <returns></returns>
            Object GetValue(CommonReportQueryInfo common);

            /// <summary>
            /// 过滤事件
            /// </summary>
            event FilterHandler OnFilterHandler;
        }

        public interface ILeftQueryCondition : ITopQueryCondition
        {

        }

        public interface IMainReportForm
        {
            /// <summary>
            /// 初始化
            /// </summary>
            /// <returns></returns>
            int Init();

            /// <summary>
            /// DataWindow对象
            /// </summary>
            string DataWindowObject
            {
                get;
                set;
            }

            /// <summary>
            /// DataWindow文件库
            /// </summary>
            string LibraryList
            {
                get;
                set;
            }

            /// <summary>
            /// 查询
            /// </summary>
            /// <param name="objects"></param>
            int Retrieve(params object[] objects);

            /// <summary>
            /// 查询
            /// </summary>
            /// <param name="dt"></param>
            /// <returns></returns>
            int Retrieve(System.Data.DataTable dt);

            /// <summary>
            /// 根据变量名称代替字符串中的值
            /// </summary>
            /// <param name="map"></param>
            /// <returns></returns>
            int Retrieve(Dictionary<String, Object> map);

            /// <summary>
            /// 导出
            /// </summary>
            /// <returns></returns>
            int Export();

            /// <summary>
            /// 打印
            /// </summary>
            /// <returns></returns>
            int Print();

            /// <summary>
            /// 打印预览
            /// </summary>
            /// <returns></returns>
            int PrintPreview(bool isPreview);

            /// <summary>
            /// 纸张设置
            /// </summary>
            int PaperSize
            {
                get;
                set;
            }

            /// <summary>
            /// 自定义纸张长度
            /// </summary>
            int CustomPageLength
            {
                get;
                set;
            }

            /// <summary>
            /// 自定义纸张宽度
            /// </summary>
            int CustomPageWidth
            {
                get;
                set;
            }

            /// <summary>
            /// 打印机名称
            /// </summary>
            string PrintName
            {
                get;
                set;
            }

            /// <summary>
            /// 左边距
            /// </summary>
            int MarginLeft
            {
                get;
                set;
            }

            /// <summary>
            /// 右边距
            /// </summary>
            int MarginRight
            {
                get;
                set;
            }

            /// <summary>
            /// 上边距
            /// </summary>
            int MarginTop
            {
                get;
                set;
            }

            /// <summary>
            /// 下边距
            /// </summary>
            int MarginBottom
            {
                get;
                set;
            }

            /// <summary>
            /// 过滤
            /// </summary>
            void OnFilter(string filter);

            /// <summary>
            /// 选择行事件
            /// </summary>
            event SelectRowHanlder OnSelectRowHandler;

            /// <summary>
            /// 根据行和列名称获取相应的值
            /// </summary>
            /// <param name="row"></param>
            /// <param name="columnName"></param>
            /// <returns></returns>
            string GetItemString(int row, string columnName);

            void Reset();

            int RowCount
            {
                get;
            }
        }

        public interface ISettingReportForm
        {
            void SetValue(ReportQueryInfo value);

            ReportQueryInfo GetValue();
        }
    }

    [Serializable]
    public class CommonReportQueryInfo
    {
        private int index = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的Index值")]
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        private string name = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的内置名称，不允许与其他控件相同")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string text = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("控件的显示名称")]
        public string Text
        {

            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }


        private bool isAddText = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("A.基本信息")]
        [System.ComponentModel.Description("是否添加显示名称标签")]
        public bool IsAddText
        {
            get
            {
                return isAddText;
            }
            set
            {
                isAddText = value;
            }
        }

        private BaseControlType controlType = new TextBoxType();
        [System.ComponentModel.Category("B.控件信息")]
        [System.ComponentModel.Description("控件设置")]
        public BaseControlType ControlType
        {
            get
            {
                return controlType;
            }
            set
            {
                controlType = value;
            }
        }
    }

    public class ControlTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value is TextBoxType)
            {
                return TypeDescriptor.GetProperties(typeof(TextBoxType), attributes);
            }
            else if (value is FilterComboBox)
            {
                return TypeDescriptor.GetProperties(typeof(FilterComboBox), attributes);
            }
            else if (value is ComboBoxType)
            {
                return TypeDescriptor.GetProperties(typeof(ComboBoxType), attributes);
            }
            else if (value is DateTimeType)
            {
                return TypeDescriptor.GetProperties(typeof(DateTimeType), attributes);
            }
            else if (value is FilterTreeView)
            {
                return TypeDescriptor.GetProperties(typeof(FilterTreeView), attributes);
            }
            else if (value is TreeViewType)
            {
                return TypeDescriptor.GetProperties(typeof(TreeViewType), attributes);
            }
            else if (value is FilterTextBox)
            {
                return TypeDescriptor.GetProperties(typeof(FilterTextBox), attributes);
            }
            else if (value is FS.FrameWork.Models.NeuObject)
            {
                return TypeDescriptor.GetProperties(typeof(FS.FrameWork.Models.NeuObject), attributes);
            }
            else if (value is CustomControl)
            {
                return TypeDescriptor.GetProperties(typeof(CustomControl), attributes);
            }
            else
            {
                return base.GetProperties(context, value, attributes);
            }

        }
    }

    public class ControlUITypeEditor : System.Drawing.Design.UITypeEditor
    {

        System.Windows.Forms.Design.IWindowsFormsEditorService editorService = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                if (editorService != null)
                {
                    System.Windows.Forms.ListBox editorControl = new System.Windows.Forms.ListBox();
                    editorControl.Items.AddRange(new object[] { 
                            new TextBoxType(),
                            new ComboBoxType(),
                            new DateTimeType(),
                            new TreeViewType(),
                            new FilterTextBox(),
                            new FilterComboBox(),
                            new FilterTreeView(),
                            new CustomControl()
                        });
                    editorControl.MouseClick += new System.Windows.Forms.MouseEventHandler(editorControl_MouseDoubleClick);
                    editorService.DropDownControl(editorControl);
                    if (editorControl.SelectedItem != null)
                    {
                        value = editorControl.SelectedItem;
                    }
                    return base.EditValue(context, provider, value);
                }
            }

            return base.EditValue(context, provider, value);

        }

        void editorControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (editorService != null)
            {
                editorService.CloseDropDown();
            }
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
            }

            return base.GetEditStyle(context);
        }
    }

    [Serializable]
    [Editor(typeof(ControlUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [TypeConverter(typeof(ControlTypeConverter))]
    public class BaseControlType
    {
        private bool isLike = false;
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("是否启用查询条件匹配模式，True启用，False不启用")]
        public bool IsLike
        {
            get
            {
                return isLike;
            }
            set
            {
                isLike = value;
            }
        }

        private string likeStr = "%{0}%";
        [System.ComponentModel.DefaultValue("%{0}%")]
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("查询条件启用匹配模式时，使用匹配的方式，例：%{0}为结尾匹配；{0}%为起始匹配；%{0}%为全匹配")]
        public string LikeStr
        {
            get
            {
                return likeStr;
            }
            set
            {
                likeStr = value;
            }
        }

        private System.Drawing.Size size = new System.Drawing.Size(120, 20);
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("控件的大小，包括标签")]
        public System.Drawing.Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        private System.Drawing.Point location = new System.Drawing.Point();
        [System.ComponentModel.Category("B.基类控件")]
        [System.ComponentModel.Description("控件的位置")]
        public System.Drawing.Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
    }

    [Serializable]
    public class TextBoxType : BaseControlType
    {
        private bool isPadLeft = false;
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("是否启用左边补齐字符模式，True启用，False不启用")]
        public bool IsPadLeft
        {
            get
            {
                return isPadLeft;
            }
            set
            {
                isPadLeft = value;
            }
        }

        private int length = 10;
        [System.ComponentModel.DefaultValue(10)]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("启用左边补齐字符模式时，补齐的位数；默认为10")]
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        private string padLeftName = "0";
        [System.ComponentModel.DefaultValue("0")]
        [System.ComponentModel.Category("T.一般输入框")]
        [System.ComponentModel.Description("启用左边补齐字符模式时，补齐的字符；默认为0")]
        public string PadLeftName
        {
            get
            {
                return padLeftName;
            }
            set
            {
                padLeftName = value;
            }
        }

        public override string ToString()
        {
            return "文本框";
        }
    }

    [Serializable]
    public class ComboBoxType : BaseControlType
    {
        private bool isAddAll = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("是否增加'全部'选项，True增加，False不增加")]
        public bool IsAddAll
        {
            get
            {
                return isAddAll;
            }
            set
            {
                this.isAddAll = value;
            }
        }

        private FS.FrameWork.Models.NeuObject allValue = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("增加'全部'选项时，全部选项的编码和名称")]
        [TypeConverter(typeof(ControlTypeConverter))]
        public FS.FrameWork.Models.NeuObject AllValue
        {
            get
            {
                return this.allValue;
            }
            set
            {
                this.allValue = value;
            }
        }

        private ArrayList datasource = new ArrayList();
        public ArrayList DataSource
        {
            get
            {
                return datasource;
            }
            set
            {
                datasource = value;
            }
        }

        private List<FS.FrameWork.Models.NeuObject> defaultDataSource = new List<FS.FrameWork.Models.NeuObject>();
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("当数据源为Custom模式时，默认数据集合的定义")]
        public List<FS.FrameWork.Models.NeuObject> DefaultDataSource
        {
            get
            {
                return defaultDataSource;
            }
            set
            {
                defaultDataSource = value;
            }
        }


        private EnumDataSourceType dataSourceType = EnumDataSourceType.Dictionary;
        [System.ComponentModel.DefaultValue(EnumDataSourceType.Dictionary)]
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("数据源的来源类型，Sql：使用Sql语句加载数据；Custom：自定义数据集；Dictionary：常数表加载；DepartmentType：科室类型；EmployeeType：人员类型")]
        public EnumDataSourceType DataSourceType
        {
            get
            {
                return dataSourceType;
            }
            set
            {
                dataSourceType = value;
            }
        }

        private string dataSourceTypeName = string.Empty;
        [System.ComponentModel.DefaultValue("ALL")]
        [System.ComponentModel.Category("C.一般下拉框")]
        [System.ComponentModel.Description("数据源加载来源类型的子类型，例如：数据来源为EmployeeType时，可以用人员类型来加载数据，ALL则为全部子类型")]
        public string DataSourceTypeName
        {
            get
            {
                return dataSourceTypeName;
            }
            set
            {
                dataSourceTypeName = value;
            }
        }

        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "DefaultDataSource" && genericValue != null)
            {
                List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
                foreach (FS.FrameWork.Models.NeuObject step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
        }

        public override string ToString()
        {
            return "下拉框";
        }
    }

    [Serializable]
    public class DateTimeType : BaseControlType
    {
        private int addDays = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("当前时间的基础上默认增加/减少几天")]
        public int AddDays
        {
            get
            {
                return addDays;
            }
            set
            {
                addDays = value;
            }
        }

        private int addMonths = 0;
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("当前时间的基础上默认增加/减少几个月")]
        public int AddMonths
        {
            get
            {
                return addMonths;
            }
            set
            {
                addMonths = value;
            }
        }

        private string customFormat = "yyyy-MM-dd";
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("自定义日期格式 默认：yyyy-MM-dd")]
        public string CustomFormat
        {
            get
            {
                return customFormat;
            }
            set
            {
                customFormat = value;
            }
        }
        private string customStartTime = "00:00:00";
        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Category("D.默认开始时间")]
        [System.ComponentModel.Description("自定义开始时间 默认：00:00:00")]
        public string CustomStartTime
        {
            get
            {
                return customStartTime;
            }
            set
            {
                customStartTime = value;
            }
        }
        private System.Windows.Forms.DateTimePickerFormat format = System.Windows.Forms.DateTimePickerFormat.Custom;
        [System.ComponentModel.DefaultValue(System.Windows.Forms.DateTimePickerFormat.Custom)]
        [System.ComponentModel.Category("D.时间选择框")]
        [System.ComponentModel.Description("使用日期格式的模式")]
        public System.Windows.Forms.DateTimePickerFormat Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
            }
        }

        public override string ToString()
        {
            return "日期控件";
        }
    }

    [Serializable]
    public class TreeViewType : BaseControlType
    {
        private bool isAddAll = true;
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("是否增加'全部'选项，True增加，False不增加")]
        public bool IsAddAll
        {
            get
            {
                return isAddAll;
            }
            set
            {
                this.isAddAll = value;
            }
        }

        private FS.FrameWork.Models.NeuObject allValue = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("增加'全部'选项时，全部选项的编码和名称")]
        [TypeConverter(typeof(ControlTypeConverter))]
        public FS.FrameWork.Models.NeuObject AllValue
        {
            get
            {
                return this.allValue;
            }
            set
            {
                this.allValue = value;
            }
        }

        private ArrayList datasource = new ArrayList();
        public ArrayList DataSource
        {
            get
            {
                return datasource;
            }
            set
            {
                datasource = value;
            }
        }

        private List<FS.FrameWork.Models.NeuObject> defaultDataSource = new List<FS.FrameWork.Models.NeuObject>();
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("当数据源为Custom模式时，默认数据集合的定义")]
        public List<FS.FrameWork.Models.NeuObject> DefaultDataSource
        {
            get
            {
                return defaultDataSource;
            }
            set
            {
                defaultDataSource = value;
            }
        }


        private EnumDataSourceType dataSourceType = EnumDataSourceType.Dictionary;
        [System.ComponentModel.DefaultValue(EnumDataSourceType.Dictionary)]
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("数据源的来源类型，Sql：使用Sql语句加载数据；Custom：自定义数据集；Dictionary：常数表加载；DepartmentType：科室类型；EmployeeType：人员类型")]
        public EnumDataSourceType DataSourceType
        {
            get
            {
                return dataSourceType;
            }
            set
            {
                dataSourceType = value;
            }
        }

        private string dataSourceTypeName = string.Empty;
        [System.ComponentModel.DefaultValue("ALL")]
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("数据源加载来源类型的子类型，例如：数据来源为EmployeeType时，可以用人员类型来加载数据，ALL则为全部子类型")]
        public string DataSourceTypeName
        {
            get
            {
                return dataSourceTypeName;
            }
            set
            {
                dataSourceTypeName = value;
            }
        }


        private bool isCheckBox = false;
        [System.ComponentModel.DefaultValue("ALL")]
        [System.ComponentModel.Category("T.树控件")]
        [System.ComponentModel.Description("数据源加载来源类型的子类型，例如：数据来源为EmployeeType时，可以用人员类型来加载数据，ALL则为全部子类型")]
        public bool IsCheckBox
        {
            get
            {
                return isCheckBox;
            }
            set
            {
                isCheckBox = value;
            }
        }

        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "DefaultDataSource" && genericValue != null)
            {
                List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
                foreach (FS.FrameWork.Models.NeuObject step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
        }

        public override string ToString()
        {
            return "树列表";
        }
    }

    [Serializable]
    public class CustomControl : BaseControlType
    {
        private string dllName = "HISFC.Components.Common";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义控件dll名称")]
        public string DllName
        {
            get
            {
                return this.dllName;
            }
            set
            {
                this.dllName = value;
            }
        }

        private string typeName = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要查找的自定义控件type名称")]
        public string TypeName
        {
            get
            {
                return this.typeName;
            }
            set
            {
                this.typeName = value;
            }
        }

        private string valueProperty = "";
        [System.ComponentModel.Category("CC.自定义控件")]
        [System.ComponentModel.Description("需要从自动控件的哪个属性取值")]
        public string ValueProperty
        {
            get
            {
                return this.valueProperty;
            }
            set
            {
                this.valueProperty = value;
            }
        }

        public override string ToString()
        {
            return "自定义控件";
        }
    }

    [Serializable]
    public class FilterTextBox : BaseControlType
    {
        private string filterStr = "";
        [System.ComponentModel.Category("FT.过滤文本框")]
        [System.ComponentModel.Description("设置过滤时使用的语句，例如patient_no like '%{0}%'")]
        public string FilterStr
        {
            get
            {
                return filterStr;
            }
            set
            {
                filterStr = value;
            }
        }

        private bool isEnterFilter = false;
        [System.ComponentModel.Category("FT.过滤文本框")]
        [System.ComponentModel.Description("是否回车后过滤，True:是，False:否")]
        public bool IsEnterFilter
        {
            get
            {
                return isEnterFilter;
            }
            set
            {
                isEnterFilter = value;
            }
        }


        public override string ToString()
        {
            return "文本过滤框";
        }
    }

    [Serializable]
    public class FilterComboBox : ComboBoxType
    {
        private string filterStr = "";
        [System.ComponentModel.Category("FC.过滤下拉框")]
        [System.ComponentModel.Description("设置过滤时使用的语句，例如patient_no like '%{0}%'")]
        public string FilterStr
        {
            get
            {
                return filterStr;
            }
            set
            {
                filterStr = value;
            }
        }

        public override string ToString()
        {
            return "下拉过滤框";
        }
    }

    [Serializable]
    public class FilterTreeView : TreeViewType
    {
        private string filterStr = "";
        [System.ComponentModel.Category("FT.过滤树")]
        [System.ComponentModel.Description("设置过滤时使用的语句，例如patient_no like '%{0}%'")]
        public string FilterStr
        {
            get
            {
                return filterStr;
            }
            set
            {
                filterStr = value;
            }
        }

        public override string ToString()
        {
            return "树过滤框";
        }
    }

    [Serializable]
    public class DataSourceType
    {
        private string name = string.Empty;
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string sql = string.Empty;
        /// <summary>
        /// 数据源sql
        /// </summary>
        public string Sql
        {
            get
            {
                return sql;
            }
            set
            {
                sql = value;
            }
        }

        private bool addMapRow = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapRow
        {
            get
            {
                return addMapRow;
            }
            set
            {
                addMapRow = value;
            }
        }

        private bool addMapColumn = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapColumn
        {
            get
            {
                return addMapColumn;
            }
            set
            {
                addMapColumn = value;
            }
        }

        private bool addMapData = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapData
        {
            get
            {
                return addMapData;
            }
            set
            {
                addMapData = value;
            }
        }

        private bool isCross = false;
        /// <summary>
        /// 是否交叉报表
        /// </summary>
        public bool IsCross
        {
            get
            {
                return isCross;
            }
            set
            {
                isCross = value;
            }
        }

        private string crossRows = string.Empty;
        private string crossColumns = string.Empty;
        private string crossValues = string.Empty;
        /// <summary>
        /// 交叉行
        /// </summary>
        public string CrossRows
        {
            get { return crossRows; }
            set { crossRows = value; }
        }
        /// <summary>
        /// 交叉列
        /// </summary>
        public string CrossColumns
        {
            get { return crossColumns; }
            set { crossColumns = value; }
        }
        /// <summary>
        /// 交叉值
        /// </summary>
        public string CrossValues
        {
            get { return crossValues; }
            set { crossValues = value; }
        }

        private string crossCombinColumns = string.Empty;
        /// <summary>
        /// 交叉合并列
        /// </summary>
        public string CrossCombinColumns
        {
            get
            {
                return crossCombinColumns;
            }
            set
            {
                crossCombinColumns = value;
            }
        }

        private string crossSumColumns = string.Empty;
        /// <summary>
        /// 交叉合计列
        /// </summary>
        public string CrossSumColumns
        {
            get
            {
                return crossSumColumns;
            }
            set
            {
                crossSumColumns = value;
            }
        }

        private string crossGroupColumns = string.Empty;
        /// <summary>
        /// 分组列
        /// </summary>
        public string CrossGroupColumns
        {
            get
            {
                return crossGroupColumns;
            }
            set
            {
                crossGroupColumns = value;
            }
        }
    }
}
