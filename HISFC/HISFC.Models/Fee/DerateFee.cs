using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// DerateFee ��ժҪ˵����
	/// ���������
	/// memo ����ԭ��
	/// </summary>
	public class DerateFee:Neusoft.NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.IInvalid
	{
		public DerateFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ������
		/// </summary>
		public decimal DerateCost;
		/// <summary>
		/// ��������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject DerateType=new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ��С���ô���
		/// </summary>
		public string FeeCode;
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ItemCode;
		/// <summary>
		/// ��׼��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject ConfirmOperator=new Neusoft.NFC.Object.NeuObject();
		#region IInvalid ��Ա
		/// <summary>
		/// ��Ч��� false 0 ��Ч
		///			 true 1 ��Ч
		/// </summary>
		public bool IsInvalid
		{
			get
			{
				// TODO:  ��� DerateFee.IsInValid getter ʵ��
				return bIsInValid;
			}
			set
			{
				// TODO:  ��� DerateFee.IsInValid setter ʵ��
				bIsInValid=value;
			}
		}

		#endregion
		/// <summary>
		/// ��Ч��� false 0 ��Ч
		///			 true 1 ��Ч
		/// </summary>
		protected bool bIsInValid = false;
	}
}
