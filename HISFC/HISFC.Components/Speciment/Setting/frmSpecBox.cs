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
    public partial class frmSpecBox : Form
    {
        #region 变量

        private SpecBox tmpBox;
        //盒子规格管理对象
        private SpecBoxManage specBoxManage;
        //病种类型管理对象
        private DisTypeManage disTypeManage;
        //标本盒规格管理对象
        private BoxSpecManage boxSpecManage;
        //冰箱层管理对象
        private IceBoxLayerManage layerManage;
        //冰箱管理对象
        private IceBoxManage iceboxManage;
        //编号最大的标本盒
        private SpecBox lastSpecBox;
        //当前使用的层
        private IceBoxLayer currentUseLayer;
        //当前使用的架子
        private Shelf currentShelf;
        /// <summary>
        /// 项目id
        /// </summary>
        private string projectId = string.Empty;
        //架子规格管理对象
        private ShelfSpecManage shelfSpecManage;
        private ShelfManage shelfManage;
        private ShelfSpec shelfSpec;
        private string title = "标本盒设置";
        private Dictionary<int, string> dicOrgType;
        private Dictionary<int, string> dicSpecType;
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private CapLogManage capLogManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private int curLayerId;
        private int curShelfId;
        private string oper = "";

        /// <summary>
        /// 当前操作，"M" 修改
        /// </summary>
        public string Oper
        {
            set
            {
                oper = value;
            }
        }
        public SpecBox CurBox
        {
            set
            {
                tmpBox = value;
            }
        }
        public int CurLayerId
        {
            get
            {
                return curLayerId;
            }
            set
            {
                curLayerId = value;
            }
        }

        public int CurShelfId
        {
            get
            {
                return curShelfId;
            }
            set
            {
                curShelfId = value;
            }
        }

        //病种ID
        int disTypeid ;
        //盒规格ID
        int boxSpecId;
        //标本种类ID
        int orgOrBlood;
        //标本类型ID
        int specTypeId;

        //在架子中的高度
        int inShelfCol = 0;
        int inShelfRow = 0;
        int inShelfHeight = 0;

        public int DisTypeId
        {
            set
            {
                disTypeid = value;
            }
        }

        public int SpecTypeId
        {
            set
            {
                specTypeId = value;
            }
        }

        public int OrgOrBlood
        {
            set
            {
                orgOrBlood = value;
            }
        }

        public int InShelfCol
        {
            set
            {
                inShelfCol = value;
            }
        }

        public int InShelfRow
        {
            set
            {
                inShelfRow = value;
            }
        }

        public int InShelfHeight
        {
            set
            {
                inShelfHeight = value;
            }
        }

        #endregion

        public frmSpecBox()
        {
            InitializeComponent();
            boxSpecManage = new BoxSpecManage();
            disTypeManage = new DisTypeManage();
            specBoxManage = new SpecBoxManage();
            layerManage = new IceBoxLayerManage();
            iceboxManage = new IceBoxManage();
            tmpBox = new SpecBox();
            lastSpecBox = new SpecBox();    
            shelfSpecManage = new ShelfSpecManage();
            shelfManage = new ShelfManage();
            shelfSpec = new ShelfSpec();
            dicOrgType=new Dictionary<int,string>();
            dicSpecType = new Dictionary<int, string>();
            orgTypeManage = new OrgTypeManage();
            capLogManage = new CapLogManage();           
            loginPerson = new FS.HISFC.Models.Base.Employee();
            specTypeManage=new SpecTypeManage();
        }

        #region 方法

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
                cmbOrgOrBlood.SelectedIndex = 0;
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
            Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDiseaseType.DisplayMember = "Value";
                cmbDiseaseType.ValueMember = "Key";
                cmbDiseaseType.DataSource = bs;
            }
        }

        /// <summary>
        /// 盒子规格绑定
        /// </summary>
        private void BoxSpecBinding()
        {
            Dictionary<int, string> dicSpec = boxSpecManage.GetAllBoxSpec();
            if (dicSpec.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicSpec;
                cmbBoxSpec.DisplayMember = "Value";
                cmbBoxSpec.ValueMember = "Key";
                cmbBoxSpec.DataSource = bs;
                cmbBoxSpec.SelectedIndex = 0;
               // cmbBoxSpec.SelectedValue;
            } 
        }

        /// <summary>
        /// 解析标本盒的位置信息
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ParseBoxCode(SpecBox box, char type)
        {
            string tmpBarCode = box.BoxBarCode;
            string boxId =Convert.ToInt32(tmpBarCode.Substring(2, 3)).ToString();
            string otherInfo = tmpBarCode.Substring(5);
            string parseResult = "";
            parseResult = iceboxManage.GetIceBoxById(boxId).IceBoxName + " ";
            parseResult += otherInfo;
            //if (type == 'J')
            //{
            //    parseResult += otherInfo + "-";
            //    //层，行，列
            //    parseResult += box.DesCapSubLayer.ToString() + "-";
            //    parseResult += box.DesCapRow.ToString() + "-";
            //    parseResult += box.DesCapCol.ToString();
                
            //}
            //if (type == 'B')
            //{
            //    parseResult += tmpBarCode.Substring(3, 2) + "-";
            //    //行，列，层
            //    parseResult += box.DesCapRow.ToString() + "-";
            //    parseResult += box.DesCapCol.ToString() + "-";
            //    parseResult += box.DesCapSubLayer.ToString();
            //}
            return parseResult;
        }

        /// <summary>
        /// 定位标本盒
        /// </summary>
        private int BoxLocate()
        {
            tmpBox = new SpecBox();
            BoxSpec boxSpec = boxSpecManage.GetSpecById(boxSpecId.ToString());
            tmpBox.Capacity = boxSpec.Row * boxSpec.Col;
            tmpBox.InIceBox = '0';
            tmpBox.IsOccupy = '0';
            tmpBox.Comment = txtComment.Text;
            tmpBox.OccupyCount = 0;            
            tmpBox.DiseaseType.DisTypeID = disTypeid;
            tmpBox.BoxSpec.BoxSpecID = boxSpecId;
            tmpBox.OrgOrBlood = orgOrBlood;
            tmpBox.SpecTypeID = specTypeId;
            if (chk863.Checked)
            {
                tmpBox.SpecialUse = chk863.Checked ? "8" : "";
            }
            if (chk115.Checked)
            {
                tmpBox.SpecialUse = chk115.Checked ? "1" : "";
            }

            #region 如果直接放在冰箱里面， 删除掉了
            //if (rbtIceBox.Checked)
            //{
            //    //获取当前使用的层
            //    if (currentUseLayer == null)
            //    {
            //        currentUseLayer = new IceBoxLayer();
            //        currentUseLayer = layerManage.LayerGetLayerById(disTypeid.ToString(), boxSpecId.ToString(), orgOrBlood.ToString(), specTypeId.ToString());
            //    } 
            //    if (currentUseLayer == null || currentUseLayer.LayerId <= 0)
            //    {
            //        MessageBox.Show("没有找到冰箱直接存放标本盒，请重新设置！", title);
            //        return 0;
            //    }
            //    lastSpecBox = specBoxManage.LayerGetLastCapBox(currentUseLayer.LayerId.ToString());
            //    tmpBox.DesCapType = 'B';
            //    IceBox iceboxTmp = new IceBox();
            //    iceboxTmp = iceboxManage.GetIceBoxByLayerID(currentUseLayer.LayerId.ToString());
            //    string iceboxId = iceboxTmp.IceBoxId.ToString();
            //    //获取标本盒的位置
            //    CalLocationInIceBox();
            //    string boxBarCode = "BX";
            //    boxBarCode += iceboxId.PadLeft(3, '0') + "-C";
            //    boxBarCode += currentUseLayer.LayerNum.ToString().PadLeft(2, '0')+"-J";
            //    boxBarCode += tmpBox.DesCapRow.ToString().PadLeft(2, '0');
            //    boxBarCode += tmpBox.DesCapCol.ToString().PadLeft(2, '0');
            //    boxBarCode += tmpBox.DesCapSubLayer.ToString().PadLeft(2, '0');
            //    tmpBox.BoxBarCode = boxBarCode;
            //    tmpBox.DesCapID = currentUseLayer.LayerId;
            //}
            //else
            //{
            #endregion

            if (currentShelf == null)
            {
                currentShelf = new Shelf();
                currentShelf = shelfManage.GetShelf(disTypeid.ToString(), boxSpecId.ToString(), specTypeId.ToString(), "0");

            }
            if (currentShelf == null || currentShelf.ShelfID <= 0)
            {
                MessageBox.Show("没有找到冻存架存放标本盒，请重新设置！", title);
                return 0;
            }
            lastSpecBox = specBoxManage.ShelfGetLastCapBox(currentShelf.ShelfID.ToString());
            shelfSpec = shelfSpecManage.GetShelfByShelf(currentShelf.ShelfID.ToString());
            CalLocationInShelf();
            tmpBox.DesCapType = 'J';
            string boxBarCode = currentShelf.SpecBarCode;// +currentShelf.IceBoxLayer.Col.ToString().PadLeft(2, '0');
            boxBarCode += "-" + tmpBox.DesCapRow.ToString().PadLeft(1, '0');
            boxBarCode += tmpBox.DesCapSubLayer.ToString().PadLeft(1, '0');
            tmpBox.BoxBarCode = boxBarCode;
            tmpBox.DesCapID = currentShelf.ShelfID;                  
 
           // }
            return 1;

        }

        /// <summary>
        /// 判断标本盒容器是否已满
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private bool CapacityIsFull(int col, int row, int height)
        {
            if (tmpBox.DesCapID <= 0)
            {
                return false;
            }
            if (tmpBox.DesCapRow == row && tmpBox.DesCapCol == col && tmpBox.DesCapSubLayer == height)
            {              
                return true;
            }
            return false; 
        }

        /// <summary>
        /// 盒在架子上定位
        /// </summary>
        private void CalLocationInShelf()
        {
            if (inShelfCol > 0 && inShelfHeight > 0 && inShelfRow > 0)
            {
                tmpBox.DesCapCol = inShelfCol;
                tmpBox.DesCapRow = inShelfRow;
                tmpBox.DesCapSubLayer = inShelfHeight;
                return;
            }
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
                tmpBox.DesCapCol = lastCol + 1;
                tmpBox.DesCapRow = lastRow;
                tmpBox.DesCapSubLayer = lastHeight;
                return;
            }

            if (lastCol == shelfCol && lastHeight < shelfHeight)
            {
                tmpBox.DesCapCol = 1;
                tmpBox.DesCapSubLayer = lastHeight + 1;
                tmpBox.DesCapRow = lastRow;
                return;
            }
            if (lastCol == shelfCol && lastHeight == shelfHeight && lastRow < shelfRow)
            {
                tmpBox.DesCapCol = 1;
                tmpBox.DesCapSubLayer = 1;
                tmpBox.DesCapRow = lastSpecBox.DesCapRow + 1;
                return;
            } 
 
        }

        /// <summary>
        /// 计算盒子的位置
        /// </summary>
        private void CalLocationInIceBox()
        {
            //获取当前使用层的行，列，高度
            int layerCol = currentUseLayer.Col;
            int layerRow = currentUseLayer.Row;
            int layerHeight = currentUseLayer.Height;
            //获取编号最大盒子所在的行，列，层
            int lastCol = lastSpecBox.DesCapCol;
            int lastRow = lastSpecBox.DesCapRow;
            int lastHeight = lastSpecBox.DesCapSubLayer;
            if (lastCol < layerCol)
            {                
                tmpBox.DesCapCol = lastCol + 1;
                tmpBox.DesCapRow = lastRow;
                tmpBox.DesCapSubLayer = lastHeight;
                return;
            }
            if(lastCol==layerCol && lastHeight<layerHeight)
            {
                tmpBox.DesCapCol = 1;
                tmpBox.DesCapSubLayer = lastHeight + 1;
                tmpBox.DesCapRow = lastRow;
                return;
            }
            if (lastCol == layerCol && lastHeight == layerHeight && lastRow < layerRow)
            {
                tmpBox.DesCapCol = 1;
                tmpBox.DesCapSubLayer = 1;
                tmpBox.DesCapRow = lastRow + 1;
                return;
            } 
        }

        /// <summary>
        /// 保存标本盒
        /// </summary>
        private int SaveSpecBox()
        {
            int result = BoxLocate();
            if(result==0)
                return result;
            string sequence = "";
            specBoxManage.GetNextSequence(ref sequence);
            tmpBox.BoxId = Convert.ToInt32(sequence);
            if (specBoxManage.InsertSpecBox(tmpBox) <= 0)
            {                
                MessageBox.Show("插入标本失败！");
                return -1;
            }
            if (capLogManage.ModifyBoxLog(new SpecBox(), loginPerson.Name, "N", tmpBox, "新建标本盒") <= 0)
            {
                MessageBox.Show("写入日志失败！");
                return -1;
            }

            this.txtBoxCode.Text = tmpBox.BoxBarCode;
            //if (rbtIceBox.Checked)
            //{
            //    if (CapacityIsFull(currentUseLayer.Col, currentUseLayer.Row, currentUseLayer.Height))
            //    {
            //        if (layerManage.UpdateOccupy("1", currentUseLayer.LayerId.ToString()) <= 0)
            //        {
            //            return -1;
            //        }
            //    }
            //    currentUseLayer.OccupyCount = currentUseLayer.OccupyCount + 1;
            //    if (layerManage.UpdateOccupyCount(currentUseLayer.OccupyCount.ToString(), currentUseLayer.LayerId.ToString()) <= 0)
            //    {
            //        return -1;
            //    }
            //}
            //if (rbtShelf.Checked)
            //{
                currentShelf.OccupyCount = currentShelf.OccupyCount + 1;
                if (shelfManage.UpdateOccupyCount(currentShelf.OccupyCount.ToString(), currentShelf.ShelfID.ToString()) <= 0)
                {
                    MessageBox.Show("更新冻存架失败！");
                    return -1;
                }
                if (CapacityIsFull(shelfSpec.Col, shelfSpec.Row, shelfSpec.Height))
                {
                    currentShelf.OccupyCount = currentShelf.Capacity;
                    if (shelfManage.UpdateIsFull("1", currentShelf.ShelfID.ToString()) <= 0)
                    {
                        MessageBox.Show("更新冻存架失败！");
                        return -1;
                    }
                    if (shelfManage.UpdateOccupyCount(currentShelf.OccupyCount.ToString(), currentShelf.ShelfID.ToString()) <= 0)
                    {
                        MessageBox.Show("更新冻存架失败！");
                        return -1;
                    }
                    ////提示用户添加新架子
                    //frmSpecShelf newShelf = new frmSpecShelf();
                    //newShelf.LayerId = currentShelf.IceBoxLayer.LayerId;
                    //newShelf.Show();
                }               
            //}
            txtLocation.Text = this.ParseBoxCode(tmpBox, tmpBox.DesCapType);
            return result;
        }

        /// <summary>
        /// 获取用过但为空的盒子
        /// </summary>
        /// <param name="saveType"></param>
        /// <returns>true:有, false: 无</returns>
        private bool GetUsedBox(string saveType)
        {            
            SpecBox usedBox = new SpecBox();
            string specUse = "";
            if (chk115.Checked)
            {
                specUse = "1";
            }
            if (chk863.Checked)
            {
                specUse = "8";
            }
            //在指定的层中添加盒子执行
            if (currentUseLayer != null && currentUseLayer.LayerId > 0)
            {
                usedBox = specBoxManage.GetUsedNoOccupyBox("B", currentUseLayer.LayerId.ToString(),specUse);
            }
            //在指定的架子中添加盒子执行
            if (currentShelf != null && currentShelf.ShelfID > 0)
            {
                usedBox = specBoxManage.GetUsedNoOccupyBox(cmbDiseaseType.SelectedValue.ToString(), cmbBoxSpec.SelectedValue.ToString(), "J", cmbOrgOrBlood.SelectedValue.ToString(), cmbSpecType.SelectedValue.ToString(),specUse);
            }
            if (currentShelf == null && currentUseLayer == null)
            {
                usedBox = specBoxManage.GetUsedNoOccupyBox(disTypeid.ToString(), boxSpecId.ToString(), saveType.ToString(), orgOrBlood.ToString(), specTypeId.ToString(),specUse);
            }
            //if(chk115.Checked
            if (usedBox != null && usedBox.BoxId > 0)
            {
                DialogResult result = MessageBox.Show("该病种类型有不使用的空盒，是否添加新的盒子？", title, MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    this.txtBoxCode.Text = usedBox.BoxBarCode;
                    txtLocation.Text = this.ParseBoxCode(usedBox, usedBox.DesCapType);
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 在标本架修改页面中添加标本盒使用
        /// </summary>
        private void SpecBoxShelfSetting()
        {
            Shelf tmpShelf = shelfManage.GetShelfByShelfId(curShelfId.ToString(), "");
            cmbOrgOrBlood.SelectedValue = orgTypeManage.GetBySpecType(tmpShelf.SpecTypeId.ToString()).OrgTypeID; 
            cmbSpecType.SelectedValue = currentShelf.SpecTypeId;
            cmbDiseaseType.SelectedValue = currentShelf.DisTypeId;
            if (currentShelf.SpecTypeId == 9)
            {
                cmbSpecType.Enabled = true;
                cmbOrgOrBlood.Enabled = true;
            }
            else
            {
                cmbSpecType.Enabled = false;
                cmbOrgOrBlood.Enabled = false;
            }
            if (currentShelf.DisTypeId == 16)
            {
                cmbDiseaseType.Enabled = true;
            }
            else
            {
                cmbDiseaseType.Enabled = false;
            }
            if (disTypeid > 0)
            {
                cmbDiseaseType.SelectedValue = disTypeid;
            }
            if (orgOrBlood > 0)
            {
                cmbOrgOrBlood.SelectedValue = orgOrBlood;
            }
            if (specTypeId > 0)
            {
                cmbSpecType.SelectedValue = specTypeId;
            }
            rbtShelf.Checked = true;
            rbtIceBox.Checked = false;
            rbtIceBox.Enabled = false;
            rbtShelf.Enabled = false;
            cmbBoxSpec.SelectedValue = shelfSpecManage.GetShelfByShelf(currentShelf.ShelfID.ToString()).BoxSpec.BoxSpecID;
            cmbBoxSpec.Enabled = false;

            if (tmpBox != null && tmpBox.BoxId > 0)
            {
                try
                {
                    cmbOrgOrBlood.SelectedValue = orgTypeManage.GetBySpecType(tmpBox.SpecTypeID.ToString()).OrgTypeID;
                    cmbSpecType.SelectedValue = tmpBox.SpecTypeID;
                    cmbDiseaseType.SelectedValue = tmpBox.DiseaseType.DisTypeID;
                    if (tmpBox.SpecialUse == "8")
                    {
                        chk863.Checked = true;
                    }
                    if (tmpBox.SpecialUse == "1")
                    {
                        chk115.Checked = true;
                    }
                    this.projectId = tmpBox.SpecialUse;
                }
                catch
                {
                    MessageBox.Show("获取标本盒信息出错");
                }
            }
        }

        /// <summary>
        /// 在冰箱层修改页面中添加标本盒使用
        /// </summary>
        private void IceBoxLayerSetting()
        {           
            cmbOrgOrBlood.SelectedValue = Convert.ToInt32(currentUseLayer.BloodOrOrgId);
            cmbSpecType.SelectedValue = currentUseLayer.SpecTypeID;
            cmbDiseaseType.SelectedValue = currentUseLayer.DiseaseType.DisTypeID;
            if (currentUseLayer.SpecTypeID == 1)
            {
                cmbSpecType.Enabled = true;
                cmbOrgOrBlood.Enabled = true;
            }
            else
            {
                cmbSpecType.Enabled = false;
                cmbOrgOrBlood.Enabled = false;
            }
            if (currentUseLayer.DiseaseType.DisTypeID == 1)
            {
                cmbDiseaseType.Enabled = true;
            }
            else
            {
                cmbDiseaseType.Enabled = false;
            }
            rbtShelf.Checked = false;
            rbtIceBox.Checked = true;
            rbtIceBox.Enabled = false;
            rbtShelf.Enabled = false;
            cmbBoxSpec.SelectedValue = currentUseLayer.SpecID;
            cmbBoxSpec.Enabled = false;
        }

        private bool ConfirmShelfIsFull(Shelf shelf)
        {
            if (shelf == null || shelf.ShelfID <= 0)
            {
                return true;
            }
            if (shelf.Capacity == shelf.OccupyCount || shelf.IsOccupy == '1')
            {
                //MessageBox.Show("标本架已满，不能添加标本盒", title);
                currentShelf = null;
                return true;
            }
            return false;
        }

        #endregion

        #region 事件

        private void frmSpecBox_Load(object sender, EventArgs e)
        {
            this.projectId = string.Empty;
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            DisTypeBinding();
            BoxSpecBinding();
            BindingSpecClass();
            if (curShelfId > 0)
            {                
                currentShelf = new Shelf();
                currentShelf = shelfManage.GetShelfByShelfId(curShelfId.ToString(),"");
                if (oper == "" &&  ConfirmShelfIsFull(currentShelf) )
                {
                    this.Close();
                    return;                    
                }
                SpecBoxShelfSetting();
            }
            if (curLayerId > 0)
            {
                currentUseLayer = new IceBoxLayer();
                currentUseLayer = layerManage.GetLayerById(curLayerId.ToString());
                if (currentUseLayer.Capacity == currentUseLayer.OccupyCount)
                {
                    MessageBox.Show("冰箱层已满，不能添加标本盒", title);
                    currentUseLayer = null;
                    return;
                }
                IceBoxLayerSetting();
            }
        }

        private void rbtShelf_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //病种ID
            disTypeid = Convert.ToInt32(cmbDiseaseType.SelectedValue);
            //盒规格ID
            boxSpecId = Convert.ToInt32(cmbBoxSpec.SelectedValue);
            //标本种类ID
            orgOrBlood = Convert.ToInt32(cmbOrgOrBlood.SelectedValue);
            //标本类型ID
            specTypeId = Convert.ToInt32(cmbSpecType.SelectedValue);
            if (oper == "M")
            {
                tmpBox.DiseaseType.DisTypeID = disTypeid;
                tmpBox.SpecTypeID = specTypeId;
                tmpBox.SpecialUse = "";
                if (chk863.Checked)
                {
                    tmpBox.SpecialUse = "8";
                }
                if (chk115.Checked)
                {
                    tmpBox.SpecialUse = "1";
                }
                if (!string.IsNullOrEmpty(this.projectId))
                {
                    tmpBox.SpecialUse = this.projectId;
                }
                if (specBoxManage.UpdateSpecBox(tmpBox) > 0)
                {
                    MessageBox.Show("更新成功!");
                }
                else
                {
                    MessageBox.Show("更新失败!");
                }
                oper = "";
                return;
            }
            string saveType = "";
            saveType = "J";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                iceboxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (GetUsedBox(saveType))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
                if (SaveSpecBox() == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存失败！", title);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                inShelfHeight = 0;
                inShelfRow = 0;
                inShelfCol = 0;

                MessageBox.Show("保存成功！", title);
                bool curShelfIsFull = CapacityIsFull(shelfSpec.Col, shelfSpec.Row, shelfSpec.Height);

                if (curShelfIsFull)
                {
                    shelfManage.UpdateIsFull("1",currentShelf.ShelfID.ToString());
                    MessageBox.Show("当前 " +cmbSpecType.Text +" 冻存架已满，请添加新的冻存架！", title);
                    frmSpecShelf newShelf = new frmSpecShelf();
                    newShelf.LayerId = currentShelf.IceBoxLayer.LayerId;
                    newShelf.Show();
                    currentShelf = null;
                    currentUseLayer = null;
                    curShelfId = 0;
                    curLayerId = 0;
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show("继续添加？", title, MessageBoxButtons.YesNo);

                    if (diagResult == DialogResult.No)
                    {
                        currentShelf = null;
                        currentUseLayer = null;
                        curShelfId = 0;
                        curLayerId = 0;
                        this.Close();
                    }
                }
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败！", title);
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            return;
        }

        private void cmbOrgOrBlood_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgId = cmbOrgOrBlood.SelectedValue.ToString();
            BindingSpecType(orgId);
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtBoxCode.Text = "";
            txtComment.Text = "";
            txtLocation.Text = "";
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
        }

        private void btnBrowseLoc_Click(object sender, EventArgs e)
        {
            frmLocate frmL = new frmLocate();
            frmL.container = "s";
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
                CurShelfId = frmLocate.containerId;
                currentShelf = new Shelf();
                currentShelf = shelfManage.GetShelfByShelfId(curShelfId.ToString(), "");                
                if (currentShelf.Capacity == currentShelf.OccupyCount)
                {
                    MessageBox.Show("标本架已满，不能添加标本盒", title);
                    currentShelf = null;
                    return;
                }
                SpecBoxShelfSetting();
                txtLocation.Text = frmLocate.location;
            }
            catch
            {

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(tmpBox==null || tmpBox.BoxId<=0)
            {
                MessageBox.Show("空盒子不能打印");
                return;
            }
            frmBoxPrint frmPrint = new frmBoxPrint();
            frmPrint.GetBarCode(tmpBox);
            frmPrint.Show();
        }

        private void btnDet_Click(object sender, EventArgs e)
        {
            if (tmpBox == null || tmpBox.BoxId <= 0)
            {
                return;
            }
            ucContainer tmpUc = new ucContainer();
            //Size size = new Size(1028, 1000);
            //tmpUc.ParentForm.Size = size;
            tmpUc.CurBox = tmpBox;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(tmpUc,FormBorderStyle.Fixed3D,FormWindowState.Maximized);
        }

        private void chk863_CheckedChanged(object sender, EventArgs e)
        {
            if (chk863.Checked)
            {
                chk115.Checked = false;
            }
        }

        private void chk115_CheckedChanged(object sender, EventArgs e)
        {
            if (chk115.Checked)
            {
                chk863.Checked = false;
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            frmSpecProject frmPjt = new frmSpecProject();
            frmPjt.StartPosition = FormStartPosition.CenterScreen;
            frmPjt.FrmType = "Setting";
            if (chk863.Checked)
            {
                this.projectId = "8";
            }
            if (chk115.Checked)
            {
                this.projectId = "1";
            }
            frmPjt.Original.ID = this.projectId;
            if (!((Form)frmPjt).Visible)
            {
                frmPjt.ShowDialog();
            }
            if (!string.IsNullOrEmpty(frmPjt.RtObj.ID))
            {
                this.projectId = frmPjt.RtObj.ID;
                if (this.projectId == "8")
                {
                    chk863.Checked = true;
                }
                if (this.projectId == "1")
                {
                    chk115.Checked = true;
                }
            }
            else
            {
                chk863.Checked = false;
                chk115.Checked = false;
                this.projectId = frmPjt.RtObj.ID;
            }
        } 
    } 
}