using System;
using System.Collections;

namespace neusoft.HISFC.Object.Power
{
	/// <summary>
	/// User ��ժҪ˵����
	/// </summary>
	public class PowerUser : neusoft.neuFC.Object.neuObject       
	{
		public PowerUser()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			
			//

			Department = new neusoft.neuFC.Object.neuObject();
			GrantDepartment = new neusoft.neuFC.Object.neuObject();

			Department.ID = "";
			Department.Name = "";
		}

		//ID;
		//Name;

		/// <summary>
		/// Ȩ�޲���
		/// </summary>
		public neuFC.Object.neuObject Department;

		public neuFC.Object.neuObject GrantDepartment;


		private IList powerDetails ;
		private IList roleDetails;


		public string PowerClass1 ;
		public string PowerClass2 ;
		public string PowerClass3 ;


		/// <summary>
		/// ��Ա����չȨ��
		/// </summary>
		public IList PowerDetails
		{
			get
			{
				return this.powerDetails;
			}
			set
			{
				this.powerDetails = value;
			}
		}


		public IList RoleDetails
		{
			get
			{
				return this.roleDetails;
			}
			set
			{
				this.roleDetails = value;
			}
		}
	}
}
