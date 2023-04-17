using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder.Forms
{
    public partial class frmOrderHistory : Form
    {
        public frmOrderHistory()
        {
            InitializeComponent();
            this.neuSpread1.CellDoubleClick+=new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            this.KeyDown += new KeyEventHandler(frm_KeyDown);
        }
        enum cols
        {
            ClinicNo,
            Name,
            Sex,
            PhoneNum,
            MTName,
            ItemName,
            ApplyTime
        }
        /// <summary>
        /// 申请历史信息列表
        /// </summary>
        private DataTable dsItems = new DataTable();
        private DataView dv;

        /// <summary>
        /// 需要显示的预约列表
        /// </summary>
        public List<Models.MedicalTechnology.Appointment> AppointList { get; set; }

        /// <summary>
        /// 已选择的预约信息
        /// </summary>
        public Models.MedicalTechnology.Appointment SelectedAppoint { get; set; }

        private void frmOrderHistory_Load(object sender, EventArgs e)
        {
            if (AppointList == null || AppointList.Count < 1)
            {
                MessageBox.Show("没有关相病人信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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
				new DataColumn(cols.ClinicNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Name.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Sex.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.PhoneNum.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.MTName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ItemName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ApplyTime.ToString(),System.Type.GetType("System.String"))
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
                AppointList.ForEach(t =>
                    {
                        dsItems.Rows.Add(new object[]
                            {
                                t.ClinicCode,
                                t.Name,
                                t.Sex,
                                t.Telephone,
                                t.MTName,
                                t.ItemName,
                                t.ApplyDate.ToString("yyyy-MM-dd")
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
            this.neuSpread1_Sheet1.Columns[(int)cols.ClinicNo].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Name].Width = 65F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Sex].Width = 35F;
            this.neuSpread1_Sheet1.Columns[(int)cols.PhoneNum].Width = 85F;
            this.neuSpread1_Sheet1.Columns[(int)cols.MTName].Width = 55F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyTime].Width = 90F;
        }
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.SelectedAppoint = AppointList[neuSpread1_Sheet1.ActiveRowIndex];
            this.DialogResult = DialogResult.OK;
        }
        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectedAppoint = AppointList[neuSpread1_Sheet1.ActiveRowIndex];
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
