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
    /// [��������: ��ԱȨ��ά��]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��4]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPrivUserManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        //��ԱȨ�޷�����ϸ������
        FS.HISFC.BizLogic.Manager.UserPowerDetailManager userMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        //��Ա������
        FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
        //��ԱȨ����ϸʵ����
        FS.HISFC.Models.Admin.UserPowerDetail userPowerDetail = new FS.HISFC.Models.Admin.UserPowerDetail();
        
        /// <summary>
        /// ��ԱȨ����ϸ����
        /// </summary>
        public FS.HISFC.Models.Admin.UserPowerDetail UserPowerDetail
        {
            get
            {
                return this.userPowerDetail;
            }
            set
            {
                this.userPowerDetail = value;
            }
        }
        /// <summary>
        /// �޲ι��캯��
        /// </summary>
        public ucPrivUserManager()
        {
            InitializeComponent();
        }
        /// <summary>
        /// �вι��캯���� ������
        /// </summary>
        /// <param name="userPD"></param>
        public ucPrivUserManager(FS.HISFC.Models.Admin.UserPowerDetail userPD)
        {
            InitializeComponent();
            this.userPowerDetail = userPD;
            init();
        }
        
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void init()
        {
            #region ��ʼ��Ȩ����ϸListView

            //Ȩ�޵ȼ������࣬��Եȼ�3���ȼ�3����1��2�ȼ�����Ϣ
            FS.HISFC.BizLogic.Manager.PowerLevelManager powerManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
          
            //��ʵ���ڴ����µ�����Ȩ��
            ArrayList al = powerManager.LoadLevel3ByLevel1(this.userPowerDetail.Class1Code);//����һ��Ȩ�ޱ���ȡ����2��3��Ȩ����Ϣ
            if (al == null)
            {
                MessageBox.Show(powerManager.Err);
                return;
            }

            ListViewItem lvi;
            //����øĴ����¾��е�����Ȩ����䵽ListView��
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in al)
            {
                //���ò���Ľڵ���Ϣ
                lvi = new ListViewItem();
                lvi.Text = info.Name;

                //Tag���Ա����ҩ������ʵ��
                lvi.Tag = info;

                //����listView���ӽڵ�
                lvi.SubItems.Add(info.PowerLevelClass2.Class2Name);

                //���ز���Ľڵ�
                this.lvPrivList.Items.Add(lvi);
            }
            #endregion

            //ȡ�û��ڴ˴�ӵ�е�����Ȩ�ޣ�����ʾ��ListView�С�
            this.ShowUserPriv();

            //����Ա��Ϣ��ʾ�ڿؼ���
            this.lblCode.Text = this.userPowerDetail.User.ID;
            this.comboName.Text = this.userPowerDetail.User.Name;
            this.lblDeptName.Text = this.userPowerDetail.Dept.Name;
            this.txtMark.Text = this.userPowerDetail.Memo;
            this.comboRole.Tag = this.userPowerDetail.RoleCode;


            //ֻ����������Աʱ�ſ����޸���Ա
            if (this.userPowerDetail.User.ID == "")
            {
                this.btnChooseUser.Enabled = true;
                this.btDelete.Enabled = false;
            }
            else
            {
                this.btnChooseUser.Enabled = false;
                this.btDelete.Enabled = true;
            }

            #region  ��ʼ����Աά����ComboBox
            //�����ʼ��1
            initInfo();
            #endregion 
            
            #region ��Ա������Ϣ
            //�����ʼ��2
            //����û�����Ϊ���򷵻�
            if (this.userPowerDetail.User.ID == "") return;

            //��Աʵ��
            FS.HISFC.Models.Base.Employee person = this.psMgr.GetPersonByID(this.userPowerDetail.User.ID);
            setInfo(person);

              #endregion
        }

        #region �����ʼ��
        /// <summary>
        /// �����ʼ��
        /// </summary>
        void initInfo()
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList depertments = deptMgr.GetDeptNoNurse();//ȡ����ʿվ����Ŀ����б�

            if (depertments == null)
            {
                MessageBox.Show(deptMgr.Err);
                return;
            }
            //��ʼ����������ѡ��
            this.comboDeptType.IsListOnly = true;
            this.comboDeptType.AddItems(depertments);
            
            //��ʼ����Ա����ѡ��
            this.comboPersonType.IsListOnly = true;
            this.comboPersonType.AddItems(FS.HISFC.Models.Base.EmployeeTypeEnumService.List());

            //��ʼ����Ա�Ա�ѡ��
            this.comboPersonSex.IsListOnly = true;
            this.comboPersonSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            //��ʼ������վѡ��
            ArrayList nurseList = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            this.comboPersonNurse.IsListOnly = true;
            this.comboPersonNurse.AddItems(nurseList);

            //��ʼ��ְ��ѡ��
            this.comboPersonDuty.IsListOnly = true;
            this.comboPersonDuty.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.POSITION));//ϵͳ����ö����

            //��ʼ��ְ������ѡ��
            this.comboPersonLevel.IsListOnly = true;
            this.comboPersonLevel.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.LEVEL));

            //��ʼ��ѧ��ѡ��

            this.comboPersonEdu.IsListOnly = true;
            this.comboPersonEdu.AddItems(GetConstant(FS.HISFC.Models.Base.EnumConstant.EDUCATION));

        }
        
        #region ��ȡ��������
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ArrayList GetConstant(FS.HISFC.Models.Base.EnumConstant type)
        {
           FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList constList = consManager.GetList(type);
            if (constList == null)
                throw new FS.FrameWork.Exceptions.ReturnNullValueException();
            return constList;
        }
        #endregion
       #endregion

        #region  �����û������õ��û���Ϣ��ӿؼ�
        /// <summary>
        /// �����û������õ��û���Ϣ��ӿؼ�
        /// </summary>
        /// <param name="person"></param>
        void setInfo(FS.HISFC.Models.Base.Employee person)
        {
            if (person ==null) return;
            //�����û����룭�����ض����
            AddDeptDetial(person.ID);

            this.txtPersonCode.Text = person.ID;//��Ա����
            this.txtPersonName.Text= person.Name;//��Ա����
            this.txtPersonSpellCode.Text = person.SpellCode;//ƴ����
            this.txtPersonWBCode.Text = person.WBCode;//�����
            this.comboPersonSex.Tag = person.Sex.ID;//�Ա�
            if (person.Birthday.ToString() == DateTime.MinValue.ToString())
                this.dtBirthday.Value = DateTime.Now;
            else
                this.dtBirthday.Value = person.Birthday;//��������  
            this.comboPersonEdu.Tag = person.GraduateSchool.ID;//ѧ��,��ҵѧУ
            this.txtPersonIDCard.Text = person.IDCard;//���֤
            this.comboPersonDuty.Tag = person.Duty.ID;//ְ��
            this.comboPersonLevel.Tag = person.Level.ID;//ְ��
            this.comboDeptType.Tag = person.Dept.ID;//��������
            this.comboPersonNurse.Tag = person.Nurse.ID;//��������վ
            this.comboPersonType.Tag = person.EmployeeType.ID;//��Ա����
            this.txtSortID.Text = person.SortID.ToString();//�����

            this.chbCanModifty.Checked = person.IsCanModify;//�Ƿ�����޸�Ʊ��
            this.chbExpert.Checked = person.IsExpert;//�Ƿ�Ϊר��
            this.chbCanFee.Checked = person.IsNoRegCanCharge;//�Ƿ����ֱ���շ�Ȩ��

            //�Ƿ���Ч
            if (person.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                chbValidState.Checked = true;
            }
            else
            {
                chbValidState.Checked = false;
            }
        }
        #endregion

        /// <summary>
        /// ���ض����
        /// </summary>
        private void AddDeptDetial(string EmployeCode)
        {
            if (EmployeCode == "")
            {
                this.neuSpread1_Sheet1.RowCount = 0;
            }
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            
            //������Ա������ȡ����Ա���Ե�½�Ŀ�����Ϣ
            ArrayList list = deMgr.GetMultiDept(EmployeCode);
            foreach (FS.HISFC.Models.Base.DepartmentStat info in list)
            {
               this.neuSpread1_Sheet1.Rows.Add(0, 1);
               this.neuSpread1_Sheet1.Cells[0, 0].Text = info.DeptName;
            }
        }

        #region  ȡ�û��ڴ˴�ӵ�е�����Ȩ�ޣ�����ʾ��ListView�С�
        /// <summary>
        /// ȡ�û��ڴ˴�ӵ�е�����Ȩ�ޣ�����ʾ��ListView�С�
        /// </summary>
        public void ShowUserPriv()
        {
            this.lvPrivList.BeginUpdate();
            this.ClearListView();
            //ȡ�û��ڴ˴�ӵ�е�����Ȩ��
            ArrayList al = this.userMgr.LoadByUserCode(this.userPowerDetail.User.ID, this.userPowerDetail.Class1Code, this.userPowerDetail.Dept.ID);
            if (al == null)
            {
                MessageBox.Show(this.userMgr.Err);
                return;
            }
            //��Ȩ����ϸ�б�����ʾ��ǰ�û�ӵ�е�Ȩ��
            FS.HISFC.Models.Admin.PowerLevelClass3 class3;
            foreach (FS.HISFC.Models.Admin.UserPowerDetail info in al)
            {
                //��ListView�в�����ͬ����Ŀ������checked������Ϊtrue
                foreach (ListViewItem lvi in this.lvPrivList.Items)
                {
                    class3 = lvi.Tag as FS.HISFC.Models.Admin.PowerLevelClass3;
                    if (class3.Class3Code == info.PowerLevelClass.Class3Code && class3.Class2Code == info.Class2Code)
                    {
                        lvi.Checked = true;
                    }
                }
            }
            this.lvPrivList.EndUpdate();
        }
        #endregion
        /// <summary>
        /// ���ListView�е�checked����
        /// </summary>
        public void ClearListView()
        {
            foreach (ListViewItem lvi in this.lvPrivList.Items)
            {
                lvi.Checked = false;
            }
        }

        private void btnChooseUser_Click(object sender, EventArgs e)
        {
            ChooseUser();
        }

        #region ѡ����Ա
        /// <summary>
        /// ѡ����Ա
        /// </summary>
        /// <param name="al"></param>
        private void GetPersonObj(ArrayList al)
        {
            FS.FrameWork.WinForms.Forms.frmEasyChoose form = new FS.FrameWork.WinForms.Forms.frmEasyChoose();
            form.InitData(al);
            form.SelectedItem+=new FS.FrameWork.WinForms.Forms.SelectedItemHandler(form_SelectedItem);
            form.ShowDialog();
        }
        /// <summary>
        /// ѡ����Ա�¼�
        /// </summary>
        /// <param name="neuObj">��Ա��Ϣ</param>
        private void form_SelectedItem(FS.FrameWork.Models.NeuObject neuObj)
        {
            //ȡ�������ݵı��������
            this.lblCode.Text = neuObj.ID;
            this.comboName.Text = neuObj.Name;
            FS.HISFC.Models.Base.Employee objPer = this.psMgr.GetPersonByID(neuObj.ID);
            if (objPer == null)
            {
                MessageBox.Show("��ѯ��Ա��Ϣʧ��");
                return;
            }

            this.setInfo(objPer);

            #region Ĭ��ѡ�ж����
            foreach (ListViewItem lvi in this.lvPrivList.Items)
            {
                FS.HISFC.Models.Admin.PowerLevelClass3 class3 = lvi.Tag as FS.HISFC.Models.Admin.PowerLevelClass3;
                if (class3.Class3Code == "01" && class3.Class2Code == "0000")
                {
                    lvi.Checked = true;
                }
            }
            #endregion
            this.txtMark.Focus();
        }

        /// <summary>
        /// ѡ����Ա
        /// </summary>
        private void ChooseUser()
        {
            //ȡ��ͳ�Ʒ������治���ڵ���Ա
            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList al = person.GetEmployeeForStat(this.userPowerDetail.Class1Code, this.userPowerDetail.Dept.ID);
            if (al == null)
            {
                MessageBox.Show(person.Err);
                return;
            }
            GetPersonObj(al);
            //FS.FrameWork.Models.NeuObject neuObj = null; 
            //if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref neuObj) == 0) return;

            ////ȡ�������ݵı��������
            //this.lblCode.Text = neuObj.ID;
            //this.comboName.Text = neuObj.Name;
            //FS.HISFC.Models.Base.Employee objPer = this.psMgr.GetPersonByID(neuObj.ID);
            //if (objPer == null)
            //{
            //    MessageBox.Show("��ѯ��Ա��Ϣʧ��");
            //    return;
            //}

            //this.setInfo(objPer);

            //#region Ĭ��ѡ�ж����
            //foreach (ListViewItem lvi in this.lvPrivList.Items)
            //{
            //    FS.HISFC.Models.Admin.PowerLevelClass3 class3 = lvi.Tag as FS.HISFC.Models.Admin.PowerLevelClass3;
            //    if (class3.Class3Code == "01" && class3.Class2Code == "0000")
            //    {
            //        lvi.Checked = true;
            //    }
            //}
            //#endregion
            //this.txtMark.Focus();
        }
        #endregion

        #region ��֤����
        /// <summary>
        ///  ��֤����
        /// </summary>
        /// <returns></returns>
        private bool ValidateState()
        {
            if (this.lblCode.Text == "")
            {
                MessageBox.Show("��ѡ��Ҫ��ӵ���Ա��", "��ʾ", MessageBoxButtons.OK);
                return false;
            }
            if(this.lvPrivList.CheckedItems.Count==0)
            {
                MessageBox.Show("��ѡ��Ҫ�����Ȩ�ޣ�", "��ʾ", MessageBoxButtons.OK);
                return false;
            }
            if (this.txtPersonCode.Text.Trim() == "")
            {
                MessageBox.Show("��Ա���벻��Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtPersonCode.Focus();
                return false;
            }
            if(FS.FrameWork.Public.String.ValidMaxLengh(this.txtPersonCode.Text,6)==false)
            {
                MessageBox.Show("��Ա����������뱣����λ�ַ���", "��ʾ", MessageBoxButtons.OK);
                this.txtPersonCode.Focus();
                return false;
            }
            if(this.txtPersonName.Text.Trim()=="")
            {
                MessageBox.Show("��Ա��������λ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.txtPersonName.Focus();
                return false;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPersonName.Text, 100) == false)// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
            {
                MessageBox.Show("��Ա�����������뱣����ʮλ���ֻ�һ��λ�ַ���", "��ʾ", MessageBoxButtons.OK);
                this.txtPersonName.Focus();
                return false;
            }
            if(this.comboPersonSex.Text=="")
            {
                MessageBox.Show("��Ա�Ա���λ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboPersonSex.Focus();
                return false;
            }
            if(this.comboPersonDuty.Text=="")
            {
                MessageBox.Show("ְ����Ų���λ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboPersonDuty.Focus();   
                return false;
            }
            if(this.comboDeptType.Text=="")
            {
                MessageBox.Show("�������Ҳ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK);
                this.comboDeptType.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region  ��ҳ��������
        /// <summary>
        /// ��ҳ��������
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Employee getPerson()
        {
            //by cube 2011-07-05 ���ҽṹά������Ա����ά��ʹ�ò�ͬ�ؼ�������Ϣ�޸���ɲ�һ������
            //FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
            FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee employee = personMgr.GetPersonByID(this.txtPersonCode.Text.Trim());
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            //end by


            employee.ID = this.txtPersonCode.Text.Trim();//Ա������
            employee.Name = this.txtPersonName.Text.Trim();//Ա������
            employee.SpellCode = this.txtPersonSpellCode.Text.Trim();//ƴ����
            employee.WBCode = this.txtPersonWBCode.Text.Trim();//�����
            employee.Sex.ID = this.comboPersonSex.Tag;//�Ա�
            employee.Birthday = this.dtBirthday.Value;//�������� 
            if (this.comboPersonEdu.Tag == null)
            {
                employee.GraduateSchool.ID = "";
            }
            else
            {
                employee.GraduateSchool.ID = this.comboPersonEdu.Tag.ToString() ;//ѧ��
            }
            employee.IDCard = this.txtPersonIDCard.Text.Trim();//���֤
            if (this.comboPersonDuty.Tag == null)
            {
                employee.Duty.ID= "";
            }
            else
            {
                employee.Duty.ID = this.comboPersonDuty.Tag.ToString();//ְ�����
            }
            if (this.comboPersonLevel.Tag == null)
            {
                employee.Level.ID = "";
            }
            else
            {
                employee.Level.ID = this.comboPersonLevel.Tag.ToString();//ְ������
            }
            if (this.comboDeptType.Tag ==null)
            {
                employee.Dept.ID = "";
            }
            else
            {
                employee.Dept.ID = this.comboDeptType.Tag.ToString();//��������
            }
            if (this.comboPersonNurse.Tag == null)
            {
                employee.Nurse.ID = "";
            }
            else
            {
                employee.Nurse.ID = this.comboPersonNurse.Tag.ToString();//��������վ
            }
            employee.EmployeeType.ID = this.comboPersonType.Tag.ToString();//��Ա����
            employee.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortID.Text);//˳���
            
                employee.IsCanModify = this.chbCanModifty.Checked;//�ܸ�Ʊ��
            
                employee.IsExpert = this.chbExpert.Checked;//�Ƿ�Ϊר��
            
                employee.IsNoRegCanCharge = this.chbCanFee.Checked;//�Ƿ�ֱ���շ�
            
          
            if (this.chbValidState.Checked)//�Ƿ���Ч
            {
                employee.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
            }
            else
            {
                employee.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            }

            return employee;
        }
        #endregion

        #region ���淽��
        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            this.lblCode.Text = this.txtPersonCode.Text;
            this.comboName.Text = this.txtPersonName.Text;
            if (ValidateState())
            {
                FS.HISFC.Models.Base.Employee objEmployee = this.getPerson();
                
                if (objEmployee == null)
                {
                    MessageBox.Show("��ȡ��Ա��Ϣʧ�ܣ�");
                    return;
                }
                this.userPowerDetail.GrantFlag = true;
                this.userPowerDetail.Memo = this.txtMark.Text;//��ע
                this.userPowerDetail.User.ID = this.lblCode.Text;//��Ա����
                this.userPowerDetail.User.Name = this.comboName.Text;//��Ա����

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.userMgr.Connection);
                //trans.BeginTransaction();
                this.userMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//��ԱȨ�޷�����ϸ������-���õ�ǰ����
                this.psMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//��Ա������-���õ�ǰ����
                try
                {
                    #region ��Ա��Ϣά��
                    if (this.psMgr.Update(objEmployee)<=0)
                    {
                        MessageBox.Show(this.psMgr.Err);
                        
                        if (this.psMgr.Insert(objEmployee)<=0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("������Ա��Ϣʧ�ܣ�"+this.psMgr.Err);
                            return;
                        }
                        else
                        {
                            //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                            string errInfo = "";
                            ArrayList alInfo = new ArrayList();
                            alInfo.Add(objEmployee);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                return;
                            }

                        }
                    }
                    else
                    {
                        //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                        string errInfo = "";
                        ArrayList alInfo = new ArrayList();
                        alInfo.Add(objEmployee);
                        int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Employee, ref errInfo);

                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            Function.ShowMessage("��Ա���ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                            return;
                        }

                    }
                    #endregion

                    //ɾ���û��ڴ˴���Ȩ��
                    if (this.userMgr.Delete(this.userPowerDetail.User.ID, this.userPowerDetail.Class1Code, this.userPowerDetail.Dept.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(this.userMgr.Err);
                        return;
                    }
                    foreach(ListViewItem lvi in this.lvPrivList.CheckedItems)
                    {
                        FS.HISFC.Models.Admin.PowerLevelClass3 level3 = lvi.Tag as FS.HISFC.Models.Admin.PowerLevelClass3;
                        this.userPowerDetail.Class2Code = level3.Class2Code;
                        this.userPowerDetail.PowerLevelClass.Class3Code = level3.Class3Code;
                        //�����û�Ȩ���б�
                        if (this.userMgr.InsertUserPowerDetail(this.userPowerDetail) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("���ݱ���ʧ�ܣ�"+this.userMgr.Err);
                            return;
                        }
                    }

                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show("���ݱ���ʧ�ܣ�"+e.Message,"��ʾ");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���ݱ���ɹ���");
                this.FindForm().DialogResult = DialogResult.OK;

            }

        }
        #endregion

        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        private void Delete()
        {

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

           //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.userMgr.Connection);
           // trans.BeginTransaction();
            userMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                //ɾ�����û��ڴ˴�ӵ�е�Ȩ��
                int parm = userMgr.Delete(this.userPowerDetail.User.ID, this.userPowerDetail.Class1Code, this.userPowerDetail.Dept.ID);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(userMgr.Err);
                    return;
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("ɾ����Ա����ʧ�ܣ�" + e.Message, "��ʾ");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.FindForm().DialogResult = DialogResult.OK;
            MessageBox.Show("ɾ����Ա�ɹ���");
        }
        #endregion

        private void btCancle_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void txtPersonName_TextChanged(object sender, EventArgs e)
        {
            this.comboName.Text = this.txtPersonName.Text;
        }

        private void txtPersonCode_TextChanged(object sender, EventArgs e)
        {
            this.lblCode.Text = this.txtPersonCode.Text;
        }

        private void comboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblCode.Text = this.comboName.Tag.ToString();
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 1)
            {
                this.comboName.Focus();
            }
        }

        private void txtPersonName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(Char)13)
            System.Windows.Forms.SendKeys.Send("{Tab}");
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            //���ñ��淽��
            this.Save();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (this.userPowerDetail.User.ID == "") return;
            if(MessageBox.Show("ȷ��Ҫɾ������Աô��","ɾ����ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            //����ɾ������
            this.Delete();
        }

        /// <summary>
        /// ���ȫѡ����  {247981BF-D6A5-465d-9AC5-66C41EE1F099}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxcheckAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in this.lvPrivList.Items)
            {
                lvi.Checked = cbxcheckAll.Checked;
            }

        }

    }
}
