using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucBackPage : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBackPage()
        {
            InitializeComponent();
        }

        private void ucBackPage_Load(object sender, EventArgs e)
        {

        }

        #region 属性与变量
        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinic_code = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string Clinic_code
        {
            get { return clinic_code; }
            set { clinic_code = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 从界面获取值
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetValue()
        {
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            CheckBox cb = new CheckBox();
            TextBox tb = new TextBox();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is System.Windows.Forms.CheckBox)
                {
                    cb = (System.Windows.Forms.CheckBox)c;
                    if (cb.Checked)
                    {
                        obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = this.clinic_code;
                        obj.Name = cb.Name;
                        obj.Memo = cb.Text;
                        obj.User01 = "BG";
                        obj.User02 = cb.GetType().ToString();
                        al.Add(obj);
                    }
                }
                else if (c is System.Windows.Forms.TextBox)
                {
                    tb = (System.Windows.Forms.TextBox)c;
                    if (!string.IsNullOrEmpty(tb.Text.Trim()))
                    {
                        obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = this.clinic_code;
                        obj.Name = tb.Name;
                        obj.Memo = tb.Text;
                        obj.User01 = "BG";
                        obj.User02 = tb.GetType().ToString();
                        al.Add(obj);
                    }
                }
            }
            return al;
        }

        /// <summary>
        /// 赋值到界面
        /// 传入参数必须user01为D1
        /// </summary>
        public void SetValue(System.Collections.Hashtable hsTemp)
        {
            FS.FrameWork.Models.NeuObject obj = null;
            CheckBox cb = new CheckBox();
            TextBox tb = new TextBox();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (hsTemp.Contains("BG" + c.Name))
                {
                    if (c is System.Windows.Forms.CheckBox)
                    {
                        cb = (System.Windows.Forms.CheckBox)c;
                        cb.Checked = true;
                    }
                    else if (c is System.Windows.Forms.TextBox)
                    {
                        tb = (System.Windows.Forms.TextBox)c;
                        obj = hsTemp["BG" + c.Name] as FS.FrameWork.Models.NeuObject;
                        tb.Text = obj.Memo;
                    }
                }
            }
        }

        public void Clean()
        {
            CheckBox cb = new CheckBox();
            TextBox tb = new TextBox();
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c is System.Windows.Forms.CheckBox)
                {
                    cb = (System.Windows.Forms.CheckBox)c;
                    cb.Checked = false;
                }
                else if (c is System.Windows.Forms.TextBox)
                {
                    tb = (System.Windows.Forms.TextBox)c;
                    tb.Text = "";
                }
            }
        }
        #endregion 
    }
}
