using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.NurseStation.Common
{
    /// <summary>
    /// 医嘱执行情况查询
    /// </summary>
    public partial class ucOrderExecQuery : UserControl
    {
        public ucOrderExecQuery()
        {
            InitializeComponent();
        }

        private void fpOrderExec_ColumnDragMoveCompleted(object sender, FarPoint.Win.Spread.DragMoveCompletedEventArgs e)
        {
            this.fpOrderExec.SaveSchema(FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\dd");
        }

        protected override void OnLoad(EventArgs e)
        {
            this.fpOrderExec.ReadSchema(FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\dd");
            base.OnLoad(e);
        }
    }

    /// <summary>
    /// 医嘱执行情况查询列设置
    /// </summary>
    enum EnumExecOrderColSet
    {

    }
}
