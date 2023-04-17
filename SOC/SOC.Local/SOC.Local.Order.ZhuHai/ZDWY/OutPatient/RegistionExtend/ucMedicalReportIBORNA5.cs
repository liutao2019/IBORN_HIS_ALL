using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend
{
    public partial class ucMedicalReportIBORNA5 : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Order.IMedicalReportPrint
    {
        /// <summary>
        /// 获取医院名字
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();


        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant conManager = null;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        public static FS.HISFC.BizLogic.Manager.Constant ConManager
        {
            get
            {
                if (conManager == null)
                {
                    conManager = new FS.HISFC.BizLogic.Manager.Constant();
                }

                return conManager;
            }
        }

        public ucMedicalReportIBORNA5()
        {
            InitializeComponent();
            //this.lblTitle.Text = this.phaManager.Hospital.Name;
            this.neuLabel1.Text = "基本信息 General Information";
            this.neuLabel5.Text = "病历记录 Progress Note";
            this.neuLabel8.Text = "医嘱信息 Medical Order";
            this.label16.Text = "";
            this.neuLabel12.Text = "健康教育评估与记录 Physical Assessment";

            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            ArrayList writedep = ConManager.GetList("HEALTHASSESSMENT");  //是否需要填写


            if ((writedep.ToArray().FirstOrDefault() as FS.FrameWork.Models.NeuObject).Name.Contains(currDept.ID))
            {
                this.panel25.Visible = true;
            }
            else
            {
                this.panel25.Visible = false;
            }


        }

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string regId = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string RegId
        {
            get { return regId; }
            set
            {
                regId = value;
                this.SetInfo();
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private string err = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            get { return err; }
            set { err = value; }
        }


        /// <summary>
        /// 是否打印
        /// </summary>
        private bool isPrint = false;
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return isPrint; }
            set { isPrint = value; }
        }
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;


        private void Clear()
        {
            this.lbInvestigation.Text = "";
            this.tbAllergicHistory.Text = "";
            this.lbPresentMedicalhistory.Text = "";
            this.lbMedicalHistory.Text = "";
            this.lbPhysicalExamination.Text = "";
            this.lbRecommendation.Text = "";
            this.lblMemo.Text = "";
            this.lblExhortation.Text = "";
            this.lblTreatment.Text = "";
            this.lblCardNO.Text = "";
            this.lblDept.Text = "";
            this.lblName.Text = "";
            this.lbVitalSigns.Text = "";
            this.lblName1.Text = "";
            this.lblDoct.Text = "";
            this.lbDiagnosis.Text = "";
            //this.isNew = false;
            this.lblPrintDate.Text = "";
            this.lblDate.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public int LengthString(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }
        public int SetInfo()
        {
            this.Clear();

            if (string.IsNullOrEmpty(this.regId))
            {
                this.err = "请录入门诊流水号！";
                return -1;
            }
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            //挂号信息
            FS.HISFC.Models.Registration.Register regiter = registerMgr.GetByClinic(this.regId);



            this.lblCardNO.Text = regiter.PID.CardNO;
            this.lblDept.Text = regiter.DoctorInfo.Templet.Dept.Name;
            this.lblName.Text = regiter.Name;
            this.lblName1.Text = regiter.Name;

            int strLength = this.LengthString(regiter.Name);
            if (strLength > 20)
            {
                this.lblName1.Visible = true;
                this.lblName.Visible = false;
            }
            else
            {

                this.lblName1.Visible = false;
                this.lblName.Visible = true;
            }
            this.lblSex.Text = regiter.Sex.Name;
            this.lblAge.Text = this.outOrderMgr.GetAge(regiter.Birthday, false);
            this.lblDoct.Text = regiter.DoctorInfo.Templet.Doct.Name;
            //this.isNew = false;
            this.lblPrintDate.Text = registerMgr.GetSysDateTime();
            this.lblDate.Text = regiter.DoctorInfo.SeeDate.ToString();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.LineBorder lineBorder2 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, true, true);
            FarPoint.Win.LineBorder lineBorder3 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, false, true);
            FarPoint.Win.LineBorder lineBorder4 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, true, false, false);
            FarPoint.Win.LineBorder lineBorder5 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, false, false, false, false);
            textCellType1.Multiline = true;
            textCellType1.WordWrap = true;
            //体征信息
            //this.labelSeeDate.Text = regiter.DoctorInfo.SeeDate.ToShortDateString();
            FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                   = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
            string Pulse = "";
            string BP = "";
            string Tempitrue = "";
            System.Collections.Hashtable hsNR = gjMgr.QueryGJRegisterInfo(this.regId, "NR");
            FS.FrameWork.Models.NeuObject obj = null;
            if (hsNR != null)
            {
                if (hsNR.ContainsKey("NRtBPulse"))
                {
                    obj = hsNR["NRtBPulse"] as FS.FrameWork.Models.NeuObject;
                    Pulse = obj.Memo;
                }
                if (hsNR.ContainsKey("NRtBBP"))
                {
                    obj = hsNR["NRtBBP"] as FS.FrameWork.Models.NeuObject;
                    BP = obj.Memo;
                }
                if (hsNR.ContainsKey("NRtBTemp"))
                {
                    obj = hsNR["NRtBTemp"] as FS.FrameWork.Models.NeuObject;
                    Tempitrue = obj.Memo;
                }
            }

            //医嘱信息
            string strOrder = "";

            int height = 0;
            System.Collections.ArrayList alRecipe = new System.Collections.ArrayList();
            if (!string.IsNullOrEmpty(regiter.Card.ID))
            {
                alRecipe = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.Card.ID, this.regId);
            }
            else
            {
                alRecipe = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.PID.CardNO, this.regId);
            }
            //(this.regId, regiter.DoctorInfo.SeeNO.ToString());
            string eName = "";
            if (alRecipe.Count > 0)
            {

                foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in alRecipe)
                {

                    if (orderTemp != null && orderTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        eName = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderTemp.Item.ID).NameCollection.EnglishName;
                        if (string.IsNullOrEmpty(eName))
                        {
                            eName = orderTemp.Item.Name;
                        }
                        this.neuSpread1_Sheet1.RowCount += 1;
                        int index = this.neuSpread1_Sheet1.RowCount - 1;
                        this.neuSpread1_Sheet1.Rows.Get(index).Height = 30F;
                        //strOrder +=  eName + "  " + orderTemp.Frequency.Name + "  " + orderTemp.Usage.Name +  "\n\r";
                     
                        string dose = (double)orderTemp.DoseOnce > 0.0 ? "每次 " + orderTemp.DoseOnce + "" + orderTemp.DoseUnit : "";
                        this.neuSpread1_Sheet1.Cells[index, 0].Text = (index + 1).ToString() + "." + eName + "   " + orderTemp.Usage.Name + "   " + orderTemp.Frequency.Name + "   " + dose;
                        this.neuSpread1_Sheet1.Cells[index, 0].CellType = textCellType1;
                        this.neuSpread1_Sheet1.Cells[index, 0].Border = lineBorder5;
                        this.neuSpread1_Sheet1.Cells[index, 0].Font = new System.Drawing.Font("宋体", 10F);

                        this.neuSpread1_Sheet1.Cells[index, 1].Text = orderTemp.Frequency.Name;
                        this.neuSpread1_Sheet1.Cells[index, 1].CellType = textCellType1;
                        this.neuSpread1_Sheet1.Cells[index, 1].Border = lineBorder5;
                        this.neuSpread1_Sheet1.Cells[index, 1].Font = new System.Drawing.Font("宋体", 10F);

                        this.neuSpread1_Sheet1.Cells[index, 2].Text = orderTemp.Usage.Name;
                        this.neuSpread1_Sheet1.Cells[index, 2].CellType = textCellType1;
                        this.neuSpread1_Sheet1.Cells[index, 2].Border = lineBorder5;
                        this.neuSpread1_Sheet1.Cells[index, 2].Font = new System.Drawing.Font("宋体", 10F);

                        //this.neuSpread1_Sheet1.Cells[index, 3].Text = orderTemp.DoseOnce + "" + orderTemp.DoseUnit;
                        //this.neuSpread1_Sheet1.Cells[index, 3].CellType = textCellType1;
                        //this.neuSpread1_Sheet1.Cells[index, 3].Border = lineBorder5;
                        //this.neuSpread1_Sheet1.Cells[index, 3].Font = new System.Drawing.Font("宋体", 10F);

                        //if (index == 0)
                        //{
                        //    this.neuSpread1_Sheet1.Cells[index, 0].Border = lineBorder4;
                        //    this.neuSpread1_Sheet1.Cells[index, 1].Border = lineBorder4;
                        //    this.neuSpread1_Sheet1.Cells[index, 2].Border = lineBorder4;
                        //}
                        //index++;
                    }
                }

                if (alRecipe.Count > 12)//{E4DA74DC-1BFF-4591-A0B5-3A9184728F8F} 修改显示，超过12行，缩小字体
                {
                    //this.lbTreatment.Font = new System.Drawing.Font("Times New Roman", 9F);
                }
            }

            int count = this.neuSpread1_Sheet1.RowCount;
            height = count * 35;
            panel6.Height = height + 25;


            //this.neuSpread1_Sheet1.RowCount += 1;
            //int count = this.neuSpread1_Sheet1.RowCount - 1;

            //this.neuSpread1_Sheet1.Cells[count, 0].Text = "(以下为空)";
            //this.neuSpread1_Sheet1.Cells[count, 0].CellType = textCellType1;
            //this.neuSpread1_Sheet1.Cells[count, 0].Border = lineBorder5;
            //this.neuSpread1_Sheet1.Cells[count, 1].Border = lineBorder5;
            //this.neuSpread1_Sheet1.Cells[count, 2].Border = lineBorder5;
            //this.neuSpread1_Sheet1.Cells[count, 0].Font = new System.Drawing.Font("宋体", 10F);
            //诊断信息
            System.Collections.ArrayList alDiag
                = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.regId,
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, true);
            string strDiag = "";
            int i = 1;
            if (alDiag.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                {
                    strDiag += i.ToString() + "、 " + diag.DiagInfo.ICD10.Name + "  ";
                    i++;
                }
            }

            //病历信息
            this.caseHistory = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(this.regId);
            if (this.caseHistory != null)
            {
                if (string.IsNullOrEmpty(caseHistory.CaseMain) && string.IsNullOrEmpty(caseHistory.CaseNow)
                    && string.IsNullOrEmpty(caseHistory.CaseOld) && string.IsNullOrEmpty(caseHistory.CheckBody)
                    && string.IsNullOrEmpty(caseHistory.Memo))
                {
                    isPrint = false;
                }
                else
                {
                    isPrint = true;
                }
                this.lbInvestigation.Text = caseHistory.CaseMain;//主诉*
                height = LineFeed(lbInvestigation.Text);
                panel5.Height = height + 1 > 39 ? height + 1 : 40;

                this.lbPresentMedicalhistory.Text = caseHistory.CaseNow;//现病史*
                height = LineFeed(lbPresentMedicalhistory.Text);
                panel1.Height = height + 1 > 39 ? height + 1 : 40;

                this.lbMedicalHistory.Text = caseHistory.CaseOld;//既往史*
                height = LineFeed(lbMedicalHistory.Text);
                panel2.Height = height + 1 > 39 ? height + 1 : 40;

                this.lbPhysicalExamination.Text = caseHistory.CheckBody;//查体*
                height = LineFeed(lbPhysicalExamination.Text);
                panel3.Height = height + 1 > 39 ? height + 1 : 40;

                this.lblMemo.Text = caseHistory.Memo2;//备注
                height = LineFeed(lblMemo.Text);
                panel33.Height = height + 1 > 39 ? height + 1 : 40;

                this.lbRecommendation.Text = caseHistory.SupExamination;//辅助检查
                height = LineFeed(lbRecommendation.Text);
                panel8.Height = height + 1 > 39 ? height + 1 : 40;

                this.lblExhortation.Text = caseHistory.Memo; //嘱托
                height = LineFeed(lblExhortation.Text);
                panel39.Height = height + 1 > 39 ? height + 1 : 40;


                this.tbAllergicHistory.Text = caseHistory.CaseAllery;//过敏史
                height = LineFeed(tbAllergicHistory.Text);
                panel19.Height = height + 1 > 39 ? height + 1 : 40;

                this.lbDiagnosis.Text = strDiag;//诊断
                height = LineFeed(lbDiagnosis.Text);
                panel17.Height = height + 1 + 3 > 39 ? height + 1 : 40;


                //体征
                if (!string.IsNullOrEmpty(BP) || !string.IsNullOrEmpty(BP) || !string.IsNullOrEmpty(BP))
                {
                    this.lbVitalSigns.Text = "BP: {0} mmHg.  Pulse: {1} x’.   T. {2} C";
                    this.lbVitalSigns.Text = string.Format(this.lbVitalSigns.Text, BP, Pulse, Tempitrue);

                    height = LineFeed(lblTreatment.Text);
                    panel4.Height = height + 1;
                }
                else
                {
                    this.lbVitalSigns.Text = "";
                }

                //this.txtDiagnose.Text = caseHistory.CaseDiag;
                //this.OldOperTime = caseHistory.CaseOper.OperTime.ToString();//操作时间
                //this.lbReportBy.Text = registerMgr.Operator.Name + "\n" + DateTime.Now.ToString();
                //this.lbVitalSigns.Text = string.Format(this.lbVitalSigns.Text, BP, Pulse, Tempitrue);
                this.lblTreatment.Text = caseHistory.User01; ;//处理
                height = LineFeed(lblTreatment.Text);
                label34.Height = height + 1 > 39 ? height + 1 : 40;


                this.txtemredu.Text = caseHistory.Emr_Educational;
                this.txteducation.Text = caseHistory.EducationContent;
                this.txtdiagnose.Text = caseHistory.PatientDiagnose;
                this.txtmedic.Text = caseHistory.MedicationKnowledge;
                this.txtdiet.Text = caseHistory.DiteKnowledge;
                this.txtdisease.Text = caseHistory.DiseaseKnowledge;
                this.txteffect.Text = caseHistory.EducationalEffect;
            
                //this.label16.Text = "医嘱信息：";


            }
            else
            {
                this.err = "没有找到相关记录！";
                return -1;
            }


            if (!this.panel25.Visible)
            {
                this.panel24.Height = this.neuPanel2.Height + this.panel5.Height + this.panel2.Height + this.panel19.Height + this.panel3.Height + this.panel4.Height + this.label34.Height + this.panel17.Height + this.panel6.Height + this.panel8.Height + this.panel1.Height - 4;//- 89 +70
                this.panel14.Height = this.neuPanel2.Height + this.panel5.Height + this.panel2.Height + this.panel19.Height + this.panel3.Height + this.panel4.Height + this.label34.Height + this.panel17.Height + this.panel6.Height + this.panel8.Height + this.panel1.Height - 4;//- 89 + 70
            }
            else
            {
                this.panel24.Height = this.neuPanel2.Height + this.panel5.Height + this.panel2.Height + this.panel19.Height + this.panel3.Height + this.panel4.Height + this.label34.Height + this.panel17.Height + this.panel6.Height + this.panel8.Height + this.panel1.Height + this.panel25.Height - 52;
                this.panel14.Height = this.neuPanel2.Height + this.panel5.Height + this.panel2.Height + this.panel19.Height + this.panel3.Height + this.panel4.Height + this.label34.Height + this.panel17.Height + this.panel6.Height + this.panel8.Height + this.panel1.Height + this.panel25.Height - 52;
           
            }
       
            //this.Print();
            //SetWidth();
            return 1;
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void PrintView()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("A4");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("A4", 850, 1100);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }
        /// <summary>
        /// 计算行数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int LineFeed(string str)
        {
            string[] stringN = str.Split('\n');

            int addRow = 0;
            foreach (string ss in stringN)
            {
                addRow++;
            }
            if (addRow > 1)
            {
                addRow = addRow - 1;
            }
            if (str == null || str.Length == 0) { return 19; }

            #region 计算长度
            int clen = System.Text.Encoding.Default.GetBytes(str).Length;

            #endregion
            int Height = 0;
            int rowNum = 58;
            int rowHeight = 17;
            int colNum = (clen - (clen / rowNum) * rowNum) == 0 ? (clen / rowNum) : (clen / rowNum) + 1 + addRow;
            Height = rowHeight * colNum;
            return Height;

        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            this.Dock = DockStyle.None;

            //字走到哪打到哪的方式，故不设置纸张名称，而是由计算得出
            int height = 10;

            int rowCount = this.neuSpread1_Sheet1.RowCount;
            //动态可变高度，设计器初始行数建议调整为0，即只有行头
            //int addHeight = rowCount == 0 ? 0 : 4 * (int)this.neuSpread1_Sheet1.Rows[0].Height;

            //int addHeight1 = rowCount == 0 ? this.neuSpread1.Height : rowCount * (int)this.neuSpread1_Sheet1.Rows[0].Height;


            //纸张实际高度由固定长度+动态可变长度组成
            height = this.neuPanel2.Height + this.panel5.Height + this.panel1.Height + this.panel2.Height + this.panel19.Height + this.panel3.Height + this.panel4.Height + this.label34.Height + this.panel17.Height + this.panel6.Height + this.panel8.Height;

            //SetWidth();

            this.Size = new System.Drawing.Size(850, 1100);

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
            int iPage = 1;
            iPage = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(height.ToString()) / 1100).ToString());
            ps = new FS.HISFC.Models.Base.PageSize("A4", 850, iPage * 1100);
            print.SetPageSize(ps);

            //this.panel8.Dock = DockStyle.None;
            //this.panel8.Location = new Point(this.neuPanel2.Location.X, height - this.panel8.Height - (rowCount == 0 ? 20 : (int)this.neuSpread1_Sheet1.Rows[0].Height));

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }

            //this.panel8.Dock = DockStyle.Bottom;
            //this.Dock = DockStyle.Fill;
        }


        void SetWidth()
        {
            this.panel5.Width = this.neuPanel2.Width;
            this.panel2.Width = this.neuPanel2.Width;
            this.panel19.Width = this.neuPanel2.Width;
            this.panel3.Width = this.neuPanel2.Width;
            this.panel4.Width = this.neuPanel2.Width;
            this.label34.Width = this.neuPanel2.Width;
            this.panel17.Width = this.neuPanel2.Width;
            this.panel6.Width = this.neuPanel2.Width;
            this.panel8.Width = this.neuPanel2.Width;
            this.panel1.Width = this.neuPanel2.Width;
            //this.neuPanel1.Width = this.neuPanel2.Width;
        }

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }



        //private void panel5_Resize(object sender, EventArgs e)
        //{
        //    Control control = (Control)sender;
        //    if (control.Size.Height != lbInvestigation.Size.Height+1)
        //    {
        //        control.Size = new Size(control.Size.Width, lbInvestigation.Size.Height + 1);
        //    }
        //}

    }
}
