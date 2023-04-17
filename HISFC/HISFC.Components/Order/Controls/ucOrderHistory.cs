using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 历史医嘱查询]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人='Dorian'
    ///		修改时间='2008-01'
    ///		修改目的='根据肿瘤医院的修改情况整合'
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOrderHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderHistory()
        {
            InitializeComponent();
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            //{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
            this.fpSpread1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpSpread1_MouseUp);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag == null) return;
                FS.FrameWork.Models.NeuObject obj = e.Node.Tag as FS.FrameWork.Models.NeuObject;
                ArrayList al = CacheManager.InOrderMgr.QueryOrder(obj.ID);
                //将医嘱分长期与临时分别绑定
                this.AddObjectsToList(al);
                if (alAllLong != null)
                {
                    FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpSpread1_Sheet1, alAllLong, FS.HISFC.Models.Base.ServiceTypes.I);
                }
                if (alAllShort != null)
                {
                    FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpSpread1_Sheet2, alAllShort, FS.HISFC.Models.Base.ServiceTypes.I);
                }
                #region {EE6864E5-796D-4b21-B9C4-ABD40F5CF9A5}
                for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
                {
                    this.fpSpread1_Sheet1.Columns[i].Locked = true;
                }
                for (int i = 0; i < this.fpSpread1_Sheet2.Columns.Count; i++)
                {
                    this.fpSpread1_Sheet2.Columns[i].Locked = true;
                }
                #endregion

            }
            catch { }
        }
        /// <summary>
        /// 长期医嘱ArrayList
        /// </summary>
        protected ArrayList alAllLong = new ArrayList(); 
        /// <summary>
        /// 临时医嘱ArrayList
        /// </summary>
        protected ArrayList alAllShort = new ArrayList();
        /// <summary>
        /// 添加实体toArrayList
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToList(ArrayList list)
        {
            if (alAllLong != null)
                alAllLong.Clear();
            if (alAllShort != null)
                alAllShort.Clear();
            foreach (object obj in list)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //长期医嘱

                    alAllLong.Add(order);
                }
                else
                {
                    //临时医嘱
                    alAllShort.Add(order);
                }
            }
        }

        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        //是否显示住院号输入框
        private bool isShowPatientControl = false;

        /// <summary>
        /// 是否显示住院号输入框
        /// </summary>
        [Category("控件设置"),Description("是否显示住院号输入框"),DefaultValue(false)]
        public bool IsShowPatientControl
        {
            get { return isShowPatientControl; }
            set { this.isShowPatientControl = value; }
        }


        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return 0;
        }

        protected FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                if (value == null) return;
                //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
                this.txtPatientNO.Text = value.PID.PatientNO;
                ArrayList al = CacheManager.RadtIntegrate.QueryInpatientNoByPatientNo(value.PID.PatientNO);
                if (al == null)
                {
                    MessageBox.Show("获取患者信息出错", CacheManager.RadtIntegrate.Err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.treeView1.Nodes[0].Nodes.Clear();
                    if (this.fpSpread1_Sheet1.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                    }
                    if (this.fpSpread1_Sheet2.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                    }

                    return;
                }
                if (al.Count == 0)
                {
                    this.treeView1.Nodes[0].Nodes.Clear();
                    if (this.fpSpread1_Sheet1.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                    }
                    if (this.fpSpread1_Sheet2.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                    }
                    return;
                }

                this.treeView1.Nodes[0].Nodes.Clear();
                #region {1705F464-D530-47e8-A675-761A10CA8E52}
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                }
                if (this.fpSpread1_Sheet2.Rows.Count > 0)
                {
                    this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                }
                #endregion
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
                    string no = obj.ID;
                    FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfomation(obj.ID);
                    if (p == null)
                    {
                        MessageBox.Show("查询患者信息出错" + CacheManager.RadtIntegrate.Err);
                        return;
                    }
                    if (p.PVisit.InState.ID.ToString() == "I")
                    {
                        continue;
                    }
                    if (obj.ID != value.ID)
                    {
                        #region {7DFF015D-7EE9-447f-8222-B29A39DBD409}
                        //try
                        //{
                        //    no = obj.ID.Substring(2, 2);
                        //}
                        //catch { }
                        //TreeNode node = new TreeNode("[" + no + "]" + obj.Name);
                        //node.ImageIndex = 1;
                        //node.SelectedImageIndex = 2;
                        //node.Tag = obj;
                        //this.treeView1.Nodes[0].Nodes.Add(node); 
                        this.AddPatientToTree(p);
                        #endregion
                    }
                }
            }
        }

        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        private void txtPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtPatientNO.Text.Trim()))
            {
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

            

            this.txtPatientNO.Text = this.txtPatientNO.Text.Trim().PadLeft(10, '0');
            p.PID.PatientNO = this.txtPatientNO.Text;

            this.Patient = p;
        }
        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        protected override void OnLoad(EventArgs e)
        {
            this.txtPatientNO.Visible = this.isShowPatientControl;

            this.neuLabel1.Visible = this.isShowPatientControl;
            this.neuPanel2.Visible = this.isShowPatientControl;

            this.ucQueryInpatientNo1.IsCanChangeInputType = true;
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);

            base.OnLoad(e);
        }

        #region {7DFF015D-7EE9-447f-8222-B29A39DBD409}


        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            string no = this.ucQueryInpatientNo1.InpatientNo;
            FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfomation(no);
            if (p == null)
            {
                MessageBox.Show("查询患者信息出错" + CacheManager.RadtIntegrate.Err);
                return;
            }
            #region {F170B20E-6C35-476c-AD94-F6819B5E7CFD}
            bool isFound = false;
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (p.ID == ((FS.FrameWork.Models.NeuObject)this.treeView1.Nodes[0].Nodes[i].Tag).ID)
                {
                    isFound = true;
                }
            }
            if (isFound)
            {
                MessageBox.Show("该患者已在列表中");
                return;
            }
            else
            {
                this.AddPatientToTree(p);
            }
            //this.AddPatientToTree(p); 
            #endregion
        }

        private void AddPatientToTree(FS.HISFC.Models.RADT.PatientInfo myPatient)
        {
            string inTimes = string.Empty;//住院次数
            string inTime = string.Empty;//入院日期
            try
            {
                //update by zhaorong at 2013-9-2 将历次住院的住院号修改为入院时间
                //no = myPatient.ID.Substring(2, 2);
                inTimes = myPatient.InTimes.ToString();
                inTime = myPatient.PVisit.InTime.ToString("yyyy-MM-dd");
            }
            catch { }
            TreeNode node = new TreeNode("[" + inTime + "]["+ inTimes+"]"+ myPatient.Name);
            node.ImageIndex = 1;
            node.SelectedImageIndex = 2;
            node.Tag = myPatient;
            this.treeView1.Nodes[0].Nodes.Add(node);
        } 
        #endregion

        #region 复制粘贴{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}

        /// <summary>
        /// 右键菜单
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();


        /// <summary>
        /// 复制
        /// </summary>
        private void CopyOrder()
        {
            #region 修改原方法--该方法只针对了fpSpread1_Sheet1 //update by zhaorong at 2013-9-2
            //if (this.fpSpread1_Sheet1.Rows.Count <= 0) return;

            //this.fpSpread1.Focus();

            //ArrayList list = new ArrayList();

            ////获取选中行的医嘱ID
            //for (int row = 0; row < this.fpSpread1_Sheet1.Rows.Count; row++)
            //{
            //    for (int col = 0; col < this.fpSpread1_Sheet1.Columns.Count; col++)
            //    {
            //        if (this.fpSpread1_Sheet1.IsSelected(row, col))
            //        {
            //            list.Add(fpSpread1_Sheet1.Cells[row, 0].Value.ToString());
            //            break;
            //        }
            //    }
            //}

            //if (list.Count <= 0) return;
            ////先添加到COPY列表
            //for (int count = 0; count < list.Count; count++)
            //{
            //    HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            //}
            //string type = "2";
            //HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            ////然后将copy列表放到剪贴板上
            //HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
            #endregion

            if (this.fpSpread1.ActiveSheet.RowCount <= 0) return;

            this.fpSpread1.Focus();

            ArrayList list = new ArrayList();

            //获取选中行的医嘱ID
            for (int row = 0; row < this.fpSpread1.ActiveSheet.RowCount; row++)
            {
                for (int col = 0; col < this.fpSpread1.ActiveSheet.ColumnCount; col++)
                {
                    if (this.fpSpread1.ActiveSheet.IsSelected(row, col))
                    {
                        list.Add(this.fpSpread1.ActiveSheet.Cells[row, 0].Value.ToString());
                        break;
                    }
                }
            }

            if (list.Count <= 0) return;
            //先添加到COPY列表
            for (int count = 0; count < list.Count; count++)
            {
                HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            }
            string type = "2";
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            //然后将copy列表放到剪贴板上
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
        }

        /// <summary>
        /// 右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            int ActiveRowIndex = -1;

            if (e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Items.Clear();
                FarPoint.Win.Spread.Model.CellRange c = fpSpread1.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    //this.fpSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.fpSpread1.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                }
                else
                {
                    ActiveRowIndex = -1;
                }
                if (ActiveRowIndex < 0) 
                    return;

                ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem();
                mnuCopyOrder.Text = "复制";
                mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                this.contextMenu1.Items.Add(mnuCopyOrder);

                this.contextMenu1.Show(this.fpSpread1, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 菜单点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
        }

        #endregion
    }
}
