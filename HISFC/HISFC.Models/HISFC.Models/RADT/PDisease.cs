using System;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// PDisease <br></br>
	/// [��������: ���߼���ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2004-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2006-09-11'
	///		�޸�Ŀ��='�汾����'
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PDisease : FS.FrameWork.Models.NeuObject 
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public PDisease()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// �Ƿ����ش󼲲�
		/// </summary>
		private bool isMainDisease;

		/// <summary>
		/// �Ƿ����
		/// </summary>
		private bool isAlleray;

		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.HISFC.Models.RADT.Tend tend=new Tend();

		/// <summary>
        /// ������Ϣ{B005D949-B8CE-4d7f-98FF-4156AEE71CCC}
		/// </summary>
        //private FS.HISFC.Models.Medical.Allergy alleray = new FS.HISFC.Models.Medical.Allergy();

		#endregion

		#region ����

		/// <summary>
		/// �Ƿ����ش󼲲�
		/// </summary>
		public bool IsMainDisease
		{
			get
			{
				return this.isMainDisease;
			}
			set
			{
				this.isMainDisease = value;
			}
		}


		/// <summary>
		/// �Ƿ����
		/// </summary>
		public bool IsAlleray
		{
			get
			{
				return this.isAlleray;
			}
			set
			{
				this.isAlleray = value;
			}
		}


		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.RADT.Tend Tend
		{
			get
			{
				return this.tend;
			}
			set
			{
				this.tend = value;
			}
		}


		/// <summary>
        /// ������Ϣ{B005D949-B8CE-4d7f-98FF-4156AEE71CCC}
		/// </summary>
        //public FS.HISFC.Models.Medical.Allergy Alleray
        //{
        //    get
        //    {
        //        return this.alleray;
        //    }
        //    set
        //    {
        //        this.alleray = value;
        //    }
        //}

		#endregion

		#region ����

		/// <summary>
		/// �Ƿ����ش󼲲�
		/// </summary>
		[Obsolete("�Ѿ����ڣ�����ΪIsMainDisease",true)]
		public bool IsMainDesease;

		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new PDisease Clone()
		{
			PDisease pDisease = base.Clone() as PDisease;

			return pDisease;
		}

		#endregion
	}
}