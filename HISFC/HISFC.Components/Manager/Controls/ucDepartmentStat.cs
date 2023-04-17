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
    /// [��������: ���ҽṹά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��4]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDepartmentStat : UserControl
    {
        #region ����

        //���ҷ���
        private FS.HISFC.Models.Base.DepartmentStat departmentStat = null;
        //ƴ�������ࣨƴ���룬�����ȣ�
        private FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();
        //����������
        private FS.HISFC.BizLogic.Manager.PrivInOutDept myPrivDept = new FS.HISFC.BizLogic.Manager.PrivInOutDept();

        //������
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        private ArrayList alChooseDept = new ArrayList();

        //�Ƿ�������֯�ṹ����ӿ���
        bool isAddDept = true;


        #endregion

        #region  ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucDepartmentStat()
        {
            InitializeComponent();
            Init();
        }
        #endregion

        #region ���캯�������������Ϣ
        /// <summary>
        /// ���캯�������������Ϣ
        /// </summary>
        /// <param name="dept"></param>
        public ucDepartmentStat(FS.HISFC.Models.Base.DepartmentStat dept)
        {
            InitializeComponent();
            this.departmentStat = dept;
            Init();
        }
        #endregion

        #region ���ҷ���CheckedChanged����
        private void chbClass_CheckedChanged(object sender, EventArgs e)
        {
            //������ұ��벻Ϊ��
            //  if (this.deptment.DeptCode != null) return;

            //������ҷ��౻ѡ��
            if (this.chbClass.Checked)
            {
                this.comboDeptName.DropDownStyle = ComboBoxStyle.Simple;
                this.txtDeptSimpleName.Enabled = false;
                this.txtUserCode.Enabled = false;
                this.txtDeptEnglish.Enabled = false;
                this.comboDeptType.Enabled = false;
                this.comboDeptPro.Enabled = false;
                this.chbReg.Enabled = false;
                this.chbStop.Enabled = false;
                this.chbTat.Enabled = false;

                this.comboDeptName.Focus();
            }
            else
            {
                this.comboDeptName.DropDownStyle = ComboBoxStyle.DropDown;
                this.txtDeptSimpleName.Enabled = true;
                this.txtUserCode.Enabled = true;
                this.txtDeptEnglish.Enabled = true;
                this.comboDeptType.Enabled = true;
                this.comboDeptPro.Enabled = true;
                this.chbReg.Enabled = true;
                this.chbStop.Enabled = true;
                this.chbTat.Enabled = true;
            }

            this.comboDeptName.Text = "";
            this.txtDeptCode.Text = "";
            this.comboDeptName.Focus();
        }
        #endregion

        #region �ؼ���ʼ������ Init()
        /// <summary>
        /// �ؼ���ʼ������
        /// </summary>
        public void Init()
        {
            this.neuTabControl1.TabPages.Remove(this.tabPage2);
            this.neuTabControl1.TabPages.Remove(this.tabPage3);
            try
            {
                if (this.departmentStat.StatCode != "00")
                {
                    isAddDept = false;
                }
                //ֻ����������ʱ���ſ���ѡ����ң��ſ��Բ����Ƿ��ǿ��ҷ���,����ʾ��ʾ��Ϣ
                if (this.departmentStat.DeptCode == "")
                {
                    this.chbClass.Enabled = true;

                }
                else
                {
                    //�޸Ŀ���ʱ�������޸Ŀ��ҷ��࣬����ѡ����������,����ʾ��ʾ��Ϣ
                    this.chbClass.Enabled = false;

                }
                //��������Ϣ��ʾ�ڿؼ���
                //�ڵ����ͣ�1�ռ����ң�0���ҷ��ࡣ
                if (this.departmentStat.NodeKind == 1)
                    this.chbClass.Checked = false;
                else
                    this.chbClass.Checked = true;

                //���ȫ������ʹ�õĿ���
                ArrayList deptList = deptMgr.GetDeptmentAll();

                //���û�в鵽����
                if (deptList == null)
                {
                    MessageBox.Show("��ȡ������Ϣʧ��" + deptMgr.Err);
                    return;
                }

                //��ѯCom_Department�� �ֱ���ӵ�2��ComboBox��
                this.comboDeptType.AddItems(FS.HISFC.Models.Base.DepartmentTypeEnumService.List());
                this.comboDeptName.AddItems(deptList);


                //�ж�������һ���ʵ����  ����ҵı����һλ��"S" 
                if (this.departmentStat.DeptCode.IndexOf("S") > -1 && this.departmentStat.DeptCode.Substring(0, 1) == "S") //�����
                {
                    this.chbClass.Checked = true;//�����
                    this.txtDeptCode.Text = this.departmentStat.DeptCode;
                    this.comboDeptName.Text = this.departmentStat.DeptName;
                    this.txtSpellCode.Text = this.departmentStat.SpellCode;
                    this.txtWbCode.Text = this.departmentStat.WBCode;
                    this.txtSortID.Text = this.departmentStat.SortId.ToString();
                    this.txtMark.Text = this.departmentStat.Memo;
                }
                else
                    if (this.departmentStat.DeptCode == "")
                    {
                        //��ʼ��
                    }
                    else//ʵ����
                    {
                        FS.HISFC.Models.Base.Department objTemp = deptMgr.GetDeptmentById(this.departmentStat.DeptCode);
                        if (objTemp == null)
                        {
                            MessageBox.Show("��ѯ������Ϣʧ��" + deptMgr.Err);
                            return;
                        }

                        this.setInfo(objTemp);
                        this.txtMark.Text = this.departmentStat.Memo;//��ע
                    }

                #region ���� ά����������
                ////ȷ���Ƿ���Ҫά���������� ���ݿ��DeptStat��StatCode  03����ҩ..05
                //if (this.department.StatCode == "03" || this.department.StatCode == "05")
                //{
                //    //��ʶ��ͳ�ƴ�����Ҫά����������
                //    this.IsInOutDept = true;

                //    //ȡ�����б�:ȫԺ����Union���ҽṹ�е��Զ������
                //    FS.HISFC.BizLogic.Manager.DepartmentStatManager dept = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
                //    this.alChooseDept = dept.LoadChildrenUnionDept(this.department.StatCode, "AAAA");
                //    if (this.alChooseDept == null)
                //    {
                //        MessageBox.Show("ȡ�����б����:" + dept.Err, "��ʾ");
                //        return;
                //    }
                //    //objHelper���ڲ��ҿ�������
                //    objHelper.ArrayObject = this.alChooseDept;

                //    //��ʾ������
                //    this.ShowInOutDept(this.neuSpread1_Sheet1, this.department.StatCode + "10");
                //    //if(this.fpSpread1_Sheet1.RowCount == 0)	this.btnDeleteInput.Enabled = false;

                //    //��ʾ�������
                //    this.ShowInOutDept(this.neuSpread2_Sheet1, this.department.StatCode + "20");
                //    //if(this.fpSpread2_Sheet1.RowCount == 0)	this.btnDeleteOutput.Enabled = false;
                //}
                //else
                //{
                //    this.neuTabControl1.TabPages.Remove(this.tabPage2);
                //    this.neuTabControl1.TabPages.Remove(this.tabPage3);
                //}
                #endregion
            }
            catch (Exception a) { MessageBox.Show(a.Message); }
        }

        #region (����)--��ʾ������
        /// <summary>
        /// ��ʾ������
        /// </summary>
        private void ShowInOutDept(FarPoint.Win.Spread.SheetView sheetView, string privType)
        {
            //ȡ�������б�
            ArrayList al = this.myPrivDept.GetPrivInOutDeptList(this.departmentStat.ID, privType);
            if (al == null)
            {
                MessageBox.Show(this.myPrivDept.Err);
                return;
            }

            FS.HISFC.Models.Base.PrivInOutDept dept;

            sheetView.RowCount = al.Count;
            for (int i = 0; i < al.Count; i++)
            {
                dept = al[i] as FS.HISFC.Models.Base.PrivInOutDept;
                sheetView.Cells[i, 0].Value = dept.SortID;				//����
                sheetView.Cells[i, 1].Value = dept.ID;		//���ű���
                sheetView.Cells[i, 2].Value = dept.Name;	//��������
                sheetView.Cells[i, 3].Value = objHelper.GetObjectFromID(dept.ID).User02;//������������
            }
        }
        #endregion

        #endregion

        #region �����¼������ж������ӿ���ʱ�Ƿ����ӵ��Ǳ�����(�޸��ˣ�·־��,�޸�ʱ�䣺2007-4-11)
        public delegate bool CheckHander(string DeptCode);
        public static event CheckHander DoCheckNode;
        protected virtual bool DoEvent(string DeptCode)
        {
            if (DoCheckNode != null)
                return DoCheckNode(DeptCode);
            return true;
        }
        #endregion

        #region  setInfo(Department dept)���ݴ����Department������䴰����ؼ�

        /// <summary>
        /// ���ݴ����Department������䴰����ؼ�
        /// </summary>
        /// <param name="dept"></param>
        private void setInfo(FS.HISFC.Models.Base.Department dept)
        {
            this.comboDeptName.Text = dept.Name;//��������
            this.txtDeptCode.Text = dept.ID;//���ұ���
            this.txtSortID.Text = dept.SortID.ToString();//˳���
            this.txtSpellCode.Text = dept.SpellCode;//ƴ���� 
            this.txtWbCode.Text = dept.WBCode;//�����           
            this.txtUserCode.Text = dept.UserCode;//�Զ�����
            this.txtDeptSimpleName.Text = dept.ShortName;//���Ҽ��
            this.txtDeptEnglish.Text = dept.EnglishName;//����Ӣ����
            this.comboDeptType.Tag = dept.DeptType.ID;//��������

            if (dept.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)//��Ч״̬
            {
                this.chbStop.Checked = true;
            }
            else
            {
                this.chbStop.Checked = false;
            }
            this.comboDeptPro.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(dept.SpecialFlag); //��������
            //this.txtMark.Text = dept.Memo;//��ע
            this.chbReg.Checked = dept.IsRegDept;//�Ƿ�Һſ���
            this.chbTat.Checked = dept.IsStatDept;//�Ƿ�ͳ�ƿ���

        }
        #endregion

        #region ȡ����ť�¼�
        private void btCancle_Click_1(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

        #region ValidateValue()��֤������Ϣ�Ƿ�¼����ȷ
        /// <summary>
        /// ��֤������Ϣ�Ƿ�¼����ȷ
        /// </summary>
        /// <returns></returns>
        private bool ValidateValue()
        {
            //������ҷ���Checked=True
            if (!this.chbClass.Checked)
            {
                if (this.txtDeptCode.Text.Trim() == "")
                {
                    MessageBox.Show("���Ҵ��벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                    this.txtDeptCode.Focus();
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptCode.Text, 4))
                {
                    MessageBox.Show("���ұ������");
                    txtDeptCode.Focus();
                    return false;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptSimpleName.Text, 16))
                {
                    MessageBox.Show("���Ҽ�ƹ���");
                    this.txtDeptSimpleName.Focus();
                    return false;
                }
                if (this.comboDeptType.Text == "")
                {
                    MessageBox.Show("���ҵ����Ͳ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                    this.comboDeptType.Focus();
                    return false;

                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtDeptEnglish.Text, 20))
                {
                    MessageBox.Show("����Ӣ�Ĺ���");
                    this.txtDeptEnglish.Focus();
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(txtSpellCode.Text, 8))
                {
                    txtSpellCode.Focus();
                    MessageBox.Show("ƴ�������");
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWbCode.Text, 8))
                {
                    this.txtWbCode.Focus();
                    MessageBox.Show("��������");
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtUserCode.Text, 8))
                {
                    this.txtUserCode.Focus();
                    MessageBox.Show("�Զ��������");
                }
                if (this.comboDeptPro.SelectedIndex == -1)
                {
                    MessageBox.Show("��ѡ���������");
                    this.comboDeptPro.Focus();
                }
            }
            //�����������Ϊ��
            if (this.comboDeptName.Text.Trim() == "")
            {
                MessageBox.Show("��������ҿ������ƣ�", "��ʾ", MessageBoxButtons.OK);
                this.comboDeptName.Focus();
                return false;
            }
            //����������
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.comboDeptName.Text, 30))
            {
                MessageBox.Show("�������ƹ���");
                this.comboDeptName.Focus();
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// ȷ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            //�ж�������Ч��
            if (!ValidateValue()) return;

            //ȡ�ؼ��еĿ���������Ϣ
            if (this.departmentStat == null)
            {
                this.departmentStat = new FS.HISFC.Models.Base.DepartmentStat();
            }

            //����ǿ��ҷ��࣬��ȡ���ҷ���������롣
            if (this.chbClass.Checked)
            {
                this.departmentStat.NodeKind = 0;

                //������������ҷ��࣬��ȡ
                if (this.departmentStat.DeptCode == "")
                {
                    this.GetMaxCode();
                }
            }
            else
            {
                this.departmentStat.NodeKind = 1;
            }

            FS.HISFC.Models.Base.Department deptment = new FS.HISFC.Models.Base.Department();

            //�����ӿ���ʱ�жϸÿ����Ƿ��Ѿ�����

            if (!this.DoEvent(this.txtDeptCode.Text.Trim()))
            {
                MessageBox.Show("�ÿ����ڱ����ҽṹ���Ѵ��ڣ�������ѡ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region ���һ�����Ϣ
            if (!this.chbClass.Checked)//���ǿ��ҷ���
            {
                deptment.ID = this.txtDeptCode.Text.Trim();//���ұ���
                deptment.Name = this.comboDeptName.Text.Trim();//��������
                deptment.SpellCode = this.txtSpellCode.Text.Trim();//ƴ����
                deptment.WBCode = this.txtWbCode.Text.Trim();//�����
                deptment.UserCode = this.txtUserCode.Text.Trim();//�Զ�����
                deptment.ShortName = this.txtDeptSimpleName.Text.Trim();//���Ҽ��

                //���Ӣ�����Ʋ�Ϊ��
                if (this.txtDeptEnglish.Text != "")
                {
                    deptment.EnglishName = this.txtDeptEnglish.Text.Trim();//����Ӣ������
                }

                if (this.comboDeptType.SelectedIndex != -1)
                {
                    deptment.DeptType.ID = this.comboDeptType.Tag;//��������ID
                }
                //ͣ��
                if (this.chbStop.Checked)
                {
                    deptment.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;//��Ч��״̬
                }
                else
                {
                    deptment.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                }
                //����ID
                if (txtSortID.Text != "")
                {
                    deptment.SortID = FS.FrameWork.Function.NConvert.ToInt32(txtSortID.Text.Trim());//����ID
                }
                deptment.SpecialFlag = this.comboDeptPro.SelectedIndex.ToString();//������ұ�ʶ
                deptment.IsRegDept = this.chbReg.Checked;//�Ƿ�Һſ���
                deptment.IsStatDept = this.chbTat.Checked;//�Ƿ�ͳ�ƿ���
                deptment.Memo = this.txtMark.Text.Trim();//��ע

                #region  �Ƿ��Ѿ����ڱ�����ͬ�����Ʋ�ͬ�Ŀ���
                FS.HISFC.Models.Base.Department depttempt = deptMgr.GetDeptmentById(deptment.ID);
                if (depttempt == null)
                {
                    MessageBox.Show("�ÿ��ұ���û����֮���Ӧ�Ŀ���" + deptMgr.Err);
                    return;
                }
                if ((depttempt.ID == deptment.ID) && (depttempt.Name != deptment.Name))
                {
                    if (MessageBox.Show("�Ѿ����ڱ���Ϊ" + depttempt.ID + "�ļ�¼" + depttempt.Name + "�Ƿ�Ҫ�滻Ϊ:" + deptment.Name + "��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
            }
                #endregion

            //���ؼ��е����ݱ��浽ʵ����
            this.departmentStat.DeptCode = this.txtDeptCode.Text.Trim();//���ұ���
            this.departmentStat.DeptName = this.comboDeptName.Text.Trim();//��������
            this.departmentStat.SpellCode = this.txtSpellCode.Text.Trim();//ƴ����
            this.departmentStat.WBCode = this.txtWbCode.Text.Trim();//�����
            this.departmentStat.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid; //��Ч��־��0 ���á�1ͣ��
            this.departmentStat.SortId = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortID.Text.Trim());//�����
            this.departmentStat.Memo = this.txtMark.Text.Trim();//��ע

            //����ʱ�����û��ƴ�����������룬���Զ�����
            if (this.txtSpellCode.Text.Trim() == "" || this.txtWbCode.Text.Trim() == "")
            {
                CreateSpell();//����ƴ����������
            }

            //������ҷ���������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

            deptMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                //Do���ҷ�����ȱ��棬��������������һ����¼
                int parm = deptStatManager.UpdateDepartmentStat(this.departmentStat);
                if (parm == 1)
                {
                    //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                    string errInfo = "";
                    ArrayList alInfo = new ArrayList();
                    alInfo.Add(this.departmentStat);
                    int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DeptStat, ref errInfo);

                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        Function.ShowMessage("���ҽṹ���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (parm == 0)
                {
                    parm = deptStatManager.InsertDepartmentStat(this.departmentStat);

                    if (parm == 1)
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(this.departmentStat);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DeptStat, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("���ҽṹ���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (parm != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ݱ���ʧ��:" + deptStatManager.Err);
                    return;
                }
                #region ��������ά��
                if (!this.chbClass.Checked)
                {
                    if (deptMgr.Insert(deptment) == -1)
                    {
                        if (deptMgr.DBErrCode == 1)
                        {
                            if (deptMgr.Update(deptment) == -1 || deptMgr.Update(deptment) == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���������Ϣʧ��" + deptMgr.Err);
                                return;
                            }
                            else
                            {
                                //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                                string errInfo = "";
                                ArrayList alInfo = new ArrayList();
                                alInfo.Add(deptment);
                                int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                                if (param == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    Function.ShowMessage("�������ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���������Ϣʧ��" + deptMgr.Err);
                            return;
                        }
                    }
                    else
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(deptment);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Department, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("�������ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                #endregion

            }
            catch (Exception a)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(a.Message);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.FindForm().DialogResult = DialogResult.OK;

            #endregion
        }

        #region ���ݿ�����������ƴ����������

        /// <summary>
        /// ���ݿ�����������ƴ����������
        /// </summary>
        private void CreateSpell()
        {
            if (this.txtSpellCode.Text == "" || this.txtWbCode.Text == "")
            {
                //������������ƴ����������
                FS.HISFC.Models.Base.Spell spell = new FS.HISFC.Models.Base.Spell();

                spell = (FS.HISFC.Models.Base.Spell)mySpell.Get(this.comboDeptName.Text.Trim());
                this.txtSpellCode.Text = spell.SpellCode;
                this.txtWbCode.Text = spell.WBCode;
            }
        }

        #endregion

        #region ȡ���ҷ���������

        /// <summary>
        /// ȡ���ҷ���������
        /// </summary>
        private int GetMaxCode()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            string deptCode = deptManager.GetMaxCode(this.departmentStat.StatCode);
            if (deptCode == "-1")
            {
                MessageBox.Show(deptManager.Err);
                return -1;
            }

            this.txtDeptCode.Text = deptCode;
            return 1;
        }
        #endregion

        #region  ��������ComboBoxSelectedIndexChanged�¼�
        /// <summary>
        /// ��������ComboBoxSelectedIndexChanged�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chbClass.Checked)
            {
                return;
            }
            if (this.comboDeptName.Tag == null)
            {
                return;
            }
            string DeptID = this.comboDeptName.Tag.ToString();
            FS.HISFC.Models.Base.Department objTemp = deptMgr.GetDeptmentById(DeptID);
            if (objTemp == null)
            {
                MessageBox.Show("��ѯ������Ϣʧ��" + deptMgr.Err);
                return;
            }
            setInfo(objTemp);
        }
        #endregion

        private string DeptName = "";

        private void comboDeptName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.chbClass.Checked)
                {
                    if (this.comboDeptName.Text.Trim() == "")
                    {
                        MessageBox.Show("������������ƣ�");
                        return;
                    }
                    this.DeptName = this.comboDeptName.Text;
                    this.chbClass.Checked = false;
                    //if (MessageBox.Show("ȷ��Ҫ����һ��" + this.comboDeptName.Text + "���ҷ�����", "���ӿ��ҷ���", MessageBoxButtons.YesNo) == DialogResult.No)
                    if (MessageBox.Show("ȷ��Ҫ����һ����" + this.DeptName + "�����ҷ�����", "���ӿ��ҷ���", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    //���ݿ�����������ƴ����������
                    this.comboDeptName.Tag = this.comboDeptName.Text;
                    this.txtDeptSimpleName.Focus();
                }
                //ע�͵��Ĵ��� ·־�� 2007-5-29
                //this.chbClass.Checked = true;
                //this.comboDeptName.Text = this.DeptName;
                //this.CreateSpell();
                //if (this.chbClass.Checked)
                //{
                //    this.txtSpellCode.Focus();
                //}
                //else
                //{
                //    if (this.comboDeptName.Tag != null)
                //    {
                //        this.txtDeptCode.Text = this.comboDeptName.Tag.ToString();
                //    }
                //    this.txtDeptSimpleName.Focus();
                //}
            }
            else
            {
                return;
            }
        }

        private void txtDeptSimpleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDeptCode.Focus();
            }
        }

        private void txtDeptCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!isAddDept)
                {
                    this.txtSortID.Focus();
                }
                else
                {
                    this.txtSpellCode.Focus();
                }
            }
        }

        private void txtSpellCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWbCode.Focus();
            }
        }

        private void txtWbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.chbClass.Checked)
                {
                    this.txtSortID.Focus();
                }
                else
                {
                    this.txtUserCode.Focus();
                }
            }
        }

        private void txtUserCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDeptEnglish.Focus();
            }
        }

        private void txtDeptEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtSortID.Focus();
            }
        }

        private void txtSortID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.chbClass.Checked || !isAddDept) //���ҷ��������֯�ṹ 
                {
                    this.txtMark.Focus();
                }
                else
                {
                    this.comboDeptType.Focus();
                }
            }
        }

        private void comboDeptType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.comboDeptPro.Focus();
            }
        }

        private void comboDeptPro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtMark.Focus();
            }
        }

        private void txtMark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.chbClass.Checked || !isAddDept)
                {
                    this.Save();
                }
                else
                {
                    this.chbReg.Focus();
                }
            }
        }

        private void chbReg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.chbTat.Focus();
            }
        }

        private void chbTat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.chbStop.Focus();
            }
        }

        private void chbStop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Save();
            }
        }

    }

    //���ӵĴ������������ӿ��ҵ�ʱ���д�������
    //·־��
    //2007-4-11
    //public class CheckEventArgs : EventArgs
    //{
    //    private string DeptCode;
    //    /// <summary>
    //    /// Ҫ������ҵĿ��ұ���
    //    /// </summary>
    //    public FS.HISFC.Models.Base.Department Message
    //    {
    //        get
    //        {
    //            return DeptCode;
    //        }
    //        set
    //        {
    //            DeptCode = value;
    //        }
    //    }
    //    public CheckEventArgs(string Msg)
    //    {
    //        this.Message = Msg;
    //    }
    //}

}
