using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucCalc : UserControl
    {
        public ucCalc()
        {
            InitializeComponent();
        }

        #region ����

        private bool isPopForm = true;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ񵯳�����
        /// </summary>
        public bool IsPopForm
        {
            set
            {
                isPopForm = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ���㲢��ʾ����
        /// </summary>
        /// <returns></returns>
        private int Compute()
        {
            string computeString = this.tbCompute.Text.Trim();
            bool isAdd = false;
            if (computeString == null || computeString == "")
            {
                if (isPopForm)
                {
                    try
                    {
                        this.FindForm().Close();
                        return 0;
                    }
                    catch { }
                }
            }
            string tmpFirstValue = "";

            if (computeString.Length > 1)
            {
                tmpFirstValue = computeString.Substring(0, 1);
            }
            if (tmpFirstValue == "*" || tmpFirstValue == "-" || tmpFirstValue == "+" || tmpFirstValue == "/")
            {
                isAdd = false;
            }
            else
            {
                isAdd = true;
            }

            decimal totCost = 0;
            decimal nowCost = 0;
            try
            {
                totCost = Convert.ToDecimal(this.tbComputeResult.Text.Trim());
            }
            catch (Exception e)
            {
                MessageBox.Show("�ܽ��������!" + e.Message);
                this.tbCompute.Focus();
                this.tbCompute.SelectAll();

                return -1;
            }

            object objComputeValue = null;
            if (!isAdd)
            {
                computeString = totCost.ToString() + computeString;
            }
            try
            {
                objComputeValue = FS.FrameWork.Public.String.ExpressionVal(computeString == null ? "" : computeString);
            }
            catch (Exception e)
            {
                MessageBox.Show("���㹫ʽ���벻��ȷ!" + e.Message);
                this.tbCompute.Focus();
                this.tbCompute.SelectAll();

                return -1;
            }

            try
            {
                nowCost = Convert.ToDecimal(objComputeValue);
            }
            catch (Exception e)
            {
                MessageBox.Show("��ǰ���������!" + e.Message);
                this.tbCompute.Focus();
                this.tbCompute.SelectAll();

                return -1;
            }
            try
            {
                if (isAdd)
                {
                    totCost += nowCost;
                }
                else
                {
                    totCost = nowCost;
                }
                this.tbComputeResult.Text = totCost.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("�ܽ��������!" + e.Message);
                this.tbCompute.Focus();
                this.tbCompute.SelectAll();

                return -1;
            }

            this.tbCompute.Text = "";
            this.lbComputeText.Text = computeString;

            if (this.ckbIsPasteAuto.Checked)
            {
                tbComputeResult.SelectAll();
                tbComputeResult.Copy();
            }

            return 0;
        }
        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            this.tbCompute.Text = "";
            this.tbComputeResult.Text = "0.00";
            this.lbComputeText.Text = "���μ�����";
            this.tbCompute.Focus();
        }

        #endregion

        #region �¼�

        private void ucCalc_Load(object sender, System.EventArgs e)
        {
            if (isPopForm)
            {
                try
                {
                    this.FindForm().Text = "������";
                    panel2.Focus();
                    this.tbCompute.Focus();
                }
                catch { }
            }
        }

        private void tbCompute_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Compute();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Compute();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (isPopForm)
            {
                try
                {
                    this.FindForm().Close();
                }
                catch { };
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                this.Clear();
            }
            if (keyData == Keys.Escape)
            {
                if (isPopForm)
                {
                    try
                    {
                        this.FindForm().Close();
                    }
                    catch { };
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            this.Clear();
        }

        #endregion
    }
}
