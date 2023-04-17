using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Integrate;
using FS.HISFC.BizLogic.Operation;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �������뵥]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucApplicationForm : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucApplicationForm()
        {

            InitializeComponent();
            if (!this.DesignMode)
            {
                this.Reset();
                this.Init();
            }

        }

        #region �ֶ�
        private FS.HISFC.BizProcess.Integrate.RADT radtmanager = new RADT();
        public FS.HISFC.Models.Operation.OperationAppllication applyoldMZ = null;  //{0E140FEC-2F31-4414-8401-86E78FA3ADDC} ��������������//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        private FS.FrameWork.Public.ObjectHelper payKindHelper=new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.Models.Operation.OperationAppllication operationApplication = new FS.HISFC.Models.Operation.OperationAppllication();
        private FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose opsDiagnose = new FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose();
        private bool isNew = true;     //�Ƿ��½�����
        private FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint arrangeFormPrint;
        private System.Windows.Forms.Control contralActive = new Control();
        private bool dirty = false;
        private FS.HISFC.BizLogic.Operation.OpsTableManage opsMgr = new OpsTableManage();
        private FS.HISFC.Models.Base.Employee var = null;
        private bool checkApplyTime = false;
        private bool checkEmergency = true;
        private bool checkDate = true;
        private bool isOwnPrivilege = true;// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        private bool isNeedApprove = false;//�Ƿ���Ҫ������뵥
        private bool isHavingApprove = false;//�����Ȩ
        private bool isupdatestate = false;
        private string defaultApplyDept = string.Empty;
        private PatientInfo pinfo; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
        /// <summary>
        /// ������Ƿ���ʾ��ӡ
        /// </summary>
        private bool isSavePrint = true;

        /// <summary>
        /// ������Ƿ���ʾ��ӡ
        /// </summary>
        [Category("�ؼ�����"), Description("������Ƿ���ʾ��ӡ")]
        public bool IsSavePrint
        {
            get
            {
                return isSavePrint;
            }
            set
            {
                isSavePrint = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ̨���ͺ�̨�����
        /// </summary>
        ArrayList alTableTypeCompare;
        Hashtable hsTableTypeCompare;
        /// <summary>
        /// ����̨��
        /// </summary>
        ArrayList alOrder;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatientInfo PatientInfo
        {
            set
            {
                this.Reset();

                if (value == null)
                    return;

                #region ��ֵ
                this.lblName.Text = value.Name; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
                this.lblGender.Text = value.Sex.Name;
                FS.FrameWork.Management.DataBaseManger daMgr = new FS.FrameWork.Management.DataBaseManger();
                int age = daMgr.GetDateTimeFromSysDateTime().Year - value.Birthday.Year;
                this.lblAge.Text = age.ToString() + "��";
                this.lblID.Text = value.PID.PatientNO;

                this.pinfo = value; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
                this.lblType.Text = payKindHelper.GetName(value.Pact.PayKind.ID);
                this.lblDept.Text = value.PVisit.PatientLocation.Dept.Name;
                this.lblBed.Text = value.PVisit.PatientLocation.Bed.Name;
                this.lblBalance.Text = value.FT.LeftCost.ToString();
                this.lblPhone.Text = value.PhoneHome; //{0a73b038-1b02-4881-b4e3-31728e3e8c4a}
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);

               
                //������
                //�������ԱΪ��������Ա,Ĭ��������Ϊ����Ա���ڿ���
                foreach (Department dept in this.cmbExeDept.alItems)
                {
                    if (dept.ID == Environment.OperatorDeptID)
                    {
                        this.cmbExeDept.Tag = dept.ID;
                        break;
                    }
                }
                //û�и�ֵ,��������Ա������������Ա,Ĭ���б��е�һ��
                if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
                {
                    if (this.cmbExeDept.Items.Count > 0)
                    {
                        if (string.IsNullOrEmpty(defaultApplyDept))
                        {
                            this.cmbExeDept.SelectedIndex = 0;
                        }
                        else
                        {
                            this.cmbExeDept.Tag = defaultApplyDept;
                        }
                    }
                }
                //����ָ��ʱ����������жϵ����Ƿ�����̨,�����Զ���Ϊ��̨
                Department d = this.cmbExeDept.SelectedItem as Department;

                int num = Environment.OperationManager.GetEnableTableNum(d, value.PVisit.PatientLocation.Dept.ID, this.dtOperDate.Value);
                if (num > 0)
                    this.cmbTableType.SelectedIndex = 0;//��̨
                else
                    this.cmbTableType.SelectedIndex = 1;//��̨
                #endregion

                this.operationApplication.PatientInfo = value;
                this.isNew = true;


            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Operation.OperationAppllication OperationApplication
        {
            get
            {
                if (this.operationApplication == null)
                    this.operationApplication = new OperationAppllication();

                return this.operationApplication;
            }
            set
            {
                this.operationApplication = value;


                #region ��ֵ

                #region �Ƿ��������������
                FS.HISFC.Models.Operation.OperationAppllication applicationCancelApply = Environment.OperationManager.QueryCancelApplyByAppID(value.ID);
                if (applicationCancelApply == null || string.IsNullOrEmpty(applicationCancelApply.ID))
                {
                    this.nlbCancelApply.Visible = false;
                }
                else
                {
                    this.nlbCancelApply.Visible = true;
                }
                #endregion

                //{DF7604C3-C7C6-4c8e-ADB1-A50C116BC378}
               // {66E970CB-3342-4c38-9B14-0E514DDC82A3}
                PatientInfo p;
                if (this.operationApplication.PatientSouce == "2")
                {
                    p = this.radtmanager.GetPatientInfomation(value.PatientInfo.ID);
                    this.pinfo = p;
                }
                else
                {
                    p = this.operationApplication.PatientInfo;
                    Manager manager = new Manager();
                    try
                    {
                        if (!string.IsNullOrEmpty(p.PVisit.PatientLocation.Dept.ID))
                        {
                            p.PVisit.PatientLocation.Dept = manager.GetDepartment(p.PVisit.PatientLocation.Dept.ID);
                            p.PVisit.PatientLocation.Bed.Name = " /";
                        }
                    }
                    catch
                    {
                    }
                }

                if (p == null)
                {
                    MessageBox.Show("�޴˻�����Ϣ!", "��ʾ");
                    return;
                }
                this.PatientInfo = p;// this.operationApplication.PatientInfo;

                if (value.OperateKind == "1")
                { this.cmbOperKind.SelectedIndex = 0; }//����
                else if (value.OperateKind == "2")
                { this.cmbOperKind.SelectedIndex = 1; }//����
                else
                { this.cmbOperKind.SelectedIndex = 2; }//��Ⱦ

                if (value.DiagnoseAl.Count > 0)//��һ���
                {
                    dirty = true;
                    this.txtDiag.Text = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                    dirty = false;
                    FS.HISFC.Models.HealthRecord.ICD icd = new FS.HISFC.Models.HealthRecord.ICD();
                    icd.ID = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                    icd.Name = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                    this.txtDiag.Tag = icd;

                    if (value.DiagnoseAl.Count >= 2) //�ڶ����
                    {
                        //dirty = true;
                        this.txtDiag2.Text = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                        //dirty = false;
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                        icd.Name = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                        this.txtDiag2.Tag = icd;
                        if (value.DiagnoseAl.Count >= 3) //������� 
                        {
                            dirty = true;
                            this.txtDiag3.Text = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                            dirty = false;
                            icd = new FS.HISFC.Models.HealthRecord.ICD();
                            icd.ID = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                            icd.Name = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                            this.txtDiag3.Tag = icd;

                        }

                    }
                }
                if (value.OperationInfos.Count > 0) //��һ���� 
                {
                    dirty = true;
                    this.txtOperation.Text = (value.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                    dirty = false;
                    this.txtOperation.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[0];//��������

                    if (value.OperationInfos.Count >= 2) //�ڶ����� 
                    {
                        dirty = true;
                        this.txtOperation2.Text = (value.OperationInfos[1] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                        dirty = false;
                        this.txtOperation2.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[1];//��������
                        if (value.OperationInfos.Count >= 3)//��������
                        {
                            dirty = true;
                            this.txtOperation3.Text = (value.OperationInfos[2] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                            dirty = false;
                            this.txtOperation3.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[2];//��������
                        }
                    }
                }
                //add by chengym 2012-5-14
                int le=value.AnesType.ID.IndexOf("|");
                if ( le> 0)
                {
                    this.cmbAnae.Tag = value.AnesType.ID.Substring(0, le);
                    value.AnesType.Name = this.cmbAnae.Text;
                    this.cmbAnae2.Tag = value.AnesType.ID.Substring(le + 1);
                }
                else
                {
                    this.cmbAnae.Tag = value.AnesType.ID;//����ʽ
                    value.AnesType.Name = this.cmbAnae.Text;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnseWay.Tag = value.AnesWay;
                this.dtOperDate.Value = value.PreDate;//��������
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID,this.dtOperDate.Value);
                this.cmbExeDept.Tag = value.ExeDept.ID;//ִ�п���
                this.comDept.Tag = value.OperationDoctor.Dept.ID;
                if (value.TableType == "1")
                { this.cmbTableType.SelectedIndex = 0; }//��̨
                else if (value.TableType == "2")
                { this.cmbTableType.SelectedIndex = 1; }//��̨
                else
                { this.cmbTableType.SelectedIndex = 2; }//��̨
                this.cmbDoctor.SelectedIndexChanged -= new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                this.cmbDoctor.Tag = value.OperationDoctor.ID;//����
                this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in value.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        this.cmbHelper1.Tag = role.ID;//һ��
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        this.cmbHelper2.Tag = role.ID;//����
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper3.ToString())
                    {
                        this.cmbHelper3.Tag = role.ID;//����
                    }
                }
                //{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                this.cmbSpecial.SelectedIndex = Convert.ToInt32(value.BloodNum);//�Ƿ���������
                this.cmbSpecial.Tag = int.Parse(value.BloodNum.ToString());
                this.cmbSpecial.Text = cmbSpecial.alItems[int.Parse(value.BloodNum.ToString())].ToString();
                this.cmbOrder.Text = value.BloodUnit;//̨��

                if (value.IsAccoNurse)
                    this.cbxNeedQX.Checked = true;//��е��ʿ
                if (value.IsPrepNurse)
                    this.cbxNeedXH.Checked = true;//Ѳ�ػ�ʿ

                if (value.IsHeavy)//�Ƿ�ͬ��ʹ���Է���Ŀ
                    this.cmbOwn.SelectedIndex = 0;
                else
                    this.cmbOwn.SelectedIndex = 1;

                this.rtbApplyNote.Text = value.ApplyNote;//˵��

                this.cmbApplyDoct.Tag = value.ApplyDoctor.ID;//����ҽ��
                this.lbApplyDate.Text = value.ApplyDate.ToString("yyyy��MM��dd�� HHʱmm��");

                //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
                this.cmbIncitepe.Tag = value.InciType.ID;
                #endregion
                //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                #region ��ֵ��Ҫ����
                if (value.Eneity.Length > 0)
                {
                    string[] str = value.Eneity.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
                        {
                            if (str[i].ToString() == chk.Text)
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
                #endregion
                #region ��ֵ��λ�Ͳ�λ
                if (value.Position.Length > 0)
                {
                    string[] str1 = value.Position.Split('|');
                    //this.cmbPortPosition.Text = str1[0].ToString();
                    this.txtPortPosition.Text = str1[0].ToString();
                    this.cmbBodyPosion.Text = str1[1].ToString();
                }
                #endregion
                #region ��ֵ�Ƿ����
                if (value.IsOlation == "1")
                {
                    this.cmbIsDevide.Text = "��";
                }
                else
                {
                    this.cmbIsDevide.Text = "��";
                }
                #endregion

                //����������ʱ��
                if (value.LastTime.Length > 0)
                {
                    string[] strTime = value.LastTime.Split('|');
                    this.txtLastTime.Text = strTime[0].ToString();
                    this.cmbUnit.Text = strTime[1].ToString();
                }
                //��������
                this.cmbOpType.Tag = value.OperationType.ID;
                #region �����Ϣ
                //�����
                this.cmbApproveDoctor.Tag = value.ApproveDoctor.ID;
                //���ʱ��
                if (value.ApproveDate == System.DateTime.MinValue)
                {
                    this.dtApproveDate.Value = System.DateTime.Now;
                }
                else
                {
                    this.dtApproveDate.Value = value.ApproveDate;
                }
                //������
                this.cmbApproveNote.Tag = value.ApproveNote;
                #endregion
                this.operationApplication = value;
                this.isNew = false;//�޸�
            }
        }

        
        //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        /// <summary>
        /// �����������������ʼ��סԺ�������봰��
        /// </summary>
        public void SetOperationApplyBYMz()
        {
            if (applyoldMZ != null)
            {
                this.operationApplication.PatientSouce = "2";
              if (applyoldMZ.OperateKind == "1")
                { this.cmbOperKind.SelectedIndex = 0; }//����
                else if (applyoldMZ.OperateKind == "2")
                { this.cmbOperKind.SelectedIndex = 1; }//����
                else
                { this.cmbOperKind.SelectedIndex = 2; }//��Ⱦ

                if (applyoldMZ.DiagnoseAl.Count > 0)//��һ���
                {
                    dirty = true;
                    this.txtDiag.Text = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                    dirty = false;
                    FS.HISFC.Models.HealthRecord.ICD icd = new FS.HISFC.Models.HealthRecord.ICD();
                    icd.ID = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                    icd.Name = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                    this.txtDiag.Tag = icd;

                    if (applyoldMZ.DiagnoseAl.Count >= 2) //�ڶ����
                    {
                        dirty = true;
                        this.txtDiag2.Text = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                        dirty = false;
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                        icd.Name = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                        this.txtDiag2.Tag = icd;
                        if (applyoldMZ.DiagnoseAl.Count >= 3) //������� 
                        {
                            dirty = true;
                            this.txtDiag3.Text = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//���
                            dirty = false;
                            icd = new FS.HISFC.Models.HealthRecord.ICD();
                            icd.ID = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                            icd.Name = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                            this.txtDiag3.Tag = icd;

                        }

                    }
                }
                if (applyoldMZ.OperationInfos.Count > 0) //��һ���� 
                {
                    dirty = true;
                    this.txtOperation.Text = (applyoldMZ.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                    dirty = false;
                    this.txtOperation.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[0];//��������

                    if (applyoldMZ.OperationInfos.Count >= 2) //�ڶ����� 
                    {
                        dirty = true;
                        this.txtOperation2.Text = (applyoldMZ.OperationInfos[1] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                        dirty = false;
                        this.txtOperation2.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[1];//��������
                        if (applyoldMZ.OperationInfos.Count >= 3)//��������
                        {
                            dirty = true;
                            this.txtOperation3.Text = (applyoldMZ.OperationInfos[2] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                            dirty = false;
                            this.txtOperation3.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[2];//��������
                        }
                    }
                }
                //add by chengym 2012-5-14
                int le=applyoldMZ.AnesType.ID.IndexOf("|");
                if ( le> 0)
                {
                    this.cmbAnae.Tag = applyoldMZ.AnesType.ID.Substring(0, le);
                    applyoldMZ.AnesType.Name = this.cmbAnae.Text;
                    this.cmbAnae2.Tag = applyoldMZ.AnesType.ID.Substring(le + 1);
                }
                else
                {
                    this.cmbAnae.Tag = applyoldMZ.AnesType.ID;//����ʽ
                    applyoldMZ.AnesType.Name = this.cmbAnae.Text;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnseWay.Tag = applyoldMZ.AnesWay;
                this.dtOperDate.Value = applyoldMZ.PreDate;//��������
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID,this.dtOperDate.Value);
                this.cmbExeDept.Tag = applyoldMZ.ExeDept.ID;//ִ�п���
                this.comDept.Tag = applyoldMZ.OperationDoctor.Dept.ID;
                if (applyoldMZ.TableType == "1")
                { this.cmbTableType.SelectedIndex = 0; }//��̨
                else if (applyoldMZ.TableType == "2")
                { this.cmbTableType.SelectedIndex = 1; }//��̨
                else
                { this.cmbTableType.SelectedIndex = 2; }//��̨
                this.cmbDoctor.SelectedIndexChanged -= new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                this.cmbDoctor.Tag = applyoldMZ.OperationDoctor.ID;//����
                this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in applyoldMZ.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        this.cmbHelper1.Tag = role.ID;//һ��
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        this.cmbHelper2.Tag = role.ID;//����
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper3.ToString())
                    {
                        this.cmbHelper3.Tag = role.ID;//����
                    }
                }
                //this.cmbSpecial.SelectedIndex = int.Parse(applyoldMZ.BloodNum.ToString());//�Ƿ���������
                //this.cmbSpecial.Tag = int.Parse(applyoldMZ.BloodNum.ToString());
                this.cmbSpecial.SelectedIndex = Convert.ToInt32(applyoldMZ.BloodNum);//�Ƿ���������  {2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                this.cmbSpecial.Tag = int.Parse(applyoldMZ.BloodNum.ToString());
                this.cmbSpecial.Text = cmbSpecial.alItems[int.Parse(applyoldMZ.BloodNum.ToString())].ToString();
                this.cmbOrder.Text = applyoldMZ.BloodUnit;//̨��

                if (applyoldMZ.IsAccoNurse)
                    this.cbxNeedQX.Checked = true;//��е��ʿ
                if (applyoldMZ.IsPrepNurse)
                    this.cbxNeedXH.Checked = true;//Ѳ�ػ�ʿ

                if (applyoldMZ.IsHeavy)//�Ƿ�ͬ��ʹ���Է���Ŀ
                    this.cmbOwn.SelectedIndex = 0;
                else
                    this.cmbOwn.SelectedIndex = 1;

                this.rtbApplyNote.Text = applyoldMZ.ApplyNote;//˵��

                this.cmbApplyDoct.Tag = applyoldMZ.ApplyDoctor.ID;//����ҽ��
                this.lbApplyDate.Text = applyoldMZ.ApplyDate.ToString("yyyy��MM��dd�� HHʱmm��");

                //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
                this.cmbIncitepe.Tag = applyoldMZ.InciType.ID;
                #endregion
                //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                #region ��ֵ��Ҫ����
                if (applyoldMZ.Eneity.Length > 0)
                {
                    string[] str = applyoldMZ.Eneity.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
                        {
                            if (str[i].ToString() == chk.Text)
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
                #endregion
                #region ��ֵ��λ�Ͳ�λ
                if (applyoldMZ.Position.Length > 0)
                {
                    string[] str1 = applyoldMZ.Position.Split('|');
                    //this.cmbPortPosition.Text = str1[0].ToString();
                    this.txtPortPosition.Text = str1[0].ToString();
                    this.cmbBodyPosion.Text = str1[1].ToString();
                }
                #endregion
                #region ��ֵ�Ƿ����
                if (applyoldMZ.IsOlation == "1")
                {
                    this.cmbIsDevide.Text = "��";
                }
                else
                {
                    this.cmbIsDevide.Text = "��";
                }
                #endregion

                //����������ʱ��
                if (applyoldMZ.LastTime.Length > 0)
                {
                    string[] strTime = applyoldMZ.LastTime.Split('|');
                    this.txtLastTime.Text = strTime[0].ToString();
                    this.cmbUnit.Text = strTime[1].ToString();
                }
                //��������
                this.cmbOpType.Tag = applyoldMZ.OperationType.ID;
                #region �����Ϣ
                //�����
                this.cmbApproveDoctor.Tag = applyoldMZ.ApproveDoctor.ID;
                //���ʱ��
                if (applyoldMZ.ApproveDate == System.DateTime.MinValue)
                {
                    this.dtApproveDate.Value = System.DateTime.Now;
                }
                else
                {
                    this.dtApproveDate.Value = applyoldMZ.ApproveDate;
                }
                //������
                this.cmbApproveNote.Tag = applyoldMZ.ApproveNote;
                #endregion
              
                this.isNew = true;//�޸�
            }
        
        }

        
        /// <summary>
        /// ��������ԤԼ���ڻ�ȡ�����������������
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string GetOrdered(string deptCode,DateTime dateTime)
        {
            string ordered = string.Empty;
            ordered = Environment.OperationManager.GetOrderByApplyDeptAndTime(deptCode, dateTime);
            if(!string.IsNullOrEmpty(ordered))
            {
                ordered = ordered.Replace(",", "\n");
            }
            return ordered;
        }

        protected new bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");


            }
        }

        /// <summary>
        /// ��������Ŀ
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NeuObject MainOperation
        {
            set
            {
                this.txtOperation.Text = value.Name;
                this.txtOperation.Tag = value.ID;
            }
        }

        /// <summary>
        /// �Ƿ�������������
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
            }
        }
        
        [Category("�ؼ�����"), Description("��������ȵ�ǰʱ���������")]
        public bool CheckApplyTime
        {
            get
            {
                return checkApplyTime;
            }
            set
            {
                checkApplyTime = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ӡ���������뵥״̬Ϊ�Ѱ���")]
        public bool IsUpdateSate
        {
            get
            {
                return isupdatestate;
            }
            set
            {
                isupdatestate = value;
            }
        }


        [Category("�ؼ�����"), Description("����ʱ�䳬����ֹʱ�䣬�Ƿ���Ҫ��Ϊ����")]
        public bool CheckEmergency
        {
            get
            {
                return checkEmergency;
            }
            set
            {
                checkEmergency = value;
            }
        }
        [Category("�ؼ�����"), Description("�������ղ���������һ������")]
        public bool CheckDate
        {
            get
            {
                return checkDate;
            }
            set
            {
                checkDate = value;
            }
        }

        // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        [Category("�ؼ�����"), Description("���������Ƿ񰴸���Ȩ�޻�ȡ������ҽ�������ȡ��������")]
        public bool IsOwnPrivilege
        {
            get
            {
                return isOwnPrivilege;
            }
            set
            {
                isOwnPrivilege = value;
            }
        }
        private string note = "ǰ�����������룬����ʹ�ý�̨��";
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string Note
        {
            get { return this.note; }
            set
            {
                this.note = value;
                this.SetNote();
            }
        }
      

        #region ����

        /// <summary>
        /// ��ʹ��
        /// </summary>
        private void Init()
        {
            //̨����̨�����
           
            alTableTypeCompare = Environment.IntegrateManager.GetConstantList("TableTypeCompare");
            if (alTableTypeCompare != null)
            {
                hsTableTypeCompare = new Hashtable();
                foreach (FS.FrameWork.Models.NeuObject tableCompareInfo in alTableTypeCompare)
                {
                    if (hsTableTypeCompare.Contains(tableCompareInfo.Name))
                    {
                        ArrayList allData = hsTableTypeCompare[tableCompareInfo.Name] as ArrayList;
                        allData.Add(Environment.IntegrateManager.GetConstant("OperatoinOrder", tableCompareInfo.Memo));
                    }
                    else
                    { 
                        ArrayList allData = new ArrayList();
                        allData.Add(Environment.IntegrateManager.GetConstant("OperatoinOrder", tableCompareInfo.Memo));
                        hsTableTypeCompare.Add(tableCompareInfo.Name, allData);
                    }
                }
            }

            var = (FS.HISFC.Models.Base.Employee)opsMgr.Operator;
            //֧������
            this.payKindHelper = new FS.FrameWork.Public.ObjectHelper(Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));

            ArrayList alRet;

            //��������
            alRet = Environment.IntegrateManager.GetConstantList("CASEANESTYPE");//FS.HISFC.Models.Base.EnumConstant.ANESTYPE);
            this.cmbAnae.AddItems(alRet);
            this.cmbAnae.IsListOnly = true;
            this.cmbAnae2.AddItems(alRet);
            this.cmbAnae2.IsListOnly = true;

            //�������'������𣨾����ѡ�飬ҽ������ʱ��д��//{B9DDCC10-3380-4212-99E5-BB909643F11B}
            alRet = Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ANESWAY);
            this.cmbAnseWay.AddItems(alRet);
            this.cmbAnseWay.IsListOnly = true;

            ArrayList alTypeOfInfection;
            //��Ⱦ����
            alTypeOfInfection = Environment.IntegrateManager.GetConstantList("TYPEOFINFECTION");
            this.cmbSpecial.AddItems(alTypeOfInfection);
            this.cmbSpecial.IsListOnly = true;

            //������
            alRet = Environment.IntegrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            //�����������{08A723B7-658F-4488-9730-4A8E2748D3C2}
            ArrayList al = new ArrayList();
            al = alRet.Clone() as ArrayList;
            alRet.Clear();
            foreach (FS.HISFC.Models.Base.Department dpt in al)
            {
                if (dpt.SpecialFlag != "2" && dpt.Name.IndexOf("��ʿվ") <= 0)
                {
                    alRet.Add(dpt);
                }
            }
            ArrayList deptList = Environment.IntegrateManager.GetDeptmentAllValid();
            this.comDept.AddItems(deptList); //���ؿ���

            this.cmbExeDept.AddItems(alRet);
            //this.cmbExeDept.SelectedIndex = 1;
            this.cmbExeDept.IsListOnly = true;
            if (alRet.Count == 2)
            {
                this.cmbExeDept.Text = alRet[0].ToString();
                this.cmbExeDept.Tag = alRet[1].ToString();
            }
            //����
            ArrayList alRetTmp;
            alRetTmp = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            alRet.Clear(); //{A157131C-EEE9-4bb9-A47F-11595614B68C}
            foreach (FS.HISFC.Models.Base.Employee empl in alRetTmp)
            {
                if (empl.ValidState==EnumValidState.Valid)
                {
                if (empl.Dept.ID == "1001" || empl.Dept.ID == "5010" || empl.Dept.ID == "5106")
                {
                    alRet.Add(empl);
                }
                }
            }

           // alRet = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.cmbDoctor.AddItems(alRet);
            this.cmbDoctor.IsListOnly = true;
            //һ��
            this.cmbHelper1.AddItems(alRet);
            this.cmbHelper1.IsListOnly = true;
            //����          
            this.cmbHelper2.AddItems(alRet);
            this.cmbHelper2.IsListOnly = true;
            //������            
            this.cmbHelper3.AddItems(alRet);
            this.cmbHelper3.IsListOnly = true;
            //����ҽ��
            this.cmbApplyDoct.AddItems(alRet);
            this.cmbApplyDoct.IsListOnly = true;
            //����� 2012-3-9 chengym
            this.cmbApproveDoctor.AddItems(alRet);
            this.cmbApproveDoctor.Tag = string.Empty;
            this.cmbApproveDoctor.IsListOnly = true;
            //���ʱ��
            this.dtApproveDate.Value = System.DateTime.Now;
            //������
            alRet=Environment.IntegrateManager.GetConstantList("OperatoinApprNote");
            this.cmbApproveNote.AddItems(alRet);
            this.cmbApproveNote.IsListOnly=true;
            this.cmbApproveNote.Tag = "1";
            //�п�����{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            alRet = Environment.IntegrateManager.GetConstantList(EnumConstant.INCITYPE);
            this.cmbIncitepe.AddItems(alRet);
            this.cmbIncitepe.IsListOnly = true;
            //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}feng.ch
            //̨��ӳ�������ȡ
            alRet = Environment.IntegrateManager.GetConstantList("OperatoinOrder");
            this.cmbOrder.ClearItems();
            this.cmbOrder.AddItems(alRet);
            alOrder = alRet.Clone() as ArrayList;
            //�������ӳ�������ȡ
            this.cmbOperKind.ClearItems();
            alRet = Environment.IntegrateManager.GetConstantList("Operatetype");
            this.cmbOperKind.AddItems(alRet);
            //��λ
            alRet = Environment.IntegrateManager.GetConstantList("OPERBODY");
            this.cmbBodyPosion.AddItems(alRet);
            //��λ
            alRet = Environment.IntegrateManager.GetConstantList("OPERPORT");
            this.cmbPortPosition.AddItems(alRet);
            //��������
            alRet = Environment.IntegrateManager.GetConstantList("OPERATETYPE");
            this.cmbOpType.AddItems(alRet);
            ////����ע������
            FS.HISFC.BizProcess.Integrate.Manager ctlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //try
            //{	//��ѯ�����������ʱ��
            //    string control = ctlMgr.QueryControlerInfo("optime");

            //    if (control != "" && control != "-1") this.lbNote.Text = "Ҫ����" + control + this.note;//"ǰ�����������룬����ʹ�ý�̨��";

            //    if (this.cmbExeDept.Items.Count > 0)
            //    {
            //        ArrayList list = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ExeDept");
            //        if (list.Count == 2)
            //        {
            //            this.cmbExeDept.Text = list[0].ToString();
            //            this.cmbExeDept.Tag = list[1].ToString();
            //        }
            //    }
            //}
            //catch { }
            #region ���
            ucDiag1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
            this.Controls.Add(ucDiag1);
            ucDiag1.Size = new Size(456, 312);
            ucDiag1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiag1_SelectItem);
            ucDiag1.Init();
            ucDiag1.Visible = false;
            #endregion

            #region ����
            ucOpItem1 = new ucOpItem();
            this.Controls.Add(ucOpItem1);
            ucOpItem1.Size = new Size(518, 338);
            ucOpItem1.SelectItem += new ucOpItem.MyDelegate(ucOpItem1_SelectItem);
            ucOpItem1.Init();
            ucOpItem1.Visible = false;
            #endregion 
            #region �������뵥���Ȩ
            try
            {
                //�Ƿ���Ҫ���
                string strNeedApprove = ctlMgr.QueryControlerInfo("opappr");
                if (strNeedApprove == "1")
                {
                    List<FS.FrameWork.Models.NeuObject> myPrivDept = new List<FS.FrameWork.Models.NeuObject>();
                    string OperID = FS.FrameWork.Management.Connection.Operator.ID;
                    string OperDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    myPrivDept = Environment.IntegrateManager.QueryUserPrivCollection(OperID, "op01", OperDept);
                    if (myPrivDept.Count > 0)
                    {
                        this.groupBox1.Visible = true;
                        this.isHavingApprove = true;
                    }
                    else
                    {
                        this.groupBox1.Visible = false;
                        this.isHavingApprove = false;
                    }

                    this.isNeedApprove = true;
                }
                else
                {
                    this.groupBox1.Visible = false;
                }
            }
            catch
            {
            }

            #endregion

            #region Ĭ��ѡ�е�������
            defaultApplyDept = ctlMgr.QueryControlerInfo("OPS001");
            #endregion
        }

        /// <summary>
        /// �������ÿؼ�
        /// </summary>
        private void Reset()
        {
            this.operationApplication = new OperationAppllication();
            this.applyoldMZ = null;//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
            this.lblName.Text = "";
            this.lblGender.Text = "";
            this.lblAge.Text = "";
            this.lblID.Text = "";
            this.lblType.Text = "";
            this.lblDept.Text = "";
            this.lblBed.Text = "";
            this.lblBalance.Text = "";
            this.lblPhone.Text = ""; //{0a73b038-1b02-4881-b4e3-31728e3e8c4a}
            //�������
            this.cmbOperKind.SelectedIndex = 0;//��ͨ

            //dirty = true;
            this.txtDiag.Text = "";//���
            this.txtDiag.Tag = null;
            this.txtDiag2.Text = "";//���
            this.txtDiag2.Tag = null;
            this.txtDiag3.Text = "";//���
            this.txtDiag3.Tag = null;

            this.txtOperation.Text = "";//��������
            this.txtOperation.Tag = null;
            this.txtOperation2.Text = "";//��������
            this.txtOperation2.Tag = null;
            this.txtOperation3.Text = "";//��������
            this.txtOperation3.Tag = null;
            //dirty = false;

            this.cmbAnae.Text = "";//��������
            this.cmbAnae.Tag = null;
            //add by chengym 2012-5-14
            this.cmbAnae2.Text = "";
            this.cmbAnae2.Tag = null;
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            this.cmbAnseWay.Text = "";
            this.cmbAnseWay.Tag = null;
            DateTime dtNow = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.lbApplyDate.Text = dtNow.ToString("yyyy��MM��dd�� HHʱmm��");//��������

            dtNow = DateTime.Parse(string.Concat(dtNow.Date.AddDays(1).ToString("yyyy-MM-dd"), " 09:00:00"));
            this.dtOperDate.Value = dtNow;//ԤԼʱ��

            this.cmbExeDept.Text = "";//������
            this.cmbExeDept.Tag = null;

            this.rtbApplyNote.Text = "";//��ע
            this.cbxNeedQX.Checked = false;
            this.cbxNeedXH.Checked = false;

            this.cmbDoctor.Text = "";//������
            this.cmbDoctor.Tag = null;
            this.cmbHelper1.Text = "";//һ��
            this.cmbHelper1.Tag = null;
            this.cmbHelper2.Text = "";//����
            this.cmbHelper2.Tag = null;
            this.cmbHelper3.Text = "";//����
            this.cmbHelper3.Tag = null;

            //this.cmbSpecial.SelectedIndex = 0;//�Ƿ���������
            this.cmbSpecial.Text = "";
            this.cmbOrder.Text = "";//̨��
            this.cmbOwn.SelectedIndex = 0;
            // {F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
            this.cmbIsDevide.Text = "��";
            this.cmbPortPosition.Tag = null;
            this.txtPortPosition.Text = "";
            this.cmbBodyPosion.Text = null;
            this.cmbUnit.Text = "";
            this.txtLastTime.Text = "";
            this.chkHBV.Checked = false;
            this.chkHIV.Checked = false;
            this.chkHCV.Checked = false;
            this.operationApplication = new FS.HISFC.Models.Operation.OperationAppllication();
            this.isNew = true;

            this.cmbApplyDoct.Text = "";//����ҽ��
            this.cmbApplyDoct.Tag = null;

            foreach (Employee person in this.cmbApplyDoct.alItems)
            {
                if (person.ID == FS.FrameWork.Management.Connection.Operator.ID)
                {
                    this.cmbApplyDoct.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                    break;
                }
            }
            this.cmbIncitepe.Tag = null;

            this.cmbApproveDoctor.Tag = null;
            this.dtApproveDate.Value = System.DateTime.Now;
            this.cmbApproveNote.Tag = null;
        }

        /// <summary>
        /// ʵ�帳ֵ
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            //��¼��ģ������µ����뵥
            if (this.isNew)
                this.operationApplication.ID = Environment.OperationManager.GetNewOperationNo();

            // TODO: ���Ϊʵ��
            #region ���
            FS.HISFC.Models.HealthRecord.DiagnoseBase diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();

            diag.OperationNo = this.operationApplication.ID;//�����
            //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
            if (((FS.FrameWork.Models.NeuObject)this.txtDiag.Tag).ID == "D991")
            {
                diag.ID = "D991";
                diag.Name = this.txtDiag.Text;
            }
            else
            {
                diag.ID = (this.txtDiag.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                diag.Name = (this.txtDiag.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
            }
            diag.Patient = this.operationApplication.PatientInfo.Clone();//.PatientInfo.Patient.Clone();
            diag.DiagType.ID = "7";//�������
            diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//��ǰ���
            diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//���ʱ��
            diag.Doctor.ID = var.ID;//���ҽ��
            diag.Doctor.Name = var.Name;//���ҽ��
            diag.Dept.ID = var.Dept.ID;//��Ͽ���
            diag.IsValid = true;//�Ƿ���Ч
            diag.IsMain = true;//�����

            if (operationApplication.DiagnoseAl.Count == 0)
                diag.HappenNo = opsDiagnose.GetNewDignoseNo();//���
            else
                diag.HappenNo = (operationApplication.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;

            operationApplication.DiagnoseAl.Clear();
            operationApplication.DiagnoseAl.Add(diag);
            #region �ڶ����
            if (txtDiag2.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = this.operationApplication.ID;//�����
                //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (((FS.FrameWork.Models.NeuObject)this.txtDiag2.Tag).ID == "D992")
                {
                    diag.ID = "D992";
                    diag.Name = this.txtDiag2.Text;
                }
                else
                {
                    diag.ID = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                    diag.Name = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                }
                diag.Patient = this.operationApplication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//�������
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//��ǰ���
                diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//���ʱ��
                diag.Doctor.ID = var.ID;//���ҽ��
                diag.Doctor.Name = var.Name;//���ҽ��
                diag.Dept.ID = var.Dept.ID;//��Ͽ���
                diag.IsValid = true;//�Ƿ���Ч
                diag.IsMain = false;//�����

                if (operationApplication.DiagnoseAl.Count == 1)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//���
                else
                    diag.HappenNo = (operationApplication.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationApplication.DiagnoseAl.Add(diag);
            }
            #endregion
            #region �������
            if (txtDiag3.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = this.operationApplication.ID;//�����
                //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (((FS.FrameWork.Models.NeuObject)this.txtDiag3.Tag).ID == "D993")
                {
                    diag.ID = "D993";
                    diag.Name = this.txtDiag3.Text;
                }
                else
                {
                    diag.ID = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                    diag.Name = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                }
                diag.Patient = this.operationApplication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//�������
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//��ǰ���
                diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//���ʱ��
                diag.Doctor.ID = var.ID;//���ҽ��
                diag.Doctor.Name = var.Name;//���ҽ��
                diag.Dept.ID = var.Dept.ID;//��Ͽ���
                diag.IsValid = true;//�Ƿ���Ч
                diag.IsMain = false;//�����

                if (operationApplication.DiagnoseAl.Count == 2)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//���
                else
                    diag.HappenNo = (operationApplication.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationApplication.DiagnoseAl.Add(diag);
            }
            #endregion
            #endregion
            #region ������Ŀ

            this.operationApplication.OperationInfos.Clear();
            //-----------------------------------------------------------------------------
            if (this.txtOperation.Text.Trim() != "" && this.txtOperation.Tag != null)
            {
                if (this.txtOperation.Tag.GetType() == typeof(FS.HISFC.Models.Operation.OperationInfo))
                {
                    FS.HISFC.Models.Operation.OperationInfo obj = (FS.HISFC.Models.Operation.OperationInfo)txtOperation.Tag;
                    operationApplication.OperationInfos.Add(obj);
                }
                else
                {
                    FS.HISFC.Models.Operation.OperationInfo opItem = new FS.HISFC.Models.Operation.OperationInfo();
                    opItem.OperationItem = (FS.HISFC.Models.Base.Item)this.txtOperation.Tag;//������Ŀ
                    opItem.FeeRate = 1m;//����
                    opItem.Qty = 1;//����
                    opItem.StockUnit = (this.txtOperation.Tag as FS.HISFC.Models.Base.Item).PriceUnit;//��λ
                    //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                    if (opItem.OperationItem.ID.IndexOf("S99")==-1)
                    {
                        opItem.OperateType.ID = (this.txtOperation.Tag as FS.HISFC.Models.Fee.Item.Undrug).OperationScale.ID;
                    }
                    opItem.IsValid = true;
                    opItem.IsMainFlag = true;
                    operationApplication.OperationInfos.Add(opItem);
                    operationApplication.OperationType.ID = opItem.OperateType.ID;
                }
            } 
            //-----------------------------------------------------------------------------
            //this.SetOperationItem(this.txtOperation.Tag);

            if (this.txtOperation2.Text.Trim() != "" && this.txtOperation2.Tag != null)
            {
                this.SetOperationItem(this.txtOperation2.Tag);
            }
            if (this.txtOperation3.Text.Trim() != "" && this.txtOperation3.Tag != null)
            {
                this.SetOperationItem(this.txtOperation3.Tag);
            }
            
            #endregion
            //����ʽ
            this.operationApplication.AnesType.ID = this.cmbAnae.Tag.ToString();
            this.operationApplication.AnesType.Name = this.cmbAnae.Text;
            //add by chengym 2012-5-14
            if (this.cmbAnae2.Tag != null && this.cmbAnae2.Text != "")
            {
                this.operationApplication.AnesType.ID += "|"+this.cmbAnae2.Tag.ToString();
                this.operationApplication.AnesType.Name += "|" + this.cmbAnae2.Text;
            }

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            if (!this.cmbAnseWay.Visible)
            {
                this.cmbAnseWay.Tag = "1";
            }
            this.operationApplication.AnesWay = this.cmbAnseWay.Tag.ToString();

            //����˵��
            this.operationApplication.ApplyNote = this.rtbApplyNote.Text.Trim();
            #region ����
            FS.HISFC.Models.Operation.ArrangeRole role;
            role = new FS.HISFC.Models.Operation.ArrangeRole();
            role.OperationNo = this.operationApplication.ID;                   //�����
            role.ID = this.cmbDoctor.Tag.ToString();                             //��Ա����
            role.Name = this.cmbDoctor.Text;
            role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Operator;    //��ɫ����
            role.ForeFlag = "0";                                                        //��ǰ¼��
            this.operationApplication.RoleAl.Clear();
            this.operationApplication.RoleAl.Add(role);
            this.operationApplication.OperationDoctor.ID = role.ID;
            this.operationApplication.OperationDoctor.Name = role.Name;
            #endregion
            #region һ��
            role = new FS.HISFC.Models.Operation.ArrangeRole();
            role.OperationNo = this.operationApplication.ID;//�����
            role.ID = this.cmbHelper1.Tag.ToString();//��Ա����
            role.Name = this.cmbHelper1.Text;
            role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper1;//��ɫ����
            role.ForeFlag = "0";//��ǰ¼��
            this.operationApplication.RoleAl.Add(role);

            FS.FrameWork.Models.NeuObject person;
            person = new FS.FrameWork.Models.NeuObject();

            person.ID = role.ID;
            person.Name = role.Name;
            this.operationApplication.HelperAl.Clear();
            this.operationApplication.HelperAl.Add(person);
            #endregion
            #region ����
            if (this.cmbHelper2.Tag != null && this.cmbHelper2.Tag.ToString() != "")
            {
                role = new FS.HISFC.Models.Operation.ArrangeRole();
                role.OperationNo = this.operationApplication.ID;//�����
                role.ID = this.cmbHelper2.Tag.ToString();//��Ա����
                role.Name = this.cmbHelper2.Text;
                role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper2;//��ɫ����
                role.ForeFlag = "0";//��ǰ¼��
                this.operationApplication.RoleAl.Add(role);

                person = new FS.FrameWork.Models.NeuObject();

                person.ID = role.ID;
                person.Name = role.Name;
                this.operationApplication.HelperAl.Clear();
                this.operationApplication.HelperAl.Add(person);
            }
            #endregion
            #region ����
            if (this.cmbHelper3.Tag != null && this.cmbHelper3.Tag.ToString() != "")
            {
                role = new FS.HISFC.Models.Operation.ArrangeRole();
                role.OperationNo = this.operationApplication.ID;//�����
                role.ID = this.cmbHelper3.Tag.ToString();//��Ա����
                role.Name = this.cmbHelper3.Text;
                role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper3;//��ɫ����
                role.ForeFlag = "0";//��ǰ¼��
                this.operationApplication.RoleAl.Add(role);

                person = new FS.FrameWork.Models.NeuObject();

                person.ID = role.ID;
                person.Name = role.Name;
                this.operationApplication.HelperAl.Clear();
                this.operationApplication.HelperAl.Add(person);
            }
            #endregion
            //ԤԼ����
            this.operationApplication.PreDate = this.dtOperDate.Value;
            //������
            this.operationApplication.OperateRoom.ID = this.cmbExeDept.Tag.ToString();
            this.operationApplication.OperateRoom.Name = this.cmbExeDept.Text;
            this.operationApplication.ExeDept = this.operationApplication.OperateRoom.Clone();
            //����̨����
            int index = this.cmbTableType.SelectedIndex + 1;
            this.operationApplication.TableType = index.ToString();
            //�Ƿ���������
            this.operationApplication.SpecialItem = this.cmbSpecial.Text;
            this.operationApplication.BloodNum = this.cmbSpecial.SelectedIndex;
            if (this.cmbSpecial.SelectedIndex == 0)//��
                this.operationApplication.IsSpecial = false;
            else
                this.operationApplication.IsSpecial = true;
            //̨��
            this.operationApplication.BloodUnit = this.cmbOrder.Text;

            //�Ƿ���ҪѲ��
            this.operationApplication.IsPrepNurse = this.cbxNeedXH.Checked;
            //�Ƿ���Ҫ��е
            this.operationApplication.IsAccoNurse = this.cbxNeedQX.Checked;

            if (this.cmbOwn.SelectedIndex == 0)//�Ƿ�ͬ��ʹ���Է���Ŀ
                this.operationApplication.IsHeavy = true;
            else
                this.operationApplication.IsHeavy = false;

            index = this.cmbOperKind.SelectedIndex + 1;
            this.operationApplication.OperateKind = index.ToString();

            //������
            this.operationApplication.User.ID = Environment.OperatorID;
            //����ҽ��
            this.operationApplication.ApplyDoctor.ID = this.cmbApplyDoct.Tag.ToString();
            this.operationApplication.ApplyDoctor.Name = this.cmbApplyDoct.Text;
            //�������
            this.operationApplication.ApplyDoctor.Dept.ID = Environment.OperatorDeptID;
            //������Դ
            this.operationApplication.PatientSouce = "2";//סԺ����
            this.operationApplication.OperationDoctor.Dept.ID = this.comDept.Tag.ToString();
            this.operationApplication.AnesWay = this.cmbAnseWay.Tag.ToString();

            //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            this.operationApplication.InciType.ID = this.cmbIncitepe.Tag.ToString();
            //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}feng.ch
            //��λ����λ
            //this.operationApplication.Position = this.cmbPortPosition.Text + "|" + this.cmbBodyPosion.Text;
            this.operationApplication.Position = this.txtPortPosition.Text + "|" + this.cmbBodyPosion.Text;
            //��Ҫ����
            string tag = string.Empty;
            foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
            {
                if (chk.Checked)
                {
                    tag += chk.Text+"|";
                }
            }
            this.operationApplication.Eneity = tag;
            //����������ʱ��
            this.operationApplication.LastTime = this.txtLastTime.Text+"|"+this.cmbUnit.Text;
            //�Ƿ����
            if (this.cmbIsDevide.Text.ToString() == "��")
            {
                this.operationApplication.IsOlation = "1";
            }
            else
            {
                this.operationApplication.IsOlation = "0";
            }

            this.operationApplication.OperationType.ID = this.cmbOpType.Tag.ToString();
            #region ���������Ϣ 2012-3-9 chengym
            this.operationApplication.ApproveDoctor.ID = this.cmbApproveDoctor.Tag.ToString();
            this.operationApplication.ApproveDate= this.dtApproveDate.Value;
            this.operationApplication.ApproveNote = this.cmbApproveNote.Tag.ToString();
            #endregion
            return 0;
        }

        /// <summary>
        /// ����������Ŀ
        /// </summary>
        /// <param name="operationItem"></param>
        private void SetOperationItem(object operationItem)
        {
            if (operationItem.GetType() == typeof(FS.HISFC.Models.Operation.OperationInfo))
            {
                FS.HISFC.Models.Operation.OperationInfo obj = (FS.HISFC.Models.Operation.OperationInfo)operationItem;
                this.operationApplication.OperationInfos.Add(obj);
            }
            else
            {
                FS.HISFC.Models.Operation.OperationInfo operationInfo = new FS.HISFC.Models.Operation.OperationInfo();
                operationInfo.OperationItem = operationItem as FS.HISFC.Models.Base.Item;//������Ŀ
                operationInfo.FeeRate = 1m;//����
                operationInfo.Qty = 1;//����
                operationInfo.StockUnit = (operationItem as FS.HISFC.Models.Base.Item).PriceUnit;//��λ
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (operationInfo.OperationItem.ID.IndexOf("S99")==-1)
                {
                    operationInfo.OperateType.ID = (operationItem as FS.HISFC.Models.Fee.Item.Undrug).OperationScale.ID;
                }
                operationInfo.IsValid = true;
                operationInfo.IsMainFlag = false;

                this.operationApplication.OperationInfos.Add(operationInfo);
                this.operationApplication.OperationType.ID = operationInfo.OperateType.ID;
            }
        }

        /// <summary>
        /// ��Ч����֤
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (this.isNew == false)
            {
                if (this.operationApplication.ExecStatus == "3" || this.operationApplication.ExecStatus == "4")
                {
                    MessageBox.Show("�����뵥�Ѱ��Ż�Ǽ�,�����޸�!", "��ʾ");
                    return -1;
                }
                if (this.operationApplication.ExecStatus == "5")
                {
                    MessageBox.Show("�����뵥��ȡ���Ǽ�,�����޸ģ�", "��ʾ");
                    return -1;
                }
                if (this.operationApplication.IsValid == false)
                {
                    MessageBox.Show("�����뵥�Ѿ�����!", "��ʾ");
                    return -1;
                }
            } 
            if (operationApplication.PatientInfo.ID == "")
            {
                MessageBox.Show("��ѡ�����뻼��!", "��ʾ");
                return -1;
            }
            if (this.txtDiag.Text.Length == 0)
            {
                MessageBox.Show("��ǰ���һ����Ϊ��!", "��ʾ");
                txtDiag.Focus();
                return -1;
            }
            string Diag1 = txtDiag.Text;
            string Diag2 = txtDiag2.Text;
            string Diag3 = txtDiag3.Text;
            //.
            if (Diag1 == "")
            { 
                MessageBox.Show("��ǰ���һ����Ϊ�գ�");
                txtDiag.Focus();
                return -1;
            }
            //
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtOperation.Text, 100) == false)
            {
                txtOperation.Focus();
                MessageBox.Show("���������ƹ�����");
                return -1;
            }
            //
            if ((Diag1 == Diag2 && Diag2 != "") || (Diag1 == Diag3 && Diag1 != "") || (Diag3 == Diag2 && Diag2 != ""))
            {
                MessageBox.Show("��ǰ��ϲ����ظ�");
                txtDiag.Focus();
                return -1;
            }
            // TODO: ��Ҫ���벡�����޸�
            if (this.txtOperation.Text.Length == 0)
            {
                MessageBox.Show("���������Ʋ���Ϊ��!", "��ʾ");
                txtOperation.Focus();
                return -1;
            }
            string Oper1 = txtOperation.Text;
            string Oper2 = txtOperation2.Text;
            string Oper3 = txtOperation3.Text;
            if ((Oper1 == Oper2 && Oper2 != "") || (Oper1 == Oper3 && Oper1 != "") || (Oper3 == Oper2 && Oper2 != ""))
            {
                MessageBox.Show("���������Ʋ����ظ�");
                txtOperation.Focus();
                return -1;
            }
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            if (this.cmbAnae.Tag == null || this.cmbAnae.Tag.ToString() == "")
            {
                MessageBox.Show("����ʽ����Ϊ��!", "��ʾ");
                cmbAnae.Focus();
                return -1;
            }
            //if (!this.cmbAnseWay.Visible)
            //{
            //    if (this.cmbAnseWay.Tag == null || this.cmbAnseWay.Tag.ToString() == "")
            //    {
            //        MessageBox.Show("���������Ϊ��!", "��ʾ");
            //        cmbAnseWay.Focus();
            //        return -1;
            //    }
            //}
            if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
            {
                MessageBox.Show("�����Ҳ���Ϊ��!", "��ʾ");
                cmbExeDept.Focus();
                return -1;
            }
            if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
            {
                MessageBox.Show("���߲���Ϊ��!", "��ʾ");
                cmbDoctor.Focus();
                return -1;
            }
            if (comDept.Text.Trim() == "" || comDept.Tag == null || comDept.Tag.ToString()=="")
            {
                MessageBox.Show("���߿��Ҳ���Ϊ��!", "��ʾ");
                comDept.Focus();
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtLastTime.Text))
            {
                MessageBox.Show("������ʱ�䲻��Ϊ��!", "��ʾ");
                this.txtLastTime.Focus();
                return -1;
            }
            if (this.cmbHelper1.Tag == null || this.cmbHelper1.Tag.ToString() == "")
            {
                MessageBox.Show("һ������Ϊ��!", "��ʾ");
                cmbHelper1.Focus();
                return -1;
            }
            string helper1 = "";
            string helper2 = "";
            string helper3 = "";
            this.cmbHelper1.Tag.ToString();
            if (this.cmbDoctor.Tag.ToString() == this.cmbHelper1.Tag.ToString())
            {
                MessageBox.Show("������һ�������ظ�!", "��ʾ");
                cmbDoctor.Focus();
                return -1;
            }

            if (cmbHelper2.Tag != null)
            {
                helper2 = this.cmbHelper2.Tag.ToString();
            }
            if (this.cmbHelper3.Tag != null)
            {
                helper3 = this.cmbHelper3.Tag.ToString();
            }
            if ((helper1 == helper2 && helper1 != "") || (helper1 == helper3 && helper3 != "") || (helper2 == helper3 && helper3 != ""))
            {
                MessageBox.Show("һ���������������ظ�");
                cmbHelper1.Focus();
                return -1;
            }
            if (this.cmbOrder.Text == "")
            {
                MessageBox.Show("��ָ��̨��!", "��ʾ");
                cmbOrder.Focus();
                return -1;
            }

            #region �ж�ÿ������������ʱֻ��ѡ��Ψһһ��̨��
            string Ordered = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);
            if (this.OperationApplication.BloodUnit!=this.cmbOrder.Text && Ordered.Contains(this.cmbOrder.Text))
            {
                MessageBox.Show("��ѡ���" + this.cmbOrder.Text + "�Ѿ�������ҽ��ѡ��,������ѡ��!", "��ʾ");
                cmbOrder.Focus();
                return -1;
            }
            #endregion

            #region ����
            //����ָ��ʱ����������жϵ����Ƿ�����̨,�����Զ���Ϊ��̨
            //Department d = new Department();

            //d.ID = this.cmbExeDept.Tag.ToString();
            //int num = Environment.OperationManager.GetEnableTableNum(d, operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, this.dtOperDate.Value);
            //int mm = Environment.OperationManager.SameDeptApplication(this.dtOperDate.Value.Date.ToString(), this.dtOperDate.Value.Date.AddDays(1).ToString(), d.ID, operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, cmbOrder.Text.Substring(0, 1));
            //if (mm == -1)
            //{
            //    MessageBox.Show("�ж��Ƿ���Ӧ������̨����" + Environment.OperationManager.Err);
            //    return -1;
            //}
            //if (num <= 0 && this.cmbTableType.SelectedIndex == 0 && isNew)
            //{
            //    MessageBox.Show("����������������̨,���޸�����̨����!", "��ʾ");
            //    cmbTableType.Focus();
            //    return -1;
            //}
            //if (num <= 0 && this.cmbTableType.SelectedIndex == 0 && mm == 1 && isNew)//����̨,����������̨
            //{
            //    MessageBox.Show("����������������̨,���޸�����̨����!", "��ʾ");
            //    cmbTableType.Focus();
            //    return -1;
            //}
            #endregion
            //#region �ж����������Ƿ���̨ û��ֻ�����̨���߼��� 2012-3-12 add  by chengym
            ////1 ����̨  -1 ֻ�ܼ�̨���߼���
            //string Error = string.Empty;
            //int lostNum = 0;
            //if (this.cmbTableType.SelectedIndex == 0 && this.isNew)//��̨���ж�
            //{
            //    lostNum = FS.HISFC.Components.Operation.Funtion.CheckLimitedLostNumber(this.dtOperDate.Value.Date, this.dtOperDate.Value.Date.AddDays(1), this.cmbExeDept.Tag.ToString(), operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, "", ref Error);
            //    if (lostNum == -2)
            //    {
            //        if (Error != string.Empty)
            //        {
            //            MessageBox.Show(Error, "��ʾ");
            //            return -1;
            //        }
            //    }
            //    if (lostNum == -1)
            //    {
            //        if (Error != string.Empty)
            //        {
            //            MessageBox.Show(Error, "��ʾ");
            //            return -1;
            //        }
            //        else
            //        {
            //            MessageBox.Show("����������������̨,���޸�����̨����!", "��ʾ");
            //            cmbTableType.Focus();
            //            return -1;
            //        }
            //    }
            //}
            //#endregion

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.rtbApplyNote.Text.Trim(), 200) == false)
            {
                MessageBox.Show("����˵������С��100������!", "��ʾ");
                rtbApplyNote.Focus();
                return -1;
            }
            if (this.cmbApplyDoct.Tag == null || this.cmbApplyDoct.Tag.ToString() == "")
            {
                MessageBox.Show("����ҽ������Ϊ��!", "��ʾ");
                cmbApplyDoct.Focus();

                return -1;
            }
            if (!checkDate)
            {
                if (((System.DateTime.Now.DayOfWeek == System.DayOfWeek.Saturday || System.DateTime.Now.DayOfWeek == System.DayOfWeek.Sunday) && cmbOperKind.Text == "����") && dtOperDate.Value.DayOfWeek == System.DayOfWeek.Monday)
                {
                    MessageBox.Show("����,���ղ���������һ������");
                    return -1;
                }
            }
            //�ж�����ʱ���Ƿ�Ϸ�
            string rtn = Environment.OperationManager.PreDateValidity(this.dtOperDate.Value);
            if (rtn == "Error")
            {
                MessageBox.Show(Environment.OperationManager.Err, "��������");
                return -1;
            }
            else if (rtn == "Before")
            {
                #region
                if (!CheckApplyTime)
                {
                    MessageBox.Show("����ʱ�䲻��С�ڵ�ǰʱ��!", "��ʾ");
                    //this.dtOperDate.Select();
                    this.dtOperDate.Focus();
                    //this.dtOperDate.ShowUpDown = true;
                    //this.dtOperDate.ShowUpDown = false;
                    return -1;
                }
                #endregion
            }
            else if (rtn == "Over")
            {
                if (this.cmbOperKind.SelectedIndex != 1)
                {
                    if (checkEmergency)
                    {
                        #region ������������������� ������ʾ
                        FS.HISFC.BizProcess.Integrate.Manager dp = new FS.HISFC.BizProcess.Integrate.Manager();

                        FS.HISFC.Models.Base.Department dd = dp.GetDepartment(Environment.OperatorDeptID);
                        if (dd.SpecialFlag != "1")
                        {
                            MessageBox.Show("�ѳ���������������Ľ�ֹʱ��,\n��ԤԼ���������ڽ�����������,���߽���������Ϊ����!", "��ʾ");
                            cmbOperKind.Focus();
                            return -1;
                        }
                        #endregion
                    }
                }
            }
            if (this.cmbSpecial.Text == string.Empty)
            {
                MessageBox.Show("��ѡ���Ƿ�����������");
                this.cmbSpecial.Focus();
                return -1;
            }

            #region У�����
            //{6C784A56-3FFD-47c3-A2A1-6382F7A7C7E1}

            if (this.txtDiag.Text.Trim() != string.Empty &&  this.txtDiag.Tag == null  )
            {
                MessageBox.Show("��¼��ġ���ǰ���һ��������,����������");
                this.txtDiag.Focus();
                return -1;
            }

            if (this.txtDiag2.Text.Trim() != string.Empty &&  this.txtDiag2.Tag == null  )
            {
                MessageBox.Show("��¼��ġ���ǰ��϶���������,����������");
                this.txtDiag2.Focus();
                return -1;
            }

            if (this.txtDiag3.Text.Trim() != string.Empty &&  this.txtDiag3.Tag == null )
            {
                MessageBox.Show("��¼��ġ���ǰ�������������,����������");
                this.txtDiag3.Focus();
                return -1;
            }


           

            if (this.txtOperation.Text.Trim() != string.Empty &&  this.txtOperation.Tag == null )
            {
                MessageBox.Show("��¼��ĵ�һ�����������ơ�������,����������");
                this.txtOperation.Focus();
                return -1;
            }


            if (this.txtOperation2.Text.Trim() != string.Empty &&  this.txtOperation2.Tag == null  )
            {
                MessageBox.Show("��¼��ĵڶ������������ơ�������,����������");
                this.txtOperation2.Focus();
                return -1;
            }

            if (this.txtOperation3.Text.Trim() != string.Empty &&  this.txtOperation3.Tag == null  )
            {
                MessageBox.Show("��¼��ĵ��������������ơ�������,����������");
                this.txtOperation3.Focus();
                return -1;
            }
            #endregion
            //�����Ȩ����Ҫ��д���ҽ��
            if (this.cmbOperKind.Tag == null)
            {
                MessageBox.Show("��ѡ���������");
                this.cmbOperKind.Focus();
                return -1;
            }

            if (this.isHavingApprove && this.cmbOperKind.Tag.ToString()=="1" && this.cmbApproveDoctor.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��ѡ�����ҽ��������д�����Ϣ��");
                this.cmbApproveDoctor.Focus();
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (Valid() == -1)
            {
                return -1;
            }

            if (this.GetValue() == -1)
            {
                return -1;
            }
           
            #region �ж��Ƿ�����ظ���������
            if (this.isNew)
            {
                //Ĭ��ȡ��һ�����Ϊͳ����ǰ���
                string strDiagnose = "";
                string strDiagName = "";
                if (this.applyoldMZ != null)
                {//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}������Դ��������������������
                    this.operationApplication.Appsourceid=applyoldMZ.ID;
                }
                if (operationApplication.DiagnoseAl.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase MainDiagnose in operationApplication.DiagnoseAl)
                    {
                        if (MainDiagnose.IsValid)
                        {
                            strDiagnose = MainDiagnose.Name + "(" + MainDiagnose.ID.ToString() + ")";
                            strDiagName = MainDiagnose.Name;
                        }
                    }
                }
                int i = Environment.OperationManager.IsExistSameApplication(operationApplication.PatientInfo.ID, strDiagnose, operationApplication.PreDate.ToString());
                if (i == -1) //��ѯ����
                {
                    MessageBox.Show("��ѯ����������Ϣ" + Environment.OperationManager.Err);
                    return -1;
                }
                if (i == 2) //���ظ��������Ϣ 
                {
                    System.Windows.Forms.DialogResult result = MessageBox.Show("����(" + operationApplication.PatientInfo.Name + ")�Ѿ�����(" + strDiagName + ")����������,�Ƿ�Ҫ��������һ��?", "��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            opsDiagnose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (this.isNew)//����
                {
                    //add by chengym 2012-3-12 �����Ȩ��ҽ���������ֱ�������
                    if (this.isNeedApprove && operationApplication.ApproveDoctor.ID!="")
                    {
                        if (operationApplication.ExecStatus == "1")
                        {
                            operationApplication.ExecStatus = "2";//1 ����״̬ 2 ���״̬ 3 �Ѱ��� 4 �ѵǼ�
                        }
                    }
                   
                    if (Environment.OperationManager.CreateApplication(operationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "��ʾ");
                        return -1;
                    }
                    //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}����������״̬
                    //if (SetOperationMzFinish() < 1)//����������������״̬Ϊ��ɣ����ʧ����ô��������
                    //{//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return -1;
                   
                    //}{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                    if (CancelApplyOldMZ() < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                else//�޸�
                {
                    //���ж�״̬
                    OperationAppllication obj = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
                    if (obj == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�޸����뵥��Ϣ!", "��ʾ");
                        return -1;
                    }
                    //1����2����3����4���
                    if (obj.ExecStatus == "3" || obj.ExecStatus == "4")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����뵥�ѱ����Ż�Ǽ�,���ܽ����޸�!", "��ʾ");
                        return -1;
                    }
                    if (obj.ExecStatus == "5")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����뵥�ѱ�ȡ���Ǽ�,���ܽ����޸�!", "��ʾ");
                        return -1;
                    }
                    if (obj.ExecStatus == "6")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����뵥��������δ�շ�״̬,���ܽ����޸�!", "��ʾ");
                        return -1;
                    }

                    if (obj.IsValid == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����뵥�Ѿ�����!", "��ʾ");
                        return -1;
                    }
                    //add by chengym 2012-3-12 �������
                    if (this.isNeedApprove && this.isHavingApprove && this.cmbOperKind.Tag.ToString()=="1")
                    {
                        if (operationApplication.ExecStatus == "1")
                        {
                            operationApplication.ExecStatus = "2";//1 ����״̬ 2 ���״̬ 3 �Ѱ��� 4 �ѵǼ�
                        }
                    }
                    //if (SetOperationMzFinish() < 1)//����������������״̬Ϊ��ɣ����ʧ����ô�������� {0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //{//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return -1;
                    //}
                    if (Environment.OperationManager.UpdateApplication(operationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "��ʾ");
                        return -1;
                    }
                    
                }
                #region �����Ϣ
                //ArrayList oldDiag = opsDiagnose.QueryOpsDiagnose(operationApplication.PatientInfo.ID, "7");
                //if (oldDiag == null)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("��ѯ�������ʧ��");
                //    return -1;
                //}

                // ArrayList IcdAl = opsDiagnose.QueryOpsDiagnose(operationApplication.PatientInfo.ID, "7");
                //if (IcdAl == null)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("��ѯ�������ʧ��");
                //    return -1;
                //}

                //ArrayList oldDiag = new ArrayList();
                //foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in IcdAl)
                //{
                //    if (diag.OperationNo == operationApplication.ID)
                //        oldDiag.Add(diag);
                //}

                int returnValue = opsDiagnose.DeleteDiagnoseByOperationNO(operationApplication.ID);

                ArrayList oldDiag = new ArrayList();

                bool bIsExist = false;
                //����Ҫ����������Ϣ�б�(OpsApp.DiagnoseAl)
                foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in operationApplication.DiagnoseAl)
                {
                    bIsExist = false;
                    //�����������е�����������ϣ����willAddDiagnose�Ѿ����ڣ�������״̬��
                    //���willAddDiagnose�в����ڣ��������ü�¼�����ݿ���
                    foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase thisDiagnose in oldDiag)
                    {
                        if (thisDiagnose.HappenNo == willAddDiagnose.HappenNo && thisDiagnose.Patient.ID.ToString() == willAddDiagnose.Patient.ID.ToString())
                        {
                            //�Ѿ�����	����				
                            if (opsDiagnose.UpdatePatientDiagnose(willAddDiagnose) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return -1; 
                            }
                            bIsExist = true;
                        }
                    }
                    //������Ϻ��ֲ����� ����
                    if (bIsExist == false)
                    {
                        if (opsDiagnose.CreatePatientDiagnose(willAddDiagnose) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                }
                #endregion 
                if (this.cmbExeDept.Tag != null)
                {
                    string[] str = new string[2];
                    str[0] = this.cmbExeDept.Text;
                    str[1] = this.cmbExeDept.Tag.ToString();
                    FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("ExeDept", str);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.ucDiag1.Visible = false;
                this.ucOpItem1.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }
            //��������,������ʾ
            if (this.operationApplication.OperateKind == "2")
                MessageBox.Show("������Ϊ��������,��绰֪ͨ������!", "��ʾ");
            // TODO: ����Ϣ
            //FS.HISFC.Common.Class.Message.SendMessage(this.lblDept.Text + "���ߣ�" + this.lblName.Text + "������������,�����!", this.operationApplication.ExeDept.ID);
            if (this.isNew)
            {
                //��ͨ��������
                if (this.isNeedApprove && this.operationApplication.OperateKind == "1" && this.operationApplication.ApproveDoctor.ID == "")
                {
                    MessageBox.Show("����ɹ�!�뼰ʱ֪ͨ����������������룡", "��ʾ");
                }
                else
                {
                    MessageBox.Show("����ɹ�!", "��ʾ");
                }
            }
            else
            {
                if (this.isNeedApprove && this.operationApplication.OperateKind == "1" && this.operationApplication.ApproveDoctor.ID != "")
                {
                    MessageBox.Show("������˳ɹ�!");
                }
                else
                {
                    MessageBox.Show("�����޸ĳɹ�����֪ͨ������!");
                }
            }
            
            if (isSavePrint)
            {
                if (MessageBox.Show("�Ƿ��ӡ�������뵥", "��ʾ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Print();
                }
            }

            //{C51C7189-3DA7-4ebc-9D1F-11864D26D059}
            this.OnSendMessage(null,"");
            if (this.isNew)
            {
                this.isNew = false;
            }
            this.Reset();
            return 0;
        }
        /// <summary>
        /// �����������������¼��״̬Ϊ1 {0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        /// </summary>
        /// <returns>1Ϊ�ɹ���-1Ϊʧ��</returns>
        private int SetOperationMzFinish()
        {
            if (this.applyoldMZ == null)
            {
                return 1;
            }
            if (Environment.OperationManager.DoAnaeRecord(this.applyoldMZ.ID, "4") < 1)
            {
                MessageBox.Show("�����շѱ��ʧ�ܣ�" + Environment.OperationManager.Err);
                return -1;
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// ��ʱ��Ϣ{C51C7189-3DA7-4ebc-9D1F-11864D26D059}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        protected override void OnSendMessage(object sender, string msg)
        {
            Patient p = this.operationApplication.PatientInfo.Clone();
            //��Ϣ����
            msg = "���ߣ�" + p.Name +"סԺ��:"+ p.PID.PatientNO+"\nΪ�����������뾡�������������!";
            //����
            FS.FrameWork.Models.NeuObject targetDept = this.cmbExeDept.Tag as FS.FrameWork.Models.NeuObject;

            base.OnSendMessage(targetDept, msg);

        }
        /// <summary>
        /// ���Ͼ������������뵥{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
        /// </summary>
        /// <returns></returns>
        public int CancelApplyOldMZ()
        {
            if (this.applyoldMZ != null && this.applyoldMZ.ID.Length > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (Environment.OperationManager.CancelApplication(this.applyoldMZ) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "��ʾ");
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message, "��ʾ");
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// �����Ѱ��ŵ��������������뵥
        /// </summary>
        /// <returns></returns>
        public int CancelApply()
        {
            if (this.isNew) return -1;
            if (this.operationApplication.ID.Length == 0)
            {
                MessageBox.Show("��ѡ����������뵥!", "��ʾ");
                return -1;
            }

            this.operationApplication = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
            if (this.operationApplication == null)
            {
                MessageBox.Show("��ȡ���뵥��Ϣ����!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "4")
            {
                MessageBox.Show("�����뵥�ѵǼ�,��������!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "5")
            {
                MessageBox.Show("�����뵥��ȡ���Ǽ�,�������ϣ�", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "6")
            {
                MessageBox.Show("�����뵥��������δ�շ�״̬,��������!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.IsValid == false)
            {
                MessageBox.Show("�����뵥�Ѿ�����!", "��ʾ");
                return -1;
            }
            if (MessageBox.Show("�Ƿ�Ըð��ŵ����뵥�����������뵽������?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;

            //1.�ж��Ƿ��Ѿ����͹���������
            //2.�����������������

            ucCancelApplicationApply ucCancelApplicationApply = new ucCancelApplicationApply();
            ucCancelApplicationApply.Application = this.OperationApplication;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(ucCancelApplicationApply);
            return 1;
            
        }

        /// <summary>
        /// ���ϵ�ǰ�޸����뵥
        /// </summary>
        /// <returns></returns>
        public int Cancel()
        {
            if (this.isNew) return -1;
            if (this.operationApplication.ID.Length == 0)
            {
                MessageBox.Show("��ѡ����������뵥!", "��ʾ");
                return -1;
            }

            this.operationApplication = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
            if (this.operationApplication == null)
            {
                MessageBox.Show("��ȡ���뵥��Ϣ����!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "4")
            {
                MessageBox.Show("�����뵥�ѵǼ�,��������!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "5")
            {
                MessageBox.Show("�����뵥��ȡ���Ǽ�,�������ϣ�", "��ʾ");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "6")
            {
                MessageBox.Show("�����뵥��������δ�շ�״̬,��������!", "��ʾ");
                return -1;
            }

            if (this.operationApplication.IsValid == false)
            {
                MessageBox.Show("�����뵥�Ѿ�����!", "��ʾ");
                return -1;
            }
            if (MessageBox.Show("�Ƿ����ϵ�ǰ���뵥?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;
            


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (Environment.OperationManager.CancelApplication(this.operationApplication) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Environment.OperationManager.Err, "��ʾ");
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

           // MessageBox.Show("��绰֪ͨ������!", "��ʾ");
            MessageBox.Show("���ϳɹ�!", "��ʾ");
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            //��ӡԤ��
            if (operationApplication.PatientInfo.ID == "")
                return -1;

            if (this.operationApplication.PreDate == System.DateTime.MinValue)
            {
                #region  �ж�������Ч��
                if (operationApplication.PatientInfo.ID == "")
                {
                    MessageBox.Show("��ѡ�����뻼��!", "��ʾ");
                    return -1;
                }
                if (this.txtDiag.Tag == null)
                {
                    MessageBox.Show("��ǰ��ϲ���Ϊ��!", "��ʾ");
                    return -1;
                }
                if (this.txtOperation.Tag == null)
                {
                    MessageBox.Show("���������Ʋ���Ϊ��!", "��ʾ");
                    return -1;
                }
                //if (this.cmbAnae.Tag == null || this.cmbAnae.Tag.ToString() == "")
                //{
                //    MessageBox.Show("����ʽ����Ϊ��!", "��ʾ");
                //    return -1;
                //}

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                //if (!this.cmbAnseWay.Visible)//chengym 12-8-17  ���岻֪������ӵģ����� 
                //{
                //    if (this.cmbAnseWay.Tag == null || this.cmbAnseWay.Tag.ToString() == "")
                //    {
                //        MessageBox.Show("���������Ϊ��!", "��ʾ");
                //        this.cmbAnseWay.Focus();
                //        return -1;
                //    }
                //}
                if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
                {
                    MessageBox.Show("�����Ҳ���Ϊ��!", "��ʾ");
                    return -1;
                }
                if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show("���߲���Ϊ��!", "��ʾ");
                    return -1;
                }
                if (this.cmbHelper1.Tag == null || this.cmbHelper1.Tag.ToString() == "")
                {
                    MessageBox.Show("һ������Ϊ��!", "��ʾ");
                    return -1;
                }
                string helper1 = "";
                string helper2 = "";
                string helper3 = "";
                this.cmbHelper1.Tag.ToString();
                if (cmbHelper2.Tag != null)
                {
                    helper2 = this.cmbHelper2.Tag.ToString();
                }
                if (this.cmbHelper3.Tag != null)
                {
                    helper3 = this.cmbHelper3.Tag.ToString();
                }
                if ((helper1 == helper2 && helper1 != "") || (helper1 == helper3 && helper3 != "") || (helper2 == helper3 && helper3 != ""))
                {
                    MessageBox.Show("һ���������������ظ�");
                    return -1;
                }
                if (this.cmbOrder.Text == "")
                {
                    MessageBox.Show("��ָ��̨��!", "��ʾ");
                    return -1;
                }
                //�ж�����ʱ���Ƿ�Ϸ�
                string rtn = Environment.OperationManager.PreDateValidity(this.dtOperDate.Value);
                if (rtn == "Error")
                {
                    MessageBox.Show(Environment.OperationManager.Err, "��������");
                    return -1;
                }
                else if (rtn == "Before")
                {
                    MessageBox.Show("����ʱ�䲻��С�ڵ�ǰʱ��!", "��ʾ");
                    return -1;
                }
                #endregion

            }
            if (GetValue() == -1)
                return -1;

            #region ɾ��ԭ���������뵥��Ϣ �� ��������֪ͨ������
            //			ucCreateAppPrint ucCreateAppPrint1 = new ucCreateAppPrint();
            //			FS.HISFC.Object.RADT.PatientInfo patient=patientMgr.PatientQuery(operationApplication.PatientInfo.Patient.ID);
            //			if(patient==null)return -1;
            //			FS.HISFC.Object.Operator.OpsApplication t=operationApplication.Clone();
            //			t.PatientInfo=patient;
            //
            //			ucCreateAppPrint1.ControlValue = t;
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.ControlBorder = FS.HISFC.Components.Interface.Classes.enuControlBorder.None;

            //p.PrintPreview(10, 40, ucCreateAppPrint1);

            #endregion


            if (this.arrangeFormPrint == null)
            {
                this.arrangeFormPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint)) as FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint;
                if (this.arrangeFormPrint == null)
                {
                    MessageBox.Show("��ýӿ�IArrangeNotifyFormPrint��������ϵͳ����Ա��ϵ��");

                    return -1;
                }
            }

            this.arrangeFormPrint.OperationApplicationForm = this.operationApplication.Clone();
            this.arrangeFormPrint.IsPrintExtendTable = false;
            this.arrangeFormPrint.Print();

            if (isupdatestate) //�Ƿ����״̬
            {
             
                //if (Environment.OperationManager.DoAnaeRecord(this.operationApplication.ID, "3")!=1)
                //{
                //    MessageBox.Show("�������뵥״̬ʧ�ܡ�");
                //}
                 
            }



            //this.arrangeFormPrint.PrintPreview();

            return 0;
        }


       

        #region ����
        FS.HISFC.Components.Operation.ucOpItem ucOpItem1 = null; 
        int ucOpItem1_SelectItem(Keys key)
        {
            this.ProcessOps();
            this.txtOperation.Focus();
            return 1;
        }
        private int ProcessOps()
        {
            FS.HISFC.Models.Fee.Item.Undrug item = null;
            if (this.ucOpItem1.GetItem(ref item) == -1)
            {
                //MessageBox.Show("��ȡ��Ŀ����!","��ʾ");
                return -1;
            }
            dirty = true;
            this.contralActive.Text = (item as FS.HISFC.Models.Fee.Item.Undrug).Name;
            dirty = false;

            this.contralActive.Tag = item;
            this.ucOpItem1.Visible = false;

            return 0;
        }
        private void txtOperation_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation2_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation2;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation3_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation3;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                this.txtOperation2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                this.txtOperation3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnae.Focus();
               // this.cmbAnseWay.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation_TextChanged(object sender, EventArgs e)
        {

            if (!txtOperation.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation.Location.X, txtOperation.Location.Y + txtOperation.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation.Tag = null;
        }

        private void txtOperation2_TextChanged(object sender, EventArgs e)
        {
            if (!txtOperation2.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation2.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation2.Location.X, txtOperation2.Location.Y + txtOperation2.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation2.Tag = null;
        }

        private void txtOperation3_TextChanged(object sender, EventArgs e)
        {
            if (!txtOperation3.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation3.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation3.Location.X, txtOperation3.Location.Y + txtOperation3.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation3.Tag = null;
        }

        #endregion

        #region ���
        FS.HISFC.Components.Common.Controls.ucDiagnose ucDiag1 = null;
        int ucDiag1_SelectItem(Keys key)
        {
            this.ProcessDiag();
            this.txtDiag.Focus();
            return 1;
        } 
        private int ProcessDiag()
        {
            FS.HISFC.Models.HealthRecord.ICD item = null;
            if (this.ucDiag1.GetItem(ref item) == -1)
            {
                //MessageBox.Show("��ȡ��Ŀ����!","��ʾ");
                return -1;
            }
            dirty = true;
            this.contralActive.Text = (item as FS.HISFC.Models.HealthRecord.ICD).Name;
            dirty = false;

            this.contralActive.Tag = item;
            this.ucDiag1.Visible = false;

            return 0;
        }
        private void txtDiag_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag2_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag2;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag3_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag3;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtDiag2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtDiag3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtOperation.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag;
            string text = this.txtDiag.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag.Location.X, txtDiag.Location.Y + txtDiag.Height + 2);
            ucDiag1.BringToFront();
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag.Tag = null;
            ucDiag1.BringToFront();
        }

        private void txtDiag2_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag2.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag2;
            string text = this.txtDiag2.Text;
            ucDiag1.BringToFront();
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag2.Location.X, txtDiag2.Location.Y + txtDiag2.Height + 2);
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag2.Tag = null;
        }

        private void txtDiag3_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag3.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag3;
            string text = this.txtDiag3.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag2.Location.X, txtDiag3.Location.Y + txtDiag3.Height + 2);
            ucDiag1.BringToFront();
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag3.Tag = null;
        }
        #endregion 
        /// <summary>
        /// �ֹ�¼����Ϻ�����������
        /// {6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
        /// </summary>
        /// <returns>�ɹ�����1</returns>
        private int InputBySelf()
        {
            if (this.chkInputBySelf.Checked==false)
            {
                return -1;
            }
            else 
            {
                FS.FrameWork.Models.NeuObject obj = null;
                FS.HISFC.Models.Base.Item item = null;
                //��һ����
                if (this.txtOperation.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S991";
                    item.Name = this.txtOperation.Text;
                    item.PriceUnit = "��";
                    this.txtOperation.Tag = item;
                }
                //�ڶ�����
                if (this.txtOperation2.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S992";
                    item.Name = this.txtOperation2.Text;
                    item.PriceUnit = "��";
                    this.txtOperation2.Tag = item;
                }
                //��������
                if (this.txtOperation3.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S993";
                    item.Name = this.txtOperation3.Text;
                    item.PriceUnit = "��";
                    this.txtOperation3.Tag = item;
                }
                //��һ���
                if (this.txtDiag.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D991";
                    obj.Name = this.txtDiag.Text;
                    this.txtDiag.Tag = obj;
                }
                //�ڶ����
                if (this.txtDiag2.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D992";
                    obj.Name = this.txtDiag2.Text;
                    this.txtDiag2.Tag = obj;
                }
                //�������
                if (this.txtDiag3.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D993";
                    obj.Name = this.txtDiag3.Text;
                    this.txtDiag3.Tag = obj;
                }          
            }
            return 1;
        }
        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!this.DesignMode)
            //{
            //    this.Reset();
            //    this.Init();
            //}
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Escape.GetHashCode())
            {
                this.ucOpItem1.Visible = false;
                this.ucDiag1.Visible = false;
                if (txtDiag.Focused)
                {
                    if (txtDiag.Text.Trim() == "")
                    {
                        txtDiag.Tag = null;
                    }
                }
                if (txtDiag3.Focused)
                {
                    if (txtDiag3.Text.Trim() == "")
                    {
                        txtDiag3.Tag = null;
                    }
                }
                if (txtDiag2.Focused)
                {
                    if (txtDiag2.Text.Trim() == "")
                    {
                        txtDiag2.Tag = null;
                    }
                }
                if (txtOperation.Focused)
                {
                    if (txtOperation.Text.Trim() == "")
                    {
                        txtOperation.Tag = null;
                    }
                }
                if (txtOperation3.Focused)
                {
                    if (txtOperation3.Text.Trim() == "")
                    {
                        txtOperation3.Tag = null;
                    }
                }
                if (txtOperation2.Focused)
                {
                    if (txtOperation2.Text.Trim() == "")
                    {
                        txtOperation2.Tag = null;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint) };
            }
        }

        #endregion 

        private void cmbAnae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbAnae2.Focus();
            }
        }
        private void cmbAnae2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbSpecial.Focus();
            }
        }
        private void dtOperDate_KeyDown(object sender, KeyEventArgs e)
        {//
            if (e.KeyCode == Keys.Enter)
            {
                cmbExeDept.Focus();
            }
        }

        private void cmbExeDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTableType.Focus();
            }
            
        }

        private void cmbTableType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbDoctor.Focus();
            }
        }

        private void cmbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comDept.Focus();
            }
        }

        private void comDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOrder.Focus();
            }
        }

        private void cmbHelper1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper2.Focus();
            }
        }

        private void cmbHelper2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper3.Focus();
            }
        }

        private void cmbHelper3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOpType.Focus();
            }
        }

        private void cmbSpecial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper1.Focus();
            }
        }

        private void cmbOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper1.Focus();
            }
        }

        private void cbxNeedQX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxNeedXH.Focus();
            }
        }
        private void cmbOpType_KeyDown(object sender, KeyEventArgs e)
        {
            this.cmbIncitepe.Focus();
        }
        private void cbxNeedXH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOwn.Focus();
            }

        }

        private void cmbOwn_KeyDown(object sender, KeyEventArgs e)
        {//rtbApplyNote
            if (e.KeyCode == Keys.Enter)
            {
                cmbApplyDoct.Focus();
            }
        }

        private void ucApplicationForm_Load(object sender, EventArgs e)
        {

        }

        ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
        private void cmbAnseWay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbAnae.Focus();
            }
        }
        //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
        private void cmbIncitepe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxNeedQX.Focus();
            }
        }

        /// <summary>
        /// ����ҽ��ְ����Ӧ��ʾ��������  {8794572D-2030-4692-B7B8-95000D75F698}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.comDept.Tag = ((FS.HISFC.Models.Base.Employee)(((FS.FrameWork.WinForms.Controls.NeuComboBox)(sender)).SelectedItem)).Dept.ID;
                
                this.cmbOpType.ClearItems();

                ArrayList al = Environment.IntegrateManager.GetConstantList("OPERATETYPE");


                ArrayList alType = new ArrayList();
                // "LEVELOPERATETYPE"
                // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                if (isOwnPrivilege)
                {
                    NeuObject NeuObj = Environment.IntegrateManager.GetConstant("LEVELOPERATETYPE", ((FS.HISFC.Models.Base.Employee)(((FS.FrameWork.WinForms.Controls.NeuComboBox)(sender)).SelectedItem)).Level.ID);

                    if (string.IsNullOrEmpty(NeuObj.Memo))
                    {
                        return;
                    }

                    string[] operateType = NeuObj.Memo.Split('|');



                    foreach (NeuObject obj in al)
                    {
                        for (int i = 0; i < operateType.Length; i++)
                        {
                            if (obj.ID == operateType[i])
                            {
                                alType.Add(obj);
                            }
                        }
                    }


                }
                else
                {
                    if (string.IsNullOrEmpty(cmbDoctor.Tag.ToString()))
                    {
                        return;
                    }

                    FS.HISFC.BizLogic.Order.Medical.Ability abilityManager = new FS.HISFC.BizLogic.Order.Medical.Ability();
                    List<FS.HISFC.Models.Order.Medical.Popedom> popedomList = new List<FS.HISFC.Models.Order.Medical.Popedom>();
                    popedomList = abilityManager.QueryPopedomByEmplID(cmbDoctor.Tag.ToString());

                    foreach (NeuObject obj in al)
                    {
                        foreach (FS.HISFC.Models.Order.Medical.Popedom popedom in popedomList)
                        {
                            if (popedom.PopedomType.ID == "0")
                            {
                                if (obj.ID == popedom.Popedoms.ID)
                                {
                                    alType.Add(obj);
                                }
                            }

                        }
                    }

                }

                this.cmbOpType.AddItems(alType);
            }
            catch
            {

            }
            finally
            {

            }
        }

        /// <summary>
        /// �ֹ�¼���л�ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInputBySelf_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkInputBySelf.Checked)//���ֹ�¼���л������ֹ�¼��ʱ����text��գ���������ѡ����Ϻ���������
            {
                DialogResult r = MessageBox.Show("�Ƿ�ȷ��Ҫת�ɷ��ֹ�¼�룿"+"\r\n"+"���ѡ��'ȷ��',��ǰ��Ϻ����������ƽ������", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    this.txtDiag.Text = string.Empty;
                    this.txtDiag2.Text = string.Empty;
                    this.txtDiag3.Text = string.Empty;
                    this.txtOperation.Text = string.Empty;
                    this.txtOperation2.Text = string.Empty;
                    this.txtOperation3.Text = string.Empty;
                }
                else
                {
                    this.chkInputBySelf.Checked = true;
                }
            }
            else
            {
                InputBySelf();
            }
        }

        private void SetNote()
        {
            //����ע������
            FS.HISFC.BizProcess.Integrate.Manager ctlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            try
            {	//��ѯ�����������ʱ��
                string control = ctlMgr.QueryControlerInfo("optime");

                if (control != "" && control != "-1") this.lbNote.Text = "Ҫ����" + control + this.note;//"ǰ�����������룬����ʹ�ý�̨��

                if (this.cmbExeDept.Items.Count > 0)
                {
                    ArrayList list = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ExeDept");
                    if (list.Count == 2)
                    {
                        this.cmbExeDept.Text = list[0].ToString();
                        this.cmbExeDept.Tag = list[1].ToString();
                    }
                }
            }
            catch { }
        }


        public void Clean()
        {
            Reset();
          
        }


        public int Update(string state)
        {

            if (this.operationApplication == null && string.IsNullOrEmpty(operationApplication.PatientInfo.ID))
            {
                MessageBox.Show("��ѡ����Ҫ���������뵥��");
                return -1;
            }
            else
            {
                if (Environment.OperationManager.DoAnaeRecord(this.operationApplication.ID, state)!=1)
                {
                    MessageBox.Show("�������뵥״̬ʧ�ܡ�");
                    return -1;
                }
            }
            if (state == "3")
            {
                MessageBox.Show("�����������Ű࣡");
                return -1;
            }
            else if (state == "4")
            {
                MessageBox.Show("�����������շѣ����߳�Ժ���������ƣ�");
            }
            return 1;
        }

        private void cmbOperKind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBodyPosion.Focus();
            }

        }

        private void cmbBodyPosion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPortPosition.Focus();
            }
        }

        private void txtPortPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiag.Focus();
            }
            
        }

        private void cmbSpecial_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbExeDept.Focus();
            }
        }

        private void cmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alTableTypeCompare == null || alTableTypeCompare.Count == 0)
            {
                return;
            }
            this.cmbOrder.ClearItems();
            this.cmbOrder.AddItems(hsTableTypeCompare[this.cmbTableType.Text] as ArrayList);
        }

        private void dtOperDate_ValueChanged(object sender, EventArgs e)
        {
            this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);
        }

        private void cmbSpecial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
