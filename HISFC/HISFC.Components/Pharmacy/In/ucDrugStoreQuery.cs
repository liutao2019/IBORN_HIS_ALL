using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: ���ڵ����ҵ���ҩƷ�������ʱ�Ŀ���ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// </summary>
    public partial class ucDrugStoreQuery : UserControl
    {
        public ucDrugStoreQuery()
        {
            InitializeComponent();
        }

        public ucDrugStoreQuery(string drugCode) : this()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alStorage = itemManager.QueryStoreDeptList(drugCode);
            if (alStorage == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҩƷ������������Ϣʧ��"));
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.Storage storage in alStorage)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = storage.StockDept.Name;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = storage.Item.Name;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = storage.Item.Specs;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = storage.BatchNO;
                this.neuSpread1_Sheet1.Cells[0, 4].Text = storage.Item.Product.Producer.ID;
                this.neuSpread1_Sheet1.Cells[0, 5].Text = storage.StoreQty.ToString("N");
                this.neuSpread1_Sheet1.Cells[0, 6].Text = storage.Item.MinUnit;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }
    }
}
