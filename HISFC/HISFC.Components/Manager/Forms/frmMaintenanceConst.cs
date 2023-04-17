using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;

namespace FS.FrameWork.WinForms.Forms
{
    public partial class frmMaintenanceConst : frmQuery
    {
        public frmMaintenanceConst()
        {
            InitializeComponent();
        }

        #region 字段
        private ucMaintenanceConst maintenance;
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            this.maintenance = new ucMaintenanceConst();
            this.MaintenanceControl = this.maintenance;
            base.OnLoad(e);

            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            string IsCanDelete = managerMgr.QueryControlerInfo("C00075");　//是否需要加载药品信息
            if (IsCanDelete != null)
            {
                if (!FS.FrameWork.Public.String.StringEqual(IsCanDelete, "1"))
                {
                    base.ShowDeleteButton = false;
                }
            }
            //base.ShowDeleteButton = false;
        }
        #endregion
    }
}