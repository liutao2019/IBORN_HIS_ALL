using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.PacsBillPrint
{
    //FS.HISFC.BizProcess.Interface.IRecipePrint
    public partial class ucPacsBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl,
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucPacsBillPrint()
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

        #endregion

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper drugQuaulityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool bEnglish = false;

        /// <summary>
        /// 医嘱排序
        /// </summary>
        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 门诊处方调剂实体
        /// </summary>
        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = new FS.HISFC.Models.Pharmacy.DrugRecipe();

        /// <summary>
        /// 药房编码
        /// </summary>
        string drugDept = "";

        /// <summary>
        /// 操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 人员列表
        /// </summary>
        Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper FreHelper
        {
            set
            {
                this.freHelper = value;
            }
        }

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;


        UsageCompare compare = new UsageCompare();
        ArrayList alSort = new ArrayList();

        /// <summary>
        /// 判断是否为双字符的正则表达式
        /// </summary>
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{15,18}$");

        /// <summary>
        /// 医嘱数组
        /// </summary>
        public ArrayList alOrder = new ArrayList();

        private ArrayList alTemp = new ArrayList();

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myReg;

        /// <summary>
        /// 用法列表，用来判断是否是针剂用法
        /// </summary>
        private ArrayList alUsage = null;

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 门诊病历实体
        /// </summary>
        FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
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
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str, int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private ArrayList GetOrderByCombId(FS.HISFC.Models.Order.OutPatient.Order order, ref string usage)
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.Order.OutPatient.Order ord = null;
            if (!hsCombID.Contains(order.Combo.ID))
            {
                for (int i = 0; i < this.alTemp.Count; i++)
                {
                    ord = alTemp[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsCombID.Add(order.Combo.ID, order);
            }
            return al;
        }

        /// <summary>
        /// 转换成英文
        /// </summary>
        private void ChangeToEnglish()
        {
            label14.Text = "FeeType:";
            label9.Text = "MedicalNum:";
            label6.Text = "Name:";
            label7.Text = "Sex:";
            label8.Text = "Age:";
            label10.Text = "Dept:";
            label12.Text = "CardNum:";
            label19.Text = "Diagnose:";
            label31.Text = "Date:";
            label20.Text = "Phone/Address:";
            label22.Text = "Doctor:";
            this.chkMale.Text = "Male";
            this.chkFemale.Text = "Female";
            label1.Text = "         Recipe";
            lblJS.Text = "";
        }

        /// <summary>
        /// 从消耗品和医嘱数组中移除医嘱
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="alOrderAndSub"></param>
        private void RemoveOrderFromArray(ArrayList alOrder, ref ArrayList alOrderAndSub)
        {
            if (alOrder == null || alOrder.Count == 0)
            {
                return;
            }
            if (alOrderAndSub == null || alOrderAndSub.Count == 0)
            {
                return;
            }
            ArrayList alTemp = new ArrayList();
            for (int i = 0; i < alOrderAndSub.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.SequenceNO == item.SequenceNO))
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
            {
                if (item.Item.MinFee.User03 != "1")
                {
                    alTemp.Add(item);
                }
            }
            alOrderAndSub = alTemp;
        }

        /// <summary>
        /// 频次信息
        /// </summary>
        FS.HISFC.Models.Order.Frequency frequencyObj = null;

        /// <summary>
        /// 根据频次获得每天剂数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return -1;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                if (freHelper != null)
                {
                    frequencyObj = freHelper.GetObjectFromID(frequencyID) as FS.HISFC.Models.Order.Frequency;
                }

                if (frequencyObj == null)
                {
                    ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToLower());

                    if (alFrequency != null && alFrequency.Count > 0)
                    {
                        FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                        string[] str = obj.Time.Split('-');
                        return str.Length;
                    }
                }

                return -1;
            }
        }

        #region IOutPatientOrderPrint 成员

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, regObj.User03, isPreview);
            return 1;
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }


        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
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
            if (this.myReg.Pact.PayKind.ID == "03")
            {
                try
                {
                    FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                    FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                }
                catch
                { }
            }
            this.lblMCardNo.Text = myReg.SSN;     //医疗证号
            //年龄按照统一格式
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
            if (this.myReg.Sex.Name == "男")
            {
                this.chkMale.Checked = true;
                this.chkFemale.Checked = false;
            }
            else
            {
                this.chkMale.Checked = false;
                this.chkFemale.Checked = true;
            }

            this.lblMCardNo.Text = this.myReg.SSN;
            this.lblCardNo.Text = myReg.PID.CardNO;
            this.lblCard.Text = "*" + myReg.PID.CardNO + "*";
            this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);
            this.chkMale.Text = "男";
            this.chkFemale.Text = "女";
            if (this.myReg.Pact.PayKind.ID == "01")
            {
                lbFeeType.Text = "自费";
            }
            else if (this.myReg.Pact.PayKind.ID == "02")
            {
                lbFeeType.Text = "医保";
            }
            else
            {
                lbFeeType.Text = "公费";
            }
            if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
            {
                this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
            }
            else
            {
                this.lblTel.Text = myReg.PhoneHome;
            }
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
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； ";
                    i++;
                }
            }
            lblDiag.Text = strDiag;
            #endregion

            this.caseHistory = this.orderManager.QueryCaseHistoryByClinicCode(this.myReg.ID);

            if (this.caseHistory != null)
            {
                lblCaseMain.Text = this.caseHistory.CaseNow + System.Environment.NewLine + this.caseHistory.CheckBody;
                lbMark.Text = this.caseHistory.Memo;
            }

        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> IList, string judPrint, bool isPreview)
        {
            string pStr = string.Empty;//部位
            //decimal phaMoney = 0m;//金额
            //decimal ownPhaMoney = 0m;//自费部分
            string tempID = string.Empty;   //临时保存组合号为了区分
            lbData.Text = IList[0].MOTime.Date.ToString("yyyy") + "/" + IList[0].MOTime.Date.ToString("MM") + "/" + IList[0].MOTime.Date.ToString("dd");
            //释放farpoint原有数据...
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                //int tempRowCount = neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.RowCount);
                //this.neuSpread1_Sheet1.Rows.Add(0, tempRowCount);
            }

            #region 查询所有收费项目（附材）

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            alSubAndOrder = new ArrayList();
            foreach (FS.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
            {
                if (!feeSeq.Contains(OutPatientOrder.ReciptSequence))
                {
                    ArrayList al = outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(OutPatientOrder.Patient.ID, OutPatientOrder.ReciptSequence, "ALL");
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

            Hashtable hsCombNo = new Hashtable();

            decimal allFee = 0;
            decimal subFee = 0;

            ArrayList hsPackage = new ArrayList();
            decimal packagePrice = 0;
            string lastPackageCode = string.Empty;
            int count = 0;
            int curRow = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order detail in IList)
            {
                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //药品直接显示
                    curRow = this.neuSpread1_Sheet1.RowCount++;
                    this.neuSpread1_Sheet1.Cells[curRow, 0].Text = detail.Item.Name;
                    this.neuSpread1_Sheet1.Cells[curRow, 1].Text = (detail.Item.Qty * detail.Item.Price).ToString();
                    this.neuSpread1_Sheet1.Cells[curRow, 2].Text = detail.ExeDept.Name;
                }
                else
                {
                    if ("1".Equals(SOC.HISFC.BizProcess.Cache.Fee.GetItem(detail.Item.ID).UnitFlag))
                    {
                        curRow = this.neuSpread1_Sheet1.RowCount++;
                        this.neuSpread1_Sheet1.Cells[curRow, 0].Text = detail.Item.Name;
                        this.neuSpread1_Sheet1.Cells[curRow, 1].Text = (detail.Item.Qty * detail.Item.Price).ToString();
                        this.neuSpread1_Sheet1.Cells[curRow, 2].Text = detail.ExeDept.Name;
                    }
                    else
                    {
                        //复合项目
                        if (!string.IsNullOrEmpty(detail.ApplyNo))
                        {
                            //有复合合项目ItemCode
                            if (!hsPackage.Contains(detail.ApplyNo + detail.Combo.ID))
                            {
                                if (!string.IsNullOrEmpty(lastPackageCode))
                                {
                                    this.neuSpread1_Sheet1.Cells[curRow, 1].Text = packagePrice.ToString();
                                    packagePrice = 0;
                                }
                                //若复合项目未有
                                lastPackageCode = detail.ApplyNo + detail.Combo.ID;
                                hsPackage.Add(lastPackageCode);
                                curRow = this.neuSpread1_Sheet1.RowCount++;
                                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(detail.ApplyNo).Name;
                                packagePrice += detail.Item.Qty * detail.Item.Price;
                                this.neuSpread1_Sheet1.Cells[curRow, 2].Text = detail.ExeDept.Name;
                            }
                            else
                            {
                                //若复合项目已有
                                packagePrice += detail.Item.Qty * detail.Item.Price;
                            }
                        }
                        else
                        {
                            //无复合合项目ItemCode,即错误情况,直接显示项目
                            curRow = this.neuSpread1_Sheet1.RowCount++;
                            this.neuSpread1_Sheet1.Cells[curRow, 0].Text = detail.Item.Name;
                            this.neuSpread1_Sheet1.Cells[curRow, 1].Text = (detail.Item.Qty * detail.Item.Price).ToString();
                            this.neuSpread1_Sheet1.Cells[curRow, 2].Text = detail.ExeDept.Name;
                        }
                    }
                }
                if (count == IList.Count - 1 || !(detail.ApplyNo + detail.Combo.ID).Equals(lastPackageCode))
                {
                    //最后一个项目或当前项目与上一项目的复合项目不同
                    if (!string.IsNullOrEmpty(detail.ApplyNo))
                    {
                        this.neuSpread1_Sheet1.Cells[curRow, 1].Text = packagePrice.ToString();
                    }
                    packagePrice = 0;
                }
                #region 计算费用
                if (!hsCombNo.Contains(detail.Combo.ID))
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                    {
                        if (feeItem.Order.Combo.ID == detail.Combo.ID)
                        {
                            if (feeItem.Item.IsMaterial)
                            {
                                subFee += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                            }
                            else
                            {
                                foreach (FS.HISFC.Models.Order.OutPatient.Order ord in IList)
                                {
                                    if (ord.Item.ID == feeItem.Item.ID
                                        && ord.ReciptNO == feeItem.RecipeNO
                                        && ord.SequenceNO == feeItem.SequenceNO)
                                    {
                                        allFee += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    hsCombNo.Add(detail.Combo.ID, null);
                }

                //if (tempID != IList[i].Combo.ID)
                //{
                //ArrayList alFee;
                //alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(IList[i].Combo.ID, this.myReg.ID, "ALL");
                //if (alFee != null && alFee.Count >= 1)
                //{
                //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                //    {
                //        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                //        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;
                //        ownPhaMoney += itemlist.FT.OwnCost;
                //    }
                //}
                //}
                tempID = detail.Combo.ID;
                pStr += detail.Sample.Name.ToString();
                #endregion

                this.lblSeeDept.Text = detail.ReciptDept.Name;

                count++;//循环计数
            }
            //this.lbPrice.Text = phaMoney.ToString();
            lbPrice.Text = "费用合计：" + FS.FrameWork.Public.String.ToSimpleString(allFee) + "元\r\n（材料费：" + FS.FrameWork.Public.String.ToSimpleString(subFee) + "元）";

            this.neuLabel5.Text = pStr;
            this.lblTips.Text = "温馨提示：请先到" + IList[0].ExeDept.Name + "划价确认再交费";
            this.label1.Text = IList[0].ExeDept.Name + "申请单";
            if (this.neuSpread1_Sheet1.RowCount > 0 && !isPreview)
            {
                PrintPage(judPrint);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage(string judPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager || judPrint == "BD")
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #endregion

        private void GetHospLogo()
        {
            picbLogo.Image = null;

            Common.ComFunc cf = new FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.ComFunc();
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);

            picbLogo.Image = Image.FromFile(imgpath);
        }

        #region IOutPatientOrderPrint Members


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }

    public class OrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 用法排序：一组内可能用法不一致，此处只显示一个用法
    /// </summary>
    public class UsageCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) < FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 1;
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) == FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}

