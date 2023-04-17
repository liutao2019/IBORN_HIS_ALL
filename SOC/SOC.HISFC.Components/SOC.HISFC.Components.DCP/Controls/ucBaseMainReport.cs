using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucBaseMainReport<br></br>
    /// [功能描述: 基类报卡uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucBaseMainReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBaseMainReport()
        {
            InitializeComponent();
        }

        public DateTime sysdate = DateTime.Now;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        public virtual int Init(DateTime sysdate)
        {
            this.sysdate = sysdate;

            return 1;
        }

        /// <summary>
        /// 赋值报卡信息（通用）
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public virtual int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            return 1;
        }

        /// <summary>
        /// 获取报卡信息（通用）
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public virtual int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            return 1;
        }

        public virtual int GetValue(ref FS.HISFC.DCP.Object.CommonReport report, OperType opertype)
        {
            return 1;
        }

        /// <summary>
        /// 赋值患者信息，主要是用于患者信息UC
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patientType">C 门诊 I住院 O 其他</param>
        /// <returns></returns>
        public virtual int SetValue(FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            return 1;
        }

        public virtual int SetControlEnable(bool enable)
        {
            return 1;
        }

        public virtual void Clear()
        {

        }

        public virtual void PrePrint()
        {
            
        }

        public virtual void Printed()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            this.Click(this);
             base.OnLoad(e);
        }

        private void Click(Control mainControl)
        {
            foreach (Control c in mainControl.Controls)
            {
                if (c.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    c.Click -= new EventHandler(c_Click);
                    c.Click += new EventHandler(c_Click);
                }

                if(c.Controls.Count > 0)
                {
                    this.Click(c);
                }
            }
        }

        void c_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                ((Control)sender).Select();
                ((Control)sender).Focus();
            }
        }
    }
}
