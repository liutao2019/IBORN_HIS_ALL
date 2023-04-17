using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    public partial class ucTopQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl,ICommonReportController.ITopQueryCondition
    {
        public ucTopQueryCondition()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> filterDictionary = new Dictionary<string, string>();

        private void onFilter()
        {
            if (this.OnFilterHandler != null)
            {
                string filter = "";
                //取数据
                foreach (KeyValuePair<string, string> item in filterDictionary)
                {
                    //找控件
                    Control[] c = this.Controls.Find(item.Key, true);
                    if (c != null && c.Length >= 0)
                    {
                        if (c[0] is NeuTextBox)
                        {
                            //去掉无效字符
                            filter += " " + string.Format(item.Value, FS.FrameWork.Public.String.TakeOffSpecialChar(((NeuTextBox)c[0]).Text));
                        }
                        else if (c[0] is NeuComboBox)
                        {
                            filter += " " + string.Format(item.Value, ((NeuComboBox)c[0]).Tag);
                        }
                    }
                }

                this.OnFilterHandler(filter);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.onFilter();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.onFilter();
            }
        }

        private void combobox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.onFilter();
        }

        #region ITopQueryCondition 成员

        public int Init()
        {
            this.Controls.Clear();
            filterDictionary.Clear();
            return 1;
        }

        public void AddControls(List<CommonReportQueryInfo> list)
        {
            if (list == null)
            {
                return;
            }

            foreach (CommonReportQueryInfo common in list)
            {
                NeuLabel l = new NeuLabel();
                if (common.IsAddText)
                {
                    l.AutoSize = true;
                    l.Name = "lb" + common.Name;
                    l.Text = common.Text;
                    l.Location = new Point(common.ControlType.Location.X, common.ControlType.Location.Y + 5);
                    this.Controls.Add(l);
                }

                if (common.ControlType is TextBoxType)//正常的文本框
                {
                    NeuTextBox textBox = new NeuTextBox();
                    textBox.Name = common.Name;
                    textBox.TabIndex = common.Index;
                    textBox.Text = "";
                    textBox.Size = common.ControlType.Size;
                    textBox.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    this.Controls.Add(textBox);
                }
                else if (common.ControlType is FilterTextBox)//过滤文本框
                {
                    NeuTextBox textBox = new NeuTextBox();
                    textBox.Name = common.Name;
                    textBox.TabIndex = common.Index;
                    textBox.Text = "";
                    textBox.Size = common.ControlType.Size;
                    textBox.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    FilterTextBox filterTxt = common.ControlType as FilterTextBox;
                    if (filterTxt.IsEnterFilter)
                    {
                        textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                    else
                    {
                        textBox.TextChanged += new EventHandler(textBox_TextChanged);
                    }
                    filterDictionary.Add(common.Name, ((FilterTextBox)common.ControlType).FilterStr);
                    this.Controls.Add(textBox);
                }
                else if (common.ControlType is FilterComboBox)//过滤下拉框
                {
                    ComboBoxType comboType = common.ControlType as ComboBoxType;
                    NeuComboBox combobox = new NeuComboBox();
                    combobox.Name = common.Name;
                    combobox.TabIndex = common.Index;
                    combobox.Text = "";
                    combobox.Size = common.ControlType.Size;
                    combobox.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    //加载数据
                    combobox.AddItems(comboType.DataSource);
                    combobox.SelectedValueChanged += new EventHandler(combobox_SelectedValueChanged);
                    filterDictionary.Add(common.Name, ((FilterComboBox)common.ControlType).FilterStr);
                    if (comboType.IsAddAll)
                    {
                        combobox.Tag = comboType.AllValue.ID;
                    }

                    if (comboType.DataSourceType == EnumDataSourceType.CurrentOper )
                    {
                        combobox.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                    }
                    else if (comboType.DataSourceType == EnumDataSourceType.CurrentDept)
                    {
                        combobox.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    }
                    this.Controls.Add(combobox);
                }
                else if (common.ControlType is ComboBoxType)//下拉框
                {
                    ComboBoxType comboType = common.ControlType as ComboBoxType;
                    NeuComboBox combobox = new NeuComboBox();
                    combobox.Name = common.Name;
                    combobox.TabIndex = common.Index;
                    combobox.Text = "";
                    combobox.Size = common.ControlType.Size;
                    combobox.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    //加载数据
                    combobox.AddItems(comboType.DataSource);
                    if (comboType.IsAddAll)
                    {
                        combobox.Tag = comboType.AllValue.ID;
                    }
                    if (comboType.DataSourceType == EnumDataSourceType.CurrentOper)
                    {
                        combobox.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                    }
                    else if (comboType.DataSourceType == EnumDataSourceType.CurrentDept)
                    {
                        combobox.Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    }
                    this.Controls.Add(combobox);
                }
                else if (common.ControlType is DateTimeType)//时间控件
                {
                    DateTimeType dtType = common.ControlType as DateTimeType;
                    NeuDateTimePicker dt = new NeuDateTimePicker();
                    dt.Size = common.ControlType.Size;
                    dt.Name = common.Name;
                    dt.TabIndex = common.Index;
                    dt.Format = dtType.Format;
                    dt.CustomFormat = dtType.CustomFormat;
                    dt.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    //默认值
                    DateTime tem = (FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetSystemTime().AddMonths(dtType.AddMonths).AddDays(dtType.AddDays));
                    dt.Value = DateTime.Parse(tem.ToShortDateString() + " " + dtType.CustomStartTime);
                    this.Controls.Add(dt);
                }
                else if (common.ControlType is CustomControl)
                {
                    CustomControl cControl = common.ControlType as CustomControl;
                    //装载程序集
                    System.Reflection.Assembly _assembly;
                    Type _type = Type.GetType(cControl.TypeName);
                    if (_type == null)
                    {
                        _assembly = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath + (cControl.DllName.ToUpper().Contains(".DLL") ? cControl.DllName : cControl.DllName + ".dll"));
                    }
                    else
                    {
                        _assembly = System.Reflection.Assembly.GetAssembly(_type);
                    }

                    _type = _assembly.GetType(cControl.TypeName);
                    Control c = Activator.CreateInstance(_type, null) as Control;
                    c.Name = common.Name;
                    c.Text = common.Text;
                    c.TabIndex = common.Index;
                    c.Size = common.ControlType.Size;
                    if (common.IsAddText)
                    {
                        c.Location = new Point(common.ControlType.Location.X + l.Size.Width + 10, common.ControlType.Location.Y);
                    }
                    else
                    {
                        c.Location = new Point(common.ControlType.Location.X , common.ControlType.Location.Y);
                    }

                    this.Controls.Add(c);
                }
            }
        }

        public Object GetValue(CommonReportQueryInfo common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TextBoxType)
                {
                    NeuTextBox textBox = controls[0] as  NeuTextBox;
                    return textBox.Text;
                }
                else if (common.ControlType is ComboBoxType)
                {
                    NeuComboBox comboType = controls[0] as NeuComboBox;
                    return comboType.Tag == null ? "" : comboType.Tag.ToString();
                }
                else if (common.ControlType is DateTimeType)
                {
                    NeuDateTimePicker dt = controls[0] as NeuDateTimePicker;
                    DateTimeType dtType = common.ControlType as DateTimeType;
                    if (dtType.Format == DateTimePickerFormat.Custom)
                    {
                        return dt.Value;
                    }
                    else 
                    {
                        return dt.Value;
                    }
                }
                else if (common.ControlType is CustomControl)
                {
                    CustomControl cControl = common.ControlType as CustomControl;
                    object o= controls[0].GetType().GetProperty(cControl.ValueProperty).GetValue(controls[0], null);
                    if (o == null)
                    {
                        return "";
                    }

                    return o.ToString();
                }
            }

            return "";
        }

        #endregion

        #region ITopQueryCondition 成员


        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion
    }
}
