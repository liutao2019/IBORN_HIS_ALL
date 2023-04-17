using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Visit.UCItemCheckArrange
{
    public partial class ucArrangeRegister : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 东莞桥头医院客服管理-病人预约辅助检查管理界面
        /// 设想:暂时按照执行档获取信息,电子申请单实行后按电子申请单来处理数据
        /// add by chengym 2012-6-12
        /// </summary>
        public ucArrangeRegister()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 辅助检查业务类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Visit.CheckRegister crBizLogic = new FS.SOC.HISFC.BizLogic.Visit.CheckRegister();
        /// <summary>
        /// 操作员业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personLogic = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 常数业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conLogic = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbNurse = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        FS.FrameWork.Public.ObjectHelper nurseHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbSendType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        FS.FrameWork.Public.ObjectHelper sendTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lblValid = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        FS.FrameWork.Public.ObjectHelper validTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.Models.Base.Employee emplInfo = new FS.HISFC.Models.Base.Employee();

        private bool isCancel = false;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarServics = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarServics.AddToolButton("删除", "删除已安排", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return toolBarServics;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "删除":
                    if (this.tabControl1.SelectedIndex == 0)
                    {
                        return;
                    }
                    else
                    {
                        this.isCancel = true;
                        this.Save();
                    }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        protected override void OnLoad(EventArgs e)
        {
            this.dtBegin.Value = this.crBizLogic.GetDateTimeFromSysDateTime().Date.AddDays(-3);
            this.dtEnd.Value = this.crBizLogic.GetDateTimeFromSysDateTime().Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            emplInfo = this.crBizLogic.Operator as FS.HISFC.Models.Base.Employee;
            this.fpSpread1.AddListBoxPopup(lbNurse, 5);
            this.fpSpread1.AddListBoxPopup(lbNurse, 6);
            this.fpSpread1.AddListBoxPopup(lbSendType, 8);
            this.fpSpread2.AddListBoxPopup(lblValid, 9);
            this.InitNurseListBox();
            this.InitSendTypeListBox();
            this.InitValidListBox();
            base.OnLoad(e);
        }

        private void InitValidListBox()
        {
            ArrayList alValid = this.conLogic.GetList("CAS_STATUS");
            validTypeHelper.ArrayObject = alValid;
            lblValid.AddItems(alValid);
            lblValid.Font = new Font("宋体", 12);
            lblValid.Size = new Size(220, 96);
            this.Controls.Add(lblValid);
            this.lblValid.Hide();
            this.lblValid.BorderStyle = BorderStyle.FixedSingle;
            this.lblValid.BringToFront();
            this.lblValid.ItemSelected += new EventHandler(lblValid_ItemSelected);
            return;
        }

        
        /// <summary>
        /// 添加护士listbox列表
        /// </summary>
        /// <returns></returns>
        private int InitNurseListBox()
        {
            ArrayList nurses = this.personLogic.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N,emplInfo.Dept.ID);
            nurseHelper.ArrayObject = nurses;
            lbNurse.AddItems(nurses);
            lbNurse.Font = new Font("宋体", 12);
            lbNurse.Size = new Size(220, 96);
            this.Controls.Add(lbNurse);
            this.lbNurse.Hide();
            this.lbNurse.BorderStyle = BorderStyle.FixedSingle;
            this.lbNurse.BringToFront();
            this.lbNurse.ItemSelected += new System.EventHandler(this.lbNurse_ItemSelected);
            return 0;
        }
        /// <summary>
        /// 添加护士listbox列表
        /// </summary>
        /// <returns></returns>
        private int InitSendTypeListBox()
        {
            ArrayList sentType = this.conLogic.GetList("CHECKSENFTYPE");
            sendTypeHelper.ArrayObject = sentType;
            lbSendType.AddItems(sentType);
            lbSendType.Font = new Font("宋体", 12);
            lbSendType.Size = new Size(220, 96);
            this.Controls.Add(lbSendType);
            this.lbSendType.Hide();
            this.lbSendType.BorderStyle = BorderStyle.FixedSingle;
            this.lbSendType.BringToFront();
            this.lbSendType.ItemSelected += new System.EventHandler(this.lbSendType_ItemSelected);
            return 0;
        }
        private void lbNurse_ItemSelected(object sender, System.EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.SelectNurse(fpSpread1_Sheet1.ActiveColumnIndex,"0");
            }
            else
            {
                this.SelectNurse(fpSpread2_Sheet1.ActiveColumnIndex,"1");
            }
        }
        private void lbSendType_ItemSelected(object sender, System.EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.SelectSendType(fpSpread1_Sheet1.ActiveColumnIndex,"0");
            }
            else
            {
                this.SelectSendType(fpSpread2_Sheet1.ActiveColumnIndex,"1");
            }
        }
        private void lblValid_ItemSelected(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.SelectValidType(fpSpread1_Sheet1.ActiveColumnIndex, "0");
            }
            else
            {
                this.SelectValidType(fpSpread2_Sheet1.ActiveColumnIndex, "1");
            }
        }
        /// <summary>
        ///  选择护士
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        private int SelectNurse(int Column,string type)
        {
            if (type == "0")
            {
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread1.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lbNurse.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread1_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread1_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lbNurse.Visible = false;
            }
            else
            {
                int CurrentRow = fpSpread2_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread2.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lbNurse.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread2_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread2_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lbNurse.Visible = false;
            }
            return 0;
        }
        /// <summary>
        ///  选择护士
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        private int SelectSendType(int Column,string type)
        {
            if (type == "0")
            {
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread1.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lbSendType.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread1_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread1_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lbSendType.Visible = false;
            }
            else
            {
                int CurrentRow = fpSpread2_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread2.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lbSendType.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread2_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread2_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lbSendType.Visible = false;
            }
            return 0;
        }
        private int SelectValidType(int Column, string type)
        {
            if (type == "0")
            {
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread1.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lblValid.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread1_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread1_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lblValid.Visible = false;
            }
            else
            {
                int CurrentRow = fpSpread2_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                fpSpread2.StopCellEditing();
                FS.FrameWork.Models.NeuObject item = this.lblValid.GetSelectedItem();

                if (item == null)
                    return -1;

                fpSpread2_Sheet1.Cells[CurrentRow, Column].Tag = item;
                fpSpread2_Sheet1.SetValue(CurrentRow, Column, item.Name, false);

                lblValid.Visible = false;
            }
            return 0;
        }
        /// <summary>
        /// 设置护士、手术台列表位置
        /// </summary>
        /// <returns></returns>
        private int SetLocation(string Type)
        {
            if (Type == "0")
            {
                Control _cell = fpSpread1.EditingControl;
                if (_cell == null) return 0;

                //未安排
                int Column = fpSpread1_Sheet1.ActiveColumnIndex;
                if (Column == 5 || Column == 6)
                {
                    lbNurse.Location = new Point(this.panel4.Location.X + _cell.Location.X + 200,
                     this.panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 90);
                    lbNurse.Size = new Size(150, 150);
                }
                else if (Column == 8)
                {
                    this.lbSendType.Location = new Point(this.panel4.Location.X + _cell.Location.X + 250,
                     this.panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 90);
                    lbSendType.Size = new Size(150, 150);
                }
            }
            else
            {
                //已安排
                Control _cell = fpSpread2.EditingControl;
                if (_cell == null) return 0;
                int ColumnReady = fpSpread2_Sheet1.ActiveColumnIndex;
                if (ColumnReady == 5 || ColumnReady == 6)
                {
                    lbNurse.Location = new Point(this.panel5.Location.X + _cell.Location.X + 200,
                     this.panel5.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 90);
                    lbNurse.Size = new Size(150, 150);
                }
                else if (ColumnReady == 8)
                {
                    this.lbSendType.Location = new Point(this.panel5.Location.X + _cell.Location.X + 250,
                     this.panel5.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 90);
                    lbSendType.Size = new Size(150, 150);
                }
                else if (ColumnReady == 9)
                {
                    this.lblValid.Location = new Point(this.panel5.Location.X + _cell.Location.X + 250,
                     this.panel5.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2 + 90);
                    lblValid.Size = new Size(150, 150);
                }
            }
            return 0;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (fpSpread1.ContainsFocus)
                {
                    //洗手
                    if (fpSpread1_Sheet1.ActiveColumnIndex == 5)
                    {
                        if (lbNurse.Visible)
                            SelectNurse(5,"0");
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 5, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == 6) 
                    {
                        if (lbNurse.Visible)
                            SelectNurse(6,"0");
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 6, false);
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex == 8)
                    {
                        if (this.lbSendType.Visible)
                            SelectSendType(8, "0");
                        fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 8, false);
                    }
                    else if (fpSpread2_Sheet1.ActiveColumnIndex == 5)//已安排的
                    {
                        if (lbNurse.Visible)
                            SelectNurse(5, "1");
                        fpSpread2_Sheet1.SetActiveCell(fpSpread2_Sheet1.ActiveRowIndex, 5, false);
                    }
                    else if (fpSpread2_Sheet1.ActiveColumnIndex == 6)
                    {
                        if (lbNurse.Visible)
                            SelectNurse(6, "1");
                        fpSpread2_Sheet1.SetActiveCell(fpSpread2_Sheet1.ActiveRowIndex, 6, false);
                    }
                    else if (fpSpread2_Sheet1.ActiveColumnIndex == 8)
                    {
                        if (this.lbSendType.Visible)
                            SelectSendType(8, "1");
                        fpSpread2_Sheet1.SetActiveCell(fpSpread2_Sheet1.ActiveRowIndex, 8, false);
                    }
                }
                #endregion
            }
            return base.ProcessDialogKey(keyData);
        }
        private void fpSpread1_EditModeOn(object sender, EventArgs e)
        {
            SetLocation("0");
            try
            {
                int ColumnIndex = fpSpread1_Sheet1.ActiveColumnIndex;
                if (ColumnIndex != 5 && ColumnIndex != 6)
                {
                    lbNurse.Visible = false;
                }
                else
                {
                    lbNurse.Visible = true;
                    lbNurse.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                }
                if (ColumnIndex != 8)
                {
                    lbSendType.Visible = false;
                }
                else
                {
                    lbSendType.Visible = true;
                    lbSendType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                }
            }
            catch { }
        }

        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string _Text;
            if (e.Column == 5 || e.Column == 6)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                lbNurse.Filter(_Text);

                if (lbNurse.Visible == false)
                    lbNurse.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
            else if (e.Column == 8)
            {
                _Text = fpSpread1_Sheet1.ActiveCell.Text;
                lbSendType.Filter(_Text);

                if (lbSendType.Visible == false)
                    lbSendType.Visible = true;
                fpSpread1_Sheet1.SetTag(e.Row, e.Column, null);
            }
        }

        private void fpSpread2_EditModeOn(object sender, EventArgs e)
        {
            SetLocation("1");
            try
            {
                int ColumnIndex = fpSpread2_Sheet1.ActiveColumnIndex;
                if (ColumnIndex != 5 && ColumnIndex != 6)
                {
                    lbNurse.Visible = false;
                }
                else
                {
                    lbNurse.Visible = true;
                    lbNurse.Filter(fpSpread2_Sheet1.ActiveCell.Text);
                }
                if (ColumnIndex != 8)
                {
                    lbSendType.Visible = false;
                }
                else
                {
                    lbSendType.Visible = true;
                    lbSendType.Filter(fpSpread2_Sheet1.ActiveCell.Text);
                }
                if (ColumnIndex != 9)
                {
                    lblValid.Visible = false;
                }
                else
                {
                    lblValid.Visible = true;
                    lblValid.Filter(fpSpread2_Sheet1.ActiveCell.Text);
                }
            }
            catch { }
        }

        private void fpSpread2_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string _Text;
            if (e.Column == 5 || e.Column == 6)
            {
                _Text = fpSpread2_Sheet1.ActiveCell.Text;
                lbNurse.Filter(_Text);

                if (lbNurse.Visible == false)
                    lbNurse.Visible = true;
                fpSpread2_Sheet1.SetTag(e.Row, e.Column, null);
            }
            else if (e.Column == 8)
            {
                _Text = fpSpread2_Sheet1.ActiveCell.Text;
                lbSendType.Filter(_Text);

                if (lbSendType.Visible == false)
                    lbSendType.Visible = true;
                fpSpread2_Sheet1.SetTag(e.Row, e.Column, null);
            }
            else if (e.Column == 9)
            {
                _Text = fpSpread2_Sheet1.ActiveCell.Text;
                lblValid.Filter(_Text);
                if (!lblValid.Visible)
                {
                    lblValid.Visible = true;
                }
                fpSpread2_Sheet1.SetTag(e.Row, e.Column, null);
            }
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        private void Query()
        {
            if (this.tabControl2.SelectedIndex == 0)
            {
                this.fpSpread1_Sheet1.RowCount = 0;
                List<FS.FrameWork.Models.NeuObject> deptCheckList = new List<FS.FrameWork.Models.NeuObject>();
                deptCheckList = this.crBizLogic.QueryDeptCheckRegister(this.dtBegin.Value, this.dtEnd.Value);
                if (deptCheckList == null)
                {
                    MessageBox.Show("获取科室已审核辅助检查信息错误", "提示");
                    return;
                }
                if (deptCheckList.Count == 0)
                {
                    MessageBox.Show("未获取到科室已审核辅助检查信息", "提示");
                    return;
                }
                this.Refresh(deptCheckList);
            }
            else
            {
                this.fpSpread2_Sheet1.RowCount = 0;
                List<FS.FrameWork.Models.NeuObject> readyDeptCheckList = new List<FS.FrameWork.Models.NeuObject>();
                readyDeptCheckList = this.crBizLogic.QueryReadyDeptCheckRegister(this.dtBegin.Value, this.dtEnd.Value);
                if (readyDeptCheckList == null)
                {
                    MessageBox.Show("获取科室已安排辅助检查信息错误", "提示");
                    return;
                }
                if (readyDeptCheckList.Count == 0)
                {
                    MessageBox.Show("未获取到科室已安排辅助检查信息", "提示");
                    return;
                }
                this.Refresh(readyDeptCheckList);
            }
        }

        private void Refresh(List<FS.FrameWork.Models.NeuObject> list)
        {
            if (this.tabControl2.SelectedIndex == 0)
            {
                this.DeptTree.Nodes.Clear();
                TreeNode RootNode = new TreeNode();
                RootNode.Text = "科室列表";
                RootNode.Tag = null;
                this.DeptTree.Nodes.Add(RootNode);

                foreach (FS.FrameWork.Models.NeuObject info in list)
                {
                    TreeNode node = new TreeNode();
                    node.Text = info.Name + "(" + info.Memo + ")";
                    node.Tag = info;
                    this.DeptTree.Nodes[0].Nodes.Add(node);
                }
                this.DeptTree.ExpandAll();
            }
            else if (this.tabControl2.SelectedIndex == 1)
            {
                this.DeptTreeReady.Nodes.Clear();
                TreeNode ParentNode = new TreeNode();
                ParentNode.Text = "科室列表";
                ParentNode.Tag = null;
                this.DeptTreeReady.Nodes.Add(ParentNode);

                foreach (FS.FrameWork.Models.NeuObject Rinfo in list)
                {
                    TreeNode Cnode = new TreeNode();
                    Cnode.Text = Rinfo.Name + "(" + Rinfo.Memo + ")";
                    Cnode.Tag = Rinfo;
                    this.DeptTreeReady.Nodes[0].Nodes.Add(Cnode);
                }
                this.DeptTreeReady.ExpandAll();
            }
        }

        private void DeptTree_DoubleClick(object sender, EventArgs e)
        {
            if (this.DeptTree.SelectedNode.Tag != null)
            {
                FS.FrameWork.Models.NeuObject obj =this.DeptTree.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                List<FS.HISFC.Models.Fee.Item.Undrug> detailList = new List<FS.HISFC.Models.Fee.Item.Undrug>();
                detailList = this.crBizLogic.QueryDeptDetailCheckRegister(this.dtBegin.Value, this.dtEnd.Value, obj.ID);
                if (detailList == null)
                {
                    return;
                }
                this.fpSpread1_Sheet1.RowCount=0;
                foreach (FS.HISFC.Models.Fee.Item.Undrug info in detailList)
                {
                    this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
                    int row = this.fpSpread1_Sheet1.RowCount - 1;
                    this.fpSpread1_Sheet1.Cells[row, 0].Text = info.CheckApplyDept;//4患者姓名
                    this.fpSpread1_Sheet1.Cells[row, 1].Text = info.CheckBody;//床号
                    if (row - 1 >= 0)//同一个患者清空赋值
                    {
                        if (this.fpSpread1_Sheet1.Cells[row - 1, 0].Text == info.CheckApplyDept)
                        {
                            this.fpSpread1_Sheet1.Cells[row, 0].Text = "";//4患者姓名
                            this.fpSpread1_Sheet1.Cells[row, 1].Text = "";
                        }
                    }
                    this.fpSpread1_Sheet1.Cells[row, 2].Text = "false";
                    this.fpSpread1_Sheet1.Cells[row, 3].Text = info.DiseaseType.Name;//项目名称
                    this.fpSpread1_Sheet1.Cells[row, 4].Text = info.Oper.OperTime.ToString();//执行日期 

                    this.fpSpread1_Sheet1.Cells[row, 7].Text = System.DateTime.Now.ToString();
                    this.fpSpread1_Sheet1.Rows[row].Tag = info;
                }
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        private void Save()
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                List<FS.HISFC.Models.Fee.Item.Undrug> sList = new List<FS.HISFC.Models.Fee.Item.Undrug>();
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    if (this.fpSpread1_Sheet1.Cells[row, 2].Text == "True")
                    {
                        FS.HISFC.Models.Fee.Item.Undrug info = this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                        info.OperationInfo.ID = this.nurseHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 5].Text);
                        info.OperationInfo.Name = this.nurseHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 6].Text);
                        info.OperationInfo.Memo = this.fpSpread1_Sheet1.Cells[row, 7].Text;
                        info.OperationScale.Memo = this.sendTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 8].Text);
                        info.ValidState = "1";
                        info.Oper.ID = this.emplInfo.ID;
                        info.Oper.OperTime = this.crBizLogic.GetDateTimeFromSysDateTime();
                        if (this.VaildExeOper(info, row + 1) == -1)
                        {
                            return;
                        }
                        sList.Add(info);
                    }
                }
                if (sList == null || sList.Count == 0)
                {
                    return;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.crBizLogic.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存请稍候!");
                Application.DoEvents();
                foreach (FS.HISFC.Models.Fee.Item.Undrug unInfo in sList)
                {
                    if (this.crBizLogic.InsertDeptCheckRegister(unInfo) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("插入辅助检查信息失败!请检查该患者是否已经安排");
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("插入辅助检查信息成功!");
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                List<FS.HISFC.Models.Fee.Item.Undrug> updateList = new List<FS.HISFC.Models.Fee.Item.Undrug>();
                for (int row = 0; row < this.fpSpread2_Sheet1.RowCount; row++)
                {
                    if (this.fpSpread2_Sheet1.Cells[row, 2].Text == "True")
                    {
                        FS.HISFC.Models.Fee.Item.Undrug info = this.fpSpread2_Sheet1.Rows[row].Tag as FS.HISFC.Models.Fee.Item.Undrug;
                        info.OperationInfo.ID = this.nurseHelper.GetID(this.fpSpread2_Sheet1.Cells[row, 5].Text);
                        info.OperationInfo.Name = this.nurseHelper.GetID(this.fpSpread2_Sheet1.Cells[row, 6].Text);
                        info.OperationInfo.Memo = this.fpSpread2_Sheet1.Cells[row, 7].Text;
                        info.OperationScale.Memo = this.sendTypeHelper.GetID(this.fpSpread2_Sheet1.Cells[row, 8].Text);
                        
                        if (this.isCancel)
                        {
                            info.ValidState = "0";
                        }
                        else
                        {
                            if (this.fpSpread2_Sheet1.Cells[row, 9].Text.ToString() == "无效")
                            {
                                info.ValidState = "0";
                            }
                            else if (this.fpSpread2_Sheet1.Cells[row, 9].Text.ToString() == "有效")
                            {
                                info.ValidState = "1";
                            }
                            
                        }
                        info.Oper.ID = this.emplInfo.ID;
                        info.Oper.OperTime = this.crBizLogic.GetDateTimeFromSysDateTime();
                        if (this.VaildExeOper(info, row + 1) == -1)
                        {
                            return;
                        }
                        updateList.Add(info);
                    }
                }
                if (updateList == null || updateList.Count == 0)
                {
                    return;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.crBizLogic.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存请稍候!");
                Application.DoEvents();
                foreach (FS.HISFC.Models.Fee.Item.Undrug updateInfo in updateList)
                {
                    if (this.crBizLogic.UpdateDeptCheckRegister(updateInfo) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新辅助检查信息失败!");
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("更新辅助检查信息成功!");
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            this.isCancel = false;
            this.Query();
        }

        private int VaildExeOper(FS.HISFC.Models.Fee.Item.Undrug unInfo, int row)
        {
            if (unInfo.OperationInfo.ID == "")
            {
                MessageBox.Show("请选择"+row.ToString()+"行执行者一!");
                return -1;
            }
            return 0;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl2.SelectedIndex == 0)
            {
                this.tabControl1.SelectedIndex = 0;
            }
            else if (this.tabControl2.SelectedIndex == 1)
            {
                this.tabControl1.SelectedIndex = 1;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.tabControl2.SelectedIndex = 0;
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                this.tabControl2.SelectedIndex = 1;
            }
        }

        private void DeptTreeReady_DoubleClick(object sender, EventArgs e)
        {
            if (this.DeptTreeReady.SelectedNode.Tag != null)
            {
                FS.FrameWork.Models.NeuObject obj = this.DeptTreeReady.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                List<FS.HISFC.Models.Fee.Item.Undrug> detailList = new List<FS.HISFC.Models.Fee.Item.Undrug>();
                detailList = this.crBizLogic.QueryReadyCheckRegister(this.dtBegin.Value, this.dtEnd.Value, obj.ID);
                if (detailList == null)
                {
                    return;
                }
                this.fpSpread2_Sheet1.RowCount = 0;
                foreach (FS.HISFC.Models.Fee.Item.Undrug info in detailList)
                {
                    this.fpSpread2_Sheet1.Rows.Add(this.fpSpread2_Sheet1.RowCount, 1);
                    int row = this.fpSpread2_Sheet1.RowCount - 1;
                    this.fpSpread2_Sheet1.Cells[row, 0].Text = info.CheckApplyDept;//4患者姓名
                    this.fpSpread2_Sheet1.Cells[row, 1].Text = info.CheckBody;//床号
                    if (row - 1 >= 0)//同一个患者清空赋值
                    {
                        if (this.fpSpread2_Sheet1.Cells[row - 1, 0].Text == info.CheckApplyDept)
                        {
                            this.fpSpread2_Sheet1.Cells[row, 0].Text = "";//4患者姓名
                            this.fpSpread2_Sheet1.Cells[row, 1].Text = "";
                        }
                    }
                    this.fpSpread2_Sheet1.Cells[row, 2].Text = "false";
                    this.fpSpread2_Sheet1.Cells[row, 3].Text = info.DiseaseType.Name;//项目名称
                    this.fpSpread2_Sheet1.Cells[row, 4].Text = info.Oper.OperTime.ToString();//执行日期 
                    this.fpSpread2_Sheet1.Cells[row, 5].Text = nurseHelper.GetName(info.OperationInfo.ID);//执行者一 
                    this.fpSpread2_Sheet1.Cells[row, 6].Text = nurseHelper.GetName(info.OperationInfo.Name);//执行者二 
                    this.fpSpread2_Sheet1.Cells[row, 7].Text = info.OperationInfo.Memo;//完成日期
                    this.fpSpread2_Sheet1.Cells[row, 8].Text = sendTypeHelper.GetName(info.OperationScale.Memo);//送检查方式
                    this.fpSpread2_Sheet1.Cells[row,9].Text =(info.ValidState=="1"?"有效":"无效");
                    if(info.ValidState=="0")
                    {
                        this.fpSpread2_Sheet1.Rows[row].BackColor = Color.Red;
                    }
                    this.fpSpread2_Sheet1.Rows[row].Tag = info;
                }
            }
        }

       
       
    }
}
