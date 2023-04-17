using System.Collections;
using FS.HISFC.BizLogic.PhysicalExamination.Table.Enum;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table 
{
	/// Archieve <br></br>
	/// [��������: ���������� Met_PE_Archieve]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Archieve : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[25];

		#endregion

		#region ˽�к���

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		private void ClearFields()
		{
			for (int i=0;i<this.fields.Length;i++)
			{
				this.fields[i] = "";
			}
		}

		/// <summary>
		/// ת��ReaderΪ����
		/// </summary>
		/// <param name="archieve">����������</param>
		private void ReaderToObject(ref FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve)
		{
			archieve.Hospital.ID = this.Reader[0].ToString();
			archieve.ID = this.Reader[1].ToString();
			archieve.Code = this.Reader[2].ToString();
			archieve.ArchieveType.ID = this.Reader[3].ToString();
			archieve.ArchieveType.Name = this.Reader[4].ToString();
			archieve.Guest.PID.CardNO = this.Reader[5].ToString();
			archieve.Collective.ID = this.Reader[6].ToString();
			archieve.Collective.Name = this.Reader[7].ToString();
			archieve.Guest.Name = this.Reader[8].ToString();
			if (this.Reader[9].ToString() == "F")
			{
				archieve.Sex = EnumSex.F;
			}
			else if (this.Reader[9].ToString() == "M")
			{
				archieve.Sex = EnumSex.M;
			}
			else if (this.Reader[9].ToString() == "O")
			{
				archieve.Sex = EnumSex.O;
			}
			else
			{
				archieve.Sex = EnumSex.U;
			}
			archieve.Guest.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
			archieve.Guest.IDCard = this.Reader[11].ToString();
			archieve.Guest.Height = this.Reader[12].ToString();
			archieve.Guest.Weight = this.Reader[13].ToString();
			archieve.Guest.BloodType.Name = this.Reader[14].ToString();
			archieve.TotalCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15].ToString());
			archieve.TotalCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16].ToString());
			archieve.CRMType.ID = this.Reader[17].ToString();
			archieve.CRMType.Name = this.Reader[18].ToString();
			archieve.Memo = this.Reader[19].ToString();
			archieve.CreateEnvironment.ID = this.Reader[20].ToString();
			archieve.CreateEnvironment.Name = this.Reader[21].ToString();
			archieve.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString());
			archieve.InvalidEnvironment.ID = this.Reader[23].ToString();
			archieve.InvalidEnvironment.Name = this.Reader[24].ToString();
			archieve.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
			if (this.Reader[26].ToString().Equals("1"))
			{
				archieve.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				archieve.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
			archieve.User01 = this.Reader[27].ToString();
			archieve.User02 = this.Reader[28].ToString();
			archieve.User03 = this.Reader[29].ToString();
			archieve.Guest.SpellCode = this.Reader[30].ToString();
			archieve.Guest.WBCode = this.Reader[31].ToString();
			archieve.Guest.UserCode = this.Reader[32].ToString();
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">����������</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Insert( NeuObject record )
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve  = (FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( archieve );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Archieve.Insert", ref this.SQL) == -1)
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
		/// <param name="record">����������</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Update( NeuObject record )
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve = (FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( archieve );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Archieve.Update", ref this.SQL) == -1)
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
		/// <param name="recordList">���صĽ�������������</param>
		/// <param name="whereCondition">SQL����Where����</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Select( ref ArrayList recordList, string whereCondition )
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Archieve.Select", ref this.SQL) == -1)
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
		/// <param name="record">����������</param>
		public void FillFields( NeuObject record )
		{
			// ����������
			FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve = (FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve)record;

			// ����ֶ�����
			this.ClearFields();

			// ���ֶ����鸳ֵ
			this.fields[(int)EnumArchieve.Hospital] = this.GetSequence("FS.HISFC.BizLogic.PhysicalExamination.Table.GetHospital");
			this.fields [(int)EnumArchieve.ArchieveID] = archieve.ID;
			this.fields[(int) EnumArchieve.ArchieveCode] = archieve.Code;
			this.fields [(int)EnumArchieve.ArchieveType] = archieve.ArchieveType.ID;
			this.fields[(int)EnumArchieve.CardNO] = archieve.Guest.PID.CardNO;
			this.fields[(int)EnumArchieve.Collective] = archieve.Collective.ID;
			this.fields[(int) EnumArchieve.Name] = archieve.Name;
			this.fields[(int)EnumArchieve.Sex] = archieve.Sex.ToString();
			this.fields[(int) EnumArchieve.Birthday] = archieve.Guest.Birthday.ToString();
			this.fields[(int) EnumArchieve.IDCard] = archieve.Guest.IDCard;
			this.fields[(int) EnumArchieve.Height] = archieve.Guest.Height;
			this.fields[(int) EnumArchieve.Weight] = archieve.Guest.Weight;
			this.fields[(int) EnumArchieve.BloodType] = archieve.Guest.BloodType.ID.ToString();
			this.fields[(int)EnumArchieve.TotalCost] = archieve.TotalCost.ToString();
			this.fields[(int)EnumArchieve.TotalCount] = archieve.TotalCount.ToString();
			this.fields[(int)EnumArchieve.CRMType] = archieve.CRMType.ID;
			this.fields[(int)EnumArchieve.Memo] = archieve.Memo;
			this.fields [(int)EnumArchieve.CreateOper] = archieve.CreateEnvironment.ID;
			this.fields [(int)EnumArchieve.CreateTime] = archieve.CreateEnvironment.OperTime.ToString();
			this.fields[(int)EnumArchieve.InvalidOper] = archieve.InvalidEnvironment.ID;
			this.fields[(int)EnumArchieve.InvalidTime] = archieve.InvalidEnvironment.OperTime.ToString();
			this.fields[(int)EnumArchieve.IsValid] = ((int)archieve.Validity).ToString();
			this.fields [(int)EnumArchieve.Extend1] = archieve.User01;
			this.fields [(int)EnumArchieve.Extend2] = archieve.User02;
			this.fields [(int)EnumArchieve.Extend3] = archieve.User03;
		}

		/// <summary>
		/// �γɷ��صĽ�������������
		/// </summary>
		/// <param name="recordList">��������������</param>
		public void ReturnArray( ref ArrayList recordList )
		{
			// ����������
			FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve archieve;

			// ѭ���������
			while (this.Reader.Read())
			{
				archieve = new FS.HISFC.Models.PhysicalExamination.HealthArchieve.HealthArchieve();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref archieve);

				recordList.Add(archieve);
			}
		}

		#endregion

		#region ��չ����
		
		public int Select(ref System.Data.DataSet dsResult, string where)
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Archieve.Select.1", ref this.SQL) == -1)
			{
				return -1;
			}

			// ���SQL���
			if (where != "")
			{
				this.SQL += " ";
				this.SQL += where;
			}

			// ִ��SQL���
			if (this.Sql.ExecQuery(this.SQL, ref dsResult) == -1)
			{
				return -1;
			}

			// �ɹ�����
			return 1;
		}
		
		#endregion
	}
}
