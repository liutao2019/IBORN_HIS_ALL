using System;

namespace Neusoft.HISFC.Object.Registration
{
	/// <summary>
	/// �Ű�ʵ��
	/// </summary>
	public class Tabulation:Neusoft.NFC.Object.NeuObject
	{
		public Tabulation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ˽�б���
		//Memo,ID
		private string arrangeid;
		private string emplid;
		private string deptid;
		private DateTime workdate;
		private string operid;
		private DateTime operdate;
		#endregion

		#region ���б���
		/// <summary>
		/// �Ű����
		/// </summary>
		public string arrangeID
		{
			get{return arrangeid;}
			set{arrangeid=value;}
		}
		/// <summary>
		/// Ա������
		/// </summary>
		public string EmplID
		{
			get{return emplid;}
			set{emplid=value;}
		}
		
		/// <summary>
		/// Ա������
		/// </summary>
		public string DeptID
		{
			get{return deptid;}
			set{deptid=value;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public WorkType Kind=new WorkType();
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime Workdate
		{
			get{return workdate;}
			set{workdate=value;}
		}
		/// <summary>
		/// ��ʾ˳��
		/// </summary>
		public int SortID = 0;
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperID
		{
			get{return operid;}
			set{operid=value;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get{return operdate;}
			set{operdate=value;}
		}
		#endregion
		
	}
	/// <summary>
	/// �Ű�����ʵ��
	/// </summary>
	public class WorkType:Neusoft.HISFC.Object.Base.Spell
	{		
		public WorkType()
		{
		}
		//ID,Name,Memo
		
		#region ˽�б���
		//private string spell;//ƴ����
		private string class_code;//���ڴ������
		private string sign;//���
		private DateTime begin;//���ڿ�ʼʱ��
		private DateTime end;//���ڽ���ʱ��
		private decimal quotiety;//�ۿ�ϵ��
		private decimal positivedays;//����Ȩֵ
		private decimal minusdays;//ȱ��Ȩֵ
		private bool isvalid;//�Ƿ���Ч
		private string operid;//����Ա
		private DateTime operdate;//��������
		#endregion
		
		#region ���б���

		
		/// <summary>
		/// ���ڴ������
		/// </summary>
		public string ClassID
		{
			get{return class_code;}
			set{class_code=value;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string Sign
		{
			get{return sign;}
			set{sign=value;}
		}
		/// <summary>
		/// ���ڿ�ʼʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get{return begin;}
			set{begin=value;}
		}
		/// <summary>
		/// ���ڽ���ʱ��
		/// </summary>
		public DateTime EndTime
		{
			get{return end;}
			set{end=value;}
		}
		/// <summary>
		/// �ۿ�ϵ��
		/// </summary>
		public decimal Quotiety
		{
			get{return quotiety;}
			set{quotiety=value;}
		}
		/// <summary>
		/// ����Ȩֵ
		/// </summary>
		public decimal PositiveDays
		{
			get{return positivedays;}
			set{positivedays=value;}
		}
		/// <summary>
		/// ȱ��Ȩֵ
		/// </summary>
		public decimal MinusDays
		{
			get{return minusdays;}
			set{minusdays=value;}
		}
		/// <summary>
		/// ǰ��ɫ
		/// </summary>
		public string ForeColor="";
		/// <summary>
		/// �Ƿ���Ч
		/// </summary>
		public bool Isvalid
		{
			get{return isvalid;}
			set{isvalid=value;}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperID
		{
			get{return operid;}
			set{operid=value;}
		}
		public DateTime OperDate
		{
			get{return operdate;}
			set{operdate=value;}
		}
		#endregion 

		
	}
	public class TabularType:Neusoft.NFC.Object.NeuObject
	{
		/* by MaoKb
		 *05.7.26*/

		public TabularType()
		{
		}
        
		#region ˽�б���
		private string empcode;      //1.Ա������
		private string deptcode;     //2.���Ҵ���
		private DateTime workdate;   //3.��������
		//private string name;         //4.������������
		private string classcode;    //5.�Ű�������
		private DateTime begin;      //6.���ڿ�ʼʱ��
		private DateTime end;        //7.�ǽ���ʱ��
		private decimal positivedays;//8.����Ȩֵ
		private decimal minusdays;   //9.ȱ��Ȩֵ
		private string opercode;     //10.�Ű��˴���
		private DateTime operdate;   //11.�Ű�����
		private bool ischecked;      //12.�Ƿ����
		private string remark;       //13.��ע
		#endregion

		#region ���б���
		/// <summary>
		/// Ա������
		/// </summary>
		public string EmpCode
		{
			get{return empcode;}
			set{empcode=value;}
		}
		public string DeptCode
		{
			get{return deptcode;}
			set{deptcode=value;}
		}
		public DateTime WorkDate
		{
			get{return workdate;}
			set{workdate=value;}
		}

		public string ClassCode
		{
			get{return classcode;}
			set{classcode=value;}
		}
		public DateTime Begin
		{
			get{return begin;}
			set{begin=value;}
		}
		public DateTime End
		{
			get{return end;}
			set{end=value;}
		}
		public decimal PositiveDays
		{
			get{return positivedays;}
			set{positivedays=value;}
		}
		public decimal MinusDays
		{
			get{return minusdays;}
			set{minusdays=value;}
		}
		public string OperCode
		{
			get{return opercode;}
			set{opercode=value;}
		}
		public DateTime OperDate
		{
			get{return operdate;}
			set{operdate=value;}
		}
		public bool IsChecked
		{
			get{return ischecked;}
			set{ischecked=value;}
		}
		public string Remark
		{
			get{return remark;}
			set{remark=value;}
		}
		#endregion
	}

}
