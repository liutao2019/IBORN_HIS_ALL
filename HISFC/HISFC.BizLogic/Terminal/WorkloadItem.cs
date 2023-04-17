using System;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Terminal
{
	/// <summary>
	/// WorkloadItem <br></br>
	/// [��������: ��������Ŀά��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2007-3-1]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class WorkloadItem : FS.FrameWork.Management.Database
	{
		public WorkloadItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ����
		/// <summary>
		/// Select���
		/// </summary>
		string SELECT = "";
		/// <summary>
		/// Where���
		/// </summary>
		string WHERE = "";
		/// <summary>
		/// SQL���
		/// </summary>
		string SQL = "";
		/// <summary>
		/// ����ֵ
		/// </summary>
		int intReturn = 0;
		/// <summary>
		/// ��������
		/// </summary>
		string [] parmItem = new string[6];
		/// <summary>
		/// �ֶζ���ö��
		/// </summary>
		enum enumItem
		{
			Sequence = 0,
			DepartmentCode = 1,
			ItemCode = 2,
			IsPhamacy = 3,
			OperatorCode = 4,
			OperateDate = 5,
			DepartmentName = 6,
			ItemName = 7
		}
		#endregion

		#region ˽�к���

		#region ��ʼ������
		/// <summary>
		/// ��ʼ������
		/// </summary>
		private void InitVar()
		{
			this.SELECT = "";
			this.WHERE = "";
			this.SQL = "";
			this.intReturn = 0;
			for (int i = 0;i < this.parmItem.Length;i++)
			{
				this.parmItem[i] = "";
			}
		}
		#endregion
		#region ����SQL���
		/// <summary>
		/// ����SQL���
		/// </summary>
		private void CreateSQL()
		{
			this.SQL = this.SELECT + " " + this.WHERE;
		}
		#endregion

		#region �������
		/// <summary>
		/// �������
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		private void FillParm(FS.HISFC.Models.Base.Item item)
		{
			// ��ˮ��
			this.parmItem[(int)enumItem.Sequence] = item.Memo;
			// ���ұ���
			this.parmItem[(int)enumItem.DepartmentCode] = item.User01;
			// ��Ŀ����
			this.parmItem[(int)enumItem.ItemCode] = item.ID;
			// �Ƿ�ҩƷ
			//if (item.IsPharmacy)
            if (item.ItemType == EnumItemType.Drug)
			{
				this.parmItem[(int)enumItem.IsPhamacy] = "1";
			}
			else
			{
				this.parmItem[(int)enumItem.IsPhamacy] = "0";
			}
			// ����Ա����
			this.parmItem[(int)enumItem.OperatorCode] = item.User02;
			// ����ʱ��
			this.parmItem[(int)enumItem.OperateDate] = item.User03;
		}
		#endregion
		#region ת��Reader��Object
		/// <summary>
		/// ת��Reader��Object
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		private void ReaderToObject(FS.HISFC.Models.Base.Item item)
		{
			// ��ˮ��
			item.Memo = this.Reader[(int)enumItem.Sequence].ToString();
			// ���ұ���
			item.User01 = this.Reader[(int)enumItem.DepartmentCode].ToString();
			// ��Ŀ����
			item.ID = this.Reader[(int)enumItem.ItemCode].ToString();
			// �Ƿ�ҩƷ
			if (this.Reader[(int)enumItem.IsPhamacy].Equals("1"))
			{
				//item.IsPharmacy = true;
                item.ItemType = EnumItemType.Drug;
			}
			else
			{
				//item.IsPharmacy = false;
                item.ItemType = EnumItemType.UnDrug;
			}
			// ����Ա����
			item.User02 = this.Reader[(int)enumItem.OperatorCode].ToString();
			// ����ʱ��
			item.User03 = this.Reader[(int)enumItem.OperateDate].ToString();
			// ��������
			item.SpellCode = this.Reader[(int)enumItem.DepartmentName].ToString();
			// ��Ŀ����
			item.WBCode = this.Reader[(int)enumItem.ItemName].ToString();
		}
		#endregion
		#region ת��Reader�ɶ�������
		/// <summary>
		/// ת��Reader�ɶ�������
		/// [����: ArrayList alItem - ��������]
		/// </summary>
		/// <param name="alItem">��������</param>
		private void ReaderToArrayList(ArrayList alItem)
		{
			while (this.Reader.Read())
			{
				// ʵ��
				FS.HISFC.Models.Base.Item item = new Item();

				// ת���ɶ���
				this.ReaderToObject(item);

				// ��ӽ���������
				alItem.Add(item);
			}

		}
		#endregion
		
		#endregion

		#region ���к���

		#region ������Ŀ
		/// <summary>
		/// ������Ŀ
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// [����: int,1-�ɹ�,-1-ʧ��]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		/// <returns>1-�ɹ�,-1-ʧ��</returns>
		protected int Insert(FS.HISFC.Models.Base.Item item)
		{
			//
			// ��ʼ������
			//
			this.InitVar();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.WorkloadItem.Create", ref this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��������Ŀά����ȡSQL���ʧ��!" + "\n" + this.Err + "\n FS.HISFC.BizLogic.Terminal.WorkloadItem.Create";
				return -1;
			}
			//
			// �������
			//
			this.FillParm(item);
			//
			// ƥ��SQL���
			//
			try
			{
				this.SQL = string.Format(this.SQL,
										// ���ұ���
										this.parmItem[(int)enumItem.DepartmentCode],
										// ��Ŀ����
										this.parmItem[(int)enumItem.ItemCode],
										// �Ƿ�ҩƷ
										this.parmItem[(int)enumItem.IsPhamacy],
										// ����Ա
										this.parmItem[(int)enumItem.OperatorCode]);
			}
			catch
			{
				this.Err = "��������Ŀά��ƥ��SQL���ʧ��!";
				return -1;
			}
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecNoQuery(this.SQL);
			if (this.intReturn <= 0)
			{
				this.Err = "��������Ŀά��ִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion
		#region ������Ŀ
		/// <summary>
		/// ������Ŀ
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// [����: int,1-�ɹ�,-1-ʧ��]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		/// <returns>1-�ɹ�,-1-ʧ��</returns>
		protected int Update(FS.HISFC.Models.Base.Item item)
		{
			//
			// ��ʼ������
			//
			this.InitVar();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.WorkloadItem.Update", ref this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��������Ŀά����ȡSQL���ʧ��!" + "\n" + this.Err + "\n FS.HISFC.BizLogic.Terminal.WorkloadItem.Update";
				return -1;
			}
			//
			// �������
			//
			this.FillParm(item);
			//
			// ƥ��SQL���
			//
			try
			{
				this.SQL = string.Format(this.SQL,
					// ��ˮ��
					this.parmItem[(int)enumItem.Sequence],
					// ���ұ���
					this.parmItem[(int)enumItem.DepartmentCode],
					// ��Ŀ����
					this.parmItem[(int)enumItem.ItemCode],
					// �Ƿ�ҩƷ
					this.parmItem[(int)enumItem.IsPhamacy],
					// ����Ա
					this.parmItem[(int)enumItem.OperatorCode]);
			}
			catch
			{
				this.Err = "��������Ŀά��ƥ��SQL���ʧ��!";
				return -1;
			}
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecNoQuery(this.SQL);
			if (this.intReturn <= 0)
			{
				this.Err = "��������Ŀά��ִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion
		#region ������Ŀ
		/// <summary>
		/// ������Ŀ
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		/// <returns>1-�ɹ�,-1ʧ��</returns>
		protected int Save(ref FS.HISFC.Models.Base.Item item)
		{
			//
			// ������ˮ���ж��Ƿ�������Ŀ
			//
			if (item.Memo.Equals("") || item.Memo == null)
			{
				// �����ˮ��Ϊ��,��ô������Ŀ
				return this.Insert(item);
			}
			else
			{
				return this.Update(item);
			}
		}
		#endregion
		#region ɾ����Ŀ
		/// <summary>
		/// ɾ����Ŀ
		/// [����: FS.HISFC.Models.Base.Item item - ��Ŀ]
		/// </summary>
		/// <param name="item">��Ŀ</param>
		/// <returns>1-�ɹ�,-1ʧ��</returns>
		protected int Delete(FS.HISFC.Models.Base.Item item)
		{
			//
			// ��ʼ������
			//
			this.InitVar();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.WorkloadItem.Delete", ref this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��������Ŀά����ȡSQL���ʧ��!" + "\n" + this.Err + "\n FS.HISFC.Management.MedTech.WorkloadItem.Delete";
				return -1;
			}
			//
			// ת�����������
			//
			this.FillParm(item);
			if (this.parmItem[(int)enumItem.Sequence] == null || this.parmItem[(int)enumItem.Sequence].Equals(""))
			{
				this.parmItem[(int)enumItem.Sequence] = "0";
			}
			//
			// ƥ��SQL���
			//
			try
			{
				this.SQL = string.Format(this.SQL, this.parmItem[(int)enumItem.Sequence]);
			}
			catch
			{
				this.Err = "��������Ŀά��ƥ��SQL���ʧ��!";
				return -1;
			}
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecNoQuery(this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��������Ŀά��ִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion

		#region ��ȡ������Ϣ
		/// <summary>
		/// ��ȡ������Ϣ
		/// [����: ArrayList alDepartment - ���صĿ�������]
		/// [����: int,1-�ɹ�,-1-ʧ��]
		/// </summary>
		/// <param name="alDepartment">���صĿ�������</param>
		/// <returns>1-�ɹ�,-1-ʧ��</returns>
		public int QueryDapartment(ArrayList alDepartment)
		{
			//
			// ��ʼ������
			//
			this.InitVar();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetDapartment.Select", ref this.SELECT);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ������Ϣ��ȡSQL���ʧ��!" + "\n" + this.Err + "\nFS.HISFC.Management.MedTech.GetDapartment.Select";
				return -1;
			}
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetDapartment.Where", ref this.WHERE);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ������Ϣ��ȡSQL���ʧ��!" + "\n" + this.Err + "\nFS.HISFC.Management.MedTech.GetDapartment.Where";
				return -1;
			}
			this.CreateSQL();
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecQuery(this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ������Ϣִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// ���ؽ��
			//
			while (this.Reader.Read())
			{
				// ���Ҷ���
				FS.FrameWork.Models.NeuObject department = new NeuObject();

				// ���ұ���
				department.ID = this.Reader[0].ToString();
				// ��������
				department.Name = this.Reader[1].ToString();

				// ��ӵ���������
				alDepartment.Add(department);
			}
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion
		#region ���ݿ��ұ����ȡ��Ŀ��Ϣ
		/// <summary>
		/// ���ݿ��ұ����ȡ��Ŀ��Ϣ
		/// [����1: string departmentCode - ���ұ���]
		/// [����2: ArrayList alItem - ��Ŀ����]
		/// [����: int,1-�ɹ�,-1-ʧ��]
		/// </summary>
		/// <param name="departmentCode">���ұ���</param>
		/// <param name="alItem">��Ŀ��Ϣ����</param>
		/// <returns>1-�ɹ�,-1-ʧ��</returns>
		public int QueryItemsByDepartmentCode(string departmentCode, ArrayList alItem)
		{
			//
			// ��ʼ������
			//
			this.CreateSQL();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetItemsByDepartmentCode.Select", ref this.SELECT);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣʧ��!" + "\n" + this.Err;
				return -1;
			}
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetItemsByDepartmentCode.Where", ref this.WHERE);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣʧ��!" + "\n" + this.Err;
				return -1;
			}
			this.CreateSQL();
			//
			// ƥ��SQL���
			//
			try
			{
				this.SQL = string.Format(this.SQL, departmentCode);
			}
			catch
			{
				this.Err = "��ȡ��Ŀ��Ϣƥ��SQL���ʧ��!";
				return -1;
			}
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecQuery(this.SQL);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// ת��Reader������
			//
			this.ReaderToArrayList(alItem);
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion

		#region ���ݿ��ұ����ȡ��������������
		/// <summary>
		/// ���ݿ��ұ����ȡ��������������
		/// [����1: string departmentCode - ���ұ���]
		/// [����2: System.Data.DataSet dsResult - ��������]
		/// [����3: System.DateTime dateFrom - ��ʼʱ��]
		/// [����4: System.DateTime dateTo - ��ֹʱ��]
		/// [����: int,1-�ɹ�,-1-ʧ��]
		/// </summary>
		/// <param name="departmentCode">���ұ���</param>
		/// <param name="dsResult">��������</param>
		/// <param name="dateFrom">��ʼʱ��</param>
		/// <param name="dateTo">��ֹʱ��</param>
		/// <returns>1-�ɹ�,-1-ʧ��</returns>
		public int QueryReportByDepartmentCode(string departmentCode, System.Data.DataSet dsResult, System.DateTime dateFrom, System.DateTime dateTo)
		{
			//
			// ��ʼ������
			//
			this.CreateSQL();
			//
			// ��ȡSQL���
			//
			this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetReportByDepartmentCode.Select", ref this.SELECT);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣʧ��!" + "\n" + this.Err;
				return -1;
			}
			if (departmentCode.Equals(""))
			{
				this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetReportByDepartmentCode.Where.All", ref this.WHERE);
			}
			else
			{
				this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetReportByDepartmentCode.Where", ref this.WHERE);
			}
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣʧ��!" + "\n" + this.Err;
				return -1;
			}
			this.CreateSQL();
			//
			// ƥ��SQL���
			//
			try
			{
				if (departmentCode.Equals(""))
				{
					this.SQL = string.Format(this.SQL, dateFrom.ToString(), dateTo.ToString());
				}
				else
				{
					this.SQL = string.Format(this.SQL, dateFrom.ToString(), dateTo.ToString(), departmentCode);
				}
			}
			catch
			{
				this.Err = "��ȡ��Ŀ��Ϣƥ��SQL���ʧ��!";
				return -1;
			}
			//
			// ִ��SQL���
			//
			this.intReturn = this.ExecQuery(this.SQL, ref dsResult);
			if (this.intReturn == -1)
			{
				this.Err = "��ȡ��Ŀ��Ϣִ��SQL���ʧ��!" + "\n" + this.Err;
				return -1;
			}
			//
			// �ɹ�����
			//
			return 1;
		}
		#endregion
		
		#endregion 

	}
}
