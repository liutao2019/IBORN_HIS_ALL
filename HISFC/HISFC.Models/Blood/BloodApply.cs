using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.Blood
{
    /// <summary>
    /// [��������: ��Ѫ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-06-05]<br></br>
    /// <�޸�>
    ///		<�޸���></�޸���>
    ///		<�޸�ʱ��></�޸�ʱ��>
    ///		<�޸�˵��></�޸�˵��>
    /// </�޸�>
    /// <˵��>
    ///		1 ID ���뵥��
    /// </˵��>
    /// </summary>
    /// 
    [System.Serializable]
    public class BloodApply : StoreBase
    {
        public BloodApply()
        {

        }


        #region �����

        /// <summary>
        /// ״̬
        /// </summary>
        EnumBloodState state = EnumBloodState.Apply;

        /// <summary>
        /// ������� 1 סԺ 2 ����
        /// </summary>
        private Neusoft.HISFC.Models.Base.ServiceTypes patientType = Neusoft.HISFC.Models.Base.ServiceTypes.I;

        /// <summary>
        /// סԺ�š����ﲡ����
        /// </summary>
        private string cardNO;

        /// <summary>
        /// סԺ��ˮ�š�������ˮ��
        /// </summary>
        private string patientID;

        /// <summary>
        /// ����
        /// </summary>
        private string patientName;

        /// <summary>
        /// �Ա�
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject sex = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private string age;

        /// <summary>
        /// ���߿���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���߻���վ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject nurseCell = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        private string bedNO;

        /// <summary>
        /// ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject diagnose = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ѪĿ��
        /// </summary>
        private string bloodAim;

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        private string quality;

        /// <summary>
        /// ��Ѫ����Դ����
        /// </summary>
        private string inSource;

        /// <summary>
        /// Ԥ����Ѫ����
        /// </summary>
        private DateTime orderBloodDate;

        /// <summary>
        /// ����Ѫ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.EnumBloodKind applyBloodKind = Neusoft.HISFC.Models.Base.EnumBloodKind.U;

        /// <summary>
        /// ����ѪҺ�ɷ�
        /// </summary>
        private Neusoft.HISFC.Models.Blood.BloodComponents applyComponent = new BloodComponents();

        /// <summary>
        /// ����RH
        /// </summary>
        private Neusoft.HISFC.Models.Base.EnumTestResult applyRH = Neusoft.HISFC.Models.Base.EnumTestResult.����;

        /// <summary>
        /// ������
        /// </summary>
        private decimal applyQty;

        /// <summary>
        /// ��λ
        /// </summary>
        private string baseUnit;

        /// <summary>
        /// ����Ѫ�ͼ�����Ϣ
        /// </summary>
        private BloodTest patientTest = new BloodTest();

        /// <summary>
        /// ��Ѫ�Ƿ�����
        /// </summary>
        private bool isMatch;

        /// <summary> 
        /// ����ҽʦǩ��,��������
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment operEnvironment = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary> 
        /// ����ҽʦǩ��
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject chargeDoc = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment cancelOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ״̬
        /// </summary>
        public EnumBloodState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }


        /// <summary>
        /// ������� 1 סԺ 2 ����
        /// </summary>
        public Neusoft.HISFC.Models.Base.ServiceTypes PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }


        /// <summary>
        /// סԺ�š����ﲡ����
        /// </summary>
        public string CardNO
        {
            get
            {
                return this.cardNO;
            }
            set
            {
                this.cardNO = value;
            }
        }


        /// <summary>
        /// סԺ��ˮ�š�������ˮ��
        /// </summary>
        public string PatientID
        {
            get
            {
                return this.patientID;
            }
            set
            {
                this.patientID = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public string PatientName
        {
            get
            {
                return this.patientName;
            }
            set
            {
                this.patientName = value;
            }
        }


        /// <summary>
        /// �Ա�
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public string Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }


        /// <summary>
        /// ���߿���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }


        /// <summary>
        /// ���߻���վ
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject NurseCell
        {
            get
            {
                return this.nurseCell;
            }
            set
            {
                this.nurseCell = value;
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        public string BedNO
        {
            get
            {
                return this.bedNO;
            }
            set
            {
                this.bedNO = value;
            }
        }


        /// <summary>
        /// ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Diagnose
        {
            get
            {
                return this.diagnose;
            }
            set
            {
                this.diagnose = value;
            }
        }


        /// <summary>
        /// ��ѪĿ��
        /// </summary>
        public string BloodAim
        {
            get
            {
                return this.bloodAim;
            }
            set
            {
                this.bloodAim = value;
            }
        }


        /// <summary>
        /// ��Ѫ����
        /// </summary>
        public string Quality
        {
            get
            {
                return this.quality;
            }
            set
            {
                this.quality = value;
            }
        }


        /// <summary>
        /// ��Ѫ����Դ����
        /// </summary>
        public string PatientSource
        {
            get
            {
                return this.inSource;
            }
            set
            {
                this.inSource = value;
            }
        }


        /// <summary>
        /// Ԥ����Ѫ����
        /// </summary>
        public DateTime OrderBloodDate
        {
            get
            {
                return this.orderBloodDate;
            }
            set
            {
                this.orderBloodDate = value;
            }
        }


        /// <summary>
        /// ����Ѫ��
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumBloodKind ApplyBloodKind
        {
            get
            {
                return this.applyBloodKind;
            }
            set
            {
                this.applyBloodKind = value;
            }
        }


        /// <summary>
        /// ����ѪҺ�ɷ�
        /// </summary>
        public Neusoft.HISFC.Models.Blood.BloodComponents ApplyComponent
        {
            get
            {
                return this.applyComponent;
            }
            set
            {
                this.applyComponent = value;
            }
        }



        /// <summary>
        /// ����ѪҺRH
        /// </summary>
        public Neusoft.HISFC.Models.Base.EnumTestResult ApplyRH
        {
            get
            {
                return this.applyRH;
            }
            set
            {
                this.applyRH = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        public decimal ApplyQty
        {
            get
            {
                return this.applyQty;
            }
            set
            {
                this.applyQty = value;
            }
        }


        /// <summary>
        /// ��λ
        /// </summary>
        public string BaseUnit
        {
            get
            {
                return this.baseUnit;
            }
            set
            {
                this.baseUnit = value;
            }
        }


        /// <summary>
        /// ����Ѫ�ͼ�����Ϣ
        /// </summary>
        public BloodTest PatientTest
        {
            get
            {
                return this.patientTest;
            }
            set
            {
                this.patientTest = value;
            }
        }

        /// <summary>
        /// ��Ѫ�Ƿ�����
        /// </summary>
        public bool IsMatch
        {
            get
            {
                return this.isMatch;
            }
            set
            {
                this.isMatch = value;
            }
        }


        /// <summary> 
        /// ����ҽʦǩ��,��������
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ApplyDoc
        {
            get
            {
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }


        /// <summary> 
        /// ����ҽʦǩ��
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject ChargeDoc
        {
            get
            {
                return this.chargeDoc;
            }
            set
            {
                this.chargeDoc = value;
            }
        }


        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment CancelOper
        {
            get
            {
                return this.cancelOper;
            }
            set
            {
                this.cancelOper = value;
            }
        }


        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }


        #endregion

        public new BloodApply Clone()
        {
            BloodApply bloodApply = base.Clone() as BloodApply;

            bloodApply.sex = this.sex.Clone();
            bloodApply.dept = this.dept.Clone();
            bloodApply.nurseCell = this.nurseCell.Clone();
            bloodApply.diagnose = this.diagnose.Clone();
            bloodApply.applyComponent = this.applyComponent.Clone();
            bloodApply.operEnvironment = this.operEnvironment.Clone();
            bloodApply.chargeDoc = this.chargeDoc.Clone();
            bloodApply.cancelOper = this.cancelOper.Clone();

            bloodApply.oper = this.oper.Clone();

            return bloodApply;

        }

    }
}
