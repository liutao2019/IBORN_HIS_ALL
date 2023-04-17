using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾��֯��ṹ����]<br></br>
    /// [�� �� ��: �ֹ���]<br></br>
    /// [����ʱ��: 2011-10-14]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class QueryEngineManage : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ����û�����������
        /// </summary>
        public class Table
        {
            private string tableName;
            private KeyValuePair<string,string>[] tableColumn;
            private KeyValuePair<string, string>[] tableChineseNote;
            /// <summary>
            /// ����
            /// </summary>
            public string TableName
            {
                get
                {
                    return tableName;
                }
                set
                {
                    tableName = value;
                }
            }
            /// <summary>
            /// ��������
            /// </summary>
            public KeyValuePair<string, string>[] TableColumn
            {
                get
                {
                    return tableColumn;
                }
                set
                {
                    tableColumn = value;
                }
            }

            public KeyValuePair<string, string>[] ColumnChinese
            {
                get
                {
                    return tableChineseNote;
                }
                set
                {
                    tableChineseNote = value;
                }
            }
        }

        /// <summary>
        /// ����sql������ȡ�û��������
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList GetTableProperity(string sqlIndex, string[] parm)
        {
            ArrayList arrTablePoertiy = new ArrayList();
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            if (parm.Length <= 0)
            {
                while (this.Reader.Read())
                {
                    arrTablePoertiy.Add(this.Reader[0].ToString());
                }
            }
            else
            {
                while (this.Reader.Read())
                {
                    string key = Reader[0].ToString();
                    string value =Reader[1].ToString();
                    KeyValuePair<string, string> tmp = new KeyValuePair<string, string>(key,value);
                    arrTablePoertiy.Add(tmp); 
                }
            }
            this.Reader.Close();
            return arrTablePoertiy;
        }

        public Dictionary<string, string> GetTableName()
        {
            ArrayList arrTableName = GetTableProperity("Speciment.BizLogic.QueryEngineManage.QueryTableName", new string[] { });
            Dictionary<string, string> tableNameNote = new Dictionary<string, string>();
            foreach (string tableName in arrTableName)
            {
                switch (tableName)
                {
                    case "SPEC_APPLICATIONTABLE":
                        tableNameNote.Add(tableName, "�û������¼");
                        break;
                    case "SPEC_BOX":
                        tableNameNote.Add(tableName, "�걾��");
                        break;
                    case "SPEC_ICEBOX":
                        tableNameNote.Add(tableName, "����");
                        break;
                    case "SPEC_ICEBOXLAYER":
                        tableNameNote.Add(tableName, "�����");
                        break;
                    case "SPEC_SHELF":
                        tableNameNote.Add(tableName, "�����");
                        break;
                    default:
                        tableNameNote.Add(tableName, tableName);
                        break;
                }
            }
            return tableNameNote;
        }

        public Table GetTableProperity(string tableName)
        {
            ArrayList tableColumns = this.GetTableProperity("Speciment.BizLogic.QueryEngineManage.QueryTableColumns", new string[] { tableName });
            Table table = new Table();
            //��¼�ֶ������ֶ���������
            KeyValuePair<string, string>[] columns = new KeyValuePair<string, string>[tableColumns.Count];
            int i = 0;
            foreach (KeyValuePair<string, string> column in tableColumns)
            {
                columns[i] = column;
                i++;
            }
            table.TableName = tableName;
            table.TableColumn = columns;
            return table;
        }

        /// <summary>
        /// ����sql��ѯ���
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="ds">dataset</param>
        public void Query(string sql, ref DataSet ds)
        {
            this.ExecQuery(sql, ref ds); 
        }
    }
}
