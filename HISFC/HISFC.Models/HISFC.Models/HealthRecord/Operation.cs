using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// Operation ��ժҪ˵����������������������Ϣ
    /// </summary>
    [Serializable]
    public class Operation : FS.HISFC.Models.Base.Spell
    {

        public Operation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ˽�б���

        private FS.FrameWork.Models.NeuObject myOperationInfo = new FS.FrameWork.Models.NeuObject();
        private string operationEnName;

        #endregion

        #region ����

        /// <summary>
        /// ������Ŀ��Ϣ ID �������� Name ������������
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperationInfo
        {
            get { return myOperationInfo; }
            set { myOperationInfo = value; }
        }
        /// <summary>
        /// ����Ӣ������
        /// </summary>
        public string OperationEnName
        {
            get { return operationEnName; }
            set { operationEnName = value; }
        }

        #endregion

        #region ���к���


        public new Operation Clone()
        {
            Operation OpClone = base.MemberwiseClone() as Operation;

            OpClone.myOperationInfo = this.myOperationInfo.Clone();

            return OpClone;
        }

        #endregion



    }
}
