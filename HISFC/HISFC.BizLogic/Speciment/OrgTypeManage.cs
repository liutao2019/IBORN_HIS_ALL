using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾��֯���͹���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-13]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-14' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class OrgTypeManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾��֯���Ͷ���</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.OrgType orgType)
        {
            string sequence = ""; 
           
            GetNextSequence(ref sequence);   
            string[] str = new string[]
						{
							sequence, 
							orgType.OrgName,
                            orgType.IsShowOnApp.ToString()
						};
            return str;
        }

        private string[] GetParam(string orgTypeName, string orgTypeID)
        {
            
            string[] str = new string[]
						{
                            orgTypeName,
							orgTypeID
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
            sequence = this.GetSequence("Speciment.BizLogic.OrgTypeManage.GetNextSequence");
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

        #region ���±걾��֯���Ͳ���
        /// <summary>
        /// ���±걾��֯������Ϣ
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateOrgType(string sqlIndex, params string[] args)
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
        /// ����sql������ȡorgtype
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetOrgType(string sqlIndex, params string[] args)
        {           
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrOrgType = new ArrayList();
            while (this.Reader.Read())
            {
                OrgType orgType = SetOrgType();
                arrOrgType.Add(orgType);
            }
            this.Reader.Close();
            return arrOrgType;
        }

        /// <summary>
        /// ��ȡ�걾��֯������Ϣ
        /// </summary>
        /// <returns>��֯����ʵ��</returns>
        private OrgType SetOrgType()
        {
            OrgType orgType = new OrgType();
            try
            {
                orgType.OrgTypeID = Convert.ToInt32(this.Reader[0].ToString());
                orgType.OrgName = this.Reader[1].ToString();
                orgType.IsShowOnApp = Convert.ToInt16(this.Reader[2].ToString());               
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return orgType;
        }
        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// �걾��֯���Ͳ���
        /// </summary>
        /// <param name="specBox">��������ı걾��֯����</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertOrgType(FS.HISFC.Models.Speciment.OrgType orgType)
        {
            return this.UpdateOrgType("Speciment.BizLogic.OrgTypeManage.Insert", this.GetParam(orgType));
        }

        /// <summary>
        /// �������Ƹ��±걾��֯����
        /// </summary>
        /// <param name="orgType"></param>
        /// <returns></returns>
        public int UpdateOrgType(string orgTypeName,int orgTypeID)
        {
            return this.UpdateOrgType("Speciment.BizLogic.OrgTypeManage.Update", this.GetParam(orgTypeName, orgTypeID.ToString()));
        }

        /// <summary>
        /// ��������ɾ����֯����
        /// </summary>
        /// <param name="orgTypeName"></param>
        /// <returns></returns>
        public int DeleteOrgTypeByName(string orgTypeName)
        {
            return this.UpdateOrgType("Speciment.BizLogic.OrgTypeManage.Delete", new string[] { orgTypeName });
        }

        /// <summary>
        /// ���ݱ걾�������ƻ�ȡID
        /// </summary>
        /// <param name="orgName"></param>
        /// <returns></returns>
        public int SelectOrgByName(string orgName)
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.OrgTypeManage.SelectByName", ref sql) == -1)
                return 0;
            sql = string.Format(sql, orgName);
            if (this.ExecQuery(sql) == -1)
                return 0;
            int orgTypeID = 0;
            while (this.Reader.Read())
            {
                OrgType org = SetOrgType();
                orgTypeID = org.OrgTypeID;
            }
            this.Reader.Close();
            return orgTypeID; 
        }

        /// <summary>
        /// �������еı걾��֯����
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public Dictionary<int, string> GetAllOrgType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.OrgTypeManage.SelectAll", ref sql) == -1)
                return null;
            
            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicOrgType = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                OrgType org = SetOrgType();
                dicOrgType.Add(org.OrgTypeID, org.OrgName);
            }
            this.Reader.Close();
            return dicOrgType;
        }

        /// <summary>
        /// ����spectypeid��ȡorgtype
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        public OrgType GetBySpecType(string specTypeId)
        {
            ArrayList arr = this.GetOrgType("Speciment.BizLogic.OrgTypeManage.SelectBySpecId", new string[] { specTypeId });
            if (arr == null || arr.Count <= 0)
                return null;
            else
                return arr[0] as OrgType;
        }
        #endregion
    }
}
