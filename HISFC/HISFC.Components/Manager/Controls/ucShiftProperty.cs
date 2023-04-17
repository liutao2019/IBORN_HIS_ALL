using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [功能描述: 变更属性记录设置]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007－04]<br></br>
    /// 
    /// <说明>
    ///     1 维护各实体类内需要进行变更记录的属性
    /// </说明>
    /// </summary>
    public partial class ucShiftProperty : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucShiftProperty()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 变更管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.ShiftData shiftManager = new FS.HISFC.BizLogic.Manager.ShiftData();

        /// <summary>
        /// 当前操作实体类
        /// </summary>
        private FS.FrameWork.Models.NeuObject reflectClass = null;

        /// <summary>
        /// 是否新增数据
        /// </summary>
        private bool isNew = false;

        /// <summary>
        /// 已存在的实体类信息
        /// </summary>
        private System.Collections.Hashtable hsExitsClass = new Hashtable();

        /// <summary>
        /// 是否管理员 管理员显示维护列表
        /// </summary>
        private bool isManager = true;

        /// <summary>
        /// 实体类名称
        /// </summary>
        private string reflectClassStr = "";
        #endregion

        #region 属性

        /// <summary>
        /// 是否管理员
        /// </summary>
        [System.ComponentModel.Description("是否管理员 管理员显示维护列表"),Category("设置"),DefaultValue(true)]
        public bool IsManager
        {
            get
            {
                return this.isManager;
            }
            set
            {
                this.isManager = value;

                if (!value)
                {
                    this.splitContainer1.Panel1Collapsed = true;

                    this.toolBarService.SetToolButtonEnabled("增加", false);
                    this.toolBarService.SetToolButtonEnabled("删除", false);
                }
            }
        }

        /// <summary>
        /// 实体类名称
        /// </summary>
        [Description("本窗口处理的实体类信息 IsManager属性设置为False时本属性才有效"),Category("设置")]
        public string ReflectClassStr
        {
            get
            {
                return this.reflectClassStr;
            }
            set
            {
                this.reflectClassStr = value;
            }
        }

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("增加", "增加分类", FS.FrameWork.WinForms.Classes.EnumImageList.X信息, true, false, null);
            toolBarService.AddToolButton("删除", "删除分类", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                this.DelShiftPropertyType();
            }
            if (e.ClickedItem.Text == "增加")
            {
                this.AddShiftProperty();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveShiftProperty();

            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.tvType.ImageList = this.tvType.groupImageList;

            if (this.isManager)
            {
                this.ShowShiftPropertyList();
            }
            else
            {
                if (this.reflectClassStr != "")
                {
                    List<FS.HISFC.Models.Base.ShiftProperty> alList = this.shiftManager.QueryShiftProperty(this.reflectClassStr);
                    if (alList != null)
                    {
                        this.reflectClass = alList[0].ReflectClass;

                        this.ShowShiftPropertyDetail();
                    }
                }
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 向Fp内加入数据
        /// </summary>
        /// <param name="sf">变更属性信息</param>
        /// <param name="iRowIndex">需增加行索引</param>
        /// <returns>成功返回1 失败返回-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Base.ShiftProperty sf,int iRowIndex)
        {
            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);

            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColRecord].Value = sf.IsRecord;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColPropertyName].Text = sf.Property.Name;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColDescription].Text = sf.PropertyDescription;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColShiftCause].Text = sf.ShiftCause;

            this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = sf;

            return 1;
        }

        /// <summary>
        /// 增加大类
        /// </summary>
        protected void AddShiftProperty()
        {
            using (ucAddShiftType ucAdd = new ucAddShiftType())
            {
                ucAdd.HsExitsClass = this.hsExitsClass;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucAdd);
                if (ucAdd.Result == DialogResult.OK)
                {
                    this.reflectClass = ucAdd.ReflectedClass;
                    if (this.reflectClass.Name == "")
                    {
                        this.reflectClass.Name = this.reflectClass.ID;
                    }

                    TreeNode node = new TreeNode(this.reflectClass.Name);
                    node.Tag = this.reflectClass;
                    this.tvType.Nodes[0].Nodes.Add(node);

                    this.tvType.SelectedNode = node;

                    int iRowIndex = 0;
                    foreach (FS.FrameWork.Models.NeuObject property in ucAdd.Properties)
                    {
                        FS.HISFC.Models.Base.ShiftProperty sf = new FS.HISFC.Models.Base.ShiftProperty();
                        sf.ReflectClass = this.reflectClass;
                        sf.Property = property;                     //属性信息
                        sf.PropertyDescription = property.Memo;     //属性描述

                        this.AddDataToFp(sf,iRowIndex);

                        iRowIndex++;
                    }

                    this.hsExitsClass.Add(this.reflectClass.ID,null);
                   
                    this.isNew = true;
                }
            }
        }

        /// <summary>
        /// 获取变更类列表
        /// </summary>
        protected void ShowShiftPropertyList()
        {
            List<FS.FrameWork.Models.NeuObject> shiftList = this.shiftManager.QueryShiftPropertyList();

            if (shiftList == null)
            {
                MessageBox.Show(Language.Msg("获取列表发生错误") + this.shiftManager.Err);
                return;
            }

            this.Clear();
            this.tvType.Nodes.Clear();

            TreeNode parentNode = new TreeNode("变更实体列表",0,0);
            parentNode.Tag = null;
            this.tvType.Nodes.Add(parentNode);

            foreach (FS.FrameWork.Models.NeuObject info in shiftList)
            {
                if (info.Name == "")
                {
                    info.Name = info.ID;
                }
                TreeNode node = new TreeNode(info.Name);

                node.ImageIndex = 2;
                node.SelectedImageIndex = 4;

                node.Tag = info;

                parentNode.Nodes.Add(node);
            }

            this.tvType.ExpandAll();
        }

        /// <summary>
        /// 显示变更属性明细
        /// </summary>
        protected void ShowShiftPropertyDetail()
        {
            if (this.reflectClass == null || this.reflectClass.ID == "")
            {
                return;
            }

            List<FS.HISFC.Models.Base.ShiftProperty> shiftDetail = this.shiftManager.QueryShiftProperty(this.reflectClass.ID);
            if (shiftDetail == null)
            {
                MessageBox.Show(Language.Msg(Language.Msg("获取变更属性明细信息发生错误") + this.shiftManager.Err));
                return;
            }

            int iRowIndex = 0;
            foreach (FS.HISFC.Models.Base.ShiftProperty sf in shiftDetail)
            {
                this.AddDataToFp(sf,iRowIndex);

                if (!this.hsExitsClass.ContainsKey(sf.ReflectClass.ID))
                {
                    hsExitsClass.Add(sf.ReflectClass.ID, null);
                }

                iRowIndex++;
            }
        }

        /// <summary>
        /// 删除当前类
        /// </summary>
        protected void DelShiftPropertyType()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return;
            }

            if (this.reflectClass == null)
            {
                return;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("确认删除该大类吗?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return;
            }

            if (this.shiftManager.DelShiftProperty(this.reflectClass.ID) == -1)
            {
                MessageBox.Show(Language.Msg("删除原类变更属性失败") + this.shiftManager.Err);
                return;
            }

            if (this.hsExitsClass.ContainsKey(this.reflectClass.ID))
            {
                this.hsExitsClass.Remove(this.reflectClass.ID);
            }

            MessageBox.Show(Language.Msg("删除成功"));

            this.ShowShiftPropertyList();            
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected int SaveShiftProperty()
        {
            if (this.reflectClass == null)
            {
                MessageBox.Show(Language.Msg("请选择具体分类"));
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.shiftManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.shiftManager.DelShiftProperty(this.reflectClass.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(Language.Msg("删除原类变更属性失败") + this.shiftManager.Err);
                return -1;
            }

            DateTime sysTime = this.shiftManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Base.ShiftProperty sf = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Base.ShiftProperty;

                sf.IsRecord = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColRecord].Value);
                sf.ShiftCause = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColShiftCause].Text;
                sf.Oper.ID = this.shiftManager.Operator.ID;
                sf.Oper.OperTime = sysTime;
                if (sf.ShiftCause == "")
                {
                    sf.ShiftCause = sf.Property.Name;
                }

                if (this.shiftManager.InsertShiftProperty(sf) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("变更属性保存失败"));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();;
            MessageBox.Show(Language.Msg("保存成功"));

            this.isNew = false;

            return 1;
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        private void tvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent == null || e.Node.Tag == null)
            {
                this.reflectClass = null;

                return;
            }

            this.reflectClass = e.Node.Tag as FS.FrameWork.Models.NeuObject;

            this.ShowShiftPropertyDetail();
        }

        private void tvType_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (isNew)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("新增数据尚未保存 是否继续?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    return;
                }

                this.isNew = false;
            }
        }

        #endregion

        #region 列设置

        private enum ColumnSet
        { 
            /// <summary>
            /// 是否记录变更
            /// </summary>
            ColRecord,
            /// <summary>
            /// 属性名称
            /// </summary>
            ColPropertyName,
            /// <summary>
            /// 描述
            /// </summary>
            ColDescription,
            /// <summary>
            /// 变更原因
            /// </summary>
            ColShiftCause
        }

        #endregion               
    }
}
