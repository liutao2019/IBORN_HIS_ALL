using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucPatientProperty : UserControl
    {
        public ucPatientProperty()
        {
            InitializeComponent();
        }

        /// <summary>
		/// ҳ�����ԣ����մ������Ļ�����Ϣ
		/// </summary>
		public FS.HISFC.Models.RADT.PatientInfo Patient
		{
			get
			{
				return this.patient;
			}
			set
			{
				this.patient = value;
				GetPatientProperty();
			}
		
		}
		private  PatientInfo patientInfo = new PatientInfo();
		private  FS.HISFC.Models.RADT.PatientInfo patient;
        private void GetPatientProperty()
		{

			if (this.Patient != null)
			{
				patientInfo.PatientNo = Patient.PID.ID;//סԺ��
				this.patientInfo.Sex = Patient.Sex.Name;//�Ա�
				this.patientInfo.Age = Patient.Age;//����
                this.patientInfo.IdenNo = Patient.IDCard;//���֤
                this.patientInfo.BedNo = Patient.PVisit.PatientLocation.Bed.Name;//����
				this.patientInfo.InDept = Patient.PVisit.PatientLocation.Dept.Name;//����
				this.patientInfo.PatientName = Patient.Name;//����
                this.patientInfo.Pact = Patient.Pact.Name;//��ͬ��λ
                this.patientInfo.Indate = Patient.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss");
				//				this.patientInfo.Paykind = Patient.PayKind.Name;
				//			
				//			this.patientInfo. = Patient.PVisit.In_State.Name;//��Ժ״̬
				this.patientInfo.TotCost = Patient.FT.TotCost.ToString("0.00");//�����ܼ�
                this.patientInfo.PrepayCost = Patient.FT.PrepayCost.ToString("0.00");//Ԥ����
				//				this.patientInfo.BalanceCost = Patient.Fee.Balance_Cost.ToString("0.00");//�ѽ�
                this.patientInfo.LeftCost = Patient.FT.LeftCost.ToString("0.00");//����
                //			this.patientInfo. = Patient.Patient.ClinicDiagnose ;//�������
                this.patientInfo.ClinicDiagnose = Patient.ClinicDiagnose;//�������// {297FED84-CB6C-41f0-86DD-51BF035C2D36}
                if (Patient.Disease.IsAlleray)//�Ƿ����
                {

                    this.patientInfo.AnaphyFlag = "��";
                }
                else
                {
                    this.patientInfo.AnaphyFlag = "��";
                }
                if (Patient.ExtendFlag1 == "1")//����
                {
                    this.patientInfo.Condition = "����";
                }
                else if (Patient.ExtendFlag1 == "2")
                {
                    this.patientInfo.Condition = "��Σ";
                }
                else
                {
                    this.patientInfo.Condition = "";
                }
                 this.patientInfo.PhoneHome= Patient.PhoneHome;
                 this.patientInfo.RelationPhone = Patient.Kin.RelationPhone;
                 this.patientInfo.KinName = Patient.Kin.Name;
				if(this.Patient.PVisit.OutTime != DateTime.MinValue)
				{
                    this.patientInfo.OutDate = Patient.PVisit.OutTime.ToShortDateString();//��Ժ����
				}
				else
				{
					this.patientInfo.OutDate = "δ��Ժ";
				}
				//				this.patientInfo.BursaryTotMedFee = Patient.Fee.BursaryTotMedFee.ToString();//����ҩ
                this.patientInfo.DayLimit = Patient.FT.DayLimitCost.ToString();//ҩ�� �����޶�
				this.patientInfo.LimitTot = Patient.FT.DayLimitTotCost.ToString();//���޶��ۼ�
			
				decimal Pub = 0;
				decimal Own = 0;
				Pub = Patient.FT.PubCost;
				Own = Patient.FT.OwnCost + Patient.FT.PayCost;
				//				this.patientInfo.PubCost = Pub.ToString("0.00");//�ܼ���
				this.patientInfo.OwnCost = Own.ToString("0.00"); 
				this.patientInfo.PayCost = Patient.FT.PayCost.ToString("0.00");
				//				this.patientInfo.Available = (Patient.Fee.Left_Cost - Patient.PVisit.MoneyAlert).ToString();
				//				this.patientInfo.Caution = Patient.Caution.Name;
                this.patientInfo.AdmittingDoctor = Patient.PVisit.AdmittingDoctor.Name;
				//				this.patientInfo.AdmittingNurse = Patient.PVisit.AdmittingNurse.Name;
                this.patientInfo.AttendingDoctor = Patient.PVisit.AttendingDoctor.Name;
                this.patientInfo.ChiefDoctor = Patient.PVisit.ConsultingDoctor.Name;//����ҽʦ
				this.patientInfo.InState = Patient.PVisit.InState.Name;
				//			this.patientInfo = Patient.Memo;
                this.patientInfo.ResponsibleDoctor = Patient.PVisit.ResponsibleDoctor.Name;//����ҽʦ

			}

			this.propertyGrid1.SelectedObject = patientInfo;
//			propertyGrid1.SelectedObjects = new object[]{patientInfo,Patient.Patient,Patient.PayKind,Patient.PVisit,Patient.SIMainInfo,Patient.Caution,Patient.Diagnoses,Patient.Disease};
		}

        private void label1_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
	}


	#region ���������
	#region ����Ҫ����PropertyGird�еĶ���Ļ���.

	class IBaseProperty : ICustomTypeDescriptor
	{
		private PropertyDescriptorCollection globalizedProps;

		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if ( globalizedProps == null) 
			{
				PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, attributes, true);

				globalizedProps = new PropertyDescriptorCollection(null);

				foreach( PropertyDescriptor oProp in baseProps )
				{
					globalizedProps.Add(new BasePropertyDescriptor (oProp));
				}
			}
			return globalizedProps;
		}

		public PropertyDescriptorCollection GetProperties()
		{
			if ( globalizedProps == null) 
			{
				PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
				globalizedProps = new PropertyDescriptorCollection(null);

				foreach( PropertyDescriptor oProp in baseProps )
				{
					globalizedProps.Add(new BasePropertyDescriptor(oProp));
				}
			}
			return globalizedProps;
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}
	}
	#endregion

	#region ����Ҫ����PropertyGird�еĶ������������д

	class BasePropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor basePropertyDescriptor; 
  
		public BasePropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
		{
			this.basePropertyDescriptor = basePropertyDescriptor;
		}

		public override bool CanResetValue(object component)
		{
			return basePropertyDescriptor.CanResetValue(component);
		}

		public override Type ComponentType
		{
			get { return basePropertyDescriptor.ComponentType; }
		}

		public override string DisplayName
		{
			get 
			{
				string svalue  = "";
				foreach(Attribute attribute in this.basePropertyDescriptor.Attributes)
				{
					if (attribute is showChinese)
					{
						svalue = attribute.ToString();
						break;
					}
				}
				if (svalue == "") return this.basePropertyDescriptor.Name;
				else return svalue;
			}
		}

		public override string Description
		{
			get
			{
				return this.basePropertyDescriptor.Description;
			}
		}

		public override object GetValue(object component)
		{
			return this.basePropertyDescriptor.GetValue(component);
		}

		public override bool IsReadOnly
		{
			get { return this.basePropertyDescriptor.IsReadOnly; }
		}

		public override string Name
		{
			get { return this.basePropertyDescriptor.Name; }
		}

		public override Type PropertyType
		{
			get { return this.basePropertyDescriptor.PropertyType; }
		}

		public override void ResetValue(object component)
		{
			this.basePropertyDescriptor.ResetValue(component);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return this.basePropertyDescriptor.ShouldSerializeValue(component);
		}

		public override void SetValue(object component, object value)
		{
			this.basePropertyDescriptor.SetValue(component, value);
		}
	}
	#endregion


	#region �Զ�������������ʾ��ıߵĺ���
	[AttributeUsage(AttributeTargets.Property)]
	class showChinese : System.Attribute
	{
		private string sChineseChar = "";

		public showChinese(string sChineseChar)
		{
			this.sChineseChar = sChineseChar;
		}

		public string ChineseChar
		{
			get
			{
				return this.sChineseChar;
			}
		}

		public override string ToString()
		{
			return this.sChineseChar;
		}
	}
	#endregion

	#endregion

	#region ����������
	/// <summary>
	/// ������ʾ��������
	/// </summary> 
    class PatientInfo : IBaseProperty
    {
        #region ���߻�����Ϣ
        private string Patientno = null; //����סԺ��
        private string Patienname = null;//��������
        //            private string  pPaykind = null;   //֧����ʽ
        private string pSex = null;//�����Ա�
        //			private string  pBirthdate = null;//��������
        private string pAge = null;//��������
        private string pIdenNo = null;//�������֤
        private string pPact = null; //��ͬ��λ
        private string pIndate = null;//סԺ����
        private string pDate_Out = null;//��Ժ����
        private string pInstate = null;//��Ժ״̬

        private string phoneHome = null; //���߼�ͥ�绰
        private string kinName = null;//������ϵ��
        private string relationPhone = null; //������ϵ�˵绰
        private string clinicDiagnose = null; //�������// {297FED84-CB6C-41f0-86DD-51BF035C2D36}

        private string anaphyFlag = null; //�Ƿ���ڹ���
        private string condition = null; //�������

        [DescriptionAttribute("����סԺ�š�"), showChinese("A.סԺ��"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string PatientNo
        {
            get { return Patientno; }
            set { Patientno = value; }
        }

        [DescriptionAttribute("����������"), showChinese("B.����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string PatientName
        {
            get { return Patienname; }
            set { Patienname = value; }
        }

        [DescriptionAttribute("�����Ա�"), showChinese("C.�Ա�"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Sex
        {
            get { return pSex; }
            set { pSex = value; }
        }

        //			[DescriptionAttribute("�������ա�"),showChinese("��������:"),CategoryAttribute("���߻�����Ϣ"),ReadOnlyAttribute(false)]
        //			public string BirthDate
        //			{
        //				get { return pBirthdate; }
        //				set { pBirthdate = value; }
        //			}
        [DescriptionAttribute("�������䡣"), showChinese("D.����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Age
        {
            get { return pAge; }
            set { pAge = value; }
        }

        [DescriptionAttribute("���֤�š�"), showChinese("E.���֤��"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string IdenNo
        {
            get { return pIdenNo; }
            set { pIdenNo = value; }
        }

        [DescriptionAttribute("��Ժ���ڡ�"), showChinese("F.��Ժ����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Indate
        {
            get { return pIndate; }
            set { pIndate = value; }
        }
        [DescriptionAttribute("������ϡ�"), showChinese("G.�������"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string ClinicDiagnose
        {
            get { return clinicDiagnose; }
            set { clinicDiagnose = value; }
        }

        [DescriptionAttribute("�Ƿ������"), showChinese("G.�Ƿ����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string AnaphyFlag
        {
            get { return anaphyFlag; }
            set { anaphyFlag = value; }
        }

        [DescriptionAttribute("���顣"), showChinese("G.����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        [DescriptionAttribute("��Ժ״̬��"), showChinese("H.״̬"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string InState
        {
            get { return pInstate; }
            set { pInstate = value; }
        }

        [DescriptionAttribute("��Ժ���ڡ�"), showChinese("I.��Ժ����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string OutDate
        {
            get { return pDate_Out; }
            set { pDate_Out = value; }
        }

        [DescriptionAttribute("���㷽ʽ��"), showChinese("���㷽ʽ:"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Pact
        {
            get { return pPact; }
            set { pPact = value; }
        }
		//			[DescriptionAttribute("֧����ʽ��"),showChinese("֧����ʽ:"),CategoryAttribute("���߻�����Ϣ"),ReadOnlyAttribute(false)]
		//			public string Paykind
		//			{
		//				get { return pPaykind; }
		//				set { pPaykind = value; }
		//			}



        [DescriptionAttribute("���߼�ͥ�绰��"), showChinese("���߼�ͥ�绰:"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string PhoneHome
        {
            get { return phoneHome; }
            set { phoneHome = value; }
        }

        [DescriptionAttribute("������ϵ�ˡ�"), showChinese("������ϵ��:"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string KinName
        {
            get { return kinName; }
            set { kinName = value; }
        }

        [DescriptionAttribute("������ϵ�˵绰��"), showChinese("������ϵ�˵绰:"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string RelationPhone
        {
            get { return relationPhone; }
            set { relationPhone = value; }
        }

		#endregion

		#region ��Ժ��Ϣ
		private string  pIndept = null;//��Ժ����
		private string  pBedno  = null;//����
        private string pAttendingDoctor = null;//����ҽʦ
        private string pAdmittingDoctor = null;//����ҽʦ��סԺҽʦ
		//			private string  pAdmittingNurse = null;//���λ�ʿ
        private string pChiefDoctor = null;//����ҽʦ
        private string pResponsibleDoctor = null;//����ҽʦ
    

		[DescriptionAttribute("��Ժ���ҡ�"),showChinese("I.��Ժ����"),CategoryAttribute("2.סԺ��Ϣ"),ReadOnlyAttribute(false)]
		public string  InDept
		{
			get { return pIndept; }
			set { pIndept = value;}
		}

		[DescriptionAttribute("���ߴ��š�"),showChinese("J.���ߴ���"),CategoryAttribute("2.סԺ��Ϣ"),ReadOnlyAttribute(false)]
		public string  BedNo
		{
			get { return pBedno; }
			set { pBedno = value;}
		}
        [DescriptionAttribute("����ҽʦ��"), showChinese("����ҽʦ:"), CategoryAttribute("2.סԺ��Ϣ"), ReadOnlyAttribute(false)]
        public string AttendingDoctor
        {
            get { return pAttendingDoctor; }
            set { pAttendingDoctor = value; }
        }
        [DescriptionAttribute("����ҽʦ��סԺҽʦ��"), showChinese("��������ҽʦ:"), CategoryAttribute("2.סԺ��Ϣ"), ReadOnlyAttribute(false)]
        public string AdmittingDoctor
        {
            get { return pAdmittingDoctor; }
            set { pAdmittingDoctor = value; }
        }
        [DescriptionAttribute("����ҽʦ��"), showChinese("����ҽʦ:"), CategoryAttribute("2.סԺ��Ϣ"), ReadOnlyAttribute(false)]
        public string ChiefDoctor
        {
            get { return pChiefDoctor; }
            set { pChiefDoctor = value; }
        }
		//			[DescriptionAttribute("���λ�ʿ��"),showChinese("���λ�ʿ:"),CategoryAttribute("סԺ��Ϣ"),ReadOnlyAttribute(false)]
		//			public string  AdmittingNurse
		//			{
		//				get { return pAdmittingNurse; }
		//				set { pAdmittingNurse = value;}
		//			}
        [DescriptionAttribute("��������ҽʦ��"), showChinese("��������ҽʦ:"), CategoryAttribute("2.סԺ��Ϣ"), ReadOnlyAttribute(false)]
        public string ResponsibleDoctor
        {
            get { return pResponsibleDoctor; }
            set { pResponsibleDoctor = value; }
        }

		#endregion

		#region ���߷�����Ϣ
		//			private string pCaution = null ;//�������
		private string pTot_Cost = null;//�ܷ���
		private string pPrepay_Cost = null ;// Ԥ����
		//            private string pBalance_Cost = null ;//�ѽ�
		private string pLeft_Cost = null ;//����
		//            private string pBursaryTotMedFee = null;//����ҩ
		private string pDay_Limit = null;//���޶�
		private string pLimitTot = null;//���޶��ۼ�
           
		//            private string pPub_Cost = null;//�ܼ���
		private string pOwn_Cost = null;//�Է�
		private string pPay_Cost = null;//�����Ը����
		//			private string pAvailable = null;//���ý��


		

		[DescriptionAttribute("Ԥ����"),showChinese("K.Ԥ����"),CategoryAttribute("3.���߷�����Ϣ"),ReadOnlyAttribute(false)]
		public string  PrepayCost
		{
			get { return pPrepay_Cost; }
			set { pPrepay_Cost = value;}
		}
		[DescriptionAttribute("�Էѡ�"),showChinese("L.�Է�"),CategoryAttribute("3.���߷�����Ϣ"),ReadOnlyAttribute(false)]
		public string  OwnCost
		{
			get { return pOwn_Cost; }
			set { pOwn_Cost = value;}
		}

		[DescriptionAttribute("�ܷ��á�"),showChinese("M.�ܷ���"),CategoryAttribute("3.���߷�����Ϣ"),ReadOnlyAttribute(false)]
		public string  TotCost
		{
			get { return pTot_Cost; }
			set { pTot_Cost = value;}
		}



		//			[DescriptionAttribute("�ѽᡣ"),showChinese("�ѽ�"),CategoryAttribute("3.���߷�����Ϣ"),ReadOnlyAttribute(false)]
		//			public string  BalanceCost
		//			{
		//				get { return pBalance_Cost; }
		//				set { pBalance_Cost = value;}
		//			}

		[DescriptionAttribute("��"),showChinese("N.���"),CategoryAttribute("3.���߷�����Ϣ"),ReadOnlyAttribute(false)]
		public string  LeftCost
		{
			get { return pLeft_Cost; }
			set { pLeft_Cost = value;}
		}

		#endregion

		#region ������Ϣ
		
		[DescriptionAttribute("���޶"),showChinese("O.���޶�"),CategoryAttribute("4.������Ϣ"),ReadOnlyAttribute(false)]
		public string  DayLimit
		{
			get { return pDay_Limit; }
			set { pDay_Limit = value;}
		}
		[DescriptionAttribute("���޶��ۼơ�"),showChinese("P.���޶��ۼ�"),CategoryAttribute("4.������Ϣ"),ReadOnlyAttribute(false)]
		public string  LimitTot
		{
			get { return pLimitTot; }
			set { pLimitTot = value;}
		}
			
		[DescriptionAttribute("�����Ը���"),showChinese("Q.�����Ը����"),CategoryAttribute("4.������Ϣ"),ReadOnlyAttribute(false)]
		public string  PayCost
		{
			get { return pPay_Cost; }
			set { pPay_Cost = value;}
		}
	
		#endregion

	 #endregion
	}	


	}
