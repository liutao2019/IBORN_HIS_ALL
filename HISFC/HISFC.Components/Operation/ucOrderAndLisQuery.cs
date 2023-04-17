using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// 患者医嘱/LIS结果查询
    /// 创建者：冯超
    /// 创建时间：2010-11-2
    /// </summary>
    public partial class ucOrderAndLisQuery : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucOrderAndLisQuery()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime begin = new DateTime();
        /// <summary>
        ///  结束时间
        /// </summary>
        DateTime end = new DateTime();
        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion

        #region 属性
        #endregion

        #region 方法
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("LIS检验结果查询", "检验结果查询", 9, true, false, null);
            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "LIS检验结果查询")
            {
                this.ShowLisResult();
            }
        }
        /// <summary>
        /// 显示LIS结果
        /// </summary>
        public void ShowLisResult()
        {
            FS.HISFC.BizProcess.Interface.Common.ILis o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(HISFC.Components.Operation.ucOrderAndLisQuery), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
            if (o == null)
            {
                MessageBox.Show(Language.Msg("没有维护LIS接口！"));
            }
            else
            {
                o.SetPatient(this.patient);
                o.ShowResultByPatient();
            }
        }
        /// <summary>
        /// 初始化患者树
        /// </summary>
        /// <returns></returns>
        private int InitTreeView()
        {
            ArrayList al = null;
            #region 初始化根节点

            this.tvPatientInfo.Nodes.Clear();
            TreeNode root = new TreeNode("未登记");
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "NO_Register";
            this.tvPatientInfo.Nodes.Add(root);

            root = new TreeNode("已登记");
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "Register";
            this.tvPatientInfo.Nodes.Add(root);

            root = new TreeNode("已作废");
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "Cancel";
            this.tvPatientInfo.Nodes.Add(root);


            #endregion

            #region 节点赋值

            //未登记
            al = new ArrayList();
            al = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, begin, end, true);
            if (al != null)
            {
                this.AddToNode(0, 0, al);
            }
            //已登记
            al = new ArrayList();
            al = Environment.RecordManager.GetOperatorRecords(Environment.OperatorDeptID, begin, end);
            if (al != null)
            {
                this.AddToNode(1, 1, al);
            }
            //已作废
            al = new ArrayList();
            al = Environment.OperationManager.GetOpsCancelRecord(Environment.OperatorDeptID, begin, end);
            if (al != null)
            {
                this.AddToNode(0, 2, al);
            }


            #endregion
            return 1;
        }
        /// <summary>
        /// 添加患者子节点
        /// </summary>
        private void AddToNode(int j, int i,ArrayList al)
        {
            this.tvPatientInfo.Nodes[i].Nodes.Clear();
            if (j == 0)
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication apply in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.Dept.ID),
                        "] ", apply.PatientInfo.Name);
                    node.Tag = apply;
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    this.tvPatientInfo.Nodes[i].Nodes.Add(node);
                }
            }
            if (j == 1)
            {
                foreach (FS.HISFC.Models.Operation.OperationRecord apply in al)
                {
                    TreeNode node = new TreeNode();
                    node.Text = string.Concat("[", Environment.GetDept(apply.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID),
                        "] ", apply.OperationAppllication.PatientInfo.Name);
                    node.Tag = apply;
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    this.tvPatientInfo.Nodes[i].Nodes.Add(node);
                }
            }
            this.tvPatientInfo.Nodes[i].ExpandAll();
        }
        #endregion

       

        #region 事件
        /// <summary>
        /// 树节点选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPatientInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "已登记" || e.Node.Text == "未登记" || e.Node.Text == "已作废")
            {
                return;
            }
            FS.HISFC.Models.Operation.OperationAppllication apply=new FS.HISFC.Models.Operation.OperationAppllication();
            FS.HISFC.Models.Operation.OperationRecord record = new  FS.HISFC.Models.Operation.OperationRecord();
            if (e.Node.Parent.Text == "已登记")
            {
                record = e.Node.Tag as FS.HISFC.Models.Operation.OperationRecord;
                patient = record.OperationAppllication.PatientInfo;
            }
            else
            {
                apply = e.Node.Tag as FS.HISFC.Models.Operation.OperationAppllication;
                patient = apply.PatientInfo;
            }            
            this.ucOrderShow1.PatientInfo = patient;
        }
        /// <summary>
        /// 重载查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            begin = this.dtBegin.Value;
            end = this.dtEnd.Value;
            if (end < begin)
            {
                MessageBox.Show("开始时间不能大于结束时间!", "提示");
                begin = end = DateTime.MinValue;
                return -1;
            }
            if (this.InitTreeView() == -1)
            {
                return -1;
            }    
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.dtBegin.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.dtEnd.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            begin = this.dtBegin.Value;
            end = this.dtEnd.Value;
            if (this.InitTreeView() == -1)
            {
                return;
            }
            if (this.ucOrderShow1.InitForOut() == -1)
            {
                MessageBox.Show(Language.Msg("初始化医嘱窗口出错！"));
                return;
            }
            base.OnLoad(e);
        }
        #endregion

        #region IInterfaceContainer 成员
        /// <summary>
        /// LIS接口
        /// </summary>
        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Common.ILis) };
            }
        }

        #endregion
    }
}
