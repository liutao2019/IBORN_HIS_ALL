using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using System.Windows.Forms;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.UITypeEditor
{

    public class SetInfoUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        System.Windows.Forms.Design.IWindowsFormsEditorService editorService = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                if (editorService != null)
                {
                    if (string.IsNullOrEmpty(((ReportQueryInfo)value).QueryFilePath))
                    {
                        ((ReportQueryInfo)value).QueryFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\" + Guid.NewGuid().ToString() + ".xml";
                    }

                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath) == false)
                    {
                        System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath).Close();

                        //保存
                        System.IO.StreamWriter swa = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath);
                        ReportQueryInfo reportQueryInfo = new ReportQueryInfo();
                        ((ReportQueryInfo)value).List = reportQueryInfo.GetDefaults();
                        //取默认值
                        swa.Write(ICommonReportController.XmlSerialization(((ReportQueryInfo)value)));
                        swa.Close();
                    }
                    else
                    {
                        //读取
                        System.IO.StreamReader sr = new System.IO.StreamReader(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ((ReportQueryInfo)value).QueryFilePath);
                        string xml = sr.ReadToEnd();
                        value = ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        //((ReportQueryInfo)value).List = r.List;
                        //((ReportQueryInfo)value).QueryDataSource = r.QueryDataSource; ;
                        //((ReportQueryInfo)value).TableGroup = r.TableGroup;
                        sr.Close();
                    }

                    ReportQueryInfo a = ((ReportQueryInfo)value);

                    ISettingReportForm ISettingReportForm =
                         ICommonReportController.CreateFactory().CreateInferface<ISettingReportForm>(this.GetType(), null);
                    if (ISettingReportForm == null)
                    {
                        ISettingReportForm = new frmQueryInfoSetting();
                    }
                    ISettingReportForm.SetValue(a);
                    if (ISettingReportForm is Form)
                    {
                        editorService.ShowDialog(ISettingReportForm as Form);
                    }

                    a = ISettingReportForm.GetValue();
                    a.QueryFilePath = ((ReportQueryInfo)value).QueryFilePath;
                    //保存
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + a.QueryFilePath);
                    sw.Write(ICommonReportController.XmlSerialization(a));
                    sw.Close();
                    return base.EditValue(context, provider, a);
                }
            }

            return base.EditValue(context, provider, value);

        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
