using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// ҽԺ�����Ŀά��ҵ���
	/// </summary>
	public class IncomeItem : DataBase
	{
		public IncomeItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����
		/// <summary>
		/// ʵ��ö��
		/// </summary>
		public enum enumItem
		{
			/// <summary>
			/// ���ұ��/code/0
			/// </summary>
			N0DepartmentCode = 0,
			/// <summary>
			/// �����Ŀ����/name/1
			/// </summary>
			N1IncomeItemName = 1,
			/// <summary>
			/// ��Ŀ����/mark/2
			/// </summary>
			N2ItemName = 2,
			/// <summary>
			/// ��ǰ����/spell_code/3
			/// </summary>
			N3CurrentDepartment = 3,
			/// <summary>
			/// ��������/wb_code/4
			/// </summary>
			N4DepartmentName = 4,
			/// <summary>
			/// ��Ŀ����/input_code/5
			/// </summary>
			N5ItemCode = 5,
			/// <summary>
			/// ����/sort_id/6
			/// </summary>
			N6Level = 6,
			/// <summary>
			/// ��Ч�Ա�־/valid_state/7
			/// </summary>
			N7ValidState = 7,
			/// <summary>
			/// ����Ա����/oper_code/8
			/// </summary>
			N8OperatorCode = 8,
			/// <summary>
			/// ����ʱ��/oper_date/9
			/// </summary>
			N9OperateDate = 9
		}

		/// <summary>
		/// ʵ����������
		/// </summary>
		string [] stringObj = new string[8];
		/// <summary>
		/// ��ѯ���
		/// </summary>
		string SQLSelect = "FS.HISFC.Management.Manager.IncomeItem.Select";
		#endregion

		// ��������
		#region ת��ReaderΪObject
		/// <summary>
		/// ת��ReaderΪObject
		/// </summary>
		/// <param name="myConst">���ص�Object</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		int ChangeReaderToObject(ref FS.HISFC.Models.Base.Const myConst)
		{
			myConst.ID = this.Reader[(int)enumItem.N0DepartmentCode].ToString();
			myConst.Name = this.Reader[(int)enumItem.N1IncomeItemName].ToString();
			myConst.Memo = this.Reader[(int)enumItem.N2ItemName].ToString();
			myConst.SpellCode = this.Reader[(int)enumItem.N3CurrentDepartment].ToString();
			myConst.WBCode = this.Reader[(int)enumItem.N4DepartmentName].ToString();
			myConst.UserCode = this.Reader[(int)enumItem.N5ItemCode].ToString();
			try
			{
				myConst.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[(int)enumItem.N6Level].ToString());
			}
			catch
			{
				this.Err = "ת��Levelʧ�ܣ�";
				return -1;
			}
			myConst.IsValid =FrameWork.Function.NConvert.ToBoolean(this.Reader[(int)enumItem.N7ValidState].ToString());
			myConst.OperEnvironment.ID = this.Reader[(int)enumItem.N8OperatorCode].ToString();

			return 1;
		}
		#endregion

		#region ת��ReaderΪArrayList
		/// <summary>
		/// ת��ReaderΪArrayList
		/// </summary>
		/// <param name="alConst">���ص�ArrayList</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		int ChangeReaderToArrayList(ref ArrayList alConst)
		{
			int intReturn = 0;

			while (this.Reader.Read())
			{
				FS.HISFC.Models.Base.Const myConst = new Const();

				intReturn = this.ChangeReaderToObject(ref myConst);
				if (intReturn == -1)
				{
					return -1;
				}

				alConst.Add(myConst);
			}

			return 1;
		}
		#endregion

		#region ת��ObjectΪ����
		/// <summary>
		/// ת��ObjectΪ����
		/// </summary>
		/// <param name="myConst">�����ʵ��</param>
		void ChangeObjectToArray(FS.HISFC.Models.Base.Const myConst)
		{
			this.stringObj[(int)enumItem.N0DepartmentCode] = myConst.ID;
			this.stringObj[(int)enumItem.N1IncomeItemName] = myConst.Name;
			this.stringObj[(int)enumItem.N2ItemName] = myConst.Memo;
			this.stringObj[(int)enumItem.N3CurrentDepartment] = myConst.SpellCode;
			this.stringObj[(int)enumItem.N4DepartmentName] = myConst.WBCode;
			this.stringObj[(int)enumItem.N5ItemCode] = myConst.UserCode;
			this.stringObj[(int)enumItem.N6Level] = myConst.SortID.ToString();
			this.stringObj[(int)enumItem.N7ValidState] = FrameWork.Function.NConvert.ToInt32(myConst.IsValid).ToString();
			this.stringObj[(int)enumItem.N8OperatorCode] = myConst.OperEnvironment.ID;
		}
		#endregion

		#region ִ�в�ѯSQL��䣬��������
		/// <summary>
		/// ִ�в�ѯSQL��䣬��������
		/// </summary>
		/// <param name="strSELECT">Select���</param>
		/// <param name="strWHERE">Where���</param>
		/// <param name="alTemp">���ص�����</param>
		/// <param name="strWhere">ƥ��SQL���</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		public int MyExecuteReturnArrayList(string strSELECT, string strWHERE, ref ArrayList alTemp, string strWhere)
		{
			// ����ֵ
			int intReturn = 0;
			// SQL���
			string SQL = "";
			string SELECT = "";
			string WHERE = "";

			// ��ȡSQL���
			intReturn = this.GetSQL(strSELECT, ref SELECT);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			intReturn = this.GetSQL(strWHERE, ref WHERE);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			SQL = SELECT + " " + WHERE;
			
			if (strWhere != "")
			{
				// ƥ��SQL���
				try
				{
					SQL = String.Format(SQL, strWhere);
				}
				catch(Exception e)
				{
					this.Err = "ƥ��SQL���ʧ�ܣ�" + e.Message;
					return -1;
				}
			}
			else
			{
				// ƥ��SQL���
				try
				{
					SQL = String.Format(SQL);
				}
				catch(Exception e)
				{
					this.Err = "ƥ��SQL���ʧ�ܣ�" + e.Message;
					return -1;
				}
			}

			// ִ��SQL���
//			this.Reader = null;
			intReturn = this.ExecQuery(SQL);
			if (intReturn == -1)
			{
				this.Err = "ִ��SQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			
			// ���ؽ����
			intReturn = this.ChangeReaderToArrayList(ref alTemp);
			if (intReturn == -1)
			{
				return -1;
			}

			return 1;
		}
		#endregion 

		// ��ѯ
		#region ��ȡ���ܿ����б�(1���ɹ�/-1��ʧ��)
		/// <summary>
		/// ��ȡ���ܿ����б�(֧�ָ��ݿ��ұ����ȡ)(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="alConstant">���صĿ����б�����</param>
		/// <param name="department">��ǰ���ұ���</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		public int GetDepartmentList(ref ArrayList alConstant, string department)
		{
			// SQL���
			string SELECT = "";
			string WHERE = "";

			// ��ȡSQL���
			SELECT = "FS.HISFC.Management.Manager.IncomeItem.GetDepartment.Select";
			WHERE = "FS.HISFC.Management.Manager.IncomeItem.GetDepartment.Where";

			// ִ�в�ѯ����
			return this.MyExecuteReturnArrayList(SELECT, WHERE, ref alConstant, department);
		}
		#endregion

		#region ���ݿ��ұ����ȡ�����Ŀ�б�(1���ɹ�/-1��ʧ��)
		/// <summary>
		/// ���ݿ��ұ����ȡ�����Ŀ�б�(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="alIncomeItem">���ص������Ŀ�б�</param>
		/// <param name="department">���ұ���</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		public int GetIncomeItemList(ref ArrayList alIncomeItem, string department)
		{
			// SQL���
			string SELECT = this.SQLSelect;
			string WHERE = "FS.HISFC.Management.Manager.IncomeItem.GetIncomeItemList.Where";

			// ִ�в�ѯ����
			return this.MyExecuteReturnArrayList(SELECT, WHERE, ref alIncomeItem, department);
		}
		#endregion

		#region ���ݿ��ұ���������Ŀ���ƣ���ȡ�����Ŀ�б�(1���ɹ�/-1��ʧ��)
		/// <summary>
		/// ���ݿ��ұ���������Ŀ���ƣ���ȡ�����Ŀ�б�(1���ɹ�/-1��ʧ��)
		/// </summary>
		/// <param name="deparment">���ұ���</param>
		/// <param name="incomeItem">�����Ŀ</param>
		/// <param name="alConstant">���ص������Ŀ�б�</param>
		/// <returns>1���ɹ�/-1��ʧ��</returns>
		public int GetItemList(string deparment, string incomeItem, ArrayList alConstant)
		{
			// ����ֵ
			int intReturn = 0;
			// SQL���
			string SQL = "";
			string SELECT = "";
			string WHERE = "";

			// ��ȡSQL���

			// ��ȡSQL���
			intReturn = this.GetSQL(this.SQLSelect, ref SELECT);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			intReturn = this.GetSQL("FS.HISFC.Management.Manager.IncomeItem.GetItemList.Where", ref WHERE);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			SQL = SELECT + " " + WHERE;
			
			// ƥ��SQL���
			try
			{
				SQL = String.Format(SQL, deparment, incomeItem);
			}
			catch(Exception e)
			{
				this.Err = "ƥ��SQL���ʧ�ܣ�" + e.Message;
				return -1;
			}

			// ִ��SQL���
//			this.Reader = null;
			intReturn = this.ExecQuery(SQL);
			if (intReturn == -1)
			{
				this.Err = "ִ��SQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			
			// ���ؽ����
			intReturn = this.ChangeReaderToArrayList(ref alConstant);
			if (intReturn == -1)
			{
				return -1;
			}

			return 1;
		}
		#endregion

		#region ���ݿ��ұ����ж������Ŀ�Ƿ��Ѿ�����(1������/0��������/-1��ʧ��)
		/// <summary>
		/// ���ݿ��ұ����ж������Ŀ�Ƿ��Ѿ�����(1������/0��������/-1��ʧ��)
		/// </summary>
		/// <param name="department">���ұ���</param>
		/// <returns>1������/0��������/-1��ʧ��</returns>
		public int JudgeIncomeItemExist(string department)
		{
			// ����ֵ
			int intReturn = 0;
			// SQL���
			string SQL = "";
			string SELECT = "";
			string WHERE = "";

			// ��ȡSQL���
			intReturn = this.GetSQL(this.SQLSelect, ref SELECT);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			intReturn = this.GetSQL("FS.HISFC.Management.Manager.JudgeIncomeItemExist.Where", ref WHERE);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			SQL = SELECT + " " + WHERE;
			
			// ƥ��SQL���
			try
			{
				SQL = String.Format(SQL, department);
			}
			catch(Exception e)
			{
				this.Err = "ƥ��SQL���ʧ�ܣ�" + e.Message;
				return -1;
			}

			// ִ��SQL���
//			this.Reader = null;
			intReturn = this.ExecQuery(SQL);
			if (intReturn == -1)
			{
				this.Err = "ִ��SQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			if (this.Reader == null)
			{
				return 0;
			}
			if (this.Reader.Read())
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
		#endregion

		// ���ݲ���
		#region ɾ��(1���ɹ�/0��û�ҵ�/-1��ʧ��)(�������ͣ�1-ɾ�������Ŀ/2-ɾ���շ���Ŀ)
		/// <summary>
		/// ɾ��(1���ɹ�/0��û�ҵ�/-1��ʧ��)(�������ͣ�1-ɾ�������Ŀ/2-ɾ���շ���Ŀ)
		/// </summary>
		/// <param name="c">ʵ��</param>
		/// <param name="intOperate">�������ͣ�1-ɾ�������Ŀ/2-ɾ���շ���Ŀ </param>
		/// <returns>1���ɹ�/0��û�ҵ�/-1��ʧ��</returns>
		public int Delete(FS.HISFC.Models.Base.Const c, int intOperate)
		{
			// ����ֵ
			int intReturn = 0;
			// SQL���
			string DELETE = "FS.HISFC.Management.Manager.IncomeItem.Delete";
			string WHERE = "";
			string SQL = "";

			// ��ȡSQL���
			intReturn = this.GetSQL(DELETE, ref DELETE);
			if (intReturn == -1)
			{
				this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
				return -1;
			}
			switch (intOperate)
			{
				case 1:
					intReturn = this.GetSQL("FS.HISFC.Management.Manager.IncomeItem.Delete.Where.IncomeItem", ref WHERE);
					if (intReturn == -1)
					{
						this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
						return -1;
					}
					break;
				case 2:
					intReturn = this.GetSQL("FS.HISFC.Management.Manager.IncomeItem.Delete.Where.FeeItem", ref WHERE);
					if (intReturn == -1)
					{
						this.Err = "��ȡSQL���ʧ�ܣ�" + this.Err;
						return -1;
					}
					break;
			}
			SQL = DELETE + " " + WHERE;

			// ƥ��SQL���
			try
			{
				switch (intOperate)
				{
					case 1:
						SQL = String.Format(SQL, c.ID, c.Name);
						break;
					case 2:
						SQL = String.Format(SQL, c.ID, c.Name, c.UserCode);
						break;
				}
			}
			catch(Exception e)
			{
				this.Err = "ƥ��SQL���ʧ�ܣ�" + e.Message;
				return -1;
			}

			// ִ��SQL���
			return this.ExecNoQuery(SQL);
		}
		#endregion
	}
}
