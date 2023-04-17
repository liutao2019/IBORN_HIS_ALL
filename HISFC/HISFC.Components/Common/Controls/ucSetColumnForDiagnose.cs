using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;

namespace Neusoft.UFC.Common.Controls
{
    /*----------------------------------------------------------------
    // Copyright (C) 2004 ���������޹�˾
    // ��Ȩ���С� 
    //
    // �ļ�����ucSetColumnForDiagnose
    // �ļ�������������������������Ͽؼ�������Ϣ�������䱣�浽���������ļ��С�
    //
    // 
    // ������ʶ��leiyj 2007-07-18
    //
    // �޸ı�ʶ��
    // �޸�������
    //
    // �޸ı�ʶ��
    // �޸�������
    ----------------------------------------------------------------*/

    public partial class ucSetColumnForDiagnose : UserControl
    {
        public ucSetColumnForDiagnose()
        {
            InitializeComponent();
            this.allColumns = new ArrayList();
            this.showColumns = new ArrayList();
            this.ConfigDoc = new XmlDocument();
        }

        public ucSetColumnForDiagnose(ArrayList allCols,ArrayList showCols)
        {
            InitializeComponent();
            this.allColumns = allCols;
            this.showColumns = showCols;
            this.ConfigDoc = new XmlDocument();
        }

        #region ����
        //���е���
        public ArrayList allColumns;

        //��ʾ����
        public ArrayList showColumns ;

        //�����ļ���
        public string configFileName = Application.StartupPath + @"\profile\DiagnoseInputSetting.xml";

        //�����ļ�
        private XmlDocument ConfigDoc ;
        #endregion

        #region ����

        /// <summary>
        /// �����޸ĵ������ļ��������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //�ж�ѡ�����Ƿ�Ϊ��
            if (this.chklsSetColumn.CheckedItems.Count != 0)
            {
                this.showColumns.Clear();
                //���ڵ�
                XmlNode setNode = this.ConfigDoc.SelectSingleNode("//Setting");

                //�ж����޸ÿؼ��ڵ�
                if (this.ConfigDoc.SelectNodes("//Setting//" + this.Name).Count != 0)
                {
                    setNode.RemoveChild(this.ConfigDoc.SelectSingleNode("//Setting//"+this.Name));
                }

                //�½��ؼ��ڵ�
                XmlNode controlNode = this.ConfigDoc.CreateNode(XmlNodeType.Element, this.Name, this.ConfigDoc.NamespaceURI);

                //��д�ؼ��ڵ�
                for (int i = 0; i < this.chklsSetColumn.Items.Count; i++)
                {
                    XmlNode columnNode = this.ConfigDoc.CreateNode(XmlNodeType.Element, "Column", this.ConfigDoc.NamespaceURI);

                    XmlAttribute name = this.ConfigDoc.CreateAttribute("name");
                    name.Value = this.chklsSetColumn.Items[i].ToString();

                    XmlAttribute index = this.ConfigDoc.CreateAttribute("index");
                    index.Value = i.ToString();

                    XmlAttribute show = this.ConfigDoc.CreateAttribute("show");
                    if (this.chklsSetColumn.GetItemChecked(i))
                    {
                        show.Value = "true";

                        //�����ʾ��
                        this.showColumns.Add(i);
                    }
                    else
                    {
                        show.Value = "false";
                    }

                    columnNode.Attributes.Append(name);
                    columnNode.Attributes.Append(index);
                    columnNode.Attributes.Append(show);

                    controlNode.AppendChild(columnNode);
                }

                //���ؼ��ڵ���ӵ����ڵ�
                setNode.AppendChild(controlNode);

                try
                {
                    this.ConfigDoc.Save(this.configFileName);
                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
                catch
                {
                    MessageBox.Show("���������ļ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("���¼�����Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ȡ���޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (this.chklsSetColumn.SelectedItem != null)
            {
                if (this.chklsSetColumn.SelectedIndex != 0)
                {
                    System.Object selObj = this.chklsSetColumn.SelectedItem;
                    int selIndex=this.chklsSetColumn.SelectedIndex;
                    this.chklsSetColumn.Items.Remove(selObj);
                    this.chklsSetColumn.Items.Insert(selIndex - 1, selObj);
                    this.chklsSetColumn.SetSelected(selIndex - 1, true);
                    this.chklsSetColumn.SetItemChecked(selIndex - 1, true);
                }
            }
            else
            {
                MessageBox.Show("����û��ѡ����Ŀ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ѡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.chklsSetColumn.SelectedItem != null)
            {
                if (this.chklsSetColumn.SelectedIndex != this.chklsSetColumn.Items.Count - 1)
                {
                    System.Object selObj = this.chklsSetColumn.SelectedItem;
                    int selIndex=this.chklsSetColumn.SelectedIndex;
                    this.chklsSetColumn.Items.Remove(selObj);
                    this.chklsSetColumn.Items.Insert(selIndex + 1, selObj);
                    this.chklsSetColumn.SetSelected(selIndex + 1, true);
                    this.chklsSetColumn.SetItemChecked(selIndex + 1, true);
                }
            }
            else
            {
                MessageBox.Show("����û��ѡ����Ŀ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ�����ļ�������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucSetColumnForDiagnose_Load(object sender, EventArgs e)
        {
            //��ȡ�����ļ�
            if(System.IO.File.Exists(this.configFileName))
            {
                try
                {
                    this.ConfigDoc.Load(this.configFileName);
                }
                catch(Exception err)
                {
                    MessageBox.Show("������������ļ��д���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("������������ļ�������","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return ;
            }

            //��ȡȫ����
            for (int i = 0; i < this.allColumns.Count; i++)
            {
                this.chklsSetColumn.Items.Add(this.allColumns[i]);
            }

            //��ȡ��ʾ��
            for(int i=0;i<this.showColumns.Count;i++)
            {
                int selIndex=(int)this.showColumns[i];
                this.chklsSetColumn.SetItemChecked(selIndex,true);
            }
        }
    }
}
