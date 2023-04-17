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
    /// [��������: ��Աά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucEmployeeInfoPanel : UserControl
    {
        //��Ա������
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
        //ƴ��������
        FS.HISFC.BizLogic.Manager.Spell spellMgr = new FS.HISFC.BizLogic.Manager.Spell();
        //��Աʵ����
        FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
        //����������
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
        public bool tr = false;//�ж�ˢ��
        private bool isModify = false;//�Ƿ��޸�
        /// <summary>
        /// �Ƿ�����޸�
        /// </summary>
        public bool IsModify
        {
            get
            {
                return this.isModify;
            }
            set
            {
                this.txtEmployeeCode.ReadOnly = value;
                this.btAutoID.Visible = !value;
            }
        }
        public ucEmployeeInfoPanel()
        {
            InitializeComponent();
            InitialCombox();
        }
        /// <summary>
        /// �вι��캯��
        /// </summary>
        /// <param name="empl"></param>
        public ucEmployeeInfoPanel(FS.HISFC.Models.Base.Employee empl)
        {
            InitializeComponent();
            this.bttAdd.Visible = false;
            this.txtEmployeeCode.ReadOnly = false;
            this.employee = empl;
            InitialCombox();
            setInfoToControls();
        }

        /// <summary>
        /// ��ʼ��ComboBoxѡ��
        /// </summary>
        private void InitialCombox()
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            //ֻ��ʾ���˻�ʿվ����Ŀ����б�
            //ArrayList aldepartments = deptMgr.GetDeptNoNurse();
            ArrayList aldepartments = deptMgr.GetDeptmentAll();
            this.comboDeptType.IsListOnly = true;
            this.comboDeptType.AddItems(aldepartments);//�����������ComboBox

            this.comboPersonType.IsListOnly = true;
            this.comboPersonType.AddItems(FS.HISFC.Models.Base.EmployeeTypeEnumService.List());//�����Ա����������

            this.comboSex.IsListOnly = true;
            this.comboSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());//�Ա�

            ArrayList alNurseDept = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            this.comboNurse.IsListOnly = true;
            this.comboNurse.AddItems(alNurseDept);//��������վ������

            this.comboDuty.IsListOnly = true;
            this.comboDuty.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.POSITION));//ְ��

            this.comboLevel.IsListOnly = true;
            this.comboLevel.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.LEVEL));//ְ��

            this.comboPersonEdu.IsListOnly = true;
            this.comboPersonEdu.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.EDUCATION));//ѧ��

            this.cmbEmployeePactType.IsListOnly = true;
            this.cmbEmployeePactType.AddItems(consManager.GetList("EmplPactType"));
        }

        /// <summary>
        /// ���ݲ������ͻ��ArrayList
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetConstant(FS.HISFC.Models.Base.EnumConstant type)
        {

            ArrayList constList = consManager.GetList(type);
            if (constList == null)
                throw new FS.FrameWork.Exceptions.ReturnNullValueException();


            return constList;

        }

        /// <summary>
        /// ���ݴ��������Ϣ���UC
        /// </summary>
        private void setInfoToControls()
        {
            this.txtEmployeeCode.Text = this.employee.ID;//��Ա���� 
            this.txtEmployeeName.Text = this.employee.Name;//��Ա����
            this.txtSpell_Code.Text = this.employee.SpellCode;//ƴ����
            this.txtWB_Code.Text = this.employee.WBCode;//�����
            this.comboSex.Tag = this.employee.Sex.ID;//�Ա�

            //[2007-01-25]��ֹ.NET��ORACLE��ʾ��Χ��ͬ
            //{997CB50F-1CE4-40d3-9A67-5128EAA10FD7}
            this.comboBirthday.Value = this.employee.Birthday;//��������
            //if (this.employee.Birthday.CompareTo(DateTime.MinValue.AddYears(1969)) <= 0 )
            //{
            //    this.comboBirthday.Value = DateTime.MinValue.AddYears(1969);
            //}
            //else
            //{
            //    this.comboBirthday.Value = this.employee.Birthday;//��������
            //}

            this.comboPersonEdu.Tag = this.employee.GraduateSchool.ID;//��ҵѧУ
            this.txtIdentity.Text = this.employee.IDCard;//���֤
            this.comboDuty.Tag = this.employee.Duty.ID;//ְ��
            this.comboLevel.Tag = this.employee.Level.ID;//ְ��
            this.comboDeptType.Tag = this.employee.Dept.ID;//��������
            this.comboNurse.Tag = this.employee.Nurse.ID;//��������վ
            this.comboPersonType.Tag = this.employee.EmployeeType.ID;//��Ա����
            this.numSortId.Text = this.employee.SortID.ToString();//˳���
            this.interface_code.Text = this.employee.InterfaceCode;
            if (this.employee.IsExpert)//�Ƿ�Ϊר��
            {
                this.neuRadioButton1.Checked = true;
            }
            else
            {
                this.neuRadioButton2.Checked = true;
            }
            if (this.employee.IsCanModify)//�Ƿ��ܸ�Ʊ��
            {
                this.neuRadioButton3.Checked = true;
            }
            else
            {
                this.neuRadioButton4.Checked = true;
            }
            if (this.employee.IsNoRegCanCharge)//�Ƿ�ֱ���շ�
            {
                this.neuRadioButton5.Checked = true;
            }
            else
            {
                this.neuRadioButton6.Checked = true;
            }
            switch (this.employee.ValidState)//��Ч��
            {
                case FS.HISFC.Models.Base.EnumValidState.Valid:
                    this.radioValidate1.Checked = true;
                    break;
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    this.radioValidate2.Checked = true;
                    break;
                case FS.HISFC.Models.Base.EnumValidState.Ignore:
                    this.radioValidate3.Checked = true;
                    break;
                default:
                    break;
            }
            this.txtUser_Code.Text = this.employee.UserCode;
            //{6A8C59DC-91FE-4246-A923-06A011918614}
            this.rtxtMark.SuperText = this.employee.Memo;

            this.cmbEmployeePactType.Tag = this.employee.User01;
        }

        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        private bool ValueValidated()
        {
            //��Ա���벻��Ϊ��
            if (this.txtEmployeeCode.Text.Trim() == "")
            {
                MessageBox.Show("��Ա���벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtEmployeeCode.Focus();
                return false;
            }
            //��Ա���볤�Ȳ��ܳ���6λ�ַ�
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtEmployeeCode.Text.Trim(), 6) == false)
            {
                MessageBox.Show("��Ա�������,�뱣����λ�ַ�!", "��ʾ", MessageBoxButtons.OK);
                this.txtEmployeeCode.Focus();
                return false;
            }
            //��Ա���Ʋ���Ϊ��
            if (this.txtEmployeeName.Text.Trim() == "")
            {
                MessageBox.Show("��Ա���Ʋ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtEmployeeName.Focus();
                return false;
            }
            //��Ա���Ƴ��Ȳ��ܳ���64λ�ַ�
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtEmployeeName.Text.Trim(), 64) == false)
            {
                MessageBox.Show("��Ա���ƹ���,�뱣��32λ�ֺ��ֻ�64λ�ַ�!", "��ʾ", MessageBoxButtons.OK);
                this.txtEmployeeName.Focus();
                return false;
            }
            //��Ա�Ա���Ϊ��
            if (this.comboSex.Text == "")
            {
                MessageBox.Show("��Ա�Ա���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboSex.Focus();
                return false;
            }
            //ְ����Ų���Ϊ��
            if (this.comboDuty.Text == "")
            {
                MessageBox.Show("ְ����Ų���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboDuty.Focus();
                return false;
            }
            //ְ�����Ų���Ϊ��
            if (this.comboLevel.Text == "")
            {
                MessageBox.Show("ְ�����Ų���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboLevel.Focus();
                return false;
            }
            //��Ա���Ͳ���Ϊ��
            if (this.comboPersonType.Text == "")
            {
                MessageBox.Show("��Ա���Ͳ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboPersonType.Focus();
                return false;
            }
            else
            {
                if (this.comboPersonType.Text == "�տ�Ա")
                {
                    if (this.personMgr.IsUserCodeSame(this.txtUser_Code.Text))
                    {
                        MessageBox.Show("�շ�Ա�Զ������ظ�����������д��", "��ʾ", MessageBoxButtons.OK);
                        this.txtUser_Code.Focus();
                        return false;
                    }
                }
            }
            //�������Ҳ���Ϊ��
            if (this.comboDeptType.Text == "")
            {
                MessageBox.Show("�������Ҳ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboDeptType.Focus();
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// ��տؼ����ݻָ�ԭ״̬
        /// </summary>
        private void ClearUp()
        {
            this.txtEmployeeCode.Text = "";
            this.txtEmployeeName.Text = "";
            this.txtIdentity.Text = "";
            this.txtSpell_Code.Text = "";
            this.txtWB_Code.Text = "";
            this.txtUser_Code.Text = "";
            this.comboBirthday.Value = DateTime.Now;
            this.comboDeptType.SelectedIndex = -1;
            this.comboDuty.SelectedIndex = -1;
            this.comboLevel.SelectedIndex = -1;
            this.comboNurse.SelectedIndex = -1;
            this.comboSex.SelectedIndex = -1;
            this.comboPersonType.SelectedIndex = -1;
            this.comboPersonEdu.SelectedIndex = -1;
            this.radioValidate1.Checked = true;
            this.neuRadioButton2.Checked = true;
            this.neuRadioButton4.Checked = true;
            this.neuRadioButton6.Checked = true;
        }

        /// <summary>
        /// ���ؼ�����ת������Աʵ��
        /// </summary>
        private FS.HISFC.Models.Base.Employee ConvertUcContextToObject()
        {
            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
            employee.ID = this.txtEmployeeCode.Text.Trim();//��Ա����
            employee.Name = this.txtEmployeeName.Text.Trim();//��Ա����
            employee.SpellCode = this.txtSpell_Code.Text.Trim();//ƴ����
            employee.WBCode = this.txtWB_Code.Text.Trim();//�����
            employee.Sex.ID = this.comboSex.Tag.ToString();//�Ա�
            employee.InterfaceCode = this.interface_code.Text.ToString();
            //[2007-01-25]��ֹ.NET��ORACLE��ʾ��Χ��ͬ
            if (this.comboBirthday.Checked)
            {
                employee.Birthday = this.comboBirthday.Value;//��������
            }
            else
            {
                employee.Birthday = DateTime.MinValue.AddYears(1969);//Ĭ��Ϊ0001��
            }

            employee.GraduateSchool.ID = this.comboPersonEdu.Tag.ToString();//��ҵѧУ
            employee.IDCard = this.txtIdentity.Text.Trim();//���֤
            employee.Duty.ID = this.comboDuty.Tag.ToString();//ְ��
            employee.Level.ID = this.comboLevel.Tag.ToString();//ְ��
            employee.Dept.ID = this.comboDeptType.Tag.ToString();//��������
            employee.Nurse.ID = this.comboNurse.Tag.ToString();//��������վ
            employee.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.numSortId.Text.Trim());//˳���
            employee.EmployeeType.ID = this.comboPersonType.Tag.ToString();//��Ա���
            if (this.neuRadioButton1.Checked)
            {
                employee.IsExpert = true;//�Ƿ�Ϊר��
            }
            else
            {
                employee.IsExpert = false;
            }
            if (this.neuRadioButton3.Checked)
            {
                employee.IsCanModify = true;//�ܷ��Ʊ��
            }
            else
            {
                employee.IsCanModify = false;
            }
            if (this.neuRadioButton5.Checked)
            {
                employee.IsNoRegCanCharge = true;//�ܷ�ֱ���շ�
            }
            else
            {
                employee.IsNoRegCanCharge = false;
            }
            if (this.radioValidate1.Checked)
            {
                employee.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
            }
            else if (this.radioValidate2.Checked)
            {
                employee.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            }
            else
            {
                employee.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
            }
            employee.UserCode = this.txtUser_Code.Text;
            //{6A8C59DC-91FE-4246-A923-06A011918614}
            employee.Memo = this.rtxtMark.Text;

            if (this.cmbEmployeePactType.Tag != null)
            {
                employee.User01 = this.cmbEmployeePactType.Tag.ToString();
            }

            return employee;
        }

        private void bttCancle_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        //private void txtEmployeeCode_Leave(object sender, EventArgs e)
        //{
        //    if (this.txtEmployeeCode.Text != "")
        //    {
        //        if (this.txtEmployeeCode.ReadOnly == false)
        //        {
        //            this.txtEmployeeCode.Text = this.txtEmployeeCode.Text.PadLeft(6, '0');
        //            FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
        //            int temp = ps.SelectEmployIsExist(this.txtEmployeeCode.Text);
        //            if (temp == -1)
        //            {
        //                MessageBox.Show("��ѯ����,������ܻ��ظ�");
        //            }
        //            else if (temp == 1)
        //            {
        //                MessageBox.Show("�����Ѿ����ڣ�����������");
        //                this.txtEmployeeCode.Focus();
        //                this.txtEmployeeCode.Text = "";
        //            }
        //        }
        //    }

        //}

        private void txtEmployeeCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtEmployeeCode.Text != "")
                {
                    if (this.txtEmployeeCode.ReadOnly == false)
                    {
                        this.txtEmployeeCode.Text = this.txtEmployeeCode.Text.PadLeft(6, '0');
                        FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
                        int temp = ps.SelectEmployIsExist(this.txtEmployeeCode.Text);
                        if (temp == -1)
                        {
                            MessageBox.Show("��ѯ����,������ܻ��ظ�");
                        }
                        else if (temp == 1)
                        {
                            MessageBox.Show("�����Ѿ����ڣ�����������");
                            this.txtEmployeeCode.Focus();
                            this.txtEmployeeCode.Text = "";
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ������Ա�����Զ����������ƴ����
        /// </summary>
        private void CreateSpell()
        {
            if (this.txtSpell_Code.Text.Trim() == "" || this.txtWB_Code.Text.Trim() == "")
            {
                FS.HISFC.Models.Base.Spell spell = new FS.HISFC.Models.Base.Spell();
                spell = (FS.HISFC.Models.Base.Spell)spellMgr.Get(this.txtEmployeeName.Text.Trim());
                this.txtSpell_Code.Text = spell.SpellCode;
                this.txtWB_Code.Text = spell.WBCode;
            }
        }
        private void txtEmployeeName_Leave(object sender, EventArgs e)
        {
            CreateSpell();//���������ƴ����

        }

        private void bttConfrim_Click(object sender, EventArgs e)
        {
            if (Save() == 0)
                this.FindForm().DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// ���淽��
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //��֤�ؼ����ݷ���Ҫ��
            if (ValueValidated())
            {
                FS.HISFC.Models.Base.Employee empl = ConvertUcContextToObject();
                if (empl == null) return -1;
                //����ƴ����������
                CreateSpell();
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.HISFC.BizLogic.Manager.Person perMgr = new FS.HISFC.BizLogic.Manager.Person();
                    //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(perMgr.Connection);
                    ////����ʼ
                    //trans.BeginTransaction();
                    ////��������
                    //perMgr.SetTrans(trans.Trans);
                    if (perMgr.Insert(empl) == -1)
                    {
                        if (perMgr.DBErrCode == 1)
                        {
                            if (perMgr.Update(empl) == -1 || perMgr.Update(empl) == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("������Աʧ�ܣ�");
                                return -1;
                            }
                            else
                            {
                                //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(empl);
                                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                                if (param == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                    return -1;
                                }

                            }
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������Աʧ�ܣ�");
                            return -1;
                        }
                    }
                    else
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(empl);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return -1;
                        }

                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("����ɹ���");
                    tr = true;
                    this.txtEmployeeCode.Focus();
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

        private void txtEmployeeCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                System.Windows.Forms.SendKeys.Send("{Tab}");
        }

        private void numSortId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //this.neuRadioButton1.Focus();
                this.txtUser_Code.Focus();
            }
        }

        private void neuRadioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuRadioButton3.Focus();
            }
        }

        private void neuRadioButton3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuRadioButton5.Focus();
            }
        }

        private void neuRadioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuRadioButton3.Focus();
            }
        }

        private void neuRadioButton4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuRadioButton5.Focus();
            }
        }

        private void neuRadioButton5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.radioValidate1.Focus();
            }
        }

        private void bttAdd_Click(object sender, EventArgs e)
        {
            if (Save() == 0)//�������ɹ�����տؼ����ݽ�������
                ClearUp();
        }

        private void txtUser_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.neuRadioButton1.Focus();

            }
        }

        /// <summary>
        /// [2007/08/16]ʧȥ����ʱ�ж�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmployeeCode_Leave(object sender, EventArgs e)
        {
            if (this.txtEmployeeCode.Text != "")
            {
                if (this.txtEmployeeCode.ReadOnly == false)
                {
                    this.txtEmployeeCode.Text = this.txtEmployeeCode.Text.PadLeft(6, '0');
                    FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
                    int temp = ps.SelectEmployIsExist(this.txtEmployeeCode.Text);
                    if (temp == -1)
                    {
                        MessageBox.Show("��ѯ����,������ܻ��ظ�");
                    }
                    else if (temp == 1)
                    {
                        MessageBox.Show("�����Ѿ����ڣ�����������");
                        this.txtEmployeeCode.Focus();
                        this.txtEmployeeCode.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ���ݿ��е������Ա����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuButton1_Click(object sender, EventArgs e)
        {
            string MaxEmplID = "";
            int NextEmplID;
            MaxEmplID = this.personMgr.GetMaxEmployeeID();
            if (MaxEmplID == "" || MaxEmplID == null)
            {
                MessageBox.Show("���ݿ���δ�洢��Ա��Ϣ��������������Ա����");
            }
            else
            {
                NextEmplID = FS.FrameWork.Function.NConvert.ToInt32(MaxEmplID) + 1;
                if (NextEmplID == 1)
                {
                    MessageBox.Show("δ�ܳɹ��������ݿ��д洢����Ա��Ϣ��������������Ա����");
                }
                else
                {
                    this.txtEmployeeCode.Text = NextEmplID.ToString().PadLeft(6, '0');
                }
            }
            this.txtEmployeeCode.Focus();
        }

        private void radioValidate1_KeyDown(object sender, KeyEventArgs e)
        {
            this.rtxtMark.Focus();
        }
    }
}
