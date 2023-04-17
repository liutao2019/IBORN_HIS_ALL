using System;
using System.Collections;
using FS.HISFC.Models.Fee;
using FS.FrameWork.Function;
namespace FS.HISFC.BizLogic.Fee
{
	/// <summary>
	/// PactUnitInfo ��ժҪ˵����
	/// </summary>
	public class PactUnitInfo :  FS.FrameWork.Management.Database
	{
		private System.Collections.ArrayList list = new System.Collections.ArrayList();
		/// <summary>
		/// ��ͬ��λ����ά��
		/// </summary>
		public PactUnitInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        #region ˽�з���

        #region ������²���

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
            if (this.Sql.GetCommonSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        #endregion

        #region ��ѯ����

        /// <summary>
        /// ��ѯҽ�������㷨����Ӧ��DLL
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitDLL()
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.GetPactUnitDLL", ref sql) == -1)
            {
                /*
                 * select distinct fcp.dll_name,fcp.dll_description
                 * from fin_com_pactunitinfo fcp
                 */
                this.Err = "û���ҵ�����Ϊ: Fee.GetPactUnitDLL ��SQL���";
                return null;
            }

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            ArrayList pactUnitList = new ArrayList();
            FS.HISFC.Models.Base.PactInfo pactUnit = null;

            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.HISFC.Models.Base.PactInfo();

                    pactUnit.PactDllName = this.Reader[0].ToString();//��ͬDLL����
                    pactUnit.PactDllDescription = this.Reader[1].ToString();//��ͬDLL����

                    pactUnitList.Add(pactUnit);

                }
                this.Reader.Close();

                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// ����SQL����ѯ��ͬ��λ��Ϣ
        /// </summary>
        /// <param name="sql">��ѯ��SQL���</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ� ��ͬ��λ��Ϣ���� ʧ�� null</returns>
        private ArrayList QueryPactUnitBySql(string sql, params string[] args) 
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList pactUnitList = new ArrayList();//������ϸ����
            FS.HISFC.Models.Base.PactInfo pactUnit = null;

            try
            {
                while (this.Reader.Read()) 
                {
                    pactUnit = new FS.HISFC.Models.Base.PactInfo();

                    pactUnit.ID = this.Reader[0].ToString();//��ͬ����          
                    pactUnit.Name = this.Reader[1].ToString();//��ͬ��λ����                    
                    pactUnit.PayKind.ID = this.Reader[2].ToString();//�������                    
                    pactUnit.Rate.PubRate = NConvert.ToDecimal(Reader[3].ToString().Trim());//���ѱ���                    
                    pactUnit.Rate.PayRate = NConvert.ToDecimal(Reader[4].ToString().Trim());//�Ը�����                   
                    pactUnit.Rate.OwnRate = NConvert.ToDecimal(Reader[5].ToString().Trim()); //�Էѱ���                   
                    pactUnit.Rate.RebateRate = NConvert.ToDecimal(Reader[6].ToString().Trim()); //�Żݱ���                    
                    pactUnit.Rate.ArrearageRate = NConvert.ToDecimal(Reader[7].ToString().Trim());//Ƿ�ѱ���                    
                    pactUnit.Rate.IsBabyShared = NConvert.ToBoolean(Reader[8].ToString());//Ӥ����־ 0 �޹� 1 �й�                                
                    pactUnit.IsNeedMCard = NConvert.ToBoolean(Reader[9].ToString().Trim()); //�Ƿ�Ҫ�������ҽ��֤�� 0 �� 1 ��                      
                    pactUnit.IsInControl = NConvert.ToBoolean(Reader[10].ToString().Trim());//�Ƿ��ܼ�� 1�ܼ��0���ܼ��                   
                    pactUnit.ItemType = Reader[11].ToString().Trim(); //��־  0 ȫ�� 1 ҩƷ 2 ��ҩƷ   
                    pactUnit.DayQuota = NConvert.ToDecimal(Reader[12].ToString().Trim());//���޶�                     
                    pactUnit.MonthQuota = NConvert.ToDecimal(Reader[13].ToString().Trim()); //���޶�                    
                    pactUnit.YearQuota = NConvert.ToDecimal(Reader[14].ToString().Trim());//���޶�
                    pactUnit.OnceQuota = NConvert.ToDecimal(Reader[15].ToString().Trim());//һ����
                    string PriceForm = Reader[16].ToString();                    
                    if (PriceForm == "0")
                    {
                        pactUnit.PriceForm = "Ĭ�ϼ�";
                    }
                    else if (PriceForm == "1")
                    {
                        pactUnit.PriceForm = "�����";
                    }
                    else if (PriceForm == "2")
                    {
                        pactUnit.PriceForm = "��ͯ��";
                    }
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}maokb���ӹ����
                    else if (PriceForm == "3")
                    {
                        pactUnit.PriceForm = "�����";
                    }
                    else
                    {
                        pactUnit.PriceForm = "Ĭ�ϼ�";
                    }

                    pactUnit.BedQuota = NConvert.ToDecimal(Reader[17].ToString());//��λ�޶�
                    pactUnit.AirConditionQuota = NConvert.ToDecimal(Reader[18].ToString());//�յ��޶�
                    pactUnit.SortID = NConvert.ToInt32(Reader[19]);//���             
                    pactUnit.ShortName = Reader[20].ToString();//��ͬ��λ���
                    pactUnit.PactDllName = Reader[21].ToString(); //����dll����
                    pactUnit.PactDllDescription = Reader[22].ToString();//����dll˵��
                    if (Reader.FieldCount > 24)
                        pactUnit.SpellCode = Reader[24].ToString();//ƴ����
                    if (Reader.FieldCount > 25)
                        pactUnit.WBCode = Reader[25].ToString();//�����
                    
                    if (Reader.FieldCount > 23)
                    {
                        pactUnit.PactSystemType = Reader[23].ToString().Trim();

                        switch (pactUnit.PactSystemType)
                        {
                            case "1":
                                pactUnit.PactSystemType = "����";
                                break;
                            case "2":
                                pactUnit.PactSystemType = "סԺ";
                                break;
                            case "3":
                                pactUnit.PactSystemType = "ϵͳ";
                                break;
                            default:
                                pactUnit.PactSystemType = "ȫԺ";
                                break;
                        }
                    }
                    
                    pactUnitList.Add(pactUnit);
                    
                 }
                 
                this.Reader.Close();

                return pactUnitList;
            }
            catch (Exception e) 
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

   #region  Ϊ��ͬ��λ���ն��ӵķ���  
  
        private ArrayList QueryPactCompareUnitBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList pactUnitList = new ArrayList();//������ϸ����
            FS.HISFC.Models.Base.PactCompare pactCompare = null;

            try
            {
                while (this.Reader.Read())
                {
                    pactCompare = new FS.HISFC.Models.Base.PactCompare();

                    pactCompare.PactCode = this.Reader[0].ToString();//��ͬ���� 
                    pactCompare.PactHead = this.Reader[1].ToString();
                    pactCompare.PactName = this.Reader[2].ToString();//��ͬ��λ����
                    pactCompare.ParentPact = this.Reader[3].ToString();//������ͬ��λ����
                    pactCompare.ParentName = this.Reader[4].ToString();//������ͬ��λ����
                    pactCompare.PactFlag = this.Reader[5].ToString();//��ͬ��λ����

                    pactCompare.PayKind.ID = this.Reader[6].ToString();//�������   

                    pactCompare.ValldState = this.Reader[7].ToString();//��Ч��
                    #region ע��
                    /*     pactUnit.Rate.PubRate = NConvert.ToDecimal(Reader[3].ToString().Trim());//���ѱ���                    
                    pactUnit.Rate.PayRate = NConvert.ToDecimal(Reader[4].ToString().Trim());//�Ը�����                   
                    pactUnit.Rate.OwnRate = NConvert.ToDecimal(Reader[5].ToString().Trim()); //�Էѱ���                   
                    pactUnit.Rate.RebateRate = NConvert.ToDecimal(Reader[6].ToString().Trim()); //�Żݱ���                    
                    pactUnit.Rate.ArrearageRate = NConvert.ToDecimal(Reader[7].ToString().Trim());//Ƿ�ѱ���                    
                    pactUnit.Rate.IsBabyShared = NConvert.ToBoolean(Reader[8].ToString());//Ӥ����־ 0 �޹� 1 �й�                                
                    pactUnit.IsNeedMCard = NConvert.ToBoolean(Reader[9].ToString().Trim()); //�Ƿ�Ҫ�������ҽ��֤�� 0 �� 1 ��                      
                    pactUnit.IsInControl = NConvert.ToBoolean(Reader[10].ToString().Trim());//�Ƿ��ܼ�� 1�ܼ��0���ܼ��                   
                    pactUnit.ItemType = Reader[11].ToString().Trim(); //��־  0 ȫ�� 1 ҩƷ 2 ��ҩƷ   
                    pactUnit.DayQuota = NConvert.ToDecimal(Reader[12].ToString().Trim());//���޶�                     
                    pactUnit.MonthQuota = NConvert.ToDecimal(Reader[13].ToString().Trim()); //���޶�                    
                    pactUnit.YearQuota = NConvert.ToDecimal(Reader[14].ToString().Trim());//���޶�
                    pactUnit.OnceQuota = NConvert.ToDecimal(Reader[15].ToString().Trim());//һ����
                    string PriceForm = Reader[16].ToString();                    
                    if (PriceForm == "0")
                    {
                        pactUnit.PriceForm = "Ĭ�ϼ�";
                    }
                    else if (PriceForm == "1")
                    {
                        pactUnit.PriceForm = "�����";
                    }
                    else if (PriceForm == "2")
                    {
                        pactUnit.PriceForm = "��ͯ��";
                    }
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}maokb���ӹ����
                    else if (PriceForm == "3")
                    {
                        pactUnit.PriceForm = "�����";
                    }
                    else
                    {
                        pactUnit.PriceForm = "Ĭ�ϼ�";
                    }

                    pactUnit.BedQuota = NConvert.ToDecimal(Reader[17].ToString());//��λ�޶�
                    pactUnit.AirConditionQuota = NConvert.ToDecimal(Reader[18].ToString());//�յ��޶�
                    pactUnit.SortID = NConvert.ToInt32(Reader[19]);//���             
                    pactUnit.ShortName = Reader[20].ToString();//��ͬ��λ���
                    pactUnit.PactDllName = Reader[21].ToString(); //����dll����
                    pactUnit.PactDllDescription = Reader[22].ToString();//����dll˵��
                    if (Reader.FieldCount > 24)
                        pactUnit.SpellCode = Reader[24].ToString();//ƴ����
                    if (Reader.FieldCount > 25)
                        pactUnit.WBCode = Reader[25].ToString();//�����
                    
                    if (Reader.FieldCount > 23)
                    {
                        pactUnit.PactSystemType = Reader[23].ToString().Trim();

                        switch (pactUnit.PactSystemType)
                        {
                            case "1":
                                pactUnit.PactSystemType = "����";
                                break;
                            case "2":
                                pactUnit.PactSystemType = "סԺ";
                                break;
                            case "3":
                                pactUnit.PactSystemType = "ϵͳ";
                                break;
                            default:
                                pactUnit.PactSystemType = "ȫԺ";
                                break;
                        }
                    }
                    */
                    #endregion
                    pactUnitList.Add(pactCompare);

                }

                this.Reader.Close();

                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }

        /// <summary>
        /// �ҳ���ѯ��ͬ��λ���յ�sql
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactCompareAll()
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.PactCompare.GetPactCompareInfo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.PactCompare.GetPactCompareInfo��SQL���";

                return null;
            }

            return this.QueryPactCompareUnitBySql(sql);
        }

#endregion 



        /// <summary>
        /// ��ȡ��ͬ��λ��ѯ���
        /// </summary>
        /// <returns>�ɹ�: ���ص�SQL��� ʧ��: null</returns>
        private string GetQueryPactUnitsSql()
        {
            string sql = string.Empty;//SQL���

            if (this.Sql.GetCommonSql("Fee.GetPactUnitSql", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.GetPactUnitSql��SQL���";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// ����Where������������ѯ��ͬ��λ��Ϣ
        /// </summary>
        /// <param name="whereIndex">Where��������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        private ArrayList QueryPactUnits(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            string where = string.Empty;//WHERE���

            //���Where���
            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            sql = this.GetQueryPactUnitsSql();

            return this.QueryPactUnitBySql(sql + " " + where, args);
        }

        #endregion

        #region �������

        /// <summary>
        /// ��ú�ͬ��λ����
        /// </summary>
        /// <param name="pactUnit">��ͬ��λʵ��</param>
        /// <returns>��ú�ͬ��λ����</returns>
        private string[] GetPactUnitParams(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            string[] args =
				{	
					pactUnit.ID,
                    pactUnit.PayKind.ID ,
                    pactUnit.Rate.PubRate.ToString(),
                    pactUnit.Rate.PayRate.ToString(),
                    pactUnit.Rate.OwnRate.ToString(),
                    pactUnit.Rate.RebateRate.ToString(),
                    pactUnit.Rate.ArrearageRate.ToString(),
                    NConvert.ToInt32(pactUnit.Rate.IsBabyShared).ToString(),
                    NConvert.ToInt32(pactUnit.IsNeedMCard).ToString(),
                    NConvert.ToInt32(pactUnit.IsInControl).ToString(),
                    pactUnit.ItemType,
                    pactUnit.DayQuota.ToString(),
                    pactUnit.MonthQuota.ToString(),
                    pactUnit.YearQuota.ToString(),
                    pactUnit.OnceQuota.ToString(),
                    pactUnit.PriceForm,
                    pactUnit.BedQuota.ToString(),
                    pactUnit.AirConditionQuota.ToString(),
                    pactUnit.SortID.ToString(),
                    this.Operator.ID,
                    pactUnit.ShortName,
                    pactUnit.PactDllName,
                    pactUnit.PactDllDescription,
                    pactUnit.PactSystemType
				};

            return args;
        }

        /// <summary>
        /// ��ò����ͬ��λ��Ϣ����
        /// </summary>
        /// <param name="pactUnit">��ͬ��λʵ��</param>
        /// <returns>��ú�ͬ��λ����</returns>
        private string[] GetInsertPactUnitParams(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            #region Sql
            /*insert into fin_com_pactunitinfo 
            ( pact_code,pact_name,paykind_code,PRICE_FORM ,
            pub_ratio,pay_ratio,own_ratio,eco_ratio ,arr_ratio,baby_flag,mcard_flag,
            control_flag ,flag,day_limit ,month_limit,year_limit,once_limit ,
            BED_LIMIT,AIR_LIMIT ,SORT_ID ,OPER_CODE,OPER_DATE,SIMPLE_NAME, DLL_NAME,DLL_DESCRIPTION ) 
            values ('{0}','{1}','{2}','{3}',{4},{5},
            {6},{7},{8},'{9}','{10}','{11}','{12}',{13},{14},{15},{16},{17},{18},{19},'{20}',sysdate,'{21}','{22}','{23}')

             */
            #endregion
            string[] args =
				{	
					pactUnit.ID,
                    pactUnit.Name,
                    pactUnit.PayKind.ID ,
                    pactUnit.PriceForm,
                    pactUnit.Rate.PubRate.ToString(),
                    pactUnit.Rate.PayRate.ToString(),
                    pactUnit.Rate.OwnRate.ToString(),
                    pactUnit.Rate.RebateRate.ToString(),
                    pactUnit.Rate.ArrearageRate.ToString(),
                    NConvert.ToInt32(pactUnit.Rate.IsBabyShared).ToString(),
                    NConvert.ToInt32(pactUnit.IsNeedMCard).ToString(),
                    NConvert.ToInt32(pactUnit.IsInControl).ToString(),
                    pactUnit.ItemType,
                    pactUnit.DayQuota.ToString(),
                    pactUnit.MonthQuota.ToString(),
                    pactUnit.YearQuota.ToString(),
                    pactUnit.OnceQuota.ToString(),
                    pactUnit.BedQuota.ToString(),
                    pactUnit.AirConditionQuota.ToString(),
                    pactUnit.SortID.ToString(),
                    this.Operator.ID,
                    pactUnit.ShortName,
                    pactUnit.PactDllName,
                    pactUnit.PactDllDescription,
                    pactUnit.PactSystemType
				};

            return args;
        }


        #endregion

        #endregion

        #region ���з���

        /// <summary>
        /// ������к�ͬ��λ��Ϣ
        /// </summary>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitAll()
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.PactUnitInfo.GetPactUnitInfo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.PactUnitInfo.GetPactUnitInfo��SQL���";

                return null;
            }

            return this.QueryPactUnitBySql(sql);
        }

        /// <summary>
        /// ��������ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitOutPatient()
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.PactUnitInfo.GetPactUnitSqlByPactSysType", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.PactUnitInfo.GetPactUnitSqlByPactSysType ��SQL���";

                return null;
            }

            sql = string.Format(sql, "'0', '1'");

            return this.QueryPactUnitBySql(sql);
        }
        /// <summary>
        /// ���סԺ��ͬ��λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitInPatient()
        {
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.PactUnitInfo.GetPactUnitSqlByPactSysType", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Fee.PactUnitInfo.GetPactUnitSqlByPactSysType ��SQL���";

                return null;
            }

            sql = string.Format(sql, "'0', '2'");

            return this.QueryPactUnitBySql(sql);
        }

        /// <summary>
        /// ���ݼ���ȡͬ��λ
        /// </summary>
        /// <param name="shortName">����</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByShortName(string shortName)
        {
            return this.QueryPactUnits("Fee.GetPactUnitSqlBySIM.Where", shortName);
        }

        /// <summary>
        /// ���ݼ���ģ����ѯȡ��ͬ��λ��Ϣ
        /// </summary>
        /// <param name="shortName">����</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByShortNameLiked(string shortName)
        {
            return this.QueryPactUnits("Fee.GetPactUnitSqlByLikeSIM.Where", shortName);
        }

        /// <summary>
        /// ���ݽ������ȡ��ͬ��λ
        /// </summary>
        /// <param name="payKindCode">����������</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByPayKindCode(string payKindCode)
        {
            return this.QueryPactUnits("Fee.GetPactUnitSqlByPayKindType.Where", payKindCode);            
        }

        /// <summary>
        /// ���ݴ����㷨DLL���ֻ�ȡ��ͬ��λ
        /// </summary>
        /// <param name="dllName">�����㷨DLL����</param>
        /// <returns>�ɹ�: ��ͬ��λ���� ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        public ArrayList QueryPactUnitByDLLName(string dllName)
        {
            return this.QueryPactUnits("Fee.GetPactUnitSqlByDLLName.Where", dllName);
        }

        #endregion

        #region ��������
        /// <summary>
        /// ���ݽ������ȡ��ͬ��λ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        [Obsolete("����,ʹ��QueryPactUnitByPayKindCode", true)]
        public ArrayList GetPactUnitInfoByPayKindType(string strID)
        {
            string strSql = "", strWhere = "";
            if (strID == "" && strID == null) return null;
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSql", ref strSql) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSqlByPayKindType.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            else
            {
                strWhere = string.Format(strWhere, strID);
            }
            strSql += " " + strWhere;
            return null;
        }
        /// <summary>
        /// ���ݼ���ģ����ѯȡ��ͬ��λ��Ϣ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        [Obsolete("����,ʹ��QueryPactUnitByShortNameLiked", true)]
        public ArrayList GetPactUnitInfoByLikeJM(string strID)
        {
            string strSql = "", strWhere = "";
            if (strID == "" && strID == null) return null;
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSql", ref strSql) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSqlByLikeSIM.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            else
            {
                strWhere = string.Format(strWhere, strID);
            }
            strSql += " " + strWhere;
            return null;
        }

        /// <summary>
        /// ���ݼ���ȡͬ��λ
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        [Obsolete("����,ʹ��QueryPactUnitByShortName", true)]
        public ArrayList GetPactUnitInfoByJM(string strID)
        {
            string strSql = "", strWhere = "";
            if (strID == "" && strID == null) return null;
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSql", ref strSql) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            if (this.Sql.GetCommonSql("Fee.GetPactUnitSqlBySIM.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�strSql�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            else
            {
                strWhere = string.Format(strWhere, strID);
            }
            strSql += " " + strWhere;
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("����,ʹ��QueryPactUnitAll", true)]
        public ArrayList GetPactUnitInfo()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.PactUnitInfo.GetPactUnitInfo", ref strSql) == -1) return null;
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.PactInfo info = new FS.HISFC.Models.Base.PactInfo();

                    //��ͬ����
                    info.ID = Reader[0].ToString();
                    //��ͬ��λ����
                    info.Name = Reader[1].ToString();

                    //�������
                    info.PayKind.ID = Reader[2].ToString();

                    //���ѱ���
                    info.Rate.PubRate = NConvert.ToDecimal(Reader[3].ToString().Trim());

                    //�Ը�����
                    info.Rate.PayRate = NConvert.ToDecimal(Reader[4].ToString().Trim());

                    //�Էѱ���
                    info.Rate.OwnRate = NConvert.ToDecimal(Reader[5].ToString().Trim());

                    //�Żݱ���
                    info.Rate.RebateRate = NConvert.ToDecimal(Reader[6].ToString().Trim());

                    //Ƿ�ѱ���
                    info.Rate.ArrearageRate = NConvert.ToDecimal(Reader[7].ToString().Trim());

                    //Ӥ����־ 0 �޹� 1 �й� 
                    info.Rate.IsBabyShared = NConvert.ToBoolean(Reader[8].ToString());

                    //�Ƿ�Ҫ�������ҽ��֤�� 0 �� 1 ��              
                    info.IsNeedMCard = NConvert.ToBoolean(Reader[9].ToString().Trim());

                    //�Ƿ��ܼ�� 1�ܼ��0���ܼ��
                    info.IsInControl = NConvert.ToBoolean(Reader[10].ToString().Trim());

                    //��־  0 ȫ�� 1 ҩƷ 2 ��ҩƷ   
                    info.ItemType = Reader[11].ToString().Trim();

                    //���޶�
                    if (Reader[12] != DBNull.Value)
                    {
                        info.DayQuota = NConvert.ToDecimal(Reader[12].ToString().Trim());
                    }

                    if (Reader[13] != DBNull.Value)
                    {
                        //���޶�
                        info.MonthQuota = NConvert.ToDecimal(Reader[13].ToString().Trim());
                    }
                    if (Reader[14] != DBNull.Value)
                    {
                        //���޶�
                        info.YearQuota = NConvert.ToDecimal(Reader[14].ToString().Trim());
                    }
                    if (Reader[15] != DBNull.Value)
                    {
                        //һ���޶�
                        info.OnceQuota = NConvert.ToDecimal(Reader[15].ToString().Trim());
                    }
                    string PriceForm = Reader[16].ToString();
                    if (PriceForm == "0")
                    {
                        info.PriceForm = "Ĭ�ϼ�";
                    }
                    else if (PriceForm == "1")
                    {
                        info.PriceForm = "�����";
                    }
                    else if (PriceForm == "2")
                    {
                        info.PriceForm = "��ͯ��";
                    }
                    else
                    {
                    }
                    //ȡ��λС��By Maokb -060920 
                    if (Reader[17] != DBNull.Value)
                    {
                        info.BedQuota = NConvert.ToDecimal(Reader[17].ToString());
                    }
                    if (Reader[18] != DBNull.Value)
                    {
                        info.AirConditionQuota = NConvert.ToDecimal(Reader[18].ToString());
                    }
                    if (Reader[19] != DBNull.Value)
                    {
                        info.SortID = NConvert.ToInt32(Reader[19]);
                    }
                    //��ͬ��λ���
                    info.ShortName = Reader[20].ToString();
                    list.Add(info);
                    info = null;
                }
                this.Reader.Close();

            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                list = null;
            }
            return list;
        }

        #endregion

        
		
		

		/// <summary>
		/// ���º�ͬ��λ��Ϣ
		/// </summary>
        /// <param name="pactUnit">��ͬ��λʵ��</param>
		/// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int  UpdatePactUnitInfo(FS.HISFC.Models.Base.PactInfo pactUnit)
		{
			return this.UpdateSingleTable("Fee.InvocieService.UpdatePactUnitInfo", this.GetPactUnitParams(pactUnit));
		}


		/// <summary>
        /// �����ͬ��λ��Ϣ
		/// </summary>
        /// <param name="pactUnit">��ͬ��λʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int InsertPactUnitInfo(FS.HISFC.Models.Base.PactInfo pactUnit)
		{
            return this.UpdateSingleTable("Fee.InvocieService.InsertPactUnitInfo", this.GetInsertPactUnitParams(pactUnit));
		}

		/// <summary>
		/// ɾ����ͬ��λ��Ϣ
		/// </summary>
        /// <param name="pactUnit">��ͬ��λʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
		public int DeletePactUnitInfo(FS.HISFC.Models.Base.PactInfo pactUnit)
		{
            return this.UpdateSingleTable("Fee.InvocieService.DeletePactUnitInfo", pactUnit.ID, pactUnit.ItemType);
		}

		/// <summary>
		/// ���ݺ�ͬ�����ѯ
		/// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
		/// <returns>�ɹ� ��ͬ��λʵ�� ʧ�� Null</returns>
		public FS.HISFC.Models.Base.PactInfo GetPactUnitInfoByPactCode(string pactCode)
		{
            string sql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.PactUnitInfo.GetPactUnitInfoByPactCode", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.PactUnitInfo.GetPactUnitInfoByPactCode��SQL���";
               
                return null;
            }

            ArrayList temp = this.QueryPactUnitBySql(sql, pactCode);

            if (temp == null || temp.Count == 0) 
            {
                return null;
            }

            return temp[0] as FS.HISFC.Models.Base.PactInfo;
		}


        /// <summary>
        /// �����ͬ��λ��̬����������ÿ�β�ѯ�۸�ȡ��ͬ��λ��Ϣ
        /// </summary>
        private Hashtable htPactInfo = new Hashtable();
		/// <summary>
		/// ���ݺ�ͬ��λ����Ŀ����õ���Ŀ�۸�
		/// </summary>
		/// <param name="patient"></param>
		/// <param name="IsDrug"></param>
		/// <param name="ItemID"></param>
		/// <param name="Price"></param>
		/// <returns></returns>
        [Obsolete("�˷���ͣ�ã�����Intergrate.Fee",true)]
		public int GetPrice(FS.HISFC.Models.RADT.PatientInfo patient,HISFC.Models.Base.EnumItemType IsDrug,string ItemID,ref decimal Price)
		{
            //{B9303CFE-755D-4585-B5EE-8C1901F79450} ����ʱ�����޸Ĵ˺���
			string strSql="";
            //��û��ߺ�ͬ��λ
            FS.HISFC.Models.Base.PactInfo pact = null;
            if (htPactInfo.Contains(patient.Pact.ID))
            {
                pact = htPactInfo[patient.Pact.ID] as FS.HISFC.Models.Base.PactInfo;
            }
            else
            {
                pact = this.GetPactUnitInfoByPactCode(patient.Pact.ID);
                htPactInfo.Add(patient.Pact.ID, pact);
            }
            if (pact == null)
            {
                this.Err = "Fee.InvoiceService.GetDrugPrice";
                this.ErrCode = "δ��������!";
                return -1;
            }
			try
			{
                decimal defaultPrice = 0;
                decimal purchasePrice = 0;
				if(IsDrug == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    #region ҩƷ {B9303CFE-755D-4585-B5EE-8C1901F79450}���ݼ۸�ʽȡ�۸�

                    if (this.Sql.GetCommonSql("Fee.InvoiceService.GetDrugPrice",ref strSql)==-1)return -1;
				
					strSql=string.Format(strSql,ItemID);
					if(this.ExecQuery(strSql)==-1)return -1;
					int count=0;

					while(Reader.Read())
					{
                        defaultPrice = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0]);
                        purchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1]);
						count++;
					}
					Reader.Close();
					if(count==0)
					{
						this.Err="Fee.InvoiceService.GetDrugPrice";
						this.ErrCode="δ��������!";
						return -1;
					}
                    if (pact.PriceForm == "�����"&&purchasePrice!=0)
                    {
                        Price = purchasePrice;
                    }
                    else
                    {
                        Price = defaultPrice;
                    }
					#endregion
				}
                else if (IsDrug == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    #region ��ҩƷ,���ݺ�ͬ��λȡ��Ŀ�۸�
                   
                    if (this.Sql.GetCommonSql("Fee.InvoiceService.GetUndrugPrice", ref strSql) == -1) return -1;
                    strSql = string.Format(strSql, ItemID);
                    if (this.ExecQuery(strSql) == -1) return -1;
                    int count = 0;

                    while (Reader.Read())
                    {
                        if (pact.PriceForm == "Ĭ�ϼ�")
                        {
                            Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                        }
                        //{B9303CFE-755D-4585-B5EE-8C1901F79450} �㶫�Ѿ�ȡ����ͯ��-�޸����ݿ�
                        //�����������С��15���꣬ȡ��ͯ��
                        TimeSpan Age = new TimeSpan(this.GetDateTimeFromSysDateTime().Ticks - patient.Birthday.Ticks);
                        if (Age.Days / 365 < 15)
                        {
                            Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                            if (Price == 0)
                            {
                                Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                            }
                        }
                        //���Ƕ�ͯ���������ﻼ��ȥ�����
                        if (pact.PriceForm == "�����")
                        {
                            Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                            if (Price == 0)
                            {
                                Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                            }
                        }
                        count++;
                    }
                    Reader.Close();

                    if (count == 0)
                    {

                        this.Err = "Fee.InvoiceService.GetDrugPrice";
                        this.ErrCode = "δ��������!";
                        return -1;
                    }
                    #endregion
                }
			}		
			catch(Exception e)
			{
				this.Err="Fee.InvoiceService.GetDrugPrice";
				this.ErrCode=e.Message;
				if(Reader.IsClosed==false)Reader.Close();
				return -1;
			}
			return 0;
		}				

        //{40E8A76F-3FEC-4740-8D54-59895DE0DC32}
        public int GetSiRegisterNO(string registerClinicNO,string invoiceNO, ref string registerNO, ref string outInvoiceNO)
        {
            string sql = @"
               select i.reg_no,j.invoice_no 
                 from fin_ipr_siinmaininfo i 
                 left join GZSI_HIS_MZJS j on i.inpatient_no=j.clinic_code 
                                          and i.reg_no = j.jydjh 
                                          and i.invoice_no = j.invoice_no  
                                          and j.valid_flag = '1' 
                                          and j.invoice_no = '{1}'
                where i.valid_flag = '1' 
                  and i.inpatient_no='{0}'
                  and i.reg_no is not null";

            sql = string.Format(sql, registerClinicNO, invoiceNO);

            try
            {
                System.Data.DataSet dsRes = new System.Data.DataSet();
                this.ExecQuery(sql, ref dsRes);
                if (dsRes != null)
                {
                    var dtRow = dsRes.Tables[0];
                    if (dtRow.Rows.Count > 0)
                    {
                        registerNO = dtRow.Rows[0]["reg_no"].ToString();
                        outInvoiceNO = dtRow.Rows[0]["invoice_no"].ToString();
                    }
                }

                return 1;
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

	}
	
}
