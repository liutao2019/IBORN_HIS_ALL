using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Assign.Components.Base
{
    /// <summary>
    /// [功能描述: 分诊基类，用于继承权限科室和护士站]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucAssignBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAssignBase()
        {
            InitializeComponent();
        }

        protected FS.FrameWork.Models.NeuObject priveDept;

        /// <summary>
        /// 权限科室，当前获取、显示数据的科室
        /// </summary>
        [Description("权限科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveDept
        {
            get { return priveDept; }
            set { priveDept = value; }
        }

        protected FS.FrameWork.Models.NeuObject priveNurse;

        /// <summary>
        /// 权限科室，当前获取、显示数据的科室
        /// </summary>
        [Description("权限护士站"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveNurse
        {
            get { return priveNurse; }
            set { priveNurse = value; }
        }

        protected bool isCheckPrivePower = false;

        /// <summary>
        /// 是否判断发药权限
        /// </summary>
        [Description("是否判断分诊权限"), Category("设置"), Browsable(true)]
        public bool IsCheckPrivePower
        {
            get { return isCheckPrivePower; }
            set { isCheckPrivePower = value; }
        }

        protected string privePowerString = "1401+01";

        [Description("使用窗口需要的权限,如：1401+01"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }
    }
}
