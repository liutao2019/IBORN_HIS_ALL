using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [��������: ����ģ����ϸʵ����]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2008-10-07]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ModelList : Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

	    /// <summary>
	    /// ���캯��(ID:��ϸ����ˮ��)
	    /// </summary>
        public ModelList()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #endregion ���캯��

        #region ����

        /// <summary>
        /// ģ��������ˮ��
        /// </summary>
        private string modelMainID;

        /// <summary>
        /// ������Ŀ������Ϣ��������ˮ�š��������ơ����������ʶ���������ֵ�����౸ע��
        /// </summary>
        private KindInfo kindInfo = new KindInfo();

        /// <summary>
        /// ������Ŀ��Ϣ����Ŀ��ˮ�š���Ŀ˳��š���Ŀ���ơ���Ŀ���͡�ȡֵ��Χ����Ŀ��λ����Ŀ��������Ŀ��ѯ�롢�Ƿ�ɱ༭����Ŀ��ע��
        /// </summary>
        private NMSKindItem kindItemInfo = new NMSKindItem();       

        #endregion

        #region ����

        /// <summary>
        /// ģ��������ˮ��
        /// </summary>
        public string ModelMainID
        {
            get
            {
                return modelMainID;
            }
            set
            {
                modelMainID = value;
            }
        }

        /// <summary>
        /// ������Ŀ������Ϣ��������ˮ�š��������ơ����������ʶ���������ֵ�����౸ע��
        /// </summary>
        public KindInfo KindInfo
        {
            get
            {
                return kindInfo;
            }
            set
            {
                kindInfo = value;
            }
        }     

        /// <summary>
        /// ������Ŀ��Ϣ����Ŀ��ˮ�š���Ŀ˳��š���Ŀ���ơ���Ŀ���͡�ȡֵ��Χ����Ŀ��λ����Ŀ��������Ŀ��ѯ�롢�Ƿ�ɱ༭����Ŀ��ע��
        /// </summary>
        public NMSKindItem KindItemInfo
        {
            get
            {
                return kindItemInfo;
            }
            set
            {
                kindItemInfo = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new ModelList Clone()
        {
            ModelList modelList = base.Clone() as ModelList;
            modelList.KindInfo = this.kindInfo.Clone();
            modelList.KindItemInfo = this.kindItemInfo.Clone();

            return modelList;
        }

        #endregion
    }
}
