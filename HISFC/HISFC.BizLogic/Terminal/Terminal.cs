using System;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Terminal;

namespace FS.HISFC.BizLogic.Terminal
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
	public class Terminal : FS.FrameWork.Management.Database
	{
		#region ˽�к���

		/// <summary>
		/// ��ʵ������Է���������
		/// </summary>
		/// <param name="medTechItem">ԤԼ��Ŀ</param>
		/// <returns>�ֶ�����</returns>
		private string [] GetParam(FS.HISFC.Models.Terminal.MedTechItem medTechItem)
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
			FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply = null;
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
				medTechBookApply = new FS.HISFC.Models.Terminal.MedTechBookApply();

				// ������ˮ��
				medTechBookApply.ItemList.Patient.PID.ID = this.Reader[0].ToString();
				medTechBookApply.ItemList.Patient.ID = medTechBookApply.ItemList.Patient.PID.ID;
				medTechBookApply.ItemList.ID = medTechBookApply.ItemList.Patient.ID;
				// ��������
				medTechBookApply.ItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
				// ���￨��
				medTechBookApply.ItemList.Patient.PID.CardNO = this.Reader[2].ToString();
				// ����
                //medTechBookApply.ItemList.Name = this.Reader[3].ToString();
                medTechBookApply.ItemList.Patient.Name = this.Reader[3].ToString(); ;
				// ����
				medTechBookApply.ItemList.User01 = this.Reader[4].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.Item.ID = this.Reader[5].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.Item.Name = this.Reader[6].ToString();
				// ��Ŀ����
				medTechBookApply.ItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��λ��ʶ
				medTechBookApply.ItemExtend.UnitFlag = this.Reader[8].ToString();
				// ������
				medTechBookApply.ItemList.RecipeNO = this.Reader[9].ToString();
				// ��������Ŀ���
				medTechBookApply.ItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
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
				medTechBookApply.MedTechBookInfo.BookTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());
				// ���
				medTechBookApply.Noon.ID = this.Reader[17].ToString();
				// ֪��ͬ����
				medTechBookApply.ItemExtend.ReasonableFlag = this.Reader[18].ToString();
				// ����״̬
				medTechBookApply.HealthFlag = this.Reader[19].ToString();
				// ִ�еص�
				medTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[20].ToString();
				// ȡ����ʱ��
				medTechBookApply.ReportTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21].ToString());
				// �д�/�޴� 
				medTechBookApply.ItemExtend.HurtFlag = this.Reader[22].ToString();
				// �걾��λ
				medTechBookApply.ItemExtend.SimpleKind = this.Reader[23].ToString();
				// ��������
				medTechBookApply.ItemExtend.SimpleWay = this.Reader[24].ToString();
				// ע������
				medTechBookApply.Memo = this.Reader[25].ToString();
				// ˳���
				medTechBookApply.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[26].ToString());
				// ����Ա
				medTechBookApply.User01 = this.Reader[27].ToString();
				// �������� 
				medTechBookApply.ItemList.Order.DoctorDept.ID = this.Reader[28].ToString();
				// 
				medTechBookApply.ItemList.Order.ID = this.Reader[30].ToString();
                //��ԤԼ����
                medTechBookApply.ArrangeQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[31]);

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
		private string[] GetParam(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
		{
			string[] str = new string[]{	// ������ˮ��
											medTechBookApply.ItemList.ID,
											// ��������
											"1",
											// ���￨��
											medTechBookApply.ItemList.Patient.PID.CardNO,
											// ����
											medTechBookApply.ItemList.Patient.Name,
											// ����
											"0",//medTechBookApply.ItemList.Patient.Age,
											// ��Ŀ����
											medTechBookApply.ItemList.Item.ID,
											// ��Ŀ����
											medTechBookApply.ItemList.Item.Name,
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
			FS.HISFC.Models.Terminal.MedTechBookApply tempMedTechBookApply = null;
			this.ExecQuery(strSql);
			while (this.Reader.Read())
			{
				tempMedTechBookApply = new MedTechBookApply();
				// ������ˮ��
				tempMedTechBookApply.ItemList.ID = this.Reader[0].ToString();
				tempMedTechBookApply.ItemList.Patient.ID = tempMedTechBookApply.ItemList.ID;
				tempMedTechBookApply.ItemList.Patient.PID.ID = tempMedTechBookApply.ItemList.ID;
				// ��������
				tempMedTechBookApply.ItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
				// ���￨��
				tempMedTechBookApply.ItemList.Patient.PID.CardNO = this.Reader[2].ToString();
				// ����
				tempMedTechBookApply.ItemList.Patient.Name = this.Reader[3].ToString(); 
				// ����
				tempMedTechBookApply.ItemList.User01 = this.Reader[4].ToString();
				// ��Ŀ���� 
				tempMedTechBookApply.ItemList.Item.ID = this.Reader[5].ToString();
				// ��Ŀ����
				tempMedTechBookApply.ItemList.Item.Name = this.Reader[6].ToString();
				// ��Ŀ����
				tempMedTechBookApply.ItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
				// ��λ��ʶ
				tempMedTechBookApply.ItemExtend.UnitFlag = this.Reader[8].ToString();
				// ������
				tempMedTechBookApply.ItemList.RecipeNO = this.Reader[9].ToString();
				// ��������Ŀ���
				tempMedTechBookApply.ItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
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
				tempMedTechBookApply.MedTechBookInfo.BookTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());
				// ���
				tempMedTechBookApply.Noon.ID = this.Reader[17].ToString();
				// ֪��ͬ����
				tempMedTechBookApply.ItemExtend.ReasonableFlag = this.Reader[18].ToString();
				// ����״̬
				tempMedTechBookApply.HealthFlag = this.Reader[19].ToString();
				// ִ�еص�
				tempMedTechBookApply.ItemList.Order.DoctorDept.Name = this.Reader[20].ToString();
				// ȡ����ʱ��
				tempMedTechBookApply.ReportTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21].ToString());
				// �д�/�޴�
				tempMedTechBookApply.ItemExtend.HurtFlag = this.Reader[22].ToString();
				// �걾��λ
				tempMedTechBookApply.ItemExtend.SimpleKind = this.Reader[23].ToString();
				// ��������
				tempMedTechBookApply.ItemExtend.SimpleWay = this.Reader[24].ToString();
				// ע������
				tempMedTechBookApply.Memo = this.Reader[25].ToString();
				// ˳���
				tempMedTechBookApply.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[26].ToString());
				// ����Ա
				tempMedTechBookApply.User01 = this.Reader[27].ToString();
				// ��������
				tempMedTechBookApply.ItemList.Order.DoctorDept.ID = this.Reader[28].ToString();
				// ��������
				tempMedTechBookApply.ItemList.Order.ID = this.Reader[30].ToString();
				// ��Ӧ��Ŀ
				tempMedTechBookApply.ItemComparison.ID = this.Reader[31].ToString();

                //ԤԼʱ���        {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ��+����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                tempMedTechBookApply.MedTechBookInfo.User01 = this.Reader[32].ToString();
                //ִ���豸          {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ��+����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                tempMedTechBookApply.MedTechBookInfo.User02 = this.Reader[33].ToString();

				detailList.Add(tempMedTechBookApply);
			}
			return detailList;
		}

		/// <summary>
		/// ��ѯ���ն�ȷ����Ϣ ��Ŀǰ�Ѱ������� �ɴ��ж��Ƿ����ȡ��һ��ԤԼ����
		/// </summary>
		/// <param name="medTechBookApply"></param>
		/// <returns> 1 ����ȡ��  0 ������ȡ�� ��1 ��ѯ����</returns>
		public int IsCanCancelMedTechBookApply(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
		{
			try
			{
				// �ն�ȷ��ҵ���
				TerminalConfirm terminalConfirm = new TerminalConfirm();
				// ���뵥��ˮ��
				string applyNumber = "";
				// ��ȷ������
				decimal alreadyCount = 0;
				// ��ȡ������Ϣ
				if (terminalConfirm.GetApplyNoByOrderNo(medTechBookApply.ItemList.Order.ID, ref applyNumber) == 1)
				{
					if (terminalConfirm.GetAlreadyCount(applyNumber, ref alreadyCount) == 1)
					{
						int alreadArrangeNum = FS.FrameWork.Function.NConvert.ToInt32(medTechBookApply.User01);
						if (alreadArrangeNum - alreadyCount <= 0)
						{
							return 0;
						}
						else
						{
							return 1;
						}
					}
				}
				
				this.Err = terminalConfirm.Err;
				
				return -1;
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}
				
		#endregion

		#region ���к���

        #region ԤԼ��Ŀ��չ�� ά��
        /// <summary>
		/// ԤԼ��Ŀ���������ά�� ����
		/// </summary>
		/// <param name="medTechItem">ԤԼ��Ŀ</param>
		/// <returns>Ӱ�����������1��ʧ��</returns>
		public int InsertMedTechItem(FS.HISFC.Models.Terminal.MedTechItem medTechItem)
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
                //sql = string.Format(sql, GetParam(medTechItem));

                return this.ExecNoQuery(sql, GetParam(medTechItem));
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
		public int UpdateMedTechItem(FS.HISFC.Models.Terminal.MedTechItem medTechItem)
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
                //sql = string.Format(sql, GetParam(medTechItem));
                return this.ExecNoQuery(sql, GetParam(medTechItem));
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
		public FS.HISFC.Models.Terminal.MedTechItem GetMedTechItem(string deptCode, string itemCode)
		{
			// ԤԼ��Ŀ
			FS.HISFC.Models.Terminal.MedTechItem medTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
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
					medTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
					
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
					medTechItem.ItemExtend.SimpleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					medTechItem.ItemExtend.Container = this.Reader[20].ToString();
					medTechItem.ItemExtend.Scope = this.Reader[21].ToString();
					medTechItem.Item.Notice = this.Reader[22].ToString();
					medTechItem.Item.Oper.ID = this.Reader[23].ToString();
					medTechItem.Item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[24].ToString());
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
		public FS.HISFC.Models.Terminal.MedTechItem GetMedTechItem(string itemCode)
		{
			// ��Ŀ��չ��Ϣ
			FS.HISFC.Models.Terminal.MedTechItem medTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
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
					medTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
					
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
					medTechItem.ItemExtend.SimpleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());
					medTechItem.ItemExtend.Container = this.Reader[20].ToString();
					medTechItem.ItemExtend.Scope = this.Reader[21].ToString();
					medTechItem.Item.Notice = this.Reader[22].ToString();
					medTechItem.Item.Oper.ID = this.Reader[23].ToString();
					medTechItem.Item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[24].ToString());
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
					return medTechItemList[0] as FS.HISFC.Models.Terminal.MedTechItem;
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
                    FS.HISFC.Models.Terminal.MedTechItem info = new MedTechItem();
                    info.ItemExtend.Dept.ID = this.Reader[0].ToString();
                    info.Item.ID = this.Reader[1].ToString();
                    info.Item.Name = this.Reader[2].ToString();
                    info.Item.SysClass.ID = this.Reader[3].ToString();
                    info.ItemExtend.UnitFlag = this.Reader[4].ToString();
                    info.ItemExtend.BookLocate = this.Reader[5].ToString();
                    info.ItemExtend.BookTime = this.Reader[6].ToString();
                    info.ItemExtend.ExecuteLocate = this.Reader[7].ToString();
                    info.ItemExtend.ReportTime = this.Reader[8].ToString();
                    info.ItemExtend.HurtFlag = this.Reader[9].ToString();
                    info.ItemExtend.SelfBookFlag = this.Reader[10].ToString();
                    info.ItemExtend.ReasonableFlag = this.Reader[11].ToString();
                    info.ItemExtend.Speciality = this.Reader[12].ToString();
                    info.ItemExtend.ClinicMeaning = this.Reader[13].ToString();
                    info.ItemExtend.SimpleKind = this.Reader[14].ToString();
                    info.ItemExtend.SimpleWay = this.Reader[15].ToString();
                    info.ItemExtend.SimpleUnit = this.Reader[16].ToString();
                    info.ItemExtend.SimpleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                    info.ItemExtend.Container = this.Reader[18].ToString();
                    info.ItemExtend.Scope = this.Reader[19].ToString();
                    info.Item.Notice = this.Reader[20].ToString();
                    info.Item.Oper.ID = this.Reader[21].ToString();
                    info.Item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString());
                    info.ItemExtend.MachineType = this.Reader[23].ToString();
                    info.ItemExtend.BloodWay = this.Reader[24].ToString();
                    info.ItemExtend.Ext1 = this.Reader[25].ToString();
                    info.ItemExtend.Ext2 = this.Reader[26].ToString();
                    info.ItemExtend.Ext3 = this.Reader[27].ToString();

                    deptItemList.Add(info);
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
        /// ���ݿ��Ҵ�������������Ѿ�ά�����豸
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryMedTechEquipment ( string deptCode )
        {
            // SQL���
            string strSql = "";
            // ��Ŀ����
            ArrayList deptEquipmentList = new ArrayList ( );
            //
            // ��ȡSQL���
            //
            if ( this.Sql.GetSql ( "MedTech.DeptEquipment.query.1" , ref strSql ) == -1 )
            {
                return null;
            }
            //
            // ƥ��SQL���
            //
            try
            {
                strSql = string.Format ( strSql , deptCode );
            }
            catch ( Exception e )
            {
                this.Err = "ƥ��SQL���MedTech.DeptEquipment.query.1ʧ��!" + e.Message;
                this.ErrCode = e.Message;
                WriteErr ( );
                return null;
            }
            try
            {
                // ִ��SQL���
                if ( this.ExecQuery ( strSql ) == -1 )
                {
                    return null;
                }

                while ( this.Reader.Read ( ) )
                {
                    FS.HISFC.Models.Terminal.TerminalCarrier terminal = new FS.HISFC.Models.Terminal.TerminalCarrier ( );
                    terminal.Dept.ID = this.Reader [ 0 ].ToString ( );
                    terminal.CarrierCode = this.Reader [ 1 ].ToString ( );
                    terminal.CarrierName = this.Reader [ 2 ].ToString ( );
                    terminal.CarrierType = this.Reader [ 3 ].ToString ( );
                    terminal.CarrierMemo = this.Reader [ 4 ].ToString ( );
                    terminal.SpellCode = this.Reader [ 5 ].ToString ( );
                    terminal.WBCode = this.Reader [ 6 ].ToString ( );
                    terminal.UserCode = this.Reader [ 7 ].ToString ( );
                    terminal.Model = this.Reader [ 8 ].ToString ( );
                    terminal.IsDisengaged = this.Reader [ 9 ].ToString ( );
                    terminal.DisengagedTime = FS.FrameWork.Function.NConvert.ToDateTime ( this.Reader [ 10 ].ToString ( ) );
                    terminal.DayQuota = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 11 ].ToString ( ) );
                    terminal.DoctorQuota = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 12 ].ToString ( ) );
                    terminal.SelfQuota = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 13 ].ToString ( ) );
                    terminal.WebQuota = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 14 ].ToString ( ) );
                    terminal.Building = this.Reader [ 15 ].ToString ( );
                    terminal.Floor = this.Reader [ 16 ].ToString ( );
                    terminal.Room = this.Reader [ 17 ].ToString ( );
                    terminal.SortId = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 18 ].ToString ( ) );
                    terminal.IsPrestopTime = this.Reader [ 19 ].ToString ( );
                    terminal.PreStopTime = FS.FrameWork.Function.NConvert.ToDateTime ( this.Reader [ 20 ].ToString ( ) );
                    terminal.PreStartTime = FS.FrameWork.Function.NConvert.ToDateTime ( this.Reader [ 21 ].ToString ( ) );
                    terminal.AvgTurnoverTime = FS.FrameWork.Function.NConvert.ToDecimal ( this.Reader [ 22 ].ToString ( ) );
                    terminal.CreateOper = this.Reader [ 23 ].ToString ( );
                    terminal.CreateTime = FS.FrameWork.Function.NConvert.ToDateTime ( this.Reader [ 24 ].ToString ( ) );
                    terminal.IsValid = this.Reader [ 25 ].ToString ( );


                    deptEquipmentList.Add ( terminal );
                }
                Reader.Close ( );
            }
            catch ( Exception e )
            {
                this.Err = e.Message;
                this.ErrCode = e.Message;
                if ( Reader.IsClosed == false )
                {
                    Reader.Close ( );
                }
                WriteErr ( );
                return null;
            }
            // ���ؽ��
            return deptEquipmentList;
        }
        #endregion

        #region ҽ��ԤԼ ����
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
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList", ref sql) == -1)
				{
					this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyListʧ��";
					return null;
				}
				// ���ݲ�ͨ�Ĳ�ѯ���ͻ�ȡ��ͨ��Where����
				if (codeType == "2")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.2", ref where) == -1)
					{
						this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyList.Where.2ʧ��";
						return null;
					}
				}
				else if (codeType == "1")
				{
					if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.1", ref where) == -1)
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
			if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList", ref sql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyListʧ��";
				return null;
			}
			if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.SequenceNo", ref where) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyList.Where.SequenceNoʧ��";
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
			if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList", ref sql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyListʧ��";
				return null;
			}
			if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.Where.3", ref where) == -1)
			{
				this.Err = "��ȡSQL���MedTech.DeptItem.GetMedTechApplyList.Where.3ʧ��";
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

        #region ҽ��ԤԼ����

        #region ҽ��ԤԼ����
        /// <summary>
		/// ҽ��ԤԼ����
		/// </summary>
		/// <param name="feeItemList">�������</param>
		/// <returns>-1��ʧ�ܣ�</returns>
		public int MedTechApply(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
		{
			// ҽ��ԤԼ��Ŀ
			FS.HISFC.Models.Terminal.MedTechItem medTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
			// ���Ʋ���ҵ��
            FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
			// ҽ��ԤԼ����
			FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply = new FS.HISFC.Models.Terminal.MedTechBookApply();
			//
			//���ݿ��Һ���Ŀ�õ�ҽ����Ŀ��չ��Ϣ
			//
			medTechItem = this.GetMedTechItem(feeItemList.ExecOper.Dept.ID, feeItemList.ID);
			if (medTechItem == null)
			{
				return -1;
			}
            //ת�� 
            feeItemList.ID = feeItemList.Patient.ID;
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
                medTechBookApply.ItemList.SequenceNO = (metTechBookApplyList[0] as FS.HISFC.Models.Terminal.MedTechBookApply).ItemList.SequenceNO;
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
        #endregion 
        #region ����ҽ��ԤԼ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="medTechBookApply">ҽ��ԤԼ��Ϣ</param>
        /// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
        private int InsertMedTechBookApply(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
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
        #endregion 
        #region ����ҽ��ԤԼ��
        /// <summary>
        /// ����ҽ��ԤԼ
        /// </summary>
        /// <param name="medTechBookApply">ҽ��ԤԼ��Ϣ</param>
        /// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
        private int UpdateMedTechBookApply(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
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
        #endregion 

        #endregion

        /// <summary>
		/// ҽ��ԤԼ��׼
		/// </summary>
		/// <param name="feeItemList">�������</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int AuditingMedTechBookApply(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList)
		{
			return this.UpdateMedTechBookApplyFlag(feeItemList, "1");
		}

		/// <summary>
		/// ԤԼ����
		/// </summary>
		/// <param name="medTechBookApply">ҽ��ԤԼ����</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int PlanMedTechBookApply(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
		{
			// sql���
			string sql = "";
			if (this.Sql.GetSql("Met.PlanMedTechBook", ref sql) == -1)
			{
				this.Err = "��ȡsql���Met.PlanMedTechBookʧ��";
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
		public int CancelMedTechBookApply(FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply)
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
			FS.HISFC.Models.Terminal.MedTechBookApply medTechBookApply = (FS.HISFC.Models.Terminal.MedTechBookApply)medTechBookApplyList[0];
			if (medTechBookApply.MedTechBookInfo.Status == "2")
			{
				this.Err = "�������� ,����ȥȡ������";
				return -1;
			}
			//
			// ��ȡsql���
			//
			if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList.DeleteArray", ref strSql) == -1)
			{
				this.Err = "��ȡsql���MedTech.DeptItem.GetMedTechApplyList.DeleteArrayʧ��";
				return -1;
			}
			// ƥ��sql���
			strSql = string.Format(strSql, sequenceNO);
			// ִ�з���
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ����ԤԼ���еı�־
		/// </summary>
		/// <param name="feeItemList"> �������</param>
		/// <param name="flagType"> ��־���ͣ�0 ԤԤԼ 1 ��Ч 2 ���</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		private int UpdateMedTechBookApplyFlag(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList, string flagType)
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
		public int InsertMedTechApplyDetailInfo(FS.HISFC.Models.Terminal.MedTechBookApply objMedTechBookApply)
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
        /// ����ҽ��ԤԼ������ϸ��
        /// </summary>
        /// <param name="objMedTechBookApply">ҽ��ԤԼ������ϸ</param>
        /// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
        public int UpdateMedTechApplyDetailInfo(FS.HISFC.Models.Terminal.MedTechBookApply objMedTechBookApply)
        {
            string strSql = "";
            if (this.Sql.GetSql("Met.UpdateMedTechApplyDetailInfo.1", ref strSql) == -1)
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

        public int UpdateMedTechApplyByMoOrder(string sequenceNO, int BackQty)
        {
            string strSql = "";
            if (this.Sql.GetSql("TerminalConfirm.UpdateMedTechApplyByMoOrder", ref strSql) == -1)
            {
                this.Err = "��ȡTerminalConfirm.UpdateMedTechApplyByMoOrder ʧ��";
                return -1;
            }
            strSql = string.Format(strSql, sequenceNO, BackQty);
            return this.ExecNoQuery(strSql);
        }

		/// <summary>
		/// ��ȡ����ԤԼ��ϸ����ֶβ���
		/// </summary>
		/// <param name="tempMedTechBookApply">ҽ��ԤԼ��ϸ</param>
		/// <returns></returns>
		private string[] GetDetailParamApply(FS.HISFC.Models.Terminal.MedTechBookApply tempMedTechBookApply)
		{
			string[] stringArray = new string[]{
			                                   // ������ˮ��
											   tempMedTechBookApply.ItemList.ID,
											   // ��������
											   "1",
											   // ���￨��
											   tempMedTechBookApply.ItemList.Patient.PID.CardNO,
											   // ����
											   tempMedTechBookApply.ItemList.Patient.Name,
											   // ����
											   "1",
											   // ��Ŀ����
											   tempMedTechBookApply.ItemList.Item.ID,
											   // ��Ŀ����
											   tempMedTechBookApply.ItemList.Item.Name,
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
											   tempMedTechBookApply.ItemComparison.ID,
                                               tempMedTechBookApply.MedTechBookInfo.User01,//ʱ���     {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
                                               tempMedTechBookApply.MedTechBookInfo.User02//ִ���豸    {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD} ���ӿ�ʼʱ�䡢����ʱ�䡢�豸��Ϣ�Ķ�ȡ
									   };
			return stringArray;
		}

		/// <summary>
		/// ���� met_tec_apply���Ѱ�������
		/// </summary>
		/// <param name="ApplyNum">�Ѱ�������</param>
		/// <param name="tempMedTechBookApply">ҽ��ԤԼ</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int UpdateApplyNum(int ApplyNum, FS.HISFC.Models.Terminal.MedTechBookApply tempMedTechBookApply)
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

                string strSql = GetApplySql();
				string strSqlWhere = "";
                if (strSql == null)
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

                string strSql = GetDetailSql();
				string strSqlWhere = "";
				if (strSql == null)
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

                string strSql = GetDetailSql();
				string strSqlWhere = "";
                if (strSql == null)
				{
					return null;
				}
				if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechDetailApplyList.Where.3", ref strSqlWhere) == -1)
				{
					return null;
				}
				strSql = strSql + strSqlWhere;
                strSql = string.Format(strSql, exeDept, strBegin, strEnd, itemComparison,noonID);

				return MyGetDetailApply(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return null;
			}
		}
        /// <summary>
        /// ��ϸ����SQL
        /// </summary>
        /// <returns></returns>
        private string GetDetailSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyDetailList", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }
        /// <summary>
        /// ԤԼ����SQL
        /// </summary>
        /// <returns></returns>
        private string GetApplySql()
        {
            string strSql = "";
            if (this.Sql.GetSql("MedTech.DeptItem.GetMedTechApplyList", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }
        #endregion 

        #endregion
	}
}
