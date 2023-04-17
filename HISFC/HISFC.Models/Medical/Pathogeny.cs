using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Medical
{
    public class Pathogeny : Neusoft.NFC.Object.NeuObject
    {
        public Pathogeny()
        {
            // TODO: �ڴ˴���ӹ��캯���߼�
        }

        #region  ����
        /// <summary>
        /// ��Դ�����Ϣid
        /// </summary>
        private string pathogenyId;
        /// <summary>
        /// Ժ�ڸ�Ⱦ����Ϣҵ�����
        /// </summary>
        private string infectionId;
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        private string inpatientNo;
        /// <summary>
        /// �걾
        /// </summary>
        private string labSample;
        /// <summary>
        /// ��ԭ������
        /// </summary>
        private string pathogenyName;
        /// <summary>
        /// ��ԭ������
        /// </summary>
        private string pathogenyKind;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool isSusceptivity;
        /// <summary>
        /// �Ƿ���ҩ
        /// </summary>
        private bool isInaction;
        /// <summary>
        /// ��ע
        /// </summary>
        private string memo;
        /// <summary>
        /// �Ǽ���
        /// </summary>
        private string operCode;
        /// <summary>
        /// �Ǽ�ʱ��
        /// </summary>
        private DateTime operDate;
        #endregion
        #region  ����
        /// <summary>
        /// ��Դ�����Ϣid
        /// </summary>
        public string PathogenyId
        {
            get
            {
                return this.pathogenyId;
            }
            set
            {
                this.pathogenyId = value;
            }
        }
        /// <summary>
        /// Ժ�ڸ�Ⱦ����Ϣҵ�����
        /// </summary>
        public string InfectionId
        {
            get
            {
                return this.infectionId;
            }
            set
            {
                this.infectionId = value;
            }
        }
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNo
        {
            get
            {
                return this.inpatientNo;
            }
            set
            {
                this.inpatientNo = value;
            }
        }
        /// <summary>
        /// �걾
        /// </summary>
        public string LabSample
        {
            get
            {
                return this.labSample;
            }
            set
            {
                this.labSample = value;
            }
        }
        /// <summary>
        /// ��ԭ������
        /// </summary>
        public string PathogenyName
        {
            get
            {
                return this.pathogenyName;
            }
            set
            {
                this.pathogenyName = value;
            }
        }
        /// <summary>
        /// ��ԭ������
        /// </summary>
        public string PathogenyKind
        {
            get
            {
                return this.pathogenyKind;
            }
            set
            {
                this.pathogenyKind = value;
            }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsSusceptivity
        {
            get
            {
                return this.isSusceptivity;
            }
            set
            {
                this.isSusceptivity = value;
            }
        }
        /// <summary>
        /// �Ƿ���ҩ
        /// </summary>
        public bool IsInaction
        {
            get
            {
                return this.isInaction;
            }
            set
            {
                this.isInaction = value;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
        }
        /// <summary>
        /// �Ǽ���
        /// </summary>
        public string OperCode
        {
            get
            {
                return this.operCode;
            }
            set
            {
                this.operCode = value;
            }
        }
        /// <summary>
        /// �Ǽ�ʱ��
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return this.operDate;
            }
            set
            {
                this.operDate = value;
            }
        }
        #endregion
        #region ����
        public new Pathogeny Clone()
        {
            Pathogeny infectionExtent = base.Clone() as Pathogeny;
            return infectionExtent;
        }
        #endregion
    }
}
