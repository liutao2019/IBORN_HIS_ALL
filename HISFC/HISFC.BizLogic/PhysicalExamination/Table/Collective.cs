using System.Collections;
using FS.HISFC.BizLogic.PhysicalExamination.Table.Enum;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table
{
	/// Collective <br></br>
	/// [��������: ���塢��˾�� Met_PE_Collective]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Collective : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[14];

		#endregion

		#region ˽�к���

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		private void ClearFields()
		{
			for (int i=0;i<=12;i++)
			{
				this.fields[i] = "";
			}
		}

		/// <summary>
		/// ת��ReaderΪ����
		/// </summary>
		/// <param name="collective">������</param>
		private void ReaderToObject( ref FS.HISFC.Models.PhysicalExamination.Collective.Collective collective )
		{
			collective.Hospital.ID = this.Reader[0].ToString();
			collective.ID = this.Reader[1].ToString();
			collective.Code = this.Reader[2].ToString();
			collective.Name = this.Reader[3].ToString();
			collective.Memo = this.Reader[4].ToString();
			collective.CollectiveType.ID = this.Reader[5].ToString();
			collective.CollectiveType.Name = this.Reader[6].ToString();
			collective.CreateEnvironment.ID = this.Reader[7].ToString();
			collective.CreateEnvironment.Name = this.Reader[8].ToString();
			collective.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
			collective.InvalidEnvironment.ID = this.Reader[10].ToString();
			collective.InvalidEnvironment.Name = this.Reader[11].ToString();
			collective.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
			if (this.Reader[13].ToString().Equals("1"))
			{
				collective.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				collective.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
			collective.SpellCode = this.Reader[14].ToString();
			collective.WBCode = this.Reader[15].ToString();
			collective.UserCode = this.Reader[16].ToString();
			collective.User01 = this.Reader[17].ToString();
			collective.User02 = this.Reader[18].ToString();
			collective.User03 = this.Reader[19].ToString();
			
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">������</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Insert(NeuObject record)
		{
			
			// ת���ɼ�����
			FS.HISFC.Models.PhysicalExamination.Collective.Collective collective = (FS.HISFC.Models.PhysicalExamination.Collective.Collective)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( collective );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Collective.Insert", ref this.SQL) == -1)
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
		/// <param name="record">������</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Update(NeuObject record)
		{
			// ת���ɼ�����
			FS.HISFC.Models.PhysicalExamination.Collective.Collective collective = (FS.HISFC.Models.PhysicalExamination.Collective.Collective)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( collective );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Collective.Update", ref this.SQL) == -1)
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
		/// <param name="recordList">����������</param>
		/// <param name="whereCondition">SQL����Where����</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Select(ref ArrayList recordList, string whereCondition)
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Collective.Select", ref this.SQL) == -1)
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
		/// <param name="record">������</param>
		public void FillFields(NeuObject record)
		{
			// ������
			FS.HISFC.Models.PhysicalExamination.Collective.Collective collective  = (FS.HISFC.Models.PhysicalExamination.Collective.Collective)record;

			// ����ֶ�����
			this.ClearFields();

			// ���ֶ����鸳ֵ
			this.fields[(int)EnumCollective.Hospital] = this.GetSequence("FS.HISFC.BizLogic.PhysicalExamination.Table.GetHospital");
			this.fields[(int)EnumCollective.CollectiveID] = collective.ID;
			this.fields[(int)EnumCollective.CollectiveCode] = collective.Code;
			this.fields[(int)EnumCollective.CollectiveName] = collective.Name;
			this.fields[(int)EnumCollective.CollectiveType] = collective.CollectiveType.ID;
			this.fields[(int)EnumCollective.Memo] = collective.Memo;
			this.fields[(int)EnumCollective.CreateOper] = collective.CreateEnvironment.ID;
			this.fields[(int)EnumCollective.CreateTime] = collective.CreateEnvironment.OperTime.ToString();
			this.fields[(int)EnumCollective.InvalidOper] = collective.InvalidEnvironment.ID;
			this.fields[(int)EnumCollective.InvalidTime] = collective.InvalidEnvironment.OperTime.ToString();
			if (collective.Validity.Equals(FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid))
			{
				this.fields[(int)EnumCollective.IsValid] = "1";
			}
			else
			{
				this.fields[(int)EnumCollective.IsValid] = "0";
			}
			this.fields[(int)EnumCollective.Extend1] = collective.User01;
			this.fields[(int)EnumCollective.Extend2] = collective.User02;
			this.fields[(int)EnumCollective.Extend3] = collective.User03;
		}

		/// <summary>
		/// �γɼ���������
		/// </summary>
		/// <param name="recordList">����������</param>
		public void ReturnArray(ref ArrayList recordList)
		{
			// ������
			FS.HISFC.Models.PhysicalExamination.Collective.Collective collective;

			// ѭ���������
			while (this.Reader.Read())
			{
				collective = new FS.HISFC.Models.PhysicalExamination.Collective.Collective();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref collective);

				recordList.Add(collective);
			}
		}

		#endregion

		#region ��չ�Ĺ�������
		
		/// <summary>
		/// ��ȡ���м���
		/// </summary>
		/// <param name="dsCollective">��켯�����ݼ�</param>
		/// <param name="whereCondition">����Ĳ�ѯ�������</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int LoadAllCollective(ref System.Data.DataSet dsCollective, string whereCondition)
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Collective.Select.1", ref this.SQL) == -1)
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
			if (this.ExecQuery(this.SQL, ref dsCollective) == -1)
			{
				return -1;
			}

			// �ɹ�����
			return 1;
		}
		#endregion
	}
}
