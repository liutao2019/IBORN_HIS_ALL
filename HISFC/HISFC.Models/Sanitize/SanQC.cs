using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// [��������: ���������¼]<br></br>
    /// [�� �� ��: shizj]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// </summary>
    public class SanQC:Neusoft.NFC.Object.NeuObject
    {
        public SanQC()
        {

        }

        #region ����

        /// <summary>
        /// ����������ˮ��SEQ_SAN_OTHER_CODE
        /// </summary>
        private string qcCode = string.Empty;

        /// <summary>
        /// ����������
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.SanSter sanSter = new SanSter();

        /// <summary>
        /// ��������ά����
        /// </summary>
        private Neusoft.HISFC.Object.Sanitize.SanQCBase sanQCBase = new SanQCBase();

        /// <summary>
        /// ��Ŀֵ
        /// </summary>
        private string itemValue = string.Empty;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment oper = new Neusoft.HISFC.Object.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ����Ա
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

        /// <summary>
        /// ��Ŀֵ
        /// </summary>
        public string ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
            }
        }

        /// <summary>
        /// ��������ά����
        /// </summary>
        public Neusoft.HISFC.Object.Sanitize.SanQCBase SanQCBase
        {
            get
            {
                return this.sanQCBase;
            }
            set
            {
                this.sanQCBase = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public Neusoft.HISFC.Object.Sanitize.SanSter SanSter
        {
            get
            {
                return this.sanSter;
            }
            set
            {
                this.sanSter = value;
            }
        }

        /// <summary>
        /// ����������ˮ��SEQ_SAN_OTHER_CODE
        /// </summary>
        public string QcCode
        {
            get
            {
                return this.qcCode;
            }
            set
            {
                this.qcCode = value;
            }
        }

        #endregion

        #region ����

        #region ��¡

        public new SanQC Clone()
        {
            SanQC sanQC = base.Clone() as SanQC;
            sanQC.SanSter = new SanSter();
            sanQC.SanQCBase = new SanQCBase();
            sanQC.Oper = new Neusoft.HISFC.Object.Base.OperEnvironment();
            return sanQC;
        } 
        #endregion

        #endregion
    }
}
