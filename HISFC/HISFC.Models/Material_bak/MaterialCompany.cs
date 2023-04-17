using System;

namespace Neusoft.HISFC.Object.Material
{
    /// <summary>
    /// [��������: ������˾����������ʵ��]
    /// [�� �� ��: ��־��]
    /// [����ʱ��: 2007-11-26].
    /// </summary>
    public class MaterialCompany : Neusoft.HISFC.Object.Base.Spell
    {
        public MaterialCompany()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ����

        /// <summary>
        /// ��˾���
        /// </summary>
        private string kind = string.Empty;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string coporation = string.Empty;

        /// <summary>
        /// ��˾��ַ
        /// </summary>
        private string address = string.Empty;

        /// <summary>
        /// ��˾�绰
        /// </summary>
        private string telCode = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private string faxCode = string.Empty;

        /// <summary>
        /// ��ַ
        /// </summary>
        private string netAddress = string.Empty;

        /// <summary>
        /// ����
        /// </summary>
        private string eMail = string.Empty;

        /// <summary>
        /// ��ϵ��
        /// </summary>
        private string linkMan = string.Empty;

        /// <summary>
        /// ��ϵ������
        /// </summary>
        private string linkMail = string.Empty;

        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        private string linkTel = string.Empty;

        /// <summary>
        /// Gmp��Ϣ
        /// </summary>
        private string gMPInfo = string.Empty;

        /// <summary>
        /// Gsp��Ϣ
        /// </summary>
        private string gSPInfo = string.Empty;

        /// <summary>
        /// ISO��Ϣ
        /// </summary>
        private string iSOInfo = string.Empty;

        /// <summary>
        /// ��˾����
        /// </summary>
        private string type = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private string openBank = string.Empty;

        /// <summary>
        /// �����ʺ�
        /// </summary>
        private string openAccounts = string.Empty;

        /// <summary>
        /// ���߿���
        /// </summary>
        private System.Decimal actualRate;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject oper = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime operTime;

        /// <summary>
        /// ��չ�ֶ�1
        /// </summary>
        private string extend1 = string.Empty;

        /// <summary>
        /// ��չ�ֶ�2
        /// </summary>
        private string extend2 = string.Empty;

        /// <summary>
        /// Ӫҵִ�յ���ʱ��
        /// </summary>
        private DateTime businessDate;
        /// <summary>
        /// ��Ӫ���֤��Ч��
        /// </summary>
        private DateTime manageDate;
        /// <summary>
        /// ˰��Ǽ�֤��Ч��
        /// </summary>
        private DateTime dutyDate;
        /// <summary>
        /// ��֯��������֤��Ч��
        /// </summary>
        private DateTime orgDate;


        #endregion

        #region ����

        /// <summary>
        /// ��˾���0-ҩ��ʹ�ã�1-���ʳ���ʹ��
        /// </summary>
        public new string Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        public new string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }


        /// <summary>
        /// ��дName
        /// </summary>
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }


        /// <summary>
        /// ��ҵ����
        /// </summary>
        public string Coporation
        {
            get
            {
                return this.coporation;
            }
            set
            {
                this.coporation = value;
            }
        }

        /// <summary>
        /// ��˾��ַ
        /// </summary>
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        /// <summary>
        /// ��˾�绰
        /// </summary>
        public string TelCode
        {
            get
            {
                return this.telCode;
            }
            set
            {
                this.telCode = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string FaxCode
        {
            get
            {
                return this.faxCode;
            }
            set
            {
                this.faxCode = value;
            }
        }

        /// <summary>
        /// ��˾��ַ
        /// </summary>
        public string NetAddress
        {
            get
            {
                return this.netAddress;
            }
            set
            {
                this.netAddress = value;
            }
        }

        /// <summary>
        /// ��˾����
        /// </summary>
        public string EMail
        {
            get
            {
                return this.eMail;
            }
            set
            {
                this.eMail = value;
            }
        }

        /// <summary>
        /// ��ϵ��
        /// </summary>
        public string LinkMan
        {
            get
            {
                return this.linkMan;
            }
            set
            {
                this.linkMan = value;
            }
        }

        /// <summary>
        /// ��ϵ������
        /// </summary>
        public string LinkMail
        {
            get
            {
                return this.linkMail;
            }
            set
            {
                this.linkMail = value;
            }
        }

        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        public string LinkTel
        {
            get
            {
                return this.linkTel;
            }
            set
            {
                this.linkTel = value;
            }
        }

        /// <summary>
        /// GMP��Ϣ
        /// </summary>
        public string GMPInfo
        {
            get
            {
                return this.gMPInfo;
            }
            set
            {
                this.gMPInfo = value;
            }
        }

        /// <summary>
        /// GSP��Ϣ
        /// </summary>
        public string GSPInfo
        {
            get
            {
                return this.gSPInfo;
            }
            set
            {
                this.gSPInfo = value;
            }
        }

        /// <summary>
        /// ISO��Ϣ
        /// </summary>
        public string ISOInfo
        {
            get
            {
                return this.iSOInfo;
            }
            set
            {
                this.iSOInfo = value;
            }
        }

        /// <summary>
        /// ƴ����
        /// </summary>
        public string SpCode
        {
            get
            {
                return base.SpellCode;
            }
            set
            {
                base.SpellCode = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string WbCode
        {
            get
            {
                return base.WBCode;
            }
            set
            {
                base.WBCode = value;
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        public string CustCode
        {
            get
            {
                return base.UserCode;
            }
            set
            {
                base.UserCode = value;
            }
        }

        /// <summary>
        /// ��˾���0���������ң�1��������
        /// </summary>
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string OpenBank
        {
            get
            {
                return this.openBank;
            }
            set
            {
                this.openBank = value;
            }
        }

        /// <summary>
        /// �����˺�
        /// </summary>
        public string OpenAccounts
        {
            get
            {
                return this.openAccounts;
            }
            set
            {
                this.openAccounts = value;
            }
        }

        /// <summary>
        /// ���߿���
        /// </summary>
        public System.Decimal ActualRate
        {
            get
            {
                return this.actualRate;
            }
            set
            {
                this.actualRate = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public Neusoft.NFC.Object.NeuObject Oper
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

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime OperTime
        {
            get
            {
                return this.operTime;
            }
            set
            {
                this.operTime = value;
            }
        }


        /// <summary>
        /// Ԥ���ֶ�1
        /// </summary>
        public string Extend1
        {
            get
            {
                return this.extend1;
            }
            set
            {
                this.extend1 = value;
            }
        }

        /// <summary>
        /// Ԥ���ֶ�2
        /// </summary>
        public string Extend2
        {
            get
            {
                return this.extend2;
            }
            set
            {
                this.extend2 = value;
            }
        }

        /// <summary>
        /// Ӫҵִ�յ���ʱ��
        /// </summary>
        public DateTime BusinessDate
        {
            get
            {
                return this.businessDate;
            }
            set
            {
                this.businessDate = value;
            }
        }

        /// <summary>
        /// ��Ӫ���֤����ʱ��
        /// </summary>
        public DateTime ManageDate
        {
            get
            {
                return this.manageDate;
            }
            set
            {
                this.manageDate = value;
            }
        }

        /// <summary>
        /// ˰��Ǽ�֤����ʱ��
        /// </summary>
        public DateTime DutyDate
        {
            get
            {
                return this.dutyDate;
            }
            set
            {
                this.dutyDate = value;
            }
        }

        /// <summary>
        /// ��֯��������֤����ʱ��
        /// </summary>
        public DateTime OrgDate
        {
            get
            {
                return this.orgDate;
            }
            set
            {
                this.orgDate = value;
            }
        }

        #endregion


        #region ����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ص�ǰʵ���Ŀ�¡ʵ��</returns>
        public new MaterialCompany Clone()
        {
            MaterialCompany company = base.Clone() as MaterialCompany;

            company.Oper = this.Oper.Clone();

            return company;
        }


        #endregion

        #region �ӿ�ʵ��

        ///// <summary>
        ///// �Ƿ���Ч true��Ч[1] false��Ч[0]
        ///// </summary>
        //public bool IsValid
        //{
        //    get
        //    {
        //        if (this.state.Equals("active"))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            this.state = "active";
        //        }
        //        else
        //        {
        //            this.state = "terminated";
        //        }
        //    }
        //}

        #endregion
    }
}
