using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.WinForms.WorkStation.Controls
{
    public partial class frmPatientListSet : Form
    {
        public frmPatientListSet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public FS.FrameWork.Models.NeuObject GetEmr()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[0, 1].Text+".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[0, 2].Text;
            return obj;
        }

        public FS.FrameWork.Models.NeuObject GetOrder()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[1, 1].Text+".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[1, 2].Text;
            return obj;
        }

        public FS.FrameWork.Models.NeuObject GetLis()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[2, 1].Text+".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[2, 2].Text;
            return obj;
        }


        public FS.FrameWork.Models.NeuObject GetPacs()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[3, 1].Text+".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[3, 2].Text;
            return obj;
        }

        public FS.FrameWork.Models.NeuObject GetConsulation()
        {
            FS.FrameWork.Models.NeuObject obj = new
                FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[4, 1].Text+".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[4, 2].Text;
            return obj;
        }

        public FS.FrameWork.Models.NeuObject GetSearchPatient()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.fpSpread1_Sheet1.Cells[5, 1].Text + ".dll";
            obj.Name = this.fpSpread1_Sheet1.Cells[5, 2].Text;
            return obj;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
