using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend
{
    public partial class ucMedicalReportIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 获取医院名字
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        public ucMedicalReportIBORN()
        {
            InitializeComponent();
            this.lblTitle.Text = this.phaManager.Hospital.Name;
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
            set { err = value;}
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
            this.lbPresentMedicalhistory.Text = "";
            this.lbMedicalHistory.Text = "";
            this.lbPhysicalExamination.Text = "";
            this.lbRecommendation.Text = "";
            this.lbDiagnosis.Text = "";
            this.lblCardNO.Text = "";
            this.lblDept.Text = "";
            this.lblName.Text = "";
            this.lbVitalSigns.Text = "";            
            this.lblName1.Text = "";
            this.lblDoct.Text = "";
            this.lblTreatment.Text = "";
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
            System.Collections.ArrayList alRecipe = new System.Collections.ArrayList();
            if (!string.IsNullOrEmpty(regiter.Card.ID))
            {
                alRecipe = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.Card.ID, this.regId);
            }
            else
            {
                alRecipe =  FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.OutOrderMgr.QueryOrderByCardNOandClinicNO(regiter.PID.CardNO, this.regId);
            }
                //(this.regId, regiter.DoctorInfo.SeeNO.ToString());
            string eName = "";
            if (alRecipe.Count > 0)
            {
                
                foreach (FS.HISFC.Models.Order.OutPatient.Order orderTemp in alRecipe)
                {

                    if (orderTemp != null && orderTemp.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
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
                        this.neuSpread1_Sheet1.Cells[index, 0].Text = (index + 1).ToString() +"."+eName + "   " + orderTemp.Usage.Name + "   " + orderTemp.Frequency.Name;
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



            this.neuSpread1_Sheet1.RowCount += 1;
            int count = this.neuSpread1_Sheet1.RowCount - 1;

            this.neuSpread1_Sheet1.Cells[count, 0].Text = "(以下为空)";
            this.neuSpread1_Sheet1.Cells[count, 0].CellType = textCellType1;
            this.neuSpread1_Sheet1.Cells[count, 0].Border = lineBorder5;
            this.neuSpread1_Sheet1.Cells[count, 1].Border = lineBorder5;
            this.neuSpread1_Sheet1.Cells[count, 2].Border = lineBorder5;
            this.neuSpread1_Sheet1.Cells[count, 0].Font = new System.Drawing.Font("宋体", 10F);
            //诊断信息
            System.Collections.ArrayList alDiag
                = FS.SOC.Local.Order.ZhuHai.Classes.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.regId,
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
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
                this.lbPresentMedicalhistory.Text = caseHistory.CaseNow;//现病史*
                this.lbMedicalHistory.Text = caseHistory.CaseOld;//既往史*
                this.lbPhysicalExamination.Text = caseHistory.CheckBody;//查体*
                this.lbRecommendation.Text = caseHistory.Memo;//备注
                //this.tbAllergicHistory.Text = caseHistory.CaseAllery;//过敏史
                this.lbDiagnosis.Text = strDiag;//诊断
                //this.txtDiagnose.Text = caseHistory.CaseDiag;
                //this.OldOperTime = caseHistory.CaseOper.OperTime.ToString();//操作时间
                //this.lbReportBy.Text = registerMgr.Operator.Name + "\n" + DateTime.Now.ToString();
                //this.lbVitalSigns.Text = string.Format(this.lbVitalSigns.Text, BP, Pulse, Tempitrue);
                this.lblTreatment.Text = caseHistory.User01; ;//处理
                this.label16.Text = "医嘱信息：";
            }
            else
            {
                this.err = "没有找到相关记录！";
                return -1;
            }
            //this.Print();
            return 1;
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
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
        /// 打印
        /// </summary>
        public void PrintView()
        {
            this.Dock = DockStyle.None;

            //字走到哪打到哪的方式，故不设置纸张名称，而是由计算得出
            int height = 10;

            int rowCount = this.neuSpread1_Sheet1.RowCount;
            //动态可变高度，设计器初始行数建议调整为0，即只有行头
            int addHeight = rowCount == 0 ? 0 : 4 * (int)this.neuSpread1_Sheet1.Rows[0].Height;

            int addHeight1 = rowCount == 0 ? this.neuSpread1.Height : rowCount * (int)this.neuSpread1_Sheet1.Rows[0].Height;


            //纸张实际高度由固定长度+动态可变长度组成
            height = (addHeight + this.neuPanel2.Height + this.panel1.Height * 7 + this.label16.Height + addHeight1);

            this.Size = new System.Drawing.Size(850, 550);

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
            int iPage = 1;
            iPage = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(height.ToString()) / 550).ToString());
            ps = new FS.HISFC.Models.Base.PageSize("550", 850, iPage * 550);
            print.SetPageSize(ps);

            this.panel8.Dock = DockStyle.None;
            this.panel8.Location = new Point(this.neuPanel2.Location.X, height - this.panel8.Height - (rowCount == 0 ? 20 : (int)this.neuSpread1_Sheet1.Rows[0].Height));

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

            this.panel8.Dock = DockStyle.Bottom;
            this.Dock = DockStyle.Fill;
        }
       
    }
}
