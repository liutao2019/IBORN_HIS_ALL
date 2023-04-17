using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
{
    /// <summary>
    /// 创建人？？？
    /// 
    /// 修改人：FZC
    /// 修改内容：功能修复、添加若干注释
    /// 修改日期：2014-10-02 
    /// PS：UI上有两个没事件的按钮 不知道干啥的 先隐藏了
    /// </summary>
    public partial class frmQueryPatientInfoNew : FS.FrameWork.WinForms.Forms.BaseForm, FS.SOC.HISFC.RADT.Interface.Patient.IQuery
    {
        public frmQueryPatientInfoNew()
        {
            InitializeComponent();
            this.tcMain.SelectedTab = this.tbpageName;
        }

        #region 变量

        /// <summary>
        /// 选中的患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 传进来的患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfoCompare = new FS.HISFC.Models.RADT.PatientInfo();

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
        /// 是否过滤历史临时住院号的患者
        /// </summary>
        private bool isFiltTemPatient = true;
        /// <summary>
        /// 是否过滤历史临时住院号的患者
        /// </summary>
        public bool IsFiltTemPatient
        {
            get { return isFiltTemPatient; }
            set { isFiltTemPatient = value; }
        }

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
                        if (!string.IsNullOrEmpty(this.txtName.Text))
                        {
                            sqlWhereNew += " and name like '%" + this.txtName.Text + "%'";
                            sqlWhereOld += "and name like  '%" + this.txtName.Text + "%'";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtName.Text))
                        {
                            sqlWhereNew += " and name = '" + this.txtName.Text + "'";
                            sqlWhereOld += "and name =  '" + this.txtName.Text + "'";
                        }
                    }

                    #endregion
                    #region 身份证号// {249D5ADE-8BE6-4c90-A8D1-F99540D24A64}
                    if (this.rbtLike.Checked)
                    {
                        if (!string.IsNullOrEmpty(this.txtCardID.Text))
                        {
                            sqlWhereNew += " and idenno like '%" + this.txtCardID.Text + "%'";
                            sqlWhereOld += "and idenno like  '%" + this.txtCardID.Text + "%'";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.txtCardID.Text))
                        {
                            sqlWhereNew += " and idenno = '" + this.txtCardID.Text + "'";
                            sqlWhereOld += "and idenno =  '" + this.txtCardID.Text + "'";
                        }
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
            this.rbtSame.Checked = true;

            this.lblNewPatient.Text = "系统读取:0";
            this.sheetNewPatient.RowCount = 0;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        private int queryPatient()
        {
            ArrayList alNewPatient = Function.QueryPatientInfo(this.sqlWhereNew);
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //旧系统数据是从病案系统中查出来的每个患者【入院次数】最大的记录
            //即每个患者最近一次的入院记录住院号
            ArrayList alOldPatient = Function.QueryOldSysPatientInfo(sqlWhereOld);

            //相同的住院号只显示最新一次记录
            Hashtable htTempPatient = new Hashtable();
            ArrayList alPatient;

            //HIS系统患者
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alNewPatient)
            {
                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO,@"^\d+$"))
                    {
                        continue;
                    }
                }
                if (htTempPatient.ContainsKey(p.PID.PatientNO.TrimStart('0')))
                {
                    FS.HISFC.Models.RADT.PatientInfo temp = htTempPatient[p.PID.PatientNO.TrimStart('0')] as FS.HISFC.Models.RADT.PatientInfo;
                    if (temp != null && temp.InTimes < p.InTimes)
                    {
                        htTempPatient.Remove(p.PID.PatientNO.TrimStart('0'));
                        htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                    }
                }
                else
                {
                    htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                }
            }

            //病案系统中的患者信息
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alOldPatient)
            {
                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO, @"^\d+$"))
                    {
                        continue;
                    }
                }
                if (htTempPatient.ContainsKey(p.PID.PatientNO.TrimStart('0')))
                {
                    FS.HISFC.Models.RADT.PatientInfo temp = htTempPatient[p.PID.PatientNO.TrimStart('0')] as FS.HISFC.Models.RADT.PatientInfo;
                    if (temp != null && temp.InTimes < p.InTimes)
                    {
                        htTempPatient.Remove(p.PID.PatientNO.TrimStart('0'));
                        htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                    }
                }
                else
                {
                    htTempPatient.Add(p.PID.PatientNO.TrimStart('0'), p);
                }
            }

            alPatient = new ArrayList(htTempPatient.Values);
            if (alPatient != null && alPatient.Count > 0)
            {
                this.patientInfo = alPatient[0] as FS.HISFC.Models.RADT.PatientInfo;
                this.setValues(alPatient, this.sheetNewPatient);
                this.sheetNewPatient.ActiveRowIndex = 0;
                this.sheetNewPatient.AddSelection(0, 0, 1, 1);
                this.lblNewPatient.Text = "系统读取:" + alPatient.Count.ToString();
            }
            else if (alPatient != null && alPatient.Count == 0)
            {
                return 0;
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
            ArrayList newOldPatient = new ArrayList();
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alOldPatient)
            {

                if (this.isFiltTemPatient)
                {
                    if (!Regex.IsMatch(p.PID.PatientNO, @"^\d+$"))
                    {
                        continue;
                    }
                }

                newOldPatient.Add(p);
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
                foreach (FS.HISFC.Models.RADT.PatientInfo p in alPatient)
                {
                    sheetView.Rows.Add(sheetView.RowCount, 1);
                    int row = sheetView.RowCount - 1;
                    sheetView.SetValue(row, (int)enuInfoColumnNew.Name, p.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.Sex, p.Sex.ID.ToString() == "M" ? "男" : "女");
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InpatientNo, p.PID.PatientNO.TrimStart('0'));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.DeptName, p.PVisit.PatientLocation.Dept.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.BirthDate, p.Birthday.ToString("yyyy-MM-dd"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.WorkName, p.CompanyName);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.BirthArea, p.AreaCode);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.HomeArea, p.AddressHome);
                    //{70C44B29-EC2A-4bc2-BFFE-ED55DC9C7560}把时间更改为24小时制
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InDate, p.PVisit.InTime.ToString("yyyy-MM-dd HH:m"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.OutDate, p.PVisit.OutTime.ToString("yyyy-MM-dd HH:m"));
                    sheetView.SetValue(row, (int)enuInfoColumnNew.IdenNo, p.IDCard);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.HomePhone, p.PhoneHome);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.LinkMan, p.Kin.Name);
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InTimes, p.InTimes);
                    string strInState = "";
                    switch (p.PVisit.InState.ID.ToString())
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
                            strInState = "结算出院";
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
                    sheetView.SetValue(row, (int)enuInfoColumnNew.InState, strInState);

                    sheetView.Rows[row].Tag = p;
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
        private int query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType queryType)
        {
            this.clear();

           
            switch (queryType)
            {
                case FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.PatientNo:
                    this.tcMain.SelectedTab = this.tbpagePatientNo;
                    break;
                case FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex:
                    this.tcMain.SelectedTab = this.tbpageName;
                    break;
            }

            //获取组合查询条件
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            #region 原先函数处理存在问题，查询新旧患者合成一个函数
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //if (this.queryOldPatient() == -1)
            //{
            //    return -1;
            //}

            //int returnInt = -1;
            //returnInt = this.queryNewPatient();
            #endregion 

            int rtn = -1;
            rtn = this.queryPatient();

            this.btSelect.Focus();
            this.btSelect.Select();

            return rtn;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private int query(TabPage page)
        {
            //查询前清屏？？？？ 没用 注掉
            //this.clear();
            if (page.Text == "住院号")
            {
                this.isFiltTemPatient = false;
            }
            this.lblNewPatient.Text = "系统读取:0";
            this.sheetNewPatient.RowCount = 0;
            //获取组合查询条件
            if (this.getSqlWhere() == -1)
            {
                return -1;
            }

            #region 原先函数处理存在问题，查询新旧患者合成一个函数
            //{67F0DA44-EB32-4556-96C5-88B60C59E157}
            //if (this.queryOldPatient() == -1)
            //{
            //    return -1;
            //}

            //int returnInt = -1;
            //returnInt = this.queryNewPatient();
            #endregion 

            if (this.queryPatient() == -1)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 开始查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
            if (this.PatientInfo != null && !string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                FS.HISFC.Models.RADT.PatientInfo tmp = radtIntegrate.QueryComPatientInfo(this.PatientInfo.PID.CardNO);

                if (tmp != null && tmp.Memo == "作废患者")
                {
                    MessageBox.Show("该患者资料已经作废！");
                    return;
                }
            }

            if (this.PatientInfo != null && this.patientInfoCompare != null )
            {
                string errMsg = string.Empty;

                if (this.patientInfoCompare.Name != this.PatientInfo.Name)
                {
                    errMsg += "★ 您选择的住院记录与当前患者的【姓名】不相同!\r\n";
                }

                if (this.patientInfoCompare.Birthday != this.PatientInfo.Birthday)
                {
                    errMsg += "★ 您选择的住院记录与当前患者的【生日】不相同!\r\n";
                }

                if (string.IsNullOrEmpty(this.PatientInfo.IDCard))
                {
                    this.PatientInfo.IDCard = "";
                }

                if (string.IsNullOrEmpty(this.patientInfoCompare.IDCard))
                {
                    this.patientInfoCompare.IDCard = "";
                }

                if (this.patientInfoCompare.IDCard != this.PatientInfo.IDCard )
                {
                    errMsg += "★ 您选择的住院记录与当前患者的【身份证号码】不相同!\r\n";
                }

                if (this.patientInfoCompare.PhoneHome != this.PatientInfo.PhoneHome)
                {
                    errMsg += "★ 您选择的住院记录与当前患者的【电话号码】不相同!\r\n";
                }

                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsg += "\r\n请进行确认！\r\n";
                    errMsg += "\r\n确认选择请点击【确定】，重新选择请点击【取消】";

                    if (MessageBox.Show(errMsg, "警告", MessageBoxButtons.OKCancel,MessageBoxIcon.Information) != DialogResult.OK)
                    {
                        return;
                    }
                }

            }

            this.isSelect = true;
            this.Close();

            //frmPatientInfo frmPatientInfo = new frmPatientInfo();
            //frmPatientInfo.Init(this.patientInfo);
            //frmPatientInfo.ShowDialog(this);
            //frmPatientInfo.BringToFront();
            //if (frmPatientInfo.IsOK == "0")
            //{
            //    return;
            //}
            //else if (frmPatientInfo.IsOK == "1")
            //{
            //    this.isSelect = true;
            //    this.Close();

            //    return;
            //}
            //else if (frmPatientInfo.IsOK == "2")
            //{
            //    this.isSelect = false;
            //    this.Close();
            //    return;
            //}
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

        #endregion

        #region  对外公有方法  //IQueryPatientInfo 成员

        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
                patientInfoCompare = value.Clone();

                if (patientInfo != null)
                {
                    //姓名
                    this.txtName.Text = patientInfo.Name;
                    this.txtCardID.Text = patientInfo.IDCard;
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

        public bool IsSelect
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

        public int Query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType enumQueryType)
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

        private void spNewPatientInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.patientInfo = this.sheetNewPatient.Rows[e.Row].Tag as FS.HISFC.Models.RADT.PatientInfo;
        
        }

    }

    #region 枚举


    /// <summary>
    /// 患者信息枚举列
    /// </summary>
    public enum enuInfoColumnNew
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
        /// 身份证号
        /// </summary>
        IdenNo,

        /// <summary>
        /// 联系电话
        /// </summary>
        HomePhone,

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
        /// 单位及地址
        /// </summary>
        WorkName,

        /// <summary>
        /// 联系人
        /// </summary>
        LinkMan,

        /// <summary>
        /// 住院次数
        /// </summary>
        InTimes
    }

    #endregion
}