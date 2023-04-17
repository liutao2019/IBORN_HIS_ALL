using System;
using System.Collections;
namespace FS.HISFC.BizLogic.File
{
    /// <summary>
    /// GetFile ��ժҪ˵����
    /// ����ļ�
    /// </summary>
    public class DataFile : FS.FrameWork.Management.Database
    {
        public DataFile(string type)
            : base()
        {
            try
            {
                dtParam = this.ParamManager.Get(type) as FS.HISFC.Models.File.DataFileParam;
                if (dtParam == null) return;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return;
            }
        }
        public DataFile()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public FS.HISFC.BizLogic.File.DataFileParam ParamManager = new FS.HISFC.BizLogic.File.DataFileParam();
        public FS.HISFC.BizLogic.File.DataFileInfo FileManager = new FS.HISFC.BizLogic.File.DataFileInfo();
        private FS.HISFC.Models.File.DataFileParam dtParam = null;
        private FS.HISFC.Models.File.DataFileInfo dtFile = null;

        #region ����
        /// <summary>
        /// ��ǰ�ļ��б�
        /// </summary>
        public ArrayList alFiles;
        /// <summary>
        /// ��ǰģ���б�
        /// </summary>
        public ArrayList alModuals;

        /// <summary>
        /// ��������ʾ����
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int SetType(string type)
        {
            try
            {
                dtParam = this.ParamManager.Get(type) as FS.HISFC.Models.File.DataFileParam;
                if (dtParam == null) return -1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        public int GetModuals(bool isAll)
        {
            try
            {

                alModuals = FileManager.GetList(dtParam, 1, isAll);
                return alModuals.Count;
            }
            catch { return -1; }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetFiles(params string[] param)
        {
            try
            {
                this.dtFile = new FS.HISFC.Models.File.DataFileInfo();
                this.dtFile.Param = (FS.HISFC.Models.File.DataFileParam)this.dtParam.Clone();
                this.dtFile.Param.ID = string.Format(this.dtParam.ID, param);
                this.dtFile.Param.Type = this.dtParam.Type;
                alFiles = FileManager.GetList(this.dtFile.Param);
                return alFiles.Count;
            }
            catch { return -1; }
        }
        /// <summary>
        /// ��ò���
        /// </summary>
        public FS.HISFC.Models.File.DataFileParam DataFileParam
        {
            get
            {
                if (this.dtParam == null) this.dtParam = new FS.HISFC.Models.File.DataFileParam();
                return this.dtParam;
            }
            set
            {
                this.dtParam = value;
            }
        }
        /// <summary>
        /// ����ļ���Ϣ
        /// </summary>
        public FS.HISFC.Models.File.DataFileInfo DataFileInfo
        {
            get
            {
                if (this.dtFile == null) this.dtFile = new FS.HISFC.Models.File.DataFileInfo();
                return this.dtFile;
            }
            set
            {
                this.dtFile = value;
            }
        }


        #endregion

        #region �ڵ����
        /// <summary>
        /// �����㵽���ݿ�
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="dt"></param>
        /// <param name="ParentText"></param>
        /// <param name="NodeText"></param>
        /// <param name="NodeValue"></param>
        /// <returns></returns>
        public int SaveNodeToDataStore(string Table, FS.HISFC.Models.File.DataFileInfo dt, string ParentText, string NodeText, string NodeValue)
        {
            string strSql = "";
            string sql = "";

            if (this.Sql.GetSql("Management.DataFile.Select", ref strSql) == -1) return -1;
            try
            {
                FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, dt.Name, dt.Index1, dt.Index2, ParentText, NodeText, NodeValue, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "Management.DataFile.Select��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecQuery(sql) == -1) return -1;
            if (this.Reader.Read())//�м�¼��ִ�и���
            {
                if (NodeValue == this.Reader[0].ToString())//��ͬ����
                {
                    this.Reader.Close();
                    return 0;
                }
                else
                {
                    if (this.Sql.GetSql("Management.DataFile.UpdateToDataStore", ref strSql) == -1) return -1;
                }
            }
            else//�޼�¼��ִ�в���
            {
                if (this.Sql.GetSql("Management.DataFile.InsertToDataStore", ref strSql) == -1) return -1;
            }
            try
            {
                FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, dt.Name, dt.Index1, dt.Index2, ParentText, NodeText, NodeValue, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// ɾ����㡡
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int DeleteAllNodeFromDataStore(string Table, FS.HISFC.Models.File.DataFileInfo dt)
        {
            string strSql = "", sql = "";
            if (this.Sql.GetSql("Management.DataFile.DeleteAllNodeFromDataStore", ref strSql) == -1) return -1;
            try
            {
                sql = string.Format(strSql, Table, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "DeleteNode��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ��ýڵ�����
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetNodeValueFormDataStore(string Table, string inpatientNo, string nodeName)
        {
            string strSql = "", sql = "";
            if (this.Sql.GetSql("Management.DataFile.GetNodeValueFormDataStore", ref strSql) == -1) return "-1";
            try
            {
                sql = string.Format(strSql, Table, inpatientNo, nodeName);
            }
            catch (Exception ex)
            {
                this.Err = "GetNodeValueFormDataStore��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return "-1";
            }
            return this.ExecSqlReturnOne(sql);
        }
        #endregion
        #region ���ڽڵ�
        public string GetDateNodeValueFormDataStoreByIndex(string Table, string inpatientNo, string Name, string NodeName, DateTime date, string index)
        {
            string strSql = "", sql = "";
            if (this.Sql.GetSql("Management.DataFile.GetDateNodeValueFormDataStoreByIndex", ref strSql) == -1) return "-1";
            try
            {
                sql = string.Format(strSql, Table, inpatientNo, Name, NodeName, date.ToString("yyyy-MM-dd"), index);
            }
            catch (Exception ex)
            {
                this.Err = "GetDateNodeValueFormDataStoreByIndex��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return "-1";
            }
            return this.ExecSqlReturnOne(sql);
        }
        /// <summary>
        /// ��ýڵ�����
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetDateNodeValueFormDataStoreByTime(string Table, string inpatientNo, string Name, string NodeName, DateTime date)
        {
            string strSql = "", sql = "";
            if (this.Sql.GetSql("Management.DataFile.GetDateNodeValueFormDataStoreByTime", ref strSql) == -1) return "-1";
            try
            {
                sql = string.Format(strSql, Table, inpatientNo, Name, NodeName, date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                this.Err = "GetDateNodeValueFormDataStoreByTime��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return "-1";
            }
            return this.ExecSqlReturnOne(sql);
        }

      /// <summary>
      /// ɾ���˲������ʱ���Ļ����¼��ص���������
      /// </summary>
      /// <param name="Table"></param>
      /// <param name="dt"></param>
      /// <param name="recordtime"></param>
      /// <returns></returns>
        public int DelDataStoreVitalSignByIndex1OneTime(string Table, FS.HISFC.Models.File.DataFileInfo dt, DateTime recordtime)
        {
            string strSql = "";
            string sql = "";
            if (this.Sql.GetSql("Management.DataFile.DelDataStoreVitalSignByIndex1OneTime", ref strSql) == -1) return -1;
            try
            {
                FS.FrameWork.Public.String.FormatString(strSql, out sql, dt.DataType, dt.Index1, this.Operator.ID, recordtime);
            }
            catch (Exception ex)
            {
                this.Err = "DelNodeToDataStoreɾ��ʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }
       /// <summary>
        /// ɾ���˲��˵����µ���ص���������
       /// </summary>
       /// <param name="Table"></param>
       /// <param name="dt"></param>
       /// <returns></returns>
        public int DelDataStoreVitalSignByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt)
        {
            string strSql = "";
            string sql = "";
            if (this.Sql.GetSql("Management.DataFile.DelDateDataStoreByIndex", ref strSql) == -1) return -1;
            try
            {
                FS.FrameWork.Public.String.FormatString(strSql, out sql, dt.DataType, dt.Index1, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "DelNodeToDataStoreɾ��ʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// �����㵽���ݿ�
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="dt"></param>
        /// <param name="Name">��������</param>
        /// <param name="nodeName">�ڵ�</param>
        /// <param name="date">����</param>
        /// <param name="index">�ڵ�����</param>
        /// <param name="NodeValue"></param>
        /// <returns></returns>
        public int SaveNodeToDateDataStoreByInsertIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string index, string NodeValue, string Unit)
        {
            string strSql = "";
            string sql = "";
                if (this.Sql.GetSql("Management.DataFile.InsertToDateDataStore", ref strSql) == -1) return -1;
                try
                {
                    FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, Name, dt.Index1, dt.Index2, nodeName, nodeName, date.ToString("yyyy-MM-dd HH:mm:ss"), index, NodeValue, this.Operator.ID, Unit);
                }
                catch (Exception ex)
                {
                    this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
            
            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }


       /// <summary>
        /// ȡ���˲��˵��������µ�����
       /// </summary>
       /// <param name="Table"></param>
       /// <param name="datatype"></param>
       /// <param name="inpatientNo"></param>
       /// <returns></returns>
        public Hashtable QueryDataStoreVitalSignByIndex1(string Table, string datatype, string inpatientNo)
        {
            Hashtable DataStoreVitalSignHashTable = new Hashtable();
            string strSql = "";
            if (this.Sql.GetSql("Management.DataFile.QueryDataStoreVitalSignByIndex1", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Table, datatype, inpatientNo);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }

            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Nurse.DataStoreVitalSign DataStoreVitalSign = null;
            ArrayList recordDateList = new ArrayList();
            ArrayList AllDateList = new ArrayList();
            while (this.Reader.Read())
            {
                DataStoreVitalSign = new FS.HISFC.Models.Nurse.DataStoreVitalSign();

                DataStoreVitalSign.NodeName = this.Reader[0].ToString();
                DataStoreVitalSign.NodeValue=this.Reader[1].ToString();
                DataStoreVitalSign.RecordDate = (DateTime) this.Reader[2];
                if (DataStoreVitalSign.NodeName == "���ʱ��")
                {
                    recordDateList.Add(DataStoreVitalSign.RecordDate);
                }
                AllDateList.Add(DataStoreVitalSign);
              
            }

            this.Reader.Close();
            DataStoreVitalSignHashTable.Add("AllDateList", AllDateList);
            DataStoreVitalSignHashTable.Add("recordDateList", recordDateList);
            return DataStoreVitalSignHashTable;
        }
        /// <summary>
        /// ���ݿ���ȡ���˲��˵�һ�������¼
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="datatype"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryDataStoreVitalSignByIndex1OneTime(string Table, string datatype, string inpatientNo, DateTime recorddate)
        {
            ArrayList DataStoreVitalSignArrayList = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("Management.DataFile.QueryDataStoreVitalSignByIndex1OneTime", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Table, datatype, inpatientNo, recorddate);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }

            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Nurse.DataStoreVitalSign DataStoreVitalSign = null;
            while (this.Reader.Read())
            {
                DataStoreVitalSign = new FS.HISFC.Models.Nurse.DataStoreVitalSign();

                DataStoreVitalSign.NodeName = this.Reader[0].ToString();
                DataStoreVitalSign.NodeValue = this.Reader[1].ToString();
                DataStoreVitalSign.RecordDate = (DateTime)this.Reader[2];

                DataStoreVitalSignArrayList.Add(DataStoreVitalSign);

            }

            this.Reader.Close();
            return DataStoreVitalSignArrayList;
        }
      /// <summary>
      /// ���ݿ���ȡ�����˼��ϵ����м�¼ʱ���¼
      /// </summary>
      /// <param name="Table"></param>
      /// <param name="datatype"></param>
      /// <param name="nodename"></param>
      /// <param name="patientids"></param>
      /// <returns></returns>
        public ArrayList QueryDataStoreVitalSignByRecordDate(string Table, string datatype, string nodename, string patientids)
        {
            ArrayList DataStoreVitalSignArrayList = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("Management.DataFile.QueryDataStoreVitalSignByRecordDate", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Table, datatype, nodename, patientids);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }

            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Nurse.DataStoreVitalSign DataStoreVitalSign = null;
            while (this.Reader.Read())
            {
                DataStoreVitalSign = new FS.HISFC.Models.Nurse.DataStoreVitalSign();

                DataStoreVitalSign.NodeName = this.Reader[0].ToString();
                DataStoreVitalSign.NodeValue = this.Reader[1].ToString();
                DataStoreVitalSign.RecordDate = (DateTime)this.Reader[2];

                DataStoreVitalSignArrayList.Add(DataStoreVitalSign);

            }

            this.Reader.Close();
            return DataStoreVitalSignArrayList;
        }
        /// <summary>
        /// ȡ��һ��ʱ�������ز��˼��ϵ��������µ���ؽڵ��¼
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="datatype"></param>
        /// <param name="nodename"></param>
        /// <param name="patientids"></param>
        /// <returns></returns>
        public Hashtable QueryDataStoreVitalSignByAllIndex1Data(string Table, string datatype, string nodename, string patientids, string recorddate)
        {
            Hashtable DataStoreVitalSignHashtable = new Hashtable();
            string strSql = "";
            if (this.Sql.GetSql("Management.DataFile.QueryDataStoreVitalSignByAllIndex1Data", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Table, datatype, nodename, patientids,recorddate);
            }
            catch
            {
                this.Err = "�����������!";
                return null;
            }

            if (this.ExecQuery(strSql) == -1) return null;
            FS.HISFC.Models.Nurse.DataStoreVitalSign DataStoreVitalSign = null;
            while (this.Reader.Read())
            {
                DataStoreVitalSign = new FS.HISFC.Models.Nurse.DataStoreVitalSign();

                DataStoreVitalSign.NodeName = this.Reader[0].ToString();
                DataStoreVitalSign.NodeValue = this.Reader[1].ToString();
                DataStoreVitalSign.RecordDate = (DateTime)this.Reader[2];
                DataStoreVitalSign.Index1 = this.Reader[3].ToString();

                //���õ����б������������
                if (DataStoreVitalSignHashtable.ContainsKey(DataStoreVitalSign.Index1))
                {
                    ArrayList DataStoreVitalSignArrayList = (ArrayList)DataStoreVitalSignHashtable[DataStoreVitalSign.Index1];
                    DataStoreVitalSignArrayList.Add(DataStoreVitalSign);
                    DataStoreVitalSignHashtable[DataStoreVitalSign.Index1] = DataStoreVitalSignArrayList;
                }
                else
                {
                    ArrayList DataStoreVitalSignArrayListnew = new ArrayList();
                    DataStoreVitalSignArrayListnew.Add(DataStoreVitalSign);
                    DataStoreVitalSignHashtable[DataStoreVitalSign.Index1] = DataStoreVitalSignArrayListnew;
                }
            }

            this.Reader.Close();



            return DataStoreVitalSignHashtable;
        }
        /// <summary>
        /// �����㵽���ݿ�
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="dt"></param>
        /// <param name="Name">��������</param>
        /// <param name="nodeName">�ڵ�</param>
        /// <param name="date">����</param>
        /// <param name="index">�ڵ�����</param>
        /// <param name="NodeValue"></param>
        /// <returns></returns>
        public int SaveNodeToDateDataStoreByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string index, string NodeValue, string Unit)
        {
            string strSql = "";
            string sql = "";
            string strReturn = this.GetDateNodeValueFormDataStoreByIndex(Table, dt.Index1, Name, nodeName, date, index);
            if (strReturn != "-1")
            {
                if (NodeValue == strReturn)//��ͬ����
                {
                    return 0;
                }
                else
                {
                    if (this.Sql.GetSql("Management.DataFile.UpdateToDateDataStoreByIndex", ref strSql) == -1) return -1;
                    try
                    {
                        FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, Name, dt.Index1, dt.Index2, "", nodeName, date.ToString("yyyy-MM-dd"), index, NodeValue, this.Operator.ID);
                    }
                    catch (Exception ex)
                    {
                        this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                        this.WriteErr();
                        return -1;
                    }
                }
            }
            else//�޼�¼��ִ�в���
            {
                if (this.Sql.GetSql("Management.DataFile.InsertToDateDataStore", ref strSql) == -1) return -1;
                try
                {
                    FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, Name, dt.Index1, dt.Index2, nodeName, nodeName, date.ToString("yyyy-MM-dd HH:mm:ss"), index, NodeValue, this.Operator.ID, Unit);
                }
                catch (Exception ex)
                {
                    this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
            }
            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// �����㵽���ݿ�
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="dt"></param>
        /// <param name="Name">��������</param>
        /// <param name="nodeName">�ڵ�</param>
        /// <param name="date">����</param>
        /// <param name="index">�ڵ�����</param>
        /// <param name="NodeValue"></param>
        /// <returns></returns>
        public int SaveNodeToDateDataStoreByTime(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string NodeValue, string Unit)
        {
            string strSql = "";
            string sql = "";
            string strReturn = this.GetDateNodeValueFormDataStoreByTime(Table, dt.Index1, Name, nodeName, date);
            if (strReturn != "-1")
            {
                if (NodeValue == strReturn)//��ͬ����
                {
                    return 0;
                }
                else
                {
                    if (this.Sql.GetSql("Management.DataFile.UpdateToDateDataStoreByTime", ref strSql) == -1) return -1;
                    try
                    {
                        FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, Name, dt.Index1, dt.Index2, "", nodeName, date.ToString("yyyy-MM-dd HH:mm:ss"), NodeValue, Unit, this.Operator.ID);
                    }
                    catch (Exception ex)
                    {
                        this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                        this.WriteErr();
                        return -1;
                    }
                }
            }
            else//�޼�¼��ִ�в���
            {
                if (this.Sql.GetSql("Management.DataFile.InsertToDateDataStore", ref strSql) == -1) return -1;
                try
                {
                    FS.FrameWork.Public.String.FormatString(strSql, out sql, Table, dt.ID, dt.Type, dt.DataType, Name, dt.Index1, dt.Index2, "", nodeName, date.ToString("yyyy-MM-dd HH:mm:ss"), "", NodeValue, Unit, this.Operator.ID,Unit);
                }
                catch (Exception ex)
                {
                    this.Err = "SaveNodeToDataStore��ֵʱ�����" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
            }
            try
            {
                this.Reader.Close();
            }
            catch { }
            return this.ExecNoQuery(sql);
        }
        #endregion ���ڽڵ����

        #region ���ֶβ���

        /// <summary>
        /// ���ļ����뵽���ݿ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData">������ļ�����</param>
        /// <returns></returns>
        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;
            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.InputBlob(sql, fileData);
        }

        /// <summary>
        /// ����ļ� 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref byte[] fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;

            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.byte", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.Modual.byte", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            fileData = this.OutputBlob(sql);
            return 0;
        }


        /// <summary>
        /// ���ļ����뵽���ݿ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData">������ļ�����</param>
        /// <returns></returns>
        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, string fileData)
        {

            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;

            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ImportToDatabase.Modual", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ImportToDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.InputLong(sql, fileData);

        }

        /// <summary>
        /// ����ļ� 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref string fileData)
        {
            string strSql = "", sql = "";
            if (dt.ID == null || dt.ID == "") return -1;
            if (dt.Type.Trim() == "0")//����
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase", ref strSql) == -1) return -1;
            }
            else if (dt.Type.Trim() == "1") //ģ��
            {
                if (this.Sql.GetSql("Management.DataFile.ExportFromDatabase.Modual", ref strSql) == -1) return -1;
            }
            else
            {
                this.Err = "δ֪�ļ�����";
                return -1;
            }

            try
            {
                sql = string.Format(strSql, dt.ID);
            }
            catch (Exception ex)
            {
                this.Err = "ExportFromDatabase��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            fileData = this.ExecSqlReturnOne(sql);
            if (fileData == "-1") return -1;
            return 0;
        }

        #endregion
    }
}
