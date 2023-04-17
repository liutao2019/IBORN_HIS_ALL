using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using FS.FrameWork.Management;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using FS.HISFC.Components.Speciment;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        private static int conditionNum = 2;
        private QueryEngineManage query = new QueryEngineManage();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string fileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }
            string connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=""Excel 8.0;HDR=YES;""";
            connectString += " ;Data Source= " + fileName + ";";
            textBox1.Text = fileName;
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            using (DbDataAdapter adapter = factory.CreateDataAdapter())
            {
                using (DbCommand command = factory.CreateCommand())
                {
                    DbConnection conn = factory.CreateConnection();
                    conn.ConnectionString = connectString;
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM[Sheet1$]";
                    adapter.SelectCommand = command;
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    ds.Tables[0].TableName = "TABLE1";
                    //Transaction trans = this.conn
                    //ds.Tables[0].TableName = "TABLE1";
                    //IBM.Data.DB2.DB2CommandBuilder cmdb = new IBM.Data.DB2.DB2CommandBuilder(adapter);
                    //adapter.InsertCommand = cmdb.GetInsertCommand();
                    //adapter.Update(ds);
                    ExlToDb2Manage manage = new ExlToDb2Manage();
                    manage.InsertDataFromExl(ds, ds.Tables[0].TableName);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> table = query.GetTableName();
            ListViewItem item;
            foreach (KeyValuePair<string, string> t in table)
            {
                item = new ListViewItem();
                item.Text = t.Value;
                item.Tag = t.Key;
                this.listTable.Items.Add(item);
            }

        }

        private void listTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            listCol.Clear();
            string tableName = "";
            foreach (ListViewItem item in listTable.SelectedItems)
            {
                tableName = item.Tag.ToString();
                break;
            }
            QueryEngineManage.Table table = query.GetTableProperity(tableName);
            KeyValuePair<string, string>[] tablePoerity = table.TableColumn;
            ListViewItem listItem;
            List<string> listSelCol = new List<string>();
            //遍历已加载的字段，防止被重复加载
            foreach (ListViewItem i in listSelColumn.Items)
            {
                listSelCol.Add(i.Tag.ToString());
            }
            foreach (KeyValuePair<string, string> p in tablePoerity)
            {
                listItem = new ListViewItem();

                listItem.Tag = p;
                listItem.ToolTipText = tableName;
                listItem.Text = tableName + "." + p.Key;
                //listItem.Name = tableName + "." + p.Key;

                if(!listSelCol.Contains(p.ToString()))
                {
                    listCol.Items.Add(listItem);
                }
            }
        }

        private void btnAddCol_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listCol.SelectedItems)
            {
                listCol.Items.Remove(i);
                listSelColumn.Items.Add(i);
            }
        }

        private void btnRemoveCol_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listSelColumn.SelectedItems)
            {
                listSelColumn.Items.Remove(i);
                listCol.Items.Add(i);
            }
        }

        private Dictionary<string, string> FieldBinding()
        {
            Dictionary<string, string> dicSelFiled = new Dictionary<string, string>();
            foreach (ListViewItem item in listSelColumn.Items)
            {
                KeyValuePair<string, string> selField = (KeyValuePair<string, string>)(item.Tag);
                dicSelFiled.Add(item.Text, selField.Value);
            }
            return dicSelFiled;
        }

        private void GetSql()
        {
            string sql = "select ";
            List<string> table = new List<string>();
            int index = 0;
            int count = listSelColumn.Items.Count;
            foreach (ListViewItem item in listSelColumn.Items)
            {
                if (index < count - 1)
                {
                    sql += (item.Text + ",");
                }
                else
                {
                    sql += item.Text;
                }
                if (!table.Contains(item.ToolTipText))
                {
                    table.Add(item.ToolTipText);
                }
                index++;
            }            
            sql += " from ";
            index = 0;
            count = table.Count;
            foreach (string t in table)
            {
                if (index < count - 1)
                {
                    sql += (t + ",");
                }
                else
                {
                    sql += t;
                }
                index++;                
            }        
            txtShowField.Text = sql;
        }
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dicSelFiled = FieldBinding();            
            this.ucCondition1.FieldBinding(dicSelFiled);
            GetSql();
        }

        private void btnAddCon_Click(object sender, EventArgs e)
        {
            //ucCondition condition = new ucCondition();
            //condition.Name = "ucCondition" + conditionNum.ToString();
            //conditionNum++;
            //condition.FieldBinding(FieldBinding());
            //this.flpCondition.Controls.Add(condition);
        }
    }
}