using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
    /// ������<br></br>
    /// <Font color='#FF1111'>[��������: ҽ��ICD]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08-14]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    [Serializable]
    public class ICDMedicare : Spell, IValid
    {
        	/// <summary>
	/// ���캯��
	/// </summary>
        public ICDMedicare()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	#region ����

	#region ˽��
        private String seqID;//���к�
        private String icdType;//��ͬ��λ
	#endregion

	#region ����
	#endregion

	#region ����
	#endregion

	#endregion

	#region ����
        /// <summary>
        /// ���к�
        /// </summary>
        public String SeqID
        {
            get
            {
                return seqID;
            }
            set
            {
                seqID = value;
            }
        }
        public String IcdType
        {
            get
            {
                return icdType;
            }
            set
            {
                icdType = value;
            }
        }
	#endregion

	#region ����

	#region ��Դ�ͷ�
	#endregion

	#region ��¡

	#endregion

	#region ˽��
	#endregion

	#region ����
	#endregion

	#region ����
	#endregion

	#endregion

	#region �¼�
	#endregion

	#region �ӿ�ʵ��
	#endregion


        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
