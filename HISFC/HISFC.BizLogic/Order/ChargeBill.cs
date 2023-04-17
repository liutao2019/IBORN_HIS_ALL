using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// ChargeBill �����շѵ�
	/// </summary>
	public class ChargeBill:FS.FrameWork.Management.Database
	{
		public ChargeBill()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		#region ��ɾ��
		/// <summary>
		/// ����һ���շѵ�
		/// </summary>
		/// <param name="bill"></param>
		/// <returns></returns>
		public int InsertChargeBill( FS.HISFC.Models.Fee.Inpatient.ChargeBill bill )
		{
			#region ����
//	    		'{0}',   --סԺ��
//            '{1}',   --�Ƿ�Ӥ����ҩ 0 ���� 1 ��
//            '{2}',   --סԺ����
//            '{3}',   --��ʿվ
//            '{4}',   --����ҽ��
//            '{5}',   --��������
//            '{6}',   --ҩƷ��־,1ҩƷ/0��ҩ
//            '{7}',   --��Ŀ����
//            '{8}',   --��Ŀ����
//            '{9}',   --���
//            '{10}',   --���ۼ�
//            '{11}',   --����
//            '{12}',   --����
//            '{13}',   --��λ
//            '{14}',   --ִ�п���
//            '{15}',   --ȡҩҩ��
//            '{16}',   --ҽ����
//            '{17}',   --ҽ��ִ�к�
//            '{18}',   --���ݺ�-s
//            '{19}',   --��ӡ��־ 0δ��/1�Ѵ�ӡ
//            '{20}',   --¼����
			#endregion
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Insert.1",ref sql)==-1) 
			{
				this.Err = this.Sql.Err;
				return -1;
			}

			try
			{
				sql=string.Format(sql,bill.InpatientNO,FS.FrameWork.Function.NConvert.ToInt32(bill.IsBaby),bill.InDept,bill.NurseStation.ID,
					bill.Doctor.ID,bill.ReciptDept.ID,FS.FrameWork.Function.NConvert.ToInt32(bill.IsPharmacy),bill.ID,
					bill.Name,bill.Specs,bill.Price.ToString(),bill.Qty.ToString(),
					bill.HerbalQty.ToString(),bill.PriceUnit,bill.ExeDept.ID,bill.StockDept.ID,
					bill.OrderID,bill.ExecOrderID,bill.BillNO,FS.FrameWork.Function.NConvert.ToInt32(bill.IsPrint),
					bill.Oper.ID,bill.TotCost.ToString(),bill.Combo.ID,bill.OutputType);
				return this.ExecNoQuery(sql);				
			}
			catch(Exception e)
			{
				this.Err="�����շѵ������![Order.ChargeBill.Insert.1]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}		
		}
		
		/// <summary>
		/// ���±��
		/// ���´�ӡ��ǣ������շѱ��
		/// </summary>
		/// <param name="p"></param>
		/// <param name="bill"></param>
		/// <returns></returns>
		public int UpdateChargeBill( EnumUpdateType p, FS.HISFC.Models.Fee.Inpatient.ChargeBill bill )
		{
			if(p==EnumUpdateType.Print)
			{
				return UpdatePrintFlag(bill);
			}
			else if(p==EnumUpdateType.Charge)
			{
				return UpdateChargeFlag(bill);
			}
			return 0;
		}
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="billID"></param>
		/// <returns></returns>
		public int DeleteChargeBill(string billID)
		{
			#region sql
//			DELETE 
//			FROM met_nui_chargebill   --�����շѵ�
//			WHERE parent_code='[��������]'
//			AND current_code='[��������]'
//			AND bill_id='{0}'
			#endregion

			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Delete.1", ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}

			try
			{
				sql = string.Format( sql, billID);
				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ���շѵ������![Order.ChargeBill.Delete.1]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		#endregion
		
		#region ����
		[Obsolete("��UpdatePrintFlag������",true)]
		private int UpdatePrint(FS.HISFC.Models.Fee.Inpatient.ChargeBill bill)
		{
			return this.UpdatePrintFlag(bill);
		}
		[Obsolete("��UpdateChargeFlag������",true)]
		private int UpdateCharge(FS.HISFC.Models.Fee.Inpatient.ChargeBill bill)
		{
			return this.UpdateChargeFlag(bill);
		}
		[Obsolete("��QueryChargeBillNotPrinted������",true)]
		public ArrayList Query(string InpatientNo,bool IsPharmacy)
		{
			return this.QueryChargeBillNotPrinted(InpatientNo,IsPharmacy);
		}
		[Obsolete("��QueryByNurseStation������",true)]
		public ArrayList QueryForNurseStation(string inpatientNo, bool isPharmacy)
		{
			return this.QueryChargeBillByNurseStation(inpatientNo,isPharmacy);
		}
		[Obsolete("��QueryForNurseStation������",true)]
		public ArrayList QueryForNurseStation(string InpatientNo,bool IsPharmacy,DateTime begin,DateTime end)
		{
			return this.QueryChargeBillByNurseStation(InpatientNo,IsPharmacy, begin, end);
		}
		/// <summary>
		/// ��ѯ���߳�����ʾ�������־.
		/// </summary>
		/// <param name="isPharmacy"></param>
		/// <param name="itemCode"></param>
		/// <returns></returns>
		[Obsolete("Ӧ�ò��ðɣ�tmdû�ҵ�sql���",false)]
		public string QuerySpFlag(bool isPharmacy, string itemCode)
		{
			string sql = "";
			string spFlag = "";
			if(isPharmacy)//ҩƷ
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.QuerySpFlag.1", ref sql) == -1)
				{
					return "";
				}
			}
			else //��ҩƷ
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.QuerySpFlag.2", ref sql) == -1)
				{
					return "";
				}
			}

			sql = string.Format(sql, itemCode);

			if(this.ExecQuery(sql) == -1)
			{
				return "";
			}

			try
			{

				while(Reader.Read())
				{
					spFlag = Reader[0].ToString();
				}
				Reader.Close();
			}
			catch(Exception ex)
			{
				if(Reader.IsClosed == false)
				{
					Reader.Close();
				}
				this.Err = "��ѯ������ʾ��־����!" + ex.Message;
				return "";
			}
			return spFlag;
		}
		#endregion 

		#region ��־
		/// <summary>
		/// ���´�ӡ��־
		/// </summary>
		/// <param name="bill"></param>
		/// <returns>-1 ���� 0 û�и��£�û�в鵽��¼�� >0 ���µ���Ϣ</returns>
		public int UpdatePrintFlag(FS.HISFC.Models.Fee.Inpatient.ChargeBill bill)
		{
			#region sql
			//			UPDATE met_nui_chargebill   --�����շѵ�
			//   SET bill_no='{1}',   --���ݺ�
			//       print_flag='{2}',   --��ӡ��־ 0δ��/1�Ѵ�ӡ
			//       print_code='{3}',   --��ӡ��
			//       print_date=sysdate    --��ӡʱ��
			// WHERE parent_code='[��������]'
			//   AND current_code='[��������]'
			//   AND bill_id='{0}'
			#endregion
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Update.1",ref sql)==-1)
			{
				this.Err = this.Sql.Err;

				return -1;
			}
			try
			{
				sql=string.Format(sql,bill.ID,bill.BillNO,FS.FrameWork.Function.NConvert.ToInt32(bill.IsPrint),bill.PrintOper.ID);

				return  this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="�����շѵ������![Order.ChargeBill.Update.1]"+e.Message;
				this.ErrCode=e.Message;

				return -1;
			}		
		}
		/// <summary>
		/// �����շѱ�־
		/// </summary>
		/// <param name="bill"></param>
		/// <returns>-1 ���� 0 û�и��£�û�в鵽��¼�� >0 ���µ���Ϣ</returns>
		public int UpdateChargeFlag(FS.HISFC.Models.Fee.Inpatient.ChargeBill bill)
		{
			#region sql
			//			UPDATE met_nui_chargebill   --�����շѵ�
			//			SET charge_flag='{1}',   --�Ƿ��շ� 0δ/1��
			//				charge_code='{2}',   --�շ���
			//				charge_date=sysdate    --�շ�ʱ��
			//			WHERE parent_code='[��������]'
			//			AND current_code='[��������]'
			//			AND bill_id='{0}'
			#endregion
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Update.2",ref sql) == -1)
			{
				this.Err = this.Sql.Err ;
				return -1;
			}

			try
			{
				sql=string.Format(sql,bill.ID,FS.FrameWork.Function.NConvert.ToInt32(bill.IsCharge),bill.Oper.ID,
					bill.ReciptNO,bill.SequenceNO);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="�����շѵ������![Order.ChargeBill.Update.2]"+e.Message;
				this.ErrCode=e.Message;

				return -1;
			}
		}
		#endregion

		#region ��ѯ
		/// <summary>
		/// �����߲�ѯδ��ӡ�շѵ�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="isPharmacy"></param>
		/// <returns>null ����</returns>
		public ArrayList QueryChargeBillNotPrinted( string inpatientNo, bool isPharmacy )
		{
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Query.1",ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
			
			sql = string.Format(sql,inpatientNo,FS.FrameWork.Function.NConvert.ToInt32(isPharmacy));

			return myQueryChargeBill(sql);
		}
		/// <summary>
		/// ��ʿռ������ѯ,����ִ�е�, ����,ҽ����ˮ�ţ�comb_no��useTime
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="isPharmacy"></param>
		/// <returns>null ����</returns>
		public ArrayList QueryChargeBillByNurseStation(string inpatientNo, bool isPharmacy)
		{
			string sql = "";
			if(isPharmacy)
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.Query.NurseStation.1",ref sql)==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
			}
			else
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.Query.NurseStation.3",ref sql)==-1)
				{
					this.Err = this.Sql.Err;
					return null;
				}
			}
			
			sql=string.Format(sql,inpatientNo,FS.FrameWork.Function.NConvert.ToInt32(isPharmacy));

			return myQueryChargeBillByNurseStation(sql);
		}
		/// <summary>
		/// �����ߡ�ʱ��β�ѯ�Ѵ�ӡ�շѵ�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public ArrayList QueryChargeBillByNurseStation( string inpatientNo, bool isPharmacy, DateTime beginTime, DateTime endTime )
		{
			string sql = "";
			if(isPharmacy)
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.Query.NurseStation.2",ref sql)==-1)
				{
					this.Err = this.Sql.Err;

					return null;
				}
			}
			else
			{
				if(this.Sql.GetCommonSql("Order.ChargeBill.Query.NurseStation.4",ref sql)==-1)
				{
					this.Err = this.Sql.Err;

					return null;
				}
			}
			
			sql = string.Format(sql,inpatientNo,FS.FrameWork.Function.NConvert.ToInt32(isPharmacy),
				beginTime.ToString(),endTime.ToString());

			return myQueryChargeBillByNurseStation(sql);
		}
		/// <summary>
		/// �����ߡ��Ƿ��ӡ���Ƿ��շѲ�ѯ�շѵ�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="isPrint"></param>
		/// <param name="isCharge"></param>
		/// <returns></returns>
		public ArrayList QueryChargeBill( string inpatientNo, bool isPrint, bool isCharge )
		{
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Query.2",ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
			
			sql = string.Format(sql,inpatientNo,FS.FrameWork.Function.NConvert.ToInt32(isPrint),FS.FrameWork.Function.NConvert.ToInt32(isCharge));

			return myQueryChargeBill(sql);
		}
		/// <summary>
		/// �����ߡ�ʱ��β�ѯ�Ѵ�ӡ�շѵ�
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public ArrayList QueryChargeBill( string inpatientNo, bool isPharmacy, DateTime beginTime, DateTime endTime )
		{
			string sql = "";
			if(this.Sql.GetCommonSql("Order.ChargeBill.Query.3",ref sql)==-1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
			
			sql = string.Format(sql,inpatientNo,FS.FrameWork.Function.NConvert.ToInt32(isPharmacy),
				beginTime.ToString(),endTime.ToString());

			return myQueryChargeBill(sql);
		}
		
		
		#endregion

		/// <summary>
		/// ������ѯ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList myQueryChargeBill( string sql )
		{
			ArrayList al=new ArrayList();
			try
			{
				if(this.ExecQuery(sql) == -1) return null;
				while(Reader.Read())
				{
					FS.HISFC.Models.Fee.Inpatient.ChargeBill bill=new FS.HISFC.Models.Fee.Inpatient.ChargeBill();
					bill.ID=Reader[2].ToString();//��ˮ��
					bill.InpatientNO = Reader[3].ToString();//סԺ��
					bill.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(Reader[4].ToString());
					bill.InDept.ID = Reader[5].ToString();//סԺ����
					bill.NurseStation.ID = Reader[6].ToString();//סԺ����
					bill.Doctor.ID = Reader[7].ToString();
					bill.ReciptDept.ID = Reader[8].ToString();//��������
					bill.IsPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(Reader[9].ToString());
					bill.ID = Reader[10].ToString();//��Ŀ����
					bill.Name = Reader[11].ToString();//��Ŀ����
					bill.Specs = Reader[12].ToString();//���
                    bill.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());//�۸�
                    bill.Qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());//����
					bill.HerbalQty =  FrameWork.Function.NConvert.ToInt32(Reader[15].ToString());//��ҩ����
					bill.PriceUnit=Reader[16].ToString();					//�۸�λ
					bill.ExeDept.ID=Reader[17].ToString();					//ִ�п���
					bill.StockDept.ID =Reader[18].ToString();				//ȡҩҩ��
					bill.OrderID =Reader[19].ToString();					//ҽ����ˮ��
					bill.ExecOrderID =Reader[20].ToString();				//ִ����ˮ��
					bill.BillNO=Reader[21].ToString();						//���ݺ�
					bill.IsPrint=FS.FrameWork.Function.NConvert.ToBoolean(Reader[22].ToString());//�Ƿ��ӡ
					bill.Oper.ID=Reader[23].ToString();
					bill.Oper.OperTime = FrameWork.Function.NConvert.ToDateTime(Reader[24].ToString());//����ʱ��
					bill.PrintOper.ID = Reader[25].ToString();						//��ӡ��
					bill.PrintOper.OperTime = FrameWork.Function.NConvert.ToDateTime(Reader[26].ToString()); //��ӡʱ��
					bill.IsCharge=FS.FrameWork.Function.NConvert.ToBoolean(Reader[27].ToString());
					bill.Oper.ID = Reader[28].ToString();					//�շ���
					bill.Oper.OperTime = FrameWork.Function.NConvert.ToDateTime(Reader[29].ToString()); //�շ�ʱ��
					bill.TotCost = FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());	//�ܶ�
					bill.Combo.ID =Reader[31].ToString();						//��Ϻ�
					bill.OutputType = Reader[32].ToString();				//��������
					al.Add(bill);
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				if(Reader.IsClosed == false) Reader.Close();
				this.Err = "��ѯ�շѵ������!"+e.Message;
				this.ErrCode=e.Message;
				this.WriteErr();
				return null;
			}
			return al;
		}
		/// <summary>
		/// ������ѯΪ��ʿռ��ѯ����
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList myQueryChargeBillByNurseStation( string sql )
		{
			ArrayList al=new ArrayList();
			try
			{
				if(this.ExecQuery(sql)==-1)return null;
				while(Reader.Read())
				{
					FS.HISFC.Models.Fee.Inpatient.ChargeBill bill=new FS.HISFC.Models.Fee.Inpatient.ChargeBill();
					bill.ID = Reader[2].ToString();//��ˮ��
					bill.InpatientNO = Reader[3].ToString();//סԺ��
					bill.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(Reader[4].ToString());
					bill.InDept.ID = Reader[5].ToString();//סԺ����
					bill.NurseStation.ID =Reader[6].ToString();//סԺ����
					bill.Doctor.ID = Reader[7].ToString();
					bill.ReciptDept.ID =Reader[8].ToString();//��������
					bill.IsPharmacy=FS.FrameWork.Function.NConvert.ToBoolean(Reader[9].ToString());
					bill.ID=Reader[10].ToString();//��Ŀ����
					bill.Name=Reader[11].ToString();//��Ŀ����
					bill.Specs=Reader[12].ToString();//���
					bill.Price = FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());//�۸�
					bill.Qty = FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());//����
					bill.HerbalQty =FrameWork.Function.NConvert.ToInt32(Reader[15].ToString());//��ҩ����
					bill.PriceUnit = Reader[16].ToString();//�۸�λ
					bill.ExeDept.ID = Reader[17].ToString();//ִ�п���
					bill.StockDept.ID = Reader[18].ToString();//ȡҩҩ��
					bill.OrderID = Reader[19].ToString();//ҽ����ˮ��
					bill.ExecOrderID = Reader[20].ToString();//ִ����ˮ��
					bill.BillNO = Reader[21].ToString();//���ݺ�
					bill.IsPrint = FS.FrameWork.Function.NConvert.ToBoolean(Reader[22].ToString());//��ӡ���
					bill.Oper.ID =Reader[23].ToString();//������
					bill.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[24].ToString());//����ʱ��
					bill.PrintOper.ID =Reader[25].ToString();//��ӡ��
					bill.PrintOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[26].ToString());//��ӡʱ��
					bill.IsCharge=FS.FrameWork.Function.NConvert.ToBoolean(Reader[27].ToString());//�Ƿ��շѱ��
					bill.Oper.ID =Reader[28].ToString();//�շ���
					bill.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[29].ToString());//�շ�ʱ��
					bill.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[30].ToString());//�ܶ�
					bill.Combo.ID = Reader[31].ToString();//��Ϻ�
					bill.UseTime  = FrameWork.Function.NConvert.ToDateTime(Reader[32].ToString());//ʹ��ʱ��
					if(Reader.IsDBNull(33) == false)
					{
						bill.Frequency.ID = Reader[33].ToString();//Ƶ��
					}
					if(Reader.IsDBNull(34) == false)
					{
						bill.Memo = Reader[34].ToString();//������ʾ��־
					}
					bill.OutputType = Reader[35].ToString();//��������

					al.Add(bill);
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				if(Reader.IsClosed == false) Reader.Close();
				this.Err="��ѯ�շѵ������!"+e.Message;
				this.ErrCode = e.Message;
				this.WriteErr();
				return null;
			}
			return al;
		}
	}


	public enum EnumUpdateType {

		/// <summary>
		/// ���´�ӡ
		/// </summary>
		Print,
		/// <summary>
		/// ȷ���շ�
		/// </summary>
		Charge
	}
}
