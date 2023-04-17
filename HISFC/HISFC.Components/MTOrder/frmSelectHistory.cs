using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder
{
    public partial class frmSelectHistory : Form
    {
        #region 属性
        /// <summary>
        /// 历史预约
        /// </summary>
        public List<HISFC.Models.MedicalTechnology.Appointment> appHist { get; set; }

        /// <summary>
        /// 用户选择的ClinicNo
        /// </summary>
        public string ClinicNo { get; set; }
        #endregion

        #region 行
        enum cols
        {
            No = 0,
            ClinicNo,
            ApplyDate,
            Name,
            Num
        }
        #endregion
        #region  域
        /// <summary>
        /// 申请历史信息列表
        /// </summary>
        private DataTable dsItems = new DataTable();
        private DataView dv;
        #endregion
        public frmSelectHistory()
        {
            InitializeComponent();
            #region 加载事件
            this.Leave += new System.EventHandler(this.frmSelectHistory_Leave);
            this.Load += new EventHandler(frmSelectHistory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KeyDown);
            neuSpread1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KeyDown);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);

            #endregion
        }
        #region 初始化
        void frmSelectHistory_Load(object sender, EventArgs e)
        {
            if (appHist == null || appHist.Count < 1)
            {
                MessageBox.Show("没有相关信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            InitDataTable();
            InitSpread();
        }
        /// <summary>
        /// 初始化DataTable
        /// </summary>
        private void InitDataTable()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.No.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ClinicNo.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ApplyDate.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Name.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Num.ToString(),System.Type.GetType("System.String"))
			});
        }
        /// <summary>
        /// 初始化Spread
        /// </summary>
        private void InitSpread()
        {
            try
            {
                dsItems.Clear();
                int Index = 1;
                appHist.GroupBy(t => t.ClinicCode).Select(t =>
                    {
                        string ClinicNo = t.Key;
                        string ApplyDate = t.FirstOrDefault().ApplyDate.ToString("yyyy-MM-dd");
                        string Name = t.FirstOrDefault().Name;
                        int Count = t.Count();
                        return new { ClinicNo, ApplyDate, Name, Count };
                    }).ToList().ForEach(t =>
                    {
                        dsItems.Rows.Add(new object[]
                            {
                            Index++,
                            t.ClinicNo,
                            t.ApplyDate,
                            t.Name,
                            t.Count
                            });
                    });

            }
            catch (Exception e)
            {
                MessageBox.Show("历史信息生成DataTable时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();
            dv = dsItems.DefaultView;
            neuSpread1_Sheet1.DataSource = dv;
            SetFpFormat();
        }
        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {
            this.neuSpread1_Sheet1.Columns[(int)cols.ClinicNo].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)cols.No].Width = 30F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyDate].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Name].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Num].Width = 90F;
        }
        #endregion
        private void frmSelectHistory_Leave(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ClinicNo = neuSpread1_Sheet1.GetText(neuSpread1_Sheet1.ActiveRowIndex, (int)cols.ClinicNo);
            this.DialogResult = DialogResult.OK;
        }

        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClinicNo = neuSpread1_Sheet1.GetText(neuSpread1_Sheet1.ActiveRowIndex, (int)cols.ClinicNo);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
