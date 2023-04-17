using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Permission
{
	/// <summary>
	/// Permission<br></br>
	/// [��������: סԺ֪��ͬ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class Permission : NeuObject
	{
		#region ����
		
		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		private Item item = new Item();

		/// <summary>
		/// ��Ŀ���0��Ŀ/1��С���ô���/2ͳ�ƴ���
		/// </summary>
		private EnumItemType itemType;

		/// <summary>
		/// �Ƿ�ͬ��
		/// </summary>
		private bool isAgree;
		
		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		private OperEnvironment oper = new OperEnvironment();

		#endregion

		#region

		/// <summary>
		/// ��Ŀ��Ϣ
		/// </summary>
		public Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>
		/// ��Ŀ���0��Ŀ/1��С���ô���/2ͳ�ƴ���
		/// </summary>
		public EnumItemType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		/// <summary>
		/// �Ƿ�ͬ��
		/// </summary>
		public bool IsAgree
		{
			get
			{
				return this.isAgree;
			}
			set
			{
				this.isAgree = value;
			}
		}

		/// <summary>
		/// ��������(����Ա,����ʱ��,��������)
		/// </summary>
		public OperEnvironment Oper
		{
			get
			{
				return this.oper;
			}
			set
			{
				this.oper = value;
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ������</returns>
		public new Permission Clone()
		{
			Permission permission = base.Clone() as Permission;

			permission.Item = this.Item.Clone();
			permission.Oper = this.Oper.Clone();

			return permission;

		}

		#endregion

		#endregion

		#region ���ñ���,����

		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		[Obsolete("����,ID", true)]
		public string InpatientNo="";
		
		
		/// <summary>
		/// ����Ա����
		/// </summary>
		[Obsolete("����,Oper", true)]
		public string OperCode="";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[Obsolete("����,Oper.OperTime", true)]
		public DateTime OperDate;

		#endregion
	}

	#region ö��

	/// <summary>
	/// ֪��Ȩ��Ŀ����ö��
	/// </summary>
	public enum EnumItemType
	{
		/// <summary>
		/// ������Ŀ
		/// </summary>
		Item,

		/// <summary>
		/// ��С����
		/// </summary>
		MiniFee,
		
		/// <summary>
		/// ͳ�Ʒ���
		/// </summary>
		StatFee,
		
		/// <summary>
		/// ҽ������
		/// </summary>
		SI
	}

	#endregion

}
