using System;

namespace FS.HISFC.Models.Fee.Useless
{
	/// <summary>
	/// �������� ���ò�����
	/// TODO:
	/// ��Ҫ�Ƶ�Local
	/// </summary>
    /// 
    [System.Serializable]
	public class OldFee: FS.FrameWork.Models.NeuObject
	{
		public OldFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//סԺ������Ϣ
		public FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
		public decimal COST;//           NUMBER(8,2)   Y               סԺ�����ܶ�               
		public decimal PUBCOST;//       NUMBER(8,2)   Y                סԺ�����ܽ��             
		public decimal PAYCOST;//       NUMBER(8,2)   Y                סԺ�Ը����               
		public decimal OWNCOST;//       NUMBER(8,2)   Y                סԺ�Էѽ��
		public decimal BEDFEE ;//       NUMBER(8,2)   Y                ��λ��                     
		public decimal DIAGFEE;//       NUMBER(8,2)   Y                ���                       
		public decimal CHECKFEE;//      NUMBER(8,2)   Y                ����                     
		public decimal CUREFEE ;//      NUMBER(8,2)   Y                ���Ʒ�                     
		public decimal NURSEFEE;//      NUMBER(8,2)   Y                �����                     
		public decimal OPERATIONFEE;//  NUMBER(8,2)   Y                ������                     
		public decimal ASSAYFEE;//      NUMBER(8,2)   Y                �����                     
		public decimal DRUGFEE;//       NUMBER(8,2)   Y                ҩ��                       
		public decimal OTHERFEE;//      NUMBER(8,2)   Y                ������                     
		public decimal CLINICFEE;//     NUMBER(8,2)   Y                ��������ܷ���     
        public decimal CLINICPAYFEE ;//NUMBER(8,2)   Y                 �����Ը��ܷ���    
		public decimal SPELLFEE;//      NUMBER(8,2)   Y                ��������                 
		public decimal STANDFEE;//      NUMBER(8,2)   Y                �걨����׼               
		public System.DateTime  DECLAREDATE;//   DATE          Y                �걨�·�
		public string MARK;//           VARCHAR2(200) Y                ��ע
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string ItemType;
		/// <summary>
		/// ������Ŀ���
		/// </summary>
		public string SpellItemType;
	}
}
