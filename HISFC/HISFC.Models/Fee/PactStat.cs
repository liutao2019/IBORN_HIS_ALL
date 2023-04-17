using System;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// PactStat ��ժҪ˵����
	/// id,name
	/// </summary>
    /// 
    [System.Serializable]
	public class PactStat :FS.FrameWork.Models.NeuObject,FS.HISFC.Models.Base.ISpell
	{
		public PactStat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		//ƴ����
		private string spellCode ;
		//���
		private string wBCode;
		// ������
		private string userCode;
		// ˳��� 
		public int SortId;

		//��Ч�Ա�ʶ
		public string ValidState;

		//�����˴���
		public string Opercode;
		#region ISpell ��Ա

		public string SpellCode
		{
			get
			{
				return spellCode;
			}
			set
			{
				this.spellCode = value;
			}
		}

		public string WBCode
		{
			get
			{
				return this.wBCode;
			}
			set
			{
				this.wBCode = value;
			}
		}

		public string UserCode
		{
			get
			{
				return this.userCode;
			}
			set
			{
				this.userCode = value;
			}
		}

		#endregion
	}
}
