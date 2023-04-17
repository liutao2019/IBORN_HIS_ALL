using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.Material.Check
{
    public partial class ucChooseList : UserControl
    {
        public ucChooseList()
        {
            InitializeComponent();
        }

        #region �����
        /// <summary>
        /// ��ѯ��ʽ
        /// </summary>
        private int intQueryType = 0;
        /// <summary>
        /// ��ѡ���б������DataSet
        /// </summary>
        protected DataTable myDataTable = new DataTable();
        protected DataView myDataView;
        /// <summary>
        /// �����ַ���
        /// </summary>
        private string[] filterField = { "" };
        /// <summary>
        /// �Ƿ�ʹ��Ĭ���ַ�������
        /// </summary>
        private bool isFilterDefault;
        /// <summary>
        /// ҩƷ������
        /// </summary>
        private Neusoft.HISFC.Management.Material.MetItem myItem = new Neusoft.HISFC.Management.Material.MetItem();
        /// <summary>
        /// ��ִ�е�SetFormat��������
        /// </summary>
        private static string formatFlag = "SetFormat";
        /// <summary>
        /// ���˺��Ƿ���ҪSetFormat
        /// </summary>
        private bool useFormat = true;
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�ʹ��Ĭ�Ϲ����ֶ�
        /// </summary>
        public bool IsFilterDefault
        {
            set { this.isFilterDefault = value; }
        }

        /// <summary>
        /// ���ݰ󶨶���
        /// </summary>
        public DataTable DataTable
        {
            get { return this.myDataTable; }
            set
            {
                this.myDataTable = value;
                this.myDataView = new DataView(this.myDataTable);
                this.neuSpread1.DataSource = this.myDataView;
            }
        }

        public string[] FilterField
        {
            set { this.filterField = value; }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        public ImageList TvImageList
        {
            get { return this.tvList.ImageList; }
            set { this.tvList.ImageList = value; }
        }


        /// <summary>
        /// �����б�
        /// </summary>
        public Neusoft.NFC.Interface.Controls.NeuTreeView TvList
        {
            get { return this.tvList; }
            set { this.tvList = value; }
        }


        /// <summary>
        /// �ؼ�����
        /// </summary>
        public string Caption
        {
            get { return this.lblCaption.Text; }
            set { this.lblCaption.Text = value; }
        }


        /// <summary>
        /// �Ƿ���ʾTreeView��true��ʾTreeView��false��ʾFarpoit
        /// </summary>
        public bool IsShowTreeView
        {
            get { return this.tvList.Visible; }
            set
            {
                if (!this.tvList.Visible.Equals(value))
                {
                    this.tvList.Visible = value;
                    this.neuPanel1.Visible = !value;
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�رհ�ť
        /// </summary>
        public bool IsShowCloseButton
        {
            get { return this.btnClose.Visible; }
            set { this.btnClose.Visible = value; }
        }

        /// <summary>
        /// ���˺��Ƿ���ҪSetFormat
        /// </summary>
        public bool UseFormatForFilter
        {
            set
            {
                this.useFormat = value;
            }
        }
        /// <summary>
        /// ��ѯ���� 0.ȫ��   1.��һ��   2.�ڶ���   3......
        /// </summary>
        //private int iFilter = -1;//liuxq ���ε��������ʱ���棩
        #endregion


        /// <summary>
        /// ͨ������Ĳ�ѯ�룬���������б�
        /// </summary>
        private void ChangeItem()
        {
            //TODO:�����б������뷨�й�
            try
            {
                //�жϵ�ǰ������DataSet
                //System.Data.DataView dv = new DataView(myDataSet.Tables[0]);

                string filter = " ";
                if (!this.isFilterDefault)
                {			//��ʹ��Ĭ�Ϲ����ֶ�
                    if (this.filterField.Length == 0)
                        return;

                    filter = "(" + this.filterField[0] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                    for (int i = 1; i < this.filterField.Length; i++)
                    {
                        filter += "OR (" + this.filterField[i] + " LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                    }
                }
                else
                {							   //ʹ��Ĭ�Ϲ����ֶ�					 �����ĳ���ģ��
                    switch (this.intQueryType)
                    {
                        case 1://���
                            filter = "(����� LIKE '" + this.txtQueryCode.Text.Trim() + "%' )";
                            break;
                        case 2://�Զ���
                            filter = "(�Զ����� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                            break;
                        default:
                            //ƴ��
                            filter = "(ƴ���� LIKE '" + this.txtQueryCode.Text.Trim() + "%' )";
                            break;
                    }
                    filter = filter + " OR (��Ʒ���� LIKE '%" + this.txtQueryCode.Text.Trim() + "%' )";
                }
                //���ù�������
                this.myDataView.RowFilter = filter;
                this.neuSpread1_Sheet1.ActiveRowIndex = 0;

                if (this.useFormat)
                {
                    switch (ucChooseList.formatFlag)
                    {
                        case "SetFormatForStorage":
                            this.SetFormatForStorage();
                            break;
                        default:
                            this.SetFormat();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ����ҩƷ������ʾ��ʽ
        /// </summary>
        public void SetFormat()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "���ʱ���";
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��������";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 120F;
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(2).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(3).Label = "���ۼ�";
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��װ��λ";
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "��װ����";
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "ͨ����";
            this.neuSpread1_Sheet1.Columns.Get(9).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "ͨ����ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(10).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(11).Label = "ͨ���������";
            this.neuSpread1_Sheet1.Columns.Get(11).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(12).Label = "ͨ�����Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(12).Visible = false;

            ucChooseList.formatFlag = "SetFormat";
        }

        /// <summary>
        /// ��ʾ���ҩƷʱ���и�ʽ��
        /// </summary>
        public void SetFormatForStorage()
        {
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "���ʱ���";
            this.neuSpread1_Sheet1.Columns.Get(0).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(1).Label = "��������";
            this.neuSpread1_Sheet1.Columns.Get(1).Visible = true;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 100F;

            this.neuSpread1_Sheet1.Columns.Get(2).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 76F;
            this.neuSpread1_Sheet1.Columns.Get(2).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(3).Label = "����";
            this.neuSpread1_Sheet1.Columns.Get(3).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 0F;

            this.neuSpread1_Sheet1.Columns.Get(4).Label = "��λ��";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 0F;
            this.neuSpread1_Sheet1.Columns.Get(4).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get(5).Label = "���";
            this.neuSpread1_Sheet1.Columns.Get(5).Width = 58F;
            this.neuSpread1_Sheet1.Columns.Get(5).Visible = true;

            this.neuSpread1_Sheet1.Columns.Get(6).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get(6).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(7).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get(7).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get(8).Label = "�Զ�����";
            this.neuSpread1_Sheet1.Columns.Get(8).Visible = false;

            ucChooseList.formatFlag = "SetFormatForStorage";
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        /// <param name="label">�б���</param>
        /// <param name="width">���</param>
        /// <param name="visible">�Ƿ���ʾ</param>
        public void SetFormat(string[] label, int[] width, bool[] visible)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (label != null && label.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Label = label[i];
                if (width != null && width.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Width = width[i];
                if (visible != null && visible.Length > i)
                    this.neuSpread1_Sheet1.Columns[i].Visible = visible[i];
            }
        }

        /// <summary>
        /// ���ù��˿�Ϊ����ȫѡ״̬
        /// </summary>
        public void SetFocusSelect()
        {
            this.txtQueryCode.SelectAll();
        }

        /// <summary>
        /// ��ȡ��ʾ���ݵĵ�һ�е�ָ���п��
        /// </summary>
        /// <param name="columnNum">������������</param>
        /// <param name="width">���صĿ��</param>
        public void GetColumnWidth(int columnNum, ref int width)
        {
            int iNum = 0;
            width = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Visible)
                {
                    width = width + (int)this.neuSpread1_Sheet1.Columns[i].Width;
                    iNum = iNum + 1;
                    if (iNum > columnNum - 1)
                        break;
                }
            }
        }

        /// <summary>
        /// ��ʾ�б�
        /// </summary>
        public void ShowChooseList(System.Data.DataSet dataSet)
        {
            try
            {
                this.neuSpread1.DataSource = this.myDataView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
        
        /// <summary>
        ///  ���ò�ѯ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, System.EventArgs e)
        {
        }

        private void txtQueryCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
                return;
            }
        }

        private void txtQueryCode_TextChanged(object sender, EventArgs e)
        {
            this.ChangeItem();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Neusoft.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, "d:\\wolf.xml");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Parent.Visible = false;
        }

        private void ucChooseList_Load(object sender, System.EventArgs e)
        {
            this.btnClose.Click += new EventHandler(btnClose_Click);
            //��myDataView��neuSpread1��
            this.myDataView = new DataView(this.myDataTable);
            this.neuSpread1.DataSource = this.myDataView;

            //��ʾneuSpread1
            this.IsShowTreeView = false;
        }

     }
}
