using System;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
using System.Data;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.HealthRecord
{
    /// <summary>
    /// Diagnose ��ժҪ˵����
    /// </summary>
    public class Diagnose : FS.FrameWork.Management.Database
    {
        public Diagnose()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���к���
        #endregion

        #region ���벡�������Ϣ
        /// <summary>
        /// ���벡�������Ϣ
        /// </summary>
        /// <param name="Item">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int InsertDiagnose(FS.HISFC.Models.HealthRecord.Diagnose Item)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Insert.1", ref strSQL) == -1) return -1;
            Item.DiagInfo.HappenNo = GetMaxHappenNum(Item.DiagInfo.Patient.ID);
            Item.IsValid = true;
            string[] strParm = myGetItemParm(Item);
            return this.ExecNoQuery(strSQL, strParm);
        }
        #endregion 

        
        #region ���벡�������Ϣ
        /// <summary>
        /// ���벡�������Ϣ
        /// </summary>
        /// <param name="Item">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int InsertCasDiagnose(FS.HISFC.Models.HealthRecord.Diagnose Item)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Insert.2", ref strSQL) == -1)
            {
                Err = "δ�ҵ�SQL��䣬IDΪCASE.Diagnose.Insert.2!" + Sql.Err;
                return -1;
            }
           // Item.DiagInfo.HappenNo = GetMaxHappenNum(Item.DiagInfo.Patient.ID);
            Item.IsValid = true;
            string[] strParm = myGetItemParm(Item);
            return this.ExecNoQuery(strSQL, strParm);
        }
        #endregion 

        #region ���²��������Ϣ
        /// <summary>
        /// ���²��������Ϣ
        /// </summary>
        /// <param name="dg">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int UpdateDiagnose(FS.HISFC.Models.HealthRecord.Diagnose dg)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Update.1", ref strSQL) == -1) return -1;
            string[] strParm = myGetItemParm(dg);  //ȡ�����б�
            return this.ExecNoQuery(strSQL, strParm);
        }
        /// <summary>
        /// ���²��������Ϣ
        /// </summary>
        /// <param name="dg">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int UpdateDiagnoseForClinic(FS.HISFC.Models.HealthRecord.Diagnose dg)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Update.2", ref strSQL) == -1) return -1;
            string[] strParm = myGetItemParm(dg);  //ȡ�����б�
            return this.ExecNoQuery(strSQL, strParm);
        }
        #endregion 
        #region ���к���

        #region �����޸ģ�wangsc�� {95DF754D-9A34-4692-B232-0EFF41ECB141}

        /// <summary>
        /// ͨ��������ˮ��ȡ���
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList GetHistoryCaseByClinicCode(string clinicCode)
        {
            string where = "";
            if (this.Sql.GetSql("CASE.Diagnose.Select.GetHistoryCaseByCardNO", ref where) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                where = string.Format(where, clinicCode);
            }
            catch
            {
                this.Err = "build string error";
                return null;
            }

            string strsql = this.QuerySql() + "   " + where;
            return this.myQuery(strsql);
        }
        /// <summary>
        /// ��ѯ��ʷ���
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="dsResult"></param>
        public void QueryCaseHistoryByID(string clinicCode, ref DataSet dsResult)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.QueryHistory.2", ref strSQL) == -1)
            {
                this.Err = "Can't Find Sql;CASE.Diagnose.QueryHistory.2";
                return;
            }
            try
            {
                strSQL = string.Format(strSQL, clinicCode);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.QueryHistory.2";
                return;
            }
            this.ExecQuery(strSQL, ref dsResult);
        }

        /// <summary>
        /// ���»��������Ϣ  0��Ч 1��Ч
        /// </summary>
        /// <Editer>xingzhuo</Editer>
        /// <param name="InpatientNO"></param>
        ///  <param name="icdCode"></param>
        ///   <param name="happenno"></param>
        /// <returns></returns>
        public int CancelDiagnoseSingleForClinic(string InpatientNO, string icdCode, string happenno)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.CancelDiagnoseSingleForClinic", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, icdCode, happenno);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.CancelDiagnoseSingleForClinic";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region ɾ��һ�����ߵ����в��������Ϣ
        /// <summary>
        /// ɾ��һ�����ߵ����в��������Ϣ
        /// </summary>
        /// <param name="InpatientNO">string ����סԺ��ˮ��</param>
        /// <param name="OperType">����Ա���ͣ�1 ҽ��¼�� 2 ����¼��</param>
        /// <param name="PersonType">�������ͣ� 0 ����   1 סԺ </param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int DeleteDiagnoseAll(string InpatientNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType,FS.HISFC.Models.Base.ServiceTypes PersonType)
        {
            string temp = "";
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            string personType = string.Empty;
            if (PersonType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                personType = "1";
            }
            else
            {
                personType = "0";
            }
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Delete.1", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, temp,personType);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Delete.1";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion 

        #region ɾ��һ�����ߵĵ��������Ϣ
        ///<summary>
        /// ɾ��һ�����ߵĵ��������Ϣ
        /// </summary>
        /// <param name="InpatientNO">����סԺ��ˮ��</param>
        /// <param name="happenNO">�����ˮ��</param>
        /// <param name="OperType">���� DOC ҽ��վ¼������ ��CAS ������¼������ </param>
        /// <returns></returns>
        public int DeleteDiagnoseSingle(string InpatientNO, int happenNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.Base.ServiceTypes personType)
        {
            string strSQL = "";
            string temp = "";
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
            }
            if (this.Sql.GetSql("CASE.Diagnose.Delete.2", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, happenNO.ToString(), temp, personType == FS.HISFC.Models.Base.ServiceTypes.C ? "0" : "1");
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Delete.2";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion 

        #region ɾ��һ�����ߵĵ��������Ϣ
        ///<summary>
        /// ɾ��һ�����ߵĵ��������Ϣ
        /// </summary>
        /// <param name="patientId">���������</param>
        /// <param name="icdCode">icdcode </param>
        /// <returns></returns>
        public int DeleteDiagnoseSingleForClinic(string patientId, string icdCode, string happenno)
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Delete.3", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, patientId, icdCode, happenno);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Delete.3";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion 

        #region ��ѯ���������Ϣ

        /// <summary>
        /// ����ʹ�ã���ѯ���������Ϣ ֻ��ѯ��Ч���
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="OperType"></param>
        /// <returns></returns>
        public ArrayList QueryCaseDiagnoseForClinic(string patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType)
        {
            return this.QueryCaseDiagnoseForClinicByState(patientId, OperType, true);
        }

        /// <summary>
        /// ����ʹ�ã���ѯ���������Ϣ
        /// </summary>
        /// <param name="patientId">������ˮ��</param>
        /// <param name="OperType">���ҽ��վ��������</param>
        /// <param name="isOnlyValide">ֻ��ѯ��Ч��</param>
        /// <returns></returns>
        public ArrayList QueryCaseDiagnoseForClinicByState(string patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, bool isOnlyValide)
        {
            string strSQL = "";
            string temp = "";
            string sqlID = "";
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }

            if (isOnlyValide)
            {
                sqlID = "CASE.Diagnose.Select.2";
            }
            else
            {
                sqlID = "CASE.Diagnose.Select.All";
            }

            if (this.Sql.GetSql(sqlID, ref strSQL) == -1)
            {
                return null;
            }
            strSQL = MainSql + strSQL;
            try
            {
                strSQL = string.Format(strSQL, patientId, temp);
            }
            catch
            {
                this.Err = "����������ԣ�[" + sqlID + "]";
                return null;
            }

            return this.myQuery(strSQL);
        }


        /// <summary>
        /// ��ò�����ϱ��еĻ��������Ϣ,����Ѿ��в����Ļ��߲�ѯ 
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="diagType">������� ������ϣ���Ժ��ϵ� ��ѯ���еĿ������� %</param>
        /// <param name="OperType">"DOC"��ѯҽ��վ¼��������Ϣ ��CAS" ��ѯ������¼��������Ϣ</param>
        /// <param name="PerssonType">�������� 1 סԺ  0 ����</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryCaseDiagnose(string InpatientNO, string diagType, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.Base.ServiceTypes PerssonType)
        {
            string strSQL = "";
            string temp = "";
            string personType = string.Empty;
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            if (PerssonType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                personType = "1";
            }
            else
            {
                personType = "0";
            }
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }
            if (this.Sql.GetSql("CASE.Diagnose.Select.1", ref strSQL) == -1) return null;
            strSQL = MainSql + strSQL;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, diagType, temp,personType);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Select.1";
                return null;
            }

            return this.myQuery(strSQL);
        }
        /// <summary>
        /// ��ѯ�����
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ�� </param>
        /// <param name="IsMain">true ����� false  �������</param>
        /// <param name="OperType">DOC ��ѯҽ��¼������,CAS ��ѯ���������������</param>
        /// <returns></returns>
        public ArrayList QueryMainDiagnose(string InpatientNO, bool IsMain, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType)
        {
            string strSQL = "";
            string MainFlag = "";
            if (IsMain)
            {
                MainFlag = "1";//�����
            }
            else
            {
                MainFlag = "0";//������� 
            }
            string temp = "";
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }
            if (this.Sql.GetSql("CASE.Diagnose.QueryMainDiagnose", ref strSQL) == -1) return null;
            strSQL = MainSql + strSQL;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, MainFlag, temp);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Select.1";
                return null;
            }

            return this.myQuery(strSQL);
        }
        #endregion 

        #region ��ȡ���ķ������
        /// <summary>
        /// ��ȡ���ķ������
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <returns></returns>
        public int GetMaxHappenNum(string InpatientNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("CASE.Diagnose.GetMaxHappenNum", ref strSQL) == -1) return -1;

            try
            {
                strSQL = string.Format(strSQL, InpatientNo);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.GetMaxHappenNum";
                return -1;
            }

            //�������ķ������
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSQL));
        }
        #endregion 

        #region ˽�к��� 
        private string QuerySql()
        {
            string strSQL = "";
            if (this.Sql.GetSql("CASE.Diagnose.Select.QuerySql", ref strSQL) == -1) return null;
            return strSQL;
        }
        #endregion 
     
        #region �ж���������Ƿ�Ϻ�����
        /// <summary>
        /// �ж���������Ƿ�Ϻ����� 
        /// �������  ���� null
        /// ��ȫ�Ϻ�����,û����ʾ list.Count == 0 
        ///  User01 =1 ������©�ĸ������ ,��������
        ///  User01 = 2 ���ڿ�����©����� ,����ɲ���
        /// </summary>
        /// <param name="list">Ҫ������Ŀ�б�</param>
        /// <param name="SexType">�Ա�</param>
        /// <returns> �������  ���� null ;list.Count == 0 ��ȫ�Ϻ�����,û����ʾ ;User01 =1  ȱ�ٱ������ϻ���ϴ��� ������ͨ��;User01 = 2 ���ڿ�����©����� ,����ɲ��� </returns>
        public ArrayList QueryDiagnoseValueState(ArrayList list, FS.HISFC.Models.Base.EnumSex SexType)
        {
            if (list == null)
            {
                return null;
            }
            ArrayList SexTypeList = null;
            ICD icd = new ICD();
            //�ж���Ů������Ƿ����
            if (SexType.ToString() == "M")
            {
                //��ѯר����Ů�Ե�����б�
                SexTypeList = icd.QueryDiagnoseBySex("F");
                if (SexTypeList == null)
                {
                    return null;
                }
            }
            else if (SexType.ToString() == "F")
            {
                //��ѯר�������Ե���� �б�
                SexTypeList = icd.QueryDiagnoseBySex("M");
                if (SexTypeList == null)
                {
                    this.Err = "��ȡ��������б����";
                    return null;
                }
            }
            //���ص�����
            ArrayList Returnlist = new ArrayList();
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.ICD10.ID.Length < 3)
                {
                    continue;
                }
                if (obj.DiagInfo.ICD10.ID.Length >= 6) //������̬ѧ���� 
                {
                    string str = obj.DiagInfo.ICD10.ID.Substring(0, 6);
                    if (str.CompareTo("M80000") >= 0 && str.CompareTo("M99890") <= 0)
                    {
                        continue;
                    }
                }
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                string strCode = obj.DiagInfo.ICD10.ID.Substring(0, 3);

                if (strCode.CompareTo("A00") >= 0 && strCode.CompareTo("R99") <= 0)
                {
                    bool boolTemp = true;
                    //�ж�list���Ƿ��� V��W��X��Y��ͷ��ʾ���˻��ж���������  ���û�� ��ӵ���ʾ������ 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 1);
                        if (strFirst.CompareTo("V") == 0 || strFirst.CompareTo("W") == 0 || strFirst.CompareTo("X") == 0 || strFirst.CompareTo("Y") == 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp) //���û�� V��W��X��Y��ͷ��ʾ���˻��ж���������
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "1"; //������Ҫ��ӵ� 
                        info.User02 = "������Ҫ���V��W��X��Y��ͷ��ʾ���˻��ж���������";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("S00") >= 0 && strCode.CompareTo("T98") <= 0)
                {
                    bool boolTemp = true;
                    //�ж�list���Ƿ��� V��W��X��Y��ͷ��ʾ���˻��ж���������  ���û�� ��ӵ���ʾ������ 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 1);
                        if (strFirst.CompareTo("V") == 0 || strFirst.CompareTo("W") == 0 || strFirst.CompareTo("X") == 0 || strFirst.CompareTo("Y") == 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = "�������V��W��X��Y��ͷ��ʾ���˻��ж���������";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                if (strCode.CompareTo("C00") >= 0 && strCode.CompareTo("D48") <= 0)
                {
                    if (strCode.CompareTo("C00") >= 0 && strCode.CompareTo("C96") <= 0)
                    {
                        bool boolTemp = true;
                        //C00 - C96 ��ʾ����M80000/3 - M99890/3
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/3") >= 0 && strFirst.CompareTo("M99890/3") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/3 - M99890/3 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }

                    }
                    if (strCode.CompareTo("C77") >= 0 && strCode.CompareTo("C79") <= 0)
                    {
                        bool boolTemp = true;
                        //C77 - C79 ��ʾ����M80000/6 - M99890/6 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/6") >= 0 && strFirst.CompareTo("M99890/6") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/6 - M99890/6 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D00") >= 0 && strCode.CompareTo("D09") <= 0)
                    {
                        bool boolTemp = true;
                        //D00 - D09 ��ʾ����M80000/2 - M99890/2 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/2") >= 0 && strFirst.CompareTo("M99890/2") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/2 - M99890/2 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D10") >= 0 && strCode.CompareTo("D36") <= 0)
                    {
                        bool boolTemp = true;
                        //D10- D36 ��ʾ���� M80000/0 - M99890/0 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/0") >= 0 && strFirst.CompareTo("M99890/0") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/0 - M99890/0 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D37") >= 0 && strCode.CompareTo("D48") <= 0)
                    {
                        bool boolTemp = true;
                        //D37- D48 ��ʾ����M80000/1 - M99890/1 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/1") >= 0 && strFirst.CompareTo("M99890/1") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽��M80000/1 - M99890/1 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                else if (strCode.CompareTo("080") == 0 && obj.MainFlag == "1")
                {
                    // �����080 ������� , 010 - 099 ���ܳ����ڸ��������
                    bool boolTemp = true;
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("010") >= 0 && strFirst.CompareTo("099") <= 0 && obj.MainFlag == "0")
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = "080 ������� , 010 - 099 ���ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("010") >= 0 && strCode.CompareTo("099") >= 0 && obj.MainFlag == "1")
                {
                    bool boolTemp = true;
                    // ����ǡ�010 - 099��������� , 080�����ܳ����ڸ��������
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("080") >= 0 && obj.MainFlag == "0")
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = " 010 - 099��������� , 080�����ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                //�ж���Ů������Ƿ����
                if (SexType.ToString() == "M")
                {
                    //�ж��Ƿ���
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in SexTypeList)
                    {
                        if (temp.DiagInfo.ICD10.ID == obj.DiagInfo.ICD10.ID)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = " Ů�Ե���ϲ�����������  ";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                else if (SexType.ToString() == "F")
                {
                    //�ж��Ƿ���
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in SexTypeList)
                    {
                        if (temp.DiagInfo.ICD10.ID == obj.DiagInfo.ICD10.ID)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = " ���Ե���ϲ�������Ů��  ";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                if (strCode.CompareTo("010") >= 0 && strCode.CompareTo("099") <= 0)
                {
                    bool boolTemp = true;
                    //���� 010  - 099 ���븽��Z31 - Z37 ���� 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("Z31") >= 0 && strFirst.CompareTo("Z37") <= 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = " 010 - 099��������� , 080�����ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("003") >= 0 && strCode.CompareTo("008") <= 0)
                {
                    //���Ӥ��������������22����Ҫ��ʾҽ������ P95����
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "1"; //��ѡ�� 
                    info.User02 = " ���Ӥ��������������22��,��Ҫ���� P95����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
                else if (strCode.CompareTo("B95") >= 0 && strCode.CompareTo("B97") <= 0 && obj.MainFlag == "1")
                {
                    //B95 - B97 ������Ϊ��Ժ����� 
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "2"; //������ӵ� 
                    info.User02 = " B95 - B97 ������Ϊ��Ժ�����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
                else if (strCode.CompareTo("J98.401") == 0)
                {
                    //��ʾ����ϸ��ѧ��̵���� �������������Ҫ���� J12 - J17 �������� 
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "1"; //��ѡ�� 
                    info.User02 = " ���ϸ��ѧ��̵�����ҽ��������,��Ҫ����J12 - J17����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
            }
            return Returnlist;
        }
        #endregion 

        #region ��ȡ��һ���
        /// <summary>
        /// ��ȡ��һ���
        /// </summary>
        /// <param name="InpatienNo"></param>
        /// <param name="diagType"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Diagnose GetFirstDiagnose(string InpatienNo, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType diagType, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            string strSql1 = "";
            FS.HISFC.Models.HealthRecord.Diagnose info = new FS.HISFC.Models.HealthRecord.Diagnose();
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }
            try
            {
                ArrayList list = new ArrayList();
                if (this.Sql.GetSql("CASE.Diagnose.GetFirstDiagnose.FIRST", ref strSql1) == -1) return null;
                strSql1 = MainSql + strSql1;
                string str = "";
                if (frmType == frmTypes.CAS)
                {
                    str = "2";
                }
                else if (frmType == frmTypes.DOC)
                {
                    str = "1";
                }
                //��ȡ����ֵ
                int i = (int)diagType;
                strSql1 = string.Format(strSql1, InpatienNo, i.ToString(), str);
                list = myQuery(strSql1);
                if (list == null)
                {
                    return null;
                }
                if (list.Count > 0)
                {
                    info = (FS.HISFC.Models.HealthRecord.Diagnose)list[0];
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return info;
        }

        #endregion

        #region ˽�к���

        /// <summary>
        /// ��ʵ���л�ȡ�����γ�����
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private string[] myGetItemParm(FS.HISFC.Models.HealthRecord.Diagnose Item)
        {

            string[] strParm = new string[26];
            string IsMain = "";
            if (Item.DiagInfo.IsMain)
            {
                IsMain = "1";
            }
            else
            {
                IsMain = "0";
            }
            strParm[0] = Item.DiagInfo.Patient.ID; // סԺ��ˮ��
            strParm[1] = Item.DiagInfo.HappenNo.ToString(); //�������
            strParm[2] = Item.DiagInfo.DiagType.ID; // ������� 
            strParm[3] = Item.LevelCode;//��ϼ��� 
            strParm[4] = Item.PeriorCode;// ��Ϸ��� 
            strParm[5] = Item.DiagInfo.ICD10.ID;// ���ICD��        
            strParm[6] = Item.DiagInfo.ICD10.Name;// �������        
            strParm[7] = Item.DiagInfo.DiagDate.ToString();// �������
            strParm[8] = Item.DiagInfo.Doctor.ID;// ҽʦ����
            strParm[9] = Item.DiagInfo.Doctor.Name;// ҽʦ����(���)
            strParm[10] = Item.Pvisit.InTime.ToString();//��Ժ���� 
            strParm[11] = Item.Pvisit.OutTime.ToString();//��Ժ���� 
            strParm[12] = Item.DiagOutState;//������� 0 ����1 ��ת 2 δ��3 ���� 4 ����
            strParm[13] = Item.SecondICD;//�ڶ�ICD    
            strParm[14] = Item.SynDromeID;// ����֢���   
            strParm[15] = Item.CLPA;//�������
            strParm[16] = Item.DubDiagFlag;//�Ƿ�����     
            strParm[17] = IsMain;//�Ƿ������ 1 ����� 0 �������             
            strParm[18] = Item.Memo;//��ע  
            strParm[19] = Item.ID;//����Ա
            strParm[20] = Item.OperType;//��� 1 ҽ��վ¼�����  2 ������¼�����
            strParm[21] = Item.OperationFlag;// ������־ 1 ������ 0 û������  
            strParm[22] = Item.Is30Disease; //�Ƿ���30�ּ���
            strParm[23] = Item.PerssonType == FS.HISFC.Models.Base.ServiceTypes.I ? "1" : "0"; //������� 0 ���ﻼ�� 1 סԺ����
            strParm[24] = Item.IsValid ? "1" : "0"; //�Ƿ���Ч
            strParm[25] = Item.Diagnosis_flag;   //�Ƿ����
            //strParm[26] = Item.DiagInfo.ICDF10.ID;//������ϱ���
            //strParm[27] = Item.DiagInfo.ICDF10.Name;//�����������
            return strParm;
        }

        /// <summary>
        /// �� reader�ж�ȡ����
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private ArrayList myQuery(string strSQL)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.Diagnose dg;

            this.ExecQuery(strSQL);

            try
            {
                while (this.Reader.Read())
                {
                    dg = new FS.HISFC.Models.HealthRecord.Diagnose();

                    dg.DiagInfo.Patient.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//סԺ��ˮ��
                    dg.DiagInfo.Patient.PID.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();
                    dg.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1] == DBNull.Value ? "0": this.Reader[1].ToString());//�������
                    dg.DiagInfo.DiagType.ID = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//�������
                    dg.LevelCode = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//��ϼ���
                    dg.PeriorCode = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//��Ϸ���
                    dg.DiagInfo.ICD10.ID = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//���ICD��
                    dg.DiagInfo.ICD10.Name = this.Reader[6] == DBNull.Value ? string.Empty : this.Reader[6].ToString();//�������
                    dg.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7] == DBNull.Value ? "0001-01-01" : this.Reader[7].ToString());//�������
                    dg.DiagInfo.Doctor.ID = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();//ҽʦ����
                    dg.DiagInfo.Doctor.Name = this.Reader[9] == DBNull.Value ? string.Empty : this.Reader[9].ToString();//ҽʦ����
                    dg.Pvisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10] == DBNull.Value ? "0001-01-01" : this.Reader[10].ToString());//��Ժ����
                    dg.Pvisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11] == DBNull.Value ? "0001-01-01" : this.Reader[11].ToString());//��Ժ����
                    dg.DiagOutState = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();
                    dg.SecondICD = this.Reader[13] == DBNull.Value ? string.Empty : this.Reader[13].ToString();//�ڶ�ICD
                    dg.SynDromeID = this.Reader[14] == DBNull.Value ? string.Empty : this.Reader[14].ToString();//����֢���
                    dg.CLPA = this.Reader[15] == DBNull.Value ? string.Empty : this.Reader[15].ToString();//�������
                    dg.DubDiagFlag = this.Reader[16] == DBNull.Value ? string.Empty : this.Reader[16].ToString();//�Ƿ�����
                    dg.DiagInfo.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[17] == DBNull.Value ? "0" : this.Reader[17].ToString());//�Ƿ������
                    dg.Memo = this.Reader[18] == DBNull.Value ? string.Empty : this.Reader[18].ToString();//��ע
                    dg.ID = this.Reader[19] == DBNull.Value ? string.Empty : this.Reader[19].ToString();//����Ա
                    dg.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20] == DBNull.Value ? "0001-01-01" : this.Reader[20].ToString());//����ʱ��
                    dg.OperType = this.Reader[21] == DBNull.Value ? string.Empty : this.Reader[21].ToString(); // 1 ҽ��վ¼�� 2 ������¼��
                    dg.OperationFlag = this.Reader[22] == DBNull.Value ? string.Empty : this.Reader[22].ToString(); //������־
                    dg.Is30Disease = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString(); // 30�ּ���

                    dg.PerssonType = this.Reader[24].ToString() == "0" ? FS.HISFC.Models.Base.ServiceTypes.C : FS.HISFC.Models.Base.ServiceTypes.I;//������� 0 ���ﻼ�� 1 סԺ����
                    dg.IsValid = this.Reader[25].ToString() == "0" ? false : true; //��Ч��

                    if (Reader.FieldCount > 26)
                    {
                        dg.Diagnosis_flag = this.Reader[26] == DBNull.Value ? string.Empty : this.Reader[26].ToString();  //�Ƿ���
                    }
                   
                    al.Add(dg);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = "��ò��������Ϣ����![com_cas_diagnose]" + ex.Message;
                this.WriteErr();
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }


        /// <summary>
        /// �� reader�ж�ȡ����
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.HealthRecord.Diagnose> MyQuery(string strSQL)
        {
            List<FS.HISFC.Models.HealthRecord.Diagnose> al = new List<FS.HISFC.Models.HealthRecord.Diagnose>();
            FS.HISFC.Models.HealthRecord.Diagnose dg;
            this.ExecQuery(strSQL);
            try
            {
                while (this.Reader.Read())
                {
                    dg = new FS.HISFC.Models.HealthRecord.Diagnose();

                    dg.DiagInfo.Patient.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//סԺ��ˮ��
                    dg.DiagInfo.Patient.PID.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();
                    dg.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1] == DBNull.Value ? "0" : this.Reader[1].ToString());//�������
                    dg.DiagInfo.DiagType.ID = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//�������
                    dg.LevelCode = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//��ϼ���
                    dg.PeriorCode = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();//��Ϸ���
                    dg.DiagInfo.ICD10.ID = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//���ICD��
                    dg.DiagInfo.ICD10.Name = this.Reader[6] == DBNull.Value ? string.Empty : this.Reader[6].ToString();//�������
                    dg.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7] == DBNull.Value ? "0001-01-01" : this.Reader[7].ToString());//�������
                    dg.DiagInfo.Doctor.ID = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();//ҽʦ����
                    dg.DiagInfo.Doctor.Name = this.Reader[9] == DBNull.Value ? string.Empty : this.Reader[9].ToString();//ҽʦ����
                    dg.Pvisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10] == DBNull.Value ? "0001-01-01" : this.Reader[10].ToString());//��Ժ����
                    dg.Pvisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11] == DBNull.Value ? "0001-01-01" : this.Reader[11].ToString());//��Ժ����
                    dg.DiagOutState = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();
                    dg.SecondICD = this.Reader[13] == DBNull.Value ? string.Empty : this.Reader[13].ToString();//�ڶ�ICD
                    dg.SynDromeID = this.Reader[14] == DBNull.Value ? string.Empty : this.Reader[14].ToString();//����֢���
                    dg.CLPA = this.Reader[15] == DBNull.Value ? string.Empty : this.Reader[15].ToString();//�������
                    dg.DubDiagFlag = this.Reader[16] == DBNull.Value ? string.Empty : this.Reader[16].ToString();//�Ƿ�����
                    dg.DiagInfo.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[17] == DBNull.Value ? "0" : this.Reader[17].ToString());//�Ƿ������
                    dg.Memo = this.Reader[18] == DBNull.Value ? string.Empty : this.Reader[18].ToString();//��ע
                    dg.ID = this.Reader[19] == DBNull.Value ? string.Empty : this.Reader[19].ToString();//����Ա
                    dg.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20] == DBNull.Value ? "0001-01-01" : this.Reader[20].ToString());//����ʱ��
                    dg.OperType = this.Reader[21] == DBNull.Value ? string.Empty : this.Reader[21].ToString(); // 1 ҽ��վ¼�� 2 ������¼��
                    dg.OperationFlag = this.Reader[22] == DBNull.Value ? string.Empty : this.Reader[22].ToString(); //������־
                    dg.Is30Disease = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString(); // 30�ּ���

                    dg.PerssonType = this.Reader[24].ToString() == "0" ? FS.HISFC.Models.Base.ServiceTypes.C : FS.HISFC.Models.Base.ServiceTypes.I;//������� 0 ���ﻼ�� 1 סԺ����
                    dg.IsValid = this.Reader[25].ToString() == "0" ? false : true; //��Ч��

                    if (Reader.FieldCount > 26)
                    {
                        dg.Diagnosis_flag = this.Reader[26] == DBNull.Value ? string.Empty : this.Reader[26].ToString();  //�Ƿ���
                    }
                    al.Add(dg);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = "��ò��������Ϣ����![com_cas_diagnose]" + ex.Message;
                this.WriteErr();
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }
        #endregion

        /// <summary>
        /// �жϸ�����Ƿ��Ǵ�Ⱦ�����
        /// </summary>
        /// <param name="ICDCode"></param>
        /// <returns></returns>
        public string IsInfect(string ICDCode)
        {
            string strSql = "";
            string result = "";
            if (this.Sql.GetSql("Case.Diagnose.IsInfect", ref strSql) == -1)
            {
                return "Error";
            }
            strSql = string.Format(strSql, ICDCode);
            if (this.ExecQuery(strSql) == -1)
                return "Error";
            while (this.Reader.Read())
            {
                try
                {
                    result = this.Reader[0].ToString();
                }
                catch
                {
                    return "Error";
                }
            }
            return result;
        }
        #endregion

        //{8BC09475-C1D9-4765-918B-299E21E04C74} ���¼������ҽ��վ������ҽ��վ������������
        #region ҽ��վ¼������� ���met_com_diagnose

        /// <summary>
        /// ��ѯ�������,�������������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <returns></returns> 
        public ArrayList QueryDiagnoseNoOps(string InPatientNo)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.1
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null)
                return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.5", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.5�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo);
            return this.myPatientQuery(sql1);
        }

        /// ��ѯ���������Ϣ��select��䣨��where������
        private string PatientQuerySelect()
        {
            #region �ӿ�˵��
            //RADT.Diagnose.DiagnoseQuery.select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("RADT.Diagnose.DiagnoseQuery.select.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.DiagnoseQuery.select.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        //˽�к�������ѯ���߻�����Ϣ 
        private ArrayList myPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.Diagnose Diagnose;
            this.ProgressBarText = "���ڲ�ѯ�������...";
            this.ProgressBarValue = 0;

            this.ExecQuery(SQLPatient);
            try
            {
                while (this.Reader.Read())
                {
                    Diagnose = new FS.HISFC.Models.HealthRecord.Diagnose();
                    Diagnose.DiagInfo.Patient.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();// סԺ��ˮ��

                    Diagnose.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1] == DBNull.Value ? "0" : this.Reader[1].ToString());//  �������

                    Diagnose.DiagInfo.Patient.PID.CardNO = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//������

                    Diagnose.DiagInfo.DiagType.ID = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//������
                    //FS.HISFC.Models.HealthRecord.DiagnoseType diagnosetype = new FS.HISFC.Models.HealthRecord.DiagnoseType();
                    //diagnosetype.ID = Diagnose.DiagType.ID;
                    //Diagnose.DiagType.Name = diagnosetype.Name;//���������� zjy

                    Diagnose.DiagInfo.ID = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();		//��ϴ���
                    Diagnose.DiagInfo.ICD10.ID = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();
                    Diagnose.DiagInfo.Name = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();		//�������
                    Diagnose.DiagInfo.ICD10.Name = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();

                    Diagnose.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6] == DBNull.Value ? "0001-01-01" : this.Reader[6].ToString());

                    Diagnose.DiagInfo.Doctor.ID = this.Reader[7] == DBNull.Value ? string.Empty : this.Reader[7].ToString();

                    Diagnose.DiagInfo.Doctor.Name = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();

                    Diagnose.DiagInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9] == DBNull.Value ? 0 : this.Reader[9]);

                    Diagnose.DiagInfo.Dept.ID = this.Reader[10] == DBNull.Value ? string.Empty : this.Reader[10].ToString();

                    Diagnose.DiagInfo.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11] == DBNull.Value ? 0 : this.Reader[11]);

                    Diagnose.DiagInfo.Memo = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();

                    Diagnose.DiagInfo.User01 = this.Reader[13] == DBNull.Value ? string.Empty : this.Reader[13].ToString();
                    Diagnose.DiagInfo.User02 = this.Reader[14] == DBNull.Value ? string.Empty : this.Reader[14].ToString();

                    //�������
                    Diagnose.DiagInfo.OperationNo = this.Reader[15] == DBNull.Value ? string.Empty : this.Reader[15].ToString();

                    al.Add(Diagnose);
                }
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û��������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            this.ProgressBarValue = -1;
            return al;
        }

        #region ˽�к�������ѯ���߻�����Ϣ -- ����д����
        //˽�к�������ѯ���߻�����Ϣ
        //private ArrayList myPatientQuery(string SQLPatient)
        //{
        //    ArrayList al = new ArrayList();
        //    FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose;
        //    this.ProgressBarText = "���ڲ�ѯ�������...";
        //    this.ProgressBarValue = 0;

        //    this.ExecQuery(SQLPatient);
        //    try
        //    {
        //        while (this.Reader.Read())
        //        {
        //            Diagnose = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
        //            Diagnose.Patient.ID = this.Reader[0].ToString();// סԺ��ˮ��

        //            Diagnose.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//  �������

        //            Diagnose.Patient.PID.CardNO = this.Reader[2].ToString();//������

        //            Diagnose.DiagType.ID = this.Reader[3].ToString();//������
        //            //FS.HISFC.Models.HealthRecord.DiagnoseType diagnosetype = new FS.HISFC.Models.HealthRecord.DiagnoseType();
        //            //diagnosetype.ID = Diagnose.DiagType.ID;
        //            //Diagnose.DiagType.Name = diagnosetype.Name;//���������� zjy

        //            Diagnose.ID = this.Reader[4].ToString();		//��ϴ���
        //            Diagnose.ICD10.ID = this.Reader[4].ToString();
        //            Diagnose.Name = this.Reader[5].ToString();		//�������
        //            Diagnose.ICD10.Name = this.Reader[5].ToString();

        //            Diagnose.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());

        //            Diagnose.Doctor.ID = this.Reader[7].ToString();

        //            Diagnose.Doctor.Name = this.Reader[8].ToString();

        //            Diagnose.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9]);

        //            Diagnose.Dept.ID = this.Reader[10].ToString();

        //            Diagnose.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11]);

        //            Diagnose.Memo = this.Reader[12].ToString();

        //            Diagnose.User01 = this.Reader[13].ToString();
        //            Diagnose.User02 = this.Reader[14].ToString();

        //            //�������
        //            Diagnose.OperationNo = this.Reader[15].ToString();

        //            al.Add(Diagnose);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "��û��������Ϣ����" + ex.Message;
        //        this.ErrCode = "-1";
        //        this.WriteErr();
        //        return null;
        //    }
        //    this.Reader.Close();

        //    this.ProgressBarValue = -1;
        //    return al;
        ////} 
        #endregion

        #region ���»��������Ϣ
        /// <summary>
        /// ���»��������Ϣ
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns>
        public int UpdatePatientDiagnose(FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.UpdatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.UpdatePatientDiagnose.1", ref strSql) == -1)
                return -1;

            try
            {
                string[] s = new string[15];
                try
                {
                    s[0] = Diagnose.Patient.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[1] = Diagnose.HappenNo.ToString();//  --�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[2] = Diagnose.Patient.PID.CardNO;// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[3] = Diagnose.DiagType.ID.ToString();//  --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[4] = Diagnose.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[5] = Diagnose.Name;//.Replace("'","''");//--�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[6] = Diagnose.DiagDate.ToString();//  --���ʱ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[7] = Diagnose.Doctor.ID.ToString();//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[8] = Diagnose.Doctor.Name;//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[9] = (System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[10] = Diagnose.Dept.ID.ToString();//  --��Ͽ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[11] = (System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }

                try
                {
                    s[12] = Diagnose.Memo;//    --��ע
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[13] = this.Operator.ID.ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[14] = this.GetSysDateTime().ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                //				strSql=string.Format(strSql,s);
                return this.ExecNoQuery(strSql, s);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }

        /// <summary>
        /// ���»��������Ϣ
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns> 
        public int UpdatePatientDiagnose(FS.HISFC.Models.HealthRecord.Diagnose Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.UpdatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.UpdatePatientDiagnose.1", ref strSql) == -1)
                return -1;

            try
            {
                string[] s = new string[15];
                try
                {
                    s[0] = Diagnose.DiagInfo.Patient.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[1] = Diagnose.DiagInfo.HappenNo.ToString();//  --�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[2] = Diagnose.DiagInfo.Patient.PID.CardNO;// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[3] = Diagnose.DiagInfo.DiagType.ID.ToString();//  --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[4] = Diagnose.DiagInfo.ICD10.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[5] = Diagnose.DiagInfo.ICD10.Name;//.Replace("'","''");//--�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[6] = Diagnose.DiagInfo.DiagDate.ToString();//  --���ʱ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[7] = Diagnose.DiagInfo.Doctor.ID.ToString();//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[8] = Diagnose.DiagInfo.Doctor.Name;//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[9] = (System.Convert.ToInt16(Diagnose.DiagInfo.IsValid)).ToString();//    --�Ƿ���Ч
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[10] = Diagnose.DiagInfo.Dept.ID.ToString();//  --��Ͽ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[11] = (System.Convert.ToInt16(Diagnose.DiagInfo.IsMain)).ToString();//  --�Ƿ������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }

                try
                {
                    s[12] = Diagnose.DiagInfo.Memo;//    --��ע
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[13] = this.Operator.ID.ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[14] = this.GetSysDateTime().ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                //				strSql=string.Format(strSql,s);
                return this.ExecNoQuery(strSql, s);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

        }

        #endregion

        #region �Ǽǻ��������Ϣ
        /// <summary>
        /// �Ǽ��µĻ������
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns>
        public int CreatePatientDiagnose(FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.CreatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.CreatePatientDiagnose.1", ref strSql) == -1)
                return -1;
            string[] s = new string[16];
            try
            {

                try
                {
                    s[0] = Diagnose.Patient.ID.ToString();// --����סԺ��ˮ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[1] = Diagnose.HappenNo.ToString();//  --�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[2] = Diagnose.Patient.PID.CardNO;// --���￨��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[3] = Diagnose.DiagType.ID.ToString();//  --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[4] = Diagnose.ID.ToString();// --��ϱ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[5] = Diagnose.Name;//.Replace("'","''") ;//--�������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[6] = Diagnose.DiagDate.ToString();//  --���ʱ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[7] = Diagnose.Doctor.ID.ToString();//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[8] = Diagnose.Doctor.Name;//    --���ҽ��
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[9] = (System.Convert.ToInt16(Diagnose.IsValid)).ToString();//    --�Ƿ���Ч
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[10] = Diagnose.Dept.ID.ToString();//  --��Ͽ���
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[11] = (System.Convert.ToInt16(Diagnose.IsMain)).ToString();//  --�Ƿ������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }

                try
                {
                    s[12] = Diagnose.Memo;//    --��ע
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[13] = this.Operator.ID.ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                try
                {
                    s[14] = this.GetSysDateTime().ToString();//    --������
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                }
                s[15] = Diagnose.OperationNo;//�������

                //				strSql=string.Format(strSql,s);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql, s);
        }

        /// <summary>
        /// �Ǽ��µĻ������
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns> 
        public int CreatePatientDiagnose(FS.HISFC.Models.HealthRecord.Diagnose Diagnose)
        {
            #region "�ӿ�˵��"
            //�ӿ����� RADT.Diagnose.CreatePatientDiagnose.1
            // 0  --סԺ��ˮ��, 1 --�������      2   --������   ,     3   --������  ,4   --��ϱ��� 
            // 5  --�������,   6   --���ʱ��   ,7   --���ҽ������  ,8   --ҽ������ , 9   --�Ƿ���Ч
            // 10 --��Ͽ���ID 11   --�Ƿ������ 12   --��ע          13   --����Ա    14   --����ʱ��
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.CreatePatientDiagnose.1", ref strSql) == -1)
                return -1;
            string[] s = new string[16];
            s[0] = Diagnose.DiagInfo.Patient.ID.ToString();// --����סԺ��ˮ�� 
            //s[1] = Diagnose.DiagInfo.HappenNo.ToString();//  --������� 
            s[1] = this.GetNewDignoseNo().ToString();//  --������� 
            s[2] = Diagnose.DiagInfo.Patient.PID.CardNO;// --���￨�� 
            s[3] = Diagnose.DiagInfo.DiagType.ID.ToString();//  --������ 
            s[4] = Diagnose.DiagInfo.ICD10.ID.ToString();// --��ϱ��� 
            s[5] = Diagnose.DiagInfo.ICD10.Name;//.Replace("'","''") ;//--������� 
            s[6] = Diagnose.DiagInfo.DiagDate.ToString();//  --���ʱ�� 
            s[7] = Diagnose.DiagInfo.Doctor.ID.ToString();//    --���ҽ�� 
            s[8] = Diagnose.DiagInfo.Doctor.Name;//    --���ҽ�� 
            s[9] = (System.Convert.ToInt16(Diagnose.DiagInfo.IsValid)).ToString();//    --�Ƿ���Ч 
            s[10] = Diagnose.DiagInfo.Dept.ID.ToString();//  --��Ͽ��� 
            s[11] = (System.Convert.ToInt16(Diagnose.DiagInfo.IsMain)).ToString();//  --�Ƿ������ 
            s[12] = Diagnose.DiagInfo.Memo;//    --��ע 
            s[13] = this.Operator.ID.ToString();//    --������ 
            s[14] = this.GetSysDateTime().ToString();//    --������ 
            s[15] = Diagnose.DiagInfo.OperationNo;//������� 

            return this.ExecNoQuery(strSql, s);
        }

        /// <summary>
        /// ɾ�����������Ϣ
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <param name="happenNO"></param>
        /// <returns></returns>
        public int DeleteDiagnoseSingle(string InpatientNO, int happenNO)
        {

            string strSQL = "";

            if (this.Sql.GetSql("RADT.Diagnose.DeleteDocDiagnose.1", ref strSQL) == -1)
                return -1;
            try
            {
                strSQL = string.Format(strSQL, InpatientNO, happenNO.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�RADT.Diagnose.DeleteDocDiagnose.1";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #region ��������Ϸ������
        /// <summary>
        /// ��������Ϸ������
        /// </summary>
        /// <returns> ���������� ����ʱ����-1</returns>
        public int GetNewDignoseNo()
        {
            int lNewNo = -1;
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.GetNewDiagnoseNo.1", ref strSql) == -1)
                return -1;
            if (strSql == null)
                return -1;
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    lNewNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                ;
                return -1;
            }
            this.Reader.Close();
            return lNewNo;
        }
        #endregion

        #endregion

        #endregion

        #region ˳�¸�������¼��ҩ�����ʷ ���met_com_diagnose���������99
        //{30C09D02-8A87-4078-9420-023A6AC61DE9}
        /// <summary>
        /// ͨ�������Ų�ѯ�������й���ҩ��
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public ArrayList QueryDrugAllergyByNo(string CardNo)
        {
            //SDLocal.DrugAllergy.WhereSql.1
            //���룺סԺ��ˮ��
            //���������������Ϣ
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = this.PatientQuerySelectDrugAllergy();
            if (sql1 == null)
                return null;

            if (this.Sql.GetSql("SDLocal.DrugAllergy.WhereSql.1", ref sql2) == -1)
            {
                this.Err = "û���ҵ�SDLocal.DrugAllergy.WhereSql.1�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, CardNo);
            return this.myPatientQuery(sql1);
        }

        /// <summary>
        /// ��ѯ����ҩ�������Ϣ��select��䣨��where������
        /// </summary>
        /// <returns></returns>
        private string PatientQuerySelectDrugAllergy()
        {
            #region �ӿ�˵��
            //SDLocal.DrugAllergy.QuerySql.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("SDLocal.DrugAllergy.QuerySql.1", ref sql) == -1)
            {
                this.Err = "û���ҵ�SDLocal.DrugAllergy.QuerySql.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ҽ�����߲������{A7A30206-5BEC-4c47-81DA-BF92F8C80F21}
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="OperType"></param>
        /// <returns></returns>
        public ArrayList QueryCaseDiagnoseForClinicSI(string patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType)
        {
            string strSQL = "";
            string temp = "";
            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }
            if (this.Sql.GetSql("CASE.Diagnose.Select.3", ref strSQL) == -1) return null;
            strSQL = MainSql + strSQL;
            try
            {
                strSQL = string.Format(strSQL, patientId, temp);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.Select.3";
                return null;
            }

            return this.myQuery(strSQL);
        }
        #endregion

        #region  �ӵ��Ӳ����л�ȡ��Ժ־ chengym 2011-9-27

        /// <summary>
        /// ��Ժ־��ȡ��Ժ���
        /// </summary>
        /// <param name="inpatient"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject>  QueryDiagnoseFromOutCaseByInpatient(string inpatient)
        {
            string sql = "";
            if (this.Sql.GetSql("CASE.Diagnose.Select.OutCase", ref sql) == -1)
            {
                this.Err = "û���ҵ�CASE.Diagnose.Select.OutCase�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }

            sql = string.Format(sql, inpatient);
            return this.myDiagnoseFromOutCaseByInpatient(sql);
        }

        //˽�к�������ѯ��Ժ־��Ժ���
        private List<FS.FrameWork.Models.NeuObject> myDiagnoseFromOutCaseByInpatient(string SQLPatient)
        {
            List<FS.FrameWork.Models.NeuObject> list = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject Diagnose;
            this.ProgressBarText = "���ڲ�ѯ�������...";
            this.ProgressBarValue = 0;

            this.ExecQuery(SQLPatient);
            try
            {
                while (this.Reader.Read())
                {
                    Diagnose = new FS.FrameWork.Models.NeuObject();
                    Diagnose.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();  // סԺ��ˮ��
                    Diagnose.Name = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString(); //����
                    Diagnose.Memo = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString(); //��Ժ־���

                    list.Add(Diagnose);
                }
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û��������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            this.ProgressBarValue = -1;
            return list;
        }

        /// <summary>
        /// ��Ժ־��ȡ������ϵ�һ��Ϊ����Ժ���
        /// </summary>
        /// <param name="inpatient"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryInDiagnoseFromInCaseByInpatient(string inpatient)
        {
            string sql = "";
            if (this.Sql.GetSql("CASE.Diagnose.Select.InDiagnose", ref sql) == -1)
            {
                this.Err = "û���ҵ�CASE.Diagnose.Select.InDiagnose�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }


            sql = string.Format(sql, inpatient);
            return this.myDiagnoseFromOutCaseByInpatient(sql);
        }

        /// <summary>
        /// ��Ժ־��ȡҩ�����ʷ
        /// </summary>
        /// <param name="inpatient"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryPharmacyAllergicFromInCaseByInpatient(string inpatient)
        {
            string sql = "";
            if (this.Sql.GetSql("CASE.Diagnose.Select.PharmacyAllergic", ref sql) == -1)
            {
                this.Err = "û���ҵ�CASE.Diagnose.Select.PharmacyAllergic�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }


            sql = string.Format(sql, inpatient);
            return this.myDiagnoseFromOutCaseByInpatient(sql);
        }

        #endregion

        #region �µ��Ӳ�����ȡ���
        /// <summary>
        /// ��õ��Ӳ��������ͼ�Ļ��������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryCaseDiagnoseFromEmrView(string InpatientNO)
        {
            string strSQL = "";
            strSQL = @"SELECT inpatient_no, --סԺ��ˮ��
       happen_no, --�������
       diag_kind, --�������
       level_code, --��ϼ���
       perior_code, --��Ϸ���
       icd_code, --���ICD��
       diag_name, --�������
       diag_date, --�������
       doct_code, --ҽʦ����
       doct_name, --ҽʦ����(���)
       in_date, --��Ժ����
       out_date, --��Ժ����
       diag_outstate, --�������
       second_icd, --�ڶ�ICD
       syndrome_id, --����֢���
       cl_pa, --�������
       dubdiag_flag, --�Ƿ�����
       main_flag, --�Ƿ������
       remark, --��ע
       oper_code, --����Ա
       oper_date, --����ʱ��
       OPER_TYPE, -- 1 ҽ��վ¼�� 2 ������¼��
       OPERATION_FLAG, -- ������־
       IS30DISEASE, -- �Ƿ���30�ּ���
       persson_type, --�������0 ���� 1 סԺ
       valid_flag, --��Ч��־ 0 ��Ч 1 ��Ч
       diagnosis_flag --�Ƿ���1Ϊ���0Ϊ����
  FROM view_met_cas_diagnose --����������Ͽ�
 WHERE inpatient_no = '{0}'";
            try
            {
                strSQL = string.Format(strSQL, InpatientNO);
            }
            catch
            {
                this.Err = "����������ԣ�";
                return null;
            }
            return this.myQuery(strSQL);
        }
        #endregion


        #region ����Emr��ϵ�ת��������۴���Ŀ��

        /// <summary>
        /// ���²�����Emr��ϵ�ת�������
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="emrDiagnoseKey"></param>
        /// <param name="zg"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        /// {3745f152-cc8b-45f6-9f63-d6d591f8c654}
        public int UpdateCaseDiagnoseZGForEmr(string inpatientNO, string emrDiagnoseKey, string zg, string operCode, DateTime operDate)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("CASE.Diagnose.UpdateCaseDiagnoseZGForEmr", ref sql) == -1)
            {
                this.Err = "δ�ҵ�Sql[CASE.Diagnose.UpdateCaseDiagnoseZGForEmr]";

                return -1;
            }

            string execSql = string.Format(sql,
                inpatientNO,
                emrDiagnoseKey.ToString(),
                zg,
                operCode,
                operDate.ToString("yyyy-MM-dd HH:mm:ss"));

            return this.ExecNoQuery(execSql);
        }

        /// <summary>
        /// ����ָ��Emr���Id�Ĳ������
        /// </summary>
        /// <param name="pid">���߾���Id�����������ˮ�Ż�סԺ��ˮ�š�</param>
        /// <param name="emrDiagnoseKey"></param>
        /// <returns></returns>
        /// {3745f152-cc8b-45f6-9f63-d6d591f8c654}
        public FS.HISFC.Models.HealthRecord.Diagnose GetCaseDiagnoseByEmrId(string pid, string emrDiagnoseKey)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("CASE.Diagnose.GetCaseDiagnoseByEmrId", ref sql) == -1)
            {
                this.Err = "δ�ҵ�Sql[CASE.Diagnose.GetCaseDiagnoseByEmrId]";

                return null;
            }

            string execSql = string.Format(sql, pid, emrDiagnoseKey);
            string mainSelectSql = this.QuerySql();

            var loadDatas = this.myQuery(mainSelectSql + Environment.NewLine + execSql);
            if (loadDatas != null && loadDatas.Count > 0)
            {
                return loadDatas[0] as FS.HISFC.Models.HealthRecord.Diagnose;
            }

            return null;
        }

        #endregion

        #region ��ȡ�������סԺ��ϡ��д���Ժ��Ŀ��

        /// <summary>
        /// ͨ����ˮ�źͻ�������ȡ���
        /// </summary>
        /// <param name="inPatientNO">��ˮ��</param>
        /// <param name="personType">�������0 ���� 1 סԺ</param>
        /// <returns></returns>
        public ArrayList QueryDiagnoseByInpatientNOAndPersonType(string inPatientNO, string personType)
        {
            string strSQL = "";
            if (this.Sql.GetSql("FS.Diagnose.GetDiagnoseByInpatientNoAndPersonType.Where.1", ref strSQL) == -1)
            {
                strSQL = @"
                           WHERE INPATIENT_NO = '{0}' AND PERSSON_TYPE = '{1}' 
                           AND VALID_FLAG = '1' ORDER BY OPER_DATE DESC";
            }

            string MainSql = this.QuerySql();
            if (MainSql == null)
            {
                return null;
            }

            strSQL = MainSql + strSQL;

            try
            {
                strSQL = string.Format(strSQL, inPatientNO, personType);
            }
            catch
            {
                this.Err = "����������ԣ�";
                return null;
            }
            return this.myQuery(strSQL);
        }

        #endregion

        #region ������
        /// <summary>
        /// ��ѯ���������
        /// </summary>
        /// <param name="inpatientNO">������ˮ�� </param>
        /// <param name="OperType">DOC ��ѯҽ��¼������,CAS ��ѯ���������������</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.Diagnose> QueryMainDiagnose(string inpatientNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType)
        {
            string strSQL = "";
            string MainFlag = "1";//�����           
            string temp = "";
            if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                temp = "1";
            }
            else if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                temp = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            string MainSql = QuerySql();
            if (MainSql == null)
            {
                return null;
            }
            if (this.Sql.GetSql("CASE.Diagnose.QueryMainDiagnose.IBORN", ref strSQL) == -1) return null;
            strSQL = MainSql + strSQL;
            try
            {
                strSQL = string.Format(strSQL, inpatientNO, MainFlag, temp);
            }
            catch
            {
                this.Err = "����������ԣ�CASE.Diagnose.QueryMainDiagnose.IBORN";
                return null;
            }

            return this.MyQuery(strSQL);
        }

        /// <summary>
        /// ��ѯ���Ӳ�����סԺ�����
        /// </summary>
        /// <param name="inpatientNO">������ˮ�� </param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryERMMainDiagnose(string inpatientNO)
        {
            string strSQL = "";
            if (this.Sql.GetSql("RADT.Diagnose.GetErmMainDiagnose", ref strSQL) == -1) return null;            
            try
            {
                strSQL = string.Format(strSQL, inpatientNO);
            }
            catch
            {
                this.Err = "����������ԣ�RADT.Diagnose.GetErmMainDiagnose";
                return null;
            }
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject dg;
            this.ExecQuery(strSQL);
            try
            {
                while (this.Reader.Read())
                {
                    dg = new FS.FrameWork.Models.NeuObject();

                    dg.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();//��Ժ��ϱ���
                    if (dg.ID!="--")
                    {
                        dg.Name = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString(); //��Ժ�������
                        dg.Memo = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//��Ժ�������
                    }
                    else
                    {
                        dg.ID = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();//��Ժ��ϱ���
                        dg.Name = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString(); //��Ժ�������
                        dg.Memo = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();//��Ժ�������
                    }                    
                    al.Add(dg);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = "��ò�����Ҫ�����Ϣ����![RADT.Diagnose.GetErmMainDiagnose]" + ex.Message;
                this.WriteErr();                
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return al;            
        }
        #endregion

        #region ����
        /// <summary>
        /// �ж���������Ƿ�Ϻ����� 
        /// �������  ���� null
        /// ��ȫ�Ϻ�����,û����ʾ list.Count == 0 
        ///  User01 =1 ������©�ĸ������ ,��������
        ///  User01 = 2 ���ڿ�����©����� ,����ɲ���
        /// </summary>
        /// <param name="list">Ҫ������Ŀ�б�</param>
        /// <param name="SexType">�Ա�</param>
        /// <returns> �������  ���� null ;list.Count == 0 ��ȫ�Ϻ�����,û����ʾ ;User01 =1  ȱ�ٱ������ϻ���ϴ��� ������ͨ��;User01 = 2 ���ڿ�����©����� ,����ɲ��� </returns>
        [Obsolete("����,��QueryDiagnoseValueState����",true)]
        public ArrayList DiagnoseValueState(ArrayList list, FS.HISFC.Models.Base.EnumSex SexType)
        {
            if (list == null)
            {
                return null;
            }
            ArrayList SexTypeList = null;
            ICD icd = new ICD();
            //�ж���Ů������Ƿ����
            if (SexType.ToString() == "M")
            {
                //��ѯר����Ů�Ե�����б�
                SexTypeList = icd.GetDiagnoseBySex("F");
                if (SexTypeList == null)
                {
                    return null;
                }
            }
            else if (SexType.ToString() == "F")
            {
                //��ѯר�������Ե���� �б�
                SexTypeList = icd.GetDiagnoseBySex("M");
                if (SexTypeList == null)
                {
                    this.Err = "��ȡ��������б����";
                    return null;
                }
            }
            //���ص�����
            ArrayList Returnlist = new ArrayList();
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.ICD10.ID.Length < 3)
                {
                    continue;
                }
                if (obj.DiagInfo.ICD10.ID.Length >= 6) //������̬ѧ���� 
                {
                    string str = obj.DiagInfo.ICD10.ID.Substring(0, 6);
                    if (str.CompareTo("M80000") >= 0 && str.CompareTo("M99890") <= 0)
                    {
                        continue;
                    }
                }
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                string strCode = obj.DiagInfo.ICD10.ID.Substring(0, 3);

                if (strCode.CompareTo("A00") >= 0 && strCode.CompareTo("R99") <= 0)
                {
                    bool boolTemp = true;
                    //�ж�list���Ƿ��� V��W��X��Y��ͷ��ʾ���˻��ж���������  ���û�� ��ӵ���ʾ������ 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 1);
                        if (strFirst.CompareTo("V") == 0 || strFirst.CompareTo("W") == 0 || strFirst.CompareTo("X") == 0 || strFirst.CompareTo("Y") == 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp) //���û�� V��W��X��Y��ͷ��ʾ���˻��ж���������
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "1"; //������Ҫ��ӵ� 
                        info.User02 = "������Ҫ���V��W��X��Y��ͷ��ʾ���˻��ж���������";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("S00") >= 0 && strCode.CompareTo("T98") <= 0)
                {
                    bool boolTemp = true;
                    //�ж�list���Ƿ��� V��W��X��Y��ͷ��ʾ���˻��ж���������  ���û�� ��ӵ���ʾ������ 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 1);
                        if (strFirst.CompareTo("V") == 0 || strFirst.CompareTo("W") == 0 || strFirst.CompareTo("X") == 0 || strFirst.CompareTo("Y") == 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = "�������V��W��X��Y��ͷ��ʾ���˻��ж���������";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                if (strCode.CompareTo("C00") >= 0 && strCode.CompareTo("D48") <= 0)
                {
                    if (strCode.CompareTo("C00") >= 0 && strCode.CompareTo("C96") <= 0)
                    {
                        bool boolTemp = true;
                        //C00 - C96 ��ʾ����M80000/3 - M99890/3
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/3") >= 0 && strFirst.CompareTo("M99890/3") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/3 - M99890/3 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }

                    }
                    if (strCode.CompareTo("C77") >= 0 && strCode.CompareTo("C79") <= 0)
                    {
                        bool boolTemp = true;
                        //C77 - C79 ��ʾ����M80000/6 - M99890/6 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/6") >= 0 && strFirst.CompareTo("M99890/6") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/6 - M99890/6 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D00") >= 0 && strCode.CompareTo("D09") <= 0)
                    {
                        bool boolTemp = true;
                        //D00 - D09 ��ʾ����M80000/2 - M99890/2 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/2") >= 0 && strFirst.CompareTo("M99890/2") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/2 - M99890/2 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D10") >= 0 && strCode.CompareTo("D36") <= 0)
                    {
                        bool boolTemp = true;
                        //D10- D36 ��ʾ���� M80000/0 - M99890/0 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/0") >= 0 && strFirst.CompareTo("M99890/0") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽�� M80000/0 - M99890/0 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                    if (strCode.CompareTo("D37") >= 0 && strCode.CompareTo("D48") <= 0)
                    {
                        bool boolTemp = true;
                        //D37- D48 ��ʾ����M80000/1 - M99890/1 
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                        {
                            if (temp.DiagInfo.ICD10.ID.Length >= 8)
                            {
                                string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 8);
                                if (strFirst.CompareTo("M80000/1") >= 0 && strFirst.CompareTo("M99890/1") <= 0)
                                {
                                    boolTemp = false;
                                    break;
                                }
                            }
                        }
                        if (boolTemp)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = "���븽��M80000/1 - M99890/1 ������̬ѧ����";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                else if (strCode.CompareTo("080") == 0 && obj.MainFlag == "1")
                {
                    // �����080 ������� , 010 - 099 ���ܳ����ڸ��������
                    bool boolTemp = true;
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("010") >= 0 && strFirst.CompareTo("099") <= 0 && obj.MainFlag == "0")
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = "080 ������� , 010 - 099 ���ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("010") >= 0 && strCode.CompareTo("099") >= 0 && obj.MainFlag == "1")
                {
                    bool boolTemp = true;
                    // ����ǡ�010 - 099��������� , 080�����ܳ����ڸ��������
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("080") >= 0 && obj.MainFlag == "0")
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = " 010 - 099��������� , 080�����ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                //�ж���Ů������Ƿ����
                if (SexType.ToString() == "M")
                {
                    //�ж��Ƿ���
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in SexTypeList)
                    {
                        if (temp.DiagInfo.ICD10.ID == obj.DiagInfo.ICD10.ID)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = " Ů�Ե���ϲ�����������  ";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                else if (SexType.ToString() == "F")
                {
                    //�ж��Ƿ���
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in SexTypeList)
                    {
                        if (temp.DiagInfo.ICD10.ID == obj.DiagInfo.ICD10.ID)
                        {
                            info = new FS.HISFC.Models.HealthRecord.Diagnose();
                            info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                            info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                            info.User01 = "2"; //������ӵ� 
                            info.User02 = " ���Ե���ϲ�������Ů��  ";//��ʾ����Ϣ
                            Returnlist.Add(info);
                        }
                    }
                }
                if (strCode.CompareTo("010") >= 0 && strCode.CompareTo("099") <= 0)
                {
                    bool boolTemp = true;
                    //���� 010  - 099 ���븽��Z31 - Z37 ���� 
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose temp in list)
                    {
                        string strFirst = temp.DiagInfo.ICD10.ID.Substring(0, 3);
                        if (strFirst.CompareTo("Z31") >= 0 && strFirst.CompareTo("Z37") <= 0)
                        {
                            boolTemp = false;
                            break;
                        }
                    }
                    if (boolTemp)
                    {
                        info = new FS.HISFC.Models.HealthRecord.Diagnose();
                        info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                        info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                        info.User01 = "2"; //������ӵ� 
                        info.User02 = " 010 - 099��������� , 080�����ܳ����ڸ�������� ";//��ʾ����Ϣ
                        Returnlist.Add(info);
                    }
                }
                else if (strCode.CompareTo("003") >= 0 && strCode.CompareTo("008") <= 0)
                {
                    //���Ӥ��������������22����Ҫ��ʾҽ������ P95����
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "1"; //��ѡ�� 
                    info.User02 = " ���Ӥ��������������22��,��Ҫ���� P95����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
                else if (strCode.CompareTo("B95") >= 0 && strCode.CompareTo("B97") <= 0 && obj.MainFlag == "1")
                {
                    //B95 - B97 ������Ϊ��Ժ����� 
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "2"; //������ӵ� 
                    info.User02 = " B95 - B97 ������Ϊ��Ժ�����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
                else if (strCode.CompareTo("J98.401") == 0)
                {
                    //��ʾ����ϸ��ѧ��̵���� �������������Ҫ���� J12 - J17 �������� 
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.ICD10.ID = obj.DiagInfo.ICD10.ID; //��ϱ���
                    info.DiagInfo.ICD10.Name = obj.DiagInfo.ICD10.Name; //������� 
                    info.User01 = "1"; //��ѡ�� 
                    info.User02 = " ���ϸ��ѧ��̵�����ҽ��������,��Ҫ����J12 - J17����  ";//��ʾ����Ϣ
                    Returnlist.Add(info);
                }
            }
            return Returnlist;
        }
        #region  �������� ���׷���
        /// <summary>
        /// ��ȡ������ 
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������ó���ά��",true)]
        public ArrayList GetDiagnoseList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode obj = new FS.HISFC.Object.Base.SpellCode();
            //#region  ��ǰ��
            ////			info.ID = "1";
            ////			info.Name = "��Ҫ���";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "2";
            ////			info.Name = "�������";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "3";
            ////			info.Name = "����֢";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "4";
            ////			info.Name = "Ժ�ڸ�Ⱦ";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "5";
            ////			info.Name = "����";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "6";
            ////			info.Name = "�������";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "7";
            ////			info.Name = "����ҩ";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "8";
            ////			info.Name = "����������";
            ////			list.Add(info);
            ////
            ////			info = new FS.HISFC.Object.Base.SpellCode();
            ////			info.ID = "9";
            ////			info.Name = "������Ժ��";
            ////			list.Add(info);
            //#endregion
            //obj.ID = "1";
            //obj.Name = "��Ժ���";
            //list.Add(obj);
            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "2";
            //obj.Name = "ת�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "3";
            //obj.Name = "��Ժ���"; //�����ҽ��վ�����𣬳�Ժ��϶�Ӧ ��Ҫ���
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "4";
            //obj.Name = "ת�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "5";
            //obj.Name = "ȷ�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "6";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "7";
            //obj.Name = "��ǰ���";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "8";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "9";
            //obj.Name = "��Ⱦ���";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "10";
            //obj.Name = "�����ж����";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "12";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "13";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "14";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "15";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Object.Base.SpellCode();
            //obj.ID = "16";
            //obj.Name = "�������";
            //list.Add(obj);
            ////
            ////			obj = new FS.HISFC.Object.Base.SpellCode();
            ////			obj.ID = "17";
            ////			obj.Name = "�������";
            ////			list.Add(obj);

            return list;
        }

        /// <summary>
        /// ����в���������
        /// </summary>
        /// <returns></returns>
        [Obsolete("�������ó��� OPERATIONTYPE ά��", true)]
        public ArrayList GetDiagOperType()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Object.Base.SpellCode info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "0";
            //info.Name = "��";
            //info.Spell_Code = "w";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "1";
            //info.Name = "����";
            //info.Spell_Code = "ss";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "2";
            //info.Name = "����";
            //info.Spell_Code = "cz";
            //list.Add(info);

            //info = new FS.HISFC.Object.Base.SpellCode();
            //info.ID = "3";
            //info.Name = "ȫ��";
            //info.Spell_Code = "qb";
            //list.Add(info);

            return list;
        }
        #endregion 

        #region �ǲ������� ��ʱ���� �����������������
        #region ���ҵ��  ��ʱ����  ���û���� met_com_diagnose�д�����ֱ�Ӵ浽 met_cas_diagnose

        #region ���ϻ��������Ϣ
        /// <summary>
        /// ���ϻ��������Ϣ
        /// </summary>
        /// <param name="Diagnose"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public int DcPatientDiagnose(FS.HISFC.Models.HealthRecord.DiagnoseBase Diagnose)
        {
            #region �ӿ�˵��
            //���ϻ��������Ϣ
            //RADT.Diagnose.DcPatientDiagnose.1
            //���룺0 InpatientNoסԺ��ˮ��,1 happenno �������
            //������0 
            #endregion
            string strSql = "";
            if (this.Sql.GetSql("RADT.Diagnose.DcPatientDiagnose.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, Diagnose.Patient.ID, Diagnose.HappenNo.ToString());
            }
            catch
            {
                this.Err = "����������ԣ�RADT.Diagnose.DcPatientDiagnose.1";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion
        #endregion

        #region "��ѯ����" ��ʱ����
        #region "��ѯ�������"
        /// <summary>
        /// ��ѯ�����������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public ArrayList PatientDiagnoseQuery(string InPatientNo)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.1
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.1", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.1�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo);
            return this.myPatientQuery(sql1);
        }
        /// <summary>
        /// ��ѯ���߸��������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <param name="DiagType"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public ArrayList PatientDiagnoseQuery(string InPatientNo, string DiagType)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.2
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.2", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.2�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo, DiagType);
            return this.myPatientQuery(sql1);
        }
        /// <summary>
        /// ��ѯ���߸�״̬���
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <param name="IsValid"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public ArrayList PatientDiagnoseQuery(string InPatientNo, bool IsValid)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.3
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.3", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.3�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo, FS.FrameWork.Function.NConvert.ToInt32(IsValid).ToString());
            return this.myPatientQuery(sql1);
        }
        /// <summary>
        /// ��ѯ������/�������
        /// </summary>
        /// <param name="InPatientNo"></param>
        /// <param name="IsMain"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public ArrayList MainDiagnoseQuery(string InPatientNo, bool IsMain)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.4
            //���룺0סԺ��ˮ��1 �Ƿ������
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect();
            if (sql1 == null) return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.4", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.4�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo, IsMain.ToString());
            return this.myPatientQuery(sql1);
        }

        #endregion
   
        #endregion

        /// <summary>
        /// ��ѯ���
        /// </summary>
        /// <param name="InPatientNo">���߿�����ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryDiagnoseNoByPatientNo(string InPatientNo)
        {
            #region �ӿ�˵��
            //RADT.Diagnose.PatientDiagnoseQuery.1
            //���룺סԺ��ˮ��
            //���������������Ϣ
            #endregion
            ArrayList al = new ArrayList();
            string sql1 = "", sql2 = "";

            sql1 = PatientQuerySelect1();
            if (sql1 == null)
                return null;

            if (this.Sql.GetSql("RADT.Diagnose.PatientDiagnoseQuery.6", ref sql2) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.PatientDiagnoseQuery.6�ֶ�!";
                this.ErrCode = "-1";
                return null;
            }
            sql1 = sql1 + " " + string.Format(sql2, InPatientNo);
            return this.myPatientQuery1(sql1);
        }

        private string PatientQuerySelect1()
        {
            #region �ӿ�˵��
            //RADT.Diagnose.DiagnoseQuery.select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("RADT.Diagnose.DiagnoseQuery.select.10", ref sql) == -1)
            {
                this.Err = "û���ҵ�RADT.Diagnose.DiagnoseQuery.select.10�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }
        private ArrayList myPatientQuery1(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.HealthRecord.Diagnose Diagnose;
            this.ProgressBarText = "���ڲ�ѯ�������...";
            this.ProgressBarValue = 0;

            this.ExecQuery(SQLPatient);
            try
            {
                while (this.Reader.Read())
                {
                    Diagnose = new FS.HISFC.Models.HealthRecord.Diagnose();
                    Diagnose.DiagInfo.Patient.ID = this.Reader[0] == DBNull.Value ? string.Empty : this.Reader[0].ToString();// סԺ��ˮ��
                    Diagnose.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1] == DBNull.Value ? "0" : this.Reader[1].ToString());//  �������
                    Diagnose.DiagInfo.DiagType.ID = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString();//������
                    //FS.HISFC.Models.HealthRecord.DiagnoseType diagnosetype = new FS.HISFC.Models.HealthRecord.DiagnoseType();
                    //diagnosetype.ID = Diagnose.DiagType.ID;
                    //Diagnose.DiagType.Name = diagnosetype.Name;//���������� zjy
                    Diagnose.DiagInfo.ID = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();		//��ϴ���
                    Diagnose.DiagInfo.ICD10.ID = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();
                    Diagnose.DiagInfo.Name = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();		//�������
                    Diagnose.DiagInfo.ICD10.Name = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();
                    Diagnose.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5] == DBNull.Value ? "0001-01-01" : this.Reader[5].ToString());
                    Diagnose.DiagInfo.Doctor.ID = this.Reader[6] == DBNull.Value ? string.Empty : this.Reader[6].ToString();
                    Diagnose.DiagInfo.Doctor.Name = this.Reader[7] == DBNull.Value ? string.Empty : this.Reader[7].ToString();
                    Diagnose.DiagInfo.IsMain = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8] == DBNull.Value ? 0 : this.Reader[8]);
                    al.Add(Diagnose);
                }
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = "��û��������Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            this.ProgressBarValue = -1;
            return al;
        }

        #region ��ȡ��������Ϣ �б�  ������ҳ �����Ϣ¼��ר�� ע����������õ���������һ��
        /// <summary> 
        /// ��ȡ��������Ϣ �б�  ������ҳ �����Ϣ¼��ר�� 
        /// creator :zhangjunyi@FS.com
        /// </summary>
        /// <returns>�������б�</returns>
        [Obsolete("����", true)]
        public ArrayList getList()
        {
            ArrayList list = new ArrayList();
            //FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();

            //obj.ID = "2";
            //obj.Name = "ת�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "3";
            //obj.Name = "��Ҫ���"; //�����ҽ��վ�����𣬳�Ժ��϶�Ӧ ��Ҫ���
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "4";
            //obj.Name = "ת�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "5";
            //obj.Name = "ȷ�����";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "6";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "7";
            //obj.Name = "��ǰ���";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "8";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "9";
            //obj.Name = "��Ⱦ���";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "10";
            //obj.Name = "�����ж����";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "12";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "13";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "14";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "15";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "16";
            //obj.Name = "�������";
            //list.Add(obj);

            //obj = new FS.HISFC.Models.Base.Const();
            //obj.ID = "17";
            //obj.Name = "�������";
            //list.Add(obj);

            return list;
        }
        #endregion
        #endregion 

        #endregion 
    }
}

