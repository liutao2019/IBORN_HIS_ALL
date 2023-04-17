using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinIpr
{
    public partial class ucFinIprPrepayStatic : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIprPrepayStatic()
        {
            InitializeComponent();
        }

        string strOper = "ALL";
        string strPID = string.Empty;
        ArrayList alOper = new ArrayList();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        #region ��ʼ��
        protected override void OnLoad(EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            alOper.Add(obj);

            ArrayList list = new ArrayList();
            list = manager.QueryEmployeeAll();
            alOper.AddRange(list);

            cmbOper.AddItems(alOper);
            cmbOper.SelectedIndex = 0;
            //foreach (FS.HISFC.Models.Base.Employee emp in list)
            //{
            //    cmbOper.Items.Add(emp);
           
            //}
            cmbOper.SelectedIndex = 0;
            base.OnLoad(e);
        }
        #endregion 

        #region ��ѯ
        protected override int OnRetrieve(params object[] objects)
        {
           // ArrayList allll = this.cmbOper.alItems;
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }
            string strOperName = string.Empty;
            string strInvo = string.Empty;
            string strCAMoney = string.Empty;
            decimal decCAMoney = 0;
            int rowcount = 0;
        
            strOper = cmbOper.SelectedItem.ID;
            strOperName = cmbOper.SelectedItem.Name;

            int intReturnNum = base.OnRetrieve(this.beginTime, this.endTime, strOper);
            rowcount = this.dwMain.RowCount;
            //����Ա��ֵ
            dwMain.Modify("oper_text.text = '" + strOperName + "'");

            //�ֽ�
            decCAMoney = FS.FrameWork.Function.NConvert.ToDecimal(this.dwMain.GetItemString(1, "ca_text"));
            strCAMoney = FS.FrameWork.Public.String.LowerMoneyToUpper(decCAMoney);
      

            this.dwMain.Modify("money_text.text = '" + strCAMoney + "'");
           
            ////���뷶Χ
            //if (rowcount > 0)
            //{   
            //    strInvo = this.dwMain.GetItemString(1, "��Ʊ��") + "~" + this.dwMain.GetItemString(rowcount, "��Ʊ��");

            //    this.dwMain.Modify("invoive_text.text = '" + strInvo + "'");
            //}
            return intReturnNum;
        }

        private void ntbPID_SelectTextChanged(object sender,EventArgs e)
        {
            strPID = this.ntbPID.Text.Trim();
            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }

            //this.dwMain.SetFilter("");
            //this.dwMain.Filter();
            dv.RowFilter = "";

            if (string.IsNullOrEmpty(strPID))
            {
                return;
            }
            //this.dwMain.SetFilter("סԺ�� like '%"+strPID+"'");
            //this.dwMain.Filter();
            try
            {
                dv.RowFilter = "סԺ�� like '%" + strPID + "'";
            }
            catch
            {
                MessageBox.Show("�������������ַ�����������ȷ�Ĳ�ѯ��Ϣ��");
                return;
            }
        }
        #endregion 
    }
}