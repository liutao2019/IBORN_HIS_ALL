using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;
using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.HISFC.BizLogic.HealthRecord
{
    

    /// <summary>
    /// (ICD10, ICD9, ����ICD, ICD9, ICD10����һ��ҵ��� Creator: zhangjunyi@FS.com  2005/05/30
    /// </summary>
    public class ICD : FS.FrameWork.Management.Database
    {

        public ICD()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ICD9 ICD10 ICDOperation ��������
        /// <summary>
        /// ����˵��: ����ICD������ICD�� Creator: zhangjunyi@FS.com  2005/06/01
        /// </summary>
        /// <param name="obj">ICD����</param>
        /// <param name="type">��ѯ���� </param>
        /// <returns> ���󷵻� -1 �ɹ�����  �������Ӱ������� </returns>
        public int Insert(FS.HISFC.Models.HealthRecord.ICD obj, FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes type)
        {
            //�����ַ��������洢SQL���
            string strSql = "";
            //Ψһ����
            string sSequenceNo = "";
            //���SQL���
            switch (type)
            {
                case ICDTypes.ICD10:
                    //��ò���ICD10��SQL���
                    if (this.Sql.GetSql("Case.ICDDML.Insert.ICD10", ref strSql) == -1)
                    {
                        this.Err = "���SQL������!û���ҵ�����Case.ICDDML.Insert.ICD10";
                        return -1;
                    }
                    //�������
                    obj.KeyCode = this.GetSequence("Case.ICDDML.GetSeq.ICD10");
                    if (obj.KeyCode == "" || sSequenceNo == null)
                    {
                        this.Err = "���ICD10������ˮ�ų���!";
                        return -1;
                    }
                    break;
                case ICDTypes.ICD9:
                    //��ò��� ICD9��SQL���
                    if (this.Sql.GetSql("Case.ICDDML.Insert.ICD9", ref strSql) == -1)
                    {
                        this.Err = "���SQL������!û���ҵ�����Case.ICDDML.Insert.ICD9";
                        return -1;
                    }
                    //�������
                    obj.KeyCode = this.GetSequence("Case.ICDDML.GetSeq.ICD9");
                    if (obj.KeyCode == "" || sSequenceNo == null)
                    {
                        this.Err = "���ICD9������ˮ�ų���!";
                        return -1;
                    }
                    break;
                case ICDTypes.ICDOperation:
                    //��ò���������SQL���
                    if (this.Sql.GetSql("Case.ICDDML.Insert.ICDOperation", ref strSql) == -1)
                    {
                        this.Err = "���SQL������!û���ҵ�����Case.ICDDML.Insert.ICDOperation";
                        return -1;
                    }
                    //�������
                    obj.KeyCode = this.GetSequence("Case.ICDDML.GetSeq.ICDOperation");
                    if (obj.KeyCode == "" || sSequenceNo == null)
                    {
                        this.Err = "���ICDOperation������ˮ�ų���!";
                        return -1;
                    }
                    break;
            }   
            return this.ExecNoQuery(strSql, GetICDParam(obj));
        }

        /// <summary>
        /// ����ICD�� 
        /// </summary>
        /// <param name="orgICD">���ǰʵ��</param>
        /// <param name="newICD">�����ʵ��</param>
        /// <param name="type">�������ö��</param>
        /// <returns>-1 ��ʾ��������δ����Ĵ��� >1 ��ʾ�ɹ�</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        public int Update(FS.HISFC.Models.HealthRecord.ICD orgICD, FS.HISFC.Models.HealthRecord.ICD newICD, ICDTypes type)
        {
            try
            {
                //�����ַ����� ���������SQL���
                string strUpdateSql = "";
                //���� ����������������»�������Ӱ�������
                switch (type)
                {
                    case ICDTypes.ICD10:
                        //��ø���ICD10��SQL���
                        if (this.Sql.GetSql("Case.ICDDML.Update.ICD10", ref strUpdateSql) == -1)
                        {
                            this.Err = "���SQL������!����:Case.ICDDML.Update.ICD10";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ø���ICD9��SQL���
                        if (this.Sql.GetSql("Case.ICDDML.Update.ICD9", ref strUpdateSql) == -1)
                        {
                            this.Err = "���SQL������!";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ø���������SQL���
                        if (this.Sql.GetSql("Case.ICDDML.Update.ICDOperation", ref strUpdateSql) == -1)
                        {
                            this.Err = "���SQL������!";
                            return -1;
                        }
                        break;
                } 
                //ִ�и��²��� 
                int iReturn = 0;
                iReturn = this.ExecNoQuery(strUpdateSql, GetICDParam(newICD)); //ִ�в���
                if (iReturn == 0) //û�и��¼�¼,ǰ̨�����Ƿ񲢷�
                {
                    return 0;
                }
                else if (iReturn == -1)//���ݿ����ʧ��
                {
                    return -1;
                }
                //ִ�в�������¼
                iReturn = InsertShift(orgICD, newICD, type);
                if (iReturn == -1)//�������ʧ��
                {
                    return -1;
                }
                else//��������ɹ�
                {
                    return iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //�����쳣
                return -1;
            }
        }

        /// <summary>
        /// �����Ӧ��ѯ����ICD��Ϣ  (�����Ƴ�������
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        /// <returns>ArrayList.Count >= 1 ��ȷ��÷���������ICD���� 
        ///			 ArrayList.Count == 0 û�з���������ICD���� 
        ///			 null  ����δ����Ĵ���    </returns>
        ///	Creator: zhangjunyi@FS.com  2005/06/01
        public ArrayList QueryNew(ICDTypes ICDType, QueryTypes queryType, string deptCode)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //��ѯ����
            string strWhere = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strValidString = "";
            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                switch (queryType)
                {
                    case QueryTypes.All:
                        //���з�������Ч��
                        strValidString = "%";
                        break;
                    case QueryTypes.Valid:
                        //��Ч��
                        strValidString = "1";
                        break;
                    case QueryTypes.Cancel:
                        //������ 
                        strValidString = "0";
                        break;
                }

                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10";
                            return null;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ������:Case.ICDDML.Query.ICD9";
                            return null;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICDOperation";
                            return null;
                        }
                        break;
                }
                //��ò�ѯ����
                if (this.Sql.GetSql("Case.ICDDML.Query.ValidNew", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.Valid";
                    return null;
                }
                strQuerySql += strWhere;
                try
                {
                    //��ʽ��SQL���
                    strQuerySql = string.Format(strQuerySql, strValidString, deptCode);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //ִ�в�ѯ����
                this.ExecQuery(strQuerySql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arryList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arryList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }
        
        /// <summary>
        /// �����Ӧ��ѯ����ICD��Ϣ 
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        /// <returns>ArrayList.Count >= 1 ��ȷ��÷���������ICD���� 
        ///			 ArrayList.Count == 0 û�з���������ICD���� 
        ///			 null  ����δ����Ĵ���    </returns>
        ///	Creator: zhangjunyi@FS.com  2005/06/01
        public ArrayList Query(ICDTypes ICDType, QueryTypes queryType)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //��ѯ����
            string strWhere = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strValidString = "";
            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();
            try
            {
                switch (queryType)
                {
                    case QueryTypes.All:
                        //���з�������Ч��
                        strValidString = "%";
                        break;
                    case QueryTypes.Valid:
                        //��Ч��
                        strValidString = "1";
                        break;
                    case QueryTypes.Cancel:
                        //������ 
                        strValidString = "0";
                        break;
                }

                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10";
                            return null;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ������:Case.ICDDML.Query.ICD9";
                            return null;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICDOperation";
                            return null;
                        }
                        break;
                }
                //��ò�ѯ����
                if (this.Sql.GetSql("Case.ICDDML.Query.Valid", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.Valid";
                    return null;
                }
                strQuerySql += strWhere;

                /////2014-08-22
                //arrayList = QueryICDCompare(strQuerySql);

                try
                {
                    //��ʽ��SQL���
                    strQuerySql = string.Format(strQuerySql, strValidString);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //ִ�в�ѯ����
                this.ExecQuery(strQuerySql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arryList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arryList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }
        /// <summary>
        ///  �����Ӧ��ѯ����ICD��Ϣ  �õ�ʱ�� ����Ҫ�жϷ���ֵ��
        ///   �����0 ��ȡDataSet �����-1 ˵���д���  Ӧ����ʾ������Ϣ
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        /// <param name="ds">�������������ݼ�</param>
        /// <returns>����δ֪���� ���� -1 �ɹ����� 1</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        public int Query(ICDTypes ICDType, QueryTypes queryType, ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //�����ַ�����, �洢��ѯ����
            string strWhere = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strValidString = "";
            try
            {
                switch (queryType)
                {
                    case QueryTypes.All:
                        //���з�������Ч��
                        strValidString = "%";
                        break;
                    case QueryTypes.Valid:
                        //��Ч��
                        strValidString = "1";
                        break;
                    case QueryTypes.Cancel:
                        //������ 
                        strValidString = "0";
                        break;
                }

                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base.ds", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10.Base.ds";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base.ds", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD9.Base.ds";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base.ds", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.ICDDML.Query.ICDOperation.Base.ds";
                            return -1;
                        }
                        break;
                }

                //��ò�ѯ����
                if (this.Sql.GetSql("Case.ICDDML.Query.Valid", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.Valid";
                    return -1;
                }

                strQuerySql += strWhere;

                try
                {
                    //�齨��ѯ��� 
                    strQuerySql = string.Format(strQuerySql, strValidString);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
                //ִ�в�ѯ����
                return this.ExecQuery(strQuerySql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //��ȡ������Ϣ
                return -1; //����δ����Ĵ���
            }
        }

        /// <summary>
        ///  �����Ӧ��ѯ����ICD��Ϣ  �õ�ʱ�� ����Ҫ�жϷ���ֵ��
        ///   �����0 ��ȡDataSet �����-1 ˵���д���  Ӧ����ʾ������Ϣ
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        /// <param name="ds">�������������ݼ�</param>
        /// <returns>����δ֪���� ���� -1 �ɹ����� 1</returns>
        /// Creator: chengym 2011-9-28
        public int QueryCase(ICDTypes ICDType, QueryTypes queryType, ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //�����ַ�����, �洢��ѯ����
            string strWhere = "";
            //�����ַ����� ,�洢WHERE ����SQL���
            string strValidString = "";
            try
            {
                switch (queryType)
                {
                    case QueryTypes.All:
                        //���з�������Ч��
                        strValidString = "%";
                        break;
                    case QueryTypes.Valid:
                        //��Ч��
                        strValidString = "1";
                        break;
                    case QueryTypes.Cancel:
                        //������ 
                        strValidString = "0";
                        break;
                }

                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base.Ds.Case", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD10.Base.Ds.Case";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base.ds", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.ICD9.Base.ds";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base.ds", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.ICDDML.Query.ICDOperation.Base.ds";
                            return -1;
                        }
                        break;
                }

                //��ò�ѯ����
                if (this.Sql.GetSql("Case.ICDDML.Query.Valid", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��,����:Case.ICDDML.Query.Valid";
                    return -1;
                }

                strQuerySql += strWhere;

                try
                {
                    //�齨��ѯ��� 
                    strQuerySql = string.Format(strQuerySql, strValidString);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
                //ִ�в�ѯ����
                return this.ExecQuery(strQuerySql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //��ȡ������Ϣ
                return -1; //����δ����Ĵ���
            }
        }
        /// <summary>
        /// ��Reader  �ж�ȡ����
        /// </summary>
        /// <returns> ����δ����Ĵ��󷵻� null �м�¼list.Count >1  û�м�¼ list.Count =0</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        private ArrayList ICDReaderInfo()
        {
            //���� ��̬���飬 �����洢��������Ϣ
            ArrayList list = new ArrayList();
            try
            {
                //����ʵ��
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                while (this.Reader.Read())
                {
                    icd = new FS.HISFC.Models.HealthRecord.ICD();

                    icd.KeyCode = Reader[0].ToString(); //ICD����
                    icd.ID = Reader[1].ToString();//ICD����
                    icd.SICode = Reader[2].ToString(); //ҽ�����Ĵ���
                    icd.UserCode = Reader[3].ToString(); //ͳ�ƴ���
                    icd.SpellCode = Reader[4].ToString();    //ƴ����
                    icd.WBCode = Reader[5].ToString();//�����
                    icd.Name = Reader[6].ToString(); //ICD����
                    icd.User01 = Reader[7].ToString(); //��������1
                    icd.User02 = Reader[8].ToString(); //��������2
                    icd.DeadReason = Reader[9].ToString(); //����ԭ��
                    icd.DiseaseCode = Reader[10].ToString(); //�����������
                    icd.StandardDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[11].ToString());//��׼סԺ��
                    icd.Is30Illness = Reader[12].ToString();//�Ƿ�30�ּ���
                    icd.IsInfection = Reader[13].ToString();//�Ƿ�Ⱦ��
                    icd.IsTumour = Reader[14].ToString();//�Ƿ�������� 
                    icd.InpGrade = Reader[15].ToString();//סԺ�ȼ�
                    icd.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[16].ToString());//�Ƿ���Ч
                    icd.SeqNo = Reader[17].ToString();//���
                    icd.OperInfo.ID = Reader[18].ToString();
                    icd.OperInfo.Name = Reader[19].ToString();
                    if (!Reader.IsDBNull(20))
                    {
                        icd.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[20].ToString());
                    }
                    icd.User01 = Reader[21].ToString(); //�������
                    icd.SexType.ID = Reader[22].ToString(); //�����Ա�
                    icd.TraditionalDiag = FS.FrameWork.Function.NConvert.ToBoolean(Reader[23].ToString());
                    //��ɽ��ׯ�����ҽԺmodels���ܸ��£�������������24���� 2012-11-14
                    if (this.Reader.FieldCount > 24)
                    {
                        icd.Category.ID = this.Reader[24].ToString();
                    }

                    list.Add(icd); //�������
                    icd = null;
                }
                this.Reader.Close(); //�ر�reade
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                {
                    this.Reader.Close();//û�йر����ȹر�
                }

                return null; //���ִ��󷵻�null 
            }

            return list;
        }
        /// <summary>
        /// ��Reader  �ж�ȡ����
        /// </summary>
        /// <returns> ����δ����Ĵ��󷵻� null �м�¼list.Count >1  û�м�¼ list.Count =0</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        private ArrayList ICDReaderInfoNotCategory()
        {
            //���� ��̬���飬 �����洢��������Ϣ
            ArrayList list = new ArrayList();
            try
            {
                //����ʵ��
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                while (this.Reader.Read())
                {
                    icd = new FS.HISFC.Models.HealthRecord.ICD();

                    icd.KeyCode = Reader[0].ToString(); //ICD����
                    icd.ID = Reader[1].ToString();//ICD����
                    icd.SICode = Reader[2].ToString(); //ҽ�����Ĵ���
                    icd.UserCode = Reader[3].ToString(); //ͳ�ƴ���
                    icd.SpellCode = Reader[4].ToString();    //ƴ����
                    icd.WBCode = Reader[5].ToString();//�����
                    icd.Name = Reader[6].ToString(); //ICD����
                    icd.User01 = Reader[7].ToString(); //��������1
                    icd.User02 = Reader[8].ToString(); //��������2
                    icd.DeadReason = Reader[9].ToString(); //����ԭ��
                    icd.DiseaseCode = Reader[10].ToString(); //�����������
                    icd.StandardDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[11].ToString());//��׼סԺ��
                    icd.Is30Illness = Reader[12].ToString();//�Ƿ�30�ּ���
                    icd.IsInfection = Reader[13].ToString();//�Ƿ�Ⱦ��
                    icd.IsTumour = Reader[14].ToString();//�Ƿ�������� 
                    icd.InpGrade = Reader[15].ToString();//סԺ�ȼ�
                    icd.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[16].ToString());//�Ƿ���Ч
                    icd.SeqNo = Reader[17].ToString();//���
                    icd.OperInfo.ID = Reader[18].ToString();
                    icd.OperInfo.Name = Reader[19].ToString();
                    if (!Reader.IsDBNull(20))
                    {
                        icd.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[20].ToString());
                    }
                    icd.User01 = Reader[21].ToString(); //�������
                    icd.SexType.ID = Reader[22].ToString(); //�����Ա�
                    icd.TraditionalDiag = FS.FrameWork.Function.NConvert.ToBoolean(Reader[23].ToString());
                    list.Add(icd); //�������
                    icd = null;
                }
                this.Reader.Close(); //�ر�reade
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                {
                    this.Reader.Close();//û�йر����ȹر�
                }

                return null; //���ִ��󷵻�null 
            }

            return list;
        }
        /// <summary>
        /// �ж�¼���ICD�����Ƿ����,������ڷ��ط���������ArrayList,���򷵻�null����
        /// </summary>
        /// <param name="ICDCode">¼���ICD����</param>
        /// <param name="type">ICD���</param>
        /// <param name="isValid">�Ƿ���Ч true ��Ч false ����</param>
        /// <returns>Arralist.Count >= 1 ʵ������ICD ���ϲ�ѯ������ICDʵ��ArralyList
        ///          ArrayList.Count = 0 û�з��������ļ�¼
        ///          null ���ݿ����ʧ�� </returns>
        ///          Creator: zhangjunyi@FS.com  2005/06/01
        public ArrayList IsExistAndReturn(string ICDCode, ICDTypes type, bool isValid)
        {
            //��Ҫ��������Ա������������ʱ�ж�����ICDCode�Ƿ����,
            //�����������ôĬ������״̬,��������ICDCode����,��Ϊ�޸�״̬
            //�����ַ��������洢SQL���
            string strSql = "";
            //�����ַ��������洢��ѯ����
            string strWhere = "";
            //��һ��̬���� ���洢���������ļ�¼
            ArrayList arrList = new ArrayList();
            try
            {
                //����type��ö�Ӧ��SQL���
                switch (type)
                {
                    case ICDTypes.ICD10:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD10";
                            return null;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD9";
                            return null;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������,����:Case.ICDDML.Query.ICDOperation";
                            return null;
                        }
                        break;
                }

                if (this.Sql.GetSql("Case.ICDDML.Query.IsExistAndReturn", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL������,����:Case.ICDDML.Query.IsExistAndReturn";
                    return null;
                }

                strSql += strWhere;

                try
                {
                    strSql = string.Format(strSql, ICDCode, NConvert.ToInt32(isValid));
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //ִ��SQL���
                this.ExecQuery(strSql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arrList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arrList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //��ȡ������Ϣ

                if (!Reader.IsClosed)  //���Readerû�йر�
                {
                    this.Reader.Close(); //�ر�Reader
                }

                return null; // ���󷵻�null
            }

            return arrList;
        }
        /// <summary>
        /// �ж�¼���ICD�����Ƿ����,������ڷ��ط���������ʵ��,���򷵻�null����
        /// </summary>
        /// <param name="ICDCode">¼���ICD����</param>
        /// <param name="type">ICD���</param>
        /// <param name="isValid">�Ƿ���Ч true ��Ч false ����</param>
        /// <returns>null ���ݿ����ʧ�� </returns>
        ///          Creator: zhangjunyi@FS.com  2005/06/01
        public FS.HISFC.Models.HealthRecord.ICD IsExistAndReturnOne(string ICDCode, ICDTypes type, bool isValid)
        {
            //��Ҫ��������Ա������������ʱ�ж�����ICDCode�Ƿ����,
            //�����������ôĬ������״̬,��������ICDCode����,��Ϊ�޸�״̬
            //�����ַ��������洢SQL���
            string strSql = "";
            //�����ַ��������洢��ѯ����
            string strWhere = "";
            //��һ��̬���� ���洢���������ļ�¼
            ArrayList arrList = new ArrayList();
            FS.HISFC.Models.HealthRecord.ICD info = null;
            try
            {
                //����type��ö�Ӧ��SQL���
                switch (type)
                {
                    case ICDTypes.ICD10:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD10";
                            return null;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICD9.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD9";
                            return null;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡSQL���
                        if (this.Sql.GetSql("Case.ICDDML.Query.ICDOperation.Base", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������,����:Case.ICDDML.Query.ICDOperation";
                            return null;
                        }
                        break;
                }

                if (this.Sql.GetSql("Case.ICDDML.Query.IsExistAndReturn", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL������,����:Case.ICDDML.Query.IsExistAndReturn";
                    return null;
                }

                strSql += strWhere;

                try
                {
                    strSql = string.Format(strSql, ICDCode, NConvert.ToInt32(isValid));
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                }
                //ִ��SQL���
                this.ExecQuery(strSql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arrList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arrList = ICDReaderInfo();
                }
                if (arrList == null)
                {
                    return null;
                }
                if (arrList.Count > 0)
                {
                    info = (FS.HISFC.Models.HealthRecord.ICD)arrList[0];
                }
                return info;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //��ȡ������Ϣ

                if (!Reader.IsClosed)  //���Readerû�йر�
                {
                    this.Reader.Close(); //�ر�Reader
                }

                return null; // ���󷵻�null
            }
        }
        /// <summary>
        /// ��ȡֻ���������Ի�Ů�Ե���� sexCodeΪ"M" ��ѯ�������, "F"��ѯŮ�����
        /// </summary>
        /// <param name="sexCode"></param>
        /// <returns></returns>
        public ArrayList QueryDiagnoseBySex(string sexCode)
        {
            //�����ַ��������洢SQL���
            string strSql = "";
            //�����ַ��������洢��ѯ����
            string strWhere = "";
            //��һ��̬���� ���洢���������ļ�¼
            ArrayList arrList = new ArrayList();
            try
            {
                if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD10";
                    return null;
                }
                if (this.Sql.GetSql("Case.ICDDML.Query.GetDiagnoseBySex", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL������,����:Case.ICDDML.Query.GetDiagnoseBySex";
                    return null;
                }
                strSql += strWhere;
                try
                {
                    strSql = string.Format(strSql, sexCode);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    if (!Reader.IsClosed)  //���Readerû�йر�
                    {
                        this.Reader.Close(); //�ر�Reader
                    }
                    return null;
                }
                //ִ��SQL���
                this.ExecQuery(strSql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arrList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arrList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)  //���Readerû�йر�
                {
                    this.Reader.Close(); //�ر�Reader
                }

                return null;
            }
            return arrList;
        }

        #region ˽�к���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgICD"></param>
        /// <param name="newICD"></param>
        /// <returns></returns>
        private string[] GetICDParam(FS.HISFC.Models.HealthRecord.ICD newICD)
        {
            string[] str = new string[]{
                                        newICD.KeyCode, //0
                                        newICD.ID,
                                        newICD.SICode, //1
                                        newICD.UserCode, //2
                                        newICD.SpellCode,//3
                                        newICD.WBCode, //4
                                        newICD.Name, //5
                                        newICD.User01, //6
                                        newICD.User02, //7
                                        newICD.DeadReason, //8
                                        newICD.DiseaseCode,//9
                                        newICD.StandardDays.ToString(), //10
                                        newICD.Is30Illness, //11
                                        newICD.IsInfection,//12
                                        newICD.IsTumour, //13
                                        newICD.InpGrade,  //14
                                        FS.FrameWork.Function.NConvert.ToInt32(newICD.IsValid).ToString(),//15
                                        newICD.SeqNo, //16
                                        newICD.OperInfo.ID, //17
                                        newICD.User01, //18
                                        newICD.SexType.ID.ToString(),//19
                                         FS.FrameWork.Function.NConvert.ToInt32(newICD.TraditionalDiag).ToString()
            };
            return str;
        }
        #endregion 
        #endregion

        #region ���ICD���յĺ���.
        /// <summary>
        /// �����Ѷ�����Ϣ  
        /// </summary>
        /// <param name="compare">obj ICD����ʵ��</param>
        /// <returns>���ִ��� ���� -1 �ɹ� ���� 1</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        public int InsertCompare(FS.HISFC.Models.HealthRecord.ICDCompare compare)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //���SQL���
                if (this.Sql.GetSql("Case.ICDDML.InsertCompare", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return -1;
                }
                //��Ч��
                string isValid = "";
                if (compare.IsValid)
                {
                    isValid = "1";
                }
                else
                {
                    isValid = "0";
                }
                //try
                //{
                //    //��ʽ��SQL��� 
                //    strSql = string.Format(strSql, compare.ICD9.ID, compare.ICD9.Name, compare.ICD10.ID,
                //        compare.ICD10.Name, isValid, compare.OperInfo.ID, compare.ICD9.SpellCode,
                //        compare.ICD9.UserCode, compare.ICD9.KeyCode);
                //}
                //catch (Exception ex)
                //{
                //    this.Err = "SQL��丳ֵ����!" + ex.Message;
                //    return -1;
                //}
                string[] str = new string[] { compare.ICD9.ID, 
                                            compare.ICD9.Name, 
                                            compare.ICD10.ID,
                                            compare.ICD10.Name, 
                                            isValid, 
                                            compare.OperInfo.ID, 
                                            compare.ICD9.SpellCode,
                                            compare.ICD9.UserCode, 
                                            compare.ICD9.KeyCode
                };
                //ִ�в������
                return this.ExecNoQuery(strSql, str);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //�������
                return -1; //���ش���
            }
        }
        /// <summary>
        /// ���δ���յ�ICD9��Ϣ
        /// </summary>
        /// <param name="type">��ѯ����</param>
        /// <returns>���󷵻� NULL ��ȷ����arrayList</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        public ArrayList QueryNoComparedICD9(QueryTypes type)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //�����ַ����� �洢 Where ����
                string strValidString = "";
                //���嶯̬���� ���洢�������������ݼ�
                ArrayList arrayList = new ArrayList();
                //��ȡSQL���
                if (this.Sql.GetSql("Case.ICDDML.QueryNoComparedICD9", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return null;
                }
                //����type��SQL��丳ֵ
                switch (type)
                {
                    case QueryTypes.All:  //��ѯ���е�
                        strValidString = "%";
                        break;
                    case QueryTypes.Cancel: //��ѯ���ϵ�
                        strValidString = "0";
                        break;
                    case QueryTypes.Valid: // ��ѯ��Ч��
                        strValidString = "1";
                        break;
                }
                try
                {
                    strSql = string.Format(strSql, strValidString);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����! " + ex.Message;
                    return null;
                }
                //ִ�в�ѯ����
                this.ExecQuery(strSql);
                //����Readerװ��ʵ��(HISFC.Object.Case.ICD)
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arrayList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arrayList = ICDReaderInfo();
                }
                //����ArrayList
                return arrayList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;//�������
                return null;  //����null
            }

        }
        /// <summary>
        /// ��Reader �ж�ȡ��Ϣ �浽 ������ 
        /// </summary>
        /// <returns>���󷵻� null  �ɹ����� ����</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        private ArrayList QueryICDCompare(string strSql)
        {
            try
            {
                this.ExecQuery(strSql);
                //���嶯̬���� ���洢��ѯ���������ݼ�
                ArrayList arryCompare = new ArrayList();
                //������� ���洢����
                FS.HISFC.Models.HealthRecord.ICDCompare compare = null;
                while (this.Reader.Read())
                {
                    compare = new FS.HISFC.Models.HealthRecord.ICDCompare();

                    compare.ICD9.ID = Reader[0].ToString();		//ICD9����
                    compare.ICD9.Name = Reader[1].ToString();		//ICD9����
                    compare.ICD10.ID = Reader[2].ToString();			//ICD10����
                    compare.ICD10.Name = Reader[3].ToString();		//ICD10����
                    compare.OperInfo.ID = Reader[4].ToString();		//¼�����Ա����
                    compare.OperInfo.Name = Reader[5].ToString();	//¼�����Ա����
                    compare.OperInfo.OperTime = NConvert.ToDateTime(Reader[6]);
                    compare.IsValid = NConvert.ToBoolean(Reader[7].ToString()); //��Ч��
                    compare.ICD9.SpellCode = Reader[8].ToString(); //ƴ��
                    compare.ICD9.UserCode = Reader[9].ToString(); //�Զ���
                    compare.ICD9.KeyCode = Reader[10].ToString();
                    arryCompare.Add(compare); //��ҵ���̬������
                    compare = null;           //�ͷ���Դ
                }
                this.Reader.Close(); //�ر�Reader
                return arryCompare;
            }
            catch (Exception ex)
            {
                //���ִ���
                this.Err = ex.Message;
                if (!this.Reader.IsClosed) //���û���ͷ� Reader
                {
                    this.Reader.Close(); //�ر�Reader
                }
                return null;
            }
        }

        /// <summary>
        /// ���� ���δ���յ�ICD9��Ϣ
        /// </summary>
        /// <param name="type">��ѯ����</param>
        /// <param name="ds">���ݼ�</param>
        /// <returns>�����г���δ����Ĵ��� ���� -1 ��ȷ���� 1 �����ط������������ݼ�</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        public int QueryNoComparedICD9(QueryTypes type, ref DataSet ds)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //�����ַ����� �洢 Where ����
                string strValidString = "";
                //���嶯̬���� ���洢�������������ݼ�
                ArrayList arrayList = new ArrayList();
                //��ȡSQL���
                if (this.Sql.GetSql("Case.ICDDML.QueryNoComparedICD9", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return -1;
                }
                //����type��SQL��丳ֵ
                switch (type)
                {
                    case QueryTypes.All:  //��ѯ���е�
                        strValidString = "%";
                        break;
                    case QueryTypes.Cancel: //��ѯ���ϵ�
                        strValidString = "0";
                        break;
                    case QueryTypes.Valid: // ��ѯ��Ч��
                        strValidString = "1";
                        break;
                }
                try
                {
                    //��ʽ��SQL
                    strSql = string.Format(strSql, strValidString);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }
                //ִ�в�ѯ����
                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;//�������
                return -1;  //����null
            }
        }
        /// <summary>
        /// ����Ѷ��յ�ICD��Ϣ
        /// </summary>
        /// <returns>����ICDCompareʵ���ArrayList 
        ///null ���ݿ����
        /// ArrayList.Count = 0 û�з��ϼ�¼������
        /// ArrayLIst.Count >= 1</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        public ArrayList QueryComparedICD()
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //���嶯̬���� ���洢�������������ݼ�
                ArrayList arrayList = new ArrayList();

                //��ò�ѯSQL���
                if (this.Sql.GetSql("Case.ICDDML.QueryComparedICD", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return null;
                }
                //ִ�в������
                
                //��ȡReader ��ʵ�帳ֵ(HISFC.Object.Case.ICDCompare)
                arrayList = QueryICDCompare(strSql);
                //����ArrayList
                return arrayList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //�������
                return null;  //����null 
            }
        }
        /// <summary>
        /// ����Ѷ��յ�ICD��Ϣ ����  DataSet  
        /// </summary>
        /// <param name="ds">�������������ݼ�</param>
        /// <returns> ��������δ���ֵĴ��󷵻�-1 û�д��󷵻� 1</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        public int QueryComparedICD(ref DataSet ds)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //���嶯̬���� ���洢�������������ݼ�
                ArrayList arrayList = new ArrayList();

                //��ò�ѯSQL���
                if (this.Sql.GetSql("Case.ICDDML.QueryComparedICD", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return -1;
                }
                //ִ�в������
                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //�������
                return -1;  //����null 
            }
        }
        /// <summary>
        /// ɾ���Ѷ�����Ϣ  
        /// </summary>
        /// <param name="ICDCode">�Ѷ��յ�ICD9����(���ձ�ICD9����������)</param>
        /// <returns>  -1 ���ݿ��������
        ///            0	û���ҵ�ɾ������(����Where��������,���߲���)
        ///		       1	ɾ���ɹ�  </returns>
        ///		       Creator: zhangjunyi@FS.com  2005/05/30
        public int DeleteCompared(string ICDCode)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = "";
                //���ɾ��SQL���
                if (this.Sql.GetSql("Case.ICDDML.DeleteCompared", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��";
                    return -1;
                }
                try
                {
                    //��SQL��丳����
                    strSql = string.Format(strSql, ICDCode);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    return -1;
                }
                //ִ��ɾ������
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message; //�������
                return -1;
            }
        }

        #endregion

        #region ICD�����
        /// <summary>
        /// ��������Ϣ 
        /// </summary>
        /// <param name="orgICD">���ǰ״̬</param>
        /// <param name="newICD">�����״̬</param>
        /// <param name="type">�������ö��</param>
        /// <returns>-1 ����  >=1�ɹ�</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        private int InsertShift(FS.HISFC.Models.HealthRecord.ICD orgICD, FS.HISFC.Models.HealthRecord.ICD newICD, ICDTypes type)
        {
            try
            {
                //�����ַ��������洢SQL���
                string strSql = "";
                //����type���SQL���
                switch (type)
                {
                    case ICDTypes.ICD10:
                        //��ȡSQL ���
                        if (this.Sql.GetSql("Case.ICDDML.InsertShift.ICD10", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡSQL ���
                        if (this.Sql.GetSql("Case.ICDDML.InsertShift.ICD9", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡSQL ���
                        if (this.Sql.GetSql("Case.ICDDML.InsertShift.ICDOperation", ref strSql) == -1)
                        {
                            this.Err = "��ȡSQL������";
                            return -1;
                        }
                        break;
                    //��ֵ
                } 
                //ִ�в������
                return this.ExecNoQuery(strSql, GetICDLogParam(orgICD, newICD));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //����δ�����쳣
                return -1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgICD"></param>
        /// <param name="newICD"></param>
        /// <returns></returns>
        private string[] GetICDLogParam(FS.HISFC.Models.HealthRecord.ICD orgICD, FS.HISFC.Models.HealthRecord.ICD newICD)
        {
            string[] str = new string[]{
                                        orgICD.ID, 
                        orgICD.SICode, 
                        orgICD.UserCode, 
                        orgICD.SpellCode, 
                        orgICD.WBCode,
                        orgICD.Name, 
                        orgICD.User01, 
                        orgICD.User02, 
                        orgICD.DeadReason, 
                        orgICD.DiseaseCode, 
                        orgICD.StandardDays.ToString(),
                        orgICD.Is30Illness, 
                        orgICD.IsInfection, 
                        orgICD.IsTumour,
                        orgICD.InpGrade, 
                        FS.FrameWork.Function.NConvert.ToInt32(orgICD.IsValid).ToString(), 
                        newICD.SICode, 
                        newICD.UserCode, 
                        newICD.SpellCode, 
                        newICD.WBCode,
                        newICD.Name, 
                        newICD.User01, 
                        newICD.User02, 
                        newICD.DeadReason, 
                        newICD.DiseaseCode, 
                        newICD.StandardDays.ToString(),
                        newICD.Is30Illness, 
                        newICD.IsInfection, 
                        newICD.IsTumour,
                        newICD.InpGrade, 
                        FS.FrameWork.Function.NConvert.ToInt32(newICD.IsValid).ToString(), 
                this.Operator.ID
            };
            return str;
        }
        #endregion 

        #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}

        /// <summary>
        /// ��ѯ���пƳ������
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryDeptDiag(string deptID)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";

            //���嶯̬���� ,�洢��ѯ������Ϣ
            ArrayList arryList = new ArrayList();

            if (this.Sql.GetSql("Case.ICD.Query.DpetICD.1", ref strQuerySql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��,����:Case.ICD.Query.DpetICD.1";
                return null;
            }

            try
            {
                //��ʽ��SQL���
                strQuerySql = string.Format(strQuerySql, deptID);
            }
            catch (Exception ex)
            {
                this.Err = "SQL��丳ֵ����!" + ex.Message;
            }
            //ִ�в�ѯ����
            this.ExecQuery(strQuerySql);

            try
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arryList = ICDReaderInfoNotCategory();
                }
                else
                {
                    //��ȡ����
                    arryList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }
            return arryList;
        }

        #endregion

        #region ��Թ㶫�ĸı�
        /// <summary>
        /// ɾ���м��
        /// </summary>
        /// <returns></returns>
        public int DeleteTempICD()
        {
            try
            {
                string strSql = "delete temp_icd10";
                //���ɾ��SQL���
                //if (this.Sql.GetSql("Case.ICDDML.DeleteTempICD", ref strSql) == -1)
                //{
                //    this.Err = "��ȡSQL���ʧ��";
                //    return -1;
                //}
              
                //ִ��ɾ������
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message; //�������
                return -1;
            }
        }
        /// <summary>
        /// �����м��
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertTempICD(FS.HISFC.Models.HealthRecord.ICD info)
        {
            try
            {
                //�����ַ����� ���洢SQL���
                string strSql = @" insert into temp_icd10
                                     (
                                      fid,
                                     icd_code,
                                     icd_name ,
                                     stat_code
                                     )
                                     values
                                     (
                                     '{0}',
                                     '{1}',
                                     '{2}',
                                     '{3}'
                                     )";
               
                string[] str = new string[] {info.ID, 
                                            info.Name, 
                                            info.Memo,
                                            info.DiseaseCode
                };
                //ִ�в������
                return this.ExecNoQuery(strSql, str);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //�������
                return -1; //���ش���
            }
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ICDType"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.ICD> QueryTempICD(ICDTypes ICDType)
        {

            string strQuerySql = "";


            //���嶯̬���� ,�洢��ѯ������Ϣ
            List<FS.HISFC.Models.HealthRecord.ICD> arryList = new List<FS.HISFC.Models.HealthRecord.ICD>();
            try
            {
                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py  from temp_icd10 t where not exists 
                                         (select t.icd_code from met_com_icd10 m  where t.icd_code=m.icd_code)
                                         ";
                        break;
                    case ICDTypes.ICD9:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py  from temp_icd10 t where not exists 
                                         (select t.icd_code from met_com_icd9 m  where t.icd_code=m.icd_code)
                                         ";
                        break;
                    case ICDTypes.ICDOperation:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py   from temp_icd10 t where not exists 
                                         (select t.icd_code from met_com_icdoperation m  where t.icd_code=m.icd_code)";
                        break;
                }

                //try
                //{
                //    //��ʽ��SQL���
                //    strQuerySql = string.Format(strQuerySql);
                //}
                //catch (Exception ex)
                //{
                //    this.Err = "SQL��丳ֵ����!" + ex.Message;
                //}
                //ִ�в�ѯ����
                this.ExecQuery(strQuerySql);
                //��ȡ����
                try
                {
                    //����ʵ��
                    FS.HISFC.Models.HealthRecord.ICD icd = null;
                    while (this.Reader.Read())
                    {
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = Reader[0].ToString();//ICD����
                        icd.Name = Reader[1].ToString(); //��������
                        icd.DiseaseCode = Reader[2].ToString(); //�����������
                        icd.WBCode=this.Reader[3].ToString();
                        icd.SpellCode=this.Reader[4].ToString();
                        arryList.Add(icd); //�������
                        icd = null;
                    }
                    this.Reader.Close(); //�ر�reade
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;

                    if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                    {
                        this.Reader.Close();//û�йر����ȹر�
                    }
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ICDType"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.ICD> QueryTempICDAll(ICDTypes ICDType)
        {

            string strQuerySql = "";


            //���嶯̬���� ,�洢��ѯ������Ϣ
            List<FS.HISFC.Models.HealthRecord.ICD> arryList = new List<FS.HISFC.Models.HealthRecord.ICD>();
            try
            {
                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py  from temp_icd10 
                                         ";
                        break;
                    case ICDTypes.ICD9:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py  from temp_icd10 t 
                                         ";
                        break;
                    case ICDTypes.ICDOperation:
                        strQuerySql = @" select t.icd_code,t.icd_name,t.stat_code,substr(fun_get_querycode(t.icd_name,0),0,8) wb,
                                         substr(fun_get_querycode(t.icd_name,1),0,8) py   from temp_icd10 t";
                        break;
                }

                //try
                //{
                //    //��ʽ��SQL���
                //    strQuerySql = string.Format(strQuerySql);
                //}
                //catch (Exception ex)
                //{
                //    this.Err = "SQL��丳ֵ����!" + ex.Message;
                //}
                //ִ�в�ѯ����
                this.ExecQuery(strQuerySql);
                //��ȡ����
                try
                {
                    //����ʵ��
                    FS.HISFC.Models.HealthRecord.ICD icd = null;
                    while (this.Reader.Read())
                    {
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = Reader[0].ToString();//ICD����
                        icd.Name = Reader[1].ToString(); //��������
                        icd.DiseaseCode = Reader[2].ToString(); //�����������
                        icd.WBCode = this.Reader[3].ToString();
                        icd.SpellCode = this.Reader[4].ToString();
                        arryList.Add(icd); //�������
                        icd = null;
                    }
                    this.Reader.Close(); //�ر�reade
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;

                    if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                    {
                        this.Reader.Close();//û�йر����ȹر�
                    }
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }
        #endregion
        #region ��ϲ�ѯ�Ƚ��������ﴦ��һ�£���ʱ���ټ������ֶ�
        /// <summary>
        /// ɾ��DeleteICDDrgs��
        /// </summary>
        /// <returns></returns>
        public int DeleteICDDrgs(ICDTypes ICDType)
        {
            try
            {
                string strSql = string.Empty;
                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        strSql = @" delete from MET_COM_ICD10 ";
                        break;
                    case ICDTypes.ICDOperation:
                        strSql = @"  delete from MET_COM_ICDOPERATION ";
                        break;
                }
                //ִ��ɾ������
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message; //�������
                return -1;
            }
        }
        /// <summary>
        /// ������Ч������
        /// </summary>
        /// <param name="ICDType"></param>
        /// <returns></returns>
        public ArrayList QueryDrgs(ICDTypes ICDType)
        {
            string strSQL = string.Empty;

            if (ICDType == FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10)
            {
                strSQL = @"select t.sequence_no,t.icd_code,t.icd_name,t.spell_code,t.wb_code from met_com_icd10 t
where   valid_state=fun_get_valid()";
            }
            else if (ICDType == FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICDOperation)
            {
                strSQL = @"select o.sequence_no,o.icd_code,o.icd_name,o.spell_code,o.wb_code from met_com_icdoperation o
where   valid_state=fun_get_valid()";
            }
            ArrayList arryList = new ArrayList();
            try
            {
                //ִ�в�ѯ����
                this.ExecQuery(strSQL);
                //��ȡ����
                arryList = ICDReaderInfoDrgs();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!Reader.IsClosed) // ���û�йر�reader
                {
                    this.Reader.Close(); //�ر�reader
                }

                return null; // ���ִ��󷵻�null
            }

            return arryList;
        }

        /// <summary>
        /// ��Reader  �ж�ȡ����
        /// </summary>
        /// <returns> ����δ����Ĵ��󷵻� null �м�¼list.Count >1  û�м�¼ list.Count =0</returns>
        /// Creator: zhangjunyi@FS.com  2005/06/01
        private ArrayList ICDReaderInfoDrgs()
        {
            //���� ��̬���飬 �����洢��������Ϣ
            ArrayList list = new ArrayList();
            try
            {
                //����ʵ��
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                while (this.Reader.Read())
                {
                    icd = new FS.HISFC.Models.HealthRecord.ICD();

                    icd.KeyCode = Reader[0].ToString(); //ICD����
                    icd.ID = Reader[1].ToString();//ICD����
                    icd.Name = Reader[2].ToString(); //ICD����
                    icd.SpellCode = Reader[3].ToString();    //ƴ����
                    icd.WBCode = Reader[4].ToString();//�����
                    list.Add(icd); //�������
                    icd = null;
                }
                this.Reader.Close(); //�ر�reade
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                if (!this.Reader.IsClosed) // �ж��Ƿ�ر���Reader
                {
                    this.Reader.Close();//û�йر����ȹر�
                }

                return null; //���ִ��󷵻�null 
            }

            return list;
        }
        #endregion
        #region ����
        /// <summary>
        /// ��ȡֻ���������Ի�Ů�Ե���� sexCodeΪ"M" ��ѯ�������, "F"��ѯŮ�����
        /// </summary>
        /// <param name="sexCode"></param>
        /// <returns></returns>
        [Obsolete("����,��QueryDiagnoseBySex����",true)]
        public ArrayList GetDiagnoseBySex(string sexCode)
        {
            //�����ַ��������洢SQL���
            string strSql = "";
            //�����ַ��������洢��ѯ����
            string strWhere = "";
            //��һ��̬���� ���洢���������ļ�¼
            ArrayList arrList = new ArrayList();
            try
            {
                if (this.Sql.GetSql("Case.ICDDML.Query.ICD10.Base", ref strSql) == -1)
                {
                    this.Err = "��ȡSQL������, ����:Case.ICDDML.Query.ICD10";
                    return null;
                }
                if (this.Sql.GetSql("Case.ICDDML.Query.GetDiagnoseBySex", ref strWhere) == -1)
                {
                    this.Err = "��ȡSQL������,����:Case.ICDDML.Query.GetDiagnoseBySex";
                    return null;
                }
                strSql += strWhere;
                try
                {
                    strSql = string.Format(strSql, sexCode);
                }
                catch (Exception ex)
                {
                    this.Err = "SQL��丳ֵ����!" + ex.Message;
                    if (!Reader.IsClosed)  //���Readerû�йر�
                    {
                        this.Reader.Close(); //�ر�Reader
                    }
                    return null;
                }
                //ִ��SQL���
                this.ExecQuery(strSql);
                //��ȡ����
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList tempAL = con.GetList("CASECATEGORY");
                if (tempAL != null && tempAL.Count > 0)
                {
                    //��ȡ����
                    arrList = ICDReaderInfoNotCategory();
                }
                else
                {
                    arrList = ICDReaderInfo();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)  //���Readerû�йر�
                {
                    this.Reader.Close(); //�ر�Reader
                }

                return null;
            }
            return arrList;
        }
        /// <summary>
        /// �Ա� 
        /// </summary>
        /// <returns></returns>
        [Obsolete("���� �� ö�� ���� ", true)]
        public ArrayList SexList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.Spell obj = new FS.HISFC.Object.Base.Spell();
            //obj.ID = "A";
            //obj.Name = "ȫ��";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "M";
            //obj.Name = "��"; //�����ҽ��վ�����𣬳�Ժ��϶�Ӧ ��Ҫ���
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "F";
            //obj.Name = "Ů"; //�����ҽ��վ�����𣬳�Ժ��϶�Ӧ ��Ҫ���
            //list.Add(obj);
            return list;
        }

        /// <summary>
        /// ��Reader �ж�ȡ��Ϣ �浽 ������ 
        /// </summary>
        /// <returns>���󷵻� null  �ɹ����� ����</returns>
        /// Creator: zhangjunyi@FS.com  2005/05/30
        [Obsolete("���� �� QueryCompareICD ���� ", true)]
        private ArrayList ReadInfoCompare()
        {
            try
            {
                //���嶯̬���� ���洢��ѯ���������ݼ�
                ArrayList arryCompare = new ArrayList();
                //������� ���洢����
                FS.HISFC.Models.HealthRecord.ICDCompare compare = null;
                while (this.Reader.Read())
                {
                    compare = new FS.HISFC.Models.HealthRecord.ICDCompare();

                    compare.ICD9.ID = Reader[0].ToString();		//ICD9����
                    compare.ICD9.Name = Reader[1].ToString();		//ICD9����
                    compare.ICD10.ID = Reader[2].ToString();			//ICD10����
                    compare.ICD10.Name = Reader[3].ToString();		//ICD10����
                    compare.OperInfo.ID = Reader[4].ToString();		//¼�����Ա����
                    compare.OperInfo.Name = Reader[5].ToString();	//¼�����Ա����
                    compare.OperInfo.OperTime = NConvert.ToDateTime(Reader[6]);
                    compare.IsValid = NConvert.ToBoolean(Reader[7].ToString()); //��Ч��
                    compare.ICD9.SpellCode = Reader[8].ToString(); //ƴ��
                    compare.ICD9.UserCode = Reader[9].ToString(); //�Զ���
                    compare.ICD9.KeyCode = Reader[10].ToString();
                    arryCompare.Add(compare); //��ҵ���̬������
                    compare = null;           //�ͷ���Դ
                }
                this.Reader.Close(); //�ر�Reader
                return arryCompare;
            }
            catch (Exception ex)
            {
                //���ִ���
                this.Err = ex.Message;
                if (!this.Reader.IsClosed) //���û���ͷ� Reader
                {
                    this.Reader.Close(); //�ر�Reader
                }
                return null;
            }
        }
        #endregion
    }
}
