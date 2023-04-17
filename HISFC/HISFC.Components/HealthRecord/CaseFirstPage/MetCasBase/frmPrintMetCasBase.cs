using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmPrintMetCasBase : Form
    {
        /// <summary>
        /// 病案首页打印
        /// </summary>
        public frmPrintMetCasBase(FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo ucCase)
        {
            InitializeComponent();
            ucCaseMainInfo = ucCase;
        }
        private int printPageNum = 3;
        /// <summary>
        /// 打印页数
        /// </summary>
        public int PrintPageNum
        {
            get { return this.printPageNum; }
            set
            {
                this.printPageNum = value;
                if (this.printPageNum == 2)
                {
                    this.btPrintThird.Visible = false;
                    this.btnPreviewThird.Visible = false;
                }
                else
                {
                    this.btPrintThird.Visible = true;
                    this.btnPreviewThird.Visible = true;
                }
            }
        }
        private FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo ucCaseMainInfo;
      
        private void btnPreviewSecond_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintBackPreview");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnPreviewFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintPreview");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnPrintFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("Print");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnPrintSecond_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintBack");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnPreviewThird_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintAdditionalPreview");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void btPrintThird_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintAdditional");
            }
            catch
            {
                MessageBox.Show("病案首页可能未保存导致异常，请先尝试【保存】再重新操作。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }
    }
}