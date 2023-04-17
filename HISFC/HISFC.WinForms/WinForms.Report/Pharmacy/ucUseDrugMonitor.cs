using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// LC ������ҩ���
    /// </summary>
    public partial class ucUseDrugMonitor : FS.HISFC.Components.Pharmacy.Report.ucQueryBase
    {
        public ucUseDrugMonitor()
        {
            InitializeComponent();

            this.Init();
        }

        #region �����

        /// <summary>
        /// �Ƿ����ѡ��ҩƷ��ѯ
        /// </summary>
        private bool isShowDrug = false;

        /// <summary>
        /// ��ѯ���
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes type = FS.HISFC.Models.Base.ServiceTypes.C;

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ����ѡ��ҩƷ��ѯ
        /// </summary>
        public bool IsShowDrug
        {
            get            
            {
                return this.isShowDrug;
            }
            set
            {
                this.isShowDrug = value;
            }
        }

        /// <summary>
        /// ��ѯ���
        /// </summary>
        public FS.HISFC.Models.Base.ServiceTypes Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        #endregion

        /// <summary>
        /// ���ݳ�ʼ�� 
        /// </summary>
        protected void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ػ�����ѯ���� ���Ժ�...");
            Application.DoEvents();

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            System.Collections.ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("���ؿ����б�ʧ��");
                return;
            }
            System.Collections.ArrayList alData = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Base.Department info in alDept)
            {
                if (info.DeptType.ID.ToString() == "P" || info.DeptType.ID.ToString() == "PI")
                {
                    alData.Add(info);
                }
            }
            this.InitItemData(0, FS.HISFC.Components.Pharmacy.Report.CustomItemTypeEnum.Custom, "��ѯҩƷ��", alData);

            if (this.isShowDrug)
            {
                this.InitItemData(1, FS.HISFC.Components.Pharmacy.Report.CustomItemTypeEnum.Drug, "", null);
                this.ckSingle.Visible = true;
            }
            else
            {
                this.ckSingle.Visible = false;
            }

            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            System.Collections.ArrayList alPerson = personManager.GetEmployeeAll();
            this.personHelper = new FS.FrameWork.Public.ObjectHelper(alPerson);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        protected override string GetSqlIndex()
        {
            if (this.type == FS.HISFC.Models.Base.ServiceTypes.C)
            {
                if (this.ckSingle.Checked)
                {
                    return "DrugStore.Report.Out.UseDrugMonitor.Drug";
                }
                else
                {
                    return "DrugStore.Report.Out.UseDrugMonitor.AllDrug";
                }
            }
            else
            {
                if (this.ckSingle.Checked)
                {
                    return "DrugStore.Report.In.UseDrugMonitor.Drug";
                }
                else
                {
                    return "DrugStore.Report.In.UseDrugMonitor.AllDrug";
                }
                return null;
            }
        }

        protected override string FormatExecSql(string sql)
        {
            if (this.FirstItemData == null)
            {
                MessageBox.Show("��ѡ���ѯҩ��");
                return null;
            }
            if (this.ckSingle.Checked)
            {
                if (this.SecondItemData == null)
                {
                    MessageBox.Show("��ѡ���ѯҩƷ");                    
                }
                return string.Format(sql, this.BeginDate.ToString(), this.EndDate.ToString(), this.FirstItemData,this.SecondItemData);
            }
            else
            {
                return string.Format(sql, this.BeginDate.ToString(), this.EndDate.ToString(), this.FirstItemData);
            }
        }

        protected override DataSet QueryDataSet(string sql)
        {
            DataSet ds = base.QueryDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Columns.Contains("����ҽ��"))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dr["����ҽ��"] = this.personHelper.GetName(dr["����ҽ��"].ToString());
                    }
                }
            }

            return ds;
        }

        private void ckSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckSingle.Checked)
            {
                this.cmbItem2.Enabled = true;
            }
            else
            {
                this.cmbItem2.Enabled = false;
            }
        }

    }
}
