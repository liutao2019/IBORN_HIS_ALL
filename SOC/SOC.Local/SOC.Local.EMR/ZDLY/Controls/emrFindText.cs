using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Neusoft.SOC.Local.EMR.ZDLY.Controls
{
    public partial class emrFindText :System.Windows.Forms.TextBox
    {
        string strHint = "请输入查找条件";

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("东软自定义控件")]
        [Description("设置提示输入的查找条件")]
        public string HintStr
        {
            get { return strHint; }
            set { strHint = value; }
        }


        public emrFindText()
        {
            InitializeComponent();
        }

        public emrFindText(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!DesignMode)
            {
                this.Text = strHint;
                this.ForeColor = Color.Gray;
            }

            base.OnLayout(levent);
        }


        protected override void OnEnter(EventArgs e)
        {
            if (this.Text == strHint)
            {
                this.Text = string.Empty;
                this.ForeColor = Color.Black;
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (this.Text == string.Empty)
            {
                this.Text = strHint;
                this.ForeColor = Color.Gray;
            }

            base.OnLeave(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.strHint) && this.Text.Equals(this.strHint))
            {
                return;
            }
            base.OnTextChanged(e);
        }

    }
}
