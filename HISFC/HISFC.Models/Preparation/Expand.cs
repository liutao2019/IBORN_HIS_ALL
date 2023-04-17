using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{
    /// <summary>
    /// Prescription<br></br>
    /// [��������: �Ƽ���������ͳ����Ϣ]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Expand : FS.FrameWork.Models.NeuObject
    {
        #region ����

        /// <summary>
        /// �Ƽ������ƻ���
        /// </summary>
        private string planNO;

        /// <summary>
        /// �Ƽ����ƴ�����Ϣ
        /// </summary>
        private FS.HISFC.Models.Preparation.Prescription prescription = new Prescription();

        /// <summary>
        /// �ƻ���Һ��
        /// </summary>
        private decimal planQty;

        /// <summary>
        /// ����������
        /// </summary>
        private decimal planExpand;

        /// <summary>
        /// �����
        /// </summary>
        private decimal storeQty;

        /// <summary>
        /// ʵ��������
        /// </summary>
        private decimal factualExpand;

        /// <summary>
        /// �Ƿ���ִ�п��۳�
        /// </summary>
        private bool isExecOutput = false;

        /// <summary>
        /// ԭ�ϳ�����ˮ�� Ŀǰ�����ڹ�Ӧ��ԭ�����ĳ�����ˮ��
        /// </summary>
        private string materialOutNO;
        #endregion

        #region ����

        /// <summary>
        /// �Ƽ������ƻ���
        /// </summary>
        public string PlanNO
        {
            get
            {
                return this.planNO;
            }
            set
            {
                this.planNO = value;
            }
        }

        /// <summary>
        /// �Ƽ����ƴ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Preparation.Prescription Prescription
        {
            get
            {
                return this.prescription;
            }
            set
            {
                this.prescription = value;
            }
        }

        /// <summary>
        /// �ƻ���Һ��
        /// </summary>
        public decimal PlanQty
        {
            get
            {
                return this.planQty;
            }
            set
            {
                this.planQty = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public decimal PlanExpand
        {
            get
            {
                return this.planExpand;
            }
            set
            {
                this.planExpand = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public decimal StoreQty
        {
            get
            {
                return this.storeQty;
            }
            set
            {
                this.storeQty = value;
            }
        }

        /// <summary>
        /// ʵ��������
        /// </summary>
        public decimal FacutalExpand
        {
            get
            {
                return this.factualExpand;
            }
            set
            {
                this.factualExpand = value;
            }
        }

        /// <summary>
        /// �Ƿ���ִ�п��۳�
        /// </summary>
        public bool ExecOutput
        {
            get
            {
                return this.isExecOutput;
            }
            set
            {
                this.isExecOutput = value;
            }
        }

        /// <summary>
        /// ԭ�ϳ�����ˮ�� Ŀǰ�����ڹ�Ӧ��ԭ�����ĳ�����ˮ��
        /// </summary>
        public string MaterialOutNO
        {
            get
            {
                return this.materialOutNO;
            }
            set
            {
                this.materialOutNO = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Expand Clone()
        {
            Expand expand = base.Clone() as Expand;
            expand.prescription = this.prescription.Clone();

            return expand;
        }

        #endregion
    }
}
