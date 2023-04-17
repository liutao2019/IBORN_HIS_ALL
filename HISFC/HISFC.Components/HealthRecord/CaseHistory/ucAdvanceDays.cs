using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucAdvanceDays : UserControl
    {
        public ucAdvanceDays()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// 我的自定义委托
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="myEventArgs"></param>
        public delegate void CustomEvent(FS.FrameWork.Models.NeuObject msg, EventArgs myEventArgs);
        /// <summary>
        /// 自定义事件 使用时再修改参数
        /// </summary>
        public event CustomEvent CustomEventHandler = null;

        private string text = string.Empty;

        /// <summary>
        /// 验证输入字符串中只有数字
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>true 只有数字 false 包含除数字外字符</returns>
        public bool IsValidString(string str)
        {
            return Regex.IsMatch(str, @"^(-?\d+)(\.\d+)?$");//(str, @"^[0-9\u4e00-\u9fa5]+$");
        }

        
        /// <summary>
        /// 获取提前的天数
        /// </summary>
        public int AdvanceDays
        {
            get 
            {
                //验证输入有效性
                return Convert.ToInt32(this.neuTextBox1.Text.Trim());
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsValidString(this.neuTextBox1.Text.Trim()))
            {
                //MessageBox.Show("请输入数字", "提示");

                this.neuTextBox1.Clear();
                return;
            }
        }


    }
}
