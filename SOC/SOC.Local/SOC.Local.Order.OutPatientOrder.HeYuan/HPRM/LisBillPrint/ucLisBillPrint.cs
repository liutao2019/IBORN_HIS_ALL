using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.LisBillPrint
{
    //Neusoft.HISFC.BizProcess.Interface.IRecipePrint
    public partial class ucLisBillPrint : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
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
        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();

        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

        Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();

        /// <summary>
        /// 科室分类维护
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager();
        #endregion


        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;


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

        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);
            return 1;
        }

        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
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

            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false); //年龄
            #endregion

            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strDiag = "";
            int i = 1;
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
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
        Neusoft.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IList, bool isPreview)
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
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
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
                tempItem += IList[i].Item.Name + "       " + Neusoft.FrameWork.Public.String.ToSimpleString(IList[i].Item.Qty) + IList[i].Unit + "\r\n";
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
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
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

            if (IList.Count > 0)
            {
                applyNo += " " + IList[0].ApplyNo;

                ArrayList deptMemo = deptStat.LoadByChildren("00", IList[0].ExeDept.ID);
                if (deptMemo.Count > 0)
                {
                    this.lblDeptPlace.Text = (deptMemo[0] as Neusoft.HISFC.Models.Base.DepartmentStat).Memo;
                }
                lblDocName.Text = IList[0].ReciptDoctor.ID + "/" + IList[0].ReciptDoctor.Name;
                this.lblDept.Text = IList[0].ReciptDept.Name; ;//申请科室
            }

            lblCheckItem.Text = tempItem;
            //this.lblCheckFee.Text = phaMoney.ToString() + "元";//化验费
            lblCheckFee.Text = "费用合计：" + Neusoft.FrameWork.Public.String.ToSimpleString(AllFee) + "元\r\n（材料费：" + Neusoft.FrameWork.Public.String.ToSimpleString(suFee) + "元）";


            lblSample.Text = sample;
            npbRecipeNo.Text = applyNo;

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
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //  print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 800));
            //  print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 808, 586));

            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 586, 808));

            print.IsLandScape = true;

            print.SetPageSize(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.getPrintPage(true));


            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common.Function.IsPreview())
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
            // string imgpath = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            //picLogo.Image = Image.FromFile(imgpath);
            try
            {
                System.IO.MemoryStream image = new System.IO.MemoryStream(((Neusoft.HISFC.Models.Base.Hospital)this.orderManager.Hospital).HosLogoImage);
                this.picLogo.Image = Image.FromStream(image);

            }
            catch
            {

            }

        }
















        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion

        #region IOutPatientOrderPrint 成员


        public void SetPage(string pageStr)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
