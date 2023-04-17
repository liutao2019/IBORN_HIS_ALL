using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmImplentSchedule : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmImplentSchedule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 申请信息
        /// </summary>
        private FS.HISFC.Models.Speciment.ApplyTable appTab = new FS.HISFC.Models.Speciment.ApplyTable();
        /// <summary>
        /// 申请信息
        /// </summary>
        public FS.HISFC.Models.Speciment.ApplyTable AppTab
        {
            set
            {
                this.appTab = value;
            }
            get
            {
                return this.appTab;
            }
        }

        /// <summary>
        /// 人员列表
        /// </summary>
        private ArrayList alEmpl = new ArrayList();

        /// <summary>
        /// 当前科室人员列表
        /// </summary>
        private ArrayList alCurDeptEmpl = new ArrayList();

        /// <summary>
        /// 当前操作员
        /// </summary>
        private FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();

        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbCurDeptEmpl = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();

        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbEmpl = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private void frmImplentSchedule_Load(object sender, EventArgs e)
        {
            ArrayList alLj = new ArrayList();
            this.loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            alLj = this.managerIntegrate.GetConstantList("SpecCommit");

            this.alEmpl = this.managerIntegrate.QueryEmployeeAll();
            this.alCurDeptEmpl = this.managerIntegrate.QueryEmployeeByDeptID(loginPerson.Dept.ID);
            if (this.alCurDeptEmpl != null)
            {
                if (this.alCurDeptEmpl.Count > 0)
                {
                    lbCurDeptEmpl.AddItems(this.alCurDeptEmpl);
                }
            }
            if (this.alEmpl != null)
            {
                if (this.alEmpl.Count > 0)
                {
                    lbEmpl.AddItems(this.alEmpl);
                }
            }

            this.neuGroupBox1.Text = "审批进度填写 申请单号[" + this.AppTab.ApplyId.ToString() + "]";
            FS.HISFC.BizLogic.Speciment.SpecOutManage outMgr = new FS.HISFC.BizLogic.Speciment.SpecOutManage();
            
            if ((alLj != null) && (alLj.Count > 0))
            {
                try
                {
                    this.neuSpread1_Sheet1.RowCount = alLj.Count;
                    if (this.AppTab.ImpResult == "2")
                    {
                        this.neuSpread1.Enabled = false;
                    }
                    int i = 0;
                    foreach (FS.HISFC.Models.Base.Const c in alLj)
                    {
                        //this.neuSpread1_Sheet1.Cells[i, 0].Text = "不通过";
                        string sql = @"select d.EMPL_NAME ENAME,b.CURDATE  CURDATE from SPEC_USERAPPLICATION b,COM_EMPLOYEE d 
                           where b.USERID = d.EMPL_CODE and b.APPLICATIONID = {0} and 
                           b.SCHEDULEID = '{1}'";
                        sql = string.Format(sql, this.AppTab.ApplyId.ToString(), c.ID.ToString());
                        DataSet ds = new DataSet();
                        outMgr.ExecQuery(sql, ref ds);

                        if (ds == null || ds.Tables.Count <= 0)
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Text = "未审批";
                            this.neuSpread1_Sheet1.Cells[i, 3].Value = System.DateTime.Now;
                            this.neuSpread1_Sheet1.Cells[i, 2].Text = "";
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataRow spDr = ds.Tables[0].Rows[0];
                                string oper = spDr["ENAME"] == null ? "" : spDr["ENAME"].ToString();
                                string operDate = spDr["CURDATE"] == null ? "" : spDr["CURDATE"].ToString();
                                if (!string.IsNullOrEmpty(oper))
                                {
                                    this.neuSpread1_Sheet1.Cells[i, 0].Text = "通过";
                                    this.neuSpread1_Sheet1.Cells[i, 2].Text = oper;
                                    this.neuSpread1_Sheet1.Cells[i, 3].Value = operDate;
                                }
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells[i, 0].Text = "未审批";
                                this.neuSpread1_Sheet1.Cells[i, 3].Value = System.DateTime.Now;
                                this.neuSpread1_Sheet1.Cells[i, 2].Text = "";
                            }
                        }
                        
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = c.Name;
                        this.neuSpread1_Sheet1.Cells[i, 1].Tag = c.ID.ToString();
                        this.neuSpread1_Sheet1.Rows[i].Tag = c;
                        i++;
                    }
                    this.neuSpread1_Sheet1.Columns[2].BackColor = Color.Pink;
                }
                catch
                {
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                FS.HISFC.BizLogic.Speciment.ApplyTableManage applyTableManage = new FS.HISFC.BizLogic.Speciment.ApplyTableManage();
                FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int result = -1;
                for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count - 1; j++)
                {
                    if (this.neuSpread1_Sheet1.Cells[j, 0].Text == "未审批")
                    {
                        //这边是按审批进度，肯定是第一个
                        if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[j, 2].Text.Trim()))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("不能越进度审批，请输入当前进度的审批人后保存！");
                            return;
                        }
                        else
                        {
                            #region 保存进度
                            try
                            {
                                FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                                string sequence = "";
                                userApply.ApplyId = this.AppTab.ApplyId;
                                userApply.OperId = loginPerson.ID;
                                userApply.OperName = loginPerson.Name;
                                userApply.UserId = this.neuSpread1_Sheet1.Cells[j, 2].Tag.ToString();
                                
                                //根据参数设置找到那个是最终审核，把申请记录置为审批通过状态
                                if ((FS.HISFC.Models.Base.Const)this.neuSpread1_Sheet1.Rows[j].Tag != null)
                                {
                                    FS.HISFC.Models.Base.Const cnt = (FS.HISFC.Models.Base.Const)this.neuSpread1_Sheet1.Rows[j].Tag;
                                    
                                    switch (cnt.ID.ToString())
                                    {
                                        case "O1": //出库员审批
                                            this.AppTab.ImpProcess = "O1";
                                            this.AppTab.AcceptConfirm = this.neuSpread1_Sheet1.Cells[j, 2].Text.Trim();
                                            this.AppTab.AcceptConfrimDate = Convert.ToDateTime(this.neuSpread1_Sheet1.Cells[j, 3].Text.Trim());
                                            //在此处插入终筛进度，并提示科室打印申请单
                                            userApply.CurDate = Convert.ToDateTime(this.neuSpread1_Sheet1.Cells[j, 3].Text.Trim());
                                            userApply.Schedule = "终筛，打印申请单";
                                            userApply.ScheduleId = "Q9";
                                            userApplyManage.GetNextSequence(ref sequence);
                                            userApply.UserAppId = Convert.ToInt32(sequence);
                                            result = userApplyManage.InsertUserApply(userApply);
                                            if (result < 0)
                                            {
                                                FS.FrameWork.Management.PublicTrans.RollBack();
                                                MessageBox.Show("插入申请进度记录失败！");
                                                return;
                                            }
                                            break;
                                        case "O2": //使用科室主任审批
                                            this.AppTab.ImpProcess = "O2";
                                            this.AppTab.DeptFromComm = this.neuSpread1_Sheet1.Cells[j, 2].Text.Trim();
                                            this.AppTab.DeptFromDate = Convert.ToDateTime(this.neuSpread1_Sheet1.Cells[j, 3].Text.Trim());
                                            break;
                                        case "O3": //标本库负责人审批
                                            this.AppTab.ImpProcess = "O3";
                                            this.AppTab.SpecAdmComment = this.neuSpread1_Sheet1.Cells[j, 2].Text.Trim();
                                            this.AppTab.SepcAdmDate = Convert.ToDateTime(this.neuSpread1_Sheet1.Cells[j, 3].Text.Trim());
                                            break;
                                        case "O4": //学术委员会审批(待定)
                                            this.AppTab.ImpProcess = "O4";
                                            this.AppTab.AcceptConfirm = this.neuSpread1_Sheet1.Cells[j, 2].Text.Trim();
                                            this.AppTab.AcceptConfrimDate = Convert.ToDateTime(this.neuSpread1_Sheet1.Cells[j, 3].Text.Trim());
                                            break;
                                        default:
                                            break;
                                    }
                                    if (cnt.UserCode.ToString() == "终审")
                                    {
                                        this.AppTab.ImpProcess = "O";
                                        this.AppTab.ImpResult = "1";
                                    }

                                }
                                if (applyTableManage.UpdateApplyTable(this.AppTab) <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新申请记录失败！");
                                    return;
                                }
                                
                                userApply.CurDate = System.DateTime.Now;
                                userApply.Schedule = this.neuSpread1_Sheet1.Cells[j, 1].Text;
                                userApply.ScheduleId = this.neuSpread1_Sheet1.Cells[j, 1].Tag.ToString();
                                userApplyManage.GetNextSequence(ref sequence);
                                userApply.UserAppId = Convert.ToInt32(sequence);
                                result = userApplyManage.InsertUserApply(userApply);
                                break;
                            }
                            catch
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("插入进度表失败!","异常错误");
                                return;
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入进度表失败!");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("保存审批成功！");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //双击审核人弹出窗口
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 2)
            {
                this.PopSpeItem(e.Row);
            }
        }

        /// <summary>
        /// 项目选择框
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public void PopSpeItem(int iIndex)
        {
            if ((iIndex == 0) || (iIndex == 2))
            {
                if ((this.alCurDeptEmpl != null) && (this.alCurDeptEmpl.Count > 0))
                {
                    string[] label = { "代码", "名称" };
                    float[] width = { 80F, 100F };
                    bool[] visible = { true, true };
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCurDeptEmpl, ref obj) == 1)
                    {
                        this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = obj.Name;
                        this.neuSpread1_Sheet1.Cells[iIndex, 2].Tag = obj.ID;
                    }
                }
            }
            else
            {
                if ((this.alEmpl != null) && (this.alEmpl.Count > 0))
                {
                    string[] label = { "代码", "名称" };
                    float[] width = { 80F, 100F };
                    bool[] visible = { true, true };
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alEmpl, ref obj) == 1)
                    {
                        this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = obj.Name;
                        this.neuSpread1_Sheet1.Cells[iIndex, 2].Tag = obj.ID;
                    }
                }
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("是否作废申请单号为：[" + this.appTab.ApplyId.ToString() + "]的申请单？作废后无法进行其他审批出库操作。","提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    FS.HISFC.BizLogic.Speciment.ApplyTableManage applyTableManage = new FS.HISFC.BizLogic.Speciment.ApplyTableManage();
                    FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();
                    FS.HISFC.Models.Base.Employee loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                    string sequence = "";
                    int result = -1;
                    userApply.ApplyId = this.AppTab.ApplyId;
                    userApply.UserId = loginPerson.ID;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;
                    userApply.CurDate = System.DateTime.Now;
                    userApply.Schedule = "审批拒绝";
                    userApply.ScheduleId = "OB";
                    userApplyManage.GetNextSequence(ref sequence);
                    userApply.UserAppId = Convert.ToInt32(sequence);
                    result = userApplyManage.InsertUserApply(userApply);
                    if (result < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入申请进度记录失败！");
                        return;
                    }
                    this.AppTab.ImpProcess = "O";
                    this.AppTab.ImpResult = "2";//拒绝
                    if (applyTableManage.UpdateApplyTable(this.AppTab) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新申请记录失败！");
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if (this.neuSpread1_Sheet1.ActiveColumnIndex == 2)
            //{
            //    if ((e.Row == 0) || (e.Row == 2))
            //    {
            //        this.neuSpread1.AddListBoxPopup(lbCurDeptEmpl, 2);
            //    }
            //    else
            //    {
            //        this.neuSpread1.AddListBoxPopup(lbEmpl, 2);
            //    }
            //}
        }

        private int SelectEmpl(FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbEmp)
        {
            int CurrentRow = neuSpread1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0)
                return 0;

            neuSpread1.StopCellEditing();
            FS.FrameWork.Models.NeuObject item = lbEmp.GetSelectedItem();

            if (item == null)
                return -1;

            neuSpread1_Sheet1.Cells[CurrentRow, 2].Tag = item;

            neuSpread1_Sheet1.SetValue(CurrentRow, 2, item.Name, false);
            neuSpread1_Sheet1.Cells[CurrentRow, 2].Text = item.Name;

            lbEmp.Visible = false;

            return 0;
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (neuSpread1.ContainsFocus)
                {
                    if (neuSpread1_Sheet1.ActiveColumnIndex == 2)
                    {
                        if ((neuSpread1_Sheet1.ActiveRowIndex == 0) || (neuSpread1_Sheet1.ActiveRowIndex == 2))
                        {
                            if (lbCurDeptEmpl.Visible)
                            {
                                SelectEmpl(lbCurDeptEmpl);
                                lbCurDeptEmpl.Visible = false;
                            }
                        }
                        else
                        {
                            if (lbEmpl.Visible)
                            {
                                SelectEmpl(lbEmpl);
                                lbEmpl.Visible = false;
                            }
                        }


                        neuSpread1_Sheet1.SetActiveCell(neuSpread1_Sheet1.ActiveRowIndex, 2, false);
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (neuSpread1.ContainsFocus)
                {
                    if (lbEmpl.Visible)
                    {
                        lbEmpl.PriorRow();
                    }
                    else if (lbCurDeptEmpl.Visible)
                    {
                        lbCurDeptEmpl.PriorRow();
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (neuSpread1.ContainsFocus)
                {
                    if (lbEmpl.Visible)
                    {
                        lbEmpl.NextRow();
                    }
                    else if (lbCurDeptEmpl.Visible)
                    {
                        lbCurDeptEmpl.NextRow();
                    }
                }
            }
        }
    }
}