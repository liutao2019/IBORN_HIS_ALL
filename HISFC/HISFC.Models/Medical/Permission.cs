using System;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.Medical
{
	/// <summary>
	/// [��������: ҽ�ƹ���Ȩ��ʵ�壬ר��Ϊҽ����]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class Permission:Neusoft.FrameWork.Models.NeuObject
	{
		public Permission()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected Neusoft.HISFC.Models.Base.Employee myPerson = new Employee();
		/// <summary>
		/// ��Ա
		/// </summary>
		public Neusoft.HISFC.Models.Base.Employee Person
		{
			get
			{
				if(myPerson == null) myPerson = new Employee();
				return myPerson;
			}
			set
			{
				myPerson = value;
				base.ID = myPerson.ID;
				base.Name  = myPerson.Name;
			}
		}
		/// <summary>
		/// ��Ա����
		/// </summary>
		public new string ID
		{
			get
			{
				return myPerson.ID;
			}
		}
		/// <summary>
		/// ��Ա����
		/// </summary>
		public new string Name
		{
			get
			{
				return myPerson.Name;
			}
		}
		protected CPermission myOrderPermission = new CPermission();
		protected CPermission myEMRPermission =new CPermission();
		protected CPermission myQCPermission = new CPermission();
		/// <summary>
		/// ҽ��Ȩ��
		/// </summary>
		public CPermission OrderPermission
		{
			get
			{

				return this.myOrderPermission;
			}
			set
			{
				this.myOrderPermission = value;
			}
		}
		/// <summary>
		/// ҽ����Ȩ��ʼʱ��
		/// </summary>
		public System.DateTime  DateBeginOrderPermission;
		/// <summary>
		/// ҽ����Ȩ����ʱ��
		/// </summary>
		public  System.DateTime DateEndOrderPermission;
		/// <summary>
		/// ����Ȩ��
		/// </summary>
		public CPermission EMRPermission
		{
			get
			{

				return this.myEMRPermission;
			}
			set
			{
				this.myEMRPermission = value;
			}
		}
		/// <summary>
		/// ָ��Ȩ��
		/// </summary>
		public CPermission QCPermission
		{
			get
			{

				return this.myQCPermission;
			}
			set
			{
				this.myQCPermission = value;
			}
		}
		/// <summary>
		/// �����˱���
		/// </summary>
		public string OperCode;
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate;
		
		/// <summary>
		/// ���ؿ�¡
		/// </summary>
		/// <returns></returns>
		public new Permission Clone()
		{
			Permission obj=base.Clone() as Permission;
			obj.Person = this.Person.Clone();
			obj.OrderPermission.Permission = this.OrderPermission.ToString();
			obj.EMRPermission.Permission = this.EMRPermission.ToString();
			obj.QCPermission.Permission = this.QCPermission.ToString();
			return obj;
		}

	}
	/// <summary>
	/// Ȩ����
	/// </summary>
	public class CPermission
	{
		public CPermission()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		protected string myPermission="";
		/// <summary>
		/// ��ǰȨ��
		/// </summary>
		public string Permission
		{
			set
			{
				myPermission = value;
			}
		}
		/// <summary>
		/// ���Ȩ��
		/// </summary>
		/// <param name="iVal"></param>
		/// <returns></returns>
		public bool GetOnePermission(object iVal)
		{
			int i = 0;
			try
			{
				i = Neusoft.FrameWork.Function.NConvert.ToInt32(iVal);
			}
			catch{return false;}
			try
			{
				if(this.myPermission.Length<i) return false;
                i = Neusoft.FrameWork.Function.NConvert.ToInt32(myPermission.Substring(i, 1));
                return Neusoft.FrameWork.Function.NConvert.ToBoolean(i);
			}
			catch{return false;}
		}

        /// <summary>
        /// ���Ȩ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetPermission(object obj)
        {
            int a = 0;
            try
            {
                a = Neusoft.FrameWork.Function.NConvert.ToInt32(obj);
            }
            catch { return -1; }
            try
            {
                if (this.myPermission.Length < a) return -1;
                a = Neusoft.FrameWork.Function.NConvert.ToInt32(myPermission.Substring(a, 1));
                return a;
            }
            catch { return -1; }

        }

		/// <summary>
		/// ���ص�ǰȨ��
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.myPermission;
		}

	}

}
