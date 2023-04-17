using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GZSI.Controls
{
    public partial class frmLoadSIData : Form
    {
		public frmLoadSIData()
		{
			InitializeComponent();
            btnLoad.Click += new EventHandler(btnLoad_Click);

            btnLoadYD.Click += new EventHandler(btnLoadYD_Click);//{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
		}

        LoadSIDataManager loadSIDataMgr = null;

        private void frmSameData_Load(object sender, System.EventArgs e)
        {
            loadSIDataMgr = new LoadSIDataManager();
            loadSIDataMgr.evLoadProgressChanged += new LoadSIDataManager.LoadProgressChanged(loadSIDataMgr_evLoadProgressChanged);
            loadSIDataMgr.evProgressCountChanged += new LoadSIDataManager.LoadProgressChanged(loadSIDataMgr_evProgressCountChanged);
           // btnStart.Enabled = false;
        }

        void loadSIDataMgr_evProgressCountChanged()
        {
            this.progressBar1.Maximum = loadSIDataMgr.ProgressCount;
        }

        void loadSIDataMgr_evLoadProgressChanged()
        {
            this.progressBar1.Value = loadSIDataMgr.LoadProgress;
        }

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
            if (DialogResult.Yes==MessageBox.Show("是否进行自动对照？","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question) )
            {        
                 this.btnClose.Enabled = false;
                this.lbInfo.Text = "正在自动对照医保项目...";
				this.loadSIDataMgr.AutoCompare();
                int ret= loadSIDataMgr.ExecGZYBCompare();
                if (ret == -1)
                {
                    System.Windows.Forms.MessageBox.Show("插入更新其他医保出错");
                }
				this.btnClose.Enabled = true;
            }
		}
        
        void btnLoad_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.lbInfo.Text = "正在下载医保数据，加载到本地...";
            loadSIDataMgr.LoadSIData();
            int ret = loadSIDataMgr.ExecGZYBSiitem();
            if (ret == -1)
            {
                System.Windows.Forms.MessageBox.Show("插入更新其他医保出错");
            }
            this.btnClose.Enabled = true;
        }

        //{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        void btnLoadYD_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.lbInfo.Text = "正在下载异地医保数据，加载到本地...";
            loadSIDataMgr.LoadYDSIData();
            int ret = loadSIDataMgr.ExecYDYBSiitem();
            if (ret == -1)
            {
                System.Windows.Forms.MessageBox.Show("插入更新其他医保出错");
            }
            this.btnClose.Enabled = true;
        }

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Q.GetHashCode())
			{
				this.loadSIDataMgr.AutoCompare();
				this.btnClose.Enabled = true;
			}
			return base.ProcessDialogKey (keyData);
        }

        private void btnAddCompare_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.lbInfo.Text = "正在增量匹配医保数据，请稍后...";
            this.loadSIDataMgr.AutoAddCompare();
            this.lbInfo.Text = "增量匹配医保数据已完成...";

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在更新其他设置,请稍等...");
            Application.DoEvents();

            try
            {
                int ret = loadSIDataMgr.ExecGZYBSiitem();

                if (ret == -1)
                {
                    System.Windows.Forms.MessageBox.Show("更新新增医保项目出错");
                }

                ret = loadSIDataMgr.ExecGZYBCompare();

                if (ret == -1)
                {
                    System.Windows.Forms.MessageBox.Show("插入更新其他医保出错");
                }
            }
            catch(Exception ex)
            {

            }
            
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("操作完成！");
            this.btnClose.Enabled = true;

        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 增量对照
        /// </summary>
        /// <returns></returns>
        private void btnAddCompareYD_Click(object sender, EventArgs e)
        {
            this.btnClose.Enabled = false;
            this.lbInfo.Text = "正在增量匹配异地医保数据，请稍后...";
            this.loadSIDataMgr.AutoAddCompareYD();
            this.lbInfo.Text = "增量匹配异地医保数据已完成...";
            int ret = loadSIDataMgr.ExecYDYBSiitem();

            if (ret == -1)
            {
                System.Windows.Forms.MessageBox.Show("插入更新其他医保出错");
            }

            ret = loadSIDataMgr.ExecYDYBCompare();    // 2017-01-11 17:47 修改到这。。。。。。。。
            if (ret == -1)
            {
                System.Windows.Forms.MessageBox.Show("插入扩展表错误");
            }

            this.btnClose.Enabled = true;

        }

	}
}
