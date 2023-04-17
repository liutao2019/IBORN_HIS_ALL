using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucOutStore : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ucQueryBySpecSource ucQuerySpecSource;
        private string strSql;
        private ucOutSpecList ucOutList;
        private QueryEngineManage queryManage;
        private ucQueryByPatient ucQueryPatient;
        private ucQueryDiagnose ucQueryByDiagnose;
        private ucSpecIdImport ucSpecId;
        private FS.HISFC.Models.Base.Employee loginPerson;

        public ucOutStore()
        {
            InitializeComponent();           
            strSql = "";
            queryManage = new QueryEngineManage();
        }

        /// <summary>
        /// ���ز�ѯ�û��ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOutStore_Load(object sender, EventArgs e)
        {
            //ÿ����һ�� �����Ƿ�����Եı걾
            DateTime dt = DateTime.Now;
            if (dt.Day == 1)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڸ��±걾��ԣ����Ժ�...");
                SpecSourceManage specManage = new SpecSourceManage();
                ArrayList arr = specManage.GetMatch();
                if (arr != null && arr.Count > 0)
                {
                    foreach (FS.HISFC.Models.Speciment.SpecSource spec in arr)
                    {
                        specManage.UpdateMatchFlag("1", spec.SpecId.ToString());                      ��
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            }
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ucOutList = new ucOutSpecList(loginPerson);
            if (rbtSpecSource.Checked)
            {
                ucQuerySpecSource = new ucQueryBySpecSource(loginPerson);
                flpQueryCondition.Controls.Add(ucQuerySpecSource);
                foreach (Control c in ucQuerySpecSource.Controls)
                {
                    if (c.Name == "txtBarCode")
                    {
                        TextBox txt = c as TextBox;
                        txt.TextChanged += txt_Changed;
                    }
                }
            }            
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Query(object sender, object neuObject)
        {
            DataSet ds = new DataSet();
            //DataTable dt = new DataTable(); 

            //���ձ걾Դ����
            if (rbtSpecSource.Checked)
            {
                ucQuerySpecSource.GetQueryCondition(ref strSql);                
            }
            if (rbtPatient.Checked)
            {
                ucQueryPatient.GetQueryCondition(ref strSql);
            }
            if (rbtSpecIdImport.Checked)
            {
                strSql = ucSpecId.GetQueryCondition(); 
            }
            if (rbtDiagnose.Checked)
            {
                ucQueryByDiagnose.GetQueryCondition(ref strSql);
            }
           ucOutList.PageSQL = strSql;
            queryManage.Query(strSql, ref ds);

            if (ds.Tables.Count > 0)
            {               
                ucOutList.DataBinding(ds.Tables[0]);
            }
            //Dock����
            ucOutList.Dock = DockStyle.Fill;
            grpQueryResult.Controls.Add(ucOutList);
            return base.Query(sender, neuObject);
        }

        /// <summary>
        /// ���ݵ�ѡ���ѡ����ز�ͬ���û���ѯ�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbt_Changed(object sender, EventArgs e)
        {
            if (rbtSpecSource.Checked)
            {
                flpQueryCondition.Controls.Clear();
                flpQueryCondition.Controls.Add(ucQuerySpecSource);
                foreach (Control c in ucQuerySpecSource.Controls)
                {
                    if (c.Name == "txtBarCode")
                    {
                        TextBox txt = c as TextBox;
                        txt.TextChanged += txt_Changed;
                    }
                }
            }
            if (rbtPatient.Checked)
            {
                ucQueryPatient = new ucQueryByPatient();
                flpQueryCondition.Controls.Clear();
                flpQueryCondition.Controls.Add(ucQueryPatient);
            }
            if (rbtDiagnose.Checked)
            {
                ucQueryByDiagnose = new ucQueryDiagnose();
                flpQueryCondition.Controls.Clear();
                flpQueryCondition.Controls.Add(ucQueryByDiagnose);
            }
            if (rbtSpecIdImport.Checked)
            {
                flpQueryCondition.Controls.Clear();
                ucSpecId = new ucSpecIdImport();
                flpQueryCondition.Controls.Add(ucSpecId);
            }
        }

        private void txt_Changed(object sender, EventArgs e)
        {          
            this.Query(sender, null);
        }

    }
}
