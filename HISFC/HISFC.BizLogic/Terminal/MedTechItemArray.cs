using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Terminal 
{
	/// <summary>
	/// MedTechItemArray <br></br>
	/// [��������: ҽ��ԤԼ�Ű�ģ��]<br></br>
	/// [�� �� ��: zhangjunyi]<br></br>
	/// [����ʱ��: 2005-3]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2007��03��06'
	///		�޸�Ŀ��='�汾�ع�'
	///		�޸�����=''
	///  />
	/// </summary>
	public class MedTechItemArray : FS.FrameWork.Management.Database 
	{

		public MedTechItemArray() 
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ˽�к���

		/// <summary>
		/// ��ȡ��sql
		/// </summary>
		/// <returns>SQL</returns>
		private string GetApplySql()
		{
			// SQL���
			string strSQL = "";
			//ȡSELECT���
			if (this.Sql.GetSql("MedTech.QueryScheMa.Query.1", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.Query.1�ֶ�!";
				return null;
			}
			// ����
			return strSQL;
		}

		/// <summary>
		/// ��ȡԤԼ�Ű�ģ��
		/// </summary>
		/// <param name="strSQL">SQL���</param>
		/// <returns>ԤԼ�Ű�ģ������</returns>
		private ArrayList QuerySchemaBase(string strSQL)
		{
			// ԤԼ�Ű�ģ������
			ArrayList schemaList = new ArrayList();
			// ִ�в�ѯ
			this.ExecQuery(strSQL);
			// ת��������
			while (this.Reader.Read())
			{
				// ��ʱģ��
				FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp = new FS.HISFC.Models.Terminal.MedTechItemTemp();

				// ��Ŀ����
				medTechItemTemp.MedTechItem.Item.ID = this.Reader[0].ToString();
				// ��Ŀ����
				medTechItemTemp.MedTechItem.Item.Name = this.Reader[1].ToString();
				// ��λ��ʶ
				medTechItemTemp.MedTechItem.ItemExtend.UnitFlag = this.Reader[2].ToString();
				// ���ұ���
				medTechItemTemp.MedTechItem.ItemExtend.Dept.ID = this.Reader[3].ToString();
				// ��������
				medTechItemTemp.Dept.Name = this.Reader[4].ToString();
				// ��ΪԤԼʱ�� 
				medTechItemTemp.MedTechItem.ItemExtend.BookTime = this.Reader[5].ToString();
				// ���
				medTechItemTemp.NoonCode = this.Reader[6].ToString();
				// ԤԼ�޶�
				medTechItemTemp.BookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ����ԤԼ�޶�
				medTechItemTemp.SpecialBookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��Ϊ--�Ѿ�ԤԼ��
				medTechItemTemp.MedTechItem.Item.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
				// ��Ϊ--����ԤԼ��
				medTechItemTemp.MedTechItem.Item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
				//				temp.MedTechItem.Item.OperInfo.ID = this.Reader[11].ToString() ;//����Ա��
				//				temp.MedTechItem.Item.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);//��������
				
				schemaList.Add(medTechItemTemp);
			}
			// ���ؽ��
			return schemaList;
		}

		/// <summary>
		/// ���update����insert��Ĵ����������
		/// </summary>
		/// <param name="medTechItemTemp">ʵ��</param>
		/// <returns>�ַ�������</returns>
		private string[] GetParms(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			string[] strParm ={   
								 medTechItemTemp.MedTechItem.Item.ID,
								 medTechItemTemp.MedTechItem.Item.Name,
								 medTechItemTemp.MedTechItem.ItemExtend.UnitFlag,
								 medTechItemTemp.MedTechItem.ItemExtend.Dept.ID,
								 medTechItemTemp.Dept.Name,
								 medTechItemTemp.MedTechItem.ItemExtend.BookTime,
								 medTechItemTemp.NoonCode,
								 medTechItemTemp.BookLmt.ToString(),
								 medTechItemTemp.SpecialBookLmt.ToString(),
								 medTechItemTemp.MedTechItem.Item.ChildPrice.ToString(),
								 medTechItemTemp.MedTechItem.Item.SpecialPrice.ToString(),
								 this.Operator.ID,
								 this.GetSysDateTime(),
								 medTechItemTemp.ConformNum.ToString(),
                                 medTechItemTemp.TmpFlag.ToString(),
                                 medTechItemTemp.StartTime,             //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
                                medTechItemTemp.EndTime,                //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
                                medTechItemTemp.Machine.ID              //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
							 };
			// ����
			return strParm;
		}

		/// <summary>
		/// ȡ��Ϣ�б�������һ�����߶���
		/// ˽�з����������������е���
		/// </summary>
		/// <param name="SQL">SQL���</param>
		/// <returns>��ʳ��Ŀ��Ϣ��Ϣ��������</returns>
		private ArrayList GetItem(string SQL)
		{
			// ��ʳ��Ŀ��Ϣ��Ϣ��������
			ArrayList medTechItemList = new ArrayList();
			// ��ʳ��Ŀ��Ϣʵ��
			FS.HISFC.Models.Terminal.MedTechItemTemp temp; 
			//
			//ִ�в�ѯ���
			//
			if (this.ExecQuery(SQL) == -1)
			{
				this.Err = "�����ʳ��Ŀ��Ϣʱ��ִ��SQL������" + this.Err;
				this.ErrCode = "-1";
				return null;
			}
			try
			{
				while (this.Reader.Read())
				{
					//ȡ��ѯ����еļ�¼
					temp = new FS.HISFC.Models.Terminal.MedTechItemTemp();

					// ��Ŀ����
					temp.MedTechItem.Item.ID = this.Reader[0].ToString();
					// ��Ŀ����
					temp.MedTechItem.Item.Name = this.Reader[1].ToString();
					// ��λ��ʶ
					temp.MedTechItem.ItemExtend.UnitFlag = this.Reader[2].ToString();
					// ���ұ���
					temp.MedTechItem.ItemExtend.Dept.ID = this.Reader[3].ToString();
					// ��������
					temp.Dept.Name = this.Reader[4].ToString();
					// ��ΪԤԼʱ�� 
					temp.MedTechItem.ItemExtend.BookTime = this.Reader[5].ToString();
					// ���
					temp.NoonCode = this.Reader[6].ToString();
					// ԤԼ�޶�
					temp.BookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
					// ����ԤԼ�޶�
					temp.SpecialBookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());
					// ��Ϊ--�Ѿ�ԤԼ��
					temp.MedTechItem.Item.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
					// ��Ϊ--����ԤԼ��
					temp.MedTechItem.Item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
					// ����Ա
					temp.MedTechItem.Item.Oper.ID = this.Reader[11].ToString();
					// ��������
					temp.MedTechItem.Item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
					//��ʶλ
                    temp.TmpFlag = this.Reader [ 14 ].ToString ( );
                    //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                    if (this.Reader.FieldCount > 14)
                    {
                        //��ʼʱ��
                        temp.StartTime = this.Reader[15].ToString();
                        //����ʱ��
                        temp.EndTime = this.Reader[16].ToString();
                        //�豸
                        temp.Machine.ID = this.Reader[17].ToString();
                    }

					medTechItemList.Add(temp);
				}
			}//�׳�����
			catch (Exception ex)
			{
				this.Err = "�����Ϣʱ����" + ex.Message;
				this.ErrCode = "-1";
				return null;
			}
			this.Reader.Close();

			this.ProgressBarValue = -1;
			// ����
			return medTechItemList;
		}

		/// <summary>
		/// ����SQL����ȡģ����Ϣ
		/// </summary>
		/// <param name="strSQL">SQL���</param>
		/// <returns>ģ����Ϣ����</returns>
		private ArrayList GetSchema(string strSQL)
		{
			// ģ������
			ArrayList list = new ArrayList();
			
			// ִ��SQL���
			this.ExecQuery(strSQL);
			
			while (this.Reader.Read())
			{
				// ģ��ʵ��
				FS.HISFC.Models.Terminal.MedTechItemTemp info = new FS.HISFC.Models.Terminal.MedTechItemTemp();

				// ��Ŀ����
				info.MedTechItem.Item.ID = this.Reader[0].ToString();
				// ��Ŀ����
				info.MedTechItem.Item.Name = this.Reader[1].ToString();
				// ��λ��ʶ
				info.MedTechItem.ItemExtend.UnitFlag = this.Reader[2].ToString();
				// ���ұ���
				info.MedTechItem.ItemExtend.Dept.ID = this.Reader[3].ToString();
				// ��������
				info.Dept.Name = this.Reader[4].ToString();
				// ��ΪԤԼʱ�� 
				info.MedTechItem.ItemExtend.BookTime = this.Reader[5].ToString();
				// ���
				info.NoonCode = this.Reader[6].ToString();
				// ԤԼ�޶�
				info.BookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ����ԤԼ�޶�
				info.SpecialBookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��Ϊ--�Ѿ�ԤԼ��
				info.MedTechItem.Item.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
				// ��Ϊ--����ԤԼ��
				info.MedTechItem.Item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());

                //��ʼʱ��    //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                info.StartTime = this.Reader[19].ToString();
                //����ʱ��    //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                info.EndTime = this.Reader[20].ToString();
                //ִ���豸    //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                info.Machine.ID = this.Reader[21].ToString();
				
				list.Add(info);
			}
			return list;
		}
		
		#endregion

		#region ���к���

		/// <summary>
		/// ����в���һ����¼
		/// </summary>
		/// <param name="medTechItemTemp">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int Insert(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			// SQL���
			string strSQL = "";
			// ȡ���������SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.Insert", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.Insert�ֶ�!";
				return -1;
			}
			try
			{
				// ȡ�����б�
				string[] strParm = GetParms(medTechItemTemp);
				//�滻SQL����еĲ�����
				strSQL = string.Format(strSQL, strParm);            
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.Insert:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			// ִ�з���
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ���±���һ����¼
		/// </summary>
		/// <param name="medTechItemTemp">������չ������</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int Update(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			// SQL���
			string strSQL = "";
			//ȡ���²�����SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.Update", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.Update�ֶ�!";
				return -1;
			}
			try
			{
				//ȡ�����б�
				string[] strParm = GetParms(medTechItemTemp);
				//�滻SQL����еĲ�����
				strSQL = string.Format(strSQL, strParm);            
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.Update:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			// ִ�з���
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ɾ������һ����¼
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <param name="bookDate">ԤԼʱ��</param>
		/// <param name="noonCode">���</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int Delete(string deptCode, string itemCode, string bookDate, string noonCode)
		{
			// SQL���
			string strSQL = "";
			// ȡɾ��������SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.Delete", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.Delete�ֶ�!";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, deptCode, itemCode, bookDate, noonCode);    //�滻SQL����еĲ�����
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.Delete:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			// ִ�з���
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// �����޶�
		/// </summary>
		/// <param name="medTechItemTemp">������չ������</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int UpdateItemLimit(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			// SQL���
			string strSQL = "";
			//ȡ���²�����SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.UpdateItemLimit", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.UpdateLmt�ֶ�!";
				return -1;
			}
			try
			{
				//ȡ�����б�
				string[] strParm = GetParms(medTechItemTemp);
				//�滻SQL����еĲ�����
				strSQL = string.Format(strSQL, strParm);            
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.UpdateLmt" + ex.Message;
				this.WriteErr();
				return -1;
			}
			// ִ�з���
			return this.ExecNoQuery(strSQL);
		}

       

		/// <summary>
		/// ���ݿ��ұ��룬ʱ�� ȡһ������
		/// </summary>
		/// <param name="deptCode"></param>
		/// <param name="bookDate"></param>
		/// <returns></returns>
		public ArrayList QueryItem(string deptCode, string bookDate)
		{
			// SQL���
			string strSQL = "";
			// �Ű�����
			ArrayList itemList = null;
			//ȡSELECT���
			if (this.Sql.GetSql("MedTech.ItemArray.Query.1", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.Query.1�ֶ�!";
				return null;
			}
			//ƥ��SQL���
			try
			{
				strSQL = string.Format(strSQL, deptCode, bookDate);
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.Query.1" + ex.Message;
				return null;
			}

			//ȡ��Ϣ
			itemList = this.GetItem(strSQL);

			return itemList;
		}

		/// <summary>
		/// ��ȡ��Ŀ�Ű����Ŀ������Ϣ
		/// </summary>
		/// <param name="deptCode"> ���ұ���</param>
		/// <param name="beginDate">��ʼ���� </param>
		/// <param name="endDate">�������� </param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>������-1</returns>
		public int QuerySchema(string deptCode, System.DateTime beginDate, System.DateTime endDate, string itemCode, ref System.Data.DataSet dsSchema)
		{
			// SQL���
			string SQL = "";
			// ��ʼʱ��
			string dateBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
			// ����ʱ��
			string dateEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";
			// SELECT���
			string select = GetApplySql();
			if (select == null)
			{
				return -1;
			}
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.QueryScheMa.Query.Where1", ref SQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.QueryScheMa.Query.Where1�ֶ�!";
				return -1;
			}
			//ƥ��SQL���
			try
			{
				SQL = string.Format(SQL, deptCode, dateBegin, dateEnd, itemCode);
				SQL = select + SQL;
			}
			catch (Exception ex)
			{
				this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.Query.1" + ex.Message;
				return -1;
			}
			// ִ��SQL���
			this.ExecQuery(SQL, ref dsSchema);
			// ����
			return 1;
		}

		/// <summary>
		/// ��ȡ��Ŀ�Ű����Ŀ������Ϣ
		/// </summary>
		/// <param name="deptCode"> ���ұ���</param>
		/// <param name="beginDate">��ʼ���� </param>
		/// <param name="endDate">�������� </param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>������null</returns>
		public ArrayList QuerySchema(string deptCode, System.DateTime beginDate, System.DateTime endDate, string itemCode)
		{
			// ��Ŀ�Ű�����
			ArrayList schemaList = new ArrayList();
			// SQL���
			string SQL = "";
			// ��ʼ����
			string dateBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
			// ��������
			string dateEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";
			//ȡSELECT���
			string select = GetApplySql();
			if (select == null)
			{
				return null;
			}
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.QueryScheMa.Query.Where1", ref SQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.QueryScheMa.Query.Where1�ֶ�!";
				return null;
			}
			//
			// ƥ��ִ��SQL���
			try
			{
				
				//ƥ��SQL���
				SQL = string.Format(SQL, deptCode, dateBegin, dateEnd, itemCode);
				// ����SQL���
				SQL = select + SQL;
				// ��ȡ����
				schemaList = QuerySchemaBase(SQL);
			}
			catch (Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
			// ����
			return schemaList;
		}

		/// <summary>
		/// ��ȡ��Ŀ�Ű����Ŀ������Ϣ
		/// </summary>
		/// <param name="deptCode"> ���ұ���</param>
		/// <param name="beginDate">��ʼ���� </param>
		/// <param name="endDate">�������� </param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <param name="noonID">������</param>
		/// <returns>������null</returns>
		public ArrayList QuerySchema(string deptCode, System.DateTime beginDate, System.DateTime endDate, string itemCode, string noonID)
		{
			ArrayList list = new ArrayList();
			try
			{
				string strSQL = "";
				string strBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
				string strEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";
				//ȡSELECT���
				string strSql = GetApplySql();
				if (strSql == null)
				{
					return null;
				}
				// ԭ��������������������û�д��ֵ��������Ի�������ǰ�İ�
				//if (this.Sql.GetSql("MedTech.QueryScheMa.Query.Wherezjy", ref strSQL) == -1)
				//{
				//    this.Err = "û���ҵ�MedTech.QueryScheMa.Query.Where1�ֶ�!";
				//    return null;
				//}
                if (this.Sql.GetSql("MedTech.QueryScheMa.Query.WhereQuerySchema", ref strSQL) == -1)
				{
                    this.Err = "û���ҵ�MedTech.QueryScheMa.Query.WhereQuerySchema�ֶ�!";
					return null;
				}
				//��ʽ��SQL���
				strSQL = string.Format(strSQL, deptCode, strBegin, strEnd, itemCode, noonID);
				strSQL = strSql + strSQL;
				
				list = this.GetSchema(strSQL);
			}
			catch (Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
			return list;
		}

		/// <summary>
		///  ����ҽ����ˮ�Ų�ѯ��Ŀ�Ű����Ŀ������Ϣ
		/// </summary>
		/// <param name="orderSequence">ҽ����ˮ��</param>
		/// <returns>��Ŀ�Ű����Ŀ������Ϣ</returns>
		public ArrayList QuerySchema(string orderSequence)
		{
			// ��Ŀ�Ű����Ŀ������Ϣ
			ArrayList SchemaList = new ArrayList();
			// SQL���
			string strSQL = "";
			//ȡSELECT���
			string strSql = GetApplySql();
			if (strSql == null)
			{
				return null;
			}
			if (this.Sql.GetSql("MedTech.QueryScheMa.Query.Where2", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.QueryScheMa.Query.Where2�ֶ�!";
				return null;
			}
			//ƥ��SQL���
			try
			{
				strSQL = string.Format(strSQL, orderSequence);
				
				strSQL = strSql + strSQL;
				// ��ѯ
				SchemaList = QuerySchemaBase(strSQL);
			}
			catch (Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
			// ����
			return SchemaList;
		}

		/// <summary>
		/// ����ԤԼ�޶�
		/// </summary>
		/// <param name="tempMedTechItemTemp">ԤԼģ��</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int UpdateItemBookingNumber(FS.HISFC.Models.Terminal.MedTechItemTemp tempMedTechItemTemp)
		{
			string strSQL = "";
			//ȡ���²�����SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.UpdateItemBookNum", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.UpdateLmt�ֶ�!";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, tempMedTechItemTemp.MedTechItem.Item.ID, tempMedTechItemTemp.MedTechItem.ItemExtend.BookTime.ToString(), tempMedTechItemTemp.NoonCode, tempMedTechItemTemp.MedTechItem.Item.ChildPrice.ToString(), tempMedTechItemTemp.MedTechItem.Item.SpecialPrice.ToString(),tempMedTechItemTemp.MedTechItem.ItemExtend.Dept.ID);
			}
			catch (Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����MedTech.ItemArray.UpdateLmt" + ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		
		#endregion 

        #region  ��ʱ
        /// <summary>
        /// ����ҽ����ˮ�� �����Ű���Ϣ
        /// </summary>
        /// <param name="orderSequence">ҽ����ˮ�� </param>
        /// <param name="operType">��������</param>
        /// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
        [Obsolete("����",true)]
        public int UpdateItemConfirm(string orderSequence, OperType operType)
        {
            // SQL���
            string strSQL = "";
            // �Ű���Ŀ��Ϣ
            ArrayList list = QuerySchema(orderSequence);
            if (list == null || list.Count == 0)
            {
                return 0;
            }
            //ԤԼʵ��
            FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp = (FS.HISFC.Models.Terminal.MedTechItemTemp)list[0];
            if (operType == OperType.Minus)
            {
                //ȡ���²�����SQL���
                if (this.Sql.GetSql("MedTech.ItemArray.UpdateItemConfirm.1", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�MedTech.ItemArray.UpdateItemConfirm.1�ֶ�!";
                    return -1;
                }
            }
            else
            {
                if (this.Sql.GetSql("MedTech.ItemArray.UpdateItemConfirm.2", ref strSQL) == -1)
                {
                    this.Err = "û���ҵ�MedTech.ItemArray.UpdateItemConfirm.2�ֶ�!";
                    return -1;
                }
            }
            try
            {
                //�����ҵ���Ӧ��ԤԼ��Ϣ Ȼ���ҵ��Ű���Ϣ,Ȼ������Ű���Ϣ
                strSQL = string.Format(strSQL,
                                       medTechItemTemp.MedTechItem.Item.ID,
                                       medTechItemTemp.MedTechItem.ItemExtend.BookTime,
                                       medTechItemTemp.NoonCode);
            }
            catch (Exception ex)
            {
                this.Err = "ƥ��SQL���ʱ����MedTech.ItemArray.UpdateLmt" + ex.Message;
                this.WriteErr();
                return -1;
            }
            // ִ�з���
            return this.ExecNoQuery(strSQL);
        }
        #endregion 

    }
	
	/// <summary>
	/// �������
	/// </summary>
	public enum OperType 
	{
		/// <summary>
		/// ����
		/// </summary>
		Add ,
		/// <summary>
		/// ����
		/// </summary>
		Minus

	}

}