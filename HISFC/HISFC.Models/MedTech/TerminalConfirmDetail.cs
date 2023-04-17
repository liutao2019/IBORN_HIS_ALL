
/*----------------------------------------------------------------
            // Copyright (C) ������������ɷ����޹�˾
            // ��Ȩ���С� 
            //
            // �ļ�����			TerminalConfirmDetail.cs
            // �ļ�����������	ҽ���ն�ȷ��ҵ����ϸʵ�����
            //
            // 
            // ������ʶ��		2006-6-21
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/

using neusoft.neuFC.Object;
namespace neusoft.HISFC.Object.MedTech
{
	/// <summary>
	/// �ն�ȷ��ҵ����ϸ
	/// </summary>
	public class TerminalConfirmDetail : neusoft.neuFC.Object.neuObject
	{
		public TerminalConfirmDetail()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���뵥��Ϣ
		/// </summary>
		TerminalApply apply = new TerminalApply();
		/// <summary>
		/// ���뵥��Ϣ
		/// </summary>
		public TerminalApply Apply
		{
			get
			{
				return this.apply;
			}
			set
			{
				this.apply = value;
			}
		}
		
		/// <summary>
		/// ҽԺ�������
		/// </summary>
		neusoft.neuFC.Object.neuObject hospital = new neuObject();
		/// <summary>
		/// ҽԺ�������
		/// </summary>
		public neusoft.neuFC.Object.neuObject Hospital
		{
			get
			{
				return this.hospital;
			}
			set
			{
				this.hospital = value;
			}
		}
		/// <summary>
		/// ��¼��ˮ��
		/// </summary>
		int sequence = 0;
		/// <summary>
		/// ��¼��ˮ��
		/// </summary>
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				this.sequence = value;
			}
		}
		/// <summary>
		/// ʣ������
		/// </summary>
		decimal freeCount = 0m;
		/// <summary>
		/// ʣ������
		/// </summary>
		public decimal FreeCount
		{
			get
			{
				return this.freeCount;
			}
			set
			{
				this.freeCount = value;
			}
		}
		/// <summary>
		/// ״̬
		/// </summary>
		neusoft.neuFC.Object.neuObject status = new neuObject();
		/// <summary>
		/// ״̬
		/// </summary>
		public neusoft.neuFC.Object.neuObject Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		public new TerminalConfirmDetail Clone()
		{
			TerminalConfirmDetail detail = base.Clone() as TerminalConfirmDetail;
			detail.Apply = this.Apply.Clone();
			detail.Hospital = this.Hospital.Clone();
			detail.Status = this.Status.Clone();
			return detail;
		}
	}
}
