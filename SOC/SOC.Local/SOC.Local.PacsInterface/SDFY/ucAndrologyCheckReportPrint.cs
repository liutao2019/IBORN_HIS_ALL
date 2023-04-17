using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.PacsInterface.SDFY
{
    /// <summary>
    /// 顺德妇幼男科检查处理单
    /// </summary>
    public partial class ucAndrologyCheckReportPrint : UserControl
    {
        public ucAndrologyCheckReportPrint()
        {
            InitializeComponent();
        }

        #region 域变量
        FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
        /// <summary>
        /// 诊断业务类
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 非药品项目业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 常数业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 项目序号
        /// </summary>
        int iOrder;
        /// <summary>
        /// 是否打印多页
        /// </summary>
        bool isPaging = false;
        /// <summary>
        /// 是否首次打印
        /// </summary>
        bool isFirstPrint = true;
        /// <summary>
        /// 常数集合
        /// </summary>
        ArrayList alCons = new ArrayList();
        /// <summary>
        /// 医院名称
        /// </summary>
        string strHosName = string.Empty;
        /// <summary>
        /// 总金额
        /// </summary>
        decimal dTotCost = 0;
        /// <summary>
        /// 打印最大行数
        /// </summary>
        int iMaxRows = 18;
        #endregion

        #region 属性
        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register regInfo;
        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return regInfo;
            }
            set
            {
                regInfo = value;
                this.SetPatientInfo(regInfo);
            }
        }

        /// <summary>
        /// 检查项目
        /// </summary>
        private ArrayList alPacsItems;
        /// <summary>
        /// 检查项目
        /// </summary>
        public ArrayList AlPacsItems
        {
            get
            {
                return alPacsItems;
            }
            set
            {
                alPacsItems = value;

                this.Clear();
                FS.HISFC.Models.Order.OutPatient.Order orderItem = null;
                string itemName = string.Empty;
                string itemID = string.Empty;
                for (int i = 0; i < alPacsItems.Count; i++)
                {
                    if (iOrder > iMaxRows)
                    {
                        this.lblTotCost.Text = "总计：" + dTotCost.ToString() + "元";
                        this.Print();
                        this.Clear();
                        this.isFirstPrint = false;
                        this.isPaging = true;
                    }
                    orderItem = alPacsItems[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    this.lblApplyDate.Text = orderItem.MOTime.ToString("yyyy-MM-dd");
                    itemName = orderItem.Item.Name + "[" + orderItem.Item.Qty.ToString() + orderItem.Unit + "]";
                    itemID = orderItem.Item.ID;
                    if (this.SetInfo(i + 1, itemName, itemID, orderItem.Item.Qty) == -1)
                    {
                        alPacsItems = null;
                        return;
                    }
                }
                foreach (Control c in this.panel3.Controls)
                {
                    if (c.Name.Contains("lblLisItem") && c.Name.Remove(0, "lblLisItem".Length) == iOrder.ToString())
                    {
                        c.Visible = true;
                        c.Text = "以下空白";
                    }
                }
                this.lblTotCost.Text = "总计：" + dTotCost.ToString() + "元";
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 患者基本信息赋值
        /// </summary>
        /// <param name="regObj"></param>
        private void SetPatientInfo(FS.HISFC.Models.Registration.Register regObj)
        {
            //需要传送医嘱信息
            lblName.Text = regObj.Name;
            lblSex.Text = regObj.Sex.Name;
            lblAge.Text = funMgr.GetAge(regObj.Birthday);
            lblProf.Text = regObj.Profession.Name;
            lblTel.Text = regObj.PhoneHome;
            lblCardNo.Text = regObj.PID.CardNO;
            this.pbxCardNo.Image = this.CreateBarCode(regInfo.PID.CardNO);
            this.lblAddress.Text = regObj.AddressHome;
            lblDept.Text = ((FS.HISFC.Models.Base.Employee)funMgr.Operator).Dept.Name;//regObj.SeeDoct.Dept.Name;
            lblBedNo.Text = "";

            this.lblDiag.Text = "";
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(regObj.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.Memo != null && diag.Memo != "")
                {
                    this.lblDiag.Text += diag.Memo + "、";
                }
                else
                {
                    this.lblDiag.Text += diag.DiagInfo.ICD10.Name;
                }
            }
            this.lblDiag.Text = this.lblDiag.Text.TrimEnd(new char[] { '、' });
        }

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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.pbxCardNo.Size.Width, this.pbxCardNo.Height);
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        private void Clear()
        {
            foreach (Control c in this.panel3.Controls)
            {
                if (c.Name.Contains("lblLisTitle"))
                {
                    c.Visible = false;
                }
                if (c.Name.Contains("lblLisItem"))
                {
                    c.Visible = false;
                    c.Text = "";
                }
            }
            foreach (Control c in this.panel4.Controls)
            {
                if (c.Name.Contains("lblTube"))
                {
                    c.Visible = false;
                }
                if (c.Name.Contains("lblExecDept"))
                {
                    c.Visible = false;
                    c.Text = "";
                }
            }
            this.iOrder = 1;
            this.isFirstPrint = true;
            this.isPaging = false;
            strHosName = this.consMgr.GetHospitalName();
            dTotCost = 0;
        }

        /// <summary>
        /// 打印内容赋值
        /// </summary>
        /// <param name="i">打印序号</param>
        /// <param name="itemName">项目名称</param>
        /// <param name="itemID">项目编码</param>
        /// <returns></returns>
        private int SetInfo(int i, string itemName, string itemID, decimal itemQty)
        {
            FS.HISFC.Models.Fee.Item.Undrug item = null;
            FS.FrameWork.WinForms.Controls.NeuLabel lblObj = null;
            object o = null;

            item = itemMgr.QueryList(itemID, "1")[0] as FS.HISFC.Models.Fee.Item.Undrug;
            if (item == null || item.ID == "")
            {
                this.errInfo = "查找项目" + itemName + "失败！";
                return -1;
            }
            try
            {
                if (itemName.Length > 26)
                {
                    if (iOrder == iMaxRows)
                    {
                        iOrder++;
                        return 1;
                    }
                    o = this.GetType().GetField("lblLisTitle" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblLisItem" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = itemName.Substring(0, 26);

                    o = this.GetType().GetField("lblTube" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = false;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblExecDept" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = (item.Price * itemQty).ToString() + "元";

                    dTotCost += item.Price * itemQty;
                    iOrder++;

                    o = this.GetType().GetField("lblLisTitle" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = false;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblLisItem" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = itemName.Substring(26);

                    o = this.GetType().GetField("lblTube" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = false;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblExecDept" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = false;
                    lblObj.Text = item.Memo;
                }
                else
                {
                    o = this.GetType().GetField("lblLisTitle" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblLisItem" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = itemName;

                    o = this.GetType().GetField("lblTube" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = false;
                    lblObj.Text = i.ToString() + ".";

                    o = this.GetType().GetField("lblExecDept" + iOrder.ToString(),
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                    lblObj = (FS.FrameWork.WinForms.Controls.NeuLabel)o;
                    lblObj.Visible = true;
                    lblObj.Text = (item.Price * itemQty).ToString() + "元";

                    dTotCost += item.Price * itemQty;
                }
                iOrder++;
            }
            catch (Exception ex)
            {
                this.errInfo = "打印内容赋值错误" + ex.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 公有方法
        FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            pSize = pgMgr.GetPageSize("LisReport");

            if (pSize != null)
            {
                print.SetPageSize(pSize);
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                }
            }
            print.PrintDocument.DefaultPageSettings.Landscape = true;
            //print.PrintPreview(this);
            print.PrintPage(580, 0, this);

            return 1;
        }
        #endregion

        #region IPacsReportPrint 成员

        string errInfo = "";

        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        /// <summary>
        /// 住院Pacs申请单打印
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alPacsOrder"></param>
        /// <returns></returns>
        public int PacsReportPrintForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alPacsOrder)
        {
            return 1;
        }

        /// <summary>
        /// 门诊Pacs申请单打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alPacsOrder"></param>
        /// <returns></returns>
        public int PacsReportPrintForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alPacsOrder, string strReportType)
        {
            try
            {
                this.RegInfo = regObj;
                this.AlPacsItems = alPacsOrder;
                //this.lblTitle.Text = strHosName + strReportType + "申请单";

                if (this.AlPacsItems == null)
                {
                    return -1;
                }
                if (this.isFirstPrint)
                {
                    this.Print();
                }
                if (this.isPaging)
                {
                    this.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
