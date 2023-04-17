using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Clear<br></br>
	/// [��������: �Ƽ�����ʵ��]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: 2006-09-14]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class Clear:PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Clear()
		{
		}


		#region ����
		/// <summary>
		/// ��һ���������ƻ�����
		/// </summary>
		private string privPlanNO;
		/// <summary>
		/// ��һ���γ�Ʒ����
		/// </summary>
		private string privDrugNum;
//		private string clearOper;
//		private DateTime clearDate;

		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		private bool isMaterial;
		/// <summary>
		/// �м�Ʒ�Ƿ�ϸ�
		/// </summary>
		private bool isMid;
		/// <summary>
		/// �������Ƿ�ϸ�
		/// </summary>
		private bool isWaster;
		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		private bool isTechnics;
		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		private bool isTool;
		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		private bool isContainer;
		/// <summary>
		/// �����豸�Ƿ�ϸ�
		/// </summary>
		private bool isEquipment;
		/// <summary>
		/// ���������Ƿ�ϸ�
		/// </summary>
		private bool isWorkShop;
		/// <summary>
		/// ����Ƿ�ϸ�
		/// </summary>
		private bool isCleaner;
		/// <summary>
		/// �峡����--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment clearEnv = new FS.HISFC.Models.Base.OperEnvironment();
		/// <summary>
		/// ���Ա
		/// </summary>
		private string checkOper;
		#endregion

		#region ����
		/// <summary>
		/// ��һ���������ƻ�����
		/// </summary>
		public string PrivPlanNO
		{
			get
			{
				return this.privPlanNO;
			}
			set
			{
				this.privPlanNO = value;
			}
		}
		/// <summary>
		/// ��һ���γ�Ʒ����
		/// </summary>
		public string PrivDrugNum
		{
			get
			{
				return this.privDrugNum;
			}
			set
			{
				this.privDrugNum = value;
			}
		}
		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		public bool IsMaterial
		{
			get
			{
				return this.isMaterial;
			}
			set
			{
				this.isMaterial = value;
			}
		}


		/// <summary>
		/// �м�Ʒ�Ƿ�ϸ�
		/// </summary>
		public bool IsMid
		{
			get
			{
				return this.isMid;
			}
			set
			{
				this.isMid = value;
			}
		}


		/// <summary>
		/// �������Ƿ�ϸ�
		/// </summary>
		public bool IsWaster
		{
			get
			{
				return this.isWaster;
			}
			set
			{
				this.isWaster = value;
			}
		}


		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		public bool IsTechnics
		{
			get
			{
				return this.isTechnics;
			}
			set
			{
				this.isTechnics = value;
			}
		}


		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		public bool IsTool
		{
			get
			{
				return this.isTool;
			}
			set
			{
				this.isTool = value;
			}
		}


		/// <summary>
		/// �����Ƿ�ϸ�
		/// </summary>
		public bool IsContainer
		{
			get
			{
				return this.isContainer;
			}
			set
			{
				this.isContainer = value;
			}
		}


		/// <summary>
		/// �����豸�Ƿ�ϸ�
		/// </summary>
		public bool IsEquipment
		{
			get
			{
				return this.isEquipment;
			}
			set
			{
				this.isEquipment = value;
			}
		}

		/// <summary>
		/// ���������Ƿ�ϸ�
		/// </summary>
		public bool IsWorkShop
		{
			get
			{
				return this.isWorkShop;
			}
			set
			{
				this.isWorkShop = value;
			}
		}
		/// <summary>
		/// ����Ƿ�ϸ�
		/// </summary>
		public bool IsCleaner
		{
			get
			{
				return this.isCleaner;
			}
			set
			{
				this.isCleaner = value;
			}
		}

		/// <summary>
		/// ���Ա
		/// </summary>
		public string CheckOper
		{
			get
			{
				return this.checkOper;
			}
			set
			{
				this.checkOper = value;
			}
		}

		/// <summary>
		/// �峡����--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ClearEnv
		{
			get
			{
				return this.clearEnv;
			}
			set
			{
				this.clearEnv = value;
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Clear</returns>
		public new Clear Clone()
		{
			Clear clear = base.Clone() as Clear;
			clear.clearEnv = this.clearEnv.Clone();
			return clear;
		}
		#endregion

		#region  �Ѿ����ڵ�����
		/// <summary>
		/// ��һ���������ƻ�����
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PrivPlanNO", true)]
		public string PrivPlanNo
		{
			get
			{
				return this.privPlanNO;
			}
			set
			{
				this.privPlanNO = value;
			}
		}
		/// <summary>
		/// �峡����ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ClearEnv", true)]
		public DateTime ClearDate
		{
			get
			{
				//				return this.clearDate;
				return DateTime.Now;
			}
			set
			{
				//				this.clearDate = value;
			}
		}
		/// <summary>
		/// �峡������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ClearEnv", true)]
		public string ClearOper
		{
			get
			{
				//				return this.clearOper;
				return null;
			}
			set
			{
				//				this.clearOper = value;
			}
		}
		/// <summary>
		/// ���������Ƿ�ϸ�
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��IsWorkShop", true)]
		public bool IsWrokShop
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
		/// <summary>
		/// ��һ���γ�Ʒ����
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��PrivDrugNum", true)]
		public string PrivDrugCode
		{
			get
			{
				return this.privDrugNum;
			}
			set
			{
				this.privDrugNum = value;
			}
		}
		#endregion
	}
}
