using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// [��������: �����������̼�¼]<br></br>
    /// [�� �� ��: liangjz]<br></br>
    /// [����ʱ��: 2007-11]<br></br>
    /// </summary>
    [Serializable]
    public class Process : FS.FrameWork.Models.NeuObject
    {
        public Process()
        {

        }

        #region �����

        /// <summary>
        /// �Ƽ�������Ϣ
        /// </summary>
        private FS.HISFC.Models.Preparation.Preparation preparation = new Preparation();

        /// <summary>
        /// ����������Ŀ
        /// </summary>
        private FS.FrameWork.Models.NeuObject processItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����ִ�н��ֵ
        /// </summary>
        private decimal resultQty;

        /// <summary>
        /// ����ִ������
        /// </summary>
        private string resultStr;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��չֵ
        /// </summary>
        private string extend;

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private string itemType;

        /// <summary>
        /// �Ƿ�ϸ�
        /// </summary>
        private bool isEligibility;
        #endregion

        #region ����

        /// <summary>
        /// �Ƽ�������Ϣ
        /// </summary>
        public FS.HISFC.Models.Preparation.Preparation Preparation
        {
            get
            {
                return this.preparation;
            }
            set
            {
                this.preparation = value;
            }

        }

        /// <summary>
        /// ����������Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject ProcessItem
        {
            get
            {
                return this.processItem;
            }
            set
            {
                this.processItem = value;
            }
        }

        /// <summary>
        /// ����ִ�н��ֵ
        /// </summary>
        public decimal ResultQty
        {
            get
            {
                return this.resultQty;
            }
            set
            {
                this.resultQty = value;
            }
        }

        /// <summary>
        /// ����ִ������
        /// </summary>
        public string ResultStr
        {
            get
            {
                return this.resultStr;
            }
            set
            {
                this.resultStr = value;
            }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
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
        /// ��չֵ
        /// </summary>
        public string Extend
        {
            get
            {
                return this.extend;
            }
            set
            {
                this.extend = value;
            }
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public string ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        /// <summary>
        /// �Ƿ�ϸ�
        /// </summary>
        public bool IsEligibility
        {
            get
            {
                return this.isEligibility;
            }
            set
            {
                this.isEligibility = value;
            }
        }

        #endregion

        #region ����

        public new Process Clone()
        {
            Process p = base.Clone() as Process;

            p.preparation = this.preparation.Clone();
            p.processItem = this.processItem.Clone();
            p.oper = this.oper.Clone();

            return p;
        }

        #endregion
    }
}
