using System;
using System.Collections;

namespace neusoft.HISFC.Object.Operator
{
	/// <summary>
	/// ����������Ա������Ա��
	/// </summary>
	public class ArrangeRole:neusoft.neuFC.Object.neuObject
	{
		//��ɫ����
		private neusoft.HISFC.Object.Operator.ArrangeRoleType roleType; 
		//��ɫ��Ա
		private neusoft.HISFC.Object.RADT.Person person;
		/// <summary>
		/// ��ɫ״̬(Ŀǰֻ�������������)
		/// </summary>
		private neusoft.HISFC.Object.Operator.RoleOperKind roleOperKind;
		public ArrangeRole()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			roleType = new ArrangeRoleType();
			person = new neusoft.HISFC.Object.RADT.Person();
			roleOperKind = new RoleOperKind();
		}	
		//�������뵥���
		public string OperationNo = "";
		/// <summary>
		/// 0��ǰ����1�����¼ ��־
		/// </summary>
		public string ForeFlag = "0";			

		public ArrangeRoleType RoleType
		{
			get{ return roleType; }
			set{ roleType = value; }
		}

		public neusoft.HISFC.Object.RADT.Person Person
		{
			get{return person;}
			set{person = value;}
		}
		
		public RoleOperKind RoleOperKind
		{
			get{return roleOperKind;}
			set{roleOperKind = value;}
		}
	}
}
