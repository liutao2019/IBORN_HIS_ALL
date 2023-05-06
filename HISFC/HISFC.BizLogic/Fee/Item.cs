using System;
using System.Collections;
using System.Data;
using FS.HISFC.Models.Fee.Item;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// Item<br></br>
    /// [��������: ��ҩƷ��Ϣҵ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-09-25]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Item : FS.FrameWork.Management.Database
    {
        #region ˽�к���

        /// <summary>
        /// ����SqlIndex��ѯ��ҩƷ�б�
        /// </summary>
        /// <param name="whereSqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<Undrug> QueryUndrugItemsBase(string whereSqlIndex, params string[] args)
        {
            string selectSQL = string.Empty; //���ȫ����ҩƷ��Ϣ��SELECT���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.Main", ref selectSQL) == -1)
            {
                this.Err = "û���ҵ�SQL��䣬IDΪ[Fee.Item.Main]!";
                this.WriteErr();

                return null;
            }

            string whereSQL = string.Empty;
            if (Sql.GetCommonSql(whereSqlIndex, ref whereSQL) == -1)
            {
                this.Err = "û���ҵ�SQL��䣬IDΪ[" + whereSqlIndex + "]!";
                this.WriteErr();

                return null;
            }

            whereSQL = string.Format(whereSQL, args);

            selectSQL = selectSQL + "\r\n" + whereSQL;

            return this.QueryUndrugItemsBase(selectSQL);
        }

        /// <summary>
        /// ȡ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="sql">��ǰSql���,����SQLID</param>
        /// <returns>�ɹ����ط�ҩƷ���� ʧ�ܷ���null</returns>
        private List<Undrug> QueryUndrugItemsBase(string sql)
        {
            List<Undrug> items = new List<Undrug>(); //���ڷ��ط�ҩƷ��Ϣ������

            //ִ�е�ǰSql���
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    Undrug item = new Undrug();

                    item.ID = this.Reader[0].ToString();//��ҩƷ���� 
                    item.Name = this.Reader[1].ToString(); //��ҩƷ���� 
                    item.SysClass.ID = this.Reader[2].ToString(); //ϵͳ���
                    item.MinFee.ID = this.Reader[3].ToString();  //��С���ô��� 
                    item.UserCode = this.Reader[4].ToString(); //������
                    item.SpellCode = this.Reader[5].ToString(); //ƴ����
                    item.WBCode = this.Reader[6].ToString();    //�����
                    item.GBCode = this.Reader[7].ToString();    //���ұ���
                    item.NationCode = this.Reader[8].ToString();//���ʱ���
                    item.Price = NConvert.ToDecimal(this.Reader[9].ToString()); //Ĭ�ϼ�
                    item.PriceUnit = this.Reader[10].ToString();  //�Ƽ۵�λ
                    item.FTRate.EMCRate = NConvert.ToDecimal(this.Reader[11].ToString()); // ����ӳɱ���
                    item.IsFamilyPlanning = NConvert.ToBoolean(this.Reader[12].ToString()); // �ƻ�������� 
                    item.User01 = this.Reader[13].ToString(); //�ض�������Ŀ
                    item.Grade = this.Reader[14].ToString();//�������־
                    if (string.IsNullOrEmpty(this.Reader[15].ToString()))
                    {
                        item.IsNeedConfirm = false;
                        item.NeedConfirm = EnumNeedConfirm.None;
                    }
                    else
                    {
                        if (Enum.IsDefined(typeof(FS.HISFC.Models.Fee.Item.EnumNeedConfirm),
                            FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString())))
                        {
                            item.NeedConfirm = (FS.HISFC.Models.Fee.Item.EnumNeedConfirm)Enum.Parse(typeof(FS.HISFC.Models.Fee.Item.EnumNeedConfirm), this.Reader[15].ToString());
                            if (item.NeedConfirm == EnumNeedConfirm.All || item.NeedConfirm == EnumNeedConfirm.Inpatient)
                            {
                                item.IsNeedConfirm = true;
                            }
                        }
                    }
                    item.ValidState = this.Reader[16].ToString(); //��Ч�Ա�ʶ ���� 1 ͣ�� 0 ���� 2   
                    item.Specs = this.Reader[17].ToString(); //���
                    item.ExecDept = this.Reader[18].ToString();//ִ�п���
                    item.MachineNO = this.Reader[19].ToString(); //�豸��� �� | ���� 
                    item.CheckBody = this.Reader[20].ToString(); //Ĭ�ϼ�鲿λ��걾
                    item.OperationInfo.ID = this.Reader[21].ToString(); // �������� 
                    item.OperationType.ID = this.Reader[22].ToString(); // ��������
                    item.OperationScale.ID = this.Reader[23].ToString(); //������ģ 
                    item.IsCompareToMaterial = NConvert.ToBoolean(this.Reader[24].ToString());//�Ƿ���������Ŀ��֮����(1�У�0û��) 
                    item.Memo = this.Reader[25].ToString(); //��ע  
                    item.ChildPrice = NConvert.ToDecimal(this.Reader[26].ToString()); //��ͯ��
                    item.SpecialPrice = NConvert.ToDecimal(this.Reader[27].ToString()); //�����
                    item.SpecialFlag = this.Reader[28].ToString(); //ʡ����
                    item.SpecialFlag1 = this.Reader[29].ToString(); //������
                    item.SpecialFlag2 = this.Reader[30].ToString(); //�Է���Ŀ
                    item.SpecialFlag3 = this.Reader[31].ToString();// ������
                    item.SpecialFlag4 = this.Reader[32].ToString();// ����		
                    item.DiseaseType.ID = this.Reader[35].ToString(); //��������
                    item.SpecialDept.ID = this.Reader[36].ToString();  //ר������
                    item.MedicalRecord = this.Reader[37].ToString(); //  --��ʷ�����
                    item.CheckRequest = this.Reader[38].ToString();//--���Ҫ��
                    item.Notice = this.Reader[39].ToString();//--  ע������  
                    item.IsConsent = NConvert.ToBoolean(this.Reader[40].ToString());
                    item.CheckApplyDept = this.Reader[41].ToString();//������뵥����
                    item.IsNeedBespeak = NConvert.ToBoolean(this.Reader[42].ToString());//�Ƿ���ҪԤԼ
                    item.ItemArea = this.Reader[43].ToString();//��Ŀ��Χ
                    item.ItemException = this.Reader[44].ToString();//��ĿԼ��
                    item.UnitFlag = this.Reader[45].ToString(); //[]��λ��ʶ 
                    item.ApplicabilityArea = this.Reader[46].ToString();
                    item.DeptList = this.Reader[47].ToString(); //����������
                    item.ItemPriceType = this.Reader[48].ToString();  //��۷������
                    item.IsOrderPrint = this.Reader[49].ToString();   //ҽ������ӡ��ʾ
                    item.Oper.ID = this.Reader[50].ToString();   //������

                    item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString()); //ֹͣʱ��
                    item.NameCollection.OtherName = Reader[52].ToString();//����
                    item.NameCollection.OtherSpell.SpellCode = Reader[53].ToString();//����ƴ����
                    item.NameCollection.OtherSpell.WBCode = Reader[54].ToString();//���������
                    item.NameCollection.OtherSpell.UserCode = Reader[55].ToString();//�����Զ�����
                    if (this.Reader.FieldCount > 56)
                    {
                        item.ManageClass.ID = this.Reader[56].ToString();//������
                    }
                    items.Add(item);
                }

                return items;
            }
            catch (Exception e)
            {
                this.Err = "��÷�ҩƷ������Ϣ����" + e.Message;
                this.WriteErr();

                return null;
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ���update����insert��ҩƷ�ֵ��Ĵ����������
        /// </summary>
        /// <param name="undrug">��ҩƷʵ��</param>
        /// <returns>��������</returns>
        private string[] GetItemParams(Undrug undrug)
        {
            string[] args = 
			{	
				undrug.ID, 
				undrug.Name,
				undrug.SysClass.ID.ToString(),
				undrug.MinFee.ID.ToString(),
				undrug.UserCode, 
				undrug.SpellCode,
				undrug.WBCode,
				undrug.GBCode, 
				undrug.NationCode,
				undrug.Price.ToString(),
				undrug.PriceUnit,						
				undrug.FTRate.EMCRate.ToString(),
				NConvert.ToInt32(undrug.IsFamilyPlanning).ToString(),	
				"",  				        
				undrug.Grade,					
				//NConvert.ToInt32(undrug.IsNeedConfirm).ToString(),
                ((int)undrug.NeedConfirm).ToString(),
				FS.FrameWork.Function.NConvert.ToInt32(undrug.ValidState).ToString(),		
				undrug.Specs,					
				undrug.ExecDept,					
				undrug.MachineNO,
				undrug.CheckBody,				
				undrug.OperationInfo.ID,                 
				undrug.OperationType.ID,			
				undrug.OperationScale.ID,
				NConvert.ToInt32(undrug.IsCompareToMaterial).ToString(),		
				undrug.Memo,					
				undrug.Oper.ID ,	
				undrug.ChildPrice.ToString(),            
				undrug.SpecialPrice.ToString(),         
				undrug.SpecialFlag,                   
				undrug.SpecialFlag1,                          
				undrug.SpecialFlag2,
				undrug.SpecialFlag3,                     
				undrug.SpecialFlag4,                  
				"0",     
                "0",
				undrug.DiseaseType.ID ,
				undrug.SpecialDept.ID,
				NConvert.ToInt32(undrug.IsConsent).ToString(),
				undrug.MedicalRecord,                        
				undrug.CheckRequest,                          
				undrug.Notice,					
				undrug.CheckApplyDept,
				NConvert.ToInt32(undrug.IsNeedBespeak).ToString(),
			    undrug.ItemArea,
			    undrug.ItemException,
                undrug.UnitFlag,/*[2007/01/19]��ӵ��ֶ�,��λ��ʶ46*/
                undrug.ApplicabilityArea,
               //{2A5608D8-26AD-47d7-82CC-81375722FF72}
                undrug.DeptList,
                //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
                undrug.ItemPriceType,
                undrug.IsOrderPrint
			};

            return args;
        }

        /// <summary>
        /// ͨ����Ŀ����������ű�Ϊ0��Ԫ��,ת���ɷ�ҩƷʵ��
        /// </summary>
        /// <param name="items">��ҩƷ��Ŀ����</param>
        /// <returns>�ɹ����ط�ҩƷʵ��,ʧ�ܷ���null</returns>
        private Undrug GetItemFromArrayList(ArrayList items)
        {
            //����������Ϊ��,˵��sql��������ԭ���������
            if (items == null)
            {
                return null;
            }
            //�����õ�����Ԫ��������0,˵�����ҵ�����Ŀ,������ֻ����һ��Ԫ��
            //����ȡ�ű�Ϊ0��Ԫ��,ת��Undrugʵ��
            if (items.Count > 0)
            {
                Undrug tempUndrug = items[0] as Undrug;

                return tempUndrug;
            }
            else//���Ԫ��������0(������С��0),˵���˱���ķ�ҩƷ��Ŀ������
            {
                return null;
            }
        }

        /// <summary>
        /// ͨ����Ŀ����������ű�Ϊ0��Ԫ��,ת���ɷ�ҩƷʵ��
        /// </summary>
        /// <param name="items">��ҩƷ��Ŀ����</param>
        /// <returns>�ɹ����ط�ҩƷʵ��,ʧ�ܷ���null</returns>
        private Undrug GetItemFromList(List<Undrug> items)
        {
            //����������Ϊ��,˵��sql��������ԭ���������
            if (items == null)
            {
                return null;
            }
            //�����õ�����Ԫ��������0,˵�����ҵ�����Ŀ,������ֻ����һ��Ԫ��
            //����ȡ�ű�Ϊ0��Ԫ��,ת��Undrugʵ��
            if (items.Count > 0)
            {
                return items[0];
            }
            else//���Ԫ��������0(������С��0),˵���˱���ķ�ҩƷ��Ŀ������
            {
                return null;
            }
        }

        #endregion

        #region ���к���

        /// <summary>
        /// �жϸ���Ŀ�Ƿ��Ѿ�ʹ�ù�
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns>true �Ѿ�ʹ�� false û��ʹ��</returns>
        public bool IsUsed(string undrugCode)
        {
            string sql = null; //���ص�SQL���
            string returnRows = null; //�������Ѿ�ʹ�õĵ�ǰ��ҩƷ��Ŀ
            bool isUsed = false; //�Ƿ����ɾ��

            //��õ�ǰ��ҩƷ��ʹ�ô���SQL���
            if (this.Sql.GetCommonSql("Fee.Item.CanDelete.Select", ref sql) == -1)
            {
                this.Err = "û���ҵ�Fee.Item.CanDelete.Select�ֶ�";

                return false;
            }

            //��ʽ��SQL���
            try
            {
                sql = string.Format(sql, undrugCode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return false;
            }

            //��õ�ǰ��ҩƷ��ʹ�ô���
            returnRows = this.ExecSqlReturnOne(sql);

            //���������Ŀ����0,�÷�ҩƷ�Ѿ�ʹ��
            if (NConvert.ToInt32(returnRows) > 0)
            {
                isUsed = true;
            }
            else//���ص���ĿС�ڵ���0 ˵������Ŀû��ʹ��
            {
                isUsed = false;
            }

            return isUsed;
        }

        /// <summary>
        /// ���ղ�ѯ������÷�ҩƷ��Ϣ�б�
        /// </summary>
        /// <param name="undrugCode">���Ϊ��ҩƷ����Ϊ��ѯ��һ��Ŀ,Ϊ�ַ���"all"ʱΪ��ѯ������Ŀ</param>
        /// <param name="validState">��ҩƷ״̬: ����(1) ͣ��(0) ����(2) ����(all)</param>
        /// <returns>�ɹ�:���ط�ҩƷʵ������ ʧ��:����null</returns>
        public ArrayList Query(string undrugCode, string validState)
        {
            List<Undrug> list = this.QueryUndrugItemsBase("Fee.Item.Query.ByCode", undrugCode, validState);
            if (list != null)
            {
                return new ArrayList(list);
            }
            return null;
        }

        /// <summary>
        /// ���ղ�ѯ������÷�ҩƷ��Ϣ�б�
        /// </summary>
        /// <param name="undrugCode">���Ϊ��ҩƷ����Ϊ��ѯ��һ��Ŀ,Ϊ�ַ���"all"ʱΪ��ѯ������Ŀ</param>
        /// <param name="validState">��ҩƷ״̬: ����(1) ͣ��(0) ����(2) ����(all)</param>
        /// <returns>�ɹ�:���ط�ҩƷʵ������ ʧ��:����null</returns>
        public List<Undrug> QueryList(string undrugCode, string validState)
        {
            return this.QueryUndrugItemsBase("Fee.Item.Query.ByCode", undrugCode, validState);
        }

        /// <summary>
        /// ��ѯ�����շ���Ŀ
        /// </summary>
        /// <param name="dept">����</param>
        /// <returns></returns>
        public List<Undrug> QueryList(string dept)
        {
            return this.QueryUndrugItemsBase("Fee.Item.Query.ByDept", dept);
        }

        /// <summary>
        /// ���ݷ�ҩƷ�����ø���Ŀ��Ϣ(����Ŀ������Ч)
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns>�ɹ�:���ط�ҩƷʵ�� ʧ��:����null</returns>
        public Undrug GetValidItemByUndrugCode(string undrugCode)
        {
            List<Undrug> list = this.QueryUndrugItemsBase("Fee.Item.Query.ByCode", undrugCode,"1");
            if (list != null
                && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// ���ݷ�ҩƷ�����ø���Ŀ��Ϣ(�����Ƿ���Ч)
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns>�ɹ�:���ط�ҩƷʵ�� ʧ��:����null</returns>
        public Undrug GetItemByUndrugCode(string undrugCode)
        {
            List<Undrug> list = this.QueryUndrugItemsBase("Fee.Item.Query.ByCode", undrugCode, "all");
            if (list != null
                && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// ���ݷ�ҩƷ���� ������ѯ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="undrugCodes"></param>
        /// <returns></returns>
        public List<Undrug> GetItemByCodeBatch(string undrugCodes)
        {
            return this.QueryUndrugItemsBase("Fee.ItemInfo.Query.ByItemID.Batch", undrugCodes);
        }

        /// <summary>
        /// ��÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="undrugCode"></param>
        /// <returns>�ɹ� ��ҩƷ��Ϣ ʧ�� null</returns>
        public Undrug GetUndrugByCode(string undrugCode)
        {
            return GetItemByUndrugCode(undrugCode);
        }

        /// <summary>
        /// �����Զ�������÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="userCode">��Ŀ�Զ�����</param>
        /// <returns>�ɹ����ط�ҩƷ��Ŀʵ�� ʧ�ܷ���null</returns>
        public Undrug GetItemByUserCode(string userCode)
        {
            List<Undrug> list = this.QueryUndrugItemsBase("Fee.ItemInfo.Query.ByUserCode", userCode);
            if (list != null
                && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// ��ȡ��Ŀ����
        /// </summary>
        /// <param name="undrugCode"></param>
        /// <returns></returns>
        public decimal GetItemRate(string undrugCode)
        {
            string sql = null;//SQL���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.Info.GetItemRate", ref sql) == -1)
            {
                sql = "select EMERG_SCALE from fin_com_undruginfo where item_code='{0}'";
            }

            decimal itemRate = 1.00M;
            //��ʽ��SQL���
            try
            {
                sql = string.Format(sql, undrugCode);

                if (this.ExecQuery(sql) <= 0)
                {
                    this.WriteErr();
                    return itemRate;
                }

                if (this.Reader != null)
                {
                    if (this.Reader.Read())
                    {
                        itemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                        if (itemRate <= 0)
                        {
                            itemRate = 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return itemRate;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }

            return itemRate;
        }

        /// <summary>
        /// �����Ч��,��Ŀ���Ϊ��������Ŀ����
        /// </summary>
        /// <returns>�ɹ�:��Ŀ���� ʧ�ܷ���null</returns>
        public ArrayList QueryOperationItems()
        {
            string strSql = string.Empty;
            string strSql1 = string.Empty;
            if(this.Sql.GetSql("Fee.Item.Main",ref strSql) == -1)
            {
               return null;
            }
            if (this.Sql.GetSql("Fee.ItemInfo.Query.GetOperationItemList", ref strSql1) == -1)
            {
                return null;
            }
            List<Undrug> list = this.QueryUndrugItemsBase(strSql + " " + strSql1);
            if (list != null)
            {
                return new ArrayList(list);
            }
            return null;
        }

        /// <summary>
        /// ��ȡICD9������Ŀ
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOperationICDItems()
        {
            ArrayList al = new ArrayList();
            string strSql = string.Empty;
            if (this.Sql.GetSql("Fee.ItemInfo.Query.GetOperationICDItemList", ref strSql) == -1)
            {
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug operationItem = new FS.HISFC.Models.Fee.Item.Undrug();
                    operationItem.UserCode = this.Reader[0].ToString();
                    operationItem.Name = this.Reader[1].ToString();
                    operationItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    operationItem.SpellCode = this.Reader[3].ToString();
                    operationItem.WBCode = this.Reader[4].ToString();
                    operationItem.OperationInfo.ID = this.Reader[5].ToString();
                    operationItem.OperationType = null;
                    operationItem.OperationScale = null;
                    operationItem.ID = this.Reader[8].ToString();
                    al.Add(operationItem);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;

        }

        /// <summary>
        /// ������п��ܵ���Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ч�Ŀ�����Ŀ��Ϣ, ʧ�� null</returns>
        public ArrayList QueryValidItems()
        {
            return this.Query("all", "1");
        }

        /// <summary>
        /// ���ݿ��һ�����п��ܵ���Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ч�Ŀ�����Ŀ��Ϣ, ʧ�� null</returns>
        public ArrayList QueryItemsForOrder(string deptCode, bool isAll)
        {
            string sql = string.Empty; //���ȫ����ҩƷ��Ϣ��SELECT���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.Info.ForOrder", ref sql) == -1)
            {
                this.Err = "û���ҵ�SQL������Ϊ��Fee.Item.Info.ForOrder��!";
                this.WriteErr();

                return null;
            }
            //��ʽ��SQL���
            try
            {
                if (isAll)
                {
                    sql = string.Format(sql, "all", "all", deptCode);
                }
                else
                {
                    sql = string.Format(sql, "all", "1", deptCode);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            List<Undrug> list = this.QueryUndrugItemsBase(sql);
            if (list != null)
            {
                return new ArrayList(list);
            }
            return null;
        }

        /// <summary>
        /// ������п��ܵ���Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ч�Ŀ�����Ŀ��Ϣ, ʧ�� null</returns>
        public List<Undrug> QueryValidItemsList()
        {
            return this.QueryList("all", "1");
        }

        /// <summary>
        /// ��ÿ������п��ܵ���Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ��Ч�Ŀ�����Ŀ��Ϣ, ʧ�� null</returns>
        public List<Undrug> QueryValidItemsList(string dept)
        {
            return this.QueryList(dept);
        }

        /// <summary>
        /// ���������Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ��Ϣ, ʧ�� null</returns>
        public List<Undrug> QueryAllItemList()
        {
            return this.QueryList("all", "all");
        }


        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ����סԺ��ˮ�ţ�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNO(string inPatientNO,string invoiceNo)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {
                    
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());
                    //����Ż� ���� ��� {18653415-C332-4a7f-8772-A1559E5FA88A}
                    feeInfo.FT.DonateCost = NConvert.ToDecimal(this.Reader[8].ToString());
                    feeInfo.FT.DerateCost = NConvert.ToDecimal(this.Reader[9].ToString());
                    feeInfo.Item.User02 = this.Reader[10].ToString();
                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }

        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ����סԺ��ˮ�ţ����̱� = ҽԺ�ۺ�� - ҽ���ۣ�
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNOAndCI(string inPatientNO)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNoAndCI", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNoAndCI��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());//ԭ��
                    //����Ż� ���� ��� {18653415-C332-4a7f-8772-A1559E5FA88A}
                    feeInfo.FT.DonateCost = NConvert.ToDecimal(this.Reader[8].ToString());//�ۺ�
                    feeInfo.FT.DerateCost = NConvert.ToDecimal(this.Reader[9].ToString());//�̱�
                    feeInfo.Item.User02 = this.Reader[10].ToString();
                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }

        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ ����dt ���ؼ���ֵ//{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientTotalFeeDTInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);
            DataSet ds=new DataSet ();
            DataTable dt=null ;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;
            
        }

        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ ����dt ���ؼ���ֵ������ҽԺ���뵼��{940D2882-F02B-488f-A8E3-07689B0D064D}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientTotalFeeDTInfoByInPatientNOHospitalDrugNo(string inPatientNO, string invoiceNo)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo3", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo3��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }


        /// <summary>
        /// ��ѯ����С�����û����嵥��Ϣ����סԺ��ˮ�ţ�//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}���С�������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientChildTotalFeeListInfoByInPatientNO(string inPatientNO)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo1��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());
                    //����Ż� ���� ��� {18653415-C332-4a7f-8772-A1559E5FA88A}
                    feeInfo.FT.DonateCost = NConvert.ToDecimal(this.Reader[8].ToString());
                    feeInfo.FT.DerateCost = NConvert.ToDecimal(this.Reader[9].ToString());
                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }


        // <summary>
        /// ��ѯ����С�����û����嵥��Ϣ ����dt ���ؼ���ֵ //{105E05C7-E3E0-43B6-B88F-480861F1F4B6}���С�������嵥����{5A04A8EF-06C3-45b9-9E6C-E5D152836257}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientChildTotalFeeDTInfoByInPatientNO(string inPatientNO)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo1", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo1��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }

        // <summary>
        /// ��ѯ���������ϵ�ҽ�������嵥��Ϣ ����dt ���ؼ���ֵ {5A04A8EF-06C3-45b9-9E6C-E5D152836257}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientChildTotalFeeDTInfoByInPatientNOBaByYB1(string inPatientNO)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo5", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo1��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }

        // <summary>
        /// ��ѯС���ҿ����������ϵ�ҽ�������嵥��Ϣ ����dt ���ؼ���ֵ 
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientChildTotalFeeDTInfoByInPatientNOBaByYB2(string inPatientNO)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo4", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo1��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }

        /// <summary>
        /// ��ѯ����������û����嵥��Ϣ����סԺ��ˮ�ţ�//{105E05C7-E3E0-43B6-B88F-480861F1F4B6}������豾������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientMontherTotalFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo2", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());
                    //����Ż� ���� ��� {18653415-C332-4a7f-8772-A1559E5FA88A}
                    feeInfo.FT.DonateCost = NConvert.ToDecimal(this.Reader[8].ToString());
                    feeInfo.FT.DerateCost = NConvert.ToDecimal(this.Reader[9].ToString());
                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }

        /// <summary>
        /// ��ѯ����������û����嵥��Ϣ ����dt ���ؼ���ֵ //{105E05C7-E3E0-43B6-B88F-480861F1F4B6}������豾������嵥����
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetPatientMontherTotalFeeDTInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNo2", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }


        /// <summary>
        /// ��ѯ���߷��û����嵥��Ϣ����סԺ��ˮ�š���Ʊ�š���ʿվ��// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList GetPatientTotalFeeListInfoByInPatientNOAndNurseCode(string inPatientNO, string invoiceNo, string nurseCode)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryTotalFeeByInPatienNoAndNurseCode", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNoAndNurseCode��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo, nurseCode);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());
                    feeInfo.ConfirmOper.Dept.ID = this.Reader[8].ToString();
                    feeInfo.ConfirmOper.Dept.Name = this.Reader[9].ToString();
                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }
        /// <summary>
        /// ��ѯ���߷�����ϸ�嵥��Ϣ����סԺ��ˮ�ţ�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientDetailFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryDetailFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryDetailFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());

                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }

        /// <summary>
        /// ��ѯ���߷���һ���嵥��Ϣ����סԺ��ˮ�ţ�// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public ArrayList GetPatientOneDayFeeListInfoByInPatientNO(string inPatientNO, string invoiceNo, string date)
        {

            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.Item.QueryOneDayFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryOneDayFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo, date);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());

                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }


        /// <summary>
        /// סԺԤ���㱨��  {34a15202-a3f9-4d3e-9bad-c7e6783b540c}
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetBalanceFeeByInPatienNo(string inPatientNO, string invoiceNo)
        {
            string sql = null;
            ArrayList items = new ArrayList();
            if (this.Sql.GetCommonSql("Fee.PreBalanceList.QueryBalanceFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.PreBalanceList.QueryBalanceFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeInfo.User01 = this.Reader[0].ToString();
                    feeInfo.Item.ID = this.Reader[1].ToString();
                    feeInfo.Item.Name = this.Reader[2].ToString();
                    feeInfo.Item.Specs = this.Reader[3].ToString();
                    feeInfo.Item.Price = NConvert.ToDecimal(this.Reader[4].ToString());
                    feeInfo.Item.Qty = NConvert.ToDecimal(this.Reader[5].ToString());
                    feeInfo.Item.PriceUnit = this.Reader[6].ToString();
                    feeInfo.FT.TotCost = NConvert.ToDecimal(this.Reader[7].ToString());

                    items.Add(feeInfo);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        
        
        }

        /// <summary>
        /// סԺԤ���㱨��
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetDTBalanceFeeByInPatienNo(string inPatientNO, string invoiceNo)
        {
            string sql = null;
            if (this.Sql.GetCommonSql("Fee.PreBalanceList.QueryBalanceFeeByInPatienNo", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.QueryTotalFeeByInPatienNo��Sql���!";

                return null;
            }
            sql = string.Format(sql, inPatientNO, invoiceNo);
            DataSet ds = new DataSet();
            DataTable dt = null;
            if (ExecQuery(sql, ref ds) > 0)
            {
                if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];
            }
            return dt;

        }


        /// <summary>
        /// ���ȫ�����÷�ҩƷ��Ϣ�������Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ�:ȫ�����÷�ҩƷ��Ϣ�������Ŀ��Ϣ ʧ��: null</returns>
        public ArrayList GetAvailableListWithGroup()
        {
            string sql = null; //���ȫ����ҩƷ��Ϣ��SELECT���
            ArrayList items = new ArrayList(); //���ڷ��ط�ҩƷ��Ϣ������

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.Info.GetAvailableListWithGroup", ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:Fee.Item.Undrug.Info.GetAvailableListWithGroup��Sql���!";

                return null;
            }

            //���ִ�в�ѯSQL���,��ô����null
            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            try
            {
                //ѭ���������
                while (this.Reader.Read())
                {
                    Undrug item = new Undrug();//��ʱ��ҩƷ��Ϣ

                    item.ID = this.Reader[0].ToString();
                    item.Name = this.Reader[1].ToString();
                    item.SysClass.ID = this.Reader[2].ToString();
                    item.UserCode = this.Reader[3].ToString();
                    item.SpellCode = this.Reader[4].ToString();
                    item.WBCode = this.Reader[5].ToString();
                    item.Price = NConvert.ToDecimal(this.Reader[6].ToString());
                    item.PriceUnit = this.Reader[7].ToString();
                    item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                    item.ExecDept = this.Reader[9].ToString();
                    item.MachineNO = this.Reader[10].ToString();
                    item.CheckBody = this.Reader[11].ToString();
                    item.Memo = this.Reader[12].ToString();
                    item.DiseaseType.ID = this.Reader[13].ToString();
                    item.SpecialDept.ID = this.Reader[14].ToString();
                    item.MedicalRecord = this.Reader[15].ToString();
                    item.CheckRequest = this.Reader[16].ToString();
                    item.Notice = this.Reader[17].ToString();
                    item.Grade = this.Reader[18].ToString();//-- ���

                    items.Add(item);
                }//ѭ������

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                //���Readerû�йر�,�ر�֮
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
            return items;
        }

        /// <summary>
        /// ����µķ�ҩƷ����
        /// </summary>
        /// <returns>�µķ�ҩƷ����</returns>
        public string GetUndrugCode()
        {
            string tempUndrugCode = null;//��ʱ��ҩƷ����
            string sql = null;//SQL���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.UndrugCode", ref sql) == -1)
            {
                this.Err = "��÷�ҩƷ��ˮ�Ų�ѯ�ֶ�Fee.Item.UndrugCode����!";

                return null;
            }

            tempUndrugCode = this.ExecSqlReturnOne(sql);

            tempUndrugCode = "F" + tempUndrugCode.PadLeft(11, '0');

            return tempUndrugCode;
        }

        /// <summary>
        /// ���ҩƷ�ֵ��(fin_com_undruginfo)�в���һ����¼
        /// </summary>
        /// <param name="item">��ҩƷʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int InsertUndrugItem(Undrug item)
        {
            string sql = null; //����fin_com_undruginfo��SQL���

            if (this.Sql.GetCommonSql("Fee.Item.InsertItem", ref sql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.InsertItem��SQL���ʧ��!";

                return -1;
            }
            //��ʽ��SQL���
            try
            {
                //ȡ�����б�
                string[] parms = this.GetItemParams(item);
                //�滻SQL����еĲ�����
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���·�ҩƷ��Ϣ���Է�ҩƷ����Ϊ����
        /// </summary>
        /// <param name="item">��ҩƷʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 ,δ���µ����� 0</returns>
        public int UpdateUndrugItem(Undrug item)
        {
            string sql = null; //����fin_com_undruginfo��SQL���

            if (this.Sql.GetCommonSql("Fee.Item.UpdateItem", ref sql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.UpdateItem��SQL���ʧ��!";

                return -1;
            }
            //��ʽ��SQL���
            try
            {
                //ȡ�����б�
                string[] parms = GetItemParams(item);
                //�滻SQL����еĲ�����
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ɾ����ҩƷ��Ϣ
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1 δɾ�������� 0</returns>
        public int DeleteUndrugItemByCode(string undrugCode)
        {
            string sql = null; //���ݷ�ҩƷ����ɾ��ĳһ��ҩƷ��Ϣ��DELETE���

            if (this.Sql.GetCommonSql("Fee.Item.DeleteItem", ref sql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.DeleteItem��SQL���ʧ��!";

                return -1;
            }
            //��ʽ��SQL���
            try
            {
                sql = string.Format(sql, undrugCode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ��������Ŀ������������{2A5608D8-26AD-47d7-82CC-81375722FF72}
        /// </summary>
        /// <param name="undrugCode">��ҩƷ����</param>
        /// <returns> 0</returns>
        public FS.HISFC.Models.Fee.Item.Undrug SelectUndrugDeptListByCode(string undrugCode)
        {
            string sql = "";
            FS.HISFC.Models.Fee.Item.Undrug returnValue = new Undrug();
            if (this.Sql.GetCommonSql("Fee.Item.Info.SelectDeptList", ref sql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.Info.SelectDeptList��SQL���ʧ��!";

                return null;
            }
            //��ʽ��SQL���
            try
            {
                sql = string.Format(sql, undrugCode);
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    returnValue.DeptList = this.Reader[0].ToString();
                    returnValue.ItemPriceType = this.Reader[1].ToString();
                    returnValue.IsOrderPrint = this.Reader[2].ToString();
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }
            return returnValue;

        }

        /// <summary>
        /// ����ҽ������ӡ��־������Ŀ
        /// {2AFC76CB-3353-4865-AEB4-AFBEE09DD1D7}
        /// </summary>
        /// <returns>������Ŀ����</returns>
        public ArrayList SelectAllItemByOrderPrint(string tag)
        {
            string strSql = string.Empty;
            ArrayList list = new ArrayList();
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            if (this.Sql.GetCommonSql("Fee.Item.Info.SelectAllItemByOrderPrint.Where", ref strSql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.Info.SelectAllItemByOrderPrint.Where��SQL���ʧ��!";

                return null;
            }          
            try
            {
                strSql = string.Format(strSql, tag);
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    undrug = new Undrug();
                    undrug.ID = this.Reader[0].ToString();
                    list.Add(undrug);
                }
            }
            catch { }
            finally { Reader.Close(); }
            return list;
        }

        /// <summary>
        /// ��ҩƷ����ר�� ��ʱ���������Ч�� ����������� ��ֻ���·�ҩƷ�� Ĭ�ϼ� ����ͯ�ۣ� �����
        /// </summary>
        /// <param name="item">�۸�仯��ķ�ҩƷʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1 δ���µ����� 0</returns>
        public int AdjustPrice(Undrug item)
        {
            string sql = null; //����SQL���

            if (this.Sql.GetCommonSql("Fee.Item.ItemPriceSave", ref sql) == -1)
            {
                this.Err = "�������Ϊ:Fee.Item.ItemPriceSave��SQL���ʧ��!";

                return -1;
            }
            //��ʽ��SQL���
            try
            {
                //�滻SQL����еĲ�����
                sql = string.Format(sql, item.ID, item.Price, item.ChildPrice, item.SpecialPrice);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            //{58010499-3CA3-4b9d-B537-BBF964F8EB8B}  ���ݱ��ε�����Ŀ���°����˸���ϸ��Ŀ�ĸ�����Ŀ�۸�
            if (this.ExecNoQuery(sql) == -1)
            {
                return -1;
            }

            return this.AdjustZTPrice(item);
        }

        /// <summary>
        /// ��ҩƷ����ʱ ���ݵ��۵ķ�ҩƷ������صĸ�����Ŀ�۸�
        /// 
        /// {58010499-3CA3-4b9d-B537-BBF964F8EB8B}  ���ݱ��ε�����Ŀ���°����˸���ϸ��Ŀ�ĸ�����Ŀ�۸�
        /// </summary>
        /// <param name="adjustPriceItem">�۸�仯��ķ�ҩƷʵ��</param>
        /// <returns>�ɹ�1 ʧ��-1 </returns>
        public int AdjustZTPrice(Undrug adjustPriceItem)
        {
            if (adjustPriceItem.UnitFlag == "1")            //������Ŀ����Ҫ���к�������
            {
                return 1;
            }

            List<FS.FrameWork.Models.NeuObject> ztList = this.QueryZTListByDetailItem(adjustPriceItem);
            if (ztList == null)
            {
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject ztInfo in ztList)
            {
                if (this.UpdateZTPrice(ztInfo.ID) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���ݸ�����Ŀ��ϸ���¼�����¸�����Ŀ�۸�
        /// 
        /// {58010499-3CA3-4b9d-B537-BBF964F8EB8B}  ���ݱ��ε�����Ŀ���°����˸���ϸ��Ŀ�ĸ�����Ŀ�۸�
        /// </summary>
        /// <param name="undrugZTCode">������Ŀ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int UpdateZTPrice(string undrugZTCode)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Item.UpdateZTPrice", ref sql) == -1)
            {
                this.Err = "û���ҵ�Fee.Item.UpdateZTPrice�ֶ�!";
                this.WriteErr();
                return -1;
            }

            sql = string.Format(sql, undrugZTCode);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���ݷ�ҩƷ��ϸ��Ŀ��ȡ�����˸���ϸ��Ŀ�ĸ�����Ŀ�б�
        /// 
        /// {58010499-3CA3-4b9d-B537-BBF964F8EB8B}  ���ݱ��ε�����Ŀ���°����˸���ϸ��Ŀ�ĸ�����Ŀ�۸�
        /// </summary>
        /// <param name="detailItem">��ҩƷ��ϸ��Ŀ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected List<FS.FrameWork.Models.NeuObject> QueryZTListByDetailItem(Undrug detailItem)
        {
            string sql = string.Empty; //���ȫ������ƻ���SELECT���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.QueryZTListByDetailItem", ref sql) == -1)
            {
                this.Err = "û���ҵ�Fee.Item.QueryZTListByDetailItem�ֶ�!";
                this.WriteErr();

                return null;
            }

            try
            {
                sql = string.Format(sql, detailItem.ID);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                List<FS.FrameWork.Models.NeuObject> ztList = new List<FS.FrameWork.Models.NeuObject>();
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();

                    tempObj.ID = this.Reader[0].ToString();             //������Ŀ����
                    tempObj.Name = this.Reader[1].ToString();           //������Ŀ����

                    ztList.Add(tempObj);
                }

                return ztList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        #region ִ�е����� ����Ŀά��
        //addby xuewj 2009-8-26 ִ�е����� ����Ŀά�� {0BB98097-E0BE-4e8c-A619-8B4BCA715001}

        /// <summary>
        /// ��ȡ���ڷ�ҩƷ��Ŀִ�е��еķ�ҩƷ��Ŀ
        /// </summary>
        /// <param name="nruseID">��ʿվ����</param>
        /// <param name="sysClass">ҽ�����</param>
        /// <param name="validState">��ҩƷ״̬: ����(1) ͣ��(0) ����(2) ����(all)</param>
        /// <returns></returns>
        public int QueryItemOutExecBill(string nruseID, string sysClass, string validState, ref DataSet ds)
        {
            string sql = string.Empty; //���ȫ����ҩƷ��Ϣ��SELECT���

            //ȡSELECT���
            if (this.Sql.GetCommonSql("Fee.Item.Info.OutExecBill", ref sql) == -1)
            {
                this.Err = "û���ҵ�Fee.Item.Info�ֶ�!";
                this.WriteErr();

                return -1;
            }
            //��ʽ��SQL���
            try
            {
                sql = string.Format(sql, nruseID, sysClass, validState);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            //����SQL���ȡ��ҩƷ�����鲢��������
            return this.ExecQuery(sql, ref ds);
        }

        #endregion

        #region zl ��ҩƷ����ά��{CA82280B-51B6-4462-B63E-43F4ECF456A3}

        /// <summary>
        /// ������Ŀ���룬��ȡִ�п���(id='{0}' or id="ALL")
        /// </summary>
        /// <param name="id">��Ŀ����</param>
        /// <returns></returns>
        public ArrayList GetDeptByItemCode(string id)
        {
            ArrayList list = new ArrayList();
            string sql = "";
            if (this.Sql.GetCommonSql("UnDrugDept.Compare.Query", ref sql) == -1)
            {
                this.Err = "û���ҵ�UnDrugDept.Compare.Query";
                return null;
            }

            sql = string.Format(sql, id);

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "û���ҵ�����";
                return null;
            }

            try
            {
                FS.HISFC.Models.Fee.Item.Undrug obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Fee.Item.Undrug();
                    obj.ID = this.Reader[0].ToString();                //��ĿID
                    obj.Name = this.Reader[1].ToString();          //��Ŀ����
                    obj.ExecDept = this.Reader[2].ToString();     //����ID
                    obj.User01 = this.Reader[3].ToString();        //��������                 
                    obj.UserCode = this.Reader[4].ToString();      //SOID
                    obj.Memo = this.Reader[5].ToString();         //Ĭ�ϱ��
                    obj.ItemArea = this.Reader[6].ToString();     //���÷�Χ
                    obj.Oper.ID = this.Reader[7].ToString(); //������
                    obj.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);    //��������

                    obj.User02 = this.Reader[2].ToString();//ԭ����ID(���ڸ��ºͲ������)
                    obj.User03 = this.Reader[0].ToString();//ԭ��ĿID(���ڸ��ºͲ������)

                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                this.Err = "��ѯ����";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return list;
        }

        /// <summary>
        /// �����ҩƷִ�п������ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="company">��ҩƷʵ��</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int SetUnDrugCompare(FS.HISFC.Models.Fee.Item.Undrug item)
        {
            int parm;
            //ִ�и��²���
            parm = UpdateUnDrugCompare(item);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = InsertUnDrugCompare(item);
            }
            return parm;
        }

        /// <summary>
        /// ���·�ҩƷִ�п��ұ�
        /// </summary>
        /// <param name="company">��ҩƷ��Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateUnDrugCompare(FS.HISFC.Models.Fee.Item.Undrug item)
        {
            string strSQL = "";
            if (this.Sql.GetCommonSql("UnDrugDept.Compare.Update", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�UnDrugDept.Compare.Update�ֶ�!";
                return -1;
            }

            try
            {
                object[] strParm = myGetParmItem(item);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);       //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            //ִ��sql���
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���ҩƷִ�п��ұ��в���һ����¼
        /// </summary>
        /// <param name="company">��ҩƷ��Ϣ</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertUnDrugCompare(FS.HISFC.Models.Fee.Item.Undrug item)
        {
            string strSQL = "";
            if (this.Sql.GetCommonSql("UnDrugDept.Compare.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�UnDrugDept.Compare.Insert�ֶ�!";
                return -1;
            }

            try
            {
                object[] strParm = myGetParmItem(item);  //ȡ�����б�
                strSQL = string.Format(strSQL, strParm);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        public int DeleteUnDrugCompare(FS.HISFC.Models.Fee.Item.Undrug item)
        {
            string strSQL = "";
            if (this.Sql.GetCommonSql("UnDrugDept.Compare.Delete", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�UnDrugDept.Compare.Delete�ֶ�!";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, item.User03, item.User02);    //�滻SQL����еĲ�����
            }
            catch (Exception ex)
            {
                this.Err = "ɾ��ʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���update����insert��˾��Ĵ����������
        /// </summary>
        /// <param name="company">��˾��Ϣ</param>
        /// <returns>��������</returns>
        private object[] myGetParmItem(FS.HISFC.Models.Fee.Item.Undrug item)
        {

            object[] strParm ={   
								 item.ID,                //����ĿID
                    item.ExecDept ,   //�¿���ID
                    item.User01,        //��������                 
                    FS.FrameWork.Function.NConvert.ToDecimal( item.UserCode),      //SOID
                    item.Memo,        //Ĭ�ϱ��
                    item.ItemArea,     //���÷�Χ
                    item.Oper.ID, //������
                    item.Oper.OperTime.ToString(),    //��������
                    item.User02 ,//ԭ����ID,
                    item.User03 //ԭ��ĿID
							 };
            return strParm;
        }

        /// <summary>
        /// ��������סԺ���ط�ҩƷִ�п���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetDeptNeuobjByItemID(string id, string type)
        {
            ArrayList list = new ArrayList();
            string sql = "";
            if (this.Sql.GetCommonSql("UnDrugDeptNeuobj.Compare.Query", ref sql) == -1)
            {
                this.Err = "û���ҵ�UnDrugDeptNeuobj.Compare.Query";
                return null;
            }

            sql = string.Format(sql, id, type);

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "û���ҵ�����";
                return null;
            }

            try
            {
                FS.FrameWork.Models.NeuObject obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();                //����ID
                    obj.Name = this.Reader[1].ToString();          //��������
                    obj.User01 = this.Reader[2].ToString();        //soid
                    obj.User02 = this.Reader[3].ToString();        //Ĭ�ϱ��
                    obj.User03 = this.Reader[4].ToString();        //���÷�Χ(0 ȫԺ 1���� 2סԺ)
                    obj.Memo = this.Reader[5].ToString();         //��ҩƷID

                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                this.Err = "��ѯ����";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return list;
        }      

        #endregion

        #region ��ҩƷ�����������Ϣ{933F5263-3408-4ccd-A2A6-4C3693A9D10C}

        /// <summary>
        /// ������Ч��������Ϣ
        /// </summary>
        /// <param name="lstUndrugzt">���ص����ݼ�</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryAllValidItemztAllDepts(ref List<FS.HISFC.Models.Fee.Item.Undrug> lstUndrugzt)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Fee.Itemzt.Info.AllDepts", ref strsql) == -1)
            {
                return -1;
            }

            //ִ�е�ǰSql���
            if (this.ExecQuery(strsql) == -1)
            {
                this.Err = this.Sql.Err;

                return -1;
            }

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();

                    item.ID = this.Reader[0].ToString();//��ҩƷ���� 
                    item.Name = this.Reader[1].ToString(); //��ҩƷ���� 
                    item.SysClass.ID = this.Reader[2].ToString(); //ϵͳ���
                    item.MinFee.ID = this.Reader[3].ToString();  //��С���ô��� 
                    item.UserCode = this.Reader[4].ToString(); //������
                    item.SpellCode = this.Reader[5].ToString(); //ƴ����
                    item.WBCode = this.Reader[6].ToString();    //�����
                    item.GBCode = this.Reader[7].ToString();    //���ұ���
                    item.NationCode = this.Reader[8].ToString();//���ʱ���
                    item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString()); //���׼�
                    item.PriceUnit = this.Reader[10].ToString();  //�Ƽ۵�λ
                    item.FTRate.EMCRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11].ToString()); // ����ӳɱ���
                    item.IsFamilyPlanning = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString()); // �ƻ�������� 
                    item.User01 = this.Reader[13].ToString(); //�ض�������Ŀ
                    item.Grade = this.Reader[14].ToString();//�������־
                    item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[15].ToString());//ȷ�ϱ�־ 1 ��Ҫȷ�� 0 ����Ҫȷ��
                    item.ValidState = this.Reader[16].ToString(); //��Ч�Ա�ʶ ���� 1 ͣ�� 0 ���� 2   
                    item.Specs = this.Reader[17].ToString(); //���
                    item.ExecDept = this.Reader[18].ToString();//ִ�п���
                    item.MachineNO = this.Reader[19].ToString(); //�豸��� �� | ���� 
                    item.CheckBody = this.Reader[20].ToString(); //Ĭ�ϼ�鲿λ��걾
                    item.OperationInfo.ID = this.Reader[21].ToString(); // �������� 
                    item.OperationType.ID = this.Reader[22].ToString(); // ��������
                    item.OperationScale.ID = this.Reader[23].ToString(); //������ģ 
                    item.IsCompareToMaterial = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[24].ToString());//�Ƿ���������Ŀ��֮����(1�У�0û��) 
                    item.Memo = this.Reader[25].ToString(); //��ע  
                    item.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString()); //��ͯ��
                    item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString()); //�����
                    item.SpecialFlag = this.Reader[28].ToString(); //ʡ����
                    item.SpecialFlag1 = this.Reader[29].ToString(); //������
                    item.SpecialFlag2 = this.Reader[30].ToString(); //�Է���Ŀ
                    item.SpecialFlag3 = this.Reader[31].ToString();// ������
                    item.SpecialFlag4 = this.Reader[32].ToString();// ����		
                    item.DiseaseType.ID = this.Reader[35].ToString(); //��������
                    item.SpecialDept.ID = this.Reader[36].ToString();  //ר������
                    item.MedicalRecord = this.Reader[37].ToString(); //  --��ʷ�����
                    item.CheckRequest = this.Reader[38].ToString();//--���Ҫ��
                    item.Notice = this.Reader[39].ToString();//--  ע������  
                    item.IsConsent = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[40].ToString());
                    item.CheckApplyDept = this.Reader[41].ToString();//������뵥����
                    item.IsNeedBespeak = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[42].ToString());//�Ƿ���ҪԤԼ
                    item.ItemArea = this.Reader[43].ToString();//��Ŀ��Χ
                    item.ItemException = this.Reader[44].ToString();//��ĿԼ��

                    //��λ��ʶ(0,��ϸ; 1,����)[2007/01/01  xuweizhe]
                    item.UnitFlag = this.Reader.IsDBNull(45) ? "" : this.Reader.GetString(45);

                    item.SpecialDept.ID = this.Reader.IsDBNull(46) ? "" : this.Reader[46].ToString();//��ҩƷ�����ά���Ŀ���

                    lstUndrugzt.Add(item);
                }//ѭ������

                //�ر�Reader
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.Reader.Close();
                return -1;
            }
            return 1;
        }

        #endregion

        #endregion
    }
}
