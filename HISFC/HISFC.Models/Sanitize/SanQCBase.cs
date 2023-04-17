using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ��������ά����]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanQCBase : Neusoft.NFC.Object.NeuObject
    {
        public SanQCBase()
        {

        }

        #region ����

        /// <summary>
        /// ��Ŀ��ˮ��
        /// </summary>
        private string thingInfo = string.Empty;

        /// <summary>
        /// ��������01����
        /// </summary>
        private QCTypes qcType = QCTypes.����;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string itemName=string.Empty;

        /// <summary>
        /// ��Ŀ˳���
        /// </summary>
        private decimal sortCode = 0;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private QCItemTypes qcItemType = QCItemTypes.�ַ���;

        /// <summary>
        /// ��Ŀ��ʽ
        /// </summary>
        private string itemFormat = string.Empty;

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        private string itemUnit = string.Empty;

        /// <summary>
        /// �Ƿ��ӡ��ǩ1��0��
        /// </summary>
        private bool labelFlag = true;

        /// <summary>
        /// �Ƿ��豸�ӿ�1��0��
        /// </summary>
        private bool equFlag = true;

        /// <summary>
        /// �豸�ӿ���Ϣ
        /// </summary>
        private string equInfo = string.Empty;

        /// <summary>
        /// �Ƿ�ͳ��1��0��
        /// </summary>
        private bool statFlag = true;

        /// <summary>
        /// �Ƿ�ϼ�1��0��
        /// </summary>
        private bool sumFlag = true;

        /// <summary>
        /// ������Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ��Ŀ��ˮ��
        /// </summary>
        public string ThingInfo
        {
            get
            {
                return this.thingInfo;
            }
            set
            {
                this.thingInfo = value;
            }
        }

        /// <summary>
        /// ��������01����
        /// </summary>
        public QCTypes QcType
        {
            get
            {
                return this.qcType;
            }
            set
            {
                this.qcType = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        /// <summary>
        /// ��Ŀ˳���
        /// </summary>
        public decimal SortCode
        {
            get
            {
                return this.sortCode;
            }
            set
            {
                this.sortCode = value;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public QCItemTypes QCItemType
        {
            get
            {
                return this.qcItemType;
            }
            set
            {
                this.qcItemType = value;
            }
        }

        /// <summary>
        /// ��Ŀ��ʽ
        /// </summary>
        public string ItemFormat
        {
            get
            {
                return this.itemFormat;
            }
            set
            {
                this.itemFormat = value;
            }
        }

        /// <summary>
        /// ��Ŀ��λ
        /// </summary>
        public string ItemUnit
        {
            get
            {
                return this.itemUnit;
            }
            set
            {
                this.itemUnit = value;
            }
        }

        /// <summary>
        /// �Ƿ��ӡ��ǩ1��0��
        /// </summary>
        public bool LabelFlag
        {
            get
            {
                return this.labelFlag;
            }
            set
            {
                this.labelFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ��豸�ӿ�1��0��
        /// </summary>
        public bool EquFlag
        {
            get
            {
                return this.equFlag;
            }
            set
            {
                this.equFlag = value;
            }
        }

        /// <summary>
        /// �豸�ӿ���Ϣ
        /// </summary>
        public string EquInfo
        {
            get
            {
                return this.equInfo;
            }
            set
            {
                this.equInfo = value;
            }
        }

        /// <summary>
        /// �Ƿ�ͳ��1��0��
        /// </summary>
        public bool StatFlag
        {
            get
            {
                return this.statFlag;
            }
            set
            {
                this.statFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ�ϼ�1��0��
        /// </summary>
        public bool SumFlag
        {
            get
            {
                return this.sumFlag;
            }
            set
            {
                this.sumFlag = value;
            }
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment Oper
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
        
        #region ����

        #region ��¡

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new SanQCBase Clone()
        {
            SanQCBase sanQCBase = base.Clone() as SanQCBase;

            sanQCBase.Oper = this.Oper.Clone();

            return sanQCBase;
        }

        #endregion

        #endregion


    }
}
