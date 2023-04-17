using System;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// SpecialFrequency ��ժҪ˵����
	/// </summary>
	public class SpecialFrequency :FS.FrameWork.Management.Database 
	{
		public SpecialFrequency()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��ɾ��
		/// <summary>
		/// ��������Ƶ��
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public  int InsertSpecialFrequency(FS.HISFC.Models.Order.SpecialFrequency info)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Order.Dfqspecial.InsertDfqspecial",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				string OperId = this.Operator.ID;
				strSql = string.Format(strSql,info.OrderID,info.Combo.ID,info.ID,info.Point,info.Dose,OperId);
			}
			catch(Exception ex)
			{
				this.Err  = ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ��������Ƶ��
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateSpecialFrequency( FS.HISFC.Models.Order.SpecialFrequency info )
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Order.Dfqspecial.updateIDfqspecial",ref strSql)==-1)return -1;
			try
			{
				string OperId =this.Operator.ID;
				strSql = string.Format(strSql,info.OrderID,info.Combo.ID ,info.ID,info.Point,info.Dose,OperId);
			}
			catch(Exception ex)
			{
				this.Err  = ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		#endregion

		#region ����
		/// <summary>
		/// ���»��������
		/// </summary>
		/// <param name="info"></param>
		/// <returns>  -1 ʧ�� >=0 ���ظ��¼�¼������ </returns>
		public int SetFrequency(FS.HISFC.Models.Order.SpecialFrequency info)
		{
			string strSql ="";

			if (this.Sql.GetCommonSql("Order.Dfqspecial.updateOrInsertDfqspecial",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,info.OrderID,info.Combo.ID );
				if(this.ExecQuery(strSql)==-1) 
				{
					return -1;
				}
				if(this.Reader.Read())
				{
					//����
					return this.UpdateSpecialFrequency(info );
				}
				else
				{
					//����
					return this.InsertSpecialFrequency(info);
				}
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
		}
		
		/// <summary>
		/// �������Ƶ��
		/// </summary>
		/// <param name="moOrder"></param>
		/// <param name="comNo"></param>
		/// <returns></returns>
		public  FS.HISFC.Models.Order.SpecialFrequency  GetSpecialFrequency(string moOrder,string comNo)
		{
			string strSql = "";
			FS.HISFC.Models.Order.SpecialFrequency info = null;
			if (this.Sql.GetCommonSql("Order.Dfqspecial.GetDfqspecial",ref strSql) == -1) return null;

			try
			{ 
				strSql = string.Format(strSql,moOrder,comNo);
				if(this.ExecQuery(strSql) == -1) return null;
				if(this.Reader.Read())
				{
					info = new FS.HISFC.Models.Order.SpecialFrequency();
					info.ID = Reader[0].ToString();
					info.Name =Reader[1].ToString();
					info.Point = Reader[2].ToString();
					info.Dose = Reader[3].ToString();
				}
				else
				{
					return null;
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				info = null;
			}
			finally
			{
				this.Reader.Close();
			}
			return info;
		}
		#endregion
	}
}
