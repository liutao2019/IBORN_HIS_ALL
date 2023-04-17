using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.BizProcess.Factory
{
    /// <summary>
    /// ���Ӳ���
    /// </summary>
    public abstract class EPRBase : FactoryBase
    {
        #region IEPR ��Ա


        public System.Collections.ArrayList UserTextGetList(string type, int usertype)
        {
            FS.HISFC.BizLogic.Manager.UserText manager = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(manager);
            return manager.GetList(type, usertype);
        }

        ////{2F7319BB-AAD6-49da-A1D7-F67E4DD5253B}
        //public bool GetMedicalPermission(FS.HISFC.Models.EPR.EnumPermissionType type, int index)
        //{
        //    FS.HISFC.BizLogic.Medical.Permission manager = new FS.HISFC.BizLogic.Medical.Permission();
        //    this.SetDB(manager);
        //    if (type == FS.HISFC.Models.EPR.EnumPermissionType.EMR)
        //    {
        //        return manager.GetPermission(FS.FrameWork.Management.Connection.Operator.ID).EMRPermission.GetOnePermission(index);
        //    }
        //    else if (type == FS.HISFC.Models.EPR.EnumPermissionType.Order)
        //    {
        //        return manager.GetPermission(FS.FrameWork.Management.Connection.Operator.ID).OrderPermission.GetOnePermission(index);

        //    }
        //    else
        //    {
        //        return manager.GetPermission(FS.FrameWork.Management.Connection.Operator.ID).QCPermission.GetOnePermission(index);

        //    }

        //}

        public FS.HISFC.Models.File.DataFileParam GetDataFileParam(string type)
        {
            FS.HISFC.BizLogic.EPR.DataFileParam manager = new FS.HISFC.BizLogic.EPR.DataFileParam();
            this.SetDB(manager);
            return manager.Get(type) as FS.HISFC.Models.File.DataFileParam;
            //COM_FILEPARAM
        }

        public System.Collections.ArrayList GetModualList(FS.HISFC.Models.File.DataFileParam param, bool isAll)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.GetModualList(param, isAll);
        }

        public System.Collections.ArrayList GetFileList(FS.HISFC.Models.File.DataFileParam param, bool isModual, bool isAll)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.GetList(param, FS.FrameWork.Function.NConvert.ToInt32(isModual), isAll);
        }

        public string GetNewFileID()
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.GetNewFileID();
        }

        public FS.HISFC.Models.File.DataFileInfo GetFile(string id)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.Get(id) as FS.HISFC.Models.File.DataFileInfo;
        }

        public FS.HISFC.Models.File.DataFileInfo GetModualFile(string id)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.Get(id, 1) as FS.HISFC.Models.File.DataFileInfo;
        }

        public int SetFile(FS.HISFC.Models.File.DataFileInfo fileInfo)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.Set(fileInfo);
        }

        public int SetFile(FS.HISFC.Models.File.DataFileInfo fileInfo, int type)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.Set(fileInfo, type);
        }

        public int DeleteFile(string fileID, int type)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.Del(fileID, type);
        }

        public int SetModualValid(FS.HISFC.Models.File.DataFileInfo fileInfo, int type)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.SetValid(fileInfo, type);
        }

        public int SetModualInValid(FS.HISFC.Models.File.DataFileInfo fileInfo, int type)
        {
            FS.HISFC.BizLogic.EPR.DataFileInfo manager = new FS.HISFC.BizLogic.EPR.DataFileInfo();
            this.SetDB(manager);
            return manager.SetInValid(fileInfo, type);
        }

        public int SaveNodeToDataStore(string Table, FS.HISFC.Models.File.DataFileInfo dt, string ParentText, string NodeText, string NodeValue)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.SaveNodeToDataStore(Table, dt, ParentText, NodeText, NodeValue);
        }

        public int DeleteAllNodeFromDataStore(string Table, FS.HISFC.Models.File.DataFileInfo dt)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.DeleteAllNodeFromDataStore(Table, dt);
        }

        public string GetNodeValueFormDataStore(string Table, string inpatientNo, string nodeName)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.GetNodeValueFormDataStore(Table, inpatientNo, nodeName);
        }

        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, byte[] fileData)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.ImportToDatabase(dt, fileData);
        }

        public byte[] ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            byte[] b = new byte[0];
            if (manager.ExportFromDatabase(dt, ref b) == -1)
                return null;
            return b;
        }

        public int ImportToDatabase(FS.HISFC.Models.File.DataFileInfo dt, string fileData)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.ImportToDatabase(dt, fileData);
        }

        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref  byte[] by)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            if (manager.ExportFromDatabase(dt, ref by) == -1)
                return -1;
            return 0;
        }
        public int ExportFromDatabase(FS.HISFC.Models.File.DataFileInfo dt, ref string fileData)
        {
            FS.HISFC.BizLogic.EPR.DataFile manager = new FS.HISFC.BizLogic.EPR.DataFile();
            this.SetDB(manager);
            return manager.ExportFromDatabase(dt, ref fileData);
        }

        public bool IsSign(string fileID)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.IsSign(fileID);
        }

        public bool IsSeal(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.IsSeal(inpatientNo);
        }

        public int Seal(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.Seal(inpatientNo);
        }

        public System.Data.DataSet QueryLogo(string where)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryLogo(where);
        }

        public int UnSeal(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.UnSeal(inpatientNo);
        }

        public int SignEmrPage(string fileId)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.SignEmrPage(fileId);
        }

        public System.Data.DataSet GetNodePath()
        {
            FS.HISFC.BizLogic.EPR.NodePath manager = new FS.HISFC.BizLogic.EPR.NodePath();
            this.SetDB(manager);
            return manager.GetNodePath();
        }

        public System.Collections.ArrayList GetNodePathList(string table)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.GetNodePathList(table);
        }

        public string GetDateNodeValueByIndex(string table, string inpatientNo, string Name, string NodeName, DateTime date, string index)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.GetDateNodeValueByIndex(table, inpatientNo, Name, NodeName, date, index);
        }

        public string GetDateNodeValueByTime(string table, string inpatientNo, string Name, string NodeName, DateTime date)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.GetDateNodeValueByTime(table, inpatientNo, Name, NodeName, date);
        }
        public ArrayList GetDateNodePathList(string table, string inpatientNo, string Name)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.GetDateNodePathList(table, inpatientNo, Name);
        }
        public ArrayList GetDateNodePathList(string table, string inpatientNo, string Name, string NodeName)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.GetDateNodePathList(table, inpatientNo, Name, NodeName);
        }
        public int SaveNodeToDateDataStoreByTime(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string NodeValue, string Unit)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.SaveNodeToDateDataStoreByTime(Table, dt, Name, nodeName, date, NodeValue, Unit);
        }
        public int SaveNodeToDateDataStoreByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string Index, string NodeValue, string Unit)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.SaveNodeToDateDataStoreByIndex(Table, dt, Name, nodeName, date, Index, NodeValue, Unit);
        }

        public int SaveNodeToDateDataStoreByInsertIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt, string Name, string nodeName, DateTime date, string Index, string NodeValue, string Unit)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.SaveNodeToDateDataStoreByInsertIndex(Table, dt, Name, nodeName, date, Index, NodeValue, Unit);
        }
        public int DelDataStoreVitalSignByIndex(string Table, FS.HISFC.Models.File.DataFileInfo dt)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.DelDataStoreVitalSignByIndex(Table,dt);
        }
        public int DelDataStoreVitalSignByIndex1OneTime(string Table, FS.HISFC.Models.File.DataFileInfo dt,DateTime recordtime)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.DelDataStoreVitalSignByIndex1OneTime(Table, dt, recordtime);
        }
        public Hashtable QueryDataStoreVitalSignByIndex1(string Table, string datatype ,string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryDataStoreVitalSignByIndex1(Table, datatype,inpatientNo);
        }
        public ArrayList QueryDataStoreVitalSignByIndex1OneTime(string Table, string datatype, string inpatientNo,DateTime recorddate)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryDataStoreVitalSignByIndex1OneTime(Table, datatype, inpatientNo, recorddate);
        }
        public ArrayList QueryDataStoreVitalSignByRecordDate(string Table, string datatype, string nodename, string patientids)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryDataStoreVitalSignByRecordDate(Table, datatype, nodename, patientids);
        }
        public Hashtable QueryDataStoreVitalSignByAllIndex1Data(string Table, string datatype, string nodename, string patientids, string recorddate)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryDataStoreVitalSignByAllIndex1Data(Table, datatype, nodename, patientids, recorddate);
        }
        public System.Data.DataSet QueryEMRByNode(string strWhere)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryEMRByNode(strWhere);
        }

        public int DeleteNodePath(string nodeName)
        {
            FS.HISFC.BizLogic.EPR.NodePath manager = new FS.HISFC.BizLogic.EPR.NodePath();
            this.SetDB(manager);
            return manager.DeleteNodePath(nodeName);
        }

        public int InsertNodePath(FS.FrameWork.Models.NeuObject obj)
        {
            FS.HISFC.BizLogic.EPR.NodePath manager = new FS.HISFC.BizLogic.EPR.NodePath();
            this.SetDB(manager);
            return manager.InsertNodePath(obj);
        }

        public int InsertQCData(FS.HISFC.Models.EPR.QC qc)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            if (manager.InsertQCData(qc) == -1)
            {
                return -1;
            }
            else
            {
                if (qc.QCData.Saver.ID != "") //ǩ������ 
                    return manager.SignEmrPage(qc);   //ǩ���ü�����
            }
            return 1;
        }

        public bool IsHaveSameEMRName(string index1, string nodeName)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.IsHaveSameEMRName(index1, nodeName);
        }

        public System.Collections.ArrayList GetQCDataBySqlWhere(string where)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.GetQCDataBySqlWhere(where);
        }

        public int DeleteQCCondition(string qcConditonID)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.DeleteQCCondition(qcConditonID);
        }

        public System.Collections.ArrayList GetQCConditionList()
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.GetQCConditionList();
        }

        public int InsertQCCondition(FS.HISFC.Models.EPR.QCConditions conditions)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.InsertQCCondition(conditions);
        }

        public int UpdateQCCondition(FS.HISFC.Models.EPR.QCConditions conditions)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.UpdateQCCondition(conditions);
        }

        public System.Collections.ArrayList GetQCScoreSetList()
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.GetQCScoreSetList();
        }

        public System.Collections.ArrayList GetQCScoreList(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.GetQCScoreList(inpatientNo);
        }

        public int DeleteQCScore(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.DeleteQCScore(inpatientNo);
        }

        public int InsertQCScore(FS.HISFC.Models.EPR.QCScore qcScore)
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.InsertQCScore(qcScore);
        }

        public int UpdateQCDataState(string qcScoreID, int state)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.UpdateQCDataState(qcScoreID, state);
        }

        public int SetSign(FS.FrameWork.Models.NeuObject obj, byte[] byteimg)
        {
            FS.HISFC.BizLogic.EPR.Sign manager = new FS.HISFC.BizLogic.EPR.Sign();
            this.SetDB(manager);
            return manager.SetSign(obj, byteimg);
        }

        public byte[] GetSignBackGround(string emplCode)
        {
            FS.HISFC.BizLogic.EPR.Sign manager = new FS.HISFC.BizLogic.EPR.Sign();
            this.SetDB(manager);
            return manager.GetSignBackGround(emplCode);
        }

        public System.Collections.ArrayList GetQCName()
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.GetQCName();
        }

        public bool ExecQCInfo(string inpatientNo, FS.FrameWork.Management.Interface ISql, FS.HISFC.Models.EPR.QCConditions condition)
        {
            FS.HISFC.BizLogic.EPR.QCInfo manager = new FS.HISFC.BizLogic.EPR.QCInfo();
            this.SetDB(manager);
            return manager.ExecQCInfo(inpatientNo, ISql, condition);
        }

        public int DeleteQCScoreSet(string id)
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.DeleteQCScoreSet(id);
        }

        public int InsertParam(FS.HISFC.Models.File.DataFileParam fileParam)
        {
            FS.HISFC.BizLogic.EPR.DataFileParam manager = new FS.HISFC.BizLogic.EPR.DataFileParam();
            this.SetDB(manager);
            return manager.Insert(fileParam);
        }

        public int UpdateParam(FS.HISFC.Models.File.DataFileParam fileParam)
        {
            FS.HISFC.BizLogic.EPR.DataFileParam manager = new FS.HISFC.BizLogic.EPR.DataFileParam();
            this.SetDB(manager);
            return manager.Update(fileParam);
        }

        public System.Collections.ArrayList GetParamList()
        {
            FS.HISFC.BizLogic.EPR.DataFileParam manager = new FS.HISFC.BizLogic.EPR.DataFileParam();
            this.SetDB(manager);
            return manager.GetList();
        }




        public FS.FrameWork.Models.NeuObject GetSign(string operID)
        {
            FS.HISFC.BizLogic.EPR.Sign manager = new FS.HISFC.BizLogic.EPR.Sign();
            this.SetDB(manager);
            return manager.GetSign(operID);
        }

        public int DeleteSign(string operID)
        {
            FS.HISFC.BizLogic.EPR.Sign manager = new FS.HISFC.BizLogic.EPR.Sign();
            this.SetDB(manager);
            return manager.DeleteSign(operID);
        }

        public System.Collections.ArrayList GetSignList()
        {
            FS.HISFC.BizLogic.EPR.Sign manager = new FS.HISFC.BizLogic.EPR.Sign();
            this.SetDB(manager);
            return manager.GetSignList();
        }

        public System.Collections.ArrayList GetQCInputCondition()
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.GetQCInputCondition();
        }

        public System.Collections.ArrayList GetQCInputCondition(string inpatientNo)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.GetQCInputCondition(inpatientNo);
        }



        public int SetQCScoreSet(FS.HISFC.Models.EPR.QCScore qcScoreSet)
        {
            FS.HISFC.BizLogic.EPR.QCScore manager = new FS.HISFC.BizLogic.EPR.QCScore();
            this.SetDB(manager);
            return manager.SetQCScoreSet(qcScoreSet);
        }




        public System.Collections.ArrayList GetSNOMED(string id, bool isAll)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.GetSNOPMED(id, isAll);
        }

        public System.Collections.ArrayList GetSNOMED()
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.GetSNOPMED();
        }

        public int UpdateSNOMED(FS.HISFC.Models.EPR.SNOMED s)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.UpdateSNOPMED(s);
        }

        public int UpdateSNOPMEDParentCode(FS.HISFC.Models.EPR.SNOMED s)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.UpdateSNOPMEDParentCode(s);
        }

        public int DelSNOPMEDByCode(string code)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.DelSNOPMEDByCode(code);
        }

        public int DelSNOPMEDByPCode(string parentcode)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.DelSNOPMEDByPCode(parentcode);
        }

        public int InsertSNOMED(FS.HISFC.Models.EPR.SNOMED s)
        {
            FS.HISFC.BizLogic.EPR.SNOMED manager = new FS.HISFC.BizLogic.EPR.SNOMED();
            this.SetDB(manager);
            return manager.InsertSNOMED(s);
        }

        public int SaveQCInputCondition(System.Collections.ArrayList al)
        {
            FS.HISFC.BizLogic.EPR.QC manager = new FS.HISFC.BizLogic.EPR.QC();
            this.SetDB(manager);
            return manager.SaveQCInputCondition(al);
        }



        //{2F7319BB-AAD6-49da-A1D7-F67E4DD5253B}
        //public int SetPermission(FS.HISFC.Models.Medical.Permission permission)
        //{
        //    FS.HISFC.BizLogic.Medical.Permission manager = new FS.HISFC.BizLogic.Medical.Permission();
        //    this.SetDB(manager);
        //    return manager.SetPermission(permission);
        //}

        //public int DeletePermission(string id)
        //{
        //    FS.HISFC.BizLogic.Medical.Permission manager = new FS.HISFC.BizLogic.Medical.Permission();
        //    this.SetDB(manager);
        //    return manager.DeletePermission(id);
        //}

        //public System.Collections.ArrayList GetPermissionList()
        //{
        //    FS.HISFC.BizLogic.Medical.Permission manager = new FS.HISFC.BizLogic.Medical.Permission();
        //    this.SetDB(manager);
        //    return manager.GetPermissionList();
        //}

        #endregion

        #region ��Ϣ����
        public System.Collections.ArrayList QueryMessage(string oper)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.QueryMessage(oper);

        }

        public FS.HISFC.Models.Base.Message QueryMessageById(string id)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.QueryMessageById(id);

        }

        public int InsertMessage(FS.HISFC.Models.Base.Message message)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.InsertMessage(message);

        }
        public int UpdateMessage(FS.HISFC.Models.Base.Message message)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.UpdateMessage(message);

        }

        public int DeleteMessage(string id)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.DeleteMessage(id);

        }
        public System.Collections.ArrayList QueryEmrId(string InpatientNo)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.QueryEmrId(InpatientNo);

        }
        public int UpdateMessage(int type,string eprid)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.UpdateMessage(type,eprid);

        }


        #endregion

        #region ��������
        /// <summary>
        /// ��û�������
        /// �������
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList QueryBaseDepartment()
        {
            FS.HISFC.BizLogic.EPR.BaseDept manager = new FS.HISFC.BizLogic.EPR.BaseDept();
            this.SetDB(manager);
            return manager.QueryDepartment();
        }

        #endregion


        #region ���Ӳ���ͨ������
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <returns>����ʱ���ַ�������ΪError,��ϵͳ����δ����</returns>
        public string GetControlArgument(string ctlID)
        {
            FS.HISFC.BizLogic.EPR.EMR emrManager = new FS.HISFC.BizLogic.EPR.EMR();

            this.SetDB(emrManager);

            return emrManager.GetControlArgument(ctlID);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public virtual int SaveSetting(FS.FrameWork.Models.NeuObject obj, string xml)
        {
            string sqlInsert = "INSERT INTO emr_setting	(id,name,memo) VALUES('{0}','{1}','{2}')		";
            string sqlDelete = "delete emr_setting where id='{0}'";
            string update = string.Format("update emr_setting set xml=:a where id='{0}'", obj.ID);
            FS.FrameWork.Management.DataBaseManger manager = new FS.FrameWork.Management.DataBaseManger();
            this.SetDB(manager);
            manager.ExecNoQuery(sqlDelete, obj.ID);
            manager.ExecNoQuery(sqlInsert, obj.ID, obj.Name, obj.Memo);
            return manager.InputLong(update, xml);
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual string GetSetting(string id)
        {
            string sql = "select xml from emr_setting where id='{0}'";
            sql = string.Format(sql, id);
            FS.FrameWork.Management.DataBaseManger manager = new FS.FrameWork.Management.DataBaseManger();
            this.SetDB(manager);
            return manager.ExecSqlReturnOne(sql);
        }
        #endregion

        #region �ճ�
        /// <summary>
        /// ��ѯȫ���ճ�
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList QueryCalendar()
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.QueryCalendar();

        }
        /// <summary>
        /// �����ճ�
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public int AddCalender(FS.HISFC.Models.Base.Calendar calendar)
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.InsertCalendar(calendar);

        }
        public System.Collections.ArrayList QueryCalendar(DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.QueryCalendar(dtBegin, dtEnd);

        }
        public int DeleteCalendar(string id)
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.DeleteCalendar(id);

        }

        #endregion

        #region �����¼
        public System.Collections.ArrayList QueryNurseSheetSettingList()
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.QueryNurseSheetSettingList();
        }

        public FS.HISFC.Models.Base.Message QueryNurseSheetSettingByID(string ID)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.QueryNurseSheetSettingByID(ID);
        }


        public FS.HISFC.Models.Base.Message QueryNurseSheetSettingByName(string Name)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.QueryNurseSheetSettingByName(Name);
        }

        public int InsertNurseSheetSetting(FS.HISFC.Models.Base.Message obj)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.InsertNurseSheetSetting(obj);
        }


        public int DeleteNurseSheetSetting(string ID)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.DeleteNurseSheetSetting(ID);
        }

        public int UpdateNurseSheetSetting(FS.HISFC.Models.Base.Message obj)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.UpdateNurseSheetSetting(obj);
        }

        public string GetXmlFromNurseSheetSetting(FS.HISFC.Models.EPR.NurseSheetSetting obj)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.GetXmlFromNurseSheetSetting(obj);
        }

        public void SetNurseSheetingSetting(string strXml, FS.HISFC.Models.EPR.NurseSheetSetting obj)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            nurseTendManager.SetNurseSheetingSetting(strXml, obj);
        }
        public ArrayList GetStringList(string strText, int maxCount, System.Drawing.Font font)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.GetStringList(strText, maxCount, font);
        }
        public System.Xml.XmlElement SetInnerText(Control panel, System.Xml.XmlDocument doc)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.SetInnerText(panel, doc);
        }
        public void GetInnerText(Control panel, string strInnerText)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            nurseTendManager.GetInnerText(panel, strInnerText);
        }
        public string GetString(string innerText, string sNode)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            return nurseTendManager.GetString(innerText, sNode);
        }
        #endregion

        #region ��
        public bool IsEMRLocked(string patientid, string fileID, ref FS.FrameWork.Models.NeuObject obj)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.IsEMRLocked(patientid, fileID, ref obj);
        }

        public int SetEMRLocked(FS.HISFC.Models.File.DataFileInfo dfi, FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject obj, bool isLocked)//(FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Object.NeuObject obj, bool isLocked)
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.SetEMRLocked(dfi, patient, obj, isLocked);
        }

        public System.Data.DataSet QueryEMRLocked()
        {
            FS.HISFC.BizLogic.EPR.EMR manager = new FS.HISFC.BizLogic.EPR.EMR();
            this.SetDB(manager);
            return manager.QueryEMRLocked();
        }
        #endregion

        #region ��д�淶


        public ArrayList QueryAllCatalog()
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.QueryAllCatalog();
        }

        public ArrayList QueryCatalogByDeptCode(string deptCode)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.QueryCatalogByDeptCode(deptCode);
        }

        public FS.HISFC.Models.EPR.CaseWriteRule GetCatalogByID(string ruleId)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.GetCatalogByID(ruleId);
        }


        /// <summary>
        /// ͨ��Ŀ¼���Ƶõ�ĳ��Ŀ¼
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryCatalogByName(string ruleName)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.QueryCatalogByName(ruleName);
        }

        public int DeleteRule(FS.HISFC.Models.EPR.CaseWriteRule rule, bool deleteChildren)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.DeleteRule(rule, deleteChildren);
        }

        public int ModifyForDrag(FS.HISFC.Models.EPR.CaseWriteRule rule, string newparent)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.ModifyForDrag(rule, newparent);
        }

        public int ModifyRule(FS.HISFC.Models.EPR.CaseWriteRule rule)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.ModifyRule(rule);
        }

        public int InsertRule(FS.HISFC.Models.EPR.CaseWriteRule rule)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.InsertRule(rule);
        }
        public FS.HISFC.Models.EPR.CaseWriteRule GetRule(string id)
        {
            FS.HISFC.BizLogic.EPR.CaseWriteRule cwrManager = new FS.HISFC.BizLogic.EPR.CaseWriteRule();
            this.SetDB(cwrManager);
            return cwrManager.GetRule(id);
        }
        public void AdjustLineSpace(RichTextBox rc, double times)
        {
            FS.HISFC.BizLogic.EPR.NurseTend nurseTendManager = new FS.HISFC.BizLogic.EPR.NurseTend();

            this.SetDB(nurseTendManager);

            nurseTendManager.AdjustLineSpace(rc, times);
        }

        public string GetRuleSequence()
        {
            FS.FrameWork.Management.DataBaseManger manager = new FS.FrameWork.Management.DataBaseManger();
            this.SetDB(manager);

            return manager.GetSequence("EPR.CaseWriteRule.GetRuleCodeSequence");
        }
        #endregion

        #region �ϼ��޸ĺۼ�
        /// <summary>
        /// �����ϼ��޸ĺۼ�
        /// </summary>
        /// <param name="supermark">Ȩ��ʵ��</param>
        /// <param name="img">�޸ĺۼ�</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int SetSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            //����
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.SetSuperMark(supermark, img);
        }
        /// <summary>
        /// �޸�һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int UpdateSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.UpdateSuperMark(supermark, img);
        }
        /// <summary>
        /// ɾ��һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int DeleteSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.DeleteSuperMark(supermark, img);
        }
        /// <summary>
        /// ����һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int InsertSuperMark(FS.FrameWork.Models.NeuObject supermark, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.InsertSuperMark(supermark, img);
        }

        /// <summary>
        /// ����ϼ��޸ĺۼ�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetSuperMark(FS.FrameWork.Models.NeuObject obj)
        {
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.GetSuperMark(obj);
        }

        /// <summary>
        /// ����ϼ��޸ĺۼ�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] GetSuperMarkImage(FS.FrameWork.Models.NeuObject obj)
        {
            FS.HISFC.BizLogic.EPR.SuperMark supermarkManager = new FS.HISFC.BizLogic.EPR.SuperMark();

            this.SetDB(supermarkManager);
            return supermarkManager.GetSuperMarkImage(obj);
        }
        #endregion �ϼ��޸ĺۼ�
        #region ��ӡҳ
        /// <summary>
        /// �����ӡҳ
        /// </summary>
        /// <param name="printPage">Ȩ��ʵ��</param>
        /// <param name="img">�޸ĺۼ�</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int SetPrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            //����
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.SetPrintPage(printPage, img);
        }
        /// <summary>
        /// �޸�һ����ӡҳ
        /// </summary>
        /// <returns></returns>
        public int UpdatePrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.UpdatePrintPage(printPage, img);
        }
        /// <summary>
        /// ɾ��һ����ӡҳ
        /// </summary>
        /// <returns></returns>
        public int DeletePrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.DeletePrintPage(printPage, img);
        }
        /// <summary>
        /// ����һ����ӡҳ
        /// </summary>
        /// <returns></returns>
        public int InsertPrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.InsertPrintPage(printPage, img);
        }

        /// <summary>
        /// ��ô�ӡҳ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public FS.HISFC.Models.EPR.EPRPrintPage GetPrintPage(FS.HISFC.Models.EPR.EPRPrintPage obj)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.GetPrintPage(obj);
        }

        /// <summary>
        /// ��ô�ӡҳ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ArrayList GetPrintPageList(FS.HISFC.Models.EPR.EPRPrintPage obj)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.GetPrintPageList(obj);
        }

        /// <summary>
        /// ��ô�ӡҳͼƬ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public byte[] GetPrintPageImage(FS.HISFC.Models.EPR.EPRPrintPage obj)
        {
            FS.HISFC.BizLogic.EPR.PrintPage printPageManager = new FS.HISFC.BizLogic.EPR.PrintPage();

            this.SetDB(printPageManager);
            return printPageManager.GetPrintPageImage(obj);
        }

        #endregion ��ӡҳ

        #region �ʿ�ҹ��ͳ��
        /// <summary>
        /// ���ݻ���ID����ͳ�ƽ��
        /// </summary>
        /// <param name="patientNo">����ID</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Object.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryQCStatByPatientNO(string patientNO)
        {
            FS.HISFC.BizLogic.EPR.QCStat manager = new FS.HISFC.BizLogic.EPR.QCStat();
            this.SetDB(manager);
            return manager.QueryByPatienNo(patientNO);
        }
        /// <summary>
        /// ����ͳ��ʱ�����ͳ�ƽ��
        /// </summary>
        /// <param name="beginDate">ͳ��ʱ����ʼʱ��</param>
        /// <param name="endDate">ͳ��ʱ����ֹʱ��</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Object.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryQCStatByStatDate(DateTime beginDate,DateTime endDate)
        {
            FS.HISFC.BizLogic.EPR.QCStat manager = new FS.HISFC.BizLogic.EPR.QCStat();
            this.SetDB(manager);
            return manager.QueryByStatDate(beginDate,endDate);
        }
        /// <summary>
        /// ���ݻ�����Ժʱ�����ͳ�ƽ��
        /// </summary>
        /// <param name="beginDate">������Ժʱ����ʼʱ��</param>
        /// <param name="endDate">������Ժʱ����ֹʱ��</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Object.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryQCStatByInDate(DateTime beginDate, DateTime endDate)
        {
            FS.HISFC.BizLogic.EPR.QCStat manager = new FS.HISFC.BizLogic.EPR.QCStat();
            this.SetDB(manager);
            return  manager.QueryByInDate(beginDate, endDate);
        }
        /// <summary>
        /// ����ͳ�ƽ��
        /// </summary>
        /// <param name="result">FS.FrameWork.Object.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</param>
        /// <returns></returns>
        public int InsertQCStat(FS.FrameWork.Models.NeuObject result)
        {
            FS.HISFC.BizLogic.EPR.QCStat manager = new FS.HISFC.BizLogic.EPR.QCStat();
            this.SetDB(manager);
            return manager.Insert(result);
        }
        /// <summary>
        /// ɾ�����ǽ���ͳ�ƽ��
        /// </summary>
        /// <returns></returns>
        public int DeleteQCStat()
        {
            FS.HISFC.BizLogic.EPR.QCStat manager = new FS.HISFC.BizLogic.EPR.QCStat();
            this.SetDB(manager);
            return manager.Delete();
        }
        #endregion
    }
}
