﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;   //

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY.PacsBillPrint
{
    //FS.HISFC.BizProcess.Interface.IRecipePrint
    public partial class ucPacsBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
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



        /// <summary>
        /// 科室分类维护
        /// </summary>
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        #endregion


        /// <summary>
        /// 科室帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

      

        /// <summary>
        /// 医嘱数组
        /// </summary>
        public ArrayList alOrder = new ArrayList();

        private ArrayList alTemp = new ArrayList();

        
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
      /*  private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }*/



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
          //  this.chkMale.Text = "Male";
          //  this.chkFemale.Text = "Female";
            labTitle.Text = "         Recipe";
         //   lblJS.Text = "";
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


        

        #region IOutPatientOrderPrint 成员

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {

            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);

            return 1;
        }
       // public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, )

       public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
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

           /* if (this.myReg.Pact.PayKind.ID == "03")
            {
                try
                {
                    FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                    FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                }
                catch
                { }
            }*/
           // this.lblMCardNo.Text = myReg.SSN;     //医疗证号
            //年龄按照统一格式
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
            if (this.myReg.Sex.Name == "男")
            {
                this.lblSex.Text = "男";
              
               // this.chkMale.Checked = true;
               //this.chkFemale.Checked = false;
            }
            else
            {
                this.lblSex.Text = "女";
              
                //this.chkMale.Checked = false;
                //this.chkFemale.Checked = true;
            }
            this.npbBarCode.Image = FS.SOC.Local.Order.OutPatientOrder.Common.ComFunc.CreateBarCode(this.myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
            //this.lblMCardNo.Text = this.myReg.SSN;
            this.lblCardNo.Text = myReg.PID.CardNO;
            this.lblTel.Text = myReg.PhoneHome;
       //     this.lblAdd.Text = myReg.AddressHome;
            this.label1.Text = myReg.AddressHome;
            //this.lblAdd.Text = "yuancun";
            #endregion
           
            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        //    this.lblDocName.Text = this.myReg.
            string strDiag = "";
            int i = 1;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； " + "\n";
                    i++;
                    lblDiag.Text = strDiag;
                }
                //this.lblDocName.Text = diag.DiagInfo.Doctor.Name;
                //this.ChkPro.Text = diag.DiagInfo.ICD10.Name;                
            }

          //  lblDiag.Text = strDiag;
           // this.ChkPro.Text = strDiag;
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
            //  decimal ownPhaMoney = 0m;//自费部分
            string tempID = string.Empty;   //临时保存组合号为了区分
            //System.DateTime currentTime = new System.DateTime();
            lbData.Text = IList[0].MOTime.Date.ToString("yyyy") + "-" + IList[0].MOTime.Date.ToString("MM") + "-" + IList[0].MOTime.Date.ToString("dd");
            // lbData.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PrintTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //释放farpoint原有数据...
            //if (this.neuSpread1_Sheet1.RowCount > 0)
            // {
            //     //int tempRowCount = neuSpread1_Sheet1.RowCount;
            //     this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.RowCount);
            //     //this.neuSpread1_Sheet1.Rows.Add(0, tempRowCount);
            // }
            //  this.ChkPro.Text = IList[k].Item.Name;
            string item = string.Empty;
            string itemtitle = string.Empty;
            for (int i = 0; i < IList.Count; i++)
            {
                item += IList[i].Item.Name + (string.IsNullOrEmpty(IList[i].Memo) ? "" : "   [备注]" + IList[i].Memo) + "   " + FS.FrameWork.Public.String.ToSimpleString(IList[i].Qty) + IList[i].Unit + "\n";
                //item = IList[i].Item.Name;
                itemtitle = IList[i].User01;
                this.ChkPro.Text = item;
                //this.labTitle.Text = itemtitle + "申请单";

                this.lblOrderId.Text = IList[i].ID;

                lblDocName.Text = IList[i].ReciptDoctor.Name;
            }

            if (IList.Count > 0)
            {
                FS.FrameWork.Models.NeuObject apply = interMgr.GetConstansObj("ApplyBillClass",(IList[0].Item as FS.HISFC.Models.Fee.Item.Undrug).CheckApplyDept);
                if (!string.IsNullOrEmpty(apply.Memo))
                {
                    lblMemo.Text = apply.Memo;
                }
                this.labTitle.Text = apply.Name + "申请单";

                if (labTitle.Text.Trim() == "申请单")
                {
                    labTitle.Text = "检查申请单";
                }

                this.lblSeeDept.Text = IList[0].ReciptDept.Name;
                this.lblExecDept.Text = IList[0].ExeDept.Name;

                ArrayList deptMemo = deptStat.LoadByChildren("00", IList[0].ExeDept.ID);
                if (deptMemo.Count > 0)
                {
                    this.lblDeptPlace.Text = "请到" + (deptMemo[0] as FS.HISFC.Models.Base.DepartmentStat).Memo + "检查";
                }
            }

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

            //材料费
            decimal suFee = 0;
            //所有检验项目费用（包含材料费）
            decimal AllFee = 0;

            Hashtable hsCombID = new Hashtable();

            for (int i = 0; i < IList.Count; i++)
            {
                // this.ChkPro.Text = IList[i].Item.Name;
                //   this.neuSpread1_Sheet1.Rows.Add(i, 1);
                //   this.neuSpread1_Sheet1.Cells[i, 0].Text = IList[i].Item.Name;
                //this.neuSpread1_Sheet1.Cells[i, 1].Text = IList[i].ExeDept.Name;
                #region 计算费用
                if (tempID != IList[i].Combo.ID)
                {
                    ArrayList alFee;
                    //alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(IList[i].Combo.ID, this.myReg.ID, "ALL");
                    //if (alFee != null && alFee.Count >= 1)
                    //{
                    //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                    //    {
                    //        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                    //        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;
                    //        //   ownPhaMoney += itemlist.FT.OwnCost;

                    //    }
                    //}
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                    {
                        if (hsCombID.Contains(feeItem.Order.Combo.ID))
                        {
                            continue;
                        }
                        else if (feeItem.Order.Combo.ID == IList[i].Combo.ID)
                        {
                            if (feeItem.Item.IsMaterial || !feeItem.Item.SysClass.ID.Equals("UC"))
                            {
                                suFee += feeItem.FT.PayCost + feeItem.FT.OwnCost + feeItem.FT.PubCost;
                            }
                            AllFee += feeItem.FT.PayCost + feeItem.FT.OwnCost + feeItem.FT.PubCost;
                        }
                    }
                }
                tempID = IList[i].Combo.ID;
                #endregion
            }

            this.lbPrice.Text = "合计金额：" + FS.FrameWork.Public.String.ToSimpleString(AllFee) + "元\r\n（材料费：" + FS.FrameWork.Public.String.ToSimpleString(suFee) + "元）";
            //  if (this.neuSpread1_Sheet1.RowCount > 0)
            //  {

            if (!isPreview)
            {
                PrintPage();
            }

            //  }
        }
        
        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
           // print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 586, 848));            

            print.SetPageSize(FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.getPrintPage(false));

            
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

        private void label21_Click(object sender, EventArgs e)
        {

        }




        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        public void SetPage(string pageStr)
        {
            this.lblPage.Visible = true;
            this.lblPage.Text = pageStr;
            return;
        }

        #endregion
    }

   
}

