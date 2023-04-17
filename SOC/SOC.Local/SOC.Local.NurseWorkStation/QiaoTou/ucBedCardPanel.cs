using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.NurseWorkStation.QiaoTou
{
    public partial class ucBedCardPanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ArrayList myValues;
        private FS.HISFC.BizProcess.Integrate.RADT Patient = new FS.HISFC.BizProcess.Integrate.RADT();
        private ArrayList myPatients = null;

        public ucBedCardPanel()
        {
            InitializeComponent();
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnInit(sender, neuObject, param);
        }
        /// <summary>
        /// 数值
        /// </summary>
        private ArrayList alValues
        {
            get
            {
                return this.myValues;
            }
            set
            {
                this.myValues = value;
                if (value != null)
                {
                    this.neuPanel1.Controls.Clear();
                    //{A12B2819-A0CC-4ba4-9C57-A6530D72AAFB}
                    //FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(value, typeof(ucBedCardFp), this.neuPanel1, new System.Drawing.Size(790, 1150));

                    FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(value, typeof(ucBedCard), this.neuPanel1, new System.Drawing.Size(196, 285));//(196,265));

                    #region 暂时不用
                    //if (this.rbBed.Checked)
                    //    FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(value, new ucBedCard(), this.panel1, new System.Drawing.Size(800, 1200));
                    //else if (this.rbBrowse.Checked)
                    //    FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(value, new ucBrowseCard(), this.panel1, new System.Drawing.Size(800, 1200));
                    //else if (this.rdOtherCard.Checked)
                    //{
                    //    FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(value, new ucCard(), this.panel1, new System.Drawing.Size(800, 1200));
                    //}
                    #endregion
                }
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        public ArrayList Patients
        {
            get
            {
                return this.myPatients;
            }
            set
            {
                this.myPatients = value;
            }
        }

        private void myQuery()
        {
            ArrayList al = new ArrayList();

            if (this.myPatients == null) return;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询患者信息...");
            for (int i = 0; i < this.myPatients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo P = new FS.HISFC.Models.RADT.PatientInfo();

                //P = this.Patient.QueryPatientInfoByInpatientNO(((FS.HISFC.Models.RADT.PatientInfo)this.myPatients[i]).ID);
                P = this.Patient.GetPatientInfomationNew(((FS.HISFC.Models.RADT.PatientInfo)this.myPatients[i]).ID);
                if (P == null)
                {
                    MessageBox.Show(Patient.Err);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                al.Add(P);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.alValues = al;

            #region 暂时不用
            //if (this.rbBed.Checked)
            //    this.tabPage1.Title = this.rbBed.Text;
            //else if (this.rbBrowse.Checked)
            //    this.tabPage1.Title = this.rbBrowse.Text;
            //else
            //    this.tabPage1.Title = this.comboBox1.Text;
            #endregion
        }

        private void ucBedCardPanel_Load(object sender, EventArgs e)
        {
            //this.myPatients = this.Patient.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);
            //FS.HISFC.BizLogic.Manager.Constant manager = new FS.HISFC.BizLogic.Manager.Constant();
            //this.neuComboBox1.AddItems(manager.GetList("NURSECARD"));
            //if (this.neuComboBox1.Items.Count > 0)
            //    this.neuComboBox1.SelectedIndex = 0;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return 0;
        }
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            if (alValues == null )
                return -1;

            this.Patients = alValues;
            this.myQuery();
            return 0;
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
            pSize = pgMgr.GetPageSize("Nurse6");
            p.SetPageSize(pSize);

             
            //p.PrintPreview(0,0,this.neuPanel1 );
            p.PrintPage(0, 0, this.neuPanel1);
            return 0;
        }
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview( this.neuPanel1);
            return 0;
        }
    }
}