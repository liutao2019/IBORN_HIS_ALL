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
    public partial class frmSpecShelf : Form
    {
        private Shelf shelf;
        private ShelfManage shelfManage;
        private ShelfSpec shelfSpec;
        private ShelfSpecManage shelfSpecManage;
        private Dictionary<int, string> dicSpecType;
        private SpecTypeManage specTypeManage;
        private Dictionary<int, string> dicOrgType;
        private IceBoxLayerManage layerManage;
        private IceBoxManage iceBoxManage;
        private OrgTypeManage orgTypeManage;
        private IceBoxLayer iceBoxLayer;
        private DisTypeManage disTypeManage;
        private CapLogManage capLogManage;
        private string title;
        private Shelf shelfFromModify;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private int layerId;

        public Shelf ShelfFromModify
        {
            get
            {
                return shelfFromModify;
            }
            set
            {
                shelfFromModify = value;
            }
        }

        public int LayerId
        {
            get
            {
                return layerId;
            }
            set
            {
                layerId = value;
            }
        }

        public frmSpecShelf()
        {
            InitializeComponent();
            shelf = new Shelf();
            shelfManage = new ShelfManage();
            shelfSpec = new ShelfSpec();
            shelfSpecManage = new ShelfSpecManage();
            dicSpecType = new Dictionary<int, string>();
            specTypeManage = new SpecTypeManage();
            dicOrgType = new Dictionary<int, string>();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            orgTypeManage = new OrgTypeManage();          
            disTypeManage = new DisTypeManage();
            capLogManage = new CapLogManage();            
            title = "冻存架添加";
            //shelfFromModify = new Shelf();
            loginPerson = new FS.HISFC.Models.Base.Employee();
        }

        /// <summary>
        /// 架子规格绑定
        /// </summary>
        private void ShelfBinding()
        {
            Dictionary<int, string> shelfSpec = shelfSpecManage.GetAllShelfSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = shelfSpec;
            this.cmbShelf.DisplayMember = "Value";
            this.cmbShelf.ValueMember = "Key";
            this.cmbShelf.DataSource = bsTemp;
        }

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
        /// 病种类型的绑定
        /// </summary>
        private void DisTypeBinding()
        {
            //Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDiseaseType.DisplayMember = "Value";
            //    cmbDiseaseType.ValueMember = "Key";
            //    cmbDiseaseType.DataSource = bs;
            //}
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDiseaseType.AddItems(alDisType);
                }
            }
        }

        /// <summary>
        /// 设置架子信息
        /// </summary>
        private void SetShelf()
        {            
            if (cmbShelf.SelectedValue != null)
            {
                string shelfSpecId = cmbShelf.SelectedValue.ToString();
                shelf.ShelfSpec.ShelfSpecID = Convert.ToInt32(shelfSpecId);
                shelfSpec = shelfSpecManage.GetShelfByID(shelfSpecId);
                shelf.Capacity = shelfSpec.Row * shelfSpec.Height;
            }
            shelf.OccupyCount = 0;
            shelf.IsOccupy = '0';
            shelf.Comment = txtComment.Text;
            if (cmbDiseaseType.Tag != null)
            {
                shelf.DisTypeId = Convert.ToInt32(cmbDiseaseType.Tag.ToString());                
            }
            if (cmbSpecType.SelectedValue != null)
            {
                shelf.SpecTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
            } 
        }

        /// <summary>
        /// 给架子重定位
        /// </summary>
        private bool ShelfLocate()
        {           
            string specClassId =cmbOrgOrBlood.SelectedValue.ToString();
            if (iceBoxLayer == null)
            {
                iceBoxLayer = new IceBoxLayer();
            }
           if (iceBoxLayer.LayerId <= 0)
            {
                iceBoxLayer = layerManage.ShelfGetLayerById(shelf.DisTypeId.ToString(), shelf.ShelfSpec.ShelfSpecID.ToString(), specClassId, shelf.SpecTypeId.ToString());
                return false;
            }
            ArrayList arrShelf = new ArrayList();
            arrShelf = shelfManage.GetShelfByLayerID(iceBoxLayer.LayerId.ToString());
            shelf.IceBoxLayer.Row = 1;
            shelf.IceBoxLayer.Height = 1;
            shelf.IceBoxLayer.LayerId = iceBoxLayer.LayerId;          
            txtLocate.Text = iceBoxManage.GetIceBoxByLayerID(iceBoxLayer.LayerId.ToString()).IceBoxName + "-";
            txtLocate.Text += iceBoxLayer.LayerNum.ToString().PadLeft(2, '0') + "-";
            if (arrShelf.Count == 0)
            {
                shelf.IceBoxLayer.Col = 1;
            }
            Shelf lastShelf = shelfManage.ScanLayer(arrShelf);
            shelf.IceBoxLayer.Col = lastShelf.IceBoxLayer.Col + 1;
            txtLocate.Text += shelf.IceBoxLayer.Col.ToString().PadLeft(2, '0');
            shelf.SpecBarCode = "BX"+iceBoxLayer.IceBox.IceBoxId.ToString().PadLeft(3, '0');
            shelf.SpecBarCode += "-C" + iceBoxLayer.LayerNum.ToString().PadLeft(1, '0');
            shelf.SpecBarCode += "-J" + shelf.IceBoxLayer.Col.ToString().PadLeft(1, '0');
            return true;
        }

        private bool DataValidate()
        {
            if (cmbDiseaseType.Tag == null)
                return false;
            if (cmbSpecType.SelectedValue == null)
                return false;
            if (cmbShelf.SelectedValue == null)
                return false;
            return true;
        }

        /// <summary>
        /// 给冰箱层添加架子时使用
        /// </summary>
        public void GetShelfSetting()
        {
            cmbOrgOrBlood.SelectedValue = Convert.ToInt32(iceBoxLayer.BloodOrOrgId);
            if(iceBoxLayer.BloodOrOrgId=="3")
                cmbOrgOrBlood.Enabled = true;
            else
                cmbOrgOrBlood.Enabled = false;
            cmbDiseaseType.Tag = iceBoxLayer.DiseaseType.DisTypeID;
            if (iceBoxLayer.DiseaseType.DisTypeID == 16)
            {
                cmbDiseaseType.Enabled = true;
            }
            else
            {
                cmbDiseaseType.Enabled = false;
            }
            cmbSpecType.SelectedValue = iceBoxLayer.SpecTypeID;
            if (iceBoxLayer.SpecTypeID == 9)
            {
                cmbSpecType.Enabled = true;
            }
            else
            {
                cmbSpecType.Enabled = false;
            }
            cmbShelf.SelectedValue = iceBoxLayer.SpecID;
            cmbShelf.Enabled = false;
        }

        /// <summary>
        /// 修改架子的属性时使用
        /// </summary>
        private void ShelfModifySetting()
        {
            cmbDiseaseType.Tag = shelfFromModify.DisTypeId;
            //如果架子的病种属性不设置为“其它”，不允许修改
            IceBoxLayer tmpLayer = layerManage.GetLayerById(shelfFromModify.IceBoxLayer.LayerId.ToString());
            if (tmpLayer.DiseaseType.DisTypeID != 16)
            {
                cmbDiseaseType.Enabled = false;
            }
            if (tmpLayer.SpecTypeID != 9)
            {
                cmbOrgOrBlood.Enabled = false;
                cmbSpecType.Enabled = false;
            }
            //if (shelfFromModify.DisTypeId != 16)
            //{
            //    cmbDiseaseType.Enabled = false;
            //}
            int orgTypeId = orgTypeManage.GetBySpecType(shelfFromModify.SpecTypeId.ToString()).OrgTypeID;
            cmbOrgOrBlood.SelectedValue = orgTypeId;    
            cmbSpecType.SelectedValue = shelfFromModify.SpecTypeId;
            //if (shelfFromModify.SpecTypeId != 9)
            //{
            //    cmbOrgOrBlood.Enabled = false;
            //    cmbSpecType.Enabled = false;
            //}
            //架子规格不能修改
            cmbShelf.SelectedValue = shelfFromModify.ShelfSpec.ShelfSpecID;
            cmbShelf.Enabled = false;
            txtComment.Text = shelfFromModify.Comment;
            txtBoxCode.Text = shelfFromModify.SpecBarCode;
            txtBoxCode.Enabled = false;
            txtLocate.Text = iceBoxManage.GetIceBoxByLayerID(tmpLayer.LayerId.ToString()).IceBoxName + "-";
            txtLocate.Text += tmpLayer.LayerNum.ToString().PadLeft(2, '0') + "-";
            txtLocate.Text += shelfFromModify.IceBoxLayer.Col.ToString().PadLeft(2, '0');          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtBoxCode.Text = "";
            txtComment.Text = "";         
            txtLocate.Text = "";
            cmbOrgOrBlood.Enabled = true;
            cmbDiseaseType.Enabled = true;
            cmbShelf.Enabled = true;
            cmbSpecType.Enabled = true;
            txtBoxCode.Enabled = true;
            txtLocate.Enabled = true;
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            BindingSpecType(orgId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!DataValidate())
            {
                MessageBox.Show("请填写必填项！", title);
                return;
            }
            SpecBoxManage specBoxManage = new SpecBoxManage();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                orgTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                string barCode = "";
                if ((shelfFromModify == null || shelfFromModify.ShelfID <= 0))
                {
                    SetShelf();     
                    if (!ShelfLocate())
                    {
                        MessageBox.Show("没有找到冰箱层存放冻存架，请检查设置！", title);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    string sequence = "";
                    shelfManage.GetNextSequence(ref sequence);
                    shelf.ShelfID = Convert.ToInt32(sequence);               
                    if (shelfManage.InsertShelf(shelf) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败!", title);
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    barCode = shelf.SpecBarCode;
                    //更新冰箱层的占用数量
                    int occupyCount = iceBoxLayer.OccupyCount + 1;
                    if (layerManage.UpdateOccupyCount(occupyCount.ToString(), iceBoxLayer.LayerId.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败!", title);
                        return;
                    }
                    //如果满了更新存满标志
                    if (occupyCount == iceBoxLayer.Capacity)
                    {
                        if (layerManage.UpdateOccupy("1", iceBoxLayer.LayerId.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败!", title);
                            return;
                        }
                    }
                    capLogManage.ModifyShelf(new Shelf(), loginPerson.Name, "N", shelf, "新建冻存架");
                    txtBoxCode.Text = barCode;
                }
                if (shelfFromModify != null && shelfFromModify.ShelfID > 0)
                {
                    DialogResult dialog = MessageBox.Show("此操作将更新架子中所有标本盒的病种及类型设置信息", "修改架子", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    Shelf beforeModify = shelfFromModify;
                    shelfFromModify.SpecTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
                    shelfFromModify.DisTypeId = Convert.ToInt32(cmbDiseaseType.Tag.ToString());
                    shelfFromModify.Comment = txtComment.Text;
                    //写入日志
                    capLogManage.ModifyShelf(beforeModify, loginPerson.Name, "M", shelfFromModify, "冻存架修改");
                    if (shelfManage.UpdateShelf(shelfFromModify) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败!", title);
                        shelfFromModify = new Shelf();
                        return;
                    }
                    ArrayList arrBox = new ArrayList();
                    arrBox = specBoxManage.GetBoxByCap(shelfFromModify.ShelfID.ToString(), 'J');
                    foreach (SpecBox sb in arrBox)
                    {
                        SpecBox tmp = new SpecBox();
                        tmp = sb;
                        tmp.SpecTypeID = shelfFromModify.SpecTypeId;
                        tmp.DiseaseType.DisTypeID = shelfFromModify.DisTypeId;
                        tmp.OrgOrBlood = specTypeManage.GetSpecTypeById(tmp.SpecTypeID.ToString()).OrgType.OrgTypeID;
                        if (specBoxManage.UpdateSpecBox(tmp) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败!", title);
                            return;
                        }
                        capLogManage.ModifyBoxLog(sb, loginPerson.Name, "M", tmp, "修改标本盒信息");
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                iceBoxLayer = null;
                layerId = 0;
                MessageBox.Show("保存成功!", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败!", title);
            }
        }

        private void frmSpecShelf_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //查询冰箱类型列表
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");
            this.txtIceBoxType.AddItems(iceBoxTypeList);
            ShelfBinding();
            BindingSpecClass();
            DisTypeBinding();
            if (layerId >= 1)
            {
                iceBoxLayer = new IceBoxLayer();
                iceBoxLayer = layerManage.GetLayerById(layerId.ToString());
                if (iceBoxLayer.OccupyCount == iceBoxLayer.Capacity)
                {
                    MessageBox.Show("冰箱层已满，不能添加冻存架", title);
                    iceBoxLayer = null;
                    return;
                }
                GetShelfSetting();
            }
            if (shelfFromModify != null && shelfFromModify.ShelfID > 0)
            {
                ShelfModifySetting();
 
            }
        }

        private void btnBrowseLoc_Click(object sender, EventArgs e)
        {
            frmLocate frmL = new frmLocate();
            frmL.container = "l";
            frmL.Show();
            frmL.OnSetContainerId += new frmLocate.SetContainerId(OnSetContainerId);
        }

        private void OnSetContainerId(object sender, EventArgs e)
        {
            try
            {
                if (frmLocate.containerId <= 0)
                {
                    return;
                }
                iceBoxLayer = new IceBoxLayer();
                layerId = frmLocate.containerId;
                iceBoxLayer = layerManage.GetLayerById(layerId.ToString());
                if (iceBoxLayer.OccupyCount == iceBoxLayer.Capacity)
                {
                    MessageBox.Show("冰箱层已满，不能添加冻存架", title);
                    iceBoxLayer = null;
                    return;
                }
                GetShelfSetting();
                txtLocate.Text = frmLocate.location;
            }
            catch
            {
 
            }
        }
    }
}