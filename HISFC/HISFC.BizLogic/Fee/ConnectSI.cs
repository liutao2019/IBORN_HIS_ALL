using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Collections;

namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// ConnectSI ��ժҪ˵����
	/// </summary>
	public class ConnectSI
	{
		/// <summary>
		/// ���캯�����������ʱ���������ݿ�
		/// </summary>
		public ConnectSI()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			conn.ConnectionString = this.GetConnectString();
			command.Connection = conn;
			command.CommandType = System.Data.CommandType.Text;
			
			try
			{
				conn.Open();
			}
			catch(SqlException ex)
			{
				this.Err = "���ݿ�����ʧ��!" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
				throw ex;
			}
			try
			{
				trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
				command.Transaction = trans;
			}
			catch(SqlException ex)
			{
				this.Err = "���ݿ�����ʧ��!" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
				throw ex;
			}
		}
		/// <summary>
		/// ȥ�����ʱ�򣬹ر����ݿ�
		/// </summary>
		~ConnectSI()
		{
			try
			{
				if(conn.State == System.Data.ConnectionState.Open)
				{
					conn.Dispose();
					conn.Close();
				}
			}
			catch(Exception ex)
			{
				this.Err = "���ݿ�����ʧ��!" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
			}
		}

		SqlConnection conn = new SqlConnection();
		SqlCommand command = new SqlCommand();
		System.Data.SqlClient.SqlTransaction trans;
		private string profileName  = System.Windows.Forms.Application.StartupPath + @".\profile\SiDataBase.xml";//ҽ�����ݿ���������;

        FS.FrameWork.Models.NeuLog log = new FS.FrameWork.Models.NeuLog();
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string Err;
		/// <summary>
		/// �������
		/// </summary>
		public string ErrCode;
		private System.Data.SqlClient.SqlDataReader Reader;
		#region ���ݿ��������
		/// <summary>
		/// �ύ
		/// </summary>
		public void Commit()
		{
			trans.Commit();
		}
		/// <summary>
		/// �ع�
		/// </summary>
		public void RollBack()
		{
			trans.Rollback();
		}
		/// <summary>
		/// �ر����ݿ�����
		/// </summary>
		public void Close()
		{
			if(conn.State == System.Data.ConnectionState.Open)
			{
				conn.Dispose();
				conn.Close();
			}
		}
		/// <summary>
		/// �����ݿ�����
		/// </summary>
		public void Open()
		{
			conn.ConnectionString = this.GetConnectString();
			command.Connection = conn;
			command.CommandType = System.Data.CommandType.Text;
			
			try
			{
				conn.Open();
			}
			catch(SqlException ex)
			{
				this.Err = "���ݿ�����ʧ��!" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
				throw ex;
			}
			try
			{
				trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
				command.Transaction = trans;
			}
			catch(SqlException ex)
			{
				this.Err = "���ݿ�����ʧ��!" + ex.Message;
				this.ErrCode = "-1";
				this.WriteErr();
				throw ex;
			}
		}

		/// <summary>
		/// д�������Ϣ
		/// </summary>
		private void WriteErr()
		{
			this.log.WriteLog("Error:" +this.GetType().ToString()+":"+this.Err+this.ErrCode);
		}
		/// <summary>
		/// ������Ӵ�
		/// </summary>
		/// <returns></returns>
		public string GetConnectString()
		{
			string dbInstance =  "";
			string DataBaseName = "";
			string userName = "";
			string password = "";
			string connString = "";
			
			if(!System.IO.File.Exists(profileName))
			{
				FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
				XmlDocument doc = new XmlDocument();
				XmlElement root;
				root = myXml.CreateRootElement(doc,"SqlServerConnectForHis4.0","1.0");
				
				XmlElement dbName = myXml.AddXmlNode(doc, root, "����", "");

				myXml.AddNodeAttibute(dbName, "���ݿ�ʵ����", "");
				myXml.AddNodeAttibute(dbName, "���ݿ���", "");
				myXml.AddNodeAttibute(dbName, "�û���", "");
				myXml.AddNodeAttibute(dbName, "����", "");

				try
				{
					StreamWriter sr = new StreamWriter(profileName, false,System.Text.Encoding.Default);
					string cleandown = doc.OuterXml;
					sr.Write(cleandown);
					sr.Close();
				}
				catch(Exception ex)
				{
					this.Err = "����ҽ�����ӷ������ó���!" + ex.Message;
					this.ErrCode = "-1";
					this.WriteErr();
					return "";
				}
				
				return "";
			}
			else
			{
				XmlDocument doc = new XmlDocument();
		
				try
				{
					StreamReader sr = new StreamReader(profileName ,System.Text.Encoding.Default);
					string cleandown = sr.ReadToEnd();
					doc.LoadXml(cleandown);
					sr.Close();
				}
				catch{return "";}
				
				XmlNode node = doc.SelectSingleNode("//����");

				try
				{
				
					dbInstance = node.Attributes["���ݿ�ʵ����"].Value.ToString();
					DataBaseName = node.Attributes["���ݿ���"].Value.ToString();
					userName = node.Attributes["�û���"].Value.ToString();
					password = node.Attributes["����"].Value.ToString();
				}
				catch{return "";}

				connString = "packet size=4096;user id=" + userName + ";data source=" + dbInstance +";pers" +
					"ist security info=True;initial catalog=" + DataBaseName  + ";password=" + password;
			}
			
			return connString;
		}
		/// <summary>
		/// ִ�и���,ɾ��,�����SQL���
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private int ExecNoQuery(string sql)
		{
			command.CommandText = sql;

			try
			{
				return command.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				this.Err = "��ȡ���ݿ�ʧ��!" + ex.Message + "|" + sql;
				this.ErrCode = "-1";
				this.WriteErr();
				return -1;
			}
		}
		/// <summary>
		/// ִ�в�ѯ���
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private int ExecQuery(string sql)
		{
			if(conn.ConnectionString == "")
				return -1;
			
			command.CommandText = sql;

			try
			{
				Reader = command.ExecuteReader();
			}
			catch(Exception ex)
			{
				this.Err = "��ȡ���ݿ�ʧ��!" + ex.Message + "|" + sql;
				this.ErrCode = "-1";
				this.WriteErr();
				return -1;
			}

			//conn.Close();

			return 0;
		}
		#endregion

		#region �Ǽ�
		/// <summary>
		/// ͨ�������,��û��߻�����Ϣ
		/// </summary>
		/// <param name="regNo">����ҽ�ƺ�</param>
		/// <returns>null û���ҵ��������ݿ���� obj ���ߵǼ���Ϣʵ��</returns>
		public FS.HISFC.Models.RADT.PatientInfo GetRegPersonInfo(string regNo)
		{
			string sql = "select * from his_zydj where jydjh = '" + regNo + "'";
			
			if(this.ExecQuery(sql) == -1)
				return null;

			FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
			try
			{
				while(Reader.Read())
				{
					
					obj.SIMainInfo.RegNo = Reader[0].ToString();
					obj.SIMainInfo.HosNo = Reader[1].ToString();
					obj.SIMainInfo.ID = Reader[0].ToString();
					obj.IDCard = Reader[2].ToString();
					obj.Name = Reader[3].ToString();
					obj.Name = Reader[3].ToString();
					obj.CompanyName = Reader[4].ToString();
					obj.Sex.ID = Reader[5].ToString();
					obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
					obj.SIMainInfo.EmplType = Reader[7].ToString();
					obj.SIMainInfo.User01 = Reader[8].ToString();
					obj.PID.PatientNO = Reader[9].ToString();
					obj.PVisit.MedicalType.ID = Reader[10].ToString();
					obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
					obj.SIMainInfo.InDiagnose.Name = Reader[12].ToString();
					obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
					obj.PVisit.PatientLocation.Dept.ID = Reader[14].ToString();
					obj.PVisit.PatientLocation.Bed.ID = Reader[15].ToString();
					obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
					obj.User01 = Reader[17].ToString();
					obj.User02 = Reader[18].ToString();
					obj.User03 = Reader[19].ToString();
					obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());
				}

				Reader.Close();
			}
			catch(Exception ex)
			{
				this.ErrCode = "-1";
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			
			return obj;
		}
		/// <summary>
		/// ���ĳ���ҽ���Ǽǻ�����Ϣ
		/// </summary>
		/// <param name="regDate">�Ǽ�����</param>
		/// <returns>null���� ArrayList �������ߵǼ���Ϣ��ʵ������</returns>
		public ArrayList GetRegPersonInfo(DateTime regDate)
		{
			string sql = "select * from his_zydj where RYRQ = '" + regDate + "'";
			
			if(this.ExecQuery(sql) == -1)
				return null;

			ArrayList al = new ArrayList();
			
			
			try
			{
				while(Reader.Read())
				{
					FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
					
					obj.SIMainInfo.RegNo = Reader[0].ToString();
					obj.SIMainInfo.HosNo = Reader[1].ToString();
					obj.SIMainInfo.ID = Reader[0].ToString();
					obj.IDCard = Reader[2].ToString();
					obj.Name = Reader[3].ToString();
					obj.Name = Reader[3].ToString();
					obj.CompanyName = Reader[4].ToString();
					obj.Sex.ID = Reader[5].ToString();
					obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
					obj.SIMainInfo.EmplType = Reader[7].ToString();
					obj.SIMainInfo.User01 = Reader[8].ToString();
					obj.PID.PatientNO = Reader[9].ToString();
					obj.PVisit.MedicalType.ID = Reader[10].ToString();
					obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11].ToString());
					obj.SIMainInfo.InDiagnose.Name = Reader[12].ToString();
					obj.SIMainInfo.InDiagnose.ID = Reader[13].ToString();
					obj.PVisit.PatientLocation.Dept.ID = Reader[14].ToString();
					obj.PVisit.PatientLocation.Bed.ID = Reader[15].ToString();
					obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[16].ToString());
					obj.User01 = Reader[17].ToString();
					obj.User02 = Reader[18].ToString();
					obj.User03 = Reader[19].ToString();
					obj.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[20].ToString());

					al.Add(obj);
				}

				Reader.Close();
			}
			catch(Exception ex)
			{
				this.ErrCode = "-1";
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}

			return al;
		}
		/// <summary>
		///  ������Ժ�ǼǵĶ����־
		/// </summary>
		/// <param name="regNo">ҽ�����߾�ҽ��ˮ��</param>
		/// <param name="readFlag">���µı�־ 1 ���� 0 δ���� 2 ����</param>
		/// <param name="commit">�Ƿ�ֱ���ύ?</param>
		/// <returns>-1 ʧ�� 0 �ɹ�</returns>
		public int UpdateRegReadFlag(string regNo, int readFlag, bool commit)
		{
			string sql = "update his_zydj set DRBZ = " + readFlag.ToString() + " where jydjh = '" + regNo + "'";

			int tempRows = 0;

			tempRows = this.ExecNoQuery(sql);
			
			if(tempRows <= 0)
			{
				if(commit)
				{
					trans.Rollback();
				}
				return -1;
			}
			
			try
			{
				if(commit)
				{
					trans.Commit();
				}

				return tempRows;
			}
			catch(Exception ex)
			{
				this.ErrCode = "-1";
				this.Err = ex.Message;
				this.WriteErr();
				//trans.Rollback();
				return -1;
			}
			
		}
		#endregion

		#region ҽ����ϸ����

		/// <summary>
		/// ɾ���������ϴ�����ϸ(�����ٻ���)
		/// </summary>
		/// <param name="regNo">��ҽ�ǼǺ�</param>
		/// <returns></returns>
		public int DeleteItemList(string regNo)
		{
			string strSql = "delete from his_cfxm where JYDJH = " + "'" + regNo + "'";
	
			return this.ExecNoQuery(strSql);
		}


		/// <summary>
		/// ���뵥��ҽ����ϸ
		/// </summary>
		/// <param name="pInfo">סԺ���߻�����Ϣ,����ҽ��������Ϣ</param>
		/// <param name="obj">������ϸ��Ϣ</param>
		/// <returns></returns>
		public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList obj)
		{
			string sqlMaxNo = "select isnull(max(XMXH), 0) from his_cfxm where JYDJH = " + "'" + pInfo.SIMainInfo.RegNo + "'";
			int i = 1;

			if(this.ExecQuery(sqlMaxNo) == -1)
				return -1;

			while(Reader.Read())
			{
				i = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
			}

			Reader.Close();

			i++;
			
			//if(obj.Item.IsPharmacy)
            if(obj.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
			{
				#region delete by maokb
//				try
//				{
//                    //��ҩƷ����ת�����Զ�����
//					FS.HISFC.Models.Pharmacy.Item drugItem = new FS.HISFC.Models.Pharmacy.Item();
//					FS.HISFC.Management.Pharmacy.Item iMgr = new FS.HISFC.Management.Pharmacy.Item();
//					obj.Item = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
//					drugItem = iMgr.GetItem(obj.Item.ID);
//					obj.Item.ID = drugItem.UserCode;	
//				}
//				catch
//				{
//					this.Err = "��ȡҩƷ�Զ��������";
//					return -1;
//				}
//			}
//			else
//			{
//				try
//				{
//					//����ҩƷ����ת�����Զ�����
//					FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
//					FS.HISFC.BizLogic.Fee.Item.Undrug itemMgr= new FS.HISFC.BizLogic.Fee.Item.Undrug();
//					item = itemMgr.GetItem(obj.Item.ID);
//					obj.Item.ID = item.UserCode;
//				}
//				catch
//				{
//					this.Err = "��ȡ��ҩƷ�Զ��������";
//					return -1;
//				}
				#endregion
				obj.Item = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
			}
			
			//���ݺϷ����ж���Ҫ���������

			string sql = "insert into his_cfxm values('" + pInfo.SIMainInfo.RegNo + "','" +
														  pInfo.SIMainInfo.HosNo + "','" +
														  pInfo.IDCard + "','" + 
				                                          pInfo.PID.PatientNO + "','" +
				                                          pInfo.PVisit.InTime.ToString() + "','" + 
				                                          obj.FeeOper.OperTime.ToString() + "'," +
														  i.ToString() + ",'" + 
														  obj.Item.ID + "','" +
														  obj.Item.Name + "'," +
													      "0" + ",'" + 
													      obj.Item.Specs + "','" + 
													      "" + "'," +
													      (obj.Item.Price * obj.FTRate.OwnRate).ToString() + "," +
													      obj.Item.Qty.ToString() + "," +
													      obj.FT.TotCost.ToString() + ",'" +
													      "" + "','" + "" + "','" + "" + "'," + "0" + ",'" + "" + "')";
			if(this.ExecNoQuery(sql) == -1)
				return -1;

			return 0;	                                          
		}
		/// <summary>
		/// ѭ������ҽ����ϸ
		/// </summary>
		/// <param name="pInfo">���߻�����Ϣ,����ҽ����Ϣ</param>
		/// <param name="itemList">���߷�����ϸʵ������</param>
		/// <returns></returns>
		public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, ArrayList itemList)
		{
			foreach(FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in itemList)
			{
				if(this.InsertItemList(pInfo, obj) == -1)
					return -1;
			}
			return 0;
		}
		/// <summary>
		///  ���ݶ����־ ��ѯ�Ѵ��ݵ���Ŀ�б�
		/// </summary>
		/// <param name="regNo">���߾�ҽ�ǼǺ�</param>
		/// <param name="flag">0 δ���� 1 ���� 2 ����</param>
		/// <returns>Fee.Inpatient.FeeItemListʵ�弯��</returns>
		public ArrayList GetUnValidItemList(string regNo, int flag)
		{
			string sql = "select * from his_cfxm where JYDJH = '" + regNo + "' and DRBZ = " + flag.ToString();

			if(this.ExecQuery(sql) == -1)
				return null;

			while(Reader.Read())
			{
				FS.HISFC.Models.Fee.Inpatient.FeeItemList obj = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();


			}
			Reader.Close();
			
			return null;
		}

		#endregion

		#region ������Ϣ
		/// <summary>
		/// �õ�ҽ�����ߵĽ�����Ϣ
		/// ҽ��������ܽ�� pInfo.SIMainInfo.TotCost
		/// ҽ��������ʻ���� pInfo.SIMainInfo.PayCost
		/// ҽ�������ͳ���� pInfo.SIMainInfo.PubCost
		/// ҽ��������Էѽ�� pInfo.SIMainInfo.OwnCost
		/// ���� totcost = payCost + pubCost + ownCost;
		/// </summary>
		/// <param name="pInfo">���߻�����Ϣ,����ҽ�����߽����Ļ�����Ϣ</param>
		/// <returns> -1 ʧ�� 0 û�н�����Ϣ 1 �ɹ���ȡ</returns>
		public int GetBalanceInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
		{
			string sql = "select * from HIS_FYJS where JYDJH = '" + pInfo.SIMainInfo.RegNo + "'";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ�����߽�����Ϣʧ��!";
				return -1;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "û�л��߽�����Ϣ";
				return 0;
			}

			while(Reader.Read())
			{
				pInfo.SIMainInfo.RegNo = Reader[0].ToString();
				pInfo.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
				pInfo.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
				pInfo.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
				pInfo.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
				pInfo.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
				pInfo.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
				pInfo.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
				pInfo.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
				pInfo.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
				pInfo.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
				pInfo.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
				pInfo.SIMainInfo.Memo = Reader[16].ToString();
				pInfo.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
				pInfo.SIMainInfo.User01 = Reader[18].ToString();
				pInfo.SIMainInfo.User02 = Reader[19].ToString();
				pInfo.SIMainInfo.User03 = Reader[20].ToString();
				pInfo.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
			}

			Reader.Close();

			return 1;
		}
		/// <summary>
		/// ���½�����Ϣ�Ķ����־
		/// </summary>
		/// <param name="regNo">���߾�ҽ�ǼǺ�</param>
		/// <param name="readFlag"> 0 δ���� 1 �Ѷ��� 2 ����</param>
		/// <returns></returns>
		public int UpdateBalaceReadFlag(string regNo, int readFlag)
		{
			string sql = "update HIS_FYJS set DRBZ = " + readFlag.ToString() + " WHERE JYDJH = '" + regNo + "'";

			if(this.ExecNoQuery(sql) <= 0)
			{
				this.Err = "����ʧ��!";
				return -1;
			}

			return 0;
		}

		#endregion

		#region ������Ϣ
		/// <summary>
		/// ���ҽ��ҩƷ��Ŀ�б�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSIDrugList()
		{
			string sql = "select MEDI_CODE,MEDI_ITEM_TYPE,MEDI_NAME,MODEL,CODE_PY,STAT_TYPE,MT_FLAG,STAPLE_FLAG,isnull(SELF_SCALE,0) "
						 + "from view_medi" +
                         " where VALID_FLAG = '1'";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ��ҩƷĿ¼ʧ��!";
				return null;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "û��ҩƷ��Ϣ";
				return null;
			}

			ArrayList al = new ArrayList();

			FS.HISFC.Models.SIInterface.Item item = null;

			string sysClass = "";
			try
			{
				while(Reader.Read())
				{
					item = new FS.HISFC.Models.SIInterface.Item();

					item.ID = Reader[0].ToString();
					if(Reader[1].ToString() == "1")
					{
						sysClass = "X";
					}
					else
					{
						sysClass = "Z";
					}
					item.SysClass = sysClass;
					item.Name = Reader[2].ToString();
					item.DoseCode = Reader[3].ToString();
					item.SpellCode = (Reader[4].ToString()).Length > 9 ? (Reader[4].ToString()).Substring(0, 10): Reader[4].ToString();
					item.FeeCode = Reader[5].ToString();
					item.ItemType = Reader[6].ToString();
					item.ItemGrade = Reader[7].ToString();
					if(item.ItemGrade == "9")
					{
						item.ItemGrade = "3";
					}
					item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

					al.Add(item);
				}

				Reader.Close();

				return al;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
		}
		/// <summary>
		/// ���ҽ����ҩƷ��Ŀ�б�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSIUndrugList()
		{
			string sql = "select item_code ," + "'F" + "'" + " as sys_class, item_name,STAT_TYPE as fee_code, " +
                         "MT_FLAG as ITEM_TYPE, SELF_SCALE from view_item";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ����ҩƷĿ¼ʧ��!";
				return null;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "û�з�ҩƷ��Ϣ";
				return null;
			}

			ArrayList al = new ArrayList();

			FS.HISFC.Models.SIInterface.Item item = null;
//			FS.HISFC.Models.Base.SpellCode sp = null;
//			FS.HISFC.Management.Manager.Spell spell = new FS.HISFC.Management.Manager.Spell();
			try
			{
				while(Reader.Read())
				{
					item = new FS.HISFC.Models.SIInterface.Item();

					item.ID = Reader[0].ToString();
					item.SysClass = Reader[1].ToString();
					item.Name = Reader[2].ToString();
//					sp = (FS.HISFC.Models.Base.SpellCode)spell.Get(item.Name);
//					if(sp != null)
//					{
//						item.SpellCode = sp.SpellCode.Length > 9 ? sp.SpellCode.Substring(0,10) : sp.SpellCode;
//					}
					item.FeeCode = Reader[3].ToString();
					item.ItemType = Reader[4].ToString();
					item.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
					if(item.Rate == 0)
					{
						item.ItemGrade = "1";
					}
					else if(item.Rate == 1)
					{
						item.ItemGrade = "3";
					}
					else
					{
						item.ItemGrade = "2";
					}

					al.Add(item);
				}

				Reader.Close();

				return al;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
		}
		/// <summary>
		/// ��ҽ����������ȡҽ���Ѷ�����Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSICompareList()
		{
			string sql = "select item_code,item_name,match_type,hosp_code,hosp_name from view_match order by match_type";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ���Ѷ�����Ϣʧ��!";
				return null;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "���Ѷ�����Ϣ";
				return null;
			}

			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject info;
			try
			{
				while(Reader.Read())
				{
					info = new FS.FrameWork.Models.NeuObject();
					info.ID = this.Reader[0].ToString();
					info.Name = this.Reader[1].ToString();
					info.Memo = this.Reader[2].ToString();
					info.User01 = this.Reader[3].ToString();
					info.User02 = this.Reader[4].ToString();
					al.Add(info);
				}

				Reader.Close();

				return al;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
		}
		/// <summary>
		/// ��÷�ҩƷ�������ж��Ƿ���Ҫ������
		/// </summary>
		/// <returns></returns>
		public int GetSIUndrugCounts()
		{
			string sql = "select count(*) from view_item where VALID_FLAG = '1'";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ����ҩƷĿ¼ʧ��!";
				return -1;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "û�з�ҩƷ��Ϣ";
				return -1;
			}

			int count = 0;
			try
			{
				while(Reader.Read())
				{
					count = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
				}

				Reader.Close();

				return count;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return -1;
			}
		}
		/// <summary>
		/// ���ҩƷ�������ж��Ƿ���Ҫ������
		/// </summary>
		/// <returns></returns>
		public int GetSIDrugCounts()
		{
			string sql = "select count(*) from view_medi where VALID_FLAG = '1'";

			if(this.ExecQuery(sql) == -1)
			{
				this.ErrCode = "-1";
				this.Err = "��ѯҽ��ҩƷĿ¼ʧ��!";
				return -1;
			}

			if(!Reader.HasRows)
			{
				
				this.ErrCode = "-1";
				this.Err = "û��ҩƷ��Ϣ";
				return -1;
			}

			int count = 0;
			try
			{
				while(Reader.Read())
				{
					count = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
				}

				Reader.Close();

				return count;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return -1;
			}
		}

		#endregion
	}
}
