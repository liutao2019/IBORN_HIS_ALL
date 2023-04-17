using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class frmBldFilter : Form
    {
        public frmBldFilter()
        {
            InitializeComponent();
        }

        #region 变量属性
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
        //"xj.storecnt >= {2}"     //血浆
        //"xq.storecnt >= {1}"     //血清
        //"xb.storecnt >= {0}"     //白细胞
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
        /// 白细胞数量
        /// </summary>
        private string wQty = "0";

        public string WQty
        {
            get
            {
                return this.wQty;
            }
            set
            {
                this.wQty = value;
            }
        }

        /// <summary>
        /// 血清数量
        /// </summary>
        private string sQty = "0";

        public string SQty
        {
            get
            {
                return this.sQty;
            }
            set
            {
                this.sQty = value;
            }
        }

        /// <summary>
        /// 血浆数量
        /// </summary>
        private string pQty = "0";

        public string PQty
        {
            get
            {
                return this.pQty;
            }
            set
            {
                this.pQty = value;
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
        private void frmBldFilter_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                #region 库存过滤条件赋值
                if (!string.IsNullOrEmpty(this.tbPRmQty.Text.Trim()))
                {
                    this.PQty = this.tbPRmQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbSRmQty.Text.Trim()))
                {
                    this.SQty = this.tbSRmQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbWRmQty.Text.Trim()))
                {
                    this.WQty = this.tbWRmQty.Text.Trim();
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

                string strFilter = @"( 细胞库存 >= {0} ) " + strLjXQ + " ( 血清库存 >= {1} ) " + strLjXJ +
                                 " ( 血浆库存 >= {2} )";
                this.FilterSql = string.Format(strFilter, this.WQty, this.SQty, this.PQty);
                #endregion

                #region 库存明细过滤条件赋值
                if (!string.IsNullOrEmpty(this.tbPQty.Text.Trim()))
                {
                    this.PQty = this.tbPQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbSQty.Text.Trim()))
                {
                    this.SQty = this.tbSQty.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbWQty.Text.Trim()))
                {
                    this.WQty = this.tbWQty.Text.Trim();
                }
                string sSql = @" (t.标本条码 like '%{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID
                                order by rr.标本条码 desc 
                                fetch first {1} rows only)) ";
                string pSql = @" (t.标本条码 like '%{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID
                                order by rr.标本条码 desc 
                                fetch first {1} rows only)) ";
                string wSql = @" (t.标本条码 like '%{0}%'
                                and t.标本条码 in
                                (select rr.标本条码
                                from t rr where  rr.标本类型=t.标本类型 and rr.标本源ID = t.标本源ID
                                order by rr.标本条码 desc 
                                fetch first {1} rows only)) ";
                string strTotal = string.Empty;
                if (Convert.ToInt32(this.WQty) > 0)
                {
                    wSql = string.Format(wSql, "W", this.WQty);
                }
                else
                {
                    wSql = string.Empty;
                }
                if (Convert.ToInt32(this.SQty) > 0)
                {
                    sSql = string.Format(sSql, "S", this.SQty);
                }
                else
                {
                    sSql = string.Empty;
                }
                if (Convert.ToInt32(this.PQty) > 0)
                {
                    pSql = string.Format(pSql, "P", this.PQty);
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
                this.SqlStr = " where " + strTotal + " order by t.标本源ID";
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
        #endregion
    }
}