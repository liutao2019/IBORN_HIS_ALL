using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����̨ά�����]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucTableSpread : UserControl
    {
        public ucTableSpread()
        {
            InitializeComponent();
        }

        #region �ֶ�
        public event EventHandler ItemModified;
        private OpsRoom room;

        private bool inited = false;    //��ʼ��
        #endregion

        #region ����
        /// <summary>
        /// ������
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OpsRoom OperationRoom
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
            }
        }
        #endregion
        #region ����
        public int ValidState()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (fpSpread1_Sheet1.Cells[i, 0].Text.Trim() == "")
                {
                    MessageBox.Show("����̨���Ʋ���Ϊ��");
                    return -1;
                }
                if (fpSpread1_Sheet1.Cells[i, 1].Text.Trim() == "")
                {
                    MessageBox.Show("�����벻��Ϊ��");
                    return -1;
                }
                for (int j = 0; j < this.fpSpread1_Sheet1.RowCount; j++)
                {
                    if (i == j)
                    {
                        continue;
                    } 
                    if (fpSpread1_Sheet1.Cells[i, 0].Text == fpSpread1_Sheet1.Cells[j, 0].Text)
                    {
                        MessageBox.Show("����̨���Ʋ�����ͬ");
                        return -1;
                    }
                    if (fpSpread1_Sheet1.Cells[i, 1].Text == fpSpread1_Sheet1.Cells[j, 1].Text)
                    {
                        MessageBox.Show("�����벻����ͬ");
                        return -1;
                    }
                }
            }
            return 1;
        }
        /// <summary>
        /// ���������һ��
        /// </summary>
        /// <param name="table"></param>
        private void AddItem(FS.HISFC.Models.Operation.OpsTable table)
        {
            this.fpSpread1_Sheet1.RowCount++;
            int index = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.Cells[index, 0].Text = table.Name;
            this.fpSpread1_Sheet1.Cells[index, 1].Text = table.InputCode;
            if (table.IsValid)
                this.fpSpread1_Sheet1.Cells[index, 2].Text = "����";
            else
                this.fpSpread1_Sheet1.Cells[index, 2].Value = "ͣ��";

            this.fpSpread1_Sheet1.Cells[index, 3].Text = table.Memo;


            this.fpSpread1_Sheet1.Rows[index].Tag = table;
        }

        /// <summary>
        /// �������һ����
        /// </summary>
        public void AddItem()
        {
            if (this.room == null)
                return;

            this.fpSpread1_Sheet1.RowCount++;
            OpsTable table = new OpsTable();
            table.ID = Environment.TableManager.GetNewTableNo();
            table.Room = room;
            table.Dept.ID = Environment.OperatorDeptID;
            table.User.ID = Environment.OperatorID;
            room.Tables.Add(table);
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].Tag = table;

        }

        /// <summary>
        /// ��������Ӷ���
        /// </summary>
        /// <param name="tables"></param>
        public void AddItem(List<OpsTable> tables)
        {
            if (this.room == null)
                return;

            this.inited = false;
            foreach (OpsTable table in tables)
            {
                this.AddItem(table);
            }
            this.inited = true;
        }

        /// <summary>
        /// ɾ��һ��
        /// </summary>
        public void DeleteItem()
        {
            if (this.room == null)
            {
                MessageBox.Show( "��ѡ����Ҫɾ��������̨", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }
            DialogResult rs = MessageBox.Show( "ȷ��ɾ����ǰѡ�������̨��\n ɾ������ע�������水ť��Ч", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information );
            if (rs == DialogResult.No)
            {
                return;
            }
            if (this.fpSpread1_Sheet1.RowCount > 0 && this.fpSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                this.room.Tables.Remove(this.fpSpread1_Sheet1.ActiveRow.Tag as OpsTable);
                this.fpSpread1_Sheet1.Rows.Remove(this.fpSpread1_Sheet1.ActiveRowIndex, 1);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.room = null;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public new void Update()
        {
            if (this.room == null)
                return;

            this.room.Tables.Clear();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                OpsTable table = this.fpSpread1_Sheet1.Rows[i].Tag as OpsTable;
                table.Name = this.fpSpread1_Sheet1.Cells[i, 0].Text;
                table.InputCode = this.fpSpread1_Sheet1.Cells[i, 1].Text;
                if (this.fpSpread1_Sheet1.Cells[i, 2].Text == "����")
                    table.IsValid = true;
                else
                    table.IsValid = false;
                table.Memo = this.fpSpread1_Sheet1.Cells[i, 3].Text;

                this.room.AddTable(table);
            }

        }
        #endregion

        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (!this.inited)
                return;
            if (this.ItemModified != null)
                this.ItemModified(this, null);
        }
    }
}