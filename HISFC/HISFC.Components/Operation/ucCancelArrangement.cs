using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// 已安排手术集中作废界面
    /// cao-lin
    /// </summary>
    public partial class ucCancelArrangement : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCancelArrangement()
        {
            InitializeComponent();
        }

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 工具栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("作废手术", "作废", FS.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, true, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 工具栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "作废手术")
            {
                this.ucArrangementSpread1.CancelOperation();
                this.QueryAllCanCelApplication();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            QueryAllCanCelApplication();
            base.OnLoad(e);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            QueryAllCanCelApplication();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 刷新手术申请列表
        /// </summary>
        /// <returns></returns>
        public int RefreshApplys()
        {

            this.ucArrangementSpread1.Reset();
            DateTime dt = Environment.OperationManager.GetDateTimeFromSysDateTime();
            //开始时间
            DateTime beginTime = DateTime.MinValue;
            DateTime endTime = dt.Date.AddDays(1);
            
            Application.DoEvents();
            ArrayList alApplys;
            try
            {
                this.ucArrangementSpread1.Reset();
                alApplys = Environment.OperationManager.GetCancelAppList(Environment.OperatorDeptID, beginTime, endTime);
                if (alApplys != null)
                {
                    foreach (OperationAppllication apply in alApplys)
                    {
                        this.ucArrangementSpread1.AddOperationApplication(apply);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("生成手术申请信息出错!" + e.Message, "提示");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 查询所有已安排且发起作废申请的手术列表
        /// </summary>
        private void QueryAllCanCelApplication()
        {
            RefreshApplys();
        }
    }
}
