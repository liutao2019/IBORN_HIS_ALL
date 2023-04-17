using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Project
{
    public partial class frmFilterSetting : Form
    {
        public frmFilterSetting()
        {
            InitializeComponent();

            this.btOK.Click += new EventHandler(btOK_Click);
        }

        public delegate void InputCompletedHander();
        public InputCompletedHander InputCompletedEven;

        private string curSettingFileName = "";

        void btOK_Click(object sender, EventArgs e)
        {
            //保存到配置文件
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                if (node.Checked)
                {
                    SOC.Public.XML.SettingFile.SaveSetting(this.curSettingFileName, "FilterSetting", "S" + node.Text, "True");
                }
                else
                {
                    SOC.Public.XML.SettingFile.SaveSetting(this.curSettingFileName, "FilterSetting", "S" + node.Text, "False");
                }
            }

            if (this.InputCompletedEven != null)
            {
                this.InputCompletedEven();
            }

            this.DialogResult = DialogResult.OK;
        }

        public int SetFilterStruct(string filters, string settingFileName)
        {
            this.treeView1.Nodes.Clear();
            this.curSettingFileName = settingFileName;

            this.treeView1.CheckBoxes = true;
            string[] filterArray = filters.Split(',');
            foreach (string filterField in filterArray)
            {
                TreeNode node = new TreeNode();
                node.Text = filterField;
                this.treeView1.Nodes.Add(node);

                if (System.IO.File.Exists(this.curSettingFileName))
                {
                    string checkState = SOC.Public.XML.SettingFile.ReadSetting(this.curSettingFileName, "FilterSetting", "S" + filterField, "False");
                    node.Checked = FS.FrameWork.Function.NConvert.ToBoolean(checkState);
                }
            }

            return 1;
        }
    }
}
