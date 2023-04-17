using System;
using System.Collections;
namespace Neusoft.HISFC.Management.Fee
{
	/// <summary>
	/// undrugztinfo ��ժҪ˵����
	/// </summary>
	public class UndrugPackAge: Neusoft.NFC.Management.Database 
	{
		
		/// <summary>
		/// ��ҩƷ������ϸ�� 
		/// </summary>
		/// <param name="packageCode"></param>
		/// <returns></returns>
		public ArrayList QueryUndrugPackagesBypackageCode(string packageCode)
		{
			ArrayList List = null;
			string strSql = "";
			if (this.Sql.GetSql("Fee.undrugzt.GetUndrugztinfo",ref strSql)==-1) return null;
			try
			{
				if(packageCode!="")
				{
					List = new ArrayList();
					// select package_code ,decode (substr(fee_code,1,1),'F',(select item_name from fin_com_undruginfo where  parent_code = '[��������]' and current_code ='[��������]' and  item_code =fin_com_undrugztinfo.ITEM_CODE),'Y', (select trade_name from pha_com_baseinfo t where  parent_code = '[��������]' and current_code ='[��������]' and t.drug_code = fin_com_undrugztinfo.ITEM_CODE )) ,item_code ,sort_id from fin_com_undrugztinfo where package_code ='{0}'and parent_code = '[��������]' and current_code ='[��������]'
					strSql = string.Format(strSql,packageCode);
					this.ExecQuery(strSql);
					Neusoft.HISFC.Object.Fee.Undrugztinfo info = null;
					while(this.Reader.Read())
					{
						info = new Neusoft.HISFC.Object.Fee.Undrugztinfo();

						info.ID = Reader[0].ToString(); //���ױ���
						info.itemName = Reader[1].ToString();//��ҩƷ����
						info.itemCode =Reader[2].ToString();  //��ҩƷ����
						if(Reader[3]!=DBNull.Value)
						{
							info.sortId =Convert.ToInt32(Reader[3]); //˳���
						}
						else
						{
							info.sortId = 0;
						}
						info.SpellCode = Reader[4].ToString();  //ȡƴ����
						info.WbCode = Reader[5].ToString();    //ȡ�����
						info.InputCode = Reader[6].ToString(); //������
						info.User01 =Reader[7].ToString(); //��־
						info.User02 = Reader[8].ToString(); // �Ƿ�����ҽ����Ŀ 0 �� 1 ��
						info.Qty = Neusoft.NFC.Function.NConvert.ToDecimal(Reader[9].ToString()); //����
						List.Add(info);
						info = null;
					}
					this.Reader.Close();
				}
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				List = null;
			}
			return List;
		}
		/// <summary>
		/// ����һ����¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int UpdateUndrugztinfo(Neusoft.HISFC.Object.Fee.Undrugztinfo info )
		{
			string strSql = "";
			if (this.Sql.GetSql("Fee.undrugzt.UpdateUndrugztinfo",ref strSql)==-1)return -1;
			try
			{
				// update fin_com_undrugztinfo set SORT_ID='{2}' where  PACKAGE_CODE ='{0}'and ITEM_CODE ='{1}' and parent_code = '[��������]' and current_code ='[��������]'
				strSql = string.Format(strSql,info.ID,info.itemCode,info.sortId,info.SpellCode,info.WbCode,info.InputCode,info.User01,info.Qty);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ɾ��һ���µļ�¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteUndrugztinfo(Neusoft.HISFC.Object.Fee.Undrugztinfo info)
		{
			string strSql = "";
			if (this.Sql.GetSql("Fee.undrugzt.DeleteUndrugztinfo",ref strSql)==-1)return -1;
			try
			{
				// delete  fin_com_undrugztinfo  where PACKAGE_CODE = '{0}' and ITEM_CODE ='{1}' and parent_code = '[��������]' and current_code ='[��������]'
				strSql = string.Format(strSql,info.ID,info.itemCode);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ����һ���µļ�¼
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertUndruaztinfo(Neusoft.HISFC.Object.Fee.Undrugztinfo info)
		{
			string strSql = "";
			if (this.Sql.GetSql("Fee.undrugzt.InsertUndruaztinfo",ref strSql)==-1)return -1;
			try
			{
				string OperId = this.Operator.ID;
				// insert into fin_com_undrugztinfo  (PACKAGE_CODE,ITEM_CODE,SORT_ID,OPER_CODE,OPER_DATE) values ('{0},'{1}','{2}','{3}',sysdate)
				strSql = string.Format(strSql,info.ID,info.itemCode,info.sortId,OperId,info.SpellCode,info.WbCode,info.InputCode,info.User01,info.Qty);
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
	}
}
