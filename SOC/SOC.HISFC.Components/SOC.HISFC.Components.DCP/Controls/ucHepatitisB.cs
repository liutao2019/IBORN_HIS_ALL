using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// 乙肝附卡
    /// </summary>
    public partial class ucHepatitisB : ucBaseAddition
    {
        public ucHepatitisB()
        {
            InitializeComponent();
            this.Init();
        }

        #region 初始化

        /// <summary>
        /// 初始化信息
        /// </summary>
        public void Init()
        {
        }

        #endregion

        #region 方法

        /// <summary>
        /// 验证附卡信息是否完整
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>-1 不完整，1 完整</returns>
        public override int IsValid(ref string msg)
        {
            int ret = 1;
           
            return ret;
        }

        public override void PrePrint()
        {

            this.groupBox1.BackColor = Color.White;
            this.BackColor = Color.White; 
            base.PrePrint();
        }

        public override void Printed()
        {
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            base.Printed();
        }

        public override void Clear()
        {
            this.neuRadioButton5.Checked = true;
            this.checkBox1.Checked = true;
            this.neuDateTimePicker2.Value = DateTime.Now.Date;
            this.textBox1.Text = "";
            this.radioButton8.Checked = true;
            this.neuRadioButton8.Checked = true;
            this.neuRadioButton11.Checked = true;
            base.Clear();
        }
        #endregion

    }
}
