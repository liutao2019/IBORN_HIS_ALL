using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY
{
	/// <summary>
	/// 检查申请单窗体
	/// </summary>
	partial class frmPacsApplyForClinic :FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        #region 事件

        /// <summary>
		/// ToolBar1的按钮触发事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.tbCancel)
			{
				if(this.ucPacsApplyForClinic1.SetUnvalid() >= 0)
				{
					MessageBox.Show("取消成功","提示");
				}
				else
				{
					MessageBox.Show("取消失败","提示");
				}
			}
			if(e.Button == this.tbSave)
			{
				this.ucPacsApplyForClinic1.Save();
			}
			else if(e.Button == this.tbPrint)
			{
				this.ucPacsApplyForClinic1.Print();
			}
			else if(e.Button == this.tbPrintView)
			{
				this.ucPacsApplyForClinic1.PrintPreview();
			}
			else if(e.Button == this.tbExit)
			{
				this.ucPacsApplyForClinic1.Exit();
			}
			else if(e.Button == this.tbRefresh)
			{
				if(this.tbRefresh.Pushed)
				{
					this.tbRefresh.Text = "自动刷新";
					this.ucPacsApplyForClinic1.Refresh(false);
				}
				else
				{
					this.tbRefresh.Text = "停止刷新";
					this.ucPacsApplyForClinic1.Refresh(true);
				}
			}
			else if(e.Button == this.tbDate)
			{
				DateTime dtBegin = this.ucPacsApplyForClinic1.DtBegin;
				DateTime dtEnd = this.ucPacsApplyForClinic1.DtEnd;

				FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref dtBegin,ref dtEnd);

				this.ucPacsApplyForClinic1.DtBegin = dtBegin;
				this.ucPacsApplyForClinic1.DtEnd = dtEnd;

				//this.ucPacsApplyForClinic1.InitPacsInfo();
			}
		}
		/// <summary>
		/// 检查申请单Load函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmPacsApply_Load(object sender, System.EventArgs e)
		{
			try
			{
				if(this.ucPacsApplyForClinic1 == null)
				{
					ucPacsApplyForClinic1 = new ucPacsApplyForClinic();
					ucPacsApplyForClinic1.Dock = System.Windows.Forms.DockStyle.Fill;
					this.Controls.Add(ucPacsApplyForClinic1);
					ucPacsApplyForClinic1.Dock = DockStyle.Fill;
					ucPacsApplyForClinic1.BringToFront();
					ucPacsApplyForClinic1.Visible = true;
					this.ucPacsApplyForClinic1.alItems = this.alItems;
					this.ucPacsApplyForClinic1.reg = this.reg;
				}

				if(this.Tag != null&& this.Tag.ToString() == "1")
				{
					this.tbCancel.Visible = false;
					this.tbRefresh.Visible = true;
				}
				else
				{
					this.tbDate.Visible = false;
					this.tbRefresh.Visible = false;
				}
			}
			catch
            {

            }
        }

        #endregion
    }
}
