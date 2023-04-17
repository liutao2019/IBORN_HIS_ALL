using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ��Ʒ������]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    /// 
    public class SanReturnMain : Neusoft.NFC.Object.NeuObject, Neusoft.HISFC.Object.Base.IValidState
    {
        public SanReturnMain()
        {

        }

        #region ����
        /// <summary>
        /// ������ˮ��
        /// </summary>
        private string returnCode = string.Empty;

        /// <summary>
        /// ���ռ�¼˳���
        /// </summary>
        private int sortNumber = 0;

        /// <summary>
        /// ���յ��ݺ�
        /// </summary>
        private string billCode = string.Empty;

        /// <summary>
        /// ����״̬1����2����ȷ��3���ȷ��4���ȷ��5��ȡȷ��
        /// </summary>
        private QCReturnState returnState = QCReturnState.APPLY;

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        private HISFC.Object.Material.StoreBase storeBase = new Neusoft.HISFC.Object.Material.StoreBase();

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment applyOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �����������
        /// </summary>
        private decimal applyNum = 0;

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment inOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��������(ʵ���յ�����)
        /// </summary>
        private decimal inNum = 0;

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment sterOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ�����1��0��
        /// </summary>
        private bool sterFlag = false;

        /// <summary>
        /// �����Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment packOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �������
        /// </summary>
        private decimal packNum = 0;

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment getOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// ��������
        /// </summary>
        private decimal getNum = 0;

        /// <summary>
        /// ͣ����Ա��Ϣ
        /// </summary>
        private HISFC.Object.Base.OperEnvironment stopOper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private HISFC.Object.Base.EnumValidState validState = Neusoft.HISFC.Object.Base.EnumValidState.Valid; 
        #endregion

        #region ʵ��
        /// <summary>
        /// ������ˮ��
        /// </summary>
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        /// <summary>
        /// ���ռ�¼˳���
        /// </summary>
        public int SortNumber
        {
            get
            {
                return sortNumber;
            }
            set
            {
                sortNumber = value;
            }
        }

        /// <summary>
        /// ���յ��ݺ�
        /// </summary>
        public string BillCode
        {
            get
            {
                return billCode;
            }
            set
            {
                billCode = value;
            }
        }

        /// <summary>
        /// ����״̬1����2����ȷ��3���ȷ��4���ȷ��5��ȡȷ��
        /// </summary>
        public QCReturnState ReturnState
        {
            get
            {
                return returnState;
            }
            set
            {
                returnState = value;
            }
        }

        /// <summary>
        /// ��Ʒ��Ϣ
        /// </summary>
        public HISFC.Object.Material.StoreBase StoreBase
        {
            get
            {
                return storeBase;
            }
            set
            {
                storeBase = value;
            }
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment ApplyOper
        {
            get
            {
                return applyOper;
            }
            set
            {
                applyOper = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal ApplyNum
        {
            get
            {
                return applyNum;
            }
            set
            {
                applyNum = value;
            }
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment InOper
        {
            get
            {
                return inOper;
            }
            set
            {
                inOper = value;
            }
        }

        /// <summary>
        /// ��������(ʵ���յ�����)
        /// </summary>
        public decimal InNum
        {
            get
            {
                return inNum;
            }
            set
            {
                inNum = value;
            }
        }

        /// <summary>
        ///�����Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment SterOper
        {
            get
            {
                return sterOper;
            }
            set
            {
                sterOper = value;
            }
        }

        /// <summary>
        /// �Ƿ�����1��0��
        /// </summary>
        public bool SterFlag
        {
            get
            {
                return sterFlag;
            }
            set
            {
                sterFlag = value;
            }
        }

        /// <summary>
        /// �����Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment PackOper
        {
            get
            {
                return packOper;
            }
            set
            {
                packOper = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public decimal PackNum
        {
            get
            {
                return packNum;
            }
            set
            {
                packNum = value;
            }
        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment GetOper
        {
            get
            {
                return getOper;
            }
            set
            {
                getOper = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal GetNum
        {
            get
            {
                return getNum;
            }
            set
            {
                getNum = value;
            }
        }

        /// <summary>
        /// ͣ����Ա��Ϣ
        /// </summary>
        public HISFC.Object.Base.OperEnvironment StopOper
        {
            get
            {
                return stopOper;
            }
            set
            {
                stopOper = value;
            }
        }

        #region IValidState ��Ա
        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.EnumValidState ValidState
        {
            get
            {
                return this.validState;
            }
            set
            {
                this.validState = value;
            }
        }

        #endregion 
        #endregion

        #region ����
        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public new SanReturnMain Clone()
        {
            SanReturnMain sanReturnMain = base.Clone() as SanReturnMain;

            sanReturnMain.StoreBase = this.StoreBase.Clone();

            sanReturnMain.ApplyOper = this.ApplyOper.Clone();

            sanReturnMain.InOper = this.InOper.Clone();

            sanReturnMain.ApplyOper = this.ApplyOper.Clone();

            sanReturnMain.SterOper = this.SterOper.Clone();

            sanReturnMain.PackOper = this.PackOper.Clone();

            sanReturnMain.GetOper = this.GetOper.Clone();

            sanReturnMain.StopOper = this.StopOper.Clone();

            return sanReturnMain;
        }

        #endregion
    }
}
