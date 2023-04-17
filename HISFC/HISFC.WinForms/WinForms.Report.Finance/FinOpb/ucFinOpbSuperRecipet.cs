using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinOpb
{
    public partial class ucFinOpbSuperRecipet : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbSuperRecipet()
        {
            InitializeComponent();
        }
        private decimal drugCost = 0;

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            try
            {
                this.drugCost = Decimal.Parse(ntbDrugCost.Text);
            }
            catch (Exception e) { 
            MessageBox.Show("����������");
            return 0;
        }
            if(drugCost<500){
                MessageBox.Show("��������ڻ����500�Ľ��");
                return 0;
            }
            return base.OnRetrieve(base.beginTime, base.endTime,this.drugCost);
        }

        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            int currRow = e.RowNumber;
            if (currRow == 0)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����ϸ�����Ժ�...");
            string recipeNo;
            recipeNo = dwMain.GetItemString(currRow, "RNO");
            dwDetail.Retrieve(recipeNo);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return;
        }
    }
}
