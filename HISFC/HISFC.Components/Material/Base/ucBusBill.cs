using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Integrate;

namespace Neusoft.UFC.Material.Base
{
    public partial class ucBusBill : UserControl
    {
        public ucBusBill()
        {
            InitializeComponent();
        }

        Neusoft.HISFC.Management.Material.ComCompany myCom = new Neusoft.HISFC.Management.Material.ComCompany();
        #region IConstManager ��Ա

        public ToolBarButton PreButton
        {
            get
            {
                // TODO:  ��� ucBusBill.PreButton getter ʵ��
                return null;
            }
        }

        public int Search()
        {
            // TODO:  ��� ucBusBill.Search ʵ��
            return 0;
        }

        public ToolBarButton SaveButton
        {
            get
            {
                // TODO:  ��� ucBusBill.SaveButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton SearchButton
        {
            get
            {
                // TODO:  ��� ucBusBill.SearchButton getter ʵ��
                return null;
            }
        }

        public int Del()
        {
            Neusoft.HISFC.Object.Material.MaterialCompany com = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as Neusoft.HISFC.Object.Material.MaterialCompany;
            if (com != null)
            {
                this.myCom.DeleteBusBill(com.ID);
            }
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
            // TODO:  ��� ucBusBill.Del ʵ��
            return 0;
        }

        public ToolBarButton AddButton
        {
            get
            {
                // TODO:  ��� ucBusBill.AddButton getter ʵ��
                return null;
            }
        }

        public int Print()
        {
            // TODO:  ��� ucBusBill.Print ʵ��
            return 0;
        }

        public int Pre()
        {
            // TODO:  ��� ucBusBill.Pre ʵ��
            return 0;
        }

        public ToolBarButton NextButton
        {
            get
            {
                // TODO:  ��� ucBusBill.NextButton getter ʵ��
                return null;
            }
        }

        public int Help()
        {
            // TODO:  ��� ucBusBill.Help ʵ��
            return 0;
        }

        public int Next()
        {
            // TODO:  ��� ucBusBill.Next ʵ��
            return 0;
        }

        public int Retrieve(string typeCode)
        {
            // TODO:  ��� ucBusBill.Retrieve ʵ��
            return 0;
        }

        //int Manager.IConstManager.Retrieve()
        //{
        //    // TODO:  ��� ucBusBill.Manager.IConstManager.Retrieve ʵ��
        //    return 0;
        //}

        public int Add()
        {
            this.neuSpread1_Sheet1.Rows.Add(0, 1);
            // TODO:  ��� ucBusBill.Add ʵ��
            return 0;
        }

        public ToolBarButton RetrieveButton
        {
            get
            {
                // TODO:  ��� ucBusBill.RetrieveButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton DelButton
        {
            get
            {
                // TODO:  ��� ucBusBill.DelButton getter ʵ��
                return null;
            }
        }

        public ToolBarButton PrintButton
        {
            get
            {
                // TODO:  ��� ucBusBill.PrintButton getter ʵ��
                return null;
            }
        }

        public int Exit()
        {
            // TODO:  ��� ucBusBill.Exit ʵ��
            return 0;
        }

        public int Save()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                Neusoft.HISFC.Object.Material.MaterialCompany com = new Neusoft.HISFC.Object.Material.MaterialCompany();

                com.ID = this.neuSpread1_Sheet1.Cells[i, 0].Tag.ToString();
                com.Name = this.neuSpread1_Sheet1.Cells[i, 0].Text;//����
                com.TelCode = this.neuSpread1_Sheet1.Cells[i, 1].Text;//����
                com.Coporation = this.neuSpread1_Sheet1.Cells[i, 2].Text;//����
                com.LinkMail = this.neuSpread1_Sheet1.Cells[i, 3].Text;//ע���ʱ�
                com.Address = this.neuSpread1_Sheet1.Cells[i, 4].Text;//��˾����
                com.FaxCode = this.neuSpread1_Sheet1.Cells[i, 5].Text;//��Ӫ��Χ
                com.NetAddress = this.neuSpread1_Sheet1.Cells[i, 6].Text;//��������
                com.EMail = this.neuSpread1_Sheet1.Cells[i, 7].Text;//Ӫҵ����
                com.LinkMan = this.neuSpread1_Sheet1.Cells[i, 8].Text;//�������
                com.LinkTel = this.neuSpread1_Sheet1.Cells[i, 9].Text;//��ע

                if (this.myCom.SetBusBill(com) < 0)
                {
                    MessageBox.Show("�������");
                    return -1;
                }
            }
            return 0;
        }

        ArrayList alCompany = new ArrayList();

        private void ucBusBill_Load(object sender, System.EventArgs e)
        {
            ArrayList al = this.myCom.QueryBusBill();

            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }

            foreach (Neusoft.HISFC.Object.Material.MaterialCompany com in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Tag = com.ID;
                this.neuSpread1_Sheet1.SetValue(0, 0, com.Name);//����
                this.neuSpread1_Sheet1.Cells[0, 1].Text = com.TelCode;//����
                this.neuSpread1_Sheet1.Cells[0, 2].Text = com.Coporation;//����
                this.neuSpread1_Sheet1.Cells[0, 3].Text = com.LinkMail;//ע���ʱ�
                this.neuSpread1_Sheet1.Cells[0, 4].Text = com.Address;//��˾����
                this.neuSpread1_Sheet1.Cells[0, 5].Text = com.FaxCode;//��Ӫ��Χ
                this.neuSpread1_Sheet1.Cells[0, 6].Text = com.NetAddress;//��������
                this.neuSpread1_Sheet1.Cells[0, 7].Text = com.EMail;//Ӫҵ����
                this.neuSpread1_Sheet1.Cells[0, 8].Text = com.LinkMan;//�������
                this.neuSpread1_Sheet1.Cells[0, 9].Text = com.LinkTel;//��ע

                this.neuSpread1_Sheet1.Rows[0].Tag = com;
            }

            this.alCompany = this.myCom.QueryCompany("1");
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Neusoft.NFC.Object.NeuObject obj = new Neusoft.NFC.Object.NeuObject();

            if (e.Column == 0)
            {
                Neusoft.NFC.Interface.Classes.Function.ChooseItem(this.alCompany, ref obj);

                if (obj != null && obj.ID.Length > 0)
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, 0].Tag = obj.ID;
                    this.neuSpread1_Sheet1.Cells[e.Row, 0].Text = obj.Name;
                }
            }
        }

        private void txtQueryCode_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string filter = this.txtQueryCode.Text.Trim();

                for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text.IndexOf(filter) >= 0)
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
            }
        }

        private void fpSpread1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Windows.Forms.ContextMenu menu = new ContextMenu();
                System.Windows.Forms.MenuItem item = new MenuItem();
                item.Text = "��������";
                item.Click += new EventHandler(item_Click);
                menu.MenuItems.Add(item);
                menu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }
        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    MessageBox.Show("�����ɹ�");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ToolBarButton AuditingButton
        {
            get
            {
                // TODO:  ��� ucBusBill.AuditingButton getter ʵ��
                return null;
            }
        }

        #endregion
    }
}
