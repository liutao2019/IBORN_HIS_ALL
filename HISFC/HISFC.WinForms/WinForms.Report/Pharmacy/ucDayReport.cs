using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// [��������: �ս��ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public partial class ucDayReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucDayReport()
        {
            InitializeComponent();
        }



        #region ����

        /// <summary>
        /// ��ѯ��ʼʱ��
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpBeginTime.Text);
            }
        }

        /// <summary>
        /// ��ѯ��ֹʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpEndTime.Text);
            }
        }

        #endregion

        #region ������

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();

            ArrayList alStockDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                if (dept.DeptType.ID.ToString() == "P" || dept.DeptType.ID.ToString() == "PI")
                {
                    alStockDept.Add(dept);
                }
            }

            this.cmbStockDept.AddItems(alStockDept);

            this.dtpBeginTime.Value = deptManager.GetDateTimeFromSysDateTime().AddDays(-1);
            this.dtpEndTime.Value = deptManager.GetDateTimeFromSysDateTime();
            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        protected int Query()
        {
            if (this.cmbStockDept.Tag == null || this.cmbStockDept.Tag.ToString() == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯҩ��"));
                return -1;
            }

            System.Data.DataSet ds = new DataSet();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();
            if (dataManager.ExecQuery("Pharmacy.DayStore.DayReport", ref ds, this.cmbStockDept.Tag.ToString(), this.BeginTime.ToString(), this.EndTime.ToString()) == -1)
            {
                MessageBox.Show(Language.Msg("��ѯ��������") + dataManager.Err);
                return -1;
            }

            if (ds == null || ds.Tables.Count <= 0)
            {
                return 0;
            }

            this.fpHead.DataSource = ds;

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }
    }
}
