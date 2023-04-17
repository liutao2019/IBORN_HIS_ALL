using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY.LisBillPrint
{
    //FS.HISFC.BizProcess.Interface.IRecipePrint
    public partial class ucLisBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucLisBillPrint()
        {
            InitializeComponent();
        }

        #region 变量

        #region 业务层

        /// <summary>
        /// 
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        FS.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 科室分类维护
        /// </summary>
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
        #endregion


        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myReg;


        #endregion

        #region 初始化

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucNewRecipePrint_Load(object sender, System.EventArgs e)
        {

        }

        #endregion


        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// 
        //private Image CreateBarCode(string code)
        //{
        //    BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        //    BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
        //    BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
        //    b.IncludeLabel = true;
        //    b.Alignment = Align;
        //    return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        //}


        #region IOutPatientOrderPrint 成员

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);
            return 1;
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void SetPage(string pageStr)
        {
            this.lblPage.Visible = true;
            this.lblPage.Text = pageStr;
            return;
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;

            if (this.myReg == null)
            {
                return;
            }
            //this.label1.Text = this.interMgr.GetHospitalName() + "检查单";
            GetHospLogo();
            #region 基本信息
            this.lblName.Text = this.myReg.Name;
            this.lblSex.Text = this.myReg.Sex.Name; //性别
            // this.npbRecipeNo.Text = this.myReg.; //申请单号??
            this.lblCardNo.Text = myReg.PID.CardNO; ;//门诊号
            this.lblClinic_Code.Text = myReg.ID;
            this.npbBarCode.Image = FS.SOC.Local.Order.OutPatientOrder.Common.ComFunc.CreateBarCode(this.myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false); //年龄
            #endregion

            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strDiag = "";
            int i = 1;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； " + "\n";
                    i++;
                }
                //this.lblDocName.Text = diag.DiagInfo.Doctor.Name;
            }
            lblDig.Text = strDiag;

            #endregion
        }
        FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> IList, bool isPreview)
        {
            decimal phaMoney = 0m;//金额
            decimal ownPhaMoney = 0m;//自费部分
            string tempID = string.Empty;   //临时保存组合号为了区分
            string tempItem = string.Empty;   //临时保存组合号为了区分
            lbData.Text = IList[0].MOTime.Date.ToString("yyyy") + "-" + IList[0].MOTime.Date.ToString("MM") + "-" + IList[0].MOTime.Date.ToString("dd");
            //lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblPrintTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            #region 查询所有收费项目（附材）

            alSubAndOrder = new ArrayList();
            foreach (FS.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
            {
                if (!feeSeq.Contains(OutPatientOrder.ReciptSequence))
                {
                    ArrayList al = outFeeMgr.QueryFeeDetailByClinicCodeAndRecipeSeq(OutPatientOrder.Patient.ID, OutPatientOrder.ReciptSequence, "ALL");
                    if (al == null)
                    {
                        //errInfo = outpatientFeeMgr.Err;
                        //return -1;
                        al = new ArrayList();
                    }

                    alSubAndOrder.AddRange(al);
                    feeSeq += "|" + OutPatientOrder.ReciptSequence + "|";
                }
            }

            #endregion

            string sample = "";
            string applyNo = "";

            //材料费
            decimal suFee = 0;
            //所有检验项目费用（包含材料费）
            decimal AllFee = 0;

            Hashtable hsCombID = new Hashtable();

            for (int i = 0; i < IList.Count; i++)
            {
                tempItem += IList[i].Item.Name + (string.IsNullOrEmpty(IList[i].Memo) ? "" : "    [备注]" + IList[i].Memo) + "       " + FS.FrameWork.Public.String.ToSimpleString(IList[i].Item.Qty) + IList[i].Unit + "\r\n";
                phaMoney += IList[i].Item.Qty * IList[i].Item.Price;

                if (!sample.Contains(IList[i].Sample.Name))
                {
                    sample += " " + IList[i].Sample.Name;
                }

                if (hsCombID.Contains(IList[i].Combo.ID))
                {
                    continue;
                }
                else
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                    {
                        if (feeItem.Order.Combo.ID == IList[i].Combo.ID)
                        {
                            if (feeItem.Item.IsMaterial)
                            {
                                suFee += feeItem.FT.PayCost + feeItem.FT.OwnCost + feeItem.FT.PubCost;
                            }
                            AllFee += feeItem.FT.PayCost + feeItem.FT.OwnCost + feeItem.FT.PubCost;
                        }
                    }
                    hsCombID.Add(IList[i].Combo.ID, null);
                }
            }
            ArrayList cons = constantMgr.GetList("NotPrintAddr");
            string notPrintString = string.Empty;
            foreach (FS.HISFC.Models.Base.Const dept in cons)
            {
                notPrintString = notPrintString + dept.ID + ",";
            }
            if (IList.Count > 0)
            {
                applyNo += " " + IList[0].ApplyNo;
                
                ArrayList deptMemo = deptStat.LoadByChildren("00", IList[0].ExeDept.ID);
                if (deptMemo.Count > 0)
                {
                    this.lblDeptPlace.Text = (deptMemo[0] as FS.HISFC.Models.Base.DepartmentStat).Memo;
                    if (notPrintString.Contains(IList[0].ReciptDept.ID))
                    {
                        this.lblDeptPlace.Text = "";
                    }
                }
                lblDocName.Text = IList[0].ReciptDoctor.ID + "/" + IList[0].ReciptDoctor.Name;
                this.lblDept.Text = IList[0].ReciptDept.Name; ;//申请科室

                lblExecDept.Text = IList[0].ExeDept.Name;
            }

            lblCheckItem.Text = tempItem;
            //this.lblCheckFee.Text = phaMoney.ToString() + "元";//化验费
            lblCheckFee.Text = "费用合计：" + FS.FrameWork.Public.String.ToSimpleString(AllFee) + "元\r\n（材料费：" + FS.FrameWork.Public.String.ToSimpleString(suFee) + "元）";


            lblSample.Text = sample;
            npbRecipeNo.Text = applyNo;

            //检验申请单上打印备注根根据科室区分
            if ("3028".Equals(IList[0].ReciptDept.ID) || "3003".Equals(IList[0].ReciptDept.ID))//妇科、产科
            {
                lblMark.Text = @"①请到6楼或2楼收费处或自助机上缴费  ②如有妇检类项目，请在妇产科护士站打印条码，然后自行把标本送检验科" + "\r\n" +
          "③其它检验，请拿此单到7楼抽血室打印检验条码，领标本容器  ④抽血、取其它标本 " + "\r\n" + 
          "⑤血液标本由医院统一送检验科，其它标本请自行送检验科";
                lbltips.Text = "特别提示：节假日、下班时间，请到2号楼急诊抽血室打印条码及抽血";
            }
            else if (IList[0].ReciptDept.Name.Contains("急"))//急诊
            {
                lblMark.Text = @"①请到急诊收费处或自助机上缴费  ②请拿此单到急诊抽血室(2号楼一楼抽血室)打印检验条码并领标本容器" + "\r\n" + 
          "③抽血、取其它标本              ④标本由医院统一送检验科";
                lbltips.Text = "";
            }
            else if (notPrintString.Contains(IList[0].ReciptDept.ID))//北院区生殖中心
            {
                lblMark.Text = "";
                lblmark1.Text = "";
                lbltips.Text = "";
            }
            if (lblCheckItem.Text != "" && !isPreview)
            {
                PrintPage();
            }
        }


        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //  print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            //  print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 808, 586));

            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 586, 808));

            print.IsLandScape = true;

            print.SetPageSize(FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.getPrintPage(true));


            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #endregion

        private void GetHospLogo()
        {
            // Common.ComFunc cf = new ComFunc();
            // string erro = "出错";
            // string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            //picLogo.Image = Image.FromFile(imgpath);
            try
            {
                System.IO.MemoryStream image = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)this.orderManager.Hospital).HosLogoImage);
                this.picLogo.Image = Image.FromStream(image);

            }
            catch
            {

            }

        }
















        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion
    }
}
