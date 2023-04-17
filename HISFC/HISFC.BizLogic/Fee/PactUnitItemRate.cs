using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// PactUnitItemRate ��ժҪ˵����
	/// </summary>
	public class PactUnitItemRate:  FS.FrameWork.Management.Database
	{
		
		/// <summary>
		/// 
		/// </summary>
		public PactUnitItemRate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ѯ���ݿ⣬�õ�ĳ��ͬ��λ�µ���С����/��Ŀ
		/// pact_code �Ǻ�ͬ��λ���룬index Ϊ0 ��ʾ�������С���ã�Ϊ1��ʾ�������Ŀ
		/// </summary>
		/// <returns></returns>
		public ArrayList GetPactUnitItemRate(string pact_code,int index)
		{
			string strSql = "";
			if(index ==0)
			{
                if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetPactUnitItemRate", ref strSql) == -1) return null;
                strSql = string.Format(strSql, pact_code);
			}
            //else if (index == 3)
            //{
            //    if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetPactUnitItemRate3", ref strSql) == -1) return null;
            //    string[] key = pact_code.Split('@');
            //    strSql = string.Format(strSql, key[0], key[1].ToUpper());
            //}
            else
            {
                if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetPactUnitItemRate2", ref strSql) == -1) return null;
                strSql = string.Format(strSql, pact_code);
            }
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			try
			{
				this.ExecQuery(strSql);
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Base.PactItemRate info = new FS.HISFC.Models.Base.PactItemRate();
					//��ͬ����
					info.ID = Reader[0].ToString();
					//���� (��С���û���Ŀ����)Item.Name
					info.PactItem.Name = Reader[1].ToString();
					//���  int ItemType
					info.ItemType = Reader[2].ToString();
					//���ѱ��� float PubRate
					info.Rate.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
					//�Էѱ��� float OwnRate
                    info.Rate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[4].ToString());
					//�Ը����� float PayRate
                    info.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    //������� float RebateRate   {1C0DA8D4-50FF-4097-B29F-C6CB21595A1B}
                    info.Rate.RebateRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6].ToString());
					//Ƿ�ѱ��� float ArrearageRate
                    info.Rate.ArrearageRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
					//���루��С���û���Ŀ���룩Item.id
					info.PactItem.ID = Reader[8].ToString();

                    
                    //�޶�
                    if (this.Reader.FieldCount >= 10)
                    {
                        info.Rate.Quota = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                    }

                    // ��ȡƴ���롢������Լ��������
                    if (this.Reader.FieldCount >= 11)
                    {
                        info.PactItem.User01 = Reader[10].ToString();
                    }
                    if (this.Reader.FieldCount >= 12)
                    {
                        info.PactItem.User02 = Reader[11].ToString();
                    }
                    if (this.Reader.FieldCount >= 13)
                    {
                        info.PactItem.User03 = Reader[12].ToString();
                    }
					//��ӵ�������
					list.Add(info);
					info = null;
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
				list = null;
			}
			return list;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pact_code"></param>
        /// <param name="ItemOrMincode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
		private  FS.HISFC.Models.Base.PactItemRate   GetOnePactUnitItemRate(string pact_code,string ItemOrMincode,int index)
		{
			string strSql = "";
			FS.HISFC.Models.Base.PactItemRate info =null;
			if(index ==0)
			{
                if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetOnePactUnitItemRate", ref strSql) == -1) return null;
				strSql = string.Format(strSql,pact_code,ItemOrMincode);
			}
			else
			{
                if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetOnePactUnitItemRate2", ref strSql) == -1) return null;
				strSql = string.Format(strSql,pact_code,ItemOrMincode);
			}
			try
			{
				if(this.ExecQuery(strSql)==-1) return null;
				if (this.Reader.Read())
				{
					info = new FS.HISFC.Models.Base.PactItemRate();
					//��ͬ����
					info.ID = Reader[0].ToString();
					//���� (��С���û���Ŀ����)Item.Name
					info.PactItem.Name = Reader[1].ToString();
					//���  int ItemType
					info.ItemType = Reader[2].ToString();
					//���ѱ��� float PubRate
					info.Rate.PubRate = Convert.ToDecimal(Reader[3].ToString());
					//�Էѱ��� float OwnRate
					info.Rate.OwnRate = Convert.ToDecimal(Reader[4].ToString());
					//�Ը����� float PayRate
					info.Rate.PayRate = Convert.ToDecimal(Reader[5].ToString());
                    //�Żݱ��� float RebateRate   {1C0DA8D4-50FF-4097-B29F-C6CB21595A1B}
					info.Rate.RebateRate = Convert.ToDecimal(Reader[6].ToString());
					//Ƿ�ѱ��� float ArrearageRate
					info.Rate.ArrearageRate = Convert.ToDecimal(Reader[7].ToString());
					//���루��С���û���Ŀ���룩Item.id
					info.PactItem.ID = Reader[8].ToString();
                    //�޶�
                    if (this.Reader.FieldCount >= 10)
                    {
                        info.Rate.Quota = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                    }
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
				info = null;
			}
			return info ;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pact_code"></param>
		/// <param name="Item"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Base.PactItemRate GetOnepPactUnitItemRateByItem(string pact_code,string Item)
		{
			return GetOnePactUnitItemRate(pact_code,Item,1);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pact_code"></param>
		/// <param name="FeeCode"></param>
		/// <returns></returns>
		public  FS.HISFC.Models.Base.PactItemRate GetOnePaceUnitItemRateByFeeCode(string pact_code,string FeeCode)
		{
			return GetOnePactUnitItemRate(pact_code,FeeCode,0);
		}

        /// <summary>
        /// ������С���ú���Ŀ��������ȡ��Ŀ����
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="minCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.PactItemRate GetOnePactUnitItemRate(string pactCode, string minCode, int index, string itemCode)
        {
            string strSql = "";
            FS.HISFC.Models.Base.PactItemRate info = null;
            if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.GetOnePactUnitItemRate3", ref strSql) == -1) return null;
            strSql = string.Format(strSql, pactCode, minCode, index, itemCode);

            try
            {
                if (this.ExecQuery(strSql) == -1) return null;
                if (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Base.PactItemRate();
                    //��ͬ����
                    info.ID = Reader[0].ToString();
                    //���� (��С���û���Ŀ����)Item.Name
                    info.PactItem.Name = Reader[1].ToString();
                    //���  int ItemType
                    info.ItemType = Reader[2].ToString();
                    //���ѱ��� float PubRate
                    info.Rate.PubRate = Convert.ToDecimal(Reader[3].ToString());
                    //�Էѱ��� float OwnRate
                    info.Rate.OwnRate = Convert.ToDecimal(Reader[4].ToString());
                    //�Ը����� float PayRate
                    info.Rate.PayRate = Convert.ToDecimal(Reader[5].ToString());
                    //�Żݱ��� float RebateRate   {1C0DA8D4-50FF-4097-B29F-C6CB21595A1B}
                    info.Rate.RebateRate = Convert.ToDecimal(Reader[6].ToString());
                    //Ƿ�ѱ��� float ArrearageRate
                    info.Rate.ArrearageRate = Convert.ToDecimal(Reader[7].ToString());
                    //���루��С���û���Ŀ���룩Item.id
                    info.PactItem.ID = Reader[8].ToString();
                    //�޶�
                    if (this.Reader.FieldCount >= 10)
                    {
                        info.Rate.Quota = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                    }
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                info = null;
            }
            return info;
        }

		/// <summary>
		/// �������ݿ��е�ֵ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int  UpdatePactUnitItemRate(FS.HISFC.Models.Base.PactItemRate info )
		{
			string strSql = "";
            if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.UpdatePactUnitItemRate", ref strSql) == -1) return -1;
			try
			{
                //{1C0DA8D4-50FF-4097-B29F-C6CB21595A1B}
                strSql = string.Format(strSql, info.ID, info.ItemType, info.PactItem.ID, info.Rate.PubRate, info.Rate.OwnRate, info.Rate.PayRate, info.Rate.RebateRate, info.Rate.ArrearageRate, info.Rate.Quota, info.Memo);//add xf �޶�
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
			//���ظ��½��
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// �����ݿ��в����µ�������
		/// </summary>
		/// <returns></returns>
		public int InsertPactUnitItemRate(FS.HISFC.Models.Base.PactItemRate info )
		{
			string strSql = "";
			string  OPER_CODE = this.Operator.ID;
            if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.InsertPactUnitItemRate", ref strSql) == -1) return -1;
			try
			{
                //{1C0DA8D4-50FF-4097-B29F-C6CB21595A1B}
				strSql = string.Format(strSql,info.ID,info.ItemType,info.PactItem.ID,info.Rate.PubRate,info.Rate.OwnRate,info.Rate.PayRate,info.Rate.RebateRate,info.Rate.ArrearageRate,OPER_CODE,info.Rate.Quota);//add xf �޶�
			}
			catch(Exception ee)
			{
				this.Err  = ee.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="info"></param>
	/// <returns></returns>
		public int DeletePactUnitItemRate(FS.HISFC.Models.Base.PactItemRate info)
		{
			string strSql = "";
            if (this.Sql.GetCommonSql("Fee.PactUnitItemRate.DeletePactUnitItemRate", ref strSql) == -1) return -1;

			try
			{
				//0��ȡʱ��1��ȡ��
				strSql = string.Format(strSql,info.ID,info.PactItem.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
	}
}
