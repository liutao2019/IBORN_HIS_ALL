using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Data.Odbc;

namespace FoShanSI.Components
{
    /// <summary>
    /// Connect UC
    /// 张琦 2010-7
    /// </summary>
    public partial class ucConnectServer : UserControl
    {
        public ucConnectServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        private string errText = string.Empty;

        /// <summary>
        /// 医保测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnectTest_Click(object sender, EventArgs e)
        {
            string dbInstance = this.tbServer.Text.Trim();
            string dataBaseName = this.tbDataBase.Text.Trim();
            string userName = this.tbUserName.Text.Trim();
            string passWord = this.tbPassword.Text.Trim();

            //DataProvider Connect
            //string connString = "server=" + dbInstance + ";database=" + dataBaseName + ";uid=" + userName + ";pwd=" + passWord;

          // string  connString = "driver={IBM   DB2   ODBC   DRIVER};DSN=" + dataBaseName + ";uid=" + userName + ";pwd=" + passWord;
            //Driver={IBM DB2 ODBC DRIVER};Database=myDataBase;Hostname=myServerAddress;Port=1234; Protocol=TCPIP;Uid=myUsername;Pwd=myPassword;

           // string connString = "driver={IBM   DB2   ODBC   DRIVER};Database=" + dataBaseName + ";Hostname=bjyy-fssb;" + "port=50000;" + "Protocol=TCPIP;" + "uid=" + userName + ";pwd=" + passWord;

            string connString = "DSN=" + dataBaseName + ";uid=" + userName + ";pwd=" + passWord;
            
            //System.Data.IDbConnection siConnect = new IBM.Data.DB2.DB2Connection(connString);
            System.Data.IDbConnection siConnect = new OdbcConnection(connString);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                siConnect.Open();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("测试连接失败:" + ex.Message);
                return;
            }
            Cursor.Current = Cursors.Arrow;

            MessageBox.Show("连接成功!");
            siConnect.Close();
        }

        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucConnectServer_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Function.Function.CreatXmlDoc())
                { return; }
            }
            catch { }
            this.InitControl();
            this.FindForm().Text = "佛山医保连接";
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";
            //从xml文件中读取数据库配置属性
            XmlDocument xmlDoc = Function.Function.GetXmlDoc();
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            //加载医保类型列表
            ArrayList alPactType = Function.Function.GetArrayListFromXmlNodes(rootNode.ChildNodes);
            //this.cmbPactType.DataSource = alPactType;
            //this.cmbPactType.ValueMember = "ID";
            //this.cmbPactType.DisplayMember = "Name";

            //int selectIndex;
            XmlNode node = Function.Function.GetUsedProfileNode();
            //this.cmbPactType.SelectedIndex = selectIndex;
            try
            {
                dbInstance = node.Attributes["数据库实例名"].Value.ToString();
                DataBaseName = node.Attributes["数据库名"].Value.ToString();
                userName = node.Attributes["用户名"].Value.ToString();
                password = node.Attributes["密码"].Value.ToString();
            }
            catch { return; }

            //将数据库配置属性显示在控件中
            this.tbServer.Text = dbInstance;
            this.tbDataBase.Text = DataBaseName;
            this.tbUserName.Text = userName;
            this.tbPassword.Text = password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject objChanged=new FS.FrameWork.Models.NeuObject();
            objChanged.ID=this.tbServer.Text;
            objChanged.Name=this.tbDataBase.Text;
            objChanged.Memo=this.tbUserName.Text;
            objChanged.User01=this.tbPassword.Text;
            if (!Function.Function.SaveXmlDoc(0, objChanged, ref errText))
            {
                MessageBox.Show("保存失败！"+errText);
            }
            else
            {
                MessageBox.Show("保存成功！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
