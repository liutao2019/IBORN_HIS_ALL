using System;
using Neusoft.FrameWork.Models;

namespace Neusoft.HISFC.Models.Medical
{

    /// <summary>
    /// [��������: ����ʵ��]<br></br>
    /// [�� �� ��: ���Ʒ�]<br></br>
    /// [����ʱ��: 2006-09-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����ΰ'
    ///		�޸�ʱ��='2006-9-12'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		�޸���='sunm'
    ///		�޸�ʱ��='2007-4-26'
    ///		�޸�Ŀ��=''
    ///		�޸�����='�������'
    ///  />
    /// </summary> 
    [Serializable]
    public class Allergy : NeuObject
    {

        /// <summary>
        /// ���캯��
        /// </summary>
        public Allergy()
        {
        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        private AllergyType type;

        /// <summary>
        /// ����֢״
        /// </summary>
        private NeuObject symptom;

        /// <summary>
        /// ����Դ
        /// </summary>
        private NeuObject allergen;

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark;
        #endregion

        #region ����

        /// <summary>
        /// ��������Value	Description
        ///DA	Drug Allergy
        ///FA	Food Allergy
        ///MA	Miscellaneous Allergy
        ///MC	Miscellaneous Contraindication
        /// </summary>
        /// User-defined Table 0127 - Allergy type
        public AllergyType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }


        /// <summary>
        /// ����֢״
        /// </summary>
        public NeuObject Symptom
        {
            get
            {
                return this.symptom;
            }
            set
            {
                this.symptom = value;
            }
        }

        /// <summary>
        /// ����Դ
        /// </summary>
        public NeuObject Allergen
        {
            get
            {
                return this.allergen;
            }
            set
            {
                this.allergen = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        #endregion

        #region  ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Allergy clone()
        {
            Allergy obj = base.Clone() as Allergy;

            return obj;
        }

        #endregion

    }
}
