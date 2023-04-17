using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucShiftData : FS.FrameWork.WinForms.Controls.ucBaseControl,IDisposable
    {
        public ucShiftData()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        #region �����

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private ArrayList alDrug = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        protected DateTime BeginTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        protected DateTime EndTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> itemList = itemManager.QueryItemList(true);
            if (itemList == null)
            {
                MessageBox.Show(Language.Msg("�������ݳ�ʼ�� ����ҩƷ�б�������") + itemManager.Err);
                return;
            }

            foreach (FS.HISFC.Models.Pharmacy.Item item in itemList)
            {
                item.Memo = item.Specs;
            }

            this.cmbDrug.AddItems(new ArrayList(itemList.ToArray()));

            itemList.Clear();
            itemList = null;

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            this.dtEnd.Value = sysTime.AddHours(1);
            this.dtBegin.Value = sysTime.AddDays(-7);
        }

        /// <summary>
        /// ��ȡSql����
        /// </summary>
        /// <returns></returns>
        private string GetSqlIndex()
        {
            if (this.ckIngoreDrug.Checked)
            {
                return "Pharmacy.ShiftData.IngoreDrug";
            }

            return "Pharmacy.ShiftData.Detail";
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.BeginTime >= this.EndTime)
            {
                MessageBox.Show(Language.Msg("��ʼʱ�䲻�ܴ��ڵ�����ֹʱ��"));
                return false;
            }
            if (!this.ckIngoreDrug.Checked)
            {
                if (this.cmbDrug.Tag == null || this.cmbDrug.Tag.ToString() == "")
                {
                    MessageBox.Show(Language.Msg("��ѡ�����ѯҩƷ"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int Query()
        {
            if (!this.IsValid())
            {
                return -1;
            }

            string strSqlIndex = this.GetSqlIndex();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            DataSet ds = new DataSet();
            if (dataManager.ExecQuery(strSqlIndex, ref ds, this.cmbDrug.Tag.ToString(), this.BeginTime.ToString(), this.EndTime.ToString()) == -1)
            {
                MessageBox.Show(Language.Msg("ִ�в�ѯ��������") + dataManager.Err);
                return -1;
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.DataSource = ds;
            }
            else
            {
                this.neuSpread1_Sheet1.DataSource = null;
                this.neuSpread1_Sheet1.Rows.Count = 0;
            }

            return 1;
        }

        #endregion      
  
        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return base.Export(sender, neuObject);
        }

        private void ckIngoreDrug_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbDrug.Enabled = !this.ckIngoreDrug.Checked;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.FormClosed += new FormClosedEventHandler(ParentForm_FormClosed);
            }
            base.OnLoad(e);
        }

        void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #region IDisposable ��Ա

        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion
    }
}
