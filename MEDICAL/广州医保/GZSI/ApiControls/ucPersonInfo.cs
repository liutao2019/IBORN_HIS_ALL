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
    /// 人员信息选择
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


            dtPersonInfo.Columns.AddRange(new DataColumn[]{new DataColumn("个人电脑号", str),
                                                          new DataColumn("身份证", str), 
                                                           new DataColumn("姓名", str),
                                                          new DataColumn("性别", str),
                                                          new DataColumn("出生日期", str),
                                                          new DataColumn("单位编码", str),
                                                          new DataColumn("单位名称", str), 
                                                          new DataColumn("人员类别", str),
                                                          new DataColumn("人员状态", str),
                                                          new DataColumn("联系电话", str)
                                                       });

            foreach (PersonInfo obj in al)
            {
                DataRow rowDisplay = dtPersonInfo.NewRow();

                rowDisplay["个人电脑号"] = obj.Api_indi_id;
                rowDisplay["身份证"] = obj.Api_idcard;
                rowDisplay["姓名"] = obj.Api_name;
                rowDisplay["性别"] = obj.Api_sex;
                rowDisplay["出生日期"] = obj.Api_birthday;
                rowDisplay["单位编码"] = obj.Api_corp_id;
                rowDisplay["单位名称"] = obj.Api_corp_name;
                rowDisplay["人员类别"] = obj.Api_pers_identity;
                rowDisplay["人员状态"] = obj.Api_pers_status;
                rowDisplay["联系电话"] = obj.Api_telephone;

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
