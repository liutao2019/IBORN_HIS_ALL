using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material
{
    public partial class ucMatInOutBase : UserControl
    {
        public ucMatInOutBase()
        {
            InitializeComponent();
        }

        public delegate void DataChangedHandler(FS.FrameWork.Models.NeuObject changeData, object param);

        public delegate void FpKeyHandler(Keys key);

        public event DataChangedHandler BeginTargetChanged;

        public event DataChangedHandler EndTargetChanged;

        public event DataChangedHandler BeginPersonChanged;

        public event DataChangedHandler EndPersonChanged;

        public event DataChangedHandler BeginPrivChanged;

        public event DataChangedHandler EndPrivChanged;

        public event FpKeyHandler FpKeyEvent;

        #region �����

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        private string filePath = "";

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject class2Priv = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰѡ��Ĳ�������
        /// </summary>
        private FS.FrameWork.Models.NeuObject privType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ŀ�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject targetDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ŀ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject targetPerson = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���������ļ��ڵ�Ŀ�굥λ��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject localTargetDept = null;

        /// <summary>
        /// ���������ļ��ڵ�Ŀ����Ա��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject localTargetPerson = null;

        /// <summary>
        /// �Ƿ��FarPoint��ʾ������� 
        /// </summary>
        private bool isClear = true;

        /// <summary>
        /// Ȩ�޼���(����Ȩ�� �û���������)
        /// </summary>
        //private ArrayList privList = new ArrayList();
        List<FS.FrameWork.Models.NeuObject> privList = new List<FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// Ŀ�����
        /// </summary>
        ArrayList alTargetDept = new ArrayList();

        /// <summary>
        /// Ŀ��������
        /// </summary>
        ArrayList alTargetPerson = new ArrayList();

        /// <summary>
        /// �˳�ʱ�Ƿ񱣴�Ȩ������
        /// </summary>
        private bool isSaveDefaultPriv = true;

        /// <summary>
        /// ������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        private bool isSetDefaultTargetDept = true;

        /// <summary>
        /// ��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        private bool isSetDefaultTargetPerson = false;

        /// <summary>
        /// Ȩ�޹�����
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();


        #endregion

        #region �ⲿ�������� ����������

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        [Description("������ʾ��ʾ��Ϣ"), Category("����")]
        public string ShowInfo
        {
            get
            {
                return this.lbInfo.Text;
            }
            set
            {
                this.lbInfo.Text = value;
            }
        }


        /// <summary>
        /// �Ƿ�����Բ������Ŀ�굥λ����ѡ����Ӧ
        /// </summary>
        [Description("�Ƿ����ѡ���������Ŀ�굥λ"), Category("����")]
        public bool LabelValid
        {
            get
            {
                return this.lnbTarget.Enabled;
            }
            set
            {
                this.lnbTarget.Enabled = value;         //Ŀ�굥λ
                this.lnbTargetPerson.Enabled = value;         //������
            }
        }


        /// <summary>
        /// �����ļ�·������
        /// </summary>
        [Description("�����ļ�����·�� ��Ե�ַ(��������Ŀ¼��"), Category("����")]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }


        /// <summary>
        /// ��Ŀ�굥λ����ʾ����
        /// </summary>
        [Description("��Ŀ�굥λ����ʾ����"), Category("����")]
        public string LinkLabelTarget
        {
            get
            {
                return this.lnbTarget.Text;
            }
            set
            {
                this.lnbTarget.Text = value;
            }
        }


        /// <summary>
        /// �������˵���ʾ����
        /// </summary>
        [Description("�������˵���ʾ����"), Category("����")]
        public string LinkLabelPerson
        {
            get
            {
                return this.lnbTargetPerson.Text;
            }
            set
            {
                this.lnbTargetPerson.Text = value;
            }
        }


        /// <summary>
        /// �˳�ʱ�Ƿ񱣴�Ȩ������
        /// </summary>
        [Description("�˳�ʱ�Ƿ񱣴�Ȩ������"), Category("����"), DefaultValue(true)]
        public bool IsSaveDefaultPriv
        {
            get
            {
                return this.isSaveDefaultPriv;
            }
            set
            {
                this.isSaveDefaultPriv = value;
            }
        }


        /// <summary>
        /// ������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        [Description("������λ����Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ"), Category("����"), DefaultValue(true)]
        public bool IsSetDefaultTargetDept
        {
            get
            {
                return isSetDefaultTargetDept;
            }
            set
            {
                isSetDefaultTargetDept = value;
            }
        }


        /// <summary>
        /// ��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ
        /// </summary>
        [Description("��ҩ������Ϊ��ʱ �Ƿ��Զ�����Ϊ��һ��ֵ"), Category("����"), DefaultValue(false)]
        public bool IsSetDefaultTargetPerson
        {
            get
            {
                return isSetDefaultTargetPerson;
            }
            set
            {
                isSetDefaultTargetPerson = value;
            }
        }

        #endregion

        #region �ڲ�ʹ������ ������ʹ��

        /// <summary>
        /// �Ƿ���ʾ�ϲ���������Panel
        /// </summary>
        internal bool IsShowInputPanel
        {
            get
            {
                return this.panelItemManager.Visible;
            }
            set
            {
                this.panelItemManager.Visible = value;
            }
        }


        /// <summary>
        /// �Ƿ���ʾ��Ŀѡ��Panel
        /// </summary>
        internal bool IsShowItemSelectpanel
        {
            get
            {
                return this.panelItemSelect.Visible;
            }
            set
            {
                this.panelItemSelect.Visible = value;
            }
        }


        /// <summary>
        /// ����Ȩ��
        /// </summary>
        internal FS.FrameWork.Models.NeuObject Class2Priv
        {
            get
            {
                return this.class2Priv;
            }
            set
            {
                this.class2Priv = value;
            }
        }


        /// <summary>
        /// ������  ID ���� Name ����
        /// </summary>
        internal FS.FrameWork.Models.NeuObject PrivType
        {
            get
            {
                return this.privType;
            }
            set
            {
                this.privType = value;
                if (value != null)
                    this.cmbPrivType.Text = value.Name;
            }
        }


        /// <summary>
        /// Ŀ�굥λ ID ���� Name ���� Memo 0 Ŀ�굥λΪ�ڲ����� 1 Ŀ�굥λΪ�ⲿ������˾
        /// </summary>
        internal FS.FrameWork.Models.NeuObject TargetDept
        {
            get
            {
                return targetDept;
            }
            set
            {
                this.targetDept = value;
                this.cmbTargetDept.Text = this.targetDept.Name;
            }
        }


        /// <summary>
        /// Ŀ���� ID ���� Name ����
        /// </summary>
        internal FS.FrameWork.Models.NeuObject TargetPerson
        {
            get
            {
                return this.targetPerson;
            }
            set
            {
                this.targetPerson = value;
                this.cmbTargetPerson.Text = this.targetPerson.Name;
            }
        }


        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        internal FS.FrameWork.Models.NeuObject OperInfo
        {
            get
            {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }


        /// <summary>
        /// ��ǰ��½����
        /// </summary>
        internal FS.FrameWork.Models.NeuObject DeptInfo
        {
            get
            {
                return this.deptInfo;
            }
            set
            {
                this.deptInfo = value;
            }
        }


        /// <summary>
        /// �Ƿ��FarPoint��ʾ������� 
        /// </summary>
        internal bool IsClear
        {
            get
            {
                return this.isClear;
            }
            set
            {
                this.isClear = value;
            }
        }


        /// <summary>
        /// �ܽ����ʾ
        /// </summary>
        internal string TotCostInfo
        {
            get
            {
                return this.lbTotCost.Text;
            }
            set
            {
                this.lbTotCost.Text = value;
            }
        }

        #endregion

        /// <summary>
        /// �����Ŀ��ʾ¼��UC
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddItemInputUC(System.Windows.Forms.UserControl ucItemInput)
        {
            if (ucItemInput == null)
            {
                this.panelItemManager.Visible = false;
            }
            else
            {
                this.panelItemManager.Controls.Clear();

                this.panelItemManager.Controls.Add(ucItemInput);

                this.panelItemManager.Size = ucItemInput.Size;

                ucItemInput.Dock = DockStyle.Fill;
            }

            return 1;
        }

        /// <summary>
        /// ����Ŀ�����
        /// </summary>
        /// <param name="isCompany">Ŀ�굥λ�Ƿ�Ϊ������˾ True ������˾ False Ժ�ڿ��� ��ΪTrueʱʣ�����������</param>
        /// <param name="isPrivInOut">�Ƿ�ά�������������б� ��ΪTrueʱ �������Ͳ���������</param>
        /// <param name="companyType">������˾���� ����/ҩƷ/�豸</param>
        /// <param name="deptType">Ժ�ڿ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetTargetDept(bool isCompany, bool isPrivInOut, FS.HISFC.Models.IMA.EnumModuelType companyType, string deptType)
        {
            this.alTargetDept.Clear();

            this.lnbTarget.Visible = true;
            this.cmbTargetDept.Visible = true;

            if (isCompany)
            {
                #region ���ع�����˾  ������ʱʹ��ҩƷ�Ĺ�����λ���в���

                switch (companyType)
                {
                    case FS.HISFC.Models.IMA.EnumModuelType.Equipment:         //�豸
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Material:          //����
                        FS.HISFC.BizLogic.Material.ComCompany matCompany = new FS.HISFC.BizLogic.Material.ComCompany();
                        this.alTargetDept = matCompany.QueryCompany("1", "A");
                        if (this.alTargetDept == null)
                        {
                            MessageBox.Show("�������ʹ�����˾�б�ʧ��" + matCompany.Err);
                            return -1;
                        }
                        break;
                    //case FS.HISFC.Models.IMA.EnumModuelType.Phamacy:			 //ҩƷ
                    //    FS.HISFC.BizLogic.Pharmacy.Constant phaConstantManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    //    this.alTargetDept = phaConstantManager.GetCompany("1");
                    //    if (this.alTargetDept == null)
                    //    {
                    //        MessageBox.Show("����ҩƷ������˾�б�ʧ��" + phaConstantManager.Err);
                    //        return -1;
                    //    }
                    //    break;
                }

                foreach (FS.FrameWork.Models.NeuObject info in this.alTargetDept)
                {
                    info.Memo = "1";
                }

                #endregion
            }
            else
            {
                if (isPrivInOut)        //Ȩ�޿���
                {
                    #region ȡ���Ȩ�޿���

                    ArrayList tempAl;
                    FS.HISFC.BizLogic.Material.Plan privInOutManager = new FS.HISFC.BizLogic.Material.Plan();
                    tempAl = privInOutManager.GetPrivInOutDeptList(this.deptInfo.ID, this.class2Priv.ID);
                    if (tempAl == null)
                    {
                        MessageBox.Show(privInOutManager.Err);
                        return -1;
                    }
                    //��privInOutDeptת��Ϊneuobject�洢
                    //FS.FrameWork.Models.NeuObject offerInfo;
                    FS.HISFC.Models.Base.DepartmentStat offerInfo;
                    FS.HISFC.Models.Base.PrivInOutDept privInOutDept;
                    for (int i = 0; i < tempAl.Count; i++)
                    {
                        privInOutDept = tempAl[i] as FS.HISFC.Models.Base.PrivInOutDept;
                        offerInfo = new FS.HISFC.Models.Base.DepartmentStat();
                        offerInfo.ID = privInOutDept.Dept.ID;				//������λ����
                        offerInfo.Name = privInOutDept.Dept.Name;		    //������λ����
                        offerInfo.Memo = privInOutDept.Memo;		    //��ע
                        offerInfo.SpellCode = privInOutDept.User01;
                        offerInfo.WBCode = privInOutDept.User02;
                        offerInfo.UserCode = privInOutDept.User03;
                        this.alTargetDept.Add(offerInfo);
                    }

                    #endregion
                }
                else
                {
                    #region ���ݿ�������ȡԺ�ڿ���

                    FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
                    this.alTargetDept = managerIntegrate.GetDeptment(deptType);
                    if (this.alTargetDept == null)
                    {
                        MessageBox.Show("���ݿ�������ȡ�����б�������" + managerIntegrate.Err);
                        return -1;
                    }
                    foreach (FS.FrameWork.Models.NeuObject info in this.alTargetDept)
                    {
                        info.Memo = "0";
                        //info.User01 = mySpell.Get(info.Name).SpellCode;
                    }

                    #endregion
                }
            }

            if (this.isSetDefaultTargetDept)
            {
                if (this.targetDept.ID == "")
                {
                    if (this.alTargetDept.Count > 0)
                    {
                        if (this.localTargetDept != null && this.localTargetDept.ID != "")
                        {
                            this.TargetDept = this.localTargetDept;
                            this.localTargetDept = null;
                        }
                        else
                        {
                            this.TargetDept = this.alTargetDept[0] as FS.FrameWork.Models.NeuObject;
                        }
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="isAllEmployee">������Ա ����ΪTrueʱ employeeType����������</param>
        /// <param name="personType">��Ա���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetTargetPerson(bool isAllEmployee, FS.HISFC.Models.Base.EnumEmployeeType personType)
        {
            this.alTargetPerson.Clear();

            this.lnbTargetPerson.Visible = true;
            this.cmbTargetPerson.Visible = true;

            FS.HISFC.BizLogic.Manager.Person managerIntegrate = new FS.HISFC.BizLogic.Manager.Person();
            if (isAllEmployee)
                this.alTargetPerson = managerIntegrate.GetEmployeeAll();
            else
                this.alTargetPerson = managerIntegrate.GetEmployee(personType);

            if (this.alTargetPerson == null)
            {
                MessageBox.Show("������Ա����ȡ��Ա�б�������" + managerIntegrate.Err);
                return -1;
            }

            if (this.isSetDefaultTargetPerson)
            {
                if (this.targetPerson.ID == "")
                {
                    if (this.alTargetPerson.Count > 0)
                    {
                        if (this.localTargetPerson != null && this.localTargetPerson.ID != "")
                        {
                            this.TargetPerson = this.localTargetPerson;
                            this.localTargetPerson = null;
                        }
                        else
                        {
                            this.TargetPerson = this.alTargetPerson[0] as FS.FrameWork.Models.NeuObject;
                        }
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// ����Ȩ�����
        /// </summary>
        /// <param name="isUseLocal">�Ƿ�Ĭ��Ϊ��һ��ѡ���Ȩ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SetPrivType(bool isUseLocal)
        {
            if (this.privList.Count == 0)
            {
                if (this.InitPrivType() == -1)
                {
                    return -1;
                }
            }

            this.FilterPriv(ref this.privList);

            if (this.privList.Count == 0)
            {
                MessageBox.Show("�����κβ���Ȩ�� ������ظ�������ϵ��ȡ��Ȩ");
                return -1;
            }

            if (isUseLocal)
            {
                #region ��ȡ���������ļ���ȡ��һ��ѡ���Ȩ��

                try
                {
                    if (System.IO.File.Exists(Application.StartupPath + this.filePath))
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(Application.StartupPath + this.filePath);
                        System.Xml.XmlNode operNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']/OperInfo[@OperID = '" + this.operInfo.ID + "']");
                        if (operNode != null)
                        {
                            #region �ҵ��ò���Ա��Ϣ

                            string xmlPrivId = operNode.ChildNodes[0].Attributes["ID"].Value.ToString();
                            foreach (FS.FrameWork.Models.NeuObject temp in this.privList)
                            {
                                if (temp.ID == xmlPrivId)
                                {
                                    //����Ա�Ծ����ϴε����Ȩ��  ���ò���������ʾ
                                    this.PrivType = temp;

                                    //���ù�����λ
                                    this.targetDept.ID = operNode.ChildNodes[1].Attributes["ID"].Value.ToString();
                                    this.targetDept.Name = operNode.ChildNodes[1].Attributes["Name"].Value.ToString();
                                    this.cmbTargetDept.Text = this.targetDept.Name;
                                    //���汾�������ļ��ڵĹ�����λ��Ϣ
                                    this.localTargetDept = this.targetDept;

                                    this.targetPerson.ID = operNode.ChildNodes[2].Attributes["ID"].Value.ToString();
                                    this.targetPerson.Name = operNode.ChildNodes[2].Attributes["Name"].Value.ToString();
                                    this.cmbTargetPerson.Text = this.targetPerson.Name;
                                    //���汾�������ļ��ڵ���������Ϣ 
                                    this.localTargetPerson = this.targetPerson;

                                    return 1;
                                }
                            }
                            #endregion

                            if (this.PrivType.ID == "")
                            {
                                FS.FrameWork.Models.NeuObject info = this.privList[0] as FS.FrameWork.Models.NeuObject;
                                this.PrivType = info;
                            }
                        }
                        else
                        {
                            FS.FrameWork.Models.NeuObject info = this.privList[0] as FS.FrameWork.Models.NeuObject;
                            this.PrivType = info;
                        }
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject info = this.privList[0] as FS.FrameWork.Models.NeuObject;
                        this.PrivType = info;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                #endregion
            }
            else
            {
                FS.FrameWork.Models.NeuObject info = this.privList[0] as FS.FrameWork.Models.NeuObject;
                this.PrivType = info;
            }

            return 1;
        }

        private ArrayList ArrayList(FS.FrameWork.Models.NeuObject[] neuObject)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #region Ȩ�޳�ʼ��������

        /// <summary>
        /// ��ʼ��Ȩ������
        /// </summary>
        /// <returns>�ɹ�����Ȩ��ѡ�񷵻�1 ���򷵻�-1</returns>
        protected int InitPrivType()
        {
            #region ��Ч���ж�

            if (this.Class2Priv.ID == "")
            {
                MessageBox.Show("δ��ȡ��ǰ�����Ķ���Ȩ�ޱ���");
                return -1;
            }
            if (this.DeptInfo.ID == "")
            {
                MessageBox.Show("δ��ȡ��ǰ�������ұ���");
                return -1;
            }
            if (this.OperInfo.ID == "")
            {
                MessageBox.Show("δ��ȡ��ǰ����Ա����");
                return -1;
            }

            #endregion

            #region ��ȡ��ǰ����Ա���е�Ȩ�޼���

            this.privList = this.powerDetailManager.QueryUserPrivCollection(this.OperInfo.ID, this.Class2Priv.ID, this.DeptInfo.ID);

            if (this.privList == null)
            {
                MessageBox.Show("��ȡ����Ա����Ȩ�޼���ʱ����\n" + this.powerDetailManager.Err);
                return -1;
            }

            #endregion

            #region ��ȡ����Ȩ�޺�����

            FS.HISFC.Models.Admin.PowerLevelClass3 privClass3;

            FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            foreach (FS.FrameWork.Models.NeuObject info in this.privList)
            {
                privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey(this.Class2Priv.ID, info.ID);
                if (privClass3 == null)
                {
                    MessageBox.Show("��ȡ����Ȩ�޺��������\n��������Ȩ�޺��������" + powerLevelManager.Err);
                    return -1;
                }
                info.Memo = privClass3.Class3MeaningCode;
            }

            #endregion

            #region �������Ĳ�ͬ���й���

            ArrayList al = new ArrayList();

            FS.FrameWork.Models.NeuObject typeInfo = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject objInfo;
            for (int i = 0; i < this.privList.Count; i++)
            {
                typeInfo = this.privList[i] as FS.FrameWork.Models.NeuObject;
                objInfo = new FS.FrameWork.Models.NeuObject();
                objInfo.ID = typeInfo.ID;
                objInfo.Name = typeInfo.Name;
                objInfo.Memo = typeInfo.Memo;

                switch (this.OperInfo.Memo)
                {
                    case "apply":
                        if (typeInfo.Memo == "13" || typeInfo.Memo == "18" || typeInfo.Memo == "24" || typeInfo.Memo == "12")
                        {
                            al.Add(typeInfo);
                        }
                        break;
                    case "in":
                        if (typeInfo.Memo != "13" || typeInfo.Memo != "18" || typeInfo.Memo != "12")
                        {
                            al.Add(typeInfo);
                        }
                        break;
                    case "out":
                        if (typeInfo.Memo != "24")
                        {
                            al.Add(typeInfo);
                        }
                        break;
                }
            }
            //this.privList = new ArrayList();
            //this.privList = new List<FS.FrameWork.Models.NeuObject>();
            //this.privList.Clear();
            //this.privList = al;
            #endregion

            return 1;
        }


        /// <summary>
        /// Ȩ�޹���
        /// </summary>
        protected virtual void FilterPriv(ref List<FS.FrameWork.Models.NeuObject> privList)
        {
        }


        /// <summary>
        /// Ȩ��ѡ�� 
        /// </summary>
        /// <returns>1 �ɹ�ѡ����Ȩ�� 0 δѡ���κ�Ȩ�޻�Ȩ��δ�����仯 -1 ��������</returns>
        public int SelectPriv()
        {
            if (this.privList.Count == 0)
                this.InitPrivType();

            FS.FrameWork.Models.NeuObject tempPriv = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(this.privList.ToArray()), ref tempPriv) == 0)
            {
                return 0;
            }
            else
            {
                if (tempPriv.ID == this.privType.ID)
                {
                    return 0;
                }
                else
                {
                    this.privType = tempPriv;
                    return 1;
                }
            }
        }


        /// <summary>
        /// �������Ա���һ��ѡ��Ĳ������͡�Ŀ�굥λд�������ļ�
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int SavePriv()
        {
            try
            {
                if (this.filePath == "")
                {
                    return 1;
                }

                FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlElement root;
                if (System.IO.File.Exists(Application.StartupPath + this.filePath))
                {		//�ļ��Ѵ���
                    doc.Load(Application.StartupPath + this.filePath);
                    root = (System.Xml.XmlElement)doc.SelectSingleNode("Setting");
                }
                else
                {											//�ļ�������
                    //���ø����
                    root = myXml.CreateRootElement(doc, "Setting", "1.0");
                }

                System.Xml.XmlNode deptNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']");
                if (deptNode != null)           //���ڿ��ҽڵ�
                {
                    System.Xml.XmlNode operNode = doc.SelectSingleNode("/Setting/DeptInfo[@DeptID = '" + this.deptInfo.ID + "']/OperInfo[@OperID = '" + this.operInfo.ID + "']");
                    if (operNode != null)       //���ڲ���Ա�ڵ�
                    {
                        //�����������
                        operNode.ChildNodes[0].Attributes["ID"].Value = this.PrivType.ID;
                        operNode.ChildNodes[0].Attributes["Name"].Value = this.privType.Name;
                        //����Ŀ�����
                        operNode.ChildNodes[1].Attributes["ID"].Value = this.targetDept.ID;
                        operNode.ChildNodes[1].Attributes["Name"].Value = this.targetDept.Name;
                        //����������
                        operNode.ChildNodes[2].Attributes["ID"].Value = this.targetPerson.ID;
                        operNode.ChildNodes[2].Attributes["Name"].Value = this.targetPerson.Name;
                    }
                    else                       //�����ڲ���Ա�ڵ�
                    {
                        #region ��Ӳ���Ա�ڵ�

                        //�ڸÿ�����δ�ҵ��ò���Ա��ʷ��Ϣ ��Ӹò���Ա��Ϣ
                        System.Xml.XmlElement xmlNewOper = doc.CreateElement("OperInfo");
                        xmlNewOper.SetAttribute("OperID", this.operInfo.ID);
                        //�����������ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlPriv = doc.CreateElement("PrivType");
                        xmlPriv.SetAttribute("ID", this.privType.ID);
                        xmlPriv.SetAttribute("Name", this.privType.Name);
                        xmlNewOper.AppendChild(xmlPriv);
                        //��ӹ�����λ�ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlOffer = doc.CreateElement("TargetDept");
                        xmlOffer.SetAttribute("ID", this.targetDept.ID);
                        xmlOffer.SetAttribute("Name", this.targetDept.Name);
                        xmlNewOper.AppendChild(xmlOffer);
                        //����������ӽڵ� ����ֵ
                        System.Xml.XmlElement xmlPerson = doc.CreateElement("TargetPerson");
                        xmlPerson.SetAttribute("ID", this.targetPerson.ID);
                        xmlPerson.SetAttribute("Name", this.targetPerson.Name);
                        xmlNewOper.AppendChild(xmlPerson);

                        //���ù���
                        ((System.Xml.XmlElement)deptNode).AppendChild(xmlNewOper);

                        #endregion
                    }
                }
                else
                {
                    #region ��ӿ��ҽڵ�

                    //�ÿ���δ�ҵ� ��Ӹÿ��ҡ��ò���Ա��������Ϣ ����������� ������λ
                    System.Xml.XmlElement xmlNewDept = doc.CreateElement("DeptInfo");
                    xmlNewDept.SetAttribute("DeptID", this.deptInfo.ID);

                    //��Ӹò���Ա��Ϣ
                    System.Xml.XmlElement xmlNewOper1 = doc.CreateElement("OperInfo");
                    xmlNewOper1.SetAttribute("OperID", this.operInfo.ID);
                    //�����������ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlPriv1 = doc.CreateElement("PrivType");
                    xmlPriv1.SetAttribute("ID", this.privType.ID);
                    xmlPriv1.SetAttribute("Name", this.privType.Name);
                    //��ӹ�����λ�ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlOffer1 = doc.CreateElement("TargetDept");
                    xmlOffer1.SetAttribute("ID", this.targetDept.ID);
                    xmlOffer1.SetAttribute("Name", this.targetDept.Name);
                    //����������ӽڵ� ����ֵ
                    System.Xml.XmlElement xmlPerson = doc.CreateElement("TargetPerson");
                    xmlPerson.SetAttribute("ID", this.targetPerson.ID);
                    xmlPerson.SetAttribute("Name", this.targetPerson.Name);

                    xmlNewOper1.AppendChild(xmlPriv1);
                    xmlNewOper1.AppendChild(xmlOffer1);
                    xmlNewOper1.AppendChild(xmlPerson);

                    xmlNewDept.AppendChild(xmlNewOper1);

                    root.AppendChild(xmlNewDept);

                    #endregion
                }

                doc.Save(Application.StartupPath + this.filePath);

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filterData"></param>
        protected virtual void Filter(string filterData)
        {

        }


        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            this.TargetDept = new FS.FrameWork.Models.NeuObject();

            this.TargetPerson = new FS.FrameWork.Models.NeuObject();

            this.ShowInfo = "��ʾ��Ϣ:";

            this.TotCostInfo = "�ܽ��:";

            this.lnbTargetPerson.Visible = false;
            this.cmbTargetPerson.Visible = false;
            this.lnbTarget.Visible = false;
            this.cmbTargetDept.Visible = false;
        }


        /// <summary>
        /// ����TargetPerson����
        /// </summary>
        public void SetPersonFocus()
        {
            this.lnbTargetPerson.Select();
            this.lnbTargetPerson.Focus();
        }


        /// <summary>
        /// ����TargetDept����
        /// </summary>
        public void SetDeptFocus()
        {
            this.lnbTarget.Select();
            this.lnbTarget.Focus();
        }


        /// <summary>
        /// ����¼��б�
        /// </summary>
        private void ClearEvent()
        {
            //���FpKeyEvent���¼�ί���б�
            if (this.FpKeyEvent != null)
            {
                Delegate[] methodDelegate = this.FpKeyEvent.GetInvocationList();
                foreach (Delegate tempDelegate in methodDelegate)
                {
                    ucMatInOutBase.FpKeyHandler tempKeyHandler = (ucMatInOutBase.FpKeyHandler)tempDelegate;

                    this.FpKeyEvent -= tempKeyHandler;
                }
            }
            //���BeginTarget�¼�ί��
            if (this.BeginTargetChanged != null)
            {
                Delegate[] beginTargetdDelegate = this.BeginTargetChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginTargetdDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginTargetChanged -= tempHandler;
                }
            }
            //���EndTarget�¼�ί��
            if (this.EndTargetChanged != null)
            {
                Delegate[] endTargetdDelegate = this.EndTargetChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endTargetdDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.EndTargetChanged -= tempHandler;
                }
            }
            //���BeginPerson�¼�ί��
            if (this.BeginPersonChanged != null)
            {
                Delegate[] beginPersonDelegate = this.BeginPersonChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginPersonDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginPersonChanged -= tempHandler;
                }
            }
            //���EndPerson�¼�ί��
            if (this.EndPersonChanged != null)
            {
                Delegate[] endPersondDelegate = this.EndPersonChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endPersondDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.EndPersonChanged -= tempHandler;
                }
            }
            //���BeginPriv�¼�ί��
            if (this.BeginPrivChanged != null)
            {
                Delegate[] beginPrivDelegate = this.BeginPrivChanged.GetInvocationList();
                foreach (Delegate tempDelegate in beginPrivDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.BeginPrivChanged -= tempHandler;
                }
            }
            //���EndPriv�¼�ί��
            if (this.EndPrivChanged != null)
            {
                Delegate[] endPrivDelegate = this.EndPrivChanged.GetInvocationList();
                foreach (Delegate tempDelegate in endPrivDelegate)
                {
                    ucMatInOutBase.DataChangedHandler tempHandler = (ucMatInOutBase.DataChangedHandler)tempDelegate;

                    this.EndPrivChanged -= tempHandler;
                }
            }
        }


        public void SetCancelVisible(bool vis)
        {
            this.tbCancel.Visible = vis;
            this.panelItemSelect.Visible = vis;
        }

        #endregion

        #region �����¼�

        /// <summary>
        /// Ŀ����Ҹ���ǰ�����¼�
        /// </summary>
        /// <param name="changeData">���ĵ�����</param>
        /// <param name="param">��չ��Ϣ</param>
        protected virtual void OnBeginTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.BeginTargetChanged != null)
                this.BeginTargetChanged(changeData, param);
        }


        /// <summary>
        /// Ŀ����Ҹ��ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndTargetChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.EndTargetChanged != null)
                this.EndTargetChanged(changeData, param);
        }


        /// <summary>
        /// Ŀ����Ա����ǰ�����¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnBeginPersonChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.BeginPersonChanged != null)
                this.BeginPersonChanged(changeData, param);
        }


        /// <summary>
        /// Ŀ����Ա���ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndPersonChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.EndPersonChanged != null)
                this.EndPersonChanged(changeData, param);
        }


        /// <summary>
        /// ������ǰ�����¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnBeginPrivChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.BeginPrivChanged != null)
                this.BeginPrivChanged(changeData, param);
        }


        /// <summary>
        /// �����ĺ󴥷��¼�
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="param"></param>
        protected virtual void OnEndPrivChanged(FS.FrameWork.Models.NeuObject changeData, object param)
        {
            if (this.EndPrivChanged != null)
                this.EndPrivChanged(changeData, param);
        }


        /// <summary>
        /// Fp�ڰ����¼�
        /// </summary>
        /// <param name="key"></param>
        protected virtual void OnFpKey(Keys key)
        {
            if (this.FpKeyEvent != null)
                this.FpKeyEvent(key);
        }

        #endregion

        private void lnbTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //����ѡ�񴰿�
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alTargetDept, ref info) == 0)
            {
                return;
            }
            else
            {
                if (info.ID == this.targetDept.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginTargetChanged(info, null);

                    this.TargetDept = info;

                    this.OnEndTargetChanged(info, null);
                }
            }
        }


        private void lnbPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.alTargetPerson == null)
            {
                FS.HISFC.BizLogic.Manager.Person managerIntegrate = new FS.HISFC.BizLogic.Manager.Person();
                this.alTargetPerson = managerIntegrate.GetEmployeeAll();
            }
            //����ѡ�񴰿�
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alTargetPerson, ref info) == 0)
            {
                return;
            }
            else
            {
                if (info.ID == this.targetPerson.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginPersonChanged(info, null);

                    this.TargetPerson = info;

                    this.OnEndPersonChanged(info, null);
                }
            }
        }


        private void lnbPrivType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.privList.Count == 0)
            {
                this.InitPrivType();
            }
            //����ѡ�񴰿�
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(this.privList.ToArray()), ref info) == 0)
            {
                return;
            }
            else
            {
                if (info.ID == this.privType.ID)		//������Ͳ������仯�򷵻�
                {
                    return;
                }
                else
                {
                    this.OnBeginPrivChanged(info, null);

                    this.ClearEvent();

                    this.PrivType = info;

                    this.OnEndPrivChanged(info, null);
                }
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                this.OnFpKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }


        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter(this.txtFilter.Text);
        }


        #region ������

        /// <summary>
        /// ��������ť��ʾ����
        /// </summary>
        public void SetToolButton(bool isShowApply, bool isShowIn, bool isShowOut, bool isShowDel, bool isShowStock)
        {

            this.tbApplyList.Visible = isShowApply;
            this.tbInList.Visible = isShowIn;
            this.tbOutList.Visible = isShowOut;
            this.tbDel.Visible = isShowDel;
            this.tbStockList.Visible = isShowStock;
        }


        /// <summary>
        /// ���Ӱ�ť
        /// </summary>
        /// <param name="index">������ťλ��</param>
        /// <param name="addButton">������ť</param>
        public void AddToolButton(int index, System.Windows.Forms.ToolBarButton addButton)
        {
            this.neuToolBar1.Buttons.Insert(index, addButton);
        }


        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.tbExit)
            {
                this.OnClose();
            }
            if (e.Button == this.tbSave)
            {
                this.OnSave();
            }
            if (e.Button == this.tbInList)
            {
                this.OnShowInList();
            }
            if (e.Button == this.tbOutList)
            {
                this.OnShowOutList();
            }
            if (e.Button == this.tbApplyList)
            {
                this.OnShowApplyList();
            }
            if (e.Button == this.tbStockList)
            {
                this.OnShowStockList();
            }
            if (e.Button == this.tbDel)
            {
                this.OnDel();
            }
            if (e.Button == this.tbShow)
            {
                this.OnShow();
            }
            if (e.Button == this.tbCancel)
            {
                this.OnCancel();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected virtual int OnCancel()
        {
            return 1;
        }

        /// <summary>
        /// ���ڹر�
        /// </summary>
        protected virtual void OnClose()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected virtual int OnSave()
        {
            return 1;
        }


        /// <summary>
        /// ��ⵥ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnShowInList()
        {
            return 1;
        }


        /// <summary>
        /// ���ⵥ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnShowOutList()
        {
            return 1;
        }


        /// <summary>
        /// ���뵥
        /// </summary>
        /// <returns></returns>
        protected virtual int OnShowApplyList()
        {
            return 1;
        }

        /// <summary>
        /// �ɹ���
        /// </summary>
        /// <returns></returns>
        protected virtual int OnShowStockList()
        {
            return 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        protected virtual int OnDel()
        {
            return 1;
        }

        /// <summary>
        /// ��ʾ���Ƿ���ʾ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnShow()
        {
            return 1;
        }
        #endregion

    }
}
