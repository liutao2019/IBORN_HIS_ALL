using System;
using System.Collections;
using Neusoft.HISFC.Object.Base;
using Neusoft.HISFC.Object.Terminal;

namespace Neusoft.HISFC.Management.Terminal
{
	/// <summary>
	/// MedTechItemArray <br></br>
	/// [��������: ҽ��ԤԼ]<br></br>
	/// [�� �� ��: ]<br></br>
	/// [����ʱ��: ]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2007��03��06'
	///		�޸�Ŀ��='�汾�ع�'
	///		�޸�����=''
	///  />
	/// </summary>
	public class Terminal : Neusoft.NFC.Management.Database
	{
		#region ˽�к���

		/// <summary>
		/// ��ʵ������Է���������
		/// </summary>
		/// <param name="medTechItem">ԤԼ��Ŀ</param>
		/// <returns>�ֶ�����</returns>
		private string [] GetParam(Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem)
		{
			string[] str = new string[]
						{
							medTechItem.ItemExtend.Dept.ID, 
							medTechItem.Item.ID,
							medTechItem.Item.Name,
							medTechItem.Item.SysClass.ID.ToString(),
							medTechItem.ItemExtend.UnitFlag,
							medTechItem.ItemExtend.BookLocate,//5
							medTechItem.ItemExtend.BookTime,
							medTechItem.ItemExtend.ExecuteLocate,
							medTechItem.ItemExtend.ReportTime,
							medTechItem.ItemExtend.HurtFlag,
							medTechItem.ItemExtend.SelfBookFlag,//10
							medTechItem.ItemExtend.ReasonableFlag,
							medTechItem.ItemExtend.Speciality,
							medTechItem.ItemExtend.ClinicMeaning,//-----
							medTechItem.ItemExtend.SimpleKind,
							medTechItem.ItemExtend.SimpleWay,//15
							medTechItem.ItemExtend.SimpleUnit,
							medTechItem.ItemExtend.SimpleQty.ToString(),
							medTechItem.ItemExtend.Container,
							medTechItem.ItemExtend.Scope,
							medTechItem.Item.Notice,//20
							medTechItem.Item.Oper.ID,
							medTechItem.Item.Oper.OperTime.ToString(),
							medTechItem.ItemExtend.MachineType,
							medTechItem.ItemExtend.BloodWay ,
							medTechItem.ItemExtend.Ext1,//25
							medTechItem.ItemExtend.Ext2,
							medTechItem.ItemExtend.Ext3,
						};
			return str;
		}

		/// <summary>
		/// ��ȡҽ��ԤԼ��Ϣ
		/// </summary>
		/// <param name="strSql">sql���</param>
		/// <returns>ҽ��ԤԼ��Ϣ</returns>
		private ArrayList QueryMedTechBookApply(string strSql)
		{
			// ҽ��ԤԼ��Ϣ����
			ArrayList MedTechBookApplyList = new ArrayList();
			// ҽ��ԤԼ��Ϣ
			Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply = null;
			//
			// ִ�в�ѯ
			//
			if (this.ExecQuery(strSql) == -1)
			{
				this.Err = "ִ�в�ѯʧ��!" + this.Err;
				return null;
			}
			// �γ�����
			while (this.Reader.Read())
			{
				medTechBookApply = new Neusoft.HISFC.Object.Terminal.MedTechBookApply();

				// ������ˮ��
				medTechBookApply.ItemList.ID = this.Reader[0].ToString();
				// ��������
				medTechBookApply.ItemList.TransType = Neusoft.HISFC.Object.Base.TransTypes.Positive;
				// ���￨��
				medTechBookApply.ItemList.Patient.PID.CardNO = this.Reader[2].ToString();
				// ����
				medTechBookApply.ItemList.User02 = this.Reader[3].ToString();
				// ����
				medTechBookApply.ItemList.User01 = this.Reader[4].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.ID = this.Reader[5].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.Name = this.Reader[6].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.Item.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��λ��ʶ
				medTechBookApply.ItemExtend.UnitFlag = this.Reader[8].ToString();
				// ������
				medTechBookApply.ItemList.RecipeNO = this.Reader[9].ToString();
				// ��������Ŀ���
				medTechBookApply.ItemList.SequenceNO = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[10].ToString());
				// ������������
				medTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[11].ToString();
				// ���Һ�
				medTechBookApply.ItemList.ExecOper.Dept.ID = this.Reader[12].ToString();
				// ��������
				medTechBookApply.ItemList.ExecOper.Dept.Name = this.Reader[13].ToString();
				// 0 ԤԤԼ 1 ��Ч 2 ���
				medTechBookApply.MedTechBookInfo.Status = this.Reader[14].ToString();
				// ԤԼ����
				medTechBookApply.MedTechBookInfo.BookID = this.Reader[15].ToString();
				// ԤԼʱ��
				medTechBookApply.MedTechBookInfo.BookTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[16].ToString());
				// ���
				medTechBookApply.Noon.ID = this.Reader[17].ToString();
				// ֪��ͬ����
				medTechBookApply.ItemExtend.ReasonableFlag = this.Reader[18].ToString();
				// ����״̬
				medTechBookApply.HealthFlag = this.Reader[19].ToString();
				// ִ�еص�
				medTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[20].ToString();
				// ȡ����ʱ��
				medTechBookApply.ReportTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[21].ToString());
				// �д�/�޴� 
				medTechBookApply.ItemExtend.HurtFlag = this.Reader[22].ToString();
				// �걾��λ
				medTechBookApply.ItemExtend.SimpleKind = this.Reader[23].ToString();
				// ��������
				medTechBookApply.ItemExtend.SimpleWay = this.Reader[24].ToString();
				// ע������
				medTechBookApply.Memo = this.Reader[25].ToString();
				// ˳���
				medTechBookApply.SortID = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[26].ToString());
				// ����Ա
				this.Operator.ID = this.Reader[27].ToString();
				// �������� 
				medTechBookApply.ItemList.Order.DoctorDept.ID = this.Reader[28].ToString();
				// ��������
				medTechBookApply.ItemList.Order.ID = this.Reader[30].ToString();

				MedTechBookApplyList.Add(medTechBookApply);
			}
			// ����
			return MedTechBookApplyList;
		}

		/// <summary>
		/// ���ҽ��ԤԼ���뵥��
		/// </summary>
		/// <returns>ҽ��ԤԼ���뵥��</returns>
		private string GetMedTechBookApplyID()
		{
			return this.GetSequence("Met.GetMedTechBookApplyID");
		}

		/// <summary>
		/// ��ȡҽ��ԤԼ����Ŀ�ÿ��Ҹ�ʱ���������ID��
		/// </summary>
		/// <returns>���ID��</returns>
		private string GetMedTechBookApplySortID(string itemCode, string deptCode, System.DateTime bookDate, string noonCode)
		{
			return "";
		}

		/// <summary>
		/// ת��ҽ��ԤԼʵ��Ϊ�ֶ�����
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ</param>
		/// <returns>�ֶ�����</returns>
		private string[] GetParam(Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply)
		{
			string[] str = new string[]{	// ������ˮ��
											medTechBookApply.ItemList.ID,
											// ��������
											"1",
											// ���￨��
											medTechBookApply.ItemList.Patient.PID.CardNO,
											// ����
											medTechBookApply.ItemList.User02,
											// ����
											"1",
											// ��Ŀ����
											medTechBookApply.ItemList.ID,
											// ��Ŀ����
											medTechBookApply.ItemList.Name,
											// ��Ŀ����
											medTechBookApply.ItemList.Item.Qty.ToString(),
											// ��λ��ʶ
											medTechBookApply.ItemExtend.UnitFlag,
											// ������
											medTechBookApply.ItemList.RecipeNO,
											// ��������Ŀ���
											medTechBookApply.ItemList.SequenceNO.ToString(),
											//������������
											medTechBookApply.ItemList.Order.DoctorDept.Name,
											// ���Һ�
											medTechBookApply.ItemList.ExecOper.Dept.ID,
											// ��������
											medTechBookApply.ItemList.ExecOper.Dept.Name,
											// 0 ԤԤԼ 1 ��Ч 2 ����
											medTechBookApply.MedTechBookInfo.Status,
											// ԤԼ����
											medTechBookApply.MedTechBookInfo.BookID,
											// ԤԼʱ��
											medTechBookApply.MedTechBookInfo.BookTime.ToString(),
											// ���
											medTechBookApply.Noon.ID,
											// ֪��ͬ����
											medTechBookApply.ItemExtend.ReasonableFlag,
											// ����״̬
											medTechBookApply.HealthFlag,
											// ִ�еص�
											medTechBookApply.ItemList.Order.DoctorDept.Name,
											// ȡ����ʱ��
											medTechBookApply.ReportTime.ToString(),
											// �д�/�޴�
											medTechBookApply.ItemExtend.HurtFlag,
											// �걾��λ
											medTechBookApply.ItemExtend.SimpleKind,
											// ��������
											medTechBookApply.ItemExtend.SimpleWay,
											// ע������
											medTechBookApply.Memo,
											// ˳���
											medTechBookApply.SortID.ToString(),
											// ����Ա
											this.Operator.ID,
											// ��������
											medTechBookApply.ItemList.Order.DoctorDept.ID,
											//��������
											System.DateTime.Now.ToString(),
											//ҽ����ˮ��
											medTechBookApply.ItemList.Order.ID
									   };
			// ����
			return str;
		}

		/// <summary>
		/// ��ȡ��Ϣ
		/// </summary>
		/// <param name="strSql">SQL���</param>
		/// <returns>ҽ��ԤԼ��Ϣ</returns>
		private ArrayList MyGetDetailApply(string strSql)
		{
			ArrayList detailList = new ArrayList();
			Neusoft.HISFC.Object.Terminal.MedTechBookApply tempMedTechBookApply = null;
			this.ExecQuery(strSql);
			while (this.Reader.Read())
			{
				tempMedTechBookApply = new MedTechBookApply();
				// ������ˮ��
				tempMedTechBookApply.ItemList.ID = this.Reader[0].ToString();
				// ��������
				tempMedTechBookApply.ItemList.TransType = Neusoft.HISFC.Object.Base.TransTypes.Positive;
				// ���￨��
				tempMedTechBookApply.ItemList.Patient.PID.CardNO = this.Reader[2].ToString();
				// ����
				tempMedTechBookApply.ItemList.User02 = this.Reader[3].ToString();
				// ����
				tempMedTechBookApply.ItemList.User01 = this.Reader[4].ToString();
				// ��Ŀ���� 
				tempMedTechBookApply.ItemList.Item.ID = this.Reader[5].ToString();
				// ��Ŀ����
				tempMedTechBookApply.ItemList.Item.Name = this.Reader[6].ToString();
				// ��Ŀ����
				tempMedTechBookApply.ItemList.Item.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��λ��ʶ
				tempMedTechBookApply.ItemExtend.UnitFlag = this.Reader[8].ToString();
				// ������
				tempMedTechBookApply.ItemList.RecipeNO = this.Reader[9].ToString();
				// ��������Ŀ���
				tempMedTechBookApply.ItemList.SequenceNO = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[10].ToString());
				// ������������
				tempMedTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[11].ToString();
				// ���Һ�
				tempMedTechBookApply.ItemList.ExecOper.Dept.ID = this.Reader[12].ToString();
				// ��������
				tempMedTechBookApply.ItemList.ExecOper.Dept.Name = this.Reader[13].ToString();
				// 0 ԤԤԼ 1 ��Ч 2 ���
				tempMedTechBookApply.MedTechBookInfo.Status = this.Reader[14].ToString();
				// ԤԼ����
				tempMedTechBookApply.MedTechBookInfo.BookID = this.Reader[15].ToString();
				// ԤԼʱ��
				tempMedTechBookApply.MedTechBookInfo.BookTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[16].ToString());
				// ���
				tempMedTechBookApply.Noon.ID = this.Reader[17].ToString();
				// ֪��ͬ����
				tempMedTechBookApply.ItemExtend.ReasonableFlag = this.Reader[18].ToString();
				// ����״̬
				tempMedTechBookApply.HealthFlag = this.Reader[19].ToString();
				// ִ�еص�
				tempMedTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[20].ToString();
				// ȡ����ʱ��
				tempMedTechBookApply.ReportTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[21].ToString());
				// �д�/�޴�
				tempMedTechBookApply.ItemExtend.HurtFlag = this.Reader[22].ToString();
				// �걾��λ
				tempMedTechBookApply.ItemExtend.SimpleKind = this.Reader[23].ToString();
				// ��������
				tempMedTechBookApply.ItemExtend.SimpleWay = this.Reader[24].ToString();
				// ע������
				tempMedTechBookApply.Memo = this.Reader[25].ToString();
				// ˳���
				tempMedTechBookApply.SortID = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[26].ToString());
				// ����Ա
				tempMedTechBookApply.User01 = this.Reader[27].ToString();
				// ��������
				tempMedTechBookApply.ItemList.Order.DoctorDept.ID = this.Reader[28].ToString();
				// ��������
				tempMedTechBookApply.ItemList.Order.ID = this.Reader[30].ToString();
				// ��Ӧ��Ŀ
				tempMedTechBookApply.ItemComparison.ID = this.Reader[31].ToString();
				
				detailList.Add(tempMedTechBookApply);
			}
			return detailList;
		}
				
		#endregion

		#region ���к���

		/// <summary>
		/// ԤԼ��Ŀ���������ά�� ����
		/// </summary>
		/// <param name="medTechItem">ԤԼ��Ŀ</param>
		/// <returns>Ӱ�����������1��ʧ��</returns>
		public int InsertMedTechItem(Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem)
		{
			// SQL���
			string sql = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.InsertDeptItem", ref sql) == -1)
			{
				this.Err = "��ȡSQL���Terminal.DeptItem.InsertDeptItemʧ��";
				return -1;
			}
			// ƥ��ִ��SQL���
			try
			{
				sql = string.Format(sql, GetParam(medTechItem));

				return this.ExecNoQuery(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		///  ԤԼ��Ŀ���������ά�� ����
		/// </summary>
		/// <param name="medTechItem">ԤԼ��Ŀ</param>
		/// <returns>Ӱ�����������1��ʧ��</returns>
		public int UpdateMedTechItem(Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem)
		{
			// SQL���
			string sql = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.UpdateDeptItem", ref sql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.UpdateDeptItemʧ��";
				return -1;
			}
			// ƥ��ִ��
			try
			{
				sql = string.Format(sql, GetParam(medTechItem));
				return this.ExecNoQuery(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ���ݿ��ұ������Ŀ����ɾ��ԤԼ��Ŀ
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int DeleteMedTechItem(string deptCode, string itemCode)
		{
			// SQL���
			string strSql = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.DelDeptItem", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.DelDeptItemʧ��";
				return -1;
			}
			// ƥ��ִ��
			try
			{
				strSql = string.Format(strSql, deptCode, itemCode);
				
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ���ݿ��Ҵ������Ŀ����Ĳ���ԤԼ��Ŀ
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>ԤԼ��Ŀ</returns>
		public Neusoft.HISFC.Object.Terminal.MedTechItem GetMedTechItem(string deptCode, string itemCode)
		{
			// ԤԼ��Ŀ
			Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			// SQL���
			string strSql = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.SelectDeptItem", ref strSql) == -1)
			{
				return null;
			}
			try
			{
				// ƥ��SQL���
				strSql = string.Format(strSql, deptCode, itemCode);
				// ִ��SQL���
				this.ExecQuery(strSql);
				
				while (this.Reader.Read())
				{
					medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					
					medTechItem.ItemExtend.Dept.ID = this.Reader[2].ToString();
					medTechItem.Item.ID = this.Reader[3].ToString();
					medTechItem.Item.Name = this.Reader[4].ToString();
					medTechItem.Item.SysClass.ID = this.Reader[5].ToString();
					medTechItem.ItemExtend.UnitFlag = this.Reader[6].ToString();
					medTechItem.ItemExtend.BookLocate = this.Reader[7].ToString();
					medTechItem.ItemExtend.BookTime = this.Reader[8].ToString();
					medTechItem.ItemExtend.ExecuteLocate = this.Reader[9].ToString();
					medTechItem.ItemExtend.ReportTime = this.Reader[10].ToString();
					medTechItem.ItemExtend.HurtFlag = this.Reader[11].ToString();
					medTechItem.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					medTechItem.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					medTechItem.ItemExtend.Speciality = this.Reader[14].ToString();
					medTechItem.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					medTechItem.ItemExtend.SimpleKind = this.Reader[16].ToString();
					medTechItem.ItemExtend.SimpleWay = this.Reader[17].ToString();
					medTechItem.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					medTechItem.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					medTechItem.ItemExtend.Container = this.Reader[20].ToString();
					medTechItem.ItemExtend.Scope = this.Reader[21].ToString();
					medTechItem.Item.Notice = this.Reader[22].ToString();
					medTechItem.Item.Oper.ID = this.Reader[23].ToString();
					medTechItem.Item.Oper.OperTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					medTechItem.ItemExtend.MachineType = this.Reader[25].ToString();
					medTechItem.ItemExtend.BloodWay = this.Reader[26].ToString();
					medTechItem.ItemExtend.Ext1 = this.Reader[27].ToString();
					medTechItem.ItemExtend.Ext2 = this.Reader[28].ToString();
					medTechItem.ItemExtend.Ext3 = this.Reader[29].ToString();
				}
				Reader.Close();
				
				return medTechItem;
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		///  ������Ŀ�������һ����Ŀ��չ��Ϣ
		/// </summary>
		/// <param name="itemCode">��Ŀ����</param>
		/// <returns>��Ŀ��չ��Ϣ</returns>
		public Neusoft.HISFC.Object.Terminal.MedTechItem GetMedTechItem(string itemCode)
		{
			// ��Ŀ��չ��Ϣ
			Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			// ��Ŀ��չ��Ϣ����
			ArrayList medTechItemList = new ArrayList();
			// SQL���
			string strSql = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.SelectDeptItem", ref strSql) == -1)
			{
				return null;
			}
			try
			{
				// ƥ��SQL���
				strSql = string.Format(strSql, itemCode);
				// ִ��SQL���
				this.ExecQuery(strSql);
				
				while (this.Reader.Read())
				{
					medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					
					medTechItem.ItemExtend.Dept.ID = this.Reader[2].ToString();
					medTechItem.Item.ID = this.Reader[3].ToString();
					medTechItem.Item.Name = this.Reader[4].ToString();
					medTechItem.Item.SysClass.ID = this.Reader[5].ToString();
					medTechItem.ItemExtend.UnitFlag = this.Reader[6].ToString();
					medTechItem.ItemExtend.BookLocate = this.Reader[7].ToString();
					medTechItem.ItemExtend.BookTime = this.Reader[8].ToString();
					medTechItem.ItemExtend.ExecuteLocate = this.Reader[9].ToString();
					medTechItem.ItemExtend.ReportTime = this.Reader[10].ToString();
					medTechItem.ItemExtend.HurtFlag = this.Reader[11].ToString();
					medTechItem.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					medTechItem.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					medTechItem.ItemExtend.Speciality = this.Reader[14].ToString();
					medTechItem.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					medTechItem.ItemExtend.SimpleKind = this.Reader[16].ToString();
					medTechItem.ItemExtend.SimpleWay = this.Reader[17].ToString();
					medTechItem.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					medTechItem.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					medTechItem.ItemExtend.Container = this.Reader[20].ToString();
					medTechItem.ItemExtend.Scope = this.Reader[21].ToString();
					medTechItem.Item.Notice = this.Reader[22].ToString();
					medTechItem.Item.Oper.ID = this.Reader[23].ToString();
					medTechItem.Item.Oper.OperTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					medTechItem.ItemExtend.MachineType = this.Reader[25].ToString();
					medTechItem.ItemExtend.BloodWay = this.Reader[26].ToString();
					medTechItem.ItemExtend.Ext1 = this.Reader[27].ToString();
					medTechItem.ItemExtend.Ext2 = this.Reader[28].ToString();
					medTechItem.ItemExtend.Ext3 = this.Reader[29].ToString();
					
					medTechItemList.Add(medTechItem);
				}
				Reader.Close();

				if (medTechItemList.Count > 0)
				{
					return medTechItemList[0] as Neusoft.HISFC.Object.Terminal.MedTechItem;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ���ݿ��Ҵ�������������Ѿ�ά������Ŀ
		/// </summary>
		/// <param name="deptCode"></param>
		/// <returns></returns>
		public ArrayList QueryMedTechItem(string deptCode)
		{
			// SQL���
			string strSql = "";
			// ��Ŀ����
			ArrayList deptItemList = new ArrayList();
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.query.1", ref strSql) == -1)
			{
				return null;
			}
			//
			// ƥ��SQL���
			//
			try
			{
				strSql = string.Format(strSql, deptCode);
			}
			catch (Exception e)
			{
				this.Err = "ƥ��SQL���MedTech.DeptItem.query.1ʧ��!" + e.Message;
				this.ErrCode = e.Message;
				WriteErr();
				return null;
			}
			try
			{
				// ִ��SQL���
				if (this.ExecQuery(strSql) == -1)
				{
					return null;
				}
				
				while (this.Reader.Read())
				{
					Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					
					medTechItem.ItemExtend.Dept.ID = this.Reader[2].ToString();
					medTechItem.Item.ID = this.Reader[3].ToString();
					medTechItem.Item.Name = this.Reader[4].ToString();
					medTechItem.Item.SysClass.ID = this.Reader[5].ToString();
					medTechItem.ItemExtend.UnitFlag = this.Reader[6].ToString();
					medTechItem.ItemExtend.BookLocate = this.Reader[7].ToString();
					medTechItem.ItemExtend.BookTime = this.Reader[8].ToString();
					medTechItem.ItemExtend.ExecuteLocate = this.Reader[9].ToString();
					medTechItem.ItemExtend.ReportTime = this.Reader[10].ToString();
					medTechItem.ItemExtend.HurtFlag = this.Reader[11].ToString();
					medTechItem.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					medTechItem.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					medTechItem.ItemExtend.Speciality = this.Reader[14].ToString();
					medTechItem.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					medTechItem.ItemExtend.SimpleKind = this.Reader[16].ToString();
					medTechItem.ItemExtend.SimpleWay = this.Reader[17].ToString();
					medTechItem.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					medTechItem.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					medTechItem.ItemExtend.Container = this.Reader[20].ToString();
					medTechItem.ItemExtend.Scope = this.Reader[21].ToString();
					medTechItem.Item.Notice = this.Reader[22].ToString();
					medTechItem.Item.Oper.ID = this.Reader[23].ToString();
					medTechItem.Item.Oper.OperTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					medTechItem.ItemExtend.MachineType = this.Reader[25].ToString();
					medTechItem.ItemExtend.BloodWay = this.Reader[26].ToString();
					medTechItem.ItemExtend.Ext1 = this.Reader[27].ToString();
					medTechItem.ItemExtend.Ext2 = this.Reader[28].ToString();
					medTechItem.ItemExtend.Ext3 = this.Reader[29].ToString();
					
					deptItemList.Add(medTechItem);
				}
				Reader.Close();
			}
			catch (Exception e)
			{
				this.Err = e.Message;
				this.ErrCode = e.Message;
				if (Reader.IsClosed == false)
				{
					Reader.Close();
				}
				WriteErr();
				return null;
			}
			// ���ؽ��
			return deptItemList;
		}

		/// <summary>
		/// ��ѯ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="execDept">ִ�п���</param>
		/// <param name="beginDate">��ʼʱ��</param>
		/// <param name="endDate">����ʱ��</param>
		/// <param name="clinicNO">����Ż򿨺� </param>
		/// <param name="codeType">���� ClinicN0 1 ���� 2 �����</param>
		/// <returns>ԤԼ��Ŀ����</returns>
		public ArrayList QueryTerminalApply(string execDept, System.DateTime beginDate, System.DateTime endDate, string clinicNO, string codeType)
		{
			// ��ʼʱ��
			string dateBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
			// ��ֹʱ��
			string dateEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";
			// SQL���
			string sql = "";
			// Where����
			string where = "";
			try
			{
				// ��ȡSQL���
				if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList", ref sql) == -1)
				{
					this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyListʧ��";
					return null;
				}
				// ���ݲ�ͨ�Ĳ�ѯ���ͻ�ȡ��ͨ��Where����
				if (codeType == "2")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList.Where.2", ref where) == -1)
					{
						this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyList.Where.2ʧ��";
						return null;
					}
				}
				else if (codeType == "1")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList.Where.1", ref where) == -1)
					{
						this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyList.Where.1ʧ��";
						return null;
					}
				}
				// ����Ҫִ�е�SQL���
				sql = sql + where;
				sql = string.Format(sql, execDept, dateBegin, dateEnd, clinicNO);
				// ִ�в�����
				return this.QueryMedTechBookApply(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ȡĳ����ˮ�ŵ�ԤԼ��Ϣ
		/// </summary>
		/// <param name="sequenceNO">��ĿԤԼ��ˮ��</param>
		/// <returns>ԤԼ��Ϣ����</returns>
		public ArrayList QueryTerminalApply(string sequenceNO)
		{
			// SQL���
			string sql = "";
			// Where����
			string where = "";
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList", ref sql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyListʧ��";
				return null;
			}
			if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList.Where.SequenceNo", ref where) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyList.Where.SequenceNoʧ��";
				return null;
			}
			sql = sql + where;
			// ƥ��ִ��
			try
			{
				sql = string.Format(sql, sequenceNO);

				return this.QueryMedTechBookApply(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="itemCode">ִ�п���</param>
		/// <param name="beginDate">��ʼʱ��</param>
		/// <param name="endDate">����ʱ��</param>
		/// <returns>ԤԼ��Ŀ��Ϣ</returns>
		public ArrayList QueryTerminalApply(string itemCode, System.DateTime beginDate, System.DateTime endDate)
		{
			// ��ʼʱ��
			string dateBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
			// ����ʱ��
			string dateEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";
			// sql���
			string sql = "";
			// Where����
			string where = "";
			//
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList", ref sql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyListʧ��";
				return null;
			}
			if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList.Where.3", ref where) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetTerminalApplyList.Where.3ʧ��";
				return null;
			}
			sql = sql + where;
			//
			// ƥ��ִ��
			//
			try
			{
				sql = string.Format(sql, itemCode, dateBegin, dateEnd);

				return this.QueryMedTechBookApply(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ҽ��ԤԼ����
		/// </summary>
		/// <param name="feeItemList">�������</param>
		/// <param name="transaction">����</param>
		/// <returns>-1��ʧ�ܣ�</returns>
		public int MedTechApply(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList feeItemList)
		{
			// ҽ��ԤԼ��Ŀ
			Neusoft.HISFC.Object.Terminal.MedTechItem medTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			// ���Ʋ���ҵ��
			Neusoft.HISFC.Management.Manager.Controler controler = new Neusoft.HISFC.Management.Manager.Controler();
			// ҽ��ԤԼ����
			Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply = new Neusoft.HISFC.Object.Terminal.MedTechBookApply();
			//
			//���ݿ��Һ���Ŀ�õ�ҽ����Ŀ��չ��Ϣ
			//
			medTechItem = this.GetMedTechItem(feeItemList.ExecOper.Dept.ID, feeItemList.ID);
			if (medTechItem == null)
			{
				return -1;
			}
			// ���������Ϣ
			medTechBookApply.ItemList = feeItemList;
			// ��չ��Ϣ
			medTechBookApply.ItemExtend = medTechItem.ItemExtend; 
			//
			// ��ȡĳ����ˮ�ŵ�ԤԼ��Ϣ
			//
			ArrayList metTechBookApplyList = QueryTerminalApply(medTechBookApply.ItemList.Order.ID);
			if (metTechBookApplyList == null)
			{
				return -1;
			}
			//
			// û������ִ�в���
			//
			if (metTechBookApplyList.Count == 0) 
			{
				if (this.InsertMedTechBookApply(medTechBookApply) <= 0)
				{
					return -1;
				}
			}
			else // ������Ҫ����
			{
				if (UpdateMedTechBookApply(medTechBookApply) <= 0)
				{
					return -1;
				}
			}
			//
			// �ж��Ƿ������ͬʱ�������
			//
			if (controler.QueryControlerInfo("300013") == "1") 
			{
				// ����ͬʱ���
				if (this.AuditingMedTechBookApply(feeItemList) <= 0)
				{
					return -1;
				}
			}
			//
			// �ɹ�����
			//
			return 0;
		}

		/// <summary>
		/// ҽ��ԤԼ��׼
		/// </summary>
		/// <param name="feeItemList">�������</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int AuditingMedTechBookApply(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList feeItemList)
		{
			return this.UpdateMedTechBookApplyFlag(feeItemList, "1");
		}

		/// <summary>
		/// ԤԼ����
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ����</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int PlanMedTechBookApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply)
		{
			// sql���
			string sql = "";
			if (this.Sql.GetSql("Met.PlanTerminalBook", ref sql) == -1)
			{
				this.Err = "��ȡsql���Met.PlanTerminalBookʧ��";
				return -1;
			}
			//
			// ƥ��ִ��
			//
			try
			{
				medTechBookApply.MedTechBookInfo.BookID = GetMedTechBookApplyID();
				medTechBookApply.MedTechBookInfo.Status = "2";
				
				sql = string.Format(sql, this.GetParam(medTechBookApply));
				
				return this.ExecNoQuery(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ҽ��ԤԼȡ��
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ����</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int CancelMedTechBookApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply)
		{
			// sql���
			string sql = "";
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("Met.CancelMedTechBookApply", ref sql) == -1)
			{
				this.Err = "��ȡsql���Met.CancelMedTechBookApplyʧ��";
				return -1;
			}
			//
			// ƥ��ִ��
			//
			try
			{
				sql = string.Format(sql, medTechBookApply.MedTechBookInfo.BookID, "1");
				
				return this.ExecNoQuery(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ɾ��ԤԼ��Ϣ
		/// </summary>
		/// <param name="sequenceNO">ҽ��ԤԼ��ˮ��</param>
		/// <returns>-1 ���� 1 ɾ���ɹ�</returns>
		public int DeleteMedTechBookApply(string sequenceNO)
		{
			// ҽ��ԤԼ��Ŀ
			ArrayList medTechBookApplyList = QueryTerminalApply(sequenceNO);
			// sql���
			string strSql = "";
			//
			// �Ƿ����
			//
			if (medTechBookApplyList == null)
			{
				return -1;
			}
			if (medTechBookApplyList.Count == 0)
			{
				return 1;
			}
			//
			// �Ƿ��Ѿ�����
			//
			Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply = (Neusoft.HISFC.Object.Terminal.MedTechBookApply)medTechBookApplyList[0];
			if (medTechBookApply.MedTechBookInfo.Status == "2")
			{
				this.Err = "�������� ,����ȥȡ������";
				return -1;
			}
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.GetTerminalApplyList.DeleteArray", ref strSql) == -1)
			{
				this.Err = "��ȡsql���MedTech.DeptItem.GetTerminalApplyList.DeleteArrayʧ��";
				return -1;
			}
			// ƥ��sql���
			strSql = string.Format(strSql, sequenceNO);
			// ִ�з���
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ����ҽ��ԤԼ
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ��Ϣ</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		private int InsertMedTechBookApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply)
		{
			// sql���
			string sql = "";
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("Met.CreateMedTechBookApply", ref sql) == -1)
			{
				this.Err = "��ȡsql���Met.CreateMedTechBookApplyʧ��";
				return -1;
			}
			//
			// ƥ��ִ��
			//
			try
			{
				sql = string.Format(sql, this.GetParam(medTechBookApply));
				
				return this.ExecNoQuery(sql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ����ҽ��ԤԼ
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ��Ϣ</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		private int UpdateMedTechBookApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply medTechBookApply)
		{
			// sql���
			string strSql = "";
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("Met.UpdateMedTechBookApply", ref strSql) == -1)
			{
				this.Err = "��ȡsql���Met.UpdateMedTechBookApplyʧ��";
				return -1;
			}
			//
			// ƥ��ִ��
			//
			try
			{
				strSql = string.Format(strSql, this.GetParam(medTechBookApply));
				
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ����ԤԼ���еı�־
		/// </summary>
		/// <param name="feeItemList"> �������</param>
		/// <param name="flagType"> ��־���ͣ�0 ԤԤԼ 1 ��Ч 2 ���</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		private int UpdateMedTechBookApplyFlag(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList feeItemList, string flagType)
		{
			// sql���
			string strSql = "";
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("Met.AffirmMedTechBookApply", ref strSql) == -1)
			{
				this.Err = "��ȡsql���Met.AffirmMedTechBookApplyʧ��";
				return -1;
			}
			//
			// ƥ��ִ��
			//
			try
			{
				strSql = string.Format(strSql, feeItemList.ID, feeItemList.RecipeNO, feeItemList.SequenceNO, flagType);
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ����ҽ��ԤԼ������ϸ��
		/// </summary>
		/// <param name="objMedTechBookApply">ҽ��ԤԼ������ϸ</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int InsertMedTechApplyDetailInfo(Neusoft.HISFC.Object.Terminal.MedTechBookApply objMedTechBookApply)
		{
			string strSql = "";
			if (this.Sql.GetSql("Met.InsertMedTechApplyDetailInfo", ref strSql) == -1)
			{
				return -1;
			}
			try
			{
				strSql = string.Format(strSql, GetDetailParamApply(objMedTechBookApply));
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ��ȡ����ԤԼ��ϸ����ֶβ���
		/// </summary>
		/// <param name="tempMedTechBookApply">ҽ��ԤԼ��ϸ</param>
		/// <returns></returns>
		private string[] GetDetailParamApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply tempMedTechBookApply)
		{
			string[] stringArray = new string[]{
			                                   // ������ˮ��
											   tempMedTechBookApply.ItemList.ID,
											   // ��������
											   "1",
											   // ���￨��
											   tempMedTechBookApply.ItemList.Patient.PID.CardNO,
											   // ����
											   tempMedTechBookApply.ItemList.User02,
											   // ����
											   "1",
											   // ��Ŀ����
											   tempMedTechBookApply.ItemList.ID,
											   // ��Ŀ����
											   tempMedTechBookApply.ItemList.Name,
											   // ��Ŀ����
											   tempMedTechBookApply.ItemList.Item.Qty.ToString(),
											   // ��λ��ʶ
											   tempMedTechBookApply.ItemExtend.UnitFlag,
											   // ������
											   tempMedTechBookApply.ItemList.RecipeNO,
											   // ��������Ŀ���
											   tempMedTechBookApply.ItemList.SequenceNO.ToString(),
											   //������������
											   tempMedTechBookApply.ItemList.Order.DoctorDept.Name,
											   // ���Һ�
											   tempMedTechBookApply.ItemList.ExecOper.Dept.ID,
											   // ��������
											   tempMedTechBookApply.ItemList.ExecOper.Dept.Name,
											   // 0 ԤԤԼ 1 ��Ч 2 ����
											   tempMedTechBookApply.MedTechBookInfo.Status,
											   // ԤԼ����
											   tempMedTechBookApply.MedTechBookInfo.BookID,
											   // ԤԼʱ��
											   tempMedTechBookApply.MedTechBookInfo.BookTime.ToString(),
											   // ���
											   tempMedTechBookApply.Noon.ID,
											   // ֪��ͬ����
											   tempMedTechBookApply.ItemExtend.ReasonableFlag,
											   // ����״̬
											   tempMedTechBookApply.HealthFlag,
											   // ִ�еص�
											   tempMedTechBookApply.ItemList.Order.DoctorDept.Name,
											   // ȡ����ʱ��
											   tempMedTechBookApply.ReportTime.ToString(),
											   // �д�/�޴�
											   tempMedTechBookApply.ItemExtend.HurtFlag,
											   // �걾��λ
											   tempMedTechBookApply.ItemExtend.SimpleKind,
											   // ��������
											   tempMedTechBookApply.ItemExtend.SimpleWay,
											   // ע������
											   tempMedTechBookApply.Memo,
											   // ˳���
											   tempMedTechBookApply.SortID.ToString(),
											   // ����Ա
											   this.Operator.ID,
											   // ��������
											   tempMedTechBookApply.ItemList.Order.DoctorDept.ID,
											   // ��������
											   System.DateTime.Now.ToString(),
											   tempMedTechBookApply.ItemList.Order.ID,
											   tempMedTechBookApply.ItemComparison.ID
									   };
			return stringArray;
		}

		/// <summary>
		/// ���� met_tec_apply���Ѱ�������
		/// </summary>
		/// <param name="ApplyNum">�Ѱ�������</param>
		/// <param name="tempMedTechBookApply">ҽ��ԤԼ</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int UpdateApplyNum(int ApplyNum, Neusoft.HISFC.Object.Terminal.MedTechBookApply tempMedTechBookApply)
		{
			//���±� met_tec_apply���Ѱ�������
			string strSQL = "";
			
			//ȡ���²�����SQL���
			if (this.Sql.GetSql("MedTech.ItemArray.UpdateApplyNum", ref strSQL) == -1)
			{
				this.Err = "û���ҵ�MedTech.ItemArray.UpdateApplyNum�ֶ�!";
				return -1;
			}
			try
			{
				strSQL = string.Format(strSQL, ApplyNum, tempMedTechBookApply.ItemList.ID, tempMedTechBookApply.ItemList.RecipeNO, tempMedTechBookApply.ItemList.SequenceNO);
			}
			catch (Exception ex)
			{
				this.Err = "��ʽ��SQL���ʱ����MedTech.ItemArray.Update:" + ex.Message;
				this.WriteErr();
				return -1;
			}
			
			return this.ExecNoQuery(strSQL);
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="execDept">ִ�п���</param>
		/// <param name="beginDate">��ʼʱ��</param>
		/// <param name="endDate">����ʱ��</param>
		/// <param name="clinicN0">����Ż򿨺� </param>
		/// <param name="codeType">���� ClinicN0 1 ���� 2 �����</param>
		/// <returns></returns>
		public ArrayList QueryMedTechApplyList(string execDept, System.DateTime beginDate, System.DateTime endDate, string clinicN0, string codeType)
		{
			try
			{
				string strBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
				string strEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";

				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList", ref strSql) == -1)
				{
					return null;
				}
				if (codeType == "2")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.2", ref strSqlWhere) == -1)
					{
						return null;
					}
				}
				else if (codeType == "1")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.1", ref strSqlWhere) == -1)
					{
						return null;
					}
				}
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, execDept, strBegin, strEnd, clinicN0);

				return this.QueryMedTechBookApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="execDept">ִ�п���</param>
		/// <param name="beginDate">��ʼʱ��</param>
		/// <param name="endDate">����ʱ��</param>
		/// <param name="clinicN0">����Ż򿨺� </param>
		/// <param name="codeType">���� ClinicN0 1 ���� 2 �����</param>
		/// <returns></returns>
		public ArrayList QueryMedTechApplyDetailList(string execDept, System.DateTime beginDate, System.DateTime endDate, string clinicN0, string codeType)
		{
			try
			{
				string strBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
				string strEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";

				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyDetailList", ref strSql) == -1)
				{
					return null;
				}
				if (codeType == "2")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechDetailApplyList.Where.2", ref strSqlWhere) == -1)
					{
						return null;
					}
				}
				else if (codeType == "1")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechDetailApplyList.Where.1", ref strSqlWhere) == -1)
					{
						return null;
					}
				}
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, execDept, strBegin, strEnd, clinicN0);

				return MyGetDetailApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="exeDept">ִ�п���</param>
		/// <param name="beginDate">��ʼʱ��</param>
		/// <param name="endDate">����ʱ��</param>
		/// <param name="noonID">���</param>
		/// <param name="itemComparison">��Ŀ��</param>
		/// <returns></returns>
		public ArrayList QueryMedTechApplyDetailList(string exeDept, string noonID, string itemComparison, System.DateTime beginDate, System.DateTime endDate)
		{
			try
			{
				string strBegin = beginDate.Year.ToString() + "-" + beginDate.Month.ToString() + "-" + beginDate.Day.ToString() + " 00:00:00";
				string strEnd = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " 23:59:59";

				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyDetailList", ref strSql) == -1)
				{
					return null;
				}
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechDetailApplyList.Where.3", ref strSqlWhere) == -1)
				{
					return null;
				}
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, exeDept, strBegin, strEnd, noonID, itemComparison);

				return MyGetDetailApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}
		
		/// <summary>
		/// ���ݿ��ұ����ȡ�Ƴ�����Ŀ
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <returns>�Ƴ�����Ŀ</returns>
		public ArrayList QueryDeptItem(string deptCode)
		{
			ArrayList deptItemList = new ArrayList();

			string s = "";
			if (this.Sql.GetSql("Neusoft.HISFC.Terminal.Booking.GetDeptItem", ref s) == -1)
			{
				this.Err = "��ȡSQL���Neusoft.HISFC.Terminal.Booking.GetDeptItemʧ��!";
				return null;
			}
			try
			{
				s = string.Format(s, deptCode);
			}
			catch(Exception e)
			{
				this.Err = e.Message;
				return null;
			}
			if (this.ExecQuery(s) == -1)
			{
				return null;
			}
			while (this.Reader.Read())
			{
				Neusoft.HISFC.Object.Base.DeptItem item = new DeptItem();

				item.ID = this.Reader[0].ToString();
				item.Name = this.Reader[1].ToString();
				item.CustomName = this.Reader[2].ToString();

				deptItemList.Add(item);
			}

			return deptItemList;
		}
		
		#endregion

		

		#region  ��ʱ

		/// <summary>
		/// ԤԼ��Ŀ���������ά�� ����
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪInsertMedTechItem", true)]
		public int  InsertDeptItem(Neusoft.HISFC.Object.Terminal.MedTechItem info)
		{
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.InsertDeptItem",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,GetParam(info));
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		///  ԤԼ��Ŀ���������ά�� ����
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪUpdateMedTechItem", true)]
		public int  UpdateDeptItem(Neusoft.HISFC.Object.Terminal.MedTechItem info)
		{
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.UpdateDeptItem",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,GetParam(info));
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ԤԼ��Ŀ���������ά�� ���ݿ��Ҵ������Ŀ����Ĳ���
		/// </summary>
		/// <param name="DeptCode"></param>
		/// <param name="ItemCode"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪGetMedTechItem", true)]
		public Neusoft.HISFC.Object.Terminal.MedTechItem   SelectDeptItem(string DeptCode ,string ItemCode)   
		{
			Neusoft.HISFC.Object.Terminal.MedTechItem info = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.SelectDeptItem",ref strSql)== -1 ) return null;

			try
			{
				strSql = string.Format(strSql,DeptCode,ItemCode);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					info = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					info.ItemExtend.Dept.ID = this.Reader[2].ToString();
					info.Item.ID = this.Reader[3].ToString();
					info.Item.Name = this.Reader[4].ToString();
					info.Item.SysClass.ID = this.Reader[5].ToString();
					info.ItemExtend.UnitFlag = this.Reader[6].ToString();
					info.ItemExtend.BookLocate = this.Reader[7].ToString();
					info.ItemExtend.BookTime = this.Reader[8].ToString();
					info.ItemExtend.ExecuteLocate = this.Reader[9].ToString();
					info.ItemExtend.ReportTime = this.Reader[10].ToString();
					info.ItemExtend.HurtFlag = this.Reader[11].ToString();
					info.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					info.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					info.ItemExtend.Speciality = this.Reader[14].ToString();
					info.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					info.ItemExtend.SimpleKind = this.Reader[16].ToString();
					info.ItemExtend.SimpleWay = this.Reader[17].ToString();
					info.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					info.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					info.ItemExtend.Container = this.Reader[20].ToString();
					info.ItemExtend.Scope = this.Reader[21].ToString();
					info.Item.Notice = this.Reader[22].ToString();
					info.Item.Oper.ID = this.Reader[23].ToString();
					info.Item.Oper.OperTime =Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					info.ItemExtend.MachineType = this.Reader[25].ToString();
					info.ItemExtend.BloodWay = this.Reader[26].ToString();
					info.ItemExtend.Ext1 = this.Reader[27].ToString();
					info.ItemExtend.Ext2 = this.Reader[28].ToString();
					info.ItemExtend.Ext3 = this.Reader[29].ToString();
				}
				Reader.Close();
				return info;
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}
		
		/// <summary>
		///  ������Ŀ�������һ����Ŀ��չ��Ϣ
		/// </summary>
		/// <param name="ItemCode"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪGetMedTechItem", true)]
		public Neusoft.HISFC.Object.Terminal.MedTechItem  Query(string ItemCode)   
		{
			Neusoft.HISFC.Object.Terminal.MedTechItem info = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			ArrayList al = new ArrayList();
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.SelectDeptItem",ref strSql)==-1)return null;

			try
			{
				strSql = string.Format(strSql,ItemCode);
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					info = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					info.ItemExtend.Dept.ID = this.Reader[2].ToString();
					info.Item.ID = this.Reader[3].ToString();
					info.Item.Name = this.Reader[4].ToString();
					info.Item.SysClass.ID = this.Reader[5].ToString();
					info.ItemExtend.UnitFlag = this.Reader[6].ToString();
					info.ItemExtend.BookLocate = this.Reader[7].ToString();
					info.ItemExtend.BookTime = this.Reader[8].ToString();
					info.ItemExtend.ExecuteLocate = this.Reader[9].ToString();
					info.ItemExtend.ReportTime = this.Reader[10].ToString();
					info.ItemExtend.HurtFlag = this.Reader[11].ToString();
					info.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					info.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					info.ItemExtend.Speciality = this.Reader[14].ToString();
					info.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					info.ItemExtend.SimpleKind = this.Reader[16].ToString();
					info.ItemExtend.SimpleWay = this.Reader[17].ToString();
					info.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					info.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					info.ItemExtend.Container = this.Reader[20].ToString();
					info.ItemExtend.Scope = this.Reader[21].ToString();
					info.Item.Notice = this.Reader[22].ToString();
					info.Item.Oper.ID = this.Reader[23].ToString();
					info.Item.Oper.OperTime =Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					info.ItemExtend.MachineType = this.Reader[25].ToString();
					info.ItemExtend.BloodWay = this.Reader[26].ToString();
					info.ItemExtend.Ext1 = this.Reader[27].ToString();
					info.ItemExtend.Ext2 = this.Reader[28].ToString();
					info.ItemExtend.Ext3 = this.Reader[29].ToString();
					al.Add(info);
				}
				Reader.Close();
			
				if(al.Count > 0)
				{
					return al[0] as Neusoft.HISFC.Object.Terminal.MedTechItem;
				}
				else 
				{
					return null;
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		///ԤԼ��Ŀ���������ά��  ���ݿ��Ҵ�������������Ѿ�ά������Ŀ
		/// </summary>
		/// <param name="deptCode"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪQueryMedTechItem", true)]
		public ArrayList  GetDeptItemAll(string deptCode)
		{

			string strSql="";
			ArrayList al=new ArrayList();
			if (this.Sql.GetSql("Terminal.DeptItem.query.1",ref strSql)==-1)return null;
			try
			{
				strSql=string.Format(strSql,deptCode);
			}
			catch(Exception e)
			{
				this.Err="Terminal.TecDeptItem.query.1!"+e.Message;
				this.ErrCode=e.Message;
				WriteErr();
				return null;
			}
			try
			{
				if(this.ExecQuery(strSql)==-1)return null;
				while(this.Reader.Read())
				{
					Neusoft.HISFC.Object.Terminal.MedTechItem info = new Neusoft.HISFC.Object.Terminal.MedTechItem();
					info.ItemExtend.Dept.ID = this.Reader[2].ToString();
					info.Item.ID = this.Reader[3].ToString();
					info.Item.Name = this.Reader[4].ToString();
					info.Item.SysClass.ID = this.Reader[5].ToString();
					info.ItemExtend.UnitFlag = this.Reader[6].ToString();
					info.ItemExtend.BookLocate = this.Reader[7].ToString();
					info.ItemExtend.BookTime = this.Reader[8].ToString();
					info.ItemExtend.ExecuteLocate = this.Reader[9].ToString(); 
					info.ItemExtend.ReportTime = this.Reader[10].ToString();
					info.ItemExtend.HurtFlag = this.Reader[11].ToString();
					info.ItemExtend.SelfBookFlag = this.Reader[12].ToString();
					info.ItemExtend.ReasonableFlag = this.Reader[13].ToString();
					info.ItemExtend.Speciality = this.Reader[14].ToString();
					info.ItemExtend.ClinicMeaning = this.Reader[15].ToString();
					info.ItemExtend.SimpleKind = this.Reader[16].ToString();
					info.ItemExtend.SimpleWay = this.Reader[17].ToString();
					info.ItemExtend.SimpleUnit = this.Reader[18].ToString();
					info.ItemExtend.SimpleQty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					info.ItemExtend.Container = this.Reader[20].ToString();
					info.ItemExtend.Scope = this.Reader[21].ToString();
					info.Item.Notice = this.Reader[22].ToString();
					info.Item.Oper.ID = this.Reader[23].ToString();
					info.Item.Oper.OperTime =Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[24].ToString());
					info.ItemExtend.MachineType = this.Reader[25].ToString();
					info.ItemExtend.BloodWay = this.Reader[26].ToString();
					info.ItemExtend.Ext1 = this.Reader[27].ToString();
					info.ItemExtend.Ext2 = this.Reader[28].ToString();
					info.ItemExtend.Ext3 = this.Reader[29].ToString();
					al.Add(info);
				}
				Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="Terminal.TecDeptItem.query.1!"+e.Message;
				this.ErrCode=e.Message;
				if(Reader.IsClosed==false)Reader.Close();
				WriteErr();
				return null;
			}
			return al;
		}

		/// <summary>
		/// ԤԼ��Ŀ���������ά��   ɾ��
		/// </summary>
		/// <param name="DeptCode"></param>
		/// <param name="ItemCode"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪDeleteMedTechItem", true)]
		public int  DelDeptItem(string DeptCode ,string ItemCode)
		{
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.DelDeptItem",ref strSql)==-1)return -1;
			try
			{
				strSql = string.Format(strSql,DeptCode,ItemCode);
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="ExeDept">ִ�п���</param>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <param name="ClinicN0">����Ż򿨺� </param>
		/// <param name="Type">���� ClinicN0 1 ���� 2 �����</param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪQueryTerminalApply", true)]
		public ArrayList GetTerminalApplyList(string ExeDept, System.DateTime BeginDate, System.DateTime EndDate, string ClinicN0, string Type)
		{
			try
			{
				string strBegin = BeginDate.Year.ToString() + "-" + BeginDate.Month.ToString() + "-" + BeginDate.Day.ToString() + " 00:00:00";
				string strEnd = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString() + " 23:59:59";

				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList", ref strSql) == -1)
					return null;
				if (Type == "2")
				{
					if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList.Where.2", ref strSqlWhere) == -1)
						return null;
				}
				else if (Type == "1")
				{
					if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList.Where.1", ref strSqlWhere) == -1)
						return null;
				}
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, ExeDept, strBegin, strEnd, ClinicN0);

				return this.QueryMedTechBookApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ȡĳ����ˮ�ŵ�ԤԼ��Ϣ
		/// </summary>
		/// <param name="SequenceNo"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪQueryTerminalApply", true)]
		public ArrayList GetTerminalApplyList(string SequenceNo)
		{
			try
			{
				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList", ref strSql) == -1)
					return null;

				if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList.Where.SequenceNo", ref strSqlWhere) == -1)
					return null;
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, SequenceNo);
				return this.QueryMedTechBookApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ѯ ����ԤԼ��Ŀ��Ϣ
		/// </summary>
		/// <param name="ItemCode">ִ�п���</param>
		/// <param name="BeginDate">��ʼʱ��</param>
		/// <param name="EndDate">����ʱ��</param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪQueryTerminalApply", true)]
		public ArrayList GetTerminalApplyList(string ItemCode, System.DateTime BeginDate, System.DateTime EndDate)
		{
			try
			{
				string strBegin = BeginDate.Year.ToString() + "-" + BeginDate.Month.ToString() + "-" + BeginDate.Day.ToString() + " 00:00:00";
				string strEnd = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString() + " 23:59:59";


				string strSql = "";
				string strSqlWhere = "";
				if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList", ref strSql) == -1)
					return null;

				if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList.Where.3", ref strSqlWhere) == -1)
					return null;
				strSql = strSql + strSqlWhere;
				strSql = string.Format(strSql, ItemCode, strBegin, strEnd);

				return this.QueryMedTechBookApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}

		#region "ҽ��ԤԼ����"
		/// <summary>
		/// ҽ��ԤԼ����
		/// </summary>
		/// <param name="objFeeItemlist"></param>
		/// <param name="t"></param>
		/// <returns>-1 ����</returns>
		[Obsolete("�Ѿ���ʱ������ΪMedTechApply", true)]
		public int CreateMedTechBookApply(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList objFeeItemlist, Neusoft.NFC.Management.Transaction t)
		{

			Neusoft.HISFC.Object.Terminal.MedTechItem objMedTechItem = new Neusoft.HISFC.Object.Terminal.MedTechItem();
			Neusoft.HISFC.Management.Manager.Controler controler = new Neusoft.HISFC.Management.Manager.Controler();
			controler.SetTrans(t.Trans);
			//���ݿ��Һ���Ŀ�õ�ҽ����Ŀ��չ��Ϣ
			objMedTechItem = this.GetMedTechItem(objFeeItemlist.ExecOper.Dept.ID, objFeeItemlist.ID);
			if (objMedTechItem == null)
			{
				return -1;
			}

			Neusoft.HISFC.Object.Terminal.MedTechBookApply obj = new Neusoft.HISFC.Object.Terminal.MedTechBookApply();
			obj.ItemList = objFeeItemlist; //���������Ϣ
			obj.ItemExtend = objMedTechItem.ItemExtend; //��չ��Ϣ
			#region ��ȡĳ����ˮ���µ�ԤԼ��Ϣ
			ArrayList list = QueryTerminalApply(obj.ItemList.Order.ID);
			if (list == null)
			{
				return -1;
			}
			if (list.Count == 0) //û�����ݲ���
			{
				if (this.InsertMedTechBookApply(obj) <= 0)
				{
					return -1;
				}
			}
			else //��Ҫ����
			{
				//				foreach(Neusoft.HISFC.Object.Terminal.MedTechBookApply info in list)
				//				{
				//					if(info.TerminalBookInfo.Status == "2"
				//				}
				if (UpdateMedTechBookApply(obj) <= 0)
				{
					return -1;
				}
			}
			#endregion
			//			int i = UpdateMedTechBookApply(obj);
			//			if(i== -1)
			//			{
			//				return -1;
			//			}
			//			if( i== 0)
			//			{
			//				if(InsertTerminalApplyInfo(obj) <=0)
			//				{
			//					return -1;
			//				}
			//			}
			//�ж��Ƿ������ͬʱ�������
			if (controler.QueryControlerInfo("300013") == "1") //����ͬʱ���
			{
				if (this.AuditingMedTechBookApply(objFeeItemlist) <= 0)
				{
					return -1;
				}
			}
			return 0;
		}

		

		#endregion

		#region "ҽ��ԤԼ��׼"
		[Obsolete("�Ѿ���ʱ������ΪAuditingMedTechBookApply", true)]
		public int AffirmMedTechBookApply(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList objFeeItemList)
		{
			return this.UpdateMedTechBookApplyFlag(objFeeItemList, "1");
		}

		#endregion

		#region "ҽ��ԤԼ����"
		/// <summary>
		/// ԤԼ����
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪPlanMedTechBookApply", true)]
		public int PlanTerminalBook(Neusoft.HISFC.Object.Terminal.MedTechBookApply obj)
		{
			string strSql = "";
			if (this.Sql.GetSql("Met.PlanTerminalBook", ref strSql) == -1)
				return -1;
			try
			{
				obj.MedTechBookInfo.BookID = GetMedTechBookApplyID();
				//				obj.ItemList.SeqNo =Neusoft.NFC.Function.NConvert.ToInt32(GetMedTechBookApplySortID(obj.ItemList.ID,obj.ItemList.ExeDeptInfo.ID,obj.TerminalBookInfo.BookDate,obj.noon.ID));
				obj.MedTechBookInfo.Status = "2";
				strSql = string.Format(strSql, this.GetParam(obj));
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}
		#endregion

		#region "ҽ��ԤԼȡ��"


		#endregion
		/// <summary>
		/// ɾ��ԤԼ��Ϣ
		/// </summary>
		/// <param name="SequenceNo"></param>
		/// <returns>-1 ���� 1 ɾ���ɹ�</returns>
		[Obsolete("�Ѿ���ʱ������ΪDeleteMedTechBookApply", true)]
		public int DeleteApply(string SequenceNo)
		{
			ArrayList list = QueryTerminalApply(SequenceNo);
			if (list == null)
			{
				return -1;
			}
			if (list.Count == 0)
			{
				return 1;
			}
			Neusoft.HISFC.Object.Terminal.MedTechBookApply info = (Neusoft.HISFC.Object.Terminal.MedTechBookApply)list[0];
			if (info.MedTechBookInfo.Status == "2")
			{
				this.Err = "�������� ,����ȥȡ������";
				return -1;
			}
			string strSql = "";
			if (this.Sql.GetSql("Terminal.DeptItem.GetTerminalApplyList.DeleteArray", ref strSql) == -1)
				return -1;
			strSql = string.Format(strSql, SequenceNo);
			return this.ExecNoQuery(strSql);
		}



		#region ˽�г�Ա
		/// <summary>
		/// ��ȡ��Ϣ
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪQueryMedTechBookApply", true)]
		private ArrayList MyGetApply(string strSql)
		{
			ArrayList list = new ArrayList();
			Neusoft.HISFC.Object.Terminal.MedTechBookApply info = null;
			this.ExecQuery(strSql);
			while (this.Reader.Read())
			{
				info = new Neusoft.HISFC.Object.Terminal.MedTechBookApply();
				info.ItemList.ID = this.Reader[0].ToString();//CLINIC_CODE      //������ˮ��             
				info.ItemList.TransType = Neusoft.HISFC.Object.Base.TransTypes.Positive; //TRANS_TYPE       // ��������               
				info.ItemList.Patient.PID.CardNO = this.Reader[2].ToString();//CARD_NO          //���￨��               
				info.ItemList.User02 = this.Reader[3].ToString();//NAME             //����                   
				info.ItemList.User01 = this.Reader[4].ToString();//AGE              // ����                   
				info.ItemList.ID = this.Reader[5].ToString();//ITEM_CODE        // ��Ŀ����               
				info.ItemList.Name = this.Reader[6].ToString();//ITEM_NAME        // ��Ŀ����               
				info.ItemList.Item.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[7].ToString());//ITEM_QTY         // ��Ŀ����               
				info.ItemExtend.UnitFlag = this.Reader[8].ToString();//UNIT_FLAG        // ��λ��ʶ               
				info.ItemList.RecipeNO = this.Reader[9].ToString();//RECIPE_NO        // ������                 
				info.ItemList.SequenceNO = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[10].ToString());//SEQUENCE_NO      // ��������Ŀ���         
				info.ItemList.Order.DoctorDept.Name = this.Reader[11].ToString();//RECIPE_DEPTNAME  //������������           
				info.ItemList.ExecOper.Dept.ID = this.Reader[12].ToString();//DEPT_CODE        // ���Һ�                 
				info.ItemList.ExecOper.Dept.Name = this.Reader[13].ToString(); //DEPT_NAME        // ��������               
				info.MedTechBookInfo.Status = this.Reader[14].ToString(); //STATUS           //0 ԤԤԼ 1 ��Ч 2 ��� 
				info.MedTechBookInfo.BookID = this.Reader[15].ToString();//BOOK_ID          //ԤԼ����               
				info.MedTechBookInfo.BookTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[16].ToString());//BOOK_DATE        //ԤԼʱ��               
				info.Noon.ID = this.Reader[17].ToString();//NOON_CODE        //���                   
				info.ItemExtend.ReasonableFlag = this.Reader[18].ToString();//REASONABLE_FLAG  //֪��ͬ����             
				info.HealthFlag = this.Reader[19].ToString();//HEALTH_STATUS    //����״̬               
				info.ItemList.Order.DoctorDept.Name = this.Reader[20].ToString();//EXECUTE_LOCATE   //ִ�еص�               
				info.ReportTime = Neusoft.NFC.Function.NConvert.ToDateTime(this.Reader[21].ToString());//REPORT_DATE      //ȡ����ʱ��             
				info.ItemExtend.HurtFlag = this.Reader[22].ToString();//HURT_FLAG        //�д�/�޴�              
				info.ItemExtend.SimpleKind = this.Reader[23].ToString();//SAMPLE_KIND      //�걾��λ             
				info.ItemExtend.SimpleWay = this.Reader[24].ToString();//SAMPLE_WAY       //��������               
				info.Memo = this.Reader[25].ToString();//REMARK           //ע������               
				info.SortID = Neusoft.NFC.Function.NConvert.ToInt32(this.Reader[26].ToString());//SORT_ID          //˳���                 
				this.Operator.ID = this.Reader[27].ToString();//OPER_CODE        //����Ա                 
				info.ItemList.Order.DoctorDept.ID = this.Reader[28].ToString();//OPER_DEPTCODE    //��������  
				info.ItemList.Order.ID = this.Reader[30].ToString();//OPER_DEPTCODE    //��������    
				//					System.DateTime= this.Reader[29].ToString();//OPER_DATE        //�������� 
				list.Add(info);
			}
			return list;
		}
		#region "��ȡ���뵥��"




		#endregion
		/// <summary>
		/// ����ҽ��ԤԼ
		/// </summary>
		/// <param name="objMedTechBookApply"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪInsertMedTechBookApply")]
		private int InsertTerminalApplyInfo(Neusoft.HISFC.Object.Terminal.MedTechBookApply objMedTechBookApply)
		{
			string strSql = "";
			if (this.Sql.GetSql("Met.CreateMedTechBookApply", ref strSql) == -1)
				return -1;
			try
			{
				strSql = string.Format(strSql, this.GetParam(objMedTechBookApply));
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}


		/// <summary>
		/// ����ԤԼ���еı�־
		/// </summary>
		/// <param name="objFeeItemList"> �շ��б�</param>
		/// <param name="Type"> 0 ԤԤԼ 1 ��Ч 2 ���       </param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪUpdateMedTechBookApplyFlag", true)]
		private int SetAffirmMedTechBookApply(Neusoft.HISFC.Object.Fee.Outpatient.FeeItemList objFeeItemList, string Type)
		{
			string strSql = "";
			if (this.Sql.GetSql("Met.AffirmMedTechBookApply", ref strSql) == -1)
				return -1;
			try
			{
				strSql = string.Format(strSql, objFeeItemList.ID, objFeeItemList.RecipeNO, objFeeItemList.SequenceNO, Type);
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}
		#region "��ʼ��������Ϣ"
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		[Obsolete("�Ѿ���ʱ������ΪGetParam", true)]
		private string[] GetParamApply(Neusoft.HISFC.Object.Terminal.MedTechBookApply info)
		{
			string[] str = new string[]{
										   info.ItemList.ID,//CLINIC_CODE      //������ˮ��   0          
										   "1", //TRANS_TYPE       // ��������   1            
										   info.ItemList.Patient.PID.CardNO,//CARD_NO          //���￨�� 2              
										   info.ItemList.User02,//NAME             //����    3               
										   "1",//AGE              // ����   4                
										   info.ItemList.ID,//ITEM_CODE        // ��Ŀ����  5            
										   info.ItemList.Name,//ITEM_NAME        // ��Ŀ����   6            
										   info.ItemList.Item.Qty.ToString(),//ITEM_QTY         // ��Ŀ����   7            
										   info.ItemExtend.UnitFlag,//UNIT_FLAG        // ��λ��ʶ      8         
										   info.ItemList.RecipeNO,//RECIPE_NO        // ������          9       
										   info.ItemList.SequenceNO.ToString(),//SEQUENCE_NO      // ��������Ŀ���   10      
										   info.ItemList.Order.DoctorDept.Name,//RECIPE_DEPTNAME  //������������   11        
										   info.ItemList.ExecOper.Dept.ID,//DEPT_CODE        // ���Һ�        12         
										   info.ItemList.ExecOper.Dept.Name, //DEPT_NAME        // ��������     13          
										   info.MedTechBookInfo.Status, //STATUS           //0 ԤԤԼ 1 ��Ч 2 ����  14
										   info.MedTechBookInfo.BookID,//BOOK_ID          //ԤԼ����  15             
										   info.MedTechBookInfo.BookTime.ToString(),//BOOK_DATE        //ԤԼʱ��    16         
										   info.Noon.ID,//NOON_CODE        //���        17         
										   info.ItemExtend.ReasonableFlag,//REASONABLE_FLAG  //֪��ͬ����  18        
										   info.HealthFlag,//HEALTH_STATUS    //����״̬    19           
										   info.ItemList.Order.DoctorDept.Name,//EXECUTE_LOCATE   //ִ�еص�    20          
										   info.ReportTime.ToString(),//REPORT_DATE      //ȡ����ʱ��   21          
										   info.ItemExtend.HurtFlag,//HURT_FLAG        //�д�/�޴�   22           
										   info.ItemExtend.SimpleKind,//SAMPLE_KIND      //�걾��λ   23          
										   info.ItemExtend.SimpleWay,//SAMPLE_WAY       //��������    24           
										   info.Memo,//REMARK           //ע������  25            
										   info.SortID.ToString(),//SORT_ID          //˳���     26           
										   this.Operator.ID,//OPER_CODE        //����Ա27                
										   info.ItemList.Order.DoctorDept.ID,//OPER_DEPTCODE    //��������    28          
										   System.DateTime.Now.ToString(),//OPER_DATE        //�������� 29
										   info.ItemList.Order.ID
									   };
			return str;
		}
		#endregion

		#endregion
		
		#endregion 
	}
}
