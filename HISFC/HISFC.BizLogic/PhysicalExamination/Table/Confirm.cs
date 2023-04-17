using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.PhysicalExamination.Table 
{
	/// Confirm <br></br>
	/// [��������: ���ִ�б� Met_PE_Confirm]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-11-17]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class Confirm : FS.HISFC.BizLogic.PhysicalExamination.Base.BaseFunction, FS.HISFC.BizLogic.PhysicalExamination.Base.TableInterface 
	{
		#region ˽�б���

		/// <summary>
		/// ʹ�õ�SQL���
		/// </summary>
		private string SQL = "";

		/// <summary>
		/// �ֶ�����
		/// </summary>
		private string [] fields = new string[15];

		#endregion

		#region ˽�к���

		/// <summary>
		/// ����ֶ�����
		/// </summary>
		private void ClearFields()
		{
			for (int i=0;i<=14;i++)
			{
				this.fields[i] = "";
			}
		}

		/// <summary>
		/// ת��ReaderΪ����
		/// </summary>
		/// <param name="confirm">���ִ����</param>
		private void ReaderToObject( ref FS.HISFC.Models.PhysicalExamination.Confirm.Confirm confirm )
		{
			confirm.ID = this.Reader[0].ToString();
			confirm.RegItem.ID = this.Reader[1].ToString();
			confirm.RegItem.Name = this.Reader[2].ToString();
			if (this.Reader[3].ToString().Equals("1"))
			{
				confirm.RegItem.IsNeedPrecontract = true;
			}
			else
			{
				confirm.RegItem.IsNeedPrecontract = false;
			}
			if (this.Reader[4].ToString().Equals("1"))
			{
				confirm.RegItem.IsPharmacy = true;
			}
			else
			{
				confirm.RegItem.IsPharmacy = false;
			}
			confirm.ExecDept.ID = this.Reader[5].ToString();
			confirm.ExecDept.Name = this.Reader[6].ToString();
			confirm.ConfirmDept.ID = this.Reader[7].ToString();
			confirm.ConfirmDept.Name = this.Reader[8].ToString();
			confirm.ConfirmOper.ID = this.Reader[9].ToString();
			confirm.ConfirmOper.Name = this.Reader[10].ToString();
			confirm.ConfirmTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());
			confirm.ItemResult = this.Reader[12].ToString();
			confirm.CreateEnvironment.ID = this.Reader[13].ToString();
			confirm.CreateEnvironment.Name = this.Reader[14].ToString();
			confirm.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
			confirm.InvalidEnvironment.ID = this.Reader[16].ToString();
			confirm.InvalidEnvironment.Name = this.Reader[17].ToString();
			confirm.InvalidEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
			if (this.Reader[19].ToString().Equals("1"))
			{
				confirm.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid;
			}
			else
			{
				confirm.Validity = FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Invalid;
			}
			confirm.User01 = this.Reader[20].ToString();
			confirm.User02 = this.Reader[21].ToString();
			confirm.User03 = this.Reader[22].ToString();
		}

		#endregion

		#region �ӿں���

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="record">���ִ����</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Insert( NeuObject record )
		{
			// ת�������ִ����
			FS.HISFC.Models.PhysicalExamination.Confirm.Confirm confirm = (FS.HISFC.Models.PhysicalExamination.Confirm.Confirm)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( confirm );

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
		/// <param name="record">���ִ����</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Update( NeuObject record )
		{
			// ת�������ִ����
			FS.HISFC.Models.PhysicalExamination.Confirm.Confirm confirm = (FS.HISFC.Models.PhysicalExamination.Confirm.Confirm)record;

			this.SQL = "";

			// ת�����ֶ�����
			this.FillFields( confirm );

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
		/// <param name="recordList">���ص����ִ��������</param>
		/// <param name="whereCondition">SQL����Where����</param>
		/// <returns>1���ɹ���0��ʧ��</returns>
		public int Select( ref ArrayList recordList, string whereCondition )
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
		/// <param name="record">���ִ����</param>
		public void FillFields( NeuObject record )
		{
			// ���ִ����
			FS.HISFC.Models.PhysicalExamination.Confirm.Confirm confirm = (FS.HISFC.Models.PhysicalExamination.Confirm.Confirm)record;

			// ����ֶ�����
			this.ClearFields();

			// ���ֶ����鸳ֵ
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ConfirmID] = confirm.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.RegItemID] = confirm.RegItem.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ExecDept] = confirm.ExecDept.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ConfirmDept] = confirm.ConfirmDept.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ConfirmOper] = confirm.ConfirmOper.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ConfirmTime] = confirm.ConfirmTime.ToString();
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.ItemResult] = confirm.ItemResult;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.CreateOper] = confirm.CreateEnvironment.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.CreateTime] = confirm.CreateEnvironment.OperTime.ToString();
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.InvalidOper] = confirm.InvalidEnvironment.ID;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.InvalidTime] = confirm.InvalidEnvironment.OperTime.ToString();
			if (confirm.Validity.Equals(FS.HISFC.Models.PhysicalExamination.Enum.EnumValidity.Valid))
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.IsValid] = "1";
			}
			else
			{
				this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.IsValid] = "0";
			}
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.Extend1] = confirm.User01;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.Extend2] = confirm.User02;
			this.fields [(int)FS.HISFC.BizLogic.PhysicalExamination.Table.Enum.EnumConfirm.Extend3] = confirm.User03;
		}

		/// <summary>
		/// �γɷ��ص����ִ��������
		/// </summary>
		/// <param name="recordList">���ִ��������</param>
		public void ReturnArray( ref ArrayList recordList )
		{
			// ���ִ����
			FS.HISFC.Models.PhysicalExamination.Confirm.Confirm confirm;

			// ѭ���������
			while (this.Reader.Read())
			{
				confirm = new FS.HISFC.Models.PhysicalExamination.Confirm.Confirm();

				// ת��ReaderΪ�����
				this.ReaderToObject(ref confirm);

				recordList.Add(confirm);
			}
		}

		#endregion
	}
}
