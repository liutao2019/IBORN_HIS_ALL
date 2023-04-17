using System;
using System.Collections;
//{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
using System.Collections.Generic;
namespace FS.HISFC.BizLogic.Order.Outsource
{
	/// <summary>
	/// Order ��ժҪ˵����
	/// ����ҽ��
	/// </summary>
    public class Order : FS.FrameWork.Management.Database
	{
		public Order()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		

		#region ������������ɾ��

		/// <summary>
		/// ����һ��
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
        public int InsertOutsourceOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.OutsourceOrder.Insert";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            sql = this.myGetCommonSql(sql, order);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

		
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
        public int UpdateOutsourceOrder(FS.HISFC.Models.Order.OutPatient.Order order)
		{
            if (this.DeleteOutsourceOrder(order.Patient.ID, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) < 0)
            {
                return -1;//ɾ�����ɹ�
            }
            return this.InsertOutsourceOrder(order);
		}
		
		
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="seeNo"></param>
		/// <param name="seqNo"></param>
		/// <returns></returns>
        public int DeleteOutsourceOrder(string clinicCode, int seqNo)
		{
            /*
             * DELETE 
             * FROM met_ord_recipedetail   --��䴦����ϸ��
                WHERE     see_no='{0}' and sequence_no = {1} 
                AND status = '0'
             * */

            string sql = "Order.OutPatient.OutsourceOrder.Delete";
			if(this.Sql.GetCommonSql(sql, ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
                sql = string.Format(sql, clinicCode, seqNo);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(sql);
		}

		#endregion

        #region ����ҽ����������add by sunm

        public int InsertOutsourceOrderChangeInfo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.OutsourceOrder.InsertChangeInfo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            sql = this.myGetCommonSql(sql, order);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }
        /// <summary>
        /// ����ҽ�������¼
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOutsourceOrderChangedInfo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.OutsourceOrder.UpdateChangeInfo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            sql = System.String.Format(sql, order.DCOper.ID, order.SeeNO, order.SequenceNO);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOutsourceOrderBeCaceled(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.OutsourceOrder.CancelOrder";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = "Can't Find Sql:Order.OutPatient.OutsourceOrder.CancelOrder";
                return -1;
            }
            sql = System.String.Format(sql, order.ID);
            if (sql == null) return -1;
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region ����µĿ������
        /// <summary>
		/// 
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public int GetNewSeeNo( string cardNo )
		{
			string sql = "Order.OutPatient.Order.GetNewSeeNo.1";
			if(this.Sql.GetCommonSql(sql,ref sql) == -1) 
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				sql = string.Format(sql,cardNo);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return -1;
			}
			return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
		}
		#endregion

        /// <summary>
        /// �����ҽ��������
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderComboID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.Order.GetComboID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }


        #region  ����ҽ�����
        /// <summary>
        /// ����ҽ�����
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="sortID"></param>
        /// <returns></returns>
        public int UpdateOrderSortID(string orderID, int sortID, string clinicCode)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderSortID.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, orderID, sortID, clinicCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        #endregion


        #region ��ѯ

        /// <summary>
		/// ��ѯִ��ҽ��--ͨ��������Ų�ѯ
		/// </summary>
		/// <param name="seeNo"></param>
		/// <returns></returns>
        public ArrayList QueryOutsourceOrder(string seeNo)
		{
			string sql ="",sqlSelect = "",sqlWhere = "Order.OutPatient.Order.Query.Where.1";
			if(this.myGetSelectSql(ref sqlSelect) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
			sql = sqlSelect + " " + sqlWhere;
			sql = string.Format (sql,seeNo);
            return this.myGetExecOrder(sql);			
		}

        /// <summary>
        /// ��ѯ���ﴦ��
        /// </summary>
        /// <param name="clinicCode">���￴����ˮ��</param>
        /// <param name="seeNo">�������</param>
        /// <returns></returns>
        public ArrayList QueryOutsourceOrderByClinicCode(string clinicCode)
        {
            return this.QueryOutsourceOrderBase("Order.OutPatient.OutsourceOrder.Query.ByClinicCode", clinicCode);
        }

        /// <summary>
        /// ����whereIndex��ѯ���ﴦ��
        /// </summary>
        /// <param name="SqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryOutsourceOrderBase(string SqlIndex, params string[] args)
        {
            string sqlStr = "";
            if (this.myGetSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(SqlIndex, ref SqlIndex) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            sqlStr = sqlStr + "\r\n" + SqlIndex;

            sqlStr = string.Format(sqlStr, args);

            return this.myGetExecOrder(sqlStr);
        }

        /// <summary>
        /// ���ݴ����Ų�ѯҽ��
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string clinicCode,string recipeNO)
        {
            return this.QueryOutsourceOrderBase("Order.OutPatient.Order.Query.Where.4", clinicCode, recipeNO);
        }

        /// <summary>
        /// ���ݴ����Ų�ѯҽ��
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string recipeNO)
        {
            return this.QueryOutsourceOrderBase("Order.OutPatient.Order.Query.ByRecipeNO", recipeNO);
        }

		/// <summary>
		/// ��ѯһ��ҽ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string clinicCode, string sequenceNO)
        {
            ArrayList al = this.QueryOutsourceOrderBase("Order.OutPatient.Order.Query.Where.2", sequenceNO, clinicCode);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }
        		/// <summary>
		/// ��ѯһ��ҽ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string clinicCode, string sequenceNO,string recipeNO)
        {
            ArrayList al = this.QueryOutsourceOrderBase("Order.OutPatient.Order.Query.Where.8", sequenceNO, clinicCode,recipeNO);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }
        /// <summary>
        /// ������ѯ���ﴦ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList QueryBatchOrder(string clinicCode, string[] batchSeq)
        {
            string strBatchSeq = "''";
            for (int i = 0; i < batchSeq.Length; i++)
            {
                strBatchSeq += ",'" + batchSeq[i] + "'";
            }

            return this.QueryOutsourceOrderBase("Order.OutPatient.Order.BatchQuery.ByClinicAndSeq", clinicCode, strBatchSeq);
        }

        /// <summary>
        /// ����ҽ����Ų�ѯһ��ҽ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string sequenceNO)
        {
            ArrayList al = this.QueryOutsourceOrderBase("Order.OutPatient.Order.Query.Where.7", sequenceNO);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }

		/// <summary>
		/// ��ÿ�������б�
		/// </summary>
		/// <param name="cardNo">���￨��</param>
		/// <returns></returns>
		public ArrayList QuerySeeNoListByCardNo(string cardNo)
		{
			string sql = "Order.OutPatient.Order.GetSeeNoList";
			if(this.Sql.GetCommonSql(sql,ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
			try
			{
				sql = string.Format(sql,cardNo);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql) == -1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = this.Reader[0].ToString();
				obj.Name = this.Reader[1].ToString();
				obj.Memo = this.Reader[2].ToString();
				try
				{
					obj.User01 = this.Reader[3].ToString();
					obj.User02  = this.Reader[4].ToString();
					obj.User03 = this.Reader[5].ToString();
				}
				catch{}
				al.Add(obj);
			}		
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ��ÿ�������б�
		/// </summary>
		/// <param name="clinicNo"></param>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public ArrayList QuerySeeNoListByCardNo( string clinicNo, string cardNo )
		{
			string sql = "Order.OutPatient.Order.GetSeeNoList.2";
			if(this.Sql.GetCommonSql(sql,ref sql) == -1) return null;
			try
			{
				sql = string.Format(sql,clinicNo,cardNo);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql) == -1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = this.Reader[0].ToString();
				obj.Name = this.Reader[1].ToString();
				obj.Memo = this.Reader[2].ToString();
				try
				{
					obj.User01 = this.Reader[3].ToString();
                    obj.User02  = this.Reader[4].ToString();
                    if (Reader.FieldCount > 5)
                    {
                        obj.User03 = this.Reader[5].ToString();
                    }
				}
				catch{}
				al.Add(obj);
			}		
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ��ѯ������Ÿ�������
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ArrayList QuerySeeNoListByName(string name)
		{
			string sql = "Order.OutPatient.Order.GetSeeNoList.Name";
			if(this.Sql.GetCommonSql(sql,ref sql) == -1)
			{
				this.Err = this.Sql.Err;
				return null;
			}
			try
			{
				sql = string.Format(sql,name);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql) == -1) return null;
			ArrayList al = new ArrayList();
			while(this.Reader.Read())
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
				obj.ID = this.Reader[0].ToString();
				obj.Name = this.Reader[1].ToString();
				obj.Memo = this.Reader[2].ToString();
				try
				{
					obj.User01 = this.Reader[3].ToString();
					obj.User02  = this.Reader[4].ToString();
					obj.User03 = this.Reader[5].ToString();
				}
				catch{}
				al.Add(obj);
			}		
			this.Reader.Close();
			return al;
		}

        /// <summary>
        /// ȡ��ҩƷ������ͨ������źͿ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="seeNo"></param>
        /// <returns></returns>
        public ArrayList GetPhaRecipeNoByClinicNoAndSeeNo(string clinicNo, string seeNo)
        {
            string sql = "Order.Outsource.Order.GetPhaRecipeNoByClinicNoAndSeeNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, clinicNo, seeNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList alRecipe = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                
                alRecipe.Add(obj);
            }
            this.Reader.Close();
            return alRecipe;
        }

       /// <summary>
        /// ��ȡ������ͨ������źͿ����
       /// </summary>
       /// <param name="clinicNo"></param>
       /// <param name="seeNo"></param>
       /// <param name="flag">0��ȫ����1��ҩƷ��2��ҩƷ</param>
       /// <returns></returns>
        public IList<FS.FrameWork.Models.NeuObject> GetRecipeNoByClinicNoAndSeeNo(string clinicNo, string seeNo,string flag)
        {
            string sql = "Order.OutPatient.Order.GetRecipeNoByClinicNoAndSeeNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, clinicNo, seeNo, flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            IList<FS.FrameWork.Models.NeuObject> iRecipe = new List<FS.FrameWork.Models.NeuObject>();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();

                iRecipe.Add(obj);
            }
            this.Reader.Close();
            return iRecipe;
        }



        /// <summary>
        /// ���ݷ�Ʊ�Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="invociceNo"></param>
        /// <returns></returns>
        public ArrayList QueryRecipeListByInvoiceNo(string invociceNo)
        {
            string sql = "Order.OutPatient.Order.QueryRecipeNOByInvoiceNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, invociceNo);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                ArrayList alRecipe = new ArrayList();
                FS.HISFC.Models.Base.Spell obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Base.Spell();
                    //������
                    obj.ID = this.Reader[0].ToString();
                    //ҽ��
                    obj.Name = this.Reader[1].ToString();
                    //����ʱ��
                    obj.Memo = this.Reader[2].ToString();
                    //����
                    obj.SpellCode = this.Reader[3].ToString();
                    //����
                    obj.WBCode = this.Reader[4].ToString();
                    //��Ʊ��
                    obj.UserCode = this.Reader[5].ToString();

                    alRecipe.Add(obj);
                }
                return alRecipe;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

		#endregion

        #region ˽�к���

        /// <summary>
		/// ���sql���������
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="order"></param>
		/// <returns></returns>
        protected string myGetCommonSql(string sql, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            #region sql
            //   0--������� ,1 --��Ŀ��ˮ��,2 --�����,3   --������ ,4    --�Һ�����
            //   5 --�Һſ���,6   --��Ŀ����,7   --��Ŀ����, 8  --���, 9  --1ҩƷ��2��ҩƷ
            //   10   --ϵͳ���,   --��С���ô���,   --����,   --��������,   --����
            //    --��װ����,   --�Ƽ۵�λ,   --�Էѽ��0,   --�Ը����0,   --�������0
            //   --��������,   --����ҩ,   --ҩƷ���ʣ���ҩ����ҩ,   --ÿ������
            //     --ÿ��������λ,   --���ʹ���,   --Ƶ��,   --Ƶ������,   --ʹ�÷���
            //     --�÷�����,   --�÷�Ӣ����д,   --ִ�п��Ҵ���,   --ִ�п�������
            //      --��ҩ��־,   --��Ϻ�,   --1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����
            //     --Ժ��ע�����,   --��ע,   --����ҽ��,   --����ҽ������,   --ҽ������
            //     --����ʱ��,   --����״̬,1������2�շѣ�3ȷ�ϣ�4����,   --������,   --����ʱ��
            //        --�Ӽ����0��ͨ/1�Ӽ�,   --��������,   --����,   --���뵥��
            //     --0���Ǹ���/1�Ǹ���,   --�Ƿ���Ҫȷ�ϣ�1��Ҫ��0����Ҫ,   --ȷ����
            //        --ȷ�Ͽ���,   --ȷ��ʱ��,   --0δ�շ�/1�շ�,   --�շ�Ա
            //       --�շ�ʱ��,   --������,    --��������ˮ��,     --��ҩҩ����    
            //      --������λ�Ƿ�����С��λ 1 �� 0 ���ǣ�      --ҽ�����ͣ�Ŀǰû�У�
            #endregion

            //if(order.Item.IsPharmacy)//ҩƷ
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item pItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} ���ڽ���������뵥��apply_no�ֶθ�order.ApplyNo20100505 yangw
                System.Object[] s = {
                                        order.SeeNO ,                                        
                                        FS.FrameWork.Function.NConvert.ToInt32(order.ID),
                                        order.Patient.ID,                                        
                                        order.Patient.PID.CardNO,                                        
                                        order.RegTime,                                        
										order.InDept.ID,                                        
                                        pItem.ID,                                        
                                        pItem.Name,                                        
                                        pItem.Specs,                                        
                                        "1",                                        
										order.Item.SysClass.ID,                                        
                                        order.Item.MinFee.ID,                                        
                                        order.Item.Price,
                                        order.Qty,
                                        order.HerbalQty,                                        
										pItem.PackQty,
                                        pItem.PriceUnit,
                                        order.FT.OwnCost ,
                                        order.FT.PayCost,
                                        order.FT.PubCost,                                        
										pItem.BaseDose,
                                        FS.FrameWork.Function.NConvert.ToInt32(pItem.Product.IsSelfMade),
                                        pItem.Quality.ID,
                                        order.DoseOnce,                                        
										order.DoseUnit,
                                        pItem.DosageForm.ID,
                                        order.Frequency.ID,
                                        order.Frequency.Name,
                                        order.Usage.ID,                                        
										order.Usage.Name,
                                        order.Usage.Memo,
                                        order.ExeDept.ID,
                                        order.ExeDept.Name,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),
                                        order.Combo.ID,
                                        ((Int32)order.HypoTest).ToString(),
										order.InjectCount,
                                        order.Memo,
                                        order.ReciptDoctor.ID,
                                        order.ReciptDoctor.Name,
                                        order.ReciptDept.ID,                                        
										order.MOTime,                                        
                                        order.Status,
                                        order.DCOper.ID,
                                        order.DCOper.OperTime,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),
                                        order.Sample.Name,
                                        order.CheckPartRecord,
                                        order.ApplyNo,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),
                                        order.ConfirmOper.ID,                                        
										order.ConfirmOper.Dept.ID,
                                        order.ConfirmOper.OperTime,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),
                                        order.ChargeOper.ID,                                        
										order.ChargeOper.OperTime,
                                        order.ReciptNO,
                                        order.SequenceNO,                                        
                                        order.StockDept.ID,
                                        order.MinunitFlag,
                                        order.UseDays.ToString(),
                                        order.SubCombNO,
                                        order.ExtendFlag1,                                        
										order.ReciptSequence,
                                        order.NurseStation.Memo,
                                        order.SortID,
                                        order.DoseOnceDisplay,
                                        order.DoseUnitDisplay,
                                        order.FirstUseNum,
                                        order.Patient.Pact.ID,
                                        order.Patient.Pact.PayKind.ID
                                    };

                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else//��ҩƷ
            {
                FS.HISFC.Models.Fee.Item.Undrug pItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} ���ڽ���������뵥��apply_no�ֶθ�order.ApplyNo 20100505 yangw
                System.Object[] s = {
                                        order.SeeNO,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.ID),
                                        order.Patient.ID,
                                        order.Patient.PID.CardNO,
                                        order.RegTime,                                        
										order.InDept.ID,
                                        pItem.ID,
                                        pItem.Name,
                                        pItem.Specs,
                                        "2",                                        
										order.Item.SysClass.ID,
                                        order.Item.MinFee.ID,
                                        order.Item.Price,
                                        order.Qty,
                                        order.HerbalQty,                                        
										pItem.PackQty,
                                        pItem.PriceUnit,
                                        order.FT.OwnCost ,
                                        order.FT.PayCost,
                                        order.FT.PubCost,                                        
										"0",
                                        0,
                                        "",
                                        order.DoseOnce,                                        
										order.DoseUnit,
                                        "",
                                        order.Frequency.ID,
                                        order.Frequency.Name,
                                        order.Usage.ID,                                        
										order.Usage.Name,
                                        order.Usage.Memo,
                                        order.ExeDept.ID,
                                        order.ExeDept.Name,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),
                                        order.Combo.ID,
                                        ((Int32)order.HypoTest).ToString(),                                        
										order.InjectCount,
                                        order.Memo,
                                        order.ReciptDoctor.ID,
                                        order.ReciptDoctor.Name,
                                        order.ReciptDept.ID,                                        
										order.MOTime,                                        
                                        order.Status,
                                        order.DCOper.ID,
                                        order.DCOper.OperTime,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),
                                        order.Sample.Name,
                                        order.CheckPartRecord,
                                        order.ApplyNo,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),
                                        order.ConfirmOper.ID,                                        
										order.ConfirmOper.Dept.ID,
                                        order.ConfirmOper.OperTime,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),
                                        order.ChargeOper.ID,                                        
										order.ChargeOper.OperTime,
                                        order.ReciptNO,
                                        order.SequenceNO,                                        
                                        order.StockDept.ID,
                                        order.MinunitFlag,
                                        "",                                        
                                        order.SubCombNO,
                                        order.ExtendFlag1,                                        
										order.ReciptSequence,
                                        order.NurseStation.Memo,
                                        order.SortID,
                                        order.DoseOnceDisplay,
                                        order.DoseUnitDisplay,
                                        order.FirstUseNum,
                                        order.Patient.Pact.ID,
                                        order.Patient.Pact.PayKind.ID
                                    };
                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
        }

		
		/// <summary>
		/// ��ò�ѯsql���
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		protected int myGetSelectSql(ref string sql)
		{
            return this.Sql.GetCommonSql("Order.OutPatient.OutsourceOrder.Query.Select", ref sql);
		}
		

		/// <summary>
		/// ���ִ��ҽ����Ϣ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
        protected ArrayList myGetExecOrder(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.OutPatient.Order order = new FS.HISFC.Models.Order.OutPatient.Order();
                try
                {
                    order.SeeNO = this.Reader[0].ToString();
                    order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//��Ŀ��ˮ��
                    order.ID = this.Reader[1].ToString();//��Ŀ��ˮ��
                    order.Patient.ID = this.Reader[2].ToString();//�����
                    order.Patient.PID.CardNO = this.Reader[3].ToString();//��������
                    order.RegTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);//�Һ�����
                    order.ReciptDept.ID = this.Reader[5].ToString();//�Һſ��� ����
                    if (this.Reader[9].ToString() == "1")//ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                        item.ID = this.Reader[6].ToString();
                        item.Name = this.Reader[7].ToString();
                        item.Specs = this.Reader[8].ToString();
                        item.SysClass.ID = this.Reader[10].ToString();
                        item.MinFee.ID = this.Reader[11].ToString();
                        item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        item.DoseUnit = this.Reader[24].ToString();
                        item.Product.IsSelfMade = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[21]);
                        item.Quality.ID = this.Reader[22].ToString();
                        item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        item.DosageForm.ID = this.Reader[25].ToString();
                        item.PriceUnit = this.Reader[16].ToString();

                        //{6DBBDC62-2303-4d97-85EF-8BA2A622117A} ������� xuc
                        item.SplitType = this.Reader[61].ToString();

                        order.Item = item;

                    }
                    else if (this.Reader[9].ToString() == "2")//��ҩƷ
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
                        item.ID = this.Reader[6].ToString();
                        item.Name = this.Reader[7].ToString();
                        item.Specs = this.Reader[8].ToString();
                        item.SysClass.ID = this.Reader[10].ToString();
                        item.MinFee.ID = this.Reader[11].ToString();
                        item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        item.PriceUnit = this.Reader[16].ToString();
                        order.Item = item;

                    }
                    else
                    {
                        this.Err = "��ȡmet_ord_recipedetail������ҩƷ��ҩƷ����drug_flag=" + this.Reader[9].ToString();
                        this.WriteErr();
                        return null;
                    }
                    order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                    order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                    order.Unit = this.Reader[16].ToString();
                    order.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                    order.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                    order.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);

                    order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                    order.DoseUnit = this.Reader[24].ToString();

                    order.Frequency.ID = this.Reader[26].ToString();
                    order.Frequency.Name = this.Reader[27].ToString();
                    order.Usage.ID = this.Reader[28].ToString();
                    order.Usage.Name = this.Reader[29].ToString();
                    order.Usage.Memo = this.Reader[30].ToString();
                    order.ExeDept.ID = this.Reader[31].ToString();
                    order.ExeDept.Name = this.Reader[32].ToString();
                    order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[33]);
                    order.Combo.ID = this.Reader[34].ToString();
                    order.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35]);
                    order.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36]);
                    order.Memo = this.Reader[37].ToString();
                    order.ReciptDoctor.ID = this.Reader[38].ToString();
                    order.ReciptDoctor.Name = this.Reader[39].ToString();
                    order.ReciptDept.ID = this.Reader[40].ToString();
                    //order.ReciptDept.Name =this.Reader[41].ToString();
                    order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[41]);
                    order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[42]);
                    order.DCOper.ID = this.Reader[43].ToString();
                    order.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44]);
                    order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[45]);
                    order.Sample.Name = this.Reader[46].ToString();
                    order.CheckPartRecord = this.Reader[47].ToString();
                    order.ApplyNo = this.Reader[48].ToString();
                    order.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[49]);
                    order.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[50]);
                    order.ConfirmOper.ID = this.Reader[51].ToString();
                    order.ConfirmOper.Dept.ID = this.Reader[52].ToString();
                    order.ConfirmOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53]);
                    order.IsHaveCharged = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[54]);
                    order.ChargeOper.ID = this.Reader[55].ToString();
                    order.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56]);
                    order.ReciptNO = this.Reader[57].ToString();
                    order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58]);
                    order.StockDept.ID = this.Reader[59].ToString();
                    order.MinunitFlag = this.Reader[60].ToString();//��С��λ��־
                    order.UseDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62]);//{08024C29-12FE-4629-B982-C50AE9034B82}
                    order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString());//������Ϻţ����飩
                    order.ExtendFlag1 = this.Reader[64].ToString();//��ƿ��Ϣ
                    order.ReciptSequence = this.Reader[65].ToString();//�շ�����
                    order.NurseStation.Memo = this.Reader[66].ToString();
                    order.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[67]);
                    #region {C3DF9328-3458-4bb4-895E-5B122B6582BB}

                    if (this.Reader[9].ToString() == "1")
                    {
                        order.DoseOnceDisplay = this.Reader[68].ToString();
                        if (order.DoseOnceDisplay.Length <= 0)
                            order.DoseOnceDisplay = order.DoseOnce.ToString();
                    }

                    order.DoseUnitDisplay = this.Reader[69].ToString();
                    order.FirstUseNum = this.Reader[70].ToString();

                    if (this.Reader.FieldCount > 71)
                    {
                        order.Patient.Pact.ID = Reader[71].ToString();
                    } 
                    if (this.Reader.FieldCount > 72)
                    {
                        order.Patient.Pact.PayKind.ID = Reader[72].ToString();
                    }

                    #endregion
                    #region sql
                    //		0--������� 1  --��Ŀ��ˮ��, 2  --�����, 3  --������ 4 -�Һ�����  5,   --�Һſ���
                    //       6,   --��Ŀ���� 7,   --��Ŀ���� 8,   --��� 9,   --1ҩƷ��2��ҩƷ 10,   --ϵͳ���
                    //       11,   --��С���ô��� 12,   --���� 13,   --�������� 14 ,   --����  15,   --��װ����
                    //       16,   --�Ƽ۵�λ 17,   --�Էѽ�� 18  --�Ը���� 19,   --������� 20,   --��������
                    //       21,   --����ҩ 2  --ҩƷ���ʣ���ҩ����ҩ 23,   --ÿ������ 24,   --ÿ��������λ 25,   --���ʹ���
                    //       26,   --Ƶ�� 27,   --Ƶ������ 28 --ʹ�÷��� 29,   --�÷����� 30,   --�÷�Ӣ����д
                    //       31,   --ִ�п��Ҵ��� 32,   --ִ�п������� 33   --��ҩ��־ 34   --��Ϻ� 35   --1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����
                    //       36,   --Ժ��ע����� 37   --��ע  38,   --����ҽ�� 39,   --����ҽ������ 40,   --ҽ������
                    //       41,   --����ʱ��   42,   --����״̬,1������2�շѣ�3ȷ�ϣ�4����    43,   --������   44,   --����ʱ��    45,   --�Ӽ����0��ͨ/1�Ӽ�
                    //       46,   --��������    47,   --���� 48,   --���뵥��    49,   --0û�и���/1������/2�Ǹ���     50,   --�Ƿ���Ҫȷ�ϣ�1��Ҫ��0����Ҫ
                    //       51,   --ȷ����     52,   --ȷ�Ͽ���    53,   --ȷ��ʱ��    54,   --0δ�շ�/1�շ�      55,   --�շ�Ա
                    //       56,   --�շ�ʱ��      57,   --������      58    --��������ˮ��
                    //  FROM met_ord_recipedetail   --��䴦����ϸ��
                    #endregion
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return null;
                }
                finally
                {
                    if (!this.Reader.IsClosed)
                    {
                        this.Reader.Close();
                    }
                }
                al.Add(order);
            }
            this.Reader.Close();
            return al;
        }


		#endregion


        /// <summary>
        /// ����Ƥ����Ϣ
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [����] 2 [��Ƥ��] 3 [+] 4 [-]</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            //return FS.FrameWork.Public.EnumHelper.Current.GetName(HypotestCode);

            switch ((int)HypotestCode)
            {
                case 0:
                    //return "����ҪƤ��";
                    return "";
                case 1:
                    return "[����]";
                case 2:
                    return "[��Ƥ��]";
                case 3:
                    return "[+]";
                case 4:
                    return "[-]";
                default:
                    return "[����]";
            }
        }
    }
}
