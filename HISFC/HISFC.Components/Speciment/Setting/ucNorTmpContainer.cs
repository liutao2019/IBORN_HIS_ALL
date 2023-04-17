using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucNorTmpContainer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private IceBoxLayerManage layerManage;

        //存储柜管理对象
        private IceBoxManage iceBoxManage;
        private BoxSpecManage specManage;
        private SpecBoxManage specBoxManage;
        private SubSpecManage subSpecManage;
        private CapLogManage capLogManage;

        private BoxSpec boxSpec;
        private IceBox icebox;

        //存储柜实体列表
        private ArrayList arrIcebox;
        private Dictionary<Position, SubSpec> dicSubSpec;
        //需要修改的层列表
        private List<IceBoxLayer> layerList;

        private FS.HISFC.Models.Base.Employee loginPerson;
        private string title;
        private string iceBoxName;
        //A：添加，M：修改
        private string oper;

        private int rowIndex = -1;
        private int colIndex = -1;

        //标本位于盒子中的行，列索引，用于盒子中的标本调整
        private int specRowIndex = -1;
        private int specColIndex = -1;

        public ucNorTmpContainer()
        {
            InitializeComponent();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            capLogManage = new CapLogManage();
            specBoxManage = new SpecBoxManage();
            subSpecManage = new SubSpecManage();

            boxSpec = new BoxSpec();
            icebox = new IceBox();
            layerList = new List<IceBoxLayer>();

            dicSubSpec = new Dictionary<Position, SubSpec>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            arrIcebox = new ArrayList();

            loginPerson = new Employee();
            title = "存储柜设置";
            iceBoxName = "";
            oper = "";
        }

        private void EnableCon(bool e)
        {
            nupLayerNum.Enabled = e;
            cmbSpec.Enabled = e;
        }

        private void ClearCon()
        {
            cmbIceBox.Text = "";
            txtComment.Text = "";
            txtName.Text = "";
            cmbSpec.Text = "";
            nupLayerNum.Value = 1;
            cmbSpecType.Text = "";
            cmbDisType.Text = "";
        }

        /// <summary>
        /// 标本盒规格绑定
        /// </summary>
        private void BoxBinding()
        {
            specManage = new BoxSpecManage();
            Dictionary<int, string> dicBoxSpec = new Dictionary<int, string>();
            dicBoxSpec = specManage.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dicBoxSpec;
            this.cmbSpec.DisplayMember = "Value";
            this.cmbSpec.ValueMember = "Key";
            this.cmbSpec.DataSource = bsTemp;
        }

        /// <summary>
        /// 标本类型绑定
        /// </summary>
        private void SpecTypeBinding()
        {            
            SpecTypeManage tmpMgr = new SpecTypeManage();
            ArrayList arrTmp = new ArrayList();
            arrTmp = tmpMgr.GetSpecByOrgName("组织");//.GetAllBoxSpec();
            cmbSpecType.DataSource = arrTmp;
            this.cmbSpecType.ValueMember = "SpecTypeID";
            this.cmbSpecType.DisplayMember = "SpecTypeName";
            this.cmbSpecType.DataSource = arrTmp;
        }

        /// <summary>
        /// 病种类型绑定
        /// </summary>
        private void DisTypeBinding()
        {
            DisTypeManage tmpMgr = new DisTypeManage();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic = tmpMgr.GetOrgDisType();//.GetSpecByOrgName("组织");//.GetAllBoxSpec();
            BindingSource bsTemp = new BindingSource();
            bsTemp.DataSource = dic;
            this.cmbDisType.DisplayMember = "Value";
            this.cmbDisType.ValueMember = "Key";
            this.cmbDisType.DataSource = bsTemp;
        }

        private void IceBoxBinding()
        {
            ArrayList arr = iceBoxManage.GetIceBoxByType("2");
            if (arr == null)
            {
                return;
            }
            cmbIceBox.DataSource = arr;
            this.cmbIceBox.ValueMember = "IceBoxId";
            this.cmbIceBox.DisplayMember = "IceBoxName";
            this.cmbIceBox.DataSource = arr;
        }

        /// <summary>
        /// 报废存储柜
        /// </summary>
        /// <param name="iceboxId"></param>
        private void DisuseIceBox(int iceboxId)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                SpecBoxManage tmpManage = new SpecBoxManage();
                tmpManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ArrayList arrLayer = layerManage.GetIceBoxLayers(iceboxId.ToString());
                ArrayList arrBox;

                foreach (IceBoxLayer l in arrLayer)
                {
                    IceBoxLayer tmpl = l;

                    #region 如果保存的是标本盒
                    if (l.SaveType == 'B')
                    {
                        arrBox = new ArrayList();
                        arrBox = tmpManage.GetBoxByCap(l.LayerId.ToString(), 'B');
                        foreach (SpecBox b1 in arrBox)
                        {
                            //废除存储柜中的标本盒
                            SpecBox tmpb1 = b1;
                            tmpb1.DesCapCol = 0;
                            tmpb1.DesCapID = 0;
                            tmpb1.DesCapRow = 0;
                            tmpb1.DesCapSubLayer = 0;
                            tmpb1.DesCapType = '0';
                            tmpb1.IsOccupy = '0';
                            tmpb1.OccupyCount = 0;
                            if (tmpManage.UpdateSpecBox(tmpb1) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("操作失败！", title);
                                return;
                            }
                            if (capLogManage.ModifyBoxLog(b1, loginPerson.Name, "D", tmpb1, "存储柜废除") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("操作失败！", title);
                                return;
                            }
                        }
                    }
                    #endregion

                    tmpl.LayerNum = 0;
                    tmpl.OccupyCount = 0;
                    tmpl.IceBox.IceBoxId = 0;
                    tmpl.SaveType = '0';
                    tmpl.SpecID = 0;
                    if (layerManage.UpdateLayer(tmpl) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！", title);
                        return;
                    }
                    int result = capLogManage.ModifyIceBoxLayer(l, loginPerson.Name, "D", tmpl, "存储柜废除");
                    if (result <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！", title);
                        return;
                    }
                }

                IceBox tmpib = iceBoxManage.GetIceBoxById(iceboxId.ToString());
                tmpib.UseStaus = "0";
                tmpib.Comment = txtComment.Text;
                if (iceBoxManage.UpdateIceBox(tmpib) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！", title);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功！", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败！", title);
            }
        }        

        /// <summary>
        /// 设置
        /// </summary>
        private void SettingLayer(IceBoxLayer l)
        {
            try
            {
                SpecTypeManage tmpMgr = new SpecTypeManage();
                DisTypeManage disTmpMgr = new DisTypeManage();
                string specType = tmpMgr.GetSpecTypeById(l.SpecTypeID.ToString()).SpecTypeName;
                string disType = disTmpMgr.SelectDisByID(l.DiseaseType.DisTypeID.ToString()).DiseaseName; ;
                BoxSpec tmpSpec = specManage.GetSpecById(l.SpecID.ToString());
                string spec = tmpSpec.Row.ToString() + "×" + tmpSpec.Col.ToString();
                if (specType == null)
                {
                    MessageBox.Show("获取标本类型失败！");
                    return;
                }

                if (disType == null)
                {
                    MessageBox.Show("获取病种失败！");
                    return;
                }
                grpLayer.Text = icebox.IceBoxName + "  共" + nupLayerNum.Value.ToString() + "层";
                dgv[0, l.LayerNum].Tag = l;
                dgv[0, l.LayerNum].Value = "第" + l.LayerNum.ToString() + "层  (" + disType + "," + specType + ") " + spec;

            }
            catch
            {
                MessageBox.Show("获取信息失败！");
                return;
            }

           
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        private bool DataValidate()
        {
            iceBoxName = txtName.Text.TrimEnd().TrimStart();
            if (cmbSpec.SelectedValue == null || cmbSpec.Text.Trim() == "")
            {
                MessageBox.Show("请选择规格",title);
                return false;
            }
            if ((txtName.Text==""))// == null) || cmbSpecType.Text.Trim() != ""())
            {
                MessageBox.Show("请填写名称", title);
                return false;
            }
            IceBox tmp = iceBoxManage.GetIceBoxByName(iceBoxName);
            if (null != tmp && tmp.IceBoxName == iceBoxName)
            {
                MessageBox.Show("冰箱名不能重复，请重新输入！",title);
                return false;
            }
            icebox.IceBoxName = iceBoxName;
            return true;
        }

        /// <summary>
        /// 绑定存储柜
        /// </summary>
        private void BindingIceBox()
        {
            arrIcebox = iceBoxManage.GetIceBoxByType("2");
            arrIcebox.Add(new IceBox());
            cmbIceBox.DataSource = arrIcebox;
            cmbIceBox.DisplayMember = "IceBoxName";
            cmbIceBox.ValueMember = "IceBoxId";
            IceBoxBinding();
            //cmbIceBox.SelectedIndex = arrIcebox.Count - 1;
        }

        private void SettinLayerDet(ref IceBoxLayer l)
        {
            layerList.Add(l);
            this.SettingLayer(l);                                   
        }

        /// <summary>
        /// 先保存存储柜，后保存每一层，最后保存盒子
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            #region 保存存储柜
            icebox.IceBoxName = iceBoxName;
            icebox.Comment = txtComment.Text;
            
            if (oper == "M")
            {
                if (iceBoxManage.UpdateIceBox(icebox) <= 0)
                {
                    MessageBox.Show("更新存储柜失败", title);
                    return -1;
                }                
            }
            if (oper == "A")
            {
                icebox.IceBoxTypeId = "2";
                icebox.UseStaus = "1";
                icebox.SpecTypeId = "0";
                icebox.OrgOrBlood = "0";
                
                if (iceBoxManage.InsertIceBox(icebox) <= 0)
                {
                    MessageBox.Show("添加存储柜失败", title);
                    return -1;
                }
            }
            #endregion

            for (int i = 1; i < dgv.Rows.Count; i++)
            {                
                IceBoxLayer tmp = dgv.Rows[i].Cells[0].Tag as IceBoxLayer;
                if (tmp == null)
                {
                    return -1;
                }

                if (oper == "M")
                {
                    try
                    {
                        if (subSpecManage.GetSubSpecInLayerOrShelf(tmp.LayerId.ToString(), "B").Count > 0)
                        {
                            MessageBox.Show("第 " + i.ToString()+" 层有标本存放 ，不能更改！", title);
                            continue;
                        }
                    }
                    catch
                    { }
                    SpecBox box = specBoxManage.GetBoxByCap(tmp.LayerId.ToString(), 'B')[0] as SpecBox;
                    if (box == null)
                    {
                        return -1;
                    }
                    if (layerManage.UpdateLayer(tmp) <= 0)
                    {
                        MessageBox.Show("更新存储柜失败", title);
                        return -1;
                    }

                    //更新存储柜每一层中的盒子信息
                    box.BoxBarCode = "BX" + tmp.IceBox.IceBoxId.ToString().PadLeft(3,'0') + "-C" + tmp.LayerNum.ToString();
                    //box.DesCapID = tmp.LayerId;
                    box.DiseaseType.DisTypeID = tmp.DiseaseType.DisTypeID;
                    box.SpecTypeID = tmp.SpecTypeID;
                    box.BoxSpec.BoxSpecID = tmp.SpecID;
                    if (specBoxManage.UpdateSpecBox(box) <= 0)
                    {
                        MessageBox.Show("更新存储柜失败", title);
                        return -1;
                    }
                }
                if (oper == "A")
                {
                    string seqL="";
                    layerManage.GetNextSequence(ref seqL);
                    tmp.LayerId = Convert.ToInt32(seqL);
                    tmp.IceBox = iceBoxManage.GetIceBoxByName(iceBoxName);
                    ;
                    if (layerManage.InsertIceBoxLayer(tmp) <= 0)
                    {
                        MessageBox.Show("添加存储柜失败", title);
                        return -1;
                    }
                    capLogManage.ModifyIceBoxLayer(new IceBoxLayer(), loginPerson.Name, "N", tmp, "新建冰箱层");
                    SpecBox newBox = new SpecBox();
                    newBox.OrgOrBlood = 2;
                    newBox.BoxBarCode = "BX" + tmp.IceBox.IceBoxId.ToString().PadLeft(3, '0') + "-C" + tmp.LayerNum.ToString();
                    newBox.DesCapID = tmp.LayerId;
                    newBox.DiseaseType.DisTypeID = tmp.DiseaseType.DisTypeID;
                    newBox.SpecTypeID = tmp.SpecTypeID;
                    string seq = "";
                    specBoxManage.GetNextSequence(ref seq);
                    newBox.BoxId = Convert.ToInt32(seq);
                    newBox.BoxSpec = boxSpec;
                    newBox.Capacity = boxSpec.Row * boxSpec.Col;
                    newBox.IsOccupy = '0';
                    newBox.InIceBox = '0';
                    newBox.ColNum = 1;
                    newBox.DesCapCol = 1;
                    newBox.DesCapRow = 1;
                    newBox.DesCapSubLayer = 1;
                    newBox.DesCapType = 'B';
                    newBox.OccupyCount = 0;
                    if (specBoxManage.InsertSpecBox(newBox) <= 0)
                    {
                        MessageBox.Show("添加存储柜失败", title);
                        return -1;
                    }
                    capLogManage.ModifyBoxLog(new SpecBox(), loginPerson.Name, "N", newBox, "新建标本盒");
                    //newBox.Capacity = 
                }
            }
            return 1;
        }

        public override int Save(object sender, object neuObject)
        {
            if (oper == "")
            {
                return 0;
            }

            IceBoxLayer tmp = dgv.Rows[0].Cells[0].Tag as IceBoxLayer;
            if (tmp != null)
            {
                MessageBox.Show("第一行不能存放");
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (Save() == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败", title);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                oper = "";
                MessageBox.Show("操作成功", title);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败");
            } 
            return base.Save(sender, neuObject);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmNorTmpLayerSetting frm = new frmNorTmpLayerSetting();
                IceBoxLayer layer = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as IceBoxLayer;
                if (layer == null)
                {
                    MessageBox.Show("获取信息失败！");
                    return;
                }
                frm.BoxName = iceBoxName;
                frm.Layer = layer;
                frm.Show();
                frm.OnSetLayer += new frmNorTmpLayerSetting.SetLayer(SettinLayerDet);
            }
            catch
            { }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                dgv.Rows.Clear();
                this.SpecTypeBinding();
                this.BoxBinding();
                this.DisTypeBinding();
                this.SpecTypeBinding();
                this.BindingIceBox();
                ClearCon();
                EnableCon(false);

                DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                imgDcl.HeaderText = "高度";
                imgDcl.Width = 210;
                dgv.Columns.Add(imgDcl);
                dgv.Rows.Clear();
            }
            catch
            { }
            base.OnLoad(e);
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("报废", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("添加", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            this.toolBarService.AddToolButton("修改", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "报废":
                    if (cmbIceBox.SelectedValue == null || cmbIceBox.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("获取存储柜失败！", title);
                        return;
                    }
                    int iceboxId = Convert.ToInt32(cmbIceBox.SelectedValue.ToString());
                    if (iceBoxManage.CheckIceBoxHaveSpecBox(iceboxId.ToString()) == 1)
                    {
                        MessageBox.Show("存储柜中存有标本，不能废除", title);
                        return;
                    }
                    DialogResult dialog = MessageBox.Show("此操作将废除存储柜中所有存放的标本架和标本盒信息，继续？", title, MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                        return;
                    DisuseIceBox(iceboxId);
                    BindingIceBox();
                    break;
                case "添加":
                    oper = "A";
                    this.EnableCon(true);
                    this.ClearCon();
                    break;
                case "修改":
                    this.EnableCon(true);
                    oper = "M";
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            { 
                if (!DataValidate())
                {
                    return;
                }
                dgv.Rows.Clear();
                dgvSpec.Rows.Clear();
                icebox = new IceBox();
                icebox.LayerNum = Convert.ToInt32(nupLayerNum.Value.ToString());
                IceBoxLayer layer = new IceBoxLayer();
                layer.BloodOrOrgId = "2";
                layer.Capacity = 1;
                layer.IsOccupy = 1;
                layer.Col = 1;
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    layer.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.SelectedValue.ToString());
                }
                layer.OccupyCount = 1;
                layer.Row = 1;
                layer.SaveType = 'B';
                if (cmbSpecType.SelectedValue != null && cmbSpecType.Text.Trim() != "")
                {
                    layer.SpecTypeID = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());

                }
                boxSpec = specManage.GetSpecById(cmbSpec.SelectedValue.ToString());
                layer.SpecID = boxSpec.BoxSpecID;

                for (int i = 0; i < icebox.LayerNum + 1; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    dgv.Rows.Add(dr);
                }
                // DataGridViewRow dr1 = new DataGridViewRow();
                //    dr1.Height = 40;
                //    dgv.Rows.Add(dr1); 
                for (int i = 0; i < icebox.LayerNum; i++)
                {
                    IceBoxLayer l = new IceBoxLayer();
                    l = layer.Clone();
                    l.LayerNum = (short)(i + 1);
                    this.SettingLayer(l);
                }
                dgv.Rows[0].Height = 40;
                dgv[0, 0].Tag = null;
                dgv[0, 0].Value = "用于移位";
                dgv.Tag = icebox;
            }
            catch
            { }

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvSpec = new DataGridView();
            //dgvSpec.RowCount = 0; 
            //dgvSpec.ColumnCount = 0;
            dgvSpec.Rows.Clear();
            dgvSpec.Columns.Clear();
            dicSubSpec.Clear();
            IceBoxLayer layer = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as IceBoxLayer;
            if (layer == null || layer.LayerId<=0)
            {
                return;
            }
            SpecBox currentBox = new SpecBox();
            try
            {
                currentBox = specBoxManage.GetBoxByCap(layer.LayerId.ToString(), 'B')[0] as SpecBox;
            }
            catch
            {
                return;
            }
            dgvSpec.Tag = currentBox;
            if (currentBox == null || currentBox.BoxId <= 0)
            {
                return;
            }             

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            #region
            try
            {
                    ArrayList arrSpec = subSpecManage.GetSubSpecInOneBox(currentBox.BoxId.ToString());
                    BoxSpec spec = specManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                    Size size = new Size();
                    if (spec != null)
                    {
                        if (spec.Row > 2)
                        {
                            size.Width = 60 + spec.Col * 60;
                            size.Height = 30 + spec.Row * 30;
                            dgvSpec.Size = size;
                        }
                        for (int c = 1; c <= spec.Col; c++)
                        {
                            //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();
                            DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                            imgDcl.HeaderText = c.ToString();
                            imgDcl.Width = 75;
                            dgvSpec.Columns.Add(imgDcl);
                        }
                        for (int r = 1; r <= spec.Row; r++)
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 30;
                            DataGridViewHeaderCell headerCell = dr.HeaderCell;

                            dr.HeaderCell.Value =r.ToString();
                            dgvSpec.Rows.Add(dr);
                        }
                    }
                    if (arrSpec == null)
                    {
                        return;
                    }
                    foreach (SubSpec s in arrSpec)
                    {
                        Position p = new Position(s.BoxEndRow.ToString(), s.BoxEndCol.ToString());
                        dicSubSpec.Add(p, s);
                    }
                    //遍历DataGridView中的每个位置，如果SpecBox在DataGridView中对应的高和行，加载该标本盒信息
                    #region
                    for (int i = 0; i < dgvSpec.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgvSpec.Columns.Count; k++)
                        {
                            int row = i + 1;
                            int col = k + 1;
                            int index = 0;
                            foreach (Position pt in dicSubSpec.Keys)
                            {
                                if (row == pt.Row && col == pt.Col)
                                {
                                    SubSpec sub = new SubSpec();
                                    sub = dicSubSpec[pt];
                                    DataGridViewCell cell1 = dgvSpec[k, i];
                                    if (sub.SubBarCode == "" && sub.StoreID == 0)
                                    {
                                        cell1.Value = "空";
                                        cell1.Tag = null;
                                        string toolTipText1 = "该位置没放标本";
                                        cell1.ToolTipText = toolTipText1;
                                        break;
                                    }
                                    cell1.Value = sub.SubBarCode;
                                    //cell1.Value = Image.FromFile("Spec.bmp");
                                    //string color = "";
                                    string specTypeName = "";
                                    //this.GetSubSpecDet(currentBox.BoxId.ToString(), sub.BoxEndRow.ToString(), sub.BoxEndCol.ToString(), ref color, ref specTypeName);
                                    cell1.Tag = sub;
                                    string toolTipText = "位置：第" + sub.BoxEndRow.ToString() + " 行,第" + sub.BoxEndCol.ToString() + " 列\n";
                                    toolTipText += "标本类型：" + specTypeName + "\n";
                                    cell1.ToolTipText = toolTipText;
                                    break;
                                }
                                index++;
                            }
                            if (index >= dicSubSpec.Keys.Count)
                            {
                                DataGridViewCell cell2 = dgvSpec[k, i];
                                cell2.Value = "空";
                                //cell2.Value = Image.FromFile("EmptySmall.bmp");
                                cell2.Tag = null;
                                string toolTipText = "该位置没放标本";
                                cell2.ToolTipText = toolTipText;
                            }
                        }
                    }
                    #endregion
            }
            catch
            {

            }
            #endregion
        }

        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIceBox.Text.Trim() == "")
                {
                    return;
                }
                icebox = iceBoxManage.GetIceBoxById(cmbIceBox.SelectedValue.ToString());
                if (icebox == null || icebox.IceBoxId <= 0)
                {
                    return;
                }
                ArrayList arrLayer = layerManage.GetLayerInOneBox(icebox.IceBoxId.ToString());
                if (arrLayer == null || arrLayer.Count <= 0)
                {
                    return;
                }
                nupLayerNum.Value = icebox.LayerNum;
                iceBoxName = icebox.IceBoxName;
                txtComment.Text = icebox.Comment;
                txtName.Text = iceBoxName;

                dgv.Rows.Clear();
                //DataGridViewRow dr1 = new DataGridViewRow();
                //dr1.Height = 40;
                //dgv.Rows.Add(dr1);
                for (int i = 0; i < arrLayer.Count + 1; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    dgv.Rows.Add(dr);
                }
                foreach (IceBoxLayer l in arrLayer)
                {

                    //DataGridViewRow dr = new DataGridViewRow();
                    //dr.Height = 40;
                    //dgv.Rows.Add(dr); 
                    cmbSpec.SelectedValue = l.SpecID;
                    this.SettingLayer(l);
                }

                dgv.Rows[0].Height = 40;
                dgv[0, 0].Tag = null;
                dgv[0, 0].Value = "用于移位";
            }
            catch
            { }
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        { 
            Point p = this.dgv.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgv.HitTest(p.X, p.Y);
            if (info.RowIndex != -1 && info.ColumnIndex != -1 && rowIndex >= 0 && colIndex >= 0)
            {
                if (dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                    return;
                }
                IceBoxLayer l = (IceBoxLayer)e.Data.GetData(typeof(IceBoxLayer));
                if (l == null || l.LayerId <= 0)
                {
                    return;
                }
                if (info.RowIndex == l.LayerNum)
                {
                    return;
                }

                try
                {
                    l.LayerNum = (short)(info.RowIndex);
                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                    int lI = cellValue.IndexOf("层");
                    string newCellValue = cellValue.Substring(lI);
                    string pre = "第" + l.LayerNum.ToString();

                    dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = pre + newCellValue;
                    dgv.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = l;
                    dgv.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgv.Rows[rowIndex].Cells[colIndex].Value = "空";
                    colIndex = -1;
                    rowIndex = -1;
                }
                catch
                {
                    MessageBox.Show("操作失败！");
                    return;
                }
                
            }
        }

        private void dgv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgv.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    IceBoxLayer l = this.dgv[info.ColumnIndex,info.RowIndex].Tag as IceBoxLayer;
                    if (l != null)
                    {
                        rowIndex = info.RowIndex;
                        colIndex = info.ColumnIndex;
                        this.DoDragDrop(l, DragDropEffects.Move);
                    }
                }
            }
        }

        private void dgvSpec_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.dgvSpec.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgvSpec.HitTest(p.X, p.Y);
            if (info.RowIndex != -1 && info.ColumnIndex != -1 && specRowIndex >= 0 && specColIndex >= 0)
            {
                if (dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                    return;
                }
                SubSpec spec = (SubSpec)e.Data.GetData(typeof(SubSpec));
                if (spec == null)
                {
                    return;
                }
                if ((info.ColumnIndex + 1 == spec.BoxEndRow) && (info.RowIndex + 1 == spec.BoxEndCol))
                {
                    return;
                }

                try
                {
                    //BX005-C5-J1-11

                    string row = (info.RowIndex + 1).ToString();
                    string col = (info.ColumnIndex + 1).ToString();

                    spec.BoxEndCol = info.ColumnIndex + 1;
                    spec.BoxEndRow = info.RowIndex + 1;
                    spec.BoxStartCol = info.ColumnIndex + 1;
                    spec.BoxStartRow = info.RowIndex + 1;

                    if (subSpecManage.UpdateSubSpec(spec) <= 0)
                    {
                        MessageBox.Show("操作失败！");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("操作失败！");
                    return;
                }
                dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = spec.SubBarCode;
                dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = spec;
                dgvSpec.Rows[specRowIndex].Cells[specColIndex].Tag = null;
                dgvSpec.Rows[specRowIndex].Cells[specColIndex].Value = "空";
                specRowIndex = -1;
                specColIndex = -1;
            }
        }

        private void dgvSpec_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgvSpec_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgvSpec.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    SubSpec sub = this.dgvSpec.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SubSpec;
                    if (sub != null)
                    {
                        specRowIndex = info.RowIndex;
                        specColIndex = info.ColumnIndex;
                        //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = null;
                        this.DoDragDrop(sub, DragDropEffects.Move);
                    }
                }
            }
        }
    }
}
