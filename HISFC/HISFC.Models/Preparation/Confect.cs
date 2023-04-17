using System;

namespace FS.HISFC.Models.Preparation
{
	/// <summary>
	/// Confect<br></br>
	/// [��������: ������]<br></br>
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
    public class Confect:PPRBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Confect()
		{
		}


		#region ����
		/// <summary>
		/// ���ƺ���У�� ҩ����ƽ 0 ���� 1 Ч����
		/// </summary>
		private string scaleFlag;
		/// <summary>
		/// ���ƺ���У�� ���� 0 ���� 1 Ч����
		/// </summary>
		private string stetlyardFlag;
		/// <summary>
		/// ���Ƹ�����
		/// </summary>
		private string checkOper;
		/// <summary>
		/// ����--�ˣ�����
		/// </summary>
		private FS.HISFC.Models.Base.OperEnvironment confectEnv = new FS.HISFC.Models.Base.OperEnvironment();


		#endregion

		#region  ����
		/// <summary>
		/// ���ƺ���У�� ҩ����ƽ 0 ���� 1 Ч����
		/// </summary>		
		public string ScaleFlag
		{
			get
			{
				return this.scaleFlag;
			}
			set
			{
				this.scaleFlag = value;
			}
		}
		/// <summary>
		/// ���ƺ���У�� ���� 0 ���� 1 Ч����
		/// </summary>
		public string StetlyardFlag
		{
			get
			{
				return this.stetlyardFlag;
			}
			set
			{
				this.stetlyardFlag = value;
			}
		}

		/// <summary>
		/// ���Ƹ�����
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
		/// ����--�ˣ�����
		/// </summary>
		public FS.HISFC.Models.Base.OperEnvironment ConfectEnv
		{
			get
			{
				return this.confectEnv;
			}
			set
			{
				this.confectEnv = value;
			}
		}

		#endregion

		#region  ����
		/// <summary>
		/// ���ƶ���
		/// </summary>
		/// <returns>Confect</returns>
		public new Confect Clone()
		{
			Confect confect = base.Clone() as Confect;
			confect.confectEnv = this.confectEnv.Clone();
			return confect;
		}
		#endregion
		
		#region ���ڵ�����
		/// <summary>
		/// ������
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ConfectEnv", true)]
		public string ConfectOper;
		/// <summary>
		/// ����ʱ��
		/// </summary>
		[System.Obsolete("�Ѿ����ڣ�ʹ��ConfectEnv", true)]
		public DateTime ConfectDate;
		#endregion
	}
}
