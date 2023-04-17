using System;
using System.Collections;
using Neusoft.NFC.Function;

namespace Neusoft.HISFC.Management.Fee
{
    /// <summary>
    /// UndrugComb<br></br>
    /// [��������: ��ҩƷ�����Ŀҵ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class UndrugComb : Neusoft.NFC.Management.Database
    {

        #region ˽�з���

        /// <summary>
        /// ����SQL����ѯ������ϸ
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: ������ϸ ʧ��: null</returns>
        private ArrayList QueryUndrugCombDetailsBySql(string sql, params string[] args) 
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList undrugCombs = new ArrayList();//�����Ŀ����
            Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();//�����Ŀʵ��

            try
            {
                while (this.Reader.Read())
                {
                    undrugComb = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();

                    undrugComb.Package.ID = this.Reader[0].ToString(); //���ױ���
                    undrugComb.Name = this.Reader[1].ToString();//��ҩƷ����
                    undrugComb.ID = this.Reader[2].ToString();  //��ҩƷ����
                    undrugComb.SortID = NConvert.ToInt32(this.Reader[3].ToString());
                    undrugComb.SpellCode = Reader[4].ToString();  //ȡƴ����
                    undrugComb.WBCode = Reader[5].ToString();    //ȡ�����
                    undrugComb.UserCode = Reader[6].ToString(); //������
                    undrugComb.User01 = Reader[7].ToString(); //��־
                    undrugComb.User02 = Reader[8].ToString(); // �Ƿ�����ҽ����Ŀ 0 �� 1 ��
                    undrugComb.Qty = NConvert.ToDecimal(Reader[9].ToString()); //����
                    
                    undrugCombs.Add(undrugComb);
                }                

                this.Reader.Close();

                return undrugCombs;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }
        
        
        /// <summary>
        /// ͨ��SQL����������Ŀ��Ϣ
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:�����Ŀ��Ϣ���� ʧ��: null</returns>
        private ArrayList QueryUndrugCombsBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList undrugCombs = new ArrayList();//�����Ŀ����
            Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();//�����Ŀʵ��

            try
            {
                while (this.Reader.Read())
                {
                    undrugComb = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();
                    undrugComb.Package.ID = this.Reader[0].ToString();
                    undrugComb.Package.Name = this.Reader[1].ToString();
                    undrugComb.SysClass.ID = this.Reader[2].ToString();
                    undrugComb.SpellCode = this.Reader[3].ToString();
                    undrugComb.WBCode = this.Reader[4].ToString();
                    undrugComb.UserCode = this.Reader[5].ToString();
                    undrugComb.ExecDept = this.Reader[6].ToString();
                    undrugComb.SortID = NConvert.ToInt32(this.Reader[7].ToString());
                    undrugComb.IsNeedConfirm = NConvert.ToBoolean(this.Reader[8].ToString());
                    undrugComb.ValidState = this.Reader[9].ToString();
                    undrugComb.User01 = this.Reader[10].ToString();//�Ƿ�����ҽ����Ŀ 
                    undrugComb.Memo = this.Reader[11].ToString(); //��ע
                    undrugComb.Mark1 = this.Reader[12].ToString();//��ʷ�����(����������뵥ʱʹ��)
                    undrugComb.Mark2 = this.Reader[13].ToString();//���Ҫ��(����������뵥ʱʹ��) 
                    undrugComb.Mark3 = this.Reader[14].ToString();// ע������(����������뵥ʱʹ��)   
                    undrugComb.Mark4 = this.Reader[15].ToString();//������뵥����   

                    undrugCombs.Add(undrugComb);
                }

                this.Reader.Close();

                return undrugCombs;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        /// <summary>
        /// ��÷�ҩƷ�����Ŀ��ʵ����������
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>��ҩƷ�����Ŀ��ʵ����������</returns>
        private string[] GetUndrugCombParams(Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb)
        {
            string[] args = 
            {
                undrugComb.Package.ID,
                undrugComb.Package.Name,
                undrugComb.SysClass.ID.ToString(),
                undrugComb.SpellCode,
                undrugComb.WBCode, 
                undrugComb.UserCode,
                undrugComb.ExecDept,
                undrugComb.SortID.ToString(),
                NConvert.ToInt32(undrugComb.IsNeedConfirm).ToString(),
                undrugComb.ValidState,
                undrugComb.User01,
                undrugComb.User02,
                undrugComb.Memo,
                undrugComb.Mark1,
                undrugComb.Mark2,
                undrugComb.Mark3,
                undrugComb.Mark4
            };

            return args;
        }

        /// <summary>
        /// ����Where��������,������Ч�ķ�ҩƷ�����Ŀ��Ϣ����
        /// </summary>
        /// <param name="whereIndex">Where��������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:�����Ŀ��Ϣ���� ʧ��: null</returns>
        private ArrayList QueryUndrugCombs(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            string where = string.Empty;//WHERE���

            //���Where���
            if (this.Sql.GetSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.Valid", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.undrugzt.GetUndrugzt.Valid��SQL���";

                return null;
            }

            return this.QueryUndrugCombsBySql(sql + " " + where, args);
        }

        /// <summary>
        /// ���µ������
        /// </summary>
        /// <param name="sqlIndex">SQL�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSingleTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ��ȡ���з�ҩƷ�����е����� 
        /// </summary>
        /// <returns>�ɹ�:�����Ŀ��Ϣ���� ʧ��: null</returns>                                            
        public ArrayList QueryUndrugCombsAll()
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.undrugzt.GetUndrugzt��SQL���";

                return null;
            }

            return this.QueryUndrugCombsBySql(sql);
        }

        /// <summary>
        /// �����Ч�ķ�ҩƷ�����Ŀ����
        /// </summary>
        /// <returns>�ɹ�:�����Ŀ��Ϣ���� ʧ��: null</returns>
        public ArrayList QueryUndrugCombsValid()
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.Valid", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.undrugzt.GetUndrugzt.Valid��SQL���";

                return null;
            }

            return this.QueryUndrugCombsBySql(sql);
        }

        /// <summary>
        /// ���� ��ҩƷ�����е�����
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�и��µ����� 0</returns>
        public int UpdateUndrugComb(Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb)
        {
            return this.UpdateSingleTable("Fee.undrugzt.UpdateUndrugzt", this.GetUndrugCombParams(undrugComb));
        }

        /// <summary>
        ///  ɾ����ҩƷ�����Ŀ
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û��ɾ�������� 0</returns>
        public int DeleteUndrugComb(Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb)
        {
            return this.UpdateSingleTable("Fee.undrugzt.DeleteUndrugzt", undrugComb.ID);
        }

        /// <summary>
        ///  �����ҩƷ�����Ŀ
        /// </summary>
        /// <param name="undrugComb">��ҩƷ�����Ŀʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� : -1 û�в������� 0</returns>
        public int InsertUndrugComb(Neusoft.HISFC.Object.Fee.Item.UndrugComb undrugComb)
        {
            return this.UpdateSingleTable("Fee.undrugzt.insertUndrugzt", this.GetUndrugCombParams(undrugComb));
        }

        /// <summary>
        /// ͨ�������Ŀ�����ȡһ����Ч�����Ŀ
        /// </summary>
        /// <param name="undrugCombCode">�����Ŀ����</param>
        /// <returns>�ɹ�: һ����Ч�����Ŀ ʧ��: null</returns>
        public Neusoft.HISFC.Object.Fee.Item.UndrugComb GetUndrugCombValidByCode(string undrugCombCode)
        {
            ArrayList undrugCombs = this.QueryUndrugCombs("Fee.undrugzt.GetUndrugzt.1", undrugCombCode);

            if (undrugCombs == null || undrugCombs.Count == 0)
            {
                return null;
            }

            return undrugCombs[0] as Neusoft.HISFC.Object.Fee.Item.UndrugComb;
        }

        /// <summary>
        /// ͨ�������Ŀ�����ȡһ�������Ŀ
        /// </summary>
        /// <param name="undrugCombCode">�����Ŀ����</param>
        /// <returns>�ɹ�: һ�������Ŀ ʧ��: null</returns>
        public Neusoft.HISFC.Object.Fee.Item.UndrugComb GetUndrugCombByCode(string undrugCombCode)
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.undrugzt.GetUndrugzt.1��SQL���";

                return null;
            }
            
            ArrayList undrugCombs = this.QueryUndrugCombsBySql(sql, undrugCombCode);

            if (undrugCombs == null || undrugCombs.Count == 0)
            {
                return null;
            }

            return undrugCombs[0] as Neusoft.HISFC.Object.Fee.Item.UndrugComb;
        }

        /// <summary>
        /// ͨ�������Ŀ�����������Ŀ��ϸ 
        /// </summary>
        /// <param name="combCode">�����Ŀ����</param>
        /// <returns>�ɹ�: �����Ŀ��ϸ  ʧ�� : null</returns>
        public ArrayList QueryUndrugCombDetailsByCombCode(string combCode)
        {
            string sql = string.Empty;

            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugztinfo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.undrugzt.GetUndrugztinfo��SQL���";

                return null;
            }

            return this.QueryUndrugCombDetailsBySql(sql, combCode);
        }
        /// <summary>
        /// ��ȡ������Ŀ���ܼ۸�
        /// </summary>
        /// <param name="ztID">������Ŀ����</param>
        /// <returns></returns>
        public decimal GetUndrugCombPrice(string ztID)
        {
            decimal Price = 0;
            string sql = "";
            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugztPrice", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, ztID);
                if (this.ExecQuery(sql) == -1) return -1;
                while (this.Reader.Read())
                {
                    Price = Neusoft.NFC.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.ErrCode = ee.Message;
                if (this.Reader.IsClosed == false) this.Reader.Close();
                return -1;
            }

            return Price;
        }
        #endregion

        #region ��������

        /// <summary>
		/// ��ȡһ��������Ŀ ��ֻ��Ч
		/// </summary>
		/// <param name="ztID"></param>
		/// <returns></returns>
        [Obsolete("����,GetUndrugCombByCode", true)]
        public Neusoft.HISFC.Object.Fee.Item.UndrugComb GetSingleUndrugzt(string ztID)
		{
			string sql="";
			if(this.Sql.GetSql("Fee.undrugzt.GetSingleUndrugzt.1",ref sql)==-1)return null;
			Neusoft.HISFC.Object.Fee.Item.UndrugComb info= new Neusoft.HISFC.Object.Fee.Item.UndrugComb();
			try
			{			
				sql=string.Format(sql,ztID);
				if(this.ExecQuery(sql)==-1)return null;								
				while(this.Reader.Read())
				{
					//��Ч�ĸ�����Ŀ
					info=new Neusoft.HISFC.Object.Fee.Item.UndrugComb();
					info.ID = Reader[0].ToString();
					info.Name =Reader[1].ToString();
					info.SysClass.ID = Reader[2].ToString();
					info.SpellCode = Reader[3].ToString();
					info.WBCode = Reader[4].ToString();
					info.UserCode = Reader[5].ToString();
					info.ExecDept = Reader[6].ToString();
					if(Reader[7] !=DBNull.Value)
					{
						info.SortID =Convert.ToInt32( Reader[7].ToString());
					}
					info.IsNeedConfirm = Neusoft.NFC.Function.NConvert.ToBoolean(Reader[8].ToString());
					info.ValidState =Reader[9].ToString();		
					info.User01  =Reader[10].ToString();
					info.User02 = Reader[11].ToString();
					info.Memo = Reader[12].ToString(); //��ע
					info.Mark1 = Reader[13].ToString();//��ʷ�����(����������뵥ʱʹ��)
					info.Mark2 = Reader[14].ToString();//���Ҫ��(����������뵥ʱʹ��) 
					info.Mark3 = Reader[15].ToString();// ע������(����������뵥ʱʹ��)   
					info.Mark4 = Reader[16].ToString();//������뵥����   
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = "[Fee.undrugzt.GetUndrugzt.1]"+ee.Message;
				this.ErrCode=ee.Message;
				if(this.Reader.IsClosed==false)this.Reader.Close();
				return null;
			}
			
			return info;			
		}
        
        /// <summary>
        /// ��ȡһ����Ч������Ŀ
        /// </summary>
        /// <param name="ztID"></param>
        /// <returns></returns>
        [Obsolete("����,GetUndrugCombValidByCode", true)]
        public Neusoft.HISFC.Object.Fee.Item.UndrugComb GetValidUndrugzt(string ztID)
        {
            string sql = "";
            string strSql = "";
            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.Valid", ref sql) == -1) return null;
            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.1", ref strSql) == -1) return null;
            sql = sql + strSql;
            Neusoft.HISFC.Object.Fee.Item.UndrugComb info = null;
            try
            {
                sql = string.Format(sql, ztID);
                if (this.ExecQuery(sql) == -1) return null;
                while (this.Reader.Read())
                {
                    //��Ч�ĸ�����Ŀ
                    if (Reader[9].ToString() == "0")
                    {
                        info = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();
                        info.ID = Reader[0].ToString();
                        info.Name = Reader[1].ToString();
                        info.SysClass.ID = Reader[2].ToString();
                        info.SpellCode = Reader[3].ToString();
                        info.WBCode = Reader[4].ToString();
                        info.UserCode = Reader[5].ToString();
                        info.ExecDept = Reader[6].ToString();
                        if (Reader[7] != DBNull.Value)
                        {
                            info.SortID = Convert.ToInt32(Reader[7].ToString());
                        }
                        info.IsNeedConfirm = Neusoft.NFC.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                        info.ValidState = Reader[9].ToString();
                        info.User01 = Reader[10].ToString();
                        info.User02 = Reader[11].ToString();
                        info.Memo = Reader[12].ToString(); //��ע
                        info.Mark1 = Reader[13].ToString();//��ʷ�����(����������뵥ʱʹ��)
                        info.Mark2 = Reader[14].ToString();//���Ҫ��(����������뵥ʱʹ��) 
                        info.Mark3 = Reader[15].ToString();// ע������(����������뵥ʱʹ��)   
                        info.Mark4 = Reader[16].ToString();//������뵥����   
                    }
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = "[Fee.undrugzt.GetUndrugzt.1]" + ee.Message;
                this.ErrCode = ee.Message;
                if (this.Reader.IsClosed == false) this.Reader.Close();
                return null;
            }

            return info;
        }

        /// <summary>
        /// ��ȡ��Ч������Ŀ
        /// </summary>
        /// <returns></returns>
        [Obsolete("����,QueryUndrugCombsValid", true)]
        public ArrayList GetValidUndrugzt()
        {

            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Fee.undrugzt.GetUndrugzt.Valid", ref strSql) == -1) return null;
            try
            {
                // SELECT PACKAGE_CODE,PACKAGE_NAME,SYS_CLASS,SPELL_CODE,WB_CODE ,INPUT_CODE,DEPT_CODE ,SORT_ID  ,CONFIRM_FLAG,VALID_STATE,EXT_FLAG,EXT1_FLAG  FROM fin_com_undrugzt where parent_code = '[��������]' and current_code ='[��������]' 
                if (this.ExecQuery(strSql) == -1) return null;
                Neusoft.HISFC.Object.Fee.Item.UndrugComb info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    //��Ч�ĸ�����Ŀ
                    if (Reader[9].ToString() == "0")
                    {
                        info = new Neusoft.HISFC.Object.Fee.Item.UndrugComb();
                        info.ID = Reader[0].ToString();
                        info.Name = Reader[1].ToString();
                        info.SysClass.ID = Reader[2].ToString();
                        info.SpellCode = Reader[3].ToString();
                        info.WBCode = Reader[4].ToString();
                        info.UserCode = Reader[5].ToString();
                        info.ExecDept = Reader[6].ToString();
                        if (Reader[7] != DBNull.Value)
                        {
                            info.SortID = Convert.ToInt32(Reader[7].ToString());
                        }
                        info.IsNeedConfirm = Neusoft.NFC.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                        info.ValidState = Reader[9].ToString();
                        info.User01 = Reader[10].ToString();
                        info.User02 = Reader[11].ToString();
                        info.Memo = Reader[12].ToString(); //��ע
                        info.Mark1 = Reader[13].ToString();//��ʷ�����(����������뵥ʱʹ��)
                        info.Mark2 = Reader[14].ToString();//���Ҫ��(����������뵥ʱʹ��) 
                        info.Mark3 = Reader[15].ToString();// ע������(����������뵥ʱʹ��)   
                        info.Mark4 = Reader[16].ToString();//������뵥����   
                        List.Add(info);
                    }
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }

        #endregion
    }
}
