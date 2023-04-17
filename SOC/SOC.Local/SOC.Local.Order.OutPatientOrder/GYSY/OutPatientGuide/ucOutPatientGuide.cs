using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.OutPatientGuide
{
    public partial class ucOutPatientGuide : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucOutPatientGuide()
        {
            InitializeComponent();

            unDrugList.AddRange(new string[] { "UL", "UC", "UZ", "UO" });

            drugList.AddRange(new string[] { "P", "PCZ", "PCC" });
        }

        #region 业务层
        /// <summary>
        /// 
        /// </summary>
        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();


        /// <summary>
        /// 
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();


        /// <summary>
        /// 科室分类维护
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager();

        #endregion

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;

        /// <summary>
        /// 药品
        /// </summary>
        List<string> drugList = new List<string>();

        /// <summary>
        /// 非药品
        /// </summary>
        List<string> unDrugList = new List<string>();

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IList, bool isPreview)
        {
            //设置开具日期
            this.lblSeeYear.Text = IList[0].MOTime.Date.ToString("yyyy");
            this.lblSeeMonth.Text = IList[0].MOTime.Date.ToString("MM");
            this.lblSeeDay.Text = IList[0].MOTime.Date.ToString("dd");
            int i = 0;
            //释放farpoint原有数据...
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                //int tempRowCount = neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.RowCount);
                //this.neuSpread1_Sheet1.Rows.Add(0, tempRowCount);
            }
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order OutPatientOrder in IList)
            {
                //非附材，非药品
                if (!OutPatientOrder.Item.IsMaterial && unDrugList.Contains(OutPatientOrder.Item.SysClass.ID.ToString()))
                {
                    this.neuSpread1_Sheet1.Rows.Add(i, 1);
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = OutPatientOrder.Item.Name;
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = OutPatientOrder.Qty + "/"+OutPatientOrder.Item.PriceUnit;
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = OutPatientOrder.ExeDept.Name;

                    //执行地点
                    ArrayList deptMemo = deptStat.LoadByChildren("00", OutPatientOrder.ExeDept.ID);
                    if (deptMemo.Count > 0)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 3].Text =(deptMemo[0] as Neusoft.HISFC.Models.Base.DepartmentStat).Memo ;
                    }

                    i++;
                }

                this.lblSeeDept.Text = OutPatientOrder.ReciptDept.Name;
            }
            if (i>0)
            {
                PrintPage(isPreview);
            }            
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
            try
            {
                GetHospLogo();    //医院名称和LOGO在Xml\\HospitalLogoInfo.xml中读取
                this.lblName.Text = this.myReg.Name;
                if (this.myReg.Pact.PayKind.ID == "03")
                {
                    try
                    {
                        Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                        Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                    }
                    catch
                    { }
                }

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
                this.lblCardNo.Text = myReg.PID.CardNO;
                this.chkMale.Text = "男";
                this.chkFemale.Text = "女";

                this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);

                if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
                {
                    this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
                }
                else
                {
                    this.lblTel.Text = myReg.PhoneHome;
                }
            }
            catch
            { }
        }

        #region 私有方法
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
        /// 获取字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetLength(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }
        
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="judPrint">初打OR补打</param>
        private void PrintPage(bool isPreview)
        {
            //Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 570, 790));
            //print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsDataAutoExtend = false;
            //if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager||judPrint=="BD")
            //{
            //    print.PrintPreview(5, 5, this);
            //}
            //else
            //{
            //    print.PrintPage(5, 5, this);
            //}
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager || isPreview)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        //获取医院名称与logo
        private void GetHospLogo()
        {
            Common.ComFunc cf = new Neusoft.SOC.Local.Order.OutPatientOrder.Common.ComFunc();
            string erro = "出错";
            string imgpath = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo

("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        } 

        #endregion

        #region IOutPatientOrderPrint 成员

        /// <summary>
        /// 医嘱保存完的后续操作
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="IList">医嘱列表</param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);
            return 1;
        }

        /// <summary>
        /// 门诊处方保存完的后续操作
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion        
    }
}
