using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// PactStatRelation<br></br>
	/// [��������: ��ͬ��λ��ͳ�ƴ���֮��Ĺ�ϵʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class PactStatRelation : FS.FrameWork.Models.NeuObject,  ISort
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PactStatRelation() 
		{
			
		}

		#region ����

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		/// <summary>
		/// �𸶽��
		/// </summary>
		private decimal baseCost;

		/// <summary>
		/// �޶�
		/// </summary>
		private decimal quota;

		/// <summary>
		/// ���޶�
		/// </summary>
		private decimal dayQuota;

		/// <summary>
		/// ��������
		/// </summary>
		private OperEnvironment operEnvironment = new OperEnvironment();

		/// <summary>
		/// ����
		/// </summary>
		private FS.FrameWork.Models.NeuObject group = new NeuObject();
		
		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		private FS.HISFC.Models.Base.PactInfo pact = new PactInfo();
		
		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		private FS.FrameWork.Models.NeuObject statClass = new NeuObject();
		
		/// <summary>
		/// ˳���
		/// </summary>
		private int sortID;

		#endregion

		#region ����

		/// <summary>
		/// �𸶽��
		/// </summary>
		public System.Decimal BaseCost
		{
			get
            { 
                return this.baseCost; 
            }
			set
            { 
                this.baseCost = value; 
            }
		}

		/// <summary>
		/// �޶�
		/// </summary>
		public System.Decimal Quota
		{
			get
            { 
                return this.quota; 
            }
			set
            { 
                this.quota = value; 
            }
		}

		/// <summary>
		/// ���޶�
		/// </summary>
		public System.Decimal DayQuota
		{
			get
            { 
                return this.dayQuota; 
            }
			set
            { 
                this.dayQuota = value; 
            }
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		public OperEnvironment OperEnvironment
		{
			get
            { 
                return this.operEnvironment; 
            }
			set
            { 
                this.operEnvironment = value; 
            }
		}

		/// <summary>
		/// ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>
		/// ��ͬ��λ
		/// </summary>
		public FS.HISFC.Models.Base.PactInfo Pact
		{
			get
			{
				return this.pact;
			}
			set
			{
				this.pact = value;
			}
		}

		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		public FS.FrameWork.Models.NeuObject StatClass
		{
			get
			{
				return this.statClass;
			}
			set
			{
				this.statClass = value;
			}
		}
		#endregion

		#region ����
		
		#region �ͷ���Դ

		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}

			if (this.operEnvironment != null)
			{
				this.operEnvironment.Dispose();
				this.operEnvironment = null;
			}
			if (this.group != null)
			{
				this.group.Dispose();
				this.group = null;
			}
			if (this.pact != null)
			{
				this.pact.Dispose();
				this.pact = null;
			}
			if (this.statClass != null)
			{
				this.statClass.Dispose();
				this.statClass = null;
			}

			base.Dispose(isDisposing);

			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>��ǰ�����ʵ���ĸ���</returns>
		public new PactStatRelation Clone()
		{
			PactStatRelation pactStatRelation = base.Clone() as PactStatRelation;

			pactStatRelation.Group = this.Group.Clone();
			pactStatRelation.OperEnvironment = this.OperEnvironment.Clone();
			pactStatRelation.Pact = this.Pact.Clone();
			pactStatRelation.StatClass = this.StatClass.Clone();

			return pactStatRelation;
		}

		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա
		
		/// <summary>
		/// ���
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#endregion


		
	}
}
