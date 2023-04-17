using System;

namespace neusoft.HISFC.Object.Case 
{
	/// <summary>
	/// ICDLog ��ժҪ˵����
	/// </summary>
	public class ICDLog : neusoft.neuFC.Object.neuObject 
	{
		public ICDLog()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region  ˽�б���
		private  ICD orgICDInfo  = new ICD(); //�ۺ�ICDʵ��,������ǰ��Ϣ
		private  ICD newICDInfo = new ICD();  //�ۺ� ICDʵ�� ����������Ϣ
		private  string modifyFlag;           // ������ 1 ���� 2 �޸� 3 ����;
		private int seqNo ;                   //���,��ĳ��ICD���޸ļ�¼�����,��һ������,���Ϊ1 �Ժ�Ϊ���ֵ��1
		#endregion
		#region  ��������
		public  ICD OrgICDInfo  
		{
			//�ۺ�ICDʵ��,������ǰ��Ϣ
			get
			{
				return orgICDInfo;
			}
			set
			{
				orgICDInfo = value;
			}
		}
		public ICD NewICDInfo
		{
			//�ۺ� ICDʵ�� ����������Ϣ
			get
			{
				return newICDInfo ;
			}
			set
			{
				newICDInfo = value; 
			}
		}
		public string ModifyFlag 
		{
			// ������ 1 ���� 2 �޸� 3 ����;
			get
			{
				return modifyFlag;
			}
			set
			{
				modifyFlag = value;
			}
		}
		public int SeqNo
		{
			//���,��ĳ��ICD���޸ļ�¼�����,��һ������,���Ϊ1 �Ժ�Ϊ���ֵ��1
			get
			{
				return seqNo;
			}
			set
			{
				seqNo = value;
			}
		}

		#endregion 
		#region  
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ICDLog Clone()
		{
			//��¡����
			ICDLog  icdLog = base.Clone() as ICDLog ; // ��¡����
			icdLog.orgICDInfo = orgICDInfo.Clone(); //���ǰ��Ϣ
			icdLog.newICDInfo = newICDInfo.Clone(); //�������Ϣ
			return icdLog;
		}
		#endregion 
	}
}
