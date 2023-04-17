using System;
using System.Collections;
using System.Data;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table 
{
	/// Business <br></br>
	/// [��������: ���ҵ��� Met_PE_Business]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Business : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[13];

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
		/// <param name="business">����������</param>
		private void ReaderToObject( ref FS.HISFC.Models.PhysicalExamination.Management.Business business )
		{
			
			business.Hospital.ID = this.Reader[0].ToString();
			business.ID = this.Reader [1].ToString();
			business.Name = this.Reader[2].ToString();
			// �ж�������ͣ�������컹�Ǽ������
			if (this.Reader [3].ToString().Equals("1"))
			{
				business.PEType = FS.HISFC.Models.PhysicalExamination.Enum.EnumPEType.Collective;
			}
			else
			{
				business.PEType = FS.HISFC.Models.PhysicalExamination.Enum.EnumPEType.Personal;
			}
			business.Memo = this.Reader[4].ToString();
			business.CreateEnvironment.ID = this.Reader[5].ToString();
			business.CreateEnvironment.Name = this.Reader[6].ToString();
			business.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
			business.InvalidEnvironment.ID = this.Reader[8].ToString();
			business.InvalidEnvironment.Name = this.Reader[9].ToString();
			business.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
			business.User01 = this.Reader[11].ToString();
			business.User02 = this.Reader[12].ToString();
			business.User03 = this.Reader[13].ToString();
			// �ж���Ч�ԣ���Ч������Ч
			if (this.Reader[14].ToString().Equals("1"))
			{
				business.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				business.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">���ҵ����</param>
		/// <returns>1-�ɹ���-1-ʧ��</returns>
		public int Insert(NeuObject record)
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.Management.Business business  = (FS.HISFC.Models.PhysicalExamination.Management.Business)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( business );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Business.Insert", ref this.SQL) == -1)
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
		/// <param name="record">���ҵ����</param>
		/// <returns>1-�ɹ���-1-ʧ��</returns>
		/// </summary>
		public int Update(NeuObject record)
		{
			// ת���ɽ���������
			FS.HISFC.Models.PhysicalExamination.Management.Business business = (FS.HISFC.Models.PhysicalExamination.Management.Business)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( business );

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Business.Update", ref this.SQL) == -1)
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
		/// <param name="recordList">���ҵ��������</param>
		/// <param name="whereCondition">SQL����Where����</param>
		/// <returns>1���ɹ�����1��ʧ��</returns>
		public int Select(ref ArrayList recordList, string whereCondition)
		{
			this.SQL = "";

			// ��ȡSQL���
			if (this.Sql.GetSql("FS.HISFC.BizLogic.PhysicalExamination.Table.Business.Select", ref this.SQL) == -1)
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
			if (this.ExecQuery(this.SQL) == -1)
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
		/// <param name="record">���ҵ����</param>
		public void FillFields(NeuObject record)
		{
			// ���ҵ����
			FS.HISFC.Models.PhysicalExamination.Management.Business business = (FS.HISFC.Models.PhysicalExamination.Management.Business)record;

			// �������
			this.ClearFields();

			// �������
			this.fields[(int)Table.Enum.EnumBusiness.BusinessID] = business.ID;
			this.fields[(int)Table.Enum.EnumBusiness.Hospital] = this.GetSequence("FS.HISFC.BizLogic.PhysicalExamination.Table.GetHospital");
			this.fields[(int)Table.Enum.EnumBusiness.BusinessName] = business.Name;
			if (business.PEType.Equals(FS.HISFC.Models.PhysicalExamination.Enum.EnumPEType.Personal))
			{
				this.fields [(int)Table.Enum.EnumBusiness.PEType] = "0";
			}
			else
			{
				this.fields [(int)Table.Enum.EnumBusiness.PEType] = "1";
			}
			this.fields[(int)Table.Enum.EnumBusiness.Memo] = business.Memo;
			this.fields[(int)Table.Enum.EnumBusiness.CreateOper] = business.CreateEnvironment.ID;
			this.fields[(int)Table.Enum.EnumBusiness.CreateTime] = business.CreateEnvironment.OperTime.ToString();
			this.fields[(int)Table.Enum.EnumBusiness.InvalidOper] = business.InvalidEnvironment.ID;
			this.fields[(int)Table.Enum.EnumBusiness.InvalidTime] = business.InvalidEnvironment.OperTime.ToString();
			if (business.Validity == FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid)
			{
				this.fields[(int)Table.Enum.EnumBusiness.IsValid] = "1";
			}
			else
			{
				this.fields[(int)Table.Enum.EnumBusiness.IsValid] = "0";
			}
			this.fields[(int)Table.Enum.EnumBusiness.Extend1] = business.User01;
			this.fields[(int)Table.Enum.EnumBusiness.Extend2] = business.User02;
			this.fields[(int)Table.Enum.EnumBusiness.Extend3] = business.User03;
		}

		/// <summary>
		/// �γ����ҵ��������
		/// </summary>
		/// <param name="recordList">���ҵ��������</param>
		public void ReturnArray(ref ArrayList recordList)
		{
			// ����������
			FS.HISFC.Models.PhysicalExamination.Management.Business business;

			// ѭ���������
			while (this.Reader.Read())
			{
				business = new FS.HISFC.Models.PhysicalExamination.Management.Business();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref business);

				recordList.Add(business);
			}
		}

		#endregion
	}
}
