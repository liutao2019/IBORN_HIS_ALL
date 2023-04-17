using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: ���߿�����ʵ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11��22]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class PatientStore : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PatientStore( )
        {

        }

        #region ����

        /// <summary>
        /// ����:0����ȡ����� 1����ȡ�����2����ȡ�����;
        /// </summary>
        private string type = "";
        /// <summary>
        /// �������
        /// </summary>
        private decimal storeQty;
        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item( );
        /// <summary>
        /// ��Ч��
        /// </summary>
        private DateTime validTime = new DateTime( );
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject patientInfo = new FS.FrameWork.Models.NeuObject( );
        /// <summary>
        /// ���߿�����Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject inDept = new FS.FrameWork.Models.NeuObject( );
        /// <summary>
        /// �Ƿ�Ƿ�
        /// </summary>
        private bool isCharge = false;
        /// <summary>
        /// �շ���Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment feeOper = new FS.HISFC.Models.Base.OperEnvironment( );
        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment( );
        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        private string extend = "";
        #endregion


        #region  ����

        /// <summary>
        /// ����:0����ȡ����� 1����ȡ�����2����ȡ�����;
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public decimal StoreQty
        {
            get
            {
                return storeQty;
            }
            set
            {
                storeQty = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        /// <summary>
        /// ��Ч��
        /// </summary>
        public DateTime ValidTime
        {
            get
            {
                return validTime;
            }
            set
            {
                validTime = value;
            }
        }
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }
        /// <summary>
        /// ���߿�����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject InDept
        {
            get
            {
                return inDept;
            }
            set
            {
                inDept = value;
            }
        }
        /// <summary>
        /// �Ƿ�Ƿ�
        /// </summary>
        public bool IsCharge
        {
            get
            {
                return isCharge;
            }
            set
            {
                isCharge = value;
            }
        }
        /// <summary>
        /// �շ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment FeeOper
        {
            get
            {
                return feeOper;
            }
            set
            {
                feeOper = value;
            }
        }
        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }
        /// <summary>
        /// ��չ�ֶ�
        /// </summary>
        public string Extend
        {
            get
            {
                return extend;
            }
            set
            {
                extend = value;
            }
        }
        #endregion


        #region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���PatientStoreManagerʵ�� ʧ�ܷ���null</returns>
        public new PatientStore Clone( )
        {
            PatientStore store = base.Clone( ) as PatientStore;
            store.feeOper = this.feeOper.Clone( );
            store.oper = this.oper.Clone( );
            store.inDept = this.inDept.Clone( );
            store.item = this.item.Clone( );
            store.patientInfo = this.patientInfo.Clone( );

            return store;
        }

        #endregion
    }
}
