using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Pharmacy
{
	/// <summary>
	/// Dictionary ��ժҪ˵����
	/// ҩƷ�ֵ�
	/// </summary>
    public class Dictionary : DataBase
	{
		public Dictionary()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���һ������
		/// </summary>
		/// <returns></returns>
        public ArrayList QueryGradeOne()
		{
			ArrayList al=new ArrayList();            //���ڷ���ҩƷ��Ϣ������
			string SQLString ="Pharmacy.Dictionary.GetClass1";
			if(this.GetSQL(SQLString,ref SQLString)==-1) return null;
			if(this.ExecQuery(SQLString)==-1) return null;
			try 
			{
				while (this.Reader.Read()) 
				{
					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();
					obj.Memo = this.Reader[2].ToString();
					al.Add(obj);
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
			}
			finally
			{
				this.Reader.Close();
			}
			return al;
		}
		/// <summary>
		/// ��ö�������
		/// </summary>
		/// <returns></returns>
        public ArrayList QueryGradeTwo(string Class1Code)
		{
			ArrayList al=new ArrayList();            //���ڷ���ҩƷ��Ϣ������
			string SQLString ="Pharmacy.Dictionary.GetClass2";
			if(this.GetSQL(SQLString,ref SQLString)==-1) return null;
			SQLString = string.Format(SQLString,Class1Code);
			if(this.ExecQuery(SQLString)==-1) return null;
			try 
			{
				while (this.Reader.Read()) 
				{
					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();
					obj.Memo = this.Reader[2].ToString();
					al.Add(obj);
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
			}
			finally
			{
				this.Reader.Close();
			}
			return al;
		}
		/// <summary>
		/// ����ֵ��б�
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
        public int QueryDictionaryList(ref System.Data.DataSet ds)
		{
			string SQLString ="Pharmacy.Dictionary.GetList";
			if(this.GetSQL(SQLString,ref SQLString)==-1) return -1;
			return this.ExecQuery(SQLString,ref ds);
        }


        #region ����
        /// <summary>
        /// ���һ������
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryGradeOne����", true)]
        public ArrayList GetClass1()
        {
            ArrayList al = new ArrayList();            //���ڷ���ҩƷ��Ϣ������
            string SQLString = "Pharmacy.Dictionary.GetClass1";
            if (this.GetSQL(SQLString, ref SQLString) == -1) return null;
            if (this.ExecQuery(SQLString) == -1) return null;
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ��ö�������
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryGradeTwo����",true)]
        public ArrayList GetClass2(string Class1Code)
        {
            ArrayList al = new ArrayList();            //���ڷ���ҩƷ��Ϣ������
            string SQLString = "Pharmacy.Dictionary.GetClass2";
            if (this.GetSQL(SQLString, ref SQLString) == -1) return null;
            SQLString = string.Format(SQLString, Class1Code);
            if (this.ExecQuery(SQLString) == -1) return null;
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ����ֵ��б�
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        [System.Obsolete("ϵͳ�ع����ú���QueryDictionaryList����", true)]
        public int GetList(ref System.Data.DataSet ds)
        {
            string SQLString = "Pharmacy.Dictionary.GetList";
            if (this.GetSQL(SQLString, ref SQLString) == -1) return -1;
            return this.ExecQuery(SQLString, ref ds);
        }

        #endregion 
    }
}
