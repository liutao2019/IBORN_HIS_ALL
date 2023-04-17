using FS.FrameWork.Models;


namespace FS.HISFC.Models.Terminal
{
    /// <summary>
    /// TerminalConfirmDetail <br></br>
    /// [��������: �ն�ȷ��ҵ����ϸ]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-3-1]<br></br>
    /// <˵��>
    ///     1��  {F8383442-78B0-40c2-B906-50BA52ADB139}  ����ʵ������ ִ���豸��ִ�м�ʦ
    /// </˵��>
    /// </summary>
    [System.Serializable]
    public class TerminalConfirmDetail : FS.FrameWork.Models.NeuObject
    {
        public TerminalConfirmDetail()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// ���뵥��Ϣ
        /// </summary>
        TerminalApply terminalApply = new TerminalApply();

        /// <summary>
        /// ҽԺ�������
        /// </summary>
        FS.FrameWork.Models.NeuObject hospital = new NeuObject();

        /// <summary>
        /// ��¼��ˮ��
        /// </summary>
        string sequence = "";

        /// <summary>
        /// ʣ������
        /// </summary>
        decimal freeCount = 0m;

        /// <summary>
        /// ״̬
        /// </summary>
        FS.FrameWork.Models.NeuObject status = new NeuObject();
        private string moOrder = ""; //ҽ����ˮ��
        private string execMoOrder = "";//ҽ������ˮ��
        //����Ա
        FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        //������
        FS.HISFC.Models.Base.OperEnvironment cancelInfo = new FS.HISFC.Models.Base.OperEnvironment();

        //ִ���豸
        private string execDevice = "";

        //ִ�м�ʦ
        FS.HISFC.Models.Base.Employee oper = new FS.HISFC.Models.Base.Employee();

        #endregion

        #region ����

        /// <summary>
        /// ִ���豸
        /// </summary>
        public string ExecDevice
        {
            get
            {
                return execDevice;
            }
            set
            {
                execDevice = value;
            }
        }

        /// <summary>
        /// ִ�м�ʦ
        /// </summary>
        public FS.HISFC.Models.Base.Employee Oper
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
        /// ����Ա 
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CancelInfo
        {
            get
            {
                return cancelInfo;
            }
            set
            {
                cancelInfo = value;
            }
        }

        /// <summary>
        /// ���뵥��Ϣ
        /// </summary>
        public TerminalApply Apply
        {
            get
            {
                return this.terminalApply;
            }
            set
            {
                this.terminalApply = value;
            }
        }


        /// <summary>
        /// ҽԺ�������
        /// </summary>
        public FS.FrameWork.Models.NeuObject Hospital
        {
            get
            {
                return this.hospital;
            }
            set
            {
                this.hospital = value;
            }
        }

        /// <summary>
        /// ��¼��ˮ��
        /// </summary>
        public string Sequence
        {
            get
            {
                return this.sequence;
            }
            set
            {
                this.sequence = value;
            }
        }

        /// <summary>
        /// ʣ������
        /// </summary>
        public decimal FreeCount
        {
            get
            {
                return this.freeCount;
            }
            set
            {
                this.freeCount = value;
            }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public FS.FrameWork.Models.NeuObject Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
        /// <summary>
        /// ҽ����ˮ��
        /// </summary>
        public string MoOrder
        {
            get
            {
                return moOrder;
            }
            set
            {
                moOrder = value;
            }
        }
        /// <summary>
        /// ҽ����ִ����ˮ��
        /// </summary>
        public string ExecMoOrder
        {
            get
            {
                return execMoOrder;
            }
            set
            {
                execMoOrder = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>�ն�ȷ����ϸ</returns>
        public new TerminalConfirmDetail Clone()
        {
            TerminalConfirmDetail terminalConfirmDetail = base.Clone() as TerminalConfirmDetail;

            terminalConfirmDetail.Apply = this.Apply.Clone();
            terminalConfirmDetail.Hospital = this.Hospital.Clone();
            terminalConfirmDetail.Status = this.Status.Clone();
            terminalConfirmDetail.oper = this.Oper.Clone();

            return terminalConfirmDetail;
        }

        #endregion
    }
}
