using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
namespace FS.HISFC.Components.EPR.Interface
{
    public class ContinuePrint:IContinuePrint
    {
        #region IContinuePrint 成员

        public bool IsCanContinuePrint(System.Windows.Forms.Control panel)
        {
            if (getRichtTextBox(panel as FS.FrameWork.EPRControl.emrPanel) == null) return false;
            return true;
        }

        public void Print(System.Windows.Forms.Control panel)
        {
            RichTextBox c = this.getRichtTextBox(panel as FS.FrameWork.EPRControl.emrPanel);
            if(c == null)
            {
                MessageBox.Show("该页不支持续打！");
                return ;
            }
            frmContinuePrint form = new frmContinuePrint(c,panel);
            if (form.ShowDialog() == DialogResult.OK)
            {
                c.Tag = form.StartLength;
                
                this.continuePrint(c,form.StartPage, form.chkTitile.Checked,panel);

            }
            
        }



     

        #endregion
        protected System.Windows.Forms.RichTextBox getRichtTextBox(FS.FrameWork.EPRControl.emrPanel panel)
        {
            if (panel == null) return null;
            foreach (Component c in panel.Components)
            {
                if (c.GetType().IsSubclassOf(typeof(RichTextBox)))
                    return c as RichTextBox;

            }
            return null;
        }
        private void continuePrint(Control c,int page, bool bTitle,Control panel)
        {
            ((FS.FrameWork.EPRControl.emrPanel)panel).AutoScrollPosition = new Point(0, 0);

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (page == 0) page = 1;
            Common.Classes.Function.GetPageSize("EMR", ref print);
            print.PrintDocument.DefaultPageSettings.PrinterSettings.FromPage = page;
            print.PrintDocument.DefaultPageSettings.PrinterSettings.ToPage = 100;
            print.IsPrintInputBox = !bTitle;
            print.IsPrintBackImage = false;
            print.IsDataAutoExtend = false;
            print.IsHaveGrid = true;
            if (bTitle == false)
                setOtherControlVisible(false,panel);
            ((RichTextBox)c).Select(0, 0);
            ((RichTextBox)c).ScrollToCaret();
            
            FS.FrameWork.WinForms.Classes.PrintControlCompare p = new FS.FrameWork.WinForms.Classes.PrintControlCompare();
            p.SetEPRControl();
            print.SetControlCompare(p);

            print.PageLabel = ((FS.FrameWork.EPRControl.emrPanel)panel).PageNumberControl;
            //this.ucDataFileLoader1.CurrentLoader.bNew = true;
            print.PrintPage(0,0,panel);
            //this.ucDataFileLoader1.CurrentLoader.bNew = false;

            if (bTitle == false) setOtherControlVisible(true,panel);
            c.Tag = c.Text.Length;
        }
        ArrayList alForVisibleControls = null;
        private void setOtherControlVisible(bool bVisible,Control panel)
        {
            if (panel == null) return;

            if (bVisible == false)
            {
                alForVisibleControls = new ArrayList();
                foreach (Control c in ((FS.FrameWork.EPRControl.emrPanel)panel).Controls)
                {
                    FS.FrameWork.EPRControl.IUserControlable ip = c as FS.FrameWork.EPRControl.IUserControlable;
                    if (ip != null)
                    {
                        if (ip.FocusedControl.GetType().IsSubclassOf(typeof(RichTextBox)) == false)
                        {
                            if (c.Visible)
                            {
                                this.alForVisibleControls.Add(c.Handle);
                                c.Visible = false;
                            }
                        }
 
                    }
                    else
                    {
                        if (c.GetType().IsSubclassOf(typeof(RichTextBox)) == false)
                        {
                            if (c.Visible)
                            {
                                c.Visible = false;
                                this.alForVisibleControls.Add(c.Handle);
                            }
                        }

                    }
                }
            }
            else
            {
                foreach (Control c in ((FS.FrameWork.EPRControl.emrPanel)panel).Controls)
                {
                    if (c.GetType().IsSubclassOf(typeof(RichTextBox)) == true)
                    {

                    }
                    else
                    {
                        if (alForVisibleControls == null) return;
                        foreach (System.IntPtr handle in alForVisibleControls)
                        {
                            if (c.Handle == handle)
                            {
                                c.Visible = true;
                                break;
                            }
                        }

                    }
                }

            }

        }
    }
}
