using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor
{
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
                            new GroupTreeViewType(),
                            new FilterTextBox(),
                            new FilterComboBox(),
                            new FilterTreeView(),
                            new CustomControl(),
                            new CheckBoxType()
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
}
