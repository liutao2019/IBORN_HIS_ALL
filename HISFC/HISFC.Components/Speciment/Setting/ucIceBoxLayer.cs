using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucIceBoxLayer : UserControl
    {

        #region 变量域
        private ShelfSpecManage shelfManage = new ShelfSpecManage();
        private BoxSpecManage boxspecManage = new BoxSpecManage();
        private DisTypeManage disTypeManage = new DisTypeManage();
        private Dictionary<int, string> dicOrgType;
        private Dictionary<int, string> dicSpecType;
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private string iceTypeId;
        #endregion

        public string IceTypeId
        {
            get
            {
                return iceTypeId;
            }
            set
            {
                iceTypeId=value;
            }
        }

        #region 构造方法
        public ucIceBoxLayer(string iceType)
        {
            InitializeComponent();
            nudCol.Enabled = false;
            nudHeight.Enabled = false;
            nudRow.Enabled = false;
            cmbSpec.Enabled = false;
            dicOrgType = new Dictionary<int, string>();
            dicSpecType = new Dictionary<int, string>();           
            orgTypeManage = new OrgTypeManage();
            specTypeManage = new SpecTypeManage();
            iceTypeId = iceType;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 绑定标本种类
        /// </summary>
        private void BindingSpecClass()
        {
            dicOrgType = orgTypeManage.GetAllOrgType();
            if (dicOrgType.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicOrgType;
                cmbOrgOrBlood.DisplayMember = "Value";
                cmbOrgOrBlood.ValueMember = "Key";
                cmbOrgOrBlood.DataSource = bsTmp;
            }
        }

        /// <summary>
        /// 绑定标本类型
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType(string orgId)
        {
            dicSpecType.Clear();
            cmbSpecType.DataSource = null;
            dicSpecType = specTypeManage.GetSpecTypeByOrgID(orgId);
            if (dicSpecType.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = dicSpecType;
                cmbSpecType.DisplayMember = "Value";
                cmbSpecType.ValueMember = "Key";
                cmbSpecType.DataSource = bsTmp;
            }
        }
        /// <summary>
        /// 架子规格绑定
        /// </summary>
        private void ShelfBinding()
        {
            Dictionary<int, string> shelfSpec = shelfManage.GetAllShelfSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = shelfSpec;
            this.cmbShelf.DisplayMember = "Value";
            this.cmbShelf.ValueMember = "Key";
            this.cmbShelf.DataSource = bsTemp;

        }

        /// <summary>
        /// 标本盒规格绑定
        /// </summary>
        private void BoxBinding()
        {
            Dictionary<int, string> boxSpec = boxspecManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = boxSpec;
            this.cmbSpec.DisplayMember = "Value";
            this.cmbSpec.ValueMember = "Key";
            this.cmbSpec.DataSource = bsTemp;
        }

        private void DisTypeBinding()
        {
            Dictionary<int,string> dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
        }
        #endregion

        #region 事件
        private void cmbShelf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ucIceBoxLayer_Load(object sender, EventArgs e)
        {
            //switch (iceTypeId)
            //{
            //    case "1":
            //        rbtSpec.Enabled = true;
            //        rbtShelf.Enabled = true;
            //        break;
            //    case "2":
            //        rbtShelf.Checked = false;
            //        rbtShelf.Enabled = false;
            //        rbtSpec.Checked = true;
            //        cmbSpec.Enabled = true;
            //        nudCol.Value = 1;
            //        nudHeight.Value = 1;
            //        nudRow.Value = 1;
            //        break;
            //    case "3":
            //        rbtSpec.Checked = false;
            //        rbtSpec.Enabled = false;
            //        rbtShelf.Checked = true;
            //        break;
            //    default:
            //        break;
            //}
            ShelfBinding();
            BoxBinding();
            DisTypeBinding();
            BindingSpecClass();
        }

        private void rbtShelf_CheckedChanged(object sender, EventArgs e)
        {
            IceBoxLayer tmp = new IceBoxLayer();
            LayerSaveObj(ref tmp);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 冰箱层中的设置
        /// </summary>
        public IceBoxLayer LayerSaveObj(ref IceBoxLayer iceBoxLayer)
        {
            //IceBoxLayer iceBoxLayer;// = new IceBoxLayer();
            //IceBoxManage iceBoxManage = new IceBoxManage();
            //iceBoxManage.GetIceBoxByName(
            //iceBoxLayer = new IceBoxLayer();
            if (cmbDisType.Items.Count > 0)
            {
                iceBoxLayer.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.SelectedValue);
            }
            if (cmbSpecType.Items.Count > 0)
            {
                iceBoxLayer.SpecTypeID = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());               
            }
            if (cmbOrgOrBlood.Items.Count > 0)
            {
                iceBoxLayer.BloodOrOrgId = cmbOrgOrBlood.SelectedValue.ToString();
            }
            if (iceBoxLayer.OccupyCount == 0)
            {
                iceBoxLayer.OccupyCount = 0;
            }
            //如果层中存放的是标本盒
            if (rbtSpec.Checked)
            {
                cmbShelf.Enabled = false;
                nudShelfCount.Enabled = false;
                nudCol.Enabled = true;
                nudHeight.Enabled = true;
                nudRow.Enabled = true;
                cmbSpec.Enabled = true;
                //能放多少层的标本盒                
                iceBoxLayer.Height = Convert.ToInt32(nudHeight.Value);
                iceBoxLayer.Row = Convert.ToInt32(nudRow.Value);
                iceBoxLayer.Col = Convert.ToInt32(nudCol.Value);
                iceBoxLayer.IsOccupy = Convert.ToInt16(0);
                //存放的标本容器B:表示直接存放的标本盒
                iceBoxLayer.SaveType = 'B';
                //存放的容器规格ID
                int specID =  Convert.ToInt32(cmbSpec.SelectedValue);
                iceBoxLayer.SpecID = specID;
                int capacity = iceBoxLayer.Height * iceBoxLayer.Row * iceBoxLayer.Col;
                iceBoxLayer.Capacity = capacity;
                iceBoxLayer.BloodOrOrgId = cmbOrgOrBlood.SelectedValue.ToString();               
                //iceBoxLayer.SpecTypeID = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
                //存放的病种类型
                //iceBoxLayer.DiseaseType.DisTypeID;
            }
            //如果存放的是标本架
            else
            {
                cmbShelf.Enabled = true;
                nudShelfCount.Enabled = true;
                nudCol.Enabled = false;
                nudHeight.Enabled = false;
                nudRow.Enabled = false;
                cmbSpec.Enabled = false;
                //一层中能放多少个的架子
                iceBoxLayer.Col = Convert.ToInt32(nudShelfCount.Value);
                if (cmbShelf.Items.Count > 0)
                {
                    iceBoxLayer.SpecID = Convert.ToInt32(cmbShelf.SelectedValue);
                }
                else
                    return null;
                iceBoxLayer.SaveType = 'J';
                iceBoxLayer.IsOccupy = Convert.ToInt16(0);
                iceBoxLayer.Capacity = iceBoxLayer.Col;
                //存放的病种类型
                //iceBoxLayer.DiseaseType.DisTypeID=''
            }
            return iceBoxLayer;
        }

        /// <summary>
        /// 根据每一层的属性值，设置页面
        /// </summary>
        /// <param name="layer"></param>
        public void SetValue(IceBoxLayer layer)
        {
            this.cmbDisType.SelectedValue = layer.DiseaseType.DisTypeID;
            cmbSpecType.SelectedValue = layer.SpecTypeID;
            cmbOrgOrBlood.SelectedValue =Convert.ToInt32(layer.BloodOrOrgId);
            if (layer.SaveType == 'B')
            {
                this.cmbSpec.SelectedValue = layer.SpecID;
                this.nudCol.Value = layer.Col;
                this.nudHeight.Value = layer.Height;
                this.nudRow.Value = layer.Row;
                this.rbtSpec.Checked = true;
            }
            if (layer.SaveType == 'J')
            {
                this.cmbShelf.SelectedValue = layer.SpecID;
                this.nudShelfCount.Value = layer.Col;
                this.rbtShelf.Checked = true;
            }
            ControlCollection chkControls = this.flpChk.Controls;
            foreach (Control c in chkControls)
                c.Enabled = false;
            ControlCollection grpControls = this.grpIcebox.Controls;
            foreach (Control c in grpControls)
                c.Enabled = false;
            ControlCollection grpShelfControls = this.grpShelf.Controls;
            foreach (Control c in grpShelfControls)
                c.Enabled = false;
            ControlCollection grpSpecControls = this.grpSpec.Controls;
            foreach (Control c in grpSpecControls)
                c.Enabled = false;
        }

        public void SetOrgClass(string orgId)
        {
            cmbOrgOrBlood.SelectedValue = Convert.ToInt32(orgId); 
        }

        public void SetSpecType(string specTypeId)
        {
            cmbSpecType.SelectedValue = Convert.ToInt32(specTypeId);
        }
        #endregion

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            BindingSpecType(orgId);
        }
    }
}
