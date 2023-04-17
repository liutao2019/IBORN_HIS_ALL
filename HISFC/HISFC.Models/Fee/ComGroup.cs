using System;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// ComGroup ��ժҪ˵����
	/// </summary>
    /// 
    [System.Serializable]
	public class ComGroup : FS.FrameWork.Models.NeuObject 
	{
		public ComGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
//		GROUP_ID     VARCHAR2(10)                   ����ID                
//		GROUP_NAME   VARCHAR2(50)  Y                ��������              
//		SPELL_CODE   VARCHAR2(8)   Y                ����ƴ����            
//		INPUT_CODE   VARCHAR2(8)   Y                ����������            
//		GROUP_KIND   VARCHAR2(3)   Y                ��������              
//		DEPT_CODE    VARCHAR2(4)   Y                ���׿���              
//		SORT_ID      NUMBER        Y                ��ʾ˳��              
//		VALID_FLAG   VARCHAR2(1)   Y                ��Ч��־��1��Ч/2��Ч 
//		REMARK       VARCHAR2(150) Y                ���ױ�ע              
//		OPER_CODE    VARCHAR2(6)   Y                ����Ա                
//		OPER_DATE    DATE          Y                ����ʱ��      
		// id ����ID
		//NAME ��������
		public string spellCode ;//����ƴ����
		public string WBCode ; //�����
		public string inputCode ;// ����������
		public string groupKind ;//�������� 
		public string deptCode; //���׿���
		public string deptName;//������
		public int    sortId ; //��ʾ˳��
		public FS.HISFC.Models.Base.EnumValidState ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;// ��Ч��־��1��Ч/0��Ч
		public string reMark ; //���ױ�ע 
		public string operCode; //����Ա
		public string operName;
		public System.DateTime operDate;
        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// <summary>
        /// �ϼ��ڵ�
        /// </summary>
        private string parentGroupID = string.Empty;

        /// <summary>
        ///�ϼ��ڵ�{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        public string ParentGroupID
        {
            get { return parentGroupID; }
            set { parentGroupID = value; }
        }
        
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ComGroup Clone()
		{
			return this.MemberwiseClone() as ComGroup;
		}
	}
}
