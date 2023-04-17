using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using GZSI.ApiModel;

namespace GZSI.ApiControls
{
    /// <summary>
    /// ��Ա��Ϣѡ��
    /// </summary>
    public partial class ucPersonInfo : UserControl
    {
        public ucPersonInfo()
        {
            InitializeComponent();
        }

        PersonInfo person = null;
        public PersonInfo Person
        {
            get { return person; }
        }

        DataTable dtPersonInfo = null;

        ArrayList alPersonInfo = new ArrayList();
        public ArrayList AlPersonInfo
        {
            set { alPersonInfo = value; }
        }

        private void ucGetTreamType_Load(object sender, EventArgs e)
        {
            if (alPersonInfo != null && alPersonInfo.Count > 0)
            {
                DisPersonInfo(alPersonInfo);
            }

        }

        private void DisPersonInfo(ArrayList al)
        {
            if (al == null && al.Count == 0)
            {
                return;
            }
            Type str = typeof(System.String);
            dtPersonInfo = new DataTable();


            dtPersonInfo.Columns.AddRange(new DataColumn[]{new DataColumn("���˵��Ժ�", str),
                                                          new DataColumn("���֤", str), 
                                                           new DataColumn("����", str),
                                                          new DataColumn("�Ա�", str),
                                                          new DataColumn("��������", str),
                                                          new DataColumn("��λ����", str),
                                                          new DataColumn("��λ����", str), 
                                                          new DataColumn("��Ա���", str),
                                                          new DataColumn("��Ա״̬", str),
                                                          new DataColumn("��ϵ�绰", str)
                                                       });

            foreach (PersonInfo obj in al)
            {
                DataRow rowDisplay = dtPersonInfo.NewRow();

                rowDisplay["���˵��Ժ�"] = obj.Api_indi_id;
                rowDisplay["���֤"] = obj.Api_idcard;
                rowDisplay["����"] = obj.Api_name;
                rowDisplay["�Ա�"] = obj.Api_sex;
                rowDisplay["��������"] = obj.Api_birthday;
                rowDisplay["��λ����"] = obj.Api_corp_id;
                rowDisplay["��λ����"] = obj.Api_corp_name;
                rowDisplay["��Ա���"] = obj.Api_pers_identity;
                rowDisplay["��Ա״̬"] = obj.Api_pers_status;
                rowDisplay["��ϵ�绰"] = obj.Api_telephone;

                dtPersonInfo.Rows.Add(rowDisplay);
            }
            this.fpSpread1_Sheet1.DataSource = dtPersonInfo;
        }

        private void GetTreamType()
        {
            person = new  PersonInfo();
            int i = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount <= 0)
                return;
            person.Api_indi_id = this.fpSpread1_Sheet1.Cells[i, 0].Text;
            person.Api_idcard = this.fpSpread1_Sheet1.Cells[i, 1].Text;
            person.Api_name = this.fpSpread1_Sheet1.Cells[i, 2].Text;

            this.FindForm().Close();

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            GetTreamType();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            GetTreamType();
        }

    }
}
