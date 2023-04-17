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
    /// [��������: ����ά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��8]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDeptmentInfoPanel : UserControl
    {
        //����ʵ��
        FS.HISFC.Models.Base.Department department = new FS.HISFC.Models.Base.Department();
        //���ҹ�����
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        //���ҽṹ������
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
        //ƴ��������
        FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

        //{335827AE-42F6-4cbd-A73F-1B900B070E74}
        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        public bool tr = false;//ȡ����ť������

        public ucDeptmentInfoPanel()
        {
            InitializeComponent();

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            ArrayList al = this.conMgr.QueryConstantList("HOSPITALLIST");
            this.cmbHospital.AddItems(al);

            this.comboDeptType.IsListOnly=true;
            this.comboDeptType.AddItems(FS.HISFC.Models.Base.DepartmentTypeEnumService.List());
            this.comboDeptType.SelectedIndex = 0;

            //�����ӵĴ���
            this.txtDeptID.Focus();
            this.txtDeptID.ReadOnly = false;
            this.txtDeptID.Text = string.Empty;
        }

        /// <summary>
        /// ��֤
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            if (this.txtDeptID.Text.Trim() == "")
            {
                MessageBox.Show("���Ҵ��벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtDeptID.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptID.Text, 4))
            {
                MessageBox.Show("���ұ������");
                this.txtDeptID.Focus();
                return false;
            }
            if (this.txtDeptName.Text.Trim() == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtDeptName.Focus();
                return false;
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptName.Text, 30))
            {
                MessageBox.Show("�������ƹ���");
                this.txtDeptName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptShortName.Text, 30))
            {
                MessageBox.Show("���Ҽ�ƹ���");
                this.txtDeptShortName.Focus();
                return false;
            }
            if (this.comboDeptType.Text == "")
            {
                MessageBox.Show("���ҵ����Ͳ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboDeptType.Focus();
                return false;

            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptEnglishName.Text, 20))
            {
                MessageBox.Show("����Ӣ�Ĺ���");
                this.txtDeptEnglishName.Focus();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtSpell_Code.Text, 8))
            {
                this.txtSpell_Code.Focus();
                MessageBox.Show("ƴ�������");
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWB_Code.Text, 8))
            {
                this.txtWB_Code.Focus();
                MessageBox.Show("��������");
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtUser_Code.Text, 8))
            {
                this.txtUser_Code.Focus();
                MessageBox.Show("�Զ��������");
                return false;
            }
            if (this.comboDeptPro.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ���������");
                this.comboDeptPro.Focus();
                return false;
            }

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            if (this.cmbHospital.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ������Ժ��");
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
        /// �вι��캯��
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
        /// ���ݴ���������ؼ���Ϣ
        /// </summary>
        void setInfo()
        { 
            this.txtDeptID.Text = this.department.ID;//���ұ���
            this.txtDeptName.Text = this.department.Name;//��������
            this.txtDeptShortName.Text = this.department.ShortName;//���Ҽ��
            this.txtSpell_Code.Text = this.department.SpellCode;//ƴ����
            this.txtWB_Code.Text = this.department.WBCode;//�����
            this.txtUser_Code.Text = this.department.UserCode;//�Զ�����
            this.txtDeptEnglishName.Text = this.department.EnglishName;//����Ӣ������
            this.comboDeptType.IsListOnly = true;
            this.comboDeptType.AddItems(FS.HISFC.Models.Base.DepartmentTypeEnumService.List());
            this.comboDeptType.Tag = this.department.DeptType.ID.ToString();//��������
            switch (this.department.ValidState)//��Ч��
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
            this.numtxtSortID.Text = this.department.SortID.ToString();//�����
            this.comboDeptPro.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.department.SpecialFlag);//�����������
            this.chbReg.Checked = this.department.IsRegDept;//�Ƿ�Һ�
            this.chbTat.Checked = this.department.IsStatDept;//�Ƿ����

            //{335827AE-42F6-4cbd-A73F-1B900B070E74}
            this.cmbHospital.Tag = this.department.HospitalID;

        }

        /// <summary>
        /// ��ȡ��������ϵ����� 
        /// </summary>
        /// <returns> ���ذ������ݵ�ʵ��</returns>
        private FS.HISFC.Models.Base.Department CovertDeptFromPanel()
        {
            FS.HISFC.Models.Base.Department info = new FS.HISFC.Models.Base.Department();
            
            info.ID = this.txtDeptID.Text.Trim();//���ұ���
            info.Name = this.txtDeptName.Text.Trim();//��������
            info.SpellCode = this.txtSpell_Code.Text.Trim();//ƴ����
            info.WBCode = this.txtWB_Code.Text.Trim();//�����            
            info.UserCode = this.txtUser_Code.Text.Trim();//�Զ�����
            info.ShortName = this.txtDeptShortName.Text;//���Ҽ��
            if (this.txtDeptEnglishName.Text != "")
                info.EnglishName = this.txtDeptEnglishName.Text.Trim();//����Ӣ������
            if (this.comboDeptType.SelectedIndex != -1)
                info.DeptType.ID = this.comboDeptType.Tag;

            if (this.radioBValid1.Checked)
                info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;                //��Ч��
            else if (this.radioBValid2.Checked)
                info.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            else info.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;

            if (this.numtxtSortID.Text != "")
             info.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.numtxtSortID.Text.Trim());//���
             info.SpecialFlag = this.comboDeptPro.SelectedIndex.ToString();//��������
             info.IsRegDept = this.chbReg.Checked;//�Ƿ�Һſ���
             info.IsStatDept = this.chbTat.Checked;//�Ƿ�ͳ�ƿ���

             //{335827AE-42F6-4cbd-A73F-1B900B070E74}
             if (this.cmbHospital.SelectedIndex != -1)
             {
                 info.HospitalID = this.cmbHospital.Tag.ToString();
                 info.HospitalName = this.cmbHospital.Text;
             }

            return info;
        }

        /// <summary>
        /// ������� 
        /// </summary>
        private void CleanUp()
        {
            this.txtDeptID.Text = "";
            this.txtDeptName.Text = "";
            this.txtSpell_Code.Text = "";
            this.txtUser_Code.Text = "";
            this.txtDeptEnglishName.Text = "";
            //���Ҽ��
            this.txtDeptShortName.Text = "";
            //�Զ�������
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
        /// �ж��ǲ���Ҫ������ͣ�ã����ͣ�û���� ���жϱ��������ǲ��� ��û��ͣ�û��������Ա ����� ����FALSE ���� ����true
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
                            MessageBox.Show("Ҫ������ͣ�ñ����ң����Ƚ��������ڵ���Աת�ƻ��óɷ�����ͣ��");
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
                //����ƴ����������
                CreateSpell();
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.HISFC.BizLogic.Manager.Department deptmentMgr = new FS.HISFC.BizLogic.Manager.Department();
                    //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(deptmentMgr.Connection);
                    //trans.BeginTransaction();
                    //deptmentMgr.SetTrans(trans.Trans);
                    //�Ȳ��룬���޸�
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
                                
                                MessageBox.Show("����ʧ�ܣ�" + deptmentMgr.Err);

                                return -1;
                            }
                            else
                            {
                                //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(dept);
                                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                                if (param == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("�������ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                    return -1;
                                }

                            }
                        }
                        else 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;

                            MessageBox.Show("����ʧ�ܣ�" + deptmentMgr.Err);

                            return -1;
                        }
                    }
                    else
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(dept);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("�������ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("����ɹ���");
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
        #region ���ݿ�����������ƴ����������
        /// <summary>
        /// ���ݿ�����������ƴ����������
        /// </summary>
        private void CreateSpell()
        {
            if (this.txtSpell_Code.Text == "" || this.txtWB_Code.Text == "")
            {
                //������������ƴ����������
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
        //                //�����������Ƿ��Ѵ���
        //                int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
        //                if (temp == -1)
        //                {
        //                    MessageBox.Show("���ݲ�ѯ����");
        //                }
        //                else if (temp == 1)
        //                {
        //                    MessageBox.Show("�˱����Ѿ����ڣ�����������");
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
                            //�����������Ƿ��Ѵ���
                            int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
                            if (temp == -1)
                            {
                                MessageBox.Show("���ݲ�ѯ����");
                            }
                            else if (temp == 1)
                            {
                                MessageBox.Show("�˱����Ѿ����ڣ�����������");
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
        /// ���Ӱ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttAdd_Click(object sender, EventArgs e)
        {
           if( Save()==0)//�������ɹ���������ؼ������Ա���������
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
        /// [2007/08/16]ʧȥ����ʱ�ж�
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
            //            //�����������Ƿ��Ѵ���
            //            int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
            //            if (temp == -1)
            //            {
            //                MessageBox.Show("���ݲ�ѯ����");
            //            }
            //            else if (temp == 1)
            //            {
            //                MessageBox.Show("�˱����Ѿ����ڣ�����������");
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
        /// У������Ƿ����{AFA32CB8-F932-45e9-98CE-1892C8B6E8E0}
        /// </summary>
        /// <returns></returns>
        private int ValidDeptID()
        {
            string deptID = this.txtDeptID.Text.Trim();
            if (!string.IsNullOrEmpty(deptID))
            {
                FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
                //�����������Ƿ��Ѵ���
                int temp = dept.SelectDepartMentIsExist(this.txtDeptID.Text.PadLeft(4, '0'));
                if (temp == -1)
                {
                    MessageBox.Show("���ݲ�ѯ����");
                }
                else if (temp == 1)
                {
                    //{BDA13B0B-D3F6-4c35-A9BE-6EFCD01BD3BA}
                    //ֻ��ʱ�������ж�
                    if (!this.txtDeptID.ReadOnly)
                    {
                        MessageBox.Show("�˱����Ѿ����ڣ�����������");
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
            //this.txtUser_Code.Text = this.txtSpell_Code.Text;ΪʲôҪ�޸��Զ����룿
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string tempStr = deptMgr.GetMaxDeptID();
            if (tempStr != null && tempStr != "")
            {
                if (tempStr == "-1")
                {
                    MessageBox.Show("δ�ܳɹ�������ݿ��п��ұ��룬���������룡");
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