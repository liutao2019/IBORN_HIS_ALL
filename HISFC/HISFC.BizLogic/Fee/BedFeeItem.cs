using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// BedFeeItem<br></br>
	/// [��������: ��λ��Ϣҵ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-26]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class BedFeeItem : FS.FrameWork.Management.Database
	{
		
		#region ˽�к���
		
		/// <summary>
		/// ͨ��ʵ�����ַ���������
		/// </summary>
		/// <param name="bedFeeItem">��λ��ʵ��</param>
		/// <returns></returns>
		private string[] GetBedFeeItemParms(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
		{
            string[] parms = {   
                                 bedFeeItem.PrimaryKey,
                                 bedFeeItem.FeeGradeCode,
								 bedFeeItem.ID,
								 bedFeeItem.Name,
								 bedFeeItem.Qty.ToString(),
								 bedFeeItem.SortID.ToString(),
								 NConvert.ToInt32(bedFeeItem.IsBabyRelation).ToString(),
								 NConvert.ToInt32(bedFeeItem.IsTimeRelation).ToString(),
								 bedFeeItem.BeginTime.ToString(),
								 bedFeeItem.EndTime.ToString(),
								 ((int)bedFeeItem.ValidState).ToString(),
								 this.Operator.ID,
                                 bedFeeItem.ExtendFlag,
                                 NConvert.ToInt32(bedFeeItem.IsOutFeeFlag).ToString()
							 };

			return parms;
		}

        /// <summary>
        /// ͨ��ʵ�����ַ���������
        /// </summary>
        /// <param name="bedFeeItem">��λ��ʵ��</param>
        /// <returns></returns>
        private string[] GetBedFeeItemParmsForPatient(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {
            string[] parms = {   
                                 bedFeeItem.PrimaryKey,
                                 bedFeeItem.FeeGradeCode,
								 bedFeeItem.ID,
								 bedFeeItem.Name,
								 bedFeeItem.Qty.ToString(),
								 bedFeeItem.SortID.ToString(),
								 NConvert.ToInt32(bedFeeItem.IsBabyRelation).ToString(),
								 NConvert.ToInt32(bedFeeItem.IsTimeRelation).ToString(),
								 bedFeeItem.BeginTime.ToString(),
								 bedFeeItem.EndTime.ToString(),
								 ((int)bedFeeItem.ValidState).ToString(),
								 this.Operator.ID,
                                 bedFeeItem.ExtendFlag,
                                 NConvert.ToInt32(bedFeeItem.IsOutFeeFlag).ToString(),
                                 bedFeeItem.Price.ToString(),
                                 bedFeeItem.PatientID,
                                 bedFeeItem.BedNO
							 };

            return parms;
        }
		#endregion

		#region ���к���
		
		/// <summary>
		/// ͨ����Ŀ�����øô�λ��Ϣ����Ч�Ա�ʶ(����(1) ͣ��(0) ����(2))
		/// </summary>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>�ɹ�������Ч�б�ʶ ���󷵻�"-1"</returns>
		public string GetValidStateByItemCode(string itemCode)
		{
			string validState = string.Empty; //��Ч״̬��־ 1 ���� 0 ͣ�� 2 ����
			string sql = string.Empty; //��ѯ��Ч��ǵ�SQL���

			if (this.Sql.GetCommonSql("Fee.BedFeeItem.IsDisuser",ref sql)==-1)
			{
				this.Err = "û���ҵ�����Ϊ: Fee.BedFeeItem.IsDisuser��SQL���";
				
				return "-1";
			}

			return this.ExecSqlReturnOne(sql, itemCode);
		}
		
		/// <summary>
		/// ���봲λ��Ϣ��һ����¼(fin_com_bedfeegrade)
		/// </summary>
		/// <param name="bedFeeItem">��λ��Ϣʵ��</param>
		/// <returns>�ɹ� 1 ʧ�� -1 û��ɾ������¼ 0</returns>
		public int InsertBedFeeItem(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
		{
			string sql = string.Empty; //����fin_com_bedfeegrade���SQL���

			if (this.Sql.GetCommonSql("Fee.BedFeeItem.InsertBedFeeItem", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ: Fee.BedFeeItem.InsertBedFeeItem ��SQL���";
				
				return -1;
			}

			return this.ExecNoQuery(sql, this.GetBedFeeItemParms(bedFeeItem));
		}

		/// <summary>
		/// ���´�λ��Ϣһ����¼
		/// </summary>
		/// <param name="bedFeeItem">��λ��Ϣʵ��</param>
		/// <returns>�ɹ� 1 ʧ�� -1 û��ɾ������¼ 0</returns>
		public int UpdateBedFeeItem(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
		{
		
			string sql = string.Empty; //����fin_com_bedfeegrade���SQL���
			
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.UpdateBedFeeItem", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ: Fee.BedFeeItem.UpdateBedFeeItem ��SQL���";
				
				return -1;
			}
			
			return this.ExecNoQuery(sql, this.GetBedFeeItemParms(bedFeeItem));
		}
		
		/// <summary>
		/// ͨ����Ŀ����ɾ��һ����Ŀ
		/// </summary>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>�ɹ� 1 ʧ�� -1 û��ɾ������¼ 0</returns>
		public int DeleteByItemCode(string itemCode)
		{
			string sql = string.Empty;

			if (this.Sql.GetCommonSql("Fee.BedFeeItem.DeleteBedFeeItem", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ: Fee.BedFeeItem.DeleteBedFeeItem ��SQL���";
				
				return -1;
			}

			return this.ExecNoQuery(sql, itemCode);
		}

		/// <summary>
		/// ���ݴ��źŻ�ȡ��λ�ȼ�
		/// </summary>
		/// <param name="bedNO">����</param>
		/// <returns>�ɹ�:��λ��Ϣʵ�� ʧ�� null</returns>
		public FS.HISFC.Models.Base.Bed GetBedGradeByBedNO(string bedNO)
		{
			Bed bed = null; //��λʵ��
			string sql = string.Empty;//��ô�λ�Ǽ�SQL���
			
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.GetBedGradeByBedNo", ref sql) == -1) 
			{		
				this.Err = "û���ҵ�����Ϊ:Fee.BedFeeItem.GetBedGradeByBedNo��SQL���";

				return null;
			}			
			
			if (this.ExecQuery(sql, bedNO) == -1)
			{
				return null;
			}
			
			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read())
				{
					bed = new Bed();

					bed.ID = this.Reader[0].ToString();
					bed.BedGrade.ID = this.Reader[1].ToString();
					bed.InpatientNO = this.Reader[2].ToString();				
				}//ѭ������

				this.Reader.Close();
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
			
			return bed;
		}
		
		/// <summary>
		/// ����סԺ��ˮ�Ż�ȡ��λ�ȼ�
		/// </summary>
		/// <param name="inpatientNO">סԺ��ˮ��</param>
		/// <returns>�ɹ�: ��λ�ȼ���Ϣ����, ʧ�� null, û���ҵ�����:Ԫ��Ϊ0��ArrayList</returns>
		public ArrayList QueryBedGradeByInpatientNO(string inpatientNO)
		{
			ArrayList beds = new ArrayList(); //��λ��Ϣ����
			string sql = string.Empty; //����סԺ��ˮ�Ż�ô�λ�ȼ����ϵ�SQL���

			if (this.Sql.GetCommonSql("Fee.BedFeeItem.GetBedGradeByInpatienNo", ref sql) == -1) 
			{				
				this.Err = "û���ҵ�����Ϊ:Fee.BedFeeItem.GetBedGradeByInpatienNo��SQL���";

				return null;
			}			
			
			if(this.ExecQuery(sql, inpatientNO) == -1)
			{
				return null;
			}
			
			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read())
				{
					Bed bed  = new Bed();

					bed.ID = this.Reader[0].ToString();
					bed.BedGrade.ID = this.Reader[1].ToString();
					bed.InpatientNO = this.Reader[2].ToString();				
					
					beds.Add(bed);
				}//ѭ������

				this.Reader.Close();
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				beds = null;

				return null;
			}

			return beds;
		}

		/// <summary>
		/// ���ݴ�λ�ȼ�������ȡBedFeeItem��Ϣ
		/// </summary>
		/// <param name="minFeeCode">��С���ñ���</param>
		/// <returns>�ɹ�: ��λ��Ϣ����, ʧ�� null, û���ҵ�����:Ԫ��Ϊ0��ArrayList</returns>
		public ArrayList QueryBedFeeItemByMinFeeCode(string minFeeCode)
		{

			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.BedFeeItem.SelectByFeeCode", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.BedFeeItem.SelectByFeeCode��SQL���";

				return null;
			}

			if(this.ExecQuery(sql, minFeeCode) == -1)
			{
				return null;
			}
			
			try
			{
				ArrayList bedFeeItems = new ArrayList();
				
				//ѭ����ȡ����
				while (this.Reader.Read())
				{
					FS.HISFC.Models.Fee.BedFeeItem bedFeeItem = new FS.HISFC.Models.Fee.BedFeeItem();

					bedFeeItem.PrimaryKey = this.Reader[0].ToString();
					bedFeeItem.FeeGradeCode = this.Reader[1].ToString();
					bedFeeItem.ID = this.Reader[2].ToString();
					bedFeeItem.Name = this.Reader[3].ToString();
					bedFeeItem.Qty = NConvert.ToDecimal(this.Reader[4].ToString());
					bedFeeItem.SortID = NConvert.ToInt32(this.Reader[5].ToString());
					bedFeeItem.IsBabyRelation = NConvert.ToBoolean(this.Reader[6].ToString());
					bedFeeItem.IsTimeRelation = NConvert.ToBoolean(this.Reader[7].ToString());
					bedFeeItem.BeginTime = NConvert.ToDateTime(this.Reader[8].ToString());
					bedFeeItem.EndTime = NConvert.ToDateTime(this.Reader[9].ToString());
					bedFeeItem.ValidState = (EnumValidState)NConvert.ToInt32(this.Reader[10]);
					bedFeeItem.ExtendFlag = this.Reader[13].ToString();
                    if (this.Reader.FieldCount>14)
                    {
                        bedFeeItem.IsOutFeeFlag = NConvert.ToBoolean(this.Reader[14].ToString());
                    }

					bedFeeItems.Add(bedFeeItem);
				}
				
				this.Reader.Close();

				return bedFeeItems;
			}
			catch (Exception e)
			{
				this.Err = e.Message;

				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
		}

        /// <summary>
        /// ��ȡ���˵Ĺ̶�������Ϣ
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="bedNO">��λ���</param>
        /// <param name="bedGradeID">��λ�ȼ�key</param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.BedFeeItem QueryBedGradeForPatient(string inpatientNO, string bedNO, string bedGradeID)
        {
            string sql = string.Empty; //��ѯSQL���

            if (this.Sql.GetCommonSql("Fee.BedFeeItem.SelectForPatientByInpatientNOAndBedGrade", ref sql) == -1)
            {
                sql = @"
					 SELECT PK_ID,					--���к�
						FEE_GRADE_CODE,                         --��λ�ȼ�����
						ITEM_CODE,                              --��Ŀ����
						ITEM_NAME,                              --������Ŀ����
						ITEM_NUMBER,                            --������Ŀ����
						SORT_ID,                                --˳���
						BABY_FLAG,                              --Ӥ����־ 0 �޹� 1 �й�
						DATE_FLAG,                              --ʱ���ʶ 1 ��ʱ����� 0 ��ʱ���޹�
						BEGIN_DATE,                             --��ʼʱ��
						END_DATE,                               --��������
						VALID_STATE,                            --��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
						OPER_CODE,                              --����Ա
						OPER_DATE,                              --��������
						EXT_FLAG,                                --��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
						OUT_FEEFLAG,
                        ITEM_PRICE,--��Ŀ�۸�
                        INPATIENT_NO,--סԺ��ˮ��
                        BED_NO--��λ��
					FROM 	fin_ipb_bedfeegrade
                    WHERE     INPATIENT_NO = '{0}'
                    AND BED_NO='{1}'
                    AND PK_ID='{2}'
                                ";
            }

            if (this.ExecQuery(sql, inpatientNO, bedNO,bedGradeID) == -1)
            {
                return null;
            }
            FS.HISFC.Models.Fee.BedFeeItem bedFeeItem = null;
            try
            {
                //ѭ����ȡ����
                if (this.Reader.Read())
                {
                    bedFeeItem = new FS.HISFC.Models.Fee.BedFeeItem();

                    bedFeeItem.PrimaryKey = this.Reader[0].ToString();
                    bedFeeItem.FeeGradeCode = this.Reader[1].ToString();
                    bedFeeItem.ID = this.Reader[2].ToString();
                    bedFeeItem.Name = this.Reader[3].ToString();
                    bedFeeItem.Qty = NConvert.ToDecimal(this.Reader[4].ToString());
                    bedFeeItem.SortID = NConvert.ToInt32(this.Reader[5].ToString());
                    bedFeeItem.IsBabyRelation = NConvert.ToBoolean(this.Reader[6].ToString());
                    bedFeeItem.IsTimeRelation = NConvert.ToBoolean(this.Reader[7].ToString());
                    bedFeeItem.BeginTime = NConvert.ToDateTime(this.Reader[8].ToString());
                    bedFeeItem.EndTime = NConvert.ToDateTime(this.Reader[9].ToString());
                    bedFeeItem.ValidState = (EnumValidState)NConvert.ToInt32(this.Reader[10]);
                    bedFeeItem.ExtendFlag = this.Reader[13].ToString();
                    bedFeeItem.IsOutFeeFlag = NConvert.ToBoolean(this.Reader[14].ToString());
                    bedFeeItem.Price = NConvert.ToDecimal(this.Reader[15].ToString());
                    bedFeeItem.PatientID = this.Reader[16].ToString();
                    bedFeeItem.BedNO = this.Reader[17].ToString();

                }

                this.Reader.Close();

                return bedFeeItem;
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        /// <summary>
        /// ���봲λ��Ϣ��һ����¼(fin_ipb_bedfeegrade)�����ڸ��˴�λ��
        /// </summary>
        /// <param name="bedFeeItem">��λ��Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û��ɾ������¼ 0</returns>
        public int InsertBedFeeItemForPatient(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {
            string sql = string.Empty; //����fin_com_bedfeegrade���SQL���

            if (this.Sql.GetCommonSql("Fee.BedFeeItem.InsertBedFeeItemForPatient", ref sql) == -1)
            {
                sql = @"
                    INSERT INTO FIN_IPB_BEDFEEGRADE ( PK_ID,
						FEE_GRADE_CODE ,
						ITEM_CODE ,
						ITEM_NAME ,
						ITEM_NUMBER ,
						SORT_ID ,
						BABY_FLAG ,
						DATE_FLAG ,
						BEGIN_DATE ,
						END_DATE ,
						VALID_STATE ,
						OPER_CODE ,
						OPER_DATE,
                        EXT_FLAG,
                        OUT_FEEFLAG,
                        ITEM_PRICE,
                        INPATIENT_NO,
                        BED_NO
					)  VALUES(
						 '{0}',
						'{1}' ,       --��λ�ȼ�����
						'{2}' ,       --��Ŀ����
						'{3}' ,       --������Ŀ����
						{4} ,       --������Ŀ����
						{5} ,       --˳���
						'{6}' ,       --Ӥ����־ 0 �޹� 1 �й�
						'{7}' ,       --ʱ���ʶ 1 ��ʱ����� 0 ��ʱ���޹�
						to_date('{8}','yyyy-mm-dd HH24:mi:ss') ,       --��ʼʱ��
						to_date('{9}','yyyy-mm-dd HH24:mi:ss') ,       --��������
						'{10}' ,       --��Ч�Ա�ʶ 1 ���� 0 ͣ�� 2 ����
						'{11}' ,       --����Ա
						sysdate ,        --��������
						'{12}',
						'{13}',
                        {14},
                        '{15}',
                        '{16}')
                            ";
            }

            return this.ExecNoQuery(sql, this.GetBedFeeItemParmsForPatient(bedFeeItem));
        }

        /// <summary>
        /// ���´�λ��Ϣһ����¼
        /// </summary>
        /// <param name="bedFeeItem">��λ��Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û��ɾ������¼ 0</returns>
        public int UpdateBedFeeItemForPatient(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem)
        {

            string sql = string.Empty; //����fin_com_bedfeegrade���SQL���

            if (this.Sql.GetCommonSql("Fee.BedFeeItem.UpdateBedFeeItemForPatient", ref sql) == -1)
            {
                sql = @"
                     Update FIN_IPB_BEDFEEGRADE 
                     Set
					 	FEE_GRADE_CODE = '{1}' ,
					 	ITEM_CODE = '{2}' ,
						ITEM_NAME = '{3}' ,                     --������Ŀ����
						ITEM_NUMBER = {4} ,                     --������Ŀ����
						SORT_ID = {5} ,                         --˳���
						BABY_FLAG = '{6}' ,                     --Ӥ����־ 0 �޹� 1 �й�
						DATE_FLAG = '{7}' ,                     --ʱ���ʶ 1 ��ʱ����� 0 ��ʱ���޹�
						BEGIN_DATE = to_date('{8}','yyyy-mm-dd HH24:mi:ss') , --��ʼʱ��
						END_DATE = to_date('{9}','yyyy-mm-dd HH24:mi:ss') , --��������
						VALID_STATE = '{10}' ,                   --��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
						OPER_CODE = '{11}' ,                    --����Ա
						OPER_DATE = sysdate, --��������
						EXT_FLAG='{12}',
						OUT_FEEFLAG='{13}',
                        ITEM_PRICE={14}
					 Where   PK_ID = '{0}'
                     And INPATIENT_NO='{15}'
                     And BED_NO='{16}'
                    ";

            }

            return this.ExecNoQuery(sql, this.GetBedFeeItemParmsForPatient(bedFeeItem));
        }
		#endregion

		#region ��������

		/// <summary>
		/// ���ݴ�λ�ȼ�������ȡBedFeeItem��Ϣ
		/// </summary>
		/// <param name="feeCode"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBedFeeItemByMinFeeCode()", true)]
		public ArrayList SelectByFeeCode(string feeCode)
		{

			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.SelectByFeeCode",ref strSql)==-1) return null;
			
			try
			{   				
				strSql = string.Format(strSql,feeCode);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}
			
			try
			{
				ArrayList List = new ArrayList();
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{

					FS.HISFC.Models.Fee.BedFeeItem obj = new FS.HISFC.Models.Fee.BedFeeItem();
					obj.ID = Reader[0].ToString();
					obj.FeeGradeCode = Reader[1].ToString();
					obj.ID = Reader[2].ToString();
					obj.Name = Reader[3].ToString();
                    obj.Qty = FrameWork.Function.NConvert.ToDecimal(Reader[4].ToString());

                    obj.SortID = FrameWork.Function.NConvert.ToInt32(Reader[5].ToString());
                    obj.IsBabyRelation = FrameWork.Function.NConvert.ToBoolean(Reader[6].ToString());
                    obj.IsTimeRelation = FrameWork.Function.NConvert.ToBoolean(Reader[7].ToString());
		
					obj.BeginTime = (DateTime)Reader[8];
					obj.EndTime = (DateTime)Reader[9];
						
					obj.ValidState = (EnumValidState)NConvert.ToInt32(Reader[10]);
					obj.ExtendFlag = Reader[13].ToString();

					List.Add(obj);
				}
				
				this.Reader.Close();

				return List;
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
		}

		/// <summary>
		/// ����סԺ��ˮ�Ż�ȡ��λ�ȼ�
		/// </summary>
		/// <param name="InpatientNo">סԺ��ˮ��</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��QueryBedGradeByInpatientNO()", true)]
		public ArrayList GetBedGradeByInpatientNo(string InpatientNo)
		{
			ArrayList ALLBedInfo = new ArrayList();
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.GetBedGradeByInpatienNo",ref strSql)==-1) 
			{				
				return null;
			}			
			try
			{
				strSql = string.Format(strSql,InpatientNo);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}      	
			if(this.ExecQuery(strSql)==-1)return null;
			while(this.Reader.Read())
			{
				FS.HISFC.Models.Base.Bed BedInfo  = new FS.HISFC.Models.Base.Bed();
				BedInfo.ID=Reader[0].ToString();
				BedInfo.BedGrade.ID =Reader[1].ToString();
				BedInfo.InpatientNO =Reader[2].ToString();				
				ALLBedInfo.Add(BedInfo);
			}
			this.Reader.Close();
			return ALLBedInfo;

		}

		/// <summary>
		/// ���ݴ��źŻ�ȡ��λ�ȼ�
		/// </summary>
		/// <param name="BedNo">����</param>
		/// <returns></returns>
		[Obsolete("����,ʹ��GetBedGradeByBedNO()", true)]
		public FS.HISFC.Models.Base.Bed GetBedGradeByBedNo(string BedNo)
		{
			FS.HISFC.Models.Base.Bed BedInfo = null;
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.GetBedGradeByBedNo",ref strSql)==-1) 
			{		
				this.Err = this.Sql.Err;
				return null;
			}			
			try
			{
				strSql = string.Format(strSql,BedNo);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}      	
			if(this.ExecQuery(strSql)==-1)return null;
			while(this.Reader.Read())
			{
				BedInfo  = new FS.HISFC.Models.Base.Bed();
				BedInfo.ID=Reader[0].ToString();
				BedInfo.BedGrade.ID =Reader[1].ToString();
				BedInfo.InpatientNO =Reader[2].ToString();				
				
			}
			this.Reader.Close();
			return BedInfo;
		}

		/// <summary>
		/// ���´�λ��Ϣ
		/// </summary>
		/// <param name="info">��λ��Ϣʵ��</param>
		/// <returns> -1 ʧ�� > 1 �ɹ�</returns>
		[Obsolete("����,ʹ��UpdateBedFeeItem()", true)]
		public int Update(FS.HISFC.Models.Fee.BedFeeItem info)
		{
		
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.UpdateBedFeeItem",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql,info.ID,info.FeeGradeCode,info.ID,info.Name,info.Qty,
					
					info.SortID,
                    FrameWork.Function.NConvert.ToInt32(info.IsBabyRelation),
                    FrameWork.Function.NConvert.ToInt32(info.IsTimeRelation),
				
					info.BeginTime,info.EndTime,
					info.ValidState,
					this.Operator.ID);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��InsertBedFeeItem()", true)]
		public int Insert(FS.HISFC.Models.Fee.BedFeeItem info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.InsertBedFeeItem",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql, info.FeeGradeCode,info.ID,info.Name,info.Qty,
                    info.SortID, FrameWork.Function.NConvert.ToInt32(info.IsBabyRelation),
                    FrameWork.Function.NConvert.ToInt32(info.IsTimeRelation),
					
					info.BeginTime,info.EndTime,
					info.ValidState,this.Operator.ID);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��DeleteByItemCode()", true)]
		public int Delete(BedFeeItem info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.BedFeeItem.DeleteBedFeeItem",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql, info.ID);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}

		/// <summary>
		/// ��ѯ�Ƿ�ͣ���ˡ�  0  ���� 1 ͣ�� 2 ���� ������Ϊ����
		/// </summary>
		/// <param name="ItemCode"></param>
		/// <returns></returns>
		[Obsolete("����,ʹ��ValidState()", true)]
		public string  IsDisuser(string  ItemCode)
		{
			string ISDisuser = "";
			try
			{
				string strSql = "";
				if (this.Sql.GetCommonSql("Fee.BedFeeItem.IsDisuser",ref strSql)==-1) return this.Sql.Err;
				strSql = string.Format(strSql,ItemCode);
				this.ExecQuery(strSql);
				this.Reader.Read();
				ISDisuser = Reader[0].ToString();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				ISDisuser=ee.Message;
			}
			return ISDisuser;
		}

		#endregion
	}
}
