
using System;
 

namespace Neusoft.HISFC.Object.RADT
{
    /// <summary>
    /// ������Ժ״̬ written by wolf 
    /// 2004-6-9
    /// <br>Value	Description</br>
    ///	<br>R	Registration סԺ�Ǽ���� �ȴ�����</br>
    ///	<br>I	after Receiption,in ����������� ��Ժ״̬</br>
    ///	<br>B	Balance  ��Ժ�Ǽ���� ����״̬</br>
    ///	<br>O	out Balance��Ժ�������</br>				
    ///	<br>P	PreOutԤԼ��Ժ</br>
    ///	<br>N	NoFee�޷���Ժ</br>
    /// <br>C cancel ȡ��״̬</br> 
    /// </summary>
    [Obsolete("�Ѿ����ڣ�����ΪEnumInState")]
    public class VisitStatus:Neusoft.NFC.Object.NeuObject
    {
        /// <summary>
        /// ������Ժ״̬��
        /// </summary>
        public VisitStatus()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��Ժ״̬
        /// </summary>
        public enum enuVisitStatus
        {
            /// <summary>
            /// Registration סԺ�Ǽ���� �ȴ�����
            /// </summary>
            R =0,
            /// <summary>
            /// after Receiption,in ����������� ��Ժ״̬
            /// </summary>
            I =1,
            /// <summary>
            /// Balance  ��Ժ�Ǽ���� ����״̬
            /// </summary>
            B =2,
            /// <summary>
            /// out Balance��Ժ�������
            /// </summary>
            O =3,
            /// <summary>
            ///PreOutԤԼ��Ժ
            /// </summary>
            P =4,
            /// <summary>
            /// NoFee�޷���Ժ
            /// </summary>
            N =5,
            /// <summary>
            /// Close ����״̬
            /// </summary>
            C =6
        };
		
        /// <summary>
        /// ����ID
        /// </summary>
        private enuVisitStatus myID;
        public new System.Object ID
        {
            get
            {
                return this.myID;
            }
            set
            {
                try
                {
                    this.myID=(this.GetIDFromName (value.ToString())); 
                }
                catch
                {}
                base.ID=this.myID.ToString();
                string s=this.Name;
            }
        }
        public enuVisitStatus GetIDFromName(string Name)
        {
            enuVisitStatus c=new enuVisitStatus();
            for(int i=0;i<100;i++)
            {
                c=(enuVisitStatus)i;
                if(c.ToString()==Name) return c;
            }
            return (enuVisitStatus)int.Parse(Name);
        }
        public new string Name
        {
            get
            {
                string strVisitStatus;
                switch ((int)this.ID)
                {
                    case 0:
                        strVisitStatus= "סԺ�Ǽ�";
                        break;
                    case 1:
                        strVisitStatus="��Ժ״̬";
                        break;
                    case 2:
                        strVisitStatus="��Ժ�Ǽ�";
                        break;
                    case 3:
                        strVisitStatus="��Ժ����";
                        break;
                    case 4:
                        strVisitStatus="ԤԼ��Ժ";
                        break;
                    case 5:
                        strVisitStatus="�޷���Ժ";
                        break;
                    case 6:
                        strVisitStatus="����";
                        break;
                    default:
                        strVisitStatus="δ֪";
                        break;
                }
                base.Name=strVisitStatus;
                return	strVisitStatus;
            }
        }
        /// <summary>
        /// ���ȫ���б�
        /// </summary>
        /// <returns>ArrayList(VisitStatus)</returns>
        public System.Collections.ArrayList List()
        {
            VisitStatus aVisitStatus;
            System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
            int i;
            for(i=0;i<=6;i++)
            {
                aVisitStatus=new VisitStatus();
                aVisitStatus.ID=(enuVisitStatus)i;
                aVisitStatus.Memo=i.ToString();
                alReturn.Add(aVisitStatus);
            }
            return alReturn;
        }
        public new VisitStatus Clone()
        {
            return this.MemberwiseClone() as VisitStatus;
        }
    }
}
