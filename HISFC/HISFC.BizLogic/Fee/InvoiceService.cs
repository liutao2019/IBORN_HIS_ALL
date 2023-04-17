using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee;
using System.Data;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// InvoiceService<br></br>
	/// [��������: ��Ʊ��Ϣҵ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-10-01]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// {C81461BC-CAFE-4df0-837F-D8F759F6D7BF}
    [Obsolete("������InvoiceServiceNoEnum�����", true)]
	public class InvoiceService	: FS.FrameWork.Management.Database
	{

		#region ˽�з���

		/// <summary>
		/// ������Ʊ��Ϣ
		/// </summary>
		/// <param name="sql">ִ�еĲ�ѯSQL���</param>
		/// <param name="args">SQL���Ĳ���</param>
		/// <returns>�ɹ�:��Ʊ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryInvoicesBySql(string sql, params string[] args)
		{
			ArrayList invoices = new ArrayList();
			
			//ִ��SQL���
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}

			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read())
				{
					Invoice invoice = new Invoice();
					
					invoice.AcceptTime = NConvert.ToDateTime(this.Reader[0].ToString());
					invoice.Type.ID = this.Reader[1].ToString();
					invoice.AcceptOper.ID = this.Reader[2].ToString();
					invoice.BeginNO = this.Reader[3].ToString();
					invoice.EndNO =this.Reader[4].ToString();
					invoice.UsedNO =this.Reader[5].ToString();
					invoice.ValidState = this.Reader[6].ToString();
					invoice.IsPublic = NConvert.ToBoolean(this.Reader[7].ToString());
					invoice.AcceptOper.Name = this.Reader[8].ToString();
					
					invoices.Add(invoice);
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

			return invoices;
		}

		/// <summary>
		/// ���update����insert�˷�����Ĵ����������
		/// </summary>
		/// <param name="invoice">��Ʊʵ����</param>
		/// <returns>��������</returns>
		private string[] GetInvoiceParams(Invoice invoice) 
		{
			string[] args =
				{
					invoice.AcceptTime.ToString(),
					invoice.AcceptOper.ID,
					invoice.Type.ID.ToString(),
					invoice.Type.Name,
					invoice.BeginNO,
					invoice.EndNO,
					invoice.UsedNO,
					invoice.ValidState.ToString(),
					NConvert.ToInt32(invoice.IsPublic).ToString(),
					this.Operator.ID,
					this.GetSysDateTime()
				};
								 
			return args;
		}

		/// <summary>
		/// ������÷�Ʊ����Ա��Ϣ
		/// </summary>
		/// <param name="sql">ִ�еĲ�ѯSQL���</param>
		/// <param name="args">SQL���Ĳ���</param>
		/// <returns>�ɹ�:��Ա��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		private ArrayList QueryPersonsBySql(string sql, params string[] args)
		{
			ArrayList persons = new ArrayList();

			//ִ��SQL���
			if (this.ExecQuery(sql, args) == -1)
			{
				return null;
			}
			
			try
			{
				//ѭ����ȡ����
				while (this.Reader.Read())
				{
					FS.HISFC.Models.Base.Employee person = new FS.HISFC.Models.Base.Employee();
				
					person.ID = this.Reader[0].ToString();
					person.Name = this.Reader[1].ToString();
					person.SpellCode = this.Reader[2].ToString();
					person.WBCode = this.Reader[3].ToString();				
					person.Sex.ID = this.Reader[4].ToString();
					person.Birthday = this.Reader.GetDateTime(5);
					person.Duty.ID = this.Reader[6].ToString();
					person.Level.ID = this.Reader[7].ToString();
					person.GraduateSchool.ID = this.Reader[8].ToString();
					person.IDCard = this.Reader[9].ToString();
					person.Dept.ID = this.Reader[10].ToString();
					person.Nurse.ID = this.Reader[11].ToString();
					person.EmployeeType.ID = Reader[12].ToString();
					person.IsExpert = NConvert.ToBoolean(Reader[13].ToString());
					person.IsCanModify = NConvert.ToBoolean(Reader[14].ToString());
					person.IsNoRegCanCharge = NConvert.ToBoolean(this.Reader[15].ToString());
					person.ValidState = (HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[16].ToString());
					person.SortID = NConvert.ToInt32(this.Reader[17].ToString());
				
					persons.Add(person);
				}//ѭ������

				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();
				
				if (!this.Reader.IsClosed)
				{
					this.Reader.Close();
				}

				return null;
			}
			
			return persons;
		}

        /// <summary>
        /// ��÷�Ʊ�����������
        /// </summary>
        /// <param name="invoiceChange"></param>
        /// <returns></returns>
        private string[] GetInvoiceChangeParms(InvoiceChange invoiceChange)
        {
            string[] args =
				{
					invoiceChange.HappenNO.ToString(),
					invoiceChange.GetOper.ID,
					invoiceChange.InvoiceType.ID.ToString(),
                    invoiceChange.InvoiceType.Name,
					
					invoiceChange.BeginNO,
					invoiceChange.EndNO,
					invoiceChange.ShiftType,
					invoiceChange.Oper.ID,
                    invoiceChange.Memo.ToString()
				};

            return args;
        }

		#endregion

		#region ���з���

		/// <summary>
		/// ͨ����Ʊ����,��ѯ��Ʊ��Ϣ
		/// </summary>
		/// <param name="invoiceType">��Ʊ���</param>
		/// <returns>�ɹ�:��Ʊ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryInvoices(InvoiceTypeEnumService invoiceType)
		{
			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.SelectInvoices.2��SQL���";

				return null;
			}

			return this.QueryInvoicesBySql(sql, invoiceType.ID.ToString());
		}

		/// <summary>
		///  �����Ƿ���Groupy�Լ���Ʊ���������ʹ��״̬�ķ�Ʊ��Ϣ��
		/// </summary>
		/// <param name="invoiceType">��Ʊ���</param>
		/// <param name="isGroup">�Ƿ�Ʊ��</param>
		/// <returns>�ɹ�:��Ʊ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryInvoices(InvoiceTypeEnumService invoiceType, bool isGroup)
		{
			
			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByTypeIsGroup",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.SelectInvoices.ByTypeIsGroup��SQL���";

				return null;
			}

			return this.QueryInvoicesBySql(sql, invoiceType.ID.ToString(), NConvert.ToInt32(isGroup).ToString());
		}

		/// <summary>
		/// ������ù���Ʊ����ΪInvoiceType��������Ա��Ϣ /
		/// </summary>
		/// <param name="invoiceType">��Ʊ���</param>
		/// <returns>�ɹ�:��Ա��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryPersonsByInvoiceType(InvoiceTypeEnumService invoiceType)
		{
			
			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.GetPersonByInvoiceType",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.GetPersonByInvoiceType��SQL���";

				return null;
			}

			return this.QueryPersonsBySql(sql, invoiceType.ID.ToString());
		}

		/// <summary>
		/// ͨ����Ա���,�ͷ�Ʊ����ѯ����Ա�ķ�Ʊ��Ϣ
		/// </summary>
		/// <param name="personID">��Ա���</param>
		/// <param name="invoiceType">��Ʊ���</param>
		/// <returns>�ɹ�:��Ʊ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryInvoices(string personID, InvoiceTypeEnumService invoiceType)
		{
			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.1",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.SelectInvoices.1��SQL���";

				return null;
			}

			return this.QueryInvoicesBySql(sql, personID, invoiceType.ID.ToString());	
		}
		
		/// <summary>
		/// ͨ����Ա���,�ͷ�Ʊ����ѯ���Ƿ���������Ա�ķ�Ʊ��Ϣ
		/// </summary>
		/// <param name="personID">��Ա���</param>
		/// <param name="invoiceType">��Ʊ���</param>
		/// <param name="isGroup">�Ƿ������</param>
		/// <returns>�ɹ�:��Ʊ��Ϣ���� ʧ��:null û�в��ҵ����ݷ���Ԫ����Ϊ0��ArrayList</returns>
		public ArrayList QueryInvoices(string personID, InvoiceTypeEnumService invoiceType, bool isGroup)
		{
			string sql = string.Empty; //��ѯSQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByIdTypeIsGroup",ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.SelectInvoices.ByIdTypeIsGroup��SQL���";

				return null;
			}

			return this.QueryInvoicesBySql(sql, personID, invoiceType.ID.ToString(), NConvert.ToInt32(isGroup).ToString().ToString());	
		}

		/// <summary>
		/// ���ݷ�Ʊ���͵õ���ǰ���õ���ʼ��(Ĭ��)
		/// </summary>
		/// <param name="invoiceType">��Ʊ����</param>
		/// <returns>������ʼ��</returns>
		public string GetDefaultStartCode(InvoiceTypeEnumService invoiceType)
		{
			string sql = string.Empty;//��ѯSQL���
			string startNO = string.Empty;//��ʼ��

			if (this.Sql.GetCommonSql("Fee.InvoiceService.GetDefaultStartCode.1", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.GetDefaultStartCode.1��SQL���";

				return null;
			}

			try
			{
				sql = string.Format(sql, invoiceType.ID);
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.WriteErr();

				return null;
			}

			startNO = this.ExecSqlReturnOne(sql);
			
			//�����ʼ��Ϊ��,��ôĬ��Ϊ"000000000001"
			if (startNO == null || startNO == string.Empty)
			{
				startNO = "000000000001";
			}
			else//����,Ϊ��ǰ��+1
			{
				startNO = (Convert.ToInt64(startNO) + 1).ToString().PadLeft(12,'0');
			}

			return startNO;
		}
		
		/// <summary>
		/// �����������ʼ�źͷ�Ʊ�����Ƿ���Ч��
		/// </summary>
		/// <param name="startNO">��ʼ��</param>
		/// <param name="endNO">��Ʊ����</param>
		/// <param name="invoiceType">��Ʊ����</param>
		/// <returns>��Чtrue, ��Ч false</returns>
		public bool InvoicesIsValid(long startNO, long endNO, InvoiceTypeEnumService invoiceType)
		{
			
			if (endNO < startNO)
			{
				this.Err="�������ֹ�Ŵ�����ʼ��!";

				return false;
			}

			string sql = string.Empty;

			ArrayList invoices = new ArrayList();

			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2", ref sql) == -1) 
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.SelectInvoices.2��SQL���";
		
				return false;
			}

			invoices= QueryInvoicesBySql(sql, invoiceType.ID.ToString());
			
			//���û�з��������ķ�Ʊ,˵����������
			if (invoices == null)
			{
				return true;
			}
			
			for (int i = 0; i < invoices.Count; i++)
			{
				Invoice invoice = invoices[i] as Invoice;
				 
				if (Convert.ToInt64(invoice.BeginNO) <= startNO && startNO <= Convert.ToInt64(invoice.EndNO))
				{
					return false;
				}
				if (Convert.ToInt64(invoice.BeginNO) <= endNO && endNO <= Convert.ToInt64(invoice.EndNO))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// ����һ����Ʊ��Ϣ.
		/// </summary>
		/// <param name="invoice">��Ʊ��Ϣ��</param>
		/// <returns> �ɹ�: 1 ʧ��: -1 û�в����¼: 0</returns>
		public int InsertInvoice(Invoice invoice)
		{
			string sql = string.Empty;//����SQL���

			if (this.Sql.GetCommonSql("Fee.InvoiceService.InsertInvoice.1", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.InsertInvoice.1��SQL���";
				
				return -1;
			}
		
			return this.ExecNoQuery(sql, this.GetInvoiceParams(invoice));
		}
		
		/// <summary>
		/// ����һ����Ʊ��Ϣ.��Ʊ����ר��
		/// </summary>
		/// <param name="invoice">��Ʊ��Ϣ��</param>
		/// <returns> �ɹ�: 1 ʧ��: -1 û�и��¼�¼: 0</returns>
		public int UpdateInvoice(Invoice invoice)
		{
			string sql = string.Empty;//����SQL���

			if (this.Sql.GetCommonSql("Fee.InvocieService.UpdateInvoice.1", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvocieService.UpdateInvoice.1��SQL���";
				
				return -1;
			}
		
			return this.ExecNoQuery(sql, this.GetInvoiceParams(invoice));
		}

		/// <summary>
		/// ɾ��һ����¼
		/// </summary>
		/// <param name="invoice">��Ʊ��Ϣ��</param>
		/// <returns>�ɹ�: ɾ������Ŀ ʧ��: -1 û��ɾ����¼: 0</returns>
		public int Delete(Invoice invoice)
		{
			string sql = string.Empty;//����SQL���

			if (this.Sql.GetCommonSql("Fee.InvocieService.DeleteInvoice.1", ref sql) == -1)
			{
				this.Err = "û���ҵ�����Ϊ:Fee.InvocieService.DeleteInvoice.1��SQL���";
				
				return -1;
			}
		
			return this.ExecNoQuery(sql, invoice.AcceptTime.ToString(), invoice.AcceptOper.ID);
        }

        #region ��Ʊ���

        /// <summary>
        /// ���뷢Ʊ�����¼
        /// </summary>
        /// <param name="invoiceChange"></param>
        /// <returns></returns>
        public int InsertInvoiceChange(InvoiceChange invoiceChange)
        {
            string sql = string.Empty;//����SQL���

            if (this.Sql.GetCommonSql("Fee.InvoiceService.InsertInvoiceChange.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.InsertInvoiceChange.1��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetInvoiceChangeParms(invoiceChange));
        }

        /// <summary>
        /// ����Ʊ��ȡ��ȡ�÷�Ʊ������
        /// </summary>
        /// <param name="getPersonID"></param>
        /// <returns></returns>
        public int GetInvoiceChangeHappenNO(string getPersonID)
        {
            string sql = string.Empty;
            string happenNO = string.Empty;

            if (this.Sql.GetCommonSql("Fee.InvoiceService.GetInvoiceChangeHappenNO.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.GetInvoiceChangeHappenNO.1��SQL���";

                return -1;
            }

            try
            {
                sql = string.Format(sql, getPersonID);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }
            happenNO = this.ExecSqlReturnOne(sql);

            if (happenNO == null || happenNO == string.Empty)
            {
                return 1;
            }
            else//����,Ϊ��ǰ��+1
            {
                return (Convert.ToInt32(happenNO) + 1);
            }
        }

        /// <summary>
        /// ���·�Ʊ���ú��루��Ʊ�����ã�
        /// </summary>
        /// <param name="usedNO"></param>
        /// <param name="getPersonID"></param>
        /// <param name="getDate"></param>
        /// <returns></returns>
        public int UpdateInvoiceUsedNO(string usedNO, string getPersonID, DateTime getDate)
        {
            string sql = string.Empty;//����SQL���

            if (this.Sql.GetCommonSql("Fee.InvoiceService.UpdateInvoice.2", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.InvoiceService.UpdateInvoice.2��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, getDate.ToString("yyyy-MM-dd HH:mm:ss"), getPersonID, usedNO);
        }

        #endregion

        #region ��Ʊ����
        /// <summary>
        /// ��ȡ���﷢Ʊ
        /// </summary>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <param name="casher">�տ�Ա</param>
        ///<param name="list"></param>
        /// <returns></returns>
        public int GetOutpatientFeeInvoice(string begin, string end,string  casher, ref List<FS.FrameWork.Models.NeuObject> list)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetOutpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.CheckInvoice.GetOutpatientInvoiceʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, begin, end,casher);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "�������﷢Ʊ����ʧ�ܣ�";
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    obj.User02 = this.Reader[3].ToString();
                    obj.User03 = this.Reader[4].ToString();
                    obj.Memo = this.Reader[5].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// ��ȡסԺ��Ʊ����
        /// </summary>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">��ֹʱ��</param>
        /// <param name="casher">�տ�Ա</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetInpatientFeeInvoice(string begin, string end,string  casher, ref List<FS.FrameWork.Models.NeuObject> list)
        { 
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.GetInpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.CheckInvoice.GetOutpatientInvoiceʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, begin, end,casher);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "����סԺ��Ʊ����ʧ�ܣ�";
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    obj.Memo = this.Reader[3].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// �������﷢Ʊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="oper">������</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public int SaveCheckOutPatientFeeInvoice(FS.FrameWork.Models.NeuObject obj, string operTime, string oper,string begin,string end)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.CheckInvoice.SaveOutpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.CheckInvoice.SaveOutpatientInvoiceʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, obj.ID,//��Ʊ��
                                            obj.User02,//seq
                                            oper,//������
                                            operTime,//����ʱ��
                                            begin,//��ʼʱ��
                                            end);//����ʱ��
                
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(sqlStr);
        }

        /// <summary>
        /// ����סԺ��Ʊ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="oper">������</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public int SaveCheckInpatientFeeInvoice(FS.FrameWork.Models.NeuObject obj, string operTime, string oper,string begin,string end)
        { 
             string sqlStr = string.Empty;
             if (this.Sql.GetCommonSql("Fee.CheckInvoice.SaveInpatientInvoice", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.CheckInvoice.SaveOutpatientInvoiceʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, obj.ID,//��Ʊ��
                                            oper,//������
                                            operTime,//����ʱ��
                                            begin,//��ʼʱ��
                                            end);//����ʱ��
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(sqlStr);
        }
        #endregion

        #endregion


        #region ��������

        /// <summary>
		/// 
		/// </summary>
		/// <param name="invoiceType"></param>
		/// <returns></returns>
		[Obsolete("����,QueryInvoices()", true)]
		public ArrayList SelectInvoices(InvoiceTypeEnumService invoiceType)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2",ref strSql)==-1) return null;
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				string typeid =invoiceType.ToString();
				strSql = string.Format(strSql,typeid);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}
			return QueryInvoicesBySql(strSql);
		}

		/// <summary>
		///  �����Ƿ���Group�������ʹ��״̬�ķ�Ʊ��Ϣ��
		/// </summary>
		/// <param name="invoiceType"></param>
		/// <param name="isGroup"></param>
		/// <returns></returns>
		[Obsolete("����,QueryInvoices()", true)]
		public ArrayList SelectInvoicesByGroup(InvoiceTypeEnumService invoiceType,bool isGroup)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByTypeIsGroup",ref strSql)==-1) return null;
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				string typeid =invoiceType.ID.ToString();
				strSql = string.Format(strSql,typeid,isGroup == true ? 1 : 0);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}
			return QueryInvoicesBySql(strSql);
		}

		/// <summary>
		/// ������ù���Ʊ����ΪInvoiceType��������Ա��Ϣ /
		/// </summary>
		/// <param name="invoiceType"></param>
		/// <returns></returns>
		[Obsolete("����,QueryPersonsByInvoiceType()", true)]
		public ArrayList GetPersonByInvoiceType(string invoiceType)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.InvoiceService.GetPersonByInvoiceType",ref strSql)==-1) return null;
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				
				strSql = string.Format(strSql,invoiceType);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}

			return QueryPersonsBySql(strSql);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="personID"></param>
		/// <param name="invoiceType"></param>
		/// <returns></returns>
		[Obsolete("����,QueryInvoices()", true)]
		public ArrayList SelectInvoices(string personID,EnumInvoiceType invoiceType)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.1",ref strSql)==-1) return null;
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				string typeid =invoiceType.ToString();
				strSql = string.Format(strSql,personID,typeid);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}
			return QueryInvoicesBySql(strSql);
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="personID"></param>
		/// <param name="invoiceType"></param>
		/// <param name="isGroup"></param>
		/// <returns></returns>
		[Obsolete("����,QueryInvoices()", true)]
		public ArrayList SelectInvoices(string personID,EnumInvoiceType invoiceType,bool isGroup)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.ByIdTypeIsGroup",ref strSql)==-1) return null;
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				string typeid =invoiceType.ToString();
				strSql = string.Format(strSql,personID,typeid,isGroup == true ? 1 : 0);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return null;
			}
			return QueryInvoicesBySql(strSql);
			
			
		}
		/// <summary>
		/// ����Ƿ�������ɷ�Ʊ�š� 
		///		----�����������ʼ�źͷ�Ʊ�����Ƿ���Ч��
		/// </summary>
		/// <param name="startcode">��ʼ��</param>
		/// <param name="endcode">��Ʊ����</param>
		/// <param name="invoiceType">��Ʊ����</param>
		/// <returns></returns>
		[Obsolete("����,InvoicesIsValid()", true)]
		public bool CheckCodeValid(long startcode, long endcode,FS.HISFC.Models.Fee.InvoiceTypeEnumService invoiceType)
		{
			
			if (endcode<startcode)
			{
				this.Err="�������ֹ�Ŵ�����ʼ��!";
				return false;
			}
			string strSql = "";
			ArrayList al = new ArrayList();
			if (this.Sql.GetCommonSql("Fee.InvoiceService.SelectInvoices.2",ref strSql)==-1) 
			{
				this.Err="��ȡsql����!";
				this.ErrCode="��ȡsql����!";
				return false;
			}
			try
			{
				//0��ȡʱ��1��Ʊ����2��ȡ��3��Ʊ��ʼ��4��Ʊ��ֹ��5��Ʊ���ú�
				//6 used_state7���ñ�־
				strSql = string.Format(strSql,invoiceType.ID.ToString());
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return false;
			}
			al= QueryInvoicesBySql(strSql);
			if (al==null) return true;
			for(int i=0;i < al.Count;i++)
			{
				FS.HISFC.Models.Fee.Invoice obj;
				obj = (FS.HISFC.Models.Fee.Invoice)al[i];
				if ((long.Parse(obj.BeginNO)<=startcode)&&(startcode<=long.Parse(obj.EndNO)))return false;
				if ((long.Parse(obj.BeginNO)<=endcode)&&(endcode<=long.Parse(obj.EndNO)))return false;
			}
			return true;
		}

		#endregion       

	}
}
