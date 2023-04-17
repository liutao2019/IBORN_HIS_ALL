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
        //���ݿ������
        Database db;// = new DataBaseManger();

        //sql ���
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
                sql = "select distinct inPatient_no סԺ��ˮ��,  patient_no ������, name ����, (case sex_code when 'F' then 'Ů' when 'M' then '��' end) �Ա�,\n" +
                     " (case in_state when 'R' then 'סԺ�Ǽ�' when 'I' then '��������' when 'B' then '��Ժ�Ǽ�' when 'O' then '��Ժ����' when 'P' then 'ԤԼ��Ժ' when 'N' then '�޷���Ժ' end) ��Ժ״̬,\n" +
                     " DIAG_NAME ��Ժ���, birthday ��������, IN_DATE ��Ժ����, OUT_DATE ��Ժ���� \n" +
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
                //��ѯ���ﲡ��
            else
            {
                sql = @"
                select p.CARD_NO ������ , p.NAME ����,p.BIRTHDAY ����,
                case p.SEX_CODE when 'M' then '��' else 'Ů' end �Ա�, p.LREG_DATE ����Һ�ʱ��
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
