using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.TypeConterter
{
    public class ControlTypeConverter : System.ComponentModel.TypeConverter
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
            else if (value is GroupTreeViewType)
            {
                return TypeDescriptor.GetProperties(typeof(GroupTreeViewType), attributes);
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
            else if (value is CheckBoxType)
            {
                return TypeDescriptor.GetProperties(typeof(CheckBoxType), attributes);
            }
            else
            {
                return base.GetProperties(context, value, attributes);
            }

        }
    }
}
