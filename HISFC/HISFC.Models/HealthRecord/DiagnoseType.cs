using System;
using System.Collections;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// DiagnoseType ��ժҪ˵����
    /// </summary>
    [Serializable]
    public class DiagnoseType : FS.FrameWork.Models.NeuObject
    {
        public DiagnoseType()
        {
            //
            // TODO: �ڴ˴����ӹ��캯���߼�
            //
        }

        ////{7D094A18-0FC9-4e8b-A8E6-901E55D4C20C}

        public enum enuDiagnoseType
        {
            /// <summary>
            /// ��Ҫ���
            /// </summary>
            MAIN = 1,
            /// <summary>
            /// �������
            /// </summary>
            OTHER = 2,
            /// <summary>
            /// ����֢���
            /// </summary>
            COMPLICATION = 3,
            /// <summary>
            /// ��Ⱦ���
            /// </summary>
            INFECT = 4,
            /// <summary>
            /// �����ж����
            /// </summary>
            DAMNIFY = 5,
            /// <summary>
            /// �������
            /// </summary>
            PATHOLOGY = 6,
            /// <summary>
            /// �������
            /// </summary>
            SENSITIVE = 7,

            /// <summary>
            /// ����������
            /// </summary>
            BABYDISEASE = 8,

            /// <summary>
            /// ������Ժ��
            /// </summary>
            BABYINFECT = 9,

            /// <summary>
            /// �������
            /// </summary>
            CLINIC = 10,

            /// <summary>
            /// ��Ժ���
            /// </summary>
            IN = 11,

            /// <summary>
            /// ��ҽ����
            /// </summary>
            CHAMED_B = 12,

            /// <summary>
            /// ��ҽ��֤
            /// </summary>
            CHAMED_Z = 13,

            /// <summary>
            /// ��Ժ���
            /// </summary>
            OUT = 14,

            /// <summary>
            /// �������
            /// </summary>
            OPERATION = 15,

            /// <summary>
            /// �������
            /// </summary>
            SUBSIDIARY = 16,

            /// <summary>
            /// �ٴ����
            /// </summary>
            BUSINESS = 17,

            /// <summary>
            /// ȷ�����
            /// </summary>
            CCONFIRMED = 18,

            /// <summary>
            /// ��ǰ���
            /// </summary>
            PREOPER = 19,

            /// <summary>
            /// סԺ���
            /// </summary>
            INHOS = 20,
            /// <summary>
            /// �������
            /// </summary>
            DEAD = 21


            #region
            //			/// <summary>
            //			/// ��Ժ���
            //			/// </summary>
            //			IN = 1,
            //			/// <summary>
            //			/// ת�����
            //			/// </summary>
            //			TURNIN = 2,
            //			/// <summary>
            //			/// ��Ժ���
            //			/// </summary>
            //			OUT = 3,
            //			/// <summary>
            //			/// ת�����
            //			/// </summary>
            //			TURNOUT = 4,
            //			/// <summary>
            //			/// ȷ�����
            //			/// </summary>
            //			SURE = 5,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			DEAD = 6,
            //			/// <summary>
            //			/// ��ǰ���
            //			/// </summary>
            //			OPSFRONT = 7,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			OPSAFTER = 8,
            //			/// <summary>
            //			/// ��Ⱦ���
            //			/// </summary>
            //			INFECT = 9,
            //			/// <summary>
            //			/// �����ж����
            //			/// </summary>
            //			DAMNIFY = 10,
            //			/// <summary>
            //			/// ����֢���
            //			/// </summary>
            //			COMPLICATION = 11,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			PATHOLOGY = 12,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			SAVE = 13,
            //			/// <summary>
            //			/// ��Σ���
            //			/// </summary>
            //			FAIL = 14,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			CLINIC = 15,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			OTHER = 16,
            //			/// <summary>
            //			/// �������
            //			/// </summary>
            //			BALANCE = 17
            #endregion

        };
        /// <summary>
        /// ����ID
        /// </summary>
        private enuDiagnoseType myID;


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
                    this.myID = (this.GetIDFromName(value.ToString()));
                }
                catch
                { }
                base.ID = this.myID.ToString();
                string s = this.Name;
            }
        }

        public enuDiagnoseType GetIDFromName(string Name)
        {
            enuDiagnoseType c = new enuDiagnoseType();
            for (int i = 0; i < 100; i++)
            {
                c = (enuDiagnoseType)i;
                if (c.ToString() == Name) return c;
            }
            return (FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType)int.Parse(Name);
        }
        /// <summary>
        /// ��������
        /// </summary>
        public new string Name
        {
            get
            {
                string strDiagnoseType = "";

                switch ((int)this.ID)
                {
                    #region ��ǰ��
                    //					case 1:
                    //						strDiagnoseType= "��Ժ���";
                    //						break;
                    //					case 2:
                    //						strDiagnoseType="ת�����";
                    //						break;
                    //					case 3:
                    //						strDiagnoseType="��Ժ���";
                    //						break;
                    //					case 4:
                    //						strDiagnoseType="ת�����";
                    //						break;
                    //					case 5:
                    //						strDiagnoseType="ȷ�����";
                    //						break;
                    //					case 6:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					case 7:
                    //						strDiagnoseType= "��ǰ���";
                    //						break;
                    //					case 8:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					case 9:
                    //						strDiagnoseType="��Ⱦ���";
                    //						break;
                    //					case 10:
                    //						strDiagnoseType="�����ж����";
                    //						break;
                    //					case 11:
                    //						strDiagnoseType="����֢���";
                    //						break;
                    //					case 12:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					case 13:
                    //						strDiagnoseType= "�������";
                    //						break;
                    //					case 14:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					case 15:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					case 16:
                    //						strDiagnoseType="�������";
                    //						break;
                    //					default:
                    //						strDiagnoseType="�������";
                    //						break;
                    #endregion
                    case 1:
                        strDiagnoseType = "��Ҫ���";
                        break;
                    case 2:
                        strDiagnoseType = "�������";
                        break;
                    case 3:
                        strDiagnoseType = "����֢���";
                        break;
                    case 4:
                        strDiagnoseType = "��Ⱦ���";
                        break;
                    case 5:
                        strDiagnoseType = "�����ж����";
                        break;
                    case 6:
                        strDiagnoseType = "�������";
                        break;
                    case 7:
                        strDiagnoseType = "�������";
                        break;
                    case 8:
                        strDiagnoseType = "����������";
                        break;
                    case 9:
                        strDiagnoseType = "������Ժ��";
                        break;
                    case 10:
                        strDiagnoseType = "�������";
                        break;
                    case 11:
                        strDiagnoseType = "��Ժ���";
                        break;
                    case 12:
                        strDiagnoseType = "��ҽ����";
                        break;
                    case 13:
                        strDiagnoseType = "��ҽ��֤";
                        break;
                    case 14:
                        strDiagnoseType = "��Ժ���";
                        break; 
                    case 15:
                        strDiagnoseType = "�������";
                        break;
                    case 16:
                        strDiagnoseType = "�������";
                        break;
                }
                base.Name = strDiagnoseType;
                return strDiagnoseType;
            }
        }
        /// <summary>
        /// ���ȫ���б�
        /// </summary>
        /// <returns>ArrayList(DiagnoseType)</returns>
        public static ArrayList List()
        {
            DiagnoseType aDiagnoseType;
            enuDiagnoseType e = new enuDiagnoseType();
            ArrayList alReturn = new ArrayList();
            int i;
            for (i = 1; i <= System.Enum.GetValues(e.GetType()).GetUpperBound(0); i++)
            {
                aDiagnoseType = new DiagnoseType();
                aDiagnoseType.ID = (enuDiagnoseType)i;
                aDiagnoseType.Memo = i.ToString();
                alReturn.Add(aDiagnoseType);
            }
            return alReturn;
        }

        /// <summary>
        /// ���ȫ���б� ���ص�ʵ�� �̳���ISpellCode�ӿ�
        /// </summary>
        /// <returns>ArrayList(DiagnoseType)</returns>
        public static ArrayList SpellList()
        {
            FS.HISFC.Models.Base.Spell info = null;
            DiagnoseType aDiagnoseType;
            enuDiagnoseType e = new enuDiagnoseType();
            ArrayList alReturn = new ArrayList();
            int i;
            for (i = 1; i <= System.Enum.GetValues(e.GetType()).GetUpperBound(0) + 1; i++)
            {
                info = new FS.HISFC.Models.Base.Spell();
                aDiagnoseType = new DiagnoseType();
                aDiagnoseType.ID = i;
                aDiagnoseType.Memo = i.ToString();
                info.ID = i.ToString();
                info.Name = aDiagnoseType.Name;
                alReturn.Add(info);
            }
            return alReturn;
        }
    }
}
