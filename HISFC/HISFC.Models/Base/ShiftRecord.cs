using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// Const<br></br>
    /// [��������: ��Ϣ�����¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// 
    /// <˵�� >ID ��Ŀ����</˵��>
    /// </summary>
    [System.Serializable]
    public class ShiftRecord : FS.FrameWork.Models.NeuObject
    {
        public ShiftRecord()
        {

        }

        #region �����

        /// <summary>
        /// �������
        /// </summary>
        private decimal happenNO;

        /// <summary>
        /// ��Ŀ��� 0 ҩƷ 1 ��ҩƷ 2 ����
        /// </summary>
        private string itemType;

        /// <summary>
        /// ԭʼ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject originalData = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject newData = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ԭ��
        /// </summary>
        private string shiftCause;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        public decimal HappenNO
        {
            get
            {
                return this.happenNO;
            }
            set
            {
                this.happenNO = value;
            }
        }

        /// <summary>
        /// ��Ŀ��� 0 ҩƷ 1 ��ҩƷ 2 ����
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
        /// ԭʼ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject OriginalData
        {
            get
            {
                return this.originalData;
            }
            set
            {
                this.originalData = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.FrameWork.Models.NeuObject NewData
        {
            get
            {
                return this.newData;
            }
            set
            {
                this.newData = value;
            }
        }

        /// <summary>
        /// ���ԭ��
        /// </summary>
        public string ShiftCause
        {
            get
            {
                return this.shiftCause;
            }
            set
            {
                this.shiftCause = value;
            }
        }

        /// <summary>
        /// ������Ϣ
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

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ShiftRecord Clone()
        {
            ShiftRecord sfRecord = base.Clone() as ShiftRecord;

            sfRecord.originalData = this.originalData.Clone();

            sfRecord.newData = this.newData.Clone();

            sfRecord.oper = this.oper.Clone();

            return sfRecord;
        }

        #endregion
    }
}
