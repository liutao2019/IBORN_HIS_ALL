using System;

namespace Neusoft.HISFC.Object.Pharmacy
{
	/// <summary>
	/// STORecipe ��ժҪ˵����
	/// �����ҩ����(��������)ʵ��
	/// </summary>
	public class STORecipe:Neusoft.NFC.Object.NeuObject
	{
		public STORecipe()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// ������
		/// </summary>
		public string RecipeNo;
		/// <summary>
		/// �����������(Ȩ������ Class3_Menaing_Code) ���� M1 ��ҩ M2 �ˡ���ҩ
		/// </summary>
		public string SystemType;
		/// <summary>
		/// �������� 1 ������ 2 ������
		/// </summary>
		public string TransType;
		/// <summary>
		/// ����״̬: 0����,1��ӡ,2��ҩ,3��ҩ,4��ҩ(����δ����ҩƷ����)
		/// </summary>
		public string RecipeState;
		/// <summary>
		/// �����
		/// </summary>
		public string ClinicCode;
		/// <summary>
		/// ��������
		/// </summary>
		public string CardNo;
		/// <summary>
		/// ��������
		/// </summary>
		public string PatientName;
		/// <summary>
		/// �Ա�
		/// </summary>
		public string SexCode;
		/// <summary>
		/// ����
		/// </summary>
		public DateTime Age;
		/// <summary>
		/// �������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject PayKind;
		/// <summary>
		/// ���߿���
		/// </summary>
		public Neusoft.NFC.Object.NeuObject PatientDept;
		/// <summary>
		/// �Һ�����
		/// </summary>
		public DateTime RegDate;
		/// <summary>
		/// ����ҽ��
		/// </summary>
		public Neusoft.NFC.Object.NeuObject Doct;
		/// <summary>
		/// ����ҽ������
		/// </summary>
		public Neusoft.NFC.Object.NeuObject DoctDept;
		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		public Neusoft.NFC.Object.NeuObject DrugTerminal;
		/// <summary>
		/// ��ҩ�ն�
		/// </summary>
		public Neusoft.NFC.Object.NeuObject SendTerminal;
		/// <summary>
		/// �շ��˱���(�������)
		/// </summary>
		public string FeeOper;
		/// <summary>
		/// �շ�ʱ��
		/// </summary>
		public DateTime FeeDate;
		/// <summary>
		/// ��Ʊ��
		/// </summary>
		public string InvoiceNo;
		/// <summary>
		/// �������
		/// </summary>
		public decimal Cost;
		/// <summary>
		/// ������ҩƷƷ������
		/// </summary>
		public decimal RecipeNum;
		/// <summary>
		/// ����ҩҩƷƷ������
		/// </summary>
		public decimal DrugedNum;
		/// <summary>
		/// ��ҩ��
		/// </summary>
		public string DrugedOper;
		/// <summary>
		/// ��ҩ����
		/// </summary>
		public string DrugedDept;
		/// <summary>
		/// ��ҩ����
		/// </summary>
		public DateTime DrugedDate;
		/// <summary>
		/// ��ҩ��
		/// </summary>
		public string SendOper;
		/// <summary>
		/// ��ҩ����
		/// </summary>
		public Neusoft.NFC.Object.NeuObject SendDept;
		/// <summary>
		/// ��ҩ����
		/// </summary>
		public DateTime SendDate;
		/// <summary>
		/// ��Ч״̬  0 ��Ч 1 ��Ч
		/// </summary>
		public string ValidState;
		/// <summary>
		/// ��/��ҩ״̬ 0 �� 1 ��
		/// </summary>
		public string ModifyState;
		/// <summary>
		/// ��ҩ��
		/// </summary>
		public string BackOper;
		/// <summary>
		/// ��ҩʱ��
		/// </summary>
		public DateTime BackDate;
		/// <summary>
		/// ȡ����
		/// </summary>
		public string CancelOper;
		/// <summary>
		/// ȡ��ʱ��
		/// </summary>
		public DateTime CancelDate;


	}
}
