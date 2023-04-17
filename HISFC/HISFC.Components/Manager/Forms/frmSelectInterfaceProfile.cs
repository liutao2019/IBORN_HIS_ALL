using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Manager.Forms
{
    public partial class frmSelectInterfaceProfile : Form
    {

        public frmSelectInterfaceProfile()
        {

            InitializeComponent();
          
        }
        FS.FrameWork.WinForms.Classes.ReportPrintManager rpm = new FS.FrameWork.WinForms.Classes.ReportPrintManager();
        protected override void OnLoad(EventArgs e)
        {
            List<FS.FrameWork.Models.NeuObject> listIP = new List<FS.FrameWork.Models.NeuObject>();
            rpm.QueryCurrenInterfaceProfileTable(ref listIP);
            DispToFP(listIP);
            base.OnLoad(e);
        }
        protected virtual int DispToFP(List<FS.FrameWork.Models.NeuObject> ip)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            foreach (NeuObject no in ip)
            {
                //FarPoint.Win.Spread.CellType.MultiOptionCellType moct;
                
              
                this.neuSpread1_Sheet1.RowCount++;
                int rowIdx = this.neuSpread1_Sheet1.RowCount-1;
                //moct = this.neuSpread1_Sheet1.Cells[rowIdx, 0].CellType;
                //moct.SetEditorValue(
                if (no.Memo.ToString()=="1")
                {
                    this.neuSpread1_Sheet1.Cells[rowIdx, 0].Text = " ";
                }
                this.neuSpread1_Sheet1.Cells[rowIdx, 1].Text = no.ID;
                this.neuSpread1_Sheet1.Cells[rowIdx, 2].Text = no.Name;
                if (no.ID=="COM_MAINTENANCE_REPORT_PRINT")
                {
                    this.neuSpread1_Sheet1.Cells[rowIdx, 1].Locked = true;
                }
            }
            return 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool hasSelected = false;
            List<NeuObject > nos = new List<NeuObject>();
             for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                NeuObject no = new NeuObject();
                no.ID = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                no.Name = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == " ")
                {
                    hasSelected = true;
                    no.Memo = "1";
                }
                else
                {
                    no.Memo = "0";
                }
                nos.Add(no);
            }
             if (hasSelected==false)
             {
                 if (nos.Count>0)
                 {
                     nos[0].Memo = "1";
                 }
             }
             FS.FrameWork.Management.PublicTrans.BeginTransaction();
             try
             {
                 if (rpm.DeleteCurrenInterfaceProfileTable() < 0)
                 {

                     FS.FrameWork.Management.PublicTrans.RollBack();
                     System.Windows.Forms.MessageBox.Show(rpm.Err);
                     return;
                 }
                 if (rpm.InsertCurrenInterfaceProfileTable(nos) < 0)
                 {
                     FS.FrameWork.Management.PublicTrans.RollBack();
                     System.Windows.Forms.MessageBox.Show(rpm.Err);
                     return;
                 }
                 FS.FrameWork.Management.PublicTrans.Commit();
                 System.Windows.Forms.MessageBox.Show("保存成功！" );
                 List<FS.FrameWork.Models.NeuObject> listIP = new List<FS.FrameWork.Models.NeuObject>();
                 rpm.QueryCurrenInterfaceProfileTable(ref listIP);
                 DispToFP(listIP);
             }
             catch (Exception ex) 
             {
                 FS.FrameWork.Management.PublicTrans.RollBack();
                 System.Windows.Forms.MessageBox.Show("保存失败："+rpm.Err);
             }
        }
        private void btnChanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.RowCount++;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text == "COM_MAINTENANCE_REPORT_PRINT")
            {
                System.Windows.Forms.MessageBox.Show("默认配置不能删除！");
                return;
            }
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column==0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = "";
                }
                this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text = " ";
            }
        }

     
        private void btnCopy_Click(object sender, EventArgs e)
        {

        }      
    }
}
