using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmBox : Form
    {
        #region 变量域
        private BoxSpec specTemp;
        private BoxSpecManage specManage;
        Dictionary<int, string> dicBoxSpec;
        private string Error;
        private string title;
        #endregion

        #region 私有方法
        /// <summary>
        /// 从页面上获取标本盒规格
        /// </summary>
        /// <returns></returns>
        private string BoxSpecInfo()
        {
            string name = txtName.Text.TrimEnd().TrimStart();
            specTemp.Col = Convert.ToInt32(nudCol.Value);
            specTemp.Row = Convert.ToInt32(nudRow.Value);
            specTemp.Comment = txtComment.Text;
            if (dicBoxSpec.ContainsValue(name) && cmbSpec.Text != name)
            {
                MessageBox.Show("名称重复，请重新设置！", title);
                return "";
            }
            specTemp.BoxSpecName = name;
            return "Insert";
        }

        /// <summary>
        /// 保存标本盒规格
        /// </summary>
        /// <returns></returns>
        private int SaveSpecInfo()
        {
            try
            {
                string result = BoxSpecInfo();
                switch (result)
                {
                    case "":
                        return 0;
                    default:
                        string sequence = "";
                        specManage.GetNextSequence(ref sequence);
                        specTemp.BoxSpecID = Convert.ToInt32(sequence);
                        return specManage.InsertBoxSpec(specTemp);
                }

            }
            catch (Exception e)
            {
                Error = e.Message;
                return -1;
            }

        }

        /// <summary>
        /// 标本盒规格绑定
        /// </summary>
        private void BoxBinding()
        {
            dicBoxSpec = specManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dicBoxSpec;
            this.cmbSpec.DisplayMember = "Value";
            this.cmbSpec.ValueMember = "Key";
            this.cmbSpec.DataSource = bsTemp;
        }
        #endregion

        #region 构造函数
        public frmBox()
        {
            InitializeComponent();
            specTemp = new BoxSpec();
            specManage = new BoxSpecManage();
            dicBoxSpec = new Dictionary<int, string>();
            title = "标本盒规格设置";
        }
        #endregion

        #region 事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            string btnText = btnSave.Text;
            if (btnText == "保存")
            {
                int save = SaveSpecInfo();
                if (save == 0)
                {
                    return; 
                }
                if (save != -1)
                {
                    MessageBox.Show("保存成功！", title);
                    BoxBinding();
                }
                if (save == -1)
                {
                    MessageBox.Show("保存失败！", title);
                }
                btnSave.Text = "添加";
            }
            else
            {
                this.txtName.Text = "";
                this.btnSave.Text = "保存";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
        #endregion

        private void nudCol_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmBox_Load(object sender, EventArgs e)
        {
            BoxBinding();            
        }

        private void cmbSpec_SelectedIndexChanged(object sender, EventArgs e)
        {            
            BoxSpec spec = specManage.GetSpecById(cmbSpec.SelectedValue.ToString());
            txtName.Text = spec.BoxSpecName;
            txtComment.Text = spec.Comment;
            nudCol.Value = spec.Col;
            nudRow.Value = spec.Row;
        }

    }
}