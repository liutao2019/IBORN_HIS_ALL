using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucPatientPropertyForClinic : UserControl
    {
        public ucPatientPropertyForClinic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҳ�����ԣ����մ������Ļ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.myPatientInfo;
            }
            set
            {
                this.myPatientInfo = value;
                ShowPatientProperty();
            }
        }

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register myPatientInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ��ʾ�õĻ�����Ϣ��
        /// </summary>
        private PatientInfoForClinic patientInfo = new PatientInfoForClinic();

       // {1C00C7EC-4B3D-4eaa-958D-A4DE50E9B0BD}
        /// <summary>
        /// ���תҵ��㣬��ѯ��ϵ�˺���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();


        /// <summary>
        /// ��ʾ����
        /// </summary>
        private void ShowPatientProperty()
        {
            if (this.PatientInfo != null)
            {
                FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                this.patientInfo.PatientNo = myPatientInfo.PID.CardNO;//���ﲡ����
                this.patientInfo.Sex = myPatientInfo.Sex.Name;//�Ա�
                this.patientInfo.Age = orderManager.GetAge(myPatientInfo.Birthday);//����
                this.patientInfo.RegDoct = myPatientInfo.DoctorInfo.Templet.Doct.Name;
                this.patientInfo.RegDept = myPatientInfo.DoctorInfo.Templet.Dept.Name;//����
                this.patientInfo.PatientName = myPatientInfo.Name;//����
                this.patientInfo.RegLevel = myPatientInfo.DoctorInfo.Templet.RegLevel.Name;
                this.patientInfo.RegDate = myPatientInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                this.patientInfo.PactName = myPatientInfo.Pact.Name;

                //��ͥ��ַΪ��ʱ��ʾ��λ��ַ
                this.patientInfo.Address = !string.IsNullOrEmpty(myPatientInfo.AddressHome) ? myPatientInfo.AddressHome : myPatientInfo.AddressBusiness;
                this.patientInfo.IdenNo = myPatientInfo.IDCard;

                //��ͥ�绰Ϊ��ʱ��ʾ��λ�绰
                this.patientInfo.RelaPhone = !string.IsNullOrEmpty(myPatientInfo.PhoneHome) ? myPatientInfo.PhoneHome : myPatientInfo.PhoneBusiness;

                //{6DD0C1B6-3E4F-4d8e-820F-B661625343E6} ��ϵ�˵绰

                FS.HISFC.Models.RADT.PatientInfo pin = radtIntegrate.QueryComPatientInfo(myPatientInfo.PID.CardNO);
             this.patientInfo.LinManTel =pin.Kin.RelationPhone;   

                this.patientInfo.SeeDate = myPatientInfo.SeeDoct.OperTime;


                if (!string.IsNullOrEmpty(myPatientInfo.SeeDoct.Dept.ID))
                {
                    this.patientInfo.SeeDept = interMgr.GetDepartment(myPatientInfo.SeeDoct.Dept.ID).Name;
                }
                if (!string.IsNullOrEmpty(myPatientInfo.SeeDoct.ID))
                {
                    this.patientInfo.SeeDoct = interMgr.GetEmployeeInfo(myPatientInfo.SeeDoct.ID).Name;
                }
            }

            this.propertyGrid1.SelectedObject = patientInfo;
        }
    }


    #region ���������

    #region ����Ҫ����PropertyGird�еĶ���Ļ���.

    /// <summary>
    /// ����Ҫ����PropertyGird�еĶ���Ļ���.
    /// </summary>
    class IBasePropertyForClinic : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection globalizedProps;

        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        /// <summary>
        /// �������ֵ
        /// </summary>
        /// <returns></returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
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
            if (globalizedProps == null)
            {
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, attributes, true);

                globalizedProps = new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor oProp in baseProps)
                {
                    globalizedProps.Add(new BasePropertyDescriptorForClinic(oProp));
                }
            }
            return globalizedProps;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            if (globalizedProps == null)
            {
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
                globalizedProps = new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor oProp in baseProps)
                {
                    globalizedProps.Add(new BasePropertyDescriptorForClinic(oProp));
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

    /// <summary>
    /// Ҫ����PropertyGird�еĶ������������д
    /// </summary>
    class BasePropertyDescriptorForClinic : PropertyDescriptor
    {
        private PropertyDescriptor basePropertyDescriptor;

        public BasePropertyDescriptorForClinic(PropertyDescriptor basePropertyDescriptor)
            : base(basePropertyDescriptor)
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
                string svalue = "";
                foreach (Attribute attribute in this.basePropertyDescriptor.Attributes)
                {
                    if (attribute is showChineseForClinic)
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

    /// <summary>
    /// �Զ�������������ʾ��ıߵĺ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class showChineseForClinic : System.Attribute
    {
        private string sChineseChar = "";

        public showChineseForClinic(string sChineseChar)
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
    class PatientInfoForClinic : IBasePropertyForClinic
    {
        #region ����

        /// <summary>
        /// �������ﲡ����
        /// </summary>
        private string Patientno = null;

        /// <summary>
        /// ��������
        /// </summary>
        private string Patienname = null;

        /// <summary>
        /// �����Ա�
        /// </summary>
        private string pSex = null;

        /// <summary>
        /// ��������
        /// </summary>
        private string pAge = null;

        /// <summary>
        /// �Һſ���
        /// </summary>
        private string regDept = null;

        /// <summary>
        /// �Һ�ҽ��
        /// </summary>
        private string regDoct = null;

        /// <summary>
        /// �Һż���
        /// </summary>
        private string regLevel = null;

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        private string pactName = null;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime seeDate;

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private string seeDoct = null;

        /// <summary>
        /// �������
        /// </summary>
        private string seeDept = null;

        /// <summary>
        /// �Һ�ʱ��
        /// </summary>
        private string regDate = null;

        /// <summary>
        /// ���֤��
        /// </summary>
        private string idenNo = null;

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        private string relaPhone = null;


      //  {6DD0C1B6-3E4F-4d8e-820F-B661625343E6}//
        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        private string linkManTel = null;


        /// <summary>
        /// ��ַ
        /// </summary>
        private string address = null;

        #endregion

        #region ������Ϣ

        /// <summary>
        /// �������ﲡ����
        /// </summary>
        [DescriptionAttribute("�������ﲡ���š�"), showChineseForClinic("A.���ﲡ����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string PatientNo
        {
            get { return Patientno; }
            set { Patientno = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [DescriptionAttribute("����������"), showChineseForClinic("B.����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string PatientName
        {
            get { return Patienname; }
            set { Patienname = value; }
        }

        /// <summary>
        /// �����Ա�
        /// </summary>
        [DescriptionAttribute("�����Ա�"), showChineseForClinic("C.�Ա�"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Sex
        {
            get { return pSex; }
            set { pSex = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [DescriptionAttribute("�������䡣"), showChineseForClinic("D.����"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Age
        {
            get
            {
                return pAge;
            }
            set
            {
                pAge = value;
            }
        }

        /// <summary>
        /// ���֤��
        /// </summary>
        [DescriptionAttribute("���֤�š�"), showChineseForClinic("E.���֤��"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string IdenNo
        {
            get { return idenNo; }
            set { idenNo = value; }
        }

        /// <summary>
        /// ��ַ
        /// </summary>
        [DescriptionAttribute("��ϵ��ַ��"), showChineseForClinic("F.��ַ"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        [DescriptionAttribute("��ϵ�绰��"), showChineseForClinic("G.��ϵ�绰"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string RelaPhone
        {
            get { return relaPhone; }
            set { relaPhone = value; }
        }
        #endregion

        #region �Һ���Ϣ

        /// <summary>
        /// �Һ�ʱ��
        /// </summary>
        [DescriptionAttribute("�Һ�ʱ�䡣"), showChineseForClinic("H.�Һ�ʱ��"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string RegDate
        {
            get
            {
                return regDate;
            }
            set
            {
                regDate = value;
            }
        }

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        [DescriptionAttribute("��ͬ��λ��"), showChineseForClinic("I.��ͬ��λ"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string PactName
        {
            get { return pactName; }
            set { pactName = value; }
        }

        /// <summary>
        /// �Һż���
        /// </summary>
        [DescriptionAttribute("�Һż���"), showChineseForClinic("J.�Һż���"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string RegLevel
        {
            get { return regLevel; }
            set { regLevel = value; }
        }

        /// <summary>
        /// �Һſ���
        /// </summary>
        [DescriptionAttribute("�Һſ��ҡ�"), showChineseForClinic("K.�Һſ���"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string RegDept
        {
            get { return regDept; }
            set { regDept = value; }
        }

        /// <summary>
        /// �Һ�ҽ��
        /// </summary>
        [DescriptionAttribute("�Һ�ҽ����"), showChineseForClinic("L.�Һ�ҽ��"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string RegDoct
        {
            get { return regDoct; }
            set { regDoct = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DescriptionAttribute("����ʱ�䡣"), showChineseForClinic("M.����ʱ��"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public DateTime SeeDate
        {
            get { return seeDate; }
            set { seeDate = value; }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        [DescriptionAttribute("����ҽ����"), showChineseForClinic("N.����ҽ��"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string SeeDoct
        {
            get { return seeDoct; }
            set { seeDoct = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        [DescriptionAttribute("������ҡ�"), showChineseForClinic("O.�������"), CategoryAttribute("2.�Һ���Ϣ"), ReadOnlyAttribute(false)]
        public string SeeDept
        {
            get { return seeDept; }
            set { seeDept = value; }
        }

       // {6DD0C1B6-3E4F-4d8e-820F-B661625343E6}
        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        [DescriptionAttribute("��ϵ�˵绰��"), showChineseForClinic("P.��ϵ�˵绰"), CategoryAttribute("1.���߻�����Ϣ"), ReadOnlyAttribute(false)]
        public string LinManTel
        {
            get { return linkManTel; }
            set { linkManTel = value; }
        }
       
        #endregion
    }
    #endregion
}
