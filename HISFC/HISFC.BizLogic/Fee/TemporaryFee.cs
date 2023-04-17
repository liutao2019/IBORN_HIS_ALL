using System;
using System.IO;
using System.Collections;
namespace FS.HISFC.BizLogic.Fee 
{
	/// <summary>
	/// TemporaryFee ��ժҪ˵����
	/// ���ۺ�ֱ���շѣ�����ʱ���ñ���
	/// </summary>
	public class TemporaryFee :FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public TemporaryFee()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		///   ���շ���ϸ��������һ����¼  
		/// </summary>
		/// <param name="item"></param>
		/// <param name="InpatientNO">סԺ��ˮ��</param>
		/// <param name="DeptCode">���ұ���</param>
		/// <param name="ApplyNO">���������</param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Fee.Inpatient.FeeItemList item,string InpatientNO,string DeptCode,string ApplyNO,string OrderID)
		{
			string strSQL="";
			if(this.Sql.GetCommonSql("Fee.TemporaryFee.Insert",ref strSQL)==-1) return -1;
			try
			{  
				string[] strParm = new string[9];  //ȡ�����б�
				strParm[0] = InpatientNO;//��ˮ��
				strParm[1] = item.Item.ID;//��Ŀ����
				strParm[3] = item.FTRate.ItemRate.ToString();//����
				strParm[2] = item.Item.Qty.ToString();//����
				strParm[4] = DeptCode;//����
				strParm[5] = ApplyNO;//��������� 
				strParm[6] = this.Operator.ID ; //����Ա����
				strParm[7] = item.Item.Price.ToString();//�۸�
                strParm[8] = OrderID;
				strSQL=string.Format(strSQL,strParm);    //�滻SQL����еĲ�����
			}
			catch(Exception ex)
			{
				this.Err="����ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		/// <summary>
		/// ɾ��ĳ������ ĳ��סԺ��ˮ���µ���ϸ
		/// </summary>
		/// <param name="InpatientNO"></param>
		/// <param name="DeptCode"></param>
		/// <param name="ApplyNO">���������</param>
		/// <returns></returns>
		public int Delete(string InpatientNO,string DeptCode,string ApplyNO)
		{
			string strSQL="";
			if(ApplyNO == "")
			{
				if(this.Sql.GetCommonSql("Fee.TemporaryFee.Delete2",ref strSQL)==-1) return -1;
			}
			else
			{
				if(this.Sql.GetCommonSql("Fee.TemporaryFee.Delete",ref strSQL)==-1) return -1;
			}
			try
			{  
				strSQL=string.Format(strSQL,InpatientNO,DeptCode,ApplyNO);    //�滻SQL����еĲ�����
			}
			catch(Exception ex)
			{
				this.Err="����ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSQL);
		}
		/// <summary>
		/// ��ѯ ����סԺ��ˮ�� ������ ���������� 
		/// </summary>
		/// <param name="InpatientNO">סԺ��ˮ��</param>
		/// <param name="DeptCode">����</param>
		/// <param name="ApplyNO">������</param>
		/// <returns></returns>
		public ArrayList  Query(string InpatientNO,string DeptCode,string ApplyNO)
		{
			ArrayList list = new  ArrayList();
			string strSQL="";
			if(this.Sql.GetCommonSql("Fee.TemporaryFee.Query",ref strSQL)==-1) return null;
			try
			{  
				strSQL=string.Format(strSQL,InpatientNO,DeptCode,ApplyNO);    //�滻SQL����еĲ�����
			
				this.ExecQuery(strSQL);
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Fee.Inpatient.FeeItemList obj = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
					obj.Item.ID = this.Reader[1].ToString();//��Ŀ����
					obj.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());//����
					obj.FTRate.ItemRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString()); //����
					obj.Item.Price =FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());//�۸�
					list.Add(obj);
				}
				this.Reader.Close();
				return list;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
		}
		/// <summary>
		/// ��ѯ ����סԺ��ˮ�� ������  ��ѯ�ݴ������ 
		/// </summary>
		/// <param name="InpatientNO">סԺ��ˮ��</param>
		/// <param name="DeptCode">���� </param>
		/// <returns></returns>
		public ArrayList Query(string InpatientNO,string DeptCode)
		{
			ArrayList list = new  ArrayList();
			string strSQL="";
			if(this.Sql.GetCommonSql("Fee.TemporaryFee.Query.2",ref strSQL)==-1) return null;
			try
			{  
				strSQL=string.Format(strSQL,InpatientNO,DeptCode);    //�滻SQL����еĲ�����
			
				this.ExecQuery(strSQL);
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Fee.Inpatient.FeeItemList obj = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
					obj.Item.ID = this.Reader[1].ToString();//��Ŀ����
					obj.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());//����
					obj.FTRate.ItemRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString()); //����
					obj.Item.Price =FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());//�۸�
					list.Add(obj);
				}
				this.Reader.Close();
				return list;
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				return null;
			}
		}
	}
}
