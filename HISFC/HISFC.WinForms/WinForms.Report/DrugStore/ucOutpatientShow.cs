using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.DrugStore
{
    public partial class ucOutpatientShow : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientShow
    {
        public ucOutpatientShow()
        {
            InitializeComponent();
        }

        #region IOutpatientShow ≥…‘±

        public void ShowInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.neuLabel1.Text = drugRecipe.PatientName;
        }
        public void Clear() { }

        #endregion
    }
}
