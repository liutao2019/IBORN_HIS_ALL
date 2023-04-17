using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.MedTech
{
    /// <summary>
    /// [��������: ҽ��ִ��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class Execute :Neusoft.HISFC.Object.Base.Spell
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Execute()
        {
        }

        #region ˽���ֶ�
        /// <summary>
        /// ҽ���ն�����
        /// </summary>
        private int terminalNO;

        /// <summary>
        /// ִ������
        /// </summary>
        private int exeQty;

        /// <summary>
        /// ʣ������
        /// </summary>
        private int freeQty;

        /// <summary>
        /// ҽ��ִ�л���
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment exeEnvironment;

        /// <summary>
        /// ҽ��ȡ������
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment cancelEnvironment;
        #endregion

        #region ����
        /// <summary>
        /// ҽ��ִ�л���
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment ExeEnvironment
        {
            get
            {
                return this.exeEnvironment;
            }
            set
            {
                this.exeEnvironment = value;
            }
        }
        /// <summary>
        /// ҽ��ȡ������
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment CancelEnvironment
        {
            get
            {
                return this.cancelEnvironment;
            }
            set
            {
                this.cancelEnvironment = value;
            }
        }
        /// <summary>
        /// ҽ���ն�����
        /// </summary>
        public int TerminalNO
        {
            get
            {
                return this.terminalNO;
            }
            set
            {
                this.terminalNO = value;
            }
        }

        /// <summary>
        /// ִ������
        /// </summary>
        public int ExeQty
        {
            get
            {
                return this.exeQty;
            }
            set
            {
                this.exeQty = value;
            }
        }

        /// <summary>
        /// ʣ������
        /// </summary>
        public int FreeQty
        {
            get
            {
                return this.freeQty;
            }
            set
            {
                this.freeQty = value;
            }
        }
        #endregion
    }
}
