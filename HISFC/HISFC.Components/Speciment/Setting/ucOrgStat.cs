using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.Setting
{
    /// <summary>
    /// 组织的送存率,维护哪些手术是不要取数据,哪些科室不属于考核范围的
    /// </summary>
    public partial class ucOrgStat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        //考核
        DataView dv0 = new DataView();
        //非考核
        DataView dv1 = new DataView();

        string insertSql = @"insert into SPEC_ORGSTATIC (
                       DEPTID
                      ,OPERNAME
                      ,ISCOL
                      ,TYPE
                        )
                      VALUES (
                       '{0}' -- DEPTID
                      ,'{1}' -- OPERNAME
                      ,'{2}' -- ISCOL
                      ,'{3}' -- TYPE
                    )";
        string updateSql = @"
                               update SPEC_ORGSTATIC
                               set ISCOL = '{2}'
                               where OPERNAME = '{0}' and TYPE='{1}'";

        private FS.HISFC.BizLogic.Speciment.SpecSourceManage mgr = new FS.HISFC.BizLogic.Speciment.SpecSourceManage();

        public ucOrgStat()
        {
            InitializeComponent();
        }

        private void Add()
        {
            //for(inti  = 0;int<


            //neuSpread1_Sheet1 表示不需要考核列表
            //SheetView 表示需要考核
            // 从非考核加到考核

            string type = rbtDept.Checked ? "D" : "O";
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                if (neuSpread1_Sheet1.IsSelected(i, 0))
                {
                    string text0 = neuSpread1_Sheet1.Cells[i, 0].Text;
                    string text1 = neuSpread1_Sheet1.Cells[i, 1].Text;
                    string text2 = neuSpread1_Sheet1.Cells[i, 2].Text.Trim();

                    sheetView1.Rows.Add(sheetView1.RowCount, 1);
                    //sheetView1.Rows[sheetView1.RowCount - 1].Tag = "1";

                    sheetView1.Cells[sheetView1.RowCount - 1, 0].Text = text0;
                    sheetView1.Cells[sheetView1.RowCount - 1, 1].Text = text1;

                    string tmpUpdate = string.Format(updateSql, text1, type,"1");
                    string tmpInsert = string.Format(insertSql, text0, text1, "1", type);

                    DialogResult drResult = MessageBox.Show("是否将 " + text1 + " 加到考核队列？", "添加", MessageBoxButtons.YesNo);
                    if (drResult == DialogResult.No)
                    {
                        return;
                    }

                    //如果在表中存在记录， 则更新数据库
                    if (text2 == "1")
                    {
                       int res =  mgr.ExecNoQuery(tmpUpdate);                        
                    }

                    if (text2 == "0")
                    {
                      int res =  mgr.ExecNoQuery(tmpInsert);
                    }
                    
                    sheetView1.Rows[sheetView1.RowCount - 1].BackColor = Color.SkyBlue;
                }
            }
            //this.BindingData();
        }

        private void Remove()
        {
            string type = rbtDept.Checked ? "D" : "O";
            //从考核加到非考核
            for (int i = 0; i < sheetView1.RowCount; i++)
            {
                //int activeRowIndex = sheetView1.ActiveRowIndex;
                //string tex = neuSpread1_Sheet1.Cells[activeRowIndex, 0].Text;
                if (sheetView1.IsSelected(i, 0))
                {
                    string text0 = sheetView1.Cells[i, 0].Text;
                    string text1 = sheetView1.Cells[i, 1].Text;
                    string text2 = neuSpread1_Sheet1.Cells[i, 2].Text.Trim();

                    //neuSpread1_Sheet1.Rows.Add(sheetView1.RowCount, 1);
                    //neuSpread1_Sheet1.Rows[neuSpread1_Sheet1.RowCount - 1].Tag = "0";

                    //neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 0].Text = text0;
                    //neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.RowCount - 1, 1].Text = text1;

                    string tmpUpdate = string.Format(updateSql, text1, type, "0");

                    string tmpInsert = string.Format(insertSql, text0, text1, "0", type);

                    DialogResult drResult = MessageBox.Show("是否排除 " + text1 + " 考核？", "排除", MessageBoxButtons.YesNo);
                    if (drResult == DialogResult.No)
                    {
                        return;
                    }


                    //如果在表中存在记录， 则更新数据库
                    //if (text2 == "1")
                    //{
                    //    int res = mgr.ExecNoQuery(tmpUpdate);
                    //}

                    //if (text2 == "0")
                    //{
                        int res = mgr.ExecNoQuery(tmpInsert);
                    //}

                    //int res = mgr.ExecNoQuery(tmpUpdate);
                    //{
                        //mgr.ExecNoQuery(tmpInsert);
                    //}
                    //sheetView1.Rows[i].Tag = "0";
                    //sheetView1.Rows[i].Visible = false;
                }
            }
            this.BindingData();
        } 

        private void BindingData()
        {
            //1 表示在表中存在记录，0 表示不存在
            string noCheckOpersql = @"
                             select min(so.OPERDEPTName) 科室, so.OPERNAME 名称, '0'
                             from SPEC_OPERAPPLY so 
                             where so.OPERNAME not in
                             (
                               select distinct  o.OPERNAME
                             from SPEC_ORGSTATIC o 
                             )
                             and so.ORGORBLOOD = 'O'
                             group by so.OPERNAME
                              union 
                             select distinct o.DEPTID 科室, o.OPERNAME 名称,'1'
                             from SPEC_ORGSTATIC o
                             where TYPE = 'O' and ISCOL = '0'
                             ";
            string checkOper = @"select distinct o.DEPTID 科室, o.OPERNAME 名称,'1'
                                   from SPEC_ORGSTATIC o
                                   where TYPE = 'O' and ISCOL = '1'";

            string chkDept = @"select distinct cd.DEPT_NAME 名称, cd.DEPT_CODE 编码,'0'
                                  from COM_DEPARTMENT cd
                                  where cd.DEPT_CODE not in
                                  (
                                    select distinct  o.OPERNAME
                                    from SPEC_ORGSTATIC o 
                                  )
                                  and cd.DEPT_TYPE in('I','C')
                                  union 
                             select distinct o.DEPTID 名称 ,o.OPERNAME 编码,'1'
                             from SPEC_ORGSTATIC o
                             where TYPE = 'D' and ISCOL = '1'                                  
                                  ";

            string noCheckDept = @"select distinct  o.OPERNAME 编码,cd.DEPT_NAME 名称,'1'
                                  from SPEC_ORGSTATIC o join COM_DEPARTMENT cd on o.OPERNAME=cd.DEPT_CODE
                                  where TYPE = 'D' and ISCOL = '0'
                                  ";
            neuSpread1_Sheet1.RowCount = 0;
            sheetView1.RowCount = 0;

            //考核
            DataSet ds0 = new DataSet();
            //非考核
            DataSet ds1 = new DataSet();
            dv0 = new DataView();
            dv1 = new DataView();

            try
            {
                sheetView1.AutoGenerateColumns = true;
                neuSpread1_Sheet1.AutoGenerateColumns = true;

                if (rbtDept.Checked)
                {
                    mgr.ExecQuery(chkDept, ref ds0);
                    mgr.ExecQuery(noCheckDept, ref ds1);

                }
                else
                {
                    mgr.ExecQuery(checkOper, ref ds0);
                    mgr.ExecQuery(noCheckOpersql, ref ds1);
                }

                dv1 = ds1.Tables[0].DefaultView;
                dv0 = ds0.Tables[0].DefaultView;

                sheetView1.DataSource = dv0;
                neuSpread1_Sheet1.DataSource = dv1;

                sheetView1.Columns[2].Visible = false;
                neuSpread1_Sheet1.Columns[2].Visible = false;


                for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                {
                    neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
                    sheetView1.Columns[i].AllowAutoFilter = true;
                    sheetView1.Columns[i].AllowAutoSort = true;
                }


                neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet1.Columns.Count);
                sheetView1.Models.ColumnHeaderSpan.Add(0, 0, 1, sheetView1.Columns.Count);

                neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "非考核";
                sheetView1.ColumnHeader.Cells[0, 0].Text = "考核";

                sheetView1.ColumnHeader.Rows[0].Height = 35;
                sheetView1.ColumnHeader.Rows[0].Font = new Font("新宋体", 14);
                sheetView1.ColumnHeader.Rows[1].Height = 25;
                sheetView1.ColumnHeader.Rows[0].Font = new Font("新宋体", 12);

                neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 35;
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font("新宋体", 14);
                neuSpread1_Sheet1.ColumnHeader.Rows[1].Height = 25;
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font("新宋体", 12);
            }
            catch
            { }

        }

        private void ucOrgStat_Load(object sender, EventArgs e)
        {
        }

        public override int Query(object sender, object neuObject)
        {
            this.BindingData();
            return base.Query(sender, neuObject);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Remove();
        }

        private void txtChk_TextChanged(object sender, EventArgs e)
        {
            string filter = txtChk.Text;
            if (rbtDept.Checked)
            {
                dv0.RowFilter = " 名称 like '%" + filter + "%' Or 编码 like '%" + filter + "%'";
            }
            else
            {
                dv0.RowFilter = " 名称 like '%" + filter + "%' Or 科室 like '%" + filter + "%'";

            }

        }

        private void txtNoCheck_TextChanged(object sender, EventArgs e)
        {
            string filter = txtNoCheck.Text;
            if (rbtDept.Checked)
            {
                dv1.RowFilter = " 名称 like '%" + filter + "%' Or 编码 like '%" + filter + "%'";
            }
            else
            {
                dv1.RowFilter = " 名称 like '%" + filter + "%' Or 科室 like '%" + filter + "%'";

            }
        }
    }
}
