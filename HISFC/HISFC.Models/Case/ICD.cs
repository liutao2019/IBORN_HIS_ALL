using System;
using neusoft.neuFC.Object;
using neusoft.HISFC.Object.Base;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ICD ��ժҪ˵����
	/// ICD����ά��ʵ��,����ICD10, ICD9��3.	ICD-9-CM-3����
	/// ID �洢 ICD���  NAME  �洢ICD���� 
	/// </summary>
	public class ICD : neuObject, ISpellCode
	{
		public ICD()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ˽�б���
		
		private string seqNo;								//���
		private string siCode;							//ҽ�����Ĵ���
		private string deadReason;						//����ԭ��
		private string diseaseCode;						//����������
		private int	standardDays;						//��׼סԺ��
		private string inpGrade;						//סԺ�ȼ�
		private string spellCode;                       //ƴ����
		private string wbCode;							//�����
		private string userCode;						//�Զ�����
		private	string  is30Illness;						//�Ƿ�30�ּ��� True �� False ����
		private string  isInfection;						//�Ƿ�Ⱦ�� True �� False ����
		private string  isTumour;							//�Ƿ�������� True �� False ����
		private string  isValid;							//�Ƿ���Ч  True ��Ч False ����
		private string keyCode;							//����,�������ô�����
		private neuObject operInfo = new neuObject();	//����Ա��Ϣ ID ���� Name ����
		private DateTime operDate;						//����ʱ��
		private string  sexType ; //�����Ա� 

		#endregion
		#region ����
		/// <summary>
		/// ���
		/// </summary>
		public string SeqNo
		{
			get
			{
				return seqNo;
			}
			set
			{
				seqNo = value;
			}
		}
		/// <summary>
		/// ҽ�����Ĵ���
		/// </summary>
		public string SICode
		{
			get
			{
				return siCode;
			}
			set
			{
				siCode = value;
			}
		}
		/// <summary>
		/// ����ԭ��
		/// </summary>
		public string DeadReason
		{
			get
			{
				return deadReason;
			}
			set
			{
				deadReason = value;
			}
		}
		/// <summary>
		/// ���༲����
		/// </summary>
		public string DiseaseCode
		{
			get
			{
				return diseaseCode;
			}
			set
			{
				diseaseCode = value;
			}
		}
		/// <summary>
		/// ��׼סԺ��
		/// </summary>
		public int StandardDays
		{
			get
			{
				return standardDays;
			}
			set
			{
				standardDays = value;
			}
		}

		/// <summary>
		/// סԺ�ȼ�
		/// </summary>
		public string InpGrade
		{
			get
			{
				return inpGrade;
			}
			set
			{
				inpGrade = value;
			}
		}
		/// <summary>
		/// �Ƿ�30�ּ��� 
		/// </summary>
		public string  Is30Illness
		{
			get
			{
				return is30Illness;
			}
			set
			{
				is30Illness = value;
			}
		}
		/// <summary>
		/// �Ƿ�Ⱦ�� 
		/// </summary>
		public string  IsInfection
		{
			get
			{
				return isInfection;
			}
			set
			{
				isInfection = value;
			}
		}
		/// <summary>
		/// �Ƿ��������
		/// </summary>
		public string  IsTumour
		{
			get
			{
				return isTumour;
			}
			set
			{
				isTumour = value;
			}
		}
		/// <summary>
		/// �Ƿ���Ч  
		/// </summary>
		public string   IsValid
		{
			get
			{
				return isValid;
			}
			set
			{
				isValid = value;
			}
		}
		/// <summary>
		/// �������ô�����
		/// </summary>
		public string KeyCode 
		{
			get 
			{
				return keyCode;
			}
			set
			{
				keyCode = value;
			}
		}
		/// <summary>
		/// ����Ա��Ϣ ID ���� Name ����
		/// </summary>
		public neusoft.neuFC.Object.neuObject OperInfo
		{
			get
			{
				return operInfo;
			}
			set
			{
				operInfo = value;
			}
		}
		/// <summary>
		///����ʱ��
		/// </summary>
		public DateTime OperDate 
		{
			get
			{
				return operDate;
			}
			set
			{
				operDate = value;
			}
		}
		#endregion
		#region ��¡����
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public new ICD Clone()
		{
			ICD icd = base.Clone() as ICD; //��¡����
			icd.operInfo = this.operInfo.Clone(); //��¡����Ա
			
			return icd;
		}
		#endregion 

		#region ISpellCode ��Ա
		/// <summary>
		/// �����Ա� 
		/// </summary>
		public string SexType
		{
			get
			{
				if(sexType == null )
				{
					sexType = "";
				}
				return sexType;
			}
			set
			{
				sexType = value;
			}
		}
		/// <summary>
		/// ƴ����
		/// </summary>
		public string Spell_Code
		{
			get
			{
				// TODO:  ��� ICD.Spell_Code getter ʵ��
				return spellCode;
			}
			set
			{
				// TODO:  ��� ICD.Spell_Code setter ʵ��
				spellCode = value;
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		public string WB_Code
		{
			get
			{
				// TODO:  ��� ICD.WB_Code getter ʵ��
				return wbCode;
			}
			set
			{
				// TODO:  ��� ICD.WB_Code setter ʵ��
				wbCode = value;
			}
		}
		/// <summary>
		/// �Զ�����
		/// </summary>
		public string User_Code
		{
			get
			{
				// TODO:  ��� ICD.User_Code getter ʵ��
				return userCode;
			}
			set
			{
				// TODO:  ��� ICD.User_Code setter ʵ��
				userCode = value;
			}
		}

		#endregion
	}
}
