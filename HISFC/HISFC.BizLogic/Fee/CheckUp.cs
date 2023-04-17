using System;
using System.Collections;
using neusoft.HISFC.Object.Check;
using neusoft.HISFC.Object.Fee.OutPatient;
using neusoft.neuFC.Object;

namespace neusoft.HISFC.Management.Fee
{
	/// <summary>
	/// Ϊ��컮���ṩ����
	/// 1�����桢�޸ĺ�ɾ����컮����ϸ
	/// </summary>
	public class CheckUp:neusoft.neuFC.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public CheckUp()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ��������
		/// <summary>
		/// �洢�������õķ���ֵ
		/// </summary>
		int intReturn = 0;
		/// <summary>
		/// ҩƷ��
		/// </summary>
		neusoft.HISFC.Object.Pharmacy.Item drug = new Object.Pharmacy.Item();
		/// <summary>
		/// ��ҩƷ������
		/// </summary>
		neusoft.HISFC.Management.Fee.Item undrugFunction = new Item();
		neusoft.HISFC.Object.Fee.Item undrug = new Object.Fee.Item();
		/// <summary>
		/// ���ﻼ����
		/// </summary>
		neusoft.HISFC.Management.Fee.OutPatient outPatient = new OutPatient();
		/// <summary>
		/// ҩƷ�ܹ�����
		/// </summary>
		decimal drugCost = 0;
		/// <summary>
		/// ��ҩƷ�ܷ���
		/// </summary>
		decimal undrugCost = 0;
		/// <summary>
		/// ���������ܽ��
		/// </summary>
		decimal priceCost = 0;
		/// <summary>
		/// ��ҩƷʵ�ս��
		/// </summary>
		decimal realCost = 0;
		string clinicNo = "";
		#endregion

		#region ���ô�����Ϣ
		/// <summary>
		/// ���ô�����Ϣ
		/// </summary>
		/// <param name="errorCode">�����</param>
		/// <param name="errorText">������Ϣ�ı�</param>
		public void SetError(string errorCode,string errorText)
		{
			this.ErrCode = errorCode;
			this.Err = errorText;
		}
		#endregion

		#region ��컮��
		/// <summary>
		/// ��컮��
		/// 1���γ�FeeItemList��MakeFeeItemList
		/// 2��������ĿӦ�ս�PAY_COST�����շ���Ŀ��������Ķ��ۣ�
		///		2.1 ҩƷ�������ۼ۸���䣨PHA_COM_BASEINFO��
		///		2.2 ��ҩƷ����ʣ����ƽ�����䣨FIN_COM_UNDRUGINFO��
		/// 3��������Ŀʵ�ս�OWN_COST����ʵ��Ӧ����ȡ���ߣ�
		///		3.1 ҩƷ�������ۼ۸���䣨PHA_COM_BASEINFO��
		///		3.2 ��ҩƷ����ʣ����ƽ�����䣨FIN_COM_UNDRUGINFO��
		///	4��������Ŀ����/�Żݽ�ECO_COST�����շ�ʱ����м��⣬��ôECO_COST=PAY_COST-OWN_COST��
		///		4.1 ҩƷû�м�����
		///		4.2 ��ҩƷ����ƽ������
		/// </summary>
		/// <param name="checkUpRegister">���˿��ࣨ����Ӧ�ս�</param>
		/// <param name="checkUpFeeList">FeeItemList���ArrayList</param>
		/// <param name="ownCost">�Ը���ʵ�գ�</param>
		/// <param name="shouldCost">����Ӧ�ս��</param>
		/// <param name="t">����</param>
		/// <returns>1���ɹ���-1��û�д���</returns>
		public int CheckUpFee(ChkRegister checkUpRegister,ArrayList checkUpFeeList,Decimal ownCost,Decimal shouldCost,
			neusoft.neuFC.Management.Transaction t)
		{
			this.drugCost = 0;
			this.undrugCost = 0;
			this.realCost = 0;
			this.priceCost = 0;
			clinicNo = "";
			undrugFunction.SetTrans(t.Trans);
			outPatient.SetTrans(t.Trans);
			int j = 0;
			// �γ�FeeItemList
//			j = MakeFeeItemList(ref checkUpFeeList,checkUpRegister,t);
//			if (j<=0)
//			{
//				this.SetError("200001","û�����ڻ��۵Ĵ�����");
//				return -1;
//			}
			if(checkUpFeeList == null)
			{
				return -1;
			}
			foreach(neusoft.HISFC.Object.Fee.OutPatient.FeeItemList obj in checkUpFeeList)
			{
				if(!obj.isPharmacy)
				{
					j++;
					undrugCost = obj.Price * obj.Amount;
				}
				else
				{
					drugCost = obj.Price * obj.Amount;
				}
			}
			// ������Ŀ�ܽ��
			priceCost = this.drugCost + this.undrugCost;
			// �����ҩƷ��Ŀ���۵�ʵ�ս��
			realCost = ownCost - drugCost;
			// ɾ������δ�շѵĴ�����ϸ
			try
			{
				this.outPatient.DeleteFeeDetail(this.clinicNo);
			}
			catch
			{
				this.SetError("200003","ɾ��������ϸʧ�ܣ�");
				t.Trans.Rollback();
				return -3;
			}
			// �����ҩƷ���ã����봦����
			foreach(neusoft.HISFC.Object.Fee.OutPatient.FeeItemList fee in checkUpFeeList)
			{
				if (fee.isPharmacy)
				{
					
				}
				else
				{
					fee.Cost.Pay_Cost = Decimal.Round(realCost/j,2); // �Ը����
					fee.Cost.Own_Cost = fee.Cost.Pay_Cost; // �ֽ�
					fee.Cost.Dereate_Cost = 0; // �Żݽ��
				}
				this.intReturn = this.outPatient.InsertFeeDetail(fee,true); // �����봦����ϸ
				if (this.intReturn<=0)
				{
					this.Err = "���봦����ʧ��:" + outPatient.Err;
					this.SetError("200002","���봦����ʧ�ܣ�");
					return -2;
				}
			}
			return 1;
		}
		#endregion

		#region �γ�FeeItemList
		/// <summary>
		/// �γ�����FeeItemList
		/// </summary>
		/// <param name="checkUpFeeList">��촦����ArrayList</param>
		/// <param name="checkUpRegister">���˿���</param>
		/// <param name="t">����</param>
		/// <returns>1���ɹ���-1��ʧ��</returns>
		private int MakeFeeItemList(ref ArrayList checkUpFeeList,ChkRegister checkUpRegister ,neusoft.neuFC.Management.Transaction t)
		{
//			int i = 1;
			int j = 0;
//			string recipeNO = ""; // ������
//			string clinicCode = ""; // �����
//			outPatient.SetTrans(t.Trans);
//            recipeNO = outPatient.GetRecipeNo(); // ȡ������
//			this.clinicNo = checkUpRegister.ChkClinicNo;
//
//			// ѭ��ÿһ������������ÿһ��������ϸ
//			foreach(neusoft.HISFC.Object.Fee.OutPatient.FeeItemList fee in checkUpFeeList)
//			{
//				fee.ID = fee.ID; // ��Ŀ���
//				fee.Name = fee.Name; // ��Ŀ����
//				fee.RecipeNo = recipeNO; // ������
//				fee.SeqNo = i; // ��������ˮ��
//				fee.TransType = neusoft.HISFC.Object.Base.TransTypes.Positive; // ��������
//				fee.ClinicCode = checkUpRegister.ChkClinicNo; // �����
//				fee.CardNo = checkUpRegister.PatientInfo.Patient.Card.ID; // ��������
//				fee.RegDate = checkUpRegister.CheckDate; // �Һ�����
//				fee.RegDeptInfo.ID = ""; // �Һſ��ұ��
//				fee.RegDeptInfo.Name = ""; // �Һſ�������
//				fee.DoctDeptInfo.ID = ""; // ����ҽ�����ڿ��ұ���
//				fee.Qty = fee.Qty; // ����
//				if (fee.Qty==0) fee.Qty = 1;
//				fee.Days = fee.Days; // ��ҩ����
//				fee.IsUrgent = fee.IsUrgent; // �Ƿ�Ӽ�
//				fee.LabTypeInfo = fee.LabTypeInfo; // ��������
//				fee.Cost.Pub_Cost = 0; // �ɱ������
//				fee.ExeDeptInfo = fee.ExeDeptInfo; // ִ�п���
//				fee.CombNo = fee.CombNo; // ��Ϻ�
//				fee.ChargeOperInfo = this.Operator; // ��������Ϣ
//				fee.ChargeDate = this.GetDateTimeFromSysDateTime(); // ����ʱ��
//				fee.PayType = neusoft.HISFC.Object.Base.PayTypes.Charged; // �շѱ�־
//				fee.CancelType = neusoft.HISFC.Object.Base.CancelTypes.Valid; // ״̬
//				fee.IsConfirm = false; // ȷ�ϱ�־
//				if (fee.isPharmacy) // ҩƷ
//				{
//					this.drug = this.drugFunction.GetItem(fee.ID);
//					fee.Name = drug.Name;
//					fee.Specs = drug.Specs; // ���
//					fee.IsSelfMade = drug.IsSelfMade; // ����ҩ��־
//					fee.DrugQualityInfo = new neuObject(); // ҩƷ����
//					fee.DoseInfo = drug.DosageForm; // ����
//					fee.Price = drug.Price; // �۸�
//					fee.FreqInfo = drug.Frequency; // Ƶ��
//					fee.UsageInfo = drug.Usage; // �÷�
//					fee.InjectCount = fee.InjectCount; // Ժע����
//					fee.MinFee = drug.MinFee; // ��С���ô���
//					fee.DoseOnce = drug.OnceDose; // ÿ������
//					fee.DoseUnit = drug.DoseUnit; // ÿ��������λ
//					fee.BaseDose = drug.BaseDose; // ��������
//					fee.PackQty = drug.PackQty; // ��װ����
//					fee.PriceUnit = drug.PriceUnit; // �Ƽ۵�λ
//					fee.Cost.Pay_Cost = fee.Price*fee.Qty; // �Ը����
//					fee.Cost.Own_Cost = fee.Price*fee.Qty; // �ֽ�
//					fee.CenterInfo = new Object.InterfaceSi.Item(); // ҽ��������Ϣ
//					fee.IsMainDrug = false; // ��ҩ��־
//					fee.Cost.Dereate_Cost = 0; // ������
//					drugCost = drugCost + fee.Cost.Own_Cost; // ҩƷ�ܽ��
//				}
//				else // ��ҩƷ
//				{
//					j++; // ���������ҩƷ������ϸ����
//					undrug = undrugFunction.GetItem(fee.ID);
//					fee.Name = undrug.Name; // ����
//					fee.Price = undrug.Price; // �۸�
//					fee.MinFee = undrug.MinFee; // ��С���ô���
//					fee.CheckBody = undrug.DefaultSample; // ����
//					fee.Cost.Pay_Cost = 0; // �Ը����
//					fee.Cost.Own_Cost = 0; // �ֽ�
//					fee.Cost.Dereate_Cost = 0; // ������
//					undrugCost = undrugCost + fee.Price*fee.Qty; // ��ҩƷ�ܽ��
//				}
//				i++;
//			}
			return j;
		}
		#endregion

		#region ����շѣ�����fin_opb_feedetail
		/// <summary>
		/// ����շѣ�����fin_opb_feedetail
		/// </summary>
		/// <param name="parm">�����ô��͵�SQL�еĲ���</param>
		/// <returns>1���ɹ���-1����ȡSQLʧ�ܣ�-2��SQL����ʽ��ʧ�ܣ�-3��SQL���ִ��ʧ��</returns>
		public int UpdateFeeDetail(string [] parm)
		{
			string SQL = "";
			// ���á���ȡSQL���
			if (this.Sql.GetSql("neusoft.HISFC.Management.Fee.CheckUp.UpdateFeeDetail",ref SQL)==-1)
			{
				this.Err = "��ȡ����SQL��neusoft.HISFC.Management.Fee.CheckUp.UpdateFeeDetailʧ��";
				return -1;
			}
			try
			{
				SQL = string.Format(SQL,parm);
			}
			catch(Exception e)
			{
				this.Err = "SQL���FORMATʧ��" + e.Message;
				return -2;
			}
			
			// ִ�и������
			try
			{
				this.ExecNoQuery(SQL);
			}
			catch(Exception e)
			{
				this.Err = "SQL���ִ��ʧ��" + e.Message;
				return -3;
			}
			return 1;
		}
		#endregion

		#region ����շѲ����շѽ���֧�������FIN_OPB_PAYMODE
		/// <summary>
		/// ����շѲ����շѽ���֧�������FIN_OPB_PAYMODE
		/// </summary>
		/// <param name="parms">�ֶ�ֵ</param>
		/// <returns>1���ɹ�</returns>
		public int InsertFinOpbPayMode(string [] parms)
		{
			string SQL = "";
			// ���á���ȡSQL���
			if (this.Sql.GetSql("neusoft.HISFC.Management.Fee.CheckUp.InsertFinOpbPayMode",ref SQL)==-1)
			{
				this.Err = "��ȡ����SQL��neusoft.HISFC.Management.Fee.CheckUp.InsertFinOpbPayModeʧ��";
				return -1;
			}
			try
			{
				SQL = string.Format(SQL,parms);
			}
			catch(Exception e)
			{
				this.Err = "SQL���FORMATʧ��" + e.Message;
				return -2;
			}
			
			// ִ�и������
			try
			{
				this.ExecNoQuery(SQL);
			}
			catch(Exception e)
			{
				this.Err = "SQL���ִ��ʧ��" + e.Message;
				return -3;
			}
			return 1;
		}
		#endregion

	}
}
