using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 延长队列时间-叫号系统使用{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
    /// </summary>
    public partial class ucExtendQueueTime : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数
        public ucExtendQueueTime()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量

        /// <summary>
        /// 是否延长队列时间
        /// </summary>
        DialogResult dr = DialogResult.Cancel;

        /// <summary>
        /// 延长时间
        /// </summary>
        double extendTime = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 是否延长队列时间
        /// </summary>
        public DialogResult Dr
        {
            get { return dr; }
            set { dr = value; }
        }

        /// <summary>
        /// 延长时间
        /// </summary>
        public double ExtendTime
        {
            get { return extendTime; }
            set { extendTime = value; }
        }

        #endregion

        #region 方法

      

        #endregion

        #region 事件

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.extendTime < 0)
                this.extendTime = 0;

            this.nudExtendTime.Value = FS.FrameWork.Function.NConvert.ToDecimal(this.extendTime);
            base.OnLoad(e);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Management.DataBaseManger dateMgr = new FS.FrameWork.Management.DataBaseManger();
            DateTime dtNow = dateMgr.GetDateTimeFromSysDateTime();
            if (Classes.Function.CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", this.nudExtendTime.Value.ToString(), dtNow.ToString()) == -1)
            {
                return;
            }
            this.extendTime = double.Parse(this.nudExtendTime.Value.ToString());
            dr = DialogResult.OK;
            this.ParentForm.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dr = DialogResult.Cancel;
            this.ParentForm.Close();
        }

        #endregion
    }
}
