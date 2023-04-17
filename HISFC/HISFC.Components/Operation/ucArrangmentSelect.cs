using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// 查询申请单信息
    /// </summary>
    public partial class ucArrangmentSelect : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucArrangmentSelect()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 登录人员信息
        /// </summary>
        FS.HISFC.Models.Base.Employee employee = null;
        /// <summary>
        /// 是否手术科室
        /// </summary>
        private bool isOpsDept = false;

        /// <summary>
        /// 科室类帮助
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室类帮助
        /// </summary>
        FS.FrameWork.Public.ObjectHelper emplHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 是否手术室属性设置
        /// </summary>
        [Category("科室属性设置"),Description("true 手术室查询全部数据 false 非手术室查询本科室数据")]
        public bool IsOpsDept
        {
            get { return this.isOpsDept; }
            set { this.isOpsDept = value; }
        }
        private void ucArrangmentSelect_Load(object sender, EventArgs e)
        {
            employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            this.Init();
        }
        private void Init()
        {
            emplHelper.ArrayObject=Environment.IntegrateManager.QueryEmployeeAll();

            ArrayList deptList = Environment.IntegrateManager.GetDepartment();
            deptHelper.ArrayObject = deptList;
            this.cmbApplyDept.AddItems(deptList);
            if (this.isOpsDept == false)
            {
                this.lblDeptName.Text = "科室";
                this.cmbApplyDept.Enabled = false;
                this.cmbApplyDept.Tag = employee.Dept.ID;
            }
            else
            {
                this.lblDeptName.Text = "手术室";
                this.cmbApplyDept.Enabled = true;
                this.cmbApplyDept.Tag = employee.Dept.ID;
            }

            //申请单状态 ALL 全部 1申请 2已审核 3已安排  
            ArrayList execStateList = Environment.IntegrateManager.GetConstantList("OPSEXECSTATUS");

            this.cmbExecStatus.AddItems(execStateList);
            if (this.isOpsDept)
            {
                this.cmbExecStatus.Tag = "3";
            }
            else
            {
                this.cmbExecStatus.Tag = "ALL";
            }
            this.dtBegin.Value = Environment.OperationManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            this.dtEnd.Value = this.dtBegin.Value;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cmbApplyDept.Tag == null)
            {
                if (this.isOpsDept)
                {
                    this.cmbExecStatus.Tag = "3";
                    MessageBox.Show("请选择科室！", "提示");
                }
                else
                {
                    MessageBox.Show("请重新登录系统并选择正确的登录科室！", "提示");
                }
                this.cmbApplyDept.Focus();
                return -1;
            }
            //如果未选择状态默认所有
            if (this.cmbExecStatus.Tag == null)
            {
                this.cmbExecStatus.Tag = "ALL";
            }

            ArrayList applyList = new ArrayList();
            if (this.isOpsDept)//手术室：执行科室 和 手术日期查询有效申请单
            {
                applyList = Environment.OperationManager.GetOpsAppList(this.cmbApplyDept.Tag.ToString(), this.dtBegin.Value.Date, this.dtEnd.Value.Date.AddDays(1), "1");
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.cmbApplyDept.Tag.ToString();

                applyList = Environment.OperationManager.GetOpsAppList(obj, this.dtBegin.Value.Date, this.dtEnd.Value.Date.AddDays(1));
            }
            this.neuFpEnter1_Sheet1.RowCount = 0;

            foreach (FS.HISFC.Models.Operation.OperationAppllication applyInfo in applyList)
            {
                if (this.cmbExecStatus.Tag.ToString() == "ALL")
                {
                    if (this.isOpsDept)
                    {
                        if (applyInfo.IsValid)
                        {
                            this.SetFarPoint(applyInfo);
                        }
                    }
                    else
                    {
                        this.SetFarPoint(applyInfo);
                    }
                }
                else if (this.cmbExecStatus.Tag.ToString() == "1")//申请
                {
                    if (applyInfo.ExecStatus == "1")
                    {
                        this.SetFarPoint(applyInfo);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (this.cmbExecStatus.Tag.ToString() == "2")
                {
                    if (applyInfo.ExecStatus == "2")
                    {
                        this.SetFarPoint(applyInfo);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (this.cmbExecStatus.Tag.ToString() == "3")
                {
                    if (applyInfo.ExecStatus == "3")
                    {
                        this.SetFarPoint(applyInfo);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 赋值到fapiont界面
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private void SetFarPoint(FS.HISFC.Models.Operation.OperationAppllication info)
        {
            this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.RowCount, 1);
            int row = this.neuFpEnter1_Sheet1.RowCount - 1;
            this.neuFpEnter1_Sheet1.Cells[row, 0].Text = deptHelper.GetName(info.ApplyDoctor.Dept.ID);//申请科室
            this.neuFpEnter1_Sheet1.Cells[row, 1].Text = info.PatientInfo.PVisit.PatientLocation.Bed.ID;//床号
            this.neuFpEnter1_Sheet1.Cells[row, 2].Text = info.PatientInfo.Name;//患者姓名
            if (info.PatientInfo.Sex.ID.ToString() == "M")
            {
                this.neuFpEnter1_Sheet1.Cells[row, 3].Text = "男";//性别
            }
            else
            {
                this.neuFpEnter1_Sheet1.Cells[row, 3].Text = "女";//性别
            }

            this.neuFpEnter1_Sheet1.Cells[row, 4].Text = Environment.OperationManager.GetAge(info.PatientInfo.Birthday);//年龄
            this.neuFpEnter1_Sheet1.Cells[row, 5].Text = info.PreDate.ToString();//手术时间
            bool isFirst = true;
            if (info.DiagnoseAl != null && info.DiagnoseAl.Count > 0)
            {

                foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in info.DiagnoseAl)
                {
                    if (isFirst)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 6].Text = diag.Name;
                        isFirst = false;
                    }
                    else
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 6].Text += "," + diag.Name;
                    }
                }
            }
            isFirst = true;
            if (info.OperationInfos != null && info.OperationInfos.Count > 0)
            {
                foreach (FS.HISFC.Models.Operation.OperationInfo operInfo in info.OperationInfos)
                {
                    if (isFirst)
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text = operInfo.OperationItem.Name;
                        isFirst = false;
                    }
                    else
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 7].Text += "," + operInfo.OperationItem.Name;
                    }
                }
            }
            this.neuFpEnter1_Sheet1.Cells[row, 8].Text = emplHelper.GetName(info.OperationDoctor.ID);
            if (info.RoleAl != null && info.RoleAl.Count > 0)
            {
                foreach (FS.HISFC.Models.Operation.ArrangeRole roleInfo in info.RoleAl)
                {
                    if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 9].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 10].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Anaesthetist.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 11].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 12].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse1.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 16].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse2.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 16].Text +=","+ roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse1.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 17].Text = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse2.ToString())
                    {
                        this.neuFpEnter1_Sheet1.Cells[row, 17].Text +=","+ roleInfo.Name;
                    }
                }
            }
            if (info.AnesType.ID != null && info.AnesType.ID !="")
            {
                try
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 13].Text = Environment.IntegrateManager.GetConstant("ANESTYPE", info.AnesType.ID).Name;
                }
                catch
                {
                }
            }
            if (info.RoomID != null && info.RoomID != "")
            {
                try
                {
                    this.neuFpEnter1_Sheet1.Cells[row, 14].Text = Environment.TableManager.GetRoomByID(info.RoomID).Name;
                }
                catch
                {
                }
            }
            this.neuFpEnter1_Sheet1.Cells[row, 15].Text = info.OpsTable.Name;
            if (info.IsValid == false)
            {
                this.neuFpEnter1_Sheet1.Rows[row].BackColor = System.Drawing.Color.Red;
            }
            else if (info.ExecStatus == "2")
            {
                this.neuFpEnter1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightGreen;
            }
            else if (info.ExecStatus == "3")
            {
                this.neuFpEnter1_Sheet1.Rows[row].BackColor = System.Drawing.Color.LightBlue;
            }

        }    
    }
}
