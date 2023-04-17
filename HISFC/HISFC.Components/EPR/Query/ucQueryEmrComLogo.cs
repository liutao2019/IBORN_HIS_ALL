using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.Query
{
    public partial class ucQueryEmrComLogo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryEmrComLogo()
        {
            InitializeComponent();
        }
        #region  ����
        public string strSql = "";
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        //������
      
        #endregion

        #region ����
        public void Query()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            int index = 0;
            string temp = "";
            temp = getSqlStr();
            DataSet ds = new DataSet();
            dbMgr.ExecQuery(temp, ref ds);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.fpSpread1_Sheet1.Rows.Add(i, 1);
                    this.fpSpread1_Sheet1.Cells[i, 0].Text = ds.Tables[0].Rows[i][0].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 1].Text = ds.Tables[0].Rows[i][1].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 2].Text = ds.Tables[0].Rows[i][2].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 3].Text = ds.Tables[0].Rows[i][3].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 4].Text = ds.Tables[0].Rows[i][4].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 5].Text = ds.Tables[0].Rows[i][5].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 6].Text = ds.Tables[0].Rows[i][6].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 7].Text = ds.Tables[0].Rows[i][7].ToString();
                    this.fpSpread1_Sheet1.Cells[i, 8].Text = ds.Tables[0].Rows[i][8].ToString();


                }
            }
            else
            {
                this.fpSpread1_Sheet1.RowCount = 0;
            }

        }
        /// <summary>
        /// ���SQL���
        /// </summary>
        public string getSqlStr()
        {
            strSql = "select e.empl_code as �޸Ĺ���,e.empl_name as �޸������� ,o.emrname as ��������," +
"o.nodename as �������,o.oldvalue as ������,o.newvalue as ������," +
"to_char(o.oper_date,'yyyy-mm-dd hh24:mi:ss') as ��������, o.index1 as סԺ��,o.index2 as �������� " +
 "from emr_com_logo o ,com_employee e " +
 "where o.type='2' and o.memo='����޸�' and o.oper_code=e.empl_code and o.oldvalue is not null and o.oldvalue <>'-'";

            if (this.chkperid.Checked)
            {
                strSql = strSql + " and o.oper_code like '%" + this.txtId.Text.Trim() + "'";
            }
            if (this.chkinpano.Checked)
            {
                strSql = strSql + " and o.index1 like '%" + this.txtInpatientno.Text.Trim() + "'";
            }
            if (this.chknodevalue.Checked)
            {
                strSql = strSql + " and o.nodename='" + this.txtnodevalue.Text.Trim() + "'";
            }
            if (this.chkoperdate.Checked)
            {
                strSql = strSql + " and to_char(o.oper_date,'yyyy-mm-dd')=to_char(to_date('" + this.dateTimePicker1.Text.Trim() + "','yyyy-mm-dd'),'yyyy-mm-dd')";
            }
            strSql = strSql + " order by o.oper_date desc";

            return strSql;


        }
        /// <summary>
        /// ����EXCEL
        /// </summary>
        public void ExportFarPoint()
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "xls|*.xls";
            if (sa.ShowDialog() == DialogResult.OK)
            {
                this.fpSpread1.SaveExcel(sa.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            }
        }
        #endregion


        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            return base.Init(sender, neuObject, param);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();
            toolBar.AddToolButton("��ѯ��־", "��ѯ��־", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBar.AddToolButton("����EXCEL", "������EXCEL", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            return toolBar;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "��ѯ��־")
            {
                this.Query();
            }
            else if (e.ClickedItem.Text == "����EXCEL")
            {
                this.ExportFarPoint();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        //protected override int OnQuery(object sender, object neuObject)
        //{
        //    Query();
        //}
   
    }
}
