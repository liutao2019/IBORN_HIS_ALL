using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾���͹���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-13]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-19' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
   public class SpecTypeManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾�й�����</param>
        /// <returns></returns>
       private string[] GetParam(FS.HISFC.Models.Speciment.SpecType specType)
       {
           string[] str;
           str = new string[]
						{
                            specType.SpecTypeID.ToString(),
							specType.SpecTypeName,
                            specType.SpecColor,
                            specType.OrgType.OrgTypeID.ToString(),
                            specType.Comment,
                            specType.IsShow,
                            specType.DefaultCnt.ToString(),
                            specType.Ext1,
                            specType.Ext2,
                            specType.Ext3
						};
           return str;
       }
        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        private int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.SpecTypeManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #endregion

        #region ���±걾���Ͳ���
        /// <summary>
        /// ���±걾������Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSpecType(string sqlIndex, params string[] args)
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

       /// <summary>
       /// ����sqlIndex��ȡ����������SpecType List
       /// </summary>
       /// <param name="sqlIndex"></param>
       /// <param name="args"></param>
       /// <returns>�걾����List</returns>
       private ArrayList GetSpecType(string sqlIndex, params string[] args)
       {
           string sql = "";
           if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
               return null;
           if (this.ExecQuery(sql, args) == -1)
               return null;
           ArrayList arrSpecList = new ArrayList();
           while (this.Reader.Read())
           {
               SpecType specType= SetSpecType();
               arrSpecList.Add(specType);
           }
           this.Reader.Close();
           return arrSpecList;
       }

        /// <summary>
        /// ��ȡ�걾������Ϣ
        /// </summary>
        /// <returns>��֯����ʵ��</returns>
       private SpecType SetSpecType()
        {
            SpecType specType= new SpecType();
            try
            {
                specType.SpecTypeID = Convert.ToInt32(this.Reader[0].ToString());
                specType.SpecTypeName = this.Reader[1].ToString();
                specType.SpecColor = this.Reader[2].ToString();
                specType.OrgType.OrgTypeID = Convert.ToInt32(this.Reader[3].ToString());
                specType.Comment =this.Reader[4].ToString();
                specType.IsShow = this.Reader[5].ToString();
                specType.DefaultCnt =  Convert.ToInt32(this.Reader[6].ToString());
                specType.Ext1 = this.Reader[7].ToString();
                specType.Ext2 = this.Reader[8].ToString();
                specType.Ext3 = this.Reader[9].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return specType;
        }
        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// �걾���Ͳ���
        /// </summary>
        /// <param name="specBox">��������ı걾����</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertSpecType(FS.HISFC.Models.Speciment.SpecType specType)
        {
            // return this.UpdateSingleTable("Fee.OutPatient.DayBalance.Insert", this.GetDayBalanceParams(dayBalance));
            return this.UpdateSpecType("Speciment.BizLogic.SpecTypeManage.Insert", this.GetParam(specType));
        }

        /// <summary>
        /// �������Ƹ��±걾��֯����
        /// </summary>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public int UpdateSpecType(SpecType specType)
        {

            // return this.UpdateSingleTable("Fee.OutPatient.DayBalance.Insert", this.GetDayBalanceParams(dayBalance));
            return this.UpdateSpecType("Speciment.BizLogic.SpecTypeManage.Update", this.GetParam(specType));
        }

       /// <summary>
       /// ��ȡsequence
       /// </summary>
       /// <returns></returns>
       public int GetSequence()
       {
           string sequence = "";
           this.GetNextSequence(ref sequence);
           try
           {
               return Convert.ToInt32(sequence);
           }
           catch
           {
               return 0;
           }
       }

        /// <summary>
        /// ��������ɾ���걾����
        /// </summary>
        /// <param name="orgTypeName"></param>
        /// <returns></returns>
        public int DeleteSpecTypeByID(string specTypeID)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("Speciment.BizLogic.SpecTypeManage.Delete", ref sql) == -1)
            {
                this.Err = "��ȡSQL���Speciment.BizLogic.SpecTypeManage.Deleteʧ��";
                return -1;
            }
            // ƥ��ִ��SQL���
            try
            {
                sql = string.Format(sql, specTypeID);

                return this.ExecNoQuery(sql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }

        }

        /// <summary>
        /// �������еı걾����
        /// </summary>
        /// <returns>�걾��֯List</returns>
        public Dictionary<int, string> GetSpecTypeByOrgID(string orgTypeID)
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.SpecTypeManage.SelectByOrgID", ref sql) == -1)
                return null;
            sql = string.Format(sql, orgTypeID);

            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicSpecType = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                SpecType type = SetSpecType();
                dicSpecType.Add(type.SpecTypeID, type.SpecTypeName);
            }
            this.Reader.Close();
            return dicSpecType;
        }

       /// <summary>
       /// �������ƻ�ȡID
       /// </summary>
       /// <param name="specName"></param>
       /// <returns></returns>
       public int GetSpecIDByName(string specName)
       {
           string sql = "";
           if (this.Sql.GetSql("Speciment.BizLogic.SpecTypeManage.SelectByName", ref sql) == -1)
               return 0;
           sql = string.Format(sql, specName);

           if (this.ExecQuery(sql) == -1)
               return 0;
           int typeID =  0;
           while (this.Reader.Read())
           {
               SpecType type = SetSpecType();
               typeID = type.SpecTypeID;
           }
           this.Reader.Close();
           return typeID;
       }

       /// <summary>
       /// ���ݱ걾�������ƺ��������ƻ�ȡ�걾����ID
       /// </summary>
       /// <param name="specName">�걾��������</param>
       /// <param name="orgName">�걾����ID</param>
       /// <returns></returns>
       public int GetSpecIDByName(string specName, string orgName)
       {
           int specTypeId = 0;
           ArrayList arrList = new ArrayList();
           arrList = GetSpecType("", new string[] { specName, orgName });
           foreach (SpecType spec in arrList)
           {
               specTypeId = spec.SpecTypeID;
           }
           return specTypeId;
 
       }

       /// <summary>
       /// ���ݱ걾���͵�ID��ȡ�걾��ɫ
       /// </summary>
       /// <param name="SpecTypeID"></param>
       /// <returns></returns>
       public string GetColorByType(int SpecTypeID)
       {
           string sql = "";
           if (this.Sql.GetSql("Speciment.BizLogic.SpecTypeManage.SelectColorByTypeID", ref sql) == -1)
               return "";
           sql = string.Format(sql, SpecTypeID);

           if (this.ExecQuery(sql) == -1)
               return "";
           string typeColor = "";
           while (this.Reader.Read())
           {
               SpecType type = SetSpecType();
               typeColor = type.SpecColor;
           }
           this.Reader.Close();
           return typeColor;
       }

       /// <summary>
       /// ������֯���ƻ�ȡ�걾����list
       /// </summary>
       /// <param name="orgName"></param>
       /// <returns>�걾����List</returns>
       public ArrayList GetSpecByOrgName(string orgName)
       {
           string[] sql = new string[] { orgName };
           return GetSpecType("Speciment.BizLogic.SpecTypeManage.SelectByOrgName", sql);
       }

       /// <summary>
       /// ����SpecTypeID���ұ걾����
       /// </summary>
       /// <param name="specTypeId">�걾����ID</param>
       /// <returns></returns>
       public SpecType GetSpecTypeById(string specTypeId)
       {
           string[] sql = new string[] { specTypeId };
           ArrayList arrList = GetSpecType("Speciment.BizLogic.SpecTypeManage.SelectByID", sql);
           SpecType specType = new SpecType();
           foreach (SpecType s in arrList)
           {
               specType = s;
               break;              
           }
           return specType;
       }

       /// <summary>
       /// ��ȡ�걾����
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public ArrayList GetSpecType(string sql)
       {
           if (this.ExecQuery(sql) == -1)
               return null;
           ArrayList arrSpecType = new ArrayList();
           while (Reader.Read())
           {
               SpecType tmp = SetSpecType();
               arrSpecType.Add(tmp); 
           }
           Reader.Close();
           return arrSpecType;
       }

       /// <summary>
       /// ���ݱ걾�е�ID���Һ����д�ŵı걾����
       /// </summary>
       /// <param name="boxId"></param>
       /// <returns></returns>
       public SpecType GetSpecTypeByBoxId(string boxId)
       {
           string[] sql = new string[] { boxId };
           ArrayList arrSpecType = new ArrayList();
           arrSpecType = GetSpecType("Speciment.BizLogic.SpecTypeManage.GetByBoxId", sql);
           return arrSpecType[0] as SpecType;
      
       }

       /// <summary>
       /// ��ȡȫ���ı걾����
       /// </summary>
       /// <returns></returns>
       public ArrayList GetAllSpecType()
       {
           ArrayList arrSpecType = new ArrayList();
           arrSpecType = GetSpecType("Speciment.BizLogic.SpecTypeManage.SelectAll", new string[] { });
           return arrSpecType;
       }
        #endregion
    }
}
