using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [模块描述: 实现自定义系统数据问题监控功能]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-9-13]
    /// [备    注: 校验视图对应的SQL语句需要以 ListenView. 开头，并写到COM_SQL表中]
    /// </summary>
    public partial class ucSystemListen : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSystemListen()
        {
            InitializeComponent();
        }

        private SystemListienClass slc = new SystemListienClass();

        private void neuButton1_Click(object sender, EventArgs e)
        {
            if (slc.CreatListenSeq(tbName.Text, tbIndex.Text, tbID.Text) == -1)
            {
                MessageBox.Show("添加监控失败!");
                return;
            }

            MessageBox.Show("添加成功!");
            this.QueryListenSeq();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            if (slc.DeleteListenByID(tbID.Text) == -1)
            {
                MessageBox.Show("删除监控失败!");
                return;
            }

            MessageBox.Show("删除成功!");
            this.QueryListenSeq();
        }

        private void neuButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string listenReturn = this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = listenReturn;//this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                if (listenReturn == "正常")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else if (listenReturn == "0")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "█";
                    this.neuSpread1_Sheet1.Cells[i, 1].ForeColor = Color.Red;
                }
            }

            timer1.Interval = Convert.ToInt32(this.tbTime.Text) * 1000;
            this.timer1.Enabled = true;
            this.neuButton3.Enabled = false;
            this.neuButton4.Enabled = true;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.tbID.Text = this.neuSpread1_Sheet1.Cells[e.Row, 1].Text;
            this.tbIndex.Text = this.neuSpread1_Sheet1.Cells[e.Row, 4].Text;
            this.tbName.Text = this.neuSpread1_Sheet1.Cells[e.Row, 2].Text;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.QueryListenSeq();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                Application.DoEvents();
                string listenReturn = this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = listenReturn;//this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                if (listenReturn == "正常")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else if (listenReturn == "0")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "█";
                    this.neuSpread1_Sheet1.Cells[i, 1].ForeColor = Color.Red;
                }
            }
            return base.OnQuery(sender, neuObject);
        }

        public void QueryListenSeq()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            ArrayList altemp = this.slc.GetListenSeq();
            if (altemp != null && altemp.Count != 0)
            {
                for (int i = 0; i < altemp.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject lsObj = altemp[i] as FS.FrameWork.Models.NeuObject;
                    this.neuSpread1_Sheet1.RowCount = i + 1;
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = lsObj.ID;
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = lsObj.Name;
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = lsObj.Memo;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                Application.DoEvents();
                string listenReturn = this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = listenReturn;//this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                if (listenReturn == "正常")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else if (listenReturn == "0")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "█";
                    this.neuSpread1_Sheet1.Cells[i, 1].ForeColor = Color.Red;
                }
            }
        }

        private void neuButton4_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.neuButton3.Enabled = true;
            this.neuButton4.Enabled = false;
        }

        private void neuButton5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                Application.DoEvents();
                string listenReturn = this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = listenReturn;//this.slc.ListenExec(this.neuSpread1_Sheet1.Cells[i, 4].Text);
                if (listenReturn == "正常")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else if (listenReturn == "0")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = "█";
                    this.neuSpread1_Sheet1.Cells[i, 1].ForeColor = Color.Red;
                }
            }
        }

        private void neuRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (neuRadioButton2.Checked)
            {
                this.neuPanel2.Visible = true;
            }
            else
            {
                this.neuPanel2.Visible = false;
            }
        }
    }

    public class SystemListienClass : FS.FrameWork.Management.Database
    {
        public int CreatListenSeq(string lsName, string lsIndex, string lsID)
        {
            string strSQL = "insert into s_t_met1(met_code,met_name,id) values('{0}','{1}',{2})";
            strSQL = string.Format(strSQL, lsName, lsIndex, lsID);

            return this.ExecNoQuery(strSQL);
        }

        public int DeleteListenByID(string lsID)
        {
            string strSQL = "delete from s_t_met1 where id={0}";
            strSQL = string.Format(strSQL, lsID);

            return this.ExecNoQuery(strSQL);
        }

        public string ListenExec(string listenIndex)
        {
            string sqlStr = "";
            if (this.Sql.GetSql(listenIndex, ref sqlStr) == -1)
            {
                this.Err = "没有找到" + listenIndex + "字段!";
                return null;
            }
            return this.ExecSqlReturnOne(sqlStr);
        }

        public ArrayList GetListenSeq()
        {
            string strSql = "select id,name,memo from com_sql where id like 'ListenView.%'";
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "加载监控队列出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();
                    info.Memo = this.Reader[2].ToString();
                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "加载监控队列出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
    }
}
