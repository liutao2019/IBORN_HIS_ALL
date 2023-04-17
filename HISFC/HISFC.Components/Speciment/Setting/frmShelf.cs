using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmShelf : Form
    {
        #region 变量域
        private ShelfSpec specTemp = new ShelfSpec();
        private ShelfSpecManage shelfManage = new ShelfSpecManage();
        private BoxSpecManage boxspecManage = new BoxSpecManage();
        private string Error;
        #endregion

        #region 私有方法
        /// <summary>
        /// 从页面上获取要添加的架子规格信息
        /// </summary>
        /// <returns> 架子规格实体</returns>
        private ShelfSpec ShelfSpecInfo()
        {
            string sequence ="";
            shelfManage.GetNextSequence(ref sequence);
            specTemp.ShelfSpecID = Convert.ToInt32(sequence);
            specTemp.ShelfSpecName = txtName.Text.ToString();
            specTemp.Row = Convert.ToInt32(nudRow.Value);
            specTemp.Col = 1;
            specTemp.Height = Convert.ToInt32(nudHeight.Value);
            specTemp.BoxSpec.BoxSpecID = Convert.ToInt32(cmbBoxSpec.SelectedValue);
            specTemp.Comment = txtComment.Text;
            return specTemp;
        }

        /// <summary>
        /// 保存架子规格
        /// </summary>
        /// <returns>-1：保存失败，其它：成功</returns>
        private int SaveShelfSpec()
        {
            try
            {
                ShelfSpecInfo();
                return shelfManage.InsertShelfSpec(specTemp);
            }
            catch (Exception e)
            {
                Error = e.Message;
                return -1;
            }

        }

        private bool DataValidte()
        {
            if (nudRow.Value <= 0)
            {
                return false;
            }
            if (nudHeight.Value <= 0)
                return false;
            if (cmbBoxSpec.SelectedValue == null || cmbBoxSpec.Text.Trim() == "")
                return false;
            return true;
 
        }
        #endregion

        #region 公有方法及事件
        public frmShelf()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!DataValidte())
            {
                MessageBox.Show("输入数据不合格，请检查！", "冻存架规格添加");
                return;
            }
            int result = SaveShelfSpec();
            if (result != -1)
            {
                MessageBox.Show("保存成功", "冻存架规格添加");
            }
            else
            {
                MessageBox.Show("保存失败", "冻存架规格添加");
            }
        }
        #endregion

        /// <summary>
        /// 绑定标本盒规格
        /// </summary>
        private void ComboxBinding()
        {
            Dictionary<int, string> dicSpec = boxspecManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dicSpec;
            this.cmbBoxSpec.DisplayMember = "Value";
            this.cmbBoxSpec.ValueMember = "Key";
            this.cmbBoxSpec.DataSource = bsTemp;
        }

        private void frmShelf_Load(object sender, EventArgs e)
        {
            ComboxBinding();
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //查询冰箱类型列表
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");            
            this.txtIceBoxType.AddItems(iceBoxTypeList);
        }

        private void cmbBoxSpec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIceBoxType_TextChanged(object sender, EventArgs e)
        {
            if (txtIceBoxType.Tag == null || txtIceBoxType.Text == "")
                return;
            else
            {
                if (txtIceBoxType.Tag.ToString() == "1")
                {
                    txtName.Text = "立式冷冻柜架规格";
                    txtName.Enabled = true;
                    nudRow.Enabled = true;
                    nudHeight.Enabled = true;
                    btnSave.Enabled = true;
                    btnAdd.Enabled = true;
                }
                if (txtIceBoxType.Tag.ToString() == "2")
                {
                    txtName.Enabled = false;
                    nudRow.Enabled = false;
                    nudHeight.Enabled = false;
                    btnSave.Enabled = false;
                    btnAdd.Enabled = false;
                }
                if (txtIceBoxType.Tag.ToString() == "3")
                {
                    txtName.Text = "液氮罐冷冻架规格";
                    txtName.Enabled = true;
                    nudRow.Enabled = false;
                    nudRow.Value = 1;
                    nudHeight.Enabled = true;
                    btnSave.Enabled = true;
                    btnAdd.Enabled = true;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtIceBoxType.Text = "";
            txtIceBoxType.Tag = null; 
        }

    }
}