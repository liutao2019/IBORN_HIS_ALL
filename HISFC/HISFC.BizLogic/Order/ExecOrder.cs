using System;
using Neusoft.HISFC.Object;
using System.Collections;
using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Management.Order
{
	/// <summary>
	/// ҽ�������ࡣ
	/// </summary>
	public class ExecOrder:Neusoft.NFC.Management.Database 
	{
		public ExecOrder()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		// <summary>
		// ����ִ�е�(����ִ�е���¼)
		// </summary>
		// <param name="ExecOrder"></param>
		// <returns>0 success -1 fail</returns>
//		public int CreateExec(Object.Order.ExecOrder ExecOrder)
//		{
//			#region insertִ�е�
//			///����ִ�е�
//			///Order.ExecOrder.CreateExec.1
//			///���룺71
//			///������0 
//			#endregion
//			string strSql="";
//			string strItemType="";
//
//			strItemType=JudgeItemType(ExecOrder);
//			if (strItemType=="") return -1;
//			#region "ҩ��ִ�е�"
//			if (strItemType== "1")
//			{
//				Neusoft.HISFC.Object.Pharmacy.Item objPharmacy ;
//				objPharmacy = (Neusoft.HISFC.Object.Pharmacy.Item)ExecOrder.Order.Item;
//
//				if(this.Sql.GetSql("Order.ExecOrder.CreateExec.Drug.1",ref strSql)==-1) return -1;
//	
//				#region "ҩ���ӿ�˵��"
//				//0 IDִ����ˮ��
//				//������Ϣ����  
//				//			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
//				//ҽ��������Ϣ
//				// ������Ŀ��Ϣ
//				//	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
//				//	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
//				//         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
//				//         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
//				//         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
//				// ����ҽ������
//				//		   30ҽ�������� 31ҽ����ˮ��  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
//				//		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��  
//				// ����ִ�����
//				//		   37����ҽʦId   38����ҽʦname  39Ҫ��ִ��ʱ��  40����ʱ��     41��������
//				//		   42����ʱ��     43�����˴���    44�����˴���    45���˿��Ҵ��� 46����ʱ��       
//				//		   47ȡҩҩ��     48ִ�п���      49ִ�л�ʿ����  50ִ�п��Ҵ��� 51ִ��ʱ��
//				//         52�ֽ�ʱ�� 
//				// ����ҽ������
//				//		   52�Ƿ�Ӥ����1��/2��          53�������  	  54������     55��ҩ��� 
//				//		   56�Ƿ��������                 57�Ƿ���Ч      58�ۿ���     59�Ƿ�ִ�� 
//				//		   60��ҩ���                     61�շѱ��      62ҽ��˵��     63��ע 
//				#endregion 
//				try
//				{
//					string[] s={ExecOrder.ID,ExecOrder.Order.Patient.ID,ExecOrder.Order.Patient.Patient.PID.PatientNo,ExecOrder.Order.Patient.PVisit.PatientLocation.Dept.ID,ExecOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID,
//								   strItemType,ExecOrder.Order.Item.ID,ExecOrder.Order.Item.Name,ExecOrder.Order.Item.UserCode,ExecOrder.Order.Item.SpellCode,
//								   ExecOrder.Order.Item.SysClass.ID.ToString(),ExecOrder.Order.Item.SysClass.Name,objPharmacy.Specs,objPharmacy.BaseDose.ToString(),objPharmacy.DoseUnit,objPharmacy.MinUnit,objPharmacy.PackQty.ToString(),
//								   objPharmacy.DosageForm.ID,objPharmacy.Type.ID,objPharmacy.Quality.ID.ToString(),objPharmacy.RetailPrice.ToString(),
//								   ExecOrder.Order.Usage.ID,ExecOrder.Order.Usage.Name,ExecOrder.Order.Usage.Memo,ExecOrder.Order.Frequency.ID,ExecOrder.Order.Frequency.Name,
//								   ExecOrder.Order.DoseOnce.ToString(),ExecOrder.Order.QTY.ToString(),ExecOrder.Order.Unit,ExecOrder.Order.Usetimes.ToString(),
//								   ExecOrder.Order.OrderType.ID,ExecOrder.Order.OrderType.Name,System.Convert.ToInt16(ExecOrder.Order.OrderType.IsDecompose).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsCharge).ToString(),
//								   System.Convert.ToInt16(ExecOrder.Order.OrderType.IsNeedPharmacy).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsPrint).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsConfirm).ToString(),
//								   ExecOrder.Order.ReciptDoct.ID,ExecOrder.Order.ReciptDoct.Name,ExecOrder.DateUse.ToString(),ExecOrder.DcExecTime.ToString(),ExecOrder.Order.ReciptDept.ID,
//								   ExecOrder.Order.Date_MO.ToString(),ExecOrder.DcExecUser.ID,ExecOrder.ChargeUser.ID,ExecOrder.ReciptDept.ID,ExecOrder.ChargeTime.ToString(),
//								   ExecOrder.StockDept.ID,ExecOrder.ExeDept.ID,ExecOrder.ExecUser.ID,ExecOrder.ReciptDept.ID,ExecOrder.ExecTime.ToString(),
//								   ExecOrder.DateDeco.ToString(),
//								   System.Convert.ToInt16(ExecOrder.Order.IsBaby).ToString(),ExecOrder.Order.BabyNo.ToString(),ExecOrder.Order.Combo.ID,System.Convert.ToInt16(ExecOrder.Order.Combo.MainDrug).ToString(),
//								   System.Convert.ToInt16(ExecOrder.Order.IsHaveSubtbl).ToString(),System.Convert.ToInt16(ExecOrder.IsInvalid).ToString(),System.Convert.ToInt16(ExecOrder.Order.IsStock).ToString(),System.Convert.ToInt16(ExecOrder.IsExec).ToString(),
//								   ExecOrder.DrugFlag.ToString(),System.Convert.ToInt16(ExecOrder.IsCharge).ToString(),ExecOrder.Order.Note,ExecOrder.Order.Memo};
//					strSql=string.Format(strSql,s);
//				}
//				catch(Exception ex)
//				{
//					this.Err="����ֵʱ�����"+ex.Message;
//					this.WriteErr();
//					return -1;
//				}
//			}			
//			#endregion
//			#region "��ҩ��ִ�е�"
//			else if (strItemType== "2")
//			{
//				Neusoft.HISFC.Object.Fee.Item objAssets;
//				objAssets = (Neusoft.HISFC.Object.Fee.Item)ExecOrder.Order.Item;
//
//				if(this.Sql.GetSql("Order.ExecOrder.CreateExec.Undrug.1",ref strSql)==-1) return -1;	
//				#region "��ҩ���ӿ�˵��"
//				//0 IDִ����ˮ��
//				//������Ϣ����  
//				//			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
//				//ҽ��������Ϣ
//				// ������Ŀ��Ϣ
//				//	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
//				//	       10��Ŀ������ 11��Ŀ�������  12���         13���ۼ�        14�÷�����   
//				//         15�÷�����     16�÷�Ӣ����д  17Ƶ�δ���     18Ƶ������      19ÿ������
//				//         20��Ŀ����     21�Ƽ۵�λ      22ʹ�ô���			  
//				// ����ҽ������
//				//		   23ҽ�������� 24ҽ����ˮ��    25ҽ���Ƿ�ֽ�:1����/2��ʱ     26�Ƿ�Ʒ� 
//				//		   27ҩ���Ƿ���ҩ 28��ӡִ�е�    29�Ƿ���Ҫȷ��  
//				// ����ִ�����
//				//		   30����ҽʦId   31����ҽʦname  32Ҫ��ִ��ʱ��  33����ʱ��     34��������
//				//		   35����ʱ��     36�����˴���    37�����˴���    38���˿��Ҵ��� 39����ʱ��       
//				//		   40ȡҩҩ��     41ִ�п���      42ִ�л�ʿ����  43ִ�п��Ҵ��� 44ִ��ʱ��
//				//       45�ֽ�ʱ��     46ִ�п�������
//				// ����ҽ������
//				//		   47�Ƿ�Ӥ����1��/2��          48�������  	  49������     50������ 
//				//		   51�Ƿ񸽲�                     52�Ƿ��������  53�Ƿ���Ч     54�Ƿ�ִ�� 
//				//		   55�շѱ��     56�Ӽ����      57��鲿λ����  58ҽ��˵��     59��ע 
//				#endregion 
//				try
//				{
//					string[] s={ExecOrder.ID,ExecOrder.Order.Patient.ID,ExecOrder.Order.Patient.Patient.PID.PatientNo,ExecOrder.Order.Patient.PVisit.PatientLocation.Dept.ID,ExecOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID,
//								   strItemType,ExecOrder.Order.Item.ID,ExecOrder.Order.Item.Name,ExecOrder.Order.Item.UserCode,ExecOrder.Order.Item.SpellCode,
//								   ExecOrder.Order.Item.SysClass.ID.ToString(),ExecOrder.Order.Item.SysClass.Name,objAssets.Specs,objAssets.Price.ToString(),ExecOrder.Order.Usage.ID,
//								   ExecOrder.Order.Usage.Name,ExecOrder.Order.Usage.Memo,ExecOrder.Order.Frequency.ID,ExecOrder.Order.Frequency.Name,ExecOrder.Order.DoseOnce.ToString(),
//								   ExecOrder.Order.QTY.ToString(),ExecOrder.Order.Unit,ExecOrder.Order.Usetimes.ToString(),
//								   ExecOrder.Order.OrderType.ID,ExecOrder.Order.OrderType.Name,System.Convert.ToInt16(ExecOrder.Order.OrderType.IsDecompose).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsCharge).ToString(),
//								   System.Convert.ToInt16(ExecOrder.Order.OrderType.IsNeedPharmacy).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsPrint).ToString(),System.Convert.ToInt16(ExecOrder.Order.OrderType.IsConfirm).ToString(),
//								   ExecOrder.Order.ReciptDoct.ID,ExecOrder.ReciptDoct.Name,ExecOrder.DateUse.ToString(),ExecOrder.DcExecTime.ToString(),ExecOrder.Order.ReciptDept.ID,
//								   ExecOrder.Order.Date_MO.ToString(),ExecOrder.DcExecUser.ID,ExecOrder.ChargeUser.ID,ExecOrder.ReciptDept.ID,ExecOrder.ChargeTime.ToString(),
//								   ExecOrder.StockDept.ID,ExecOrder.ExeDept.ID,ExecOrder.ExecUser.ID,ExecOrder.ReciptDept.ID,ExecOrder.ExecTime.ToString(),
//								   ExecOrder.DateDeco.ToString(),ExecOrder.ExeDept.Name,
//								   System.Convert.ToInt16(ExecOrder.Order.IsBaby).ToString(),ExecOrder.Order.BabyNo.ToString(),ExecOrder.Order.Combo.ID,System.Convert.ToInt16(ExecOrder.Order.Combo.MainDrug).ToString(),
//								   System.Convert.ToInt16(ExecOrder.Order.IsSubtbl).ToString(),System.Convert.ToInt16(ExecOrder.Order.IsHaveSubtbl).ToString(),System.Convert.ToInt16(ExecOrder.IsInvalid).ToString(),System.Convert.ToInt16(ExecOrder.IsExec).ToString(),
//								   System.Convert.ToInt16(ExecOrder.IsCharge).ToString(),System.Convert.ToInt16(ExecOrder.Order.IsEmergency).ToString(),ExecOrder.Order.CheckPartRecord,ExecOrder.Order.Note,ExecOrder.Order.Memo};
//					strSql=string.Format(strSql,s);
//				}
//				catch(Exception ex)
//				{
//					this.Err="����ֵʱ�����"+ex.Message;
//					this.WriteErr();
//					return -1;
//				}
//			}
//			#endregion
//		
//			if (strSql == null) return -1;
//			
//			return this.ExecNoQuery(strSql);
//		}
//		private string JudgeItemType(Object.Order.ExecOrder ExecOrder)
//		{
//			string strItem="";
//			//�ж�ҩƷ/��ҩƷ 
//			if (ExecOrder.Order.Item.GetType().ToString()== "Neusoft.HISFC.Object.Pharmacy.Item")
//			{
//				strItem="1";
//			}
//			else if (ExecOrder.Order.Item.GetType().ToString()== "Neusoft.HISFC.Object.Fee.Item")
//			{
//				strItem="2";
//			}
//			return strItem;
//		}
//		#region "����ִ�е�"
//		/// <summary>
//		/// ����ִ�е�
//		/// </summary>
//		/// <param name="ExecOrder">ִ�е���Ϣ</param>
//		/// <returns>0 success -1 fail</returns>
//		public int dcExec(Object.Order.ExecOrder ExecOrder)
//		{
//			#region ����ִ�е�
//			///����ִ�е�(ҽ��ֹͣ��ֱ������)
//			///Order.ExecOrder.dcExec
//			///���룺0 id��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4���ϱ�־ 
//			///������0 
//			#endregion
//			string strSql="",strSqlName="Order.ExecOrder.dcExec.";
//			string strItemType="";
//
//			strItemType=JudgeItemType(ExecOrder);
//			if (strItemType=="") return -1;
//			strSqlName= strSqlName + strItemType;
//
//			if(this.Sql.GetSql(strSqlName,ref strSql)==-1) return -1;
//			try
//			{
//				strSql=string.Format(strSql,ExecOrder.ID,ExecOrder.DcExecUser.ID,ExecOrder.DcExecUser.Name,ExecOrder.DcExecTime.ToString(),System.Convert.ToInt16(ExecOrder.IsInvalid).ToString());
//			}
//			catch
//			{
//				this.Err="�����������"+strSqlName;
//				return -1;
//			}
//			return this.ExecNoQuery(strSql);
//		}
//		/// <summary>
//		/// ��ҽ����ˮ������ִ�е�
//		/// </summary>
//		/// <param name="OrderNo"></param>
//		/// <returns>0 success -1 fail</returns>
//		public int dcExec(string OrderNo,string ItemType,Neusoft.NFC.Object.NeuObject dcPerson)
//		{
//			#region ��ҽ����ˮ������ִ�е�
//			///����ִ�е�(ҽ��ֹͣ��ֱ������)
//			///Order.ExecOrder.dcOrderExec
//			///���룺0 orderid��1 ֹͣ��id,2ֹͣ��������3ֹͣʱ��,4���ϱ�־ 
//			///������0 
//			#endregion
//			string strSql="",strSqlName="Order.ExecOrder.dcOrderExec.";
//			
//			if (ItemType=="") return -1;
//			strSqlName= strSqlName + ItemType;
//
//			if(this.Sql.GetSql(strSqlName,ref strSql)==-1) return -1;
//			try
//			{
//				strSql=string.Format(strSql,OrderNo,dcPerson.ID,dcPerson.Name,this.GetSysDateTime(),"0");
//			}
//			catch
//			{
//				this.Err="�����������"+strSqlName;
//				return -1;
//			}
//			return this.ExecNoQuery(strSql);
//		}
//		/// <summary>
//		/// ִ�м�¼
//		/// </summary>
//		/// <param name="ExecOrder">ִ�е���Ϣ</param>
//		/// <returns>0 success -1 fail</returns>
//		public int RecordExec(Object.Order.ExecOrder ExecOrder)
//		{
//			#region ִ�м�¼
//			///ִ�м�¼
//			///Order.ExecOrder.RecordExec.1
//			///���룺0 id��1 ִ����id,2ִ�п��ң�3ִ�п������� 4ִ��ʱ��,5ִ�б�־ 
//			///������0 
//			#endregion
//			string strSql="",strSqlName="Order.ExecOrder.RecordExec.";
//			string strItemType="";
//
//			strItemType=JudgeItemType(ExecOrder);
//			if (strItemType=="") return -1;
//			strSqlName= strSqlName + strItemType;
//
//			if(this.Sql.GetSql(strSqlName,ref strSql)==-1) return -1;
//			try
//			{
//				strSql=string.Format(strSql,ExecOrder.ID,ExecOrder.DcExecUser.ID,ExecOrder.DcExecUser.Name,ExecOrder.DcExecTime.ToString(),System.Convert.ToInt16(ExecOrder.IsInvalid).ToString());
//			}
//			catch
//			{
//				this.Err="����������ԣ�"+strSqlName;
//				return -1;
//			}
//			return this.ExecNoQuery(strSql);
//		}
//		/// <summary>
//		/// �շѼ�¼
//		/// </summary>
//		/// <param name="ExecOrder">ִ�е���Ϣ</param>
//		/// <returns>0 success -1 fail</returns>
//		public int ChargeExec(Object.Order.ExecOrder ExecOrder)
//		{
//			#region �շѼ�¼
//			///�շѼ�¼
//			///Order.ExecOrder.Charge.
//			///���룺0 id��1 �շ���id,2�շѿ���ID��3�շ�ʱ��,5�շѱ�־ 
//			///������0 
//			#endregion
//			string strSql="",strSqlName="Order.ExecOrder.Charge.";
//			string strItemType="";
//
//			strItemType=JudgeItemType(ExecOrder);
//			if (strItemType=="") return -1;
//			strSqlName= strSqlName + strItemType;
//
//			if(this.Sql.GetSql(strSqlName,ref strSql)==-1) return -1;
//			try
//			{
//				strSql=string.Format(strSql,ExecOrder.ID,ExecOrder.ChargeUser.ID,ExecOrder.ChargeUser.Name,ExecOrder.ChargeTime.ToString(),System.Convert.ToInt16(ExecOrder.IsCharge).ToString());
//			}
//			catch
//			{
//				this.Err="����������ԣ�"+strSqlName;
//				return -1;
//			}
//			return this.ExecNoQuery(strSql);
//		}
//		/// <summary>
//		/// ��ҩ��¼
//		/// </summary>
//		/// <param name="ExecOrder">ִ�е���Ϣ</param>
//		/// <returns>0 success -1 fail</returns>
//		public int DrugExec(Object.Order.ExecOrder ExecOrder)
//		{
//			#region ��ҩ��¼
//			///��ҩ��¼
//			///Order.ExecOrder.DrugExec.
//			///���룺0 id��1 ��ҩ״̬ 
//			///������0 
//			#endregion
//			string strSql="";
//			string strItemType="";
//
//			strItemType=JudgeItemType(ExecOrder);
//			if (strItemType !="1") return -1;
//
//			if(this.Sql.GetSql("Order.ExecOrder.DrugExec.1",ref strSql)==-1) return -1;
//			try
//			{
//				strSql=string.Format(strSql,ExecOrder.ID,ExecOrder.DrugFlag.ToString());
//			}
//			catch
//			{
//				this.Err="����������ԣ�Order.ExecOrder.DrugExec.1";
//				return -1;
//			}
//			return this.ExecNoQuery(strSql);
//		}
//		#endregion 
//
//		#region "��ѯҽ��ִ����Ϣ"
//		/// <summary>
//		/// ��ѯ����ҽ��ִ�����
//		/// </summary>
//		/// <param name="InPatientNo"></param>
//		/// <param name="ItemType">""ȫ����1ҩ2��ҩ</param>
//		/// <returns></returns>
//		public ArrayList QueryPatientExec(string InPatientNo,string ItemType)
//		{
//			#region ��ѯ����ҽ��ִ�����
//			///��ѯ����ҽ��ִ�������ҩ����ҩ��
//			///Order.ExecOrder.QueryPatientExec.1
//			///���룺0 inpatientno
//			///������ArrayList
//			#endregion
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			
//			s= ExecOrderQuerySelect(ItemType);
//			for (int i=0;i<2;i++)
//			{
//				sql= s[i];
//				if (sql==null ) return null;
//				if(this.Sql.GetSql("Order.ExecOrder.QueryPatientExec.1",ref sql1)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryPatientExec.1�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				sql= sql +" " +string.Format(sql1,InPatientNo);
//				addExecOrder(al,sql);
//			}
//			return al;
//		}
//		/// <summary>
//		/// ����ѯ�Ƿ���Чִ��ҽ��
//		/// </summary>
//		/// <param name="InPatientNo"></param>
//		/// <param name="ItemType"></param>
//		/// <param name="IsValid"></param>
//		/// <returns></returns>
//		public ArrayList QueryValidOrder(string InPatientNo,string ItemType,bool IsValid)
//		{
//			#region ����ѯ��Чִ��ҽ��
//			///����ѯ��Чִ��ҽ��
//			///Order.ExecOrder.QueryValidOrder.1
//			///���룺0 inpatientno 1  IsValid
//			///������ArrayList
//			#endregion
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			
//			s= ExecOrderQuerySelect(ItemType);
//			for (int i=0;i<2;i++)
//			{
//				sql= s[i];
//				if (sql==null ) return null;
//				if(this.Sql.GetSql("Order.ExecOrder.QueryValidOrder.1",ref sql1)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryValidOrder.1�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				sql= sql +" " +string.Format(sql1,InPatientNo,System.Convert.ToInt16(IsValid).ToString());
//				addExecOrder(al,sql);
//			}
//			return al;
//		}
//		/// <summary>
//		/// ����ѯ�Ƿ�ִ��ҽ��
//		/// </summary>
//		/// <param name="InPatientNo"></param>
//		/// <param name="ItemType"></param>
//		/// <param name="IsExec"></param>
//		/// <returns></returns>
//		public ArrayList QueryExecOrder(string InPatientNo,string ItemType,bool IsExec)
//		{
//			#region ����ѯ�Ƿ�ִ��ҽ��
//			///����ѯ�Ƿ�ִ��ҽ��
//			///Order.ExecOrder.QueryExecOrder.1
//			///���룺0 inpatientno 1 IsExec
//			///������ArrayList
//			#endregion
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			
//			s= ExecOrderQuerySelect(ItemType);
//			for (int i=0;i<2;i++)
//			{
//				sql= s[i];
//				if (sql==null ) return null;
//				if(this.Sql.GetSql("Order.ExecOrder.QueryExecOrder.1",ref sql1)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryExecOrder.1�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				sql= sql +" " +string.Format(sql1,InPatientNo,System.Convert.ToInt16(IsExec).ToString());
//				addExecOrder(al,sql);
//			}
//			return al;
//		}
//		/// <summary>
//		/// ����ѯ�Ƿ��շ�ҽ��
//		/// </summary>
//		/// <param name="InPatientNo"></param>
//		/// <param name="ItemType"></param>
//		/// <param name="IsCharge"></param>
//		/// <returns></returns>
//		public ArrayList QueryChargeOrder(string InPatientNo,string ItemType,bool IsCharge)
//		{
//			#region ����ѯ�Ƿ��շ�ҽ��
//			///����ѯ�Ƿ��շ�ҽ��
//			///Order.ExecOrder.QueryChargeOrder.1
//			///���룺0 inpatientno 1  IsCharge
//			///������ArrayList
//			#endregion
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			
//			s= ExecOrderQuerySelect(ItemType);
//			for (int i=0;i<2;i++)
//			{
//				sql= s[i];
//				if (sql==null ) return null;
//				if(this.Sql.GetSql("Order.ExecOrder.QueryChargeOrder.1",ref sql1)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryChargeOrder.1�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				sql= sql +" " +string.Format(sql1,InPatientNo,System.Convert.ToInt16(IsCharge).ToString());
//				addExecOrder(al,sql);
//			}
//			return al;
//		}
//		/// <summary>
//		/// ����ѯ��ҩ״̬ҽ��
//		/// </summary>
//		/// <param name="InPatientNo"></param>
//		/// <param name="DrugFlag"></param>
//		/// <returns></returns>
//		public ArrayList QueryOrderDrugFlag(string InPatientNo,int DrugFlag)
//		{
//			#region ����ѯ��ҩ״̬ҽ��
//			///����ѯ��ҩ״̬ҽ��
//			///Order.ExecOrder.QueryOrderDrugFlag.1
//			///���룺0 inpatientno 1  DrugFlag
//			///������ArrayList
//			#endregion
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			
//			s= ExecOrderQuerySelect("1");
//			sql= s[1];
//			if (sql==null ) return null;
//			if(this.Sql.GetSql("Order.ExecOrder.QueryOrderDrugFlag.1",ref sql1)==-1)
//			{
//				this.Err="û���ҵ�Order.ExecOrder.QueryOrderDrugFlag.1�ֶ�!";
//				this.ErrCode="-1";
//				this.WriteErr();
//				return null;
//			}
//			sql= sql +" " +string.Format(sql1,InPatientNo,DrugFlag.ToString());
//			return this.myExecOrderQuery(sql);
//		}	
//		/// <summary>
//		/// ��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
//		/// </summary>
//		/// <param name="OrderNo"></param>
//		/// <param name="ItemType">1ҩ2��ҩ""ȫ��</param>
//		/// <returns></returns>
//		public ArrayList QueryOneOrder(string OrderNo,string ItemType)
//		{
//			string[] s;
//			string sql="",sql1="";
//			ArrayList al=new ArrayList();
//			#region ��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
//			///��ҽ����ˮ�Ų�ѯҽ��ִ����Ϣ
//			///Order.ExecOrder.QueryOrder.where.5
//			///���룺0 OrderNo
//			///������ArrayList
//			#endregion
//			s= ExecOrderQuerySelect(ItemType);
//			for (int i=0;i<2;i++)
//			{
//				sql= s[i];
//				if (sql==null ) return null;
//				if(this.Sql.GetSql("Order.ExecOrder.Query.where.5",ref sql1)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.Query.where.5�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				sql= sql +" " +string.Format(sql1,OrderNo);
//				addExecOrder(al,sql);
//			}
//			return al;
//		}
//		//��Ӳ�ѯ��Ϣ
//		private void addExecOrder(ArrayList al,string sqlOrder)
//		{
//			ArrayList al1=null;
//			try
//			{
//				al1=this.myExecOrderQuery(sqlOrder);;
//			}
//			catch(Exception ex)
//			{
//				this.Err = ex.Message;
//			}
//			foreach(Object.Order.ExecOrder ExecOrder in al1)
//			{
//				al.Add(ExecOrder);
//			}
//		}
//		/// ��ѯ������Ϣ��select��䣨��where������
//		private string[] ExecOrderQuerySelect(string Type)
//		{
//			#region �ӿ�˵��
//			///Order.ExecOrder.QueryOrder.Select.1
//			///���룺0
//			///������sql.select
//			#endregion
//			string[] s=null;
//			string sql="";
//			if (Type=="1" || Type =="")
//			{
//				if(this.Sql.GetSql("Order.ExecOrder.QueryOrder.Select.1",ref sql)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryOrder.Select.1�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				s[1]=sql;
//			}
//			else if (Type=="2" || Type =="")
//			{
//				if(this.Sql.GetSql("Order.ExecOrder.QueryOrder.Select.2",ref sql)==-1)
//				{
//					this.Err="û���ҵ�Order.ExecOrder.QueryOrder.Select.2�ֶ�!";
//					this.ErrCode="-1";
//					this.WriteErr();
//					return null;
//				}
//				s[2]=sql;
//			}
//			
//			return s;
//		}
//		//˽�к�������ѯҽ����Ϣ
//		private ArrayList myExecOrderQuery(string SQLPatient)
//		{
//			
//			ArrayList al=new ArrayList();
//
//			this.ExecQuery(SQLPatient);
//			try
//			{
//				while (this.Reader.Read())
//				{
//					Object.Order.ExecOrder objOrder = new Neusoft.HISFC.Object.Order.ExecOrder();
//					    
//					#region "������Ϣ"
//					//������Ϣ����  
//					//			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
//					try
//					{
//						objOrder.Order.Patient.ID =this.Reader[1].ToString();
//						objOrder.Order.Patient.Patient.PID.PatientNo = this.Reader[2].ToString(); 
//						objOrder.Order.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString(); 
//						objOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString(); 
//						objOrder.NurseStation.ID = this.Reader[4].ToString();
//						objOrder.InDept.ID=this.Reader[3].ToString();
//
//					}
//					catch(Exception ex)
//					{
//						this.Err="��û��߻�����Ϣ����"+ex.Message;
//						this.WriteErr();
//						return null;
//					}
//					#endregion
//					  
//					if (this.Reader[5].ToString() == "1")
//					{
//						Neusoft.HISFC.Object.Pharmacy.Item objPharmacy= new Neusoft.HISFC.Object.Pharmacy.Item();
//						try
//						{
//							#region "��Ŀ��Ϣ"
//							//ҽ��������Ϣ
//							// ������Ŀ��Ϣ
//							//	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
//							//	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
//							//         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
//							//         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
//							//         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			
//							objPharmacy.ID = this.Reader[6].ToString();
//							objPharmacy.Name = this.Reader[7].ToString();
//							objPharmacy.UserCode = this.Reader[8].ToString();
//							objPharmacy.SpellCode = this.Reader[9].ToString();
//							objPharmacy.SysClass.ID = this.Reader[10].ToString();
//							//objPharmacy.SysClass.Name = this.Reader[11].ToString();
//							objPharmacy.Specs     = this.Reader[12].ToString();
//							try{
//							objPharmacy.BaseDose  = decimal.Parse(this.Reader[13].ToString());}
//							catch{}
//							objPharmacy.DoseUnit = this.Reader[14].ToString();
//							objPharmacy.MinUnit = this.Reader[15].ToString();
//							try{
//							objPharmacy.PackQty = decimal.Parse(this.Reader[16].ToString());}
//							catch{}
//							objPharmacy.DosageForm.ID = this.Reader[17].ToString();
//							objPharmacy.Type.ID    = this.Reader[18].ToString();
//							objPharmacy.Quality.ID = this.Reader[19].ToString();
//							try{
//							objPharmacy.RetailPrice= decimal.Parse(this.Reader[20].ToString());}
//							catch{}		
//							#endregion
//
//							objOrder.Order.Usage.ID  =this.Reader[21].ToString();
//							objOrder.Order.Usage.Name=this.Reader[22].ToString();
//							objOrder.Order.Usage.Memo=this.Reader[23].ToString();
//							objOrder.Order.Frequency.ID  =this.Reader[24].ToString();
//							objOrder.Order.Frequency.Name=this.Reader[25].ToString();
//							try
//							{
//								objOrder.Order.DoseOnce=decimal.Parse(this.Reader[26].ToString());}
//							catch{}
//							try
//							{
//								objOrder.Order.QTY =decimal.Parse(this.Reader[27].ToString());}
//							catch{}
//							objOrder.Order.Unit=this.Reader[28].ToString();
//							try
//							{
//								objOrder.Order.Usetimes=int.Parse(this.Reader[29].ToString());}
//							catch{}
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ����Ŀ��Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						objOrder.Order.Item = objPharmacy;
//
//						#region "ҽ������"
//						// ����ҽ������
//						//		   30ҽ�������� 31ҽ����ˮ��  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
//						//		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��  
//						try
//						{
//							objOrder.ID = this.Reader[0].ToString();
//							objOrder.Order.OrderType.ID = this.Reader[30].ToString();
//							objOrder.Order.OrderType.Name = this.Reader[31].ToString();
//							try
//							{
//								objOrder.Order.OrderType.IsDecompose = System.Convert.ToBoolean(int.Parse(this.Reader[32].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsCharge = System.Convert.ToBoolean(int.Parse(this.Reader[33].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(int.Parse(this.Reader[34].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsPrint = System.Convert.ToBoolean(int.Parse(this.Reader[35].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsConfirm = System.Convert.ToBoolean(int.Parse(this.Reader[36].ToString()));}
//							catch{}
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//						#region "ִ�����"
//						// ����ִ�����
//						//		   37����ҽʦId   38����ҽʦname  39Ҫ��ִ��ʱ��  40����ʱ��     41��������
//						//		   42����ʱ��     43�����˴���    44�����˴���    45���˿��Ҵ��� 46����ʱ��       
//						//		   47ȡҩҩ��     48ִ�п���      49ִ�л�ʿ����  50ִ�п��Ҵ��� 51ִ��ʱ��
//						//         52�ֽ�ʱ��
//						try
//						{						  
//							objOrder.ReciptDoct.ID = this.Reader[37].ToString();
//							objOrder.ReciptDoct.Name = this.Reader[38].ToString();
//							try{objOrder.DateUse = DateTime.Parse(this.Reader[39].ToString());}
//							catch{}
//							try{objOrder.DcExecTime = DateTime.Parse(this.Reader[40].ToString());}
//							catch{}
//							objOrder.Order.ReciptDept.ID = this.Reader[41].ToString();
//							try{objOrder.Order.Date_MO = DateTime.Parse(this.Reader[42].ToString());}
//							catch{}
//							objOrder.DcExecUser.ID = this.Reader[43].ToString();
//							objOrder.ChargeUser.ID = this.Reader[44].ToString();
//							objOrder.ReciptDept.ID = this.Reader[45].ToString();
//							try{objOrder.ChargeTime = DateTime.Parse(this.Reader[46].ToString());}
//							catch{}
//							objOrder.StockDept.ID = this.Reader[47].ToString();
//							objOrder.ExeDept.ID = this.Reader[48].ToString();
//							objOrder.ExecUser.ID = this.Reader[49].ToString();
//							objOrder.ExeDept.ID = this.Reader[50].ToString();
//							try{objOrder.ExecTime = DateTime.Parse(this.Reader[51].ToString());}
//							catch{}
//							objOrder.DateDeco = DateTime.Parse(this.Reader[52].ToString());
//						
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��ִ�������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//						#region "ҽ������"
//						// ����ҽ������
//						//		   52�Ƿ�Ӥ����1��/2��          53�������  	  54������     55��ҩ��� 
//						//		   56�Ƿ��������                 57�Ƿ���Ч      58�ۿ���     59�Ƿ�ִ�� 
//						//		   60��ҩ���                     61�շѱ��      62ҽ��˵��     63��ע 
//						try
//						{
//							try{objOrder.Order.IsBaby = System.Convert.ToBoolean(int.Parse(this.Reader[52].ToString()));}
//							catch{}
//							try{objOrder.Order.BabyNo = this.Reader[53].ToString();}
//							catch{}
//							objOrder.Order.Combo.ID = this.Reader[54].ToString();
//							try{objOrder.Order.Combo.MainDrug = System.Convert.ToBoolean(int.Parse(this.Reader[55].ToString()));}
//							catch{}
//							try{objOrder.Order.IsHaveSubtbl = System.Convert.ToBoolean(int.Parse(this.Reader[56].ToString()));}
//							catch{}
//							try{objOrder.IsInvalid = System.Convert.ToBoolean(this.Reader[57].ToString());}
//							catch{}
//							try{objOrder.Order.IsStock = System.Convert.ToBoolean(int.Parse(this.Reader[58].ToString()));}
//							catch{}
//							try{objOrder.IsExec = System.Convert.ToBoolean(this.Reader[59].ToString());}
//							catch{}
//							try{objOrder.DrugFlag = int.Parse(this.Reader[60].ToString());}
//							catch{}
//							try{objOrder.IsCharge = System.Convert.ToBoolean(this.Reader[61].ToString());}
//							catch{}
//							objOrder.Order.Note = this.Reader[62].ToString();
//							objOrder.Order.Memo = this.Reader[63].ToString();
//
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//					} 	
//					else if (this.Reader[5].ToString() == "2")
//					{
//						Neusoft.HISFC.Object.Fee.Item objAssets=new Neusoft.HISFC.Object.Fee.Item();
//						try
//						{
//							#region "��Ŀ��Ϣ"
//							// ������Ŀ��Ϣ
//							//	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
//							//	       10��Ŀ������ 11��Ŀ�������  12���         13���ۼ�        14�÷�����   
//							//         15�÷�����     16�÷�Ӣ����д  17Ƶ�δ���     18Ƶ������      19ÿ������
//							//         20��Ŀ����     21�Ƽ۵�λ      22ʹ�ô���	
//							objAssets.ID = this.Reader[6].ToString();
//							objAssets.Name = this.Reader[7].ToString();
//							objAssets.UserCode = this.Reader[8].ToString();
//							objAssets.SpellCode = this.Reader[9].ToString();
//							objAssets.SysClass.ID = this.Reader[10].ToString();
//							//objAssets.SysClass.Name = this.Reader[11].ToString();
//							objAssets.Specs     = this.Reader[12].ToString();
//							try{
//							objAssets.Price= decimal.Parse(this.Reader[13].ToString());}
//							catch{}	
//							objAssets.PriceUnit = this.Reader[21].ToString();
//							#endregion
//
//							objOrder.Order.Usage.ID  =this.Reader[14].ToString();
//							objOrder.Order.Usage.Name=this.Reader[15].ToString();
//							objOrder.Order.Usage.Memo=this.Reader[16].ToString();
//							objOrder.Order.Frequency.ID  =this.Reader[17].ToString();
//							objOrder.Order.Frequency.Name=this.Reader[18].ToString();
//							try
//							{
//								objOrder.Order.DoseOnce=decimal.Parse(this.Reader[19].ToString());}
//							catch{}
//							try
//							{
//								objOrder.Order.QTY =decimal.Parse(this.Reader[20].ToString());}
//							catch{}
//							objOrder.Order.Unit=this.Reader[21].ToString();
//							try
//							{
//								objOrder.Order.Usetimes=int.Parse(this.Reader[22].ToString());}
//							catch{}
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ����Ŀ��Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						objOrder.Order.Item = objAssets;	
//						#region "ҽ������"
//						// ����ҽ������
//						//		   23ҽ�������� 24ҽ����ˮ��    25ҽ���Ƿ�ֽ�:1����/2��ʱ     26�Ƿ�Ʒ� 
//						//		   27ҩ���Ƿ���ҩ 28��ӡִ�е�    29�Ƿ���Ҫȷ��    
//						try
//						{
//							objOrder.Order.ID = this.Reader[0].ToString();
//							objOrder.Order.OrderType.ID = this.Reader[23].ToString();
//							objOrder.Order.OrderType.Name = this.Reader[24].ToString();
//							try
//							{
//								objOrder.Order.OrderType.IsDecompose = System.Convert.ToBoolean(int.Parse(this.Reader[25].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsCharge = System.Convert.ToBoolean(int.Parse(this.Reader[26].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(int.Parse(this.Reader[27].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsPrint = System.Convert.ToBoolean(int.Parse(this.Reader[28].ToString()));}
//							catch{}
//							try
//							{
//								objOrder.Order.OrderType.IsConfirm = System.Convert.ToBoolean(int.Parse(this.Reader[29].ToString()));}
//							catch{}
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//						#region "ִ�����"
//						// ����ִ�����
//						//		   30����ҽʦId   31����ҽʦname  32Ҫ��ִ��ʱ��  33����ʱ��     34��������
//						//		   35����ʱ��     36�����˴���    37�����˴���    38���˿��Ҵ��� 39����ʱ��       
//						//		   40ȡҩҩ��     41ִ�п���      42ִ�л�ʿ����  43ִ�п��Ҵ��� 44ִ��ʱ��
//						//         45�ֽ�ʱ��     46ִ�п�������
//						try
//						{						  
//							objOrder.ReciptDoct.ID = this.Reader[30].ToString();
//							objOrder.ReciptDoct.Name = this.Reader[31].ToString();
//							try{objOrder.DateUse = DateTime.Parse(this.Reader[32].ToString());}
//							catch{}
//							try{objOrder.DcExecTime = DateTime.Parse(this.Reader[33].ToString());}
//							catch{}
//							objOrder.Order.ReciptDept.ID = this.Reader[34].ToString();
//							try{objOrder.Order.Date_MO = DateTime.Parse(this.Reader[35].ToString());}
//							catch{}
//							objOrder.DcExecUser.ID = this.Reader[36].ToString();
//							objOrder.ChargeUser.ID = this.Reader[37].ToString();
//							objOrder.ReciptDept.ID = this.Reader[38].ToString();
//							try{objOrder.ChargeTime = DateTime.Parse(this.Reader[39].ToString());}
//							catch{}
//							objOrder.StockDept.ID = this.Reader[40].ToString();
//							objOrder.ExeDept.ID = this.Reader[41].ToString();
//							objOrder.ExecUser.ID = this.Reader[42].ToString();
//							objOrder.ExeDept.ID = this.Reader[43].ToString();
//							try{objOrder.ExecTime = DateTime.Parse(this.Reader[44].ToString());}
//							catch{}
//							objOrder.DateDeco = DateTime.Parse(this.Reader[45].ToString());
//							objOrder.ExeDept.Name = this.Reader[46].ToString();
//						
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��ִ�������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//						#region "ҽ������"
//						// ����ҽ������
//						//		   47�Ƿ�Ӥ����1��/2��          48�������  	  49������     50������ 
//						//		   51�Ƿ񸽲�                     52�Ƿ��������  53�Ƿ���Ч     54�Ƿ�ִ�� 
//						//		   55�շѱ��     56�Ӽ����      57��鲿λ����  58ҽ��˵��     59��ע  
//						try
//						{
//							try{objOrder.Order.IsBaby = System.Convert.ToBoolean(int.Parse(this.Reader[47].ToString()));}
//							catch{}
//							try{objOrder.Order.BabyNo = this.Reader[48].ToString();}
//							catch{}
//							objOrder.Order.Combo.ID = this.Reader[49].ToString();
//							try{objOrder.Order.Combo.MainDrug = System.Convert.ToBoolean(int.Parse(this.Reader[50].ToString()));}
//							catch{}
//							try{objOrder.Order.IsSubtbl = System.Convert.ToBoolean(int.Parse(this.Reader[51].ToString()));}
//							catch{}
//							try{objOrder.Order.IsHaveSubtbl = System.Convert.ToBoolean(int.Parse(this.Reader[52].ToString()));}
//							catch{}
//							try{objOrder.IsInvalid = System.Convert.ToBoolean(this.Reader[53].ToString());}
//							catch{}
//							try{objOrder.IsExec = System.Convert.ToBoolean(this.Reader[54].ToString());}
//							catch{}
//							try{objOrder.IsCharge = System.Convert.ToBoolean(this.Reader[55].ToString());}
//							catch{}
//							try{objOrder.Order.IsEmergency = System.Convert.ToBoolean(int.Parse(this.Reader[56].ToString()));}
//							catch{}
//							objOrder.Order.CheckPartRecord = this.Reader[57].ToString();
//
//							objOrder.Order.Note = this.Reader[58].ToString();
//							objOrder.Order.Memo = this.Reader[59].ToString();
//
//						}
//						catch(Exception ex)
//						{
//							this.Err="���ҽ��������Ϣ����"+ex.Message;
//							this.WriteErr();
//							return null;
//						}
//						#endregion
//					}
//					al.Add(objOrder);
//				}
//			}
//			catch(Exception ex)
//			{
//				this.Err="���ҽ����Ϣ����"+ex.Message;
//				this.ErrCode="-1";
//				this.WriteErr();
//				return null;
//			}
//			this.Reader.Close();
//			return al;
//		}
//		#endregion 
//		
//		
//		/// <summary>
//		/// �ж�¼�����ݵĺϷ���
//		/// </summary>
//		/// <param name="Order"></param>
//		/// <returns></returns>
//		private int CheckOrder(Object.Order.Order Order)
//		{
//
//			//�ж�ҩƷ/��ҩƷ
//			if (Order.Item.GetType().ToString()== "Neusoft.HISFC.Object.Pharmacy.Item")
//			{
//				Neusoft.HISFC.Object.Pharmacy.Item objPharmacy ;
//				objPharmacy = (Neusoft.HISFC.Object.Pharmacy.Item)Order.Item;
//
//			}
//			else if (Order.Item.GetType().ToString()== "Neusoft.HISFC.Object.Fee.Item")
//			{
//				Neusoft.HISFC.Object.Fee.Item objAssets;
//				objAssets = (Neusoft.HISFC.Object.Fee.Item)Order.Item;
//			}
//			return 0;
//		}

	}
}
