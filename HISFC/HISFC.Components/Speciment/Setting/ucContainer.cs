using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class ucContainer : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private IceBoxManage iceBoxManage;
        private IceBoxLayerManage layerManage;
        private ShelfManage shelfManage;
        private SpecBoxManage specBoxManage;
        private BoxSpecManage boxSpecManage;
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private DisTypeManage disTypeManage;

        StringBuilder sb = new StringBuilder();

        private ArrayList arrIceBoxList;
        private ArrayList arrLayerList;
        private ArrayList arrBoxList;
        private ArrayList arrShelfList;

        private List<SubSpec> specList = new List<SubSpec>();
        private List<SpecBox> boxList = new List<SpecBox>();

        private Dictionary<Position, SpecBox> dicBoxPos;
        private Dictionary<Position, SubSpec> dicSubSpec;

        //用于记录标本盒或架子时的原节点
        private TreeNode tnParent;       

        private int rowIndex = -1;
        private int colIndex = -1;

        //标本位于盒子中的行，列索引，用于盒子中的标本调整
        private int specRowIndex = -1;
        private int specColIndex = -1;
        //用于打印移动位置信息
        private FarPoint.Win.Spread.SheetView sheetView;
        //是否已经打印了调整的位置，如果打印了退出时不提示，如果没有提示用户打印
        private bool isPrint = true;

        //记录标本盒是否已经移动，不然当拖动标本盒到数菜单上时会操作2次
        private bool isDrag = false;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        #region 属性
        /// <summary>
        /// 当前标本盒
        /// </summary>
        private SpecBox curBox = new SpecBox();
        public SpecBox CurBox
        {
            set
            {
                curBox = value;
            }
        }

        private Shelf curShelf = new Shelf();
        public Shelf CurShelf
        {
            set
            {
                curShelf = value;
            }
        }
        #endregion

        private enum SheetCols
        {
            类型,
            原条码,
            原位置,
            新条码,
            新位置,
        }

        public ucContainer()
        {
            InitializeComponent();           
            iceBoxManage = new IceBoxManage();
            layerManage = new IceBoxLayerManage();
            shelfManage = new ShelfManage();
            specBoxManage = new SpecBoxManage();
            boxSpecManage = new BoxSpecManage();
            disTypeManage = new DisTypeManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            arrIceBoxList = new ArrayList();
            arrLayerList = new ArrayList();
            arrShelfList = new ArrayList();
            arrBoxList = new ArrayList();
            dicBoxPos = new Dictionary<Position, SpecBox>();
            dicSubSpec = new Dictionary<Position, SubSpec>();
            sheetView = new FarPoint.Win.Spread.SheetView();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        }

        /// <summary>
        /// 绑定标本类型
        /// </summary>
        /// <param name="orgId"></param>
        private void BindingSpecType()
        {
            ArrayList arrTmp = new ArrayList(); 
            cmbSpecType.DataSource = null;
            arrTmp = specTypeManage.GetAllSpecType();
            if (arrTmp.Count > 0)
            {
                cmbSpecType.DisplayMember = "SpecTypeName";
                cmbSpecType.ValueMember = "SpecTypeID";
                cmbSpecType.DataSource = arrTmp;
                cmbSpecType.Text = "";
            }
        }

        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void BindingDisType()
        {
            //disTypeManage = new DisTypeManage();
            //Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDisType.AddItems(alDisType);
                }
            }
            cmbDisType.Text = "";
        }

        /// <summary>
        /// 获取标本架存放的类型
        /// </summary>
        /// <param name="shelfId"></param>
        /// <param name="disType"></param>
        /// <param name="specType"></param>
        /// <returns></returns>
        private string GetShelfDisAndType(string shelfId,string disType,string specType)
        {

            ArrayList arrSpecBox = specBoxManage.GetBoxByCap(shelfId, 'J');
            try
            {
                DiseaseType dis = new DiseaseType();;
                FS.HISFC.Models.Speciment.SpecType st = new FS.HISFC.Models.Speciment.SpecType();
                if (arrSpecBox != null && arrSpecBox.Count > 0)
                {
                    SpecBox box = arrSpecBox[0] as SpecBox;
                    dis = disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString());
                    st = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString());
                }
                else
                {
                    dis = disTypeManage.SelectDisByID(disType);
                    st = specTypeManage.GetSpecTypeById(specType);
                }
                return dis.DiseaseName + " " + st.SpecTypeName;                
            }
            catch
            { }
            return "";
        }

        /// <summary>
        /// 根据类型绑定冰箱层
        /// </summary>
        /// <param name="type">S: 冻存架，B 标本盒</param>
        private void GetLayer(string type)
        {
            Size size = new Size(1028, 1000);
            this.ParentForm.Size = size;
            this.Size = size;
            if (type == "")
            { }
            IceBox tmpIceBox = new IceBox();
            IceBoxLayer tmpLayer = new IceBoxLayer();
            Shelf tmpShelf = new Shelf();

            #region 获取冰箱信息
            if (type == "B")
            {
                string tmpBoxId = curBox.BoxId.ToString();
               //IceBoxLayer tmpLayer= layerManage 
                tmpIceBox = iceBoxManage.GetIceBoxBySpecBoxId(tmpBoxId);
                if (tmpIceBox == null)
                {
                    return;
                }
                tmpLayer = layerManage.GetLayerBySpecBox(tmpBoxId);
                if (tmpLayer == null)
                {
                    return;
                }
                tmpShelf = shelfManage.GetShelfByBoxId(tmpBoxId);
                if (tmpShelf == null)
                {
                    return;
                }
            }
            if (type == "J")
            {
                string tmpShelfId = curShelf.ShelfID.ToString();
                //IceBoxLayer tmpLayer= layerManage 
                tmpIceBox = iceBoxManage.GetIceBoxByShelf(tmpShelfId);
                if (tmpIceBox == null)
                {
                    return;
                }
                tmpLayer = layerManage.GetLayerByShelf(tmpShelfId);
                if (tmpLayer == null)
                {
                    return;
                }
                tmpShelf =curShelf;
                if (tmpShelf == null)
                {
                    return;
                }
            }
     #endregion
 
            TreeNode root = new TreeNode();
            root.Tag = tmpIceBox;
            root.Text = tmpIceBox.IceBoxName + " (共" + tmpIceBox.LayerNum.ToString() + "层)";
            this.tvIceBox.Nodes.Add(root);

            TreeNode layerRoot = new TreeNode();
            layerRoot.Tag = tmpLayer;
            layerRoot.Text = "第 " + tmpLayer.LayerNum + " 层 (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
            root.Nodes.Add(layerRoot);
            TreeNode shelfRoot = new TreeNode();
            shelfRoot.Tag = tmpShelf;
            string shelfBarCode = tmpShelf.SpecBarCode;
            shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + tmpShelf.OccupyCount.ToString() + "/" + tmpShelf.Capacity.ToString() + ")";
            shelfRoot.Text += this.GetShelfDisAndType(tmpShelf.ShelfID.ToString(), tmpShelf.DisTypeId.ToString(), tmpShelf.SpecTypeId.ToString());
            layerRoot.Nodes.Add(shelfRoot);
            tvIceBox.SelectedNode = shelfRoot;

            if (type == "B")
            {
                dgvBox.SelectionMode = DataGridViewSelectionMode.CellSelect;
                for(int i =0;i<dgvBox.ColumnCount;i++)
                    for (int k = 0; k < dgvBox.RowCount; k++)
                    {
                        if (dgvBox[i, k].Selected)
                            dgvBox[i, k].Selected = false;
                    }
                int tmpSubLayer = curBox.DesCapSubLayer;
                int tmpRow = curBox.DesCapRow;
                dgvBox[tmpRow-1,dgvBox.RowCount - tmpSubLayer].Selected = true;
                DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(tmpRow - 1, dgvBox.RowCount - tmpSubLayer);
                dgvBox_CellClick(null,e);
            }
            tvIceBox.ExpandAll();
        }

        private void GetLayerByShelf()
        {
            if (curShelf == null || curShelf.ShelfID <= 0)
            {
                return;
            }
            this.GetLayer("S");
        }

        private void GetLayerBySpecBox()
        {
            if (curBox == null || curBox.BoxId <= 0)
            {
                return;
            }
            this.GetLayer("B");
        }


        /// <summary>
        /// 获取所有冰箱
        /// </summary>
        private void GetAllIceBox()
        {
            arrIceBoxList.Clear();
           //arrIceBoxList = new ArrayList();
           arrIceBoxList = iceBoxManage.GetAllIceBox();           
        }

        /// <summary>
        /// 根据冰箱Id获取冰箱层
        /// </summary>
        /// <param name="iceBoxId"></param>
        private void GetLayerByIceBoxId(string iceBoxId)
        {
            arrLayerList.Clear();
            //arrLayerList = new ArrayList();
            arrLayerList = layerManage.GetIceBoxLayers(iceBoxId);
        }

        /// <summary>
        /// 根据冰箱层ID获取所有架子
        /// </summary>
        /// <param name="layerId"></param>
        private void GetShelfByLayerId(string layerId)
        {
            arrShelfList.Clear();
            //arrShelfList = new ArrayList();
            arrShelfList = shelfManage.GetShelfByLayerID(layerId);
        }

        /// <summary>
        /// 根据架子ID获取所有标本盒
        /// </summary>
        /// <param name="shelfId"></param>
        private void GetBoxByShelfId(string shelfId)
        {
            arrBoxList.Clear();
            arrBoxList = specBoxManage.GetBoxByCap(shelfId,'J');
        }

        /// <summary>
        /// 根据层Id获取所有标本盒
        /// </summary>
        /// <param name="layerId"></param>
        private void GetBoxByLayerId(string layerId)
        {
            arrLayerList.Clear();
            arrLayerList = specBoxManage.GetBoxByCap(layerId, 'B');
        }

        /// <summary>
        /// 移动标本
        /// </summary>
        private void CopySubSpec()
        {
            specList.Clear();
            try
            { 
                for (int i = dgvSpec.Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = dgvSpec.Columns.Count - 1; j >= 0; j--)
                    {
                        DataGridViewCell cell = dgvSpec[j, i];
                        if (cell.Selected)
                        {
                            SubSpec s = cell.Tag as SubSpec;
                            if (s == null)
                                continue;
                            specList.Add(cell.Tag as SubSpec);
                        }
                    }
                }
                specList.Sort(new SpecSort());
                
            }
            catch
            {
                specList.Clear();
            }
        }

        /// <summary>
        /// 移动标本
        /// </summary>
        private void TransferSpec()
        {
            DialogResult r = MessageBox.Show("移动标本,是否继续?","提示", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                specList.Clear();
                return;
            }
            if (specList.Count <= 0)
                return;
            if (dgvBox.SelectedCells.Count > 1)
            {
                MessageBox.Show("请不要选择多个标本盒");
                return;
            }
            if(dgvBox.SelectedCells.Count == 0)
            {
                MessageBox.Show("请选择目标标本盒");
                return;
            }

            SpecBox box = dgvBox.SelectedCells[0].Tag as SpecBox;
            if (box == null)
            {
                MessageBox.Show("获取标本盒失败");
                return;
            }

            //重新再获取一次
            box = specBoxManage.GetBoxById(box.BoxId.ToString());

            if (box == null)
            {
                MessageBox.Show("获取标本盒失败");
                return;
            }          
           

            SubSpec sub = specList[0];
            if (sub.SpecTypeId != box.SpecTypeID)
            {
                MessageBox.Show("标本类型不相符");
                return;
            }

            if (sub.BoxId != box.BoxId)
            {
                if (box.Capacity - box.OccupyCount < specList.Count)
                {
                    MessageBox.Show("当前标本盒空间不够！");
                    return;
                }
            } 

            r = MessageBox.Show("移动标本,是否继续?", "提示", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                specList.Clear();
                return;
            }

            SpecInOper tmpIn = new SpecInOper();                       //BX005-C5-J1-11
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            tmpIn.Trans = FS.FrameWork.Management.PublicTrans.Trans;
            tmpIn.SetTrans();

            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (sub.BoxId == box.BoxId)
                {
                    boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    BoxSpec tmp = new BoxSpec();
                    tmp = boxSpecManage.GetSpecByBoxId(sub.BoxId.ToString());
                    subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    int tmpRow;
                    int tmpCol;
                    #region 增加判断前边是否有空位置，防止操作时没有选择目标标本盒就直接在当前已满的标本盒移动lingk20110622
                    int vpNum = 0;
                    int rts = subSpecManage.ScanSpecBox(sub.BoxId.ToString(), out vpNum);
                    if (rts < 0) //查询出错
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！查询标本盒空位出错！");
                        specList.Clear();
                        return;
                    }
                    else
                    {
                        if (specList.Count <= tmp.Capacity) //全选排序是允许的
                        {
                        }
                        //如果标本空位数小于选中移动标本数and选中移动标本数小于总标本盒容量（及没有全选）
                        else if ((vpNum < specList.Count) || (specList.Count < tmp.Capacity))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败！标本盒空位不够！如果需要在本标本盒排序请全选，如果移动到其他标本盒请选择目标标本盒！");
                            specList.Clear();
                            return;
                        }
                    }
                    #endregion
                    for (int i = 0; i < specList.Count; i++)
                    {
                        tmpRow = (i + 1) % tmp.Col == 0 ? (i + 1) / tmp.Col : (i + 1) / tmp.Col + 1;
                        tmpCol = (i + 1) % tmp.Col == 0 ? tmp.Col : (i + 1) % tmp.Col;
                        sub = specList[i];
                        sub.BoxEndRow = tmpRow;
                        sub.BoxEndCol = tmpCol;
                        sub.BoxStartCol = tmpCol;
                        sub.BoxStartRow = tmpRow;

                        #region 增加判断前边是否有空位置，防止操作时没有选择目标标本盒就直接在当前已满的标本盒移动lingk20110622
                        //SubSpec tmpSS = new SubSpec();
                        //tmpSS = subSpecManage.GetSubSpecByLocate(sub.BoxId, sub.BoxEndCol, sub.BoxEndRow, sub.BoxStartCol, sub.BoxStartRow);
                        //if (tmpSS != null) //原来位置存在标本
                        //{
                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("操作失败！");
                        //    specList.Clear();
                        //    return;
                        //}
                        //貌似如果更新了update又使用select取数的话存储与查询数据都会有问题（db2用with ur不知道可行否），前边有标本信息移动后再次循环就会有问题了
                        #endregion

                        if (subSpecManage.UpdateSubSpec(sub) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败！");
                            specList.Clear();
                            return;
                        }                         
                    }
                }
                else
                {
                    for (int i = 0; i < specList.Count; i++)
                    {
                        sub = specList[i];
                        SpecBox sourceBox = specBoxManage.GetBoxById(sub.BoxId.ToString());
                        SpecBox desBox = specBoxManage.GetBoxById(box.BoxId.ToString());
                        if (tmpIn.TransferSpec(sourceBox, desBox, sub) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败！");
                            specList.Clear();
                            return;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功!");
                specList.Clear();
            }
            catch
            {
                MessageBox.Show("操作失败！");
                specList.Clear();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
 
            
        }

        /// <summary>
        /// 移动标本盒
        /// </summary>
        private void CopyBox()
        {
            boxList.Clear();

            if (dgvBox.Tag == null)
            {
                return;
            }

            try
            {
                for (int i = dgvBox.Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = dgvBox.Columns.Count - 1; j >= 0; j--)
                    {
                        DataGridViewCell cell = dgvBox[j, i];
                        if (cell.Selected)
                        {
                            SpecBox s = cell.Tag as SpecBox;
                            if (s == null)
                                continue;
                            boxList.Add(cell.Tag as SpecBox);
                        }
                    }
                }
                boxList.Sort(new BoxSort());

            }
            catch
            {
                boxList.Clear();
            }
        }

        /// <summary>
        /// 移动标本盒
        /// </summary>
        private void TransferBox()
        {
            DialogResult r = MessageBox.Show("移动标本盒,是否继续?", "提示", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                boxList.Clear();
                return;
            }
            if (boxList.Count <= 0)
                return;

            if (tvIceBox.SelectedNode.Tag == null)
            {
                return;
            }
            Shelf shelf = new Shelf();
            try
            {
                if (dgvBox.SelectedCells.Count > 1)
                {
                    MessageBox.Show("请不要选择多个冻存架!");
                    return;
                }
                shelf = dgvBox.SelectedCells[0].Tag as Shelf;
                if (shelf == null || shelf.ShelfID == 0)
                {
                    if (!tvIceBox.SelectedNode.Tag.GetType().ToString().Contains("Shelf"))
                    {
                        MessageBox.Show("请选择冻存架!");
                        return;
                    }

                    shelf = tvIceBox.SelectedNode.Tag as Shelf;
                }
            }
            catch
            { 
            }


            if (shelf == null || shelf.ShelfID == 0)
            {
                MessageBox.Show("请选择目标冻存架！");
                return;
            }
            
            //重新再获取一次
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");

            if (shelf == null)
            {
                MessageBox.Show("获取冻存架失败");
                return;
            }


            SpecBox box = boxList[0];

            if (shelf.SpecTypeId != 9)
            {

                if (box.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("标本类型不相符");
                    return;
                }
            }

            int boxCount = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J').Count;

            if (box.DesCapID != shelf.ShelfID)
            {
                if (shelf.Capacity - boxCount < boxList.Count)
                {
                    MessageBox.Show("当前架子空间不够！");
                    return;
                }
            } 

            r = MessageBox.Show("移动标本盒,是否继续?", "提示", MessageBoxButtons.YesNo);
            if (r == DialogResult.No)
            {
                boxList.Clear();
                return;
            }

            ShelfSpecManage shelfSpecMgr = new ShelfSpecManage();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            shelfSpecMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (box.DesCapID == shelf.ShelfID)
                {

                    boxList.Clear();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return;
                }
                else
                {
                    if (shelfManage.UpdateIsFull("0", box.DesCapID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！");
                        return;
                    }

                    for (int i = 0; i < boxList.Count; i++)
                    {
                        box = new SpecBox();
                        box = boxList[i];
                        Shelf sourceShelf = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(), "");
                        Shelf desShelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");

                        int sourceCount = specBoxManage.GetBoxByCap(box.DesCapID.ToString(), 'J').Count;
                        int desCount = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J').Count;

                        ShelfSpec tmpShelfSpec = new ShelfSpec();
                        tmpShelfSpec = shelfSpecMgr.GetShelfByShelf(desShelf.ShelfID.ToString());                        

                        //当前标本盒是否定位
                        bool located = false; 

                        //循环遍历目标冻存架，哪里有空位放哪里
                        for (int rs = 1; rs <= tmpShelfSpec.Row; rs++)
                        {
                            if (located)
                            {
                                break;
                            }
                            for (int h = 1; h <= tmpShelfSpec.Height; h++)
                            {
                                if (specBoxManage.GetByPoint("1", rs.ToString(), h.ToString(), desShelf.ShelfID.ToString(), "J") == null)
                                {
                                    box.DesCapRow = rs;
                                    box.DesCapSubLayer = h;
                                    box.BoxBarCode = desShelf.SpecBarCode + "-" + rs.ToString() + h.ToString();
                                    box.DesCapID = desShelf.ShelfID;
                                    if (specBoxManage.UpdateSpecBox(box) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("操作失败！");                                        
                                        return;
                                    }

                                    //更新新冻存架
                                    if (shelfManage.UpdateOccupyCount((desCount + 1).ToString(), shelf.ShelfID.ToString()) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("操作失败！");                                        
                                        return;
                                    }

                                    if (desCount + 1 == desShelf.OccupyCount)
                                    {
                                        if (shelfManage.UpdateIsFull("1", desShelf.ShelfID.ToString()) <= 0)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("操作失败！");
                                            return;
                                        }
                                    }
                                    //更新旧冻存架
                                    if (shelfManage.UpdateOccupyCount((sourceCount - 1).ToString(), sourceShelf.ShelfID.ToString()) <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("操作失败！");
                                        return;
                                    }
                                    located = true;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功!");

            }
            catch
            {
                MessageBox.Show("操作失败！");
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            finally
            {
                boxList.Clear();
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTree()
        {             
            this.tvIceBox.Nodes.Clear();
            this.tvIceBox.AfterSelect += this.TreeClick;
            this.tvIceBox.MouseDown += this.lv_MouseDown;
            this.tvIceBox.DragDrop += this.lv_DragDrop;
            this.tvIceBox.DragEnter += this.lv_DragEnter;
            if (curBox != null && curBox.BoxId > 0)
            {
                this.GetLayerBySpecBox();
                return;
            }
            if (curShelf != null && curShelf.ShelfID > 0)
            {
                this.GetLayerByShelf();
                return;
            }
            GetAllIceBox();
            if (arrIceBoxList == null || arrIceBoxList.Count <= 0)
            {
                return;
            }
            this.tvIceBox.Font = new Font ("宋体", 12);
            //foreach (ColumnHeader ch in nlvSpecContainer.c)
            //{
               
            //}
            //nlvSpecContainer.Font = new Font ("宋体", 14);
            foreach (IceBox i in arrIceBoxList)
            {
                if (i.IceBoxTypeId == "2")
                {
                    continue;
                }
                TreeNode root = new TreeNode();
                root.Tag = i;
                root.Text = i.IceBoxName + " (共" + i.LayerNum.ToString() + "层)";
                this.tvIceBox.Nodes.Add(root);
                GetLayerByIceBoxId(i.IceBoxId.ToString());
                foreach (IceBoxLayer layer in arrLayerList)
                {
                    TreeNode layerRoot = new TreeNode();
                    layerRoot.Tag = layer;
                    layerRoot.Text = "第 " + layer.LayerNum + " 层 (" + layer.OccupyCount.ToString() + "/" + layer.Capacity.ToString() + ")";
                    root.Nodes.Add(layerRoot);
                    if (layer.SaveType == 'J')
                    {
                        GetShelfByLayerId(layer.LayerId.ToString());
                        foreach (Shelf shelf in arrShelfList)
                        {
                            TreeNode shelfRoot = new TreeNode();
                            shelfRoot.Tag = shelf;
                            string shelfBarCode = shelf.SpecBarCode;
                            shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + shelf.OccupyCount.ToString() + "/" + shelf.Capacity.ToString() + ")";
                            shelfRoot.Text += this.GetShelfDisAndType(shelf.ShelfID.ToString(), shelf.DisTypeId.ToString(), shelf.SpecTypeId.ToString());
                            layerRoot.Nodes.Add(shelfRoot);                            
                        }
                    }
                    //if (layer.SaveType == 'B')
                    //{
                    //    for (int k = 1; k <= layer.Col; k++)
                    //    {
                    //        TreeNode boxRoot = new TreeNode();
                    //        boxRoot.Tag = k;
                    //        boxRoot.Text = "第 " + k.ToString() + " 列";
                    //        layerRoot.Nodes.Add(boxRoot);
                    //    }
                    //}
                }
            }
            tvIceBox.CollapseAll();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            bool selAll = false;
            if (cmbDisType.Text.Trim() == "" && cmbSpecType.Text.Trim() == "")
            {
                chkTime.Checked = true;
                selAll = true;
            }
            tvIceBox.Nodes.Clear();
            int disTypeId = 0;
            if (cmbDisType.Text.Trim() != "")
            {
                disTypeId = Convert.ToInt32(cmbDisType.Tag.ToString());
            }

            int specTypeId = 0;
            if (cmbSpecType.Text.Trim() != "")
            {
                specTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
            }

            //冰箱列表
            arrIceBoxList = new ArrayList();
            //层列表
            arrLayerList = new ArrayList();
            //架子列表
            arrShelfList = new ArrayList();
            if (!chkTime.Checked)
            {
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetLayerByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
            }
            if (chkTime.Checked)
            {
                string start= dtpStart.Value.Date.ToString();
                string end = dtpEnd.Value.Date.AddDays(1.0).ToString();
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(),start,end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                if (selAll)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTime(start, end);
                    arrLayerList = layerManage.GetIceBoxByStoreTime(start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTime(start, end);
                }
            }

            #region 循环遍历容器列表 加载到树形菜单
            foreach (IceBox tmpIceBox in arrIceBoxList)
            {
                TreeNode root = new TreeNode();
                root.Tag = tmpIceBox;
                root.Text = tmpIceBox.IceBoxName + " (共" + tmpIceBox.LayerNum.ToString() + "层)";
                this.tvIceBox.Nodes.Add(root);

                foreach (IceBoxLayer tmpLayer in arrLayerList)
                {
                    if (tmpLayer.IceBox.IceBoxId == tmpIceBox.IceBoxId)
                    {
                        TreeNode layerRoot = new TreeNode();
                        layerRoot.Tag = tmpLayer;
                        layerRoot.Text = "第 " + tmpLayer.LayerNum + " 层 (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        root.Nodes.Add(layerRoot);
                        foreach (Shelf tmpShelf in arrShelfList)
                        {
                            if (tmpShelf.IceBoxLayer.LayerId == tmpLayer.LayerId)
                            {
                                TreeNode shelfRoot = new TreeNode();
                                shelfRoot.Tag = tmpShelf;
                                string shelfBarCode = tmpShelf.SpecBarCode;
                                shelfRoot.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + tmpShelf.OccupyCount.ToString() + "/" + tmpShelf.Capacity.ToString() + ")";
                                if (tmpShelf.OccupyCount < tmpShelf.Capacity)
                                {
                                    shelfRoot.ImageIndex = 2;
                                }
                                shelfRoot.Text += this.GetShelfDisAndType(tmpShelf.ShelfID.ToString(), tmpShelf.DisTypeId.ToString(), tmpShelf.SpecTypeId.ToString());
                                layerRoot.Nodes.Add(shelfRoot);
                            }
                        }
                    }
                }
            }
            tvIceBox.ExpandAll();

            selAll = false;            
            #endregion
        }

        private void SetContainer(IceBoxLayer l, Shelf s)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在获取信息，请稍候...");
                Application.DoEvents();
                this.dgvBox.Rows.Clear();
                this.dgvBox.Columns.Clear();

                FS.HISFC.Models.Speciment.Container container = new FS.HISFC.Models.Speciment.Container();

                if (l != null)
                {
                    //container.Row = l.Row;
                    //container.Col = l.Col;
                    //container.Height = l.Height; 
                    // arrBoxList = specBoxManage.GetBoxByCapCol(l.LayerId.ToString(), tn.Tag.ToString());

                }
                if (s != null)
                {
                    arrBoxList = specBoxManage.GetBoxByCap(s.ShelfID.ToString(), 'J');
                    ShelfSpecManage tmpShelfSpec = new ShelfSpecManage();
                    ShelfSpec shelfSpec = tmpShelfSpec.GetShelfByShelf(s.ShelfID.ToString());
                    container.Height = shelfSpec.Height;
                    container.Col = shelfSpec.Col;
                    container.Row = shelfSpec.Row;
                    dgvBox.Tag = s;
                }

                #region 点击架子时

                //设置标本列表容器的大小
                #region 设置标本列表容器的大小
                if (container != null)
                {
                    if (arrBoxList.Count > 0)
                    {
                        grpSpecBoxInfo.Visible = true;
                        grpSpecInfo.Visible = true;
                        //nlvSpecContainer.Visible = true;
                    }
                    Size size = new Size();
                    if (container.Row > 2)
                    {
                        size = new Size(59 * container.Row + 30, 45 * container.Height + 59);
                        dgvBox.Size = size;
                    }
                }
                #endregion

                for (int r = 1; r <= container.Row; r++)
                {
                    DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                    //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                    imgDcl.HeaderText = "深度:" + r.ToString();
                    imgDcl.Width = 180;
                    dgvBox.Columns.Add(imgDcl);
                }
                for (int h = container.Height; h >= 1; h--)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Height = 40;
                    DataGridViewHeaderCell cell = dr.HeaderCell;
                    dr.HeaderCell.Value = "第" + h.ToString() + "层";
                    dgvBox.Rows.Add(dr);
                }
                foreach (SpecBox box in arrBoxList)
                {
                    Position p = new Position(box.DesCapRow, box.DesCapSubLayer);
                    dicBoxPos.Add(p, box);
                }
                //遍历dgvBox中的每个位置，如果SpecBox在listview中对应的高和行，加载该标本盒信息

                for (int i = 0; i < dgvBox.Rows.Count; i++)
                {
                    for (int k = 0; k < dgvBox.Columns.Count; k++)
                    {

                        int index = 0;

                        foreach (Position pt in dicBoxPos.Keys)
                        {
                            if (pt.Row == k + 1 && dgvBox.RowCount - pt.Height + 1 == dgvBox.RowCount - i)
                            {
                                SpecBox box = new SpecBox();
                                //subSpecManage.GetSubSpecInOneBox();
                                box = dicBoxPos[pt];
                                string boxBarCode = box.BoxBarCode;
                                DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i - 1];
                                //d.DISEASENAME ||' '||
                                string tmpSpecTypeName = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString()).SpecTypeName;
                                string tmpSql = @"select case length(d.specimentname) when 1 then d.specimentname when 2 then d.specimentname when 3 then d.specimentname
                                else substr(d.specimentname,3,2) end|| '('|| min(ss.SPEC_NO) || '-' || max(ss.SPEC_NO)||')'
                                from spec_box b join spec_type d on b.spectypeid = d.specimenttypeid
                                left join spec_subspec s on b.boxid = s.boxid
                                left join spec_source ss on ss.specid = s.specid
                                where b.boxid = {0} and b.DISEASETYPEID = ss.DISEASETYPEID
                                group by b.boxid, specimentname";

                                tmpSql = string.Format(tmpSql, box.BoxId.ToString());
                                string boxName = specBoxManage.ExecSqlReturnOne(tmpSql);
                                if (boxName == "-1")
                                {
                                    cell.Value = "";
                                }
                                else
                                {
                                    cell.Value = boxName;
                                }
                                cell.Tag = box;
                                string toolTipText = "位置：第" + box.DesCapRow.ToString() + " 行,第" + box.DesCapSubLayer.ToString() + " 层\n";
                                toolTipText += "病种类型：" + disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString()).DiseaseName + "\n";
                                toolTipText += "标本类型：" + tmpSpecTypeName;
                                cell.ToolTipText = toolTipText;
                                //if (box.SpecialUse == "8")
                                //{
                                //    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                //    style.BackColor = Color.SkyBlue;
                                //    cell.Style = style;
                                //}
                                //if (box.SpecialUse == "1")
                                //{
                                //    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                //    style.BackColor = Color.Orange;
                                //    cell.Style = style;
                                //}
                                if (box.SpecialUse != "")
                                {
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.SkyBlue;
                                    cell.Style = style;
                                }
                                break;
                            }
                            index++;
                        }
                        if (index >= dicBoxPos.Keys.Count)
                        {
                            DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i - 1];
                            cell.Value = "空";
                            cell.Tag = null;
                            string toolTipText = "该位置没放标本盒";
                            cell.ToolTipText = toolTipText;
                        }
                    }
                }
                #endregion
            }
            catch
            { }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            }

        /// <summary>
        /// 移除或修改冻存架
        /// </summary>
        /// <param name="oper">移除：D，修改: M</param>
        private void OperShelf(string oper)
        {
            if (grpSpecBoxInfo.Tag == null)
            {
                return;
            }
            string curTag = grpSpecBoxInfo.Tag.ToString();
            //如果是冰箱
            if (curTag == "I")
            {
                if (dgvBox.SelectedCells.Count > 1)
                {
                    MessageBox.Show("请选择一个需要移除的冻存架");
                    return;
                }
                try
                {
                    DataGridViewCell cell = dgvBox.SelectedCells[0];
                    Shelf tmpShelf = cell.Tag as Shelf;
                    OperShelf tmp = new OperShelf();
                    //修改
                    if (oper == "M")
                    {
                        tmp.ModifyShelf(ref tmpShelf);
                        cell.Tag = tmpShelf;
                    }
                    //移除
                    if (oper == "D")
                    {
                        tmp.RemoveShelf(ref tmpShelf);
                        cell.Tag = tmpShelf;
                    }
                    
                }
                catch
                { }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("添加冻存架", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B摆药台添加, true, false, null);
            this.toolBarService.AddToolButton("添加标本盒", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历添加, true, false, null);
            this.toolBarService.AddToolButton("打印标本盒条码", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("修改标本盒", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("移动标本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z转科, true, false, null);
            this.toolBarService.AddToolButton("确定标本位置", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z转科确认, true, false, null);
            this.toolBarService.AddToolButton("移除冻存架", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);
            this.toolBarService.AddToolButton("修改冻存架", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("移动标本盒", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T跳转, true, false, null);
            this.toolBarService.AddToolButton("确定标本盒位置", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T跳转, true, false, null);
            
            this.toolBarService.AddToolButton("刷新列表", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {              
                case "添加冻存架":
                    //每一次添加冻存架确保取得的是最新的记录   
                    if (dgvBox.Tag == null)
                        return;
                    int layer = dgvBox.SelectedCells[0].RowIndex + 1;
                    if (layer <= 0)
                        return;
                    IceBox tmpIceBox = new IceBox();
                    IceBoxLayer tmpLayer = new IceBoxLayer();
                    try
                    {
                        tmpIceBox = dgvBox.Tag as IceBox;
                    }
                    catch
                    {
                        MessageBox.Show("信息出错！");
                        return;
                    }
                    tmpLayer = layerManage.GetLayerIDByIceBox(tmpIceBox.IceBoxId.ToString(), layer.ToString());
                    if (tmpLayer == null||tmpLayer.LayerId <= 0)
                    {
                        MessageBox.Show("获取冰箱信息失败！");
                        return;
                    }
                    if (tmpLayer.SaveType == 'B')
                    {
                        return;
                    }
                    if (tmpLayer.OccupyCount == tmpLayer.Capacity)
                    {
                        MessageBox.Show("冰箱层已满！");
                        return;
                    }
                    //dgvBox.CurrentCell.ColumnIndex+1为当前列，系统从0至最后，人习惯思维1至最后，所以如下lingk0928
                    if (dgvBox.CurrentCell.ColumnIndex >= tmpLayer.Col)
                    {
                        MessageBox.Show("该冰箱层只有" + tmpLayer.Col.ToString() + "容量，不能在第" + (dgvBox.CurrentCell.ColumnIndex + 1).ToString() + "空架子上添加冻存架！");
                        return;
                    }
                    frmSpecShelf frmCurShelf = new frmSpecShelf();
                    frmCurShelf.LayerId = tmpLayer.LayerId;
                    frmCurShelf.ShowDialog();
                    break;
                case "添加标本盒":

                    #region 添加标本盒
                    Shelf s = new Shelf();
                    frmSpecBox frmCurBox = new frmSpecBox();

                    if (dgvBox.Tag.GetType().ToString().Contains("Shelf"))// is Shelf)
                    {
                        s = dgvBox.Tag as Shelf;
                        int sublayer = dgvBox.RowCount - dgvBox.SelectedCells[0].RowIndex ;
                        int col = 1;
                        int row = dgvBox.SelectedCells[0].ColumnIndex + 1;

                        SpecBox box = specBoxManage.GetByPoint(col.ToString(), row.ToString(), sublayer.ToString(), s.ShelfID.ToString(), "J");
                        if (box != null && box.BoxId > 0)
                        {
                            MessageBox.Show("该位置存在标本盒！");
                            return;
                        }
                        frmCurBox.InShelfCol = col;
                        frmCurBox.InShelfRow = row;
                        frmCurBox.InShelfHeight = sublayer;
                    }
                    else
                    {
                        try
                        {
                            s = dgvBox.SelectedCells[0].Tag as Shelf;
                        }
                        catch
                        {
                            return;
                        }
                    }
                     
                    if (s == null)
                    {
                        return;
                    }
                    string shelfId = s.ShelfID.ToString();
                    if (shelfId == null || shelfId == "")
                    {
                        MessageBox.Show("请选择目标架！");
                        return;
                    }
                     if (s.OccupyCount == s.Capacity)
                    {
                        MessageBox.Show("冻存架已满！");
                        return;
                    }
                    frmCurBox.CurShelfId = Convert.ToInt32(shelfId);
                    frmCurBox.ShowDialog();
                    tvIceBox.Refresh();
                    break;
                    #endregion

                case "修改标本盒":
                    try
                    {
                        SpecBox tmpBox = new SpecBox();
                        tmpBox = dgvBox.SelectedCells[0].Tag as SpecBox;
                        if (subSpecManage.GetSubSpecInOneBox(tmpBox.BoxId.ToString()).Count > 0)
                        {
                            MessageBox.Show("标本盒内存有标本不能修改！");
                            return;
                        }                       
                        frmSpecBox frm = new frmSpecBox();
                        frm.CurBox = tmpBox;
                        frm.CurShelfId = tmpBox.DesCapID;
                        frm.Oper = "M";
                        frm.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("获取标本盒失败");
                    }
                    break;
           
                case "确定标本位置":
                    this.TransferSpec();
                    break;
                case "移动标本":
                    this.CopySubSpec();
                    break;
                case "移除冻存架":
                    this.OperShelf("D");
                    break;
                case "修改冻存架":
                    this.OperShelf("M");
                    break;
                case "移动标本盒":
                    CopyBox();                   
                    break;
                case "确定标本盒位置":
                    TransferBox();
                    break;
                case "刷新列表":
                    InitTree();
                    break;
                default:
                    break;
            }
            //this.Query();
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 树的Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeClick(object sender, EventArgs e)
        {
            try
            {
                grpSpecBoxInfo.Visible = false;
                grpSpecInfo.Visible = false;
                //nlvSpecContainer.Visible = false;
                grpSpecBoxInfo.Tag = "";
                dgvBox.Rows.Clear();
                //dgvSpec.Columns.Clear();
                dgvBox.Columns.Clear();
                //dgvSpec.Rows.Clear();
                dicBoxPos.Clear();
                dicSubSpec.Clear();

                dgvBox.Tag = null;

                dgvBox.RowHeadersWidth = 100;
                dgvSpec.RowHeadersWidth = 50;

                TreeNode tn = tvIceBox.SelectedNode;
                string type = tn.Tag.GetType().ToString();

                #region 当点击冰箱时
                if (type.Contains("IceBox"))
                {
                    IceBox tmpIceBox = new IceBox();
                    if (type.Contains("Layer"))
                    {
                        IceBoxLayer ly = new IceBoxLayer();
                        ly = tn.Tag as IceBoxLayer;
                        ly = layerManage.GetLayerById(ly.LayerId.ToString());
                        tn.Text = "第 " + ly.LayerNum + " 层 (" + ly.OccupyCount.ToString() + "/" + ly.Capacity.ToString() + ")";
                        tmpIceBox = tn.Parent.Tag as IceBox;
                        grpSpecBoxInfo.Text = tn.Parent.Text;
                    }
                    else
                    {
                        tmpIceBox = tn.Tag as IceBox;
                        tmpIceBox = iceBoxManage.GetIceBoxById(tmpIceBox.IceBoxId.ToString());
                        grpSpecBoxInfo.Text = tn.Text;
                    }
                    grpSpecBoxInfo.Visible = true;
                    grpSpecBoxInfo.Tag = "I";
                    dgvBox.Tag = tmpIceBox;

                    ArrayList arrLayer = layerManage.GetLayerInOneBox(tmpIceBox.IceBoxId.ToString());
                    IceBoxLayer tmpLayer = arrLayer[0] as IceBoxLayer;
                    dgvBox.ShowCellToolTips = true;                    
                    for (int r = 1; r <= tmpLayer.Col; r++)
                    {
                        DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                        //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                        imgDcl.HeaderText = "架子:" + r.ToString();
                        imgDcl.Width = 150;
                        dgvBox.Columns.Add(imgDcl);
                    }
                    for (int h = 1; h<= tmpIceBox.LayerNum; h++)
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.Height = 40;
                        DataGridViewHeaderCell cell = dr.HeaderCell;
                        dr.HeaderCell.Value = "第" + h.ToString() + "层";
                        dgvBox.Rows.Add(dr);
                    }                   

                    foreach (IceBoxLayer l in arrLayer)
                    {
                        ArrayList arrS = shelfManage.GetShelfByLayerID(l.LayerId.ToString());
                        if ((arrS != null) && (arrS.Count > 0))
                        {
                            foreach (Shelf s in arrS)
                            {
                                int row = l.LayerNum;
                                int col = s.IceBoxLayer.Col;
                                DataGridViewCell cell = dgvBox[col - 1, row - 1];
                                cell.Tag = s;

                                string disSetting = this.GetShelfDisAndType(s.ShelfID.ToString(), s.DisTypeId.ToString(), s.SpecTypeId.ToString());
                                string tipText = "存放类型: " + disSetting;
                                tipText += "\n容量: " + s.Capacity.ToString();
                                tipText += "\n占用数量: " + s.OccupyCount.ToString();
                                cell.Value = disSetting;
                                cell.ToolTipText = tipText;
                                if (s.IsOccupy == '1' || s.OccupyCount == s.Capacity)
                                {
                                    //DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    //style.BackColor = Color.SkyBlue;
                                    //cell.Style = style;
                                    cell.Value = disSetting + "(已满)";
                                }
                                if (s.OccupyCount == 0)
                                {
                                    //DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    //style.BackColor = Color.Orange;
                                    //cell.Style = style;
                                    cell.Value = disSetting + " (空)";
                                }
                            }
                            for (int ki = 0; ki < dgvBox.ColumnCount; ki++)
                            {
                                if (l.Col <= ki)
                                {
                                    int lNum = l.LayerNum;
                                    DataGridViewCell cell = dgvBox[ki, lNum - 1];
                                    cell.Value = "虚位置";
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.LightGray;
                                    cell.Style = style;
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0; k < dgvBox.ColumnCount; k++)
                            {
                                if (l.Col <= k)
                                {
                                    int lNum = l.LayerNum;
                                    DataGridViewCell cell = dgvBox[k, lNum - 1];
                                    cell.Value = "虚位置";
                                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                                    style.BackColor = Color.LightGray;
                                    cell.Style = style;
                                }
                            }
                        }
                    }
                    return;
                }
                #endregion 

                #region 点击架子时
                //FS.HISFC.Object.Speciment.Container container = new FS.HISFC.Object.Speciment.Container();
                
                #region  冰箱直接存放标本盒，不用了
                //if (tn != null && tn.Text.Contains("列"))
                //{
                //    if (tn.Parent == null)
                //        return;
                //    IceBoxLayer currentLayer = tn.Parent.Tag as IceBoxLayer;
                //    if (currentLayer.SaveType == 'B')
                //    {
                //        arrBoxList = specBoxManage.GetBoxByCapCol(currentLayer.LayerId.ToString(), tn.Tag.ToString());
                //    }
                //    if (arrBoxList.Count > 0)
                //    {
                //        //SpecBox s = arrBoxList[0] as FS.HISFC.Object.Speciment.SpecBox;    
                //        container.Row = currentLayer.Row;
                //        container.Col = currentLayer.Col;
                //        container.Height = currentLayer.Height;
                //        grpSpecBoxInfo.Visible = true;
                //        grpSpecInfo.Visible = true;                         
                //    } 
                //}
                #endregion

                if (tn != null && type.Contains("Shelf"))
                {
                    grpSpecBoxInfo.Tag = "S";
                    Shelf currentShelf = tn.Tag as Shelf;
                    currentShelf = shelfManage.GetShelfByShelfId(currentShelf.ShelfID.ToString(), "");
                    string shelfBarCode = currentShelf.SpecBarCode;
                    tn.Text = shelfBarCode.Substring(shelfBarCode.Length - 2) + " (" + currentShelf.OccupyCount.ToString() + "/" + currentShelf.Capacity.ToString() + ")";
                    tn.Text += this.GetShelfDisAndType(currentShelf.ShelfID.ToString(), currentShelf.DisTypeId.ToString(), currentShelf.SpecTypeId.ToString());

                    tnParent = tn;
                    //arrBoxList = specBoxManage.GetBoxByCap(currentShelf.ShelfID.ToString(), 'J');
                    //ShelfSpecManage tmpShelfSpec = new ShelfSpecManage();
                    //ShelfSpec shelfSpec = tmpShelfSpec.GetShelfByShelf(currentShelf.ShelfID.ToString());
                    //container.Height = shelfSpec.Height;
                    //container.Col = shelfSpec.Col;
                    //container.Row = shelfSpec.Row;
                    grpSpecBoxInfo.Text = ParseLocation.ParseShelf(currentShelf.SpecBarCode) +"  标本盒位置信息";
                    this.SetContainer(null, currentShelf);
                }

                #region 这段代码提取出来
                //设置标本列表容器的大小
                //#region 设置标本列表容器的大小
                //if (container != null)
                //{
                //    if (arrBoxList.Count > 0)
                //    {
                //        grpSpecBoxInfo.Visible = true;
                //        grpSpecInfo.Visible = true;
                //        //nlvSpecContainer.Visible = true;
                //    }

                //    Size size = new Size();
                //    if (container.Row > 2)
                //    {
                //        size = new Size(59 * container.Row + 30, 45 * container.Height + 59);
                //        dgvBox.Size = size;
                //    }
                //}
                //#endregion

                //for (int r = 1; r <= container.Row; r++)
                //{
                //    DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                //    //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();

                //    imgDcl.HeaderText = "深度:" + r.ToString();
                //    imgDcl.Width = 150;
                //    dgvBox.Columns.Add(imgDcl);
                //}
                //for (int h = container.Height; h >= 1; h--)
                //{                    
                //    DataGridViewRow dr = new DataGridViewRow();                     
                //    dr.Height = 40;                    
                //    DataGridViewHeaderCell cell = dr.HeaderCell;                    
                //    dr.HeaderCell.Value = "第" + h.ToString()+"层" ;
                //    dgvBox.Rows.Add(dr);                    
                //}
                //foreach (SpecBox box in arrBoxList)
                //{
                //    Position p = new Position(box.DesCapRow, box.DesCapSubLayer);
                //    dicBoxPos.Add(p, box);
                //}
                ////遍历dgvBox中的每个位置，如果SpecBox在listview中对应的高和行，加载该标本盒信息
                
                //for (int i = 0; i < dgvBox.Rows.Count; i++)
                //{
                //    for (int k = 0; k < dgvBox.Columns.Count; k++)
                //    {
                        
                //        int index = 0;

                //        foreach (Position pt in dicBoxPos.Keys)
                //        {
                //            if (pt.Row == k + 1 && dgvBox.RowCount - pt.Height + 1== dgvBox.RowCount - i)
                //            {
                //                SpecBox box = new SpecBox();
                //                box = dicBoxPos[pt];
                //                string boxBarCode = box.BoxBarCode;
                //                DataGridViewCell cell = dgvBox[k,dgvBox.RowCount- i - 1];
                //                cell.Value = box.BoxBarCode;
                //                cell.Tag = box;
                //                string toolTipText = "位置：第" + box.DesCapRow.ToString() + " 行,第" + box.DesCapSubLayer.ToString() + " 层\n";
                //                toolTipText += "病种类型：" + disTypeManage.GetDisTypeByBoxId(box.BoxId.ToString()).DiseaseName + "\n";
                //                toolTipText += "标本类型：" + specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString()).SpecTypeName;
                //                cell.ToolTipText = toolTipText;
                //                if (box.SpecialUse == "8")
                //                {
                //                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                //                    style.BackColor = Color.SkyBlue;
                //                    cell.Style = style;
                //                }
                //                if (box.SpecialUse == "1")
                //                {
                //                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                //                    style.BackColor = Color.Orange;
                //                    cell.Style = style;
                //                }
                //                break;
                //            }
                //            index++;
                //        }
                //        if (index >= dicBoxPos.Keys.Count)
                //        {
                //            DataGridViewCell cell = dgvBox[k, dgvBox.RowCount - i -1];
                //            cell.Value = "空";
                //            cell.Tag = null;
                //            string toolTipText = "该位置没放标本盒";
                //            cell.ToolTipText = toolTipText;
                //        }
                //    }
                //}
                #endregion
            }
            catch
            {

            }
                #endregion
        }

        /// <summary>
        /// 记录架子或者盒子移动的原始位置及新位置
        /// </summary>
        private void SetSheetView()
        {
            sheetView.AutoGenerateColumns = false;
            sheetView.DataAutoSizeColumns = false;
            sheetView.Rows.Count = 0;
            sheetView.Columns.Count = 6;            
            sheetView.ColumnHeader.Rows.Count = 2;
            sheetView.ColumnHeader.Cells[1,Convert.ToInt32(SheetCols.类型)].Text = SheetCols.类型.ToString();
            sheetView.Columns[Convert.ToInt32(SheetCols.类型)].Label = SheetCols.类型.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.原条码)].Text = SheetCols.原条码.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.原位置)].Text = SheetCols.原位置.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.新条码)].Text = SheetCols.新条码.ToString();
            sheetView.ColumnHeader.Cells[1, Convert.ToInt32(SheetCols.新位置)].Text = SheetCols.新位置.ToString();

            for (int i = 0; i < sheetView.Columns.Count; i++)
            {
                sheetView.Columns[i].Width = 145;
                sheetView.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                sheetView.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
        }

        /// <summary>
        /// 加载数据到SheetView上
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="oldBarCode">旧条码</param>
        /// <param name="oldLoc">旧位置</param>
        /// <param name="newBarCode">新条码</param>
        /// <param name="newLoc">新位置</param>
        private void AddToSheet(string type, string oldBarCode, string oldLoc, string newBarCode, string newLoc)
        {
            int rowIndex = sheetView.Rows.Count;
            sheetView.Rows.Count = rowIndex + 1;
            int i = Convert.ToInt32(SheetCols.新位置);
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.类型)].Text = type;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.原条码)].Text = oldBarCode;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.原位置)].Text = oldLoc;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.新条码)].Text = newBarCode;
            sheetView.Cells[rowIndex, Convert.ToInt32(SheetCols.新位置)].Text = newLoc;
            this.isPrint = false;
        }

        /// <summary>
        /// 架子移动，给架子重定位
        /// </summary>
        private bool ShelfLocate(IceBoxLayer iceBoxLayer, Shelf shelf, IceBoxLayer oldLayer)
        {
            string type = "架子";
            string oldBarCode = shelf.SpecBarCode;
            string oldLoc = ParseLocation.ParseShelf(oldBarCode);
            string newBarCode = "";
            string newLoc = "";

            iceBoxLayer = layerManage.GetLayerById(iceBoxLayer.LayerId.ToString());
            if (oldLayer == null || oldLayer.LayerId <= 0)
            {
                return false;
            }
            if (iceBoxLayer == null || iceBoxLayer.LayerId <= 0)
            {
                return false;
            }
            if (iceBoxLayer.LayerId == oldLayer.LayerId)
            {
                return false;
            }
            if ((iceBoxLayer.OccupyCount == iceBoxLayer.Capacity) || iceBoxLayer.IsOccupy == (short)1)
            {
                MessageBox.Show("该冰箱层已满");
                return false;
            }
            if (iceBoxLayer.SpecID != shelf.ShelfSpec.ShelfSpecID)
            {
                MessageBox.Show("架子规格不相符");
                return false;
            }
            //if (specTypeId != 9 && disTypeId != 16)
            if (iceBoxLayer.SpecTypeID != 9)
            {
                if (iceBoxLayer.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("标本类型不相符！");
                    return false;
                }
            }
            if (iceBoxLayer.DiseaseType.DisTypeID != 16)
            {
                if (iceBoxLayer.DiseaseType.DisTypeID != shelf.DisTypeId)
                {
                    MessageBox.Show("标本病种不相符！");
                    return false;
                }
            }

            iceBoxLayer = layerManage.GetLayerById(iceBoxLayer.LayerId.ToString());
            oldLayer = layerManage.GetLayerById(oldLayer.LayerId.ToString());
            ArrayList arrShelf = new ArrayList();
            arrShelf = shelfManage.GetShelfByLayerID(iceBoxLayer.LayerId.ToString());
            shelf.IceBoxLayer.Row = 1;
            shelf.IceBoxLayer.Height = 1;
            shelf.IceBoxLayer.LayerId = iceBoxLayer.LayerId;
            if (arrShelf.Count == 0)
            {
                shelf.IceBoxLayer.Col = 1;
            }
            Shelf lastShelf = shelfManage.ScanLayer(arrShelf);
            shelf.IceBoxLayer.Col = lastShelf.IceBoxLayer.Col + 1;           
            shelf.SpecBarCode = "BX" + iceBoxLayer.IceBox.IceBoxId.ToString().PadLeft(3, '0');
            shelf.SpecBarCode += "-C" + iceBoxLayer.LayerNum.ToString().PadLeft(1, '0');
            shelf.SpecBarCode += "-J" + shelf.IceBoxLayer.Col.ToString().PadLeft(1, '0');
            newBarCode = shelf.SpecBarCode;
            newLoc = ParseLocation.ParseShelf(newBarCode);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (shelfManage.UpdateShelf(shelf) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败");
                    return false;
                }
                int occupyCount = 0;
                occupyCount = iceBoxLayer.OccupyCount + 1;
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(),iceBoxLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败");
                    return false;
                }
                occupyCount = oldLayer.OccupyCount -1;
                occupyCount = occupyCount < 0 ? 0 : occupyCount;
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(), oldLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败");
                    return false;
                }
                if (iceBoxLayer.OccupyCount == iceBoxLayer.Capacity)
                {
                    if (layerManage.UpdateOccupyCount("1", iceBoxLayer.LayerId.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败");
                        return false;
                    }
                }
                if(layerManage.UpdateOccupy("0",oldLayer.LayerId.ToString())<=0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败");
                    return false;
                }
                ArrayList arrBox = specBoxManage.GetBoxByCap(shelf.ShelfID.ToString(), 'J');
                if (arrBox != null && arrBox.Count > 0)
                {
                    foreach (SpecBox b in arrBox)
                    {
                        string barCode = b.BoxBarCode.Replace(b.BoxBarCode.Substring(0, 11), shelf.SpecBarCode);
                        b.BoxBarCode = barCode;
                        if (specBoxManage.UpdateSpecBox(b) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("操作失败");
                            return false;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.AddToSheet(type, oldBarCode, oldLoc, newBarCode, newLoc);
                MessageBox.Show("操作成功");
                return true;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return false;
            }           
        }
 
         /// <summary>
        /// 定位标本盒
        /// </summary>
        private bool BoxLocate(Shelf shelf , SpecBox box)
        {
            string type = "盒子";
            string oldBarCode = box.BoxBarCode;
            string oldLoc = ParseLocation.ParseSpecBox(oldBarCode);
            string newBarcode = "";
            string newLoc = "";
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");
            if (shelf.Capacity == shelf.OccupyCount)
            {
                MessageBox.Show("架子已满!");
                return false;
            }
            ShelfSpec shelfSpec = new ShelfSpec();
            ShelfSpecManage shelfSpecManage = new ShelfSpecManage();
            shelfSpec = shelfSpecManage.GetShelfByID(shelf.ShelfSpec.ShelfSpecID.ToString());
            Shelf oldShelf = new Shelf();
            oldShelf = shelfManage.GetShelfByShelfId(box.DesCapID.ToString(),"");
            shelf = shelfManage.GetShelfByShelfId(shelf.ShelfID.ToString(), "");
            if (shelfSpec == null || oldShelf==null || oldShelf.ShelfID <= 0)
            {
                MessageBox.Show("获取信息失败!");
                return false;
            }            
            if (shelfSpec.BoxSpec.BoxSpecID != box.BoxSpec.BoxSpecID)
            {
                MessageBox.Show("标本盒规格不一致!");
                return false;
            }
            if (shelf.SpecTypeId != 9)
            {
                if (box.SpecTypeID != shelf.SpecTypeId)
                {
                    MessageBox.Show("标本类型不相符！");
                    return false;
                }
            }
            if (shelf.DisTypeId != 16)
            {
                if (box.DiseaseType.DisTypeID != shelf.DisTypeId)
                {
                    MessageBox.Show("标本病种不相符！");
                    return false;
                }
            }          
             SpecBox lastSpecBox = new SpecBox(); 
             lastSpecBox = specBoxManage.ShelfGetLastCapBox(shelf.ShelfID.ToString());                           
               // currentShelf = shelfManage
                //获取当前使用架子的规格行，列，高
                int shelfCol = shelfSpec.Col;
                int shelfRow = shelfSpec.Row;
                int shelfHeight = shelfSpec.Height;
                //获取编号最大盒子所在的行，列，层
                int lastCol = lastSpecBox.DesCapCol;
                int lastRow = lastSpecBox.DesCapRow;
                int lastHeight = lastSpecBox.DesCapSubLayer;
                if (lastCol < shelfCol)
                {
                    box.DesCapCol = lastCol + 1;
                    box.DesCapRow = lastRow;
                    box.DesCapSubLayer = lastHeight;                  
                }
                if (lastCol == shelfCol && lastHeight < shelfHeight)
                {
                    box.DesCapCol = 1;
                    box.DesCapSubLayer = lastHeight + 1;
                    box.DesCapRow = lastRow;                 
                }
                if (lastCol == shelfCol && lastHeight == shelfHeight && lastRow < shelfRow)
                {
                    box.DesCapCol = 1;
                    box.DesCapSubLayer = 1;
                    box.DesCapRow = lastSpecBox.DesCapRow + 1;                  
                }          
                string boxBarCode = shelf.SpecBarCode;// +currentShelf.IceBoxLayer.Col.ToString().PadLeft(2, '0');
                boxBarCode += "-" + box.DesCapRow.ToString().PadLeft(1, '0');
                boxBarCode += box.DesCapSubLayer.ToString().PadLeft(1, '0');
                box.BoxBarCode = boxBarCode;
                newBarcode = boxBarCode;
                newLoc = ParseLocation.ParseSpecBox(newBarcode);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                bool isFull = false;

                int occupyCount = oldShelf.OccupyCount - 1;
                if (shelfManage.UpdateOccupyCount(occupyCount.ToString(), oldShelf.ShelfID.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return false;
                }
                if (oldShelf.IsOccupy == '1')
                {
                    if (shelfManage.UpdateIsFull("0", oldShelf.ShelfID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！");
                        return false;
                    }
                }
                occupyCount = shelf.OccupyCount + 1;
                if (shelfManage.UpdateOccupyCount(occupyCount.ToString(), shelf.ShelfID.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return false;
                }
                box.DesCapID = shelf.ShelfID;
                if (specBoxManage.UpdateSpecBox(box) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return false;
                }
                if (occupyCount == shelf.Capacity)
                {
                    isFull = true;
                    if (shelfManage.UpdateIsFull("1", shelf.ShelfID.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！");
                        return false;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit(); 
                this.AddToSheet(type, oldBarCode, oldLoc, newBarcode, newLoc);
                MessageBox.Show("操作成功");
                if (isFull)
                {
                    DialogResult diaRe = MessageBox.Show("该冻存架已满，添加新的冻存架!","添加架子",MessageBoxButtons.YesNo);
                    if (diaRe == DialogResult.No)
                    {
                        return true;
                    }
                    frmSpecShelf frmShelf = new frmSpecShelf();
                    frmShelf.LayerId = shelf.IceBoxLayer.LayerId;
                    frmShelf.ShowDialog();
                }
                return true;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败！");
                return false;
            }       

        }

        /// <summary>
        /// 获取标本的病种类型颜色和标本类型
        /// </summary>
        /// <param name="boxId">标本盒ID</param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <param name="disColor"></param>
        /// <param name="specTypeName"></param>
        private void GetSubSpecDet(string boxId, string endRow, string endCol,  ref string specTypeName)
        {
            string[] parms = new string[] { boxId, endRow, endCol };
            string sql = " select DISTINCT SPEC_DISEASETYPE.DISEASECOLOR,SPEC_TYPE.SPECIMENTNAME \n" +
                       " from SPEC_SUBSPEC LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID \n" +
                       " left join SPEC_DISEASETYPE on SPEC_SOURCE.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID\n" +
                       " LEFT JOIN SPEC_TYPE ON SPEC_TYPE.SPECIMENTTYPEID = SPEC_SUBSPEC.SPECTYPEID\n" +
                       " where boxid={0} AND SPEC_SUBSPEC.BOXENDROW={1} AND SPEC_SUBSPEC.BOXENDCOL={2}";
            string Sql = string.Format(sql, parms);
            DataSet ds = new DataSet();
            subSpecManage.ExecQuery(Sql, ref ds);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                //disColor = dt.Rows[0]["DISEASECOLOR"].ToString();
                specTypeName = dt.Rows[0]["SPECIMENTNAME"].ToString();
            }

        }

        private void ucContainer_Load(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载冰箱信息，请稍候...");
                Application.DoEvents();
                InitTree();
                tvIceBox.SelectedImageIndex = 1;
                tvIceBox.ImageIndex = 0;
                grpSpecInfo.Visible = false;
                grpSpecBoxInfo.Visible = false;
                if (curShelf != null && curShelf.ShelfID > 0)
                {
                    grpSpecInfo.Visible = true;
                    grpSpecBoxInfo.Visible = true;
                }
                if (curBox != null && curBox.BoxId > 0)
                {
                    grpSpecInfo.Visible = true;
                    grpSpecBoxInfo.Visible = true;
                }

                this.tvIceBox.AllowDrop = true;
                SetSheetView();
                this.BindingDisType();
                this.BindingSpecType();
            }
            catch
            { }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            //nlvSpecContainer.Visible = false;
        }

        private void dgvBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void lv_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lv_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))            
            {
                Point p = this.tvIceBox.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = tvIceBox.HitTest(p);

                if (index == null || index.Node == null || index.Node.Tag == null)
                {
                    return;
                }
                if (!index.Node.Tag.GetType().ToString().Contains("IceBoxLayer"))
                {
                    return;
                }
                if (index.Node != null)
                {
                    TreeNode tnLayer = index.Node;
                    tvIceBox.SelectedNode = tnLayer;
                    TreeNode tnTmp = (TreeNode)e.Data.GetData(typeof(TreeNode));
                    if (tnTmp == null || tnTmp.Tag == null)
                    {
                        return;
                    }
                    if (!tnTmp.Tag.GetType().ToString().Contains("Shelf"))
                    {
                        return;
                    }
                    bool succeed = this.ShelfLocate(tnLayer.Tag as IceBoxLayer, tnTmp.Tag as Shelf, tnParent.Tag as IceBoxLayer);
                    if (succeed)
                    {
                        tnParent.Nodes.Remove(tnTmp);
                        IceBoxLayer tmpLayer = layerManage.GetLayerById((tnParent.Tag as IceBoxLayer).LayerId.ToString());
                        if (tmpLayer != null)
                        {
                            tnParent.Text = "第 " + tmpLayer.LayerNum + " 层 (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        }
                        tnLayer.Nodes.Add(tnTmp);

                        tmpLayer = new IceBoxLayer();
                        tmpLayer = layerManage.GetLayerById((tnLayer.Tag as IceBoxLayer).LayerId.ToString());
                        if (tmpLayer != null)
                        {
                            tnLayer.Text = "第 " + tmpLayer.LayerNum + " 层 (" + tmpLayer.OccupyCount.ToString() + "/" + tmpLayer.Capacity.ToString() + ")";
                        }
                        Shelf s = (tnTmp.Tag as Shelf);
                        if (s != null)
                        {
                            tnTmp.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 

                        }//InitTree();
                    }
                    tnParent = new TreeNode();
                    //DataGridViewCell drv = (DataGridViewCell)e.Data.GetData(typeof(DataGridViewCell));
                    //index.Node.Text = drv.Value.ToString();
                }
            }
            if (e.Data.GetDataPresent(typeof(SpecBox)))
            {
                if (isDrag)
                {
                    return;
                }
                Point p = tvIceBox.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = tvIceBox.HitTest(p);
                if (index.Node == null || index.Node.Tag == null || !index.Node.Tag.GetType().ToString().Contains("Shelf"))
                {
                    return;
                }

                this.tvIceBox.SelectedNode = index.Node;
                //TreeNode tnd = (TreeNode)e.Data.GetData(typeof(TreeNode));
                SpecBox box = (SpecBox)e.Data.GetData(typeof(SpecBox));
                if (box == null)
                {
                    return;
                }
                
                if (BoxLocate(index.Node.Tag as Shelf, box))
                {
                    dgvBox.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Value = "空";
                    Shelf s = shelfManage.GetShelfByShelfId((index.Node.Tag as Shelf).ShelfID.ToString(),"");
                    if (s != null)
                    {
                        index.Node.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 
                    }
                    s = new Shelf();
                    s = shelfManage.GetShelfByShelfId((tnParent.Tag as Shelf).ShelfID.ToString(),"");
                    if (s != null)
                    {
                        tnParent.Text = s.SpecBarCode.Substring(9, 2) + " (" + s.OccupyCount.ToString() + "/" + s.Capacity.ToString() + ")"; 
                    }
                    
                    rowIndex = -1;
                    colIndex = -1;
                    //TreeClick(null, null);
                   // InitTree();                   
                }
                tnParent = new TreeNode();
                isDrag = true;
            }
        }

        private void lv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = tvIceBox.SelectedNode;
                if (tn == null || tn.Tag == null)
                {
                    return;
                }
                if (!tn.Tag.GetType().ToString().Contains("Shelf"))
                {
                    return;
                }
                if (tn != null)
                {
                    tnParent = new TreeNode();
                    tnParent = tn.Parent;
                    tvIceBox.DoDragDrop(tn, DragDropEffects.Move);
                }
            }
        }

        private void dgvBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = this.dgvBox.HitTest(e.X, e.Y);
                if (info.RowIndex != -1 && info.ColumnIndex != -1)
                {
                    SpecBox box = this.dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SpecBox;
                    if (box != null)
                    {
                        isDrag = false;
                        rowIndex = info.RowIndex;
                        colIndex = info.ColumnIndex;
                        //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = null;
                        this.DoDragDrop(box, DragDropEffects.Move);
                    }
                }
            }
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

        private void dgvBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            string type = "盒子";
            string oldBarCode = "";
            string oldLoc = "";
            string newBarCode = "";
            string newLoc = "";

            Point p = this.dgvBox.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo info = this.dgvBox.HitTest(p.X, p.Y);

            #region 移动标本
            if (e.Data.GetDataPresent(typeof(SubSpec)))
            {
                SpecBox desBox = dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag as SpecBox;
                desBox = specBoxManage.GetBoxById(desBox.BoxId.ToString());
                if (desBox == null)
                {
                    MessageBox.Show("获取目的标本盒失败!");
                    return;
                }
                SubSpec sub = (SubSpec)e.Data.GetData(typeof(SubSpec));
                if (sub == null)
                {
                    return;
                }

                if (desBox.SpecTypeID != sub.SpecTypeId)
                {
                    MessageBox.Show("标本类型不相符!");
                    return;
                }

                if (desBox.OccupyCount == desBox.Capacity)
                {
                    MessageBox.Show("标本盒已满!");
                    return;
                }

                SpecBox sourceBox = specBoxManage.GetBoxById(sub.BoxId.ToString());
                if (sourceBox == null)
                {
                    MessageBox.Show("获取原标本盒失败!");
                    return;
                }

                if (desBox.BoxId == sourceBox.BoxId)
                {
                    return;
                }

                SpecInOper tmpIn = new SpecInOper();                       //BX005-C5-J1-11

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                tmpIn.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                tmpIn.SetTrans();

                try
                {

                    if (tmpIn.TransferSpec(sourceBox, desBox, sub) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("操作失败！");
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return;
                }
                DataGridViewCellEventArgs te = new DataGridViewCellEventArgs(info.ColumnIndex, info.RowIndex);
                dgvBox.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvBox[info.ColumnIndex, info.RowIndex].Selected = true;                
                this.dgvBox_CellDoubleClick(sender, te);
                return;
            }
            #endregion

            if (info.RowIndex != -1 && info.ColumnIndex != -1 && rowIndex >= 0 && colIndex >= 0)
            {


                if (dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag != null)
                {
                   return;
                }
                #region 移动标本盒
                if (e.Data.GetDataPresent(typeof(SpecBox)))
                {
                    SpecBox box = (SpecBox)e.Data.GetData(typeof(SpecBox));
                    if (box == null)
                    {
                        return;
                    }
                    if ((info.ColumnIndex + 1 == box.DesCapRow) && (dgvBox.RowCount - info.RowIndex == box.DesCapSubLayer))
                    {
                        return;
                    }

                    try
                    {
                        //BX005-C5-J1-11
                        oldBarCode = box.BoxBarCode;
                        oldLoc = ParseLocation.ParseSpecBox(oldBarCode);
                        string boxBarCode = box.BoxBarCode.Substring(0, 12);
                        box.DesCapRow = info.ColumnIndex + 1;
                        box.DesCapSubLayer = dgvBox.RowCount - info.RowIndex;
                        boxBarCode += box.DesCapRow.ToString().PadLeft(1, '0');
                        boxBarCode += box.DesCapSubLayer.ToString().PadLeft(1, '0');
                        box.BoxBarCode = boxBarCode;
                        newBarCode = boxBarCode;
                        newLoc = ParseLocation.ParseSpecBox(newBarCode);

                        if (specBoxManage.UpdateSpecBox(box) <= 0)
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
                    dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = dgvBox.Rows[rowIndex].Cells[colIndex].Value; ;
                    dgvBox.Rows[info.RowIndex].Cells[info.ColumnIndex].Tag = box;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Tag = null;
                    dgvBox.Rows[rowIndex].Cells[colIndex].Value = "空";
                    rowIndex = -1;
                    colIndex = -1;
                    this.AddToSheet(type, oldBarCode, oldLoc, newBarCode, newLoc);
                }
                #endregion
                //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = value;
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
                //this.grdCampaigns.Rows[info.RowIndex].Cells[info.ColumnIndex].Value = value;
            }
        }

        private void dgvBox_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                string tag = grpSpecBoxInfo.Tag.ToString();
                if (tag == null || tag == "I")
                    return;
            }
            catch
            { }
            e.Effect = DragDropEffects.Move;
        }

        private void dgvSpec_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            try
            {
                ucLocPrint print = new ucLocPrint();
                print.SheetTitle = "新旧位置对照";
                print.SheetView = this.sheetView;
                print.SetSheet();
                print.Print();
                isPrint = true;
                this.sheetView.Rows.Count = 0;
            }
            catch
            {
                isPrint = false;
            }
            return base.Print(sender, neuObject);
        }

        public override int Exit(object sender, object neuObject)
        {
            if (!isPrint)
            {
                Print(null, null);
                isPrint = true;
                sheetView.Rows.Count = 0;
            }

            return base.Exit(sender, neuObject);
        }

        private void dgvBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSpec.Rows.Clear();
            dgvSpec.Columns.Clear();
            dicSubSpec.Clear();

            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }

            DataGridViewCell cell = dgvBox[e.ColumnIndex, e.RowIndex];

            if (cell.Value == "虚位置")
            {
                MessageBox.Show("虚位置里边没东西，不用进去看了！");
                return;
            }

            if (grpSpecBoxInfo.Tag == null)
            {
                return;
            }
            string curTag = grpSpecBoxInfo.Tag.ToString();
            //如果是冰箱
            if (curTag == "I")
            {
                try
                {
                    Shelf tmpShelf = cell.Tag as Shelf;
                    this.SetContainer(null, tmpShelf);
                    grpSpecBoxInfo.Text = ParseLocation.ParseShelf(tmpShelf.SpecBarCode) + " 标本盒位置信息";
                    grpSpecBoxInfo.Tag = "";                    
                }
                catch
                { }
                return;
            }

            #region
            try
            {
                if (cell == null)
                {
                    return;
                }


                if (cell.Tag == null)
                {
                    return;
                }
                if (cell.Tag != null)
                {
                    SpecBox currentBox = cell.Tag as SpecBox;
                    ArrayList arrSpec = subSpecManage.GetSubSpecInOneBox(currentBox.BoxId.ToString());
                    BoxSpec spec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                    grpSpecInfo.Text = "盒子: " + ParseLocation.ParseSpecBox(currentBox.BoxBarCode) + " 中标本位置详情";
                    Size size = new Size();
                    if (spec != null)
                    {
                        if (spec.Row > 2)
                        {
                            size.Width = 60 + spec.Col * 60;
                            grpSpecInfo.Width = (size.Width + 60);
                            size.Height = 30 + spec.Row * 30;
                            dgvSpec.Size = size;
                        }
                        for (int c = 1; c <= spec.Col; c++)
                        {
                            //DataGridViewImageColumn imgDcl = new DataGridViewImageColumn();
                            DataGridViewTextBoxColumn imgDcl = new DataGridViewTextBoxColumn();
                            imgDcl.HeaderText = c.ToString();
                            imgDcl.Width = 90;
                            dgvSpec.Columns.Add(imgDcl);
                        }
                        for (int r = 1; r <= spec.Row; r++)
                        {
                            DataGridViewRow dr = new DataGridViewRow();
                            dr.Height = 30;
                            DataGridViewHeaderCell headerCell = dr.HeaderCell;

                            dr.HeaderCell.Value = Convert.ToChar(64 + r).ToString();
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
                                    string color = "";
                                    string specTypeName = "";
                                    //this.GetSubSpecDet(currentBox.BoxId.ToString(), sub.BoxEndRow.ToString(), sub.BoxEndCol.ToString(), ref specTypeName);
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
                dgvSpec.Visible = true;
            }
            catch
            {

            }
            #endregion

        }

        private void dgvSpec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SubSpec tmp = dgvSpec.SelectedCells[0].Tag as SubSpec;                 

                 OrgTypeManage orgManage = new OrgTypeManage();
                string orgName = orgManage.GetBySpecType(tmp.SpecTypeId.ToString()).OrgName;

                  if(orgName.Contains("血"))
                    {
                        ucSpecimentSource ucBld = new ucSpecimentSource();
                        ucBld.SpecId = tmp.SpecId;
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucBld.Size = size;

                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBld, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                        //FS.NFC.Interface.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                 
                        
                    }
                    if (orgName.Contains("组织"))
                    {
                        ucSpecSourceForOrg ucOrg = new ucSpecSourceForOrg();
                        ucOrg.SpecId = tmp.SpecId;                         
                       // FormBorderStyle formStyle = new FormBorderStyle();
                        Size size = new Size();
                        size.Height = 800;
                        size.Width = 1280;
                        ucOrg.Size = size;
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucOrg, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                        //FS.NFC.Interface.Classes.Function.PopForm popForm = new FS.NFC.Interface.Forms.BaseForm();
                        
                    }
 
            }
            catch
            {
                return;
            }

        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                InitTree();
            }
        }

        /// <summary>
        /// 获取标本查询条件
        /// </summary>
        public void GetQuery()
        {
            int disTypeId = 0;
            if (cmbDisType.Text.Trim() != "")
            {
                disTypeId = Convert.ToInt32(cmbDisType.Tag.ToString());
            }

            int specTypeId = 0;
            if (cmbSpecType.Text.Trim() != "")
            {
                specTypeId = Convert.ToInt32(cmbSpecType.SelectedValue.ToString());
            }

            //冰箱列表
            arrIceBoxList = new ArrayList();
            //层列表
            arrLayerList = new ArrayList();
            //架子列表
            arrShelfList = new ArrayList();
            if (!chkTime.Checked)
            {
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetIceBoxByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisAndSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrLayerList = layerManage.GetLayerByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                    arrShelfList = shelfManage.GetShelfByDisOrSpecType(disTypeId.ToString(), specTypeId.ToString());
                }
            }
            if (chkTime.Checked)
            {
                string start = dtpStart.Value.Date.ToString();
                string end = dtpEnd.Value.Date.AddDays(1.0).ToString();
                if (rbtAnd.Checked)
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeA(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                else
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrLayerList = layerManage.GetIceBoxBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTimeO(disTypeId.ToString(), specTypeId.ToString(), start, end);
                }
                if (string.IsNullOrEmpty(cmbDisType.Text.Trim().ToString()) && string.IsNullOrEmpty(cmbSpecType.Text.Trim().ToString()))
                {
                    arrIceBoxList = iceBoxManage.GetIceBoxBySubStoreTime(start, end);
                    arrLayerList = layerManage.GetIceBoxByStoreTime(start, end);
                    arrShelfList = shelfManage.GetShelfBySubStoreTime(start, end);
                }
            }   
        }
    }

    sealed class Position
    {
        public int Row = 0;
        public int Height = 0;
        public int Col = 0;
        public Position(int r, int h)
        {
            Row = r;
            Height = h;
        }
        public Position(string r, string c)
        {
            Row = Convert.ToInt32(r);
            Col = Convert.ToInt32(c);
        }

    }

    sealed class SpecSort : IComparer<SubSpec>
    {
        int IComparer<SubSpec>.Compare(SubSpec s1, SubSpec s2)
        {
            return s1.SubSpecId.CompareTo(s2.SubSpecId); 
        }
    }

    sealed class BoxSort : IComparer<SpecBox>
    {
        int IComparer<SpecBox>.Compare(SpecBox b1, SpecBox b2)
        {
            return b1.BoxBarCode.CompareTo(b2.BoxBarCode);
        }
    }
}
