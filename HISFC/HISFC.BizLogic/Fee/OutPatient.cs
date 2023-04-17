using System;
using System.Collections;
using System.Data;
using FS.HISFC.Models;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// Outpatient<br></br>
	/// [��������: �������ҵ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-10-15]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Outpatient:FS.FrameWork.Management.Database
	{
		
		#region ˽�к���
		
		#region �ս���Ϣ����
			
		/// <summary>
		/// ����ս�ʵ��SQL
		/// </summary>
		/// <returns>�ɹ�: ��ѯ�ս��SELECT���� ʧ��: null</returns>
		private string GetSqlDayBalance()
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.Outpatient.GetSqlPayMode", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.Outpatient.GetSqlPayMode��SQL���";

				return null;
			}

			return sql;
		}
		
		/// <summary>
		/// ����SQL���Ͳ����б����ս���Ϣ����
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <param name="args">SQL������</param>
		/// <returns>�ɹ�:�ս���Ϣ���� ʧ�� null û�в��ҵ����� Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryDayBalanceBySql(string sql, params string[] args)
		{	
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}

			ArrayList dayBalances = new ArrayList();//�ս���Ϣ����
			
			DayBalance dayBalance ;//�ս���Ϣʵ��
			
			try
			{   //ѭ����ȡ����
				while (this.Reader.Read()) 
				{
					dayBalance = new DayBalance();
				
					dayBalance.ID = this.Reader[0].ToString();//�ս����
					dayBalance.BeginTime = NConvert.ToDateTime(this.Reader[1].ToString());//��ʼʱ��
					dayBalance.EndTime = NConvert.ToDateTime(this.Reader[2].ToString());//����ʱ��
					dayBalance.FT.TotCost = NConvert.ToDecimal(this.Reader[3].ToString());//������
					dayBalance.Oper.ID = this.Reader[4].ToString();//�տ�Ա����
					dayBalance.Oper.Name = this.Reader[5].ToString();//�տ�Ա����
					dayBalance.Oper.Memo = this.Reader[6].ToString();//��������
					dayBalance.User01 = this.Reader[7].ToString();//��������
					dayBalance.User02 = this.Reader[8].ToString();//��ע1
					dayBalance.User03 = this.Reader[9].ToString();//��ע2
					if (this.Reader[10].ToString() == "1")
					{
						dayBalance.IsAuditing = true;
					}
					else
					{
						dayBalance.IsAuditing = false;
					}
					dayBalance.AuditingOper.ID = this.Reader[11].ToString();//�˲���
					dayBalance.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[12].ToString());//�˲�����

					dayBalances.Add(dayBalance);
					
				}//ѭ������
				
				this.Reader.Close();

				return dayBalances;
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
		}

		/// <summary>
		/// ���֧�����ʵ������
		/// </summary>
		/// <param name="dayBalance"></param>
		/// <returns></returns>
		private string [] GetDayBalanceParams(DayBalance dayBalance)
		{
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new Department();
            }
            if (string.IsNullOrEmpty(dayBalance.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospital_id = dept.HospitalID;
                string hospital_name = dept.HospitalName;
                dayBalance.Hospital_id = hospital_id;
                dayBalance.Hospital_name = hospital_name;
            }
           
			string[] args = 
				{	
					dayBalance.ID ,//�ս����
					dayBalance.BeginTime.ToString(),//��ʼʱ��
					dayBalance.EndTime.ToString(),//����ʱ��
					dayBalance.FT.TotCost.ToString(),//������
					dayBalance.Oper.ID,//�տ�Ա����
					dayBalance.Oper.Name,//�տ�Ա����
					dayBalance.Oper.Memo,//��������
					dayBalance.User01,//;
					dayBalance.User02,//
					dayBalance.User03 ,
					dayBalance.IsAuditing ? "1" : "0",
					dayBalance.AuditingOper.ID,
					dayBalance.AuditingOper.OperTime.ToString(),
                   dayBalance.Hospital_id ,
                    dayBalance.Hospital_name //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
				};

			return args;
		}

		#endregion

		#region ����֧����Ϣ����

		/// <summary>
		/// ���֧��������sql���
		/// </summary>
		/// <returns>�ɹ�: ��ѯ֧����SELECT���� ʧ��: null</returns>
		private string GetBalancePaySelectSql()
		{
			string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.OutPatient.GetSqlPayMode.1", ref sql) == -1)
			{
                this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetSqlPayMode.1��SQL���";

				return null;
			}
			
			return sql;
		}

		/// <summary>
		/// ���֧����ʽ����
		/// </summary>
		/// <param name="sql">��ѯSQL���</param>
		/// <param name="args">SQL����</param>
		/// <returns>�ɹ�:���֧����ʽ���� ʧ��:null û�в��ҵ�����: Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryBalancePaysBySql(string sql, params string[] args) 
		{
			//ִ��SQL���
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}

			ArrayList balancePays = new ArrayList();//֧����ʽ��Ϣ
			BalancePay balancePay;//֧����ʽʵ��
			
			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read()) 
                {
					balancePay = new BalancePay();
				
					balancePay.Invoice.ID = this.Reader[0].ToString();//,	--		��Ʊ��
					if(this.Reader[1].ToString()=="2")//��������
					{
						balancePay.TransType = TransTypes.Negative;
					}
					else
					{
						balancePay.TransType = TransTypes.Positive;
					}
					balancePay.Squence = this.Reader[2].ToString();//������ˮ��
					balancePay.PayType.ID = this.Reader[3].ToString();//֧����ʽ
					balancePay.FT.TotCost = NConvert.ToDecimal(this.Reader[4].ToString());//Ӧ�����
					balancePay.FT.RealCost = NConvert.ToDecimal(this.Reader[5].ToString());//ʵ�����
					balancePay.Bank.ID = this.Reader[6].ToString();//���к�
					balancePay.Bank.Name = this.Reader[7].ToString();//��
					balancePay.Bank.Account = this.Reader[8].ToString();//�ʺ�
					balancePay.POSNO = this.Reader[9].ToString();//pos��
					balancePay.Bank.InvoiceNO = this.Reader[10].ToString();//֧Ʊ��
					balancePay.InputOper.ID = this.Reader[11].ToString();//������
                    balancePay.InputOper.OperTime = NConvert.ToDateTime(this.Reader[12].ToString());//����ʱ��
					//�Ƿ�˲�
					if (this.Reader[13].ToString() == "1")
					{
						balancePay.IsAuditing = true;
					}
					else
					{
						balancePay.IsAuditing = false;
					}	
					balancePay.AuditingOper.ID = this.Reader[14].ToString();
					balancePay.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[15].ToString());//���ʱ��
					balancePay.IsDayBalanced = NConvert.ToBoolean(this.Reader[16].ToString());//�Ƿ��ս�
					balancePay.BalanceOper.ID = this.Reader[18].ToString();//�ս���
					//�Ƿ����
					if(this.Reader[19].ToString()=="1")
					{
						balancePay.IsChecked = true;
					}
					else
					{
						balancePay.IsChecked = false;
					}
					balancePay.CheckOper.ID = this.Reader[20].ToString();//������
					balancePay.CheckOper.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());//����ʱ��
					balancePay.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[22].ToString());//�ս�ʱ��
                    balancePay.InvoiceCombNO = this.Reader[23].ToString();//��Ʊ���
                    balancePay.CancelType = (CancelTypes)NConvert.ToInt32(this.Reader[24].ToString());
                    if (this.Reader[25] != DBNull.Value) balancePay.IsEmpPay = NConvert.ToBoolean(this.Reader[25].ToString());
                    if (this.Reader[26] != DBNull.Value) balancePay.AccountNo = this.Reader[26].ToString();// {FE32F3CC-19B9-49f3-B073-D61235DB11B0}
                    if (this.Reader[27] != DBNull.Value) balancePay.AccountTypeCode = this.Reader[27].ToString();
					balancePays.Add(balancePay);
				}//ѭ������

				this.Reader.Close();

				return balancePays;
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
		}
		/// <summary>
		/// ���֧�����ʵ������
		/// </summary>
		/// <param name="balancePay"></param>
		/// <returns></returns>
		private string [] GetBalancePayParams(BalancePay balancePay)
		{
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            if (dept == null)
            {
                dept = new Department();
            }
            if (string.IsNullOrEmpty(balancePay.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospital_id = dept.HospitalID;
                string hospital_name = dept.HospitalName;
                balancePay.Hospital_id = hospital_id;
                balancePay.Hospital_name = hospital_name;
            }
            
			string[] args = 
				{	
					balancePay.Invoice.ID,
					((int)balancePay.TransType).ToString(),
					balancePay.Squence.ToString(),
					balancePay.PayType.ID.ToString(),
					balancePay.FT.TotCost.ToString(),
					balancePay.FT.RealCost.ToString(),
					balancePay.Bank.ID,
					balancePay.Bank.Name,
					balancePay.Bank.Account,
					balancePay.POSNO,
					balancePay.Bank.InvoiceNO,
					balancePay.InputOper.ID ,
					balancePay.InputOper.OperTime.ToString(),
					balancePay.IsAuditing?"1":"0",
					balancePay.AuditingOper.ID,
					balancePay.AuditingOper.OperTime.ToString(),
					NConvert.ToInt32(balancePay.IsDayBalanced).ToString(),
					"",
					balancePay.BalanceOper.ID,
					NConvert.ToInt32(balancePay.IsChecked).ToString(),
					balancePay.CheckOper.ID,//������
					balancePay.CheckOper.OperTime.ToString(),//����ʱ��
					balancePay.BalanceOper.OperTime.ToString(), //;//�ս�ʱ��
                    balancePay.InvoiceCombNO,
                    ((int)balancePay.CancelType).ToString(),
                    balancePay.Memo,
                    NConvert.ToInt32(balancePay.IsEmpPay).ToString(),// {FE32F3CC-19B9-49f3-B073-D61235DB11B0}
                    balancePay.AccountNo,
                    balancePay.AccountTypeCode,
                     balancePay.Hospital_id,
                     balancePay.Hospital_name //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
				};

			return args;
		}

		/// <summary>
		/// ����Where������������ѯ֧����Ϣ
		/// </summary>
		/// <param name="whereIndex">Where��������</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:֧����Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryBalancePays(string whereIndex, params string[] args)
		{
			string sql = string.Empty;//SELECT���
			string where = string.Empty;//WHERE���
			
			//���Where���
			if (this.Sql.GetCommonSql(whereIndex, ref where) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return null;
			}

			sql = this.GetBalancePaySelectSql();

			return this.QueryBalancePaysBySql(sql + " " + where, args);
		}

		#endregion

		#region ������ϸ����
		
		/// <summary>
		/// ��ô�����ϸ��sql���
		/// </summary>
		/// <returns>���ز�ѯ������ϸSQL���</returns>
		private string GetSqlFeeDetail() 
		{
			string sql = string.Empty;//��ѯSQL����SELECT����

			if (this.Sql.GetCommonSql("Fee.Item.GetFeeItem",ref sql) == -1)
			{
				this.Err = "û���ҵ�����ΪFee.Item.GetFeeItem��SQL���";

				return null;
			}

			return sql;
		}

        /// <summary>
        /// ��ô�����ϸ��sql���
        /// </summary>
        /// <returns>���ز�ѯ������ϸSQL���</returns>
        private string GetSqlFeeDetailAndFeeName()
        {
            string sql = string.Empty;//��ѯSQL����SELECT����

            if (this.Sql.GetCommonSql("Fee.Item.GetFeeItem1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����ΪFee.Item.GetFeeItem��SQL���";

                return null;
            }

            return sql;
        }

		/// <summary>
		/// ͨ��SQL����÷�����ϸ��Ϣ
		/// </summary>
		/// <param name="sql">SQL���</param>
		/// <param name="args">SQL����</param>
		/// <returns>�ɹ�:������ϸ���� ʧ��: null û�в��ҵ�����: Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryFeeDetailBySql(string sql, params string[] args) 
		{
			if(this.ExecQuery(sql, args) == -1)
			{
				return null;
			}
			
			ArrayList feeItemLists = new ArrayList();//������ϸ����
			FeeItemList feeItemList = null;//������ϸʵ��

			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read()) 
				{
					feeItemList = new FeeItemList();

                    //feeItemList.Item.IsPharmacy = NConvert.ToBoolean(this.Reader[11].ToString());
                    //feeItemList.User01
                    feeItemList.Item.ItemType = (EnumItemType)NConvert.ToInt32(this.Reader[11]);

                    //if (feeItemList.Item.IsPharmacy)
                    if(feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Pharmacy.Item();
                        feeItemList.Item.ItemType = EnumItemType.Drug;
                        //feeItemList.Item.IsPharmacy = true;
                    }
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                        //feeItemList.Item.IsPharmacy = false;
                        feeItemList.Item.ItemType = EnumItemType.UnDrug;
                    }
                    //���� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else
                    {
                        feeItemList.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                        feeItemList.Item.ItemType = EnumItemType.MatItem;

                    }

					feeItemList.RecipeNO = this.Reader[0].ToString();		
					feeItemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());
					if (this.Reader[2].ToString() == "1")
					{
						feeItemList.TransType = TransTypes.Positive;
					}
					else
					{
						feeItemList.TransType = TransTypes.Negative;
					}
					feeItemList.Patient.ID = this.Reader[3].ToString();
					feeItemList.Patient.PID.CardNO = this.Reader[4].ToString();	
					((Register)feeItemList.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[5].ToString());
					((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
					feeItemList.RecipeOper.ID = this.Reader[7].ToString();
                    ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID = this.Reader[7].ToString();
					feeItemList.RecipeOper.Dept.ID = this.Reader[8].ToString();
					feeItemList.Item.ID = this.Reader[9].ToString();
					feeItemList.Item.Name = this.Reader[10].ToString();
                    feeItemList.Item.Specs = this.Reader[12].ToString();

                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade = NConvert.ToBoolean(this.Reader[13].ToString());
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID = this.Reader[14].ToString();
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID = this.Reader[15].ToString();
                    }
					feeItemList.Item.MinFee.ID = this.Reader[16].ToString();
					feeItemList.Item.SysClass.ID = this.Reader[17].ToString();
					feeItemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());
					feeItemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());
					feeItemList.Days = NConvert.ToDecimal(this.Reader[20].ToString());
					feeItemList.Order.Frequency.ID = this.Reader[21].ToString();
					feeItemList.Order.Usage.ID = this.Reader[22].ToString();
					feeItemList.Order.Usage.Name = this.Reader[23].ToString();
					feeItemList.InjectCount = NConvert.ToInt32(this.Reader[24].ToString());
					feeItemList.IsUrgent = NConvert.ToBoolean(this.Reader[25].ToString());
					feeItemList.Order.Sample.ID = this.Reader[26].ToString();
					feeItemList.Order.CheckPartRecord = this.Reader[27].ToString();
					feeItemList.Order.DoseOnce = NConvert.ToDecimal(this.Reader[28].ToString());
					feeItemList.Order.DoseUnit = this.Reader[29].ToString();
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose = NConvert.ToDecimal(this.Reader[30].ToString());
                    }
					feeItemList.Item.PackQty = NConvert.ToDecimal(this.Reader[31].ToString());
					feeItemList.Item.PriceUnit = this.Reader[32].ToString();
					feeItemList.FT.PubCost = NConvert.ToDecimal(this.Reader[33].ToString());
					feeItemList.FT.PayCost = NConvert.ToDecimal(this.Reader[34].ToString());
					feeItemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[35].ToString());
					feeItemList.ExecOper.Dept.ID = this.Reader[36].ToString();
					feeItemList.ExecOper.Dept.Name = this.Reader[37].ToString();
					feeItemList.Compare.CenterItem.ID = this.Reader[38].ToString();
					feeItemList.Compare.CenterItem.ItemGrade = this.Reader[39].ToString();
					feeItemList.Order.Combo.IsMainDrug = NConvert.ToBoolean(this.Reader[40].ToString());
					feeItemList.Order.Combo.ID = this.Reader[41].ToString();
					feeItemList.ChargeOper.ID = this.Reader[42].ToString();
					feeItemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());
					feeItemList.PayType = (PayTypes)(NConvert.ToInt32(this.Reader[44].ToString()));
					feeItemList.CancelType = (CancelTypes)(NConvert.ToInt32(this.Reader[45].ToString()));
					feeItemList.FeeOper.ID = this.Reader[46].ToString();
					feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[47].ToString());
					feeItemList.Invoice.ID = this.Reader[48].ToString();
					feeItemList.Invoice.Type.ID = this.Reader[49].ToString();
                    feeItemList.FeeCodeStat.ID = this.Reader[49].ToString();
                    feeItemList.FeeCodeStat.SortID = NConvert.ToInt32(this.Reader[50].ToString());
					feeItemList.IsConfirmed = NConvert.ToBoolean(this.Reader[51].ToString());
					feeItemList.ConfirmOper.ID = this.Reader[52].ToString();
					feeItemList.ConfirmOper.Dept.ID = this.Reader[53].ToString();
					feeItemList.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[54].ToString());

                    //�ۿ����
                    feeItemList.StockOper.Dept.ID = feeItemList.ConfirmOper.Dept.ID;//�ۿ����

					feeItemList.InvoiceCombNO = this.Reader[55].ToString();
					feeItemList.NewItemRate = NConvert.ToDecimal(this.Reader[56].ToString());
					feeItemList.OrgItemRate = NConvert.ToDecimal(this.Reader[57].ToString());
					feeItemList.ItemRateFlag = this.Reader[58].ToString();
					feeItemList.Item.SpecialFlag1 = this.Reader[59].ToString();
					feeItemList.Item.SpecialFlag2 = this.Reader[60].ToString();
					feeItemList.FeePack = this.Reader[61].ToString();
					feeItemList.UndrugComb.ID = this.Reader[62].ToString();
					feeItemList.UndrugComb.Name = this.Reader[63].ToString();
					feeItemList.NoBackQty = NConvert.ToDecimal(this.Reader[64].ToString());
					feeItemList.ConfirmedQty = NConvert.ToDecimal(this.Reader[65].ToString());
					feeItemList.ConfirmedInjectCount = NConvert.ToInt32(this.Reader[66].ToString());
					feeItemList.Order.ID = this.Reader[67].ToString();
					feeItemList.RecipeSequence = this.Reader[68].ToString();
					feeItemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[69].ToString());
					feeItemList.SpecialPrice = NConvert.ToDecimal(this.Reader[70].ToString());
					feeItemList.FT.ExcessCost = NConvert.ToDecimal(this.Reader[71].ToString());
					feeItemList.FT.DrugOwnCost = NConvert.ToDecimal(this.Reader[72].ToString());
					feeItemList.FTSource = this.Reader[73].ToString();
					feeItemList.Item.IsMaterial = NConvert.ToBoolean(this.Reader[74].ToString());
                    feeItemList.IsAccounted = NConvert.ToBoolean(this.Reader[75].ToString());
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    //���ʳ�����ˮ��
                    feeItemList.UpdateSequence = NConvert.ToInt32(this.Reader[76].ToString());

                    //�ж�77����������Ƿ����
                    if (this.Reader.FieldCount > 78)
                    {
                        feeItemList.Order.Patient.Pact.PayKind.ID = this.Reader[77].ToString();
                        feeItemList.Order.Patient.Pact.ID = this.Reader[78].ToString();
                    }

                    if (this.Reader.FieldCount > 82)
                    {
                        feeItemList.OrgPrice = NConvert.ToDecimal(this.Reader[79]);
                        feeItemList.UndrugComb.Qty = NConvert.ToDecimal(this.Reader[80]);
                        feeItemList.Order.Memo = this.Reader[81].ToString();
                        feeItemList.Memo = this.Reader[82].ToString();
                    }

                    if (this.Reader.FieldCount > 84)
                    {
                        feeItemList.DoctDeptInfo.ID = this.Reader[83].ToString();
                        feeItemList.MedicalGroupCode.ID = this.Reader[84].ToString();
                    }

                    if (this.Reader.FieldCount > 85)
                    {
                        feeItemList.FT.FTRate.User03 = this.Reader[85].ToString();
                    }

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (this.Reader.FieldCount > 86)
                    {
                        feeItemList.Order.QuitFlag = NConvert.ToInt32(this.Reader[86]);
                    }

                    //���ͽ��
                    if (this.Reader.FieldCount > 87)
                    {
                        feeItemList.FT.DonateCost = NConvert.ToDecimal(this.Reader[87]);
                    }

                    //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
                    //�ײͱ��
                    if (this.Reader.FieldCount > 88)
                    {
                        feeItemList.IsPackage = this.Reader[88].ToString();
                    }

                    if (this.Reader.FieldCount > 89)
                    {
                        feeItemList.FT.PackageEco = NConvert.ToDecimal(this.Reader[89]);
                    }
                    if (this.Reader.FieldCount > 90)
                    {
                        feeItemList.User01 = this.Reader[90].ToString();
                    }
                    //{4007AD8C-65AE-4962-95F6-AC1907A07553}��ȡ����ײ�����
                    if (this.Reader.FieldCount > 91)
                    {
                        feeItemList.CheckBodyPackage = this.Reader[91].ToString();
                    }
                    //{DC67634A-696F-4e03-8C63-447C4265817E}
                    if (this.Reader.FieldCount > 92)
                    {
                        feeItemList.RangeFlag = this.Reader[92].ToString();
                    }
                    if (this.Reader.FieldCount > 93)
                    {
                        feeItemList.IsExceeded = string.IsNullOrEmpty(this.Reader[93].ToString()) ? false : true;
                    }

                     if (this.Reader.FieldCount > 94)
                    {
                        feeItemList.Order.RefundReason = this.Reader[94].ToString(); //ҽ���˷�ԭ��
                    }

					feeItemLists.Add(feeItemList);
				}//ѭ������

				this.Reader.Close();

				return feeItemLists;
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}
				
				return null;
			}
		}
		
		/// <summary>
		/// ����Where������������ѯ������ϸ��Ϣ
		/// </summary>
		/// <param name="whereIndex">Where��������</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryFeeItemLists(string whereIndex, params string[] args)
		{
			string sql = string.Empty;//SELECT���
			string where = string.Empty;//WHERE���
			
			//���Where���
			if (this.Sql.GetCommonSql(whereIndex, ref where) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return null;
			}

			sql = this.GetSqlFeeDetail();

			return this.QueryFeeDetailBySql(sql + " " + where, args);
		}


        /// <summary>
        /// ����Where������������ѯ������ϸ��Ϣ//{2044475C-E8CE-454B-B328-90EAAC174D1A} ��ѯ������ϸ��ӷѱ�����
        /// </summary>
        /// <param name="whereIndex">Where��������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        private ArrayList QueryFeeItemListsAndFeeName(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            string where = string.Empty;//WHERE���

            //���Where���
            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            sql = this.GetSqlFeeDetailAndFeeName();

            return this.QueryFeeDetailBySql(sql + " " + where, args);
        }

		/// <summary>
		/// ���insert��Ĵ����������update
		/// </summary>
		/// <param name="feeItemList">������ϸʵ��</param>
		/// <returns>�ַ�������</returns>
        private string[] GetFeeItemListParams(FeeItemList feeItemList)
        {
            //{4007AD8C-65AE-4962-95F6-AC1907A07553} //{143CA424-7AF9-493a-8601-2F7B1D635027}
           // {4B7EF27D-EAB1-43d7-AF5C-524B76310866}
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            if (dept == null)
            {
                dept = new Department();
            }
            if (string.IsNullOrEmpty(feeItemList.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospital_id = dept.HospitalID;
                string hospital_name = dept.HospitalName;
                feeItemList.Hospital_id = hospital_id;
                feeItemList.Hospital_name = hospital_name;
            }
           

            string[] args = new string[92];	//{3AEB5613-1CB0-4158-89E6-F82F0B643388}				 

            args[0] = feeItemList.RecipeNO;//RECIPE_NO,	--		������							0
            args[1] = feeItemList.SequenceNO.ToString();	  //SEQUENCE_NO;	--		��������Ŀ��ˮ��				1
            args[2] = ((int)feeItemList.TransType).ToString();//TRANS_TYPE;	--		��������;1�����ף�2������		2
            args[3] = feeItemList.Patient.ID;//CLINIC_CODE;	--		�����								3	
            args[4] = feeItemList.Patient.PID.CardNO;//CARD_NO;	--		��������									4		
            args[5] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.SeeDate.ToString();//REG_DATE;	--		�Һ�����						5	
            args[6] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;//REG_DPCD;	--		�Һſ���							6	
            args[7] = feeItemList.RecipeOper.ID;//DOCT_CODE;	--		����ҽʦ							7
            args[8] = feeItemList.RecipeOper.Dept.ID;//DOCT_DEPT;	--		����ҽʦ���ڿ���				8
            args[9] = feeItemList.Item.ID;//ITEM_CODE;	--		��Ŀ����									9.
            args[10] = feeItemList.Item.Name;//ITEM_NAME;	--		��Ŀ����									10
            //args[11] = NConvert.ToInt32(feeItemList.Item.IsPharmacy).ToString();//DRUG_FLAG;	--		1ҩƷ/0��Ҫ					11
            args[11] = ((int)(feeItemList.Item.ItemType)).ToString();
            args[12] = feeItemList.Item.Specs;//SPECS;		--		���										12
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[13] = NConvert.ToInt32(((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade).ToString();//SELF_MADE;	--		����ҩ��־					13
                args[14] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID;//DRUG_QUALITY;	--		ҩƷ���ʣ���ҩ����ҩ		14	
                args[15] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID;//DOSE_MODEL_CODE;--		����							15.
            }
            args[16] = feeItemList.Item.MinFee.ID;//FEE_CODE;	--		��С���ô���							16	
            args[17] = feeItemList.Item.SysClass.ID.ToString();//CLASS_CODE;	--		ϵͳ���				17	
            args[18] = feeItemList.Item.Price.ToString();//UNIT_PRICE;	--		����							18	
            args[19] = feeItemList.Item.Qty.ToString();//QTY;		--		����								19	
            args[20] = feeItemList.Days.ToString();//DAYS;		--		��ҩ�ĸ���������ҩƷΪ1			20	
            args[21] = feeItemList.Order.Frequency.ID;//FREQUENCY_CODE;	--		Ƶ�δ���						21	
            args[22] = feeItemList.Order.Usage.ID;//USAGE_CODE;	--		�÷�����							22	
            args[23] = feeItemList.Order.Usage.Name;//USE_NAME;	--		�÷�����							23
            args[24] = feeItemList.InjectCount.ToString();//INJECT_NUMBER;	--		Ժ��ע�����		24	
            args[25] = NConvert.ToInt32(feeItemList.IsUrgent).ToString();//EMC_FLAG;	--		�Ӽ����:1�Ӽ�/0��ͨ			25	
            args[26] = feeItemList.Order.Sample.ID;//LAB_TYPE;	--		��������							26	
            args[27] = feeItemList.Order.CheckPartRecord;//CHECK_BODY;	--		����								27	
            args[28] = feeItemList.Order.DoseOnce.ToString();//DOSE_ONCE;	--		ÿ������					28
            args[29] = feeItemList.Order.DoseUnit;//DOSE_UNIT;	--		ÿ��������λ							29
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[30] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose.ToString();//BASE_DOSE;	--		��������					30
            }
            args[31] = feeItemList.Item.PackQty.ToString();//PACK_QTY;	--		��װ����						31	
            args[32] = feeItemList.Item.PriceUnit;//PRICE_UNIT;	--		�Ƽ۵�λ							32	
            args[33] = feeItemList.FT.PubCost.ToString();//PUB_COST;	--		�ɱ�Ч���				33	
            args[34] = feeItemList.FT.PayCost.ToString();//PAY_COST;	--		�Ը����				34	
            args[35] = feeItemList.FT.OwnCost.ToString();//OWN_COST;	--		�ֽ���				35	
            args[36] = feeItemList.ExecOper.Dept.ID;//EXEC_DPCD;	--		ִ�п��Ҵ���					36
            args[37] = feeItemList.ExecOper.Dept.Name;//EXEC_DPNM;	--		ִ�п�������					37
            args[38] = feeItemList.Compare.CenterItem.ID;//CENTER_CODE;	--		ҽ��������Ŀ����				38	
            args[39] = feeItemList.Compare.CenterItem.ItemGrade;//ITEM_GRADE;	--		��Ŀ�ȼ�1����2����3����		39	
            args[40] = NConvert.ToInt32(feeItemList.Order.Combo.IsMainDrug).ToString();//MAIN_DRUG;	--		��ҩ��־					40
            args[41] = feeItemList.Order.Combo.ID;//COMB_NO;	--		��Ϻ�										41	
            args[42] = feeItemList.ChargeOper.ID;//OPER_CODE;	--		������							42
            args[43] = feeItemList.ChargeOper.OperTime.ToString();//OPER_DATE;	--		����ʱ��					43
            args[44] = ((int)feeItemList.PayType).ToString();// //PAY_FLAG;	--		�շѱ�־��1δ�շѣ�2�շ�	44	
            args[45] = ((int)feeItemList.CancelType).ToString();
            args[46] = feeItemList.FeeOper.ID;//FEE_CPCD;	--		�շ�Ա����							46	
            args[47] = feeItemList.FeeOper.OperTime.ToString();//FEE_DATE;	--		�շ�����					47	
            args[48] = feeItemList.Invoice.ID;//INVOICE_NO;	--		Ʊ�ݺ�								48	
            args[49] = feeItemList.FeeCodeStat.ID;//INVO_CODE;	--		��Ʊ��Ŀ����				49
            args[50] = feeItemList.FeeCodeStat.SortID.ToString();//INVO_SEQUENCE;	--		��Ʊ����ˮ��		50
            args[51] = NConvert.ToInt32(feeItemList.IsConfirmed).ToString();//CONFIRM_FLAG;	--		1δȷ��/2ȷ��				51		
            args[52] = feeItemList.ConfirmOper.ID;//CONFIRM_CODE;	--		ȷ����						52		
            args[53] = feeItemList.ConfirmOper.Dept.ID;//CONFIRM_DEPT;	--		ȷ�Ͽ���					53	
            args[54] = feeItemList.ConfirmOper.OperTime.ToString();//CONFIRM_DATE;	--		ȷ��ʱ��				54	
            args[55] = feeItemList.FT.RebateCost.ToString();// ECO_COST -- �Żݽ�� 55
            args[56] = feeItemList.InvoiceCombNO;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo  56
            args[57] = feeItemList.NewItemRate.ToString();//����Ŀ����  57
            args[58] = feeItemList.OrgItemRate.ToString();//ԭ��Ŀ����  58 
            args[59] = feeItemList.ItemRateFlag;//��չ��־ ������Ŀ��־ 1�Է� 2 ���� 3 ����  59
            args[60] = feeItemList.UndrugComb.ID;
            args[61] = feeItemList.UndrugComb.Name;
            args[62] = feeItemList.Item.SpecialFlag1;
            args[63] = feeItemList.Item.SpecialFlag2;
            args[64] = feeItemList.FeePack;
            args[65] = feeItemList.NoBackQty.ToString();
            args[66] = feeItemList.ConfirmedQty.ToString();
            args[67] = feeItemList.ConfirmedInjectCount.ToString();
            args[68] = feeItemList.Order.ID;
            args[69] = feeItemList.RecipeSequence;
            args[70] = feeItemList.SpecialPrice.ToString();
            args[71] = feeItemList.FT.ExcessCost.ToString();
            args[72] = feeItemList.FT.DrugOwnCost.ToString();
            args[73] = feeItemList.FTSource;
            args[74] = NConvert.ToInt32(feeItemList.Item.IsMaterial).ToString();
            args[75] = NConvert.ToInt32(feeItemList.IsAccounted).ToString();
            //���ʳ�����ˮ��
            args[76] = NConvert.ToInt32(feeItemList.UpdateSequence).ToString();
            //����ҽ����������
            args[77] = feeItemList.DoctDeptInfo.ID.ToString();
            args[78] = feeItemList.MedicalGroupCode.ID.ToString();
            if (string.IsNullOrEmpty(feeItemList.Order.Patient.Pact.ID))
            {
                args[79] = feeItemList.Patient.Pact.PayKind.ID;
                args[80] = feeItemList.Patient.Pact.ID;
            }
            else
            {
                args[79] = feeItemList.Order.Patient.Pact.PayKind.ID;//6D7EC8BC-BDBB-4a47-BCFF-36BB0113499A}�ѽ���������ӵ��շ���Ŀ��ϸ��
                feeItemList.Order.Patient.Pact.ID = feeItemList.Patient.Pact.ID;
                args[80] = feeItemList.Order.Patient.Pact.ID;
            }

            args[81] = feeItemList.OrgPrice.ToString();
            args[82] = feeItemList.UndrugComb.Qty.ToString();
            args[83] = feeItemList.Order.Memo;//������ע
            args[84] = feeItemList.Memo;//���ñ�ע
            args[85] = feeItemList.FT.FTRate.User03;  //ҽ���������� Extflag3

            args[86] = feeItemList.FT.DonateCost.ToString();   //���ͽ��

            //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
            if (!string.IsNullOrEmpty(feeItemList.IsPackage))
            {
                args[87] = feeItemList.IsPackage;
            }
            else
            {
                args[87] = "0";
            }

            if (!string.IsNullOrEmpty(feeItemList.FT.PackageEco.ToString()))
            {
                args[88] = feeItemList.FT.PackageEco.ToString();
            }
            else
            {
                args[88] = "0.00";
            }
            //{4007AD8C-65AE-4962-95F6-AC1907A07553}
            if (!string.IsNullOrEmpty(feeItemList.CheckBodyPackage))
            {
                args[89] = feeItemList.CheckBodyPackage;
            }
            //�������
            //args[79] = feeItemList.SeeNo;

            args[90] = feeItemList.Hospital_id;
            args[91] =feeItemList.Hospital_name; //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
          
            return args;
        }

		/// <summary>
		/// ��ȡ������ϸ��ѯ���
		/// </summary>
		/// <returns>�ɹ�: ���ص�SQL��� ʧ��: null</returns>
		public string GetQueryFeeItemListsSql()
		{
			string sql = string.Empty;//SQL���

			if (this.Sql.GetCommonSql("Fee.OutPatient.GetFeeDetailByInvoiceNo.Select", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetFeeDetailByInvoiceNo.Select��SQL���";

				return null;
			}

			return sql;
		}

		/// <summary>
		/// ����Where����������ѯ������ϸ��Ϣ����
		/// </summary>
		/// <param name="whereIndex">where����</param>
		/// <param name="ds">���ص�DataSet</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������Ϣ��ϸDataSet ʧ��:null</returns>
		private int QueryFeeItemLists(string whereIndex, ref DataSet ds, params string[] args)
		{
			string select = string.Empty;//SELECT���;
			string where = string.Empty;//WHERE���;

			if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return -1;
			}

			try
			{
				where = string.Format(where, args);
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				return -1;
			}

			select = this.GetQueryFeeItemListsSql();
			
			return this.ExecQuery(select + " " + where, ref ds);
		}

		#endregion

		#region �������

		/// <summary>
		/// ��÷�Ʊ��Ϣ����
		/// </summary>
        /// <param name="balance">��Ʊʵ��</param>  {31E733E1-8329-4e47-A024-47BCD4C6976D}
		/// <returns>��Ʊ��Ϣ����</returns>
		private string [] GetBalanceParams(Balance balance)
		{
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            if (dept == null)
            {
                dept = new Department();
            }
            if (string.IsNullOrEmpty(balance.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospital_id = dept.HospitalID;
                string hospital_name = dept.HospitalName;
                balance.Hospital_id = hospital_id;
                balance.Hospital_name = hospital_name;
            }
            string[] args =
				{	
					balance.Invoice.ID,//��Ʊ��	
					((int)balance.TransType).ToString(),//1��������,1����2��
					balance.Patient.PID.CardNO,//2��������
					((Register)balance.Patient).DoctorInfo.SeeDate.ToString(),//3 �Һ�����
					balance.Patient.Name,//4��������
					balance.Patient.Pact.PayKind.ID,//5����������
					balance.Patient.Pact.ID,//6��ͬ��λ����
					balance.Patient.Pact.Name,//7��ͬ��λ����
					balance.Patient.SSN,//8���˱��
					"",//9ҽ�����
					balance.FT.TotCost.ToString(),//10�ܶ�
					balance.FT.PubCost.ToString(),//11�ɱ�Ч���
					balance.FT.OwnCost.ToString(),//12���ɱ�Ч���
					balance.FT.PayCost.ToString(),//13�Ը����
					balance.FT.RebateCost.ToString(),//14�Żݽ�� //balance.User01,//14Ԥ��1 //{95A75EAC-F101-46aa-8DE7-A881F0B08DBB}
					balance.User02,//15Ԥ��2
					balance.User03,//16Ԥ��3
					balance.FT.BalancedCost.ToString(),//17ʵ�����
					balance.BalanceOper.ID,//18������
					balance.BalanceOper.OperTime.ToString(),//19����ʱ��
					balance.ExamineFlag,//0�������/1�������/2������� 
					((int)balance.CancelType).ToString(),//21���ϱ�־,0δ,1��
					balance.CanceledInvoiceNO,//22����Ʊ�ݺ�
					balance.CancelOper.ID,//23���ϲ���Ա
					balance.CancelOper.OperTime.ToString(),//24����ʱ��
					NConvert.ToInt32(balance.IsAuditing).ToString(),//25 0δ�˲�/1�Ѻ˲�
					balance.AuditingOper.ID,//26�˲���
					balance.AuditingOper.OperTime.ToString(),//	27�˲�ʱ��
					NConvert.ToInt32(balance.IsDayBalanced).ToString(),//0δ�ս�/1���ս�
					balance.BalanceID,//29	�ս��ʶ��
					balance.DayBalanceOper.ID,//			30�ս���
					balance.DayBalanceOper.OperTime.ToString(),//31�ս�ʱ��0
					balance.CombNO, // 32 ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo 		
					balance.InvoiceType.ID, // 33��չ��־ 1 �Է� 2 ���� 3 ����
					balance.Patient.ID, //34�Һ���ˮ��	
				    balance.PrintedInvoiceNO,  //35
                    balance.DrugWindowsNO,  //36
                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    NConvert.ToInt32(balance.IsAccount).ToString(),  //37
                    balance.Memo,//38 {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    balance.FT.DonateCost.ToString() ,  //39 ���ͽ��
                    balance.Hospital_id,//Ժ��id
                    balance.Hospital_name//Ժ���� {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
				};
			
			return args;
		}

		/// <summary>
		/// ͨ��SQL����ý�����Ϣ����
		/// </summary>
		/// <param name="sql">SQL���</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������Ϣ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryBalancesBySql(string sql, params string[] args)
		{
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}
			
			ArrayList balances = new ArrayList();//������Ϣʵ������
			Balance balance = null;//������Ϣʵ��
			
			try
			{	
				//ѭ����ȡ����
				while (this.Reader.Read()) 
				{
					balance = new Balance();
					
					balance.Invoice.ID = this.Reader[0].ToString();//0��Ʊ��
					balance.TransType = (TransTypes)NConvert.ToInt32(this.Reader[1].ToString());//��������,1�����ף�2������
					balance.Patient.PID.CardNO = this.Reader[2].ToString();//2��������
					((Register)balance.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[3].ToString());//3�Һ�����
					balance.Patient.Name = this.Reader[4].ToString();//	4��������
					balance.Patient.Pact.PayKind.ID = this.Reader[5].ToString();//5����������
					balance.Patient.Pact.ID = this.Reader[6].ToString();//6��ͬ��λ����
					balance.Patient.Pact.Name = this.Reader[7].ToString();//7��ͬ��λ����
					balance.Patient.SSN = this.Reader[8].ToString();//8���˱��
					balance.FT.TotCost = NConvert.ToDecimal(this.Reader[10].ToString());//10�ܶ�
					balance.FT.PubCost = NConvert.ToDecimal(this.Reader[11].ToString());//11�ɱ�Ч���
					balance.FT.OwnCost = NConvert.ToDecimal(this.Reader[12].ToString());//12���ɱ�Ч���
					balance.FT.PayCost = NConvert.ToDecimal(this.Reader[13].ToString());//13�Ը����
					balance.User01 = this.Reader[14].ToString();//14Ԥ��1
                    balance.FT.RebateCost = NConvert.ToDecimal(this.Reader[14]);
					balance.User02 = this.Reader[15].ToString();//15Ԥ��2
					balance.User03 = this.Reader[16].ToString();//16Ԥ��3
					balance.FT.BalancedCost = NConvert.ToDecimal(this.Reader[17].ToString());//17ʵ�����
					balance.BalanceOper.ID = this.Reader[18].ToString();//18������
					balance.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[19].ToString());//19����ʱ��
					balance.ExamineFlag = this.Reader[20].ToString();//0�������/1�������/2������� 
					balance.CancelType = (CancelTypes)NConvert.ToInt32(this.Reader[21].ToString());
					balance.CanceledInvoiceNO = this.Reader[22].ToString();//22����Ʊ�ݺ�
					balance.CancelOper.ID = this.Reader[23].ToString();//23���ϲ���Ա
					balance.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[24].ToString());//24����ʱ��
					balance.IsAuditing = NConvert.ToBoolean(this.Reader[25].ToString());//�Ƿ�˲�
					balance.AuditingOper.ID = this.Reader[26].ToString();//		26�˲���
					balance.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());//27�˲�ʱ��
					balance.IsDayBalanced = NConvert.ToBoolean(this.Reader[28].ToString());//28�Ƿ��ս�
					balance.BalanceID = this.Reader[29].ToString();//29	�ս��ʶ��
					balance.DayBalanceOper.ID = this.Reader[30].ToString();//30�ս���
					balance.DayBalanceOper.OperTime = NConvert.ToDateTime(this.Reader[31].ToString());//31�ս�ʱ��0
					balance.CombNO = this.Reader[32].ToString();
					balance.InvoiceType.ID = this.Reader[33].ToString();
					balance.Patient.ID = this.Reader[34].ToString();
                    balance.PrintedInvoiceNO = this.Reader[35].ToString();
                    balance.DrugWindowsNO = this.Reader[36].ToString();
                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    balance.IsAccount = NConvert.ToBoolean(this.Reader[37]);
                    balance.Memo = this.Reader[38].ToString();   //�˷�ԭ��
                    balance.FT.DonateCost = NConvert.ToDecimal(this.Reader[39].ToString());   //���ͽ��
                    balances.Add(balance);
				}//ѭ������

				this.Reader.Close();

				return balances;
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
		}
		/// <summary>
		/// ��÷�Ʊ��Ϣ��Select��SQL���
		/// </summary>
		/// <returns>�ɹ�:��Ʊ��Ϣ��Select��SQL��� ʧ��: null</returns>
		public string GetBalanceSelectSql() 
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoInfo",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetInvoInfo��SQL���";
				
				return null;
			}

			return sql;
		}

		/// <summary>
		/// ����Where������������ѯ������Ϣ
		/// </summary>
		/// <param name="whereIndex">Where��������</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryBalances(string whereIndex, params string[] args)
		{
			string sql = string.Empty;//SELECT���
			string where = string.Empty;//WHERE���
			
			//���Where���
			if (this.Sql.GetCommonSql(whereIndex, ref where) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return null;
			}

			sql = this.GetBalanceSelectSql();

			return this.QueryBalancesBySql(sql + " " + where, args);
		}

		/// <summary>
		/// ��ȡ��Ʊ������Ϣ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <returns>�ɹ�:��ȡ������ϢSQL��ѯ��� ʧ��: null</returns>
		public string GetQueryBalancesSql()
		{
			string sql = string.Empty;//SQL���
			
			if( this.Sql.GetCommonSql("Fee.OutPatient.GetInvoiceInformation.Select", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetInvoiceInformation.Select��SQL���";
				
				return null;
			}

			return sql;
		}
		
		/// <summary>
		/// ����Where����������ѯ������Ϣ����
		/// </summary>
		/// <param name="whereIndex">where����</param>
		/// <param name="ds">���ص�DataSet</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������ϢDataSet ʧ��:null</returns>
		private int QueryBalances(string whereIndex, ref DataSet ds, params string[] args)
		{
			string select = string.Empty;//SELECT���;
			string where = string.Empty;//WHERE���;

			if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return -1;
			}

			try
			{
				where = string.Format(where, args);
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				return -1;
			}

			select = this.GetQueryBalancesSql();
			
			return this.ExecQuery(select + " " + where, ref ds);
		}

		#endregion

		#region ������ϸ����

		/// <summary>
		/// ��ý�����ϸ����
		/// </summary>
        /// <param name="balanceList">������ϸʵ��</param> {31E733E1-8329-4e47-A024-47BCD4C6976D}
		/// <returns>������ϸʵ���ֶ�����</returns>
		protected string [] GetBalanceListParams(BalanceList balanceList)
		{
            HISFC.Models.Base.Employee employee = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new Employee();
            }
            HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;

            if (dept == null)
            {
                dept = new Department();
            }
            if (string.IsNullOrEmpty(balanceList.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospital_id = dept.HospitalID;
                string hospital_name = dept.HospitalName;
                balanceList.Hospital_id = hospital_id;
                balanceList.Hospital_name = hospital_name;
            }
           
           
			string[] args =
				{	
					balanceList.BalanceBase.Invoice.ID,//��Ʊ��
					((int)balanceList.BalanceBase.TransType).ToString(),//��������,1�����ף�2������		2
					balanceList.InvoiceSquence.ToString(),//2��Ʊ����ˮ��
					balanceList.FeeCodeStat.ID,//3��Ʊ��Ŀ����
					balanceList.FeeCodeStat.Name,//4��Ʊ��Ŀ����
					balanceList.BalanceBase.FT.PubCost.ToString(),//5�ɱ�Ч���
					balanceList.BalanceBase.FT.OwnCost.ToString(),//6���ɱ�Ч���
					balanceList.BalanceBase.FT.PayCost.ToString(),//7�Ը����
					balanceList.BalanceBase.RecipeOper.Dept.ID,//8��������
					balanceList.BalanceBase.RecipeOper.Dept.Name,//9������������
					balanceList.BalanceBase.BalanceOper.OperTime.ToString(),//10����ʱ��
					balanceList.BalanceBase.BalanceOper.ID,//11����Ա
					NConvert.ToInt32(balanceList.BalanceBase.IsDayBalanced).ToString(),//12 0δ�ս�/1���ս�
					((Balance)balanceList.BalanceBase).BalanceID,//13�ս��ʶ��
					balanceList.BalanceBase.DayBalanceOper.ID,//14�ս���
					balanceList.BalanceBase.DayBalanceOper.OperTime.ToString(),//15�ս�ʱ��
					((int)balanceList.BalanceBase.CancelType).ToString(),//16 �˷ѱ��
					((Balance)balanceList.BalanceBase).CombNO, //17 ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo 
                    balanceList.Hospital_id, ///Ժ��id
                    balanceList.Hospital_name // Ժ����{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
				};

			return args;
		}

		/// <summary>
		/// ͨ��SQL����ý�����ϸʵ��
		/// </summary>
		/// <param name="sql">SQL���</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�: ������ϸʵ������ ʧ��: null</returns>
		private ArrayList QueryBalanceListsBySql(string sql, params string[] args)
		{
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}
			
			ArrayList balanceLists = new ArrayList();//������ϸʵ�弯��
			BalanceList balanceList = null;//������ϸʵ��
            
			try
			{	//ѭ����ȡ����
				while (this.Reader.Read()) 
				{
					balanceList = new BalanceList();
               
					balanceList.BalanceBase.Invoice.ID = this.Reader[0].ToString();//0��Ʊ��
					balanceList.BalanceBase.TransType = (TransTypes)NConvert.ToInt32(this.Reader[1].ToString());//1��������,1����2��	
					balanceList.InvoiceSquence = NConvert.ToInt32(this.Reader[2].ToString());//2��Ʊ����ˮ��
					balanceList.FeeCodeStat.ID = this.Reader[3].ToString();//3��Ʊ��Ŀ����
					balanceList.FeeCodeStat.Name = this.Reader[4].ToString();//4��Ʊ��Ŀ����
					balanceList.BalanceBase.FT.PubCost = NConvert.ToDecimal(this.Reader[5].ToString());//5�ɱ�Ч���
					balanceList.BalanceBase.FT.OwnCost = NConvert.ToDecimal(this.Reader[6].ToString());//6���ɱ�Ч���
					balanceList.BalanceBase.FT.PayCost = NConvert.ToDecimal(this.Reader[7].ToString());//7�Ը����
                    balanceList.BalanceBase.RecipeOper.Dept.ID = this.Reader[8].ToString();
                    balanceList.BalanceBase.RecipeOper.Dept.Name = this.Reader[9].ToString();
					balanceList.BalanceBase.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[10].ToString());//10����ʱ��
					balanceList.BalanceBase.BalanceOper.ID = this.Reader[11].ToString();//11����Ա
					balanceList.BalanceBase.IsDayBalanced = NConvert.ToBoolean(this.Reader[12].ToString());//12 1���ս�/0δ�ս�
					((Balance)balanceList.BalanceBase).BalanceID = this.Reader[13].ToString();//13�ս��ʶ��
					balanceList.BalanceBase.DayBalanceOper.ID = this.Reader[14].ToString();//14	�ս���
					balanceList.BalanceBase.DayBalanceOper.OperTime = NConvert.ToDateTime(this.Reader[15].ToString());//15�ս�ʱ��
					((Balance)balanceList.BalanceBase).CombNO = this.Reader[16].ToString();//16��Ʊ���к�

                    // {89A168FF-5BCB-4e05-8A0E-143E9EA1F80E}
                    balanceList.BalanceBase.FT.TotCost = balanceList.BalanceBase.FT.PubCost + balanceList.BalanceBase.FT.OwnCost + balanceList.BalanceBase.FT.PayCost;
					balanceLists.Add(balanceList);
				}

				this.Reader.Close();
				
				return balanceLists;
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();
				
				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
		}
		/// <summary>
		/// ��÷�Ʊ��ϸ��SQL���
		/// </summary>
		/// <returns>�ɹ�:��Ʊ��ϸ��SQL��� ʧ��: null</returns>
		public string GetBalanceListsSql() 
		{
			string sql = string.Empty;//SQL���
			
			if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoDetailInfo" ,ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetInvoDetailInfo��SQL���";

				return null;
			}

			return sql;
		}

		/// <summary>
		/// ����Where������������ѯ������Ϣ
		/// </summary>
		/// <param name="whereIndex">Where��������</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������ϸ��Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryBalanceLists(string whereIndex, params string[] args)
		{
			string sql = string.Empty;//SELECT���
			string where = string.Empty;//WHERE���
			
			//���Where���
			if (this.Sql.GetCommonSql(whereIndex, ref where) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return null;
			}

			sql = this.GetBalanceListsSql();

			return this.QueryBalanceListsBySql(sql + " " + where, args);
		}

		/// <summary>
		/// ��ȡ������ϸ��ѯSQL���
		/// </summary>
		/// <returns>�ɹ�:��Ʊ��ϸ��SQL��� ʧ��: null</returns>
		public string GetQueryBalanceListsSql()
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoiceDetailByInvoiceNo.Select", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.OutPatient.GetInvoiceDetailByInvoiceNo.Select��SQL���";

				return null;
			}

			return sql;
		}

		/// <summary>
		/// ����Where����������ѯ������ϸ��Ϣ����
		/// </summary>
		/// <param name="whereIndex">where����</param>
		/// <param name="ds">���ص�DataSet</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�:������Ϣ��ϸDataSet ʧ��:null</returns>
		private int QueryBalanceLists(string whereIndex, ref DataSet ds, params string[] args)
		{
			string select = string.Empty;//SELECT���;
			string where = string.Empty;//WHERE���;

			if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

				return -1;
			}

			try
			{
				where = string.Format(where, args);
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				return -1;
			}

			select = this.GetQueryBalanceListsSql();
			
			return this.ExecQuery(select + " " + where, ref ds);
		}

		#endregion

		#region ������²���
		
		/// <summary>
		/// ���µ������
		/// </summary>
		/// <param name="sqlIndex">SQL�������</param>
		/// <param name="args">����</param>
		/// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
		private int UpdateSingleTable(string sqlIndex, params string[] args)
		{
			string sql = string.Empty;//Update���
			
			//���Where���
			if (this.Sql.GetCommonSql(sqlIndex, ref sql) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

				return -1;
			}

			return this.ExecNoQuery(sql, args);
		}

        /// <summary>
        /// ����Ψһֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:���ص�ǰΨһֵ ʧ��:null</returns>
        private string ExecSqlReturnOne(string index, params string[] args)
        {
            string sql = string.Empty;//SQL���

            if (this.Sql.GetCommonSql(index, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + index + "��SQL���";

                return null;
            }

            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            return base.ExecSqlReturnOne(sql);
        }

		#endregion

		#endregion

		#region ���з���

		#region �ս����

		/// <summary>
		/// �����ս���Ϣ
		/// </summary>
		/// <param name="dayBalance">�ս�ʵ��</param>
		/// <returns>�ɹ�: 1 ʧ�� -1 û�в������� 0</returns>
		public int InsertDayBalance(DayBalance dayBalance)
		{
			return this.UpdateSingleTable("Fee.OutPatient.DayBalance.Insert", this.GetDayBalanceParams(dayBalance));
		}

		#endregion

		#region ֧����Ϣ����
		
		/// <summary>
		/// ����֧�����
		/// </summary>
		/// <param name="balancePay">֧����Ϣʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
		public int InsertBalancePay(BalancePay balancePay)
		{
			return this.UpdateSingleTable("Fee.OutPatient.PayMode.Insert.1", this.GetBalancePayParams(balancePay));
		}
		
		/// <summary>
		/// ����֧����Ϣ
		/// </summary>
		/// <param name="balancePay">֧����Ϣʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�в������� 0</returns>
		public int UpdateBalancePay(BalancePay balancePay)
		{
			return this.UpdateSingleTable("Fee.OutPatient.PayMode.Update", this.GetBalancePayParams(balancePay));
		}

		/// <summary>
		/// ���ݷ�Ʊ�Ų�ѯ֧����Ϣ
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�: ֧����Ϣ���� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalancePaysByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalancePays("Fee.OutPatient.GetSqlPayMode.Where.1", invoiceNO);
		}

		/// <summary>
		/// ���ݽ�����Ų�ѯ֧����Ϣ
		/// </summary>
		/// <param name="invoiceSequence">��������</param>
		/// <returns>�ɹ�: ֧����Ϣ���� ʧ��: null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalancePaysByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryBalancePays("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
		}
		
		#endregion

		#region ������ϸ����

		/// <summary>
		/// ���������ϸ
		/// </summary>
		/// <param name="feeItemList">������ϸʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�в������ݷ��� 0</returns>
		public int InsertFeeItemList(FeeItemList feeItemList) 
		{
			return this.UpdateSingleTable("Fee.Item.GetFeeItemDetail.Insert", this.GetFeeItemListParams(feeItemList));
		}

		/// <summary>
		/// ���·�����ϸ
		/// </summary>
		/// <param name="feeItemList">������ϸʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
		public int UpdateFeeItemList(FeeItemList feeItemList) 
		{
			return this.UpdateSingleTable("Fee.OutPatient.ItemDetail.Update", this.GetFeeItemListParams(feeItemList));
		}
		
		/// <summary>
		/// ɾ��������ϸ������Ϻ�
		/// </summary>
		/// <param name="combNO">��Ϻ�</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û��ɾ�������ݷ��� 0</returns>
		public int DeleteFeeItemListByCombNO(string combNO)
		{
			return this.UpdateSingleTable("Fee.DelFeeDetail.1", combNO);
		}

		/// <summary>
		/// ���ݴ����źʹ�����Ŀ��ˮ�Ÿ���ȷ�ϱ�־
		/// </summary>
		/// <param name="recipeNO">������</param>
		/// <param name="recipeSquence">������Ŀ��ˮ��</param>
		/// <param name="confirmFlag">ȷ�ϱ�־ 1δȷ��/2ȷ��</param>
		/// <param name="confirmOper">ȷ����</param>
		/// <param name="confirmDeptCode">ȷ�Ͽ���</param>
		/// <param name="confirmTime">ȷ��ʱ��</param>
		/// <param name="noBackQty">��������</param>
		/// <param name="confirmQty">ȷ������</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
		public int UpdateConfirmFlag(string recipeNO, int recipeSquence, string confirmFlag, string confirmOper, string confirmDeptCode, DateTime confirmTime,
			decimal noBackQty, decimal confirmQty)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateConfirmFlag.Update.1", recipeNO, recipeSquence.ToString(), confirmFlag, confirmOper, confirmDeptCode, confirmTime.ToString(),
				noBackQty.ToString(), confirmQty.ToString());
		}

        /// <summary>
        /// ���ݴ����źʹ�����Ŀ��ˮ�Ÿ���ȷ�ϱ�־
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="confirmFlag">ȷ�ϱ�־ 1δȷ��/2ȷ��</param>
        /// <param name="confirmOper">ȷ����</param>
        /// <param name="confirmDeptCode">ȷ�Ͽ���</param>
        /// <param name="confirmTime">ȷ��ʱ��</param>
        /// <param name="noBackQty">��������</param>
        /// <param name="confirmQty">ȷ������</param>
        /// <returns>�ɹ�: >= 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
        public int UpdateConfirmFlag(string recipeNO, string moOrder, string confirmFlag, string confirmOper, string confirmDeptCode, DateTime confirmTime, decimal noBackQty, decimal confirmQty)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateConfirmFlag.Update.2", recipeNO, moOrder, confirmFlag, confirmOper, confirmDeptCode, confirmTime.ToString(),
                noBackQty.ToString(), confirmQty.ToString());
        }

		/// <summary>
		/// ���ݴ����źʹ�����Ŀ��ˮ�Ÿ���Ժע��ȷ������
		/// </summary>
		/// <param name="moOrder">ҽ����ˮ��</param>
		/// <param name="recipeNO">������</param>
		/// <param name="recipeSquence">��������ˮ��</param>
		/// <param name="qty">Ժע����</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
		public int UpdateConfirmInject(string moOrder,string recipeNO,string recipeSquence, int qty)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateConfirmInject.Update.1", moOrder, recipeNO, recipeSquence, qty.ToString());
		}


        /// <summary>
        /// ���ݴ����źʹ�����Ŀ��ˮ�Ÿ���Ժע��ȷ������
        /// </summary>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSquence">��������ˮ��</param>
        /// <param name="qty">Ժע����</param>
        /// <returns>�ɹ�: >= 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
        public int UpdateConfirmInject(string moOrder, string recipeNO, string recipeSquence, int qty,string cancelFlag)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateConfirmInjectContainQuit.Update.1", moOrder, recipeNO, recipeSquence, qty.ToString(), cancelFlag);
        }

		/// <summary>
		/// ���ݴ����źʹ�������ˮ��ɾ��������ϸ.
		/// </summary>
		/// <param name="recipeNO">������</param>
		/// <param name="recipeSequence">��������ˮ��</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û��ɾ�������ݷ��� 0</returns>
		public int DeleteFeeItemListByRecipeNO(string recipeNO, string recipeSequence)
		{
			return this.UpdateSingleTable("Fee.OutPatient.DeleteFeeDetailByRecipeNo", recipeNO, recipeSequence);
		}

		/// <summary>
		/// ����ҽ�����������Ŀ��ˮ��ɾ����ϸ
		/// </summary>
		/// <param name="moOrder">ҽ�����������Ŀ��ˮ��</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û��ɾ�������ݷ��� 0</returns>
		public int DeleteFeeItemListByMoOrder(string moOrder)
		{
			return this.UpdateSingleTable("Fee.OutPatient.DeleteFeeDetailbySeqNo", moOrder);
		}

		/// <summary>
		/// ɾ������������������Ϣ
		/// </summary>
		/// <param name="moOrder">ҽ����ˮ��</param>
		/// <returns>�ɹ�: >= 1 ʧ��: -1 û��ɾ�������ݷ��� 0</returns>
		public int DeletePackageByMoOrder(string moOrder)
		{
			return this.UpdateSingleTable("Fee.OutPatient.DeleteGroup", moOrder);
		}

		/// <summary>
		///  ɾ�������ϸ�����Ŷ�Ӧ��δ�շѵĴ�����ϸ
		/// </summary>
		/// <param name="clinicNO">����</param>
		/// <returns>1���ɹ�</returns>
		public int DeleteFeeItemListByClinicNO(string clinicNO)
		{
			return this.UpdateSingleTable("FS.HISFC.BizLogic.Fee.CheckUp.DeleteFeeList", clinicNO);
		}

        /// <summary>
        /// ������Ϻź���ˮ��ɾ��������ϸ
        /// </summary>
        /// <param name="combNo"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int DeleteFeeDetailByCombNoAndClinicCode(string combNo, string clinicCode)
        {
            return this.UpdateSingleTable("Fee.OutPatient.DeleteFeeDetailByCombNoAndClinicCode", combNo, clinicCode);
        }

		/// <summary>
		/// ��ô�����
		/// </summary>
		/// <returns>�ɹ�</returns>
		public string GetRecipeNO()
		{
			return this.GetSequence("Fee.OutPatient.GetRecipeNo.Select");
		}

		
		/// <summary>
		/// ͨ�����߿��ţ��õ�������ϸ
		/// </summary>
		/// <param name="cardNO">���￨��</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryFeeItemListsByCardNO(string cardNO)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetail.Where.1", cardNO);
		}
		
		/// <summary>
		/// ͨ����Ʊ�Ż�û�û��߷�����ϸ��Ϣ
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryFeeItemListsByInvoiceNO(string invoiceNO)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetailFromInvoiceNo.Where.1", invoiceNO);
		}

        /// <summary>
        /// ͨ����Ʊ�Ż�û�û��߷�����ϸ������Ϣ
        /// </summary>
        /// <param name="invoiceNO">��Ʊ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsTogetherByInvoiceNO(string invoiceNO)
        {
            string sql = string.Empty;//sql���

            //���sql���
            if (this.Sql.GetCommonSql("Fee.OutPatient.GetChargeDetailTogetherFromInvoiceNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + "Fee.OutPatient.GetChargeDetailTogetherFromInvoiceNo" + "��SQL���";

                return null;
            }

            return this.QueryFeeDetailBySql(sql, invoiceNO);
        }

        /// <summary>
        /// ͨ��������ˮ�ź���Ϻŵõ�������ϸ
        /// </summary>
        /// <param name="ComoNO"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailbyComoNOAndClinicCode(string ComoNO, string clinicCode)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromComoIdAndClinicCode.Select.1", ComoNO, clinicCode);
        }

		/// <summary>
		/// ��û��ߵ�δ�շ���Ŀ��Ϣ
		/// </summary>
		/// <param name="clinicNO">�Һ���ˮ��</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryChargedFeeItemListsByClinicNO(string clinicNO)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.1", clinicNO);
		}


        /// <summary>
        /// ��û��ߵ�δ�շ���Ŀ��Ϣ ���ݴ���������
        /// </summary>{C5626AAE-D12F-429f-8F4C-B1614A9C9EF0}
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryChargedFeeItemListsByClinicNOExt(string clinicNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.1.1", clinicNO);
        }


        /// <summary>
        /// ��û��ߵ�δ�շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="isFee">�Ƿ����շ� ALL��ʾȫ��</param>
        /// <param name="subFlag">���ı�� ALL��ʾȫ��</param>
        /// <param name="costSource">������Դ ALL��ʾȫ��</param>
        /// <returns></returns>
        public ArrayList QueryAllFeeItemListsByClinicNO(string clinicNO, string isFee, string subFlag, string costSource)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetAllFeeDetailByClinicNo", clinicNO, isFee, subFlag, costSource);
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// ���ݿ������һ�û���Ϊ�շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="doctDept"></param>
        /// <returns></returns>
        public ArrayList QueryChargedFeeItemListsByClinicNODoctDept(string clinicNO, string doctDept)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.5", clinicNO, doctDept);
        }


        /// <summary>
        /// ��û��ߵ����շ���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsByClinicNO(string clinicNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.AlreadFee", clinicNO);
        }

        /// <summary>
        /// ��û��ߵ����շ�����Ч����Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO">�Һ���ˮ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsByClinicNOAndValid(string clinicNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.AlreadFeeAndValid", clinicNO);
        }

		/// <summary>
		/// ��û��ߵ� �Ѿ��շѣ� δȷ�ϵ�ָ��SysClass����Ŀ��Ϣ
		/// </summary>
		/// <param name="cardNO">���߿���</param>
		/// <param name="sysClass">��Ŀϵͳ���</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryFeeItemLists(string cardNO, EnumSysClass sysClass)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.2", cardNO, sysClass.ToString());
		}

		/// <summary>
		/// ��û��ߵ� �Ѿ��շѣ� δȷ�ϵ�ָ�� ��ҪԺע����Ŀ��Ϣ
		/// </summary>
		/// <param name="cardNO">���߿���</param>
		/// <param name="isInject">true��Ҫ��Ժע����Ŀ false ��ѯ����������Ŀ</param>
		/// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryFeeItemLists(string cardNO, bool isInject)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.3", cardNO, NConvert.ToInt32(isInject).ToString());
		}

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ�����δ�շ���ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemLists(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.3", cardNO, dtFrom.ToString(), dtTo.ToString());

        }

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ������Ѿ��շ���ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsForZs(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.4", cardNO, dtFrom.ToString(), dtTo.ToString());

        }

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ������Ѿ��շ���ϸ(����������Ϣ)
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsForZsSubjob(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetail.Select.GetSubjob", cardNO, dtFrom.ToString(), dtTo.ToString());

        }

        /// <summary>
        /// ���ݲ����ź�ʱ��εõ������Ѿ��շ���ϸ--�����˷Ѽ�¼
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">����ʱ��</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryFeeItemListsAndQuitForZs(string cardNO, DateTime dtFrom, DateTime dtTo)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetChargeDetailAndQuit.Select.4", cardNO, dtFrom.ToString(), dtTo.ToString());

        }


        #region  ����ҽ��

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="pactID"></param>
        public int InvalidByClinicCode(string clinicCode, string pactID)
        {
            string sql = string.Empty;//SELECT���
            int result = 0;
            if (this.Sql.GetCommonSql("Fee.OutPatient.InvalidChargeDetail", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.OutPatient.InvalidChargeDetail ��SQL���";
            }
            try
            {
                result=this.ExecQuery(sql, clinicCode, pactID);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                result = -1;
            }
            return result;
        }


        #endregion


        #region ��ȡ����һ�ο������з�����ϸ��Ϣ -- ���ж�ҽ���Ƿ��ϴ�
        /// <summary>
        /// ��ȡ����һ�ο������з�����ϸ��Ϣ
        /// {4C5542EA-E90E-4831-B430-3D3DBDE12066}
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="pactID"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemByClinicCode(string clinicCode, string pactID)
        {
            string sql = string.Empty;//SELECT���

            if (this.Sql.GetCommonSql("Fee.OutPatient.GetChargeDetail.bysiupdateload", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.OutPatient.GetChargeDetail.bysiupdateload ��SQL���";

                return null;
            }

            if (this.ExecQuery(sql, clinicCode, pactID) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//������ϸ����
            FeeItemList feeItemList = null;//������ϸʵ��

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    feeItemList = new FeeItemList();

                    feeItemList.Item.ItemType = (EnumItemType)NConvert.ToInt32(this.Reader[11]);
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Pharmacy.Item();
                        feeItemList.Item.ItemType = EnumItemType.Drug;
                    }
                    else if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                        feeItemList.Item.ItemType = EnumItemType.UnDrug;
                    }
                    else
                    {
                        feeItemList.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                        feeItemList.Item.ItemType = EnumItemType.MatItem;

                    }

                    feeItemList.RecipeNO = this.Reader[0].ToString();
                    feeItemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());
                    if (this.Reader[2].ToString() == "1")
                    {
                        feeItemList.TransType = TransTypes.Positive;
                    }
                    else
                    {
                        feeItemList.TransType = TransTypes.Negative;
                    }
                    feeItemList.Patient.ID = this.Reader[3].ToString();
                    feeItemList.Patient.PID.CardNO = this.Reader[4].ToString();
                    ((Register)feeItemList.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[5].ToString());
                    ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    feeItemList.RecipeOper.ID = this.Reader[7].ToString();
                    ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID = this.Reader[7].ToString();
                    feeItemList.RecipeOper.Dept.ID = this.Reader[8].ToString();
                    feeItemList.Item.ID = this.Reader[9].ToString();
                    feeItemList.Item.Name = this.Reader[10].ToString();
                    feeItemList.Item.Specs = this.Reader[12].ToString();

                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade = NConvert.ToBoolean(this.Reader[13].ToString());
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID = this.Reader[14].ToString();
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID = this.Reader[15].ToString();
                    }
                    feeItemList.Item.MinFee.ID = this.Reader[16].ToString();
                    feeItemList.Item.SysClass.ID = this.Reader[17].ToString();
                    feeItemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());
                    feeItemList.Days = NConvert.ToDecimal(this.Reader[20].ToString());
                    feeItemList.Order.Frequency.ID = this.Reader[21].ToString();
                    feeItemList.Order.Usage.ID = this.Reader[22].ToString();
                    feeItemList.Order.Usage.Name = this.Reader[23].ToString();
                    feeItemList.InjectCount = NConvert.ToInt32(this.Reader[24].ToString());
                    feeItemList.IsUrgent = NConvert.ToBoolean(this.Reader[25].ToString());
                    feeItemList.Order.Sample.ID = this.Reader[26].ToString();
                    feeItemList.Order.CheckPartRecord = this.Reader[27].ToString();
                    feeItemList.Order.DoseOnce = NConvert.ToDecimal(this.Reader[28].ToString());
                    feeItemList.Order.DoseUnit = this.Reader[29].ToString();
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose = NConvert.ToDecimal(this.Reader[30].ToString());
                    }
                    feeItemList.Item.PackQty = NConvert.ToDecimal(this.Reader[31].ToString());
                    feeItemList.Item.PriceUnit = this.Reader[32].ToString();
                    feeItemList.FT.PubCost = NConvert.ToDecimal(this.Reader[33].ToString());
                    feeItemList.FT.PayCost = NConvert.ToDecimal(this.Reader[34].ToString());
                    feeItemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[35].ToString());
                    feeItemList.ExecOper.Dept.ID = this.Reader[36].ToString();
                    feeItemList.ExecOper.Dept.Name = this.Reader[37].ToString();
                    feeItemList.Compare.CenterItem.ID = this.Reader[38].ToString();
                    feeItemList.Compare.CenterItem.ItemGrade = this.Reader[39].ToString();
                    feeItemList.Order.Combo.IsMainDrug = NConvert.ToBoolean(this.Reader[40].ToString());
                    feeItemList.Order.Combo.ID = this.Reader[41].ToString();
                    feeItemList.ChargeOper.ID = this.Reader[42].ToString();
                    feeItemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());
                    feeItemList.PayType = (PayTypes)(NConvert.ToInt32(this.Reader[44].ToString()));
                    feeItemList.CancelType = (CancelTypes)(NConvert.ToInt32(this.Reader[45].ToString()));
                    feeItemList.FeeOper.ID = this.Reader[46].ToString();
                    feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[47].ToString());
                    feeItemList.Invoice.ID = this.Reader[48].ToString();
                    feeItemList.Invoice.Type.ID = this.Reader[49].ToString();
                    feeItemList.IsConfirmed = NConvert.ToBoolean(this.Reader[51].ToString());
                    feeItemList.ConfirmOper.ID = this.Reader[52].ToString();
                    feeItemList.ConfirmOper.Dept.ID = this.Reader[53].ToString();
                    feeItemList.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[54].ToString());
                    feeItemList.InvoiceCombNO = this.Reader[55].ToString();
                    feeItemList.NewItemRate = NConvert.ToDecimal(this.Reader[56].ToString());
                    feeItemList.OrgItemRate = NConvert.ToDecimal(this.Reader[57].ToString());
                    feeItemList.ItemRateFlag = this.Reader[58].ToString();
                    feeItemList.Item.SpecialFlag1 = this.Reader[59].ToString();
                    feeItemList.Item.SpecialFlag2 = this.Reader[60].ToString();
                    feeItemList.FeePack = this.Reader[61].ToString();
                    feeItemList.UndrugComb.ID = this.Reader[62].ToString();
                    feeItemList.UndrugComb.Name = this.Reader[63].ToString();
                    feeItemList.NoBackQty = NConvert.ToDecimal(this.Reader[64].ToString());
                    feeItemList.ConfirmedQty = NConvert.ToDecimal(this.Reader[65].ToString());
                    feeItemList.ConfirmedInjectCount = NConvert.ToInt32(this.Reader[66].ToString());
                    feeItemList.Order.ID = this.Reader[67].ToString();
                    feeItemList.RecipeSequence = this.Reader[68].ToString();
                    feeItemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[69].ToString());
                    feeItemList.SpecialPrice = NConvert.ToDecimal(this.Reader[70].ToString());
                    feeItemList.FT.ExcessCost = NConvert.ToDecimal(this.Reader[71].ToString());
                    feeItemList.FT.DrugOwnCost = NConvert.ToDecimal(this.Reader[72].ToString());
                    feeItemList.FTSource = this.Reader[73].ToString();
                    feeItemList.Item.IsMaterial = NConvert.ToBoolean(this.Reader[74].ToString());
                    feeItemList.IsAccounted = NConvert.ToBoolean(this.Reader[75].ToString());
                    feeItemList.UpdateSequence = NConvert.ToInt32(this.Reader[76].ToString());

                    // ���� 1 Ϊ���ϴ�
                    feeItemList.Item.UserCode = this.Reader[77].ToString().Trim();
                    feeItemList.User03 = this.Reader[78].ToString().Trim();

                    // ����ֵ
                    feeItemList.FT.TotCost = feeItemList.FT.PubCost + feeItemList.FT.PayCost + feeItemList.FT.OwnCost;

                    feeItemLists.Add(feeItemList);
                }//ѭ������

                this.Reader.Close();

                return feeItemLists;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        #endregion
        /// <summary>
        /// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��(�Ѿ��շ���Ϣ)
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">��������ˮ��</param>
        /// <returns>�ɹ�:������ϸʵ�� ʧ�ܻ���û������:null</returns>
        public FeeItemList GetFeeItemListBalanced(string recipeNO, int sequenceNO) 
        {
            ArrayList feeItemLists = this.QueryFeeItemLists("Fee.Item.GetDrugItemList.WhereFeed", recipeNO, sequenceNO.ToString());

            if (feeItemLists == null)
            {
                return null;
            }

            if (feeItemLists.Count > 0)
            {
                foreach (FeeItemList f in feeItemLists) 
                {
                    if (f.CancelType == CancelTypes.Valid) 
                    {
                        return f;
                    }
                }
            }
            else
            {
                return null;
            }

            return null;
        }

		/// <summary>
		/// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��(������Ϣ)
		/// </summary>
		/// <param name="recipeNO">������</param>
		/// <param name="sequenceNO">��������ˮ��</param>
		/// <returns>�ɹ�:������ϸʵ�� ʧ�ܻ���û������:null</returns>
		public FeeItemList GetFeeItemList(string recipeNO, int sequenceNO)
		{
			ArrayList feeItemLists = this.QueryFeeItemLists("Fee.Item.GetDrugItemList.Where2", recipeNO, sequenceNO.ToString());

			if (feeItemLists == null)
			{
				return null;
			}
			
			if (feeItemLists.Count > 0)
			{
				return feeItemLists[0] as FeeItemList;
			}
			else
			{
				return null;
			}
		}

        //{39B2599D-2E90-4b3d-A027-4708A70E45C3}
        /// <summary>
        /// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��������
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="se">��������ˮ��</param>
        /// <returns></returns>
        public int GetChargeItemCount(string recipeNO, int sequenceNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Item.GetDrugItemList.Where6", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.Item.GetDrugItemList.Where6��SQL���ʧ�ܣ�";
                return -1;
            }
            sql = string.Format(sql, recipeNO, sequenceNO);
            return NConvert.ToInt32(base.ExecSqlReturnOne(sql));
        }


		/// <summary>
		/// ���ݽ������м���ҩƷ��ϸ
		/// </summary>
		/// <param name="invoiceSequence">��������</param>
		/// <returns>�ɹ�:ҩƷ��ϸ ʧ��:null û������: ����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryDrugFeeItemListByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryFeeItemLists("Fee.Item.GetDrugItemList.Where", invoiceSequence);
		}

		/// <summary>
		///���ݽ������м�����ҩƷ��ϸ
		/// </summary>
		/// <param name="invoiceSequence">��������</param>
		/// <returns>�ɹ�:��ҩƷ��ϸ ʧ��:null û������: ����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryUndrugFeeItemListByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryFeeItemLists("Fee.Item.GetUndrugItemList.Where", invoiceSequence);
		}


        //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
        /// <summary>
        /// ���ݽ������м���������ϸ
        /// </summary>
        /// <param name="invoiceSequence">�������</param>
        /// <returns>�ɹ�:������ϸ ʧ��: null û������: ����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryMateFeeItemListByInvoiceSequence(string invoiceSequence)
        {
            return this.QueryFeeItemLists("Fee.Item.GetMateItemList.Where", invoiceSequence);
        }

		/// <summary>
		/// ���ݽ������л�÷�����ϸ
		/// </summary>
		/// <param name="invoiceSequence"></param>
		/// <returns></returns>
		public ArrayList QueryFeeItemListsByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
		}

        /// <summary>
        /// ���ݽ������л�÷�����ϸ//{2044475C-E8CE-454B-B328-90EAAC174D1A} ��ѯ������ϸ��ӷѱ�����
        /// </summary>
        /// <param name="invoiceSequence"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsAndNameByInvoiceSequence(string invoiceSequence)
        {
            return this.QueryFeeItemListsAndFeeName("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
        }

        private ArrayList QueryFeeDetailBySqlNew(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }
            ArrayList feeItemLists = new ArrayList();//������ϸ����
            FeeItemList feeItemList = null;//������ϸʵ��
            try
            {
                while (this.Reader.Read())
                {
                    feeItemList = new FeeItemList();
                    feeItemList.Item.ID = this.Reader[0].ToString();
                    feeItemList.Item.Name = this.Reader[1].ToString();
                    feeItemList.Item.Specs = this.Reader[2].ToString();
                    feeItemList.Item.Price = NConvert.ToDecimal(this.Reader[3].ToString());
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeItemList.Days = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeItemList.Item.PriceUnit = this.Reader[6].ToString();
                    feeItemList.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());
                    feeItemLists.Add(feeItemList);
                }
                this.Reader.Close();

                return feeItemLists;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }


        private ArrayList QueryFeeItemListsNew(string sqlStr, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            sql = sqlStr;
            //string where = string.Empty;//WHERE���

            ////���Where���
            //if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            //{
            //    this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

            //    return null;
            //}

            //sql = this.GetSqlFeeDetail();

            return this.QueryFeeDetailBySqlNew(sql, args);
        }

        /// <summary>
        /// ���ݽ������л�÷�����ϸnew
        /// </summary>
        /// <param name="invoiceSequence"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByInvoiceSequenceNew(string invoiceSequence)
        {
            string sqlStr = string.Empty;
            sqlStr = @"select 
 t.item_code ���,
 t.item_name ����, 
 t.specs, 
 decode(t.ext_flag3, '1', t.unit_price, round(t.unit_price / t.pack_qty, 4)) �۸�, 
 t.qty,
 t.days, 
 --t.price_unit, 
  (select m.min_unit from pha_com_baseinfo m where m.drug_code = t.item_code) price_unit,
 t.own_cost + t.pay_cost + t.pub_cost �ܽ��
  from fin_opb_feedetail t
 where t.invoice_seq = '{0}'
   and t.cancel_flag = '1'      
   and t.package_code is null   
   
   union all
select mm.���,mm.����,mm.specs,sum(mm.�۸�),package_qty,mm.days,mm.price_unit,sum(�ܽ��)
from 
(
select ���,
       ����,
       specs,
       �۸�,
       sum(package_qty) package_qty,
       days,
       price_unit,
       sum(�ܽ��) �ܽ��
  from (
         select        
         t.package_code ���,          
          t.package_name ����,          
          '' specs,          
          -- SUM(T.UNIT_PRICE) AS �۸�,           
         -- sum(decode(t.qty, '1', t.unit_price, t.unit_price * t.qty)) as �۸�,
          sum(decode (t.qty,'1',t.unit_price,t.unit_price*m.qty)) as �۸�,
          t.package_qty,          
          -- t.qty,          
          t.days,          
          '��' price_unit,
 decode (t.package_qty,'1',SUM(T.UNIT_PRICE * T.Qty * T.Package_Qty),
 decode(t.qty,'1', SUM(T.UNIT_PRICE * T.Package_Qty), SUM(T.UNIT_PRICE *m.qty* T.Package_Qty)
 ) )AS �ܽ��                
          from fin_opb_feedetail t,fin_com_undrugztinfo m        
         where t.invoice_seq = '{0}'
           and t.cancel_flag = '1'              
           and t.package_code is not null
           and m.package_code=t.package_code
           and m.item_code = t.item_code
         group by t.package_code,
                   t.package_name,
                   t.package_qty,
                   t.days,
                   t.comb_no ,t.qty 
        )
 group by ���, ����, specs, �۸�,package_qty,days, price_unit
 ) mm
 group by  mm.���,mm.����,mm.specs,mm.package_qty,mm.days,mm.price_unit";
            return this.QueryFeeItemListsNew(sqlStr, invoiceSequence);

        }

		/// <summary>
		/// ����һ����Ʊ�ţ���ȡȫ���ֵܷ�Ʊ�ŵķ�����ϸ
		/// </summary>
		/// <param name="invoiceNO"></param>
		/// <returns></returns>
		public ArrayList QueryFeeItemListsSameInvoiceCombNOByInvoiceNO(string invoiceNO)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where8", invoiceNO);
		}


		/// <summary>
		/// ���ݷ�Ʊ�Ż�ȡ������ϸ
		/// </summary>
		/// <param name="invoiceNO">����ķ�Ʊ��</param>
		/// <param name="dataSet">���صĽ�����ݼ�</param>
		/// <returns>�ɹ� 1 ʧ��: -1</returns>
		public int QueryFeeItemListsByInvoiceNO(string invoiceNO, ref DataSet dataSet)
		{
			return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where", ref dataSet, invoiceNO);
		}

        /// <summary>
        /// ͨ��ҽ����Ŀ��ˮ�Ż��������Ŀ��ˮ�ţ��õ�������ϸ
        /// </summary>
        /// <param name="MOOrder">ҽ����Ŀ��ˮ�Ż��������Ŀ��ˮ��</param>
        /// <returns>null ���� ArrayList Fee.OutPatient.FeeItemListʵ�弯��</returns>
        public ArrayList QueryFeeDetailFromMOOrder(string MOOrder)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromMOOrder.Select.1", MOOrder);
        }

        /// <summary>
        /// ����ҽ����ˮ�Ų�ѯ�����շѵķ�����Ϣ
        /// </summary>
        /// <param name="MOOrder"></param>
        /// <returns></returns>
        public FeeItemList QueryFeeItemListFromMOOrder(string MOOrder)
        {
            ArrayList al = this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromMOOrder.Select.1", MOOrder);
            if (al == null || al.Count == 0)
            {
                this.Err = "��ѯ���߷�����Ϣʧ�ܣ�";
                return null;
            }
            return al[0] as FeeItemList;
        }

        /// <summary>
        /// ͨ�������ţ��õ�������ϸ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <returns>null ���� ArrayList Fee.OutPatient.FeeItemListʵ�弯��</returns>
        public ArrayList QueryFeeDetailFromRecipeNO(string recipeNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromRecipeNo.Select.1", recipeNO);
        }

        /// <summary>
        /// ͨ�������ţ��õ�������ϸ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <returns>null ���� ArrayList Fee.OutPatient.FeeItemListʵ�弯��</returns>
        public ArrayList QueryFeeDetailFromRecipeNOForHistoryRecipe(string recipeNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromRecipeNo.Select.1.ForHistoryRecipe", recipeNO);
        }

        /// <summary>
        /// ͨ��������ˮ�ź���Ϻŵõ����շ�δ�˷ѵķ�����ϸ
        /// </summary>
        /// <param name="ComoNO"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryValidFeeDetailbyComoNOAndClinicCode(string ComoNO, string clinicCode)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailFromComoIdAndClinicCode.Select.2", ComoNO, clinicCode);
        }

        /// <summary>
        /// ͨ��������ˮ�ź��շ���ŵõ�δ�շѵķ�����ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryValidFeeDetailbyClinicCodeAndRecipeSeq(string clinicCode, string recipeNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailbyClinicCodeAndRecipeSeq", clinicCode, recipeNO);
        }

        /// <summary>
        /// ͨ��������ˮ�źͿ�����ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNONotNull(string clinicCode, string feeFlag)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.QueryFeeDetailByClinicCodeAndSeeNONotNull", clinicCode, feeFlag);
        }

        /// <summary>
        /// ͨ��������ˮ�źͿ�����ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndSeeNO(string clinicCode,string seeNO, string feeFlag)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.QueryFeeDetailByClinicCodeAndSeeNO", clinicCode,seeNO, feeFlag);
        }

        /// <summary>
        /// ͨ��������ˮ�źʹ����ŵõ�������ϸ 2012-8-30BY yyj
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndRecipeNO(string clinicCode, string recipeNO, string feeFlag)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.QueryFeeDetailByClinicCodeAndRecipeNO", clinicCode, recipeNO, feeFlag);
        }

        /// <summary>
        /// ��������ϸ����Ƿ���Ҫ�ն�ȷ����Ϣ 2013-8-15 yerl
        /// </summary>
        /// <param name="feeItemLists">������ϸ</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailIfNeedConfirm(ArrayList feeItemLists)
        {
            //���SQL���
            string sql=string.Empty;
			if (this.Sql.GetCommonSql("Fee.Item.GetNeedConfirmFlag", ref sql) == - 1)
			{
				this.Err = "û���ҵ�����Ϊ:" + sql + "��SQL���";

				return feeItemLists;
			}
            foreach (FeeItemList itemList in feeItemLists)
            {
                if (itemList.Item.ItemType == EnumItemType.UnDrug)
                {
                    string sqlwhere = string.Format(sql, itemList.Item.ID);
                    this.ExecQuery(sqlwhere);
                    while (this.Reader.Read())
                    {
                        itemList.Item.NeedConfirm = (FS.HISFC.Models.Fee.Item.EnumNeedConfirm)NConvert.ToInt32(this.Reader[0]);
                    }
                    Reader.Close();
                }
            }
            return feeItemLists;
        }

        /// <summary>
        /// ͨ��������ˮ�ź��շ���ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCodeAndRecipeSeq(string clinicCode, string recipeSeq, string feeFlag)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailbyRecipeSeq", clinicCode, recipeSeq, feeFlag);
        }

        /// <summary>
        /// ͨ��������ˮ�ŵõ�������ϸ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <param name="feeFlag">ALL ȫ�� 0���� 1�շ� 3Ԥ�շ�������� 4 ҩƷԤ���</param>
        /// <returns></returns>
        public ArrayList QueryFeeDetailByClinicCode(string clinicCode,  string feeFlag)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetailbyClinicCode", clinicCode, feeFlag);
        }

		#endregion

		#region ����Զ����ɵĿ���
		
		/// <summary>
		/// ����Զ����ɵĿ��ţ� ��ҪΪ�շ�ֱ�����뻼����Ϣʱ���ɡ�
		/// </summary>
		/// <returns>�ɹ�:�Զ����ɵĿ��� ʧ��:null </returns>
		public string GetAutoCardNO()
		{
			string tempCardNo = this.GetSequence("Fee.OutPatient.GetAutoCardNo.Select");
			
			return tempCardNo.PadLeft(9, '0');
		}
        /// <summary>
        /// �������֤��ÿ��š�
        /// </summary>
        public DataTable GetAutoCardNObyIdenno(string idenno)
        {
            string format = string.Empty;
            if (this.Sql.GetCommonSql("Fee.OutPatient.GetAutoCardNo.SelectByIdenno", ref format) == -1)
            {
                //this.Err("û���ҵ�����ΪFee.OutPatient.GetAutoCardNo.SelectByIdenno��SQL���");
                return null;
            }
            format = string.Format(format, idenno);
            DataSet set = null;
            if (this.ExecQuery(format, ref set) == -1)
            {
                //this.Err("ִ��SQL���ʧ�ܣ�");
                return null;
            }
            return set.Tables[0];
        }


		#endregion

		#region ����շ����к�

		/// <summary>
		/// ����շ����к�
		/// </summary>
		/// <returns>�ɹ�:�շ����к� ʧ��:null</returns>
		public string GetRecipeSequence()
		{
			return this.GetSequence("Fee.OutPatient.GetRecipeSeq.Select");
		}

		#endregion

		#region �������

		/// <summary>
		/// ��÷�Ʊ��Ϻ�
		/// </summary>
		/// <returns>�ɹ�:��Ʊ��Ϻ� ʧ�� null</returns>
		public string GetInvoiceCombNO()
		{
			return this.GetSequence("Fee.OutPatient.GetInvoiceSeq.Select");
		}
		
		/// <summary>
		/// ���뷢Ʊ��Ϣ
		/// </summary>
		/// <param name="balance">��Ʊ��Ϣʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�в������ݷ��� 0</returns>
		public int InsertBalance(Balance balance)
		{
			return this.UpdateSingleTable("Fee.OutPatient.InvoInfo.Insert", this.GetBalanceParams(balance));
		}

		/// <summary>
		/// ���·�Ʊ��Ϣ
		/// </summary>
		/// <param name="balance">��Ʊ��Ϣʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�и������ݷ��� 0</returns>
		public int UpdateBalance(Balance balance)
		{
			return this.UpdateSingleTable("Fee.OutPatient.InvoInfo.Update", this.GetBalanceParams(balance));
		}

        /// <summary>
         /// �ظ�ҽ����Ʊ��Ϣ
         /// </summary>
         /// <param name="balance"></param>
         /// <returns></returns>
         public int UpdateSIBalanceInvoiesInfo(Balance balance)
         {
             return this.UpdateSingleTable("Fee.OutPatient.InvoInfo.balance.Update", this.GetBalanceParams(balance));
 
         }

		#endregion

		#region ������ϸ����

		/// <summary>
		/// ���������ϸ
		/// </summary>
		/// <param name="balanceList">������ϸʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�в������ݷ��� 0</returns>
		public int InsertBalanceList(BalanceList balanceList)
		{
			return this.UpdateSingleTable("Fee.OutPatient.InvoDetail.Insert", this.GetBalanceListParams(balanceList));
		}

		/// <summary>
		/// ���½�����ϸ
		/// </summary>
		/// <param name="balanceList">������ϸʵ��</param>
		/// <returns>�ɹ�: 1 ʧ��: -1 û�и������ݷ��� 0</returns>
		public int UpdateBalanceList(BalanceList balanceList)
		{
			return this.UpdateSingleTable("Fee.OutPatient.InvoDetail.Update", this.GetBalanceListParams(balanceList));
		}

		#endregion

		#region �������

		/// <summary>
		/// ���ݷ�Ʊ��,�������ڵķ�Ʊ��Ŀ
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�:��Ʊ����Ŀ ʧ�� -1</returns>
		public string QueryExistInvoiceCount(string invoiceNO)
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.OutPatient.QueryExistInvoiceCount.Select.1", ref sql) == -1)
			{
				this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.QueryExistInvoiceCount.Select.1 ��SQL���";
				
				return "-1";
			}
			
			return this.ExecSqlReturnOne(sql, invoiceNO);
		}

		/// <summary>
		/// �õ���ǰ����Ա�ӵ�ǰ��ʼ����ǰN�ŷ�Ʊ����Ϣ
		/// </summary>
		/// <param name="count">����</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesByCount(int count)
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.OutPatient.GetSpecifyCountsInfosSinceNow.Select.1", ref sql) == -1)
			{
				this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetSpecifyCountsInfosSinceNow.Select.1 ��SQL���";

				return null;
			}

			return this.QueryBalancesBySql(sql, (count + 1).ToString());
		}
        /// <summary>
        /// �õ���ǰ����Ա�ӵ�ǰ��ʼ����ǰN�ŷ�Ʊ����Ϣ
        /// </summary>
        /// <param name="count">����</param>
        /// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
        public ArrayList QueryBalancesByCount(string operCode,int count)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.OutPatient.GetSpecifyCountsInfosSinceNow.Select.2", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetSpecifyCountsInfosSinceNow.Select.2 ��SQL���";

                return null;
            }

            return this.QueryBalancesBySql(sql, operCode,(count ).ToString());
        }
		/// <summary>
		/// ��û��ߵ������׷�Ʊ��Ϣ����Ʊ�ش���
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesValidByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalances("Fee.OutPatient.GetValidInvoiceInfo.Where.1", invoiceNO);
		}

        /// <summary>
        /// ���ݿ��Ų�ѯ���������ķ�Ʊʵ�弯��
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryBalancesByCardNO(string cardNO)
        {
            return this.QueryBalances("Fee.OutPatient.GetInvoiceInfoByPatientCardNo.Where.2", cardNO);
        }

		/// <summary>
		/// ���ݻ��߿��ź�ʱ��β��ҷ��������ķ�Ʊʵ�弯��
		/// </summary>
		/// <param name="cardNO">���߿���</param>
		/// <param name="beginTime">��ʼʱ��</param>
		/// <param name="endTime">����ʱ��</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesByCardNO(string cardNO, DateTime beginTime, DateTime endTime)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoiceInfoByPatientCardNo.Where.1", cardNO, beginTime.ToString(), endTime.ToString());
		}

		/// <summary>
		/// ���ݻ���������ʱ��β��ҷ��������ķ�Ʊʵ�弯��
		/// </summary>
		/// <param name="name">��������</param>
		/// <param name="beginTime">��ʼʱ��</param>
		/// <param name="endTime">����ʱ��</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesByName(string name, DateTime beginTime, DateTime endTime)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoiceInfoByPatientName.Where.1", name, beginTime.ToString(), endTime.ToString());
		}

		/// <summary>
		/// ͨ����Ʊ�Ż�����н�����Ϣ
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoInfo.Where", invoiceNO);
		}

		/// <summary>
		/// ��������Ʊ�ţ���ȡ��ͬ������ŵĽ�����Ϣ(��Ч������Ϣ)   
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesSameInvoiceCombNOByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoInfo.Where7", invoiceNO);
		}

		/// <summary>
		/// ���ݽ������,��ȡ��ͬ������ŵĽ�����Ϣ(��Ч������Ϣ)   
		/// </summary>
		/// <param name="invoiceSequence">�������</param>
		/// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
		public ArrayList QueryBalancesByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
		}
		
		/// <summary>
		/// ���ݷ�Ʊ�Ż�ý�����Ϣ��DataSet
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <param name="dataSet">������ϢDataSet</param>
		/// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int QueryBalancesByInvoiceNO(string invoiceNO, ref DataSet dataSet)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoInfo.Where", ref dataSet, invoiceNO);
		}

		/// <summary>
		/// ���ݻ���������ý�����Ϣ��DataSet
		/// </summary>
		/// <param name="name">���뻼������</param>
		/// <param name="beginTime">��ѯ����ʼ����</param>
		/// <param name="endTime">��ѯ�Ľ�ֹ����</param>
		/// <param name="dataSet">���صĽ�����ݼ�</param>
		/// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int QueryBalancesByPatientName(string name, DateTime beginTime, DateTime endTime, ref DataSet dataSet)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoiceInformationByName.Where", ref dataSet, name, beginTime.ToString(), endTime.ToString());
		}

		/// <summary>
		/// ���ݲ����Ż�ý�����ϢDataSet
		/// </summary>
		/// <param name="cardNO">���߲�����</param>
		/// <param name="beginTime">��ʼʱ��</param>
		/// <param name="endTime">����ʱ��</param>
		/// <param name="dataSet">���صĽ�����ݼ�</param>
		/// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int QueryBalancesByCardNO(string cardNO, DateTime beginTime, DateTime endTime, ref DataSet dataSet)
		{
			return this.QueryBalances("Fee.OutPatient.GetInvoiceInformationByCardNo.Where", ref dataSet, cardNO, beginTime.ToString(), endTime.ToString());
		}
        /// <summary>
        /// ��ȡ��Ʊ��Ϣ
        /// {2E5139C9-52D8-4fec-A96B-09BECFDDFBD1}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <param name="regDateEnd"></param>
        /// <param name="lstInvoice"></param>
        /// <returns></returns>
        public int QueryInvoiceInfoByCardNo(string cardNo, DateTime regDate, DateTime regDateEnd, out List<Balance> lstInvoice)
        {
            lstInvoice = new List<Balance>();
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoInfo", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetInvoInfo ��SQL���";

                return -1;
            }
            string where = string.Empty;
            if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoInfo.Where9", ref where) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetInvoInfo.Where9 ��SQL���";

                return -1;
            }

            sql = sql + where;

            ArrayList arlBalace = this.QueryBalancesBySql(sql, cardNo, regDate.ToString(), regDateEnd.ToString());

            if (arlBalace != null && arlBalace.Count > 0)
            {
                lstInvoice.AddRange((Balance[])arlBalace.ToArray(typeof(Balance)));
            }

            return 1;
        }
        /// <summary>
        /// ��ȡ�������շ�ָ����ͬ��λ���ͷ�Ʊ��Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <param name="regDateEnd"></param>
        /// <param name="pact_code"></param>
        /// <param name="lstInvoice"></param>
        /// <returns></returns>
        public int QueryInvoiceInfoByPactAndDate(string cardNo, DateTime regDate, DateTime regDateEnd, string pact_code, out List<Balance> lstInvoice)
        {
            lstInvoice = new List<Balance>();
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoInfo", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetInvoInfo ��SQL���";

                return -1;
            }
            string where = string.Empty;
            if (this.Sql.GetCommonSql("Fee.OutPatient.GetInvoInfo.Where10", ref where) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Fee.OutPatient.GetInvoInfo.Where10 ��SQL���";

                return -1;
            }

            sql = sql + where;

            ArrayList arlBalace = this.QueryBalancesBySql(sql, cardNo, regDate.ToString(), regDateEnd.ToString(), pact_code);

            if (arlBalace != null && arlBalace.Count > 0)
            {
                lstInvoice.AddRange((Balance[])arlBalace.ToArray(typeof(Balance)));
            }

            return 1;
        }


		#endregion

		#region ������ϸ����
		
		/// <summary>
		/// ͨ����Ʊ�Ż�����еĽ�����ϸ
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�:������ϸ��Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalanceListsByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalanceLists("Fee.OutPatient.GetInvoDetail.Where", invoiceNO);
		}

		/// <summary>
		/// ����һ����Ʊ�� ��ȡ������ͬ�������еĽ�����ϸ(��Ч�Ľ�����ϸ)
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <returns>�ɹ�:������ϸ��Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalanceListsSameInvoiceCombNOByInvoiceNO(string invoiceNO)
		{
			return this.QueryBalanceLists("Fee.OutPatient.GetBalanceBrotherInvoDetail.Where", invoiceNO);
		}
		/// <summary>
		/// ���ݽ������л�ý�����ϸ(��Ч�Ľ�����ϸ)
		/// </summary>
		/// <param name="invoiceSequence">��������</param>
		/// <returns>�ɹ�:������ϸ��Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalanceListsByInvoiceSequence(string invoiceSequence)
		{
			return this.QueryBalanceLists("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
		}

		/// <summary>
		/// ���ݷ�Ʊ�źͽ������л�ý�����ϸ(��Ч�Ľ�����ϸ)
		/// </summary>
		/// <param name="invoiceNO">��Ʊ��</param>
		/// <param name="invoiceSequence">��������</param>
		/// <returns>�ɹ�:������ϸ��Ϣ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryBalanceListsByInvoiceNOAndInvoiceSequence(string invoiceNO, string invoiceSequence)
		{
			return this.QueryBalanceLists("Fee.OutPatient.GetBalanceBrotherInvoDetailBySeq.Where.1", invoiceNO, invoiceSequence);
		}

		/// <summary>
		/// ���ݷ�Ʊ�Ż�ȡ��Ʊ��ϸ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="invoiceNO">����ķ�Ʊ��</param>
		/// <param name="dataSet">���صĽ�����ݼ�</param>
		/// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int QueryBalanceListsByInvoiceNO(string invoiceNO, ref DataSet dataSet)
		{
			return this.QueryBalanceLists("Fee.OutPatient.GetInvoInfo.Where", ref dataSet, invoiceNO);
		}

		#endregion

		#region ������Ŀ�б����

		/// <summary>
		/// �������������Ŀ�б�
		/// </summary>
		/// <param name="deptCode">�շ�Ա���ڿ���</param>
		/// <param name="ds">��Ŀ�б�</param>
		/// <returns> -1 ʧ�� > 0 �ɹ�</returns>
		public int QueryItemList(string deptCode, ref DataSet ds)
		{			
			return this.ExecQuery("Fee.Item.GetOutPatientItemList.Select",ref ds, deptCode);
		}

		#endregion
        #region {5D62CB1F-6134-48f4-B905-02AD69D6A433}
        /// <summary>
        /// �������������Ŀ�б�
        /// </summary>
        /// <param name="deptCode">�շ�Ա���ڿ���</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="ds">��Ŀ�б�</param>
        /// <returns> -1 ʧ�� > 0 �ɹ�</returns>
        public int QueryItemList(string deptCode, string itemCode, ref DataSet ds)
        {
            return this.ExecQuery("Fee.Item.GetOutPatientItemList.Select.ItemCode", ref ds, deptCode, itemCode);
        }

          #endregion
        /// <summary>
        /// �������������Ŀ�б�
        /// </summary>
        /// <param name="deptCode">�շ�Ա���ڿ���</param>
        /// <param name="itemKind">��Ŀ�б����</param>
        /// <param name="ds">��Ŀ�б�</param>
        /// <returns> -1 ʧ�� > 0 �ɹ�</returns>
        public int QueryItemList(string deptCode, FS.HISFC.Models.Base.ItemKind itemKind, ref DataSet ds)
        {
            if (itemKind == ItemKind.All)
            {
                return this.ExecQuery("Fee.Item.GetOutPatientItemList.Select", ref ds, deptCode);
            }
            if (itemKind == ItemKind.Undrug)
            {
                return this.ExecQuery("Fee.Item.GetOutPatientItemList.Select.Undrug", ref ds, deptCode);
            }
            if (itemKind == ItemKind.Pharmacy)
            {
                return this.ExecQuery("Fee.Item.GetOutPatientItemList.Select.Pharmacy", ref ds, deptCode);
            }
            return 1;
        }
		#region �˷�ҵ��

		/// <summary>
		/// ����ԭʼ��Ʊ�Ÿ��·�����ϸ����Ч��־
		/// </summary>
		/// <param name="orgInvoiceNO">ԭʼ��Ʊ��</param>
		/// <param name="operTime">����ʱ��</param>
		/// <param name="cancelType">��������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int UpdateFeeItemListCancelType(string orgInvoiceNO, DateTime operTime, CancelTypes cancelType)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailCancelFlag.1", orgInvoiceNO, operTime.ToString(), ((int)cancelType).ToString());
		}

		/// <summary>
		/// ������Ŀ��ˮ�ź�����ˮ�Ÿ��·�����ϸ����Ч��־
		/// </summary>
		/// <param name="recipeNO">������</param>
		/// <param name="recipeSequence">��������ˮ��</param>
		/// <param name="cancelType">��������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int UpdateFeeItemListCancelType(string recipeNO, int recipeSequence, CancelTypes cancelType)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailCancelFlag", recipeNO, recipeSequence.ToString(), ((int)cancelType).ToString());
		}

		/// <summary>
		/// ����ԭʼ��Ʊ�źͽ�����Ÿ��½�����Ϣ
		/// </summary>
		/// <param name="orgInvoiceNO">ԭʼ��Ʊ��</param>
		/// <param name="invoiceSequence">��������</param>
		/// <param name="operTime">����ʱ��</param>
		/// <param name="cancelType">��������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int UpdateBalanceCancelType(string orgInvoiceNO, string invoiceSequence, DateTime operTime, CancelTypes cancelType)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateInvoCancelFlag", orgInvoiceNO, invoiceSequence, operTime.ToString(), ((int)cancelType).ToString());
		}

		/// <summary>
		/// ����ԭʼ��Ʊ�źͽ�����Ÿ��½�����ϸ��Ϣ
		/// </summary>
		/// <param name="orgInvoiceNO">ԭʼ��Ʊ��</param>
		/// <param name="invoiceSequence">��������</param>
		/// <param name="operTime">����ʱ��</param>
		/// <param name="cancelType">��������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int UpdateBalanceListCancelType(string orgInvoiceNO, string invoiceSequence, DateTime operTime, CancelTypes cancelType)
		{
			return this.UpdateSingleTable("Fee.OutPatient.UpdateInvoDetailCancelFlag", orgInvoiceNO, invoiceSequence, operTime.ToString(), ((int)cancelType).ToString());
		}

        /// <summary>
        /// ����ԭʼ��Ʊ�źͽ�����Ÿ��½���֧����ʽ��Ϣ
        /// </summary>
        /// <param name="orgInvoiceNO">ԭʼ��Ʊ��</param>
        /// <param name="invoiceSequence">��������</param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="cancelType">��������</param>
        /// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
        public int UpdateBalancePayModeCancelType(string orgInvoiceNO, string invoiceSequence, DateTime operTime, CancelTypes cancelType)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdatePayModeCancelFlag", orgInvoiceNO, invoiceSequence, operTime.ToString(), ((int)cancelType).ToString());
        }


		#endregion

		#region ��Ʊ�ش�ҵ��

		/// <summary>
		/// ���Ϸ�����Ϣ��
		/// </summary>
		/// <param name="type">����: 1 �������� 2 ������ϸ�� 3 ������ϸ�� 4 ֧����ʽ��</param>
		/// <param name="invoiceSequence">�������</param>
		/// <param name="cancelType">��������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int UpdateCancelTyeByInvoiceSequence(string type, string invoiceSequence, CancelTypes cancelType)
		{
			string sql = string.Empty; //SQL���
			string index = string.Empty;; //SQL�������

			switch(type)
			{
				case "1"://��Ʊ����
					index = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.Invoice";
					break;
				case "2"://��Ʊ��ϸ��
					index = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.InvoiceDetail";
					break;
				case "3"://������ϸ��
					index = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.FeeDetail";
					break;
				case "4"://֧����ʽ
					index = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.PayMode";
					break;
			}

			return this.UpdateSingleTable(index, invoiceSequence, ((int)cancelType).ToString());
		}

		#endregion

		#region ��Ʊע��

        /// <summary>
        /// �����˻�֧���ķ�Ʊ����������ҽ���۳��Һŷѵ��˷�
        /// </summary>
        /// <param name="invoiceNo">��Ʊ��</param>
        /// <param name="invoiceSeq">��Ʊ���</param>
        /// <param name="payCost">�˻����</param>
        /// <returns>1 ���ϳɹ������˻���0 ���˻�֧������������ -1 ����</returns>
        public int LogOutInvoiceByAccout(string invoiceNo,string invoiceSeq,ref decimal payCost)
        {
            #region �ж�֧����ʽ�ǲ����˻�
            payCost = 0;
            ArrayList alPayMode = this.QueryBalancePaysByInvoiceNO(invoiceNo);
            if (alPayMode == null)
            {
                return -1;
            }

            foreach (BalancePay payObj in alPayMode)
            {
                if (payObj.PayType.ID == "YS")
                {
                    payCost += payObj.FT.TotCost;
                }
                else
                {
                    this.Err = "��Ʊ" + payObj.Invoice.ID + "֧����ʽ�����˻����������Ϸ�Ʊ��";
                    return -1;
                }
            }
            #endregion

            //���Ϸ�Ʊ
            if (this.LogOutInvoice(invoiceSeq) == -1)
            {
                return -1;
            }

            return 1;
        }

		/// <summary>
		/// ��Ʊע��
		/// </summary>
		/// <param name="invoiceSequence">�������</param>
		/// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
		public int LogOutInvoice(string invoiceSequence)
		{
			if (invoiceSequence == string.Empty)
			{
				this.Err = "��ˮ�ų���";

				return -1;
			}

			int iReturn = 0;

			iReturn = UpdateCancelTyeByInvoiceSequence("1", invoiceSequence, CancelTypes.LogOut);
			if (iReturn <= 0)
			{
				this.Err += "���·�Ʊ�������!";
				
				return iReturn;
			}

			iReturn = UpdateCancelTyeByInvoiceSequence("2", invoiceSequence, CancelTypes.LogOut);
			if (iReturn <= 0)
			{
				this.Err += "���·�Ʊ��ϸ����!";
				
				return iReturn;
			}

			iReturn = UpdateCancelTyeByInvoiceSequence("3", invoiceSequence, CancelTypes.LogOut);
			if (iReturn == -1)
			{
				this.Err += "���·�����ϸ����!";
				return iReturn;
			}
			if( iReturn == 0)
			{
				this.Err += "��Ʊ����Ŀ�Ѿ�ȷ�ϣ�����ȡ��!";
				return -1;
			}

			iReturn = UpdateCancelTyeByInvoiceSequence("4", invoiceSequence, CancelTypes.LogOut);
			if (iReturn <= 0)
			{																									
				this.Err += "����֧����Ϣ�����!";
				return -1;
			}
			
			return iReturn;
		}

		#endregion


        #region ɾ�������������ܻ�����Ϣ
        /// <summary>
        /// ���������ˮ�źͷ�Ʊ��Ϻ�ɾ����������Ϣ��
        /// </summary>
        /// <param name="ClinicNO">�����ˮ��</param>
        /// <param name="RecipeNO">��Ʊ��Ϻ�</param>
        /// <returns></returns>
        public int DeleteFeeItemListByClinicNOAndRecipeNO(string ClinicNO, string RecipeNO)
        {
            string sql = string.Empty; //��ѯSQL���

            if (this.Sql.GetCommonSql("Fee.InvoiceService.DeleteFeeItemListByClinicNOAndRecipeNO", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.DeleteFeeItemListByClinicNOAndRecipeNO��SQL���";

                return -1;
            }
            sql = string.Format(sql, ClinicNO, RecipeNO);

            return this.ExecNoQuery(sql);
        }
        #endregion 
       
        #region ��ѯ��Ʊ��Ϻ��Ƿ��Ѿ��շ�
        /// <summary>
        /// ���ݷ�Ʊ��ϺŲ�ѯ��������Ϣ�Ƿ��շ� ��
        /// </summary>
        /// <param name="RecipeSeq">��Ʊ��Ϻ�</param>
        /// <returns>0 ���շѣ� 1 δ�շ� ��-1 ��ѯ����</returns>
        public int IsFeeItemListByRecipeNO(string RecipeSeq)
        {
            string strSql1 = "";
            string strSql2 = "";
            ArrayList list = new ArrayList();
            //�����Ŀ��ϸ��SQL���
            strSql1 = this.GetSqlFeeDetail();
            if (this.Sql.GetCommonSql("Fee.Item.IsFeeItemListByRecipeNO.Where", ref strSql2) == -1) return -1;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, RecipeSeq);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            list = this.QueryFeeDetailBySql(strSql1);
            if(list == null)
            {
                return -1; //����
            }
            if (list.Count == 0)
            {
                return 1;
            }
            foreach (FeeItemList feeItemList in list)
            {
                if (feeItemList.PayType == PayTypes.Balanced) //����Ѿ��շ�
                {
                    return 0; 
                }
            }
            return 1; //û���շ� 
        }
        #endregion 

        #region ���¿�ȡ�˻���־

        /// <summary>
        /// ���·�����ϸ,�Ƿ��Ѿ���ȡ�˻�(���մ�����,�ʹ�����ˮ��)
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">������ˮ��</param>
        /// <param name="isAccounted">�Ƿ��Ѿ���ȡ�˻�</param>
        /// <returns>�ɹ� 1 �����ϸ������� 0 ���� -1</returns>
        public int UpdateAccountFlag(string recipeNO, int sequenceNO, bool isAccounted) 
        {
            return this.UpdateSingleTable("Fee.Outpatient.UpdateAccountFlag.RecipeNO", recipeNO, sequenceNO.ToString(), NConvert.ToInt32(isAccounted).ToString());
        }

        /// <summary>
        /// ���·�����ϸ,�Ƿ��Ѿ���ȡ�˻�(������Ŀ����, ҽ����ˮ��)
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="moOrder">ҽ����ˮ��</param>
        /// <param name="isAccounted">�Ƿ��Ѿ���ȡ�˻�</param>
        /// <returns>�ɹ� 1 �����ϸ������� 0 ���� -1</returns>
        public int UpdateAccountFlag(string itemCode, string moOrder, bool isAccounted) 
        {
            return this.UpdateSingleTable("Fee.Outpatient.UpdateAccountFlag.MoOrder", itemCode, moOrder, NConvert.ToInt32(isAccounted).ToString());            
        }

        #endregion


        #region ����

        /// <summary>
        /// ��÷�Ʊ���࣬������С��������
        /// </summary>
        /// <param name="type">��Ʊ���Ĭ��MZ01</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetInvoiceClass(string type, ref DataSet ds)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Item.GetInvoiceClass.Select", ref sql) == -1) 
            {
                this.Err = "û���ҵ�����Ϊ: " + "Fee.Item.GetInvoiceClass.Select��SQL���";

                return -1;
            }

            sql = string.Format(sql, type);

            return this.ExecQuery(sql, ref ds);
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݴ����Ż����󴦷���ˮ��
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public string GetMaxSeqByRecipeNO(string recipeNO,string clinicCode)
        {
            return this.ExecSqlReturnOne("Fee.OutPatient.GetMaxSeqByRecipeNo", recipeNO,clinicCode);
        }

        /// <summary>
        /// ���ݷ�����ϸ��ѯҽ�������뵥��
        /// {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} �������뵥�� 20100505
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public string GetApplyNoByRecipeFeeSeq(FeeItemList f)
        {
            return this.ExecSqlReturnOne("Fee.OutPatient.GetApplyNoByRecipeFeeSeq", f.RecipeNO, "" + f.SequenceNO);
        }

        #endregion

        #region �ս�

        /// <summary>
        /// ���·�Ʊ�����ս���
        /// </summary>
        /// <param name="beginTime">�սῪʼʱ��</param>
        /// <param name="endTime">�ս����ʱ��</param>
        /// <param name="balanceFlag">�ս���</param>
        /// <param name="balanceNO">�ս����</param>
        /// <param name="balanceDate">�ս�ʱ��</param>
        /// <returns> >=1�ɹ�, 0 û���ҵ����µ��У� -1 ʧ��</returns>
        public int UpdateInvoiceForDayBalance(DateTime beginTime, DateTime endTime, string balanceFlag,
            string balanceNO, DateTime balanceDate)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateInvoiceForDayBalance.Update", beginTime.ToString(), 
                endTime.ToString(), this.Operator.ID, balanceFlag, balanceNO, balanceDate.ToString());
        }

        /// <summary>
        /// ���·�Ʊ��ϸ���ս���
        /// </summary>
        /// <param name="beginTime">�սῪʼʱ��</param>
        /// <param name="endTime">�ս����ʱ��</param>
        /// <param name="balanceFlag">�ս���</param>
        /// <param name="balanceNO">�ս����</param>
        /// <param name="balanceDate">�ս�ʱ��</param>
        /// <returns> >=1�ɹ�, 0 û���ҵ����µ��У� -1 ʧ��</returns>
        public int UpdateInvoiceDetailForDayBalance(DateTime beginTime, DateTime endTime, string balanceFlag,
            string balanceNO, DateTime balanceDate)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateInvoiceDetailForDayBalance.Update", beginTime.ToString(),
                endTime.ToString(), this.Operator.ID, balanceFlag, balanceNO, balanceDate.ToString());
        }

        /// <summary>
        /// ���·�Ʊ֧����ʽ���ս���
        /// </summary>
        /// <param name="dtBegin">�սῪʼʱ��</param>
        /// <param name="dtEnd">�ս����ʱ��</param>
        /// <param name="balanceFlag">�ս���</param>
        /// <param name="balanceNO">�ս����</param>
        /// <param name="balanceDate">�ս�ʱ��</param>
        /// <returns> >=1�ɹ�, 0 û���ҵ����µ��У� -1 ʧ��</returns>
        public int UpdatePayModeForDayBalance(DateTime beginTime, DateTime endTime, string balanceFlag,
            string balanceNO, DateTime balanceDate)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdatePayModeForDayBalance.Update", beginTime.ToString(),
                endTime.ToString(), this.Operator.ID, balanceFlag, balanceNO, balanceDate.ToString());
        }

        #endregion

        #endregion

        #region ��������
        /// <summary>
		/// ���ݷ�Ʊ�Ż�ȡ������ϸ
		/// </summary>
		/// <param name="strInvoice">����ķ�Ʊ��</param>
		/// <param name="dsResult">���صĽ�����ݼ�</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		[Obsolete("����,ʹ��QueryFeeItemListsByInvoiceNO", true)]
		public int QueryFeeDetailByInvoiceNo(string strInvoice, ref System.Data.DataSet dsResult)
		{
			return 1;
		}
		
		/// <summary>
		/// ���ݷ�Ʊ�Ż�ȡ��Ʊ��ϸ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="strInvoice">����ķ�Ʊ��</param>
		/// <param name="dsResult">���صĽ�����ݼ�</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		[Obsolete("����,ʹ��QueryBalanceListsByInvoiceNO", true)]
		public int QueryInvoiceDetailByInvoiceNo(string strInvoice, ref System.Data.DataSet dsResult)
		{
			return 1;
		}


		/// <summary>
		/// ���ݲ����Ų�ѯ��Ʊ������Ϣ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="strCard">����Ĳ�����</param>
		/// <param name="dsResult">���صĽ�����ݼ�</param>
		/// <param name="dtFrom">��ѯ����ʼ����</param>
		/// <param name="dtTo">��ѯ�Ľ�ֹ����</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		[Obsolete("����,ʹ��QueryBalancesByCardNO", true)]
		public int QueryInvoiceInformationByCardNo(string strCard, DateTime dtFrom, DateTime dtTo, ref System.Data.DataSet dsResult)
		{
	
			return 1;
		}

		/// <summary>
		/// ���ݻ���������ѯ��Ʊ������Ϣ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="strName">���뻼������</param>
		/// <param name="dtFrom">��ѯ����ʼ����</param>
		/// <param name="dtTo">��ѯ�Ľ�ֹ����</param>
		/// <param name="dsResult">���صĽ�����ݼ�</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		[Obsolete("����,ʹ��QueryBalancesByPatientName", true)]
		public int QueryInvoiceInformationByName(string strName, DateTime dtFrom, DateTime dtTo, ref System.Data.DataSet dsResult)
		{
			return 1;
		}


		/// <summary>
		/// ���ݷ�Ʊ�Ų�ѯ��Ʊ������Ϣ(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="strInvoiceNo">����ķ�Ʊ��</param>
		/// <param name="dsResult">���صĽ�����ݼ�</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		[Obsolete("����,ʹ��QueryBalancesByInvoiceNO", true)]
		public int QueryInvoiceInformationByInvoiceNo(string strInvoiceNo, ref System.Data.DataSet dsResult)
		{
			return 1;
		}
		
		/// <summary>
		/// ��Ʊע��
		/// </summary>
		/// <param name="oldInvoiceNo"></param>
		/// <param name="operDate"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��LogOutInvoice", true)]
		public int LonoutBill(string oldInvoiceNo, DateTime operDate)
		{
			return 0;
		}

		
		/// <summary>
		/// ���Ϸ�����Ϣ��
		/// </summary>
		/// <param name="type"></param>
		/// <param name="invoiceSeq"></param>
		/// <param name="cancelType"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateCancelTyeByInvoiceSequence", true)]
		public int UpdateOutItemsUsingSeqNo(string type, string invoiceSeq, CancelTypes cancelType)
		{
			string strSQL = null;
			string strIndex = null;
			switch(type)
			{
				case "1"://��Ʊ����
					strIndex = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.Invoice";
					break;
				case "2"://��Ʊ��ϸ��
					strIndex = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.InvoiceDetail";
					break;
				case "3"://������ϸ��
					strIndex = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.FeeDetail";
					break;
				case "4"://֧����ʽ
					strIndex = "Fee.OutPatient.UpdateOutItemsUsingSeqNo.PayMode";
					break;
			}
			if(this.Sql.GetCommonSql(strIndex, ref strSQL) == -1)
			{
				this.Err += "û���ҵ�����Ϊ:" + strIndex + "��sql���";
				return -1;
			}
			
			try
			{
				strSQL = string.Format(strSQL, invoiceSeq, ((int)cancelType).ToString());
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// �������������Ŀ�б�
		/// </summary>
		/// <param name="deptCode">�շ�Ա���ڿ���</param>
		/// <param name="ds">��Ŀ�б�</param>
		/// <returns> -1 ʧ�� > 0 �ɹ�</returns>
		[Obsolete("����,ʹ��QueryItemList", true)]
		public int GetItemList(string deptCode, ref DataSet ds)
		{
			return -1;
		}

		/// <summary>
		/// ���ݷ�Ʊ��������ϸ
		/// </summary>
		/// <param name="invoNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalanceCancelType", true)]
		public int UpdateInvoDetailCancelFlag(string invoNo)
		{
			string strSql = "";
			if(this.Sql.GetCommonSql("Fee.OutPatient.UpdateInvoDetailCancelFlagByInvo",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,invoNo);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ���ݷ�Ʊ�źͲ��������Ϸ�Ʊ
		/// </summary>
		/// <param name="invoNo"></param>
		/// <param name="cardNo"></param>
		/// <param name="Sysdate"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalanceCancelType", true)]
		public int UpdateInvoInfoCancelFlag(string invoNo,string cardNo,string Sysdate)
		{
			string strSql = "";
			if(this.Sql.GetCommonSql("Fee.OutPatient.UpdateInvoInfoCancelFlag",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,invoNo,cardNo,this.Operator.ID,Sysdate);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ���·�Ʊ��
		/// </summary>
		/// <param name="oldInvoiceNo"></param>
		/// <param name="invoiceSeq"></param>
		/// <param name="operDate"></param>
		/// <param name="cancelType"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalanceCancelType", true)]
		public int UpdateInvoCancelFlag(string oldInvoiceNo, string invoiceSeq, DateTime operDate,CancelTypes cancelType)
		{
			return -1;
		}
		/// <summary>
		/// ���·�Ʊ��ϸ
		/// </summary>
		/// <param name="oldInvoiceNo"></param>
		/// <param name="invoiceSeq"></param>
		/// <param name="operDate"></param>
		/// <param name="cancelType"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalanceListCancelType", true)]
		public int UpdateInvoDetailCancelFlag(string oldInvoiceNo, string invoiceSeq, DateTime operDate,CancelTypes cancelType)
		{
			return -1;
		}

		/// <summary>
		/// ������Ŀ��ˮ�ź�����ˮ��������Ŀ��¼
		/// </summary>
		/// <param name="recipe"></param>
		/// <param name="seq"></param>
		/// <param name="cancelType"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateFeeItemListCancelType", true)]
		public int UpdateFeeDetailCancelFlag(string recipe, int seq,CancelTypes cancelType)
		{
			return -1;
		}

		/// <summary>
		/// ������Ŀ��ˮ�ź�����ˮ��������Ŀ��¼
		/// </summary>
		/// <param name="oldInvoiceNo"></param>
		/// <param name="operDate"></param>
		/// <param name="cancelType"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateFeeItemListCancelType", true)]
		public int UpdateFeeDetailCancelFlag(string oldInvoiceNo, DateTime operDate,FS.HISFC.Models.Base.CancelTypes cancelType)
		{
			string strSql = "",CancelType = "0";
			if(this.Sql.GetCommonSql("Fee.Outpatient.UpdateFeeDetailCancelFlag.1",ref strSql)==-1)return -1;
			
			try
			{
				switch(cancelType)
				{
					case FS.HISFC.Models.Base.CancelTypes.Canceled:
						CancelType = "1";
						break;
					case FS.HISFC.Models.Base.CancelTypes.LogOut:
						CancelType = "3";
						break;
					case FS.HISFC.Models.Base.CancelTypes.Valid:
						CancelType = "0";
						break;
					case FS.HISFC.Models.Base.CancelTypes.Reprint:
						CancelType = "2";
						break;
					default:
						CancelType = "0";
						break;

				}
				strSql = string.Format(strSql,oldInvoiceNo,operDate,CancelType);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ����һ����Ʊ�ţ���ȡȫ���ֵܷ�Ʊ�ŵķ�����ϸ
		/// </summary>
		/// <param name="strInvo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemListsSameInvoiceCombNOByInvoiceNO", true)]
		public ArrayList GetBrotherFeeDetail(string strInvo)
		{
			return null;
		}

		/// <summary>
		/// ���ݷ�Ʊ��ˮ�ţ���ȡȫ���ֵܷ�Ʊ��   
		/// </summary>
		/// <param name="strSeq">����Ʊ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancesByInvoiceSequence", true)]
		public ArrayList GetBrotherInvoBySeq(string strSeq)
		{
			return null;
		}

		/// <summary>
		/// ��������Ʊ�ţ���ȡȫ���ֵܷ�Ʊ��   
		/// </summary>
		/// <param name="strInvo">����Ʊ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancesSameInvoiceCombNOByInvoiceNO", true)]
		public ArrayList GetBrotherInvo(string strInvo)
		{
			return null;
		}
		
		/// <summary>
		/// ���ݷ�Ʊ���л��֧����ʽ
		/// </summary>
		/// <param name="seq"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancePaysByInvoiceSequence", true)]
		public ArrayList GetPayModeBySeq(string seq)
		{
			return null;
		}


		/// <summary>
		/// ���ݷ�Ʊ���л�÷�����ϸ
		/// </summary>
		/// <param name="seq"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemListsByInvoiceSequence", true)]
		public ArrayList GetBrotherFeeDetailBySeq(string seq)
		{
			return null;
		}
		/// <summary>
		/// ��ʱ��Ʊ�ķ�Ʊ��ϸ
		/// </summary>
		/// <param name="invoiceNo"></param>
		/// <param name="seq"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalanceListsByInvoiceNOAndInvoiceSequence", true)]
		public ArrayList GetBalanceBrotherInvoDetailBySeq(string invoiceNo, string seq)
		{
			return null;
		}
		/// <summary>
		/// ���ݷ�Ʊ���л�÷�Ʊ��ϸ
		/// </summary>
		/// <param name="seq"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalanceListsByInvoiceSequence", true)]
		public ArrayList GetBalanceBrotherInvoDetailBySeq(string seq)
		{
			return null;
		}

		/// <summary>
		/// ����һ����Ʊ�� ��ȡ���е��ֵܷ�Ʊ����ϸ
		/// </summary>
		/// <param name="strInvo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalanceListsSameInvoiceCombNOByInvoiceNO", true)]
		public ArrayList GetBalanceBrotherInvoDetail(string strInvo)
		{
			return null;
		}
		
		/// <summary>
		/// ��û��ߵ������׷�Ʊ��Ϣ����Ʊ�ش���
		/// </summary>
		/// <param name="invoNo">��Ʊ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancesValidByInvoiceNO", true)]
		public ArrayList GetValidInvoiceInfo(string invoNo)
		{
			string strMain = "";
			string strWhere = "";

			strMain = this.GetBalanceSelectSql();
			
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetValidInvoiceInfo.Where.1", ref strWhere) == -1)
			{
				this.Err += "������� Fee.Outpatient.GetValidInvoiceInfo.Where.1 ����";
				return null;
			}
			try
			{
				strWhere = string.Format(strWhere, invoNo);
			}
			catch(Exception ex)
			{
				this.Err += ex.Message;
				return null;
			}

			return this.QueryBalancesBySql(strMain + strWhere);
		}
		/// <summary>
		/// �õ���ǰ����Ա�ӵ�ǰ��ʼ����ǰN�ŷ�Ʊ����Ϣ
		/// </summary>
		/// <param name="count">����</param>
		/// <returns>������Ϣ�ķ�Ʊʵ����Ϣ null ����</returns>
		[Obsolete("����,ʹ��QueryBalancesByCount", true)]
		public ArrayList GetSpecifyCountsInfosSinceNow(int count)
		{
			string sql = null;
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetSpecifyCountsInfosSinceNow.Select.1", ref sql) == -1)
			{
				this.Err += "������� Fee.Outpatient.GetSpecifyCountsInfosSinceNow.Select.1 ����";
				return null;
			}
			try
			{
				sql = string.Format(sql, this.Operator.ID, count + 1);	
			}
			catch(Exception ex)
			{
				this.Err += ex.Message;
				return null;
			}
			return this.QueryBalancesBySql(sql);
		}
		/// <summary>
		/// ���·�Ʊ��ϸ
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalanceList", true)]
		public int UpdateInvoDetail(FS.HISFC.Models.Fee.Outpatient.BalanceList obj)
		{
			string strSql="";			
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.InvoDetail.Update",ref strSql)==-1) return -1;
			try
			{
				//��ȡ�����б�
				strParam = GetBalanceListParams(obj);
				strSql = string.Format(strSql,strParam);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
            
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		/// ���뷢Ʊ��ϸ
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��InsertBalanceList", true)]
		public int InsertInvoDetail(FS.HISFC.Models.Fee.Outpatient.BalanceList objInvoDetail)
		{
			string sql = string.Empty;
			//ȡ���������SQL���
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.InvoDetail.Insert",ref sql) == -1) 
			{
				this.Err = "û���ҵ��ֶ�!";
				return -1;
			}
			try
			{

				if (objInvoDetail.ID == null) return -1;
				strParam = GetBalanceListParams(objInvoDetail);  
				
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql,strParam);
		}
		
		
		/// <summary>
		/// ���·�Ʊ��Ϣ
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalance", true)]
		public int UpdateInvoInfo(FS.HISFC.Models.Fee.Outpatient.Balance obj)
		{
			string strSql="";			
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.InvoInfo.Update",ref strSql)==-1) return -1;
			try
			{
				//��ȡ�����б�
				strParam = this.GetBalanceParams(obj);
				strSql = string.Format(strSql,strParam);

			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
            
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ���뷢Ʊ��Ϣ
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��InsertBalance", true)]
		public int InsertInvoInfo(Balance objInvoInfo)
		{
			string strSQL = "";
			//ȡ���������SQL���
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.OutPatient.InvoInfo.Insert",ref strSQL) == -1) 
			{
				this.Err = "û���ҵ��ֶ�!";
				return -1;
			}
			try
			{
				if (objInvoInfo.ID == null) return -1;
				strParam = this.GetBalanceParams(objInvoInfo);  
				
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL,strParam);
		}

		/// <summary>
		/// ����շ����к�
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetRecipeSequence", true)]
		public string GetRecipeSeq()
		{
			return this.GetSequence("Fee.OutPatient.GetRecipeSeq.Select");
		}
		/// <summary>
		/// ��÷�Ʊ��ˮ��
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetInvoiceCombNO", true)]
		public string GetInvoiceSeq()
		{
			return this.GetSequence("Fee.OutPatient.GetInvoiceSeq.Select");
		}
		/// <summary>
		/// ����Զ����ɵĿ��ţ� ��ҪΪ�շ�ֱ�����뻼����Ϣʱ���ɡ�
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetAutoCardNO", true)]
		public string GetAutoCardNo()
		{
			string tempCardNo = this.GetSequence("Fee.OutPatient.GetAutoCardNo.Select");
			
			return tempCardNo.PadLeft(9, '0');
		}

		/// <summary>
		/// ��ô�����
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetRecipeNO", true)]
		public string GetRecipeNo()
		{
			return this.GetSequence("Fee.OutPatient.GetRecipeNo.Select");
		}
		/// <summary>
		/// �޸Ĵ�����ϸ
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateFeeItemList", true)]
		public int UpdateFeeDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList f) 
		{
			string strSql="";			
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.ItemDetail.Update",ref strSql)==-1) return -1;
			try
			{
				//��ȡ�����б�
				strParam = this.GetFeeItemListParams(f);
				strSql = string.Format(strSql,strParam);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
            
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		///  ɾ�������ϸ�����Ŷ�Ӧ��δ�շѵĴ�����ϸ
		/// </summary>
		/// <param name="clinicCode">����</param>
		/// <returns>1���ɹ�</returns>
		[Obsolete("����,ʹ��DeleteFeeItemListByClinicNO", true)]
		public int DeleteFeeDetail(string clinicCode)
		{
			string strSQL = "";
			if (this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.CheckUp.DeleteFeeList",ref strSQL)==-1)
			{
				this.Err = "û��ɾ���õ�SQL���";
				return -1;
			}
			strSQL = string.Format(strSQL,clinicCode);
			try
			{
				if(this.ExecNoQuery(strSQL)==-1)
				{
					this.Err = "ִ��ɾ��ʧ��";
					return -2;
				}
			}
			catch(Exception ex)
			{
				this.Err = "ִ��ɾ��ʧ��" + ex.Message;
				return -2;
			}
			return 1;
		}
		/// <summary>
		/// ɾ������������������Ϣ
		/// </summary>
		/// <param name="seqNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��DeletePackageByMoOrder", true)]
		public int DeleteGroup(string seqNo)
		{
			string strSQL = "";
			if (this.Sql.GetCommonSql("Fee.OutPatient.DeleteGroup",ref strSQL)==-1)
			{
				this.Err = "û��ɾ���õ�SQL���";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, seqNo);
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				return -1;
			}
			
			return this.ExecNoQuery(strSQL);
		}
		/// <summary>
		/// ����ҽ�����������Ŀ��ˮ��ɾ����ϸ
		/// </summary>
		/// <param name="seqNo">ҽ�����������Ŀ��ˮ��</param>
		/// <returns>-1ʧ�� 0 û��ɾ����¼ >=1ɾ���ɹ�</returns>
		[Obsolete("����,ʹ��DeleteFeeItemListByMoOrder", true)]
		public int DeleteFeeDetailBySeqNo(string seqNo)
		{
			string strSQL = "";
			if (this.Sql.GetCommonSql("Fee.OutPatient.DeleteFeeDetailbySeqNo",ref strSQL)==-1)
			{
				this.Err = "û��ɾ���õ�SQL���";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, seqNo);
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				return -1;
			}
			
			return this.ExecNoQuery(strSQL);
		}
		/// <summary>
		/// ���ݴ����źʹ�������ˮ��ɾ��������ϸ.
		/// </summary>
		/// <param name="recipeNo">������</param>
		/// <param name="seqNo">��������ˮ��</param>
		/// <returns>-1ʧ�� 0 û��ɾ����¼ >=1ɾ���ɹ�</returns>
		[Obsolete("����,ʹ��DeleteFeeItemListByRecipeNO", true)]
		public int DeleteFeeDetail(string recipeNo, string seqNo)
		{
			string strSQL = "";
			if (this.Sql.GetCommonSql("Fee.OutPatient.DeleteFeeDetailByRecipeNo",ref strSQL)==-1)
			{
				this.Err = "û��ɾ���õ�SQL���";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, recipeNo, seqNo);
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				return -1;
			}
			
			return this.ExecNoQuery(strSQL);
		}
		/// <summary>
		/// ���봦����ϸ
		/// </summary>
		/// <returns></returns>
		[Obsolete("����,ʹ��InsertFeeItemList", true)]
		public int InsertFeeDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList objFeeItemList) 
		{
			string sql = string.Empty;
			//ȡ���������SQL���
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Item.Undrug.GetFeeItemDetail.Insert",ref sql) == -1) 
			{
				this.Err = "û���ҵ��ֶ�!";
				return -1;
			}
			try
			{
				//ȡ������
				//				objFeeItemList.ID = this.GetSequence("Manager.%CLASSName%.GetConstantID");
				//				if (objFeeItemList.ID == null) return -1;
				strParam = this.GetFeeItemListParams(objFeeItemList);  
				
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql,strParam);
		}
		/// <summary>
		/// ɾ��������ϸ������Ϻ�
		/// </summary>
		/// <param name="combNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��DeleteFeeItemListByCombNO", true)]
		public int DelFeeDetail(string combNo)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.DelFeeDetail.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,combNo);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}			
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ������ҩƷ��ϸ
		/// </summary>
		/// <param name="invoNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryUndrugFeeItemListByInvoiceSequence", true)]
		public ArrayList GetUnDrugItemList(string invoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlFeeDetail();
			if(this.Sql.GetCommonSql("Fee.Item.GetUndrugItemList.Where",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,invoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryFeeDetailBySql(strSql1);
		}
		
		/// <summary>
		/// ����ҩƷ��ϸ
		/// </summary>
		/// <param name="invoNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryDrugFeeItemListByInvoiceSequence", true)]
		public ArrayList GetDrugItemList(string invoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlFeeDetail();
			if(this.Sql.GetCommonSql("Fee.Item.GetDrugItemList.Where",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,invoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryFeeDetailBySql(strSql1);
		}
		/// <summary>
		/// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��
		/// </summary>
		/// <param name="noteNo"></param>
		/// <param name="seqNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetFeeItemList", true)]
		public FS.HISFC.Models.Fee.Outpatient.FeeItemList GetItemObj(string noteNo,int seqNo)
		{
			string sql="",where="";
			sql = this.GetSqlFeeDetail();
			if(sql == "")return null;			
			if(this.Sql.GetCommonSql("Fee.Item.GetDrugItemList.Where2",ref where)==-1)return null;

			try
			{
				where=string.Format(where,noteNo,seqNo.ToString());
			}
			catch(Exception e)
			{
				this.Err="[Registration.Register.Query.6]"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}

			sql=sql +" "+where;
			al = this.QueryFeeDetailBySql(sql);
			if(this.al.Count>0)
				return (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.al[0];
			else
				return null;
		}
		/// <summary>
		/// ��û��ߵ� �Ѿ��շѣ� δȷ�ϵ�ָ�� ��ҪԺע����Ŀ��Ϣ
		/// </summary>
		/// <param name="cardNo"></param>
		/// <param name="isInject">t��Ҫ��Ժע����Ŀ false ��ѯ����������Ŀ</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemLists", true)]
		public ArrayList GetChargeDetail(string cardNo, bool isInject)
		{
			string strSqlWhere = "";
			string strSqlOrg = "";
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetChargeDetail.Select.3", ref strSqlWhere) == -1)
			{
				this.Err += "���SQL������" + "����: Fee.Outpatient.GetChargeDetail.Select.1";
				return null;
			}
			if(!isInject)
			{
				//return this.GetChargeDetail(cardNo);
			}
			try
			{
				strSqlWhere = string.Format(strSqlWhere, cardNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}
			strSqlOrg = GetSqlFeeDetail();
			strSqlOrg = strSqlOrg + strSqlWhere;
			return QueryFeeDetailBySql(strSqlOrg);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemListsByCardNO()", true)]
		public ArrayList GetFeeDetailByCardNo(string cardNo)
		{
			string strSql = "",strWhere = "";
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetFeeDetail.Where.1", ref strWhere) == - 1)
			{
				this.Err += "���SQL������" + "����: Fee.Outpatient.GetFeeDetail.Where.1";
				return null;
			}
			try
			{
				strWhere = string.Format(strWhere, cardNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}

			strSql = this.GetSqlFeeDetail();
			strSql = strSql +" "+strWhere;
			return QueryFeeDetailBySql(strSql);
		}

		/// <summary>
		/// ͨ�����߿��ţ��õ�������ϸ
		/// </summary>
		/// <param name="cardNo">���߲�����</param>
		/// <returns>null ���� ArrayList Fee.Outpatient.FeeItemListʵ�弯��</returns>
		[Obsolete("����,ʹ��QueryFeeItemListsByCardNO()", true)]
		public ArrayList GetFeeDetailFromCardNo(string cardNo)
		{
			string strSql = "";
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetFeeDetail.Select.1", ref strSql) == - 1)
			{
				this.Err += "���SQL������" + "����: Fee.Outpatient.GetFeeDetail.Select.1";
				return null;
			}
			try
			{
				strSql = string.Format(strSql, cardNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}

			return QueryFeeDetailBySql(strSql);
		}
		
		/// <summary>
		/// ����֧�������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��InsertBalancePay()", true)]
		public int InsertPayMode(FS.HISFC.Models.Fee.Outpatient.BalancePay obj)
		{
			string sql = string.Empty;
			//ȡ���������SQL���
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.PayMode.Insert",ref sql) == -1) 
			{
				this.Err = "û���ҵ��ֶ�!";
				return -1;
			}
			try
			{
				if (obj.Invoice.ID == null) return -1;
				strParam = this.GetBalancePayParams(obj);  				
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql,strParam);
		}
		/// <summary>
		/// �޸��ս���Ϣ
		/// </summary>
		/// <param name="dayBalance"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalancePay()", true)]
		public int UpdateDayBalance(FS.HISFC.Models.Fee.Outpatient.BalancePay dayBalance)
		{
			string strSql="";			
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.OutPatient.PayMode.Update",ref strSql)==-1) return -1;
			try
			{
				//��ȡ�����б�
				strParam = this.GetBalancePayParams(dayBalance);
				strSql = string.Format(strSql,strParam);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
            
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="invoNo">��Ʊ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancePayByInvoiceNO()", true)]
		public ArrayList GetPayModeByInvo(string invoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetBalancePaySelectSql();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetSqlPayMode.Where.1",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,invoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryBalancePaysBySql(strSql1);
		}

		/// <summary>
		/// �޸�֧�������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��UpdateBalancePay()", true)]
		public int UpdatePayMode(FS.HISFC.Models.Fee.Outpatient.BalancePay obj)
		{
			string strSql="";			
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.Outpatient.PayMode.Update",ref strSql)==-1) return -1;
			try
			{
				//��ȡ�����б�
				strParam = this.GetBalancePayParams(obj);
				strSql = string.Format(strSql,strParam);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
            
			return this.ExecNoQuery(strSql);
		}
		
		/// <summary>
		/// ͨ����Ʊ�Ż�û�û��߷�����ϸ��Ϣ
		/// </summary>
		/// <param name="invoNo">��Ʊ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemListsByInvoiceNO()", true)]
		public ArrayList GetChargeDetailFromInvoiceNo(string invoNo)
		{
			string strSqlWhere = "";
			string strSqlOrg = "";
			if(this.Sql.GetCommonSql("Fee.OutPatient.GetChargeDetailFromInvoiceNo.Where.1", ref strSqlWhere) == -1)
			{
				this.Err += "���SQL������" + "����: Fee.OutPatient.GetChargeDetailFromInvoiceNo.Where.1";
				return null;
			}
			try
			{
				strSqlWhere = string.Format(strSqlWhere, invoNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}
			strSqlOrg = GetSqlFeeDetail();
			strSqlOrg = strSqlOrg + strSqlWhere;
			return QueryFeeDetailBySql(strSqlOrg);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="invoNo"></param>
		/// <returns></returns>
		[Obsolete("����,��ʹ��()", true)]
		public ArrayList QueryFeeDetailByInvoiceNo(string invoNo)
		{
			string strSqlWhere = "";
			string strSqlOrg = "";
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetChargeDetailFromInvoiceNo.Where.5", ref strSqlWhere) == -1)
			{
				this.Err += "���SQL������" + "����: Fee.Outpatient.GetChargeDetailFromInvoiceNo.Where.1";
				return null;
			}
			try
			{
				strSqlWhere = string.Format(strSqlWhere, invoNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}
			strSqlOrg = GetSqlFeeDetail();
			strSqlOrg = strSqlOrg + strSqlWhere;
			return QueryFeeDetailBySql(strSqlOrg);
		}
		/// <summary>
		/// ��û��ߵ�δ�շ���Ŀ��Ϣ
		/// </summary>
		/// <param name="clinicNo"></param>
		/// <returns></returns>
		[Obsolete("����,QueryFeeItemListsByClinicNO()", true)]
		public ArrayList GetChargeDetail(string clinicNo)
		{
			string strSqlWhere = "";
			string strSqlOrg = "";
			if(this.Sql.GetCommonSql("Fee.OutPatient.GetChargeDetail.Select.1", ref strSqlWhere) == -1)
			{
				this.Err += "���SQL������" + "����: Fee.OutPatient.GetChargeDetail.Select.1";
				return null;
			}
			try
			{
				strSqlWhere = string.Format(strSqlWhere, clinicNo);
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}
			strSqlOrg = GetSqlFeeDetail();
			strSqlOrg = strSqlOrg + strSqlWhere;
			return QueryFeeDetailBySql(strSqlOrg);
		}

		/// <summary>
		/// ��û��ߵ� �Ѿ��շѣ� δȷ�ϵ�ָ��SysClass����Ŀ��Ϣ
		/// </summary>
		/// <param name="cardNo">���߿���</param>
		/// <param name="sysClass">��Ŀϵͳ���</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryFeeItemList()", true)]
		public ArrayList GetChargeDetail(string cardNo, FS.HISFC.Models.Base.EnumSysClass sysClass)
		{
			string strSqlWhere = "";
			string strSqlOrg = "";
			if(this.Sql.GetCommonSql("Fee.OutPatient.GetChargeDetail.Select.2", ref strSqlWhere) == -1)
			{
				this.Err += "���SQL������" + "����: Fee.Outpatient.GetChargeDetail.Select.1";
				return null;
			}
			try
			{
				strSqlWhere = string.Format(strSqlWhere, cardNo, sysClass.ToString());
			}
			catch(Exception ex)
			{
				this.Err += "������ֵ����!" + ex.Message;
				return null;
			}
			strSqlOrg = GetSqlFeeDetail();
			strSqlOrg = strSqlOrg + strSqlWhere;
			return QueryFeeDetailBySql(strSqlOrg);
		}
		/// <summary>
		/// ���ݻ��߿��ź�ʱ��β��ҷ��������ķ�Ʊʵ�弯��
		/// </summary>
		/// <param name="cardNo">���߿���</param>
		/// <param name="dtBegin">��ʼʱ��</param>
		/// <param name="dtEnd">����ʱ��</param>
		/// <returns>nullʧ�� count = 0 û�н�� ��0 ��ȷ</returns>
		[Obsolete("����,ʹ��QueryBalancesByCardNO()", true)]
		public ArrayList GetInvoiceInfoByPatientCardNo(string cardNo, DateTime dtBegin, DateTime dtEnd)
		{
			string strMain = "";
			string strWhere = "";

			strMain = this.GetBalanceSelectSql();
			
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoiceInfoByPatientCardNo.Where.1", ref strWhere) == -1)
			{
				this.Err += "������� Fee.Outpatient.GetInvoiceInfoByPatientCardNo.Where.1 ����";
				return null;
			}
			try
			{
				strWhere = string.Format(strWhere, cardNo, dtBegin.ToString(), dtEnd.ToString());
			}
			catch(Exception ex)
			{
				this.Err += ex.Message;
				return null;
			}

			return this.QueryBalancesBySql(strMain + strWhere);
		}
		/// <summary>
		/// ���ݻ���������ʱ��β��ҷ��������ķ�Ʊʵ�弯��
		/// </summary>
		/// <param name="name">���߿���</param>
		/// <param name="dtBegin">��ʼʱ��</param>
		/// <param name="dtEnd">����ʱ��</param>
		/// <returns>nullʧ�� count = 0 û�н�� ��0 ��ȷ</returns>
		[Obsolete("����,ʹ��QueryBalancesByName()", true)]
		public ArrayList GetInvoiceInfoByPatientName(string name, DateTime dtBegin, DateTime dtEnd)
		{
			string strMain = "";
			string strWhere = "";

			strMain = this.GetBalanceSelectSql();
			
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoiceInfoByPatientName.Where.1", ref strWhere) == -1)
			{
				this.Err += "������� Fee.Outpatient.GetInvoiceInfoByPatientName.Where.1 ����";
				return null;
			}
			try
			{
				strWhere = string.Format(strWhere, name, dtBegin.ToString(), dtEnd.ToString());
			}
			catch(Exception ex)
			{
				this.Err += ex.Message;
				return null;
			}

			return this.QueryBalancesBySql(strMain + strWhere);
		}
		/// <summary>
		/// ��÷�Ʊ��Ϣ
		/// </summary>
		/// <param name="invoNo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalancesByInvoiceNO()", true)]
		public ArrayList GetBalanceInfoByInvoNo(string invoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetBalanceSelectSql();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,invoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryBalancesBySql(strSql1);
		}
		
		/// <summary>
		/// ��÷�Ʊ��ϸ
		/// </summary>
		/// <param name="strInvo"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBalanceListsByInvoiceNO()", true)]
		public ArrayList GetBalanceInvoDetail(string strInvo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetBalanceListsSql();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoDetail.Where",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,strInvo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryBalanceListsBySql(strSql1);
		}

		/// <summary>
		/// ��������շ���Ŀ�б�
		/// </summary>
		/// <param name="itemType">��ʾ����Ŀ���</param>
		/// <param name="inputType">��ѯ��ʽ</param>
		/// <param name="queryCode">��ѯ��</param>
		/// <param name="beginRows">��ʼ��</param>
		/// <param name="endRows">������</param>
		/// <returns></returns>
		[Obsolete("��������", true)]
		public ArrayList GetItemList(ItemTypes itemType, InputTypes inputType, string queryCode, int beginRows, int endRows)
		{
			string sysClass = "";//ϵͳ���;
			string drugFlag = "";//�Ƿ�ҩƷ 1�� 0 ����;
			string sql = string.Empty;
			ArrayList al = new ArrayList();//�����Ŀ�б���Ϣ;

			Spell inputInfo = new Spell();

			switch(itemType)
			{
				case ItemTypes.All: //������Ŀ
					sysClass = "%";
					drugFlag = "%";
					break;
				case ItemTypes.AllMedicine: //����ҩƷ��Ŀ
					sysClass = "P%";
					drugFlag = "1";
					break;
				case ItemTypes.WesternMedicine: //��ҩ
					sysClass = "P";
					drugFlag = "1";
					break;
				case ItemTypes.ChineseMedicine: //�г�ҩ
					sysClass = "PCZ";
					drugFlag = "1";
					break;
				case ItemTypes.HerbalMedicine: //�в�ҩ
					sysClass = "PCC";
					drugFlag = "1";
					break;
				case ItemTypes.Undrug: //��ҩƷ
					sysClass = "%";
					drugFlag = "0";
					break;
				default: //Ĭ��ѡ��������Ŀ
					sysClass = "%";
					drugFlag = "%";
					break;
			}

			switch(inputType)
			{
				case InputTypes.Spell: //�������ƴ��
					inputInfo.SpellCode = "%" + queryCode + "%";
					inputInfo.WBCode = "%";
					inputInfo.UserCode = "%";
					inputInfo.Name = "%";
					break;
				case InputTypes.WB: //����������
					inputInfo.SpellCode = "%";
					inputInfo.WBCode = "%" + queryCode + "%";
					inputInfo.UserCode = "%";
					inputInfo.Name = "%";
					break;
				case InputTypes.UserCode: //��������Զ���
					inputInfo.SpellCode = "%";
					inputInfo.WBCode = "%";
					inputInfo.UserCode = "%" + queryCode + "%";
					inputInfo.Name = "%";
					break;
				case InputTypes.Name: //�����������
					inputInfo.SpellCode = "%";
					inputInfo.WBCode = "%";
					inputInfo.UserCode = "%" + queryCode + "%";
					inputInfo.Name = "%";
					break;
				default: //Ĭ��Ϊƴ��
					inputInfo.SpellCode = "%" + queryCode + "%";
					inputInfo.WBCode = "%";
					inputInfo.UserCode = "%";
					inputInfo.Name = "%";
					break;
			}

			if(this.Sql.GetCommonSql("Fee.Item.Undrug.GetOutPatientItemList.Select", ref sql) == -1)
			{
				this.Err = "���SQL����";
				return null;
			}

			try
			{
				sql = string.Format(sql, sysClass, drugFlag, inputInfo.SpellCode, inputInfo.WBCode,
					inputInfo.UserCode, inputInfo.Name, beginRows, endRows);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}

			this.ExecQuery(sql);

			FS.HISFC.Models.Fee.Item.Undrug feeItem = null;
			FS.HISFC.Models.Pharmacy.Item drugItem = null;

			try
			{	
				while(Reader.Read())
				{
					if(Reader[0].ToString() == "1")//ҩƷ
					{
						drugItem = new FS.HISFC.Models.Pharmacy.Item();
						drugItem.IsPharmacy = true;
						drugItem.SysClass.ID = Reader[1].ToString();
						drugItem.MinFee.ID = Reader[2].ToString();
						drugItem.ID = Reader[3].ToString();
						drugItem.Name = Reader[4].ToString();
						drugItem.NameCollection.EnglishName = Reader[5].ToString();
						drugItem.Specs = Reader[6].ToString();
						drugItem.DosageForm.ID = Reader[7].ToString();
						drugItem.Price = NConvert.ToDecimal(Reader[8].ToString());
						drugItem.ChildPrice = NConvert.ToDecimal(Reader[9].ToString());
						drugItem.SpecialPrice = NConvert.ToDecimal(Reader[10].ToString());
						drugItem.PriceUnit = Reader[11].ToString();
						al.Add(drugItem);
						drugItem = null;
					}
					else //��ҩƷ
					{
						feeItem = new FS.HISFC.Models.Fee.Item.Undrug();
						feeItem.IsPharmacy = false;
						feeItem.SysClass.ID = Reader[1].ToString();
						feeItem.MinFee.ID = Reader[2].ToString();
						feeItem.ID = Reader[3].ToString();
						feeItem.Name = Reader[4].ToString();
						feeItem.Specs = Reader[6].ToString();
						feeItem.Price = NConvert.ToDecimal(Reader[8].ToString());
						feeItem.ChildPrice = NConvert.ToDecimal(Reader[9].ToString());
						feeItem.SpecialPrice = NConvert.ToDecimal(Reader[10].ToString());
						feeItem.PriceUnit = Reader[11].ToString();
						feeItem.ExecDept = Reader[12].ToString();
						al.Add(feeItem);
						feeItem = null;
					}
				}

				Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				if(Reader != null || !Reader.IsClosed)
				{
					Reader.Close();
				}
				feeItem = null;
				drugItem = null;
				al = null;
				this.Err += ex.Message;
				return null;
			}
			finally
			{
				feeItem = null;
				drugItem = null;
				al = null;
			}
		}

		ArrayList al = new ArrayList();

		
		
		
		#endregion

		#region Ժעά��

		/// <summary>
		/// ��ö������
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected string [] myGetParmInjectInfo(FS.HISFC.Models.Order.OrderSubtbl obj)
		{
			string[] strParm={	
								 obj.Item.ID,
								 obj.Item.Name,
								 obj.Usage.ID,
								 obj.Usage.Name,
								 obj.Oper.ID,
								 obj.QtyRule.ToString()	
							 };

			return strParm;

		}
        
		/// <summary>
		/// ɾ���÷���Ŀ��Ϣ
		/// </summary>
		/// <param name="Usage"></param>
		/// <returns></returns>
		public int DelInjectInfo(string Usage)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.OutPatient.DelInjectInfo.Del",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,Usage);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// �����÷���Ŀ��Ϣ
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int InsertInjectInfo(FS.HISFC.Models.Order.OrderSubtbl obj)
		{
			string sql = string.Empty;
			//ȡ���������SQL���
			//			 obj.ID,
			//								 obj.Name,
			//								 obj.Memo,
			//								 obj.User01,
			//								 obj.User02		
			string[] strParam ;
			if(this.Sql.GetCommonSql("Fee.OutPatient.InsertInjectInfo.Insert",ref sql) == -1) 
			{
				this.Err = "û���ҵ��ֶ�!";
				return -1;
			}
			try
			{
				if (obj.ID == null) return -1;
				strParam = this.myGetParmInjectInfo(obj); 
				
			}
			catch(Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql,strParam);
			
		}

		/// <summary>
		/// ����÷���Ŀ��Ϣsql���
		/// </summary>
		/// <returns></returns>
		public string GetSqlInject() 
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.OutPatient.GetSqlInject.Select",ref strSql)==-1) return null;
			return strSql;
		}

		/// <summary>
		/// ���Ժע��Ϣ�����÷�
		/// </summary>
		/// <param name="usageCode"></param>
		/// <returns></returns>
		public ArrayList GetInjectInfoByUsage(string usageCode)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlInject();
			if(this.Sql.GetCommonSql("Fee.OutPatient.GetSqlInject.Where1",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,usageCode);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.GetInjectInfo(strSql1);
		}

		private ArrayList GetInjectInfo(string strSql)
		{
			ArrayList al = new ArrayList();
			FS.HISFC.Models.Order.OrderSubtbl obj;
			this.ExecQuery(strSql);
			while (this.Reader.Read()) 
			{
				#region
				//USAGE_CODE	VARCHAR2(4)	N			�÷�����
				//ITEM_CODE	VARCHAR2(12)	N			��Ŀ����
				//ITEM_NAME	VARCHAR2(100)	Y			��Ŀ����
				//OPER_CODE	VARCHAR2(6)	Y			����Ա
				//OPER_DATE	DATE	Y			����ʱ��
				//USAGE_NAME	VARCHAR2(50)	Y			
				#endregion
                obj = new FS.HISFC.Models.Order.OrderSubtbl();
				try 
				{
					obj.Item.ID = this.Reader[0].ToString();//��Ŀ����
	
					obj.Item.Name = this.Reader[1].ToString();//��Ŀ����

                    obj.Usage.ID = this.Reader[2].ToString();//�÷�

					obj.Usage.Name = this.Reader[3].ToString();//�÷�

					obj.Oper.ID = this.Reader[4].ToString();//����Ա
			
					obj.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());//����ʱ��		

                    obj.QtyRule = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());//�շѹ���

					
				}

				catch(Exception ex) 
				{
					this.Err= "��ѯ��ϸ��ֵ����"+ex.Message;
					this.ErrCode=ex.Message;
					this.WriteErr();
					return null;
				}
				
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}


		#endregion

		#region �����ѯ

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetSqlInvoInfoName() 
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Name",ref strSql)==-1) return null;
			return strSql;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		private ArrayList GetInvoName(string strSql)
		{
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj = null;
			this.ExecQuery(strSql);
			while (this.Reader.Read()) 
			{
				#region
				
				#endregion
				obj = new FS.FrameWork.Models.NeuObject();
				try 
				{
					obj.ID = this.Reader[0].ToString();//			0			����
					obj.Name = this.Reader[1].ToString();
					
				}

				catch(Exception ex) 
				{
					this.Err= "��ѯ������ϸ��ֵ����"+ex.Message;
					this.ErrCode=ex.Message;
					this.WriteErr();
					return null;
				}
				
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strName"></param>
		/// <returns></returns>
        [Obsolete("�Ҳ���SQL���", true)]
		public ArrayList QueryInvoInfoByName(string strName)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlInvoInfoName();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where3",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,strName);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.GetInvoName(strSql1);
		}
        
        #region 
    
        /// <summary>
        /// �����տ�Ա���Ż�ȡ�ϴ��ս�ʱ��(1���ɹ�/0��û�������ս�/-1��ʧ��)
        /// </summary>
        /// <param name="employee">����Ա</param>
        /// <param name="lastDate">�����ϴ��ս��ֹʱ��</param>
        /// <returns>1���ɹ�/0��û�������ս�/-1��ʧ��</returns>
        public int GetLastBalanceDate(FS.FrameWork.Models.NeuObject employee, ref string lastDate)
        {

            lastDate = this.ExecSqlReturnOne("Fee.Outpatient.GetLastBalanceDate.Select", employee.ID);          
            if (lastDate == ""||lastDate=="-1")
            {
                lastDate = System.DateTime.MinValue.ToString();                
            }
            return 1;
        }
        #endregion
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CardNO"></param>
		/// <returns></returns>
        [Obsolete("�Ҳ���SQL���", true)]
		public ArrayList QueryInvoInfoByCardNo(string CardNO)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlInvoInfoName();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where4",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,CardNO);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.GetInvoName(strSql1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="InvoNo"></param>
		/// <returns></returns>
        [Obsolete("�Ҳ���SQL���", true)]
		public ArrayList QueryInvoInfoByInvoNo(string InvoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetSqlInvoInfoName();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where5",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,InvoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.GetInvoName(strSql1);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="InvoNo"></param>
		/// <returns></returns>
        [Obsolete("�Ҳ���SQL���", true)]
		public ArrayList QueryInvoInfoLikeInvoNo(string InvoNo)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetBalanceSelectSql();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where5",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,InvoNo);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryBalancesBySql(strSql1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="CardNO"></param>
		/// <returns></returns>
        [Obsolete("�Ҳ���SQL���",true)]
		public ArrayList QueryBalanceInvoInfoByCardNo(string CardNO)
		{
			string strSql1="";
			string strSql2="";
			//�����Ŀ��ϸ��SQL���
			strSql1=this.GetBalanceSelectSql();
			if(this.Sql.GetCommonSql("Fee.Outpatient.GetInvoInfo.Where4",ref strSql2)==-1)return null;
			strSql1=strSql1+" "+strSql2;
			try
			{
				strSql1=string.Format(strSql1,CardNO);
			}
			catch(Exception ex)
			{
				this.ErrCode = ex.Message;
				this.Err = ex.Message;
				return null;
			}			
			return this.QueryBalancesBySql(strSql1);
		}

		#endregion

		#region ���Ѳ���
		/// <summary>
		/// ��ù��ѻ��ߵ�������ȡ��ҩƷ���ý��
		/// </summary>
		/// <param name="mCardNo">���߿���</param>
		/// <returns>���ѻ��ߵ�������ȡ��ҩƷ���ý�� - 1����</returns>
		public decimal GetDayDrugFee(string mCardNo,string name)
		{
			string strSql = null;
			decimal tmpDayFee = 0;
			if (this.Sql.GetCommonSql("Fee.Outpatient.GetDayDrugFee.Select", ref strSql) == -1) 
			{
				this.Err = Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql, mCardNo,name);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
			try
			{
				this.ExecQuery(strSql);
				while (this.Reader.Read()) 
				{
					tmpDayFee = NConvert.ToDecimal(Reader[0].ToString());
				}
				this.Reader.Close();

				return tmpDayFee;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return -1;
			}
			finally
			{
				if(!Reader.IsClosed)
				{
					this.Reader.Close();
					strSql = null;
				}
			}
		}

		#endregion

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �˻��շ�����

        /// <summary>
        /// ���·����շѱ��
        /// </summary>
        /// <param name="f">����ʵ��</param>
        /// <returns></returns>
        public int UpdateFeeDetailFeeFlag(FeeItemList f)
        {
            string[] parms = new string[] { f.RecipeNO,
                                            f.SequenceNO.ToString(),
                                            ((int)f.PayType).ToString(),
                                            f.FeeOper.ID,
                                            f.FeeOper.OperTime.ToString()};
            return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailFeeFlag", parms);
        }

        /// <summary>
        /// ���ݴ�����ִ�п��Ҳ�ѯҩƷ������ϸ
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList GetDurgFeeByRecipeAndDept(string recipeNO, string deptCode)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetDrugFeeByRecipeAndDept.Where", recipeNO, deptCode);
        }

        /// <summary>
        /// ���ݲ�����ʱ��λ�ȡ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="isDrug">�Ƿ�ҩƷ</param>
        /// <returns></returns>
        public ArrayList GetDrugFeeByCardNODate(string cardNO, DateTime beginDate, DateTime endDate, bool isDrug)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetFeeDetail.Where", cardNO, beginDate.ToString(), endDate.ToString(), NConvert.ToInt32(isDrug).ToString());
        }

        /// <summary>
        /// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��(�շ���Ϣ)
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">��������ˮ��</param>
        /// <returns>�ɹ�:������ϸʵ�� ʧ�ܻ���û������:null</returns>
        public FeeItemList GetFeeItemListForFee(string recipeNO, int sequenceNO)
        {
            ArrayList feeItemLists = this.QueryFeeItemLists("Fee.Item.GetDrugItemList.Where3", recipeNO, sequenceNO.ToString());
            if (feeItemLists == null)
            {
                return null;
            }

            if (feeItemLists.Count > 0)
            {
                return feeItemLists[0] as FeeItemList;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��(�շ���Ϣ)--�����˷�
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">��������ˮ��</param>
        /// <returns>�ɹ�:������ϸʵ�� ʧ�ܻ���û������:null</returns>
        public FeeItemList GetFeeItemListAndQuitForFee(string recipeNO, int sequenceNO)
        {
            ArrayList feeItemLists = this.QueryFeeItemLists("Fee.Item.GetDrugItemList.WhereFeed", recipeNO, sequenceNO.ToString());
            if (feeItemLists == null)
            {
                return null;
            }

            if (feeItemLists.Count > 0)
            {
                return feeItemLists[0] as FeeItemList;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// ���ݴ�����ִ�п��Ҳ���ҩƷ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="deptCode">ִ�п���</param>
        /// <returns></returns>
        public int GetDrugUnFeeCount(string recipeNO, string deptCode)
        {
            return Convert.ToInt32(this.ExecSqlReturnOne("Fee.OutPatient.GetFeeDrugCountByRecipe", recipeNO, deptCode));
        }

        /// <summary>
        /// ������ʱ�ķ�Ʊ���
        /// </summary>
        /// <returns></returns>
        public string GetTempInvoiceComboNO()
        {
            string resutValue = this.ExecSqlReturnOne("Fee.OutPatient.GetTempInvoiceSeq.Select");
            if (resutValue == "-1") return "-1";
            return "T" + resutValue;
        }

        /// <summary>
        /// ���ݲ����Ż��δ��ӡ��Ʊ���˻���Ŀ��ϸ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="payType">�շѻ��۱�ʶ</param>
        /// <param name="isAccount">�Ƿ��˻�����</param>
        /// <returns></returns>
        public ArrayList GetAccountNoPrintFeeItemList(string cardNO, PayTypes payType,bool isAccount)
        {
            return this.QueryFeeItemLists("Fee.Item.GetDrugItemList.Where4", cardNO, ((int)payType).ToString(),NConvert.ToInt32(isAccount).ToString());
        }

        /// <summary>
        /// ���·��õķ�Ʊ��Ϣ
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UpdateFeeItemListInvoiceInfo(FeeItemList f)
        {
            string[] args = new string[] { f.RecipeNO, f.SequenceNO.ToString(), f.Invoice.ID, f.InvoiceCombNO };
            return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailInvoiceInfo", args);
        }

        /// <summary>
        /// ���ݲ����Ų�ѯ�˻�����δ�շѵķ�����Ϣ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <returns></returns>
        public ArrayList GetAccountNoFeeFeeItemList(string cardNO)
        {
            return this.QueryFeeItemLists("Fee.Item.GetDrugItemList.Where5", cardNO);
        }
        #endregion


        #region Ԥ����Ʊ���� {2AC3219B-972D-4541-A90C-18D371B0C638}
        /// <summary>
        /// Ԥ����Ʊ����
        /// {2AC3219B-972D-4541-A90C-18D371B0C638}
        /// 
        /// ��Ԥ���������У�һ�ο����ο۷Ѳ�������ʱ��Ʊ��Ϣ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="employee">����Ա��</param>
        /// <param name="lstInvoice">��Ʊ��Ϣ�б�</param>
        /// <param name="invoiceNo">�·�Ʊ��</param>
        /// <param name="realInvoiceNo">�´�ӡ��Ʊ��</param>
        /// <param name="invoiceSeqNegative">����Ʊ��ˮ��</param>
        /// <param name="invoiceSeqPositive">����Ʊ��ˮ��</param>
        /// <returns></returns>
        public int SummaryAccountInvoice(FS.HISFC.Models.Registration.Register regInfo, Employee employee, List<Balance> lstInvoice, string invoiceNo, string realInvoiceNo, string invoiceSeqNegative, string invoiceSeqPositive)
        {
            if (employee == null || lstInvoice == null || lstInvoice.Count <= 0)
            {
                this.Err = "����Ϊ�գ�";
                return -1;
            }

            string invoiceSeqWhere = "";
            foreach (Balance obj in lstInvoice)
            {
                invoiceSeqWhere += " '" + obj.CombNO + "',";
            }
            if (string.IsNullOrEmpty(invoiceSeqWhere))
            {
                this.Err = "��Ʊ��ϢΪ�գ�";
                return -1;
            }
            else
            {
                invoiceSeqWhere = invoiceSeqWhere.Trim(new char[] { ' ', ',', '\'' });
            }

            try
            {
                string strSql = null;
                int iRes = 0;
                Balance invoice = lstInvoice[0];

                #region ��Ʊ��

                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.Invoice1", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }

                strSql = string.Format(strSql, invoiceNo, regInfo.PID.CardNO, regInfo.DoctorInfo.SeeDate.ToString(), regInfo.Name, regInfo.Pact.PayKind.ID, regInfo.Pact.ID,
                    regInfo.Pact.Name, regInfo.SSN, "", employee.ID, regInfo.ChkKind, invoiceSeqPositive, regInfo.ID, realInvoiceNo, invoiceSeqWhere);

                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err +=  "  ���ܷ�Ʊ��Ϣʧ�ܣ�";
                    return iRes;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.Invoice2", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, employee.ID, invoiceNo, invoiceSeqNegative, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ���ܷ�Ʊ��Ϣ,���Ϸ�Ʊ��Ϣʧ�ܣ�";
                    return -1;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.Invoice3", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceNo, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ���ܷ�Ʊ��Ϣ,����Ʊ��Ϣʧ�ܣ�";
                    return -1;
                }
                #endregion

                #region ��Ʊ��ϸ��
                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvioce.Summary.InvoiceDetial1", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceNo, "", "", employee.ID, invoiceSeqPositive, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ���ܷ�Ʊ��ϸʧ�ܣ�";
                    return -1;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvioce.Summary.InvoiceDetial2", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceSeqNegative, invoiceSeqWhere, this.Operator.ID);//{96A6C0EA-2F0F-4a19-8567-3178C505E3FE}����sql����bug����˲�����
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ���ܷ�Ʊ��ϸ,������ϸ��¼ʧ�ܣ�";
                    return -1;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvioce.Summary.InvoiceDetial3", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ���ܷ�Ʊ��ϸ,������ϸ��¼ʧ�ܣ�";
                    return -1;
                }
                #endregion

                #region ֧����ʽ��

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.InvoicePayMode1", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceNo, employee.ID, invoiceSeqPositive, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ����֧����ʽʧ�ܣ�";
                    return -1;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.InvoicePayMode2", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceSeqNegative, invoiceSeqWhere, this.Operator.ID);//{96A6C0EA-2F0F-4a19-8567-3178C505E3FE}����sql����bug����˲�����
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ����֧����ʽ,����֧����ʽ��¼ʧ�ܣ�";
                    return -1;
                }

                this.Err = "";
                strSql = "";
                if (this.Sql.GetCommonSql("Fee.AccountInvoice.Summary.InvoicePayMode3", ref strSql) == -1)
                {
                    this.Err = Sql.Err;
                    return -1;
                }
                strSql = string.Format(strSql, invoiceSeqWhere);
                iRes = this.ExecNoQuery(strSql);
                if (iRes <= 0)
                {
                    this.Err += "  ����֧����ʽ,����֧����ʽ��¼ʧ�ܣ�";
                    return -1;
                }

                #endregion

                #region ���������ϸ��
                ArrayList feeItemList = null;
                DateTime nowTime = GetDateTimeFromSysDateTime();
                foreach (Balance objInvoice in lstInvoice)
                {
                    feeItemList = QueryFeeItemListsByInvoiceSequence(objInvoice.CombNO);
                    if (feeItemList == null || feeItemList.Count <= 0)
                    {
                        //this.Err = "��û��߷�����ϸ����!  " + this.Err;
                        //return -1;
                        continue;
                    }
                    iRes = UpdateFeeItemListCancelType(objInvoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    if (iRes <= 0)
                    {
                        this.Err = "���ϻ�����ϸ����!  " + this.Err;
                        return -1;
                    }

                    foreach (FeeItemList f in feeItemList)
                    {
                        f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        f.FT.OwnCost = -f.FT.OwnCost;
                        f.FT.PayCost = -f.FT.PayCost;
                        f.FT.PubCost = -f.FT.PubCost;
                        f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        f.Item.Qty = -f.Item.Qty;
                        f.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                        //f.FeeOper.ID = employee.ID;
                        //f.FeeOper.OperTime = nowTime;
                        f.ChargeOper.ID = employee.ID;
                        f.ChargeOper.OperTime = nowTime;
                        f.InvoiceCombNO = invoiceSeqNegative;

                        iRes = InsertFeeItemList(f);
                        if (iRes <= 0)
                        {
                            this.Err = "���������ϸ������Ϣ����!  " + this.Err;
                            return -1;
                        }
                    }

                    foreach (FeeItemList f in feeItemList)
                    {
                        f.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                        f.FT.OwnCost = -f.FT.OwnCost;
                        f.FT.PayCost = -f.FT.PayCost;
                        f.FT.PubCost = -f.FT.PubCost;
                        f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        f.Item.Qty = -f.Item.Qty;
                        f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                        //f.FeeOper.ID = employee.ID;
                        //f.FeeOper.OperTime = nowTime;
                        f.ChargeOper.ID = employee.ID;
                        f.ChargeOper.OperTime = nowTime;

                        f.Invoice.ID = invoiceNo;
                        f.InvoiceCombNO = invoiceSeqPositive;

                        iRes = InsertFeeItemList(f);
                        if (iRes <= 0)
                        {
                            this.Err = "���������ϸ��Ϣ����!  " + this.Err;
                            return -1;
                        }
                    }
                }


                #endregion

            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                return -1;
            }

            return 1;
        }

        #endregion

        #region �շ��ս�

        /// <summary>
        /// �������Ա�ս�
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        /// <param name="operCode">����</param>
        /// <param name="balancer">�ս���</param>
        /// <param name="beginDate">�ϴ��ս�ʱ��</param>
        /// <param name="endDate">�����ս����ʱ��</param>
        /// <returns>-1����1�ɹ�</returns>
        public int DealOperDayBalance(string operCode, string balancer, string beginDate, string endDate)
        {
            string strReturn = "";
            string strSql = "";
            /*strSql = "pkg_rep.prc_opb_daybalance,opercode,22,1,{0}," +
                "begindate,22,1,{1}," +
                "endate,22,1,{2}," +
                "Par_ErrCode,13,2,1," +
                "Par_ErrText,22,2,1";
            */
            if (Sql.GetSql("Fee.Outpatient.Procedurce.DayBalance", ref strSql) == -1)
            {
                this.Err = "ִ�д洢����ʧ�ܣ�û���ҵ�sql��Fee.Outpatient.Procedurce.DayBalance";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, operCode, balancer, beginDate, endDate);
                if (this.ExecEvent(strSql, ref strReturn) == -1)
                {
                    this.Err = "ִ�д洢���̳���" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[1]) == -1)
                {
                    this.Err = str[0];
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// ����ClinicNo �������е���Ŀ��Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryCmsAllFeeItemListsByClinicNO(string clinicNO)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetAllDetail.Cms.Select.Fee", clinicNO);
        }


        #region ���︽��
        /// <summary>
        /// ���ݴ�����ɾ��������Ϣ
        /// ����������ȫɾ��
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <returns>-1ʧ�� 0û�и���</returns>
        public int DeleteSubFeeItem(string recipeNO)
        {
            return UpdateSingleTable("SOC.Fee.Outpatient.DeleteSub.ByRecipeNO", recipeNO);
        }
        #endregion

        #region �����շ��б����
        /// <summary>
        /// ��ȡָ���շ�Ա
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public ArrayList QueryBalanceListsByOper(string operCode)
        {
            return this.QueryBalances("Fee.OutPatient.Fee.GetBalanceByOper", operCode);
        }

        /// <summary>
        /// ��ȡ���������շ�
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryBalancesAllByCardNO(string cardNo, DateTime beginTime, DateTime endTime)
        {
            return this.QueryBalances("Fee.OutPatient.GetInvoiceInfoByPatientCardNo.Where.all", cardNo, beginTime.ToString(), endTime.ToString());

        }

        /// <summary>
        /// ���ݽ������,��ȡ��ͬ������ŵĽ�����Ϣ(��Ч������Ϣ)   
        /// </summary>
        /// <param name="invoiceSequence">�������</param>
        /// <returns>�ɹ�: ������Ϣ���� ʧ��: null</returns>
        public ArrayList QueryBalancesAllByInvoiceSequence(string invoiceSequence)
        {
            return this.QueryBalances("Fee.OutPatient.GetInvoInfo.Where.Seq.1", invoiceSequence);
        }

        /// <summary>
        /// ���ݽ������л�÷�����ϸ
        /// </summary>
        /// <param name="invoiceSequence"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByAllInvoiceSequence(string invoiceSequence)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where.Seq.all", invoiceSequence);
        }

        #endregion

        #region ���ʻ���ҵ�����
        /// <summary>
        /// �жϼ��ʻ������
        /// 
        /// return > 0 ��ʾ���ʻ���
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int IsKeepAccountPatient(string cardNo)
        {
            int iRes = 0;
            if (string.IsNullOrEmpty(cardNo))
            {
                return iRes;
            }
            string strCurrentDate = this.GetSysDateTime();

            //string strSql = "";

            //if (this.Sql.GetCommonSql("Fee.FSlocal.KeepAccountPatient.JudgePatient", ref strSql) == -1)
            //{
            //    this.Err = "û���ҵ�����Ϊ: Fee.FSlocal.KeepAccountPatient.JudgePatient ��SQL���";

            //    return -1;
            //}
            //try
            //{
            //    strSql = string.Format(strSql, strCurrentDate, strCurrentDate, cardNo);
            //}
            //catch (Exception e)
            //{
            //    this.Err = e.Message;
            //    this.WriteErr();

            //    return -1;
            //}

            string strTemp = this.ExecSqlReturnOne("Fee.FSlocal.KeepAccountPatient.JudgePatient", strCurrentDate, strCurrentDate, cardNo);
            int.TryParse(strTemp, out iRes);

            return iRes;
        }

        #endregion

        #region ��ȡ���ѻ�����Ϣ
        /// <summary>
        /// ��ȡ���ѻ�����Ϣ
        /// </summary>
        /// <param name="SSN">ҽ��֤��</param>
        /// <returns>�ɹ���Obj��ʧ�ܣ�NULL</returns>
        public FS.FrameWork.Models.NeuObject GetPubPatient(string SSN)
        {
            FS.FrameWork.Models.NeuObject objTemp = new FS.FrameWork.Models.NeuObject();

            string query = string.Empty;
            if (this.Sql.GetCommonSql("select.com_pub_patientinfo", ref query) == -1)
            {
                this.Err += "wwwww";
                return null;
            }
            query = string.Format(query, SSN);

            try
            {
                if (this.ExecQuery(query) == -1)
                {
                    this.Err += "eee";
                    return null;
                }
                while (this.Reader.Read())
                {
                    objTemp.ID = this.Reader[0].ToString();
                    objTemp.User01 = this.Reader[1].ToString();
                    objTemp.User02 = this.Reader[2].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                this.Err += ex.Message;
                return null;
            }

            return objTemp;
        }
        #endregion

        #region �ж���Ŀ�Ƿ�Ϊ������Ŀ
        /// <summary>
        /// �ж���Ŀ�Ƿ�Ϊ������Ŀ
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <returns>�ǣ�true;��false</returns>
        public bool IsMalignPha(string itemCode)
        {
            string sqlQuery = "";

            if (this.Sql.GetCommonSql("GetPhaMalignFlag", ref sqlQuery) == -1)
            {
                this.Err += "��ȡSQL����GetPhaMalignFlag";
                return false;
            }

            sqlQuery = string.Format(sqlQuery, itemCode);

            try
            {
                string temp = string.Empty;
                if (this.ExecQuery(sqlQuery) == -1)
                {
                    this.Err += "��ȡҩƷ��Ϣ����";
                    return false;
                }
                while (this.Reader.Read())
                {
                    temp = this.Reader[0].ToString();
                }
                this.Reader.Close();

                if (temp == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Err += ex.Message;
                this.Reader.Close();
                return false;
            }
        }
        #endregion

        #region ������Ŀ�����ȡ��Ŀ
        /// <summary>
        /// ������Ŀ�����ȡ��Ŀ
        /// </summary>
        /// <param name="itemcode"></param>
        /// <returns></returns>
        public FeeItemList GetFeeItemByItemCode(string itemcode)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("GetFeeItemByItemCode", ref sql) == -1)
            {
                sql = @"select item_code,item_name,fee_code,unit_price from fin_com_undruginfo where item_code='{0}' and VALID_STATE='1'";
            }
            try
            {
                sql=string.Format(sql, itemcode);
                if (this.ExecQuery(sql) == -1)
                {
                    this.Err += "��ȡ��Ŀ��Ϣ����";
                    return null;
                }
                FeeItemList feeItemList = new FeeItemList();
                while (this.Reader.Read())
                {
                    feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                    feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    feeItemList.Item.ID = Reader[0].ToString();
                    feeItemList.ID = Reader[0].ToString();
                    feeItemList.Item.Name = Reader[1].ToString();
                    feeItemList.Name = Reader[1].ToString();
                    feeItemList.FTSource = "0";//�շ�Ա�Լ��շ�
                    feeItemList.Item.PriceUnit = "��";
                    feeItemList.Item.Qty = 1;
                    feeItemList.Item.Price = FS.FrameWork.Public.String.FormatNumber(decimal.Parse(Reader[3].ToString()), 4);
                    feeItemList.Item.MinFee.ID = Reader[2].ToString();

                }
                this.Reader.Close();
                return feeItemList;


            }
            catch (Exception ex)
            {
                this.Err += ex.Message;
                this.Reader.Close();
                return null;
            }
 
        }
        #endregion

        #region ���ѱ������
        /// <summary>
        /// ��ȡ����Ա�յĹ��ѷ�Ʊ��Ϣ
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pactHead"></param>
        /// <returns></returns>
        public ArrayList QueryPubFeeInvoice(DateTime beginDate, DateTime endDate)
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Local.Pub.OutPatientFee.GetInvoiceInfo", ref sql) == -1)
            {
                this.Err += "û���ҵ�����Ϊ: Local.Pub.OutPatientFee.GetInvoiceInfo ��SQL���";

                return null;
            }

            return this.QueryBalancesBySql(sql, beginDate.ToString(), endDate.ToString());
        }

        /// <summary>
        /// ��ҽ���������ֶ����ش˷���
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="withOverLimitDrugFee"></param>
        /// <returns></returns>
        public ArrayList QueryPubFeeInvoice(DateTime beginDate, DateTime endDate, bool withOverLimitDrugFee)
        {
            if (!withOverLimitDrugFee)
            {
                return this.QueryPubFeeInvoice(beginDate, endDate);
            }
            else
            {
                string sql = string.Empty;

                if (this.Sql.GetCommonSql("Local.Pub.OutPatientFee.GetInvoiceInfoWithOverLimitDrugFee", ref sql) == -1)
                {
                    this.Err += "û���ҵ�����Ϊ: Local.Pub.OutPatientFee.GetInvoiceInfoWithOverLimitDrugFee ��SQL���";

                    return null;
                }
                return this.QueryBalancesBySql(sql, beginDate.ToString(), endDate.ToString());
            }

        }

        /// <summary>
        /// �����ն�ȷ��
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="sequence_No"></param>
        /// <returns></returns>
        public int CancleTechApply(string recipeNo, int sequence_No)
        {

            // sql���
            string sql = "";
            //
            // ��ȡsql���
            //
            if (this.Sql.GetCommonSql("Met.CancleTechApply", ref sql) == -1)
            {
                sql = @"update met_tec_terminalapply  set ext_flag1='0' where recipe_no='{0}' and sequence_no='{1}'";
            }
            //
            // ƥ��ִ��
            //
            try
            {
                sql = string.Format(sql, recipeNo, sequence_No);

                return this.ExecNoQuery(sql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }

        /// <summary>
        /// �ж��Ƿ�����ն������
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="sequence_No"></param>
        /// <returns></returns>
        public bool  IsHaveTechApplyNo(string recipeNo, int sequence_No)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Outpatient.IsHaveTechApplyNo", ref strSql) == -1)
            {
                strSql = @"select count(1) from met_tec_terminalapply a where a.recipe_no ='{0}' and a.sequence_no={1}";
            }

            strSql = System.String.Format(strSql,recipeNo, sequence_No);
            string temp="0";
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err += "�ж��Ƿ�����ն�����ų���";
                return false;
            }
            while (this.Reader.Read())
            {
                temp = this.Reader[0].ToString();
            }
            this.Reader.Close();

            if (temp == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}