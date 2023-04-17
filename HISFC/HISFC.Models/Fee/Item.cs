using System;
using System.Data;

using System.Collections;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ��Ŀ ��ҩƷ��̳���Neusoft.HISFC.Object.Item written by zhouxs 
	/// 2004-11-24 
	/// <br><a href="">Item</a></br>
	/// </summary>
	public class Item:Neusoft.HISFC.Object.Base.Item
	{
		/// <summary>
		/// ��Ŀ�� ��Ŀ ��ҩƷ/��
		/// ID			��ҩƷ����  
		/// Name		��ҩƷ����  
		/// SysClass	ϵͳ���
		/// MinFee		��С���ô��� 
		/// UserCode   ������
		/// SpellCode   ƴ����
		/// WbCode      ���
		/// GbCode      ����
		/// NationCode  ���ʱ�׼����
		/// Price       ����
		/// PriceUnit	��λ
		/// EmcRate     ����ӳɱ���                       
		/// Family      �ƻ��������
		/// Special     �ض�������Ŀ
		/// ItemGrade   ������
		/// ConfirmFlag ȷ�ϱ�־
		/// ValidState  ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ���� 
		/// Specs	    ���
		/// ExecuteDept ִ�п���
		/// MachineNo	�豸���
		/// DefaultSampleĬ�ϼ�鲿λ
		/// OperateId   ��������
		/// OperateKind ��������
		/// OperateType ������ģ
		/// CollateFlag �Ƿ���������Ŀ��֮����  22  ��������  23 ������ģ
		/// Mark        �Ƿ���������Ŀ��֮����(1�У�0û��)
		/// OperCode    ����Ա
		/// OperDate    ����ʱ��
		/// </summary>
		public Item()
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
			this.isPharmacy = false;
		}
		/// <summary>
		/// ִ�п���
		/// </summary>
		private string exeDept;


		/// <summary>
		/// �������
		/// </summary>
		public decimal EmcRate;
		/// <summary>
		/// �ƻ��������
		/// </summary>
		public bool Family;
		///<summary>
		///ȷ�ϱ�־
		///</summary>
		public bool  ConfirmFlag;
		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ���� 
		/// </summary>
		public bool  ValidState;
		///<summary>
		///ִ�п���
		///</summary>
		public ArrayList ExecuteDepts = new  ArrayList();
		/// <summary>
		/// �����ַ���
		/// </summary>
		public string ExecuteDept
		{
			get
			{
				return exeDept;
			}
			set
			{
				exeDept = value;
				string[] s =value.Split('|');
				this.ExecuteDepts.Clear();
				foreach(string temp in s)
				{
					ExecuteDepts.Add(temp);
				}

				//this.ExecuteDepts.CopyTo(s,0);
			}
		}
		
		///<summary>
		///�豸���
		///</summary>
		public ArrayList MachineNos = new ArrayList();
		/// <summary>
		/// ��������
		/// </summary>
		public string MachineNo
		{
			get
			{
				return "";
			}
			set
			{
				string[] s1=value.Split('|');
				this.MachineNos.Clear();
				this.MachineNos.CopyTo(s1,0);
			}
		}
		///<summary>
		///Ĭ�ϼ�鲿λ
		///</summary>
		public string  DefaultSample = "";
		///<summary>
		///�Ƿ���������֮����
		///</summary>
		///
		public bool  CollateFlag;
		/// <summary>
		/// ������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject  OperInfo= new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public Neusoft.NFC.Object.NeuObject  OperateInfo  = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// �������� ID �洢����  name �洢����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject OperateKind = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// ������ģ ID�洢���� name�洢����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject OperateType = new Neusoft.NFC.Object.NeuObject();
		/// <summary>
		/// �������� id�洢���� name �洢����
		/// </summary>
		public string DiscaseClass = "";
		/// <summary>
		/// ר������ 
		/// </summary>
		public string SpecalDept  = "";
		/// <summary>
		/// ֪��ͬ����
		/// </summary>
		public bool ConsentFlag ;
		/// <summary>
		///  ��ʷ�����
		/// </summary>
		public string Mark1 = "";
		/// <summary>
		/// ���Ҫ��  
		/// </summary>
		public string Mark2 = "";
		/// <summary>
		/// ע������           
		/// </summary>
		public string Mark3 = "";
		/// <summary>
		/// ������뵥����  
		/// </summary>
		public string Mark4 = "";
		/// <summary>
		/// �Ƿ���ҪԤԼ  1 ��Ҫ 0 ����Ҫ  
		/// </summary>
		public string NeedBespeak;
		/// <summary>
		/// ��Ŀ��Χ
		/// </summary>
		public string ItemArea = "";
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ItemNoArea = "";
		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public new Item  Clone()
		{
			return this.MemberwiseClone() as Item;
		}
	
	
	}
}