using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [功能描述: 科室维护]<br></br>
    /// [创 建 者: 薛占广]<br></br>
    /// [创建时间: 2006－12－8]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDeptmentInfoPanel : UserControl
    {
        //科室实体
        FS.HISFC.Models.Base.Department department = new FS.HISFC.Models.Base.Department();
        //科室管理类
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        //科室结构管理类
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
        //拼音管理类
        FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        public bool tr = false;//取消按钮更新用

        public ucDeptmentInfoPanel()
        {
            InitializeComponent();

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            ArrayList al = this.conMgr.QueryConstantList("HOSPITALLIST");
            this.cmbHospital.AddItems(al);

            this.comboDeptType.IsListOnly=true;
            this.comboDeptType.AddItems(FS.HISFC.Models.Base.DepartmentTypeEnumService.List());
            this.comboDeptType.SelectedIndex = 0;

            //新增加的代码
            this.txtDeptID.Focus();
            this.txtDeptID.ReadOnly = false;
            this.txtDeptID.Text = string.Empty;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            if (this.txtDeptID.Text.Trim() == "")
            {
                MessageBox.Show("科室代码不能为空！", "提示", MessageBoxButtons.OK);
                this.txtDeptID.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptID.Text, 4))
            {
                MessageBox.Show("科室编码过长");
                this.txtDeptID.Focus();
                return false;
            }
            if (this.txtDeptName.Text.Trim() == "")
            {
                MessageBox.Show("科室名称不能为空！", "提示", MessageBoxButtons.OK);
                this.txtDeptName.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptName.Text, 30))
            {
                MessageBox.Show("科室名称过长");
                this.txtDeptName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptShortName.Text, 30))
            {
                MessageBox.Show("科室简称过长");
                this.txtDeptShortName.Focus();
                return false;
            }
            if (this.comboDeptType.Text == "")
            {
                MessageBox.Show("科室的类型不能为空！", "提示", MessageBoxButtons.OK);
                this.comboDeptType.Focus();
                return false;

            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptEnglishName.Text, 20))
            {
                MessageBox.Show("科室英文过长");
                this.txtDeptEnglishName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtSpell_Code.Text, 8))
            {
                this.txtSpell_Code.Focus();
                MessageBox.Show("拼音码过长");
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWB_Code.Text, 8))
            {
                this.txtWB_Code.Focus();
                MessageBox.Show("五笔码过长");
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtUser_Code.Text, 8))
            {
                this.txtUser_Code.Focus();
                MessageBox.Show("自定义码过长");
                return false;
            }
            if (this.comboDeptPro.SelectedIndex == -1)
            {
                MessageBox.Show("请选择科室属性");
                this.comboDeptPro.Focus();
                return false;
            }

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            if (this.cmbHospital.SelectedIndex == -1)
            {
                MessageBox.Show("请选择所属院区");
                this.cmbHospital.Focus();
                return false;
            }

            //{AFA32CB8-F932-45e9-98CE-1892C8B6E8E0}
            if (this.ValidDeptID() < 0)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="dept"></param>
        public ucDeptmentInfoPanel(FS.HISFC.Models.Base.Department dept)
        {
            InitializeComponent();

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            ArrayList al = this.conMgr.QueryConstantList("HOSPITALLIST");
            this.cmbHospital.AddItems(al);

            this.department = dept;
            this.txtDeptID.ReadOnly = true;
            this.chbContinue.Visible = false;
            this.bttAdd.Visible = false;
            this.btAutoID.Visible = false;
            setInfo();
        }

        /// <summary>
        /// 根据传入对象填充控件信息
        /// </summary>
        void setInfo()
        { 
            this.txtDeptID.Text = this.department.ID;//科室编码
            this.txtDeptName.Text = this.department.Name;//科室名称
            this.txtDeptShortName.Text = this.department.ShortName;//科室简称
            this.txtSpell_Code.Text = this.department.SpellCode;//拼音码
            this.txtWB_Code.Text = this.department.WBCode;//五笔码
            this.txtUser_Code.Text = this.department.UserCode;//自定义码
            this.txtDeptEnglishName.Text = this.department.EnglishName;//科室英文名称
            this.comboDeptType.IsListOnly = true;
            this.comboDeptType.AddItems(FS.HISFC.Models.Base.DepartmentTypeEnumService.List());
            this.comboDeptType.Tag = this.department.DeptType.ID.ToString();//科室类型
            switch (this.department.ValidState)//有效性
            { 
                case FS.HISFC.Models.Base.EnumValidState.Valid:
                    this.radioBValid1.Checked = true;
                    this.radioBValid2.Checked = false;
                    this.radioBValid3.Checked = false;
                    break;
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    this.radioBValid1.Checked = false;
                    this.radioBValid2.Checked = true;
                    this.radioBValid3.Checked = false;
                    break;
                default:
                    this.radioBValid1.Checked = false;
                    this.radioBValid2.Checked = false;
                    this.radioBValid3.Checked = true;
                    break;
            }
            this.numtxtSortID.Text = this.department.SortID.ToString();//排序号
            this.comboDeptPro.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.department.SpecialFlag);//特殊科室属性
            this.chbReg.Checked = this.department.IsRegDept;//是否挂号
            this.chbTat.Checked = this.department.IsStatDept;//是否核算

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            this.cmbHospital.Tag = this.department.HospitalID;

        }

        /// <summary>
        /// 获取控制面板上的数据 
        /// </summary>
        /// <returns> 返回包含数据的实体</returns>
        private FS.HISFC.Models.Base.Department CovertDeptFromPanel()
        {
            FS.HISFC.Models.Base.Department info = new FS.HISFC.Models.Base.Department();
            
            info.ID = this.txtDeptID.Text.Trim();//科室编码
            info.Name = this.txtDeptName.Text.Trim();//科室名称
            info.SpellCode = this.txtSpell_Code.Text.Trim();//拼音码
            info.WBCode = this.txtWB_Code.Text.Trim();//五笔码            
            info.UserCode = this.txtUser_Code.Text.Trim();//自定义码
            info.ShortName = this.txtDeptShortName.Text;//科室简称
            if (this.txtDeptEnglishName.Text != "")
                info.EnglishName = this.txtDeptEnglishName.Text.Trim();//科室英文名称
            if (this.comboDeptType.SelectedIndex != -1)
                info.DeptType.ID = this.comboDeptType.Tag;

            if (this.radioBValid1.Checked)
                info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;                //有效性
            else if (this.radioBValid2.Checked)
                info.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            else info.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;

            if (this.numtxtSortID.Text != "")
             info.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.numtxtSortID.Text.Trim());//序号
             info.SpecialFlag = this.comboDeptPro.SelectedIndex.ToString();//科室属性
             info.IsRegDept = this.chbReg.Checked;//是否挂号科室
             info.IsStatDept = this.chbTat.Checked;//是否统计科室

             //{335827AE-42F6-4cbd-A73F-1B900B070E74}
             if (this.cmbHospital.SelectedIndex != -1)
             {
                 info.HospitalID = this.cmbHospital.Tag.ToString();
                 info.HospitalName = this.cmbHospital.Text;
             }

            return info;
        }

        /// <summary>
        /// 清空数据 
        /// </summary>
        private void CleanUp()
        {
            this.txtDeptID.Text = "";
            this.txtDeptName.Text = "";
            this.txtSpell_Code.Text = "";
            this.txtUser_Code.Text = "";
            this.txtDeptEnglishName.Text = "";
            //科室简称
            this.txtDeptShortName.Text = "";
            //自定义名称
            this.txtUser_Code.Text = "";
            this.comboDeptType.SelectedIndex = -1;
            this.radioBValid1.Checked = true;
            this.numtxtSortID.Text = "0";

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            this.cmbHospital.SelectedIndex = -1;
            
            this.chbReg.Checked = true;
            this.chbTat.Checked = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bttCancle_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void bttConfirm_Click(object sender, EventArgs e)
        {   
            if(Save()==0)
            this.FindForm().DialogResult = DialogResult.OK;

        }
        /// <summary>
        /// 判断是不是要废弃或停用，如果停用或废弃 ，判断本科室下是不是 有没有停用或废弃的人员 如果有 返回FALSE 否则 返回true
        /// </summary>
        /// <returns></returns>
        private bool IsDisuse()
        {
            if (this.radioBValid2.Checked || this.radioBValid3.Checked)
            {
                if (department != null)
                {
                    FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
                    ArrayList list = new ArrayList();
                    list = p.GetPersonsByDeptID(department.ID);
                    if (list != null)
                    {
                        if (list.Count > 0)
                        {
                            MessageBox.Show("要废弃或停用本科室，请先将本科室内的人员转移或置成废弃或停用");
                            return false;
                        }
                    }
                }
            }
            return true;
            //departmentInfo
        }

        public int Save()
        {
            if (this.ValueValidated() && IsDisuse())
            {
                FS.HISFC.Models.Base.Department dept = CovertDeptFromPanel();
                if (dept == null) return -1;
                //生成拼音码和五笔码
                CreateSpell();
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.HISFC.BizLogic.Manager.Department deptmentMgr = new FS.HISFC.BizLogic.Manager.Department();
                    //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(deptmentMgr.Connection);
                    //trans.BeginTransaction();
                    //deptmentMgr.SetTrans(trans.Trans);
                    //先插入，在修改
                    int returnValue = 0;
                    returnValue = deptmentMgr.Insert(dept);

                    if (returnValue == -1) 
                    {
                        if (deptmentMgr.DBErrCode == 1)
                        {
                            returnValue = deptmentMgr.Update(dept);

                            int deptStatRtn = deptStatMgr.UpdateDeptName(dept);

                            if (returnValue <= 0 || deptStatRtn == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();;
                                
                                MessageBox.Show("更新失败！" + deptmentMgr.Err);

                                return -1;
                            }
                            else
                            {
                                //嵌入对其他系统或其他业务模块的信息传送桥接处理
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(dept);
                                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                                if (param == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("科室添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                                    return -1;
                                }

                            }
                        }
                        else 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;

                            MessageBox.Show("插入失败！" + deptmentMgr.Err);

                            return -1;
                        }
                    }
                    else
                    {
                        //嵌入对其他系统或其他业务模块的信息传送桥接处理
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(dept);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("科室添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("保存成功！");
                    this.txtDeptID.Focus();
                    tr = true;
                    return 0;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    return -1;
                }
            }
            else 
            {              
                return -1;
            }
        }
        #region 根据科室名称生成拼音码和五笔码
        /// <summary>
        /// 根据科室名称生成拼音码和五笔码
        /// </summary>
        private void CreateSpell()
        {
            if (this.txtSpell_Code.Text == "" || this.txtWB_Code.Text == "")
            {
                //根据名称生产拼音码和五笔码
                FS.HISFC.Models.Base.Spell spell = (FS.HISFC.Models.Base.Spell)mySpell.Get(this.txtDeptName.Text.Trim());
                this.txtSpell_Code.Text = spell.SpellCode;
                this.txtWB_Code.Text = spell.WBCode;
            }
        }
        #endregion

        private void txtDeptID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                if (this.txtDeptID.Text == "")
                {
                    string tempStr = deptMgr.GetMaxDeptID();
                    if (tempStr != null && tempStr != "")
                    {
                        this.txtDeptID.Text = tempStr.PadLeft(4, '0');
                    }
                    else
                    {
                        return;
                    }
                }
                this.txtDeptName.Focus();
            }
        }

        private void txtDeptName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                
                this.txtDeptShortName.Focus();
            }
        }

        //private void txtDeptID_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (this.txtDeptID.Text != "")
        //        {
        //            this.txtDeptID.Text = this.txtDeptID.Text.PadLeft(4, '0');
        //            if (this.txtDeptID.ReadOnly == false)
        //            {
        //               FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        //                //检查输入编码是否已存在
        //                int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
        //                if (temp == -1)
        //                {
        //                    MessageBox.Show("数据查询出错");
        //                }
        //                else if (temp == 1)
        //                {
        //                    MessageBox.Show("此编码已经存在，请重新输入");
        //                    this.txtDeptID.Text = "";
        //                    this.txtDeptID.Focus();
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }
        //        else
        //        {
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}


        private void txtDeptID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    if (this.txtDeptID.Text != "")
                    {
                        this.txtDeptID.Text = this.txtDeptID.Text.PadLeft(4, '0');
                        if (this.txtDeptID.ReadOnly == false)
                        {
                            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
                            //检查输入编码是否已存在
                            int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
                            if (temp == -1)
                            {
                                MessageBox.Show("数据查询出错");
                            }
                            else if (temp == 1)
                            {
                                MessageBox.Show("此编码已经存在，请重新输入");
                                this.txtDeptID.Text = "";
                                this.txtDeptID.Focus();
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void txtDeptShortName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{Tab}");
            }
        }

        private void chbTat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.chbContinue.Checked)
                {
                    bttAdd_Click(sender, null);
                }
                else
                {
                    bttConfirm_Click(sender, null);
                }
            }
        }

        /// <summary>
        /// 增加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttAdd_Click(object sender, EventArgs e)
        {
           if( Save()==0)//如果保存成功，则情况控件内容以便重新输入
             CleanUp();
        }

        private void radioBValid1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            this.numtxtSortID.Focus();
        }

        private void chbReg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.chbTat.Focus();
        }

        private void comboDeptPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.chbReg.Focus();
        }

        /// <summary>
        /// [2007/08/16]失去焦点时判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDeptID_Leave(object sender, EventArgs e)
        {
            //try{AFA32CB8-F932-45e9-98CE-1892C8B6E8E0}
            //{

            //    if (this.txtDeptID.Text != "")
            //    {
            //        this.txtDeptID.Text = this.txtDeptID.Text.PadLeft(4, '0');
            //        if (this.txtDeptID.ReadOnly == false)
            //        {
            //            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            //            //检查输入编码是否已存在
            //            int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
            //            if (temp == -1)
            //            {
            //                MessageBox.Show("数据查询出错");
            //            }
            //            else if (temp == 1)
            //            {
            //                MessageBox.Show("此编码已经存在，请重新输入");
            //                this.txtDeptID.Text = "";
            //                this.txtDeptID.Focus();
            //            }
            //            else
            //            {
            //            }
            //        }
            //    }
            //    else
            //    {
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

        /// <summary>
        /// 校验编码是否存在{AFA32CB8-F932-45e9-98CE-1892C8B6E8E0}
        /// </summary>
        /// <returns></returns>
        private int ValidDeptID()
        {
            string deptID = this.txtDeptID.Text.Trim();
            if (!string.IsNullOrEmpty(deptID))
            {
                FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
                //检查输入编码是否已存在
                int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
                if (temp == -1)
                {
                    MessageBox.Show("数据查询出错");
                }
                else if (temp == 1)
                {
                    //{BDA13B0B-D3F6-4c35-A9BE-6EFCD01BD3BA}
                    //只读时不进行判断
                    if (!this.txtDeptID.ReadOnly)
                    {
                        MessageBox.Show("此编码已经存在，请重新输入");
                        this.txtDeptID.Text = "";
                        this.txtDeptID.Focus();
                    }
                }
                else
                {
                }
 
            }
            return 1;
        }

        private void txtDeptName_Leave(object sender, EventArgs e)
        {
            this.txtDeptShortName.Text = this.txtDeptName.Text;
            CreateSpell();
            //this.txtUser_Code.Text = this.txtSpell_Code.Text;为什么要修改自定义码？
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string tempStr = deptMgr.GetMaxDeptID();
            if (tempStr != null && tempStr != "")
            {
                if (tempStr == "-1")
                {
                    MessageBox.Show("未能成功获得数据库中科室编码，请自行输入！");
                }
                else
                {
                    this.txtDeptID.Text = tempStr.PadLeft(4, '0');
                }
            }
            this.txtDeptID.Focus();
        }

    }
}