using System;

namespace FS.HISFC.Models.Pharmacy 
{
	/// <summary>
	/// Copyright (C) 2004 ����ɷ����޹�˾
	/// ��Ȩ����
	/// 
	/// �ļ�����DrugRecipe.cs
	/// �ļ����������������ն�ʵ��
	/// 
	/// 
	/// ������ʶ�������� 2005-11
	/// ����������ID �ն˱��� Name �ն�����
	/// 
	/// 
	/// �޸ı�ʶ�������� 2006-09
	/// �޸���������������
	/// </summary>
    [Serializable]
    public class DrugTerminal : FS.FrameWork.Models.NeuObject
    {

        public DrugTerminal()
        {

        }


        #region ����

        /// <summary>
        /// �����ⷿ
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        ///	�ն����� 0 ��ҩ���� 1 ��ҩ̨
        /// </summary>
        private EnumTerminalType terminalType = EnumTerminalType.��ҩ̨;

        /// <summary>
        /// �ն����� 0 ��ͨ 1 ר�� 2 ����
        /// </summary>
        private EnumTerminalProperty terminalProperty = EnumTerminalProperty.��ͨ;

        /// <summary>
        /// ����ն�
        /// </summary>
        private FS.FrameWork.Models.NeuObject replaceTerminal = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ�ر�
        /// </summary>
        private bool isClose = false;

        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// ����ˢ�¼�� (��ӡ��ǩ���)
        /// </summary>
        private decimal refreshInterval1 = 10;

        /// <summary>
        /// ����Ļ��ʾ ˢ�¼��
        /// </summary>
        private decimal refreshInterval2 = 10;

        /// <summary>
        /// ������
        /// </summary>
        private int alertNum = 15;

        /// <summary>
        /// ��ʾ����
        /// </summary>
        private int showNum = 5;

        /// <summary>
        /// ��ҩ���ڱ���(ֻ������ҩ̨)
        /// </summary>
        private FS.FrameWork.Models.NeuObject sendWindow = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �ѷ��ʹ���Ʒ����
        /// </summary>
        private decimal sendQty = 0;

        /// <summary>
        /// ����ҩ����Ʒ����
        /// </summary>
        private decimal drugQty = 0;

        /// <summary>
        /// ���������о��������ľ��ִ�������
        /// </summary>
        private decimal averageNum = 0;

        /// <summary>
        /// ��ӡ����
        /// </summary>
        private EnumClinicPrintType terimalPrintType = EnumClinicPrintType.�嵥;

        #endregion

        /// <summary>
        /// �����ⷿ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
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
        /// �ն�����
        /// </summary>
        public EnumTerminalType TerminalType
        {
            get { return terminalType; }
            set { terminalType = value; }
        }


        /// <summary>
        /// �ն�����
        /// </summary>
        public EnumTerminalProperty TerminalProperty
        {
            get { return terminalProperty; }
            set { terminalProperty = value; }
        }


        /// <summary>
        /// ����ն�
        /// </summary>
        public FS.FrameWork.Models.NeuObject ReplaceTerminal
        {
            get
            {
                return this.replaceTerminal;
            }
            set
            {
                this.replaceTerminal = value;
            }
        }


        /// <summary>
        /// �Ƿ�ر�
        /// </summary>
        public bool IsClose
        {
            get { return isClose; }
            set { isClose = value; }
        }


        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        public bool IsAutoPrint
        {
            get { return isAutoPrint; }
            set { isAutoPrint = value; }
        }


        /// <summary>
        /// ����ˢ�¼��
        /// </summary>
        public decimal RefreshInterval1
        {
            get { return refreshInterval1; }
            set { refreshInterval1 = value; }
        }


        /// <summary>
        /// ��ӡ/��ʾ ˢ�¼��
        /// </summary>
        public decimal RefreshInterval2
        {
            get { return refreshInterval2; }
            set { refreshInterval2 = value; }
        }


        /// <summary>
        /// ������
        /// </summary>
        public int AlertQty
        {
            get { return alertNum; }
            set { alertNum = value; }
        }


        /// <summary>
        /// ����Ļ��ʾ����
        /// </summary>
        public int ShowQty
        {
            get { return showNum; }
            set { showNum = value; }
        }


        /// <summary>
        /// ��ҩ���ڱ���(ֻ������ҩ̨)
        /// </summary>
        public FS.FrameWork.Models.NeuObject SendWindow
        {
            get { return sendWindow; }
            set { sendWindow = value; }
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
        /// �ѷ�����ҩ����Ʒ����
        /// </summary>
        public decimal SendQty
        {
            get
            {
                return this.sendQty;
            }
            set
            {
                this.sendQty = value;
            }
        }


        /// <summary>
        /// ����ҩ����Ʒ����
        /// </summary>
        public decimal DrugQty
        {
            get
            {
                return this.drugQty;
            }
            set
            {
                this.drugQty = value;
            }
        }


        /// <summary>
        /// ���������о��������ľ��ִ�������
        /// </summary>
        public decimal Average
        {
            get
            {
                return this.averageNum;
            }
            set
            {
                this.averageNum = value;
            }
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        public EnumClinicPrintType TerimalPrintType
        {
            get
            {
                return terimalPrintType;
            }
            set
            {
                this.terimalPrintType = value;
            }
        }

        #region ����

        /// <summary>
        /// ��¡���� 
        /// </summary>
        /// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
        public new DrugTerminal Clone()
        {
            DrugTerminal drugTerminal = base.Clone() as DrugTerminal;

            drugTerminal.Dept = this.Dept.Clone();
            drugTerminal.ReplaceTerminal = this.ReplaceTerminal.Clone();
            drugTerminal.Oper = this.Oper.Clone();
            drugTerminal.SendWindow = this.sendWindow.Clone();

            return drugTerminal;
        }


        #endregion

        #region ��Ч����

        private string terminalCode;		//�ն˱��

        private string terminalName;		//�ն�����

        private string deptCode;			//�����ⷿ����

        private string replaceCode;			//����ն˱��

        private string operCode;			//����Ա

        private DateTime operDate;			//����ʱ��

        private string mark;				//��ע

        /// <summary>
        /// �����ⷿ����
        /// </summary>
        [System.Obsolete("�������� ����ΪNeuobject���͵�Dept����", true)]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }


        /// <summary>
        /// ����ն˱��
        /// </summary>
        [System.Obsolete("�������� ����ΪNeuobject���͵�ReplactTerminal����")]
        public string ReplaceCode
        {
            get { return replaceCode; }
            set { replaceCode = value; }
        }



        /// <summary>
        /// ����Ա
        /// </summary>
        [System.Obsolete("�������� ����ΪOper����", true)]
        public string OperCode
        {
            get { return operCode; }
            set { operCode = value; }
        }


        /// <summary>
        /// ����ʱ��
        /// </summary>
        [System.Obsolete("�������� ����ΪOper����", true)]
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }


        /// <summary>
        /// ��ע
        /// </summary>
        [System.Obsolete("�������� ����Ϊ�����Memo����")]
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }


        /// <summary>
        /// ���������о��������ľ��ִ�������
        /// </summary>
        [System.Obsolete("�������� ����ΪAverage����")]
        public decimal AverageNum
        {
            get
            {
                return this.averageNum;
            }
            set
            {
                this.averageNum = value;
            }
        }


        /// <summary>
        /// �ն˱��
        /// </summary>
        [System.Obsolete("�������� ����Ϊ�����ID����", true)]
        public string TerminalCode
        {
            get { return terminalCode; }
            set
            {
                terminalCode = value;
                this.ID = value;
            }
        }


        /// <summary>
        /// �ն�����
        /// </summary>
        [System.Obsolete("�������� ����Ϊ�����Name����", true)]
        public string TerminalName
        {
            get { return terminalName; }
            set
            {
                terminalName = value;
                this.Name = value;
            }
        }


        /// <summary>
        /// ������
        /// </summary>
        [System.Obsolete("�������� ����ΪAlterQty����", true)]
        public int AlertNum
        {
            get { return alertNum; }
            set { alertNum = value; }
        }


        /// <summary>
        /// ����Ļ��ʾ����
        /// </summary>
        [System.Obsolete("�������� ����ΪShowQty����", true)]
        public int ShowNum
        {
            get { return showNum; }
            set { showNum = value; }
        }


        #endregion

    }
}
