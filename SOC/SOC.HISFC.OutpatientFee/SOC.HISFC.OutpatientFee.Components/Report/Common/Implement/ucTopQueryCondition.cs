using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    public partial class ucTopQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl,ITopQueryCondition
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

        public void AddControls(List<QueryControl> list)
        {
            if (list == null)
            {
                return;
            }

            foreach (QueryControl common in list)
            {
                NeuLabel label = new NeuLabel();
                if (common.IsAddText)
                {
                    label.AutoSize = true;
                    label.Name = "lb" + common.Name;
                    label.Text = common.Text;
                    label.Location = new Point(common.ControlType.Location.X, common.ControlType.Location.Y + 5);
                    this.Controls.Add(label);
                }
                
                Control control = null;
                
                if (common.ControlType is TextBoxType)//正常的文本框
                {
                    TextBoxType textType = common.ControlType as TextBoxType;
                    control = new NeuTextBox();
                    setCommonControl(ref control, common,label);
                    control.Text = textType.DataSource;

                    control.KeyDown += new KeyEventHandler(textBox_KeyDown);
                }
                else if (common.ControlType is FilterTextBox)//过滤文本框
                {
                    control = new NeuTextBox();
                    setCommonControl(ref control, common, label);
                    control.Text = "";
                    FilterTextBox filterTxt = common.ControlType as FilterTextBox;
                    if (filterTxt.IsEnterFilter)
                    {
                        control.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                    else
                    {
                        control.TextChanged += new EventHandler(textBox_TextChanged);
                    }
                    filterDictionary.Add(common.Name, ((FilterTextBox)common.ControlType).FilterStr);
                }
                else if (common.ControlType is FilterComboBox)//过滤下拉框
                {
                    ComboBoxType comboType = common.ControlType as ComboBoxType;
                    control = new NeuComboBox();
                    setCommonControl(ref control, common, label);
                    control.Text = "";
                    //加载数据
                    ((NeuComboBox)control).AddItems(comboType.DataSource);
                    ((NeuComboBox)control).SelectedValueChanged += new EventHandler(combobox_SelectedValueChanged);
                    filterDictionary.Add(common.Name, ((FilterComboBox)common.ControlType).FilterStr);
                    if (comboType.IsAddAll)
                    {
                        ((NeuComboBox)control).Tag = comboType.AllValue.ID;
                    }
                }
                else if (common.ControlType is ComboBoxType)//下拉框
                {
                    ComboBoxType comboType = common.ControlType as ComboBoxType;
                    control = new NeuComboBox();
                    setCommonControl(ref control, common, label);
                    control.Text = "";
                    //加载数据
                    ((NeuComboBox)control).AddItems(comboType.DataSource);
                    if (comboType.IsAddAll)
                    {
                        ((NeuComboBox)control).Tag = comboType.AllValue.ID;
                    }
                }
                else if (common.ControlType is DateTimeType)//时间控件
                {
                    DateTimeType dtType = common.ControlType as DateTimeType;
                    control = new NeuDateTimePicker();
                    setCommonControl(ref control, common, label);
                    ((NeuDateTimePicker)control).Format = dtType.Format;
                    ((NeuDateTimePicker)control).CustomFormat = dtType.CustomFormat;
                    if (string.IsNullOrEmpty(dtType.DataSource))
                    {
                        //默认值
                        ((NeuDateTimePicker)control).Value = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetSystemTime().AddMonths(dtType.AddMonths).AddDays(dtType.AddDays);
                    }
                    else
                    {
                        ((NeuDateTimePicker)control).Value = FS.FrameWork.Function.NConvert.ToDateTime(dtType.DataSource).AddMonths(dtType.AddMonths).AddDays(dtType.AddDays);
                    }
                }
                else if (common.ControlType is CheckBoxType)
                {
                    CheckBoxType ckType = common.ControlType as CheckBoxType;
                    control = new NeuCheckBox();
                    control.Name =common.Name;
                    control.Text = common.Text;
                    control.Location = new Point(common.ControlType.Location.X, common.ControlType.Location.Y + 5);
                    ((NeuCheckBox)control).AutoSize = false;
                    control.Size = new Size(common.ControlType.Size.Width, common.ControlType.Size.Height);
                    ((NeuCheckBox)control).Checked = ckType.DefaultDataSource;

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
                    control = Activator.CreateInstance(_type, null) as Control;
                    control.Name = common.Name;
                    control.Text = common.Text;
                    control.TabIndex = common.Index;
                    control.Size = common.ControlType.Size;
                    if (string.IsNullOrEmpty(cControl.EventProperty) == false)
                    {
                        if (_type.GetEvent(cControl.EventProperty) != null)
                        {
                            //自定义事件
                            _type = Type.GetType(cControl.DelegateTypeName);
                            if (_type == null)
                            {
                                _assembly = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath + (cControl.DelegateDllName.ToUpper().Contains(".DLL") ? cControl.DelegateDllName : cControl.DelegateDllName + ".dll"));
                            }
                            else
                            {
                                _assembly = System.Reflection.Assembly.GetAssembly(_type);
                            }

                            _type = _assembly.GetType(cControl.DelegateTypeName);

                            control.GetType().GetEvent(cControl.EventProperty).AddEventHandler(control, Delegate.CreateDelegate(_type, this, "myEnter"));
                        }
                    }
                    if (common.IsAddText)
                    {
                        control.Location = new Point(common.ControlType.Location.X + label.Size.Width + 10, common.ControlType.Location.Y);
                    }
                    else
                    {
                        control.Location = new Point(common.ControlType.Location.X, common.ControlType.Location.Y);
                    }
                }
                this.Controls.Add(control);
            }
        }

        private void setCommonControl(ref Control control, QueryControl common, NeuLabel label)
        {
            control.Name = common.Name;
            control.TabIndex = common.Index;
            control.Size = common.ControlType.Size;
            control.Enabled = common.ControlType.Enabled;
            control.Location = new Point(common.ControlType.Location.X + label.Size.Width + 10, common.ControlType.Location.Y);
        }

        public Object GetValue(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TextBoxType)
                {
                    NeuTextBox textBox = controls[0] as  NeuTextBox;
                    return textBox.Text;
                }
                else if (common.ControlType is CheckBoxType)
                {
                    return ((NeuCheckBox)controls[0]).Checked;
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
                        return dt.Value.ToString(dtType.CustomFormat);
                    }
                    else
                    {
                        return dt.Value.ToString();
                    }
                }
                else if (common.ControlType is CustomControl)
                {
                    CustomControl cControl = common.ControlType as CustomControl;
                    object o = controls[0].GetType().GetProperty(cControl.ValueProperty).GetValue(controls[0], null);
                    if (o == null)
                    {
                        return "";
                    }

                    return o.ToString();
                }
            }

            return "";
        }

        public Object GetText(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is CustomControl)
                {
                    CustomControl cControl = common.ControlType as CustomControl;
                    object o = controls[0].GetType().GetProperty(cControl.ValueProperty).GetValue(controls[0], null);
                    if (o == null)
                    {
                        return "";
                    }

                    return o.ToString();
                }
                else
                {
                    return controls[0].Text;
                }
            }

            return "";
        }
        #endregion

        #region ITopQueryCondition 成员

        public event ICommonReportController.FilterHandler OnFilterHandler;

        public void myEnter()
        {
            if (this.OnFilterHandler != null)
            {
                this.OnFilterHandler("");
            }
        }

        #endregion
    }
}
