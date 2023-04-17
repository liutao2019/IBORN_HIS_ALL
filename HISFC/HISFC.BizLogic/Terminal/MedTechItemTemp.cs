using System;
using System.Collections;
using FS.FrameWork.Function;


namespace FS.HISFC.BizLogic.Terminal
{
	/// <summary>
	/// MedTechItemTemp <br></br>
	/// [��������: ҽ��ԤԼ�Ű�ģ��]<br></br>
	/// [�� �� ��: δ֪]<br></br>
	/// [����ʱ��: ]<br></br>
	/// <�޸ļ�¼
	///		�޸���='��һ��'
	///		�޸�ʱ��='2007��03��06'
	///		�޸�Ŀ��='�汾�ع�'
	///		�޸�����=''
	///  />
	/// </summary>
	public class MedTechItemTemp : FS.FrameWork.Management.Database
	{
		public MedTechItemTemp()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ˽�к���
		
		/// <summary>
		/// ҽ��ԤԼ�Ű�ģ�壬������һ�����߶���
		/// ˽�з����������������е���
		/// </summary>
		/// <param name="SQL">SQL���</param>
		/// <returns>ҽ��ԤԼ�Ű�ģ����Ϣ��������</returns>
		private ArrayList GetItem(string SQL)
		{
			// ���ص�ҽ��ԤԼ�Ű�ģ������
			ArrayList tempList = new ArrayList();
			//
			//ִ�в�ѯ���
			//
			if (this.ExecQuery(SQL) == -1)
			{
				this.Err = "���ҽ��ԤԼ�Ű�ģ����Ϣʱ��ִ��SQL������" + this.Err;
				this.ErrCode = "-1";
				return null;
			}
			try
			{
				while (this.Reader.Read())
				{
					//��ʱҽ��ԤԼ�Ű�ģ��
					FS.HISFC.Models.Terminal.MedTechItemTemp Item = new FS.HISFC.Models.Terminal.MedTechItemTemp();
					// �Ƿ���Ч
					bool bl = true;
					
					if (this.Reader[11].ToString().ToUpper() == "FALSE" || this.Reader[11].ToString() == "0")
					{
						bl = false;
					}
					Item.MedTechItem.Item.ID = this.Reader[2].ToString();
					Item.MedTechItem.Item.Name = this.Reader[3].ToString();
					Item.MedTechItem.ItemExtend.UnitFlag = this.Reader[4].ToString();
					Item.MedTechItem.ItemExtend.Dept.ID = this.Reader[5].ToString();
					Item.Dept.Name = this.Reader[6].ToString();
					Item.Week = this.Reader[7].ToString();
					Item.NoonCode = this.Reader[8].ToString();
					Item.BookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
					Item.SpecialBookLmt = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
					Item.MedTechItem.Item.IsValid = bl;
					Item.MedTechItem.Item.Notice = this.Reader[12].ToString();
					Item.MedTechItem.Item.Oper.ID = this.Reader[13].ToString();
					Item.MedTechItem.Item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    Item.TmpFlag = this.Reader [ 15 ].ToString ( );
                    //{FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
                    if (this.Reader.FieldCount > 15)
                    {
                        Item.StartTime = this.Reader[16].ToString();
                        Item.EndTime = this.Reader[17].ToString();
                    }

					tempList.Add(Item);
				}
			}
			catch (Exception ex)
			{
				this.Err = "���ҽ��ԤԼ�Ű�ģ����Ϣʱ����" + ex.Message;
				this.ErrCode = "-1";
				return null;
			}
			this.Reader.Close();
			this.ProgressBarValue = -1;
			//
			// �ɹ�����
			//
			return tempList;
		}

		/// <summary>
		/// ��ʵ������Է���������
		/// </summary>
		/// <param name="medTechItemTemp">ҽ��ԤԼģ��</param>
		/// <returns>�ֶ�����</returns>
		private string [] GetParam(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			string valid = "0";
			if (medTechItemTemp.MedTechItem.Item.IsValid)
			{
				valid = "1"; //�����Ч
			}
			string[] str = new string[]
						{
							//��Ŀ����
							medTechItemTemp.MedTechItem.Item.ID,
							//��Ŀ����
							medTechItemTemp.MedTechItem.Item.Name,
							//��λ��ʶ
							medTechItemTemp.MedTechItem.ItemExtend.UnitFlag,
							//���Ҵ���
							medTechItemTemp.MedTechItem.ItemExtend.Dept.ID,
							//��������
							medTechItemTemp.Dept.Name,   
							//����
							medTechItemTemp.Week,
							//���
							medTechItemTemp.NoonCode,
							//ԤԼ���
							medTechItemTemp.BookLmt.ToString(),
							//����ԤԼ���
							medTechItemTemp.SpecialBookLmt.ToString(),
							//��Ч��ʶ
							valid,
							//ע������
							medTechItemTemp.MedTechItem.Item.Notice,
							//����Ա
							medTechItemTemp.MedTechItem.Item.Oper.ID,
							//13����ʱ��
							medTechItemTemp.MedTechItem.Item.Oper.OperTime.ToString(), 
                            //��ʶλ
                            medTechItemTemp.TmpFlag.ToString(),
                            //��ʼʱ��          {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
                            medTechItemTemp.StartTime,
                            //����ʱ��          {FAA10645-3E78-4866-BA0F-E4F2FF7CD8FD}
                            medTechItemTemp.EndTime
						};
			return str;
		}
		
		#endregion

		#region ���к���

		/// <summary>
		/// ����ҽ��ԤԼ�Ű�ģ��
		/// </summary>
		/// <param name="medTechItemTemp">ҽ��ԤԼ�Ű�ģ��</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int Insert(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			// SQL���
			string strSql = "";
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.ItemTemp.Insert", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.ItemTemp.Insertʧ��!";
				return -1;
			}
			// ƥ��SQL���
			try
			{
                //strSql = string.Format(strSql, GetParam(medTechItemTemp));
				// ִ�� 
                return this.ExecNoQuery(strSql, GetParam(medTechItemTemp));
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ����ҽ��ԤԼ�Ű�ģ��
		/// </summary>
		/// <param name="medTechItemTemp">ҽ��ԤԼģ��</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
        [Obsolete("���,Sql���������,�޸��ˣ�·־��",true)]
		public int Update(FS.HISFC.Models.Terminal.MedTechItemTemp medTechItemTemp)
		{
			// SQL���
			string strSql = "";
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.ItemTemp.Update", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.ItemTemp.Updateʧ��!";
				return -1;
			}
			// ƥ��SQL���
			try
			{
                //strSql = string.Format(strSql, GetParam(medTechItemTemp));
                return this.ExecNoQuery(strSql, GetParam(medTechItemTemp));
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ɾ��ҽ��ԤԼ�Ű�ģ��   
		/// </summary>
		/// <param name="DeptCode">���ұ���</param>
		/// <param name="ItemCode">��Ŀ����</param>
		/// <param name="Week">����</param>
		/// <param name="Noon">���</param>
		/// <returns>��1��ʧ�ܣ�Ӱ�������</returns>
		public int Delete(string DeptCode, string ItemCode, string Week, string Noon)
		{
			// SQL���
			string strSql = "";
			// ��ȡSQL���
			if (this.Sql.GetSql("MedTech.ItemTemp.DelItemTemp", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.ItemTemp.DelItemTempʧ��!";
				return -1;
			}
			// ƥ��SQL���
			try
			{
				strSql = string.Format(strSql, DeptCode, ItemCode, Week, Noon);
				
				return this.ExecNoQuery(strSql);
			}
			catch (Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}

		/// <summary>
		/// ���ݿ��Ҵ������ҽ��ԤԼ�Ű�ģ��
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <returns>ҽ��ԤԼ�Ű�ģ��</returns>
		public ArrayList QueryTemp(string deptCode)
		{
			// SQL���
			string strSql = "";
			// ҽ��ԤԼ�Ű�ģ������
			ArrayList tempList = new ArrayList();
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.ItemTemp.Query.1", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.ItemTemp.Query.1ʧ��";
				return null;
			}
			//
			// ƥ��SQL���
			//
			strSql = string.Format(strSql, deptCode);
			// ִ�в�ѯ 
			tempList = this.GetItem(strSql);
			// ���ؽ��
			if (tempList == null)
			{
				return null;
			}
			return tempList;
		}

		/// <summary>
		/// ���ݿ��Ҵ�������ڲ���ҽ��ԤԼ�Ű�ģ��
		/// </summary>
		/// <param name="deptCode">���ұ���</param>
		/// <param name="week">����</param>
		/// <returns>ҽ��ԤԼ�Ű�ģ��</returns>
		public ArrayList QueryTemp(string deptCode, string week)
		{
			// SQL���
			string strSql = "";
			// ҽ��ԤԼ�Ű�ģ������
			ArrayList tempList = new ArrayList();
			//
			// ��ȡSQL���
			//
			if (this.Sql.GetSql("MedTech.ItemTemp.Query.2", ref strSql) == -1)
			{
				this.Err = "��ȡSQL���MedTech.ItemTemp.Query.2ʧ��";
				return null;
			}
			// ƥ��SQL���
			strSql = string.Format(strSql, deptCode, week);
			// ��ѯ
			tempList = this.GetItem(strSql);
			//
			// ���ؽ��
			//
			if (tempList == null)
			{
				return null;
			}
			return tempList;
		}

		#endregion 
	}
}

