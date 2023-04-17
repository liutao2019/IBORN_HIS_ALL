using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace UFC.HealthRecord
{
    public partial class ucCaseMainInfo : System.Windows.Forms.UserControl
    {
        public ucCaseMainInfo()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���

        //icd10��������б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox ICDListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        //��ǰ��ؼ�
        private System.Windows.Forms.Control contralActive = new Control();
        //��ǰ������б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox listBoxActive = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        //��־ ��ʶ��ҽ��վ�û��ǲ�������
        private Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes frmType;
        //�ݴ浱ǰ�޸��˵Ĳ���������Ϣ
        private Neusoft.HISFC.Object.HealthRecord.Base CaseBase = new Neusoft.HISFC.Object.HealthRecord.Base();
        //����������Ϣ������
        private Neusoft.HISFC.Management.HealthRecord.Base baseDml = new Neusoft.HISFC.Management.HealthRecord.Base();
        private Neusoft.HISFC.Management.HealthRecord.DeptShift deptShift = new Neusoft.HISFC.Management.HealthRecord.DeptShift();
        private Neusoft.HISFC.Integrate.Fee  feeMgr = new Neusoft.HISFC.Integrate.Fee();
        //�������
        Neusoft.HISFC.Management.Manager.Constant con = new Neusoft.HISFC.Management.Manager.Constant();
        #region �����б�
        //�Ա�
        private Neusoft.NFC.Interface.Controls.PopUpListBox SexListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper SexTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����
        private Neusoft.NFC.Interface.Controls.PopUpListBox CountryListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CountryHelper = new Neusoft.NFC.Public.ObjectHelper();
        //�����б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox NationalListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper NationalHelper = new Neusoft.NFC.Public.ObjectHelper();
        //ְҵ�б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox ProfessionListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper ProfessionHelper = new Neusoft.NFC.Public.ObjectHelper();
        //Ѫ�ͱ���
        private Neusoft.NFC.Interface.Controls.PopUpListBox BloodTypeListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper BloodTypeHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����
        private Neusoft.NFC.Interface.Controls.PopUpListBox MaritalStatusListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper MaritalStatusHelper = new Neusoft.NFC.Public.ObjectHelper();
        //�������
        private Neusoft.NFC.Interface.Controls.PopUpListBox pactKindListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper pactKindHelper = new Neusoft.NFC.Public.ObjectHelper();
        //����ϵ�˹�ϵ
        private Neusoft.NFC.Interface.Controls.PopUpListBox RelationListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper RelationHelper = new Neusoft.NFC.Public.ObjectHelper();
        //ҽ����ʿ����Ա�б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox DoctorListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper DoctorHelper = new Neusoft.NFC.Public.ObjectHelper();
        //������Դ
        private Neusoft.NFC.Interface.Controls.PopUpListBox InAvenueListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper InAvenueHelper = new Neusoft.NFC.Public.ObjectHelper();
        //��Ժ���
        private Neusoft.NFC.Interface.Controls.PopUpListBox CircsListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CircsHelper = new Neusoft.NFC.Public.ObjectHelper();
        //ҩ����� 
        private Neusoft.NFC.Interface.Controls.PopUpListBox HbsagListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper HbsagHelper = new Neusoft.NFC.Public.ObjectHelper();
        //��Ϸ���
        private Neusoft.NFC.Interface.Controls.PopUpListBox diagAccordListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper diagAccordHelper = new Neusoft.NFC.Public.ObjectHelper();
        //�������� 
        private Neusoft.NFC.Interface.Controls.PopUpListBox CaseQCListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper CaseQCHelper = new Neusoft.NFC.Public.ObjectHelper();
        //RH ����
        private Neusoft.NFC.Interface.Controls.PopUpListBox RHListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper RHListHelper = new Neusoft.NFC.Public.ObjectHelper();
        //���������б�
        private Neusoft.NFC.Interface.Controls.PopUpListBox DeptListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        //ѪҺ��Ӧ
        private Neusoft.NFC.Interface.Controls.PopUpListBox ReactionBloodListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper ReactionBloodHelper = new Neusoft.NFC.Public.ObjectHelper();
        //���� ��ͥסַ ��ϵ�˵�ַ
        private Neusoft.NFC.Interface.Controls.PopUpListBox AddressHomeListBox = new Neusoft.NFC.Interface.Controls.PopUpListBox();
        private Neusoft.NFC.Public.ObjectHelper AddressHomeListHelper = new Neusoft.NFC.Public.ObjectHelper();
        #endregion
        //������� 
        private Neusoft.HISFC.Object.HealthRecord.Diagnose clinicDiag = null;
        //��Ժ��� 
        private Neusoft.HISFC.Object.HealthRecord.Diagnose InDiag = null;
        //ת����Ϣ
        ArrayList changeDept = new ArrayList();
        //��һ��ת��
        private Neusoft.HISFC.Object.RADT.Location firDept = null;
        //�ڶ���ת����Ϣ
        private Neusoft.HISFC.Object.RADT.Location secDept = null;
        //������ת����Ϣ
        private Neusoft.HISFC.Object.RADT.Location thirDept = null;
        //��ʶ�ֹ������״̬�ǲ��뻹�Ǹ���  0Ĭ��״̬  1  ���� 2����  
        private int HandCraft = 0;

        //		//��Ժ��ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
        //		public int RuDiag = 0;
        //		//������ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
        //		public int menDiag = 0;
        //���没����״̬
        private int CaseFlag = 0;
        //��ʾ����
        //ucDiagNoseCheck frm = null; //zjy
        private Neusoft.NFC.Object.NeuObject localObj = new Neusoft.NFC.Object.NeuObject();
        #endregion 

        public int InitCaseMainInfo()
        {
            //ICD10 ���
            InitSexList(); //�Ա�
            InitCountryList();//����
            return 1;
        }

        #region  ���е������б�
       
        //��ʼ���Ա������˵�
        private int InitSexList()
        {	//��ȡ�б�
            ArrayList list = Neusoft.HISFC.Object.Base.SexEnumService.List();
            SexTypeHelper.ArrayObject = list;
            return 1;
        }
        private int InitCountryList()
        {
            //g��ѯ�����б�
            ArrayList list = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.COUNTRY);
            CountryHelper.ArrayObject = list;
            this.Country.AddItems(list);

            //��ѯ�����б�
            ArrayList Nationallist1 = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.NATION);
            NationalHelper.ArrayObject = Nationallist1;
            this.Nationality.AddItems(Nationallist1);

            //��ѯְҵ�б�
            ArrayList Professionlist = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.PROFESSION);
            ProfessionHelper.ArrayObject = Professionlist;
            this.Profession.AddItems(Professionlist);
            //Ѫ���б�
            ArrayList BloodTypeList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.BLOODTYPE);
            BloodTypeHelper.ArrayObject = BloodTypeList;
            this.BloodType.AddItems(BloodTypeList);
            //�����б�
            ArrayList MaritalStatusList = Neusoft.HISFC.Object.RADT.MaritalStatusEnumService.List();
            MaritalStatusHelper.ArrayObject = MaritalStatusList;
            this.MaritalStatus.AddItems(MaritalStatusList);
            //�������
            ArrayList pactKindlist = feeMgr.QueryPactUnitAll();
            pactKindHelper.ArrayObject = pactKindlist;
            this.pactKind.AddItems(pactKindlist);
            //����ϵ�˹�ϵ
            ArrayList RelationList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.RELATIVE);
            RelationHelper.ArrayObject = RelationList;
            this.Relation.AddItems(RelationList);

            Neusoft.HISFC.Management.Manager.Person person = new Neusoft.HISFC.Management.Manager.Person();
            //��ȡҽ���б�
            ArrayList DoctorList = person.GetEmployeeAll();//.GetEmployee(Neusoft.HISFC.Object.RADT.PersonType.enuPersonType.D);
            DoctorHelper.ArrayObject = DoctorList;
            ClinicDocd.AddItems(DoctorList);//����ҽ�� 
            AdmittingDoctor.AddItems(DoctorList);//סԺҽ��
            RefresherDocd.AddItems(DoctorList);//����ҽʦ 
            GraDocCode.AddItems(DoctorList);//�о���ʵϰҽʦ 
            PraDocCode.AddItems(DoctorList);//ʵϰҽ�� 
            AttendingDoctor.AddItems(DoctorList); //����ҽʦ
            ConsultingDoctor.AddItems(DoctorList);//����ҽʦ
            QcNucd.AddItems(DoctorList);//�ʿػ�ʿ
            QcDocd.AddItems(DoctorList);//�ʿ�ҽ��
            CodingCode.AddItems(DoctorList);//����Ա
            textBox33.AddItems(DoctorList);//����Ա

            //��ȡ������Դ
            //			ArrayList InAvenuelist = baseDml.GetPatientSource();
            ArrayList InAvenuelist = con.GetAllList(Neusoft.HISFC.Object.Base.EnumConstant.INAVENUE);
            InAvenueHelper.ArrayObject = InAvenuelist;
            InAvenue.AddItems(InAvenuelist); 

            //��Ժ���
            ArrayList CircsList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.INCIRCS);
            CircsHelper.ArrayObject = CircsList;
            this.Circs.AddItems(CircsList);
            //ҩ�����
            ArrayList arraylist = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.PHARMACYALLERGIC);
            HbsagHelper.ArrayObject = arraylist;
            this.Hbsag.AddItems(arraylist);

            //��Ϸ������
            ArrayList diagAccord = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.DIAGNOSEACCORD);
            diagAccordHelper.ArrayObject = diagAccord;
            CePi.AddItems(diagAccord);
            PiPo.AddItems(diagAccord);
            OpbOpa.AddItems(diagAccord);
            ClPa.AddItems(diagAccord);
            FsBl.AddItems(diagAccord);

            //��������
            ArrayList qcList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.CASEQUALITY);
            CaseQCHelper.ArrayObject = qcList;
            MrQual.AddItems(qcList);

            //RH���� 
            ArrayList RHList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.RHSTATE); 
            RHListHelper.ArrayObject = RHList;
            RhBlood.AddItems(RHList);

            //���������б�
            Neusoft.HISFC.Management.Manager.Department dept = new Neusoft.HISFC.Management.Manager.Department();
            ArrayList deptList = dept.GetInHosDepartment();
            firstDept.AddItems(deptList);
            deptSecond.AddItems(deptList);
            deptThird.AddItems(deptList);
            DeptInHospital.AddItems(deptList);
            deptOut.AddItems(deptList);

            //ѪҺ��Ӧ

            ArrayList ReactionBloodList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.BLOODREACTION); 
            ReactionBloodHelper.ArrayObject = ReactionBloodList;
            //���� ��ͥסַ ��ϵ�˵�ַ
            ArrayList AddressHomeList = con.GetList(Neusoft.HISFC.Object.Base.EnumConstant.AREA);
            AddressHomeListHelper.ArrayObject = AddressHomeList;

            return 1;
        }

        #endregion

        #region �¼�

        #region �Ա�
        private void PatientSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                patientBirthday.Focus();
            }
        }
        #endregion
        #region �������
        private void ClinicDiag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ClinicDocd.Focus();
            }
        }
        #endregion
        #region ����
        private void Country_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DIST.Focus();
            }
            else if (e.KeyData == Keys.Up)
            {
                CountryListBox.PriorRow();
            }
            else if (e.KeyData == Keys.Down)
            {
                CountryListBox.NextRow();
            }
        }
        #endregion
        #region  ����
        private void Nationality_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Country.Focus();
            }
        }
        #endregion
        #region  Ѫ��
        private void BloodType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.RhBlood.Focus();
            }
        }
        #endregion
        #region ����
        private void MaritalStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Profession.Focus();
            } 
        }
        #endregion
        #region ְҵ
        private void Profession_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AreaCode.Focus();
            } 
        }
        #endregion
        #region ��ϵ�˹�ϵ
        private void Relation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                LinkmanTel.Focus();
            } 
        }
        #endregion
        #region  ��Ժ��� 
        private void Circs_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                firstDept.Focus();
            } 
        } 
        #endregion
        #region ����ҽ�� 
        private void ClinicDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.PiDays.Focus();
            } 
        }
        #endregion
        #region ������Դ
        private void InAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.infectNum.Focus();
            } 
        }
        #endregion
        #region ҩ����� 
        private void Hbsag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                HcvAb.Focus();
            } 
        }  
        private void HcvAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                HivAb.Focus();
            }
        }  
        private void HivAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.CePi.Focus();
            } 
        }
        #endregion
        #region ��Ϸ���

        private void CePi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                PiPo.Focus();
            } 
        }  
        private void PiPo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                OpbOpa.Focus();
            } 
        } 
        private void OpbOpa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ClPa.Focus();
            } 
        }  
        private void ClPa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                FsBl.Focus();
            } 
        }  
        private void FsBl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SalvTimes.Focus();
            } 
        } 
        #endregion
        #region  סԺҽ�� 
        private void AdmittingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                RefresherDocd.Focus();
            } 
        }
        #endregion
        #region ����ҽʦ 
        private void RefresherDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                GraDocCode.Focus();
            } 
        } 
        #endregion
        #region �о���ʵϰҽʦ 
        private void GraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CheckDate.Focus();
            } 
        }
        #endregion
        #region ʵϰҽ�� 
        private void PraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CodingCode.Focus();
            } 
        }

        #endregion
        #region  ����ҽʦ 
        private void AttendingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AdmittingDoctor.Focus();
            } 
        }
        #endregion
        #region ����ҽʦ
        private void ConsultingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AttendingDoctor.Focus();
            } 
        }
        #endregion
        #region  �ʿػ�ʿ 
        private void QcNucd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ConsultingDoctor.Focus();
            } 
        } 
        #endregion
        #region �ʿ�ҽ�� 
        private void QcDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                QcNucd.Focus();
            } 
        } 
        #endregion
        #region ����Ա 
        private void CodingCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox33.Focus();
            } 
        }
        #endregion
        #region ����Ա
        private void textBox33_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                OperationCode.Focus();
            } 
        } 
        #endregion
        #region �������� 
        private void MrQual_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                QcDocd.Focus();
            } 
        } 
        #endregion
        #region  ��Ѫ��ӳ 
        private void ReactionBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (ReactionBlood.Tag != null)
                {
                    if (ReactionBlood.Tag.ToString() != "2")
                    {
                        BloodRed.Focus();
                    }
                    else
                    {
                        //Ժ�ʻ������
                        InconNum.Focus();
                    }
                }
                else
                {
                    BloodRed.Focus();
                }
            } 
        } 
        #endregion
        #region ����Ա 
        private void InputDoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�����ж� ��������ϰ�
                //this.tab1.SelectedIndex = 1;
            } 
        } 
        #endregion
        #region  ��Ժ��� 
        private void RuyuanDiagNose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�����ж� ���������
                this.ComeFrom.Focus();
            } 
        } 
        #endregion
        #region  ��Ժ���� 
        private void DeptInHospital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Date_In.Focus();
            } 
        }
        #endregion
        #region  RH��Ӧ
        private void RhBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ReactionBlood.Focus();
            } 
        } 
        #endregion
        #region  ������ 
        private void AreaCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Nationality.Focus();
            } 
        } 
        #endregion
        #region ת��1
        private void firstDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dateTimePicker3.Focus();
            } 
        } 
        #endregion
        #region ת�� 2 
        private void deptSecond_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dateTimePicker4.Focus();
            } 
        } 
        #endregion
        #region  ת��3 
        private void deptThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dateTimePicker5.Focus();
            } 
        } 
        #endregion
        #region ��Ժ���� 
        private void deptOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Date_Out.Focus();
            } 
        } 
        #endregion
        #region ������� 
        private void pactKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SSN.Focus();
            } 
        }

        #endregion
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int SaveInfo()
        {
            #region  �ж�����Ƿ����Լ��
            Neusoft.HISFC.Management.HealthRecord.Diagnose diagNose = new Neusoft.HISFC.Management.HealthRecord.Diagnose();
            if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC) //ҽ��վ��ʾ �����Ҳ���Ҫ��ʾ
            {
                if (DiagValueState(diagNose) != 1)
                {
                    return -1;
                }
            }

            System.DateTime dt = diagNose.GetDateTimeFromSysDateTime(); //��ȡϵͳʱ��
            #endregion
            #region  �ж�סԺ�ź�סԺ�����Ƿ��Ѿ�����
            int intI = baseDml.ExistCase(this.CaseBase.PatientInfo.ID, caseNum.Text, InTimes.Text);
            if (intI == -1)
            {
                MessageBox.Show("��ѯ����ʧ��");
                return -1;
            }
            if (intI == 2)
            {
                MessageBox.Show(caseNum.Text + " ��" + "�� " + InTimes.Text + " ����Ժ�Ѿ�����,�������Ժ����");
                return -1;
            }
            #endregion
            //��������
            Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction(baseDml.Connection);
            try
            {

                if (CaseBase == null)
                {
                    return -2;
                }
                if (CaseBase.PatientInfo.ID == "")
                {
                    MessageBox.Show("��ָ��Ҫ���没���Ĳ���");
                    return -2;
                }
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("���˲������в���");
                    return 0;
                }
                if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC && CaseBase.PatientInfo.CaseState == "3")
                {
                    MessageBox.Show("�������Ѿ��浵���������޸�");
                    return -3;
                }
                if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC && (HandCraft == 1 || HandCraft == 2))
                {
                    MessageBox.Show("�������Ѿ��浵�������޸�");
                    return -3;
                }
                if (CaseBase.PatientInfo.CaseState == "4")
                {
                    MessageBox.Show("���˲����Ѿ���棬��������");
                    return -4;
                }
                if (HandCraft == 1) //�ֹ�¼�� ����
                {
                    CaseBase.PatientInfo.CaseState = "1";
                }
                if (HandCraft == 2) //�ֹ�¼���޸� 
                {
                    CaseBase.PatientInfo.CaseState = "2";
                }
                trans.BeginTransaction();
                baseDml.SetTrans(trans.Trans);
                diagNose.SetTrans(trans.Trans);

                #region ����������Ϣ
                Neusoft.HISFC.Object.HealthRecord.Base info = new Neusoft.HISFC.Object.HealthRecord.Base();
                int i = this.GetInfoFromPanel(info);
                if (ValidState(info) == -1)
                {
                    trans.RollBack();
                    return -1;
                }
                //��ִ�и��²��� 
                if (baseDml.UpdateBaseInfo(info) < 1)
                {
                    //����ʧ�� ��ִ�в������ 
                    if (baseDml.InsertBaseInfo(info) < 1)
                    {
                        //����
                        trans.RollBack();
                        MessageBox.Show("���没�˻�����Ϣʧ�� :" + baseDml.Err);
                        return -1;
                    }
                }

                #endregion

                #region  ����ɹ�

                //����Ŀǰ������־ �޸�סԺ����Ĳ�����Ϣ
                if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC)
                {
                    //ҽ��վ¼�벡��
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, "2") < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("��������ʧ��" + baseDml.Err);
                        return -1;
                    }
                    CaseBase.PatientInfo.CaseState = "2";
                }
                else if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS) //������¼�벡��
                {
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, "3") < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("�������� case_flag ʧ��" + baseDml.Err);
                        return -1;
                    }
                    if (baseDml.UpdateMainInfoCaseSendFlag(CaseBase.PatientInfo.ID, "1") < 1)
                    {
                        trans.RollBack();
                        MessageBox.Show("��������casesend_flag ʧ��" + baseDml.Err);
                        return -1;
                    }
                    CaseBase.PatientInfo.CaseState = "3";
                }

                //������Ϣ
                trans.Commit();

                #region ��������
                //���²����������� ������ϣ���Ժ��ϣ���Ժ��� ������ ����һ��� ��������
                if (baseDml.UpdateBaseDiagAndOperation(CaseBase.PatientInfo.ID, frmType) == -1)
                {
                    trans.RollBack();
                    MessageBox.Show("���²����������������Ϣʧ��.");
                    return -1;
                }
                localObj.User01 = CaseBase.PatientInfo.PVisit.OutTime.ToString(); //��Ժһ��
                localObj.User02 = CaseBase.PatientInfo.PVisit.PatientLocation.ID; //��Ժ���� 
                if (baseDml.DiagnoseAndOperation(localObj, CaseBase.PatientInfo.ID) == -1)
                {
                    trans.RollBack();
                    MessageBox.Show("���²����������������Ϣʧ��.");
                    return -1;
                }
                trans.Commit();
                #endregion
                //�ֹ�¼�벡����־�ó�Ĭ�ϱ�־ 
                this.HandCraft = 0;
                #endregion
            }
            catch (Exception ex)
            {
                trans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        #endregion 

        /// <summary>
        /// ��������ʾ��������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ConvertInfoToPanel(Neusoft.HISFC.Object.HealthRecord.Base info)
        {
            try
            {
                #region  ��Ժ���ң���Ժ����
                if (CaseBase.PatientInfo.CaseState == "1")
                {
                    Neusoft.HISFC.Object.RADT.Location indept = baseDml.GetDeptIn(CaseBase.PatientInfo.ID);
                    Neusoft.HISFC.Object.RADT.Location outdept = baseDml.GetDeptOut(CaseBase.PatientInfo.ID);
                    if (indept != null) //��Ժ���� 
                    {
                        CaseBase.InDept.ID = indept.Dept.ID;
                        CaseBase.InDept.Name = indept.Dept.Name;
                        //��Ժ���Ҵ���
                        DeptInHospital.Tag = indept.Dept.ID;
                        //��Ժ��������
                        DeptInHospital.Text = indept.Dept.Name;

                    }
                    //��Ժ����
                    CaseBase.OutDept.ID = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    CaseBase.OutDept.Name = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                    //��Ժ���Ҵ���
                    deptOut.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    //��Ժ��������
                    deptOut.Text = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                }
                else
                {
                    //��Ժ���Ҵ���
                    DeptInHospital.Tag = info.InDept.ID;
                    //��Ժ��������
                    DeptInHospital.Text = info.InDept.Name;
                    //��Ժ���Ҵ���
                    deptOut.Tag = info.OutDept.ID;
                    //��Ժ��������
                    deptOut.Text = info.OutDept.Name;
                }

                #endregion

                //סԺ��  ������
                caseNum.Text = info.PatientInfo.PID.PatientNO;
                //���￨��  �����
                clinicNo.Text = info.PatientInfo.PID.CardNO;
                //����
                this.PatientName.Text = info.PatientInfo.Name;
                //������
                Nomen.Text = info.Nomen;
                //�Ա�
                if (info.PatientInfo.Sex.ID != null)
                {
                    PatientSex.Tag = info.PatientInfo.Sex.ID.ToString();
                    PatientSex.Text = SexTypeHelper.GetName(info.PatientInfo.Sex.ID.ToString());
                }
                //����
                if (info.PatientInfo.Birthday != System.DateTime.MinValue)
                {
                    patientBirthday.Value = info.PatientInfo.Birthday;
                }
                else
                {
                    patientBirthday.Value = System.DateTime.Now;
                }
                //���� ����
                Country.Tag = info.PatientInfo.Country.ID;
                //��������
                Country.Text = CountryHelper.GetName(info.PatientInfo.Country.ID);
                //���� 
                Nationality.Tag = info.PatientInfo.Nationality.ID;
                Nationality.Text = NationalHelper.GetName(info.PatientInfo.Nationality.ID);
                //ְҵ
                Profession.Tag = info.PatientInfo.Profession.ID;
                Profession.Text = ProfessionHelper.GetName(info.PatientInfo.Profession.ID);

                //Ѫ�ͱ���
                if (info.PatientInfo.BloodType.ID != null)
                {
                    BloodType.Text = info.PatientInfo.BloodType.ID.ToString();
                }
                //����
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    MaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
                    MaritalStatus.Text = MaritalStatusHelper.GetName(info.PatientInfo.MaritalStatus.ID.ToString());
                }
                //���� ����ǴӲ�������������� Ҫ ƴ��
                if (info.PatientInfo.CaseState == "2" || info.PatientInfo.CaseState == "3" || info.PatientInfo.CaseState == "4")
                {
                    PatientAge.Text = info.PatientInfo.Age.ToString() + info.AgeUnit;
                }
                else
                {
                    //����Ǵ�סԺ������������Ĳ���ƴ��
                    PatientAge.Text = info.PatientInfo.Age;
                }
                //���֤��
                IDNo.Text = info.PatientInfo.IDCard;
                //��������
                pactKind.Tag = info.PatientInfo.Pact.PayKind.ID;
                pactKind.Text = pactKindHelper.GetName(info.PatientInfo.Pact.PayKind.ID);
                //ҽ�����Ѻ�
                SSN.Text = info.PatientInfo.SSN;
                //����
                DIST.Text = info.PatientInfo.DIST;
                //������
                AreaCode.Tag = info.PatientInfo.AreaCode;
                AreaCode.Text = AddressHomeListHelper.GetName(info.PatientInfo.AreaCode);
                if (AreaCode.Text == "")
                {
                    AreaCode.Text = info.PatientInfo.AreaCode;
                }
                //��ͥסַ
                AddressHome.Text = info.PatientInfo.AddressHome;
                //��ͥ�绰
                PhoneHome.Text = info.PatientInfo.PhoneHome;
                //סַ�ʱ�
                if (info.PatientInfo.CaseState == "1")
                {
                    HomeZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    HomeZip.Text = info.PatientInfo.HomeZip;
                }
                //��λ��ַ
                if (info.PatientInfo.CaseState == "1")
                {
                    AddressBusiness.Text = info.PatientInfo.CompanyName;
                }
                else
                {
                    AddressBusiness.Text = info.PatientInfo.AddressBusiness;
                }
                //��λ�绰
                PhoneBusiness.Text = info.PatientInfo.PhoneBusiness;
                //��λ�ʱ�
                if (info.PatientInfo.CaseState == "1")
                {
                    BusinessZip.Text = info.PatientInfo.User01;
                }
                else
                {
                    BusinessZip.Text = info.PatientInfo.BusinessZip;
                }
                //��ϵ������
                Kin.Text = info.PatientInfo.Kin.Name;
                Kin.Tag = info.PatientInfo.Kin.ID;
                //�뻼�߹�ϵ
                Relation.Tag = info.PatientInfo.Kin.Relation.ID;
                Relation.Text = RelationHelper.GetName(info.PatientInfo.Kin.Relation.ID);
                //��ϵ�绰
                if (info.PatientInfo.CaseState == "1")
                {
                    LinkmanTel.Text = info.PatientInfo.Kin.Memo;
                }
                else
                {
                    LinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                }
                //��ϵ��ַ
                if (info.PatientInfo.CaseState == "1")
                {
                    LinkmanAdd.Text = info.PatientInfo.Kin.User01;
                }
                else
                {
                    LinkmanAdd.Text = info.PatientInfo.Kin.RelationAddress;
                }
                //�������ҽ�� ID
                ClinicDocd.Tag = info.ClinicDoc.ID;
                //�������ҽ������
                ClinicDocd.Text = info.ClinicDoc.Name;
                //ת��ҽԺ
                ComeFrom.Text = info.ComeFrom;
                //��Ժ����
                if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                {
                    Date_In.Value = info.PatientInfo.PVisit.InTime;
                }
                else
                {
                    Date_In.Value = System.DateTime.Now;
                }
                if (info.PatientInfo.CaseState == "1")
                {
                    //Ժ�д��� 
                    infectNum.Text = "0";
                }
                else
                {
                    //Ժ�д��� 
                    infectNum.Text = info.InfectionNum.ToString();
                }
                //סԺ����
                InTimes.Text = info.PatientInfo.InTimes.ToString();
                //��Ժ��Դ

                InAvenue.Tag = info.PatientInfo.PVisit.InSource.ID;
                InAvenue.Text = InAvenueHelper.GetName(info.PatientInfo.PVisit.InSource.ID);

                //��Ժ״̬                  
                Circs.Tag = info.PatientInfo.PVisit.Circs.ID;
                Circs.Text = this.CircsHelper.GetName(info.PatientInfo.PVisit.Circs.ID);
                //ȷ������
                if (info.DiagDate != System.DateTime.MinValue)
                {
                    DiagDate.Value = info.DiagDate;
                }
                else
                {
                    DiagDate.Value = System.DateTime.Now;
                }
                //��������
                //			info.OperationDate 
                //��Ժ����
                if (info.PatientInfo.PVisit.OutTime != System.DateTime.MinValue)
                {
                    Date_Out.Value = info.PatientInfo.PVisit.OutTime;
                }
                else
                {
                    Date_Out.Value = System.DateTime.Now;
                }

                //ת�����
                //			info.PatientInfo.PVisit.Zg.ID 
                //ȷ������
                //			info.DiagDays
                //סԺ����
                PiDays.Text = info.InHospitalDays.ToString();
                //��������
                //			info.DeadDate = 
                //����ԭ��
                //			info.DeadReason
                //ʬ��
                if (info.CadaverCheck == "1")
                {
                    BodyCheck.Checked = true;
                }
                //��������
                //			info.DeadKind 
                //ʬ����ʺ�
                //			info.BodyAnotomize
                //�Ҹα��濹ԭ
                Hbsag.Tag = info.Hbsag;
                Hbsag.Text = HbsagHelper.GetName(info.Hbsag);
                if (Hbsag.Tag == null)
                {
                    Hbsag.Tag = "0";
                    Hbsag.Text = "δ��";
                }
                //���β�������
                HcvAb.Tag = info.HcvAb;
                HcvAb.Text = HbsagHelper.GetName(info.HcvAb);
                if (HcvAb.Tag == null)
                {
                    HcvAb.Tag = "0";
                    HcvAb.Text = "δ��";
                }
                //�������������ȱ�ݲ�������
                HivAb.Tag = info.HivAb;
                HivAb.Text = HbsagHelper.GetName(info.HivAb);
                if (HivAb.Tag == null)
                {
                    HivAb.Tag = "0";
                    HivAb.Text = "δ��";
                }
                //�ż�_��Ժ����
                CePi.Tag = info.CePi;
                CePi.Text = diagAccordHelper.GetName(info.CePi);
                if (CePi.Tag == null)
                {
                    CePi.Tag = "0";
                    CePi.Text = "δ��";
                }
                //���_Ժ����
                PiPo.Tag = info.PiPo;
                PiPo.Text = diagAccordHelper.GetName(info.PiPo);
                if (PiPo.Tag == null)
                {
                    PiPo.Tag = "0";
                    PiPo.Text = "δ��";
                }
                //��ǰ_�����
                OpbOpa.Tag = info.OpbOpa;
                OpbOpa.Text = diagAccordHelper.GetName(info.OpbOpa);
                if (OpbOpa.Tag == null)
                {
                    OpbOpa.Tag = "0";
                    OpbOpa.Text = "δ��";
                }
                //�ٴ�_�������

                //�ٴ�_CT����
                //�ٴ�_MRI����
                //�ٴ�_�������
                ClPa.Tag = info.ClPa;
                ClPa.Text = diagAccordHelper.GetName(info.ClPa);
                if (ClPa.Tag == null)
                {
                    ClPa.Tag = "0";
                    ClPa.Text = "δ��";
                }
                //����_�������
                FsBl.Tag = info.FsBl;
                FsBl.Text = diagAccordHelper.GetName(info.ClPa);
                if (FsBl.Tag == null)
                {
                    FsBl.Tag = "0";
                    FsBl.Text = "δ��";
                }
                //���ȴ���
                SalvTimes.Text = info.SalvTimes.ToString();
                //�ɹ�����
                SuccTimes.Text = info.SuccTimes.ToString();
                //ʾ�̿���
                if (info.TechSerc == "1")
                {
                    TechSerc.Checked = true;
                }
                //�Ƿ�����
                if (info.VisiStat == "1")
                {
                    VisiStat.Checked = true;
                }
                //������� ��
                if (info.VisiPeriodWeek == "")
                {
                    VisiPeriWeek.Text = "0";
                }
                else
                {
                    VisiPeriWeek.Text = info.VisiPeriodWeek;
                }
                //������� ��
                if (info.VisiPeriodMonth == "")
                {
                    VisiPeriMonth.Text = "0";
                }
                else
                {
                    VisiPeriMonth.Text = info.VisiPeriodMonth;
                }
                //������� ��
                if (info.VisiPeriodYear == "")
                {
                    VisiPeriYear.Text = "0";
                }
                else
                {
                    VisiPeriYear.Text = info.VisiPeriodYear;
                }
                //Ժ�ʻ������
                InconNum.Text = info.InconNum.ToString();
                //Զ�̻���
                outOutconNum.Text = info.OutconNum.ToString();
                //ҩ�����
                //			info.AnaphyFlag 
                //����ҩ������
                //			info.AnaphyName1
                //����ҩ������
                //			info.AnaphyName2
                //���ĺ��Ժ����
                //			info.CoutDate
                //סԺҽʦ����
                AdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
                //סԺҽʦ����
                AdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
                //����ҽʦ����
                AttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
                AttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
                //����ҽʦ����
                ConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
                ConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
                //�����δ���
                //			info.PatientInfo.PVisit.ReferringDoctor.ID
                //����ҽʦ����
                RefresherDocd.Tag = info.RefresherDoc.ID;
                RefresherDocd.Text = info.RefresherDoc.Name;
                //�о���ʵϰҽʦ����
                GraDocCode.Tag = info.GraduateDoc.ID;
                GraDocCode.Text = info.GraduateDoc.Name;
                //ʵϰҽʦ����
                PraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
                PraDocCode.Text = info.PatientInfo.PVisit.TempDoctor.Name;
                //����Ա
                CodingCode.Tag = info.CodingOper.ID;
                CodingCode.Text = info.CodingOper.Name;
                //��������
                MrQual.Tag = info.MrQuality;
                MrQual.Text = CaseQCHelper.GetName(info.MrQuality);
                //�ϸ񲡰�
                //			info.MrElig
                //�ʿ�ҽʦ����
                QcDocd.Tag = info.QcDoc.ID;
                QcDocd.Text = info.QcDoc.Name;
                //�ʿػ�ʿ����
                QcNucd.Tag = info.QcNurse.ID;
                QcNucd.Text = info.QcNurse.Name;
                //���ʱ��
                if (info.CheckDate != System.DateTime.MinValue)
                {
                    CheckDate.Value = info.CheckDate;
                }
                else
                {
                    CheckDate.Value = System.DateTime.Now;
                }
                //�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
                if (info.YnFirst == "1")
                {
                    YnFirst.Checked = true;
                }
                //RhѪ��(����)
                RhBlood.Tag = info.RhBlood;
                RhBlood.Text = RHListHelper.GetName(info.RhBlood);
                //��Ѫ��Ӧ�����ޣ�
                ReactionBlood.Tag = info.ReactionBlood;
                ReactionBlood.Text = ReactionBloodHelper.GetName(info.ReactionBlood);
                //��ϸ����
                if (info.BloodRed == "" || info.BloodRed == null)
                {
                    BloodRed.Text = "0";
                }
                else
                {
                    BloodRed.Text = info.BloodRed;
                }
                //ѪС����
                if (info.BloodPlatelet == "" || info.BloodPlatelet == null)
                {
                    BloodPlatelet.Text = "0";
                }
                else
                {
                    BloodPlatelet.Text = info.BloodPlatelet;
                }
                //Ѫ����
                if (info.BodyAnotomize == "" || info.BodyAnotomize == null)
                {
                    BodyAnotomize.Text = "0";
                }
                else
                {
                    BodyAnotomize.Text = info.BodyAnotomize;
                }
                //ȫѪ��
                if (info.BloodWhole == "" || info.BodyAnotomize == null)
                {
                    BloodWhole.Text = "0";
                }
                else
                {
                    BloodWhole.Text = info.BloodWhole;
                }
                //������Ѫ��
                if (info.BloodOther == "" || info.BodyAnotomize == null)
                {
                    BloodOther.Text = "0";
                }
                else
                {
                    BloodOther.Text = info.BloodOther;
                }
                //X���
                XNumb.Text = info.XQty.ToString();
                //CT��
                CtNumb.Text = info.CTQty.ToString();
                //MRI��
                MriNumb.Text = info.MRQty.ToString();
                //�����
                PathNumb.Text = info.PathNum;
                //DSA��
                //			info.DsaNumb
                //PET��
                //			info.PetNumb
                //ECT��
                //			info.EctNumb
                //X�ߴ���
                //			info.XTimes
                //CT����
                //			info.CtTimes
                //MR����
                //			info.MrTimes;
                //DSA����
                //			info.DsaTimes
                //PET����
                //			info.PetTimes
                //ECT����
                //			info.EctTimes
                //˵��
                //			info.Memo
                //�鵵�����
                //			info.BarCode
                //��������״̬(O��� I�ڼ�)
                //			info.LendStus
                //����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч
                //			info.CaseStus 
                //�ؼ�����ʱ��
                SuperNus.Text = info.SuperNus.ToString();
                //I������ʱ��
                INus.Text = info.INus.ToString();
                //II������ʱ��
                IINus.Text = info.IINus.ToString();
                //III������ʱ��
                IIINus.Text = info.IIINus.ToString();
                //��֢�໤ʱ��
                StrictNuss.Text = info.StrictNuss.ToString();
                //���⻤��
                SPecalNus.Text = info.SpecalNus.ToString();
                //����Ա
                InputDoc.Tag = info.OperInfo.ID;
                InputDoc.Text = DoctorHelper.GetName(info.OperInfo.ID);
                //����Ա 
                textBox33.Tag = info.PackupMan.ID;
                textBox33.Text = DoctorHelper.GetName(info.PackupMan.ID);
                //��������Ա 
                this.OperationCode.Tag = info.OperationCoding.ID;
                this.OperationCode.Text = DoctorHelper.GetName(info.OperationCoding.ID);
                //������ 
                checkBox8.Checked = Neusoft.NFC.Function.NConvert.ToBoolean(info.Disease30);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// �ӿ�������ϻ�ȡ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int GetInfoFromPanel(Neusoft.HISFC.Object.HealthRecord.Base info)
        {

            //סԺ��ˮ��
            info.PatientInfo.ID = CaseBase.PatientInfo.ID;
            //סԺ��  ������
            info.PatientInfo.PID.PatientNO = caseNum.Text;
            info.CaseNO = caseNum.Text;
            //���￨��  �����
            info.PatientInfo.PID.CardNO = clinicNo.Text;
            //����
            info.PatientInfo.Name = PatientName.Text;
            //������
            info.Nomen = Nomen.Text;
            //�Ա�
            if (PatientSex.Tag != null)
            {
                info.PatientInfo.Sex.ID = PatientSex.Tag;
            }
            else
            {
                info.PatientInfo.Sex.ID = CaseBase.PatientInfo.Sex.ID;
            }
            //����
            info.PatientInfo.Birthday = patientBirthday.Value;
            //����
            if (Country.Tag != null)
            {
                info.PatientInfo.Country.ID = Country.Tag.ToString();
            }
            //���� 
            if (Nationality.Tag != null)
            {
                info.PatientInfo.Nationality.ID = Nationality.Tag.ToString();
            }
            //ְҵ
            if (Profession.Tag != null)
            {
                info.PatientInfo.Profession.ID = Profession.Tag.ToString();
            }
            //Ѫ�ͱ���
            info.PatientInfo.BloodType.ID = BloodType.Text;
            //����
            if (MaritalStatus.Tag != null)
            {
                info.PatientInfo.MaritalStatus.ID = MaritalStatus.Tag;
            }
            if (PatientAge.Text.Length > 1)
            {
                //���䵥λ
                info.AgeUnit = PatientAge.Text.Substring(PatientAge.Text.Length - 1);
                //����
                info.PatientInfo.Age = Neusoft.NFC.Function.NConvert.ToInt32(PatientAge.Text.Substring(0, PatientAge.Text.Length - 1)).ToString();
            }
            //���֤��
            info.PatientInfo.IDCard = IDNo.Text;
            //��Ժ;��
            //			if( InSource.Tag != null)
            //			{
            //				info.PatientInfo.PVisit.InSource.ID =  InSource.Tag.ToString();
            //			}
            //��������
            if (pactKind.Tag != null)
            {
                info.PatientInfo.Pact.PayKind.ID = pactKind.Tag.ToString();
                info.PatientInfo.Pact.ID = pactKind.Tag.ToString();
            }
            //ҽ�����Ѻ�
            info.PatientInfo.SSN = SSN.Text;
            //����
            info.PatientInfo.DIST = DIST.Text;
            //������
            info.PatientInfo.AreaCode = AreaCode.Text;
            //��ͥסַ
            info.PatientInfo.AddressHome = AddressHome.Text;
            //��ͥ�绰
            info.PatientInfo.PhoneHome = PhoneHome.Text;
            //סַ�ʱ�
            info.PatientInfo.HomeZip = HomeZip.Text;
            //��λ��ַ
            info.PatientInfo.AddressBusiness = AddressBusiness.Text;
            //��λ�绰
            info.PatientInfo.PhoneBusiness = PhoneBusiness.Text;
            //��λ�ʱ�
            info.PatientInfo.BusinessZip = BusinessZip.Text;
            //��ϵ������
            info.PatientInfo.Kin.Name = Kin.Text;
            //�뻼�߹�ϵ
            if (Relation.Tag != null)
            {
                info.PatientInfo.Kin.Relation.ID = Relation.Tag.ToString();
            }
            //��ϵ�绰
            info.PatientInfo.Kin.RelationPhone = LinkmanTel.Text;
            //��ϵ��ַ
            info.PatientInfo.Kin.RelationAddress = LinkmanAdd.Text;
            //�������ҽ�� ID
            if (ClinicDocd.Tag != null)
            {
                info.ClinicDoc.ID = ClinicDocd.Tag.ToString();
            }
            //�������ҽ������
            info.ClinicDoc.Name = ClinicDocd.Text;
            //ת��ҽԺ
            info.ComeFrom = ComeFrom.Text;
            //��Ժ����
            info.PatientInfo.PVisit.InTime = Date_In.Value;
            //סԺ����
            info.PatientInfo.InTimes = Neusoft.NFC.Function.NConvert.ToInt32(InTimes.Text);
            //��Ժ���Ҵ���
            if (DeptInHospital.Tag != null)
            {
                info.InDept.ID = DeptInHospital.Tag.ToString();
            }
            //��Ժ��������
            info.InDept.Name = DeptInHospital.Text;
            //��Ժ��Դ
            if (InAvenue.Tag != null)
            {
                info.PatientInfo.PVisit.InSource.ID = InAvenue.Tag.ToString();
                info.PatientInfo.PVisit.InSource.Name = InAvenue.Text;
            }
            //��Ժ״̬
            if (Circs.Tag != null)
            {
                info.PatientInfo.PVisit.Circs.ID = Circs.Tag.ToString();
            }
            //ȷ������
            info.DiagDate = DiagDate.Value;
            //��������
            //			info.OperationDate 
            //��Ժ����
            info.PatientInfo.PVisit.OutTime = Date_Out.Value;
            //��Ժ���Ҵ���
            if (deptOut.Tag != null)
            {
                info.OutDept.ID = deptOut.Tag.ToString();
            }
            //��Ժ��������
            info.OutDept.Name = deptOut.Text;
            //ת�����
            //			info.PatientInfo.PVisit.Zg.ID 
            //ȷ������
            //			info.DiagDays
            //סԺ����
            info.InHospitalDays = Neusoft.NFC.Function.NConvert.ToInt32(PiDays.Text);
            //��������
            //			info.DeadDate = 
            //����ԭ��
            //			info.DeadReason
            //ʬ��
            if (BodyCheck.Checked)
            {
                info.CadaverCheck = "1";
            }
            else
            {
                info.CadaverCheck = "0";
            }
            //��������
            //			info.DeadKind 
            //ʬ����ʺ�
            //			info.BodyAnotomize
            //�Ҹα��濹ԭ
            if (Hbsag.Tag != null)
            {
                info.Hbsag = Hbsag.Tag.ToString();
            }
            //���β�������
            if (HcvAb.Tag != null)
            {
                info.HcvAb = HcvAb.Tag.ToString();
            }
            //�������������ȱ�ݲ�������
            if (HivAb.Tag != null)
            {
                info.HivAb = HivAb.Tag.ToString();
            }
            //�ż�_��Ժ����
            if (CePi.Tag != null)
            {
                info.CePi = CePi.Tag.ToString();
            }
            //���_Ժ����
            if (PiPo.Tag != null)
            {
                info.PiPo = PiPo.Tag.ToString();
            }
            //��ǰ_�����
            if (OpbOpa.Tag != null)
            {
                info.OpbOpa = OpbOpa.Tag.ToString();
            }
            //�ٴ�_�������

            //�ٴ�_CT����
            //�ٴ�_MRI����
            //�ٴ�_�������
            if (ClPa.Tag != null)
            {
                info.ClPa = ClPa.Tag.ToString();
            }
            //����_�������
            if (FsBl.Tag != null)
            {
                info.FsBl = FsBl.Tag.ToString();
            }
            //���ȴ���
            info.SalvTimes = Neusoft.NFC.Function.NConvert.ToInt32(SalvTimes.Text.Trim());
            //�ɹ�����
            info.SuccTimes = Neusoft.NFC.Function.NConvert.ToInt32(SuccTimes.Text.Trim());
            //ʾ�̿���
            if (TechSerc.Checked)
            {
                info.TechSerc = "1";
            }
            else
            {
                info.TechSerc = "0";
            }
            //�Ƿ�����
            if (VisiStat.Checked)
            {
                info.VisiStat = "1";
            }
            else
            {
                info.VisiStat = "0";
            }
            //������� ��
            info.VisiPeriodWeek = VisiPeriWeek.Text;
            //������� ��
            info.VisiPeriodMonth = VisiPeriMonth.Text;
            //������� ��
            info.VisiPeriodYear = VisiPeriMonth.Text;
            //Ժ�ʻ������
            info.InconNum = Neusoft.NFC.Function.NConvert.ToInt32(InconNum.Text.Trim());
            //Զ�̻���
            info.OutconNum = Neusoft.NFC.Function.NConvert.ToInt32(outOutconNum.Text.Trim());
            //ҩ�����
            //			info.AnaphyFlag 
            //����ҩ������
            //			info.AnaphyName1
            //����ҩ������
            //			info.AnaphyName2
            //���ĺ��Ժ����
            //			info.CoutDate
            //סԺҽʦ����
            if (AdmittingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = AdmittingDoctor.Tag.ToString();
                //סԺҽʦ����
                info.PatientInfo.PVisit.AdmittingDoctor.Name = AdmittingDoctor.Text;
            }
            //����ҽʦ����
            if (AttendingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = AttendingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.AttendingDoctor.Name = AttendingDoctor.Text;
            }
            //����ҽʦ����
            if (ConsultingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = ConsultingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.ConsultingDoctor.Name = ConsultingDoctor.Text;
            }
            //�����δ���
            //			info.PatientInfo.PVisit.ReferringDoctor.ID
            //����ҽʦ����
            if (RefresherDocd.Tag != null)
            {
                info.RefresherDoc.ID = RefresherDocd.Tag.ToString();
                info.RefresherDoc.Name = RefresherDocd.Text;
            }
            //�о���ʵϰҽʦ����
            if (GraDocCode.Tag != null)
            {
                info.PatientInfo.PVisit.TempDoctor.ID = GraDocCode.Tag.ToString();
                info.PatientInfo.PVisit.TempDoctor.Name = GraDocCode.Text.Trim();
            }
            //ʵϰҽʦ����
            if (PraDocCode.Tag != null)
            {
                info.GraduateDoc.ID = PraDocCode.Tag.ToString();
                info.GraduateDoc.Name = PraDocCode.Text.Trim();
            }
            //����Ա
            if (CodingCode.Tag != null)
            {
                info.CodingOper.ID = CodingCode.Tag.ToString();
                info.CodingOper.Name = CodingCode.Text.Trim();
            }
            //��������
            if (MrQual.Tag != null)
            {
                info.MrQuality = MrQual.Tag.ToString();
            }
            //�ϸ񲡰�
            //			info.MrElig
            //�ʿ�ҽʦ����
            if (QcDocd.Tag != null)
            {
                info.QcDoc.ID = QcDocd.Tag.ToString();
                info.QcDoc.Name = QcDocd.Text.Trim();
            }
            //�ʿػ�ʿ����
            if (QcNucd.Tag != null)
            {
                info.QcNurse.ID = QcNucd.Tag.ToString();
                info.QcNurse.Name = QcNucd.Text.Trim();
            }
            //���ʱ��
            info.CheckDate = CheckDate.Value;
            //�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
            if (YnFirst.Checked)
            {
                info.YnFirst = "1";
            }
            else
            {
                info.YnFirst = "0";
            }
            //RhѪ��(����)
            if (RhBlood.Tag != null)
            {
                info.RhBlood = RhBlood.Tag.ToString();
            }
            //��Ѫ��Ӧ�����ޣ�
            if (ReactionBlood.Tag != null)
            {
                info.ReactionBlood = ReactionBlood.Tag.ToString();
            }
            //��ϸ����
            info.BloodRed = BloodRed.Text;
            //ѪС����
            info.BloodPlatelet = BloodPlatelet.Text;
            //Ѫ����
            info.BodyAnotomize = BodyAnotomize.Text;
            //ȫѪ��
            info.BloodWhole = BloodWhole.Text;
            //������Ѫ��
            info.BloodOther = BloodOther.Text;
            //X���
            info.XQty = Neusoft.NFC.Function.NConvert.ToInt32(XNumb.Text);
            //CT��
            info.CTQty = Neusoft.NFC.Function.NConvert.ToInt32(CtNumb.Text);
            //MRI��
            info.MRQty = Neusoft.NFC.Function.NConvert.ToInt32(MriNumb.Text);
            // UFCT 
            info.PathNum = PathNumb.Text;
            if (bchao.Checked)
            {
                info.DsaNum = "1";
            }
            //DSA��
            //			info.DsaNumb
            //PET��
            //			info.PetNumb
            //ECT��
            //			info.EctNumb
            //X�ߴ���
            //			info.XTimes
            //CT����
            //			info.CtTimes
            //MR����
            //			info.MrTimes;
            //DSA����
            //			info.DsaTimes
            //PET����
            //			info.PetTimes
            //ECT����
            //			info.EctTimes
            //˵��
            //			info.Memo
            //�鵵�����
            //			info.BarCode
            //��������״̬(O��� I�ڼ�)
            //			info.LendStus
            //����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч
            //			info.CaseStus 
            //�ؼ�����ʱ��
            info.SuperNus = Neusoft.NFC.Function.NConvert.ToInt32(SuperNus.Text);
            //I������ʱ��
            info.INus = Neusoft.NFC.Function.NConvert.ToInt32(INus.Text);
            //II������ʱ��
            info.IINus = Neusoft.NFC.Function.NConvert.ToInt32(IINus.Text);
            //III������ʱ��
            info.IIINus = Neusoft.NFC.Function.NConvert.ToInt32(IIINus.Text);
            //��֢�໤ʱ��
            info.StrictNuss = Neusoft.NFC.Function.NConvert.ToInt32(StrictNuss.Text);
            //���⻤��
            info.SpecalNus = Neusoft.NFC.Function.NConvert.ToInt32(SPecalNus.Text);
            if (InputDoc.Tag != null)
            {
                info.OperInfo.ID = InputDoc.Tag.ToString();
            }
            //����Ա 
            if (textBox33.Tag != null)
            {
                info.PackupMan.ID = textBox33.Tag.ToString();
            }
            if (this.OperationCode.Tag != null)
            {
                info.OperationCoding.ID = this.OperationCode.Tag.ToString();
            }
            //������ 
            if (checkBox8.Checked)
            {
                info.Disease30 = "1";
            }
            else
            {
                info.Disease30 = "0";
            }
            info.LendStat = "1"; //��������״̬ 0 Ϊ��� 1Ϊ�ڼ� 
            if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC)
            {
                info.PatientInfo.CaseState = "2";
            }
            else if (this.frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS) //������¼�벡��
            {
                info.PatientInfo.CaseState = "3";
            }
            //�Ƿ��в���֢
            //info.SyndromeFlag = this.ucDiagNoseInput1.GetSyndromeFlag(); // zjy
            info.InfectionNum = Neusoft.NFC.Function.NConvert.ToInt32(infectNum.Text);  //��Ⱦ����
            if (this.CaseBase.LendStat == null || this.CaseBase.LendStat == "") //��������״̬ 
            {
                info.LendStat = "I";
            }
            else
            {
                info.LendStat = this.CaseBase.LendStat;
            }
            return 0;
        }

        /// <summary>
        /// ���ݴ���Ĳ�����Ϣ�Ĳ���״̬,���ز�����Ϣ 
        /// </summary>
        /// <param name="InpatientNo">����סԺ��ˮ��</param>
        /// <param name="Type">����</param>
        /// <returns>-1 �������,����Ĳ�����ϢΪ�� 0 ���˲������в��� 1 �ֹ�¼����Ϣ </returns>
        public int LoadInfo(string InpatientNo, Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                if (InpatientNo == null || InpatientNo == "")
                {
                    MessageBox.Show("�����סԺ��ˮ��Ϊ��");
                    return -1;
                }
                Neusoft.HISFC.Integrate.RADT pa = new Neusoft.HISFC.Integrate.RADT();
                Neusoft.HISFC.Object.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
                //��סԺ�����в�Ѯ
                patientInfo = pa.GetPatientInfoByPatientNO(InpatientNo);
                if (patientInfo == null)
                {
                    patientInfo = new Neusoft.HISFC.Object.RADT.PatientInfo();
                }
                //1. ��� סԺ�������м�¼����ȡ��Ϣ д��������. 
                //2. ���סԺ������û����Ϣ ��ȥ��������ȥ��ѯ 1������������м�¼ ��ȡ��Ϣ ���ص�������  2. �����������û�м�¼ �� ��ʾ�����ֹ�¼��,
                if (patientInfo.ID == "")//סԺ������û�м�¼
                {
                    //��ѯ��������  ���˻�����Ϣ
                    CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);
                    if (CaseBase.PatientInfo.ID == "") //����������Ҳû����ػ�����Ϣ
                    {
                        //��ֵסԺ��ˮ��
                        CaseBase.PatientInfo.ID = InpatientNo;
                        //�˳� �ֹ�¼����Ϣ
                        CaseBase.IsHandCraft = "1";
                        //��һ��ά��,������� 
                        HandCraft = 1;
                        return 1;
                    }
                    else
                    {
                        //��ǰά����,���²��� 
                        HandCraft = 2;
                        //�ֹ�¼����
                        CaseBase.IsHandCraft = "1";
                    }
                }
                else //סԺ�������м�¼ 
                {
                    CaseBase.PatientInfo =  patientInfo;
                }
                //������ֹ�¼�벡�� ���ܲ�ѯ��������Ϣ��Ϊ�� ֻ�д����InpatientNo ��Ϊ��
                this.frmType = Type;
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("�ò��˲������в���");
                    return 0;
                }
                //���没����״̬
                CaseFlag = Neusoft.NFC.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState);

                #region  ת����Ϣ
                //����ת����Ϣ���б�
                ArrayList changeDept = new ArrayList();
                //��ȡת����Ϣ
                changeDept = deptShift.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "2");
                firDept = null;
                secDept = null;
                thirDept = null;
                if (changeDept != null)
                {
                    if (changeDept.Count == 0)
                    {
                        changeDept = deptShift.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "1");
                    }
                    //���� 
                    LoadChangeDept(changeDept);
                }
                #endregion
                if (frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC) // ҽ��վ¼�벡��
                {
                    #region  ҽ��վ¼�벡��

                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (CaseBase.PatientInfo.CaseState == "1")
                    {
                        //��סԺ�����л�ȡ��Ϣ ����ʾ�ڽ����� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    // ҽ��վ¼������� 
                    else if (CaseBase.PatientInfo.CaseState == "2")
                    {
                        //�Ӳ����������л�ȡ��Ϣ ����ʾ�ڽ����� 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                            return -1;
                        }
                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else
                    {
                        // �����Ѿ�����Ѿ�������ҽ���޸�
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }

                    #endregion
                }
                else if (frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS)//������¼�벡��
                {
                    #region ������¼�벡��
                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (CaseBase.PatientInfo.CaseState == "1")
                    {
                        //��סԺ�����л�ȡ��Ϣ ����ʾ�ڽ����� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "2" || CaseBase.PatientInfo.CaseState == "3")
                    {
                        //					//ҽ��վ�Ѿ�¼�벡��
                        ////					list = diag.QueryCaseDiagnose(patientInfo.ID,"%","1");
                        //				}
                        //				else if( patientInfo.Patient.CaseState == "3")
                        //				{
                        //�Ӳ����������л�ȡ��Ϣ ����ʾ�ڽ����� 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                            return -1;
                        }
                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else if ((CaseBase.PatientInfo.CaseState == "" || CaseBase.PatientInfo.CaseState == null) && CaseBase.IsHandCraft == "1")
                    {
                        //�������
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "4")
                    {
                        //�����Ѿ���� �������޸ġ�
                        //					MessageBox.Show("�����Ѿ����,�������޸�");
                        ConvertInfoToPanel(CaseBase);
                        this.SetReadOnly(true); //��Ϊֻ�� 
                    }

                    #endregion
                }
                else
                {
                    //û�д������ �����κδ���
                }

                //��ʾ������Ϣ
                //this.tab1.SelectedIndex = 0;
                //������
                this.caseNum.Focus();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// ����ǰ����ת����Ϣ
        /// </summary>
        /// <param name="list"></param>
        private void LoadChangeDept(ArrayList list)
        {
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
                            firDept = (Neusoft.HISFC.Object.RADT.Location)list[0];
                            break;
                        case 1:
                            secDept = (Neusoft.HISFC.Object.RADT.Location)list[1];
                            break;
                        case 2:
                            thirDept = (Neusoft.HISFC.Object.RADT.Location)list[2];
                            break;
                    }
                }
            }
            #endregion

            #region ת����Ϣ
            if (this.firDept != null)
            {
                firstDept.Text = firDept.Dept.Name;
                firstDept.Tag = firDept.Dept.ID;
                this.dateTimePicker3.Value = Neusoft.NFC.Function.NConvert.ToDateTime(firDept.User01);
            }
            if (this.secDept != null)
            {
                deptSecond.Text = this.secDept.Dept.Name;
                deptSecond.Tag = this.secDept.Dept.ID;
                this.dateTimePicker3.Value = Neusoft.NFC.Function.NConvert.ToDateTime(secDept.User01);
            }
            if (this.thirDept != null)
            {
                deptThird.Text = this.thirDept.Dept.Name;
                deptThird.Tag = this.thirDept.Dept.ID;
                this.dateTimePicker3.Value = Neusoft.NFC.Function.NConvert.ToDateTime(thirDept.User01);
            }
            #endregion
        }

        private void ucCaseMainInfo_Load(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// ���� ��Ժ��Ϻ��������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int LoadDiag(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            clinicDiag = null;
            InDiag = null;
            #region ��Ĭ������һ�����������
            foreach (Neusoft.HISFC.Object.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10" && obj.DiagInfo.IsMain)
                {	//������� 
                    this.ClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                    this.ClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                    this.ClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                    this.ClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                    clinicDiag = obj;
                }
                else if (obj.DiagInfo.DiagType.ID == "11" && obj.DiagInfo.IsMain)
                {	//��Ժ���
                    RuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    RuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    InDiag = obj;
                }
            }
            #endregion

            #region ���û������� ���������������
            foreach (Neusoft.HISFC.Object.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10")
                {	//������� 
                    if (this.ClinicDiag.Tag == null)
                    {
                        this.ClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                        this.ClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                        this.ClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                        this.ClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                        clinicDiag = obj;
                    }
                }
                else if (obj.DiagInfo.DiagType.ID == "11")
                {	//��Ժ���
                    if (RuyuanDiagNose.Tag == null)
                    {
                        RuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                        RuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                        InDiag = obj;
                    }
                }
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// �������ݵĺϷ���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ValidState(Neusoft.HISFC.Object.HealthRecord.Base info)
        {
            if (info.PatientInfo.ID.Length > 14)
            {
                MessageBox.Show("סԺ��ˮ�Ź���");
                return -1;
            }
            if (info.PatientInfo.PID.PatientNO.Length > 10)
            {
                caseNum.Focus();
                MessageBox.Show("סԺ�Ź���");
                return -1;
            }
            if (info.PatientInfo.PID.CardNO.Length > 10)
            {
                clinicNo.Focus();
                MessageBox.Show("���￨�Ź���");
                return -1;
            }
            if (info.PatientInfo.Name.Length > 20)
            {
                PatientName.Focus();
                MessageBox.Show("��������");
                return -1;
            }
            if (info.Nomen.Length > 16)
            {
                Nomen.Focus();
                MessageBox.Show("����������");
                return -1;
            }
            if (info.PatientInfo.Sex.ID.ToString().Length > 1)
            {
                PatientSex.Focus();
                MessageBox.Show("�Ա�������");
                return -1;
            }

            if (info.PatientInfo.Country.ID.Length > 3)
            {
                Country.Focus();
                MessageBox.Show("�����������");
                return -1;
            }

            if (info.PatientInfo.Nationality.ID.Length > 2)
            {
                Nationality.Focus();
                MessageBox.Show("����������");
                return -1;
            }
            if (info.PatientInfo.Profession.ID.Length > 2)
            {
                Profession.Focus();
                MessageBox.Show("ְҵ�������");
                return -1;
            }
            if (info.PatientInfo.BloodType.ID != null)
            {
                if (info.PatientInfo.BloodType.ID.ToString().Length > 2)
                {
                    BloodType.Focus();
                    MessageBox.Show("Ѫ�ͱ������");
                    return -1;
                }
            }
            if (info.PatientInfo.MaritalStatus.ID != null)
            {
                if (info.PatientInfo.MaritalStatus.ID.ToString().Length > 1)
                {
                    MaritalStatus.Focus();
                    MessageBox.Show("�����������");
                    return -1;
                }
            }
            if (info.AgeUnit != null)
            {
                if (info.AgeUnit.Length > 1)
                {
                    PatientAge.Focus();
                    MessageBox.Show("���䵥λ����");
                    return -1;
                }
            }

            if (Neusoft.NFC.Function.NConvert.ToInt32(info.PatientInfo.Age) > 999)
            {
                PatientAge.Focus();
                MessageBox.Show("�������");
                return -1;
            }
            if (info.PatientInfo.IDCard.Length > 18)
            {
                IDNo.Focus();
                MessageBox.Show("���֤����");
                return -1;
            }
            //			if(info.PatientInfo.PVisit.InSource.ID.Length  > 1 )
            //			{
            //				In.Focus();
            //				MessageBox.Show("������Դ�������");
            //				return -1;
            //			} 
            if (info.PatientInfo.Pact.PayKind.ID.Length > 02)
            {
                pactKind.Focus();
                MessageBox.Show("�������������");
                return -1;
            }

            if (info.PatientInfo.Pact.ID.Length > 10)
            {
                pactKind.Focus();
                MessageBox.Show("��ͬ��λ�������");
                return -1;
            }

            if (info.PatientInfo.SSN.Length > 18)
            {
                SSN.Focus();
                MessageBox.Show("ҽ�����ѺŹ���");
                return -1;
            }

            if (info.PatientInfo.DIST.Length > 50)
            {
                DIST.Focus();
                MessageBox.Show("�������");
                return -1;
            }

            if (info.PatientInfo.AddressHome.Length > 50)
            {
                AddressHome.Focus();
                MessageBox.Show("��ͥסַ����");
                return -1;
            }

            if (info.PatientInfo.PhoneHome.Length > 25)
            {
                PhoneHome.Focus();
                MessageBox.Show("��ͥ�绰����");
                return -1;
            }

            if (info.PatientInfo.HomeZip.Length > 25)
            {
                HomeZip.Focus();
                MessageBox.Show("סַ�ʱ����");
                return -1;
            }

            if (info.PatientInfo.AddressBusiness.Length > 25)
            {
                AddressBusiness.Focus();
                MessageBox.Show("��λ��ַ����");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����Ϊֻ��
        /// </summary>
        /// <param name="type"></param>
        public void SetReadOnly(bool type)
        {
            //������ 
            caseNum.ReadOnly = type;
            caseNum.BackColor = System.Drawing.Color.White;
            //סԺ����
            InTimes.ReadOnly = type;
            InTimes.BackColor = System.Drawing.Color.White;
            //�������
            pactKind.ReadOnly = type;
            pactKind.BackColor = System.Drawing.Color.White;
            //ҽ����
            SSN.ReadOnly = type;
            SSN.BackColor = System.Drawing.Color.White;
            //�����
            clinicNo.ReadOnly = type;
            clinicNo.BackColor = System.Drawing.Color.White;
            //����
            PatientName.ReadOnly = type;
            PatientName.BackColor = System.Drawing.Color.White;
            //�Ա�
            PatientSex.ReadOnly = type;
            PatientSex.BackColor = System.Drawing.Color.White;
            //����
            patientBirthday.Enabled = !type;
            //����
            PatientAge.ReadOnly = type;
            PatientAge.BackColor = System.Drawing.Color.White;
            //����
            MaritalStatus.ReadOnly = type;
            MaritalStatus.BackColor = System.Drawing.Color.White;
            //ְҵ
            Profession.ReadOnly = type;
            Profession.BackColor = System.Drawing.Color.White;
            //������
            AreaCode.ReadOnly = type;
            AreaCode.BackColor = System.Drawing.Color.White;
            //����
            Nationality.ReadOnly = type;
            Nationality.BackColor = System.Drawing.Color.White;
            //����
            Country.ReadOnly = type;
            Country.BackColor = System.Drawing.Color.White;
            //����
            DIST.ReadOnly = type;
            DIST.BackColor = System.Drawing.Color.White;
            //���֤
            IDNo.ReadOnly = type;
            IDNo.BackColor = System.Drawing.Color.White;
            //������λ
            AddressBusiness.ReadOnly = type;
            AddressBusiness.BackColor = System.Drawing.Color.White;
            //��λ�ʱ�
            BusinessZip.ReadOnly = type;
            BusinessZip.BackColor = System.Drawing.Color.White;
            //��λ�绰
            PhoneBusiness.ReadOnly = type;
            PhoneBusiness.BackColor = System.Drawing.Color.White;
            //���ڵ�ַ
            AddressHome.ReadOnly = type;
            AddressHome.BackColor = System.Drawing.Color.White;
            //�����ʱ�
            HomeZip.ReadOnly = type;
            HomeZip.BackColor = System.Drawing.Color.White;
            //��ͥ�绰
            PhoneHome.ReadOnly = type;
            PhoneHome.BackColor = System.Drawing.Color.White;
            //��ϵ�� 
            Kin.ReadOnly = type;
            Kin.BackColor = System.Drawing.Color.White;
            //��ϵ
            Relation.ReadOnly = type;
            Relation.BackColor = System.Drawing.Color.White;
            //��ϵ�绰
            LinkmanTel.ReadOnly = type;
            LinkmanTel.BackColor = System.Drawing.Color.White;
            //l��ϵ�˵�ַ
            LinkmanAdd.ReadOnly = type;
            LinkmanAdd.BackColor = System.Drawing.Color.White;
            //��Ժ����
            DeptInHospital.ReadOnly = type;
            DeptInHospital.BackColor = System.Drawing.Color.White;
            //��Ժʱ�� 
            Date_In.Enabled = !type;
            //��Ժ���
            Circs.ReadOnly = type;
            Circs.BackColor = System.Drawing.Color.White;
            //ת�����
            firstDept.ReadOnly = type;
            firstDept.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dateTimePicker3.Enabled = !type;
            dateTimePicker3.BackColor = System.Drawing.Color.White;
            //ת�����
            deptSecond.ReadOnly = type;
            deptSecond.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dateTimePicker4.Enabled = !type;
            //ת�����
            deptThird.ReadOnly = type;
            deptThird.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dateTimePicker5.Enabled = !type;
            //��Ժ����
            deptOut.ReadOnly = type;
            deptOut.BackColor = System.Drawing.Color.White;
            //��Ժʱ��
            Date_Out.Enabled = !type;
            //�������
            //			ClinicDiag.ReadOnly = type;
            ClinicDiag.BackColor = System.Drawing.Color.Gainsboro;
            //���ҽ��
            ClinicDocd.ReadOnly = type;
            ClinicDocd.BackColor = System.Drawing.Color.White;
            //סԺ����
            PiDays.ReadOnly = type;
            PiDays.BackColor = System.Drawing.Color.White;
            //ȷ֤ʱ��
            DiagDate.Enabled = !type;
            //��Ժ���
            //			RuyuanDiagNose.ReadOnly = type;
            RuyuanDiagNose.BackColor = System.Drawing.Color.Gainsboro;
            //�ɺ�ҽԺת��
            ComeFrom.ReadOnly = type;
            ComeFrom.BackColor = System.Drawing.Color.White;
            //������
            Nomen.ReadOnly = type;
            Nomen.BackColor = System.Drawing.Color.White;
            //������Դ
            InAvenue.ReadOnly = type;
            InAvenue.BackColor = System.Drawing.Color.White;
            //Ժ�д���
            infectNum.ReadOnly = type;
            infectNum.BackColor = System.Drawing.Color.White;
            //hbsag
            Hbsag.ReadOnly = type;
            Hbsag.BackColor = System.Drawing.Color.White;
            HcvAb.ReadOnly = type;
            HcvAb.BackColor = System.Drawing.Color.White;
            HivAb.ReadOnly = type;
            HivAb.BackColor = System.Drawing.Color.White;
            //�������Ժ
            CePi.ReadOnly = type;
            CePi.BackColor = System.Drawing.Color.White;
            //��Ժ���Ժ 
            PiPo.ReadOnly = type;
            PiPo.BackColor = System.Drawing.Color.White;
            //��ǰ������
            OpbOpa.ReadOnly = type;
            OpbOpa.BackColor = System.Drawing.Color.White;
            //�ٴ��벡��
            ClPa.ReadOnly = type;
            ClPa.BackColor = System.Drawing.Color.White;
            //�����벡��
            FsBl.ReadOnly = type;
            FsBl.BackColor = System.Drawing.Color.White;
            //���ȴ���
            SalvTimes.ReadOnly = type;
            SalvTimes.BackColor = System.Drawing.Color.White;
            //�ɹ�����
            SuccTimes.ReadOnly = type;
            SuccTimes.BackColor = System.Drawing.Color.White;
            //��������
            MrQual.ReadOnly = type;
            MrQual.BackColor = System.Drawing.Color.White;
            //�ʿ�ҽʦ
            QcDocd.ReadOnly = type;
            QcDocd.BackColor = System.Drawing.Color.White;
            //�ʿػ�ʿ
            QcNucd.ReadOnly = type;
            QcNucd.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            ConsultingDoctor.ReadOnly = type;
            ConsultingDoctor.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            AttendingDoctor.ReadOnly = type;
            AttendingDoctor.BackColor = System.Drawing.Color.White;
            //סԺҽʦ
            AdmittingDoctor.ReadOnly = type;
            AdmittingDoctor.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            RefresherDocd.ReadOnly = type;
            RefresherDocd.BackColor = System.Drawing.Color.White;
            //�о���ʵϰҽʦ
            GraDocCode.ReadOnly = type;
            GraDocCode.BackColor = System.Drawing.Color.White;
            //�ʿ�ʱ��
            CheckDate.Enabled = !type;
            //ʵϰҽ��
            PraDocCode.ReadOnly = type;
            PraDocCode.BackColor = System.Drawing.Color.White;
            //����Ա
            CodingCode.ReadOnly = type;
            CodingCode.BackColor = System.Drawing.Color.White;
            //����Ա 
            textBox33.ReadOnly = type;
            textBox33.BackColor = System.Drawing.Color.White;
            this.OperationCode.ReadOnly = type;
            this.OperationCode.BackColor = System.Drawing.Color.White;
            //ʬ�
            BodyCheck.Enabled = !type;
            //���������ơ���顢��ϡ��Ƿ�Ժ����
            YnFirst.Enabled = !type;
            //����
            VisiStat.Enabled = !type;
            //��������
            VisiPeriWeek.ReadOnly = type;
            VisiPeriWeek.BackColor = System.Drawing.Color.White;
            VisiPeriMonth.ReadOnly = type;
            VisiPeriMonth.BackColor = System.Drawing.Color.White;
            VisiPeriYear.ReadOnly = type;
            VisiPeriYear.BackColor = System.Drawing.Color.White;
            //ʾ�̲���
            TechSerc.Enabled = !type;
            //������
            checkBox8.Enabled = !type;
            //Ѫ��
            BloodType.ReadOnly = type;
            BloodType.BackColor = System.Drawing.Color.White;
            RhBlood.ReadOnly = type;
            RhBlood.BackColor = System.Drawing.Color.White;
            //��Ѫ��Ӧ
            ReactionBlood.ReadOnly = type;
            ReactionBlood.BackColor = System.Drawing.Color.White;
            //��ϸ��
            BloodRed.ReadOnly = type;
            BloodRed.BackColor = System.Drawing.Color.White;
            //ѪС��
            BloodPlatelet.ReadOnly = type;
            BloodPlatelet.BackColor = System.Drawing.Color.White;
            //Ѫ��
            BodyAnotomize.ReadOnly = type;
            BodyAnotomize.BackColor = System.Drawing.Color.White;
            //ȫѪ
            BloodWhole.ReadOnly = type;
            BloodWhole.BackColor = System.Drawing.Color.White;
            //����
            BloodOther.ReadOnly = type;
            BloodOther.BackColor = System.Drawing.Color.White;
            //Ժ�ʻ���
            InconNum.ReadOnly = type;
            InconNum.BackColor = System.Drawing.Color.White;
            //Զ�̻���
            outOutconNum.ReadOnly = type;
            outOutconNum.BackColor = System.Drawing.Color.White;
            //SuperNus �ؼ�����
            SuperNus.ReadOnly = type;
            SuperNus.BackColor = System.Drawing.Color.White;
            //һ������
            INus.ReadOnly = type;
            INus.BackColor = System.Drawing.Color.White;
            //��������
            IINus.ReadOnly = type;
            IINus.BackColor = System.Drawing.Color.White;
            //��������
            IIINus.ReadOnly = type;
            IIINus.BackColor = System.Drawing.Color.White;
            //��֢�໤
            StrictNuss.ReadOnly = type;
            StrictNuss.BackColor = System.Drawing.Color.White;
            //���⻤��
            SPecalNus.ReadOnly = type;
            SPecalNus.BackColor = System.Drawing.Color.White;
            //ct
            CtNumb.ReadOnly = type;
            CtNumb.BackColor = System.Drawing.Color.White;
            //UCFT
            PathNumb.ReadOnly = type;
            PathNumb.BackColor = System.Drawing.Color.White;
            //MR
            MriNumb.ReadOnly = type;
            MriNumb.BackColor = System.Drawing.Color.White;
            //X��
            XNumb.ReadOnly = type;
            XNumb.BackColor = System.Drawing.Color.White;
            //B��
            bchao.Enabled = !type;
            //����Ա
            InputDoc.ReadOnly = type;
            InputDoc.BackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// �����������
        /// </summary>
        public void ClearInfo()
        {
            //������ 
            caseNum.Text = "";
            //סԺ����
            InTimes.Text = "";
            //�������
            pactKind.Text = "";
            //ҽ����
            SSN.Text = "";
            //�����
            clinicNo.Text = "";
            //����
            PatientName.Text = "";
            //�Ա�
            PatientSex.Text = "";
            //����
            //			patientBirthday.Enabled = !type;
            //����
            PatientAge.Text = "";
            //����
            MaritalStatus.Text = "";
            //ְҵ
            Profession.Text = "";
            //������
            AreaCode.Text = "";
            //����
            Nationality.Text = "";
            //����
            Country.Text = "";
            //����
            DIST.Text = "";
            //���֤
            IDNo.Text = "";
            //������λ
            AddressBusiness.Text = "";
            //��λ�ʱ�
            BusinessZip.Text = "";
            //��λ�绰
            PhoneBusiness.Text = "";
            //���ڵ�ַ
            AddressHome.Text = "";
            //�����ʱ�
            HomeZip.Text = "";
            //��ͥ�绰
            PhoneHome.Text = "";
            //��ϵ�� 
            Kin.Text = "";
            //��ϵ
            Relation.Text = "";
            //��ϵ�绰
            LinkmanTel.Text = "";
            //l��ϵ�˵�ַ
            LinkmanAdd.Text = "";
            //��Ժ����
            DeptInHospital.Text = "";
            //��Ժʱ�� 
            //			Date_In.Enabled = !type;
            //��Ժ���
            Circs.Text = "";
            //ת�����
            firstDept.Text = "";
            //ת��ʱ��
            dateTimePicker3.Value = System.DateTime.Now;
            //ת�����
            deptSecond.Text = "";
            //ת��ʱ��
            dateTimePicker4.Value = System.DateTime.Now;
            //ת�����
            deptThird.Text = "";
            //ת��ʱ��
            dateTimePicker5.Value = System.DateTime.Now;
            //��Ժ����
            deptOut.Text = "";
            //��Ժʱ��
            Date_Out.Value = System.DateTime.Now;
            //�������
            ClinicDiag.Text = "";
            //���ҽ��
            ClinicDocd.Text = "";
            //סԺ����
            PiDays.Text = "";
            //ȷ֤ʱ��
            DiagDate.Value = System.DateTime.Now;
            //��Ժ���
            RuyuanDiagNose.Text = "";
            //�ɺ�ҽԺת��
            ComeFrom.Text = "";
            //������
            Nomen.Text = "";
            //������Դ
            InAvenue.Text = "";
            //Ժ�д���
            infectNum.Text = "";
            //hbsag
            Hbsag.Text = "";
            HcvAb.Text = "";
            HivAb.Text = "";
            //�������Ժ
            CePi.Text = "";
            //��Ժ���Ժ 
            PiPo.Text = "";
            //��ǰ������
            OpbOpa.Text = "";
            //�ٴ��벡��
            ClPa.Text = "";
            //�����벡��
            FsBl.Text = "";
            //���ȴ���
            SalvTimes.Text = "";
            //�ɹ�����
            SuccTimes.Text = "";
            //��������
            MrQual.Text = "";
            //�ʿ�ҽʦ
            QcDocd.Text = "";
            //�ʿػ�ʿ
            QcNucd.Text = "";
            //����ҽʦ
            ConsultingDoctor.Text = "";
            //����ҽʦ
            AttendingDoctor.Text = "";
            //סԺҽʦ
            AdmittingDoctor.Text = "";
            //����ҽʦ
            RefresherDocd.Text = "";
            //�о���ʵϰҽʦ
            GraDocCode.Text = "";
            //�ʿ�ʱ��
            CheckDate.Value = System.DateTime.Now;
            //ʵϰҽ��
            PraDocCode.Text = "";
            //����Ա
            CodingCode.Text = "";
            //����Ա 
            textBox33.Text = "";
            this.OperationCode.Tag = null;
            this.OperationCode.Text = "";
            //ʬ�
            BodyCheck.Checked = false;
            //���������ơ���顢��ϡ��Ƿ�Ժ����
            YnFirst.Checked = false;
            //����
            VisiStat.Checked = false;
            //��������
            VisiPeriWeek.Text = "";
            VisiPeriMonth.Text = "";
            VisiPeriYear.Text = "";
            //ʾ�̲���
            TechSerc.Checked = false;
            //������
            checkBox8.Checked = false;
            //Ѫ��
            BloodType.Text = "";
            RhBlood.Text = "";
            //��Ѫ��Ӧ
            ReactionBlood.Text = "";
            //��ϸ��
            BloodRed.Text = "";
            //ѪС��
            BloodPlatelet.Text = "";
            //Ѫ��
            BodyAnotomize.Text = "";
            //ȫѪ
            BloodWhole.Text = "";
            //����
            BloodOther.Text = "";
            //Ժ�ʻ���
            InconNum.Text = "";
            //Զ�̻���
            outOutconNum.Text = "";
            //SuperNus �ؼ�����
            SuperNus.Text = "";
            //һ������
            INus.Text = "";
            //��������
            IINus.Text = "";
            //��������
            IIINus.Text = "";
            //��֢�໤
            StrictNuss.Text = "";
            //���⻤��
            SPecalNus.Text = "";
            //ct
            CtNumb.Text = "";
            //UCFT
            PathNumb.Text = "";
            //MR
            MriNumb.Text = "";
            //X��
            XNumb.Text = "";
            //B��
            bchao.Checked = false;
            //����Ա
            InputDoc.Text = "";
        }
        private void patientBirthday_ValueChanged(object sender, System.EventArgs e)
        {
            if (patientBirthday.Value > System.DateTime.Now)
            {
                patientBirthday.Value = System.DateTime.Now;
            }
            if (patientBirthday.Value.Year == System.DateTime.Now.Year)
            {
                if (patientBirthday.Value.Month == System.DateTime.Now.Month)
                {
                    System.TimeSpan span = System.DateTime.Now - patientBirthday.Value;
                    if (span.Days != 0) //��
                    {
                        PatientAge.Text = span.Days + "��";
                    }
                    else
                    {
                        PatientAge.Text = span.Hours + "Сʱ";
                    }
                }
                else //��
                {
                    PatientAge.Text = Convert.ToString(System.DateTime.Now.Month - patientBirthday.Value.Month) + "��"; ;
                }
            }
            else //��
            {
                PatientAge.Text = Convert.ToString(System.DateTime.Now.Year - patientBirthday.Value.Year) + "��";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diag"></param>
        /// <returns></returns>
        public int DiagValueState(Neusoft.HISFC.Management.HealthRecord.Diagnose diag)
        {
            //ArrayList allList = new ArrayList();
            //this.ucDiagNoseInput1.GetAllDiagnose(allList);
            //if (allList == null)
            //{
            //    return -1;
            //}
            //if (allList.Count == 0)
            //{
            //    return 1;
            //}
            //Neusoft.HISFC.Object.Base.EnumSex sex;
            //if (CaseBase.Patient.Sex.ID.ToString() == "F")
            //{
            //    sex = Neusoft.HISFC.Object.Base.EnumSex.F;
            //}
            //else if (CaseBase.Patient.Sex.ID.ToString() == "M")
            //{
            //    sex = Neusoft.HISFC.Object.Base.EnumSex.M;
            //}
            //else
            //{
            //    sex = Neusoft.HISFC.Object.Base.EnumSex.U;
            //}
            ////����
            //ArrayList diagCheckList = diag.DiagnoseValueState(allList, sex);
            //Neusoft.Common.Controls.ucDiagnoseCheck ucdia = new Neusoft.Common.Controls.ucDiagnoseCheck();
            //if (diagCheckList == null)
            //{
            //    MessageBox.Show("��ȡԼ������");
            //    return -1;
            //}
            //if (diagCheckList.Count == 0)
            //{
            //    return 1;
            //}
            //try
            //{
            //    if (frm != null)
            //    {
            //        frm.Close();
            //    }
            //}
            //catch { }

            //frm = new ucDiagNoseCheck();
            //frm.initDiangNoseCheck(diagCheckList);
            //if (frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC)
            //{
            //    frm.Show();
            //    if (frm.GetRedALarm())
            //    {
            //        return -1;
            //    }
            //}
            ////			else if(frmType == Neusoft.HISFC.Object.HealthRecord.EnumServer.frmTypes.CAS)
            ////			{
            ////				frm.ShowDialog();
            ////				if(frm.GetRedALarm() )
            ////				{
            ////					return -1;
            ////				}
            ////			}
            return 1;
        }

        #region  ��������

        [Obsolete("����", true)]
        private void tab1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //�л�tabҳʱ ��ǰ����������ʾ
            //if (listBoxActive != null)
            //{
            //    this.listBoxActive.Visible = false;
            //}
            //switch (tab1.SelectedTab.Text)
            //{
            //    case "������Ϣ":
            //        //���С���� ������һ��
            //        if (this.ucTumourCard2.GetfpSpreadRowCount() == 0)
            //        {
            //            this.ucTumourCard2.AddRow();
            //            this.ucTumourCard2.SetActiveCells();
            //        }
            //        break;
            //    case "������Ϣ":
            //        //���С���� ������һ��
            //        if (this.ucOperation1.GetfpSpread1RowCount() == 0)
            //        {
            //            this.ucOperation1.AddRow();
            //            this.ucOperation1.SetActiveCells();
            //        }
            //        break;
            //    case "������Ϣ":
            //        break;
            //    case "�����Ϣ":
            //        if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
            //        {
            //            this.ucDiagNoseInput1.AddRow();
            //            this.ucDiagNoseInput1.SetActiveCells();
            //        }
            //        break;
            //    case "��Ӥ��Ϣ":
            //        if (this.ucBabyCardInput1.GetfpSpreadRowCount() == 0)
            //        {
            //            this.ucBabyCardInput1.AddRow();
            //            this.ucBabyCardInput1.SetActiveCells();
            //        }
            //        break;
            //    case "ת����Ϣ":
            //        if (this.ucChangeDept1.GetfpSpreadRowCount() == 0)
            //        {
            //            this.ucChangeDept1.AddRow();
            //            this.ucChangeDept1.SetActiveCells();
            //        }
            //        break;
            //}
        }
        /// <summary>
        ///ɾ����ǰ��
        /// </summary>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int DeleteActiveRow()
        {
            //string strName = this.tab1.SelectedTab.Text;
            //switch (strName)
            //{
            //    case "������Ϣ":
            //        this.ucOperation1.DeleteActiveRow();
            //        break;
            //    case "�����Ϣ":
            //        this.ucDiagNoseInput1.DeleteActiveRow();
            //        break;
            //    case "ת����Ϣ":
            //        this.ucChangeDept1.DeleteActiveRow();
            //        break;
            //    case "������Ϣ":
            //        this.ucTumourCard2.DeleteActiveRow();
            //        break;
            //    case "��Ӥ��Ϣ":
            //        this.ucBabyCardInput1.DeleteActiveRow();
            //        break;
            //    case "������Ϣ":
            //        MessageBox.Show("������Ϣ������ɾ��");
            //        break;
            //}
            return 1;
        }
        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        [Obsolete("����", true)]
        public void InitTreeView()
        {
            //			ArrayList al = new ArrayList();
            //			TreeNode tnParent;
            //			this.patientTreeView.HideSelection = false;
            //			Neusoft.HISFC.Management.RADT.InPatient pQuery = new Neusoft.HISFC.Management.RADT.InPatient();
            //			this.patientTreeView.BeginUpdate();
            //			this.patientTreeView.Nodes.Clear();
            //			//����ͷ
            //			tnParent = new TreeNode();
            //			tnParent.Text = "δ�Ǽǲ�������";
            //			tnParent.Tag = "%";
            //			try
            //			{
            //				tnParent.ImageIndex = 0;
            //				tnParent.SelectedImageIndex = 1;
            //			}
            //			catch{}
            //			this.patientTreeView.Nodes.Add( tnParent );
            //			
            //			//��ý���δ�Ǽǻ�����Ϣ
            //			al = pQuery.PatientsHavingCase( "I" );
            //
            //			foreach( Neusoft.HISFC.Object.RADT.PatientInfo pInfo in al )
            //			{
            //				TreeNode tnPatient = new TreeNode();
            //
            //				tnPatient.Text = pInfo.Name + "[" + pInfo.Patient.PID.PatientNO + "]";
            //				tnPatient.Tag = pInfo;
            //				try
            //				{
            //					tnPatient.ImageIndex = 2;
            //					tnPatient.SelectedImageIndex = 3;
            //				}
            //				catch{}
            //				tnParent.Nodes.Add( tnPatient );
            //			}
            //
            //			tnParent.Expand();
            //			this.patientTreeView.EndUpdate();
        }

        [Obsolete("����", true)]
        private void patientTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(Neusoft.HISFC.Object.RADT.PatientInfo))
            {
                //				this.Reset();
                //				caseBase.PatientInfo = ((Neusoft.HISFC.Object.RADT.PatientInfo)e.Node.Tag).Clone();
                //				this.ucCaseFirstPage1.Item = caseBase.Clone();
                //				ArrayList alOrg = new ArrayList();
                //				ArrayList alNew = new ArrayList();
                //				alOrg = myBaseDML.GetInhosDiagInfo( caseBase.PatientInfo.ID, "%");
                //				Neusoft.HISFC.Object.HealthRecord.Diagnose dg;
                //				for(int i = 0; i < alOrg.Count; i++)
                //				{
                //					dg = new Neusoft.HISFC.Object.HealthRecord.Diagnose();
                //					dg.DiagInfo = ((Neusoft.HISFC.Object.Case.DiagnoseBase)alOrg[i]).Clone();
                //					alNew.Add( dg );
                //				}
                //				this.ucCaseFirstPage1.AlDiag = alNew;
            }
        }
        #endregion 
    }
}
