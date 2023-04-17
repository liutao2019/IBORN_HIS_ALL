using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou.Report
{
    public partial class ucQueryFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryFee()
        {
            InitializeComponent();
        }

        FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        Function.Operation oper = new Function.Operation();

        private void ucQueryInpatientNo1_myEvent()
        {
            PatientInfo patientInfo = radtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);

            if (patientInfo == null)
            {
                MessageBox.Show("没有查到该患者信息");
                return;
            }

            this.lbPatientInfo.Text = patientInfo.Name + "[住院号：" + patientInfo.PID.PatientNO + ",床号："
                + patientInfo.PVisit.PatientLocation.Bed.Name + ",住院科室：" + patientInfo.PVisit.PatientLocation.Dept.Name + "]";

            ArrayList al = oper.GetPatientOprationFee(patientInfo.PID.PatientNO, ((FS.HISFC.Models.Base.Employee)oper.Operator).Dept.ID);

            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("未查询到患者费用！");
                return;
            }
            FS.FrameWork.WinForms.Controls.NeuListView lvAllReg = new FS.FrameWork.WinForms.Controls.NeuListView();

            System.Windows.Forms.ColumnHeader colPatientNo1 = new ColumnHeader();
            System.Windows.Forms.ColumnHeader colName1 = new ColumnHeader();
            System.Windows.Forms.ColumnHeader colTotCost1 = new ColumnHeader();
            System.Windows.Forms.ColumnHeader colOperCode1 = new ColumnHeader();
            System.Windows.Forms.ColumnHeader colFeeDate1 = new ColumnHeader();

            colPatientNo1.Text = "住院号";
            colPatientNo1.Width = 100;
            colName1.Text = "姓名";
            colName1.Width = 60;
            colTotCost1.Text = "总金额";
            colTotCost1.Width = 70;
            colOperCode1.Text = "操作员";
            colOperCode1.Width = 60;
            colFeeDate1.Text = "计费时间";
            colFeeDate1.Width = 150;

            lvAllReg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                colPatientNo1,
                                                colName1,
                                                colTotCost1,
                                                colOperCode1,
                                                colFeeDate1});

            lvAllReg.Dock = System.Windows.Forms.DockStyle.Fill;
            lvAllReg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lvAllReg.FullRowSelect = true;
            lvAllReg.GridLines = true;
            lvAllReg.Location = new System.Drawing.Point(0, 0);
            lvAllReg.Name = "lvAllReg";
            lvAllReg.Size = new System.Drawing.Size(500, 250);
            lvAllReg.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            lvAllReg.TabIndex = 1;
            lvAllReg.UseCompatibleStateImageBehavior = false;
            lvAllReg.View = System.Windows.Forms.View.Details;

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                ListViewItem item = new ListViewItem();
                item.Text = obj.ID;
                item.Tag = obj.ID;
                item.SubItems.Add(obj.Name);
                item.SubItems.Add(obj.Memo);
                item.SubItems.Add(obj.User01);
                item.SubItems.Add(obj.User02);

                lvAllReg.Items.Add(item);
            }

            lvAllReg.DoubleClick += new EventHandler(lvAllReg_DoubleClick);

            if (al.Count > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(lvAllReg, FormBorderStyle.None);
            }
            //else
            //{
            //    ListViewItem listItem = lvAllReg.Items[0];

            //    if (listItem != null)
            //    {
            //        this.ucQueryInpatientNo1.Text = listItem.SubItems[0].Text;
            //        DataSet ds = new DataSet();

            //        this.oper.GetPatientOprationFeeDetail(listItem.SubItems[0].Text, ((FS.HISFC.Models.Base.Employee)oper.Operator).Dept.ID,
            //            listItem.SubItems[4].Text, ref ds);

            //        this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;
            //        this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计：";
            //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = listItem.SubItems[2].Text;
            //    }
            //}
        }

        /// <summary>
        /// 双击检索的患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lvAllReg_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem listItem = new ListViewItem();
            if ((sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems.Count > 0)
            {
                listItem = (sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems[0];

                if (listItem != null)
                {
                    this.ucQueryInpatientNo1.Text = listItem.SubItems[0].Text;
                }
            }

            ((sender as ListView).Parent as Form).Close();

            DataSet ds = new DataSet();

            this.oper.GetPatientOprationFeeDetail(listItem.SubItems[0].Text, ((FS.HISFC.Models.Base.Employee)oper.Operator).Dept.ID,
                listItem.SubItems[4].Text, ref ds);

            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 6;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计：";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = listItem.SubItems[2].Text;
        }
    }
}
