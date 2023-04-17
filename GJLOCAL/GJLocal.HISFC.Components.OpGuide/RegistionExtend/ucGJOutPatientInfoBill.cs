using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucGJOutPatientInfoBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //{AB392EE7-0666-4456-B29F-458730318812}
        public ucGJOutPatientInfoBill()
        {
            InitializeComponent();
            this.ucMain2.AutoScroll = true;
        }

        #region 变量与属性


        #region 业务变量
        FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                    = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
        #endregion
        #endregion


        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 数节点查询
        /// </summary>
        private void Query()
        {
            this.Clean();
            this.neuTreeView1.Nodes.Clear();

            string sql = "";
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                sql = @"
                  select to_char(r.reg_date,'yyyy-MM-dd'),
                    r.clinic_code,
                    r.name
                    from fin_opr_register r
                    where r.reg_date between to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and  to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    ";
                sql = string.Format(sql, this.ndtpBegin.Value.Date.ToString(), this.ndtpEnd.Value.AddDays(1).Date.ToString());
            }
            else
            {
                sql = @"
                  select to_char(r.reg_date,'yyyy-MM-dd'),
                    r.clinic_code,
                    r.name
                    from fin_opr_register r,fin_opb_accountcard_record a
                    where (r.card_no like '%{0}' or a.markno like '%{0}')
                    and r.card_no=a.card_no
                    ";
                sql = string.Format(sql, this.textBox1.Text.Trim());
            }
            
            DataSet dsRes = new DataSet();

            this.gjMgr.ExecQuery(sql, ref dsRes);

            Hashtable hsDate = new Hashtable();

            foreach (DataRow row in dsRes.Tables[0].Rows)
            {
                if (!hsDate.Contains(row[0].ToString()))
                {
                    TreeNode nodeDate = new TreeNode();
                    nodeDate.Text = row[0].ToString();

                    TreeNode nodeRep = new TreeNode();
                    nodeRep.Text = "患者:" + row[2].ToString();
                    nodeRep.Tag = row[1].ToString();

                    nodeDate.Nodes.Add(nodeRep);

                    this.neuTreeView1.Nodes.Add(nodeDate);
                    hsDate.Add(row[0].ToString(), nodeDate);
                }
                else
                {
                    TreeNode nodeDate = hsDate[row[0].ToString()] as TreeNode;

                    TreeNode nodeRep = new TreeNode();
                    nodeRep.Text = "患者:" + row[2].ToString();
                    nodeRep.Tag = row[1].ToString();

                    nodeDate.Nodes.Add(nodeRep);
                }
            }

            this.neuTreeView1.ExpandAll();
        }

        /// <summary>
        /// 清除
        /// </summary>
        private void Clean()
        {
            this.ucMain2.Clean();
        }

        private void neuTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains("患者"))
            {
                this.ucMain2.Clean();
                this.ucMain2.DtReg = e.Node.Parent.Text;
                this.ucMain2.Query(e.Node.Tag.ToString(), true);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                this.Query();
            }
        }
    }
}
