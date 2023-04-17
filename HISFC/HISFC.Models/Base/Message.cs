using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// ID���룬Name��Ϣ����
    /// </summary>
    [Serializable]
    public class Message : FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject sender = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject receiver = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }
        /// <summary>
        /// �����˿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject senderDept = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject SenderDept
        {
            get { return senderDept; }
            set { senderDept = value; }
        }
        /// <summary>
        /// �����˿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject receiverDept = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject ReceiverDept
        {
            get { return receiverDept; }
            set { receiverDept = value; }
        }
        /// <summary>
        /// �Ƿ����״̬ 1���Ķ� 0δ�Ķ�
        /// </summary>
        private bool isRecieved = false;

        public bool IsRecieved
        {
            get { return isRecieved; }
            set { isRecieved = value; }
        }
        /// <summary>
        /// ��Ϣ��������  0���Ķ� 1 �ѻظ� 2 �Ѵ���
        /// </summary>
        private int replyType;

        public int ReplyType
        {
            get { return replyType; }
            set { replyType = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        private OperEnvironment oper = new OperEnvironment();

        public OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        /// <summary>
        /// ���߲���
        /// </summary>
        private FS.FrameWork.Models.NeuObject emr = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Emr
        {
            get { return emr; }
            set { emr = value; }
        }
        /// <summary>
        /// ����סԺ��ˮ��
        /// </summary>
        private string inpatientNo;

        public string InpatientNo
        {
            get { return inpatientNo; }
            set { inpatientNo = value; }
        }

        
    }
}
