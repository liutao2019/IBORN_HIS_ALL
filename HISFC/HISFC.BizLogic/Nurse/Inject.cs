using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{

	/// <summary>
	/// ���ﻤʿע�������
	/// </summary>
	public class Inject : FS.FrameWork.Management.Database
	{
		public Inject()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ����ʱ�䣬�����Ų�ѯ����δע����Ϣ
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public ArrayList Query(string cardNo,DateTime dt)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Inject.Query.1",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,cardNo,dt);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// ����ʱ�䣬�����Ų�ѯ��������ע����Ϣ��δע�䣬��ע�䣩
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public ArrayList QueryAll(string cardNo,DateTime dt)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Inject.Query.2",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,cardNo,dt);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// ����ʱ�䣬�����Ų�ѯ��������ע����Ϣ��δע�䣬��ע�䣩
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public ArrayList QueryByOrder(string orderNo,DateTime dt)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Inject.Query.3",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,orderNo,dt);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// ����ʱ�䣬������,�����ţ�����˳��Ų�ѯ�����Ѿ��Ǽ���Ϣ
		/// </summary>
		/// <param name="cardNo"></param>
		/// <returns></returns>
		public ArrayList Query(string cardNo,string recipeNo,string seq,DateTime dt)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Inject.Query.4",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,cardNo,recipeNo,seq,dt);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// ����ע��ֽ���Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Nurse.Inject info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Inject.Insert",ref sql) == -1)return -1;

            try
            {
                string strMainDrug = "0";
                string strSelfMake = "0";
                if (info.Item.Order.Combo.IsMainDrug)
                {
                    strMainDrug = "1";
                }
                if (info.Item.Item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item tempi = (FS.HISFC.Models.Pharmacy.Item)info.Item.Item;

                    if (tempi.Product.IsSelfMade)
                    {
                        strSelfMake = "1";
                    }
                    sql = string.Format(sql,
                        info.ID,
                        info.OrderNO,
                        info.Patient.ID,
                        info.Patient.PID.CardNO,
                        info.Patient.Name,
                        info.Patient.Sex.ID,
                        info.Patient.Birthday,
                        info.Item.Order.DoctorDept.ID,
                        info.Item.Order.DoctorDept.Name,
                        info.Item.Order.ReciptDoctor.ID,
                        info.Item.Order.ReciptDoctor.Name,
                        info.Item.RecipeNO,
                        info.Item.SequenceNO,
                        info.Item.ID,
                        info.Item.Name,
                        info.Item.Item.Specs,
                        strSelfMake,
                        tempi.Quality.ID,
                        tempi.DosageForm,       /*����*/
                        info.Item.Item.MinFee.ID,
                        info.Item.Item.Price,
                        info.Item.Order.Frequency.ID,
                        info.Item.Order.Usage.ID,
                        info.Item.Order.Usage.Name,
                        info.Item.InjectCount,
                        ((Int32)info.Hypotest).ToString(),
                        info.Item.Order.DoseOnce,
                        info.Item.Order.DoseUnit,
                        tempi.BaseDose,
                        info.Item.Item.PackQty,
                        strMainDrug,
                        info.Item.Order.Combo.ID,
                        info.ExecTime,
                        info.Booker.ID,
                        info.Booker.OperTime,
                        info.MixOperInfo.ID,
                        info.MixOperInfo.Name,
                        info.MixTime,
                        info.InjectOperInfo.ID,
                        info.InjectOperInfo.Name,
                        info.InjectTime,
                        info.InjectSpeed,
                        info.EndTime,
                        info.SendemcTime,
                        info.Memo,
                        info.Item.ExecOper.ID,
                        info.InjectOrder,
                        info.StopOper.ID,
                        //{EB016FFE-0980-479c-879E-225462ECA6D0} ƿǩ����
                        info.PrintNo,
                        info.Item.Days);
                }
                else
                {
                    sql = string.Format(sql,
                         info.ID,
                         info.OrderNO,
                         info.Patient.ID,
                         info.Patient.PID.CardNO,
                         info.Patient.Name,
                         info.Patient.Sex.ID,
                         info.Patient.Birthday,
                         info.Item.Order.DoctorDept.ID,
                         info.Item.Order.DoctorDept.Name,
                         info.Item.Order.ReciptDoctor.ID,
                         info.Item.Order.ReciptDoctor.Name,
                         info.Item.RecipeNO,
                         info.Item.SequenceNO,
                         info.Item.ID,
                         info.Item.Name,
                         info.Item.Item.Specs,
                         "",
                         "",
                         "",   
                         info.Item.Item.MinFee.ID,
                         info.Item.Item.Price,
                         info.Item.Order.Frequency.ID,
                         info.Item.Order.Usage.ID,
                         info.Item.Order.Usage.Name,
                         info.Item.InjectCount,
                         "",
                         info.Item.Order.DoseOnce,
                         info.Item.Order.DoseUnit,
                         "",
                         info.Item.Item.PackQty,
                         strMainDrug,
                         info.Item.Order.Combo.ID,
                         info.ExecTime,
                         info.Booker.ID,
                         info.Booker.OperTime,
                         info.MixOperInfo.ID,
                         info.MixOperInfo.Name,
                         info.MixTime,
                         info.InjectOperInfo.ID,
                         info.InjectOperInfo.Name,
                         info.InjectTime,
                         info.InjectSpeed,
                         info.EndTime,
                         info.SendemcTime,
                         info.Memo,
                         info.Item.ExecOper.ID,
                         info.InjectOrder,
                         info.StopOper.ID,
                         info.PrintNo,
                         info.Item.Days);
                }
            }
            catch (Exception e)
            {
                this.Err = "ת������!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

			return this.ExecNoQuery(sql);
		}

		/// <summary>
		/// ������ˮ��ȡ����ɾ����һ�εǼ���Ϣ
		/// </summary>
		/// <param name="strId"></param> 
		/// <returns></returns>
		public int Delete(string strId)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Nurse.Inject.Delete",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,strId);
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
		/// ���µǼ���Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateReg(FS.HISFC.Models.Nurse.Inject info)
		{
			string sql = "";
			if(this.Sql.GetCommonSql("Nurse.Inject.Update.1",ref sql) == -1) return -1;
			try
			{
				sql = string.Format(sql,
                    info.ID,
                    info.OrderNO,
                    info.Item.Order.Combo.ID,
                    info.Booker.Name,   /*�Ǽǿ���*/
                    info.Booker.ID,
                    info.Booker.OperTime);
			}
			catch(Exception e)
			{
				this.Err ="ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}

		/// <summary>
		/// ������ҩ��Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateMix(FS.HISFC.Models.Nurse.Inject info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Inject.Update.2",ref sql) == -1) return -1;
			try
			{
				sql = string.Format(sql,
                    info.ID,
                    info.MixOperInfo.ID,
                    info.MixOperInfo.Name,
                    info.MixTime,
                    info.Memo);
			}
			catch(Exception e)
			{
				this.Err ="ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}

		/// <summary>
		/// ����ע����Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateInject(FS.HISFC.Models.Nurse.Inject info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Inject.Update.3",ref sql) == -1) return -1;
            
            try
			{
				sql = string.Format(sql,
                    info.ID,
                    info.InjectOperInfo.ID,
                    info.InjectOperInfo.Name,
                    info.InjectTime,
					info.InjectSpeed,
                    info.SendemcTime,
                    info.Memo);
			}
			catch(Exception e)
			{
				this.Err ="ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// ���°�����Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateStop(FS.HISFC.Models.Nurse.Inject info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Inject.Update.4",ref sql) == -1) return -1;

			try
			{
				sql = string.Format(sql,info.ID,info.StopOper.ID,info.EndTime);
			}
			catch(Exception e)
			{
				this.Err ="ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}

		/// <summary>
		/// ��ѯ���һ��ע����Ϣ
		/// </summary>
		/// <returns></returns>
		public FS.HISFC.Models.Nurse.Inject QueryLast()
		{
			FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Inject.Query.5",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL);
			ArrayList al = new ArrayList();
			al = this.myGetInfo(strSQL);
            if (al.Count > 0)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[0];
                return info;
            }
            else
            {
                return null;
            }
		}

        /// <summary>
        /// ��ѯ�����ע��ŵ�ע����Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryInjectOrder(string id)
        {
            FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
            string strSQL;
            string strWhere = "";
            strSQL = this.GetSqlInjectInfo();
            if (this.Sql.GetCommonSql("Nurse.Inject.Query.7", ref strWhere) == -1) return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, id);
            ArrayList al = new ArrayList();
            al = this.myGetInfo(strSQL);
            return al;
        }

        /// <summary>
        /// ��ѯ���һ��ע����Ϣ
        /// </summary>
        /// <param name="code">ʹ�÷�������</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Inject QueryLast(string code)
        {
            FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
            string strSQL;
            string strWhere = "";
            strSQL = this.GetSqlInjectInfo();
            if (this.Sql.GetCommonSql("Nurse.Inject.Query.6", ref strWhere) == -1) return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, code);
            ArrayList al = new ArrayList();
            al = this.myGetInfo(strSQL);
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            return info;
        }

        /// <summary>
        /// ��ѯ��Ҫ��ҩ�ļ�¼
        /// {03E7916F-5AA8-4e95-BBE2-61EB6FDEB96C}
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryNeedDosageInjectRecord(DateTime beginDate)
        {
            string strSQL;
            string strWhere = "";
            strSQL = this.GetSqlInjectInfo();
            if (this.Sql.GetCommonSql("Nurse.Inject.Query.NeedDosage", ref strWhere) == -1)
                return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, beginDate.ToString("yyyy-MM-dd HH:mm:ss"));
            ArrayList al = new ArrayList();
            al = this.myGetInfo(strSQL);
            return al;
        }

        /// <summary>
        /// ��ѯ�û������һ��ע����Ϣ
        /// {03E7916F-5AA8-4e95-BBE2-61EB6FDEB96C}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Inject QueryLastByPatient(string cardNo)
        {
            FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
            string strSQL;
            string strWhere = "";
            strSQL = this.GetSqlInjectInfo();
            if (this.Sql.GetCommonSql("Nurse.Inject.Query.Last", ref strWhere) == -1)
                return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, cardNo);
            ArrayList al = new ArrayList();
            al = this.myGetInfo(strSQL);
            if (al == null)
            {
                return null;
            }
            if (al.Count == 0)
            {
                this.Err = "û���ҵ���¼";
                return null;
            }
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            return info;
        }

        /// <summary>
        /// ��ѯע��ǼǼ�¼�����ڲ���
        /// {EB016FFE-0980-479c-879E-225462ECA6D0}
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryRePrintInjectRecord(string printNo)
        {
            string strSQL;
            string strWhere = "";
            strSQL = this.GetSqlInjectInfo();
            if (this.Sql.GetCommonSql("Nurse.Inject.Query.CureRePrint", ref strWhere) == -1)
                return null;
            strSQL = strSQL + strWhere;
            strSQL = string.Format(strSQL, printNo);
            ArrayList al = new ArrayList();
            al = this.myGetInfo(strSQL);
            return al;
        }

		#region ������Ϣ
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetSqlInjectInfo() 
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Nurse.Inject.Query",ref strSql)==-1) return null;
			return strSql;
		}
		/// <summary>
		/// ����SQL,��ȡʵ������
		/// </summary>
		/// <param name="SQLString"></param>
		/// <returns></returns>
		public ArrayList myGetInfo(string SQLString) 
		{
			ArrayList al=new ArrayList();         
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) 
			{
				this.Err="���ע����Ϣʱ��ִ��SQL������"+this.Err; 
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read())  
				{
					#region �����ת��Ϊʵ��
                    FS.HISFC.Models.Nurse.Inject info = new FS.HISFC.Models.Nurse.Inject();
					info.ID = this.Reader[0].ToString();
					info.OrderNO = this.Reader[1].ToString();
					info.Patient.ID = this.Reader[2].ToString();
					info.Patient.PID.CardNO = this.Reader[3].ToString();
					info.Patient.Name = this.Reader[4].ToString();
					info.Patient.Sex.ID = this.Reader[5].ToString();
                    info.Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
					info.Item.Order.DoctorDept.ID = this.Reader[7].ToString();
					info.Item.Order.DoctorDept.Name = this.Reader[8].ToString();
					info.Item.Order.ReciptDoctor.ID = this.Reader[9].ToString();
					info.Item.Order.ReciptDoctor.Name = this.Reader[10].ToString();
					info.Item.RecipeNO = this.Reader[11].ToString();
                    info.Item.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[12].ToString());
					info.Item.ID = this.Reader[13].ToString();
					info.Item.Name = this.Reader[14].ToString();
					info.Item.Item.Specs = this.Reader[15].ToString();

                    //��������������ʱ�õ�,û�ط���
					info.Item.Item.SpecialFlag1 = this.GetBool(this.Reader[16].ToString()).ToString();//����ҩ
					info.Item.Item.SpecialFlag2 = this.Reader[17].ToString();
                    info.Item.Item.SpecialFlag3 = this.Reader[18].ToString();

					info.Item.Item.MinFee.ID = this.Reader[19].ToString();
                    info.Item.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
					info.Item.Order.Frequency.ID = this.Reader[21].ToString();
					info.Item.Order.Usage.ID = this.Reader[22].ToString();
					info.Item.Order.Usage.Name = this.Reader[23].ToString();
                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[24].ToString());
                    info.Hypotest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[25]);
                    info.Item.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
					info.Item.Order.DoseUnit = this.Reader[27].ToString();

                    info.Item.User01 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28]).ToString();
                    //info.Item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28]);

                    info.Item.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[29]);

                    info.Item.User02 = this.GetBool(this.Reader[30].ToString()).ToString();
					//info.Item.IsMainDrug = this.getBool(this.Reader[30].ToString());

					info.Item.Order.Combo.ID = this.Reader[31].ToString();
                    info.ExecTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32].ToString());
					info.Booker.ID = this.Reader[33].ToString();
                    info.Booker.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34]);
					info.MixOperInfo.ID = this.Reader[35].ToString();
					info.MixOperInfo.Name = this.Reader[36].ToString();
                    info.MixTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37]);
					info.InjectOperInfo.ID = this.Reader[38].ToString();
					info.InjectOperInfo.Name = this.Reader[39].ToString();
                    info.InjectTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40]);
					info.InjectSpeed = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[41].ToString());
					info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42]);
					info.SendemcTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[43]);
					info.Memo = this.Reader[44].ToString();
					info.Item.ExecOper.ID = this.Reader[45].ToString();//�Ǽǿ���
					info.InjectOrder = this.Reader[46].ToString();// 
					info.StopOper.ID = this.Reader[47].ToString();
                    //{EB016FFE-0980-479c-879E-225462ECA6D0} ƿǩ����
                    info.PrintNo = this.Reader[48].ToString();//��ӡ��ˮ��
                    info.Item.Days = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[49].ToString());
					#endregion
					al.Add(info);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="���ע����Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();
			this.ProgressBarValue=-1;
			return al;
		}
		private bool GetBool(string str)
		{
			bool bl = false;
			if(str == "1") bl = true;
			return bl;
		}
		#endregion
	}
}