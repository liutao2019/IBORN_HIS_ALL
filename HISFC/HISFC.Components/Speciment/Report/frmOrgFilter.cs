using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class frmOrgFilter : Form
    {
        public frmOrgFilter()
        {
            InitializeComponent();
        }

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        private Hashtable hsTr = new Hashtable();

        /// <summary>
        /// ɸѡsql���(���ڼ�������ϸ��Ϣ��)
        /// </summary>
        private string sqlStr = string.Empty;

        public string SqlStr
        {
            get
            {
                return this.sqlStr;
            }
            set
            {
                this.sqlStr = value;
            }
        }

        /// <summary>
        /// �����Ϣ���ˣ��жϷ��������Ŀ����Ϣ
        /// </summary>
        private string filterSql = string.Empty;
        public string FilterSql
        {
            set
            {
                this.filterSql = value;
            }
            get
            {
                return this.filterSql;
            }

        }

        /// <summary>
        /// DNA����
        /// </summary>
        private string dQty = "0";

        public string DQty
        {
            get
            {
                return this.dQty;
            }
            set
            {
                this.dQty = value;
            }
        }

        /// <summary>
        /// RNA����
        /// </summary>
        private string rQty = "0";

        public string RQty
        {
            get
            {
                return this.rQty;
            }
            set
            {
                this.rQty = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private string lQty = "0";

        public string LQty
        {
            get
            {
                return this.lQty;
            }
            set
            {
                this.lQty = value;
            }
        }

        private DialogResult dlRt = DialogResult.No;

        public DialogResult DlRt
        {
            get
            {
                return this.dlRt;
            }
            set
            {
                this.dlRt = value;
            }
        }
        #endregion

        #region ����
        private void frmOrgFilter_Load(object sender, EventArgs e)
        {
            hsTr.Add(cbxZW.Text, cbxZW.Text);
            hsTr.Add(cbxZL.Text, cbxZL.Text);
            hsTr.Add(cbxZC.Text, cbxZC.Text);
            hsTr.Add(cbxLBJ.Text, cbxLBJ.Text);
            hsTr.Add(cbxAS.Text, cbxAS.Text);
            hsTr.Add(cbxAP.Text, cbxAP.Text);
        }
        #endregion

        #region �¼�
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                #region ������������ֵ
                if (!string.IsNullOrEmpty(this.tbDNAQty.Text.Trim()))
                {
                    this.DQty = this.tbDNAQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbRNAQty.Text.Trim()))
                {
                    this.RQty = this.tbRNAQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbLKQty.Text.Trim()))
                {
                    this.LQty = this.tbLKQty.Text.Trim();
                }
                string strLjXQ = string.Empty;
                string strLjXJ = string.Empty;
                if (rbtAND.Checked)
                {
                    strLjXQ = "AND";
                }
                if (rbtOR.Checked)
                {
                    strLjXQ = "OR";
                }
                if (cbAND.Checked)
                {
                    strLjXJ = "AND";
                }
                if (cbOR.Checked)
                {
                    strLjXJ = "OR";
                }

                string strFilter = @"( DNA��� >= {0} ) " + strLjXQ + " ( RNA��� >= {1} ) " + strLjXJ +
                                 " ( ������ >= {2} )";
                this.FilterSql = string.Format(strFilter, this.DQty, this.RQty, this.LQty);
                #endregion

                #region �����ϸ����������ֵ
                if (!string.IsNullOrEmpty(this.tbDQty.Text.Trim()))
                {
                    this.DQty = this.tbDQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbRQty.Text.Trim()))
                {
                    this.RQty = this.tbRQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbLQty.Text.Trim()))
                {
                    this.LQty = this.tbLQty.Text.Trim();
                }
                string sSql = @" (right(t.�걾����,2) like '{0}%'
                                and t.�걾���� in
                                (select rr.�걾����
                                from t rr where  rr.�걾����=t.�걾���� and rr.�걾ԴID = t.�걾ԴID and rr.�������� = t.��������
                                order by rr.�걾���� desc 
                                fetch first {1} rows only)) ";
                string pSql = @" (right(t.�걾����,2) like '{0}%'
                                and t.�걾���� in
                                (select rr.�걾����
                                from t rr where  rr.�걾����=t.�걾���� and rr.�걾ԴID = t.�걾ԴID and rr.�������� = t.��������
                                order by rr.�걾���� desc 
                                fetch first {1} rows only)) ";
                string wSql = @" (right(t.�걾����,2) like '{0}%'
                                and t.�걾���� in
                                (select rr.�걾����
                                from t rr where  rr.�걾����=t.�걾���� and rr.�걾ԴID = t.�걾ԴID and rr.�������� = t.��������
                                order by rr.�걾���� desc 
                                fetch first {1} rows only)) ";
                string strTotal = string.Empty;
                if (Convert.ToInt32(this.LQty) > 0)
                {
                    wSql = string.Format(wSql, "P", this.LQty);
                }
                else
                {
                    wSql = string.Empty;
                }
                if (Convert.ToInt32(this.RQty) > 0)
                {
                    sSql = string.Format(sSql, "R", this.RQty);
                }
                else
                {
                    sSql = string.Empty;
                }
                if (Convert.ToInt32(this.DQty) > 0)
                {
                    pSql = string.Format(pSql, "D", this.DQty);
                }
                else
                {
                    pSql = string.Empty;
                }
                if (!string.IsNullOrEmpty(wSql))
                {
                    strTotal += wSql;
                }
                if (!string.IsNullOrEmpty(strTotal))
                {
                    if (!string.IsNullOrEmpty(sSql))
                    {
                        strTotal += " OR " + sSql;
                    }
                    else
                    {
                        strTotal += sSql;
                    }
                }
                else
                {
                    strTotal += sSql;
                }
                if (!string.IsNullOrEmpty(strTotal))
                {
                    if (!string.IsNullOrEmpty(pSql))
                    {
                        strTotal += " OR " + pSql;
                    }
                    else
                    {
                        strTotal += pSql;
                    }
                }
                else
                {
                    strTotal += pSql;
                }
                if (hsTr.Count > 0)
                {
                    string specCondition = string.Empty;
                    foreach (string s in hsTr.Values)
                    {
                        specCondition += "'" + s + "',";
                    }
                    specCondition = specCondition.Substring(0, specCondition.Length - 1);
                    this.SqlStr = " where  t.�������� in (" + specCondition + ") and (" + strTotal + ") order by t.�걾ԴID";
                }
                else
                {
                    this.SqlStr = " where " + strTotal + " order by t.�걾ԴID";
                }
                if (string.IsNullOrEmpty(this.SqlStr))
                {
                    MessageBox.Show("û�������κ�ɸѡ�������������Ӧ�걾������");
                }
                else
                {
                    this.DlRt = DialogResult.OK;
                    this.Close();
                }
                #endregion
            }
            catch
            {
                MessageBox.Show("�쳣����");
                return;
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DlRt = DialogResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.SqlStr = string.Empty;
            this.FilterSql = "1=1";
            this.DlRt = DialogResult.OK;
            this.Close();
        }

        private void cbOR_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbOR.Checked)
            {
                this.cbAND.Checked = false;
            }
            else
            {
                this.cbAND.Checked = true;
            }
        }

        private void cbAND_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAND.Checked)
            {
                this.cbOR.Checked = false;
            }
            else
            {
                this.cbOR.Checked = true;
            }
        }

        private void cbxZW_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxZW.Checked)
            {
                if (!this.hsTr.Contains(cbxZW.Text))
                {
                    this.hsTr.Add(cbxZW.Text, cbxZW.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxZW.Text))
                {
                    hsTr.Remove(cbxZW.Text);
                }
            }
        }

        private void cbxZC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxZC.Checked)
            {
                if (!this.hsTr.Contains(cbxZC.Text))
                {
                    this.hsTr.Add(cbxZC.Text, cbxZC.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxZC.Text))
                {
                    hsTr.Remove(cbxZC.Text);
                }
            }
        }

        private void cbxAP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAP.Checked)
            {
                if (!this.hsTr.Contains(cbxAP.Text))
                {
                    this.hsTr.Add(cbxAP.Text, cbxAP.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxAP.Text))
                {
                    hsTr.Remove(cbxAP.Text);
                }
            }
        }

        private void cbxZL_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxZL.Checked)
            {
                if (!this.hsTr.Contains(cbxZL.Text))
                {
                    this.hsTr.Add(cbxZL.Text, cbxZL.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxZL.Text))
                {
                    hsTr.Remove(cbxZL.Text);
                }
            }
        }

        private void cbxAS_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAS.Checked)
            {
                if (!this.hsTr.Contains(cbxAS.Text))
                {
                    this.hsTr.Add(cbxAS.Text, cbxAS.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxAS.Text))
                {
                    hsTr.Remove(cbxAS.Text);
                }
            }
        }

        private void cbxLBJ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxLBJ.Checked)
            {
                if (!this.hsTr.Contains(cbxLBJ.Text))
                {
                    this.hsTr.Add(cbxLBJ.Text, cbxLBJ.Text);
                }
            }
            else
            {
                if (hsTr.Contains(cbxLBJ.Text))
                {
                    hsTr.Remove(cbxLBJ.Text);
                }
            }
        }
        #endregion
    }
}