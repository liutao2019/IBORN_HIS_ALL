using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FS.HISFC.Components.OutpatientFee.Forms
{
    public partial class frmModifyUserKeys : Form
    {
        public frmModifyUserKeys()
        {
            InitializeComponent();
        }

        #region ����
        
        /// <summary>
        /// ����·��
        /// </summary>
        public string filePath = Application.StartupPath  + @".\" + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\clinicShotcut.xml";

        #endregion

        #region ����

        /// <summary>
        /// ˢ�¹�ϣ����Ϣ
        /// </summary>
        public void RefreshHashCode()
        {
            int row = this.fpSpead1_Sheet1.ActiveRowIndex;
            int col = this.fpSpead1_Sheet1.ActiveColumnIndex;
            if (this.fpSpead1_Sheet1.RowCount <= 0)
            {
                return;
            }
            int finHashCode = 0; int groupHashCode = 0; int useHashCode = 0;
            if (this.fpSpead1_Sheet1.Cells[row, 2].Text != string.Empty)//��ϼ�Ϊ��
            {
                groupHashCode = Convert.ToInt32(this.fpSpead1_Sheet1.Cells[row, 2].Tag.ToString());
            }
            if (this.fpSpead1_Sheet1.Cells[row, 3].Text == string.Empty)//���ܼ�Ϊ��
            {
                this.fpSpead1_Sheet1.Cells[row, 4].Text = string.Empty;
                return;
            }
            else
            {
                useHashCode = Convert.ToInt32(this.fpSpead1_Sheet1.Cells[row, 3].Tag.ToString());
            }
            finHashCode = groupHashCode + useHashCode;

            this.fpSpead1_Sheet1.Cells[row, 4].Text = finHashCode.ToString();
        }
        /// <summary>
        /// ��֤¼���Ƿ�Ϸ�
        /// </summary>
        /// <param name="beginRow">��ʼ��֤������</param>
        /// <returns>false���Ϸ� true�Ϸ�</returns>
        public bool IsValid(int beginRow)
        {
            if (beginRow >= this.fpSpead1_Sheet1.RowCount)
            {
                return true;
            }
            string tmpHashCode = this.fpSpead1_Sheet1.Cells[beginRow, 4].Text;
            for (int i = beginRow + 1; i < this.fpSpead1_Sheet1.RowCount; i++)
            {
                string currHashCode = this.fpSpead1_Sheet1.Cells[i, 4].Text;
                if (currHashCode != string.Empty && currHashCode == tmpHashCode)
                {
                    return false;
                }
            }
            if (this.IsValid(beginRow + 1) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// �����͸��������ļ�
        /// </summary>
        /// <param name="filePath">�����ļ�·��</param>
        /// <returns>false����ʧ�� true�����ɹ�</returns>
        public bool CreateXml(string filePath)
        {
            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            root = myXml.CreateRootElement(doc, "Setting", "1.0");
            for (int i = 0; i < this.fpSpead1_Sheet1.RowCount; i++)
            {
                XmlElement e = myXml.AddXmlNode(doc, root, "Column", string.Empty);
                myXml.AddNodeAttibute(e, "opCode", this.fpSpead1_Sheet1.Cells[i, 0].Text);
                myXml.AddNodeAttibute(e, "opName", this.fpSpead1_Sheet1.Cells[i, 1].Text);
                myXml.AddNodeAttibute(e, "opKey", this.fpSpead1_Sheet1.Cells[i, 2].Text);
                string opKeyHash = string.Empty, cuKeyHash = string.Empty;
                if (this.fpSpead1_Sheet1.Cells[i, 2].Text != string.Empty)
                {
                    opKeyHash = this.fpSpead1_Sheet1.Cells[i, 2].Tag.ToString();
                }
                myXml.AddNodeAttibute(e, "opKeyHash", opKeyHash);
                myXml.AddNodeAttibute(e, "cuKey", this.fpSpead1_Sheet1.Cells[i, 3].Text);
                if (this.fpSpead1_Sheet1.Cells[i, 3].Text != string.Empty)
                {
                    cuKeyHash = this.fpSpead1_Sheet1.Cells[i, 3].Tag.ToString();
                }
                myXml.AddNodeAttibute(e, "cuKeyHash", cuKeyHash);
                myXml.AddNodeAttibute(e, "hash", this.fpSpead1_Sheet1.Cells[i, 4].Text);
            }
            try
            {
                StreamWriter sr = new StreamWriter(filePath, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("�޷����棡" + ex.Message); 
            }

            return true;
        }
        /// <summary>
        /// ���ݹ��ܴ��������
        /// </summary>
        /// <param name="opCode">���ܴ���</param>
        /// <returns>-1û���ҵ� ����Ϊ�ҵ��ĺ���</returns>
        public int FindRow(string opCode)
        {
            for (int i = 0; i < this.fpSpead1_Sheet1.RowCount; i++)
            {
                string tmpCode = this.fpSpead1_Sheet1.Cells[i, 0].Text;
                if (tmpCode == opCode)
                {
                    return i;
                }
            }

            return -1;
        }
        /// <summary>
        /// ��ȡXML��Ϣ
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadFromXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == string.Empty) return;
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch 
            { 
                return; 
            }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {

                string opCode = node.Attributes["opCode"].Value;
                int findRow = FindRow(opCode);
                if (findRow < 0)
                {
                    continue;
                }
                this.fpSpead1_Sheet1.Cells[findRow, 2].Text = node.Attributes["opKey"].Value;
                this.fpSpead1_Sheet1.Cells[findRow, 3].Text = node.Attributes["cuKey"].Value;
                this.fpSpead1_Sheet1.Cells[findRow, 2].Tag = node.Attributes["opKeyHash"].Value;
                this.fpSpead1_Sheet1.Cells[findRow, 3].Tag = node.Attributes["cuKeyHash"].Value;
                this.fpSpead1_Sheet1.Cells[findRow, 4].Text = node.Attributes["hash"].Value;

            }
        }

        /// <summary>
        /// �����ļ�·���Ͱ���HashCode���ҵ�ǰֵ
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <param name="hashCode">����HashCode</param>
        /// <returns>�ɹ� ��ǰֵ ʧ�� string.Empty</returns>
        public string Operation(string filePath, string hashCode)
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == string.Empty) return string.Empty;
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch { return string.Empty; }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["hash"].Value == hashCode)
                {
                    return node.Attributes["opCode"].Value;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="code">��ǰֵ</param>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        public int OperationExe(string code)
        {
            switch (code)
            {
                case "1":
                    MessageBox.Show("Save");
                    break;
                case "2":
                    MessageBox.Show("���۱���!");
                    break;
                case "3":
                    MessageBox.Show("����");
                    break;
            }

            return 1;
        }

        #endregion

        #region �¼�

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bReturn = IsValid(0);
            if (!bReturn)
            {
                MessageBox.Show("��ݼ����ò����ظ�!");
                return;
            }
            bReturn = CreateXml(filePath);
            if (!bReturn)
            {
                MessageBox.Show("����ʧ��!");
                return;
            }
            else
            {
                MessageBox.Show("����ɹ�!");
            }
        }

        private void fpSpead1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int row = this.fpSpead1_Sheet1.ActiveRowIndex;
            int col = this.fpSpead1_Sheet1.ActiveColumnIndex;
            if (this.fpSpead1_Sheet1.RowCount <= 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                if (col <= 1)
                {
                    return;
                }
                this.fpSpead1_Sheet1.SetValue(row, col, string.Empty);
                this.fpSpead1_Sheet1.Cells[row, col].Tag = null;

                RefreshHashCode();

                return;

            }
            if (e.KeyCode != Keys.LButton)
            {
                if (col <= 1 || col == 4)
                {
                    return;
                }
                if (col == 2)
                {
                    if (e.KeyCode == Keys.ControlKey)
                    {
                        this.fpSpead1_Sheet1.SetValue(row, col, Keys.Control.ToString());
                        this.fpSpead1_Sheet1.Cells[row, col].Tag = Keys.Control.GetHashCode();
                    }
                }
                else
                {
                    this.fpSpead1_Sheet1.SetValue(row, col, e.KeyCode.ToString());
                    this.fpSpead1_Sheet1.Cells[row, col].Tag = e.KeyCode.GetHashCode();
                }

                RefreshHashCode();
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmModifyUserKeys_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.ReadFromXml(this.filePath);
            }
            catch { }
        }

        #endregion
    }
}