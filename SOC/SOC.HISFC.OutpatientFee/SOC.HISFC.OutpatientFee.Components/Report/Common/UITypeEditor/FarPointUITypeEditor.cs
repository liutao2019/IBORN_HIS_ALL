using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor
{
    public class FarPointUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    value = "Report\\" + Guid.NewGuid().ToString() + ".xml";
                }

                FarPoint.Win.Spread.Design.DesignerMain c = new FarPoint.Win.Spread.Design.DesignerMain();
                //使用默认的文件
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString()) == false)
                {
                    System.IO.FileStream fs = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString());
                    c.Spread.Save(fs, false);
                    fs.Close();
                }
                c.Spread.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString());
                c.Tag = FS.FrameWork.WinForms.Classes.Function.CurrentPath + value.ToString();
                c.FormClosing += new FormClosingEventHandler(c_FormClosing);
                c.ShowDialog();
                return base.EditValue(context, provider, value);
            }

            return base.EditValue(context, provider, value);
        }

        void c_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FarPoint.Win.Spread.Design.DesignerMain)sender).SaveFile(((FarPoint.Win.Spread.Design.DesignerMain)sender).Tag.ToString());
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
