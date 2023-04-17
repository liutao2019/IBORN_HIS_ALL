using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucPatInfoQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //数据库管理类
        Database db;// = new DataBaseManger();

        //sql 语句
        string sql;

        public ucPatInfoQuery()
        {
            InitializeComponent();
            db = new DataBaseManger();
            sql = "";
        }

        private void GenerateSql()
        {
            if (chkInhos.Checked)
            {
                sql = "select distinct inPatient_no 住院流水号,  patient_no 病历号, name 姓名, (case sex_code when 'F' then '女' when 'M' then '男' end) 性别,\n" +
                     " (case in_state when 'R' then '住院登记' when 'I' then '病房接诊' when 'B' then '出院登记' when 'O' then '出院结算' when 'P' then '预约出院' when 'N' then '无费退院' end) 在院状态,\n" +
                     " DIAG_NAME 入院诊断, birthday 出生日期, IN_DATE 入院日期, OUT_DATE 出院日期 \n" +
                     " from FIN_IPR_INMAININFO where IN_DATE between '{0}' and '{1}'";
                if (chkIndate.Checked)
                {
                    sql = string.Format(sql, DateTime.Now.Date.AddDays(-30).ToString(), DateTime.Now.Date.AddDays(1).ToString());
                }
                else
                {
                    sql = string.Format(sql, DateTime.MinValue.Date.ToString(), DateTime.Now.Date.AddDays(1).ToString());
                }

                if (rbtAnd.Checked)
                {
                    if (chk.Checked)
                    {
                        if (txtName.Text.Trim() != "")
                        {
                            sql += " and name = '" + txtName.Text.Trim() + "'";
                        }
                        if (txtPatNo.Text.Trim() != "")
                        {
                            sql += " and patient_NO = '" + txtPatNo.Text.Trim().PadLeft(10, '0') + "'";
                        }
                        if (cmbDept.Text.Trim() != "")
                        {
                            sql += " and DEPT_CODE = '" + cmbDept.Tag.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (txtName.Text.Trim() != "")
                        {
                            sql += " and name like '%" + txtName.Text.Trim() + "%'";
                        }
                        if (txtPatNo.Text.Trim() != "")
                        {
                            sql += " and patient_NO like '%" + txtPatNo.Text.Trim() + "%'";
                        }
                        if (cmbDept.Text.Trim() != "")
                        {
                            sql += " and DEPT_CODE = '" + cmbDept.Tag.ToString() + "'";
                        }
                    }
                }
                if (rbtOr.Checked)
                {
                    if (chk.Checked)
                    {
                        if (txtName.Text.Trim() != "")
                        {
                            sql += " Or name = '" + txtName.Text.Trim() + "'";
                        }
                        if (txtPatNo.Text.Trim() != "")
                        {
                            sql += " Or patient_NO = '" + txtPatNo.Text.Trim().PadLeft(10, '0') + "'";
                        }
                        if (cmbDept.Text.Trim() != "")
                        {
                            sql += " Or DEPT_CODE = '" + cmbDept.Tag.ToString() + "'";
                        }
                    }
                    else
                    {
                        if (txtName.Text.Trim() != "")
                        {
                            sql += " Or name like '%" + txtName.Text.Trim() + "%'";
                        }
                        if (txtPatNo.Text.Trim() != "")
                        {
                            sql += " Or patient_NO like '%" + txtPatNo.Text.Trim() + "%'";
                        }
                        if (cmbDept.Text.Trim() != "")
                        {
                            sql += " Or DEPT_CODE = '" + cmbDept.Tag.ToString() + "'";
                        }
                    }
                }
            }
                //查询门诊病人
            else
            {
                sql = @"
                select p.CARD_NO 病历号 , p.NAME 姓名,p.BIRTHDAY 生日,
                case p.SEX_CODE when 'M' then '男' else '女' end 性别, p.LREG_DATE 最近挂号时间
                from COM_PATIENTINFO p
                where 0=0 ";
                if (chk.Checked)
                {
                    if (txtName.Text.Trim() != "")
                    {
                        sql += " Operator  p.NAME = '" + txtName.Text.Trim() + "'";
                    }
                    if (txtPatNo.Text.Trim() != "")
                    {
                        sql += " Operator  p.CARD_NO = '" + txtPatNo.Text.Trim() + "'";

                    }
                }
                else
                {
                    if (txtName.Text.Trim() != "")
                    {
                        sql += " Operator  p.NAME like '%" + txtName.Text.Trim() + "%'";
                    }
                    if (txtPatNo.Text.Trim() != "")
                    {
                        sql += " Operator  p.CARD_NO like '%" + txtPatNo.Text.Trim() + "%'";

                    }
                }
                if (rbtAnd.Checked)
                {
                    sql = sql.Replace("Operator", "And");
                }
                else
                {
                    sql = sql.Replace("Operator", "Or");
                }

            }
//where patient_NO='' and name = ''"
        }

        private void Query()
        {
            GenerateSql();
            DataSet ds = new DataSet();
            db.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            neuSpread1_Sheet1.Rows.Count = 0;
            neuSpread1_Sheet1.DataSource = ds.Tables[0];
             neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 35;
            

            //for(int k=0;k<neuSpread1_Sheet1.ColumnCount;k++)
            //{
            //                neuSpread1_Sheet1.ColumnHeader.Cells[0,1]

            //}
             
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                neuSpread1_Sheet1.Rows[i].Height = 35;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query();
            }
        }

        public override int Query(object sender, object neuObject)
        {
            Query();
            return base.Query(sender, neuObject);
        }

        private void ucPatInfoQuery_Load(object sender, EventArgs e)
        {
            System.Collections.ArrayList alDepts = new System.Collections.ArrayList();
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            alDepts = manager.LoadAll();//.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
        }
    }
}
