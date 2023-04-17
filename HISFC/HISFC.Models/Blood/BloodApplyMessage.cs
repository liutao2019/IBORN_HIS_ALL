using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Blood
{
    /// <summary>
    /// [��������: Ѫ�����뵥�����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-5-9]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  /> 
    /// </summary>
    public class BloodApplyMessage : Neusoft.NFC.Object.NeuObject
    {
        public BloodApplyMessage()
        {

        }

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject applyDept = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ������(ȡѪ����)
        /// </summary>
        private Neusoft.NFC.Object.NeuObject myBloodDept = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ���뵥������
        /// </summary>
        private Neusoft.NFC.Object.NeuObject myBloodApplyClass = new Neusoft.NFC.Object.NeuObject();

        /// <summary>
        /// ֪ͨ����ʱ��  
        /// </summary>
        private DateTime mySendDtime;

        /// <summary>
        /// ֪ͨ��������
        /// </summary>
        private int mySendType;

        /// <summary>
        /// ��ҩ֪ͨ��� 0 ֪ͨ 1 �Ѱ�
        /// </summary>
        private int mySendFlag;

        #endregion

        #region

        /// <summary>
        /// ������ұ��� 0-ȫ������
        /// </summary>
        public Neusoft.NFC.Object.NeuObject ApplyDept
        {
            get
            {
                return this.applyDept;
            }
            set
            {
                this.applyDept = value;
            }
        }


        /// <summary>
        /// ���뵥����
        /// </summary>
        public Neusoft.NFC.Object.NeuObject MyBloodApplyClass
        {
            get
            {
                return this.myBloodApplyClass;
            }
            set
            {
                this.myBloodApplyClass = value;
            }
        }


        /// <summary>
        /// �������ͣ�1-���з��ͣ�0-��ʱ����
        /// </summary>
        public int SendType
        {
            get
            {
                return this.mySendType;
            }
            set
            {
                this.mySendType = value;
            }
        }


        /// <summary>
        /// ����֪ͨʱ��
        /// </summary>
        public System.DateTime SendTime
        {
            get
            {
                return this.mySendDtime;
            }
            set
            {
                this.mySendDtime = value;
            }
        }


        /// <summary>
        /// ȡѪ���0-֪ͨ1-��ȡ
        /// </summary>
        public int SendFlag
        {
            get
            {
                return this.mySendFlag;
            }
            set
            {
                this.mySendFlag = value;
            }
        }


        /// <summary>
        /// ȡѪ����(������)
        /// </summary>
        public Neusoft.NFC.Object.NeuObject MyBloodDept
        {
            get
            {
                return this.myBloodDept;
            }
            set
            {
                this.myBloodDept = value;
            }
        }

        #endregion

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>�ɹ����ص�ǰʵ������</returns>
        public new BloodApplyMessage Clone()
        {
            BloodApplyMessage bloodApplyMessage = base.Clone() as BloodApplyMessage;

            bloodApplyMessage.ApplyDept = this.ApplyDept.Clone();

            bloodApplyMessage.MyBloodApplyClass = this.MyBloodApplyClass.Clone();

            bloodApplyMessage.MyBloodDept = this.MyBloodDept.Clone();

            return bloodApplyMessage;
        }
    }
}
