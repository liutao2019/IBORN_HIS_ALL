using System;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizLogic.EPR
{
    /// <summary>
    /// EMR ��ժҪ˵����
    /// ���Ӳ���������
    /// </summary>
    public class EMR : FS.FrameWork.Management.Database
    {
        public EMR()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        protected FS.HISFC.BizLogic.File.DataFile manager = new FS.HISFC.BizLogic.File.DataFile();

        #region "���Ӳ���"
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <returns>����ʱ���ַ�������ΪError,��ϵͳ����δ����</returns>
        public string GetControlArgument(string ctlID)
        {
            string strSql = string.Empty;
            string ctlValue = string.Empty;

            if (this.Sql.GetSql("QueryControlerInfo.2", ref strSql) == -1) return string.Empty;
            if (strSql == null) return string.Empty;
            try
            {
                strSql = string.Format(strSql, ctlID);
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    ctlValue = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                if (Reader.IsClosed == false) Reader.Close();
                return "Error";
            }
            this.Reader.Close();

            if (ctlValue == string.Empty)
            {
                this.Err = "ϵͳδά���������ã���������:" + ctlID + "����ϵϵͳ����Ա��";
                this.ErrCode = "ϵͳδά���������ã���������:" + ctlID + "����ϵϵͳ����Ա��";
                this.WriteErr();
                return "Error";
            }
            return ctlValue;
        }

        /// <summary>
        /// ����ļ��б�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetEmrList(string inpatientNo)
        {
            if (manager.GetFiles(inpatientNo) > 0)//����ļ��б�
            {
                return manager.alFiles;
            }
            return null;
        }


        /// <summary>
        /// ��ѯ���Ӳ�����־����
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet QueryLogo(string strWhere)
        {
            string sql = "";
            if (this.Sql.GetSql("Emr_Logo_Query", ref sql) == -1) return null;
            DataSet ds = new DataSet();
            if (this.ExecQuery(sql + " " + strWhere, ref ds) == -1) return null;
            return ds;
        }
        #endregion

        #region �ڵ����
        /// <summary>
        /// ��ýڵ�����
        /// </summary>
        /// <param name="table"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string table, string inpatientNo, string nodeName)
        {
            return manager.GetNodeValueFormDataStore(table, inpatientNo, nodeName);
        }

        /// <summary>
        /// ��ýڵ��б�
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public ArrayList GetNodePathList(string table)
        {
            string strSql = "EPR.EMR.GetNodePathList";
            if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;
            if (this.ExecQuery(strSql, table) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = obj.ID;
                obj.Memo = "STRING";
                obj.User01 = obj.ID;
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public System.Data.DataSet QueryEMRByNode(string strWhere)
        {
            string strSql = "EPR.EMR.QueryEMRByNode";
            if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, strWhere);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (strSql.TrimEnd().Substring(strSql.TrimEnd().Length - 5).ToUpper() == "WHERE")
                strSql = strSql.TrimEnd().Substring(0, strSql.TrimEnd().Length - 5);

            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        #endregion

        #region ʱ��ڵ����
        /// <summary>
        /// ��ýڵ�����
        /// </summary>
        /// <param name="table"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetDateNodeValueByIndex(string table, string inpatientNo, string Name, string NodeName, DateTime date, string index)
        {
            return manager.GetDateNodeValueFormDataStoreByIndex(table, inpatientNo, Name, NodeName, date, index);
        }

        /// <summary>
        /// ��ýڵ�����
        /// </summary>
        /// <param name="table"></param>
        /// <param name="inpatientNo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetDateNodeValueByTime(string table, string inpatientNo, string Name, string NodeName, DateTime date)
        {
            return manager.GetDateNodeValueFormDataStoreByTime(table, inpatientNo, Name, NodeName, date);
        }

        public int SaveNodeToDateDataStoreByTime(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string NodeValue, string Unit)
        {
            return manager.SaveNodeToDateDataStoreByTime(Table, dt, Name, nodeName, date, NodeValue, Unit);
        }

        public int SaveNodeToDateDataStoreByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string Index, string NodeValue, string Unit)
        {
            return manager.SaveNodeToDateDataStoreByIndex(Table, dt, Name, nodeName, date, Index, NodeValue, Unit);
        }
       
        public int SaveNodeToDateDataStoreByInsertIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string Index, string NodeValue, string Unit)
        {
            return manager.SaveNodeToDateDataStoreByInsertIndex(Table, dt, Name, nodeName, date, Index, NodeValue, Unit);
        }
        public int DelDataStoreVitalSignByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt)
        {
            return manager.DelDataStoreVitalSignByIndex(Table, dt);
        }
        public int DelDataStoreVitalSignByIndex1OneTime(string Table, FS.HISFC.Models.File.DataFileInfo dt, DateTime recordtime)
        {
            return manager.DelDataStoreVitalSignByIndex1OneTime(Table, dt, recordtime);
        }

        public Hashtable QueryDataStoreVitalSignByIndex1(string Table, string datatype, string inpatientNo)
        {
            return manager.QueryDataStoreVitalSignByIndex1(Table, datatype, inpatientNo);
        }


        public ArrayList QueryDataStoreVitalSignByIndex1OneTime(string Table, string datatype, string inpatientNo, DateTime recorddate)
        {
            return manager.QueryDataStoreVitalSignByIndex1OneTime(Table, datatype, inpatientNo, recorddate);
        }
        public ArrayList QueryDataStoreVitalSignByRecordDate(string Table, string datatype, string nodename, string patientids)
        {
            return manager.QueryDataStoreVitalSignByRecordDate(Table, datatype, nodename, patientids);
        }

        public Hashtable QueryDataStoreVitalSignByAllIndex1Data(string Table, string datatype, string nodename, string patientids, string recorddate)
        {
            return manager.QueryDataStoreVitalSignByAllIndex1Data(Table, datatype, nodename, patientids, recorddate);
        }

        /// <summary>
        /// ��ýڵ��б�
        /// </summary>
        /// <param name="table"></param>
        /// <returns>ArrayList:NeuObject(id, nodename,record_date,record_index,nodevalue,unit)</returns>
        public ArrayList GetDateNodePathList(string table, string inpatientNo, string Name)
        {
            string strSql = "EPR.EMR.GetDateNodePathList.1";
            //id, nodename,record_date,record_index,nodevalue,unit 
            if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;
            if (this.ExecQuery(strSql, table, inpatientNo, Name) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.Memo = this.Reader[2].ToString();
                obj.User01 = this.Reader[3].ToString();
                obj.User02 = this.Reader[4].ToString();
                obj.User03 = this.Reader[5].ToString();
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ��ýڵ��б�
        /// </summary>
        /// <param name="table"></param>
        /// <returns>ArrayList:NeuObject(id, nodename,record_date,record_index,nodevalue,unit)</returns>
        public ArrayList GetDateNodePathList(string table, string inpatientNo, string Name, string NodeName)
        {
            string strSql = "EPR.EMR.GetDateNodePathList.2";
            //id, nodename,record_date,record_index,nodevalue,unit 
            if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;
            if (this.ExecQuery(strSql, table, inpatientNo, Name, NodeName) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.Memo = this.Reader[2].ToString();
                obj.User01 = this.Reader[3].ToString();
                obj.User02 = this.Reader[4].ToString();
                obj.User03 = this.Reader[5].ToString();
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        //public System.Data.DataSet QueryEMRByNode(string strWhere)
        //{
        //    string strSql = "EPR.EMR.QueryEMRByNode";
        //    if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;
        //    try
        //    {
        //        strSql = string.Format(strSql, strWhere);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.WriteErr();
        //        return null;
        //    }
        //    if (strSql.TrimEnd().Substring(strSql.TrimEnd().Length - 5).ToUpper() == "WHERE")
        //        strSql = strSql.TrimEnd().Substring(0, strSql.TrimEnd().Length - 5);

        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    if (this.ExecQuery(strSql, ref ds) == -1) return null;
        //    return ds;
        //}
        #endregion ʱ��ڵ����

        #region �����
        /// <summary>
        ///  ����һ����
        /// </summary>
        /// <param name="obj"> id = fileid ;name fileName,memo,fileMemo ,User01,�����ơ�������</param>
        /// <returns></returns>
        public int InsertMacro(FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.Macro.InsertMacro", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, obj.ID, obj.Name, obj.Memo, obj.User01);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


































        /// <summary>
        /// ɾ��һ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMacro(string id, string user01)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.Macro.DeleteMacro", ref strSql) == -1) return -1;
            return this.ExecNoQuery(strSql, id, user01);
        }


























        /// <summary>
        /// ����һ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateMacro(FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.Macro.UpdateMacro", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, obj.ID, obj.Name, obj.Memo, obj.User01);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ú�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetMacroList(string user01)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.Macro.GetMacroList", ref strSql) == -1) return null;
            if (this.ExecQuery(strSql, user01) == -1) return null;
            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    obj.User01 = this.Reader[3].ToString();
                    al.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                this.Err = ex.Message;
                return null;
            }
            this.Reader.Close();
            return al;
        }

        #endregion

        #region ����


        /// <summary>
        /// ��ѯ���ŵĻ���
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet QueryEMRLocked()
        {
            string strSql = "EPR.EMR.QueryEMRLocked";
            if (this.Sql.GetSql(strSql, ref strSql) == -1) return null;

            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }

        #region [Obsolete]
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [ObsoleteAttribute("�÷����ѹ�ʱ������Ϊint SetEMRLocked(string fileID, FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject obj, bool isLocked)")]
        public int SetEMRLocked(FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject oper, bool locked)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.DeleteEMRLocked", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, patient.ID);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }

            if (this.ExecNoQuery(strSql) == -1) return -1;

            if (this.Sql.GetSql("EPR.EMR.InsertEMRLocked", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Name,
                    patient.PVisit.PatientLocation.Dept.ID, patient.PVisit.PatientLocation.Dept.Name, patient.Memo, FS.FrameWork.Function.NConvert.ToInt32(locked),
                    oper.ID, oper.Name);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        //<summary>
        //�ж��Ƿ���
        //</summary>
        //<param name="inpatient_no"></param>
        //<returns></returns>
        [ObsoleteAttribute("�÷����ѹ�ʱ������Ϊbool IsEMRLocked(string fileID, ref FS.FrameWork.Models.NeuObject obj)")]
        public bool IsEMRLocked(string inpatient_no, ref FS.FrameWork.Models.NeuObject oper)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.IsEMRLocked", ref strSql) == -1) return false;
            strSql = string.Format(strSql, inpatient_no);
















            if (this.ExecQuery(strSql) == -1) return false;

            bool bLocked = false;
            if (this.Reader.Read())
            {
                bLocked = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[0]);
                oper = new FS.FrameWork.Models.NeuObject();
                oper.ID = this.Reader[1].ToString();
                oper.Name = this.Reader[2].ToString();
                oper.Memo = this.Reader[3].ToString();
            }
            this.Reader.Close();
            return bLocked;
        }
        #endregion

        #region ���ݲ����ſ��Ʋ���������modified by pantiejun date:2007-10-17
        /// <summary>
        ///  ��ѯ�Ƿ���
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEMRLocked(string patientid, string fileID, ref FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.IsEMRLocked", ref strSql) == -1) return false;
            strSql = string.Format(strSql, patientid, fileID);

            if (this.ExecQuery(strSql) == -1) return false;

            bool bLocked = false;
            if (this.Reader.Read())
            {
                bLocked = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[0]);
                obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[1].ToString();
                obj.Name = this.Reader[2].ToString();
                obj.Memo = this.Reader[3].ToString();
            }
            this.Reader.Close();
            return bLocked;
        }
        /// <summary>
        /// ���������ǲ���
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="patient"></param>
        /// <param name="obj"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        public int SetEMRLocked(FS.HISFC.Models.File.DataFileInfo dfi, FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject obj, bool isLocked)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.DeleteEMRLocked", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, patient.ID, dfi.ID);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }

            if (this.ExecNoQuery(strSql) == -1) return -1;

            if (this.Sql.GetSql("EPR.EMR.InsertEMRLocked", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Name,
                    patient.PVisit.PatientLocation.Dept.ID, patient.PVisit.PatientLocation.Dept.Name, patient.Memo, FS.FrameWork.Function.NConvert.ToInt32(isLocked),
                    obj.ID, obj.Name, dfi.ID, dfi.Name);
            }
            catch (Exception ex)
            {
                this.Err = "����Ĳ�����\n" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #endregion
        #region MCA���µ�
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="queryTime"></param>
        /// <returns></returns>
        public DataSet QueryTemperature(string inpatientNo, DateTime queryTime)
        {
            string strSql = "";
            if (this.Sql.GetSql("QueryTemperature", ref strSql) == -1) return null;
            strSql = String.Format(strSql, inpatientNo, queryTime);
            DataSet ds = new DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="queryTime"></param>
        /// <returns></returns>
        public DataSet QueryThrob(string inpatientNo, DateTime queryTime)
        {
            string strSql = "";
            if (this.Sql.GetSql("QueryThrob", ref strSql) == -1) return null;
            strSql = String.Format(strSql, inpatientNo, queryTime);
            DataSet ds = new DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="queryTime"></param>
        /// <returns></returns>
        public DataSet QueryBreath(string inpatientNo, DateTime queryTime)
        {
            string strSql = "";
            if (this.Sql.GetSql("QueryBreath", ref strSql) == -1) return null;
            strSql = String.Format(strSql, inpatientNo, queryTime);
            DataSet ds = new DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// Ѫѹ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="queryTime"></param>
        /// <returns></returns>
        public DataSet QueryPressure(string inpatientNo, DateTime queryTime)
        {
            string strSql = "";
            if (this.Sql.GetSql("QueryPressure", ref strSql) == -1) return null;
            strSql = String.Format(strSql, inpatientNo, queryTime);
            DataSet ds = new DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        /// <summary>
        /// �� ��Һ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="queryTime"></param>
        /// <returns></returns>
        public DataSet QueryInject(string inpatientNo, DateTime queryTime)
        {
            string strSql = "";
            if (this.Sql.GetSql("QueryInject", ref strSql) == -1) return null;
            strSql = String.Format(strSql, inpatientNo, queryTime);
            DataSet ds = new DataSet();
            if (this.ExecQuery(strSql, ref ds) == -1) return null;
            return ds;
        }
        #endregion MCA���µ�


    }
}