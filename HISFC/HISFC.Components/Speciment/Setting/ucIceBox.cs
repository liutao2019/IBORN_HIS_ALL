using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.FrameWork.WinForms.Controls;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.BizLogic.Manager;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucIceBox : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //规格的个数
        private int specCount;
        //层数
        private int layerCount;
        //checkBox的个数
        private int chkNum;
        //层数组的索引
        private int specIndex;
        //checkBox的索引
        private int chkIndex;
        //TalbeLayoutPanel的列数
        private int tableCol;
        ////TalbeLayoutPanel的行数
        private int tableRow;
        //checkbox 数组
        private NeuCheckBox[] chk;
        private ucIceBoxLayer[] ucLayer;
        //checkBox 是否选中
        //private bool[] chkChecked;
        private IceBox tmpIcebox;
        //冰箱层管理对象
        private IceBox curSelectedIceBox;
        private IceBoxLayerManage layerManage;
        //冰箱管理对象
        private IceBoxManage iceBoxManage;
        //冰箱实体列表
        private ArrayList arrIcebox;
        private Dictionary<int, string> dicOrgType;
        private Dictionary<int, string> dicSpecType;
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private ShelfManage shelfManage;
        private ShelfSpecManage shelfSpecManage;        
        private string title = "冰箱设置";
        private int LayerSetting = 0;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;      
        private CapLogManage capLogManage;
        private FS.HISFC.Models.Base.Employee loginPerson;

        public ucIceBox()
        {
            InitializeComponent();
            layerManage = new IceBoxLayerManage();
            iceBoxManage = new IceBoxManage();
            tmpIcebox = new IceBox();
            curSelectedIceBox = new IceBox();
            shelfManage = new ShelfManage();
            arrIcebox = new ArrayList();
            dicOrgType = new Dictionary<int, string>();
            dicSpecType = new Dictionary<int, string>();
            shelfSpecManage = new ShelfSpecManage();
            orgTypeManage = new OrgTypeManage();
            specTypeManage = new SpecTypeManage();
            capLogManage = new CapLogManage();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            tlpLayer.MouseWheel += this.tlpLayer_MouseWheel;
            loginPerson = new FS.HISFC.Models.Base.Employee();   
        }

        #region 初始化页面数据绑定
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
        /// 绑定冰箱
        /// </summary>
        private void BindingIceBox(string typeId)
        {
            arrIcebox = iceBoxManage.GetIceBoxByType(typeId);
            if (arrIcebox != null)
            {
                if (arrIcebox.Count > 0)
                {
                    cmbIceBox.DataSource = arrIcebox;
                    cmbIceBox.DisplayMember = "IceBoxName";
                    cmbIceBox.ValueMember = "IceBoxId";
                    cmbIceBox.Text = "";
                }
            }
            //cmbIceBox.SelectedIndex = arrIcebox.Count - 1;
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
                cmbSpecType.SelectedIndex = 0;
            }
        }
        #endregion

        #region 信息获取
        /// <summary>
        /// 根据架子规格Id得到架子的容量
        /// </summary>
        /// <param name="shelfSpecID">架子的规格ID</param>
        /// <returns>架子的容量</returns>
        private int GetShelfSpec(string shelfSpecID)
        {
            ShelfSpec sp = new ShelfSpec();
            sp = shelfSpecManage.GetShelfByID(shelfSpecID);
            int capacity=0;
            if (sp.Col > 0 && sp.Row > 0 && sp.Height > 0)
            {
                capacity = sp.Col * sp.Height * sp.Row;
            }
            return capacity;
        }

        /// <summary>
        /// 从页面读取冰箱的信息
        /// </summary>
        /// <returns></returns>
        private bool IceBoxInfo(System.Data.IDbTransaction trans)
        {
            tmpIcebox.LayerNum = Convert.ToInt32(nudLayerNum.Value);
            if (txtIceBoxType.Tag != null)
            {
                tmpIcebox.IceBoxTypeId = txtIceBoxType.Tag.ToString();
            }
            //默认为立式冷冻柜
            else
                tmpIcebox.IceBoxTypeId = "1";
            string iceboxName = txtName.Text.TrimEnd().TrimStart();// txtName.Text;            
            tmpIcebox.IceBoxName = iceboxName;
            iceBoxManage.SetTrans(trans);
            IceBox tmp = iceBoxManage.GetIceBoxByName(iceboxName);
            if (null != tmp && tmp.IceBoxName == iceboxName)
            {
                MessageBox.Show("冰箱名不能重复，请重新输入！", title);
                return false;
            }
            tmpIcebox.IsOccupy = Convert.ToInt16(0);
            tmpIcebox.Comment = txtComment.Text;
            if (cmbOrgOrBlood.SelectedValue != null)
            {
                tmpIcebox.OrgOrBlood = cmbOrgOrBlood.SelectedValue.ToString();
            }
            else
            {
                tmpIcebox.OrgOrBlood = "0";
            }
            if (cmbSpecType.SelectedValue != null)
            {
                tmpIcebox.SpecTypeId = cmbSpecType.SelectedValue.ToString();
            }
            else
            {
                tmpIcebox.SpecTypeId = "0";
            }
            tmpIcebox.UseStaus = "1";
            return true;
        }
        
        /// <summary>
        /// 设置冰箱层的标本类型和标本种类
        /// </summary>
        private void LayerSpecSetting()
        {
            if (cmbOrgOrBlood.SelectedValue == null)
            {
                return;
            }
            string orgID = cmbOrgOrBlood.SelectedValue.ToString();
            string specTypeID = "";
            if (curSelectedIceBox.SpecTypeId != ""&&curSelectedIceBox.SpecTypeId != null)
            {
                specTypeID = curSelectedIceBox.SpecTypeId.ToString();
            }
            if (cmbSpecType.SelectedValue != null)
            {
                specTypeID = cmbSpecType.SelectedValue.ToString();
            }
            foreach (ucIceBoxLayer ibl in ucLayer)
            {
                ibl.SetOrgClass(orgID);
                ibl.SetSpecType(specTypeID);
            }
            LayerSetting = 1;
        }
        #endregion

        #region 页面布局
        /// <summary>
        /// TableLayout 布局
        /// </summary>
        private void TableLayoutSet()
        {
            //计算table Layout中的布局         
            specCount = Convert.ToInt32(nudLayerSpec.Value);
            layerCount = Convert.ToInt32(nudLayerNum.Value);
            chkNum = specCount * layerCount;
            specIndex = 0;
            chkIndex = 0;
            tableCol = 2;
            if (Convert.ToBoolean(specCount % 2))
            {
                tableRow = (specCount / 2 + 1);
            }
            else
            {
                tableRow = specCount / 2;
            }
            //tableLayerout的列数
            tlpLayer.ColumnCount = tableCol;
            tlpLayer.RowCount = tableRow;
            chk = new NeuCheckBox[chkNum];
            ucLayer = new ucIceBoxLayer[specCount];            

        }

        /// <summary>
        /// 规格是偶数的布局
        /// </summary>
        private void DoulbeSet()
        {

            for (int j = 0; j < tlpLayer.RowCount; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.tlpLayer.Controls.Add(FlowAddControls(), i, j);
                    specIndex++;
                }
            }
            LayerSpecSetting();
        }

        /// <summary>
        /// 在TableLayout中加入ucIceBoxLayer 控件
        /// </summary>
        /// <returns>ucIceBoxLayer:需要加在页面上的控件</returns>
        private ucIceBoxLayer FlowAddControls()
        {
            ucIceBoxLayer layerTemp = new ucIceBoxLayer(txtIceBoxType.Tag.ToString());
            layerTemp.Controls.Find("flpChk", false);
            ControlCollection collection = layerTemp.Controls;
            foreach (Control c in collection)
            {
                if (c.Name == "flpChk")
                {
                    for (int k = 0; k < layerCount; k++)
                    {
                        NeuCheckBox chkTemp = new NeuCheckBox();
                        chkTemp.AutoSize = true;
                        chkTemp.Name = "chk" + chkIndex.ToString();
                        chkTemp.Text = "第" + (k + 1).ToString() + "层";
                        chkTemp.Width = 60;
                        chk[chkIndex] = chkTemp;
                        ((FlowLayoutPanel)c).Controls.Add(chk[chkIndex]);
                        chkIndex++;
                    }
                    break;
                }
            }
            layerTemp.Name = "layer" + specIndex.ToString();
            ucLayer[specIndex] = layerTemp; 
            return ucLayer[specIndex];

        }

        /// <summary>
        /// 规格是奇数的布局
        /// </summary>
        private void SingleTableLayout()
        {
            for (int j = 0; j < tlpLayer.RowCount - 1; j++)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.tlpLayer.Controls.Add(FlowAddControls(), i, j);
                    specIndex++;
                }
            }
            this.tlpLayer.Controls.Add(FlowAddControls(), 0, tlpLayer.RowCount - 1);
            LayerSpecSetting();

        }
        #endregion

        #region 数据校验
        /// <summary>
        /// 层与规格的数据校验
        /// </summary>
        /// <returns></returns>
        private bool SpecDataValidate()
        {
            if (txtIceBoxType.Text == "" || txtIceBoxType.Tag == null)
            {
                MessageBox.Show("请设置冰箱类型！", title);
                return false;
            }
            if (Convert.ToInt32(nudLayerSpec.Value) > Convert.ToInt32(nudLayerNum.Value))
            {
                MessageBox.Show("规格不能大于层数", title);
                return false;
            }
            string iceBoxName = txtName.Text.Trim();
            if (iceBoxName == "")
            {
                MessageBox.Show("冰箱名字不能为空", title);
                return false;
            }
            foreach (IceBox i in arrIcebox)
            {
                if (iceBoxName == i.IceBoxName)
                {
                    MessageBox.Show("冰箱名字不能重复", title);
                    return false;
                }
                break;
            }

            return true;
        }

        /// <summary>
        /// 层中数据的校验
        /// </summary>
        /// <returns>false：数据不合格</returns>
        private bool IceBoxDataValidate()
        {
            if (txtIceBoxType.Tag == null || txtIceBoxType.Text == "")
            {
                MessageBox.Show("请设置冰箱类型！", title);
                return false;
            }
            //选中的层列表，哪一层被选中了
            List<string> checkedLayerList = new List<string>();
            //循环遍历几种不同规格的冰箱层
            foreach (ucIceBoxLayer layer in ucLayer)
            {
                ControlCollection controls = layer.Controls;
                foreach (Control c in controls)
                {
                    //遍历FlowLayout中的CheckBox，查看哪个CheckBox被选中
                    if (c.Name.Contains("flpChk"))
                    {
                        ControlCollection flpControl = ((FlowLayoutPanel)c).Controls;
                        int checkedLayerIndex = 0;
                        foreach (Control flpc in flpControl)
                        {
                            NeuCheckBox tmpCheck = (NeuCheckBox)flpc;
                            string chkText = tmpCheck.Text;
                            if (tmpCheck.Checked)
                            {
                                //查看同一层是否被选中2次
                                if (checkedLayerList.Contains(chkText))
                                {
                                    MessageBox.Show("同一层不能设置2种以上规格", "层的设置");
                                    return false;
                                }
                                else
                                {
                                    checkedLayerList.Add(chkText);
                                }
                            }
                            //checkedIndex++;
                            checkedLayerIndex++;
                        }
                    }
                }
            }
            if (checkedLayerList.Count < Convert.ToInt32(nudLayerNum.Value))
            {
                MessageBox.Show("并不是每一层都设置了规格，请检查!", "冰箱层设置");
                return false;
            }
            return true;
        }
        #endregion

        #region 冰箱信息保存
        /// <summary>
        /// 保存每一层的设置
        /// </summary>
        /// <returns></returns>
        private bool IceBoxLayerSave(System.Data.IDbTransaction trans)
        {
            int checkedIndex = 0;
            shelfManage.SetTrans(trans);
            shelfSpecManage.SetTrans(trans);
            layerManage.SetTrans(trans);
            capLogManage.SetTrans(trans);

            try
            {
                foreach (ucIceBoxLayer layer in ucLayer)
                {                    
                    ControlCollection controls = layer.Controls;
                    foreach (Control c in controls)
                    {
                        if (c.Name.Contains("flpChk"))
                        {
                            ControlCollection flpControl = ((FlowLayoutPanel)c).Controls;
                            foreach (Control flpc in flpControl)
                            {
                                NeuCheckBox tmpCheck = (NeuCheckBox)flpc;
                                if (tmpCheck.Checked)
                                {
                                    IceBoxLayer layerTmp = new IceBoxLayer();                                   
                                    layer.LayerSaveObj(ref layerTmp); 
                                    layerTmp.IceBox = iceBoxManage.GetIceBoxByName(txtName.Text.TrimStart().TrimEnd());
                                    int layerNum = checkedIndex % layerCount + 1;
                                    layerTmp.LayerNum = Convert.ToInt16(layerNum);// as short;                                    
                                    string sequence = "";
                                    layerManage.GetNextSequence(ref sequence);
                                    layerTmp.LayerId = Convert.ToInt32(sequence);
                                    int r = layerManage.InsertIceBoxLayer(layerTmp);
                                    capLogManage.ModifyIceBoxLayer(new IceBoxLayer(), loginPerson.Name, "N", layerTmp, "新建冰箱层");
                                    //如果冰箱层中保存的冻存架
                                    //if (layerTmp.SaveType == 'J')
                                    //{
                                    //    int layerId = layerManage.GetLayerIDByIceBox(layerTmp.IceBox.IceBoxId.ToString(), layerNum.ToString());
                                    //    Shelf shelfTmp = new Shelf();
                                    //    for (int i = 1; i <= layerTmp.Col; i++)
                                    //    {
                                            
                                    //        string shelfsequence = "";
                                    //        shelfManage.GetNextSequence(ref shelfsequence);
                                    //        shelfTmp.ShelfID = Convert.ToInt32(shelfsequence);
                                    //        //架所在冰箱的行
                                    //        shelfTmp.IceBoxLayer.Row = 1;
                                    //        //架所在冰箱的列
                                    //        shelfTmp.IceBoxLayer.Col = i;
                                    //        //架在冰箱层的第几层
                                    //        shelfTmp.IceBoxLayer.Height = 1;
                                    //        shelfTmp.IceBoxLayer.LayerId = layerId;
                                    //        shelfTmp.SpecBarCode = layerTmp.IceBox.IceBoxId.ToString().PadLeft(3, '0') + layerTmp.LayerNum.ToString().Trim().PadLeft(2, '0') + i.ToString().Trim().PadLeft(2, '0');
                                    //        shelfTmp.ShelfSpec.ShelfSpecID = layerTmp.SpecID;
                                    //        shelfTmp.OccupyCount = 0;
                                    //        shelfTmp.Capacity = GetShelfSpec(shelfTmp.ShelfSpec.ShelfSpecID.ToString());
                                    //        shelfTmp.IsOccupy = '0';                                           
                                    //        int s = shelfManage.InsertShelf(shelfTmp);
                                    //    }
                                    //}
                                    //iceBoxTmp.Col
                                }
                                checkedIndex++;
                            }
                        }
                        break;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                //IceBoxLayerSave();
                string error = e.Message;
                return false;
            }

        }

        private int IceBoxSave(System.Data.IDbTransaction trans)
        {
            int i = 0;
            try
            {

                if (!IceBoxInfo(trans))
                    return i;
                if (!IceBoxDataValidate())
                {
                    return i;
                }

                iceBoxManage.SetTrans(trans);

                if (cmbIceBox.Text.Trim() == "")
                {
                    DialogResult resBut = MessageBox.Show("此操作将添加一个新的冰箱!", title, MessageBoxButtons.YesNo);
                    if (resBut == DialogResult.No)
                        return 0;
                    i = iceBoxManage.InsertIceBox(tmpIcebox);
                    if (i == -1) return i;
                    if (!IceBoxLayerSave(trans)) return -1;

                }

                else
                {
                    
                        DialogResult resBut = MessageBox.Show("此操作更新指定的冰箱名称!", title, MessageBoxButtons.YesNo);
                        if (resBut == DialogResult.No)
                            return 0;
                         
                    return iceBoxManage.ExecNoQuery(" update SPEC_ICEBOX set ICEBOXNAME = '" + txtName.Text + "' where ICEBOXID = " + cmbIceBox.SelectedValue.ToString());
                }

                return 1;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                return -1;
            }

        }
        #endregion

        #region 显示冰箱信息
        private void GetIceBoxShow()
        {
            txtName.Text = cmbIceBox.Text;
            ArrayList arr = new ArrayList();
            //获取冰箱中层的信息
            arr = layerManage.GetIceBoxLayers(cmbIceBox.SelectedValue.ToString());
            if (arr==null || arr.Count <= 0)
            {
                tlpLayer.Controls.Clear();
                return;
            }
            //arr = layerManage.GetIceBoxSpec("76");
            //获取冰箱中层的规格详细信息
            Dictionary<IceBoxLayer, List<int>> dicLayer = layerManage.GetLayerSpec(arr);            
            //获取冰箱信息
            curSelectedIceBox = iceBoxManage.GetIceBoxByName(cmbIceBox.Text);
            cmbOrgOrBlood.SelectedValue = Convert.ToInt32(curSelectedIceBox.OrgOrBlood);
            cmbSpecType.SelectedValue = Convert.ToInt32(curSelectedIceBox.SpecTypeId);
            txtComment.Text = curSelectedIceBox == null ? "" : curSelectedIceBox.Comment;
            //foreach (Control c in gpBaseInfo.Controls)
            //{
            //    if (c.Name != "cmbIceBox" || c.Name != "txtIceBoxType")
            //        c.Enabled = false;
            //}
            //设置冰箱有多少层
            nudLayerNum.Value = curSelectedIceBox.LayerNum;
            //设置冰箱有几种规格
            nudLayerSpec.Value = dicLayer.Count;
            this.tlpLayer.Controls.Clear();
            //计算布局
            TableLayoutSet();
            int specCount = Convert.ToInt32(nudLayerSpec.Value);
            if (specCount <= 0)
                return;
            if (specCount % 2 == 0)
            {
                DoulbeSet();
            }
            else
            {
                SingleTableLayout();
            }

            int ucIndex = 0;

            foreach (KeyValuePair<IceBoxLayer, List<int>> item in dicLayer)
            {
                ucIceBoxLayer tmpucLayer = new ucIceBoxLayer(txtIceBoxType.Tag.ToString());
                tmpucLayer = ucLayer[ucIndex];
                IceBoxLayer tmpLayer = new IceBoxLayer();
                tmpLayer = item.Key;
                tmpucLayer.SetValue(tmpLayer);
                ControlCollection cLayer = tmpucLayer.Controls;
                //foreach (Control c in cLayer)
                //{
                //    if (c.Name == "cmbDisType")
                //    {
                //        System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)c;
                //        cmb.SelectedValue = tmpLayer.DiseaseType.DisTypeID;
                //        break;
                //    }
                //}
                #region 如果存放的是架子
                //if (tmpLayer.SaveType == 'J')
                //{
                //    foreach (Control c in cLayer)
                //    {
                //        if (c.Name == "rbtShelf")
                //        {
                //            RadioButton rb = c as RadioButton;
                //            rb.Checked = true;
                //        }
                //        if (c.Name == "cmbShelf")
                //        {
                //            System.Windows.Forms.ComboBox cmb = c as System.Windows.Forms.ComboBox;
                //            cmb.SelectedValue = tmpLayer.SpecID;
                //        }
                //        if (c.Name == "nudShelfCount")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Col;
                //        }
                //    }
                //}
                #endregion
                #region 如果存放的是标本盒
                //if (tmpLayer.SaveType == 'B')
                //{
                //    foreach (Control c in cLayer)
                //    {
                //        if (c.Name == "rbtSpec")
                //        {
                //            RadioButton rb = c as RadioButton;
                //            rb.Checked = true;
                //        }
                //        if (c.Name == "cmbSpec")
                //        {
                //            System.Windows.Forms.ComboBox cmb = c as System.Windows.Forms.ComboBox;
                //            cmb.SelectedValue = tmpLayer.SpecID;
                //        }
                //        if (c.Name == "nudRow")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Row;
                //        }
                //        if (c.Name == "nudCol")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Col;
                //        }
                //        if (c.Name == "nudHeight")
                //        {
                //            NumericUpDown nud = c as NumericUpDown;
                //            nud.Value = tmpLayer.Height;
                //        }
                //    }
                //}
                #endregion
                List<int> tmpNum = item.Value;
                foreach (int i in tmpNum)
                {
                    foreach (Control c in cLayer)
                    {
                        if (c.Name.Contains("flp"))
                        {
                            FlowLayoutPanel flp = c as FlowLayoutPanel;
                            ControlCollection flpControls = flp.Controls;
                            foreach (Control fc in flpControls)
                            {
                                //NeuCheckBox neuChk = c as NeuCheckBox;
                                chk[ucIndex * layerCount + i - 1].Checked = true;
                            }
                            break;
                        }
                    }
                }
                ucIndex++;
            }

        }
        #endregion

        private void DisuseIceBox(int iceboxId)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                SpecBoxManage tmpManage = new SpecBoxManage();
                tmpManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ArrayList arrLayer = layerManage.GetIceBoxLayers(iceboxId.ToString());
                ArrayList arrShelf;
                ArrayList arrBox;

                foreach (IceBoxLayer l in arrLayer)
                {
                    IceBoxLayer tmpl = l;
                    #region 如果保存的是架子
                    if (tmpl.SaveType == 'J')
                    {
                        arrShelf = new ArrayList();
                        arrShelf = shelfManage.GetShelfByLayerID(tmpl.LayerId.ToString());
                        foreach (Shelf s in arrShelf)
                        {
                            Shelf tmps = s;
                            arrBox = new ArrayList();
                            arrBox = tmpManage.GetBoxByCap(tmps.ShelfID.ToString(), 'J');
                            foreach (SpecBox b in arrBox)
                            {
                                //废除冰箱中的标本盒
                                SpecBox tmpb = b;
                                tmpb.DesCapCol = 0;
                                tmpb.DesCapID = 0;
                                tmpb.DesCapRow = 0;
                                tmpb.DesCapSubLayer = 0;
                                tmpb.DesCapType = '0';
                                tmpb.IsOccupy = '0';
                                tmpb.OccupyCount = 0;
                                if (tmpManage.UpdateSpecBox(tmpb) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("操作失败！", title);
                                    return;
                                }
                                if (capLogManage.ModifyBoxLog(b, loginPerson.Name, "D", tmpb, "冰箱废除") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("操作失败！", title);
                                    return;
                                }
                            }
                            tmps.IceBoxLayer.LayerId = 0;
                            tmps.IceBoxLayer.Col = 0;
                            tmps.IceBoxLayer.Row = 0;
                            tmps.IceBoxLayer.Height = 0;
                            tmps.IsOccupy = '0';
                            tmps.OccupyCount = 0;
                            if (shelfManage.UpdateShelf(tmps) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("操作失败！", title);
                                return;
                            }
                            if (capLogManage.ModifyShelf(s, loginPerson.Name, "D", tmps, "冰箱废除") <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("操作失败！", title);
                                return;
                            }
                        }
                    }
                    #endregion
                    #region 如果保存的是标本盒
                    if (l.SaveType == 'B')
                    {
                        arrBox = new ArrayList();
                        arrBox = tmpManage.GetBoxByCap(l.LayerId.ToString(), 'B');
                        foreach (SpecBox b1 in arrBox)
                        {
                            //废除冰箱中的标本盒
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
                            if (capLogManage.ModifyBoxLog(b1, loginPerson.Name, "D", tmpb1, "冰箱废除")<=0)
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
                    int result = capLogManage.ModifyIceBoxLayer(l, loginPerson.Name, "D", tmpl, "冰箱废除");
                    if (result<=0)
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            if (!SpecDataValidate()) return -1;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                int result = IceBoxSave(FS.FrameWork.Management.PublicTrans.Trans);
                switch (result)
                {
                    case -1:
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！", title);
                        return -1;                      
                    case 1:
                        MessageBox.Show("保存成功!", title);
                        break;
                    default:
                        return 0;
                        //break;

                }
               FS.FrameWork.Management.PublicTrans.Commit();
                //重新绑定IceBox
                if (txtIceBoxType.Tag != null && txtIceBoxType.Text.Trim() != "")
                {
                    BindingIceBox(txtIceBoxType.Tag.ToString());
                }
                //选择为当前的iceboxid
                cmbIceBox.SelectedValue = tmpIcebox.IceBoxId;
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败！消息：" + ex.Message, title);
            }
            return base.Save(sender, neuObject);
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
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (Control c in gpBaseInfo.Controls)
            {
                c.Enabled = true;
            }
            switch (e.ClickedItem.Text.Trim())
            {
                case "报废":
                    if (cmbIceBox.SelectedValue == null || cmbIceBox.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("获取冰箱失败！", title);
                        return;
                    }
                    int iceboxId = Convert.ToInt32(cmbIceBox.SelectedValue.ToString());
                    if (iceBoxManage.CheckIceBoxHaveSpecBox(iceboxId.ToString()) == 1)
                    {
                        MessageBox.Show("冰箱中存有标本，不能废除", title);
                        return;
                    }
                    DialogResult dialog = MessageBox.Show("此操作将废除冰箱中所有存放的标本架和标本盒信息，继续？", title, MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                        return;
                    DisuseIceBox(iceboxId);
                    BindingIceBox(txtIceBoxType.Tag.ToString());
                    break;
                case "添加":
                    tlpLayer.Controls.Clear();
                    txtIceBoxType.Text = "";
                    txtName.Text = "";
                    nudLayerNum.Value = 1;
                    nudLayerSpec.Value = 1;
                    txtComment.Text = "";
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
      
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                if (!SpecDataValidate())
                {
                    return;
                }
                this.tlpLayer.Dock = DockStyle.Fill;
                this.tlpLayer.Controls.Clear();
                //TableRowColSet();
                TableLayoutSet();
                int specCount = Convert.ToInt32(nudLayerSpec.Value);
                if (specCount <= 0)
                    return;
                if (specCount % 2 == 0)
                {
                    //DoubleTableLayout();
                    DoulbeSet();

                }
                else
                {
                    SingleTableLayout();
                }
            }
            catch
            {
 
            }

        }

        /// <summary>
        /// 添加 tlpLayer容器的滚动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpLayer_Scroll(object sender, ScrollEventArgs e)
        {
            tlpLayer.PerformLayout();
            tlpLayer.Focus();
        }

        private void tlpLayer_MouseWheel(object sender, MouseEventArgs e)
        {
            tlpLayer.Focus();
        }

        /// <summary>
        /// 根据下拉列表选中冰箱的规格显示在页面上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbIceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIceBox.Text.Trim() != "")
            {
                GetIceBoxShow();
            }
            txtName.Text = "";           
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrgOrBlood.SelectedValue == null)
            {
                return;
            }
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            BindingSpecType(orgId);
            if (LayerSetting == 1)
            {
                LayerSpecSetting();
            }
        }

        private void cmbSpecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayerSetting == 1)
            {
                LayerSpecSetting();
            }
        }

        private void ucIceBox_Load(object sender, EventArgs e)
        {         
            BindingSpecClass();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //查询冰箱类型列表
            ArrayList iceBoxTypeList = new ArrayList();
            iceBoxTypeList = con.GetList("ICEBOXTYPE");
            this.txtIceBoxType.AddItems(iceBoxTypeList);
        }

        private void txtIceBoxType_TextChanged(object sender, EventArgs e)
        {
            if (txtIceBoxType.Text.Trim() != "" && txtIceBoxType.Tag != null)
            {
                BindingIceBox(txtIceBoxType.Tag.ToString());
            }
        }
    }
}
