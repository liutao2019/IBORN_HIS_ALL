using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table
{
	/// DeptItem <br></br>
	/// [��������: ����������Ŀ��ϵ�� Met_PE_DeptItem]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class DeptItem : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[12];

		#endregion

		#region ˽�к���

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		private void ClearFields()
		{
			for (int i=0;i<=11;i++)
			{
				this.fields[i] = "";
			}
		}

		/// <summary>
		/// ת��ReaderΪ����
		/// </summary>
		/// <param name="relation">�������������Ŀ�Ĺ�ϵ��</param>
		private void ReaderToObject( ref FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation relation )
		{
			relation.Business.ID = this.Reader[0].ToString();
			relation.Business.Name = this.Reader[1].ToString();
			relation.ID = this.Reader[2].ToString();
			relation.Name = this.Reader[3].ToString();
			relation.Item.ID = this.Reader[4].ToString();
			relation.Item.Name = this.Reader[5].ToString();
			relation.Memo = this.Reader[6].ToString();
			relation.CreateEnvironment.ID = this.Reader[7].ToString();
			relation.CreateEnvironment.Name = this.Reader[8].ToString();
			relation.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
			relation.InvalidEnvironment.ID = this.Reader[10].ToString();
			relation.InvalidEnvironment.Name = this.Reader[11].ToString();
			relation.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());
			if (this.Reader[12].ToString().Equals("1"))
			{
				relation.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				relation.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
			relation.User01 = this.Reader[13].ToString();
			relation.User02 = this.Reader[14].ToString();
			relation.User03 = this.Reader[15].ToString();
			relation.SpellCode = this.Reader[16].ToString();
			relation.WBCode = this.Reader[17].ToString();
			relation.UserCode = this.Reader[18].ToString();
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">����������Ŀ�Ĺ�ϵ��</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Insert(NeuObject record)
		{
			// ת��������������Ŀ�Ĺ�ϵ��
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation relation = (FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( relation );

			// ��ȡSQL���
			if (this.Sql.GetSql( "", ref this.SQL ) == -1)
			{
				return -1;
			}

			// ִ��SQL���
			if (this.ExecNoQuery( this.SQL, this.fields ) == -1)
			{
				return -1;
			}

			// �ɹ�����
			return 1;
		}

		/// <summary>
		/// ���±�
		/// </summary>
		/// <param name="record">����������Ŀ�Ĺ�ϵ��</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Update(NeuObject record)
		{
			// ת��������������Ŀ�Ĺ�ϵ��
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation relation = (FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( relation );

			// ��ȡSQL���
			if (this.Sql.GetSql( "", ref this.SQL ) == -1)
			{
				return -1;
			}

			// ִ��SQL���
			if (this.ExecNoQuery( this.SQL, this.fields ) == -1)
			{
				return -1;
			}

			// �ɹ�����
			return 1;
		}

		/// <summary>
		/// ������ѯ
		/// </summary>
		/// <param name="recordList">����������Ŀ�Ĺ�ϵ������</param>
		/// <param name="whereCondition">SQL���Where����</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Select(ref ArrayList recordList, string whereCondition)
		{
			this.SQL = "";

			// ��ȡSQL���
			if( this.Sql.GetSql("", ref this.SQL) ==  -1)
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
			if( this.Sql.ExecQuery(this.SQL) == -1 )
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
		/// <param name="record">����������Ŀ�Ĺ�ϵ��</param>
		public void FillFields(NeuObject record)
		{
			// ����������Ŀ�Ĺ�ϵ
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation relation = (FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation)record;

			// ����ֶ�����
			this.ClearFields();

			// ����ֶ�����
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Business] = relation.Business.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Department] = relation.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Item] = relation.Item.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Memo] = relation.Memo;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.CreateOper] = relation.CreateEnvironment.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.CreateTime] = relation.CreateEnvironment.OperTime.ToString();
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.InvalidOper] = relation.InvalidEnvironment.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.InvalidTime] = relation.InvalidEnvironment.OperTime.ToString();
			if (relation.Validity.Equals(FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid))
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.IsValid] = "1";
			}
			else
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.IsValid] = "0";
			}
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Extend1] = relation.User01;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Extend2] = relation.User02;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumDeptItem.Extend3] = relation.User03;
		}

		/// <summary>
		/// �γ�����������Ŀ�Ĺ�ϵ������
		/// </summary>
		/// <param name="recordList">����������Ŀ�Ĺ�ϵ������</param>
		public void ReturnArray(ref ArrayList recordList)
		{
			
			// ����������Ŀ�Ĺ�ϵ��
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation relation;

			// ѭ���������
			while (this.Reader.Read())
			{
				relation = new FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptItemRelation();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref relation);

				recordList.Add(relation);
			}
		}

		#endregion
	}
}
