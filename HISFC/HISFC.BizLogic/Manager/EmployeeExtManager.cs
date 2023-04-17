using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Neusoft.HISFC.BizLogic.Manager
{
    /// <summary>
    ///<br>[��������: Ա���ֶ���չҵ����]</br>
    ///<br>[�� �� ��: �εº�]</br>
    ///<br>[����ʱ��: 2008-09-25]</br>
    ///    <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class EmployeeExtManager : Neusoft.FrameWork.Management.Database
    {

        /// <summary>
        /// ������չ�м�¼����
        /// </summary>
        /// <param name="expandColumn">��չ��</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int InsertExpandColumn(Neusoft.HISFC.Models.HR.ExpandColumn expandColumn)
        {
            return this.ExecUpdateSql("HR.ExpandColumn.InsertExpandColumn", GetExpandColumn(expandColumn));
        }

        /// <summary>
        /// ������չ�м�¼����
        /// </summary>
        /// <param name="expandColumn">��չ��</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int InsertExpandColumnData(Neusoft.HISFC.Models.HR.ExpandColumn expandColumn)
        {
            return this.ExecUpdateSql("HR.ExpandColumnData.InsertExpandColumnData", new string[] { expandColumn.TableName,expandColumn.ColumnName,expandColumn.ID,expandColumn.Memo});
        }
        /// <summary>
        /// ������չ�м�¼����
        /// </summary>
        /// <param name="expandColumn">��չ��</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int UpdateExpandColumn(Neusoft.HISFC.Models.HR.ExpandColumn expandColumn)
        {
            return this.ExecUpdateSql("HR.ExpandColumn.UpdateExpandColumn", GetExpandColumn(expandColumn));
        }

        /// <summary>
        /// ������չ�м�¼����
        /// </summary>
        /// <param name="expandColumn">��չ��</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int UpdateExpandColumnData(Neusoft.HISFC.Models.HR.ExpandColumn expandColumn)
        {
            return this.ExecUpdateSql("HR.ExpandColumnData.UpdateExpandColumnData", new string[] { expandColumn.TableName, expandColumn.ColumnName, expandColumn.ID, expandColumn.Memo });
        }
        /// <summary>
        /// ɾ����չ�м�¼����
        /// </summary>
        /// <param name="expandColumn">��չ��</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int DeleteExpandColumn(string tableName,string columnName)
        {
            return this.ExecUpdateSql("HR.ExpandColumn.DeleteExpandColumn", new string[] { tableName,columnName});
        }
         /// <summary>
        /// ����ִ�в��롢���¡�ɾ�����
        /// </summary>
        /// <param name="strSql">��ִ�е�SQL����</param>
        /// <param name="strParam">�������Ĳ�������</param>
        /// <returns>���ز������ֵ������1��������ɹ�������0����û�н��в�����-1�������ʧ��</returns>
        public int ExecUpdateSql(string sqlParam, string[] strParam)
        {
            //���������������ΪSQL�������
            string strSql = "";
            //���ڵõ�Sql���
            if (this.Sql.GetSql(sqlParam, ref strSql) == -1)
            {
                this.Err = "�������Ϊ" + sqlParam + "��SQL������!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, strParam);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }
     
        /// <summary>
        /// �����չ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetExpandColumn()
        {

            string strsql = string.Empty;
            DataSet ds = new DataSet();
            if (this.Sql.GetSql("HR.ExpandColumn.QueryExpandColumn", ref strsql) == -1)
            {
                this.Err = "û���ҵ���HR.ExpandColumn.QueryExpandColumn��SQL���";

                return null;
            }

            strsql = string.Format(strsql);
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                this.Err = "ִ��" + strsql + "��������" + this.Err;

                return null;
            }

            return ds;

        }

        /// <summary>
        /// ͨ�������õ��нṹ
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet QueryExpandColumnByTableNameAndObjID(string tableName,string objID)
        {

            string strsql = string.Empty;
            DataSet ds = new DataSet();
            if (this.Sql.GetSql("HR.ExpandColumnData.QueryExpandColumnDataByTabNameAndObjID", ref strsql) == -1)
            {
                this.Err = "û���ҵ���HR.ExpandColumnData.QueryExpandColumnDataByTabNameAndObjID��SQL���";

                return null;
            }

            strsql = string.Format(strsql, tableName,objID);
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                this.Err = "ִ��" + strsql + "��������" + this.Err;

                return null;
            }

            return ds;

        }

        /// <summary>
        /// ͨ�������õ��нṹ
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet QueryExpandColumnByTableName(string tableName)
        {

            string strsql = string.Empty;
            DataSet ds = new DataSet();
            if (this.Sql.GetSql("HR.ExpandColumn.QueryExpandColumnByTableName", ref strsql) == -1)
            {
                this.Err = "û���ҵ���HR.ExpandColumn.QueryExpandColumnByTableName��SQL���";

                return null;
            }

            strsql = string.Format(strsql,tableName);
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                this.Err = "ִ��" + strsql + "��������" + this.Err;

                return null;
            }

            return ds;

        }
        /// <summary>
        /// �����չ������
        /// </summary>
        /// <returns></returns>
        public DataSet GetExpandColumnData(string tableName,string columnName)
        {

            string strsql = string.Empty;
            DataSet ds = new DataSet();
            if (this.Sql.GetSql("HR.ExpandColumnData.QueryExpandColumnData", ref strsql) == -1)
            {
                this.Err = "û���ҵ���HR.ExpandColumnData.QueryExpandColumnData��SQL���";

                return null;
            }

            strsql = string.Format(strsql,tableName,columnName);
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                this.Err = "ִ��" + strsql + "��������" + this.Err;

                return null;
            }

            return ds;

        }

        #region ��ʵ��ת���ɲ�������
        /// <summary>
        /// ����ʵ��õ���������
        /// </summary>
        /// <param name="expandColumn">��չ�ֶ�ʵ��</param>
        /// <returns>ʵ��Ĳ�������</returns>
        private string[] GetExpandColumn(Neusoft.HISFC.Models.HR.ExpandColumn expandColumn)
        {
            string[] strParam = new string[]
            {
                expandColumn.TableName ,
                expandColumn.ColumnName,
                expandColumn.ColumnType,
                expandColumn.ColumnLength.ToString(),
                expandColumn.ColumnDecimalLen.ToString(),
                expandColumn.DefaultValue,
                Neusoft.FrameWork.Function.NConvert.ToInt32(expandColumn.IsNull).ToString(),
                Neusoft.FrameWork.Function.NConvert.ToInt32( expandColumn.IsValid).ToString(),
               
                expandColumn.Remark ,
               expandColumn.SortID.ToString()
            };
            return strParam;

        }
        #endregion
    }
}
