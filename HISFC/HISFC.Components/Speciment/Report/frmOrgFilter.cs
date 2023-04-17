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

        #region 变量属性
        /// <summary>
        /// 肿物性质
        /// </summary>
        private Hashtable hsTr = new Hashtable();

        /// <summary>
        /// 筛选sql语句(用于加载至详细信息中)
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
        /// 库存信息过滤，判断符合条件的库存信息
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
        /// DNA数量
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
        /// RNA数量
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
        /// 蜡块数量
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

        #region 方法
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

        #region 事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                #region 库存过滤条件赋值
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

                string strFilter = @"( DNA库存 >= {0} ) " + strLjXQ + " ( RNA库存 >= {1} ) " + strLjXJ +
                                 " ( 蜡块库存 >= {2} )";
                this.FilterSql = string.Format(strFilter, this.DQty, this.RQty, this.LQty);
                #endregion

                #region 库存明细过滤条件赋值
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
                string sSql = @" (right(t.标本条码,2) like '{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID and rr.肿物性质 = t.肿物性质
                                order by rr.标本条码 desc 
                                fetch first {1} rows only)) ";
                string pSql = @" (right(t.标本条码,2) like '{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID and rr.肿物性质 = t.肿物性质
                                order by rr.标本条码 desc 
                                fetch first {1} rows only)) ";
                string wSql = @" (right(t.标本条码,2) like '{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID and rr.肿物性质 = t.肿物性质
                                order by rr.标本条码 desc 
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
                    this.SqlStr = " where  t.肿物性质 in (" + specCondition + ") and (" + strTotal + ") order by t.标本源ID";
                }
                else
                {
                    this.SqlStr = " where " + strTotal + " order by t.标本源ID";
                }
                if (string.IsNullOrEmpty(this.SqlStr))
                {
                    MessageBox.Show("没有输入任何筛选条件，请输入对应标本数量！");
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
                MessageBox.Show("异常错误！");
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