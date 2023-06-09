using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYZL.Base.Inpatient
{
    public partial class frmQueryPatientInfo : Neusoft.FrameWork.WinForms.Forms.BaseForm, Neusoft.SOC.HISFC.RADT.Interface.Patient.IQuery
    {
        public frmQueryPatientInfo()
        {
            InitializeComponent();
            this.tcMain.SelectedTab = this.tbpageName;
        }

        #region 变量

        /// <summary>
        /// 患者信息
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 新系统查询条件
        /// </summary>
        string sqlWhereNew = string.Empty;

        /// <summary>
        /// 旧系统查询条件
        /// </summary>
        string sqlWhereOld = string.Empty;

        /// <summary>
        /// 是否选中患者
        /// </summary>
        public bool isSelect = false;

        /// <summary>
        /// 是否新系统患者
        /// </summary>
        public bool isNewPerson = true;

        /// <summary>
        /// 当前选择的患者类别
        /// </summary>
        private enumSelectPatient selectPatient = enumSelectPatient.NewPatient; 

        #endregion

        #region 方法

        private int Init()
        {
            return 1;
        }

        /// <summary>
        /// 获得多个查询条件
        /// </summary>
        /// <returns></returns>
        private int getSqlWhere()
        {
            //不检索无费退院的信息
            sqlWhereNew = "where 1=1 and in_state <> 'N' ";
            sqlWhereOld = "where 1=1 ";

            try
            {
                #region 选择姓名和性别Tab页

                if (this.tcMain.SelectedTab == this.tbpageName)
                {
                    #region 姓名
                    if (this.rbtLike.Checked)
                    {
                        sqlWhereNew += " and name like '%" + this.txtName.Text + "%'";
                        sqlWhereOld += "and name like  '%" + this.txtName.Text + "%'";
                    }
                    else
                    {
                        sqlWhereNew += " and name = '" + this.txtName.Text + "'";
                        sqlWhereOld += "and name =  '" + this.txtName.Text + "'";
                    }

                    #endregion

                    #region 性别

                    if (this.rbSexM.Checked == true)
                    {
                        sqlWhereNew += " and sex_code = 'M'";
                        sqlWhereOld += " and sex_code = 'M'";
                    }
                    else if (this.rbSexF.Checked == true)
                    {
                        sqlWhereNew += " and sex_code = 'F'";
                        sqlWhereOld += " and sex_code = 'F'";
                    }
                    else if (this.rbDimness.Checked == true)
                    {
                        sqlWhereNew += " and sex_code like '%%'";
                        sqlWhereOld += " and sex_code like '%%'";
                    }

                    #endregion
                }
                #endregion

                #region 选择住院号Tab页

                else if (this.tcMain.SelectedTab == this.tbpagePatientNo)
                {
                    if (string.IsNullOrEmpty(this.txtPatientNo.Text))
                    {
                        MessageBox.Show("未输入住院号!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    if (this.rbtLike.Checked)
                    {
                        sqlWhereNew += " and patient_no like '%" + this.txtPatientNo.Text.PadLeft(10, '0') + "%'";
                        sqlWhereOld += " and card_no like '%" + this.txtPatientNo.Text.Trim() + "%'";
                    }
                    else
                    {
                        sqlWhereNew += " and patient_no = '" + this.txtPatientNo.Text.PadLeft(10, '0') + "'";
                        sqlWhereOld += " and card_no  ='" + this.txtPatientNo.Text.Trim() + "'";
                    }

                    sqlWhereNew += "and INPATIENT_NO Not LIKE '%B%' ";
                    sqlWhereOld += "and card_no Not LIKE '%*%' ";
                }
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void clear()
        {
            //this.tcMain.SelectedTab = this.tbpageName;
            this.isSelect = false;
            this.isNewPerson = true;
            this.cbxNewSys.Checked = true;
            this.cbxOldSys.Checked = true;
            this.rbtSame.Checked = true;

            this.lblNewPatient.Text = "新系统读取:0";
            this.lblOldPatient.Text = "旧系统读取:0";
            this.sheetNewPatient.RowCount = 0;
            this.sheetOldPatient.RowCount = 0;
        }

        /// <summary>
        /// 查询新系统患者信息
        /// </summary>
        /// <returns></returns>
        private int queryNewPatient()
        {
            ArrayList alNewPatient = Function.QueryPatientInfo(this.sqlWhereNew);
            if (alNewPatient != null && alNewPatient.Count > 0)
            {
                this.setValues(alNewPatient, this.sheetNewPatient);
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.sheetNewPatient.AddSelection(0, 0, 1, 1);
                this.selectPatient = enumSelectPatient.NewPatient;
                this.lblNewPatient.Text = "新系统读取:" + alNewPatient.Count.ToString();
            }
            return 1;
        }

        /// <summary>
        /// 查询旧系统患者信息
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private int queryOldPatient()
        {
            ArrayList alOldPatient = Function.QueryOldSysPatientInfo(sqlWhereOld);

            if (alOldPatient != null && alOldPatient.Count > 0)
            {
                this.setValues(alOldPatient, this.sheetOldPatient);

                this.sheetOldPatient.ActiveRowIndex = 0;
                this.sheetOldPatient.AddSelection(0, 0, 1, 1);
                this.selectPatient = enumSelectPatient.OldPatient;
                this.lblOldPatient.Text = "旧系统读取:" + alOldPatient.Count.ToString();
            }

            return 1;
        }

        /// <summary>
        /// 患者列表赋值
        /// </summary>
        /// <param name="alPatient"></param>
        /// <param name="sheetView"></param>
        private void setValues(ArrayList alPatient, FarPoint.Win.Spread.SheetView sheetView)
        {
            try
            {
                foreach (Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo in alPatient)
                {
                    sheetView.Rows.Add(sheetView.RowCount, 1);
                    int row = sheetView.RowCount - 1;
                    sheetView.SetValue(row, (int)enuInfoColumn.Name, PatientInfo.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.Sex, PatientInfo.Sex.ID.ToString() == "M" ? "男" : "女");
                    sheetView.SetValue(row, (int)enuInfoColumn.InpatientNo, PatientInfo.PID.PatientNO.TrimStart('0'));
                    sheetView.SetValue(row, (int)enuInfoColumn.DeptName, PatientInfo.PVisit.PatientLocation.Dept.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthDate, PatientInfo.Birthday.ToString("yyyy-MM-dd"));
                    sheetView.SetValue(row, (int)enuInfoColumn.WorkName, PatientInfo.CompanyName);
                    sheetView.SetValue(row, (int)enuInfoColumn.BirthArea, PatientInfo.AreaCode);
                    sheetView.SetValue(row, (int)enuInfoColumn.HomeArea, PatientInfo.AddressHome);
                    sheetView.SetValue(row, (int)enuInfoColumn.InDate, PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.OutDate, PatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd hh:m"));
                    sheetView.SetValue(row, (int)enuInfoColumn.IdenNo, PatientInfo.IDCard);
                    sheetView.SetValue(row, (int)enuInfoColumn.LinkMan, PatientInfo.Kin.Name);
                    sheetView.SetValue(row, (int)enuInfoColumn.InTimes, PatientInfo.InTimes);
                    string strInState = "";
                    switch (PatientInfo.PVisit.InState.ID.ToString())
                    {
                        case "R":
                            strInState = "在院";
                            break;
                        case "I":
                            strInState = "在院";
                            break;
                        case "B":
                            strInState = "出院登记";
                            break;
                        case "O":
                            strInState = "清帐";
                            break;
                        case "P":
                            strInState = "预约出院";
                            break;
                        case "N":
                            strInState = "无费退院";
                            break;
                        case "C":
                            strInState = "取消";
                            break;
                    }
                    sheetView.SetValue(row, (int)enuInfoColumn.InState, strInState);

                    sheetView.Rows[row].Tag = PatientInfo;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// 按照类别查询患者基本信息
        /// </summary>
        /// <param name="queryType"></param>
        private int query(Neusoft.SOC.HISFC.RADT.Interface.Patient.EnumQueryType queryType)
        {
            this.clear();

            switch(queryType)
            {
                case Neusoft.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.PatientNo:
                    this.tcMain.SelectedTab = this.tbpagePatientNo;
                    break;
                case Neusoft.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex:
                    this.tcMain.SelectedTab = this.tbpageName;
                    break;
            }

            //获取组合查询条件
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            if (this.queryOldPatient() == -1)
            {
                return -1;
            }

            if (this.queryNewPatient() == -1)
            {
                return -1;
            }

            //如果没有查到患者就返回
            if (this.sheetNewPatient.RowCount <= 0 && this.sheetOldPatient.RowCount <= 0)
            {
                return -1;
            }
            else if ((this.sheetNewPatient.RowCount <= 0 && this.sheetOldPatient.RowCount > 0))
            {
                this.selectPatient = enumSelectPatient.OldPatient;
                this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
            }
            else
            {
                this.selectPatient = enumSelectPatient.NewPatient;
                this.sheetOldPatient.RemoveSelection(this.sheetOldPatient.ActiveRowIndex, 0, 1, 1);
            }

            this.btSelect.Focus();
            this.btSelect.Select();

            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private int query(TabPage page)
        {
            this.clear();

            //获取组合查询条件
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            if (this.queryOldPatient() == -1)
            {
                return -1;
            }

            if (this.queryNewPatient() == -1)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        #region 事件

        private void nbtSearch_Click(object sender, EventArgs e)
        {
            this.query(this.tcMain.SelectedTab);
        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelect_Click(object sender, EventArgs e)
        {
            //选中新系统患者
            if (this.selectPatient == enumSelectPatient.NewPatient && this.sheetNewPatient.Rows.Count > 0)
            {
                if (this.sheetNewPatient.Rows[this.sheetNewPatient.ActiveRowIndex].Tag == null)
                {
                    this.isSelect = false;
                    return;
                }
                this.isNewPerson = true;
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.PatientInfo = this.sheetNewPatient.Rows[this.sheetNewPatient.ActiveRowIndex].Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
            }
            //选中旧系统患者
            if (this.selectPatient == enumSelectPatient.OldPatient && this.sheetOldPatient.RowCount > 0)
            {
                if (this.sheetOldPatient.Rows[this.sheetOldPatient.ActiveRowIndex].Tag == null)
                {
                    this.isSelect = false;
                    return;
                }
                this.isNewPerson = false;
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.PatientInfo = this.sheetOldPatient.Rows[this.sheetOldPatient.ActiveRowIndex].Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                this.patientInfo.PID.PatientNO = this.patientInfo.PID.PatientNO.PadLeft(10, '0');
                this.patientInfo.PID.CardNO = Function.GetCardNOByPatientNO(string.Empty, this.patientInfo.PID.PatientNO);
            }

            //if (this.patientInfo != null && string.IsNullOrEmpty(this.patientInfo.ID))
            //{
            //    this.isSelect = false;
            //    return;
            //}

            frmPatientInfo frmPatientInfo = new frmPatientInfo();
            frmPatientInfo.Init(this.patientInfo);
            frmPatientInfo.ShowDialog(this);
            frmPatientInfo.BringToFront();
            if (frmPatientInfo.IsOK == "0")
            {
                return;
            }
            else if (frmPatientInfo.IsOK == "1")
            {
                this.isSelect = true;
                this.Close();

                return;
            }
            else if (frmPatientInfo.IsOK == "2")
            {
                this.isSelect = false;
                this.Close();
                return;
            }
        }

        private void btNoSelect_Click(object sender, EventArgs e)
        {
            this.isSelect = false;
            this.Close();
        }

        private void frmQueryPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.isSelect = false;
                this.Close();
            }
        }

        private void spOldPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            this.selectPatient = enumSelectPatient.OldPatient;
            this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
        }

        private void spOldPatientInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectPatient = enumSelectPatient.OldPatient;
            this.sheetNewPatient.RemoveSelection(this.sheetNewPatient.ActiveRowIndex, 0, 1, 1);
        }

        private void spNewPatientInfo_KeyDown(object sender, KeyEventArgs e)
        {
            this.selectPatient = enumSelectPatient.NewPatient;
            this.spOldPatientInfo.ActiveSheet.ActiveRowIndex = -1;
        }

        private void spNewPatientInfo_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectPatient = enumSelectPatient.NewPatient;
            this.sheetOldPatient.RemoveSelection(this.sheetOldPatient.ActiveRowIndex, 0, 1, 1);
        }

        #endregion

        #region IQueryPatientInfo 成员

        /// <summary>
        /// 当前患者信息
        /// </summary>
        public Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;

                if (patientInfo != null)
                {
                    //姓名
                    this.txtName.Text = patientInfo.Name;
                    //性别
                    switch (patientInfo.Sex.Name.Trim())
                    {
                        case "男":
                            this.rbSexM.Checked = true;
                            break;
                        case "女":
                            this.rbSexF.Checked = true;
                            break;
                        default:
                            this.rbDimness.Checked = true;
                            break;
                    }
                    //住院号
                    this.txtPatientNo.Text = patientInfo.PID.PatientNO;
                }
            }
        }

       public  bool IsSelect
        {
            get
            {
                return isSelect;
            }
        }

        public bool IsOldSystem
        {
            get
            {
                return !isNewPerson;
            }
        }

        public int Query(Neusoft.SOC.HISFC.RADT.Interface.Patient.EnumQueryType enumQueryType)
        {
            return this.query(enumQueryType);
        }

        public new void Show(IWin32Window owner)
        {
            this.ShowDialog(owner);
        }

        public void Clear()
        {
            this.clear();
        }

        #endregion
    }

    /// <summary>
    /// 选择的患者类别
    /// </summary>
    public enum enumSelectPatient
    {
        /// <summary>
        /// 新患者
        /// </summary>
        NewPatient,

        /// <summary>
        /// 旧患者
        /// </summary>
        OldPatient
    }

    /// <summary>
    /// 患者信息枚举列
    /// </summary>
    public enum enuInfoColumn
    {
        /// <summary>
        /// 姓名
        /// </summary>
        Name = 1,

        /// <summary>
        /// 性别
        /// </summary>
        Sex,

        /// <summary>
        /// 住院号
        /// </summary>
        InpatientNo,

        /// <summary>
        /// 出生日期
        /// </summary>
        BirthDate,

        /// <summary>
        /// 当前病区
        /// </summary>
        DeptName,

        /// <summary>
        /// 标志
        /// </summary>
        InState,

        /// <summary>
        /// 单位及地址
        /// </summary>
        WorkName,

        /// <summary>
        /// 出生地
        /// </summary>
        BirthArea,

        /// <summary>
        /// 家庭地址
        /// </summary>
        HomeArea,

        /// <summary>
        /// 入院日期
        /// </summary>
        InDate,

        /// <summary>
        /// 出院日期
        /// </summary>
        OutDate,

        /// <summary>
        /// 身份证号
        /// </summary>
        IdenNo,

        /// <summary>
        /// 联系人
        /// </summary>
        LinkMan,

        /// <summary>
        /// 住院次数
        /// </summary>
        InTimes
    }
}