using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// Copyright (C) 2004 ��˹
	/// ��Ȩ����
	/// 
	/// �ļ�����DrugRecipe.cs
	/// �ļ��������������������ն�ʵ��
	/// 
	/// 
	/// ������ʶ����˹ 2005-11
	/// 
	/// 
	/// �޸ı�ʶ����˹ 2006-09
	/// �޸���������������
	/// </summary>
    [Serializable]
    public class DrugSPETerminal : FS.FrameWork.Models.NeuObject
	{
		
		public enum SPEType 
		{
			ҩƷ = 1,
			ר�� = 2,
			������� = 3,
			�ض��շѴ��� = 4
		}

		public DrugSPETerminal()
		{

		}


		#region ����

		/// <summary>
		/// �����ն�ʵ��
		/// </summary>
		private DrugTerminal drugTerminal = new DrugTerminal();	

		/// <summary>
		/// ������ҩ̨���  1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ���
		/// </summary>
		private string itemType;			

		/// <summary>
		/// ������Ŀ
		/// </summary>
		private FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// ����������Ϣ
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

		#endregion

		/// <summary>
		/// �ն�ʵ��
		/// </summary>
		public DrugTerminal Terminal 
		{
			get 
			{
				return drugTerminal;
			}
			set 
			{
				drugTerminal = value;
			}
		}


		/// <summary>
		/// ������ҩ̨��� 1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ���
		/// </summary>
		public string ItemType 
		{
			get {return itemType;}
			set {itemType = value;}
		}


		/// <summary>
		/// ������Ŀ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Item
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
		/// ����������Ϣ
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment Oper
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


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ������</returns>
		public new DrugSPETerminal Clone()
		{
			DrugSPETerminal drugSpeTerminal = base.Clone() as DrugSPETerminal;

			drugSpeTerminal.Terminal = this.Terminal.Clone();

			drugSpeTerminal.Item = this.Item.Clone();

			drugSpeTerminal.Oper = this.Oper.Clone();

			return drugSpeTerminal;
		}
		#endregion

		#region ��Ч����

		private string itemCode;			//��Ŀ����

		private string itemName;			//��Ŀ����

		private string operCode;			//����Ա

		private DateTime operDate;			//����ʱ��

		private string mark;				//��ע

		/// <summary>
		/// ������Ŀ����
		/// </summary>
		[System.Obsolete("�������� ����ΪItem����",true)]
		public string ItemCode 
		{
			get {return itemCode;}
			set {itemCode = value;}
		}


		/// <summary>
		/// ������Ŀ����
		/// </summary>
		[System.Obsolete("�������� ����ΪItem����",true)]
		public string ItemName 
		{
			get {return itemName;}
			set {itemName = value;}
		}

		
		/// <summary>
		/// ����Ա
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public string OperCode 
		{
			get {return operCode;}
			set {operCode = value;}
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�������� ����ΪOper����",true)]
		public DateTime OperDate
		{
			get {return operDate;}
			set {operDate = value;}
		}


		/// <summary>
		/// ��ע
		/// </summary>
		[System.Obsolete("�������� ����Ϊ�����Memo����",true)]
		public string Mark
		{
			get {return mark;}
			set {mark = value;}
		}


		#endregion
	}
}
