using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// AdditionalItem ��ժҪ˵����
	/// ������Ŀ����
	/// </summary>
	public class AdditionalItem:FS.FrameWork.Management.Database 
	{
		public AdditionalItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��Ӹ�����Ϣ
		/// ������д��item.id,item.Amount,Item.isPharmacy��
        /// {24F859D1-3399-4950-A79D-BCCFBEEAB939}
		/// </summary>
		/// <param name="item"></param>
		/// <param name="deptCode"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="typeCode"></param>
        /// <param name="USE_INTERVAL"></param>{24F859D1-3399-4950-A79D-BCCFBEEAB939}
		/// <returns></returns>
        /// {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ���price
        public int InsertAdditionalItem(FS.HISFC.Models.Base.Item item, string deptCode, bool isPharmacy, string typeCode, string USE_INTERVAL, decimal price)
		{
			#region "�ӿ�"
			//���룺0 ���ұ��� 1�Ƿ�ҩƷ��2�÷�����Ŀ������ 3 ������ĿID 4 ������Ŀ���� 5 ����Ա 6 ����ʱ��
			//������0
			#endregion
			string strSql = "";
		
			if (this.Sql.GetCommonSql("Order.AdditionalItem.InsertItem",ref strSql)==-1) 
			{
				this.Err = "û���ҵ�Order.AdditionalItem.InsertItem";
				return -1;
			}
			try
			{
                //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
                //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ���price
                strSql = string.Format(strSql, deptCode, System.Convert.ToInt16(isPharmacy).ToString(), typeCode, item.ID, item.Qty, this.Operator.ID, this.GetSysDateTime(), System.Convert.ToInt32(USE_INTERVAL.Replace('H', ' ')), price);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		///  ���¸�����Ϣ
		/// ������д��item.id,item.Amount,Item.isPharmacy��
		/// </summary>
		/// <param name="item"></param>
		/// <param name="deptCode"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="typeCode"></param>
        /// <param name="USE_INTERVAL"></param>//{24F859D1-3399-4950-A79D-BCCFBEEAB939}
		/// <returns></returns>
        /// {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ���price
        public int UpdateAdditionalItem(FS.HISFC.Models.Base.Item item, string deptCode, bool isPharmacy, string typeCode, string USE_INTERVAL, decimal price)
		{
			string strSql = "";
			#region "�ӿ�"
			//���룺0 ���ұ��� 1�Ƿ�ҩƷ��2�÷�����Ŀ������ 3 ������ĿID 4 ������Ŀ���� 5 ����Ա 6 ����ʱ��
			//������0
			#endregion
			if (this.Sql.GetCommonSql("Order.AdditionalItem.UpdateItem",ref strSql)==-1)
			{
				this.Err = "û���ҵ�Order.AdditionalItem.UpdateItem";
				return -1;
			}
			try
			{
                //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
                // {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ���price
                strSql = string.Format(strSql, deptCode, FS.FrameWork.Function.NConvert.ToInt32(isPharmacy).ToString(), typeCode, item.ID, item.Qty, this.Operator.ID, this.GetSysDateTime(), System.Convert.ToInt32(USE_INTERVAL.Replace('H', ' ')), price);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		///  ɾ��������Ϣ
		/// ������д��item.id,item.Amount,Item.isPharmacy��
		/// </summary>
		/// <param name="item"></param>
		/// <param name="deptCode"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="typeCode"></param>
		/// <returns></returns>
		public int DeleteAdditionalItem( FS.HISFC.Models.Base.Item item, string deptCode, bool isPharmacy, string typeCode )
		{
			string strSql = "";
			#region "�ӿ�"
			//���룺0 ���ұ��� 1�Ƿ�ҩƷ��2�÷�����Ŀ������ 3 ������ĿID 
			//������0
			#endregion
			if (this.Sql.GetCommonSql("Order.AdditionalItem.DeleteItem",ref strSql)==-1) 
			{
				this.Err = "û���ҵ�Order.AdditionalItem.DeleteItem";
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,deptCode,FrameWork.Function.NConvert.ToInt32(isPharmacy).ToString(),typeCode,item.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ���ø���
		/// </summary>
		/// <param name="item"></param>
		/// <param name="deptCode"></param>
		/// <param name="isPharmacy"></param>
		/// <param name="typeCode"></param>
        /// <param name="USE_INTERVAL"></param>//{24F859D1-3399-4950-A79D-BCCFBEEAB939}
		/// <returns></returns>
        /// {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ���price
        public int SetAdditionalItem(FS.HISFC.Models.Base.Item item, string deptCode, bool isPharmacy, string typeCode, string USE_INTERVAL, decimal price)
		{
            //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
            int i = this.UpdateAdditionalItem(item, deptCode, isPharmacy, typeCode, (USE_INTERVAL == null || USE_INTERVAL == "") ? "0" : USE_INTERVAL, price);
			if( i== 0 )
			{
                //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
                i = this.InsertAdditionalItem(item, deptCode, isPharmacy, typeCode, (USE_INTERVAL == null || USE_INTERVAL == "") ? "0" : USE_INTERVAL, price);
				if(i<0) return -1;
				return i;
			}
			return i;
		}

		#region ���ϵ�
		/// <summary>
		/// ���ҽ���÷�����Ŀ���ĸ�����Ϣ
		/// </summary>
		/// <param name="IsPharmacy"></param>
		/// <param name="TypeCode"></param>
		/// <param name="DeptCode"></param>
		/// <returns></returns>
		[Obsolete("��QueryAdditionalItem����",true)]
		public ArrayList GetAdditionalItem(bool IsPharmacy,string TypeCode,string DeptCode)
		{
			return this.QueryAdditionalItem(IsPharmacy,TypeCode,DeptCode);			
		}

		[Obsolete("��QueryAdditionalItem����",true)]
		public ArrayList GetAdditionalUndrugItems(bool IsPharmacy,string DeptCode)
		{
			return this.QueryAdditionalItem(IsPharmacy,DeptCode);
		}
		#endregion 

		/// <summary>
		/// ���ҽ���÷�����Ŀ���ĸ�����Ϣ
		/// </summary>
		/// <param name="isPharmacy"></param>
		/// <param name="typeCode"></param>
		/// <param name="deptCode"></param>
		/// <returns></returns>
		public ArrayList QueryAdditionalItem( bool isPharmacy, string typeCode, string deptCode )
		{
			ArrayList al=new ArrayList();
			string strSql="";
			//Order.AdditionalItem.SelectItem.1
			//���룺0 DeptCode 1 IsPharmacy,2 TypeCode 
			//����:���ڸ���Ŀ���÷��ĸ���
			if(this.Sql.GetCommonSql("Order.AdditionalItem.SelectItem.1",ref strSql) == 0)
			{
				if(this.ExecQuery(strSql,deptCode,FS.FrameWork.Function.NConvert.ToInt32(isPharmacy).ToString(),typeCode)==-1) return null;
				while(this.Reader.Read())
				{
					
					FS.HISFC.Models.Base.Item obj=new FS.HISFC.Models.Base.Item();
					//0 TypeCode û�� 
					//1 ��Ŀ���� 
					//2 ����
					//3 ����Ա
					//4 ����ʱ��
					obj.ID=this.Reader[1].ToString();
					obj.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
					obj.User01 = this.Reader[3].ToString();
					obj.User02 = this.Reader[4].ToString();
                    //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
                    obj.User03 = this.Reader[5].ToString() == "0" ? "0" : this.Reader[5].ToString() + "H";
                    //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ���Ӳ��ҵ���
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
					al.Add(obj);
				}
				this.Reader.Close();
			}
			else
			{
				this.Err = "û���ҵ�Order.AdditionalItem..SelectItem.1";
				return null;
			}
			return al;
		}
		/// <summary>
		/// ����Ѿ�ά���ķ�ҩƷ������Ŀ
		/// ֻ��÷�ҩƷ��Ŀ
		/// </summary>
		/// <param name="isPharmacy"></param>
		/// <param name="deptCode"></param>
		/// <returns></returns>
		public ArrayList QueryAdditionalItem( bool isPharmacy, string deptCode )
		{
			ArrayList al = new ArrayList();
			string strSql="";
			
			if(this.Sql.GetCommonSql("Order.AdditionalItem.SelectItems.1",ref strSql) == 0)
			{
				if(this.ExecQuery(strSql,deptCode,FrameWork.Function.NConvert.ToInt32(isPharmacy).ToString())==-1) return null;
				while(this.Reader.Read())
				{
					
					FS.FrameWork.Models.NeuObject  obj=new FS.FrameWork.Models.NeuObject();
					//ID ��Ŀ���
					//Name ��Ŀ����
					//Memo ����
					obj.ID=this.Reader[0].ToString();
					try
					{
						obj.Name = this.Reader[1].ToString();
						obj.Memo = this.Reader[2].ToString();
                        //{24F859D1-3399-4950-A79D-BCCFBEEAB939}
                        obj.User03 = this.Reader[3].ToString() == "0" ? "0" : this.Reader[3].ToString() + "H";
                        //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                        obj.User01 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]).ToString();
					}
					catch{}
					al.Add(obj);
				}
				this.Reader.Close();
			}
			else
			{
				this.Err = "û���ҵ�Order.AdditionalItem..SelectItem.1";
				return null;
			}
			return al;
		}
	}
}
