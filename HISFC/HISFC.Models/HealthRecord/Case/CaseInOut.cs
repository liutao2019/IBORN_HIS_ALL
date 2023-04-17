using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.HealthRecord.Case
{
    /// <summary>
    /// [��������: ���������ʵ��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007/08/23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    public class CaseInOut : Neusoft.NFC.Object.NeuObject
    {
        public CaseInOut()
        {
        }

        #region ˽�б���

        /// <summary>
        /// �������ˮ��
        /// </summary>
        private int bill;

   
        /// <summary>
        ///���ⵥ����
        /// </summary>
        private string code;

     
        /// <summary>
        ///���������Ϣ  
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment operRequest = new Neusoft.HISFC.Object.Base.OperEnvironment();

 
        /// <summary>
        ///������벡������        
        /// </summary>
        private string requestNurseCode;

 
        /// <summary>
        ///��������������
        /// </summary>
        private string requestPartCode;

      
        /// <summary>
        ///�����׼��Ϣ 
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment operAuditing = new Neusoft.HISFC.Object.Base.OperEnvironment();

        
        /// <summary>
        ///������˲�������
        /// </summary>
        private string auditingNurseCode;

        
        /// <summary>
        ///���ȷ����Ϣ 
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment operConfirm = new Neusoft.HISFC.Object.Base.OperEnvironment();

       
        /// <summary>
        ///����ȷ����Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment operSend = new Neusoft.HISFC.Object.Base.OperEnvironment();

      
        /// <summary>
        ///����ȷ����Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Base.OperEnvironment operReceive = new Neusoft.HISFC.Object.Base.OperEnvironment();

         
        /// <summary>           
        /// �Ƿ����
        /// </summary>
        private bool isReceive;

      
        /// <summary>
        /// ����ΨһID
        /// </summary>
        private int caseID;

      
        /// <summary>
        /// ����״̬
        /// </summary>
        private string billState;

     
        /// <summary>           
        /// �Ƿ񱻷���
        /// </summary>
        private bool isSend;

        #endregion


        #region ����

        /// <summary>
        /// �������ˮ��
        /// </summary>
        public int Bill
        {
            get { return bill; }
            set { bill = value; }
        }
        
        /// <summary>
        ///���ⵥ����
        /// </summary>
        public string Code
        {
            get { return Code; }
            set { Code = value; }
        }

        /// <summary>
        ///���������Ϣ  �˹��� ������� ���ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment OperRequest
        {
            get { return operRequest; }
            set { operRequest = value; }
        }


        /// <summary>
        ///������벡������        
        /// </summary>
        public string RequestNurseCode
        {
            get { return RequestNurseCode; }
            set { RequestNurseCode = value; }
        }

           
        /// <summary>
        ///��������������
        /// </summary>
        public string RequestPartCode
        {
            get { return RequestPartCode; }
            set { RequestPartCode = value; }
        }

        /// <summary>
        ///�����׼��Ϣ  �˹��� ������� ����ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment OperAuditing
        {
            get { return OperAuditing; }
            set { OperAuditing = value; }
        }

        /// <summary>
        ///������˲�������
        /// </summary>
        public string AuditingNurseCode
        {
            get { return AuditingNurseCode; }
            set { AuditingNurseCode = value; }
        }

        /// <summary>
        ///���ȷ����Ϣ �˹���  ���ȷ��ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment OperConfirm
        {
            get { return OperConfirm; }
            set { OperConfirm = value; }
        }

        /// <summary>
        ///����ȷ����Ϣ �˹���  ȷ��ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment OperSend
        {
            get { return OperSend; }
            set { OperSend = value; }
        }


        /// <summary>
        ///����ȷ����Ϣ �˹��� ȷ��ʱ��
        /// </summary>
        public Neusoft.HISFC.Object.Base.OperEnvironment OperReceive
        {
            get { return OperReceive; }
            set { OperReceive = value; }
        }

        /// <summary>           
        /// �Ƿ����
        /// </summary>
        public bool IsReceive
        {
            get { return isReceive; }
            set { isReceive = value; }
        }

        /// <summary>
        /// ����ΨһID
        /// </summary>
        public int CaseID
        {
            get { return CaseID; }
            set { CaseID = value; }
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public string BillState
        {
            get { return BillState; }
            set { BillState = value; }
        }
                
        /// <summary>           
        /// �Ƿ񱻷���
        /// </summary>
        public bool IsSend
        {
            get { return isSend; }
            set { isSend = value; }
        }
        #endregion

        #region ���к���


        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new CaseInOut Clone()
        {
            CaseInOut caseInOut = base.Clone() as CaseInOut;
            caseInOut.operRequest = this.operRequest.Clone();  
            caseInOut.operAuditing = this.operAuditing.Clone();          
            caseInOut.operConfirm = this.operConfirm.Clone();
            caseInOut.operSend = this.operSend.Clone();
            caseInOut.operReceive = this.operReceive.Clone();
          
            return caseInOut;
        }

        #endregion



    }
   
}
