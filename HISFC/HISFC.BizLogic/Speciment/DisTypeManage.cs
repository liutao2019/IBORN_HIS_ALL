using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �������͹���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-15]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class DisTypeManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="disType">�걾��֯���Ͷ���</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.DiseaseType disType)
        {
            string[] str;
            str = new string[]
						{
							disType.DisTypeID.ToString(), 
                            disType.DiseaseName,
                            disType.DiseaseColor,
                            disType.Comment,
                            disType.OrgOrBld,
                            disType.Ext1,
                            disType.Ext2,
                            disType.Ext3
						};
            return str; 
            
        }

        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.DisTypeManage.GetNextSequence");
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

        #region ���²������Ͳ���
        /// <summary>
        /// ���²�������
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateDisType(string sqlIndex, params string[] args)
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>����ʵ��</returns>
        private DiseaseType SetDisType()
        {
            DiseaseType disType = new DiseaseType();
            try
            {
                 disType.DisTypeID = Convert.ToInt32(this.Reader[0].ToString());
                disType.DiseaseName = this.Reader[1].ToString();
                disType.DiseaseColor = this.Reader[2].ToString();
                disType.Comment = this.Reader[3].ToString();
                disType.OrgOrBld = this.Reader[4].ToString();
                disType.Ext1 = this.Reader[5].ToString();
                disType.Ext2 = this.Reader[6].ToString();
                disType.Ext3 = this.Reader[7].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return disType;
        }

        /// <summary>
        /// ��ȡ���������б�
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetDisType(string sqlIndex, params string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            sql = string.Format(sql,parm);
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrDis = new ArrayList();
           
            while (this.Reader.Read())
            {
                DiseaseType dis = SetDisType();
                arrDis.Add(dis);
            }
            this.Reader.Close();
            return arrDis;
        }

        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// ����ʵ�����
        /// </summary>
        /// <param name="disType">��������Ĳ���ʵ��</param>
        /// <returns>Ӱ�����������1��ʧ��</returns>
        public int InsertOrgType(FS.HISFC.Models.Speciment.DiseaseType disType)
        {
            return this.UpdateDisType("Speciment.BizLogic.DisTypeManage.Insert", this.GetParam(disType));
           
        }

        /// <summary>
        /// �������Ƹ��²���
        /// </summary>
        /// <param name="disType"></param>
        /// <returns></returns>
        public int UpdateDisType(DiseaseType disType)
        {
            return this.UpdateDisType("Speciment.BizLogic.DisTypeManage.Update", this.GetParam(disType));

        }

        /// <summary>
        /// ��������ɾ������
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int DeleteOrgTypeByName(string typeName)
        {
            return this.UpdateDisType("Speciment.BizLogic.DisTypeManage.Delete", new string[] { typeName });
        }

        /// <summary>
        /// ����ID��ȡ��������
        /// </summary>
        /// <param name="disID"></param>
        /// <returns></returns>
        public DiseaseType SelectDisByID(string disID)
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectByID", ref sql) == -1)
                return null;
            sql = string.Format(sql, disID);
            if (this.ExecQuery(sql) == -1)
                return null;
            DiseaseType dis = new DiseaseType();
            while (this.Reader.Read())
            {
                dis = SetDisType();               
            }
            this.Reader.Close();
            return dis;
        }

        /// <summary>
        /// �������еĲ�������
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public Dictionary<int, string> GetAllDisType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectAll", ref sql) == -1)
                return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicDisType = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                DiseaseType dis = SetDisType();
                if (dis.DiseaseName.Length == 2 || dis.DiseaseName == "��" || dis.DiseaseName == "��" || dis.DiseaseName.Length == 4)
                {
                   dicDisType.Add(dis.DisTypeID, dis.DiseaseName);
                }
            }
            this.Reader.Close();
            return dicDisType;
        }

        /// <summary>
        /// �������еĲ�������
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public ArrayList GetAllValidDisType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectAllValid", ref sql) == -1)
                return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            FS.HISFC.Models.Base.Const obj = null;
            ArrayList alDisType = new ArrayList();
            while (this.Reader.Read())
            {
                obj = new FS.HISFC.Models.Base.Const();
                DiseaseType dis = SetDisType();
                if (dis != null)
                {
                    obj.ID = dis.DisTypeID.ToString();
                    obj.Name = dis.DiseaseName;
                    obj.SpellCode = dis.Ext1;
                    obj.WBCode = dis.Ext2;
                    obj.UserCode = dis.Ext3;
                    alDisType.Add(obj);
                }
            }
            this.Reader.Close();
            return alDisType;
        }

        /// <summary>
        /// �������еĲ�������
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public ArrayList QueryAllDisType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectAll", ref sql) == -1)
                return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList dicDisType = new ArrayList();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                DiseaseType dis = SetDisType();
                if (dis.DiseaseName.Length == 2 || dis.DiseaseName == "��" || dis.DiseaseName == "��")
                {
                    dicDisType.Add(dis);
                }
            }
            this.Reader.Close();
            return dicDisType;
        } 

        /// <summary>
        /// ������֯�Ĳ�������
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public Dictionary<int, string> GetOrgDisType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectOrg", ref sql) == -1)
                return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicDisType = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                DiseaseType dis = SetDisType();
                dicDisType.Add(dis.DisTypeID, dis.DiseaseName);                
            }
            this.Reader.Close();
            return dicDisType;
        }


        /// <summary>
        /// ����Ѫ�Ĳ�������
        /// </summary>
        /// <returns>�걾��֯����List</returns>
        public Dictionary<int, string> GetBldDisType()
        {
            string sql = "";
            if (this.Sql.GetSql("Speciment.BizLogic.DisTypeManage.SelectBld", ref sql) == -1)
                return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<int, string> dicDisType = new Dictionary<int, string>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                DiseaseType dis = SetDisType();
                dicDisType.Add(dis.DisTypeID, dis.DiseaseName);
            }
            this.Reader.Close();
            return dicDisType;
        }


        /// <summary>
        /// ����boxId���ұ걾���д�ŵĲ�������
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public DiseaseType GetDisTypeByBoxId(string boxId)
        {
            return this.GetDisType("Speciment.BizLogic.DisTypeManage.GetByBoxId", new string[] { boxId })[0] as DiseaseType;
        }

        /// <summary>
        /// ����������Ժ��϶�Ӧ�Ĳ�λ������
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetDiagToDis()
        {
            string sql = "select name, DISEASETYPEID from SPEC_DISEASETYPE, COM_DICTIONARY c where c.TYPE = 'DiagnosebyNurse' and c.INPUT_CODE = DISEASENAME";
            //if (this.Sql.GetSql("FS.HISFC.BizLogic.Speciment.DisType.SelectAll", ref sql) == -1)
            //    return null;

            if (this.ExecQuery(sql) == -1)
                return null;
            Dictionary<string, int> dicDiaToDis = new Dictionary<string, int>();
            //ArrayList specList = new ArrayList();
            while (this.Reader.Read())
            {
                if (Reader["NAME"] != null && Reader["DISEASETYPEID"] != null)
                {
                    string diagName = Reader["NAME"].ToString();
                    if (Reader["DISEASETYPEID"].ToString().Trim() == "")
                    {
                        continue;
                    }
                    if (!dicDiaToDis.ContainsKey(diagName))
                    {
                        dicDiaToDis.Add(diagName,Convert.ToInt32(Reader["DISEASETYPEID"].ToString()));
                    }
                }
            }
            this.Reader.Close();
            return dicDiaToDis;
        }
        #endregion
    }
}
