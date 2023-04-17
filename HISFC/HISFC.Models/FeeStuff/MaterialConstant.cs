using System;

namespace FS.HISFC.Models.FeeStuff
{
	/// <summary>
	/// [��������: ���ʳ���]
	/// [�� �� ��: �]
	/// [����ʱ��: 2007-03-12]
	/// </summary>
    [Serializable]
	public class MaterialConstant:FS.HISFC.Models.Base.Spell
	{
		public MaterialConstant()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
#region ��

		/// <summary>
		/// ���������
		/// </summary>
		private string tableCode;

		/// <summary>
		/// ����Ա
		/// </summary>
		private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();
		
		/// <summary>
		/// ����ʱ��
		/// </summary>
		private DateTime operTime;

		/// <summary>
		/// �ϼ�����
		/// </summary>
		private string parentCode;

#endregion

#region ����

		/// <summary>
		/// ���������
		/// </summary>
		public string TableCode
		{
			get
			{
				return this.tableCode;
			}
			set
			{
				this.tableCode = value;
			}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		public FS.FrameWork.Models.NeuObject Oper 
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

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperTime
		{
			get
			{
				return this.operTime;
			}
			set
			{
				this.operTime = value;
			}
		}

		/// <summary>
		/// �ϼ�����
		/// </summary>
		public string ParentCode 
		{
			get
			{
				return this.parentCode;
			}
			set
			{
				this.parentCode = value;
			}
		}

#endregion

#region ����
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ؿ�¡���MaterialConstantʵ�� ʧ�ܷ���null</returns>
		public new MaterialConstant Clone()
		{

			MaterialConstant materialConstant = base.Clone() as MaterialConstant;

			materialConstant.Oper = this.Oper.Clone();

			return materialConstant;
		}
#endregion
	}
}
