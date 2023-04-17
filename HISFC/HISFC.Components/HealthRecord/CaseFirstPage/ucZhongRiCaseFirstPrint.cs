using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections; 

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucZhongRiCaseFirstPrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface
    {
        public ucZhongRiCaseFirstPrint()
        {
            InitializeComponent();
            LoadInfo();
        }

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        ucZhongRiCaseBackPrint caseBackPrint = new ucZhongRiCaseBackPrint();
        //�������
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        FS.FrameWork.Public.ObjectHelper AnaphyHelper = null;
        #endregion
        #region ˽�к���
        /// <summary>
        /// ����ǰ����ת����Ϣ
        /// </summary>
        /// <param name="list"></param>
        private void LoadChangeDept(ArrayList list)
        {
            FS.HISFC.Models.RADT.Location firDept = null;
            FS.HISFC.Models.RADT.Location secDept = null;
            FS.HISFC.Models.RADT.Location thirDept = null;

            if (list == null)
            {
                return;
            }

            #region ת����Ϣ��ǰ�����ڽ�������ʾ
            if (list.Count > 0) //��ת����Ϣ
            {
                //ת����Ϣ��ǰ�����ڽ�������ʾ
                int j = 0;
                if (list.Count >= 3)
                {
                    j = 3;
                }
                else
                {
                    j = list.Count;
                }
                for (int i = 0; i < j; i++)
                {
                    switch (i)
                    {
                        case 0:
                            firDept = (FS.HISFC.Models.RADT.Location)list[0];
                            break;
                        case 1:
                            secDept = (FS.HISFC.Models.RADT.Location)list[1];
                            break;
                        case 2:
                            thirDept = (FS.HISFC.Models.RADT.Location)list[2];
                            break;
                    }
                }
            }
            #endregion

            #region ת����Ϣ
            if (firDept != null)
            {
                //changeDeptFirstComboBox.Text = firDept.Dept.Name;
                //changeDeptFirstComboBox.Tag = firDept.Dept.ID;
                System.DateTime dd = FS.FrameWork.Function.NConvert.ToDateTime(firDept.User01);
                this.CYear2.Text = dd.Year.ToString();
                this.CMon2.Text = dd.Month.ToString();
                this.CDay2.Text = dd.Day.ToString();
            }
            if (secDept != null)
            {
                //changeDeptSecondComboBox.Text = secDept.Dept.Name;
                //changeDeptSecondComboBox.Tag = secDept.Dept.ID;
                System.DateTime mm = FS.FrameWork.Function.NConvert.ToDateTime(secDept.User01);
                //this.CMon3.Text = mm.Month.ToString();
                //this.CDay3.Text = mm.Day.ToString();
            }
            if (thirDept != null)
            {
                //changeDeptThirdComboBox.Text = thirDept.Dept.Name;
                //changeDeptThirdComboBox.Tag = thirDept.Dept.ID;
                System.DateTime cc = FS.FrameWork.Function.NConvert.ToDateTime(thirDept.User01);
                //this.CMon4.Text = cc.Month.ToString();
                this.CDay4.Text = cc.Day.ToString();
            }
            #endregion
        }
        #endregion

        #region ��ʱ��
        public void LoadInfo()
        {
            try
            {
                ArrayList alZG = new ArrayList();
                ArrayList alDepts = null;//��Ժ����
                ArrayList alDoctors = null;//��Ժҽ��
                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.BizLogic.Manager.Department managerDept = new FS.HISFC.BizLogic.Manager.Department();
                //��ʼ�����㷽ʽ
        //        this.payKindCbx.ShowCustomerList = false;
        //        this.payKindCbx.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
        //        //��ʼ���Ա�:
        //        this.sexComboBox.ShowCustomerList = false;
        //        this.sexComboBox.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
        //        //��ʼ��������Ϣ:
        //        this.marryComboBox.ShowCustomerList = false;
        //        this.marryComboBox.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());
        //        //��ʼ��������Ϣ:
                this.neuComboBox1.ShowCustomerList = false;
                this.neuComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION));
                //��ʼ����������Ϣ:
                this.birthInComboBox1.ShowCustomerList = false;
                this.birthInComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.AREA));
 
                //��ʼ��������Ϣ
                 this.nationComboBox1.ShowCustomerList = false;
                 this.nationComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.NATION));
                //��ʼ��������Ϣ
                //this.districtComboBox1.ShowCustomerList = false;
                //this.districtComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.DIST));
                 //��ʼ���뻼�߹�ϵ��Ϣ
                 this.neuComboBox2.ShowCustomerList = false;
                 this.neuComboBox2.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE));
                 AnaphyHelper = new FS.FrameWork.Public.ObjectHelper(Constant.GetList("PHARMACYALLERGIC"));
        //        //��ʼ����Ժ�����Ϣ
        //        this.inCircsComboBox.ShowCustomerList = false;
        //        this.inCircsComboBox.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS));


        //        alDoctors = p.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
        //        //�ʿ�ҽ��
        //        this.QcDocComboBox.AddItems(alDoctors);
        //        //����ҽ��
        //        this.chiefDocComboBox.AddItems(alDoctors);
        //        //����ҽ��
        //        this.chargeDocComboBox.AddItems(alDoctors);
        //        //סԺҽ��
        //        this.houseDocComboBox.AddItems(alDoctors);
        //        //ʵϰҽ��
        //        this.refDocComboBox.AddItems(alDoctors);
        //        //ʵϰ
        //        this.praDocComboBox.AddItems(alDoctors);
        //        //�о���
        //        this.graDocComboBox.AddItems(alDoctors);
        //        //��ʿ
        //        this.QcNurComboBox.AddItems(p.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N));
        //        //����Ա
        //        this.operComboBox.AddItems(p.GetEmployeeAll());

        //        //ת��
        //        alZG = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);

        //        try
        //        {
        //            alDepts = managerDept.GetInHosDepartment();
        //        }
        //        catch { MessageBox.Show("�����Ժ���ҳ���"); }

        //        //ת������
        //        inDeptComboBox.AddItems(alDepts);
        //        this.changeDeptFirstComboBox.AddItems(alDepts);
        //        this.changeDeptSecondComboBox.AddItems(alDepts);
        //        this.changeDeptThirdComboBox.AddItems(alDepts);
        //        this.outDeptComboBox.AddItems(alDepts);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department inpatientManager = new FS.HISFC.BizLogic.Manager.Department();

        #region HealthRecordInterface ��Ա

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        {
            #region ��ֵ
            FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
            FS.HISFC.BizLogic.HealthRecord.DeptShift deptMger = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
            FS.HISFC.BizLogic.HealthRecord.Fee feeCaseMgr = new FS.HISFC.BizLogic.HealthRecord.Fee();
            FS.HISFC.Models.HealthRecord.Base myItem = obj as FS.HISFC.Models.HealthRecord.Base;

            this.payKindCbx.Text = GetPaykindForCaseFirst(myItem.PatientInfo.Pact.ID);// myItem.PatientInfo.Pact.Name;//.PayKind.ID;//���ѷ�ʽ��
            this.cardNO.Text = myItem.PatientInfo.PID.CardNO;//���￨��
            this.xNO.Text = myItem.XNum;//X���

            this.InpatientNO.Text = myItem.PatientInfo.PID.PatientNO;//סԺ��
            this.medCardTextBox.Text = myItem.PatientInfo.SSN;//�籣��
            txtInHosNo.Text = myItem.PatientInfo.InTimes.ToString();//��Ժ����
            //this.inpatientNOTextBox.Text = myItem.PatientInfo.ID; //סԺ��ˮ��
            this.nameTextBox.Text = myItem.PatientInfo.Name;//����
            this.sexComboBox.Text = myItem.PatientInfo.Sex.Name;//�Ա�
            if (myItem.PatientInfo.Sex.ID.ToString() == "M")
            {
                sexComboBox.Text = "1";
            }
            else if (myItem.PatientInfo.Sex.ID.ToString() == "F")
            {
                sexComboBox.Text = "2";
            }
            //if(myItem.PatientInfo.MainDiagnose.ToString()!= null)
            if (myItem.PatientInfo.ClinicDiagnose != null)
                this.inDiagComboBox.Text = myItem.PatientInfo.ClinicDiagnose.ToString(); //סԺ���
            this.birYear.Text = myItem.PatientInfo.Birthday.Year.ToString();//��������
            this.birMon.Text = myItem.PatientInfo.Birthday.Month.ToString();//��������
            this.birDay.Text = myItem.PatientInfo.Birthday.Day.ToString();//��������
            this.ageTextBox.Text = this.inpatientManager.GetAge(myItem.PatientInfo.Birthday);
            //this.ageTextBox.Text = myItem.PatientInfo.Age;//����
            //this.marryComboBox.Tag = myItem.PatientInfo.MaritalStatus.ID;//����״��

            switch (myItem.PatientInfo.MaritalStatus.ID.ToString())
            {
                case "M":
                    marryTextBox.Text = "2";
                    break;
                case "W":
                    marryTextBox.Text = "4";
                    break;
                case "A":
                    marryTextBox.Text = "3";
                    break;
                case "D":
                    marryTextBox.Text = "3";
                    break;
                case "R":
                    marryTextBox.Text = "2";
                    break;
                case "S":
                    marryTextBox.Text = "1";
                    break;
            }
            //marryTextBox.Text = myItem.PatientInfo.ClinicDiagnose.ToString();// MaritalStatus.ID.ToString();
            this.neuComboBox1.Tag = myItem.PatientInfo.Profession.ID;
            this.workComboBox.Text = this.neuComboBox1.Text;
            //this.workComboBox.Tag = myItem.PatientInfo.Profession.ID;//ְҵ
            //this.workComboBox.Text = myItem.PatientInfo.Profession.Name;

            this.birthInComboBox1.Tag = myItem.PatientInfo.AreaCode; //������ 
            //this.birthInComboBox.Text = this.birthInComboBox1.Text;
            this.birthInComboBox.Text = myItem.PatientInfo.AreaCode; //������ 
            //switch (myItem.PatientInfo.Nationality.ID.ToString())
            //{
            //    case "1":
            //        this.nationComboBox.Text = "����";
            //}


            this.nationComboBox1.Tag = myItem.PatientInfo.Nationality.ID;//.ID;//����
            this.nationComboBox.Text = this.nationComboBox1.Text;   //myItem.PatientInfo.Country.ToString();
            this.districtComboBox.Text = myItem.PatientInfo.Country.ToString(); //����
            //this.districtComboBox.Text = myItem.PatientInfo.DIST; //����
            this.IDTextBox.Text = myItem.PatientInfo.IDCard;//���֤
            this.workAdressTextBox.Text = myItem.PatientInfo.AddressBusiness; ;//������λ��ַ
            this.workPhoneTextBox.Text = myItem.PatientInfo.PhoneBusiness;//������λ�绰
            this.workZipTextBox.Text = myItem.PatientInfo.BusinessZip;//�ʱࡡ
            this.homeAdTextBox.Text = myItem.PatientInfo.AddressHome;//��ͥסַ
            this.homeZipTextBox.Text = myItem.PatientInfo.HomeZip;//�ʱࡡ
            this.linkNameTextBox.Text = myItem.PatientInfo.Kin.Name;//��ϵ������
            //this.relationComboBox.Tag = myItem.PatientInfo.Kin.RelationLink;//��ϵ�˹�ϵ
            this.neuComboBox2.Tag = myItem.PatientInfo.Kin.Relation.ID;// PatientInfo.Kin.RelationLink;
            this.relationComboBox.Text = myItem.PatientInfo.Kin.Relation.Name;//��ϵ�˹�ϵ

            //this.relationComboBox.Text =  myItem.PatientInfo.Kin.Relation.Name;
            this.linkAdressTextBox.Text = myItem.PatientInfo.Kin.RelationAddress; //��ϵ�˵�ַ �д�����;
            this.linkPhoneTextBox.Text = myItem.PatientInfo.Kin.RelationPhone; //��ϵ�˵绰 �д�����;

            //�ɱ�����ȡ ��Ժ����
            FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(myItem.PatientInfo.ID);
            if (indept != null) //��Ժ���� 
            {
                //��Ժ���Ҵ���
                inDeptComboBox.Tag = indept.Dept.ID;
                //��Ժ��������
                inDeptComboBox.Text = indept.Dept.Name;
            }
            else
            {
                this.inDeptComboBox.Tag = myItem.PatientInfo.PVisit.PatientLocation.Dept.ID;
                this.inDeptComboBox.Text = myItem.PatientInfo.PVisit.PatientLocation.Dept.Name;
            }
            //�ɱ�����ȡ ��Ժ����
            FS.HISFC.Models.RADT.Location outDept = baseDml.GetDeptOut(myItem.PatientInfo.ID);
            if (outDept != null)
            {
                this.outDeptComboBox.Tag = outDept.Dept.ID;
                this.outDeptComboBox.Text = outDept.Dept.Name;
            }

            this.CYear2.Text = myItem.PatientInfo.PVisit.InTime.Year.ToString();//��Ժʱ��
            this.CMon2.Text = myItem.PatientInfo.PVisit.InTime.Month.ToString();//��Ժʱ��
            this.CDay2.Text = myItem.PatientInfo.PVisit.InTime.Day.ToString();//��Ժʱ��
            this.neuLabel21.Text = myItem.PatientInfo.PVisit.Circs.ID;//��Ժ���

            this.txtDiagYear.Text = myItem.DiagDate.Year.ToString(); //ȷ������
            this.txtDiagMonth.Text = myItem.DiagDate.Month.ToString(); //ȷ������
            this.txtDiagDay.Text = myItem.DiagDate.Day.ToString(); //ȷ������

            //��Ժʱ�䲻������Сʱ���һ���״̬Ϊ ��Ժ�Ǽ�״̬
            if (myItem.PatientInfo.PVisit.OutTime != System.DateTime.MinValue && myItem.PatientInfo.PVisit.InState.ID.ToString() == "B")
            {
                this.OutYear.Text = myItem.PatientInfo.PVisit.OutTime.Year.ToString();//��Ժ����
                this.OutMon.Text = myItem.PatientInfo.PVisit.OutTime.Month.ToString();//��Ժ����
                this.OutDay.Text = myItem.PatientInfo.PVisit.OutTime.Day.ToString();//��Ժ����
            }
            this.ClinicDiag.Text = myItem.ClinicDiag.Name;//�������
            this.inDiagComboBox.Text = myItem.InHospitalDiag.Name;//��Ժ��� 

            #region ��ʱ���� ��ӡ��ʱ�� ����Ҫ��ӡ��Ա
            //				houseDocComboBox.Tag = myItem.PatientInfo.PVisit.AdmittingDoctor.ID ;
            //				houseDocTextBox.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.ID ;
            //				//סԺҽʦ����
            //				houseDocComboBox.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.Name;
            //				//����ҽʦ����
            //				chargeDocComboBox.Tag = myItem.PatientInfo.PVisit.AttendingDoctor.ID;
            //				chargeDocComboBox.Text = myItem.PatientInfo.PVisit.AttendingDoctor.Name;
            //				chargeDocTextBox.Text =  myItem.PatientInfo.PVisit.AttendingDoctor.ID;
            //				//����ҽʦ����
            //				chiefDocComboBox.Tag = myItem.PatientInfo.PVisit.ConsultingDoctor.ID;
            //				chiefDocComboBox.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.Name;
            //				chiefDocTextBox.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.ID;
            //				//�����δ���
            //				//			info.PVisit.ReferringDoctor.ID
            //				//����ҽʦ����
            //				refDocComboBox.Tag = myItem.RefresherDocd;
            //				refDocTextBox.Text = myItem.RefresherDocd;
            //				refDocComboBox.Text = myItem.RefresherDonm;
            //				//�о���ʵϰҽʦ����
            //				graDocComboBox.Tag = myItem.GraDocCode;
            //				graDocComboBox.Text = myItem.GraDocName;
            //				//ʵϰҽʦ����
            //				praDocComboBox.Tag = myItem.PraDocCode;
            //				praDocComboBox.Text = myItem.PraDocName;
            #endregion

            //if (this.OutYear.Text != "1")
            //{
            //    int inDays = (int)new System.TimeSpan(myItem.PatientInfo.PVisit.OutTime.Ticks
            //        - myItem.PatientInfo.PVisit.InTime.Ticks).TotalDays;
            //    if (inDays > 0)
            //        this.inDaysTextBox.Text = inDays.ToString();//סԺ����
            //}
            //else
            //{
            //    this.inDaysTextBox.Text = "";
            //}
            this.inDaysTextBox.Text = myItem.InHospitalDays.ToString();//סԺ����


            //this.inSourceComboBox.Tag = myItem.PatientInfo.PVisit.InSource.ID;//��Ժ��Դ


            //�����Ϣ
            FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList alDiag = new ArrayList();

            //����ֻ��ҽ��¼�벡��
            alDiag = diag.QueryCaseDiagnose(myItem.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
            if (alDiag.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diagNose = new FS.HISFC.Models.HealthRecord.Diagnose();

                //foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in alDiag)
                #region ��ϸ�ֵ
                int row = 0;
                for (int i=0;i<alDiag.Count;i++)
                {
                    diagNose = alDiag[i] as FS.HISFC.Models.HealthRecord.Diagnose;
                    if (diagNose.DiagInfo.IsMain)
                    {
                        this.mainDiagICD.Text = diagNose.DiagInfo.ICD10.ID;
                        this.MainDiagName.Text = diagNose.DiagInfo.ICD10.Name;
                        switch (diagNose.DiagOutState)
                        {
                            case "1":
                                this.MainDiag1.Text = "��";
                                break;
                            case "2":
                                this.MainDiag2.Text = "��";
                                break;
                            case "3":
                                this.MainDiag3.Text = "��";
                                break;
                            case "4":
                                this.MainDiag4.Text = "��";
                                break;
                            case "5":
                                this.MainDiag5.Text = "��";
                                break;
                        }

                    }
                    else if(diagNose.DiagInfo.DiagType.ID=="6")//�������
                    {
                        this.txtDiagClPa.Text = diagNose.DiagInfo.ICD10.Name;
                    }
                    else if(diagNose.DiagInfo.DiagType.ID=="10")//�������
                    {
                        this.ClinicDiag.Text = diagNose.DiagInfo.ICD10.Name;
                    }
                    else if (diagNose.DiagInfo.DiagType.ID == "11")//��Ժ���
                    {
                        this.inDiagComboBox.Text = diagNose.DiagInfo.ICD10.Name;
                    }
                    else if (diagNose.DiagInfo.DiagType.ID == "4")
                    {
                        this.txtInfectionPosition.Text += diagNose.DiagInfo.ICD10.Name+" ";
                    }
                    else
                    {
                        if (i > 6)
                        {
                            continue;
                        }
                        row = row + 1;
                        switch (row)
                        {
                            case 1:
                                this.otherDiagName1.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode1.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag11.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag12.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag13.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag14.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag15.Text = "��";
                                        break;
                                }
                                break;
                            case 2:
                                this.otherDiagName2.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode2.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag21.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag22.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag23.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag24.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag25.Text = "��";
                                        break;
                                }
                                break;
                            case 3:
                                this.otherDiagName3.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode3.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag31.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag32.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag33.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag34.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag35.Text = "��";
                                        break;
                                }
                                break;
                            case 4:
                                this.otherDiagName4.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode4.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag41.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag42.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag43.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag44.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag45.Text = "��";
                                        break;
                                }
                                break;
                            case 5:
                                this.otherDiagName5.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode5.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag51.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag52.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag53.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag54.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag55.Text = "��";
                                        break;
                                }
                                break;
                            case 6:
                                this.otherDiagName6.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode6.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag61.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag62.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag63.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag64.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag65.Text = "��";
                                        break;
                                }
                                break;
                            case 7:
                                this.otherDiagName7.Text = diagNose.DiagInfo.ICD10.Name;
                                this.otherDiagCode7.Text = diagNose.DiagInfo.ICD10.ID;
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.otherDiag71.Text = "��";
                                        break;
                                    case "2":
                                        this.otherDiag72.Text = "��";
                                        break;
                                    case "3":
                                        this.otherDiag73.Text = "��";
                                        break;
                                    case "4":
                                        this.otherDiag74.Text = "��";
                                        break;
                                    case "5":
                                        this.otherDiag75.Text = "��";
                                        break;
                                }
                                break;

                        }
                    }
                }

                #endregion 
            }

            //this.txtInfectionPosition.Text = myItem.InfectionPosition.Name;//��Ⱦ��λ����
            if (string.IsNullOrEmpty(myItem.FirstAnaphyPharmacy.Name))//ҩ�����1
            {
                myItem.FirstAnaphyPharmacy.Name = "";
            }
            if (string.IsNullOrEmpty(myItem.SecondAnaphyPharmacy.Name))//ҩ�����2
            {
                myItem.SecondAnaphyPharmacy.Name = "";
            }
            txtAnaphyFlag.Text = AnaphyHelper.GetName(myItem.FirstAnaphyPharmacy.ID) + " " + AnaphyHelper.GetName(myItem.SecondAnaphyPharmacy.ID);

            if (string.IsNullOrEmpty(myItem.Hbsag))
            {
                myItem.Hbsag = "00";
            }
            if (string.IsNullOrEmpty(myItem.HcvAb))
            {
                myItem.HcvAb = "00";
            }
            if (string.IsNullOrEmpty(myItem.HivAb))
            {
                myItem.HivAb = "00";
            }
            if (string.IsNullOrEmpty(myItem.CePi))
            {
                myItem.CePi = "00";
            }
            if (string.IsNullOrEmpty(myItem.ClPa))
            {
                myItem.ClPa = "00";
            }
            if (string.IsNullOrEmpty(myItem.PiPo))
            {
                myItem.PiPo = "00";
            }
            if (string.IsNullOrEmpty(myItem.OpbOpa))
            {
                myItem.OpbOpa = "00";
            }
            if (string.IsNullOrEmpty(myItem.FsBl))
            {
                myItem.FsBl = "00";
            }
            this.txtInjuryOrPoisoningCause.Text = myItem.InjuryOrPoisoningCause;
            this.HbsAg.Text = myItem.Hbsag.Substring(myItem.Hbsag.Length-1);
            this.HCVAb.Text = myItem.HcvAb.Substring(myItem.Hbsag.Length - 1);
            this.HIVAb.Text = myItem.HivAb.Substring(myItem.Hbsag.Length - 1);
            this.txtCepi.Text = myItem.CePi.Substring(myItem.Hbsag.Length - 1);//�������Ժ
            this.txtClPa.Text = myItem.ClPa.Substring(myItem.Hbsag.Length - 1);//�ٴ�_�������
            this.txtPiPo.Text = myItem.PiPo.Substring(myItem.Hbsag.Length - 1);//���_Ժ����
            this.txtOpbOpa.Text = myItem.OpbOpa.Substring(myItem.Hbsag.Length - 1);//��ǰ_�����
            this.txtFsBl.Text = myItem.FsBl.Substring(myItem.Hbsag.Length - 1);//����_�������
            this.txtSalvTimes.Text = myItem.SalvTimes.ToString();//���ȴ���
            this.txtSuccTimes.Text = myItem.SuccTimes.ToString();//�ɹ�����
            this.txtDeptChiefDonm.Text = myItem.ClinicDoc.Name;//������
            this.txtChiefDocName.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.Name;//����ҽʦ
            this.txtChargeDocName.Text = myItem.PatientInfo.PVisit.AttendingDoctor.Name;//����ҽʦ
            this.RefresherDoc.Text = myItem.RefresherDoc.Name;//����ҽ��
            this.txtGraDocName.Text = myItem.GraduateDoc.Name; //�о���ʵϰҽ��
            this.txtHouseDocName.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.Name;//סԺҽʦ
            this.txtPraDocCode.Text = myItem.PatientInfo.PVisit.TempDoctor.Name;//ʵϰҽʦ
            this.txtCodingCode.Text = myItem.CodingOper.Name;//����Ա
            this.txtMrQual.Text = myItem.MrQuality; //��������
            this.txtQcDocd.Text = myItem.QcDoc.Name;//�ʿ�ҽ��
            this.txtQcNucd.Text = myItem.QcNurse.Name;//�ʿػ�ʿ
            this.txtCheckYear.Text = myItem.CheckDate.Year.ToString();
            this.txtCheckMonth.Text = myItem.CheckDate.Month.ToString();
            this.txtCheckDay.Text = myItem.CheckDate.Day.ToString();
            #endregion

            ////����ת����Ϣ���б�
            //ArrayList changeDept = new ArrayList();
            ////��ȡת����Ϣ
            //changeDept = deptMger.QueryChangeDeptFromShiftApply(myItem.PatientInfo.ID, "2");
            //LoadChangeDept(changeDept);

            //ArrayList alOrg = new ArrayList();
            //FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //alOrg = diag.QueryCaseDiagnose(myItem.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
           
        }

        /// <summary>
        /// ���ݷ�����Դ��ʾҽ�Ƹ��ʽ   ID = pactcode name=pactname memo=���ʽ����
        /// </summary>
        /// <returns></returns>
        private string GetPaykindForCaseFirst(string pactCode)
        {
            ArrayList al = new ArrayList();
            al = con.GetList("PAYMODES");

            if (al.Count > 0)
            {
                foreach (FS.HISFC.Models.Base.Const cons in al)
                {
                    if (pactCode == cons.ID)
                    {
                        return cons.Memo;
                    }
                }
            }

            return "����";
        }

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.Reset()
        {
            payKindCbx.Tag = "";//���ѷ�ʽ��
            InpatientNO.Text = "";//סԺ��ˮ��
            //this.medCardTextBox.Text = "";//�籣��
            txtInHosNo.Text = "";//��Ժ����
            //this.inpatientNOTextBox.Text = "";//סԺ��
            this.nameTextBox.Text = "";//����
            this.birMon.Tag = "";//�Ա�
            this.birYear.Text = "";//��������
            this.birMon.Text = "";//��������
            this.birDay.Text = "";//��������
            this.ageTextBox.Text = "";//����
            //this.marryComboBox.Tag = "";//����״��
            this.workComboBox.Tag = "";//ְҵ
            this.birthInComboBox.Tag = ""; //������
            this.nationComboBox.Tag = "";//����
            this.districtComboBox.Text = ""; //����
            this.IDTextBox.Text = "";//���֤
            this.workAdressTextBox.Text = ""; ;//������λ��ַ
            this.workPhoneTextBox.Text = "";//������λ�绰
            this.workZipTextBox.Text = "";//�ʱࡡ
            this.homeAdTextBox.Text = "";//��ͥסַ
            this.homeZipTextBox.Text = "";//�ʱࡡ
            this.linkNameTextBox.Text = "";//��ϵ������
            this.relationComboBox.Tag = "";//��ϵ�˹�ϵ
            this.linkAdressTextBox.Text = ""; //��ϵ�˵�ַ �д�����;
            this.linkPhoneTextBox.Text = ""; //��ϵ�˵绰 �д�����;\
            inDaysTextBox.Text = "";

        }

        #endregion

        #region IReportPrinter ��Ա

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Export()
        {
            return 1;
        }
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        int FS.FrameWork.WinForms.Forms.IReportPrinter.Print()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.FrameWork.WinForms.Classes.Print print = null;

            try
            {
                print = new FS.FrameWork.WinForms.Classes.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show("��ʼ����ӡ��ʧ��");
            }
            print.SetPageSize(pageSizeManager.GetPageSize("BAGL"));

            //caseBackPrint.Print();

            //return print.PrintPage(0, 0, this);
            //this.SetVisibled(false);
            return p.PrintPage(20, 10, this);

            
           
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            return p.PrintPreview(20, 10, this);
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        protected virtual void SetVisibled(bool isVisible)
        {
            foreach (Control control in this.panel1.Controls)
            {
                if (control.Name.Contains("neuLabel") || control.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuPanel) )
                {
                    if (control.Name == this.panel1.Name)
                    {
                        continue;
                    }
                    control.Visible = false;
                }
                if (!isVisible)
                {
                    if (control.Name == "sexComboBox" ||
                        control.Name == "marryTextBox" ||
                        control.Name == "neuLabel21" ||
                        control.Name == "HbsAg" ||
                        control.Name == "HCVAb" ||
                        control.Name == "HIVAb" ||
                        control.Name == "txtCepi" ||
                        control.Name == "txtPiPo" ||
                        control.Name == "txtOpbOpa" ||
                        control.Name == "txtClPa" ||
                        control.Name == "txtFsBl" ||
                        control.Name == "neuLabel19" ||
                        control.Name == "txtFsBl")
                    {
                        (control as Label).BorderStyle = BorderStyle.None;
                        control.Visible = true;
                    }
                }
                else
                {
                    if (control.Name == "sexComboBox" ||
                                           control.Name == "marryTextBox" ||
                                           control.Name == "neuLabel21" ||
                                           control.Name == "HbsAg" ||
                                           control.Name == "HCVAb" ||
                                           control.Name == "HIVAb" ||
                                           control.Name == "txtCepi" ||
                                           control.Name == "txtPiPo" ||
                                           control.Name == "txtOpbOpa" ||
                                           control.Name == "txtClPa" ||
                                           control.Name == "txtFsBl" ||
                                           control.Name == "neuLabel19" ||
                                           control.Name == "txtFsBl")
                    {
                        (control as Label).BorderStyle = BorderStyle.FixedSingle;
                        control.Visible = true;
                    }
                }
            }
        }
    }
}
