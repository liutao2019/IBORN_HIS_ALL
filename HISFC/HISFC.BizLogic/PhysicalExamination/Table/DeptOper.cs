using System;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table
{
	/// DeptOper <br></br>
	/// [��������: ����������Ա�Ĺ�ϵ�� Met_PE_Dept_Oper]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class DeptOper : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
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
		/// <param name="archieve">����������Ա��</param>
		private void ReaderToObject(ref FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve)
		{
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">����������Ա�Ĺ�ϵ��</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Insert(NeuObject record)
		{
			// ת��������������Ա�Ĺ�ϵ��
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptUserRelation relation = (FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptUserRelation)record;

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
		/// <param name="record">����������Ա�Ĺ�ϵ��</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Update(NeuObject record)
		{
			// ת��������������Ա�Ĺ�ϵ��
			FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptUserRelation relation = (FS.HISFC.Models.PhysicalExamination.Management.Relation.DeptUserRelation)record;

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
		/// <param name="recordList">����������Ա�Ĺ�ϵ������</param>
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
		/// <param name="record">����������Ա�Ĺ�ϵ��</param>
		public void FillFields(NeuObject record)
		{
			this.ClearFields();
		}

		public void ReturnArray(ref ArrayList recordList)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
