using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Management;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucCharge_new<br></br>
    /// [��������: סԺ�շ�UC]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: ]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCharge_new : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucCharge_new()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        protected FS.HISFC.Components.Common.Controls.EnumShowItemType itemKind = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected PatientInfo patientInfo = null;

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �������ͬʱ�շ��б�
        /// </summary>
        protected ArrayList patientList = new ArrayList();

        /// <summary>
        /// �Ƿ�ӵ�л�����,�����,��ôסԺ������ؼ�������,���������
        /// </summary>
        protected bool isHavePatientTree = false;

        /// <summary>
        /// �Ƿ���֤��Ŀ��ʱͣ��
        /// </summary>
        protected bool isJudgeValid = false;

        /// <summary>
        /// �Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��
        /// </summary>
        FS.HISFC.Models.Base.MessType messtype = FS.HISFC.Models.Base.MessType.Y;

        /// <summary>
        /// ���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ
        /// </summary>
        EnumDrugStorageDept drugStorageDept = EnumDrugStorageDept.PatientInDept;

        #region ҵ������

        /// <summary>
        /// �������תҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        /// <summary>
        /// ��ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint printInterface = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ָ�����Ŀ��������{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        [Category("�ؼ�����"), Description("���øÿؼ��Ƿ��ڽ����Ϸֽ⸴����Ŀ true�ֽ� false���ֽ�")]
        public bool IsSplitUndrugCombItem
        {
            get
            {
                return this.ucInpatientCharge_new1.IsSplitUndrugCombItem;
            }
            set
            {
                this.ucInpatientCharge_new1.IsSplitUndrugCombItem = value;
            }
        }
        //{F4912030-EF65-4099-880A-8A1792A3B449} ����

        /// <summary>
        /// �Ƿ�ӵ�л�����,�����,��ôסԺ������ؼ�������,���������
        /// </summary>
        public bool IsHavePatientTree 
        {
            get 
            {
                return this.isHavePatientTree;
            }
            set 
            {
                this.isHavePatientTree = value;

                this.ucQueryPatientInfo.Enabled = !this.isHavePatientTree;
            }
        }

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���ص���Ŀ��� All���� Undrug��ҩƷ drugҩƷ")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType ItemKind
        {
            set
            {
                this.itemKind = value;

                this.ucInpatientCharge_new1.������Ŀ��� = this.itemKind;
            }
            get
            {
                return this.ucInpatientCharge_new1.������Ŀ���;
            }
        } 
        /// <summary>
        /// �Ƿ�������ȡ0������Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("���û��߻���Ƿ�������ȡ0������Ŀ true ���� false ������")]
        public bool isChargeZero 
        {
            get 
            {
                return this.ucInpatientCharge_new1.IsChargeZero;
            }
            set 
            {
                this.ucInpatientCharge_new1.IsChargeZero = value;
            }
        }

        /// <summary>
        /// �Ƿ���֤��Ŀ��ʱͣ��
        /// </summary>
        [Category("�ؼ�����"), Description("���û��߻���Ƿ�ʱ��֤��Ŀͣ�� true ��֤ false ����֤")]
        public bool IsJudgeValid 
        {
            get 
            {
                return this.ucInpatientCharge_new1.IsJudgeValid;
            }
            set 
            {
                this.ucInpatientCharge_new1.IsJudgeValid = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.messtype;
            }
            set
            {
                this.messtype = value;
            }
        }
        [Category("�ؼ�����"), Description("����Ϊ���Ƿ���ʾ")]
        public bool IsJudgeQty
        {
            get
            {
                return this.ucInpatientCharge_new1.IsJudgeQty;
            }
            set
            {
                this.ucInpatientCharge_new1.IsJudgeQty = value;
            }
        }
        [Category("�ؼ�����"), Description("ִ�п����Ƿ�Ĭ��Ϊ��½����")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return this.ucInpatientCharge_new1.DefaultExeDeptIsDeptIn;
            }
            set
            {
                this.ucInpatientCharge_new1.DefaultExeDeptIsDeptIn = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ���ʾ�շ�������"), DefaultValue(false)]
        //{7F5CF034-5DCD-4d10-B2FD-CDD02F45E58D}
        public bool IsShowGroupTree
        {
            get
            {
                return this.ucInpatientCharge_new1.IsShowGroupTree;
            }
            set
            {
                this.ucInpatientCharge_new1.IsShowGroupTree = value;
            }
        }

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public PatientInfo PatientInfo 
        {
            set 
            {
                this.patientInfo = value;

                this.SetPatientInfomation();

                this.ucInpatientCharge_new1.PatientInfo = this.patientInfo;

                this.cmbDoct.Focus();
            }
        }

        /// <summary>
        /// ��ǰ�����Ƿ����ģʽ
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");
            }
        }

        [Category("�ؼ�����"), Description("���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ")]
        public EnumDrugStorageDept DrugStorageDept
        {
            get
            {
                return this.ucInpatientCharge_new1.DrugStorageDept;
            }
            set
            {
                this.drugStorageDept = value;
                this.ucInpatientCharge_new1.DrugStorageDept = this.drugStorageDept;
            }
        }
        #endregion

        #region ˽�з���

        /// <summary>
        /// ��û��߻�����Ϣ
        /// </summary>
        private void ucQueryPatientInfo_myEvent()
        {
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty) 
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));
                
                return;
            }

            PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "N")  
            {
                MessageBox.Show(Language.Msg("�û����Ѿ��޷���Ժ���������շ�!"));

                this.Clear();
                this.ucQueryPatientInfo.Focus();

                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "O")
            {
                MessageBox.Show(Language.Msg("�û����Ѿ���Ժ���㣬�������շ�!"));

                this.Clear();
                this.ucQueryPatientInfo.Focus();

                return;
            }
            this.patientInfo = patientTemp;
            
            this.SetPatientInfomation();

            this.ucInpatientCharge_new1.PatientInfo = this.patientInfo;

            this.cmbDoct.Focus();
        }

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        protected virtual void SetPatientInfomation() 
        {
            if (this.patientInfo == null) 
            {
                return;
            }
            
            this.ucQueryPatientInfo.Text = this.patientInfo.PID.PatientNO;
            this.txtName.Text = this.patientInfo.Name;
            this.txtBedNO.Text = (string.IsNullOrEmpty(this.patientInfo.PVisit.PatientLocation.Bed.ID) ? "" : this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4));
            this.txtInTime.Text = this.patientInfo.PVisit.InTime.ToString("d");
            this.txtPact.Text  = this.patientInfo.Pact.Name;
            this.txtInDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            this.cmbDoct.Tag = this.patientInfo.PVisit.AdmittingDoctor.ID;
            this.txtAlarm.Text = this.patientInfo.PVisit.MoneyAlert.ToString();
            this.txtLeft.Text = this.patientInfo.FT.LeftCost.ToString();
            this.txtAge.Text = this.patientInfo.Age;
            //���ʿۿ����
            this.cmbDept.Tag = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        }

        /// <summary>
        /// ��պ���
        /// </summary>
        protected virtual void Clear() 
        {
            this.ucQueryPatientInfo.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtBedNO.Text = string.Empty;
            this.txtInTime.Text = string.Empty;
            this.txtPact.Text = string.Empty;
            this.txtInDept.Text = string.Empty;
            this.cmbDoct.Tag = string.Empty;
            this.txtAlarm.Text = string.Empty;
            this.txtLeft.Text = string.Empty;

            this.ucInpatientCharge_new1.Clear();           

            this.ucQueryPatientInfo.Focus();
        }

        /// <summary>
        /// ���溯��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Save() 
        {
            this.Cursor = Cursors.WaitCursor;
         
            if (this.patientList != null && this.patientList.Count > 0) 
            { 
                int iRow = 0;
                foreach (PatientInfo patient in this.patientList) 
                {
                    this.ucInpatientCharge_new1.PatientInfo = patient;

                    int returnValue = this.ucInpatientCharge_new1.Save(); 
                    if (returnValue < 0)
                    {

                        break; 
                        ///this.Cursor = Cursors.Arrow;
                    }
                    iRow++;

                    this.Print(patient, this.ucInpatientCharge_new1.FeeItemCollection);
                }

                //if (noFeedPatientList.Count > 0) 
                if(iRow <this.patientList.Count)
                {
                    string msg = string.Empty;

                    for(;iRow<this.patientList.Count;iRow++)
                    {
                        PatientInfo patient = (PatientInfo)patientList[iRow];

                        msg += "       "+ patient.Name + " " + "\n";
                    } 
                         msg = "���»���û���շѳɹ� \n" + msg 
                               +"   �뵥������"; 

                    MessageBox.Show(Language.Msg(msg));

                    return -1;
                }

                this.Cursor = Cursors.Arrow;

                MessageBox.Show(this.ucInpatientCharge_new1.SucessMsg);

                this.Clear();

                return 1;
            }
            else
            {
                int returnValue = this.ucInpatientCharge_new1.Save();

                if (returnValue < 0)
                {
                    return -1;
                }
                else 
                {
                    MessageBox.Show(this.ucInpatientCharge_new1.SucessMsg);
                }

                this.Print(this.patientInfo, this.ucInpatientCharge_new1.FeeItemCollection);

                this.Clear(); 

                this.Cursor = Cursors.Arrow;

                return 1;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init() 
        {

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����Ŀ��Ϣ,��ȴ�...");
            Application.DoEvents();
            
            //��ʼ��ҽ����Ϣ
            if (InitDoctors() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            //{7376038F-EFE8-46c8-BA63-3147C6EF67F0}
            //��ʼ��������Ϣ
            if (InitDept() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }

            FS.HISFC.Models.Base.Employee emplTemp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            //��ʼ����Ŀ���ѿؼ�
            if (this.ucInpatientCharge_new1.Init(emplTemp.Dept.ID) == -1) 
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                return -1;
            }
            this.ucInpatientCharge_new1.MessageType = this.MessageType;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// ��ʼ��ҽ����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDoctors()
        {
            ArrayList doctors = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctors == null)
            {
                MessageBox.Show(Language.Msg("����ҽ����Ϣ����!") + this.managerIntegrate.Err);

                return -1;
            }

            this.cmbDoct.AddItems(doctors);

            return 1;
        }
        /// <summary>
        /// ��ʼ��������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        //{7376038F-EFE8-46c8-BA63-3147C6EF67F0}
        private int InitDept()
        {
            ArrayList depts = managerIntegrate.GetDepartment();
            if (depts == null)
            {
                MessageBox.Show(Language.Msg("���ؿ�����Ϣ����!") + this.managerIntegrate.Err);
                return -1;
            }
            this.cmbDept.AddItems(depts);
            return 1;
        }

        /// <summary>
        /// ���ݴ�ӡ
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeItemCollection"></param>
        protected void Print(FS.HISFC.Models.RADT.PatientInfo patient,List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemCollection)
        {
            if (this.printInterface == null)
            {
                this.printInterface = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint;
            }

            if (this.printInterface != null)
            {
                this.printInterface.Patient = patient;
                this.printInterface.SetData(feeItemCollection);

                this.printInterface.Print();
            }
            else
            {
                return;
                //MessageBox.Show(Language.Msg("��ӡ�ӿ�δ��������,�����е��ݴ�ӡ"));
            }
        }

        #endregion

        #region �ؼ�����
        
        /// <summary>
        /// ����ToolBar�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("����", "����һ����Ŀ¼����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��һ��¼�����Ŀ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
            toolBarService.AddToolButton("��������", "���滼���շ�����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z��������, true, false, null);
            toolBarService.AddToolButton("��������", "���뻼�ߵ��շ�����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H����, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// �Զ��尴ť���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Clear();
                    break;
                case "����":
                    this.ucInpatientCharge_new1.AddRow();
                    break;
                case "ɾ��":
                    this.ucInpatientCharge_new1.DelRow();
                    break;
                //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
                case "��������":
                    {
                        this.ucInpatientCharge_new1.ExpFeeGroup();
                        break;
                    }
                case "��������":
                    {
                        this.ucInpatientCharge_new1.SaveFeeGroup();
                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.LoadFrom(@"Report.dll");

            object uc = a.CreateInstance("Report.InpatientFee.ucPatientFeeQuery");
            
            if (uc != null) 
            {
                FS.FrameWork.WinForms.Classes.Function.ShowControl((Control)uc);
            }

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                this.ucQueryPatientInfo.Focus();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void ucCharge_new_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.Init();
            }
        }

        /// <summary>
        /// Save�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("�Ƿ�Ҫ�����շѣ�", 422) == DialogResult.No)
            {
                return -1;
            }
            this.Save();
            
            return base.OnSave(sender, neuObject);
        }

        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDoct.Tag != null) 
            {
                if (this.patientInfo == null) 
                {
                    return;
                }
                
                this.patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoct.Tag.ToString();
                this.patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoct.Text;

                this.ucInpatientCharge_new1.RecipeDoctCode = this.cmbDoct.Tag.ToString();
            }
        }

        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                this.ucInpatientCharge_new1.Focus();
            }
        }

        /// <summary>
        /// TreeView˫������Selected��Ӧ�¼� {B8E87F3F-E397-42f3-9418-6FC4DEE36729} wbo 2010-08-27
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //this.tv.Nodes[0].Checked = false;// {A196A50F-36E2-49b4-B530-DCC38D9D4464} �𲻵����ã���������ѡ����ʱ��������
            //e.Checked = !e.Checked;
            //if (!e.Checked)
            //{
            //    this.Clear();
            //    return -2;
            //}

            this.Clear();
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            if (this.patientInfo == null)
            {
                return -2;
            }
            this.PatientInfo = this.radtIntegrate.GetPatientInfomation(this.patientInfo.ID);
            return base.OnSetValue(neuObject, e);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint) };
            }
        }

        #endregion
        //{7376038F-EFE8-46c8-BA63-3147C6EF67F0}
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ucInpatientCharge_new1.ChangeDept(this.cmbDept.SelectedItem);
        }
    }
}
