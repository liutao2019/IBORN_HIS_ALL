using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Account
{
    [Serializable]
    public class CardVolume : NeuObject, IValidState
    {

        #region ����

        /// <summary>
        /// ʹ�øÿ���Ļ��߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        /// <summary>
        /// �����
        /// </summary>
        private string volumeNo = string.Empty;

        /// <summary>
        /// ������
        /// </summary>
        private decimal money = 0;

        /// <summary>
        /// ����ʼʱ��
        /// </summary>
        private DateTime begTime = new DateTime();


        /// <summary>
        /// �����ֹʱ��
        /// </summary>
        private DateTime endTime = new DateTime();

        /// <summary>
        /// �������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ���㣻// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        private EnumPayTypesService useType = new EnumPayTypesService();

        /// <summary>
        /// ��Ӧ���ѷ�Ʊ��
        /// </summary>
        private string invoiceNo = string.Empty;

        /// <summary>
        /// ��ע
        /// </summary>
        private string mark = string.Empty;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private EnumValidState validState = EnumValidState.Valid;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment operEnvironment;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment createEnvironment;
   


        #endregion

        #region ����

        /// <summary>
        /// ʹ�øÿ���Ļ��߻�����Ϣ
        /// </summary>
        public HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string VolumeNo
        {
            get { return volumeNo; }
            set { volumeNo = value; }
        }
        
        /// <summary>
        /// ������
        /// </summary>
        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
            }
        }


        /// <summary>
        /// ����ʼʱ��
        /// </summary>
        public DateTime BegTime
        {
            get { return begTime; }
            set { begTime = value; }
        }

        /// <summary>
        /// �����ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
         /// <summary>
        /// �������ͣ�P�����ײͣ�R����Һţ�C�������ѣ�IסԺ���㣻// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        public EnumPayTypesService UseType
        {
            get
            {
                return useType;
            }
            set
            {
                useType = value;
            }
        }

        /// <summary>
        /// ��Ӧ���ѵķ�Ʊ��
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        /// <summary>
        /// ����״̬ 0��Ч 1��Ч
        /// </summary>
        public EnumValidState ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public OperEnvironment CreateEnvironment
        {
            get
            {
                if (createEnvironment == null)
                {
                    createEnvironment = new OperEnvironment();
                }
                return this.createEnvironment;
            }
            set
            {
                this.createEnvironment = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public OperEnvironment OperEnvironment
        {
            get
            {
                if (operEnvironment == null)
                {
                    operEnvironment = new OperEnvironment();
                }
                return this.operEnvironment;
            }
            set
            {
                this.operEnvironment = value;
            }
        }

        #endregion


        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new CardVolume Clone()
        {
            CardVolume cardVolume = base.Clone() as CardVolume;
            cardVolume.patient = this.Patient.Clone();

            return cardVolume;
        }
        #endregion
    }
}
