using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class frmNewApply : Form
    {
        public frmNewApply()
        {
            InitializeComponent();
        }
        private UserApplyManage userApplyManage = new UserApplyManage();
        private ApplyTableManage applyTableManage = new ApplyTableManage();
        private FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private ApplyTable applyTable;
        private UserApply userApply;
        /// <summary>
        /// 结果返回1成功生成ID，-1失败
        /// </summary>
        private int rtResult = -1;
        public int RtResult
        {
            get
            {
                return this.rtResult;
            }
            set
            {
                this.rtResult = value;
            }
        }
        /// <summary>
        /// 申请号
        /// </summary>
        private string applyId = string.Empty;
        public string ApplyId
        {
            get
            {
                return this.applyId;
            }
            set
            {
                this.applyId = value;
            }
        }

        private void frmNewApply_Load(object sender, EventArgs e)
        {
            ArrayList alDept = new ArrayList();
            alDept = this.dpMgr.GetDepartment();
            if ((alDept != null) && (alDept.Count > 0))
            {
                this.cmbDept.AddItems(alDept);
            }
            ArrayList alEmpl = new ArrayList();
            alEmpl = this.dpMgr.QueryEmployeeAll();
            if ((alEmpl != null) && (alEmpl.Count > 0))
            {
                this.cmbOper.AddItems(alEmpl);
                this.cmbImpName.AddItems(alEmpl);
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Employee loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                applyTableManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                int result = 0;
                applyTable = new ApplyTable();
                userApply = new UserApply();

                if (string.IsNullOrEmpty(applyTable.ApplyUserId))
                {
                    applyTable.ApplyId = applyTableManage.GetMaxApplyId();
                    //初始值
                    if (applyTable.ApplyId < 0)
                    {
                        applyTable.ApplyId = 1;
                    }
                    if ((cmbOper.Tag != null) && (cmbOper.Text != ""))
                    {
                        applyTable.ApplyUserId = cmbOper.Tag.ToString();
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("申请人不能为空!", "生成申请ID号");
                        return;
                    }
                    if (cmbOper.Text != "")
                        applyTable.ApplyUserName = cmbOper.Text.TrimEnd().TrimStart();

                    if ((cmbDept.Tag != null) && (cmbDept.Text != ""))
                    {
                        applyTable.DeptId = cmbDept.Tag.ToString();
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("申请科室不能为空!", "生成申请ID号");
                        return;
                    }
                    if ((cmbImpName.Text != null) && (cmbImpName.Text != ""))
                    {
                        applyTable.ImpName = cmbImpName.Text.TrimEnd().TrimStart();
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("执行人不能为空！", "生成申请ID号");
                        return;
                    }
                    if (cmbDept.Text != "")
                        applyTable.DeptName = cmbDept.Text;
                    if (applyTable.ApplyId <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.RtResult = -1;
                        MessageBox.Show("申请编号不能为空!", "生成申请ID号");
                        return;
                    }
                    result = applyTableManage.InsertApplyTable(applyTable);
                    userApply.ApplyId = applyTable.ApplyId;
                    userApply.UserId = applyTable.ApplyUserId;
                    userApply.Schedule = "生成申请ID号";
                    userApply.ScheduleId = "Q1";
                    userApply.CurDate = this.dtpAppDate.Value;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;

                    string sequence = "";
                    userApplyManage.GetNextSequence(ref sequence);
                    userApply.UserAppId = Convert.ToInt32(sequence);
                    result = userApplyManage.InsertUserApply(userApply);
                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.RtResult = -1;
                        MessageBox.Show("生成失败!", "生成申请ID号");
                        return;
                    }
                }
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.RtResult = -1;
                    MessageBox.Show("生成失败!", "生成申请ID号");
                    return;
                }
                this.ApplyId = applyTable.ApplyId.ToString();
                FS.FrameWork.Management.PublicTrans.Commit();
                this.RtResult = 1;
                MessageBox.Show("成功生成申请ID号!", "生成申请ID号");
                this.Close();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.RtResult = -1;
                MessageBox.Show("异常错误" + ex.ToString());
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbOper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                cmbDept.Focus();
            }
        }

        private void cmbDept_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                cmbImpName.Focus();
            }
        }

        private void cmbImpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                dtpAppDate.Focus();
            }
        }

        private void dtpAppDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnCancel.Focus();
        }

        //private void frmNewApply_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e == Keys.Enter)
        //    {
        //        SendKeys.Send("{TAB}");
        //    }
        //}
    }
}