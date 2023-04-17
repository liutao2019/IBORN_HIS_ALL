using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// Sequence ��ժҪ˵����
	/// </summary>
	public class Sequence:DataBase
	{
		public Sequence()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public ArrayList GetList()
		{
			string sqlSelect ="";
			if(this.GetSQL("Manager.Sequence.Get",ref sqlSelect)==-1) return null;
			return this.myGetSequence(sqlSelect);
		}
		/// <summary>
		/// ��õ�ǰ���к�
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int GetCurrentSequenceNo(FS.HISFC.Models.Base.Sequence info)
		{
			#region �ӿ�
			#endregion
			string sqlSelect="",sqlWhere="",sql="";
			if(this.GetSQL("Manager.Sequence.Get",ref sqlSelect)==-1) return -1;
			if(this.GetSQL("Manager.Sequence.Where.1",ref sqlWhere)==-1) return -1;
			sql = sqlSelect +" "+sqlWhere;
			try
			{
				sql=string.Format(sql,info.ID,info.Name,System.Convert.ToInt16(info.Type).ToString(),info.MinValue,info.CurrentValue,
					info.Rule,info.SortID,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err="�ӿڴ���"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecQuery(sql)==-1) return -1;
			
			if(this.Reader.Read())
			{
				info.ID = this.Reader[0].ToString();
				info.Name = this.Reader[1].ToString();
				try
				{
					info.Type = (FS.HISFC.Models.Base.Sequence.enuType)this.Reader[2];
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.WriteErr();
				}
				info.MinValue  = this.Reader[3].ToString();
				info.CurrentValue = this.Reader[4].ToString();
				info.Rule  = this.Reader[5].ToString();
				return 0;
			}
			else
			{
				return -1;
			}
		}
		/// <summary>
		/// �������к�
		/// </summary>
		/// <returns></returns>
		public int UpdateSequenceNo(FS.HISFC.Models.Base.Sequence info)
		{
			string sql = "";
			if(this.GetSQL("Manager.Sequence.Update.1",ref sql)==-1) return -1;
			try
			{
				sql=string.Format(sql,info.ID,info.Name,System.Convert.ToInt16(info.Type).ToString(),info.MinValue,info.CurrentValue,
					info.Rule,info.SortID,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err="�ӿڴ���"+ex.Message;
				this.WriteErr();
				return -1;
			}
			int i =this.ExecNoQuery(sql);
			if(i==0)
			{
				this.Err="δ�ҵ��У�";
				return -1;
			}
			else if(i<=-1)
			{
				this.Err = "����!";
			}
			else
			{
				return 0;
			}
			return -1;
		}
		private ArrayList myGetSequence(string sql)
		{
			ArrayList al = new ArrayList();
			if(this.ExecQuery(sql)==-1) return null;
			while(this.Reader.Read())
			{
				FS.HISFC.Models.Base.Sequence info = new FS.HISFC.Models.Base.Sequence();
				info.ID = this.Reader[0].ToString();
				info.Name = this.Reader[1].ToString();
				try
				{
					info.Type = (FS.HISFC.Models.Base.Sequence.enuType)this.Reader[2];
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.WriteErr();
				}
				info.MinValue  = this.Reader[3].ToString();
				info.CurrentValue = this.Reader[4].ToString();
				info.Rule  = this.Reader[5].ToString();
				al.Add(info);
			}
			return al;
        }

        /*======�˴��ǳ���ķֽ��ߣ�������ԭ�г�������������ϵͳȡSequence�ķ��� =====*/

        #region �������Sequence

        #region ȡ��Ʊ��ˮ��

        /// <summary>
        /// ��ȡ�µ����﷢Ʊ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewMzInvoiceNO()
        {
            return this.GetSequence("Manager.Fee.NewMzInvoiceNO");
        }     

        /// <summary>
        /// ��ȡ�µĹҺŷ�Ʊ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewGHInvoiceNO()
        {
            return this.GetSequence("Manager.Fee.NewGHInvoiceNO");
        }      

        /// <summary>
        /// ��ȡ�µ������˻���Ʊ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewZHInvoiceNO()
        {
            return this.GetSequence("Manager.Fee.NewZHInvoiceNO");
        }     


        /// <summary>
        /// ��ȡ�µ�Ԥ����Ʊ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewYJInvoiceNO()
        {
            return this.GetSequence("Manager.Fee.NewYJInvoiceNO");
        }       

        /// <summary>
        /// ��ȡ�µĳ�Ժ���㷢Ʊ��ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewJSInvoiceNO()
        {
            return this.GetSequence("Manager.Fee.NewJSInvoiceNO");
        }        

        #endregion

        #endregion

        #region ҽ�����Sequence
        #endregion

        #region ҩƷ���Sequence

        #endregion



    }
}
