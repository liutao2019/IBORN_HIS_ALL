using System;

namespace FS.HISFC.Models.MedTech.Management
{
    /// <summary>
    /// [��������: ҽ�����Ա]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2006-12-03]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// 
    /// </summary>
    /// 
    [System.Serializable]
    public class Member : FS.HISFC.Models.Base.Spell
	{
		#region ˽�г�Ա
        private MedTech.Management.Group group;
		private string memberType;
		#endregion

		#region ����

		/// <summary>
		/// ҽ���飬��Ӧҽ��������
		/// </summary>
        public MedTech.Management.Group Group
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
		/// ��Ա���ͣ�Machine-�豸��User-��Ա
		/// </summary>
		public string MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}
		#endregion

		/// <summary>
		/// ���캯��
		/// </summary>
		public Member()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
