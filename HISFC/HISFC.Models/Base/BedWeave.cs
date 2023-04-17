namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	/// BedWeave<br></br>
	/// [��������: ��λʵ��]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-08-28]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class BedWeave : Neusoft.NFC.Object.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public BedWeave()
		{
		}


		#region ����
		/// <summary>
		/// ö��
		/// </summary>
		private BedAttribute myID; 

		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;

		#endregion

		#region Enum
		/// <summary>
		/// ����״̬
		/// </summary>
		#endregion

		#region ����

		/// <summary>
		/// ����ID
		/// </summary>
		public new System.Object ID
		{
			get
			{
				return this.myID;
			}
			set
			{
				try
				{
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch
				{}
				base.ID = this.myID.ToString();
			}
		}

		/// <summary>
		/// ���ر�������
		/// </summary>
		public new string Name
		{
			get
			{
				string strBedWeave;
				switch ((int)this.ID)
				{
					case 0:
						strBedWeave= "������";
						break;
					case 1:
						strBedWeave="������";
						break;
					case 2:
						strBedWeave="�Ӵ�";
						break;
					case 3:
						strBedWeave="��ͥ����";
						break;
					default:
						strBedWeave="������";
						break;
				}
				base.Name=strBedWeave;
				return	strBedWeave;
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

			base.Dispose (isDisposing);

			this.alreadyDisposed = true;
		}
		#endregion

		#region ��¡
		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>Bed��ʵ��</returns>
		public new BedWeave Clone()
		{
			return base.Clone() as BedWeave;
		}
		#endregion


		/// <summary>
		/// �������Ƶõ�ID
		/// </summary>
		/// <param name="Name">����</param>
		/// <returns>ID</returns>
		public BedAttribute GetIDFromName(string Name)
		{
			BedAttribute c=new BedAttribute();
			for(int i = 0;i < 100 ;i++)
			{
				c=(BedAttribute)i;
				if(c.ToString()==Name) return c;
			}
			return (BedAttribute)int.Parse(Name);
		}

		/// <summary>
		/// ��ñ�������ȫ���б�
		/// </summary>
		/// <returns>ArrayList(BedWeave)</returns>
		public static System.Collections.ArrayList List()
		{
			BedWeave aBedWeave;
			System.Collections.ArrayList alReturn=new System.Collections.ArrayList();
			int i;
			for(i = 0;i <=3 ;i++)
			{
				aBedWeave = new BedWeave();
				aBedWeave.ID = (BedAttribute)i;
				aBedWeave.Memo =i.ToString();
				alReturn.Add(aBedWeave);
			}
			return alReturn;
		}
		#endregion

		

	}


	public enum BedAttribute {

		/// <summary>
		/// ������
		/// </summary>
		I,
		/// <summary>
		/// ������
		/// </summary>
		O,
		/// <summary>
		/// �Ӵ�
		/// </summary>
		A,
		/// <summary>
		/// ��ͥ����
		/// </summary>
		F
	}	

}
