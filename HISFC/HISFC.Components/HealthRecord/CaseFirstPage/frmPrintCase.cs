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
    public partial class frmPrintCase : Form
    {
        /// <summary>
        /// ������ҳ��ӡ
        /// </summary>
        public frmPrintCase(FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfo ucCase)
        {
            InitializeComponent();
            ucCaseMainInfo = ucCase;
        }
        private FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfo ucCaseMainInfo;
      
        private void btnPreviewSecond_Click(object sender, EventArgs e)
        {
            try
            {
                ucCaseMainInfo.PrintCase("PrintBackPreview");
            }
            catch
            {
                MessageBox.Show("������ҳ����δ���浼���쳣�����ȳ��ԡ����桿�����²�����", "��ʾ", 
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
                //ucCaseMainInfo.PrintPreview(sender, null);
            }
            catch
            {
                MessageBox.Show("������ҳ����δ���浼���쳣�����ȳ��ԡ����桿�����²�����", "��ʾ",
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
                //ucCaseMainInfo.Print(sender, null);
            }
            catch
            {
                MessageBox.Show("������ҳ����δ���浼���쳣�����ȳ��ԡ����桿�����²�����", "��ʾ",
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
                //ucCaseMainInfo.PrintBack(null);
            }
            catch
            {
                MessageBox.Show("������ҳ����δ���浼���쳣�����ȳ��ԡ����桿�����²�����", "��ʾ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        private void frmPrintCase_Load(object sender, EventArgs e)
        {

        }
    }
}