using System;
using System.Collections;
using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Fee 
{
	public class ApplyReturn: Neusoft.NFC.Object.NeuObject 
	{
		private System.String myBillCode ;
		private System.String myInpatientNo ;
		private System.Boolean isBaby;
		private Neusoft.NFC.Object.NeuObject myDept = new Neusoft.NFC.Object.NeuObject();
		private Neusoft.NFC.Object.NeuObject myNurseCellCode = new Neusoft.NFC.Object.NeuObject();
		private System.String myDrugFlag ;
		private Neusoft.HISFC.Object.Base.Item item = new Neusoft.HISFC.Object.Base.Item();
		private System.Decimal myDays ;
		private System.String myPriceUnit ;
		private System.String myExecDpcd ;
		private System.String myOperCode ;
		private System.DateTime myOperDate ;
		private System.String myOperDpcd ;
		private System.String myRecipeNo ;
		private System.Int32 mySequenceNo ;
		private string myBillNo;
		private System.String myConfirmFlag ;
		private System.String myConfirmDpcd ;
		private System.String myConfirmCode ;
		private System.DateTime myConfirmDate ;
		private System.String myChargeFlag ;
		private System.String myChargeCode ;
		private System.DateTime myChargeDate ;
		private string extFlag3 ; //  1 ��װ ��λ 0, ��С��λ    

		/// <summary>
		/// �˷�����ʵ��
		/// ID    ������ˮ��
		/// Name  ��������
		/// </summary>
		public ApplyReturn() 
		{
			// TODO: �ڴ˴���ӹ��캯���߼�
		}


		/// <summary>
		/// ���뵥�ݺ�
		/// </summary>
		public System.String BillCode 
		{
			get{ return this.myBillCode; }
			set{ this.myBillCode = value; }
		}


		/// <summary>
		/// סԺ��ˮ��
		/// </summary>
		public System.String InpatientNo 
		{
			get{ return this.myInpatientNo; }
			set{ this.myInpatientNo = value; }
		}


		/// <summary>
		/// Ӥ�����
		/// </summary>
		public System.Boolean IsBaby 
		{
			get{ return this.isBaby; }
			set{ this.isBaby = value; }
		}


		/// <summary>
		/// �������ڿ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Dept 
		{
			get{ return this.myDept; }
			set{ this.myDept = value; }
		}


		/// <summary>
		/// ���ڲ���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject NurseCellCode 
		{
			get{ return this.myNurseCellCode; }
			set{ this.myNurseCellCode = value; }
		}


		/// <summary>
		/// ҩƷ��־,1ҩƷ/2��ҩ
		/// </summary>
		public System.String DrugFlag 
		{
			get{ return this.myDrugFlag; }
			set{ this.myDrugFlag = value; }
		}


		/// <summary>
		/// �շ���Ŀ��Ϣ(ҩƷ/��ҩƷ)
		/// </summary>
		public Neusoft.HISFC.Object.Base.Item Item 
		{
			get{return this.item;}
			set{this.item = value;}
		}


		/// <summary>
		/// ����
		/// </summary>
		public System.Decimal Days 
		{
			get{ return this.myDays; }
			set{ this.myDays = value; }
		}


		/// <summary>
		/// �Ƽ۵�λ
		/// </summary>
		public System.String PriceUnit 
		{
			get{ return this.myPriceUnit; }
			set{ this.myPriceUnit = value; }
		}


		/// <summary>
		/// ִ�п���
		/// </summary>
		public System.String ExecDpcd 
		{
			get{ return this.myExecDpcd; }
			set{ this.myExecDpcd = value; }
		}


		/// <summary>
		/// ����Ա����
		/// </summary>
		public System.String OperCode 
		{
			get{ return this.myOperCode; }
			set{ this.myOperCode = value; }
		}


		/// <summary>
		/// ����ʱ��
		/// </summary>
		public System.DateTime OperDate 
		{
			get{ return this.myOperDate; }
			set{ this.myOperDate = value; }
		}


		/// <summary>
		/// ����Ա���ڿ���
		/// </summary>
		public System.String OperDpcd 
		{
			get{ return this.myOperDpcd; }
			set{ this.myOperDpcd = value; }
		}


		/// <summary>
		/// ��Ӧ�շ���ϸ������
		/// </summary>
		public System.String RecipeNo 
		{
			get{ return this.myRecipeNo; }
			set{ this.myRecipeNo = value; }
		}


		/// <summary>
		/// ��Ӧ��������ˮ��
		/// </summary>
		public System.Int32 SequenceNo 
		{
			get{ return this.mySequenceNo; }
			set{ this.mySequenceNo = value; }
		}

		
		/// <summary>
		/// �˷ѵ��ݺ� ����ϵͳ�б����˷ѵķ�Ʊ��
		/// </summary>
		public string BillNo
		{
			get
			{
				return this.myBillNo;
			}
			set
			{
				this.myBillNo = value;
			}
		}

		/// <summary>
		/// ��ҩȷ�ϱ�ʶ 0δȷ��/1ȷ��
		/// </summary>
		public System.String ConfirmFlag 
		{
			get{ return this.myConfirmFlag; }
			set{ this.myConfirmFlag = value; }
		}


		/// <summary>
		/// ȷ�Ͽ��Ҵ���
		/// </summary>
		public System.String ConfirmDpcd 
		{
			get{ return this.myConfirmDpcd; }
			set{ this.myConfirmDpcd = value; }
		}


		/// <summary>
		/// ȷ���˱���
		/// </summary>
		public System.String ConfirmCode 
		{
			get{ return this.myConfirmCode; }
			set{ this.myConfirmCode = value; }
		}


		/// <summary>
		/// ȷ��ʱ��
		/// </summary>
		public System.DateTime ConfirmDate 
		{
			get{ return this.myConfirmDate; }
			set{ this.myConfirmDate = value; }
		}


		/// <summary>
		/// �˷ѱ�ʶ 0δ�˷�/1�˷�
		/// </summary>
		public System.String ChargeFlag 
		{
			get{ return this.myChargeFlag; }
			set{ this.myChargeFlag = value; }
		}


		/// <summary>
		/// �˷�ȷ����
		/// </summary>
		public System.String ChargeCode 
		{
			get{ return this.myChargeCode; }
			set{ this.myChargeCode = value; }
		}


		/// <summary>
		/// �˷�ȷ��ʱ��
		/// </summary>
		public System.DateTime ChargeDate 
		{
			get{ return this.myChargeDate; }
			set{ this.myChargeDate = value; }
		}

		/// <summary>
		/// ��װ ��λ 0, ��С��λ
		/// </summary>
		public string ExtFlage3
		{
			get
			{
				return extFlag3;
			}
			set
			{
				extFlag3 = value;
			}
		}
	}
}