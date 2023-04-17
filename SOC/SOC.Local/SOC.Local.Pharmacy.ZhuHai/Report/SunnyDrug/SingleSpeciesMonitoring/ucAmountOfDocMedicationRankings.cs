using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.SingleSpeciesMonitoring
{
    public partial class ucAmountOfDocMedicationRankings : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 医生用药用药金额排名
        /// </summary>
        public ucAmountOfDocMedicationRankings()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.AmountOfDocMedicationRankings.In";
            this.MainTitle = "医生用药用药金额排名";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.CustomTypeShowType = "名次：";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select '10' id,'10' name,'10' memo,'10' spell_code,'10','10' input_code from dual";
         
            this.IsDeptAsCondition = true;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            this.cbInPatient.CheckedChanged += new EventHandler(cbInPatient_CheckedChanged);
            this.cbOutPatient.CheckedChanged += new EventHandler(cbOutPatient_CheckedChanged);
            this.cbInPatient.Visible = true;
            this.cbOutPatient.Visible = true;
        }

        void cbInPatient_CheckedChanged(object sender, EventArgs e)
        {
            this.cbOutPatient.Checked = !this.cbInPatient.Checked;
        }

        void cbOutPatient_CheckedChanged(object sender, EventArgs e)
        {
            this.cbInPatient.Checked = !this.cbOutPatient.Checked;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.lbDept.Text = "药品";
            this.cmbCustomType.Text = "10";
            this.cmbCustomType.Tag = "10";
            this.cmbCustomType.Enabled = false;
            FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem("1");
            this.cmbDept.Items.Clear();
            ArrayList allData = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper.ArrayObject;
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "AAAA";
            allObj.Name = "全部";
            allData.Insert(0, allObj);
            this.cmbDept.AddItems(allData);
            this.cmbDept.SelectedIndex = 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cbInPatient.Checked)
            {
                this.SQLIndexs = @"Pharmacy.NewReport.SunnyDrug.AmountOfDocMedicationRankings.In";
            }
            else
            {
                this.SQLIndexs = @"Pharmacy.NewReport.SunnyDrug.AmountOfDocMedicationRankings.Out";
            }
            return base.OnQuery(sender, neuObject);
        }
    }
}
