using System;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table
{
	/// Dept <br></br>
	/// [��������: �����ұ� Met_PE_Dept]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Dept : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[11];

		#endregion

		#region ˽�к���

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		private void ClearFields()
		{
			for (int i=0;i<=10;i++)
			{
				this.fields[i] = "";
			}
		}

		/// <summary>
		/// ת��ReaderΪ����
		/// </summary>
		/// <param name="dept">��������</param>
		private void ReaderToObject( ref FS.HISFC.Models.PhysicalExamination.Management.Department dept )
		{
			dept.Hospital.ID = this.Reader[0].ToString();
			dept.ID = this.Reader[1].ToString();
			dept.Name = this.Reader[2].ToString();
			dept.Memo = this.Reader[3].ToString();
			dept.CreateEnvironment.ID = this.Reader[4].ToString();
			dept.CreateEnvironment.Name = this.Reader[5].ToString();
			dept.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
			dept.InvalidEnvironment.ID = this.Reader[7].ToString();
			dept.InvalidEnvironment.Name = this.Reader[8].ToString();
			dept.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
			if (this.Reader[10].ToString().Equals("1"))
			{
				dept.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				dept.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
			dept.User01 = this.Reader[11].ToString();
			dept.User02 = this.Reader[12].ToString();
			dept.User03 = this.Reader[13].ToString();
			dept.SpellCode = this.Reader[14].ToString();
			dept.WBCode = this.Reader[15].ToString();
			dept.UserCode = this.Reader[16].ToString();
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">��������</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Insert(NeuObject record)
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.Management.Department dept = (FS.HISFC.Models.PhysicalExamination.Management.Department)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( dept );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Dept.Insert", ref this.SQL) == -1)
			{
				return -1;
			}
			
			// ƥ�����
			try
			{
				this.SQL = string.Format(this.SQL, this.fields);
			}
			catch(Exception e)
			{
				this.Err += e.Message;
				return -1;
			}

			// ִ��SQL���
			if (this.ExecNoQuery( this.SQL) == -1)
			{
				return -1;
			}

			// �ɹ�����
			return 1;
		}

		/// <summary>
		/// ���±�
		/// </summary>
		/// <param name="record">��������</param>
		/// <returns>1���ɹ������ڵ���0�ɹ�</returns>
		public int Update(NeuObject record)
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.Management.Department dept = (FS.HISFC.Models.PhysicalExamination.Management.Department)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( dept );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Dept.Update", ref this.SQL) == -1)
			{
				return -1;
			}

			// ִ��SQL���
			return this.ExecNoQuery(this.SQL, this.fields);
		}

		/// <summary>
		/// ������ѯ
		/// </summary>
		/// <param name="recordList">������������</param>
		/// <param name="whereCondition">SQL����Where����</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Select(ref ArrayList recordList, string whereCondition)
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Dept.Select", ref this.SQL) == -1)
			{
				return -1;
			}

			// ���SQL���
			if (whereCondition != "")
			{
				this.SQL += " ";
				this.SQL += whereCondition;
			}
			
			// ִ��SQL���
			if( this.ExecQuery(this.SQL) == -1 )
			{
				return -1;
			}

			// �γɽ��
			this.ReturnArray(ref recordList);

			// �ɹ�����
			return 1;
		}

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		/// <param name="record">��������</param>
		public void FillFields(NeuObject record)
		{
			// ��������
			FS.HISFC.Models.PhysicalExamination.Management.Department dept = (FS.HISFC.Models.PhysicalExamination.Management.Department)record;

			// ����ֶ�����
			this.ClearFields();

			// ����ֶ�����
			this.fields[(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Hospital] = this.GetSequence("FS.HISFC.BizLogic.PhysicalExamination.Table.GetHospital");
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Dept] = dept.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Memo] = dept.Memo;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.CreateOper] = dept.CreateEnvironment.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.CreateTime] = dept.CreateEnvironment.OperTime.ToString();
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.InvalidOper] = dept.InvalidEnvironment.ID;
			this.fields[(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.InvalidTime] = dept.InvalidEnvironment.OperTime.ToString();
			if (dept.Validity.Equals(FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid))
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.IsValid] = "1";
			}
			else
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.IsValid] = "0";
			}
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Extend1] = dept.User01;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Extend2] = dept.User02;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDept.Extend3] = dept.User03;
		}

		/// <summary>
		/// �γɷ��ص�������������
		/// </summary>
		/// <param name="recordList">������������</param>
		public void ReturnArray(ref ArrayList recordList)
		{
			// ��������
			FS.HISFC.Models.PhysicalExamination.Management.Department dept;

			// ѭ���������
			while (this.Reader.Read())
			{
				dept = new FS.HISFC.Models.PhysicalExamination.Management.Department();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref dept);

				recordList.Add(dept);
			}
		}

		#endregion
	}
}
