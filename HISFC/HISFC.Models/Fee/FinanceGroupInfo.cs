using System;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// FinanceGroupInfo ��ժҪ˵����
	/// </summary>
	public class FinanceGroupInfo  : NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.ISort
	{
		public FinanceGroupInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

//FINAGRP_CODE VARCHAR2(6)                   ���������                      
//FINAGRP_NAME VARCHAR2(20)                  ����������                      
//EMPL_CODE    VARCHAR2(6)                   Ա������                        
//VALID_STATE  VARCHAR2(1)           '0'     ��Ч��״̬ 0 ���� 1 ͣ�� 2 ���� 
//SORT_ID      NUMBER       Y                ˳���  

		//public NFC.Object.NeuObject Employee = new Neusoft.NFC.Object.NeuObject();
		public string PKId;
		public string EmployeeID;
		public string EmployeeName;	   
		public string ValidState;
		#region ISort ��Ա

		private int sortId;
		public int SortID
		{
			get
			{
				// TODO:  ��� FinanceGroupInfo.SortID getter ʵ��
				return sortId;
			}
			set
			{
				// TODO:  ��� FinanceGroupInfo.SortID setter ʵ��
				sortId = value;
			}
		}

		#endregion
	}
}
